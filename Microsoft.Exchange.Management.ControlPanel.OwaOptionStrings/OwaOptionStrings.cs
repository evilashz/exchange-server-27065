using System;
using System.Collections.Generic;
using System.Reflection;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x02000002 RID: 2
	public static class OwaOptionStrings
	{
		// Token: 0x06000001 RID: 1 RVA: 0x000020D0 File Offset: 0x000002D0
		static OwaOptionStrings()
		{
			OwaOptionStrings.stringIDs.Add(4145884488U, "NeverSyncText");
			OwaOptionStrings.stringIDs.Add(1481793251U, "FromAddressContainsConditionFormat");
			OwaOptionStrings.stringIDs.Add(2278445393U, "CalendarPublishingBasic");
			OwaOptionStrings.stringIDs.Add(1471007325U, "ChangePhoneNumber");
			OwaOptionStrings.stringIDs.Add(566587615U, "TimeZoneNote");
			OwaOptionStrings.stringIDs.Add(2785077264U, "ShowWorkWeekAsCheckBoxText");
			OwaOptionStrings.stringIDs.Add(85271849U, "ViewInboxRule");
			OwaOptionStrings.stringIDs.Add(3270571164U, "DeviceMobileOperatorLabel");
			OwaOptionStrings.stringIDs.Add(62320074U, "FinishButtonText");
			OwaOptionStrings.stringIDs.Add(3179829084U, "UserNameMOSIDLabel");
			OwaOptionStrings.stringIDs.Add(3449770263U, "ChangeMyMobilePhoneSettings");
			OwaOptionStrings.stringIDs.Add(2657242021U, "RequirementsReadWriteMailboxDescription");
			OwaOptionStrings.stringIDs.Add(1654895332U, "MessageTypeMatchesConditionFormat");
			OwaOptionStrings.stringIDs.Add(226228553U, "NewRoomCreationWarningText");
			OwaOptionStrings.stringIDs.Add(932151145U, "InboxRuleMyNameInToBoxConditionFlyOutText");
			OwaOptionStrings.stringIDs.Add(2712198432U, "FirstSyncOnLabel");
			OwaOptionStrings.stringIDs.Add(2830841285U, "DeleteGroupConfirmation");
			OwaOptionStrings.stringIDs.Add(2230814600U, "PendingWipeCommandIssuedLabel");
			OwaOptionStrings.stringIDs.Add(3090796481U, "OwnerChangedModerationReminder");
			OwaOptionStrings.stringIDs.Add(495329060U, "InboxRuleFromConditionPreCannedText");
			OwaOptionStrings.stringIDs.Add(3366412874U, "MaximumConflictInstances");
			OwaOptionStrings.stringIDs.Add(2502183272U, "EmailSubscriptions");
			OwaOptionStrings.stringIDs.Add(2174801140U, "InboxRuleFlaggedForActionConditionFlyOutText");
			OwaOptionStrings.stringIDs.Add(3159390523U, "ViewRPTDurationLabel");
			OwaOptionStrings.stringIDs.Add(926911138U, "OnOffColumn");
			OwaOptionStrings.stringIDs.Add(4270339173U, "FromSubscriptionConditionFormat");
			OwaOptionStrings.stringIDs.Add(2476211802U, "EmailComposeModeSeparateForm");
			OwaOptionStrings.stringIDs.Add(1739026864U, "ReadingPaneSlab");
			OwaOptionStrings.stringIDs.Add(77678270U, "OOF");
			OwaOptionStrings.stringIDs.Add(1330353058U, "SearchResultsCaption");
			OwaOptionStrings.stringIDs.Add(3130274824U, "SubjectLabel");
			OwaOptionStrings.stringIDs.Add(2220842206U, "Minute");
			OwaOptionStrings.stringIDs.Add(753397160U, "SendAtColon");
			OwaOptionStrings.stringIDs.Add(177845278U, "NewCommandText");
			OwaOptionStrings.stringIDs.Add(4084881753U, "RetentionTypeRequired");
			OwaOptionStrings.stringIDs.Add(1138078555U, "UninstallExtensionsConfirmation");
			OwaOptionStrings.stringIDs.Add(3158885598U, "InboxRuleHasAttachmentConditionFlyOutText");
			OwaOptionStrings.stringIDs.Add(2285322212U, "MyselfEntFormat");
			OwaOptionStrings.stringIDs.Add(3472797073U, "ConnectedAccounts");
			OwaOptionStrings.stringIDs.Add(1659711502U, "GroupNotes");
			OwaOptionStrings.stringIDs.Add(1959773104U, "Status");
			OwaOptionStrings.stringIDs.Add(2313850175U, "TeamMailboxSyncStatusString");
			OwaOptionStrings.stringIDs.Add(993996681U, "AccountSecondaryNavigation");
			OwaOptionStrings.stringIDs.Add(2256263033U, "EmailSubscriptionSlabDescription");
			OwaOptionStrings.stringIDs.Add(3692628685U, "TeamMailboxMailString");
			OwaOptionStrings.stringIDs.Add(166656410U, "TeamMailboxLifecycleStatusString2");
			OwaOptionStrings.stringIDs.Add(298572739U, "JunkEmailTrustedListDescription");
			OwaOptionStrings.stringIDs.Add(3700740504U, "SundayCheckBoxText");
			OwaOptionStrings.stringIDs.Add(3309799253U, "ExtensionVersionTag");
			OwaOptionStrings.stringIDs.Add(2902697354U, "MailTip");
			OwaOptionStrings.stringIDs.Add(861811732U, "CalendarPublishingRestricted");
			OwaOptionStrings.stringIDs.Add(884476035U, "MailboxUsageUnavailable");
			OwaOptionStrings.stringIDs.Add(2108510325U, "Customize");
			OwaOptionStrings.stringIDs.Add(1115629481U, "ModerationEnabled");
			OwaOptionStrings.stringIDs.Add(3758991955U, "PreviewMarkAsReadBehaviorDelayed");
			OwaOptionStrings.stringIDs.Add(3807282937U, "ShareInformation");
			OwaOptionStrings.stringIDs.Add(1441613828U, "RetentionActionTypeArchive");
			OwaOptionStrings.stringIDs.Add(2977853317U, "SetUpNotifications");
			OwaOptionStrings.stringIDs.Add(2600878742U, "InboxRuleMoveToFolderActionFlyOutText");
			OwaOptionStrings.stringIDs.Add(854412308U, "JunkEmailContactsTrusted");
			OwaOptionStrings.stringIDs.Add(3522427619U, "TeamMailboxManagementString");
			OwaOptionStrings.stringIDs.Add(636192742U, "MessageTrackingTransferredEvent");
			OwaOptionStrings.stringIDs.Add(2329485994U, "SendToAllGalLessText");
			OwaOptionStrings.stringIDs.Add(2574861626U, "CalendarReminderInstruction");
			OwaOptionStrings.stringIDs.Add(3667432429U, "TotalMembers");
			OwaOptionStrings.stringIDs.Add(1262029551U, "MailboxUsageUnlimitedText");
			OwaOptionStrings.stringIDs.Add(394064455U, "CalendarTroubleshootingLinkText");
			OwaOptionStrings.stringIDs.Add(2661286284U, "DisplayRecoveryPasswordCommandText");
			OwaOptionStrings.stringIDs.Add(366963388U, "VoicemailWizardStep4Description");
			OwaOptionStrings.stringIDs.Add(365990270U, "IncomingSecurityLabel");
			OwaOptionStrings.stringIDs.Add(1133353571U, "InboxRuleForwardToActionText");
			OwaOptionStrings.stringIDs.Add(3807748391U, "InboxRuleMyNameIsGroupText");
			OwaOptionStrings.stringIDs.Add(3420690180U, "MailboxFolderDialogLabel");
			OwaOptionStrings.stringIDs.Add(21771688U, "ReturnToView");
			OwaOptionStrings.stringIDs.Add(469603767U, "DeviceActiveSyncVersionLabel");
			OwaOptionStrings.stringIDs.Add(4036777603U, "InstallFromPrivateUrlCaption");
			OwaOptionStrings.stringIDs.Add(3877512413U, "DeleteEmailSubscriptionConfirmation");
			OwaOptionStrings.stringIDs.Add(3119904789U, "VoicemailWizardComplete");
			OwaOptionStrings.stringIDs.Add(303742195U, "InboxRuleMarkAsReadActionFlyOutText");
			OwaOptionStrings.stringIDs.Add(3738802218U, "RPTDay");
			OwaOptionStrings.stringIDs.Add(2590635398U, "DeviceAccessStateSetByLabel");
			OwaOptionStrings.stringIDs.Add(1799036564U, "ViewGroupDetails");
			OwaOptionStrings.stringIDs.Add(2408048685U, "ToOnlyLabel");
			OwaOptionStrings.stringIDs.Add(2384846465U, "SensitivityDialogTitle");
			OwaOptionStrings.stringIDs.Add(1200890284U, "TeamMailboxLifecycleStatusString");
			OwaOptionStrings.stringIDs.Add(3028241800U, "WednesdayCheckBoxText");
			OwaOptionStrings.stringIDs.Add(2254792003U, "ExtensionRequirementsLabel");
			OwaOptionStrings.stringIDs.Add(2459846522U, "AlwaysShowBcc");
			OwaOptionStrings.stringIDs.Add(1703184725U, "ConflictPercentageAllowedErrorMessage");
			OwaOptionStrings.stringIDs.Add(3413282040U, "JoinRestrictionApprovalRequiredDetails");
			OwaOptionStrings.stringIDs.Add(1917681062U, "InboxRuleMarkImportanceActionText");
			OwaOptionStrings.stringIDs.Add(1073903281U, "InboxRuleRecipientAddressContainsConditionFlyOutText");
			OwaOptionStrings.stringIDs.Add(2228506517U, "Regional");
			OwaOptionStrings.stringIDs.Add(3644924947U, "VoicemailWizardTestDoneText");
			OwaOptionStrings.stringIDs.Add(2743720278U, "RemoveOldMeetingMessagesCheckBoxText");
			OwaOptionStrings.stringIDs.Add(63766217U, "InboxRuleBodyContainsConditionText");
			OwaOptionStrings.stringIDs.Add(1435597588U, "QLForward");
			OwaOptionStrings.stringIDs.Add(4103313421U, "VoicemailPhoneNumberColon");
			OwaOptionStrings.stringIDs.Add(1416133447U, "AddCommandText");
			OwaOptionStrings.stringIDs.Add(3917579091U, "Voicemail");
			OwaOptionStrings.stringIDs.Add(2321140182U, "StringArrayDialogTitle");
			OwaOptionStrings.stringIDs.Add(2696088269U, "MailboxUsageUnitTB");
			OwaOptionStrings.stringIDs.Add(1272238732U, "CalendarPublishingCopyLink");
			OwaOptionStrings.stringIDs.Add(1224467389U, "TimeStyles");
			OwaOptionStrings.stringIDs.Add(559979509U, "RPTYear");
			OwaOptionStrings.stringIDs.Add(2493642031U, "VoicemailLearnMoreVideo");
			OwaOptionStrings.stringIDs.Add(2564457623U, "InboxRuleHeaderContainsConditionFlyOutText");
			OwaOptionStrings.stringIDs.Add(1916260782U, "InboxRuleHasClassificationConditionText");
			OwaOptionStrings.stringIDs.Add(1806836259U, "ImportContactListPage1Caption");
			OwaOptionStrings.stringIDs.Add(3804549298U, "VoicemailWizardStep2Description");
			OwaOptionStrings.stringIDs.Add(1269143998U, "AddExtension");
			OwaOptionStrings.stringIDs.Add(2355901091U, "WithinSizeRangeDialogTitle");
			OwaOptionStrings.stringIDs.Add(2269401639U, "GetCalendarLogButtonText");
			OwaOptionStrings.stringIDs.Add(954238138U, "BlockDeviceConfirmMessage");
			OwaOptionStrings.stringIDs.Add(1294877450U, "CalendarPublishingRangeFrom");
			OwaOptionStrings.stringIDs.Add(3777014377U, "EnterPasscodeStepMessage");
			OwaOptionStrings.stringIDs.Add(3308373688U, "VoicemailCallFwdHavingTrouble");
			OwaOptionStrings.stringIDs.Add(3543278019U, "StartForward");
			OwaOptionStrings.stringIDs.Add(2118875232U, "VoicemailCallFwdStep2");
			OwaOptionStrings.stringIDs.Add(1225604874U, "JoinRestrictionOpenDetails");
			OwaOptionStrings.stringIDs.Add(2863884442U, "IncomingSecurityNone");
			OwaOptionStrings.stringIDs.Add(1827349577U, "InboxRuleMyNameNotInToBoxConditionText");
			OwaOptionStrings.stringIDs.Add(2515135494U, "ChangePermissions");
			OwaOptionStrings.stringIDs.Add(1807321962U, "InboxRuleCopyToFolderActionFlyOutText");
			OwaOptionStrings.stringIDs.Add(3118863998U, "InboxRuleSubjectContainsConditionFlyOutText");
			OwaOptionStrings.stringIDs.Add(1238462568U, "RequirementsRestrictedValue");
			OwaOptionStrings.stringIDs.Add(394662652U, "InboxRuleRedirectToActionText");
			OwaOptionStrings.stringIDs.Add(1472234713U, "ImportContactListPage1Step2");
			OwaOptionStrings.stringIDs.Add(1087517319U, "JunkEmailWatermarkText");
			OwaOptionStrings.stringIDs.Add(3803411495U, "TextMessagingStatusPrefixStatus");
			OwaOptionStrings.stringIDs.Add(4182339999U, "ShowHoursIn");
			OwaOptionStrings.stringIDs.Add(3390931208U, "DefaultFormat");
			OwaOptionStrings.stringIDs.Add(2893153135U, "SubscriptionDialogTitle");
			OwaOptionStrings.stringIDs.Add(3581746357U, "NewItemNotificationEmailToast");
			OwaOptionStrings.stringIDs.Add(994337869U, "TeamMailboxTabUsersHelpString1");
			OwaOptionStrings.stringIDs.Add(307942560U, "SchedulingPermissionsSlab");
			OwaOptionStrings.stringIDs.Add(3141117137U, "ConversationSortOrderInstruction");
			OwaOptionStrings.stringIDs.Add(3348884585U, "WipeDeviceCommandText");
			OwaOptionStrings.stringIDs.Add(757309800U, "InboxRuleSentOrReceivedGroupText");
			OwaOptionStrings.stringIDs.Add(1884883826U, "Myself");
			OwaOptionStrings.stringIDs.Add(885509158U, "NewestOnBottom");
			OwaOptionStrings.stringIDs.Add(4104125374U, "NewItemNotificationFaxToast");
			OwaOptionStrings.stringIDs.Add(4049797668U, "EmailComposeModeInline");
			OwaOptionStrings.stringIDs.Add(880505963U, "NewRuleString");
			OwaOptionStrings.stringIDs.Add(2688608043U, "NoMessageCategoryAvailable");
			OwaOptionStrings.stringIDs.Add(1328547437U, "CurrentStatus");
			OwaOptionStrings.stringIDs.Add(1395647162U, "SubscriptionProcessingError");
			OwaOptionStrings.stringIDs.Add(1075069807U, "StopAndRetrieveLogCommandText");
			OwaOptionStrings.stringIDs.Add(1917268543U, "TimeIncrementThirtyMinutes");
			OwaOptionStrings.stringIDs.Add(3253517897U, "RetentionActionNeverMove");
			OwaOptionStrings.stringIDs.Add(3600602092U, "VoicemailMobileOperatorColon");
			OwaOptionStrings.stringIDs.Add(2788873687U, "ConnectedAccountsDescriptionForForwarding");
			OwaOptionStrings.stringIDs.Add(2316526399U, "StopForward");
			OwaOptionStrings.stringIDs.Add(3739699132U, "FirstWeekOfYear");
			OwaOptionStrings.stringIDs.Add(2986818840U, "RegionListLabel");
			OwaOptionStrings.stringIDs.Add(2982857632U, "InstallFromMarketplace");
			OwaOptionStrings.stringIDs.Add(738818108U, "RulesNameColumn");
			OwaOptionStrings.stringIDs.Add(3166948736U, "DeviceOSLabel");
			OwaOptionStrings.stringIDs.Add(781854721U, "InboxRuleSentOnlyToMeConditionText");
			OwaOptionStrings.stringIDs.Add(364458648U, "EditYourPassword");
			OwaOptionStrings.stringIDs.Add(3609822921U, "EnforceSchedulingHorizon");
			OwaOptionStrings.stringIDs.Add(872104809U, "TeamMailboxManagementString2");
			OwaOptionStrings.stringIDs.Add(3459526114U, "SearchMessageTipForIWUser");
			OwaOptionStrings.stringIDs.Add(904314811U, "ConnectedAccountsDescriptionForSubscription");
			OwaOptionStrings.stringIDs.Add(3180823751U, "QLManageOrganization");
			OwaOptionStrings.stringIDs.Add(2201160322U, "JoinRestrictionApprovalRequired");
			OwaOptionStrings.stringIDs.Add(3344174056U, "ExtensionCanNotBeUninstalled");
			OwaOptionStrings.stringIDs.Add(1952564584U, "VoicemailWizardStep4Title");
			OwaOptionStrings.stringIDs.Add(1001261206U, "ViewExtensionDetails");
			OwaOptionStrings.stringIDs.Add(4181395631U, "VoicemailCarrierRatesMayApply");
			OwaOptionStrings.stringIDs.Add(2288925761U, "DeliveryReports");
			OwaOptionStrings.stringIDs.Add(3052389362U, "AllRequestOutOfPolicyText");
			OwaOptionStrings.stringIDs.Add(3585825647U, "RemoveDeviceConfirmMessage");
			OwaOptionStrings.stringIDs.Add(1401723706U, "StatusLabel");
			OwaOptionStrings.stringIDs.Add(3255907698U, "InboxRuleSubjectOrBodyContainsConditionText");
			OwaOptionStrings.stringIDs.Add(1547367963U, "OwnerLabel");
			OwaOptionStrings.stringIDs.Add(3925202351U, "RequireSenderAuthFalse");
			OwaOptionStrings.stringIDs.Add(2460807174U, "AllowedSendersLabel");
			OwaOptionStrings.stringIDs.Add(759422978U, "IncomingSecuritySSL");
			OwaOptionStrings.stringIDs.Add(952725660U, "CarrierListLabel");
			OwaOptionStrings.stringIDs.Add(3525277106U, "InboxRuleDescriptionNote");
			OwaOptionStrings.stringIDs.Add(3736448266U, "NewImapSubscription");
			OwaOptionStrings.stringIDs.Add(3089967521U, "TeamMailboxStartSyncButtonString");
			OwaOptionStrings.stringIDs.Add(2243312622U, "NotificationsForCalendarUpdate");
			OwaOptionStrings.stringIDs.Add(1134119669U, "ReadReceiptsSlab");
			OwaOptionStrings.stringIDs.Add(1550773931U, "DetailsLinkText");
			OwaOptionStrings.stringIDs.Add(1454393937U, "Help");
			OwaOptionStrings.stringIDs.Add(491491954U, "SearchGroups");
			OwaOptionStrings.stringIDs.Add(3108971784U, "ShowConversationAsTreeInstruction");
			OwaOptionStrings.stringIDs.Add(3137025124U, "BypassModerationSenders");
			OwaOptionStrings.stringIDs.Add(3781814528U, "RetentionActionDeleteAndAllowRecovery");
			OwaOptionStrings.stringIDs.Add(999443141U, "PreviewMarkAsReadDelaytimeTextPre");
			OwaOptionStrings.stringIDs.Add(565319905U, "RPTMonths");
			OwaOptionStrings.stringIDs.Add(1719300497U, "AfterMoveOrDeleteBehavior");
			OwaOptionStrings.stringIDs.Add(1359223188U, "HideGroupFromAddressLists");
			OwaOptionStrings.stringIDs.Add(2825394221U, "VoicemailWizardStep1Description");
			OwaOptionStrings.stringIDs.Add(2930172971U, "ReviewLinkText");
			OwaOptionStrings.stringIDs.Add(2325389167U, "Processing");
			OwaOptionStrings.stringIDs.Add(312564594U, "DailyCalendarAgendas");
			OwaOptionStrings.stringIDs.Add(1969050778U, "PreviewMarkAsReadBehaviorOnSelectionChange");
			OwaOptionStrings.stringIDs.Add(1817328658U, "TimeZoneLabelText");
			OwaOptionStrings.stringIDs.Add(3165477128U, "QLVoiceMail");
			OwaOptionStrings.stringIDs.Add(1490355023U, "VoicemailSignUpIntro");
			OwaOptionStrings.stringIDs.Add(4294954307U, "VoicemailStep2");
			OwaOptionStrings.stringIDs.Add(519599668U, "TeamMailboxMembershipString");
			OwaOptionStrings.stringIDs.Add(3463577248U, "PasscodeLabel");
			OwaOptionStrings.stringIDs.Add(1836294011U, "PersonalSettingPasswordAfterChange");
			OwaOptionStrings.stringIDs.Add(3090647821U, "VerificationSuccessPageTitle");
			OwaOptionStrings.stringIDs.Add(843476083U, "EnableAutomaticProcessingNote");
			OwaOptionStrings.stringIDs.Add(2422328107U, "Days");
			OwaOptionStrings.stringIDs.Add(521914504U, "NotificationsNotSetUp");
			OwaOptionStrings.stringIDs.Add(2608069889U, "ModerationNotificationsInternal");
			OwaOptionStrings.stringIDs.Add(3040964019U, "ProtocolSettings");
			OwaOptionStrings.stringIDs.Add(4275718891U, "EnableAutomaticProcessing");
			OwaOptionStrings.stringIDs.Add(2045071371U, "MessageOptionsSlab");
			OwaOptionStrings.stringIDs.Add(2789466833U, "ChooseMessageFont");
			OwaOptionStrings.stringIDs.Add(3563093647U, "Password");
			OwaOptionStrings.stringIDs.Add(1901616043U, "OWAExtensions");
			OwaOptionStrings.stringIDs.Add(664237562U, "StringArrayDialogLabel");
			OwaOptionStrings.stringIDs.Add(944449543U, "Unlimited");
			OwaOptionStrings.stringIDs.Add(4033068416U, "VoicemailSMSOptionVoiceMailOnly");
			OwaOptionStrings.stringIDs.Add(3853542865U, "Rules");
			OwaOptionStrings.stringIDs.Add(860120982U, "ModeratedByEmptyDataText");
			OwaOptionStrings.stringIDs.Add(499094223U, "TextMessaging");
			OwaOptionStrings.stringIDs.Add(2038251428U, "FromLabel");
			OwaOptionStrings.stringIDs.Add(2108668357U, "GroupModerators");
			OwaOptionStrings.stringIDs.Add(4089833856U, "ReadingPaneInstruction");
			OwaOptionStrings.stringIDs.Add(1749888754U, "RPTNone");
			OwaOptionStrings.stringIDs.Add(2618236676U, "Spelling");
			OwaOptionStrings.stringIDs.Add(3198693159U, "CancelWipeDeviceCommandText");
			OwaOptionStrings.stringIDs.Add(3747885709U, "AutomaticRepliesEnabledText");
			OwaOptionStrings.stringIDs.Add(2372820691U, "DisplayNameLabel");
			OwaOptionStrings.stringIDs.Add(2449169153U, "CancelButtonText");
			OwaOptionStrings.stringIDs.Add(3156651890U, "GroupMembershipApproval");
			OwaOptionStrings.stringIDs.Add(3398667566U, "InlcudedRecipientTypesLabel");
			OwaOptionStrings.stringIDs.Add(2328219947U, "Name");
			OwaOptionStrings.stringIDs.Add(590111363U, "RetentionActionTypeDelete");
			OwaOptionStrings.stringIDs.Add(3652102134U, "InboxRuleMyNameInCcBoxConditionFlyOutText");
			OwaOptionStrings.stringIDs.Add(2328532194U, "ThursdayCheckBoxText");
			OwaOptionStrings.stringIDs.Add(258131565U, "JoinGroup");
			OwaOptionStrings.stringIDs.Add(767759945U, "Account");
			OwaOptionStrings.stringIDs.Add(1107043549U, "InboxRuleMessageTypeMatchesConditionFlyOutText");
			OwaOptionStrings.stringIDs.Add(1934149463U, "InboxRuleMoveCopyDeleteGroupText");
			OwaOptionStrings.stringIDs.Add(294641626U, "RegionalSettingsInstruction");
			OwaOptionStrings.stringIDs.Add(1009337581U, "NameColumn");
			OwaOptionStrings.stringIDs.Add(2921806668U, "InboxRuleWithSensitivityConditionFlyOutText");
			OwaOptionStrings.stringIDs.Add(744201260U, "ClassificationDialogTitle");
			OwaOptionStrings.stringIDs.Add(2012947571U, "RuleFromAndMoveToFolderTemplate");
			OwaOptionStrings.stringIDs.Add(2112722880U, "DomainNameNotSetError");
			OwaOptionStrings.stringIDs.Add(3382481163U, "MakeSecurityGroup");
			OwaOptionStrings.stringIDs.Add(111122248U, "ContactNumbersBookmark");
			OwaOptionStrings.stringIDs.Add(3443085801U, "InboxRuleSentToConditionText");
			OwaOptionStrings.stringIDs.Add(1620775745U, "MemberOfGroups");
			OwaOptionStrings.stringIDs.Add(2393490804U, "InboxRuleIncludeTheseWordsGroupText");
			OwaOptionStrings.stringIDs.Add(691241872U, "MailTipLabel");
			OwaOptionStrings.stringIDs.Add(1413097981U, "MessageTypeDialogLabel");
			OwaOptionStrings.stringIDs.Add(1541052658U, "RegionalSettingsSlab");
			OwaOptionStrings.stringIDs.Add(346591353U, "VoicemailWizardStep3Title");
			OwaOptionStrings.stringIDs.Add(2731962334U, "InboxRuleMyNameNotInToBoxConditionFlyOutText");
			OwaOptionStrings.stringIDs.Add(510973434U, "ReturnToOWA");
			OwaOptionStrings.stringIDs.Add(1564510770U, "InboxRuleMyNameInToBoxConditionText");
			OwaOptionStrings.stringIDs.Add(3567818904U, "CommitButtonText");
			OwaOptionStrings.stringIDs.Add(1864411629U, "TeamMailboxShowInMyClientString");
			OwaOptionStrings.stringIDs.Add(2397374352U, "InboxRuleMarkAsReadActionText");
			OwaOptionStrings.stringIDs.Add(663639812U, "ClassificationDialogLabel");
			OwaOptionStrings.stringIDs.Add(3313607735U, "WarningAlt");
			OwaOptionStrings.stringIDs.Add(2034904223U, "TeamMailboxManagementString4");
			OwaOptionStrings.stringIDs.Add(405905481U, "Mail");
			OwaOptionStrings.stringIDs.Add(624357391U, "ImportContactList");
			OwaOptionStrings.stringIDs.Add(1680686955U, "QLImportContacts");
			OwaOptionStrings.stringIDs.Add(3391394280U, "InboxRule");
			OwaOptionStrings.stringIDs.Add(3979669380U, "WithinDateRangeDialogTitle");
			OwaOptionStrings.stringIDs.Add(1449143764U, "ReminderSoundEnabled");
			OwaOptionStrings.stringIDs.Add(1634623908U, "RecipientAddressContainsConditionFormat");
			OwaOptionStrings.stringIDs.Add(481168139U, "MessageFormatPlainText");
			OwaOptionStrings.stringIDs.Add(203738408U, "DeleteInboxRuleConfirmation");
			OwaOptionStrings.stringIDs.Add(2879015231U, "ForwardEmailTitle");
			OwaOptionStrings.stringIDs.Add(3395154598U, "BypassModerationSendersEmptyDataText");
			OwaOptionStrings.stringIDs.Add(396113514U, "RuleSubjectContainsAndDeleteMessageTemplate");
			OwaOptionStrings.stringIDs.Add(1053461607U, "HideDeletedItems");
			OwaOptionStrings.stringIDs.Add(3137581701U, "VoicemailSetupNowButtonText");
			OwaOptionStrings.stringIDs.Add(797589720U, "CalendarPermissions");
			OwaOptionStrings.stringIDs.Add(1108157303U, "ModerationNotificationsAlways");
			OwaOptionStrings.stringIDs.Add(1038822924U, "ExternalMessageInstruction");
			OwaOptionStrings.stringIDs.Add(2936411810U, "RequirementsReadItemValue");
			OwaOptionStrings.stringIDs.Add(4225166530U, "SchedulingOptionsSlab");
			OwaOptionStrings.stringIDs.Add(742094856U, "ShowConversationTree");
			OwaOptionStrings.stringIDs.Add(1303277130U, "RetentionPolicies");
			OwaOptionStrings.stringIDs.Add(2271033387U, "CalendarPublishingSubscriptionUrl");
			OwaOptionStrings.stringIDs.Add(1808002020U, "ResendVerificationEmailCommandText");
			OwaOptionStrings.stringIDs.Add(2949887068U, "TextMessagingNotification");
			OwaOptionStrings.stringIDs.Add(3246855305U, "InstalledByColumn");
			OwaOptionStrings.stringIDs.Add(3100669839U, "GroupOrganizationalUnit");
			OwaOptionStrings.stringIDs.Add(1750301704U, "MailboxFolderDialogTitle");
			OwaOptionStrings.stringIDs.Add(2669253750U, "PersonalSettingOldPassword");
			OwaOptionStrings.stringIDs.Add(2728870366U, "VoicemailStep3");
			OwaOptionStrings.stringIDs.Add(3140976517U, "CityLabel");
			OwaOptionStrings.stringIDs.Add(1022114031U, "SentToConditionFormat");
			OwaOptionStrings.stringIDs.Add(260433958U, "QLSubscription");
			OwaOptionStrings.stringIDs.Add(803108465U, "ViewRPTDetailsTitle");
			OwaOptionStrings.stringIDs.Add(3201916728U, "MyGroups");
			OwaOptionStrings.stringIDs.Add(349837376U, "TeamMailboxesString");
			OwaOptionStrings.stringIDs.Add(1220880656U, "DeliveryReport");
			OwaOptionStrings.stringIDs.Add(3294022635U, "LastNameLabel");
			OwaOptionStrings.stringIDs.Add(813081783U, "CalendarPublishingStop");
			OwaOptionStrings.stringIDs.Add(3339583031U, "VoicemailWizardStep3Description");
			OwaOptionStrings.stringIDs.Add(2176445071U, "QLWhatsNewForOrganizations");
			OwaOptionStrings.stringIDs.Add(3853933336U, "ReadReceiptResponseAlways");
			OwaOptionStrings.stringIDs.Add(3555736038U, "JunkEmailTrustedListsOnly");
			OwaOptionStrings.stringIDs.Add(2391157043U, "MatchSortOrder");
			OwaOptionStrings.stringIDs.Add(2421998101U, "DevicePhoneNumberLabel");
			OwaOptionStrings.stringIDs.Add(3176989690U, "InboxRuleMessageTypeMatchesConditionText");
			OwaOptionStrings.stringIDs.Add(4266991160U, "RetentionTypeOptional");
			OwaOptionStrings.stringIDs.Add(3996928596U, "UseSettings");
			OwaOptionStrings.stringIDs.Add(1958633523U, "SearchAllGroups");
			OwaOptionStrings.stringIDs.Add(2117869041U, "MembersLabel");
			OwaOptionStrings.stringIDs.Add(1063608921U, "FreeBusyInformation");
			OwaOptionStrings.stringIDs.Add(133785142U, "DeviceIMEILabel");
			OwaOptionStrings.stringIDs.Add(696030412U, "Day");
			OwaOptionStrings.stringIDs.Add(192786603U, "InboxRuleMoveToFolderActionText");
			OwaOptionStrings.stringIDs.Add(779120846U, "SelectOne");
			OwaOptionStrings.stringIDs.Add(846466190U, "InboxRuleApplyCategoryActionFlyOutText");
			OwaOptionStrings.stringIDs.Add(3990783281U, "SetupEmailNotificationsLink");
			OwaOptionStrings.stringIDs.Add(117083423U, "NewDistributionGroupCaption");
			OwaOptionStrings.stringIDs.Add(1217964679U, "ViewRPTRetentionActionLabel");
			OwaOptionStrings.stringIDs.Add(2135021465U, "ShowWeekNumbersCheckBoxText");
			OwaOptionStrings.stringIDs.Add(1136770150U, "UserNameWLIDLabel");
			OwaOptionStrings.stringIDs.Add(2028785684U, "SearchMessagesIReceivedLabel");
			OwaOptionStrings.stringIDs.Add(2151247442U, "InboxRuleWithinDateRangeConditionText");
			OwaOptionStrings.stringIDs.Add(839503915U, "InboxRuleSendTextMessageNotificationToActionText");
			OwaOptionStrings.stringIDs.Add(3985530922U, "SentLabel");
			OwaOptionStrings.stringIDs.Add(2324730983U, "GroupInformation");
			OwaOptionStrings.stringIDs.Add(2601154324U, "VoicemailAskOperator");
			OwaOptionStrings.stringIDs.Add(4016415205U, "OWA");
			OwaOptionStrings.stringIDs.Add(574892128U, "MailTipWaterMark");
			OwaOptionStrings.stringIDs.Add(969182653U, "InboxRuleWithSensitivityConditionText");
			OwaOptionStrings.stringIDs.Add(1090313663U, "RetentionActionTypeDefaultArchive");
			OwaOptionStrings.stringIDs.Add(454922625U, "RemoveOptionalRPTConfirmation");
			OwaOptionStrings.stringIDs.Add(1570694400U, "DevicePolicyUpdateTimeLabel");
			OwaOptionStrings.stringIDs.Add(1674956465U, "MyselfLiveFormat");
			OwaOptionStrings.stringIDs.Add(2143618034U, "EmailDomain");
			OwaOptionStrings.stringIDs.Add(148346506U, "RequireSenderAuthTrue");
			OwaOptionStrings.stringIDs.Add(774036985U, "InstallFromPrivateUrlInformation");
			OwaOptionStrings.stringIDs.Add(446237888U, "PhoneLabel");
			OwaOptionStrings.stringIDs.Add(731708393U, "SubscriptionAction");
			OwaOptionStrings.stringIDs.Add(2401508539U, "Weeks");
			OwaOptionStrings.stringIDs.Add(3825281971U, "InboxRuleForwardRedirectGroupText");
			OwaOptionStrings.stringIDs.Add(3719582777U, "DisplayName");
			OwaOptionStrings.stringIDs.Add(3507262891U, "MembershipApprovalLabel");
			OwaOptionStrings.stringIDs.Add(1542072756U, "InboxRuleSendTextMessageNotificationToActionFlyOutText");
			OwaOptionStrings.stringIDs.Add(3475829388U, "TeamMailboxSPSiteString");
			OwaOptionStrings.stringIDs.Add(1559211265U, "InboxRuleSubjectOrBodyContainsConditionFlyOutText");
			OwaOptionStrings.stringIDs.Add(1690161621U, "PleaseWait");
			OwaOptionStrings.stringIDs.Add(258867536U, "JoinRestrictionOpen");
			OwaOptionStrings.stringIDs.Add(418352798U, "CalendarSharingConfirmDeletionSingle");
			OwaOptionStrings.stringIDs.Add(1869505729U, "InboxRuleSubjectContainsConditionPreCannedText");
			OwaOptionStrings.stringIDs.Add(2815967975U, "CalendarPublishingStart");
			OwaOptionStrings.stringIDs.Add(1632853873U, "TextMessagingSlabMessageNotificationOnly");
			OwaOptionStrings.stringIDs.Add(3304439859U, "RequirementsRestrictedDescription");
			OwaOptionStrings.stringIDs.Add(4114414654U, "SelectAUser");
			OwaOptionStrings.stringIDs.Add(3042824760U, "NotificationStepOneMessage");
			OwaOptionStrings.stringIDs.Add(2897641359U, "QLPushEmail");
			OwaOptionStrings.stringIDs.Add(1890566340U, "NewInboxRuleTitle");
			OwaOptionStrings.stringIDs.Add(929330042U, "SendToKnownContactsText");
			OwaOptionStrings.stringIDs.Add(48495520U, "IncomingAuthenticationSpa");
			OwaOptionStrings.stringIDs.Add(2696088282U, "MailboxUsageUnitGB");
			OwaOptionStrings.stringIDs.Add(3651870140U, "MessageTrackingDeliveredEvent");
			OwaOptionStrings.stringIDs.Add(3772909321U, "SelectOneOrMoreText");
			OwaOptionStrings.stringIDs.Add(2600859168U, "InboxRuleForwardAsAttachmentToActionText");
			OwaOptionStrings.stringIDs.Add(1105583403U, "DontSeeMyRegionOrMobileOperator");
			OwaOptionStrings.stringIDs.Add(1436160346U, "PreviewMarkAsReadDelaytimeTextPost");
			OwaOptionStrings.stringIDs.Add(2060430325U, "NoMessageClassificationAvailable");
			OwaOptionStrings.stringIDs.Add(3929633211U, "TeamMailboxTabPropertiesHelpString");
			OwaOptionStrings.stringIDs.Add(2438188750U, "TeamMailboxManagementString1");
			OwaOptionStrings.stringIDs.Add(4047036620U, "RetentionPeriodHeader");
			OwaOptionStrings.stringIDs.Add(4263477646U, "Phone");
			OwaOptionStrings.stringIDs.Add(2862314013U, "WhoHasPermission");
			OwaOptionStrings.stringIDs.Add(1766818386U, "NotAvailable");
			OwaOptionStrings.stringIDs.Add(1253897847U, "FlaggedForActionConditionFormat");
			OwaOptionStrings.stringIDs.Add(4028994607U, "EndTimeText");
			OwaOptionStrings.stringIDs.Add(234118977U, "BookingWindowInDays");
			OwaOptionStrings.stringIDs.Add(3253425509U, "RemovingPreviewPhoto");
			OwaOptionStrings.stringIDs.Add(1666931485U, "CalendarDiagnosticLogSlab");
			OwaOptionStrings.stringIDs.Add(3454695329U, "ModerationNotification");
			OwaOptionStrings.stringIDs.Add(1207804035U, "VoicemailWizardPinlessOptionsText");
			OwaOptionStrings.stringIDs.Add(2476883967U, "VoicemailClearSettings");
			OwaOptionStrings.stringIDs.Add(1094425034U, "PersonalGroups");
			OwaOptionStrings.stringIDs.Add(259319032U, "DistributionGroupText");
			OwaOptionStrings.stringIDs.Add(1335943323U, "ProfileContactNumbers");
			OwaOptionStrings.stringIDs.Add(167657761U, "DeliveryReportFor");
			OwaOptionStrings.stringIDs.Add(232975871U, "InboxRuleRedirectToActionFlyOutText");
			OwaOptionStrings.stringIDs.Add(2251801313U, "AutomateProcessing");
			OwaOptionStrings.stringIDs.Add(3493646365U, "CalendarPublishingAccessLevel");
			OwaOptionStrings.stringIDs.Add(248447020U, "ForwardEmailTo");
			OwaOptionStrings.stringIDs.Add(2062923077U, "SettingUpProtocolAccess");
			OwaOptionStrings.stringIDs.Add(1732697476U, "AdminTools");
			OwaOptionStrings.stringIDs.Add(1669803655U, "InstalledExtensionDescription");
			OwaOptionStrings.stringIDs.Add(568490923U, "EmailComposeModeInstruction");
			OwaOptionStrings.stringIDs.Add(3271192817U, "InboxRuleDeleteMessageActionText");
			OwaOptionStrings.stringIDs.Add(442231679U, "SummaryToDate");
			OwaOptionStrings.stringIDs.Add(2631270417U, "Extension");
			OwaOptionStrings.stringIDs.Add(1549389490U, "CalendarSharingConfirmDeletionMultiple");
			OwaOptionStrings.stringIDs.Add(1314739276U, "Depart");
			OwaOptionStrings.stringIDs.Add(2193710474U, "EmailNotificationsLink");
			OwaOptionStrings.stringIDs.Add(1192767596U, "OpenPreviousItem");
			OwaOptionStrings.stringIDs.Add(1886990352U, "RPTPickerDialogTitle");
			OwaOptionStrings.stringIDs.Add(625886851U, "CheckForForgottenAttachments");
			OwaOptionStrings.stringIDs.Add(2755319165U, "FaxLabel");
			OwaOptionStrings.stringIDs.Add(2677919833U, "SentTime");
			OwaOptionStrings.stringIDs.Add(673404970U, "DeviceTypeHeaderText");
			OwaOptionStrings.stringIDs.Add(338457679U, "RPTExpireNever");
			OwaOptionStrings.stringIDs.Add(779255172U, "TeamMailboxEmailAddressString");
			OwaOptionStrings.stringIDs.Add(530176925U, "InboxRuleWithinSizeRangeConditionText");
			OwaOptionStrings.stringIDs.Add(2213908942U, "Week");
			OwaOptionStrings.stringIDs.Add(2185035188U, "WithinDateRangeConditionFormat");
			OwaOptionStrings.stringIDs.Add(2502896170U, "DisplayNameNotSetError");
			OwaOptionStrings.stringIDs.Add(198361899U, "FirstDayOfWeek");
			OwaOptionStrings.stringIDs.Add(2556260573U, "ProcessExternalMeetingMessagesCheckBoxText");
			OwaOptionStrings.stringIDs.Add(2041861674U, "DepartRestrictionClosed");
			OwaOptionStrings.stringIDs.Add(886092638U, "MailTipShortLabel");
			OwaOptionStrings.stringIDs.Add(2483718204U, "StateOrProvinceLabel");
			OwaOptionStrings.stringIDs.Add(67466988U, "PrimaryEmailAddress");
			OwaOptionStrings.stringIDs.Add(2578200319U, "AllowedSendersEmptyLabel");
			OwaOptionStrings.stringIDs.Add(1878748957U, "HotmailSubscription");
			OwaOptionStrings.stringIDs.Add(2638705563U, "DeviceIDLabel");
			OwaOptionStrings.stringIDs.Add(1891390470U, "AlwaysShowFrom");
			OwaOptionStrings.stringIDs.Add(1795719989U, "SearchButtonText");
			OwaOptionStrings.stringIDs.Add(4001240631U, "ViewRPTDescriptionLabel");
			OwaOptionStrings.stringIDs.Add(2624099920U, "Hour");
			OwaOptionStrings.stringIDs.Add(3567033964U, "FlagStatusDialogTitle");
			OwaOptionStrings.stringIDs.Add(1463926066U, "NewSubscriptionProgress");
			OwaOptionStrings.stringIDs.Add(2455977975U, "EditProfilePhoto");
			OwaOptionStrings.stringIDs.Add(614024525U, "InstallFromPrivateUrl");
			OwaOptionStrings.stringIDs.Add(2869098816U, "DeleteGroupsConfirmation");
			OwaOptionStrings.stringIDs.Add(870015935U, "OwnedGroups");
			OwaOptionStrings.stringIDs.Add(2322301053U, "NewDistributionGroupTitle");
			OwaOptionStrings.stringIDs.Add(2562827638U, "JunkEmailConfiguration");
			OwaOptionStrings.stringIDs.Add(3873537801U, "ProfileGeneral");
			OwaOptionStrings.stringIDs.Add(2344432611U, "CalendarSharingOutlookNote");
			OwaOptionStrings.stringIDs.Add(829380600U, "RemoveOptionalRPTsConfirmation");
			OwaOptionStrings.stringIDs.Add(2472855514U, "VerificationSuccessTitle");
			OwaOptionStrings.stringIDs.Add(3585330084U, "ResponseMessageSlab");
			OwaOptionStrings.stringIDs.Add(1695816201U, "TeamMailboxMaintenanceString");
			OwaOptionStrings.stringIDs.Add(1565811752U, "UrlLabelText");
			OwaOptionStrings.stringIDs.Add(3765209046U, "DepartRestrictionOpen");
			OwaOptionStrings.stringIDs.Add(2239331369U, "PendingWipeCommandSentLabel");
			OwaOptionStrings.stringIDs.Add(2494867144U, "SubjectOrBodyContainsConditionFormat");
			OwaOptionStrings.stringIDs.Add(1747388690U, "LeaveMailOnServerLabel");
			OwaOptionStrings.stringIDs.Add(2054798096U, "JoinRestrictionClosed");
			OwaOptionStrings.stringIDs.Add(46775972U, "ScheduleOnlyDuringWorkHours");
			OwaOptionStrings.stringIDs.Add(614963610U, "ImportanceDialogLabel");
			OwaOptionStrings.stringIDs.Add(3410351290U, "CalendarPublishingLinkNotes");
			OwaOptionStrings.stringIDs.Add(1356158868U, "InboxRuleSentOnlyToMeConditionFlyOutText");
			OwaOptionStrings.stringIDs.Add(2409360890U, "InternalMessageInstruction");
			OwaOptionStrings.stringIDs.Add(1192855764U, "JunkEmailDisabled");
			OwaOptionStrings.stringIDs.Add(624477965U, "InboxRuleForwardAsAttachmentToActionFlyOutText");
			OwaOptionStrings.stringIDs.Add(2343908218U, "WarningNote");
			OwaOptionStrings.stringIDs.Add(4104737160U, "HomePagePrimaryLink");
			OwaOptionStrings.stringIDs.Add(837698603U, "LimitDuration");
			OwaOptionStrings.stringIDs.Add(3506885255U, "MailboxUsageExceededText");
			OwaOptionStrings.stringIDs.Add(3647997407U, "UnSupportedRule");
			OwaOptionStrings.stringIDs.Add(37810473U, "DeviceModelLabel");
			OwaOptionStrings.stringIDs.Add(2737072517U, "NotificationLinksMessage");
			OwaOptionStrings.stringIDs.Add(2702941902U, "ResourceSlab");
			OwaOptionStrings.stringIDs.Add(4137250117U, "RetentionPolicyTagPageTitle");
			OwaOptionStrings.stringIDs.Add(1485698978U, "AllBookInPolicyText");
			OwaOptionStrings.stringIDs.Add(2179002981U, "MessageFormatHtml");
			OwaOptionStrings.stringIDs.Add(755136567U, "FridayCheckBoxText");
			OwaOptionStrings.stringIDs.Add(3099627267U, "InboxRuleMyNameInCcBoxConditionText");
			OwaOptionStrings.stringIDs.Add(2592606558U, "PreviewMarkAsReadDelaytimeErrorMessage");
			OwaOptionStrings.stringIDs.Add(3576971014U, "JunkEmailValidationErrorMessage");
			OwaOptionStrings.stringIDs.Add(3723221224U, "TeamMailboxTabUsersHelpString2");
			OwaOptionStrings.stringIDs.Add(856468152U, "AllRequestInPolicyText");
			OwaOptionStrings.stringIDs.Add(3383857716U, "ImapSubscription");
			OwaOptionStrings.stringIDs.Add(2201998933U, "InboxRuleWithImportanceConditionFlyOutText");
			OwaOptionStrings.stringIDs.Add(600392665U, "TeamMailboxDisplayNameString");
			OwaOptionStrings.stringIDs.Add(3600988164U, "TeamMailboxManagementString3");
			OwaOptionStrings.stringIDs.Add(1735921300U, "ConflictPercentageAllowed");
			OwaOptionStrings.stringIDs.Add(695525694U, "ImportanceDialogTitle");
			OwaOptionStrings.stringIDs.Add(265625760U, "SecurityGroupText");
			OwaOptionStrings.stringIDs.Add(1528543719U, "SubjectContainsConditionFormat");
			OwaOptionStrings.stringIDs.Add(1566070952U, "VoicemailStep1");
			OwaOptionStrings.stringIDs.Add(1472234710U, "ImportContactListPage1Step1");
			OwaOptionStrings.stringIDs.Add(2919653011U, "CalendarPublishingRangeTo");
			OwaOptionStrings.stringIDs.Add(924528516U, "Installed");
			OwaOptionStrings.stringIDs.Add(2532105814U, "JoinRestrictionClosedDetails");
			OwaOptionStrings.stringIDs.Add(3686719114U, "EnterNumberStepMessage");
			OwaOptionStrings.stringIDs.Add(3769321652U, "TeamMailboxString");
			OwaOptionStrings.stringIDs.Add(1116185611U, "ExternalAudienceCheckboxText");
			OwaOptionStrings.stringIDs.Add(3758567081U, "RetentionActionNeverDelete");
			OwaOptionStrings.stringIDs.Add(3601061204U, "TeamMailboxOwnersString");
			OwaOptionStrings.stringIDs.Add(2454510675U, "SentOnlyToMeConditionFormat");
			OwaOptionStrings.stringIDs.Add(685553614U, "PostalCodeLabel");
			OwaOptionStrings.stringIDs.Add(2399940313U, "StreetAddressLabel");
			OwaOptionStrings.stringIDs.Add(1949934660U, "ModerationNotificationsNever");
			OwaOptionStrings.stringIDs.Add(3043881226U, "AutoAddSignature");
			OwaOptionStrings.stringIDs.Add(362303575U, "VoicemailConfiguration");
			OwaOptionStrings.stringIDs.Add(1231202734U, "TeamMailboxUsersString");
			OwaOptionStrings.stringIDs.Add(4116169389U, "Minutes");
			OwaOptionStrings.stringIDs.Add(1475482257U, "TextMessagingStatusPrefixNotifications");
			OwaOptionStrings.stringIDs.Add(947054436U, "OfficeLabel");
			OwaOptionStrings.stringIDs.Add(3570705387U, "SetupCalendarNotificationsLink");
			OwaOptionStrings.stringIDs.Add(2829325136U, "WipeDeviceConfirmMessage");
			OwaOptionStrings.stringIDs.Add(1970261471U, "JunkEmailEnabled");
			OwaOptionStrings.stringIDs.Add(2091217323U, "ProfilePhoto");
			OwaOptionStrings.stringIDs.Add(1213316404U, "JoinRestriction");
			OwaOptionStrings.stringIDs.Add(1603898988U, "SubscriptionAccountInformation");
			OwaOptionStrings.stringIDs.Add(1983970360U, "PopSubscription");
			OwaOptionStrings.stringIDs.Add(1651913438U, "ConnectedAccountsDescriptionForSendAs");
			OwaOptionStrings.stringIDs.Add(1355905147U, "VoicemailNotConfiguredText");
			OwaOptionStrings.stringIDs.Add(2328219770U, "Date");
			OwaOptionStrings.stringIDs.Add(2118902877U, "SubscriptionEmailAddress");
			OwaOptionStrings.stringIDs.Add(1262918758U, "CalendarAppearanceInstruction");
			OwaOptionStrings.stringIDs.Add(779604653U, "DisableReminders");
			OwaOptionStrings.stringIDs.Add(58190828U, "UninstallExtensionConfirmation");
			OwaOptionStrings.stringIDs.Add(272167933U, "ReadReceiptResponseAskBefore");
			OwaOptionStrings.stringIDs.Add(2721707952U, "AutomaticRepliesDisabledText");
			OwaOptionStrings.stringIDs.Add(2824009732U, "PermissionGranted");
			OwaOptionStrings.stringIDs.Add(473555910U, "CalendarTroubleShootingSlab");
			OwaOptionStrings.stringIDs.Add(3318664764U, "QLRemotePowerShell");
			OwaOptionStrings.stringIDs.Add(3266331791U, "Ownership");
			OwaOptionStrings.stringIDs.Add(1330376901U, "DevicePhoneNumberHeaderText");
			OwaOptionStrings.stringIDs.Add(1467122827U, "OwnerApprovalRequired");
			OwaOptionStrings.stringIDs.Add(936255600U, "AddExtensionTitle");
			OwaOptionStrings.stringIDs.Add(2172431100U, "DevicePolicyApplicationStatusLabel");
			OwaOptionStrings.stringIDs.Add(3387549369U, "CalendarWorkflowSlab");
			OwaOptionStrings.stringIDs.Add(1249796258U, "SettingNotAvailable");
			OwaOptionStrings.stringIDs.Add(1244143874U, "DepartRestriction");
			OwaOptionStrings.stringIDs.Add(3089991175U, "RetentionTypeRequiredDescription");
			OwaOptionStrings.stringIDs.Add(4116953573U, "RemoveForwardedMeetingNotificationsCheckBoxText");
			OwaOptionStrings.stringIDs.Add(3708929833U, "Everyone");
			OwaOptionStrings.stringIDs.Add(3452175682U, "TeamMailboxAppTitle");
			OwaOptionStrings.stringIDs.Add(2525011123U, "PersonalSettingConfirmPassword");
			OwaOptionStrings.stringIDs.Add(2696088292U, "MailboxUsageUnitMB");
			OwaOptionStrings.stringIDs.Add(4155090378U, "SubscriptionServerInformation");
			OwaOptionStrings.stringIDs.Add(2385969192U, "UserNameLabel");
			OwaOptionStrings.stringIDs.Add(776327314U, "TimeIncrementFifteenMinutes");
			OwaOptionStrings.stringIDs.Add(2051542189U, "LastSuccessfulSync");
			OwaOptionStrings.stringIDs.Add(4052999952U, "SchedulingPermissionsInstruction");
			OwaOptionStrings.stringIDs.Add(1517497942U, "EmptyDeletedItemsOnLogoff");
			OwaOptionStrings.stringIDs.Add(2251065475U, "BeforeDateDisplayTemplate");
			OwaOptionStrings.stringIDs.Add(2121350552U, "EmailAddressLabel");
			OwaOptionStrings.stringIDs.Add(1895146354U, "JunkEmailBlockedListDescription");
			OwaOptionStrings.stringIDs.Add(3334007327U, "NewItemNotificationSound");
			OwaOptionStrings.stringIDs.Add(272267124U, "NewInboxRuleCaption");
			OwaOptionStrings.stringIDs.Add(1462746207U, "UpdateTimeZoneNoteLinkText");
			OwaOptionStrings.stringIDs.Add(5631064U, "HasClassificationConditionFormat");
			OwaOptionStrings.stringIDs.Add(705899704U, "NoneAccessRightRole");
			OwaOptionStrings.stringIDs.Add(3116350389U, "MobileDeviceDetailTitle");
			OwaOptionStrings.stringIDs.Add(624738140U, "EditCommandText");
			OwaOptionStrings.stringIDs.Add(3623666006U, "DeviceTypeLabel");
			OwaOptionStrings.stringIDs.Add(4012923742U, "AllowConflicts");
			OwaOptionStrings.stringIDs.Add(3458491959U, "CalendarPublishing");
			OwaOptionStrings.stringIDs.Add(2717011154U, "RetentionNameHeader");
			OwaOptionStrings.stringIDs.Add(1036626U, "ImportContactListPage1InformationText");
			OwaOptionStrings.stringIDs.Add(3055482209U, "VoicemailAskPhoneNumber");
			OwaOptionStrings.stringIDs.Add(453080500U, "TeamMailboxDocumentsString");
			OwaOptionStrings.stringIDs.Add(3751262095U, "CountryOrRegionLabel");
			OwaOptionStrings.stringIDs.Add(1816995256U, "CalendarPublishingLearnMore");
			OwaOptionStrings.stringIDs.Add(1989730689U, "GroupModeratedBy");
			OwaOptionStrings.stringIDs.Add(4168470247U, "DidntReceivePasscodeMessage");
			OwaOptionStrings.stringIDs.Add(3197162109U, "InboxRuleFromAddressContainsConditionText");
			OwaOptionStrings.stringIDs.Add(1889609004U, "IncomingAuthenticationLabel");
			OwaOptionStrings.stringIDs.Add(2280348446U, "InboxRuleRecipientAddressContainsConditionText");
			OwaOptionStrings.stringIDs.Add(1975661589U, "DepartGroupsConfirmation");
			OwaOptionStrings.stringIDs.Add(2084354126U, "CalendarAppearanceSlab");
			OwaOptionStrings.stringIDs.Add(2908173103U, "InitialsLabel");
			OwaOptionStrings.stringIDs.Add(3446713793U, "InboxRuleFlaggedForActionConditionText");
			OwaOptionStrings.stringIDs.Add(3191690622U, "QuickLinks");
			OwaOptionStrings.stringIDs.Add(3789018700U, "TeamMailboxMyRoleString2");
			OwaOptionStrings.stringIDs.Add(2359136379U, "RoomEmailAddressLabel");
			OwaOptionStrings.stringIDs.Add(1233816147U, "ToColumnLabel");
			OwaOptionStrings.stringIDs.Add(1628295023U, "SensitivityDialogLabel");
			OwaOptionStrings.stringIDs.Add(3728295286U, "AllowedSendersEmptyLabelForEndUser");
			OwaOptionStrings.stringIDs.Add(3699558192U, "RequirementsReadWriteMailboxValue");
			OwaOptionStrings.stringIDs.Add(2323655060U, "FlagStatusDialogLabel");
			OwaOptionStrings.stringIDs.Add(993324455U, "ImapSetting");
			OwaOptionStrings.stringIDs.Add(1286082276U, "VoicemailConfiguredText");
			OwaOptionStrings.stringIDs.Add(517504798U, "JunkEmailTrustedListHeader");
			OwaOptionStrings.stringIDs.Add(1255618141U, "RequirementsReadItemDescription");
			OwaOptionStrings.stringIDs.Add(311875894U, "RequireSenderAuth");
			OwaOptionStrings.stringIDs.Add(2656242830U, "IWantToEditMyNotificationSettings");
			OwaOptionStrings.stringIDs.Add(4223818683U, "DevicePolicyAppliedLabel");
			OwaOptionStrings.stringIDs.Add(980863707U, "VoicemailSMSOptionNone");
			OwaOptionStrings.stringIDs.Add(4218310877U, "EditDistributionGroupTitle");
			OwaOptionStrings.stringIDs.Add(1540907116U, "MaximumDurationInMinutes");
			OwaOptionStrings.stringIDs.Add(3482669506U, "NewPopSubscription");
			OwaOptionStrings.stringIDs.Add(3896686788U, "MobileDeviceHeadNoteInfo");
			OwaOptionStrings.stringIDs.Add(4151878805U, "HomePhoneLabel");
			OwaOptionStrings.stringIDs.Add(2680900463U, "BlockDeviceCommandText");
			OwaOptionStrings.stringIDs.Add(572559498U, "SelectUsers");
			OwaOptionStrings.stringIDs.Add(3849562096U, "SearchGroupsButtonDescription");
			OwaOptionStrings.stringIDs.Add(382247602U, "DeleteEmailSubscriptionsConfirmation");
			OwaOptionStrings.stringIDs.Add(844022153U, "LearnHowToUseRedirectTo");
			OwaOptionStrings.stringIDs.Add(1008265668U, "InboxRuleDeleteMessageActionFlyOutText");
			OwaOptionStrings.stringIDs.Add(187395633U, "RetentionExplanationLabel");
			OwaOptionStrings.stringIDs.Add(2136591767U, "MessageTrackingPendingEvent");
			OwaOptionStrings.stringIDs.Add(773553315U, "ProviderColumn");
			OwaOptionStrings.stringIDs.Add(718036160U, "SettingAccessDisabled");
			OwaOptionStrings.stringIDs.Add(1339697241U, "RPTDays");
			OwaOptionStrings.stringIDs.Add(433722170U, "InboxRuleFromSubscriptionConditionFlyOutText");
			OwaOptionStrings.stringIDs.Add(2080149828U, "SmtpAddressExample");
			OwaOptionStrings.stringIDs.Add(759967779U, "InboxRuleMarkImportanceActionFlyOutText");
			OwaOptionStrings.stringIDs.Add(3100350500U, "CategoryDialogLabel");
			OwaOptionStrings.stringIDs.Add(3531743005U, "DeviceAccessStateLabel");
			OwaOptionStrings.stringIDs.Add(854461327U, "JunkEmailBlockedListHeader");
			OwaOptionStrings.stringIDs.Add(3007171092U, "VoicemailNotAvailableText");
			OwaOptionStrings.stringIDs.Add(290121153U, "ContactLocationBookmark");
			OwaOptionStrings.stringIDs.Add(1437070541U, "InboxRuleSubjectContainsConditionText");
			OwaOptionStrings.stringIDs.Add(130154415U, "VoicemailWizardConfirmPinLabel");
			OwaOptionStrings.stringIDs.Add(1880859719U, "NoSubscriptionAvailable");
			OwaOptionStrings.stringIDs.Add(2399040354U, "QLPassword");
			OwaOptionStrings.stringIDs.Add(540558863U, "CustomAccessRightRole");
			OwaOptionStrings.stringIDs.Add(570023042U, "ClearSettings");
			OwaOptionStrings.stringIDs.Add(2696088286U, "MailboxUsageUnitKB");
			OwaOptionStrings.stringIDs.Add(3947650662U, "ExtensionPackageLocation");
			OwaOptionStrings.stringIDs.Add(3084881239U, "InboxRuleApplyCategoryActionText");
			OwaOptionStrings.stringIDs.Add(2226605436U, "OWAVoicemail");
			OwaOptionStrings.stringIDs.Add(853755201U, "PersonalSettingPassword");
			OwaOptionStrings.stringIDs.Add(726021877U, "SendDuringWorkHoursOnly");
			OwaOptionStrings.stringIDs.Add(677776990U, "AliasLabelForDataCenter");
			OwaOptionStrings.stringIDs.Add(1351408741U, "DeliverAndForward");
			OwaOptionStrings.stringIDs.Add(468777496U, "Language");
			OwaOptionStrings.stringIDs.Add(3918450461U, "NameAndAccountBookmark");
			OwaOptionStrings.stringIDs.Add(2977885649U, "SendMyPhoneColon");
			OwaOptionStrings.stringIDs.Add(2956146252U, "DateStyles");
			OwaOptionStrings.stringIDs.Add(4067290517U, "GroupsIBelongToDescription");
			OwaOptionStrings.stringIDs.Add(2161945740U, "StartTimeText");
			OwaOptionStrings.stringIDs.Add(91158280U, "RemoveCommandText");
			OwaOptionStrings.stringIDs.Add(1057459323U, "PersonalSettingChangePassword");
			OwaOptionStrings.stringIDs.Add(3598785721U, "PasswordLabel");
			OwaOptionStrings.stringIDs.Add(1986416405U, "InboxRuleHasAttachmentConditionText");
			OwaOptionStrings.stringIDs.Add(988028286U, "CalendarPublishingLinks");
			OwaOptionStrings.stringIDs.Add(3784078605U, "RequirementsReadWriteItemValue");
			OwaOptionStrings.stringIDs.Add(1710677402U, "ImportContactListProgress");
			OwaOptionStrings.stringIDs.Add(874684615U, "TrialReminderActionLinkText");
			OwaOptionStrings.stringIDs.Add(2040917585U, "VoiceMailNotificationsLink");
			OwaOptionStrings.stringIDs.Add(856915648U, "InboxRuleMyNameInToCcBoxConditionText");
			OwaOptionStrings.stringIDs.Add(2736378449U, "InstallFromFile");
			OwaOptionStrings.stringIDs.Add(2000126950U, "DeviceMOWAVersionLabel");
			OwaOptionStrings.stringIDs.Add(1732412034U, "Subject");
			OwaOptionStrings.stringIDs.Add(1266539147U, "EditAccountCommandText");
			OwaOptionStrings.stringIDs.Add(3491325124U, "AutomaticRepliesScheduledCheckboxText");
			OwaOptionStrings.stringIDs.Add(3616424413U, "RetentionActionTypeHeader");
			OwaOptionStrings.stringIDs.Add(3507358566U, "QLOutlook");
			OwaOptionStrings.stringIDs.Add(3300930250U, "InboxRuleFromConditionText");
			OwaOptionStrings.stringIDs.Add(582625299U, "MessageTrackingFailedEvent");
			OwaOptionStrings.stringIDs.Add(1068599355U, "IncomingSecurityTLS");
			OwaOptionStrings.stringIDs.Add(961444931U, "WithinSizeRangeConditionFormat");
			OwaOptionStrings.stringIDs.Add(2118875233U, "VoicemailCallFwdStep3");
			OwaOptionStrings.stringIDs.Add(3710804460U, "MyMailbox");
			OwaOptionStrings.stringIDs.Add(2534471784U, "CalendarPublishingDetail");
			OwaOptionStrings.stringIDs.Add(1753433743U, "DeviceLastSuccessfulSyncLabel");
			OwaOptionStrings.stringIDs.Add(1279019904U, "ClearButtonText");
			OwaOptionStrings.stringIDs.Add(1541555783U, "InboxRuleCopyToFolderActionText");
			OwaOptionStrings.stringIDs.Add(1806917741U, "RPTYearsMonths");
			OwaOptionStrings.stringIDs.Add(3714415956U, "FromConditionFormat");
			OwaOptionStrings.stringIDs.Add(908779566U, "DeviceUserAgentLabel");
			OwaOptionStrings.stringIDs.Add(2657380710U, "DepartGroupConfirmation");
			OwaOptionStrings.stringIDs.Add(2291011248U, "InboxRuleWithImportanceConditionText");
			OwaOptionStrings.stringIDs.Add(492355799U, "PersonalSettingDomainUser");
			OwaOptionStrings.stringIDs.Add(1452418752U, "SiteMailboxEmailMeDiagnosticsButtonString");
			OwaOptionStrings.stringIDs.Add(2174687123U, "DeliveryManagement");
			OwaOptionStrings.stringIDs.Add(1249600476U, "PersonalSettingPasswordBeforeChange");
			OwaOptionStrings.stringIDs.Add(1511367726U, "SmtpSetting");
			OwaOptionStrings.stringIDs.Add(1472422U, "MessageFormatSlab");
			OwaOptionStrings.stringIDs.Add(3994376130U, "InboxRuleForwardToActionFlyOutText");
			OwaOptionStrings.stringIDs.Add(3663958361U, "DuplicateProxyAddressError");
			OwaOptionStrings.stringIDs.Add(2209887359U, "CreatedBy");
			OwaOptionStrings.stringIDs.Add(2999683987U, "ReadReceiptResponseNever");
			OwaOptionStrings.stringIDs.Add(992729393U, "AccessDeniedFooterBottom");
			OwaOptionStrings.stringIDs.Add(900932107U, "OwnerChangedUpdateModerator");
			OwaOptionStrings.stringIDs.Add(1233400920U, "AllowRecurringMeetings");
			OwaOptionStrings.stringIDs.Add(508731823U, "VoicemailWizardStep1Title");
			OwaOptionStrings.stringIDs.Add(2265850499U, "TeamMailboxMembersString");
			OwaOptionStrings.stringIDs.Add(417105304U, "AfterDateDisplayTemplate");
			OwaOptionStrings.stringIDs.Add(1413204805U, "TuesdayCheckBoxText");
			OwaOptionStrings.stringIDs.Add(1367192090U, "Join");
			OwaOptionStrings.stringIDs.Add(2408804455U, "AddAdditionalResponse");
			OwaOptionStrings.stringIDs.Add(737660655U, "OkButtonText");
			OwaOptionStrings.stringIDs.Add(3175109089U, "FoldersSyncedLabel");
			OwaOptionStrings.stringIDs.Add(3789286439U, "SendToAllText");
			OwaOptionStrings.stringIDs.Add(3957821083U, "MaximumDurationInMinutesErrorMessage");
			OwaOptionStrings.stringIDs.Add(3260418603U, "TextMessagingTurnedOnViaEas");
			OwaOptionStrings.stringIDs.Add(3047217661U, "StartLoggingCommandText");
			OwaOptionStrings.stringIDs.Add(2811760173U, "MailboxUsageLegacyText");
			OwaOptionStrings.stringIDs.Add(1144080682U, "RPTYears");
			OwaOptionStrings.stringIDs.Add(143410217U, "ReadReceiptResponseInstruction");
			OwaOptionStrings.stringIDs.Add(636289130U, "RemindersEnabled");
			OwaOptionStrings.stringIDs.Add(1284433590U, "AddNewRequestsTentativelyCheckBoxText");
			OwaOptionStrings.stringIDs.Add(1865553596U, "MessageTrackingSubmitEvent");
			OwaOptionStrings.stringIDs.Add(1981955278U, "VoicemailLearnMore");
			OwaOptionStrings.stringIDs.Add(2288506116U, "EmailThisReport");
			OwaOptionStrings.stringIDs.Add(1435311110U, "CalendarPublishingDateRange");
			OwaOptionStrings.stringIDs.Add(3485844721U, "SelectUsersAndGroups");
			OwaOptionStrings.stringIDs.Add(1183459890U, "PhotoBookmark");
			OwaOptionStrings.stringIDs.Add(2440572518U, "InboxRuleHeaderContainsConditionText");
			OwaOptionStrings.stringIDs.Add(1356144580U, "BookingWindowInDaysErrorMessage");
			OwaOptionStrings.stringIDs.Add(1292798904U, "Calendar");
			OwaOptionStrings.stringIDs.Add(1152863492U, "RuleSubjectContainsAndMoveToFolderTemplate");
			OwaOptionStrings.stringIDs.Add(817924740U, "RuleStateOn");
			OwaOptionStrings.stringIDs.Add(3913001929U, "UserLogonNameLabel");
			OwaOptionStrings.stringIDs.Add(2426358350U, "RPTMonth");
			OwaOptionStrings.stringIDs.Add(1824826658U, "EndTime");
			OwaOptionStrings.stringIDs.Add(2917434001U, "InstallFromPrivateUrlTitle");
			OwaOptionStrings.stringIDs.Add(3131435199U, "LastSyncAttemptTimeHeaderText");
			OwaOptionStrings.stringIDs.Add(2572011012U, "RequirementsReadWriteItemDescription");
			OwaOptionStrings.stringIDs.Add(940101428U, "NewItemNotificationVoiceMailToast");
			OwaOptionStrings.stringIDs.Add(2853482896U, "RenameDefaultFoldersCheckBoxText");
			OwaOptionStrings.stringIDs.Add(1380850475U, "InboxRuleFromSubscriptionConditionText");
			OwaOptionStrings.stringIDs.Add(1440211402U, "RetentionSelectOptionalLabel");
			OwaOptionStrings.stringIDs.Add(1914666238U, "FromColumnLabel");
			OwaOptionStrings.stringIDs.Add(524522579U, "ExtensionCanNotBeDisabledNorUninstalled");
			OwaOptionStrings.stringIDs.Add(2157690544U, "CalendarPublishingPublic");
			OwaOptionStrings.stringIDs.Add(1380849357U, "CalendarSharingExplanation");
			OwaOptionStrings.stringIDs.Add(1525092013U, "CalendarPublishingViewUrl");
			OwaOptionStrings.stringIDs.Add(3391167931U, "SaturdayCheckBoxText");
			OwaOptionStrings.stringIDs.Add(1194500941U, "MailboxUsageUnitB");
			OwaOptionStrings.stringIDs.Add(2771220587U, "HasAttachmentConditionFormat");
			OwaOptionStrings.stringIDs.Add(2993887811U, "VoicemailWizardStep5Title");
			OwaOptionStrings.stringIDs.Add(3866819705U, "PreviewMarkAsReadBehaviorNever");
			OwaOptionStrings.stringIDs.Add(4136480461U, "SubscriptionDialogLabel");
			OwaOptionStrings.stringIDs.Add(1033615903U, "NewSubscription");
			OwaOptionStrings.stringIDs.Add(826260386U, "LastSynchronization");
			OwaOptionStrings.stringIDs.Add(1189779660U, "ChangeCalendarPermissions");
			OwaOptionStrings.stringIDs.Add(3183463582U, "DefaultReminderTimeLabel");
			OwaOptionStrings.stringIDs.Add(2947609428U, "OpenNextItem");
			OwaOptionStrings.stringIDs.Add(3213113944U, "EmailOptions");
			OwaOptionStrings.stringIDs.Add(461256164U, "RetentionActionTypeDefaultDelete");
			OwaOptionStrings.stringIDs.Add(3032482008U, "ChangeCommandText");
			OwaOptionStrings.stringIDs.Add(1054605049U, "ExternalMessageGalLessInstruction");
			OwaOptionStrings.stringIDs.Add(508072824U, "ImportContactListNoFileUploaded");
			OwaOptionStrings.stringIDs.Add(1294601601U, "BadOfficeCallbackMessage");
			OwaOptionStrings.stringIDs.Add(2511894476U, "DefaultImage");
			OwaOptionStrings.stringIDs.Add(393831119U, "JoinAndDepart");
			OwaOptionStrings.stringIDs.Add(3881794065U, "InboxRuleMyNameInToCcBoxConditionFlyOutText");
			OwaOptionStrings.stringIDs.Add(2202590644U, "TeamMailboxTitleString");
			OwaOptionStrings.stringIDs.Add(1746211700U, "NewestOnTop");
			OwaOptionStrings.stringIDs.Add(2730942909U, "ToWithExample");
			OwaOptionStrings.stringIDs.Add(1571878129U, "GroupOwners");
			OwaOptionStrings.stringIDs.Add(1456975951U, "DefaultRetentionTagDescription");
			OwaOptionStrings.stringIDs.Add(2811696355U, "Hours");
			OwaOptionStrings.stringIDs.Add(643576831U, "MessageTypeDialogTitle");
			OwaOptionStrings.stringIDs.Add(3392276543U, "SearchDeliveryReports");
			OwaOptionStrings.stringIDs.Add(2118875235U, "VoicemailCallFwdStep1");
			OwaOptionStrings.stringIDs.Add(4130400892U, "SendAddressSetting");
			OwaOptionStrings.stringIDs.Add(787898729U, "InboxRuleItIsGroupText");
			OwaOptionStrings.stringIDs.Add(1694176600U, "LaunchOfficeMarketplace");
			OwaOptionStrings.stringIDs.Add(534802423U, "CalendarDiagnosticLogDescription");
			OwaOptionStrings.stringIDs.Add(1084745363U, "InboxRules");
			OwaOptionStrings.stringIDs.Add(942329033U, "VoicemailSMSOptionVoiceMailAndMissedCalls");
			OwaOptionStrings.stringIDs.Add(2138448017U, "StartTime");
			OwaOptionStrings.stringIDs.Add(1074402166U, "TextMessagingSlabMessage");
			OwaOptionStrings.stringIDs.Add(1548729791U, "WipeDeviceConfirmMessageDetail");
			OwaOptionStrings.stringIDs.Add(2711159495U, "VoicemailWizardStep2DescriptionNoPasscode");
			OwaOptionStrings.stringIDs.Add(3410748733U, "DeleteInboxRulesConfirmation");
			OwaOptionStrings.stringIDs.Add(3782284348U, "CalendarNotificationsLink");
			OwaOptionStrings.stringIDs.Add(2245512691U, "InboxRuleHasClassificationConditionFlyOutText");
			OwaOptionStrings.stringIDs.Add(3223016465U, "CalendarWorkflowInstruction");
			OwaOptionStrings.stringIDs.Add(672959953U, "AutomaticRepliesInstruction");
			OwaOptionStrings.stringIDs.Add(3095426072U, "DeviceLanguageLabel");
			OwaOptionStrings.stringIDs.Add(137938150U, "MoveUp");
			OwaOptionStrings.stringIDs.Add(810469194U, "InstallButtonText");
			OwaOptionStrings.stringIDs.Add(3324278371U, "WithSensitivityConditionFormat");
			OwaOptionStrings.stringIDs.Add(434719229U, "VoicemailWizardPinLabel");
			OwaOptionStrings.stringIDs.Add(2560210044U, "MondayCheckBoxText");
			OwaOptionStrings.stringIDs.Add(984832422U, "WithImportanceConditionFormat");
			OwaOptionStrings.stringIDs.Add(2489355482U, "NewDistributionGroupText");
			OwaOptionStrings.stringIDs.Add(1716421970U, "CalendarReminderSlab");
			OwaOptionStrings.stringIDs.Add(2879758792U, "EmailAddressesLabel");
			OwaOptionStrings.stringIDs.Add(1512647751U, "PortLabel");
			OwaOptionStrings.stringIDs.Add(2405760124U, "SetupVoiceMailNotificationsLink");
			OwaOptionStrings.stringIDs.Add(1075596584U, "TextMessagingOff");
			OwaOptionStrings.stringIDs.Add(602937486U, "RuleSentToAndMoveToFolderTemplate");
			OwaOptionStrings.stringIDs.Add(1393180149U, "UserNameNotSetError");
			OwaOptionStrings.stringIDs.Add(1065442140U, "MailboxUsageWarningText");
			OwaOptionStrings.stringIDs.Add(2112942921U, "PopSetting");
			OwaOptionStrings.stringIDs.Add(4023555694U, "InboxRuleFromAddressContainsConditionFlyOutText");
			OwaOptionStrings.stringIDs.Add(1511584348U, "Options");
			OwaOptionStrings.stringIDs.Add(3474229968U, "EMailSignatureSlab");
			OwaOptionStrings.stringIDs.Add(3820898101U, "RetentionPeriodHoldFor");
			OwaOptionStrings.stringIDs.Add(1362687599U, "MaximumConflictInstancesErrorMessage");
			OwaOptionStrings.stringIDs.Add(3952282363U, "Organize");
			OwaOptionStrings.stringIDs.Add(4129994431U, "MessageTrackingDLExpandedEvent");
			OwaOptionStrings.stringIDs.Add(1620472852U, "VoicemailBrowserNotSupported");
			OwaOptionStrings.stringIDs.Add(1877028846U, "SendAddressSettingSlabDescription");
			OwaOptionStrings.stringIDs.Add(2765871439U, "RetrieveLogConfirmMessage");
			OwaOptionStrings.stringIDs.Add(460637234U, "DescriptionLabel");
			OwaOptionStrings.stringIDs.Add(3776153215U, "CustomAttributeDialogTitle");
			OwaOptionStrings.stringIDs.Add(527639998U, "MessageApproval");
			OwaOptionStrings.stringIDs.Add(931483573U, "TeamMailboxPropertiesString");
			OwaOptionStrings.stringIDs.Add(2650315815U, "BodyContainsConditionFormat");
			OwaOptionStrings.stringIDs.Add(3936874395U, "GroupModeration");
			OwaOptionStrings.stringIDs.Add(2556623818U, "FreeBusySubjectLocationInformation");
			OwaOptionStrings.stringIDs.Add(3000766872U, "InboxRuleMarkMessageGroupText");
			OwaOptionStrings.stringIDs.Add(2673767273U, "InboxRuleConditionSectionHeader");
			OwaOptionStrings.stringIDs.Add(3741421195U, "RecipientEmailButtonDescription");
			OwaOptionStrings.stringIDs.Add(1779041463U, "TimeZone");
			OwaOptionStrings.stringIDs.Add(286364476U, "InboxRuleSentToConditionFlyOutText");
			OwaOptionStrings.stringIDs.Add(3496306419U, "MoveDown");
			OwaOptionStrings.stringIDs.Add(105290154U, "UnblockDeviceCommandText");
			OwaOptionStrings.stringIDs.Add(3851792428U, "TextMessagingSms");
			OwaOptionStrings.stringIDs.Add(3338384120U, "LanguageInstruction");
			OwaOptionStrings.stringIDs.Add(2653201341U, "InboxRuleFromConditionFlyOutText");
			OwaOptionStrings.stringIDs.Add(773833243U, "OutOfOffice");
			OwaOptionStrings.stringIDs.Add(2449687995U, "FirstNameLabel");
			OwaOptionStrings.stringIDs.Add(271571182U, "InboxRuleBodyContainsConditionFlyOutText");
			OwaOptionStrings.stringIDs.Add(2979279075U, "AllowedSendersLabelForEndUser");
			OwaOptionStrings.stringIDs.Add(3600235422U, "VoicemailWizardStep2Title");
			OwaOptionStrings.stringIDs.Add(2483845882U, "ConversationsSlab");
			OwaOptionStrings.stringIDs.Add(3601145337U, "AutomaticRepliesSlab");
			OwaOptionStrings.stringIDs.Add(1137051325U, "IncomingAuthenticationNtlm");
			OwaOptionStrings.stringIDs.Add(1448959650U, "MobilePhoneLabel");
			OwaOptionStrings.stringIDs.Add(2211346257U, "DeviceNameLabel");
			OwaOptionStrings.stringIDs.Add(2946311768U, "HeaderContainsConditionFormat");
			OwaOptionStrings.stringIDs.Add(944969227U, "TeamMailboxDescription");
			OwaOptionStrings.stringIDs.Add(2267981074U, "EnterNumberClickNext");
			OwaOptionStrings.stringIDs.Add(3063130671U, "MobileDevices");
			OwaOptionStrings.stringIDs.Add(1188277387U, "DisplayRecoveryPasswordCommandDescription");
			OwaOptionStrings.stringIDs.Add(3451245278U, "AtMostOnlyDisplayTemplate");
			OwaOptionStrings.stringIDs.Add(777656741U, "InboxRuleSentToConditionPreCannedText");
			OwaOptionStrings.stringIDs.Add(2303439412U, "InboxRuleMarkedWithGroupText");
			OwaOptionStrings.stringIDs.Add(3455154594U, "Membership");
			OwaOptionStrings.stringIDs.Add(31525559U, "VoicemailSlab");
			OwaOptionStrings.stringIDs.Add(2801441113U, "AllInformation");
			OwaOptionStrings.stringIDs.Add(3723544685U, "BlockOrAllow");
			OwaOptionStrings.stringIDs.Add(1716779644U, "CalendarDiagnosticLogWatermarkText");
			OwaOptionStrings.stringIDs.Add(2601540438U, "EditGroups");
			OwaOptionStrings.stringIDs.Add(3335990695U, "TurnOnTextMessaging");
			OwaOptionStrings.stringIDs.Add(1418080636U, "AliasLabelForEnterprise");
			OwaOptionStrings.stringIDs.Add(3019822456U, "CategoryDialogTitle");
			OwaOptionStrings.stringIDs.Add(1806349341U, "SetYourWorkingHours");
			OwaOptionStrings.stringIDs.Add(3830590072U, "ProfileMailboxUsage");
			OwaOptionStrings.stringIDs.Add(1985922850U, "AtLeastAtMostDisplayTemplate");
			OwaOptionStrings.stringIDs.Add(1502911297U, "NotificationsForMeetingReminders");
			OwaOptionStrings.stringIDs.Add(1650094581U, "IncomingServerLabel");
			OwaOptionStrings.stringIDs.Add(3052975740U, "IncomingAuthenticationNone");
			OwaOptionStrings.stringIDs.Add(1925535397U, "TrialReminderText");
			OwaOptionStrings.stringIDs.Add(783706065U, "GroupsIBelongToAndGroupsIOwnDescription");
		}

		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000002 RID: 2 RVA: 0x0000629B File Offset: 0x0000449B
		public static LocalizedString NeverSyncText
		{
			get
			{
				return new LocalizedString("NeverSyncText", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000003 RID: 3 RVA: 0x000062B9 File Offset: 0x000044B9
		public static LocalizedString FromAddressContainsConditionFormat
		{
			get
			{
				return new LocalizedString("FromAddressContainsConditionFormat", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000004 RID: 4 RVA: 0x000062D7 File Offset: 0x000044D7
		public static LocalizedString CalendarPublishingBasic
		{
			get
			{
				return new LocalizedString("CalendarPublishingBasic", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000005 RID: 5 RVA: 0x000062F5 File Offset: 0x000044F5
		public static LocalizedString ChangePhoneNumber
		{
			get
			{
				return new LocalizedString("ChangePhoneNumber", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000006 RID: 6 RVA: 0x00006314 File Offset: 0x00004514
		public static LocalizedString SetSubscriptionSucceed(string feedback)
		{
			return new LocalizedString("SetSubscriptionSucceed", "", false, false, OwaOptionStrings.ResourceManager, new object[]
			{
				feedback
			});
		}

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x06000007 RID: 7 RVA: 0x00006343 File Offset: 0x00004543
		public static LocalizedString TimeZoneNote
		{
			get
			{
				return new LocalizedString("TimeZoneNote", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x06000008 RID: 8 RVA: 0x00006361 File Offset: 0x00004561
		public static LocalizedString ShowWorkWeekAsCheckBoxText
		{
			get
			{
				return new LocalizedString("ShowWorkWeekAsCheckBoxText", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x06000009 RID: 9 RVA: 0x0000637F File Offset: 0x0000457F
		public static LocalizedString ViewInboxRule
		{
			get
			{
				return new LocalizedString("ViewInboxRule", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x0600000A RID: 10 RVA: 0x0000639D File Offset: 0x0000459D
		public static LocalizedString DeviceMobileOperatorLabel
		{
			get
			{
				return new LocalizedString("DeviceMobileOperatorLabel", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x0600000B RID: 11 RVA: 0x000063BB File Offset: 0x000045BB
		public static LocalizedString FinishButtonText
		{
			get
			{
				return new LocalizedString("FinishButtonText", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x0600000C RID: 12 RVA: 0x000063D9 File Offset: 0x000045D9
		public static LocalizedString UserNameMOSIDLabel
		{
			get
			{
				return new LocalizedString("UserNameMOSIDLabel", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x0600000D RID: 13 RVA: 0x000063F7 File Offset: 0x000045F7
		public static LocalizedString ChangeMyMobilePhoneSettings
		{
			get
			{
				return new LocalizedString("ChangeMyMobilePhoneSettings", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x0600000E RID: 14 RVA: 0x00006415 File Offset: 0x00004615
		public static LocalizedString RequirementsReadWriteMailboxDescription
		{
			get
			{
				return new LocalizedString("RequirementsReadWriteMailboxDescription", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x0600000F RID: 15 RVA: 0x00006433 File Offset: 0x00004633
		public static LocalizedString MessageTypeMatchesConditionFormat
		{
			get
			{
				return new LocalizedString("MessageTypeMatchesConditionFormat", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x06000010 RID: 16 RVA: 0x00006451 File Offset: 0x00004651
		public static LocalizedString NewRoomCreationWarningText
		{
			get
			{
				return new LocalizedString("NewRoomCreationWarningText", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x06000011 RID: 17 RVA: 0x0000646F File Offset: 0x0000466F
		public static LocalizedString InboxRuleMyNameInToBoxConditionFlyOutText
		{
			get
			{
				return new LocalizedString("InboxRuleMyNameInToBoxConditionFlyOutText", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x06000012 RID: 18 RVA: 0x0000648D File Offset: 0x0000468D
		public static LocalizedString FirstSyncOnLabel
		{
			get
			{
				return new LocalizedString("FirstSyncOnLabel", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x06000013 RID: 19 RVA: 0x000064AB File Offset: 0x000046AB
		public static LocalizedString DeleteGroupConfirmation
		{
			get
			{
				return new LocalizedString("DeleteGroupConfirmation", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x06000014 RID: 20 RVA: 0x000064C9 File Offset: 0x000046C9
		public static LocalizedString PendingWipeCommandIssuedLabel
		{
			get
			{
				return new LocalizedString("PendingWipeCommandIssuedLabel", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x06000015 RID: 21 RVA: 0x000064E7 File Offset: 0x000046E7
		public static LocalizedString OwnerChangedModerationReminder
		{
			get
			{
				return new LocalizedString("OwnerChangedModerationReminder", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x06000016 RID: 22 RVA: 0x00006505 File Offset: 0x00004705
		public static LocalizedString InboxRuleFromConditionPreCannedText
		{
			get
			{
				return new LocalizedString("InboxRuleFromConditionPreCannedText", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x06000017 RID: 23 RVA: 0x00006523 File Offset: 0x00004723
		public static LocalizedString MaximumConflictInstances
		{
			get
			{
				return new LocalizedString("MaximumConflictInstances", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x06000018 RID: 24 RVA: 0x00006541 File Offset: 0x00004741
		public static LocalizedString EmailSubscriptions
		{
			get
			{
				return new LocalizedString("EmailSubscriptions", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x06000019 RID: 25 RVA: 0x0000655F File Offset: 0x0000475F
		public static LocalizedString InboxRuleFlaggedForActionConditionFlyOutText
		{
			get
			{
				return new LocalizedString("InboxRuleFlaggedForActionConditionFlyOutText", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x0600001A RID: 26 RVA: 0x0000657D File Offset: 0x0000477D
		public static LocalizedString ViewRPTDurationLabel
		{
			get
			{
				return new LocalizedString("ViewRPTDurationLabel", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x0600001B RID: 27 RVA: 0x0000659B File Offset: 0x0000479B
		public static LocalizedString OnOffColumn
		{
			get
			{
				return new LocalizedString("OnOffColumn", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x0600001C RID: 28 RVA: 0x000065B9 File Offset: 0x000047B9
		public static LocalizedString FromSubscriptionConditionFormat
		{
			get
			{
				return new LocalizedString("FromSubscriptionConditionFormat", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x0600001D RID: 29 RVA: 0x000065D7 File Offset: 0x000047D7
		public static LocalizedString EmailComposeModeSeparateForm
		{
			get
			{
				return new LocalizedString("EmailComposeModeSeparateForm", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x0600001E RID: 30 RVA: 0x000065F5 File Offset: 0x000047F5
		public static LocalizedString ReadingPaneSlab
		{
			get
			{
				return new LocalizedString("ReadingPaneSlab", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700001D RID: 29
		// (get) Token: 0x0600001F RID: 31 RVA: 0x00006613 File Offset: 0x00004813
		public static LocalizedString OOF
		{
			get
			{
				return new LocalizedString("OOF", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700001E RID: 30
		// (get) Token: 0x06000020 RID: 32 RVA: 0x00006631 File Offset: 0x00004831
		public static LocalizedString SearchResultsCaption
		{
			get
			{
				return new LocalizedString("SearchResultsCaption", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700001F RID: 31
		// (get) Token: 0x06000021 RID: 33 RVA: 0x0000664F File Offset: 0x0000484F
		public static LocalizedString SubjectLabel
		{
			get
			{
				return new LocalizedString("SubjectLabel", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000020 RID: 32
		// (get) Token: 0x06000022 RID: 34 RVA: 0x0000666D File Offset: 0x0000486D
		public static LocalizedString Minute
		{
			get
			{
				return new LocalizedString("Minute", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000021 RID: 33
		// (get) Token: 0x06000023 RID: 35 RVA: 0x0000668B File Offset: 0x0000488B
		public static LocalizedString SendAtColon
		{
			get
			{
				return new LocalizedString("SendAtColon", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000022 RID: 34
		// (get) Token: 0x06000024 RID: 36 RVA: 0x000066A9 File Offset: 0x000048A9
		public static LocalizedString NewCommandText
		{
			get
			{
				return new LocalizedString("NewCommandText", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000023 RID: 35
		// (get) Token: 0x06000025 RID: 37 RVA: 0x000066C7 File Offset: 0x000048C7
		public static LocalizedString RetentionTypeRequired
		{
			get
			{
				return new LocalizedString("RetentionTypeRequired", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000024 RID: 36
		// (get) Token: 0x06000026 RID: 38 RVA: 0x000066E5 File Offset: 0x000048E5
		public static LocalizedString UninstallExtensionsConfirmation
		{
			get
			{
				return new LocalizedString("UninstallExtensionsConfirmation", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000025 RID: 37
		// (get) Token: 0x06000027 RID: 39 RVA: 0x00006703 File Offset: 0x00004903
		public static LocalizedString InboxRuleHasAttachmentConditionFlyOutText
		{
			get
			{
				return new LocalizedString("InboxRuleHasAttachmentConditionFlyOutText", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000026 RID: 38
		// (get) Token: 0x06000028 RID: 40 RVA: 0x00006721 File Offset: 0x00004921
		public static LocalizedString MyselfEntFormat
		{
			get
			{
				return new LocalizedString("MyselfEntFormat", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000027 RID: 39
		// (get) Token: 0x06000029 RID: 41 RVA: 0x0000673F File Offset: 0x0000493F
		public static LocalizedString ConnectedAccounts
		{
			get
			{
				return new LocalizedString("ConnectedAccounts", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000028 RID: 40
		// (get) Token: 0x0600002A RID: 42 RVA: 0x0000675D File Offset: 0x0000495D
		public static LocalizedString GroupNotes
		{
			get
			{
				return new LocalizedString("GroupNotes", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000029 RID: 41
		// (get) Token: 0x0600002B RID: 43 RVA: 0x0000677B File Offset: 0x0000497B
		public static LocalizedString Status
		{
			get
			{
				return new LocalizedString("Status", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700002A RID: 42
		// (get) Token: 0x0600002C RID: 44 RVA: 0x00006799 File Offset: 0x00004999
		public static LocalizedString TeamMailboxSyncStatusString
		{
			get
			{
				return new LocalizedString("TeamMailboxSyncStatusString", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700002B RID: 43
		// (get) Token: 0x0600002D RID: 45 RVA: 0x000067B7 File Offset: 0x000049B7
		public static LocalizedString AccountSecondaryNavigation
		{
			get
			{
				return new LocalizedString("AccountSecondaryNavigation", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700002C RID: 44
		// (get) Token: 0x0600002E RID: 46 RVA: 0x000067D5 File Offset: 0x000049D5
		public static LocalizedString EmailSubscriptionSlabDescription
		{
			get
			{
				return new LocalizedString("EmailSubscriptionSlabDescription", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700002D RID: 45
		// (get) Token: 0x0600002F RID: 47 RVA: 0x000067F3 File Offset: 0x000049F3
		public static LocalizedString TeamMailboxMailString
		{
			get
			{
				return new LocalizedString("TeamMailboxMailString", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700002E RID: 46
		// (get) Token: 0x06000030 RID: 48 RVA: 0x00006811 File Offset: 0x00004A11
		public static LocalizedString TeamMailboxLifecycleStatusString2
		{
			get
			{
				return new LocalizedString("TeamMailboxLifecycleStatusString2", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000031 RID: 49 RVA: 0x00006830 File Offset: 0x00004A30
		public static LocalizedString ImportContactListPage2ResultNumber(int numContacts)
		{
			return new LocalizedString("ImportContactListPage2ResultNumber", "", false, false, OwaOptionStrings.ResourceManager, new object[]
			{
				numContacts
			});
		}

		// Token: 0x1700002F RID: 47
		// (get) Token: 0x06000032 RID: 50 RVA: 0x00006864 File Offset: 0x00004A64
		public static LocalizedString JunkEmailTrustedListDescription
		{
			get
			{
				return new LocalizedString("JunkEmailTrustedListDescription", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000030 RID: 48
		// (get) Token: 0x06000033 RID: 51 RVA: 0x00006882 File Offset: 0x00004A82
		public static LocalizedString SundayCheckBoxText
		{
			get
			{
				return new LocalizedString("SundayCheckBoxText", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000031 RID: 49
		// (get) Token: 0x06000034 RID: 52 RVA: 0x000068A0 File Offset: 0x00004AA0
		public static LocalizedString ExtensionVersionTag
		{
			get
			{
				return new LocalizedString("ExtensionVersionTag", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000032 RID: 50
		// (get) Token: 0x06000035 RID: 53 RVA: 0x000068BE File Offset: 0x00004ABE
		public static LocalizedString MailTip
		{
			get
			{
				return new LocalizedString("MailTip", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000033 RID: 51
		// (get) Token: 0x06000036 RID: 54 RVA: 0x000068DC File Offset: 0x00004ADC
		public static LocalizedString CalendarPublishingRestricted
		{
			get
			{
				return new LocalizedString("CalendarPublishingRestricted", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000037 RID: 55 RVA: 0x000068FC File Offset: 0x00004AFC
		public static LocalizedString JoinDlsSuccess(int num)
		{
			return new LocalizedString("JoinDlsSuccess", "", false, false, OwaOptionStrings.ResourceManager, new object[]
			{
				num
			});
		}

		// Token: 0x17000034 RID: 52
		// (get) Token: 0x06000038 RID: 56 RVA: 0x00006930 File Offset: 0x00004B30
		public static LocalizedString MailboxUsageUnavailable
		{
			get
			{
				return new LocalizedString("MailboxUsageUnavailable", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000035 RID: 53
		// (get) Token: 0x06000039 RID: 57 RVA: 0x0000694E File Offset: 0x00004B4E
		public static LocalizedString Customize
		{
			get
			{
				return new LocalizedString("Customize", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000036 RID: 54
		// (get) Token: 0x0600003A RID: 58 RVA: 0x0000696C File Offset: 0x00004B6C
		public static LocalizedString ModerationEnabled
		{
			get
			{
				return new LocalizedString("ModerationEnabled", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000037 RID: 55
		// (get) Token: 0x0600003B RID: 59 RVA: 0x0000698A File Offset: 0x00004B8A
		public static LocalizedString PreviewMarkAsReadBehaviorDelayed
		{
			get
			{
				return new LocalizedString("PreviewMarkAsReadBehaviorDelayed", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000038 RID: 56
		// (get) Token: 0x0600003C RID: 60 RVA: 0x000069A8 File Offset: 0x00004BA8
		public static LocalizedString ShareInformation
		{
			get
			{
				return new LocalizedString("ShareInformation", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000039 RID: 57
		// (get) Token: 0x0600003D RID: 61 RVA: 0x000069C6 File Offset: 0x00004BC6
		public static LocalizedString RetentionActionTypeArchive
		{
			get
			{
				return new LocalizedString("RetentionActionTypeArchive", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700003A RID: 58
		// (get) Token: 0x0600003E RID: 62 RVA: 0x000069E4 File Offset: 0x00004BE4
		public static LocalizedString SetUpNotifications
		{
			get
			{
				return new LocalizedString("SetUpNotifications", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700003B RID: 59
		// (get) Token: 0x0600003F RID: 63 RVA: 0x00006A02 File Offset: 0x00004C02
		public static LocalizedString InboxRuleMoveToFolderActionFlyOutText
		{
			get
			{
				return new LocalizedString("InboxRuleMoveToFolderActionFlyOutText", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700003C RID: 60
		// (get) Token: 0x06000040 RID: 64 RVA: 0x00006A20 File Offset: 0x00004C20
		public static LocalizedString JunkEmailContactsTrusted
		{
			get
			{
				return new LocalizedString("JunkEmailContactsTrusted", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700003D RID: 61
		// (get) Token: 0x06000041 RID: 65 RVA: 0x00006A3E File Offset: 0x00004C3E
		public static LocalizedString TeamMailboxManagementString
		{
			get
			{
				return new LocalizedString("TeamMailboxManagementString", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700003E RID: 62
		// (get) Token: 0x06000042 RID: 66 RVA: 0x00006A5C File Offset: 0x00004C5C
		public static LocalizedString MessageTrackingTransferredEvent
		{
			get
			{
				return new LocalizedString("MessageTrackingTransferredEvent", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700003F RID: 63
		// (get) Token: 0x06000043 RID: 67 RVA: 0x00006A7A File Offset: 0x00004C7A
		public static LocalizedString SendToAllGalLessText
		{
			get
			{
				return new LocalizedString("SendToAllGalLessText", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000040 RID: 64
		// (get) Token: 0x06000044 RID: 68 RVA: 0x00006A98 File Offset: 0x00004C98
		public static LocalizedString CalendarReminderInstruction
		{
			get
			{
				return new LocalizedString("CalendarReminderInstruction", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000041 RID: 65
		// (get) Token: 0x06000045 RID: 69 RVA: 0x00006AB6 File Offset: 0x00004CB6
		public static LocalizedString TotalMembers
		{
			get
			{
				return new LocalizedString("TotalMembers", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000042 RID: 66
		// (get) Token: 0x06000046 RID: 70 RVA: 0x00006AD4 File Offset: 0x00004CD4
		public static LocalizedString MailboxUsageUnlimitedText
		{
			get
			{
				return new LocalizedString("MailboxUsageUnlimitedText", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000043 RID: 67
		// (get) Token: 0x06000047 RID: 71 RVA: 0x00006AF2 File Offset: 0x00004CF2
		public static LocalizedString CalendarTroubleshootingLinkText
		{
			get
			{
				return new LocalizedString("CalendarTroubleshootingLinkText", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000044 RID: 68
		// (get) Token: 0x06000048 RID: 72 RVA: 0x00006B10 File Offset: 0x00004D10
		public static LocalizedString DisplayRecoveryPasswordCommandText
		{
			get
			{
				return new LocalizedString("DisplayRecoveryPasswordCommandText", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000045 RID: 69
		// (get) Token: 0x06000049 RID: 73 RVA: 0x00006B2E File Offset: 0x00004D2E
		public static LocalizedString VoicemailWizardStep4Description
		{
			get
			{
				return new LocalizedString("VoicemailWizardStep4Description", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000046 RID: 70
		// (get) Token: 0x0600004A RID: 74 RVA: 0x00006B4C File Offset: 0x00004D4C
		public static LocalizedString IncomingSecurityLabel
		{
			get
			{
				return new LocalizedString("IncomingSecurityLabel", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000047 RID: 71
		// (get) Token: 0x0600004B RID: 75 RVA: 0x00006B6A File Offset: 0x00004D6A
		public static LocalizedString InboxRuleForwardToActionText
		{
			get
			{
				return new LocalizedString("InboxRuleForwardToActionText", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000048 RID: 72
		// (get) Token: 0x0600004C RID: 76 RVA: 0x00006B88 File Offset: 0x00004D88
		public static LocalizedString InboxRuleMyNameIsGroupText
		{
			get
			{
				return new LocalizedString("InboxRuleMyNameIsGroupText", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000049 RID: 73
		// (get) Token: 0x0600004D RID: 77 RVA: 0x00006BA6 File Offset: 0x00004DA6
		public static LocalizedString MailboxFolderDialogLabel
		{
			get
			{
				return new LocalizedString("MailboxFolderDialogLabel", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700004A RID: 74
		// (get) Token: 0x0600004E RID: 78 RVA: 0x00006BC4 File Offset: 0x00004DC4
		public static LocalizedString ReturnToView
		{
			get
			{
				return new LocalizedString("ReturnToView", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700004B RID: 75
		// (get) Token: 0x0600004F RID: 79 RVA: 0x00006BE2 File Offset: 0x00004DE2
		public static LocalizedString DeviceActiveSyncVersionLabel
		{
			get
			{
				return new LocalizedString("DeviceActiveSyncVersionLabel", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700004C RID: 76
		// (get) Token: 0x06000050 RID: 80 RVA: 0x00006C00 File Offset: 0x00004E00
		public static LocalizedString InstallFromPrivateUrlCaption
		{
			get
			{
				return new LocalizedString("InstallFromPrivateUrlCaption", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700004D RID: 77
		// (get) Token: 0x06000051 RID: 81 RVA: 0x00006C1E File Offset: 0x00004E1E
		public static LocalizedString DeleteEmailSubscriptionConfirmation
		{
			get
			{
				return new LocalizedString("DeleteEmailSubscriptionConfirmation", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700004E RID: 78
		// (get) Token: 0x06000052 RID: 82 RVA: 0x00006C3C File Offset: 0x00004E3C
		public static LocalizedString VoicemailWizardComplete
		{
			get
			{
				return new LocalizedString("VoicemailWizardComplete", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700004F RID: 79
		// (get) Token: 0x06000053 RID: 83 RVA: 0x00006C5A File Offset: 0x00004E5A
		public static LocalizedString InboxRuleMarkAsReadActionFlyOutText
		{
			get
			{
				return new LocalizedString("InboxRuleMarkAsReadActionFlyOutText", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000050 RID: 80
		// (get) Token: 0x06000054 RID: 84 RVA: 0x00006C78 File Offset: 0x00004E78
		public static LocalizedString RPTDay
		{
			get
			{
				return new LocalizedString("RPTDay", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000051 RID: 81
		// (get) Token: 0x06000055 RID: 85 RVA: 0x00006C96 File Offset: 0x00004E96
		public static LocalizedString DeviceAccessStateSetByLabel
		{
			get
			{
				return new LocalizedString("DeviceAccessStateSetByLabel", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000052 RID: 82
		// (get) Token: 0x06000056 RID: 86 RVA: 0x00006CB4 File Offset: 0x00004EB4
		public static LocalizedString ViewGroupDetails
		{
			get
			{
				return new LocalizedString("ViewGroupDetails", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000053 RID: 83
		// (get) Token: 0x06000057 RID: 87 RVA: 0x00006CD2 File Offset: 0x00004ED2
		public static LocalizedString ToOnlyLabel
		{
			get
			{
				return new LocalizedString("ToOnlyLabel", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000054 RID: 84
		// (get) Token: 0x06000058 RID: 88 RVA: 0x00006CF0 File Offset: 0x00004EF0
		public static LocalizedString SensitivityDialogTitle
		{
			get
			{
				return new LocalizedString("SensitivityDialogTitle", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000055 RID: 85
		// (get) Token: 0x06000059 RID: 89 RVA: 0x00006D0E File Offset: 0x00004F0E
		public static LocalizedString TeamMailboxLifecycleStatusString
		{
			get
			{
				return new LocalizedString("TeamMailboxLifecycleStatusString", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000056 RID: 86
		// (get) Token: 0x0600005A RID: 90 RVA: 0x00006D2C File Offset: 0x00004F2C
		public static LocalizedString WednesdayCheckBoxText
		{
			get
			{
				return new LocalizedString("WednesdayCheckBoxText", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000057 RID: 87
		// (get) Token: 0x0600005B RID: 91 RVA: 0x00006D4A File Offset: 0x00004F4A
		public static LocalizedString ExtensionRequirementsLabel
		{
			get
			{
				return new LocalizedString("ExtensionRequirementsLabel", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000058 RID: 88
		// (get) Token: 0x0600005C RID: 92 RVA: 0x00006D68 File Offset: 0x00004F68
		public static LocalizedString AlwaysShowBcc
		{
			get
			{
				return new LocalizedString("AlwaysShowBcc", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000059 RID: 89
		// (get) Token: 0x0600005D RID: 93 RVA: 0x00006D86 File Offset: 0x00004F86
		public static LocalizedString ConflictPercentageAllowedErrorMessage
		{
			get
			{
				return new LocalizedString("ConflictPercentageAllowedErrorMessage", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700005A RID: 90
		// (get) Token: 0x0600005E RID: 94 RVA: 0x00006DA4 File Offset: 0x00004FA4
		public static LocalizedString JoinRestrictionApprovalRequiredDetails
		{
			get
			{
				return new LocalizedString("JoinRestrictionApprovalRequiredDetails", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700005B RID: 91
		// (get) Token: 0x0600005F RID: 95 RVA: 0x00006DC2 File Offset: 0x00004FC2
		public static LocalizedString InboxRuleMarkImportanceActionText
		{
			get
			{
				return new LocalizedString("InboxRuleMarkImportanceActionText", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700005C RID: 92
		// (get) Token: 0x06000060 RID: 96 RVA: 0x00006DE0 File Offset: 0x00004FE0
		public static LocalizedString InboxRuleRecipientAddressContainsConditionFlyOutText
		{
			get
			{
				return new LocalizedString("InboxRuleRecipientAddressContainsConditionFlyOutText", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700005D RID: 93
		// (get) Token: 0x06000061 RID: 97 RVA: 0x00006DFE File Offset: 0x00004FFE
		public static LocalizedString Regional
		{
			get
			{
				return new LocalizedString("Regional", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700005E RID: 94
		// (get) Token: 0x06000062 RID: 98 RVA: 0x00006E1C File Offset: 0x0000501C
		public static LocalizedString VoicemailWizardTestDoneText
		{
			get
			{
				return new LocalizedString("VoicemailWizardTestDoneText", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700005F RID: 95
		// (get) Token: 0x06000063 RID: 99 RVA: 0x00006E3A File Offset: 0x0000503A
		public static LocalizedString RemoveOldMeetingMessagesCheckBoxText
		{
			get
			{
				return new LocalizedString("RemoveOldMeetingMessagesCheckBoxText", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000060 RID: 96
		// (get) Token: 0x06000064 RID: 100 RVA: 0x00006E58 File Offset: 0x00005058
		public static LocalizedString InboxRuleBodyContainsConditionText
		{
			get
			{
				return new LocalizedString("InboxRuleBodyContainsConditionText", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000061 RID: 97
		// (get) Token: 0x06000065 RID: 101 RVA: 0x00006E76 File Offset: 0x00005076
		public static LocalizedString QLForward
		{
			get
			{
				return new LocalizedString("QLForward", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000062 RID: 98
		// (get) Token: 0x06000066 RID: 102 RVA: 0x00006E94 File Offset: 0x00005094
		public static LocalizedString VoicemailPhoneNumberColon
		{
			get
			{
				return new LocalizedString("VoicemailPhoneNumberColon", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000063 RID: 99
		// (get) Token: 0x06000067 RID: 103 RVA: 0x00006EB2 File Offset: 0x000050B2
		public static LocalizedString AddCommandText
		{
			get
			{
				return new LocalizedString("AddCommandText", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000064 RID: 100
		// (get) Token: 0x06000068 RID: 104 RVA: 0x00006ED0 File Offset: 0x000050D0
		public static LocalizedString Voicemail
		{
			get
			{
				return new LocalizedString("Voicemail", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000065 RID: 101
		// (get) Token: 0x06000069 RID: 105 RVA: 0x00006EEE File Offset: 0x000050EE
		public static LocalizedString StringArrayDialogTitle
		{
			get
			{
				return new LocalizedString("StringArrayDialogTitle", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000066 RID: 102
		// (get) Token: 0x0600006A RID: 106 RVA: 0x00006F0C File Offset: 0x0000510C
		public static LocalizedString MailboxUsageUnitTB
		{
			get
			{
				return new LocalizedString("MailboxUsageUnitTB", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000067 RID: 103
		// (get) Token: 0x0600006B RID: 107 RVA: 0x00006F2A File Offset: 0x0000512A
		public static LocalizedString CalendarPublishingCopyLink
		{
			get
			{
				return new LocalizedString("CalendarPublishingCopyLink", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000068 RID: 104
		// (get) Token: 0x0600006C RID: 108 RVA: 0x00006F48 File Offset: 0x00005148
		public static LocalizedString TimeStyles
		{
			get
			{
				return new LocalizedString("TimeStyles", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000069 RID: 105
		// (get) Token: 0x0600006D RID: 109 RVA: 0x00006F66 File Offset: 0x00005166
		public static LocalizedString RPTYear
		{
			get
			{
				return new LocalizedString("RPTYear", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700006A RID: 106
		// (get) Token: 0x0600006E RID: 110 RVA: 0x00006F84 File Offset: 0x00005184
		public static LocalizedString VoicemailLearnMoreVideo
		{
			get
			{
				return new LocalizedString("VoicemailLearnMoreVideo", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700006B RID: 107
		// (get) Token: 0x0600006F RID: 111 RVA: 0x00006FA2 File Offset: 0x000051A2
		public static LocalizedString InboxRuleHeaderContainsConditionFlyOutText
		{
			get
			{
				return new LocalizedString("InboxRuleHeaderContainsConditionFlyOutText", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700006C RID: 108
		// (get) Token: 0x06000070 RID: 112 RVA: 0x00006FC0 File Offset: 0x000051C0
		public static LocalizedString InboxRuleHasClassificationConditionText
		{
			get
			{
				return new LocalizedString("InboxRuleHasClassificationConditionText", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700006D RID: 109
		// (get) Token: 0x06000071 RID: 113 RVA: 0x00006FDE File Offset: 0x000051DE
		public static LocalizedString ImportContactListPage1Caption
		{
			get
			{
				return new LocalizedString("ImportContactListPage1Caption", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700006E RID: 110
		// (get) Token: 0x06000072 RID: 114 RVA: 0x00006FFC File Offset: 0x000051FC
		public static LocalizedString VoicemailWizardStep2Description
		{
			get
			{
				return new LocalizedString("VoicemailWizardStep2Description", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700006F RID: 111
		// (get) Token: 0x06000073 RID: 115 RVA: 0x0000701A File Offset: 0x0000521A
		public static LocalizedString AddExtension
		{
			get
			{
				return new LocalizedString("AddExtension", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000070 RID: 112
		// (get) Token: 0x06000074 RID: 116 RVA: 0x00007038 File Offset: 0x00005238
		public static LocalizedString WithinSizeRangeDialogTitle
		{
			get
			{
				return new LocalizedString("WithinSizeRangeDialogTitle", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000071 RID: 113
		// (get) Token: 0x06000075 RID: 117 RVA: 0x00007056 File Offset: 0x00005256
		public static LocalizedString GetCalendarLogButtonText
		{
			get
			{
				return new LocalizedString("GetCalendarLogButtonText", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000072 RID: 114
		// (get) Token: 0x06000076 RID: 118 RVA: 0x00007074 File Offset: 0x00005274
		public static LocalizedString BlockDeviceConfirmMessage
		{
			get
			{
				return new LocalizedString("BlockDeviceConfirmMessage", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000073 RID: 115
		// (get) Token: 0x06000077 RID: 119 RVA: 0x00007092 File Offset: 0x00005292
		public static LocalizedString CalendarPublishingRangeFrom
		{
			get
			{
				return new LocalizedString("CalendarPublishingRangeFrom", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000074 RID: 116
		// (get) Token: 0x06000078 RID: 120 RVA: 0x000070B0 File Offset: 0x000052B0
		public static LocalizedString EnterPasscodeStepMessage
		{
			get
			{
				return new LocalizedString("EnterPasscodeStepMessage", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000075 RID: 117
		// (get) Token: 0x06000079 RID: 121 RVA: 0x000070CE File Offset: 0x000052CE
		public static LocalizedString VoicemailCallFwdHavingTrouble
		{
			get
			{
				return new LocalizedString("VoicemailCallFwdHavingTrouble", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000076 RID: 118
		// (get) Token: 0x0600007A RID: 122 RVA: 0x000070EC File Offset: 0x000052EC
		public static LocalizedString StartForward
		{
			get
			{
				return new LocalizedString("StartForward", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000077 RID: 119
		// (get) Token: 0x0600007B RID: 123 RVA: 0x0000710A File Offset: 0x0000530A
		public static LocalizedString VoicemailCallFwdStep2
		{
			get
			{
				return new LocalizedString("VoicemailCallFwdStep2", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000078 RID: 120
		// (get) Token: 0x0600007C RID: 124 RVA: 0x00007128 File Offset: 0x00005328
		public static LocalizedString JoinRestrictionOpenDetails
		{
			get
			{
				return new LocalizedString("JoinRestrictionOpenDetails", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000079 RID: 121
		// (get) Token: 0x0600007D RID: 125 RVA: 0x00007146 File Offset: 0x00005346
		public static LocalizedString IncomingSecurityNone
		{
			get
			{
				return new LocalizedString("IncomingSecurityNone", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700007A RID: 122
		// (get) Token: 0x0600007E RID: 126 RVA: 0x00007164 File Offset: 0x00005364
		public static LocalizedString InboxRuleMyNameNotInToBoxConditionText
		{
			get
			{
				return new LocalizedString("InboxRuleMyNameNotInToBoxConditionText", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700007B RID: 123
		// (get) Token: 0x0600007F RID: 127 RVA: 0x00007182 File Offset: 0x00005382
		public static LocalizedString ChangePermissions
		{
			get
			{
				return new LocalizedString("ChangePermissions", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700007C RID: 124
		// (get) Token: 0x06000080 RID: 128 RVA: 0x000071A0 File Offset: 0x000053A0
		public static LocalizedString InboxRuleCopyToFolderActionFlyOutText
		{
			get
			{
				return new LocalizedString("InboxRuleCopyToFolderActionFlyOutText", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700007D RID: 125
		// (get) Token: 0x06000081 RID: 129 RVA: 0x000071BE File Offset: 0x000053BE
		public static LocalizedString InboxRuleSubjectContainsConditionFlyOutText
		{
			get
			{
				return new LocalizedString("InboxRuleSubjectContainsConditionFlyOutText", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700007E RID: 126
		// (get) Token: 0x06000082 RID: 130 RVA: 0x000071DC File Offset: 0x000053DC
		public static LocalizedString RequirementsRestrictedValue
		{
			get
			{
				return new LocalizedString("RequirementsRestrictedValue", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700007F RID: 127
		// (get) Token: 0x06000083 RID: 131 RVA: 0x000071FA File Offset: 0x000053FA
		public static LocalizedString InboxRuleRedirectToActionText
		{
			get
			{
				return new LocalizedString("InboxRuleRedirectToActionText", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000080 RID: 128
		// (get) Token: 0x06000084 RID: 132 RVA: 0x00007218 File Offset: 0x00005418
		public static LocalizedString ImportContactListPage1Step2
		{
			get
			{
				return new LocalizedString("ImportContactListPage1Step2", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000081 RID: 129
		// (get) Token: 0x06000085 RID: 133 RVA: 0x00007236 File Offset: 0x00005436
		public static LocalizedString JunkEmailWatermarkText
		{
			get
			{
				return new LocalizedString("JunkEmailWatermarkText", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000082 RID: 130
		// (get) Token: 0x06000086 RID: 134 RVA: 0x00007254 File Offset: 0x00005454
		public static LocalizedString TextMessagingStatusPrefixStatus
		{
			get
			{
				return new LocalizedString("TextMessagingStatusPrefixStatus", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000083 RID: 131
		// (get) Token: 0x06000087 RID: 135 RVA: 0x00007272 File Offset: 0x00005472
		public static LocalizedString ShowHoursIn
		{
			get
			{
				return new LocalizedString("ShowHoursIn", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000084 RID: 132
		// (get) Token: 0x06000088 RID: 136 RVA: 0x00007290 File Offset: 0x00005490
		public static LocalizedString DefaultFormat
		{
			get
			{
				return new LocalizedString("DefaultFormat", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000085 RID: 133
		// (get) Token: 0x06000089 RID: 137 RVA: 0x000072AE File Offset: 0x000054AE
		public static LocalizedString SubscriptionDialogTitle
		{
			get
			{
				return new LocalizedString("SubscriptionDialogTitle", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000086 RID: 134
		// (get) Token: 0x0600008A RID: 138 RVA: 0x000072CC File Offset: 0x000054CC
		public static LocalizedString NewItemNotificationEmailToast
		{
			get
			{
				return new LocalizedString("NewItemNotificationEmailToast", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000087 RID: 135
		// (get) Token: 0x0600008B RID: 139 RVA: 0x000072EA File Offset: 0x000054EA
		public static LocalizedString TeamMailboxTabUsersHelpString1
		{
			get
			{
				return new LocalizedString("TeamMailboxTabUsersHelpString1", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000088 RID: 136
		// (get) Token: 0x0600008C RID: 140 RVA: 0x00007308 File Offset: 0x00005508
		public static LocalizedString SchedulingPermissionsSlab
		{
			get
			{
				return new LocalizedString("SchedulingPermissionsSlab", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000089 RID: 137
		// (get) Token: 0x0600008D RID: 141 RVA: 0x00007326 File Offset: 0x00005526
		public static LocalizedString ConversationSortOrderInstruction
		{
			get
			{
				return new LocalizedString("ConversationSortOrderInstruction", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700008A RID: 138
		// (get) Token: 0x0600008E RID: 142 RVA: 0x00007344 File Offset: 0x00005544
		public static LocalizedString WipeDeviceCommandText
		{
			get
			{
				return new LocalizedString("WipeDeviceCommandText", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700008B RID: 139
		// (get) Token: 0x0600008F RID: 143 RVA: 0x00007362 File Offset: 0x00005562
		public static LocalizedString InboxRuleSentOrReceivedGroupText
		{
			get
			{
				return new LocalizedString("InboxRuleSentOrReceivedGroupText", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700008C RID: 140
		// (get) Token: 0x06000090 RID: 144 RVA: 0x00007380 File Offset: 0x00005580
		public static LocalizedString Myself
		{
			get
			{
				return new LocalizedString("Myself", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700008D RID: 141
		// (get) Token: 0x06000091 RID: 145 RVA: 0x0000739E File Offset: 0x0000559E
		public static LocalizedString NewestOnBottom
		{
			get
			{
				return new LocalizedString("NewestOnBottom", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700008E RID: 142
		// (get) Token: 0x06000092 RID: 146 RVA: 0x000073BC File Offset: 0x000055BC
		public static LocalizedString NewItemNotificationFaxToast
		{
			get
			{
				return new LocalizedString("NewItemNotificationFaxToast", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700008F RID: 143
		// (get) Token: 0x06000093 RID: 147 RVA: 0x000073DA File Offset: 0x000055DA
		public static LocalizedString EmailComposeModeInline
		{
			get
			{
				return new LocalizedString("EmailComposeModeInline", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000090 RID: 144
		// (get) Token: 0x06000094 RID: 148 RVA: 0x000073F8 File Offset: 0x000055F8
		public static LocalizedString NewRuleString
		{
			get
			{
				return new LocalizedString("NewRuleString", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000095 RID: 149 RVA: 0x00007418 File Offset: 0x00005618
		public static LocalizedString EditRuleCaption(string name)
		{
			return new LocalizedString("EditRuleCaption", "", false, false, OwaOptionStrings.ResourceManager, new object[]
			{
				name
			});
		}

		// Token: 0x17000091 RID: 145
		// (get) Token: 0x06000096 RID: 150 RVA: 0x00007447 File Offset: 0x00005647
		public static LocalizedString NoMessageCategoryAvailable
		{
			get
			{
				return new LocalizedString("NoMessageCategoryAvailable", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000092 RID: 146
		// (get) Token: 0x06000097 RID: 151 RVA: 0x00007465 File Offset: 0x00005665
		public static LocalizedString CurrentStatus
		{
			get
			{
				return new LocalizedString("CurrentStatus", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000093 RID: 147
		// (get) Token: 0x06000098 RID: 152 RVA: 0x00007483 File Offset: 0x00005683
		public static LocalizedString SubscriptionProcessingError
		{
			get
			{
				return new LocalizedString("SubscriptionProcessingError", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000094 RID: 148
		// (get) Token: 0x06000099 RID: 153 RVA: 0x000074A1 File Offset: 0x000056A1
		public static LocalizedString StopAndRetrieveLogCommandText
		{
			get
			{
				return new LocalizedString("StopAndRetrieveLogCommandText", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000095 RID: 149
		// (get) Token: 0x0600009A RID: 154 RVA: 0x000074BF File Offset: 0x000056BF
		public static LocalizedString TimeIncrementThirtyMinutes
		{
			get
			{
				return new LocalizedString("TimeIncrementThirtyMinutes", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000096 RID: 150
		// (get) Token: 0x0600009B RID: 155 RVA: 0x000074DD File Offset: 0x000056DD
		public static LocalizedString RetentionActionNeverMove
		{
			get
			{
				return new LocalizedString("RetentionActionNeverMove", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000097 RID: 151
		// (get) Token: 0x0600009C RID: 156 RVA: 0x000074FB File Offset: 0x000056FB
		public static LocalizedString VoicemailMobileOperatorColon
		{
			get
			{
				return new LocalizedString("VoicemailMobileOperatorColon", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000098 RID: 152
		// (get) Token: 0x0600009D RID: 157 RVA: 0x00007519 File Offset: 0x00005719
		public static LocalizedString ConnectedAccountsDescriptionForForwarding
		{
			get
			{
				return new LocalizedString("ConnectedAccountsDescriptionForForwarding", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000099 RID: 153
		// (get) Token: 0x0600009E RID: 158 RVA: 0x00007537 File Offset: 0x00005737
		public static LocalizedString StopForward
		{
			get
			{
				return new LocalizedString("StopForward", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700009A RID: 154
		// (get) Token: 0x0600009F RID: 159 RVA: 0x00007555 File Offset: 0x00005755
		public static LocalizedString FirstWeekOfYear
		{
			get
			{
				return new LocalizedString("FirstWeekOfYear", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700009B RID: 155
		// (get) Token: 0x060000A0 RID: 160 RVA: 0x00007573 File Offset: 0x00005773
		public static LocalizedString RegionListLabel
		{
			get
			{
				return new LocalizedString("RegionListLabel", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700009C RID: 156
		// (get) Token: 0x060000A1 RID: 161 RVA: 0x00007591 File Offset: 0x00005791
		public static LocalizedString InstallFromMarketplace
		{
			get
			{
				return new LocalizedString("InstallFromMarketplace", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700009D RID: 157
		// (get) Token: 0x060000A2 RID: 162 RVA: 0x000075AF File Offset: 0x000057AF
		public static LocalizedString RulesNameColumn
		{
			get
			{
				return new LocalizedString("RulesNameColumn", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700009E RID: 158
		// (get) Token: 0x060000A3 RID: 163 RVA: 0x000075CD File Offset: 0x000057CD
		public static LocalizedString DeviceOSLabel
		{
			get
			{
				return new LocalizedString("DeviceOSLabel", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060000A4 RID: 164 RVA: 0x000075EC File Offset: 0x000057EC
		public static LocalizedString VerificationEmailFailedToSend(string emailAddress)
		{
			return new LocalizedString("VerificationEmailFailedToSend", "", false, false, OwaOptionStrings.ResourceManager, new object[]
			{
				emailAddress
			});
		}

		// Token: 0x1700009F RID: 159
		// (get) Token: 0x060000A5 RID: 165 RVA: 0x0000761B File Offset: 0x0000581B
		public static LocalizedString InboxRuleSentOnlyToMeConditionText
		{
			get
			{
				return new LocalizedString("InboxRuleSentOnlyToMeConditionText", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170000A0 RID: 160
		// (get) Token: 0x060000A6 RID: 166 RVA: 0x00007639 File Offset: 0x00005839
		public static LocalizedString EditYourPassword
		{
			get
			{
				return new LocalizedString("EditYourPassword", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170000A1 RID: 161
		// (get) Token: 0x060000A7 RID: 167 RVA: 0x00007657 File Offset: 0x00005857
		public static LocalizedString EnforceSchedulingHorizon
		{
			get
			{
				return new LocalizedString("EnforceSchedulingHorizon", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170000A2 RID: 162
		// (get) Token: 0x060000A8 RID: 168 RVA: 0x00007675 File Offset: 0x00005875
		public static LocalizedString TeamMailboxManagementString2
		{
			get
			{
				return new LocalizedString("TeamMailboxManagementString2", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170000A3 RID: 163
		// (get) Token: 0x060000A9 RID: 169 RVA: 0x00007693 File Offset: 0x00005893
		public static LocalizedString SearchMessageTipForIWUser
		{
			get
			{
				return new LocalizedString("SearchMessageTipForIWUser", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170000A4 RID: 164
		// (get) Token: 0x060000AA RID: 170 RVA: 0x000076B1 File Offset: 0x000058B1
		public static LocalizedString ConnectedAccountsDescriptionForSubscription
		{
			get
			{
				return new LocalizedString("ConnectedAccountsDescriptionForSubscription", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170000A5 RID: 165
		// (get) Token: 0x060000AB RID: 171 RVA: 0x000076CF File Offset: 0x000058CF
		public static LocalizedString QLManageOrganization
		{
			get
			{
				return new LocalizedString("QLManageOrganization", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170000A6 RID: 166
		// (get) Token: 0x060000AC RID: 172 RVA: 0x000076ED File Offset: 0x000058ED
		public static LocalizedString JoinRestrictionApprovalRequired
		{
			get
			{
				return new LocalizedString("JoinRestrictionApprovalRequired", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170000A7 RID: 167
		// (get) Token: 0x060000AD RID: 173 RVA: 0x0000770B File Offset: 0x0000590B
		public static LocalizedString ExtensionCanNotBeUninstalled
		{
			get
			{
				return new LocalizedString("ExtensionCanNotBeUninstalled", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170000A8 RID: 168
		// (get) Token: 0x060000AE RID: 174 RVA: 0x00007729 File Offset: 0x00005929
		public static LocalizedString VoicemailWizardStep4Title
		{
			get
			{
				return new LocalizedString("VoicemailWizardStep4Title", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170000A9 RID: 169
		// (get) Token: 0x060000AF RID: 175 RVA: 0x00007747 File Offset: 0x00005947
		public static LocalizedString ViewExtensionDetails
		{
			get
			{
				return new LocalizedString("ViewExtensionDetails", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170000AA RID: 170
		// (get) Token: 0x060000B0 RID: 176 RVA: 0x00007765 File Offset: 0x00005965
		public static LocalizedString VoicemailCarrierRatesMayApply
		{
			get
			{
				return new LocalizedString("VoicemailCarrierRatesMayApply", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170000AB RID: 171
		// (get) Token: 0x060000B1 RID: 177 RVA: 0x00007783 File Offset: 0x00005983
		public static LocalizedString DeliveryReports
		{
			get
			{
				return new LocalizedString("DeliveryReports", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170000AC RID: 172
		// (get) Token: 0x060000B2 RID: 178 RVA: 0x000077A1 File Offset: 0x000059A1
		public static LocalizedString AllRequestOutOfPolicyText
		{
			get
			{
				return new LocalizedString("AllRequestOutOfPolicyText", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170000AD RID: 173
		// (get) Token: 0x060000B3 RID: 179 RVA: 0x000077BF File Offset: 0x000059BF
		public static LocalizedString RemoveDeviceConfirmMessage
		{
			get
			{
				return new LocalizedString("RemoveDeviceConfirmMessage", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170000AE RID: 174
		// (get) Token: 0x060000B4 RID: 180 RVA: 0x000077DD File Offset: 0x000059DD
		public static LocalizedString StatusLabel
		{
			get
			{
				return new LocalizedString("StatusLabel", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170000AF RID: 175
		// (get) Token: 0x060000B5 RID: 181 RVA: 0x000077FB File Offset: 0x000059FB
		public static LocalizedString InboxRuleSubjectOrBodyContainsConditionText
		{
			get
			{
				return new LocalizedString("InboxRuleSubjectOrBodyContainsConditionText", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170000B0 RID: 176
		// (get) Token: 0x060000B6 RID: 182 RVA: 0x00007819 File Offset: 0x00005A19
		public static LocalizedString OwnerLabel
		{
			get
			{
				return new LocalizedString("OwnerLabel", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170000B1 RID: 177
		// (get) Token: 0x060000B7 RID: 183 RVA: 0x00007837 File Offset: 0x00005A37
		public static LocalizedString RequireSenderAuthFalse
		{
			get
			{
				return new LocalizedString("RequireSenderAuthFalse", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170000B2 RID: 178
		// (get) Token: 0x060000B8 RID: 184 RVA: 0x00007855 File Offset: 0x00005A55
		public static LocalizedString AllowedSendersLabel
		{
			get
			{
				return new LocalizedString("AllowedSendersLabel", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170000B3 RID: 179
		// (get) Token: 0x060000B9 RID: 185 RVA: 0x00007873 File Offset: 0x00005A73
		public static LocalizedString IncomingSecuritySSL
		{
			get
			{
				return new LocalizedString("IncomingSecuritySSL", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170000B4 RID: 180
		// (get) Token: 0x060000BA RID: 186 RVA: 0x00007891 File Offset: 0x00005A91
		public static LocalizedString CarrierListLabel
		{
			get
			{
				return new LocalizedString("CarrierListLabel", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170000B5 RID: 181
		// (get) Token: 0x060000BB RID: 187 RVA: 0x000078AF File Offset: 0x00005AAF
		public static LocalizedString InboxRuleDescriptionNote
		{
			get
			{
				return new LocalizedString("InboxRuleDescriptionNote", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170000B6 RID: 182
		// (get) Token: 0x060000BC RID: 188 RVA: 0x000078CD File Offset: 0x00005ACD
		public static LocalizedString NewImapSubscription
		{
			get
			{
				return new LocalizedString("NewImapSubscription", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170000B7 RID: 183
		// (get) Token: 0x060000BD RID: 189 RVA: 0x000078EB File Offset: 0x00005AEB
		public static LocalizedString TeamMailboxStartSyncButtonString
		{
			get
			{
				return new LocalizedString("TeamMailboxStartSyncButtonString", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170000B8 RID: 184
		// (get) Token: 0x060000BE RID: 190 RVA: 0x00007909 File Offset: 0x00005B09
		public static LocalizedString NotificationsForCalendarUpdate
		{
			get
			{
				return new LocalizedString("NotificationsForCalendarUpdate", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170000B9 RID: 185
		// (get) Token: 0x060000BF RID: 191 RVA: 0x00007927 File Offset: 0x00005B27
		public static LocalizedString ReadReceiptsSlab
		{
			get
			{
				return new LocalizedString("ReadReceiptsSlab", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170000BA RID: 186
		// (get) Token: 0x060000C0 RID: 192 RVA: 0x00007945 File Offset: 0x00005B45
		public static LocalizedString DetailsLinkText
		{
			get
			{
				return new LocalizedString("DetailsLinkText", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170000BB RID: 187
		// (get) Token: 0x060000C1 RID: 193 RVA: 0x00007963 File Offset: 0x00005B63
		public static LocalizedString Help
		{
			get
			{
				return new LocalizedString("Help", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170000BC RID: 188
		// (get) Token: 0x060000C2 RID: 194 RVA: 0x00007981 File Offset: 0x00005B81
		public static LocalizedString SearchGroups
		{
			get
			{
				return new LocalizedString("SearchGroups", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170000BD RID: 189
		// (get) Token: 0x060000C3 RID: 195 RVA: 0x0000799F File Offset: 0x00005B9F
		public static LocalizedString ShowConversationAsTreeInstruction
		{
			get
			{
				return new LocalizedString("ShowConversationAsTreeInstruction", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170000BE RID: 190
		// (get) Token: 0x060000C4 RID: 196 RVA: 0x000079BD File Offset: 0x00005BBD
		public static LocalizedString BypassModerationSenders
		{
			get
			{
				return new LocalizedString("BypassModerationSenders", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170000BF RID: 191
		// (get) Token: 0x060000C5 RID: 197 RVA: 0x000079DB File Offset: 0x00005BDB
		public static LocalizedString RetentionActionDeleteAndAllowRecovery
		{
			get
			{
				return new LocalizedString("RetentionActionDeleteAndAllowRecovery", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170000C0 RID: 192
		// (get) Token: 0x060000C6 RID: 198 RVA: 0x000079F9 File Offset: 0x00005BF9
		public static LocalizedString PreviewMarkAsReadDelaytimeTextPre
		{
			get
			{
				return new LocalizedString("PreviewMarkAsReadDelaytimeTextPre", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170000C1 RID: 193
		// (get) Token: 0x060000C7 RID: 199 RVA: 0x00007A17 File Offset: 0x00005C17
		public static LocalizedString RPTMonths
		{
			get
			{
				return new LocalizedString("RPTMonths", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170000C2 RID: 194
		// (get) Token: 0x060000C8 RID: 200 RVA: 0x00007A35 File Offset: 0x00005C35
		public static LocalizedString AfterMoveOrDeleteBehavior
		{
			get
			{
				return new LocalizedString("AfterMoveOrDeleteBehavior", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060000C9 RID: 201 RVA: 0x00007A54 File Offset: 0x00005C54
		public static LocalizedString VerificationSuccessText(string emailAddress)
		{
			return new LocalizedString("VerificationSuccessText", "", false, false, OwaOptionStrings.ResourceManager, new object[]
			{
				emailAddress
			});
		}

		// Token: 0x170000C3 RID: 195
		// (get) Token: 0x060000CA RID: 202 RVA: 0x00007A83 File Offset: 0x00005C83
		public static LocalizedString HideGroupFromAddressLists
		{
			get
			{
				return new LocalizedString("HideGroupFromAddressLists", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170000C4 RID: 196
		// (get) Token: 0x060000CB RID: 203 RVA: 0x00007AA1 File Offset: 0x00005CA1
		public static LocalizedString VoicemailWizardStep1Description
		{
			get
			{
				return new LocalizedString("VoicemailWizardStep1Description", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170000C5 RID: 197
		// (get) Token: 0x060000CC RID: 204 RVA: 0x00007ABF File Offset: 0x00005CBF
		public static LocalizedString ReviewLinkText
		{
			get
			{
				return new LocalizedString("ReviewLinkText", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170000C6 RID: 198
		// (get) Token: 0x060000CD RID: 205 RVA: 0x00007ADD File Offset: 0x00005CDD
		public static LocalizedString Processing
		{
			get
			{
				return new LocalizedString("Processing", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170000C7 RID: 199
		// (get) Token: 0x060000CE RID: 206 RVA: 0x00007AFB File Offset: 0x00005CFB
		public static LocalizedString DailyCalendarAgendas
		{
			get
			{
				return new LocalizedString("DailyCalendarAgendas", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170000C8 RID: 200
		// (get) Token: 0x060000CF RID: 207 RVA: 0x00007B19 File Offset: 0x00005D19
		public static LocalizedString PreviewMarkAsReadBehaviorOnSelectionChange
		{
			get
			{
				return new LocalizedString("PreviewMarkAsReadBehaviorOnSelectionChange", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170000C9 RID: 201
		// (get) Token: 0x060000D0 RID: 208 RVA: 0x00007B37 File Offset: 0x00005D37
		public static LocalizedString TimeZoneLabelText
		{
			get
			{
				return new LocalizedString("TimeZoneLabelText", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170000CA RID: 202
		// (get) Token: 0x060000D1 RID: 209 RVA: 0x00007B55 File Offset: 0x00005D55
		public static LocalizedString QLVoiceMail
		{
			get
			{
				return new LocalizedString("QLVoiceMail", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170000CB RID: 203
		// (get) Token: 0x060000D2 RID: 210 RVA: 0x00007B73 File Offset: 0x00005D73
		public static LocalizedString VoicemailSignUpIntro
		{
			get
			{
				return new LocalizedString("VoicemailSignUpIntro", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170000CC RID: 204
		// (get) Token: 0x060000D3 RID: 211 RVA: 0x00007B91 File Offset: 0x00005D91
		public static LocalizedString VoicemailStep2
		{
			get
			{
				return new LocalizedString("VoicemailStep2", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170000CD RID: 205
		// (get) Token: 0x060000D4 RID: 212 RVA: 0x00007BAF File Offset: 0x00005DAF
		public static LocalizedString TeamMailboxMembershipString
		{
			get
			{
				return new LocalizedString("TeamMailboxMembershipString", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170000CE RID: 206
		// (get) Token: 0x060000D5 RID: 213 RVA: 0x00007BCD File Offset: 0x00005DCD
		public static LocalizedString PasscodeLabel
		{
			get
			{
				return new LocalizedString("PasscodeLabel", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170000CF RID: 207
		// (get) Token: 0x060000D6 RID: 214 RVA: 0x00007BEB File Offset: 0x00005DEB
		public static LocalizedString PersonalSettingPasswordAfterChange
		{
			get
			{
				return new LocalizedString("PersonalSettingPasswordAfterChange", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170000D0 RID: 208
		// (get) Token: 0x060000D7 RID: 215 RVA: 0x00007C09 File Offset: 0x00005E09
		public static LocalizedString VerificationSuccessPageTitle
		{
			get
			{
				return new LocalizedString("VerificationSuccessPageTitle", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170000D1 RID: 209
		// (get) Token: 0x060000D8 RID: 216 RVA: 0x00007C27 File Offset: 0x00005E27
		public static LocalizedString EnableAutomaticProcessingNote
		{
			get
			{
				return new LocalizedString("EnableAutomaticProcessingNote", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170000D2 RID: 210
		// (get) Token: 0x060000D9 RID: 217 RVA: 0x00007C45 File Offset: 0x00005E45
		public static LocalizedString Days
		{
			get
			{
				return new LocalizedString("Days", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170000D3 RID: 211
		// (get) Token: 0x060000DA RID: 218 RVA: 0x00007C63 File Offset: 0x00005E63
		public static LocalizedString NotificationsNotSetUp
		{
			get
			{
				return new LocalizedString("NotificationsNotSetUp", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170000D4 RID: 212
		// (get) Token: 0x060000DB RID: 219 RVA: 0x00007C81 File Offset: 0x00005E81
		public static LocalizedString ModerationNotificationsInternal
		{
			get
			{
				return new LocalizedString("ModerationNotificationsInternal", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170000D5 RID: 213
		// (get) Token: 0x060000DC RID: 220 RVA: 0x00007C9F File Offset: 0x00005E9F
		public static LocalizedString ProtocolSettings
		{
			get
			{
				return new LocalizedString("ProtocolSettings", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170000D6 RID: 214
		// (get) Token: 0x060000DD RID: 221 RVA: 0x00007CBD File Offset: 0x00005EBD
		public static LocalizedString EnableAutomaticProcessing
		{
			get
			{
				return new LocalizedString("EnableAutomaticProcessing", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170000D7 RID: 215
		// (get) Token: 0x060000DE RID: 222 RVA: 0x00007CDB File Offset: 0x00005EDB
		public static LocalizedString MessageOptionsSlab
		{
			get
			{
				return new LocalizedString("MessageOptionsSlab", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170000D8 RID: 216
		// (get) Token: 0x060000DF RID: 223 RVA: 0x00007CF9 File Offset: 0x00005EF9
		public static LocalizedString ChooseMessageFont
		{
			get
			{
				return new LocalizedString("ChooseMessageFont", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170000D9 RID: 217
		// (get) Token: 0x060000E0 RID: 224 RVA: 0x00007D17 File Offset: 0x00005F17
		public static LocalizedString Password
		{
			get
			{
				return new LocalizedString("Password", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170000DA RID: 218
		// (get) Token: 0x060000E1 RID: 225 RVA: 0x00007D35 File Offset: 0x00005F35
		public static LocalizedString OWAExtensions
		{
			get
			{
				return new LocalizedString("OWAExtensions", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170000DB RID: 219
		// (get) Token: 0x060000E2 RID: 226 RVA: 0x00007D53 File Offset: 0x00005F53
		public static LocalizedString StringArrayDialogLabel
		{
			get
			{
				return new LocalizedString("StringArrayDialogLabel", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170000DC RID: 220
		// (get) Token: 0x060000E3 RID: 227 RVA: 0x00007D71 File Offset: 0x00005F71
		public static LocalizedString Unlimited
		{
			get
			{
				return new LocalizedString("Unlimited", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170000DD RID: 221
		// (get) Token: 0x060000E4 RID: 228 RVA: 0x00007D8F File Offset: 0x00005F8F
		public static LocalizedString VoicemailSMSOptionVoiceMailOnly
		{
			get
			{
				return new LocalizedString("VoicemailSMSOptionVoiceMailOnly", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170000DE RID: 222
		// (get) Token: 0x060000E5 RID: 229 RVA: 0x00007DAD File Offset: 0x00005FAD
		public static LocalizedString Rules
		{
			get
			{
				return new LocalizedString("Rules", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170000DF RID: 223
		// (get) Token: 0x060000E6 RID: 230 RVA: 0x00007DCB File Offset: 0x00005FCB
		public static LocalizedString ModeratedByEmptyDataText
		{
			get
			{
				return new LocalizedString("ModeratedByEmptyDataText", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170000E0 RID: 224
		// (get) Token: 0x060000E7 RID: 231 RVA: 0x00007DE9 File Offset: 0x00005FE9
		public static LocalizedString TextMessaging
		{
			get
			{
				return new LocalizedString("TextMessaging", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170000E1 RID: 225
		// (get) Token: 0x060000E8 RID: 232 RVA: 0x00007E07 File Offset: 0x00006007
		public static LocalizedString FromLabel
		{
			get
			{
				return new LocalizedString("FromLabel", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170000E2 RID: 226
		// (get) Token: 0x060000E9 RID: 233 RVA: 0x00007E25 File Offset: 0x00006025
		public static LocalizedString GroupModerators
		{
			get
			{
				return new LocalizedString("GroupModerators", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170000E3 RID: 227
		// (get) Token: 0x060000EA RID: 234 RVA: 0x00007E43 File Offset: 0x00006043
		public static LocalizedString ReadingPaneInstruction
		{
			get
			{
				return new LocalizedString("ReadingPaneInstruction", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170000E4 RID: 228
		// (get) Token: 0x060000EB RID: 235 RVA: 0x00007E61 File Offset: 0x00006061
		public static LocalizedString RPTNone
		{
			get
			{
				return new LocalizedString("RPTNone", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170000E5 RID: 229
		// (get) Token: 0x060000EC RID: 236 RVA: 0x00007E7F File Offset: 0x0000607F
		public static LocalizedString Spelling
		{
			get
			{
				return new LocalizedString("Spelling", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170000E6 RID: 230
		// (get) Token: 0x060000ED RID: 237 RVA: 0x00007E9D File Offset: 0x0000609D
		public static LocalizedString CancelWipeDeviceCommandText
		{
			get
			{
				return new LocalizedString("CancelWipeDeviceCommandText", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170000E7 RID: 231
		// (get) Token: 0x060000EE RID: 238 RVA: 0x00007EBB File Offset: 0x000060BB
		public static LocalizedString AutomaticRepliesEnabledText
		{
			get
			{
				return new LocalizedString("AutomaticRepliesEnabledText", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170000E8 RID: 232
		// (get) Token: 0x060000EF RID: 239 RVA: 0x00007ED9 File Offset: 0x000060D9
		public static LocalizedString DisplayNameLabel
		{
			get
			{
				return new LocalizedString("DisplayNameLabel", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170000E9 RID: 233
		// (get) Token: 0x060000F0 RID: 240 RVA: 0x00007EF7 File Offset: 0x000060F7
		public static LocalizedString CancelButtonText
		{
			get
			{
				return new LocalizedString("CancelButtonText", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170000EA RID: 234
		// (get) Token: 0x060000F1 RID: 241 RVA: 0x00007F15 File Offset: 0x00006115
		public static LocalizedString GroupMembershipApproval
		{
			get
			{
				return new LocalizedString("GroupMembershipApproval", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170000EB RID: 235
		// (get) Token: 0x060000F2 RID: 242 RVA: 0x00007F33 File Offset: 0x00006133
		public static LocalizedString InlcudedRecipientTypesLabel
		{
			get
			{
				return new LocalizedString("InlcudedRecipientTypesLabel", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170000EC RID: 236
		// (get) Token: 0x060000F3 RID: 243 RVA: 0x00007F51 File Offset: 0x00006151
		public static LocalizedString Name
		{
			get
			{
				return new LocalizedString("Name", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170000ED RID: 237
		// (get) Token: 0x060000F4 RID: 244 RVA: 0x00007F6F File Offset: 0x0000616F
		public static LocalizedString RetentionActionTypeDelete
		{
			get
			{
				return new LocalizedString("RetentionActionTypeDelete", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170000EE RID: 238
		// (get) Token: 0x060000F5 RID: 245 RVA: 0x00007F8D File Offset: 0x0000618D
		public static LocalizedString InboxRuleMyNameInCcBoxConditionFlyOutText
		{
			get
			{
				return new LocalizedString("InboxRuleMyNameInCcBoxConditionFlyOutText", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170000EF RID: 239
		// (get) Token: 0x060000F6 RID: 246 RVA: 0x00007FAB File Offset: 0x000061AB
		public static LocalizedString ThursdayCheckBoxText
		{
			get
			{
				return new LocalizedString("ThursdayCheckBoxText", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170000F0 RID: 240
		// (get) Token: 0x060000F7 RID: 247 RVA: 0x00007FC9 File Offset: 0x000061C9
		public static LocalizedString JoinGroup
		{
			get
			{
				return new LocalizedString("JoinGroup", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170000F1 RID: 241
		// (get) Token: 0x060000F8 RID: 248 RVA: 0x00007FE7 File Offset: 0x000061E7
		public static LocalizedString Account
		{
			get
			{
				return new LocalizedString("Account", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170000F2 RID: 242
		// (get) Token: 0x060000F9 RID: 249 RVA: 0x00008005 File Offset: 0x00006205
		public static LocalizedString InboxRuleMessageTypeMatchesConditionFlyOutText
		{
			get
			{
				return new LocalizedString("InboxRuleMessageTypeMatchesConditionFlyOutText", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170000F3 RID: 243
		// (get) Token: 0x060000FA RID: 250 RVA: 0x00008023 File Offset: 0x00006223
		public static LocalizedString InboxRuleMoveCopyDeleteGroupText
		{
			get
			{
				return new LocalizedString("InboxRuleMoveCopyDeleteGroupText", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170000F4 RID: 244
		// (get) Token: 0x060000FB RID: 251 RVA: 0x00008041 File Offset: 0x00006241
		public static LocalizedString RegionalSettingsInstruction
		{
			get
			{
				return new LocalizedString("RegionalSettingsInstruction", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170000F5 RID: 245
		// (get) Token: 0x060000FC RID: 252 RVA: 0x0000805F File Offset: 0x0000625F
		public static LocalizedString NameColumn
		{
			get
			{
				return new LocalizedString("NameColumn", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170000F6 RID: 246
		// (get) Token: 0x060000FD RID: 253 RVA: 0x0000807D File Offset: 0x0000627D
		public static LocalizedString InboxRuleWithSensitivityConditionFlyOutText
		{
			get
			{
				return new LocalizedString("InboxRuleWithSensitivityConditionFlyOutText", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170000F7 RID: 247
		// (get) Token: 0x060000FE RID: 254 RVA: 0x0000809B File Offset: 0x0000629B
		public static LocalizedString ClassificationDialogTitle
		{
			get
			{
				return new LocalizedString("ClassificationDialogTitle", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170000F8 RID: 248
		// (get) Token: 0x060000FF RID: 255 RVA: 0x000080B9 File Offset: 0x000062B9
		public static LocalizedString RuleFromAndMoveToFolderTemplate
		{
			get
			{
				return new LocalizedString("RuleFromAndMoveToFolderTemplate", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170000F9 RID: 249
		// (get) Token: 0x06000100 RID: 256 RVA: 0x000080D7 File Offset: 0x000062D7
		public static LocalizedString DomainNameNotSetError
		{
			get
			{
				return new LocalizedString("DomainNameNotSetError", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170000FA RID: 250
		// (get) Token: 0x06000101 RID: 257 RVA: 0x000080F5 File Offset: 0x000062F5
		public static LocalizedString MakeSecurityGroup
		{
			get
			{
				return new LocalizedString("MakeSecurityGroup", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170000FB RID: 251
		// (get) Token: 0x06000102 RID: 258 RVA: 0x00008113 File Offset: 0x00006313
		public static LocalizedString ContactNumbersBookmark
		{
			get
			{
				return new LocalizedString("ContactNumbersBookmark", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170000FC RID: 252
		// (get) Token: 0x06000103 RID: 259 RVA: 0x00008131 File Offset: 0x00006331
		public static LocalizedString InboxRuleSentToConditionText
		{
			get
			{
				return new LocalizedString("InboxRuleSentToConditionText", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170000FD RID: 253
		// (get) Token: 0x06000104 RID: 260 RVA: 0x0000814F File Offset: 0x0000634F
		public static LocalizedString MemberOfGroups
		{
			get
			{
				return new LocalizedString("MemberOfGroups", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170000FE RID: 254
		// (get) Token: 0x06000105 RID: 261 RVA: 0x0000816D File Offset: 0x0000636D
		public static LocalizedString InboxRuleIncludeTheseWordsGroupText
		{
			get
			{
				return new LocalizedString("InboxRuleIncludeTheseWordsGroupText", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170000FF RID: 255
		// (get) Token: 0x06000106 RID: 262 RVA: 0x0000818B File Offset: 0x0000638B
		public static LocalizedString MailTipLabel
		{
			get
			{
				return new LocalizedString("MailTipLabel", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000100 RID: 256
		// (get) Token: 0x06000107 RID: 263 RVA: 0x000081A9 File Offset: 0x000063A9
		public static LocalizedString MessageTypeDialogLabel
		{
			get
			{
				return new LocalizedString("MessageTypeDialogLabel", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000101 RID: 257
		// (get) Token: 0x06000108 RID: 264 RVA: 0x000081C7 File Offset: 0x000063C7
		public static LocalizedString RegionalSettingsSlab
		{
			get
			{
				return new LocalizedString("RegionalSettingsSlab", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000102 RID: 258
		// (get) Token: 0x06000109 RID: 265 RVA: 0x000081E5 File Offset: 0x000063E5
		public static LocalizedString VoicemailWizardStep3Title
		{
			get
			{
				return new LocalizedString("VoicemailWizardStep3Title", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000103 RID: 259
		// (get) Token: 0x0600010A RID: 266 RVA: 0x00008203 File Offset: 0x00006403
		public static LocalizedString InboxRuleMyNameNotInToBoxConditionFlyOutText
		{
			get
			{
				return new LocalizedString("InboxRuleMyNameNotInToBoxConditionFlyOutText", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000104 RID: 260
		// (get) Token: 0x0600010B RID: 267 RVA: 0x00008221 File Offset: 0x00006421
		public static LocalizedString ReturnToOWA
		{
			get
			{
				return new LocalizedString("ReturnToOWA", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000105 RID: 261
		// (get) Token: 0x0600010C RID: 268 RVA: 0x0000823F File Offset: 0x0000643F
		public static LocalizedString InboxRuleMyNameInToBoxConditionText
		{
			get
			{
				return new LocalizedString("InboxRuleMyNameInToBoxConditionText", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000106 RID: 262
		// (get) Token: 0x0600010D RID: 269 RVA: 0x0000825D File Offset: 0x0000645D
		public static LocalizedString CommitButtonText
		{
			get
			{
				return new LocalizedString("CommitButtonText", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000107 RID: 263
		// (get) Token: 0x0600010E RID: 270 RVA: 0x0000827B File Offset: 0x0000647B
		public static LocalizedString TeamMailboxShowInMyClientString
		{
			get
			{
				return new LocalizedString("TeamMailboxShowInMyClientString", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000108 RID: 264
		// (get) Token: 0x0600010F RID: 271 RVA: 0x00008299 File Offset: 0x00006499
		public static LocalizedString InboxRuleMarkAsReadActionText
		{
			get
			{
				return new LocalizedString("InboxRuleMarkAsReadActionText", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000109 RID: 265
		// (get) Token: 0x06000110 RID: 272 RVA: 0x000082B7 File Offset: 0x000064B7
		public static LocalizedString ClassificationDialogLabel
		{
			get
			{
				return new LocalizedString("ClassificationDialogLabel", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700010A RID: 266
		// (get) Token: 0x06000111 RID: 273 RVA: 0x000082D5 File Offset: 0x000064D5
		public static LocalizedString WarningAlt
		{
			get
			{
				return new LocalizedString("WarningAlt", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700010B RID: 267
		// (get) Token: 0x06000112 RID: 274 RVA: 0x000082F3 File Offset: 0x000064F3
		public static LocalizedString TeamMailboxManagementString4
		{
			get
			{
				return new LocalizedString("TeamMailboxManagementString4", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700010C RID: 268
		// (get) Token: 0x06000113 RID: 275 RVA: 0x00008311 File Offset: 0x00006511
		public static LocalizedString Mail
		{
			get
			{
				return new LocalizedString("Mail", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700010D RID: 269
		// (get) Token: 0x06000114 RID: 276 RVA: 0x0000832F File Offset: 0x0000652F
		public static LocalizedString ImportContactList
		{
			get
			{
				return new LocalizedString("ImportContactList", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000115 RID: 277 RVA: 0x00008350 File Offset: 0x00006550
		public static LocalizedString JoinOtherDlsSuccess(int num)
		{
			return new LocalizedString("JoinOtherDlsSuccess", "", false, false, OwaOptionStrings.ResourceManager, new object[]
			{
				num
			});
		}

		// Token: 0x1700010E RID: 270
		// (get) Token: 0x06000116 RID: 278 RVA: 0x00008384 File Offset: 0x00006584
		public static LocalizedString QLImportContacts
		{
			get
			{
				return new LocalizedString("QLImportContacts", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700010F RID: 271
		// (get) Token: 0x06000117 RID: 279 RVA: 0x000083A2 File Offset: 0x000065A2
		public static LocalizedString InboxRule
		{
			get
			{
				return new LocalizedString("InboxRule", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000110 RID: 272
		// (get) Token: 0x06000118 RID: 280 RVA: 0x000083C0 File Offset: 0x000065C0
		public static LocalizedString WithinDateRangeDialogTitle
		{
			get
			{
				return new LocalizedString("WithinDateRangeDialogTitle", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000111 RID: 273
		// (get) Token: 0x06000119 RID: 281 RVA: 0x000083DE File Offset: 0x000065DE
		public static LocalizedString ReminderSoundEnabled
		{
			get
			{
				return new LocalizedString("ReminderSoundEnabled", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000112 RID: 274
		// (get) Token: 0x0600011A RID: 282 RVA: 0x000083FC File Offset: 0x000065FC
		public static LocalizedString RecipientAddressContainsConditionFormat
		{
			get
			{
				return new LocalizedString("RecipientAddressContainsConditionFormat", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000113 RID: 275
		// (get) Token: 0x0600011B RID: 283 RVA: 0x0000841A File Offset: 0x0000661A
		public static LocalizedString MessageFormatPlainText
		{
			get
			{
				return new LocalizedString("MessageFormatPlainText", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000114 RID: 276
		// (get) Token: 0x0600011C RID: 284 RVA: 0x00008438 File Offset: 0x00006638
		public static LocalizedString DeleteInboxRuleConfirmation
		{
			get
			{
				return new LocalizedString("DeleteInboxRuleConfirmation", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000115 RID: 277
		// (get) Token: 0x0600011D RID: 285 RVA: 0x00008456 File Offset: 0x00006656
		public static LocalizedString ForwardEmailTitle
		{
			get
			{
				return new LocalizedString("ForwardEmailTitle", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000116 RID: 278
		// (get) Token: 0x0600011E RID: 286 RVA: 0x00008474 File Offset: 0x00006674
		public static LocalizedString BypassModerationSendersEmptyDataText
		{
			get
			{
				return new LocalizedString("BypassModerationSendersEmptyDataText", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000117 RID: 279
		// (get) Token: 0x0600011F RID: 287 RVA: 0x00008492 File Offset: 0x00006692
		public static LocalizedString RuleSubjectContainsAndDeleteMessageTemplate
		{
			get
			{
				return new LocalizedString("RuleSubjectContainsAndDeleteMessageTemplate", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000118 RID: 280
		// (get) Token: 0x06000120 RID: 288 RVA: 0x000084B0 File Offset: 0x000066B0
		public static LocalizedString HideDeletedItems
		{
			get
			{
				return new LocalizedString("HideDeletedItems", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000119 RID: 281
		// (get) Token: 0x06000121 RID: 289 RVA: 0x000084CE File Offset: 0x000066CE
		public static LocalizedString VoicemailSetupNowButtonText
		{
			get
			{
				return new LocalizedString("VoicemailSetupNowButtonText", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700011A RID: 282
		// (get) Token: 0x06000122 RID: 290 RVA: 0x000084EC File Offset: 0x000066EC
		public static LocalizedString CalendarPermissions
		{
			get
			{
				return new LocalizedString("CalendarPermissions", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700011B RID: 283
		// (get) Token: 0x06000123 RID: 291 RVA: 0x0000850A File Offset: 0x0000670A
		public static LocalizedString ModerationNotificationsAlways
		{
			get
			{
				return new LocalizedString("ModerationNotificationsAlways", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700011C RID: 284
		// (get) Token: 0x06000124 RID: 292 RVA: 0x00008528 File Offset: 0x00006728
		public static LocalizedString ExternalMessageInstruction
		{
			get
			{
				return new LocalizedString("ExternalMessageInstruction", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700011D RID: 285
		// (get) Token: 0x06000125 RID: 293 RVA: 0x00008546 File Offset: 0x00006746
		public static LocalizedString RequirementsReadItemValue
		{
			get
			{
				return new LocalizedString("RequirementsReadItemValue", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700011E RID: 286
		// (get) Token: 0x06000126 RID: 294 RVA: 0x00008564 File Offset: 0x00006764
		public static LocalizedString SchedulingOptionsSlab
		{
			get
			{
				return new LocalizedString("SchedulingOptionsSlab", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700011F RID: 287
		// (get) Token: 0x06000127 RID: 295 RVA: 0x00008582 File Offset: 0x00006782
		public static LocalizedString ShowConversationTree
		{
			get
			{
				return new LocalizedString("ShowConversationTree", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000120 RID: 288
		// (get) Token: 0x06000128 RID: 296 RVA: 0x000085A0 File Offset: 0x000067A0
		public static LocalizedString RetentionPolicies
		{
			get
			{
				return new LocalizedString("RetentionPolicies", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000121 RID: 289
		// (get) Token: 0x06000129 RID: 297 RVA: 0x000085BE File Offset: 0x000067BE
		public static LocalizedString CalendarPublishingSubscriptionUrl
		{
			get
			{
				return new LocalizedString("CalendarPublishingSubscriptionUrl", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000122 RID: 290
		// (get) Token: 0x0600012A RID: 298 RVA: 0x000085DC File Offset: 0x000067DC
		public static LocalizedString ResendVerificationEmailCommandText
		{
			get
			{
				return new LocalizedString("ResendVerificationEmailCommandText", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000123 RID: 291
		// (get) Token: 0x0600012B RID: 299 RVA: 0x000085FA File Offset: 0x000067FA
		public static LocalizedString TextMessagingNotification
		{
			get
			{
				return new LocalizedString("TextMessagingNotification", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000124 RID: 292
		// (get) Token: 0x0600012C RID: 300 RVA: 0x00008618 File Offset: 0x00006818
		public static LocalizedString InstalledByColumn
		{
			get
			{
				return new LocalizedString("InstalledByColumn", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000125 RID: 293
		// (get) Token: 0x0600012D RID: 301 RVA: 0x00008636 File Offset: 0x00006836
		public static LocalizedString GroupOrganizationalUnit
		{
			get
			{
				return new LocalizedString("GroupOrganizationalUnit", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000126 RID: 294
		// (get) Token: 0x0600012E RID: 302 RVA: 0x00008654 File Offset: 0x00006854
		public static LocalizedString MailboxFolderDialogTitle
		{
			get
			{
				return new LocalizedString("MailboxFolderDialogTitle", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000127 RID: 295
		// (get) Token: 0x0600012F RID: 303 RVA: 0x00008672 File Offset: 0x00006872
		public static LocalizedString PersonalSettingOldPassword
		{
			get
			{
				return new LocalizedString("PersonalSettingOldPassword", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000128 RID: 296
		// (get) Token: 0x06000130 RID: 304 RVA: 0x00008690 File Offset: 0x00006890
		public static LocalizedString VoicemailStep3
		{
			get
			{
				return new LocalizedString("VoicemailStep3", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000129 RID: 297
		// (get) Token: 0x06000131 RID: 305 RVA: 0x000086AE File Offset: 0x000068AE
		public static LocalizedString CityLabel
		{
			get
			{
				return new LocalizedString("CityLabel", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700012A RID: 298
		// (get) Token: 0x06000132 RID: 306 RVA: 0x000086CC File Offset: 0x000068CC
		public static LocalizedString SentToConditionFormat
		{
			get
			{
				return new LocalizedString("SentToConditionFormat", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700012B RID: 299
		// (get) Token: 0x06000133 RID: 307 RVA: 0x000086EA File Offset: 0x000068EA
		public static LocalizedString QLSubscription
		{
			get
			{
				return new LocalizedString("QLSubscription", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700012C RID: 300
		// (get) Token: 0x06000134 RID: 308 RVA: 0x00008708 File Offset: 0x00006908
		public static LocalizedString ViewRPTDetailsTitle
		{
			get
			{
				return new LocalizedString("ViewRPTDetailsTitle", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700012D RID: 301
		// (get) Token: 0x06000135 RID: 309 RVA: 0x00008726 File Offset: 0x00006926
		public static LocalizedString MyGroups
		{
			get
			{
				return new LocalizedString("MyGroups", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700012E RID: 302
		// (get) Token: 0x06000136 RID: 310 RVA: 0x00008744 File Offset: 0x00006944
		public static LocalizedString TeamMailboxesString
		{
			get
			{
				return new LocalizedString("TeamMailboxesString", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700012F RID: 303
		// (get) Token: 0x06000137 RID: 311 RVA: 0x00008762 File Offset: 0x00006962
		public static LocalizedString DeliveryReport
		{
			get
			{
				return new LocalizedString("DeliveryReport", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000130 RID: 304
		// (get) Token: 0x06000138 RID: 312 RVA: 0x00008780 File Offset: 0x00006980
		public static LocalizedString LastNameLabel
		{
			get
			{
				return new LocalizedString("LastNameLabel", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000131 RID: 305
		// (get) Token: 0x06000139 RID: 313 RVA: 0x0000879E File Offset: 0x0000699E
		public static LocalizedString CalendarPublishingStop
		{
			get
			{
				return new LocalizedString("CalendarPublishingStop", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000132 RID: 306
		// (get) Token: 0x0600013A RID: 314 RVA: 0x000087BC File Offset: 0x000069BC
		public static LocalizedString VoicemailWizardStep3Description
		{
			get
			{
				return new LocalizedString("VoicemailWizardStep3Description", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x0600013B RID: 315 RVA: 0x000087DC File Offset: 0x000069DC
		public static LocalizedString ReceiveNotificationsUsingFormat(string phoneNumber)
		{
			return new LocalizedString("ReceiveNotificationsUsingFormat", "", false, false, OwaOptionStrings.ResourceManager, new object[]
			{
				phoneNumber
			});
		}

		// Token: 0x17000133 RID: 307
		// (get) Token: 0x0600013C RID: 316 RVA: 0x0000880B File Offset: 0x00006A0B
		public static LocalizedString QLWhatsNewForOrganizations
		{
			get
			{
				return new LocalizedString("QLWhatsNewForOrganizations", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000134 RID: 308
		// (get) Token: 0x0600013D RID: 317 RVA: 0x00008829 File Offset: 0x00006A29
		public static LocalizedString ReadReceiptResponseAlways
		{
			get
			{
				return new LocalizedString("ReadReceiptResponseAlways", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000135 RID: 309
		// (get) Token: 0x0600013E RID: 318 RVA: 0x00008847 File Offset: 0x00006A47
		public static LocalizedString JunkEmailTrustedListsOnly
		{
			get
			{
				return new LocalizedString("JunkEmailTrustedListsOnly", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000136 RID: 310
		// (get) Token: 0x0600013F RID: 319 RVA: 0x00008865 File Offset: 0x00006A65
		public static LocalizedString MatchSortOrder
		{
			get
			{
				return new LocalizedString("MatchSortOrder", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000137 RID: 311
		// (get) Token: 0x06000140 RID: 320 RVA: 0x00008883 File Offset: 0x00006A83
		public static LocalizedString DevicePhoneNumberLabel
		{
			get
			{
				return new LocalizedString("DevicePhoneNumberLabel", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000138 RID: 312
		// (get) Token: 0x06000141 RID: 321 RVA: 0x000088A1 File Offset: 0x00006AA1
		public static LocalizedString InboxRuleMessageTypeMatchesConditionText
		{
			get
			{
				return new LocalizedString("InboxRuleMessageTypeMatchesConditionText", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000139 RID: 313
		// (get) Token: 0x06000142 RID: 322 RVA: 0x000088BF File Offset: 0x00006ABF
		public static LocalizedString RetentionTypeOptional
		{
			get
			{
				return new LocalizedString("RetentionTypeOptional", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700013A RID: 314
		// (get) Token: 0x06000143 RID: 323 RVA: 0x000088DD File Offset: 0x00006ADD
		public static LocalizedString UseSettings
		{
			get
			{
				return new LocalizedString("UseSettings", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700013B RID: 315
		// (get) Token: 0x06000144 RID: 324 RVA: 0x000088FB File Offset: 0x00006AFB
		public static LocalizedString SearchAllGroups
		{
			get
			{
				return new LocalizedString("SearchAllGroups", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700013C RID: 316
		// (get) Token: 0x06000145 RID: 325 RVA: 0x00008919 File Offset: 0x00006B19
		public static LocalizedString MembersLabel
		{
			get
			{
				return new LocalizedString("MembersLabel", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700013D RID: 317
		// (get) Token: 0x06000146 RID: 326 RVA: 0x00008937 File Offset: 0x00006B37
		public static LocalizedString FreeBusyInformation
		{
			get
			{
				return new LocalizedString("FreeBusyInformation", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700013E RID: 318
		// (get) Token: 0x06000147 RID: 327 RVA: 0x00008955 File Offset: 0x00006B55
		public static LocalizedString DeviceIMEILabel
		{
			get
			{
				return new LocalizedString("DeviceIMEILabel", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700013F RID: 319
		// (get) Token: 0x06000148 RID: 328 RVA: 0x00008973 File Offset: 0x00006B73
		public static LocalizedString Day
		{
			get
			{
				return new LocalizedString("Day", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000140 RID: 320
		// (get) Token: 0x06000149 RID: 329 RVA: 0x00008991 File Offset: 0x00006B91
		public static LocalizedString InboxRuleMoveToFolderActionText
		{
			get
			{
				return new LocalizedString("InboxRuleMoveToFolderActionText", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000141 RID: 321
		// (get) Token: 0x0600014A RID: 330 RVA: 0x000089AF File Offset: 0x00006BAF
		public static LocalizedString SelectOne
		{
			get
			{
				return new LocalizedString("SelectOne", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000142 RID: 322
		// (get) Token: 0x0600014B RID: 331 RVA: 0x000089CD File Offset: 0x00006BCD
		public static LocalizedString InboxRuleApplyCategoryActionFlyOutText
		{
			get
			{
				return new LocalizedString("InboxRuleApplyCategoryActionFlyOutText", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000143 RID: 323
		// (get) Token: 0x0600014C RID: 332 RVA: 0x000089EB File Offset: 0x00006BEB
		public static LocalizedString SetupEmailNotificationsLink
		{
			get
			{
				return new LocalizedString("SetupEmailNotificationsLink", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000144 RID: 324
		// (get) Token: 0x0600014D RID: 333 RVA: 0x00008A09 File Offset: 0x00006C09
		public static LocalizedString NewDistributionGroupCaption
		{
			get
			{
				return new LocalizedString("NewDistributionGroupCaption", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000145 RID: 325
		// (get) Token: 0x0600014E RID: 334 RVA: 0x00008A27 File Offset: 0x00006C27
		public static LocalizedString ViewRPTRetentionActionLabel
		{
			get
			{
				return new LocalizedString("ViewRPTRetentionActionLabel", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000146 RID: 326
		// (get) Token: 0x0600014F RID: 335 RVA: 0x00008A45 File Offset: 0x00006C45
		public static LocalizedString ShowWeekNumbersCheckBoxText
		{
			get
			{
				return new LocalizedString("ShowWeekNumbersCheckBoxText", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000147 RID: 327
		// (get) Token: 0x06000150 RID: 336 RVA: 0x00008A63 File Offset: 0x00006C63
		public static LocalizedString UserNameWLIDLabel
		{
			get
			{
				return new LocalizedString("UserNameWLIDLabel", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000148 RID: 328
		// (get) Token: 0x06000151 RID: 337 RVA: 0x00008A81 File Offset: 0x00006C81
		public static LocalizedString SearchMessagesIReceivedLabel
		{
			get
			{
				return new LocalizedString("SearchMessagesIReceivedLabel", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000149 RID: 329
		// (get) Token: 0x06000152 RID: 338 RVA: 0x00008A9F File Offset: 0x00006C9F
		public static LocalizedString InboxRuleWithinDateRangeConditionText
		{
			get
			{
				return new LocalizedString("InboxRuleWithinDateRangeConditionText", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700014A RID: 330
		// (get) Token: 0x06000153 RID: 339 RVA: 0x00008ABD File Offset: 0x00006CBD
		public static LocalizedString InboxRuleSendTextMessageNotificationToActionText
		{
			get
			{
				return new LocalizedString("InboxRuleSendTextMessageNotificationToActionText", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700014B RID: 331
		// (get) Token: 0x06000154 RID: 340 RVA: 0x00008ADB File Offset: 0x00006CDB
		public static LocalizedString SentLabel
		{
			get
			{
				return new LocalizedString("SentLabel", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700014C RID: 332
		// (get) Token: 0x06000155 RID: 341 RVA: 0x00008AF9 File Offset: 0x00006CF9
		public static LocalizedString GroupInformation
		{
			get
			{
				return new LocalizedString("GroupInformation", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700014D RID: 333
		// (get) Token: 0x06000156 RID: 342 RVA: 0x00008B17 File Offset: 0x00006D17
		public static LocalizedString VoicemailAskOperator
		{
			get
			{
				return new LocalizedString("VoicemailAskOperator", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700014E RID: 334
		// (get) Token: 0x06000157 RID: 343 RVA: 0x00008B35 File Offset: 0x00006D35
		public static LocalizedString OWA
		{
			get
			{
				return new LocalizedString("OWA", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700014F RID: 335
		// (get) Token: 0x06000158 RID: 344 RVA: 0x00008B53 File Offset: 0x00006D53
		public static LocalizedString MailTipWaterMark
		{
			get
			{
				return new LocalizedString("MailTipWaterMark", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000150 RID: 336
		// (get) Token: 0x06000159 RID: 345 RVA: 0x00008B71 File Offset: 0x00006D71
		public static LocalizedString InboxRuleWithSensitivityConditionText
		{
			get
			{
				return new LocalizedString("InboxRuleWithSensitivityConditionText", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000151 RID: 337
		// (get) Token: 0x0600015A RID: 346 RVA: 0x00008B8F File Offset: 0x00006D8F
		public static LocalizedString RetentionActionTypeDefaultArchive
		{
			get
			{
				return new LocalizedString("RetentionActionTypeDefaultArchive", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000152 RID: 338
		// (get) Token: 0x0600015B RID: 347 RVA: 0x00008BAD File Offset: 0x00006DAD
		public static LocalizedString RemoveOptionalRPTConfirmation
		{
			get
			{
				return new LocalizedString("RemoveOptionalRPTConfirmation", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000153 RID: 339
		// (get) Token: 0x0600015C RID: 348 RVA: 0x00008BCB File Offset: 0x00006DCB
		public static LocalizedString DevicePolicyUpdateTimeLabel
		{
			get
			{
				return new LocalizedString("DevicePolicyUpdateTimeLabel", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000154 RID: 340
		// (get) Token: 0x0600015D RID: 349 RVA: 0x00008BE9 File Offset: 0x00006DE9
		public static LocalizedString MyselfLiveFormat
		{
			get
			{
				return new LocalizedString("MyselfLiveFormat", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000155 RID: 341
		// (get) Token: 0x0600015E RID: 350 RVA: 0x00008C07 File Offset: 0x00006E07
		public static LocalizedString EmailDomain
		{
			get
			{
				return new LocalizedString("EmailDomain", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000156 RID: 342
		// (get) Token: 0x0600015F RID: 351 RVA: 0x00008C25 File Offset: 0x00006E25
		public static LocalizedString RequireSenderAuthTrue
		{
			get
			{
				return new LocalizedString("RequireSenderAuthTrue", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000157 RID: 343
		// (get) Token: 0x06000160 RID: 352 RVA: 0x00008C43 File Offset: 0x00006E43
		public static LocalizedString InstallFromPrivateUrlInformation
		{
			get
			{
				return new LocalizedString("InstallFromPrivateUrlInformation", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000158 RID: 344
		// (get) Token: 0x06000161 RID: 353 RVA: 0x00008C61 File Offset: 0x00006E61
		public static LocalizedString PhoneLabel
		{
			get
			{
				return new LocalizedString("PhoneLabel", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000159 RID: 345
		// (get) Token: 0x06000162 RID: 354 RVA: 0x00008C7F File Offset: 0x00006E7F
		public static LocalizedString SubscriptionAction
		{
			get
			{
				return new LocalizedString("SubscriptionAction", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700015A RID: 346
		// (get) Token: 0x06000163 RID: 355 RVA: 0x00008C9D File Offset: 0x00006E9D
		public static LocalizedString Weeks
		{
			get
			{
				return new LocalizedString("Weeks", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000164 RID: 356 RVA: 0x00008CBC File Offset: 0x00006EBC
		public static LocalizedString ImportContactListPage2Result(string filename)
		{
			return new LocalizedString("ImportContactListPage2Result", "", false, false, OwaOptionStrings.ResourceManager, new object[]
			{
				filename
			});
		}

		// Token: 0x1700015B RID: 347
		// (get) Token: 0x06000165 RID: 357 RVA: 0x00008CEB File Offset: 0x00006EEB
		public static LocalizedString InboxRuleForwardRedirectGroupText
		{
			get
			{
				return new LocalizedString("InboxRuleForwardRedirectGroupText", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700015C RID: 348
		// (get) Token: 0x06000166 RID: 358 RVA: 0x00008D09 File Offset: 0x00006F09
		public static LocalizedString DisplayName
		{
			get
			{
				return new LocalizedString("DisplayName", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700015D RID: 349
		// (get) Token: 0x06000167 RID: 359 RVA: 0x00008D27 File Offset: 0x00006F27
		public static LocalizedString MembershipApprovalLabel
		{
			get
			{
				return new LocalizedString("MembershipApprovalLabel", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700015E RID: 350
		// (get) Token: 0x06000168 RID: 360 RVA: 0x00008D45 File Offset: 0x00006F45
		public static LocalizedString InboxRuleSendTextMessageNotificationToActionFlyOutText
		{
			get
			{
				return new LocalizedString("InboxRuleSendTextMessageNotificationToActionFlyOutText", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700015F RID: 351
		// (get) Token: 0x06000169 RID: 361 RVA: 0x00008D63 File Offset: 0x00006F63
		public static LocalizedString TeamMailboxSPSiteString
		{
			get
			{
				return new LocalizedString("TeamMailboxSPSiteString", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000160 RID: 352
		// (get) Token: 0x0600016A RID: 362 RVA: 0x00008D81 File Offset: 0x00006F81
		public static LocalizedString InboxRuleSubjectOrBodyContainsConditionFlyOutText
		{
			get
			{
				return new LocalizedString("InboxRuleSubjectOrBodyContainsConditionFlyOutText", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000161 RID: 353
		// (get) Token: 0x0600016B RID: 363 RVA: 0x00008D9F File Offset: 0x00006F9F
		public static LocalizedString PleaseWait
		{
			get
			{
				return new LocalizedString("PleaseWait", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000162 RID: 354
		// (get) Token: 0x0600016C RID: 364 RVA: 0x00008DBD File Offset: 0x00006FBD
		public static LocalizedString JoinRestrictionOpen
		{
			get
			{
				return new LocalizedString("JoinRestrictionOpen", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000163 RID: 355
		// (get) Token: 0x0600016D RID: 365 RVA: 0x00008DDB File Offset: 0x00006FDB
		public static LocalizedString CalendarSharingConfirmDeletionSingle
		{
			get
			{
				return new LocalizedString("CalendarSharingConfirmDeletionSingle", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000164 RID: 356
		// (get) Token: 0x0600016E RID: 366 RVA: 0x00008DF9 File Offset: 0x00006FF9
		public static LocalizedString InboxRuleSubjectContainsConditionPreCannedText
		{
			get
			{
				return new LocalizedString("InboxRuleSubjectContainsConditionPreCannedText", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000165 RID: 357
		// (get) Token: 0x0600016F RID: 367 RVA: 0x00008E17 File Offset: 0x00007017
		public static LocalizedString CalendarPublishingStart
		{
			get
			{
				return new LocalizedString("CalendarPublishingStart", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000166 RID: 358
		// (get) Token: 0x06000170 RID: 368 RVA: 0x00008E35 File Offset: 0x00007035
		public static LocalizedString TextMessagingSlabMessageNotificationOnly
		{
			get
			{
				return new LocalizedString("TextMessagingSlabMessageNotificationOnly", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000167 RID: 359
		// (get) Token: 0x06000171 RID: 369 RVA: 0x00008E53 File Offset: 0x00007053
		public static LocalizedString RequirementsRestrictedDescription
		{
			get
			{
				return new LocalizedString("RequirementsRestrictedDescription", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000168 RID: 360
		// (get) Token: 0x06000172 RID: 370 RVA: 0x00008E71 File Offset: 0x00007071
		public static LocalizedString SelectAUser
		{
			get
			{
				return new LocalizedString("SelectAUser", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000169 RID: 361
		// (get) Token: 0x06000173 RID: 371 RVA: 0x00008E8F File Offset: 0x0000708F
		public static LocalizedString NotificationStepOneMessage
		{
			get
			{
				return new LocalizedString("NotificationStepOneMessage", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700016A RID: 362
		// (get) Token: 0x06000174 RID: 372 RVA: 0x00008EAD File Offset: 0x000070AD
		public static LocalizedString QLPushEmail
		{
			get
			{
				return new LocalizedString("QLPushEmail", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700016B RID: 363
		// (get) Token: 0x06000175 RID: 373 RVA: 0x00008ECB File Offset: 0x000070CB
		public static LocalizedString NewInboxRuleTitle
		{
			get
			{
				return new LocalizedString("NewInboxRuleTitle", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700016C RID: 364
		// (get) Token: 0x06000176 RID: 374 RVA: 0x00008EE9 File Offset: 0x000070E9
		public static LocalizedString SendToKnownContactsText
		{
			get
			{
				return new LocalizedString("SendToKnownContactsText", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700016D RID: 365
		// (get) Token: 0x06000177 RID: 375 RVA: 0x00008F07 File Offset: 0x00007107
		public static LocalizedString IncomingAuthenticationSpa
		{
			get
			{
				return new LocalizedString("IncomingAuthenticationSpa", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700016E RID: 366
		// (get) Token: 0x06000178 RID: 376 RVA: 0x00008F25 File Offset: 0x00007125
		public static LocalizedString MailboxUsageUnitGB
		{
			get
			{
				return new LocalizedString("MailboxUsageUnitGB", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700016F RID: 367
		// (get) Token: 0x06000179 RID: 377 RVA: 0x00008F43 File Offset: 0x00007143
		public static LocalizedString MessageTrackingDeliveredEvent
		{
			get
			{
				return new LocalizedString("MessageTrackingDeliveredEvent", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000170 RID: 368
		// (get) Token: 0x0600017A RID: 378 RVA: 0x00008F61 File Offset: 0x00007161
		public static LocalizedString SelectOneOrMoreText
		{
			get
			{
				return new LocalizedString("SelectOneOrMoreText", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000171 RID: 369
		// (get) Token: 0x0600017B RID: 379 RVA: 0x00008F7F File Offset: 0x0000717F
		public static LocalizedString InboxRuleForwardAsAttachmentToActionText
		{
			get
			{
				return new LocalizedString("InboxRuleForwardAsAttachmentToActionText", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000172 RID: 370
		// (get) Token: 0x0600017C RID: 380 RVA: 0x00008F9D File Offset: 0x0000719D
		public static LocalizedString DontSeeMyRegionOrMobileOperator
		{
			get
			{
				return new LocalizedString("DontSeeMyRegionOrMobileOperator", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000173 RID: 371
		// (get) Token: 0x0600017D RID: 381 RVA: 0x00008FBB File Offset: 0x000071BB
		public static LocalizedString PreviewMarkAsReadDelaytimeTextPost
		{
			get
			{
				return new LocalizedString("PreviewMarkAsReadDelaytimeTextPost", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000174 RID: 372
		// (get) Token: 0x0600017E RID: 382 RVA: 0x00008FD9 File Offset: 0x000071D9
		public static LocalizedString NoMessageClassificationAvailable
		{
			get
			{
				return new LocalizedString("NoMessageClassificationAvailable", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000175 RID: 373
		// (get) Token: 0x0600017F RID: 383 RVA: 0x00008FF7 File Offset: 0x000071F7
		public static LocalizedString TeamMailboxTabPropertiesHelpString
		{
			get
			{
				return new LocalizedString("TeamMailboxTabPropertiesHelpString", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000176 RID: 374
		// (get) Token: 0x06000180 RID: 384 RVA: 0x00009015 File Offset: 0x00007215
		public static LocalizedString TeamMailboxManagementString1
		{
			get
			{
				return new LocalizedString("TeamMailboxManagementString1", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000177 RID: 375
		// (get) Token: 0x06000181 RID: 385 RVA: 0x00009033 File Offset: 0x00007233
		public static LocalizedString RetentionPeriodHeader
		{
			get
			{
				return new LocalizedString("RetentionPeriodHeader", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000178 RID: 376
		// (get) Token: 0x06000182 RID: 386 RVA: 0x00009051 File Offset: 0x00007251
		public static LocalizedString Phone
		{
			get
			{
				return new LocalizedString("Phone", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000179 RID: 377
		// (get) Token: 0x06000183 RID: 387 RVA: 0x0000906F File Offset: 0x0000726F
		public static LocalizedString WhoHasPermission
		{
			get
			{
				return new LocalizedString("WhoHasPermission", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700017A RID: 378
		// (get) Token: 0x06000184 RID: 388 RVA: 0x0000908D File Offset: 0x0000728D
		public static LocalizedString NotAvailable
		{
			get
			{
				return new LocalizedString("NotAvailable", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700017B RID: 379
		// (get) Token: 0x06000185 RID: 389 RVA: 0x000090AB File Offset: 0x000072AB
		public static LocalizedString FlaggedForActionConditionFormat
		{
			get
			{
				return new LocalizedString("FlaggedForActionConditionFormat", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700017C RID: 380
		// (get) Token: 0x06000186 RID: 390 RVA: 0x000090C9 File Offset: 0x000072C9
		public static LocalizedString EndTimeText
		{
			get
			{
				return new LocalizedString("EndTimeText", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700017D RID: 381
		// (get) Token: 0x06000187 RID: 391 RVA: 0x000090E7 File Offset: 0x000072E7
		public static LocalizedString BookingWindowInDays
		{
			get
			{
				return new LocalizedString("BookingWindowInDays", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700017E RID: 382
		// (get) Token: 0x06000188 RID: 392 RVA: 0x00009105 File Offset: 0x00007305
		public static LocalizedString RemovingPreviewPhoto
		{
			get
			{
				return new LocalizedString("RemovingPreviewPhoto", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700017F RID: 383
		// (get) Token: 0x06000189 RID: 393 RVA: 0x00009123 File Offset: 0x00007323
		public static LocalizedString CalendarDiagnosticLogSlab
		{
			get
			{
				return new LocalizedString("CalendarDiagnosticLogSlab", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000180 RID: 384
		// (get) Token: 0x0600018A RID: 394 RVA: 0x00009141 File Offset: 0x00007341
		public static LocalizedString ModerationNotification
		{
			get
			{
				return new LocalizedString("ModerationNotification", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000181 RID: 385
		// (get) Token: 0x0600018B RID: 395 RVA: 0x0000915F File Offset: 0x0000735F
		public static LocalizedString VoicemailWizardPinlessOptionsText
		{
			get
			{
				return new LocalizedString("VoicemailWizardPinlessOptionsText", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000182 RID: 386
		// (get) Token: 0x0600018C RID: 396 RVA: 0x0000917D File Offset: 0x0000737D
		public static LocalizedString VoicemailClearSettings
		{
			get
			{
				return new LocalizedString("VoicemailClearSettings", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000183 RID: 387
		// (get) Token: 0x0600018D RID: 397 RVA: 0x0000919B File Offset: 0x0000739B
		public static LocalizedString PersonalGroups
		{
			get
			{
				return new LocalizedString("PersonalGroups", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000184 RID: 388
		// (get) Token: 0x0600018E RID: 398 RVA: 0x000091B9 File Offset: 0x000073B9
		public static LocalizedString DistributionGroupText
		{
			get
			{
				return new LocalizedString("DistributionGroupText", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000185 RID: 389
		// (get) Token: 0x0600018F RID: 399 RVA: 0x000091D7 File Offset: 0x000073D7
		public static LocalizedString ProfileContactNumbers
		{
			get
			{
				return new LocalizedString("ProfileContactNumbers", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000186 RID: 390
		// (get) Token: 0x06000190 RID: 400 RVA: 0x000091F5 File Offset: 0x000073F5
		public static LocalizedString DeliveryReportFor
		{
			get
			{
				return new LocalizedString("DeliveryReportFor", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000187 RID: 391
		// (get) Token: 0x06000191 RID: 401 RVA: 0x00009213 File Offset: 0x00007413
		public static LocalizedString InboxRuleRedirectToActionFlyOutText
		{
			get
			{
				return new LocalizedString("InboxRuleRedirectToActionFlyOutText", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000188 RID: 392
		// (get) Token: 0x06000192 RID: 402 RVA: 0x00009231 File Offset: 0x00007431
		public static LocalizedString AutomateProcessing
		{
			get
			{
				return new LocalizedString("AutomateProcessing", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000189 RID: 393
		// (get) Token: 0x06000193 RID: 403 RVA: 0x0000924F File Offset: 0x0000744F
		public static LocalizedString CalendarPublishingAccessLevel
		{
			get
			{
				return new LocalizedString("CalendarPublishingAccessLevel", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700018A RID: 394
		// (get) Token: 0x06000194 RID: 404 RVA: 0x0000926D File Offset: 0x0000746D
		public static LocalizedString ForwardEmailTo
		{
			get
			{
				return new LocalizedString("ForwardEmailTo", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700018B RID: 395
		// (get) Token: 0x06000195 RID: 405 RVA: 0x0000928B File Offset: 0x0000748B
		public static LocalizedString SettingUpProtocolAccess
		{
			get
			{
				return new LocalizedString("SettingUpProtocolAccess", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700018C RID: 396
		// (get) Token: 0x06000196 RID: 406 RVA: 0x000092A9 File Offset: 0x000074A9
		public static LocalizedString AdminTools
		{
			get
			{
				return new LocalizedString("AdminTools", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700018D RID: 397
		// (get) Token: 0x06000197 RID: 407 RVA: 0x000092C7 File Offset: 0x000074C7
		public static LocalizedString InstalledExtensionDescription
		{
			get
			{
				return new LocalizedString("InstalledExtensionDescription", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700018E RID: 398
		// (get) Token: 0x06000198 RID: 408 RVA: 0x000092E5 File Offset: 0x000074E5
		public static LocalizedString EmailComposeModeInstruction
		{
			get
			{
				return new LocalizedString("EmailComposeModeInstruction", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700018F RID: 399
		// (get) Token: 0x06000199 RID: 409 RVA: 0x00009303 File Offset: 0x00007503
		public static LocalizedString InboxRuleDeleteMessageActionText
		{
			get
			{
				return new LocalizedString("InboxRuleDeleteMessageActionText", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000190 RID: 400
		// (get) Token: 0x0600019A RID: 410 RVA: 0x00009321 File Offset: 0x00007521
		public static LocalizedString SummaryToDate
		{
			get
			{
				return new LocalizedString("SummaryToDate", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000191 RID: 401
		// (get) Token: 0x0600019B RID: 411 RVA: 0x0000933F File Offset: 0x0000753F
		public static LocalizedString Extension
		{
			get
			{
				return new LocalizedString("Extension", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000192 RID: 402
		// (get) Token: 0x0600019C RID: 412 RVA: 0x0000935D File Offset: 0x0000755D
		public static LocalizedString CalendarSharingConfirmDeletionMultiple
		{
			get
			{
				return new LocalizedString("CalendarSharingConfirmDeletionMultiple", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000193 RID: 403
		// (get) Token: 0x0600019D RID: 413 RVA: 0x0000937B File Offset: 0x0000757B
		public static LocalizedString Depart
		{
			get
			{
				return new LocalizedString("Depart", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000194 RID: 404
		// (get) Token: 0x0600019E RID: 414 RVA: 0x00009399 File Offset: 0x00007599
		public static LocalizedString EmailNotificationsLink
		{
			get
			{
				return new LocalizedString("EmailNotificationsLink", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000195 RID: 405
		// (get) Token: 0x0600019F RID: 415 RVA: 0x000093B7 File Offset: 0x000075B7
		public static LocalizedString OpenPreviousItem
		{
			get
			{
				return new LocalizedString("OpenPreviousItem", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000196 RID: 406
		// (get) Token: 0x060001A0 RID: 416 RVA: 0x000093D5 File Offset: 0x000075D5
		public static LocalizedString RPTPickerDialogTitle
		{
			get
			{
				return new LocalizedString("RPTPickerDialogTitle", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000197 RID: 407
		// (get) Token: 0x060001A1 RID: 417 RVA: 0x000093F3 File Offset: 0x000075F3
		public static LocalizedString CheckForForgottenAttachments
		{
			get
			{
				return new LocalizedString("CheckForForgottenAttachments", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000198 RID: 408
		// (get) Token: 0x060001A2 RID: 418 RVA: 0x00009411 File Offset: 0x00007611
		public static LocalizedString FaxLabel
		{
			get
			{
				return new LocalizedString("FaxLabel", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000199 RID: 409
		// (get) Token: 0x060001A3 RID: 419 RVA: 0x0000942F File Offset: 0x0000762F
		public static LocalizedString SentTime
		{
			get
			{
				return new LocalizedString("SentTime", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700019A RID: 410
		// (get) Token: 0x060001A4 RID: 420 RVA: 0x0000944D File Offset: 0x0000764D
		public static LocalizedString DeviceTypeHeaderText
		{
			get
			{
				return new LocalizedString("DeviceTypeHeaderText", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700019B RID: 411
		// (get) Token: 0x060001A5 RID: 421 RVA: 0x0000946B File Offset: 0x0000766B
		public static LocalizedString RPTExpireNever
		{
			get
			{
				return new LocalizedString("RPTExpireNever", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700019C RID: 412
		// (get) Token: 0x060001A6 RID: 422 RVA: 0x00009489 File Offset: 0x00007689
		public static LocalizedString TeamMailboxEmailAddressString
		{
			get
			{
				return new LocalizedString("TeamMailboxEmailAddressString", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700019D RID: 413
		// (get) Token: 0x060001A7 RID: 423 RVA: 0x000094A7 File Offset: 0x000076A7
		public static LocalizedString InboxRuleWithinSizeRangeConditionText
		{
			get
			{
				return new LocalizedString("InboxRuleWithinSizeRangeConditionText", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700019E RID: 414
		// (get) Token: 0x060001A8 RID: 424 RVA: 0x000094C5 File Offset: 0x000076C5
		public static LocalizedString Week
		{
			get
			{
				return new LocalizedString("Week", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700019F RID: 415
		// (get) Token: 0x060001A9 RID: 425 RVA: 0x000094E3 File Offset: 0x000076E3
		public static LocalizedString WithinDateRangeConditionFormat
		{
			get
			{
				return new LocalizedString("WithinDateRangeConditionFormat", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170001A0 RID: 416
		// (get) Token: 0x060001AA RID: 426 RVA: 0x00009501 File Offset: 0x00007701
		public static LocalizedString DisplayNameNotSetError
		{
			get
			{
				return new LocalizedString("DisplayNameNotSetError", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170001A1 RID: 417
		// (get) Token: 0x060001AB RID: 427 RVA: 0x0000951F File Offset: 0x0000771F
		public static LocalizedString FirstDayOfWeek
		{
			get
			{
				return new LocalizedString("FirstDayOfWeek", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170001A2 RID: 418
		// (get) Token: 0x060001AC RID: 428 RVA: 0x0000953D File Offset: 0x0000773D
		public static LocalizedString ProcessExternalMeetingMessagesCheckBoxText
		{
			get
			{
				return new LocalizedString("ProcessExternalMeetingMessagesCheckBoxText", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170001A3 RID: 419
		// (get) Token: 0x060001AD RID: 429 RVA: 0x0000955B File Offset: 0x0000775B
		public static LocalizedString DepartRestrictionClosed
		{
			get
			{
				return new LocalizedString("DepartRestrictionClosed", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170001A4 RID: 420
		// (get) Token: 0x060001AE RID: 430 RVA: 0x00009579 File Offset: 0x00007779
		public static LocalizedString MailTipShortLabel
		{
			get
			{
				return new LocalizedString("MailTipShortLabel", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170001A5 RID: 421
		// (get) Token: 0x060001AF RID: 431 RVA: 0x00009597 File Offset: 0x00007797
		public static LocalizedString StateOrProvinceLabel
		{
			get
			{
				return new LocalizedString("StateOrProvinceLabel", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170001A6 RID: 422
		// (get) Token: 0x060001B0 RID: 432 RVA: 0x000095B5 File Offset: 0x000077B5
		public static LocalizedString PrimaryEmailAddress
		{
			get
			{
				return new LocalizedString("PrimaryEmailAddress", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170001A7 RID: 423
		// (get) Token: 0x060001B1 RID: 433 RVA: 0x000095D3 File Offset: 0x000077D3
		public static LocalizedString AllowedSendersEmptyLabel
		{
			get
			{
				return new LocalizedString("AllowedSendersEmptyLabel", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170001A8 RID: 424
		// (get) Token: 0x060001B2 RID: 434 RVA: 0x000095F1 File Offset: 0x000077F1
		public static LocalizedString HotmailSubscription
		{
			get
			{
				return new LocalizedString("HotmailSubscription", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170001A9 RID: 425
		// (get) Token: 0x060001B3 RID: 435 RVA: 0x0000960F File Offset: 0x0000780F
		public static LocalizedString DeviceIDLabel
		{
			get
			{
				return new LocalizedString("DeviceIDLabel", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170001AA RID: 426
		// (get) Token: 0x060001B4 RID: 436 RVA: 0x0000962D File Offset: 0x0000782D
		public static LocalizedString AlwaysShowFrom
		{
			get
			{
				return new LocalizedString("AlwaysShowFrom", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170001AB RID: 427
		// (get) Token: 0x060001B5 RID: 437 RVA: 0x0000964B File Offset: 0x0000784B
		public static LocalizedString SearchButtonText
		{
			get
			{
				return new LocalizedString("SearchButtonText", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170001AC RID: 428
		// (get) Token: 0x060001B6 RID: 438 RVA: 0x00009669 File Offset: 0x00007869
		public static LocalizedString ViewRPTDescriptionLabel
		{
			get
			{
				return new LocalizedString("ViewRPTDescriptionLabel", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170001AD RID: 429
		// (get) Token: 0x060001B7 RID: 439 RVA: 0x00009687 File Offset: 0x00007887
		public static LocalizedString Hour
		{
			get
			{
				return new LocalizedString("Hour", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170001AE RID: 430
		// (get) Token: 0x060001B8 RID: 440 RVA: 0x000096A5 File Offset: 0x000078A5
		public static LocalizedString FlagStatusDialogTitle
		{
			get
			{
				return new LocalizedString("FlagStatusDialogTitle", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170001AF RID: 431
		// (get) Token: 0x060001B9 RID: 441 RVA: 0x000096C3 File Offset: 0x000078C3
		public static LocalizedString NewSubscriptionProgress
		{
			get
			{
				return new LocalizedString("NewSubscriptionProgress", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170001B0 RID: 432
		// (get) Token: 0x060001BA RID: 442 RVA: 0x000096E1 File Offset: 0x000078E1
		public static LocalizedString EditProfilePhoto
		{
			get
			{
				return new LocalizedString("EditProfilePhoto", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170001B1 RID: 433
		// (get) Token: 0x060001BB RID: 443 RVA: 0x000096FF File Offset: 0x000078FF
		public static LocalizedString InstallFromPrivateUrl
		{
			get
			{
				return new LocalizedString("InstallFromPrivateUrl", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170001B2 RID: 434
		// (get) Token: 0x060001BC RID: 444 RVA: 0x0000971D File Offset: 0x0000791D
		public static LocalizedString DeleteGroupsConfirmation
		{
			get
			{
				return new LocalizedString("DeleteGroupsConfirmation", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170001B3 RID: 435
		// (get) Token: 0x060001BD RID: 445 RVA: 0x0000973B File Offset: 0x0000793B
		public static LocalizedString OwnedGroups
		{
			get
			{
				return new LocalizedString("OwnedGroups", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170001B4 RID: 436
		// (get) Token: 0x060001BE RID: 446 RVA: 0x00009759 File Offset: 0x00007959
		public static LocalizedString NewDistributionGroupTitle
		{
			get
			{
				return new LocalizedString("NewDistributionGroupTitle", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170001B5 RID: 437
		// (get) Token: 0x060001BF RID: 447 RVA: 0x00009777 File Offset: 0x00007977
		public static LocalizedString JunkEmailConfiguration
		{
			get
			{
				return new LocalizedString("JunkEmailConfiguration", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170001B6 RID: 438
		// (get) Token: 0x060001C0 RID: 448 RVA: 0x00009795 File Offset: 0x00007995
		public static LocalizedString ProfileGeneral
		{
			get
			{
				return new LocalizedString("ProfileGeneral", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170001B7 RID: 439
		// (get) Token: 0x060001C1 RID: 449 RVA: 0x000097B3 File Offset: 0x000079B3
		public static LocalizedString CalendarSharingOutlookNote
		{
			get
			{
				return new LocalizedString("CalendarSharingOutlookNote", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170001B8 RID: 440
		// (get) Token: 0x060001C2 RID: 450 RVA: 0x000097D1 File Offset: 0x000079D1
		public static LocalizedString RemoveOptionalRPTsConfirmation
		{
			get
			{
				return new LocalizedString("RemoveOptionalRPTsConfirmation", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170001B9 RID: 441
		// (get) Token: 0x060001C3 RID: 451 RVA: 0x000097EF File Offset: 0x000079EF
		public static LocalizedString VerificationSuccessTitle
		{
			get
			{
				return new LocalizedString("VerificationSuccessTitle", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170001BA RID: 442
		// (get) Token: 0x060001C4 RID: 452 RVA: 0x0000980D File Offset: 0x00007A0D
		public static LocalizedString ResponseMessageSlab
		{
			get
			{
				return new LocalizedString("ResponseMessageSlab", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170001BB RID: 443
		// (get) Token: 0x060001C5 RID: 453 RVA: 0x0000982B File Offset: 0x00007A2B
		public static LocalizedString TeamMailboxMaintenanceString
		{
			get
			{
				return new LocalizedString("TeamMailboxMaintenanceString", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170001BC RID: 444
		// (get) Token: 0x060001C6 RID: 454 RVA: 0x00009849 File Offset: 0x00007A49
		public static LocalizedString UrlLabelText
		{
			get
			{
				return new LocalizedString("UrlLabelText", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170001BD RID: 445
		// (get) Token: 0x060001C7 RID: 455 RVA: 0x00009867 File Offset: 0x00007A67
		public static LocalizedString DepartRestrictionOpen
		{
			get
			{
				return new LocalizedString("DepartRestrictionOpen", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170001BE RID: 446
		// (get) Token: 0x060001C8 RID: 456 RVA: 0x00009885 File Offset: 0x00007A85
		public static LocalizedString PendingWipeCommandSentLabel
		{
			get
			{
				return new LocalizedString("PendingWipeCommandSentLabel", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170001BF RID: 447
		// (get) Token: 0x060001C9 RID: 457 RVA: 0x000098A3 File Offset: 0x00007AA3
		public static LocalizedString SubjectOrBodyContainsConditionFormat
		{
			get
			{
				return new LocalizedString("SubjectOrBodyContainsConditionFormat", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170001C0 RID: 448
		// (get) Token: 0x060001CA RID: 458 RVA: 0x000098C1 File Offset: 0x00007AC1
		public static LocalizedString LeaveMailOnServerLabel
		{
			get
			{
				return new LocalizedString("LeaveMailOnServerLabel", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170001C1 RID: 449
		// (get) Token: 0x060001CB RID: 459 RVA: 0x000098DF File Offset: 0x00007ADF
		public static LocalizedString JoinRestrictionClosed
		{
			get
			{
				return new LocalizedString("JoinRestrictionClosed", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170001C2 RID: 450
		// (get) Token: 0x060001CC RID: 460 RVA: 0x000098FD File Offset: 0x00007AFD
		public static LocalizedString ScheduleOnlyDuringWorkHours
		{
			get
			{
				return new LocalizedString("ScheduleOnlyDuringWorkHours", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170001C3 RID: 451
		// (get) Token: 0x060001CD RID: 461 RVA: 0x0000991B File Offset: 0x00007B1B
		public static LocalizedString ImportanceDialogLabel
		{
			get
			{
				return new LocalizedString("ImportanceDialogLabel", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170001C4 RID: 452
		// (get) Token: 0x060001CE RID: 462 RVA: 0x00009939 File Offset: 0x00007B39
		public static LocalizedString CalendarPublishingLinkNotes
		{
			get
			{
				return new LocalizedString("CalendarPublishingLinkNotes", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170001C5 RID: 453
		// (get) Token: 0x060001CF RID: 463 RVA: 0x00009957 File Offset: 0x00007B57
		public static LocalizedString InboxRuleSentOnlyToMeConditionFlyOutText
		{
			get
			{
				return new LocalizedString("InboxRuleSentOnlyToMeConditionFlyOutText", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170001C6 RID: 454
		// (get) Token: 0x060001D0 RID: 464 RVA: 0x00009975 File Offset: 0x00007B75
		public static LocalizedString InternalMessageInstruction
		{
			get
			{
				return new LocalizedString("InternalMessageInstruction", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170001C7 RID: 455
		// (get) Token: 0x060001D1 RID: 465 RVA: 0x00009993 File Offset: 0x00007B93
		public static LocalizedString JunkEmailDisabled
		{
			get
			{
				return new LocalizedString("JunkEmailDisabled", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170001C8 RID: 456
		// (get) Token: 0x060001D2 RID: 466 RVA: 0x000099B1 File Offset: 0x00007BB1
		public static LocalizedString InboxRuleForwardAsAttachmentToActionFlyOutText
		{
			get
			{
				return new LocalizedString("InboxRuleForwardAsAttachmentToActionFlyOutText", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170001C9 RID: 457
		// (get) Token: 0x060001D3 RID: 467 RVA: 0x000099CF File Offset: 0x00007BCF
		public static LocalizedString WarningNote
		{
			get
			{
				return new LocalizedString("WarningNote", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170001CA RID: 458
		// (get) Token: 0x060001D4 RID: 468 RVA: 0x000099ED File Offset: 0x00007BED
		public static LocalizedString HomePagePrimaryLink
		{
			get
			{
				return new LocalizedString("HomePagePrimaryLink", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170001CB RID: 459
		// (get) Token: 0x060001D5 RID: 469 RVA: 0x00009A0B File Offset: 0x00007C0B
		public static LocalizedString LimitDuration
		{
			get
			{
				return new LocalizedString("LimitDuration", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170001CC RID: 460
		// (get) Token: 0x060001D6 RID: 470 RVA: 0x00009A29 File Offset: 0x00007C29
		public static LocalizedString MailboxUsageExceededText
		{
			get
			{
				return new LocalizedString("MailboxUsageExceededText", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170001CD RID: 461
		// (get) Token: 0x060001D7 RID: 471 RVA: 0x00009A47 File Offset: 0x00007C47
		public static LocalizedString UnSupportedRule
		{
			get
			{
				return new LocalizedString("UnSupportedRule", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170001CE RID: 462
		// (get) Token: 0x060001D8 RID: 472 RVA: 0x00009A65 File Offset: 0x00007C65
		public static LocalizedString DeviceModelLabel
		{
			get
			{
				return new LocalizedString("DeviceModelLabel", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170001CF RID: 463
		// (get) Token: 0x060001D9 RID: 473 RVA: 0x00009A83 File Offset: 0x00007C83
		public static LocalizedString NotificationLinksMessage
		{
			get
			{
				return new LocalizedString("NotificationLinksMessage", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170001D0 RID: 464
		// (get) Token: 0x060001DA RID: 474 RVA: 0x00009AA1 File Offset: 0x00007CA1
		public static LocalizedString ResourceSlab
		{
			get
			{
				return new LocalizedString("ResourceSlab", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170001D1 RID: 465
		// (get) Token: 0x060001DB RID: 475 RVA: 0x00009ABF File Offset: 0x00007CBF
		public static LocalizedString RetentionPolicyTagPageTitle
		{
			get
			{
				return new LocalizedString("RetentionPolicyTagPageTitle", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060001DC RID: 476 RVA: 0x00009AE0 File Offset: 0x00007CE0
		public static LocalizedString LargeRecipientList(int number)
		{
			return new LocalizedString("LargeRecipientList", "", false, false, OwaOptionStrings.ResourceManager, new object[]
			{
				number
			});
		}

		// Token: 0x170001D2 RID: 466
		// (get) Token: 0x060001DD RID: 477 RVA: 0x00009B14 File Offset: 0x00007D14
		public static LocalizedString AllBookInPolicyText
		{
			get
			{
				return new LocalizedString("AllBookInPolicyText", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170001D3 RID: 467
		// (get) Token: 0x060001DE RID: 478 RVA: 0x00009B32 File Offset: 0x00007D32
		public static LocalizedString MessageFormatHtml
		{
			get
			{
				return new LocalizedString("MessageFormatHtml", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170001D4 RID: 468
		// (get) Token: 0x060001DF RID: 479 RVA: 0x00009B50 File Offset: 0x00007D50
		public static LocalizedString FridayCheckBoxText
		{
			get
			{
				return new LocalizedString("FridayCheckBoxText", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170001D5 RID: 469
		// (get) Token: 0x060001E0 RID: 480 RVA: 0x00009B6E File Offset: 0x00007D6E
		public static LocalizedString InboxRuleMyNameInCcBoxConditionText
		{
			get
			{
				return new LocalizedString("InboxRuleMyNameInCcBoxConditionText", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170001D6 RID: 470
		// (get) Token: 0x060001E1 RID: 481 RVA: 0x00009B8C File Offset: 0x00007D8C
		public static LocalizedString PreviewMarkAsReadDelaytimeErrorMessage
		{
			get
			{
				return new LocalizedString("PreviewMarkAsReadDelaytimeErrorMessage", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170001D7 RID: 471
		// (get) Token: 0x060001E2 RID: 482 RVA: 0x00009BAA File Offset: 0x00007DAA
		public static LocalizedString JunkEmailValidationErrorMessage
		{
			get
			{
				return new LocalizedString("JunkEmailValidationErrorMessage", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170001D8 RID: 472
		// (get) Token: 0x060001E3 RID: 483 RVA: 0x00009BC8 File Offset: 0x00007DC8
		public static LocalizedString TeamMailboxTabUsersHelpString2
		{
			get
			{
				return new LocalizedString("TeamMailboxTabUsersHelpString2", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170001D9 RID: 473
		// (get) Token: 0x060001E4 RID: 484 RVA: 0x00009BE6 File Offset: 0x00007DE6
		public static LocalizedString AllRequestInPolicyText
		{
			get
			{
				return new LocalizedString("AllRequestInPolicyText", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170001DA RID: 474
		// (get) Token: 0x060001E5 RID: 485 RVA: 0x00009C04 File Offset: 0x00007E04
		public static LocalizedString ImapSubscription
		{
			get
			{
				return new LocalizedString("ImapSubscription", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170001DB RID: 475
		// (get) Token: 0x060001E6 RID: 486 RVA: 0x00009C22 File Offset: 0x00007E22
		public static LocalizedString InboxRuleWithImportanceConditionFlyOutText
		{
			get
			{
				return new LocalizedString("InboxRuleWithImportanceConditionFlyOutText", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170001DC RID: 476
		// (get) Token: 0x060001E7 RID: 487 RVA: 0x00009C40 File Offset: 0x00007E40
		public static LocalizedString TeamMailboxDisplayNameString
		{
			get
			{
				return new LocalizedString("TeamMailboxDisplayNameString", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170001DD RID: 477
		// (get) Token: 0x060001E8 RID: 488 RVA: 0x00009C5E File Offset: 0x00007E5E
		public static LocalizedString TeamMailboxManagementString3
		{
			get
			{
				return new LocalizedString("TeamMailboxManagementString3", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170001DE RID: 478
		// (get) Token: 0x060001E9 RID: 489 RVA: 0x00009C7C File Offset: 0x00007E7C
		public static LocalizedString ConflictPercentageAllowed
		{
			get
			{
				return new LocalizedString("ConflictPercentageAllowed", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170001DF RID: 479
		// (get) Token: 0x060001EA RID: 490 RVA: 0x00009C9A File Offset: 0x00007E9A
		public static LocalizedString ImportanceDialogTitle
		{
			get
			{
				return new LocalizedString("ImportanceDialogTitle", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170001E0 RID: 480
		// (get) Token: 0x060001EB RID: 491 RVA: 0x00009CB8 File Offset: 0x00007EB8
		public static LocalizedString SecurityGroupText
		{
			get
			{
				return new LocalizedString("SecurityGroupText", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170001E1 RID: 481
		// (get) Token: 0x060001EC RID: 492 RVA: 0x00009CD6 File Offset: 0x00007ED6
		public static LocalizedString SubjectContainsConditionFormat
		{
			get
			{
				return new LocalizedString("SubjectContainsConditionFormat", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170001E2 RID: 482
		// (get) Token: 0x060001ED RID: 493 RVA: 0x00009CF4 File Offset: 0x00007EF4
		public static LocalizedString VoicemailStep1
		{
			get
			{
				return new LocalizedString("VoicemailStep1", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170001E3 RID: 483
		// (get) Token: 0x060001EE RID: 494 RVA: 0x00009D12 File Offset: 0x00007F12
		public static LocalizedString ImportContactListPage1Step1
		{
			get
			{
				return new LocalizedString("ImportContactListPage1Step1", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170001E4 RID: 484
		// (get) Token: 0x060001EF RID: 495 RVA: 0x00009D30 File Offset: 0x00007F30
		public static LocalizedString CalendarPublishingRangeTo
		{
			get
			{
				return new LocalizedString("CalendarPublishingRangeTo", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170001E5 RID: 485
		// (get) Token: 0x060001F0 RID: 496 RVA: 0x00009D4E File Offset: 0x00007F4E
		public static LocalizedString Installed
		{
			get
			{
				return new LocalizedString("Installed", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170001E6 RID: 486
		// (get) Token: 0x060001F1 RID: 497 RVA: 0x00009D6C File Offset: 0x00007F6C
		public static LocalizedString JoinRestrictionClosedDetails
		{
			get
			{
				return new LocalizedString("JoinRestrictionClosedDetails", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170001E7 RID: 487
		// (get) Token: 0x060001F2 RID: 498 RVA: 0x00009D8A File Offset: 0x00007F8A
		public static LocalizedString EnterNumberStepMessage
		{
			get
			{
				return new LocalizedString("EnterNumberStepMessage", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170001E8 RID: 488
		// (get) Token: 0x060001F3 RID: 499 RVA: 0x00009DA8 File Offset: 0x00007FA8
		public static LocalizedString TeamMailboxString
		{
			get
			{
				return new LocalizedString("TeamMailboxString", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170001E9 RID: 489
		// (get) Token: 0x060001F4 RID: 500 RVA: 0x00009DC6 File Offset: 0x00007FC6
		public static LocalizedString ExternalAudienceCheckboxText
		{
			get
			{
				return new LocalizedString("ExternalAudienceCheckboxText", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170001EA RID: 490
		// (get) Token: 0x060001F5 RID: 501 RVA: 0x00009DE4 File Offset: 0x00007FE4
		public static LocalizedString RetentionActionNeverDelete
		{
			get
			{
				return new LocalizedString("RetentionActionNeverDelete", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170001EB RID: 491
		// (get) Token: 0x060001F6 RID: 502 RVA: 0x00009E02 File Offset: 0x00008002
		public static LocalizedString TeamMailboxOwnersString
		{
			get
			{
				return new LocalizedString("TeamMailboxOwnersString", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170001EC RID: 492
		// (get) Token: 0x060001F7 RID: 503 RVA: 0x00009E20 File Offset: 0x00008020
		public static LocalizedString SentOnlyToMeConditionFormat
		{
			get
			{
				return new LocalizedString("SentOnlyToMeConditionFormat", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170001ED RID: 493
		// (get) Token: 0x060001F8 RID: 504 RVA: 0x00009E3E File Offset: 0x0000803E
		public static LocalizedString PostalCodeLabel
		{
			get
			{
				return new LocalizedString("PostalCodeLabel", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060001F9 RID: 505 RVA: 0x00009E5C File Offset: 0x0000805C
		public static LocalizedString VerificationEmailSucceeded(string emailAddress)
		{
			return new LocalizedString("VerificationEmailSucceeded", "", false, false, OwaOptionStrings.ResourceManager, new object[]
			{
				emailAddress
			});
		}

		// Token: 0x170001EE RID: 494
		// (get) Token: 0x060001FA RID: 506 RVA: 0x00009E8B File Offset: 0x0000808B
		public static LocalizedString StreetAddressLabel
		{
			get
			{
				return new LocalizedString("StreetAddressLabel", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170001EF RID: 495
		// (get) Token: 0x060001FB RID: 507 RVA: 0x00009EA9 File Offset: 0x000080A9
		public static LocalizedString ModerationNotificationsNever
		{
			get
			{
				return new LocalizedString("ModerationNotificationsNever", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170001F0 RID: 496
		// (get) Token: 0x060001FC RID: 508 RVA: 0x00009EC7 File Offset: 0x000080C7
		public static LocalizedString AutoAddSignature
		{
			get
			{
				return new LocalizedString("AutoAddSignature", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170001F1 RID: 497
		// (get) Token: 0x060001FD RID: 509 RVA: 0x00009EE5 File Offset: 0x000080E5
		public static LocalizedString VoicemailConfiguration
		{
			get
			{
				return new LocalizedString("VoicemailConfiguration", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170001F2 RID: 498
		// (get) Token: 0x060001FE RID: 510 RVA: 0x00009F03 File Offset: 0x00008103
		public static LocalizedString TeamMailboxUsersString
		{
			get
			{
				return new LocalizedString("TeamMailboxUsersString", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170001F3 RID: 499
		// (get) Token: 0x060001FF RID: 511 RVA: 0x00009F21 File Offset: 0x00008121
		public static LocalizedString Minutes
		{
			get
			{
				return new LocalizedString("Minutes", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170001F4 RID: 500
		// (get) Token: 0x06000200 RID: 512 RVA: 0x00009F3F File Offset: 0x0000813F
		public static LocalizedString TextMessagingStatusPrefixNotifications
		{
			get
			{
				return new LocalizedString("TextMessagingStatusPrefixNotifications", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000201 RID: 513 RVA: 0x00009F60 File Offset: 0x00008160
		public static LocalizedString NewSubscriptionSucceed(string feedback)
		{
			return new LocalizedString("NewSubscriptionSucceed", "", false, false, OwaOptionStrings.ResourceManager, new object[]
			{
				feedback
			});
		}

		// Token: 0x170001F5 RID: 501
		// (get) Token: 0x06000202 RID: 514 RVA: 0x00009F8F File Offset: 0x0000818F
		public static LocalizedString OfficeLabel
		{
			get
			{
				return new LocalizedString("OfficeLabel", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170001F6 RID: 502
		// (get) Token: 0x06000203 RID: 515 RVA: 0x00009FAD File Offset: 0x000081AD
		public static LocalizedString SetupCalendarNotificationsLink
		{
			get
			{
				return new LocalizedString("SetupCalendarNotificationsLink", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170001F7 RID: 503
		// (get) Token: 0x06000204 RID: 516 RVA: 0x00009FCB File Offset: 0x000081CB
		public static LocalizedString WipeDeviceConfirmMessage
		{
			get
			{
				return new LocalizedString("WipeDeviceConfirmMessage", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170001F8 RID: 504
		// (get) Token: 0x06000205 RID: 517 RVA: 0x00009FE9 File Offset: 0x000081E9
		public static LocalizedString JunkEmailEnabled
		{
			get
			{
				return new LocalizedString("JunkEmailEnabled", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170001F9 RID: 505
		// (get) Token: 0x06000206 RID: 518 RVA: 0x0000A007 File Offset: 0x00008207
		public static LocalizedString ProfilePhoto
		{
			get
			{
				return new LocalizedString("ProfilePhoto", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170001FA RID: 506
		// (get) Token: 0x06000207 RID: 519 RVA: 0x0000A025 File Offset: 0x00008225
		public static LocalizedString JoinRestriction
		{
			get
			{
				return new LocalizedString("JoinRestriction", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170001FB RID: 507
		// (get) Token: 0x06000208 RID: 520 RVA: 0x0000A043 File Offset: 0x00008243
		public static LocalizedString SubscriptionAccountInformation
		{
			get
			{
				return new LocalizedString("SubscriptionAccountInformation", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170001FC RID: 508
		// (get) Token: 0x06000209 RID: 521 RVA: 0x0000A061 File Offset: 0x00008261
		public static LocalizedString PopSubscription
		{
			get
			{
				return new LocalizedString("PopSubscription", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170001FD RID: 509
		// (get) Token: 0x0600020A RID: 522 RVA: 0x0000A07F File Offset: 0x0000827F
		public static LocalizedString ConnectedAccountsDescriptionForSendAs
		{
			get
			{
				return new LocalizedString("ConnectedAccountsDescriptionForSendAs", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170001FE RID: 510
		// (get) Token: 0x0600020B RID: 523 RVA: 0x0000A09D File Offset: 0x0000829D
		public static LocalizedString VoicemailNotConfiguredText
		{
			get
			{
				return new LocalizedString("VoicemailNotConfiguredText", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170001FF RID: 511
		// (get) Token: 0x0600020C RID: 524 RVA: 0x0000A0BB File Offset: 0x000082BB
		public static LocalizedString Date
		{
			get
			{
				return new LocalizedString("Date", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000200 RID: 512
		// (get) Token: 0x0600020D RID: 525 RVA: 0x0000A0D9 File Offset: 0x000082D9
		public static LocalizedString SubscriptionEmailAddress
		{
			get
			{
				return new LocalizedString("SubscriptionEmailAddress", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000201 RID: 513
		// (get) Token: 0x0600020E RID: 526 RVA: 0x0000A0F7 File Offset: 0x000082F7
		public static LocalizedString CalendarAppearanceInstruction
		{
			get
			{
				return new LocalizedString("CalendarAppearanceInstruction", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000202 RID: 514
		// (get) Token: 0x0600020F RID: 527 RVA: 0x0000A115 File Offset: 0x00008315
		public static LocalizedString DisableReminders
		{
			get
			{
				return new LocalizedString("DisableReminders", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000203 RID: 515
		// (get) Token: 0x06000210 RID: 528 RVA: 0x0000A133 File Offset: 0x00008333
		public static LocalizedString UninstallExtensionConfirmation
		{
			get
			{
				return new LocalizedString("UninstallExtensionConfirmation", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000204 RID: 516
		// (get) Token: 0x06000211 RID: 529 RVA: 0x0000A151 File Offset: 0x00008351
		public static LocalizedString ReadReceiptResponseAskBefore
		{
			get
			{
				return new LocalizedString("ReadReceiptResponseAskBefore", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000205 RID: 517
		// (get) Token: 0x06000212 RID: 530 RVA: 0x0000A16F File Offset: 0x0000836F
		public static LocalizedString AutomaticRepliesDisabledText
		{
			get
			{
				return new LocalizedString("AutomaticRepliesDisabledText", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000206 RID: 518
		// (get) Token: 0x06000213 RID: 531 RVA: 0x0000A18D File Offset: 0x0000838D
		public static LocalizedString PermissionGranted
		{
			get
			{
				return new LocalizedString("PermissionGranted", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000207 RID: 519
		// (get) Token: 0x06000214 RID: 532 RVA: 0x0000A1AB File Offset: 0x000083AB
		public static LocalizedString CalendarTroubleShootingSlab
		{
			get
			{
				return new LocalizedString("CalendarTroubleShootingSlab", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000208 RID: 520
		// (get) Token: 0x06000215 RID: 533 RVA: 0x0000A1C9 File Offset: 0x000083C9
		public static LocalizedString QLRemotePowerShell
		{
			get
			{
				return new LocalizedString("QLRemotePowerShell", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000209 RID: 521
		// (get) Token: 0x06000216 RID: 534 RVA: 0x0000A1E7 File Offset: 0x000083E7
		public static LocalizedString Ownership
		{
			get
			{
				return new LocalizedString("Ownership", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700020A RID: 522
		// (get) Token: 0x06000217 RID: 535 RVA: 0x0000A205 File Offset: 0x00008405
		public static LocalizedString DevicePhoneNumberHeaderText
		{
			get
			{
				return new LocalizedString("DevicePhoneNumberHeaderText", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700020B RID: 523
		// (get) Token: 0x06000218 RID: 536 RVA: 0x0000A223 File Offset: 0x00008423
		public static LocalizedString OwnerApprovalRequired
		{
			get
			{
				return new LocalizedString("OwnerApprovalRequired", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700020C RID: 524
		// (get) Token: 0x06000219 RID: 537 RVA: 0x0000A241 File Offset: 0x00008441
		public static LocalizedString AddExtensionTitle
		{
			get
			{
				return new LocalizedString("AddExtensionTitle", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700020D RID: 525
		// (get) Token: 0x0600021A RID: 538 RVA: 0x0000A25F File Offset: 0x0000845F
		public static LocalizedString DevicePolicyApplicationStatusLabel
		{
			get
			{
				return new LocalizedString("DevicePolicyApplicationStatusLabel", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700020E RID: 526
		// (get) Token: 0x0600021B RID: 539 RVA: 0x0000A27D File Offset: 0x0000847D
		public static LocalizedString CalendarWorkflowSlab
		{
			get
			{
				return new LocalizedString("CalendarWorkflowSlab", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700020F RID: 527
		// (get) Token: 0x0600021C RID: 540 RVA: 0x0000A29B File Offset: 0x0000849B
		public static LocalizedString SettingNotAvailable
		{
			get
			{
				return new LocalizedString("SettingNotAvailable", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000210 RID: 528
		// (get) Token: 0x0600021D RID: 541 RVA: 0x0000A2B9 File Offset: 0x000084B9
		public static LocalizedString DepartRestriction
		{
			get
			{
				return new LocalizedString("DepartRestriction", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000211 RID: 529
		// (get) Token: 0x0600021E RID: 542 RVA: 0x0000A2D7 File Offset: 0x000084D7
		public static LocalizedString RetentionTypeRequiredDescription
		{
			get
			{
				return new LocalizedString("RetentionTypeRequiredDescription", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000212 RID: 530
		// (get) Token: 0x0600021F RID: 543 RVA: 0x0000A2F5 File Offset: 0x000084F5
		public static LocalizedString RemoveForwardedMeetingNotificationsCheckBoxText
		{
			get
			{
				return new LocalizedString("RemoveForwardedMeetingNotificationsCheckBoxText", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000213 RID: 531
		// (get) Token: 0x06000220 RID: 544 RVA: 0x0000A313 File Offset: 0x00008513
		public static LocalizedString Everyone
		{
			get
			{
				return new LocalizedString("Everyone", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000214 RID: 532
		// (get) Token: 0x06000221 RID: 545 RVA: 0x0000A331 File Offset: 0x00008531
		public static LocalizedString TeamMailboxAppTitle
		{
			get
			{
				return new LocalizedString("TeamMailboxAppTitle", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000215 RID: 533
		// (get) Token: 0x06000222 RID: 546 RVA: 0x0000A34F File Offset: 0x0000854F
		public static LocalizedString PersonalSettingConfirmPassword
		{
			get
			{
				return new LocalizedString("PersonalSettingConfirmPassword", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000216 RID: 534
		// (get) Token: 0x06000223 RID: 547 RVA: 0x0000A36D File Offset: 0x0000856D
		public static LocalizedString MailboxUsageUnitMB
		{
			get
			{
				return new LocalizedString("MailboxUsageUnitMB", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000217 RID: 535
		// (get) Token: 0x06000224 RID: 548 RVA: 0x0000A38B File Offset: 0x0000858B
		public static LocalizedString SubscriptionServerInformation
		{
			get
			{
				return new LocalizedString("SubscriptionServerInformation", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000218 RID: 536
		// (get) Token: 0x06000225 RID: 549 RVA: 0x0000A3A9 File Offset: 0x000085A9
		public static LocalizedString UserNameLabel
		{
			get
			{
				return new LocalizedString("UserNameLabel", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000219 RID: 537
		// (get) Token: 0x06000226 RID: 550 RVA: 0x0000A3C7 File Offset: 0x000085C7
		public static LocalizedString TimeIncrementFifteenMinutes
		{
			get
			{
				return new LocalizedString("TimeIncrementFifteenMinutes", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700021A RID: 538
		// (get) Token: 0x06000227 RID: 551 RVA: 0x0000A3E5 File Offset: 0x000085E5
		public static LocalizedString LastSuccessfulSync
		{
			get
			{
				return new LocalizedString("LastSuccessfulSync", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700021B RID: 539
		// (get) Token: 0x06000228 RID: 552 RVA: 0x0000A403 File Offset: 0x00008603
		public static LocalizedString SchedulingPermissionsInstruction
		{
			get
			{
				return new LocalizedString("SchedulingPermissionsInstruction", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700021C RID: 540
		// (get) Token: 0x06000229 RID: 553 RVA: 0x0000A421 File Offset: 0x00008621
		public static LocalizedString EmptyDeletedItemsOnLogoff
		{
			get
			{
				return new LocalizedString("EmptyDeletedItemsOnLogoff", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700021D RID: 541
		// (get) Token: 0x0600022A RID: 554 RVA: 0x0000A43F File Offset: 0x0000863F
		public static LocalizedString BeforeDateDisplayTemplate
		{
			get
			{
				return new LocalizedString("BeforeDateDisplayTemplate", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700021E RID: 542
		// (get) Token: 0x0600022B RID: 555 RVA: 0x0000A45D File Offset: 0x0000865D
		public static LocalizedString EmailAddressLabel
		{
			get
			{
				return new LocalizedString("EmailAddressLabel", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700021F RID: 543
		// (get) Token: 0x0600022C RID: 556 RVA: 0x0000A47B File Offset: 0x0000867B
		public static LocalizedString JunkEmailBlockedListDescription
		{
			get
			{
				return new LocalizedString("JunkEmailBlockedListDescription", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000220 RID: 544
		// (get) Token: 0x0600022D RID: 557 RVA: 0x0000A499 File Offset: 0x00008699
		public static LocalizedString NewItemNotificationSound
		{
			get
			{
				return new LocalizedString("NewItemNotificationSound", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000221 RID: 545
		// (get) Token: 0x0600022E RID: 558 RVA: 0x0000A4B7 File Offset: 0x000086B7
		public static LocalizedString NewInboxRuleCaption
		{
			get
			{
				return new LocalizedString("NewInboxRuleCaption", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000222 RID: 546
		// (get) Token: 0x0600022F RID: 559 RVA: 0x0000A4D5 File Offset: 0x000086D5
		public static LocalizedString UpdateTimeZoneNoteLinkText
		{
			get
			{
				return new LocalizedString("UpdateTimeZoneNoteLinkText", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000223 RID: 547
		// (get) Token: 0x06000230 RID: 560 RVA: 0x0000A4F3 File Offset: 0x000086F3
		public static LocalizedString HasClassificationConditionFormat
		{
			get
			{
				return new LocalizedString("HasClassificationConditionFormat", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000224 RID: 548
		// (get) Token: 0x06000231 RID: 561 RVA: 0x0000A511 File Offset: 0x00008711
		public static LocalizedString NoneAccessRightRole
		{
			get
			{
				return new LocalizedString("NoneAccessRightRole", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000225 RID: 549
		// (get) Token: 0x06000232 RID: 562 RVA: 0x0000A52F File Offset: 0x0000872F
		public static LocalizedString MobileDeviceDetailTitle
		{
			get
			{
				return new LocalizedString("MobileDeviceDetailTitle", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000226 RID: 550
		// (get) Token: 0x06000233 RID: 563 RVA: 0x0000A54D File Offset: 0x0000874D
		public static LocalizedString EditCommandText
		{
			get
			{
				return new LocalizedString("EditCommandText", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000227 RID: 551
		// (get) Token: 0x06000234 RID: 564 RVA: 0x0000A56B File Offset: 0x0000876B
		public static LocalizedString DeviceTypeLabel
		{
			get
			{
				return new LocalizedString("DeviceTypeLabel", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000228 RID: 552
		// (get) Token: 0x06000235 RID: 565 RVA: 0x0000A589 File Offset: 0x00008789
		public static LocalizedString AllowConflicts
		{
			get
			{
				return new LocalizedString("AllowConflicts", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000229 RID: 553
		// (get) Token: 0x06000236 RID: 566 RVA: 0x0000A5A7 File Offset: 0x000087A7
		public static LocalizedString CalendarPublishing
		{
			get
			{
				return new LocalizedString("CalendarPublishing", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700022A RID: 554
		// (get) Token: 0x06000237 RID: 567 RVA: 0x0000A5C5 File Offset: 0x000087C5
		public static LocalizedString RetentionNameHeader
		{
			get
			{
				return new LocalizedString("RetentionNameHeader", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700022B RID: 555
		// (get) Token: 0x06000238 RID: 568 RVA: 0x0000A5E3 File Offset: 0x000087E3
		public static LocalizedString ImportContactListPage1InformationText
		{
			get
			{
				return new LocalizedString("ImportContactListPage1InformationText", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700022C RID: 556
		// (get) Token: 0x06000239 RID: 569 RVA: 0x0000A601 File Offset: 0x00008801
		public static LocalizedString VoicemailAskPhoneNumber
		{
			get
			{
				return new LocalizedString("VoicemailAskPhoneNumber", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700022D RID: 557
		// (get) Token: 0x0600023A RID: 570 RVA: 0x0000A61F File Offset: 0x0000881F
		public static LocalizedString TeamMailboxDocumentsString
		{
			get
			{
				return new LocalizedString("TeamMailboxDocumentsString", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700022E RID: 558
		// (get) Token: 0x0600023B RID: 571 RVA: 0x0000A63D File Offset: 0x0000883D
		public static LocalizedString CountryOrRegionLabel
		{
			get
			{
				return new LocalizedString("CountryOrRegionLabel", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700022F RID: 559
		// (get) Token: 0x0600023C RID: 572 RVA: 0x0000A65B File Offset: 0x0000885B
		public static LocalizedString CalendarPublishingLearnMore
		{
			get
			{
				return new LocalizedString("CalendarPublishingLearnMore", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000230 RID: 560
		// (get) Token: 0x0600023D RID: 573 RVA: 0x0000A679 File Offset: 0x00008879
		public static LocalizedString GroupModeratedBy
		{
			get
			{
				return new LocalizedString("GroupModeratedBy", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000231 RID: 561
		// (get) Token: 0x0600023E RID: 574 RVA: 0x0000A697 File Offset: 0x00008897
		public static LocalizedString DidntReceivePasscodeMessage
		{
			get
			{
				return new LocalizedString("DidntReceivePasscodeMessage", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000232 RID: 562
		// (get) Token: 0x0600023F RID: 575 RVA: 0x0000A6B5 File Offset: 0x000088B5
		public static LocalizedString InboxRuleFromAddressContainsConditionText
		{
			get
			{
				return new LocalizedString("InboxRuleFromAddressContainsConditionText", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000233 RID: 563
		// (get) Token: 0x06000240 RID: 576 RVA: 0x0000A6D3 File Offset: 0x000088D3
		public static LocalizedString IncomingAuthenticationLabel
		{
			get
			{
				return new LocalizedString("IncomingAuthenticationLabel", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000234 RID: 564
		// (get) Token: 0x06000241 RID: 577 RVA: 0x0000A6F1 File Offset: 0x000088F1
		public static LocalizedString InboxRuleRecipientAddressContainsConditionText
		{
			get
			{
				return new LocalizedString("InboxRuleRecipientAddressContainsConditionText", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000235 RID: 565
		// (get) Token: 0x06000242 RID: 578 RVA: 0x0000A70F File Offset: 0x0000890F
		public static LocalizedString DepartGroupsConfirmation
		{
			get
			{
				return new LocalizedString("DepartGroupsConfirmation", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000236 RID: 566
		// (get) Token: 0x06000243 RID: 579 RVA: 0x0000A72D File Offset: 0x0000892D
		public static LocalizedString CalendarAppearanceSlab
		{
			get
			{
				return new LocalizedString("CalendarAppearanceSlab", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000237 RID: 567
		// (get) Token: 0x06000244 RID: 580 RVA: 0x0000A74B File Offset: 0x0000894B
		public static LocalizedString InitialsLabel
		{
			get
			{
				return new LocalizedString("InitialsLabel", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000238 RID: 568
		// (get) Token: 0x06000245 RID: 581 RVA: 0x0000A769 File Offset: 0x00008969
		public static LocalizedString InboxRuleFlaggedForActionConditionText
		{
			get
			{
				return new LocalizedString("InboxRuleFlaggedForActionConditionText", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000239 RID: 569
		// (get) Token: 0x06000246 RID: 582 RVA: 0x0000A787 File Offset: 0x00008987
		public static LocalizedString QuickLinks
		{
			get
			{
				return new LocalizedString("QuickLinks", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700023A RID: 570
		// (get) Token: 0x06000247 RID: 583 RVA: 0x0000A7A5 File Offset: 0x000089A5
		public static LocalizedString TeamMailboxMyRoleString2
		{
			get
			{
				return new LocalizedString("TeamMailboxMyRoleString2", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700023B RID: 571
		// (get) Token: 0x06000248 RID: 584 RVA: 0x0000A7C3 File Offset: 0x000089C3
		public static LocalizedString RoomEmailAddressLabel
		{
			get
			{
				return new LocalizedString("RoomEmailAddressLabel", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700023C RID: 572
		// (get) Token: 0x06000249 RID: 585 RVA: 0x0000A7E1 File Offset: 0x000089E1
		public static LocalizedString ToColumnLabel
		{
			get
			{
				return new LocalizedString("ToColumnLabel", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700023D RID: 573
		// (get) Token: 0x0600024A RID: 586 RVA: 0x0000A7FF File Offset: 0x000089FF
		public static LocalizedString SensitivityDialogLabel
		{
			get
			{
				return new LocalizedString("SensitivityDialogLabel", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700023E RID: 574
		// (get) Token: 0x0600024B RID: 587 RVA: 0x0000A81D File Offset: 0x00008A1D
		public static LocalizedString AllowedSendersEmptyLabelForEndUser
		{
			get
			{
				return new LocalizedString("AllowedSendersEmptyLabelForEndUser", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700023F RID: 575
		// (get) Token: 0x0600024C RID: 588 RVA: 0x0000A83B File Offset: 0x00008A3B
		public static LocalizedString RequirementsReadWriteMailboxValue
		{
			get
			{
				return new LocalizedString("RequirementsReadWriteMailboxValue", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000240 RID: 576
		// (get) Token: 0x0600024D RID: 589 RVA: 0x0000A859 File Offset: 0x00008A59
		public static LocalizedString FlagStatusDialogLabel
		{
			get
			{
				return new LocalizedString("FlagStatusDialogLabel", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000241 RID: 577
		// (get) Token: 0x0600024E RID: 590 RVA: 0x0000A877 File Offset: 0x00008A77
		public static LocalizedString ImapSetting
		{
			get
			{
				return new LocalizedString("ImapSetting", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000242 RID: 578
		// (get) Token: 0x0600024F RID: 591 RVA: 0x0000A895 File Offset: 0x00008A95
		public static LocalizedString VoicemailConfiguredText
		{
			get
			{
				return new LocalizedString("VoicemailConfiguredText", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000243 RID: 579
		// (get) Token: 0x06000250 RID: 592 RVA: 0x0000A8B3 File Offset: 0x00008AB3
		public static LocalizedString JunkEmailTrustedListHeader
		{
			get
			{
				return new LocalizedString("JunkEmailTrustedListHeader", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000244 RID: 580
		// (get) Token: 0x06000251 RID: 593 RVA: 0x0000A8D1 File Offset: 0x00008AD1
		public static LocalizedString RequirementsReadItemDescription
		{
			get
			{
				return new LocalizedString("RequirementsReadItemDescription", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000245 RID: 581
		// (get) Token: 0x06000252 RID: 594 RVA: 0x0000A8EF File Offset: 0x00008AEF
		public static LocalizedString RequireSenderAuth
		{
			get
			{
				return new LocalizedString("RequireSenderAuth", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000246 RID: 582
		// (get) Token: 0x06000253 RID: 595 RVA: 0x0000A90D File Offset: 0x00008B0D
		public static LocalizedString IWantToEditMyNotificationSettings
		{
			get
			{
				return new LocalizedString("IWantToEditMyNotificationSettings", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000247 RID: 583
		// (get) Token: 0x06000254 RID: 596 RVA: 0x0000A92B File Offset: 0x00008B2B
		public static LocalizedString DevicePolicyAppliedLabel
		{
			get
			{
				return new LocalizedString("DevicePolicyAppliedLabel", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000248 RID: 584
		// (get) Token: 0x06000255 RID: 597 RVA: 0x0000A949 File Offset: 0x00008B49
		public static LocalizedString VoicemailSMSOptionNone
		{
			get
			{
				return new LocalizedString("VoicemailSMSOptionNone", "ExFB5221", false, true, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000249 RID: 585
		// (get) Token: 0x06000256 RID: 598 RVA: 0x0000A967 File Offset: 0x00008B67
		public static LocalizedString EditDistributionGroupTitle
		{
			get
			{
				return new LocalizedString("EditDistributionGroupTitle", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700024A RID: 586
		// (get) Token: 0x06000257 RID: 599 RVA: 0x0000A985 File Offset: 0x00008B85
		public static LocalizedString MaximumDurationInMinutes
		{
			get
			{
				return new LocalizedString("MaximumDurationInMinutes", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700024B RID: 587
		// (get) Token: 0x06000258 RID: 600 RVA: 0x0000A9A3 File Offset: 0x00008BA3
		public static LocalizedString NewPopSubscription
		{
			get
			{
				return new LocalizedString("NewPopSubscription", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700024C RID: 588
		// (get) Token: 0x06000259 RID: 601 RVA: 0x0000A9C1 File Offset: 0x00008BC1
		public static LocalizedString MobileDeviceHeadNoteInfo
		{
			get
			{
				return new LocalizedString("MobileDeviceHeadNoteInfo", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700024D RID: 589
		// (get) Token: 0x0600025A RID: 602 RVA: 0x0000A9DF File Offset: 0x00008BDF
		public static LocalizedString HomePhoneLabel
		{
			get
			{
				return new LocalizedString("HomePhoneLabel", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700024E RID: 590
		// (get) Token: 0x0600025B RID: 603 RVA: 0x0000A9FD File Offset: 0x00008BFD
		public static LocalizedString BlockDeviceCommandText
		{
			get
			{
				return new LocalizedString("BlockDeviceCommandText", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700024F RID: 591
		// (get) Token: 0x0600025C RID: 604 RVA: 0x0000AA1B File Offset: 0x00008C1B
		public static LocalizedString SelectUsers
		{
			get
			{
				return new LocalizedString("SelectUsers", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000250 RID: 592
		// (get) Token: 0x0600025D RID: 605 RVA: 0x0000AA39 File Offset: 0x00008C39
		public static LocalizedString SearchGroupsButtonDescription
		{
			get
			{
				return new LocalizedString("SearchGroupsButtonDescription", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000251 RID: 593
		// (get) Token: 0x0600025E RID: 606 RVA: 0x0000AA57 File Offset: 0x00008C57
		public static LocalizedString DeleteEmailSubscriptionsConfirmation
		{
			get
			{
				return new LocalizedString("DeleteEmailSubscriptionsConfirmation", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000252 RID: 594
		// (get) Token: 0x0600025F RID: 607 RVA: 0x0000AA75 File Offset: 0x00008C75
		public static LocalizedString LearnHowToUseRedirectTo
		{
			get
			{
				return new LocalizedString("LearnHowToUseRedirectTo", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000253 RID: 595
		// (get) Token: 0x06000260 RID: 608 RVA: 0x0000AA93 File Offset: 0x00008C93
		public static LocalizedString InboxRuleDeleteMessageActionFlyOutText
		{
			get
			{
				return new LocalizedString("InboxRuleDeleteMessageActionFlyOutText", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000254 RID: 596
		// (get) Token: 0x06000261 RID: 609 RVA: 0x0000AAB1 File Offset: 0x00008CB1
		public static LocalizedString RetentionExplanationLabel
		{
			get
			{
				return new LocalizedString("RetentionExplanationLabel", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000255 RID: 597
		// (get) Token: 0x06000262 RID: 610 RVA: 0x0000AACF File Offset: 0x00008CCF
		public static LocalizedString MessageTrackingPendingEvent
		{
			get
			{
				return new LocalizedString("MessageTrackingPendingEvent", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000256 RID: 598
		// (get) Token: 0x06000263 RID: 611 RVA: 0x0000AAED File Offset: 0x00008CED
		public static LocalizedString ProviderColumn
		{
			get
			{
				return new LocalizedString("ProviderColumn", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000257 RID: 599
		// (get) Token: 0x06000264 RID: 612 RVA: 0x0000AB0B File Offset: 0x00008D0B
		public static LocalizedString SettingAccessDisabled
		{
			get
			{
				return new LocalizedString("SettingAccessDisabled", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000258 RID: 600
		// (get) Token: 0x06000265 RID: 613 RVA: 0x0000AB29 File Offset: 0x00008D29
		public static LocalizedString RPTDays
		{
			get
			{
				return new LocalizedString("RPTDays", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000259 RID: 601
		// (get) Token: 0x06000266 RID: 614 RVA: 0x0000AB47 File Offset: 0x00008D47
		public static LocalizedString InboxRuleFromSubscriptionConditionFlyOutText
		{
			get
			{
				return new LocalizedString("InboxRuleFromSubscriptionConditionFlyOutText", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700025A RID: 602
		// (get) Token: 0x06000267 RID: 615 RVA: 0x0000AB65 File Offset: 0x00008D65
		public static LocalizedString SmtpAddressExample
		{
			get
			{
				return new LocalizedString("SmtpAddressExample", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700025B RID: 603
		// (get) Token: 0x06000268 RID: 616 RVA: 0x0000AB83 File Offset: 0x00008D83
		public static LocalizedString InboxRuleMarkImportanceActionFlyOutText
		{
			get
			{
				return new LocalizedString("InboxRuleMarkImportanceActionFlyOutText", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700025C RID: 604
		// (get) Token: 0x06000269 RID: 617 RVA: 0x0000ABA1 File Offset: 0x00008DA1
		public static LocalizedString CategoryDialogLabel
		{
			get
			{
				return new LocalizedString("CategoryDialogLabel", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700025D RID: 605
		// (get) Token: 0x0600026A RID: 618 RVA: 0x0000ABBF File Offset: 0x00008DBF
		public static LocalizedString DeviceAccessStateLabel
		{
			get
			{
				return new LocalizedString("DeviceAccessStateLabel", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700025E RID: 606
		// (get) Token: 0x0600026B RID: 619 RVA: 0x0000ABDD File Offset: 0x00008DDD
		public static LocalizedString JunkEmailBlockedListHeader
		{
			get
			{
				return new LocalizedString("JunkEmailBlockedListHeader", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700025F RID: 607
		// (get) Token: 0x0600026C RID: 620 RVA: 0x0000ABFB File Offset: 0x00008DFB
		public static LocalizedString VoicemailNotAvailableText
		{
			get
			{
				return new LocalizedString("VoicemailNotAvailableText", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000260 RID: 608
		// (get) Token: 0x0600026D RID: 621 RVA: 0x0000AC19 File Offset: 0x00008E19
		public static LocalizedString ContactLocationBookmark
		{
			get
			{
				return new LocalizedString("ContactLocationBookmark", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000261 RID: 609
		// (get) Token: 0x0600026E RID: 622 RVA: 0x0000AC37 File Offset: 0x00008E37
		public static LocalizedString InboxRuleSubjectContainsConditionText
		{
			get
			{
				return new LocalizedString("InboxRuleSubjectContainsConditionText", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000262 RID: 610
		// (get) Token: 0x0600026F RID: 623 RVA: 0x0000AC55 File Offset: 0x00008E55
		public static LocalizedString VoicemailWizardConfirmPinLabel
		{
			get
			{
				return new LocalizedString("VoicemailWizardConfirmPinLabel", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000263 RID: 611
		// (get) Token: 0x06000270 RID: 624 RVA: 0x0000AC73 File Offset: 0x00008E73
		public static LocalizedString NoSubscriptionAvailable
		{
			get
			{
				return new LocalizedString("NoSubscriptionAvailable", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000264 RID: 612
		// (get) Token: 0x06000271 RID: 625 RVA: 0x0000AC91 File Offset: 0x00008E91
		public static LocalizedString QLPassword
		{
			get
			{
				return new LocalizedString("QLPassword", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000265 RID: 613
		// (get) Token: 0x06000272 RID: 626 RVA: 0x0000ACAF File Offset: 0x00008EAF
		public static LocalizedString CustomAccessRightRole
		{
			get
			{
				return new LocalizedString("CustomAccessRightRole", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000266 RID: 614
		// (get) Token: 0x06000273 RID: 627 RVA: 0x0000ACCD File Offset: 0x00008ECD
		public static LocalizedString ClearSettings
		{
			get
			{
				return new LocalizedString("ClearSettings", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000267 RID: 615
		// (get) Token: 0x06000274 RID: 628 RVA: 0x0000ACEB File Offset: 0x00008EEB
		public static LocalizedString MailboxUsageUnitKB
		{
			get
			{
				return new LocalizedString("MailboxUsageUnitKB", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000268 RID: 616
		// (get) Token: 0x06000275 RID: 629 RVA: 0x0000AD09 File Offset: 0x00008F09
		public static LocalizedString ExtensionPackageLocation
		{
			get
			{
				return new LocalizedString("ExtensionPackageLocation", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000269 RID: 617
		// (get) Token: 0x06000276 RID: 630 RVA: 0x0000AD27 File Offset: 0x00008F27
		public static LocalizedString InboxRuleApplyCategoryActionText
		{
			get
			{
				return new LocalizedString("InboxRuleApplyCategoryActionText", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700026A RID: 618
		// (get) Token: 0x06000277 RID: 631 RVA: 0x0000AD45 File Offset: 0x00008F45
		public static LocalizedString OWAVoicemail
		{
			get
			{
				return new LocalizedString("OWAVoicemail", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700026B RID: 619
		// (get) Token: 0x06000278 RID: 632 RVA: 0x0000AD63 File Offset: 0x00008F63
		public static LocalizedString PersonalSettingPassword
		{
			get
			{
				return new LocalizedString("PersonalSettingPassword", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700026C RID: 620
		// (get) Token: 0x06000279 RID: 633 RVA: 0x0000AD81 File Offset: 0x00008F81
		public static LocalizedString SendDuringWorkHoursOnly
		{
			get
			{
				return new LocalizedString("SendDuringWorkHoursOnly", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700026D RID: 621
		// (get) Token: 0x0600027A RID: 634 RVA: 0x0000AD9F File Offset: 0x00008F9F
		public static LocalizedString AliasLabelForDataCenter
		{
			get
			{
				return new LocalizedString("AliasLabelForDataCenter", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700026E RID: 622
		// (get) Token: 0x0600027B RID: 635 RVA: 0x0000ADBD File Offset: 0x00008FBD
		public static LocalizedString DeliverAndForward
		{
			get
			{
				return new LocalizedString("DeliverAndForward", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700026F RID: 623
		// (get) Token: 0x0600027C RID: 636 RVA: 0x0000ADDB File Offset: 0x00008FDB
		public static LocalizedString Language
		{
			get
			{
				return new LocalizedString("Language", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000270 RID: 624
		// (get) Token: 0x0600027D RID: 637 RVA: 0x0000ADF9 File Offset: 0x00008FF9
		public static LocalizedString NameAndAccountBookmark
		{
			get
			{
				return new LocalizedString("NameAndAccountBookmark", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000271 RID: 625
		// (get) Token: 0x0600027E RID: 638 RVA: 0x0000AE17 File Offset: 0x00009017
		public static LocalizedString SendMyPhoneColon
		{
			get
			{
				return new LocalizedString("SendMyPhoneColon", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000272 RID: 626
		// (get) Token: 0x0600027F RID: 639 RVA: 0x0000AE35 File Offset: 0x00009035
		public static LocalizedString DateStyles
		{
			get
			{
				return new LocalizedString("DateStyles", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000273 RID: 627
		// (get) Token: 0x06000280 RID: 640 RVA: 0x0000AE53 File Offset: 0x00009053
		public static LocalizedString GroupsIBelongToDescription
		{
			get
			{
				return new LocalizedString("GroupsIBelongToDescription", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000274 RID: 628
		// (get) Token: 0x06000281 RID: 641 RVA: 0x0000AE71 File Offset: 0x00009071
		public static LocalizedString StartTimeText
		{
			get
			{
				return new LocalizedString("StartTimeText", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000275 RID: 629
		// (get) Token: 0x06000282 RID: 642 RVA: 0x0000AE8F File Offset: 0x0000908F
		public static LocalizedString RemoveCommandText
		{
			get
			{
				return new LocalizedString("RemoveCommandText", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000276 RID: 630
		// (get) Token: 0x06000283 RID: 643 RVA: 0x0000AEAD File Offset: 0x000090AD
		public static LocalizedString PersonalSettingChangePassword
		{
			get
			{
				return new LocalizedString("PersonalSettingChangePassword", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000277 RID: 631
		// (get) Token: 0x06000284 RID: 644 RVA: 0x0000AECB File Offset: 0x000090CB
		public static LocalizedString PasswordLabel
		{
			get
			{
				return new LocalizedString("PasswordLabel", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000278 RID: 632
		// (get) Token: 0x06000285 RID: 645 RVA: 0x0000AEE9 File Offset: 0x000090E9
		public static LocalizedString InboxRuleHasAttachmentConditionText
		{
			get
			{
				return new LocalizedString("InboxRuleHasAttachmentConditionText", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000279 RID: 633
		// (get) Token: 0x06000286 RID: 646 RVA: 0x0000AF07 File Offset: 0x00009107
		public static LocalizedString CalendarPublishingLinks
		{
			get
			{
				return new LocalizedString("CalendarPublishingLinks", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700027A RID: 634
		// (get) Token: 0x06000287 RID: 647 RVA: 0x0000AF25 File Offset: 0x00009125
		public static LocalizedString RequirementsReadWriteItemValue
		{
			get
			{
				return new LocalizedString("RequirementsReadWriteItemValue", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700027B RID: 635
		// (get) Token: 0x06000288 RID: 648 RVA: 0x0000AF43 File Offset: 0x00009143
		public static LocalizedString ImportContactListProgress
		{
			get
			{
				return new LocalizedString("ImportContactListProgress", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700027C RID: 636
		// (get) Token: 0x06000289 RID: 649 RVA: 0x0000AF61 File Offset: 0x00009161
		public static LocalizedString TrialReminderActionLinkText
		{
			get
			{
				return new LocalizedString("TrialReminderActionLinkText", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700027D RID: 637
		// (get) Token: 0x0600028A RID: 650 RVA: 0x0000AF7F File Offset: 0x0000917F
		public static LocalizedString VoiceMailNotificationsLink
		{
			get
			{
				return new LocalizedString("VoiceMailNotificationsLink", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700027E RID: 638
		// (get) Token: 0x0600028B RID: 651 RVA: 0x0000AF9D File Offset: 0x0000919D
		public static LocalizedString InboxRuleMyNameInToCcBoxConditionText
		{
			get
			{
				return new LocalizedString("InboxRuleMyNameInToCcBoxConditionText", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700027F RID: 639
		// (get) Token: 0x0600028C RID: 652 RVA: 0x0000AFBB File Offset: 0x000091BB
		public static LocalizedString InstallFromFile
		{
			get
			{
				return new LocalizedString("InstallFromFile", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000280 RID: 640
		// (get) Token: 0x0600028D RID: 653 RVA: 0x0000AFD9 File Offset: 0x000091D9
		public static LocalizedString DeviceMOWAVersionLabel
		{
			get
			{
				return new LocalizedString("DeviceMOWAVersionLabel", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000281 RID: 641
		// (get) Token: 0x0600028E RID: 654 RVA: 0x0000AFF7 File Offset: 0x000091F7
		public static LocalizedString Subject
		{
			get
			{
				return new LocalizedString("Subject", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000282 RID: 642
		// (get) Token: 0x0600028F RID: 655 RVA: 0x0000B015 File Offset: 0x00009215
		public static LocalizedString EditAccountCommandText
		{
			get
			{
				return new LocalizedString("EditAccountCommandText", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000283 RID: 643
		// (get) Token: 0x06000290 RID: 656 RVA: 0x0000B033 File Offset: 0x00009233
		public static LocalizedString AutomaticRepliesScheduledCheckboxText
		{
			get
			{
				return new LocalizedString("AutomaticRepliesScheduledCheckboxText", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000284 RID: 644
		// (get) Token: 0x06000291 RID: 657 RVA: 0x0000B051 File Offset: 0x00009251
		public static LocalizedString RetentionActionTypeHeader
		{
			get
			{
				return new LocalizedString("RetentionActionTypeHeader", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000285 RID: 645
		// (get) Token: 0x06000292 RID: 658 RVA: 0x0000B06F File Offset: 0x0000926F
		public static LocalizedString QLOutlook
		{
			get
			{
				return new LocalizedString("QLOutlook", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000286 RID: 646
		// (get) Token: 0x06000293 RID: 659 RVA: 0x0000B08D File Offset: 0x0000928D
		public static LocalizedString InboxRuleFromConditionText
		{
			get
			{
				return new LocalizedString("InboxRuleFromConditionText", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000287 RID: 647
		// (get) Token: 0x06000294 RID: 660 RVA: 0x0000B0AB File Offset: 0x000092AB
		public static LocalizedString MessageTrackingFailedEvent
		{
			get
			{
				return new LocalizedString("MessageTrackingFailedEvent", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000288 RID: 648
		// (get) Token: 0x06000295 RID: 661 RVA: 0x0000B0C9 File Offset: 0x000092C9
		public static LocalizedString IncomingSecurityTLS
		{
			get
			{
				return new LocalizedString("IncomingSecurityTLS", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000289 RID: 649
		// (get) Token: 0x06000296 RID: 662 RVA: 0x0000B0E7 File Offset: 0x000092E7
		public static LocalizedString WithinSizeRangeConditionFormat
		{
			get
			{
				return new LocalizedString("WithinSizeRangeConditionFormat", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700028A RID: 650
		// (get) Token: 0x06000297 RID: 663 RVA: 0x0000B105 File Offset: 0x00009305
		public static LocalizedString VoicemailCallFwdStep3
		{
			get
			{
				return new LocalizedString("VoicemailCallFwdStep3", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700028B RID: 651
		// (get) Token: 0x06000298 RID: 664 RVA: 0x0000B123 File Offset: 0x00009323
		public static LocalizedString MyMailbox
		{
			get
			{
				return new LocalizedString("MyMailbox", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700028C RID: 652
		// (get) Token: 0x06000299 RID: 665 RVA: 0x0000B141 File Offset: 0x00009341
		public static LocalizedString CalendarPublishingDetail
		{
			get
			{
				return new LocalizedString("CalendarPublishingDetail", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700028D RID: 653
		// (get) Token: 0x0600029A RID: 666 RVA: 0x0000B15F File Offset: 0x0000935F
		public static LocalizedString DeviceLastSuccessfulSyncLabel
		{
			get
			{
				return new LocalizedString("DeviceLastSuccessfulSyncLabel", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700028E RID: 654
		// (get) Token: 0x0600029B RID: 667 RVA: 0x0000B17D File Offset: 0x0000937D
		public static LocalizedString ClearButtonText
		{
			get
			{
				return new LocalizedString("ClearButtonText", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700028F RID: 655
		// (get) Token: 0x0600029C RID: 668 RVA: 0x0000B19B File Offset: 0x0000939B
		public static LocalizedString InboxRuleCopyToFolderActionText
		{
			get
			{
				return new LocalizedString("InboxRuleCopyToFolderActionText", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000290 RID: 656
		// (get) Token: 0x0600029D RID: 669 RVA: 0x0000B1B9 File Offset: 0x000093B9
		public static LocalizedString RPTYearsMonths
		{
			get
			{
				return new LocalizedString("RPTYearsMonths", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000291 RID: 657
		// (get) Token: 0x0600029E RID: 670 RVA: 0x0000B1D7 File Offset: 0x000093D7
		public static LocalizedString FromConditionFormat
		{
			get
			{
				return new LocalizedString("FromConditionFormat", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000292 RID: 658
		// (get) Token: 0x0600029F RID: 671 RVA: 0x0000B1F5 File Offset: 0x000093F5
		public static LocalizedString DeviceUserAgentLabel
		{
			get
			{
				return new LocalizedString("DeviceUserAgentLabel", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000293 RID: 659
		// (get) Token: 0x060002A0 RID: 672 RVA: 0x0000B213 File Offset: 0x00009413
		public static LocalizedString DepartGroupConfirmation
		{
			get
			{
				return new LocalizedString("DepartGroupConfirmation", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000294 RID: 660
		// (get) Token: 0x060002A1 RID: 673 RVA: 0x0000B231 File Offset: 0x00009431
		public static LocalizedString InboxRuleWithImportanceConditionText
		{
			get
			{
				return new LocalizedString("InboxRuleWithImportanceConditionText", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000295 RID: 661
		// (get) Token: 0x060002A2 RID: 674 RVA: 0x0000B24F File Offset: 0x0000944F
		public static LocalizedString PersonalSettingDomainUser
		{
			get
			{
				return new LocalizedString("PersonalSettingDomainUser", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000296 RID: 662
		// (get) Token: 0x060002A3 RID: 675 RVA: 0x0000B26D File Offset: 0x0000946D
		public static LocalizedString SiteMailboxEmailMeDiagnosticsButtonString
		{
			get
			{
				return new LocalizedString("SiteMailboxEmailMeDiagnosticsButtonString", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000297 RID: 663
		// (get) Token: 0x060002A4 RID: 676 RVA: 0x0000B28B File Offset: 0x0000948B
		public static LocalizedString DeliveryManagement
		{
			get
			{
				return new LocalizedString("DeliveryManagement", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000298 RID: 664
		// (get) Token: 0x060002A5 RID: 677 RVA: 0x0000B2A9 File Offset: 0x000094A9
		public static LocalizedString PersonalSettingPasswordBeforeChange
		{
			get
			{
				return new LocalizedString("PersonalSettingPasswordBeforeChange", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000299 RID: 665
		// (get) Token: 0x060002A6 RID: 678 RVA: 0x0000B2C7 File Offset: 0x000094C7
		public static LocalizedString SmtpSetting
		{
			get
			{
				return new LocalizedString("SmtpSetting", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700029A RID: 666
		// (get) Token: 0x060002A7 RID: 679 RVA: 0x0000B2E5 File Offset: 0x000094E5
		public static LocalizedString MessageFormatSlab
		{
			get
			{
				return new LocalizedString("MessageFormatSlab", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700029B RID: 667
		// (get) Token: 0x060002A8 RID: 680 RVA: 0x0000B303 File Offset: 0x00009503
		public static LocalizedString InboxRuleForwardToActionFlyOutText
		{
			get
			{
				return new LocalizedString("InboxRuleForwardToActionFlyOutText", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700029C RID: 668
		// (get) Token: 0x060002A9 RID: 681 RVA: 0x0000B321 File Offset: 0x00009521
		public static LocalizedString DuplicateProxyAddressError
		{
			get
			{
				return new LocalizedString("DuplicateProxyAddressError", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700029D RID: 669
		// (get) Token: 0x060002AA RID: 682 RVA: 0x0000B33F File Offset: 0x0000953F
		public static LocalizedString CreatedBy
		{
			get
			{
				return new LocalizedString("CreatedBy", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700029E RID: 670
		// (get) Token: 0x060002AB RID: 683 RVA: 0x0000B35D File Offset: 0x0000955D
		public static LocalizedString ReadReceiptResponseNever
		{
			get
			{
				return new LocalizedString("ReadReceiptResponseNever", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700029F RID: 671
		// (get) Token: 0x060002AC RID: 684 RVA: 0x0000B37B File Offset: 0x0000957B
		public static LocalizedString AccessDeniedFooterBottom
		{
			get
			{
				return new LocalizedString("AccessDeniedFooterBottom", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170002A0 RID: 672
		// (get) Token: 0x060002AD RID: 685 RVA: 0x0000B399 File Offset: 0x00009599
		public static LocalizedString OwnerChangedUpdateModerator
		{
			get
			{
				return new LocalizedString("OwnerChangedUpdateModerator", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170002A1 RID: 673
		// (get) Token: 0x060002AE RID: 686 RVA: 0x0000B3B7 File Offset: 0x000095B7
		public static LocalizedString AllowRecurringMeetings
		{
			get
			{
				return new LocalizedString("AllowRecurringMeetings", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170002A2 RID: 674
		// (get) Token: 0x060002AF RID: 687 RVA: 0x0000B3D5 File Offset: 0x000095D5
		public static LocalizedString VoicemailWizardStep1Title
		{
			get
			{
				return new LocalizedString("VoicemailWizardStep1Title", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170002A3 RID: 675
		// (get) Token: 0x060002B0 RID: 688 RVA: 0x0000B3F3 File Offset: 0x000095F3
		public static LocalizedString TeamMailboxMembersString
		{
			get
			{
				return new LocalizedString("TeamMailboxMembersString", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170002A4 RID: 676
		// (get) Token: 0x060002B1 RID: 689 RVA: 0x0000B411 File Offset: 0x00009611
		public static LocalizedString AfterDateDisplayTemplate
		{
			get
			{
				return new LocalizedString("AfterDateDisplayTemplate", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170002A5 RID: 677
		// (get) Token: 0x060002B2 RID: 690 RVA: 0x0000B42F File Offset: 0x0000962F
		public static LocalizedString TuesdayCheckBoxText
		{
			get
			{
				return new LocalizedString("TuesdayCheckBoxText", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170002A6 RID: 678
		// (get) Token: 0x060002B3 RID: 691 RVA: 0x0000B44D File Offset: 0x0000964D
		public static LocalizedString Join
		{
			get
			{
				return new LocalizedString("Join", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170002A7 RID: 679
		// (get) Token: 0x060002B4 RID: 692 RVA: 0x0000B46B File Offset: 0x0000966B
		public static LocalizedString AddAdditionalResponse
		{
			get
			{
				return new LocalizedString("AddAdditionalResponse", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170002A8 RID: 680
		// (get) Token: 0x060002B5 RID: 693 RVA: 0x0000B489 File Offset: 0x00009689
		public static LocalizedString OkButtonText
		{
			get
			{
				return new LocalizedString("OkButtonText", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170002A9 RID: 681
		// (get) Token: 0x060002B6 RID: 694 RVA: 0x0000B4A7 File Offset: 0x000096A7
		public static LocalizedString FoldersSyncedLabel
		{
			get
			{
				return new LocalizedString("FoldersSyncedLabel", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170002AA RID: 682
		// (get) Token: 0x060002B7 RID: 695 RVA: 0x0000B4C5 File Offset: 0x000096C5
		public static LocalizedString SendToAllText
		{
			get
			{
				return new LocalizedString("SendToAllText", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170002AB RID: 683
		// (get) Token: 0x060002B8 RID: 696 RVA: 0x0000B4E3 File Offset: 0x000096E3
		public static LocalizedString MaximumDurationInMinutesErrorMessage
		{
			get
			{
				return new LocalizedString("MaximumDurationInMinutesErrorMessage", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170002AC RID: 684
		// (get) Token: 0x060002B9 RID: 697 RVA: 0x0000B501 File Offset: 0x00009701
		public static LocalizedString TextMessagingTurnedOnViaEas
		{
			get
			{
				return new LocalizedString("TextMessagingTurnedOnViaEas", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170002AD RID: 685
		// (get) Token: 0x060002BA RID: 698 RVA: 0x0000B51F File Offset: 0x0000971F
		public static LocalizedString StartLoggingCommandText
		{
			get
			{
				return new LocalizedString("StartLoggingCommandText", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170002AE RID: 686
		// (get) Token: 0x060002BB RID: 699 RVA: 0x0000B53D File Offset: 0x0000973D
		public static LocalizedString MailboxUsageLegacyText
		{
			get
			{
				return new LocalizedString("MailboxUsageLegacyText", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170002AF RID: 687
		// (get) Token: 0x060002BC RID: 700 RVA: 0x0000B55B File Offset: 0x0000975B
		public static LocalizedString RPTYears
		{
			get
			{
				return new LocalizedString("RPTYears", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170002B0 RID: 688
		// (get) Token: 0x060002BD RID: 701 RVA: 0x0000B579 File Offset: 0x00009779
		public static LocalizedString ReadReceiptResponseInstruction
		{
			get
			{
				return new LocalizedString("ReadReceiptResponseInstruction", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170002B1 RID: 689
		// (get) Token: 0x060002BE RID: 702 RVA: 0x0000B597 File Offset: 0x00009797
		public static LocalizedString RemindersEnabled
		{
			get
			{
				return new LocalizedString("RemindersEnabled", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170002B2 RID: 690
		// (get) Token: 0x060002BF RID: 703 RVA: 0x0000B5B5 File Offset: 0x000097B5
		public static LocalizedString AddNewRequestsTentativelyCheckBoxText
		{
			get
			{
				return new LocalizedString("AddNewRequestsTentativelyCheckBoxText", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170002B3 RID: 691
		// (get) Token: 0x060002C0 RID: 704 RVA: 0x0000B5D3 File Offset: 0x000097D3
		public static LocalizedString MessageTrackingSubmitEvent
		{
			get
			{
				return new LocalizedString("MessageTrackingSubmitEvent", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170002B4 RID: 692
		// (get) Token: 0x060002C1 RID: 705 RVA: 0x0000B5F1 File Offset: 0x000097F1
		public static LocalizedString VoicemailLearnMore
		{
			get
			{
				return new LocalizedString("VoicemailLearnMore", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170002B5 RID: 693
		// (get) Token: 0x060002C2 RID: 706 RVA: 0x0000B60F File Offset: 0x0000980F
		public static LocalizedString EmailThisReport
		{
			get
			{
				return new LocalizedString("EmailThisReport", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170002B6 RID: 694
		// (get) Token: 0x060002C3 RID: 707 RVA: 0x0000B62D File Offset: 0x0000982D
		public static LocalizedString CalendarPublishingDateRange
		{
			get
			{
				return new LocalizedString("CalendarPublishingDateRange", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170002B7 RID: 695
		// (get) Token: 0x060002C4 RID: 708 RVA: 0x0000B64B File Offset: 0x0000984B
		public static LocalizedString SelectUsersAndGroups
		{
			get
			{
				return new LocalizedString("SelectUsersAndGroups", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170002B8 RID: 696
		// (get) Token: 0x060002C5 RID: 709 RVA: 0x0000B669 File Offset: 0x00009869
		public static LocalizedString PhotoBookmark
		{
			get
			{
				return new LocalizedString("PhotoBookmark", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170002B9 RID: 697
		// (get) Token: 0x060002C6 RID: 710 RVA: 0x0000B687 File Offset: 0x00009887
		public static LocalizedString InboxRuleHeaderContainsConditionText
		{
			get
			{
				return new LocalizedString("InboxRuleHeaderContainsConditionText", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170002BA RID: 698
		// (get) Token: 0x060002C7 RID: 711 RVA: 0x0000B6A5 File Offset: 0x000098A5
		public static LocalizedString BookingWindowInDaysErrorMessage
		{
			get
			{
				return new LocalizedString("BookingWindowInDaysErrorMessage", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170002BB RID: 699
		// (get) Token: 0x060002C8 RID: 712 RVA: 0x0000B6C3 File Offset: 0x000098C3
		public static LocalizedString Calendar
		{
			get
			{
				return new LocalizedString("Calendar", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170002BC RID: 700
		// (get) Token: 0x060002C9 RID: 713 RVA: 0x0000B6E1 File Offset: 0x000098E1
		public static LocalizedString RuleSubjectContainsAndMoveToFolderTemplate
		{
			get
			{
				return new LocalizedString("RuleSubjectContainsAndMoveToFolderTemplate", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170002BD RID: 701
		// (get) Token: 0x060002CA RID: 714 RVA: 0x0000B6FF File Offset: 0x000098FF
		public static LocalizedString RuleStateOn
		{
			get
			{
				return new LocalizedString("RuleStateOn", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170002BE RID: 702
		// (get) Token: 0x060002CB RID: 715 RVA: 0x0000B71D File Offset: 0x0000991D
		public static LocalizedString UserLogonNameLabel
		{
			get
			{
				return new LocalizedString("UserLogonNameLabel", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170002BF RID: 703
		// (get) Token: 0x060002CC RID: 716 RVA: 0x0000B73B File Offset: 0x0000993B
		public static LocalizedString RPTMonth
		{
			get
			{
				return new LocalizedString("RPTMonth", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170002C0 RID: 704
		// (get) Token: 0x060002CD RID: 717 RVA: 0x0000B759 File Offset: 0x00009959
		public static LocalizedString EndTime
		{
			get
			{
				return new LocalizedString("EndTime", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170002C1 RID: 705
		// (get) Token: 0x060002CE RID: 718 RVA: 0x0000B777 File Offset: 0x00009977
		public static LocalizedString InstallFromPrivateUrlTitle
		{
			get
			{
				return new LocalizedString("InstallFromPrivateUrlTitle", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170002C2 RID: 706
		// (get) Token: 0x060002CF RID: 719 RVA: 0x0000B795 File Offset: 0x00009995
		public static LocalizedString LastSyncAttemptTimeHeaderText
		{
			get
			{
				return new LocalizedString("LastSyncAttemptTimeHeaderText", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170002C3 RID: 707
		// (get) Token: 0x060002D0 RID: 720 RVA: 0x0000B7B3 File Offset: 0x000099B3
		public static LocalizedString RequirementsReadWriteItemDescription
		{
			get
			{
				return new LocalizedString("RequirementsReadWriteItemDescription", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170002C4 RID: 708
		// (get) Token: 0x060002D1 RID: 721 RVA: 0x0000B7D1 File Offset: 0x000099D1
		public static LocalizedString NewItemNotificationVoiceMailToast
		{
			get
			{
				return new LocalizedString("NewItemNotificationVoiceMailToast", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170002C5 RID: 709
		// (get) Token: 0x060002D2 RID: 722 RVA: 0x0000B7EF File Offset: 0x000099EF
		public static LocalizedString RenameDefaultFoldersCheckBoxText
		{
			get
			{
				return new LocalizedString("RenameDefaultFoldersCheckBoxText", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170002C6 RID: 710
		// (get) Token: 0x060002D3 RID: 723 RVA: 0x0000B80D File Offset: 0x00009A0D
		public static LocalizedString InboxRuleFromSubscriptionConditionText
		{
			get
			{
				return new LocalizedString("InboxRuleFromSubscriptionConditionText", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170002C7 RID: 711
		// (get) Token: 0x060002D4 RID: 724 RVA: 0x0000B82B File Offset: 0x00009A2B
		public static LocalizedString RetentionSelectOptionalLabel
		{
			get
			{
				return new LocalizedString("RetentionSelectOptionalLabel", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170002C8 RID: 712
		// (get) Token: 0x060002D5 RID: 725 RVA: 0x0000B849 File Offset: 0x00009A49
		public static LocalizedString FromColumnLabel
		{
			get
			{
				return new LocalizedString("FromColumnLabel", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170002C9 RID: 713
		// (get) Token: 0x060002D6 RID: 726 RVA: 0x0000B867 File Offset: 0x00009A67
		public static LocalizedString ExtensionCanNotBeDisabledNorUninstalled
		{
			get
			{
				return new LocalizedString("ExtensionCanNotBeDisabledNorUninstalled", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170002CA RID: 714
		// (get) Token: 0x060002D7 RID: 727 RVA: 0x0000B885 File Offset: 0x00009A85
		public static LocalizedString CalendarPublishingPublic
		{
			get
			{
				return new LocalizedString("CalendarPublishingPublic", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170002CB RID: 715
		// (get) Token: 0x060002D8 RID: 728 RVA: 0x0000B8A3 File Offset: 0x00009AA3
		public static LocalizedString CalendarSharingExplanation
		{
			get
			{
				return new LocalizedString("CalendarSharingExplanation", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170002CC RID: 716
		// (get) Token: 0x060002D9 RID: 729 RVA: 0x0000B8C1 File Offset: 0x00009AC1
		public static LocalizedString CalendarPublishingViewUrl
		{
			get
			{
				return new LocalizedString("CalendarPublishingViewUrl", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170002CD RID: 717
		// (get) Token: 0x060002DA RID: 730 RVA: 0x0000B8DF File Offset: 0x00009ADF
		public static LocalizedString SaturdayCheckBoxText
		{
			get
			{
				return new LocalizedString("SaturdayCheckBoxText", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170002CE RID: 718
		// (get) Token: 0x060002DB RID: 731 RVA: 0x0000B8FD File Offset: 0x00009AFD
		public static LocalizedString MailboxUsageUnitB
		{
			get
			{
				return new LocalizedString("MailboxUsageUnitB", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170002CF RID: 719
		// (get) Token: 0x060002DC RID: 732 RVA: 0x0000B91B File Offset: 0x00009B1B
		public static LocalizedString HasAttachmentConditionFormat
		{
			get
			{
				return new LocalizedString("HasAttachmentConditionFormat", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170002D0 RID: 720
		// (get) Token: 0x060002DD RID: 733 RVA: 0x0000B939 File Offset: 0x00009B39
		public static LocalizedString VoicemailWizardStep5Title
		{
			get
			{
				return new LocalizedString("VoicemailWizardStep5Title", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170002D1 RID: 721
		// (get) Token: 0x060002DE RID: 734 RVA: 0x0000B957 File Offset: 0x00009B57
		public static LocalizedString PreviewMarkAsReadBehaviorNever
		{
			get
			{
				return new LocalizedString("PreviewMarkAsReadBehaviorNever", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170002D2 RID: 722
		// (get) Token: 0x060002DF RID: 735 RVA: 0x0000B975 File Offset: 0x00009B75
		public static LocalizedString SubscriptionDialogLabel
		{
			get
			{
				return new LocalizedString("SubscriptionDialogLabel", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170002D3 RID: 723
		// (get) Token: 0x060002E0 RID: 736 RVA: 0x0000B993 File Offset: 0x00009B93
		public static LocalizedString NewSubscription
		{
			get
			{
				return new LocalizedString("NewSubscription", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170002D4 RID: 724
		// (get) Token: 0x060002E1 RID: 737 RVA: 0x0000B9B1 File Offset: 0x00009BB1
		public static LocalizedString LastSynchronization
		{
			get
			{
				return new LocalizedString("LastSynchronization", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170002D5 RID: 725
		// (get) Token: 0x060002E2 RID: 738 RVA: 0x0000B9CF File Offset: 0x00009BCF
		public static LocalizedString ChangeCalendarPermissions
		{
			get
			{
				return new LocalizedString("ChangeCalendarPermissions", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170002D6 RID: 726
		// (get) Token: 0x060002E3 RID: 739 RVA: 0x0000B9ED File Offset: 0x00009BED
		public static LocalizedString DefaultReminderTimeLabel
		{
			get
			{
				return new LocalizedString("DefaultReminderTimeLabel", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170002D7 RID: 727
		// (get) Token: 0x060002E4 RID: 740 RVA: 0x0000BA0B File Offset: 0x00009C0B
		public static LocalizedString OpenNextItem
		{
			get
			{
				return new LocalizedString("OpenNextItem", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170002D8 RID: 728
		// (get) Token: 0x060002E5 RID: 741 RVA: 0x0000BA29 File Offset: 0x00009C29
		public static LocalizedString EmailOptions
		{
			get
			{
				return new LocalizedString("EmailOptions", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170002D9 RID: 729
		// (get) Token: 0x060002E6 RID: 742 RVA: 0x0000BA47 File Offset: 0x00009C47
		public static LocalizedString RetentionActionTypeDefaultDelete
		{
			get
			{
				return new LocalizedString("RetentionActionTypeDefaultDelete", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170002DA RID: 730
		// (get) Token: 0x060002E7 RID: 743 RVA: 0x0000BA65 File Offset: 0x00009C65
		public static LocalizedString ChangeCommandText
		{
			get
			{
				return new LocalizedString("ChangeCommandText", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170002DB RID: 731
		// (get) Token: 0x060002E8 RID: 744 RVA: 0x0000BA83 File Offset: 0x00009C83
		public static LocalizedString ExternalMessageGalLessInstruction
		{
			get
			{
				return new LocalizedString("ExternalMessageGalLessInstruction", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170002DC RID: 732
		// (get) Token: 0x060002E9 RID: 745 RVA: 0x0000BAA1 File Offset: 0x00009CA1
		public static LocalizedString ImportContactListNoFileUploaded
		{
			get
			{
				return new LocalizedString("ImportContactListNoFileUploaded", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170002DD RID: 733
		// (get) Token: 0x060002EA RID: 746 RVA: 0x0000BABF File Offset: 0x00009CBF
		public static LocalizedString BadOfficeCallbackMessage
		{
			get
			{
				return new LocalizedString("BadOfficeCallbackMessage", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170002DE RID: 734
		// (get) Token: 0x060002EB RID: 747 RVA: 0x0000BADD File Offset: 0x00009CDD
		public static LocalizedString DefaultImage
		{
			get
			{
				return new LocalizedString("DefaultImage", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170002DF RID: 735
		// (get) Token: 0x060002EC RID: 748 RVA: 0x0000BAFB File Offset: 0x00009CFB
		public static LocalizedString JoinAndDepart
		{
			get
			{
				return new LocalizedString("JoinAndDepart", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170002E0 RID: 736
		// (get) Token: 0x060002ED RID: 749 RVA: 0x0000BB19 File Offset: 0x00009D19
		public static LocalizedString InboxRuleMyNameInToCcBoxConditionFlyOutText
		{
			get
			{
				return new LocalizedString("InboxRuleMyNameInToCcBoxConditionFlyOutText", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170002E1 RID: 737
		// (get) Token: 0x060002EE RID: 750 RVA: 0x0000BB37 File Offset: 0x00009D37
		public static LocalizedString TeamMailboxTitleString
		{
			get
			{
				return new LocalizedString("TeamMailboxTitleString", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170002E2 RID: 738
		// (get) Token: 0x060002EF RID: 751 RVA: 0x0000BB55 File Offset: 0x00009D55
		public static LocalizedString NewestOnTop
		{
			get
			{
				return new LocalizedString("NewestOnTop", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170002E3 RID: 739
		// (get) Token: 0x060002F0 RID: 752 RVA: 0x0000BB73 File Offset: 0x00009D73
		public static LocalizedString ToWithExample
		{
			get
			{
				return new LocalizedString("ToWithExample", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170002E4 RID: 740
		// (get) Token: 0x060002F1 RID: 753 RVA: 0x0000BB91 File Offset: 0x00009D91
		public static LocalizedString GroupOwners
		{
			get
			{
				return new LocalizedString("GroupOwners", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170002E5 RID: 741
		// (get) Token: 0x060002F2 RID: 754 RVA: 0x0000BBAF File Offset: 0x00009DAF
		public static LocalizedString DefaultRetentionTagDescription
		{
			get
			{
				return new LocalizedString("DefaultRetentionTagDescription", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170002E6 RID: 742
		// (get) Token: 0x060002F3 RID: 755 RVA: 0x0000BBCD File Offset: 0x00009DCD
		public static LocalizedString Hours
		{
			get
			{
				return new LocalizedString("Hours", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170002E7 RID: 743
		// (get) Token: 0x060002F4 RID: 756 RVA: 0x0000BBEB File Offset: 0x00009DEB
		public static LocalizedString MessageTypeDialogTitle
		{
			get
			{
				return new LocalizedString("MessageTypeDialogTitle", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170002E8 RID: 744
		// (get) Token: 0x060002F5 RID: 757 RVA: 0x0000BC09 File Offset: 0x00009E09
		public static LocalizedString SearchDeliveryReports
		{
			get
			{
				return new LocalizedString("SearchDeliveryReports", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170002E9 RID: 745
		// (get) Token: 0x060002F6 RID: 758 RVA: 0x0000BC27 File Offset: 0x00009E27
		public static LocalizedString VoicemailCallFwdStep1
		{
			get
			{
				return new LocalizedString("VoicemailCallFwdStep1", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170002EA RID: 746
		// (get) Token: 0x060002F7 RID: 759 RVA: 0x0000BC45 File Offset: 0x00009E45
		public static LocalizedString SendAddressSetting
		{
			get
			{
				return new LocalizedString("SendAddressSetting", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170002EB RID: 747
		// (get) Token: 0x060002F8 RID: 760 RVA: 0x0000BC63 File Offset: 0x00009E63
		public static LocalizedString InboxRuleItIsGroupText
		{
			get
			{
				return new LocalizedString("InboxRuleItIsGroupText", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170002EC RID: 748
		// (get) Token: 0x060002F9 RID: 761 RVA: 0x0000BC81 File Offset: 0x00009E81
		public static LocalizedString LaunchOfficeMarketplace
		{
			get
			{
				return new LocalizedString("LaunchOfficeMarketplace", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170002ED RID: 749
		// (get) Token: 0x060002FA RID: 762 RVA: 0x0000BC9F File Offset: 0x00009E9F
		public static LocalizedString CalendarDiagnosticLogDescription
		{
			get
			{
				return new LocalizedString("CalendarDiagnosticLogDescription", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170002EE RID: 750
		// (get) Token: 0x060002FB RID: 763 RVA: 0x0000BCBD File Offset: 0x00009EBD
		public static LocalizedString InboxRules
		{
			get
			{
				return new LocalizedString("InboxRules", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170002EF RID: 751
		// (get) Token: 0x060002FC RID: 764 RVA: 0x0000BCDB File Offset: 0x00009EDB
		public static LocalizedString VoicemailSMSOptionVoiceMailAndMissedCalls
		{
			get
			{
				return new LocalizedString("VoicemailSMSOptionVoiceMailAndMissedCalls", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170002F0 RID: 752
		// (get) Token: 0x060002FD RID: 765 RVA: 0x0000BCF9 File Offset: 0x00009EF9
		public static LocalizedString StartTime
		{
			get
			{
				return new LocalizedString("StartTime", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170002F1 RID: 753
		// (get) Token: 0x060002FE RID: 766 RVA: 0x0000BD17 File Offset: 0x00009F17
		public static LocalizedString TextMessagingSlabMessage
		{
			get
			{
				return new LocalizedString("TextMessagingSlabMessage", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060002FF RID: 767 RVA: 0x0000BD38 File Offset: 0x00009F38
		public static LocalizedString JoinDlSuccess(string name)
		{
			return new LocalizedString("JoinDlSuccess", "", false, false, OwaOptionStrings.ResourceManager, new object[]
			{
				name
			});
		}

		// Token: 0x170002F2 RID: 754
		// (get) Token: 0x06000300 RID: 768 RVA: 0x0000BD67 File Offset: 0x00009F67
		public static LocalizedString WipeDeviceConfirmMessageDetail
		{
			get
			{
				return new LocalizedString("WipeDeviceConfirmMessageDetail", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170002F3 RID: 755
		// (get) Token: 0x06000301 RID: 769 RVA: 0x0000BD85 File Offset: 0x00009F85
		public static LocalizedString VoicemailWizardStep2DescriptionNoPasscode
		{
			get
			{
				return new LocalizedString("VoicemailWizardStep2DescriptionNoPasscode", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170002F4 RID: 756
		// (get) Token: 0x06000302 RID: 770 RVA: 0x0000BDA3 File Offset: 0x00009FA3
		public static LocalizedString DeleteInboxRulesConfirmation
		{
			get
			{
				return new LocalizedString("DeleteInboxRulesConfirmation", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170002F5 RID: 757
		// (get) Token: 0x06000303 RID: 771 RVA: 0x0000BDC1 File Offset: 0x00009FC1
		public static LocalizedString CalendarNotificationsLink
		{
			get
			{
				return new LocalizedString("CalendarNotificationsLink", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170002F6 RID: 758
		// (get) Token: 0x06000304 RID: 772 RVA: 0x0000BDDF File Offset: 0x00009FDF
		public static LocalizedString InboxRuleHasClassificationConditionFlyOutText
		{
			get
			{
				return new LocalizedString("InboxRuleHasClassificationConditionFlyOutText", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170002F7 RID: 759
		// (get) Token: 0x06000305 RID: 773 RVA: 0x0000BDFD File Offset: 0x00009FFD
		public static LocalizedString CalendarWorkflowInstruction
		{
			get
			{
				return new LocalizedString("CalendarWorkflowInstruction", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170002F8 RID: 760
		// (get) Token: 0x06000306 RID: 774 RVA: 0x0000BE1B File Offset: 0x0000A01B
		public static LocalizedString AutomaticRepliesInstruction
		{
			get
			{
				return new LocalizedString("AutomaticRepliesInstruction", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170002F9 RID: 761
		// (get) Token: 0x06000307 RID: 775 RVA: 0x0000BE39 File Offset: 0x0000A039
		public static LocalizedString DeviceLanguageLabel
		{
			get
			{
				return new LocalizedString("DeviceLanguageLabel", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170002FA RID: 762
		// (get) Token: 0x06000308 RID: 776 RVA: 0x0000BE57 File Offset: 0x0000A057
		public static LocalizedString MoveUp
		{
			get
			{
				return new LocalizedString("MoveUp", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170002FB RID: 763
		// (get) Token: 0x06000309 RID: 777 RVA: 0x0000BE75 File Offset: 0x0000A075
		public static LocalizedString InstallButtonText
		{
			get
			{
				return new LocalizedString("InstallButtonText", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170002FC RID: 764
		// (get) Token: 0x0600030A RID: 778 RVA: 0x0000BE93 File Offset: 0x0000A093
		public static LocalizedString WithSensitivityConditionFormat
		{
			get
			{
				return new LocalizedString("WithSensitivityConditionFormat", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170002FD RID: 765
		// (get) Token: 0x0600030B RID: 779 RVA: 0x0000BEB1 File Offset: 0x0000A0B1
		public static LocalizedString VoicemailWizardPinLabel
		{
			get
			{
				return new LocalizedString("VoicemailWizardPinLabel", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170002FE RID: 766
		// (get) Token: 0x0600030C RID: 780 RVA: 0x0000BECF File Offset: 0x0000A0CF
		public static LocalizedString MondayCheckBoxText
		{
			get
			{
				return new LocalizedString("MondayCheckBoxText", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170002FF RID: 767
		// (get) Token: 0x0600030D RID: 781 RVA: 0x0000BEED File Offset: 0x0000A0ED
		public static LocalizedString WithImportanceConditionFormat
		{
			get
			{
				return new LocalizedString("WithImportanceConditionFormat", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000300 RID: 768
		// (get) Token: 0x0600030E RID: 782 RVA: 0x0000BF0B File Offset: 0x0000A10B
		public static LocalizedString NewDistributionGroupText
		{
			get
			{
				return new LocalizedString("NewDistributionGroupText", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000301 RID: 769
		// (get) Token: 0x0600030F RID: 783 RVA: 0x0000BF29 File Offset: 0x0000A129
		public static LocalizedString CalendarReminderSlab
		{
			get
			{
				return new LocalizedString("CalendarReminderSlab", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000302 RID: 770
		// (get) Token: 0x06000310 RID: 784 RVA: 0x0000BF47 File Offset: 0x0000A147
		public static LocalizedString EmailAddressesLabel
		{
			get
			{
				return new LocalizedString("EmailAddressesLabel", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000303 RID: 771
		// (get) Token: 0x06000311 RID: 785 RVA: 0x0000BF65 File Offset: 0x0000A165
		public static LocalizedString PortLabel
		{
			get
			{
				return new LocalizedString("PortLabel", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000304 RID: 772
		// (get) Token: 0x06000312 RID: 786 RVA: 0x0000BF83 File Offset: 0x0000A183
		public static LocalizedString SetupVoiceMailNotificationsLink
		{
			get
			{
				return new LocalizedString("SetupVoiceMailNotificationsLink", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000305 RID: 773
		// (get) Token: 0x06000313 RID: 787 RVA: 0x0000BFA1 File Offset: 0x0000A1A1
		public static LocalizedString TextMessagingOff
		{
			get
			{
				return new LocalizedString("TextMessagingOff", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000306 RID: 774
		// (get) Token: 0x06000314 RID: 788 RVA: 0x0000BFBF File Offset: 0x0000A1BF
		public static LocalizedString RuleSentToAndMoveToFolderTemplate
		{
			get
			{
				return new LocalizedString("RuleSentToAndMoveToFolderTemplate", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000307 RID: 775
		// (get) Token: 0x06000315 RID: 789 RVA: 0x0000BFDD File Offset: 0x0000A1DD
		public static LocalizedString UserNameNotSetError
		{
			get
			{
				return new LocalizedString("UserNameNotSetError", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000308 RID: 776
		// (get) Token: 0x06000316 RID: 790 RVA: 0x0000BFFB File Offset: 0x0000A1FB
		public static LocalizedString MailboxUsageWarningText
		{
			get
			{
				return new LocalizedString("MailboxUsageWarningText", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000309 RID: 777
		// (get) Token: 0x06000317 RID: 791 RVA: 0x0000C019 File Offset: 0x0000A219
		public static LocalizedString PopSetting
		{
			get
			{
				return new LocalizedString("PopSetting", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700030A RID: 778
		// (get) Token: 0x06000318 RID: 792 RVA: 0x0000C037 File Offset: 0x0000A237
		public static LocalizedString InboxRuleFromAddressContainsConditionFlyOutText
		{
			get
			{
				return new LocalizedString("InboxRuleFromAddressContainsConditionFlyOutText", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700030B RID: 779
		// (get) Token: 0x06000319 RID: 793 RVA: 0x0000C055 File Offset: 0x0000A255
		public static LocalizedString Options
		{
			get
			{
				return new LocalizedString("Options", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700030C RID: 780
		// (get) Token: 0x0600031A RID: 794 RVA: 0x0000C073 File Offset: 0x0000A273
		public static LocalizedString EMailSignatureSlab
		{
			get
			{
				return new LocalizedString("EMailSignatureSlab", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700030D RID: 781
		// (get) Token: 0x0600031B RID: 795 RVA: 0x0000C091 File Offset: 0x0000A291
		public static LocalizedString RetentionPeriodHoldFor
		{
			get
			{
				return new LocalizedString("RetentionPeriodHoldFor", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700030E RID: 782
		// (get) Token: 0x0600031C RID: 796 RVA: 0x0000C0AF File Offset: 0x0000A2AF
		public static LocalizedString MaximumConflictInstancesErrorMessage
		{
			get
			{
				return new LocalizedString("MaximumConflictInstancesErrorMessage", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700030F RID: 783
		// (get) Token: 0x0600031D RID: 797 RVA: 0x0000C0CD File Offset: 0x0000A2CD
		public static LocalizedString Organize
		{
			get
			{
				return new LocalizedString("Organize", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000310 RID: 784
		// (get) Token: 0x0600031E RID: 798 RVA: 0x0000C0EB File Offset: 0x0000A2EB
		public static LocalizedString MessageTrackingDLExpandedEvent
		{
			get
			{
				return new LocalizedString("MessageTrackingDLExpandedEvent", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000311 RID: 785
		// (get) Token: 0x0600031F RID: 799 RVA: 0x0000C109 File Offset: 0x0000A309
		public static LocalizedString VoicemailBrowserNotSupported
		{
			get
			{
				return new LocalizedString("VoicemailBrowserNotSupported", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000312 RID: 786
		// (get) Token: 0x06000320 RID: 800 RVA: 0x0000C127 File Offset: 0x0000A327
		public static LocalizedString SendAddressSettingSlabDescription
		{
			get
			{
				return new LocalizedString("SendAddressSettingSlabDescription", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000313 RID: 787
		// (get) Token: 0x06000321 RID: 801 RVA: 0x0000C145 File Offset: 0x0000A345
		public static LocalizedString RetrieveLogConfirmMessage
		{
			get
			{
				return new LocalizedString("RetrieveLogConfirmMessage", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000314 RID: 788
		// (get) Token: 0x06000322 RID: 802 RVA: 0x0000C163 File Offset: 0x0000A363
		public static LocalizedString DescriptionLabel
		{
			get
			{
				return new LocalizedString("DescriptionLabel", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000315 RID: 789
		// (get) Token: 0x06000323 RID: 803 RVA: 0x0000C181 File Offset: 0x0000A381
		public static LocalizedString CustomAttributeDialogTitle
		{
			get
			{
				return new LocalizedString("CustomAttributeDialogTitle", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000316 RID: 790
		// (get) Token: 0x06000324 RID: 804 RVA: 0x0000C19F File Offset: 0x0000A39F
		public static LocalizedString MessageApproval
		{
			get
			{
				return new LocalizedString("MessageApproval", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000325 RID: 805 RVA: 0x0000C1C0 File Offset: 0x0000A3C0
		public static LocalizedString VoicemailAccessNumbersTemplate(string number)
		{
			return new LocalizedString("VoicemailAccessNumbersTemplate", "", false, false, OwaOptionStrings.ResourceManager, new object[]
			{
				number
			});
		}

		// Token: 0x17000317 RID: 791
		// (get) Token: 0x06000326 RID: 806 RVA: 0x0000C1EF File Offset: 0x0000A3EF
		public static LocalizedString TeamMailboxPropertiesString
		{
			get
			{
				return new LocalizedString("TeamMailboxPropertiesString", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000318 RID: 792
		// (get) Token: 0x06000327 RID: 807 RVA: 0x0000C20D File Offset: 0x0000A40D
		public static LocalizedString BodyContainsConditionFormat
		{
			get
			{
				return new LocalizedString("BodyContainsConditionFormat", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000319 RID: 793
		// (get) Token: 0x06000328 RID: 808 RVA: 0x0000C22B File Offset: 0x0000A42B
		public static LocalizedString GroupModeration
		{
			get
			{
				return new LocalizedString("GroupModeration", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700031A RID: 794
		// (get) Token: 0x06000329 RID: 809 RVA: 0x0000C249 File Offset: 0x0000A449
		public static LocalizedString FreeBusySubjectLocationInformation
		{
			get
			{
				return new LocalizedString("FreeBusySubjectLocationInformation", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700031B RID: 795
		// (get) Token: 0x0600032A RID: 810 RVA: 0x0000C267 File Offset: 0x0000A467
		public static LocalizedString InboxRuleMarkMessageGroupText
		{
			get
			{
				return new LocalizedString("InboxRuleMarkMessageGroupText", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700031C RID: 796
		// (get) Token: 0x0600032B RID: 811 RVA: 0x0000C285 File Offset: 0x0000A485
		public static LocalizedString InboxRuleConditionSectionHeader
		{
			get
			{
				return new LocalizedString("InboxRuleConditionSectionHeader", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700031D RID: 797
		// (get) Token: 0x0600032C RID: 812 RVA: 0x0000C2A3 File Offset: 0x0000A4A3
		public static LocalizedString RecipientEmailButtonDescription
		{
			get
			{
				return new LocalizedString("RecipientEmailButtonDescription", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700031E RID: 798
		// (get) Token: 0x0600032D RID: 813 RVA: 0x0000C2C1 File Offset: 0x0000A4C1
		public static LocalizedString TimeZone
		{
			get
			{
				return new LocalizedString("TimeZone", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700031F RID: 799
		// (get) Token: 0x0600032E RID: 814 RVA: 0x0000C2DF File Offset: 0x0000A4DF
		public static LocalizedString InboxRuleSentToConditionFlyOutText
		{
			get
			{
				return new LocalizedString("InboxRuleSentToConditionFlyOutText", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000320 RID: 800
		// (get) Token: 0x0600032F RID: 815 RVA: 0x0000C2FD File Offset: 0x0000A4FD
		public static LocalizedString MoveDown
		{
			get
			{
				return new LocalizedString("MoveDown", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000321 RID: 801
		// (get) Token: 0x06000330 RID: 816 RVA: 0x0000C31B File Offset: 0x0000A51B
		public static LocalizedString UnblockDeviceCommandText
		{
			get
			{
				return new LocalizedString("UnblockDeviceCommandText", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000322 RID: 802
		// (get) Token: 0x06000331 RID: 817 RVA: 0x0000C339 File Offset: 0x0000A539
		public static LocalizedString TextMessagingSms
		{
			get
			{
				return new LocalizedString("TextMessagingSms", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000323 RID: 803
		// (get) Token: 0x06000332 RID: 818 RVA: 0x0000C357 File Offset: 0x0000A557
		public static LocalizedString LanguageInstruction
		{
			get
			{
				return new LocalizedString("LanguageInstruction", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000324 RID: 804
		// (get) Token: 0x06000333 RID: 819 RVA: 0x0000C375 File Offset: 0x0000A575
		public static LocalizedString InboxRuleFromConditionFlyOutText
		{
			get
			{
				return new LocalizedString("InboxRuleFromConditionFlyOutText", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000325 RID: 805
		// (get) Token: 0x06000334 RID: 820 RVA: 0x0000C393 File Offset: 0x0000A593
		public static LocalizedString OutOfOffice
		{
			get
			{
				return new LocalizedString("OutOfOffice", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000326 RID: 806
		// (get) Token: 0x06000335 RID: 821 RVA: 0x0000C3B1 File Offset: 0x0000A5B1
		public static LocalizedString FirstNameLabel
		{
			get
			{
				return new LocalizedString("FirstNameLabel", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000327 RID: 807
		// (get) Token: 0x06000336 RID: 822 RVA: 0x0000C3CF File Offset: 0x0000A5CF
		public static LocalizedString InboxRuleBodyContainsConditionFlyOutText
		{
			get
			{
				return new LocalizedString("InboxRuleBodyContainsConditionFlyOutText", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000328 RID: 808
		// (get) Token: 0x06000337 RID: 823 RVA: 0x0000C3ED File Offset: 0x0000A5ED
		public static LocalizedString AllowedSendersLabelForEndUser
		{
			get
			{
				return new LocalizedString("AllowedSendersLabelForEndUser", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000329 RID: 809
		// (get) Token: 0x06000338 RID: 824 RVA: 0x0000C40B File Offset: 0x0000A60B
		public static LocalizedString VoicemailWizardStep2Title
		{
			get
			{
				return new LocalizedString("VoicemailWizardStep2Title", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700032A RID: 810
		// (get) Token: 0x06000339 RID: 825 RVA: 0x0000C429 File Offset: 0x0000A629
		public static LocalizedString ConversationsSlab
		{
			get
			{
				return new LocalizedString("ConversationsSlab", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700032B RID: 811
		// (get) Token: 0x0600033A RID: 826 RVA: 0x0000C447 File Offset: 0x0000A647
		public static LocalizedString AutomaticRepliesSlab
		{
			get
			{
				return new LocalizedString("AutomaticRepliesSlab", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700032C RID: 812
		// (get) Token: 0x0600033B RID: 827 RVA: 0x0000C465 File Offset: 0x0000A665
		public static LocalizedString IncomingAuthenticationNtlm
		{
			get
			{
				return new LocalizedString("IncomingAuthenticationNtlm", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700032D RID: 813
		// (get) Token: 0x0600033C RID: 828 RVA: 0x0000C483 File Offset: 0x0000A683
		public static LocalizedString MobilePhoneLabel
		{
			get
			{
				return new LocalizedString("MobilePhoneLabel", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700032E RID: 814
		// (get) Token: 0x0600033D RID: 829 RVA: 0x0000C4A1 File Offset: 0x0000A6A1
		public static LocalizedString DeviceNameLabel
		{
			get
			{
				return new LocalizedString("DeviceNameLabel", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700032F RID: 815
		// (get) Token: 0x0600033E RID: 830 RVA: 0x0000C4BF File Offset: 0x0000A6BF
		public static LocalizedString HeaderContainsConditionFormat
		{
			get
			{
				return new LocalizedString("HeaderContainsConditionFormat", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000330 RID: 816
		// (get) Token: 0x0600033F RID: 831 RVA: 0x0000C4DD File Offset: 0x0000A6DD
		public static LocalizedString TeamMailboxDescription
		{
			get
			{
				return new LocalizedString("TeamMailboxDescription", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000331 RID: 817
		// (get) Token: 0x06000340 RID: 832 RVA: 0x0000C4FB File Offset: 0x0000A6FB
		public static LocalizedString EnterNumberClickNext
		{
			get
			{
				return new LocalizedString("EnterNumberClickNext", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000332 RID: 818
		// (get) Token: 0x06000341 RID: 833 RVA: 0x0000C519 File Offset: 0x0000A719
		public static LocalizedString MobileDevices
		{
			get
			{
				return new LocalizedString("MobileDevices", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000333 RID: 819
		// (get) Token: 0x06000342 RID: 834 RVA: 0x0000C537 File Offset: 0x0000A737
		public static LocalizedString DisplayRecoveryPasswordCommandDescription
		{
			get
			{
				return new LocalizedString("DisplayRecoveryPasswordCommandDescription", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000334 RID: 820
		// (get) Token: 0x06000343 RID: 835 RVA: 0x0000C555 File Offset: 0x0000A755
		public static LocalizedString AtMostOnlyDisplayTemplate
		{
			get
			{
				return new LocalizedString("AtMostOnlyDisplayTemplate", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000335 RID: 821
		// (get) Token: 0x06000344 RID: 836 RVA: 0x0000C573 File Offset: 0x0000A773
		public static LocalizedString InboxRuleSentToConditionPreCannedText
		{
			get
			{
				return new LocalizedString("InboxRuleSentToConditionPreCannedText", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000336 RID: 822
		// (get) Token: 0x06000345 RID: 837 RVA: 0x0000C591 File Offset: 0x0000A791
		public static LocalizedString InboxRuleMarkedWithGroupText
		{
			get
			{
				return new LocalizedString("InboxRuleMarkedWithGroupText", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000337 RID: 823
		// (get) Token: 0x06000346 RID: 838 RVA: 0x0000C5AF File Offset: 0x0000A7AF
		public static LocalizedString Membership
		{
			get
			{
				return new LocalizedString("Membership", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000338 RID: 824
		// (get) Token: 0x06000347 RID: 839 RVA: 0x0000C5CD File Offset: 0x0000A7CD
		public static LocalizedString VoicemailSlab
		{
			get
			{
				return new LocalizedString("VoicemailSlab", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000339 RID: 825
		// (get) Token: 0x06000348 RID: 840 RVA: 0x0000C5EB File Offset: 0x0000A7EB
		public static LocalizedString AllInformation
		{
			get
			{
				return new LocalizedString("AllInformation", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700033A RID: 826
		// (get) Token: 0x06000349 RID: 841 RVA: 0x0000C609 File Offset: 0x0000A809
		public static LocalizedString BlockOrAllow
		{
			get
			{
				return new LocalizedString("BlockOrAllow", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700033B RID: 827
		// (get) Token: 0x0600034A RID: 842 RVA: 0x0000C627 File Offset: 0x0000A827
		public static LocalizedString CalendarDiagnosticLogWatermarkText
		{
			get
			{
				return new LocalizedString("CalendarDiagnosticLogWatermarkText", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700033C RID: 828
		// (get) Token: 0x0600034B RID: 843 RVA: 0x0000C645 File Offset: 0x0000A845
		public static LocalizedString EditGroups
		{
			get
			{
				return new LocalizedString("EditGroups", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700033D RID: 829
		// (get) Token: 0x0600034C RID: 844 RVA: 0x0000C663 File Offset: 0x0000A863
		public static LocalizedString TurnOnTextMessaging
		{
			get
			{
				return new LocalizedString("TurnOnTextMessaging", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700033E RID: 830
		// (get) Token: 0x0600034D RID: 845 RVA: 0x0000C681 File Offset: 0x0000A881
		public static LocalizedString AliasLabelForEnterprise
		{
			get
			{
				return new LocalizedString("AliasLabelForEnterprise", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700033F RID: 831
		// (get) Token: 0x0600034E RID: 846 RVA: 0x0000C69F File Offset: 0x0000A89F
		public static LocalizedString CategoryDialogTitle
		{
			get
			{
				return new LocalizedString("CategoryDialogTitle", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000340 RID: 832
		// (get) Token: 0x0600034F RID: 847 RVA: 0x0000C6BD File Offset: 0x0000A8BD
		public static LocalizedString SetYourWorkingHours
		{
			get
			{
				return new LocalizedString("SetYourWorkingHours", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000341 RID: 833
		// (get) Token: 0x06000350 RID: 848 RVA: 0x0000C6DB File Offset: 0x0000A8DB
		public static LocalizedString ProfileMailboxUsage
		{
			get
			{
				return new LocalizedString("ProfileMailboxUsage", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000342 RID: 834
		// (get) Token: 0x06000351 RID: 849 RVA: 0x0000C6F9 File Offset: 0x0000A8F9
		public static LocalizedString AtLeastAtMostDisplayTemplate
		{
			get
			{
				return new LocalizedString("AtLeastAtMostDisplayTemplate", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000343 RID: 835
		// (get) Token: 0x06000352 RID: 850 RVA: 0x0000C717 File Offset: 0x0000A917
		public static LocalizedString NotificationsForMeetingReminders
		{
			get
			{
				return new LocalizedString("NotificationsForMeetingReminders", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000344 RID: 836
		// (get) Token: 0x06000353 RID: 851 RVA: 0x0000C735 File Offset: 0x0000A935
		public static LocalizedString IncomingServerLabel
		{
			get
			{
				return new LocalizedString("IncomingServerLabel", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000345 RID: 837
		// (get) Token: 0x06000354 RID: 852 RVA: 0x0000C753 File Offset: 0x0000A953
		public static LocalizedString IncomingAuthenticationNone
		{
			get
			{
				return new LocalizedString("IncomingAuthenticationNone", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000346 RID: 838
		// (get) Token: 0x06000355 RID: 853 RVA: 0x0000C771 File Offset: 0x0000A971
		public static LocalizedString TrialReminderText
		{
			get
			{
				return new LocalizedString("TrialReminderText", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000347 RID: 839
		// (get) Token: 0x06000356 RID: 854 RVA: 0x0000C78F File Offset: 0x0000A98F
		public static LocalizedString GroupsIBelongToAndGroupsIOwnDescription
		{
			get
			{
				return new LocalizedString("GroupsIBelongToAndGroupsIOwnDescription", "", false, false, OwaOptionStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000357 RID: 855 RVA: 0x0000C7AD File Offset: 0x0000A9AD
		public static LocalizedString GetLocalizedString(OwaOptionStrings.IDs key)
		{
			return new LocalizedString(OwaOptionStrings.stringIDs[(uint)key], OwaOptionStrings.ResourceManager, new object[0]);
		}

		// Token: 0x04000001 RID: 1
		private static Dictionary<uint, string> stringIDs = new Dictionary<uint, string>(839);

		// Token: 0x04000002 RID: 2
		private static ExchangeResourceManager ResourceManager = ExchangeResourceManager.GetResourceManager("Microsoft.Exchange.Management.ControlPanel.OwaOptionStrings", typeof(OwaOptionStrings).GetTypeInfo().Assembly);

		// Token: 0x02000003 RID: 3
		public enum IDs : uint
		{
			// Token: 0x04000004 RID: 4
			NeverSyncText = 4145884488U,
			// Token: 0x04000005 RID: 5
			FromAddressContainsConditionFormat = 1481793251U,
			// Token: 0x04000006 RID: 6
			CalendarPublishingBasic = 2278445393U,
			// Token: 0x04000007 RID: 7
			ChangePhoneNumber = 1471007325U,
			// Token: 0x04000008 RID: 8
			TimeZoneNote = 566587615U,
			// Token: 0x04000009 RID: 9
			ShowWorkWeekAsCheckBoxText = 2785077264U,
			// Token: 0x0400000A RID: 10
			ViewInboxRule = 85271849U,
			// Token: 0x0400000B RID: 11
			DeviceMobileOperatorLabel = 3270571164U,
			// Token: 0x0400000C RID: 12
			FinishButtonText = 62320074U,
			// Token: 0x0400000D RID: 13
			UserNameMOSIDLabel = 3179829084U,
			// Token: 0x0400000E RID: 14
			ChangeMyMobilePhoneSettings = 3449770263U,
			// Token: 0x0400000F RID: 15
			RequirementsReadWriteMailboxDescription = 2657242021U,
			// Token: 0x04000010 RID: 16
			MessageTypeMatchesConditionFormat = 1654895332U,
			// Token: 0x04000011 RID: 17
			NewRoomCreationWarningText = 226228553U,
			// Token: 0x04000012 RID: 18
			InboxRuleMyNameInToBoxConditionFlyOutText = 932151145U,
			// Token: 0x04000013 RID: 19
			FirstSyncOnLabel = 2712198432U,
			// Token: 0x04000014 RID: 20
			DeleteGroupConfirmation = 2830841285U,
			// Token: 0x04000015 RID: 21
			PendingWipeCommandIssuedLabel = 2230814600U,
			// Token: 0x04000016 RID: 22
			OwnerChangedModerationReminder = 3090796481U,
			// Token: 0x04000017 RID: 23
			InboxRuleFromConditionPreCannedText = 495329060U,
			// Token: 0x04000018 RID: 24
			MaximumConflictInstances = 3366412874U,
			// Token: 0x04000019 RID: 25
			EmailSubscriptions = 2502183272U,
			// Token: 0x0400001A RID: 26
			InboxRuleFlaggedForActionConditionFlyOutText = 2174801140U,
			// Token: 0x0400001B RID: 27
			ViewRPTDurationLabel = 3159390523U,
			// Token: 0x0400001C RID: 28
			OnOffColumn = 926911138U,
			// Token: 0x0400001D RID: 29
			FromSubscriptionConditionFormat = 4270339173U,
			// Token: 0x0400001E RID: 30
			EmailComposeModeSeparateForm = 2476211802U,
			// Token: 0x0400001F RID: 31
			ReadingPaneSlab = 1739026864U,
			// Token: 0x04000020 RID: 32
			OOF = 77678270U,
			// Token: 0x04000021 RID: 33
			SearchResultsCaption = 1330353058U,
			// Token: 0x04000022 RID: 34
			SubjectLabel = 3130274824U,
			// Token: 0x04000023 RID: 35
			Minute = 2220842206U,
			// Token: 0x04000024 RID: 36
			SendAtColon = 753397160U,
			// Token: 0x04000025 RID: 37
			NewCommandText = 177845278U,
			// Token: 0x04000026 RID: 38
			RetentionTypeRequired = 4084881753U,
			// Token: 0x04000027 RID: 39
			UninstallExtensionsConfirmation = 1138078555U,
			// Token: 0x04000028 RID: 40
			InboxRuleHasAttachmentConditionFlyOutText = 3158885598U,
			// Token: 0x04000029 RID: 41
			MyselfEntFormat = 2285322212U,
			// Token: 0x0400002A RID: 42
			ConnectedAccounts = 3472797073U,
			// Token: 0x0400002B RID: 43
			GroupNotes = 1659711502U,
			// Token: 0x0400002C RID: 44
			Status = 1959773104U,
			// Token: 0x0400002D RID: 45
			TeamMailboxSyncStatusString = 2313850175U,
			// Token: 0x0400002E RID: 46
			AccountSecondaryNavigation = 993996681U,
			// Token: 0x0400002F RID: 47
			EmailSubscriptionSlabDescription = 2256263033U,
			// Token: 0x04000030 RID: 48
			TeamMailboxMailString = 3692628685U,
			// Token: 0x04000031 RID: 49
			TeamMailboxLifecycleStatusString2 = 166656410U,
			// Token: 0x04000032 RID: 50
			JunkEmailTrustedListDescription = 298572739U,
			// Token: 0x04000033 RID: 51
			SundayCheckBoxText = 3700740504U,
			// Token: 0x04000034 RID: 52
			ExtensionVersionTag = 3309799253U,
			// Token: 0x04000035 RID: 53
			MailTip = 2902697354U,
			// Token: 0x04000036 RID: 54
			CalendarPublishingRestricted = 861811732U,
			// Token: 0x04000037 RID: 55
			MailboxUsageUnavailable = 884476035U,
			// Token: 0x04000038 RID: 56
			Customize = 2108510325U,
			// Token: 0x04000039 RID: 57
			ModerationEnabled = 1115629481U,
			// Token: 0x0400003A RID: 58
			PreviewMarkAsReadBehaviorDelayed = 3758991955U,
			// Token: 0x0400003B RID: 59
			ShareInformation = 3807282937U,
			// Token: 0x0400003C RID: 60
			RetentionActionTypeArchive = 1441613828U,
			// Token: 0x0400003D RID: 61
			SetUpNotifications = 2977853317U,
			// Token: 0x0400003E RID: 62
			InboxRuleMoveToFolderActionFlyOutText = 2600878742U,
			// Token: 0x0400003F RID: 63
			JunkEmailContactsTrusted = 854412308U,
			// Token: 0x04000040 RID: 64
			TeamMailboxManagementString = 3522427619U,
			// Token: 0x04000041 RID: 65
			MessageTrackingTransferredEvent = 636192742U,
			// Token: 0x04000042 RID: 66
			SendToAllGalLessText = 2329485994U,
			// Token: 0x04000043 RID: 67
			CalendarReminderInstruction = 2574861626U,
			// Token: 0x04000044 RID: 68
			TotalMembers = 3667432429U,
			// Token: 0x04000045 RID: 69
			MailboxUsageUnlimitedText = 1262029551U,
			// Token: 0x04000046 RID: 70
			CalendarTroubleshootingLinkText = 394064455U,
			// Token: 0x04000047 RID: 71
			DisplayRecoveryPasswordCommandText = 2661286284U,
			// Token: 0x04000048 RID: 72
			VoicemailWizardStep4Description = 366963388U,
			// Token: 0x04000049 RID: 73
			IncomingSecurityLabel = 365990270U,
			// Token: 0x0400004A RID: 74
			InboxRuleForwardToActionText = 1133353571U,
			// Token: 0x0400004B RID: 75
			InboxRuleMyNameIsGroupText = 3807748391U,
			// Token: 0x0400004C RID: 76
			MailboxFolderDialogLabel = 3420690180U,
			// Token: 0x0400004D RID: 77
			ReturnToView = 21771688U,
			// Token: 0x0400004E RID: 78
			DeviceActiveSyncVersionLabel = 469603767U,
			// Token: 0x0400004F RID: 79
			InstallFromPrivateUrlCaption = 4036777603U,
			// Token: 0x04000050 RID: 80
			DeleteEmailSubscriptionConfirmation = 3877512413U,
			// Token: 0x04000051 RID: 81
			VoicemailWizardComplete = 3119904789U,
			// Token: 0x04000052 RID: 82
			InboxRuleMarkAsReadActionFlyOutText = 303742195U,
			// Token: 0x04000053 RID: 83
			RPTDay = 3738802218U,
			// Token: 0x04000054 RID: 84
			DeviceAccessStateSetByLabel = 2590635398U,
			// Token: 0x04000055 RID: 85
			ViewGroupDetails = 1799036564U,
			// Token: 0x04000056 RID: 86
			ToOnlyLabel = 2408048685U,
			// Token: 0x04000057 RID: 87
			SensitivityDialogTitle = 2384846465U,
			// Token: 0x04000058 RID: 88
			TeamMailboxLifecycleStatusString = 1200890284U,
			// Token: 0x04000059 RID: 89
			WednesdayCheckBoxText = 3028241800U,
			// Token: 0x0400005A RID: 90
			ExtensionRequirementsLabel = 2254792003U,
			// Token: 0x0400005B RID: 91
			AlwaysShowBcc = 2459846522U,
			// Token: 0x0400005C RID: 92
			ConflictPercentageAllowedErrorMessage = 1703184725U,
			// Token: 0x0400005D RID: 93
			JoinRestrictionApprovalRequiredDetails = 3413282040U,
			// Token: 0x0400005E RID: 94
			InboxRuleMarkImportanceActionText = 1917681062U,
			// Token: 0x0400005F RID: 95
			InboxRuleRecipientAddressContainsConditionFlyOutText = 1073903281U,
			// Token: 0x04000060 RID: 96
			Regional = 2228506517U,
			// Token: 0x04000061 RID: 97
			VoicemailWizardTestDoneText = 3644924947U,
			// Token: 0x04000062 RID: 98
			RemoveOldMeetingMessagesCheckBoxText = 2743720278U,
			// Token: 0x04000063 RID: 99
			InboxRuleBodyContainsConditionText = 63766217U,
			// Token: 0x04000064 RID: 100
			QLForward = 1435597588U,
			// Token: 0x04000065 RID: 101
			VoicemailPhoneNumberColon = 4103313421U,
			// Token: 0x04000066 RID: 102
			AddCommandText = 1416133447U,
			// Token: 0x04000067 RID: 103
			Voicemail = 3917579091U,
			// Token: 0x04000068 RID: 104
			StringArrayDialogTitle = 2321140182U,
			// Token: 0x04000069 RID: 105
			MailboxUsageUnitTB = 2696088269U,
			// Token: 0x0400006A RID: 106
			CalendarPublishingCopyLink = 1272238732U,
			// Token: 0x0400006B RID: 107
			TimeStyles = 1224467389U,
			// Token: 0x0400006C RID: 108
			RPTYear = 559979509U,
			// Token: 0x0400006D RID: 109
			VoicemailLearnMoreVideo = 2493642031U,
			// Token: 0x0400006E RID: 110
			InboxRuleHeaderContainsConditionFlyOutText = 2564457623U,
			// Token: 0x0400006F RID: 111
			InboxRuleHasClassificationConditionText = 1916260782U,
			// Token: 0x04000070 RID: 112
			ImportContactListPage1Caption = 1806836259U,
			// Token: 0x04000071 RID: 113
			VoicemailWizardStep2Description = 3804549298U,
			// Token: 0x04000072 RID: 114
			AddExtension = 1269143998U,
			// Token: 0x04000073 RID: 115
			WithinSizeRangeDialogTitle = 2355901091U,
			// Token: 0x04000074 RID: 116
			GetCalendarLogButtonText = 2269401639U,
			// Token: 0x04000075 RID: 117
			BlockDeviceConfirmMessage = 954238138U,
			// Token: 0x04000076 RID: 118
			CalendarPublishingRangeFrom = 1294877450U,
			// Token: 0x04000077 RID: 119
			EnterPasscodeStepMessage = 3777014377U,
			// Token: 0x04000078 RID: 120
			VoicemailCallFwdHavingTrouble = 3308373688U,
			// Token: 0x04000079 RID: 121
			StartForward = 3543278019U,
			// Token: 0x0400007A RID: 122
			VoicemailCallFwdStep2 = 2118875232U,
			// Token: 0x0400007B RID: 123
			JoinRestrictionOpenDetails = 1225604874U,
			// Token: 0x0400007C RID: 124
			IncomingSecurityNone = 2863884442U,
			// Token: 0x0400007D RID: 125
			InboxRuleMyNameNotInToBoxConditionText = 1827349577U,
			// Token: 0x0400007E RID: 126
			ChangePermissions = 2515135494U,
			// Token: 0x0400007F RID: 127
			InboxRuleCopyToFolderActionFlyOutText = 1807321962U,
			// Token: 0x04000080 RID: 128
			InboxRuleSubjectContainsConditionFlyOutText = 3118863998U,
			// Token: 0x04000081 RID: 129
			RequirementsRestrictedValue = 1238462568U,
			// Token: 0x04000082 RID: 130
			InboxRuleRedirectToActionText = 394662652U,
			// Token: 0x04000083 RID: 131
			ImportContactListPage1Step2 = 1472234713U,
			// Token: 0x04000084 RID: 132
			JunkEmailWatermarkText = 1087517319U,
			// Token: 0x04000085 RID: 133
			TextMessagingStatusPrefixStatus = 3803411495U,
			// Token: 0x04000086 RID: 134
			ShowHoursIn = 4182339999U,
			// Token: 0x04000087 RID: 135
			DefaultFormat = 3390931208U,
			// Token: 0x04000088 RID: 136
			SubscriptionDialogTitle = 2893153135U,
			// Token: 0x04000089 RID: 137
			NewItemNotificationEmailToast = 3581746357U,
			// Token: 0x0400008A RID: 138
			TeamMailboxTabUsersHelpString1 = 994337869U,
			// Token: 0x0400008B RID: 139
			SchedulingPermissionsSlab = 307942560U,
			// Token: 0x0400008C RID: 140
			ConversationSortOrderInstruction = 3141117137U,
			// Token: 0x0400008D RID: 141
			WipeDeviceCommandText = 3348884585U,
			// Token: 0x0400008E RID: 142
			InboxRuleSentOrReceivedGroupText = 757309800U,
			// Token: 0x0400008F RID: 143
			Myself = 1884883826U,
			// Token: 0x04000090 RID: 144
			NewestOnBottom = 885509158U,
			// Token: 0x04000091 RID: 145
			NewItemNotificationFaxToast = 4104125374U,
			// Token: 0x04000092 RID: 146
			EmailComposeModeInline = 4049797668U,
			// Token: 0x04000093 RID: 147
			NewRuleString = 880505963U,
			// Token: 0x04000094 RID: 148
			NoMessageCategoryAvailable = 2688608043U,
			// Token: 0x04000095 RID: 149
			CurrentStatus = 1328547437U,
			// Token: 0x04000096 RID: 150
			SubscriptionProcessingError = 1395647162U,
			// Token: 0x04000097 RID: 151
			StopAndRetrieveLogCommandText = 1075069807U,
			// Token: 0x04000098 RID: 152
			TimeIncrementThirtyMinutes = 1917268543U,
			// Token: 0x04000099 RID: 153
			RetentionActionNeverMove = 3253517897U,
			// Token: 0x0400009A RID: 154
			VoicemailMobileOperatorColon = 3600602092U,
			// Token: 0x0400009B RID: 155
			ConnectedAccountsDescriptionForForwarding = 2788873687U,
			// Token: 0x0400009C RID: 156
			StopForward = 2316526399U,
			// Token: 0x0400009D RID: 157
			FirstWeekOfYear = 3739699132U,
			// Token: 0x0400009E RID: 158
			RegionListLabel = 2986818840U,
			// Token: 0x0400009F RID: 159
			InstallFromMarketplace = 2982857632U,
			// Token: 0x040000A0 RID: 160
			RulesNameColumn = 738818108U,
			// Token: 0x040000A1 RID: 161
			DeviceOSLabel = 3166948736U,
			// Token: 0x040000A2 RID: 162
			InboxRuleSentOnlyToMeConditionText = 781854721U,
			// Token: 0x040000A3 RID: 163
			EditYourPassword = 364458648U,
			// Token: 0x040000A4 RID: 164
			EnforceSchedulingHorizon = 3609822921U,
			// Token: 0x040000A5 RID: 165
			TeamMailboxManagementString2 = 872104809U,
			// Token: 0x040000A6 RID: 166
			SearchMessageTipForIWUser = 3459526114U,
			// Token: 0x040000A7 RID: 167
			ConnectedAccountsDescriptionForSubscription = 904314811U,
			// Token: 0x040000A8 RID: 168
			QLManageOrganization = 3180823751U,
			// Token: 0x040000A9 RID: 169
			JoinRestrictionApprovalRequired = 2201160322U,
			// Token: 0x040000AA RID: 170
			ExtensionCanNotBeUninstalled = 3344174056U,
			// Token: 0x040000AB RID: 171
			VoicemailWizardStep4Title = 1952564584U,
			// Token: 0x040000AC RID: 172
			ViewExtensionDetails = 1001261206U,
			// Token: 0x040000AD RID: 173
			VoicemailCarrierRatesMayApply = 4181395631U,
			// Token: 0x040000AE RID: 174
			DeliveryReports = 2288925761U,
			// Token: 0x040000AF RID: 175
			AllRequestOutOfPolicyText = 3052389362U,
			// Token: 0x040000B0 RID: 176
			RemoveDeviceConfirmMessage = 3585825647U,
			// Token: 0x040000B1 RID: 177
			StatusLabel = 1401723706U,
			// Token: 0x040000B2 RID: 178
			InboxRuleSubjectOrBodyContainsConditionText = 3255907698U,
			// Token: 0x040000B3 RID: 179
			OwnerLabel = 1547367963U,
			// Token: 0x040000B4 RID: 180
			RequireSenderAuthFalse = 3925202351U,
			// Token: 0x040000B5 RID: 181
			AllowedSendersLabel = 2460807174U,
			// Token: 0x040000B6 RID: 182
			IncomingSecuritySSL = 759422978U,
			// Token: 0x040000B7 RID: 183
			CarrierListLabel = 952725660U,
			// Token: 0x040000B8 RID: 184
			InboxRuleDescriptionNote = 3525277106U,
			// Token: 0x040000B9 RID: 185
			NewImapSubscription = 3736448266U,
			// Token: 0x040000BA RID: 186
			TeamMailboxStartSyncButtonString = 3089967521U,
			// Token: 0x040000BB RID: 187
			NotificationsForCalendarUpdate = 2243312622U,
			// Token: 0x040000BC RID: 188
			ReadReceiptsSlab = 1134119669U,
			// Token: 0x040000BD RID: 189
			DetailsLinkText = 1550773931U,
			// Token: 0x040000BE RID: 190
			Help = 1454393937U,
			// Token: 0x040000BF RID: 191
			SearchGroups = 491491954U,
			// Token: 0x040000C0 RID: 192
			ShowConversationAsTreeInstruction = 3108971784U,
			// Token: 0x040000C1 RID: 193
			BypassModerationSenders = 3137025124U,
			// Token: 0x040000C2 RID: 194
			RetentionActionDeleteAndAllowRecovery = 3781814528U,
			// Token: 0x040000C3 RID: 195
			PreviewMarkAsReadDelaytimeTextPre = 999443141U,
			// Token: 0x040000C4 RID: 196
			RPTMonths = 565319905U,
			// Token: 0x040000C5 RID: 197
			AfterMoveOrDeleteBehavior = 1719300497U,
			// Token: 0x040000C6 RID: 198
			HideGroupFromAddressLists = 1359223188U,
			// Token: 0x040000C7 RID: 199
			VoicemailWizardStep1Description = 2825394221U,
			// Token: 0x040000C8 RID: 200
			ReviewLinkText = 2930172971U,
			// Token: 0x040000C9 RID: 201
			Processing = 2325389167U,
			// Token: 0x040000CA RID: 202
			DailyCalendarAgendas = 312564594U,
			// Token: 0x040000CB RID: 203
			PreviewMarkAsReadBehaviorOnSelectionChange = 1969050778U,
			// Token: 0x040000CC RID: 204
			TimeZoneLabelText = 1817328658U,
			// Token: 0x040000CD RID: 205
			QLVoiceMail = 3165477128U,
			// Token: 0x040000CE RID: 206
			VoicemailSignUpIntro = 1490355023U,
			// Token: 0x040000CF RID: 207
			VoicemailStep2 = 4294954307U,
			// Token: 0x040000D0 RID: 208
			TeamMailboxMembershipString = 519599668U,
			// Token: 0x040000D1 RID: 209
			PasscodeLabel = 3463577248U,
			// Token: 0x040000D2 RID: 210
			PersonalSettingPasswordAfterChange = 1836294011U,
			// Token: 0x040000D3 RID: 211
			VerificationSuccessPageTitle = 3090647821U,
			// Token: 0x040000D4 RID: 212
			EnableAutomaticProcessingNote = 843476083U,
			// Token: 0x040000D5 RID: 213
			Days = 2422328107U,
			// Token: 0x040000D6 RID: 214
			NotificationsNotSetUp = 521914504U,
			// Token: 0x040000D7 RID: 215
			ModerationNotificationsInternal = 2608069889U,
			// Token: 0x040000D8 RID: 216
			ProtocolSettings = 3040964019U,
			// Token: 0x040000D9 RID: 217
			EnableAutomaticProcessing = 4275718891U,
			// Token: 0x040000DA RID: 218
			MessageOptionsSlab = 2045071371U,
			// Token: 0x040000DB RID: 219
			ChooseMessageFont = 2789466833U,
			// Token: 0x040000DC RID: 220
			Password = 3563093647U,
			// Token: 0x040000DD RID: 221
			OWAExtensions = 1901616043U,
			// Token: 0x040000DE RID: 222
			StringArrayDialogLabel = 664237562U,
			// Token: 0x040000DF RID: 223
			Unlimited = 944449543U,
			// Token: 0x040000E0 RID: 224
			VoicemailSMSOptionVoiceMailOnly = 4033068416U,
			// Token: 0x040000E1 RID: 225
			Rules = 3853542865U,
			// Token: 0x040000E2 RID: 226
			ModeratedByEmptyDataText = 860120982U,
			// Token: 0x040000E3 RID: 227
			TextMessaging = 499094223U,
			// Token: 0x040000E4 RID: 228
			FromLabel = 2038251428U,
			// Token: 0x040000E5 RID: 229
			GroupModerators = 2108668357U,
			// Token: 0x040000E6 RID: 230
			ReadingPaneInstruction = 4089833856U,
			// Token: 0x040000E7 RID: 231
			RPTNone = 1749888754U,
			// Token: 0x040000E8 RID: 232
			Spelling = 2618236676U,
			// Token: 0x040000E9 RID: 233
			CancelWipeDeviceCommandText = 3198693159U,
			// Token: 0x040000EA RID: 234
			AutomaticRepliesEnabledText = 3747885709U,
			// Token: 0x040000EB RID: 235
			DisplayNameLabel = 2372820691U,
			// Token: 0x040000EC RID: 236
			CancelButtonText = 2449169153U,
			// Token: 0x040000ED RID: 237
			GroupMembershipApproval = 3156651890U,
			// Token: 0x040000EE RID: 238
			InlcudedRecipientTypesLabel = 3398667566U,
			// Token: 0x040000EF RID: 239
			Name = 2328219947U,
			// Token: 0x040000F0 RID: 240
			RetentionActionTypeDelete = 590111363U,
			// Token: 0x040000F1 RID: 241
			InboxRuleMyNameInCcBoxConditionFlyOutText = 3652102134U,
			// Token: 0x040000F2 RID: 242
			ThursdayCheckBoxText = 2328532194U,
			// Token: 0x040000F3 RID: 243
			JoinGroup = 258131565U,
			// Token: 0x040000F4 RID: 244
			Account = 767759945U,
			// Token: 0x040000F5 RID: 245
			InboxRuleMessageTypeMatchesConditionFlyOutText = 1107043549U,
			// Token: 0x040000F6 RID: 246
			InboxRuleMoveCopyDeleteGroupText = 1934149463U,
			// Token: 0x040000F7 RID: 247
			RegionalSettingsInstruction = 294641626U,
			// Token: 0x040000F8 RID: 248
			NameColumn = 1009337581U,
			// Token: 0x040000F9 RID: 249
			InboxRuleWithSensitivityConditionFlyOutText = 2921806668U,
			// Token: 0x040000FA RID: 250
			ClassificationDialogTitle = 744201260U,
			// Token: 0x040000FB RID: 251
			RuleFromAndMoveToFolderTemplate = 2012947571U,
			// Token: 0x040000FC RID: 252
			DomainNameNotSetError = 2112722880U,
			// Token: 0x040000FD RID: 253
			MakeSecurityGroup = 3382481163U,
			// Token: 0x040000FE RID: 254
			ContactNumbersBookmark = 111122248U,
			// Token: 0x040000FF RID: 255
			InboxRuleSentToConditionText = 3443085801U,
			// Token: 0x04000100 RID: 256
			MemberOfGroups = 1620775745U,
			// Token: 0x04000101 RID: 257
			InboxRuleIncludeTheseWordsGroupText = 2393490804U,
			// Token: 0x04000102 RID: 258
			MailTipLabel = 691241872U,
			// Token: 0x04000103 RID: 259
			MessageTypeDialogLabel = 1413097981U,
			// Token: 0x04000104 RID: 260
			RegionalSettingsSlab = 1541052658U,
			// Token: 0x04000105 RID: 261
			VoicemailWizardStep3Title = 346591353U,
			// Token: 0x04000106 RID: 262
			InboxRuleMyNameNotInToBoxConditionFlyOutText = 2731962334U,
			// Token: 0x04000107 RID: 263
			ReturnToOWA = 510973434U,
			// Token: 0x04000108 RID: 264
			InboxRuleMyNameInToBoxConditionText = 1564510770U,
			// Token: 0x04000109 RID: 265
			CommitButtonText = 3567818904U,
			// Token: 0x0400010A RID: 266
			TeamMailboxShowInMyClientString = 1864411629U,
			// Token: 0x0400010B RID: 267
			InboxRuleMarkAsReadActionText = 2397374352U,
			// Token: 0x0400010C RID: 268
			ClassificationDialogLabel = 663639812U,
			// Token: 0x0400010D RID: 269
			WarningAlt = 3313607735U,
			// Token: 0x0400010E RID: 270
			TeamMailboxManagementString4 = 2034904223U,
			// Token: 0x0400010F RID: 271
			Mail = 405905481U,
			// Token: 0x04000110 RID: 272
			ImportContactList = 624357391U,
			// Token: 0x04000111 RID: 273
			QLImportContacts = 1680686955U,
			// Token: 0x04000112 RID: 274
			InboxRule = 3391394280U,
			// Token: 0x04000113 RID: 275
			WithinDateRangeDialogTitle = 3979669380U,
			// Token: 0x04000114 RID: 276
			ReminderSoundEnabled = 1449143764U,
			// Token: 0x04000115 RID: 277
			RecipientAddressContainsConditionFormat = 1634623908U,
			// Token: 0x04000116 RID: 278
			MessageFormatPlainText = 481168139U,
			// Token: 0x04000117 RID: 279
			DeleteInboxRuleConfirmation = 203738408U,
			// Token: 0x04000118 RID: 280
			ForwardEmailTitle = 2879015231U,
			// Token: 0x04000119 RID: 281
			BypassModerationSendersEmptyDataText = 3395154598U,
			// Token: 0x0400011A RID: 282
			RuleSubjectContainsAndDeleteMessageTemplate = 396113514U,
			// Token: 0x0400011B RID: 283
			HideDeletedItems = 1053461607U,
			// Token: 0x0400011C RID: 284
			VoicemailSetupNowButtonText = 3137581701U,
			// Token: 0x0400011D RID: 285
			CalendarPermissions = 797589720U,
			// Token: 0x0400011E RID: 286
			ModerationNotificationsAlways = 1108157303U,
			// Token: 0x0400011F RID: 287
			ExternalMessageInstruction = 1038822924U,
			// Token: 0x04000120 RID: 288
			RequirementsReadItemValue = 2936411810U,
			// Token: 0x04000121 RID: 289
			SchedulingOptionsSlab = 4225166530U,
			// Token: 0x04000122 RID: 290
			ShowConversationTree = 742094856U,
			// Token: 0x04000123 RID: 291
			RetentionPolicies = 1303277130U,
			// Token: 0x04000124 RID: 292
			CalendarPublishingSubscriptionUrl = 2271033387U,
			// Token: 0x04000125 RID: 293
			ResendVerificationEmailCommandText = 1808002020U,
			// Token: 0x04000126 RID: 294
			TextMessagingNotification = 2949887068U,
			// Token: 0x04000127 RID: 295
			InstalledByColumn = 3246855305U,
			// Token: 0x04000128 RID: 296
			GroupOrganizationalUnit = 3100669839U,
			// Token: 0x04000129 RID: 297
			MailboxFolderDialogTitle = 1750301704U,
			// Token: 0x0400012A RID: 298
			PersonalSettingOldPassword = 2669253750U,
			// Token: 0x0400012B RID: 299
			VoicemailStep3 = 2728870366U,
			// Token: 0x0400012C RID: 300
			CityLabel = 3140976517U,
			// Token: 0x0400012D RID: 301
			SentToConditionFormat = 1022114031U,
			// Token: 0x0400012E RID: 302
			QLSubscription = 260433958U,
			// Token: 0x0400012F RID: 303
			ViewRPTDetailsTitle = 803108465U,
			// Token: 0x04000130 RID: 304
			MyGroups = 3201916728U,
			// Token: 0x04000131 RID: 305
			TeamMailboxesString = 349837376U,
			// Token: 0x04000132 RID: 306
			DeliveryReport = 1220880656U,
			// Token: 0x04000133 RID: 307
			LastNameLabel = 3294022635U,
			// Token: 0x04000134 RID: 308
			CalendarPublishingStop = 813081783U,
			// Token: 0x04000135 RID: 309
			VoicemailWizardStep3Description = 3339583031U,
			// Token: 0x04000136 RID: 310
			QLWhatsNewForOrganizations = 2176445071U,
			// Token: 0x04000137 RID: 311
			ReadReceiptResponseAlways = 3853933336U,
			// Token: 0x04000138 RID: 312
			JunkEmailTrustedListsOnly = 3555736038U,
			// Token: 0x04000139 RID: 313
			MatchSortOrder = 2391157043U,
			// Token: 0x0400013A RID: 314
			DevicePhoneNumberLabel = 2421998101U,
			// Token: 0x0400013B RID: 315
			InboxRuleMessageTypeMatchesConditionText = 3176989690U,
			// Token: 0x0400013C RID: 316
			RetentionTypeOptional = 4266991160U,
			// Token: 0x0400013D RID: 317
			UseSettings = 3996928596U,
			// Token: 0x0400013E RID: 318
			SearchAllGroups = 1958633523U,
			// Token: 0x0400013F RID: 319
			MembersLabel = 2117869041U,
			// Token: 0x04000140 RID: 320
			FreeBusyInformation = 1063608921U,
			// Token: 0x04000141 RID: 321
			DeviceIMEILabel = 133785142U,
			// Token: 0x04000142 RID: 322
			Day = 696030412U,
			// Token: 0x04000143 RID: 323
			InboxRuleMoveToFolderActionText = 192786603U,
			// Token: 0x04000144 RID: 324
			SelectOne = 779120846U,
			// Token: 0x04000145 RID: 325
			InboxRuleApplyCategoryActionFlyOutText = 846466190U,
			// Token: 0x04000146 RID: 326
			SetupEmailNotificationsLink = 3990783281U,
			// Token: 0x04000147 RID: 327
			NewDistributionGroupCaption = 117083423U,
			// Token: 0x04000148 RID: 328
			ViewRPTRetentionActionLabel = 1217964679U,
			// Token: 0x04000149 RID: 329
			ShowWeekNumbersCheckBoxText = 2135021465U,
			// Token: 0x0400014A RID: 330
			UserNameWLIDLabel = 1136770150U,
			// Token: 0x0400014B RID: 331
			SearchMessagesIReceivedLabel = 2028785684U,
			// Token: 0x0400014C RID: 332
			InboxRuleWithinDateRangeConditionText = 2151247442U,
			// Token: 0x0400014D RID: 333
			InboxRuleSendTextMessageNotificationToActionText = 839503915U,
			// Token: 0x0400014E RID: 334
			SentLabel = 3985530922U,
			// Token: 0x0400014F RID: 335
			GroupInformation = 2324730983U,
			// Token: 0x04000150 RID: 336
			VoicemailAskOperator = 2601154324U,
			// Token: 0x04000151 RID: 337
			OWA = 4016415205U,
			// Token: 0x04000152 RID: 338
			MailTipWaterMark = 574892128U,
			// Token: 0x04000153 RID: 339
			InboxRuleWithSensitivityConditionText = 969182653U,
			// Token: 0x04000154 RID: 340
			RetentionActionTypeDefaultArchive = 1090313663U,
			// Token: 0x04000155 RID: 341
			RemoveOptionalRPTConfirmation = 454922625U,
			// Token: 0x04000156 RID: 342
			DevicePolicyUpdateTimeLabel = 1570694400U,
			// Token: 0x04000157 RID: 343
			MyselfLiveFormat = 1674956465U,
			// Token: 0x04000158 RID: 344
			EmailDomain = 2143618034U,
			// Token: 0x04000159 RID: 345
			RequireSenderAuthTrue = 148346506U,
			// Token: 0x0400015A RID: 346
			InstallFromPrivateUrlInformation = 774036985U,
			// Token: 0x0400015B RID: 347
			PhoneLabel = 446237888U,
			// Token: 0x0400015C RID: 348
			SubscriptionAction = 731708393U,
			// Token: 0x0400015D RID: 349
			Weeks = 2401508539U,
			// Token: 0x0400015E RID: 350
			InboxRuleForwardRedirectGroupText = 3825281971U,
			// Token: 0x0400015F RID: 351
			DisplayName = 3719582777U,
			// Token: 0x04000160 RID: 352
			MembershipApprovalLabel = 3507262891U,
			// Token: 0x04000161 RID: 353
			InboxRuleSendTextMessageNotificationToActionFlyOutText = 1542072756U,
			// Token: 0x04000162 RID: 354
			TeamMailboxSPSiteString = 3475829388U,
			// Token: 0x04000163 RID: 355
			InboxRuleSubjectOrBodyContainsConditionFlyOutText = 1559211265U,
			// Token: 0x04000164 RID: 356
			PleaseWait = 1690161621U,
			// Token: 0x04000165 RID: 357
			JoinRestrictionOpen = 258867536U,
			// Token: 0x04000166 RID: 358
			CalendarSharingConfirmDeletionSingle = 418352798U,
			// Token: 0x04000167 RID: 359
			InboxRuleSubjectContainsConditionPreCannedText = 1869505729U,
			// Token: 0x04000168 RID: 360
			CalendarPublishingStart = 2815967975U,
			// Token: 0x04000169 RID: 361
			TextMessagingSlabMessageNotificationOnly = 1632853873U,
			// Token: 0x0400016A RID: 362
			RequirementsRestrictedDescription = 3304439859U,
			// Token: 0x0400016B RID: 363
			SelectAUser = 4114414654U,
			// Token: 0x0400016C RID: 364
			NotificationStepOneMessage = 3042824760U,
			// Token: 0x0400016D RID: 365
			QLPushEmail = 2897641359U,
			// Token: 0x0400016E RID: 366
			NewInboxRuleTitle = 1890566340U,
			// Token: 0x0400016F RID: 367
			SendToKnownContactsText = 929330042U,
			// Token: 0x04000170 RID: 368
			IncomingAuthenticationSpa = 48495520U,
			// Token: 0x04000171 RID: 369
			MailboxUsageUnitGB = 2696088282U,
			// Token: 0x04000172 RID: 370
			MessageTrackingDeliveredEvent = 3651870140U,
			// Token: 0x04000173 RID: 371
			SelectOneOrMoreText = 3772909321U,
			// Token: 0x04000174 RID: 372
			InboxRuleForwardAsAttachmentToActionText = 2600859168U,
			// Token: 0x04000175 RID: 373
			DontSeeMyRegionOrMobileOperator = 1105583403U,
			// Token: 0x04000176 RID: 374
			PreviewMarkAsReadDelaytimeTextPost = 1436160346U,
			// Token: 0x04000177 RID: 375
			NoMessageClassificationAvailable = 2060430325U,
			// Token: 0x04000178 RID: 376
			TeamMailboxTabPropertiesHelpString = 3929633211U,
			// Token: 0x04000179 RID: 377
			TeamMailboxManagementString1 = 2438188750U,
			// Token: 0x0400017A RID: 378
			RetentionPeriodHeader = 4047036620U,
			// Token: 0x0400017B RID: 379
			Phone = 4263477646U,
			// Token: 0x0400017C RID: 380
			WhoHasPermission = 2862314013U,
			// Token: 0x0400017D RID: 381
			NotAvailable = 1766818386U,
			// Token: 0x0400017E RID: 382
			FlaggedForActionConditionFormat = 1253897847U,
			// Token: 0x0400017F RID: 383
			EndTimeText = 4028994607U,
			// Token: 0x04000180 RID: 384
			BookingWindowInDays = 234118977U,
			// Token: 0x04000181 RID: 385
			RemovingPreviewPhoto = 3253425509U,
			// Token: 0x04000182 RID: 386
			CalendarDiagnosticLogSlab = 1666931485U,
			// Token: 0x04000183 RID: 387
			ModerationNotification = 3454695329U,
			// Token: 0x04000184 RID: 388
			VoicemailWizardPinlessOptionsText = 1207804035U,
			// Token: 0x04000185 RID: 389
			VoicemailClearSettings = 2476883967U,
			// Token: 0x04000186 RID: 390
			PersonalGroups = 1094425034U,
			// Token: 0x04000187 RID: 391
			DistributionGroupText = 259319032U,
			// Token: 0x04000188 RID: 392
			ProfileContactNumbers = 1335943323U,
			// Token: 0x04000189 RID: 393
			DeliveryReportFor = 167657761U,
			// Token: 0x0400018A RID: 394
			InboxRuleRedirectToActionFlyOutText = 232975871U,
			// Token: 0x0400018B RID: 395
			AutomateProcessing = 2251801313U,
			// Token: 0x0400018C RID: 396
			CalendarPublishingAccessLevel = 3493646365U,
			// Token: 0x0400018D RID: 397
			ForwardEmailTo = 248447020U,
			// Token: 0x0400018E RID: 398
			SettingUpProtocolAccess = 2062923077U,
			// Token: 0x0400018F RID: 399
			AdminTools = 1732697476U,
			// Token: 0x04000190 RID: 400
			InstalledExtensionDescription = 1669803655U,
			// Token: 0x04000191 RID: 401
			EmailComposeModeInstruction = 568490923U,
			// Token: 0x04000192 RID: 402
			InboxRuleDeleteMessageActionText = 3271192817U,
			// Token: 0x04000193 RID: 403
			SummaryToDate = 442231679U,
			// Token: 0x04000194 RID: 404
			Extension = 2631270417U,
			// Token: 0x04000195 RID: 405
			CalendarSharingConfirmDeletionMultiple = 1549389490U,
			// Token: 0x04000196 RID: 406
			Depart = 1314739276U,
			// Token: 0x04000197 RID: 407
			EmailNotificationsLink = 2193710474U,
			// Token: 0x04000198 RID: 408
			OpenPreviousItem = 1192767596U,
			// Token: 0x04000199 RID: 409
			RPTPickerDialogTitle = 1886990352U,
			// Token: 0x0400019A RID: 410
			CheckForForgottenAttachments = 625886851U,
			// Token: 0x0400019B RID: 411
			FaxLabel = 2755319165U,
			// Token: 0x0400019C RID: 412
			SentTime = 2677919833U,
			// Token: 0x0400019D RID: 413
			DeviceTypeHeaderText = 673404970U,
			// Token: 0x0400019E RID: 414
			RPTExpireNever = 338457679U,
			// Token: 0x0400019F RID: 415
			TeamMailboxEmailAddressString = 779255172U,
			// Token: 0x040001A0 RID: 416
			InboxRuleWithinSizeRangeConditionText = 530176925U,
			// Token: 0x040001A1 RID: 417
			Week = 2213908942U,
			// Token: 0x040001A2 RID: 418
			WithinDateRangeConditionFormat = 2185035188U,
			// Token: 0x040001A3 RID: 419
			DisplayNameNotSetError = 2502896170U,
			// Token: 0x040001A4 RID: 420
			FirstDayOfWeek = 198361899U,
			// Token: 0x040001A5 RID: 421
			ProcessExternalMeetingMessagesCheckBoxText = 2556260573U,
			// Token: 0x040001A6 RID: 422
			DepartRestrictionClosed = 2041861674U,
			// Token: 0x040001A7 RID: 423
			MailTipShortLabel = 886092638U,
			// Token: 0x040001A8 RID: 424
			StateOrProvinceLabel = 2483718204U,
			// Token: 0x040001A9 RID: 425
			PrimaryEmailAddress = 67466988U,
			// Token: 0x040001AA RID: 426
			AllowedSendersEmptyLabel = 2578200319U,
			// Token: 0x040001AB RID: 427
			HotmailSubscription = 1878748957U,
			// Token: 0x040001AC RID: 428
			DeviceIDLabel = 2638705563U,
			// Token: 0x040001AD RID: 429
			AlwaysShowFrom = 1891390470U,
			// Token: 0x040001AE RID: 430
			SearchButtonText = 1795719989U,
			// Token: 0x040001AF RID: 431
			ViewRPTDescriptionLabel = 4001240631U,
			// Token: 0x040001B0 RID: 432
			Hour = 2624099920U,
			// Token: 0x040001B1 RID: 433
			FlagStatusDialogTitle = 3567033964U,
			// Token: 0x040001B2 RID: 434
			NewSubscriptionProgress = 1463926066U,
			// Token: 0x040001B3 RID: 435
			EditProfilePhoto = 2455977975U,
			// Token: 0x040001B4 RID: 436
			InstallFromPrivateUrl = 614024525U,
			// Token: 0x040001B5 RID: 437
			DeleteGroupsConfirmation = 2869098816U,
			// Token: 0x040001B6 RID: 438
			OwnedGroups = 870015935U,
			// Token: 0x040001B7 RID: 439
			NewDistributionGroupTitle = 2322301053U,
			// Token: 0x040001B8 RID: 440
			JunkEmailConfiguration = 2562827638U,
			// Token: 0x040001B9 RID: 441
			ProfileGeneral = 3873537801U,
			// Token: 0x040001BA RID: 442
			CalendarSharingOutlookNote = 2344432611U,
			// Token: 0x040001BB RID: 443
			RemoveOptionalRPTsConfirmation = 829380600U,
			// Token: 0x040001BC RID: 444
			VerificationSuccessTitle = 2472855514U,
			// Token: 0x040001BD RID: 445
			ResponseMessageSlab = 3585330084U,
			// Token: 0x040001BE RID: 446
			TeamMailboxMaintenanceString = 1695816201U,
			// Token: 0x040001BF RID: 447
			UrlLabelText = 1565811752U,
			// Token: 0x040001C0 RID: 448
			DepartRestrictionOpen = 3765209046U,
			// Token: 0x040001C1 RID: 449
			PendingWipeCommandSentLabel = 2239331369U,
			// Token: 0x040001C2 RID: 450
			SubjectOrBodyContainsConditionFormat = 2494867144U,
			// Token: 0x040001C3 RID: 451
			LeaveMailOnServerLabel = 1747388690U,
			// Token: 0x040001C4 RID: 452
			JoinRestrictionClosed = 2054798096U,
			// Token: 0x040001C5 RID: 453
			ScheduleOnlyDuringWorkHours = 46775972U,
			// Token: 0x040001C6 RID: 454
			ImportanceDialogLabel = 614963610U,
			// Token: 0x040001C7 RID: 455
			CalendarPublishingLinkNotes = 3410351290U,
			// Token: 0x040001C8 RID: 456
			InboxRuleSentOnlyToMeConditionFlyOutText = 1356158868U,
			// Token: 0x040001C9 RID: 457
			InternalMessageInstruction = 2409360890U,
			// Token: 0x040001CA RID: 458
			JunkEmailDisabled = 1192855764U,
			// Token: 0x040001CB RID: 459
			InboxRuleForwardAsAttachmentToActionFlyOutText = 624477965U,
			// Token: 0x040001CC RID: 460
			WarningNote = 2343908218U,
			// Token: 0x040001CD RID: 461
			HomePagePrimaryLink = 4104737160U,
			// Token: 0x040001CE RID: 462
			LimitDuration = 837698603U,
			// Token: 0x040001CF RID: 463
			MailboxUsageExceededText = 3506885255U,
			// Token: 0x040001D0 RID: 464
			UnSupportedRule = 3647997407U,
			// Token: 0x040001D1 RID: 465
			DeviceModelLabel = 37810473U,
			// Token: 0x040001D2 RID: 466
			NotificationLinksMessage = 2737072517U,
			// Token: 0x040001D3 RID: 467
			ResourceSlab = 2702941902U,
			// Token: 0x040001D4 RID: 468
			RetentionPolicyTagPageTitle = 4137250117U,
			// Token: 0x040001D5 RID: 469
			AllBookInPolicyText = 1485698978U,
			// Token: 0x040001D6 RID: 470
			MessageFormatHtml = 2179002981U,
			// Token: 0x040001D7 RID: 471
			FridayCheckBoxText = 755136567U,
			// Token: 0x040001D8 RID: 472
			InboxRuleMyNameInCcBoxConditionText = 3099627267U,
			// Token: 0x040001D9 RID: 473
			PreviewMarkAsReadDelaytimeErrorMessage = 2592606558U,
			// Token: 0x040001DA RID: 474
			JunkEmailValidationErrorMessage = 3576971014U,
			// Token: 0x040001DB RID: 475
			TeamMailboxTabUsersHelpString2 = 3723221224U,
			// Token: 0x040001DC RID: 476
			AllRequestInPolicyText = 856468152U,
			// Token: 0x040001DD RID: 477
			ImapSubscription = 3383857716U,
			// Token: 0x040001DE RID: 478
			InboxRuleWithImportanceConditionFlyOutText = 2201998933U,
			// Token: 0x040001DF RID: 479
			TeamMailboxDisplayNameString = 600392665U,
			// Token: 0x040001E0 RID: 480
			TeamMailboxManagementString3 = 3600988164U,
			// Token: 0x040001E1 RID: 481
			ConflictPercentageAllowed = 1735921300U,
			// Token: 0x040001E2 RID: 482
			ImportanceDialogTitle = 695525694U,
			// Token: 0x040001E3 RID: 483
			SecurityGroupText = 265625760U,
			// Token: 0x040001E4 RID: 484
			SubjectContainsConditionFormat = 1528543719U,
			// Token: 0x040001E5 RID: 485
			VoicemailStep1 = 1566070952U,
			// Token: 0x040001E6 RID: 486
			ImportContactListPage1Step1 = 1472234710U,
			// Token: 0x040001E7 RID: 487
			CalendarPublishingRangeTo = 2919653011U,
			// Token: 0x040001E8 RID: 488
			Installed = 924528516U,
			// Token: 0x040001E9 RID: 489
			JoinRestrictionClosedDetails = 2532105814U,
			// Token: 0x040001EA RID: 490
			EnterNumberStepMessage = 3686719114U,
			// Token: 0x040001EB RID: 491
			TeamMailboxString = 3769321652U,
			// Token: 0x040001EC RID: 492
			ExternalAudienceCheckboxText = 1116185611U,
			// Token: 0x040001ED RID: 493
			RetentionActionNeverDelete = 3758567081U,
			// Token: 0x040001EE RID: 494
			TeamMailboxOwnersString = 3601061204U,
			// Token: 0x040001EF RID: 495
			SentOnlyToMeConditionFormat = 2454510675U,
			// Token: 0x040001F0 RID: 496
			PostalCodeLabel = 685553614U,
			// Token: 0x040001F1 RID: 497
			StreetAddressLabel = 2399940313U,
			// Token: 0x040001F2 RID: 498
			ModerationNotificationsNever = 1949934660U,
			// Token: 0x040001F3 RID: 499
			AutoAddSignature = 3043881226U,
			// Token: 0x040001F4 RID: 500
			VoicemailConfiguration = 362303575U,
			// Token: 0x040001F5 RID: 501
			TeamMailboxUsersString = 1231202734U,
			// Token: 0x040001F6 RID: 502
			Minutes = 4116169389U,
			// Token: 0x040001F7 RID: 503
			TextMessagingStatusPrefixNotifications = 1475482257U,
			// Token: 0x040001F8 RID: 504
			OfficeLabel = 947054436U,
			// Token: 0x040001F9 RID: 505
			SetupCalendarNotificationsLink = 3570705387U,
			// Token: 0x040001FA RID: 506
			WipeDeviceConfirmMessage = 2829325136U,
			// Token: 0x040001FB RID: 507
			JunkEmailEnabled = 1970261471U,
			// Token: 0x040001FC RID: 508
			ProfilePhoto = 2091217323U,
			// Token: 0x040001FD RID: 509
			JoinRestriction = 1213316404U,
			// Token: 0x040001FE RID: 510
			SubscriptionAccountInformation = 1603898988U,
			// Token: 0x040001FF RID: 511
			PopSubscription = 1983970360U,
			// Token: 0x04000200 RID: 512
			ConnectedAccountsDescriptionForSendAs = 1651913438U,
			// Token: 0x04000201 RID: 513
			VoicemailNotConfiguredText = 1355905147U,
			// Token: 0x04000202 RID: 514
			Date = 2328219770U,
			// Token: 0x04000203 RID: 515
			SubscriptionEmailAddress = 2118902877U,
			// Token: 0x04000204 RID: 516
			CalendarAppearanceInstruction = 1262918758U,
			// Token: 0x04000205 RID: 517
			DisableReminders = 779604653U,
			// Token: 0x04000206 RID: 518
			UninstallExtensionConfirmation = 58190828U,
			// Token: 0x04000207 RID: 519
			ReadReceiptResponseAskBefore = 272167933U,
			// Token: 0x04000208 RID: 520
			AutomaticRepliesDisabledText = 2721707952U,
			// Token: 0x04000209 RID: 521
			PermissionGranted = 2824009732U,
			// Token: 0x0400020A RID: 522
			CalendarTroubleShootingSlab = 473555910U,
			// Token: 0x0400020B RID: 523
			QLRemotePowerShell = 3318664764U,
			// Token: 0x0400020C RID: 524
			Ownership = 3266331791U,
			// Token: 0x0400020D RID: 525
			DevicePhoneNumberHeaderText = 1330376901U,
			// Token: 0x0400020E RID: 526
			OwnerApprovalRequired = 1467122827U,
			// Token: 0x0400020F RID: 527
			AddExtensionTitle = 936255600U,
			// Token: 0x04000210 RID: 528
			DevicePolicyApplicationStatusLabel = 2172431100U,
			// Token: 0x04000211 RID: 529
			CalendarWorkflowSlab = 3387549369U,
			// Token: 0x04000212 RID: 530
			SettingNotAvailable = 1249796258U,
			// Token: 0x04000213 RID: 531
			DepartRestriction = 1244143874U,
			// Token: 0x04000214 RID: 532
			RetentionTypeRequiredDescription = 3089991175U,
			// Token: 0x04000215 RID: 533
			RemoveForwardedMeetingNotificationsCheckBoxText = 4116953573U,
			// Token: 0x04000216 RID: 534
			Everyone = 3708929833U,
			// Token: 0x04000217 RID: 535
			TeamMailboxAppTitle = 3452175682U,
			// Token: 0x04000218 RID: 536
			PersonalSettingConfirmPassword = 2525011123U,
			// Token: 0x04000219 RID: 537
			MailboxUsageUnitMB = 2696088292U,
			// Token: 0x0400021A RID: 538
			SubscriptionServerInformation = 4155090378U,
			// Token: 0x0400021B RID: 539
			UserNameLabel = 2385969192U,
			// Token: 0x0400021C RID: 540
			TimeIncrementFifteenMinutes = 776327314U,
			// Token: 0x0400021D RID: 541
			LastSuccessfulSync = 2051542189U,
			// Token: 0x0400021E RID: 542
			SchedulingPermissionsInstruction = 4052999952U,
			// Token: 0x0400021F RID: 543
			EmptyDeletedItemsOnLogoff = 1517497942U,
			// Token: 0x04000220 RID: 544
			BeforeDateDisplayTemplate = 2251065475U,
			// Token: 0x04000221 RID: 545
			EmailAddressLabel = 2121350552U,
			// Token: 0x04000222 RID: 546
			JunkEmailBlockedListDescription = 1895146354U,
			// Token: 0x04000223 RID: 547
			NewItemNotificationSound = 3334007327U,
			// Token: 0x04000224 RID: 548
			NewInboxRuleCaption = 272267124U,
			// Token: 0x04000225 RID: 549
			UpdateTimeZoneNoteLinkText = 1462746207U,
			// Token: 0x04000226 RID: 550
			HasClassificationConditionFormat = 5631064U,
			// Token: 0x04000227 RID: 551
			NoneAccessRightRole = 705899704U,
			// Token: 0x04000228 RID: 552
			MobileDeviceDetailTitle = 3116350389U,
			// Token: 0x04000229 RID: 553
			EditCommandText = 624738140U,
			// Token: 0x0400022A RID: 554
			DeviceTypeLabel = 3623666006U,
			// Token: 0x0400022B RID: 555
			AllowConflicts = 4012923742U,
			// Token: 0x0400022C RID: 556
			CalendarPublishing = 3458491959U,
			// Token: 0x0400022D RID: 557
			RetentionNameHeader = 2717011154U,
			// Token: 0x0400022E RID: 558
			ImportContactListPage1InformationText = 1036626U,
			// Token: 0x0400022F RID: 559
			VoicemailAskPhoneNumber = 3055482209U,
			// Token: 0x04000230 RID: 560
			TeamMailboxDocumentsString = 453080500U,
			// Token: 0x04000231 RID: 561
			CountryOrRegionLabel = 3751262095U,
			// Token: 0x04000232 RID: 562
			CalendarPublishingLearnMore = 1816995256U,
			// Token: 0x04000233 RID: 563
			GroupModeratedBy = 1989730689U,
			// Token: 0x04000234 RID: 564
			DidntReceivePasscodeMessage = 4168470247U,
			// Token: 0x04000235 RID: 565
			InboxRuleFromAddressContainsConditionText = 3197162109U,
			// Token: 0x04000236 RID: 566
			IncomingAuthenticationLabel = 1889609004U,
			// Token: 0x04000237 RID: 567
			InboxRuleRecipientAddressContainsConditionText = 2280348446U,
			// Token: 0x04000238 RID: 568
			DepartGroupsConfirmation = 1975661589U,
			// Token: 0x04000239 RID: 569
			CalendarAppearanceSlab = 2084354126U,
			// Token: 0x0400023A RID: 570
			InitialsLabel = 2908173103U,
			// Token: 0x0400023B RID: 571
			InboxRuleFlaggedForActionConditionText = 3446713793U,
			// Token: 0x0400023C RID: 572
			QuickLinks = 3191690622U,
			// Token: 0x0400023D RID: 573
			TeamMailboxMyRoleString2 = 3789018700U,
			// Token: 0x0400023E RID: 574
			RoomEmailAddressLabel = 2359136379U,
			// Token: 0x0400023F RID: 575
			ToColumnLabel = 1233816147U,
			// Token: 0x04000240 RID: 576
			SensitivityDialogLabel = 1628295023U,
			// Token: 0x04000241 RID: 577
			AllowedSendersEmptyLabelForEndUser = 3728295286U,
			// Token: 0x04000242 RID: 578
			RequirementsReadWriteMailboxValue = 3699558192U,
			// Token: 0x04000243 RID: 579
			FlagStatusDialogLabel = 2323655060U,
			// Token: 0x04000244 RID: 580
			ImapSetting = 993324455U,
			// Token: 0x04000245 RID: 581
			VoicemailConfiguredText = 1286082276U,
			// Token: 0x04000246 RID: 582
			JunkEmailTrustedListHeader = 517504798U,
			// Token: 0x04000247 RID: 583
			RequirementsReadItemDescription = 1255618141U,
			// Token: 0x04000248 RID: 584
			RequireSenderAuth = 311875894U,
			// Token: 0x04000249 RID: 585
			IWantToEditMyNotificationSettings = 2656242830U,
			// Token: 0x0400024A RID: 586
			DevicePolicyAppliedLabel = 4223818683U,
			// Token: 0x0400024B RID: 587
			VoicemailSMSOptionNone = 980863707U,
			// Token: 0x0400024C RID: 588
			EditDistributionGroupTitle = 4218310877U,
			// Token: 0x0400024D RID: 589
			MaximumDurationInMinutes = 1540907116U,
			// Token: 0x0400024E RID: 590
			NewPopSubscription = 3482669506U,
			// Token: 0x0400024F RID: 591
			MobileDeviceHeadNoteInfo = 3896686788U,
			// Token: 0x04000250 RID: 592
			HomePhoneLabel = 4151878805U,
			// Token: 0x04000251 RID: 593
			BlockDeviceCommandText = 2680900463U,
			// Token: 0x04000252 RID: 594
			SelectUsers = 572559498U,
			// Token: 0x04000253 RID: 595
			SearchGroupsButtonDescription = 3849562096U,
			// Token: 0x04000254 RID: 596
			DeleteEmailSubscriptionsConfirmation = 382247602U,
			// Token: 0x04000255 RID: 597
			LearnHowToUseRedirectTo = 844022153U,
			// Token: 0x04000256 RID: 598
			InboxRuleDeleteMessageActionFlyOutText = 1008265668U,
			// Token: 0x04000257 RID: 599
			RetentionExplanationLabel = 187395633U,
			// Token: 0x04000258 RID: 600
			MessageTrackingPendingEvent = 2136591767U,
			// Token: 0x04000259 RID: 601
			ProviderColumn = 773553315U,
			// Token: 0x0400025A RID: 602
			SettingAccessDisabled = 718036160U,
			// Token: 0x0400025B RID: 603
			RPTDays = 1339697241U,
			// Token: 0x0400025C RID: 604
			InboxRuleFromSubscriptionConditionFlyOutText = 433722170U,
			// Token: 0x0400025D RID: 605
			SmtpAddressExample = 2080149828U,
			// Token: 0x0400025E RID: 606
			InboxRuleMarkImportanceActionFlyOutText = 759967779U,
			// Token: 0x0400025F RID: 607
			CategoryDialogLabel = 3100350500U,
			// Token: 0x04000260 RID: 608
			DeviceAccessStateLabel = 3531743005U,
			// Token: 0x04000261 RID: 609
			JunkEmailBlockedListHeader = 854461327U,
			// Token: 0x04000262 RID: 610
			VoicemailNotAvailableText = 3007171092U,
			// Token: 0x04000263 RID: 611
			ContactLocationBookmark = 290121153U,
			// Token: 0x04000264 RID: 612
			InboxRuleSubjectContainsConditionText = 1437070541U,
			// Token: 0x04000265 RID: 613
			VoicemailWizardConfirmPinLabel = 130154415U,
			// Token: 0x04000266 RID: 614
			NoSubscriptionAvailable = 1880859719U,
			// Token: 0x04000267 RID: 615
			QLPassword = 2399040354U,
			// Token: 0x04000268 RID: 616
			CustomAccessRightRole = 540558863U,
			// Token: 0x04000269 RID: 617
			ClearSettings = 570023042U,
			// Token: 0x0400026A RID: 618
			MailboxUsageUnitKB = 2696088286U,
			// Token: 0x0400026B RID: 619
			ExtensionPackageLocation = 3947650662U,
			// Token: 0x0400026C RID: 620
			InboxRuleApplyCategoryActionText = 3084881239U,
			// Token: 0x0400026D RID: 621
			OWAVoicemail = 2226605436U,
			// Token: 0x0400026E RID: 622
			PersonalSettingPassword = 853755201U,
			// Token: 0x0400026F RID: 623
			SendDuringWorkHoursOnly = 726021877U,
			// Token: 0x04000270 RID: 624
			AliasLabelForDataCenter = 677776990U,
			// Token: 0x04000271 RID: 625
			DeliverAndForward = 1351408741U,
			// Token: 0x04000272 RID: 626
			Language = 468777496U,
			// Token: 0x04000273 RID: 627
			NameAndAccountBookmark = 3918450461U,
			// Token: 0x04000274 RID: 628
			SendMyPhoneColon = 2977885649U,
			// Token: 0x04000275 RID: 629
			DateStyles = 2956146252U,
			// Token: 0x04000276 RID: 630
			GroupsIBelongToDescription = 4067290517U,
			// Token: 0x04000277 RID: 631
			StartTimeText = 2161945740U,
			// Token: 0x04000278 RID: 632
			RemoveCommandText = 91158280U,
			// Token: 0x04000279 RID: 633
			PersonalSettingChangePassword = 1057459323U,
			// Token: 0x0400027A RID: 634
			PasswordLabel = 3598785721U,
			// Token: 0x0400027B RID: 635
			InboxRuleHasAttachmentConditionText = 1986416405U,
			// Token: 0x0400027C RID: 636
			CalendarPublishingLinks = 988028286U,
			// Token: 0x0400027D RID: 637
			RequirementsReadWriteItemValue = 3784078605U,
			// Token: 0x0400027E RID: 638
			ImportContactListProgress = 1710677402U,
			// Token: 0x0400027F RID: 639
			TrialReminderActionLinkText = 874684615U,
			// Token: 0x04000280 RID: 640
			VoiceMailNotificationsLink = 2040917585U,
			// Token: 0x04000281 RID: 641
			InboxRuleMyNameInToCcBoxConditionText = 856915648U,
			// Token: 0x04000282 RID: 642
			InstallFromFile = 2736378449U,
			// Token: 0x04000283 RID: 643
			DeviceMOWAVersionLabel = 2000126950U,
			// Token: 0x04000284 RID: 644
			Subject = 1732412034U,
			// Token: 0x04000285 RID: 645
			EditAccountCommandText = 1266539147U,
			// Token: 0x04000286 RID: 646
			AutomaticRepliesScheduledCheckboxText = 3491325124U,
			// Token: 0x04000287 RID: 647
			RetentionActionTypeHeader = 3616424413U,
			// Token: 0x04000288 RID: 648
			QLOutlook = 3507358566U,
			// Token: 0x04000289 RID: 649
			InboxRuleFromConditionText = 3300930250U,
			// Token: 0x0400028A RID: 650
			MessageTrackingFailedEvent = 582625299U,
			// Token: 0x0400028B RID: 651
			IncomingSecurityTLS = 1068599355U,
			// Token: 0x0400028C RID: 652
			WithinSizeRangeConditionFormat = 961444931U,
			// Token: 0x0400028D RID: 653
			VoicemailCallFwdStep3 = 2118875233U,
			// Token: 0x0400028E RID: 654
			MyMailbox = 3710804460U,
			// Token: 0x0400028F RID: 655
			CalendarPublishingDetail = 2534471784U,
			// Token: 0x04000290 RID: 656
			DeviceLastSuccessfulSyncLabel = 1753433743U,
			// Token: 0x04000291 RID: 657
			ClearButtonText = 1279019904U,
			// Token: 0x04000292 RID: 658
			InboxRuleCopyToFolderActionText = 1541555783U,
			// Token: 0x04000293 RID: 659
			RPTYearsMonths = 1806917741U,
			// Token: 0x04000294 RID: 660
			FromConditionFormat = 3714415956U,
			// Token: 0x04000295 RID: 661
			DeviceUserAgentLabel = 908779566U,
			// Token: 0x04000296 RID: 662
			DepartGroupConfirmation = 2657380710U,
			// Token: 0x04000297 RID: 663
			InboxRuleWithImportanceConditionText = 2291011248U,
			// Token: 0x04000298 RID: 664
			PersonalSettingDomainUser = 492355799U,
			// Token: 0x04000299 RID: 665
			SiteMailboxEmailMeDiagnosticsButtonString = 1452418752U,
			// Token: 0x0400029A RID: 666
			DeliveryManagement = 2174687123U,
			// Token: 0x0400029B RID: 667
			PersonalSettingPasswordBeforeChange = 1249600476U,
			// Token: 0x0400029C RID: 668
			SmtpSetting = 1511367726U,
			// Token: 0x0400029D RID: 669
			MessageFormatSlab = 1472422U,
			// Token: 0x0400029E RID: 670
			InboxRuleForwardToActionFlyOutText = 3994376130U,
			// Token: 0x0400029F RID: 671
			DuplicateProxyAddressError = 3663958361U,
			// Token: 0x040002A0 RID: 672
			CreatedBy = 2209887359U,
			// Token: 0x040002A1 RID: 673
			ReadReceiptResponseNever = 2999683987U,
			// Token: 0x040002A2 RID: 674
			AccessDeniedFooterBottom = 992729393U,
			// Token: 0x040002A3 RID: 675
			OwnerChangedUpdateModerator = 900932107U,
			// Token: 0x040002A4 RID: 676
			AllowRecurringMeetings = 1233400920U,
			// Token: 0x040002A5 RID: 677
			VoicemailWizardStep1Title = 508731823U,
			// Token: 0x040002A6 RID: 678
			TeamMailboxMembersString = 2265850499U,
			// Token: 0x040002A7 RID: 679
			AfterDateDisplayTemplate = 417105304U,
			// Token: 0x040002A8 RID: 680
			TuesdayCheckBoxText = 1413204805U,
			// Token: 0x040002A9 RID: 681
			Join = 1367192090U,
			// Token: 0x040002AA RID: 682
			AddAdditionalResponse = 2408804455U,
			// Token: 0x040002AB RID: 683
			OkButtonText = 737660655U,
			// Token: 0x040002AC RID: 684
			FoldersSyncedLabel = 3175109089U,
			// Token: 0x040002AD RID: 685
			SendToAllText = 3789286439U,
			// Token: 0x040002AE RID: 686
			MaximumDurationInMinutesErrorMessage = 3957821083U,
			// Token: 0x040002AF RID: 687
			TextMessagingTurnedOnViaEas = 3260418603U,
			// Token: 0x040002B0 RID: 688
			StartLoggingCommandText = 3047217661U,
			// Token: 0x040002B1 RID: 689
			MailboxUsageLegacyText = 2811760173U,
			// Token: 0x040002B2 RID: 690
			RPTYears = 1144080682U,
			// Token: 0x040002B3 RID: 691
			ReadReceiptResponseInstruction = 143410217U,
			// Token: 0x040002B4 RID: 692
			RemindersEnabled = 636289130U,
			// Token: 0x040002B5 RID: 693
			AddNewRequestsTentativelyCheckBoxText = 1284433590U,
			// Token: 0x040002B6 RID: 694
			MessageTrackingSubmitEvent = 1865553596U,
			// Token: 0x040002B7 RID: 695
			VoicemailLearnMore = 1981955278U,
			// Token: 0x040002B8 RID: 696
			EmailThisReport = 2288506116U,
			// Token: 0x040002B9 RID: 697
			CalendarPublishingDateRange = 1435311110U,
			// Token: 0x040002BA RID: 698
			SelectUsersAndGroups = 3485844721U,
			// Token: 0x040002BB RID: 699
			PhotoBookmark = 1183459890U,
			// Token: 0x040002BC RID: 700
			InboxRuleHeaderContainsConditionText = 2440572518U,
			// Token: 0x040002BD RID: 701
			BookingWindowInDaysErrorMessage = 1356144580U,
			// Token: 0x040002BE RID: 702
			Calendar = 1292798904U,
			// Token: 0x040002BF RID: 703
			RuleSubjectContainsAndMoveToFolderTemplate = 1152863492U,
			// Token: 0x040002C0 RID: 704
			RuleStateOn = 817924740U,
			// Token: 0x040002C1 RID: 705
			UserLogonNameLabel = 3913001929U,
			// Token: 0x040002C2 RID: 706
			RPTMonth = 2426358350U,
			// Token: 0x040002C3 RID: 707
			EndTime = 1824826658U,
			// Token: 0x040002C4 RID: 708
			InstallFromPrivateUrlTitle = 2917434001U,
			// Token: 0x040002C5 RID: 709
			LastSyncAttemptTimeHeaderText = 3131435199U,
			// Token: 0x040002C6 RID: 710
			RequirementsReadWriteItemDescription = 2572011012U,
			// Token: 0x040002C7 RID: 711
			NewItemNotificationVoiceMailToast = 940101428U,
			// Token: 0x040002C8 RID: 712
			RenameDefaultFoldersCheckBoxText = 2853482896U,
			// Token: 0x040002C9 RID: 713
			InboxRuleFromSubscriptionConditionText = 1380850475U,
			// Token: 0x040002CA RID: 714
			RetentionSelectOptionalLabel = 1440211402U,
			// Token: 0x040002CB RID: 715
			FromColumnLabel = 1914666238U,
			// Token: 0x040002CC RID: 716
			ExtensionCanNotBeDisabledNorUninstalled = 524522579U,
			// Token: 0x040002CD RID: 717
			CalendarPublishingPublic = 2157690544U,
			// Token: 0x040002CE RID: 718
			CalendarSharingExplanation = 1380849357U,
			// Token: 0x040002CF RID: 719
			CalendarPublishingViewUrl = 1525092013U,
			// Token: 0x040002D0 RID: 720
			SaturdayCheckBoxText = 3391167931U,
			// Token: 0x040002D1 RID: 721
			MailboxUsageUnitB = 1194500941U,
			// Token: 0x040002D2 RID: 722
			HasAttachmentConditionFormat = 2771220587U,
			// Token: 0x040002D3 RID: 723
			VoicemailWizardStep5Title = 2993887811U,
			// Token: 0x040002D4 RID: 724
			PreviewMarkAsReadBehaviorNever = 3866819705U,
			// Token: 0x040002D5 RID: 725
			SubscriptionDialogLabel = 4136480461U,
			// Token: 0x040002D6 RID: 726
			NewSubscription = 1033615903U,
			// Token: 0x040002D7 RID: 727
			LastSynchronization = 826260386U,
			// Token: 0x040002D8 RID: 728
			ChangeCalendarPermissions = 1189779660U,
			// Token: 0x040002D9 RID: 729
			DefaultReminderTimeLabel = 3183463582U,
			// Token: 0x040002DA RID: 730
			OpenNextItem = 2947609428U,
			// Token: 0x040002DB RID: 731
			EmailOptions = 3213113944U,
			// Token: 0x040002DC RID: 732
			RetentionActionTypeDefaultDelete = 461256164U,
			// Token: 0x040002DD RID: 733
			ChangeCommandText = 3032482008U,
			// Token: 0x040002DE RID: 734
			ExternalMessageGalLessInstruction = 1054605049U,
			// Token: 0x040002DF RID: 735
			ImportContactListNoFileUploaded = 508072824U,
			// Token: 0x040002E0 RID: 736
			BadOfficeCallbackMessage = 1294601601U,
			// Token: 0x040002E1 RID: 737
			DefaultImage = 2511894476U,
			// Token: 0x040002E2 RID: 738
			JoinAndDepart = 393831119U,
			// Token: 0x040002E3 RID: 739
			InboxRuleMyNameInToCcBoxConditionFlyOutText = 3881794065U,
			// Token: 0x040002E4 RID: 740
			TeamMailboxTitleString = 2202590644U,
			// Token: 0x040002E5 RID: 741
			NewestOnTop = 1746211700U,
			// Token: 0x040002E6 RID: 742
			ToWithExample = 2730942909U,
			// Token: 0x040002E7 RID: 743
			GroupOwners = 1571878129U,
			// Token: 0x040002E8 RID: 744
			DefaultRetentionTagDescription = 1456975951U,
			// Token: 0x040002E9 RID: 745
			Hours = 2811696355U,
			// Token: 0x040002EA RID: 746
			MessageTypeDialogTitle = 643576831U,
			// Token: 0x040002EB RID: 747
			SearchDeliveryReports = 3392276543U,
			// Token: 0x040002EC RID: 748
			VoicemailCallFwdStep1 = 2118875235U,
			// Token: 0x040002ED RID: 749
			SendAddressSetting = 4130400892U,
			// Token: 0x040002EE RID: 750
			InboxRuleItIsGroupText = 787898729U,
			// Token: 0x040002EF RID: 751
			LaunchOfficeMarketplace = 1694176600U,
			// Token: 0x040002F0 RID: 752
			CalendarDiagnosticLogDescription = 534802423U,
			// Token: 0x040002F1 RID: 753
			InboxRules = 1084745363U,
			// Token: 0x040002F2 RID: 754
			VoicemailSMSOptionVoiceMailAndMissedCalls = 942329033U,
			// Token: 0x040002F3 RID: 755
			StartTime = 2138448017U,
			// Token: 0x040002F4 RID: 756
			TextMessagingSlabMessage = 1074402166U,
			// Token: 0x040002F5 RID: 757
			WipeDeviceConfirmMessageDetail = 1548729791U,
			// Token: 0x040002F6 RID: 758
			VoicemailWizardStep2DescriptionNoPasscode = 2711159495U,
			// Token: 0x040002F7 RID: 759
			DeleteInboxRulesConfirmation = 3410748733U,
			// Token: 0x040002F8 RID: 760
			CalendarNotificationsLink = 3782284348U,
			// Token: 0x040002F9 RID: 761
			InboxRuleHasClassificationConditionFlyOutText = 2245512691U,
			// Token: 0x040002FA RID: 762
			CalendarWorkflowInstruction = 3223016465U,
			// Token: 0x040002FB RID: 763
			AutomaticRepliesInstruction = 672959953U,
			// Token: 0x040002FC RID: 764
			DeviceLanguageLabel = 3095426072U,
			// Token: 0x040002FD RID: 765
			MoveUp = 137938150U,
			// Token: 0x040002FE RID: 766
			InstallButtonText = 810469194U,
			// Token: 0x040002FF RID: 767
			WithSensitivityConditionFormat = 3324278371U,
			// Token: 0x04000300 RID: 768
			VoicemailWizardPinLabel = 434719229U,
			// Token: 0x04000301 RID: 769
			MondayCheckBoxText = 2560210044U,
			// Token: 0x04000302 RID: 770
			WithImportanceConditionFormat = 984832422U,
			// Token: 0x04000303 RID: 771
			NewDistributionGroupText = 2489355482U,
			// Token: 0x04000304 RID: 772
			CalendarReminderSlab = 1716421970U,
			// Token: 0x04000305 RID: 773
			EmailAddressesLabel = 2879758792U,
			// Token: 0x04000306 RID: 774
			PortLabel = 1512647751U,
			// Token: 0x04000307 RID: 775
			SetupVoiceMailNotificationsLink = 2405760124U,
			// Token: 0x04000308 RID: 776
			TextMessagingOff = 1075596584U,
			// Token: 0x04000309 RID: 777
			RuleSentToAndMoveToFolderTemplate = 602937486U,
			// Token: 0x0400030A RID: 778
			UserNameNotSetError = 1393180149U,
			// Token: 0x0400030B RID: 779
			MailboxUsageWarningText = 1065442140U,
			// Token: 0x0400030C RID: 780
			PopSetting = 2112942921U,
			// Token: 0x0400030D RID: 781
			InboxRuleFromAddressContainsConditionFlyOutText = 4023555694U,
			// Token: 0x0400030E RID: 782
			Options = 1511584348U,
			// Token: 0x0400030F RID: 783
			EMailSignatureSlab = 3474229968U,
			// Token: 0x04000310 RID: 784
			RetentionPeriodHoldFor = 3820898101U,
			// Token: 0x04000311 RID: 785
			MaximumConflictInstancesErrorMessage = 1362687599U,
			// Token: 0x04000312 RID: 786
			Organize = 3952282363U,
			// Token: 0x04000313 RID: 787
			MessageTrackingDLExpandedEvent = 4129994431U,
			// Token: 0x04000314 RID: 788
			VoicemailBrowserNotSupported = 1620472852U,
			// Token: 0x04000315 RID: 789
			SendAddressSettingSlabDescription = 1877028846U,
			// Token: 0x04000316 RID: 790
			RetrieveLogConfirmMessage = 2765871439U,
			// Token: 0x04000317 RID: 791
			DescriptionLabel = 460637234U,
			// Token: 0x04000318 RID: 792
			CustomAttributeDialogTitle = 3776153215U,
			// Token: 0x04000319 RID: 793
			MessageApproval = 527639998U,
			// Token: 0x0400031A RID: 794
			TeamMailboxPropertiesString = 931483573U,
			// Token: 0x0400031B RID: 795
			BodyContainsConditionFormat = 2650315815U,
			// Token: 0x0400031C RID: 796
			GroupModeration = 3936874395U,
			// Token: 0x0400031D RID: 797
			FreeBusySubjectLocationInformation = 2556623818U,
			// Token: 0x0400031E RID: 798
			InboxRuleMarkMessageGroupText = 3000766872U,
			// Token: 0x0400031F RID: 799
			InboxRuleConditionSectionHeader = 2673767273U,
			// Token: 0x04000320 RID: 800
			RecipientEmailButtonDescription = 3741421195U,
			// Token: 0x04000321 RID: 801
			TimeZone = 1779041463U,
			// Token: 0x04000322 RID: 802
			InboxRuleSentToConditionFlyOutText = 286364476U,
			// Token: 0x04000323 RID: 803
			MoveDown = 3496306419U,
			// Token: 0x04000324 RID: 804
			UnblockDeviceCommandText = 105290154U,
			// Token: 0x04000325 RID: 805
			TextMessagingSms = 3851792428U,
			// Token: 0x04000326 RID: 806
			LanguageInstruction = 3338384120U,
			// Token: 0x04000327 RID: 807
			InboxRuleFromConditionFlyOutText = 2653201341U,
			// Token: 0x04000328 RID: 808
			OutOfOffice = 773833243U,
			// Token: 0x04000329 RID: 809
			FirstNameLabel = 2449687995U,
			// Token: 0x0400032A RID: 810
			InboxRuleBodyContainsConditionFlyOutText = 271571182U,
			// Token: 0x0400032B RID: 811
			AllowedSendersLabelForEndUser = 2979279075U,
			// Token: 0x0400032C RID: 812
			VoicemailWizardStep2Title = 3600235422U,
			// Token: 0x0400032D RID: 813
			ConversationsSlab = 2483845882U,
			// Token: 0x0400032E RID: 814
			AutomaticRepliesSlab = 3601145337U,
			// Token: 0x0400032F RID: 815
			IncomingAuthenticationNtlm = 1137051325U,
			// Token: 0x04000330 RID: 816
			MobilePhoneLabel = 1448959650U,
			// Token: 0x04000331 RID: 817
			DeviceNameLabel = 2211346257U,
			// Token: 0x04000332 RID: 818
			HeaderContainsConditionFormat = 2946311768U,
			// Token: 0x04000333 RID: 819
			TeamMailboxDescription = 944969227U,
			// Token: 0x04000334 RID: 820
			EnterNumberClickNext = 2267981074U,
			// Token: 0x04000335 RID: 821
			MobileDevices = 3063130671U,
			// Token: 0x04000336 RID: 822
			DisplayRecoveryPasswordCommandDescription = 1188277387U,
			// Token: 0x04000337 RID: 823
			AtMostOnlyDisplayTemplate = 3451245278U,
			// Token: 0x04000338 RID: 824
			InboxRuleSentToConditionPreCannedText = 777656741U,
			// Token: 0x04000339 RID: 825
			InboxRuleMarkedWithGroupText = 2303439412U,
			// Token: 0x0400033A RID: 826
			Membership = 3455154594U,
			// Token: 0x0400033B RID: 827
			VoicemailSlab = 31525559U,
			// Token: 0x0400033C RID: 828
			AllInformation = 2801441113U,
			// Token: 0x0400033D RID: 829
			BlockOrAllow = 3723544685U,
			// Token: 0x0400033E RID: 830
			CalendarDiagnosticLogWatermarkText = 1716779644U,
			// Token: 0x0400033F RID: 831
			EditGroups = 2601540438U,
			// Token: 0x04000340 RID: 832
			TurnOnTextMessaging = 3335990695U,
			// Token: 0x04000341 RID: 833
			AliasLabelForEnterprise = 1418080636U,
			// Token: 0x04000342 RID: 834
			CategoryDialogTitle = 3019822456U,
			// Token: 0x04000343 RID: 835
			SetYourWorkingHours = 1806349341U,
			// Token: 0x04000344 RID: 836
			ProfileMailboxUsage = 3830590072U,
			// Token: 0x04000345 RID: 837
			AtLeastAtMostDisplayTemplate = 1985922850U,
			// Token: 0x04000346 RID: 838
			NotificationsForMeetingReminders = 1502911297U,
			// Token: 0x04000347 RID: 839
			IncomingServerLabel = 1650094581U,
			// Token: 0x04000348 RID: 840
			IncomingAuthenticationNone = 3052975740U,
			// Token: 0x04000349 RID: 841
			TrialReminderText = 1925535397U,
			// Token: 0x0400034A RID: 842
			GroupsIBelongToAndGroupsIOwnDescription = 783706065U
		}

		// Token: 0x02000004 RID: 4
		private enum ParamIDs
		{
			// Token: 0x0400034C RID: 844
			SetSubscriptionSucceed,
			// Token: 0x0400034D RID: 845
			ImportContactListPage2ResultNumber,
			// Token: 0x0400034E RID: 846
			JoinDlsSuccess,
			// Token: 0x0400034F RID: 847
			EditRuleCaption,
			// Token: 0x04000350 RID: 848
			VerificationEmailFailedToSend,
			// Token: 0x04000351 RID: 849
			VerificationSuccessText,
			// Token: 0x04000352 RID: 850
			JoinOtherDlsSuccess,
			// Token: 0x04000353 RID: 851
			ReceiveNotificationsUsingFormat,
			// Token: 0x04000354 RID: 852
			ImportContactListPage2Result,
			// Token: 0x04000355 RID: 853
			LargeRecipientList,
			// Token: 0x04000356 RID: 854
			VerificationEmailSucceeded,
			// Token: 0x04000357 RID: 855
			NewSubscriptionSucceed,
			// Token: 0x04000358 RID: 856
			JoinDlSuccess,
			// Token: 0x04000359 RID: 857
			VoicemailAccessNumbersTemplate
		}
	}
}
