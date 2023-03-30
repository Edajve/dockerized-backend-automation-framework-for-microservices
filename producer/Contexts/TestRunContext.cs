using System;
using System.Threading.Tasks;
using producer.TestUtils;

namespace producer.Contexts;

public class TestRunContext
{
    // public static OpinionApplicatorService OpinionApplicatorService;
    public static KafkaContext KafkaContext;

    static TestRunContext()
    {
        // OpinionApplicatorService = new OpinionApplicatorService();
        KafkaContext = new KafkaContext();
    }

    public static async Task InitAsync()
    {
        await KafkaContext.InitAsync();

        // OpinionApplicatorService.Start();

        var isAlive = false;
        // await TestHelper.WaitForConditionAsync(() => { return isAlive = OpinionApplicatorService.IsHealthy(); }, 60000);

        if (!isAlive)
        {
            throw new Exception("Market Orchestrator service is DOWN!");
        }
    }

    public static async Task TearDownAsync()
    {
        await KafkaContext.TearDownAsync();
    }
}