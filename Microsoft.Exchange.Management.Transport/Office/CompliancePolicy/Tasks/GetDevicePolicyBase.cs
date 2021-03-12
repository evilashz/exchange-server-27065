using System;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Data.Directory.UnifiedPolicy;
using Microsoft.Exchange.Management.Transport;
using Microsoft.Office.CompliancePolicy.PolicyConfiguration;

namespace Microsoft.Office.CompliancePolicy.Tasks
{
	// Token: 0x020000EB RID: 235
	public class GetDevicePolicyBase : GetCompliancePolicyBase
	{
		// Token: 0x06000973 RID: 2419 RVA: 0x00026C42 File Offset: 0x00024E42
		protected GetDevicePolicyBase()
		{
		}

		// Token: 0x06000974 RID: 2420 RVA: 0x00026C4A File Offset: 0x00024E4A
		protected GetDevicePolicyBase(PolicyScenario scenario) : base(scenario)
		{
			DevicePolicyUtility.ValidateDeviceScenarioArgument(scenario);
		}

		// Token: 0x06000975 RID: 2421 RVA: 0x00026C59 File Offset: 0x00024E59
		private PsCompliancePolicyBase CreatePolicyByScenario(PolicyStorage policyStorage)
		{
			if (policyStorage.Scenario == PolicyScenario.DeviceSettings)
			{
				return new DevicePolicy(policyStorage);
			}
			if (policyStorage.Scenario == PolicyScenario.DeviceConditionalAccess)
			{
				return new DeviceConditionalAccessPolicy(policyStorage);
			}
			if (policyStorage.Scenario == PolicyScenario.DeviceTenantConditionalAccess)
			{
				return new DeviceTenantPolicy(policyStorage);
			}
			return null;
		}

		// Token: 0x06000976 RID: 2422 RVA: 0x00026C8C File Offset: 0x00024E8C
		protected override void WriteResult(IConfigurable dataObject)
		{
			PsCompliancePolicyBase psCompliancePolicyBase = this.CreatePolicyByScenario(dataObject as PolicyStorage);
			if (psCompliancePolicyBase != null)
			{
				psCompliancePolicyBase.StorageBindings = Utils.LoadBindingStoragesByPolicy(base.DataSession, dataObject as PolicyStorage);
				foreach (BindingStorage bindingStorage in psCompliancePolicyBase.StorageBindings)
				{
					base.WriteVerbose(Strings.VerboseLoadBindingStorageObjects(bindingStorage.ToString(), psCompliancePolicyBase.ToString()));
				}
				psCompliancePolicyBase.PopulateTaskProperties(this, base.DataSession as IConfigurationSession);
				if (psCompliancePolicyBase.ReadOnly)
				{
					this.WriteWarning(Strings.WarningTaskPolicyIsTooAdvancedToRead(psCompliancePolicyBase.Name));
				}
				PolicySettingStatusHelpers.PopulatePolicyDistributionStatus(psCompliancePolicyBase, dataObject as PolicyStorage, base.DataSession, this, this.executionLogger);
				base.WriteResult(psCompliancePolicyBase);
			}
		}
	}
}
