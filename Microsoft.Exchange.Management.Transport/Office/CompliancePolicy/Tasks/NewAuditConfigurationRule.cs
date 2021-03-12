using System;
using System.Management.Automation;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.UnifiedPolicy;
using Microsoft.Exchange.Data.Storage.UnifiedPolicy;
using Microsoft.Exchange.Management.Transport;
using Microsoft.Office.CompliancePolicy.PolicyConfiguration;

namespace Microsoft.Office.CompliancePolicy.Tasks
{
	// Token: 0x02000117 RID: 279
	[Cmdlet("New", "AuditConfigurationRule", SupportsShouldProcess = true)]
	public sealed class NewAuditConfigurationRule : NewComplianceRuleBase
	{
		// Token: 0x17000472 RID: 1138
		// (get) Token: 0x06000CC6 RID: 3270 RVA: 0x0002E336 File Offset: 0x0002C536
		// (set) Token: 0x06000CC7 RID: 3271 RVA: 0x0002E34D File Offset: 0x0002C54D
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

		// Token: 0x17000473 RID: 1139
		// (get) Token: 0x06000CC8 RID: 3272 RVA: 0x0002E365 File Offset: 0x0002C565
		// (set) Token: 0x06000CC9 RID: 3273 RVA: 0x0002E37C File Offset: 0x0002C57C
		[Parameter(Mandatory = true)]
		public MultiValuedProperty<AuditableOperations> AuditOperation
		{
			get
			{
				return (MultiValuedProperty<AuditableOperations>)base.Fields["AuditOperation"];
			}
			set
			{
				base.Fields["AuditOperation"] = value;
			}
		}

		// Token: 0x17000474 RID: 1140
		// (get) Token: 0x06000CCA RID: 3274 RVA: 0x0002E38F File Offset: 0x0002C58F
		// (set) Token: 0x06000CCB RID: 3275 RVA: 0x0002E397 File Offset: 0x0002C597
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

		// Token: 0x17000475 RID: 1141
		// (get) Token: 0x06000CCC RID: 3276 RVA: 0x0002E3A0 File Offset: 0x0002C5A0
		// (set) Token: 0x06000CCD RID: 3277 RVA: 0x0002E3A8 File Offset: 0x0002C5A8
		[Parameter(Mandatory = false)]
		public new PolicyIdParameter Policy
		{
			get
			{
				return base.Policy;
			}
			private set
			{
				base.Policy = value;
			}
		}

		// Token: 0x06000CCE RID: 3278 RVA: 0x0002E3B1 File Offset: 0x0002C5B1
		public NewAuditConfigurationRule() : base(PolicyScenario.AuditSettings)
		{
		}

		// Token: 0x06000CCF RID: 3279 RVA: 0x0002E3BC File Offset: 0x0002C5BC
		protected override void InternalValidate()
		{
			this.ValidateWorkloadParameter();
			this.policyStorage = (PolicyStorage)base.GetDataObject<PolicyStorage>(this.Policy, base.DataSession, null, new LocalizedString?(Strings.ErrorPolicyNotFound(this.Policy.ToString())), new LocalizedString?(Strings.ErrorPolicyNotUnique(this.Policy.ToString())), ExchangeErrorCategory.Client);
			if (this.policyStorage.Mode == Mode.PendingDeletion)
			{
				base.WriteError(new ErrorCannotCreateRuleUnderPendingDeletionPolicyException(this.policyStorage.Name), ErrorCategory.InvalidOperation, null);
			}
			base.InternalValidate();
		}

		// Token: 0x06000CD0 RID: 3280 RVA: 0x0002E448 File Offset: 0x0002C648
		private void ValidateWorkloadParameter()
		{
			Guid guid;
			if (!AuditPolicyUtility.GetRuleGuidFromWorkload(this.Workload, out guid))
			{
				base.WriteError(new ArgumentException(Strings.InvalidAuditRuleWorkload), ErrorCategory.InvalidArgument, null);
			}
			this.Name = guid.ToString();
			Guid guid2;
			if (!AuditPolicyUtility.GetPolicyGuidFromWorkload(this.Workload, out guid2))
			{
				base.WriteError(new ArgumentException(Strings.InvalidAuditRuleWorkload), ErrorCategory.InvalidArgument, null);
			}
			this.Policy = new PolicyIdParameter(guid2.ToString());
		}

		// Token: 0x06000CD1 RID: 3281 RVA: 0x0002E4CC File Offset: 0x0002C6CC
		protected override IConfigurable PrepareDataObject()
		{
			RuleStorage ruleStorage = (RuleStorage)base.PrepareDataObject();
			ruleStorage.Name = this.Name;
			ruleStorage.SetId(((ADObjectId)this.policyStorage.Identity).GetChildId(this.Name));
			AuditConfigurationRule auditConfigurationRule = new AuditConfigurationRule(ruleStorage)
			{
				Policy = Utils.GetUniversalIdentity(this.policyStorage),
				Workload = this.policyStorage.Workload,
				AuditOperation = this.AuditOperation
			};
			auditConfigurationRule.UpdateStorageProperties();
			return ruleStorage;
		}
	}
}
