using System;
using System.Collections.Generic;
using System.Configuration;
using System.Runtime.Caching;
using System.Threading;
using Microsoft.Exchange.Data.ConfigurationSettings;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Directory.SystemConfiguration.ConfigurationSettings;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Directory.Cache
{
	// Token: 0x0200009B RID: 155
	internal class ConfigurationADImpl : ICacheConfiguration, IDisposable
	{
		// Token: 0x060008D3 RID: 2259 RVA: 0x000276E4 File Offset: 0x000258E4
		public bool IsCacheEnabled(string processNameOrProcessAppName)
		{
			if (!Globals.IsDatacenter)
			{
				return false;
			}
			if (!this.Initialize())
			{
				return false;
			}
			this.RefreshIfNecessary();
			return this.isEnabled && !this.exclusiveProcessSet.Contains(processNameOrProcessAppName) && (this.inclusiveProcessSet.Contains(processNameOrProcessAppName) || this.inclusiveProcessSet.Contains("*"));
		}

		// Token: 0x060008D4 RID: 2260 RVA: 0x00027742 File Offset: 0x00025942
		public bool IsCacheEnableForCurrentProcess()
		{
			if (!Globals.IsDatacenter)
			{
				return false;
			}
			if (!this.Initialize())
			{
				return false;
			}
			this.RefreshIfNecessary();
			return this.isCurrentProcessEnabled;
		}

		// Token: 0x060008D5 RID: 2261 RVA: 0x00027763 File Offset: 0x00025963
		public CacheMode GetCacheMode(string processNameOrProcessAppName)
		{
			if (this.IsCacheEnabled(processNameOrProcessAppName))
			{
				return this.GetCacheMode();
			}
			return CacheMode.Disabled;
		}

		// Token: 0x060008D6 RID: 2262 RVA: 0x00027776 File Offset: 0x00025976
		public CacheMode GetCacheModeForCurrentProcess()
		{
			if (this.IsCacheEnableForCurrentProcess())
			{
				return this.GetCacheMode();
			}
			return CacheMode.Disabled;
		}

		// Token: 0x060008D7 RID: 2263 RVA: 0x00027788 File Offset: 0x00025988
		public bool IsCacheEnabled(Type objectType)
		{
			if (!Globals.IsDatacenter)
			{
				return false;
			}
			if (!this.Initialize())
			{
				return false;
			}
			this.RefreshIfNecessary();
			return this.isCurrentProcessEnabled && ((this.isWhiteList && (this.objectTypeSet.Contains("*") || this.objectTypeSet.Contains(objectType.Name))) || (!this.isWhiteList && !this.objectTypeSet.Contains(objectType.Name) && !this.objectTypeSet.Contains("*")));
		}

		// Token: 0x060008D8 RID: 2264 RVA: 0x00027817 File Offset: 0x00025A17
		public bool IsCacheEnabledForInsertOnSave(ADRawEntry rawEntry)
		{
			return this.IsObjectEligibleForRecentlyCreatedBehavior(rawEntry) && this.newObjectBehaviorConfiguration.InsertOnSave;
		}

		// Token: 0x060008D9 RID: 2265 RVA: 0x0002782F File Offset: 0x00025A2F
		public int GetCacheExpirationForObject(ADRawEntry rawEntry)
		{
			if (this.IsObjectEligibleForRecentlyCreatedBehavior(rawEntry))
			{
				return this.newObjectBehaviorConfiguration.TimeToLiveInSeconds;
			}
			return 2147483646;
		}

		// Token: 0x060008DA RID: 2266 RVA: 0x0002784B File Offset: 0x00025A4B
		public CacheItemPriority GetCachePriorityForObject(ADRawEntry rawEntry)
		{
			if (this.IsObjectEligibleForRecentlyCreatedBehavior(rawEntry) && this.newObjectBehaviorConfiguration.InsertWithHigherPriority)
			{
				return CacheItemPriority.NotRemovable;
			}
			return CacheItemPriority.Default;
		}

		// Token: 0x060008DB RID: 2267 RVA: 0x00027866 File Offset: 0x00025A66
		public void Dispose()
		{
			if (this.provider != null)
			{
				this.provider.Dispose();
				this.provider = null;
			}
		}

		// Token: 0x060008DC RID: 2268 RVA: 0x00027884 File Offset: 0x00025A84
		private bool Initialize()
		{
			if (this.provider == null)
			{
				if (Globals.ProcessInstanceType == InstanceType.NotInitialized)
				{
					return false;
				}
				lock (this.lockObj)
				{
					if (this.provider == null)
					{
						this.provider = ConfigProvider.CreateADProvider(new ConfigurationADImpl.ADCacheConfigurationSchema(), null);
						this.provider.Initialize();
					}
				}
			}
			return true;
		}

		// Token: 0x060008DD RID: 2269 RVA: 0x000278FC File Offset: 0x00025AFC
		private string GetProcessOrAppName()
		{
			if (Globals.ProcessName.StartsWith("w3wp", StringComparison.OrdinalIgnoreCase))
			{
				return Globals.ProcessNameAppName;
			}
			return Globals.ProcessName;
		}

		// Token: 0x060008DE RID: 2270 RVA: 0x0002791C File Offset: 0x00025B1C
		private bool IsObjectEligibleForRecentlyCreatedBehavior(ADRawEntry rawEntry)
		{
			if (rawEntry == null)
			{
				return false;
			}
			if (!this.IsCacheEnabled(rawEntry.GetType()))
			{
				return false;
			}
			RecipientTypeDetails? recipientTypeDetails = rawEntry[ADRecipientSchema.RecipientTypeDetails] as RecipientTypeDetails?;
			if (!this.newObjectBehaviorConfiguration.IsRecipientTypeDetailsEnabled(recipientTypeDetails))
			{
				return false;
			}
			DateTime? dateTime = rawEntry[ADObjectSchema.WhenCreatedUTC] as DateTime?;
			return dateTime != null && this.newObjectBehaviorConfiguration.IsDateTimeWithinInclusionThreshold(dateTime.Value);
		}

		// Token: 0x060008DF RID: 2271 RVA: 0x00027998 File Offset: 0x00025B98
		private void RefreshIfNecessary()
		{
			if (this.lastUpdate + TimeSpan.FromMinutes(5.0) >= DateTime.UtcNow)
			{
				return;
			}
			try
			{
				if (Monitor.TryEnter(this.lastUpdateLockObj))
				{
					this.configIsNotFound = !this.provider.TryGetConfig<bool>(ServerSettingsContext.LocalServer, "IsEnabled", out this.isEnabled);
					if (this.configIsNotFound)
					{
						this.isEnabled = ExEnvironment.IsTest;
					}
					this.exclusiveProcessSet = this.GetConfigValues("ExclusiveProcesses");
					this.inclusiveProcessSet = this.GetConfigValues("InclusiveProcesses");
					this.objectTypeSet = this.GetConfigValues("ObjectTypes");
					this.isWhiteList = this.provider.GetConfig<bool>(ServerSettingsContext.LocalServer, "IsWhiteList");
					this.newObjectBehaviorConfiguration = new ConfigurationADImpl.NewObjectCacheBehaviorConfiguration
					{
						EnabledRecipientTypes = this.GetConfigValues("NewObjectCacheRecipientTypes"),
						InclusionThresholdInMinutes = this.provider.GetConfig<int>(ServerSettingsContext.LocalServer, "NewObjectCacheInclusionThresholdMinutes"),
						InsertOnSave = this.provider.GetConfig<bool>(ServerSettingsContext.LocalServer, "NewObjectCacheInsertOnSave"),
						TimeToLiveInMinutes = this.provider.GetConfig<int>(ServerSettingsContext.LocalServer, "NewObjectCacheTimeToLiveMinutes"),
						InsertWithHigherPriority = this.provider.GetConfig<bool>(ServerSettingsContext.LocalServer, "NewObjectCacheWithHighPriority")
					};
					this.isCurrentProcessEnabled = false;
					if (this.isEnabled && Globals.InstanceType != InstanceType.NotInitialized)
					{
						string processOrAppName = this.GetProcessOrAppName();
						if (!"ExSetup.exe".StartsWith(processOrAppName, StringComparison.OrdinalIgnoreCase))
						{
							this.isCurrentProcessEnabled = (!this.exclusiveProcessSet.Contains(processOrAppName) && (this.inclusiveProcessSet.Contains(processOrAppName) || this.inclusiveProcessSet.Contains("*")));
						}
					}
					this.lastUpdate = DateTime.UtcNow;
				}
			}
			finally
			{
				if (Monitor.IsEntered(this.lastUpdateLockObj))
				{
					Monitor.Exit(this.lastUpdateLockObj);
				}
			}
		}

		// Token: 0x060008E0 RID: 2272 RVA: 0x00027B90 File Offset: 0x00025D90
		private HashSet<string> GetConfigValues(string propertyName)
		{
			string config = this.provider.GetConfig<string>(ServerSettingsContext.LocalServer, propertyName);
			if (string.IsNullOrEmpty(config))
			{
				return new HashSet<string>(StringComparer.OrdinalIgnoreCase);
			}
			return new HashSet<string>(config.Split(new char[]
			{
				','
			}), StringComparer.OrdinalIgnoreCase);
		}

		// Token: 0x060008E1 RID: 2273 RVA: 0x00027BE0 File Offset: 0x00025DE0
		private CacheMode GetCacheMode()
		{
			bool flag = false;
			ADDriverContext addriverContext = ADSessionSettings.GetProcessADContext() ?? ADSessionSettings.GetThreadADContext();
			if (addriverContext != null)
			{
				if (ContextMode.Setup == addriverContext.Mode || ContextMode.TopologyService == addriverContext.Mode)
				{
					return CacheMode.Disabled;
				}
				flag = (ContextMode.Cmdlet == addriverContext.Mode);
			}
			if (!flag)
			{
				return CacheMode.Read | CacheMode.SyncWrite;
			}
			return CacheMode.AsyncWrite;
		}

		// Token: 0x040002C2 RID: 706
		private const string ExcludedProcess = "ExSetup.exe";

		// Token: 0x040002C3 RID: 707
		private const string W3wpProcessName = "w3wp";

		// Token: 0x040002C4 RID: 708
		private const int UpdateIntervalInMin = 5;

		// Token: 0x040002C5 RID: 709
		private IConfigProvider provider;

		// Token: 0x040002C6 RID: 710
		private object lockObj = new object();

		// Token: 0x040002C7 RID: 711
		private bool configIsNotFound;

		// Token: 0x040002C8 RID: 712
		private bool isEnabled;

		// Token: 0x040002C9 RID: 713
		private HashSet<string> exclusiveProcessSet = new HashSet<string>(StringComparer.OrdinalIgnoreCase);

		// Token: 0x040002CA RID: 714
		private HashSet<string> inclusiveProcessSet = new HashSet<string>(StringComparer.OrdinalIgnoreCase);

		// Token: 0x040002CB RID: 715
		private HashSet<string> objectTypeSet = new HashSet<string>(StringComparer.OrdinalIgnoreCase);

		// Token: 0x040002CC RID: 716
		private ConfigurationADImpl.NewObjectCacheBehaviorConfiguration newObjectBehaviorConfiguration = new ConfigurationADImpl.NewObjectCacheBehaviorConfiguration();

		// Token: 0x040002CD RID: 717
		private bool isWhiteList;

		// Token: 0x040002CE RID: 718
		private DateTime lastUpdate;

		// Token: 0x040002CF RID: 719
		private bool isCurrentProcessEnabled;

		// Token: 0x040002D0 RID: 720
		private object lastUpdateLockObj = new object();

		// Token: 0x0200009C RID: 156
		internal class NewObjectCacheBehaviorConfiguration
		{
			// Token: 0x060008E3 RID: 2275 RVA: 0x00027C8C File Offset: 0x00025E8C
			internal NewObjectCacheBehaviorConfiguration()
			{
				this.EnabledRecipientTypes = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
			}

			// Token: 0x1700019A RID: 410
			// (get) Token: 0x060008E4 RID: 2276 RVA: 0x00027CA4 File Offset: 0x00025EA4
			// (set) Token: 0x060008E5 RID: 2277 RVA: 0x00027CAC File Offset: 0x00025EAC
			internal HashSet<string> EnabledRecipientTypes { get; set; }

			// Token: 0x1700019B RID: 411
			// (get) Token: 0x060008E6 RID: 2278 RVA: 0x00027CB5 File Offset: 0x00025EB5
			// (set) Token: 0x060008E7 RID: 2279 RVA: 0x00027CC3 File Offset: 0x00025EC3
			internal int InclusionThresholdInMinutes
			{
				get
				{
					return (int)this.inclusionThreshold.TotalMinutes;
				}
				set
				{
					this.inclusionThreshold = TimeSpan.FromMinutes((double)value);
				}
			}

			// Token: 0x1700019C RID: 412
			// (get) Token: 0x060008E8 RID: 2280 RVA: 0x00027CD2 File Offset: 0x00025ED2
			// (set) Token: 0x060008E9 RID: 2281 RVA: 0x00027CDA File Offset: 0x00025EDA
			internal bool InsertOnSave { get; set; }

			// Token: 0x1700019D RID: 413
			// (set) Token: 0x060008EA RID: 2282 RVA: 0x00027CE3 File Offset: 0x00025EE3
			internal int TimeToLiveInMinutes
			{
				set
				{
					this.TimeToLiveInSeconds = value * 60;
				}
			}

			// Token: 0x1700019E RID: 414
			// (get) Token: 0x060008EB RID: 2283 RVA: 0x00027CEF File Offset: 0x00025EEF
			// (set) Token: 0x060008EC RID: 2284 RVA: 0x00027CF7 File Offset: 0x00025EF7
			internal int TimeToLiveInSeconds { get; private set; }

			// Token: 0x1700019F RID: 415
			// (get) Token: 0x060008ED RID: 2285 RVA: 0x00027D00 File Offset: 0x00025F00
			// (set) Token: 0x060008EE RID: 2286 RVA: 0x00027D08 File Offset: 0x00025F08
			internal bool InsertWithHigherPriority { get; set; }

			// Token: 0x060008EF RID: 2287 RVA: 0x00027D11 File Offset: 0x00025F11
			internal bool IsRecipientTypeDetailsEnabled(RecipientTypeDetails? recipientTypeDetails)
			{
				return recipientTypeDetails != null && this.EnabledRecipientTypes.Contains(recipientTypeDetails.ToString());
			}

			// Token: 0x060008F0 RID: 2288 RVA: 0x00027D38 File Offset: 0x00025F38
			internal bool IsDateTimeWithinInclusionThreshold(DateTime dateTime)
			{
				DateTime t = DateTime.UtcNow.Subtract(this.inclusionThreshold);
				return dateTime > t;
			}

			// Token: 0x040002D1 RID: 721
			private TimeSpan inclusionThreshold;
		}

		// Token: 0x0200009D RID: 157
		internal class ADCacheConfigurationSchema : ConfigSchemaBase
		{
			// Token: 0x170001A0 RID: 416
			// (get) Token: 0x060008F1 RID: 2289 RVA: 0x00027D60 File Offset: 0x00025F60
			public override string Name
			{
				get
				{
					return "ADCache";
				}
			}

			// Token: 0x170001A1 RID: 417
			// (get) Token: 0x060008F2 RID: 2290 RVA: 0x00027D67 File Offset: 0x00025F67
			public override string SectionName
			{
				get
				{
					return "ADCacheConfiguration";
				}
			}

			// Token: 0x170001A2 RID: 418
			// (get) Token: 0x060008F3 RID: 2291 RVA: 0x00027D6E File Offset: 0x00025F6E
			// (set) Token: 0x060008F4 RID: 2292 RVA: 0x00027D80 File Offset: 0x00025F80
			[ConfigurationProperty("IsEnabled", DefaultValue = false)]
			public bool IsEnabled
			{
				get
				{
					return (bool)base["IsEnabled"];
				}
				set
				{
					base["IsEnabled"] = value;
				}
			}

			// Token: 0x170001A3 RID: 419
			// (get) Token: 0x060008F5 RID: 2293 RVA: 0x00027D93 File Offset: 0x00025F93
			// (set) Token: 0x060008F6 RID: 2294 RVA: 0x00027DA5 File Offset: 0x00025FA5
			[ConfigurationProperty("ExclusiveProcesses", DefaultValue = "Microsoft.Exchange.Management.ForwardSync.exe,Microsoft.Exchange.ServiceHost.exe,wsmprovhost.exe,MSExchangeMailboxReplication.exe,Microsoft.Exchange.Directory.TopologyService.exe")]
			public string ExclusiveProcesses
			{
				get
				{
					return (string)base["ExclusiveProcesses"];
				}
				set
				{
					base["ExclusiveProcesses"] = value;
				}
			}

			// Token: 0x170001A4 RID: 420
			// (get) Token: 0x060008F7 RID: 2295 RVA: 0x00027DB3 File Offset: 0x00025FB3
			// (set) Token: 0x060008F8 RID: 2296 RVA: 0x00027DC5 File Offset: 0x00025FC5
			[ConfigurationProperty("InclusiveProcesses", DefaultValue = "*")]
			public string InclusiveProcesses
			{
				get
				{
					return (string)base["InclusiveProcesses"];
				}
				set
				{
					base["InclusiveProcesses"] = value;
				}
			}

			// Token: 0x170001A5 RID: 421
			// (get) Token: 0x060008F9 RID: 2297 RVA: 0x00027DD3 File Offset: 0x00025FD3
			// (set) Token: 0x060008FA RID: 2298 RVA: 0x00027DE5 File Offset: 0x00025FE5
			[ConfigurationProperty("ObjectTypes", DefaultValue = "")]
			public string ObjectTypes
			{
				get
				{
					return (string)base["ObjectTypes"];
				}
				set
				{
					base["ObjectTypes"] = value;
				}
			}

			// Token: 0x170001A6 RID: 422
			// (get) Token: 0x060008FB RID: 2299 RVA: 0x00027DF3 File Offset: 0x00025FF3
			// (set) Token: 0x060008FC RID: 2300 RVA: 0x00027E05 File Offset: 0x00026005
			[ConfigurationProperty("IsWhiteList", DefaultValue = false)]
			public bool IsWhiteList
			{
				get
				{
					return (bool)base["IsWhiteList"];
				}
				set
				{
					base["IsWhiteList"] = value;
				}
			}

			// Token: 0x170001A7 RID: 423
			// (get) Token: 0x060008FD RID: 2301 RVA: 0x00027E18 File Offset: 0x00026018
			// (set) Token: 0x060008FE RID: 2302 RVA: 0x00027E2A File Offset: 0x0002602A
			[ConfigurationProperty("NewObjectCacheRecipientTypes", DefaultValue = "")]
			public string NewObjectCacheRecipientTypes
			{
				get
				{
					return (string)base["NewObjectCacheRecipientTypes"];
				}
				set
				{
					base["NewObjectCacheRecipientTypes"] = value;
				}
			}

			// Token: 0x170001A8 RID: 424
			// (get) Token: 0x060008FF RID: 2303 RVA: 0x00027E38 File Offset: 0x00026038
			// (set) Token: 0x06000900 RID: 2304 RVA: 0x00027E4A File Offset: 0x0002604A
			[ConfigurationProperty("NewObjectCacheInclusionThresholdMinutes", DefaultValue = 15)]
			public int NewObjectCacheInclusionThresholdMinutes
			{
				get
				{
					return (int)base["NewObjectCacheInclusionThresholdMinutes"];
				}
				set
				{
					base["NewObjectCacheInclusionThresholdMinutes"] = value;
				}
			}

			// Token: 0x170001A9 RID: 425
			// (get) Token: 0x06000901 RID: 2305 RVA: 0x00027E5D File Offset: 0x0002605D
			// (set) Token: 0x06000902 RID: 2306 RVA: 0x00027E6F File Offset: 0x0002606F
			[ConfigurationProperty("NewObjectCacheInsertOnSave", DefaultValue = true)]
			public bool NewObjectCacheInsertOnSave
			{
				get
				{
					return (bool)base["NewObjectCacheInsertOnSave"];
				}
				set
				{
					base["NewObjectCacheInsertOnSave"] = value;
				}
			}

			// Token: 0x170001AA RID: 426
			// (get) Token: 0x06000903 RID: 2307 RVA: 0x00027E82 File Offset: 0x00026082
			// (set) Token: 0x06000904 RID: 2308 RVA: 0x00027E94 File Offset: 0x00026094
			[ConfigurationProperty("NewObjectCacheTimeToLiveMinutes", DefaultValue = 15)]
			public int NewObjectCacheTimeToLiveMinutes
			{
				get
				{
					return (int)base["NewObjectCacheTimeToLiveMinutes"];
				}
				set
				{
					base["NewObjectCacheTimeToLiveMinutes"] = value;
				}
			}

			// Token: 0x170001AB RID: 427
			// (get) Token: 0x06000905 RID: 2309 RVA: 0x00027EA7 File Offset: 0x000260A7
			// (set) Token: 0x06000906 RID: 2310 RVA: 0x00027EB9 File Offset: 0x000260B9
			[ConfigurationProperty("NewObjectCacheWithHighPriority", DefaultValue = true)]
			public bool NewObjectCacheWithHighPriority
			{
				get
				{
					return (bool)base["NewObjectCacheWithHighPriority"];
				}
				set
				{
					base["NewObjectCacheWithHighPriority"] = value;
				}
			}

			// Token: 0x06000907 RID: 2311 RVA: 0x00027ECC File Offset: 0x000260CC
			protected override bool OnDeserializeUnrecognizedAttribute(string name, string value)
			{
				return base.OnDeserializeUnrecognizedAttribute(name, value);
			}

			// Token: 0x0200009E RID: 158
			public static class Constants
			{
				// Token: 0x040002D6 RID: 726
				public const string SchemaName = "ADCache";

				// Token: 0x040002D7 RID: 727
				public const string IsEnabled = "IsEnabled";

				// Token: 0x040002D8 RID: 728
				public const string ExclusiveProcesses = "ExclusiveProcesses";

				// Token: 0x040002D9 RID: 729
				public const string InclusiveProcesses = "InclusiveProcesses";

				// Token: 0x040002DA RID: 730
				public const string ObjectTypes = "ObjectTypes";

				// Token: 0x040002DB RID: 731
				public const string IsWhiteList = "IsWhiteList";

				// Token: 0x040002DC RID: 732
				public const string NewObjectCacheRecipientTypes = "NewObjectCacheRecipientTypes";

				// Token: 0x040002DD RID: 733
				public const string NewObjectCacheInclusionThresholdMinutes = "NewObjectCacheInclusionThresholdMinutes";

				// Token: 0x040002DE RID: 734
				public const string NewObjectCacheInsertOnSave = "NewObjectCacheInsertOnSave";

				// Token: 0x040002DF RID: 735
				public const string NewObjectCacheTimeToLiveMinutes = "NewObjectCacheTimeToLiveMinutes";

				// Token: 0x040002E0 RID: 736
				public const string NewObjectCacheWithHighPriority = "NewObjectCacheWithHighPriority";
			}
		}
	}
}
