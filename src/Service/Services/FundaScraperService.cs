using System.Threading.Tasks;
using Grpc.Core;

namespace FundaScraper
{
    public class FundaScraperService : FundaScraper.FundaScraperBase
    {
        public override Task<GetObjectCountReply> GetObjectCount(
            GetObjectCountRequest request,
            ServerCallContext context)
        {
            return Task.FromResult(new GetObjectCountReply { Count = 1 });
        }
    }
}
