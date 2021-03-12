using System;
using System.Collections.Generic;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Hygiene.Data.Spam
{
	// Token: 0x0200020A RID: 522
	internal class SpamRuleProcessorBlobBatch : ConfigurableBatch
	{
		// Token: 0x060015BF RID: 5567 RVA: 0x00043745 File Offset: 0x00041945
		public SpamRuleProcessorBlobBatch()
		{
		}

		// Token: 0x060015C0 RID: 5568 RVA: 0x00043750 File Offset: 0x00041950
		public SpamRuleProcessorBlobBatch(IEnumerable<SpamRuleProcessorBlob> items)
		{
			BatchPropertyTable batchPropertyTable = new BatchPropertyTable();
			foreach (SpamRuleProcessorBlob spamRuleProcessorBlob in items)
			{
				Guid identity = CombGuidGenerator.NewGuid();
				batchPropertyTable.AddPropertyValue(identity, SpamRuleProcessorBlobSchema.IdProperty, spamRuleProcessorBlob.Id);
				batchPropertyTable.AddPropertyValue(identity, SpamRuleProcessorBlobSchema.ProcessorIdProperty, spamRuleProcessorBlob.ProcessorId);
				batchPropertyTable.AddPropertyValue(identity, SpamRuleProcessorBlobSchema.DataProperty, spamRuleProcessorBlob.Data);
			}
			base.Batch = batchPropertyTable;
		}
	}
}
