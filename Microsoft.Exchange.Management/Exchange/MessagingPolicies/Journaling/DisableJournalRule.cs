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
	// Token: 0x02000A20 RID: 2592
	[Cmdlet("disable", "journalrule", SupportsShouldProcess = true, ConfirmImpact = ConfirmImpact.High)]
	public class DisableJournalRule : DisableRuleTaskBase
	{
		// Token: 0x17001BDA RID: 7130
		// (get) Token: 0x06005CDB RID: 23771 RVA: 0x00187434 File Offset: 0x00185634
		protected override LocalizedString ConfirmationMessage
		{
			get
			{
				return Strings.ConfirmationMessageDisableJournalrule(this.Identity.ToString());
			}
		}

		// Token: 0x06005CDC RID: 23772 RVA: 0x00187446 File Offset: 0x00185646
		public DisableJournalRule() : base("JournalingVersioned")
		{
		}

		// Token: 0x17001BDB RID: 7131
		// (get) Token: 0x06005CDD RID: 23773 RVA: 0x00187453 File Offset: 0x00185653
		// (set) Token: 0x06005CDE RID: 23774 RVA: 0x00187479 File Offset: 0x00185679
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

		// Token: 0x06005CDF RID: 23775 RVA: 0x00187494 File Offset: 0x00185694
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
			journalingRule.Enabled = RuleState.Disabled;
			string xml = JournalingRuleSerializer.Instance.SaveRuleToString(journalingRule);
			transportRule.Xml = xml;
			TaskLogger.LogExit();
			return transportRule;
		}
	}
}
