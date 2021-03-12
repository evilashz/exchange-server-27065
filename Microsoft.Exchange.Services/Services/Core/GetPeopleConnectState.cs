using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Services;
using Microsoft.Exchange.Services.Core.Types;
using Microsoft.Exchange.Transport.Sync.Common.Subscription.Connect;

namespace Microsoft.Exchange.Services.Core
{
	// Token: 0x0200031C RID: 796
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal sealed class GetPeopleConnectState : SingleStepServiceCommand<GetPeopleConnectStateRequest, PeopleConnectionState>
	{
		// Token: 0x06001688 RID: 5768 RVA: 0x00076584 File Offset: 0x00074784
		public GetPeopleConnectState(CallContext callContext, GetPeopleConnectStateRequest request) : base(callContext, request)
		{
			this.session = callContext.SessionCache.GetMailboxIdentityMailboxSession();
			this.provider = request.Provider;
		}

		// Token: 0x06001689 RID: 5769 RVA: 0x000765AC File Offset: 0x000747AC
		internal override ServiceResult<PeopleConnectionState> Execute()
		{
			GetPeopleConnectState.Tracer.TraceDebug<string>(0L, "GetPeopleConnectState called for provider '{0}'", this.provider);
			using (IEnumerator<ConnectSubscription> enumerator = new ConnectSubscriptionsEnumerator(this.session, this.provider).GetEnumerator())
			{
				if (enumerator.MoveNext())
				{
					ConnectSubscription connectSubscription = enumerator.Current;
					return new ServiceResult<PeopleConnectionState>(GetPeopleConnectState.ConvertToPeopleConnectionState(connectSubscription.ConnectState));
				}
			}
			throw new PeopleConnectionNotFoundException();
		}

		// Token: 0x0600168A RID: 5770 RVA: 0x00076630 File Offset: 0x00074830
		internal override IExchangeWebMethodResponse GetResponse()
		{
			GetPeopleConnectStateResponse getPeopleConnectStateResponse = new GetPeopleConnectStateResponse();
			getPeopleConnectStateResponse.AddResponse(new GetPeopleConnectStateResponseMessage(base.Result.Code, base.Result.Error, base.Result.Value));
			return getPeopleConnectStateResponse;
		}

		// Token: 0x0600168B RID: 5771 RVA: 0x00076670 File Offset: 0x00074870
		private static PeopleConnectionState ConvertToPeopleConnectionState(ConnectState state)
		{
			switch (state)
			{
			case ConnectState.Connected:
				return PeopleConnectionState.Connected;
			case ConnectState.ConnectedNeedsToken:
				return PeopleConnectionState.ConnectedNeedsToken;
			}
			return PeopleConnectionState.Disconnected;
		}

		// Token: 0x04000F23 RID: 3875
		private static readonly Trace Tracer = ExTraceGlobals.ServiceCommandBaseCallTracer;

		// Token: 0x04000F24 RID: 3876
		private readonly MailboxSession session;

		// Token: 0x04000F25 RID: 3877
		private readonly string provider;
	}
}
