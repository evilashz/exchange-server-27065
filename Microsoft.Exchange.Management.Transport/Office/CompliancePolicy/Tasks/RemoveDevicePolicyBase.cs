using System;
using System.Collections.Generic;
using System.Management.Automation;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Data.Directory.UnifiedPolicy;
using Microsoft.Office.CompliancePolicy.PolicyConfiguration;

namespace Microsoft.Office.CompliancePolicy.Tasks
{
	// Token: 0x020000F4 RID: 244
	[Cmdlet("Remove", "DevicePolicyBase", SupportsShouldProcess = true, ConfirmImpact = ConfirmImpact.High)]
	public abstract class RemoveDevicePolicyBase : RemoveCompliancePolicyBase
	{
		// Token: 0x06000A20 RID: 2592 RVA: 0x000292CA File Offset: 0x000274CA
		protected RemoveDevicePolicyBase(PolicyScenario deviceConditionalAccess) : base(deviceConditionalAccess)
		{
		}

		// Token: 0x06000A21 RID: 2593 RVA: 0x000292D3 File Offset: 0x000274D3
		protected override IEnumerable<ChangeNotificationData> OnNotifyChanges(IEnumerable<UnifiedPolicyStorageBase> bindingStorageObjects, IEnumerable<UnifiedPolicyStorageBase> ruleStorageObjects)
		{
			return IntuneCompliancePolicySyncNotificationClient.NotifyChange(this, base.DataObject, ruleStorageObjects, (IConfigurationSession)base.DataSession, this.executionLogger);
		}
	}
}
