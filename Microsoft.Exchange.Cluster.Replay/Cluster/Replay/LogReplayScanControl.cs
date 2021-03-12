using System;
using Microsoft.Exchange.Cluster.ReplayEventLog;
using Microsoft.Exchange.Cluster.Shared;
using Microsoft.Exchange.Data.HA.DirectoryServices;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Cluster.Replay;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x020002E5 RID: 741
	internal class LogReplayScanControl
	{
		// Token: 0x170007E9 RID: 2025
		// (get) Token: 0x06001DC8 RID: 7624 RVA: 0x0008857F File Offset: 0x0008677F
		public static Trace Tracer
		{
			get
			{
				return ExTraceGlobals.LogReplayerTracer;
			}
		}

		// Token: 0x170007EA RID: 2026
		// (get) Token: 0x06001DC9 RID: 7625 RVA: 0x00088586 File Offset: 0x00086786
		// (set) Token: 0x06001DCA RID: 7626 RVA: 0x0008858E File Offset: 0x0008678E
		public LogReplayScanControl.ControlParameters Parameters { get; set; }

		// Token: 0x170007EB RID: 2027
		// (get) Token: 0x06001DCB RID: 7627 RVA: 0x00088597 File Offset: 0x00086797
		// (set) Token: 0x06001DCC RID: 7628 RVA: 0x0008859F File Offset: 0x0008679F
		public IADDatabase Database { get; private set; }

		// Token: 0x06001DCD RID: 7629 RVA: 0x000885A8 File Offset: 0x000867A8
		public LogReplayScanControl(IADDatabase database, bool isLagCopy, IMonitoringADConfigProvider adConfigProvider, ICopyStatusClientLookup statusLookup, IPerfmonCounters keepingUpReader) : this(database, isLagCopy, adConfigProvider, statusLookup, keepingUpReader, new LogReplayScanControl.ControlParameters())
		{
		}

		// Token: 0x06001DCE RID: 7630 RVA: 0x000885BC File Offset: 0x000867BC
		public LogReplayScanControl(IADDatabase database, bool isLagCopy, IMonitoringADConfigProvider adConfigProvider, ICopyStatusClientLookup statusLookup, IPerfmonCounters keepingUpReader, LogReplayScanControl.ControlParameters parameters)
		{
			this.sensor = new AvailabilitySensor(database, adConfigProvider, statusLookup, parameters.MinAvailablePassiveCopies + 1, parameters.MaxProbeFreq);
			this.isLagCopy = isLagCopy;
			this.Database = database;
			this.Parameters = parameters;
			this.keepingUp = keepingUpReader;
		}

		// Token: 0x06001DCF RID: 7631 RVA: 0x00088613 File Offset: 0x00086813
		public bool ShouldBeEnabled(bool isFastLagPlaydownDesired, long replayQ)
		{
			this.RunLogic(isFastLagPlaydownDesired, replayQ);
			return this.isEnabled;
		}

		// Token: 0x06001DD0 RID: 7632 RVA: 0x00088624 File Offset: 0x00086824
		private void RunLogic(bool isFastLagPlaydownDesired, long replayQ)
		{
			if (RegistryParameters.DisableDatabaseScan)
			{
				this.isEnabled = false;
				return;
			}
			if (isFastLagPlaydownDesired)
			{
				if (this.DisableScan())
				{
					this.LogDisable("FastLagPlaydownDesired");
				}
				return;
			}
			if (!this.isEnabled && ExDateTime.UtcNow - this.lastDisableTime < this.Parameters.MinDisabledWindow)
			{
				return;
			}
			if (this.isEnabled && replayQ < this.Parameters.MinReplayQOfInterest)
			{
				return;
			}
			if (this.isLagCopy)
			{
				if (replayQ > this.Parameters.MaxReplayQForBehindCopy)
				{
					if (this.DisableScan())
					{
						this.LogDisable(string.Format("Lag too behind. ReplayQ={0} MaxQ={1}", replayQ, this.Parameters.MaxReplayQForBehindCopy));
						return;
					}
				}
				else if (!this.isEnabled && replayQ < this.Parameters.MaxReplayQForAvailableCopy && this.EnableScan())
				{
					this.LogEnable(string.Format("Lag catching up. ReplayQ={0} MaxQ={1}", replayQ, this.Parameters.MaxReplayQForAvailableCopy));
				}
				return;
			}
			bool flag = this.sensor.DatabaseHasSufficientAvailability();
			if (this.sensor.FailedToRead)
			{
				if (replayQ > this.Parameters.MaxReplayQForAvailableCopy && this.DisableScan())
				{
					this.LogDisable("DatabaseHasSufficientAvailability failed");
				}
				return;
			}
			IHealthValidationResult lastReading = this.sensor.LastReading;
			if (!flag)
			{
				if (this.DisableScan())
				{
					string reason = string.Format("Database '{0}' has insufficient availability. MinAvail={1} CurAvail={2}", this.Database.Name, this.sensor.MinAvailableCopies, lastReading.HealthyCopiesCount);
					this.LogDisable(reason);
				}
				return;
			}
			if (this.isEnabled)
			{
				if (replayQ <= this.Parameters.MaxReplayQForAvailableCopy)
				{
					return;
				}
				if (lastReading.IsTargetCopyHealthy)
				{
					if (this.DisableScan())
					{
						this.LogDisable(string.Format("Availability at risk. ReplayQ={0}", replayQ));
					}
					return;
				}
				if (replayQ > this.Parameters.MaxReplayQForBehindCopy || (this.keepingUp != null && this.keepingUp.ReplayQueueNotKeepingUp > 0L))
				{
					if (this.DisableScan())
					{
						this.LogDisable(string.Format("Too far behind. ReplayQ={0}", replayQ));
					}
					return;
				}
			}
			else if (lastReading.IsTargetCopyHealthy)
			{
				if (replayQ > this.Parameters.MinReplayQOfInterest)
				{
					return;
				}
				if (this.EnableScan())
				{
					this.LogEnable(string.Format("Available copy recovered. ReplayQ={0}", replayQ));
				}
				return;
			}
			else if (this.EnableScan())
			{
				this.LogEnable(string.Format("Availability is ok due to other copies. This copy can resume scanning. ReplayQ={0}", replayQ));
			}
		}

		// Token: 0x06001DD1 RID: 7633 RVA: 0x0008888A File Offset: 0x00086A8A
		private bool EnableScan()
		{
			if (!this.isEnabled)
			{
				this.isEnabled = true;
				return true;
			}
			return false;
		}

		// Token: 0x06001DD2 RID: 7634 RVA: 0x0008889E File Offset: 0x00086A9E
		private bool DisableScan()
		{
			if (this.isEnabled)
			{
				this.isEnabled = false;
				this.lastDisableTime = ExDateTime.UtcNow;
				return true;
			}
			return false;
		}

		// Token: 0x06001DD3 RID: 7635 RVA: 0x000888C0 File Offset: 0x00086AC0
		private void LogDisable(string reason)
		{
			LogReplayScanControl.Tracer.TraceError<string, string>((long)this.GetHashCode(), "LogReplayScanDisabled('{0}'): {1}", this.Database.Name, reason);
			ReplayCrimsonEvents.LogReplayScanDisabled.LogPeriodic<string, string, Guid, string>(this.GetHashCode(), DiagCore.DefaultEventSuppressionInterval, this.Database.Name, Environment.MachineName, this.Database.Guid, reason);
		}

		// Token: 0x06001DD4 RID: 7636 RVA: 0x00088928 File Offset: 0x00086B28
		private void LogEnable(string reason)
		{
			LogReplayScanControl.Tracer.TraceDebug<string, string>((long)this.GetHashCode(), "LogReplayScanEnabled('{0}'): {1}", this.Database.Name, reason);
			ReplayCrimsonEvents.LogReplayScanEnabled.LogPeriodic<string, string, Guid, string>(this.GetHashCode(), DiagCore.DefaultEventSuppressionInterval, this.Database.Name, Environment.MachineName, this.Database.Guid, reason);
		}

		// Token: 0x04000C84 RID: 3204
		private readonly bool isLagCopy;

		// Token: 0x04000C85 RID: 3205
		private IPerfmonCounters keepingUp;

		// Token: 0x04000C86 RID: 3206
		private bool isEnabled = true;

		// Token: 0x04000C87 RID: 3207
		private ExDateTime lastDisableTime;

		// Token: 0x04000C88 RID: 3208
		private AvailabilitySensor sensor;

		// Token: 0x020002E6 RID: 742
		public class ControlParameters
		{
			// Token: 0x170007EC RID: 2028
			// (get) Token: 0x06001DD5 RID: 7637 RVA: 0x0008898E File Offset: 0x00086B8E
			// (set) Token: 0x06001DD6 RID: 7638 RVA: 0x00088996 File Offset: 0x00086B96
			public int MinAvailablePassiveCopies { get; set; }

			// Token: 0x170007ED RID: 2029
			// (get) Token: 0x06001DD7 RID: 7639 RVA: 0x0008899F File Offset: 0x00086B9F
			// (set) Token: 0x06001DD8 RID: 7640 RVA: 0x000889A7 File Offset: 0x00086BA7
			public TimeSpan MinDisabledWindow { get; set; }

			// Token: 0x170007EE RID: 2030
			// (get) Token: 0x06001DD9 RID: 7641 RVA: 0x000889B0 File Offset: 0x00086BB0
			// (set) Token: 0x06001DDA RID: 7642 RVA: 0x000889B8 File Offset: 0x00086BB8
			public long MinReplayQOfInterest { get; set; }

			// Token: 0x170007EF RID: 2031
			// (get) Token: 0x06001DDB RID: 7643 RVA: 0x000889C1 File Offset: 0x00086BC1
			// (set) Token: 0x06001DDC RID: 7644 RVA: 0x000889C9 File Offset: 0x00086BC9
			public long MaxReplayQForAvailableCopy { get; set; }

			// Token: 0x170007F0 RID: 2032
			// (get) Token: 0x06001DDD RID: 7645 RVA: 0x000889D2 File Offset: 0x00086BD2
			// (set) Token: 0x06001DDE RID: 7646 RVA: 0x000889DA File Offset: 0x00086BDA
			public long MaxReplayQForBehindCopy { get; set; }

			// Token: 0x170007F1 RID: 2033
			// (get) Token: 0x06001DDF RID: 7647 RVA: 0x000889E3 File Offset: 0x00086BE3
			// (set) Token: 0x06001DE0 RID: 7648 RVA: 0x000889EB File Offset: 0x00086BEB
			public TimeSpan MaxProbeFreq { get; set; }

			// Token: 0x06001DE1 RID: 7649 RVA: 0x000889F4 File Offset: 0x00086BF4
			public ControlParameters()
			{
				this.MinDisabledWindow = TimeSpan.FromSeconds(60.0);
				this.MinAvailablePassiveCopies = 1;
				this.MinReplayQOfInterest = 5L;
				this.MaxReplayQForAvailableCopy = 100L;
				this.MaxReplayQForBehindCopy = 10000L;
				this.MaxProbeFreq = AvailabilitySensor.DefaultMaxProbeFreq;
			}
		}
	}
}
