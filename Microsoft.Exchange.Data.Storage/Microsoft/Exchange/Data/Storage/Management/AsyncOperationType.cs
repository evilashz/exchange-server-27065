using System;

namespace Microsoft.Exchange.Data.Storage.Management
{
	// Token: 0x020009BC RID: 2492
	public enum AsyncOperationType
	{
		// Token: 0x040032BB RID: 12987
		[LocDescription(ServerStrings.IDs.AsyncOperationTypeUnknown)]
		Unknown,
		// Token: 0x040032BC RID: 12988
		[LocDescription(ServerStrings.IDs.AsyncOperationTypeImportPST)]
		ImportPST = 3,
		// Token: 0x040032BD RID: 12989
		[LocDescription(ServerStrings.IDs.AsyncOperationTypeExportPST)]
		ExportPST,
		// Token: 0x040032BE RID: 12990
		[LocDescription(ServerStrings.IDs.AsyncOperationTypeMigration)]
		Migration = 6,
		// Token: 0x040032BF RID: 12991
		[LocDescription(ServerStrings.IDs.AsyncOperationTypeMailboxRestore)]
		MailboxRestore = 8,
		// Token: 0x040032C0 RID: 12992
		[LocDescription(ServerStrings.IDs.AsyncOperationTypeCertExpiry)]
		CertExpiry
	}
}
