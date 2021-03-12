using System;
using System.Configuration;

namespace Microsoft.Exchange.Hygiene.Cache.Data
{
	// Token: 0x0200005E RID: 94
	internal class EntityCacheConfiguration : ConfigurationElement
	{
		// Token: 0x17000175 RID: 373
		// (get) Token: 0x060003A3 RID: 931 RVA: 0x0000A6BD File Offset: 0x000088BD
		// (set) Token: 0x060003A4 RID: 932 RVA: 0x0000A6CF File Offset: 0x000088CF
		[ConfigurationProperty("Name", IsKey = true, IsRequired = true)]
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

		// Token: 0x17000176 RID: 374
		// (get) Token: 0x060003A5 RID: 933 RVA: 0x0000A6DD File Offset: 0x000088DD
		// (set) Token: 0x060003A6 RID: 934 RVA: 0x0000A6EF File Offset: 0x000088EF
		[ConfigurationProperty("MaxCapacity", IsRequired = false, DefaultValue = 100000)]
		public int MaxCapacity
		{
			get
			{
				return (int)base["MaxCapacity"];
			}
			set
			{
				base["MaxCapacity"] = value;
			}
		}

		// Token: 0x17000177 RID: 375
		// (get) Token: 0x060003A7 RID: 935 RVA: 0x0000A702 File Offset: 0x00008902
		// (set) Token: 0x060003A8 RID: 936 RVA: 0x0000A714 File Offset: 0x00008914
		[ConfigurationProperty("TenantBatchSize", IsRequired = false, DefaultValue = 100)]
		public int TenantBatchSize
		{
			get
			{
				return (int)base["TenantBatchSize"];
			}
			set
			{
				base["TenantBatchSize"] = value;
			}
		}

		// Token: 0x17000178 RID: 376
		// (get) Token: 0x060003A9 RID: 937 RVA: 0x0000A727 File Offset: 0x00008927
		// (set) Token: 0x060003AA RID: 938 RVA: 0x0000A739 File Offset: 0x00008939
		[ConfigurationProperty("ItemRefreshInterval", IsRequired = false, DefaultValue = "00:30:00")]
		public TimeSpan ItemRefreshInterval
		{
			get
			{
				return (TimeSpan)base["ItemRefreshInterval"];
			}
			set
			{
				base["ItemRefreshInterval"] = value;
			}
		}

		// Token: 0x17000179 RID: 377
		// (get) Token: 0x060003AB RID: 939 RVA: 0x0000A74C File Offset: 0x0000894C
		// (set) Token: 0x060003AC RID: 940 RVA: 0x0000A75E File Offset: 0x0000895E
		[ConfigurationProperty("MaxUnusedInterval", IsRequired = false, DefaultValue = "1.00:00:00")]
		public TimeSpan MaxUnusedInterval
		{
			get
			{
				return (TimeSpan)base["MaxUnusedInterval"];
			}
			set
			{
				base["MaxUnusedInterval"] = value;
			}
		}

		// Token: 0x1700017A RID: 378
		// (get) Token: 0x060003AD RID: 941 RVA: 0x0000A771 File Offset: 0x00008971
		// (set) Token: 0x060003AE RID: 942 RVA: 0x0000A783 File Offset: 0x00008983
		[ConfigurationProperty("MaxItemRefreshStaggerInterval", IsRequired = false, DefaultValue = "00:05:00")]
		public TimeSpan MaxItemRefreshStaggerInterval
		{
			get
			{
				return (TimeSpan)base["MaxItemRefreshStaggerInterval"];
			}
			set
			{
				base["MaxItemRefreshStaggerInterval"] = value;
			}
		}

		// Token: 0x1700017B RID: 379
		// (get) Token: 0x060003AF RID: 943 RVA: 0x0000A796 File Offset: 0x00008996
		// (set) Token: 0x060003B0 RID: 944 RVA: 0x0000A7A8 File Offset: 0x000089A8
		[ConfigurationProperty("RefreshThreadSleepInterval", IsRequired = false, DefaultValue = "00:00:30")]
		public TimeSpan RefreshThreadSleepInterval
		{
			get
			{
				return (TimeSpan)base["RefreshThreadSleepInterval"];
			}
			set
			{
				base["RefreshThreadSleepInterval"] = value;
			}
		}

		// Token: 0x1700017C RID: 380
		// (get) Token: 0x060003B1 RID: 945 RVA: 0x0000A7BB File Offset: 0x000089BB
		// (set) Token: 0x060003B2 RID: 946 RVA: 0x0000A7CD File Offset: 0x000089CD
		[ConfigurationProperty("Serialize", IsRequired = false, DefaultValue = false)]
		public bool Serialize
		{
			get
			{
				return (bool)base["Serialize"];
			}
			set
			{
				base["Serialize"] = value;
			}
		}

		// Token: 0x1700017D RID: 381
		// (get) Token: 0x060003B3 RID: 947 RVA: 0x0000A7E0 File Offset: 0x000089E0
		// (set) Token: 0x060003B4 RID: 948 RVA: 0x0000A7F2 File Offset: 0x000089F2
		[ConfigurationProperty("PrepopulateTenants", IsRequired = false, DefaultValue = true)]
		public bool PrepopulateTenants
		{
			get
			{
				return (bool)base["PrepopulateTenants"];
			}
			set
			{
				base["PrepopulateTenants"] = value;
			}
		}

		// Token: 0x04000232 RID: 562
		private const string NameKey = "Name";

		// Token: 0x04000233 RID: 563
		private const string MaxCapacityKey = "MaxCapacity";

		// Token: 0x04000234 RID: 564
		private const string TenantBatchSizeKey = "TenantBatchSize";

		// Token: 0x04000235 RID: 565
		private const string ItemRefreshIntervalKey = "ItemRefreshInterval";

		// Token: 0x04000236 RID: 566
		private const string MaxUnusedIntervalKey = "MaxUnusedInterval";

		// Token: 0x04000237 RID: 567
		private const string MaxItemRefreshStaggerIntervalKey = "MaxItemRefreshStaggerInterval";

		// Token: 0x04000238 RID: 568
		private const string RefreshThreadSleepIntervalKey = "RefreshThreadSleepInterval";

		// Token: 0x04000239 RID: 569
		private const string SerializeKey = "Serialize";

		// Token: 0x0400023A RID: 570
		private const string PrepopulateTenantsKey = "PrepopulateTenants";
	}
}
