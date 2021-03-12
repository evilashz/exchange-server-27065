using System;
using System.Threading;
using Microsoft.Exchange.Cluster.Replay;
using Microsoft.Exchange.Cluster.Shared;
using Microsoft.Exchange.Data.HA.DirectoryServices;
using Microsoft.Exchange.ThirdPartyReplication;

namespace Microsoft.Exchange.Cluster.ActiveManagerServer
{
	// Token: 0x02000095 RID: 149
	internal class AmActiveThirdPartyMove
	{
		// Token: 0x06000612 RID: 1554 RVA: 0x0001E3D2 File Offset: 0x0001C5D2
		public AmActiveThirdPartyMove(IADDatabase db, string activeNodeName, bool mountDesired)
		{
			this.m_db = db;
			this.m_currentActiveNodeName = activeNodeName;
			this.m_mountDesired = mountDesired;
			this.Response = NotificationResponse.Incomplete;
		}

		// Token: 0x1700014E RID: 334
		// (get) Token: 0x06000613 RID: 1555 RVA: 0x0001E3F6 File Offset: 0x0001C5F6
		// (set) Token: 0x06000614 RID: 1556 RVA: 0x0001E3FE File Offset: 0x0001C5FE
		public NotificationResponse Response { get; set; }

		// Token: 0x06000615 RID: 1557 RVA: 0x0001E408 File Offset: 0x0001C608
		internal void Notify(WaitCallback compRtn)
		{
			this.m_completionCallback = compRtn;
			AmTrace.Debug("AMTPR: Queuing notification for database {0}", new object[]
			{
				this.m_db.Name
			});
			ThreadPool.QueueUserWorkItem(new WaitCallback(this.NotifyProcessing));
		}

		// Token: 0x06000616 RID: 1558 RVA: 0x0001E450 File Offset: 0x0001C650
		private void NotifyProcessing(object dummy)
		{
			try
			{
				this.Response = ThirdPartyManager.Instance.DatabaseMoveNeeded(this.m_db.Guid, this.m_currentActiveNodeName, this.m_mountDesired);
			}
			finally
			{
				AmTrace.Debug("AMTPR: Invoking completion callback for database {0}. Response={1}", new object[]
				{
					this.m_db.Name,
					this.Response
				});
				this.m_completionCallback(this);
			}
		}

		// Token: 0x04000282 RID: 642
		private IADDatabase m_db;

		// Token: 0x04000283 RID: 643
		private string m_currentActiveNodeName;

		// Token: 0x04000284 RID: 644
		private bool m_mountDesired;

		// Token: 0x04000285 RID: 645
		private WaitCallback m_completionCallback;
	}
}
