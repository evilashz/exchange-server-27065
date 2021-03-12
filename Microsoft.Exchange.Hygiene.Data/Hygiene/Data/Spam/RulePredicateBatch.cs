using System;
using System.Collections.Generic;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Hygiene.Data.Spam
{
	// Token: 0x020001F8 RID: 504
	internal class RulePredicateBatch : ConfigurableBatch
	{
		// Token: 0x06001523 RID: 5411 RVA: 0x000423B3 File Offset: 0x000405B3
		public RulePredicateBatch()
		{
		}

		// Token: 0x06001524 RID: 5412 RVA: 0x000423BC File Offset: 0x000405BC
		public RulePredicateBatch(IEnumerable<RulePredicate> items)
		{
			BatchPropertyTable batchPropertyTable = new BatchPropertyTable();
			foreach (RulePredicate rulePredicate in items)
			{
				Guid identity = CombGuidGenerator.NewGuid();
				batchPropertyTable.AddPropertyValue(identity, RulePredicate.IDProperty, rulePredicate.ID);
				batchPropertyTable.AddPropertyValue(identity, RulePredicate.PredicateIDProperty, rulePredicate.PredicateID);
				batchPropertyTable.AddPropertyValue(identity, RulePredicate.PredicateTypeProperty, rulePredicate.PredicateType);
				if (rulePredicate.Sequence != null)
				{
					batchPropertyTable.AddPropertyValue(identity, RulePredicate.SequenceProperty, rulePredicate.Sequence);
				}
				if (rulePredicate.ParentID != null)
				{
					batchPropertyTable.AddPropertyValue(identity, RulePredicate.ParentIDProperty, rulePredicate.ParentID);
				}
				if (rulePredicate.ProcessorID != null)
				{
					batchPropertyTable.AddPropertyValue(identity, RulePredicate.ProcessorIdProperty, rulePredicate.ProcessorID);
				}
			}
			base.Batch = batchPropertyTable;
		}
	}
}
