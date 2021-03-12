using System;
using System.Management.Automation;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Data.Directory.UnifiedPolicy;
using Microsoft.Exchange.Management.Transport;
using Microsoft.Office.CompliancePolicy.PolicyConfiguration;

namespace Microsoft.Office.CompliancePolicy.Tasks
{
	// Token: 0x0200010C RID: 268
	[Cmdlet("New", "DeviceTenantRule", SupportsShouldProcess = true)]
	public sealed class NewDeviceTenantRule : NewDeviceRuleBase
	{
		// Token: 0x170003FA RID: 1018
		// (get) Token: 0x06000BAA RID: 2986 RVA: 0x0002C313 File Offset: 0x0002A513
		// (set) Token: 0x06000BAB RID: 2987 RVA: 0x0002C31B File Offset: 0x0002A51B
		[Parameter(Mandatory = false)]
		private new MultiValuedProperty<Guid> TargetGroups { get; set; }

		// Token: 0x170003FB RID: 1019
		// (get) Token: 0x06000BAC RID: 2988 RVA: 0x0002C324 File Offset: 0x0002A524
		// (set) Token: 0x06000BAD RID: 2989 RVA: 0x0002C33B File Offset: 0x0002A53B
		[Parameter(Mandatory = false)]
		public MultiValuedProperty<Guid> ExclusionList
		{
			get
			{
				return (MultiValuedProperty<Guid>)base.Fields["ExclusionList"];
			}
			set
			{
				base.Fields["ExclusionList"] = value;
			}
		}

		// Token: 0x06000BAE RID: 2990 RVA: 0x0002C34E File Offset: 0x0002A54E
		public NewDeviceTenantRule() : base(PolicyScenario.DeviceTenantConditionalAccess)
		{
		}

		// Token: 0x06000BAF RID: 2991 RVA: 0x0002C357 File Offset: 0x0002A557
		protected override DeviceRuleBase CreateDeviceRule(RuleStorage ruleStorage)
		{
			return new DeviceTenantRule(ruleStorage);
		}

		// Token: 0x06000BB0 RID: 2992 RVA: 0x0002C35F File Offset: 0x0002A55F
		protected override Exception GetDeviceRuleAlreadyExistsException(string name)
		{
			return new DeviceTenantRuleAlreadyExistsException(name);
		}

		// Token: 0x06000BB1 RID: 2993 RVA: 0x0002C367 File Offset: 0x0002A567
		protected override bool GetDeviceRuleGuidFromWorkload(Workload workload, out Guid ruleGuid)
		{
			ruleGuid = default(Guid);
			return DevicePolicyUtility.GetTenantRuleGuidFromWorkload(workload, out ruleGuid);
		}

		// Token: 0x06000BB2 RID: 2994 RVA: 0x0002C378 File Offset: 0x0002A578
		protected override void ValidateWorkloadParameter()
		{
			Guid guid;
			if (!this.GetDeviceRuleGuidFromWorkload(Workload.Intune, out guid))
			{
				base.WriteError(new ArgumentException(Strings.InvalidDeviceRuleWorkload), ErrorCategory.InvalidArgument, null);
			}
			base.Name = (this.ruleName = guid.ToString());
		}

		// Token: 0x06000BB3 RID: 2995 RVA: 0x0002C3C4 File Offset: 0x0002A5C4
		protected override IConfigurable PrepareDataObject()
		{
			RuleStorage ruleStorage = (RuleStorage)base.PrepareDataObject();
			ruleStorage.Name = this.ruleName;
			ruleStorage.SetId(((ADObjectId)this.policyStorage.Identity).GetChildId(this.ruleName));
			DeviceTenantRule deviceTenantRule = this.CreateDeviceRule(ruleStorage) as DeviceTenantRule;
			deviceTenantRule.Policy = Utils.GetUniversalIdentity(this.policyStorage);
			deviceTenantRule.Workload = this.policyStorage.Workload;
			deviceTenantRule.ExclusionList = this.ExclusionList;
			this.SetPropsOnDeviceRule(deviceTenantRule);
			deviceTenantRule.UpdateStorageProperties(this, base.DataSession as IConfigurationSession, true);
			return ruleStorage;
		}

		// Token: 0x170003FC RID: 1020
		// (get) Token: 0x06000BB4 RID: 2996 RVA: 0x0002C460 File Offset: 0x0002A660
		// (set) Token: 0x06000BB5 RID: 2997 RVA: 0x0002C477 File Offset: 0x0002A677
		[Parameter(Mandatory = false)]
		public PolicyResourceScope? ApplyPolicyTo
		{
			get
			{
				return (PolicyResourceScope?)base.Fields[DeviceTenantRule.AccessControl_ResourceScope];
			}
			set
			{
				base.Fields[DeviceTenantRule.AccessControl_ResourceScope] = value;
			}
		}

		// Token: 0x170003FD RID: 1021
		// (get) Token: 0x06000BB6 RID: 2998 RVA: 0x0002C48F File Offset: 0x0002A68F
		// (set) Token: 0x06000BB7 RID: 2999 RVA: 0x0002C4A6 File Offset: 0x0002A6A6
		[Parameter(Mandatory = false)]
		public bool? BlockUnsupportedDevices
		{
			get
			{
				return (bool?)base.Fields[DeviceTenantRule.AccessControl_AllowActionOnUnsupportedPlatform];
			}
			set
			{
				base.Fields[DeviceTenantRule.AccessControl_AllowActionOnUnsupportedPlatform] = value;
			}
		}

		// Token: 0x06000BB8 RID: 3000 RVA: 0x0002C4C0 File Offset: 0x0002A6C0
		protected override void SetPropsOnDeviceRule(DeviceRuleBase pdeviceRule)
		{
			DeviceTenantRule deviceTenantRule = (DeviceTenantRule)pdeviceRule;
			deviceTenantRule.ApplyPolicyTo = this.ApplyPolicyTo;
			deviceTenantRule.BlockUnsupportedDevices = this.BlockUnsupportedDevices;
		}
	}
}
