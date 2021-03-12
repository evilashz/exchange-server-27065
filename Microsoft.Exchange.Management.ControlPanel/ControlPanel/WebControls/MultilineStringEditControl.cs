using System;
using System.Web.UI;
using AjaxControlToolkit;

namespace Microsoft.Exchange.Management.ControlPanel.WebControls
{
	// Token: 0x02000619 RID: 1561
	[ClientScriptResource("MultilineStringEditControl", "Microsoft.Exchange.Management.ControlPanel.Client.Rules.js")]
	[ToolboxData("<{0}:MultilineStringEditControl runat=server></{0}:MultilineStringEditControl>")]
	public class MultilineStringEditControl : ScriptControlBase
	{
		// Token: 0x0600456F RID: 17775 RVA: 0x000D1D38 File Offset: 0x000CFF38
		public MultilineStringEditControl() : base(HtmlTextWriterTag.Div)
		{
			this.CssClass = "SingleStringEditor";
		}

		// Token: 0x170026C6 RID: 9926
		// (get) Token: 0x06004570 RID: 17776 RVA: 0x000D1D4D File Offset: 0x000CFF4D
		public string TextAreaID
		{
			get
			{
				this.EnsureChildControls();
				return this.textArea.ClientID;
			}
		}

		// Token: 0x06004571 RID: 17777 RVA: 0x000D1D60 File Offset: 0x000CFF60
		protected override void BuildScriptDescriptor(ScriptComponentDescriptor descriptor)
		{
			base.BuildScriptDescriptor(descriptor);
			descriptor.AddElementProperty("TextArea", this.TextAreaID, this);
		}

		// Token: 0x06004572 RID: 17778 RVA: 0x000D1D7C File Offset: 0x000CFF7C
		protected override void CreateChildControls()
		{
			base.CreateChildControls();
			this.textArea = new TextArea();
			this.textArea.ID = "txtArea";
			this.textArea.Rows = 8;
			this.textArea.MaxLength = 1;
			this.Controls.Add(this.textArea);
		}

		// Token: 0x04002E8A RID: 11914
		private TextArea textArea;
	}
}
