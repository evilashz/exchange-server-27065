using System;
using System.Collections.Generic;
using Microsoft.Exchange.Cluster.Common;
using Microsoft.Exchange.Cluster.ReplayEventLog;
using Microsoft.Exchange.Cluster.Shared;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Data.HA.DirectoryServices;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Cluster.Replay;
using Microsoft.Exchange.Rpc.Cluster;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x0200022D RID: 557
	internal class ReplayLagManager : TimerComponent
	{
		// Token: 0x170005FC RID: 1532
		// (get) Token: 0x06001539 RID: 5433 RVA: 0x00053F60 File Offset: 0x00052160
		private static Trace Tracer
		{
			get
			{
				return ExTraceGlobals.MonitoringTracer;
			}
		}

		// Token: 0x0600153A RID: 5434 RVA: 0x00053F67 File Offset: 0x00052167
		public ReplayLagManager(IMonitoringADConfigProvider adConfigProvider, ICopyStatusClientLookup statusLookup) : base(TimeSpan.FromMilliseconds((double)RegistryParameters.ReplayLagManagerPollerIntervalInMsec), TimeSpan.FromMilliseconds((double)RegistryParameters.ReplayLagManagerPollerIntervalInMsec), "ReplayLagManager")
		{
			this.m_adConfigProvider = adConfigProvider;
			this.m_statusLookup = statusLookup;
			this.m_errorSuppression = new TransientDatabaseErrorSuppression();
		}

		// Token: 0x0600153B RID: 5435 RVA: 0x00053FA4 File Offset: 0x000521A4
		protected override void TimerCallbackInternal()
		{
			try
			{
				this.Run();
			}
			catch (MonitoringADServiceShuttingDownException arg)
			{
				ReplayLagManager.Tracer.TraceError<MonitoringADServiceShuttingDownException>((long)this.GetHashCode(), "DatabaseHealthMonitor: Got service shutting down exception when retrieving AD config: {0}", arg);
			}
			catch (MonitoringADConfigException ex)
			{
				ReplayLagManager.Tracer.TraceError<MonitoringADConfigException>((long)this.GetHashCode(), "ReplayLagManager: Got exception when retrieving AD config: {0}", ex);
				ReplayCrimsonEvents.ReplayLagManagerError.LogPeriodic<string, MonitoringADConfigException>(this.GetHashCode(), DiagCore.DefaultEventSuppressionInterval, ex.Message, ex);
			}
		}

		// Token: 0x0600153C RID: 5436 RVA: 0x0005402C File Offset: 0x0005222C
		private void Run()
		{
			IMonitoringADConfig config = this.m_adConfigProvider.GetConfig(true);
			if (config.ServerRole == MonitoringServerRole.Standalone)
			{
				ReplayLagManager.Tracer.TraceDebug((long)this.GetHashCode(), "ReplayLagManager: Skipping running ReplayLagManager because local server is not a member of a DAG.");
				return;
			}
			if (!config.Dag.ReplayLagManagerEnabled)
			{
				ReplayLagManager.Tracer.TraceDebug<string>((long)this.GetHashCode(), "ReplayLagManager: Skipping running ReplayLagManager because the local DAG '{0}' has ReplayLagManagerEnabled set to disabled.", config.Dag.Name);
				return;
			}
			IEnumerable<IADDatabase> enumerable = config.DatabaseMap[config.TargetServerName];
			foreach (IADDatabase db in enumerable)
			{
				this.ProcessDatabase(db, config);
			}
		}

		// Token: 0x0600153D RID: 5437 RVA: 0x000540E4 File Offset: 0x000522E4
		private void ProcessDatabase(IADDatabase db, IMonitoringADConfig adConfig)
		{
			Exception ex = null;
			try
			{
				this.ProcessDatabaseInternal(db, adConfig);
			}
			catch (ReplayLagManagerException ex2)
			{
				ex = ex2;
			}
			catch (DatabaseValidationException ex3)
			{
				ex = ex3;
			}
			catch (AmCommonTransientException ex4)
			{
				ex = ex4;
			}
			catch (AmServerException ex5)
			{
				ex = ex5;
			}
			catch (AmServerTransientException ex6)
			{
				ex = ex6;
			}
			catch (ADOperationException ex7)
			{
				ex = ex7;
			}
			catch (ADTransientException ex8)
			{
				ex = ex8;
			}
			if (ex != null)
			{
				ReplayLagManager.Tracer.TraceError<string>((long)this.GetHashCode(), "ReplayLagManager.ProcessDatabase: Got exception when processing database '{0}'. Skipping this database.", db.Name);
				ReplayCrimsonEvents.ReplayLagManagerDatabaseError.LogPeriodic<string, Guid, string, Exception>(db.Guid, DiagCore.DefaultEventSuppressionInterval, db.Name, db.Guid, ex.Message, ex);
			}
		}

		// Token: 0x0600153E RID: 5438 RVA: 0x000541D0 File Offset: 0x000523D0
		private void ProcessDatabaseInternal(IADDatabase db, IMonitoringADConfig adConfig)
		{
			if (db.ReplicationType != ReplicationType.Remote)
			{
				ReplayLagManager.Tracer.TraceDebug<string>((long)this.GetHashCode(), "ReplayLagManager.ProcessDatabase: Database '{0}' is not a replicated database. Skipping this database.", db.Name);
				return;
			}
			IADDatabaseCopy databaseCopy = this.GetDatabaseCopy(db, AmServerName.LocalComputerName);
			EnhancedTimeSpan replayLagTime = databaseCopy.ReplayLagTime;
			if (replayLagTime == EnhancedTimeSpan.Zero)
			{
				ReplayLagManager.Tracer.TraceDebug<string>((long)this.GetHashCode(), "ReplayLagManager.ProcessDatabase: Local copy of Database '{0}' is not a lag copy. Skipping this database.", db.Name);
				return;
			}
			ReplayLagManager.Tracer.TraceDebug<string, EnhancedTimeSpan>((long)this.GetHashCode(), "ReplayLagManager.ProcessDatabase: Local copy of Database '{0}' is a lag copy. Configured lag: {1}. Processing this database.", db.Name, replayLagTime);
			DatabaseAvailabilityValidator databaseAvailabilityValidator = new DatabaseAvailabilityValidator(db, ReplayLagManager.NUM_AVAILABLE_NONLAG_COPIES_MIN, this.m_statusLookup, adConfig, null, true);
			IHealthValidationResult healthValidationResult = databaseAvailabilityValidator.Run();
			int num = ReplayLagManager.NUM_AVAILABLE_NONLAG_COPIES_MIN;
			if (healthValidationResult.IsTargetCopyHealthy)
			{
				num++;
				ReplayLagManager.Tracer.TraceDebug<string, int>((long)this.GetHashCode(), "ReplayLagManager.ProcessDatabase: Local lag-copy of Database '{0}' is available. Increasing the minimum available copy count to: {1}", db.Name, num);
			}
			CopyStatusClientCachedEntry targetCopyStatus = healthValidationResult.TargetCopyStatus;
			if (healthValidationResult.HealthyCopiesCount >= num)
			{
				ReplayLagManager.Tracer.TraceDebug<string>((long)this.GetHashCode(), "ReplayLagManager.ProcessDatabase: Local copy of Database '{0}' has met availability critera. Replay Lag will be re-instated as necessary.", db.Name);
				if (this.m_errorSuppression.ReportSuccess(db.Guid, ReplayLagManager.EnableLagSuppressionWindow))
				{
					this.EnableReplayLag(db, targetCopyStatus);
					return;
				}
				ReplayLagManager.Tracer.TraceDebug<string, double>((long)this.GetHashCode(), "ReplayLagManager.ProcessDatabase: '{0}': Skipping EnableReplayLag() due to transient suppression of {1} secs", db.Name, ReplayLagManager.EnableLagSuppressionWindow.TotalSeconds);
				return;
			}
			else
			{
				ReplayLagManager.Tracer.TraceDebug<string>((long)this.GetHashCode(), "ReplayLagManager.ProcessDatabase: Local copy of Database '{0}' has *NOT* met availability critera. Replay Lag will be played down as necessary.", db.Name);
				string errorMessageWithoutFullStatus = healthValidationResult.ErrorMessageWithoutFullStatus;
				if (this.m_errorSuppression.ReportFailure(db.Guid, ReplayLagManager.DisableLagSuppressionWindow))
				{
					this.DisableReplayLag(db, targetCopyStatus, errorMessageWithoutFullStatus);
					return;
				}
				ReplayLagManager.Tracer.TraceDebug<string, double>((long)this.GetHashCode(), "ReplayLagManager.ProcessDatabase: '{0}': Skipping DisableReplayLag() due to transient suppression of {1} secs", db.Name, ReplayLagManager.DisableLagSuppressionWindow.TotalSeconds);
				ReplayCrimsonEvents.RLMDisableReplayLagRequestSuppressed.LogPeriodic<string, string, Guid, string, TimeSpan>(Environment.MachineName, DateTimeHelper.FourHours, db.Name, Environment.MachineName, db.Guid, errorMessageWithoutFullStatus, ReplayLagManager.DisableLagSuppressionWindow);
				return;
			}
		}

		// Token: 0x0600153F RID: 5439 RVA: 0x000543C8 File Offset: 0x000525C8
		private void EnableReplayLag(IADDatabase db, CopyStatusClientCachedEntry localStatus)
		{
			Exception ex = null;
			try
			{
				if (localStatus != null)
				{
					if (localStatus.Result != CopyStatusRpcResult.Success)
					{
						ReplayLagManager.Tracer.TraceError<string, Exception>((long)this.GetHashCode(), "ReplayLagManager.EnableReplayLag: '{0}': Skipping enabling lag because local GetCopyStatus RPC failed! Error: {1}", db.Name, localStatus.LastException);
						return;
					}
					if (localStatus.CopyStatus.ReplayLagEnabled == ReplayLagEnabledEnum.CmdletDisabled)
					{
						ReplayLagManager.Tracer.TraceDebug<string>((long)this.GetHashCode(), "ReplayLagManager.EnableReplayLag: '{0}': Skipping enabling lag because it is CMDLET disabled!", db.Name);
						return;
					}
					if (localStatus.CopyStatus.ReplayLagEnabled == ReplayLagEnabledEnum.Enabled)
					{
						ReplayLagManager.Tracer.TraceDebug<string>((long)this.GetHashCode(), "ReplayLagManager.EnableReplayLag: '{0}': Skipping enabling lag because it is already enabled.", db.Name);
						return;
					}
				}
				ReplayCrimsonEvents.RLMEnableReplayLagRequested.Log<string, string, Guid, TimeSpan>(db.Name, Environment.MachineName, db.Guid, ReplayLagManager.EnableLagSuppressionWindow);
				Dependencies.ReplayRpcClientWrapper.RpccEnableReplayLag(Environment.MachineName, db.Guid, ActionInitiatorType.Service);
				ReplayCrimsonEvents.RLMEnableReplayLagSuccessful.Log<string, string, Guid>(db.Name, Environment.MachineName, db.Guid);
			}
			catch (TaskServerException ex2)
			{
				ex = ex2;
			}
			catch (TaskServerTransientException ex3)
			{
				ex = ex3;
			}
			if (ex != null)
			{
				ReplayLagManager.Tracer.TraceError<string, Exception>((long)this.GetHashCode(), "ReplayLagManager.ProcessDatabase: Failed to enable replay lag for local copy of Database '{0}'. Exception: {1}", db.Name, ex);
				ReplayCrimsonEvents.RLMEnableReplayLagFailed.LogPeriodic<string, string, Guid, string, Exception>(db.Guid, DiagCore.DefaultEventSuppressionInterval, db.Name, Environment.MachineName, db.Guid, ex.Message, ex);
			}
		}

		// Token: 0x06001540 RID: 5440 RVA: 0x00054538 File Offset: 0x00052738
		private void DisableReplayLag(IADDatabase db, CopyStatusClientCachedEntry localStatus, string disableReason)
		{
			Exception ex = null;
			try
			{
				if (localStatus != null)
				{
					if (localStatus.Result == CopyStatusRpcResult.Success)
					{
						if (localStatus.CopyStatus.ReplayLagEnabled == ReplayLagEnabledEnum.CmdletDisabled)
						{
							ReplayLagManager.Tracer.TraceDebug<string>((long)this.GetHashCode(), "ReplayLagManager.DisableReplayLag: '{0}': Skipping disabling lag because it is CMDLET disabled!", db.Name);
							return;
						}
						if (localStatus.CopyStatus.ReplayLagEnabled == ReplayLagEnabledEnum.Disabled)
						{
							ReplayLagManager.Tracer.TraceDebug<string>((long)this.GetHashCode(), "ReplayLagManager.DisableReplayLag: '{0}': Skipping disabling lag because it is already disabled!", db.Name);
							return;
						}
					}
					else
					{
						ReplayLagManager.Tracer.TraceError<string, Exception>((long)this.GetHashCode(), "ReplayLagManager.DisableReplayLag: '{0}': *NOT* skipping disabling lag because local GetCopyStatus RPC failed! Error: {1}", db.Name, localStatus.LastException);
					}
				}
				ReplayLagManager.Tracer.TraceDebug<string, string>((long)this.GetHashCode(), "ReplayLagManager.DisableReplayLag: '{0}': Disabling replay lag due to reason: {1}", db.Name, disableReason);
				ReplayCrimsonEvents.RLMDisableReplayLagRequested.Log<string, string, Guid, string, TimeSpan>(db.Name, Environment.MachineName, db.Guid, disableReason, ReplayLagManager.DisableLagSuppressionWindow);
				Dependencies.ReplayRpcClientWrapper.RpccDisableReplayLag(Environment.MachineName, db.Guid, disableReason, ActionInitiatorType.Service);
				ReplayCrimsonEvents.RLMDisableReplayLagSuccessful.Log<string, string, Guid, string>(db.Name, Environment.MachineName, db.Guid, disableReason);
			}
			catch (TaskServerException ex2)
			{
				ex = ex2;
			}
			catch (TaskServerTransientException ex3)
			{
				ex = ex3;
			}
			if (ex != null)
			{
				ReplayLagManager.Tracer.TraceError<string, Exception>((long)this.GetHashCode(), "ReplayLagManager.ProcessDatabase: Failed to play down replay lag for local copy of Database '{0}'. Exception: {1}", db.Name, ex);
				ReplayCrimsonEvents.RLMDisableReplayLagFailed.LogPeriodic<string, string, Guid, string, Exception>(db.Guid, DiagCore.DefaultEventSuppressionInterval, db.Name, Environment.MachineName, db.Guid, ex.Message, ex);
			}
		}

		// Token: 0x06001541 RID: 5441 RVA: 0x000546DC File Offset: 0x000528DC
		private IADDatabaseCopy GetDatabaseCopy(IADDatabase db, AmServerName server)
		{
			IADDatabaseCopy databaseCopy = db.GetDatabaseCopy(server.NetbiosName);
			if (databaseCopy == null || !databaseCopy.IsValid)
			{
				throw new RlmDatabaseCopyInvalidException(db.Name, server.NetbiosName);
			}
			return databaseCopy;
		}

		// Token: 0x04000842 RID: 2114
		private static readonly int NUM_AVAILABLE_NONLAG_COPIES_MIN = RegistryParameters.ReplayLagManagerNumAvailableCopies;

		// Token: 0x04000843 RID: 2115
		private static readonly TimeSpan EnableLagSuppressionWindow = TimeSpan.FromSeconds((double)RegistryParameters.ReplayLagManagerEnableLagSuppressionWindowInSecs);

		// Token: 0x04000844 RID: 2116
		private static readonly TimeSpan DisableLagSuppressionWindow = TimeSpan.FromSeconds((double)RegistryParameters.ReplayLagManagerDisableLagSuppressionWindowInSecs);

		// Token: 0x04000845 RID: 2117
		private IMonitoringADConfigProvider m_adConfigProvider;

		// Token: 0x04000846 RID: 2118
		private ICopyStatusClientLookup m_statusLookup;

		// Token: 0x04000847 RID: 2119
		private TransientDatabaseErrorSuppression m_errorSuppression;
	}
}
