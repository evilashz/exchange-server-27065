using System;
using System.Threading;
using System.Web;
using System.Web.UI;

namespace Microsoft.Exchange.Clients.Security
{
	// Token: 0x02000026 RID: 38
	public class LiveIdError : Page
	{
		// Token: 0x17000040 RID: 64
		// (get) Token: 0x06000100 RID: 256 RVA: 0x000097FD File Offset: 0x000079FD
		public bool IsRtl
		{
			get
			{
				return Thread.CurrentThread.CurrentUICulture.TextInfo.IsRightToLeft;
			}
		}

		// Token: 0x06000101 RID: 257 RVA: 0x00009814 File Offset: 0x00007A14
		protected override void OnLoad(EventArgs e)
		{
			this.errorInformation = (HttpContext.Current.Items["LiveIdErrorInformation"] as LiveIdErrorInformation);
			if (this.errorInformation == null)
			{
				this.errorInformation = new LiveIdErrorInformation();
				this.errorInformation.Message = Strings.ErrorTitle;
				this.errorInformation.MessageDetails = Strings.ErrorUnexpectedFailure;
			}
			else if (this.errorInformation.Exception != null)
			{
				HttpContext.Current.Response.Headers.Add("X-OWA-Error", this.errorInformation.Exception.GetType().FullName);
			}
			this.OnInit(e);
		}

		// Token: 0x06000102 RID: 258 RVA: 0x000098C1 File Offset: 0x00007AC1
		protected void RenderIcon()
		{
			base.Response.Write(this.errorInformation.Icon);
		}

		// Token: 0x06000103 RID: 259 RVA: 0x000098D9 File Offset: 0x00007AD9
		protected void RenderBackground()
		{
			base.Response.Write(this.errorInformation.Background);
		}

		// Token: 0x06000104 RID: 260 RVA: 0x000098F1 File Offset: 0x00007AF1
		protected void RenderErrorPageTitle()
		{
			base.Response.Write(Strings.ErrorTitle);
		}

		// Token: 0x06000105 RID: 261 RVA: 0x00009908 File Offset: 0x00007B08
		protected void RenderMessage()
		{
			base.Response.Write(this.errorInformation.Message);
		}

		// Token: 0x06000106 RID: 262 RVA: 0x00009920 File Offset: 0x00007B20
		protected void RenderMessageDetails()
		{
			base.Response.Write(this.errorInformation.MessageDetails);
		}

		// Token: 0x0400013A RID: 314
		protected LiveIdErrorInformation errorInformation;
	}
}
