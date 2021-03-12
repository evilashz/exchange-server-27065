using System;
using System.Diagnostics;
using System.Threading;
using Microsoft.Exchange.Cluster.Shared;

namespace Microsoft.Exchange.Cluster.ActiveManagerServer
{
	// Token: 0x02000075 RID: 117
	internal class AmPeriodicEventManager
	{
		// Token: 0x060004F0 RID: 1264 RVA: 0x0001A6B0 File Offset: 0x000188B0
		internal AmPeriodicEventManager()
		{
		}

		// Token: 0x17000100 RID: 256
		// (get) Token: 0x060004F1 RID: 1265 RVA: 0x0001A6C3 File Offset: 0x000188C3
		// (set) Token: 0x060004F2 RID: 1266 RVA: 0x0001A6CB File Offset: 0x000188CB
		internal bool IsExiting { get; private set; }

		// Token: 0x060004F3 RID: 1267 RVA: 0x0001A6D4 File Offset: 0x000188D4
		internal static bool IsPeriodicEvent(AmEvtBase evt)
		{
			return evt is AmEvtPeriodicDbStateAnalyze || evt is AmEvtPeriodicCheckMismountedDatabase;
		}

		// Token: 0x060004F4 RID: 1268 RVA: 0x0001A6EC File Offset: 0x000188EC
		internal void EnqueuePeriodicEventIfRequired()
		{
			if (!RegistryParameters.AmPeriodicDatabaseAnalyzerEnabled)
			{
				return;
			}
			if (this.IsExiting)
			{
				return;
			}
			lock (this.m_locker)
			{
				if (this.m_checkDbWatch == null)
				{
					this.m_checkDbWatch = new Stopwatch();
					this.m_checkDbWatch.Reset();
					this.m_checkDbWatch.Start();
				}
			}
			if (this.m_checkDbWatch.ElapsedMilliseconds >= (long)RegistryParameters.AmPeriodicDatabaseAnalyzerIntervalInMSec)
			{
				AmConfig config = AmSystemManager.Instance.Config;
				if (config.IsDecidingAuthority)
				{
					AmEvtPeriodicDbStateAnalyze amEvtPeriodicDbStateAnalyze = new AmEvtPeriodicDbStateAnalyze();
					amEvtPeriodicDbStateAnalyze.Notify();
				}
				else if (config.IsSAM)
				{
					AmEvtPeriodicCheckMismountedDatabase amEvtPeriodicCheckMismountedDatabase = new AmEvtPeriodicCheckMismountedDatabase();
					amEvtPeriodicCheckMismountedDatabase.Notify();
				}
				this.m_checkDbWatch.Reset();
				this.m_checkDbWatch.Start();
			}
		}

		// Token: 0x060004F5 RID: 1269 RVA: 0x0001A7C4 File Offset: 0x000189C4
		internal bool EnqueueDeferredSystemEvent(AmEvtBase evt, int after)
		{
			bool result;
			lock (this.m_locker)
			{
				if (this.m_isInUse || this.IsExiting)
				{
					result = false;
				}
				else
				{
					this.m_isInUse = true;
					if (this.m_deferredTimer == null)
					{
						this.m_deferredTimer = new Timer(new TimerCallback(this.DeferredActionCallback), null, -1, -1);
					}
					this.m_deferredEvt = evt;
					this.m_deferredTimer.Change(after, -1);
					result = true;
				}
			}
			return result;
		}

		// Token: 0x060004F6 RID: 1270 RVA: 0x0001A854 File Offset: 0x00018A54
		internal void DeferredActionCallback(object unused)
		{
			lock (this.m_locker)
			{
				this.m_deferredTimer.Change(-1, -1);
				if (!this.IsExiting)
				{
					this.m_deferredEvt.Notify();
				}
				this.m_isInUse = false;
			}
		}

		// Token: 0x060004F7 RID: 1271 RVA: 0x0001A8B8 File Offset: 0x00018AB8
		internal void Stop()
		{
			lock (this.m_locker)
			{
				this.IsExiting = true;
				if (this.m_deferredTimer != null)
				{
					this.m_deferredTimer.Change(-1, -1);
				}
				if (this.m_checkDbWatch != null)
				{
					this.m_checkDbWatch.Reset();
				}
			}
		}

		// Token: 0x04000215 RID: 533
		private object m_locker = new object();

		// Token: 0x04000216 RID: 534
		private Timer m_deferredTimer;

		// Token: 0x04000217 RID: 535
		private AmEvtBase m_deferredEvt;

		// Token: 0x04000218 RID: 536
		private bool m_isInUse;

		// Token: 0x04000219 RID: 537
		private Stopwatch m_checkDbWatch;
	}
}
