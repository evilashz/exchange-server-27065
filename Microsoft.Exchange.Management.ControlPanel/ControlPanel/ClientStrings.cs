using System;
using System.Collections.Generic;
using System.Reflection;
using System.Resources;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x02000002 RID: 2
	public static class ClientStrings
	{
		// Token: 0x06000001 RID: 1 RVA: 0x000020D0 File Offset: 0x000002D0
		static ClientStrings()
		{
			ClientStrings.stringIDs.Add(616132976U, "ReconnectProviderCommandText");
			ClientStrings.stringIDs.Add(1708942702U, "FieldsInError");
			ClientStrings.stringIDs.Add(1038968483U, "TlsTitle");
			ClientStrings.stringIDs.Add(2085954050U, "UMKeyMappingTimeout");
			ClientStrings.stringIDs.Add(4245786716U, "RequiredFieldValidatorErrorMessage");
			ClientStrings.stringIDs.Add(385068607U, "OwaMailboxPolicyTasks");
			ClientStrings.stringIDs.Add(375881263U, "CopyIsIEOnly");
			ClientStrings.stringIDs.Add(1064772435U, "MinimumCriteriaFieldsInErrorDeliveryStatus");
			ClientStrings.stringIDs.Add(1884426512U, "EnterHybridUIConfirm");
			ClientStrings.stringIDs.Add(1692971068U, "DisableCommandText");
			ClientStrings.stringIDs.Add(1752619070U, "UMHolidayScheduleHolidayStartDateRequiredError");
			ClientStrings.stringIDs.Add(3063981087U, "EnterHybridUIButtonText");
			ClientStrings.stringIDs.Add(3891394090U, "ConstraintViolationValueOutOfRangeForQuota");
			ClientStrings.stringIDs.Add(1245979421U, "HydratingMessage");
			ClientStrings.stringIDs.Add(3561883164U, "GroupNamingPolicyPreviewDescriptionHeader");
			ClientStrings.stringIDs.Add(866598955U, "Validating");
			ClientStrings.stringIDs.Add(1125975266U, "UriKindRelative");
			ClientStrings.stringIDs.Add(3033402446U, "Close");
			ClientStrings.stringIDs.Add(3496306419U, "MoveDown");
			ClientStrings.stringIDs.Add(155777604U, "PeopleConnectBusy");
			ClientStrings.stringIDs.Add(509325205U, "LegacyRegExEnabledRuleLabel");
			ClientStrings.stringIDs.Add(1609794183U, "HydrationDataLossWarning");
			ClientStrings.stringIDs.Add(1398029738U, "NoTreeItem");
			ClientStrings.stringIDs.Add(1133914413U, "LearnMore");
			ClientStrings.stringIDs.Add(1032072896U, "AddEAPCondtionButtonText");
			ClientStrings.stringIDs.Add(848625314U, "InvalidDateRange");
			ClientStrings.stringIDs.Add(2717328418U, "DefaultRuleEditorCaption");
			ClientStrings.stringIDs.Add(3701726907U, "CtrlKeyGoToSearch");
			ClientStrings.stringIDs.Add(3084590849U, "DisableFVA");
			ClientStrings.stringIDs.Add(3237053118U, "Searching");
			ClientStrings.stringIDs.Add(859380341U, "Update");
			ClientStrings.stringIDs.Add(2781755715U, "CurrentPolicyCaption");
			ClientStrings.stringIDs.Add(4233379362U, "FollowedByColon");
			ClientStrings.stringIDs.Add(1281963896U, "EnabledDisplayText");
			ClientStrings.stringIDs.Add(2288607912U, "OffDisplayText");
			ClientStrings.stringIDs.Add(3866481608U, "OwaMailboxPolicyActiveSyncIntegration");
			ClientStrings.stringIDs.Add(915129961U, "PlayOnPhoneDisconnecting");
			ClientStrings.stringIDs.Add(4221859415U, "LegacyOUError");
			ClientStrings.stringIDs.Add(1158475769U, "WebServiceRequestServerError");
			ClientStrings.stringIDs.Add(1861340610U, "Warning");
			ClientStrings.stringIDs.Add(2089076466U, "BlockedPendingDisplayText");
			ClientStrings.stringIDs.Add(4153102186U, "MessageTypePickerInvalid");
			ClientStrings.stringIDs.Add(1693255708U, "OwaMailboxPolicyTextMessaging");
			ClientStrings.stringIDs.Add(717626182U, "OwaMailboxPolicyContacts");
			ClientStrings.stringIDs.Add(2200441302U, "Expand");
			ClientStrings.stringIDs.Add(2959308597U, "DisableConnectorConfirm");
			ClientStrings.stringIDs.Add(2142856376U, "CmdLogTitleForHybridEnterprise");
			ClientStrings.stringIDs.Add(3292878272U, "ProviderConnectedWithError");
			ClientStrings.stringIDs.Add(3612561219U, "RequestSpamDetail");
			ClientStrings.stringIDs.Add(1414246128U, "None");
			ClientStrings.stringIDs.Add(94147420U, "PassiveText");
			ClientStrings.stringIDs.Add(506561007U, "DisableFederationInProgress");
			ClientStrings.stringIDs.Add(1174301401U, "MobileDeviceDisableText");
			ClientStrings.stringIDs.Add(3442569333U, "MoreOptions");
			ClientStrings.stringIDs.Add(2522455672U, "MidnightAM");
			ClientStrings.stringIDs.Add(4104581892U, "NotificationCount");
			ClientStrings.stringIDs.Add(906828259U, "ContactsSharing");
			ClientStrings.stringIDs.Add(1644825107U, "LongRunInProgressDescription");
			ClientStrings.stringIDs.Add(2545492551U, "MailboxToSearchRequiredErrorMessage");
			ClientStrings.stringIDs.Add(321700842U, "DomainNoValue");
			ClientStrings.stringIDs.Add(3455735334U, "MyOptions");
			ClientStrings.stringIDs.Add(335355257U, "VoicemailConfigurationDetails");
			ClientStrings.stringIDs.Add(1803016445U, "CloseWindowOnLogout");
			ClientStrings.stringIDs.Add(3832063564U, "CustomizeColumns");
			ClientStrings.stringIDs.Add(3018361386U, "EnterProductKey");
			ClientStrings.stringIDs.Add(4158450825U, "PlayOnPhoneDialing");
			ClientStrings.stringIDs.Add(207011947U, "OwaMailboxPolicyUMIntegration");
			ClientStrings.stringIDs.Add(1066902945U, "HydrationFailed");
			ClientStrings.stringIDs.Add(506890308U, "EnableActiveSyncConfirm");
			ClientStrings.stringIDs.Add(3423705850U, "ConstraintViolationStringLengthIsEmpty");
			ClientStrings.stringIDs.Add(2455574594U, "SelectOneLink");
			ClientStrings.stringIDs.Add(2336951795U, "ConstraintNotNullOrEmpty");
			ClientStrings.stringIDs.Add(4193083863U, "LitigationHoldOwnerNotSet");
			ClientStrings.stringIDs.Add(1189851274U, "RequiredFieldIndicator");
			ClientStrings.stringIDs.Add(3508492594U, "FolderTree");
			ClientStrings.stringIDs.Add(3771622829U, "IncidentReportSelectAll");
			ClientStrings.stringIDs.Add(3819852093U, "Notification");
			ClientStrings.stringIDs.Add(2571312913U, "HydrationDoneFeatureFailed");
			ClientStrings.stringIDs.Add(1749577099U, "LongRunWarningLabel");
			ClientStrings.stringIDs.Add(1796910490U, "PublicFoldersEmptyDataTextRoot");
			ClientStrings.stringIDs.Add(1517827063U, "Unsuccessful");
			ClientStrings.stringIDs.Add(3951041989U, "TextMessagingNotificationNotSetupText");
			ClientStrings.stringIDs.Add(879792491U, "VoicemailConfigurationConfirmationMessage");
			ClientStrings.stringIDs.Add(865985204U, "EnableFederationInProgress");
			ClientStrings.stringIDs.Add(3625629356U, "OwaMailboxPolicyAllowCopyContactsToDeviceAddressBook");
			ClientStrings.stringIDs.Add(2357636744U, "OwaMailboxPolicyInformationManagement");
			ClientStrings.stringIDs.Add(3506211787U, "WarningPanelDisMissMsg");
			ClientStrings.stringIDs.Add(1523028278U, "OwaMailboxPolicyJournal");
			ClientStrings.stringIDs.Add(993523547U, "DatesNotDefined");
			ClientStrings.stringIDs.Add(4062764466U, "EnableOWAConfirm");
			ClientStrings.stringIDs.Add(2266587541U, "CancelWipePendingDisplayText");
			ClientStrings.stringIDs.Add(2546927256U, "DeliveryReportSearchFieldsInError");
			ClientStrings.stringIDs.Add(2954372719U, "MyOrganization");
			ClientStrings.stringIDs.Add(3927445923U, "Today");
			ClientStrings.stringIDs.Add(1992800455U, "ExtendedReportsInsufficientData");
			ClientStrings.stringIDs.Add(3963002503U, "EnableConnectorLoggingConfirm");
			ClientStrings.stringIDs.Add(2805968874U, "MessageTraceInvalidEndDate");
			ClientStrings.stringIDs.Add(1788720538U, "AddSubnetCaption");
			ClientStrings.stringIDs.Add(4091981230U, "CustomizeSenderLabel");
			ClientStrings.stringIDs.Add(233002034U, "SharedUMAutoAttendantPilotIdentifierListE164Error");
			ClientStrings.stringIDs.Add(1562506021U, "PreviousMonth");
			ClientStrings.stringIDs.Add(3833953726U, "Stop");
			ClientStrings.stringIDs.Add(3958687323U, "AllAvailableIPV6Address");
			ClientStrings.stringIDs.Add(3462654059U, "LetCallersInterruptGreetingsText");
			ClientStrings.stringIDs.Add(3386445864U, "GroupNamingPolicyEditorPrefixLabel");
			ClientStrings.stringIDs.Add(1102979128U, "Transferred");
			ClientStrings.stringIDs.Add(2787872604U, "NewDomain");
			ClientStrings.stringIDs.Add(2390512187U, "PublicFoldersEmptyDataTextChildren");
			ClientStrings.stringIDs.Add(2682114028U, "FacebookDelayed");
			ClientStrings.stringIDs.Add(1342839575U, "Collapse");
			ClientStrings.stringIDs.Add(4211282305U, "GroupNamingPolicyEditorSuffixLabel");
			ClientStrings.stringIDs.Add(630233035U, "MessageFontSampleText");
			ClientStrings.stringIDs.Add(3186571665U, "HideModalDialogErrorReport");
			ClientStrings.stringIDs.Add(187569137U, "JobSubmissionWaitText");
			ClientStrings.stringIDs.Add(4151098027U, "ServiceNone");
			ClientStrings.stringIDs.Add(2328220407U, "Page");
			ClientStrings.stringIDs.Add(2828598385U, "NavigateAway");
			ClientStrings.stringIDs.Add(1122075U, "RemoveDisabledLinkedInConnectionText");
			ClientStrings.stringIDs.Add(59720971U, "HCWStoppedDescription");
			ClientStrings.stringIDs.Add(2658434708U, "OnDisplayText");
			ClientStrings.stringIDs.Add(1981061025U, "LongRunErrorLabel");
			ClientStrings.stringIDs.Add(809706446U, "EIAE");
			ClientStrings.stringIDs.Add(1907621764U, "NegotiateAuth");
			ClientStrings.stringIDs.Add(3122995196U, "RequestDLPDetailReportTitle");
			ClientStrings.stringIDs.Add(2736422594U, "WipeConfirmMessage");
			ClientStrings.stringIDs.Add(317346726U, "MobileDeviceEnableText");
			ClientStrings.stringIDs.Add(3197491520U, "EnableConnectorConfirm");
			ClientStrings.stringIDs.Add(1875331145U, "OffCommandText");
			ClientStrings.stringIDs.Add(3778906594U, "HydrationDoneTitle");
			ClientStrings.stringIDs.Add(1144247250U, "ConnectorAllAvailableIPv6");
			ClientStrings.stringIDs.Add(1795974124U, "UMCallAnsweringRulesEditorRuleConditionLabelText");
			ClientStrings.stringIDs.Add(3225869645U, "OwaMailboxPolicyPlaces");
			ClientStrings.stringIDs.Add(602293241U, "JournalEmailAddressLabel");
			ClientStrings.stringIDs.Add(259008929U, "PopupBlockedMessage");
			ClientStrings.stringIDs.Add(2802879859U, "IUnderstandAction");
			ClientStrings.stringIDs.Add(2532213629U, "Select15Minutes");
			ClientStrings.stringIDs.Add(1811854250U, "AllowedPendingDisplayText");
			ClientStrings.stringIDs.Add(1848756223U, "OwaMailboxPolicyTimeManagement");
			ClientStrings.stringIDs.Add(3077953159U, "PasswordNote");
			ClientStrings.stringIDs.Add(573217698U, "HasLinkQueryField");
			ClientStrings.stringIDs.Add(1318187683U, "VoicemailClearSettingsTitle");
			ClientStrings.stringIDs.Add(3269293275U, "ConfigureOAuth");
			ClientStrings.stringIDs.Add(3068683287U, "And");
			ClientStrings.stringIDs.Add(4210295185U, "VoicemailResetPINSuccessMessage");
			ClientStrings.stringIDs.Add(3121279917U, "FileDownloadFailed");
			ClientStrings.stringIDs.Add(3545744552U, "ConfirmRemoveLinkedIn");
			ClientStrings.stringIDs.Add(1354285736U, "RemoveFacebookSupportingText");
			ClientStrings.stringIDs.Add(4266824568U, "ListViewMoreResultsWarning");
			ClientStrings.stringIDs.Add(795464922U, "DisableReplicationCommandText");
			ClientStrings.stringIDs.Add(519362517U, "EnterpriseMainHeader");
			ClientStrings.stringIDs.Add(313078617U, "AddGroupNamingPolicyElementButtonText");
			ClientStrings.stringIDs.Add(571113625U, "UseAlias");
			ClientStrings.stringIDs.Add(1300295012U, "FileUploadFailed");
			ClientStrings.stringIDs.Add(1277846131U, "CustomDateLink");
			ClientStrings.stringIDs.Add(4122267321U, "PolicyGroupMembership");
			ClientStrings.stringIDs.Add(1548165396U, "NextPage");
			ClientStrings.stringIDs.Add(437474464U, "HydrationDone");
			ClientStrings.stringIDs.Add(4033288601U, "ProviderDisabled");
			ClientStrings.stringIDs.Add(306768456U, "IndividualSettings");
			ClientStrings.stringIDs.Add(616309426U, "CalendarSharingFreeBusyDetail");
			ClientStrings.stringIDs.Add(2134452995U, "LongRunInProgressTips");
			ClientStrings.stringIDs.Add(2004565666U, "OwaMailboxPolicyChangePassword");
			ClientStrings.stringIDs.Add(1595040325U, "VoicemailClearSettingsDetailsContactOperator");
			ClientStrings.stringIDs.Add(2738758716U, "DisableFederationStopped");
			ClientStrings.stringIDs.Add(2659939651U, "Success");
			ClientStrings.stringIDs.Add(2197391249U, "NoOnboardingPermission");
			ClientStrings.stringIDs.Add(183956792U, "HydratingTitle");
			ClientStrings.stringIDs.Add(2622420678U, "TextMessagingNotificationSetupLinkText");
			ClientStrings.stringIDs.Add(533757016U, "WipePendingPendingDisplayText");
			ClientStrings.stringIDs.Add(1565584202U, "InvalidMultiEmailAddress");
			ClientStrings.stringIDs.Add(4033771799U, "DataCenterMainHeader");
			ClientStrings.stringIDs.Add(893456894U, "BulkEditNotificationTenMinuteLabel");
			ClientStrings.stringIDs.Add(124543120U, "DefaultRuleExceptionLabel");
			ClientStrings.stringIDs.Add(2402178338U, "SelectTheTextAndCopy");
			ClientStrings.stringIDs.Add(2165251921U, "FailedToRetrieveMailboxOnboarding");
			ClientStrings.stringIDs.Add(3546966747U, "DisabledDisplayText");
			ClientStrings.stringIDs.Add(1361553573U, "ConditionValueSeparator");
			ClientStrings.stringIDs.Add(2388288698U, "LinkedInDelayed");
			ClientStrings.stringIDs.Add(933672694U, "ErrorTitle");
			ClientStrings.stringIDs.Add(3042237373U, "InvalidSmtpAddress");
			ClientStrings.stringIDs.Add(128074410U, "RemoveLinkedInSupportingText");
			ClientStrings.stringIDs.Add(2368306281U, "ResumeDBCopyConfirmation");
			ClientStrings.stringIDs.Add(1168324224U, "MessageTypeAll");
			ClientStrings.stringIDs.Add(3966162852U, "CmdLogTitleForHybridO365");
			ClientStrings.stringIDs.Add(207358046U, "ConfirmRemoveFacebook");
			ClientStrings.stringIDs.Add(3329734225U, "BulkEditNotificationMinuteLabel");
			ClientStrings.stringIDs.Add(2523533992U, "VoiceMailText");
			ClientStrings.stringIDs.Add(2616506832U, "CollapseAll");
			ClientStrings.stringIDs.Add(1728961555U, "DefaultContactsFolderText");
			ClientStrings.stringIDs.Add(979822077U, "OwaMailboxPolicyWeather");
			ClientStrings.stringIDs.Add(1129028723U, "LegacyFolderError");
			ClientStrings.stringIDs.Add(2904016598U, "MessageTraceReportTitle");
			ClientStrings.stringIDs.Add(2966716344U, "JournalEmailAddressInvalid");
			ClientStrings.stringIDs.Add(2501247758U, "JobSubmitted");
			ClientStrings.stringIDs.Add(3058620969U, "UMEnableE164ActionSummary");
			ClientStrings.stringIDs.Add(2041362128U, "OK");
			ClientStrings.stringIDs.Add(3303348785U, "LastPage");
			ClientStrings.stringIDs.Add(2715655425U, "OwaMailboxPolicyRemindersAndNotifications");
			ClientStrings.stringIDs.Add(1359139161U, "DataLossWarning");
			ClientStrings.stringIDs.Add(3428137968U, "SuspendComments");
			ClientStrings.stringIDs.Add(1531436154U, "Delivered");
			ClientStrings.stringIDs.Add(479196852U, "Retry");
			ClientStrings.stringIDs.Add(1777112844U, "Descending");
			ClientStrings.stringIDs.Add(872019732U, "SimpleFilterTextBoxWaterMark");
			ClientStrings.stringIDs.Add(2058638459U, "TypingDescription");
			ClientStrings.stringIDs.Add(4012579652U, "NonEditingAuthor");
			ClientStrings.stringIDs.Add(3976037671U, "MinimumCriteriaFieldsInError");
			ClientStrings.stringIDs.Add(1349255527U, "ListSeparator");
			ClientStrings.stringIDs.Add(18372887U, "ExpandAll");
			ClientStrings.stringIDs.Add(968612268U, "AutoInternal");
			ClientStrings.stringIDs.Add(3953482277U, "NeverUse");
			ClientStrings.stringIDs.Add(4257989793U, "NoonPM");
			ClientStrings.stringIDs.Add(2204193123U, "EnableReplicationCommandText");
			ClientStrings.stringIDs.Add(1536001239U, "HCWCompletedDescription");
			ClientStrings.stringIDs.Add(3618788766U, "PWTNS");
			ClientStrings.stringIDs.Add(3667211102U, "DeviceModelPickerAll");
			ClientStrings.stringIDs.Add(1040160067U, "NextMonth");
			ClientStrings.stringIDs.Add(3066268041U, "UploaderUnhandledExceptionMessage");
			ClientStrings.stringIDs.Add(1677432033U, "MobileExternal");
			ClientStrings.stringIDs.Add(184677197U, "SearchButtonTooltip");
			ClientStrings.stringIDs.Add(1715033809U, "SavingCompletedInformation");
			ClientStrings.stringIDs.Add(2274379572U, "SetupExchangeHybrid");
			ClientStrings.stringIDs.Add(1355682252U, "EnableFVA");
			ClientStrings.stringIDs.Add(3247026326U, "PWTNAB");
			ClientStrings.stringIDs.Add(1759109339U, "ForceConnectMailbox");
			ClientStrings.stringIDs.Add(4291377740U, "ShowModalDialogErrorReport");
			ClientStrings.stringIDs.Add(2583693733U, "Imap");
			ClientStrings.stringIDs.Add(3122803756U, "ConnectToFacebookMessage");
			ClientStrings.stringIDs.Add(1638106043U, "DateRangeError");
			ClientStrings.stringIDs.Add(1138816392U, "OwaMailboxPolicyUserExperience");
			ClientStrings.stringIDs.Add(3831099320U, "WebServiceRequestInetError");
			ClientStrings.stringIDs.Add(839901080U, "FindMeText");
			ClientStrings.stringIDs.Add(3186529231U, "CtrlKeySelectAllInListView");
			ClientStrings.stringIDs.Add(3086537020U, "RemoveAction");
			ClientStrings.stringIDs.Add(2835992379U, "UMHolidayScheduleHolidayEndDateRequiredError");
			ClientStrings.stringIDs.Add(3179034952U, "NetworkCredentialUserNameErrorMessage");
			ClientStrings.stringIDs.Add(1579815837U, "SetupHybridUIFirst");
			ClientStrings.stringIDs.Add(1205297021U, "UMKeyMappingActionSummaryAnnounceBusinessLocation");
			ClientStrings.stringIDs.Add(1139168619U, "GroupNamingPolicyCaption");
			ClientStrings.stringIDs.Add(3022174399U, "TransferToGalContactText");
			ClientStrings.stringIDs.Add(4125561529U, "UnhandledExceptionMessage");
			ClientStrings.stringIDs.Add(2614439623U, "OwaMailboxPolicyJunkEmail");
			ClientStrings.stringIDs.Add(4070877873U, "DynamicDistributionGroupText");
			ClientStrings.stringIDs.Add(2522496037U, "ServerNameColumnText");
			ClientStrings.stringIDs.Add(1445885649U, "TextMessagingNotificationNotSetupLinkText");
			ClientStrings.stringIDs.Add(3450836391U, "QuerySyntaxError");
			ClientStrings.stringIDs.Add(1278568394U, "SharingDomainOptionAll");
			ClientStrings.stringIDs.Add(1907037086U, "VoicemailWizardEnterPinText");
			ClientStrings.stringIDs.Add(4068892486U, "AddressExists");
			ClientStrings.stringIDs.Add(1987425903U, "ModifyExchangeHybrid");
			ClientStrings.stringIDs.Add(2944220729U, "HydrationAndFeatureDone");
			ClientStrings.stringIDs.Add(997295902U, "ProviderConnected");
			ClientStrings.stringIDs.Add(3174388608U, "NoNamingPolicySetup");
			ClientStrings.stringIDs.Add(3000098309U, "DoNotShowDialog");
			ClientStrings.stringIDs.Add(238804546U, "RemoveMailboxDeleteLiveID");
			ClientStrings.stringIDs.Add(1527687315U, "GreetingsAndPromptsTitleText");
			ClientStrings.stringIDs.Add(3621606012U, "ConfigureVoicemailButtonText");
			ClientStrings.stringIDs.Add(613554384U, "FirstNameLastName");
			ClientStrings.stringIDs.Add(3021629903U, "Yes");
			ClientStrings.stringIDs.Add(3560108081U, "Author");
			ClientStrings.stringIDs.Add(148099519U, "PWTRAS");
			ClientStrings.stringIDs.Add(2482463458U, "TransferToGalContactVoicemailText");
			ClientStrings.stringIDs.Add(2706637479U, "MailboxDelegationDetail");
			ClientStrings.stringIDs.Add(381169853U, "Or");
			ClientStrings.stringIDs.Add(1594942057U, "Reset");
			ClientStrings.stringIDs.Add(1904041070U, "UpdateTimeZonePrompt");
			ClientStrings.stringIDs.Add(3369491564U, "DontSave");
			ClientStrings.stringIDs.Add(2908249814U, "VoicemailSummaryAccessNumber");
			ClientStrings.stringIDs.Add(2328220357U, "Save");
			ClientStrings.stringIDs.Add(2864395898U, "OwaMailboxPolicyThemeSelection");
			ClientStrings.stringIDs.Add(1393796710U, "ReadThis");
			ClientStrings.stringIDs.Add(2379979535U, "SubnetIPEditorTitle");
			ClientStrings.stringIDs.Add(3782146179U, "UMEnableExtensionAuto");
			ClientStrings.stringIDs.Add(1062541333U, "WarningPanelMultipleWarnings");
			ClientStrings.stringIDs.Add(502251987U, "UMHolidayScheduleHolidayNameRequiredError");
			ClientStrings.stringIDs.Add(3674505245U, "Select24Hours");
			ClientStrings.stringIDs.Add(2794185135U, "MemberUpdateTypeApprovalRequired");
			ClientStrings.stringIDs.Add(639660141U, "DefaultRuleActionLabel");
			ClientStrings.stringIDs.Add(4195962670U, "EmptyValueError");
			ClientStrings.stringIDs.Add(2794530675U, "ApplyToAllCalls");
			ClientStrings.stringIDs.Add(3908900425U, "OutOfMemoryErrorMessage");
			ClientStrings.stringIDs.Add(1594930120U, "Never");
			ClientStrings.stringIDs.Add(2694533749U, "OABExternal");
			ClientStrings.stringIDs.Add(320649385U, "ReconnectToFacebookMessage");
			ClientStrings.stringIDs.Add(1889215887U, "MemberApprovalHasChanged");
			ClientStrings.stringIDs.Add(2257237083U, "CreatingFolder");
			ClientStrings.stringIDs.Add(2743921036U, "UMEnableMailboxAutoSipDescription");
			ClientStrings.stringIDs.Add(367868548U, "FirstNameLastNameInitial");
			ClientStrings.stringIDs.Add(1690161621U, "PleaseWait");
			ClientStrings.stringIDs.Add(2091505548U, "WhatDoesThisMean");
			ClientStrings.stringIDs.Add(3036539697U, "ModalDialogErrorReport");
			ClientStrings.stringIDs.Add(354624350U, "SecondaryNavigation");
			ClientStrings.stringIDs.Add(305410994U, "ConnectToLinkedInMessage");
			ClientStrings.stringIDs.Add(503026860U, "MessageTraceMessageIDCannotContainComma");
			ClientStrings.stringIDs.Add(918737566U, "ApplyToAllMessages");
			ClientStrings.stringIDs.Add(689982738U, "ActiveSyncDisableText");
			ClientStrings.stringIDs.Add(637136194U, "BasicAuth");
			ClientStrings.stringIDs.Add(2870521773U, "VoicemailResetPINTitle");
			ClientStrings.stringIDs.Add(2777189520U, "LongRunCompletedTips");
			ClientStrings.stringIDs.Add(3817888713U, "GroupNamingPolicyPreviewLabel");
			ClientStrings.stringIDs.Add(2053059583U, "AASL");
			ClientStrings.stringIDs.Add(924670953U, "MemberUpdateTypeOpen");
			ClientStrings.stringIDs.Add(2013213994U, "PublishingEditor");
			ClientStrings.stringIDs.Add(1356895513U, "LossDataWarning");
			ClientStrings.stringIDs.Add(2014560916U, "OwaMailboxPolicyInstantMessaging");
			ClientStrings.stringIDs.Add(2093942654U, "TextTooLongErrorMessage");
			ClientStrings.stringIDs.Add(1833904645U, "EnableFederationStopped");
			ClientStrings.stringIDs.Add(912552836U, "GreetingsAndPromptsInstructionsText");
			ClientStrings.stringIDs.Add(771355430U, "UMHolidayScheduleHolidayPromptRequiredError");
			ClientStrings.stringIDs.Add(1515892343U, "AddDagMember");
			ClientStrings.stringIDs.Add(259319032U, "DistributionGroupText");
			ClientStrings.stringIDs.Add(1244644071U, "KeyMappingDisplayTextFormat");
			ClientStrings.stringIDs.Add(3924205880U, "RequestRuleDetailReportTitle");
			ClientStrings.stringIDs.Add(1865766159U, "RequestMalwareDetailReportTitle");
			ClientStrings.stringIDs.Add(173806984U, "ConfirmRemoveConnectionTitle");
			ClientStrings.stringIDs.Add(2320161129U, "WebServicesInternal");
			ClientStrings.stringIDs.Add(49675595U, "Wait");
			ClientStrings.stringIDs.Add(4196350991U, "DisableOWAConfirm");
			ClientStrings.stringIDs.Add(2663567466U, "UriKindRelativeOrAbsolute");
			ClientStrings.stringIDs.Add(2122906481U, "SessionTimeout");
			ClientStrings.stringIDs.Add(1742879518U, "Change");
			ClientStrings.stringIDs.Add(339824766U, "LastNameFirstName");
			ClientStrings.stringIDs.Add(204633756U, "AddIPAddress");
			ClientStrings.stringIDs.Add(1091377885U, "InvalidUnlimitedQuotaRegex");
			ClientStrings.stringIDs.Add(1623670356U, "NoSenderAddressWarning");
			ClientStrings.stringIDs.Add(3333839866U, "TextMessagingNotificationSetupText");
			ClientStrings.stringIDs.Add(2334202318U, "LastNameFirstNameInitial");
			ClientStrings.stringIDs.Add(2124576504U, "UnhandledExceptionTitle");
			ClientStrings.stringIDs.Add(3689797535U, "NoConditionErrorText");
			ClientStrings.stringIDs.Add(3348900521U, "FirstPage");
			ClientStrings.stringIDs.Add(1673297715U, "CannotUploadMultipleFiles");
			ClientStrings.stringIDs.Add(3328778458U, "ClearButtonTooltip");
			ClientStrings.stringIDs.Add(2941133106U, "AutoExternal");
			ClientStrings.stringIDs.Add(768075136U, "DayAndTimeRangeTooltip");
			ClientStrings.stringIDs.Add(3147146074U, "ViewNotificationDetails");
			ClientStrings.stringIDs.Add(2053059451U, "EASL");
			ClientStrings.stringIDs.Add(965845689U, "Outlook");
			ClientStrings.stringIDs.Add(3904375665U, "ConnectProviderCommandText");
			ClientStrings.stringIDs.Add(2391603832U, "SpecificPhoneNumberText");
			ClientStrings.stringIDs.Add(334057287U, "Pending");
			ClientStrings.stringIDs.Add(3216786356U, "CalendarSharingFreeBusyReviewer");
			ClientStrings.stringIDs.Add(3097242366U, "LitigationHoldDateNotSet");
			ClientStrings.stringIDs.Add(2796848775U, "OwaMailboxPolicyAllAddressLists");
			ClientStrings.stringIDs.Add(22442200U, "Error");
			ClientStrings.stringIDs.Add(1466672057U, "Externaldnslookups");
			ClientStrings.stringIDs.Add(3849511688U, "EditVoicemailButtonText");
			ClientStrings.stringIDs.Add(903130308U, "FailedToRetrieveMailboxLocalMove");
			ClientStrings.stringIDs.Add(1144247248U, "ConnectorAllAvailableIPv4");
			ClientStrings.stringIDs.Add(2262897061U, "HydrationAndFeatureDoneTitle");
			ClientStrings.stringIDs.Add(2058499689U, "ConstraintViolationNoLeadingOrTrailingWhitespace");
			ClientStrings.stringIDs.Add(2862582797U, "CalendarSharingFreeBusySimple");
			ClientStrings.stringIDs.Add(4277755996U, "PageSize");
			ClientStrings.stringIDs.Add(2742068696U, "ConstraintFieldsNotMatchError");
			ClientStrings.stringIDs.Add(3900615856U, "Updating");
			ClientStrings.stringIDs.Add(353630484U, "AdditionalPropertiesLabel");
			ClientStrings.stringIDs.Add(1011589268U, "JumpToMigrationSlabConfirmation");
			ClientStrings.stringIDs.Add(7739616U, "VoicemailCallFwdContactOperator");
			ClientStrings.stringIDs.Add(2680934369U, "InvalidValueRange");
			ClientStrings.stringIDs.Add(2697991219U, "NoActionRuleAuditSeverity");
			ClientStrings.stringIDs.Add(3273613659U, "VoicemailClearSettingsConfirmationMessage");
			ClientStrings.stringIDs.Add(1655108951U, "WebServicesExternal");
			ClientStrings.stringIDs.Add(2101584371U, "UploaderValidationError");
			ClientStrings.stringIDs.Add(3336188783U, "ServerAboutToExpireWarningText");
			ClientStrings.stringIDs.Add(3200788962U, "ConditionValueRequriedErrorMessage");
			ClientStrings.stringIDs.Add(439636863U, "PreviousePage");
			ClientStrings.stringIDs.Add(3390434404U, "Ascending");
			ClientStrings.stringIDs.Add(2068973737U, "EditSubnetCaption");
			ClientStrings.stringIDs.Add(267746805U, "ChooseAtLeastOneColumn");
			ClientStrings.stringIDs.Add(2900910704U, "EditSharingEnabledDomains");
			ClientStrings.stringIDs.Add(986397318U, "Recipients");
			ClientStrings.stringIDs.Add(2778558511U, "Back");
			ClientStrings.stringIDs.Add(3068528108U, "VoicemailPostFwdRecordGreeting");
			ClientStrings.stringIDs.Add(1857390484U, "ColumnChooseFailed");
			ClientStrings.stringIDs.Add(1219538243U, "TransportRuleBusinessContinuity");
			ClientStrings.stringIDs.Add(936826416U, "TitleSectionMobileDevices");
			ClientStrings.stringIDs.Add(2894502448U, "NewFolder");
			ClientStrings.stringIDs.Add(3265439859U, "EnableCommandText");
			ClientStrings.stringIDs.Add(583273118U, "PrimaryAddressLabel");
			ClientStrings.stringIDs.Add(3843987112U, "IncidentReportContentCustom");
			ClientStrings.stringIDs.Add(3566331407U, "MessageTraceInvalidStartDate");
			ClientStrings.stringIDs.Add(3393270247U, "InvalidDecimal1");
			ClientStrings.stringIDs.Add(2636721601U, "ActivateDBCopyConfirmation");
			ClientStrings.stringIDs.Add(1063565289U, "DisableActiveSyncConfirm");
			ClientStrings.stringIDs.Add(652753372U, "PleaseWaitWhileSaving");
			ClientStrings.stringIDs.Add(4082347473U, "AddConditionButtonText");
			ClientStrings.stringIDs.Add(3022976242U, "AcceptedDomainAuthoritativeWarning");
			ClientStrings.stringIDs.Add(3653626825U, "Editor");
			ClientStrings.stringIDs.Add(1801819654U, "PlayOnPhoneConnected");
			ClientStrings.stringIDs.Add(2762661174U, "InvalidDomainName");
			ClientStrings.stringIDs.Add(2068086983U, "SetECPAuthConfirmText");
			ClientStrings.stringIDs.Add(4262899755U, "RuleNameTextBoxLabel");
			ClientStrings.stringIDs.Add(1414245989U, "More");
			ClientStrings.stringIDs.Add(2549262094U, "FirstNameInitialLastName");
			ClientStrings.stringIDs.Add(2810364182U, "OwaMailboxPolicyCommunicationManagement");
			ClientStrings.stringIDs.Add(2731136937U, "Owner");
			ClientStrings.stringIDs.Add(1400729458U, "EditDomain");
			ClientStrings.stringIDs.Add(3134721922U, "InvalidEmailAddressName");
			ClientStrings.stringIDs.Add(467409560U, "LeadingTrailingSpaceError");
			ClientStrings.stringIDs.Add(2307360811U, "DayRangeAndTimeRangeTooltip");
			ClientStrings.stringIDs.Add(3180237931U, "OwaMailboxPolicyPremiumClient");
			ClientStrings.stringIDs.Add(1849292272U, "IncidentReportDeselectAll");
			ClientStrings.stringIDs.Add(1911032188U, "OwaMailboxPolicyNotes");
			ClientStrings.stringIDs.Add(3151174489U, "UMEnableSipActionSummary");
			ClientStrings.stringIDs.Add(1045743613U, "Reviewer");
			ClientStrings.stringIDs.Add(1761836208U, "LongRunStoppedTips");
			ClientStrings.stringIDs.Add(1259584465U, "RequestSpamDetailReportTitle");
			ClientStrings.stringIDs.Add(288995097U, "AddExceptionButtonText");
			ClientStrings.stringIDs.Add(1283935370U, "OwaMailboxPolicyRecoverDeletedItems");
			ClientStrings.stringIDs.Add(117844352U, "AddActionButtonText");
			ClientStrings.stringIDs.Add(2009538927U, "EndDateMustBeYesterday");
			ClientStrings.stringIDs.Add(3100497742U, "OwaInternal");
			ClientStrings.stringIDs.Add(2920201570U, "CheckNames");
			ClientStrings.stringIDs.Add(2752727145U, "UMHolidayScheduleHolidayStartEndDateValidationError");
			ClientStrings.stringIDs.Add(3104459904U, "InvalidMailboxSearchName");
			ClientStrings.stringIDs.Add(2559957656U, "RemoveMailboxKeepLiveID");
			ClientStrings.stringIDs.Add(3766407320U, "OwaMailboxPolicySignatures");
			ClientStrings.stringIDs.Add(3982517327U, "WorkingHoursText");
			ClientStrings.stringIDs.Add(3189265994U, "ESHL");
			ClientStrings.stringIDs.Add(1911656207U, "FileStillUploading");
			ClientStrings.stringIDs.Add(472573372U, "StartOverMailboxSearch");
			ClientStrings.stringIDs.Add(9358886U, "DisableConnectorLoggingConfirm");
			ClientStrings.stringIDs.Add(228304082U, "LongRunDataLossWarning");
			ClientStrings.stringIDs.Add(798401137U, "ActiveText");
			ClientStrings.stringIDs.Add(461710906U, "DisableMOWAConfirm");
			ClientStrings.stringIDs.Add(1400887235U, "WarningPanelSeeMoreMsg");
			ClientStrings.stringIDs.Add(2860566285U, "EnterpriseLogoutFail");
			ClientStrings.stringIDs.Add(3375594338U, "AlwaysUse");
			ClientStrings.stringIDs.Add(1271124552U, "PublishingAuthor");
			ClientStrings.stringIDs.Add(3934389177U, "CopyError");
			ClientStrings.stringIDs.Add(3980910748U, "LastNameInitialFirstName");
			ClientStrings.stringIDs.Add(2757672907U, "Internaldnslookups");
			ClientStrings.stringIDs.Add(2794407267U, "ApplyAllMessagesWarning");
			ClientStrings.stringIDs.Add(3799642099U, "EditIPAddress");
			ClientStrings.stringIDs.Add(265625760U, "SecurityGroupText");
			ClientStrings.stringIDs.Add(356602967U, "ResumeMailboxSearch");
			ClientStrings.stringIDs.Add(3688802170U, "JumpToOffice365MigrationSlabFailed");
			ClientStrings.stringIDs.Add(3738684211U, "DeliveryReportEmailBodyForMailTo");
			ClientStrings.stringIDs.Add(2929991304U, "DefaultRuleConditionLabel");
			ClientStrings.stringIDs.Add(1506239268U, "RequestDLPDetail");
			ClientStrings.stringIDs.Add(667665198U, "AuditSeverityLevelLabel");
			ClientStrings.stringIDs.Add(3800601796U, "NoneForEmpty");
			ClientStrings.stringIDs.Add(210154378U, "OwaMailboxPolicyRules");
			ClientStrings.stringIDs.Add(4035228826U, "CtrlKeyToSave");
			ClientStrings.stringIDs.Add(3041967243U, "CustomPeriodText");
			ClientStrings.stringIDs.Add(3189266341U, "NSHL");
			ClientStrings.stringIDs.Add(663637318U, "TransferToNumberText");
			ClientStrings.stringIDs.Add(2950784627U, "UMKeyMappingActionSummaryAnnounceBusinessHours");
			ClientStrings.stringIDs.Add(912527261U, "OwaMailboxPolicyFacebook");
			ClientStrings.stringIDs.Add(1929634872U, "ConnectMailboxLaunchWizard");
			ClientStrings.stringIDs.Add(280551769U, "SomeSelectionNotAdded");
			ClientStrings.stringIDs.Add(32190104U, "LoadingInformation");
			ClientStrings.stringIDs.Add(1710116876U, "ProceedWithoutTenantInfo");
			ClientStrings.stringIDs.Add(3599592070U, "Loading");
			ClientStrings.stringIDs.Add(1891837447U, "DeviceTypePickerAll");
			ClientStrings.stringIDs.Add(892936075U, "VoicemailConfigurationTitle");
			ClientStrings.stringIDs.Add(2336878616U, "RemoveDBCopyConfirmation");
			ClientStrings.stringIDs.Add(1308147703U, "MemberUpdateTypeClosed");
			ClientStrings.stringIDs.Add(2359945479U, "OnCommandText");
			ClientStrings.stringIDs.Add(779120846U, "SelectOne");
			ClientStrings.stringIDs.Add(49706295U, "NtlmAuth");
			ClientStrings.stringIDs.Add(1716565179U, "EnableMOWAConfirm");
			ClientStrings.stringIDs.Add(1502600087U, "Pop");
			ClientStrings.stringIDs.Add(882294734U, "DisabledPendingDisplayText");
			ClientStrings.stringIDs.Add(2145940917U, "FirstFocusTextForScreenReader");
			ClientStrings.stringIDs.Add(137938150U, "MoveUp");
			ClientStrings.stringIDs.Add(347463522U, "GalOrPersonalContactText");
			ClientStrings.stringIDs.Add(2903019109U, "AllAvailableIPV4Address");
			ClientStrings.stringIDs.Add(3765422820U, "OwaExternal");
			ClientStrings.stringIDs.Add(1015147527U, "ActiveSyncEnableText");
			ClientStrings.stringIDs.Add(2594574748U, "ConstraintViolationInputUnlimitedValue");
			ClientStrings.stringIDs.Add(1603757297U, "ResetPinMessageTitle");
			ClientStrings.stringIDs.Add(1496915101U, "No");
			ClientStrings.stringIDs.Add(2846873532U, "LongRunStoppedDescription");
			ClientStrings.stringIDs.Add(809706807U, "NIAE");
			ClientStrings.stringIDs.Add(116135148U, "CtrlKeyCloseForm");
			ClientStrings.stringIDs.Add(3705047298U, "InvalidUrl");
			ClientStrings.stringIDs.Add(3403459873U, "InvalidDomain");
			ClientStrings.stringIDs.Add(2852593944U, "Display");
			ClientStrings.stringIDs.Add(2814896254U, "SslTitle");
			ClientStrings.stringIDs.Add(4028691857U, "ProviderDelayed");
			ClientStrings.stringIDs.Add(3512966386U, "TrialExpiredWarningText");
			ClientStrings.stringIDs.Add(1496927839U, "StopProcessingRuleLabel");
			ClientStrings.stringIDs.Add(1872640986U, "UMEnabledPolicyRequired");
			ClientStrings.stringIDs.Add(4231482709U, "All");
			ClientStrings.stringIDs.Add(238716676U, "ClickHereForHelp");
			ClientStrings.stringIDs.Add(945688236U, "LongRunCompletedDescription");
			ClientStrings.stringIDs.Add(4193953058U, "ConnectToO365");
			ClientStrings.stringIDs.Add(2021920769U, "NoRetentionPolicy");
			ClientStrings.stringIDs.Add(995902246U, "SavingInformation");
			ClientStrings.stringIDs.Add(2939777527U, "InvalidAlias");
			ClientStrings.stringIDs.Add(1226897823U, "Contributor");
			ClientStrings.stringIDs.Add(2358390244U, "Cancel");
			ClientStrings.stringIDs.Add(3309738928U, "NoIPWarning");
			ClientStrings.stringIDs.Add(427688167U, "Custom");
			ClientStrings.stringIDs.Add(664253366U, "RemovePendingDisplayText");
			ClientStrings.stringIDs.Add(892169149U, "EnabledPendingDisplayText");
			ClientStrings.stringIDs.Add(3027332405U, "RecordGreetingLinkText");
			ClientStrings.stringIDs.Add(3423762231U, "Next");
			ClientStrings.stringIDs.Add(4164526598U, "PrimaryNavigation");
			ClientStrings.stringIDs.Add(2835676179U, "KeyMappingVoiceMailDisplayText");
			ClientStrings.stringIDs.Add(2381414750U, "WarningPanelMultipleWarningsMsg");
			ClientStrings.stringIDs.Add(67348609U, "RequestMalwareDetail");
			ClientStrings.stringIDs.Add(2193536568U, "Information");
			ClientStrings.stringIDs.Add(1749370292U, "RequestRuleDetail");
			ClientStrings.stringIDs.Add(2470451672U, "OutsideWorkingHoursText");
			ClientStrings.stringIDs.Add(1325539240U, "UMDialPlanRequiredField");
			ClientStrings.stringIDs.Add(3205750596U, "DisableFederationCompleted");
			ClientStrings.stringIDs.Add(3276462966U, "SharingRuleEntryDomainConflict");
			ClientStrings.stringIDs.Add(4005194968U, "EditSharingEnabledDomainsStep2");
			ClientStrings.stringIDs.Add(2220221264U, "NoSpaceValidatorMessage");
			ClientStrings.stringIDs.Add(3125926615U, "WaitForHybridUIReady");
			ClientStrings.stringIDs.Add(684930172U, "MailboxDelegation");
			ClientStrings.stringIDs.Add(1396631711U, "OwaMailboxPolicySecurity");
			ClientStrings.stringIDs.Add(3453349145U, "SenderRequiredErrorMessage");
			ClientStrings.stringIDs.Add(1294686955U, "ServerTrailDaysColumnText");
			ClientStrings.stringIDs.Add(1173301205U, "ResetPinMessage");
			ClientStrings.stringIDs.Add(1482742945U, "RemoveDisabledFacebookConnectionText");
			ClientStrings.stringIDs.Add(214141546U, "ReportTitleMissing");
			ClientStrings.stringIDs.Add(2593970847U, "OwaMailboxPolicyLinkedIn");
			ClientStrings.stringIDs.Add(1904327111U, "ConstraintViolationValueOutOfRange");
			ClientStrings.stringIDs.Add(3379654077U, "MTRTDetailsTitle");
			ClientStrings.stringIDs.Add(2658661820U, "Column");
			ClientStrings.stringIDs.Add(4021298411U, "ClickToShowText");
			ClientStrings.stringIDs.Add(2116880466U, "HybridConfiguration");
			ClientStrings.stringIDs.Add(2408935185U, "PWTAS");
			ClientStrings.stringIDs.Add(3243184343U, "ClickToCopyToClipboard");
			ClientStrings.stringIDs.Add(3296253953U, "UriKindAbsolute");
			ClientStrings.stringIDs.Add(1147585405U, "VoicemailResetPINConfirmationMessage");
			ClientStrings.stringIDs.Add(1513997499U, "ConnectMailboxWizardCaption");
			ClientStrings.stringIDs.Add(2506867635U, "MobileInternal");
			ClientStrings.stringIDs.Add(2184256814U, "RetrievingStatistics");
			ClientStrings.stringIDs.Add(3983756775U, "WebServiceRequestTimeout");
			ClientStrings.stringIDs.Add(3868849319U, "UMEnableTelexActionSummary");
			ClientStrings.stringIDs.Add(3698078783U, "PermissionLevelWarning");
			ClientStrings.stringIDs.Add(3774473073U, "GetAllResults");
			ClientStrings.stringIDs.Add(259417423U, "LongRunCopyToClipboardLabel");
			ClientStrings.stringIDs.Add(721624957U, "EnableFederationCompleted");
			ClientStrings.stringIDs.Add(1957457715U, "OABInternal");
			ClientStrings.stringIDs.Add(2767693563U, "OwaMailboxPolicyCalendar");
		}

		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000002 RID: 2 RVA: 0x00004B2B File Offset: 0x00002D2B
		public static string ReconnectProviderCommandText
		{
			get
			{
				return ClientStrings.ResourceManager.GetString("ReconnectProviderCommandText");
			}
		}

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000003 RID: 3 RVA: 0x00004B3C File Offset: 0x00002D3C
		public static string FieldsInError
		{
			get
			{
				return ClientStrings.ResourceManager.GetString("FieldsInError");
			}
		}

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000004 RID: 4 RVA: 0x00004B4D File Offset: 0x00002D4D
		public static string TlsTitle
		{
			get
			{
				return ClientStrings.ResourceManager.GetString("TlsTitle");
			}
		}

		// Token: 0x06000005 RID: 5 RVA: 0x00004B5E File Offset: 0x00002D5E
		public static string ModalDialgMultipleSRMsgTemplate(int n, string msg)
		{
			return string.Format(ClientStrings.ResourceManager.GetString("ModalDialgMultipleSRMsgTemplate"), n, msg);
		}

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000006 RID: 6 RVA: 0x00004B7B File Offset: 0x00002D7B
		public static string UMKeyMappingTimeout
		{
			get
			{
				return ClientStrings.ResourceManager.GetString("UMKeyMappingTimeout");
			}
		}

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x06000007 RID: 7 RVA: 0x00004B8C File Offset: 0x00002D8C
		public static string RequiredFieldValidatorErrorMessage
		{
			get
			{
				return ClientStrings.ResourceManager.GetString("RequiredFieldValidatorErrorMessage");
			}
		}

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x06000008 RID: 8 RVA: 0x00004B9D File Offset: 0x00002D9D
		public static string OwaMailboxPolicyTasks
		{
			get
			{
				return ClientStrings.ResourceManager.GetString("OwaMailboxPolicyTasks");
			}
		}

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x06000009 RID: 9 RVA: 0x00004BAE File Offset: 0x00002DAE
		public static string CopyIsIEOnly
		{
			get
			{
				return ClientStrings.ResourceManager.GetString("CopyIsIEOnly");
			}
		}

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x0600000A RID: 10 RVA: 0x00004BBF File Offset: 0x00002DBF
		public static string MinimumCriteriaFieldsInErrorDeliveryStatus
		{
			get
			{
				return ClientStrings.ResourceManager.GetString("MinimumCriteriaFieldsInErrorDeliveryStatus");
			}
		}

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x0600000B RID: 11 RVA: 0x00004BD0 File Offset: 0x00002DD0
		public static string EnterHybridUIConfirm
		{
			get
			{
				return ClientStrings.ResourceManager.GetString("EnterHybridUIConfirm");
			}
		}

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x0600000C RID: 12 RVA: 0x00004BE1 File Offset: 0x00002DE1
		public static string DisableCommandText
		{
			get
			{
				return ClientStrings.ResourceManager.GetString("DisableCommandText");
			}
		}

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x0600000D RID: 13 RVA: 0x00004BF2 File Offset: 0x00002DF2
		public static string UMHolidayScheduleHolidayStartDateRequiredError
		{
			get
			{
				return ClientStrings.ResourceManager.GetString("UMHolidayScheduleHolidayStartDateRequiredError");
			}
		}

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x0600000E RID: 14 RVA: 0x00004C03 File Offset: 0x00002E03
		public static string EnterHybridUIButtonText
		{
			get
			{
				return ClientStrings.ResourceManager.GetString("EnterHybridUIButtonText");
			}
		}

		// Token: 0x0600000F RID: 15 RVA: 0x00004C14 File Offset: 0x00002E14
		public static string ErrorMessageInvalidInteger(string str)
		{
			return string.Format(ClientStrings.ResourceManager.GetString("ErrorMessageInvalidInteger"), str);
		}

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x06000010 RID: 16 RVA: 0x00004C2B File Offset: 0x00002E2B
		public static string ConstraintViolationValueOutOfRangeForQuota
		{
			get
			{
				return ClientStrings.ResourceManager.GetString("ConstraintViolationValueOutOfRangeForQuota");
			}
		}

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x06000011 RID: 17 RVA: 0x00004C3C File Offset: 0x00002E3C
		public static string HydratingMessage
		{
			get
			{
				return ClientStrings.ResourceManager.GetString("HydratingMessage");
			}
		}

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x06000012 RID: 18 RVA: 0x00004C4D File Offset: 0x00002E4D
		public static string GroupNamingPolicyPreviewDescriptionHeader
		{
			get
			{
				return ClientStrings.ResourceManager.GetString("GroupNamingPolicyPreviewDescriptionHeader");
			}
		}

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x06000013 RID: 19 RVA: 0x00004C5E File Offset: 0x00002E5E
		public static string Validating
		{
			get
			{
				return ClientStrings.ResourceManager.GetString("Validating");
			}
		}

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x06000014 RID: 20 RVA: 0x00004C6F File Offset: 0x00002E6F
		public static string UriKindRelative
		{
			get
			{
				return ClientStrings.ResourceManager.GetString("UriKindRelative");
			}
		}

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x06000015 RID: 21 RVA: 0x00004C80 File Offset: 0x00002E80
		public static string Close
		{
			get
			{
				return ClientStrings.ResourceManager.GetString("Close");
			}
		}

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x06000016 RID: 22 RVA: 0x00004C91 File Offset: 0x00002E91
		public static string MoveDown
		{
			get
			{
				return ClientStrings.ResourceManager.GetString("MoveDown");
			}
		}

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x06000017 RID: 23 RVA: 0x00004CA2 File Offset: 0x00002EA2
		public static string PeopleConnectBusy
		{
			get
			{
				return ClientStrings.ResourceManager.GetString("PeopleConnectBusy");
			}
		}

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x06000018 RID: 24 RVA: 0x00004CB3 File Offset: 0x00002EB3
		public static string LegacyRegExEnabledRuleLabel
		{
			get
			{
				return ClientStrings.ResourceManager.GetString("LegacyRegExEnabledRuleLabel");
			}
		}

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x06000019 RID: 25 RVA: 0x00004CC4 File Offset: 0x00002EC4
		public static string HydrationDataLossWarning
		{
			get
			{
				return ClientStrings.ResourceManager.GetString("HydrationDataLossWarning");
			}
		}

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x0600001A RID: 26 RVA: 0x00004CD5 File Offset: 0x00002ED5
		public static string NoTreeItem
		{
			get
			{
				return ClientStrings.ResourceManager.GetString("NoTreeItem");
			}
		}

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x0600001B RID: 27 RVA: 0x00004CE6 File Offset: 0x00002EE6
		public static string LearnMore
		{
			get
			{
				return ClientStrings.ResourceManager.GetString("LearnMore");
			}
		}

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x0600001C RID: 28 RVA: 0x00004CF7 File Offset: 0x00002EF7
		public static string AddEAPCondtionButtonText
		{
			get
			{
				return ClientStrings.ResourceManager.GetString("AddEAPCondtionButtonText");
			}
		}

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x0600001D RID: 29 RVA: 0x00004D08 File Offset: 0x00002F08
		public static string InvalidDateRange
		{
			get
			{
				return ClientStrings.ResourceManager.GetString("InvalidDateRange");
			}
		}

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x0600001E RID: 30 RVA: 0x00004D19 File Offset: 0x00002F19
		public static string DefaultRuleEditorCaption
		{
			get
			{
				return ClientStrings.ResourceManager.GetString("DefaultRuleEditorCaption");
			}
		}

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x0600001F RID: 31 RVA: 0x00004D2A File Offset: 0x00002F2A
		public static string CtrlKeyGoToSearch
		{
			get
			{
				return ClientStrings.ResourceManager.GetString("CtrlKeyGoToSearch");
			}
		}

		// Token: 0x1700001D RID: 29
		// (get) Token: 0x06000020 RID: 32 RVA: 0x00004D3B File Offset: 0x00002F3B
		public static string DisableFVA
		{
			get
			{
				return ClientStrings.ResourceManager.GetString("DisableFVA");
			}
		}

		// Token: 0x1700001E RID: 30
		// (get) Token: 0x06000021 RID: 33 RVA: 0x00004D4C File Offset: 0x00002F4C
		public static string Searching
		{
			get
			{
				return ClientStrings.ResourceManager.GetString("Searching");
			}
		}

		// Token: 0x06000022 RID: 34 RVA: 0x00004D5D File Offset: 0x00002F5D
		public static string ModalDialogSRHelpTemplate(string help)
		{
			return string.Format(ClientStrings.ResourceManager.GetString("ModalDialogSRHelpTemplate"), help);
		}

		// Token: 0x1700001F RID: 31
		// (get) Token: 0x06000023 RID: 35 RVA: 0x00004D74 File Offset: 0x00002F74
		public static string Update
		{
			get
			{
				return ClientStrings.ResourceManager.GetString("Update");
			}
		}

		// Token: 0x17000020 RID: 32
		// (get) Token: 0x06000024 RID: 36 RVA: 0x00004D85 File Offset: 0x00002F85
		public static string CurrentPolicyCaption
		{
			get
			{
				return ClientStrings.ResourceManager.GetString("CurrentPolicyCaption");
			}
		}

		// Token: 0x06000025 RID: 37 RVA: 0x00004D96 File Offset: 0x00002F96
		public static string RemoveCertificateConfirm(string name, string server)
		{
			return string.Format(ClientStrings.ResourceManager.GetString("RemoveCertificateConfirm"), name, server);
		}

		// Token: 0x17000021 RID: 33
		// (get) Token: 0x06000026 RID: 38 RVA: 0x00004DAE File Offset: 0x00002FAE
		public static string FollowedByColon
		{
			get
			{
				return ClientStrings.ResourceManager.GetString("FollowedByColon");
			}
		}

		// Token: 0x17000022 RID: 34
		// (get) Token: 0x06000027 RID: 39 RVA: 0x00004DBF File Offset: 0x00002FBF
		public static string EnabledDisplayText
		{
			get
			{
				return ClientStrings.ResourceManager.GetString("EnabledDisplayText");
			}
		}

		// Token: 0x17000023 RID: 35
		// (get) Token: 0x06000028 RID: 40 RVA: 0x00004DD0 File Offset: 0x00002FD0
		public static string OffDisplayText
		{
			get
			{
				return ClientStrings.ResourceManager.GetString("OffDisplayText");
			}
		}

		// Token: 0x17000024 RID: 36
		// (get) Token: 0x06000029 RID: 41 RVA: 0x00004DE1 File Offset: 0x00002FE1
		public static string OwaMailboxPolicyActiveSyncIntegration
		{
			get
			{
				return ClientStrings.ResourceManager.GetString("OwaMailboxPolicyActiveSyncIntegration");
			}
		}

		// Token: 0x17000025 RID: 37
		// (get) Token: 0x0600002A RID: 42 RVA: 0x00004DF2 File Offset: 0x00002FF2
		public static string PlayOnPhoneDisconnecting
		{
			get
			{
				return ClientStrings.ResourceManager.GetString("PlayOnPhoneDisconnecting");
			}
		}

		// Token: 0x0600002B RID: 43 RVA: 0x00004E03 File Offset: 0x00003003
		public static string QueryValueStartsWithStar(string queryName)
		{
			return string.Format(ClientStrings.ResourceManager.GetString("QueryValueStartsWithStar"), queryName);
		}

		// Token: 0x17000026 RID: 38
		// (get) Token: 0x0600002C RID: 44 RVA: 0x00004E1A File Offset: 0x0000301A
		public static string LegacyOUError
		{
			get
			{
				return ClientStrings.ResourceManager.GetString("LegacyOUError");
			}
		}

		// Token: 0x17000027 RID: 39
		// (get) Token: 0x0600002D RID: 45 RVA: 0x00004E2B File Offset: 0x0000302B
		public static string WebServiceRequestServerError
		{
			get
			{
				return ClientStrings.ResourceManager.GetString("WebServiceRequestServerError");
			}
		}

		// Token: 0x17000028 RID: 40
		// (get) Token: 0x0600002E RID: 46 RVA: 0x00004E3C File Offset: 0x0000303C
		public static string Warning
		{
			get
			{
				return ClientStrings.ResourceManager.GetString("Warning");
			}
		}

		// Token: 0x17000029 RID: 41
		// (get) Token: 0x0600002F RID: 47 RVA: 0x00004E4D File Offset: 0x0000304D
		public static string BlockedPendingDisplayText
		{
			get
			{
				return ClientStrings.ResourceManager.GetString("BlockedPendingDisplayText");
			}
		}

		// Token: 0x1700002A RID: 42
		// (get) Token: 0x06000030 RID: 48 RVA: 0x00004E5E File Offset: 0x0000305E
		public static string MessageTypePickerInvalid
		{
			get
			{
				return ClientStrings.ResourceManager.GetString("MessageTypePickerInvalid");
			}
		}

		// Token: 0x1700002B RID: 43
		// (get) Token: 0x06000031 RID: 49 RVA: 0x00004E6F File Offset: 0x0000306F
		public static string OwaMailboxPolicyTextMessaging
		{
			get
			{
				return ClientStrings.ResourceManager.GetString("OwaMailboxPolicyTextMessaging");
			}
		}

		// Token: 0x1700002C RID: 44
		// (get) Token: 0x06000032 RID: 50 RVA: 0x00004E80 File Offset: 0x00003080
		public static string OwaMailboxPolicyContacts
		{
			get
			{
				return ClientStrings.ResourceManager.GetString("OwaMailboxPolicyContacts");
			}
		}

		// Token: 0x1700002D RID: 45
		// (get) Token: 0x06000033 RID: 51 RVA: 0x00004E91 File Offset: 0x00003091
		public static string Expand
		{
			get
			{
				return ClientStrings.ResourceManager.GetString("Expand");
			}
		}

		// Token: 0x06000034 RID: 52 RVA: 0x00004EA2 File Offset: 0x000030A2
		public static string InvalidInteger(string str)
		{
			return string.Format(ClientStrings.ResourceManager.GetString("InvalidInteger"), str);
		}

		// Token: 0x1700002E RID: 46
		// (get) Token: 0x06000035 RID: 53 RVA: 0x00004EB9 File Offset: 0x000030B9
		public static string DisableConnectorConfirm
		{
			get
			{
				return ClientStrings.ResourceManager.GetString("DisableConnectorConfirm");
			}
		}

		// Token: 0x1700002F RID: 47
		// (get) Token: 0x06000036 RID: 54 RVA: 0x00004ECA File Offset: 0x000030CA
		public static string CmdLogTitleForHybridEnterprise
		{
			get
			{
				return ClientStrings.ResourceManager.GetString("CmdLogTitleForHybridEnterprise");
			}
		}

		// Token: 0x17000030 RID: 48
		// (get) Token: 0x06000037 RID: 55 RVA: 0x00004EDB File Offset: 0x000030DB
		public static string ProviderConnectedWithError
		{
			get
			{
				return ClientStrings.ResourceManager.GetString("ProviderConnectedWithError");
			}
		}

		// Token: 0x17000031 RID: 49
		// (get) Token: 0x06000038 RID: 56 RVA: 0x00004EEC File Offset: 0x000030EC
		public static string RequestSpamDetail
		{
			get
			{
				return ClientStrings.ResourceManager.GetString("RequestSpamDetail");
			}
		}

		// Token: 0x17000032 RID: 50
		// (get) Token: 0x06000039 RID: 57 RVA: 0x00004EFD File Offset: 0x000030FD
		public static string None
		{
			get
			{
				return ClientStrings.ResourceManager.GetString("None");
			}
		}

		// Token: 0x17000033 RID: 51
		// (get) Token: 0x0600003A RID: 58 RVA: 0x00004F0E File Offset: 0x0000310E
		public static string PassiveText
		{
			get
			{
				return ClientStrings.ResourceManager.GetString("PassiveText");
			}
		}

		// Token: 0x17000034 RID: 52
		// (get) Token: 0x0600003B RID: 59 RVA: 0x00004F1F File Offset: 0x0000311F
		public static string DisableFederationInProgress
		{
			get
			{
				return ClientStrings.ResourceManager.GetString("DisableFederationInProgress");
			}
		}

		// Token: 0x17000035 RID: 53
		// (get) Token: 0x0600003C RID: 60 RVA: 0x00004F30 File Offset: 0x00003130
		public static string MobileDeviceDisableText
		{
			get
			{
				return ClientStrings.ResourceManager.GetString("MobileDeviceDisableText");
			}
		}

		// Token: 0x17000036 RID: 54
		// (get) Token: 0x0600003D RID: 61 RVA: 0x00004F41 File Offset: 0x00003141
		public static string MoreOptions
		{
			get
			{
				return ClientStrings.ResourceManager.GetString("MoreOptions");
			}
		}

		// Token: 0x17000037 RID: 55
		// (get) Token: 0x0600003E RID: 62 RVA: 0x00004F52 File Offset: 0x00003152
		public static string MidnightAM
		{
			get
			{
				return ClientStrings.ResourceManager.GetString("MidnightAM");
			}
		}

		// Token: 0x17000038 RID: 56
		// (get) Token: 0x0600003F RID: 63 RVA: 0x00004F63 File Offset: 0x00003163
		public static string NotificationCount
		{
			get
			{
				return ClientStrings.ResourceManager.GetString("NotificationCount");
			}
		}

		// Token: 0x17000039 RID: 57
		// (get) Token: 0x06000040 RID: 64 RVA: 0x00004F74 File Offset: 0x00003174
		public static string ContactsSharing
		{
			get
			{
				return ClientStrings.ResourceManager.GetString("ContactsSharing");
			}
		}

		// Token: 0x1700003A RID: 58
		// (get) Token: 0x06000041 RID: 65 RVA: 0x00004F85 File Offset: 0x00003185
		public static string LongRunInProgressDescription
		{
			get
			{
				return ClientStrings.ResourceManager.GetString("LongRunInProgressDescription");
			}
		}

		// Token: 0x1700003B RID: 59
		// (get) Token: 0x06000042 RID: 66 RVA: 0x00004F96 File Offset: 0x00003196
		public static string MailboxToSearchRequiredErrorMessage
		{
			get
			{
				return ClientStrings.ResourceManager.GetString("MailboxToSearchRequiredErrorMessage");
			}
		}

		// Token: 0x1700003C RID: 60
		// (get) Token: 0x06000043 RID: 67 RVA: 0x00004FA7 File Offset: 0x000031A7
		public static string DomainNoValue
		{
			get
			{
				return ClientStrings.ResourceManager.GetString("DomainNoValue");
			}
		}

		// Token: 0x06000044 RID: 68 RVA: 0x00004FB8 File Offset: 0x000031B8
		public static string InvalidCharacter(string str)
		{
			return string.Format(ClientStrings.ResourceManager.GetString("InvalidCharacter"), str);
		}

		// Token: 0x1700003D RID: 61
		// (get) Token: 0x06000045 RID: 69 RVA: 0x00004FCF File Offset: 0x000031CF
		public static string MyOptions
		{
			get
			{
				return ClientStrings.ResourceManager.GetString("MyOptions");
			}
		}

		// Token: 0x1700003E RID: 62
		// (get) Token: 0x06000046 RID: 70 RVA: 0x00004FE0 File Offset: 0x000031E0
		public static string VoicemailConfigurationDetails
		{
			get
			{
				return ClientStrings.ResourceManager.GetString("VoicemailConfigurationDetails");
			}
		}

		// Token: 0x06000047 RID: 71 RVA: 0x00004FF1 File Offset: 0x000031F1
		public static string RenewCertificateHelp(string name)
		{
			return string.Format(ClientStrings.ResourceManager.GetString("RenewCertificateHelp"), name);
		}

		// Token: 0x1700003F RID: 63
		// (get) Token: 0x06000048 RID: 72 RVA: 0x00005008 File Offset: 0x00003208
		public static string CloseWindowOnLogout
		{
			get
			{
				return ClientStrings.ResourceManager.GetString("CloseWindowOnLogout");
			}
		}

		// Token: 0x17000040 RID: 64
		// (get) Token: 0x06000049 RID: 73 RVA: 0x00005019 File Offset: 0x00003219
		public static string CustomizeColumns
		{
			get
			{
				return ClientStrings.ResourceManager.GetString("CustomizeColumns");
			}
		}

		// Token: 0x17000041 RID: 65
		// (get) Token: 0x0600004A RID: 74 RVA: 0x0000502A File Offset: 0x0000322A
		public static string EnterProductKey
		{
			get
			{
				return ClientStrings.ResourceManager.GetString("EnterProductKey");
			}
		}

		// Token: 0x17000042 RID: 66
		// (get) Token: 0x0600004B RID: 75 RVA: 0x0000503B File Offset: 0x0000323B
		public static string PlayOnPhoneDialing
		{
			get
			{
				return ClientStrings.ResourceManager.GetString("PlayOnPhoneDialing");
			}
		}

		// Token: 0x17000043 RID: 67
		// (get) Token: 0x0600004C RID: 76 RVA: 0x0000504C File Offset: 0x0000324C
		public static string OwaMailboxPolicyUMIntegration
		{
			get
			{
				return ClientStrings.ResourceManager.GetString("OwaMailboxPolicyUMIntegration");
			}
		}

		// Token: 0x17000044 RID: 68
		// (get) Token: 0x0600004D RID: 77 RVA: 0x0000505D File Offset: 0x0000325D
		public static string HydrationFailed
		{
			get
			{
				return ClientStrings.ResourceManager.GetString("HydrationFailed");
			}
		}

		// Token: 0x17000045 RID: 69
		// (get) Token: 0x0600004E RID: 78 RVA: 0x0000506E File Offset: 0x0000326E
		public static string EnableActiveSyncConfirm
		{
			get
			{
				return ClientStrings.ResourceManager.GetString("EnableActiveSyncConfirm");
			}
		}

		// Token: 0x17000046 RID: 70
		// (get) Token: 0x0600004F RID: 79 RVA: 0x0000507F File Offset: 0x0000327F
		public static string ConstraintViolationStringLengthIsEmpty
		{
			get
			{
				return ClientStrings.ResourceManager.GetString("ConstraintViolationStringLengthIsEmpty");
			}
		}

		// Token: 0x17000047 RID: 71
		// (get) Token: 0x06000050 RID: 80 RVA: 0x00005090 File Offset: 0x00003290
		public static string SelectOneLink
		{
			get
			{
				return ClientStrings.ResourceManager.GetString("SelectOneLink");
			}
		}

		// Token: 0x06000051 RID: 81 RVA: 0x000050A1 File Offset: 0x000032A1
		public static string CancelForAjaxUploader(string str)
		{
			return string.Format(ClientStrings.ResourceManager.GetString("CancelForAjaxUploader"), str);
		}

		// Token: 0x17000048 RID: 72
		// (get) Token: 0x06000052 RID: 82 RVA: 0x000050B8 File Offset: 0x000032B8
		public static string ConstraintNotNullOrEmpty
		{
			get
			{
				return ClientStrings.ResourceManager.GetString("ConstraintNotNullOrEmpty");
			}
		}

		// Token: 0x06000053 RID: 83 RVA: 0x000050C9 File Offset: 0x000032C9
		public static string UMKeyMappingActionSummaryTransferToAA(string name)
		{
			return string.Format(ClientStrings.ResourceManager.GetString("UMKeyMappingActionSummaryTransferToAA"), name);
		}

		// Token: 0x17000049 RID: 73
		// (get) Token: 0x06000054 RID: 84 RVA: 0x000050E0 File Offset: 0x000032E0
		public static string LitigationHoldOwnerNotSet
		{
			get
			{
				return ClientStrings.ResourceManager.GetString("LitigationHoldOwnerNotSet");
			}
		}

		// Token: 0x1700004A RID: 74
		// (get) Token: 0x06000055 RID: 85 RVA: 0x000050F1 File Offset: 0x000032F1
		public static string RequiredFieldIndicator
		{
			get
			{
				return ClientStrings.ResourceManager.GetString("RequiredFieldIndicator");
			}
		}

		// Token: 0x1700004B RID: 75
		// (get) Token: 0x06000056 RID: 86 RVA: 0x00005102 File Offset: 0x00003302
		public static string FolderTree
		{
			get
			{
				return ClientStrings.ResourceManager.GetString("FolderTree");
			}
		}

		// Token: 0x1700004C RID: 76
		// (get) Token: 0x06000057 RID: 87 RVA: 0x00005113 File Offset: 0x00003313
		public static string IncidentReportSelectAll
		{
			get
			{
				return ClientStrings.ResourceManager.GetString("IncidentReportSelectAll");
			}
		}

		// Token: 0x06000058 RID: 88 RVA: 0x00005124 File Offset: 0x00003324
		public static string UMKeyMappingActionSummaryTransferToExtension(string str)
		{
			return string.Format(ClientStrings.ResourceManager.GetString("UMKeyMappingActionSummaryTransferToExtension"), str);
		}

		// Token: 0x06000059 RID: 89 RVA: 0x0000513B File Offset: 0x0000333B
		public static string AutoProvisionFailedMsg(string msg)
		{
			return string.Format(ClientStrings.ResourceManager.GetString("AutoProvisionFailedMsg"), msg);
		}

		// Token: 0x1700004D RID: 77
		// (get) Token: 0x0600005A RID: 90 RVA: 0x00005152 File Offset: 0x00003352
		public static string Notification
		{
			get
			{
				return ClientStrings.ResourceManager.GetString("Notification");
			}
		}

		// Token: 0x1700004E RID: 78
		// (get) Token: 0x0600005B RID: 91 RVA: 0x00005163 File Offset: 0x00003363
		public static string HydrationDoneFeatureFailed
		{
			get
			{
				return ClientStrings.ResourceManager.GetString("HydrationDoneFeatureFailed");
			}
		}

		// Token: 0x0600005C RID: 92 RVA: 0x00005174 File Offset: 0x00003374
		public static string NameFromFirstInitialsLastName(string firstName, string initials, string lastName)
		{
			return string.Format(ClientStrings.ResourceManager.GetString("NameFromFirstInitialsLastName"), firstName, initials, lastName);
		}

		// Token: 0x1700004F RID: 79
		// (get) Token: 0x0600005D RID: 93 RVA: 0x0000518D File Offset: 0x0000338D
		public static string LongRunWarningLabel
		{
			get
			{
				return ClientStrings.ResourceManager.GetString("LongRunWarningLabel");
			}
		}

		// Token: 0x0600005E RID: 94 RVA: 0x0000519E File Offset: 0x0000339E
		public static string WipeConfirmTitle(string model)
		{
			return string.Format(ClientStrings.ResourceManager.GetString("WipeConfirmTitle"), model);
		}

		// Token: 0x17000050 RID: 80
		// (get) Token: 0x0600005F RID: 95 RVA: 0x000051B5 File Offset: 0x000033B5
		public static string PublicFoldersEmptyDataTextRoot
		{
			get
			{
				return ClientStrings.ResourceManager.GetString("PublicFoldersEmptyDataTextRoot");
			}
		}

		// Token: 0x17000051 RID: 81
		// (get) Token: 0x06000060 RID: 96 RVA: 0x000051C6 File Offset: 0x000033C6
		public static string Unsuccessful
		{
			get
			{
				return ClientStrings.ResourceManager.GetString("Unsuccessful");
			}
		}

		// Token: 0x17000052 RID: 82
		// (get) Token: 0x06000061 RID: 97 RVA: 0x000051D7 File Offset: 0x000033D7
		public static string TextMessagingNotificationNotSetupText
		{
			get
			{
				return ClientStrings.ResourceManager.GetString("TextMessagingNotificationNotSetupText");
			}
		}

		// Token: 0x17000053 RID: 83
		// (get) Token: 0x06000062 RID: 98 RVA: 0x000051E8 File Offset: 0x000033E8
		public static string VoicemailConfigurationConfirmationMessage
		{
			get
			{
				return ClientStrings.ResourceManager.GetString("VoicemailConfigurationConfirmationMessage");
			}
		}

		// Token: 0x17000054 RID: 84
		// (get) Token: 0x06000063 RID: 99 RVA: 0x000051F9 File Offset: 0x000033F9
		public static string EnableFederationInProgress
		{
			get
			{
				return ClientStrings.ResourceManager.GetString("EnableFederationInProgress");
			}
		}

		// Token: 0x17000055 RID: 85
		// (get) Token: 0x06000064 RID: 100 RVA: 0x0000520A File Offset: 0x0000340A
		public static string OwaMailboxPolicyAllowCopyContactsToDeviceAddressBook
		{
			get
			{
				return ClientStrings.ResourceManager.GetString("OwaMailboxPolicyAllowCopyContactsToDeviceAddressBook");
			}
		}

		// Token: 0x17000056 RID: 86
		// (get) Token: 0x06000065 RID: 101 RVA: 0x0000521B File Offset: 0x0000341B
		public static string OwaMailboxPolicyInformationManagement
		{
			get
			{
				return ClientStrings.ResourceManager.GetString("OwaMailboxPolicyInformationManagement");
			}
		}

		// Token: 0x17000057 RID: 87
		// (get) Token: 0x06000066 RID: 102 RVA: 0x0000522C File Offset: 0x0000342C
		public static string WarningPanelDisMissMsg
		{
			get
			{
				return ClientStrings.ResourceManager.GetString("WarningPanelDisMissMsg");
			}
		}

		// Token: 0x17000058 RID: 88
		// (get) Token: 0x06000067 RID: 103 RVA: 0x0000523D File Offset: 0x0000343D
		public static string OwaMailboxPolicyJournal
		{
			get
			{
				return ClientStrings.ResourceManager.GetString("OwaMailboxPolicyJournal");
			}
		}

		// Token: 0x17000059 RID: 89
		// (get) Token: 0x06000068 RID: 104 RVA: 0x0000524E File Offset: 0x0000344E
		public static string DatesNotDefined
		{
			get
			{
				return ClientStrings.ResourceManager.GetString("DatesNotDefined");
			}
		}

		// Token: 0x1700005A RID: 90
		// (get) Token: 0x06000069 RID: 105 RVA: 0x0000525F File Offset: 0x0000345F
		public static string EnableOWAConfirm
		{
			get
			{
				return ClientStrings.ResourceManager.GetString("EnableOWAConfirm");
			}
		}

		// Token: 0x1700005B RID: 91
		// (get) Token: 0x0600006A RID: 106 RVA: 0x00005270 File Offset: 0x00003470
		public static string CancelWipePendingDisplayText
		{
			get
			{
				return ClientStrings.ResourceManager.GetString("CancelWipePendingDisplayText");
			}
		}

		// Token: 0x1700005C RID: 92
		// (get) Token: 0x0600006B RID: 107 RVA: 0x00005281 File Offset: 0x00003481
		public static string DeliveryReportSearchFieldsInError
		{
			get
			{
				return ClientStrings.ResourceManager.GetString("DeliveryReportSearchFieldsInError");
			}
		}

		// Token: 0x1700005D RID: 93
		// (get) Token: 0x0600006C RID: 108 RVA: 0x00005292 File Offset: 0x00003492
		public static string MyOrganization
		{
			get
			{
				return ClientStrings.ResourceManager.GetString("MyOrganization");
			}
		}

		// Token: 0x1700005E RID: 94
		// (get) Token: 0x0600006D RID: 109 RVA: 0x000052A3 File Offset: 0x000034A3
		public static string Today
		{
			get
			{
				return ClientStrings.ResourceManager.GetString("Today");
			}
		}

		// Token: 0x1700005F RID: 95
		// (get) Token: 0x0600006E RID: 110 RVA: 0x000052B4 File Offset: 0x000034B4
		public static string ExtendedReportsInsufficientData
		{
			get
			{
				return ClientStrings.ResourceManager.GetString("ExtendedReportsInsufficientData");
			}
		}

		// Token: 0x17000060 RID: 96
		// (get) Token: 0x0600006F RID: 111 RVA: 0x000052C5 File Offset: 0x000034C5
		public static string EnableConnectorLoggingConfirm
		{
			get
			{
				return ClientStrings.ResourceManager.GetString("EnableConnectorLoggingConfirm");
			}
		}

		// Token: 0x17000061 RID: 97
		// (get) Token: 0x06000070 RID: 112 RVA: 0x000052D6 File Offset: 0x000034D6
		public static string MessageTraceInvalidEndDate
		{
			get
			{
				return ClientStrings.ResourceManager.GetString("MessageTraceInvalidEndDate");
			}
		}

		// Token: 0x06000071 RID: 113 RVA: 0x000052E7 File Offset: 0x000034E7
		public static string BulkEditProgressNoSuccessFailedCount(int m)
		{
			return string.Format(ClientStrings.ResourceManager.GetString("BulkEditProgressNoSuccessFailedCount"), m);
		}

		// Token: 0x17000062 RID: 98
		// (get) Token: 0x06000072 RID: 114 RVA: 0x00005303 File Offset: 0x00003503
		public static string AddSubnetCaption
		{
			get
			{
				return ClientStrings.ResourceManager.GetString("AddSubnetCaption");
			}
		}

		// Token: 0x17000063 RID: 99
		// (get) Token: 0x06000073 RID: 115 RVA: 0x00005314 File Offset: 0x00003514
		public static string CustomizeSenderLabel
		{
			get
			{
				return ClientStrings.ResourceManager.GetString("CustomizeSenderLabel");
			}
		}

		// Token: 0x17000064 RID: 100
		// (get) Token: 0x06000074 RID: 116 RVA: 0x00005325 File Offset: 0x00003525
		public static string SharedUMAutoAttendantPilotIdentifierListE164Error
		{
			get
			{
				return ClientStrings.ResourceManager.GetString("SharedUMAutoAttendantPilotIdentifierListE164Error");
			}
		}

		// Token: 0x17000065 RID: 101
		// (get) Token: 0x06000075 RID: 117 RVA: 0x00005336 File Offset: 0x00003536
		public static string PreviousMonth
		{
			get
			{
				return ClientStrings.ResourceManager.GetString("PreviousMonth");
			}
		}

		// Token: 0x06000076 RID: 118 RVA: 0x00005347 File Offset: 0x00003547
		public static string InvalidNumberValue(long minValue, long maxValue)
		{
			return string.Format(ClientStrings.ResourceManager.GetString("InvalidNumberValue"), minValue, maxValue);
		}

		// Token: 0x17000066 RID: 102
		// (get) Token: 0x06000077 RID: 119 RVA: 0x00005369 File Offset: 0x00003569
		public static string Stop
		{
			get
			{
				return ClientStrings.ResourceManager.GetString("Stop");
			}
		}

		// Token: 0x17000067 RID: 103
		// (get) Token: 0x06000078 RID: 120 RVA: 0x0000537A File Offset: 0x0000357A
		public static string AllAvailableIPV6Address
		{
			get
			{
				return ClientStrings.ResourceManager.GetString("AllAvailableIPV6Address");
			}
		}

		// Token: 0x17000068 RID: 104
		// (get) Token: 0x06000079 RID: 121 RVA: 0x0000538B File Offset: 0x0000358B
		public static string LetCallersInterruptGreetingsText
		{
			get
			{
				return ClientStrings.ResourceManager.GetString("LetCallersInterruptGreetingsText");
			}
		}

		// Token: 0x0600007A RID: 122 RVA: 0x0000539C File Offset: 0x0000359C
		public static string ClearForAjaxUploader(string str1, string str2)
		{
			return string.Format(ClientStrings.ResourceManager.GetString("ClearForAjaxUploader"), str1, str2);
		}

		// Token: 0x17000069 RID: 105
		// (get) Token: 0x0600007B RID: 123 RVA: 0x000053B4 File Offset: 0x000035B4
		public static string GroupNamingPolicyEditorPrefixLabel
		{
			get
			{
				return ClientStrings.ResourceManager.GetString("GroupNamingPolicyEditorPrefixLabel");
			}
		}

		// Token: 0x0600007C RID: 124 RVA: 0x000053C5 File Offset: 0x000035C5
		public static string DeliveryReportEmailBody(string url)
		{
			return string.Format(ClientStrings.ResourceManager.GetString("DeliveryReportEmailBody"), url);
		}

		// Token: 0x1700006A RID: 106
		// (get) Token: 0x0600007D RID: 125 RVA: 0x000053DC File Offset: 0x000035DC
		public static string Transferred
		{
			get
			{
				return ClientStrings.ResourceManager.GetString("Transferred");
			}
		}

		// Token: 0x1700006B RID: 107
		// (get) Token: 0x0600007E RID: 126 RVA: 0x000053ED File Offset: 0x000035ED
		public static string NewDomain
		{
			get
			{
				return ClientStrings.ResourceManager.GetString("NewDomain");
			}
		}

		// Token: 0x1700006C RID: 108
		// (get) Token: 0x0600007F RID: 127 RVA: 0x000053FE File Offset: 0x000035FE
		public static string PublicFoldersEmptyDataTextChildren
		{
			get
			{
				return ClientStrings.ResourceManager.GetString("PublicFoldersEmptyDataTextChildren");
			}
		}

		// Token: 0x06000080 RID: 128 RVA: 0x0000540F File Offset: 0x0000360F
		public static string WrongExtension(string exts)
		{
			return string.Format(ClientStrings.ResourceManager.GetString("WrongExtension"), exts);
		}

		// Token: 0x1700006D RID: 109
		// (get) Token: 0x06000081 RID: 129 RVA: 0x00005426 File Offset: 0x00003626
		public static string FacebookDelayed
		{
			get
			{
				return ClientStrings.ResourceManager.GetString("FacebookDelayed");
			}
		}

		// Token: 0x1700006E RID: 110
		// (get) Token: 0x06000082 RID: 130 RVA: 0x00005437 File Offset: 0x00003637
		public static string Collapse
		{
			get
			{
				return ClientStrings.ResourceManager.GetString("Collapse");
			}
		}

		// Token: 0x1700006F RID: 111
		// (get) Token: 0x06000083 RID: 131 RVA: 0x00005448 File Offset: 0x00003648
		public static string GroupNamingPolicyEditorSuffixLabel
		{
			get
			{
				return ClientStrings.ResourceManager.GetString("GroupNamingPolicyEditorSuffixLabel");
			}
		}

		// Token: 0x17000070 RID: 112
		// (get) Token: 0x06000084 RID: 132 RVA: 0x00005459 File Offset: 0x00003659
		public static string MessageFontSampleText
		{
			get
			{
				return ClientStrings.ResourceManager.GetString("MessageFontSampleText");
			}
		}

		// Token: 0x17000071 RID: 113
		// (get) Token: 0x06000085 RID: 133 RVA: 0x0000546A File Offset: 0x0000366A
		public static string HideModalDialogErrorReport
		{
			get
			{
				return ClientStrings.ResourceManager.GetString("HideModalDialogErrorReport");
			}
		}

		// Token: 0x17000072 RID: 114
		// (get) Token: 0x06000086 RID: 134 RVA: 0x0000547B File Offset: 0x0000367B
		public static string JobSubmissionWaitText
		{
			get
			{
				return ClientStrings.ResourceManager.GetString("JobSubmissionWaitText");
			}
		}

		// Token: 0x06000087 RID: 135 RVA: 0x0000548C File Offset: 0x0000368C
		public static string ConstraintViolationLocalLongFullPath(string str)
		{
			return string.Format(ClientStrings.ResourceManager.GetString("ConstraintViolationLocalLongFullPath"), str);
		}

		// Token: 0x06000088 RID: 136 RVA: 0x000054A3 File Offset: 0x000036A3
		public static string ConstraintViolationUNCFilePath(string str)
		{
			return string.Format(ClientStrings.ResourceManager.GetString("ConstraintViolationUNCFilePath"), str);
		}

		// Token: 0x17000073 RID: 115
		// (get) Token: 0x06000089 RID: 137 RVA: 0x000054BA File Offset: 0x000036BA
		public static string ServiceNone
		{
			get
			{
				return ClientStrings.ResourceManager.GetString("ServiceNone");
			}
		}

		// Token: 0x17000074 RID: 116
		// (get) Token: 0x0600008A RID: 138 RVA: 0x000054CB File Offset: 0x000036CB
		public static string Page
		{
			get
			{
				return ClientStrings.ResourceManager.GetString("Page");
			}
		}

		// Token: 0x17000075 RID: 117
		// (get) Token: 0x0600008B RID: 139 RVA: 0x000054DC File Offset: 0x000036DC
		public static string NavigateAway
		{
			get
			{
				return ClientStrings.ResourceManager.GetString("NavigateAway");
			}
		}

		// Token: 0x17000076 RID: 118
		// (get) Token: 0x0600008C RID: 140 RVA: 0x000054ED File Offset: 0x000036ED
		public static string RemoveDisabledLinkedInConnectionText
		{
			get
			{
				return ClientStrings.ResourceManager.GetString("RemoveDisabledLinkedInConnectionText");
			}
		}

		// Token: 0x17000077 RID: 119
		// (get) Token: 0x0600008D RID: 141 RVA: 0x000054FE File Offset: 0x000036FE
		public static string HCWStoppedDescription
		{
			get
			{
				return ClientStrings.ResourceManager.GetString("HCWStoppedDescription");
			}
		}

		// Token: 0x17000078 RID: 120
		// (get) Token: 0x0600008E RID: 142 RVA: 0x0000550F File Offset: 0x0000370F
		public static string OnDisplayText
		{
			get
			{
				return ClientStrings.ResourceManager.GetString("OnDisplayText");
			}
		}

		// Token: 0x17000079 RID: 121
		// (get) Token: 0x0600008F RID: 143 RVA: 0x00005520 File Offset: 0x00003720
		public static string LongRunErrorLabel
		{
			get
			{
				return ClientStrings.ResourceManager.GetString("LongRunErrorLabel");
			}
		}

		// Token: 0x06000090 RID: 144 RVA: 0x00005531 File Offset: 0x00003731
		public static string EmptyQueryValue(string queryName)
		{
			return string.Format(ClientStrings.ResourceManager.GetString("EmptyQueryValue"), queryName);
		}

		// Token: 0x1700007A RID: 122
		// (get) Token: 0x06000091 RID: 145 RVA: 0x00005548 File Offset: 0x00003748
		public static string EIAE
		{
			get
			{
				return ClientStrings.ResourceManager.GetString("EIAE");
			}
		}

		// Token: 0x1700007B RID: 123
		// (get) Token: 0x06000092 RID: 146 RVA: 0x00005559 File Offset: 0x00003759
		public static string NegotiateAuth
		{
			get
			{
				return ClientStrings.ResourceManager.GetString("NegotiateAuth");
			}
		}

		// Token: 0x1700007C RID: 124
		// (get) Token: 0x06000093 RID: 147 RVA: 0x0000556A File Offset: 0x0000376A
		public static string RequestDLPDetailReportTitle
		{
			get
			{
				return ClientStrings.ResourceManager.GetString("RequestDLPDetailReportTitle");
			}
		}

		// Token: 0x1700007D RID: 125
		// (get) Token: 0x06000094 RID: 148 RVA: 0x0000557B File Offset: 0x0000377B
		public static string WipeConfirmMessage
		{
			get
			{
				return ClientStrings.ResourceManager.GetString("WipeConfirmMessage");
			}
		}

		// Token: 0x1700007E RID: 126
		// (get) Token: 0x06000095 RID: 149 RVA: 0x0000558C File Offset: 0x0000378C
		public static string MobileDeviceEnableText
		{
			get
			{
				return ClientStrings.ResourceManager.GetString("MobileDeviceEnableText");
			}
		}

		// Token: 0x1700007F RID: 127
		// (get) Token: 0x06000096 RID: 150 RVA: 0x0000559D File Offset: 0x0000379D
		public static string EnableConnectorConfirm
		{
			get
			{
				return ClientStrings.ResourceManager.GetString("EnableConnectorConfirm");
			}
		}

		// Token: 0x17000080 RID: 128
		// (get) Token: 0x06000097 RID: 151 RVA: 0x000055AE File Offset: 0x000037AE
		public static string OffCommandText
		{
			get
			{
				return ClientStrings.ResourceManager.GetString("OffCommandText");
			}
		}

		// Token: 0x17000081 RID: 129
		// (get) Token: 0x06000098 RID: 152 RVA: 0x000055BF File Offset: 0x000037BF
		public static string HydrationDoneTitle
		{
			get
			{
				return ClientStrings.ResourceManager.GetString("HydrationDoneTitle");
			}
		}

		// Token: 0x17000082 RID: 130
		// (get) Token: 0x06000099 RID: 153 RVA: 0x000055D0 File Offset: 0x000037D0
		public static string ConnectorAllAvailableIPv6
		{
			get
			{
				return ClientStrings.ResourceManager.GetString("ConnectorAllAvailableIPv6");
			}
		}

		// Token: 0x17000083 RID: 131
		// (get) Token: 0x0600009A RID: 154 RVA: 0x000055E1 File Offset: 0x000037E1
		public static string UMCallAnsweringRulesEditorRuleConditionLabelText
		{
			get
			{
				return ClientStrings.ResourceManager.GetString("UMCallAnsweringRulesEditorRuleConditionLabelText");
			}
		}

		// Token: 0x17000084 RID: 132
		// (get) Token: 0x0600009B RID: 155 RVA: 0x000055F2 File Offset: 0x000037F2
		public static string OwaMailboxPolicyPlaces
		{
			get
			{
				return ClientStrings.ResourceManager.GetString("OwaMailboxPolicyPlaces");
			}
		}

		// Token: 0x17000085 RID: 133
		// (get) Token: 0x0600009C RID: 156 RVA: 0x00005603 File Offset: 0x00003803
		public static string JournalEmailAddressLabel
		{
			get
			{
				return ClientStrings.ResourceManager.GetString("JournalEmailAddressLabel");
			}
		}

		// Token: 0x17000086 RID: 134
		// (get) Token: 0x0600009D RID: 157 RVA: 0x00005614 File Offset: 0x00003814
		public static string PopupBlockedMessage
		{
			get
			{
				return ClientStrings.ResourceManager.GetString("PopupBlockedMessage");
			}
		}

		// Token: 0x17000087 RID: 135
		// (get) Token: 0x0600009E RID: 158 RVA: 0x00005625 File Offset: 0x00003825
		public static string IUnderstandAction
		{
			get
			{
				return ClientStrings.ResourceManager.GetString("IUnderstandAction");
			}
		}

		// Token: 0x17000088 RID: 136
		// (get) Token: 0x0600009F RID: 159 RVA: 0x00005636 File Offset: 0x00003836
		public static string Select15Minutes
		{
			get
			{
				return ClientStrings.ResourceManager.GetString("Select15Minutes");
			}
		}

		// Token: 0x17000089 RID: 137
		// (get) Token: 0x060000A0 RID: 160 RVA: 0x00005647 File Offset: 0x00003847
		public static string AllowedPendingDisplayText
		{
			get
			{
				return ClientStrings.ResourceManager.GetString("AllowedPendingDisplayText");
			}
		}

		// Token: 0x1700008A RID: 138
		// (get) Token: 0x060000A1 RID: 161 RVA: 0x00005658 File Offset: 0x00003858
		public static string OwaMailboxPolicyTimeManagement
		{
			get
			{
				return ClientStrings.ResourceManager.GetString("OwaMailboxPolicyTimeManagement");
			}
		}

		// Token: 0x1700008B RID: 139
		// (get) Token: 0x060000A2 RID: 162 RVA: 0x00005669 File Offset: 0x00003869
		public static string PasswordNote
		{
			get
			{
				return ClientStrings.ResourceManager.GetString("PasswordNote");
			}
		}

		// Token: 0x1700008C RID: 140
		// (get) Token: 0x060000A3 RID: 163 RVA: 0x0000567A File Offset: 0x0000387A
		public static string HasLinkQueryField
		{
			get
			{
				return ClientStrings.ResourceManager.GetString("HasLinkQueryField");
			}
		}

		// Token: 0x1700008D RID: 141
		// (get) Token: 0x060000A4 RID: 164 RVA: 0x0000568B File Offset: 0x0000388B
		public static string VoicemailClearSettingsTitle
		{
			get
			{
				return ClientStrings.ResourceManager.GetString("VoicemailClearSettingsTitle");
			}
		}

		// Token: 0x1700008E RID: 142
		// (get) Token: 0x060000A5 RID: 165 RVA: 0x0000569C File Offset: 0x0000389C
		public static string ConfigureOAuth
		{
			get
			{
				return ClientStrings.ResourceManager.GetString("ConfigureOAuth");
			}
		}

		// Token: 0x060000A6 RID: 166 RVA: 0x000056AD File Offset: 0x000038AD
		public static string ApplyToAllSelected(int n)
		{
			return string.Format(ClientStrings.ResourceManager.GetString("ApplyToAllSelected"), n);
		}

		// Token: 0x060000A7 RID: 167 RVA: 0x000056C9 File Offset: 0x000038C9
		public static string ValidationErrorFormat(string msg)
		{
			return string.Format(ClientStrings.ResourceManager.GetString("ValidationErrorFormat"), msg);
		}

		// Token: 0x1700008F RID: 143
		// (get) Token: 0x060000A8 RID: 168 RVA: 0x000056E0 File Offset: 0x000038E0
		public static string And
		{
			get
			{
				return ClientStrings.ResourceManager.GetString("And");
			}
		}

		// Token: 0x17000090 RID: 144
		// (get) Token: 0x060000A9 RID: 169 RVA: 0x000056F1 File Offset: 0x000038F1
		public static string VoicemailResetPINSuccessMessage
		{
			get
			{
				return ClientStrings.ResourceManager.GetString("VoicemailResetPINSuccessMessage");
			}
		}

		// Token: 0x17000091 RID: 145
		// (get) Token: 0x060000AA RID: 170 RVA: 0x00005702 File Offset: 0x00003902
		public static string FileDownloadFailed
		{
			get
			{
				return ClientStrings.ResourceManager.GetString("FileDownloadFailed");
			}
		}

		// Token: 0x17000092 RID: 146
		// (get) Token: 0x060000AB RID: 171 RVA: 0x00005713 File Offset: 0x00003913
		public static string ConfirmRemoveLinkedIn
		{
			get
			{
				return ClientStrings.ResourceManager.GetString("ConfirmRemoveLinkedIn");
			}
		}

		// Token: 0x060000AC RID: 172 RVA: 0x00005724 File Offset: 0x00003924
		public static string SyncedMailboxText(int cur, int n)
		{
			return string.Format(ClientStrings.ResourceManager.GetString("SyncedMailboxText"), cur, n);
		}

		// Token: 0x17000093 RID: 147
		// (get) Token: 0x060000AD RID: 173 RVA: 0x00005746 File Offset: 0x00003946
		public static string RemoveFacebookSupportingText
		{
			get
			{
				return ClientStrings.ResourceManager.GetString("RemoveFacebookSupportingText");
			}
		}

		// Token: 0x17000094 RID: 148
		// (get) Token: 0x060000AE RID: 174 RVA: 0x00005757 File Offset: 0x00003957
		public static string ListViewMoreResultsWarning
		{
			get
			{
				return ClientStrings.ResourceManager.GetString("ListViewMoreResultsWarning");
			}
		}

		// Token: 0x17000095 RID: 149
		// (get) Token: 0x060000AF RID: 175 RVA: 0x00005768 File Offset: 0x00003968
		public static string DisableReplicationCommandText
		{
			get
			{
				return ClientStrings.ResourceManager.GetString("DisableReplicationCommandText");
			}
		}

		// Token: 0x17000096 RID: 150
		// (get) Token: 0x060000B0 RID: 176 RVA: 0x00005779 File Offset: 0x00003979
		public static string EnterpriseMainHeader
		{
			get
			{
				return ClientStrings.ResourceManager.GetString("EnterpriseMainHeader");
			}
		}

		// Token: 0x17000097 RID: 151
		// (get) Token: 0x060000B1 RID: 177 RVA: 0x0000578A File Offset: 0x0000398A
		public static string AddGroupNamingPolicyElementButtonText
		{
			get
			{
				return ClientStrings.ResourceManager.GetString("AddGroupNamingPolicyElementButtonText");
			}
		}

		// Token: 0x17000098 RID: 152
		// (get) Token: 0x060000B2 RID: 178 RVA: 0x0000579B File Offset: 0x0000399B
		public static string UseAlias
		{
			get
			{
				return ClientStrings.ResourceManager.GetString("UseAlias");
			}
		}

		// Token: 0x17000099 RID: 153
		// (get) Token: 0x060000B3 RID: 179 RVA: 0x000057AC File Offset: 0x000039AC
		public static string FileUploadFailed
		{
			get
			{
				return ClientStrings.ResourceManager.GetString("FileUploadFailed");
			}
		}

		// Token: 0x060000B4 RID: 180 RVA: 0x000057BD File Offset: 0x000039BD
		public static string NameFromFirstLastName(string firstName, string lastName)
		{
			return string.Format(ClientStrings.ResourceManager.GetString("NameFromFirstLastName"), firstName, lastName);
		}

		// Token: 0x1700009A RID: 154
		// (get) Token: 0x060000B5 RID: 181 RVA: 0x000057D5 File Offset: 0x000039D5
		public static string CustomDateLink
		{
			get
			{
				return ClientStrings.ResourceManager.GetString("CustomDateLink");
			}
		}

		// Token: 0x1700009B RID: 155
		// (get) Token: 0x060000B6 RID: 182 RVA: 0x000057E6 File Offset: 0x000039E6
		public static string PolicyGroupMembership
		{
			get
			{
				return ClientStrings.ResourceManager.GetString("PolicyGroupMembership");
			}
		}

		// Token: 0x060000B7 RID: 183 RVA: 0x000057F7 File Offset: 0x000039F7
		public static string InvalidEmailAddress(string str)
		{
			return string.Format(ClientStrings.ResourceManager.GetString("InvalidEmailAddress"), str);
		}

		// Token: 0x1700009C RID: 156
		// (get) Token: 0x060000B8 RID: 184 RVA: 0x0000580E File Offset: 0x00003A0E
		public static string NextPage
		{
			get
			{
				return ClientStrings.ResourceManager.GetString("NextPage");
			}
		}

		// Token: 0x1700009D RID: 157
		// (get) Token: 0x060000B9 RID: 185 RVA: 0x0000581F File Offset: 0x00003A1F
		public static string HydrationDone
		{
			get
			{
				return ClientStrings.ResourceManager.GetString("HydrationDone");
			}
		}

		// Token: 0x1700009E RID: 158
		// (get) Token: 0x060000BA RID: 186 RVA: 0x00005830 File Offset: 0x00003A30
		public static string ProviderDisabled
		{
			get
			{
				return ClientStrings.ResourceManager.GetString("ProviderDisabled");
			}
		}

		// Token: 0x060000BB RID: 187 RVA: 0x00005841 File Offset: 0x00003A41
		public static string StateOrProvinceConditionText(string names)
		{
			return string.Format(ClientStrings.ResourceManager.GetString("StateOrProvinceConditionText"), names);
		}

		// Token: 0x1700009F RID: 159
		// (get) Token: 0x060000BC RID: 188 RVA: 0x00005858 File Offset: 0x00003A58
		public static string IndividualSettings
		{
			get
			{
				return ClientStrings.ResourceManager.GetString("IndividualSettings");
			}
		}

		// Token: 0x170000A0 RID: 160
		// (get) Token: 0x060000BD RID: 189 RVA: 0x00005869 File Offset: 0x00003A69
		public static string CalendarSharingFreeBusyDetail
		{
			get
			{
				return ClientStrings.ResourceManager.GetString("CalendarSharingFreeBusyDetail");
			}
		}

		// Token: 0x170000A1 RID: 161
		// (get) Token: 0x060000BE RID: 190 RVA: 0x0000587A File Offset: 0x00003A7A
		public static string LongRunInProgressTips
		{
			get
			{
				return ClientStrings.ResourceManager.GetString("LongRunInProgressTips");
			}
		}

		// Token: 0x170000A2 RID: 162
		// (get) Token: 0x060000BF RID: 191 RVA: 0x0000588B File Offset: 0x00003A8B
		public static string OwaMailboxPolicyChangePassword
		{
			get
			{
				return ClientStrings.ResourceManager.GetString("OwaMailboxPolicyChangePassword");
			}
		}

		// Token: 0x170000A3 RID: 163
		// (get) Token: 0x060000C0 RID: 192 RVA: 0x0000589C File Offset: 0x00003A9C
		public static string VoicemailClearSettingsDetailsContactOperator
		{
			get
			{
				return ClientStrings.ResourceManager.GetString("VoicemailClearSettingsDetailsContactOperator");
			}
		}

		// Token: 0x170000A4 RID: 164
		// (get) Token: 0x060000C1 RID: 193 RVA: 0x000058AD File Offset: 0x00003AAD
		public static string DisableFederationStopped
		{
			get
			{
				return ClientStrings.ResourceManager.GetString("DisableFederationStopped");
			}
		}

		// Token: 0x170000A5 RID: 165
		// (get) Token: 0x060000C2 RID: 194 RVA: 0x000058BE File Offset: 0x00003ABE
		public static string Success
		{
			get
			{
				return ClientStrings.ResourceManager.GetString("Success");
			}
		}

		// Token: 0x170000A6 RID: 166
		// (get) Token: 0x060000C3 RID: 195 RVA: 0x000058CF File Offset: 0x00003ACF
		public static string NoOnboardingPermission
		{
			get
			{
				return ClientStrings.ResourceManager.GetString("NoOnboardingPermission");
			}
		}

		// Token: 0x170000A7 RID: 167
		// (get) Token: 0x060000C4 RID: 196 RVA: 0x000058E0 File Offset: 0x00003AE0
		public static string HydratingTitle
		{
			get
			{
				return ClientStrings.ResourceManager.GetString("HydratingTitle");
			}
		}

		// Token: 0x170000A8 RID: 168
		// (get) Token: 0x060000C5 RID: 197 RVA: 0x000058F1 File Offset: 0x00003AF1
		public static string TextMessagingNotificationSetupLinkText
		{
			get
			{
				return ClientStrings.ResourceManager.GetString("TextMessagingNotificationSetupLinkText");
			}
		}

		// Token: 0x170000A9 RID: 169
		// (get) Token: 0x060000C6 RID: 198 RVA: 0x00005902 File Offset: 0x00003B02
		public static string WipePendingPendingDisplayText
		{
			get
			{
				return ClientStrings.ResourceManager.GetString("WipePendingPendingDisplayText");
			}
		}

		// Token: 0x170000AA RID: 170
		// (get) Token: 0x060000C7 RID: 199 RVA: 0x00005913 File Offset: 0x00003B13
		public static string InvalidMultiEmailAddress
		{
			get
			{
				return ClientStrings.ResourceManager.GetString("InvalidMultiEmailAddress");
			}
		}

		// Token: 0x170000AB RID: 171
		// (get) Token: 0x060000C8 RID: 200 RVA: 0x00005924 File Offset: 0x00003B24
		public static string DataCenterMainHeader
		{
			get
			{
				return ClientStrings.ResourceManager.GetString("DataCenterMainHeader");
			}
		}

		// Token: 0x170000AC RID: 172
		// (get) Token: 0x060000C9 RID: 201 RVA: 0x00005935 File Offset: 0x00003B35
		public static string BulkEditNotificationTenMinuteLabel
		{
			get
			{
				return ClientStrings.ResourceManager.GetString("BulkEditNotificationTenMinuteLabel");
			}
		}

		// Token: 0x170000AD RID: 173
		// (get) Token: 0x060000CA RID: 202 RVA: 0x00005946 File Offset: 0x00003B46
		public static string DefaultRuleExceptionLabel
		{
			get
			{
				return ClientStrings.ResourceManager.GetString("DefaultRuleExceptionLabel");
			}
		}

		// Token: 0x170000AE RID: 174
		// (get) Token: 0x060000CB RID: 203 RVA: 0x00005957 File Offset: 0x00003B57
		public static string SelectTheTextAndCopy
		{
			get
			{
				return ClientStrings.ResourceManager.GetString("SelectTheTextAndCopy");
			}
		}

		// Token: 0x170000AF RID: 175
		// (get) Token: 0x060000CC RID: 204 RVA: 0x00005968 File Offset: 0x00003B68
		public static string FailedToRetrieveMailboxOnboarding
		{
			get
			{
				return ClientStrings.ResourceManager.GetString("FailedToRetrieveMailboxOnboarding");
			}
		}

		// Token: 0x170000B0 RID: 176
		// (get) Token: 0x060000CD RID: 205 RVA: 0x00005979 File Offset: 0x00003B79
		public static string DisabledDisplayText
		{
			get
			{
				return ClientStrings.ResourceManager.GetString("DisabledDisplayText");
			}
		}

		// Token: 0x170000B1 RID: 177
		// (get) Token: 0x060000CE RID: 206 RVA: 0x0000598A File Offset: 0x00003B8A
		public static string ConditionValueSeparator
		{
			get
			{
				return ClientStrings.ResourceManager.GetString("ConditionValueSeparator");
			}
		}

		// Token: 0x170000B2 RID: 178
		// (get) Token: 0x060000CF RID: 207 RVA: 0x0000599B File Offset: 0x00003B9B
		public static string LinkedInDelayed
		{
			get
			{
				return ClientStrings.ResourceManager.GetString("LinkedInDelayed");
			}
		}

		// Token: 0x170000B3 RID: 179
		// (get) Token: 0x060000D0 RID: 208 RVA: 0x000059AC File Offset: 0x00003BAC
		public static string ErrorTitle
		{
			get
			{
				return ClientStrings.ResourceManager.GetString("ErrorTitle");
			}
		}

		// Token: 0x170000B4 RID: 180
		// (get) Token: 0x060000D1 RID: 209 RVA: 0x000059BD File Offset: 0x00003BBD
		public static string InvalidSmtpAddress
		{
			get
			{
				return ClientStrings.ResourceManager.GetString("InvalidSmtpAddress");
			}
		}

		// Token: 0x170000B5 RID: 181
		// (get) Token: 0x060000D2 RID: 210 RVA: 0x000059CE File Offset: 0x00003BCE
		public static string RemoveLinkedInSupportingText
		{
			get
			{
				return ClientStrings.ResourceManager.GetString("RemoveLinkedInSupportingText");
			}
		}

		// Token: 0x060000D3 RID: 211 RVA: 0x000059DF File Offset: 0x00003BDF
		public static string BulkEditedProgressSomeOperationsFailed(int m)
		{
			return string.Format(ClientStrings.ResourceManager.GetString("BulkEditedProgressSomeOperationsFailed"), m);
		}

		// Token: 0x170000B6 RID: 182
		// (get) Token: 0x060000D4 RID: 212 RVA: 0x000059FB File Offset: 0x00003BFB
		public static string ResumeDBCopyConfirmation
		{
			get
			{
				return ClientStrings.ResourceManager.GetString("ResumeDBCopyConfirmation");
			}
		}

		// Token: 0x170000B7 RID: 183
		// (get) Token: 0x060000D5 RID: 213 RVA: 0x00005A0C File Offset: 0x00003C0C
		public static string MessageTypeAll
		{
			get
			{
				return ClientStrings.ResourceManager.GetString("MessageTypeAll");
			}
		}

		// Token: 0x170000B8 RID: 184
		// (get) Token: 0x060000D6 RID: 214 RVA: 0x00005A1D File Offset: 0x00003C1D
		public static string CmdLogTitleForHybridO365
		{
			get
			{
				return ClientStrings.ResourceManager.GetString("CmdLogTitleForHybridO365");
			}
		}

		// Token: 0x170000B9 RID: 185
		// (get) Token: 0x060000D7 RID: 215 RVA: 0x00005A2E File Offset: 0x00003C2E
		public static string ConfirmRemoveFacebook
		{
			get
			{
				return ClientStrings.ResourceManager.GetString("ConfirmRemoveFacebook");
			}
		}

		// Token: 0x060000D8 RID: 216 RVA: 0x00005A3F File Offset: 0x00003C3F
		public static string DepartmentConditionText(string names)
		{
			return string.Format(ClientStrings.ResourceManager.GetString("DepartmentConditionText"), names);
		}

		// Token: 0x170000BA RID: 186
		// (get) Token: 0x060000D9 RID: 217 RVA: 0x00005A56 File Offset: 0x00003C56
		public static string BulkEditNotificationMinuteLabel
		{
			get
			{
				return ClientStrings.ResourceManager.GetString("BulkEditNotificationMinuteLabel");
			}
		}

		// Token: 0x060000DA RID: 218 RVA: 0x00005A67 File Offset: 0x00003C67
		public static string RecipientContainerText(string name)
		{
			return string.Format(ClientStrings.ResourceManager.GetString("RecipientContainerText"), name);
		}

		// Token: 0x170000BB RID: 187
		// (get) Token: 0x060000DB RID: 219 RVA: 0x00005A7E File Offset: 0x00003C7E
		public static string VoiceMailText
		{
			get
			{
				return ClientStrings.ResourceManager.GetString("VoiceMailText");
			}
		}

		// Token: 0x170000BC RID: 188
		// (get) Token: 0x060000DC RID: 220 RVA: 0x00005A8F File Offset: 0x00003C8F
		public static string CollapseAll
		{
			get
			{
				return ClientStrings.ResourceManager.GetString("CollapseAll");
			}
		}

		// Token: 0x170000BD RID: 189
		// (get) Token: 0x060000DD RID: 221 RVA: 0x00005AA0 File Offset: 0x00003CA0
		public static string DefaultContactsFolderText
		{
			get
			{
				return ClientStrings.ResourceManager.GetString("DefaultContactsFolderText");
			}
		}

		// Token: 0x170000BE RID: 190
		// (get) Token: 0x060000DE RID: 222 RVA: 0x00005AB1 File Offset: 0x00003CB1
		public static string OwaMailboxPolicyWeather
		{
			get
			{
				return ClientStrings.ResourceManager.GetString("OwaMailboxPolicyWeather");
			}
		}

		// Token: 0x170000BF RID: 191
		// (get) Token: 0x060000DF RID: 223 RVA: 0x00005AC2 File Offset: 0x00003CC2
		public static string LegacyFolderError
		{
			get
			{
				return ClientStrings.ResourceManager.GetString("LegacyFolderError");
			}
		}

		// Token: 0x170000C0 RID: 192
		// (get) Token: 0x060000E0 RID: 224 RVA: 0x00005AD3 File Offset: 0x00003CD3
		public static string MessageTraceReportTitle
		{
			get
			{
				return ClientStrings.ResourceManager.GetString("MessageTraceReportTitle");
			}
		}

		// Token: 0x060000E1 RID: 225 RVA: 0x00005AE4 File Offset: 0x00003CE4
		public static string InvalidSmtpDomainWithSubdomains(string str)
		{
			return string.Format(ClientStrings.ResourceManager.GetString("InvalidSmtpDomainWithSubdomains"), str);
		}

		// Token: 0x170000C1 RID: 193
		// (get) Token: 0x060000E2 RID: 226 RVA: 0x00005AFB File Offset: 0x00003CFB
		public static string JournalEmailAddressInvalid
		{
			get
			{
				return ClientStrings.ResourceManager.GetString("JournalEmailAddressInvalid");
			}
		}

		// Token: 0x170000C2 RID: 194
		// (get) Token: 0x060000E3 RID: 227 RVA: 0x00005B0C File Offset: 0x00003D0C
		public static string JobSubmitted
		{
			get
			{
				return ClientStrings.ResourceManager.GetString("JobSubmitted");
			}
		}

		// Token: 0x170000C3 RID: 195
		// (get) Token: 0x060000E4 RID: 228 RVA: 0x00005B1D File Offset: 0x00003D1D
		public static string UMEnableE164ActionSummary
		{
			get
			{
				return ClientStrings.ResourceManager.GetString("UMEnableE164ActionSummary");
			}
		}

		// Token: 0x170000C4 RID: 196
		// (get) Token: 0x060000E5 RID: 229 RVA: 0x00005B2E File Offset: 0x00003D2E
		public static string OK
		{
			get
			{
				return ClientStrings.ResourceManager.GetString("OK");
			}
		}

		// Token: 0x170000C5 RID: 197
		// (get) Token: 0x060000E6 RID: 230 RVA: 0x00005B3F File Offset: 0x00003D3F
		public static string LastPage
		{
			get
			{
				return ClientStrings.ResourceManager.GetString("LastPage");
			}
		}

		// Token: 0x170000C6 RID: 198
		// (get) Token: 0x060000E7 RID: 231 RVA: 0x00005B50 File Offset: 0x00003D50
		public static string OwaMailboxPolicyRemindersAndNotifications
		{
			get
			{
				return ClientStrings.ResourceManager.GetString("OwaMailboxPolicyRemindersAndNotifications");
			}
		}

		// Token: 0x170000C7 RID: 199
		// (get) Token: 0x060000E8 RID: 232 RVA: 0x00005B61 File Offset: 0x00003D61
		public static string DataLossWarning
		{
			get
			{
				return ClientStrings.ResourceManager.GetString("DataLossWarning");
			}
		}

		// Token: 0x170000C8 RID: 200
		// (get) Token: 0x060000E9 RID: 233 RVA: 0x00005B72 File Offset: 0x00003D72
		public static string SuspendComments
		{
			get
			{
				return ClientStrings.ResourceManager.GetString("SuspendComments");
			}
		}

		// Token: 0x170000C9 RID: 201
		// (get) Token: 0x060000EA RID: 234 RVA: 0x00005B83 File Offset: 0x00003D83
		public static string Delivered
		{
			get
			{
				return ClientStrings.ResourceManager.GetString("Delivered");
			}
		}

		// Token: 0x170000CA RID: 202
		// (get) Token: 0x060000EB RID: 235 RVA: 0x00005B94 File Offset: 0x00003D94
		public static string Retry
		{
			get
			{
				return ClientStrings.ResourceManager.GetString("Retry");
			}
		}

		// Token: 0x170000CB RID: 203
		// (get) Token: 0x060000EC RID: 236 RVA: 0x00005BA5 File Offset: 0x00003DA5
		public static string Descending
		{
			get
			{
				return ClientStrings.ResourceManager.GetString("Descending");
			}
		}

		// Token: 0x170000CC RID: 204
		// (get) Token: 0x060000ED RID: 237 RVA: 0x00005BB6 File Offset: 0x00003DB6
		public static string SimpleFilterTextBoxWaterMark
		{
			get
			{
				return ClientStrings.ResourceManager.GetString("SimpleFilterTextBoxWaterMark");
			}
		}

		// Token: 0x170000CD RID: 205
		// (get) Token: 0x060000EE RID: 238 RVA: 0x00005BC7 File Offset: 0x00003DC7
		public static string TypingDescription
		{
			get
			{
				return ClientStrings.ResourceManager.GetString("TypingDescription");
			}
		}

		// Token: 0x170000CE RID: 206
		// (get) Token: 0x060000EF RID: 239 RVA: 0x00005BD8 File Offset: 0x00003DD8
		public static string NonEditingAuthor
		{
			get
			{
				return ClientStrings.ResourceManager.GetString("NonEditingAuthor");
			}
		}

		// Token: 0x170000CF RID: 207
		// (get) Token: 0x060000F0 RID: 240 RVA: 0x00005BE9 File Offset: 0x00003DE9
		public static string MinimumCriteriaFieldsInError
		{
			get
			{
				return ClientStrings.ResourceManager.GetString("MinimumCriteriaFieldsInError");
			}
		}

		// Token: 0x060000F1 RID: 241 RVA: 0x00005BFA File Offset: 0x00003DFA
		public static string InvalidNumber(string str)
		{
			return string.Format(ClientStrings.ResourceManager.GetString("InvalidNumber"), str);
		}

		// Token: 0x170000D0 RID: 208
		// (get) Token: 0x060000F2 RID: 242 RVA: 0x00005C11 File Offset: 0x00003E11
		public static string ListSeparator
		{
			get
			{
				return ClientStrings.ResourceManager.GetString("ListSeparator");
			}
		}

		// Token: 0x170000D1 RID: 209
		// (get) Token: 0x060000F3 RID: 243 RVA: 0x00005C22 File Offset: 0x00003E22
		public static string ExpandAll
		{
			get
			{
				return ClientStrings.ResourceManager.GetString("ExpandAll");
			}
		}

		// Token: 0x170000D2 RID: 210
		// (get) Token: 0x060000F4 RID: 244 RVA: 0x00005C33 File Offset: 0x00003E33
		public static string AutoInternal
		{
			get
			{
				return ClientStrings.ResourceManager.GetString("AutoInternal");
			}
		}

		// Token: 0x170000D3 RID: 211
		// (get) Token: 0x060000F5 RID: 245 RVA: 0x00005C44 File Offset: 0x00003E44
		public static string NeverUse
		{
			get
			{
				return ClientStrings.ResourceManager.GetString("NeverUse");
			}
		}

		// Token: 0x170000D4 RID: 212
		// (get) Token: 0x060000F6 RID: 246 RVA: 0x00005C55 File Offset: 0x00003E55
		public static string NoonPM
		{
			get
			{
				return ClientStrings.ResourceManager.GetString("NoonPM");
			}
		}

		// Token: 0x170000D5 RID: 213
		// (get) Token: 0x060000F7 RID: 247 RVA: 0x00005C66 File Offset: 0x00003E66
		public static string EnableReplicationCommandText
		{
			get
			{
				return ClientStrings.ResourceManager.GetString("EnableReplicationCommandText");
			}
		}

		// Token: 0x170000D6 RID: 214
		// (get) Token: 0x060000F8 RID: 248 RVA: 0x00005C77 File Offset: 0x00003E77
		public static string HCWCompletedDescription
		{
			get
			{
				return ClientStrings.ResourceManager.GetString("HCWCompletedDescription");
			}
		}

		// Token: 0x170000D7 RID: 215
		// (get) Token: 0x060000F9 RID: 249 RVA: 0x00005C88 File Offset: 0x00003E88
		public static string PWTNS
		{
			get
			{
				return ClientStrings.ResourceManager.GetString("PWTNS");
			}
		}

		// Token: 0x170000D8 RID: 216
		// (get) Token: 0x060000FA RID: 250 RVA: 0x00005C99 File Offset: 0x00003E99
		public static string DeviceModelPickerAll
		{
			get
			{
				return ClientStrings.ResourceManager.GetString("DeviceModelPickerAll");
			}
		}

		// Token: 0x170000D9 RID: 217
		// (get) Token: 0x060000FB RID: 251 RVA: 0x00005CAA File Offset: 0x00003EAA
		public static string NextMonth
		{
			get
			{
				return ClientStrings.ResourceManager.GetString("NextMonth");
			}
		}

		// Token: 0x170000DA RID: 218
		// (get) Token: 0x060000FC RID: 252 RVA: 0x00005CBB File Offset: 0x00003EBB
		public static string UploaderUnhandledExceptionMessage
		{
			get
			{
				return ClientStrings.ResourceManager.GetString("UploaderUnhandledExceptionMessage");
			}
		}

		// Token: 0x170000DB RID: 219
		// (get) Token: 0x060000FD RID: 253 RVA: 0x00005CCC File Offset: 0x00003ECC
		public static string MobileExternal
		{
			get
			{
				return ClientStrings.ResourceManager.GetString("MobileExternal");
			}
		}

		// Token: 0x170000DC RID: 220
		// (get) Token: 0x060000FE RID: 254 RVA: 0x00005CDD File Offset: 0x00003EDD
		public static string SearchButtonTooltip
		{
			get
			{
				return ClientStrings.ResourceManager.GetString("SearchButtonTooltip");
			}
		}

		// Token: 0x170000DD RID: 221
		// (get) Token: 0x060000FF RID: 255 RVA: 0x00005CEE File Offset: 0x00003EEE
		public static string SavingCompletedInformation
		{
			get
			{
				return ClientStrings.ResourceManager.GetString("SavingCompletedInformation");
			}
		}

		// Token: 0x170000DE RID: 222
		// (get) Token: 0x06000100 RID: 256 RVA: 0x00005CFF File Offset: 0x00003EFF
		public static string SetupExchangeHybrid
		{
			get
			{
				return ClientStrings.ResourceManager.GetString("SetupExchangeHybrid");
			}
		}

		// Token: 0x170000DF RID: 223
		// (get) Token: 0x06000101 RID: 257 RVA: 0x00005D10 File Offset: 0x00003F10
		public static string EnableFVA
		{
			get
			{
				return ClientStrings.ResourceManager.GetString("EnableFVA");
			}
		}

		// Token: 0x170000E0 RID: 224
		// (get) Token: 0x06000102 RID: 258 RVA: 0x00005D21 File Offset: 0x00003F21
		public static string PWTNAB
		{
			get
			{
				return ClientStrings.ResourceManager.GetString("PWTNAB");
			}
		}

		// Token: 0x170000E1 RID: 225
		// (get) Token: 0x06000103 RID: 259 RVA: 0x00005D32 File Offset: 0x00003F32
		public static string ForceConnectMailbox
		{
			get
			{
				return ClientStrings.ResourceManager.GetString("ForceConnectMailbox");
			}
		}

		// Token: 0x170000E2 RID: 226
		// (get) Token: 0x06000104 RID: 260 RVA: 0x00005D43 File Offset: 0x00003F43
		public static string ShowModalDialogErrorReport
		{
			get
			{
				return ClientStrings.ResourceManager.GetString("ShowModalDialogErrorReport");
			}
		}

		// Token: 0x170000E3 RID: 227
		// (get) Token: 0x06000105 RID: 261 RVA: 0x00005D54 File Offset: 0x00003F54
		public static string Imap
		{
			get
			{
				return ClientStrings.ResourceManager.GetString("Imap");
			}
		}

		// Token: 0x170000E4 RID: 228
		// (get) Token: 0x06000106 RID: 262 RVA: 0x00005D65 File Offset: 0x00003F65
		public static string ConnectToFacebookMessage
		{
			get
			{
				return ClientStrings.ResourceManager.GetString("ConnectToFacebookMessage");
			}
		}

		// Token: 0x06000107 RID: 263 RVA: 0x00005D76 File Offset: 0x00003F76
		public static string ConstraintNoTrailingSpecificCharacter(string str, char invalidChar)
		{
			return string.Format(ClientStrings.ResourceManager.GetString("ConstraintNoTrailingSpecificCharacter"), str, invalidChar);
		}

		// Token: 0x170000E5 RID: 229
		// (get) Token: 0x06000108 RID: 264 RVA: 0x00005D93 File Offset: 0x00003F93
		public static string DateRangeError
		{
			get
			{
				return ClientStrings.ResourceManager.GetString("DateRangeError");
			}
		}

		// Token: 0x06000109 RID: 265 RVA: 0x00005DA4 File Offset: 0x00003FA4
		public static string ConstraintViolationStringOnlyCanContainSpecificCharacters(string characters, string value)
		{
			return string.Format(ClientStrings.ResourceManager.GetString("ConstraintViolationStringOnlyCanContainSpecificCharacters"), characters, value);
		}

		// Token: 0x170000E6 RID: 230
		// (get) Token: 0x0600010A RID: 266 RVA: 0x00005DBC File Offset: 0x00003FBC
		public static string OwaMailboxPolicyUserExperience
		{
			get
			{
				return ClientStrings.ResourceManager.GetString("OwaMailboxPolicyUserExperience");
			}
		}

		// Token: 0x170000E7 RID: 231
		// (get) Token: 0x0600010B RID: 267 RVA: 0x00005DCD File Offset: 0x00003FCD
		public static string WebServiceRequestInetError
		{
			get
			{
				return ClientStrings.ResourceManager.GetString("WebServiceRequestInetError");
			}
		}

		// Token: 0x170000E8 RID: 232
		// (get) Token: 0x0600010C RID: 268 RVA: 0x00005DDE File Offset: 0x00003FDE
		public static string FindMeText
		{
			get
			{
				return ClientStrings.ResourceManager.GetString("FindMeText");
			}
		}

		// Token: 0x170000E9 RID: 233
		// (get) Token: 0x0600010D RID: 269 RVA: 0x00005DEF File Offset: 0x00003FEF
		public static string CtrlKeySelectAllInListView
		{
			get
			{
				return ClientStrings.ResourceManager.GetString("CtrlKeySelectAllInListView");
			}
		}

		// Token: 0x170000EA RID: 234
		// (get) Token: 0x0600010E RID: 270 RVA: 0x00005E00 File Offset: 0x00004000
		public static string RemoveAction
		{
			get
			{
				return ClientStrings.ResourceManager.GetString("RemoveAction");
			}
		}

		// Token: 0x170000EB RID: 235
		// (get) Token: 0x0600010F RID: 271 RVA: 0x00005E11 File Offset: 0x00004011
		public static string UMHolidayScheduleHolidayEndDateRequiredError
		{
			get
			{
				return ClientStrings.ResourceManager.GetString("UMHolidayScheduleHolidayEndDateRequiredError");
			}
		}

		// Token: 0x170000EC RID: 236
		// (get) Token: 0x06000110 RID: 272 RVA: 0x00005E22 File Offset: 0x00004022
		public static string NetworkCredentialUserNameErrorMessage
		{
			get
			{
				return ClientStrings.ResourceManager.GetString("NetworkCredentialUserNameErrorMessage");
			}
		}

		// Token: 0x170000ED RID: 237
		// (get) Token: 0x06000111 RID: 273 RVA: 0x00005E33 File Offset: 0x00004033
		public static string SetupHybridUIFirst
		{
			get
			{
				return ClientStrings.ResourceManager.GetString("SetupHybridUIFirst");
			}
		}

		// Token: 0x170000EE RID: 238
		// (get) Token: 0x06000112 RID: 274 RVA: 0x00005E44 File Offset: 0x00004044
		public static string UMKeyMappingActionSummaryAnnounceBusinessLocation
		{
			get
			{
				return ClientStrings.ResourceManager.GetString("UMKeyMappingActionSummaryAnnounceBusinessLocation");
			}
		}

		// Token: 0x06000113 RID: 275 RVA: 0x00005E55 File Offset: 0x00004055
		public static string UMExtensionWithDigitLabel(int length)
		{
			return string.Format(ClientStrings.ResourceManager.GetString("UMExtensionWithDigitLabel"), length);
		}

		// Token: 0x170000EF RID: 239
		// (get) Token: 0x06000114 RID: 276 RVA: 0x00005E71 File Offset: 0x00004071
		public static string GroupNamingPolicyCaption
		{
			get
			{
				return ClientStrings.ResourceManager.GetString("GroupNamingPolicyCaption");
			}
		}

		// Token: 0x170000F0 RID: 240
		// (get) Token: 0x06000115 RID: 277 RVA: 0x00005E82 File Offset: 0x00004082
		public static string TransferToGalContactText
		{
			get
			{
				return ClientStrings.ResourceManager.GetString("TransferToGalContactText");
			}
		}

		// Token: 0x170000F1 RID: 241
		// (get) Token: 0x06000116 RID: 278 RVA: 0x00005E93 File Offset: 0x00004093
		public static string UnhandledExceptionMessage
		{
			get
			{
				return ClientStrings.ResourceManager.GetString("UnhandledExceptionMessage");
			}
		}

		// Token: 0x170000F2 RID: 242
		// (get) Token: 0x06000117 RID: 279 RVA: 0x00005EA4 File Offset: 0x000040A4
		public static string OwaMailboxPolicyJunkEmail
		{
			get
			{
				return ClientStrings.ResourceManager.GetString("OwaMailboxPolicyJunkEmail");
			}
		}

		// Token: 0x06000118 RID: 280 RVA: 0x00005EB5 File Offset: 0x000040B5
		public static string InvalidSmtpDomainWithSubdomainsOrIP6(string str)
		{
			return string.Format(ClientStrings.ResourceManager.GetString("InvalidSmtpDomainWithSubdomainsOrIP6"), str);
		}

		// Token: 0x170000F3 RID: 243
		// (get) Token: 0x06000119 RID: 281 RVA: 0x00005ECC File Offset: 0x000040CC
		public static string DynamicDistributionGroupText
		{
			get
			{
				return ClientStrings.ResourceManager.GetString("DynamicDistributionGroupText");
			}
		}

		// Token: 0x0600011A RID: 282 RVA: 0x00005EDD File Offset: 0x000040DD
		public static string ConstraintViolationStringDoesNotMatchRegularExpression(string pattern, string value)
		{
			return string.Format(ClientStrings.ResourceManager.GetString("ConstraintViolationStringDoesNotMatchRegularExpression"), pattern, value);
		}

		// Token: 0x170000F4 RID: 244
		// (get) Token: 0x0600011B RID: 283 RVA: 0x00005EF5 File Offset: 0x000040F5
		public static string ServerNameColumnText
		{
			get
			{
				return ClientStrings.ResourceManager.GetString("ServerNameColumnText");
			}
		}

		// Token: 0x170000F5 RID: 245
		// (get) Token: 0x0600011C RID: 284 RVA: 0x00005F06 File Offset: 0x00004106
		public static string TextMessagingNotificationNotSetupLinkText
		{
			get
			{
				return ClientStrings.ResourceManager.GetString("TextMessagingNotificationNotSetupLinkText");
			}
		}

		// Token: 0x170000F6 RID: 246
		// (get) Token: 0x0600011D RID: 285 RVA: 0x00005F17 File Offset: 0x00004117
		public static string QuerySyntaxError
		{
			get
			{
				return ClientStrings.ResourceManager.GetString("QuerySyntaxError");
			}
		}

		// Token: 0x170000F7 RID: 247
		// (get) Token: 0x0600011E RID: 286 RVA: 0x00005F28 File Offset: 0x00004128
		public static string SharingDomainOptionAll
		{
			get
			{
				return ClientStrings.ResourceManager.GetString("SharingDomainOptionAll");
			}
		}

		// Token: 0x170000F8 RID: 248
		// (get) Token: 0x0600011F RID: 287 RVA: 0x00005F39 File Offset: 0x00004139
		public static string VoicemailWizardEnterPinText
		{
			get
			{
				return ClientStrings.ResourceManager.GetString("VoicemailWizardEnterPinText");
			}
		}

		// Token: 0x06000120 RID: 288 RVA: 0x00005F4A File Offset: 0x0000414A
		public static string UMEnablePinDigitLabel(int minLength)
		{
			return string.Format(ClientStrings.ResourceManager.GetString("UMEnablePinDigitLabel"), minLength);
		}

		// Token: 0x170000F9 RID: 249
		// (get) Token: 0x06000121 RID: 289 RVA: 0x00005F66 File Offset: 0x00004166
		public static string AddressExists
		{
			get
			{
				return ClientStrings.ResourceManager.GetString("AddressExists");
			}
		}

		// Token: 0x170000FA RID: 250
		// (get) Token: 0x06000122 RID: 290 RVA: 0x00005F77 File Offset: 0x00004177
		public static string ModifyExchangeHybrid
		{
			get
			{
				return ClientStrings.ResourceManager.GetString("ModifyExchangeHybrid");
			}
		}

		// Token: 0x170000FB RID: 251
		// (get) Token: 0x06000123 RID: 291 RVA: 0x00005F88 File Offset: 0x00004188
		public static string HydrationAndFeatureDone
		{
			get
			{
				return ClientStrings.ResourceManager.GetString("HydrationAndFeatureDone");
			}
		}

		// Token: 0x170000FC RID: 252
		// (get) Token: 0x06000124 RID: 292 RVA: 0x00005F99 File Offset: 0x00004199
		public static string ProviderConnected
		{
			get
			{
				return ClientStrings.ResourceManager.GetString("ProviderConnected");
			}
		}

		// Token: 0x170000FD RID: 253
		// (get) Token: 0x06000125 RID: 293 RVA: 0x00005FAA File Offset: 0x000041AA
		public static string NoNamingPolicySetup
		{
			get
			{
				return ClientStrings.ResourceManager.GetString("NoNamingPolicySetup");
			}
		}

		// Token: 0x170000FE RID: 254
		// (get) Token: 0x06000126 RID: 294 RVA: 0x00005FBB File Offset: 0x000041BB
		public static string DoNotShowDialog
		{
			get
			{
				return ClientStrings.ResourceManager.GetString("DoNotShowDialog");
			}
		}

		// Token: 0x170000FF RID: 255
		// (get) Token: 0x06000127 RID: 295 RVA: 0x00005FCC File Offset: 0x000041CC
		public static string RemoveMailboxDeleteLiveID
		{
			get
			{
				return ClientStrings.ResourceManager.GetString("RemoveMailboxDeleteLiveID");
			}
		}

		// Token: 0x17000100 RID: 256
		// (get) Token: 0x06000128 RID: 296 RVA: 0x00005FDD File Offset: 0x000041DD
		public static string GreetingsAndPromptsTitleText
		{
			get
			{
				return ClientStrings.ResourceManager.GetString("GreetingsAndPromptsTitleText");
			}
		}

		// Token: 0x06000129 RID: 297 RVA: 0x00005FEE File Offset: 0x000041EE
		public static string VoicemailClearSettingsDetails(string number)
		{
			return string.Format(ClientStrings.ResourceManager.GetString("VoicemailClearSettingsDetails"), number);
		}

		// Token: 0x17000101 RID: 257
		// (get) Token: 0x0600012A RID: 298 RVA: 0x00006005 File Offset: 0x00004205
		public static string ConfigureVoicemailButtonText
		{
			get
			{
				return ClientStrings.ResourceManager.GetString("ConfigureVoicemailButtonText");
			}
		}

		// Token: 0x17000102 RID: 258
		// (get) Token: 0x0600012B RID: 299 RVA: 0x00006016 File Offset: 0x00004216
		public static string FirstNameLastName
		{
			get
			{
				return ClientStrings.ResourceManager.GetString("FirstNameLastName");
			}
		}

		// Token: 0x17000103 RID: 259
		// (get) Token: 0x0600012C RID: 300 RVA: 0x00006027 File Offset: 0x00004227
		public static string Yes
		{
			get
			{
				return ClientStrings.ResourceManager.GetString("Yes");
			}
		}

		// Token: 0x17000104 RID: 260
		// (get) Token: 0x0600012D RID: 301 RVA: 0x00006038 File Offset: 0x00004238
		public static string Author
		{
			get
			{
				return ClientStrings.ResourceManager.GetString("Author");
			}
		}

		// Token: 0x17000105 RID: 261
		// (get) Token: 0x0600012E RID: 302 RVA: 0x00006049 File Offset: 0x00004249
		public static string PWTRAS
		{
			get
			{
				return ClientStrings.ResourceManager.GetString("PWTRAS");
			}
		}

		// Token: 0x17000106 RID: 262
		// (get) Token: 0x0600012F RID: 303 RVA: 0x0000605A File Offset: 0x0000425A
		public static string TransferToGalContactVoicemailText
		{
			get
			{
				return ClientStrings.ResourceManager.GetString("TransferToGalContactVoicemailText");
			}
		}

		// Token: 0x17000107 RID: 263
		// (get) Token: 0x06000130 RID: 304 RVA: 0x0000606B File Offset: 0x0000426B
		public static string MailboxDelegationDetail
		{
			get
			{
				return ClientStrings.ResourceManager.GetString("MailboxDelegationDetail");
			}
		}

		// Token: 0x17000108 RID: 264
		// (get) Token: 0x06000131 RID: 305 RVA: 0x0000607C File Offset: 0x0000427C
		public static string Or
		{
			get
			{
				return ClientStrings.ResourceManager.GetString("Or");
			}
		}

		// Token: 0x17000109 RID: 265
		// (get) Token: 0x06000132 RID: 306 RVA: 0x0000608D File Offset: 0x0000428D
		public static string Reset
		{
			get
			{
				return ClientStrings.ResourceManager.GetString("Reset");
			}
		}

		// Token: 0x1700010A RID: 266
		// (get) Token: 0x06000133 RID: 307 RVA: 0x0000609E File Offset: 0x0000429E
		public static string UpdateTimeZonePrompt
		{
			get
			{
				return ClientStrings.ResourceManager.GetString("UpdateTimeZonePrompt");
			}
		}

		// Token: 0x1700010B RID: 267
		// (get) Token: 0x06000134 RID: 308 RVA: 0x000060AF File Offset: 0x000042AF
		public static string DontSave
		{
			get
			{
				return ClientStrings.ResourceManager.GetString("DontSave");
			}
		}

		// Token: 0x1700010C RID: 268
		// (get) Token: 0x06000135 RID: 309 RVA: 0x000060C0 File Offset: 0x000042C0
		public static string VoicemailSummaryAccessNumber
		{
			get
			{
				return ClientStrings.ResourceManager.GetString("VoicemailSummaryAccessNumber");
			}
		}

		// Token: 0x1700010D RID: 269
		// (get) Token: 0x06000136 RID: 310 RVA: 0x000060D1 File Offset: 0x000042D1
		public static string Save
		{
			get
			{
				return ClientStrings.ResourceManager.GetString("Save");
			}
		}

		// Token: 0x1700010E RID: 270
		// (get) Token: 0x06000137 RID: 311 RVA: 0x000060E2 File Offset: 0x000042E2
		public static string OwaMailboxPolicyThemeSelection
		{
			get
			{
				return ClientStrings.ResourceManager.GetString("OwaMailboxPolicyThemeSelection");
			}
		}

		// Token: 0x1700010F RID: 271
		// (get) Token: 0x06000138 RID: 312 RVA: 0x000060F3 File Offset: 0x000042F3
		public static string ReadThis
		{
			get
			{
				return ClientStrings.ResourceManager.GetString("ReadThis");
			}
		}

		// Token: 0x17000110 RID: 272
		// (get) Token: 0x06000139 RID: 313 RVA: 0x00006104 File Offset: 0x00004304
		public static string SubnetIPEditorTitle
		{
			get
			{
				return ClientStrings.ResourceManager.GetString("SubnetIPEditorTitle");
			}
		}

		// Token: 0x0600013A RID: 314 RVA: 0x00006115 File Offset: 0x00004315
		public static string ModalDialgSingleSRMsgTemplate(int n, string msg)
		{
			return string.Format(ClientStrings.ResourceManager.GetString("ModalDialgSingleSRMsgTemplate"), n, msg);
		}

		// Token: 0x17000111 RID: 273
		// (get) Token: 0x0600013B RID: 315 RVA: 0x00006132 File Offset: 0x00004332
		public static string UMEnableExtensionAuto
		{
			get
			{
				return ClientStrings.ResourceManager.GetString("UMEnableExtensionAuto");
			}
		}

		// Token: 0x17000112 RID: 274
		// (get) Token: 0x0600013C RID: 316 RVA: 0x00006143 File Offset: 0x00004343
		public static string WarningPanelMultipleWarnings
		{
			get
			{
				return ClientStrings.ResourceManager.GetString("WarningPanelMultipleWarnings");
			}
		}

		// Token: 0x17000113 RID: 275
		// (get) Token: 0x0600013D RID: 317 RVA: 0x00006154 File Offset: 0x00004354
		public static string UMHolidayScheduleHolidayNameRequiredError
		{
			get
			{
				return ClientStrings.ResourceManager.GetString("UMHolidayScheduleHolidayNameRequiredError");
			}
		}

		// Token: 0x17000114 RID: 276
		// (get) Token: 0x0600013E RID: 318 RVA: 0x00006165 File Offset: 0x00004365
		public static string Select24Hours
		{
			get
			{
				return ClientStrings.ResourceManager.GetString("Select24Hours");
			}
		}

		// Token: 0x17000115 RID: 277
		// (get) Token: 0x0600013F RID: 319 RVA: 0x00006176 File Offset: 0x00004376
		public static string MemberUpdateTypeApprovalRequired
		{
			get
			{
				return ClientStrings.ResourceManager.GetString("MemberUpdateTypeApprovalRequired");
			}
		}

		// Token: 0x17000116 RID: 278
		// (get) Token: 0x06000140 RID: 320 RVA: 0x00006187 File Offset: 0x00004387
		public static string DefaultRuleActionLabel
		{
			get
			{
				return ClientStrings.ResourceManager.GetString("DefaultRuleActionLabel");
			}
		}

		// Token: 0x17000117 RID: 279
		// (get) Token: 0x06000141 RID: 321 RVA: 0x00006198 File Offset: 0x00004398
		public static string EmptyValueError
		{
			get
			{
				return ClientStrings.ResourceManager.GetString("EmptyValueError");
			}
		}

		// Token: 0x17000118 RID: 280
		// (get) Token: 0x06000142 RID: 322 RVA: 0x000061A9 File Offset: 0x000043A9
		public static string ApplyToAllCalls
		{
			get
			{
				return ClientStrings.ResourceManager.GetString("ApplyToAllCalls");
			}
		}

		// Token: 0x17000119 RID: 281
		// (get) Token: 0x06000143 RID: 323 RVA: 0x000061BA File Offset: 0x000043BA
		public static string OutOfMemoryErrorMessage
		{
			get
			{
				return ClientStrings.ResourceManager.GetString("OutOfMemoryErrorMessage");
			}
		}

		// Token: 0x1700011A RID: 282
		// (get) Token: 0x06000144 RID: 324 RVA: 0x000061CB File Offset: 0x000043CB
		public static string Never
		{
			get
			{
				return ClientStrings.ResourceManager.GetString("Never");
			}
		}

		// Token: 0x1700011B RID: 283
		// (get) Token: 0x06000145 RID: 325 RVA: 0x000061DC File Offset: 0x000043DC
		public static string OABExternal
		{
			get
			{
				return ClientStrings.ResourceManager.GetString("OABExternal");
			}
		}

		// Token: 0x1700011C RID: 284
		// (get) Token: 0x06000146 RID: 326 RVA: 0x000061ED File Offset: 0x000043ED
		public static string ReconnectToFacebookMessage
		{
			get
			{
				return ClientStrings.ResourceManager.GetString("ReconnectToFacebookMessage");
			}
		}

		// Token: 0x1700011D RID: 285
		// (get) Token: 0x06000147 RID: 327 RVA: 0x000061FE File Offset: 0x000043FE
		public static string MemberApprovalHasChanged
		{
			get
			{
				return ClientStrings.ResourceManager.GetString("MemberApprovalHasChanged");
			}
		}

		// Token: 0x1700011E RID: 286
		// (get) Token: 0x06000148 RID: 328 RVA: 0x0000620F File Offset: 0x0000440F
		public static string CreatingFolder
		{
			get
			{
				return ClientStrings.ResourceManager.GetString("CreatingFolder");
			}
		}

		// Token: 0x1700011F RID: 287
		// (get) Token: 0x06000149 RID: 329 RVA: 0x00006220 File Offset: 0x00004420
		public static string UMEnableMailboxAutoSipDescription
		{
			get
			{
				return ClientStrings.ResourceManager.GetString("UMEnableMailboxAutoSipDescription");
			}
		}

		// Token: 0x17000120 RID: 288
		// (get) Token: 0x0600014A RID: 330 RVA: 0x00006231 File Offset: 0x00004431
		public static string FirstNameLastNameInitial
		{
			get
			{
				return ClientStrings.ResourceManager.GetString("FirstNameLastNameInitial");
			}
		}

		// Token: 0x0600014B RID: 331 RVA: 0x00006242 File Offset: 0x00004442
		public static string CompanyConditionText(string names)
		{
			return string.Format(ClientStrings.ResourceManager.GetString("CompanyConditionText"), names);
		}

		// Token: 0x17000121 RID: 289
		// (get) Token: 0x0600014C RID: 332 RVA: 0x00006259 File Offset: 0x00004459
		public static string PleaseWait
		{
			get
			{
				return ClientStrings.ResourceManager.GetString("PleaseWait");
			}
		}

		// Token: 0x17000122 RID: 290
		// (get) Token: 0x0600014D RID: 333 RVA: 0x0000626A File Offset: 0x0000446A
		public static string WhatDoesThisMean
		{
			get
			{
				return ClientStrings.ResourceManager.GetString("WhatDoesThisMean");
			}
		}

		// Token: 0x17000123 RID: 291
		// (get) Token: 0x0600014E RID: 334 RVA: 0x0000627B File Offset: 0x0000447B
		public static string ModalDialogErrorReport
		{
			get
			{
				return ClientStrings.ResourceManager.GetString("ModalDialogErrorReport");
			}
		}

		// Token: 0x0600014F RID: 335 RVA: 0x0000628C File Offset: 0x0000448C
		public static string ClearForPicker(string str1, string str2)
		{
			return string.Format(ClientStrings.ResourceManager.GetString("ClearForPicker"), str1, str2);
		}

		// Token: 0x17000124 RID: 292
		// (get) Token: 0x06000150 RID: 336 RVA: 0x000062A4 File Offset: 0x000044A4
		public static string SecondaryNavigation
		{
			get
			{
				return ClientStrings.ResourceManager.GetString("SecondaryNavigation");
			}
		}

		// Token: 0x17000125 RID: 293
		// (get) Token: 0x06000151 RID: 337 RVA: 0x000062B5 File Offset: 0x000044B5
		public static string ConnectToLinkedInMessage
		{
			get
			{
				return ClientStrings.ResourceManager.GetString("ConnectToLinkedInMessage");
			}
		}

		// Token: 0x17000126 RID: 294
		// (get) Token: 0x06000152 RID: 338 RVA: 0x000062C6 File Offset: 0x000044C6
		public static string MessageTraceMessageIDCannotContainComma
		{
			get
			{
				return ClientStrings.ResourceManager.GetString("MessageTraceMessageIDCannotContainComma");
			}
		}

		// Token: 0x17000127 RID: 295
		// (get) Token: 0x06000153 RID: 339 RVA: 0x000062D7 File Offset: 0x000044D7
		public static string ApplyToAllMessages
		{
			get
			{
				return ClientStrings.ResourceManager.GetString("ApplyToAllMessages");
			}
		}

		// Token: 0x17000128 RID: 296
		// (get) Token: 0x06000154 RID: 340 RVA: 0x000062E8 File Offset: 0x000044E8
		public static string ActiveSyncDisableText
		{
			get
			{
				return ClientStrings.ResourceManager.GetString("ActiveSyncDisableText");
			}
		}

		// Token: 0x17000129 RID: 297
		// (get) Token: 0x06000155 RID: 341 RVA: 0x000062F9 File Offset: 0x000044F9
		public static string BasicAuth
		{
			get
			{
				return ClientStrings.ResourceManager.GetString("BasicAuth");
			}
		}

		// Token: 0x1700012A RID: 298
		// (get) Token: 0x06000156 RID: 342 RVA: 0x0000630A File Offset: 0x0000450A
		public static string VoicemailResetPINTitle
		{
			get
			{
				return ClientStrings.ResourceManager.GetString("VoicemailResetPINTitle");
			}
		}

		// Token: 0x1700012B RID: 299
		// (get) Token: 0x06000157 RID: 343 RVA: 0x0000631B File Offset: 0x0000451B
		public static string LongRunCompletedTips
		{
			get
			{
				return ClientStrings.ResourceManager.GetString("LongRunCompletedTips");
			}
		}

		// Token: 0x1700012C RID: 300
		// (get) Token: 0x06000158 RID: 344 RVA: 0x0000632C File Offset: 0x0000452C
		public static string GroupNamingPolicyPreviewLabel
		{
			get
			{
				return ClientStrings.ResourceManager.GetString("GroupNamingPolicyPreviewLabel");
			}
		}

		// Token: 0x1700012D RID: 301
		// (get) Token: 0x06000159 RID: 345 RVA: 0x0000633D File Offset: 0x0000453D
		public static string AASL
		{
			get
			{
				return ClientStrings.ResourceManager.GetString("AASL");
			}
		}

		// Token: 0x1700012E RID: 302
		// (get) Token: 0x0600015A RID: 346 RVA: 0x0000634E File Offset: 0x0000454E
		public static string MemberUpdateTypeOpen
		{
			get
			{
				return ClientStrings.ResourceManager.GetString("MemberUpdateTypeOpen");
			}
		}

		// Token: 0x1700012F RID: 303
		// (get) Token: 0x0600015B RID: 347 RVA: 0x0000635F File Offset: 0x0000455F
		public static string PublishingEditor
		{
			get
			{
				return ClientStrings.ResourceManager.GetString("PublishingEditor");
			}
		}

		// Token: 0x17000130 RID: 304
		// (get) Token: 0x0600015C RID: 348 RVA: 0x00006370 File Offset: 0x00004570
		public static string LossDataWarning
		{
			get
			{
				return ClientStrings.ResourceManager.GetString("LossDataWarning");
			}
		}

		// Token: 0x17000131 RID: 305
		// (get) Token: 0x0600015D RID: 349 RVA: 0x00006381 File Offset: 0x00004581
		public static string OwaMailboxPolicyInstantMessaging
		{
			get
			{
				return ClientStrings.ResourceManager.GetString("OwaMailboxPolicyInstantMessaging");
			}
		}

		// Token: 0x17000132 RID: 306
		// (get) Token: 0x0600015E RID: 350 RVA: 0x00006392 File Offset: 0x00004592
		public static string TextTooLongErrorMessage
		{
			get
			{
				return ClientStrings.ResourceManager.GetString("TextTooLongErrorMessage");
			}
		}

		// Token: 0x17000133 RID: 307
		// (get) Token: 0x0600015F RID: 351 RVA: 0x000063A3 File Offset: 0x000045A3
		public static string EnableFederationStopped
		{
			get
			{
				return ClientStrings.ResourceManager.GetString("EnableFederationStopped");
			}
		}

		// Token: 0x17000134 RID: 308
		// (get) Token: 0x06000160 RID: 352 RVA: 0x000063B4 File Offset: 0x000045B4
		public static string GreetingsAndPromptsInstructionsText
		{
			get
			{
				return ClientStrings.ResourceManager.GetString("GreetingsAndPromptsInstructionsText");
			}
		}

		// Token: 0x17000135 RID: 309
		// (get) Token: 0x06000161 RID: 353 RVA: 0x000063C5 File Offset: 0x000045C5
		public static string UMHolidayScheduleHolidayPromptRequiredError
		{
			get
			{
				return ClientStrings.ResourceManager.GetString("UMHolidayScheduleHolidayPromptRequiredError");
			}
		}

		// Token: 0x17000136 RID: 310
		// (get) Token: 0x06000162 RID: 354 RVA: 0x000063D6 File Offset: 0x000045D6
		public static string AddDagMember
		{
			get
			{
				return ClientStrings.ResourceManager.GetString("AddDagMember");
			}
		}

		// Token: 0x17000137 RID: 311
		// (get) Token: 0x06000163 RID: 355 RVA: 0x000063E7 File Offset: 0x000045E7
		public static string DistributionGroupText
		{
			get
			{
				return ClientStrings.ResourceManager.GetString("DistributionGroupText");
			}
		}

		// Token: 0x17000138 RID: 312
		// (get) Token: 0x06000164 RID: 356 RVA: 0x000063F8 File Offset: 0x000045F8
		public static string KeyMappingDisplayTextFormat
		{
			get
			{
				return ClientStrings.ResourceManager.GetString("KeyMappingDisplayTextFormat");
			}
		}

		// Token: 0x17000139 RID: 313
		// (get) Token: 0x06000165 RID: 357 RVA: 0x00006409 File Offset: 0x00004609
		public static string RequestRuleDetailReportTitle
		{
			get
			{
				return ClientStrings.ResourceManager.GetString("RequestRuleDetailReportTitle");
			}
		}

		// Token: 0x1700013A RID: 314
		// (get) Token: 0x06000166 RID: 358 RVA: 0x0000641A File Offset: 0x0000461A
		public static string RequestMalwareDetailReportTitle
		{
			get
			{
				return ClientStrings.ResourceManager.GetString("RequestMalwareDetailReportTitle");
			}
		}

		// Token: 0x1700013B RID: 315
		// (get) Token: 0x06000167 RID: 359 RVA: 0x0000642B File Offset: 0x0000462B
		public static string ConfirmRemoveConnectionTitle
		{
			get
			{
				return ClientStrings.ResourceManager.GetString("ConfirmRemoveConnectionTitle");
			}
		}

		// Token: 0x1700013C RID: 316
		// (get) Token: 0x06000168 RID: 360 RVA: 0x0000643C File Offset: 0x0000463C
		public static string WebServicesInternal
		{
			get
			{
				return ClientStrings.ResourceManager.GetString("WebServicesInternal");
			}
		}

		// Token: 0x1700013D RID: 317
		// (get) Token: 0x06000169 RID: 361 RVA: 0x0000644D File Offset: 0x0000464D
		public static string Wait
		{
			get
			{
				return ClientStrings.ResourceManager.GetString("Wait");
			}
		}

		// Token: 0x1700013E RID: 318
		// (get) Token: 0x0600016A RID: 362 RVA: 0x0000645E File Offset: 0x0000465E
		public static string DisableOWAConfirm
		{
			get
			{
				return ClientStrings.ResourceManager.GetString("DisableOWAConfirm");
			}
		}

		// Token: 0x1700013F RID: 319
		// (get) Token: 0x0600016B RID: 363 RVA: 0x0000646F File Offset: 0x0000466F
		public static string UriKindRelativeOrAbsolute
		{
			get
			{
				return ClientStrings.ResourceManager.GetString("UriKindRelativeOrAbsolute");
			}
		}

		// Token: 0x17000140 RID: 320
		// (get) Token: 0x0600016C RID: 364 RVA: 0x00006480 File Offset: 0x00004680
		public static string SessionTimeout
		{
			get
			{
				return ClientStrings.ResourceManager.GetString("SessionTimeout");
			}
		}

		// Token: 0x17000141 RID: 321
		// (get) Token: 0x0600016D RID: 365 RVA: 0x00006491 File Offset: 0x00004691
		public static string Change
		{
			get
			{
				return ClientStrings.ResourceManager.GetString("Change");
			}
		}

		// Token: 0x17000142 RID: 322
		// (get) Token: 0x0600016E RID: 366 RVA: 0x000064A2 File Offset: 0x000046A2
		public static string LastNameFirstName
		{
			get
			{
				return ClientStrings.ResourceManager.GetString("LastNameFirstName");
			}
		}

		// Token: 0x17000143 RID: 323
		// (get) Token: 0x0600016F RID: 367 RVA: 0x000064B3 File Offset: 0x000046B3
		public static string AddIPAddress
		{
			get
			{
				return ClientStrings.ResourceManager.GetString("AddIPAddress");
			}
		}

		// Token: 0x17000144 RID: 324
		// (get) Token: 0x06000170 RID: 368 RVA: 0x000064C4 File Offset: 0x000046C4
		public static string InvalidUnlimitedQuotaRegex
		{
			get
			{
				return ClientStrings.ResourceManager.GetString("InvalidUnlimitedQuotaRegex");
			}
		}

		// Token: 0x17000145 RID: 325
		// (get) Token: 0x06000171 RID: 369 RVA: 0x000064D5 File Offset: 0x000046D5
		public static string NoSenderAddressWarning
		{
			get
			{
				return ClientStrings.ResourceManager.GetString("NoSenderAddressWarning");
			}
		}

		// Token: 0x17000146 RID: 326
		// (get) Token: 0x06000172 RID: 370 RVA: 0x000064E6 File Offset: 0x000046E6
		public static string TextMessagingNotificationSetupText
		{
			get
			{
				return ClientStrings.ResourceManager.GetString("TextMessagingNotificationSetupText");
			}
		}

		// Token: 0x17000147 RID: 327
		// (get) Token: 0x06000173 RID: 371 RVA: 0x000064F7 File Offset: 0x000046F7
		public static string LastNameFirstNameInitial
		{
			get
			{
				return ClientStrings.ResourceManager.GetString("LastNameFirstNameInitial");
			}
		}

		// Token: 0x06000174 RID: 372 RVA: 0x00006508 File Offset: 0x00004708
		public static string DateChooserListName(string str1, string str2)
		{
			return string.Format(ClientStrings.ResourceManager.GetString("DateChooserListName"), str1, str2);
		}

		// Token: 0x17000148 RID: 328
		// (get) Token: 0x06000175 RID: 373 RVA: 0x00006520 File Offset: 0x00004720
		public static string UnhandledExceptionTitle
		{
			get
			{
				return ClientStrings.ResourceManager.GetString("UnhandledExceptionTitle");
			}
		}

		// Token: 0x17000149 RID: 329
		// (get) Token: 0x06000176 RID: 374 RVA: 0x00006531 File Offset: 0x00004731
		public static string NoConditionErrorText
		{
			get
			{
				return ClientStrings.ResourceManager.GetString("NoConditionErrorText");
			}
		}

		// Token: 0x1700014A RID: 330
		// (get) Token: 0x06000177 RID: 375 RVA: 0x00006542 File Offset: 0x00004742
		public static string FirstPage
		{
			get
			{
				return ClientStrings.ResourceManager.GetString("FirstPage");
			}
		}

		// Token: 0x1700014B RID: 331
		// (get) Token: 0x06000178 RID: 376 RVA: 0x00006553 File Offset: 0x00004753
		public static string CannotUploadMultipleFiles
		{
			get
			{
				return ClientStrings.ResourceManager.GetString("CannotUploadMultipleFiles");
			}
		}

		// Token: 0x1700014C RID: 332
		// (get) Token: 0x06000179 RID: 377 RVA: 0x00006564 File Offset: 0x00004764
		public static string ClearButtonTooltip
		{
			get
			{
				return ClientStrings.ResourceManager.GetString("ClearButtonTooltip");
			}
		}

		// Token: 0x1700014D RID: 333
		// (get) Token: 0x0600017A RID: 378 RVA: 0x00006575 File Offset: 0x00004775
		public static string AutoExternal
		{
			get
			{
				return ClientStrings.ResourceManager.GetString("AutoExternal");
			}
		}

		// Token: 0x1700014E RID: 334
		// (get) Token: 0x0600017B RID: 379 RVA: 0x00006586 File Offset: 0x00004786
		public static string DayAndTimeRangeTooltip
		{
			get
			{
				return ClientStrings.ResourceManager.GetString("DayAndTimeRangeTooltip");
			}
		}

		// Token: 0x1700014F RID: 335
		// (get) Token: 0x0600017C RID: 380 RVA: 0x00006597 File Offset: 0x00004797
		public static string ViewNotificationDetails
		{
			get
			{
				return ClientStrings.ResourceManager.GetString("ViewNotificationDetails");
			}
		}

		// Token: 0x17000150 RID: 336
		// (get) Token: 0x0600017D RID: 381 RVA: 0x000065A8 File Offset: 0x000047A8
		public static string EASL
		{
			get
			{
				return ClientStrings.ResourceManager.GetString("EASL");
			}
		}

		// Token: 0x17000151 RID: 337
		// (get) Token: 0x0600017E RID: 382 RVA: 0x000065B9 File Offset: 0x000047B9
		public static string Outlook
		{
			get
			{
				return ClientStrings.ResourceManager.GetString("Outlook");
			}
		}

		// Token: 0x17000152 RID: 338
		// (get) Token: 0x0600017F RID: 383 RVA: 0x000065CA File Offset: 0x000047CA
		public static string ConnectProviderCommandText
		{
			get
			{
				return ClientStrings.ResourceManager.GetString("ConnectProviderCommandText");
			}
		}

		// Token: 0x17000153 RID: 339
		// (get) Token: 0x06000180 RID: 384 RVA: 0x000065DB File Offset: 0x000047DB
		public static string SpecificPhoneNumberText
		{
			get
			{
				return ClientStrings.ResourceManager.GetString("SpecificPhoneNumberText");
			}
		}

		// Token: 0x17000154 RID: 340
		// (get) Token: 0x06000181 RID: 385 RVA: 0x000065EC File Offset: 0x000047EC
		public static string Pending
		{
			get
			{
				return ClientStrings.ResourceManager.GetString("Pending");
			}
		}

		// Token: 0x17000155 RID: 341
		// (get) Token: 0x06000182 RID: 386 RVA: 0x000065FD File Offset: 0x000047FD
		public static string CalendarSharingFreeBusyReviewer
		{
			get
			{
				return ClientStrings.ResourceManager.GetString("CalendarSharingFreeBusyReviewer");
			}
		}

		// Token: 0x17000156 RID: 342
		// (get) Token: 0x06000183 RID: 387 RVA: 0x0000660E File Offset: 0x0000480E
		public static string LitigationHoldDateNotSet
		{
			get
			{
				return ClientStrings.ResourceManager.GetString("LitigationHoldDateNotSet");
			}
		}

		// Token: 0x17000157 RID: 343
		// (get) Token: 0x06000184 RID: 388 RVA: 0x0000661F File Offset: 0x0000481F
		public static string OwaMailboxPolicyAllAddressLists
		{
			get
			{
				return ClientStrings.ResourceManager.GetString("OwaMailboxPolicyAllAddressLists");
			}
		}

		// Token: 0x17000158 RID: 344
		// (get) Token: 0x06000185 RID: 389 RVA: 0x00006630 File Offset: 0x00004830
		public static string Error
		{
			get
			{
				return ClientStrings.ResourceManager.GetString("Error");
			}
		}

		// Token: 0x17000159 RID: 345
		// (get) Token: 0x06000186 RID: 390 RVA: 0x00006641 File Offset: 0x00004841
		public static string Externaldnslookups
		{
			get
			{
				return ClientStrings.ResourceManager.GetString("Externaldnslookups");
			}
		}

		// Token: 0x1700015A RID: 346
		// (get) Token: 0x06000187 RID: 391 RVA: 0x00006652 File Offset: 0x00004852
		public static string EditVoicemailButtonText
		{
			get
			{
				return ClientStrings.ResourceManager.GetString("EditVoicemailButtonText");
			}
		}

		// Token: 0x1700015B RID: 347
		// (get) Token: 0x06000188 RID: 392 RVA: 0x00006663 File Offset: 0x00004863
		public static string FailedToRetrieveMailboxLocalMove
		{
			get
			{
				return ClientStrings.ResourceManager.GetString("FailedToRetrieveMailboxLocalMove");
			}
		}

		// Token: 0x06000189 RID: 393 RVA: 0x00006674 File Offset: 0x00004874
		public static string AddressLabel(int index)
		{
			return string.Format(ClientStrings.ResourceManager.GetString("AddressLabel"), index);
		}

		// Token: 0x1700015C RID: 348
		// (get) Token: 0x0600018A RID: 394 RVA: 0x00006690 File Offset: 0x00004890
		public static string ConnectorAllAvailableIPv4
		{
			get
			{
				return ClientStrings.ResourceManager.GetString("ConnectorAllAvailableIPv4");
			}
		}

		// Token: 0x1700015D RID: 349
		// (get) Token: 0x0600018B RID: 395 RVA: 0x000066A1 File Offset: 0x000048A1
		public static string HydrationAndFeatureDoneTitle
		{
			get
			{
				return ClientStrings.ResourceManager.GetString("HydrationAndFeatureDoneTitle");
			}
		}

		// Token: 0x1700015E RID: 350
		// (get) Token: 0x0600018C RID: 396 RVA: 0x000066B2 File Offset: 0x000048B2
		public static string ConstraintViolationNoLeadingOrTrailingWhitespace
		{
			get
			{
				return ClientStrings.ResourceManager.GetString("ConstraintViolationNoLeadingOrTrailingWhitespace");
			}
		}

		// Token: 0x1700015F RID: 351
		// (get) Token: 0x0600018D RID: 397 RVA: 0x000066C3 File Offset: 0x000048C3
		public static string CalendarSharingFreeBusySimple
		{
			get
			{
				return ClientStrings.ResourceManager.GetString("CalendarSharingFreeBusySimple");
			}
		}

		// Token: 0x17000160 RID: 352
		// (get) Token: 0x0600018E RID: 398 RVA: 0x000066D4 File Offset: 0x000048D4
		public static string PageSize
		{
			get
			{
				return ClientStrings.ResourceManager.GetString("PageSize");
			}
		}

		// Token: 0x17000161 RID: 353
		// (get) Token: 0x0600018F RID: 399 RVA: 0x000066E5 File Offset: 0x000048E5
		public static string ConstraintFieldsNotMatchError
		{
			get
			{
				return ClientStrings.ResourceManager.GetString("ConstraintFieldsNotMatchError");
			}
		}

		// Token: 0x17000162 RID: 354
		// (get) Token: 0x06000190 RID: 400 RVA: 0x000066F6 File Offset: 0x000048F6
		public static string Updating
		{
			get
			{
				return ClientStrings.ResourceManager.GetString("Updating");
			}
		}

		// Token: 0x17000163 RID: 355
		// (get) Token: 0x06000191 RID: 401 RVA: 0x00006707 File Offset: 0x00004907
		public static string AdditionalPropertiesLabel
		{
			get
			{
				return ClientStrings.ResourceManager.GetString("AdditionalPropertiesLabel");
			}
		}

		// Token: 0x06000192 RID: 402 RVA: 0x00006718 File Offset: 0x00004918
		public static string SharedUMAutoAttendantPilotIdentifierListNumberError(int number)
		{
			return string.Format(ClientStrings.ResourceManager.GetString("SharedUMAutoAttendantPilotIdentifierListNumberError"), number);
		}

		// Token: 0x17000164 RID: 356
		// (get) Token: 0x06000193 RID: 403 RVA: 0x00006734 File Offset: 0x00004934
		public static string JumpToMigrationSlabConfirmation
		{
			get
			{
				return ClientStrings.ResourceManager.GetString("JumpToMigrationSlabConfirmation");
			}
		}

		// Token: 0x17000165 RID: 357
		// (get) Token: 0x06000194 RID: 404 RVA: 0x00006745 File Offset: 0x00004945
		public static string VoicemailCallFwdContactOperator
		{
			get
			{
				return ClientStrings.ResourceManager.GetString("VoicemailCallFwdContactOperator");
			}
		}

		// Token: 0x17000166 RID: 358
		// (get) Token: 0x06000195 RID: 405 RVA: 0x00006756 File Offset: 0x00004956
		public static string InvalidValueRange
		{
			get
			{
				return ClientStrings.ResourceManager.GetString("InvalidValueRange");
			}
		}

		// Token: 0x17000167 RID: 359
		// (get) Token: 0x06000196 RID: 406 RVA: 0x00006767 File Offset: 0x00004967
		public static string NoActionRuleAuditSeverity
		{
			get
			{
				return ClientStrings.ResourceManager.GetString("NoActionRuleAuditSeverity");
			}
		}

		// Token: 0x17000168 RID: 360
		// (get) Token: 0x06000197 RID: 407 RVA: 0x00006778 File Offset: 0x00004978
		public static string VoicemailClearSettingsConfirmationMessage
		{
			get
			{
				return ClientStrings.ResourceManager.GetString("VoicemailClearSettingsConfirmationMessage");
			}
		}

		// Token: 0x17000169 RID: 361
		// (get) Token: 0x06000198 RID: 408 RVA: 0x00006789 File Offset: 0x00004989
		public static string WebServicesExternal
		{
			get
			{
				return ClientStrings.ResourceManager.GetString("WebServicesExternal");
			}
		}

		// Token: 0x1700016A RID: 362
		// (get) Token: 0x06000199 RID: 409 RVA: 0x0000679A File Offset: 0x0000499A
		public static string UploaderValidationError
		{
			get
			{
				return ClientStrings.ResourceManager.GetString("UploaderValidationError");
			}
		}

		// Token: 0x1700016B RID: 363
		// (get) Token: 0x0600019A RID: 410 RVA: 0x000067AB File Offset: 0x000049AB
		public static string ServerAboutToExpireWarningText
		{
			get
			{
				return ClientStrings.ResourceManager.GetString("ServerAboutToExpireWarningText");
			}
		}

		// Token: 0x1700016C RID: 364
		// (get) Token: 0x0600019B RID: 411 RVA: 0x000067BC File Offset: 0x000049BC
		public static string ConditionValueRequriedErrorMessage
		{
			get
			{
				return ClientStrings.ResourceManager.GetString("ConditionValueRequriedErrorMessage");
			}
		}

		// Token: 0x0600019C RID: 412 RVA: 0x000067CD File Offset: 0x000049CD
		public static string UMEnableExtensionValidation(int length)
		{
			return string.Format(ClientStrings.ResourceManager.GetString("UMEnableExtensionValidation"), length);
		}

		// Token: 0x1700016D RID: 365
		// (get) Token: 0x0600019D RID: 413 RVA: 0x000067E9 File Offset: 0x000049E9
		public static string PreviousePage
		{
			get
			{
				return ClientStrings.ResourceManager.GetString("PreviousePage");
			}
		}

		// Token: 0x1700016E RID: 366
		// (get) Token: 0x0600019E RID: 414 RVA: 0x000067FA File Offset: 0x000049FA
		public static string Ascending
		{
			get
			{
				return ClientStrings.ResourceManager.GetString("Ascending");
			}
		}

		// Token: 0x1700016F RID: 367
		// (get) Token: 0x0600019F RID: 415 RVA: 0x0000680B File Offset: 0x00004A0B
		public static string EditSubnetCaption
		{
			get
			{
				return ClientStrings.ResourceManager.GetString("EditSubnetCaption");
			}
		}

		// Token: 0x17000170 RID: 368
		// (get) Token: 0x060001A0 RID: 416 RVA: 0x0000681C File Offset: 0x00004A1C
		public static string ChooseAtLeastOneColumn
		{
			get
			{
				return ClientStrings.ResourceManager.GetString("ChooseAtLeastOneColumn");
			}
		}

		// Token: 0x060001A1 RID: 417 RVA: 0x0000682D File Offset: 0x00004A2D
		public static string ConstraintViolationStringLengthTooShort(int minLength, int realLength)
		{
			return string.Format(ClientStrings.ResourceManager.GetString("ConstraintViolationStringLengthTooShort"), minLength, realLength);
		}

		// Token: 0x17000171 RID: 369
		// (get) Token: 0x060001A2 RID: 418 RVA: 0x0000684F File Offset: 0x00004A4F
		public static string EditSharingEnabledDomains
		{
			get
			{
				return ClientStrings.ResourceManager.GetString("EditSharingEnabledDomains");
			}
		}

		// Token: 0x17000172 RID: 370
		// (get) Token: 0x060001A3 RID: 419 RVA: 0x00006860 File Offset: 0x00004A60
		public static string Recipients
		{
			get
			{
				return ClientStrings.ResourceManager.GetString("Recipients");
			}
		}

		// Token: 0x17000173 RID: 371
		// (get) Token: 0x060001A4 RID: 420 RVA: 0x00006871 File Offset: 0x00004A71
		public static string Back
		{
			get
			{
				return ClientStrings.ResourceManager.GetString("Back");
			}
		}

		// Token: 0x17000174 RID: 372
		// (get) Token: 0x060001A5 RID: 421 RVA: 0x00006882 File Offset: 0x00004A82
		public static string VoicemailPostFwdRecordGreeting
		{
			get
			{
				return ClientStrings.ResourceManager.GetString("VoicemailPostFwdRecordGreeting");
			}
		}

		// Token: 0x17000175 RID: 373
		// (get) Token: 0x060001A6 RID: 422 RVA: 0x00006893 File Offset: 0x00004A93
		public static string ColumnChooseFailed
		{
			get
			{
				return ClientStrings.ResourceManager.GetString("ColumnChooseFailed");
			}
		}

		// Token: 0x17000176 RID: 374
		// (get) Token: 0x060001A7 RID: 423 RVA: 0x000068A4 File Offset: 0x00004AA4
		public static string TransportRuleBusinessContinuity
		{
			get
			{
				return ClientStrings.ResourceManager.GetString("TransportRuleBusinessContinuity");
			}
		}

		// Token: 0x17000177 RID: 375
		// (get) Token: 0x060001A8 RID: 424 RVA: 0x000068B5 File Offset: 0x00004AB5
		public static string TitleSectionMobileDevices
		{
			get
			{
				return ClientStrings.ResourceManager.GetString("TitleSectionMobileDevices");
			}
		}

		// Token: 0x17000178 RID: 376
		// (get) Token: 0x060001A9 RID: 425 RVA: 0x000068C6 File Offset: 0x00004AC6
		public static string NewFolder
		{
			get
			{
				return ClientStrings.ResourceManager.GetString("NewFolder");
			}
		}

		// Token: 0x17000179 RID: 377
		// (get) Token: 0x060001AA RID: 426 RVA: 0x000068D7 File Offset: 0x00004AD7
		public static string EnableCommandText
		{
			get
			{
				return ClientStrings.ResourceManager.GetString("EnableCommandText");
			}
		}

		// Token: 0x1700017A RID: 378
		// (get) Token: 0x060001AB RID: 427 RVA: 0x000068E8 File Offset: 0x00004AE8
		public static string PrimaryAddressLabel
		{
			get
			{
				return ClientStrings.ResourceManager.GetString("PrimaryAddressLabel");
			}
		}

		// Token: 0x1700017B RID: 379
		// (get) Token: 0x060001AC RID: 428 RVA: 0x000068F9 File Offset: 0x00004AF9
		public static string IncidentReportContentCustom
		{
			get
			{
				return ClientStrings.ResourceManager.GetString("IncidentReportContentCustom");
			}
		}

		// Token: 0x1700017C RID: 380
		// (get) Token: 0x060001AD RID: 429 RVA: 0x0000690A File Offset: 0x00004B0A
		public static string MessageTraceInvalidStartDate
		{
			get
			{
				return ClientStrings.ResourceManager.GetString("MessageTraceInvalidStartDate");
			}
		}

		// Token: 0x1700017D RID: 381
		// (get) Token: 0x060001AE RID: 430 RVA: 0x0000691B File Offset: 0x00004B1B
		public static string InvalidDecimal1
		{
			get
			{
				return ClientStrings.ResourceManager.GetString("InvalidDecimal1");
			}
		}

		// Token: 0x1700017E RID: 382
		// (get) Token: 0x060001AF RID: 431 RVA: 0x0000692C File Offset: 0x00004B2C
		public static string ActivateDBCopyConfirmation
		{
			get
			{
				return ClientStrings.ResourceManager.GetString("ActivateDBCopyConfirmation");
			}
		}

		// Token: 0x1700017F RID: 383
		// (get) Token: 0x060001B0 RID: 432 RVA: 0x0000693D File Offset: 0x00004B3D
		public static string DisableActiveSyncConfirm
		{
			get
			{
				return ClientStrings.ResourceManager.GetString("DisableActiveSyncConfirm");
			}
		}

		// Token: 0x17000180 RID: 384
		// (get) Token: 0x060001B1 RID: 433 RVA: 0x0000694E File Offset: 0x00004B4E
		public static string PleaseWaitWhileSaving
		{
			get
			{
				return ClientStrings.ResourceManager.GetString("PleaseWaitWhileSaving");
			}
		}

		// Token: 0x060001B2 RID: 434 RVA: 0x0000695F File Offset: 0x00004B5F
		public static string CustomAttributeConditionText(int index, string value)
		{
			return string.Format(ClientStrings.ResourceManager.GetString("CustomAttributeConditionText"), index, value);
		}

		// Token: 0x17000181 RID: 385
		// (get) Token: 0x060001B3 RID: 435 RVA: 0x0000697C File Offset: 0x00004B7C
		public static string AddConditionButtonText
		{
			get
			{
				return ClientStrings.ResourceManager.GetString("AddConditionButtonText");
			}
		}

		// Token: 0x17000182 RID: 386
		// (get) Token: 0x060001B4 RID: 436 RVA: 0x0000698D File Offset: 0x00004B8D
		public static string AcceptedDomainAuthoritativeWarning
		{
			get
			{
				return ClientStrings.ResourceManager.GetString("AcceptedDomainAuthoritativeWarning");
			}
		}

		// Token: 0x17000183 RID: 387
		// (get) Token: 0x060001B5 RID: 437 RVA: 0x0000699E File Offset: 0x00004B9E
		public static string Editor
		{
			get
			{
				return ClientStrings.ResourceManager.GetString("Editor");
			}
		}

		// Token: 0x17000184 RID: 388
		// (get) Token: 0x060001B6 RID: 438 RVA: 0x000069AF File Offset: 0x00004BAF
		public static string PlayOnPhoneConnected
		{
			get
			{
				return ClientStrings.ResourceManager.GetString("PlayOnPhoneConnected");
			}
		}

		// Token: 0x17000185 RID: 389
		// (get) Token: 0x060001B7 RID: 439 RVA: 0x000069C0 File Offset: 0x00004BC0
		public static string InvalidDomainName
		{
			get
			{
				return ClientStrings.ResourceManager.GetString("InvalidDomainName");
			}
		}

		// Token: 0x17000186 RID: 390
		// (get) Token: 0x060001B8 RID: 440 RVA: 0x000069D1 File Offset: 0x00004BD1
		public static string SetECPAuthConfirmText
		{
			get
			{
				return ClientStrings.ResourceManager.GetString("SetECPAuthConfirmText");
			}
		}

		// Token: 0x17000187 RID: 391
		// (get) Token: 0x060001B9 RID: 441 RVA: 0x000069E2 File Offset: 0x00004BE2
		public static string RuleNameTextBoxLabel
		{
			get
			{
				return ClientStrings.ResourceManager.GetString("RuleNameTextBoxLabel");
			}
		}

		// Token: 0x17000188 RID: 392
		// (get) Token: 0x060001BA RID: 442 RVA: 0x000069F3 File Offset: 0x00004BF3
		public static string More
		{
			get
			{
				return ClientStrings.ResourceManager.GetString("More");
			}
		}

		// Token: 0x17000189 RID: 393
		// (get) Token: 0x060001BB RID: 443 RVA: 0x00006A04 File Offset: 0x00004C04
		public static string FirstNameInitialLastName
		{
			get
			{
				return ClientStrings.ResourceManager.GetString("FirstNameInitialLastName");
			}
		}

		// Token: 0x1700018A RID: 394
		// (get) Token: 0x060001BC RID: 444 RVA: 0x00006A15 File Offset: 0x00004C15
		public static string OwaMailboxPolicyCommunicationManagement
		{
			get
			{
				return ClientStrings.ResourceManager.GetString("OwaMailboxPolicyCommunicationManagement");
			}
		}

		// Token: 0x1700018B RID: 395
		// (get) Token: 0x060001BD RID: 445 RVA: 0x00006A26 File Offset: 0x00004C26
		public static string Owner
		{
			get
			{
				return ClientStrings.ResourceManager.GetString("Owner");
			}
		}

		// Token: 0x1700018C RID: 396
		// (get) Token: 0x060001BE RID: 446 RVA: 0x00006A37 File Offset: 0x00004C37
		public static string EditDomain
		{
			get
			{
				return ClientStrings.ResourceManager.GetString("EditDomain");
			}
		}

		// Token: 0x1700018D RID: 397
		// (get) Token: 0x060001BF RID: 447 RVA: 0x00006A48 File Offset: 0x00004C48
		public static string InvalidEmailAddressName
		{
			get
			{
				return ClientStrings.ResourceManager.GetString("InvalidEmailAddressName");
			}
		}

		// Token: 0x1700018E RID: 398
		// (get) Token: 0x060001C0 RID: 448 RVA: 0x00006A59 File Offset: 0x00004C59
		public static string LeadingTrailingSpaceError
		{
			get
			{
				return ClientStrings.ResourceManager.GetString("LeadingTrailingSpaceError");
			}
		}

		// Token: 0x1700018F RID: 399
		// (get) Token: 0x060001C1 RID: 449 RVA: 0x00006A6A File Offset: 0x00004C6A
		public static string DayRangeAndTimeRangeTooltip
		{
			get
			{
				return ClientStrings.ResourceManager.GetString("DayRangeAndTimeRangeTooltip");
			}
		}

		// Token: 0x17000190 RID: 400
		// (get) Token: 0x060001C2 RID: 450 RVA: 0x00006A7B File Offset: 0x00004C7B
		public static string OwaMailboxPolicyPremiumClient
		{
			get
			{
				return ClientStrings.ResourceManager.GetString("OwaMailboxPolicyPremiumClient");
			}
		}

		// Token: 0x17000191 RID: 401
		// (get) Token: 0x060001C3 RID: 451 RVA: 0x00006A8C File Offset: 0x00004C8C
		public static string IncidentReportDeselectAll
		{
			get
			{
				return ClientStrings.ResourceManager.GetString("IncidentReportDeselectAll");
			}
		}

		// Token: 0x17000192 RID: 402
		// (get) Token: 0x060001C4 RID: 452 RVA: 0x00006A9D File Offset: 0x00004C9D
		public static string OwaMailboxPolicyNotes
		{
			get
			{
				return ClientStrings.ResourceManager.GetString("OwaMailboxPolicyNotes");
			}
		}

		// Token: 0x17000193 RID: 403
		// (get) Token: 0x060001C5 RID: 453 RVA: 0x00006AAE File Offset: 0x00004CAE
		public static string UMEnableSipActionSummary
		{
			get
			{
				return ClientStrings.ResourceManager.GetString("UMEnableSipActionSummary");
			}
		}

		// Token: 0x17000194 RID: 404
		// (get) Token: 0x060001C6 RID: 454 RVA: 0x00006ABF File Offset: 0x00004CBF
		public static string Reviewer
		{
			get
			{
				return ClientStrings.ResourceManager.GetString("Reviewer");
			}
		}

		// Token: 0x060001C7 RID: 455 RVA: 0x00006AD0 File Offset: 0x00004CD0
		public static string UMExtensionValidationFailure(int length)
		{
			return string.Format(ClientStrings.ResourceManager.GetString("UMExtensionValidationFailure"), length);
		}

		// Token: 0x17000195 RID: 405
		// (get) Token: 0x060001C8 RID: 456 RVA: 0x00006AEC File Offset: 0x00004CEC
		public static string LongRunStoppedTips
		{
			get
			{
				return ClientStrings.ResourceManager.GetString("LongRunStoppedTips");
			}
		}

		// Token: 0x17000196 RID: 406
		// (get) Token: 0x060001C9 RID: 457 RVA: 0x00006AFD File Offset: 0x00004CFD
		public static string RequestSpamDetailReportTitle
		{
			get
			{
				return ClientStrings.ResourceManager.GetString("RequestSpamDetailReportTitle");
			}
		}

		// Token: 0x17000197 RID: 407
		// (get) Token: 0x060001CA RID: 458 RVA: 0x00006B0E File Offset: 0x00004D0E
		public static string AddExceptionButtonText
		{
			get
			{
				return ClientStrings.ResourceManager.GetString("AddExceptionButtonText");
			}
		}

		// Token: 0x17000198 RID: 408
		// (get) Token: 0x060001CB RID: 459 RVA: 0x00006B1F File Offset: 0x00004D1F
		public static string OwaMailboxPolicyRecoverDeletedItems
		{
			get
			{
				return ClientStrings.ResourceManager.GetString("OwaMailboxPolicyRecoverDeletedItems");
			}
		}

		// Token: 0x060001CC RID: 460 RVA: 0x00006B30 File Offset: 0x00004D30
		public static string GroupNamingPolicyPreviewDescriptionFooter(string name)
		{
			return string.Format(ClientStrings.ResourceManager.GetString("GroupNamingPolicyPreviewDescriptionFooter"), name);
		}

		// Token: 0x17000199 RID: 409
		// (get) Token: 0x060001CD RID: 461 RVA: 0x00006B47 File Offset: 0x00004D47
		public static string AddActionButtonText
		{
			get
			{
				return ClientStrings.ResourceManager.GetString("AddActionButtonText");
			}
		}

		// Token: 0x1700019A RID: 410
		// (get) Token: 0x060001CE RID: 462 RVA: 0x00006B58 File Offset: 0x00004D58
		public static string EndDateMustBeYesterday
		{
			get
			{
				return ClientStrings.ResourceManager.GetString("EndDateMustBeYesterday");
			}
		}

		// Token: 0x1700019B RID: 411
		// (get) Token: 0x060001CF RID: 463 RVA: 0x00006B69 File Offset: 0x00004D69
		public static string OwaInternal
		{
			get
			{
				return ClientStrings.ResourceManager.GetString("OwaInternal");
			}
		}

		// Token: 0x060001D0 RID: 464 RVA: 0x00006B7A File Offset: 0x00004D7A
		public static string DateExceedsRange(int numdays)
		{
			return string.Format(ClientStrings.ResourceManager.GetString("DateExceedsRange"), numdays);
		}

		// Token: 0x1700019C RID: 412
		// (get) Token: 0x060001D1 RID: 465 RVA: 0x00006B96 File Offset: 0x00004D96
		public static string CheckNames
		{
			get
			{
				return ClientStrings.ResourceManager.GetString("CheckNames");
			}
		}

		// Token: 0x1700019D RID: 413
		// (get) Token: 0x060001D2 RID: 466 RVA: 0x00006BA7 File Offset: 0x00004DA7
		public static string UMHolidayScheduleHolidayStartEndDateValidationError
		{
			get
			{
				return ClientStrings.ResourceManager.GetString("UMHolidayScheduleHolidayStartEndDateValidationError");
			}
		}

		// Token: 0x1700019E RID: 414
		// (get) Token: 0x060001D3 RID: 467 RVA: 0x00006BB8 File Offset: 0x00004DB8
		public static string InvalidMailboxSearchName
		{
			get
			{
				return ClientStrings.ResourceManager.GetString("InvalidMailboxSearchName");
			}
		}

		// Token: 0x060001D4 RID: 468 RVA: 0x00006BC9 File Offset: 0x00004DC9
		public static string ConstraintViolationInvalidUriKind(string uri, string expectedUriKind)
		{
			return string.Format(ClientStrings.ResourceManager.GetString("ConstraintViolationInvalidUriKind"), uri, expectedUriKind);
		}

		// Token: 0x060001D5 RID: 469 RVA: 0x00006BE1 File Offset: 0x00004DE1
		public static string FinalizedMailboxText(int cur, int n)
		{
			return string.Format(ClientStrings.ResourceManager.GetString("FinalizedMailboxText"), cur, n);
		}

		// Token: 0x1700019F RID: 415
		// (get) Token: 0x060001D6 RID: 470 RVA: 0x00006C03 File Offset: 0x00004E03
		public static string RemoveMailboxKeepLiveID
		{
			get
			{
				return ClientStrings.ResourceManager.GetString("RemoveMailboxKeepLiveID");
			}
		}

		// Token: 0x170001A0 RID: 416
		// (get) Token: 0x060001D7 RID: 471 RVA: 0x00006C14 File Offset: 0x00004E14
		public static string OwaMailboxPolicySignatures
		{
			get
			{
				return ClientStrings.ResourceManager.GetString("OwaMailboxPolicySignatures");
			}
		}

		// Token: 0x170001A1 RID: 417
		// (get) Token: 0x060001D8 RID: 472 RVA: 0x00006C25 File Offset: 0x00004E25
		public static string WorkingHoursText
		{
			get
			{
				return ClientStrings.ResourceManager.GetString("WorkingHoursText");
			}
		}

		// Token: 0x170001A2 RID: 418
		// (get) Token: 0x060001D9 RID: 473 RVA: 0x00006C36 File Offset: 0x00004E36
		public static string ESHL
		{
			get
			{
				return ClientStrings.ResourceManager.GetString("ESHL");
			}
		}

		// Token: 0x170001A3 RID: 419
		// (get) Token: 0x060001DA RID: 474 RVA: 0x00006C47 File Offset: 0x00004E47
		public static string FileStillUploading
		{
			get
			{
				return ClientStrings.ResourceManager.GetString("FileStillUploading");
			}
		}

		// Token: 0x170001A4 RID: 420
		// (get) Token: 0x060001DB RID: 475 RVA: 0x00006C58 File Offset: 0x00004E58
		public static string StartOverMailboxSearch
		{
			get
			{
				return ClientStrings.ResourceManager.GetString("StartOverMailboxSearch");
			}
		}

		// Token: 0x170001A5 RID: 421
		// (get) Token: 0x060001DC RID: 476 RVA: 0x00006C69 File Offset: 0x00004E69
		public static string DisableConnectorLoggingConfirm
		{
			get
			{
				return ClientStrings.ResourceManager.GetString("DisableConnectorLoggingConfirm");
			}
		}

		// Token: 0x170001A6 RID: 422
		// (get) Token: 0x060001DD RID: 477 RVA: 0x00006C7A File Offset: 0x00004E7A
		public static string LongRunDataLossWarning
		{
			get
			{
				return ClientStrings.ResourceManager.GetString("LongRunDataLossWarning");
			}
		}

		// Token: 0x170001A7 RID: 423
		// (get) Token: 0x060001DE RID: 478 RVA: 0x00006C8B File Offset: 0x00004E8B
		public static string ActiveText
		{
			get
			{
				return ClientStrings.ResourceManager.GetString("ActiveText");
			}
		}

		// Token: 0x170001A8 RID: 424
		// (get) Token: 0x060001DF RID: 479 RVA: 0x00006C9C File Offset: 0x00004E9C
		public static string DisableMOWAConfirm
		{
			get
			{
				return ClientStrings.ResourceManager.GetString("DisableMOWAConfirm");
			}
		}

		// Token: 0x170001A9 RID: 425
		// (get) Token: 0x060001E0 RID: 480 RVA: 0x00006CAD File Offset: 0x00004EAD
		public static string WarningPanelSeeMoreMsg
		{
			get
			{
				return ClientStrings.ResourceManager.GetString("WarningPanelSeeMoreMsg");
			}
		}

		// Token: 0x060001E1 RID: 481 RVA: 0x00006CBE File Offset: 0x00004EBE
		public static string BulkEditProgress(int m, int n, int o)
		{
			return string.Format(ClientStrings.ResourceManager.GetString("BulkEditProgress"), m, n, o);
		}

		// Token: 0x170001AA RID: 426
		// (get) Token: 0x060001E2 RID: 482 RVA: 0x00006CE6 File Offset: 0x00004EE6
		public static string EnterpriseLogoutFail
		{
			get
			{
				return ClientStrings.ResourceManager.GetString("EnterpriseLogoutFail");
			}
		}

		// Token: 0x170001AB RID: 427
		// (get) Token: 0x060001E3 RID: 483 RVA: 0x00006CF7 File Offset: 0x00004EF7
		public static string AlwaysUse
		{
			get
			{
				return ClientStrings.ResourceManager.GetString("AlwaysUse");
			}
		}

		// Token: 0x060001E4 RID: 484 RVA: 0x00006D08 File Offset: 0x00004F08
		public static string GuideToSubscriptionPages(string popUrl, string imapUrl, string onClick)
		{
			return string.Format(ClientStrings.ResourceManager.GetString("GuideToSubscriptionPages"), popUrl, imapUrl, onClick);
		}

		// Token: 0x170001AC RID: 428
		// (get) Token: 0x060001E5 RID: 485 RVA: 0x00006D21 File Offset: 0x00004F21
		public static string PublishingAuthor
		{
			get
			{
				return ClientStrings.ResourceManager.GetString("PublishingAuthor");
			}
		}

		// Token: 0x170001AD RID: 429
		// (get) Token: 0x060001E6 RID: 486 RVA: 0x00006D32 File Offset: 0x00004F32
		public static string CopyError
		{
			get
			{
				return ClientStrings.ResourceManager.GetString("CopyError");
			}
		}

		// Token: 0x170001AE RID: 430
		// (get) Token: 0x060001E7 RID: 487 RVA: 0x00006D43 File Offset: 0x00004F43
		public static string LastNameInitialFirstName
		{
			get
			{
				return ClientStrings.ResourceManager.GetString("LastNameInitialFirstName");
			}
		}

		// Token: 0x170001AF RID: 431
		// (get) Token: 0x060001E8 RID: 488 RVA: 0x00006D54 File Offset: 0x00004F54
		public static string Internaldnslookups
		{
			get
			{
				return ClientStrings.ResourceManager.GetString("Internaldnslookups");
			}
		}

		// Token: 0x170001B0 RID: 432
		// (get) Token: 0x060001E9 RID: 489 RVA: 0x00006D65 File Offset: 0x00004F65
		public static string ApplyAllMessagesWarning
		{
			get
			{
				return ClientStrings.ResourceManager.GetString("ApplyAllMessagesWarning");
			}
		}

		// Token: 0x060001EA RID: 490 RVA: 0x00006D78 File Offset: 0x00004F78
		public static string ModalDialogSREachMsgTemplate(int index, string messageType, string message, string details, string helpMessage)
		{
			return string.Format(ClientStrings.ResourceManager.GetString("ModalDialogSREachMsgTemplate"), new object[]
			{
				index,
				messageType,
				message,
				details,
				helpMessage
			});
		}

		// Token: 0x170001B1 RID: 433
		// (get) Token: 0x060001EB RID: 491 RVA: 0x00006DBB File Offset: 0x00004FBB
		public static string EditIPAddress
		{
			get
			{
				return ClientStrings.ResourceManager.GetString("EditIPAddress");
			}
		}

		// Token: 0x170001B2 RID: 434
		// (get) Token: 0x060001EC RID: 492 RVA: 0x00006DCC File Offset: 0x00004FCC
		public static string SecurityGroupText
		{
			get
			{
				return ClientStrings.ResourceManager.GetString("SecurityGroupText");
			}
		}

		// Token: 0x170001B3 RID: 435
		// (get) Token: 0x060001ED RID: 493 RVA: 0x00006DDD File Offset: 0x00004FDD
		public static string ResumeMailboxSearch
		{
			get
			{
				return ClientStrings.ResourceManager.GetString("ResumeMailboxSearch");
			}
		}

		// Token: 0x170001B4 RID: 436
		// (get) Token: 0x060001EE RID: 494 RVA: 0x00006DEE File Offset: 0x00004FEE
		public static string JumpToOffice365MigrationSlabFailed
		{
			get
			{
				return ClientStrings.ResourceManager.GetString("JumpToOffice365MigrationSlabFailed");
			}
		}

		// Token: 0x170001B5 RID: 437
		// (get) Token: 0x060001EF RID: 495 RVA: 0x00006DFF File Offset: 0x00004FFF
		public static string DeliveryReportEmailBodyForMailTo
		{
			get
			{
				return ClientStrings.ResourceManager.GetString("DeliveryReportEmailBodyForMailTo");
			}
		}

		// Token: 0x170001B6 RID: 438
		// (get) Token: 0x060001F0 RID: 496 RVA: 0x00006E10 File Offset: 0x00005010
		public static string DefaultRuleConditionLabel
		{
			get
			{
				return ClientStrings.ResourceManager.GetString("DefaultRuleConditionLabel");
			}
		}

		// Token: 0x170001B7 RID: 439
		// (get) Token: 0x060001F1 RID: 497 RVA: 0x00006E21 File Offset: 0x00005021
		public static string RequestDLPDetail
		{
			get
			{
				return ClientStrings.ResourceManager.GetString("RequestDLPDetail");
			}
		}

		// Token: 0x170001B8 RID: 440
		// (get) Token: 0x060001F2 RID: 498 RVA: 0x00006E32 File Offset: 0x00005032
		public static string AuditSeverityLevelLabel
		{
			get
			{
				return ClientStrings.ResourceManager.GetString("AuditSeverityLevelLabel");
			}
		}

		// Token: 0x170001B9 RID: 441
		// (get) Token: 0x060001F3 RID: 499 RVA: 0x00006E43 File Offset: 0x00005043
		public static string NoneForEmpty
		{
			get
			{
				return ClientStrings.ResourceManager.GetString("NoneForEmpty");
			}
		}

		// Token: 0x170001BA RID: 442
		// (get) Token: 0x060001F4 RID: 500 RVA: 0x00006E54 File Offset: 0x00005054
		public static string OwaMailboxPolicyRules
		{
			get
			{
				return ClientStrings.ResourceManager.GetString("OwaMailboxPolicyRules");
			}
		}

		// Token: 0x170001BB RID: 443
		// (get) Token: 0x060001F5 RID: 501 RVA: 0x00006E65 File Offset: 0x00005065
		public static string CtrlKeyToSave
		{
			get
			{
				return ClientStrings.ResourceManager.GetString("CtrlKeyToSave");
			}
		}

		// Token: 0x170001BC RID: 444
		// (get) Token: 0x060001F6 RID: 502 RVA: 0x00006E76 File Offset: 0x00005076
		public static string CustomPeriodText
		{
			get
			{
				return ClientStrings.ResourceManager.GetString("CustomPeriodText");
			}
		}

		// Token: 0x170001BD RID: 445
		// (get) Token: 0x060001F7 RID: 503 RVA: 0x00006E87 File Offset: 0x00005087
		public static string NSHL
		{
			get
			{
				return ClientStrings.ResourceManager.GetString("NSHL");
			}
		}

		// Token: 0x170001BE RID: 446
		// (get) Token: 0x060001F8 RID: 504 RVA: 0x00006E98 File Offset: 0x00005098
		public static string TransferToNumberText
		{
			get
			{
				return ClientStrings.ResourceManager.GetString("TransferToNumberText");
			}
		}

		// Token: 0x170001BF RID: 447
		// (get) Token: 0x060001F9 RID: 505 RVA: 0x00006EA9 File Offset: 0x000050A9
		public static string UMKeyMappingActionSummaryAnnounceBusinessHours
		{
			get
			{
				return ClientStrings.ResourceManager.GetString("UMKeyMappingActionSummaryAnnounceBusinessHours");
			}
		}

		// Token: 0x170001C0 RID: 448
		// (get) Token: 0x060001FA RID: 506 RVA: 0x00006EBA File Offset: 0x000050BA
		public static string OwaMailboxPolicyFacebook
		{
			get
			{
				return ClientStrings.ResourceManager.GetString("OwaMailboxPolicyFacebook");
			}
		}

		// Token: 0x170001C1 RID: 449
		// (get) Token: 0x060001FB RID: 507 RVA: 0x00006ECB File Offset: 0x000050CB
		public static string ConnectMailboxLaunchWizard
		{
			get
			{
				return ClientStrings.ResourceManager.GetString("ConnectMailboxLaunchWizard");
			}
		}

		// Token: 0x170001C2 RID: 450
		// (get) Token: 0x060001FC RID: 508 RVA: 0x00006EDC File Offset: 0x000050DC
		public static string SomeSelectionNotAdded
		{
			get
			{
				return ClientStrings.ResourceManager.GetString("SomeSelectionNotAdded");
			}
		}

		// Token: 0x060001FD RID: 509 RVA: 0x00006EED File Offset: 0x000050ED
		public static string ListViewStatusText(int select, int total)
		{
			return string.Format(ClientStrings.ResourceManager.GetString("ListViewStatusText"), select, total);
		}

		// Token: 0x170001C3 RID: 451
		// (get) Token: 0x060001FE RID: 510 RVA: 0x00006F0F File Offset: 0x0000510F
		public static string LoadingInformation
		{
			get
			{
				return ClientStrings.ResourceManager.GetString("LoadingInformation");
			}
		}

		// Token: 0x170001C4 RID: 452
		// (get) Token: 0x060001FF RID: 511 RVA: 0x00006F20 File Offset: 0x00005120
		public static string ProceedWithoutTenantInfo
		{
			get
			{
				return ClientStrings.ResourceManager.GetString("ProceedWithoutTenantInfo");
			}
		}

		// Token: 0x06000200 RID: 512 RVA: 0x00006F31 File Offset: 0x00005131
		public static string IncorrectQueryFieldName(string queryFieldName)
		{
			return string.Format(ClientStrings.ResourceManager.GetString("IncorrectQueryFieldName"), queryFieldName);
		}

		// Token: 0x170001C5 RID: 453
		// (get) Token: 0x06000201 RID: 513 RVA: 0x00006F48 File Offset: 0x00005148
		public static string Loading
		{
			get
			{
				return ClientStrings.ResourceManager.GetString("Loading");
			}
		}

		// Token: 0x170001C6 RID: 454
		// (get) Token: 0x06000202 RID: 514 RVA: 0x00006F59 File Offset: 0x00005159
		public static string DeviceTypePickerAll
		{
			get
			{
				return ClientStrings.ResourceManager.GetString("DeviceTypePickerAll");
			}
		}

		// Token: 0x170001C7 RID: 455
		// (get) Token: 0x06000203 RID: 515 RVA: 0x00006F6A File Offset: 0x0000516A
		public static string VoicemailConfigurationTitle
		{
			get
			{
				return ClientStrings.ResourceManager.GetString("VoicemailConfigurationTitle");
			}
		}

		// Token: 0x170001C8 RID: 456
		// (get) Token: 0x06000204 RID: 516 RVA: 0x00006F7B File Offset: 0x0000517B
		public static string RemoveDBCopyConfirmation
		{
			get
			{
				return ClientStrings.ResourceManager.GetString("RemoveDBCopyConfirmation");
			}
		}

		// Token: 0x170001C9 RID: 457
		// (get) Token: 0x06000205 RID: 517 RVA: 0x00006F8C File Offset: 0x0000518C
		public static string MemberUpdateTypeClosed
		{
			get
			{
				return ClientStrings.ResourceManager.GetString("MemberUpdateTypeClosed");
			}
		}

		// Token: 0x170001CA RID: 458
		// (get) Token: 0x06000206 RID: 518 RVA: 0x00006F9D File Offset: 0x0000519D
		public static string OnCommandText
		{
			get
			{
				return ClientStrings.ResourceManager.GetString("OnCommandText");
			}
		}

		// Token: 0x170001CB RID: 459
		// (get) Token: 0x06000207 RID: 519 RVA: 0x00006FAE File Offset: 0x000051AE
		public static string SelectOne
		{
			get
			{
				return ClientStrings.ResourceManager.GetString("SelectOne");
			}
		}

		// Token: 0x170001CC RID: 460
		// (get) Token: 0x06000208 RID: 520 RVA: 0x00006FBF File Offset: 0x000051BF
		public static string NtlmAuth
		{
			get
			{
				return ClientStrings.ResourceManager.GetString("NtlmAuth");
			}
		}

		// Token: 0x170001CD RID: 461
		// (get) Token: 0x06000209 RID: 521 RVA: 0x00006FD0 File Offset: 0x000051D0
		public static string EnableMOWAConfirm
		{
			get
			{
				return ClientStrings.ResourceManager.GetString("EnableMOWAConfirm");
			}
		}

		// Token: 0x170001CE RID: 462
		// (get) Token: 0x0600020A RID: 522 RVA: 0x00006FE1 File Offset: 0x000051E1
		public static string Pop
		{
			get
			{
				return ClientStrings.ResourceManager.GetString("Pop");
			}
		}

		// Token: 0x170001CF RID: 463
		// (get) Token: 0x0600020B RID: 523 RVA: 0x00006FF2 File Offset: 0x000051F2
		public static string DisabledPendingDisplayText
		{
			get
			{
				return ClientStrings.ResourceManager.GetString("DisabledPendingDisplayText");
			}
		}

		// Token: 0x170001D0 RID: 464
		// (get) Token: 0x0600020C RID: 524 RVA: 0x00007003 File Offset: 0x00005203
		public static string FirstFocusTextForScreenReader
		{
			get
			{
				return ClientStrings.ResourceManager.GetString("FirstFocusTextForScreenReader");
			}
		}

		// Token: 0x170001D1 RID: 465
		// (get) Token: 0x0600020D RID: 525 RVA: 0x00007014 File Offset: 0x00005214
		public static string MoveUp
		{
			get
			{
				return ClientStrings.ResourceManager.GetString("MoveUp");
			}
		}

		// Token: 0x170001D2 RID: 466
		// (get) Token: 0x0600020E RID: 526 RVA: 0x00007025 File Offset: 0x00005225
		public static string GalOrPersonalContactText
		{
			get
			{
				return ClientStrings.ResourceManager.GetString("GalOrPersonalContactText");
			}
		}

		// Token: 0x170001D3 RID: 467
		// (get) Token: 0x0600020F RID: 527 RVA: 0x00007036 File Offset: 0x00005236
		public static string AllAvailableIPV4Address
		{
			get
			{
				return ClientStrings.ResourceManager.GetString("AllAvailableIPV4Address");
			}
		}

		// Token: 0x170001D4 RID: 468
		// (get) Token: 0x06000210 RID: 528 RVA: 0x00007047 File Offset: 0x00005247
		public static string OwaExternal
		{
			get
			{
				return ClientStrings.ResourceManager.GetString("OwaExternal");
			}
		}

		// Token: 0x170001D5 RID: 469
		// (get) Token: 0x06000211 RID: 529 RVA: 0x00007058 File Offset: 0x00005258
		public static string ActiveSyncEnableText
		{
			get
			{
				return ClientStrings.ResourceManager.GetString("ActiveSyncEnableText");
			}
		}

		// Token: 0x170001D6 RID: 470
		// (get) Token: 0x06000212 RID: 530 RVA: 0x00007069 File Offset: 0x00005269
		public static string ConstraintViolationInputUnlimitedValue
		{
			get
			{
				return ClientStrings.ResourceManager.GetString("ConstraintViolationInputUnlimitedValue");
			}
		}

		// Token: 0x170001D7 RID: 471
		// (get) Token: 0x06000213 RID: 531 RVA: 0x0000707A File Offset: 0x0000527A
		public static string ResetPinMessageTitle
		{
			get
			{
				return ClientStrings.ResourceManager.GetString("ResetPinMessageTitle");
			}
		}

		// Token: 0x170001D8 RID: 472
		// (get) Token: 0x06000214 RID: 532 RVA: 0x0000708B File Offset: 0x0000528B
		public static string No
		{
			get
			{
				return ClientStrings.ResourceManager.GetString("No");
			}
		}

		// Token: 0x170001D9 RID: 473
		// (get) Token: 0x06000215 RID: 533 RVA: 0x0000709C File Offset: 0x0000529C
		public static string LongRunStoppedDescription
		{
			get
			{
				return ClientStrings.ResourceManager.GetString("LongRunStoppedDescription");
			}
		}

		// Token: 0x170001DA RID: 474
		// (get) Token: 0x06000216 RID: 534 RVA: 0x000070AD File Offset: 0x000052AD
		public static string NIAE
		{
			get
			{
				return ClientStrings.ResourceManager.GetString("NIAE");
			}
		}

		// Token: 0x06000217 RID: 535 RVA: 0x000070BE File Offset: 0x000052BE
		public static string BulkEditedProgressNoSuccessFailedCount(int m)
		{
			return string.Format(ClientStrings.ResourceManager.GetString("BulkEditedProgressNoSuccessFailedCount"), m);
		}

		// Token: 0x06000218 RID: 536 RVA: 0x000070DA File Offset: 0x000052DA
		public static string ConstraintViolationStringLengthTooLong(int maxLength, int realLength)
		{
			return string.Format(ClientStrings.ResourceManager.GetString("ConstraintViolationStringLengthTooLong"), maxLength, realLength);
		}

		// Token: 0x170001DB RID: 475
		// (get) Token: 0x06000219 RID: 537 RVA: 0x000070FC File Offset: 0x000052FC
		public static string CtrlKeyCloseForm
		{
			get
			{
				return ClientStrings.ResourceManager.GetString("CtrlKeyCloseForm");
			}
		}

		// Token: 0x170001DC RID: 476
		// (get) Token: 0x0600021A RID: 538 RVA: 0x0000710D File Offset: 0x0000530D
		public static string InvalidUrl
		{
			get
			{
				return ClientStrings.ResourceManager.GetString("InvalidUrl");
			}
		}

		// Token: 0x170001DD RID: 477
		// (get) Token: 0x0600021B RID: 539 RVA: 0x0000711E File Offset: 0x0000531E
		public static string InvalidDomain
		{
			get
			{
				return ClientStrings.ResourceManager.GetString("InvalidDomain");
			}
		}

		// Token: 0x170001DE RID: 478
		// (get) Token: 0x0600021C RID: 540 RVA: 0x0000712F File Offset: 0x0000532F
		public static string Display
		{
			get
			{
				return ClientStrings.ResourceManager.GetString("Display");
			}
		}

		// Token: 0x170001DF RID: 479
		// (get) Token: 0x0600021D RID: 541 RVA: 0x00007140 File Offset: 0x00005340
		public static string SslTitle
		{
			get
			{
				return ClientStrings.ResourceManager.GetString("SslTitle");
			}
		}

		// Token: 0x170001E0 RID: 480
		// (get) Token: 0x0600021E RID: 542 RVA: 0x00007151 File Offset: 0x00005351
		public static string ProviderDelayed
		{
			get
			{
				return ClientStrings.ResourceManager.GetString("ProviderDelayed");
			}
		}

		// Token: 0x170001E1 RID: 481
		// (get) Token: 0x0600021F RID: 543 RVA: 0x00007162 File Offset: 0x00005362
		public static string TrialExpiredWarningText
		{
			get
			{
				return ClientStrings.ResourceManager.GetString("TrialExpiredWarningText");
			}
		}

		// Token: 0x170001E2 RID: 482
		// (get) Token: 0x06000220 RID: 544 RVA: 0x00007173 File Offset: 0x00005373
		public static string StopProcessingRuleLabel
		{
			get
			{
				return ClientStrings.ResourceManager.GetString("StopProcessingRuleLabel");
			}
		}

		// Token: 0x170001E3 RID: 483
		// (get) Token: 0x06000221 RID: 545 RVA: 0x00007184 File Offset: 0x00005384
		public static string UMEnabledPolicyRequired
		{
			get
			{
				return ClientStrings.ResourceManager.GetString("UMEnabledPolicyRequired");
			}
		}

		// Token: 0x170001E4 RID: 484
		// (get) Token: 0x06000222 RID: 546 RVA: 0x00007195 File Offset: 0x00005395
		public static string All
		{
			get
			{
				return ClientStrings.ResourceManager.GetString("All");
			}
		}

		// Token: 0x170001E5 RID: 485
		// (get) Token: 0x06000223 RID: 547 RVA: 0x000071A6 File Offset: 0x000053A6
		public static string ClickHereForHelp
		{
			get
			{
				return ClientStrings.ResourceManager.GetString("ClickHereForHelp");
			}
		}

		// Token: 0x170001E6 RID: 486
		// (get) Token: 0x06000224 RID: 548 RVA: 0x000071B7 File Offset: 0x000053B7
		public static string LongRunCompletedDescription
		{
			get
			{
				return ClientStrings.ResourceManager.GetString("LongRunCompletedDescription");
			}
		}

		// Token: 0x170001E7 RID: 487
		// (get) Token: 0x06000225 RID: 549 RVA: 0x000071C8 File Offset: 0x000053C8
		public static string ConnectToO365
		{
			get
			{
				return ClientStrings.ResourceManager.GetString("ConnectToO365");
			}
		}

		// Token: 0x170001E8 RID: 488
		// (get) Token: 0x06000226 RID: 550 RVA: 0x000071D9 File Offset: 0x000053D9
		public static string NoRetentionPolicy
		{
			get
			{
				return ClientStrings.ResourceManager.GetString("NoRetentionPolicy");
			}
		}

		// Token: 0x06000227 RID: 551 RVA: 0x000071EA File Offset: 0x000053EA
		public static string BulkEditedProgress(int m, int n, int o)
		{
			return string.Format(ClientStrings.ResourceManager.GetString("BulkEditedProgress"), m, n, o);
		}

		// Token: 0x170001E9 RID: 489
		// (get) Token: 0x06000228 RID: 552 RVA: 0x00007212 File Offset: 0x00005412
		public static string SavingInformation
		{
			get
			{
				return ClientStrings.ResourceManager.GetString("SavingInformation");
			}
		}

		// Token: 0x170001EA RID: 490
		// (get) Token: 0x06000229 RID: 553 RVA: 0x00007223 File Offset: 0x00005423
		public static string InvalidAlias
		{
			get
			{
				return ClientStrings.ResourceManager.GetString("InvalidAlias");
			}
		}

		// Token: 0x170001EB RID: 491
		// (get) Token: 0x0600022A RID: 554 RVA: 0x00007234 File Offset: 0x00005434
		public static string Contributor
		{
			get
			{
				return ClientStrings.ResourceManager.GetString("Contributor");
			}
		}

		// Token: 0x170001EC RID: 492
		// (get) Token: 0x0600022B RID: 555 RVA: 0x00007245 File Offset: 0x00005445
		public static string Cancel
		{
			get
			{
				return ClientStrings.ResourceManager.GetString("Cancel");
			}
		}

		// Token: 0x170001ED RID: 493
		// (get) Token: 0x0600022C RID: 556 RVA: 0x00007256 File Offset: 0x00005456
		public static string NoIPWarning
		{
			get
			{
				return ClientStrings.ResourceManager.GetString("NoIPWarning");
			}
		}

		// Token: 0x0600022D RID: 557 RVA: 0x00007267 File Offset: 0x00005467
		public static string UnhandledExceptionDetails(string msg)
		{
			return string.Format(ClientStrings.ResourceManager.GetString("UnhandledExceptionDetails"), msg);
		}

		// Token: 0x170001EE RID: 494
		// (get) Token: 0x0600022E RID: 558 RVA: 0x0000727E File Offset: 0x0000547E
		public static string Custom
		{
			get
			{
				return ClientStrings.ResourceManager.GetString("Custom");
			}
		}

		// Token: 0x0600022F RID: 559 RVA: 0x0000728F File Offset: 0x0000548F
		public static string SendPasscodeSucceededFormat(string phone)
		{
			return string.Format(ClientStrings.ResourceManager.GetString("SendPasscodeSucceededFormat"), phone);
		}

		// Token: 0x170001EF RID: 495
		// (get) Token: 0x06000230 RID: 560 RVA: 0x000072A6 File Offset: 0x000054A6
		public static string RemovePendingDisplayText
		{
			get
			{
				return ClientStrings.ResourceManager.GetString("RemovePendingDisplayText");
			}
		}

		// Token: 0x170001F0 RID: 496
		// (get) Token: 0x06000231 RID: 561 RVA: 0x000072B7 File Offset: 0x000054B7
		public static string EnabledPendingDisplayText
		{
			get
			{
				return ClientStrings.ResourceManager.GetString("EnabledPendingDisplayText");
			}
		}

		// Token: 0x170001F1 RID: 497
		// (get) Token: 0x06000232 RID: 562 RVA: 0x000072C8 File Offset: 0x000054C8
		public static string RecordGreetingLinkText
		{
			get
			{
				return ClientStrings.ResourceManager.GetString("RecordGreetingLinkText");
			}
		}

		// Token: 0x170001F2 RID: 498
		// (get) Token: 0x06000233 RID: 563 RVA: 0x000072D9 File Offset: 0x000054D9
		public static string Next
		{
			get
			{
				return ClientStrings.ResourceManager.GetString("Next");
			}
		}

		// Token: 0x170001F3 RID: 499
		// (get) Token: 0x06000234 RID: 564 RVA: 0x000072EA File Offset: 0x000054EA
		public static string PrimaryNavigation
		{
			get
			{
				return ClientStrings.ResourceManager.GetString("PrimaryNavigation");
			}
		}

		// Token: 0x06000235 RID: 565 RVA: 0x000072FB File Offset: 0x000054FB
		public static string DeliveryReportEmailSubject(string recipient)
		{
			return string.Format(ClientStrings.ResourceManager.GetString("DeliveryReportEmailSubject"), recipient);
		}

		// Token: 0x170001F4 RID: 500
		// (get) Token: 0x06000236 RID: 566 RVA: 0x00007312 File Offset: 0x00005512
		public static string KeyMappingVoiceMailDisplayText
		{
			get
			{
				return ClientStrings.ResourceManager.GetString("KeyMappingVoiceMailDisplayText");
			}
		}

		// Token: 0x06000237 RID: 567 RVA: 0x00007323 File Offset: 0x00005523
		public static string DuplicatedQueryFieldName(string queryFieldName)
		{
			return string.Format(ClientStrings.ResourceManager.GetString("DuplicatedQueryFieldName"), queryFieldName);
		}

		// Token: 0x170001F5 RID: 501
		// (get) Token: 0x06000238 RID: 568 RVA: 0x0000733A File Offset: 0x0000553A
		public static string WarningPanelMultipleWarningsMsg
		{
			get
			{
				return ClientStrings.ResourceManager.GetString("WarningPanelMultipleWarningsMsg");
			}
		}

		// Token: 0x170001F6 RID: 502
		// (get) Token: 0x06000239 RID: 569 RVA: 0x0000734B File Offset: 0x0000554B
		public static string RequestMalwareDetail
		{
			get
			{
				return ClientStrings.ResourceManager.GetString("RequestMalwareDetail");
			}
		}

		// Token: 0x170001F7 RID: 503
		// (get) Token: 0x0600023A RID: 570 RVA: 0x0000735C File Offset: 0x0000555C
		public static string Information
		{
			get
			{
				return ClientStrings.ResourceManager.GetString("Information");
			}
		}

		// Token: 0x170001F8 RID: 504
		// (get) Token: 0x0600023B RID: 571 RVA: 0x0000736D File Offset: 0x0000556D
		public static string RequestRuleDetail
		{
			get
			{
				return ClientStrings.ResourceManager.GetString("RequestRuleDetail");
			}
		}

		// Token: 0x170001F9 RID: 505
		// (get) Token: 0x0600023C RID: 572 RVA: 0x0000737E File Offset: 0x0000557E
		public static string OutsideWorkingHoursText
		{
			get
			{
				return ClientStrings.ResourceManager.GetString("OutsideWorkingHoursText");
			}
		}

		// Token: 0x170001FA RID: 506
		// (get) Token: 0x0600023D RID: 573 RVA: 0x0000738F File Offset: 0x0000558F
		public static string UMDialPlanRequiredField
		{
			get
			{
				return ClientStrings.ResourceManager.GetString("UMDialPlanRequiredField");
			}
		}

		// Token: 0x170001FB RID: 507
		// (get) Token: 0x0600023E RID: 574 RVA: 0x000073A0 File Offset: 0x000055A0
		public static string DisableFederationCompleted
		{
			get
			{
				return ClientStrings.ResourceManager.GetString("DisableFederationCompleted");
			}
		}

		// Token: 0x170001FC RID: 508
		// (get) Token: 0x0600023F RID: 575 RVA: 0x000073B1 File Offset: 0x000055B1
		public static string SharingRuleEntryDomainConflict
		{
			get
			{
				return ClientStrings.ResourceManager.GetString("SharingRuleEntryDomainConflict");
			}
		}

		// Token: 0x170001FD RID: 509
		// (get) Token: 0x06000240 RID: 576 RVA: 0x000073C2 File Offset: 0x000055C2
		public static string EditSharingEnabledDomainsStep2
		{
			get
			{
				return ClientStrings.ResourceManager.GetString("EditSharingEnabledDomainsStep2");
			}
		}

		// Token: 0x170001FE RID: 510
		// (get) Token: 0x06000241 RID: 577 RVA: 0x000073D3 File Offset: 0x000055D3
		public static string NoSpaceValidatorMessage
		{
			get
			{
				return ClientStrings.ResourceManager.GetString("NoSpaceValidatorMessage");
			}
		}

		// Token: 0x170001FF RID: 511
		// (get) Token: 0x06000242 RID: 578 RVA: 0x000073E4 File Offset: 0x000055E4
		public static string WaitForHybridUIReady
		{
			get
			{
				return ClientStrings.ResourceManager.GetString("WaitForHybridUIReady");
			}
		}

		// Token: 0x17000200 RID: 512
		// (get) Token: 0x06000243 RID: 579 RVA: 0x000073F5 File Offset: 0x000055F5
		public static string MailboxDelegation
		{
			get
			{
				return ClientStrings.ResourceManager.GetString("MailboxDelegation");
			}
		}

		// Token: 0x06000244 RID: 580 RVA: 0x00007406 File Offset: 0x00005606
		public static string UMKeyMappingActionSummaryLeaveVM(string name)
		{
			return string.Format(ClientStrings.ResourceManager.GetString("UMKeyMappingActionSummaryLeaveVM"), name);
		}

		// Token: 0x17000201 RID: 513
		// (get) Token: 0x06000245 RID: 581 RVA: 0x0000741D File Offset: 0x0000561D
		public static string OwaMailboxPolicySecurity
		{
			get
			{
				return ClientStrings.ResourceManager.GetString("OwaMailboxPolicySecurity");
			}
		}

		// Token: 0x17000202 RID: 514
		// (get) Token: 0x06000246 RID: 582 RVA: 0x0000742E File Offset: 0x0000562E
		public static string SenderRequiredErrorMessage
		{
			get
			{
				return ClientStrings.ResourceManager.GetString("SenderRequiredErrorMessage");
			}
		}

		// Token: 0x17000203 RID: 515
		// (get) Token: 0x06000247 RID: 583 RVA: 0x0000743F File Offset: 0x0000563F
		public static string ServerTrailDaysColumnText
		{
			get
			{
				return ClientStrings.ResourceManager.GetString("ServerTrailDaysColumnText");
			}
		}

		// Token: 0x17000204 RID: 516
		// (get) Token: 0x06000248 RID: 584 RVA: 0x00007450 File Offset: 0x00005650
		public static string ResetPinMessage
		{
			get
			{
				return ClientStrings.ResourceManager.GetString("ResetPinMessage");
			}
		}

		// Token: 0x17000205 RID: 517
		// (get) Token: 0x06000249 RID: 585 RVA: 0x00007461 File Offset: 0x00005661
		public static string RemoveDisabledFacebookConnectionText
		{
			get
			{
				return ClientStrings.ResourceManager.GetString("RemoveDisabledFacebookConnectionText");
			}
		}

		// Token: 0x0600024A RID: 586 RVA: 0x00007472 File Offset: 0x00005672
		public static string InvalidIPAddress(string str)
		{
			return string.Format(ClientStrings.ResourceManager.GetString("InvalidIPAddress"), str);
		}

		// Token: 0x17000206 RID: 518
		// (get) Token: 0x0600024B RID: 587 RVA: 0x00007489 File Offset: 0x00005689
		public static string ReportTitleMissing
		{
			get
			{
				return ClientStrings.ResourceManager.GetString("ReportTitleMissing");
			}
		}

		// Token: 0x17000207 RID: 519
		// (get) Token: 0x0600024C RID: 588 RVA: 0x0000749A File Offset: 0x0000569A
		public static string OwaMailboxPolicyLinkedIn
		{
			get
			{
				return ClientStrings.ResourceManager.GetString("OwaMailboxPolicyLinkedIn");
			}
		}

		// Token: 0x17000208 RID: 520
		// (get) Token: 0x0600024D RID: 589 RVA: 0x000074AB File Offset: 0x000056AB
		public static string ConstraintViolationValueOutOfRange
		{
			get
			{
				return ClientStrings.ResourceManager.GetString("ConstraintViolationValueOutOfRange");
			}
		}

		// Token: 0x17000209 RID: 521
		// (get) Token: 0x0600024E RID: 590 RVA: 0x000074BC File Offset: 0x000056BC
		public static string MTRTDetailsTitle
		{
			get
			{
				return ClientStrings.ResourceManager.GetString("MTRTDetailsTitle");
			}
		}

		// Token: 0x1700020A RID: 522
		// (get) Token: 0x0600024F RID: 591 RVA: 0x000074CD File Offset: 0x000056CD
		public static string Column
		{
			get
			{
				return ClientStrings.ResourceManager.GetString("Column");
			}
		}

		// Token: 0x1700020B RID: 523
		// (get) Token: 0x06000250 RID: 592 RVA: 0x000074DE File Offset: 0x000056DE
		public static string ClickToShowText
		{
			get
			{
				return ClientStrings.ResourceManager.GetString("ClickToShowText");
			}
		}

		// Token: 0x1700020C RID: 524
		// (get) Token: 0x06000251 RID: 593 RVA: 0x000074EF File Offset: 0x000056EF
		public static string HybridConfiguration
		{
			get
			{
				return ClientStrings.ResourceManager.GetString("HybridConfiguration");
			}
		}

		// Token: 0x1700020D RID: 525
		// (get) Token: 0x06000252 RID: 594 RVA: 0x00007500 File Offset: 0x00005700
		public static string PWTAS
		{
			get
			{
				return ClientStrings.ResourceManager.GetString("PWTAS");
			}
		}

		// Token: 0x1700020E RID: 526
		// (get) Token: 0x06000253 RID: 595 RVA: 0x00007511 File Offset: 0x00005711
		public static string ClickToCopyToClipboard
		{
			get
			{
				return ClientStrings.ResourceManager.GetString("ClickToCopyToClipboard");
			}
		}

		// Token: 0x1700020F RID: 527
		// (get) Token: 0x06000254 RID: 596 RVA: 0x00007522 File Offset: 0x00005722
		public static string UriKindAbsolute
		{
			get
			{
				return ClientStrings.ResourceManager.GetString("UriKindAbsolute");
			}
		}

		// Token: 0x06000255 RID: 597 RVA: 0x00007533 File Offset: 0x00005733
		public static string GuideToSubscriptionPagePopOrImapOnly(string url, string text, string onClick)
		{
			return string.Format(ClientStrings.ResourceManager.GetString("GuideToSubscriptionPagePopOrImapOnly"), url, text, onClick);
		}

		// Token: 0x17000210 RID: 528
		// (get) Token: 0x06000256 RID: 598 RVA: 0x0000754C File Offset: 0x0000574C
		public static string VoicemailResetPINConfirmationMessage
		{
			get
			{
				return ClientStrings.ResourceManager.GetString("VoicemailResetPINConfirmationMessage");
			}
		}

		// Token: 0x17000211 RID: 529
		// (get) Token: 0x06000257 RID: 599 RVA: 0x0000755D File Offset: 0x0000575D
		public static string ConnectMailboxWizardCaption
		{
			get
			{
				return ClientStrings.ResourceManager.GetString("ConnectMailboxWizardCaption");
			}
		}

		// Token: 0x17000212 RID: 530
		// (get) Token: 0x06000258 RID: 600 RVA: 0x0000756E File Offset: 0x0000576E
		public static string MobileInternal
		{
			get
			{
				return ClientStrings.ResourceManager.GetString("MobileInternal");
			}
		}

		// Token: 0x17000213 RID: 531
		// (get) Token: 0x06000259 RID: 601 RVA: 0x0000757F File Offset: 0x0000577F
		public static string RetrievingStatistics
		{
			get
			{
				return ClientStrings.ResourceManager.GetString("RetrievingStatistics");
			}
		}

		// Token: 0x17000214 RID: 532
		// (get) Token: 0x0600025A RID: 602 RVA: 0x00007590 File Offset: 0x00005790
		public static string WebServiceRequestTimeout
		{
			get
			{
				return ClientStrings.ResourceManager.GetString("WebServiceRequestTimeout");
			}
		}

		// Token: 0x17000215 RID: 533
		// (get) Token: 0x0600025B RID: 603 RVA: 0x000075A1 File Offset: 0x000057A1
		public static string UMEnableTelexActionSummary
		{
			get
			{
				return ClientStrings.ResourceManager.GetString("UMEnableTelexActionSummary");
			}
		}

		// Token: 0x0600025C RID: 604 RVA: 0x000075B2 File Offset: 0x000057B2
		public static string ConstraintViolationStringDoesNotContainNonWhitespaceCharacter(string str)
		{
			return string.Format(ClientStrings.ResourceManager.GetString("ConstraintViolationStringDoesNotContainNonWhitespaceCharacter"), str);
		}

		// Token: 0x17000216 RID: 534
		// (get) Token: 0x0600025D RID: 605 RVA: 0x000075C9 File Offset: 0x000057C9
		public static string PermissionLevelWarning
		{
			get
			{
				return ClientStrings.ResourceManager.GetString("PermissionLevelWarning");
			}
		}

		// Token: 0x17000217 RID: 535
		// (get) Token: 0x0600025E RID: 606 RVA: 0x000075DA File Offset: 0x000057DA
		public static string GetAllResults
		{
			get
			{
				return ClientStrings.ResourceManager.GetString("GetAllResults");
			}
		}

		// Token: 0x0600025F RID: 607 RVA: 0x000075EB File Offset: 0x000057EB
		public static string MultipleRecipientsSelected(int n)
		{
			return string.Format(ClientStrings.ResourceManager.GetString("MultipleRecipientsSelected"), n);
		}

		// Token: 0x06000260 RID: 608 RVA: 0x00007607 File Offset: 0x00005807
		public static string ConstraintViolationStringDoesContainsNonASCIICharacter(string value)
		{
			return string.Format(ClientStrings.ResourceManager.GetString("ConstraintViolationStringDoesContainsNonASCIICharacter"), value);
		}

		// Token: 0x17000218 RID: 536
		// (get) Token: 0x06000261 RID: 609 RVA: 0x0000761E File Offset: 0x0000581E
		public static string LongRunCopyToClipboardLabel
		{
			get
			{
				return ClientStrings.ResourceManager.GetString("LongRunCopyToClipboardLabel");
			}
		}

		// Token: 0x06000262 RID: 610 RVA: 0x0000762F File Offset: 0x0000582F
		public static string UMEnablePinValidation(int length)
		{
			return string.Format(ClientStrings.ResourceManager.GetString("UMEnablePinValidation"), length);
		}

		// Token: 0x17000219 RID: 537
		// (get) Token: 0x06000263 RID: 611 RVA: 0x0000764B File Offset: 0x0000584B
		public static string EnableFederationCompleted
		{
			get
			{
				return ClientStrings.ResourceManager.GetString("EnableFederationCompleted");
			}
		}

		// Token: 0x1700021A RID: 538
		// (get) Token: 0x06000264 RID: 612 RVA: 0x0000765C File Offset: 0x0000585C
		public static string OABInternal
		{
			get
			{
				return ClientStrings.ResourceManager.GetString("OABInternal");
			}
		}

		// Token: 0x06000265 RID: 613 RVA: 0x0000766D File Offset: 0x0000586D
		public static string QueryValNotInValidRange(string queryValue, string queryName)
		{
			return string.Format(ClientStrings.ResourceManager.GetString("QueryValNotInValidRange"), queryValue, queryName);
		}

		// Token: 0x06000266 RID: 614 RVA: 0x00007685 File Offset: 0x00005885
		public static string ConstraintViolationStringMustNotContainSpecificCharacters(string characters, string value)
		{
			return string.Format(ClientStrings.ResourceManager.GetString("ConstraintViolationStringMustNotContainSpecificCharacters"), characters, value);
		}

		// Token: 0x1700021B RID: 539
		// (get) Token: 0x06000267 RID: 615 RVA: 0x0000769D File Offset: 0x0000589D
		public static string OwaMailboxPolicyCalendar
		{
			get
			{
				return ClientStrings.ResourceManager.GetString("OwaMailboxPolicyCalendar");
			}
		}

		// Token: 0x06000268 RID: 616 RVA: 0x000076AE File Offset: 0x000058AE
		public static string GetLocalizedString(ClientStrings.IDs key)
		{
			return ClientStrings.ResourceManager.GetString(ClientStrings.stringIDs[(uint)key]);
		}

		// Token: 0x04000001 RID: 1
		private static Dictionary<uint, string> stringIDs = new Dictionary<uint, string>(539);

		// Token: 0x04000002 RID: 2
		private static ResourceManager ResourceManager = new ResourceManager("Microsoft.Exchange.Management.ControlPanel.ClientStrings", typeof(ClientStrings).GetTypeInfo().Assembly);

		// Token: 0x02000003 RID: 3
		public enum IDs : uint
		{
			// Token: 0x04000004 RID: 4
			ReconnectProviderCommandText = 616132976U,
			// Token: 0x04000005 RID: 5
			FieldsInError = 1708942702U,
			// Token: 0x04000006 RID: 6
			TlsTitle = 1038968483U,
			// Token: 0x04000007 RID: 7
			UMKeyMappingTimeout = 2085954050U,
			// Token: 0x04000008 RID: 8
			RequiredFieldValidatorErrorMessage = 4245786716U,
			// Token: 0x04000009 RID: 9
			OwaMailboxPolicyTasks = 385068607U,
			// Token: 0x0400000A RID: 10
			CopyIsIEOnly = 375881263U,
			// Token: 0x0400000B RID: 11
			MinimumCriteriaFieldsInErrorDeliveryStatus = 1064772435U,
			// Token: 0x0400000C RID: 12
			EnterHybridUIConfirm = 1884426512U,
			// Token: 0x0400000D RID: 13
			DisableCommandText = 1692971068U,
			// Token: 0x0400000E RID: 14
			UMHolidayScheduleHolidayStartDateRequiredError = 1752619070U,
			// Token: 0x0400000F RID: 15
			EnterHybridUIButtonText = 3063981087U,
			// Token: 0x04000010 RID: 16
			ConstraintViolationValueOutOfRangeForQuota = 3891394090U,
			// Token: 0x04000011 RID: 17
			HydratingMessage = 1245979421U,
			// Token: 0x04000012 RID: 18
			GroupNamingPolicyPreviewDescriptionHeader = 3561883164U,
			// Token: 0x04000013 RID: 19
			Validating = 866598955U,
			// Token: 0x04000014 RID: 20
			UriKindRelative = 1125975266U,
			// Token: 0x04000015 RID: 21
			Close = 3033402446U,
			// Token: 0x04000016 RID: 22
			MoveDown = 3496306419U,
			// Token: 0x04000017 RID: 23
			PeopleConnectBusy = 155777604U,
			// Token: 0x04000018 RID: 24
			LegacyRegExEnabledRuleLabel = 509325205U,
			// Token: 0x04000019 RID: 25
			HydrationDataLossWarning = 1609794183U,
			// Token: 0x0400001A RID: 26
			NoTreeItem = 1398029738U,
			// Token: 0x0400001B RID: 27
			LearnMore = 1133914413U,
			// Token: 0x0400001C RID: 28
			AddEAPCondtionButtonText = 1032072896U,
			// Token: 0x0400001D RID: 29
			InvalidDateRange = 848625314U,
			// Token: 0x0400001E RID: 30
			DefaultRuleEditorCaption = 2717328418U,
			// Token: 0x0400001F RID: 31
			CtrlKeyGoToSearch = 3701726907U,
			// Token: 0x04000020 RID: 32
			DisableFVA = 3084590849U,
			// Token: 0x04000021 RID: 33
			Searching = 3237053118U,
			// Token: 0x04000022 RID: 34
			Update = 859380341U,
			// Token: 0x04000023 RID: 35
			CurrentPolicyCaption = 2781755715U,
			// Token: 0x04000024 RID: 36
			FollowedByColon = 4233379362U,
			// Token: 0x04000025 RID: 37
			EnabledDisplayText = 1281963896U,
			// Token: 0x04000026 RID: 38
			OffDisplayText = 2288607912U,
			// Token: 0x04000027 RID: 39
			OwaMailboxPolicyActiveSyncIntegration = 3866481608U,
			// Token: 0x04000028 RID: 40
			PlayOnPhoneDisconnecting = 915129961U,
			// Token: 0x04000029 RID: 41
			LegacyOUError = 4221859415U,
			// Token: 0x0400002A RID: 42
			WebServiceRequestServerError = 1158475769U,
			// Token: 0x0400002B RID: 43
			Warning = 1861340610U,
			// Token: 0x0400002C RID: 44
			BlockedPendingDisplayText = 2089076466U,
			// Token: 0x0400002D RID: 45
			MessageTypePickerInvalid = 4153102186U,
			// Token: 0x0400002E RID: 46
			OwaMailboxPolicyTextMessaging = 1693255708U,
			// Token: 0x0400002F RID: 47
			OwaMailboxPolicyContacts = 717626182U,
			// Token: 0x04000030 RID: 48
			Expand = 2200441302U,
			// Token: 0x04000031 RID: 49
			DisableConnectorConfirm = 2959308597U,
			// Token: 0x04000032 RID: 50
			CmdLogTitleForHybridEnterprise = 2142856376U,
			// Token: 0x04000033 RID: 51
			ProviderConnectedWithError = 3292878272U,
			// Token: 0x04000034 RID: 52
			RequestSpamDetail = 3612561219U,
			// Token: 0x04000035 RID: 53
			None = 1414246128U,
			// Token: 0x04000036 RID: 54
			PassiveText = 94147420U,
			// Token: 0x04000037 RID: 55
			DisableFederationInProgress = 506561007U,
			// Token: 0x04000038 RID: 56
			MobileDeviceDisableText = 1174301401U,
			// Token: 0x04000039 RID: 57
			MoreOptions = 3442569333U,
			// Token: 0x0400003A RID: 58
			MidnightAM = 2522455672U,
			// Token: 0x0400003B RID: 59
			NotificationCount = 4104581892U,
			// Token: 0x0400003C RID: 60
			ContactsSharing = 906828259U,
			// Token: 0x0400003D RID: 61
			LongRunInProgressDescription = 1644825107U,
			// Token: 0x0400003E RID: 62
			MailboxToSearchRequiredErrorMessage = 2545492551U,
			// Token: 0x0400003F RID: 63
			DomainNoValue = 321700842U,
			// Token: 0x04000040 RID: 64
			MyOptions = 3455735334U,
			// Token: 0x04000041 RID: 65
			VoicemailConfigurationDetails = 335355257U,
			// Token: 0x04000042 RID: 66
			CloseWindowOnLogout = 1803016445U,
			// Token: 0x04000043 RID: 67
			CustomizeColumns = 3832063564U,
			// Token: 0x04000044 RID: 68
			EnterProductKey = 3018361386U,
			// Token: 0x04000045 RID: 69
			PlayOnPhoneDialing = 4158450825U,
			// Token: 0x04000046 RID: 70
			OwaMailboxPolicyUMIntegration = 207011947U,
			// Token: 0x04000047 RID: 71
			HydrationFailed = 1066902945U,
			// Token: 0x04000048 RID: 72
			EnableActiveSyncConfirm = 506890308U,
			// Token: 0x04000049 RID: 73
			ConstraintViolationStringLengthIsEmpty = 3423705850U,
			// Token: 0x0400004A RID: 74
			SelectOneLink = 2455574594U,
			// Token: 0x0400004B RID: 75
			ConstraintNotNullOrEmpty = 2336951795U,
			// Token: 0x0400004C RID: 76
			LitigationHoldOwnerNotSet = 4193083863U,
			// Token: 0x0400004D RID: 77
			RequiredFieldIndicator = 1189851274U,
			// Token: 0x0400004E RID: 78
			FolderTree = 3508492594U,
			// Token: 0x0400004F RID: 79
			IncidentReportSelectAll = 3771622829U,
			// Token: 0x04000050 RID: 80
			Notification = 3819852093U,
			// Token: 0x04000051 RID: 81
			HydrationDoneFeatureFailed = 2571312913U,
			// Token: 0x04000052 RID: 82
			LongRunWarningLabel = 1749577099U,
			// Token: 0x04000053 RID: 83
			PublicFoldersEmptyDataTextRoot = 1796910490U,
			// Token: 0x04000054 RID: 84
			Unsuccessful = 1517827063U,
			// Token: 0x04000055 RID: 85
			TextMessagingNotificationNotSetupText = 3951041989U,
			// Token: 0x04000056 RID: 86
			VoicemailConfigurationConfirmationMessage = 879792491U,
			// Token: 0x04000057 RID: 87
			EnableFederationInProgress = 865985204U,
			// Token: 0x04000058 RID: 88
			OwaMailboxPolicyAllowCopyContactsToDeviceAddressBook = 3625629356U,
			// Token: 0x04000059 RID: 89
			OwaMailboxPolicyInformationManagement = 2357636744U,
			// Token: 0x0400005A RID: 90
			WarningPanelDisMissMsg = 3506211787U,
			// Token: 0x0400005B RID: 91
			OwaMailboxPolicyJournal = 1523028278U,
			// Token: 0x0400005C RID: 92
			DatesNotDefined = 993523547U,
			// Token: 0x0400005D RID: 93
			EnableOWAConfirm = 4062764466U,
			// Token: 0x0400005E RID: 94
			CancelWipePendingDisplayText = 2266587541U,
			// Token: 0x0400005F RID: 95
			DeliveryReportSearchFieldsInError = 2546927256U,
			// Token: 0x04000060 RID: 96
			MyOrganization = 2954372719U,
			// Token: 0x04000061 RID: 97
			Today = 3927445923U,
			// Token: 0x04000062 RID: 98
			ExtendedReportsInsufficientData = 1992800455U,
			// Token: 0x04000063 RID: 99
			EnableConnectorLoggingConfirm = 3963002503U,
			// Token: 0x04000064 RID: 100
			MessageTraceInvalidEndDate = 2805968874U,
			// Token: 0x04000065 RID: 101
			AddSubnetCaption = 1788720538U,
			// Token: 0x04000066 RID: 102
			CustomizeSenderLabel = 4091981230U,
			// Token: 0x04000067 RID: 103
			SharedUMAutoAttendantPilotIdentifierListE164Error = 233002034U,
			// Token: 0x04000068 RID: 104
			PreviousMonth = 1562506021U,
			// Token: 0x04000069 RID: 105
			Stop = 3833953726U,
			// Token: 0x0400006A RID: 106
			AllAvailableIPV6Address = 3958687323U,
			// Token: 0x0400006B RID: 107
			LetCallersInterruptGreetingsText = 3462654059U,
			// Token: 0x0400006C RID: 108
			GroupNamingPolicyEditorPrefixLabel = 3386445864U,
			// Token: 0x0400006D RID: 109
			Transferred = 1102979128U,
			// Token: 0x0400006E RID: 110
			NewDomain = 2787872604U,
			// Token: 0x0400006F RID: 111
			PublicFoldersEmptyDataTextChildren = 2390512187U,
			// Token: 0x04000070 RID: 112
			FacebookDelayed = 2682114028U,
			// Token: 0x04000071 RID: 113
			Collapse = 1342839575U,
			// Token: 0x04000072 RID: 114
			GroupNamingPolicyEditorSuffixLabel = 4211282305U,
			// Token: 0x04000073 RID: 115
			MessageFontSampleText = 630233035U,
			// Token: 0x04000074 RID: 116
			HideModalDialogErrorReport = 3186571665U,
			// Token: 0x04000075 RID: 117
			JobSubmissionWaitText = 187569137U,
			// Token: 0x04000076 RID: 118
			ServiceNone = 4151098027U,
			// Token: 0x04000077 RID: 119
			Page = 2328220407U,
			// Token: 0x04000078 RID: 120
			NavigateAway = 2828598385U,
			// Token: 0x04000079 RID: 121
			RemoveDisabledLinkedInConnectionText = 1122075U,
			// Token: 0x0400007A RID: 122
			HCWStoppedDescription = 59720971U,
			// Token: 0x0400007B RID: 123
			OnDisplayText = 2658434708U,
			// Token: 0x0400007C RID: 124
			LongRunErrorLabel = 1981061025U,
			// Token: 0x0400007D RID: 125
			EIAE = 809706446U,
			// Token: 0x0400007E RID: 126
			NegotiateAuth = 1907621764U,
			// Token: 0x0400007F RID: 127
			RequestDLPDetailReportTitle = 3122995196U,
			// Token: 0x04000080 RID: 128
			WipeConfirmMessage = 2736422594U,
			// Token: 0x04000081 RID: 129
			MobileDeviceEnableText = 317346726U,
			// Token: 0x04000082 RID: 130
			EnableConnectorConfirm = 3197491520U,
			// Token: 0x04000083 RID: 131
			OffCommandText = 1875331145U,
			// Token: 0x04000084 RID: 132
			HydrationDoneTitle = 3778906594U,
			// Token: 0x04000085 RID: 133
			ConnectorAllAvailableIPv6 = 1144247250U,
			// Token: 0x04000086 RID: 134
			UMCallAnsweringRulesEditorRuleConditionLabelText = 1795974124U,
			// Token: 0x04000087 RID: 135
			OwaMailboxPolicyPlaces = 3225869645U,
			// Token: 0x04000088 RID: 136
			JournalEmailAddressLabel = 602293241U,
			// Token: 0x04000089 RID: 137
			PopupBlockedMessage = 259008929U,
			// Token: 0x0400008A RID: 138
			IUnderstandAction = 2802879859U,
			// Token: 0x0400008B RID: 139
			Select15Minutes = 2532213629U,
			// Token: 0x0400008C RID: 140
			AllowedPendingDisplayText = 1811854250U,
			// Token: 0x0400008D RID: 141
			OwaMailboxPolicyTimeManagement = 1848756223U,
			// Token: 0x0400008E RID: 142
			PasswordNote = 3077953159U,
			// Token: 0x0400008F RID: 143
			HasLinkQueryField = 573217698U,
			// Token: 0x04000090 RID: 144
			VoicemailClearSettingsTitle = 1318187683U,
			// Token: 0x04000091 RID: 145
			ConfigureOAuth = 3269293275U,
			// Token: 0x04000092 RID: 146
			And = 3068683287U,
			// Token: 0x04000093 RID: 147
			VoicemailResetPINSuccessMessage = 4210295185U,
			// Token: 0x04000094 RID: 148
			FileDownloadFailed = 3121279917U,
			// Token: 0x04000095 RID: 149
			ConfirmRemoveLinkedIn = 3545744552U,
			// Token: 0x04000096 RID: 150
			RemoveFacebookSupportingText = 1354285736U,
			// Token: 0x04000097 RID: 151
			ListViewMoreResultsWarning = 4266824568U,
			// Token: 0x04000098 RID: 152
			DisableReplicationCommandText = 795464922U,
			// Token: 0x04000099 RID: 153
			EnterpriseMainHeader = 519362517U,
			// Token: 0x0400009A RID: 154
			AddGroupNamingPolicyElementButtonText = 313078617U,
			// Token: 0x0400009B RID: 155
			UseAlias = 571113625U,
			// Token: 0x0400009C RID: 156
			FileUploadFailed = 1300295012U,
			// Token: 0x0400009D RID: 157
			CustomDateLink = 1277846131U,
			// Token: 0x0400009E RID: 158
			PolicyGroupMembership = 4122267321U,
			// Token: 0x0400009F RID: 159
			NextPage = 1548165396U,
			// Token: 0x040000A0 RID: 160
			HydrationDone = 437474464U,
			// Token: 0x040000A1 RID: 161
			ProviderDisabled = 4033288601U,
			// Token: 0x040000A2 RID: 162
			IndividualSettings = 306768456U,
			// Token: 0x040000A3 RID: 163
			CalendarSharingFreeBusyDetail = 616309426U,
			// Token: 0x040000A4 RID: 164
			LongRunInProgressTips = 2134452995U,
			// Token: 0x040000A5 RID: 165
			OwaMailboxPolicyChangePassword = 2004565666U,
			// Token: 0x040000A6 RID: 166
			VoicemailClearSettingsDetailsContactOperator = 1595040325U,
			// Token: 0x040000A7 RID: 167
			DisableFederationStopped = 2738758716U,
			// Token: 0x040000A8 RID: 168
			Success = 2659939651U,
			// Token: 0x040000A9 RID: 169
			NoOnboardingPermission = 2197391249U,
			// Token: 0x040000AA RID: 170
			HydratingTitle = 183956792U,
			// Token: 0x040000AB RID: 171
			TextMessagingNotificationSetupLinkText = 2622420678U,
			// Token: 0x040000AC RID: 172
			WipePendingPendingDisplayText = 533757016U,
			// Token: 0x040000AD RID: 173
			InvalidMultiEmailAddress = 1565584202U,
			// Token: 0x040000AE RID: 174
			DataCenterMainHeader = 4033771799U,
			// Token: 0x040000AF RID: 175
			BulkEditNotificationTenMinuteLabel = 893456894U,
			// Token: 0x040000B0 RID: 176
			DefaultRuleExceptionLabel = 124543120U,
			// Token: 0x040000B1 RID: 177
			SelectTheTextAndCopy = 2402178338U,
			// Token: 0x040000B2 RID: 178
			FailedToRetrieveMailboxOnboarding = 2165251921U,
			// Token: 0x040000B3 RID: 179
			DisabledDisplayText = 3546966747U,
			// Token: 0x040000B4 RID: 180
			ConditionValueSeparator = 1361553573U,
			// Token: 0x040000B5 RID: 181
			LinkedInDelayed = 2388288698U,
			// Token: 0x040000B6 RID: 182
			ErrorTitle = 933672694U,
			// Token: 0x040000B7 RID: 183
			InvalidSmtpAddress = 3042237373U,
			// Token: 0x040000B8 RID: 184
			RemoveLinkedInSupportingText = 128074410U,
			// Token: 0x040000B9 RID: 185
			ResumeDBCopyConfirmation = 2368306281U,
			// Token: 0x040000BA RID: 186
			MessageTypeAll = 1168324224U,
			// Token: 0x040000BB RID: 187
			CmdLogTitleForHybridO365 = 3966162852U,
			// Token: 0x040000BC RID: 188
			ConfirmRemoveFacebook = 207358046U,
			// Token: 0x040000BD RID: 189
			BulkEditNotificationMinuteLabel = 3329734225U,
			// Token: 0x040000BE RID: 190
			VoiceMailText = 2523533992U,
			// Token: 0x040000BF RID: 191
			CollapseAll = 2616506832U,
			// Token: 0x040000C0 RID: 192
			DefaultContactsFolderText = 1728961555U,
			// Token: 0x040000C1 RID: 193
			OwaMailboxPolicyWeather = 979822077U,
			// Token: 0x040000C2 RID: 194
			LegacyFolderError = 1129028723U,
			// Token: 0x040000C3 RID: 195
			MessageTraceReportTitle = 2904016598U,
			// Token: 0x040000C4 RID: 196
			JournalEmailAddressInvalid = 2966716344U,
			// Token: 0x040000C5 RID: 197
			JobSubmitted = 2501247758U,
			// Token: 0x040000C6 RID: 198
			UMEnableE164ActionSummary = 3058620969U,
			// Token: 0x040000C7 RID: 199
			OK = 2041362128U,
			// Token: 0x040000C8 RID: 200
			LastPage = 3303348785U,
			// Token: 0x040000C9 RID: 201
			OwaMailboxPolicyRemindersAndNotifications = 2715655425U,
			// Token: 0x040000CA RID: 202
			DataLossWarning = 1359139161U,
			// Token: 0x040000CB RID: 203
			SuspendComments = 3428137968U,
			// Token: 0x040000CC RID: 204
			Delivered = 1531436154U,
			// Token: 0x040000CD RID: 205
			Retry = 479196852U,
			// Token: 0x040000CE RID: 206
			Descending = 1777112844U,
			// Token: 0x040000CF RID: 207
			SimpleFilterTextBoxWaterMark = 872019732U,
			// Token: 0x040000D0 RID: 208
			TypingDescription = 2058638459U,
			// Token: 0x040000D1 RID: 209
			NonEditingAuthor = 4012579652U,
			// Token: 0x040000D2 RID: 210
			MinimumCriteriaFieldsInError = 3976037671U,
			// Token: 0x040000D3 RID: 211
			ListSeparator = 1349255527U,
			// Token: 0x040000D4 RID: 212
			ExpandAll = 18372887U,
			// Token: 0x040000D5 RID: 213
			AutoInternal = 968612268U,
			// Token: 0x040000D6 RID: 214
			NeverUse = 3953482277U,
			// Token: 0x040000D7 RID: 215
			NoonPM = 4257989793U,
			// Token: 0x040000D8 RID: 216
			EnableReplicationCommandText = 2204193123U,
			// Token: 0x040000D9 RID: 217
			HCWCompletedDescription = 1536001239U,
			// Token: 0x040000DA RID: 218
			PWTNS = 3618788766U,
			// Token: 0x040000DB RID: 219
			DeviceModelPickerAll = 3667211102U,
			// Token: 0x040000DC RID: 220
			NextMonth = 1040160067U,
			// Token: 0x040000DD RID: 221
			UploaderUnhandledExceptionMessage = 3066268041U,
			// Token: 0x040000DE RID: 222
			MobileExternal = 1677432033U,
			// Token: 0x040000DF RID: 223
			SearchButtonTooltip = 184677197U,
			// Token: 0x040000E0 RID: 224
			SavingCompletedInformation = 1715033809U,
			// Token: 0x040000E1 RID: 225
			SetupExchangeHybrid = 2274379572U,
			// Token: 0x040000E2 RID: 226
			EnableFVA = 1355682252U,
			// Token: 0x040000E3 RID: 227
			PWTNAB = 3247026326U,
			// Token: 0x040000E4 RID: 228
			ForceConnectMailbox = 1759109339U,
			// Token: 0x040000E5 RID: 229
			ShowModalDialogErrorReport = 4291377740U,
			// Token: 0x040000E6 RID: 230
			Imap = 2583693733U,
			// Token: 0x040000E7 RID: 231
			ConnectToFacebookMessage = 3122803756U,
			// Token: 0x040000E8 RID: 232
			DateRangeError = 1638106043U,
			// Token: 0x040000E9 RID: 233
			OwaMailboxPolicyUserExperience = 1138816392U,
			// Token: 0x040000EA RID: 234
			WebServiceRequestInetError = 3831099320U,
			// Token: 0x040000EB RID: 235
			FindMeText = 839901080U,
			// Token: 0x040000EC RID: 236
			CtrlKeySelectAllInListView = 3186529231U,
			// Token: 0x040000ED RID: 237
			RemoveAction = 3086537020U,
			// Token: 0x040000EE RID: 238
			UMHolidayScheduleHolidayEndDateRequiredError = 2835992379U,
			// Token: 0x040000EF RID: 239
			NetworkCredentialUserNameErrorMessage = 3179034952U,
			// Token: 0x040000F0 RID: 240
			SetupHybridUIFirst = 1579815837U,
			// Token: 0x040000F1 RID: 241
			UMKeyMappingActionSummaryAnnounceBusinessLocation = 1205297021U,
			// Token: 0x040000F2 RID: 242
			GroupNamingPolicyCaption = 1139168619U,
			// Token: 0x040000F3 RID: 243
			TransferToGalContactText = 3022174399U,
			// Token: 0x040000F4 RID: 244
			UnhandledExceptionMessage = 4125561529U,
			// Token: 0x040000F5 RID: 245
			OwaMailboxPolicyJunkEmail = 2614439623U,
			// Token: 0x040000F6 RID: 246
			DynamicDistributionGroupText = 4070877873U,
			// Token: 0x040000F7 RID: 247
			ServerNameColumnText = 2522496037U,
			// Token: 0x040000F8 RID: 248
			TextMessagingNotificationNotSetupLinkText = 1445885649U,
			// Token: 0x040000F9 RID: 249
			QuerySyntaxError = 3450836391U,
			// Token: 0x040000FA RID: 250
			SharingDomainOptionAll = 1278568394U,
			// Token: 0x040000FB RID: 251
			VoicemailWizardEnterPinText = 1907037086U,
			// Token: 0x040000FC RID: 252
			AddressExists = 4068892486U,
			// Token: 0x040000FD RID: 253
			ModifyExchangeHybrid = 1987425903U,
			// Token: 0x040000FE RID: 254
			HydrationAndFeatureDone = 2944220729U,
			// Token: 0x040000FF RID: 255
			ProviderConnected = 997295902U,
			// Token: 0x04000100 RID: 256
			NoNamingPolicySetup = 3174388608U,
			// Token: 0x04000101 RID: 257
			DoNotShowDialog = 3000098309U,
			// Token: 0x04000102 RID: 258
			RemoveMailboxDeleteLiveID = 238804546U,
			// Token: 0x04000103 RID: 259
			GreetingsAndPromptsTitleText = 1527687315U,
			// Token: 0x04000104 RID: 260
			ConfigureVoicemailButtonText = 3621606012U,
			// Token: 0x04000105 RID: 261
			FirstNameLastName = 613554384U,
			// Token: 0x04000106 RID: 262
			Yes = 3021629903U,
			// Token: 0x04000107 RID: 263
			Author = 3560108081U,
			// Token: 0x04000108 RID: 264
			PWTRAS = 148099519U,
			// Token: 0x04000109 RID: 265
			TransferToGalContactVoicemailText = 2482463458U,
			// Token: 0x0400010A RID: 266
			MailboxDelegationDetail = 2706637479U,
			// Token: 0x0400010B RID: 267
			Or = 381169853U,
			// Token: 0x0400010C RID: 268
			Reset = 1594942057U,
			// Token: 0x0400010D RID: 269
			UpdateTimeZonePrompt = 1904041070U,
			// Token: 0x0400010E RID: 270
			DontSave = 3369491564U,
			// Token: 0x0400010F RID: 271
			VoicemailSummaryAccessNumber = 2908249814U,
			// Token: 0x04000110 RID: 272
			Save = 2328220357U,
			// Token: 0x04000111 RID: 273
			OwaMailboxPolicyThemeSelection = 2864395898U,
			// Token: 0x04000112 RID: 274
			ReadThis = 1393796710U,
			// Token: 0x04000113 RID: 275
			SubnetIPEditorTitle = 2379979535U,
			// Token: 0x04000114 RID: 276
			UMEnableExtensionAuto = 3782146179U,
			// Token: 0x04000115 RID: 277
			WarningPanelMultipleWarnings = 1062541333U,
			// Token: 0x04000116 RID: 278
			UMHolidayScheduleHolidayNameRequiredError = 502251987U,
			// Token: 0x04000117 RID: 279
			Select24Hours = 3674505245U,
			// Token: 0x04000118 RID: 280
			MemberUpdateTypeApprovalRequired = 2794185135U,
			// Token: 0x04000119 RID: 281
			DefaultRuleActionLabel = 639660141U,
			// Token: 0x0400011A RID: 282
			EmptyValueError = 4195962670U,
			// Token: 0x0400011B RID: 283
			ApplyToAllCalls = 2794530675U,
			// Token: 0x0400011C RID: 284
			OutOfMemoryErrorMessage = 3908900425U,
			// Token: 0x0400011D RID: 285
			Never = 1594930120U,
			// Token: 0x0400011E RID: 286
			OABExternal = 2694533749U,
			// Token: 0x0400011F RID: 287
			ReconnectToFacebookMessage = 320649385U,
			// Token: 0x04000120 RID: 288
			MemberApprovalHasChanged = 1889215887U,
			// Token: 0x04000121 RID: 289
			CreatingFolder = 2257237083U,
			// Token: 0x04000122 RID: 290
			UMEnableMailboxAutoSipDescription = 2743921036U,
			// Token: 0x04000123 RID: 291
			FirstNameLastNameInitial = 367868548U,
			// Token: 0x04000124 RID: 292
			PleaseWait = 1690161621U,
			// Token: 0x04000125 RID: 293
			WhatDoesThisMean = 2091505548U,
			// Token: 0x04000126 RID: 294
			ModalDialogErrorReport = 3036539697U,
			// Token: 0x04000127 RID: 295
			SecondaryNavigation = 354624350U,
			// Token: 0x04000128 RID: 296
			ConnectToLinkedInMessage = 305410994U,
			// Token: 0x04000129 RID: 297
			MessageTraceMessageIDCannotContainComma = 503026860U,
			// Token: 0x0400012A RID: 298
			ApplyToAllMessages = 918737566U,
			// Token: 0x0400012B RID: 299
			ActiveSyncDisableText = 689982738U,
			// Token: 0x0400012C RID: 300
			BasicAuth = 637136194U,
			// Token: 0x0400012D RID: 301
			VoicemailResetPINTitle = 2870521773U,
			// Token: 0x0400012E RID: 302
			LongRunCompletedTips = 2777189520U,
			// Token: 0x0400012F RID: 303
			GroupNamingPolicyPreviewLabel = 3817888713U,
			// Token: 0x04000130 RID: 304
			AASL = 2053059583U,
			// Token: 0x04000131 RID: 305
			MemberUpdateTypeOpen = 924670953U,
			// Token: 0x04000132 RID: 306
			PublishingEditor = 2013213994U,
			// Token: 0x04000133 RID: 307
			LossDataWarning = 1356895513U,
			// Token: 0x04000134 RID: 308
			OwaMailboxPolicyInstantMessaging = 2014560916U,
			// Token: 0x04000135 RID: 309
			TextTooLongErrorMessage = 2093942654U,
			// Token: 0x04000136 RID: 310
			EnableFederationStopped = 1833904645U,
			// Token: 0x04000137 RID: 311
			GreetingsAndPromptsInstructionsText = 912552836U,
			// Token: 0x04000138 RID: 312
			UMHolidayScheduleHolidayPromptRequiredError = 771355430U,
			// Token: 0x04000139 RID: 313
			AddDagMember = 1515892343U,
			// Token: 0x0400013A RID: 314
			DistributionGroupText = 259319032U,
			// Token: 0x0400013B RID: 315
			KeyMappingDisplayTextFormat = 1244644071U,
			// Token: 0x0400013C RID: 316
			RequestRuleDetailReportTitle = 3924205880U,
			// Token: 0x0400013D RID: 317
			RequestMalwareDetailReportTitle = 1865766159U,
			// Token: 0x0400013E RID: 318
			ConfirmRemoveConnectionTitle = 173806984U,
			// Token: 0x0400013F RID: 319
			WebServicesInternal = 2320161129U,
			// Token: 0x04000140 RID: 320
			Wait = 49675595U,
			// Token: 0x04000141 RID: 321
			DisableOWAConfirm = 4196350991U,
			// Token: 0x04000142 RID: 322
			UriKindRelativeOrAbsolute = 2663567466U,
			// Token: 0x04000143 RID: 323
			SessionTimeout = 2122906481U,
			// Token: 0x04000144 RID: 324
			Change = 1742879518U,
			// Token: 0x04000145 RID: 325
			LastNameFirstName = 339824766U,
			// Token: 0x04000146 RID: 326
			AddIPAddress = 204633756U,
			// Token: 0x04000147 RID: 327
			InvalidUnlimitedQuotaRegex = 1091377885U,
			// Token: 0x04000148 RID: 328
			NoSenderAddressWarning = 1623670356U,
			// Token: 0x04000149 RID: 329
			TextMessagingNotificationSetupText = 3333839866U,
			// Token: 0x0400014A RID: 330
			LastNameFirstNameInitial = 2334202318U,
			// Token: 0x0400014B RID: 331
			UnhandledExceptionTitle = 2124576504U,
			// Token: 0x0400014C RID: 332
			NoConditionErrorText = 3689797535U,
			// Token: 0x0400014D RID: 333
			FirstPage = 3348900521U,
			// Token: 0x0400014E RID: 334
			CannotUploadMultipleFiles = 1673297715U,
			// Token: 0x0400014F RID: 335
			ClearButtonTooltip = 3328778458U,
			// Token: 0x04000150 RID: 336
			AutoExternal = 2941133106U,
			// Token: 0x04000151 RID: 337
			DayAndTimeRangeTooltip = 768075136U,
			// Token: 0x04000152 RID: 338
			ViewNotificationDetails = 3147146074U,
			// Token: 0x04000153 RID: 339
			EASL = 2053059451U,
			// Token: 0x04000154 RID: 340
			Outlook = 965845689U,
			// Token: 0x04000155 RID: 341
			ConnectProviderCommandText = 3904375665U,
			// Token: 0x04000156 RID: 342
			SpecificPhoneNumberText = 2391603832U,
			// Token: 0x04000157 RID: 343
			Pending = 334057287U,
			// Token: 0x04000158 RID: 344
			CalendarSharingFreeBusyReviewer = 3216786356U,
			// Token: 0x04000159 RID: 345
			LitigationHoldDateNotSet = 3097242366U,
			// Token: 0x0400015A RID: 346
			OwaMailboxPolicyAllAddressLists = 2796848775U,
			// Token: 0x0400015B RID: 347
			Error = 22442200U,
			// Token: 0x0400015C RID: 348
			Externaldnslookups = 1466672057U,
			// Token: 0x0400015D RID: 349
			EditVoicemailButtonText = 3849511688U,
			// Token: 0x0400015E RID: 350
			FailedToRetrieveMailboxLocalMove = 903130308U,
			// Token: 0x0400015F RID: 351
			ConnectorAllAvailableIPv4 = 1144247248U,
			// Token: 0x04000160 RID: 352
			HydrationAndFeatureDoneTitle = 2262897061U,
			// Token: 0x04000161 RID: 353
			ConstraintViolationNoLeadingOrTrailingWhitespace = 2058499689U,
			// Token: 0x04000162 RID: 354
			CalendarSharingFreeBusySimple = 2862582797U,
			// Token: 0x04000163 RID: 355
			PageSize = 4277755996U,
			// Token: 0x04000164 RID: 356
			ConstraintFieldsNotMatchError = 2742068696U,
			// Token: 0x04000165 RID: 357
			Updating = 3900615856U,
			// Token: 0x04000166 RID: 358
			AdditionalPropertiesLabel = 353630484U,
			// Token: 0x04000167 RID: 359
			JumpToMigrationSlabConfirmation = 1011589268U,
			// Token: 0x04000168 RID: 360
			VoicemailCallFwdContactOperator = 7739616U,
			// Token: 0x04000169 RID: 361
			InvalidValueRange = 2680934369U,
			// Token: 0x0400016A RID: 362
			NoActionRuleAuditSeverity = 2697991219U,
			// Token: 0x0400016B RID: 363
			VoicemailClearSettingsConfirmationMessage = 3273613659U,
			// Token: 0x0400016C RID: 364
			WebServicesExternal = 1655108951U,
			// Token: 0x0400016D RID: 365
			UploaderValidationError = 2101584371U,
			// Token: 0x0400016E RID: 366
			ServerAboutToExpireWarningText = 3336188783U,
			// Token: 0x0400016F RID: 367
			ConditionValueRequriedErrorMessage = 3200788962U,
			// Token: 0x04000170 RID: 368
			PreviousePage = 439636863U,
			// Token: 0x04000171 RID: 369
			Ascending = 3390434404U,
			// Token: 0x04000172 RID: 370
			EditSubnetCaption = 2068973737U,
			// Token: 0x04000173 RID: 371
			ChooseAtLeastOneColumn = 267746805U,
			// Token: 0x04000174 RID: 372
			EditSharingEnabledDomains = 2900910704U,
			// Token: 0x04000175 RID: 373
			Recipients = 986397318U,
			// Token: 0x04000176 RID: 374
			Back = 2778558511U,
			// Token: 0x04000177 RID: 375
			VoicemailPostFwdRecordGreeting = 3068528108U,
			// Token: 0x04000178 RID: 376
			ColumnChooseFailed = 1857390484U,
			// Token: 0x04000179 RID: 377
			TransportRuleBusinessContinuity = 1219538243U,
			// Token: 0x0400017A RID: 378
			TitleSectionMobileDevices = 936826416U,
			// Token: 0x0400017B RID: 379
			NewFolder = 2894502448U,
			// Token: 0x0400017C RID: 380
			EnableCommandText = 3265439859U,
			// Token: 0x0400017D RID: 381
			PrimaryAddressLabel = 583273118U,
			// Token: 0x0400017E RID: 382
			IncidentReportContentCustom = 3843987112U,
			// Token: 0x0400017F RID: 383
			MessageTraceInvalidStartDate = 3566331407U,
			// Token: 0x04000180 RID: 384
			InvalidDecimal1 = 3393270247U,
			// Token: 0x04000181 RID: 385
			ActivateDBCopyConfirmation = 2636721601U,
			// Token: 0x04000182 RID: 386
			DisableActiveSyncConfirm = 1063565289U,
			// Token: 0x04000183 RID: 387
			PleaseWaitWhileSaving = 652753372U,
			// Token: 0x04000184 RID: 388
			AddConditionButtonText = 4082347473U,
			// Token: 0x04000185 RID: 389
			AcceptedDomainAuthoritativeWarning = 3022976242U,
			// Token: 0x04000186 RID: 390
			Editor = 3653626825U,
			// Token: 0x04000187 RID: 391
			PlayOnPhoneConnected = 1801819654U,
			// Token: 0x04000188 RID: 392
			InvalidDomainName = 2762661174U,
			// Token: 0x04000189 RID: 393
			SetECPAuthConfirmText = 2068086983U,
			// Token: 0x0400018A RID: 394
			RuleNameTextBoxLabel = 4262899755U,
			// Token: 0x0400018B RID: 395
			More = 1414245989U,
			// Token: 0x0400018C RID: 396
			FirstNameInitialLastName = 2549262094U,
			// Token: 0x0400018D RID: 397
			OwaMailboxPolicyCommunicationManagement = 2810364182U,
			// Token: 0x0400018E RID: 398
			Owner = 2731136937U,
			// Token: 0x0400018F RID: 399
			EditDomain = 1400729458U,
			// Token: 0x04000190 RID: 400
			InvalidEmailAddressName = 3134721922U,
			// Token: 0x04000191 RID: 401
			LeadingTrailingSpaceError = 467409560U,
			// Token: 0x04000192 RID: 402
			DayRangeAndTimeRangeTooltip = 2307360811U,
			// Token: 0x04000193 RID: 403
			OwaMailboxPolicyPremiumClient = 3180237931U,
			// Token: 0x04000194 RID: 404
			IncidentReportDeselectAll = 1849292272U,
			// Token: 0x04000195 RID: 405
			OwaMailboxPolicyNotes = 1911032188U,
			// Token: 0x04000196 RID: 406
			UMEnableSipActionSummary = 3151174489U,
			// Token: 0x04000197 RID: 407
			Reviewer = 1045743613U,
			// Token: 0x04000198 RID: 408
			LongRunStoppedTips = 1761836208U,
			// Token: 0x04000199 RID: 409
			RequestSpamDetailReportTitle = 1259584465U,
			// Token: 0x0400019A RID: 410
			AddExceptionButtonText = 288995097U,
			// Token: 0x0400019B RID: 411
			OwaMailboxPolicyRecoverDeletedItems = 1283935370U,
			// Token: 0x0400019C RID: 412
			AddActionButtonText = 117844352U,
			// Token: 0x0400019D RID: 413
			EndDateMustBeYesterday = 2009538927U,
			// Token: 0x0400019E RID: 414
			OwaInternal = 3100497742U,
			// Token: 0x0400019F RID: 415
			CheckNames = 2920201570U,
			// Token: 0x040001A0 RID: 416
			UMHolidayScheduleHolidayStartEndDateValidationError = 2752727145U,
			// Token: 0x040001A1 RID: 417
			InvalidMailboxSearchName = 3104459904U,
			// Token: 0x040001A2 RID: 418
			RemoveMailboxKeepLiveID = 2559957656U,
			// Token: 0x040001A3 RID: 419
			OwaMailboxPolicySignatures = 3766407320U,
			// Token: 0x040001A4 RID: 420
			WorkingHoursText = 3982517327U,
			// Token: 0x040001A5 RID: 421
			ESHL = 3189265994U,
			// Token: 0x040001A6 RID: 422
			FileStillUploading = 1911656207U,
			// Token: 0x040001A7 RID: 423
			StartOverMailboxSearch = 472573372U,
			// Token: 0x040001A8 RID: 424
			DisableConnectorLoggingConfirm = 9358886U,
			// Token: 0x040001A9 RID: 425
			LongRunDataLossWarning = 228304082U,
			// Token: 0x040001AA RID: 426
			ActiveText = 798401137U,
			// Token: 0x040001AB RID: 427
			DisableMOWAConfirm = 461710906U,
			// Token: 0x040001AC RID: 428
			WarningPanelSeeMoreMsg = 1400887235U,
			// Token: 0x040001AD RID: 429
			EnterpriseLogoutFail = 2860566285U,
			// Token: 0x040001AE RID: 430
			AlwaysUse = 3375594338U,
			// Token: 0x040001AF RID: 431
			PublishingAuthor = 1271124552U,
			// Token: 0x040001B0 RID: 432
			CopyError = 3934389177U,
			// Token: 0x040001B1 RID: 433
			LastNameInitialFirstName = 3980910748U,
			// Token: 0x040001B2 RID: 434
			Internaldnslookups = 2757672907U,
			// Token: 0x040001B3 RID: 435
			ApplyAllMessagesWarning = 2794407267U,
			// Token: 0x040001B4 RID: 436
			EditIPAddress = 3799642099U,
			// Token: 0x040001B5 RID: 437
			SecurityGroupText = 265625760U,
			// Token: 0x040001B6 RID: 438
			ResumeMailboxSearch = 356602967U,
			// Token: 0x040001B7 RID: 439
			JumpToOffice365MigrationSlabFailed = 3688802170U,
			// Token: 0x040001B8 RID: 440
			DeliveryReportEmailBodyForMailTo = 3738684211U,
			// Token: 0x040001B9 RID: 441
			DefaultRuleConditionLabel = 2929991304U,
			// Token: 0x040001BA RID: 442
			RequestDLPDetail = 1506239268U,
			// Token: 0x040001BB RID: 443
			AuditSeverityLevelLabel = 667665198U,
			// Token: 0x040001BC RID: 444
			NoneForEmpty = 3800601796U,
			// Token: 0x040001BD RID: 445
			OwaMailboxPolicyRules = 210154378U,
			// Token: 0x040001BE RID: 446
			CtrlKeyToSave = 4035228826U,
			// Token: 0x040001BF RID: 447
			CustomPeriodText = 3041967243U,
			// Token: 0x040001C0 RID: 448
			NSHL = 3189266341U,
			// Token: 0x040001C1 RID: 449
			TransferToNumberText = 663637318U,
			// Token: 0x040001C2 RID: 450
			UMKeyMappingActionSummaryAnnounceBusinessHours = 2950784627U,
			// Token: 0x040001C3 RID: 451
			OwaMailboxPolicyFacebook = 912527261U,
			// Token: 0x040001C4 RID: 452
			ConnectMailboxLaunchWizard = 1929634872U,
			// Token: 0x040001C5 RID: 453
			SomeSelectionNotAdded = 280551769U,
			// Token: 0x040001C6 RID: 454
			LoadingInformation = 32190104U,
			// Token: 0x040001C7 RID: 455
			ProceedWithoutTenantInfo = 1710116876U,
			// Token: 0x040001C8 RID: 456
			Loading = 3599592070U,
			// Token: 0x040001C9 RID: 457
			DeviceTypePickerAll = 1891837447U,
			// Token: 0x040001CA RID: 458
			VoicemailConfigurationTitle = 892936075U,
			// Token: 0x040001CB RID: 459
			RemoveDBCopyConfirmation = 2336878616U,
			// Token: 0x040001CC RID: 460
			MemberUpdateTypeClosed = 1308147703U,
			// Token: 0x040001CD RID: 461
			OnCommandText = 2359945479U,
			// Token: 0x040001CE RID: 462
			SelectOne = 779120846U,
			// Token: 0x040001CF RID: 463
			NtlmAuth = 49706295U,
			// Token: 0x040001D0 RID: 464
			EnableMOWAConfirm = 1716565179U,
			// Token: 0x040001D1 RID: 465
			Pop = 1502600087U,
			// Token: 0x040001D2 RID: 466
			DisabledPendingDisplayText = 882294734U,
			// Token: 0x040001D3 RID: 467
			FirstFocusTextForScreenReader = 2145940917U,
			// Token: 0x040001D4 RID: 468
			MoveUp = 137938150U,
			// Token: 0x040001D5 RID: 469
			GalOrPersonalContactText = 347463522U,
			// Token: 0x040001D6 RID: 470
			AllAvailableIPV4Address = 2903019109U,
			// Token: 0x040001D7 RID: 471
			OwaExternal = 3765422820U,
			// Token: 0x040001D8 RID: 472
			ActiveSyncEnableText = 1015147527U,
			// Token: 0x040001D9 RID: 473
			ConstraintViolationInputUnlimitedValue = 2594574748U,
			// Token: 0x040001DA RID: 474
			ResetPinMessageTitle = 1603757297U,
			// Token: 0x040001DB RID: 475
			No = 1496915101U,
			// Token: 0x040001DC RID: 476
			LongRunStoppedDescription = 2846873532U,
			// Token: 0x040001DD RID: 477
			NIAE = 809706807U,
			// Token: 0x040001DE RID: 478
			CtrlKeyCloseForm = 116135148U,
			// Token: 0x040001DF RID: 479
			InvalidUrl = 3705047298U,
			// Token: 0x040001E0 RID: 480
			InvalidDomain = 3403459873U,
			// Token: 0x040001E1 RID: 481
			Display = 2852593944U,
			// Token: 0x040001E2 RID: 482
			SslTitle = 2814896254U,
			// Token: 0x040001E3 RID: 483
			ProviderDelayed = 4028691857U,
			// Token: 0x040001E4 RID: 484
			TrialExpiredWarningText = 3512966386U,
			// Token: 0x040001E5 RID: 485
			StopProcessingRuleLabel = 1496927839U,
			// Token: 0x040001E6 RID: 486
			UMEnabledPolicyRequired = 1872640986U,
			// Token: 0x040001E7 RID: 487
			All = 4231482709U,
			// Token: 0x040001E8 RID: 488
			ClickHereForHelp = 238716676U,
			// Token: 0x040001E9 RID: 489
			LongRunCompletedDescription = 945688236U,
			// Token: 0x040001EA RID: 490
			ConnectToO365 = 4193953058U,
			// Token: 0x040001EB RID: 491
			NoRetentionPolicy = 2021920769U,
			// Token: 0x040001EC RID: 492
			SavingInformation = 995902246U,
			// Token: 0x040001ED RID: 493
			InvalidAlias = 2939777527U,
			// Token: 0x040001EE RID: 494
			Contributor = 1226897823U,
			// Token: 0x040001EF RID: 495
			Cancel = 2358390244U,
			// Token: 0x040001F0 RID: 496
			NoIPWarning = 3309738928U,
			// Token: 0x040001F1 RID: 497
			Custom = 427688167U,
			// Token: 0x040001F2 RID: 498
			RemovePendingDisplayText = 664253366U,
			// Token: 0x040001F3 RID: 499
			EnabledPendingDisplayText = 892169149U,
			// Token: 0x040001F4 RID: 500
			RecordGreetingLinkText = 3027332405U,
			// Token: 0x040001F5 RID: 501
			Next = 3423762231U,
			// Token: 0x040001F6 RID: 502
			PrimaryNavigation = 4164526598U,
			// Token: 0x040001F7 RID: 503
			KeyMappingVoiceMailDisplayText = 2835676179U,
			// Token: 0x040001F8 RID: 504
			WarningPanelMultipleWarningsMsg = 2381414750U,
			// Token: 0x040001F9 RID: 505
			RequestMalwareDetail = 67348609U,
			// Token: 0x040001FA RID: 506
			Information = 2193536568U,
			// Token: 0x040001FB RID: 507
			RequestRuleDetail = 1749370292U,
			// Token: 0x040001FC RID: 508
			OutsideWorkingHoursText = 2470451672U,
			// Token: 0x040001FD RID: 509
			UMDialPlanRequiredField = 1325539240U,
			// Token: 0x040001FE RID: 510
			DisableFederationCompleted = 3205750596U,
			// Token: 0x040001FF RID: 511
			SharingRuleEntryDomainConflict = 3276462966U,
			// Token: 0x04000200 RID: 512
			EditSharingEnabledDomainsStep2 = 4005194968U,
			// Token: 0x04000201 RID: 513
			NoSpaceValidatorMessage = 2220221264U,
			// Token: 0x04000202 RID: 514
			WaitForHybridUIReady = 3125926615U,
			// Token: 0x04000203 RID: 515
			MailboxDelegation = 684930172U,
			// Token: 0x04000204 RID: 516
			OwaMailboxPolicySecurity = 1396631711U,
			// Token: 0x04000205 RID: 517
			SenderRequiredErrorMessage = 3453349145U,
			// Token: 0x04000206 RID: 518
			ServerTrailDaysColumnText = 1294686955U,
			// Token: 0x04000207 RID: 519
			ResetPinMessage = 1173301205U,
			// Token: 0x04000208 RID: 520
			RemoveDisabledFacebookConnectionText = 1482742945U,
			// Token: 0x04000209 RID: 521
			ReportTitleMissing = 214141546U,
			// Token: 0x0400020A RID: 522
			OwaMailboxPolicyLinkedIn = 2593970847U,
			// Token: 0x0400020B RID: 523
			ConstraintViolationValueOutOfRange = 1904327111U,
			// Token: 0x0400020C RID: 524
			MTRTDetailsTitle = 3379654077U,
			// Token: 0x0400020D RID: 525
			Column = 2658661820U,
			// Token: 0x0400020E RID: 526
			ClickToShowText = 4021298411U,
			// Token: 0x0400020F RID: 527
			HybridConfiguration = 2116880466U,
			// Token: 0x04000210 RID: 528
			PWTAS = 2408935185U,
			// Token: 0x04000211 RID: 529
			ClickToCopyToClipboard = 3243184343U,
			// Token: 0x04000212 RID: 530
			UriKindAbsolute = 3296253953U,
			// Token: 0x04000213 RID: 531
			VoicemailResetPINConfirmationMessage = 1147585405U,
			// Token: 0x04000214 RID: 532
			ConnectMailboxWizardCaption = 1513997499U,
			// Token: 0x04000215 RID: 533
			MobileInternal = 2506867635U,
			// Token: 0x04000216 RID: 534
			RetrievingStatistics = 2184256814U,
			// Token: 0x04000217 RID: 535
			WebServiceRequestTimeout = 3983756775U,
			// Token: 0x04000218 RID: 536
			UMEnableTelexActionSummary = 3868849319U,
			// Token: 0x04000219 RID: 537
			PermissionLevelWarning = 3698078783U,
			// Token: 0x0400021A RID: 538
			GetAllResults = 3774473073U,
			// Token: 0x0400021B RID: 539
			LongRunCopyToClipboardLabel = 259417423U,
			// Token: 0x0400021C RID: 540
			EnableFederationCompleted = 721624957U,
			// Token: 0x0400021D RID: 541
			OABInternal = 1957457715U,
			// Token: 0x0400021E RID: 542
			OwaMailboxPolicyCalendar = 2767693563U
		}

		// Token: 0x02000004 RID: 4
		private enum ParamIDs
		{
			// Token: 0x04000220 RID: 544
			ModalDialgMultipleSRMsgTemplate,
			// Token: 0x04000221 RID: 545
			ErrorMessageInvalidInteger,
			// Token: 0x04000222 RID: 546
			ModalDialogSRHelpTemplate,
			// Token: 0x04000223 RID: 547
			RemoveCertificateConfirm,
			// Token: 0x04000224 RID: 548
			QueryValueStartsWithStar,
			// Token: 0x04000225 RID: 549
			InvalidInteger,
			// Token: 0x04000226 RID: 550
			InvalidCharacter,
			// Token: 0x04000227 RID: 551
			RenewCertificateHelp,
			// Token: 0x04000228 RID: 552
			CancelForAjaxUploader,
			// Token: 0x04000229 RID: 553
			UMKeyMappingActionSummaryTransferToAA,
			// Token: 0x0400022A RID: 554
			UMKeyMappingActionSummaryTransferToExtension,
			// Token: 0x0400022B RID: 555
			AutoProvisionFailedMsg,
			// Token: 0x0400022C RID: 556
			NameFromFirstInitialsLastName,
			// Token: 0x0400022D RID: 557
			WipeConfirmTitle,
			// Token: 0x0400022E RID: 558
			BulkEditProgressNoSuccessFailedCount,
			// Token: 0x0400022F RID: 559
			InvalidNumberValue,
			// Token: 0x04000230 RID: 560
			ClearForAjaxUploader,
			// Token: 0x04000231 RID: 561
			DeliveryReportEmailBody,
			// Token: 0x04000232 RID: 562
			WrongExtension,
			// Token: 0x04000233 RID: 563
			ConstraintViolationLocalLongFullPath,
			// Token: 0x04000234 RID: 564
			ConstraintViolationUNCFilePath,
			// Token: 0x04000235 RID: 565
			EmptyQueryValue,
			// Token: 0x04000236 RID: 566
			ApplyToAllSelected,
			// Token: 0x04000237 RID: 567
			ValidationErrorFormat,
			// Token: 0x04000238 RID: 568
			SyncedMailboxText,
			// Token: 0x04000239 RID: 569
			NameFromFirstLastName,
			// Token: 0x0400023A RID: 570
			InvalidEmailAddress,
			// Token: 0x0400023B RID: 571
			StateOrProvinceConditionText,
			// Token: 0x0400023C RID: 572
			BulkEditedProgressSomeOperationsFailed,
			// Token: 0x0400023D RID: 573
			DepartmentConditionText,
			// Token: 0x0400023E RID: 574
			RecipientContainerText,
			// Token: 0x0400023F RID: 575
			InvalidSmtpDomainWithSubdomains,
			// Token: 0x04000240 RID: 576
			InvalidNumber,
			// Token: 0x04000241 RID: 577
			ConstraintNoTrailingSpecificCharacter,
			// Token: 0x04000242 RID: 578
			ConstraintViolationStringOnlyCanContainSpecificCharacters,
			// Token: 0x04000243 RID: 579
			UMExtensionWithDigitLabel,
			// Token: 0x04000244 RID: 580
			InvalidSmtpDomainWithSubdomainsOrIP6,
			// Token: 0x04000245 RID: 581
			ConstraintViolationStringDoesNotMatchRegularExpression,
			// Token: 0x04000246 RID: 582
			UMEnablePinDigitLabel,
			// Token: 0x04000247 RID: 583
			VoicemailClearSettingsDetails,
			// Token: 0x04000248 RID: 584
			ModalDialgSingleSRMsgTemplate,
			// Token: 0x04000249 RID: 585
			CompanyConditionText,
			// Token: 0x0400024A RID: 586
			ClearForPicker,
			// Token: 0x0400024B RID: 587
			DateChooserListName,
			// Token: 0x0400024C RID: 588
			AddressLabel,
			// Token: 0x0400024D RID: 589
			SharedUMAutoAttendantPilotIdentifierListNumberError,
			// Token: 0x0400024E RID: 590
			UMEnableExtensionValidation,
			// Token: 0x0400024F RID: 591
			ConstraintViolationStringLengthTooShort,
			// Token: 0x04000250 RID: 592
			CustomAttributeConditionText,
			// Token: 0x04000251 RID: 593
			UMExtensionValidationFailure,
			// Token: 0x04000252 RID: 594
			GroupNamingPolicyPreviewDescriptionFooter,
			// Token: 0x04000253 RID: 595
			DateExceedsRange,
			// Token: 0x04000254 RID: 596
			ConstraintViolationInvalidUriKind,
			// Token: 0x04000255 RID: 597
			FinalizedMailboxText,
			// Token: 0x04000256 RID: 598
			BulkEditProgress,
			// Token: 0x04000257 RID: 599
			GuideToSubscriptionPages,
			// Token: 0x04000258 RID: 600
			ModalDialogSREachMsgTemplate,
			// Token: 0x04000259 RID: 601
			ListViewStatusText,
			// Token: 0x0400025A RID: 602
			IncorrectQueryFieldName,
			// Token: 0x0400025B RID: 603
			BulkEditedProgressNoSuccessFailedCount,
			// Token: 0x0400025C RID: 604
			ConstraintViolationStringLengthTooLong,
			// Token: 0x0400025D RID: 605
			BulkEditedProgress,
			// Token: 0x0400025E RID: 606
			UnhandledExceptionDetails,
			// Token: 0x0400025F RID: 607
			SendPasscodeSucceededFormat,
			// Token: 0x04000260 RID: 608
			DeliveryReportEmailSubject,
			// Token: 0x04000261 RID: 609
			DuplicatedQueryFieldName,
			// Token: 0x04000262 RID: 610
			UMKeyMappingActionSummaryLeaveVM,
			// Token: 0x04000263 RID: 611
			InvalidIPAddress,
			// Token: 0x04000264 RID: 612
			GuideToSubscriptionPagePopOrImapOnly,
			// Token: 0x04000265 RID: 613
			ConstraintViolationStringDoesNotContainNonWhitespaceCharacter,
			// Token: 0x04000266 RID: 614
			MultipleRecipientsSelected,
			// Token: 0x04000267 RID: 615
			ConstraintViolationStringDoesContainsNonASCIICharacter,
			// Token: 0x04000268 RID: 616
			UMEnablePinValidation,
			// Token: 0x04000269 RID: 617
			QueryValNotInValidRange,
			// Token: 0x0400026A RID: 618
			ConstraintViolationStringMustNotContainSpecificCharacters
		}
	}
}
