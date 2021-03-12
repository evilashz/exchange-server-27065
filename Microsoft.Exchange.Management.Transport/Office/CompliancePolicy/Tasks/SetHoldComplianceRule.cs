using System;
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
	// Token: 0x02000125 RID: 293
	[Cmdlet("Set", "HoldComplianceRule", SupportsShouldProcess = true, DefaultParameterSetName = "Identity")]
	public sealed class SetHoldComplianceRule : SetComplianceRuleBase
	{
		// Token: 0x17000485 RID: 1157
		// (get) Token: 0x06000D1E RID: 3358 RVA: 0x0002F512 File Offset: 0x0002D712
		// (set) Token: 0x06000D1F RID: 3359 RVA: 0x0002F529 File Offset: 0x0002D729
		[Parameter(Mandatory = false)]
		public Unlimited<int>? HoldContent
		{
			get
			{
				return (Unlimited<int>?)base.Fields["HoldContent"];
			}
			set
			{
				base.Fields["HoldContent"] = value;
			}
		}

		// Token: 0x17000486 RID: 1158
		// (get) Token: 0x06000D20 RID: 3360 RVA: 0x0002F541 File Offset: 0x0002D741
		// (set) Token: 0x06000D21 RID: 3361 RVA: 0x0002F558 File Offset: 0x0002D758
		[Parameter(Mandatory = false)]
		public HoldDurationHint HoldDurationDisplayHint
		{
			get
			{
				return (HoldDurationHint)base.Fields["HoldDurationDisplayHint"];
			}
			set
			{
				base.Fields["HoldDurationDisplayHint"] = value;
			}
		}

		// Token: 0x17000487 RID: 1159
		// (get) Token: 0x06000D22 RID: 3362 RVA: 0x0002F570 File Offset: 0x0002D770
		// (set) Token: 0x06000D23 RID: 3363 RVA: 0x0002F587 File Offset: 0x0002D787
		[Parameter(Mandatory = false)]
		public DateTime? ContentDateFrom
		{
			get
			{
				return (DateTime?)base.Fields["ContentDateFrom"];
			}
			set
			{
				base.Fields["ContentDateFrom"] = value;
			}
		}

		// Token: 0x17000488 RID: 1160
		// (get) Token: 0x06000D24 RID: 3364 RVA: 0x0002F59F File Offset: 0x0002D79F
		// (set) Token: 0x06000D25 RID: 3365 RVA: 0x0002F5B6 File Offset: 0x0002D7B6
		[Parameter(Mandatory = false)]
		public DateTime? ContentDateTo
		{
			get
			{
				return (DateTime?)base.Fields["ContentDateTo"];
			}
			set
			{
				base.Fields["ContentDateTo"] = value;
			}
		}

		// Token: 0x06000D26 RID: 3366 RVA: 0x0002F5CE File Offset: 0x0002D7CE
		public SetHoldComplianceRule() : base(PolicyScenario.Hold)
		{
		}

		// Token: 0x06000D27 RID: 3367 RVA: 0x0002F5D8 File Offset: 0x0002D7D8
		protected override void StampChangesOn(IConfigurable dataObject)
		{
			RuleStorage ruleStorage = (RuleStorage)dataObject;
			ruleStorage.ResetChangeTracking(true);
			base.PsRulePresentationObject = new PsHoldRule(ruleStorage);
			PsHoldRule psHoldRule = (PsHoldRule)base.PsRulePresentationObject;
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
			if (!Utils.ValidateContentDateParameter(psHoldRule.ContentDateFrom, psHoldRule.ContentDateTo))
			{
				throw new InvalidContentDateFromAndContentDateToPredicateException();
			}
			if (!psHoldRule.GetTaskActions().Any<PsComplianceRuleActionBase>())
			{
				base.WriteError(new RuleContainsNoActionsException(psHoldRule.Name), ErrorCategory.InvalidData, psHoldRule);
			}
			psHoldRule.UpdateStorageProperties(this, base.DataSession as IConfigurationSession, false);
		}

		// Token: 0x06000D28 RID: 3368 RVA: 0x0002F6B8 File Offset: 0x0002D8B8
		protected override void InternalValidate()
		{
			if (base.Fields.IsModified("HoldContent") && this.HoldContent != null && this.HoldContent.Value <= 0)
			{
				throw new InvalidHoldContentActionException();
			}
			base.InternalValidate();
		}

		// Token: 0x06000D29 RID: 3369 RVA: 0x0002F710 File Offset: 0x0002D910
		protected override void CopyExplicitParameters()
		{
			base.CopyExplicitParameters();
			PsHoldRule psHoldRule = (PsHoldRule)base.PsRulePresentationObject;
			if (base.Fields.IsModified("HoldContent"))
			{
				psHoldRule.HoldContent = this.HoldContent;
			}
			if (base.Fields.IsModified("HoldDurationDisplayHint"))
			{
				psHoldRule.HoldDurationDisplayHint = this.HoldDurationDisplayHint;
			}
			if (base.Fields.IsModified("ContentDateFrom"))
			{
				psHoldRule.ContentDateFrom = this.ContentDateFrom;
			}
			if (base.Fields.IsModified("ContentDateTo"))
			{
				psHoldRule.ContentDateTo = this.ContentDateTo;
			}
		}
	}
}
