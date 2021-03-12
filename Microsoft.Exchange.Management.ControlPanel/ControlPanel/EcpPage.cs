using System;
using System.Threading;
using System.Web;
using System.Web.UI;
using Microsoft.Exchange.Management.ControlPanel.WebControls;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x0200002D RID: 45
	public class EcpPage : Page
	{
		// Token: 0x170017DF RID: 6111
		// (get) Token: 0x060018E7 RID: 6375 RVA: 0x0004E3E2 File Offset: 0x0004C5E2
		// (set) Token: 0x060018E8 RID: 6376 RVA: 0x0004E3EA File Offset: 0x0004C5EA
		public FeatureSet FeatureSet { get; set; }

		// Token: 0x060018E9 RID: 6377 RVA: 0x0004E3F3 File Offset: 0x0004C5F3
		protected override void OnInit(EventArgs e)
		{
			this.SetThreadPrincipal();
			base.OnInit(e);
			base.Response.Headers["X-BEResourcePath"] = HttpRuntime.AppDomainAppVirtualPath + "/" + ThemeResource.ApplicationVersion;
		}

		// Token: 0x060018EA RID: 6378 RVA: 0x0004E42B File Offset: 0x0004C62B
		protected override void OnLoad(EventArgs e)
		{
			this.SetThreadPrincipal();
			base.OnLoad(e);
		}

		// Token: 0x060018EB RID: 6379 RVA: 0x0004E43A File Offset: 0x0004C63A
		protected override void OnPreRender(EventArgs e)
		{
			this.SetThreadPrincipal();
			base.OnPreRender(e);
		}

		// Token: 0x060018EC RID: 6380 RVA: 0x0004E449 File Offset: 0x0004C649
		protected override void OnPreRenderComplete(EventArgs e)
		{
			this.SetThreadPrincipal();
			base.OnPreRenderComplete(e);
		}

		// Token: 0x060018ED RID: 6381 RVA: 0x0004E458 File Offset: 0x0004C658
		private void SetThreadPrincipal()
		{
			if (base.User == Thread.CurrentPrincipal)
			{
				((IRbacSession)base.User).SetCurrentThreadPrincipal();
			}
		}

		// Token: 0x04001A89 RID: 6793
		public const string BEResourcePathHeaderName = "X-BEResourcePath";
	}
}
