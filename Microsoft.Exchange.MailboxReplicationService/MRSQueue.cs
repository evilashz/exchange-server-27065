using System;
using System.Collections.Generic;
using System.Xml.Linq;
using Microsoft.Exchange.Collections.TimeoutCache;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Extensions;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x02000047 RID: 71
	internal class MRSQueue
	{
		// Token: 0x060003C7 RID: 967 RVA: 0x00018554 File Offset: 0x00016754
		public MRSQueue(Guid mdbGuid)
		{
			this.MdbGuid = mdbGuid;
			this.MdbDiscoveryTimestamp = DateTime.UtcNow;
		}

		// Token: 0x17000106 RID: 262
		// (get) Token: 0x060003C8 RID: 968 RVA: 0x0001856E File Offset: 0x0001676E
		// (set) Token: 0x060003C9 RID: 969 RVA: 0x00018576 File Offset: 0x00016776
		public Guid MdbGuid { get; private set; }

		// Token: 0x17000107 RID: 263
		// (get) Token: 0x060003CA RID: 970 RVA: 0x0001857F File Offset: 0x0001677F
		// (set) Token: 0x060003CB RID: 971 RVA: 0x00018587 File Offset: 0x00016787
		public DateTime MdbDiscoveryTimestamp { get; private set; }

		// Token: 0x17000108 RID: 264
		// (get) Token: 0x060003CC RID: 972 RVA: 0x00018590 File Offset: 0x00016790
		// (set) Token: 0x060003CD RID: 973 RVA: 0x00018598 File Offset: 0x00016798
		public DateTime LastScanTimestamp { get; private set; }

		// Token: 0x17000109 RID: 265
		// (get) Token: 0x060003CE RID: 974 RVA: 0x000185A1 File Offset: 0x000167A1
		// (set) Token: 0x060003CF RID: 975 RVA: 0x000185A9 File Offset: 0x000167A9
		public TimeSpan LastScanDuration { get; private set; }

		// Token: 0x1700010A RID: 266
		// (get) Token: 0x060003D0 RID: 976 RVA: 0x000185B2 File Offset: 0x000167B2
		// (set) Token: 0x060003D1 RID: 977 RVA: 0x000185BA File Offset: 0x000167BA
		public DateTime NextRecommendedScan { get; private set; }

		// Token: 0x1700010B RID: 267
		// (get) Token: 0x060003D2 RID: 978 RVA: 0x000185C3 File Offset: 0x000167C3
		// (set) Token: 0x060003D3 RID: 979 RVA: 0x000185CB File Offset: 0x000167CB
		public DateTime NextRecommendedLightScan { get; private set; }

		// Token: 0x1700010C RID: 268
		// (get) Token: 0x060003D4 RID: 980 RVA: 0x000185D4 File Offset: 0x000167D4
		// (set) Token: 0x060003D5 RID: 981 RVA: 0x000185DC File Offset: 0x000167DC
		public DateTime LastJobPickup { get; private set; }

		// Token: 0x1700010D RID: 269
		// (get) Token: 0x060003D6 RID: 982 RVA: 0x000185E5 File Offset: 0x000167E5
		// (set) Token: 0x060003D7 RID: 983 RVA: 0x000185ED File Offset: 0x000167ED
		public DateTime LastInteractiveJobPickup { get; private set; }

		// Token: 0x1700010E RID: 270
		// (get) Token: 0x060003D8 RID: 984 RVA: 0x000185F6 File Offset: 0x000167F6
		// (set) Token: 0x060003D9 RID: 985 RVA: 0x000185FE File Offset: 0x000167FE
		public int QueuedJobsCount { get; private set; }

		// Token: 0x1700010F RID: 271
		// (get) Token: 0x060003DA RID: 986 RVA: 0x00018607 File Offset: 0x00016807
		// (set) Token: 0x060003DB RID: 987 RVA: 0x0001860F File Offset: 0x0001680F
		public int InProgressJobsCount { get; private set; }

		// Token: 0x17000110 RID: 272
		// (get) Token: 0x060003DC RID: 988 RVA: 0x00018618 File Offset: 0x00016818
		// (set) Token: 0x060003DD RID: 989 RVA: 0x00018620 File Offset: 0x00016820
		public List<JobPickupRec> LastScanResults { get; private set; }

		// Token: 0x17000111 RID: 273
		// (get) Token: 0x060003DE RID: 990 RVA: 0x00018629 File Offset: 0x00016829
		// (set) Token: 0x060003DF RID: 991 RVA: 0x00018631 File Offset: 0x00016831
		public string LastScanFailure { get; private set; }

		// Token: 0x17000112 RID: 274
		// (get) Token: 0x060003E0 RID: 992 RVA: 0x0001863A File Offset: 0x0001683A
		// (set) Token: 0x060003E1 RID: 993 RVA: 0x00018642 File Offset: 0x00016842
		public DateTime LastActiveJobFinishTime { get; set; }

		// Token: 0x17000113 RID: 275
		// (get) Token: 0x060003E2 RID: 994 RVA: 0x0001864B File Offset: 0x0001684B
		// (set) Token: 0x060003E3 RID: 995 RVA: 0x00018653 File Offset: 0x00016853
		public Guid LastActiveJobFinished { get; set; }

		// Token: 0x060003E4 RID: 996 RVA: 0x0001865C File Offset: 0x0001685C
		public static MRSQueue Get(Guid mdbGuid)
		{
			MRSQueue mrsqueue;
			lock (MRSQueue.locker)
			{
				if (!MRSQueue.queues.TryGetValue(mdbGuid, out mrsqueue))
				{
					mrsqueue = new MRSQueue(mdbGuid);
					MRSQueue.queues.TryInsertSliding(mdbGuid, mrsqueue, MRSQueue.CacheTimeout);
				}
			}
			return mrsqueue;
		}

		// Token: 0x060003E5 RID: 997 RVA: 0x000186E8 File Offset: 0x000168E8
		public static XElement GetDiagnosticInfo(MRSDiagnosticArgument arguments)
		{
			XElement xelement = new XElement("Queues");
			lock (MRSQueue.locker)
			{
				using (List<MRSQueue>.Enumerator enumerator = MRSQueue.queues.Values.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						MRSQueue queue = enumerator.Current;
						xelement.Add(arguments.RunDiagnosticOperation(() => queue.GetQueueDiagnosticInfo(arguments)));
					}
				}
			}
			return xelement;
		}

		// Token: 0x060003E6 RID: 998 RVA: 0x000187BC File Offset: 0x000169BC
		public static List<Guid> GetQueuesToScan(MRSQueue.ScanType scanType)
		{
			List<Guid> list = null;
			DateTime utcNow = DateTime.UtcNow;
			lock (MRSQueue.locker)
			{
				foreach (MRSQueue mrsqueue in MRSQueue.queues.Values)
				{
					DateTime t = (scanType == MRSQueue.ScanType.Light) ? mrsqueue.NextRecommendedLightScan : mrsqueue.NextRecommendedScan;
					if (t <= utcNow)
					{
						if (list == null)
						{
							list = new List<Guid>();
						}
						list.Add(mrsqueue.MdbGuid);
					}
				}
			}
			if (list != null)
			{
				list = CommonUtils.RandomizeSequence<Guid>(list);
			}
			return list;
		}

		// Token: 0x060003E7 RID: 999 RVA: 0x00018880 File Offset: 0x00016A80
		public static List<Guid> GetQueues()
		{
			List<Guid> list = new List<Guid>(MRSQueue.queues.Count);
			lock (MRSQueue.locker)
			{
				foreach (MRSQueue mrsqueue in MRSQueue.queues.Values)
				{
					list.Add(mrsqueue.MdbGuid);
				}
			}
			return list;
		}

		// Token: 0x060003E8 RID: 1000 RVA: 0x00018934 File Offset: 0x00016B34
		public static LocalizedString GetJobPickupFailureMessageForRequest(Guid requestGuid)
		{
			LocalizedString result = LocalizedString.Empty;
			List<Guid> list = MRSQueue.GetQueues();
			if (!list.IsNullOrEmpty())
			{
				foreach (Guid mdbGuid in list)
				{
					MRSQueue mrsqueue = MRSQueue.Get(mdbGuid);
					List<JobPickupRec> lastScanResults = mrsqueue.LastScanResults;
					if (!lastScanResults.IsNullOrEmpty())
					{
						JobPickupRec jobPickupRec = lastScanResults.Find((JobPickupRec x) => x.RequestGuid == requestGuid);
						if (jobPickupRec != null)
						{
							result = jobPickupRec.GetPickupFailureMessage();
							break;
						}
					}
				}
			}
			return result;
		}

		// Token: 0x060003E9 RID: 1001 RVA: 0x000189E8 File Offset: 0x00016BE8
		public static void RemoveQueue(Guid mdbGuid)
		{
			lock (MRSQueue.locker)
			{
				MRSQueue.queues.Remove(mdbGuid);
			}
		}

		// Token: 0x060003EA RID: 1002 RVA: 0x00018A30 File Offset: 0x00016C30
		public void Tickle(MRSQueue.ScanType scanType)
		{
			switch (scanType)
			{
			case MRSQueue.ScanType.Light:
				this.NextRecommendedLightScan = DateTime.UtcNow;
				return;
			case MRSQueue.ScanType.Heavy:
				this.NextRecommendedScan = DateTime.UtcNow;
				return;
			default:
				return;
			}
		}

		// Token: 0x060003EB RID: 1003 RVA: 0x00018A68 File Offset: 0x00016C68
		public void PickupLightJobs()
		{
			DateTime utcNow = DateTime.UtcNow;
			SystemMailboxLightJobs systemMailboxLightJobs = new SystemMailboxLightJobs(this.MdbGuid);
			systemMailboxLightJobs.PickupJobs();
			if (systemMailboxLightJobs.ScanFailure != null)
			{
				this.NextRecommendedLightScan = DateTime.UtcNow + MRSQueue.ScanRetryInterval;
				return;
			}
			if (this.NextRecommendedLightScan <= utcNow)
			{
				this.NextRecommendedLightScan = systemMailboxLightJobs.RecommendedNextScan;
			}
		}

		// Token: 0x060003EC RID: 1004 RVA: 0x00018AC8 File Offset: 0x00016CC8
		public void PickupHeavyJobs(out bool mdbIsUnreachable)
		{
			SystemMailboxHeavyJobs systemMailboxHeavyJobs = new SystemMailboxHeavyJobs(this.MdbGuid);
			this.LastScanTimestamp = DateTime.UtcNow;
			MailboxReplicationServicePerMdbPerformanceCountersInstance perfCounter = MDBPerfCounterHelperCollection.GetMDBHelper(this.MdbGuid, true).PerfCounter;
			perfCounter.LastScanTime.RawValue = CommonUtils.TimestampToPerfcounterLong(this.LastScanTimestamp);
			systemMailboxHeavyJobs.PickupJobs();
			mdbIsUnreachable = (systemMailboxHeavyJobs.ScanFailure != null);
			this.LastScanDuration = DateTime.UtcNow - this.LastScanTimestamp;
			perfCounter.LastScanDuration.RawValue = (long)this.LastScanDuration.TotalMilliseconds;
			perfCounter.LastScanFailure.RawValue = (mdbIsUnreachable ? 1L : 0L);
			this.LastScanFailure = systemMailboxHeavyJobs.ScanFailure;
			this.LastScanResults = systemMailboxHeavyJobs.ScanResults;
			if (systemMailboxHeavyJobs.ScanResults != null)
			{
				foreach (JobPickupRec jobPickupRec in systemMailboxHeavyJobs.ScanResults)
				{
					if (jobPickupRec.PickupResult == JobPickupResult.JobPickedUp && jobPickupRec.Timestamp > this.LastJobPickup)
					{
						this.LastJobPickup = jobPickupRec.Timestamp;
					}
				}
			}
			if (mdbIsUnreachable)
			{
				this.NextRecommendedScan = DateTime.UtcNow + MRSQueue.ScanRetryInterval;
				return;
			}
			if (this.NextRecommendedScan <= this.LastScanTimestamp)
			{
				this.NextRecommendedScan = ((systemMailboxHeavyJobs.RecommendedNextScan < MRSService.NextFullScanTime) ? systemMailboxHeavyJobs.RecommendedNextScan : MRSService.NextFullScanTime);
				perfCounter.MdbQueueQueued.RawValue = (long)systemMailboxHeavyJobs.QueuedJobsCount;
				perfCounter.MdbQueueInProgress.RawValue = (long)systemMailboxHeavyJobs.InProgressJobsCount;
				this.QueuedJobsCount = systemMailboxHeavyJobs.QueuedJobsCount;
				this.InProgressJobsCount = systemMailboxHeavyJobs.InProgressJobsCount;
			}
		}

		// Token: 0x060003ED RID: 1005 RVA: 0x00018C84 File Offset: 0x00016E84
		private XElement GetQueueDiagnosticInfo(MRSDiagnosticArgument arguments)
		{
			DatabaseInformation databaseInformation = MapiUtils.FindServerForMdb(this.MdbGuid, null, null, FindServerFlags.AllowMissing);
			string argument = arguments.GetArgument<string>("queues");
			if (!string.IsNullOrEmpty(argument) && (databaseInformation.IsMissing || !CommonUtils.IsValueInWildcardedList(databaseInformation.DatabaseName, argument)))
			{
				return null;
			}
			MRSQueueDiagnosticInfoXML mrsqueueDiagnosticInfoXML = new MRSQueueDiagnosticInfoXML
			{
				MdbGuid = this.MdbGuid,
				MdbName = databaseInformation.DatabaseName,
				LastJobPickup = this.LastJobPickup,
				LastInteractiveJobPickup = this.LastInteractiveJobPickup,
				QueuedJobsCount = this.QueuedJobsCount,
				InProgressJobsCount = this.InProgressJobsCount,
				LastScanFailure = this.LastScanFailure,
				MdbDiscoveryTimestamp = this.MdbDiscoveryTimestamp,
				LastScanTimestamp = this.LastScanTimestamp,
				LastScanDurationMs = (int)this.LastScanDuration.TotalMilliseconds,
				NextRecommendedScan = this.NextRecommendedScan,
				LastActiveJobFinishTime = this.LastActiveJobFinishTime,
				LastActiveJobFinished = this.LastActiveJobFinished
			};
			if (arguments.HasArgument("pickupresults"))
			{
				mrsqueueDiagnosticInfoXML.LastScanResults = this.LastScanResults;
			}
			return mrsqueueDiagnosticInfoXML.ToDiagnosticInfo(null);
		}

		// Token: 0x04000189 RID: 393
		private static readonly TimeSpan CacheTimeout = TimeSpan.FromMinutes(30.0);

		// Token: 0x0400018A RID: 394
		private static readonly TimeSpan ScanRetryInterval = TimeSpan.FromMinutes(1.0);

		// Token: 0x0400018B RID: 395
		private static readonly ExactTimeoutCache<Guid, MRSQueue> queues = new ExactTimeoutCache<Guid, MRSQueue>(null, null, null, 1000, false);

		// Token: 0x0400018C RID: 396
		private static readonly object locker = new object();

		// Token: 0x02000048 RID: 72
		public enum ScanType
		{
			// Token: 0x0400019C RID: 412
			Light = 1,
			// Token: 0x0400019D RID: 413
			Heavy
		}
	}
}
