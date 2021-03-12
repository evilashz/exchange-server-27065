using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Management.Tasks;
using Microsoft.Exchange.MessagingPolicies.Rules;
using Microsoft.Exchange.MessagingPolicies.Rules.Tasks;

namespace Microsoft.Exchange.MessagingPolicies.Journaling
{
	// Token: 0x02000A1C RID: 2588
	[Cmdlet("remove", "journalrule", SupportsShouldProcess = true, ConfirmImpact = ConfirmImpact.High)]
	public class RemoveJournalRule : RemoveRuleTaskBase
	{
		// Token: 0x17001BD5 RID: 7125
		// (get) Token: 0x06005CCC RID: 23756 RVA: 0x00187050 File Offset: 0x00185250
		protected override LocalizedString ConfirmationMessage
		{
			get
			{
				return Strings.ConfirmationMessageRemoveJournalrule(this.Identity.ToString());
			}
		}

		// Token: 0x06005CCD RID: 23757 RVA: 0x00187062 File Offset: 0x00185262
		public RemoveJournalRule() : base("JournalingVersioned")
		{
		}

		// Token: 0x17001BD6 RID: 7126
		// (get) Token: 0x06005CCE RID: 23758 RVA: 0x0018706F File Offset: 0x0018526F
		// (set) Token: 0x06005CCF RID: 23759 RVA: 0x00187095 File Offset: 0x00185295
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

		// Token: 0x06005CD0 RID: 23760 RVA: 0x001870B0 File Offset: 0x001852B0
		protected override void InternalProcessRecord()
		{
			if (Utils.Exchange12HubServersExist(this))
			{
				this.WriteWarning(Strings.RemoveRuleSyncAcrossDifferentVersionsNeeded);
			}
			TransportRule dataObject = base.DataObject;
			JournalingRule journalingRule;
			try
			{
				journalingRule = (JournalingRule)JournalingRuleParser.Instance.GetRule(dataObject.Xml);
			}
			catch (ParserException)
			{
				journalingRule = null;
			}
			if (journalingRule != null && journalingRule.GccRuleType != GccType.None && !this.LawfulInterception)
			{
				throw new ManagementObjectNotFoundException(base.GetErrorMessageObjectNotFound((this.Identity != null) ? this.Identity.ToString() : null, typeof(RuleIdParameter).ToString(), (base.DataSession != null) ? base.DataSession.Source : null));
			}
			base.InternalProcessRecord();
		}
	}
}
