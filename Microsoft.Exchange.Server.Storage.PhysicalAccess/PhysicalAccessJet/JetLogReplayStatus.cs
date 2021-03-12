using System;
using System.Collections.Generic;
using System.Threading;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.ManagedStore.HA;
using Microsoft.Exchange.Server.Storage.Common;
using Microsoft.Exchange.Server.Storage.PhysicalAccess;
using Microsoft.Isam.Esent.Interop;
using Microsoft.Isam.Esent.Interop.Unpublished;

namespace Microsoft.Exchange.Server.Storage.PhysicalAccessJet
{
	// Token: 0x020000AD RID: 173
	internal class JetLogReplayStatus : DisposableBase, ILogReplayStatus
	{
		// Token: 0x060007A6 RID: 1958 RVA: 0x000250A3 File Offset: 0x000232A3
		internal static IDisposable SetRecordPassiveReplayFailureTestHook(Action<Exception> testDelegate)
		{
			return JetLogReplayStatus.recordPassiveReplayFailureTestHook.SetTestHook(testDelegate);
		}

		// Token: 0x170001EF RID: 495
		// (get) Token: 0x060007A7 RID: 1959 RVA: 0x000250B0 File Offset: 0x000232B0
		private bool PatchRequestPending
		{
			get
			{
				return this.patchPageNumber != 0U;
			}
		}

		// Token: 0x060007A8 RID: 1960 RVA: 0x000250C0 File Offset: 0x000232C0
		private static byte[] SerializeDatabaseInfo(JET_DBINFOMISC dbInfo)
		{
			byte[] result = null;
			if (dbInfo != null)
			{
				result = InteropShim.SerializeDatabaseInfo(dbInfo);
			}
			return result;
		}

		// Token: 0x060007A9 RID: 1961 RVA: 0x000250DC File Offset: 0x000232DC
		public JetLogReplayStatus(Func<bool, bool> passiveDatabaseAttachDetachHandler)
		{
			this.lockObject = new object();
			this.wakeEvent = new ManualResetEvent(false);
			this.corruptedPages = new List<uint>(0);
			this.passiveDatabaseAttachDetachHandler = passiveDatabaseAttachDetachHandler;
			this.logReplayInitiatedEventHandle = new ManualResetEvent(false);
		}

		// Token: 0x060007AA RID: 1962 RVA: 0x00025130 File Offset: 0x00023330
		public void InitializeWithInstance(Instance jetInstanceConfiguredPreInit)
		{
			this.jetInstance = jetInstanceConfiguredPreInit;
			this.enableDbScanInRecovery = this.jetInstance.Parameters.EnableDbScanInRecovery;
			this.jetParamsAreUsable = true;
		}

		// Token: 0x060007AB RID: 1963 RVA: 0x00025158 File Offset: 0x00023358
		public void SetLogReplayInitiatedEvent()
		{
			base.CheckDisposed();
			if (!this.logReplayInitiatedEventHandleWasSet)
			{
				this.logReplayInitiatedEventHandleWasSet = true;
				bool flag = this.logReplayInitiatedEventHandle.Set();
				if (ExTraceGlobals.LogReplayStatusTracer.IsTraceEnabled(TraceType.DebugTrace))
				{
					ExTraceGlobals.LogReplayStatusTracer.TraceDebug<string>(0L, "Signalling LogReplayInitiated event handle {0}.", flag ? "succeeded" : "FAILED");
				}
			}
		}

		// Token: 0x060007AC RID: 1964 RVA: 0x000251B4 File Offset: 0x000233B4
		public void WaitLogReplayInitiatedEvent()
		{
			base.CheckDisposed();
			bool flag = ExTraceGlobals.LogReplayStatusTracer.IsTraceEnabled(TraceType.DebugTrace);
			if (flag)
			{
				ExTraceGlobals.LogReplayStatusTracer.TraceDebug(0L, "Waiting for log replay to be initiated by the async. log replay thread.");
			}
			bool assertCondition = this.logReplayInitiatedEventHandle.WaitOne(TimeSpan.FromMinutes(10.0));
			Microsoft.Exchange.Server.Storage.Common.Globals.AssertRetail(assertCondition, "Passive database mount timeout.");
			if (flag)
			{
				ExTraceGlobals.LogReplayStatusTracer.TraceDebug(0L, "Done waiting for log replay to be initiated.");
			}
			this.logReplayInitiatedEventHandle.Dispose();
			this.logReplayInitiatedEventHandle = null;
		}

		// Token: 0x060007AD RID: 1965 RVA: 0x00025234 File Offset: 0x00023434
		void ILogReplayStatus.GetReplayStatus(out uint nextLogToReplay, out byte[] databaseInfo, out uint patchPageNumber, out byte[] patchToken, out byte[] patchData, out uint[] corruptPages)
		{
			base.CheckDisposed();
			using (LockManager.Lock(this.lockObject))
			{
				nextLogToReplay = this.nextLogToReplay;
				databaseInfo = JetLogReplayStatus.SerializeDatabaseInfo(this.databaseInfo);
				patchPageNumber = this.patchPageNumber;
				patchToken = this.patchToken;
				patchData = this.patchData;
				corruptPages = ((this.corruptedPages.Count > 0) ? this.corruptedPages.ToArray() : null);
				this.corruptedPages.Clear();
				this.patchPageNumber = 0U;
				this.patchToken = null;
				this.patchData = null;
				this.wakeEvent.Set();
			}
			if (ExTraceGlobals.LogReplayStatusTracer.IsTraceEnabled(TraceType.DebugTrace))
			{
				ExTraceGlobals.LogReplayStatusTracer.TraceDebug<uint, uint, string>(0L, "GetReplayStatus: nextLogToReplay={0}, patchPageNumber={1}, corruptPages={2}", nextLogToReplay, patchPageNumber, (corruptPages == null) ? string.Empty : string.Format("{0} entries", corruptPages.Length));
			}
			if (this.passiveReplayException != null && patchData == null && corruptPages == null)
			{
				throw new FatalDatabaseException("LogReplayFailure", this.passiveReplayException);
			}
		}

		// Token: 0x060007AE RID: 1966 RVA: 0x00025354 File Offset: 0x00023554
		void ILogReplayStatus.SetMaxLogGenerationToReplay(uint value, uint flags)
		{
			base.CheckDisposed();
			if ((flags & 1U) != 0U)
			{
				if ((flags & 2U) != 0U)
				{
					this.haRequestsDbScanEnable = true;
				}
				else if (this.haRequestsDbScanEnable)
				{
					using (LockManager.Lock(this.lockObject))
					{
						this.haRequestsDbScanEnable = false;
						if (this.jetParamsAreUsable)
						{
							this.DisableDbScan();
						}
					}
				}
			}
			this.SetMember<uint>(ref this.maxLogGenerationToReplay, value);
			if (ExTraceGlobals.LogReplayStatusTracer.IsTraceEnabled(TraceType.DebugTrace))
			{
				ExTraceGlobals.LogReplayStatusTracer.TraceDebug<uint, bool>(0L, "Set max log generation to replay to {0}. ScanEnable={1}", value, this.haRequestsDbScanEnable);
			}
		}

		// Token: 0x060007AF RID: 1967 RVA: 0x000253F8 File Offset: 0x000235F8
		void ILogReplayStatus.GetDatabaseInformation(out byte[] databaseInfo)
		{
			base.CheckDisposed();
			using (LockManager.Lock(this.lockObject))
			{
				databaseInfo = JetLogReplayStatus.SerializeDatabaseInfo(this.databaseInfo);
			}
		}

		// Token: 0x060007B0 RID: 1968 RVA: 0x00025444 File Offset: 0x00023644
		internal void RecordPassiveReplayFailure(Exception e)
		{
			if (ExTraceGlobals.LogReplayStatusTracer.IsTraceEnabled(TraceType.DebugTrace))
			{
				ExTraceGlobals.LogReplayStatusTracer.TraceDebug<Exception>(0L, "RecordPassiveReplayFailure: {0}", e);
			}
			this.passiveReplayException = e;
			if (JetLogReplayStatus.recordPassiveReplayFailureTestHook.Value != null)
			{
				JetLogReplayStatus.recordPassiveReplayFailureTestHook.Value(e);
			}
		}

		// Token: 0x060007B1 RID: 1969 RVA: 0x00025494 File Offset: 0x00023694
		internal JET_err RecoveryControlForPassiveDatabase(JET_RECOVERYCONTROL status, JET_err errDefault)
		{
			base.CheckDisposed();
			if (ExTraceGlobals.LogReplayStatusTracer.IsTraceEnabled(TraceType.DebugTrace))
			{
				ExTraceGlobals.LogReplayStatusTracer.TraceDebug<JET_RECOVERYCONTROL, bool>(0L, "RecoveryControl callback {0}, logReplayInitiatedEventHandleWasSet={1}", status, this.logReplayInitiatedEventHandleWasSet);
			}
			JET_err jet_err = errDefault;
			if (status is JET_SNOPENLOG)
			{
				this.HandleLogOpen((JET_SNOPENLOG)status);
				jet_err = this.WaitForReplayToContinue(errDefault);
			}
			else if (status is JET_SNBEGINUNDO)
			{
				if (ExTraceGlobals.FaultInjectionTracer.IsTraceEnabled(TraceType.FaultInjection))
				{
					ExTraceGlobals.FaultInjectionTracer.TraceTest(4106628413U);
				}
				Microsoft.Exchange.Server.Storage.Common.Globals.AssertRetail(this.transitionToActive, "Shouldn't begin undo unless we are transitioning to active");
			}
			else if (status is JET_SNMISSINGLOG)
			{
				jet_err = this.HandleMissingLog((JET_SNMISSINGLOG)status, errDefault);
			}
			else if (!(status is JET_SNOPENCHECKPOINT))
			{
				if (status is JET_SNNOTIFICATIONEVENT)
				{
					JET_SNNOTIFICATIONEVENT jet_SNNOTIFICATIONEVENT = (JET_SNNOTIFICATIONEVENT)status;
					if (!this.transitionToActive && jet_SNNOTIFICATIONEVENT.EventID == 301)
					{
						return (JET_err)(-1);
					}
				}
				else if (status is JET_SNSIGNALERROR)
				{
					if (JET_err.SoftRecoveryOnBackupDatabase == status.errDefault)
					{
						jet_err = JET_err.Success;
					}
				}
				else if (status is JET_SNDBATTACHED)
				{
					Microsoft.Exchange.Server.Storage.Common.Globals.AssertRetail(this.logReplayInitiatedEventHandleWasSet, "DbAttached should never be the first callback.");
					using (LockManager.Lock(this.lockObject))
					{
						this.jetParamsAreUsable = true;
					}
					if (ConfigurationSchema.EnableReadFromPassive.Value && this.passiveDatabaseAttachDetachHandler != null)
					{
						this.passiveDatabaseAttachDetachHandler(true);
					}
				}
				else if (status is JET_SNDBDETACHED)
				{
					Microsoft.Exchange.Server.Storage.Common.Globals.AssertRetail(this.logReplayInitiatedEventHandleWasSet, "DbDetached should never be the first callback.");
					using (LockManager.Lock(this.lockObject))
					{
						this.jetParamsAreUsable = false;
					}
					if (ConfigurationSchema.EnableReadFromPassive.Value && this.passiveDatabaseAttachDetachHandler != null)
					{
						this.passiveDatabaseAttachDetachHandler(false);
					}
				}
			}
			if (ExTraceGlobals.LogReplayStatusTracer.IsTraceEnabled(TraceType.DebugTrace))
			{
				ExTraceGlobals.LogReplayStatusTracer.TraceDebug<JET_err>(0L, "RecoveryControl callback returns {0}", jet_err);
			}
			this.SetLogReplayInitiatedEvent();
			return jet_err;
		}

		// Token: 0x060007B2 RID: 1970 RVA: 0x00025698 File Offset: 0x00023898
		internal void CorruptedPage(JET_SNCORRUPTEDPAGE corruptedPage)
		{
			base.CheckDisposed();
			int pageNumber = corruptedPage.pageNumber;
			if (ExTraceGlobals.LogReplayStatusTracer.IsTraceEnabled(TraceType.DebugTrace))
			{
				ExTraceGlobals.LogReplayStatusTracer.TraceDebug<int>(0L, "CorruptedPage callback: {0}", pageNumber);
			}
			using (LockManager.Lock(this.lockObject))
			{
				this.corruptedPages.Add(checked((uint)pageNumber));
			}
		}

		// Token: 0x060007B3 RID: 1971 RVA: 0x0002570C File Offset: 0x0002390C
		internal void Progress(JET_SNT snt, JET_SNPROG snprog)
		{
			base.CheckDisposed();
			if (ExTraceGlobals.LogReplayStatusTracer.IsTraceEnabled(TraceType.DebugTrace))
			{
				ExTraceGlobals.LogReplayStatusTracer.TraceDebug<int, int>(0L, "Progress callback {0}/{1}", snprog.cunitDone, snprog.cunitTotal);
			}
		}

		// Token: 0x060007B4 RID: 1972 RVA: 0x00025740 File Offset: 0x00023940
		internal void PagePatch(JET_SNPATCHREQUEST snpatch)
		{
			base.CheckDisposed();
			if (ExTraceGlobals.LogReplayStatusTracer.IsTraceEnabled(TraceType.DebugTrace))
			{
				ExTraceGlobals.LogReplayStatusTracer.TraceDebug<int>(0L, "PagePatch callback: {0}", snpatch.pageNumber);
			}
			using (LockManager.Lock(this.lockObject))
			{
				this.patchPageNumber = checked((uint)snpatch.pageNumber);
				this.patchToken = snpatch.pvToken;
				this.patchData = snpatch.pvData;
			}
			this.WaitForReplayToContinue(JET_err.Success);
		}

		// Token: 0x060007B5 RID: 1973 RVA: 0x000257D0 File Offset: 0x000239D0
		public JET_err InitStatusCallback(JET_SESID sesid, JET_SNP snp, JET_SNT snt, object data)
		{
			if (snp != JET_SNP.Restore)
			{
				switch (snp)
				{
				case (JET_SNP)18:
				{
					JET_RECOVERYCONTROL jet_RECOVERYCONTROL = (JET_RECOVERYCONTROL)data;
					return this.RecoveryControlForPassiveDatabase(jet_RECOVERYCONTROL, jet_RECOVERYCONTROL.errDefault);
				}
				case (JET_SNP)19:
					switch (snt)
					{
					case (JET_SNT)1101:
					{
						JET_SNPATCHREQUEST snpatch = (JET_SNPATCHREQUEST)data;
						this.PagePatch(snpatch);
						break;
					}
					case (JET_SNT)1102:
					{
						JET_SNCORRUPTEDPAGE corruptedPage = (JET_SNCORRUPTEDPAGE)data;
						this.CorruptedPage(corruptedPage);
						break;
					}
					}
					break;
				default:
					return JET_err.Success;
				}
			}
			else if (snt == JET_SNT.Progress)
			{
				JET_SNPROG snprog = (JET_SNPROG)data;
				this.Progress(snt, snprog);
			}
			return JET_err.Success;
		}

		// Token: 0x060007B6 RID: 1974 RVA: 0x00025864 File Offset: 0x00023A64
		internal void TransitionToActive()
		{
			base.CheckDisposed();
			if (ExTraceGlobals.LogReplayStatusTracer.IsTraceEnabled(TraceType.DebugTrace))
			{
				ExTraceGlobals.LogReplayStatusTracer.TraceDebug(0L, "Transitioning to active");
			}
			using (LockManager.Lock(this.lockObject))
			{
				this.transitionToActive = true;
				if (this.jetParamsAreUsable)
				{
					this.DisableDbScan();
				}
				this.wakeEvent.Set();
			}
		}

		// Token: 0x060007B7 RID: 1975 RVA: 0x000258E4 File Offset: 0x00023AE4
		internal void Cancel()
		{
			base.CheckDisposed();
			if (ExTraceGlobals.LogReplayStatusTracer.IsTraceEnabled(TraceType.DebugTrace))
			{
				ExTraceGlobals.LogReplayStatusTracer.TraceDebug(0L, "Cancelling replay");
			}
			this.SetMember<bool>(ref this.isCancelled, true);
		}

		// Token: 0x060007B8 RID: 1976 RVA: 0x00025917 File Offset: 0x00023B17
		protected override DisposeTracker InternalGetDisposeTracker()
		{
			return DisposeTracker.Get<JetLogReplayStatus>(this);
		}

		// Token: 0x060007B9 RID: 1977 RVA: 0x0002591F File Offset: 0x00023B1F
		protected override void InternalDispose(bool calledFromDispose)
		{
			if (calledFromDispose)
			{
				this.wakeEvent.Dispose();
				if (this.logReplayInitiatedEventHandle != null)
				{
					this.logReplayInitiatedEventHandle.Dispose();
				}
			}
		}

		// Token: 0x060007BA RID: 1978 RVA: 0x00025944 File Offset: 0x00023B44
		private void SetMember<T>(ref T member, T value)
		{
			using (LockManager.Lock(this.lockObject))
			{
				member = value;
				this.wakeEvent.Set();
			}
		}

		// Token: 0x060007BB RID: 1979 RVA: 0x00025990 File Offset: 0x00023B90
		private JET_err HandleMissingLog(JET_SNMISSINGLOG missingLogStatus, JET_err errDefault)
		{
			if (ExTraceGlobals.LogReplayStatusTracer.IsTraceEnabled(TraceType.DebugTrace))
			{
				ExTraceGlobals.LogReplayStatusTracer.TraceDebug(0L, "Missing log {0} (generation {1}), IsCurrentLog={2}, NextAction={3}, ErrDefault={4}", new object[]
				{
					missingLogStatus.wszLogFile,
					missingLogStatus.lGenMissing,
					missingLogStatus.fCurrentLog,
					missingLogStatus.eNextAction,
					missingLogStatus.errDefault
				});
			}
			if (JET_RECOVERYACTIONS.MissingLogContinueToRedo == missingLogStatus.eNextAction)
			{
				return JET_err.Success;
			}
			using (LockManager.Lock(this.lockObject))
			{
				if (JET_RECOVERYACTIONS.MissingLogContinueToUndo == missingLogStatus.eNextAction && this.transitionToActive)
				{
					if (missingLogStatus.fCurrentLog)
					{
						return JET_err.Success;
					}
					if ((long)missingLogStatus.lGenMissing > (long)((ulong)this.maxLogGenerationToReplay))
					{
						return JET_err.Success;
					}
				}
			}
			return errDefault;
		}

		// Token: 0x060007BC RID: 1980 RVA: 0x00025A70 File Offset: 0x00023C70
		private void DisableDbScan()
		{
			if (this.jetInstance != null && this.jetInstance.Parameters.EnableDbScanInRecovery != 0)
			{
				this.jetInstance.Parameters.EnableDbScanInRecovery = 0;
			}
		}

		// Token: 0x060007BD RID: 1981 RVA: 0x00025A9D File Offset: 0x00023C9D
		private void EnableDbScan()
		{
			if (this.jetInstance != null && this.jetInstance.Parameters.EnableDbScanInRecovery != this.enableDbScanInRecovery)
			{
				this.jetInstance.Parameters.EnableDbScanInRecovery = this.enableDbScanInRecovery;
			}
		}

		// Token: 0x060007BE RID: 1982 RVA: 0x00025AD8 File Offset: 0x00023CD8
		private void HandleLogOpen(JET_SNOPENLOG openLogStatus)
		{
			if (ExTraceGlobals.LogReplayStatusTracer.IsTraceEnabled(TraceType.DebugTrace))
			{
				ExTraceGlobals.LogReplayStatusTracer.TraceDebug(0L, "Opening log {0} (generation {1}), IsCurrentLog={2} for {3}", new object[]
				{
					openLogStatus.wszLogFile,
					openLogStatus.lGenNext,
					openLogStatus.fCurrentLog,
					openLogStatus.eReason
				});
			}
			if (JET_OpenLog.ForRedo != openLogStatus.eReason)
			{
				return;
			}
			using (LockManager.Lock(this.lockObject))
			{
				if (openLogStatus.cdbinfomisc > 0 && openLogStatus.rgdbinfomisc.Length > 0)
				{
					this.databaseInfo = openLogStatus.rgdbinfomisc[0];
				}
				this.nextLogToReplay = checked((uint)openLogStatus.lGenNext);
				if (this.jetInstance != null)
				{
					if (!this.haRequestsDbScanEnable || this.transitionToActive)
					{
						this.DisableDbScan();
					}
					else
					{
						this.EnableDbScan();
					}
				}
			}
		}

		// Token: 0x060007BF RID: 1983 RVA: 0x00025BCC File Offset: 0x00023DCC
		private JET_err WaitForReplayToContinue(JET_err errDefault)
		{
			JET_err result;
			for (;;)
			{
				using (LockManager.Lock(this.lockObject))
				{
					this.wakeEvent.Reset();
					if (this.isCancelled)
					{
						result = JET_err.RecoveredWithoutUndo;
						break;
					}
					if (this.transitionToActive)
					{
						result = errDefault;
						break;
					}
					if (!this.PatchRequestPending && this.nextLogToReplay <= this.maxLogGenerationToReplay)
					{
						result = errDefault;
						break;
					}
				}
				this.wakeEvent.WaitOne();
			}
			return result;
		}

		// Token: 0x040002C9 RID: 713
		private static readonly Hookable<Action<Exception>> recordPassiveReplayFailureTestHook = Hookable<Action<Exception>>.Create(true, null);

		// Token: 0x040002CA RID: 714
		private readonly object lockObject;

		// Token: 0x040002CB RID: 715
		private readonly ManualResetEvent wakeEvent;

		// Token: 0x040002CC RID: 716
		private readonly List<uint> corruptedPages;

		// Token: 0x040002CD RID: 717
		private readonly Func<bool, bool> passiveDatabaseAttachDetachHandler;

		// Token: 0x040002CE RID: 718
		private EventWaitHandle logReplayInitiatedEventHandle;

		// Token: 0x040002CF RID: 719
		private bool logReplayInitiatedEventHandleWasSet;

		// Token: 0x040002D0 RID: 720
		private JET_DBINFOMISC databaseInfo = new JET_DBINFOMISC();

		// Token: 0x040002D1 RID: 721
		private bool isCancelled;

		// Token: 0x040002D2 RID: 722
		private bool transitionToActive;

		// Token: 0x040002D3 RID: 723
		private bool jetParamsAreUsable;

		// Token: 0x040002D4 RID: 724
		private uint nextLogToReplay;

		// Token: 0x040002D5 RID: 725
		private uint maxLogGenerationToReplay;

		// Token: 0x040002D6 RID: 726
		private uint patchPageNumber;

		// Token: 0x040002D7 RID: 727
		private byte[] patchToken;

		// Token: 0x040002D8 RID: 728
		private byte[] patchData;

		// Token: 0x040002D9 RID: 729
		private Exception passiveReplayException;

		// Token: 0x040002DA RID: 730
		private Instance jetInstance;

		// Token: 0x040002DB RID: 731
		private int enableDbScanInRecovery;

		// Token: 0x040002DC RID: 732
		private bool haRequestsDbScanEnable;

		// Token: 0x020000AE RID: 174
		[Flags]
		internal enum LogReplayRpcFlags : uint
		{
			// Token: 0x040002DE RID: 734
			None = 0U,
			// Token: 0x040002DF RID: 735
			SetDbScan = 1U,
			// Token: 0x040002E0 RID: 736
			EnableDbScan = 2U
		}
	}
}
