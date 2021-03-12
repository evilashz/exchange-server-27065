using System;
using System.Collections.Generic;
using System.Net;
using System.Threading;
using Microsoft.Office.Datacenter.ActiveMonitoring;

namespace Microsoft.Exchange.Monitoring.ActiveMonitoring.ActiveSync.Probes
{
	// Token: 0x0200005D RID: 93
	public class ActiveSyncProbeStateObject
	{
		// Token: 0x170000A5 RID: 165
		// (get) Token: 0x060002ED RID: 749 RVA: 0x00013BDE File Offset: 0x00011DDE
		// (set) Token: 0x060002EE RID: 750 RVA: 0x00013BE6 File Offset: 0x00011DE6
		internal HttpWebRequest WebRequest { get; set; }

		// Token: 0x170000A6 RID: 166
		// (get) Token: 0x060002EF RID: 751 RVA: 0x00013BEF File Offset: 0x00011DEF
		// (set) Token: 0x060002F0 RID: 752 RVA: 0x00013BF7 File Offset: 0x00011DF7
		internal ProbeState State { get; set; }

		// Token: 0x170000A7 RID: 167
		// (get) Token: 0x060002F1 RID: 753 RVA: 0x00013C00 File Offset: 0x00011E00
		// (set) Token: 0x060002F2 RID: 754 RVA: 0x00013C08 File Offset: 0x00011E08
		internal ProbeResult Result { get; private set; }

		// Token: 0x170000A8 RID: 168
		// (get) Token: 0x060002F3 RID: 755 RVA: 0x00013C11 File Offset: 0x00011E11
		// (set) Token: 0x060002F4 RID: 756 RVA: 0x00013C19 File Offset: 0x00011E19
		internal List<ActiveSyncWebResponse> WebResponses { get; set; }

		// Token: 0x170000A9 RID: 169
		// (get) Token: 0x060002F5 RID: 757 RVA: 0x00013C22 File Offset: 0x00011E22
		// (set) Token: 0x060002F6 RID: 758 RVA: 0x00013C2A File Offset: 0x00011E2A
		internal DateTime TimeoutLimit { get; set; }

		// Token: 0x170000AA RID: 170
		// (get) Token: 0x060002F7 RID: 759 RVA: 0x00013C33 File Offset: 0x00011E33
		// (set) Token: 0x060002F8 RID: 760 RVA: 0x00013C3B File Offset: 0x00011E3B
		internal ManualResetEvent ResetEvent { get; set; }

		// Token: 0x170000AB RID: 171
		// (get) Token: 0x060002F9 RID: 761 RVA: 0x00013C44 File Offset: 0x00011E44
		internal int LastResponseIndex
		{
			get
			{
				if (this.WebResponses.Count <= 0)
				{
					return -1;
				}
				return this.WebResponses.Count - 1;
			}
		}

		// Token: 0x170000AC RID: 172
		// (get) Token: 0x060002FA RID: 762 RVA: 0x00013C63 File Offset: 0x00011E63
		// (set) Token: 0x060002FB RID: 763 RVA: 0x00013C6B File Offset: 0x00011E6B
		internal string HostOverrideValue { get; set; }

		// Token: 0x170000AD RID: 173
		// (get) Token: 0x060002FC RID: 764 RVA: 0x00013C74 File Offset: 0x00011E74
		// (set) Token: 0x060002FD RID: 765 RVA: 0x00013C7C File Offset: 0x00011E7C
		internal string ProbeErrorResponse { get; set; }

		// Token: 0x060002FE RID: 766 RVA: 0x00013C85 File Offset: 0x00011E85
		internal ActiveSyncProbeStateObject(HttpWebRequest request, ProbeResult result, ProbeState startingState)
		{
			this.WebRequest = request;
			this.Result = result;
			this.State = startingState;
			this.WebResponses = new List<ActiveSyncWebResponse>();
		}
	}
}
