using System;
using Microsoft.Exchange.Common.HA;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x0200037C RID: 892
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class ShipLogsSetBroken : ISetBroken, ISetDisconnected
	{
		// Token: 0x1700093E RID: 2366
		// (get) Token: 0x060023B9 RID: 9145 RVA: 0x000A7730 File Offset: 0x000A5930
		private ISetBroken SetBrokenInterface
		{
			get
			{
				ISetBroken result;
				lock (this)
				{
					ISetBroken setBroken = this.m_setBroken;
					if (this.m_setBrokenForAcll != null)
					{
						setBroken = this.m_setBrokenForAcll;
					}
					result = setBroken;
				}
				return result;
			}
		}

		// Token: 0x1700093F RID: 2367
		// (get) Token: 0x060023BA RID: 9146 RVA: 0x000A7780 File Offset: 0x000A5980
		private ISetDisconnected SetDisconnectedInterface
		{
			get
			{
				ISetDisconnected result;
				lock (this)
				{
					ISetDisconnected setDisconnected = this.m_setDisconnected;
					if (this.m_setDisconnectedForAcll != null)
					{
						setDisconnected = this.m_setDisconnectedForAcll;
					}
					result = setDisconnected;
				}
				return result;
			}
		}

		// Token: 0x060023BB RID: 9147 RVA: 0x000A77D0 File Offset: 0x000A59D0
		public ShipLogsSetBroken(ISetBroken setBroken, ISetDisconnected setDisconnected)
		{
			this.m_setBroken = setBroken;
			this.m_setDisconnected = setDisconnected;
		}

		// Token: 0x060023BC RID: 9148 RVA: 0x000A77E8 File Offset: 0x000A59E8
		internal void SetReportingCallbacksForAcll(ISetBroken setBroken, ISetDisconnected setDisconnected)
		{
			lock (this)
			{
				this.m_setBrokenForAcll = setBroken;
				this.m_setDisconnectedForAcll = setDisconnected;
			}
		}

		// Token: 0x060023BD RID: 9149 RVA: 0x000A782C File Offset: 0x000A5A2C
		public void SetBroken(FailureTag failureTag, ExEventLog.EventTuple setBrokenEventTuple, params string[] setBrokenArgs)
		{
			lock (this)
			{
				this.SetBrokenInterface.SetBroken(failureTag, setBrokenEventTuple, setBrokenArgs);
			}
		}

		// Token: 0x060023BE RID: 9150 RVA: 0x000A7870 File Offset: 0x000A5A70
		public void SetBroken(FailureTag failureTag, ExEventLog.EventTuple setBrokenEventTuple, Exception exception, params string[] setBrokenArgs)
		{
			lock (this)
			{
				this.SetBrokenInterface.SetBroken(failureTag, setBrokenEventTuple, exception, setBrokenArgs);
			}
		}

		// Token: 0x060023BF RID: 9151 RVA: 0x000A78B8 File Offset: 0x000A5AB8
		public void ClearBroken()
		{
			lock (this)
			{
				this.m_setBroken.ClearBroken();
				if (this.m_setBrokenForAcll != null)
				{
					this.m_setBrokenForAcll.ClearBroken();
				}
			}
		}

		// Token: 0x060023C0 RID: 9152 RVA: 0x000A790C File Offset: 0x000A5B0C
		public void RestartInstanceSoon(bool fPrepareToStop)
		{
			lock (this)
			{
				this.m_setBroken.RestartInstanceSoon(fPrepareToStop);
				if (this.m_setBrokenForAcll != null)
				{
					this.m_setBrokenForAcll.RestartInstanceSoon(fPrepareToStop);
				}
			}
		}

		// Token: 0x060023C1 RID: 9153 RVA: 0x000A7964 File Offset: 0x000A5B64
		public void RestartInstanceSoonAdminVisible()
		{
			lock (this)
			{
				this.m_setBroken.RestartInstanceSoonAdminVisible();
				if (this.m_setBrokenForAcll != null)
				{
					this.m_setBrokenForAcll.RestartInstanceSoonAdminVisible();
				}
			}
		}

		// Token: 0x060023C2 RID: 9154 RVA: 0x000A79B8 File Offset: 0x000A5BB8
		public void RestartInstanceNow(ReplayConfigChangeHints restartReason)
		{
			lock (this)
			{
				this.m_setBroken.RestartInstanceNow(restartReason);
				if (this.m_setBrokenForAcll != null)
				{
					this.m_setBrokenForAcll.RestartInstanceNow(restartReason);
				}
			}
		}

		// Token: 0x17000940 RID: 2368
		// (get) Token: 0x060023C3 RID: 9155 RVA: 0x000A7A10 File Offset: 0x000A5C10
		public bool IsBroken
		{
			get
			{
				bool result;
				lock (this)
				{
					bool flag2 = this.m_setBroken.IsBroken;
					if (this.m_setBrokenForAcll != null)
					{
						flag2 = (flag2 || this.m_setBrokenForAcll.IsBroken);
					}
					result = flag2;
				}
				return result;
			}
		}

		// Token: 0x17000941 RID: 2369
		// (get) Token: 0x060023C4 RID: 9156 RVA: 0x000A7A70 File Offset: 0x000A5C70
		public LocalizedString ErrorMessage
		{
			get
			{
				LocalizedString result;
				lock (this)
				{
					LocalizedString errorMessage = this.m_setBroken.ErrorMessage;
					if (this.m_setBrokenForAcll != null && !this.m_setBrokenForAcll.ErrorMessage.IsEmpty)
					{
						errorMessage = this.m_setBrokenForAcll.ErrorMessage;
					}
					result = errorMessage;
				}
				return result;
			}
		}

		// Token: 0x17000942 RID: 2370
		// (get) Token: 0x060023C5 RID: 9157 RVA: 0x000A7AE0 File Offset: 0x000A5CE0
		public bool IsDisconnected
		{
			get
			{
				bool flag = this.m_setDisconnected.IsDisconnected;
				if (this.m_setDisconnectedForAcll != null)
				{
					flag = (flag || this.m_setDisconnectedForAcll.IsDisconnected);
				}
				return flag;
			}
		}

		// Token: 0x060023C6 RID: 9158 RVA: 0x000A7B14 File Offset: 0x000A5D14
		public void SetDisconnected(FailureTag failureTag, ExEventLog.EventTuple setDisconnectedEventTuple, params string[] setDisconnectedArgs)
		{
			this.SetDisconnectedInterface.SetDisconnected(failureTag, setDisconnectedEventTuple, setDisconnectedArgs);
		}

		// Token: 0x060023C7 RID: 9159 RVA: 0x000A7B24 File Offset: 0x000A5D24
		public void ClearDisconnected()
		{
			lock (this)
			{
				this.m_setDisconnected.ClearDisconnected();
				if (this.m_setDisconnectedForAcll != null)
				{
					this.m_setDisconnectedForAcll.ClearDisconnected();
				}
			}
		}

		// Token: 0x04000F4C RID: 3916
		private ISetBroken m_setBroken;

		// Token: 0x04000F4D RID: 3917
		private ISetDisconnected m_setDisconnected;

		// Token: 0x04000F4E RID: 3918
		private ISetBroken m_setBrokenForAcll;

		// Token: 0x04000F4F RID: 3919
		private ISetDisconnected m_setDisconnectedForAcll;
	}
}
