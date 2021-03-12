using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Data.Directory.UnifiedPolicy;
using Microsoft.Office.CompliancePolicy.PolicyConfiguration;

namespace Microsoft.Office.CompliancePolicy.Tasks
{
	// Token: 0x02000128 RID: 296
	[Cmdlet("New", "DlpCompliancePolicy", SupportsShouldProcess = true)]
	public sealed class NewDlpCompliancePolicy : NewCompliancePolicyBase
	{
		// Token: 0x06000D2D RID: 3373 RVA: 0x0002F880 File Offset: 0x0002DA80
		public NewDlpCompliancePolicy() : base(PolicyScenario.Dlp)
		{
		}

		// Token: 0x06000D2E RID: 3374 RVA: 0x0002F88C File Offset: 0x0002DA8C
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
				SharePointBinding = base.InternalSharePointBindings,
				OneDriveBinding = base.InternalOneDriveBindings
			};
			base.InternalProcessRecord();
			TaskLogger.LogExit();
		}

		// Token: 0x06000D2F RID: 3375 RVA: 0x0002F918 File Offset: 0x0002DB18
		protected override void WriteResult(IConfigurable dataObject)
		{
			PsDlpCompliancePolicy psDlpCompliancePolicy = new PsDlpCompliancePolicy(dataObject as PolicyStorage)
			{
				StorageBindings = Utils.LoadBindingStoragesByPolicy(base.DataSession, dataObject as PolicyStorage)
			};
			psDlpCompliancePolicy.PopulateTaskProperties(this, base.DataSession as IConfigurationSession);
			base.WriteResult(psDlpCompliancePolicy);
		}
	}
}
