using System;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x0200001E RID: 30
	internal interface IDataExport
	{
		// Token: 0x060001C9 RID: 457
		DataExportBatch ExportData();

		// Token: 0x060001CA RID: 458
		void CancelExport();
	}
}
