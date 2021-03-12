using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Data.Directory.UnifiedPolicy;
using Microsoft.Office.CompliancePolicy.PolicyConfiguration;

namespace Microsoft.Office.CompliancePolicy.Tasks
{
	// Token: 0x020000EF RID: 239
	[Cmdlet("New", "DeviceConfigurationPolicy", SupportsShouldProcess = true)]
	public sealed class NewDeviceConfigurationPolicy : NewDevicePolicyBase
	{
		// Token: 0x060009AC RID: 2476 RVA: 0x000275E7 File Offset: 0x000257E7
		public NewDeviceConfigurationPolicy() : base(PolicyScenario.DeviceSettings)
		{
		}

		// Token: 0x060009AD RID: 2477 RVA: 0x000275F0 File Offset: 0x000257F0
		protected override void InternalProcessRecord()
		{
			TaskLogger.LogEnter();
			base.PsPolicyPresentationObject = new DevicePolicy(this.DataObject)
			{
				Name = base.Name,
				Comment = base.Comment,
				Workload = Workload.Intune,
				Enabled = base.Enabled,
				Mode = Mode.Enforce,
				ExchangeBinding = base.InternalExchangeBindings,
				SharePointBinding = base.InternalSharePointBindings
			};
			base.InternalProcessRecord();
			TaskLogger.LogExit();
		}

		// Token: 0x060009AE RID: 2478 RVA: 0x0002766C File Offset: 0x0002586C
		protected override void WriteResult(IConfigurable dataObject)
		{
			DevicePolicy devicePolicy = new DevicePolicy(dataObject as PolicyStorage)
			{
				StorageBindings = Utils.LoadBindingStoragesByPolicy(base.DataSession, dataObject as PolicyStorage)
			};
			devicePolicy.PopulateTaskProperties(this, base.DataSession as IConfigurationSession);
			base.WriteResult(devicePolicy);
		}
	}
}
