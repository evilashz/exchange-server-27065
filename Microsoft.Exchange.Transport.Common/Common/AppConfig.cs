using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.Globalization;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.Transport.Common
{
	// Token: 0x02000002 RID: 2
	internal abstract class AppConfig
	{
		// Token: 0x06000001 RID: 1 RVA: 0x000020D0 File Offset: 0x000002D0
		protected AppConfig(NameValueCollection appSettings = null)
		{
			this.AppSettings = (appSettings ?? ConfigurationManager.AppSettings);
		}

		// Token: 0x06000002 RID: 2 RVA: 0x000020E8 File Offset: 0x000002E8
		protected T GetConfigValue<T>(string label, T min, T max, T defaultValue, AppConfig.TryParse<T> tryParse) where T : IComparable<T>
		{
			string value = this.AppSettings[label];
			T result;
			this.TryParseConfigValue<T>(label, value, min, max, defaultValue, tryParse, out result);
			return result;
		}

		// Token: 0x06000003 RID: 3 RVA: 0x00002114 File Offset: 0x00000314
		protected T GetConfigValue<T>(string label, T defaultValue, AppConfig.TryParse<T> tryParse)
		{
			string value = this.AppSettings[label];
			T result;
			this.TryParseConfigValue<T>(value, defaultValue, tryParse, out result);
			return result;
		}

		// Token: 0x06000004 RID: 4 RVA: 0x0000213C File Offset: 0x0000033C
		protected List<T> GetConfigList<T>(string label, char separator, AppConfig.TryParse<T> tryParse)
		{
			string configValuesString = this.AppSettings[label];
			return this.GetConfigListFromValue<T>(configValuesString, separator, tryParse);
		}

		// Token: 0x06000005 RID: 5 RVA: 0x0000215F File Offset: 0x0000035F
		protected ByteQuantifiedSize GetConfigByteQuantifiedSize(string label, ByteQuantifiedSize min, ByteQuantifiedSize max, ByteQuantifiedSize defaultValue)
		{
			return this.GetConfigValue<ByteQuantifiedSize>(label, min, max, defaultValue, new AppConfig.TryParse<ByteQuantifiedSize>(ByteQuantifiedSize.TryParse));
		}

		// Token: 0x06000006 RID: 6 RVA: 0x00002178 File Offset: 0x00000378
		protected int GetConfigInt(string label, int min, int max, int defaultValue)
		{
			return this.GetConfigValue<int>(label, min, max, defaultValue, new AppConfig.TryParse<int>(int.TryParse));
		}

		// Token: 0x06000007 RID: 7 RVA: 0x00002191 File Offset: 0x00000391
		protected long GetConfigLong(string label, long min, long max, long defaultValue)
		{
			return this.GetConfigValue<long>(label, min, max, defaultValue, new AppConfig.TryParse<long>(long.TryParse));
		}

		// Token: 0x06000008 RID: 8 RVA: 0x000021AC File Offset: 0x000003AC
		protected List<int> GetConfigIntList(string label, int min, int max, int defaultValue, char separator)
		{
			List<int> configList = this.GetConfigList<int>(label, separator, new AppConfig.TryParse<int>(int.TryParse));
			for (int i = 0; i < configList.Count; i++)
			{
				if (configList[i] < min || configList[i] > max)
				{
					configList[i] = defaultValue;
				}
			}
			return configList;
		}

		// Token: 0x06000009 RID: 9 RVA: 0x000021FE File Offset: 0x000003FE
		protected double GetConfigDouble(string label, double min, double max, double defaultValue)
		{
			return this.GetConfigValue<double>(label, min, max, defaultValue, new AppConfig.TryParse<double>(double.TryParse));
		}

		// Token: 0x0600000A RID: 10 RVA: 0x00002217 File Offset: 0x00000417
		protected TimeSpan GetConfigTimeSpan(string label, TimeSpan min, TimeSpan max, TimeSpan defaultValue)
		{
			return this.GetConfigValue<TimeSpan>(label, min, max, defaultValue, new AppConfig.TryParse<TimeSpan>(TimeSpan.TryParse));
		}

		// Token: 0x0600000B RID: 11 RVA: 0x00002230 File Offset: 0x00000430
		protected bool GetConfigBool(string label, bool defaultValue)
		{
			return this.GetConfigValue<bool>(label, defaultValue, new AppConfig.TryParse<bool>(bool.TryParse));
		}

		// Token: 0x0600000C RID: 12 RVA: 0x00002280 File Offset: 0x00000480
		protected bool? GetConfigNullableBool(string label)
		{
			return this.GetConfigValue<bool?>(label, null, delegate(string s, out bool? parsed)
			{
				bool value = false;
				if (!string.IsNullOrEmpty(s) && bool.TryParse(s, out value))
				{
					parsed = new bool?(value);
					return true;
				}
				parsed = null;
				return false;
			});
		}

		// Token: 0x0600000D RID: 13 RVA: 0x000022BA File Offset: 0x000004BA
		protected T GetConfigEnum<T>(string label, T defaultValue) where T : struct
		{
			return this.GetConfigEnum<T>(label, defaultValue, EnumParseOptions.IgnoreCase);
		}

		// Token: 0x0600000E RID: 14 RVA: 0x000022DC File Offset: 0x000004DC
		protected T GetConfigEnum<T>(string label, T defaultValue, EnumParseOptions options) where T : struct
		{
			return this.GetConfigValue<T>(label, defaultValue, delegate(string s, out T parsed)
			{
				return EnumValidator.TryParse<T>(s, options, out parsed);
			});
		}

		// Token: 0x0600000F RID: 15 RVA: 0x0000230A File Offset: 0x0000050A
		protected string GetConfigString(string label, string defaultValue)
		{
			return this.AppSettings[label] ?? defaultValue;
		}

		// Token: 0x06000010 RID: 16 RVA: 0x0000231D File Offset: 0x0000051D
		private bool TryParseConfigValue<T>(string value, T defaultValue, AppConfig.TryParse<T> tryParse, out T configValue)
		{
			if (!string.IsNullOrEmpty(value) && tryParse(value, out configValue))
			{
				return true;
			}
			configValue = defaultValue;
			return false;
		}

		// Token: 0x06000011 RID: 17 RVA: 0x00002340 File Offset: 0x00000540
		private bool TryParseConfigValue<T>(string label, string value, T min, T max, T defaultValue, AppConfig.TryParse<T> tryParse, out T configValue) where T : IComparable<T>
		{
			if (min != null && max != null && min.CompareTo(max) > 0)
			{
				throw new ArgumentException(string.Format(CultureInfo.InvariantCulture, "Minimum must be smaller than or equal to Maximum (Config='{0}', Min='{1}', Max='{2}', Default='{3}').", new object[]
				{
					label,
					min,
					max,
					defaultValue
				}));
			}
			if (this.TryParseConfigValue<T>(value, defaultValue, tryParse, out configValue) && (min == null || configValue.CompareTo(min) >= 0) && (max == null || configValue.CompareTo(max) <= 0))
			{
				return true;
			}
			configValue = defaultValue;
			return false;
		}

		// Token: 0x06000012 RID: 18 RVA: 0x00002400 File Offset: 0x00000600
		private List<T> GetConfigListFromValue<T>(string configValuesString, char separator, AppConfig.TryParse<T> tryParse)
		{
			List<T> list = new List<T>();
			if (!string.IsNullOrEmpty(configValuesString))
			{
				string[] array = configValuesString.Split(new char[]
				{
					separator
				});
				foreach (string value in array)
				{
					T item;
					if (this.TryParseConfigValue<T>(value, default(T), tryParse, out item))
					{
						list.Add(item);
					}
				}
			}
			return list;
		}

		// Token: 0x04000001 RID: 1
		protected readonly NameValueCollection AppSettings;

		// Token: 0x02000003 RID: 3
		// (Invoke) Token: 0x06000015 RID: 21
		public delegate bool TryParse<T>(string config, out T parsedConfig);
	}
}
