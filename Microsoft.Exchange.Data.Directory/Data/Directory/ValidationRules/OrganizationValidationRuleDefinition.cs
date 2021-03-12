using System;
using System.Collections.Generic;

namespace Microsoft.Exchange.Data.Directory.ValidationRules
{
	// Token: 0x02000A33 RID: 2611
	internal class OrganizationValidationRuleDefinition : ValidationRuleDefinition
	{
		// Token: 0x0600780B RID: 30731 RVA: 0x0018B8EA File Offset: 0x00189AEA
		public OrganizationValidationRuleDefinition(string name, string feature, ValidationRuleSkus applicableSku, List<RoleEntry> applicableRoleEntries, ValidationErrorStringProvider errorStringProvider, List<ValidationRuleExpression> restrictionExpressions, List<ValidationRuleExpression> overridingAllowExpressions) : base(name, feature, applicableSku, applicableRoleEntries, new List<Capability>(), new List<Capability>(), errorStringProvider)
		{
			this.RestrictionExpressions = restrictionExpressions;
			this.OverridingAllowExpressions = overridingAllowExpressions;
		}

		// Token: 0x17002AD8 RID: 10968
		// (get) Token: 0x0600780C RID: 30732 RVA: 0x0018B913 File Offset: 0x00189B13
		// (set) Token: 0x0600780D RID: 30733 RVA: 0x0018B91B File Offset: 0x00189B1B
		public List<ValidationRuleExpression> RestrictionExpressions { get; private set; }

		// Token: 0x17002AD9 RID: 10969
		// (get) Token: 0x0600780E RID: 30734 RVA: 0x0018B924 File Offset: 0x00189B24
		// (set) Token: 0x0600780F RID: 30735 RVA: 0x0018B92C File Offset: 0x00189B2C
		public List<ValidationRuleExpression> OverridingAllowExpressions { get; private set; }
	}
}
