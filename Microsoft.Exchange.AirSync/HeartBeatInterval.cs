using System;
using System.Globalization;

namespace Microsoft.Exchange.AirSync
{
	// Token: 0x020000C6 RID: 198
	internal struct HeartBeatInterval
	{
		// Token: 0x06000BA0 RID: 2976 RVA: 0x0003F1B7 File Offset: 0x0003D3B7
		private HeartBeatInterval(int lowHbi, int highHbi)
		{
			this.lowInterval = lowHbi;
			this.highInterval = highHbi;
		}

		// Token: 0x06000BA1 RID: 2977 RVA: 0x0003F1C8 File Offset: 0x0003D3C8
		public static HeartBeatInterval Read()
		{
			string text = GlobalSettingsSchema.GetAppSetting(GlobalSettingsSchema.Supporting_MinHeartbeatInterval);
			string text2 = GlobalSettingsSchema.GetAppSetting(GlobalSettingsSchema.Supporting_MaxHeartbeatInterval);
			if (string.IsNullOrEmpty(text))
			{
				text = 60.ToString();
			}
			if (string.IsNullOrEmpty(text2))
			{
				text2 = 3540.ToString();
			}
			int num;
			if (!int.TryParse(text, NumberStyles.Integer, CultureInfo.InvariantCulture, out num))
			{
				AirSyncDiagnostics.LogEvent(AirSyncEventLogConstants.Tuple_GlobalValueNotParsable, new string[]
				{
					"MinHeartbeatInterval",
					typeof(int).Name,
					text ?? "$null",
					60.ToString()
				});
				num = 60;
			}
			int num2;
			if (!int.TryParse(text2, NumberStyles.Integer, CultureInfo.InvariantCulture, out num2))
			{
				AirSyncDiagnostics.LogEvent(AirSyncEventLogConstants.Tuple_GlobalValueNotParsable, new string[]
				{
					"MaxHeartbeatInterval",
					typeof(int).Name,
					text2 ?? "$null",
					3540.ToString()
				});
				num2 = 3540;
			}
			if (num > num2)
			{
				AirSyncDiagnostics.LogEvent(AirSyncEventLogConstants.Tuple_InvalidHbiLimits, new string[]
				{
					60.ToString(CultureInfo.InvariantCulture),
					3540.ToString(CultureInfo.InvariantCulture)
				});
				num = 60;
				num2 = 3540;
			}
			else if (num2 > 3540)
			{
				AirSyncDiagnostics.LogEvent(AirSyncEventLogConstants.Tuple_MaxHbiTooHigh, new string[]
				{
					3540.ToString(CultureInfo.InvariantCulture),
					3540.ToString(CultureInfo.InvariantCulture),
					60.ToString(CultureInfo.InvariantCulture)
				});
				num = 60;
				num2 = 3540;
			}
			else if (num < 1)
			{
				AirSyncDiagnostics.LogEvent(AirSyncEventLogConstants.Tuple_MinHbiTooLow, new string[]
				{
					60.ToString(CultureInfo.InvariantCulture),
					3540.ToString(CultureInfo.InvariantCulture)
				});
				num = 60;
				num2 = 3540;
			}
			return new HeartBeatInterval(num, num2);
		}

		// Token: 0x17000487 RID: 1159
		// (get) Token: 0x06000BA2 RID: 2978 RVA: 0x0003F3E9 File Offset: 0x0003D5E9
		public int LowInterval
		{
			get
			{
				return this.lowInterval;
			}
		}

		// Token: 0x17000488 RID: 1160
		// (get) Token: 0x06000BA3 RID: 2979 RVA: 0x0003F3F1 File Offset: 0x0003D5F1
		public int HighInterval
		{
			get
			{
				return this.highInterval;
			}
		}

		// Token: 0x06000BA4 RID: 2980 RVA: 0x0003F3F9 File Offset: 0x0003D5F9
		public override string ToString()
		{
			return string.Format("Low: {0}, High: {1}", this.LowInterval, this.HighInterval);
		}

		// Token: 0x04000737 RID: 1847
		public const string LowAppSettingName = "MinHeartbeatInterval";

		// Token: 0x04000738 RID: 1848
		public const string HighAppSettingName = "MaxHeartbeatInterval";

		// Token: 0x04000739 RID: 1849
		public const int DefaultLowHbi = 60;

		// Token: 0x0400073A RID: 1850
		public const int MinLowHbi = 1;

		// Token: 0x0400073B RID: 1851
		public const int MaxLowHbi = 3540;

		// Token: 0x0400073C RID: 1852
		public const int DefaultHighHbi = 3540;

		// Token: 0x0400073D RID: 1853
		public const int MinHighHbi = 1;

		// Token: 0x0400073E RID: 1854
		public const int MaxHighHbi = 3540;

		// Token: 0x0400073F RID: 1855
		private int lowInterval;

		// Token: 0x04000740 RID: 1856
		private int highInterval;

		// Token: 0x04000741 RID: 1857
		public static readonly HeartBeatInterval Default = new HeartBeatInterval(60, 3540);
	}
}
