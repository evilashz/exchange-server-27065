using System;
using System.Management.Automation;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Management.Tasks;
using Microsoft.Exchange.MessagingPolicies.Rules;
using Microsoft.Exchange.MessagingPolicies.Rules.Tasks;

namespace Microsoft.Exchange.MessagingPolicies.Journaling
{
	// Token: 0x02000A18 RID: 2584
	[Cmdlet("Export", "JournalRuleCollection", SupportsShouldProcess = true, DefaultParameterSetName = "Identity")]
	public sealed class ExportJournalRuleCollection : ExportRuleCollectionTaskBase
	{
		// Token: 0x17001BCA RID: 7114
		// (get) Token: 0x06005CAE RID: 23726 RVA: 0x00186722 File Offset: 0x00184922
		protected override LocalizedString ConfirmationMessage
		{
			get
			{
				return Strings.ConfirmationMessageExportJournalRuleCollection;
			}
		}

		// Token: 0x06005CAF RID: 23727 RVA: 0x00186729 File Offset: 0x00184929
		public ExportJournalRuleCollection()
		{
			base.RuleCollectionName = "JournalingVersioned";
		}

		// Token: 0x06005CB0 RID: 23728 RVA: 0x0018673C File Offset: 0x0018493C
		protected override void InternalProcessRecord()
		{
			try
			{
				base.RuleStorageManager = new ADJournalRuleStorageManager(base.RuleCollectionName, base.DataSession);
			}
			catch (RuleCollectionNotInAdException)
			{
				this.WriteWarning(Strings.RuleCollectionNotFoundDuringExport(base.RuleCollectionName));
				return;
			}
			base.InternalProcessRecord();
		}
	}
}
