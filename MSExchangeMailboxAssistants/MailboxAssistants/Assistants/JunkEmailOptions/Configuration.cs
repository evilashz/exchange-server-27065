using System;
using System.Collections.Specialized;
using System.Configuration;
using System.Globalization;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.MailboxAssistants.Assistants.JunkEmailOptions
{
	// Token: 0x02000111 RID: 273
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal static class Configuration
	{
		// Token: 0x170002A9 RID: 681
		// (get) Token: 0x06000B1E RID: 2846 RVA: 0x00047F3C File Offset: 0x0004613C
		public static bool IncludeSafeDomains
		{
			get
			{
				return Configuration.TryReadBool("IncludeSafeDomains", false);
			}
		}

		// Token: 0x170002AA RID: 682
		// (get) Token: 0x06000B1F RID: 2847 RVA: 0x00047F4C File Offset: 0x0004614C
		public static TimeSpan UpdateInterval
		{
			get
			{
				int defaultValue = (int)AssistantConfiguration.JunkEmailOptionsCommitterWorkCycle.Read().TotalMinutes;
				int num = 60 * Configuration.TryReadInt("UpdateInterval", defaultValue, (int)Configuration.MinUpdateInterval.TotalMinutes, (int)Configuration.MaxUpdateInterval.TotalMinutes);
				if (Configuration.IsTestConfiguration)
				{
					num = Configuration.TryReadInt("TestUpdateInterval", num, (int)Configuration.MinTestUpdateInterval.TotalSeconds, (int)Configuration.MaxTestUpdateInterval.TotalSeconds);
				}
				return TimeSpan.FromSeconds((double)num);
			}
		}

		// Token: 0x170002AB RID: 683
		// (get) Token: 0x06000B20 RID: 2848 RVA: 0x00047FD1 File Offset: 0x000461D1
		public static int MaxSafeSenders
		{
			get
			{
				return Configuration.TryReadInt("MaxSafeSenders", 3072, 0, 3072);
			}
		}

		// Token: 0x170002AC RID: 684
		// (get) Token: 0x06000B21 RID: 2849 RVA: 0x00047FE8 File Offset: 0x000461E8
		public static int MaxSafeRecipients
		{
			get
			{
				return Configuration.TryReadInt("MaxSafeRecipients", 2048, 0, 2048);
			}
		}

		// Token: 0x170002AD RID: 685
		// (get) Token: 0x06000B22 RID: 2850 RVA: 0x00047FFF File Offset: 0x000461FF
		public static int MaxBlockedSenders
		{
			get
			{
				return Configuration.TryReadInt("MaxBlockedSenders", 500, 0, 1000);
			}
		}

		// Token: 0x170002AE RID: 686
		// (get) Token: 0x06000B23 RID: 2851 RVA: 0x00048016 File Offset: 0x00046216
		public static bool IsTestConfiguration
		{
			get
			{
				return Configuration.PropertyExists("TestUpdateInterval");
			}
		}

		// Token: 0x06000B24 RID: 2852 RVA: 0x00048024 File Offset: 0x00046224
		private static bool TryReadBool(string propertyName, bool defaultValue)
		{
			string value = Configuration.TryReadProperty(propertyName);
			bool result;
			if (string.IsNullOrEmpty(value) || !bool.TryParse(value, out result))
			{
				result = defaultValue;
			}
			return result;
		}

		// Token: 0x06000B25 RID: 2853 RVA: 0x00048050 File Offset: 0x00046250
		private static int TryReadInt(string propertyName, int defaultValue, int minValue, int maxValue)
		{
			string text = Configuration.TryReadProperty(propertyName);
			int num;
			if (string.IsNullOrEmpty(text) || !int.TryParse(text, NumberStyles.Integer, CultureInfo.InvariantCulture, out num))
			{
				num = defaultValue;
			}
			if (num < minValue || num > maxValue)
			{
				num = defaultValue;
			}
			return num;
		}

		// Token: 0x06000B26 RID: 2854 RVA: 0x0004808C File Offset: 0x0004628C
		private static string TryReadProperty(string name)
		{
			string result;
			try
			{
				NameValueCollection appSettings = ConfigurationManager.AppSettings;
				result = appSettings[name];
			}
			catch (ConfigurationErrorsException)
			{
				result = null;
			}
			return result;
		}

		// Token: 0x06000B27 RID: 2855 RVA: 0x000480C0 File Offset: 0x000462C0
		private static bool PropertyExists(string propertyName)
		{
			string value = Configuration.TryReadProperty(propertyName);
			return !string.IsNullOrEmpty(value);
		}

		// Token: 0x04000703 RID: 1795
		private static readonly TimeSpan MinUpdateInterval = TimeSpan.FromMinutes(15.0);

		// Token: 0x04000704 RID: 1796
		private static readonly TimeSpan MaxUpdateInterval = TimeSpan.FromDays(1.0);

		// Token: 0x04000705 RID: 1797
		private static readonly TimeSpan MinTestUpdateInterval = TimeSpan.FromSeconds(10.0);

		// Token: 0x04000706 RID: 1798
		private static readonly TimeSpan MaxTestUpdateInterval = TimeSpan.FromHours(1.0);
	}
}
