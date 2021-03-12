using System;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.Recipient;

namespace Microsoft.Exchange.AutoDiscoverV2
{
	// Token: 0x02000010 RID: 16
	internal interface ITenantRepository
	{
		// Token: 0x0600004E RID: 78
		ADRecipient GetOnPremUser(SmtpAddress emailAddress);

		// Token: 0x0600004F RID: 79
		IAutodMiniRecipient GetNextUserFromSortedList(SmtpAddress emailAddress);
	}
}
