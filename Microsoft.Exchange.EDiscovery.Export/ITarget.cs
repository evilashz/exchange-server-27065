using System;
using System.Collections.Generic;

namespace Microsoft.Exchange.EDiscovery.Export
{
	// Token: 0x02000012 RID: 18
	internal interface ITarget
	{
		// Token: 0x1700001F RID: 31
		// (get) Token: 0x06000068 RID: 104
		IExportContext ExportContext { get; }

		// Token: 0x17000020 RID: 32
		// (get) Token: 0x06000069 RID: 105
		// (set) Token: 0x0600006A RID: 106
		ExportSettings ExportSettings { get; set; }

		// Token: 0x0600006B RID: 107
		IItemIdList CreateItemIdList(string mailboxId, bool isUnsearchable);

		// Token: 0x0600006C RID: 108
		void RemoveItemIdList(string mailboxId, bool isUnsearchable);

		// Token: 0x0600006D RID: 109
		IContextualBatchDataWriter<List<ItemInformation>> CreateDataWriter(IProgressController progressController);

		// Token: 0x0600006E RID: 110
		void Rollback(SourceInformationCollection allSourceInformation);

		// Token: 0x0600006F RID: 111
		IStatusLog GetStatusLog();

		// Token: 0x06000070 RID: 112
		void CheckInitialStatus(SourceInformationCollection allSourceInformation, OperationStatus status);
	}
}
