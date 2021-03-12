using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.EdgeSync.Common;
using Microsoft.Exchange.EdgeSync.Datacenter;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.EdgeSync.Ehf
{
	// Token: 0x0200002E RID: 46
	internal sealed class EhfSyncErrorTracker
	{
		// Token: 0x17000064 RID: 100
		// (get) Token: 0x06000205 RID: 517 RVA: 0x0000E497 File Offset: 0x0000C697
		public int AllTransientFailuresCount
		{
			get
			{
				return this.transientFailureCount + this.criticalTransientFailureCount;
			}
		}

		// Token: 0x17000065 RID: 101
		// (get) Token: 0x06000206 RID: 518 RVA: 0x0000E4A6 File Offset: 0x0000C6A6
		public int CriticalTransientFailureCount
		{
			get
			{
				return this.criticalTransientFailureCount;
			}
		}

		// Token: 0x17000066 RID: 102
		// (get) Token: 0x06000207 RID: 519 RVA: 0x0000E4AE File Offset: 0x0000C6AE
		public int PermanentFailureCount
		{
			get
			{
				return this.permanentFailureCount;
			}
		}

		// Token: 0x17000067 RID: 103
		// (get) Token: 0x06000208 RID: 520 RVA: 0x0000E4B8 File Offset: 0x0000C6B8
		public bool HasTooManyFailures
		{
			get
			{
				int ehfAdminSyncMaxFailureCount = this.ehfSyncAppConfig.EhfAdminSyncMaxFailureCount;
				return this.criticalTransientFailureCount != 0 && (this.criticalTransientFailureCount > ehfAdminSyncMaxFailureCount || this.permanentFailureCount > ehfAdminSyncMaxFailureCount || this.transientFailuresInCurrentCycle.Count > ehfAdminSyncMaxFailureCount);
			}
		}

		// Token: 0x17000068 RID: 104
		// (get) Token: 0x06000209 RID: 521 RVA: 0x0000E502 File Offset: 0x0000C702
		public bool HasTransientFailure
		{
			get
			{
				return this.criticalTransientFailureCount > 0 || this.transientFailuresInCurrentCycle.Count > 0;
			}
		}

		// Token: 0x0600020A RID: 522 RVA: 0x0000E520 File Offset: 0x0000C720
		public void PrepareForNewSyncCycle(EnhancedTimeSpan syncInterval)
		{
			this.transientFailureDetails = new Dictionary<EhfCompanyIdentity, List<EhfTransientFailureInfo>>();
			this.criticalTransientFailureCount = 0;
			this.permanentFailureCount = 0;
			this.transientFailureCount = 0;
			ExDateTime utcNow = ExDateTime.UtcNow;
			Dictionary<Guid, int> dictionary = new Dictionary<Guid, int>();
			if (utcNow < this.lastSyncTime + 2L * syncInterval)
			{
				foreach (EhfCompanyIdentity ehfCompanyIdentity in this.transientFailuresInCurrentCycle)
				{
					int num;
					if (this.transientFailuresHistory.TryGetValue(ehfCompanyIdentity.EhfCompanyGuid, out num))
					{
						dictionary.Add(ehfCompanyIdentity.EhfCompanyGuid, num + 1);
					}
					else
					{
						dictionary.Add(ehfCompanyIdentity.EhfCompanyGuid, 1);
					}
				}
			}
			this.transientFailuresHistory = dictionary;
			this.transientFailuresInCurrentCycle = new HashSet<EhfCompanyIdentity>();
			this.lastSyncTime = utcNow;
		}

		// Token: 0x0600020B RID: 523 RVA: 0x0000E608 File Offset: 0x0000C808
		public void AddTransientFailure(EhfCompanyIdentity companyIdentity, Exception failureException, string operationName)
		{
			if (companyIdentity == null)
			{
				throw new ArgumentNullException("companyIdentity");
			}
			this.transientFailuresInCurrentCycle.Add(companyIdentity);
			EhfTransientFailureInfo item = new EhfTransientFailureInfo(failureException, operationName);
			List<EhfTransientFailureInfo> list;
			if (!this.transientFailureDetails.TryGetValue(companyIdentity, out list))
			{
				list = new List<EhfTransientFailureInfo>();
				this.transientFailureDetails[companyIdentity] = list;
			}
			list.Add(item);
			this.transientFailureCount++;
		}

		// Token: 0x0600020C RID: 524 RVA: 0x0000E670 File Offset: 0x0000C870
		public void AddPermanentFailure()
		{
			this.permanentFailureCount++;
		}

		// Token: 0x0600020D RID: 525 RVA: 0x0000E680 File Offset: 0x0000C880
		public void AddCriticalFailure()
		{
			this.criticalTransientFailureCount++;
		}

		// Token: 0x0600020E RID: 526 RVA: 0x0000E690 File Offset: 0x0000C890
		public void AbortSyncCycleIfRequired(EhfAdminAccountSynchronizer ehfAdminAccountSynchronizer, EdgeSyncDiag diagSession)
		{
			if (this.criticalTransientFailureCount != 0)
			{
				ehfAdminAccountSynchronizer.LogAndAbortSyncCycle();
			}
			foreach (EhfCompanyIdentity ehfCompanyIdentity in this.transientFailuresInCurrentCycle)
			{
				int num;
				if (!this.transientFailuresHistory.TryGetValue(ehfCompanyIdentity.EhfCompanyGuid, out num))
				{
					num = 0;
				}
				int num2 = num + 1;
				if (num2 < this.ehfSyncAppConfig.EhfAdminSyncTransientFailureRetryThreshold)
				{
					ehfAdminAccountSynchronizer.LogAndAbortSyncCycle();
				}
			}
			foreach (KeyValuePair<EhfCompanyIdentity, List<EhfTransientFailureInfo>> keyValuePair in this.transientFailureDetails)
			{
				StringBuilder stringBuilder = new StringBuilder();
				foreach (EhfTransientFailureInfo ehfTransientFailureInfo in keyValuePair.Value)
				{
					stringBuilder.AppendLine(ehfTransientFailureInfo.OperationName);
					stringBuilder.AppendLine(ehfTransientFailureInfo.FailureException.ToString());
					stringBuilder.AppendLine();
				}
				diagSession.EventLog.LogEvent(EdgeSyncEventLogConstants.Tuple_EhfAdminSyncTransientFailureRetryThresholdReached, null, new object[]
				{
					keyValuePair.Key.EhfCompanyId,
					keyValuePair.Key.EhfCompanyGuid,
					stringBuilder.ToString()
				});
			}
		}

		// Token: 0x0600020F RID: 527 RVA: 0x0000E820 File Offset: 0x0000CA20
		public void Initialize(EhfSyncAppConfig ehfSyncAppConfig)
		{
			this.ehfSyncAppConfig = ehfSyncAppConfig;
		}

		// Token: 0x040000C6 RID: 198
		private int criticalTransientFailureCount;

		// Token: 0x040000C7 RID: 199
		private int permanentFailureCount;

		// Token: 0x040000C8 RID: 200
		private int transientFailureCount;

		// Token: 0x040000C9 RID: 201
		private ExDateTime lastSyncTime = ExDateTime.MinValue;

		// Token: 0x040000CA RID: 202
		private EhfSyncAppConfig ehfSyncAppConfig;

		// Token: 0x040000CB RID: 203
		private Dictionary<Guid, int> transientFailuresHistory = new Dictionary<Guid, int>();

		// Token: 0x040000CC RID: 204
		private Dictionary<EhfCompanyIdentity, List<EhfTransientFailureInfo>> transientFailureDetails = new Dictionary<EhfCompanyIdentity, List<EhfTransientFailureInfo>>();

		// Token: 0x040000CD RID: 205
		private HashSet<EhfCompanyIdentity> transientFailuresInCurrentCycle = new HashSet<EhfCompanyIdentity>();
	}
}
