using System;
using System.Configuration;
using Microsoft.Exchange.Diagnostics.Components.SharedCache;
using Microsoft.Exchange.Net;

namespace Microsoft.Exchange.SharedCache
{
	// Token: 0x0200000B RID: 11
	internal sealed class CacheSettings : ConfigurationElement
	{
		// Token: 0x1700000B RID: 11
		// (get) Token: 0x06000036 RID: 54 RVA: 0x00002FDA File Offset: 0x000011DA
		public string DatabaseDirectory
		{
			get
			{
				return CacheSettings.JetDatabasePath.Value + "\\" + this.Name;
			}
		}

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x06000037 RID: 55 RVA: 0x00002FF6 File Offset: 0x000011F6
		// (set) Token: 0x06000038 RID: 56 RVA: 0x00003008 File Offset: 0x00001208
		[StringValidator(InvalidCharacters = "~!@#$%^&*()[]{}/;'\"|\\", MinLength = 3, MaxLength = 60)]
		[ConfigurationProperty("Name", IsKey = true, IsRequired = true, DefaultValue = "ACache")]
		public string Name
		{
			get
			{
				return (string)base["Name"];
			}
			set
			{
				base["Name"] = value;
			}
		}

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x06000039 RID: 57 RVA: 0x00003016 File Offset: 0x00001216
		// (set) Token: 0x0600003A RID: 58 RVA: 0x00003028 File Offset: 0x00001228
		[ConfigurationProperty("Guid", IsKey = true, IsRequired = true)]
		public Guid Guid
		{
			get
			{
				return (Guid)base["Guid"];
			}
			set
			{
				base["Guid"] = value;
			}
		}

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x0600003B RID: 59 RVA: 0x0000303B File Offset: 0x0000123B
		// (set) Token: 0x0600003C RID: 60 RVA: 0x00003057 File Offset: 0x00001257
		[ConfigurationProperty("Type", DefaultValue = "SharedTimeoutCache", IsKey = false, IsRequired = true)]
		[StringValidator(InvalidCharacters = " ~!@#$%^&*()[]{}/;'\"|\\", MinLength = 3, MaxLength = 60)]
		public string Type
		{
			get
			{
				return this.typeOverride ?? ((string)base["Type"]);
			}
			set
			{
				this.typeOverride = value;
			}
		}

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x0600003D RID: 61 RVA: 0x00003060 File Offset: 0x00001260
		// (set) Token: 0x0600003E RID: 62 RVA: 0x00003072 File Offset: 0x00001272
		[IntegerValidator(MinValue = 1, ExcludeRange = false)]
		[ConfigurationProperty("Size", DefaultValue = 100000, IsKey = false, IsRequired = false)]
		public int Size
		{
			get
			{
				return (int)base["Size"];
			}
			set
			{
				base["Size"] = value;
			}
		}

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x0600003F RID: 63 RVA: 0x00003085 File Offset: 0x00001285
		// (set) Token: 0x06000040 RID: 64 RVA: 0x00003097 File Offset: 0x00001297
		[ConfigurationProperty("ServerRole", DefaultValue = "All", IsKey = false, IsRequired = true)]
		[StringValidator(InvalidCharacters = " ~!@#$%^&*()[]{}/;'\"|\\", MinLength = 2, MaxLength = 10)]
		public string ServerRole
		{
			get
			{
				return (string)base["ServerRole"];
			}
			set
			{
				base["ServerRole"] = value;
			}
		}

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x06000041 RID: 65 RVA: 0x000030A5 File Offset: 0x000012A5
		// (set) Token: 0x06000042 RID: 66 RVA: 0x000030B7 File Offset: 0x000012B7
		[ConfigurationProperty("EntryTimeout", DefaultValue = "00:15:00", IsKey = false, IsRequired = false)]
		public TimeSpan EntryTimeout
		{
			get
			{
				return (TimeSpan)base["EntryTimeout"];
			}
			set
			{
				base["EntryTimeout"] = value;
			}
		}

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x06000043 RID: 67 RVA: 0x000030CA File Offset: 0x000012CA
		// (set) Token: 0x06000044 RID: 68 RVA: 0x000030DC File Offset: 0x000012DC
		[ConfigurationProperty("PerfCounterHitRateSlidingWindow", DefaultValue = "00:05:00", IsKey = false, IsRequired = false)]
		public TimeSpan PerfCounterHitRateSlidingWindow
		{
			get
			{
				return (TimeSpan)base["PerfCounterHitRateSlidingWindow"];
			}
			set
			{
				base["PerfCounterHitRateSlidingWindow"] = value;
			}
		}

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x06000045 RID: 69 RVA: 0x000030EF File Offset: 0x000012EF
		// (set) Token: 0x06000046 RID: 70 RVA: 0x00003101 File Offset: 0x00001301
		[ConfigurationProperty("PerfCounterHitRateGranularity", DefaultValue = "00:00:15", IsKey = false, IsRequired = false)]
		public TimeSpan PerfCounterHitRateGranularity
		{
			get
			{
				return (TimeSpan)base["PerfCounterHitRateGranularity"];
			}
			set
			{
				base["PerfCounterHitRateGranularity"] = value;
			}
		}

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x06000047 RID: 71 RVA: 0x00003114 File Offset: 0x00001314
		// (set) Token: 0x06000048 RID: 72 RVA: 0x00003126 File Offset: 0x00001326
		[IntegerValidator(MinValue = 1, ExcludeRange = false)]
		[ConfigurationProperty("PerfCounterAverageLatencySampleCount", DefaultValue = 200, IsKey = false, IsRequired = false)]
		public int PerfCounterAverageLatencySampleCount
		{
			get
			{
				return (int)base["PerfCounterAverageLatencySampleCount"];
			}
			set
			{
				base["PerfCounterAverageLatencySampleCount"] = value;
			}
		}

		// Token: 0x06000049 RID: 73 RVA: 0x0000313C File Offset: 0x0000133C
		public override string ToString()
		{
			return string.Format("Name={0}, Type={1}, Size={2}, EntryTimeout={3}, PerfCounterHitRateSlidingWindow={4}, PerfCounterHitRateGranularity={5}, PerfCounterAverageLatencySampleCount={6}, DatabaseDirectory={7}", new object[]
			{
				this.Name,
				this.Type,
				this.Size,
				this.EntryTimeout.ToString(),
				this.PerfCounterHitRateSlidingWindow.ToString(),
				this.PerfCounterHitRateGranularity.ToString(),
				this.PerfCounterAverageLatencySampleCount.ToString(),
				this.DatabaseDirectory
			});
		}

		// Token: 0x04000021 RID: 33
		public static readonly StringAppSettingsEntry JetDatabasePath = new StringAppSettingsEntry("DatabaseFolder", "C:\\", ExTraceGlobals.ServerTracer);

		// Token: 0x04000022 RID: 34
		public static readonly IntAppSettingsEntry MaxResults = new IntAppSettingsEntry("EDIMaxFindResultSize", 100, ExTraceGlobals.ServerTracer);

		// Token: 0x04000023 RID: 35
		private string typeOverride;
	}
}
