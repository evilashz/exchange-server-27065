using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.ApplicationLogic;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Services;
using Microsoft.Exchange.Services.Core.Types;
using Microsoft.Exchange.Transport.Sync.Common.Subscription.Connect;

namespace Microsoft.Exchange.Services.Core
{
	// Token: 0x0200031D RID: 797
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal sealed class GetPeopleConnectToken : SingleStepServiceCommand<GetPeopleConnectTokenRequest, PeopleConnectionToken>
	{
		// Token: 0x0600168D RID: 5773 RVA: 0x000766A4 File Offset: 0x000748A4
		public GetPeopleConnectToken(CallContext callContext, GetPeopleConnectTokenRequest request) : base(callContext, request)
		{
			this.session = callContext.SessionCache.GetMailboxIdentityMailboxSession();
			this.provider = request.Provider;
		}

		// Token: 0x0600168E RID: 5774 RVA: 0x000766CC File Offset: 0x000748CC
		internal override ServiceResult<PeopleConnectionToken> Execute()
		{
			GetPeopleConnectToken.Tracer.TraceDebug<string>(0L, "GetPeopleConnectToken called for provider '{0}'", this.provider);
			using (IEnumerator<ConnectSubscription> enumerator = new ConnectSubscriptionsEnumerator(this.session, this.provider).GetEnumerator())
			{
				if (enumerator.MoveNext())
				{
					ConnectSubscription connectSubscription = enumerator.Current;
					if (!this.HasAccessToken(connectSubscription))
					{
						throw new PeopleConnectionNoTokenException();
					}
					return new ServiceResult<PeopleConnectionToken>(new PeopleConnectionToken
					{
						AccessToken = connectSubscription.AccessTokenInClearText,
						ApplicationId = connectSubscription.AppId,
						ApplicationSecret = this.ReadAppSecret()
					});
				}
			}
			throw new PeopleConnectionNotFoundException();
		}

		// Token: 0x0600168F RID: 5775 RVA: 0x00076780 File Offset: 0x00074980
		internal override IExchangeWebMethodResponse GetResponse()
		{
			GetPeopleConnectTokenResponse getPeopleConnectTokenResponse = new GetPeopleConnectTokenResponse();
			getPeopleConnectTokenResponse.AddResponse(new GetPeopleConnectTokenResponseMessage(base.Result.Code, base.Result.Error, base.Result.Value));
			return getPeopleConnectTokenResponse;
		}

		// Token: 0x06001690 RID: 5776 RVA: 0x000767C0 File Offset: 0x000749C0
		private bool HasAccessToken(ConnectSubscription subscription)
		{
			if (!subscription.HasAccessToken)
			{
				return false;
			}
			switch (subscription.ConnectState)
			{
			case ConnectState.Connected:
				return true;
			case ConnectState.ConnectedNeedsToken:
				return false;
			}
			return false;
		}

		// Token: 0x06001691 RID: 5777 RVA: 0x000767F8 File Offset: 0x000749F8
		private string ReadAppSecret()
		{
			string appSecretClearText;
			try
			{
				appSecretClearText = CachedPeopleConnectApplicationConfig.Instance.ReadProvider(this.provider).AppSecretClearText;
			}
			catch (ExchangeConfigurationException innerException)
			{
				throw new PeopleConnectApplicationConfigException(innerException);
			}
			return appSecretClearText;
		}

		// Token: 0x04000F26 RID: 3878
		private static readonly Trace Tracer = ExTraceGlobals.ServiceCommandBaseCallTracer;

		// Token: 0x04000F27 RID: 3879
		private readonly MailboxSession session;

		// Token: 0x04000F28 RID: 3880
		private readonly string provider;
	}
}
