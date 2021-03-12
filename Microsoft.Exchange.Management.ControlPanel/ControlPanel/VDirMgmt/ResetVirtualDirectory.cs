using System;
using System.Web;

namespace Microsoft.Exchange.Management.ControlPanel.VDirMgmt
{
	// Token: 0x02000560 RID: 1376
	public class ResetVirtualDirectory : BaseForm
	{
		// Token: 0x06004032 RID: 16434 RVA: 0x000C3774 File Offset: 0x000C1974
		protected override void OnInit(EventArgs e)
		{
			string text = HttpContext.Current.Request.QueryString["schema"];
			if (string.Equals(text, "ResetOABVirtualDirectory") || string.Equals(text, "ResetOWAVirtualDirectory") || string.Equals(text, "ResetECPVirtualDirectory") || string.Equals(text, "ResetEASVirtualDirectory") || string.Equals(text, "ResetEWSVirtualDirectory") || string.Equals(text, "ResetAutoDiscVirtualDirectory") || string.Equals(text, "ResetPowerShellVirtualDirectory"))
			{
				this.resetVDir.ServiceUrl = new WebServiceReference(string.Format("{0}DDI/DDIService.svc?schema={1}", EcpUrl.EcpVDirForStaticResource, text));
				base.OnInit(e);
				return;
			}
			throw new BadQueryParameterException(string.Format("schema {0} is not reconized.", text));
		}

		// Token: 0x04002AC4 RID: 10948
		protected PropertyPageSheet resetVDir;
	}
}
