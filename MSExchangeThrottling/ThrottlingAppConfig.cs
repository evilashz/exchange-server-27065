using System;
using System.Configuration;

namespace Microsoft.Exchange.Data.ThrottlingService
{
	// Token: 0x02000004 RID: 4
	internal static class ThrottlingAppConfig
	{
		// Token: 0x17000008 RID: 8
		// (get) Token: 0x06000015 RID: 21 RVA: 0x00002359 File Offset: 0x00000559
		public static bool AuthenticatedUsersRpcEnabled
		{
			get
			{
				return ThrottlingAppConfig.GetBoolValue("AuthenticatedUsersRpcEnabled", false);
			}
		}

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x06000016 RID: 22 RVA: 0x00002366 File Offset: 0x00000566
		public static bool LoggingEnabled
		{
			get
			{
				return ThrottlingAppConfig.GetBoolValue("LoggingEnabled", false);
			}
		}

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x06000017 RID: 23 RVA: 0x00002373 File Offset: 0x00000573
		public static TimeSpan LoggingMaxAge
		{
			get
			{
				return ThrottlingAppConfig.GetTimeSpanValue("LoggingMaxAge", ThrottlingAppConfig.LoggingMaxAgeDefault);
			}
		}

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x06000018 RID: 24 RVA: 0x00002384 File Offset: 0x00000584
		public static ByteQuantifiedSize LoggingDirectorySize
		{
			get
			{
				return ThrottlingAppConfig.GetByteQuantifiedSizeValue("LoggingDirectorySize", ThrottlingAppConfig.LoggingDirectorySizeDefault);
			}
		}

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x06000019 RID: 25 RVA: 0x00002395 File Offset: 0x00000595
		public static string LogPath
		{
			get
			{
				return ThrottlingAppConfig.GetStringValue("LogPath");
			}
		}

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x0600001A RID: 26 RVA: 0x000023A1 File Offset: 0x000005A1
		public static TimeSpan ADOperationTimeout
		{
			get
			{
				return ThrottlingAppConfig.GetTimeSpanValue("ADOperationTimeout", ThrottlingAppConfig.ADOperationTimeoutDefault);
			}
		}

		// Token: 0x0600001B RID: 27 RVA: 0x000023B4 File Offset: 0x000005B4
		private static bool GetBoolValue(string label, bool defaultValue)
		{
			bool result = defaultValue;
			try
			{
				string value = ConfigurationManager.AppSettings[label];
				if (string.IsNullOrEmpty(value) || !bool.TryParse(value, out result))
				{
					return defaultValue;
				}
			}
			catch (ConfigurationErrorsException)
			{
			}
			return result;
		}

		// Token: 0x0600001C RID: 28 RVA: 0x00002400 File Offset: 0x00000600
		private static string GetStringValue(string label)
		{
			try
			{
				return ConfigurationManager.AppSettings[label];
			}
			catch (ConfigurationErrorsException)
			{
			}
			return null;
		}

		// Token: 0x0600001D RID: 29 RVA: 0x00002434 File Offset: 0x00000634
		private static TimeSpan GetTimeSpanValue(string label, TimeSpan defaultValue)
		{
			TimeSpan result = defaultValue;
			try
			{
				string text = ConfigurationManager.AppSettings[label];
				if (string.IsNullOrEmpty(text) || !TimeSpan.TryParse(text, out result))
				{
					return defaultValue;
				}
			}
			catch (ConfigurationErrorsException)
			{
			}
			return result;
		}

		// Token: 0x0600001E RID: 30 RVA: 0x00002480 File Offset: 0x00000680
		private static ByteQuantifiedSize GetByteQuantifiedSizeValue(string label, ByteQuantifiedSize defaultValue)
		{
			ByteQuantifiedSize result = defaultValue;
			try
			{
				string text = ConfigurationManager.AppSettings[label];
				if (string.IsNullOrEmpty(text) || !ByteQuantifiedSize.TryParse(text, out result))
				{
					return defaultValue;
				}
			}
			catch (ConfigurationErrorsException)
			{
			}
			return result;
		}

		// Token: 0x0400000B RID: 11
		public const string AuthenticatedUsersRpcEnabledLabel = "AuthenticatedUsersRpcEnabled";

		// Token: 0x0400000C RID: 12
		public const bool AuthenticatedUsersRpcEnabledDefault = false;

		// Token: 0x0400000D RID: 13
		public const bool LoggingEnabledDefault = false;

		// Token: 0x0400000E RID: 14
		private const string LoggingEnabledLabel = "LoggingEnabled";

		// Token: 0x0400000F RID: 15
		private const string LogPathLabel = "LogPath";

		// Token: 0x04000010 RID: 16
		private const string LoggingMaxAgeLabel = "LoggingMaxAge";

		// Token: 0x04000011 RID: 17
		private const string LoggingDirectorySizeLabel = "LoggingDirectorySize";

		// Token: 0x04000012 RID: 18
		private const string ADOperationTimeoutLabel = "ADOperationTimeout";

		// Token: 0x04000013 RID: 19
		private static readonly TimeSpan ADOperationTimeoutDefault = TimeSpan.FromSeconds(30.0);

		// Token: 0x04000014 RID: 20
		private static readonly TimeSpan LoggingMaxAgeDefault = TimeSpan.FromDays(21.0);

		// Token: 0x04000015 RID: 21
		private static readonly ByteQuantifiedSize LoggingDirectorySizeDefault = ByteQuantifiedSize.FromMB(40UL);
	}
}
