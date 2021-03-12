using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using AjaxControlToolkit;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;

namespace Microsoft.Exchange.Management.ControlPanel.WebControls
{
	// Token: 0x020005A6 RID: 1446
	[ToolboxData("<{0}:EcpCollectionEditor runat=server></{0}:EcpCollectionEditor>")]
	[ClientScriptResource("EcpCollectionEditor", "Microsoft.Exchange.Management.ControlPanel.Client.WizardProperties.js")]
	[ControlValueProperty("Values")]
	[DefaultProperty("Text")]
	public class EcpCollectionEditor : ScriptControlBase, INamingContainer
	{
		// Token: 0x06004218 RID: 16920 RVA: 0x000C95F4 File Offset: 0x000C77F4
		public EcpCollectionEditor()
		{
			this.listview = new ListView();
			this.showListviewHeader = true;
			this.listSource = new ListSource();
			this.listSource.ID = "DataSource";
			this.ListViewCssClass = "collectionEditorListView";
		}

		// Token: 0x06004219 RID: 16921 RVA: 0x000C9694 File Offset: 0x000C7894
		protected override void BuildScriptDescriptor(ScriptComponentDescriptor descriptor)
		{
			base.BuildScriptDescriptor(descriptor);
			if (this.ValueProperty != "Identity")
			{
				descriptor.AddProperty("ValueProperty", this.ValueProperty);
			}
			descriptor.AddUrlProperty("EditCommandUrl", this.EditCommandUrl, this);
			descriptor.AddUrlProperty("PickerFormUrl", this.PickerFormUrl, this);
			descriptor.AddProperty("SplitAddRemove", this.SplitAddRemove, true);
			descriptor.AddProperty("UseModalPopup", this.AlwaysOpenModalDialog || !string.IsNullOrEmpty(this.EditCommandUrl));
			descriptor.AddProperty("UseModalessForAdd", this.UseModalessForAdd);
			descriptor.AddUrlProperty("WholeListEditorUrl", this.WholeListEditorUrl, this);
			if (this.DialogHeight != 530)
			{
				descriptor.AddProperty("DialogHeight", this.DialogHeight);
			}
			if (this.DialogWidth != 450)
			{
				descriptor.AddProperty("DialogWidth", this.DialogWidth);
			}
			if (this.AddCommandDialogHeight != 0)
			{
				descriptor.AddProperty("AddCommandDialogHeight", this.AddCommandDialogHeight);
			}
			if (this.AddCommandDialogWidth != 0)
			{
				descriptor.AddProperty("AddCommandDialogWidth", this.AddCommandDialogWidth);
			}
			if (this.EditCommandDialogHeight != 0)
			{
				descriptor.AddProperty("EditCommandDialogHeight", this.EditCommandDialogHeight);
			}
			if (this.EditCommandDialogWidth != 0)
			{
				descriptor.AddProperty("EditCommandDialogWidth", this.EditCommandDialogWidth);
			}
			descriptor.AddComponentProperty("ListView", this.ListViewID, this);
			descriptor.AddComponentProperty("DataSource", this.ListSourceID, this);
			if (this.ReadOnly)
			{
				descriptor.AddProperty("ReadOnly", true);
			}
			descriptor.AddProperty("IsUsingOwaPeoplePicker", OwaPickerUtil.CanUseOwaPicker && this.IsUsingOwaPeoplePicker);
			descriptor.AddProperty("PickerCallerType", this.PickerCallerType);
			descriptor.AddProperty("IgnoreCaseWhenComparison", this.IgnoreCaseWhenComparison);
			descriptor.AddProperty("KeepOrderChange", this.KeepOrderChange);
		}

		// Token: 0x1700259E RID: 9630
		// (get) Token: 0x0600421A RID: 16922 RVA: 0x000C98AF File Offset: 0x000C7AAF
		protected override HtmlTextWriterTag TagKey
		{
			get
			{
				return HtmlTextWriterTag.Div;
			}
		}

		// Token: 0x1700259F RID: 9631
		// (get) Token: 0x0600421B RID: 16923 RVA: 0x000C98B3 File Offset: 0x000C7AB3
		protected ListView Listview
		{
			get
			{
				return this.listview;
			}
		}

		// Token: 0x170025A0 RID: 9632
		// (get) Token: 0x0600421C RID: 16924 RVA: 0x000C98BB File Offset: 0x000C7ABB
		// (set) Token: 0x0600421D RID: 16925 RVA: 0x000C98C3 File Offset: 0x000C7AC3
		public bool ShowListviewHeader
		{
			get
			{
				return this.showListviewHeader;
			}
			set
			{
				this.showListviewHeader = value;
			}
		}

		// Token: 0x170025A1 RID: 9633
		// (get) Token: 0x0600421E RID: 16926 RVA: 0x000C98CC File Offset: 0x000C7ACC
		// (set) Token: 0x0600421F RID: 16927 RVA: 0x000C98D4 File Offset: 0x000C7AD4
		protected Command EditCommand
		{
			get
			{
				return this.editCommand;
			}
			set
			{
				this.editCommand = value;
			}
		}

		// Token: 0x170025A2 RID: 9634
		// (get) Token: 0x06004220 RID: 16928 RVA: 0x000C98DD File Offset: 0x000C7ADD
		// (set) Token: 0x06004221 RID: 16929 RVA: 0x000C98EA File Offset: 0x000C7AEA
		[IDReferenceProperty(typeof(DataSourceControl))]
		public string DataSourceID
		{
			get
			{
				return this.listSource.DataSourceID;
			}
			set
			{
				this.listSource.DataSourceID = value;
			}
		}

		// Token: 0x170025A3 RID: 9635
		// (get) Token: 0x06004222 RID: 16930 RVA: 0x000C98F8 File Offset: 0x000C7AF8
		// (set) Token: 0x06004223 RID: 16931 RVA: 0x000C9905 File Offset: 0x000C7B05
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[DefaultValue("")]
		[Themeable(false)]
		[Bindable(true)]
		public object DataSource
		{
			get
			{
				return this.listSource.DataSource;
			}
			set
			{
				if (value is MultiValuedProperty<ADObjectId>)
				{
					value = RecipientObjectResolver.Instance.ResolveObjects(((MultiValuedProperty<ADObjectId>)value).ToArray()).ToArray<RecipientObjectResolverRow>();
				}
				this.listSource.DataSource = value;
			}
		}

		// Token: 0x170025A4 RID: 9636
		// (get) Token: 0x06004224 RID: 16932 RVA: 0x000C9937 File Offset: 0x000C7B37
		[MergableProperty(false)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[DefaultValue("")]
		public List<ColumnHeader> Columns
		{
			get
			{
				return this.listview.Columns;
			}
		}

		// Token: 0x170025A5 RID: 9637
		// (get) Token: 0x06004225 RID: 16933 RVA: 0x000C9944 File Offset: 0x000C7B44
		[DefaultValue(null)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		public CommandCollection Commands
		{
			get
			{
				return this.commands;
			}
		}

		// Token: 0x170025A6 RID: 9638
		// (get) Token: 0x06004226 RID: 16934 RVA: 0x000C994C File Offset: 0x000C7B4C
		// (set) Token: 0x06004227 RID: 16935 RVA: 0x000C9954 File Offset: 0x000C7B54
		[TemplateContainer(typeof(PropertiesContentPanel))]
		[Description("Content to build query parameters")]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[TemplateInstance(TemplateInstance.Single)]
		[Browsable(false)]
		[DefaultValue(null)]
		public virtual ITemplate Content { get; set; }

		// Token: 0x06004228 RID: 16936 RVA: 0x000C9960 File Offset: 0x000C7B60
		protected virtual void InitListviewCommandCollection()
		{
			if (!string.IsNullOrEmpty(this.ViewDetailsCommandUrl))
			{
				this.viewDetailsCommand.Name = "ViewDetails";
				this.viewDetailsCommand.NavigateUrl = this.ViewDetailsCommandUrl;
				if (this.viewDetailsDialogSize != Size.Empty)
				{
					this.viewDetailsCommand.DialogSize = this.viewDetailsDialogSize;
				}
				this.listview.Commands.Add(this.viewDetailsCommand);
			}
			if (!string.IsNullOrEmpty(this.WholeListEditorUrl))
			{
				this.editWholeListCommand = new Command(string.Empty, CommandSprite.SpriteId.ToolBarProperties);
				this.editWholeListCommand.SelectionMode = SelectionMode.SelectionIndependent;
				this.editWholeListCommand.OnClientClick = "$find('" + this.ClientID + "').editWholeListCommand();";
				this.editWholeListCommand.ImageAltText = Strings.EditCommandText;
				this.listview.Commands.Add(this.editWholeListCommand);
				return;
			}
			if (!this.ReadOnly)
			{
				string[] roles = (!string.IsNullOrEmpty(base.Attributes["SetRoles"])) ? base.Attributes["SetRoles"].ToArrayOfStrings() : null;
				this.addCommand.Name = "Add";
				this.addCommand.ImageAltText = Strings.CollectionEditorAddText;
				this.addCommand.SelectionMode = SelectionMode.SelectionIndependent;
				this.addCommand.OnClientClick = "$find('" + this.ClientID + "').addCommand();";
				this.addCommand.Roles = roles;
				this.listview.Commands.Add(this.addCommand);
				if (!string.IsNullOrEmpty(this.EditCommandUrl))
				{
					this.editCommand = new Command(string.Empty, CommandSprite.SpriteId.ToolBarProperties);
					this.editCommand.ImageAltText = Strings.EditCommandText;
					this.editCommand.SelectionMode = SelectionMode.RequiresSingleSelection;
					this.editCommand.DefaultCommand = true;
					this.editCommand.OnClientClick = "$find('" + this.ClientID + "').editCommand();";
					this.editCommand.Roles = roles;
					this.listview.Commands.Add(this.editCommand);
				}
				if (!this.DisableRemove)
				{
					this.removeCommand.Roles = roles;
					this.listview.Commands.Add(this.removeCommand);
				}
			}
		}

		// Token: 0x06004229 RID: 16937 RVA: 0x000C9BB4 File Offset: 0x000C7DB4
		protected override void CreateChildControls()
		{
			base.CreateChildControls();
			this.Controls.Add(this.listSource);
			this.listview.CssClass = this.ListViewCssClass;
			this.listview.ID = "listview";
			this.listview.Height = Unit.Percentage(100.0);
			this.listview.ShowSearchBar = false;
			this.listview.ShowHeader = false;
			if (this.showListviewHeader)
			{
				foreach (ColumnHeader columnHeader in this.listview.Columns)
				{
					if (!string.IsNullOrEmpty(columnHeader.Text))
					{
						this.listview.ShowHeader = true;
						break;
					}
				}
			}
			this.listview.ShowTitle = false;
			this.listview.ShowStatus = false;
			this.listview.EmptyDataText = this.EmptyDataText;
			this.listview.DataSourceID = this.listSource.ID;
			this.InitListviewCommandCollection();
			this.Controls.Add(this.listview);
			if (this.Content != null)
			{
				PropertiesContentPanel propertiesContentPanel = new PropertiesContentPanel();
				propertiesContentPanel.ID = "contentContainer";
				this.Controls.Add(propertiesContentPanel);
				this.Content.InstantiateIn(propertiesContentPanel);
			}
		}

		// Token: 0x0600422A RID: 16938 RVA: 0x000C9D18 File Offset: 0x000C7F18
		protected override void AddAttributesToRender(HtmlTextWriter writer)
		{
			if (this.ReadOnly)
			{
				writer.AddAttribute(HtmlTextWriterAttribute.ReadOnly, "readonly");
			}
			this.CssClass = "EcpCollectionEditor " + this.CssClass;
			if (this.ReadOnly && string.IsNullOrEmpty(this.ViewDetailsCommandUrl))
			{
				this.CssClass += " EcpCollectionEditorReadOnly";
			}
			base.AddAttributesToRender(writer);
		}

		// Token: 0x170025A7 RID: 9639
		// (get) Token: 0x0600422B RID: 16939 RVA: 0x000C9D82 File Offset: 0x000C7F82
		// (set) Token: 0x0600422C RID: 16940 RVA: 0x000C9D8A File Offset: 0x000C7F8A
		public string ViewDetailsCommandUrl
		{
			get
			{
				return this.viewDetailsUrl;
			}
			set
			{
				this.viewDetailsUrl = value;
			}
		}

		// Token: 0x170025A8 RID: 9640
		// (get) Token: 0x0600422D RID: 16941 RVA: 0x000C9D93 File Offset: 0x000C7F93
		public ViewDetailsCommand ViewDetailsCommand
		{
			get
			{
				return this.viewDetailsCommand;
			}
		}

		// Token: 0x170025A9 RID: 9641
		// (get) Token: 0x0600422E RID: 16942 RVA: 0x000C9D9B File Offset: 0x000C7F9B
		// (set) Token: 0x0600422F RID: 16943 RVA: 0x000C9DA3 File Offset: 0x000C7FA3
		public string EditCommandUrl
		{
			get
			{
				return this.editUrl;
			}
			set
			{
				this.editUrl = value;
			}
		}

		// Token: 0x170025AA RID: 9642
		// (get) Token: 0x06004230 RID: 16944 RVA: 0x000C9DAC File Offset: 0x000C7FAC
		// (set) Token: 0x06004231 RID: 16945 RVA: 0x000C9DB4 File Offset: 0x000C7FB4
		[DefaultValue(false)]
		public bool SplitAddRemove { get; set; }

		// Token: 0x170025AB RID: 9643
		// (get) Token: 0x06004232 RID: 16946 RVA: 0x000C9DBD File Offset: 0x000C7FBD
		// (set) Token: 0x06004233 RID: 16947 RVA: 0x000C9DC5 File Offset: 0x000C7FC5
		public Size ViewDetailsDialogSize
		{
			get
			{
				return this.viewDetailsDialogSize;
			}
			set
			{
				this.viewDetailsDialogSize = value;
			}
		}

		// Token: 0x170025AC RID: 9644
		// (get) Token: 0x06004234 RID: 16948 RVA: 0x000C9DCE File Offset: 0x000C7FCE
		// (set) Token: 0x06004235 RID: 16949 RVA: 0x000C9DDB File Offset: 0x000C7FDB
		[DefaultValue("Identity")]
		public string ValueProperty
		{
			get
			{
				return this.listview.IdentityProperty;
			}
			set
			{
				this.listview.IdentityProperty = value;
			}
		}

		// Token: 0x170025AD RID: 9645
		// (get) Token: 0x06004236 RID: 16950 RVA: 0x000C9DE9 File Offset: 0x000C7FE9
		// (set) Token: 0x06004237 RID: 16951 RVA: 0x000C9DF1 File Offset: 0x000C7FF1
		[DefaultValue("")]
		[UrlProperty("*.aspx")]
		public string PickerFormUrl
		{
			get
			{
				return this.pickerFormUrl;
			}
			set
			{
				this.pickerFormUrl = value;
			}
		}

		// Token: 0x170025AE RID: 9646
		// (get) Token: 0x06004238 RID: 16952 RVA: 0x000C9DFA File Offset: 0x000C7FFA
		// (set) Token: 0x06004239 RID: 16953 RVA: 0x000C9E02 File Offset: 0x000C8002
		[UrlProperty("*.aspx")]
		[DefaultValue("")]
		public string WholeListEditorUrl { get; set; }

		// Token: 0x170025AF RID: 9647
		// (get) Token: 0x0600423A RID: 16954 RVA: 0x000C9E0B File Offset: 0x000C800B
		// (set) Token: 0x0600423B RID: 16955 RVA: 0x000C9E13 File Offset: 0x000C8013
		[DefaultValue(530)]
		public int DialogHeight
		{
			get
			{
				return this.dialogHeight;
			}
			set
			{
				this.dialogHeight = value;
			}
		}

		// Token: 0x170025B0 RID: 9648
		// (get) Token: 0x0600423C RID: 16956 RVA: 0x000C9E1C File Offset: 0x000C801C
		// (set) Token: 0x0600423D RID: 16957 RVA: 0x000C9E24 File Offset: 0x000C8024
		[DefaultValue(450)]
		public int DialogWidth
		{
			get
			{
				return this.dialogWidth;
			}
			set
			{
				this.dialogWidth = value;
			}
		}

		// Token: 0x170025B1 RID: 9649
		// (get) Token: 0x0600423E RID: 16958 RVA: 0x000C9E2D File Offset: 0x000C802D
		// (set) Token: 0x0600423F RID: 16959 RVA: 0x000C9E35 File Offset: 0x000C8035
		[DefaultValue(0)]
		public int AddCommandDialogHeight
		{
			get
			{
				return this.addCommandDialogHeight;
			}
			set
			{
				this.addCommandDialogHeight = value;
			}
		}

		// Token: 0x170025B2 RID: 9650
		// (get) Token: 0x06004240 RID: 16960 RVA: 0x000C9E3E File Offset: 0x000C803E
		// (set) Token: 0x06004241 RID: 16961 RVA: 0x000C9E46 File Offset: 0x000C8046
		[DefaultValue(0)]
		public int AddCommandDialogWidth
		{
			get
			{
				return this.addCommandDialogWidth;
			}
			set
			{
				this.addCommandDialogWidth = value;
			}
		}

		// Token: 0x170025B3 RID: 9651
		// (get) Token: 0x06004242 RID: 16962 RVA: 0x000C9E4F File Offset: 0x000C804F
		// (set) Token: 0x06004243 RID: 16963 RVA: 0x000C9E57 File Offset: 0x000C8057
		[DefaultValue(0)]
		public int EditCommandDialogHeight
		{
			get
			{
				return this.editCommandDialogHeight;
			}
			set
			{
				this.editCommandDialogHeight = value;
			}
		}

		// Token: 0x170025B4 RID: 9652
		// (get) Token: 0x06004244 RID: 16964 RVA: 0x000C9E60 File Offset: 0x000C8060
		// (set) Token: 0x06004245 RID: 16965 RVA: 0x000C9E68 File Offset: 0x000C8068
		[DefaultValue(0)]
		public int EditCommandDialogWidth
		{
			get
			{
				return this.editCommandDialogWidth;
			}
			set
			{
				this.editCommandDialogWidth = value;
			}
		}

		// Token: 0x170025B5 RID: 9653
		// (get) Token: 0x06004246 RID: 16966 RVA: 0x000C9E71 File Offset: 0x000C8071
		public string ListViewID
		{
			get
			{
				return this.listview.ClientID;
			}
		}

		// Token: 0x170025B6 RID: 9654
		// (get) Token: 0x06004247 RID: 16967 RVA: 0x000C9E7E File Offset: 0x000C807E
		public string ListSourceID
		{
			get
			{
				return this.listSource.ClientID;
			}
		}

		// Token: 0x170025B7 RID: 9655
		// (get) Token: 0x06004248 RID: 16968 RVA: 0x000C9E8B File Offset: 0x000C808B
		public Command AddCommand
		{
			get
			{
				return this.addCommand;
			}
		}

		// Token: 0x170025B8 RID: 9656
		// (get) Token: 0x06004249 RID: 16969 RVA: 0x000C9E93 File Offset: 0x000C8093
		public Command RemoveCommand
		{
			get
			{
				return this.removeCommand;
			}
		}

		// Token: 0x170025B9 RID: 9657
		// (get) Token: 0x0600424A RID: 16970 RVA: 0x000C9E9B File Offset: 0x000C809B
		// (set) Token: 0x0600424B RID: 16971 RVA: 0x000C9EA4 File Offset: 0x000C80A4
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
				if (this.ReadOnly != value)
				{
					this.readOnly = value;
					if (this.addCommand != null)
					{
						this.addCommand.Visible = !value;
					}
					if (this.editCommand != null)
					{
						this.editCommand.Visible = !value;
					}
					if (this.removeCommand != null)
					{
						this.removeCommand.Visible = !value;
					}
				}
			}
		}

		// Token: 0x170025BA RID: 9658
		// (get) Token: 0x0600424C RID: 16972 RVA: 0x000C9F06 File Offset: 0x000C8106
		// (set) Token: 0x0600424D RID: 16973 RVA: 0x000C9F0E File Offset: 0x000C810E
		public bool DisableRemove { get; set; }

		// Token: 0x170025BB RID: 9659
		// (get) Token: 0x0600424E RID: 16974 RVA: 0x000C9F17 File Offset: 0x000C8117
		// (set) Token: 0x0600424F RID: 16975 RVA: 0x000C9F1F File Offset: 0x000C811F
		[Localizable(true)]
		[DefaultValue("")]
		public string EmptyDataText
		{
			get
			{
				return this.emptyDataText;
			}
			set
			{
				this.emptyDataText = (string.IsNullOrEmpty(value) ? " " : value);
			}
		}

		// Token: 0x170025BC RID: 9660
		// (get) Token: 0x06004250 RID: 16976 RVA: 0x000C9F37 File Offset: 0x000C8137
		// (set) Token: 0x06004251 RID: 16977 RVA: 0x000C9F44 File Offset: 0x000C8144
		[Themeable(true)]
		[DefaultValue(null)]
		public string SpriteProperty
		{
			get
			{
				return this.listview.SpriteProperty;
			}
			set
			{
				this.listview.SpriteProperty = value;
			}
		}

		// Token: 0x170025BD RID: 9661
		// (get) Token: 0x06004252 RID: 16978 RVA: 0x000C9F52 File Offset: 0x000C8152
		// (set) Token: 0x06004253 RID: 16979 RVA: 0x000C9F5F File Offset: 0x000C815F
		[Themeable(true)]
		[DefaultValue(null)]
		public string SpriteAltTextProperty
		{
			get
			{
				return this.listview.SpriteAltTextProperty;
			}
			set
			{
				this.listview.SpriteAltTextProperty = value;
			}
		}

		// Token: 0x170025BE RID: 9662
		// (get) Token: 0x06004254 RID: 16980 RVA: 0x000C9F6D File Offset: 0x000C816D
		// (set) Token: 0x06004255 RID: 16981 RVA: 0x000C9F75 File Offset: 0x000C8175
		[DefaultValue(false)]
		public bool AlwaysOpenModalDialog { get; set; }

		// Token: 0x170025BF RID: 9663
		// (get) Token: 0x06004256 RID: 16982 RVA: 0x000C9F7E File Offset: 0x000C817E
		// (set) Token: 0x06004257 RID: 16983 RVA: 0x000C9F95 File Offset: 0x000C8195
		[DefaultValue(true)]
		public bool AllowSorting
		{
			get
			{
				return this.listview != null && this.listview.AllowSorting;
			}
			set
			{
				if (this.listview != null)
				{
					this.listview.AllowSorting = value;
				}
			}
		}

		// Token: 0x170025C0 RID: 9664
		// (get) Token: 0x06004258 RID: 16984 RVA: 0x000C9FAB File Offset: 0x000C81AB
		// (set) Token: 0x06004259 RID: 16985 RVA: 0x000C9FB3 File Offset: 0x000C81B3
		[DefaultValue(false)]
		public bool UseModalessForAdd { get; set; }

		// Token: 0x170025C1 RID: 9665
		// (get) Token: 0x0600425A RID: 16986 RVA: 0x000C9FBC File Offset: 0x000C81BC
		// (set) Token: 0x0600425B RID: 16987 RVA: 0x000C9FC4 File Offset: 0x000C81C4
		[DefaultValue(false)]
		public bool IsUsingOwaPeoplePicker { get; set; }

		// Token: 0x170025C2 RID: 9666
		// (get) Token: 0x0600425C RID: 16988 RVA: 0x000C9FCD File Offset: 0x000C81CD
		// (set) Token: 0x0600425D RID: 16989 RVA: 0x000C9FD5 File Offset: 0x000C81D5
		[DefaultValue(false)]
		public bool IgnoreCaseWhenComparison { get; set; }

		// Token: 0x170025C3 RID: 9667
		// (get) Token: 0x0600425E RID: 16990 RVA: 0x000C9FDE File Offset: 0x000C81DE
		// (set) Token: 0x0600425F RID: 16991 RVA: 0x000C9FE6 File Offset: 0x000C81E6
		[DefaultValue(false)]
		public bool KeepOrderChange { get; set; }

		// Token: 0x170025C4 RID: 9668
		// (get) Token: 0x06004260 RID: 16992 RVA: 0x000C9FEF File Offset: 0x000C81EF
		// (set) Token: 0x06004261 RID: 16993 RVA: 0x000C9FF7 File Offset: 0x000C81F7
		public PickerCallerType PickerCallerType { get; set; }

		// Token: 0x170025C5 RID: 9669
		// (get) Token: 0x06004262 RID: 16994 RVA: 0x000CA000 File Offset: 0x000C8200
		// (set) Token: 0x06004263 RID: 16995 RVA: 0x000CA008 File Offset: 0x000C8208
		[DefaultValue("collectionEditorListView")]
		[Description("The CSS class of ListView in the control.")]
		[DisplayName("ListView Css Class")]
		public string ListViewCssClass
		{
			get
			{
				return this.listViewCssClass;
			}
			set
			{
				this.listViewCssClass = value;
			}
		}

		// Token: 0x170025C6 RID: 9670
		// (get) Token: 0x06004264 RID: 16996 RVA: 0x000CA011 File Offset: 0x000C8211
		// (set) Token: 0x06004265 RID: 16997 RVA: 0x000CA019 File Offset: 0x000C8219
		[Bindable(true)]
		[DefaultValue(false)]
		public bool BindOnDataSourceItem
		{
			get
			{
				return this.bindOnDataSourceItem;
			}
			set
			{
				this.bindOnDataSourceItem = value;
			}
		}

		// Token: 0x170025C7 RID: 9671
		// (set) Token: 0x06004266 RID: 16998 RVA: 0x000CA022 File Offset: 0x000C8222
		public string OnClientItemUpdated
		{
			set
			{
				this.Listview.OnClientItemUpdated = value;
			}
		}

		// Token: 0x04002B81 RID: 11137
		private string emptyDataText;

		// Token: 0x04002B82 RID: 11138
		private ListSource listSource;

		// Token: 0x04002B83 RID: 11139
		private ListView listview;

		// Token: 0x04002B84 RID: 11140
		private Command addCommand = new Command(string.Empty, CommandSprite.SpriteId.MetroAdd);

		// Token: 0x04002B85 RID: 11141
		private Command removeCommand = new RemoveCommand(false);

		// Token: 0x04002B86 RID: 11142
		private Command editWholeListCommand;

		// Token: 0x04002B87 RID: 11143
		private string pickerFormUrl;

		// Token: 0x04002B88 RID: 11144
		private ViewDetailsCommand viewDetailsCommand = new ViewDetailsCommand();

		// Token: 0x04002B89 RID: 11145
		private string viewDetailsUrl;

		// Token: 0x04002B8A RID: 11146
		private Command editCommand;

		// Token: 0x04002B8B RID: 11147
		private string editUrl;

		// Token: 0x04002B8C RID: 11148
		private bool showListviewHeader;

		// Token: 0x04002B8D RID: 11149
		private Size viewDetailsDialogSize = Size.Empty;

		// Token: 0x04002B8E RID: 11150
		private bool bindOnDataSourceItem;

		// Token: 0x04002B8F RID: 11151
		private bool readOnly;

		// Token: 0x04002B90 RID: 11152
		private string listViewCssClass;

		// Token: 0x04002B91 RID: 11153
		private CommandCollection commands = new CommandCollection();

		// Token: 0x04002B92 RID: 11154
		private int dialogHeight = 530;

		// Token: 0x04002B93 RID: 11155
		private int dialogWidth = 450;

		// Token: 0x04002B94 RID: 11156
		private int addCommandDialogHeight;

		// Token: 0x04002B95 RID: 11157
		private int addCommandDialogWidth;

		// Token: 0x04002B96 RID: 11158
		private int editCommandDialogHeight;

		// Token: 0x04002B97 RID: 11159
		private int editCommandDialogWidth;
	}
}
