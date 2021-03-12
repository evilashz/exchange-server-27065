using System;
using System.Collections.Generic;
using Microsoft.Exchange.Cluster.Shared;

namespace Microsoft.Exchange.Cluster.Replay.Monitoring
{
	// Token: 0x020001C9 RID: 457
	internal class DbHealthInfo
	{
		// Token: 0x170004E2 RID: 1250
		// (get) Token: 0x06001205 RID: 4613 RVA: 0x0004AEB6 File Offset: 0x000490B6
		// (set) Token: 0x06001206 RID: 4614 RVA: 0x0004AEBE File Offset: 0x000490BE
		public Guid DbGuid { get; private set; }

		// Token: 0x170004E3 RID: 1251
		// (get) Token: 0x06001207 RID: 4615 RVA: 0x0004AEC7 File Offset: 0x000490C7
		// (set) Token: 0x06001208 RID: 4616 RVA: 0x0004AECF File Offset: 0x000490CF
		public string DbName { get; set; }

		// Token: 0x170004E4 RID: 1252
		// (get) Token: 0x06001209 RID: 4617 RVA: 0x0004AED8 File Offset: 0x000490D8
		// (set) Token: 0x0600120A RID: 4618 RVA: 0x0004AEE0 File Offset: 0x000490E0
		public Dictionary<AmServerName, DbCopyHealthInfo> DbServerInfos { get; private set; }

		// Token: 0x170004E5 RID: 1253
		// (get) Token: 0x0600120B RID: 4619 RVA: 0x0004AEE9 File Offset: 0x000490E9
		// (set) Token: 0x0600120C RID: 4620 RVA: 0x0004AEF1 File Offset: 0x000490F1
		public StateTransitionInfo DbFoundInAD { get; private set; }

		// Token: 0x170004E6 RID: 1254
		// (get) Token: 0x0600120D RID: 4621 RVA: 0x0004AEFA File Offset: 0x000490FA
		// (set) Token: 0x0600120E RID: 4622 RVA: 0x0004AF02 File Offset: 0x00049102
		public StateTransitionInfo SkippedFromMonitoring { get; private set; }

		// Token: 0x170004E7 RID: 1255
		// (get) Token: 0x0600120F RID: 4623 RVA: 0x0004AF0B File Offset: 0x0004910B
		// (set) Token: 0x06001210 RID: 4624 RVA: 0x0004AF13 File Offset: 0x00049113
		public DbAvailabilityRedundancyInfo DbAvailabilityRedundancyInfo { get; private set; }

		// Token: 0x170004E8 RID: 1256
		// (get) Token: 0x06001211 RID: 4625 RVA: 0x0004AF1C File Offset: 0x0004911C
		public int RedundancyCount
		{
			get
			{
				return this.DbAvailabilityRedundancyInfo.RedundancyCount;
			}
		}

		// Token: 0x170004E9 RID: 1257
		// (get) Token: 0x06001212 RID: 4626 RVA: 0x0004AF29 File Offset: 0x00049129
		public int AvailabilityCount
		{
			get
			{
				return this.DbAvailabilityRedundancyInfo.AvailabilityCount;
			}
		}

		// Token: 0x06001213 RID: 4627 RVA: 0x0004AF38 File Offset: 0x00049138
		public DbHealthInfo(Guid dbGuid, string dbName)
		{
			this.DbGuid = dbGuid;
			this.DbName = dbName;
			this.DbFoundInAD = new StateTransitionInfo();
			this.SkippedFromMonitoring = new StateTransitionInfo();
			this.DbAvailabilityRedundancyInfo = new DbAvailabilityRedundancyInfo(this);
			this.DbServerInfos = new Dictionary<AmServerName, DbCopyHealthInfo>(5);
		}

		// Token: 0x06001214 RID: 4628 RVA: 0x0004AF87 File Offset: 0x00049187
		public void UpdateAvailabilityRedundancyStates()
		{
			this.DbAvailabilityRedundancyInfo.Update();
		}

		// Token: 0x06001215 RID: 4629 RVA: 0x0004AF94 File Offset: 0x00049194
		public bool ContainsDbCopy(AmServerName serverName)
		{
			return this.DbServerInfos.ContainsKey(serverName);
		}

		// Token: 0x06001216 RID: 4630 RVA: 0x0004AFA2 File Offset: 0x000491A2
		public DbCopyHealthInfo GetDbCopy(AmServerName serverName)
		{
			return this.DbServerInfos[serverName];
		}

		// Token: 0x06001217 RID: 4631 RVA: 0x0004AFB0 File Offset: 0x000491B0
		public DbCopyHealthInfo GetOrAddDbCopy(AmServerName serverName)
		{
			DbCopyHealthInfo dbCopyHealthInfo;
			if (!this.ContainsDbCopy(serverName))
			{
				dbCopyHealthInfo = new DbCopyHealthInfo(this.DbGuid, this.DbName, serverName);
				this.DbServerInfos[serverName] = dbCopyHealthInfo;
			}
			else
			{
				dbCopyHealthInfo = this.DbServerInfos[serverName];
			}
			return dbCopyHealthInfo;
		}

		// Token: 0x06001218 RID: 4632 RVA: 0x0004AFF8 File Offset: 0x000491F8
		public DbCopyHealthInfo AddNewDbCopy(AmServerName serverName)
		{
			DbCopyHealthInfo dbCopyHealthInfo = new DbCopyHealthInfo(this.DbGuid, this.DbName, serverName);
			this.DbServerInfos[serverName] = dbCopyHealthInfo;
			return dbCopyHealthInfo;
		}

		// Token: 0x06001219 RID: 4633 RVA: 0x0004B026 File Offset: 0x00049226
		public void RemoveDbCopy(AmServerName serverName)
		{
			this.DbServerInfos.Remove(serverName);
			this.UpdateAvailabilityRedundancyStates();
		}

		// Token: 0x0600121A RID: 4634 RVA: 0x0004B03C File Offset: 0x0004923C
		public DbHealthInfoPersisted ConvertToSerializable()
		{
			DbHealthInfoPersisted dbHealthInfoPersisted = new DbHealthInfoPersisted(this.DbGuid, this.DbName);
			dbHealthInfoPersisted.DbFoundInAD = this.DbFoundInAD.ConvertToSerializable();
			dbHealthInfoPersisted.SkippedFromMonitoring = this.SkippedFromMonitoring.ConvertToSerializable();
			foreach (DbCopyHealthInfo dbCopyHealthInfo in this.DbServerInfos.Values)
			{
				dbHealthInfoPersisted.DbCopies.Add(dbCopyHealthInfo.ConvertToSerializable());
			}
			DbAvailabilityRedundancyInfo dbAvailabilityRedundancyInfo = this.DbAvailabilityRedundancyInfo;
			dbHealthInfoPersisted.IsAtLeast1AvailableCopy = dbAvailabilityRedundancyInfo.AvailabilityInfo[1].ConvertToSerializable();
			dbHealthInfoPersisted.IsAtLeast2AvailableCopy = dbAvailabilityRedundancyInfo.AvailabilityInfo[2].ConvertToSerializable();
			dbHealthInfoPersisted.IsAtLeast3AvailableCopy = dbAvailabilityRedundancyInfo.AvailabilityInfo[3].ConvertToSerializable();
			dbHealthInfoPersisted.IsAtLeast4AvailableCopy = dbAvailabilityRedundancyInfo.AvailabilityInfo[4].ConvertToSerializable();
			dbHealthInfoPersisted.IsAtLeast1RedundantCopy = dbAvailabilityRedundancyInfo.RedundancyInfo[1].ConvertToSerializable();
			dbHealthInfoPersisted.IsAtLeast2RedundantCopy = dbAvailabilityRedundancyInfo.RedundancyInfo[2].ConvertToSerializable();
			dbHealthInfoPersisted.IsAtLeast3RedundantCopy = dbAvailabilityRedundancyInfo.RedundancyInfo[3].ConvertToSerializable();
			dbHealthInfoPersisted.IsAtLeast4RedundantCopy = dbAvailabilityRedundancyInfo.RedundancyInfo[4].ConvertToSerializable();
			return dbHealthInfoPersisted;
		}

		// Token: 0x0600121B RID: 4635 RVA: 0x0004B194 File Offset: 0x00049394
		public void InitializeFromSerializable(DbHealthInfoPersisted dbHip)
		{
			this.DbFoundInAD = StateTransitionInfo.ConstructFromPersisted(dbHip.DbFoundInAD);
			this.SkippedFromMonitoring = StateTransitionInfo.ConstructFromPersisted(dbHip.SkippedFromMonitoring);
			if (dbHip.DbCopies != null)
			{
				foreach (DbCopyHealthInfoPersisted dbCopyHealthInfoPersisted in dbHip.DbCopies)
				{
					AmServerName serverName = new AmServerName(dbCopyHealthInfoPersisted.ServerFqdn);
					DbCopyHealthInfo dbCopyHealthInfo = this.AddNewDbCopy(serverName);
					dbCopyHealthInfo.InitializeFromSerializable(dbCopyHealthInfoPersisted);
				}
			}
			DbAvailabilityRedundancyInfo dbAvailabilityRedundancyInfo = this.DbAvailabilityRedundancyInfo;
			dbAvailabilityRedundancyInfo.AvailabilityInfo[1] = StateTransitionInfo.ConstructFromPersisted(dbHip.IsAtLeast1AvailableCopy);
			dbAvailabilityRedundancyInfo.AvailabilityInfo[2] = StateTransitionInfo.ConstructFromPersisted(dbHip.IsAtLeast2AvailableCopy);
			dbAvailabilityRedundancyInfo.AvailabilityInfo[3] = StateTransitionInfo.ConstructFromPersisted(dbHip.IsAtLeast3AvailableCopy);
			dbAvailabilityRedundancyInfo.AvailabilityInfo[4] = StateTransitionInfo.ConstructFromPersisted(dbHip.IsAtLeast4AvailableCopy);
			dbAvailabilityRedundancyInfo.RedundancyInfo[1] = StateTransitionInfo.ConstructFromPersisted(dbHip.IsAtLeast1RedundantCopy);
			dbAvailabilityRedundancyInfo.RedundancyInfo[2] = StateTransitionInfo.ConstructFromPersisted(dbHip.IsAtLeast2RedundantCopy);
			dbAvailabilityRedundancyInfo.RedundancyInfo[3] = StateTransitionInfo.ConstructFromPersisted(dbHip.IsAtLeast3RedundantCopy);
			dbAvailabilityRedundancyInfo.RedundancyInfo[4] = StateTransitionInfo.ConstructFromPersisted(dbHip.IsAtLeast4RedundantCopy);
		}
	}
}
