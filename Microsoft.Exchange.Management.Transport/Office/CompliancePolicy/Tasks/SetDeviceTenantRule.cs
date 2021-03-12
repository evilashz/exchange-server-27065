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
	// Token: 0x0200010D RID: 269
	[Cmdlet("Set", "DeviceTenantRule", SupportsShouldProcess = true, DefaultParameterSetName = "Identity")]
	public sealed class SetDeviceTenantRule : SetDeviceRuleBase
	{
		// Token: 0x170003FE RID: 1022
		// (get) Token: 0x06000BB9 RID: 3001 RVA: 0x0002C4EC File Offset: 0x0002A6EC
		// (set) Token: 0x06000BBA RID: 3002 RVA: 0x0002C4F4 File Offset: 0x0002A6F4
		[Parameter(Mandatory = false)]
		private new MultiValuedProperty<Guid> TargetGroups { get; set; }

		// Token: 0x170003FF RID: 1023
		// (get) Token: 0x06000BBB RID: 3003 RVA: 0x0002C4FD File Offset: 0x0002A6FD
		// (set) Token: 0x06000BBC RID: 3004 RVA: 0x0002C514 File Offset: 0x0002A714
		[Parameter(Mandatory = true)]
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

		// Token: 0x06000BBD RID: 3005 RVA: 0x0002C527 File Offset: 0x0002A727
		public SetDeviceTenantRule() : base(PolicyScenario.DeviceTenantConditionalAccess)
		{
		}

		// Token: 0x06000BBE RID: 3006 RVA: 0x0002C530 File Offset: 0x0002A730
		protected override DeviceRuleBase CreateDeviceRule(RuleStorage dataObject)
		{
			return new DeviceTenantRule(dataObject);
		}

		// Token: 0x06000BBF RID: 3007 RVA: 0x0002C538 File Offset: 0x0002A738
		protected override void ValidateUnacceptableParameter()
		{
			if (this.Identity != null && !DevicePolicyUtility.IsDeviceTenantRule(this.Identity.ToString()))
			{
				base.WriteError(new ArgumentException(Strings.CanOnlyManipulateDeviceTenantRule), ErrorCategory.InvalidArgument, null);
			}
			if (DevicePolicyUtility.IsPropertySpecified(base.DynamicParametersInstance, ADObjectSchema.Name))
			{
				base.WriteError(new ArgumentException(Strings.CannotChangeDeviceTenantRuleName), ErrorCategory.InvalidArgument, null);
			}
		}

		// Token: 0x06000BC0 RID: 3008 RVA: 0x0002C5A0 File Offset: 0x0002A7A0
		protected override void StampChangesOn(IConfigurable dataObject)
		{
			base.StampChangesOn(dataObject);
			DeviceTenantRule deviceTenantRule = this.CreateDeviceRule(dataObject as RuleStorage) as DeviceTenantRule;
			if (deviceTenantRule != null)
			{
				deviceTenantRule.PopulateTaskProperties(this, base.DataSession as IConfigurationSession);
				deviceTenantRule.CopyChangesFrom(base.DynamicParametersInstance);
				deviceTenantRule.ExclusionList = this.ExclusionList;
				this.SetPropsOnDeviceRule(deviceTenantRule);
				deviceTenantRule.UpdateStorageProperties(this, base.DataSession as IConfigurationSession, false);
			}
		}

		// Token: 0x17000400 RID: 1024
		// (get) Token: 0x06000BC1 RID: 3009 RVA: 0x0002C60D File Offset: 0x0002A80D
		// (set) Token: 0x06000BC2 RID: 3010 RVA: 0x0002C624 File Offset: 0x0002A824
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

		// Token: 0x17000401 RID: 1025
		// (get) Token: 0x06000BC3 RID: 3011 RVA: 0x0002C63C File Offset: 0x0002A83C
		// (set) Token: 0x06000BC4 RID: 3012 RVA: 0x0002C653 File Offset: 0x0002A853
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

		// Token: 0x06000BC5 RID: 3013 RVA: 0x0002C66C File Offset: 0x0002A86C
		protected override void SetPropsOnDeviceRule(DeviceRuleBase pdeviceRule)
		{
			DeviceTenantRule deviceTenantRule = (DeviceTenantRule)pdeviceRule;
			deviceTenantRule.ApplyPolicyTo = this.ApplyPolicyTo;
			deviceTenantRule.BlockUnsupportedDevices = this.BlockUnsupportedDevices;
		}
	}
}
