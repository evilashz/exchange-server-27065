using System;
using System.Collections.Generic;
using System.Linq;

namespace Microsoft.Office.CompliancePolicy.PolicyEvaluation
{
	// Token: 0x020000CE RID: 206
	public class ExistsPredicate : PredicateCondition
	{
		// Token: 0x06000548 RID: 1352 RVA: 0x00010410 File Offset: 0x0000E610
		public ExistsPredicate(Property property, List<string> entries) : base(property, entries)
		{
		}

		// Token: 0x1700017C RID: 380
		// (get) Token: 0x06000549 RID: 1353 RVA: 0x0001041A File Offset: 0x0000E61A
		public override string Name
		{
			get
			{
				return "exists";
			}
		}

		// Token: 0x0600054A RID: 1354 RVA: 0x00010424 File Offset: 0x0000E624
		public override bool Evaluate(PolicyEvaluationContext context)
		{
			object value = base.Property.GetValue(context);
			if (value == null)
			{
				return false;
			}
			IEnumerable<string> enumerable = value as IEnumerable<string>;
			return enumerable == null || enumerable.Any<string>();
		}

		// Token: 0x0600054B RID: 1355 RVA: 0x00010455 File Offset: 0x0000E655
		protected override Value BuildValue(List<string> entries)
		{
			if (entries.Count != 0)
			{
				throw new CompliancePolicyValidationException(string.Format("Predicate '{0}' does not support values", this.Name));
			}
			return null;
		}
	}
}
