using System;
using Microsoft.Exchange.Clients.Owa.Core;

namespace Microsoft.Exchange.Clients.Owa.Premium
{
	// Token: 0x0200043A RID: 1082
	public class AttachFileHost : OwaPage, IRegistryOnlyForm
	{
		// Token: 0x17000AE1 RID: 2785
		// (get) Token: 0x06002715 RID: 10005 RVA: 0x000DF0B0 File Offset: 0x000DD2B0
		protected bool IsInline
		{
			get
			{
				return Utilities.GetQueryStringParameter(base.Request, "a", false) != null;
			}
		}

		// Token: 0x06002716 RID: 10006 RVA: 0x000DF0C9 File Offset: 0x000DD2C9
		protected void RenderWindowTitle()
		{
			if (this.IsInline)
			{
				base.SanitizingResponse.Write(SanitizedHtmlString.FromStringId(-1408141425));
				return;
			}
			base.SanitizingResponse.Write(SanitizedHtmlString.FromStringId(763095470));
		}

		// Token: 0x06002717 RID: 10007 RVA: 0x000DF0FE File Offset: 0x000DD2FE
		protected void RenderIframeSource()
		{
			base.SanitizingResponse.Write("?ae=Dialog&t=AttachFileDialog");
			if (this.IsInline)
			{
				base.SanitizingResponse.Write("&a=InsertImage");
			}
		}
	}
}
