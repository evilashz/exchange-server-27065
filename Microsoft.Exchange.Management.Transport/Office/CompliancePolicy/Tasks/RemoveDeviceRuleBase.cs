using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Data.Directory.UnifiedPolicy;
using Microsoft.Exchange.Management.Transport;
using Microsoft.Office.CompliancePolicy.PolicyConfiguration;

namespace Microsoft.Office.CompliancePolicy.Tasks
{
	// Token: 0x02000105 RID: 261
	public abstract class RemoveDeviceRuleBase : RemoveComplianceRuleBase
	{
		// Token: 0x06000B95 RID: 2965
		protected abstract DeviceRuleBase CreateDeviceRule(RuleStorage ruleStorage);

		// Token: 0x06000B96 RID: 2966 RVA: 0x0002C12E File Offset: 0x0002A32E
		protected RemoveDeviceRuleBase(PolicyScenario scenario) : base(scenario)
		{
		}

		// Token: 0x170003F7 RID: 1015
		// (get) Token: 0x06000B97 RID: 2967 RVA: 0x0002C137 File Offset: 0x0002A337
		protected override LocalizedString ConfirmationMessage
		{
			get
			{
				return Strings.RemoveDeviceConfiguationRuleConfirmation(this.Identity.ToString());
			}
		}

		// Token: 0x06000B98 RID: 2968 RVA: 0x0002C149 File Offset: 0x0002A349
		protected override IEnumerable<ChangeNotificationData> OnNotifyChanges()
		{
			return IntuneCompliancePolicySyncNotificationClient.NotifyChange(this, base.DataObject, new List<UnifiedPolicyStorageBase>(), (IConfigurationSession)base.DataSession, this.executionLogger);
		}
	}
}
