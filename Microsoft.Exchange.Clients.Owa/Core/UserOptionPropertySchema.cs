using System;
using Microsoft.Exchange.Diagnostics.Components.Clients;

namespace Microsoft.Exchange.Clients.Owa.Core
{
	// Token: 0x02000283 RID: 643
	internal sealed class UserOptionPropertySchema
	{
		// Token: 0x06001684 RID: 5764 RVA: 0x00083F43 File Offset: 0x00082143
		private UserOptionPropertySchema()
		{
		}

		// Token: 0x06001685 RID: 5765 RVA: 0x00083F4C File Offset: 0x0008214C
		internal static UserOptionPropertyDefinition GetPropertyDefinition(UserOptionPropertySchema.UserOptionPropertyID id)
		{
			return UserOptionPropertySchema.GetPropertyDefinition((int)id);
		}

		// Token: 0x06001686 RID: 5766 RVA: 0x00083F61 File Offset: 0x00082161
		internal static UserOptionPropertyDefinition GetPropertyDefinition(int index)
		{
			ExTraceGlobals.UserOptionsDataTracer.TraceDebug<int, string>(0L, "Get UserOptionPropertyDefinition: index = '{0}', name = '{1}'", index, UserOptionPropertySchema.propertyDefinitions[index].PropertyName);
			return UserOptionPropertySchema.propertyDefinitions[index];
		}

		// Token: 0x17000633 RID: 1587
		// (get) Token: 0x06001687 RID: 5767 RVA: 0x00083F88 File Offset: 0x00082188
		internal static int Count
		{
			get
			{
				return UserOptionPropertySchema.propertyDefinitions.Length;
			}
		}

		// Token: 0x04001171 RID: 4465
		private static readonly UserOptionPropertyDefinition[] propertyDefinitions = new UserOptionPropertyDefinition[]
		{
			new UserOptionPropertyDefinition("timezone", typeof(string), new UserOptionPropertyDefinition.Validate(UserOptionPropertyValidationUtility.ValidateTimeZoneCallback)),
			new UserOptionPropertyDefinition("timeformat", typeof(string), new UserOptionPropertyDefinition.Validate(UserOptionPropertyValidationUtility.ValidateTimeFormatCallback)),
			new UserOptionPropertyDefinition("dateformat", typeof(string), new UserOptionPropertyDefinition.Validate(UserOptionPropertyValidationUtility.ValidateDateFormatCallback)),
			new UserOptionPropertyDefinition("weekstartday", typeof(int), new UserOptionPropertyDefinition.Validate(UserOptionPropertyValidationUtility.ValidateWeekStartDayCallback)),
			new UserOptionPropertyDefinition("hourincrement", typeof(int), new UserOptionPropertyDefinition.Validate(UserOptionPropertyValidationUtility.ValidateHourIncrementCallback)),
			new UserOptionPropertyDefinition("showweeknumbers", typeof(bool), new UserOptionPropertyDefinition.Validate(UserOptionPropertyValidationUtility.ValidateShowWeekNumbersCallback)),
			new UserOptionPropertyDefinition("checknameincontactsfirst", typeof(bool), new UserOptionPropertyDefinition.Validate(UserOptionPropertyValidationUtility.ValidateCheckNameInContactsFirstCallback)),
			new UserOptionPropertyDefinition("firstweekofyear", typeof(int), new UserOptionPropertyDefinition.Validate(UserOptionPropertyValidationUtility.ValidateFirstWeekOfYearCallback)),
			new UserOptionPropertyDefinition("enablereminders", typeof(bool), new UserOptionPropertyDefinition.Validate(UserOptionPropertyValidationUtility.ValidateEnableRemindersCallback)),
			new UserOptionPropertyDefinition("enableremindersound", typeof(bool), new UserOptionPropertyDefinition.Validate(UserOptionPropertyValidationUtility.ValidateEnableReminderSoundCallback)),
			new UserOptionPropertyDefinition("newitemnotify", typeof(int), new UserOptionPropertyDefinition.Validate(UserOptionPropertyValidationUtility.ValidateNewItemNotifyCallback)),
			new UserOptionPropertyDefinition("viewrowcount", typeof(int), new UserOptionPropertyDefinition.Validate(UserOptionPropertyValidationUtility.ValidateViewRowCountCallback)),
			new UserOptionPropertyDefinition("basicviewrowcount", typeof(int), new UserOptionPropertyDefinition.Validate(UserOptionPropertyValidationUtility.ValidateBasicViewRowCountCallback)),
			new UserOptionPropertyDefinition("spellingdictionarylanguage", typeof(int), new UserOptionPropertyDefinition.Validate(UserOptionPropertyValidationUtility.ValidateSpellingDictionaryLanguageCallback)),
			new UserOptionPropertyDefinition("spellingignoreuppercase", typeof(bool), new UserOptionPropertyDefinition.Validate(UserOptionPropertyValidationUtility.ValidateSpellingIgnoreUppercaseCallback)),
			new UserOptionPropertyDefinition("spellingignoremixeddigits", typeof(bool), new UserOptionPropertyDefinition.Validate(UserOptionPropertyValidationUtility.ValidateSpellingIgnoreMixedDigitsCallback)),
			new UserOptionPropertyDefinition("spellingcheckbeforesend", typeof(bool), new UserOptionPropertyDefinition.Validate(UserOptionPropertyValidationUtility.ValidateSpellingCheckBeforeSendCallback)),
			new UserOptionPropertyDefinition("smimeencrypt", typeof(bool), new UserOptionPropertyDefinition.Validate(UserOptionPropertyValidationUtility.ValidateSmimeEncryptCallback)),
			new UserOptionPropertyDefinition("smimesign", typeof(bool), new UserOptionPropertyDefinition.Validate(UserOptionPropertyValidationUtility.ValidateSmimeSignCallback)),
			new UserOptionPropertyDefinition("alwaysshowbcc", typeof(bool), new UserOptionPropertyDefinition.Validate(UserOptionPropertyValidationUtility.ValidateAlwaysShowBccCallback)),
			new UserOptionPropertyDefinition("alwaysshowfrom", typeof(bool), new UserOptionPropertyDefinition.Validate(UserOptionPropertyValidationUtility.ValidateAlwaysShowFromCallback)),
			new UserOptionPropertyDefinition("composemarkup", typeof(int), new UserOptionPropertyDefinition.Validate(UserOptionPropertyValidationUtility.ValidateComposeMarkupCallback)),
			new UserOptionPropertyDefinition("composefontname", typeof(string), new UserOptionPropertyDefinition.Validate(UserOptionPropertyValidationUtility.ValidateComposeFontNameCallback)),
			new UserOptionPropertyDefinition("composefontsize", typeof(int), new UserOptionPropertyDefinition.Validate(UserOptionPropertyValidationUtility.ValidateComposeFontSizeCallback)),
			new UserOptionPropertyDefinition("composefontcolor", typeof(string), new UserOptionPropertyDefinition.Validate(UserOptionPropertyValidationUtility.ValidateComposeFontColorCallback)),
			new UserOptionPropertyDefinition("composefontflags", typeof(int), new UserOptionPropertyDefinition.Validate(UserOptionPropertyValidationUtility.ValidateComposeFontFlagsCallback)),
			new UserOptionPropertyDefinition("autoaddsignature", typeof(bool), new UserOptionPropertyDefinition.Validate(UserOptionPropertyValidationUtility.ValidateAutoAddSignatureCallback)),
			new UserOptionPropertyDefinition("signaturetext", typeof(string), new UserOptionPropertyDefinition.Validate(UserOptionPropertyValidationUtility.ValidateSignatureTextCallback)),
			new UserOptionPropertyDefinition("signaturehtml", typeof(string), new UserOptionPropertyDefinition.Validate(UserOptionPropertyValidationUtility.ValidateSignatureHtmlCallback)),
			new UserOptionPropertyDefinition("blockexternalcontent", typeof(bool), new UserOptionPropertyDefinition.Validate(UserOptionPropertyValidationUtility.ValidateBlockExternalContentCallback)),
			new UserOptionPropertyDefinition("previewmarkasread", typeof(int), new UserOptionPropertyDefinition.Validate(UserOptionPropertyValidationUtility.ValidatePreviewMarkAsReadCallback)),
			new UserOptionPropertyDefinition("markasreaddelaytime", typeof(int), new UserOptionPropertyDefinition.Validate(UserOptionPropertyValidationUtility.ValidateMarkAsReadDelaytimeCallback)),
			new UserOptionPropertyDefinition("nextselection", typeof(int), new UserOptionPropertyDefinition.Validate(UserOptionPropertyValidationUtility.ValidateNextSelectionCallback)),
			new UserOptionPropertyDefinition("readreceipt", typeof(int), new UserOptionPropertyDefinition.Validate(UserOptionPropertyValidationUtility.ValidateReadReceiptCallback)),
			new UserOptionPropertyDefinition("emptydeleteditemsonlogoff", typeof(bool), new UserOptionPropertyDefinition.Validate(UserOptionPropertyValidationUtility.ValidateEmptyDeletedItemsOnLogoffCallback)),
			new UserOptionPropertyDefinition("navigationbarwidth", typeof(int), new UserOptionPropertyDefinition.Validate(UserOptionPropertyValidationUtility.ValidateNavigationBarWidthCallback)),
			new UserOptionPropertyDefinition("isminibarvisible", typeof(bool), new UserOptionPropertyDefinition.Validate(UserOptionPropertyValidationUtility.ValidateMiniBarCallback)),
			new UserOptionPropertyDefinition("isquicklinksbarvisible", typeof(bool), new UserOptionPropertyDefinition.Validate(UserOptionPropertyValidationUtility.ValidateQuickLinksCallback)),
			new UserOptionPropertyDefinition("istaskdetailsvisible", typeof(bool), new UserOptionPropertyDefinition.Validate(UserOptionPropertyValidationUtility.ValidateTaskDetailsCallback)),
			new UserOptionPropertyDefinition("isdocumentfavoritesvisible", typeof(bool), new UserOptionPropertyDefinition.Validate(UserOptionPropertyValidationUtility.ValidateDocumentFavoritesCallback)),
			new UserOptionPropertyDefinition("isoutlooksharedfoldersvisible", typeof(bool), new UserOptionPropertyDefinition.Validate(UserOptionPropertyValidationUtility.ValidateOutlookSharedFoldersCallback)),
			new UserOptionPropertyDefinition("formatbarstate", typeof(int), new UserOptionPropertyDefinition.Validate(UserOptionPropertyValidationUtility.ValidateFormatBarStateCallback)),
			new UserOptionPropertyDefinition("mrufonts", typeof(string), new UserOptionPropertyDefinition.Validate(UserOptionPropertyValidationUtility.ValidateMruFontsCallback)),
			new UserOptionPropertyDefinition("primarynavigationcollapsed", typeof(bool), new UserOptionPropertyDefinition.Validate(UserOptionPropertyValidationUtility.ValidatePrimaryNavigationCollapsedCallback)),
			new UserOptionPropertyDefinition("themeStorageId", typeof(string), new UserOptionPropertyDefinition.Validate(UserOptionPropertyValidationUtility.ValidateThemeStorageIdCallback)),
			new UserOptionPropertyDefinition("MailFindBarOn", typeof(bool), new UserOptionPropertyDefinition.Validate(UserOptionPropertyValidationUtility.ValidateFindBarOnCallback)),
			new UserOptionPropertyDefinition("CalendarFindBarOn", typeof(bool), new UserOptionPropertyDefinition.Validate(UserOptionPropertyValidationUtility.ValidateFindBarOnCallback)),
			new UserOptionPropertyDefinition("ContactsFindBarOn", typeof(bool), new UserOptionPropertyDefinition.Validate(UserOptionPropertyValidationUtility.ValidateFindBarOnCallback)),
			new UserOptionPropertyDefinition("SearchScope", typeof(int), new UserOptionPropertyDefinition.Validate(UserOptionPropertyValidationUtility.ValidateSearchScopeCallback)),
			new UserOptionPropertyDefinition("ContactsSearchScope", typeof(int), new UserOptionPropertyDefinition.Validate(UserOptionPropertyValidationUtility.ValidateContactsSearchScopeCallback)),
			new UserOptionPropertyDefinition("TasksSearchScope", typeof(int), new UserOptionPropertyDefinition.Validate(UserOptionPropertyValidationUtility.ValidateTasksSearchScopeCallback)),
			new UserOptionPropertyDefinition("IsOptimizedForAccessibility", typeof(bool), new UserOptionPropertyDefinition.Validate(UserOptionPropertyValidationUtility.ValidateIsOptimizedForAccessibilityCallback)),
			new UserOptionPropertyDefinition("NewEnabledPonts", typeof(int), new UserOptionPropertyDefinition.Validate(UserOptionPropertyValidationUtility.ValidateEnabledPontsCallback)),
			new UserOptionPropertyDefinition("FlagAction", typeof(int), new UserOptionPropertyDefinition.Validate(UserOptionPropertyValidationUtility.ValidateFlagActionCallback)),
			new UserOptionPropertyDefinition("AddRecipientsToAutoCompleteCache", typeof(bool), new UserOptionPropertyDefinition.Validate(UserOptionPropertyValidationUtility.ValidateAddRecipientsToAutoCompleteCacheCallBack)),
			new UserOptionPropertyDefinition("ManuallyPickCertificate", typeof(bool), new UserOptionPropertyDefinition.Validate(UserOptionPropertyValidationUtility.ValidateManuallyPickCertificateCallback)),
			new UserOptionPropertyDefinition("SigningCertificateSubject", typeof(string), new UserOptionPropertyDefinition.Validate(UserOptionPropertyValidationUtility.ValidateSigningCertificateSubjectCallback)),
			new UserOptionPropertyDefinition("SigningCertificateId", typeof(string), new UserOptionPropertyDefinition.Validate(UserOptionPropertyValidationUtility.ValidateSigningCertificateIdCallback)),
			new UserOptionPropertyDefinition("UseDataCenterCustomTheme", typeof(int), new UserOptionPropertyDefinition.Validate(UserOptionPropertyValidationUtility.ValidateUseDataCenterCustomThemeCallback)),
			new UserOptionPropertyDefinition("ConversationSortOrder", typeof(int), new UserOptionPropertyDefinition.Validate(UserOptionPropertyValidationUtility.ValidateConversationSortOrderCallback)),
			new UserOptionPropertyDefinition("ShowTreeInListView", typeof(bool), new UserOptionPropertyDefinition.Validate(UserOptionPropertyValidationUtility.ValidateShowTreeInListViewCallback)),
			new UserOptionPropertyDefinition("HideDeletedItems", typeof(bool), new UserOptionPropertyDefinition.Validate(UserOptionPropertyValidationUtility.ValidateHideDeletedItemsCallback)),
			new UserOptionPropertyDefinition("HideMailTipsByDefault", typeof(bool), new UserOptionPropertyDefinition.Validate(UserOptionPropertyValidationUtility.ValidateHideMailTipsByDefaultCallback)),
			new UserOptionPropertyDefinition("sendaddressdefault", typeof(string), new UserOptionPropertyDefinition.Validate(UserOptionPropertyValidationUtility.ValidateSendAddressDefaultCallback))
		};

		// Token: 0x02000284 RID: 644
		internal enum UserOptionPropertyID
		{
			// Token: 0x04001173 RID: 4467
			TimeZone,
			// Token: 0x04001174 RID: 4468
			TimeFormat,
			// Token: 0x04001175 RID: 4469
			DateFormat,
			// Token: 0x04001176 RID: 4470
			WeekStartDay,
			// Token: 0x04001177 RID: 4471
			HourIncrement,
			// Token: 0x04001178 RID: 4472
			ShowWeekNumbers,
			// Token: 0x04001179 RID: 4473
			CheckNameInContactsFirst,
			// Token: 0x0400117A RID: 4474
			FirstWeekOfYear,
			// Token: 0x0400117B RID: 4475
			EnableReminders,
			// Token: 0x0400117C RID: 4476
			EnableReminderSound,
			// Token: 0x0400117D RID: 4477
			NewItemNotify,
			// Token: 0x0400117E RID: 4478
			ViewRowCount,
			// Token: 0x0400117F RID: 4479
			BasicViewRowCount,
			// Token: 0x04001180 RID: 4480
			SpellingDictionaryLanguage,
			// Token: 0x04001181 RID: 4481
			SpellingIgnoreUppercase,
			// Token: 0x04001182 RID: 4482
			SpellingIgnoreMixedDigits,
			// Token: 0x04001183 RID: 4483
			SpellingCheckBeforeSend,
			// Token: 0x04001184 RID: 4484
			SmimeEncrypt,
			// Token: 0x04001185 RID: 4485
			SmimeSign,
			// Token: 0x04001186 RID: 4486
			AlwaysShowBcc,
			// Token: 0x04001187 RID: 4487
			AlwaysShowFrom,
			// Token: 0x04001188 RID: 4488
			ComposeMarkup,
			// Token: 0x04001189 RID: 4489
			ComposeFontName,
			// Token: 0x0400118A RID: 4490
			ComposeFontSize,
			// Token: 0x0400118B RID: 4491
			ComposeFontColor,
			// Token: 0x0400118C RID: 4492
			ComposeFontFlags,
			// Token: 0x0400118D RID: 4493
			AutoAddSignature,
			// Token: 0x0400118E RID: 4494
			SignatureText,
			// Token: 0x0400118F RID: 4495
			SignatureHtml,
			// Token: 0x04001190 RID: 4496
			BlockExternalContent,
			// Token: 0x04001191 RID: 4497
			PreviewMarkAsRead,
			// Token: 0x04001192 RID: 4498
			MarkAsReadDelaytime,
			// Token: 0x04001193 RID: 4499
			NextSelection,
			// Token: 0x04001194 RID: 4500
			ReadReceipt,
			// Token: 0x04001195 RID: 4501
			EmptyDeletedItemsOnLogoff,
			// Token: 0x04001196 RID: 4502
			NavigationBarWidth,
			// Token: 0x04001197 RID: 4503
			IsMiniBarVisible,
			// Token: 0x04001198 RID: 4504
			IsQuickLinksBarVisible,
			// Token: 0x04001199 RID: 4505
			IsTaskDetailsVisible,
			// Token: 0x0400119A RID: 4506
			IsDocumentFavoritesVisible,
			// Token: 0x0400119B RID: 4507
			IsOutlookSharedFoldersVisible,
			// Token: 0x0400119C RID: 4508
			FormatBarState,
			// Token: 0x0400119D RID: 4509
			MruFonts,
			// Token: 0x0400119E RID: 4510
			PrimaryNavigationCollapsed,
			// Token: 0x0400119F RID: 4511
			ThemeStorageId,
			// Token: 0x040011A0 RID: 4512
			MailFindBarOn,
			// Token: 0x040011A1 RID: 4513
			CalendarFindBarOn,
			// Token: 0x040011A2 RID: 4514
			ContactsFindBarOn,
			// Token: 0x040011A3 RID: 4515
			SearchScope,
			// Token: 0x040011A4 RID: 4516
			ContactsSearchScope,
			// Token: 0x040011A5 RID: 4517
			TasksSearchScope,
			// Token: 0x040011A6 RID: 4518
			IsOptimizedForAccessibility,
			// Token: 0x040011A7 RID: 4519
			NewEnabledPonts,
			// Token: 0x040011A8 RID: 4520
			FlagAction,
			// Token: 0x040011A9 RID: 4521
			AddRecipientsToAutoCompleteCache,
			// Token: 0x040011AA RID: 4522
			ManuallyPickCertificate,
			// Token: 0x040011AB RID: 4523
			SigningCertificateSubject,
			// Token: 0x040011AC RID: 4524
			SigningCertificateId,
			// Token: 0x040011AD RID: 4525
			UseDataCenterCustomTheme,
			// Token: 0x040011AE RID: 4526
			ConversationSortOrder,
			// Token: 0x040011AF RID: 4527
			ShowTreeInListView,
			// Token: 0x040011B0 RID: 4528
			HideDeletedItems,
			// Token: 0x040011B1 RID: 4529
			HideMailTipsByDefault,
			// Token: 0x040011B2 RID: 4530
			SendAddressDefault
		}
	}
}
