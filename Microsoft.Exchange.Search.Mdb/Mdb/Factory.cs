using System;
using System.Diagnostics;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Search.Core.Abstraction;
using Microsoft.Exchange.Search.OperatorSchema;
using Microsoft.Exchange.Search.Performance;

namespace Microsoft.Exchange.Search.Mdb
{
	// Token: 0x0200000D RID: 13
	internal class Factory
	{
		// Token: 0x0600003D RID: 61 RVA: 0x00005BD4 File Offset: 0x00003DD4
		protected Factory()
		{
		}

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x0600003E RID: 62 RVA: 0x00005BDC File Offset: 0x00003DDC
		internal static Hookable<Factory> Instance
		{
			[DebuggerStepThrough]
			get
			{
				return Factory.instance;
			}
		}

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x0600003F RID: 63 RVA: 0x00005BE3 File Offset: 0x00003DE3
		internal static Factory Current
		{
			[DebuggerStepThrough]
			get
			{
				return Factory.instance.Value;
			}
		}

		// Token: 0x06000040 RID: 64 RVA: 0x00005BEF File Offset: 0x00003DEF
		internal virtual IMdbWatcher CreateMdbWatcher()
		{
			return new MdbWatcher();
		}

		// Token: 0x06000041 RID: 65 RVA: 0x00005BF6 File Offset: 0x00003DF6
		internal virtual IFeeder CreateNotificationsFeeder(MdbPerfCountersInstance mdbFeedingPerfCounters, MdbInfo mdbInfo, ISearchServiceConfig config, ISubmitDocument indexFeeder, IWatermarkStorage watermarkStorage, IIndexStatusStore indexStatusStore)
		{
			return new NotificationsFeeder(mdbFeedingPerfCounters, mdbInfo, config, indexFeeder, watermarkStorage, indexStatusStore);
		}

		// Token: 0x06000042 RID: 66 RVA: 0x00005C08 File Offset: 0x00003E08
		internal virtual IFeeder CreateCrawlerFeeder(MdbPerfCountersInstance mdbFeedingPerfCounters, MdbInfo mdbInfo, ISearchServiceConfig config, ISubmitDocument indexFeeder, IWatermarkStorage watermarkStorage, IFailedItemStorage failedItemStorage, IIndexStatusStore indexStatusStore)
		{
			CrawlerMailboxIterator mailboxIterator = Factory.Current.CreateCrawlerMailboxIterator(mdbInfo);
			ICrawlerItemIterator<int> itemIterator = Factory.Current.CreateCrawlerItemIterator(config.MaxRowCount);
			return new CrawlerFeeder(mdbFeedingPerfCounters, mdbInfo, config, mailboxIterator, itemIterator, watermarkStorage, failedItemStorage, indexFeeder, indexStatusStore);
		}

		// Token: 0x06000043 RID: 67 RVA: 0x00005C44 File Offset: 0x00003E44
		internal virtual IFeeder CreateRetryFeeder(MdbPerfCountersInstance mdbFeedingPerfCounters, MdbInfo mdbInfo, ISearchServiceConfig config, ISubmitDocument indexFeeder, IFailedItemStorage failedItemStorage, IWatermarkStorage watermarkStorage, IIndexStatusStore indexStatusStore)
		{
			return new RetryFeeder(mdbFeedingPerfCounters, mdbInfo, indexFeeder, config, failedItemStorage, watermarkStorage, indexStatusStore);
		}

		// Token: 0x06000044 RID: 68 RVA: 0x00005C56 File Offset: 0x00003E56
		internal virtual INotificationsEventSource CreateNotificationsEventSource(MdbInfo mdbInfo)
		{
			return new NotificationsEventSource(mdbInfo);
		}

		// Token: 0x06000045 RID: 69 RVA: 0x00005C5E File Offset: 0x00003E5E
		internal virtual CrawlerMailboxIterator CreateCrawlerMailboxIterator(MdbInfo mdbInfo)
		{
			return new CrawlerMailboxIterator(mdbInfo);
		}

		// Token: 0x06000046 RID: 70 RVA: 0x00005C66 File Offset: 0x00003E66
		internal virtual ICrawlerItemIterator<int> CreateCrawlerItemIterator(int maxRowCount)
		{
			return new CrawlerDocIdViewIterator(maxRowCount);
		}

		// Token: 0x06000047 RID: 71 RVA: 0x00005C6E File Offset: 0x00003E6E
		internal virtual IFeederRateThrottlingManager CreateFeederRateThrottlingManager(ISearchServiceConfig config, MdbInfo mdbInfo, FeederRateThrottlingManager.ThrottlingRateExecutionType execType)
		{
			return new FeederRateThrottlingManager(config, mdbInfo, execType);
		}

		// Token: 0x06000048 RID: 72 RVA: 0x00005C78 File Offset: 0x00003E78
		internal virtual IFeederDelayThrottlingManager CreateFeederDelayThrottlingManager(ISearchServiceConfig config)
		{
			return new FeederDelayThrottlingManager(config);
		}

		// Token: 0x06000049 RID: 73 RVA: 0x00005C80 File Offset: 0x00003E80
		internal virtual ISearchServiceConfig CreateSearchServiceConfig()
		{
			return SearchConfig.Instance;
		}

		// Token: 0x0400003A RID: 58
		private static readonly Hookable<Factory> instance = Hookable<Factory>.Create(true, new Factory());
	}
}
