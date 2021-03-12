using System;
using System.Collections.Generic;
using System.Management.Automation;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Data.Directory.UnifiedPolicy;
using Microsoft.Office.CompliancePolicy.PolicyConfiguration;

namespace Microsoft.Office.CompliancePolicy.Tasks
{
	// Token: 0x020000F1 RID: 241
	[Cmdlet("Set", "DevicePolicyBase", SupportsShouldProcess = true, DefaultParameterSetName = "Identity")]
	public abstract class SetDevicePolicyBase : SetCompliancePolicyBase
	{
		// Token: 0x06000A07 RID: 2567 RVA: 0x00028E80 File Offset: 0x00027080
		public SetDevicePolicyBase(PolicyScenario scenario) : base(scenario)
		{
			DevicePolicyUtility.ValidateDeviceScenarioArgument(scenario);
		}

		// Token: 0x06000A08 RID: 2568 RVA: 0x00028E8F File Offset: 0x0002708F
		protected override IEnumerable<ChangeNotificationData> OnNotifyChanges()
		{
			return IntuneCompliancePolicySyncNotificationClient.NotifyChange(this, this.DataObject, new List<UnifiedPolicyStorageBase>(), (IConfigurationSession)base.DataSession, base.ExecutionLogger);
		}
	}
}
