using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Data.Directory.UnifiedPolicy;
using Microsoft.Exchange.Management.Transport;
using Microsoft.Office.CompliancePolicy.PolicyConfiguration;

namespace Microsoft.Office.CompliancePolicy.Tasks
{
	// Token: 0x02000108 RID: 264
	[Cmdlet("New", "DeviceTenantPolicy", SupportsShouldProcess = true)]
	public sealed class NewDeviceTenantPolicy : NewDevicePolicyBase
	{
		// Token: 0x170003F9 RID: 1017
		// (get) Token: 0x06000B9D RID: 2973 RVA: 0x0002C19E File Offset: 0x0002A39E
		// (set) Token: 0x06000B9E RID: 2974 RVA: 0x0002C1A6 File Offset: 0x0002A3A6
		[Parameter(Mandatory = false)]
		public new string Name
		{
			get
			{
				return base.Name;
			}
			private set
			{
				base.Name = value;
			}
		}

		// Token: 0x06000B9F RID: 2975 RVA: 0x0002C1AF File Offset: 0x0002A3AF
		public NewDeviceTenantPolicy() : base(PolicyScenario.DeviceTenantConditionalAccess)
		{
		}

		// Token: 0x06000BA0 RID: 2976 RVA: 0x0002C1B8 File Offset: 0x0002A3B8
		protected override void InternalProcessRecord()
		{
			TaskLogger.LogEnter();
			base.PsPolicyPresentationObject = new DeviceTenantPolicy(this.DataObject)
			{
				Name = this.Name,
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

		// Token: 0x06000BA1 RID: 2977 RVA: 0x0002C234 File Offset: 0x0002A434
		protected override void WriteResult(IConfigurable dataObject)
		{
			DeviceTenantPolicy deviceTenantPolicy = new DeviceTenantPolicy(dataObject as PolicyStorage)
			{
				StorageBindings = Utils.LoadBindingStoragesByPolicy(base.DataSession, dataObject as PolicyStorage)
			};
			deviceTenantPolicy.PopulateTaskProperties(this, base.DataSession as IConfigurationSession);
			base.WriteResult(deviceTenantPolicy);
		}

		// Token: 0x06000BA2 RID: 2978 RVA: 0x0002C280 File Offset: 0x0002A480
		private void ValidateWorkloadParameter()
		{
			Guid guid;
			if (DevicePolicyUtility.GetTenantPolicyGuidFromWorkload(Workload.Intune, out guid))
			{
				this.policyName = guid.ToString();
				this.Name = this.policyName;
				return;
			}
			base.WriteError(new ArgumentException(Strings.InvalidCombinationOfCompliancePolicyTypeAndWorkload), ErrorCategory.InvalidArgument, null);
		}

		// Token: 0x06000BA3 RID: 2979 RVA: 0x0002C2CE File Offset: 0x0002A4CE
		protected override void InternalValidate()
		{
			this.ValidateWorkloadParameter();
			base.InternalValidate();
		}

		// Token: 0x04000438 RID: 1080
		private string policyName;
	}
}
