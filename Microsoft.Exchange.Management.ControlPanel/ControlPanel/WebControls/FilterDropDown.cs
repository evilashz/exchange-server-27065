using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Web.UI;
using System.Web.UI.WebControls;
using AjaxControlToolkit;

namespace Microsoft.Exchange.Management.ControlPanel.WebControls
{
	// Token: 0x020005E0 RID: 1504
	[ToolboxData("<{0}:FilterDropDown runat=server></{0}:FilterDropDown>")]
	[RequiredScript(typeof(CommonToolkitScripts))]
	[ClientScriptResource("FilterDropDown", "Microsoft.Exchange.Management.ControlPanel.Client.List.js")]
	public class FilterDropDown : DropDownList, IScriptControl
	{
		// Token: 0x0600438F RID: 17295 RVA: 0x000CC616 File Offset: 0x000CA816
		public FilterDropDown()
		{
			this.AutoPostBack = false;
			this.selectViewLabel = new Label();
		}

		// Token: 0x17002634 RID: 9780
		// (get) Token: 0x06004390 RID: 17296 RVA: 0x000CC630 File Offset: 0x000CA830
		// (set) Token: 0x06004391 RID: 17297 RVA: 0x000CC638 File Offset: 0x000CA838
		[DefaultValue(null)]
		public string LabelText { get; set; }

		// Token: 0x06004392 RID: 17298 RVA: 0x000CC644 File Offset: 0x000CA844
		IEnumerable<ScriptDescriptor> IScriptControl.GetScriptDescriptors()
		{
			ScriptControlDescriptor scriptControlDescriptor = new ScriptControlDescriptor("FilterDropDown", this.ClientID);
			return new ScriptDescriptor[]
			{
				scriptControlDescriptor
			};
		}

		// Token: 0x06004393 RID: 17299 RVA: 0x000CC66E File Offset: 0x000CA86E
		IEnumerable<ScriptReference> IScriptControl.GetScriptReferences()
		{
			return ScriptObjectBuilder.GetScriptReferences(typeof(FilterDropDown));
		}

		// Token: 0x06004394 RID: 17300 RVA: 0x000CC680 File Offset: 0x000CA880
		public override void RenderBeginTag(HtmlTextWriter writer)
		{
			this.selectViewLabel.Text = (this.LabelText ?? Strings.ViewFilterLabel);
			writer.Write("<table cellspacing='0' cellpadding='0' width='80%' style='margin-top:2px;margin-bottom:1px'>");
			writer.Write("<tr>");
			writer.Write("<td width='20%'>");
			this.selectViewLabel.RenderControl(writer);
			writer.Write("</td>");
			writer.Write("<td width='80%'>");
			base.RenderBeginTag(writer);
		}

		// Token: 0x06004395 RID: 17301 RVA: 0x000CC6F6 File Offset: 0x000CA8F6
		public override void RenderEndTag(HtmlTextWriter writer)
		{
			base.RenderEndTag(writer);
			writer.Write("</td>");
			writer.Write("</tr>");
			writer.Write("</table>");
		}

		// Token: 0x06004396 RID: 17302 RVA: 0x000CC720 File Offset: 0x000CA920
		protected override void OnPreRender(EventArgs e)
		{
			base.OnPreRender(e);
			ScriptManager.GetCurrent(this.Page).RegisterScriptControl<FilterDropDown>(this);
			this.selectViewLabel.ID = this.ClientID + "_label";
			ScriptObjectBuilder.RegisterCssReferences(this);
		}

		// Token: 0x06004397 RID: 17303 RVA: 0x000CC75B File Offset: 0x000CA95B
		protected override void Render(HtmlTextWriter writer)
		{
			base.Render(writer);
			if (!base.DesignMode)
			{
				ScriptManager.GetCurrent(this.Page).RegisterScriptDescriptors(this);
			}
		}

		// Token: 0x04002DAF RID: 11695
		private Label selectViewLabel;
	}
}
