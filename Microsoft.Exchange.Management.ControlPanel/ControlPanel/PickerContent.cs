using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using AjaxControlToolkit;
using Microsoft.Exchange.Management.ControlPanel.WebControls;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x02000338 RID: 824
	[ControlValueProperty("Values")]
	[ParseChildren(true)]
	[PersistChildren(true)]
	[ToolboxData("<{0}:PickerContent runat=server></{0}:PickerContent>")]
	[ClientScriptResource("PickerContent", "Microsoft.Exchange.Management.ControlPanel.Client.Pickers.js")]
	public class PickerContent : ScriptControlBase
	{
		// Token: 0x06002EFD RID: 12029 RVA: 0x0008F4C8 File Offset: 0x0008D6C8
		public PickerContent()
		{
			this.CssClass = "pickerContainer";
			this.listView = new Microsoft.Exchange.Management.ControlPanel.WebControls.ListView();
			this.listView.ID = "pickerListView";
			this.listView.ShowHeader = true;
			this.selectionPanel = new Panel();
			this.selectionPanel.ID = "selectionPanel";
			this.NameProperty = "DisplayName";
		}

		// Token: 0x17001ECA RID: 7882
		// (get) Token: 0x06002EFE RID: 12030 RVA: 0x0008F545 File Offset: 0x0008D745
		[PersistenceMode(PersistenceMode.InnerDefaultProperty)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		public BindingCollection FilterParameters
		{
			get
			{
				return this.filterParameters;
			}
		}

		// Token: 0x17001ECB RID: 7883
		// (get) Token: 0x06002EFF RID: 12031 RVA: 0x0008F550 File Offset: 0x0008D750
		private bool AllowTyping
		{
			get
			{
				if (this.allowTyping == null)
				{
					this.allowTyping = new bool?(HttpContext.Current.Request.QueryString["allowtyping"] == "t");
				}
				return this.allowTyping.Value;
			}
		}

		// Token: 0x06002F00 RID: 12032 RVA: 0x0008F5A3 File Offset: 0x0008D7A3
		protected virtual void AddDetails()
		{
			this.contentPanel.Controls.Add(this.listView);
		}

		// Token: 0x17001ECC RID: 7884
		// (get) Token: 0x06002F01 RID: 12033 RVA: 0x0008F5BB File Offset: 0x0008D7BB
		protected Panel ContentPanel
		{
			get
			{
				return this.contentPanel;
			}
		}

		// Token: 0x17001ECD RID: 7885
		// (get) Token: 0x06002F02 RID: 12034 RVA: 0x0008F5C3 File Offset: 0x0008D7C3
		// (set) Token: 0x06002F03 RID: 12035 RVA: 0x0008F5CB File Offset: 0x0008D7CB
		protected bool IsMasterDetailed
		{
			get
			{
				return this.isMasterDetailed;
			}
			set
			{
				this.isMasterDetailed = value;
			}
		}

		// Token: 0x17001ECE RID: 7886
		// (get) Token: 0x06002F04 RID: 12036 RVA: 0x0008F5D4 File Offset: 0x0008D7D4
		public Microsoft.Exchange.Management.ControlPanel.WebControls.ListView ListView
		{
			get
			{
				return this.listView;
			}
		}

		// Token: 0x06002F05 RID: 12037 RVA: 0x0008F5DC File Offset: 0x0008D7DC
		protected override void CreateChildControls()
		{
			base.CreateChildControls();
			this.CreateListSource();
			this.searchPanel = this.CreateSearchPanel();
			this.contentPanel = new Panel();
			this.contentPanel.ID = "pickerContentPanel";
			this.contentPanel.CssClass = "contentPanel";
			this.listView.ShowTitle = false;
			this.listView.DataSourceID = this.listSource.ID;
			this.listView.CssClass = "pickerListView";
			this.AddDetails();
			this.bottomPanel = new Panel();
			this.bottomPanel.ID = "bottom";
			this.bottomPanel.CssClass = "bottom";
			this.detailsPanel = new Panel();
			this.detailsPanel.ID = "detailsPanel";
			this.detailsPanel.CssClass = "detailsPanel";
			this.detailsCaption = new Literal();
			this.detailsCaption.ID = "detailsCaption";
			this.detailsCaption.Text = Strings.PickerFormDetailsText;
			this.detailsTextBox = new TextBox();
			this.detailsTextBox.ID = "detailsTextBox";
			this.detailsTextBox.ReadOnly = true;
			this.detailsTextBox.TextMode = TextBoxMode.MultiLine;
			this.detailsTextBox.Rows = 3;
			this.detailsTextBox.CssClass = "detailsTextBox";
			this.detailsPanel.Controls.Add(this.detailsCaption);
			this.detailsPanel.Controls.Add(this.detailsTextBox);
			this.selectionPanel.CssClass = "selectionPanel";
			this.btnAddItem = new HtmlButton();
			this.btnAddItem.ID = "btnAddItem";
			this.btnAddItem.CausesValidation = false;
			this.btnAddItem.Attributes["type"] = "button";
			this.btnAddItem.Attributes["onClick"] = "javascript:return false;";
			this.btnAddItem.Attributes.Add("class", "selectbutton");
			this.btnAddItem.InnerText = Strings.PickerFormItemsButtonText;
			this.wellControl = new WellControl();
			this.wellControl.ID = "wellControl";
			this.wellControl.DisplayProperty = ((this.listView.Columns.Count == 0) ? "DisplayName" : this.listView.NameProperty);
			this.wellControl.IdentityProperty = this.listView.IdentityProperty;
			this.wellControl.CssClass = "wellControl";
			Table table = new Table();
			table.Width = Unit.Percentage(100.0);
			table.CellSpacing = 0;
			table.CellPadding = 0;
			TableRow tableRow = new TableRow();
			TableCell tableCell = new TableCell();
			tableCell.CssClass = "selectButtonCell";
			tableCell.Controls.Add(this.btnAddItem);
			TableCell tableCell2 = new TableCell();
			tableCell2.CssClass = "wellContainerCell";
			Table table2 = new Table();
			table2.CssClass = "wellWrapperTable";
			table2.CellSpacing = 0;
			table2.CellPadding = 0;
			TableRow tableRow2 = new TableRow();
			TableCell tableCell3 = new TableCell();
			tableCell3.Controls.Add(this.wellControl);
			tableRow2.Cells.Add(tableCell3);
			table2.Rows.Add(tableRow2);
			tableCell2.Controls.Add(table2);
			tableRow.Cells.Add(tableCell);
			tableRow.Cells.Add(tableCell2);
			table.Rows.Add(tableRow);
			this.selectionPanel.Controls.Add(table);
			this.bottomPanel.Controls.Add(this.detailsPanel);
			this.bottomPanel.Controls.Add(this.selectionPanel);
			this.Controls.Add(this.searchPanel);
			this.Controls.Add(this.contentPanel);
			this.Controls.Add(this.bottomPanel);
		}

		// Token: 0x06002F06 RID: 12038 RVA: 0x0008F9D8 File Offset: 0x0008DBD8
		private void CreateListSource()
		{
			if (this.ServiceUrl != null)
			{
				this.webServiceListSource = new WebServiceListSource();
				this.webServiceListSource.ID = "webServiceListSource";
				this.webServiceListSource.ServiceUrl = this.ServiceUrl;
				this.webServiceListSource.SupportAsyncGetList = this.SupportAsyncGetList;
				this.listSource = this.webServiceListSource;
			}
			else
			{
				this.listSource = new ListSource();
				this.listSource.ID = "listSource";
			}
			this.Controls.Add(this.listSource);
		}

		// Token: 0x06002F07 RID: 12039 RVA: 0x0008FA64 File Offset: 0x0008DC64
		protected override void OnPreRender(EventArgs e)
		{
			if (!this.HideSearchPanel && this.webServiceListSource == null)
			{
				throw new ArgumentException("Please specify a ServiceUrl for PickerContent or set HideSearchPanel to true");
			}
			if (this.webServiceListSource == null && this.FilterParameters != null && this.FilterParameters.Count > 0)
			{
				throw new ArgumentException("Please specify a ServiceUrl for PickerContent otherwise FilterParameters are not supported");
			}
			base.OnPreRender(e);
			this.detailsPanel.Visible = !string.IsNullOrEmpty(this.DetailsProperty);
			this.searchPanel.Visible = !this.HideSearchPanel;
			this.selectionPanel.Visible = this.ShowWellControl;
			if (!this.HideSearchPanel)
			{
				ComponentBinding componentBinding = new ComponentBinding(this.filterTextBox, "text");
				componentBinding.Name = "SearchText";
				this.webServiceListSource.FilterParameters.Add(componentBinding);
			}
			if (this.HasCustomizedFilter)
			{
				ClientControlBinding clientControlBinding = new ComponentBinding(this, "CustomizedFilters");
				clientControlBinding.Name = "CustomizedFilters";
				this.webServiceListSource.FilterParameters.Add(clientControlBinding);
			}
			foreach (Binding binding in this.FilterParameters)
			{
				QueryStringBinding queryStringBinding = binding as QueryStringBinding;
				if (queryStringBinding == null || queryStringBinding.HasValue || !queryStringBinding.Optional)
				{
					this.webServiceListSource.FilterParameters.Add(binding);
				}
			}
		}

		// Token: 0x17001ECF RID: 7887
		// (get) Token: 0x06002F08 RID: 12040 RVA: 0x0008FBC8 File Offset: 0x0008DDC8
		protected override HtmlTextWriterTag TagKey
		{
			get
			{
				return HtmlTextWriterTag.Div;
			}
		}

		// Token: 0x06002F09 RID: 12041 RVA: 0x0008FBCC File Offset: 0x0008DDCC
		private Panel CreateSearchPanel()
		{
			Panel panel = new Panel();
			panel.ID = "searchPanel";
			panel.CssClass = "searchPanel";
			Table table = new Table();
			table.CellPadding = 0;
			table.CellSpacing = 0;
			table.Width = Unit.Percentage(100.0);
			TableRow tableRow = new TableRow();
			TableCell tableCell = new TableCell();
			tableCell.CssClass = "pickerFilterCell";
			this.filterTextBox = new FilterTextBox();
			this.filterTextBox.ID = "txtSearch";
			tableCell.Controls.Add(this.filterTextBox);
			tableRow.Cells.Add(tableCell);
			table.Rows.Add(tableRow);
			panel.Controls.Add(table);
			return panel;
		}

		// Token: 0x17001ED0 RID: 7888
		// (get) Token: 0x06002F0A RID: 12042 RVA: 0x0008FC87 File Offset: 0x0008DE87
		[MergableProperty(false)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[DefaultValue("")]
		public List<ColumnHeader> Columns
		{
			get
			{
				return this.listView.Columns;
			}
		}

		// Token: 0x17001ED1 RID: 7889
		// (get) Token: 0x06002F0B RID: 12043 RVA: 0x0008FC94 File Offset: 0x0008DE94
		// (set) Token: 0x06002F0C RID: 12044 RVA: 0x0008FC9C File Offset: 0x0008DE9C
		public PickerSelectionType SelectionMode
		{
			get
			{
				return this.selectionMode;
			}
			set
			{
				if (this.SelectionMode != value)
				{
					this.selectionMode = value;
					this.listView.ShowStatus = (this.SelectionMode == PickerSelectionType.Multiple || this.SupportAsyncGetList);
					this.listView.MultiSelect = (this.SelectionMode == PickerSelectionType.Multiple);
				}
			}
		}

		// Token: 0x17001ED2 RID: 7890
		// (get) Token: 0x06002F0D RID: 12045 RVA: 0x0008FCEA File Offset: 0x0008DEEA
		private bool ShowWellControl
		{
			get
			{
				return this.selectionMode == PickerSelectionType.Multiple || this.AllowTyping;
			}
		}

		// Token: 0x17001ED3 RID: 7891
		// (get) Token: 0x06002F0E RID: 12046 RVA: 0x0008FCFD File Offset: 0x0008DEFD
		// (set) Token: 0x06002F0F RID: 12047 RVA: 0x0008FD0A File Offset: 0x0008DF0A
		[DefaultValue("DisplayName")]
		public string NameProperty
		{
			get
			{
				return this.listView.NameProperty;
			}
			set
			{
				this.listView.NameProperty = value;
			}
		}

		// Token: 0x17001ED4 RID: 7892
		// (get) Token: 0x06002F10 RID: 12048 RVA: 0x0008FD18 File Offset: 0x0008DF18
		// (set) Token: 0x06002F11 RID: 12049 RVA: 0x0008FD25 File Offset: 0x0008DF25
		public string SortProperty
		{
			get
			{
				return this.listView.SortProperty;
			}
			set
			{
				this.listView.SortProperty = value;
			}
		}

		// Token: 0x17001ED5 RID: 7893
		// (get) Token: 0x06002F12 RID: 12050 RVA: 0x0008FD33 File Offset: 0x0008DF33
		// (set) Token: 0x06002F13 RID: 12051 RVA: 0x0008FD40 File Offset: 0x0008DF40
		public bool ClientSort
		{
			get
			{
				return this.listView.ClientSort;
			}
			set
			{
				this.listView.ClientSort = value;
			}
		}

		// Token: 0x17001ED6 RID: 7894
		// (get) Token: 0x06002F14 RID: 12052 RVA: 0x0008FD4E File Offset: 0x0008DF4E
		// (set) Token: 0x06002F15 RID: 12053 RVA: 0x0008FD5B File Offset: 0x0008DF5B
		[DefaultValue("false")]
		public bool ShowHeader
		{
			get
			{
				return this.listView.ShowHeader;
			}
			set
			{
				this.listView.ShowHeader = value;
			}
		}

		// Token: 0x17001ED7 RID: 7895
		// (get) Token: 0x06002F16 RID: 12054 RVA: 0x0008FD69 File Offset: 0x0008DF69
		// (set) Token: 0x06002F17 RID: 12055 RVA: 0x0008FD76 File Offset: 0x0008DF76
		[DefaultValue("Identity")]
		public string IdentityProperty
		{
			get
			{
				return this.listView.IdentityProperty;
			}
			set
			{
				this.listView.IdentityProperty = value;
			}
		}

		// Token: 0x17001ED8 RID: 7896
		// (get) Token: 0x06002F18 RID: 12056 RVA: 0x0008FD84 File Offset: 0x0008DF84
		// (set) Token: 0x06002F19 RID: 12057 RVA: 0x0008FD91 File Offset: 0x0008DF91
		[DefaultValue(null)]
		[Themeable(true)]
		public string SpriteProperty
		{
			get
			{
				return this.listView.SpriteProperty;
			}
			set
			{
				this.listView.SpriteProperty = value;
			}
		}

		// Token: 0x17001ED9 RID: 7897
		// (get) Token: 0x06002F1A RID: 12058 RVA: 0x0008FD9F File Offset: 0x0008DF9F
		// (set) Token: 0x06002F1B RID: 12059 RVA: 0x0008FDAC File Offset: 0x0008DFAC
		[DefaultValue(null)]
		[Themeable(true)]
		public string SpriteAltTextProperty
		{
			get
			{
				return this.listView.SpriteAltTextProperty;
			}
			set
			{
				this.listView.SpriteAltTextProperty = value;
			}
		}

		// Token: 0x17001EDA RID: 7898
		// (get) Token: 0x06002F1C RID: 12060 RVA: 0x0008FDBA File Offset: 0x0008DFBA
		// (set) Token: 0x06002F1D RID: 12061 RVA: 0x0008FDC2 File Offset: 0x0008DFC2
		public WebServiceReference ServiceUrl { get; set; }

		// Token: 0x17001EDB RID: 7899
		// (get) Token: 0x06002F1E RID: 12062 RVA: 0x0008FDCB File Offset: 0x0008DFCB
		// (set) Token: 0x06002F1F RID: 12063 RVA: 0x0008FDD8 File Offset: 0x0008DFD8
		public int SearchBarMaxLength
		{
			get
			{
				return this.filterTextBox.MaxLength;
			}
			set
			{
				this.filterTextBox.MaxLength = value;
			}
		}

		// Token: 0x17001EDC RID: 7900
		// (get) Token: 0x06002F20 RID: 12064 RVA: 0x0008FDE6 File Offset: 0x0008DFE6
		// (set) Token: 0x06002F21 RID: 12065 RVA: 0x0008FDEE File Offset: 0x0008DFEE
		[DefaultValue(false)]
		public bool HasCustomizedFilter { get; set; }

		// Token: 0x17001EDD RID: 7901
		// (get) Token: 0x06002F22 RID: 12066 RVA: 0x0008FDF7 File Offset: 0x0008DFF7
		// (set) Token: 0x06002F23 RID: 12067 RVA: 0x0008FDFF File Offset: 0x0008DFFF
		[Category("Appearance")]
		[Bindable(true)]
		[DefaultValue(false)]
		public bool HideSearchPanel { get; set; }

		// Token: 0x17001EDE RID: 7902
		// (get) Token: 0x06002F24 RID: 12068 RVA: 0x0008FE08 File Offset: 0x0008E008
		// (set) Token: 0x06002F25 RID: 12069 RVA: 0x0008FE10 File Offset: 0x0008E010
		[DefaultValue(false)]
		public bool ReturnIdentities { get; set; }

		// Token: 0x06002F26 RID: 12070 RVA: 0x0008FE1C File Offset: 0x0008E01C
		protected override void BuildScriptDescriptor(ScriptComponentDescriptor descriptor)
		{
			base.BuildScriptDescriptor(descriptor);
			BaseForm baseForm = this.Page as BaseForm;
			if (baseForm != null)
			{
				descriptor.AddComponentProperty("Form", "aspnetForm");
			}
			descriptor.AddComponentProperty("ListView", this.ListViewID, this);
			descriptor.AddElementProperty("DetailsTextBox", this.DetailsTextBoxID, this);
			descriptor.AddElementProperty("DetailsPanel", this.DetailsPanelID, this);
			descriptor.AddComponentProperty("ListSource", this.ListSourceID, this);
			descriptor.AddElementProperty("AddButton", this.AddButtonID, this);
			descriptor.AddElementProperty("WrapperControl", this.WrapperControlID, this);
			descriptor.AddElementProperty("SearchPanel", this.SearchPanelID, this);
			descriptor.AddElementProperty("ContentPanel", this.ContentPanelID, this);
			descriptor.AddElementProperty("BottomPanel", this.BottomPanelID, this);
			descriptor.AddProperty("IsMasterDetailed", this.IsMasterDetailed, true);
			descriptor.AddProperty("DetailsProperty", this.DetailsProperty, true);
			descriptor.AddProperty("ReturnIdentities", this.ReturnIdentities, true);
			descriptor.AddProperty("AllowTyping", this.AllowTyping, true);
			descriptor.AddProperty("SpriteSrc", Util.GetSpriteImageSrc(this));
			if (this.ShowWellControl)
			{
				descriptor.AddComponentProperty("WellControl", this.wellControl.ClientID);
			}
			if (this.filterTextBox != null && !this.HideSearchPanel)
			{
				descriptor.AddComponentProperty("FilterTextBox", this.filterTextBox.ClientID);
			}
		}

		// Token: 0x17001EDF RID: 7903
		// (get) Token: 0x06002F27 RID: 12071 RVA: 0x0008FF8E File Offset: 0x0008E18E
		public string ListViewID
		{
			get
			{
				return this.listView.ClientID;
			}
		}

		// Token: 0x17001EE0 RID: 7904
		// (get) Token: 0x06002F28 RID: 12072 RVA: 0x0008FF9B File Offset: 0x0008E19B
		public string DetailsTextBoxID
		{
			get
			{
				return this.detailsTextBox.ClientID;
			}
		}

		// Token: 0x17001EE1 RID: 7905
		// (get) Token: 0x06002F29 RID: 12073 RVA: 0x0008FFA8 File Offset: 0x0008E1A8
		public string DetailsPanelID
		{
			get
			{
				return this.detailsPanel.ClientID;
			}
		}

		// Token: 0x17001EE2 RID: 7906
		// (get) Token: 0x06002F2A RID: 12074 RVA: 0x0008FFB5 File Offset: 0x0008E1B5
		public string ListSourceID
		{
			get
			{
				return this.listSource.ClientID;
			}
		}

		// Token: 0x17001EE3 RID: 7907
		// (get) Token: 0x06002F2B RID: 12075 RVA: 0x0008FFC2 File Offset: 0x0008E1C2
		public string AddButtonID
		{
			get
			{
				return this.btnAddItem.ClientID;
			}
		}

		// Token: 0x17001EE4 RID: 7908
		// (get) Token: 0x06002F2C RID: 12076 RVA: 0x0008FFCF File Offset: 0x0008E1CF
		// (set) Token: 0x06002F2D RID: 12077 RVA: 0x0008FFD7 File Offset: 0x0008E1D7
		public string WrapperControlID { get; set; }

		// Token: 0x17001EE5 RID: 7909
		// (get) Token: 0x06002F2E RID: 12078 RVA: 0x0008FFE0 File Offset: 0x0008E1E0
		public string SearchPanelID
		{
			get
			{
				return this.searchPanel.ClientID;
			}
		}

		// Token: 0x17001EE6 RID: 7910
		// (get) Token: 0x06002F2F RID: 12079 RVA: 0x0008FFED File Offset: 0x0008E1ED
		public string ContentPanelID
		{
			get
			{
				return this.contentPanel.ClientID;
			}
		}

		// Token: 0x17001EE7 RID: 7911
		// (get) Token: 0x06002F30 RID: 12080 RVA: 0x0008FFFA File Offset: 0x0008E1FA
		public string BottomPanelID
		{
			get
			{
				return this.bottomPanel.ClientID;
			}
		}

		// Token: 0x17001EE8 RID: 7912
		// (get) Token: 0x06002F31 RID: 12081 RVA: 0x00090007 File Offset: 0x0008E207
		// (set) Token: 0x06002F32 RID: 12082 RVA: 0x0009000F File Offset: 0x0008E20F
		public string DetailsProperty { get; set; }

		// Token: 0x17001EE9 RID: 7913
		// (get) Token: 0x06002F33 RID: 12083 RVA: 0x00090018 File Offset: 0x0008E218
		// (set) Token: 0x06002F34 RID: 12084 RVA: 0x00090020 File Offset: 0x0008E220
		[DefaultValue(false)]
		public bool SupportAsyncGetList { get; set; }

		// Token: 0x040022E4 RID: 8932
		private ListSource listSource;

		// Token: 0x040022E5 RID: 8933
		private WebServiceListSource webServiceListSource;

		// Token: 0x040022E6 RID: 8934
		private Panel searchPanel;

		// Token: 0x040022E7 RID: 8935
		private FilterTextBox filterTextBox;

		// Token: 0x040022E8 RID: 8936
		private Panel contentPanel;

		// Token: 0x040022E9 RID: 8937
		private bool isMasterDetailed;

		// Token: 0x040022EA RID: 8938
		private Microsoft.Exchange.Management.ControlPanel.WebControls.ListView listView;

		// Token: 0x040022EB RID: 8939
		private Panel bottomPanel;

		// Token: 0x040022EC RID: 8940
		private Panel detailsPanel;

		// Token: 0x040022ED RID: 8941
		private Literal detailsCaption;

		// Token: 0x040022EE RID: 8942
		private TextBox detailsTextBox;

		// Token: 0x040022EF RID: 8943
		private Panel selectionPanel;

		// Token: 0x040022F0 RID: 8944
		private HtmlButton btnAddItem;

		// Token: 0x040022F1 RID: 8945
		private WellControl wellControl;

		// Token: 0x040022F2 RID: 8946
		private BindingCollection filterParameters = new BindingCollection();

		// Token: 0x040022F3 RID: 8947
		private bool? allowTyping;

		// Token: 0x040022F4 RID: 8948
		private PickerSelectionType selectionMode = PickerSelectionType.Multiple;
	}
}
