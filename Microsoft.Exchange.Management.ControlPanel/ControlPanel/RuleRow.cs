using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Data.Storage.Management;
using Microsoft.Exchange.Management.Common;
using Microsoft.Exchange.Management.RecipientTasks;
using Microsoft.Exchange.MessagingPolicies.Journaling;
using Microsoft.Exchange.MessagingPolicies.Rules;
using Microsoft.Exchange.MessagingPolicies.Rules.Tasks;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x020003F8 RID: 1016
	[DataContract]
	public class RuleRow : BaseRow
	{
		// Token: 0x06003388 RID: 13192 RVA: 0x000A104C File Offset: 0x0009F24C
		public RuleRow(Microsoft.Exchange.MessagingPolicies.Rules.Tasks.Rule rule) : base(rule)
		{
			this.Name = rule.Name;
			this.Priority = rule.Priority;
			this.Enabled = (rule.State == RuleState.Enabled);
			this.CaptionText = Strings.EditRuleCaption(rule.Name);
			this.DlpPolicy = rule.DlpPolicy;
			this.RuleVersion = rule.RuleVersion;
			this.RuleMode = rule.Mode.ToStringWithNull();
			int num = 0;
			num += ((!rule.From.IsNullOrEmpty()) ? 1 : 0);
			num += ((!rule.SentTo.IsNullOrEmpty()) ? 1 : 0);
			num += ((rule.FromScope != null) ? 1 : 0);
			num += ((rule.SentToScope != null) ? 1 : 0);
			num += ((!rule.FromMemberOf.IsNullOrEmpty()) ? 1 : 0);
			num += ((!rule.SentToMemberOf.IsNullOrEmpty()) ? 1 : 0);
			num += ((!rule.SubjectOrBodyContainsWords.IsNullOrEmpty()) ? 1 : 0);
			num += ((!rule.FromAddressContainsWords.IsNullOrEmpty()) ? 1 : 0);
			num += ((!rule.RecipientAddressContainsWords.IsNullOrEmpty()) ? 1 : 0);
			num += ((!rule.AttachmentContainsWords.IsNullOrEmpty()) ? 1 : 0);
			num += ((!rule.AttachmentMatchesPatterns.IsNullOrEmpty()) ? 1 : 0);
			num += (rule.AttachmentIsUnsupported ? 1 : 0);
			num += ((!rule.SubjectOrBodyMatchesPatterns.IsNullOrEmpty()) ? 1 : 0);
			num += ((!rule.FromAddressMatchesPatterns.IsNullOrEmpty()) ? 1 : 0);
			num += ((!rule.RecipientAddressMatchesPatterns.IsNullOrEmpty()) ? 1 : 0);
			num += ((!rule.AttachmentNameMatchesPatterns.IsNullOrEmpty()) ? 1 : 0);
			num += (rule.HasNoClassification ? 1 : 0);
			num += ((!rule.SubjectContainsWords.IsNullOrEmpty()) ? 1 : 0);
			num += ((!rule.SubjectMatchesPatterns.IsNullOrEmpty()) ? 1 : 0);
			num += ((!rule.AnyOfToHeader.IsNullOrEmpty()) ? 1 : 0);
			num += ((!rule.AnyOfToHeaderMemberOf.IsNullOrEmpty()) ? 1 : 0);
			num += ((!rule.AnyOfCcHeader.IsNullOrEmpty()) ? 1 : 0);
			num += ((!rule.AnyOfCcHeaderMemberOf.IsNullOrEmpty()) ? 1 : 0);
			num += ((!rule.AnyOfToCcHeader.IsNullOrEmpty()) ? 1 : 0);
			num += ((!rule.AnyOfToCcHeaderMemberOf.IsNullOrEmpty()) ? 1 : 0);
			num += ((rule.MessageTypeMatches != null) ? 1 : 0);
			num += ((rule.SenderManagementRelationship != null) ? 1 : 0);
			num += ((rule.WithImportance != null) ? 1 : 0);
			num += ((!rule.RecipientInSenderList.IsNullOrEmpty()) ? 1 : 0);
			num += ((!rule.SenderInRecipientList.IsNullOrEmpty()) ? 1 : 0);
			num += ((rule.HeaderContainsMessageHeader != null && !rule.HeaderContainsWords.IsNullOrEmpty()) ? 1 : 0);
			num += ((!rule.BetweenMemberOf1.IsNullOrEmpty() && !rule.BetweenMemberOf2.IsNullOrEmpty()) ? 1 : 0);
			num += ((rule.HeaderMatchesMessageHeader != null && !rule.HeaderMatchesPatterns.IsNullOrEmpty()) ? 1 : 0);
			num += ((rule.ManagerForEvaluatedUser != null && !rule.ManagerAddresses.IsNullOrEmpty()) ? 1 : 0);
			num += ((rule.ADComparisonAttribute != null && rule.ADComparisonOperator != null) ? 1 : 0);
			num += ((rule.AttachmentSizeOver != null) ? 1 : 0);
			num += ((rule.SCLOver != null) ? 1 : 0);
			num += ((rule.HasClassification != null) ? 1 : 0);
			num += ((!rule.SenderADAttributeContainsWords.IsNullOrEmpty()) ? 1 : 0);
			num += ((!rule.SenderADAttributeMatchesPatterns.IsNullOrEmpty()) ? 1 : 0);
			num += ((!rule.RecipientADAttributeContainsWords.IsNullOrEmpty()) ? 1 : 0);
			num += ((!rule.RecipientADAttributeMatchesPatterns.IsNullOrEmpty()) ? 1 : 0);
			num += ((rule.MessageSizeOver != null) ? 1 : 0);
			num += ((!rule.MessageContainsDataClassifications.IsNullOrEmpty()) ? 1 : 0);
			num += ((!rule.AttachmentExtensionMatchesWords.IsNullOrEmpty()) ? 1 : 0);
			num += (rule.HasSenderOverride ? 1 : 0);
			num += (rule.AttachmentHasExecutableContent ? 1 : 0);
			num += ((rule.SenderIpRanges != null) ? 1 : 0);
			num += (rule.AttachmentProcessingLimitExceeded ? 1 : 0);
			num += ((!rule.SenderDomainIs.IsNullOrEmpty()) ? 1 : 0);
			num += ((!rule.RecipientDomainIs.IsNullOrEmpty()) ? 1 : 0);
			num += ((!rule.ContentCharacterSetContainsWords.IsNullOrEmpty()) ? 1 : 0);
			num += (rule.AttachmentIsPasswordProtected ? 1 : 0);
			num += ((!rule.AnyOfRecipientAddressContainsWords.IsNullOrEmpty()) ? 1 : 0);
			num += ((!rule.AnyOfRecipientAddressMatchesPatterns.IsNullOrEmpty()) ? 1 : 0);
			int num2 = 0;
			num2 += ((!rule.ModerateMessageByUser.IsNullOrEmpty()) ? 1 : 0);
			num2 += ((!rule.RedirectMessageTo.IsNullOrEmpty()) ? 1 : 0);
			num2 += ((rule.RejectMessageReasonText != null || rule.SenderNotificationType != null) ? 1 : 0);
			num2 += (rule.DeleteMessage ? 1 : 0);
			num2 += ((!rule.BlindCopyTo.IsNullOrEmpty()) ? 1 : 0);
			num2 += ((rule.ApplyHtmlDisclaimerText != null) ? 1 : 0);
			num2 += ((rule.RemoveHeader != null) ? 1 : 0);
			num2 += ((!rule.AddToRecipients.IsNullOrEmpty()) ? 1 : 0);
			num2 += ((!rule.CopyTo.IsNullOrEmpty()) ? 1 : 0);
			num2 += (rule.ModerateMessageByManager ? 1 : 0);
			num2 += ((!string.IsNullOrEmpty(rule.PrependSubject)) ? 1 : 0);
			num2 += ((rule.AddManagerAsRecipientType != null) ? 1 : 0);
			num2 += ((rule.ApplyRightsProtectionTemplate != null) ? 1 : 0);
			num2 += ((rule.SetHeaderName != null && rule.SetHeaderValue != null) ? 1 : 0);
			num2 += ((rule.SetSCL != null) ? 1 : 0);
			num2 += ((rule.ApplyClassification != null) ? 1 : 0);
			num2 += (rule.StopRuleProcessing ? 1 : 0);
			num2 += ((!string.IsNullOrEmpty(rule.SetAuditSeverity)) ? 1 : 0);
			num2 += ((rule.GenerateIncidentReport != null) ? 1 : 0);
			num2 += (rule.RouteMessageOutboundRequireTls ? 1 : 0);
			num2 += (rule.ApplyOME ? 1 : 0);
			num2 += (rule.RemoveOME ? 1 : 0);
			num2 += (rule.Quarantine ? 1 : 0);
			num2 += ((rule.RouteMessageOutboundConnector != null) ? 1 : 0);
			num2 += ((rule.GenerateNotification != null) ? 1 : 0);
			int num3 = 0;
			num3 += ((!rule.ExceptIfFrom.IsNullOrEmpty()) ? 1 : 0);
			num3 += ((!rule.ExceptIfSentTo.IsNullOrEmpty()) ? 1 : 0);
			num3 += ((rule.ExceptIfFromScope != null) ? 1 : 0);
			num3 += ((rule.ExceptIfSentToScope != null) ? 1 : 0);
			num3 += ((!rule.ExceptIfFromMemberOf.IsNullOrEmpty()) ? 1 : 0);
			num3 += ((!rule.ExceptIfSentToMemberOf.IsNullOrEmpty()) ? 1 : 0);
			num3 += ((!rule.ExceptIfSubjectOrBodyContainsWords.IsNullOrEmpty()) ? 1 : 0);
			num3 += ((!rule.ExceptIfFromAddressContainsWords.IsNullOrEmpty()) ? 1 : 0);
			num3 += ((!rule.ExceptIfRecipientAddressContainsWords.IsNullOrEmpty()) ? 1 : 0);
			num3 += ((!rule.ExceptIfAttachmentContainsWords.IsNullOrEmpty()) ? 1 : 0);
			num3 += ((!rule.ExceptIfAttachmentMatchesPatterns.IsNullOrEmpty()) ? 1 : 0);
			num3 += (rule.ExceptIfAttachmentIsUnsupported ? 1 : 0);
			num3 += ((!rule.ExceptIfSubjectOrBodyMatchesPatterns.IsNullOrEmpty()) ? 1 : 0);
			num3 += ((!rule.ExceptIfFromAddressMatchesPatterns.IsNullOrEmpty()) ? 1 : 0);
			num3 += ((!rule.ExceptIfRecipientAddressMatchesPatterns.IsNullOrEmpty()) ? 1 : 0);
			num3 += ((!rule.ExceptIfAttachmentNameMatchesPatterns.IsNullOrEmpty()) ? 1 : 0);
			num3 += (rule.ExceptIfHasNoClassification ? 1 : 0);
			num3 += ((!rule.ExceptIfSubjectContainsWords.IsNullOrEmpty()) ? 1 : 0);
			num3 += ((!rule.ExceptIfSubjectMatchesPatterns.IsNullOrEmpty()) ? 1 : 0);
			num3 += ((!rule.ExceptIfAnyOfToHeader.IsNullOrEmpty()) ? 1 : 0);
			num3 += ((!rule.ExceptIfAnyOfToHeaderMemberOf.IsNullOrEmpty()) ? 1 : 0);
			num3 += ((!rule.ExceptIfAnyOfCcHeader.IsNullOrEmpty()) ? 1 : 0);
			num3 += ((!rule.ExceptIfAnyOfCcHeaderMemberOf.IsNullOrEmpty()) ? 1 : 0);
			num3 += ((!rule.ExceptIfAnyOfToCcHeader.IsNullOrEmpty()) ? 1 : 0);
			num3 += ((!rule.ExceptIfAnyOfToCcHeaderMemberOf.IsNullOrEmpty()) ? 1 : 0);
			num3 += ((rule.ExceptIfMessageTypeMatches != null) ? 1 : 0);
			num3 += ((rule.ExceptIfSenderManagementRelationship != null) ? 1 : 0);
			num3 += ((rule.ExceptIfWithImportance != null) ? 1 : 0);
			num3 += ((!rule.ExceptIfRecipientInSenderList.IsNullOrEmpty()) ? 1 : 0);
			num3 += ((!rule.ExceptIfSenderInRecipientList.IsNullOrEmpty()) ? 1 : 0);
			num3 += ((rule.ExceptIfHeaderContainsMessageHeader != null && !rule.ExceptIfHeaderContainsWords.IsNullOrEmpty()) ? 1 : 0);
			num3 += ((!rule.ExceptIfBetweenMemberOf1.IsNullOrEmpty() && !rule.ExceptIfBetweenMemberOf2.IsNullOrEmpty()) ? 1 : 0);
			num3 += ((rule.ExceptIfHeaderMatchesMessageHeader != null && !rule.ExceptIfHeaderMatchesPatterns.IsNullOrEmpty()) ? 1 : 0);
			num3 += ((rule.ExceptIfManagerForEvaluatedUser != null && !rule.ExceptIfManagerAddresses.IsNullOrEmpty()) ? 1 : 0);
			num3 += ((rule.ExceptIfADComparisonAttribute != null && rule.ExceptIfADComparisonOperator != null) ? 1 : 0);
			num3 += ((rule.ExceptIfAttachmentSizeOver != null) ? 1 : 0);
			num3 += ((rule.ExceptIfSCLOver != null) ? 1 : 0);
			num3 += ((rule.ExceptIfHasClassification != null) ? 1 : 0);
			num3 += ((!rule.ExceptIfSenderADAttributeContainsWords.IsNullOrEmpty()) ? 1 : 0);
			num3 += ((!rule.ExceptIfSenderADAttributeMatchesPatterns.IsNullOrEmpty()) ? 1 : 0);
			num3 += ((!rule.ExceptIfRecipientADAttributeContainsWords.IsNullOrEmpty()) ? 1 : 0);
			num3 += ((!rule.ExceptIfRecipientADAttributeMatchesPatterns.IsNullOrEmpty()) ? 1 : 0);
			num3 += ((rule.ExceptIfMessageSizeOver != null) ? 1 : 0);
			num3 += ((!rule.ExceptIfMessageContainsDataClassifications.IsNullOrEmpty()) ? 1 : 0);
			num3 += ((!rule.ExceptIfAttachmentExtensionMatchesWords.IsNullOrEmpty()) ? 1 : 0);
			num3 += (rule.ExceptIfHasSenderOverride ? 1 : 0);
			num3 += (rule.ExceptIfAttachmentHasExecutableContent ? 1 : 0);
			num3 += ((rule.ExceptIfSenderIpRanges != null) ? 1 : 0);
			num3 += (rule.ExceptIfAttachmentProcessingLimitExceeded ? 1 : 0);
			num3 += ((!rule.ExceptIfSenderDomainIs.IsNullOrEmpty()) ? 1 : 0);
			num3 += ((!rule.ExceptIfRecipientDomainIs.IsNullOrEmpty()) ? 1 : 0);
			num3 += ((!rule.ExceptIfContentCharacterSetContainsWords.IsNullOrEmpty()) ? 1 : 0);
			num3 += (rule.ExceptIfAttachmentIsPasswordProtected ? 1 : 0);
			num3 += ((!rule.ExceptIfAnyOfRecipientAddressMatchesPatterns.IsNullOrEmpty()) ? 1 : 0);
			num3 += ((!rule.ExceptIfAnyOfRecipientAddressContainsWords.IsNullOrEmpty()) ? 1 : 0);
			this.Supported = (num2 > 0 && !rule.Name.StartsWith("__", StringComparison.Ordinal) && num == (rule.Conditions.IsNullOrEmpty() ? 0 : rule.Conditions.Length) && num2 == (rule.Actions.IsNullOrEmpty() ? 0 : rule.Actions.Length) && num3 == (rule.Exceptions.IsNullOrEmpty() ? 0 : rule.Exceptions.Length));
		}

		// Token: 0x06003389 RID: 13193 RVA: 0x000A1C70 File Offset: 0x0009FE70
		public RuleRow(InboxRule rule) : base(((InboxRuleId)rule.Identity).ToIdentity(), rule)
		{
			this.Name = rule.Name;
			this.Priority = rule.Priority;
			this.Enabled = rule.Enabled;
			this.CaptionText = Strings.EditRuleCaption(rule.Name);
			this.Supported = rule.SupportedByTask;
		}

		// Token: 0x0600338A RID: 13194 RVA: 0x000A1CDA File Offset: 0x0009FEDA
		public RuleRow(JournalRuleObject rule) : base(rule)
		{
			this.Name = rule.Name;
			this.Enabled = rule.Enabled;
			this.CaptionText = Strings.EditRuleCaption(rule.Name);
		}

		// Token: 0x0600338B RID: 13195 RVA: 0x000A1D14 File Offset: 0x0009FF14
		public RuleRow(UMCallAnsweringRule rule) : base(((UMCallAnsweringRuleId)rule.Identity).ToIdentity(), rule)
		{
			this.Name = rule.Name;
			this.Enabled = rule.Enabled;
			this.Priority = rule.Priority;
			this.CaptionText = Strings.EditRuleCaption(rule.Name);
			this.Supported = (rule.Validate().Length == 0);
		}

		// Token: 0x17002023 RID: 8227
		// (get) Token: 0x0600338C RID: 13196 RVA: 0x000A1D86 File Offset: 0x0009FF86
		// (set) Token: 0x0600338D RID: 13197 RVA: 0x000A1D8E File Offset: 0x0009FF8E
		[DataMember]
		public string CaptionText { get; protected set; }

		// Token: 0x17002024 RID: 8228
		// (get) Token: 0x0600338E RID: 13198 RVA: 0x000A1D97 File Offset: 0x0009FF97
		// (set) Token: 0x0600338F RID: 13199 RVA: 0x000A1D9F File Offset: 0x0009FF9F
		[DataMember]
		public string Name { get; protected set; }

		// Token: 0x17002025 RID: 8229
		// (get) Token: 0x06003390 RID: 13200 RVA: 0x000A1DA8 File Offset: 0x0009FFA8
		// (set) Token: 0x06003391 RID: 13201 RVA: 0x000A1DB0 File Offset: 0x0009FFB0
		[DataMember]
		public int Priority { get; protected set; }

		// Token: 0x17002026 RID: 8230
		// (get) Token: 0x06003392 RID: 13202 RVA: 0x000A1DB9 File Offset: 0x0009FFB9
		// (set) Token: 0x06003393 RID: 13203 RVA: 0x000A1DC1 File Offset: 0x0009FFC1
		[DataMember]
		public bool Enabled { get; protected set; }

		// Token: 0x17002027 RID: 8231
		// (get) Token: 0x06003394 RID: 13204 RVA: 0x000A1DCA File Offset: 0x0009FFCA
		// (set) Token: 0x06003395 RID: 13205 RVA: 0x000A1DD2 File Offset: 0x0009FFD2
		public string[] ConditionDescriptions { get; protected set; }

		// Token: 0x17002028 RID: 8232
		// (get) Token: 0x06003396 RID: 13206 RVA: 0x000A1DDB File Offset: 0x0009FFDB
		// (set) Token: 0x06003397 RID: 13207 RVA: 0x000A1DE3 File Offset: 0x0009FFE3
		public RuleDescription DescriptionObject
		{
			get
			{
				return this.descriptionObject;
			}
			protected set
			{
				this.descriptionObject = value;
			}
		}

		// Token: 0x17002029 RID: 8233
		// (get) Token: 0x06003398 RID: 13208 RVA: 0x000A1DEC File Offset: 0x0009FFEC
		// (set) Token: 0x06003399 RID: 13209 RVA: 0x000A1DF4 File Offset: 0x0009FFF4
		public string[] ActionDescriptions { get; protected set; }

		// Token: 0x1700202A RID: 8234
		// (get) Token: 0x0600339A RID: 13210 RVA: 0x000A1DFD File Offset: 0x0009FFFD
		// (set) Token: 0x0600339B RID: 13211 RVA: 0x000A1E05 File Offset: 0x000A0005
		public string[] ExceptionDescriptions { get; protected set; }

		// Token: 0x1700202B RID: 8235
		// (get) Token: 0x0600339C RID: 13212 RVA: 0x000A1E0E File Offset: 0x000A000E
		// (set) Token: 0x0600339D RID: 13213 RVA: 0x000A1E16 File Offset: 0x000A0016
		public string ActivationDateDescription { get; protected set; }

		// Token: 0x1700202C RID: 8236
		// (get) Token: 0x0600339E RID: 13214 RVA: 0x000A1E1F File Offset: 0x000A001F
		// (set) Token: 0x0600339F RID: 13215 RVA: 0x000A1E27 File Offset: 0x000A0027
		public string ExpiryDateDescription { get; protected set; }

		// Token: 0x1700202D RID: 8237
		// (get) Token: 0x060033A0 RID: 13216 RVA: 0x000A1E30 File Offset: 0x000A0030
		// (set) Token: 0x060033A1 RID: 13217 RVA: 0x000A1E38 File Offset: 0x000A0038
		[DataMember]
		public bool Supported { get; protected set; }

		// Token: 0x1700202E RID: 8238
		// (get) Token: 0x060033A2 RID: 13218 RVA: 0x000A1E41 File Offset: 0x000A0041
		// (set) Token: 0x060033A3 RID: 13219 RVA: 0x000A1E49 File Offset: 0x000A0049
		[DataMember]
		public string DlpPolicy { get; private set; }

		// Token: 0x1700202F RID: 8239
		// (get) Token: 0x060033A4 RID: 13220 RVA: 0x000A1E52 File Offset: 0x000A0052
		// (set) Token: 0x060033A5 RID: 13221 RVA: 0x000A1E5A File Offset: 0x000A005A
		[DataMember]
		public string RuleMode { get; private set; }

		// Token: 0x17002030 RID: 8240
		// (get) Token: 0x060033A6 RID: 13222 RVA: 0x000A1E63 File Offset: 0x000A0063
		// (set) Token: 0x060033A7 RID: 13223 RVA: 0x000A1E6B File Offset: 0x000A006B
		[DataMember]
		public Version RuleVersion { get; private set; }

		// Token: 0x04002511 RID: 9489
		private RuleDescription descriptionObject;
	}
}
