using System;

namespace Microsoft.Exchange.Data
{
	// Token: 0x02000129 RID: 297
	public enum ClientAccessProtocol
	{
		// Token: 0x04000653 RID: 1619
		[LocDescription(DataStrings.IDs.ClientAccessProtocolEWS)]
		ExchangeWebServices,
		// Token: 0x04000654 RID: 1620
		[LocDescription(DataStrings.IDs.ClientAccessProtocolRPS)]
		RemotePowerShell,
		// Token: 0x04000655 RID: 1621
		[LocDescription(DataStrings.IDs.ClientAccessProtocolOA)]
		OutlookAnywhere,
		// Token: 0x04000656 RID: 1622
		[LocDescription(DataStrings.IDs.ClientAccessProtocolPOP3)]
		POP3,
		// Token: 0x04000657 RID: 1623
		[LocDescription(DataStrings.IDs.ClientAccessProtocolIMAP4)]
		IMAP4,
		// Token: 0x04000658 RID: 1624
		[LocDescription(DataStrings.IDs.ClientAccessProtocolOWA)]
		OutlookWebApp,
		// Token: 0x04000659 RID: 1625
		[LocDescription(DataStrings.IDs.ClientAccessProtocolEAC)]
		ExchangeAdminCenter,
		// Token: 0x0400065A RID: 1626
		[LocDescription(DataStrings.IDs.ClientAccessProtocolEAS)]
		ExchangeActiveSync,
		// Token: 0x0400065B RID: 1627
		[LocDescription(DataStrings.IDs.ClientAccessProtocolOAB)]
		OfflineAddressBook,
		// Token: 0x0400065C RID: 1628
		[LocDescription(DataStrings.IDs.ClientAccessProtocolPSWS)]
		PowerShellWebServices
	}
}
