using System;
using System.Management.Automation;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Management.SystemConfigurationTasks;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.MessagingPolicies.Rules.Tasks
{
	// Token: 0x02000B67 RID: 2919
	[Cmdlet("Remove", "TransportRule", SupportsShouldProcess = true, ConfirmImpact = ConfirmImpact.High)]
	public sealed class RemoveTransportRule : RemoveRuleTaskBase
	{
		// Token: 0x17002140 RID: 8512
		// (get) Token: 0x06006B29 RID: 27433 RVA: 0x001B75F8 File Offset: 0x001B57F8
		protected override LocalizedString ConfirmationMessage
		{
			get
			{
				try
				{
					TransportRule transportRule = (TransportRule)TransportRuleParser.Instance.GetRule(base.DataObject.Xml);
					Guid a;
					if (transportRule.TryGetDlpPolicyId(out a) && a != Guid.Empty)
					{
						return Strings.ConfirmationMessageRemoveTransportRuleThatBelongsToDLpPolicy(this.Identity.ToString(), a.ToString());
					}
				}
				catch (ParserException ex)
				{
					this.WriteWarning(Strings.RuleIsCorrupt(this.Identity.ToString(), ex.Message));
				}
				return Strings.ConfirmationMessageRemoveTransportRule(this.Identity.ToString());
			}
		}

		// Token: 0x06006B2A RID: 27434 RVA: 0x001B769C File Offset: 0x001B589C
		public RemoveTransportRule() : base(Utils.RuleCollectionNameFromRole())
		{
		}

		// Token: 0x06006B2B RID: 27435 RVA: 0x001B76AC File Offset: 0x001B58AC
		protected override void InternalProcessRecord()
		{
			if (Utils.Exchange12HubServersExist(this))
			{
				this.WriteWarning(Strings.RemoveRuleSyncAcrossDifferentVersionsNeeded);
			}
			IConfigDataProvider configDataProvider = new MessagingPoliciesSyncLogDataSession(base.DataSession, null, null);
			configDataProvider.Delete(base.DataObject);
		}
	}
}
