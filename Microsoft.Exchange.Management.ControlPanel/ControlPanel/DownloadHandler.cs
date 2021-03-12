using System;
using System.Web;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x020001F6 RID: 502
	internal class DownloadHandler : IHttpHandler
	{
		// Token: 0x17001BD6 RID: 7126
		// (get) Token: 0x0600265F RID: 9823 RVA: 0x00076794 File Offset: 0x00074994
		public bool IsReusable
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06002660 RID: 9824 RVA: 0x00076797 File Offset: 0x00074997
		public void ProcessRequest(HttpContext context)
		{
			this.SetCommonHeadersOnResponse(context.Response);
			ServerDownloadManager.Instance.Value.ProcessDownloadRequest(context);
			context.Response.End();
		}

		// Token: 0x06002661 RID: 9825 RVA: 0x000767C0 File Offset: 0x000749C0
		private void SetCommonHeadersOnResponse(HttpResponse response)
		{
			response.Clear();
			response.BufferOutput = false;
			response.Cache.SetExpires(DateTime.UtcNow.AddDays(-1.0));
		}
	}
}
