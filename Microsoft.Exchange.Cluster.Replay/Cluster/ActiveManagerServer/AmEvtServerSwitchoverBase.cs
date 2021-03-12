using System;
using System.Collections.Generic;
using System.Threading;
using Microsoft.Exchange.Cluster.Shared;

namespace Microsoft.Exchange.Cluster.ActiveManagerServer
{
	// Token: 0x02000083 RID: 131
	internal abstract class AmEvtServerSwitchoverBase : AmEvtBase
	{
		// Token: 0x0600052D RID: 1325 RVA: 0x0001ACB1 File Offset: 0x00018EB1
		internal AmEvtServerSwitchoverBase(AmServerName nodeName)
		{
			this.NodeName = nodeName;
			this.m_completionEvent = new ManualResetEvent(false);
		}

		// Token: 0x1700010E RID: 270
		// (get) Token: 0x0600052E RID: 1326 RVA: 0x0001ACD7 File Offset: 0x00018ED7
		// (set) Token: 0x0600052F RID: 1327 RVA: 0x0001ACDF File Offset: 0x00018EDF
		internal AmServerName NodeName { get; set; }

		// Token: 0x1700010F RID: 271
		// (get) Token: 0x06000530 RID: 1328 RVA: 0x0001ACE8 File Offset: 0x00018EE8
		// (set) Token: 0x06000531 RID: 1329 RVA: 0x0001ACF0 File Offset: 0x00018EF0
		internal bool IsDone { get; set; }

		// Token: 0x17000110 RID: 272
		// (get) Token: 0x06000532 RID: 1330 RVA: 0x0001ACF9 File Offset: 0x00018EF9
		// (set) Token: 0x06000533 RID: 1331 RVA: 0x0001AD01 File Offset: 0x00018F01
		internal List<AmDbOperation> OperationList { get; private set; }

		// Token: 0x06000534 RID: 1332 RVA: 0x0001AD0A File Offset: 0x00018F0A
		public override string ToString()
		{
			return string.Format("{0}: Params: (Nodename={1})", base.GetType().Name, this.NodeName);
		}

		// Token: 0x06000535 RID: 1333 RVA: 0x0001AD28 File Offset: 0x00018F28
		internal void SwitchoverCompletedCallback(List<AmDbOperation> operationList)
		{
			lock (this.m_locker)
			{
				this.OperationList = operationList;
				this.IsDone = true;
				if (this.m_completionEvent != null)
				{
					this.m_completionEvent.Set();
				}
			}
		}

		// Token: 0x06000536 RID: 1334 RVA: 0x0001AD84 File Offset: 0x00018F84
		internal void WaitForSwitchoverComplete(TimeSpan timeout)
		{
			if (this.m_completionEvent != null)
			{
				this.m_completionEvent.WaitOne(timeout, false);
				lock (this.m_locker)
				{
					this.m_completionEvent.Close();
					this.m_completionEvent = null;
				}
			}
		}

		// Token: 0x06000537 RID: 1335 RVA: 0x0001ADE8 File Offset: 0x00018FE8
		internal void WaitForSwitchoverComplete()
		{
			this.WaitForSwitchoverComplete(TimeSpan.FromMilliseconds(-1.0));
		}

		// Token: 0x04000228 RID: 552
		private object m_locker = new object();

		// Token: 0x04000229 RID: 553
		private ManualResetEvent m_completionEvent;
	}
}
