using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using AjaxControlToolkit;

namespace Microsoft.Exchange.Management.ControlPanel.WebControls
{
	// Token: 0x0200067D RID: 1661
	[ToolboxData("<{0}:WaterMarkedTextBox runat=server></{0}:WaterMarkedTextBox>")]
	[RequiredScript(typeof(ExtenderControlBase))]
	[ClientScriptResource("WaterMarkedTextBox", "Microsoft.Exchange.Management.ControlPanel.Client.Common.js")]
	[ControlValueProperty("Value")]
	public class WaterMarkedTextBox : TextBox, IScriptControl
	{
		// Token: 0x060047E2 RID: 18402 RVA: 0x000DABD8 File Offset: 0x000D8DD8
		public WaterMarkedTextBox()
		{
			this.watermarkExtender = new TextBoxWatermarkExtender();
			this.WatermarkExtender.ID = "watermarkBehavior" + Guid.NewGuid();
			this.Controls.Add(this.WatermarkExtender);
		}

		// Token: 0x060047E3 RID: 18403 RVA: 0x000DAC26 File Offset: 0x000D8E26
		protected override void OnPreRender(EventArgs e)
		{
			ScriptManager.GetCurrent(this.Page).RegisterScriptControl<WaterMarkedTextBox>(this);
			this.WatermarkExtender.TargetControlID = this.ID;
			base.OnPreRender(e);
		}

		// Token: 0x060047E4 RID: 18404 RVA: 0x000DAC51 File Offset: 0x000D8E51
		protected override void Render(HtmlTextWriter writer)
		{
			ScriptManager.GetCurrent(this.Page).RegisterScriptDescriptors(this);
			this.RenderBeginTag(writer);
			if (this.TextMode == TextBoxMode.MultiLine)
			{
				HttpUtility.HtmlEncode(this.Text, writer);
			}
			this.RenderChildren(writer);
			this.RenderEndTag(writer);
		}

		// Token: 0x17002792 RID: 10130
		// (get) Token: 0x060047E5 RID: 18405 RVA: 0x000DAC8E File Offset: 0x000D8E8E
		// (set) Token: 0x060047E6 RID: 18406 RVA: 0x000DAC9B File Offset: 0x000D8E9B
		public string WatermarkText
		{
			get
			{
				return this.WatermarkExtender.WatermarkText;
			}
			set
			{
				this.WatermarkExtender.WatermarkText = value;
			}
		}

		// Token: 0x17002793 RID: 10131
		// (get) Token: 0x060047E7 RID: 18407 RVA: 0x000DACA9 File Offset: 0x000D8EA9
		// (set) Token: 0x060047E8 RID: 18408 RVA: 0x000DACB6 File Offset: 0x000D8EB6
		public string WaterMarkCssClass
		{
			get
			{
				return this.WatermarkExtender.WatermarkCssClass;
			}
			set
			{
				this.WatermarkExtender.WatermarkCssClass = value;
			}
		}

		// Token: 0x17002794 RID: 10132
		// (get) Token: 0x060047E9 RID: 18409 RVA: 0x000DACC4 File Offset: 0x000D8EC4
		private TextBoxWatermarkExtender WatermarkExtender
		{
			get
			{
				return this.watermarkExtender;
			}
		}

		// Token: 0x060047EA RID: 18410 RVA: 0x000DACCC File Offset: 0x000D8ECC
		IEnumerable<ScriptDescriptor> IScriptControl.GetScriptDescriptors()
		{
			ScriptControlDescriptor scriptControlDescriptor = new ScriptControlDescriptor("WaterMarkedTextBox", this.ClientID);
			scriptControlDescriptor.AddComponentProperty("WatermarkBehavior", this.WatermarkExtender.ClientID);
			return new ScriptDescriptor[]
			{
				scriptControlDescriptor
			};
		}

		// Token: 0x060047EB RID: 18411 RVA: 0x000DAD0C File Offset: 0x000D8F0C
		IEnumerable<ScriptReference> IScriptControl.GetScriptReferences()
		{
			IEnumerable<ScriptReference> enumerable = new List<ScriptReference>();
			return ScriptObjectBuilder.GetScriptReferences(typeof(WaterMarkedTextBox));
		}

		// Token: 0x04003044 RID: 12356
		private TextBoxWatermarkExtender watermarkExtender;
	}
}
