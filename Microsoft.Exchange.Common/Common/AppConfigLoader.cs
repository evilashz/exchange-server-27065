using System;
using System.Configuration;
using System.Diagnostics;
using System.Web.Configuration;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Common;

namespace Microsoft.Exchange.Common
{
	// Token: 0x02000026 RID: 38
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal static class AppConfigLoader
	{
		// Token: 0x1700001B RID: 27
		// (get) Token: 0x060000BA RID: 186 RVA: 0x000041F8 File Offset: 0x000023F8
		public static bool IsWebApp
		{
			get
			{
				lock (AppConfigLoader.initLock)
				{
					if (AppConfigLoader.isWebApp == null)
					{
						AppConfigLoader.isWebApp = new bool?(Process.GetCurrentProcess().MainModule.ModuleName.Equals("w3wp.exe", StringComparison.OrdinalIgnoreCase));
					}
				}
				return AppConfigLoader.isWebApp.Value;
			}
		}

		// Token: 0x060000BB RID: 187 RVA: 0x0000426C File Offset: 0x0000246C
		public static bool GetConfigBoolValue(string configName, bool defaultValue)
		{
			return AppConfigLoader.GetConfigValue<bool>(configName, null, null, defaultValue, new AppConfigLoader.TryParseValue<bool>(bool.TryParse));
		}

		// Token: 0x060000BC RID: 188 RVA: 0x000042A0 File Offset: 0x000024A0
		public static string GetConfigStringValue(string configName, string defaultValue)
		{
			string result = null;
			if (!AppConfigLoader.TryGetConfigRawValue(configName, out result))
			{
				return defaultValue;
			}
			return result;
		}

		// Token: 0x060000BD RID: 189 RVA: 0x000042BC File Offset: 0x000024BC
		public static T GetConfigEnumValue<T>(string configName, T defaultValue)
		{
			string text = null;
			if (!AppConfigLoader.TryGetConfigRawValue(configName, out text))
			{
				return defaultValue;
			}
			T result;
			try
			{
				T t = (T)((object)Enum.Parse(typeof(T), text, true));
				result = t;
			}
			catch (ArgumentException arg)
			{
				AppConfigLoader.Tracer.TraceWarning<string, string, ArgumentException>(0L, "Invalid Value:{0}, for config:{1}, parsing failed with error:{2}", text, configName, arg);
				result = defaultValue;
			}
			return result;
		}

		// Token: 0x060000BE RID: 190 RVA: 0x0000431C File Offset: 0x0000251C
		public static int GetConfigIntValue(string configName, int minValue, int maxValue, int defaultValue)
		{
			return AppConfigLoader.GetConfigValue<int>(configName, new int?(minValue), new int?(maxValue), defaultValue, new AppConfigLoader.TryParseValue<int>(int.TryParse));
		}

		// Token: 0x060000BF RID: 191 RVA: 0x0000433D File Offset: 0x0000253D
		public static double GetConfigDoubleValue(string configName, double minValue, double maxValue, double defaultValue)
		{
			return AppConfigLoader.GetConfigValue<double>(configName, new double?(minValue), new double?(maxValue), defaultValue, new AppConfigLoader.TryParseValue<double>(double.TryParse));
		}

		// Token: 0x060000C0 RID: 192 RVA: 0x0000435E File Offset: 0x0000255E
		public static TimeSpan GetConfigTimeSpanValue(string configName, TimeSpan minValue, TimeSpan maxValue, TimeSpan defaultValue)
		{
			return AppConfigLoader.GetConfigValue<TimeSpan>(configName, new TimeSpan?(minValue), new TimeSpan?(maxValue), defaultValue, new AppConfigLoader.TryParseValue<TimeSpan>(TimeSpan.TryParse));
		}

		// Token: 0x060000C1 RID: 193 RVA: 0x00004380 File Offset: 0x00002580
		public static bool TryGetConfigRawValue(string configName, out string rawValue)
		{
			if (configName == null)
			{
				throw new ArgumentNullException(configName);
			}
			if (configName.Equals(string.Empty, StringComparison.OrdinalIgnoreCase))
			{
				throw new ArgumentException("ConfigName cannot be empty", configName);
			}
			rawValue = null;
			try
			{
				if (AppConfigLoader.IsWebApp)
				{
					rawValue = WebConfigurationManager.AppSettings[configName];
				}
				else
				{
					rawValue = ConfigurationManager.AppSettings[configName];
				}
			}
			catch (ConfigurationErrorsException arg)
			{
				AppConfigLoader.Tracer.TraceWarning<string, ConfigurationErrorsException>(0L, "failed to read config {0}: {1}", configName, arg);
				return false;
			}
			if (rawValue == null)
			{
				AppConfigLoader.Tracer.TraceDebug<string>(0L, "cannot apply null config {0}", configName);
				return false;
			}
			return true;
		}

		// Token: 0x060000C2 RID: 194 RVA: 0x0000441C File Offset: 0x0000261C
		private static T GetConfigValue<T>(string configName, T? minValue, T? maxValue, T defaultValue, AppConfigLoader.TryParseValue<T> tryParseValue) where T : struct, IComparable<T>
		{
			string text = null;
			if (!AppConfigLoader.TryGetConfigRawValue(configName, out text))
			{
				return defaultValue;
			}
			T t = defaultValue;
			if (!tryParseValue(text, out t))
			{
				AppConfigLoader.Tracer.TraceWarning<string, string>(0L, "cannot apply config {0} with invalid value: {1}", configName, text);
				return defaultValue;
			}
			if (minValue != null && t.CompareTo(minValue.Value) < 0)
			{
				AppConfigLoader.Tracer.TraceWarning<string, T, T>(0L, "cannot apply config:{0}, value:{1} is less than minValue:{2}", configName, t, minValue.Value);
				return defaultValue;
			}
			if (maxValue != null && t.CompareTo(maxValue.Value) > 0)
			{
				AppConfigLoader.Tracer.TraceWarning<string, T, T>(0L, "cannot apply config:{0}, value:{1} is greater than maxValue:{2}", configName, t, maxValue.Value);
				return defaultValue;
			}
			return t;
		}

		// Token: 0x04000080 RID: 128
		private static readonly Microsoft.Exchange.Diagnostics.Trace Tracer = ExTraceGlobals.AppConfigLoaderTracer;

		// Token: 0x04000081 RID: 129
		private static bool? isWebApp = null;

		// Token: 0x04000082 RID: 130
		private static object initLock = new object();

		// Token: 0x02000027 RID: 39
		// (Invoke) Token: 0x060000C5 RID: 197
		private delegate bool TryParseValue<T>(string stringValue, out T configValue);
	}
}
