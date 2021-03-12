using System;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.Recipient;

namespace Microsoft.Exchange.AutoDiscoverV2
{
	// Token: 0x02000004 RID: 4
	public interface IAutodMiniRecipient
	{
		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000010 RID: 16
		SmtpProxyAddress ExternalEmailAddress { get; }

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000011 RID: 17
		SmtpAddress PrimarySmtpAddress { get; }

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x06000012 RID: 18
		string UserPrincipalName { get; }

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x06000013 RID: 19
		RecipientType RecipientType { get; }
	}
}
