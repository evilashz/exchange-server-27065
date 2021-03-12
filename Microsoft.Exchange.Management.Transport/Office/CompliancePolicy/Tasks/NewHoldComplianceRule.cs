using System;
using System.Linq;
using System.Management.Automation;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Data.Directory.UnifiedPolicy;
using Microsoft.Exchange.Management.Transport;
using Microsoft.Office.CompliancePolicy.PolicyConfiguration;
using Microsoft.Office.CompliancePolicy.PolicyEvaluation;

namespace Microsoft.Office.CompliancePolicy.Tasks
{
	// Token: 0x02000124 RID: 292
	[Cmdlet("New", "HoldComplianceRule", SupportsShouldProcess = true)]
	public sealed class NewHoldComplianceRule : NewComplianceRuleBase
	{
		// Token: 0x17000480 RID: 1152
		// (get) Token: 0x06000D10 RID: 3344 RVA: 0x0002F263 File Offset: 0x0002D463
		// (set) Token: 0x06000D11 RID: 3345 RVA: 0x0002F27A File Offset: 0x0002D47A
		[Parameter(Mandatory = false)]
		public DateTime? ContentDateFrom
		{
			get
			{
				return (DateTime?)base.Fields[PsHoldRuleSchema.ContentDateFrom];
			}
			set
			{
				base.Fields[PsHoldRuleSchema.ContentDateFrom] = value;
			}
		}

		// Token: 0x17000481 RID: 1153
		// (get) Token: 0x06000D12 RID: 3346 RVA: 0x0002F292 File Offset: 0x0002D492
		// (set) Token: 0x06000D13 RID: 3347 RVA: 0x0002F2A9 File Offset: 0x0002D4A9
		[Parameter(Mandatory = false)]
		public DateTime? ContentDateTo
		{
			get
			{
				return (DateTime?)base.Fields[PsHoldRuleSchema.ContentDateTo];
			}
			set
			{
				base.Fields[PsHoldRuleSchema.ContentDateTo] = value;
			}
		}

		// Token: 0x17000482 RID: 1154
		// (get) Token: 0x06000D14 RID: 3348 RVA: 0x0002F2C1 File Offset: 0x0002D4C1
		// (set) Token: 0x06000D15 RID: 3349 RVA: 0x0002F2D8 File Offset: 0x0002D4D8
		[Parameter(Mandatory = false)]
		public string ContentMatchQuery
		{
			get
			{
				return (string)base.Fields[PsComplianceRuleBaseSchema.ContentMatchQuery];
			}
			set
			{
				base.Fields[PsComplianceRuleBaseSchema.ContentMatchQuery] = value;
			}
		}

		// Token: 0x17000483 RID: 1155
		// (get) Token: 0x06000D16 RID: 3350 RVA: 0x0002F2EB File Offset: 0x0002D4EB
		// (set) Token: 0x06000D17 RID: 3351 RVA: 0x0002F2F3 File Offset: 0x0002D4F3
		[Parameter(Mandatory = false)]
		public Unlimited<int>? HoldContent { get; set; }

		// Token: 0x17000484 RID: 1156
		// (get) Token: 0x06000D18 RID: 3352 RVA: 0x0002F2FC File Offset: 0x0002D4FC
		// (set) Token: 0x06000D19 RID: 3353 RVA: 0x0002F327 File Offset: 0x0002D527
		[Parameter(Mandatory = false)]
		public HoldDurationHint HoldDurationDisplayHint
		{
			get
			{
				if (base.Fields[PsHoldRuleSchema.HoldDurationDisplayHint] != null)
				{
					return (HoldDurationHint)base.Fields[PsHoldRuleSchema.HoldDurationDisplayHint];
				}
				return HoldDurationHint.Days;
			}
			set
			{
				base.Fields[PsHoldRuleSchema.HoldDurationDisplayHint] = value;
			}
		}

		// Token: 0x06000D1A RID: 3354 RVA: 0x0002F33F File Offset: 0x0002D53F
		public NewHoldComplianceRule() : base(PolicyScenario.Hold)
		{
		}

		// Token: 0x06000D1B RID: 3355 RVA: 0x0002F348 File Offset: 0x0002D548
		protected override void InternalValidate()
		{
			base.InternalValidate();
			if (Utils.LoadRuleStoragesByPolicy(base.DataSession, this.policyStorage, this.policyStorage.Identity).Any<RuleStorage>())
			{
				throw new MulipleComplianceRulesFoundInPolicyException(this.policyStorage.Name);
			}
			if (this.HoldContent != null && this.HoldContent.Value <= 0)
			{
				throw new InvalidHoldContentActionException();
			}
			if (!Utils.ValidateContentDateParameter(this.ContentDateFrom, this.ContentDateTo))
			{
				throw new InvalidContentDateFromAndContentDateToPredicateException();
			}
		}

		// Token: 0x06000D1C RID: 3356 RVA: 0x0002F3DC File Offset: 0x0002D5DC
		protected override IConfigurable PrepareDataObject()
		{
			RuleStorage ruleStorage = (RuleStorage)base.PrepareDataObject();
			ruleStorage.Name = base.Name;
			ruleStorage.SetId(((ADObjectId)this.policyStorage.Identity).GetChildId(base.Name));
			PsHoldRule psHoldRule = new PsHoldRule(ruleStorage)
			{
				Comment = base.Comment,
				Disabled = base.Disabled,
				Mode = Mode.Enforce,
				Policy = Utils.GetUniversalIdentity(this.policyStorage),
				Workload = this.policyStorage.Workload,
				ContentMatchQuery = this.ContentMatchQuery,
				ContentDateFrom = this.ContentDateFrom,
				ContentDateTo = this.ContentDateTo,
				HoldContent = this.HoldContent,
				HoldDurationDisplayHint = this.HoldDurationDisplayHint
			};
			if (!psHoldRule.GetTaskActions().Any<PsComplianceRuleActionBase>())
			{
				throw new RuleContainsNoActionsException(psHoldRule.Name);
			}
			ADObjectId adobjectId;
			base.TryGetExecutingUserId(out adobjectId);
			psHoldRule.UpdateStorageProperties(this, base.DataSession as IConfigurationSession, true);
			return ruleStorage;
		}

		// Token: 0x06000D1D RID: 3357 RVA: 0x0002F4E0 File Offset: 0x0002D6E0
		protected override void WriteResult(IConfigurable dataObject)
		{
			PsHoldRule psHoldRule = new PsHoldRule(dataObject as RuleStorage);
			psHoldRule.PopulateTaskProperties(this, base.DataSession as IConfigurationSession);
			base.WriteResult(psHoldRule);
		}
	}
}
