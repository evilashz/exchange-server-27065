using System;
using Microsoft.Exchange.Diagnostics.WorkloadManagement;

namespace Microsoft.Exchange.Services.Diagnostics
{
	// Token: 0x0200005A RID: 90
	internal enum SyncPersonaContactsMetadata
	{
		// Token: 0x04000503 RID: 1283
		[DisplayName("SPe", "TT")]
		TotalTime,
		// Token: 0x04000504 RID: 1284
		[DisplayName("SPe", "IT")]
		IcsTime,
		// Token: 0x04000505 RID: 1285
		[DisplayName("SPe", "CT")]
		CatchUpTime,
		// Token: 0x04000506 RID: 1286
		[DisplayName("SPe", "QS")]
		QuerySync,
		// Token: 0x04000507 RID: 1287
		[DisplayName("SPe", "IS")]
		IcsSync,
		// Token: 0x04000508 RID: 1288
		[DisplayName("SPe", "FC")]
		FolderCount,
		// Token: 0x04000509 RID: 1289
		[DisplayName("SPe", "ENUM")]
		ContactsEnumerated,
		// Token: 0x0400050A RID: 1290
		[DisplayName("SPe", "ICS")]
		IcsChangesProcessed,
		// Token: 0x0400050B RID: 1291
		[DisplayName("SPe", "SSS")]
		SyncStateSize,
		// Token: 0x0400050C RID: 1292
		[DisplayName("SPe", "SSH")]
		SyncStateHash,
		// Token: 0x0400050D RID: 1293
		[DisplayName("SPe", "PC")]
		PeopleCount,
		// Token: 0x0400050E RID: 1294
		[DisplayName("SPe", "DPC")]
		DeletedPeopleCount,
		// Token: 0x0400050F RID: 1295
		[DisplayName("SPe", "Last")]
		IncludesLastItemInRange,
		// Token: 0x04000510 RID: 1296
		[DisplayName("SPe", "Bad")]
		InvalidContacts,
		// Token: 0x04000511 RID: 1297
		[DisplayName("SPe", "XcptId")]
		ExceptionPersonId,
		// Token: 0x04000512 RID: 1298
		[DisplayName("SPe", "Spct")]
		SyncPersonaContactsType
	}
}
