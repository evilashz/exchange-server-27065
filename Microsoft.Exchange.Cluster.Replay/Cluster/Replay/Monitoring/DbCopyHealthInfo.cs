using System;
using Microsoft.Exchange.Cluster.Shared;

namespace Microsoft.Exchange.Cluster.Replay.Monitoring
{
	// Token: 0x020001CA RID: 458
	internal class DbCopyHealthInfo
	{
		// Token: 0x170004EA RID: 1258
		// (get) Token: 0x0600121C RID: 4636 RVA: 0x0004B2E8 File Offset: 0x000494E8
		// (set) Token: 0x0600121D RID: 4637 RVA: 0x0004B2F0 File Offset: 0x000494F0
		public string DbIdentity { get; private set; }

		// Token: 0x170004EB RID: 1259
		// (get) Token: 0x0600121E RID: 4638 RVA: 0x0004B2F9 File Offset: 0x000494F9
		// (set) Token: 0x0600121F RID: 4639 RVA: 0x0004B301 File Offset: 0x00049501
		public Guid DbGuid { get; private set; }

		// Token: 0x170004EC RID: 1260
		// (get) Token: 0x06001220 RID: 4640 RVA: 0x0004B30A File Offset: 0x0004950A
		// (set) Token: 0x06001221 RID: 4641 RVA: 0x0004B312 File Offset: 0x00049512
		public string DbName { get; private set; }

		// Token: 0x170004ED RID: 1261
		// (get) Token: 0x06001222 RID: 4642 RVA: 0x0004B31B File Offset: 0x0004951B
		// (set) Token: 0x06001223 RID: 4643 RVA: 0x0004B323 File Offset: 0x00049523
		public AmServerName ServerName { get; private set; }

		// Token: 0x170004EE RID: 1262
		// (get) Token: 0x06001224 RID: 4644 RVA: 0x0004B32C File Offset: 0x0004952C
		// (set) Token: 0x06001225 RID: 4645 RVA: 0x0004B334 File Offset: 0x00049534
		public DateTime LastTouchedTime { get; set; }

		// Token: 0x170004EF RID: 1263
		// (get) Token: 0x06001226 RID: 4646 RVA: 0x0004B33D File Offset: 0x0004953D
		// (set) Token: 0x06001227 RID: 4647 RVA: 0x0004B345 File Offset: 0x00049545
		public DateTime LastCopyStatusTransitionTime { get; set; }

		// Token: 0x170004F0 RID: 1264
		// (get) Token: 0x06001228 RID: 4648 RVA: 0x0004B34E File Offset: 0x0004954E
		// (set) Token: 0x06001229 RID: 4649 RVA: 0x0004B356 File Offset: 0x00049556
		public StateTransitionInfo CopyStatusRetrieved { get; private set; }

		// Token: 0x170004F1 RID: 1265
		// (get) Token: 0x0600122A RID: 4650 RVA: 0x0004B35F File Offset: 0x0004955F
		// (set) Token: 0x0600122B RID: 4651 RVA: 0x0004B367 File Offset: 0x00049567
		public StateTransitionInfo CopyIsAvailable { get; private set; }

		// Token: 0x170004F2 RID: 1266
		// (get) Token: 0x0600122C RID: 4652 RVA: 0x0004B370 File Offset: 0x00049570
		// (set) Token: 0x0600122D RID: 4653 RVA: 0x0004B378 File Offset: 0x00049578
		public StateTransitionInfo CopyIsRedundant { get; private set; }

		// Token: 0x170004F3 RID: 1267
		// (get) Token: 0x0600122E RID: 4654 RVA: 0x0004B381 File Offset: 0x00049581
		// (set) Token: 0x0600122F RID: 4655 RVA: 0x0004B389 File Offset: 0x00049589
		public StateTransitionInfo CopyStatusHealthy { get; private set; }

		// Token: 0x170004F4 RID: 1268
		// (get) Token: 0x06001230 RID: 4656 RVA: 0x0004B392 File Offset: 0x00049592
		// (set) Token: 0x06001231 RID: 4657 RVA: 0x0004B39A File Offset: 0x0004959A
		public StateTransitionInfo CopyStatusActive { get; private set; }

		// Token: 0x170004F5 RID: 1269
		// (get) Token: 0x06001232 RID: 4658 RVA: 0x0004B3A3 File Offset: 0x000495A3
		// (set) Token: 0x06001233 RID: 4659 RVA: 0x0004B3AB File Offset: 0x000495AB
		public StateTransitionInfo CopyStatusMounted { get; private set; }

		// Token: 0x170004F6 RID: 1270
		// (get) Token: 0x06001234 RID: 4660 RVA: 0x0004B3B4 File Offset: 0x000495B4
		// (set) Token: 0x06001235 RID: 4661 RVA: 0x0004B3BC File Offset: 0x000495BC
		public StateTransitionInfo CopyFoundInAD { get; private set; }

		// Token: 0x06001236 RID: 4662 RVA: 0x0004B3C8 File Offset: 0x000495C8
		public DbCopyHealthInfo(Guid dbGuid, string dbName, AmServerName serverName)
		{
			this.DbGuid = dbGuid;
			this.DbName = dbName;
			this.DbIdentity = dbGuid.ToString();
			this.ServerName = serverName;
			this.CopyIsAvailable = new StateTransitionInfo();
			this.CopyIsRedundant = new StateTransitionInfo();
			this.CopyStatusRetrieved = new StateTransitionInfo();
			this.CopyStatusHealthy = new StateTransitionInfo();
			this.CopyStatusActive = new StateTransitionInfo();
			this.CopyStatusMounted = new StateTransitionInfo();
			this.CopyFoundInAD = new StateTransitionInfo();
		}

		// Token: 0x06001237 RID: 4663 RVA: 0x0004B450 File Offset: 0x00049650
		public bool IsAvailable()
		{
			return this.CopyFoundInAD.IsSuccess && this.CopyStatusRetrieved.IsSuccess && this.CopyIsAvailable.IsSuccess;
		}

		// Token: 0x06001238 RID: 4664 RVA: 0x0004B479 File Offset: 0x00049679
		public bool IsRedundant()
		{
			return this.CopyFoundInAD.IsSuccess && this.CopyStatusRetrieved.IsSuccess && this.CopyIsRedundant.IsSuccess;
		}

		// Token: 0x06001239 RID: 4665 RVA: 0x0004B4A4 File Offset: 0x000496A4
		public DbCopyHealthInfoPersisted ConvertToSerializable()
		{
			return new DbCopyHealthInfoPersisted(this.DbGuid, this.ServerName.Fqdn)
			{
				LastTouchedTime = this.LastTouchedTime,
				CopyStatusRetrieved = this.CopyStatusRetrieved.ConvertToSerializable(),
				CopyIsAvailable = this.CopyIsAvailable.ConvertToSerializable(),
				CopyIsRedundant = this.CopyIsRedundant.ConvertToSerializable(),
				CopyStatusHealthy = this.CopyStatusHealthy.ConvertToSerializable(),
				LastCopyStatusTransitionTime = this.LastCopyStatusTransitionTime,
				CopyStatusActive = this.CopyStatusActive.ConvertToSerializable(),
				CopyStatusMounted = this.CopyStatusMounted.ConvertToSerializable(),
				CopyFoundInAD = this.CopyFoundInAD.ConvertToSerializable()
			};
		}

		// Token: 0x0600123A RID: 4666 RVA: 0x0004B558 File Offset: 0x00049758
		public void InitializeFromSerializable(DbCopyHealthInfoPersisted dbcHip)
		{
			this.LastTouchedTime = dbcHip.LastTouchedTime;
			this.CopyStatusRetrieved = StateTransitionInfo.ConstructFromPersisted(dbcHip.CopyStatusRetrieved);
			this.CopyIsAvailable = StateTransitionInfo.ConstructFromPersisted(dbcHip.CopyIsAvailable);
			this.CopyIsRedundant = StateTransitionInfo.ConstructFromPersisted(dbcHip.CopyIsRedundant);
			this.CopyStatusHealthy = StateTransitionInfo.ConstructFromPersisted(dbcHip.CopyStatusHealthy);
			this.LastCopyStatusTransitionTime = dbcHip.LastCopyStatusTransitionTime;
			this.CopyStatusActive = StateTransitionInfo.ConstructFromPersisted(dbcHip.CopyStatusActive);
			this.CopyStatusMounted = StateTransitionInfo.ConstructFromPersisted(dbcHip.CopyStatusMounted);
			this.CopyFoundInAD = StateTransitionInfo.ConstructFromPersisted(dbcHip.CopyFoundInAD);
		}
	}
}
