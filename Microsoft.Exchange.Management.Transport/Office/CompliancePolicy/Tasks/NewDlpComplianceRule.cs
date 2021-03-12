using System;
using System.Collections;
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
	// Token: 0x0200012C RID: 300
	[Cmdlet("New", "DlpComplianceRule", SupportsShouldProcess = true)]
	public sealed class NewDlpComplianceRule : NewComplianceRuleBase
	{
		// Token: 0x17000489 RID: 1161
		// (get) Token: 0x06000D36 RID: 3382 RVA: 0x0002F9F3 File Offset: 0x0002DBF3
		// (set) Token: 0x06000D37 RID: 3383 RVA: 0x0002F9FB File Offset: 0x0002DBFB
		[Parameter(Mandatory = false)]
		public Hashtable[] ContentContainsSensitiveInformation { get; set; }

		// Token: 0x1700048A RID: 1162
		// (get) Token: 0x06000D38 RID: 3384 RVA: 0x0002FA04 File Offset: 0x0002DC04
		// (set) Token: 0x06000D39 RID: 3385 RVA: 0x0002FA1B File Offset: 0x0002DC1B
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

		// Token: 0x1700048B RID: 1163
		// (get) Token: 0x06000D3A RID: 3386 RVA: 0x0002FA33 File Offset: 0x0002DC33
		// (set) Token: 0x06000D3B RID: 3387 RVA: 0x0002FA4A File Offset: 0x0002DC4A
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

		// Token: 0x1700048C RID: 1164
		// (get) Token: 0x06000D3C RID: 3388 RVA: 0x0002FA5D File Offset: 0x0002DC5D
		// (set) Token: 0x06000D3D RID: 3389 RVA: 0x0002FA83 File Offset: 0x0002DC83
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

		// Token: 0x06000D3E RID: 3390 RVA: 0x0002FA9B File Offset: 0x0002DC9B
		public NewDlpComplianceRule() : base(PolicyScenario.Dlp)
		{
		}

		// Token: 0x06000D3F RID: 3391 RVA: 0x0002FAA4 File Offset: 0x0002DCA4
		protected override void InternalValidate()
		{
			base.InternalValidate();
			if (this.ContentContainsSensitiveInformation != null)
			{
				Utils.ValidateDataClassification(this.ContentContainsSensitiveInformation);
			}
			Utils.ValidateAccessScopeIsPredicate(this.AccessScopeIs);
		}

		// Token: 0x06000D40 RID: 3392 RVA: 0x0002FACC File Offset: 0x0002DCCC
		protected override IConfigurable PrepareDataObject()
		{
			RuleStorage ruleStorage = (RuleStorage)base.PrepareDataObject();
			ruleStorage.Name = base.Name;
			ruleStorage.SetId(((ADObjectId)this.policyStorage.Identity).GetChildId(base.Name));
			ruleStorage.MasterIdentity = Guid.NewGuid();
			PsDlpComplianceRule psDlpComplianceRule = new PsDlpComplianceRule(ruleStorage)
			{
				Comment = base.Comment,
				Disabled = base.Disabled,
				Mode = Mode.Enforce,
				Policy = Utils.GetUniversalIdentity(this.policyStorage),
				Workload = this.policyStorage.Workload,
				ContentPropertyContainsWords = this.ContentPropertyContainsWords,
				ContentContainsSensitiveInformation = this.ContentContainsSensitiveInformation,
				AccessScopeIs = this.AccessScopeIs,
				BlockAccess = this.BlockAccess
			};
			if (!psDlpComplianceRule.GetTaskActions().Any<PsComplianceRuleActionBase>())
			{
				throw new RuleContainsNoActionsException(psDlpComplianceRule.Name);
			}
			ADObjectId adobjectId;
			base.TryGetExecutingUserId(out adobjectId);
			psDlpComplianceRule.UpdateStorageProperties(this, base.DataSession as IConfigurationSession, true);
			return ruleStorage;
		}

		// Token: 0x06000D41 RID: 3393 RVA: 0x0002FBD4 File Offset: 0x0002DDD4
		protected override void WriteResult(IConfigurable dataObject)
		{
			PsDlpComplianceRule psDlpComplianceRule = new PsDlpComplianceRule(dataObject as RuleStorage);
			psDlpComplianceRule.PopulateTaskProperties(this, base.DataSession as IConfigurationSession);
			base.WriteResult(psDlpComplianceRule);
		}
	}
}
