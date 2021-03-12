using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using AjaxControlToolkit;

namespace Microsoft.Exchange.Management.ControlPanel.WebControls
{
	// Token: 0x0200067C RID: 1660
	[ClientScriptResource("WaterMarkedTextArea", "Microsoft.Exchange.Management.ControlPanel.Client.WizardProperties.js")]
	[ToolboxData("<{0}:WaterMarkedTextArea runat=server></{0}:WaterMarkedTextArea>")]
	[ControlValueProperty("Value")]
	[RequiredScript(typeof(ExtenderControlBase))]
	public class WaterMarkedTextArea : TextArea, IScriptControl
	{
		// Token: 0x060047D8 RID: 18392 RVA: 0x000DAA9A File Offset: 0x000D8C9A
		public WaterMarkedTextArea()
		{
			this.watermarkExtender = new TextBoxWatermarkExtender();
			this.WatermarkExtender.ID = "watermarkBehavior";
			this.Controls.Add(this.WatermarkExtender);
		}

		// Token: 0x060047D9 RID: 18393 RVA: 0x000DAACE File Offset: 0x000D8CCE
		protected override void OnPreRender(EventArgs e)
		{
			ScriptManager.GetCurrent(this.Page).RegisterScriptControl<WaterMarkedTextArea>(this);
			this.WatermarkExtender.TargetControlID = this.ID;
			base.OnPreRender(e);
		}

		// Token: 0x060047DA RID: 18394 RVA: 0x000DAAF9 File Offset: 0x000D8CF9
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

		// Token: 0x1700278F RID: 10127
		// (get) Token: 0x060047DB RID: 18395 RVA: 0x000DAB36 File Offset: 0x000D8D36
		// (set) Token: 0x060047DC RID: 18396 RVA: 0x000DAB43 File Offset: 0x000D8D43
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

		// Token: 0x17002790 RID: 10128
		// (get) Token: 0x060047DD RID: 18397 RVA: 0x000DAB51 File Offset: 0x000D8D51
		// (set) Token: 0x060047DE RID: 18398 RVA: 0x000DAB5E File Offset: 0x000D8D5E
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

		// Token: 0x17002791 RID: 10129
		// (get) Token: 0x060047DF RID: 18399 RVA: 0x000DAB6C File Offset: 0x000D8D6C
		private TextBoxWatermarkExtender WatermarkExtender
		{
			get
			{
				return this.watermarkExtender;
			}
		}

		// Token: 0x060047E0 RID: 18400 RVA: 0x000DAB74 File Offset: 0x000D8D74
		IEnumerable<ScriptDescriptor> IScriptControl.GetScriptDescriptors()
		{
			ScriptControlDescriptor scriptControlDescriptor = new ScriptControlDescriptor("WaterMarkedTextArea", this.ClientID);
			scriptControlDescriptor.AddComponentProperty("WatermarkBehavior", this.WatermarkExtender.ClientID);
			return new ScriptDescriptor[]
			{
				scriptControlDescriptor
			};
		}

		// Token: 0x060047E1 RID: 18401 RVA: 0x000DABB4 File Offset: 0x000D8DB4
		IEnumerable<ScriptReference> IScriptControl.GetScriptReferences()
		{
			IEnumerable<ScriptReference> enumerable = new List<ScriptReference>();
			return ScriptObjectBuilder.GetScriptReferences(typeof(WaterMarkedTextArea));
		}

		// Token: 0x04003043 RID: 12355
		private TextBoxWatermarkExtender watermarkExtender;
	}
}
