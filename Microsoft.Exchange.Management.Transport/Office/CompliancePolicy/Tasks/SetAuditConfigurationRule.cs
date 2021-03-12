using System;
using System.Management.Automation;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.UnifiedPolicy;
using Microsoft.Exchange.Data.Storage.UnifiedPolicy;
using Microsoft.Exchange.Management.Transport;
using Microsoft.Office.CompliancePolicy.PolicyConfiguration;

namespace Microsoft.Office.CompliancePolicy.Tasks
{
	// Token: 0x02000118 RID: 280
	[Cmdlet("Set", "AuditConfigurationRule", SupportsShouldProcess = true, DefaultParameterSetName = "Identity")]
	public sealed class SetAuditConfigurationRule : SetComplianceRuleBase
	{
		// Token: 0x17000476 RID: 1142
		// (get) Token: 0x06000CD2 RID: 3282 RVA: 0x0002E550 File Offset: 0x0002C750
		// (set) Token: 0x06000CD3 RID: 3283 RVA: 0x0002E567 File Offset: 0x0002C767
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

		// Token: 0x06000CD4 RID: 3284 RVA: 0x0002E57A File Offset: 0x0002C77A
		public SetAuditConfigurationRule() : base(PolicyScenario.AuditSettings)
		{
		}

		// Token: 0x06000CD5 RID: 3285 RVA: 0x0002E584 File Offset: 0x0002C784
		protected override void StampChangesOn(IConfigurable dataObject)
		{
			RuleStorage ruleStorage = (RuleStorage)dataObject;
			ruleStorage.ResetChangeTracking(true);
			AuditConfigurationRule auditConfigurationRule = new AuditConfigurationRule(dataObject as RuleStorage);
			auditConfigurationRule.PopulateTaskProperties();
			if (ruleStorage.Mode == Mode.PendingDeletion)
			{
				base.WriteError(new ErrorCommonComplianceRuleIsDeletedException(ruleStorage.Name), ErrorCategory.InvalidOperation, null);
			}
			base.StampChangesOn(dataObject);
			auditConfigurationRule.CopyChangesFrom(base.DynamicParametersInstance);
			auditConfigurationRule.AuditOperation = this.AuditOperation;
			auditConfigurationRule.UpdateStorageProperties();
		}

		// Token: 0x06000CD6 RID: 3286 RVA: 0x0002E5F2 File Offset: 0x0002C7F2
		protected override void InternalValidate()
		{
			this.ValidateUnacceptableParameter();
			base.InternalValidate();
		}

		// Token: 0x06000CD7 RID: 3287 RVA: 0x0002E600 File Offset: 0x0002C800
		private void ValidateUnacceptableParameter()
		{
			if (this.Identity != null && !AuditPolicyUtility.IsAuditConfigurationRule(this.Identity.ToString()))
			{
				base.WriteError(new ArgumentException(Strings.CanOnlyManipulateAuditConfigurationRule), ErrorCategory.InvalidArgument, null);
			}
			if (base.DynamicParametersInstance.IsModified(ADObjectSchema.Name))
			{
				base.WriteError(new ArgumentException(Strings.CannotChangeAuditConfigurationRuleName), ErrorCategory.InvalidArgument, null);
			}
		}
	}
}
