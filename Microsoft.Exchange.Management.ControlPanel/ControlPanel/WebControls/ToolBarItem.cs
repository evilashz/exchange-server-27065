using System;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Microsoft.Exchange.Management.ControlPanel.WebControls
{
	// Token: 0x02000671 RID: 1649
	public abstract class ToolBarItem : WebControl
	{
		// Token: 0x0600477D RID: 18301 RVA: 0x000D8EBD File Offset: 0x000D70BD
		protected ToolBarItem()
		{
			this.CssClass = "ToolBarItem ";
		}

		// Token: 0x17002772 RID: 10098
		// (get) Token: 0x0600477E RID: 18302 RVA: 0x000D8ED0 File Offset: 0x000D70D0
		protected override HtmlTextWriterTag TagKey
		{
			get
			{
				return HtmlTextWriterTag.Div;
			}
		}

		// Token: 0x0600477F RID: 18303 RVA: 0x000D8ED4 File Offset: 0x000D70D4
		public virtual string ToJavaScript()
		{
			return "new ToolBarItem()";
		}
	}
}
