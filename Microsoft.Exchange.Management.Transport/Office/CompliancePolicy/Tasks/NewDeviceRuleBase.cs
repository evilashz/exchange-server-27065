using System;
using System.Collections.Generic;
using System.Management.Automation;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Data.Directory.UnifiedPolicy;
using Microsoft.Exchange.Management.Transport;
using Microsoft.Office.CompliancePolicy.PolicyConfiguration;

namespace Microsoft.Office.CompliancePolicy.Tasks
{
	// Token: 0x020000FF RID: 255
	public abstract class NewDeviceRuleBase : NewComplianceRuleBase
	{
		// Token: 0x06000A67 RID: 2663
		protected abstract void SetPropsOnDeviceRule(DeviceRuleBase deviceRule);

		// Token: 0x06000A68 RID: 2664
		protected abstract DeviceRuleBase CreateDeviceRule(RuleStorage ruleStorage);

		// Token: 0x06000A69 RID: 2665
		protected abstract Exception GetDeviceRuleAlreadyExistsException(string name);

		// Token: 0x06000A6A RID: 2666
		protected abstract bool GetDeviceRuleGuidFromWorkload(Workload workload, out Guid ruleGuid);

		// Token: 0x06000A6B RID: 2667 RVA: 0x00029C56 File Offset: 0x00027E56
		protected NewDeviceRuleBase(PolicyScenario scenario) : base(scenario)
		{
		}

		// Token: 0x1700037C RID: 892
		// (get) Token: 0x06000A6C RID: 2668 RVA: 0x00029C5F File Offset: 0x00027E5F
		// (set) Token: 0x06000A6D RID: 2669 RVA: 0x00029C67 File Offset: 0x00027E67
		[Parameter(Mandatory = false)]
		public new string Name
		{
			get
			{
				return base.Name;
			}
			protected set
			{
				base.Name = value;
			}
		}

		// Token: 0x1700037D RID: 893
		// (get) Token: 0x06000A6E RID: 2670 RVA: 0x00029C70 File Offset: 0x00027E70
		// (set) Token: 0x06000A6F RID: 2671 RVA: 0x00029C87 File Offset: 0x00027E87
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

		// Token: 0x06000A70 RID: 2672 RVA: 0x00029C9A File Offset: 0x00027E9A
		protected override void InternalValidate()
		{
			this.ValidateWorkloadParameter();
			base.InternalValidate();
		}

		// Token: 0x06000A71 RID: 2673 RVA: 0x00029CA8 File Offset: 0x00027EA8
		protected override IConfigurable PrepareDataObject()
		{
			RuleStorage ruleStorage = (RuleStorage)base.PrepareDataObject();
			ruleStorage.Name = this.ruleName;
			ruleStorage.SetId(((ADObjectId)this.policyStorage.Identity).GetChildId(this.ruleName));
			DeviceRuleBase deviceRuleBase = this.CreateDeviceRule(ruleStorage);
			deviceRuleBase.Policy = Utils.GetUniversalIdentity(this.policyStorage);
			deviceRuleBase.Workload = this.policyStorage.Workload;
			deviceRuleBase.TargetGroups = this.TargetGroups;
			this.SetPropsOnDeviceRule(deviceRuleBase);
			deviceRuleBase.UpdateStorageProperties(this, base.DataSession as IConfigurationSession, true);
			return ruleStorage;
		}

		// Token: 0x06000A72 RID: 2674 RVA: 0x00029D40 File Offset: 0x00027F40
		protected virtual void ValidateWorkloadParameter()
		{
			Guid guid;
			if (!this.GetDeviceRuleGuidFromWorkload(Workload.Intune, out guid))
			{
				base.WriteError(new ArgumentException(Strings.InvalidDeviceRuleWorkload), ErrorCategory.InvalidArgument, null);
			}
			this.Name = (this.ruleName = base.Policy.RawIdentity + guid.ToString("B"));
		}

		// Token: 0x06000A73 RID: 2675 RVA: 0x00029D9A File Offset: 0x00027F9A
		protected override IEnumerable<ChangeNotificationData> OnNotifyChanges()
		{
			return IntuneCompliancePolicySyncNotificationClient.NotifyChange(this, this.DataObject, new List<UnifiedPolicyStorageBase>(), (IConfigurationSession)base.DataSession, this.executionLogger);
		}

		// Token: 0x04000432 RID: 1074
		protected string ruleName;
	}
}
