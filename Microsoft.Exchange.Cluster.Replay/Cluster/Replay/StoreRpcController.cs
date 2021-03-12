using System;
using Microsoft.Exchange.Cluster.Shared;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.HA;
using Microsoft.Isam.Esent.Interop;
using Microsoft.Mapi;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x02000321 RID: 801
	internal class StoreRpcController : SafeRefCountedTimeoutWrapper, IStoreRpc, IListMDBStatus, IStoreMountDismount, IDisposable
	{
		// Token: 0x1700089D RID: 2205
		// (get) Token: 0x060020DB RID: 8411 RVA: 0x00097300 File Offset: 0x00095500
		private ExRpcAdmin ExRpcAdmin
		{
			get
			{
				ExRpcAdmin exRpcAdmin;
				lock (this.m_lockObj)
				{
					if (this.m_exRpcAdmin == null)
					{
						this.CreateExRpcAdminWithTimeout(this.ConnectivityTimeout);
					}
					exRpcAdmin = this.m_exRpcAdmin;
				}
				return exRpcAdmin;
			}
		}

		// Token: 0x1700089E RID: 2206
		// (get) Token: 0x060020DC RID: 8412 RVA: 0x00097358 File Offset: 0x00095558
		public virtual TimeSpan ConnectivityTimeout
		{
			get
			{
				return TimeSpan.FromSeconds((double)RegistryParameters.StoreRpcConnectivityTimeoutInSec);
			}
		}

		// Token: 0x1700089F RID: 2207
		// (get) Token: 0x060020DD RID: 8413 RVA: 0x00097365 File Offset: 0x00095565
		public TimeSpan RpcTimeout
		{
			get
			{
				if (!this.ShouldUseTimeout)
				{
					return InvokeWithTimeout.InfiniteTimeSpan;
				}
				return TimeSpan.FromSeconds((double)RegistryParameters.StoreRpcGenericTimeoutInSec);
			}
		}

		// Token: 0x170008A0 RID: 2208
		// (get) Token: 0x060020DE RID: 8414 RVA: 0x00097380 File Offset: 0x00095580
		public TimeSpan ListMdbStatusRpcTimeout
		{
			get
			{
				if (!this.ShouldUseTimeout)
				{
					return InvokeWithTimeout.InfiniteTimeSpan;
				}
				return TimeSpan.FromSeconds((double)RegistryParameters.ListMdbStatusRpcTimeoutInSec);
			}
		}

		// Token: 0x170008A1 RID: 2209
		// (get) Token: 0x060020DF RID: 8415 RVA: 0x0009739B File Offset: 0x0009559B
		public string ServerNameOrFqdn
		{
			get
			{
				return this.m_serverNameOrFqdn;
			}
		}

		// Token: 0x170008A2 RID: 2210
		// (get) Token: 0x060020E0 RID: 8416 RVA: 0x000973A3 File Offset: 0x000955A3
		protected bool ShouldUseTimeout
		{
			get
			{
				return this.ConnectivityTimeout != InvokeWithTimeout.InfiniteTimeSpan;
			}
		}

		// Token: 0x060020E1 RID: 8417 RVA: 0x000973B8 File Offset: 0x000955B8
		public StoreRpcController(string serverNameOrFqdn, string clientTypeId) : base("StoreRpcController")
		{
			if (string.IsNullOrEmpty(serverNameOrFqdn))
			{
				this.m_serverNameOrFqdn = null;
			}
			else
			{
				this.m_serverNameOrFqdn = serverNameOrFqdn;
			}
			if (string.IsNullOrEmpty(clientTypeId))
			{
				this.m_clientTypeId = "Client=HA";
				return;
			}
			this.m_clientTypeId = clientTypeId;
		}

		// Token: 0x060020E2 RID: 8418 RVA: 0x00097434 File Offset: 0x00095634
		public bool TestStoreConnectivity(TimeSpan timeout, out LocalizedException ex)
		{
			ex = null;
			bool result;
			try
			{
				int verMajor;
				int verMinor;
				base.ProtectedCallWithTimeout("GetAdminVersion", timeout, delegate
				{
					this.ExRpcAdmin.GetAdminVersion(out verMajor, out verMinor);
				});
				result = true;
			}
			catch (MapiRetryableException ex2)
			{
				ex = ex2;
				result = false;
			}
			catch (MapiPermanentException ex3)
			{
				ex = ex3;
				result = false;
			}
			return result;
		}

		// Token: 0x060020E3 RID: 8419 RVA: 0x000974BC File Offset: 0x000956BC
		public void ForceNewLog(Guid guidMdb, long numLogsToRoll = 1L)
		{
			for (long num = 0L; num < numLogsToRoll; num += 1L)
			{
				base.ProtectedCallWithTimeout("ForceNewLog", this.RpcTimeout, delegate
				{
					this.ExRpcAdmin.ForceNewLog(guidMdb);
				});
			}
		}

		// Token: 0x060020E4 RID: 8420 RVA: 0x0009753C File Offset: 0x0009573C
		public void MountDatabase(Guid guidStorageGroup, Guid guidMdb, int flags)
		{
			base.ProtectedCall("MountDatabase", delegate
			{
				this.ExRpcAdmin.MountDatabase(guidStorageGroup, guidMdb, flags);
			});
		}

		// Token: 0x060020E5 RID: 8421 RVA: 0x000975B0 File Offset: 0x000957B0
		public void UnmountDatabase(Guid guidStorageGroup, Guid guidMdb, int flags)
		{
			UnmountFlags flags2 = (UnmountFlags)flags;
			TimeSpan timeout = InvokeWithTimeout.InfiniteTimeSpan;
			if (this.ShouldUseTimeout && (flags2 & UnmountFlags.SkipCacheFlush) != UnmountFlags.None)
			{
				timeout = TimeSpan.FromSeconds((double)RegistryParameters.AmDismountOrKillTimeoutInSec);
			}
			base.ProtectedCallWithTimeout("UnmountDatabase", timeout, delegate
			{
				this.ExRpcAdmin.UnmountDatabase(guidStorageGroup, guidMdb, flags);
			});
		}

		// Token: 0x060020E6 RID: 8422 RVA: 0x0009767C File Offset: 0x0009587C
		public void LogReplayRequest(Guid guidMdb, uint ulgenLogReplayMax, uint ulLogReplayFlags, out uint ulgenLogReplayNext, out JET_DBINFOMISC dbinfo, out IPagePatchReply pagePatchReply, out uint[] corruptedPages)
		{
			pagePatchReply = null;
			corruptedPages = null;
			uint tmplgenLogReplayNext = 0U;
			JET_DBINFOMISC tmpdbinfo = new JET_DBINFOMISC();
			uint patchReplyPageNumber = 0U;
			byte[] patchReplyToken = null;
			byte[] patchReplyData = null;
			uint[] pagesToBePatched = null;
			base.ProtectedCallWithTimeout("LogReplayRequest2", this.RpcTimeout, delegate
			{
				this.ExRpcAdmin.LogReplayRequest(guidMdb, ulgenLogReplayMax, ulLogReplayFlags, out tmplgenLogReplayNext, out tmpdbinfo, out patchReplyPageNumber, out patchReplyToken, out patchReplyData, out pagesToBePatched);
			});
			ulgenLogReplayNext = tmplgenLogReplayNext;
			dbinfo = tmpdbinfo;
			if (patchReplyPageNumber != 0U)
			{
				pagePatchReply = new PagePatchReply
				{
					PageNumber = checked((int)patchReplyPageNumber),
					Data = patchReplyData,
					Token = patchReplyToken
				};
			}
			if (pagesToBePatched != null && pagesToBePatched.Length > 0)
			{
				corruptedPages = pagesToBePatched;
			}
		}

		// Token: 0x060020E7 RID: 8423 RVA: 0x00097790 File Offset: 0x00095990
		public void StartBlockModeReplicationToPassive(Guid guidMdb, string passiveName, uint ulFirstGenToSend)
		{
			base.ProtectedCallWithTimeout("StartBlockModeReplicationToPassive", this.RpcTimeout, delegate
			{
				this.ExRpcAdmin.StartBlockModeReplicationToPassive(guidMdb, passiveName, ulFirstGenToSend);
			});
		}

		// Token: 0x060020E8 RID: 8424 RVA: 0x000977DC File Offset: 0x000959DC
		public MdbStatus[] ListMdbStatus(Guid[] dbGuids)
		{
			return this.ListMdbStatus(dbGuids, null);
		}

		// Token: 0x060020E9 RID: 8425 RVA: 0x00097820 File Offset: 0x00095A20
		public MdbStatus[] ListMdbStatus(Guid[] dbGuids, TimeSpan? timeout)
		{
			MdbStatus[] status = null;
			base.ProtectedCallWithTimeout("ListMdbStatus", (timeout != null) ? timeout.Value : this.ListMdbStatusRpcTimeout, delegate
			{
				status = this.ExRpcAdmin.ListMdbStatus(dbGuids);
			});
			return status;
		}

		// Token: 0x060020EA RID: 8426 RVA: 0x00097880 File Offset: 0x00095A80
		public MdbStatus[] ListMdbStatus(bool isBasicInformation)
		{
			return this.ListMdbStatus(isBasicInformation, null);
		}

		// Token: 0x060020EB RID: 8427 RVA: 0x000978C4 File Offset: 0x00095AC4
		public MdbStatus[] ListMdbStatus(bool isBasicInformation, TimeSpan? timeout)
		{
			MdbStatus[] status = null;
			base.ProtectedCallWithTimeout("ListMdbStatus", (timeout != null) ? timeout.Value : this.ListMdbStatusRpcTimeout, delegate
			{
				status = this.ExRpcAdmin.ListMdbStatus(isBasicInformation);
			});
			return status;
		}

		// Token: 0x060020EC RID: 8428 RVA: 0x00097948 File Offset: 0x00095B48
		public void SnapshotPrepare(Guid dbGuid, uint flags)
		{
			base.ProtectedCall("SnapshotPrepare", delegate
			{
				this.ExRpcAdmin.SnapshotPrepare(dbGuid, flags);
			});
		}

		// Token: 0x060020ED RID: 8429 RVA: 0x000979B0 File Offset: 0x00095BB0
		public void SnapshotFreeze(Guid dbGuid, uint flags)
		{
			base.ProtectedCall("SnapshotFreeze", delegate
			{
				this.ExRpcAdmin.SnapshotFreeze(dbGuid, flags);
			});
		}

		// Token: 0x060020EE RID: 8430 RVA: 0x00097A18 File Offset: 0x00095C18
		public void SnapshotThaw(Guid dbGuid, uint flags)
		{
			base.ProtectedCall("SnapshotThaw", delegate
			{
				this.ExRpcAdmin.SnapshotThaw(dbGuid, flags);
			});
		}

		// Token: 0x060020EF RID: 8431 RVA: 0x00097A80 File Offset: 0x00095C80
		public void SnapshotTruncateLogInstance(Guid dbGuid, uint flags)
		{
			base.ProtectedCall("SnapshotTruncateLogInstance", delegate
			{
				this.ExRpcAdmin.SnapshotTruncateLogInstance(dbGuid, flags);
			});
		}

		// Token: 0x060020F0 RID: 8432 RVA: 0x00097AE8 File Offset: 0x00095CE8
		public void SnapshotStop(Guid dbGuid, uint flags)
		{
			base.ProtectedCall("SnapshotStop", delegate
			{
				this.ExRpcAdmin.SnapshotStop(dbGuid, flags);
			});
		}

		// Token: 0x060020F1 RID: 8433 RVA: 0x00097B50 File Offset: 0x00095D50
		public void GetDatabaseInformation(Guid guidMdb, out JET_DBINFOMISC databaseInformation)
		{
			JET_DBINFOMISC tmpDbInfo = new JET_DBINFOMISC();
			base.ProtectedCallWithTimeout("GetPhysicalDatabaseInformation", this.RpcTimeout, delegate
			{
				this.ExRpcAdmin.GetPhysicalDatabaseInformation(guidMdb, out tmpDbInfo);
			});
			databaseInformation = tmpDbInfo;
		}

		// Token: 0x060020F2 RID: 8434 RVA: 0x00097C00 File Offset: 0x00095E00
		public void GetDatabaseProcessInfo(Guid guidMdb, out int workerProcessId, out int minVersion, out int maxVersion, out int requestedVersion)
		{
			PropValue[][] retVals = null;
			base.ProtectedCallWithTimeout("GetDatabaseProcessInfo", this.RpcTimeout, delegate
			{
				retVals = this.ExRpcAdmin.GetDatabaseProcessInfo(guidMdb, new PropTag[]
				{
					PropTag.WorkerProcessId,
					PropTag.MinimumDatabaseSchemaVersion,
					PropTag.OverallAgeLimit,
					PropTag.RequestedDatabaseSchemaVersion
				});
			});
			workerProcessId = 0;
			minVersion = 0;
			maxVersion = 0;
			requestedVersion = 0;
			if (retVals.Length > 0)
			{
				foreach (PropValue propValue in retVals[0])
				{
					if (propValue.PropTag == PropTag.WorkerProcessId && propValue.Value is int)
					{
						workerProcessId = (int)propValue.Value;
					}
					if (propValue.PropTag == PropTag.MinimumDatabaseSchemaVersion && propValue.Value is int)
					{
						minVersion = (int)propValue.Value;
					}
					if (propValue.PropTag == PropTag.OverallAgeLimit && propValue.Value is int)
					{
						maxVersion = (int)propValue.Value;
					}
					if (propValue.PropTag == PropTag.RequestedDatabaseSchemaVersion && propValue.Value is int)
					{
						requestedVersion = (int)propValue.Value;
					}
				}
			}
		}

		// Token: 0x060020F3 RID: 8435 RVA: 0x00097D35 File Offset: 0x00095F35
		public void Close()
		{
			this.Dispose();
		}

		// Token: 0x060020F4 RID: 8436 RVA: 0x00097D3D File Offset: 0x00095F3D
		protected override Exception GetOperationCanceledException(string operationName, OperationAbortedException abortedEx)
		{
			return MapiExceptionHelper.CancelException(ReplayStrings.ReplayStoreOperationAbortedException(operationName), abortedEx);
		}

		// Token: 0x060020F5 RID: 8437 RVA: 0x00097D50 File Offset: 0x00095F50
		protected override Exception GetOperationTimedOutException(string operationName, TimeoutException timeoutEx)
		{
			return MapiExceptionHelper.TimeoutException(ReplayStrings.ReplayTestStoreConnectivityTimedoutException(operationName, timeoutEx.Message), timeoutEx);
		}

		// Token: 0x060020F6 RID: 8438 RVA: 0x00097D69 File Offset: 0x00095F69
		protected override void InternalProtectedDispose()
		{
			if (this.m_exRpcAdmin != null)
			{
				this.m_exRpcAdmin.Dispose();
				this.m_exRpcAdmin = null;
			}
		}

		// Token: 0x060020F7 RID: 8439 RVA: 0x00097D88 File Offset: 0x00095F88
		private void CallExRpcWithTimeout(string operationName, TimeSpan timeout, Action exRpcAction)
		{
			try
			{
				InvokeWithTimeout.Invoke(exRpcAction, timeout);
			}
			catch (TimeoutException ex)
			{
				throw MapiExceptionHelper.TimeoutException(ReplayStrings.ReplayTestStoreConnectivityTimedoutException(operationName, ex.Message), ex);
			}
		}

		// Token: 0x060020F8 RID: 8440 RVA: 0x00097DE4 File Offset: 0x00095FE4
		private void CreateExRpcAdminWithTimeout(TimeSpan timeout)
		{
			Action operation = delegate()
			{
				this.m_exRpcAdmin = ExRpcAdmin.Create(this.m_clientTypeId, this.m_serverNameOrFqdn, null, null, null);
			};
			base.ProtectedCallWithTimeout("ExRpcAdmin.Create", timeout, operation);
		}

		// Token: 0x04000D53 RID: 3411
		private readonly object m_lockObj = new object();

		// Token: 0x04000D54 RID: 3412
		private readonly string m_serverNameOrFqdn;

		// Token: 0x04000D55 RID: 3413
		private readonly string m_clientTypeId;

		// Token: 0x04000D56 RID: 3414
		private ExRpcAdmin m_exRpcAdmin;
	}
}
