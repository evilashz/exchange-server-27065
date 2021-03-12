using System;
using System.Globalization;
using System.Text.RegularExpressions;
using Microsoft.Exchange.Data.Search;
using Microsoft.Exchange.Diagnostics.Components.Clients;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.Clients.Owa.Core
{
	// Token: 0x02000285 RID: 645
	public sealed class UserOptionPropertyValidationUtility
	{
		// Token: 0x06001689 RID: 5769 RVA: 0x000848A6 File Offset: 0x00082AA6
		private UserOptionPropertyValidationUtility()
		{
		}

		// Token: 0x0600168A RID: 5770 RVA: 0x000848B0 File Offset: 0x00082AB0
		private static object ValidateIntCollection(object value, object defaultValue, int[] validValues)
		{
			if (value != null && validValues != null)
			{
				try
				{
					int num = (int)value;
					for (int i = 0; i < validValues.Length; i++)
					{
						if (num == validValues[i])
						{
							ExTraceGlobals.UserOptionsDataTracer.TraceDebug(0L, "Returning original value: {0}", new object[]
							{
								value
							});
							return value;
						}
					}
				}
				catch (InvalidCastException)
				{
					ExTraceGlobals.UserOptionsTracer.TraceDebug(0L, "Failed to cast '{0}' to int type", new object[]
					{
						value
					});
				}
			}
			ExTraceGlobals.UserOptionsDataTracer.TraceDebug(0L, "Returning default value: {0}", new object[]
			{
				defaultValue
			});
			return defaultValue;
		}

		// Token: 0x0600168B RID: 5771 RVA: 0x00084958 File Offset: 0x00082B58
		private static object ValidateIntRange(object value, object defaultValue, int minValidValue, int maxValidValue)
		{
			if (value != null)
			{
				try
				{
					int num = (int)value;
					if (num <= maxValidValue && num >= minValidValue)
					{
						ExTraceGlobals.UserOptionsDataTracer.TraceDebug(0L, "Returning original value: {0}", new object[]
						{
							value
						});
						return value;
					}
				}
				catch (InvalidCastException)
				{
					ExTraceGlobals.UserOptionsTracer.TraceDebug(0L, "Failed to cast '{0}' to int type", new object[]
					{
						value
					});
				}
			}
			ExTraceGlobals.UserOptionsDataTracer.TraceDebug(0L, "Returning default value: {0}", new object[]
			{
				defaultValue
			});
			return defaultValue;
		}

		// Token: 0x0600168C RID: 5772 RVA: 0x000849F0 File Offset: 0x00082BF0
		private static object ValidateBoolValue(object value, object defaultValue)
		{
			if (value != null)
			{
				try
				{
					bool flag = (bool)value;
					ExTraceGlobals.UserOptionsDataTracer.TraceDebug(0L, "Returning original value: {0}", new object[]
					{
						value
					});
					return value;
				}
				catch (InvalidCastException)
				{
					ExTraceGlobals.UserOptionsTracer.TraceDebug(0L, "Failed to cast '{0}' to bool type", new object[]
					{
						value
					});
				}
			}
			ExTraceGlobals.UserOptionsDataTracer.TraceDebug(0L, "Returning default value: {0}", new object[]
			{
				defaultValue
			});
			return defaultValue;
		}

		// Token: 0x0600168D RID: 5773 RVA: 0x00084A78 File Offset: 0x00082C78
		private static object ValidateDateTimeFormat(object value, string defaultFormat)
		{
			string text = (string)value;
			if (!string.IsNullOrEmpty(text) && text.Length <= 80)
			{
				try
				{
					ExDateTime.UtcNow.ToString(text, CultureInfo.CurrentUICulture);
					ExTraceGlobals.UserOptionsDataTracer.TraceDebug(0L, "Returning original value: {0}", new object[]
					{
						value
					});
					return value;
				}
				catch (FormatException)
				{
					ExTraceGlobals.UserOptionsTracer.TraceDebug(0L, "'{0}' is not a valid DateTime format.", new object[]
					{
						value
					});
				}
			}
			ExTraceGlobals.UserOptionsDataTracer.TraceDebug<string>(0L, "Returning default value: {0}", defaultFormat);
			return defaultFormat;
		}

		// Token: 0x0600168E RID: 5774 RVA: 0x00084B1C File Offset: 0x00082D1C
		internal static object ValidateViewRowCountCallbackHelp(object value, int defaultValue)
		{
			if (value != null)
			{
				try
				{
					int num = (int)value;
					int num2;
					if (UserOptionPropertyValidationUtility.ViewRowCountValues.IsConfigValueDefined)
					{
						num2 = ((1000 < UserOptionPropertyValidationUtility.ViewRowCountValues.MaxViewRowCountConfigValue) ? 1000 : UserOptionPropertyValidationUtility.ViewRowCountValues.MaxViewRowCountConfigValue);
					}
					else
					{
						num2 = 100;
					}
					if (num >= 5 && num <= num2 && ((num <= 50 && num % 5 == 0) || (num > 50 && num % 25 == 0)))
					{
						ExTraceGlobals.UserOptionsDataTracer.TraceDebug(0L, "Returning original value: {0}", new object[]
						{
							value
						});
						return value;
					}
				}
				catch (InvalidCastException)
				{
					ExTraceGlobals.UserOptionsTracer.TraceDebug(0L, "Failed to cast '{0}' to int type", new object[]
					{
						value
					});
				}
			}
			ExTraceGlobals.UserOptionsDataTracer.TraceDebug<int>(0L, "Returning default value: {0}", defaultValue);
			return defaultValue;
		}

		// Token: 0x0600168F RID: 5775 RVA: 0x00084BE8 File Offset: 0x00082DE8
		internal static object ValidateViewRowCountCallback(object value)
		{
			return UserOptionPropertyValidationUtility.ValidateViewRowCountCallbackHelp(value, 50);
		}

		// Token: 0x06001690 RID: 5776 RVA: 0x00084BF2 File Offset: 0x00082DF2
		internal static object ValidateBasicViewRowCountCallback(object value)
		{
			return UserOptionPropertyValidationUtility.ValidateViewRowCountCallbackHelp(value, 20);
		}

		// Token: 0x06001691 RID: 5777 RVA: 0x00084BFC File Offset: 0x00082DFC
		internal static object ValidateNextSelectionCallback(object value)
		{
			return (NextSelectionDirection)UserOptionPropertyValidationUtility.ValidateIntRange(value, 1, 0, 2);
		}

		// Token: 0x06001692 RID: 5778 RVA: 0x00084C18 File Offset: 0x00082E18
		internal static object ValidateTimeZoneCallback(object value)
		{
			string text = value as string;
			if (text != null)
			{
				ExTimeZone exTimeZone = null;
				bool flag = ExTimeZoneEnumerator.Instance.TryGetTimeZoneByName(text, out exTimeZone);
				if (flag)
				{
					ExTraceGlobals.UserOptionsDataTracer.TraceDebug(0L, "Returning original value: {0}", new object[]
					{
						value
					});
					return value;
				}
			}
			ExTraceGlobals.UserOptionsDataTracer.TraceDebug<string>(0L, "Returning default value: {0}", UserOptionPropertyValidationUtility.DefaultTimeZone);
			return UserOptionPropertyValidationUtility.DefaultTimeZone;
		}

		// Token: 0x06001693 RID: 5779 RVA: 0x00084C7C File Offset: 0x00082E7C
		internal static object ValidateTimeFormatCallback(object value)
		{
			return UserOptionPropertyValidationUtility.ValidateDateTimeFormat(value, DateTimeFormatInfo.CurrentInfo.ShortTimePattern);
		}

		// Token: 0x06001694 RID: 5780 RVA: 0x00084C8E File Offset: 0x00082E8E
		internal static object ValidateDateFormatCallback(object value)
		{
			return UserOptionPropertyValidationUtility.ValidateDateTimeFormat(value, DateTimeFormatInfo.CurrentInfo.ShortDatePattern);
		}

		// Token: 0x06001695 RID: 5781 RVA: 0x00084CA0 File Offset: 0x00082EA0
		internal static object ValidateWeekStartDayCallback(object value)
		{
			return (DayOfWeek)UserOptionPropertyValidationUtility.ValidateIntRange(value, 0, 0, 6);
		}

		// Token: 0x06001696 RID: 5782 RVA: 0x00084CBC File Offset: 0x00082EBC
		internal static object ValidateHourIncrementCallback(object value)
		{
			return UserOptionPropertyValidationUtility.ValidateIntCollection(value, 30, new int[]
			{
				15,
				30
			});
		}

		// Token: 0x06001697 RID: 5783 RVA: 0x00084CE8 File Offset: 0x00082EE8
		internal static object ValidateShowWeekNumbersCallback(object value)
		{
			return UserOptionPropertyValidationUtility.ValidateBoolValue(value, false);
		}

		// Token: 0x06001698 RID: 5784 RVA: 0x00084CF6 File Offset: 0x00082EF6
		internal static object ValidateCheckNameInContactsFirstCallback(object value)
		{
			return UserOptionPropertyValidationUtility.ValidateBoolValue(value, false);
		}

		// Token: 0x06001699 RID: 5785 RVA: 0x00084D04 File Offset: 0x00082F04
		internal static object ValidateFirstWeekOfYearCallback(object value)
		{
			return UserOptionPropertyValidationUtility.ValidateIntRange(value, 1, 0, 3);
		}

		// Token: 0x0600169A RID: 5786 RVA: 0x00084D14 File Offset: 0x00082F14
		internal static object ValidateEnableRemindersCallback(object value)
		{
			return UserOptionPropertyValidationUtility.ValidateBoolValue(value, true);
		}

		// Token: 0x0600169B RID: 5787 RVA: 0x00084D22 File Offset: 0x00082F22
		internal static object ValidateEnableReminderSoundCallback(object value)
		{
			return UserOptionPropertyValidationUtility.ValidateBoolValue(value, true);
		}

		// Token: 0x0600169C RID: 5788 RVA: 0x00084D30 File Offset: 0x00082F30
		internal static object ValidateNewItemNotifyCallback(object value)
		{
			return (NewNotification)UserOptionPropertyValidationUtility.ValidateIntCollection(value, 15, UserOptionPropertyValidationUtility.ValidNewItemNotifyCollection);
		}

		// Token: 0x0600169D RID: 5789 RVA: 0x00084D4E File Offset: 0x00082F4E
		internal static object ValidateSpellingDictionaryLanguageCallback(object value)
		{
			return value;
		}

		// Token: 0x0600169E RID: 5790 RVA: 0x00084D51 File Offset: 0x00082F51
		internal static object ValidateSpellingIgnoreUppercaseCallback(object value)
		{
			return UserOptionPropertyValidationUtility.ValidateBoolValue(value, false);
		}

		// Token: 0x0600169F RID: 5791 RVA: 0x00084D5F File Offset: 0x00082F5F
		internal static object ValidateSpellingIgnoreMixedDigitsCallback(object value)
		{
			return UserOptionPropertyValidationUtility.ValidateBoolValue(value, false);
		}

		// Token: 0x060016A0 RID: 5792 RVA: 0x00084D70 File Offset: 0x00082F70
		internal static object ValidateSpellingCheckBeforeSendCallback(object value)
		{
			bool flag = UserOptionPropertyValidationUtility.SpellingCheckBeforeSendValues.IsCofigValueDefine && UserOptionPropertyValidationUtility.SpellingCheckBeforeSendValues.SpellingCheckBeforeSendConfigValue;
			return UserOptionPropertyValidationUtility.ValidateBoolValue(value, flag);
		}

		// Token: 0x060016A1 RID: 5793 RVA: 0x00084D99 File Offset: 0x00082F99
		internal static object ValidateSmimeEncryptCallback(object value)
		{
			return UserOptionPropertyValidationUtility.ValidateBoolValue(value, false);
		}

		// Token: 0x060016A2 RID: 5794 RVA: 0x00084DA7 File Offset: 0x00082FA7
		internal static object ValidateSmimeSignCallback(object value)
		{
			return UserOptionPropertyValidationUtility.ValidateBoolValue(value, false);
		}

		// Token: 0x060016A3 RID: 5795 RVA: 0x00084DB5 File Offset: 0x00082FB5
		internal static object ValidateAlwaysShowBccCallback(object value)
		{
			return UserOptionPropertyValidationUtility.ValidateBoolValue(value, false);
		}

		// Token: 0x060016A4 RID: 5796 RVA: 0x00084DC3 File Offset: 0x00082FC3
		internal static object ValidateAlwaysShowFromCallback(object value)
		{
			return UserOptionPropertyValidationUtility.ValidateBoolValue(value, false);
		}

		// Token: 0x060016A5 RID: 5797 RVA: 0x00084DD1 File Offset: 0x00082FD1
		internal static object ValidateComposeMarkupCallback(object value)
		{
			return (Markup)UserOptionPropertyValidationUtility.ValidateIntRange(value, 0, 0, 1);
		}

		// Token: 0x060016A6 RID: 5798 RVA: 0x00084DEC File Offset: 0x00082FEC
		internal static object ValidateComposeFontNameCallback(object value)
		{
			int num = 100;
			string defaultFontName = Utilities.GetDefaultFontName();
			string text = (string)value;
			if (!string.IsNullOrEmpty(text) && text.Length <= num)
			{
				ExTraceGlobals.UserOptionsDataTracer.TraceDebug<string>(0L, "Returning default value: {0}", defaultFontName);
				return value;
			}
			return defaultFontName;
		}

		// Token: 0x060016A7 RID: 5799 RVA: 0x00084E2F File Offset: 0x0008302F
		internal static object ValidateComposeFontSizeCallback(object value)
		{
			return UserOptionPropertyValidationUtility.ValidateIntRange(value, 2, 1, 7);
		}

		// Token: 0x060016A8 RID: 5800 RVA: 0x00084E40 File Offset: 0x00083040
		internal static object ValidateComposeFontColorCallback(object value)
		{
			string text = (string)value;
			if (!string.IsNullOrEmpty(text) && UserOptionPropertyValidationUtility.validColorRegex.Match(text).Success)
			{
				ExTraceGlobals.UserOptionsDataTracer.TraceDebug(0L, "Returning original value: {0}", new object[]
				{
					value
				});
				return value;
			}
			ExTraceGlobals.UserOptionsDataTracer.TraceDebug<string>(0L, "Returning default value: {0}", "#000000");
			return "#000000";
		}

		// Token: 0x060016A9 RID: 5801 RVA: 0x00084EA8 File Offset: 0x000830A8
		internal static object ValidateComposeFontFlagsCallback(object value)
		{
			return (FontFlags)UserOptionPropertyValidationUtility.ValidateIntRange(value, 0, 0, 7);
		}

		// Token: 0x060016AA RID: 5802 RVA: 0x00084EC2 File Offset: 0x000830C2
		internal static object ValidateAutoAddSignatureCallback(object value)
		{
			return UserOptionPropertyValidationUtility.ValidateBoolValue(value, false);
		}

		// Token: 0x060016AB RID: 5803 RVA: 0x00084ED0 File Offset: 0x000830D0
		internal static object ValidateSignatureTextCallback(object value)
		{
			string text = (string)value;
			if (string.IsNullOrEmpty(text))
			{
				return value;
			}
			int num;
			if (UserOptionPropertyValidationUtility.SignatureValues.IsConfigValueDefined)
			{
				num = ((UserOptionPropertyValidationUtility.SignatureValues.SignatureMaxLengthConfigValue < 16000) ? UserOptionPropertyValidationUtility.SignatureValues.SignatureMaxLengthConfigValue : 16000);
			}
			else
			{
				num = 4096;
			}
			if (text.Length <= num)
			{
				ExTraceGlobals.UserOptionsDataTracer.TraceDebug(0L, "Returning original value: {0}", new object[]
				{
					value
				});
				return value;
			}
			string text2 = text.Substring(0, 4096);
			ExTraceGlobals.UserOptionsDataTracer.TraceDebug<int, string>(0L, "Signature is longer that max length '{0}'. Returning truncated value: {1}", 4096, text2);
			return text2;
		}

		// Token: 0x060016AC RID: 5804 RVA: 0x00084F63 File Offset: 0x00083163
		internal static object ValidateSignatureHtmlCallback(object value)
		{
			return UserOptionPropertyValidationUtility.ValidateSignatureTextCallback(value);
		}

		// Token: 0x060016AD RID: 5805 RVA: 0x00084F6B File Offset: 0x0008316B
		internal static object ValidateBlockExternalContentCallback(object value)
		{
			return UserOptionPropertyValidationUtility.ValidateBoolValue(value, true);
		}

		// Token: 0x060016AE RID: 5806 RVA: 0x00084F79 File Offset: 0x00083179
		internal static object ValidatePreviewMarkAsReadCallback(object value)
		{
			return (MarkAsRead)UserOptionPropertyValidationUtility.ValidateIntRange(value, 1, 0, 2);
		}

		// Token: 0x060016AF RID: 5807 RVA: 0x00084F93 File Offset: 0x00083193
		internal static object ValidateMarkAsReadDelaytimeCallback(object value)
		{
			return UserOptionPropertyValidationUtility.ValidateIntRange(value, 5, 0, 30);
		}

		// Token: 0x060016B0 RID: 5808 RVA: 0x00084FA4 File Offset: 0x000831A4
		internal static object ValidateReadReceiptCallback(object value)
		{
			return (ReadReceiptResponse)UserOptionPropertyValidationUtility.ValidateIntRange(value, 0, 0, 2);
		}

		// Token: 0x060016B1 RID: 5809 RVA: 0x00084FBE File Offset: 0x000831BE
		internal static object ValidateEmptyDeletedItemsOnLogoffCallback(object value)
		{
			return UserOptionPropertyValidationUtility.ValidateBoolValue(value, false);
		}

		// Token: 0x060016B2 RID: 5810 RVA: 0x00084FCC File Offset: 0x000831CC
		internal static object ValidateNavigationBarWidthCallback(object value)
		{
			return UserOptionPropertyValidationUtility.ValidateIntRange(value, 175, 50, 2000);
		}

		// Token: 0x060016B3 RID: 5811 RVA: 0x00084FE5 File Offset: 0x000831E5
		internal static object ValidateMiniBarCallback(object value)
		{
			return UserOptionPropertyValidationUtility.ValidateBoolValue(value, false);
		}

		// Token: 0x060016B4 RID: 5812 RVA: 0x00084FF3 File Offset: 0x000831F3
		internal static object ValidateQuickLinksCallback(object value)
		{
			return UserOptionPropertyValidationUtility.ValidateBoolValue(value, false);
		}

		// Token: 0x060016B5 RID: 5813 RVA: 0x00085001 File Offset: 0x00083201
		internal static object ValidateTaskDetailsCallback(object value)
		{
			return UserOptionPropertyValidationUtility.ValidateBoolValue(value, true);
		}

		// Token: 0x060016B6 RID: 5814 RVA: 0x0008500F File Offset: 0x0008320F
		internal static object ValidateDocumentFavoritesCallback(object value)
		{
			return UserOptionPropertyValidationUtility.ValidateBoolValue(value, true);
		}

		// Token: 0x060016B7 RID: 5815 RVA: 0x0008501D File Offset: 0x0008321D
		internal static object ValidateOutlookSharedFoldersCallback(object value)
		{
			return UserOptionPropertyValidationUtility.ValidateBoolValue(value, true);
		}

		// Token: 0x060016B8 RID: 5816 RVA: 0x0008502B File Offset: 0x0008322B
		internal static object ValidateFormatBarStateCallback(object value)
		{
			return (FormatBarButtonGroups)UserOptionPropertyValidationUtility.ValidateIntRange(value, FormatBarButtonGroups.BoldItalicUnderline | FormatBarButtonGroups.Lists | FormatBarButtonGroups.Indenting | FormatBarButtonGroups.ForegroundColor | FormatBarButtonGroups.BackgroundColor, 0, 16383);
		}

		// Token: 0x060016B9 RID: 5817 RVA: 0x0008504C File Offset: 0x0008324C
		internal static object ValidateMruFontsCallback(object value)
		{
			int num = 1000;
			string empty = string.Empty;
			string text = (string)value;
			if (string.IsNullOrEmpty(text) || text.Length > num)
			{
				ExTraceGlobals.UserOptionsDataTracer.TraceDebug<string>(0L, "Returning default value: {0}", empty);
				return empty;
			}
			return text;
		}

		// Token: 0x060016BA RID: 5818 RVA: 0x00085092 File Offset: 0x00083292
		internal static object ValidatePrimaryNavigationCollapsedCallback(object value)
		{
			return UserOptionPropertyValidationUtility.ValidateBoolValue(value, false);
		}

		// Token: 0x060016BB RID: 5819 RVA: 0x000850A0 File Offset: 0x000832A0
		internal static object ValidateThemeStorageIdCallback(object value)
		{
			string text = value as string;
			if (string.IsNullOrEmpty(text))
			{
				return string.Empty;
			}
			return text;
		}

		// Token: 0x060016BC RID: 5820 RVA: 0x000850C3 File Offset: 0x000832C3
		internal static object ValidateFindBarOnCallback(object value)
		{
			return UserOptionPropertyValidationUtility.ValidateBoolValue(value, true);
		}

		// Token: 0x060016BD RID: 5821 RVA: 0x000850D1 File Offset: 0x000832D1
		internal static object ValidateSearchScopeCallback(object value)
		{
			return (SearchScope)UserOptionPropertyValidationUtility.ValidateIntRange(value, 3, 0, 3);
		}

		// Token: 0x060016BE RID: 5822 RVA: 0x000850EB File Offset: 0x000832EB
		internal static object ValidateContactsSearchScopeCallback(object value)
		{
			return (SearchScope)UserOptionPropertyValidationUtility.ValidateIntRange(value, 0, 0, 2);
		}

		// Token: 0x060016BF RID: 5823 RVA: 0x00085105 File Offset: 0x00083305
		internal static object ValidateTasksSearchScopeCallback(object value)
		{
			return (SearchScope)UserOptionPropertyValidationUtility.ValidateIntRange(value, 0, 0, 2);
		}

		// Token: 0x060016C0 RID: 5824 RVA: 0x0008511F File Offset: 0x0008331F
		internal static object ValidateIsOptimizedForAccessibilityCallback(object value)
		{
			return UserOptionPropertyValidationUtility.ValidateBoolValue(value, false);
		}

		// Token: 0x060016C1 RID: 5825 RVA: 0x0008512D File Offset: 0x0008332D
		internal static object ValidateEnabledPontsCallback(object value)
		{
			return (PontType)UserOptionPropertyValidationUtility.ValidateIntRange(value, int.MaxValue, 0, int.MaxValue);
		}

		// Token: 0x060016C2 RID: 5826 RVA: 0x0008514F File Offset: 0x0008334F
		internal static object ValidateFlagActionCallback(object value)
		{
			return (FlagAction)UserOptionPropertyValidationUtility.ValidateIntRange(value, 2, 2, 6);
		}

		// Token: 0x060016C3 RID: 5827 RVA: 0x00085169 File Offset: 0x00083369
		internal static object ValidateAddRecipientsToAutoCompleteCacheCallBack(object value)
		{
			return UserOptionPropertyValidationUtility.ValidateBoolValue(value, true);
		}

		// Token: 0x060016C4 RID: 5828 RVA: 0x00085177 File Offset: 0x00083377
		internal static object ValidateManuallyPickCertificateCallback(object value)
		{
			return UserOptionPropertyValidationUtility.ValidateBoolValue(value, false);
		}

		// Token: 0x060016C5 RID: 5829 RVA: 0x00085185 File Offset: 0x00083385
		internal static object ValidateSigningCertificateSubjectCallback(object value)
		{
			return value;
		}

		// Token: 0x060016C6 RID: 5830 RVA: 0x00085188 File Offset: 0x00083388
		internal static object ValidateSigningCertificateIdCallback(object value)
		{
			return value;
		}

		// Token: 0x060016C7 RID: 5831 RVA: 0x0008518C File Offset: 0x0008338C
		internal static object ValidateUseDataCenterCustomThemeCallback(object value)
		{
			return UserOptionPropertyValidationUtility.ValidateIntCollection(value, -1, new int[]
			{
				0,
				1
			});
		}

		// Token: 0x060016C8 RID: 5832 RVA: 0x000851B1 File Offset: 0x000833B1
		internal static object ValidateConversationSortOrderCallback(object value)
		{
			return UserOptionPropertyValidationUtility.ValidateIntCollection(value, 5, UserOptionPropertyValidationUtility.ValidConversationSortOrderValues);
		}

		// Token: 0x060016C9 RID: 5833 RVA: 0x000851C4 File Offset: 0x000833C4
		internal static object ValidateShowTreeInListViewCallback(object value)
		{
			return UserOptionPropertyValidationUtility.ValidateBoolValue(value, false);
		}

		// Token: 0x060016CA RID: 5834 RVA: 0x000851D2 File Offset: 0x000833D2
		internal static object ValidateHideDeletedItemsCallback(object value)
		{
			return UserOptionPropertyValidationUtility.ValidateBoolValue(value, false);
		}

		// Token: 0x060016CB RID: 5835 RVA: 0x000851E0 File Offset: 0x000833E0
		internal static object ValidateHideMailTipsByDefaultCallback(object value)
		{
			return UserOptionPropertyValidationUtility.ValidateBoolValue(value, false);
		}

		// Token: 0x060016CC RID: 5836 RVA: 0x000851EE File Offset: 0x000833EE
		internal static object ValidateSendAddressDefaultCallback(object value)
		{
			return value;
		}

		// Token: 0x040011B3 RID: 4531
		private const int DefaultViewRowCount = 50;

		// Token: 0x040011B4 RID: 4532
		private const int DefaultBasicViewRowCount = 20;

		// Token: 0x040011B5 RID: 4533
		private const NextSelectionDirection DefaultNextSelection = NextSelectionDirection.Next;

		// Token: 0x040011B6 RID: 4534
		private const DayOfWeek DefaultWeekStartDay = DayOfWeek.Sunday;

		// Token: 0x040011B7 RID: 4535
		private const int DefaultHourIncrement = 30;

		// Token: 0x040011B8 RID: 4536
		private const bool DefaultShowWeekNumbers = false;

		// Token: 0x040011B9 RID: 4537
		private const bool DefaultCheckNameInContactsFirst = false;

		// Token: 0x040011BA RID: 4538
		private const int DefaultFirstWeekOfYear = 1;

		// Token: 0x040011BB RID: 4539
		private const bool DefaultEnableReminders = true;

		// Token: 0x040011BC RID: 4540
		private const bool DefaultEnableReminderSound = true;

		// Token: 0x040011BD RID: 4541
		private const NewNotification DefaultNewItemNotify = NewNotification.Sound | NewNotification.EMailToast | NewNotification.VoiceMailToast | NewNotification.FaxToast;

		// Token: 0x040011BE RID: 4542
		private const bool DefaultSpellingIgnoreUppercase = false;

		// Token: 0x040011BF RID: 4543
		private const bool DefaultSpellingIgnoreMixedDigits = false;

		// Token: 0x040011C0 RID: 4544
		private const bool DefaultSpellingCheckBeforeSend = false;

		// Token: 0x040011C1 RID: 4545
		private const bool DefaultSmimeEncrypt = false;

		// Token: 0x040011C2 RID: 4546
		private const bool DefaultSmimeSign = false;

		// Token: 0x040011C3 RID: 4547
		private const bool DefaultAlwaysShowBcc = false;

		// Token: 0x040011C4 RID: 4548
		private const bool DefaultAlwaysShowFrom = false;

		// Token: 0x040011C5 RID: 4549
		private const Markup DefaultComposeMarkup = Markup.Html;

		// Token: 0x040011C6 RID: 4550
		private const int DefaultComposeFontSize = 2;

		// Token: 0x040011C7 RID: 4551
		private const string DefaultComposeFontColor = "#000000";

		// Token: 0x040011C8 RID: 4552
		private const FontFlags DefaultComposeFontFlags = FontFlags.Normal;

		// Token: 0x040011C9 RID: 4553
		private const bool DefaultAutoAddSignature = false;

		// Token: 0x040011CA RID: 4554
		private const int DefaultSignatureMaxLength = 4096;

		// Token: 0x040011CB RID: 4555
		private const bool DefaultBlockExternalContent = true;

		// Token: 0x040011CC RID: 4556
		private const MarkAsRead DefaultPreviewMarkAsRead = MarkAsRead.OnSelectionChange;

		// Token: 0x040011CD RID: 4557
		private const int DefaultMarkAsReadDelaytime = 5;

		// Token: 0x040011CE RID: 4558
		private const ReadReceiptResponse DefaultReadReceipt = ReadReceiptResponse.DoNotAutomaticallySend;

		// Token: 0x040011CF RID: 4559
		private const bool DefaultEmptyDeletedItemsOnLogoff = false;

		// Token: 0x040011D0 RID: 4560
		private const int DefaultNavBarWidth = 175;

		// Token: 0x040011D1 RID: 4561
		private const bool DefaultMiniBarVisible = false;

		// Token: 0x040011D2 RID: 4562
		private const bool DefaultQuickLinksVisible = false;

		// Token: 0x040011D3 RID: 4563
		private const bool DefaultTaskDetailsVisible = true;

		// Token: 0x040011D4 RID: 4564
		private const bool DefaultDocumentFavoritesVisible = true;

		// Token: 0x040011D5 RID: 4565
		private const bool DefaultOutlookSharedFoldersVisible = true;

		// Token: 0x040011D6 RID: 4566
		private const FormatBarButtonGroups DefaultFormatBarState = FormatBarButtonGroups.BoldItalicUnderline | FormatBarButtonGroups.Lists | FormatBarButtonGroups.Indenting | FormatBarButtonGroups.ForegroundColor | FormatBarButtonGroups.BackgroundColor;

		// Token: 0x040011D7 RID: 4567
		private const bool PrimaryNavigationCollapsed = false;

		// Token: 0x040011D8 RID: 4568
		private const bool DefaultFindBarOn = true;

		// Token: 0x040011D9 RID: 4569
		private const SearchScope DefaultSearchScope = SearchScope.AllFoldersAndItems;

		// Token: 0x040011DA RID: 4570
		private const SearchScope DefaultContactsSearchScope = SearchScope.SelectedFolder;

		// Token: 0x040011DB RID: 4571
		private const SearchScope DefaultTasksSearchScope = SearchScope.SelectedFolder;

		// Token: 0x040011DC RID: 4572
		private const bool DefaultIsOptimizedForAccessibility = false;

		// Token: 0x040011DD RID: 4573
		private const PontType DefaultEnabledPonts = PontType.All;

		// Token: 0x040011DE RID: 4574
		private const FlagAction DefaultFlagAction = FlagAction.Today;

		// Token: 0x040011DF RID: 4575
		private const bool DefaultAddRecipientsToAutoCompleteCache = true;

		// Token: 0x040011E0 RID: 4576
		private const bool DefaultManuallyPickCertificate = false;

		// Token: 0x040011E1 RID: 4577
		private const int DefaultUseDataCenterCustomTheme = -1;

		// Token: 0x040011E2 RID: 4578
		private const ConversationSortOrder DefaultConversationSortOrder = ConversationSortOrder.ChronologicalNewestOnTop;

		// Token: 0x040011E3 RID: 4579
		private const bool DefaultShowTreeInListView = false;

		// Token: 0x040011E4 RID: 4580
		private const bool DefaultHideDeletedItems = false;

		// Token: 0x040011E5 RID: 4581
		private const bool DefaultHideMailTipsByDefault = false;

		// Token: 0x040011E6 RID: 4582
		private static readonly string DefaultTimeZone = string.Empty;

		// Token: 0x040011E7 RID: 4583
		private static readonly int[] ValidNewItemNotifyCollection = new int[]
		{
			0,
			1,
			2,
			3,
			4,
			5,
			6,
			7,
			8,
			9,
			10,
			11,
			12,
			13,
			14,
			15
		};

		// Token: 0x040011E8 RID: 4584
		private static readonly Regex validColorRegex = new Regex("^\\#[0-9a-fA-F]{6}$", RegexOptions.Compiled | RegexOptions.CultureInvariant);

		// Token: 0x040011E9 RID: 4585
		private static readonly int[] ValidConversationSortOrderValues = new int[]
		{
			5,
			9
		};

		// Token: 0x02000286 RID: 646
		internal struct ViewRowCountValues
		{
			// Token: 0x040011EA RID: 4586
			internal const int DefaultMaxViewRowCount = 100;

			// Token: 0x040011EB RID: 4587
			internal const int AllowedMaxViewRowCount = 1000;

			// Token: 0x040011EC RID: 4588
			internal const int MinViewRowCount = 5;

			// Token: 0x040011ED RID: 4589
			internal const int ThreshholdViewRowCount = 50;

			// Token: 0x040011EE RID: 4590
			internal const int LowMultipleRowCount = 5;

			// Token: 0x040011EF RID: 4591
			internal const int HighMultipleRowCount = 25;

			// Token: 0x040011F0 RID: 4592
			internal static int MaxViewRowCountConfigValue = 1000;

			// Token: 0x040011F1 RID: 4593
			internal static bool IsConfigValueDefined = false;
		}

		// Token: 0x02000287 RID: 647
		internal struct SpellingCheckBeforeSendValues
		{
			// Token: 0x040011F2 RID: 4594
			internal static bool IsCofigValueDefine;

			// Token: 0x040011F3 RID: 4595
			internal static bool SpellingCheckBeforeSendConfigValue;
		}

		// Token: 0x02000288 RID: 648
		internal struct SignatureValues
		{
			// Token: 0x040011F4 RID: 4596
			internal const int AllowedSignatureMaxLength = 16000;

			// Token: 0x040011F5 RID: 4597
			internal static int SignatureMaxLengthConfigValue = 16000;

			// Token: 0x040011F6 RID: 4598
			internal static bool IsConfigValueDefined = false;
		}
	}
}
