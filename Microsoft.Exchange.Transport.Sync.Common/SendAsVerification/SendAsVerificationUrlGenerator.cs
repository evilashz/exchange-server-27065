using System;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Transport.Sync.Common.Logging;
using Microsoft.Exchange.Transport.Sync.Common.Subscription;

namespace Microsoft.Exchange.Transport.Sync.Common.SendAsVerification
{
	// Token: 0x020000AB RID: 171
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal class SendAsVerificationUrlGenerator
	{
		// Token: 0x06000431 RID: 1073 RVA: 0x00016FF0 File Offset: 0x000151F0
		internal string GenerateURLFor(ExchangePrincipal subscriptionExchangePrincipal, AggregationSubscriptionType subscriptionType, Guid subscriptionGuid, Guid sharedSecret, SyncLogSession syncLogSession)
		{
			string result;
			if (!EcpUtilities.TryGetSendAsVerificationUrl(subscriptionExchangePrincipal, (int)subscriptionType, subscriptionGuid, sharedSecret, syncLogSession, out result))
			{
				throw new FailedToGenerateVerificationEmailException();
			}
			return result;
		}
	}
}
