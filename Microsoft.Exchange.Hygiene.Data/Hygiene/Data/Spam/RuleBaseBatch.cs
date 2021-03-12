using System;
using System.Collections.Generic;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Hygiene.Data.Spam
{
	// Token: 0x020001EF RID: 495
	internal class RuleBaseBatch : ConfigurableBatch
	{
		// Token: 0x060014E1 RID: 5345 RVA: 0x00041CAF File Offset: 0x0003FEAF
		public RuleBaseBatch()
		{
		}

		// Token: 0x060014E2 RID: 5346 RVA: 0x00041CB8 File Offset: 0x0003FEB8
		public RuleBaseBatch(IEnumerable<RuleBase> items)
		{
			BatchPropertyTable batchPropertyTable = new BatchPropertyTable();
			foreach (RuleBase ruleBase in items)
			{
				Guid identity = CombGuidGenerator.NewGuid();
				batchPropertyTable.AddPropertyValue(identity, RuleBase.IDProperty, ruleBase.ID);
				batchPropertyTable.AddPropertyValue(identity, RuleBase.RuleIDProperty, ruleBase.RuleID);
				batchPropertyTable.AddPropertyValue(identity, RuleBase.RuleTypeProperty, ruleBase.RuleType);
				batchPropertyTable.AddPropertyValue(identity, RuleBase.ScopeIDProperty, ruleBase.ScopeID);
				batchPropertyTable.AddPropertyValue(identity, RuleBase.GroupIDProperty, ruleBase.GroupID);
				batchPropertyTable.AddPropertyValue(identity, RuleBase.SequenceProperty, ruleBase.Sequence);
				batchPropertyTable.AddPropertyValue(identity, RuleBase.IsPersistentProperty, ruleBase.IsPersistent);
				batchPropertyTable.AddPropertyValue(identity, RuleBase.IsActiveProperty, ruleBase.IsActive);
				if (ruleBase.State != null)
				{
					batchPropertyTable.AddPropertyValue(identity, RuleBase.StateProperty, ruleBase.State);
				}
				if (ruleBase.AddedVersion != null)
				{
					batchPropertyTable.AddPropertyValue(identity, RuleBase.AddedVersionProperty, ruleBase.AddedVersion);
				}
				if (ruleBase.RemovedVersion != null)
				{
					batchPropertyTable.AddPropertyValue(identity, RuleBase.RemovedVersionProperty, ruleBase.RemovedVersion);
				}
			}
			base.Batch = batchPropertyTable;
		}
	}
}
