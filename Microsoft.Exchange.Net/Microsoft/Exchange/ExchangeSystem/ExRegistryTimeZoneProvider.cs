using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using Microsoft.Exchange.Core;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.ExchangeSystem
{
	// Token: 0x0200006E RID: 110
	public sealed class ExRegistryTimeZoneProvider : ExTimeZoneProviderBase
	{
		// Token: 0x170000B4 RID: 180
		// (get) Token: 0x060003CD RID: 973 RVA: 0x0000FFDC File Offset: 0x0000E1DC
		public static ExRegistryTimeZoneProvider Instance
		{
			get
			{
				if (ExRegistryTimeZoneProvider.instance == null)
				{
					lock (ExRegistryTimeZoneProvider.instanceLock)
					{
						if (ExRegistryTimeZoneProvider.instance == null)
						{
							ExRegistryTimeZoneProvider.instance = new ExRegistryTimeZoneProvider("WindowsRegistry");
						}
					}
				}
				return ExRegistryTimeZoneProvider.instance;
			}
		}

		// Token: 0x170000B5 RID: 181
		// (get) Token: 0x060003CE RID: 974 RVA: 0x00010038 File Offset: 0x0000E238
		public static ExRegistryTimeZoneProvider InstanceWithReload
		{
			get
			{
				lock (ExRegistryTimeZoneProvider.instanceLock)
				{
					ExRegistryTimeZoneProvider.instance = null;
					ExRegistryTimeZoneProvider.instance = new ExRegistryTimeZoneProvider("WindowsRegistry");
				}
				return ExRegistryTimeZoneProvider.instance;
			}
		}

		// Token: 0x060003CF RID: 975 RVA: 0x0001008C File Offset: 0x0000E28C
		public ExRegistryTimeZoneProvider(string id) : base(id)
		{
			IList<RegistryTimeZoneInformation> list = ExRegistryReader.ReadTimeZones();
			foreach (RegistryTimeZoneInformation tzInfo in list)
			{
				try
				{
					ExTimeZone timeZoneFromRegistryInfo = ExRegistryTimeZoneProvider.GetTimeZoneFromRegistryInfo(tzInfo);
					if (timeZoneFromRegistryInfo != null)
					{
						base.AddTimeZone(timeZoneFromRegistryInfo);
					}
				}
				catch (InvalidTimeZoneException)
				{
				}
			}
		}

		// Token: 0x060003D0 RID: 976 RVA: 0x000100FC File Offset: 0x0000E2FC
		internal static ExTimeZone GetTimeZoneFromRegistryInfo(RegistryTimeZoneInformation tzInfo)
		{
			string id = ExRegistryTimeZoneProvider.BuildRegistryTimeZoneIdFromName(tzInfo.KeyName);
			return ExRegistryTimeZoneProvider.GetTimeZoneWithIdFromRegistryInfo(id, tzInfo);
		}

		// Token: 0x060003D1 RID: 977 RVA: 0x0001011C File Offset: 0x0000E31C
		internal static ExTimeZone GetTimeZoneWithIdFromRegistryInfo(string id, RegistryTimeZoneInformation tzInfo)
		{
			ExTimeZone result;
			try
			{
				LocalizedString localizedDisplayName = (id == "tzone://Microsoft/Custom") ? new LocalizedString(tzInfo.DisplayName) : ExRegistryTimeZoneProvider.GetLocalizedDisplayName(id, tzInfo.DisplayName);
				ExTimeZoneInformation exTimeZoneInformation = new ExTimeZoneInformation(id, tzInfo.DisplayName, localizedDisplayName, tzInfo.MuiStandardName);
				for (int i = 0; i < tzInfo.Rules.Count; i++)
				{
					ExRegistryTimeZoneProvider.LoadTimeZoneRuleFromRegistryInfo(exTimeZoneInformation, tzInfo, i);
				}
				result = new ExTimeZone(exTimeZoneInformation);
			}
			catch (ArgumentOutOfRangeException ex)
			{
				throw new InvalidTimeZoneException(ex.Message, ex);
			}
			return result;
		}

		// Token: 0x060003D2 RID: 978 RVA: 0x000101AC File Offset: 0x0000E3AC
		internal static void LoadTimeZoneRuleFromRegistryInfo(ExTimeZoneInformation exTzInfo, RegistryTimeZoneInformation tzInfo, int index)
		{
			try
			{
				REG_TIMEZONE_INFO regTimeZoneInfo = tzInfo.Rules[index].RegTimeZoneInfo;
				TimeSpan bias = TimeSpan.FromMinutes((double)(-(double)regTimeZoneInfo.Bias - regTimeZoneInfo.StandardBias));
				TimeSpan bias2 = TimeSpan.FromMinutes((double)(-(double)regTimeZoneInfo.Bias - regTimeZoneInfo.DaylightBias));
				string ruleId = tzInfo.Rules[index].Start.Year.ToString();
				ExTimeZoneRuleGroup exTimeZoneRuleGroup;
				if (index < tzInfo.Rules.Count - 1)
				{
					int year = tzInfo.Rules[index + 1].Start.Year;
					exTimeZoneRuleGroup = new ExTimeZoneRuleGroup(new DateTime?(DateTime.SpecifyKind(new DateTime(year, 1, 1), DateTimeKind.Unspecified)));
				}
				else
				{
					exTimeZoneRuleGroup = new ExTimeZoneRuleGroup(null);
				}
				exTzInfo.AddGroup(exTimeZoneRuleGroup);
				if (regTimeZoneInfo.StandardDate.Month == 0 != (regTimeZoneInfo.DaylightDate.Month == 0))
				{
					throw new InvalidTimeZoneException("Incompatible DST transitions");
				}
				if (regTimeZoneInfo.StandardDate.Month == 0)
				{
					ExTimeZoneRule ruleInfo = new ExTimeZoneRule(ExRegistryTimeZoneProvider.BuildRegistryTimeZoneRuleIdFromName(tzInfo.KeyName, ruleId, "Standard"), "Standard", bias, null);
					exTimeZoneRuleGroup.AddRule(ruleInfo);
				}
				else
				{
					ExYearlyRecurringTime observanceEnd;
					ExYearlyRecurringTime observanceEnd2;
					if (regTimeZoneInfo.StandardDate.Year != 0)
					{
						observanceEnd = new ExYearlyRecurringDate((int)regTimeZoneInfo.DaylightDate.Month, (int)regTimeZoneInfo.DaylightDate.Day, (int)regTimeZoneInfo.DaylightDate.Hour, (int)regTimeZoneInfo.DaylightDate.Minute, (int)regTimeZoneInfo.DaylightDate.Second, (int)regTimeZoneInfo.DaylightDate.Milliseconds);
						observanceEnd2 = new ExYearlyRecurringDate((int)regTimeZoneInfo.StandardDate.Month, (int)regTimeZoneInfo.StandardDate.Day, (int)regTimeZoneInfo.StandardDate.Hour, (int)regTimeZoneInfo.StandardDate.Minute, (int)regTimeZoneInfo.StandardDate.Second, (int)regTimeZoneInfo.StandardDate.Milliseconds);
					}
					else
					{
						int occurrence = (regTimeZoneInfo.DaylightDate.Day == 5) ? -1 : ((int)regTimeZoneInfo.DaylightDate.Day);
						int occurrence2 = (regTimeZoneInfo.StandardDate.Day == 5) ? -1 : ((int)regTimeZoneInfo.StandardDate.Day);
						observanceEnd = new ExYearlyRecurringDay(occurrence, (DayOfWeek)regTimeZoneInfo.DaylightDate.DayOfWeek, (int)regTimeZoneInfo.DaylightDate.Month, (int)regTimeZoneInfo.DaylightDate.Hour, (int)regTimeZoneInfo.DaylightDate.Minute, (int)regTimeZoneInfo.DaylightDate.Second, (int)regTimeZoneInfo.DaylightDate.Milliseconds);
						observanceEnd2 = new ExYearlyRecurringDay(occurrence2, (DayOfWeek)regTimeZoneInfo.StandardDate.DayOfWeek, (int)regTimeZoneInfo.StandardDate.Month, (int)regTimeZoneInfo.StandardDate.Hour, (int)regTimeZoneInfo.StandardDate.Minute, (int)regTimeZoneInfo.StandardDate.Second, (int)regTimeZoneInfo.StandardDate.Milliseconds);
					}
					ExTimeZoneRule ruleInfo2 = new ExTimeZoneRule(ExRegistryTimeZoneProvider.BuildRegistryTimeZoneRuleIdFromName(tzInfo.KeyName, ruleId, "Daylight"), "Daylight", bias2, observanceEnd2);
					exTimeZoneRuleGroup.AddRule(ruleInfo2);
					ExTimeZoneRule ruleInfo3 = new ExTimeZoneRule(ExRegistryTimeZoneProvider.BuildRegistryTimeZoneRuleIdFromName(tzInfo.KeyName, ruleId, "Standard"), "Standard", bias, observanceEnd);
					exTimeZoneRuleGroup.AddRule(ruleInfo3);
				}
			}
			catch (ArgumentOutOfRangeException ex)
			{
				throw new InvalidTimeZoneException(ex.Message, ex);
			}
		}

		// Token: 0x060003D3 RID: 979 RVA: 0x00010500 File Offset: 0x0000E700
		private static string BuildRegistryTimeZoneIdFromName(string name)
		{
			return name;
		}

		// Token: 0x060003D4 RID: 980 RVA: 0x00010504 File Offset: 0x0000E704
		private static string BuildRegistryTimeZoneRuleIdFromName(string name, string ruleId, string relativeName)
		{
			StringBuilder stringBuilder = new StringBuilder(name.Length + ruleId.Length + relativeName.Length + 30);
			stringBuilder.Append("trule:Microsoft/Registry/");
			stringBuilder.Append(name);
			stringBuilder.Append("/");
			stringBuilder.Append(ruleId);
			stringBuilder.Append("-");
			stringBuilder.Append(relativeName);
			return stringBuilder.ToString();
		}

		// Token: 0x060003D5 RID: 981 RVA: 0x00010570 File Offset: 0x0000E770
		private static LocalizedString GetLocalizedDisplayName(string id, string unlocalizedDisplayName)
		{
			string id2 = "TimeZone" + Regex.Replace(id, "[\\.\\(\\)\\s-+]", string.Empty);
			LocalizedString result = new LocalizedString(id2, ExRegistryTimeZoneProvider.ResourceManager, new object[0]);
			if (string.IsNullOrEmpty(result.ToString()))
			{
				result = new LocalizedString(unlocalizedDisplayName);
			}
			return result;
		}

		// Token: 0x040001E0 RID: 480
		private const string TimeZoneIdEscapePattern = "[\\.\\(\\)\\s-+]";

		// Token: 0x040001E1 RID: 481
		private const string TimeZoneLocalizedStringIdPrefix = "TimeZone";

		// Token: 0x040001E2 RID: 482
		private static readonly ExchangeResourceManager ResourceManager = new ExchangeResourceManager.Concurrent(ExchangeResourceManager.GetResourceManager("Microsoft.Exchange.Core.CoreStrings", typeof(CoreStrings).GetTypeInfo().Assembly));

		// Token: 0x040001E3 RID: 483
		private static ExRegistryTimeZoneProvider instance;

		// Token: 0x040001E4 RID: 484
		private static object instanceLock = new object();
	}
}
