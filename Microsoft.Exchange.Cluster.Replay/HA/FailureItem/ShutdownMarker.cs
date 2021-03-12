using System;
using System.Threading;

namespace Microsoft.Exchange.HA.FailureItem
{
	// Token: 0x0200018B RID: 395
	internal class ShutdownMarker
	{
		// Token: 0x170003F4 RID: 1012
		// (get) Token: 0x06000FCA RID: 4042 RVA: 0x0004474F File Offset: 0x0004294F
		internal bool IsShutdown
		{
			get
			{
				return this.m_isShutdown;
			}
		}

		// Token: 0x06000FCB RID: 4043 RVA: 0x00044758 File Offset: 0x00042958
		internal bool Enter()
		{
			lock (this.m_locker)
			{
				if (this.m_isShutdown)
				{
					return false;
				}
				if (this.m_isInUse)
				{
					return false;
				}
				this.m_isInUse = true;
			}
			return true;
		}

		// Token: 0x06000FCC RID: 4044 RVA: 0x000447B4 File Offset: 0x000429B4
		internal void Leave()
		{
			lock (this.m_locker)
			{
				this.m_isInUse = false;
				if (this.m_doneEvent != null)
				{
					this.m_doneEvent.Set();
				}
			}
		}

		// Token: 0x06000FCD RID: 4045 RVA: 0x0004480C File Offset: 0x00042A0C
		internal void TriggerShutdownAndWait()
		{
			lock (this.m_locker)
			{
				this.m_isShutdown = true;
				if (this.m_isInUse)
				{
					this.m_doneEvent = new AutoResetEvent(false);
				}
			}
			if (this.m_doneEvent != null)
			{
				this.m_doneEvent.WaitOne();
				this.m_doneEvent.Close();
				this.m_doneEvent = null;
			}
		}

		// Token: 0x04000679 RID: 1657
		private object m_locker = new object();

		// Token: 0x0400067A RID: 1658
		private bool m_isShutdown;

		// Token: 0x0400067B RID: 1659
		private bool m_isInUse;

		// Token: 0x0400067C RID: 1660
		private AutoResetEvent m_doneEvent;
	}
}
