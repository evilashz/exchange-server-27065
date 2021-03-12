using System;
using System.Web;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x020001FD RID: 509
	internal interface IDownloadHandler
	{
		// Token: 0x0600268C RID: 9868
		PowerShellResults ProcessRequest(HttpContext context);
	}
}
