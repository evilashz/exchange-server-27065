using System;
using System.ComponentModel;
using System.Web.UI;
using System.Web.UI.WebControls;
using AjaxControlToolkit;

namespace Microsoft.Exchange.Management.ControlPanel.WebControls
{
	// Token: 0x020005F6 RID: 1526
	[ClientScriptResource("InlineEditor", "Microsoft.Exchange.Management.ControlPanel.Client.Rules.js")]
	[ControlValueProperty("Value")]
	[ToolboxData("<{0}:InlineEditor runat=server></{0}:InlineEditor>")]
	public class InlineEditor : ScriptControlBase, INamingContainer
	{
		// Token: 0x0600446D RID: 17517 RVA: 0x000CEC5C File Offset: 0x000CCE5C
		public InlineEditor()
		{
			this.listview = new ListView();
			this.listSource = new ListSource();
			this.MaxLength = 128;
			this.listSource.ID = "DataSource";
			this.listview.IdentityProperty = null;
			this.listview.NameProperty = null;
			this.listview.WarningAsError = true;
			this.ValidationExpression = string.Empty;
			this.CssClass = "InlineEditor";
			this.ListViewCssClass = "InlineEditorListView";
		}

		// Token: 0x0600446E RID: 17518 RVA: 0x000CECE8 File Offset: 0x000CCEE8
		protected override void BuildScriptDescriptor(ScriptComponentDescriptor descriptor)
		{
			base.BuildScriptDescriptor(descriptor);
			descriptor.AddComponentProperty("ListView", this.ListViewID, this);
			descriptor.AddComponentProperty("DataSource", this.ListSourceID, this);
			if (!string.IsNullOrEmpty(this.ValidationExpression))
			{
				descriptor.AddScriptProperty("ValidationExpression", "function($_) { return /" + this.ValidationExpression + "/.test($_) }");
				descriptor.AddProperty("ValidationErrorMessage", this.ValidationErrorMessage);
			}
			if (this.DuplicateHandlingType != DuplicateHandlingType.AllowDuplicate)
			{
				descriptor.AddProperty("DuplicateHandlingType", this.DuplicateHandlingType);
			}
			if (this.ReadOnly)
			{
				descriptor.AddProperty("ReadOnly", true);
			}
		}

		// Token: 0x17002670 RID: 9840
		// (get) Token: 0x0600446F RID: 17519 RVA: 0x000CED96 File Offset: 0x000CCF96
		protected override HtmlTextWriterTag TagKey
		{
			get
			{
				return HtmlTextWriterTag.Div;
			}
		}

		// Token: 0x06004470 RID: 17520 RVA: 0x000CED9C File Offset: 0x000CCF9C
		protected override void CreateChildControls()
		{
			base.CreateChildControls();
			this.Controls.Add(this.listSource);
			this.listview.ID = "listview";
			this.listview.Height = Unit.Percentage(100.0);
			this.listview.AllowSorting = false;
			this.listview.SortProperty = null;
			this.listview.ShowSearchBar = false;
			this.listview.ShowHeader = false;
			this.listview.ShowTitle = false;
			this.listview.ShowStatus = false;
			this.listview.DataSourceID = this.listSource.ID;
			this.listview.IsEditable = true;
			this.listview.EmptyDataText = null;
			this.listview.Columns.Add(new ColumnHeader
			{
				Width = Unit.Percentage(100.0)
			});
			this.editCommand = new Command(string.Empty, CommandSprite.SpriteId.ToolBarProperties);
			this.editCommand.ImageAltText = Strings.EditCommandText;
			this.editCommand.SelectionMode = SelectionMode.RequiresSingleSelectionDisabledOnInlineEdit;
			this.editCommand.OnClientClick = "$find('" + this.ClientID + "').editCommand();";
			this.editCommand.DefaultCommand = true;
			this.listview.Commands.Add(this.editCommand);
			this.removeCommand = new RemoveCommand(false);
			this.listview.Commands.Add(this.removeCommand);
			this.Controls.Add(this.listview);
		}

		// Token: 0x17002671 RID: 9841
		// (get) Token: 0x06004471 RID: 17521 RVA: 0x000CEF32 File Offset: 0x000CD132
		public string ListViewID
		{
			get
			{
				return this.listview.ClientID;
			}
		}

		// Token: 0x17002672 RID: 9842
		// (get) Token: 0x06004472 RID: 17522 RVA: 0x000CEF3F File Offset: 0x000CD13F
		public string ListSourceID
		{
			get
			{
				return this.listSource.ClientID;
			}
		}

		// Token: 0x17002673 RID: 9843
		// (get) Token: 0x06004473 RID: 17523 RVA: 0x000CEF4C File Offset: 0x000CD14C
		// (set) Token: 0x06004474 RID: 17524 RVA: 0x000CEF59 File Offset: 0x000CD159
		[DefaultValue(128)]
		public int MaxLength
		{
			get
			{
				return this.listview.InlineEditMaxLength;
			}
			set
			{
				this.listview.InlineEditMaxLength = value;
			}
		}

		// Token: 0x17002674 RID: 9844
		// (get) Token: 0x06004475 RID: 17525 RVA: 0x000CEF67 File Offset: 0x000CD167
		// (set) Token: 0x06004476 RID: 17526 RVA: 0x000CEF74 File Offset: 0x000CD174
		public string InputWaterMarkText
		{
			get
			{
				return this.listview.InputWaterMarkText;
			}
			set
			{
				this.listview.InputWaterMarkText = value;
			}
		}

		// Token: 0x17002675 RID: 9845
		// (get) Token: 0x06004477 RID: 17527 RVA: 0x000CEF82 File Offset: 0x000CD182
		// (set) Token: 0x06004478 RID: 17528 RVA: 0x000CEF8F File Offset: 0x000CD18F
		[DefaultValue("InlineEditorListView")]
		public string ListViewCssClass
		{
			get
			{
				return this.listview.CssClass;
			}
			set
			{
				this.listview.CssClass = value + " " + this.ListViewCssClass;
			}
		}

		// Token: 0x17002676 RID: 9846
		// (get) Token: 0x06004479 RID: 17529 RVA: 0x000CEFAD File Offset: 0x000CD1AD
		// (set) Token: 0x0600447A RID: 17530 RVA: 0x000CEFB5 File Offset: 0x000CD1B5
		public DuplicateHandlingType DuplicateHandlingType { get; set; }

		// Token: 0x17002677 RID: 9847
		// (get) Token: 0x0600447B RID: 17531 RVA: 0x000CEFBE File Offset: 0x000CD1BE
		// (set) Token: 0x0600447C RID: 17532 RVA: 0x000CEFC6 File Offset: 0x000CD1C6
		public string ValidationExpression { get; set; }

		// Token: 0x17002678 RID: 9848
		// (get) Token: 0x0600447D RID: 17533 RVA: 0x000CEFCF File Offset: 0x000CD1CF
		// (set) Token: 0x0600447E RID: 17534 RVA: 0x000CEFD7 File Offset: 0x000CD1D7
		public string ValidationErrorMessage { get; set; }

		// Token: 0x17002679 RID: 9849
		// (get) Token: 0x0600447F RID: 17535 RVA: 0x000CEFE0 File Offset: 0x000CD1E0
		// (set) Token: 0x06004480 RID: 17536 RVA: 0x000CEFE8 File Offset: 0x000CD1E8
		[Bindable(true)]
		[DefaultValue(false)]
		public bool ReadOnly
		{
			get
			{
				return this.readOnly;
			}
			set
			{
				this.readOnly = value;
			}
		}

		// Token: 0x06004481 RID: 17537 RVA: 0x000CEFF1 File Offset: 0x000CD1F1
		protected override void AddAttributesToRender(HtmlTextWriter writer)
		{
			if (this.ReadOnly)
			{
				writer.AddAttribute(HtmlTextWriterAttribute.ReadOnly, "readonly");
			}
			base.AddAttributesToRender(writer);
		}

		// Token: 0x04002DEF RID: 11759
		private ListSource listSource;

		// Token: 0x04002DF0 RID: 11760
		private ListView listview;

		// Token: 0x04002DF1 RID: 11761
		private Command removeCommand;

		// Token: 0x04002DF2 RID: 11762
		private Command editCommand;

		// Token: 0x04002DF3 RID: 11763
		private bool readOnly;
	}
}
