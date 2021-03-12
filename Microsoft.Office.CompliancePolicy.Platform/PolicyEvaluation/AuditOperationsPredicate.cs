using System;
using System.Collections.Generic;

namespace Microsoft.Office.CompliancePolicy.PolicyEvaluation
{
	// Token: 0x020000C9 RID: 201
	public class AuditOperationsPredicate : PredicateCondition
	{
		// Token: 0x0600051E RID: 1310 RVA: 0x0000F806 File Offset: 0x0000DA06
		public AuditOperationsPredicate(List<string> operations) : this(new Property("NotUsed", typeof(string)), operations)
		{
		}

		// Token: 0x0600051F RID: 1311 RVA: 0x0000F823 File Offset: 0x0000DA23
		internal AuditOperationsPredicate(Property property, List<string> operations) : base(property, operations)
		{
		}

		// Token: 0x1700016D RID: 365
		// (get) Token: 0x06000520 RID: 1312 RVA: 0x0000F82D File Offset: 0x0000DA2D
		public override string Name
		{
			get
			{
				return "auditOperations";
			}
		}

		// Token: 0x1700016E RID: 366
		// (get) Token: 0x06000521 RID: 1313 RVA: 0x0000F834 File Offset: 0x0000DA34
		public override ConditionType ConditionType
		{
			get
			{
				return ConditionType.Predicate;
			}
		}

		// Token: 0x06000522 RID: 1314 RVA: 0x0000F837 File Offset: 0x0000DA37
		public override bool Evaluate(PolicyEvaluationContext context)
		{
			return false;
		}
	}
}
