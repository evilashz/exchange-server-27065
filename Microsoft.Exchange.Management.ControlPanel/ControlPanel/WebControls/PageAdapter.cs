using System;
using System.Web;
using System.Web.UI.Adapters;

namespace Microsoft.Exchange.Management.ControlPanel.WebControls
{
	// Token: 0x02000629 RID: 1577
	public class PageAdapter : PageAdapter
	{
		// Token: 0x060045A9 RID: 17833 RVA: 0x000D29B8 File Offset: 0x000D0BB8
		protected override void OnInit(EventArgs e)
		{
			base.Page.ViewStateUserKey = HttpContext.Current.GetSessionID();
			base.OnInit(e);
		}
	}
}
