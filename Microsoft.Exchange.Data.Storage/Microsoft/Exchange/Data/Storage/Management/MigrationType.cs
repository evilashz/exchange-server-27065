using System;

namespace Microsoft.Exchange.Data.Storage.Management
{
	// Token: 0x02000A3E RID: 2622
	[Flags]
	[Serializable]
	public enum MigrationType
	{
		// Token: 0x0400367C RID: 13948
		[LocDescription(ServerStrings.IDs.MigrationTypeNone)]
		None = 0,
		// Token: 0x0400367D RID: 13949
		[LocDescription(ServerStrings.IDs.MigrationTypeIMAP)]
		IMAP = 1,
		// Token: 0x0400367E RID: 13950
		[LocDescription(ServerStrings.IDs.MigrationTypeXO1)]
		XO1 = 2,
		// Token: 0x0400367F RID: 13951
		[LocDescription(ServerStrings.IDs.MigrationTypeExchangeOutlookAnywhere)]
		ExchangeOutlookAnywhere = 4,
		// Token: 0x04003680 RID: 13952
		[LocDescription(ServerStrings.IDs.MigrationTypeBulkProvisioning)]
		BulkProvisioning = 8,
		// Token: 0x04003681 RID: 13953
		[LocDescription(ServerStrings.IDs.MigrationTypeExchangeRemoteMove)]
		ExchangeRemoteMove = 16,
		// Token: 0x04003682 RID: 13954
		[LocDescription(ServerStrings.IDs.MigrationTypeExchangeLocalMove)]
		ExchangeLocalMove = 32,
		// Token: 0x04003683 RID: 13955
		[LocDescription(ServerStrings.IDs.MigrationTypePSTImport)]
		PSTImport = 64,
		// Token: 0x04003684 RID: 13956
		[LocDescription(ServerStrings.IDs.MigrationTypePublicFolder)]
		PublicFolder = 128
	}
}
