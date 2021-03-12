using System;

namespace Microsoft.Exchange.Data.Directory.ValidationRules
{
	// Token: 0x02000A2B RID: 2603
	internal class RoleEntryValidationRuleTuple
	{
		// Token: 0x060077D9 RID: 30681 RVA: 0x0018AC64 File Offset: 0x00188E64
		public RoleEntryValidationRuleTuple(ValidationRuleDefinition ruleDefinition, RoleEntry matchingRoleEntry)
		{
			if (ruleDefinition == null)
			{
				throw new ArgumentNullException("ruleDefinition");
			}
			if (null == matchingRoleEntry)
			{
				throw new ArgumentNullException("matchingRoleEntry");
			}
			this.RuleDefinition = ruleDefinition;
			this.MatchingRoleEntry = matchingRoleEntry;
		}

		// Token: 0x17002AC8 RID: 10952
		// (get) Token: 0x060077DA RID: 30682 RVA: 0x0018AC9C File Offset: 0x00188E9C
		// (set) Token: 0x060077DB RID: 30683 RVA: 0x0018ACA4 File Offset: 0x00188EA4
		public ValidationRuleDefinition RuleDefinition { get; private set; }

		// Token: 0x17002AC9 RID: 10953
		// (get) Token: 0x060077DC RID: 30684 RVA: 0x0018ACAD File Offset: 0x00188EAD
		// (set) Token: 0x060077DD RID: 30685 RVA: 0x0018ACB5 File Offset: 0x00188EB5
		public RoleEntry MatchingRoleEntry { get; private set; }
	}
}
