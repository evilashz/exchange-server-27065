using System;
using System.Collections.Generic;
using Microsoft.Exchange.Configuration.Authorization;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.WorkloadManagement;
using Microsoft.Exchange.EDiscovery.MailboxSearch.WebService.Model;
using Microsoft.Exchange.Flighting;
using Microsoft.Exchange.InfoWorker.Common.MultiMailboxSearch;
using Microsoft.Exchange.VariantConfiguration;
using Microsoft.Win32;

namespace Microsoft.Exchange.EDiscovery.MailboxSearch.WebService.Infrastructure
{
	// Token: 0x02000034 RID: 52
	internal class SearchPolicy : ISearchPolicy
	{
		// Token: 0x06000268 RID: 616 RVA: 0x00013258 File Offset: 0x00011458
		public SearchPolicy(IRecipientSession recipientSession, CallerInfo callerInfo, ExchangeRunspaceConfiguration runspaceConfiguration, IBudget budget = null)
		{
			if (recipientSession == null)
			{
				throw new ArgumentNullException("recipientSession");
			}
			if (callerInfo == null)
			{
				throw new ArgumentNullException("callerInfo");
			}
			this.CallerInfo = callerInfo;
			this.RecipientSession = recipientSession;
			this.RunspaceConfiguration = runspaceConfiguration;
			this.ThrottlingSettings = new SearchPolicy.ThrottlingPolicySettings(this);
			this.ExecutionSettings = new SearchPolicy.ExecutionPolicySettings(this);
			this.Recorder = new Recorder(this);
			this.Budget = budget;
		}

		// Token: 0x17000098 RID: 152
		// (get) Token: 0x06000269 RID: 617 RVA: 0x000132C8 File Offset: 0x000114C8
		// (set) Token: 0x0600026A RID: 618 RVA: 0x000132D0 File Offset: 0x000114D0
		public CallerInfo CallerInfo { get; private set; }

		// Token: 0x17000099 RID: 153
		// (get) Token: 0x0600026B RID: 619 RVA: 0x000132D9 File Offset: 0x000114D9
		// (set) Token: 0x0600026C RID: 620 RVA: 0x000132E1 File Offset: 0x000114E1
		public IRecipientSession RecipientSession { get; private set; }

		// Token: 0x1700009A RID: 154
		// (get) Token: 0x0600026D RID: 621 RVA: 0x000132EA File Offset: 0x000114EA
		// (set) Token: 0x0600026E RID: 622 RVA: 0x000132F2 File Offset: 0x000114F2
		public ExchangeRunspaceConfiguration RunspaceConfiguration { get; private set; }

		// Token: 0x1700009B RID: 155
		// (get) Token: 0x0600026F RID: 623 RVA: 0x000132FB File Offset: 0x000114FB
		// (set) Token: 0x06000270 RID: 624 RVA: 0x00013303 File Offset: 0x00011503
		public IThrottlingSettings ThrottlingSettings { get; private set; }

		// Token: 0x1700009C RID: 156
		// (get) Token: 0x06000271 RID: 625 RVA: 0x0001330C File Offset: 0x0001150C
		// (set) Token: 0x06000272 RID: 626 RVA: 0x00013314 File Offset: 0x00011514
		public IExecutionSettings ExecutionSettings { get; private set; }

		// Token: 0x1700009D RID: 157
		// (get) Token: 0x06000273 RID: 627 RVA: 0x0001331D File Offset: 0x0001151D
		// (set) Token: 0x06000274 RID: 628 RVA: 0x00013325 File Offset: 0x00011525
		public IBudget Budget { get; private set; }

		// Token: 0x1700009E RID: 158
		// (get) Token: 0x06000275 RID: 629 RVA: 0x0001332E File Offset: 0x0001152E
		// (set) Token: 0x06000276 RID: 630 RVA: 0x00013336 File Offset: 0x00011536
		public Recorder Recorder { get; private set; }

		// Token: 0x06000277 RID: 631 RVA: 0x0001333F File Offset: 0x0001153F
		public ActivityScope GetActivityScope()
		{
			return ActivityContext.Start(null);
		}

		// Token: 0x02000035 RID: 53
		public static class ServiceName
		{
			// Token: 0x0400012A RID: 298
			public const string GetSearchableMailboxes = "GetSearchableMailboxes";

			// Token: 0x0400012B RID: 299
			public const string SearchMailboxes = "SearchMailboxes";
		}

		// Token: 0x02000037 RID: 55
		internal class ThrottlingPolicySettings : IThrottlingSettings
		{
			// Token: 0x06000282 RID: 642 RVA: 0x00013347 File Offset: 0x00011547
			public ThrottlingPolicySettings(ISearchPolicy policy)
			{
				if (policy == null)
				{
					throw new ArgumentNullException("policy");
				}
				this.throttlingPolicy = SearchFactory.Current.GetThrottlingPolicy(policy);
			}

			// Token: 0x170000A9 RID: 169
			// (get) Token: 0x06000283 RID: 643 RVA: 0x00013370 File Offset: 0x00011570
			public uint DiscoveryMaxConcurrency
			{
				get
				{
					if (!this.throttlingPolicy.DiscoveryMaxConcurrency.IsUnlimited)
					{
						return this.throttlingPolicy.DiscoveryMaxConcurrency.Value;
					}
					Recorder.Trace(2L, TraceType.WarningTrace, "ThrottlingPolicySettings.DiscoveryMaxConcurrency Fallback");
					return ThrottlingPolicyDefaults.DiscoveryMaxConcurrency.Value;
				}
			}

			// Token: 0x170000AA RID: 170
			// (get) Token: 0x06000284 RID: 644 RVA: 0x000133C0 File Offset: 0x000115C0
			public uint DiscoveryMaxKeywords
			{
				get
				{
					if (!this.throttlingPolicy.DiscoveryMaxKeywords.IsUnlimited)
					{
						return this.throttlingPolicy.DiscoveryMaxKeywords.Value;
					}
					Recorder.Trace(2L, TraceType.WarningTrace, "ThrottlingPolicySettings.DiscoveryMaxKeywords Fallback");
					return ThrottlingPolicyDefaults.DiscoveryMaxKeywords.Value;
				}
			}

			// Token: 0x170000AB RID: 171
			// (get) Token: 0x06000285 RID: 645 RVA: 0x00013410 File Offset: 0x00011610
			public uint DiscoveryMaxKeywordsPerPage
			{
				get
				{
					if (!this.throttlingPolicy.DiscoveryMaxKeywordsPerPage.IsUnlimited)
					{
						return this.throttlingPolicy.DiscoveryMaxKeywordsPerPage.Value;
					}
					Recorder.Trace(2L, TraceType.WarningTrace, "ThrottlingPolicySettings.DiscoveryMaxKeywordsPerPage Fallback");
					return ThrottlingPolicyDefaults.DiscoveryMaxKeywordsPerPage.Value;
				}
			}

			// Token: 0x170000AC RID: 172
			// (get) Token: 0x06000286 RID: 646 RVA: 0x00013460 File Offset: 0x00011660
			public uint DiscoveryMaxMailboxes
			{
				get
				{
					if (!this.throttlingPolicy.DiscoveryMaxMailboxes.IsUnlimited)
					{
						return this.throttlingPolicy.DiscoveryMaxMailboxes.Value;
					}
					Recorder.Trace(2L, TraceType.WarningTrace, "ThrottlingPolicySettings.DiscoveryMaxMailboxes Fallback");
					return ThrottlingPolicyDefaults.DiscoveryMaxMailboxes.Value;
				}
			}

			// Token: 0x170000AD RID: 173
			// (get) Token: 0x06000287 RID: 647 RVA: 0x000134B0 File Offset: 0x000116B0
			public uint DiscoveryMaxPreviewSearchMailboxes
			{
				get
				{
					if (!this.throttlingPolicy.DiscoveryMaxPreviewSearchMailboxes.IsUnlimited)
					{
						return this.throttlingPolicy.DiscoveryMaxPreviewSearchMailboxes.Value;
					}
					Recorder.Trace(2L, TraceType.WarningTrace, "ThrottlingPolicySettings.DiscoveryMaxPreviewSearchMailboxes Fallback");
					return ThrottlingPolicyDefaults.DiscoveryMaxPreviewSearchMailboxes.Value;
				}
			}

			// Token: 0x170000AE RID: 174
			// (get) Token: 0x06000288 RID: 648 RVA: 0x00013500 File Offset: 0x00011700
			public uint DiscoveryMaxRefinerResults
			{
				get
				{
					if (!this.throttlingPolicy.DiscoveryMaxRefinerResults.IsUnlimited)
					{
						return this.throttlingPolicy.DiscoveryMaxRefinerResults.Value;
					}
					Recorder.Trace(2L, TraceType.WarningTrace, "ThrottlingPolicySettings.DiscoveryMaxRefinerResults Fallback");
					return ThrottlingPolicyDefaults.DiscoveryMaxRefinerResults.Value;
				}
			}

			// Token: 0x170000AF RID: 175
			// (get) Token: 0x06000289 RID: 649 RVA: 0x00013550 File Offset: 0x00011750
			public uint DiscoveryMaxSearchQueueDepth
			{
				get
				{
					if (!this.throttlingPolicy.DiscoveryMaxSearchQueueDepth.IsUnlimited)
					{
						return this.throttlingPolicy.DiscoveryMaxSearchQueueDepth.Value;
					}
					Recorder.Trace(2L, TraceType.WarningTrace, "ThrottlingPolicySettings.DiscoveryMaxSearchQueueDepth Fallback");
					return ThrottlingPolicyDefaults.DiscoveryMaxSearchQueueDepth.Value;
				}
			}

			// Token: 0x170000B0 RID: 176
			// (get) Token: 0x0600028A RID: 650 RVA: 0x000135A0 File Offset: 0x000117A0
			public uint DiscoveryMaxStatsSearchMailboxes
			{
				get
				{
					if (!this.throttlingPolicy.DiscoveryMaxStatsSearchMailboxes.IsUnlimited)
					{
						return this.throttlingPolicy.DiscoveryMaxStatsSearchMailboxes.Value;
					}
					Recorder.Trace(2L, TraceType.WarningTrace, "ThrottlingPolicySettings.DiscoveryMaxStatsSearchMailboxes Fallback");
					return ThrottlingPolicyDefaults.DiscoveryMaxStatsSearchMailboxes.Value;
				}
			}

			// Token: 0x170000B1 RID: 177
			// (get) Token: 0x0600028B RID: 651 RVA: 0x000135F0 File Offset: 0x000117F0
			public uint DiscoveryPreviewSearchResultsPageSize
			{
				get
				{
					if (!this.throttlingPolicy.DiscoveryPreviewSearchResultsPageSize.IsUnlimited)
					{
						return this.throttlingPolicy.DiscoveryPreviewSearchResultsPageSize.Value;
					}
					Recorder.Trace(2L, TraceType.WarningTrace, "ThrottlingPolicySettings.DiscoveryPreviewSearchResultsPageSize Fallback");
					return ThrottlingPolicyDefaults.DiscoveryPreviewSearchResultsPageSize.Value;
				}
			}

			// Token: 0x170000B2 RID: 178
			// (get) Token: 0x0600028C RID: 652 RVA: 0x00013640 File Offset: 0x00011840
			public uint DiscoverySearchTimeoutPeriod
			{
				get
				{
					if (!this.throttlingPolicy.DiscoverySearchTimeoutPeriod.IsUnlimited)
					{
						return this.throttlingPolicy.DiscoverySearchTimeoutPeriod.Value;
					}
					Recorder.Trace(2L, TraceType.WarningTrace, "ThrottlingPolicySettings.DiscoverySearchTimeoutPeriod Fallback");
					return ThrottlingPolicyDefaults.DiscoverySearchTimeoutPeriod.Value;
				}
			}

			// Token: 0x0400012C RID: 300
			private IThrottlingPolicy throttlingPolicy;
		}

		// Token: 0x02000039 RID: 57
		internal class ExecutionPolicySettings : IExecutionSettings
		{
			// Token: 0x060002A7 RID: 679 RVA: 0x00013690 File Offset: 0x00011890
			public ExecutionPolicySettings(ISearchPolicy policy)
			{
				if (policy == null)
				{
					throw new ArgumentNullException("policy");
				}
				this.policy = policy;
				this.Snapshot = SearchFactory.Current.GetVariantConfigurationSnapshot(policy);
				this.settingsMap = this.Snapshot.Discovery.GetObjectsOfType<ISettingsValue>();
				this.useRegDiscoveryUseFastSearch = this.LookupRegBool("DiscoveryUseFastSearch", out this.regDiscoveryUseFastSearch);
			}

			// Token: 0x170000CD RID: 205
			// (get) Token: 0x060002A8 RID: 680 RVA: 0x00013704 File Offset: 0x00011904
			// (set) Token: 0x060002A9 RID: 681 RVA: 0x0001370C File Offset: 0x0001190C
			public VariantConfigurationSnapshot Snapshot { get; private set; }

			// Token: 0x170000CE RID: 206
			// (get) Token: 0x060002AA RID: 682 RVA: 0x00013715 File Offset: 0x00011915
			public bool DiscoveryUseFastSearch
			{
				get
				{
					if (this.useRegDiscoveryUseFastSearch)
					{
						return this.regDiscoveryUseFastSearch;
					}
					return this.GetBool("DiscoveryUseFastSearch", false);
				}
			}

			// Token: 0x170000CF RID: 207
			// (get) Token: 0x060002AB RID: 683 RVA: 0x00013732 File Offset: 0x00011932
			public bool DiscoveryAggregateLogs
			{
				get
				{
					return this.GetBool("DiscoveryAggregateLogs", false);
				}
			}

			// Token: 0x170000D0 RID: 208
			// (get) Token: 0x060002AC RID: 684 RVA: 0x00013740 File Offset: 0x00011940
			public bool DiscoveryExecutesInParallel
			{
				get
				{
					return this.GetBool("DiscoveryExecutesInParallel", true);
				}
			}

			// Token: 0x170000D1 RID: 209
			// (get) Token: 0x060002AD RID: 685 RVA: 0x0001374E File Offset: 0x0001194E
			public bool DiscoveryLocalSearchIsParallel
			{
				get
				{
					return this.GetBool("DiscoveryLocalSearchIsParallel", false);
				}
			}

			// Token: 0x170000D2 RID: 210
			// (get) Token: 0x060002AE RID: 686 RVA: 0x0001375C File Offset: 0x0001195C
			public int DiscoveryMaxMailboxes
			{
				get
				{
					return (int)this.GetNumber("DiscoveryMaxMailboxes", this.policy.ThrottlingSettings.DiscoveryMaxMailboxes);
				}
			}

			// Token: 0x170000D3 RID: 211
			// (get) Token: 0x060002AF RID: 687 RVA: 0x0001377C File Offset: 0x0001197C
			public int DiscoveryKeywordsBatchSize
			{
				get
				{
					return (int)this.GetNumber("DiscoveryKeywordsBatchSize", 6.0);
				}
			}

			// Token: 0x170000D4 RID: 212
			// (get) Token: 0x060002B0 RID: 688 RVA: 0x00013793 File Offset: 0x00011993
			public int DiscoveryDefaultPageSize
			{
				get
				{
					return (int)this.GetNumber("DiscoveryDefaultPageSize", 100.0);
				}
			}

			// Token: 0x170000D5 RID: 213
			// (get) Token: 0x060002B1 RID: 689 RVA: 0x000137AA File Offset: 0x000119AA
			public uint DiscoveryMaxAllowedExecutorItems
			{
				get
				{
					return (uint)this.GetNumber("DiscoveryMaxAllowedExecutorItems", 30000.0);
				}
			}

			// Token: 0x170000D6 RID: 214
			// (get) Token: 0x060002B2 RID: 690 RVA: 0x000137C1 File Offset: 0x000119C1
			public int DiscoveryMaxAllowedMailboxQueriesPerRequest
			{
				get
				{
					return (int)this.GetNumber("DiscoveryMaxAllowedMailboxQueriesPerRequest", 5.0);
				}
			}

			// Token: 0x170000D7 RID: 215
			// (get) Token: 0x060002B3 RID: 691 RVA: 0x000137D8 File Offset: 0x000119D8
			public int DiscoveryMaxAllowedResultsPageSize
			{
				get
				{
					return (int)this.GetNumber("DiscoveryMaxAllowedResultsPageSize", 500.0);
				}
			}

			// Token: 0x170000D8 RID: 216
			// (get) Token: 0x060002B4 RID: 692 RVA: 0x000137EF File Offset: 0x000119EF
			public uint DiscoveryADPageSize
			{
				get
				{
					return (uint)this.GetNumber("DiscoveryADPageSize", 50.0);
				}
			}

			// Token: 0x170000D9 RID: 217
			// (get) Token: 0x060002B5 RID: 693 RVA: 0x00013806 File Offset: 0x00011A06
			public uint DiscoveryADLookupConcurrency
			{
				get
				{
					return (uint)this.GetNumber("DiscoveryADLookupConcurrency", 4.0);
				}
			}

			// Token: 0x170000DA RID: 218
			// (get) Token: 0x060002B6 RID: 694 RVA: 0x0001381D File Offset: 0x00011A1D
			public uint DiscoveryServerLookupConcurrency
			{
				get
				{
					return (uint)this.GetNumber("DiscoveryServerLookupConcurrency", 4.0);
				}
			}

			// Token: 0x170000DB RID: 219
			// (get) Token: 0x060002B7 RID: 695 RVA: 0x00013834 File Offset: 0x00011A34
			public uint DiscoveryServerLookupBatch
			{
				get
				{
					return (uint)this.GetNumber("DiscoveryServerLookupBatch", 30.0);
				}
			}

			// Token: 0x170000DC RID: 220
			// (get) Token: 0x060002B8 RID: 696 RVA: 0x0001384B File Offset: 0x00011A4B
			public uint DiscoveryFanoutConcurrency
			{
				get
				{
					return (uint)this.GetNumber("DiscoveryFanoutConcurrency", 100.0);
				}
			}

			// Token: 0x170000DD RID: 221
			// (get) Token: 0x060002B9 RID: 697 RVA: 0x00013862 File Offset: 0x00011A62
			public uint DiscoveryFanoutBatch
			{
				get
				{
					return (uint)this.GetNumber("DiscoveryFanoutBatch", 50.0);
				}
			}

			// Token: 0x170000DE RID: 222
			// (get) Token: 0x060002BA RID: 698 RVA: 0x00013879 File Offset: 0x00011A79
			public uint DiscoveryLocalSearchConcurrency
			{
				get
				{
					return (uint)this.GetNumber("DiscoveryLocalSearchConcurrency", 50.0);
				}
			}

			// Token: 0x170000DF RID: 223
			// (get) Token: 0x060002BB RID: 699 RVA: 0x00013890 File Offset: 0x00011A90
			public uint DiscoveryLocalSearchBatch
			{
				get
				{
					return (uint)this.GetNumber("DiscoveryLocalSearchBatchSize", 4294967295.0);
				}
			}

			// Token: 0x170000E0 RID: 224
			// (get) Token: 0x060002BC RID: 700 RVA: 0x000138A7 File Offset: 0x00011AA7
			public uint DiscoverySynchronousConcurrency
			{
				get
				{
					return 0U;
				}
			}

			// Token: 0x170000E1 RID: 225
			// (get) Token: 0x060002BD RID: 701 RVA: 0x000138AA File Offset: 0x00011AAA
			public uint DiscoveryDisplaySearchBatchSize
			{
				get
				{
					return (uint)this.GetNumber("DiscoveryDisplaySearchBatchSize", 50.0);
				}
			}

			// Token: 0x170000E2 RID: 226
			// (get) Token: 0x060002BE RID: 702 RVA: 0x000138C1 File Offset: 0x00011AC1
			public uint DiscoveryDisplaySearchPageSize
			{
				get
				{
					return (uint)this.GetNumber("DiscoveryDisplaySearchPageSize", 1500.0);
				}
			}

			// Token: 0x170000E3 RID: 227
			// (get) Token: 0x060002BF RID: 703 RVA: 0x000138D8 File Offset: 0x00011AD8
			public TimeSpan SearchTimeout
			{
				get
				{
					return TimeSpan.FromMinutes(this.GetNumber("SearchTimeout", 4.5));
				}
			}

			// Token: 0x170000E4 RID: 228
			// (get) Token: 0x060002C0 RID: 704 RVA: 0x000138F3 File Offset: 0x00011AF3
			public TimeSpan ServiceTopologyTimeout
			{
				get
				{
					return TimeSpan.FromSeconds(this.GetNumber("ServiceTopologyTimeout", 10.0));
				}
			}

			// Token: 0x170000E5 RID: 229
			// (get) Token: 0x060002C1 RID: 705 RVA: 0x0001390E File Offset: 0x00011B0E
			public TimeSpan MailboxServerLocatorTimeout
			{
				get
				{
					return TimeSpan.FromSeconds(this.GetNumber("MailboxServerLocatorTimeout", 30.0));
				}
			}

			// Token: 0x170000E6 RID: 230
			// (get) Token: 0x060002C2 RID: 706 RVA: 0x00013929 File Offset: 0x00011B29
			public List<DefaultFolderType> ExcludedFolders
			{
				get
				{
					if (this.excludedFolders == null)
					{
						if (this.GetBool("DiscoveryExcludedFoldersEnabled", false))
						{
							this.excludedFolders = this.GetEnumList<DefaultFolderType>("DiscoveryExcludedFolders");
						}
						else
						{
							this.excludedFolders = new List<DefaultFolderType>();
						}
					}
					return this.excludedFolders;
				}
			}

			// Token: 0x060002C3 RID: 707 RVA: 0x00013968 File Offset: 0x00011B68
			private double GetNumber(string key, double fallback)
			{
				Recorder.Trace(2L, TraceType.InfoTrace, new object[]
				{
					"ExecutionPolicySettings.GetNumber Key:",
					key,
					"Fallback:",
					fallback
				});
				if (this.settingsMap.ContainsKey(key))
				{
					Recorder.Trace(2L, TraceType.InfoTrace, "ExecutionPolicySettings.GetNumber Found Setting Key:", key);
					double num;
					if (double.TryParse(this.settingsMap[key].Value, out num))
					{
						if (num == -1.0)
						{
							num = 4294967295.0;
						}
						Recorder.Trace(2L, TraceType.InfoTrace, new object[]
						{
							"ExecutionPolicySettings.GetNumber Found Setting Key:",
							key,
							"Value:",
							num
						});
						return num;
					}
				}
				return fallback;
			}

			// Token: 0x060002C4 RID: 708 RVA: 0x00013A20 File Offset: 0x00011C20
			private bool GetBool(string key, bool fallback)
			{
				Recorder.Trace(2L, TraceType.InfoTrace, new object[]
				{
					"ExecutionPolicySettings.GetBool Key:",
					key,
					"Fallback:",
					fallback
				});
				if (this.settingsMap.ContainsKey(key))
				{
					Recorder.Trace(2L, TraceType.InfoTrace, "ExecutionPolicySettings.GetBool Found Setting Key:", key);
					bool flag;
					if (bool.TryParse(this.settingsMap[key].Value, out flag))
					{
						Recorder.Trace(2L, TraceType.InfoTrace, new object[]
						{
							"ExecutionPolicySettings.GetBool Found Setting Key:",
							key,
							"Value:",
							flag
						});
						return flag;
					}
				}
				return fallback;
			}

			// Token: 0x060002C5 RID: 709 RVA: 0x00013AC0 File Offset: 0x00011CC0
			private List<T> GetEnumList<T>(string key) where T : struct, IConvertible
			{
				Recorder.Trace(2L, TraceType.InfoTrace, "ExecutionPolicySettings.GetList Key:", key);
				if (!typeof(T).IsEnum)
				{
					throw new ArgumentException("Specified type is not an enum");
				}
				List<T> list = new List<T>();
				if (this.settingsMap.ContainsKey(key))
				{
					string value = this.settingsMap[key].Value;
					Recorder.Trace(2L, TraceType.InfoTrace, new object[]
					{
						"ExecutionPolicySettings.GetList Setting Key:",
						key,
						"Value:",
						(value == null) ? string.Empty : value
					});
					if (!string.IsNullOrWhiteSpace(value))
					{
						string[] array = value.Split(new char[]
						{
							','
						});
						foreach (string text in array)
						{
							T item;
							if (Enum.TryParse<T>(text, true, out item))
							{
								list.Add(item);
							}
							else
							{
								Recorder.Trace(2L, TraceType.InfoTrace, "ExecutionPolicySettings.GetList invalid enum value:", text);
							}
						}
					}
					else
					{
						Recorder.Trace(2L, TraceType.InfoTrace, "ExecutionPolicySettings.GetList Setting key not found:", key);
					}
				}
				return list;
			}

			// Token: 0x060002C6 RID: 710 RVA: 0x00013BC8 File Offset: 0x00011DC8
			private bool LookupRegBool(string name, out bool boolValue)
			{
				boolValue = false;
				bool result;
				try
				{
					using (RegistryKey registryKey = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, Environment.Is64BitOperatingSystem ? RegistryView.Registry64 : RegistryView.Registry32))
					{
						using (RegistryKey registryKey2 = registryKey.OpenSubKey("SOFTWARE\\Microsoft\\ExchangeServer\\v15\\eDiscovery"))
						{
							if (registryKey2 != null)
							{
								string text = registryKey2.GetValue(name) as string;
								bool flag;
								if (text == null || !bool.TryParse(text, out flag))
								{
									result = false;
								}
								else
								{
									boolValue = flag;
									result = true;
								}
							}
							else
							{
								Recorder.Trace(2L, TraceType.InfoTrace, "SearchPolicySettings.LookupRegBool: Registry not found for constants");
								result = false;
							}
						}
					}
				}
				catch (Exception arg)
				{
					Recorder.Trace(2L, TraceType.InfoTrace, string.Format("SearchPolicySettings.LookupRegBool: Failed to load registry data. Details: {0}", arg));
					result = false;
				}
				return result;
			}

			// Token: 0x0400012D RID: 301
			private readonly bool useRegDiscoveryUseFastSearch;

			// Token: 0x0400012E RID: 302
			private IDictionary<string, ISettingsValue> settingsMap = new Dictionary<string, ISettingsValue>();

			// Token: 0x0400012F RID: 303
			private List<DefaultFolderType> excludedFolders;

			// Token: 0x04000130 RID: 304
			private ISearchPolicy policy;

			// Token: 0x04000131 RID: 305
			private bool regDiscoveryUseFastSearch;
		}
	}
}
