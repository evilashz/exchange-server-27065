using System;
using System.Collections.Specialized;
using System.Configuration;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Monitoring;

namespace Microsoft.Exchange.Monitoring
{
	// Token: 0x020001EA RID: 490
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal static class MonitoringHelper
	{
		// Token: 0x1700034F RID: 847
		// (get) Token: 0x0600118B RID: 4491 RVA: 0x00035B51 File Offset: 0x00033D51
		// (set) Token: 0x0600118C RID: 4492 RVA: 0x00035B58 File Offset: 0x00033D58
		internal static bool RunningInMonitoringService
		{
			get
			{
				return MonitoringHelper.runningInMonitoringService;
			}
			set
			{
				MonitoringHelper.runningInMonitoringService = value;
			}
		}

		// Token: 0x0600118D RID: 4493 RVA: 0x00035B60 File Offset: 0x00033D60
		static MonitoringHelper()
		{
			try
			{
				MonitoringHelper.appSettingsCollection = ConfigurationManager.AppSettings;
			}
			catch (ConfigurationErrorsException arg)
			{
				ExTraceGlobals.MonitoringHelperTracer.TraceError<ConfigurationErrorsException>(0L, "Error trying to get the appSettings secion of the configuration file. Error: {0}.", arg);
				MonitoringHelper.appSettingsCollection = new NameValueCollection();
			}
			MonitoringHelper.reportFilteredExceptions = MonitoringHelper.ReadBoolAppSettingKey("ReportFilteredExceptions", false);
		}

		// Token: 0x0600118E RID: 4494 RVA: 0x00035BB8 File Offset: 0x00033DB8
		internal static int ReadIntAppSettingKey(string key, int min, int max, int defaultValue)
		{
			int num = defaultValue;
			string text = MonitoringHelper.appSettingsCollection[key];
			if (string.IsNullOrEmpty(text))
			{
				ExTraceGlobals.MonitoringHelperTracer.TraceDebug<string>(0L, "The appSetting value for key '{0}' is null or empty, using the default value.", key);
			}
			else if (!int.TryParse(text, out num))
			{
				ExTraceGlobals.MonitoringHelperTracer.TraceDebug<string>(0L, "The appSetting value for key '{0}', '{1}', cannot be converted to int; using the default value.", key);
				num = defaultValue;
			}
			if (num < min || num > max)
			{
				ExTraceGlobals.MonitoringHelperTracer.TraceDebug<string, int, int>(0L, "The appSetting value for key '{0}' is out of the valid range [{1}, {2}]", key, min, max);
				num = defaultValue;
			}
			ExTraceGlobals.MonitoringHelperTracer.TraceDebug<int, string>(0L, "Using {0} as the value for the appSetting key '{1}'.", num, key);
			return num;
		}

		// Token: 0x0600118F RID: 4495 RVA: 0x00035C40 File Offset: 0x00033E40
		internal static bool ReadBoolAppSettingKey(string key, bool defaultValue)
		{
			bool flag = defaultValue;
			string value = MonitoringHelper.appSettingsCollection[key];
			if (string.IsNullOrEmpty(value))
			{
				ExTraceGlobals.MonitoringHelperTracer.TraceDebug<string>(0L, "The appSetting value for key '{0}' is null or empty, using the default value.", key);
			}
			else if (!bool.TryParse(value, out flag))
			{
				ExTraceGlobals.MonitoringHelperTracer.TraceDebug<string>(0L, "The appSetting value for key '{0}', '{1}', cannot be converted to bool; using the default value.", key);
				flag = defaultValue;
			}
			ExTraceGlobals.MonitoringHelperTracer.TraceDebug<bool, string>(0L, "Using {0} as the value for the appSetting key '{1}'.", flag, key);
			return flag;
		}

		// Token: 0x06001190 RID: 4496 RVA: 0x00035CAC File Offset: 0x00033EAC
		internal static string ReadStringAppSettingKey(string key, string defaultValue)
		{
			string text = defaultValue;
			string text2 = MonitoringHelper.appSettingsCollection[key];
			if (string.IsNullOrEmpty(text2) || string.IsNullOrEmpty(text2.Trim()))
			{
				ExTraceGlobals.MonitoringHelperTracer.TraceDebug<string>(0L, "The appSetting value for key '{0}' is null or empty, using the default value.", key);
			}
			else
			{
				text = text2.Trim();
			}
			ExTraceGlobals.MonitoringHelperTracer.TraceDebug<string, string>(0L, "Using {0} as the value for the appSetting key '{1}'.", text, key);
			return text;
		}

		// Token: 0x06001191 RID: 4497 RVA: 0x00035D0C File Offset: 0x00033F0C
		internal static bool IsKnownExceptionForMonitoring(Exception e)
		{
			if (e == null)
			{
				throw new ArgumentNullException("e");
			}
			ExTraceGlobals.MonitoringHelperTracer.TraceDebug<Exception, bool, bool>(0L, "IsKnownExceptionForMonitoring was called to check if exception {0} is known in the current context (runningInMonitoringService = {1}, reportFilteredExceptions = {2})", e, MonitoringHelper.runningInMonitoringService, MonitoringHelper.reportFilteredExceptions);
			bool flag = MonitoringHelper.runningInMonitoringService && !MonitoringHelper.reportFilteredExceptions && !(e is AccessViolationException) && !(e is DataMisalignedException) && !(e is TypeLoadException) && !(e is TypeInitializationException) && !(e is EntryPointNotFoundException) && !(e is InsufficientMemoryException) && !(e is OutOfMemoryException) && !(e is BadImageFormatException) && !(e is StackOverflowException) && !(e is InvalidProgramException);
			ExTraceGlobals.MonitoringHelperTracer.TraceDebug<bool>(0L, "IsKnownExceptionForMonitoring result = {0}", flag);
			return flag;
		}

		// Token: 0x040003FA RID: 1018
		private static NameValueCollection appSettingsCollection;

		// Token: 0x040003FB RID: 1019
		private static bool reportFilteredExceptions;

		// Token: 0x040003FC RID: 1020
		private static bool runningInMonitoringService;

		// Token: 0x020001EB RID: 491
		internal static class Config
		{
			// Token: 0x020001EC RID: 492
			internal static class RaiseCannotUnloadAppDomain
			{
				// Token: 0x040003FD RID: 1021
				internal const string Key = "RaiseCannotUnloadAppDomain";

				// Token: 0x040003FE RID: 1022
				internal const bool Default = false;
			}

			// Token: 0x020001ED RID: 493
			internal static class DelayBeforeAppDomainUnloadSeconds
			{
				// Token: 0x040003FF RID: 1023
				internal const string Key = "DelayBeforeAppDomainUnloadSeconds";

				// Token: 0x04000400 RID: 1024
				internal const int Min = 0;

				// Token: 0x04000401 RID: 1025
				internal const int Max = 2147483647;

				// Token: 0x04000402 RID: 1026
				internal const int Default = 180;
			}

			// Token: 0x020001EE RID: 494
			internal static class TestUserCacheRefreshMinutes
			{
				// Token: 0x04000403 RID: 1027
				internal const string Key = "TestUserCacheRefreshMinutes";

				// Token: 0x04000404 RID: 1028
				internal const int Min = 5;

				// Token: 0x04000405 RID: 1029
				internal const int Max = 1440;

				// Token: 0x04000406 RID: 1030
				internal const int Default = 60;
			}

			// Token: 0x020001EF RID: 495
			internal static class ReportFilteredExceptions
			{
				// Token: 0x04000407 RID: 1031
				internal const string Key = "ReportFilteredExceptions";

				// Token: 0x04000408 RID: 1032
				internal const bool Default = false;
			}

			// Token: 0x020001F0 RID: 496
			internal static class IncludeStackTraceInReportError
			{
				// Token: 0x04000409 RID: 1033
				internal const string Key = "IncludeStackTraceInReportError";

				// Token: 0x0400040A RID: 1034
				internal const bool Default = false;
			}

			// Token: 0x020001F1 RID: 497
			internal static class EnableLogging
			{
				// Token: 0x0400040B RID: 1035
				internal const string Key = "EnableLogging";

				// Token: 0x0400040C RID: 1036
				internal const bool Default = true;
			}

			// Token: 0x020001F2 RID: 498
			internal static class MaxLogDays
			{
				// Token: 0x0400040D RID: 1037
				internal const string Key = "MaxLogDays";

				// Token: 0x0400040E RID: 1038
				internal const int Min = 0;

				// Token: 0x0400040F RID: 1039
				internal const int Max = 2147483647;

				// Token: 0x04000410 RID: 1040
				internal const int Default = 7;
			}

			// Token: 0x020001F3 RID: 499
			internal static class LogVerbose
			{
				// Token: 0x04000411 RID: 1041
				internal const string Key = "LogVerbose";

				// Token: 0x04000412 RID: 1042
				internal const bool Default = false;
			}
		}
	}
}
