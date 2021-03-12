using System;
using Microsoft.Office.CompliancePolicy.ComplianceData;
using Microsoft.Office.CompliancePolicy.PolicyConfiguration;
using Microsoft.Office.CompliancePolicy.PolicyEvaluation;

namespace Microsoft.Office.CompliancePolicy.ComplianceTask
{
	// Token: 0x02000062 RID: 98
	public abstract class ComplianceServiceProvider
	{
		// Token: 0x060002AD RID: 685
		public abstract PolicyConfigProvider GetPolicyStore(ComplianceItemContainer rootContainer);

		// Token: 0x060002AE RID: 686
		public abstract PolicyConfigProvider GetPolicyStore(string tenantId);

		// Token: 0x060002AF RID: 687
		public abstract ExecutionLog GetExecutionLog();

		// Token: 0x060002B0 RID: 688
		public abstract Auditor GetAuditor();

		// Token: 0x060002B1 RID: 689
		public abstract ComplianceItemPagedReader GetPagedReader(ComplianceItemContainer container);

		// Token: 0x060002B2 RID: 690
		public abstract RuleParser GetRuleParser();

		// Token: 0x060002B3 RID: 691
		public abstract ComplianceItemContainer GetComplianceItemContainer(string tenantId, string scope);
	}
}
