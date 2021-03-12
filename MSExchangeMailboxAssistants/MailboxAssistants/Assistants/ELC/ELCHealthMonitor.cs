using System;
using System.Collections.Generic;
using System.Threading;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.ResourceHealth;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Data.Storage.ResourceHealth;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.MailboxAssistants.Assistants.ELC;
using Microsoft.Exchange.WorkloadManagement;

namespace Microsoft.Exchange.MailboxAssistants.Assistants.ELC
{
	// Token: 0x02000047 RID: 71
	internal class ELCHealthMonitor : IDisposable
	{
		// Token: 0x06000298 RID: 664 RVA: 0x0000F900 File Offset: 0x0000DB00
		public ELCHealthMonitor(Guid databaseGuid, string databaseName, ResourceKey[] primaryResourceDependencies)
		{
			this.databaseGuid = databaseGuid;
			this.databaseName = databaseName;
			this.budget = StandardBudget.AcquireUnthrottledBudget(this.databaseGuid.ToString(), BudgetType.ResourceTracking);
			this.primaryResourceDependencies = primaryResourceDependencies;
			this.sleepWaitHandle = new EventWaitHandle(false, EventResetMode.ManualReset);
		}

		// Token: 0x06000299 RID: 665 RVA: 0x0000F953 File Offset: 0x0000DB53
		public void Dispose()
		{
			if (this.sleepWaitHandle != null)
			{
				this.sleepWaitHandle.Close();
				this.sleepWaitHandle = null;
			}
		}

		// Token: 0x0600029A RID: 666 RVA: 0x0000F96F File Offset: 0x0000DB6F
		public void EnableLoadTrackingOnSession(MailboxSession mailboxSession)
		{
			mailboxSession.AccountingObject = this.budget;
		}

		// Token: 0x0600029B RID: 667 RVA: 0x0000F97D File Offset: 0x0000DB7D
		public int ThrottleStoreCall()
		{
			return this.InternalThrottleStoreCall(null);
		}

		// Token: 0x0600029C RID: 668 RVA: 0x0000F986 File Offset: 0x0000DB86
		public int ThrottleStoreAndArchiveCall(List<ResourceKey> archiveResourceDependencies)
		{
			return this.InternalThrottleStoreCall(archiveResourceDependencies);
		}

		// Token: 0x0600029D RID: 669 RVA: 0x0000F990 File Offset: 0x0000DB90
		public static List<ResourceKey> GetArchiveResourceHealthMonitorKeys(MailboxSession archiveSession, MailboxSession primarySession)
		{
			List<ResourceKey> list = null;
			if (archiveSession != null && !archiveSession.IsRemote && !archiveSession.MdbGuid.Equals(primarySession.MdbGuid))
			{
				list = new List<ResourceKey>();
				list.Add(new MdbResourceHealthMonitorKey(archiveSession.MdbGuid));
				list.Add(new MdbReplicationResourceHealthMonitorKey(archiveSession.MdbGuid));
				list.Add(new MdbAvailabilityResourceHealthMonitorKey(archiveSession.MdbGuid));
			}
			return list;
		}

		// Token: 0x0600029E RID: 670 RVA: 0x0000FAF8 File Offset: 0x0000DCF8
		private int InternalThrottleStoreCall(List<ResourceKey> archiveResourceDependencies)
		{
			WorkloadSettings settings = new WorkloadSettings(WorkloadType.ELCAssistant, true);
			ResourceKey[] array = (archiveResourceDependencies != null) ? archiveResourceDependencies.ToArray() : null;
			ResourceUnhealthyException ex;
			if (ResourceLoadDelayInfo.TryCheckResourceHealth(this.budget, settings, this.primaryResourceDependencies, out ex))
			{
				ELCHealthMonitor.Tracer.TraceDebug<ResourceKey>((long)this.GetHashCode(), "ELCHealthMonitor: Unhealthy database resource {0}", ex.ResourceKey);
				ELCPerfmon.HealthMonitorUnhealthy.Increment();
				throw ex;
			}
			if (array != null && array.Length > 0 && ResourceLoadDelayInfo.TryCheckResourceHealth(this.budget, settings, array, out ex))
			{
				ELCHealthMonitor.Tracer.TraceDebug<ResourceKey>((long)this.GetHashCode(), "ELCHealthMonitor: Unhealthy archive database resource {0}", ex.ResourceKey);
				ELCPerfmon.HealthMonitorUnhealthy.Increment();
				throw ex;
			}
			int backoff = 0;
			ResourceLoadDelayInfo.EnforceDelay(this.budget, settings, this.primaryResourceDependencies, TimeSpan.MaxValue, delegate(DelayInfo delayInfo)
			{
				ELCHealthMonitor.Tracer.TraceDebug((long)this.GetHashCode(), "ELCHealthMonitor: Backoff for database {0} ({1}) with resource {2} doing a wait of {3} ms", new object[]
				{
					this.databaseName,
					this.databaseGuid,
					ResourceLoadDelayInfo.GetInstance(delayInfo),
					delayInfo.Delay.TotalMilliseconds
				});
				backoff = (int)delayInfo.Delay.TotalMilliseconds;
				ELCPerfmon.HealthMonitorDelayRate.Increment();
				return true;
			});
			ELCHealthMonitor.averageDelay.Update((float)backoff);
			ELCPerfmon.HealthMonitorAverageDelay.RawValue = (long)ELCHealthMonitor.averageDelay.Value;
			if (array != null && array.Length > 0)
			{
				backoff = 0;
				ResourceLoadDelayInfo.EnforceDelay(this.budget, settings, array, TimeSpan.MaxValue, delegate(DelayInfo delayInfo)
				{
					ELCHealthMonitor.Tracer.TraceDebug<string, double>((long)this.GetHashCode(), "ELCHealthMonitor: Backoff for archive resource {0} doing a wait of {1} ms", ResourceLoadDelayInfo.GetInstance(delayInfo), delayInfo.Delay.TotalMilliseconds);
					backoff = (int)delayInfo.Delay.TotalMilliseconds;
					ELCPerfmon.HealthMonitorDelayRate.Increment();
					return true;
				});
				ELCHealthMonitor.averageDelay.Update((float)backoff);
				ELCPerfmon.HealthMonitorAverageDelay.RawValue = (long)ELCHealthMonitor.averageDelay.Value;
			}
			if (ResourceLoadDelayInfo.TryCheckResourceHealth(this.budget, settings, this.primaryResourceDependencies, out ex))
			{
				ELCHealthMonitor.Tracer.TraceDebug<ResourceKey>((long)this.GetHashCode(), "ELCHealthMonitor: Unhealthy database resource {0} after the delay", ex.ResourceKey);
				ELCPerfmon.HealthMonitorUnhealthy.Increment();
				throw ex;
			}
			if (array != null && array.Length > 0 && ResourceLoadDelayInfo.TryCheckResourceHealth(this.budget, settings, array, out ex))
			{
				ELCHealthMonitor.Tracer.TraceDebug<ResourceKey>((long)this.GetHashCode(), "ELCHealthMonitor: Unhealthy archive database resource {0} after the delay", ex.ResourceKey);
				ELCPerfmon.HealthMonitorUnhealthy.Increment();
				throw ex;
			}
			return backoff;
		}

		// Token: 0x0600029F RID: 671 RVA: 0x0000FCEA File Offset: 0x0000DEEA
		public void OnShutdown()
		{
			ELCHealthMonitor.Tracer.TraceDebug<string, Guid>((long)this.GetHashCode(), "ELCHealthMonitor: OnShutdown started for database DB {0} (guid {1})", this.databaseName, this.databaseGuid);
			this.sleepWaitHandle.Reset();
		}

		// Token: 0x060002A0 RID: 672 RVA: 0x0000FD1A File Offset: 0x0000DF1A
		public override string ToString()
		{
			if (this.toString == null)
			{
				this.toString = "ELCHealthMonitor: ELC Health Manager for " + this.databaseName;
			}
			return this.toString;
		}

		// Token: 0x04000237 RID: 567
		private static readonly Trace Tracer = ExTraceGlobals.ELCAssistantTracer;

		// Token: 0x04000238 RID: 568
		private static readonly ushort NumberOfDelaySamples = 100;

		// Token: 0x04000239 RID: 569
		private static RunningAverageFloat averageDelay = new RunningAverageFloat(ELCHealthMonitor.NumberOfDelaySamples);

		// Token: 0x0400023A RID: 570
		private string toString;

		// Token: 0x0400023B RID: 571
		private Guid databaseGuid;

		// Token: 0x0400023C RID: 572
		private string databaseName;

		// Token: 0x0400023D RID: 573
		private IStandardBudget budget;

		// Token: 0x0400023E RID: 574
		private ResourceKey[] primaryResourceDependencies;

		// Token: 0x0400023F RID: 575
		private EventWaitHandle sleepWaitHandle;
	}
}
