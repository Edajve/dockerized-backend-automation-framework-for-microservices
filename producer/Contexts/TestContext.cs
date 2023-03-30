using System.Threading.Tasks;
using producer.Infrastructure;

namespace producer.Contexts;

    public class TestContext
    {
        public KafkaProducer KafkaProducer;
        public KafkaContext KafkaContext;
        // public OpinionApplicatorConsumingService OpinionApplicatorConsumingService;
        // public OpinionApplicatorService OpinionApplicatorService;
        // public BlendedOpinion BlendedOpinion;
        // public ApplicableOpinion ApplicableOpinion;


        public TestContext()
        {
            KafkaProducer = new KafkaProducer();
            KafkaContext = new KafkaContext();
            // OpinionApplicatorConsumingService = new OpinionApplicatorConsumingService();
            // OpinionApplicatorService = new OpinionApplicatorService();
            // BlendedOpinion = new BlendedOpinion();
            // ApplicableOpinion = new ApplicableOpinion();
        }

        // public async Task InitAsync()
        // {
        //     await Task.Run(() => OpinionApplicatorConsumingService.Start());
        // }
        //
        // public async Task TearDownAsync()
        // {
        //     await Task.Run(() => OpinionApplicatorConsumingService.Stop());
        // }
    }