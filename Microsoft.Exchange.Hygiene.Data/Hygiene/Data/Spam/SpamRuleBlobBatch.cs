using System;
using System.Collections.Generic;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Hygiene.Data.Spam
{
	// Token: 0x02000204 RID: 516
	internal class SpamRuleBlobBatch : ConfigurableBatch
	{
		// Token: 0x06001595 RID: 5525 RVA: 0x000432C1 File Offset: 0x000414C1
		public SpamRuleBlobBatch()
		{
		}

		// Token: 0x06001596 RID: 5526 RVA: 0x000432CC File Offset: 0x000414CC
		public SpamRuleBlobBatch(IEnumerable<SpamRuleBlob> items)
		{
			BatchPropertyTable batchPropertyTable = new BatchPropertyTable();
			foreach (SpamRuleBlob spamRuleBlob in items)
			{
				Guid identity = CombGuidGenerator.NewGuid();
				batchPropertyTable.AddPropertyValue(identity, SpamRuleBlobSchema.IdProperty, spamRuleBlob.Id);
				batchPropertyTable.AddPropertyValue(identity, SpamRuleBlobSchema.RuleIdProperty, spamRuleBlob.RuleId);
				batchPropertyTable.AddPropertyValue(identity, SpamRuleBlobSchema.GroupIdProperty, spamRuleBlob.GroupId);
				batchPropertyTable.AddPropertyValue(identity, SpamRuleBlobSchema.ScopeIdProperty, spamRuleBlob.ScopeId);
				batchPropertyTable.AddPropertyValue(identity, SpamRuleBlobSchema.RuleDataProperty, spamRuleBlob.RuleData);
				batchPropertyTable.AddPropertyValue(identity, SpamRuleBlobSchema.RuleMetaDataProperty, spamRuleBlob.RuleMetaData);
				batchPropertyTable.AddPropertyValue(identity, SpamRuleBlobSchema.PriorityProperty, spamRuleBlob.Priority);
				batchPropertyTable.AddPropertyValue(identity, SpamRuleBlobSchema.PublishingStateProperty, spamRuleBlob.PublishingState);
				batchPropertyTable.AddPropertyValue(identity, SpamRuleBlobSchema.ProcessorDataProperty, spamRuleBlob.ProcessorData);
				if (spamRuleBlob.DeletedDatetime != null)
				{
					batchPropertyTable.AddPropertyValue(identity, SpamRuleBlobSchema.DeletedDatetimeProperty, spamRuleBlob.DeletedDatetime);
				}
			}
			base.Batch = batchPropertyTable;
		}
	}
}
