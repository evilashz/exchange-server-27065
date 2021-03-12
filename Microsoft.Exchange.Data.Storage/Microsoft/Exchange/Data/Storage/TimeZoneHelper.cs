using System;
using System.Collections;
using System.Collections.Generic;
using Microsoft.Exchange.Data.Storage.Management;
using Microsoft.Exchange.Data.Storage.Principal;
using Microsoft.Exchange.Data.Storage.StoreConfigurableType;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Data.Storage;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.Win32;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x020002A3 RID: 675
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal static class TimeZoneHelper
	{
		// Token: 0x06001C0F RID: 7183 RVA: 0x00081215 File Offset: 0x0007F415
		public static REG_TIMEZONE_INFO RegTimeZoneInfoFromExTimeZone(ExTimeZone timeZone)
		{
			return TimeZoneHelper.RegTimeZoneInfoFromExTimeZone(timeZone, ExDateTime.Now);
		}

		// Token: 0x06001C10 RID: 7184 RVA: 0x00081224 File Offset: 0x0007F424
		public static REG_TIMEZONE_INFO RegTimeZoneInfoFromExTimeZone(ExTimeZone timeZone, ExDateTime effectiveTime)
		{
			DateTime t = (DateTime)effectiveTime.ToUtc();
			ExTimeZoneRuleGroup exTimeZoneRuleGroup = null;
			foreach (ExTimeZoneRuleGroup exTimeZoneRuleGroup2 in timeZone.TimeZoneInformation.Groups)
			{
				if (exTimeZoneRuleGroup == null)
				{
					exTimeZoneRuleGroup = exTimeZoneRuleGroup2;
				}
				if (exTimeZoneRuleGroup2.EffectiveUtcStart <= t && exTimeZoneRuleGroup2.EffectiveUtcEnd > t)
				{
					exTimeZoneRuleGroup = exTimeZoneRuleGroup2;
					break;
				}
			}
			if (exTimeZoneRuleGroup.Rules.Count > 2)
			{
				throw new NotImplementedException();
			}
			return TimeZoneHelper.RegTimeZoneInfoFromExTimeZoneRuleGroup(exTimeZoneRuleGroup);
		}

		// Token: 0x06001C11 RID: 7185 RVA: 0x000812C0 File Offset: 0x0007F4C0
		internal static REG_TIMEZONE_INFO RegTimeZoneInfoFromExTimeZoneRuleGroup(ExTimeZoneRuleGroup group)
		{
			REG_TIMEZONE_INFO result = default(REG_TIMEZONE_INFO);
			ExTimeZoneRule exTimeZoneRule = group.Rules[0];
			ExTimeZoneRule exTimeZoneRule2 = (group.Rules.Count > 1) ? group.Rules[1] : null;
			if (exTimeZoneRule2 != null && exTimeZoneRule.Bias > exTimeZoneRule2.Bias)
			{
				ExTimeZoneRule exTimeZoneRule3 = exTimeZoneRule;
				exTimeZoneRule = exTimeZoneRule2;
				exTimeZoneRule2 = exTimeZoneRule3;
			}
			result.Bias = (int)(-(int)exTimeZoneRule.Bias.TotalMinutes);
			result.StandardBias = 0;
			if (exTimeZoneRule2 != null)
			{
				result.DaylightBias = (int)(exTimeZoneRule.Bias.TotalMinutes - exTimeZoneRule2.Bias.TotalMinutes);
				result.StandardDate = TimeZoneHelper.Win32SystemTimeFromRecurringTime(exTimeZoneRule2.ObservanceEnd);
				result.DaylightDate = TimeZoneHelper.Win32SystemTimeFromRecurringTime(exTimeZoneRule.ObservanceEnd);
			}
			return result;
		}

		// Token: 0x06001C12 RID: 7186 RVA: 0x0008138C File Offset: 0x0007F58C
		private static NativeMethods.SystemTime Win32SystemTimeFromRecurringTime(ExYearlyRecurringTime recurring)
		{
			NativeMethods.SystemTime result = default(NativeMethods.SystemTime);
			ExYearlyRecurringDate exYearlyRecurringDate = recurring as ExYearlyRecurringDate;
			ExYearlyRecurringDay exYearlyRecurringDay = recurring as ExYearlyRecurringDay;
			if (exYearlyRecurringDate != null)
			{
				result.Year = (ushort)ExDateTime.Now.Year;
				result.Month = (ushort)exYearlyRecurringDate.Month;
				result.Day = (ushort)exYearlyRecurringDate.Day;
				result.Hour = (ushort)exYearlyRecurringDate.Hour;
				result.Minute = (ushort)exYearlyRecurringDate.Minute;
				result.Second = (ushort)exYearlyRecurringDate.Second;
				result.Milliseconds = (ushort)exYearlyRecurringDate.Milliseconds;
			}
			else
			{
				if (exYearlyRecurringDay == null)
				{
					throw new InvalidOperationException();
				}
				result.Year = 0;
				result.Month = (ushort)exYearlyRecurringDay.Month;
				result.Day = (ushort)((exYearlyRecurringDay.Occurrence == -1) ? 5 : exYearlyRecurringDay.Occurrence);
				result.DayOfWeek = (ushort)exYearlyRecurringDay.DayOfWeek;
				result.Hour = (ushort)exYearlyRecurringDay.Hour;
				result.Minute = (ushort)exYearlyRecurringDay.Minute;
				result.Second = (ushort)exYearlyRecurringDay.Second;
				result.Milliseconds = (ushort)exYearlyRecurringDay.Milliseconds;
			}
			return result;
		}

		// Token: 0x06001C13 RID: 7187 RVA: 0x000814A4 File Offset: 0x0007F6A4
		public static ExTimeZone GetExTimeZoneFromItem(Item item)
		{
			ExTimeZone exTimeZone = TimeZoneHelper.GetRecurringTimeZoneFromPropertyBag(item.PropertyBag);
			if (exTimeZone == null)
			{
				exTimeZone = TimeZoneHelper.GetTimeZoneFromProperties("Customized Time Zone", null, item.GetValueOrDefault<byte[]>(InternalSchema.TimeZoneDefinitionStart));
			}
			if (exTimeZone == null)
			{
				if (item.Session != null && item.Session.ExTimeZone != ExTimeZone.UtcTimeZone)
				{
					exTimeZone = item.Session.ExTimeZone;
				}
				else
				{
					ExTimeZoneHelperForMigrationOnly.CheckValidationLevel(false, ExTimeZoneHelperForMigrationOnly.ValidationLevel.Low, "TimeZoneHelper.GetTimeZoneFromItem: no time zone", new object[0]);
					exTimeZone = ExTimeZone.CurrentTimeZone;
				}
			}
			return exTimeZone;
		}

		// Token: 0x06001C14 RID: 7188 RVA: 0x0008151C File Offset: 0x0007F71C
		public static ExTimeZone GetPromotedTimeZoneFromItem(Item item)
		{
			ExTimeZone exTimeZoneFromItem = TimeZoneHelper.GetExTimeZoneFromItem(item);
			ExTimeZone exTimeZone = null;
			if (exTimeZoneFromItem != null)
			{
				exTimeZone = TimeZoneHelper.PromoteCustomizedTimeZone(exTimeZoneFromItem);
			}
			return exTimeZone ?? exTimeZoneFromItem;
		}

		// Token: 0x06001C15 RID: 7189 RVA: 0x00081544 File Offset: 0x0007F744
		public static ExDateTime NormalizeUtcTime(ExDateTime utcTime, ExTimeZone legacyTimeZone)
		{
			if (legacyTimeZone == null || !legacyTimeZone.IsCustomTimeZone)
			{
				return utcTime.ToUtc();
			}
			DateTime localTime = legacyTimeZone.ConvertDateTime(utcTime).LocalTime;
			ExTimeZone exTimeZone = TimeZoneHelper.PromoteCustomizedTimeZone(legacyTimeZone);
			if (exTimeZone != null)
			{
				return new ExDateTime(exTimeZone, localTime);
			}
			return utcTime.ToUtc();
		}

		// Token: 0x06001C16 RID: 7190 RVA: 0x00081590 File Offset: 0x0007F790
		public static ExDateTime DeNormalizeToUtcTime(ExDateTime time, ExTimeZone legacyTimeZone)
		{
			if (legacyTimeZone == null || !legacyTimeZone.IsCustomTimeZone)
			{
				return time.ToUtc();
			}
			ExTimeZone exTimeZone = TimeZoneHelper.PromoteCustomizedTimeZone(legacyTimeZone);
			if (exTimeZone != null)
			{
				DateTime localTime = exTimeZone.ConvertDateTime(time).LocalTime;
				return new ExDateTime(legacyTimeZone, localTime).ToUtc();
			}
			return time;
		}

		// Token: 0x06001C17 RID: 7191 RVA: 0x000815DC File Offset: 0x0007F7DC
		public static ExTimeZone GetUserTimeZone(MailboxSession mailboxSession)
		{
			ExTimeZone result = null;
			UserConfiguration mailboxConfiguration = UserConfigurationHelper.GetMailboxConfiguration(mailboxSession, "OWA.UserOptions", UserConfigurationTypes.Dictionary, false);
			if (mailboxConfiguration == null)
			{
				ExTraceGlobals.StorageTracer.TraceDebug<IExchangePrincipal>(0L, "{0}: UserOption doesn't exist.", mailboxSession.MailboxOwner);
			}
			else
			{
				using (mailboxConfiguration)
				{
					IDictionary dictionary = null;
					try
					{
						dictionary = mailboxConfiguration.GetDictionary();
					}
					catch (CorruptDataException)
					{
						ExTraceGlobals.StorageTracer.TraceError<IExchangePrincipal>(0L, "{0}: Dictionary exists but is corrupt.", mailboxSession.MailboxOwner);
					}
					catch (InvalidOperationException)
					{
						ExTraceGlobals.StorageTracer.TraceError<IExchangePrincipal>(0L, "{0}: Dictionary is invalid.", mailboxSession.MailboxOwner);
					}
					if (dictionary != null)
					{
						string text = dictionary[MailboxRegionalConfigurationSchema.TimeZone.Name] as string;
						ExTraceGlobals.StorageTracer.TraceDebug<IExchangePrincipal, string>(0L, "{0}: Get timezone from dictionary of configuration. KeyName = {1}", mailboxSession.MailboxOwner, text);
						if (string.IsNullOrEmpty(text) || !ExTimeZoneEnumerator.Instance.TryGetTimeZoneByName(text, out result))
						{
							ExTraceGlobals.StorageTracer.TraceError<IExchangePrincipal, string>(0L, "{0}: The KeyName of TimeZone is invalid. KeyName = {1}", mailboxSession.MailboxOwner, text);
						}
					}
				}
			}
			return result;
		}

		// Token: 0x06001C18 RID: 7192 RVA: 0x000816F4 File Offset: 0x0007F8F4
		internal static ExDateTime AssignLocalTimeToUtc(ExDateTime timeToReserve)
		{
			DateTime dateTime = DateTime.SpecifyKind(timeToReserve.LocalTime, DateTimeKind.Unspecified);
			return new ExDateTime(ExTimeZone.UtcTimeZone, dateTime);
		}

		// Token: 0x06001C19 RID: 7193 RVA: 0x0008171C File Offset: 0x0007F91C
		internal static ExDateTime AssignUtcTimeToLocal(ExDateTime timeToRestore, ExTimeZone localTimeZone)
		{
			DateTime dateTime = DateTime.SpecifyKind(timeToRestore.UniversalTime, DateTimeKind.Unspecified);
			return new ExDateTime(localTimeZone, dateTime);
		}

		// Token: 0x06001C1A RID: 7194 RVA: 0x0008173E File Offset: 0x0007F93E
		public static ExTimeZone GetRecurringTimeZoneFromPropertyBag(PropertyBag propertyBag)
		{
			return TimeZoneHelper.GetRecurringTimeZoneFromPropertyBag(propertyBag.AsIStorePropertyBag());
		}

		// Token: 0x06001C1B RID: 7195 RVA: 0x0008174C File Offset: 0x0007F94C
		public static ExTimeZone GetRecurringTimeZoneFromPropertyBag(IStorePropertyBag propertyBag)
		{
			string valueOrDefault = propertyBag.GetValueOrDefault<string>(InternalSchema.TimeZone, null);
			byte[] valueOrDefault2 = propertyBag.GetValueOrDefault<byte[]>(InternalSchema.TimeZoneBlob, null);
			byte[] valueOrDefault3 = propertyBag.GetValueOrDefault<byte[]>(InternalSchema.TimeZoneDefinitionRecurring, null);
			return TimeZoneHelper.GetTimeZoneFromProperties(valueOrDefault, valueOrDefault2, valueOrDefault3);
		}

		// Token: 0x06001C1C RID: 7196 RVA: 0x00081788 File Offset: 0x0007F988
		internal static ExTimeZone GetTimeZoneFromProperties(string timeZoneDisplayName, byte[] o11TimeZoneBlob, byte[] o12TimeZoneBlob)
		{
			string text = timeZoneDisplayName ?? string.Empty;
			ExTimeZone result;
			if (O12TimeZoneFormatter.TryParseTimeZoneBlob(o12TimeZoneBlob, text, out result))
			{
				return result;
			}
			ExTimeZone result2;
			if (O11TimeZoneFormatter.TryParseTimeZoneBlob(o11TimeZoneBlob, text, out result2))
			{
				return result2;
			}
			return null;
		}

		// Token: 0x06001C1D RID: 7197 RVA: 0x000817BC File Offset: 0x0007F9BC
		internal static ExTimeZone PromoteCustomizedTimeZone(ExTimeZone customizedTimeZone)
		{
			ExTimeZone exTimeZone = null;
			if (customizedTimeZone.IsCustomTimeZone && !ExTimeZoneEnumerator.Instance.TryGetTimeZoneByName(customizedTimeZone.AlternativeId, out exTimeZone))
			{
				string displayName = customizedTimeZone.LocalizableDisplayName.ToString();
				List<ExTimeZone> list = TimeZoneHelper.MatchCustomTimeZoneByEffectiveRule(customizedTimeZone);
				exTimeZone = TimeZoneHelper.GetUniqueTimeZoneMatch(list, displayName);
				if (exTimeZone == null && list.Count > 1)
				{
					List<ExTimeZone> timeZones = TimeZoneHelper.MatchCustomTimeZoneByRules(customizedTimeZone, list);
					exTimeZone = TimeZoneHelper.GetUniqueTimeZoneMatch(timeZones, displayName);
				}
			}
			return exTimeZone;
		}

		// Token: 0x06001C1E RID: 7198 RVA: 0x0008182C File Offset: 0x0007FA2C
		private static List<ExTimeZone> MatchCustomTimeZoneByEffectiveRule(ExTimeZone customTimeZone)
		{
			List<ExTimeZone> list = new List<ExTimeZone>();
			REG_TIMEZONE_INFO v = TimeZoneHelper.RegTimeZoneInfoFromExTimeZone(customTimeZone);
			foreach (ExTimeZone exTimeZone in ExTimeZoneEnumerator.Instance)
			{
				if (v == TimeZoneHelper.RegTimeZoneInfoFromExTimeZone(exTimeZone))
				{
					list.Add(exTimeZone);
				}
			}
			return list;
		}

		// Token: 0x06001C1F RID: 7199 RVA: 0x00081894 File Offset: 0x0007FA94
		private static List<ExTimeZone> MatchCustomTimeZoneByRules(ExTimeZone customTimeZone, List<ExTimeZone> candidates)
		{
			List<ExTimeZone> list = new List<ExTimeZone>();
			byte[] timeZoneBlob = O12TimeZoneFormatter.GetTimeZoneBlob(customTimeZone);
			foreach (ExTimeZone exTimeZone in candidates)
			{
				byte[] timeZoneBlob2 = O12TimeZoneFormatter.GetTimeZoneBlob(exTimeZone);
				if (O12TimeZoneFormatter.CompareBlob(timeZoneBlob, timeZoneBlob2, false))
				{
					list.Add(exTimeZone);
				}
			}
			return list;
		}

		// Token: 0x06001C20 RID: 7200 RVA: 0x00081904 File Offset: 0x0007FB04
		private static ExTimeZone GetUniqueTimeZoneMatch(List<ExTimeZone> timeZones, string displayName)
		{
			ExTimeZone exTimeZone = null;
			if (timeZones.Count == 1)
			{
				exTimeZone = timeZones[0];
			}
			else if (timeZones.Count > 1 && !string.IsNullOrEmpty(displayName))
			{
				foreach (ExTimeZone exTimeZone2 in timeZones)
				{
					if (displayName.Equals(exTimeZone2.LocalizableDisplayName.ToString(), StringComparison.OrdinalIgnoreCase))
					{
						if (exTimeZone != null)
						{
							exTimeZone = null;
							break;
						}
						exTimeZone = exTimeZone2;
					}
				}
			}
			return exTimeZone;
		}

		// Token: 0x06001C21 RID: 7201 RVA: 0x0008199C File Offset: 0x0007FB9C
		public static ExTimeZone CreateExTimeZoneFromRegTimeZoneInfo(REG_TIMEZONE_INFO regInfo, string keyName)
		{
			ExTimeZone exTimeZone = TimeZoneHelper.CreateCustomExTimeZoneFromRegTimeZoneInfo(regInfo, keyName, keyName);
			return TimeZoneHelper.PromoteCustomizedTimeZone(exTimeZone) ?? exTimeZone;
		}

		// Token: 0x06001C22 RID: 7202 RVA: 0x000819C0 File Offset: 0x0007FBC0
		public static ExTimeZone CreateCustomExTimeZoneFromRegTimeZoneInfo(REG_TIMEZONE_INFO regInfo, string keyName, string displayName)
		{
			return TimeZoneHelper.CreateCustomExTimeZoneFromRegRules(regInfo, keyName, displayName, new List<RegistryTimeZoneRule>(1)
			{
				new RegistryTimeZoneRule(DateTime.MinValue.Year, regInfo)
			});
		}

		// Token: 0x06001C23 RID: 7203 RVA: 0x000819F8 File Offset: 0x0007FBF8
		internal static ExTimeZone CreateCustomExTimeZoneFromRegRules(REG_TIMEZONE_INFO regInfo, string keyName, string displayName, List<RegistryTimeZoneRule> regRules)
		{
			RegistryTimeZoneInformation registryTimeZoneInformation = new RegistryTimeZoneInformation(keyName, displayName, string.Empty, string.Empty, string.Empty, regInfo);
			foreach (RegistryTimeZoneRule item in regRules)
			{
				registryTimeZoneInformation.Rules.Add(item);
			}
			ExTimeZone timeZoneWithIdFromRegistryInfo = ExRegistryTimeZoneProvider.GetTimeZoneWithIdFromRegistryInfo("tzone://Microsoft/Custom", registryTimeZoneInformation);
			timeZoneWithIdFromRegistryInfo.TimeZoneInformation.AlternativeId = keyName;
			return timeZoneWithIdFromRegistryInfo;
		}

		// Token: 0x020002A4 RID: 676
		internal static class TestAccess
		{
			// Token: 0x06001C24 RID: 7204 RVA: 0x00081A7C File Offset: 0x0007FC7C
			internal static REG_TIMEZONE_INFO RegTimeZoneInfoFromExTimeZone(ExTimeZone timeZone)
			{
				return TimeZoneHelper.RegTimeZoneInfoFromExTimeZone(timeZone);
			}
		}
	}
}
