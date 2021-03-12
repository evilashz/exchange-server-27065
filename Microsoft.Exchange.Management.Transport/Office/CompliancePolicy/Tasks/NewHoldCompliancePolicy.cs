using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Data.Directory.UnifiedPolicy;
using Microsoft.Office.CompliancePolicy.PolicyConfiguration;

namespace Microsoft.Office.CompliancePolicy.Tasks
{
	// Token: 0x02000120 RID: 288
	[Cmdlet("New", "HoldCompliancePolicy", SupportsShouldProcess = true)]
	public sealed class NewHoldCompliancePolicy : NewCompliancePolicyBase
	{
		// Token: 0x06000D07 RID: 3335 RVA: 0x0002F0AC File Offset: 0x0002D2AC
		public NewHoldCompliancePolicy() : base(PolicyScenario.Hold)
		{
		}

		// Token: 0x06000D08 RID: 3336 RVA: 0x0002F0B8 File Offset: 0x0002D2B8
		protected override void InternalProcessRecord()
		{
			TaskLogger.LogEnter();
			base.PsPolicyPresentationObject = new PsHoldCompliancePolicy(this.DataObject)
			{
				Name = base.Name,
				Comment = base.Comment,
				Workload = base.TenantWorkloadConfig,
				Enabled = base.Enabled,
				Mode = Mode.Enforce,
				ExchangeBinding = base.InternalExchangeBindings,
				SharePointBinding = base.InternalSharePointBindings
			};
			base.InternalProcessRecord();
			TaskLogger.LogExit();
		}

		// Token: 0x06000D09 RID: 3337 RVA: 0x0002F138 File Offset: 0x0002D338
		protected override void WriteResult(IConfigurable dataObject)
		{
			PsHoldCompliancePolicy psHoldCompliancePolicy = new PsHoldCompliancePolicy(dataObject as PolicyStorage)
			{
				StorageBindings = Utils.LoadBindingStoragesByPolicy(base.DataSession, dataObject as PolicyStorage)
			};
			psHoldCompliancePolicy.PopulateTaskProperties(this, base.DataSession as IConfigurationSession);
			base.WriteResult(psHoldCompliancePolicy);
		}
	}
}
