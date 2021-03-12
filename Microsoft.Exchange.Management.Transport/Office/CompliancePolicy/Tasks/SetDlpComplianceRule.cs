using System;
using System.Collections;
using System.Linq;
using System.Management.Automation;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Data.Directory.UnifiedPolicy;
using Microsoft.Exchange.Management.Transport;
using Microsoft.Office.CompliancePolicy.PolicyConfiguration;
using Microsoft.Office.CompliancePolicy.PolicyEvaluation;

namespace Microsoft.Office.CompliancePolicy.Tasks
{
	// Token: 0x0200012D RID: 301
	[Cmdlet("Set", "DlpComplianceRule", SupportsShouldProcess = true, DefaultParameterSetName = "Identity")]
	public sealed class SetDlpComplianceRule : SetComplianceRuleBase
	{
		// Token: 0x1700048D RID: 1165
		// (get) Token: 0x06000D42 RID: 3394 RVA: 0x0002FC06 File Offset: 0x0002DE06
		// (set) Token: 0x06000D43 RID: 3395 RVA: 0x0002FC1D File Offset: 0x0002DE1D
		[Parameter(Mandatory = false)]
		public Hashtable[] ContentContainsSensitiveInformation
		{
			get
			{
				return (Hashtable[])base.Fields["ContentContainsSensitiveInformation"];
			}
			set
			{
				base.Fields["ContentContainsSensitiveInformation"] = value;
			}
		}

		// Token: 0x1700048E RID: 1166
		// (get) Token: 0x06000D44 RID: 3396 RVA: 0x0002FC30 File Offset: 0x0002DE30
		// (set) Token: 0x06000D45 RID: 3397 RVA: 0x0002FC47 File Offset: 0x0002DE47
		[Parameter(Mandatory = false)]
		public AccessScope? AccessScopeIs
		{
			get
			{
				return (AccessScope?)base.Fields[PsDlpComplianceRuleSchema.AccessScopeIs];
			}
			set
			{
				base.Fields[PsDlpComplianceRuleSchema.AccessScopeIs] = value;
			}
		}

		// Token: 0x1700048F RID: 1167
		// (get) Token: 0x06000D46 RID: 3398 RVA: 0x0002FC5F File Offset: 0x0002DE5F
		// (set) Token: 0x06000D47 RID: 3399 RVA: 0x0002FC76 File Offset: 0x0002DE76
		[Parameter(Mandatory = false)]
		public MultiValuedProperty<string> ContentPropertyContainsWords
		{
			get
			{
				return (MultiValuedProperty<string>)base.Fields[PsDlpComplianceRuleSchema.ContentPropertyContainsWords];
			}
			set
			{
				base.Fields[PsDlpComplianceRuleSchema.ContentPropertyContainsWords] = value;
			}
		}

		// Token: 0x17000490 RID: 1168
		// (get) Token: 0x06000D48 RID: 3400 RVA: 0x0002FC89 File Offset: 0x0002DE89
		// (set) Token: 0x06000D49 RID: 3401 RVA: 0x0002FCAF File Offset: 0x0002DEAF
		[Parameter(Mandatory = false)]
		public SwitchParameter BlockAccess
		{
			get
			{
				return (SwitchParameter)(base.Fields[PsDlpComplianceRuleSchema.BlockAccess] ?? new SwitchParameter(false));
			}
			set
			{
				base.Fields[PsDlpComplianceRuleSchema.BlockAccess] = value;
			}
		}

		// Token: 0x06000D4A RID: 3402 RVA: 0x0002FCC7 File Offset: 0x0002DEC7
		public SetDlpComplianceRule() : base(PolicyScenario.Dlp)
		{
		}

		// Token: 0x06000D4B RID: 3403 RVA: 0x0002FCD0 File Offset: 0x0002DED0
		protected override void StampChangesOn(IConfigurable dataObject)
		{
			RuleStorage ruleStorage = (RuleStorage)dataObject;
			ruleStorage.ResetChangeTracking(true);
			base.PsRulePresentationObject = new PsDlpComplianceRule(ruleStorage);
			PsDlpComplianceRule psDlpComplianceRule = (PsDlpComplianceRule)base.PsRulePresentationObject;
			base.PsRulePresentationObject.PopulateTaskProperties(this, base.DataSession as IConfigurationSession);
			if (base.PsRulePresentationObject.ReadOnly)
			{
				throw new TaskRuleIsTooAdvancedToModifyException(base.PsRulePresentationObject.Name);
			}
			if (ruleStorage.Mode == Mode.PendingDeletion)
			{
				base.WriteError(new ErrorCommonComplianceRuleIsDeletedException(ruleStorage.Name), ErrorCategory.InvalidOperation, null);
			}
			base.StampChangesOn(dataObject);
			this.CopyExplicitParameters();
			if (!psDlpComplianceRule.GetTaskActions().Any<PsComplianceRuleActionBase>())
			{
				throw new RuleContainsNoActionsException(psDlpComplianceRule.Name);
			}
			psDlpComplianceRule.UpdateStorageProperties(this, base.DataSession as IConfigurationSession, false);
		}

		// Token: 0x06000D4C RID: 3404 RVA: 0x0002FD90 File Offset: 0x0002DF90
		protected override void InternalValidate()
		{
			base.InternalValidate();
			if (base.Fields.IsModified("ContentContainsSensitiveInformation") && base.Fields["ContentContainsSensitiveInformation"] != null)
			{
				Utils.ValidateDataClassification(this.ContentContainsSensitiveInformation);
			}
			if (base.Fields.IsModified("AccessScopeIs"))
			{
				Utils.ValidateAccessScopeIsPredicate(this.AccessScopeIs);
			}
		}

		// Token: 0x06000D4D RID: 3405 RVA: 0x0002FDF0 File Offset: 0x0002DFF0
		protected override void CopyExplicitParameters()
		{
			base.CopyExplicitParameters();
			PsDlpComplianceRule psDlpComplianceRule = (PsDlpComplianceRule)base.PsRulePresentationObject;
			if (base.Fields.IsModified("ContentContainsSensitiveInformation"))
			{
				psDlpComplianceRule.ContentContainsSensitiveInformation = this.ContentContainsSensitiveInformation;
			}
			if (base.Fields.IsModified(PsDlpComplianceRuleSchema.ContentPropertyContainsWords))
			{
				psDlpComplianceRule.ContentPropertyContainsWords = this.ContentPropertyContainsWords;
			}
			if (base.Fields.IsModified(PsDlpComplianceRuleSchema.AccessScopeIs))
			{
				psDlpComplianceRule.AccessScopeIs = this.AccessScopeIs;
			}
			if (base.Fields.IsModified(PsDlpComplianceRuleSchema.BlockAccess))
			{
				psDlpComplianceRule.BlockAccess = this.BlockAccess;
			}
		}
	}
}
