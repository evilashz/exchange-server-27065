using System;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x02000554 RID: 1364
	[Flags]
	public enum AuthMechanisms
	{
		// Token: 0x04002974 RID: 10612
		[LocDescription(DirectoryStrings.IDs.ReceiveAuthMechanismNone)]
		None = 0,
		// Token: 0x04002975 RID: 10613
		[LocDescription(DirectoryStrings.IDs.ReceiveAuthMechanismTls)]
		Tls = 1,
		// Token: 0x04002976 RID: 10614
		[LocDescription(DirectoryStrings.IDs.ReceiveAuthMechanismIntegrated)]
		Integrated = 2,
		// Token: 0x04002977 RID: 10615
		[LocDescription(DirectoryStrings.IDs.ReceiveAuthMechanismBasicAuth)]
		BasicAuth = 4,
		// Token: 0x04002978 RID: 10616
		[LocDescription(DirectoryStrings.IDs.ReceiveAuthMechanismBasicAuthPlusTls)]
		BasicAuthRequireTLS = 8,
		// Token: 0x04002979 RID: 10617
		[LocDescription(DirectoryStrings.IDs.ReceiveAuthMechanismExchangeServer)]
		ExchangeServer = 16,
		// Token: 0x0400297A RID: 10618
		[LocDescription(DirectoryStrings.IDs.ReceiveAuthMechanismExternalAuthoritative)]
		ExternalAuthoritative = 32
	}
}
