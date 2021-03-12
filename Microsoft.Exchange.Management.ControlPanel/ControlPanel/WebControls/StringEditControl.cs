using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using AjaxControlToolkit;

namespace Microsoft.Exchange.Management.ControlPanel.WebControls
{
	// Token: 0x0200061F RID: 1567
	[ClientScriptResource("StringEditControl", "Microsoft.Exchange.Management.ControlPanel.Client.Rules.js")]
	[ToolboxData("<{0}:StringEditControl runat=server></{0}:StringEditControl>")]
	public class StringEditControl : ScriptControlBase
	{
		// Token: 0x06004592 RID: 17810 RVA: 0x000D2435 File Offset: 0x000D0635
		public StringEditControl() : base(HtmlTextWriterTag.Div)
		{
			this.CssClass = "SingleStringEditor";
		}

		// Token: 0x170026D3 RID: 9939
		// (get) Token: 0x06004593 RID: 17811 RVA: 0x000D244A File Offset: 0x000D064A
		public string TextBoxID
		{
			get
			{
				this.EnsureChildControls();
				return this.textBox.ClientID;
			}
		}

		// Token: 0x06004594 RID: 17812 RVA: 0x000D245D File Offset: 0x000D065D
		protected override void BuildScriptDescriptor(ScriptComponentDescriptor descriptor)
		{
			base.BuildScriptDescriptor(descriptor);
			descriptor.AddElementProperty("TextBox", this.TextBoxID, this);
		}

		// Token: 0x06004595 RID: 17813 RVA: 0x000D2478 File Offset: 0x000D0678
		protected override void CreateChildControls()
		{
			base.CreateChildControls();
			this.textBox = new TextBox();
			this.textBox.ID = "txtBox";
			this.Controls.Add(this.textBox);
		}

		// Token: 0x04002EA5 RID: 11941
		protected TextBox textBox;
	}
}
