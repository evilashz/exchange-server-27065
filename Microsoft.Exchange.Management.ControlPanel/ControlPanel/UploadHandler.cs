using System;
using System.Web;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x0200020E RID: 526
	public class UploadHandler : IHttpHandler
	{
		// Token: 0x17001BF5 RID: 7157
		// (get) Token: 0x060026E1 RID: 9953 RVA: 0x000796B5 File Offset: 0x000778B5
		public bool IsReusable
		{
			get
			{
				return true;
			}
		}

		// Token: 0x060026E2 RID: 9954 RVA: 0x000796B8 File Offset: 0x000778B8
		public void ProcessRequest(HttpContext context)
		{
			PowerShellResults dataContract = ServerUploadManager.Instance.Value.ProcessFileUploadRequest(context);
			context.Response.Clear();
			context.Response.ContentType = "text/html";
			context.Response.Write(HttpUtility.HtmlEncode(dataContract.ToJsonString(null)));
			context.Response.End();
		}
	}
}
