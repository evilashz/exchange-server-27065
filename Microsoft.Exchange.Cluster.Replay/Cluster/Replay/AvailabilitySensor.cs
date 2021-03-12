using System;
using Microsoft.Exchange.Cluster.ReplayEventLog;
using Microsoft.Exchange.Data.HA.DirectoryServices;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Cluster.Replay;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x020001DE RID: 478
	internal class AvailabilitySensor
	{
		// Token: 0x1700053D RID: 1341
		// (get) Token: 0x06001307 RID: 4871 RVA: 0x0004CFBA File Offset: 0x0004B1BA
		public static Trace Tracer
		{
			get
			{
				return ExTraceGlobals.LogReplayerTracer;
			}
		}

		// Token: 0x1700053E RID: 1342
		// (get) Token: 0x06001308 RID: 4872 RVA: 0x0004CFC1 File Offset: 0x0004B1C1
		// (set) Token: 0x06001309 RID: 4873 RVA: 0x0004CFC9 File Offset: 0x0004B1C9
		public bool FailedToRead { get; private set; }

		// Token: 0x1700053F RID: 1343
		// (get) Token: 0x0600130A RID: 4874 RVA: 0x0004CFD2 File Offset: 0x0004B1D2
		// (set) Token: 0x0600130B RID: 4875 RVA: 0x0004CFDA File Offset: 0x0004B1DA
		public IHealthValidationResult LastReading { get; private set; }

		// Token: 0x17000540 RID: 1344
		// (get) Token: 0x0600130C RID: 4876 RVA: 0x0004CFE3 File Offset: 0x0004B1E3
		// (set) Token: 0x0600130D RID: 4877 RVA: 0x0004CFEB File Offset: 0x0004B1EB
		public ExDateTime LastReadingTime { get; private set; }

		// Token: 0x17000541 RID: 1345
		// (get) Token: 0x0600130E RID: 4878 RVA: 0x0004CFF4 File Offset: 0x0004B1F4
		// (set) Token: 0x0600130F RID: 4879 RVA: 0x0004CFFC File Offset: 0x0004B1FC
		public int MinAvailableCopies { get; set; }

		// Token: 0x17000542 RID: 1346
		// (get) Token: 0x06001310 RID: 4880 RVA: 0x0004D005 File Offset: 0x0004B205
		// (set) Token: 0x06001311 RID: 4881 RVA: 0x0004D00D File Offset: 0x0004B20D
		public TimeSpan MaxProbeFreq { get; set; }

		// Token: 0x17000543 RID: 1347
		// (get) Token: 0x06001312 RID: 4882 RVA: 0x0004D016 File Offset: 0x0004B216
		// (set) Token: 0x06001313 RID: 4883 RVA: 0x0004D01E File Offset: 0x0004B21E
		public IMonitoringADConfigProvider ADConfigProvider { get; private set; }

		// Token: 0x17000544 RID: 1348
		// (get) Token: 0x06001314 RID: 4884 RVA: 0x0004D027 File Offset: 0x0004B227
		// (set) Token: 0x06001315 RID: 4885 RVA: 0x0004D02F File Offset: 0x0004B22F
		public ICopyStatusClientLookup CopyStatusLookup { get; private set; }

		// Token: 0x17000545 RID: 1349
		// (get) Token: 0x06001316 RID: 4886 RVA: 0x0004D038 File Offset: 0x0004B238
		// (set) Token: 0x06001317 RID: 4887 RVA: 0x0004D040 File Offset: 0x0004B240
		public IADDatabase Database { get; private set; }

		// Token: 0x06001318 RID: 4888 RVA: 0x0004D049 File Offset: 0x0004B249
		public AvailabilitySensor(IADDatabase database, IMonitoringADConfigProvider adConfigProvider, ICopyStatusClientLookup statusLookup) : this(database, adConfigProvider, statusLookup, 2, AvailabilitySensor.DefaultMaxProbeFreq)
		{
		}

		// Token: 0x06001319 RID: 4889 RVA: 0x0004D05A File Offset: 0x0004B25A
		public AvailabilitySensor(IADDatabase database, IMonitoringADConfigProvider adConfigProvider, ICopyStatusClientLookup statusLookup, int minCopies, TimeSpan maxProbeFreq)
		{
			this.Database = database;
			this.ADConfigProvider = adConfigProvider;
			this.CopyStatusLookup = statusLookup;
			this.MinAvailableCopies = minCopies;
			this.MaxProbeFreq = maxProbeFreq;
		}

		// Token: 0x0600131A RID: 4890 RVA: 0x0004D088 File Offset: 0x0004B288
		public bool DatabaseHasSufficientAvailability()
		{
			if (this.LastReading != null)
			{
				if (!(ExDateTime.UtcNow - this.LastReadingTime >= this.MaxProbeFreq))
				{
					goto IL_C6;
				}
			}
			try
			{
				IMonitoringADConfig configIgnoringStaleness = this.ADConfigProvider.GetConfigIgnoringStaleness(true);
				DatabaseAvailabilityValidator databaseAvailabilityValidator = new DatabaseAvailabilityValidator(this.Database, this.MinAvailableCopies, this.CopyStatusLookup, configIgnoringStaleness, null, true);
				this.LastReading = databaseAvailabilityValidator.Run();
				this.LastReadingTime = ExDateTime.Now;
			}
			catch (MonitoringADServiceShuttingDownException arg)
			{
				AvailabilitySensor.Tracer.TraceError<MonitoringADServiceShuttingDownException>((long)this.GetHashCode(), "AvailabilitySensor: Got service shutting down exception when retrieving AD config: {0}", arg);
			}
			catch (MonitoringADConfigException ex)
			{
				this.FailedToRead = true;
				AvailabilitySensor.Tracer.TraceError<MonitoringADConfigException>((long)this.GetHashCode(), "AvailabilitySensor: Got exception when retrieving AD config: {0}", ex);
				ReplayCrimsonEvents.AvailabilitySensorError.LogPeriodic<string, MonitoringADConfigException>(this.GetHashCode(), DiagCore.DefaultEventSuppressionInterval, ex.Message, ex);
			}
			IL_C6:
			return this.LastReading != null && this.LastReading.HealthyCopiesCount >= this.MinAvailableCopies;
		}

		// Token: 0x04000749 RID: 1865
		public const int DefaultMinAvailableCopies = 2;

		// Token: 0x0400074A RID: 1866
		public static readonly TimeSpan DefaultMaxProbeFreq = TimeSpan.FromSeconds(60.0);
	}
}
