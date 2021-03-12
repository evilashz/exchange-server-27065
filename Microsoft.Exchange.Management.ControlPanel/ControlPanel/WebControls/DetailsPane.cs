using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using AjaxControlToolkit;

namespace Microsoft.Exchange.Management.ControlPanel.WebControls
{
	// Token: 0x02000561 RID: 1377
	[ToolboxData("<{0}:DetailsPane runat=server></{0}:DetailsPane>")]
	[ClientScriptResource("DetailsPane", "Microsoft.Exchange.Management.ControlPanel.Client.List.js")]
	public class DetailsPane : ScriptControlBase, INamingContainer
	{
		// Token: 0x06004034 RID: 16436 RVA: 0x000C3838 File Offset: 0x000C1A38
		public DetailsPane() : base(HtmlTextWriterTag.Div)
		{
			this.toolbar = new ToolBar();
			this.toolbar.ID = "ToolBar";
			this.toolbar.CssClass = "ListViewToolBar";
			this.ShowPaddingPanels = true;
			Util.RequireUpdateProgressPopUp(this);
		}

		// Token: 0x06004035 RID: 16437 RVA: 0x000C3890 File Offset: 0x000C1A90
		protected override void CreateChildControls()
		{
			base.CreateChildControls();
			if (string.IsNullOrWhiteSpace(this.TypeProperty))
			{
				this.TypeProperty = "Type";
			}
			if (this.ShowPaddingPanels)
			{
				this.topEmptyPanel = new Panel();
				this.topEmptyPanel.ID = "topEmptyPanel";
				this.topEmptyPanel.CssClass = "masterTopEmptyPane";
				this.Controls.Add(this.topEmptyPanel);
				this.middleEmptyPanel = new Panel();
				this.middleEmptyPanel.ID = "middleEmptyPanel";
				this.middleEmptyPanel.CssClass = "masterMiddleEmptyPane";
				this.Controls.Add(this.middleEmptyPanel);
			}
			this.toolbar.ApplyRolesFilter();
			if (this.ShowToolbar)
			{
				this.toolbarPanel = this.CreateToolBarPanel();
				this.Controls.Add(this.toolbarPanel);
			}
			this.contentPanel = new Panel();
			this.contentPanel.ID = "contentPanel";
			if (string.IsNullOrEmpty(this.ContentPaneCssClass))
			{
				this.contentPanel.CssClass = "masterDetailsContentPane";
			}
			else
			{
				this.contentPanel.CssClass = this.ContentPaneCssClass;
			}
			this.frame = new HtmlGenericControl();
			this.frame.ID = "detailsFrame";
			this.frame.Attributes["frameborder"] = "0";
			this.frame.Style.Add(HtmlTextWriterStyle.Width, "100%");
			this.frame.Style.Add(HtmlTextWriterStyle.Height, "100%");
			this.frame.TagName = HtmlTextWriterTag.Iframe.ToString();
			this.frame.Style.Add(HtmlTextWriterStyle.Display, "none");
			this.frame.Attributes["class"] = "detailsFrame";
			if (Util.IsIE())
			{
				this.frame.Attributes["src"] = ThemeResource.BlankHtmlPath;
			}
			this.loadingDiv = this.CreateLoadingDivPanel();
			this.noPermissionDiv = this.CreateNoPermissionDivPanel();
			this.contentPanel.Controls.Add(this.loadingDiv);
			this.contentPanel.Controls.Add(this.frame);
			this.contentPanel.Controls.Add(this.noPermissionDiv);
			this.Controls.Add(this.contentPanel);
			if (this.ShowPaddingPanels)
			{
				this.bottomEmptyPanel = new Panel();
				this.bottomEmptyPanel.ID = "bottomEmptyPanel";
				this.bottomEmptyPanel.CssClass = "masterBottomEmptyPane";
				this.Controls.Add(this.bottomEmptyPanel);
			}
		}

		// Token: 0x06004036 RID: 16438 RVA: 0x000C3B34 File Offset: 0x000C1D34
		private Panel CreateToolBarPanel()
		{
			return new Panel
			{
				CssClass = "ToolBarContainer",
				Controls = 
				{
					this.toolbar
				}
			};
		}

		// Token: 0x06004037 RID: 16439 RVA: 0x000C3B64 File Offset: 0x000C1D64
		private Panel CreateLoadingDivPanel()
		{
			Panel panel = new Panel();
			panel.ID = "loadingDiv";
			panel.CssClass = "loadingDetails";
			panel.Style[HtmlTextWriterStyle.Display] = "none";
			Image image = new Image();
			image.ImageUrl = ThemeResource.GetThemeResource(this, "progress_sm.gif");
			image.Style[HtmlTextWriterStyle.MarginTop] = "1px";
			image.AlternateText = Strings.Loading;
			Literal literal = new Literal();
			literal.Text = Strings.Loading;
			panel.Controls.Add(image);
			panel.Controls.Add(literal);
			return panel;
		}

		// Token: 0x06004038 RID: 16440 RVA: 0x000C3C08 File Offset: 0x000C1E08
		private Panel CreateNoPermissionDivPanel()
		{
			Panel panel = new Panel();
			panel.ID = "noPermissionDiv";
			panel.CssClass = "noPermissionPane";
			panel.Style[HtmlTextWriterStyle.Display] = "none";
			Literal literal = new Literal();
			literal.Text = Strings.NoPermissionToView;
			panel.Controls.Add(literal);
			return panel;
		}

		// Token: 0x06004039 RID: 16441 RVA: 0x000C3C66 File Offset: 0x000C1E66
		protected override void AddAttributesToRender(HtmlTextWriter writer)
		{
			if (string.IsNullOrEmpty(this.CssClass))
			{
				this.CssClass = "masterDetailsPane";
			}
			else
			{
				this.CssClass += " masterDetailsPane";
			}
			base.AddAttributesToRender(writer);
		}

		// Token: 0x0600403A RID: 16442 RVA: 0x000C3CA0 File Offset: 0x000C1EA0
		protected override void OnPreRender(EventArgs e)
		{
			base.OnPreRender(e);
			if (string.IsNullOrEmpty(this.SourceID))
			{
				throw new InvalidOperationException("DetailsPane requires the SourceID parameter to be set");
			}
			Control control = this.NamingContainer.FindControl(this.SourceID);
			if (control != null)
			{
				ListView listView = control as ListView;
				if (listView != null)
				{
					this.ListViewID = listView.ClientID;
				}
				else
				{
					EcpCollectionEditor ecpCollectionEditor = control as EcpCollectionEditor;
					if (ecpCollectionEditor != null)
					{
						this.ListViewID = ecpCollectionEditor.ListViewID;
					}
				}
			}
			if (string.IsNullOrEmpty(this.ListViewID))
			{
				throw new InvalidOperationException(string.Format("Cannot find control that corresponds to SourceID '{0}' passed to the details pane with ID '{1}'. ", this.SourceID, this.ID));
			}
			if (string.IsNullOrEmpty(this.BaseUrl))
			{
				throw new InvalidOperationException("DetailsPane requires the BaseUrl property to be set");
			}
			this.BaseUrl = base.ResolveClientUrl(this.BaseUrl);
			if (string.IsNullOrEmpty(this.FrameTitle))
			{
				throw new InvalidOperationException("DetailsPane requires the FrameTitle property to be set, otherwise there is accessibility issue for screen readers.");
			}
			if (this.TypeMappings.Count != 0)
			{
				foreach (TypeMapping typeMapping in this.TypeMappings)
				{
					if (string.IsNullOrWhiteSpace(typeMapping.BaseUrl))
					{
						throw new InvalidOperationException("BaseUrl in TypeMappings property cannot be empty string");
					}
					typeMapping.BaseUrl = base.ResolveClientUrl(typeMapping.BaseUrl);
					if (typeMapping.InRole == null)
					{
						typeMapping.InRole = new bool?(LoginUtil.CheckUrlAccess(typeMapping.BaseUrl));
					}
				}
			}
		}

		// Token: 0x170024E3 RID: 9443
		// (get) Token: 0x0600403B RID: 16443 RVA: 0x000C3E20 File Offset: 0x000C2020
		private bool ShowToolbar
		{
			get
			{
				return this.Commands.Count > 0;
			}
		}

		// Token: 0x170024E4 RID: 9444
		// (get) Token: 0x0600403C RID: 16444 RVA: 0x000C3E30 File Offset: 0x000C2030
		// (set) Token: 0x0600403D RID: 16445 RVA: 0x000C3E38 File Offset: 0x000C2038
		[DefaultValue(true)]
		public bool ShowPaddingPanels { get; set; }

		// Token: 0x170024E5 RID: 9445
		// (get) Token: 0x0600403E RID: 16446 RVA: 0x000C3E41 File Offset: 0x000C2041
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[DefaultValue(null)]
		public CommandCollection Commands
		{
			get
			{
				return this.toolbar.Commands;
			}
		}

		// Token: 0x170024E6 RID: 9446
		// (get) Token: 0x0600403F RID: 16447 RVA: 0x000C3E4E File Offset: 0x000C204E
		// (set) Token: 0x06004040 RID: 16448 RVA: 0x000C3E56 File Offset: 0x000C2056
		public string ListViewID { get; private set; }

		// Token: 0x170024E7 RID: 9447
		// (get) Token: 0x06004041 RID: 16449 RVA: 0x000C3E5F File Offset: 0x000C205F
		// (set) Token: 0x06004042 RID: 16450 RVA: 0x000C3E67 File Offset: 0x000C2067
		public string BaseUrl { get; set; }

		// Token: 0x170024E8 RID: 9448
		// (get) Token: 0x06004043 RID: 16451 RVA: 0x000C3E70 File Offset: 0x000C2070
		public string ContentPanelID
		{
			get
			{
				return this.contentPanel.ClientID;
			}
		}

		// Token: 0x170024E9 RID: 9449
		// (get) Token: 0x06004044 RID: 16452 RVA: 0x000C3E7D File Offset: 0x000C207D
		public string LoadingDivID
		{
			get
			{
				return this.loadingDiv.ClientID;
			}
		}

		// Token: 0x170024EA RID: 9450
		// (get) Token: 0x06004045 RID: 16453 RVA: 0x000C3E8A File Offset: 0x000C208A
		public string MiddleEmptyPanelID
		{
			get
			{
				if (this.middleEmptyPanel == null)
				{
					return null;
				}
				return this.middleEmptyPanel.ClientID;
			}
		}

		// Token: 0x170024EB RID: 9451
		// (get) Token: 0x06004046 RID: 16454 RVA: 0x000C3EA1 File Offset: 0x000C20A1
		public string TopEmptyPanelID
		{
			get
			{
				if (this.topEmptyPanel == null)
				{
					return null;
				}
				return this.topEmptyPanel.ClientID;
			}
		}

		// Token: 0x170024EC RID: 9452
		// (get) Token: 0x06004047 RID: 16455 RVA: 0x000C3EB8 File Offset: 0x000C20B8
		public string BottomEmptyPanelID
		{
			get
			{
				if (this.bottomEmptyPanel == null)
				{
					return null;
				}
				return this.bottomEmptyPanel.ClientID;
			}
		}

		// Token: 0x170024ED RID: 9453
		// (get) Token: 0x06004048 RID: 16456 RVA: 0x000C3ECF File Offset: 0x000C20CF
		public string FrameID
		{
			get
			{
				return this.frame.ClientID;
			}
		}

		// Token: 0x170024EE RID: 9454
		// (get) Token: 0x06004049 RID: 16457 RVA: 0x000C3EDC File Offset: 0x000C20DC
		// (set) Token: 0x0600404A RID: 16458 RVA: 0x000C3EE4 File Offset: 0x000C20E4
		public string FrameTitle { get; set; }

		// Token: 0x170024EF RID: 9455
		// (get) Token: 0x0600404B RID: 16459 RVA: 0x000C3EED File Offset: 0x000C20ED
		// (set) Token: 0x0600404C RID: 16460 RVA: 0x000C3EF5 File Offset: 0x000C20F5
		public string SourceID { get; set; }

		// Token: 0x170024F0 RID: 9456
		// (get) Token: 0x0600404D RID: 16461 RVA: 0x000C3EFE File Offset: 0x000C20FE
		public List<TypeMapping> TypeMappings
		{
			get
			{
				return this.typeMappings;
			}
		}

		// Token: 0x170024F1 RID: 9457
		// (get) Token: 0x0600404E RID: 16462 RVA: 0x000C3F06 File Offset: 0x000C2106
		// (set) Token: 0x0600404F RID: 16463 RVA: 0x000C3F0E File Offset: 0x000C210E
		public string TypeProperty { get; set; }

		// Token: 0x170024F2 RID: 9458
		// (get) Token: 0x06004050 RID: 16464 RVA: 0x000C3F17 File Offset: 0x000C2117
		// (set) Token: 0x06004051 RID: 16465 RVA: 0x000C3F1F File Offset: 0x000C211F
		public string ArgumentProperty { get; set; }

		// Token: 0x170024F3 RID: 9459
		// (get) Token: 0x06004052 RID: 16466 RVA: 0x000C3F28 File Offset: 0x000C2128
		// (set) Token: 0x06004053 RID: 16467 RVA: 0x000C3F30 File Offset: 0x000C2130
		public string ContentPaneCssClass { get; set; }

		// Token: 0x170024F4 RID: 9460
		// (get) Token: 0x06004054 RID: 16468 RVA: 0x000C3F39 File Offset: 0x000C2139
		// (set) Token: 0x06004055 RID: 16469 RVA: 0x000C3F41 File Offset: 0x000C2141
		[DefaultValue(false)]
		public bool SuppressFrameCache { get; set; }

		// Token: 0x06004056 RID: 16470 RVA: 0x000C3F4C File Offset: 0x000C214C
		protected override void BuildScriptDescriptor(ScriptComponentDescriptor descriptor)
		{
			base.BuildScriptDescriptor(descriptor);
			descriptor.AddComponentProperty("ListView", this.ListViewID, this);
			descriptor.AddScriptProperty("BaseTypeMapping", new TypeMapping
			{
				BaseUrl = this.BaseUrl,
				InRole = new bool?(LoginUtil.CheckUrlAccess(this.BaseUrl))
			}.ToJsonString(null));
			if (this.TypeProperty != "Type")
			{
				descriptor.AddProperty("TypeProperty", this.TypeProperty);
			}
			if (!string.IsNullOrWhiteSpace(this.ArgumentProperty))
			{
				descriptor.AddProperty("ArgumentProperty", this.ArgumentProperty);
			}
			descriptor.AddScriptProperty("TypeMappings", this.TypeMappings.ToJsonString(null));
			if (this.SuppressFrameCache)
			{
				descriptor.AddProperty("SuppressFrameCache", true);
			}
			descriptor.AddElementProperty("ContentPanel", this.ContentPanelID, true);
			descriptor.AddElementProperty("LoadingDiv", this.LoadingDivID, true);
			descriptor.AddElementProperty("NoPermissionDiv", this.noPermissionDiv.ClientID, true);
			if (this.ShowPaddingPanels)
			{
				descriptor.AddProperty("ShowPaddingPanels", true);
				descriptor.AddElementProperty("TopEmptyPanel", this.TopEmptyPanelID, true);
				descriptor.AddElementProperty("MiddleEmptyPanel", this.MiddleEmptyPanelID, true);
				descriptor.AddElementProperty("BottomEmptyPanel", this.BottomEmptyPanelID, true);
			}
			descriptor.AddElementProperty("Frame", this.FrameID, true);
			descriptor.AddProperty("FrameTitle", this.FrameTitle, true);
			if (this.toolbar != null && this.ShowToolbar)
			{
				descriptor.AddComponentProperty("Toolbar", this.toolbar.ClientID);
			}
		}

		// Token: 0x04002AC5 RID: 10949
		private Panel toolbarPanel;

		// Token: 0x04002AC6 RID: 10950
		private ToolBar toolbar;

		// Token: 0x04002AC7 RID: 10951
		private Panel contentPanel;

		// Token: 0x04002AC8 RID: 10952
		private Panel loadingDiv;

		// Token: 0x04002AC9 RID: 10953
		private Panel noPermissionDiv;

		// Token: 0x04002ACA RID: 10954
		private Panel topEmptyPanel;

		// Token: 0x04002ACB RID: 10955
		private Panel middleEmptyPanel;

		// Token: 0x04002ACC RID: 10956
		private Panel bottomEmptyPanel;

		// Token: 0x04002ACD RID: 10957
		private HtmlGenericControl frame;

		// Token: 0x04002ACE RID: 10958
		private List<TypeMapping> typeMappings = new List<TypeMapping>();
	}
}
