using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using AjaxControlToolkit;

namespace Microsoft.Exchange.Management.ControlPanel.WebControls
{
	// Token: 0x02000569 RID: 1385
	[ToolboxData("<{0}:ADAttributePicker runat=server></{0}:ADAttributePicker>")]
	[ClientScriptResource("ADAttributePicker", "Microsoft.Exchange.Management.ControlPanel.Client.Rules.js")]
	public class ADAttributePicker : ScriptControlBase
	{
		// Token: 0x06004075 RID: 16501 RVA: 0x000C4A86 File Offset: 0x000C2C86
		public ADAttributePicker() : base(HtmlTextWriterTag.Div)
		{
			this.ecpCollectionEditor = new NameValueCollectionEditor();
		}

		// Token: 0x170024FB RID: 9467
		// (get) Token: 0x06004076 RID: 16502 RVA: 0x000C4A9B File Offset: 0x000C2C9B
		public string EcpCollectionEditorID
		{
			get
			{
				this.EnsureChildControls();
				return this.ecpCollectionEditor.ClientID;
			}
		}

		// Token: 0x06004077 RID: 16503 RVA: 0x000C4AAE File Offset: 0x000C2CAE
		protected override void BuildScriptDescriptor(ScriptComponentDescriptor descriptor)
		{
			base.BuildScriptDescriptor(descriptor);
			descriptor.AddComponentProperty("ECPCollectionEditor", this.EcpCollectionEditorID, this);
		}

		// Token: 0x06004078 RID: 16504 RVA: 0x000C4ACC File Offset: 0x000C2CCC
		protected override void CreateChildControls()
		{
			base.CreateChildControls();
			this.ecpCollectionEditor.ID = "ecpCollectionEditor";
			this.ecpCollectionEditor.Height = Unit.Pixel(200);
			this.ecpCollectionEditor.Width = Unit.Pixel(300);
			this.ecpCollectionEditor.PickerFormUrl = "~/RulesEditor/ADAttribute.aspx";
			this.ecpCollectionEditor.EditCommandUrl = "~/RulesEditor/ADAttribute.aspx";
			this.ecpCollectionEditor.ValueProperty = "Identity";
			ColumnHeader columnHeader = new ColumnHeader();
			columnHeader.Name = "Name";
			columnHeader.Text = Strings.ADAttributeNameColumnName;
			columnHeader.Width = Unit.Percentage(50.0);
			ColumnHeader columnHeader2 = new ColumnHeader();
			columnHeader2.Name = "Value";
			columnHeader2.Text = Strings.ADAttributeValueColumnName;
			columnHeader2.Width = Unit.Percentage(50.0);
			this.ecpCollectionEditor.Columns.Add(columnHeader);
			this.ecpCollectionEditor.Columns.Add(columnHeader2);
			this.ecpCollectionEditor.DialogHeight = 210;
			this.ecpCollectionEditor.DialogWidth = 585;
			this.Controls.Add(this.ecpCollectionEditor);
		}

		// Token: 0x04002AE2 RID: 10978
		private NameValueCollectionEditor ecpCollectionEditor;
	}
}
