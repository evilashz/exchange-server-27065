using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Data.Directory.UnifiedPolicy;
using Microsoft.Office.CompliancePolicy.PolicyConfiguration;

namespace Microsoft.Office.CompliancePolicy.Tasks
{
	// Token: 0x020000F8 RID: 248
	[Cmdlet("New", "DeviceConditionalAccessPolicy", SupportsShouldProcess = true)]
	public sealed class NewDeviceConditionalAccessPolicy : NewDevicePolicyBase
	{
		// Token: 0x06000A27 RID: 2599 RVA: 0x000293B1 File Offset: 0x000275B1
		public NewDeviceConditionalAccessPolicy() : base(PolicyScenario.DeviceConditionalAccess)
		{
		}

		// Token: 0x06000A28 RID: 2600 RVA: 0x000293BC File Offset: 0x000275BC
		protected override void InternalProcessRecord()
		{
			TaskLogger.LogEnter();
			base.PsPolicyPresentationObject = new DeviceConditionalAccessPolicy(this.DataObject)
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

		// Token: 0x06000A29 RID: 2601 RVA: 0x00029438 File Offset: 0x00027638
		protected override void WriteResult(IConfigurable dataObject)
		{
			DeviceConditionalAccessPolicy deviceConditionalAccessPolicy = new DeviceConditionalAccessPolicy(dataObject as PolicyStorage)
			{
				StorageBindings = Utils.LoadBindingStoragesByPolicy(base.DataSession, dataObject as PolicyStorage)
			};
			deviceConditionalAccessPolicy.PopulateTaskProperties(this, base.DataSession as IConfigurationSession);
			base.WriteResult(deviceConditionalAccessPolicy);
		}
	}
}
