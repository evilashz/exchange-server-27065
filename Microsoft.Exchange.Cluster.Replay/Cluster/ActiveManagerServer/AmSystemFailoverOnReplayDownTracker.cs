using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Microsoft.Exchange.Cluster.Shared;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.Cluster.ActiveManagerServer
{
	// Token: 0x02000092 RID: 146
	internal class AmSystemFailoverOnReplayDownTracker : AmThrottledActionTracker<FailoverData>
	{
		// Token: 0x06000591 RID: 1425 RVA: 0x0001BD70 File Offset: 0x00019F70
		internal AmSystemFailoverOnReplayDownTracker() : base("ServerLevelFailover", 1)
		{
			base.MaxHistorySize = RegistryParameters.OnReplDownMaxAllowedFailoversPerNodeInADay;
			if (base.MaxHistorySize < 1)
			{
				base.MaxHistorySize = 1;
			}
		}

		// Token: 0x06000592 RID: 1426 RVA: 0x0001BDC8 File Offset: 0x00019FC8
		internal void MarkReplayDown(AmServerName node, bool isForce = false)
		{
			lock (this.locker)
			{
				ExDateTime replayDownFromTime = this.GetReplayDownFromTime(node);
				if (isForce || replayDownFromTime == ExDateTime.MinValue)
				{
					this.replayDownMap[node] = ExDateTime.Now;
				}
			}
		}

		// Token: 0x06000593 RID: 1427 RVA: 0x0001BE2C File Offset: 0x0001A02C
		internal void MarkReplayUp(AmServerName node)
		{
			lock (this.locker)
			{
				if (this.replayDownMap.Remove(node))
				{
					this.RemoveTimer(node);
				}
			}
		}

		// Token: 0x06000594 RID: 1428 RVA: 0x0001BE7C File Offset: 0x0001A07C
		internal ExDateTime GetReplayDownFromTime(AmServerName node)
		{
			ExDateTime result;
			lock (this.locker)
			{
				ExDateTime minValue;
				if (!this.replayDownMap.TryGetValue(node, out minValue))
				{
					minValue = ExDateTime.MinValue;
				}
				result = minValue;
			}
			return result;
		}

		// Token: 0x06000595 RID: 1429 RVA: 0x0001BED0 File Offset: 0x0001A0D0
		internal override void Cleanup()
		{
			lock (this.locker)
			{
				this.replayDownMap.Clear();
				this.RemoveAllTimers();
				base.Cleanup();
			}
		}

		// Token: 0x06000596 RID: 1430 RVA: 0x0001BF24 File Offset: 0x0001A124
		internal bool IsFailoverScheduled(AmServerName serverName)
		{
			bool result;
			lock (this.locker)
			{
				TimerComponent timerComponent;
				this.scheduledFailovers.TryGetValue(serverName, out timerComponent);
				result = (timerComponent != null);
			}
			return result;
		}

		// Token: 0x06000597 RID: 1431 RVA: 0x0001BF78 File Offset: 0x0001A178
		internal void ScheduleFailover(AmServerName serverName)
		{
			lock (this.locker)
			{
				if (!this.IsFailoverScheduled(serverName))
				{
					ExDateTime replayDownFromTime = this.GetReplayDownFromTime(serverName);
					if (replayDownFromTime != ExDateTime.MinValue)
					{
						int onReplDownConfirmDurationBeforeFailoverInSecs = RegistryParameters.OnReplDownConfirmDurationBeforeFailoverInSecs;
						AmTrace.Debug("AmSystemFailoverOnReplayDownTracker finds {0} down. Timer scheduled in {1} sec", new object[]
						{
							serverName.NetbiosName,
							onReplDownConfirmDurationBeforeFailoverInSecs
						});
						AmSystemFailoverOnReplayDownTimer amSystemFailoverOnReplayDownTimer = new AmSystemFailoverOnReplayDownTimer(serverName, TimeSpan.FromSeconds((double)onReplDownConfirmDurationBeforeFailoverInSecs));
						this.scheduledFailovers[serverName] = amSystemFailoverOnReplayDownTimer;
						amSystemFailoverOnReplayDownTimer.Start();
					}
				}
			}
		}

		// Token: 0x06000598 RID: 1432 RVA: 0x0001C024 File Offset: 0x0001A224
		internal void AddFailoverEntry(AmServerName serverName)
		{
			base.AddEntry(serverName, new FailoverData());
		}

		// Token: 0x06000599 RID: 1433 RVA: 0x0001C034 File Offset: 0x0001A234
		internal AmThrottledActionTracker<FailoverData>.ThrottlingShapshot GetThrottlingSnapshot(AmServerName serverName)
		{
			TimeSpan minDurationBetweenActionsPerNode = TimeSpan.FromSeconds((double)RegistryParameters.OnReplDownDurationBetweenFailoversInSecs);
			TimeSpan maxCheckDurationPerNode = TimeSpan.FromDays(1.0);
			int onReplDownMaxAllowedFailoversPerNodeInADay = RegistryParameters.OnReplDownMaxAllowedFailoversPerNodeInADay;
			TimeSpan minDurationBetweenActionsAcrossDag = TimeSpan.FromSeconds((double)RegistryParameters.OnReplDownDurationBetweenFailoversInSecs);
			TimeSpan maxCheckDurationAcrossDag = TimeSpan.FromDays(1.0);
			int onReplDownMaxAllowedFailoversAcrossDagInADay = RegistryParameters.OnReplDownMaxAllowedFailoversAcrossDagInADay;
			return base.GetThrottlingSnapshot(serverName, minDurationBetweenActionsPerNode, maxCheckDurationPerNode, onReplDownMaxAllowedFailoversPerNodeInADay, minDurationBetweenActionsAcrossDag, maxCheckDurationAcrossDag, onReplDownMaxAllowedFailoversAcrossDagInADay);
		}

		// Token: 0x0600059A RID: 1434 RVA: 0x0001C0A8 File Offset: 0x0001A2A8
		internal void RemoveTimer(AmServerName serverName)
		{
			lock (this.locker)
			{
				TimerComponent state = null;
				if (this.scheduledFailovers.TryGetValue(serverName, out state))
				{
					this.scheduledFailovers[serverName] = null;
					ThreadPool.QueueUserWorkItem(delegate(object timerObject)
					{
						if (timerObject != null)
						{
							((TimerComponent)timerObject).Dispose();
						}
					}, state);
				}
			}
		}

		// Token: 0x0600059B RID: 1435 RVA: 0x0001C128 File Offset: 0x0001A328
		internal void RemoveAllTimers()
		{
			TimerComponent[] array = null;
			lock (this.locker)
			{
				array = this.scheduledFailovers.Values.ToArray<TimerComponent>();
				this.scheduledFailovers.Clear();
			}
			foreach (TimerComponent timerComponent in array)
			{
				if (timerComponent != null)
				{
					timerComponent.Dispose();
				}
			}
		}

		// Token: 0x0600059C RID: 1436 RVA: 0x0001C1A8 File Offset: 0x0001A3A8
		internal static void RemoveFailoverEntryFromClusterDatabase(string serverName)
		{
			AmThrottledActionTracker<FailoverData>.RemoveEntryFromClusdb(serverName, "ServerLevelFailover");
		}

		// Token: 0x0400024B RID: 587
		private const string ServerLevelFailoverAction = "ServerLevelFailover";

		// Token: 0x0400024C RID: 588
		private readonly object locker = new object();

		// Token: 0x0400024D RID: 589
		private readonly Dictionary<AmServerName, ExDateTime> replayDownMap = new Dictionary<AmServerName, ExDateTime>();

		// Token: 0x0400024E RID: 590
		private readonly Dictionary<AmServerName, TimerComponent> scheduledFailovers = new Dictionary<AmServerName, TimerComponent>();
	}
}
