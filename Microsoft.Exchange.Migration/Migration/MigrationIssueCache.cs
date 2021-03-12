using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory.SystemConfiguration.ConfigurationSettings;
using Microsoft.Exchange.Data.Storage.Management;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Migration
{
	// Token: 0x02000016 RID: 22
	internal class MigrationIssueCache : ServiceIssueCache
	{
		// Token: 0x06000075 RID: 117 RVA: 0x000037B4 File Offset: 0x000019B4
		public MigrationIssueCache(Func<IMigrationJobCache> getJobCache)
		{
			this.GetJobCache = getJobCache;
		}

		// Token: 0x1700001E RID: 30
		// (get) Token: 0x06000076 RID: 118 RVA: 0x000037C3 File Offset: 0x000019C3
		protected override string ComponentName
		{
			get
			{
				return "MigrationIssueCache";
			}
		}

		// Token: 0x1700001F RID: 31
		// (get) Token: 0x06000077 RID: 119 RVA: 0x000037CA File Offset: 0x000019CA
		public override bool ScanningIsEnabled
		{
			get
			{
				return ConfigBase<MigrationServiceConfigSchema>.GetConfig<bool>("IssueCacheIsEnabled");
			}
		}

		// Token: 0x17000020 RID: 32
		// (get) Token: 0x06000078 RID: 120 RVA: 0x000037D6 File Offset: 0x000019D6
		protected override int IssueLimit
		{
			get
			{
				return ConfigBase<MigrationServiceConfigSchema>.GetConfig<int>("IssueCacheItemLimit");
			}
		}

		// Token: 0x17000021 RID: 33
		// (get) Token: 0x06000079 RID: 121 RVA: 0x000037E2 File Offset: 0x000019E2
		protected override TimeSpan FullScanFrequency
		{
			get
			{
				return ConfigBase<MigrationServiceConfigSchema>.GetConfig<TimeSpan>("IssueCacheScanFrequency");
			}
		}

		// Token: 0x17000022 RID: 34
		// (get) Token: 0x0600007A RID: 122 RVA: 0x000037EE File Offset: 0x000019EE
		// (set) Token: 0x0600007B RID: 123 RVA: 0x000037F6 File Offset: 0x000019F6
		private Func<IMigrationJobCache> GetJobCache { get; set; }

		// Token: 0x0600007C RID: 124 RVA: 0x00003800 File Offset: 0x00001A00
		protected override ICollection<ServiceIssue> RunFullIssueScan()
		{
			ICollection<ServiceIssue> collection = new List<ServiceIssue>();
			List<MigrationCacheEntry> list = this.GetJobCache().Get();
			foreach (MigrationCacheEntry migrationCacheEntry in list)
			{
				try
				{
					using (IMigrationDataProvider migrationDataProvider = MigrationServiceFactory.Instance.CreateProviderForMigrationMailbox(migrationCacheEntry.TenantPartitionHint, migrationCacheEntry.MigrationMailboxLegacyDN))
					{
						IEnumerable<MigrationJob> byStatus = MigrationJob.GetByStatus(migrationDataProvider, null, MigrationJobStatus.Corrupted);
						foreach (MigrationJob job in byStatus)
						{
							collection.Add(new MigrationJobIssue(job));
						}
						IEnumerable<MigrationJobItem> byStatus2 = MigrationJobItem.GetByStatus(migrationDataProvider, null, MigrationUserStatus.Corrupted, null);
						foreach (MigrationJobItem jobItem in byStatus2)
						{
							collection.Add(new MigrationJobItemIssue(jobItem));
						}
					}
				}
				catch (LocalizedException lastScanError)
				{
					base.LastScanError = lastScanError;
				}
				catch (InvalidDataException lastScanError2)
				{
					base.LastScanError = lastScanError2;
				}
			}
			return collection;
		}

		// Token: 0x0400002A RID: 42
		private const string DiagnosticsComponentName = "MigrationIssueCache";
	}
}
