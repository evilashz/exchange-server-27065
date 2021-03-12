using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Management.Tasks;
using Microsoft.Exchange.MessagingPolicies.Rules;
using Microsoft.Exchange.MessagingPolicies.Rules.Tasks;

namespace Microsoft.Exchange.MessagingPolicies.Journaling
{
	// Token: 0x02000A1E RID: 2590
	[Cmdlet("enable", "journalrule", SupportsShouldProcess = true)]
	public class EnableJournalRule : EnableRuleTaskBase
	{
		// Token: 0x17001BD8 RID: 7128
		// (get) Token: 0x06005CD4 RID: 23764 RVA: 0x00187218 File Offset: 0x00185418
		protected override LocalizedString ConfirmationMessage
		{
			get
			{
				return Strings.ConfirmationMessageEnableJournalrule(this.Identity.ToString());
			}
		}

		// Token: 0x06005CD5 RID: 23765 RVA: 0x0018722A File Offset: 0x0018542A
		public EnableJournalRule() : base("JournalingVersioned")
		{
		}

		// Token: 0x17001BD9 RID: 7129
		// (get) Token: 0x06005CD6 RID: 23766 RVA: 0x00187237 File Offset: 0x00185437
		// (set) Token: 0x06005CD7 RID: 23767 RVA: 0x0018725D File Offset: 0x0018545D
		[Parameter(Mandatory = false)]
		public SwitchParameter LawfulInterception
		{
			get
			{
				return (SwitchParameter)(base.Fields["LawfulInterception"] ?? new SwitchParameter(false));
			}
			set
			{
				base.Fields["LawfulInterception"] = value;
			}
		}

		// Token: 0x06005CD8 RID: 23768 RVA: 0x00187278 File Offset: 0x00185478
		protected override IConfigurable PrepareDataObject()
		{
			TaskLogger.LogEnter();
			TransportRule transportRule = (TransportRule)base.PrepareDataObject();
			if (base.HasErrors)
			{
				return null;
			}
			JournalingRule journalingRule;
			try
			{
				journalingRule = (JournalingRule)JournalingRuleParser.Instance.GetRule(transportRule.Xml);
			}
			catch (ParserException exception)
			{
				base.WriteError(exception, ErrorCategory.InvalidData, null);
				return null;
			}
			if (journalingRule.GccRuleType != GccType.None && !this.LawfulInterception)
			{
				LocalizedString errorMessageObjectNotFound = base.GetErrorMessageObjectNotFound((this.Identity != null) ? this.Identity.ToString() : null, typeof(RuleIdParameter).ToString(), (base.DataSession != null) ? base.DataSession.Source : null);
				base.WriteError(new ManagementObjectNotFoundException(errorMessageObjectNotFound), ErrorCategory.ObjectNotFound, null);
				return null;
			}
			if (journalingRule.IsTooAdvancedToParse)
			{
				base.WriteError(new InvalidOperationException(Strings.CannotModifyRuleDueToVersion(journalingRule.Name)), ErrorCategory.InvalidOperation, null);
				return null;
			}
			journalingRule.Enabled = RuleState.Enabled;
			string xml = JournalingRuleSerializer.Instance.SaveRuleToString(journalingRule);
			transportRule.Xml = xml;
			TaskLogger.LogExit();
			return transportRule;
		}
	}
}
