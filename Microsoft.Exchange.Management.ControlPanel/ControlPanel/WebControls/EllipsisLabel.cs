using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;
using AjaxControlToolkit;

namespace Microsoft.Exchange.Management.ControlPanel.WebControls
{
	// Token: 0x020005D1 RID: 1489
	[ClientScriptResource("EllipsisLabel", "Microsoft.Exchange.Management.ControlPanel.Client.Common.js")]
	public class EllipsisLabel : WebControl, IScriptControl, INamingContainer
	{
		// Token: 0x06004348 RID: 17224 RVA: 0x000CBD38 File Offset: 0x000C9F38
		public EllipsisLabel() : base(HtmlTextWriterTag.Table)
		{
			this.label = new DivLabel();
		}

		// Token: 0x06004349 RID: 17225 RVA: 0x000CBD50 File Offset: 0x000C9F50
		protected override void OnPreRender(EventArgs e)
		{
			base.Attributes.Add("cellspacing", "0");
			base.Attributes.Add("cellpadding", "0");
			base.Attributes.Add("border", "0");
			base.Style.Add("table-layout", "fixed");
			base.Style.Add(HtmlTextWriterStyle.Width, "100%");
			if (!Util.IsFirefox() && (!Util.IsSafari() || !RtlUtil.IsRtl))
			{
				this.label.Style.Add(HtmlTextWriterStyle.TextOverflow, "ellipsis");
			}
			base.OnPreRender(e);
			if (this.Page != null)
			{
				ScriptManager.GetCurrent(this.Page).RegisterScriptControl<EllipsisLabel>(this);
			}
		}

		// Token: 0x0600434A RID: 17226 RVA: 0x000CBE0F File Offset: 0x000CA00F
		protected override void Render(HtmlTextWriter writer)
		{
			base.Render(writer);
			if (!base.DesignMode)
			{
				ScriptManager.GetCurrent(this.Page).RegisterScriptDescriptors(this);
			}
		}

		// Token: 0x0600434B RID: 17227 RVA: 0x000CBE34 File Offset: 0x000CA034
		protected override void CreateChildControls()
		{
			base.CreateChildControls();
			TableRow tableRow = new TableRow();
			TableCell tableCell = new TableCell();
			this.label.ID = "textContainer";
			this.label.Style.Add(HtmlTextWriterStyle.Overflow, "hidden");
			this.label.Style.Add(HtmlTextWriterStyle.WhiteSpace, "nowrap");
			this.label.Height = this.Height;
			tableCell.Controls.Add(this.label);
			tableRow.Controls.Add(tableCell);
			this.Controls.Add(tableRow);
		}

		// Token: 0x1700261E RID: 9758
		// (get) Token: 0x0600434C RID: 17228 RVA: 0x000CBECB File Offset: 0x000CA0CB
		public WebControl TextContainer
		{
			get
			{
				return this.label;
			}
		}

		// Token: 0x1700261F RID: 9759
		// (get) Token: 0x0600434D RID: 17229 RVA: 0x000CBED3 File Offset: 0x000CA0D3
		// (set) Token: 0x0600434E RID: 17230 RVA: 0x000CBEE0 File Offset: 0x000CA0E0
		public string Text
		{
			get
			{
				return this.label.Text;
			}
			set
			{
				this.label.Text = value;
				this.label.Attributes["aria-label"] = value;
			}
		}

		// Token: 0x0600434F RID: 17231 RVA: 0x000CBF04 File Offset: 0x000CA104
		IEnumerable<ScriptDescriptor> IScriptControl.GetScriptDescriptors()
		{
			ScriptControlDescriptor scriptControlDescriptor = new ScriptControlDescriptor("EllipsisLabel", this.ClientID);
			return new ScriptDescriptor[]
			{
				scriptControlDescriptor
			};
		}

		// Token: 0x06004350 RID: 17232 RVA: 0x000CBF2E File Offset: 0x000CA12E
		IEnumerable<ScriptReference> IScriptControl.GetScriptReferences()
		{
			return ScriptObjectBuilder.GetScriptReferences(typeof(EllipsisLabel));
		}

		// Token: 0x04002D99 RID: 11673
		private DivLabel label;
	}
}
