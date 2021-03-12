using System;
using System.Globalization;
using System.Xml.Linq;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.Transport.Sync.Common;

namespace Microsoft.Exchange.Transport.Sync.Worker.Health
{
	// Token: 0x02000029 RID: 41
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal class RemoteServerHealthData
	{
		// Token: 0x06000214 RID: 532 RVA: 0x00009817 File Offset: 0x00007A17
		internal RemoteServerHealthData(string serverName, TimeSpan slidingCounterWindowSize, TimeSpan slidingCounterBucketLength)
		{
			SyncUtilities.ThrowIfArgumentNullOrEmpty("serverName", serverName);
			this.serverName = serverName;
			this.slidingAverageLatencyCounter = new SlidingAverageCounter(slidingCounterWindowSize, slidingCounterBucketLength);
			this.Reset();
		}

		// Token: 0x06000215 RID: 533 RVA: 0x00009844 File Offset: 0x00007A44
		protected RemoteServerHealthData(string serverName, RemoteServerHealthState state, int backOffCount, ExDateTime lastUpdateTime, ExDateTime? lastBackOffStartTime, TimeSpan slidingCounterWindowSize, TimeSpan slidingCounterBucketLength)
		{
			this.serverName = serverName;
			this.state = state;
			this.backOffCount = backOffCount;
			this.lastUpdateTime = lastUpdateTime;
			this.lastBackOffStartTime = lastBackOffStartTime;
			this.slidingAverageLatencyCounter = new SlidingAverageCounter(slidingCounterWindowSize, slidingCounterBucketLength);
		}

		// Token: 0x170000DA RID: 218
		// (get) Token: 0x06000216 RID: 534 RVA: 0x00009880 File Offset: 0x00007A80
		internal string ServerName
		{
			get
			{
				return this.serverName;
			}
		}

		// Token: 0x170000DB RID: 219
		// (get) Token: 0x06000217 RID: 535 RVA: 0x00009888 File Offset: 0x00007A88
		internal RemoteServerHealthState State
		{
			get
			{
				return this.state;
			}
		}

		// Token: 0x170000DC RID: 220
		// (get) Token: 0x06000218 RID: 536 RVA: 0x00009890 File Offset: 0x00007A90
		internal int BackOffCount
		{
			get
			{
				return this.backOffCount;
			}
		}

		// Token: 0x170000DD RID: 221
		// (get) Token: 0x06000219 RID: 537 RVA: 0x00009898 File Offset: 0x00007A98
		internal ExDateTime LastUpdateTime
		{
			get
			{
				return this.lastUpdateTime;
			}
		}

		// Token: 0x170000DE RID: 222
		// (get) Token: 0x0600021A RID: 538 RVA: 0x000098A0 File Offset: 0x00007AA0
		internal virtual ExDateTime? LastBackOffStartTime
		{
			get
			{
				return this.lastBackOffStartTime;
			}
		}

		// Token: 0x0600021B RID: 539 RVA: 0x000098A8 File Offset: 0x00007AA8
		internal static RemoteServerHealthData CreateRemoteServerHealthDataForViolatingServer(string serverName, RemoteServerHealthState state, int backOffCount, ExDateTime lastUpdateTime, ExDateTime lastBackOffStartTime, TimeSpan slidingCounterWindowSize, TimeSpan slidingCounterBucketLength)
		{
			SyncUtilities.ThrowIfArgumentNullOrEmpty("serverName", serverName);
			SyncUtilities.ThrowIfArgumentLessThanEqualToZero("backOffCount", backOffCount);
			SyncUtilities.ThrowIfArg1LessThenArg2("lastUpdateTime", lastUpdateTime, "lastBackOffStartTime", lastBackOffStartTime);
			return new RemoteServerHealthData(serverName, state, backOffCount, lastUpdateTime, new ExDateTime?(lastBackOffStartTime), slidingCounterWindowSize, slidingCounterBucketLength);
		}

		// Token: 0x0600021C RID: 540 RVA: 0x000098E8 File Offset: 0x00007AE8
		internal static bool TryCreateRemoteServerHealthDataForViolatingServer(string serverName, RemoteServerHealthState state, int backOffCount, ExDateTime lastUpdateTime, ExDateTime lastBackOffStartTime, TimeSpan slidingCounterWindowSize, TimeSpan slidingCounterBucketLength, out RemoteServerHealthData healthData, out Exception exception)
		{
			healthData = null;
			exception = null;
			bool result;
			try
			{
				healthData = RemoteServerHealthData.CreateRemoteServerHealthDataForViolatingServer(serverName, state, backOffCount, lastUpdateTime, lastBackOffStartTime, slidingCounterWindowSize, slidingCounterBucketLength);
				result = true;
			}
			catch (ArgumentException ex)
			{
				exception = ex;
				result = false;
			}
			return result;
		}

		// Token: 0x0600021D RID: 541 RVA: 0x00009930 File Offset: 0x00007B30
		internal TimeSpan TimeSinceLastUpdate()
		{
			return this.GetCurrentTime() - this.lastUpdateTime;
		}

		// Token: 0x0600021E RID: 542 RVA: 0x00009950 File Offset: 0x00007B50
		internal TimeSpan TimeSinceLastBackOff()
		{
			return this.GetCurrentTime() - this.LastBackOffStartTime.Value;
		}

		// Token: 0x0600021F RID: 543 RVA: 0x00009978 File Offset: 0x00007B78
		internal XElement GetDiagnosticInfo()
		{
			string content = (this.lastBackOffStartTime != null) ? this.TimeSinceLastBackOff().ToString() : null;
			long num = 0L;
			long averageLantency = this.GetAverageLantency(out num);
			return new XElement("HealthData", new object[]
			{
				new XElement("serverName", this.serverName),
				new XElement("state", this.state),
				new XElement("backOffCount", this.backOffCount),
				new XElement("lastUpdateTime", this.lastUpdateTime),
				new XElement("timeSinceLastUpdate", this.TimeSinceLastUpdate().ToString()),
				new XElement("lastBackOffStartTime", this.lastBackOffStartTime),
				new XElement("timeSinceLastBackOff", content),
				new XElement("averageLatency", averageLantency),
				new XElement("numberOfRoundtrips", num)
			});
		}

		// Token: 0x06000220 RID: 544 RVA: 0x00009ACF File Offset: 0x00007CCF
		internal virtual void RecordServerLatency(long latency)
		{
			this.slidingAverageLatencyCounter.AddValue(latency);
			this.lastUpdateTime = this.GetCurrentTime();
		}

		// Token: 0x06000221 RID: 545 RVA: 0x00009AE9 File Offset: 0x00007CE9
		internal virtual long GetAverageLantency(out long numberOfRoundtrips)
		{
			return this.slidingAverageLatencyCounter.CalculateAverageAcrossAllSamples(out numberOfRoundtrips);
		}

		// Token: 0x06000222 RID: 546 RVA: 0x00009AF8 File Offset: 0x00007CF8
		internal void MarkAsBackedOff()
		{
			this.state = RemoteServerHealthState.BackedOff;
			this.lastBackOffStartTime = new ExDateTime?(this.lastUpdateTime = this.GetCurrentTime());
			this.backOffCount++;
		}

		// Token: 0x06000223 RID: 547 RVA: 0x00009B34 File Offset: 0x00007D34
		internal void MarkAsPoisonous()
		{
			this.state = RemoteServerHealthState.Poisonous;
			this.lastUpdateTime = this.GetCurrentTime();
		}

		// Token: 0x06000224 RID: 548 RVA: 0x00009B49 File Offset: 0x00007D49
		internal void MarkAsClean()
		{
			this.state = RemoteServerHealthState.Clean;
			this.lastUpdateTime = this.GetCurrentTime();
		}

		// Token: 0x06000225 RID: 549 RVA: 0x00009B5E File Offset: 0x00007D5E
		internal void Reset()
		{
			this.state = RemoteServerHealthState.Clean;
			this.backOffCount = 0;
			this.lastBackOffStartTime = null;
			this.lastUpdateTime = this.GetCurrentTime();
		}

		// Token: 0x06000226 RID: 550 RVA: 0x00009B86 File Offset: 0x00007D86
		protected virtual ExDateTime GetCurrentTime()
		{
			return ExDateTime.UtcNow;
		}

		// Token: 0x06000227 RID: 551 RVA: 0x00009B90 File Offset: 0x00007D90
		public override string ToString()
		{
			return string.Format(CultureInfo.InvariantCulture, "serverName:{0}, state:{1}, backOffCount:{2}, lastUpdateTime:{3}, lastBackOffStartTime:{4}", new object[]
			{
				this.serverName,
				this.state,
				this.backOffCount,
				this.lastUpdateTime,
				this.lastBackOffStartTime
			});
		}

		// Token: 0x04000119 RID: 281
		protected ExDateTime? lastBackOffStartTime;

		// Token: 0x0400011A RID: 282
		private readonly string serverName;

		// Token: 0x0400011B RID: 283
		private readonly SlidingAverageCounter slidingAverageLatencyCounter;

		// Token: 0x0400011C RID: 284
		private RemoteServerHealthState state;

		// Token: 0x0400011D RID: 285
		private int backOffCount;

		// Token: 0x0400011E RID: 286
		private ExDateTime lastUpdateTime;
	}
}
