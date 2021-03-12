using System;
using System.Globalization;
using System.Web.UI;
using System.Web.UI.WebControls;
using AjaxControlToolkit;

namespace Microsoft.Exchange.Management.ControlPanel.WebControls
{
	// Token: 0x020005A1 RID: 1441
	[ClientScriptResource("SimpleProgressBar", "Microsoft.Exchange.Management.ControlPanel.Client.WizardProperties.js")]
	[ControlValueProperty("Value")]
	[ToolboxData("<{0}:SimpleProgressBar runat=server></{0}:ProgressBar>")]
	public class SimpleProgressBar : ScriptControlBase
	{
		// Token: 0x060041EE RID: 16878 RVA: 0x000C8BA8 File Offset: 0x000C6DA8
		public SimpleProgressBar() : base(HtmlTextWriterTag.Div)
		{
			this.CssClass = "ProgressBar";
		}

		// Token: 0x17002587 RID: 9607
		// (get) Token: 0x060041EF RID: 16879 RVA: 0x000C8BBD File Offset: 0x000C6DBD
		// (set) Token: 0x060041F0 RID: 16880 RVA: 0x000C8BC5 File Offset: 0x000C6DC5
		public int Complete
		{
			get
			{
				return this.complete;
			}
			set
			{
				if (value >= 0 && value <= 100)
				{
					this.complete = value;
					return;
				}
				throw new InvalidOperationException("Complete for the progress bar can only be 0 - 100.");
			}
		}

		// Token: 0x060041F1 RID: 16881 RVA: 0x000C8BE4 File Offset: 0x000C6DE4
		protected override void CreateChildControls()
		{
			base.CreateChildControls();
			Panel panel = new Panel();
			panel.ID = "pnlComplete";
			panel.CssClass = "ProgressComplete";
			panel.Attributes["Width"] = this.Complete + "%";
			panel.Attributes["Height"] = "100%";
			this.Controls.Add(panel);
		}

		// Token: 0x060041F2 RID: 16882 RVA: 0x000C8C5C File Offset: 0x000C6E5C
		protected override void BuildScriptDescriptor(ScriptComponentDescriptor descriptor)
		{
			base.BuildScriptDescriptor(descriptor);
			if (this.Complete > 0)
			{
				descriptor.AddProperty("Complete", this.Complete.ToString(CultureInfo.InvariantCulture));
			}
		}

		// Token: 0x04002B72 RID: 11122
		private int complete;
	}
}
