using System;
using System.Globalization;
using System.Text.RegularExpressions;
using Microsoft.Exchange.Data.Search;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Data.Storage.Management;
using Microsoft.Exchange.Diagnostics.Components.Clients;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.Services.Wcf.Types;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core
{
	// Token: 0x020000F9 RID: 249
	public sealed class UserConfigurationPropertyValidationUtility
	{
		// Token: 0x060008CB RID: 2251 RVA: 0x0001CD8A File Offset: 0x0001AF8A
		private UserConfigurationPropertyValidationUtility()
		{
		}

		// Token: 0x060008CC RID: 2252 RVA: 0x0001CD94 File Offset: 0x0001AF94
		internal static object ValidateViewRowCountCallbackInternal(object value, int defaultValue)
		{
			if (value != null)
			{
				try
				{
					int num = (int)value;
					int num2;
					if (UserConfigurationPropertyValidationUtility.ViewRowCountValues.IsConfigValueDefined)
					{
						num2 = ((1000 < UserConfigurationPropertyValidationUtility.ViewRowCountValues.MaxViewRowCountConfigValue) ? 1000 : UserConfigurationPropertyValidationUtility.ViewRowCountValues.MaxViewRowCountConfigValue);
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
					ExTraceGlobals.UserOptionsTracer.TraceError(0L, "Failed to cast '{0}' to int type", new object[]
					{
						value
					});
				}
			}
			ExTraceGlobals.UserOptionsDataTracer.TraceDebug<int>(0L, "Returning default value: {0}", defaultValue);
			return defaultValue;
		}

		// Token: 0x060008CD RID: 2253 RVA: 0x0001CE60 File Offset: 0x0001B060
		internal static object ValidateViewRowCountCallback(object value)
		{
			return UserConfigurationPropertyValidationUtility.ValidateViewRowCountCallbackInternal(value, 50);
		}

		// Token: 0x060008CE RID: 2254 RVA: 0x0001CE6A File Offset: 0x0001B06A
		internal static object ValidateBasicViewRowCountCallback(object value)
		{
			return UserConfigurationPropertyValidationUtility.ValidateViewRowCountCallbackInternal(value, 20);
		}

		// Token: 0x060008CF RID: 2255 RVA: 0x0001CE74 File Offset: 0x0001B074
		internal static object ValidateNextSelectionCallback(object value)
		{
			return (NextSelectionDirection)UserConfigurationPropertyValidationUtility.ValidateIntRange(value, 1, 0, 2);
		}

		// Token: 0x060008D0 RID: 2256 RVA: 0x0001CE90 File Offset: 0x0001B090
		internal static object ValidateTimeZoneCallback(object value)
		{
			string text = value as string;
			if (text != null)
			{
				ExTimeZone exTimeZone;
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
			ExTraceGlobals.UserOptionsDataTracer.TraceDebug<string>(0L, "Returning default value: {0}", UserConfigurationPropertyValidationUtility.DefaultTimeZone);
			return UserConfigurationPropertyValidationUtility.DefaultTimeZone;
		}

		// Token: 0x060008D1 RID: 2257 RVA: 0x0001CEF4 File Offset: 0x0001B0F4
		internal static object ValidateTimeFormatCallback(object value)
		{
			string text;
			if (!MailboxRegionalConfiguration.ValidateTimeFormat(CultureInfo.CurrentUICulture, value, out text))
			{
				ExTraceGlobals.UserOptionsDataTracer.TraceDebug<string, object>(0L, "Returning default TimeFormat value: {0}. OriginalFormat: '{1}'", text, (value == null) ? "isnull" : ((value is string) ? value : "is not a string"));
				return text;
			}
			ExTraceGlobals.UserOptionsDataTracer.TraceDebug(0L, "Returning user specified TimeFormat value: {0}", new object[]
			{
				value
			});
			return value;
		}

		// Token: 0x060008D2 RID: 2258 RVA: 0x0001CF5C File Offset: 0x0001B15C
		internal static object ValidateDateFormatCallback(object value)
		{
			string text;
			if (!MailboxRegionalConfiguration.ValidateDateFormat(CultureInfo.CurrentUICulture, value, out text))
			{
				ExTraceGlobals.UserOptionsDataTracer.TraceDebug<string, object>(0L, "Returning default DateFormat value: {0}. OriginalFormat: '{1}'", text, (value == null) ? "isnull" : ((value is string) ? value : "is not a string"));
				return text;
			}
			ExTraceGlobals.UserOptionsDataTracer.TraceDebug(0L, "Returning user specified DateFormat value: {0}", new object[]
			{
				value
			});
			return value;
		}

		// Token: 0x060008D3 RID: 2259 RVA: 0x0001CFC4 File Offset: 0x0001B1C4
		internal static object ValidateWeekStartDayCallback(object value)
		{
			return (System.DayOfWeek)UserConfigurationPropertyValidationUtility.ValidateIntRange(value, (int)DateTimeFormatInfo.CurrentInfo.FirstDayOfWeek, 0, 6);
		}

		// Token: 0x060008D4 RID: 2260 RVA: 0x0001CFE7 File Offset: 0x0001B1E7
		internal static object ValidateHourIncrementCallback(object value)
		{
			return UserConfigurationPropertyValidationUtility.ValidateIntCollection(value, 30, UserConfigurationPropertyValidationUtility.ValidHourIncrementOptions);
		}

		// Token: 0x060008D5 RID: 2261 RVA: 0x0001CFFB File Offset: 0x0001B1FB
		internal static object ValidateShowWeekNumbersCallback(object value)
		{
			return UserConfigurationPropertyValidationUtility.ValidateBoolValue(value, false);
		}

		// Token: 0x060008D6 RID: 2262 RVA: 0x0001D009 File Offset: 0x0001B209
		internal static object ValidateCheckNameInContactsFirstCallback(object value)
		{
			return UserConfigurationPropertyValidationUtility.ValidateBoolValue(value, false);
		}

		// Token: 0x060008D7 RID: 2263 RVA: 0x0001D017 File Offset: 0x0001B217
		internal static object ValidateFirstWeekOfYearCallback(object value)
		{
			return UserConfigurationPropertyValidationUtility.ValidateIntRange(value, DateTimeFormatInfo.CurrentInfo.CalendarWeekRule.ToFirstWeekRules(), 0, 3);
		}

		// Token: 0x060008D8 RID: 2264 RVA: 0x0001D035 File Offset: 0x0001B235
		internal static object ValidateEnableRemindersCallback(object value)
		{
			return UserConfigurationPropertyValidationUtility.ValidateBoolValue(value, true);
		}

		// Token: 0x060008D9 RID: 2265 RVA: 0x0001D043 File Offset: 0x0001B243
		internal static object ValidateEnableReminderSoundCallback(object value)
		{
			return UserConfigurationPropertyValidationUtility.ValidateBoolValue(value, true);
		}

		// Token: 0x060008DA RID: 2266 RVA: 0x0001D051 File Offset: 0x0001B251
		internal static object ValidateNewItemNotifyCallback(object value)
		{
			return (NewNotification)UserConfigurationPropertyValidationUtility.ValidateIntCollection(value, 15, UserConfigurationPropertyValidationUtility.ValidNewItemNotifyCollection);
		}

		// Token: 0x060008DB RID: 2267 RVA: 0x0001D06F File Offset: 0x0001B26F
		internal static object ValidateSpellingDictionaryLanguageCallback(object value)
		{
			if (value is int)
			{
				return value;
			}
			return -1;
		}

		// Token: 0x060008DC RID: 2268 RVA: 0x0001D081 File Offset: 0x0001B281
		internal static object ValidateSpellingIgnoreUppercaseCallback(object value)
		{
			return UserConfigurationPropertyValidationUtility.ValidateBoolValue(value, false);
		}

		// Token: 0x060008DD RID: 2269 RVA: 0x0001D08F File Offset: 0x0001B28F
		internal static object ValidateSpellingIgnoreMixedDigitsCallback(object value)
		{
			return UserConfigurationPropertyValidationUtility.ValidateBoolValue(value, false);
		}

		// Token: 0x060008DE RID: 2270 RVA: 0x0001D0A0 File Offset: 0x0001B2A0
		internal static object ValidateSpellingCheckBeforeSendCallback(object value)
		{
			bool flag = UserConfigurationPropertyValidationUtility.SpellingCheckBeforeSendValues.IsCofigValueDefine && UserConfigurationPropertyValidationUtility.SpellingCheckBeforeSendValues.SpellingCheckBeforeSendConfigValue;
			return UserConfigurationPropertyValidationUtility.ValidateBoolValue(value, flag);
		}

		// Token: 0x060008DF RID: 2271 RVA: 0x0001D0C9 File Offset: 0x0001B2C9
		internal static object ValidateSmimeEncryptCallback(object value)
		{
			return UserConfigurationPropertyValidationUtility.ValidateBoolValue(value, false);
		}

		// Token: 0x060008E0 RID: 2272 RVA: 0x0001D0D7 File Offset: 0x0001B2D7
		internal static object ValidateSmimeSignCallback(object value)
		{
			return UserConfigurationPropertyValidationUtility.ValidateBoolValue(value, false);
		}

		// Token: 0x060008E1 RID: 2273 RVA: 0x0001D0E5 File Offset: 0x0001B2E5
		internal static object ValidateAlwaysShowBccCallback(object value)
		{
			return UserConfigurationPropertyValidationUtility.ValidateBoolValue(value, false);
		}

		// Token: 0x060008E2 RID: 2274 RVA: 0x0001D0F3 File Offset: 0x0001B2F3
		internal static object ValidateAlwaysShowFromCallback(object value)
		{
			return UserConfigurationPropertyValidationUtility.ValidateBoolValue(value, false);
		}

		// Token: 0x060008E3 RID: 2275 RVA: 0x0001D101 File Offset: 0x0001B301
		internal static object ValidateComposeMarkupCallback(object value)
		{
			return (Markup)UserConfigurationPropertyValidationUtility.ValidateIntRange(value, 0, 0, 1);
		}

		// Token: 0x060008E4 RID: 2276 RVA: 0x0001D11C File Offset: 0x0001B31C
		internal static object ValidateComposeFontNameCallback(object value)
		{
			int num = 100;
			string text = (string)value;
			if (!string.IsNullOrEmpty(text) && text.Length <= num)
			{
				ExTraceGlobals.UserOptionsDataTracer.TraceDebug<string>(0L, "Returning value: {0}", text);
				return value;
			}
			return null;
		}

		// Token: 0x060008E5 RID: 2277 RVA: 0x0001D159 File Offset: 0x0001B359
		internal static object ValidateComposeFontSizeCallback(object value)
		{
			return UserConfigurationPropertyValidationUtility.ValidateIntRange(value, 3, 1, 7);
		}

		// Token: 0x060008E6 RID: 2278 RVA: 0x0001D16C File Offset: 0x0001B36C
		internal static object ValidateComposeFontColorCallback(object value)
		{
			string text = (string)value;
			if (!string.IsNullOrEmpty(text) && UserConfigurationPropertyValidationUtility.validColorRegex.Match(text).Success)
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

		// Token: 0x060008E7 RID: 2279 RVA: 0x0001D1D4 File Offset: 0x0001B3D4
		internal static object ValidateComposeFontFlagsCallback(object value)
		{
			return (FontFlags)UserConfigurationPropertyValidationUtility.ValidateIntRange(value, 0, 0, 7);
		}

		// Token: 0x060008E8 RID: 2280 RVA: 0x0001D1EE File Offset: 0x0001B3EE
		internal static object ValidateAutoAddSignatureCallback(object value)
		{
			return UserConfigurationPropertyValidationUtility.ValidateBoolValue(value, false);
		}

		// Token: 0x060008E9 RID: 2281 RVA: 0x0001D1FC File Offset: 0x0001B3FC
		internal static object ValidateSignatureTextCallback(object value)
		{
			return UserConfigurationPropertyValidationUtility.ValidateSignatureTextCallbackCommon(value, UserConfigurationPropertyValidationUtility.SignatureValues.SignatureMaxLengthConfigValue, 16000, 4096);
		}

		// Token: 0x060008EA RID: 2282 RVA: 0x0001D213 File Offset: 0x0001B413
		internal static object ValidateAutoAddSignatureOnMobileCallback(object value)
		{
			return UserConfigurationPropertyValidationUtility.ValidateBoolValue(value, true);
		}

		// Token: 0x060008EB RID: 2283 RVA: 0x0001D221 File Offset: 0x0001B421
		internal static object ValidateSignatureTextOnMobileCallback(object value)
		{
			return UserConfigurationPropertyValidationUtility.ValidateSignatureTextCallbackCommon(value, UserConfigurationPropertyValidationUtility.MOWASignatureValues.SignatureMaxLengthConfigValue, 512, 512);
		}

		// Token: 0x060008EC RID: 2284 RVA: 0x0001D238 File Offset: 0x0001B438
		internal static object ValidateUseDesktopSignatureCallback(object value)
		{
			return UserConfigurationPropertyValidationUtility.ValidateBoolValue(value, true);
		}

		// Token: 0x060008ED RID: 2285 RVA: 0x0001D248 File Offset: 0x0001B448
		internal static object ValidateSignatureTextCallbackCommon(object value, int signatureMaxLengthConfigValue, int allowedSignatureMaxLength, int defaultSignatureMaxLength)
		{
			string text = (string)value;
			if (string.IsNullOrEmpty(text))
			{
				return value;
			}
			int num;
			if (UserConfigurationPropertyValidationUtility.SignatureValues.IsConfigValueDefined)
			{
				num = ((signatureMaxLengthConfigValue < allowedSignatureMaxLength) ? signatureMaxLengthConfigValue : allowedSignatureMaxLength);
			}
			else
			{
				num = defaultSignatureMaxLength;
			}
			if (text.Length <= num)
			{
				ExTraceGlobals.UserOptionsDataTracer.TraceDebug(0L, "Returning original value: {0}", new object[]
				{
					value
				});
				return value;
			}
			string text2 = text.Substring(0, num);
			ExTraceGlobals.UserOptionsDataTracer.TraceDebug<int, string>(0L, "Signature is longer that max length '{0}'. Returning truncated value: {1}", num, text2);
			return text2;
		}

		// Token: 0x060008EE RID: 2286 RVA: 0x0001D2BF File Offset: 0x0001B4BF
		internal static object ValidateSignatureHtmlCallback(object value)
		{
			return UserConfigurationPropertyValidationUtility.ValidateSignatureTextCallback(value);
		}

		// Token: 0x060008EF RID: 2287 RVA: 0x0001D2C7 File Offset: 0x0001B4C7
		internal static object ValidateBlockExternalContentCallback(object value)
		{
			return UserConfigurationPropertyValidationUtility.ValidateBoolValue(value, true);
		}

		// Token: 0x060008F0 RID: 2288 RVA: 0x0001D2D5 File Offset: 0x0001B4D5
		internal static object ValidatePreviewMarkAsReadCallback(object value)
		{
			return (MarkAsRead)UserConfigurationPropertyValidationUtility.ValidateIntRange(value, 1, 0, 2);
		}

		// Token: 0x060008F1 RID: 2289 RVA: 0x0001D2EF File Offset: 0x0001B4EF
		internal static object ValidateEmailComposeModeCallback(object value)
		{
			return (EmailComposeMode)UserConfigurationPropertyValidationUtility.ValidateIntRange(value, 0, 0, 1);
		}

		// Token: 0x060008F2 RID: 2290 RVA: 0x0001D30C File Offset: 0x0001B50C
		internal static object ValidateSendAsMruAddressesCallback(object value)
		{
			string[] array = value as string[];
			if (array != null && array.Length <= 10)
			{
				return array;
			}
			return null;
		}

		// Token: 0x060008F3 RID: 2291 RVA: 0x0001D32D File Offset: 0x0001B52D
		internal static object ValidateCheckForForgottenAttachmentsCallback(object value)
		{
			return UserConfigurationPropertyValidationUtility.ValidateBoolValue(value, true);
		}

		// Token: 0x060008F4 RID: 2292 RVA: 0x0001D33B File Offset: 0x0001B53B
		internal static object ValidateMarkAsReadDelaytimeCallback(object value)
		{
			return UserConfigurationPropertyValidationUtility.ValidateIntRange(value, 5, 0, 30);
		}

		// Token: 0x060008F5 RID: 2293 RVA: 0x0001D34C File Offset: 0x0001B54C
		internal static object ValidateReadReceiptCallback(object value)
		{
			return (ReadReceiptResponse)UserConfigurationPropertyValidationUtility.ValidateIntRange(value, 0, 0, 2);
		}

		// Token: 0x060008F6 RID: 2294 RVA: 0x0001D366 File Offset: 0x0001B566
		internal static object ValidateEmptyDeletedItemsOnLogoffCallback(object value)
		{
			return UserConfigurationPropertyValidationUtility.ValidateBoolValue(value, false);
		}

		// Token: 0x060008F7 RID: 2295 RVA: 0x0001D374 File Offset: 0x0001B574
		internal static object ValidateNavigationBarWidthCallback(object value)
		{
			return UserConfigurationPropertyValidationUtility.ValidateIntRange(value, 214, 50, 2000);
		}

		// Token: 0x060008F8 RID: 2296 RVA: 0x0001D38D File Offset: 0x0001B58D
		internal static object ValidateNavigationBarWidthRatioCallback(object value)
		{
			if (!(value is string))
			{
				return null;
			}
			return value;
		}

		// Token: 0x060008F9 RID: 2297 RVA: 0x0001D39A File Offset: 0x0001B59A
		internal static object ValidateMailFolderPaneExpandedCallback(object value)
		{
			return UserConfigurationPropertyValidationUtility.ValidateBoolValue(value, true);
		}

		// Token: 0x060008FA RID: 2298 RVA: 0x0001D3A8 File Offset: 0x0001B5A8
		internal static object ValidateIsFavoritesFolderTreeCollapsedCallback(object value)
		{
			return UserConfigurationPropertyValidationUtility.ValidateBoolValue(value, false);
		}

		// Token: 0x060008FB RID: 2299 RVA: 0x0001D3B6 File Offset: 0x0001B5B6
		internal static object ValidateIsPeopleIKnowFolderTreeCollapsedCallback(object value)
		{
			return UserConfigurationPropertyValidationUtility.ValidateBoolValue(value, false);
		}

		// Token: 0x060008FC RID: 2300 RVA: 0x0001D3C4 File Offset: 0x0001B5C4
		internal static object ValidateShowReadingPaneOnFirstLoadCallback(object value)
		{
			return UserConfigurationPropertyValidationUtility.ValidateBoolValue(value, false);
		}

		// Token: 0x060008FD RID: 2301 RVA: 0x0001D3D2 File Offset: 0x0001B5D2
		internal static object ValidateIsMailRootFolderTreeCollapsedCallback(object value)
		{
			return UserConfigurationPropertyValidationUtility.ValidateBoolValue(value, false);
		}

		// Token: 0x060008FE RID: 2302 RVA: 0x0001D3E0 File Offset: 0x0001B5E0
		internal static object ValidateMiniBarCallback(object value)
		{
			return UserConfigurationPropertyValidationUtility.ValidateBoolValue(value, false);
		}

		// Token: 0x060008FF RID: 2303 RVA: 0x0001D3EE File Offset: 0x0001B5EE
		internal static object ValidateQuickLinksCallback(object value)
		{
			return UserConfigurationPropertyValidationUtility.ValidateBoolValue(value, false);
		}

		// Token: 0x06000900 RID: 2304 RVA: 0x0001D3FC File Offset: 0x0001B5FC
		internal static object ValidateTaskDetailsCallback(object value)
		{
			return UserConfigurationPropertyValidationUtility.ValidateBoolValue(value, true);
		}

		// Token: 0x06000901 RID: 2305 RVA: 0x0001D40A File Offset: 0x0001B60A
		internal static object ValidateDocumentFavoritesCallback(object value)
		{
			return UserConfigurationPropertyValidationUtility.ValidateBoolValue(value, true);
		}

		// Token: 0x06000902 RID: 2306 RVA: 0x0001D418 File Offset: 0x0001B618
		internal static object ValidateOutlookSharedFoldersCallback(object value)
		{
			return UserConfigurationPropertyValidationUtility.ValidateBoolValue(value, true);
		}

		// Token: 0x06000903 RID: 2307 RVA: 0x0001D426 File Offset: 0x0001B626
		internal static object ValidateFormatBarStateCallback(object value)
		{
			return (FormatBarButtonGroups)UserConfigurationPropertyValidationUtility.ValidateIntRange(value, FormatBarButtonGroups.BoldItalicUnderline | FormatBarButtonGroups.Lists | FormatBarButtonGroups.ForegroundColor | FormatBarButtonGroups.BackgroundColor | FormatBarButtonGroups.Customize, 0, 16383);
		}

		// Token: 0x06000904 RID: 2308 RVA: 0x0001D448 File Offset: 0x0001B648
		internal static object ValidateMruFontsCallback(object value)
		{
			int num = 100;
			string[] array = (string[])value;
			if (array == null || array.Length > num)
			{
				ExTraceGlobals.UserOptionsDataTracer.TraceDebug<string[]>(0L, "Returning default value: {0}", UserConfigurationPropertyValidationUtility.DefaultMruFonts);
				return UserConfigurationPropertyValidationUtility.DefaultMruFonts;
			}
			return array;
		}

		// Token: 0x06000905 RID: 2309 RVA: 0x0001D485 File Offset: 0x0001B685
		internal static object ValidatePrimaryNavigationCollapsedCallback(object value)
		{
			return UserConfigurationPropertyValidationUtility.ValidateBoolValue(value, false);
		}

		// Token: 0x06000906 RID: 2310 RVA: 0x0001D494 File Offset: 0x0001B694
		internal static object ValidateThemeStorageIdCallback(object value)
		{
			string text = value as string;
			if (!string.IsNullOrEmpty(text))
			{
				return text;
			}
			return string.Empty;
		}

		// Token: 0x06000907 RID: 2311 RVA: 0x0001D4B7 File Offset: 0x0001B6B7
		internal static object ValidateFindBarOnCallback(object value)
		{
			return UserConfigurationPropertyValidationUtility.ValidateBoolValue(value, true);
		}

		// Token: 0x06000908 RID: 2312 RVA: 0x0001D4C5 File Offset: 0x0001B6C5
		internal static object ValidateSearchScopeCallback(object value)
		{
			return (SearchScope)UserConfigurationPropertyValidationUtility.ValidateIntRange(value, 3, 0, 3);
		}

		// Token: 0x06000909 RID: 2313 RVA: 0x0001D4DF File Offset: 0x0001B6DF
		internal static object ValidateContactsSearchScopeCallback(object value)
		{
			return (SearchScope)UserConfigurationPropertyValidationUtility.ValidateIntRange(value, 0, 0, 2);
		}

		// Token: 0x0600090A RID: 2314 RVA: 0x0001D4F9 File Offset: 0x0001B6F9
		internal static object ValidateTasksSearchScopeCallback(object value)
		{
			return (SearchScope)UserConfigurationPropertyValidationUtility.ValidateIntRange(value, 0, 0, 2);
		}

		// Token: 0x0600090B RID: 2315 RVA: 0x0001D513 File Offset: 0x0001B713
		internal static object ValidateIsOptimizedForAccessibilityCallback(object value)
		{
			return UserConfigurationPropertyValidationUtility.ValidateBoolValue(value, false);
		}

		// Token: 0x0600090C RID: 2316 RVA: 0x0001D521 File Offset: 0x0001B721
		internal static object ValidateEnabledPontsCallback(object value)
		{
			return (PontType)UserConfigurationPropertyValidationUtility.ValidateIntRange(value, int.MaxValue, 0, int.MaxValue);
		}

		// Token: 0x0600090D RID: 2317 RVA: 0x0001D543 File Offset: 0x0001B743
		internal static object ValidateFlagActionCallback(object value)
		{
			return (FlagAction)UserConfigurationPropertyValidationUtility.ValidateIntRange(value, 2, 2, 6);
		}

		// Token: 0x0600090E RID: 2318 RVA: 0x0001D55D File Offset: 0x0001B75D
		internal static object ValidateAddRecipientsToAutoCompleteCacheCallBack(object value)
		{
			return UserConfigurationPropertyValidationUtility.ValidateBoolValue(value, true);
		}

		// Token: 0x0600090F RID: 2319 RVA: 0x0001D56B File Offset: 0x0001B76B
		internal static object ValidateManuallyPickCertificateCallback(object value)
		{
			return UserConfigurationPropertyValidationUtility.ValidateBoolValue(value, false);
		}

		// Token: 0x06000910 RID: 2320 RVA: 0x0001D579 File Offset: 0x0001B779
		internal static object ValidateSigningCertificateSubjectCallback(object value)
		{
			return value;
		}

		// Token: 0x06000911 RID: 2321 RVA: 0x0001D57C File Offset: 0x0001B77C
		internal static object ValidateSigningCertificateIdCallback(object value)
		{
			return value;
		}

		// Token: 0x06000912 RID: 2322 RVA: 0x0001D580 File Offset: 0x0001B780
		internal static object ValidateUseDataCenterCustomThemeCallback(object value)
		{
			return UserConfigurationPropertyValidationUtility.ValidateIntCollection(value, -1, new int[]
			{
				0,
				1
			});
		}

		// Token: 0x06000913 RID: 2323 RVA: 0x0001D5A5 File Offset: 0x0001B7A5
		internal static object ValidateConversationSortOrderCallback(object value)
		{
			return UserConfigurationPropertyValidationUtility.ValidateIntCollection(value, 5, UserConfigurationPropertyValidationUtility.ValidConversationSortOrderValues);
		}

		// Token: 0x06000914 RID: 2324 RVA: 0x0001D5B8 File Offset: 0x0001B7B8
		internal static object ValidateShowTreeInListViewCallback(object value)
		{
			return UserConfigurationPropertyValidationUtility.ValidateBoolValue(value, false);
		}

		// Token: 0x06000915 RID: 2325 RVA: 0x0001D5C6 File Offset: 0x0001B7C6
		internal static object ValidateHideDeletedItemsCallback(object value)
		{
			return UserConfigurationPropertyValidationUtility.ValidateBoolValue(value, false);
		}

		// Token: 0x06000916 RID: 2326 RVA: 0x0001D5D4 File Offset: 0x0001B7D4
		internal static object ValidateHideMailTipsByDefaultCallback(object value)
		{
			return UserConfigurationPropertyValidationUtility.ValidateBoolValue(value, false);
		}

		// Token: 0x06000917 RID: 2327 RVA: 0x0001D5E2 File Offset: 0x0001B7E2
		internal static object ValidateSendAddressDefaultCallback(object value)
		{
			return value;
		}

		// Token: 0x06000918 RID: 2328 RVA: 0x0001D5E5 File Offset: 0x0001B7E5
		internal static object ValidateCalendarViewType(object value)
		{
			return (int)UserConfigurationPropertyValidationUtility.ValidateIntRange(value, CalendarViewType.Monthly, 1, 5);
		}

		// Token: 0x06000919 RID: 2329 RVA: 0x0001D5FF File Offset: 0x0001B7FF
		internal static object ValidateCalendarViewTypeNarrow(object value)
		{
			return (int)UserConfigurationPropertyValidationUtility.ValidateIntRange(value, CalendarViewType.Daily, 1, 5);
		}

		// Token: 0x0600091A RID: 2330 RVA: 0x0001D619 File Offset: 0x0001B819
		internal static object ValidateCalendarSidePanelMonthPickerCount(object value)
		{
			return (int)UserConfigurationPropertyValidationUtility.ValidateIntRange(value, 1, 1, 20);
		}

		// Token: 0x0600091B RID: 2331 RVA: 0x0001D634 File Offset: 0x0001B834
		internal static object ValidateUserOptionsMigrationState(object value)
		{
			return (int)UserConfigurationPropertyValidationUtility.ValidateIntRange(value, 0, 0, int.MaxValue);
		}

		// Token: 0x0600091C RID: 2332 RVA: 0x0001D652 File Offset: 0x0001B852
		internal static object ValidateCalendarSidePanelIsExpanded(object value)
		{
			return UserConfigurationPropertyValidationUtility.ValidateBoolValue(value, true);
		}

		// Token: 0x0600091D RID: 2333 RVA: 0x0001D660 File Offset: 0x0001B860
		internal static object ValidateShowInferenceUiElementsCallback(object value)
		{
			return UserConfigurationPropertyValidationUtility.ValidateBoolValue(value, true);
		}

		// Token: 0x0600091E RID: 2334 RVA: 0x0001D66E File Offset: 0x0001B86E
		internal static object ValidateIsClutterUIEnabledCallback(object value)
		{
			return UserConfigurationPropertyValidationUtility.ValidateBoolValue(value, UserConfigurationPropertyValidationUtility.DefaultIsClutterUIEnabled);
		}

		// Token: 0x0600091F RID: 2335 RVA: 0x0001D680 File Offset: 0x0001B880
		internal static object ValidateDefaultAttachmentsUploadFolderIdCallback(object value)
		{
			string text = (string)value;
			if (!string.IsNullOrEmpty(text) && text.Length <= 100)
			{
				return value;
			}
			return null;
		}

		// Token: 0x06000920 RID: 2336 RVA: 0x0001D6A9 File Offset: 0x0001B8A9
		internal static object ValidateHasShownClutterBarIntroductionMouseCallback(object value)
		{
			return UserConfigurationPropertyValidationUtility.ValidateBoolValue(value, false);
		}

		// Token: 0x06000921 RID: 2337 RVA: 0x0001D6B7 File Offset: 0x0001B8B7
		internal static object ValidateHasShownClutterDeleteAllIntroductionMouseCallback(object value)
		{
			return UserConfigurationPropertyValidationUtility.ValidateBoolValue(value, false);
		}

		// Token: 0x06000922 RID: 2338 RVA: 0x0001D6C5 File Offset: 0x0001B8C5
		internal static object ValidateHasShownClutterBarIntroductionTNarrowCallback(object value)
		{
			return UserConfigurationPropertyValidationUtility.ValidateBoolValue(value, false);
		}

		// Token: 0x06000923 RID: 2339 RVA: 0x0001D6D3 File Offset: 0x0001B8D3
		internal static object ValidateHasShownClutterDeleteAllIntroductionTNarrowCallback(object value)
		{
			return UserConfigurationPropertyValidationUtility.ValidateBoolValue(value, false);
		}

		// Token: 0x06000924 RID: 2340 RVA: 0x0001D6E1 File Offset: 0x0001B8E1
		internal static object ValidateHasShownClutterBarIntroductionTWideCallback(object value)
		{
			return UserConfigurationPropertyValidationUtility.ValidateBoolValue(value, false);
		}

		// Token: 0x06000925 RID: 2341 RVA: 0x0001D6EF File Offset: 0x0001B8EF
		internal static object ValidateHasShownClutterDeleteAllIntroductionTWideCallback(object value)
		{
			return UserConfigurationPropertyValidationUtility.ValidateBoolValue(value, false);
		}

		// Token: 0x06000926 RID: 2342 RVA: 0x0001D6FD File Offset: 0x0001B8FD
		internal static object ValidateIsInferenceSurveyCompleteCallback(object value)
		{
			return UserConfigurationPropertyValidationUtility.ValidateBoolValue(value, false);
		}

		// Token: 0x06000927 RID: 2343 RVA: 0x0001D70B File Offset: 0x0001B90B
		internal static object ValidateDontShowSurveysCallback(object value)
		{
			return UserConfigurationPropertyValidationUtility.ValidateBoolValue(value, false);
		}

		// Token: 0x06000928 RID: 2344 RVA: 0x0001D719 File Offset: 0x0001B919
		internal static object ValidateActiveSurveyCallback(object value)
		{
			return (int)UserConfigurationPropertyValidationUtility.ValidateIntRange(value, 0, 0, int.MaxValue);
		}

		// Token: 0x06000929 RID: 2345 RVA: 0x0001D737 File Offset: 0x0001B937
		internal static object ValidateCompletedSurveysCallback(object value)
		{
			return (int)UserConfigurationPropertyValidationUtility.ValidateIntRange(value, 0, 0, int.MaxValue);
		}

		// Token: 0x0600092A RID: 2346 RVA: 0x0001D755 File Offset: 0x0001B955
		internal static object ValidateDismissedSurveysCallback(object value)
		{
			return (int)UserConfigurationPropertyValidationUtility.ValidateIntRange(value, 0, 0, int.MaxValue);
		}

		// Token: 0x0600092B RID: 2347 RVA: 0x0001D773 File Offset: 0x0001B973
		internal static object ValidateLastSurveyDateCallback(object value)
		{
			return UserConfigurationPropertyValidationUtility.ValidateDateTimeString(value, UserConfigurationPropertyValidationUtility.DefaultLastSurveyDate);
		}

		// Token: 0x0600092C RID: 2348 RVA: 0x0001D780 File Offset: 0x0001B980
		internal static object ValidateInferenceSurveyDateCallback(object value)
		{
			return UserConfigurationPropertyValidationUtility.ValidateDateTimeString(value, UserConfigurationPropertyValidationUtility.DefaultSurveyDate);
		}

		// Token: 0x0600092D RID: 2349 RVA: 0x0001D78D File Offset: 0x0001B98D
		internal static object ValidatePeopleIKnowFirstUseDateCallback(object value)
		{
			return UserConfigurationPropertyValidationUtility.ValidateDateTimeString(value, UserConfigurationPropertyValidationUtility.DefaultSurveyDate);
		}

		// Token: 0x0600092E RID: 2350 RVA: 0x0001D79A File Offset: 0x0001B99A
		internal static object ValidatePeopleIKnowLastUseDateCallback(object value)
		{
			return UserConfigurationPropertyValidationUtility.ValidateDateTimeString(value, UserConfigurationPropertyValidationUtility.DefaultSurveyDate);
		}

		// Token: 0x0600092F RID: 2351 RVA: 0x0001D7A7 File Offset: 0x0001B9A7
		internal static object ValidatePeopleIKnowUseCallback(object value)
		{
			return (int)UserConfigurationPropertyValidationUtility.ValidateIntRange(value, 0, 0, int.MaxValue);
		}

		// Token: 0x06000930 RID: 2352 RVA: 0x0001D7C5 File Offset: 0x0001B9C5
		internal static object ValidateModernGroupsFirstUseDateCallback(object value)
		{
			return UserConfigurationPropertyValidationUtility.ValidateDateTimeString(value, UserConfigurationPropertyValidationUtility.DefaultSurveyDate);
		}

		// Token: 0x06000931 RID: 2353 RVA: 0x0001D7D2 File Offset: 0x0001B9D2
		internal static object ValidateModernGroupsLastUseDateCallback(object value)
		{
			return UserConfigurationPropertyValidationUtility.ValidateDateTimeString(value, UserConfigurationPropertyValidationUtility.DefaultSurveyDate);
		}

		// Token: 0x06000932 RID: 2354 RVA: 0x0001D7DF File Offset: 0x0001B9DF
		internal static object ValidateModernGroupsUseCountCallback(object value)
		{
			return (int)UserConfigurationPropertyValidationUtility.ValidateIntRange(value, 0, 0, int.MaxValue);
		}

		// Token: 0x06000933 RID: 2355 RVA: 0x0001D7FD File Offset: 0x0001B9FD
		internal static object ValidateShowSenderOnTopInListViewCallback(object value)
		{
			return UserConfigurationPropertyValidationUtility.ValidateBoolValue(value, true);
		}

		// Token: 0x06000934 RID: 2356 RVA: 0x0001D80B File Offset: 0x0001BA0B
		internal static object ValidateShowPreviewTextInListViewCallback(object value)
		{
			return UserConfigurationPropertyValidationUtility.ValidateBoolValue(value, true);
		}

		// Token: 0x06000935 RID: 2357 RVA: 0x0001D819 File Offset: 0x0001BA19
		internal static object ValidateGlobalReadingPanePosition(object value)
		{
			return (int)UserConfigurationPropertyValidationUtility.ValidateIntRange(value, MailReadingPanePosition.Right, 0, 2);
		}

		// Token: 0x06000936 RID: 2358 RVA: 0x0001D833 File Offset: 0x0001BA33
		internal static object ValidateSchedulingViewType(object value)
		{
			return (int)UserConfigurationPropertyValidationUtility.ValidateIntRange(value, CalendarViewType.Daily, 1, 2);
		}

		// Token: 0x06000937 RID: 2359 RVA: 0x0001D84D File Offset: 0x0001BA4D
		internal static object ValidatePeopleHubDisplayOptionType(object value)
		{
			return (int)UserConfigurationPropertyValidationUtility.ValidateIntRange(value, PeopleDisplayOptionsType.Uninitialized, 1, 2);
		}

		// Token: 0x06000938 RID: 2360 RVA: 0x0001D867 File Offset: 0x0001BA67
		internal static object ValidatePeopleHubSortOptionType(object value)
		{
			return (int)UserConfigurationPropertyValidationUtility.ValidateIntRange(value, PeopleSortOptionsType.Uninitialized, 1, 6);
		}

		// Token: 0x06000939 RID: 2361 RVA: 0x0001D881 File Offset: 0x0001BA81
		internal static object ValidateAttachmentsFilePickerViewType(object value)
		{
			return (int)UserConfigurationPropertyValidationUtility.ValidateIntRange(value, AttachmentsFilePickerViewType.None, 0, 2);
		}

		// Token: 0x0600093A RID: 2362 RVA: 0x0001D89B File Offset: 0x0001BA9B
		internal static object ValidateAttachmentsFilePickerHideBanner(object value)
		{
			return UserConfigurationPropertyValidationUtility.ValidateBoolValue(value, false);
		}

		// Token: 0x0600093B RID: 2363 RVA: 0x0001D8A9 File Offset: 0x0001BAA9
		internal static object ValidateCurrentWeatherLocationBookmarkIndex(object value)
		{
			return (int)UserConfigurationPropertyValidationUtility.ValidateIntRange(value, CurrentWeatherLocationBookmarkIndexOption.None, -1, 4);
		}

		// Token: 0x0600093C RID: 2364 RVA: 0x0001D8C3 File Offset: 0x0001BAC3
		internal static object ValidateTemperatureUnit(object value)
		{
			return (int)UserConfigurationPropertyValidationUtility.ValidateIntRange(value, TemperatureUnit.Default, 0, 2);
		}

		// Token: 0x0600093D RID: 2365 RVA: 0x0001D8DD File Offset: 0x0001BADD
		internal static object ValidateHasShownIntroductionForPeopleCentricTriageCallback(object value)
		{
			return UserConfigurationPropertyValidationUtility.ValidateBoolValue(value, false);
		}

		// Token: 0x0600093E RID: 2366 RVA: 0x0001D8EB File Offset: 0x0001BAEB
		internal static object ValidateHasShownIntroductionForModernGroupsCallback(object value)
		{
			return UserConfigurationPropertyValidationUtility.ValidateBoolValue(value, false);
		}

		// Token: 0x0600093F RID: 2367 RVA: 0x0001D8F9 File Offset: 0x0001BAF9
		internal static object ValidateLearnabilityTypesShownCallback(object value)
		{
			return (int)UserConfigurationPropertyValidationUtility.ValidateIntRange(value, 0, 0, int.MaxValue);
		}

		// Token: 0x06000940 RID: 2368 RVA: 0x0001D917 File Offset: 0x0001BB17
		internal static object ValidateNavigationPaneViewOptionCallback(object value)
		{
			return (int)UserConfigurationPropertyValidationUtility.ValidateIntRange(value, NavigationPaneView.Default, 0, 4);
		}

		// Token: 0x06000941 RID: 2369 RVA: 0x0001D931 File Offset: 0x0001BB31
		internal static object ValidateHasShownPeopleIKnowCallback(object value)
		{
			return UserConfigurationPropertyValidationUtility.ValidateBoolValue(value, false);
		}

		// Token: 0x06000942 RID: 2370 RVA: 0x0001D93F File Offset: 0x0001BB3F
		internal static object ValidateCalendarSearchUseCountCallback(object value)
		{
			return (int)UserConfigurationPropertyValidationUtility.ValidateIntRange(value, 0, 0, int.MaxValue);
		}

		// Token: 0x06000943 RID: 2371 RVA: 0x0001D960 File Offset: 0x0001BB60
		internal static object ValidateFrequentlyUsedFoldersCallback(object value)
		{
			string[] array = value as string[];
			if (array == null)
			{
				return null;
			}
			if (array.Length <= 10)
			{
				return array;
			}
			ExTraceGlobals.UserOptionsDataTracer.TraceDebug<int>(0L, "FrequentlyUsedFolders length exceends maximum size.  length: {0}", array.Length);
			string[] array2 = new string[10];
			for (int i = 0; i < 10; i++)
			{
				array2[i] = array[i];
			}
			return array2;
		}

		// Token: 0x06000944 RID: 2372 RVA: 0x0001D9B2 File Offset: 0x0001BBB2
		internal static object ValidateCalendarAgendaViewIsExpandedMouse(object value)
		{
			return UserConfigurationPropertyValidationUtility.ValidateBoolValue(value, true);
		}

		// Token: 0x06000945 RID: 2373 RVA: 0x0001D9C0 File Offset: 0x0001BBC0
		internal static object ValidateCalendarAgendaViewIsExpandedTWide(object value)
		{
			return UserConfigurationPropertyValidationUtility.ValidateBoolValue(value, true);
		}

		// Token: 0x06000946 RID: 2374 RVA: 0x0001D9D0 File Offset: 0x0001BBD0
		internal static object ValidateArchiveFolderIdCallback(object value)
		{
			string text = value as string;
			if (!string.IsNullOrEmpty(text))
			{
				try
				{
					if (Folder.IsFolderId(StoreId.EwsIdToFolderStoreObjectId(text)))
					{
						return value;
					}
				}
				catch (InvalidIdMalformedException)
				{
					ExTraceGlobals.UserOptionsTracer.TraceDebug(0L, "Invalid archive folder id: '{0}'", new object[]
					{
						value
					});
				}
			}
			return null;
		}

		// Token: 0x06000947 RID: 2375 RVA: 0x0001DA34 File Offset: 0x0001BC34
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

		// Token: 0x06000948 RID: 2376 RVA: 0x0001DADC File Offset: 0x0001BCDC
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

		// Token: 0x06000949 RID: 2377 RVA: 0x0001DB74 File Offset: 0x0001BD74
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

		// Token: 0x0600094A RID: 2378 RVA: 0x0001DBFC File Offset: 0x0001BDFC
		private static object ValidateDateTimeString(object value, string defaultDateTimeString)
		{
			string text = value as string;
			if (!string.IsNullOrEmpty(text))
			{
				ExDateTime exDateTime;
				if (ExDateTime.TryParse(text, out exDateTime))
				{
					ExTraceGlobals.UserOptionsDataTracer.TraceDebug(0L, "Returning original value: '{0}'", new object[]
					{
						value
					});
					return text;
				}
				ExTraceGlobals.UserOptionsTracer.TraceDebug(0L, "'{0}' is not a valid DateTime string.", new object[]
				{
					value
				});
			}
			ExTraceGlobals.UserOptionsDataTracer.TraceDebug<string>(0L, "Returning default value: '{0}'", defaultDateTimeString);
			return defaultDateTimeString;
		}

		// Token: 0x0600094B RID: 2379 RVA: 0x0001DC71 File Offset: 0x0001BE71
		internal static object ValidateReportJunkSelectedCallback(object value)
		{
			return UserConfigurationPropertyValidationUtility.ValidateBoolValue(value, false);
		}

		// Token: 0x0600094C RID: 2380 RVA: 0x0001DC7F File Offset: 0x0001BE7F
		internal static object ValidateCheckForReportJunkDialogCallback(object value)
		{
			return UserConfigurationPropertyValidationUtility.ValidateBoolValue(value, false);
		}

		// Token: 0x0600094D RID: 2381 RVA: 0x0001DC8D File Offset: 0x0001BE8D
		internal static object ValidateBuildGreenLightSurveyLastShownDateCallback(object value)
		{
			return UserConfigurationPropertyValidationUtility.ValidateDateTimeString(value, UserConfigurationPropertyValidationUtility.DefaultSurveyDate);
		}

		// Token: 0x040005F5 RID: 1525
		private const int MaxSendAsMruAddressCount = 10;

		// Token: 0x040005F6 RID: 1526
		private const int DefaultViewRowCount = 50;

		// Token: 0x040005F7 RID: 1527
		private const int DefaultBasicViewRowCount = 20;

		// Token: 0x040005F8 RID: 1528
		private const NextSelectionDirection DefaultNextSelection = NextSelectionDirection.Next;

		// Token: 0x040005F9 RID: 1529
		private const int MaxFrequentlyUsedFoldersCount = 10;

		// Token: 0x040005FA RID: 1530
		private const int DefaultHourIncrement = 30;

		// Token: 0x040005FB RID: 1531
		private const bool DefaultShowWeekNumbers = false;

		// Token: 0x040005FC RID: 1532
		private const bool DefaultCheckNameInContactsFirst = false;

		// Token: 0x040005FD RID: 1533
		private const bool DefaultEnableReminders = true;

		// Token: 0x040005FE RID: 1534
		private const bool DefaultEnableReminderSound = true;

		// Token: 0x040005FF RID: 1535
		private const NewNotification DefaultNewItemNotify = NewNotification.Sound | NewNotification.EMailToast | NewNotification.VoiceMailToast | NewNotification.FaxToast;

		// Token: 0x04000600 RID: 1536
		private const int DefaultMarkAsReadDelaytime = 5;

		// Token: 0x04000601 RID: 1537
		private const int DefaultNavBarWidth = 214;

		// Token: 0x04000602 RID: 1538
		private const bool DefaultBlockExternalContent = true;

		// Token: 0x04000603 RID: 1539
		private const FormatBarButtonGroups DefaultFormatBarState = FormatBarButtonGroups.BoldItalicUnderline | FormatBarButtonGroups.Lists | FormatBarButtonGroups.ForegroundColor | FormatBarButtonGroups.BackgroundColor | FormatBarButtonGroups.Customize;

		// Token: 0x04000604 RID: 1540
		private const SearchScope DefaultSearchScope = SearchScope.AllFoldersAndItems;

		// Token: 0x04000605 RID: 1541
		private const bool DefaultIsOptimizedForAccessibility = false;

		// Token: 0x04000606 RID: 1542
		private const bool DefaultManuallyPickCertificate = false;

		// Token: 0x04000607 RID: 1543
		private const bool DefaultShowTreeInListView = false;

		// Token: 0x04000608 RID: 1544
		private const bool DefaultHideDeletedItems = false;

		// Token: 0x04000609 RID: 1545
		private const bool DefaultHideMailTipsByDefault = false;

		// Token: 0x0400060A RID: 1546
		private const ConversationSortOrder DefaultConversationSortOrder = ConversationSortOrder.ChronologicalNewestOnTop;

		// Token: 0x0400060B RID: 1547
		private const int DefaultUseDataCenterCustomTheme = -1;

		// Token: 0x0400060C RID: 1548
		private const bool DefaultAddRecipientsToAutoCompleteCache = true;

		// Token: 0x0400060D RID: 1549
		private const FlagAction DefaultFlagAction = FlagAction.Today;

		// Token: 0x0400060E RID: 1550
		private const PontType DefaultEnabledPonts = PontType.All;

		// Token: 0x0400060F RID: 1551
		private const SearchScope DefaultTasksSearchScope = SearchScope.SelectedFolder;

		// Token: 0x04000610 RID: 1552
		private const SearchScope DefaultContactsSearchScope = SearchScope.SelectedFolder;

		// Token: 0x04000611 RID: 1553
		private const bool DefaultFindBarOn = true;

		// Token: 0x04000612 RID: 1554
		private const bool PrimaryNavigationCollapsed = false;

		// Token: 0x04000613 RID: 1555
		private const bool DefaultOutlookSharedFoldersVisible = true;

		// Token: 0x04000614 RID: 1556
		private const bool DefaultDocumentFavoritesVisible = true;

		// Token: 0x04000615 RID: 1557
		private const bool DefaultTaskDetailsVisible = true;

		// Token: 0x04000616 RID: 1558
		private const bool DefaultMiniBarVisible = false;

		// Token: 0x04000617 RID: 1559
		private const bool DefaultEmptyDeletedItemsOnLogoff = false;

		// Token: 0x04000618 RID: 1560
		private const ReadReceiptResponse DefaultReadReceipt = ReadReceiptResponse.DoNotAutomaticallySend;

		// Token: 0x04000619 RID: 1561
		private const bool DefaultQuickLinksVisible = false;

		// Token: 0x0400061A RID: 1562
		private const MarkAsRead DefaultPreviewMarkAsRead = MarkAsRead.OnSelectionChange;

		// Token: 0x0400061B RID: 1563
		private const EmailComposeMode DefaultEmailComposeMode = EmailComposeMode.Inline;

		// Token: 0x0400061C RID: 1564
		private const bool DefaultCheckForForgottenAttachments = true;

		// Token: 0x0400061D RID: 1565
		private const int DefaultComposeFontSize = 3;

		// Token: 0x0400061E RID: 1566
		private const int DefaultMonthPickerCount = 1;

		// Token: 0x0400061F RID: 1567
		private const int MinimumMonthPickerCount = 1;

		// Token: 0x04000620 RID: 1568
		private const int MaximumMonthPickerCount = 20;

		// Token: 0x04000621 RID: 1569
		private const string DefaultComposeFontColor = "#000000";

		// Token: 0x04000622 RID: 1570
		private const bool DefaultSpellingIgnoreMixedDigits = false;

		// Token: 0x04000623 RID: 1571
		private const bool DefaultSpellingIgnoreUppercase = false;

		// Token: 0x04000624 RID: 1572
		private const bool DefaultSpellingCheckBeforeSend = false;

		// Token: 0x04000625 RID: 1573
		private const bool DefaultSmimeEncrypt = false;

		// Token: 0x04000626 RID: 1574
		private const bool DefaultSmimeSign = false;

		// Token: 0x04000627 RID: 1575
		private const bool DefaultAlwaysShowBcc = false;

		// Token: 0x04000628 RID: 1576
		private const bool DefaultAlwaysShowFrom = false;

		// Token: 0x04000629 RID: 1577
		private const Markup DefaultComposeMarkup = Markup.Html;

		// Token: 0x0400062A RID: 1578
		private const FontFlags DefaultComposeFontFlags = FontFlags.Normal;

		// Token: 0x0400062B RID: 1579
		private const bool DefaultAutoAddSignature = false;

		// Token: 0x0400062C RID: 1580
		private const int DefaultSignatureMaxLength = 4096;

		// Token: 0x0400062D RID: 1581
		private const bool DefaultAutoAddSignatureOnMobile = true;

		// Token: 0x0400062E RID: 1582
		private const int DefaultSignatureOnMobileMaxLength = 512;

		// Token: 0x0400062F RID: 1583
		private const bool DefaultUseDesktopSignature = true;

		// Token: 0x04000630 RID: 1584
		private const bool DefaultShowInferenceUiElements = true;

		// Token: 0x04000631 RID: 1585
		private const bool DefaultHasShownClutterBarIntroductionMouse = false;

		// Token: 0x04000632 RID: 1586
		private const bool DefaultHasShownClutterDeleteAllIntroductionMouse = false;

		// Token: 0x04000633 RID: 1587
		private const bool DefaultHasShownClutterBarIntroductionTNarrow = false;

		// Token: 0x04000634 RID: 1588
		private const bool DefaultHasShownClutterDeleteAllIntroductionTNarrow = false;

		// Token: 0x04000635 RID: 1589
		private const bool DefaultHasShownClutterBarIntroductionTWide = false;

		// Token: 0x04000636 RID: 1590
		private const bool DefaultHasShownClutterDeleteAllIntroductionTWide = false;

		// Token: 0x04000637 RID: 1591
		private const bool DefaultIsInferenceSurveyComplete = false;

		// Token: 0x04000638 RID: 1592
		private const int DefaultActiveSurvey = 0;

		// Token: 0x04000639 RID: 1593
		private const int DefaultCompletedSurveys = 0;

		// Token: 0x0400063A RID: 1594
		private const int DefaultDismissedSurveys = 0;

		// Token: 0x0400063B RID: 1595
		private const bool DefaultDontShowSurveys = false;

		// Token: 0x0400063C RID: 1596
		private const bool DefaultShowSenderOnTopInListView = true;

		// Token: 0x0400063D RID: 1597
		private const bool DefaultShowPreviewTextInListView = true;

		// Token: 0x0400063E RID: 1598
		private const bool DefaultReportJunkSelected = false;

		// Token: 0x0400063F RID: 1599
		private const bool DefaultCheckForReportJunkDialog = false;

		// Token: 0x04000640 RID: 1600
		private const bool DefaultHasShownIntroductionForPeopleCentricTriage = false;

		// Token: 0x04000641 RID: 1601
		private const bool DefaultHasShownIntroductionForModernGroups = false;

		// Token: 0x04000642 RID: 1602
		private const bool DefaultHasShownPeopleIKnow = false;

		// Token: 0x04000643 RID: 1603
		private const bool DefaultAttachmentsFilePickerHideBanner = false;

		// Token: 0x04000644 RID: 1604
		private static readonly int[] ValidHourIncrementOptions = new int[]
		{
			15,
			30
		};

		// Token: 0x04000645 RID: 1605
		private static readonly string[] DefaultMruFonts = new string[0];

		// Token: 0x04000646 RID: 1606
		private static readonly string DefaultTimeZone = string.Empty;

		// Token: 0x04000647 RID: 1607
		private static readonly int[] ValidConversationSortOrderValues = new int[]
		{
			5,
			9
		};

		// Token: 0x04000648 RID: 1608
		private static readonly Regex validColorRegex = new Regex("^\\#[0-9a-fA-F]{6}$", RegexOptions.Compiled | RegexOptions.CultureInvariant);

		// Token: 0x04000649 RID: 1609
		private static readonly bool? DefaultIsClutterUIEnabled = null;

		// Token: 0x0400064A RID: 1610
		private static readonly string DefaultLastSurveyDate = null;

		// Token: 0x0400064B RID: 1611
		private static readonly string DefaultSurveyDate = null;

		// Token: 0x0400064C RID: 1612
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

		// Token: 0x020000FA RID: 250
		internal struct ViewRowCountValues
		{
			// Token: 0x0400064D RID: 1613
			internal const int DefaultMaxViewRowCount = 100;

			// Token: 0x0400064E RID: 1614
			internal const int AllowedMaxViewRowCount = 1000;

			// Token: 0x0400064F RID: 1615
			internal const int MinViewRowCount = 5;

			// Token: 0x04000650 RID: 1616
			internal const int ThreshholdViewRowCount = 50;

			// Token: 0x04000651 RID: 1617
			internal const int LowMultipleRowCount = 5;

			// Token: 0x04000652 RID: 1618
			internal const int HighMultipleRowCount = 25;

			// Token: 0x04000653 RID: 1619
			internal static int MaxViewRowCountConfigValue = 1000;

			// Token: 0x04000654 RID: 1620
			internal static bool IsConfigValueDefined = false;
		}

		// Token: 0x020000FB RID: 251
		internal struct SignatureValues
		{
			// Token: 0x04000655 RID: 1621
			internal const int AllowedSignatureMaxLength = 16000;

			// Token: 0x04000656 RID: 1622
			internal static int SignatureMaxLengthConfigValue = 16000;

			// Token: 0x04000657 RID: 1623
			internal static bool IsConfigValueDefined = false;
		}

		// Token: 0x020000FC RID: 252
		internal struct MOWASignatureValues
		{
			// Token: 0x04000658 RID: 1624
			internal const int AllowedSignatureMaxLength = 512;

			// Token: 0x04000659 RID: 1625
			internal static int SignatureMaxLengthConfigValue = 512;

			// Token: 0x0400065A RID: 1626
			internal static bool IsConfigValueDefined = false;
		}

		// Token: 0x020000FD RID: 253
		internal struct SpellingCheckBeforeSendValues
		{
			// Token: 0x0400065B RID: 1627
			internal static bool IsCofigValueDefine;

			// Token: 0x0400065C RID: 1628
			internal static bool SpellingCheckBeforeSendConfigValue;
		}
	}
}
