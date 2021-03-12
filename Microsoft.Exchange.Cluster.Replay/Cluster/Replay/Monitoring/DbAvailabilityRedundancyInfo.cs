using System;

namespace Microsoft.Exchange.Cluster.Replay.Monitoring
{
	// Token: 0x020001C7 RID: 455
	internal class DbAvailabilityRedundancyInfo
	{
		// Token: 0x170004DC RID: 1244
		// (get) Token: 0x060011EF RID: 4591 RVA: 0x0004ABC0 File Offset: 0x00048DC0
		// (set) Token: 0x060011F0 RID: 4592 RVA: 0x0004ABC8 File Offset: 0x00048DC8
		public DbAvailabilityRedundancyInfo.NCopyStateTransitionInfo RedundancyInfo { get; private set; }

		// Token: 0x170004DD RID: 1245
		// (get) Token: 0x060011F1 RID: 4593 RVA: 0x0004ABD1 File Offset: 0x00048DD1
		// (set) Token: 0x060011F2 RID: 4594 RVA: 0x0004ABD9 File Offset: 0x00048DD9
		public DbAvailabilityRedundancyInfo.NCopyStateTransitionInfo AvailabilityInfo { get; private set; }

		// Token: 0x170004DE RID: 1246
		// (get) Token: 0x060011F3 RID: 4595 RVA: 0x0004ABE2 File Offset: 0x00048DE2
		// (set) Token: 0x060011F4 RID: 4596 RVA: 0x0004ABEA File Offset: 0x00048DEA
		public int RedundancyCount { get; private set; }

		// Token: 0x170004DF RID: 1247
		// (get) Token: 0x060011F5 RID: 4597 RVA: 0x0004ABF3 File Offset: 0x00048DF3
		// (set) Token: 0x060011F6 RID: 4598 RVA: 0x0004ABFB File Offset: 0x00048DFB
		public int AvailabilityCount { get; private set; }

		// Token: 0x170004E0 RID: 1248
		// (get) Token: 0x060011F7 RID: 4599 RVA: 0x0004AC04 File Offset: 0x00048E04
		// (set) Token: 0x060011F8 RID: 4600 RVA: 0x0004AC0C File Offset: 0x00048E0C
		private DbHealthInfo DbInfo { get; set; }

		// Token: 0x060011F9 RID: 4601 RVA: 0x0004AC15 File Offset: 0x00048E15
		public DbAvailabilityRedundancyInfo(DbHealthInfo dbInfo)
		{
			this.DbInfo = dbInfo;
			this.RedundancyInfo = new DbAvailabilityRedundancyInfo.NCopyStateTransitionInfo();
			this.AvailabilityInfo = new DbAvailabilityRedundancyInfo.NCopyStateTransitionInfo();
		}

		// Token: 0x060011FA RID: 4602 RVA: 0x0004AC4C File Offset: 0x00048E4C
		public void Update()
		{
			DbHealthInfo dbInfo = this.DbInfo;
			if (!dbInfo.DbFoundInAD.IsSuccess || dbInfo.DbServerInfos.Count == 0)
			{
				this.AvailabilityInfo.UpdateStates(0);
				this.RedundancyInfo.UpdateStates(0);
				return;
			}
			this.AvailabilityCount = this.GetHealthyCopyCount((DbCopyHealthInfo copyInfo) => copyInfo.CopyIsAvailable);
			this.RedundancyCount = this.GetHealthyCopyCount((DbCopyHealthInfo copyInfo) => copyInfo.CopyIsRedundant);
			this.AvailabilityInfo.UpdateStates(this.AvailabilityCount);
			this.RedundancyInfo.UpdateStates(this.RedundancyCount);
		}

		// Token: 0x060011FB RID: 4603 RVA: 0x0004AD08 File Offset: 0x00048F08
		public int GetHealthyCopyCount(Func<DbCopyHealthInfo, StateTransitionInfo> healthCheckStateGetter)
		{
			int num = 0;
			foreach (DbCopyHealthInfo copyInfo in this.DbInfo.DbServerInfos.Values)
			{
				if (this.IsCopyHealthy(copyInfo, healthCheckStateGetter))
				{
					num++;
				}
			}
			return num;
		}

		// Token: 0x060011FC RID: 4604 RVA: 0x0004AD70 File Offset: 0x00048F70
		public bool IsCopyHealthy(DbCopyHealthInfo copyInfo, Func<DbCopyHealthInfo, StateTransitionInfo> healthCheckStateGetter)
		{
			if (!copyInfo.CopyFoundInAD.IsSuccess)
			{
				return false;
			}
			if (!copyInfo.CopyStatusRetrieved.IsSuccess)
			{
				return false;
			}
			StateTransitionInfo stateTransitionInfo = healthCheckStateGetter(copyInfo);
			return stateTransitionInfo.IsSuccess;
		}

		// Token: 0x020001C8 RID: 456
		public class NCopyStateTransitionInfo
		{
			// Token: 0x060011FF RID: 4607 RVA: 0x0004ADAC File Offset: 0x00048FAC
			public NCopyStateTransitionInfo()
			{
				for (int i = 0; i < this.m_infos.Length; i++)
				{
					this.m_infos[i] = new StateTransitionInfo();
				}
			}

			// Token: 0x170004E1 RID: 1249
			public StateTransitionInfo this[int i]
			{
				get
				{
					int num = this.SanitizeIndex(i);
					return this.m_infos[num - 1];
				}
				set
				{
					int num = this.SanitizeIndex(i);
					this.m_infos[num - 1] = value;
				}
			}

			// Token: 0x06001202 RID: 4610 RVA: 0x0004AE2C File Offset: 0x0004902C
			public void UpdateStates(int healthyCopyCount)
			{
				healthyCopyCount = Math.Min(healthyCopyCount, 4);
				for (int i = 1; i <= 4; i++)
				{
					StateTransitionInfo stateTransitionInfo = this[i];
					if (i <= healthyCopyCount)
					{
						stateTransitionInfo.ReportSuccess();
					}
					else
					{
						stateTransitionInfo.ReportFailure();
					}
				}
			}

			// Token: 0x06001203 RID: 4611 RVA: 0x0004AE68 File Offset: 0x00049068
			public void ForEach(Action<StateTransitionInfo> doSomething)
			{
				for (int i = 0; i < this.m_infos.Length; i++)
				{
					StateTransitionInfo obj = this.m_infos[i];
					doSomething(obj);
				}
			}

			// Token: 0x06001204 RID: 4612 RVA: 0x0004AE98 File Offset: 0x00049098
			private int SanitizeIndex(int i)
			{
				int val = Math.Min(i, 4);
				return Math.Max(val, 1);
			}

			// Token: 0x040006E4 RID: 1764
			public const int MinNumCopies = 1;

			// Token: 0x040006E5 RID: 1765
			public const int MaxNumCopies = 4;

			// Token: 0x040006E6 RID: 1766
			private StateTransitionInfo[] m_infos = new StateTransitionInfo[4];
		}
	}
}
