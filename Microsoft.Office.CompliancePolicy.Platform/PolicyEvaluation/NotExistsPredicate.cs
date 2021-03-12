using System;
using System.Collections.Generic;

namespace Microsoft.Office.CompliancePolicy.PolicyEvaluation
{
	// Token: 0x020000D6 RID: 214
	public class NotExistsPredicate : ExistsPredicate
	{
		// Token: 0x0600056F RID: 1391 RVA: 0x00010A4F File Offset: 0x0000EC4F
		public NotExistsPredicate(Property property, List<string> entries) : base(property, entries)
		{
		}

		// Token: 0x1700018C RID: 396
		// (get) Token: 0x06000570 RID: 1392 RVA: 0x00010A59 File Offset: 0x0000EC59
		public override string Name
		{
			get
			{
				return "notExists";
			}
		}

		// Token: 0x06000571 RID: 1393 RVA: 0x00010A60 File Offset: 0x0000EC60
		public override bool Evaluate(PolicyEvaluationContext context)
		{
			return !base.Evaluate(context);
		}
	}
}
