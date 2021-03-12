using System;
using System.Collections.Generic;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Hygiene.Data.Spam;

namespace Microsoft.Exchange.Hygiene.Data.Kes
{
	// Token: 0x0200020E RID: 526
	internal class SyncWatermarkBatch : ConfigurableBatch
	{
		// Token: 0x060015F3 RID: 5619 RVA: 0x000447B2 File Offset: 0x000429B2
		public SyncWatermarkBatch()
		{
		}

		// Token: 0x060015F4 RID: 5620 RVA: 0x000447BC File Offset: 0x000429BC
		public SyncWatermarkBatch(IEnumerable<SyncWatermark> items)
		{
			BatchPropertyTable batchPropertyTable = new BatchPropertyTable();
			foreach (SyncWatermark syncWatermark in items)
			{
				Guid identity = CombGuidGenerator.NewGuid();
				batchPropertyTable.AddPropertyValue(identity, SyncWatermark.IdProperty, syncWatermark.Id);
				batchPropertyTable.AddPropertyValue(identity, SyncWatermark.SyncContextProperty, syncWatermark.SyncContext);
				batchPropertyTable.AddPropertyValue(identity, SyncWatermark.DataProperty, syncWatermark.Data);
				batchPropertyTable.AddPropertyValue(identity, SyncWatermark.OwnerProperty, syncWatermark.Owner);
			}
			base.Batch = batchPropertyTable;
		}
	}
}
