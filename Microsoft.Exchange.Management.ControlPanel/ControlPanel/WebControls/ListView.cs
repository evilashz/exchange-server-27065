using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using AjaxControlToolkit;
using Microsoft.Exchange.Management.DDIService;
using Microsoft.Exchange.PowerShell.RbacHostingTools;

namespace Microsoft.Exchange.Management.ControlPanel.WebControls
{
	// Token: 0x020001C3 RID: 451
	[ClientScriptResource("ListView", "Microsoft.Exchange.Management.ControlPanel.Client.List.js")]
	[RequiredScript(typeof(CommonToolkitScripts))]
	public class ListView : ScriptControlBase
	{
		// Token: 0x0600245F RID: 9311 RVA: 0x0006FDA4 File Offset: 0x0006DFA4
		public ListView()
		{
			this.Height = Unit.Percentage(100.0);
			this.Width = Unit.Percentage(100.0);
			this.toolbar = new ToolBar();
			this.toolbar.ID = "ToolBar";
			this.toolbar.CssClass = "ListViewToolBar";
			this.listSource = new WebServiceListSource();
			this.listSource.ID = "listSource";
			this.AllowSorting = true;
			this.EmptyDataText = Strings.ListViewEmptyDataText;
			this.MultiSelect = true;
			this.ShowHeader = true;
			this.ShowTitle = true;
			this.ShowToolBar = true;
			this.EnableColumnResize = true;
			this.IdentityProperty = "Identity";
			this.NameProperty = "Name";
			this.IsEditable = false;
			this.ProgressDelay = 0;
		}

		// Token: 0x17001B1B RID: 6939
		// (get) Token: 0x06002460 RID: 9312 RVA: 0x0006FEA0 File Offset: 0x0006E0A0
		// (set) Token: 0x06002461 RID: 9313 RVA: 0x0006FEA8 File Offset: 0x0006E0A8
		[DefaultValue(true)]
		public bool AllowSorting { get; set; }

		// Token: 0x17001B1C RID: 6940
		// (get) Token: 0x06002462 RID: 9314 RVA: 0x0006FEB1 File Offset: 0x0006E0B1
		// (set) Token: 0x06002463 RID: 9315 RVA: 0x0006FED8 File Offset: 0x0006E0D8
		[DefaultValue("")]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[MergableProperty(false)]
		public List<ListItem> Views
		{
			get
			{
				return this.views;
			}
			set
			{
				if (value != null)
				{
					this.views = (from x in value
					where x.Enabled && x.IsAccessibleToUser(this.Context.User)
					select x).ToList<ListItem>();
				}
			}
		}

		// Token: 0x17001B1D RID: 6941
		// (get) Token: 0x06002464 RID: 9316 RVA: 0x0006FF0C File Offset: 0x0006E10C
		// (set) Token: 0x06002465 RID: 9317 RVA: 0x0006FF14 File Offset: 0x0006E114
		public string LocalSearchViewModel { get; set; }

		// Token: 0x17001B1E RID: 6942
		// (get) Token: 0x06002466 RID: 9318 RVA: 0x0006FF1D File Offset: 0x0006E11D
		// (set) Token: 0x06002467 RID: 9319 RVA: 0x0006FF25 File Offset: 0x0006E125
		[DefaultValue(false)]
		public bool ShowSearchBarOnToolBar { get; set; }

		// Token: 0x17001B1F RID: 6943
		// (get) Token: 0x06002468 RID: 9320 RVA: 0x0006FF2E File Offset: 0x0006E12E
		// (set) Token: 0x06002469 RID: 9321 RVA: 0x0006FF36 File Offset: 0x0006E136
		[DefaultValue(false)]
		public bool PreLoad { get; set; }

		// Token: 0x17001B20 RID: 6944
		// (get) Token: 0x0600246A RID: 9322 RVA: 0x0006FF3F File Offset: 0x0006E13F
		[DefaultValue("")]
		[MergableProperty(false)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		public List<ColumnHeader> Columns
		{
			get
			{
				return this.columns;
			}
		}

		// Token: 0x17001B21 RID: 6945
		// (get) Token: 0x0600246B RID: 9323 RVA: 0x0006FF47 File Offset: 0x0006E147
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[DefaultValue(null)]
		public CommandCollection Commands
		{
			get
			{
				return this.toolbar.Commands;
			}
		}

		// Token: 0x17001B22 RID: 6946
		// (get) Token: 0x0600246C RID: 9324 RVA: 0x0006FF54 File Offset: 0x0006E154
		// (set) Token: 0x0600246D RID: 9325 RVA: 0x0006FF5C File Offset: 0x0006E15C
		public string DataSourceID { get; set; }

		// Token: 0x17001B23 RID: 6947
		// (get) Token: 0x0600246E RID: 9326 RVA: 0x0006FF65 File Offset: 0x0006E165
		// (set) Token: 0x0600246F RID: 9327 RVA: 0x0006FF72 File Offset: 0x0006E172
		[DefaultValue(null)]
		public string RefreshCookieName
		{
			get
			{
				return this.listSource.RefreshCookieName;
			}
			set
			{
				this.listSource.RefreshCookieName = value;
			}
		}

		// Token: 0x17001B24 RID: 6948
		// (get) Token: 0x06002470 RID: 9328 RVA: 0x0006FF80 File Offset: 0x0006E180
		[DefaultValue(null)]
		protected BindingCollection FilterParameters
		{
			get
			{
				return this.listSource.FilterParameters;
			}
		}

		// Token: 0x17001B25 RID: 6949
		// (get) Token: 0x06002471 RID: 9329 RVA: 0x0006FF8D File Offset: 0x0006E18D
		// (set) Token: 0x06002472 RID: 9330 RVA: 0x0006FF9A File Offset: 0x0006E19A
		public WebServiceReference ServiceUrl
		{
			get
			{
				return this.listSource.ServiceUrl;
			}
			set
			{
				this.listSource.ServiceUrl = value;
				this.DataSourceID = this.listSource.ID;
			}
		}

		// Token: 0x17001B26 RID: 6950
		// (get) Token: 0x06002473 RID: 9331 RVA: 0x0006FFB9 File Offset: 0x0006E1B9
		// (set) Token: 0x06002474 RID: 9332 RVA: 0x0006FFC6 File Offset: 0x0006E1C6
		public bool SupportAsyncGetList
		{
			get
			{
				return this.listSource.SupportAsyncGetList;
			}
			set
			{
				this.listSource.SupportAsyncGetList = value;
			}
		}

		// Token: 0x17001B27 RID: 6951
		// (get) Token: 0x06002475 RID: 9333 RVA: 0x0006FFD4 File Offset: 0x0006E1D4
		// (set) Token: 0x06002476 RID: 9334 RVA: 0x0006FFE1 File Offset: 0x0006E1E1
		[TypeConverter(typeof(StringArrayConverter))]
		public string[] RefreshAfter
		{
			get
			{
				return this.listSource.RefreshAfter;
			}
			set
			{
				this.listSource.RefreshAfter = value;
			}
		}

		// Token: 0x17001B28 RID: 6952
		// (get) Token: 0x06002477 RID: 9335 RVA: 0x0006FFEF File Offset: 0x0006E1EF
		// (set) Token: 0x06002478 RID: 9336 RVA: 0x0006FFF7 File Offset: 0x0006E1F7
		[Localizable(true)]
		public string EmptyDataText { get; set; }

		// Token: 0x17001B29 RID: 6953
		// (get) Token: 0x06002479 RID: 9337 RVA: 0x00070000 File Offset: 0x0006E200
		// (set) Token: 0x0600247A RID: 9338 RVA: 0x00070008 File Offset: 0x0006E208
		public int ProgressDelay { get; set; }

		// Token: 0x17001B2A RID: 6954
		// (get) Token: 0x0600247B RID: 9339 RVA: 0x00070011 File Offset: 0x0006E211
		// (set) Token: 0x0600247C RID: 9340 RVA: 0x00070019 File Offset: 0x0006E219
		public string OnClientItemActivated { get; set; }

		// Token: 0x17001B2B RID: 6955
		// (get) Token: 0x0600247D RID: 9341 RVA: 0x00070022 File Offset: 0x0006E222
		// (set) Token: 0x0600247E RID: 9342 RVA: 0x0007002A File Offset: 0x0006E22A
		public string OnClientItemUpdated { get; set; }

		// Token: 0x17001B2C RID: 6956
		// (get) Token: 0x0600247F RID: 9343 RVA: 0x00070033 File Offset: 0x0006E233
		// (set) Token: 0x06002480 RID: 9344 RVA: 0x0007003B File Offset: 0x0006E23B
		public string OnClientSelectionChanged { get; set; }

		// Token: 0x17001B2D RID: 6957
		// (get) Token: 0x06002481 RID: 9345 RVA: 0x00070044 File Offset: 0x0006E244
		// (set) Token: 0x06002482 RID: 9346 RVA: 0x0007004C File Offset: 0x0006E24C
		[DefaultValue(true)]
		public bool MultiSelect { get; set; }

		// Token: 0x17001B2E RID: 6958
		// (get) Token: 0x06002483 RID: 9347 RVA: 0x00070055 File Offset: 0x0006E255
		// (set) Token: 0x06002484 RID: 9348 RVA: 0x0007005D File Offset: 0x0006E25D
		[DefaultValue(false)]
		public virtual bool ShowHeader { get; set; }

		// Token: 0x17001B2F RID: 6959
		// (get) Token: 0x06002485 RID: 9349 RVA: 0x00070068 File Offset: 0x0006E268
		public int Features
		{
			get
			{
				ListViewFlags listViewFlags = (ListViewFlags)0;
				if (this.ShowTitle)
				{
					listViewFlags |= ListViewFlags.Title;
				}
				if (this.Views.Count > 0)
				{
					listViewFlags |= ListViewFlags.ViewsPanel;
				}
				if (this.ShowSearchBar)
				{
					listViewFlags |= ListViewFlags.SearchBar;
				}
				if (this.ShowStatus)
				{
					listViewFlags |= ListViewFlags.Status;
				}
				if (this.IsEditable)
				{
					listViewFlags |= ListViewFlags.IsEditable;
				}
				if (this.AllowSorting)
				{
					listViewFlags |= ListViewFlags.AllowSorting;
				}
				if (this.ShowHeader)
				{
					listViewFlags |= ListViewFlags.ShowHeader;
				}
				if (this.MultiSelect)
				{
					listViewFlags |= ListViewFlags.MultiSelect;
				}
				if (this.EnableColumnResize)
				{
					listViewFlags |= ListViewFlags.EnableColumnResize;
				}
				return (int)listViewFlags;
			}
		}

		// Token: 0x17001B30 RID: 6960
		// (get) Token: 0x06002486 RID: 9350 RVA: 0x000700F5 File Offset: 0x0006E2F5
		// (set) Token: 0x06002487 RID: 9351 RVA: 0x000700FD File Offset: 0x0006E2FD
		[DefaultValue(true)]
		public bool ShowToolBar { get; set; }

		// Token: 0x17001B31 RID: 6961
		// (get) Token: 0x06002488 RID: 9352 RVA: 0x00070106 File Offset: 0x0006E306
		// (set) Token: 0x06002489 RID: 9353 RVA: 0x00070127 File Offset: 0x0006E327
		public virtual bool ShowStatus
		{
			get
			{
				if (this.showStatus == null)
				{
					return this.showStatusDefault;
				}
				return this.showStatus.Value;
			}
			set
			{
				this.showStatus = new bool?(value);
			}
		}

		// Token: 0x17001B32 RID: 6962
		// (get) Token: 0x0600248A RID: 9354 RVA: 0x00070135 File Offset: 0x0006E335
		// (set) Token: 0x0600248B RID: 9355 RVA: 0x0007013D File Offset: 0x0006E33D
		[DefaultValue(true)]
		public bool ShowTitle { get; set; }

		// Token: 0x17001B33 RID: 6963
		// (get) Token: 0x0600248C RID: 9356 RVA: 0x00070146 File Offset: 0x0006E346
		// (set) Token: 0x0600248D RID: 9357 RVA: 0x0007014E File Offset: 0x0006E34E
		[DefaultValue(false)]
		public bool ShowSearchBar { get; set; }

		// Token: 0x17001B34 RID: 6964
		// (get) Token: 0x0600248E RID: 9358 RVA: 0x00070157 File Offset: 0x0006E357
		// (set) Token: 0x0600248F RID: 9359 RVA: 0x00070164 File Offset: 0x0006E364
		[DefaultValue(256)]
		public int SearchBarMaxLength
		{
			get
			{
				return this.SearchTextBox.MaxLength;
			}
			set
			{
				this.SearchTextBox.MaxLength = value;
			}
		}

		// Token: 0x17001B35 RID: 6965
		// (get) Token: 0x06002490 RID: 9360 RVA: 0x00070172 File Offset: 0x0006E372
		// (set) Token: 0x06002491 RID: 9361 RVA: 0x0007017F File Offset: 0x0006E37F
		[Localizable(true)]
		public string SearchButtonToolTip
		{
			get
			{
				return this.SearchTextBox.SearchButtonToolTip;
			}
			set
			{
				this.SearchTextBox.SearchButtonToolTip = value;
			}
		}

		// Token: 0x17001B36 RID: 6966
		// (get) Token: 0x06002492 RID: 9362 RVA: 0x0007018D File Offset: 0x0006E38D
		// (set) Token: 0x06002493 RID: 9363 RVA: 0x0007019A File Offset: 0x0006E39A
		[Localizable(true)]
		public string SearchTextBoxWatermarkText
		{
			get
			{
				return this.SearchTextBox.WatermarkText;
			}
			set
			{
				this.SearchTextBox.WatermarkText = value;
			}
		}

		// Token: 0x17001B37 RID: 6967
		// (get) Token: 0x06002494 RID: 9364 RVA: 0x000701A8 File Offset: 0x0006E3A8
		// (set) Token: 0x06002495 RID: 9365 RVA: 0x000701B5 File Offset: 0x0006E3B5
		[DefaultValue(false)]
		public bool EnableAutoSuggestion
		{
			get
			{
				return this.SearchTextBox.EnableAutoSuggestion;
			}
			set
			{
				this.SearchTextBox.EnableAutoSuggestion = value;
			}
		}

		// Token: 0x17001B38 RID: 6968
		// (get) Token: 0x06002496 RID: 9366 RVA: 0x000701C3 File Offset: 0x0006E3C3
		// (set) Token: 0x06002497 RID: 9367 RVA: 0x000701D0 File Offset: 0x0006E3D0
		[DefaultValue("GetSuggestion")]
		public string AutoSuggestionServiceWorkflow
		{
			get
			{
				return this.SearchTextBox.SuggestionServiceWorkFlow;
			}
			set
			{
				this.SearchTextBox.SuggestionServiceWorkFlow = value;
			}
		}

		// Token: 0x17001B39 RID: 6969
		// (get) Token: 0x06002498 RID: 9368 RVA: 0x000701DE File Offset: 0x0006E3DE
		// (set) Token: 0x06002499 RID: 9369 RVA: 0x000701EB File Offset: 0x0006E3EB
		[DefaultValue("GetList")]
		public string AutoSuggestionServiceMethod
		{
			get
			{
				return this.SearchTextBox.SuggestionServiceMethod;
			}
			set
			{
				this.SearchTextBox.SuggestionServiceMethod = value;
			}
		}

		// Token: 0x17001B3A RID: 6970
		// (get) Token: 0x0600249A RID: 9370 RVA: 0x000701F9 File Offset: 0x0006E3F9
		// (set) Token: 0x0600249B RID: 9371 RVA: 0x00070206 File Offset: 0x0006E406
		public string AutoSuggestionPropertyNames
		{
			get
			{
				return this.SearchTextBox.AutoSuggestionPropertyNames;
			}
			set
			{
				this.SearchTextBox.AutoSuggestionPropertyNames = value;
			}
		}

		// Token: 0x17001B3B RID: 6971
		// (get) Token: 0x0600249C RID: 9372 RVA: 0x00070214 File Offset: 0x0006E414
		// (set) Token: 0x0600249D RID: 9373 RVA: 0x00070221 File Offset: 0x0006E421
		public string AutoSuggestionPropertyValues
		{
			get
			{
				return this.SearchTextBox.AutoSuggestionPropertyValues;
			}
			set
			{
				this.SearchTextBox.AutoSuggestionPropertyValues = value;
			}
		}

		// Token: 0x17001B3C RID: 6972
		// (get) Token: 0x0600249E RID: 9374 RVA: 0x0007022F File Offset: 0x0006E42F
		// (set) Token: 0x0600249F RID: 9375 RVA: 0x00070237 File Offset: 0x0006E437
		public SortDirection SortDirection { get; set; }

		// Token: 0x17001B3D RID: 6973
		// (get) Token: 0x060024A0 RID: 9376 RVA: 0x00070240 File Offset: 0x0006E440
		// (set) Token: 0x060024A1 RID: 9377 RVA: 0x00070248 File Offset: 0x0006E448
		public string SortProperty { get; set; }

		// Token: 0x17001B3E RID: 6974
		// (get) Token: 0x060024A2 RID: 9378 RVA: 0x00070251 File Offset: 0x0006E451
		// (set) Token: 0x060024A3 RID: 9379 RVA: 0x0007025E File Offset: 0x0006E45E
		public bool ClientSort
		{
			get
			{
				return this.listSource.ClientSort;
			}
			set
			{
				this.listSource.ClientSort = value;
			}
		}

		// Token: 0x17001B3F RID: 6975
		// (get) Token: 0x060024A4 RID: 9380 RVA: 0x0007026C File Offset: 0x0006E46C
		// (set) Token: 0x060024A5 RID: 9381 RVA: 0x00070274 File Offset: 0x0006E474
		[DefaultValue(null)]
		public string CaptionText { get; set; }

		// Token: 0x17001B40 RID: 6976
		// (get) Token: 0x060024A6 RID: 9382 RVA: 0x0007027D File Offset: 0x0006E47D
		// (set) Token: 0x060024A7 RID: 9383 RVA: 0x00070285 File Offset: 0x0006E485
		[DefaultValue(typeof(Unit), "100%")]
		public override Unit Height
		{
			get
			{
				return base.Height;
			}
			set
			{
				base.Height = value;
			}
		}

		// Token: 0x17001B41 RID: 6977
		// (get) Token: 0x060024A8 RID: 9384 RVA: 0x0007028E File Offset: 0x0006E48E
		// (set) Token: 0x060024A9 RID: 9385 RVA: 0x00070296 File Offset: 0x0006E496
		public string IdentityProperty { get; set; }

		// Token: 0x17001B42 RID: 6978
		// (get) Token: 0x060024AA RID: 9386 RVA: 0x0007029F File Offset: 0x0006E49F
		// (set) Token: 0x060024AB RID: 9387 RVA: 0x000702A7 File Offset: 0x0006E4A7
		[DefaultValue(null)]
		public string DefaultSprite { get; set; }

		// Token: 0x17001B43 RID: 6979
		// (get) Token: 0x060024AC RID: 9388 RVA: 0x000702B0 File Offset: 0x0006E4B0
		// (set) Token: 0x060024AD RID: 9389 RVA: 0x000702B8 File Offset: 0x0006E4B8
		[DefaultValue(null)]
		public string SpriteProperty { get; set; }

		// Token: 0x17001B44 RID: 6980
		// (get) Token: 0x060024AE RID: 9390 RVA: 0x000702C1 File Offset: 0x0006E4C1
		// (set) Token: 0x060024AF RID: 9391 RVA: 0x000702C9 File Offset: 0x0006E4C9
		[DefaultValue(null)]
		public string SpriteAltTextProperty { get; set; }

		// Token: 0x17001B45 RID: 6981
		// (get) Token: 0x060024B0 RID: 9392 RVA: 0x000702D2 File Offset: 0x0006E4D2
		// (set) Token: 0x060024B1 RID: 9393 RVA: 0x000702DA File Offset: 0x0006E4DA
		public string NameProperty { get; set; }

		// Token: 0x17001B46 RID: 6982
		// (get) Token: 0x060024B2 RID: 9394 RVA: 0x000702E3 File Offset: 0x0006E4E3
		// (set) Token: 0x060024B3 RID: 9395 RVA: 0x000702EB File Offset: 0x0006E4EB
		[DefaultValue(false)]
		[Browsable(false)]
		public bool IsEditable { get; set; }

		// Token: 0x17001B47 RID: 6983
		// (get) Token: 0x060024B4 RID: 9396 RVA: 0x000702F4 File Offset: 0x0006E4F4
		// (set) Token: 0x060024B5 RID: 9397 RVA: 0x000702FC File Offset: 0x0006E4FC
		[DefaultValue(false)]
		public bool WarningAsError { get; set; }

		// Token: 0x17001B48 RID: 6984
		// (get) Token: 0x060024B6 RID: 9398 RVA: 0x00070305 File Offset: 0x0006E505
		// (set) Token: 0x060024B7 RID: 9399 RVA: 0x0007030D File Offset: 0x0006E50D
		[Browsable(false)]
		public int InlineEditMaxLength { get; set; }

		// Token: 0x17001B49 RID: 6985
		// (get) Token: 0x060024B8 RID: 9400 RVA: 0x00070316 File Offset: 0x0006E516
		// (set) Token: 0x060024B9 RID: 9401 RVA: 0x0007031E File Offset: 0x0006E51E
		public string InputWaterMarkText { get; set; }

		// Token: 0x17001B4A RID: 6986
		// (get) Token: 0x060024BA RID: 9402 RVA: 0x00070327 File Offset: 0x0006E527
		// (set) Token: 0x060024BB RID: 9403 RVA: 0x0007032F File Offset: 0x0006E52F
		[DefaultValue(typeof(Unit), "100%")]
		public override Unit Width
		{
			get
			{
				return base.Width;
			}
			set
			{
				base.Width = value;
			}
		}

		// Token: 0x17001B4B RID: 6987
		// (get) Token: 0x060024BC RID: 9404 RVA: 0x00070338 File Offset: 0x0006E538
		// (set) Token: 0x060024BD RID: 9405 RVA: 0x00070340 File Offset: 0x0006E540
		[DefaultValue(true)]
		public bool EnableColumnResize { get; set; }

		// Token: 0x17001B4C RID: 6988
		// (get) Token: 0x060024BE RID: 9406 RVA: 0x00070349 File Offset: 0x0006E549
		protected override HtmlTextWriterTag TagKey
		{
			get
			{
				return HtmlTextWriterTag.Div;
			}
		}

		// Token: 0x17001B4D RID: 6989
		// (get) Token: 0x060024BF RID: 9407 RVA: 0x0007034D File Offset: 0x0006E54D
		// (set) Token: 0x060024C0 RID: 9408 RVA: 0x0007035A File Offset: 0x0006E55A
		public string SearchTextBoxToolTip
		{
			get
			{
				return this.SearchTextBox.ToolTip;
			}
			set
			{
				this.SearchTextBox.ToolTip = value;
			}
		}

		// Token: 0x17001B4E RID: 6990
		// (get) Token: 0x060024C1 RID: 9409 RVA: 0x00070368 File Offset: 0x0006E568
		internal FilterTextBox SearchTextBox
		{
			get
			{
				if (this.searchTextBox == null)
				{
					this.searchTextBox = new FilterTextBox();
					this.searchTextBox.ID = "SearchBox";
				}
				return this.searchTextBox;
			}
		}

		// Token: 0x17001B4F RID: 6991
		// (get) Token: 0x060024C2 RID: 9410 RVA: 0x00070393 File Offset: 0x0006E593
		private FilterDropDown ViewFilterDropDown
		{
			get
			{
				if (this.viewFilterDropDown == null)
				{
					this.viewFilterDropDown = new FilterDropDown();
					this.viewFilterDropDown.ID = "ViewFilterDropDown";
				}
				return this.viewFilterDropDown;
			}
		}

		// Token: 0x060024C3 RID: 9411 RVA: 0x000703C0 File Offset: 0x0006E5C0
		protected override void OnLoad(EventArgs e)
		{
			base.OnLoad(e);
			if (this.showStatus == null && this.Page != null)
			{
				EcpContentPage ecpContentPage = this.Page as EcpContentPage;
				if (ecpContentPage != null && ecpContentPage.FeatureSet == FeatureSet.Options)
				{
					this.showStatusDefault = false;
				}
			}
			if (!string.IsNullOrEmpty(this.DataSourceID))
			{
				WebServiceListSource webServiceListSource = this.FindControl(this.DataSourceID) as WebServiceListSource;
				if (webServiceListSource != null)
				{
					this.listSource = webServiceListSource;
					this.Commands.Add(new RefreshCommand());
				}
			}
			this.toolbar.ApplyRolesFilter();
			if (this.Commands.Count == 0 && this.Controls.Contains(this.toolbar))
			{
				this.Controls.Remove(this.toolbar);
			}
			this.UpdateColumns();
			if (this.Views.Count > 0)
			{
				ComponentBinding componentBinding = new ComponentBinding(this.viewFilterDropDown, "filterValue");
				componentBinding.Name = "SelectedView";
				this.listSource.FilterParameters.Add(componentBinding);
			}
			if (this.ShowSearchBar)
			{
				if (string.IsNullOrEmpty(this.LocalSearchViewModel))
				{
					ComponentBinding componentBinding2 = new ComponentBinding(this.searchTextBox, "filterText");
					componentBinding2.Name = "SearchText";
					this.listSource.FilterParameters.Add(componentBinding2);
				}
				else
				{
					ComponentBinding componentBinding3 = new ComponentBinding(this.searchTextBox, "advancedSearch");
					componentBinding3.Name = "SearchText";
					this.listSource.FilterParameters.Add(componentBinding3);
				}
			}
			this.ClientSort |= this.SupportAsyncGetList;
			if (this.ShowHeader && !this.ClientSort)
			{
				ComponentBinding componentBinding4 = new ComponentBinding(this, "SortDirection");
				componentBinding4.Name = "Direction";
				this.listSource.SortParameters.Add(componentBinding4);
				ComponentBinding componentBinding5 = new ComponentBinding(this, "SortProperty");
				componentBinding5.Name = "PropertyName";
				this.listSource.SortParameters.Add(componentBinding5);
			}
			this.listSource.UpdateParameters();
		}

		// Token: 0x060024C4 RID: 9412 RVA: 0x000705E4 File Offset: 0x0006E7E4
		private void UpdateColumns()
		{
			if (!string.IsNullOrEmpty(this.DefaultSprite) || !string.IsNullOrEmpty(this.SpriteProperty))
			{
				this.Columns.Insert(0, new SpriteColumnHeader
				{
					DefaultSprite = this.DefaultSprite,
					Name = this.SpriteProperty,
					AlternateTextProperty = this.SpriteAltTextProperty
				});
			}
			if (this.AllowSorting && this.SortProperty == null)
			{
				foreach (ColumnHeader columnHeader in this.Columns)
				{
					if (!string.IsNullOrEmpty(columnHeader.SortExpression) && !(columnHeader is SpriteColumnHeader))
					{
						this.SortProperty = columnHeader.SortExpression;
						break;
					}
				}
			}
			RbacPrincipal rbacPrincipal = RbacPrincipal.Current;
			this.Columns.RemoveAll((ColumnHeader x) => !string.IsNullOrEmpty(x.Role) && !rbacPrincipal.IsInRole(x.Role));
		}

		// Token: 0x060024C5 RID: 9413 RVA: 0x000706E0 File Offset: 0x0006E8E0
		protected override void OnPreRender(EventArgs e)
		{
			base.OnPreRender(e);
			if (this.ServiceUrl != null)
			{
				this.SearchTextBox.SuggestionServicePath = this.ServiceUrl.ServiceUrl;
			}
			if (!string.IsNullOrEmpty(this.LocalSearchViewModel))
			{
				this.SearchTextBox.Attributes.Add("data-type", this.LocalSearchViewModel);
				this.SearchTextBox.Attributes.Add("data-control", "FilterTextBox");
				this.SearchTextBox.Attributes.Add("data-text", "{Text}");
				this.SearchTextBox.Attributes.Add("data-advancedSearch", "{AdvancedSearch, Mode=OneWay}");
				this.SearchTextBox.Attributes.Add("vm-SimpleSearchFields", base.Attributes["vm-SimpleSearchFields"]);
			}
			if (this.PreLoad)
			{
				HttpContext httpContext = HttpContext.Current;
				httpContext.Items.Add("getlistasync", "1");
				this.preLoadResults = this.ServiceUrl.GetList(new DDIParameters(), new SortOptions());
			}
		}

		// Token: 0x060024C6 RID: 9414 RVA: 0x000707F0 File Offset: 0x0006E9F0
		protected override void Render(HtmlTextWriter writer)
		{
			if (this.toolbarPanel != null)
			{
				this.toolbarPanel.Style[HtmlTextWriterStyle.Display] = ((this.ShowToolBar && this.Commands.ContainsVisibleCommands()) ? string.Empty : "none");
			}
			base.Render(writer);
		}

		// Token: 0x060024C7 RID: 9415 RVA: 0x00070840 File Offset: 0x0006EA40
		protected override void AddAttributesToRender(HtmlTextWriter writer)
		{
			writer.AddStyleAttribute(HtmlTextWriterStyle.Overflow, "hidden");
			string cssClass = this.CssClass;
			if (string.IsNullOrEmpty(cssClass))
			{
				this.CssClass = "ListView";
			}
			else
			{
				this.CssClass += " ListView";
			}
			base.AddAttributesToRender(writer);
			this.CssClass = cssClass;
		}

		// Token: 0x060024C8 RID: 9416 RVA: 0x0007089C File Offset: 0x0006EA9C
		protected override void CreateChildControls()
		{
			bool hasViewsPanel = false;
			if (this.Views.Count > 0)
			{
				hasViewsPanel = true;
				WebControl child = this.CreateViewsPanel();
				this.Controls.Add(child);
			}
			WebControl webControl = null;
			if (this.ShowSearchBar)
			{
				webControl = this.CreateSearchBar();
				if (this.ShowSearchBarOnToolBar)
				{
					InlineSearchBarCommand inlineSearchBarCommand = new InlineSearchBarCommand();
					inlineSearchBarCommand.ImageAltText = Strings.SearchCommandText;
					inlineSearchBarCommand.ImageId = CommandSprite.SpriteId.SearchDefault;
					webControl.ClientIDMode = ClientIDMode.Static;
					inlineSearchBarCommand.ControlIdToMove = webControl.ClientID;
					inlineSearchBarCommand.MovedControlCss = "ToolBarSearchBar";
					this.Commands.Add(inlineSearchBarCommand);
				}
			}
			if (this.Commands.Count > 0 && this.ShowToolBar)
			{
				this.toolbarPanel = (Panel)this.CreateToolBarControl(hasViewsPanel);
				this.Controls.Add(this.toolbarPanel);
			}
			if (webControl != null)
			{
				this.Controls.Add(webControl);
			}
			if (this.IsEditable)
			{
				this.listViewInputPanel = this.CreateInputBar();
				this.Controls.Add(this.listViewInputPanel);
			}
			if (!this.AllowSorting)
			{
				foreach (ColumnHeader columnHeader in this.Columns)
				{
					columnHeader.IsSortable = false;
				}
			}
			if (this.ServiceUrl != null)
			{
				this.Controls.Add(this.listSource);
			}
			base.Attributes.Add("role", "application");
			base.CreateChildControls();
		}

		// Token: 0x060024C9 RID: 9417 RVA: 0x00070A2C File Offset: 0x0006EC2C
		protected override void BuildScriptDescriptor(ScriptComponentDescriptor descriptor)
		{
			base.BuildScriptDescriptor(descriptor);
			descriptor.AddComponentProperty("DataSource", this.DataSourceID, this);
			descriptor.AddEvent("itemActivated", this.OnClientItemActivated, true);
			descriptor.AddEvent("itemUpdated", this.OnClientItemUpdated, true);
			descriptor.AddEvent("selectionChanged", this.OnClientSelectionChanged, true);
			descriptor.AddProperty("EmptyDataText", this.EmptyDataText);
			descriptor.AddProperty("CaptionText", this.CaptionText, true);
			if (this.ProgressDelay != 0)
			{
				descriptor.AddProperty("ProgressDelay", this.ProgressDelay);
			}
			if (this.Features != 0)
			{
				descriptor.AddProperty("Features", this.Features);
			}
			if (this.SortDirection != SortDirection.Ascending)
			{
				descriptor.AddProperty("SortDirection", this.SortDirection);
			}
			descriptor.AddProperty("SortProperty", this.SortProperty, true);
			if (this.IdentityProperty != "Identity")
			{
				descriptor.AddProperty("IdentityProperty", this.IdentityProperty);
			}
			if (this.PreLoad)
			{
				descriptor.AddProperty("PreLoadResultsString", this.preLoadResults.ToJsonString(null));
			}
			if (this.NameProperty != "Name")
			{
				descriptor.AddProperty("NameProperty", this.NameProperty);
			}
			if (this.InlineEditMaxLength != 128)
			{
				descriptor.AddProperty("InlineEditMaxLength", this.InlineEditMaxLength);
			}
			if (this.SupportAsyncGetList)
			{
				descriptor.AddProperty("PageSize", ListView.pageSize, 500);
			}
			else
			{
				descriptor.AddProperty("PageSize", ListView.pageSizeWithNoPaging, 3000);
			}
			descriptor.AddProperty("PageSizes", ListView.pageSizes, true);
			StringBuilder stringBuilder = new StringBuilder("[");
			stringBuilder.Append(string.Join(",", from o in this.Columns
			select o.ToJavaScript()));
			stringBuilder.Append("]");
			descriptor.AddScriptProperty("Columns", stringBuilder.ToString());
			if (this.toolbarPanel != null && this.ShowToolBar)
			{
				descriptor.AddComponentProperty("ToolBar", this.toolbar.ClientID);
			}
			if (this.viewFilterDropDown != null)
			{
				descriptor.AddComponentProperty("ViewFilterDropDown", this.viewFilterDropDown.ClientID);
			}
			if (this.searchTextBox != null && this.ShowSearchBar)
			{
				descriptor.AddComponentProperty("SearchTextBox", this.searchTextBox.ClientID);
			}
			if (this.listViewInputPanel != null && this.IsEditable)
			{
				descriptor.AddComponentProperty("InputTextBox", this.listViewInputPanel.ClientID);
			}
		}

		// Token: 0x060024CA RID: 9418 RVA: 0x00070CD8 File Offset: 0x0006EED8
		private WebControl CreateToolBarControl(bool hasViewsPanel)
		{
			return new Panel
			{
				CssClass = (hasViewsPanel ? "ToolBarContainer WithViewsPanel" : "ToolBarContainer"),
				Controls = 
				{
					this.toolbar
				}
			};
		}

		// Token: 0x060024CB RID: 9419 RVA: 0x00070D14 File Offset: 0x0006EF14
		private WebControl CreateViewsPanel()
		{
			Panel panel = new Panel();
			panel.CssClass = "ViewsPanel";
			panel.ID = "ViewsPanel";
			this.ViewFilterDropDown.Items.AddRange(this.Views.ToArray());
			string value = this.Page.Request.QueryString["vw"];
			if (!string.IsNullOrEmpty(value))
			{
				ListItem listItem = this.ViewFilterDropDown.Items.FindByValue(value);
				if (listItem != null)
				{
					this.ViewFilterDropDown.ClearSelection();
					listItem.Selected = true;
				}
			}
			this.ViewFilterDropDown.Width = Unit.Percentage(100.0);
			Panel panel2 = new Panel();
			panel2.Controls.Add(this.ViewFilterDropDown);
			panel.Controls.Add(panel2);
			return panel;
		}

		// Token: 0x060024CC RID: 9420 RVA: 0x00070DE0 File Offset: 0x0006EFE0
		private WebControl CreateSearchBar()
		{
			Panel panel = new Panel();
			panel.CssClass = "SearchBar";
			panel.ID = "SearchBar";
			panel.Attributes.Add("role", "search");
			this.SearchTextBox.Width = Unit.Percentage(100.0);
			Panel panel2 = new Panel();
			panel2.Controls.Add(this.SearchTextBox);
			panel.Controls.Add(panel2);
			if (this.ShowSearchBarOnToolBar)
			{
				panel.Style.Add(HtmlTextWriterStyle.Display, "none");
			}
			return panel;
		}

		// Token: 0x060024CD RID: 9421 RVA: 0x00070E78 File Offset: 0x0006F078
		private Panel CreateInputBar()
		{
			return new ListViewInputPanel
			{
				ID = "InputBar",
				MaxLength = this.InlineEditMaxLength,
				WatermarkText = this.InputWaterMarkText
			};
		}

		// Token: 0x04001E71 RID: 7793
		public const string RefreshCommandName = "Refresh";

		// Token: 0x04001E72 RID: 7794
		private const int DefaultPageSize = 500;

		// Token: 0x04001E73 RID: 7795
		private const int DefaultSizeForNoPagingListView = 3000;

		// Token: 0x04001E74 RID: 7796
		private static readonly object EventSelectionChanged = new object();

		// Token: 0x04001E75 RID: 7797
		private static readonly int pageSize = ConfigUtil.ReadInt("ListViewPageSize", 500);

		// Token: 0x04001E76 RID: 7798
		private static readonly string pageSizes = ConfigurationManager.AppSettings["ListViewPageSizes"];

		// Token: 0x04001E77 RID: 7799
		private static readonly int pageSizeWithNoPaging = ConfigUtil.ReadInt("ListViewSizeForNoPaging", 3000);

		// Token: 0x04001E78 RID: 7800
		private bool? showStatus;

		// Token: 0x04001E79 RID: 7801
		private bool showStatusDefault = true;

		// Token: 0x04001E7A RID: 7802
		private ToolBar toolbar;

		// Token: 0x04001E7B RID: 7803
		private Panel toolbarPanel;

		// Token: 0x04001E7C RID: 7804
		private Panel listViewInputPanel;

		// Token: 0x04001E7D RID: 7805
		private List<ListItem> views = new List<ListItem>();

		// Token: 0x04001E7E RID: 7806
		private List<ColumnHeader> columns = new List<ColumnHeader>();

		// Token: 0x04001E7F RID: 7807
		private WebServiceListSource listSource;

		// Token: 0x04001E80 RID: 7808
		private FilterTextBox searchTextBox;

		// Token: 0x04001E81 RID: 7809
		private FilterDropDown viewFilterDropDown;

		// Token: 0x04001E82 RID: 7810
		private PowerShellResults preLoadResults;
	}
}
