using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.UnifiedPolicy;
using Microsoft.Exchange.Data.Storage.UnifiedPolicy;
using Microsoft.Exchange.Management.Transport;
using Microsoft.Office.CompliancePolicy.PolicyConfiguration;

namespace Microsoft.Office.CompliancePolicy.Tasks
{
	// Token: 0x02000114 RID: 276
	[Cmdlet("New", "AuditConfigurationPolicy", SupportsShouldProcess = true)]
	public sealed class NewAuditConfigurationPolicy : NewCompliancePolicyBase
	{
		// Token: 0x17000470 RID: 1136
		// (get) Token: 0x06000CB4 RID: 3252 RVA: 0x0002E0EE File Offset: 0x0002C2EE
		// (set) Token: 0x06000CB5 RID: 3253 RVA: 0x0002E0F6 File Offset: 0x0002C2F6
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

		// Token: 0x17000471 RID: 1137
		// (get) Token: 0x06000CB6 RID: 3254 RVA: 0x0002E0FF File Offset: 0x0002C2FF
		// (set) Token: 0x06000CB7 RID: 3255 RVA: 0x0002E116 File Offset: 0x0002C316
		[Parameter(Mandatory = true)]
		public Workload Workload
		{
			get
			{
				return (Workload)base.Fields[PsCompliancePolicyBaseSchema.Workload];
			}
			set
			{
				base.Fields[PsCompliancePolicyBaseSchema.Workload] = value;
			}
		}

		// Token: 0x06000CB8 RID: 3256 RVA: 0x0002E12E File Offset: 0x0002C32E
		public NewAuditConfigurationPolicy() : base(PolicyScenario.AuditSettings)
		{
		}

		// Token: 0x06000CB9 RID: 3257 RVA: 0x0002E137 File Offset: 0x0002C337
		protected override IConfigurable PrepareDataObject()
		{
			base.Name = this.policyName;
			return (PolicyStorage)base.PrepareDataObject();
		}

		// Token: 0x06000CBA RID: 3258 RVA: 0x0002E150 File Offset: 0x0002C350
		protected override void InternalValidate()
		{
			this.ValidateWorkloadParameter(this.Workload);
			base.InternalValidate();
		}

		// Token: 0x06000CBB RID: 3259 RVA: 0x0002E164 File Offset: 0x0002C364
		protected override void InternalProcessRecord()
		{
			TaskLogger.LogEnter();
			base.PsPolicyPresentationObject = new AuditConfigurationPolicy(this.DataObject)
			{
				Name = this.policyName,
				Workload = AuditConfigUtility.GetEffectiveWorkload(this.Workload),
				Enabled = false,
				Mode = Mode.Enforce,
				ExchangeBinding = base.InternalExchangeBindings,
				SharePointBinding = base.InternalSharePointBindings
			};
			base.InternalProcessRecord();
			TaskLogger.LogExit();
		}

		// Token: 0x06000CBC RID: 3260 RVA: 0x0002E1D8 File Offset: 0x0002C3D8
		protected override void WriteResult(IConfigurable dataObject)
		{
			AuditConfigurationPolicy result = new AuditConfigurationPolicy(dataObject as PolicyStorage);
			base.WriteResult(result);
		}

		// Token: 0x06000CBD RID: 3261 RVA: 0x0002E1F8 File Offset: 0x0002C3F8
		private void ValidateWorkloadParameter(Workload workload)
		{
			Guid guid;
			if (AuditPolicyUtility.GetPolicyGuidFromWorkload(this.Workload, out guid))
			{
				this.policyName = guid.ToString();
				this.Name = this.policyName;
				return;
			}
			base.WriteError(new ArgumentException(Strings.InvalidCombinationOfCompliancePolicyTypeAndWorkload), ErrorCategory.InvalidArgument, null);
		}

		// Token: 0x0400043B RID: 1083
		private string policyName;
	}
}
