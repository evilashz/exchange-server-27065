using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Net;
using Microsoft.Win32;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000F10 RID: 3856
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class ActivityLoggingConfig : IActivityLoggingConfig
	{
		// Token: 0x17002332 RID: 9010
		// (get) Token: 0x060084B4 RID: 33972 RVA: 0x00243C7A File Offset: 0x00241E7A
		public static IActivityLoggingConfig Instance
		{
			get
			{
				return ActivityLoggingConfig.InstanceHook.Value;
			}
		}

		// Token: 0x17002333 RID: 9011
		// (get) Token: 0x060084B5 RID: 33973 RVA: 0x00243C86 File Offset: 0x00241E86
		public TimeSpan MaxLogFileAge
		{
			get
			{
				return this.maxLogFileAgeInternal;
			}
		}

		// Token: 0x17002334 RID: 9012
		// (get) Token: 0x060084B6 RID: 33974 RVA: 0x00243C8E File Offset: 0x00241E8E
		public ByteQuantifiedSize MaxLogDirectorySize
		{
			get
			{
				return this.maxLogDirectorySizeInternal;
			}
		}

		// Token: 0x17002335 RID: 9013
		// (get) Token: 0x060084B7 RID: 33975 RVA: 0x00243C96 File Offset: 0x00241E96
		public ByteQuantifiedSize MaxLogFileSize
		{
			get
			{
				return this.maxLogFileSizeInternal;
			}
		}

		// Token: 0x17002336 RID: 9014
		// (get) Token: 0x060084B8 RID: 33976 RVA: 0x00243C9E File Offset: 0x00241E9E
		public bool IsDumpCollectionEnabled
		{
			get
			{
				return this.isDumpCollectionEnabled;
			}
		}

		// Token: 0x060084B9 RID: 33977 RVA: 0x00243CA6 File Offset: 0x00241EA6
		internal ActivityLoggingConfig()
		{
			this.maxLogFileAgeInternal = ActivityLoggingConfig.GetMaxLogFileAgeOrDefault();
			this.maxLogDirectorySizeInternal = ActivityLoggingConfig.GetMaxLogDirectorySizeOrDefault();
			this.maxLogFileSizeInternal = ActivityLoggingConfig.GetMaxLogFileSizeOrDefault();
			this.isDumpCollectionEnabled = ActivityLoggingConfig.GetIsDumpCollectionEnabled();
		}

		// Token: 0x060084BA RID: 33978 RVA: 0x00243CDC File Offset: 0x00241EDC
		private static ByteQuantifiedSize GetMaxLogFileSizeOrDefault()
		{
			ulong value = RegistryReader.Instance.GetValue<ulong>(Registry.LocalMachine, "SOFTWARE\\Microsoft\\ExchangeServer\\v15\\Inference\\ActivityLogging", "MaxLogFileSizeInMB", ActivityLoggingConfig.MaxLogFileSizeDefault.ToMB());
			return ByteQuantifiedSize.FromMB(value);
		}

		// Token: 0x060084BB RID: 33979 RVA: 0x00243D18 File Offset: 0x00241F18
		private static ByteQuantifiedSize GetMaxLogDirectorySizeOrDefault()
		{
			ulong value = RegistryReader.Instance.GetValue<ulong>(Registry.LocalMachine, "SOFTWARE\\Microsoft\\ExchangeServer\\v15\\Inference\\ActivityLogging", "MaxLogDirectorySizeInMB", ActivityLoggingConfig.MaxLogDirectorySizeDefault.ToMB());
			return ByteQuantifiedSize.FromMB(value);
		}

		// Token: 0x060084BC RID: 33980 RVA: 0x00243D54 File Offset: 0x00241F54
		private static TimeSpan GetMaxLogFileAgeOrDefault()
		{
			string value = RegistryReader.Instance.GetValue<string>(Registry.LocalMachine, "SOFTWARE\\Microsoft\\ExchangeServer\\v15\\Inference\\ActivityLogging", "MaxLogFileAge", string.Empty);
			TimeSpan result;
			if (string.IsNullOrEmpty(value) || !TimeSpan.TryParse(value, out result))
			{
				return ActivityLoggingConfig.MaxLogFileAgeDefault;
			}
			return result;
		}

		// Token: 0x060084BD RID: 33981 RVA: 0x00243D99 File Offset: 0x00241F99
		private static bool GetIsDumpCollectionEnabled()
		{
			return RegistryReader.Instance.GetValue<bool>(Registry.LocalMachine, "SOFTWARE\\Microsoft\\ExchangeServer\\v15\\Inference\\ActivityLogging", "IsDumpCollectionEnabled", ActivityLoggingConfig.IsDumpCollectionEnabledDefault);
		}

		// Token: 0x040058E5 RID: 22757
		private const string RegkeyName = "SOFTWARE\\Microsoft\\ExchangeServer\\v15\\Inference\\ActivityLogging";

		// Token: 0x040058E6 RID: 22758
		private const string MaxLogFileAgeRegkeyValueName = "MaxLogFileAge";

		// Token: 0x040058E7 RID: 22759
		private const string MaxLogDirectorySizeInMBRegkeyValueName = "MaxLogDirectorySizeInMB";

		// Token: 0x040058E8 RID: 22760
		private const string MaxLogFileSizeInMBRegkeyValueName = "MaxLogFileSizeInMB";

		// Token: 0x040058E9 RID: 22761
		private const string IsDumpCollectionEnabledRegkeyValueName = "IsDumpCollectionEnabled";

		// Token: 0x040058EA RID: 22762
		private static readonly TimeSpan MaxLogFileAgeDefault = TimeSpan.FromDays(14.0);

		// Token: 0x040058EB RID: 22763
		private static readonly ByteQuantifiedSize MaxLogDirectorySizeDefault = ByteQuantifiedSize.FromGB(3UL);

		// Token: 0x040058EC RID: 22764
		private static readonly ByteQuantifiedSize MaxLogFileSizeDefault = ByteQuantifiedSize.FromMB(10UL);

		// Token: 0x040058ED RID: 22765
		private static readonly bool IsDumpCollectionEnabledDefault = false;

		// Token: 0x040058EE RID: 22766
		internal static readonly Hookable<IActivityLoggingConfig> InstanceHook = Hookable<IActivityLoggingConfig>.Create(true, new ActivityLoggingConfig());

		// Token: 0x040058EF RID: 22767
		private readonly TimeSpan maxLogFileAgeInternal;

		// Token: 0x040058F0 RID: 22768
		private readonly ByteQuantifiedSize maxLogDirectorySizeInternal;

		// Token: 0x040058F1 RID: 22769
		private readonly ByteQuantifiedSize maxLogFileSizeInternal;

		// Token: 0x040058F2 RID: 22770
		private readonly bool isDumpCollectionEnabled;
	}
}
