using System;
using System.Collections.Generic;
using System.Threading;
using Microsoft.Exchange.Cluster.ActiveManagerServer;
using Microsoft.Exchange.Cluster.Shared;
using Microsoft.Exchange.Data.HA.DirectoryServices;
using Microsoft.Exchange.Data.Storage.ActiveManager;
using Microsoft.Exchange.Rpc.Cluster;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x020001F2 RID: 498
	public class DatabaseHealthValidationRunner
	{
		// Token: 0x1700056B RID: 1387
		// (get) Token: 0x060013B4 RID: 5044 RVA: 0x0005028B File Offset: 0x0004E48B
		// (set) Token: 0x060013B5 RID: 5045 RVA: 0x00050293 File Offset: 0x0004E493
		private MonitoringADConfig ADConfig { get; set; }

		// Token: 0x1700056C RID: 1388
		// (get) Token: 0x060013B6 RID: 5046 RVA: 0x0005029C File Offset: 0x0004E49C
		// (set) Token: 0x060013B7 RID: 5047 RVA: 0x000502A4 File Offset: 0x0004E4A4
		private ICopyStatusClientLookup CopyStatusLookup { get; set; }

		// Token: 0x060013B8 RID: 5048 RVA: 0x000502AD File Offset: 0x0004E4AD
		public DatabaseHealthValidationRunner(string serverName)
		{
			this.m_serverName = serverName;
		}

		// Token: 0x060013B9 RID: 5049 RVA: 0x000502BC File Offset: 0x0004E4BC
		public void Initialize()
		{
			if (Interlocked.CompareExchange(ref this.m_fInitialized, 1, 0) == 1)
			{
				return;
			}
			this.ADConfig = MonitoringADConfig.GetConfig(new AmServerName(this.m_serverName, true), Dependencies.ReplayAdObjectLookup, Dependencies.ReplayAdObjectLookupPartiallyConsistent, ADSessionFactory.CreateIgnoreInvalidRootOrgSession(true), ADSessionFactory.CreatePartiallyConsistentRootOrgSession(true), null);
			ActiveManager noncachingActiveManagerInstance = ActiveManager.GetNoncachingActiveManagerInstance();
			AmMultiNodeCopyStatusFetcher amMultiNodeCopyStatusFetcher = new AmMultiNodeCopyStatusFetcher(this.ADConfig.AmServerNames, this.ADConfig.DatabaseMap, RpcGetDatabaseCopyStatusFlags2.None, noncachingActiveManagerInstance, false);
			Dictionary<Guid, Dictionary<AmServerName, CopyStatusClientCachedEntry>> status = amMultiNodeCopyStatusFetcher.GetStatus();
			CopyStatusClientLookupTable copyStatusClientLookupTable = new CopyStatusClientLookupTable();
			copyStatusClientLookupTable.UpdateCopyStatusCachedEntries(status);
			this.CopyStatusLookup = new CopyStatusClientLookup(copyStatusClientLookupTable, null, noncachingActiveManagerInstance);
		}

		// Token: 0x060013BA RID: 5050 RVA: 0x0005036A File Offset: 0x0004E56A
		public IEnumerable<IHealthValidationResultMinimal> RunDatabaseRedundancyChecks(Guid? dbGuid = null, bool skipUnMonitoredDatabase = true)
		{
			return this.RunChecks<DatabaseRedundancyValidator>((IADDatabase db) => new DatabaseRedundancyValidator(db, RegistryParameters.DatabaseHealthCheckAtLeastNCopies, this.CopyStatusLookup, this.ADConfig, null, true), dbGuid, skipUnMonitoredDatabase);
		}

		// Token: 0x060013BB RID: 5051 RVA: 0x000503CC File Offset: 0x0004E5CC
		public IEnumerable<IHealthValidationResultMinimal> RunDatabaseRedundancyChecksForCopyRemoval(bool ignoreActivationDisfavored, Guid? dbGuid = null, bool ignoreMaintenanceChecks = false, bool ignoreTooManyActivesCheck = false, int atLeastNCopies = -1)
		{
			if (atLeastNCopies == -1)
			{
				atLeastNCopies = RegistryParameters.DatabaseHealthCheckAtLeastNCopies;
			}
			return this.RunChecks<DatabaseRedundancyValidator>((IADDatabase db) => new DatabaseRedundancyValidator(db, atLeastNCopies, this.CopyStatusLookup, this.ADConfig, null, true, ignoreActivationDisfavored, ignoreMaintenanceChecks, ignoreTooManyActivesCheck, true), dbGuid, true);
		}

		// Token: 0x060013BC RID: 5052 RVA: 0x00050448 File Offset: 0x0004E648
		public IEnumerable<IHealthValidationResultMinimal> RunDatabaseAvailabilityChecks()
		{
			return this.RunChecks<DatabaseAvailabilityValidator>((IADDatabase db) => new DatabaseAvailabilityValidator(db, RegistryParameters.DatabaseHealthCheckAtLeastNCopies, this.CopyStatusLookup, this.ADConfig, null, true), null, true);
		}

		// Token: 0x060013BD RID: 5053 RVA: 0x000506CC File Offset: 0x0004E8CC
		private IEnumerable<IHealthValidationResultMinimal> RunChecks<TValidator>(Func<IADDatabase, TValidator> createValidatorFunc, Guid? dbGuid = null, bool skipUnMonitoredDatabase = true) where TValidator : DatabaseValidatorBase
		{
			IEnumerable<IADDatabase> databases = this.ADConfig.DatabasesIncludingMisconfiguredMap[this.ADConfig.TargetServerName];
			foreach (IADDatabase db in databases)
			{
				if ((!skipUnMonitoredDatabase || DatabaseHealthMonitor.ShouldMonitorDatabase(db)) && (dbGuid == null || db.Guid.Equals(dbGuid.Value)))
				{
					TValidator validator = createValidatorFunc(db);
					IHealthValidationResultMinimal result = validator.Run();
					yield return result;
				}
			}
			yield break;
		}

		// Token: 0x0400079E RID: 1950
		private readonly string m_serverName;

		// Token: 0x0400079F RID: 1951
		private int m_fInitialized;
	}
}
