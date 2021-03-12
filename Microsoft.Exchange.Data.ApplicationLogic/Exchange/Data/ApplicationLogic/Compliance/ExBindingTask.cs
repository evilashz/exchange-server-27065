using System;
using Microsoft.Office.CompliancePolicy;
using Microsoft.Office.CompliancePolicy.ComplianceTask;

namespace Microsoft.Exchange.Data.ApplicationLogic.Compliance
{
	// Token: 0x020000C4 RID: 196
	internal class ExBindingTask : BindingTask
	{
		// Token: 0x17000211 RID: 529
		// (get) Token: 0x06000851 RID: 2129 RVA: 0x00021C0E File Offset: 0x0001FE0E
		// (set) Token: 0x06000852 RID: 2130 RVA: 0x00021C16 File Offset: 0x0001FE16
		public ExecutionLog ComplianceProviderExecutionLog { get; set; }

		// Token: 0x17000212 RID: 530
		// (get) Token: 0x06000853 RID: 2131 RVA: 0x00021C20 File Offset: 0x0001FE20
		public override ComplianceServiceProvider ComplianceServiceProvider
		{
			get
			{
				if (base.ComplianceServiceProvider == null)
				{
					string workloadData = this.WorkloadData;
					this.ComplianceServiceProvider = new ExComplianceServiceProvider(workloadData, this.ComplianceProviderExecutionLog);
				}
				return base.ComplianceServiceProvider;
			}
		}

		// Token: 0x06000854 RID: 2132 RVA: 0x00021C54 File Offset: 0x0001FE54
		public override string GetWorkloadDataFromWorkload()
		{
			string result = string.Empty;
			ExComplianceServiceProvider exComplianceServiceProvider = this.ComplianceServiceProvider as ExComplianceServiceProvider;
			if (exComplianceServiceProvider != null)
			{
				result = exComplianceServiceProvider.PreferredDomainController;
			}
			return result;
		}
	}
}
