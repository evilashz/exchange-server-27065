using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using AjaxControlToolkit;

namespace Microsoft.Exchange.Management.ControlPanel.WebControls
{
	// Token: 0x020005A9 RID: 1449
	[ClientScriptResource("DLPPicker", "Microsoft.Exchange.Management.ControlPanel.Client.Rules.js")]
	[ToolboxData("<{0}:DLPPicker runat=server></{0}:DLPPicker>")]
	public class DLPPicker : ScriptControlBase
	{
		// Token: 0x0600426A RID: 17002 RVA: 0x000CA13B File Offset: 0x000C833B
		public DLPPicker() : base(HtmlTextWriterTag.Div)
		{
			this.ecpCollectionEditor = new DLPCollectionEditor();
		}

		// Token: 0x170025C8 RID: 9672
		// (get) Token: 0x0600426B RID: 17003 RVA: 0x000CA150 File Offset: 0x000C8350
		public string EcpCollectionEditorID
		{
			get
			{
				this.EnsureChildControls();
				return this.ecpCollectionEditor.ClientID;
			}
		}

		// Token: 0x0600426C RID: 17004 RVA: 0x000CA163 File Offset: 0x000C8363
		protected override void BuildScriptDescriptor(ScriptComponentDescriptor descriptor)
		{
			base.BuildScriptDescriptor(descriptor);
			descriptor.AddComponentProperty("ECPCollectionEditor", this.EcpCollectionEditorID, this);
		}

		// Token: 0x0600426D RID: 17005 RVA: 0x000CA188 File Offset: 0x000C8388
		protected override void CreateChildControls()
		{
			base.CreateChildControls();
			this.ecpCollectionEditor.ID = "dlpCollectionEditor";
			this.ecpCollectionEditor.EmptyDataText = Strings.DLPPickerEmptyText;
			this.ecpCollectionEditor.ShowListviewHeader = true;
			this.ecpCollectionEditor.Height = Unit.Pixel(400);
			this.ecpCollectionEditor.Width = Unit.Pixel(650);
			this.ecpCollectionEditor.PickerFormUrl = "~/DLPPolicy/SensitiveInformationPicker.aspx";
			this.ecpCollectionEditor.EditCommandUrl = "~/RulesEditor/EditDLPAttributes.aspx";
			this.ecpCollectionEditor.ValueProperty = "name";
			ColumnHeader columnHeader = new ColumnHeader();
			columnHeader.Name = "displayName";
			columnHeader.Text = Strings.DLPPickerName;
			columnHeader.Width = Unit.Percentage(20.0);
			ColumnHeader columnHeader2 = new ColumnHeader();
			columnHeader2.Name = "minCount";
			columnHeader2.Text = Strings.DLPPickerMinCount;
			columnHeader2.Width = Unit.Percentage(20.0);
			columnHeader2.EmptyText = Strings.DLPAny;
			ColumnHeader columnHeader3 = new ColumnHeader();
			columnHeader3.Name = "maxCount";
			columnHeader3.Text = Strings.DLPPickerMaxCount;
			columnHeader3.Width = Unit.Percentage(20.0);
			columnHeader3.EmptyText = Strings.DLPAny;
			ColumnHeader columnHeader4 = new ColumnHeader();
			columnHeader4.Name = "minConfidence";
			columnHeader4.Text = Strings.DLPPickerMinConfidence;
			columnHeader4.Width = Unit.Percentage(20.0);
			columnHeader4.EmptyText = Strings.DLPDefault;
			ColumnHeader columnHeader5 = new ColumnHeader();
			columnHeader5.Name = "maxConfidence";
			columnHeader5.Text = Strings.DLPPickerMaxConfidence;
			columnHeader5.Width = Unit.Percentage(20.0);
			columnHeader5.EmptyText = Strings.DLPDefault;
			this.ecpCollectionEditor.Columns.Add(columnHeader);
			this.ecpCollectionEditor.Columns.Add(columnHeader2);
			this.ecpCollectionEditor.Columns.Add(columnHeader3);
			this.ecpCollectionEditor.Columns.Add(columnHeader4);
			this.ecpCollectionEditor.Columns.Add(columnHeader5);
			this.ecpCollectionEditor.Columns.ForEach(delegate(ColumnHeader f)
			{
				f.DefaultOff = false;
			});
			this.ecpCollectionEditor.AddCommandDialogHeight = 450;
			this.ecpCollectionEditor.AddCommandDialogWidth = 585;
			this.ecpCollectionEditor.EditCommandDialogHeight = 400;
			this.ecpCollectionEditor.EditCommandDialogWidth = 575;
			this.ecpCollectionEditor.UseModalessForAdd = true;
			this.Controls.Add(this.ecpCollectionEditor);
		}

		// Token: 0x04002BA3 RID: 11171
		public const string ID_KEY = "name";

		// Token: 0x04002BA4 RID: 11172
		public const string DISPLAYNAME_KEY = "displayName";

		// Token: 0x04002BA5 RID: 11173
		public const string MINCOUNT_KEY = "minCount";

		// Token: 0x04002BA6 RID: 11174
		public const string MAXCOUNT_KEY = "maxCount";

		// Token: 0x04002BA7 RID: 11175
		public const string MINCONFIDENCE_KEY = "minConfidence";

		// Token: 0x04002BA8 RID: 11176
		public const string MAXCONFIDENCE_KEY = "maxConfidence";

		// Token: 0x04002BA9 RID: 11177
		private DLPCollectionEditor ecpCollectionEditor;
	}
}
