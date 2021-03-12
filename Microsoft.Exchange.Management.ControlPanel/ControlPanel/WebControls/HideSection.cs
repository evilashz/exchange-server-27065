using System;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Microsoft.Exchange.Management.ControlPanel.WebControls
{
	// Token: 0x020005EF RID: 1519
	public class HideSection : WebControl
	{
		// Token: 0x06004439 RID: 17465 RVA: 0x000CE379 File Offset: 0x000CC579
		public HideSection() : base(HtmlTextWriterTag.Div)
		{
		}

		// Token: 0x0600443A RID: 17466 RVA: 0x000CE384 File Offset: 0x000CC584
		protected override void OnPreRender(EventArgs e)
		{
			base.OnPreRender(e);
			for (Control parent = this.Parent; parent != null; parent = parent.Parent)
			{
				Section section = parent as Section;
				if (section != null)
				{
					section.Visible = false;
					return;
				}
			}
		}
	}
}
