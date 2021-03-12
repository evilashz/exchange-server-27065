using System;
using System.IO;
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
	// Token: 0x02000A19 RID: 2585
	[Cmdlet("Import", "JournalRuleCollection", SupportsShouldProcess = true, DefaultParameterSetName = "Identity")]
	public sealed class ImportJournalRuleCollection : GetMultitenancySystemConfigurationObjectTask<RuleIdParameter, TransportRule>
	{
		// Token: 0x17001BCB RID: 7115
		// (get) Token: 0x06005CB1 RID: 23729 RVA: 0x0018678C File Offset: 0x0018498C
		protected override LocalizedString ConfirmationMessage
		{
			get
			{
				return Strings.ConfirmationMessageImportJournalRuleCollection;
			}
		}

		// Token: 0x17001BCC RID: 7116
		// (get) Token: 0x06005CB3 RID: 23731 RVA: 0x0018679B File Offset: 0x0018499B
		// (set) Token: 0x06005CB4 RID: 23732 RVA: 0x001867B2 File Offset: 0x001849B2
		[Parameter(Mandatory = true, Position = 0)]
		public byte[] FileData
		{
			get
			{
				return (byte[])base.Fields["FileData"];
			}
			set
			{
				base.Fields["FileData"] = value;
			}
		}

		// Token: 0x06005CB5 RID: 23733 RVA: 0x001867C5 File Offset: 0x001849C5
		protected override void InternalValidate()
		{
			if (this.FileData == null)
			{
				base.WriteError(new ArgumentException(Strings.ImportFileDataIsNull), ErrorCategory.InvalidArgument, "FileData");
				return;
			}
			base.InternalValidate();
		}

		// Token: 0x06005CB6 RID: 23734 RVA: 0x001867F4 File Offset: 0x001849F4
		protected override void InternalProcessRecord()
		{
			if (!base.ShouldContinue(Strings.PromptToOverwriteRulesOnImport))
			{
				return;
			}
			ADJournalRuleStorageManager adjournalRuleStorageManager;
			try
			{
				adjournalRuleStorageManager = new ADJournalRuleStorageManager("JournalingVersioned", base.DataSession);
			}
			catch (RuleCollectionNotInAdException exception)
			{
				base.WriteError(exception, ErrorCategory.InvalidOperation, null);
				return;
			}
			TransportRuleCollection transportRuleCollection = null;
			using (Stream stream = new MemoryStream(this.FileData))
			{
				try
				{
					transportRuleCollection = (TransportRuleCollection)JournalingRuleParser.Instance.LoadStream(stream);
				}
				catch (ParserException exception2)
				{
					base.WriteError(exception2, ErrorCategory.InvalidData, "FileData");
					return;
				}
			}
			JournalRuleObject journalRuleObject = new JournalRuleObject();
			foreach (Microsoft.Exchange.MessagingPolicies.Rules.Rule rule in transportRuleCollection)
			{
				JournalingRule journalingRule = (JournalingRule)rule;
				try
				{
					journalRuleObject.Deserialize(journalingRule);
				}
				catch (RecipientInvalidException exception3)
				{
					base.WriteError(exception3, ErrorCategory.InvalidArgument, journalRuleObject.JournalEmailAddress);
					return;
				}
				catch (JournalRuleCorruptException exception4)
				{
					base.WriteError(exception4, ErrorCategory.InvalidArgument, journalingRule.Name);
				}
				if (journalingRule.IsTooAdvancedToParse)
				{
					base.WriteError(new InvalidOperationException(Strings.CannotCreateRuleDueToVersion(journalingRule.Name)), ErrorCategory.InvalidOperation, null);
					return;
				}
			}
			try
			{
				adjournalRuleStorageManager.ReplaceRules(transportRuleCollection, this.ResolveCurrentOrganization());
			}
			catch (DataValidationException exception5)
			{
				base.WriteError(exception5, ErrorCategory.InvalidArgument, null);
			}
		}
	}
}
