using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.Exchange.Management.ControlPanel.WebControls;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x020005D2 RID: 1490
	public class EncodingLabel : Label
	{
		// Token: 0x06004351 RID: 17233 RVA: 0x000CBF40 File Offset: 0x000CA140
		protected override void RenderContents(HtmlTextWriter writer)
		{
			if (this.IsRequiredField)
			{
				this.Text = ClientStrings.RequiredFieldIndicator + this.Text;
			}
			if (this.SkipEncoding)
			{
				writer.Write(this.Text);
				return;
			}
			HttpUtility.HtmlEncode(this.Text, writer);
		}

		// Token: 0x06004352 RID: 17234 RVA: 0x000CBF8C File Offset: 0x000CA18C
		protected override void OnInit(EventArgs e)
		{
			base.OnInit(e);
			if (Util.IsIE())
			{
				base.Attributes.Add("aria-hidden", "false");
			}
		}

		// Token: 0x06004353 RID: 17235 RVA: 0x000CBFB4 File Offset: 0x000CA1B4
		private Properties FindPropertiesControl()
		{
			Control parent = this.Parent;
			while (parent != null && !(parent is Properties))
			{
				parent = parent.Parent;
			}
			return (Properties)parent;
		}

		// Token: 0x17002620 RID: 9760
		// (get) Token: 0x06004354 RID: 17236 RVA: 0x000CBFE2 File Offset: 0x000CA1E2
		private bool IsRequiredField
		{
			get
			{
				return !string.IsNullOrEmpty(base.Attributes["RequiredField"]);
			}
		}

		// Token: 0x17002621 RID: 9761
		// (get) Token: 0x06004355 RID: 17237 RVA: 0x000CBFFC File Offset: 0x000CA1FC
		// (set) Token: 0x06004356 RID: 17238 RVA: 0x000CC004 File Offset: 0x000CA204
		public bool SkipEncoding { get; set; }
	}
}
