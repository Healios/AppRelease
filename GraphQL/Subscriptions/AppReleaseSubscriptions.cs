using Core.Models;
using HotChocolate;
using HotChocolate.Subscriptions;
using HotChocolate.Types;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GraphQL.Subscriptions
{
    public class AppReleaseSubscriptions
    {
        [SubscribeAndResolve]
        public async ValueTask<IAsyncEnumerable<AppRelease>> OnNewAppRelease([Service] ITopicEventReceiver receiver) => await receiver.SubscribeAsync<string, AppRelease>("OnNewAppRelease");
    }
}
