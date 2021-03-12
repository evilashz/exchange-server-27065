using System;

namespace Microsoft.Exchange.Net.MonitoringWebClient
{
	// Token: 0x020007BE RID: 1982
	internal class HttpWebEventArgs : EventArgs
	{
		// Token: 0x060028C3 RID: 10435 RVA: 0x000573D2 File Offset: 0x000555D2
		public HttpWebEventArgs(HttpWebRequestWrapper request, object eventState) : this(request, null, eventState)
		{
		}

		// Token: 0x060028C4 RID: 10436 RVA: 0x000573DD File Offset: 0x000555DD
		public HttpWebEventArgs(HttpWebRequestWrapper request, HttpWebResponseWrapper response, object eventState)
		{
			this.Request = request;
			this.Response = response;
			this.EventState = eventState;
		}

		// Token: 0x17000AB7 RID: 2743
		// (get) Token: 0x060028C5 RID: 10437 RVA: 0x000573FA File Offset: 0x000555FA
		// (set) Token: 0x060028C6 RID: 10438 RVA: 0x00057402 File Offset: 0x00055602
		public HttpWebRequestWrapper Request { get; set; }

		// Token: 0x17000AB8 RID: 2744
		// (get) Token: 0x060028C7 RID: 10439 RVA: 0x0005740B File Offset: 0x0005560B
		// (set) Token: 0x060028C8 RID: 10440 RVA: 0x00057413 File Offset: 0x00055613
		public HttpWebResponseWrapper Response { get; set; }

		// Token: 0x17000AB9 RID: 2745
		// (get) Token: 0x060028C9 RID: 10441 RVA: 0x0005741C File Offset: 0x0005561C
		// (set) Token: 0x060028CA RID: 10442 RVA: 0x00057424 File Offset: 0x00055624
		public object EventState { get; set; }
	}
}
