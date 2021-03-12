using System;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.Rpc.Cluster;
using Microsoft.Exchange.Search.OperatorSchema;

namespace Microsoft.Exchange.Search.Core.Abstraction
{
	// Token: 0x0200002E RID: 46
	internal sealed class IndexStatus
	{
		// Token: 0x060000FA RID: 250 RVA: 0x000026E8 File Offset: 0x000008E8
		public IndexStatus(string seedingSource, VersionInfo version) : this(ContentIndexStatusType.Seeding, IndexStatusErrorCode.SeedingCatalog, version, seedingSource)
		{
		}

		// Token: 0x060000FB RID: 251 RVA: 0x000026F4 File Offset: 0x000008F4
		public IndexStatus(int mailboxToCrawl, VersionInfo version) : this(version.IsUpgrading ? ContentIndexStatusType.HealthyAndUpgrading : ContentIndexStatusType.Crawling, IndexStatusErrorCode.CrawlingDatabase, version, null, mailboxToCrawl)
		{
		}

		// Token: 0x060000FC RID: 252 RVA: 0x0000270E File Offset: 0x0000090E
		public IndexStatus(ContentIndexStatusType indexingState, IndexStatusErrorCode errorCode, VersionInfo version) : this(indexingState, errorCode, version, null)
		{
		}

		// Token: 0x060000FD RID: 253 RVA: 0x0000271A File Offset: 0x0000091A
		public IndexStatus(ContentIndexStatusType indexingState, IndexStatusErrorCode errorCode, VersionInfo version, string seedingSource) : this(indexingState, errorCode, version, seedingSource, 0)
		{
		}

		// Token: 0x060000FE RID: 254 RVA: 0x00002728 File Offset: 0x00000928
		public IndexStatus(ContentIndexStatusType indexingState, IndexStatusErrorCode errorCode, VersionInfo version, string seedingSource, int mailboxToCrawl) : this(indexingState, errorCode, version, seedingSource, mailboxToCrawl, ExDateTime.UtcNow)
		{
			if ((mailboxToCrawl != 0 && indexingState != ContentIndexStatusType.Crawling && indexingState != ContentIndexStatusType.HealthyAndUpgrading) || (mailboxToCrawl <= 0 && (indexingState == ContentIndexStatusType.Crawling || indexingState == ContentIndexStatusType.HealthyAndUpgrading)))
			{
				throw new ArgumentOutOfRangeException(string.Format("Unexpected mailboxToCrawl value {0} when indexingState is {1}", mailboxToCrawl, indexingState));
			}
		}

		// Token: 0x060000FF RID: 255 RVA: 0x00002780 File Offset: 0x00000980
		public IndexStatus(ContentIndexStatusType indexingState, IndexStatusErrorCode errorCode, VersionInfo version, string seedingSource, int mailboxToCrawl, ExDateTime timeStamp)
		{
			if (indexingState == ContentIndexStatusType.Failed && errorCode == IndexStatusErrorCode.Unknown)
			{
				throw new ArgumentException("Must have a known error code for failed state");
			}
			this.TimeStamp = timeStamp;
			this.IndexingState = indexingState;
			this.ErrorCode = errorCode;
			this.Version = version;
			this.MailboxesToCrawl = mailboxToCrawl;
			this.AgeOfLastNotificationProcessed = IndexStatus.DefaultCounterValue;
			this.RetriableItemsCount = IndexStatus.DefaultCounterValue;
			this.SeedingSource = seedingSource;
		}

		// Token: 0x17000047 RID: 71
		// (get) Token: 0x06000100 RID: 256 RVA: 0x000027E8 File Offset: 0x000009E8
		// (set) Token: 0x06000101 RID: 257 RVA: 0x000027F0 File Offset: 0x000009F0
		public ExDateTime TimeStamp { get; set; }

		// Token: 0x17000048 RID: 72
		// (get) Token: 0x06000102 RID: 258 RVA: 0x000027F9 File Offset: 0x000009F9
		// (set) Token: 0x06000103 RID: 259 RVA: 0x00002801 File Offset: 0x00000A01
		public ContentIndexStatusType IndexingState { get; set; }

		// Token: 0x17000049 RID: 73
		// (get) Token: 0x06000104 RID: 260 RVA: 0x0000280A File Offset: 0x00000A0A
		// (set) Token: 0x06000105 RID: 261 RVA: 0x00002812 File Offset: 0x00000A12
		public IndexStatusErrorCode ErrorCode { get; set; }

		// Token: 0x1700004A RID: 74
		// (get) Token: 0x06000106 RID: 262 RVA: 0x0000281B File Offset: 0x00000A1B
		// (set) Token: 0x06000107 RID: 263 RVA: 0x00002823 File Offset: 0x00000A23
		public VersionInfo Version { get; set; }

		// Token: 0x1700004B RID: 75
		// (get) Token: 0x06000108 RID: 264 RVA: 0x0000282C File Offset: 0x00000A2C
		// (set) Token: 0x06000109 RID: 265 RVA: 0x00002834 File Offset: 0x00000A34
		public int MailboxesToCrawl { get; set; }

		// Token: 0x1700004C RID: 76
		// (get) Token: 0x0600010A RID: 266 RVA: 0x0000283D File Offset: 0x00000A3D
		// (set) Token: 0x0600010B RID: 267 RVA: 0x00002845 File Offset: 0x00000A45
		public string SeedingSource { get; set; }

		// Token: 0x1700004D RID: 77
		// (get) Token: 0x0600010C RID: 268 RVA: 0x0000284E File Offset: 0x00000A4E
		// (set) Token: 0x0600010D RID: 269 RVA: 0x00002856 File Offset: 0x00000A56
		public long AgeOfLastNotificationProcessed { get; set; }

		// Token: 0x1700004E RID: 78
		// (get) Token: 0x0600010E RID: 270 RVA: 0x0000285F File Offset: 0x00000A5F
		// (set) Token: 0x0600010F RID: 271 RVA: 0x00002867 File Offset: 0x00000A67
		public long RetriableItemsCount { get; set; }

		// Token: 0x06000110 RID: 272 RVA: 0x00002870 File Offset: 0x00000A70
		public static LocalizedString GetExcludeReasonFromErrorCode(IndexStatusErrorCode errorCode)
		{
			switch (errorCode)
			{
			case IndexStatusErrorCode.Unknown:
			case IndexStatusErrorCode.Success:
				return LocalizedString.Empty;
			case IndexStatusErrorCode.InternalError:
				return Strings.InternalError;
			case IndexStatusErrorCode.CrawlingDatabase:
				return Strings.CrawlingDatabase;
			case IndexStatusErrorCode.DatabaseOffline:
				return Strings.DatabaseOffline;
			case IndexStatusErrorCode.MapiNetworkError:
				return Strings.MapiNetworkError;
			case IndexStatusErrorCode.CatalogCorruption:
			case IndexStatusErrorCode.CatalogCorruptionWhenFeedingStarts:
			case IndexStatusErrorCode.CatalogCorruptionWhenFeedingCompletes:
				return Strings.CatalogCorruption;
			case IndexStatusErrorCode.SeedingCatalog:
				return Strings.SeedingCatalog;
			case IndexStatusErrorCode.CatalogSuspended:
				return Strings.CatalogSuspended;
			case IndexStatusErrorCode.CatalogReseed:
			case IndexStatusErrorCode.EventsMissingWithNotificationsWatermark:
			case IndexStatusErrorCode.CrawlOnNonPreferredActiveWithNotificationsWatermark:
			case IndexStatusErrorCode.CrawlOnNonPreferredActiveWithTooManyNotificationEvents:
			case IndexStatusErrorCode.CrawlOnPassive:
				return Strings.CatalogReseed;
			case IndexStatusErrorCode.IndexNotEnabled:
				return Strings.IndexNotEnabled;
			case IndexStatusErrorCode.CatalogExcluded:
				return Strings.CatalogExcluded;
			case IndexStatusErrorCode.ActivationPreferenceSkipped:
				return Strings.ActivationPreferenceSkipped;
			case IndexStatusErrorCode.LagCopySkipped:
				return Strings.LagCopySkipped;
			case IndexStatusErrorCode.RecoveryDatabaseSkipped:
				return Strings.RecoveryDatabaseSkipped;
			case IndexStatusErrorCode.FastError:
				return Strings.FastServiceNotRunning(Environment.MachineName);
			case IndexStatusErrorCode.ServiceNotRunning:
				return Strings.SearchServiceNotRunning(Environment.MachineName);
			case IndexStatusErrorCode.IndexStatusTimestampTooOld:
				return Strings.IndexStatusTimestampTooOld;
			default:
				throw new ArgumentException(string.Format("Error code {0} doesn't match any reason string", errorCode));
			}
		}

		// Token: 0x06000111 RID: 273 RVA: 0x0000296C File Offset: 0x00000B6C
		public override string ToString()
		{
			return string.Concat(new object[]
			{
				"IndexingState=",
				this.IndexingState,
				", ErrorCode=",
				this.ErrorCode,
				", Version=",
				this.Version,
				", MailboxesToCrawl=",
				this.MailboxesToCrawl,
				", SeedingSource=",
				this.SeedingSource ?? "(n/a)",
				", TimeStamp=",
				this.TimeStamp.ToString("u"),
				", AgeOfLastNotificationProcessed=",
				this.AgeOfLastNotificationProcessed,
				", RetriableItemsCount=",
				this.RetriableItemsCount
			});
		}

		// Token: 0x06000112 RID: 274 RVA: 0x00002A4C File Offset: 0x00000C4C
		public void UpdateValue(IndexStatusIndex indexStatusIndex, long value)
		{
			switch (indexStatusIndex)
			{
			case IndexStatusIndex.AgeOfLastNotificationProcessed:
				this.AgeOfLastNotificationProcessed = value;
				return;
			case IndexStatusIndex.RetriableItemsCount:
				this.RetriableItemsCount = value;
				return;
			default:
				throw new InvalidOperationException("indexStatusIndex");
			}
		}

		// Token: 0x0400002C RID: 44
		internal static readonly long DefaultCounterValue;
	}
}
