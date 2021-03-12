using System;
using System.Management.Automation;
using System.Security.Permissions;
using System.ServiceModel.Activation;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Management.Common;
using Microsoft.Exchange.Management.ControlPanel.WebControls;
using Microsoft.Exchange.PowerShell.RbacHostingTools;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x02000405 RID: 1029
	[AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Required)]
	public class InboxRules : RuleDataService, IInboxRules, IDataSourceService<InboxRuleFilter, RuleRow, InboxRule, SetInboxRule, NewInboxRule, RemoveInboxRule>, IEditListService<InboxRuleFilter, RuleRow, InboxRule, NewInboxRule, RemoveInboxRule>, IGetListService<InboxRuleFilter, RuleRow>, INewObjectService<RuleRow, NewInboxRule>, IRemoveObjectsService<RemoveInboxRule>, IEditObjectForListService<InboxRule, SetInboxRule, RuleRow>, IGetObjectService<InboxRule>, IGetObjectForListService<RuleRow>
	{
		// Token: 0x060034B7 RID: 13495 RVA: 0x000A3B45 File Offset: 0x000A1D45
		public InboxRules() : base("InboxRule")
		{
		}

		// Token: 0x060034B8 RID: 13496 RVA: 0x000A3B52 File Offset: 0x000A1D52
		[PrincipalPermission(SecurityAction.Demand, Role = "Get-InboxRule@R:Self")]
		public PowerShellResults<RuleRow> GetList(InboxRuleFilter filter, SortOptions sort)
		{
			return base.GetList<RuleRow, InboxRuleFilter>("Get-InboxRule", filter, sort);
		}

		// Token: 0x060034B9 RID: 13497 RVA: 0x000A3B61 File Offset: 0x000A1D61
		[PrincipalPermission(SecurityAction.Demand, Role = "Remove-InboxRule?Identity@W:Self")]
		public PowerShellResults RemoveObjects(Identity[] identities, RemoveInboxRule parameters)
		{
			parameters = (parameters ?? new RemoveInboxRule());
			return base.Invoke(new PSCommand().AddCommand("Remove-InboxRule"), identities, parameters);
		}

		// Token: 0x060034BA RID: 13498 RVA: 0x000A3B88 File Offset: 0x000A1D88
		[PrincipalPermission(SecurityAction.Demand, Role = "Get-InboxRule?Identity@R:Self")]
		public PowerShellResults<InboxRule> GetObject(Identity identity)
		{
			PSCommand pscommand = new PSCommand().AddCommand("Get-InboxRule");
			pscommand.AddParameter("DescriptionTimeFormat", EcpDateTimeHelper.GetWeekdayDateFormat(true));
			pscommand.AddParameter("DescriptionTimeZone", RbacPrincipal.Current.UserTimeZone);
			return base.GetObject<InboxRule>(pscommand, identity);
		}

		// Token: 0x060034BB RID: 13499 RVA: 0x000A3BD5 File Offset: 0x000A1DD5
		[PrincipalPermission(SecurityAction.Demand, Role = "Get-InboxRule?Identity@R:Self")]
		public PowerShellResults<RuleRow> GetObjectForList(Identity identity)
		{
			return base.GetObjectForList<RuleRow>("Get-InboxRule", identity);
		}

		// Token: 0x060034BC RID: 13500 RVA: 0x000A3BE3 File Offset: 0x000A1DE3
		[PrincipalPermission(SecurityAction.Demand, Role = "New-InboxRule?StopProcessingRules@W:Self")]
		public PowerShellResults<InboxRule> GetMailMessage(NewInboxRule properties)
		{
			return base.NewObject<InboxRule, NewInboxRule>("New-InboxRule", properties);
		}

		// Token: 0x060034BD RID: 13501 RVA: 0x000A3BF4 File Offset: 0x000A1DF4
		[PrincipalPermission(SecurityAction.Demand, Role = "New-InboxRule?StopProcessingRules@W:Self")]
		public PowerShellResults<RuleRow> NewObject(NewInboxRule properties)
		{
			properties.FaultIfNull();
			properties = (NewInboxRule)InboxRules.SanitizeIdentityParameter(properties);
			properties.Name = base.GetUniqueRuleName(properties.Name, this.GetList(null, null).Output);
			PowerShellResults<RuleRow> powerShellResults = base.NewObject<RuleRow, NewInboxRule>("New-InboxRule", properties);
			powerShellResults.Output = null;
			return powerShellResults;
		}

		// Token: 0x060034BE RID: 13502 RVA: 0x000A3C54 File Offset: 0x000A1E54
		private static InboxRuleParameters SanitizeIdentityParameter(InboxRuleParameters properties)
		{
			PeopleIdentity[] array = new PeopleIdentity[0];
			Action<PeopleIdentity> action = delegate(PeopleIdentity peopleIdentity)
			{
				peopleIdentity.IgnoreDisplayNameInIdentity = true;
			};
			Array.ForEach<PeopleIdentity>(properties.From ?? array, action);
			Array.ForEach<PeopleIdentity>(properties.SentTo ?? array, action);
			Array.ForEach<PeopleIdentity>(properties.ForwardTo ?? array, action);
			Array.ForEach<PeopleIdentity>(properties.RedirectTo ?? array, action);
			Array.ForEach<PeopleIdentity>(properties.ForwardAsAttachmentTo ?? array, action);
			Array.ForEach<PeopleIdentity>(properties.ExceptIfFrom ?? array, action);
			Array.ForEach<PeopleIdentity>(properties.ExceptIfSentTo ?? array, action);
			return properties;
		}

		// Token: 0x060034BF RID: 13503 RVA: 0x000A3D00 File Offset: 0x000A1F00
		[PrincipalPermission(SecurityAction.Demand, Role = "Get-InboxRule?Identity@R:Self+Set-InboxRule?Identity@W:Self")]
		public PowerShellResults<RuleRow> SetObject(Identity identity, SetInboxRule properties)
		{
			properties.FaultIfNull();
			properties = (SetInboxRule)InboxRules.SanitizeIdentityParameter(properties);
			if (properties.Name != null)
			{
				properties.Name = base.GetUniqueRuleName(properties.Name, this.GetList(null, null).Output);
			}
			return base.SetObject<InboxRule, SetInboxRule, RuleRow>("Set-InboxRule", identity, properties);
		}

		// Token: 0x060034C0 RID: 13504 RVA: 0x000A3D54 File Offset: 0x000A1F54
		[PrincipalPermission(SecurityAction.Demand, Role = "Disable-InboxRule?Identity@W:Self")]
		public PowerShellResults<RuleRow> DisableRule(Identity[] identities, DisableInboxRule parameters)
		{
			parameters = (parameters ?? new DisableInboxRule());
			return base.InvokeAndGetObject<RuleRow>(new PSCommand().AddCommand("Disable-InboxRule"), identities, parameters);
		}

		// Token: 0x060034C1 RID: 13505 RVA: 0x000A3D79 File Offset: 0x000A1F79
		[PrincipalPermission(SecurityAction.Demand, Role = "Enable-InboxRule?Identity@W:Self")]
		public PowerShellResults<RuleRow> EnableRule(Identity[] identities, EnableInboxRule parameters)
		{
			parameters = (parameters ?? new EnableInboxRule());
			return base.InvokeAndGetObject<RuleRow>(new PSCommand().AddCommand("Enable-InboxRule"), identities, parameters);
		}

		// Token: 0x060034C2 RID: 13506 RVA: 0x000A3D9E File Offset: 0x000A1F9E
		[PrincipalPermission(SecurityAction.Demand, Role = "Get-InboxRule?Identity@R:Self+Set-InboxRule?Identity&Priority@W:Self")]
		public PowerShellResults IncreasePriority(Identity[] identities, ChangeInboxRule parameters)
		{
			parameters = (parameters ?? new ChangeInboxRule());
			return base.ChangePriority<InboxRule>(identities, -1, parameters);
		}

		// Token: 0x060034C3 RID: 13507 RVA: 0x000A3DB5 File Offset: 0x000A1FB5
		[PrincipalPermission(SecurityAction.Demand, Role = "Get-InboxRule?Identity@R:Self+Set-InboxRule?Identity&Priority@W:Self")]
		public PowerShellResults DecreasePriority(Identity[] identities, ChangeInboxRule parameters)
		{
			parameters = (parameters ?? new ChangeInboxRule());
			return base.ChangePriority<InboxRule>(identities, 1, parameters);
		}

		// Token: 0x170020B4 RID: 8372
		// (get) Token: 0x060034C4 RID: 13508 RVA: 0x000A3DCC File Offset: 0x000A1FCC
		public override int RuleNameMaxLength
		{
			get
			{
				return InboxRules.ruleNameMaxLength;
			}
		}

		// Token: 0x170020B5 RID: 8373
		// (get) Token: 0x060034C5 RID: 13509 RVA: 0x000A3DD3 File Offset: 0x000A1FD3
		public override RulePhrase[] SupportedConditions
		{
			get
			{
				return InboxRules.supportedConditions;
			}
		}

		// Token: 0x170020B6 RID: 8374
		// (get) Token: 0x060034C6 RID: 13510 RVA: 0x000A3DDA File Offset: 0x000A1FDA
		public override RulePhrase[] SupportedActions
		{
			get
			{
				return InboxRules.supportedActions;
			}
		}

		// Token: 0x170020B7 RID: 8375
		// (get) Token: 0x060034C7 RID: 13511 RVA: 0x000A3DE1 File Offset: 0x000A1FE1
		public override RulePhrase[] SupportedExceptions
		{
			get
			{
				return InboxRules.supportedExceptions;
			}
		}

		// Token: 0x0400252A RID: 9514
		internal const string ReadScope = "@R:Self";

		// Token: 0x0400252B RID: 9515
		internal const string WriteScope = "@W:Self";

		// Token: 0x0400252C RID: 9516
		internal const string NewInboxRule = "New-InboxRule";

		// Token: 0x0400252D RID: 9517
		internal const string GetInboxRule = "Get-InboxRule";

		// Token: 0x0400252E RID: 9518
		internal const string SetInboxRule = "Set-InboxRule";

		// Token: 0x0400252F RID: 9519
		internal const string RemoveInboxRule = "Remove-InboxRule";

		// Token: 0x04002530 RID: 9520
		internal const string DisableInboxRule = "Disable-InboxRule";

		// Token: 0x04002531 RID: 9521
		internal const string EnableInboxRule = "Enable-InboxRule";

		// Token: 0x04002532 RID: 9522
		private const string PeoplePickerRole = "Get-Recipient";

		// Token: 0x04002533 RID: 9523
		internal const string AlwaysDeleteOutlookRulesBlob = "AlwaysDeleteOutlookRulesBlob";

		// Token: 0x04002534 RID: 9524
		internal const string GetListRole = "Get-InboxRule@R:Self";

		// Token: 0x04002535 RID: 9525
		internal const string RemoveObjectsRole = "Remove-InboxRule?Identity@W:Self";

		// Token: 0x04002536 RID: 9526
		internal const string GetObjectRole = "Get-InboxRule?Identity@R:Self";

		// Token: 0x04002537 RID: 9527
		internal const string NewObjectRole = "New-InboxRule?StopProcessingRules@W:Self";

		// Token: 0x04002538 RID: 9528
		internal const string SetObjectRole = "Get-InboxRule?Identity@R:Self+Set-InboxRule?Identity@W:Self";

		// Token: 0x04002539 RID: 9529
		internal const string DisableRuleRole = "Disable-InboxRule?Identity@W:Self";

		// Token: 0x0400253A RID: 9530
		internal const string EnableRuleRole = "Enable-InboxRule?Identity@W:Self";

		// Token: 0x0400253B RID: 9531
		internal const string ChangePriorityRole = "Get-InboxRule?Identity@R:Self+Set-InboxRule?Identity&Priority@W:Self";

		// Token: 0x0400253C RID: 9532
		private static int ruleNameMaxLength = Util.GetMaxLengthFromDefinition(InboxRuleSchema.Name);

		// Token: 0x0400253D RID: 9533
		private static RulePhrase[] supportedConditions = new RulePhrase[]
		{
			new RuleCondition("From", OwaOptionStrings.InboxRuleFromConditionText, new FormletParameter[]
			{
				new PeopleParameter("From", PickerType.PickFrom)
			}, "Get-Recipient", OwaOptionStrings.FromConditionFormat, OwaOptionStrings.InboxRuleSentOrReceivedGroupText, OwaOptionStrings.InboxRuleFromConditionFlyOutText, OwaOptionStrings.InboxRuleFromConditionPreCannedText, true),
			new RuleCondition("SentTo", OwaOptionStrings.InboxRuleSentToConditionText, new FormletParameter[]
			{
				new PeopleParameter("SentTo", PickerType.PickTo)
			}, "Get-Recipient", OwaOptionStrings.SentToConditionFormat, OwaOptionStrings.InboxRuleSentOrReceivedGroupText, OwaOptionStrings.InboxRuleSentToConditionFlyOutText, OwaOptionStrings.InboxRuleSentToConditionPreCannedText, true),
			new RuleCondition("FromSubscription", OwaOptionStrings.InboxRuleFromSubscriptionConditionText, new FormletParameter[]
			{
				new EnhancedEnumParameter("FromSubscription", OwaOptionStrings.SubscriptionDialogTitle, OwaOptionStrings.SubscriptionDialogLabel, "RulesEditor/SubscriptionItems.svc", OwaOptionStrings.NoSubscriptionAvailable, null)
			}, "MultiTenant+Get-Subscription@R:Self", OwaOptionStrings.FromSubscriptionConditionFormat, OwaOptionStrings.InboxRuleSentOrReceivedGroupText, OwaOptionStrings.InboxRuleFromSubscriptionConditionFlyOutText, LocalizedString.Empty, false),
			new RuleCondition("SubjectContains", OwaOptionStrings.InboxRuleSubjectContainsConditionText, new FormletParameter[]
			{
				new StringArrayParameter("SubjectContainsWords", OwaOptionStrings.StringArrayDialogTitle, OwaOptionStrings.StringArrayDialogLabel)
			}, null, OwaOptionStrings.SubjectContainsConditionFormat, OwaOptionStrings.InboxRuleIncludeTheseWordsGroupText, OwaOptionStrings.InboxRuleSubjectContainsConditionFlyOutText, OwaOptionStrings.InboxRuleSubjectContainsConditionPreCannedText, true),
			new RuleCondition("SubjectOrBodyContains", OwaOptionStrings.InboxRuleSubjectOrBodyContainsConditionText, new FormletParameter[]
			{
				new StringArrayParameter("SubjectOrBodyContainsWords", OwaOptionStrings.StringArrayDialogTitle, OwaOptionStrings.StringArrayDialogLabel)
			}, null, OwaOptionStrings.SubjectOrBodyContainsConditionFormat, OwaOptionStrings.InboxRuleIncludeTheseWordsGroupText, OwaOptionStrings.InboxRuleSubjectOrBodyContainsConditionFlyOutText, LocalizedString.Empty, true),
			new RuleCondition("FromAddressContains", OwaOptionStrings.InboxRuleFromAddressContainsConditionText, new FormletParameter[]
			{
				new StringArrayParameter("FromAddressContainsWords", OwaOptionStrings.StringArrayDialogTitle, OwaOptionStrings.StringArrayDialogLabel)
			}, null, OwaOptionStrings.FromAddressContainsConditionFormat, OwaOptionStrings.InboxRuleIncludeTheseWordsGroupText, OwaOptionStrings.InboxRuleFromAddressContainsConditionFlyOutText, LocalizedString.Empty, true),
			new RuleCondition("MyNameInToOrCcBox", OwaOptionStrings.InboxRuleMyNameInToCcBoxConditionText, new FormletParameter[]
			{
				new BooleanParameter("MyNameInToOrCcBox")
			}, null, OwaOptionStrings.InboxRuleMyNameInToCcBoxConditionText, OwaOptionStrings.InboxRuleMyNameIsGroupText, OwaOptionStrings.InboxRuleMyNameInToCcBoxConditionFlyOutText, LocalizedString.Empty, true),
			new RuleCondition("SentOnlyToMe", OwaOptionStrings.InboxRuleSentOnlyToMeConditionText, new FormletParameter[]
			{
				new BooleanParameter("SentOnlyToMe")
			}, null, OwaOptionStrings.SentOnlyToMeConditionFormat, OwaOptionStrings.InboxRuleMyNameIsGroupText, OwaOptionStrings.InboxRuleSentOnlyToMeConditionFlyOutText, LocalizedString.Empty, false),
			new RuleCondition("MyNameInToBox", OwaOptionStrings.InboxRuleMyNameInToBoxConditionText, new FormletParameter[]
			{
				new BooleanParameter("MyNameInToBox")
			}, null, OwaOptionStrings.InboxRuleMyNameInToBoxConditionText, OwaOptionStrings.InboxRuleMyNameIsGroupText, OwaOptionStrings.InboxRuleMyNameInToBoxConditionFlyOutText, LocalizedString.Empty, false),
			new RuleCondition("MyNameInCcBox", OwaOptionStrings.InboxRuleMyNameInCcBoxConditionText, new FormletParameter[]
			{
				new BooleanParameter("MyNameInCcBox")
			}, null, OwaOptionStrings.InboxRuleMyNameInCcBoxConditionText, OwaOptionStrings.InboxRuleMyNameIsGroupText, OwaOptionStrings.InboxRuleMyNameInCcBoxConditionFlyOutText, LocalizedString.Empty, false),
			new RuleCondition("MyNameNotInToBox", OwaOptionStrings.InboxRuleMyNameNotInToBoxConditionText, new FormletParameter[]
			{
				new BooleanParameter("MyNameNotInToBox")
			}, null, OwaOptionStrings.InboxRuleMyNameNotInToBoxConditionText, OwaOptionStrings.InboxRuleMyNameIsGroupText, OwaOptionStrings.InboxRuleMyNameNotInToBoxConditionFlyOutText, LocalizedString.Empty, false),
			new RuleCondition("BodyContains", OwaOptionStrings.InboxRuleBodyContainsConditionText, new FormletParameter[]
			{
				new StringArrayParameter("BodyContainsWords", OwaOptionStrings.StringArrayDialogTitle, OwaOptionStrings.StringArrayDialogLabel)
			}, null, OwaOptionStrings.BodyContainsConditionFormat, OwaOptionStrings.InboxRuleIncludeTheseWordsGroupText, OwaOptionStrings.InboxRuleBodyContainsConditionFlyOutText, LocalizedString.Empty, false),
			new RuleCondition("RecipientAddressContains", OwaOptionStrings.InboxRuleRecipientAddressContainsConditionText, new FormletParameter[]
			{
				new StringArrayParameter("RecipientAddressContainsWords", OwaOptionStrings.StringArrayDialogTitle, OwaOptionStrings.StringArrayDialogLabel)
			}, null, OwaOptionStrings.RecipientAddressContainsConditionFormat, OwaOptionStrings.InboxRuleIncludeTheseWordsGroupText, OwaOptionStrings.InboxRuleRecipientAddressContainsConditionFlyOutText, LocalizedString.Empty, false),
			new RuleCondition("HeaderContains", OwaOptionStrings.InboxRuleHeaderContainsConditionText, new FormletParameter[]
			{
				new StringArrayParameter("HeaderContainsWords", OwaOptionStrings.StringArrayDialogTitle, OwaOptionStrings.StringArrayDialogLabel)
			}, null, OwaOptionStrings.HeaderContainsConditionFormat, OwaOptionStrings.InboxRuleIncludeTheseWordsGroupText, OwaOptionStrings.InboxRuleHeaderContainsConditionFlyOutText, LocalizedString.Empty, false),
			new RuleCondition("WithImportance", OwaOptionStrings.InboxRuleWithImportanceConditionText, new FormletParameter[]
			{
				new EnumParameter("WithImportance", OwaOptionStrings.ImportanceDialogTitle, OwaOptionStrings.ImportanceDialogLabel, typeof(Importance), null)
			}, null, OwaOptionStrings.WithImportanceConditionFormat, OwaOptionStrings.InboxRuleMarkedWithGroupText, OwaOptionStrings.InboxRuleWithImportanceConditionFlyOutText, LocalizedString.Empty, false),
			new RuleCondition("WithSensitivity", OwaOptionStrings.InboxRuleWithSensitivityConditionText, new FormletParameter[]
			{
				new EnumParameter("WithSensitivity", OwaOptionStrings.SensitivityDialogTitle, OwaOptionStrings.SensitivityDialogLabel, typeof(Sensitivity), null)
			}, null, OwaOptionStrings.WithSensitivityConditionFormat, OwaOptionStrings.InboxRuleMarkedWithGroupText, OwaOptionStrings.InboxRuleWithSensitivityConditionFlyOutText, LocalizedString.Empty, false),
			new RuleCondition("HasAttachment", OwaOptionStrings.InboxRuleHasAttachmentConditionText, new FormletParameter[]
			{
				new BooleanParameter("HasAttachment")
			}, null, OwaOptionStrings.HasAttachmentConditionFormat, OwaOptionStrings.InboxRuleItIsGroupText, OwaOptionStrings.InboxRuleHasAttachmentConditionFlyOutText, LocalizedString.Empty, false),
			new RuleCondition("MessageTypeMatches", OwaOptionStrings.InboxRuleMessageTypeMatchesConditionText, new FormletParameter[]
			{
				new EnumParameter("MessageTypeMatches", OwaOptionStrings.MessageTypeDialogTitle, OwaOptionStrings.MessageTypeDialogLabel, typeof(InboxRuleMessageType), null)
			}, null, OwaOptionStrings.MessageTypeMatchesConditionFormat, OwaOptionStrings.InboxRuleItIsGroupText, OwaOptionStrings.InboxRuleMessageTypeMatchesConditionFlyOutText, LocalizedString.Empty, false),
			new RuleCondition("HasClassification", OwaOptionStrings.InboxRuleHasClassificationConditionText, new FormletParameter[]
			{
				new EnhancedEnumParameter("HasClassification", OwaOptionStrings.ClassificationDialogTitle, OwaOptionStrings.ClassificationDialogLabel, "RulesEditor/MessageClassifications.svc", OwaOptionStrings.NoMessageClassificationAvailable, null)
			}, "Get-MessageClassification@R:Self", OwaOptionStrings.HasClassificationConditionFormat, OwaOptionStrings.InboxRuleItIsGroupText, OwaOptionStrings.InboxRuleHasClassificationConditionFlyOutText, LocalizedString.Empty, false),
			new RuleCondition("FlaggedForAction", OwaOptionStrings.InboxRuleFlaggedForActionConditionText, new FormletParameter[]
			{
				new EnumParameter("FlaggedForAction", OwaOptionStrings.FlagStatusDialogTitle, OwaOptionStrings.FlagStatusDialogLabel, typeof(RequestedAction), null, true)
			}, null, OwaOptionStrings.FlaggedForActionConditionFormat, OwaOptionStrings.InboxRuleItIsGroupText, OwaOptionStrings.InboxRuleFlaggedForActionConditionFlyOutText, LocalizedString.Empty, false),
			new RuleCondition("WithinSizeRange", OwaOptionStrings.InboxRuleWithinSizeRangeConditionText, new FormletParameter[]
			{
				new NumberRangeParameter("WithinSizeRange", new string[]
				{
					"WithinSizeRangeMaximum",
					"WithinSizeRangeMinimum"
				}, OwaOptionStrings.WithinSizeRangeDialogTitle, OwaOptionStrings.AtMostOnlyDisplayTemplate, OwaOptionStrings.AtLeastAtMostDisplayTemplate)
			}, null, OwaOptionStrings.WithinSizeRangeConditionFormat, false),
			new RuleCondition("WithinDateRange", OwaOptionStrings.InboxRuleWithinDateRangeConditionText, new FormletParameter[]
			{
				new DateRangeParameter("WithinDateRange", new string[]
				{
					"ReceivedBeforeDate",
					"ReceivedAfterDate"
				}, OwaOptionStrings.WithinDateRangeDialogTitle, OwaOptionStrings.BeforeDateDisplayTemplate, OwaOptionStrings.AfterDateDisplayTemplate)
			}, null, OwaOptionStrings.WithinDateRangeConditionFormat, false)
		};

		// Token: 0x0400253E RID: 9534
		private static RulePhrase[] supportedActions = new RulePhrase[]
		{
			new RulePhrase("MoveToFolder", OwaOptionStrings.InboxRuleMoveToFolderActionText, new FormletParameter[]
			{
				new FolderParameter("MoveToFolder", OwaOptionStrings.MailboxFolderDialogTitle, OwaOptionStrings.MailboxFolderDialogLabel)
			}, "Get-MailboxFolder?Recurse&MailFolderOnly&ResultSize@R:Self", OwaOptionStrings.InboxRuleMoveCopyDeleteGroupText, OwaOptionStrings.InboxRuleMoveToFolderActionFlyOutText, true, true),
			new RulePhrase("ApplyCategory", OwaOptionStrings.InboxRuleApplyCategoryActionText, new FormletParameter[]
			{
				new EnhancedEnumParameter("ApplyCategory", OwaOptionStrings.CategoryDialogTitle, OwaOptionStrings.CategoryDialogLabel, "RulesEditor/MessageCategories.svc", OwaOptionStrings.NoMessageCategoryAvailable, null)
			}, null, OwaOptionStrings.InboxRuleMarkMessageGroupText, OwaOptionStrings.InboxRuleApplyCategoryActionFlyOutText, true, true),
			new RulePhrase("RedirectTo", OwaOptionStrings.InboxRuleRedirectToActionText, new FormletParameter[]
			{
				new PeopleParameter("RedirectTo", PickerType.PickTo)
				{
					UseAndDelimiter = true
				}
			}, "Get-Recipient", OwaOptionStrings.InboxRuleForwardRedirectGroupText, OwaOptionStrings.InboxRuleRedirectToActionFlyOutText, true, true),
			new RulePhrase("DeleteMessage", OwaOptionStrings.InboxRuleDeleteMessageActionText, new FormletParameter[]
			{
				new BooleanParameter("DeleteMessage")
			}, null, OwaOptionStrings.InboxRuleMoveCopyDeleteGroupText, OwaOptionStrings.InboxRuleDeleteMessageActionFlyOutText, true, true),
			new RulePhrase("SendTextMessageNotificationTo", OwaOptionStrings.InboxRuleSendTextMessageNotificationToActionText, new FormletParameter[]
			{
				new NotificationPhoneNumberParameter("SendTextMessageNotificationTo")
			}, "MachineToPersonTextingOnly", OwaOptionStrings.InboxRuleForwardRedirectGroupText, OwaOptionStrings.InboxRuleSendTextMessageNotificationToActionFlyOutText, true, false),
			new RulePhrase("CopyToFolder", OwaOptionStrings.InboxRuleCopyToFolderActionText, new FormletParameter[]
			{
				new FolderParameter("CopyToFolder", OwaOptionStrings.MailboxFolderDialogTitle, OwaOptionStrings.MailboxFolderDialogLabel)
			}, "Get-MailboxFolder?Recurse&MailFolderOnly&ResultSize@R:Self", OwaOptionStrings.InboxRuleMoveCopyDeleteGroupText, OwaOptionStrings.InboxRuleCopyToFolderActionFlyOutText, false, true),
			new RulePhrase("MarkAsRead", OwaOptionStrings.InboxRuleMarkAsReadActionText, new FormletParameter[]
			{
				new BooleanParameter("MarkAsRead")
			}, null, OwaOptionStrings.InboxRuleMarkMessageGroupText, OwaOptionStrings.InboxRuleMarkAsReadActionFlyOutText, false, true),
			new RulePhrase("ForwardTo", OwaOptionStrings.InboxRuleForwardToActionText, new FormletParameter[]
			{
				new PeopleParameter("ForwardTo", PickerType.PickTo)
				{
					UseAndDelimiter = true
				}
			}, "Get-Recipient", OwaOptionStrings.InboxRuleForwardRedirectGroupText, OwaOptionStrings.InboxRuleForwardToActionFlyOutText, false, true),
			new RulePhrase("ForwardAsAttachmentTo", OwaOptionStrings.InboxRuleForwardAsAttachmentToActionText, new FormletParameter[]
			{
				new PeopleParameter("ForwardAsAttachmentTo", PickerType.PickTo)
				{
					UseAndDelimiter = true
				}
			}, "Get-Recipient", OwaOptionStrings.InboxRuleForwardRedirectGroupText, OwaOptionStrings.InboxRuleForwardAsAttachmentToActionFlyOutText, false, true),
			new RulePhrase("MarkImportance", OwaOptionStrings.InboxRuleMarkImportanceActionText, new FormletParameter[]
			{
				new EnumParameter("MarkImportance", OwaOptionStrings.ImportanceDialogTitle, OwaOptionStrings.ImportanceDialogLabel, typeof(Importance), null)
			}, null, OwaOptionStrings.InboxRuleMarkMessageGroupText, OwaOptionStrings.InboxRuleMarkImportanceActionFlyOutText, false, true)
		};

		// Token: 0x0400253F RID: 9535
		private static RulePhrase[] supportedExceptions = new RulePhrase[]
		{
			new RulePhrase("ExceptIfFrom", OwaOptionStrings.InboxRuleFromConditionText, new FormletParameter[]
			{
				new PeopleParameter("ExceptIfFrom", PickerType.PickFrom)
			}, "Get-Recipient", OwaOptionStrings.InboxRuleSentOrReceivedGroupText, OwaOptionStrings.InboxRuleFromConditionFlyOutText, false),
			new RulePhrase("ExceptIfSentTo", OwaOptionStrings.InboxRuleSentToConditionText, new FormletParameter[]
			{
				new PeopleParameter("ExceptIfSentTo", PickerType.PickTo)
			}, "Get-Recipient", OwaOptionStrings.InboxRuleSentOrReceivedGroupText, OwaOptionStrings.InboxRuleSentToConditionFlyOutText, false),
			new RulePhrase("ExceptIfFromSubscription", OwaOptionStrings.InboxRuleFromSubscriptionConditionText, new FormletParameter[]
			{
				new EnhancedEnumParameter("ExceptIfFromSubscription", OwaOptionStrings.SubscriptionDialogTitle, OwaOptionStrings.SubscriptionDialogLabel, "RulesEditor/SubscriptionItems.svc", OwaOptionStrings.NoSubscriptionAvailable, null)
			}, "MultiTenant+Get-Subscription@R:Self", OwaOptionStrings.InboxRuleSentOrReceivedGroupText, OwaOptionStrings.InboxRuleFromSubscriptionConditionFlyOutText, false),
			new RulePhrase("ExceptIfSubjectContains", OwaOptionStrings.InboxRuleSubjectContainsConditionText, new FormletParameter[]
			{
				new StringArrayParameter("ExceptIfSubjectContainsWords", OwaOptionStrings.StringArrayDialogTitle, OwaOptionStrings.StringArrayDialogLabel)
			}, null, OwaOptionStrings.InboxRuleIncludeTheseWordsGroupText, OwaOptionStrings.InboxRuleSubjectContainsConditionFlyOutText, false),
			new RulePhrase("ExceptIfSubjectOrBodyContains", OwaOptionStrings.InboxRuleSubjectOrBodyContainsConditionText, new FormletParameter[]
			{
				new StringArrayParameter("ExceptIfSubjectOrBodyContainsWords", OwaOptionStrings.StringArrayDialogTitle, OwaOptionStrings.StringArrayDialogLabel)
			}, null, OwaOptionStrings.InboxRuleIncludeTheseWordsGroupText, OwaOptionStrings.InboxRuleSubjectOrBodyContainsConditionFlyOutText, false),
			new RulePhrase("ExceptIfFromAddressContains", OwaOptionStrings.InboxRuleFromAddressContainsConditionText, new FormletParameter[]
			{
				new StringArrayParameter("ExceptIfFromAddressContainsWords", OwaOptionStrings.StringArrayDialogTitle, OwaOptionStrings.StringArrayDialogLabel)
			}, null, OwaOptionStrings.InboxRuleIncludeTheseWordsGroupText, OwaOptionStrings.InboxRuleFromAddressContainsConditionFlyOutText, false),
			new RulePhrase("ExceptIfMyNameInToOrCcBox", OwaOptionStrings.InboxRuleMyNameInToCcBoxConditionText, new FormletParameter[]
			{
				new BooleanParameter("ExceptIfMyNameInToOrCcBox")
			}, null, OwaOptionStrings.InboxRuleMyNameIsGroupText, OwaOptionStrings.InboxRuleMyNameInToCcBoxConditionFlyOutText, false),
			new RulePhrase("ExceptIfSentOnlyToMe", OwaOptionStrings.InboxRuleSentOnlyToMeConditionText, new FormletParameter[]
			{
				new BooleanParameter("ExceptIfSentOnlyToMe")
			}, null, OwaOptionStrings.InboxRuleMyNameIsGroupText, OwaOptionStrings.InboxRuleSentOnlyToMeConditionFlyOutText, false),
			new RulePhrase("ExceptIfMyNameInToBox", OwaOptionStrings.InboxRuleMyNameInToBoxConditionText, new FormletParameter[]
			{
				new BooleanParameter("ExceptIfMyNameInToBox")
			}, null, OwaOptionStrings.InboxRuleMyNameIsGroupText, OwaOptionStrings.InboxRuleMyNameInToBoxConditionFlyOutText, false),
			new RulePhrase("ExceptIfMyNameInCcBox", OwaOptionStrings.InboxRuleMyNameInCcBoxConditionText, new FormletParameter[]
			{
				new BooleanParameter("ExceptIfMyNameInCcBox")
			}, null, OwaOptionStrings.InboxRuleMyNameIsGroupText, OwaOptionStrings.InboxRuleMyNameInCcBoxConditionFlyOutText, false),
			new RulePhrase("ExceptIfMyNameNotInToBox", OwaOptionStrings.InboxRuleMyNameNotInToBoxConditionText, new FormletParameter[]
			{
				new BooleanParameter("ExceptIfMyNameNotInToBox")
			}, null, OwaOptionStrings.InboxRuleMyNameIsGroupText, OwaOptionStrings.InboxRuleMyNameNotInToBoxConditionFlyOutText, false),
			new RulePhrase("ExceptIfBodyContains", OwaOptionStrings.InboxRuleBodyContainsConditionText, new FormletParameter[]
			{
				new StringArrayParameter("ExceptIfBodyContainsWords", OwaOptionStrings.StringArrayDialogTitle, OwaOptionStrings.StringArrayDialogLabel)
			}, null, OwaOptionStrings.InboxRuleIncludeTheseWordsGroupText, OwaOptionStrings.InboxRuleBodyContainsConditionFlyOutText, false),
			new RulePhrase("ExceptIfRecipientAddressContains", OwaOptionStrings.InboxRuleRecipientAddressContainsConditionText, new FormletParameter[]
			{
				new StringArrayParameter("ExceptIfRecipientAddressContainsWords", OwaOptionStrings.StringArrayDialogTitle, OwaOptionStrings.StringArrayDialogLabel)
			}, null, OwaOptionStrings.InboxRuleIncludeTheseWordsGroupText, OwaOptionStrings.InboxRuleRecipientAddressContainsConditionFlyOutText, false),
			new RulePhrase("ExceptIfHeaderContains", OwaOptionStrings.InboxRuleHeaderContainsConditionText, new FormletParameter[]
			{
				new StringArrayParameter("ExceptIfHeaderContainsWords", OwaOptionStrings.StringArrayDialogTitle, OwaOptionStrings.StringArrayDialogLabel)
			}, null, OwaOptionStrings.InboxRuleIncludeTheseWordsGroupText, OwaOptionStrings.InboxRuleHeaderContainsConditionFlyOutText, false),
			new RulePhrase("ExceptIfWithImportance", OwaOptionStrings.InboxRuleWithImportanceConditionText, new FormletParameter[]
			{
				new EnumParameter("ExceptIfWithImportance", OwaOptionStrings.ImportanceDialogTitle, OwaOptionStrings.ImportanceDialogLabel, typeof(Importance), null)
			}, null, OwaOptionStrings.InboxRuleMarkedWithGroupText, OwaOptionStrings.InboxRuleWithImportanceConditionFlyOutText, false),
			new RulePhrase("ExceptIfWithSensitivity", OwaOptionStrings.InboxRuleWithSensitivityConditionText, new FormletParameter[]
			{
				new EnumParameter("ExceptIfWithSensitivity", OwaOptionStrings.SensitivityDialogTitle, OwaOptionStrings.SensitivityDialogLabel, typeof(Sensitivity), null)
			}, null, OwaOptionStrings.InboxRuleMarkedWithGroupText, OwaOptionStrings.InboxRuleWithSensitivityConditionFlyOutText, false),
			new RulePhrase("ExceptIfHasAttachment", OwaOptionStrings.InboxRuleHasAttachmentConditionText, new FormletParameter[]
			{
				new BooleanParameter("ExceptIfHasAttachment")
			}, null, OwaOptionStrings.InboxRuleItIsGroupText, OwaOptionStrings.InboxRuleHasAttachmentConditionFlyOutText, false),
			new RulePhrase("ExceptIfMessageTypeMatches", OwaOptionStrings.InboxRuleMessageTypeMatchesConditionText, new FormletParameter[]
			{
				new EnumParameter("ExceptIfMessageTypeMatches", OwaOptionStrings.MessageTypeDialogTitle, OwaOptionStrings.MessageTypeDialogLabel, typeof(InboxRuleMessageType), null)
			}, null, OwaOptionStrings.InboxRuleItIsGroupText, OwaOptionStrings.InboxRuleMessageTypeMatchesConditionFlyOutText, false),
			new RulePhrase("ExceptIfHasClassification", OwaOptionStrings.InboxRuleHasClassificationConditionText, new FormletParameter[]
			{
				new EnhancedEnumParameter("ExceptIfHasClassification", OwaOptionStrings.ClassificationDialogTitle, OwaOptionStrings.ClassificationDialogLabel, "RulesEditor/MessageClassifications.svc", OwaOptionStrings.NoMessageClassificationAvailable, null)
			}, "Get-MessageClassification@R:Self", OwaOptionStrings.InboxRuleItIsGroupText, OwaOptionStrings.InboxRuleHasClassificationConditionFlyOutText, false),
			new RulePhrase("ExceptIfFlaggedForAction", OwaOptionStrings.InboxRuleFlaggedForActionConditionText, new FormletParameter[]
			{
				new EnumParameter("ExceptIfFlaggedForAction", OwaOptionStrings.FlagStatusDialogTitle, OwaOptionStrings.FlagStatusDialogLabel, typeof(RequestedAction), null, true)
			}, null, OwaOptionStrings.InboxRuleItIsGroupText, OwaOptionStrings.InboxRuleFlaggedForActionConditionFlyOutText, false),
			new RulePhrase("ExceptIfWithinSizeRange", OwaOptionStrings.InboxRuleWithinSizeRangeConditionText, new FormletParameter[]
			{
				new NumberRangeParameter("ExceptIfWithinSizeRange", new string[]
				{
					"WithinSizeRangeMaximum",
					"WithinSizeRangeMinimum"
				}, OwaOptionStrings.WithinSizeRangeDialogTitle, OwaOptionStrings.AtMostOnlyDisplayTemplate, OwaOptionStrings.AtLeastAtMostDisplayTemplate)
			}, null, false),
			new RulePhrase("ExceptIfWithinDateRange", OwaOptionStrings.InboxRuleWithinDateRangeConditionText, new FormletParameter[]
			{
				new DateRangeParameter("ExceptIfWithinDateRange", new string[]
				{
					"ReceivedBeforeDate",
					"ReceivedAfterDate"
				}, OwaOptionStrings.WithinDateRangeDialogTitle, OwaOptionStrings.BeforeDateDisplayTemplate, OwaOptionStrings.AfterDateDisplayTemplate)
			}, null, false)
		};
	}
}
