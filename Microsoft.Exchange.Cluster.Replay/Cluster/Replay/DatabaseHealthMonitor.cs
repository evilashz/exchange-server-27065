using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text.RegularExpressions;
using System.Threading;
using Microsoft.Exchange.Cluster.ReplayEventLog;
using Microsoft.Exchange.Cluster.Shared;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Data.HA;
using Microsoft.Exchange.Data.HA.DirectoryServices;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Cluster.Replay;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x020001F1 RID: 497
	internal class DatabaseHealthMonitor : TimerComponent
	{
		// Token: 0x1700056A RID: 1386
		// (get) Token: 0x060013A8 RID: 5032 RVA: 0x0004FC3D File Offset: 0x0004DE3D
		private static Microsoft.Exchange.Diagnostics.Trace Tracer
		{
			get
			{
				return ExTraceGlobals.MonitoringTracer;
			}
		}

		// Token: 0x060013A9 RID: 5033 RVA: 0x0004FC44 File Offset: 0x0004DE44
		public DatabaseHealthMonitor(IMonitoringADConfigProvider adConfigProvider, ICopyStatusClientLookup statusLookup) : base(TimeSpan.FromMilliseconds((double)RegistryParameters.DatabaseHealthMonitorPeriodicIntervalInMsec), TimeSpan.FromMilliseconds((double)RegistryParameters.DatabaseHealthMonitorPeriodicIntervalInMsec), "DatabaseHealthMonitor")
		{
			this.m_adConfigProvider = adConfigProvider;
			this.m_statusLookup = statusLookup;
			this.m_dbAlerts = new DatabaseLevelAlerts();
			this.m_propertyUpdateTracker = new PropertyUpdateTracker();
			this.stopWatch = new Stopwatch();
		}

		// Token: 0x060013AA RID: 5034 RVA: 0x0004FCA4 File Offset: 0x0004DEA4
		protected override void TimerCallbackInternal()
		{
			try
			{
				this.Run();
			}
			catch (MonitoringADServiceShuttingDownException arg)
			{
				DatabaseHealthMonitor.Tracer.TraceError<MonitoringADServiceShuttingDownException>((long)this.GetHashCode(), "DatabaseHealthMonitor: Got service shutting down exception when retrieving AD config: {0}", arg);
			}
			catch (MonitoringADConfigException ex)
			{
				DatabaseHealthMonitor.Tracer.TraceError<MonitoringADConfigException>((long)this.GetHashCode(), "DatabaseHealthMonitor: Got exception when retrieving AD config: {0}", ex);
				ReplayCrimsonEvents.DatabaseHealthMonitorError.LogPeriodic<string, MonitoringADConfigException>(this.GetHashCode(), DiagCore.DefaultEventSuppressionInterval, ex.Message, ex);
			}
		}

		// Token: 0x060013AB RID: 5035 RVA: 0x0004FD34 File Offset: 0x0004DF34
		private void Run()
		{
			if (!this.stopWatch.IsRunning)
			{
				this.stopWatch.Start();
			}
			IMonitoringADConfig configIgnoringStaleness = this.m_adConfigProvider.GetConfigIgnoringStaleness(true);
			if (configIgnoringStaleness.ServerRole == MonitoringServerRole.Standalone)
			{
				DatabaseHealthMonitor.Tracer.TraceDebug((long)this.GetHashCode(), "DatabaseHealthMonitor: Skipping running health checks because local server is not a member of a DAG.");
				return;
			}
			if (configIgnoringStaleness.Dag.ThirdPartyReplication == ThirdPartyReplicationMode.Enabled)
			{
				DatabaseHealthMonitor.Tracer.TraceDebug<string>((long)this.GetHashCode(), "DatabaseHealthMonitor: Dag '{0}' is in ThirdPartyReplication mode. Shutting down the DatabaseHealthMonitor component.", configIgnoringStaleness.Dag.Name);
				base.ChangeTimer(InvokeWithTimeout.InfiniteTimeSpan, InvokeWithTimeout.InfiniteTimeSpan);
				ThreadPool.QueueUserWorkItem(delegate(object stateNotUsed)
				{
					base.Stop();
				});
				return;
			}
			IEnumerable<IADDatabase> enumerable = configIgnoringStaleness.DatabasesIncludingMisconfiguredMap[configIgnoringStaleness.TargetServerName];
			HashSet<Guid> hashSet = new HashSet<Guid>();
			foreach (IADDatabase iaddatabase in enumerable)
			{
				hashSet.Add(iaddatabase.Guid);
				this.ProcessDatabase(iaddatabase, configIgnoringStaleness);
			}
			this.m_dbAlerts.Cleanup(hashSet);
			this.RaiseServerLevelAlertsIfNecessary(configIgnoringStaleness);
		}

		// Token: 0x060013AC RID: 5036 RVA: 0x0004FE5C File Offset: 0x0004E05C
		private void RaiseServerLevelAlertsIfNecessary(IMonitoringADConfig adConfig)
		{
			ServerValidationResult serverValidationResult = new ServerValidationResult(adConfig.TargetMiniServer.Name, adConfig.TargetMiniServer.Guid);
			if (this.m_serverRedundancyAlert == null)
			{
				this.m_serverRedundancyAlert = new ServerLevelDatabaseRedundancyAlert(serverValidationResult.Identity, serverValidationResult.IdentityGuid, this.m_dbAlerts.OneRedundantCopy);
			}
			this.m_serverRedundancyAlert.RaiseAppropriateAlertIfNecessary(serverValidationResult);
			serverValidationResult = new ServerValidationResult(adConfig.TargetMiniServer.Name, adConfig.TargetMiniServer.Guid);
			if (this.m_serverStaleStatusAlert == null)
			{
				this.m_serverStaleStatusAlert = new ServerLevelDatabaseStaleStatusAlert(serverValidationResult.Identity, serverValidationResult.IdentityGuid, this.m_dbAlerts.StaleStatus);
			}
			this.m_serverStaleStatusAlert.RaiseAppropriateAlertIfNecessary(serverValidationResult);
			serverValidationResult = new ServerValidationResult("", Guid.Empty);
			if (this.m_potentialRedundancyAlertByRemoteServer == null)
			{
				this.m_potentialRedundancyAlertByRemoteServer = new PotentialRedundancyAlertByRemoteServer(this.m_dbAlerts.PotentialOneRedundantCopy);
			}
			this.m_potentialRedundancyAlertByRemoteServer.RaiseAppropriateAlertIfNecessary(serverValidationResult);
		}

		// Token: 0x060013AD RID: 5037 RVA: 0x0004FF47 File Offset: 0x0004E147
		private void ProcessDatabase(IADDatabase db, IMonitoringADConfig adConfig)
		{
			if (!DatabaseHealthMonitor.ShouldMonitorDatabase(db))
			{
				this.m_dbAlerts.ResetState(db.Guid);
				return;
			}
			this.RaiseDatabaseRedundancyAlertIfNecessary(db, adConfig);
			this.RaiseDatabaseAvailabilityAlertIfNecessary(db, adConfig);
			this.RaiseDatabasePotentialRedundancyAlertIfNecessary(db, adConfig);
		}

		// Token: 0x060013AE RID: 5038 RVA: 0x0004FF7C File Offset: 0x0004E17C
		private void RaiseDatabaseRedundancyAlertIfNecessary(IADDatabase db, IMonitoringADConfig adConfig)
		{
			DatabaseHealthMonitor.Tracer.TraceDebug<string>((long)this.GetHashCode(), "DatabaseHealthMonitor.RaiseDatabaseRedundancyAlertIfNecessary: DB '{0}': Starting DB-level redundancy checks.", db.Name);
			DatabaseRedundancyValidator databaseRedundancyValidator = new DatabaseRedundancyValidator(db, DatabaseHealthMonitor.NUM_HEALTHY_COPIES_MIN, this.m_statusLookup, adConfig, this.m_propertyUpdateTracker, false);
			IHealthValidationResult result = databaseRedundancyValidator.Run();
			this.m_dbAlerts.OneRedundantCopy.RaiseAppropriateAlertIfNecessary(result);
			this.m_dbAlerts.TwoCopy.RaiseAppropriateAlertIfNecessary(result);
			this.m_dbAlerts.StaleStatus.RaiseAppropriateAlertIfNecessary(result);
			if (adConfig.Dag.DatacenterActivationMode == DatacenterActivationModeOption.DagOnly)
			{
				this.m_dbAlerts.OneRedundantCopySite.RaiseAppropriateAlertIfNecessary(result);
				return;
			}
			DatabaseHealthMonitor.Tracer.TraceDebug<string, DatacenterActivationModeOption>((long)this.GetHashCode(), "DatabaseHealthMonitor.RaiseDatabaseRedundancyAlertIfNecessary: DB '{0}': Skipping raising site alerts because DatacenterActivationMode is '{1}'.", db.Name, adConfig.Dag.DatacenterActivationMode);
		}

		// Token: 0x060013AF RID: 5039 RVA: 0x00050040 File Offset: 0x0004E240
		private void RaiseDatabasePotentialRedundancyAlertIfNecessary(IADDatabase db, IMonitoringADConfig adConfig)
		{
			if (this.stopWatch.Elapsed.TotalSeconds < (double)RegistryParameters.DatabaseHealthCheckDelayInRaisingDatabasePotentialRedundancyAlertDueToServiceStartInSec)
			{
				DatabaseHealthMonitor.Tracer.TraceDebug<string>((long)this.GetHashCode(), "DatabaseHealthMonitor.RaiseDatabasePotentialRedundancyAlertIfNecessary: Service just started, skip DB-level checks.", db.Name);
				return;
			}
			DatabaseHealthMonitor.Tracer.TraceDebug<string>((long)this.GetHashCode(), "DatabaseHealthMonitor.RaiseDatabasePotentialRedundancyAlertIfNecessary: DB '{0}': Starting DB-level checks.", db.Name);
			DatabaseReplicationNotStalledValidator databaseReplicationNotStalledValidator = new DatabaseReplicationNotStalledValidator(db, this.m_statusLookup, adConfig, this.m_propertyUpdateTracker, false);
			IHealthValidationResult healthValidationResult = databaseReplicationNotStalledValidator.Run();
			int databaseHealthCheckPotentialOneCopyTotalPassiveCopiesRequiredMin = RegistryParameters.DatabaseHealthCheckPotentialOneCopyTotalPassiveCopiesRequiredMin;
			if (!healthValidationResult.IsValidationSuccessful && healthValidationResult.TotalPassiveCopiesCount < databaseHealthCheckPotentialOneCopyTotalPassiveCopiesRequiredMin)
			{
				DatabaseHealthMonitor.Tracer.TraceDebug<string>((long)this.GetHashCode(), "DatabaseHealthMonitor.RaiseDatabasePotentialRedundancyAlertIfNecessary: DB '{0}': Skipping raising potential redundancy alert because not enough passive copies are found", db.Name);
				string text = string.Format("{0}-NotEnoughTotalPassiveCopies", healthValidationResult.Identity);
				ReplayCrimsonEvents.DatabaseLevelPotentialRedundancyCheckFailedButNotEnoughTotalPassiveCopies.LogPeriodic<string, int, int, int, int, string, Guid>(text, TimeSpan.FromMinutes(60.0), healthValidationResult.Identity, healthValidationResult.HealthyCopiesCount, healthValidationResult.HealthyPassiveCopiesCount, healthValidationResult.TotalPassiveCopiesCount, databaseHealthCheckPotentialOneCopyTotalPassiveCopiesRequiredMin, "", healthValidationResult.IdentityGuid);
				return;
			}
			this.m_dbAlerts.PotentialOneRedundantCopy.RaiseAppropriateAlertIfNecessary(healthValidationResult);
		}

		// Token: 0x060013B0 RID: 5040 RVA: 0x00050154 File Offset: 0x0004E354
		private void RaiseDatabaseAvailabilityAlertIfNecessary(IADDatabase db, IMonitoringADConfig adConfig)
		{
			DatabaseHealthMonitor.Tracer.TraceDebug<string>((long)this.GetHashCode(), "DatabaseHealthMonitor.RaiseDatabaseAvailabilityAlertIfNecessary: DB '{0}': Starting DB-level availability checks.", db.Name);
			DatabaseAvailabilityValidator databaseAvailabilityValidator = new DatabaseAvailabilityValidator(db, DatabaseHealthMonitor.NUM_HEALTHY_COPIES_MIN, this.m_statusLookup, adConfig, this.m_propertyUpdateTracker, false);
			IHealthValidationResult result = databaseAvailabilityValidator.Run();
			this.m_dbAlerts.OneAvailableCopy.RaiseAppropriateAlertIfNecessary(result);
			if (adConfig.Dag.DatacenterActivationMode == DatacenterActivationModeOption.DagOnly)
			{
				this.m_dbAlerts.OneAvailableCopySite.RaiseAppropriateAlertIfNecessary(result);
				return;
			}
			DatabaseHealthMonitor.Tracer.TraceDebug<string, DatacenterActivationModeOption>((long)this.GetHashCode(), "DatabaseHealthMonitor.RaiseDatabaseAvailabilityAlertIfNecessary: DB '{0}': Skipping raising site alerts because DatacenterActivationMode is '{1}'.", db.Name, adConfig.Dag.DatacenterActivationMode);
		}

		// Token: 0x060013B1 RID: 5041 RVA: 0x000501F8 File Offset: 0x0004E3F8
		public static bool ShouldMonitorDatabase(IADDatabase db)
		{
			if (db.Recovery)
			{
				DatabaseHealthMonitor.Tracer.TraceDebug<string>(0L, "ShouldMonitorDatabase: Skipping monitoring of database '{0}' because it is a Recovery DB.", db.Name);
				return false;
			}
			if (db.AutoDagExcludeFromMonitoring)
			{
				DatabaseHealthMonitor.Tracer.TraceDebug<string>(0L, "ShouldMonitorDatabase: Skipping monitoring of database '{0}' because it has been excluded from monitoring (AutoDagExcludeFromMonitoring = true).", db.Name);
				return false;
			}
			string databaseHealthCheckSkipDatabasesRegex = RegistryParameters.DatabaseHealthCheckSkipDatabasesRegex;
			if (!string.IsNullOrEmpty(databaseHealthCheckSkipDatabasesRegex) && Regex.IsMatch(db.Name, databaseHealthCheckSkipDatabasesRegex, RegexOptions.IgnoreCase))
			{
				DatabaseHealthMonitor.Tracer.TraceDebug<string, string>(0L, "ShouldMonitorDatabase: Skipping monitoring of database '{0}' because it has been excluded from monitoring since it matches the skip Regex:  {1}", db.Name, databaseHealthCheckSkipDatabasesRegex);
				return false;
			}
			return true;
		}

		// Token: 0x04000795 RID: 1941
		public static readonly int NUM_HEALTHY_COPIES_MIN = RegistryParameters.DatabaseHealthCheckAtLeastNCopies;

		// Token: 0x04000796 RID: 1942
		private IMonitoringADConfigProvider m_adConfigProvider;

		// Token: 0x04000797 RID: 1943
		private ICopyStatusClientLookup m_statusLookup;

		// Token: 0x04000798 RID: 1944
		private DatabaseLevelAlerts m_dbAlerts;

		// Token: 0x04000799 RID: 1945
		private ServerLevelDatabaseRedundancyAlert m_serverRedundancyAlert;

		// Token: 0x0400079A RID: 1946
		private ServerLevelDatabaseStaleStatusAlert m_serverStaleStatusAlert;

		// Token: 0x0400079B RID: 1947
		private PotentialRedundancyAlertByRemoteServer m_potentialRedundancyAlertByRemoteServer;

		// Token: 0x0400079C RID: 1948
		private PropertyUpdateTracker m_propertyUpdateTracker;

		// Token: 0x0400079D RID: 1949
		private Stopwatch stopWatch;
	}
}
