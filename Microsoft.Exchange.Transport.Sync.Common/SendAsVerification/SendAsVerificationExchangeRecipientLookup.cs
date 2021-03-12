using System;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Transport.Sync.Common.Logging;

namespace Microsoft.Exchange.Transport.Sync.Common.SendAsVerification
{
	// Token: 0x020000AA RID: 170
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal class SendAsVerificationExchangeRecipientLookup
	{
		// Token: 0x0600042F RID: 1071 RVA: 0x00016FA8 File Offset: 0x000151A8
		internal string ExchangeRecipientFor(ADUser user, SyncLogSession syncLogSession)
		{
			SyncUtilities.ThrowIfArgumentNull("user", user);
			SyncUtilities.ThrowIfArgumentNull("syncLogSession", syncLogSession);
			string result;
			if (!EmailGenerationUtilities.TryGetMicrosoftExchangeRecipientSmtpAddress(user.Session.SessionSettings, syncLogSession, out result))
			{
				throw new FailedToGenerateVerificationEmailException();
			}
			return result;
		}
	}
}
