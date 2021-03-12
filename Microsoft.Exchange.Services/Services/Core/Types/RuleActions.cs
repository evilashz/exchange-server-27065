using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics.Components.Services;
using Microsoft.Exchange.Services.Core.DataConverter;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x02000589 RID: 1417
	[XmlType(TypeName = "RuleActionsType", Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	public class RuleActions
	{
		// Token: 0x170006A2 RID: 1698
		// (get) Token: 0x0600275F RID: 10079 RVA: 0x000A742E File Offset: 0x000A562E
		// (set) Token: 0x06002760 RID: 10080 RVA: 0x000A7436 File Offset: 0x000A5636
		[XmlArrayItem("String", Type = typeof(string))]
		[XmlArray(Order = 0)]
		public string[] AssignCategories { get; set; }

		// Token: 0x170006A3 RID: 1699
		// (get) Token: 0x06002761 RID: 10081 RVA: 0x000A743F File Offset: 0x000A563F
		// (set) Token: 0x06002762 RID: 10082 RVA: 0x000A7447 File Offset: 0x000A5647
		[XmlElement(Order = 1)]
		public TargetFolderId CopyToFolder { get; set; }

		// Token: 0x170006A4 RID: 1700
		// (get) Token: 0x06002763 RID: 10083 RVA: 0x000A7450 File Offset: 0x000A5650
		// (set) Token: 0x06002764 RID: 10084 RVA: 0x000A7458 File Offset: 0x000A5658
		[XmlElement(Order = 2)]
		public bool Delete { get; set; }

		// Token: 0x170006A5 RID: 1701
		// (get) Token: 0x06002765 RID: 10085 RVA: 0x000A7461 File Offset: 0x000A5661
		// (set) Token: 0x06002766 RID: 10086 RVA: 0x000A7469 File Offset: 0x000A5669
		[XmlIgnore]
		public bool DeleteSpecified { get; set; }

		// Token: 0x170006A6 RID: 1702
		// (get) Token: 0x06002767 RID: 10087 RVA: 0x000A7472 File Offset: 0x000A5672
		// (set) Token: 0x06002768 RID: 10088 RVA: 0x000A747A File Offset: 0x000A567A
		[XmlArrayItem("Address", Type = typeof(EmailAddressWrapper))]
		[XmlArray(Order = 3)]
		public EmailAddressWrapper[] ForwardAsAttachmentToRecipients { get; set; }

		// Token: 0x170006A7 RID: 1703
		// (get) Token: 0x06002769 RID: 10089 RVA: 0x000A7483 File Offset: 0x000A5683
		// (set) Token: 0x0600276A RID: 10090 RVA: 0x000A748B File Offset: 0x000A568B
		[XmlArrayItem("Address", Type = typeof(EmailAddressWrapper))]
		[XmlArray(Order = 4)]
		public EmailAddressWrapper[] ForwardToRecipients { get; set; }

		// Token: 0x170006A8 RID: 1704
		// (get) Token: 0x0600276B RID: 10091 RVA: 0x000A7494 File Offset: 0x000A5694
		// (set) Token: 0x0600276C RID: 10092 RVA: 0x000A749C File Offset: 0x000A569C
		[XmlElement(Order = 5)]
		public ImportanceType MarkImportance { get; set; }

		// Token: 0x170006A9 RID: 1705
		// (get) Token: 0x0600276D RID: 10093 RVA: 0x000A74A5 File Offset: 0x000A56A5
		// (set) Token: 0x0600276E RID: 10094 RVA: 0x000A74AD File Offset: 0x000A56AD
		[XmlIgnore]
		public bool MarkImportanceSpecified { get; set; }

		// Token: 0x170006AA RID: 1706
		// (get) Token: 0x0600276F RID: 10095 RVA: 0x000A74B6 File Offset: 0x000A56B6
		// (set) Token: 0x06002770 RID: 10096 RVA: 0x000A74BE File Offset: 0x000A56BE
		[XmlElement(Order = 6)]
		public bool MarkAsRead { get; set; }

		// Token: 0x170006AB RID: 1707
		// (get) Token: 0x06002771 RID: 10097 RVA: 0x000A74C7 File Offset: 0x000A56C7
		// (set) Token: 0x06002772 RID: 10098 RVA: 0x000A74CF File Offset: 0x000A56CF
		[XmlIgnore]
		public bool MarkAsReadSpecified { get; set; }

		// Token: 0x170006AC RID: 1708
		// (get) Token: 0x06002773 RID: 10099 RVA: 0x000A74D8 File Offset: 0x000A56D8
		// (set) Token: 0x06002774 RID: 10100 RVA: 0x000A74E0 File Offset: 0x000A56E0
		[XmlElement(Order = 7)]
		public TargetFolderId MoveToFolder { get; set; }

		// Token: 0x170006AD RID: 1709
		// (get) Token: 0x06002775 RID: 10101 RVA: 0x000A74E9 File Offset: 0x000A56E9
		// (set) Token: 0x06002776 RID: 10102 RVA: 0x000A74F1 File Offset: 0x000A56F1
		[XmlElement(Order = 8)]
		public bool PermanentDelete { get; set; }

		// Token: 0x170006AE RID: 1710
		// (get) Token: 0x06002777 RID: 10103 RVA: 0x000A74FA File Offset: 0x000A56FA
		// (set) Token: 0x06002778 RID: 10104 RVA: 0x000A7502 File Offset: 0x000A5702
		[XmlIgnore]
		public bool PermanentDeleteSpecified { get; set; }

		// Token: 0x170006AF RID: 1711
		// (get) Token: 0x06002779 RID: 10105 RVA: 0x000A750B File Offset: 0x000A570B
		// (set) Token: 0x0600277A RID: 10106 RVA: 0x000A7513 File Offset: 0x000A5713
		[XmlArrayItem("Address", Type = typeof(EmailAddressWrapper))]
		[XmlArray(Order = 9)]
		public EmailAddressWrapper[] RedirectToRecipients { get; set; }

		// Token: 0x170006B0 RID: 1712
		// (get) Token: 0x0600277B RID: 10107 RVA: 0x000A751C File Offset: 0x000A571C
		// (set) Token: 0x0600277C RID: 10108 RVA: 0x000A7524 File Offset: 0x000A5724
		[XmlArrayItem("Address", Type = typeof(EmailAddressWrapper))]
		[XmlArray(Order = 10)]
		public EmailAddressWrapper[] SendSMSAlertToRecipients { get; set; }

		// Token: 0x170006B1 RID: 1713
		// (get) Token: 0x0600277D RID: 10109 RVA: 0x000A752D File Offset: 0x000A572D
		// (set) Token: 0x0600277E RID: 10110 RVA: 0x000A7535 File Offset: 0x000A5735
		[XmlElement(Order = 11)]
		public ItemId ServerReplyWithMessage { get; set; }

		// Token: 0x170006B2 RID: 1714
		// (get) Token: 0x0600277F RID: 10111 RVA: 0x000A753E File Offset: 0x000A573E
		// (set) Token: 0x06002780 RID: 10112 RVA: 0x000A7546 File Offset: 0x000A5746
		[XmlElement(Order = 12)]
		public bool StopProcessingRules { get; set; }

		// Token: 0x170006B3 RID: 1715
		// (get) Token: 0x06002781 RID: 10113 RVA: 0x000A754F File Offset: 0x000A574F
		// (set) Token: 0x06002782 RID: 10114 RVA: 0x000A7557 File Offset: 0x000A5757
		[XmlIgnore]
		public bool StopProcessingRulesSpecified { get; set; }

		// Token: 0x170006B4 RID: 1716
		// (get) Token: 0x06002783 RID: 10115 RVA: 0x000A7560 File Offset: 0x000A5760
		// (set) Token: 0x06002784 RID: 10116 RVA: 0x000A7568 File Offset: 0x000A5768
		[XmlIgnore]
		internal EwsRule Rule { get; set; }

		// Token: 0x06002785 RID: 10117 RVA: 0x000A7571 File Offset: 0x000A5771
		public RuleActions()
		{
		}

		// Token: 0x06002786 RID: 10118 RVA: 0x000A7579 File Offset: 0x000A5779
		private RuleActions(EwsRule rule)
		{
			this.Rule = rule;
		}

		// Token: 0x06002787 RID: 10119 RVA: 0x000A7588 File Offset: 0x000A5788
		internal static RuleActions CreateFromServerRuleActions(IList<ActionBase> serverActions, EwsRule rule, int hashCode, MailboxSession session)
		{
			RuleActions ruleActions = new RuleActions(rule);
			foreach (ActionBase actionBase in serverActions)
			{
				switch (actionBase.ActionType)
				{
				case ActionType.MoveToFolderAction:
					ruleActions.MoveToFolder = new TargetFolderId(IdConverter.ConvertStoreFolderIdToFolderId(((MoveToFolderAction)actionBase).Id, session));
					continue;
				case ActionType.DeleteAction:
					ruleActions.Delete = true;
					ruleActions.DeleteSpecified = true;
					continue;
				case ActionType.CopyToFolderAction:
					ruleActions.CopyToFolder = new TargetFolderId(IdConverter.ConvertStoreFolderIdToFolderId(((CopyToFolderAction)actionBase).Id, session));
					continue;
				case ActionType.ForwardToRecipientsAction:
					ruleActions.ForwardToRecipients = ParticipantsAddressesConverter.ToAddresses(((ForwardToRecipientsAction)actionBase).Participants);
					continue;
				case ActionType.ForwardAsAttachmentToRecipientsAction:
					ruleActions.ForwardAsAttachmentToRecipients = ParticipantsAddressesConverter.ToAddresses(((ForwardAsAttachmentToRecipientsAction)actionBase).Participants);
					continue;
				case ActionType.RedirectToRecipientsAction:
					ruleActions.RedirectToRecipients = ParticipantsAddressesConverter.ToAddresses(((RedirectToRecipientsAction)actionBase).Participants);
					continue;
				case ActionType.ServerReplyMessageAction:
					ruleActions.ServerReplyWithMessage = IdConverter.ConvertStoreItemIdToItemId(((ServerReplyMessageAction)actionBase).Id, session);
					continue;
				case ActionType.MarkImportanceAction:
					ruleActions.MarkImportance = (ImportanceType)((MarkImportanceAction)actionBase).Importance;
					ruleActions.MarkImportanceSpecified = true;
					continue;
				case ActionType.StopProcessingAction:
					ruleActions.StopProcessingRules = true;
					ruleActions.StopProcessingRulesSpecified = true;
					continue;
				case ActionType.SendSmsAlertToRecipientsAction:
					ruleActions.SendSMSAlertToRecipients = ParticipantsAddressesConverter.ToAddresses(((SendSmsAlertToRecipientsAction)actionBase).Participants);
					continue;
				case ActionType.AssignCategoriesAction:
					ruleActions.AssignCategories = ((AssignCategoriesAction)actionBase).Categories;
					continue;
				case ActionType.PermanentDeleteAction:
					ruleActions.PermanentDelete = true;
					ruleActions.PermanentDeleteSpecified = true;
					continue;
				case ActionType.MarkAsReadAction:
					ruleActions.MarkAsRead = true;
					ruleActions.MarkAsReadSpecified = true;
					continue;
				}
				ExTraceGlobals.GetInboxRulesCallTracer.TraceError<ActionType>((long)hashCode, "UnsupportedPredicateType={0};", actionBase.ActionType);
				rule.IsNotSupported = true;
				return null;
			}
			return ruleActions;
		}

		// Token: 0x06002788 RID: 10120 RVA: 0x000A7794 File Offset: 0x000A5994
		internal bool HasActions()
		{
			return this.HasNonBooleanActions() || this.Delete || this.MarkAsRead || this.PermanentDelete || this.StopProcessingRules;
		}

		// Token: 0x06002789 RID: 10121 RVA: 0x000A77C9 File Offset: 0x000A59C9
		internal bool SpecifiedActions()
		{
			return this.HasNonBooleanActions() || this.DeleteSpecified || this.MarkAsReadSpecified || this.PermanentDeleteSpecified || this.StopProcessingRulesSpecified;
		}

		// Token: 0x0600278A RID: 10122 RVA: 0x000A7800 File Offset: 0x000A5A00
		private bool HasNonBooleanActions()
		{
			return (this.AssignCategories != null && 0 < this.AssignCategories.Length) || this.CopyToFolder != null || (this.ForwardAsAttachmentToRecipients != null && 0 < this.ForwardAsAttachmentToRecipients.Length) || (this.ForwardToRecipients != null && 0 < this.ForwardToRecipients.Length) || this.MarkImportanceSpecified || this.MoveToFolder != null || (this.RedirectToRecipients != null && 0 < this.RedirectToRecipients.Length) || (this.SendSMSAlertToRecipients != null && 0 < this.SendSMSAlertToRecipients.Length) || this.ServerReplyWithMessage != null;
		}
	}
}
