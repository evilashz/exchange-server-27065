using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using Microsoft.Exchange.Cluster.Replay;
using Microsoft.Exchange.Cluster.Shared;
using Microsoft.Exchange.Data.HA.DirectoryServices;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Data.Storage.ActiveManager;
using Microsoft.Exchange.Diagnostics.Components.Cluster.Replay;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.Rpc.ActiveManager;
using Microsoft.Exchange.Rpc.Cluster;

namespace Microsoft.Exchange.Cluster.ActiveManagerServer
{
	// Token: 0x02000039 RID: 57
	internal abstract class AmDbOperation
	{
		// Token: 0x06000279 RID: 633 RVA: 0x0000F208 File Offset: 0x0000D408
		internal AmDbOperation(IADDatabase db)
		{
			this.Database = db;
			this.CreationTime = ExDateTime.Now;
			this.Counter = (long)Interlocked.Increment(ref AmDbOperation.sm_operationCounter);
			this.UniqueId = AmDbOperation.GenerateUniqueId(this.Database.Guid, this.CreationTime, this.Counter);
		}

		// Token: 0x1700008E RID: 142
		// (get) Token: 0x0600027A RID: 634 RVA: 0x0000F26B File Offset: 0x0000D46B
		// (set) Token: 0x0600027B RID: 635 RVA: 0x0000F273 File Offset: 0x0000D473
		internal IADDatabase Database { get; private set; }

		// Token: 0x1700008F RID: 143
		// (get) Token: 0x0600027C RID: 636 RVA: 0x0000F27C File Offset: 0x0000D47C
		// (set) Token: 0x0600027D RID: 637 RVA: 0x0000F284 File Offset: 0x0000D484
		internal AmReportCompletionDelegate CompletionCallback { get; set; }

		// Token: 0x17000090 RID: 144
		// (get) Token: 0x0600027E RID: 638 RVA: 0x0000F28D File Offset: 0x0000D48D
		// (set) Token: 0x0600027F RID: 639 RVA: 0x0000F295 File Offset: 0x0000D495
		internal AmDbActionStatus CustomStatus { get; set; }

		// Token: 0x17000091 RID: 145
		// (get) Token: 0x06000280 RID: 640 RVA: 0x0000F29E File Offset: 0x0000D49E
		// (set) Token: 0x06000281 RID: 641 RVA: 0x0000F2A6 File Offset: 0x0000D4A6
		internal Exception LastException { get; set; }

		// Token: 0x17000092 RID: 146
		// (get) Token: 0x06000282 RID: 642 RVA: 0x0000F2AF File Offset: 0x0000D4AF
		// (set) Token: 0x06000283 RID: 643 RVA: 0x0000F2B7 File Offset: 0x0000D4B7
		internal bool IsComplete { get; private set; }

		// Token: 0x17000093 RID: 147
		// (get) Token: 0x06000284 RID: 644 RVA: 0x0000F2C0 File Offset: 0x0000D4C0
		// (set) Token: 0x06000285 RID: 645 RVA: 0x0000F2C8 File Offset: 0x0000D4C8
		internal bool IsCancelled { get; set; }

		// Token: 0x17000094 RID: 148
		// (get) Token: 0x06000286 RID: 646 RVA: 0x0000F2D1 File Offset: 0x0000D4D1
		// (set) Token: 0x06000287 RID: 647 RVA: 0x0000F2D9 File Offset: 0x0000D4D9
		internal long Counter { get; private set; }

		// Token: 0x17000095 RID: 149
		// (get) Token: 0x06000288 RID: 648 RVA: 0x0000F2E2 File Offset: 0x0000D4E2
		// (set) Token: 0x06000289 RID: 649 RVA: 0x0000F2EA File Offset: 0x0000D4EA
		internal string UniqueId { get; private set; }

		// Token: 0x17000096 RID: 150
		// (get) Token: 0x0600028A RID: 650 RVA: 0x0000F2F3 File Offset: 0x0000D4F3
		// (set) Token: 0x0600028B RID: 651 RVA: 0x0000F2FB File Offset: 0x0000D4FB
		internal ExDateTime CreationTime { get; private set; }

		// Token: 0x17000097 RID: 151
		// (get) Token: 0x0600028C RID: 652 RVA: 0x0000F304 File Offset: 0x0000D504
		// (set) Token: 0x0600028D RID: 653 RVA: 0x0000F30C File Offset: 0x0000D50C
		internal AmDbOperationDetailedStatus DetailedStatus { get; set; }

		// Token: 0x0600028E RID: 654 RVA: 0x0000F318 File Offset: 0x0000D518
		internal static string GenerateUniqueId(Guid dbGuid, ExDateTime creationTime, long counter)
		{
			return string.Format("{0}#{1}#{2}#{3}", new object[]
			{
				creationTime.ToString("yyyy.MM.dd.hh.mm.ss.fff"),
				counter,
				AmServerName.LocalComputerName.NetbiosName,
				dbGuid
			});
		}

		// Token: 0x0600028F RID: 655 RVA: 0x0000F367 File Offset: 0x0000D567
		internal static bool IsCompletionStatus(AmDbActionStatus status)
		{
			return status == AmDbActionStatus.Completed || status == AmDbActionStatus.Failed || status == AmDbActionStatus.Cancelled;
		}

		// Token: 0x06000290 RID: 656 RVA: 0x0000F378 File Offset: 0x0000D578
		internal void Cancel()
		{
			this.IsCancelled = true;
			this.LastException = new AmDbActionCancelledException(this.Database.Name, base.GetType().Name);
			this.ReportStatus(this.Database, AmDbActionStatus.Cancelled);
		}

		// Token: 0x06000291 RID: 657 RVA: 0x0000F3B0 File Offset: 0x0000D5B0
		internal void Enqueue()
		{
			AmDatabaseQueueManager databaseQueueManager = AmSystemManager.Instance.DatabaseQueueManager;
			databaseQueueManager.Enqueue(this);
		}

		// Token: 0x06000292 RID: 658 RVA: 0x0000F3D0 File Offset: 0x0000D5D0
		internal AmDbCompletionReason Wait(TimeSpan timeout)
		{
			Stopwatch stopwatch = new Stopwatch();
			stopwatch.Start();
			AmDbCompletionReason amDbCompletionReason;
			while (!this.IsCancelled)
			{
				if (stopwatch.Elapsed > timeout)
				{
					amDbCompletionReason = AmDbCompletionReason.Timedout;
				}
				else
				{
					if (!this.IsComplete)
					{
						Thread.Sleep(50);
						continue;
					}
					amDbCompletionReason = AmDbCompletionReason.Finished;
				}
				IL_41:
				Exception ex = this.LastException;
				if (ex == null && amDbCompletionReason == AmDbCompletionReason.Timedout)
				{
					ex = new AmDbOperationTimedoutException(base.GetType().Name, this.Database.Name, timeout);
				}
				if (ex != null)
				{
					throw ex;
				}
				return amDbCompletionReason;
			}
			amDbCompletionReason = AmDbCompletionReason.Cancelled;
			goto IL_41;
		}

		// Token: 0x06000293 RID: 659 RVA: 0x0000F44F File Offset: 0x0000D64F
		internal AmDbCompletionReason Wait()
		{
			return this.Wait(TimeSpan.MaxValue);
		}

		// Token: 0x06000294 RID: 660 RVA: 0x0000F45C File Offset: 0x0000D65C
		internal bool IsStatusReached(AmDbActionStatus status)
		{
			lock (this.m_statusInfo)
			{
				ExDateTime exDateTime;
				if (this.m_statusInfo.TryGetValue(status, out exDateTime))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06000295 RID: 661 RVA: 0x0000F4B0 File Offset: 0x0000D6B0
		internal void ReportStatus(IADDatabase db, AmDbActionStatus status)
		{
			lock (this.m_statusInfo)
			{
				this.m_statusInfo[status] = ExDateTime.Now;
			}
			if (AmDbOperation.IsCompletionStatus(status) || status == this.CustomStatus)
			{
				this.IsComplete = true;
			}
			if (this.CompletionCallback != null && this.IsComplete && !this.m_isCompletionCalled)
			{
				this.m_isCompletionCalled = true;
				this.CompletionCallback(db);
			}
			if (status == AmDbActionStatus.UpdateMasterServerInitiated)
			{
				ExTraceGlobals.FaultInjectionTracer.TraceTest(2309369149U);
				return;
			}
			if (status == AmDbActionStatus.StoreMountInitiated)
			{
				ExTraceGlobals.FaultInjectionTracer.TraceTest(3383110973U);
			}
		}

		// Token: 0x06000296 RID: 662 RVA: 0x0000F568 File Offset: 0x0000D768
		internal void Run()
		{
			this.RunInternal();
		}

		// Token: 0x06000297 RID: 663 RVA: 0x0000F578 File Offset: 0x0000D778
		internal AmDatabaseMoveResult ConvertDetailedStatusToRpcMoveResult(AmDbOperationDetailedStatus detailedStatus)
		{
			Guid guid = detailedStatus.Database.Guid;
			string name = detailedStatus.Database.Name;
			string fromServerFqdn = string.Empty;
			string finalActiveServerFqdn = string.Empty;
			AmDbMountStatus dbMountStatusAtStart = AmDbMountStatus.Unknown;
			AmDbMountStatus dbMountStatusAtEnd = AmDbMountStatus.Unknown;
			if (detailedStatus.InitialDbState != null)
			{
				fromServerFqdn = detailedStatus.InitialDbState.ActiveServer.Fqdn;
				dbMountStatusAtStart = AmDbOperation.ConvertMountStatusToRpcMountStatus(detailedStatus.InitialDbState.MountStatus);
			}
			if (detailedStatus.FinalDbState != null)
			{
				finalActiveServerFqdn = detailedStatus.FinalDbState.ActiveServer.Fqdn;
				dbMountStatusAtEnd = AmDbOperation.ConvertMountStatusToRpcMountStatus(detailedStatus.FinalDbState.MountStatus);
			}
			Exception lastException = this.LastException;
			RpcErrorExceptionInfo errorInfo = AmRpcExceptionWrapper.Instance.ConvertExceptionToErrorExceptionInfo(lastException);
			AmDbMoveStatus dbMoveStatus = AmDbOperation.TranslateExceptionIntoMoveStatusEnum(lastException);
			List<AmDbRpcOperationSubStatus> attemptedServerSubStatuses = (from opSubStatus in detailedStatus.GetAllSubStatuses()
			select opSubStatus.ConvertToRpcSubStatus()).ToList<AmDbRpcOperationSubStatus>();
			return new AmDatabaseMoveResult(guid, name, fromServerFqdn, finalActiveServerFqdn, dbMoveStatus, dbMountStatusAtStart, dbMountStatusAtEnd, errorInfo, attemptedServerSubStatuses);
		}

		// Token: 0x06000298 RID: 664 RVA: 0x0000F66C File Offset: 0x0000D86C
		protected virtual void CheckIfOperationIsAllowedOnCurrentRole()
		{
			AmConfig config = AmSystemManager.Instance.Config;
			if (config.Role == AmRole.SAM)
			{
				AmReferralException ex = new AmReferralException(config.DagConfig.CurrentPAM.Fqdn);
				throw ex;
			}
			if (config.Role == AmRole.Unknown)
			{
				throw new AmInvalidConfiguration(config.LastError);
			}
		}

		// Token: 0x06000299 RID: 665
		protected abstract void RunInternal();

		// Token: 0x0600029A RID: 666 RVA: 0x0000F6BC File Offset: 0x0000D8BC
		protected AmDbAction PrepareDbAction(AmDbActionCode actionCode)
		{
			this.CheckIfOperationIsAllowedOnCurrentRole();
			AmConfig config = AmSystemManager.Instance.Config;
			AmDbAction amDbAction;
			if (config.IsPAM)
			{
				amDbAction = new AmDbPamAction(config, this.Database, actionCode, this.UniqueId);
			}
			else
			{
				amDbAction = new AmDbStandaloneAction(config, this.Database, actionCode, this.UniqueId);
			}
			AmDbAction amDbAction2 = amDbAction;
			amDbAction2.StatusCallback = (AmReportStatusDelegate)Delegate.Combine(amDbAction2.StatusCallback, new AmReportStatusDelegate(this.ReportStatus));
			return amDbAction;
		}

		// Token: 0x0600029B RID: 667 RVA: 0x0000F730 File Offset: 0x0000D930
		private static AmDbMoveStatus TranslateExceptionIntoMoveStatusEnum(Exception lastException)
		{
			AmDbMoveStatus result;
			if (lastException == null)
			{
				result = AmDbMoveStatus.Succeeded;
			}
			else
			{
				result = AmDbMoveStatus.Failed;
				if (lastException is AmDbMoveOperationNotSupportedException)
				{
					result = AmDbMoveStatus.Warning;
				}
				else if (lastException is AmDbMoveOperationSkippedException)
				{
					result = AmDbMoveStatus.Warning;
				}
			}
			return result;
		}

		// Token: 0x0600029C RID: 668 RVA: 0x0000F760 File Offset: 0x0000D960
		private static AmDbMountStatus ConvertMountStatusToRpcMountStatus(MountStatus mountStatus)
		{
			switch (mountStatus)
			{
			case MountStatus.Unknown:
				return AmDbMountStatus.Unknown;
			case MountStatus.Mounted:
				return AmDbMountStatus.Mounted;
			case MountStatus.Dismounted:
				return AmDbMountStatus.Dismounted;
			case MountStatus.Mounting:
				return AmDbMountStatus.Mounting;
			case MountStatus.Dismounting:
				return AmDbMountStatus.Dismounting;
			default:
				DiagCore.RetailAssert(false, "Unhandled case for mountStatus={0}", new object[]
				{
					mountStatus
				});
				return AmDbMountStatus.Unknown;
			}
		}

		// Token: 0x04000101 RID: 257
		private static int sm_operationCounter;

		// Token: 0x04000102 RID: 258
		private bool m_isCompletionCalled;

		// Token: 0x04000103 RID: 259
		private Dictionary<AmDbActionStatus, ExDateTime> m_statusInfo = new Dictionary<AmDbActionStatus, ExDateTime>();
	}
}
