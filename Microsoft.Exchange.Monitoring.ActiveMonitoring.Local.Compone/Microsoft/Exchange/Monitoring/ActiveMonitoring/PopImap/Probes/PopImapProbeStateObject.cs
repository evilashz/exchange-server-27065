using System;
using System.Collections.Generic;
using Microsoft.Exchange.Monitoring.ActiveMonitoring.Common;
using Microsoft.Office.Datacenter.ActiveMonitoring;

namespace Microsoft.Exchange.Monitoring.ActiveMonitoring.PopImap.Probes
{
	// Token: 0x020000B7 RID: 183
	public class PopImapProbeStateObject
	{
		// Token: 0x17000173 RID: 371
		// (get) Token: 0x06000627 RID: 1575 RVA: 0x000256B5 File Offset: 0x000238B5
		// (set) Token: 0x06000628 RID: 1576 RVA: 0x000256BD File Offset: 0x000238BD
		internal ProbeState State { get; set; }

		// Token: 0x17000174 RID: 372
		// (get) Token: 0x06000629 RID: 1577 RVA: 0x000256C6 File Offset: 0x000238C6
		// (set) Token: 0x0600062A RID: 1578 RVA: 0x000256CE File Offset: 0x000238CE
		internal ProbeResult Result { get; private set; }

		// Token: 0x17000175 RID: 373
		// (get) Token: 0x0600062B RID: 1579 RVA: 0x000256D7 File Offset: 0x000238D7
		// (set) Token: 0x0600062C RID: 1580 RVA: 0x000256DF File Offset: 0x000238DF
		internal List<TcpResponse> TcpResponses { get; set; }

		// Token: 0x17000176 RID: 374
		// (get) Token: 0x0600062D RID: 1581 RVA: 0x000256E8 File Offset: 0x000238E8
		// (set) Token: 0x0600062E RID: 1582 RVA: 0x000256F0 File Offset: 0x000238F0
		internal DateTime TimeoutLimit { get; set; }

		// Token: 0x17000177 RID: 375
		// (get) Token: 0x0600062F RID: 1583 RVA: 0x000256F9 File Offset: 0x000238F9
		internal int LastResponseIndex
		{
			get
			{
				if (this.TcpResponses.Count <= 0)
				{
					return -1;
				}
				return this.TcpResponses.Count - 1;
			}
		}

		// Token: 0x17000178 RID: 376
		// (get) Token: 0x06000630 RID: 1584 RVA: 0x00025718 File Offset: 0x00023918
		// (set) Token: 0x06000631 RID: 1585 RVA: 0x00025720 File Offset: 0x00023920
		internal string ProbeErrorResponse { get; set; }

		// Token: 0x17000179 RID: 377
		// (get) Token: 0x06000632 RID: 1586 RVA: 0x00025729 File Offset: 0x00023929
		// (set) Token: 0x06000633 RID: 1587 RVA: 0x00025731 File Offset: 0x00023931
		internal TcpConnection Connection { get; private set; }

		// Token: 0x1700017A RID: 378
		// (get) Token: 0x06000634 RID: 1588 RVA: 0x0002573A File Offset: 0x0002393A
		// (set) Token: 0x06000635 RID: 1589 RVA: 0x00025742 File Offset: 0x00023942
		internal string Command { get; set; }

		// Token: 0x1700017B RID: 379
		// (get) Token: 0x06000636 RID: 1590 RVA: 0x0002574B File Offset: 0x0002394B
		// (set) Token: 0x06000637 RID: 1591 RVA: 0x00025753 File Offset: 0x00023953
		internal string ExpectedTag { get; set; }

		// Token: 0x1700017C RID: 380
		// (get) Token: 0x06000638 RID: 1592 RVA: 0x0002575C File Offset: 0x0002395C
		// (set) Token: 0x06000639 RID: 1593 RVA: 0x00025764 File Offset: 0x00023964
		internal bool MultiLine { get; set; }

		// Token: 0x1700017D RID: 381
		// (get) Token: 0x0600063A RID: 1594 RVA: 0x0002576D File Offset: 0x0002396D
		// (set) Token: 0x0600063B RID: 1595 RVA: 0x00025775 File Offset: 0x00023975
		internal string UserAccount { get; set; }

		// Token: 0x1700017E RID: 382
		// (get) Token: 0x0600063C RID: 1596 RVA: 0x0002577E File Offset: 0x0002397E
		// (set) Token: 0x0600063D RID: 1597 RVA: 0x00025786 File Offset: 0x00023986
		internal string UserPassword { get; set; }

		// Token: 0x1700017F RID: 383
		// (get) Token: 0x0600063E RID: 1598 RVA: 0x0002578F File Offset: 0x0002398F
		// (set) Token: 0x0600063F RID: 1599 RVA: 0x00025797 File Offset: 0x00023997
		internal string FailingReason { get; set; }

		// Token: 0x17000180 RID: 384
		// (get) Token: 0x06000640 RID: 1600 RVA: 0x000257A0 File Offset: 0x000239A0
		// (set) Token: 0x06000641 RID: 1601 RVA: 0x000257A8 File Offset: 0x000239A8
		internal Exception ConnectionException { get; set; }

		// Token: 0x06000642 RID: 1602 RVA: 0x000257B1 File Offset: 0x000239B1
		internal PopImapProbeStateObject(TcpConnection protocolConnection, ProbeResult result, ProbeState startingState)
		{
			this.Connection = protocolConnection;
			this.Result = result;
			this.State = startingState;
			this.TcpResponses = new List<TcpResponse>();
		}
	}
}
