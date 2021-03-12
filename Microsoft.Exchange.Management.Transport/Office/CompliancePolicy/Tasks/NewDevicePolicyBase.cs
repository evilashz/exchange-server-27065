using System;
using System.Collections.Generic;
using System.Management.Automation;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Data.Directory.UnifiedPolicy;
using Microsoft.Office.CompliancePolicy.PolicyConfiguration;

namespace Microsoft.Office.CompliancePolicy.Tasks
{
	// Token: 0x020000EE RID: 238
	[Cmdlet("New", "DevicePolicyBase", SupportsShouldProcess = true)]
	public abstract class NewDevicePolicyBase : NewCompliancePolicyBase
	{
		// Token: 0x060009AA RID: 2474 RVA: 0x000275BA File Offset: 0x000257BA
		protected NewDevicePolicyBase(PolicyScenario deviceConditionalAccess) : base(deviceConditionalAccess)
		{
		}

		// Token: 0x060009AB RID: 2475 RVA: 0x000275C3 File Offset: 0x000257C3
		protected override IEnumerable<ChangeNotificationData> OnNotifyChanges()
		{
			return IntuneCompliancePolicySyncNotificationClient.NotifyChange(this, this.DataObject, new List<UnifiedPolicyStorageBase>(), (IConfigurationSession)base.DataSession, this.executionLogger);
		}
	}
}
