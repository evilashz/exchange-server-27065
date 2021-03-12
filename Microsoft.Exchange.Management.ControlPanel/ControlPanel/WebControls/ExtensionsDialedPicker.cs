using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using AjaxControlToolkit;

namespace Microsoft.Exchange.Management.ControlPanel.WebControls
{
	// Token: 0x0200068F RID: 1679
	[ClientScriptResource("ExtensionsDialedPicker", "Microsoft.Exchange.Management.ControlPanel.Client.Rules.js")]
	[ControlValueProperty("Value")]
	[ToolboxData("<{0}:ExtensionsDialedPicker runat=server></{0}:ExtensionsDialedPicker>")]
	public class ExtensionsDialedPicker : ScriptControlBase, INamingContainer
	{
		// Token: 0x0600485B RID: 18523 RVA: 0x000DC84E File Offset: 0x000DAA4E
		public ExtensionsDialedPicker() : base(HtmlTextWriterTag.Div)
		{
			this.CssClass = "pickerContainer";
			this.ServiceUrl = new WebServiceReference(EcpUrl.EcpVDir + "RulesEditor/UserExtensions.svc");
		}

		// Token: 0x170027B7 RID: 10167
		// (get) Token: 0x0600485C RID: 18524 RVA: 0x000DC87D File Offset: 0x000DAA7D
		// (set) Token: 0x0600485D RID: 18525 RVA: 0x000DC885 File Offset: 0x000DAA85
		public WebServiceReference ServiceUrl { get; set; }

		// Token: 0x0600485E RID: 18526 RVA: 0x000DC88E File Offset: 0x000DAA8E
		protected override void BuildScriptDescriptor(ScriptComponentDescriptor descriptor)
		{
			this.EnsureChildControls();
			base.BuildScriptDescriptor(descriptor);
			descriptor.AddComponentProperty("ExtensionsListView", this.listView.ClientID, this);
		}

		// Token: 0x0600485F RID: 18527 RVA: 0x000DC8B4 File Offset: 0x000DAAB4
		protected override void CreateChildControls()
		{
			base.CreateChildControls();
			this.contentPanel = new Panel();
			this.contentPanel.ID = "pickerContentPanel";
			this.contentPanel.CssClass = "contentPanel";
			ColumnHeader columnHeader = new ColumnHeader();
			columnHeader.Name = "DisplayName";
			columnHeader.Width = 100;
			this.listView = new ListView
			{
				ID = "pickerListView"
			};
			this.listView.ShowSearchBar = false;
			this.listView.ShowTitle = false;
			this.listView.ShowHeader = false;
			this.listView.ShowStatus = false;
			this.listView.MultiSelect = true;
			this.listView.Height = Unit.Pixel(150);
			this.listView.Width = Unit.Pixel(200);
			this.listView.CssClass = "pickerListView";
			this.listView.ServiceUrl = this.ServiceUrl;
			this.listView.Columns.Add(columnHeader);
			this.contentPanel.Controls.Add(this.listView);
			this.Controls.Add(this.contentPanel);
		}

		// Token: 0x04003091 RID: 12433
		private ListView listView;

		// Token: 0x04003092 RID: 12434
		private Panel contentPanel;
	}
}
