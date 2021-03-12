using System;
using System.Collections.Generic;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Hygiene.Data.Spam
{
	// Token: 0x020001FA RID: 506
	internal class RuleUpdateBatch : ConfigurableBatch
	{
		// Token: 0x06001538 RID: 5432 RVA: 0x00042709 File Offset: 0x00040909
		public RuleUpdateBatch()
		{
		}

		// Token: 0x06001539 RID: 5433 RVA: 0x00042714 File Offset: 0x00040914
		public RuleUpdateBatch(IEnumerable<RuleUpdate> items)
		{
			BatchPropertyTable batchPropertyTable = new BatchPropertyTable();
			foreach (RuleUpdate ruleUpdate in items)
			{
				Guid identity = CombGuidGenerator.NewGuid();
				if (ruleUpdate.ID != null)
				{
					batchPropertyTable.AddPropertyValue(identity, RuleUpdate.IDProperty, ruleUpdate.ID);
				}
				if (ruleUpdate.RuleID != null)
				{
					batchPropertyTable.AddPropertyValue(identity, RuleUpdate.RuleIDProperty, ruleUpdate.RuleID);
				}
				if (ruleUpdate.RuleType != null)
				{
					batchPropertyTable.AddPropertyValue(identity, RuleUpdate.RuleTypeProperty, ruleUpdate.RuleType);
				}
				if (ruleUpdate.IsPersistent != null)
				{
					batchPropertyTable.AddPropertyValue(identity, RuleUpdate.IsPersistentProperty, ruleUpdate.IsPersistent);
				}
				if (ruleUpdate.IsActive != null)
				{
					batchPropertyTable.AddPropertyValue(identity, RuleUpdate.IsActiveProperty, ruleUpdate.IsActive);
				}
				if (ruleUpdate.State != null)
				{
					batchPropertyTable.AddPropertyValue(identity, RuleUpdate.StateProperty, ruleUpdate.State);
				}
				if (ruleUpdate.AddedVersion != null)
				{
					batchPropertyTable.AddPropertyValue(identity, RuleUpdate.AddedVersionProperty, ruleUpdate.AddedVersion);
				}
				if (ruleUpdate.RemovedVersion != null)
				{
					batchPropertyTable.AddPropertyValue(identity, RuleUpdate.RemovedVersionProperty, ruleUpdate.RemovedVersion);
				}
			}
			base.Batch = batchPropertyTable;
		}
	}
}
