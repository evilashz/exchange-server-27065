using System;
using System.ComponentModel;
using System.Web.UI;
using System.Web.UI.WebControls;
using AjaxControlToolkit;

namespace Microsoft.Exchange.Management.ControlPanel.WebControls
{
	// Token: 0x020005C1 RID: 1473
	[ClientScriptResource("EcpTextBoxSelectBase", "Microsoft.Exchange.Management.ControlPanel.Client.WizardProperties.js")]
	[ToolboxData("<{0}:EcpTextBoxSelectBase runat=server></{0}:EcpTextBoxSelectBase>")]
	public abstract class EcpTextBoxSelectBase : PickerControl
	{
		// Token: 0x170025FF RID: 9727
		// (get) Token: 0x060042FC RID: 17148 RVA: 0x000CB53E File Offset: 0x000C973E
		// (set) Token: 0x060042FD RID: 17149 RVA: 0x000CB546 File Offset: 0x000C9746
		[DefaultValue("")]
		public string QueryDataBound { get; set; }

		// Token: 0x17002600 RID: 9728
		// (get) Token: 0x060042FE RID: 17150 RVA: 0x000CB54F File Offset: 0x000C974F
		// (set) Token: 0x060042FF RID: 17151 RVA: 0x000CB557 File Offset: 0x000C9757
		[DefaultValue("")]
		public string EmptyValueText { get; set; }

		// Token: 0x06004300 RID: 17152 RVA: 0x000CB560 File Offset: 0x000C9760
		protected override void CreateChildControls()
		{
			base.CreateChildControls();
			if (!string.IsNullOrEmpty(this.QueryDataBound))
			{
				this.queryBoundLabel = new Label();
				this.queryBoundLabel.ID = "queryBoundLabel";
				this.queryBoundLabel.Attributes.Add("DataBoundProperty", this.QueryDataBound);
				this.queryBoundLabel.CssClass = "hidden";
				this.Controls.Add(this.queryBoundLabel);
			}
		}

		// Token: 0x06004301 RID: 17153 RVA: 0x000CB5D8 File Offset: 0x000C97D8
		protected override void BuildScriptDescriptor(ScriptComponentDescriptor descriptor)
		{
			base.BuildScriptDescriptor(descriptor);
			if (!string.IsNullOrEmpty(this.QueryDataBound))
			{
				descriptor.AddElementProperty("QueryDataBound", this.queryBoundLabel.ClientID);
				descriptor.AddProperty("QueryDataBoundName", this.QueryDataBound);
			}
			descriptor.AddProperty("EmptyValueText", this.EmptyValueText, true);
		}

		// Token: 0x04002D8E RID: 11662
		private Label queryBoundLabel;
	}
}
