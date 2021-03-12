using System;
using System.Web;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x02000564 RID: 1380
	public class ViewVirtualDirectory : EcpContentPage
	{
		// Token: 0x0600405C RID: 16476 RVA: 0x000C43EC File Offset: 0x000C25EC
		protected override void OnInit(EventArgs e)
		{
			string text = HttpContext.Current.Request.QueryString["schema"];
			if (string.Equals(text, "OWAVirtualDirectory") || string.Equals(text, "ECPVirtualDirectory") || string.Equals(text, "AutoDiscVirtualDirectory") || string.Equals(text, "EASVirtualDirectory") || string.Equals(text, "EWSVirtualDirectory") || string.Equals(text, "OABVirtualDirectory") || string.Equals(text, "PowershellVirtualDirectory"))
			{
				this.vdirSDO.ServiceUrl = new WebServiceReference(string.Format("{0}DDI/DDIService.svc?schema={1}&workflow=GetForSDO", EcpUrl.EcpVDirForStaticResource, text));
				base.OnInit(e);
				return;
			}
			throw new BadQueryParameterException(string.Format("schema {0} is not reconized.", text));
		}

		// Token: 0x04002ADB RID: 10971
		protected PropertyPageSheet vdirSDO;
	}
}
