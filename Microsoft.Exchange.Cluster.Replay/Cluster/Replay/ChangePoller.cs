using System;
using System.Threading;
using Microsoft.Exchange.Cluster.Shared;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x02000017 RID: 23
	internal abstract class ChangePoller : IStartStop
	{
		// Token: 0x060000CF RID: 207 RVA: 0x00006000 File Offset: 0x00004200
		public ChangePoller(bool fMakeShutdownEvent)
		{
			if (fMakeShutdownEvent)
			{
				this.m_shutdownEvent = new ManualResetEvent(false);
			}
		}

		// Token: 0x17000022 RID: 34
		// (get) Token: 0x060000D0 RID: 208 RVA: 0x00006017 File Offset: 0x00004217
		public bool StopCalled
		{
			get
			{
				return this.m_fShutdown;
			}
		}

		// Token: 0x060000D1 RID: 209 RVA: 0x00006020 File Offset: 0x00004220
		public void Start()
		{
			lock (this)
			{
				this.m_pollerThread = new Thread(new ThreadStart(this.PollerThread));
				this.m_pollerThread.Start();
			}
		}

		// Token: 0x060000D2 RID: 210 RVA: 0x00006078 File Offset: 0x00004278
		public virtual void PrepareToStop()
		{
			this.m_fShutdown = true;
			if (this.m_shutdownEvent != null)
			{
				this.m_shutdownEvent.Set();
			}
		}

		// Token: 0x060000D3 RID: 211 RVA: 0x00006098 File Offset: 0x00004298
		public virtual void Stop()
		{
			if (!this.m_fShutdown)
			{
				this.PrepareToStop();
			}
			lock (this)
			{
				if (this.m_pollerThread != null)
				{
					this.m_pollerThread.Join();
				}
				if (this.m_shutdownEvent != null)
				{
					this.m_shutdownEvent.Close();
					this.m_shutdownEvent = null;
				}
			}
		}

		// Token: 0x060000D4 RID: 212
		protected abstract void PollerThread();

		// Token: 0x04000057 RID: 87
		protected bool m_fShutdown;

		// Token: 0x04000058 RID: 88
		protected ManualResetEvent m_shutdownEvent;

		// Token: 0x04000059 RID: 89
		private Thread m_pollerThread;
	}
}
