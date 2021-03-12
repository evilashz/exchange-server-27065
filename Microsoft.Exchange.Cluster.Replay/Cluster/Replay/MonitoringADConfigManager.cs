using System;
using Microsoft.Exchange.Cluster.ClusApi;
using Microsoft.Exchange.Cluster.ReplayEventLog;
using Microsoft.Exchange.Cluster.Shared;
using Microsoft.Exchange.Data.HA;
using Microsoft.Exchange.Data.HA.DirectoryServices;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Cluster.Replay;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x0200022A RID: 554
	internal class MonitoringADConfigManager : TimerComponent, IMonitoringADConfigProvider
	{
		// Token: 0x170005F0 RID: 1520
		// (get) Token: 0x06001517 RID: 5399 RVA: 0x00053952 File Offset: 0x00051B52
		private static Trace Tracer
		{
			get
			{
				return ExTraceGlobals.MonitoringTracer;
			}
		}

		// Token: 0x06001518 RID: 5400 RVA: 0x0005395C File Offset: 0x00051B5C
		public MonitoringADConfigManager(IReplayAdObjectLookup adObjectLookup, IReplayAdObjectLookup adObjectLookupPartiallyConsistent, IADToplogyConfigurationSession adSession, IADToplogyConfigurationSession adSessionPartiallyConsistent) : base(TimeSpan.Zero, TimeSpan.FromMilliseconds((double)RegistryParameters.MonitoringADConfigManagerIntervalInMsec), "MonitoringADConfigManager")
		{
			this.m_adObjectLookup = adObjectLookup;
			this.m_adObjectLookupPartiallyConsistent = adObjectLookupPartiallyConsistent;
			this.m_adSession = adSession;
			this.m_adSessionPartiallyConsistent = adSessionPartiallyConsistent;
		}

		// Token: 0x170005F1 RID: 1521
		// (get) Token: 0x06001519 RID: 5401 RVA: 0x000539C7 File Offset: 0x00051BC7
		// (set) Token: 0x0600151A RID: 5402 RVA: 0x000539CF File Offset: 0x00051BCF
		public Exception LastException { get; private set; }

		// Token: 0x0600151B RID: 5403 RVA: 0x000539D8 File Offset: 0x00051BD8
		public IMonitoringADConfig GetRecentConfig(bool waitForInit = true)
		{
			return this.GetConfig(waitForInit, MonitoringADConfigManager.CachedConfigShortTTL);
		}

		// Token: 0x0600151C RID: 5404 RVA: 0x000539E6 File Offset: 0x00051BE6
		public IMonitoringADConfig GetConfigIgnoringStaleness(bool waitForInit = true)
		{
			return this.GetConfig(waitForInit, InvokeWithTimeout.InfiniteTimeSpan);
		}

		// Token: 0x0600151D RID: 5405 RVA: 0x000539F4 File Offset: 0x00051BF4
		public IMonitoringADConfig GetConfig(bool waitForInit = true)
		{
			return this.GetConfig(waitForInit, MonitoringADConfigManager.CachedConfigLongTTL);
		}

		// Token: 0x0600151E RID: 5406 RVA: 0x00053A04 File Offset: 0x00051C04
		private IMonitoringADConfig GetConfig(bool waitForInit, TimeSpan allowedStaleness)
		{
			if (waitForInit)
			{
				TimeSpan getConfigWaitTimeout = MonitoringADConfigManager.GetConfigWaitTimeout;
				ManualOneShotEvent.Result result = this.m_firstLookupCompleted.WaitOne(getConfigWaitTimeout);
				if (result == ManualOneShotEvent.Result.WaitTimedOut)
				{
					MonitoringADConfigManager.Tracer.TraceError<TimeSpan>((long)this.GetHashCode(), "MonitoringADConfigManager.GetConfig(): AD initial lookup timed out after {0}.", getConfigWaitTimeout);
					throw new MonitoringADFirstLookupTimeoutException((int)getConfigWaitTimeout.TotalMilliseconds);
				}
				if (result == ManualOneShotEvent.Result.ShuttingDown)
				{
					MonitoringADConfigManager.Tracer.TraceError((long)this.GetHashCode(), "MonitoringADConfigManager.GetConfig(): m_firstLookupCompleted event is null, which means the service is shutting down!");
					throw new MonitoringADServiceShuttingDownException();
				}
			}
			Exception ex = null;
			IMonitoringADConfig monitoringADConfig = null;
			lock (this.m_locker)
			{
				monitoringADConfig = this.m_config;
				ex = this.LastException;
			}
			if (monitoringADConfig != null)
			{
				TimeSpan timeSpan = DateTime.UtcNow.Subtract(monitoringADConfig.CreateTimeUtc);
				if (allowedStaleness != InvokeWithTimeout.InfiniteTimeSpan)
				{
					if (timeSpan > allowedStaleness)
					{
						MonitoringADConfigManager.Tracer.TraceError<TimeSpan, TimeSpan>((long)this.GetHashCode(), "MonitoringADConfigManager.GetConfig(): Cached config is older ({0}) than max TTL ({1})", timeSpan, allowedStaleness);
						throw new MonitoringADConfigStaleException(timeSpan.ToString(), allowedStaleness.ToString(), AmExceptionHelper.GetExceptionMessageOrNoneString(ex), ex);
					}
				}
				else
				{
					MonitoringADConfigManager.Tracer.TraceDebug((long)this.GetHashCode(), "MonitoringADConfigManager.GetConfig(): Ignoring cached config staleness check.");
				}
				MonitoringADConfigManager.Tracer.TraceDebug<TimeSpan, DateTime>((long)this.GetHashCode(), "MonitoringADConfigManager.GetConfig(): Returning cached config of age ({0}), created at '{1} UTC'", timeSpan, monitoringADConfig.CreateTimeUtc);
				return monitoringADConfig;
			}
			if (ex == null)
			{
				MonitoringADConfigManager.Tracer.TraceError((long)this.GetHashCode(), "MonitoringADConfigManager.GetConfig(): AD initial lookup has not completed yet.");
				throw new MonitoringADInitNotCompleteException();
			}
			MonitoringADConfigManager.Tracer.TraceError<Exception>((long)this.GetHashCode(), "MonitoringADConfigManager.GetConfig(): Throwing last exception: {0}", ex);
			throw ex;
		}

		// Token: 0x0600151F RID: 5407 RVA: 0x00053B90 File Offset: 0x00051D90
		protected override void TimerCallbackInternal()
		{
			ExTraceGlobals.ADCacheTracer.TraceDebug((long)this.GetHashCode(), "Refresh: MonitoringADConfigManager.TimerCallbackInternal");
			this.RefreshInternal();
		}

		// Token: 0x06001520 RID: 5408 RVA: 0x00053BB8 File Offset: 0x00051DB8
		public void TryRefreshConfig()
		{
			try
			{
				InvokeWithTimeout.Invoke(delegate()
				{
					this.RefreshInternal();
				}, MonitoringADConfigManager.ConfigRefreshTimeout);
			}
			catch (TimeoutException ex)
			{
				MonitoringADConfigManager.Tracer.TraceError<TimeoutException>((long)this.GetHashCode(), "MonitoringADConfigManager.RunRefresh failed: {0}", ex);
				ReplayCrimsonEvents.MonitoringADLookupError.LogPeriodic<string, TimeoutException>(this.GetHashCode(), DiagCore.DefaultEventSuppressionInterval, ex.Message, ex);
				this.healthReporter.RaiseRedEvent(ex.Message);
			}
		}

		// Token: 0x06001521 RID: 5409 RVA: 0x00053C4C File Offset: 0x00051E4C
		private void RefreshInternal()
		{
			MonitoringADConfig config = null;
			Exception ex = null;
			try
			{
				Dependencies.ReplayAdObjectLookup.Clear();
				config = MonitoringADConfig.GetConfig(new AmServerName(Dependencies.ManagementClassHelper.LocalComputerFqdn), this.m_adObjectLookup, this.m_adObjectLookupPartiallyConsistent, this.m_adSession, this.m_adSessionPartiallyConsistent, () => base.PrepareToStopCalled);
				this.healthReporter.RaiseGreenEvent();
			}
			catch (MonitoringADConfigException ex2)
			{
				ex = ex2;
				ReplayCrimsonEvents.MonitoringADLookupError.LogPeriodic<string, MonitoringADConfigException>(this.GetHashCode(), DiagCore.DefaultEventSuppressionInterval, ex2.ErrorMsg, ex2);
				this.healthReporter.RaiseRedEvent(ex2.ErrorMsg);
			}
			finally
			{
				lock (this.m_locker)
				{
					if (ex == null)
					{
						this.m_config = config;
					}
					this.LastException = ex;
				}
				this.m_firstLookupCompleted.Set();
			}
		}

		// Token: 0x06001522 RID: 5410 RVA: 0x00053D58 File Offset: 0x00051F58
		protected override void StopInternal()
		{
			base.StopInternal();
			this.m_firstLookupCompleted.Close();
		}

		// Token: 0x0400082F RID: 2095
		private const string FirstMonitoringADLookupCompletedEventName = "FirstMonitoringADLookupCompletedEvent";

		// Token: 0x04000830 RID: 2096
		private static readonly TimeSpan CachedConfigLongTTL = TimeSpan.FromSeconds((double)RegistryParameters.MonitoringADConfigStaleTimeoutLongInSec);

		// Token: 0x04000831 RID: 2097
		private static readonly TimeSpan CachedConfigShortTTL = TimeSpan.FromSeconds((double)RegistryParameters.MonitoringADConfigStaleTimeoutShortInSec);

		// Token: 0x04000832 RID: 2098
		private static readonly TimeSpan ConfigRefreshTimeout = TimeSpan.FromSeconds((double)RegistryParameters.MonitoringADGetConfigTimeoutInSec);

		// Token: 0x04000833 RID: 2099
		private static readonly TimeSpan GetConfigWaitTimeout = TimeSpan.FromSeconds((double)RegistryParameters.MonitoringADGetConfigTimeoutInSec);

		// Token: 0x04000834 RID: 2100
		private IMonitoringADConfig m_config;

		// Token: 0x04000835 RID: 2101
		private IReplayAdObjectLookup m_adObjectLookup;

		// Token: 0x04000836 RID: 2102
		private IReplayAdObjectLookup m_adObjectLookupPartiallyConsistent;

		// Token: 0x04000837 RID: 2103
		private IADToplogyConfigurationSession m_adSession;

		// Token: 0x04000838 RID: 2104
		private IADToplogyConfigurationSession m_adSessionPartiallyConsistent;

		// Token: 0x04000839 RID: 2105
		private object m_locker = new object();

		// Token: 0x0400083A RID: 2106
		private ADHealthReporter healthReporter = new ADHealthReporter();

		// Token: 0x0400083B RID: 2107
		private ManualOneShotEvent m_firstLookupCompleted = new ManualOneShotEvent("FirstMonitoringADLookupCompletedEvent");
	}
}
