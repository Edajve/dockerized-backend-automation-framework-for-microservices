using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using TechTalk.SpecFlow;

namespace producer.TestUtils
{
    public static class TestHelper
    {
        public static async Task WaitForConditionAsync(Func<bool> conditionFunc, int maxWaitTimeMs = 10000)
        {
            var stopwatch = new Stopwatch();

            stopwatch.Start();

            while (stopwatch.ElapsedMilliseconds < maxWaitTimeMs)
            {
                if (conditionFunc())
                {
                    break;
                }
                await Task.Delay(1000);
            }

            stopwatch.Stop();
        }

        public static void WaitForCondition(Func<bool> conditionFunc, int maxWaitTimeMs = 10000)
        {
            Task.WaitAll(WaitForConditionAsync(conditionFunc, maxWaitTimeMs));
        }

        public static IDictionary<string, Item> AsDictionary(this Table table)
        {
            if (table.Rows.Count != 1)
            {
                throw new Exception("Cannot generate a dictionary from this table");
            }

            var keys = table.Header.ToList();
            var values = table.Rows.First();

            var dictionary = new Dictionary<string, Item>();

            if (values.Count != keys.Count)
            {
                throw new Exception($"Mismatch between number of header cells and number of row cells. {keys.Count} header cells vs {values.Count} rows cells");
            }

            for (var index = 0; index < keys.Count; index++)
            {
                dictionary.Add(keys[index], new Item(values[index]));
            }

            return dictionary;
        }

        public static List<IDictionary<string, Item>> AsDictionaryList(this Table table)
        {
            if (table.Rows.Count < 1)
            {
                throw new Exception("Cannot generate a dictionary list from this table");
            }

            var dictionaryList = new List<IDictionary<string, Item>>();

            var keys = table.Header.ToList();

            foreach (var tableRow in table.Rows)
            {
                var dictionary = new Dictionary<string, Item>();

                if (tableRow.Count != keys.Count)
                {
                    throw new Exception($"Mismatch between number of header cells and number of row cells. {keys.Count} header cells vs {tableRow.Count} rows cells");
                }

                for (var index = 0; index < keys.Count; index++)
                {
                    dictionary.Add(keys[index], new Item(tableRow[index]));
                }

                dictionaryList.Add(dictionary);
            }

            return dictionaryList;
        }

        public static IDictionary<string, Item> AsPivotedDictionary(this Table table)
        {
            if (table.Header.Count != 2)
            {
                throw new Exception("Cannot generate a dictionary from this table");
            }

            var dictionary = new Dictionary<string, Item>();

            for (var index = 0; index < table.RowCount; index++)
            {
                dictionary.Add(table.Rows[index][0], new Item(table.Rows[index][1]));
            }

            return dictionary;
        }

        public class Item
        {
            public Item(string value)
            {
                Value = value;
            }

            public string Value { get; set; }

            public T As<T>()
            {
                if (typeof(T).IsEnum)
                {
                    return (T)Enum.Parse(typeof(T), Value);
                }

                if (typeof(T) == typeof(Guid))
                {
                    return (T)Convert.ChangeType(Guid.Parse(Value), typeof(T));
                }

                return (T)Convert.ChangeType(Value, typeof(T));
            }
        }
    }
}