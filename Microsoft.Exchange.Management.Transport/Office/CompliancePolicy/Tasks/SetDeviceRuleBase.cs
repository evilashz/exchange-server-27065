using System;
using System.Collections.Generic;
using System.Management.Automation;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Data.Directory.UnifiedPolicy;
using Microsoft.Office.CompliancePolicy.PolicyConfiguration;

namespace Microsoft.Office.CompliancePolicy.Tasks
{
	// Token: 0x02000102 RID: 258
	public abstract class SetDeviceRuleBase : SetComplianceRuleBase
	{
		// Token: 0x06000B04 RID: 2820
		protected abstract DeviceRuleBase CreateDeviceRule(RuleStorage dataObject);

		// Token: 0x06000B05 RID: 2821
		protected abstract void SetPropsOnDeviceRule(DeviceRuleBase deviceRule);

		// Token: 0x06000B06 RID: 2822
		protected abstract void ValidateUnacceptableParameter();

		// Token: 0x170003BB RID: 955
		// (get) Token: 0x06000B07 RID: 2823 RVA: 0x0002B00A File Offset: 0x0002920A
		// (set) Token: 0x06000B08 RID: 2824 RVA: 0x0002B021 File Offset: 0x00029221
		[Parameter(Mandatory = true)]
		public MultiValuedProperty<Guid> TargetGroups
		{
			get
			{
				return (MultiValuedProperty<Guid>)base.Fields["TargetGroups"];
			}
			set
			{
				base.Fields["TargetGroups"] = value;
			}
		}

		// Token: 0x06000B09 RID: 2825 RVA: 0x0002B034 File Offset: 0x00029234
		protected SetDeviceRuleBase(PolicyScenario scenario) : base(scenario)
		{
			DevicePolicyUtility.ValidateDeviceScenarioArgument(scenario);
		}

		// Token: 0x06000B0A RID: 2826 RVA: 0x0002B044 File Offset: 0x00029244
		protected override void StampChangesOn(IConfigurable dataObject)
		{
			base.StampChangesOn(dataObject);
			DeviceRuleBase deviceRuleBase = this.CreateDeviceRule(dataObject as RuleStorage);
			deviceRuleBase.PopulateTaskProperties(this, base.DataSession as IConfigurationSession);
			deviceRuleBase.CopyChangesFrom(base.DynamicParametersInstance);
			deviceRuleBase.TargetGroups = this.TargetGroups;
			this.SetPropsOnDeviceRule(deviceRuleBase);
			deviceRuleBase.UpdateStorageProperties(this, base.DataSession as IConfigurationSession, false);
		}

		// Token: 0x06000B0B RID: 2827 RVA: 0x0002B0A9 File Offset: 0x000292A9
		protected override void InternalValidate()
		{
			this.ValidateUnacceptableParameter();
			base.InternalValidate();
		}

		// Token: 0x06000B0C RID: 2828 RVA: 0x0002B0B7 File Offset: 0x000292B7
		protected override IEnumerable<ChangeNotificationData> OnNotifyChanges()
		{
			return IntuneCompliancePolicySyncNotificationClient.NotifyChange(this, this.DataObject, new List<UnifiedPolicyStorageBase>(), (IConfigurationSession)base.DataSession, this.executionLogger);
		}
	}
}
