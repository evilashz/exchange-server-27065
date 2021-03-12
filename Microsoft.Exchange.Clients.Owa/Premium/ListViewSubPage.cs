using System;
using System.Globalization;
using System.IO;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.Exchange.Clients.Owa.Core;
using Microsoft.Exchange.Clients.Owa.Core.Controls;
using Microsoft.Exchange.Clients.Owa.Premium.Controls;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Search;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Clients.Owa.Premium
{
	// Token: 0x02000380 RID: 896
	public abstract class ListViewSubPage : OwaSubPage
	{
		// Token: 0x0600219D RID: 8605 RVA: 0x000C04E2 File Offset: 0x000BE6E2
		protected ListViewSubPage(Trace callTracer, Trace algorithmTracer)
		{
			this.callTracer = callTracer;
			this.algorithmTracer = algorithmTracer;
		}

		// Token: 0x170008DB RID: 2267
		// (get) Token: 0x0600219E RID: 8606 RVA: 0x000C04F8 File Offset: 0x000BE6F8
		protected TextWriter Writer
		{
			get
			{
				return base.Response.Output;
			}
		}

		// Token: 0x170008DC RID: 2268
		// (get) Token: 0x0600219F RID: 8607 RVA: 0x000C0505 File Offset: 0x000BE705
		protected Trace CallTracer
		{
			get
			{
				return this.callTracer;
			}
		}

		// Token: 0x170008DD RID: 2269
		// (get) Token: 0x060021A0 RID: 8608 RVA: 0x000C050D File Offset: 0x000BE70D
		protected Trace AlgorithmTracer
		{
			get
			{
				return this.algorithmTracer;
			}
		}

		// Token: 0x170008DE RID: 2270
		// (get) Token: 0x060021A1 RID: 8609
		protected abstract int ViewWidth { get; }

		// Token: 0x170008DF RID: 2271
		// (get) Token: 0x060021A2 RID: 8610 RVA: 0x000C0515 File Offset: 0x000BE715
		protected virtual int LVRPContainerTop
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x170008E0 RID: 2272
		// (get) Token: 0x060021A3 RID: 8611 RVA: 0x000C0518 File Offset: 0x000BE718
		protected virtual int SearchControlTop
		{
			get
			{
				if (this.ShouldRenderToolbar)
				{
					return 27;
				}
				return 27 - this.ToolbarHeight;
			}
		}

		// Token: 0x170008E1 RID: 2273
		// (get) Token: 0x060021A4 RID: 8612 RVA: 0x000C052E File Offset: 0x000BE72E
		protected virtual int ToolbarHeight
		{
			get
			{
				return 25;
			}
		}

		// Token: 0x170008E2 RID: 2274
		// (get) Token: 0x060021A5 RID: 8613 RVA: 0x000C0532 File Offset: 0x000BE732
		protected virtual int ListViewTop
		{
			get
			{
				if (this.ShouldRenderToolbar)
				{
					return 60;
				}
				return 60 - this.ToolbarHeight;
			}
		}

		// Token: 0x170008E3 RID: 2275
		// (get) Token: 0x060021A6 RID: 8614
		protected abstract int ViewHeight { get; }

		// Token: 0x170008E4 RID: 2276
		// (get) Token: 0x060021A7 RID: 8615
		protected abstract SortOrder DefaultSortOrder { get; }

		// Token: 0x170008E5 RID: 2277
		// (get) Token: 0x060021A8 RID: 8616
		protected abstract SortOrder SortOrder { get; }

		// Token: 0x170008E6 RID: 2278
		// (get) Token: 0x060021A9 RID: 8617
		protected abstract ColumnId DefaultSortedColumn { get; }

		// Token: 0x170008E7 RID: 2279
		// (get) Token: 0x060021AA RID: 8618
		protected abstract ColumnId SortedColumn { get; }

		// Token: 0x170008E8 RID: 2280
		// (get) Token: 0x060021AB RID: 8619
		protected abstract ReadingPanePosition DefaultReadingPanePosition { get; }

		// Token: 0x170008E9 RID: 2281
		// (get) Token: 0x060021AC RID: 8620
		protected abstract ReadingPanePosition ReadingPanePosition { get; }

		// Token: 0x170008EA RID: 2282
		// (get) Token: 0x060021AD RID: 8621
		protected abstract bool DefaultMultiLineSetting { get; }

		// Token: 0x170008EB RID: 2283
		// (get) Token: 0x060021AE RID: 8622
		protected abstract bool IsMultiLine { get; }

		// Token: 0x170008EC RID: 2284
		// (get) Token: 0x060021AF RID: 8623
		protected abstract bool FindBarOn { get; }

		// Token: 0x170008ED RID: 2285
		// (get) Token: 0x060021B0 RID: 8624 RVA: 0x000C0548 File Offset: 0x000BE748
		protected virtual bool ShouldRenderSearch
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170008EE RID: 2286
		// (get) Token: 0x060021B1 RID: 8625
		protected abstract bool AllowAdvancedSearch { get; }

		// Token: 0x170008EF RID: 2287
		// (get) Token: 0x060021B2 RID: 8626 RVA: 0x000C054B File Offset: 0x000BE74B
		protected virtual bool RenderSearchDropDown
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170008F0 RID: 2288
		// (get) Token: 0x060021B3 RID: 8627 RVA: 0x000C054E File Offset: 0x000BE74E
		protected virtual string ContainerName
		{
			get
			{
				return string.Empty;
			}
		}

		// Token: 0x170008F1 RID: 2289
		// (get) Token: 0x060021B4 RID: 8628 RVA: 0x000C0555 File Offset: 0x000BE755
		protected virtual bool ShouldRenderToolbar
		{
			get
			{
				return true;
			}
		}

		// Token: 0x060021B5 RID: 8629
		protected abstract void LoadViewState();

		// Token: 0x060021B6 RID: 8630
		protected abstract IListView CreateListView(ColumnId sortedColumn, SortOrder sortOrder);

		// Token: 0x060021B7 RID: 8631
		protected abstract IListViewDataSource CreateDataSource(ListView listView);

		// Token: 0x060021B8 RID: 8632
		protected abstract Toolbar CreateListToolbar();

		// Token: 0x060021B9 RID: 8633
		protected abstract Toolbar CreateActionToolbar();

		// Token: 0x060021BA RID: 8634 RVA: 0x000C0558 File Offset: 0x000BE758
		protected virtual void RenderViewInfobars()
		{
		}

		// Token: 0x060021BB RID: 8635 RVA: 0x000C055A File Offset: 0x000BE75A
		protected override void OnInit(EventArgs e)
		{
			base.OnInit(e);
			this.serializedContainerId = Utilities.GetQueryStringParameter(base.Request, "id", false);
			if (Utilities.GetQueryStringParameter(base.Request, "dl", false) != null)
			{
				this.isPaaPicker = true;
			}
		}

		// Token: 0x060021BC RID: 8636 RVA: 0x000C0594 File Offset: 0x000BE794
		protected override void OnLoad(EventArgs e)
		{
			this.CallTracer.TraceDebug((long)this.GetHashCode(), "FolderListViewPage.OnLoad");
			this.LoadViewState();
			this.AlgorithmTracer.TraceDebug<bool, int, int>((long)this.GetHashCode(), "Creating ListView with isMultiline={0}, sortedColumn={1}, and sortOrder={2}", this.IsMultiLine, (int)this.SortedColumn, (int)this.SortOrder);
			this.listView = this.CreateListView(this.SortedColumn, this.SortOrder);
			ListView listView = this.listView as ListView;
			if (listView != null)
			{
				IListViewDataSource listViewDataSource = this.CreateDataSource(listView);
				listViewDataSource.Load(0, base.UserContext.UserOptions.ViewRowCount);
				listView.DataSource = listViewDataSource;
			}
			base.OnLoad(e);
		}

		// Token: 0x170008F2 RID: 2290
		// (get) Token: 0x060021BD RID: 8637 RVA: 0x000C063B File Offset: 0x000BE83B
		protected string SerializedContainerId
		{
			get
			{
				return this.serializedContainerId;
			}
		}

		// Token: 0x170008F3 RID: 2291
		// (get) Token: 0x060021BE RID: 8638 RVA: 0x000C0643 File Offset: 0x000BE843
		protected bool IsPersonalAutoAttendantPicker
		{
			get
			{
				return this.isPaaPicker;
			}
		}

		// Token: 0x060021BF RID: 8639 RVA: 0x000C064C File Offset: 0x000BE84C
		protected virtual void RenderListViewPage()
		{
			this.Writer.Write("<div id=divVw>\r\n");
			this.RenderViewInfobars();
			this.Writer.Write("\t<div id=divLVRPContainer class=lvRPContainer style=\"top:");
			this.Writer.Write(this.LVRPContainerTop);
			this.Writer.Write("px;\">\r\n");
			this.RenderReadingPaneContainer();
			this.RenderListViewContainer();
			this.RenderListViewDivider();
			this.Writer.Write("\t</div>\r\n");
			this.Writer.Write("</div>\r\n");
		}

		// Token: 0x060021C0 RID: 8640 RVA: 0x000C06D4 File Offset: 0x000BE8D4
		protected void RenderListViewContainer()
		{
			if (this.ReadingPanePosition == ReadingPanePosition.Right)
			{
				this.Writer.Write("\t\t<div id=divLVContainer class=lvContainer style=\"height:100%; width:");
				this.Writer.Write(this.ViewWidth);
				this.Writer.Write("px;\">\r\n");
			}
			else if (this.ReadingPanePosition == ReadingPanePosition.Bottom)
			{
				this.Writer.Write("\t\t<div id=divLVContainer class=\"lvContainer rpBtm\" style=\"width:100%; height:");
				this.Writer.Write(this.ViewHeight);
				this.Writer.Write("px;\">\r\n");
			}
			else if (this.ReadingPanePosition == ReadingPanePosition.Off)
			{
				this.Writer.Write("\t\t<div id=divLVContainer class=\"lvContainer rpOff\" style=\"width:100%; height:100%;\">\r\n");
			}
			if (this.ShouldRenderToolbar)
			{
				this.Writer.Write("\t\t\t<div id=\"divToolbarStrip\" class=\"ts\">");
				Toolbar toolbar = this.CreateListToolbar();
				toolbar.Render(this.Writer);
				this.Writer.Write("</div>");
			}
			if (this.ShouldRenderSearch)
			{
				this.RenderSearch();
			}
			this.Writer.Write("\r\n\t\t\t<div id=divLV class=lvPos style=\"top:");
			this.Writer.Write(this.ListViewTop);
			this.Writer.Write("px; min-width:");
			this.Writer.Write(325);
			this.Writer.Write("px;\">");
			this.RenderListView();
			this.Writer.Write("</div>\r\n");
			this.Writer.Write("\t\t</div>\r\n");
		}

		// Token: 0x060021C1 RID: 8641 RVA: 0x000C0834 File Offset: 0x000BEA34
		protected void RenderListViewDivider()
		{
			this.Writer.Write("\t\t<div id=divLVDivider class=lvDivider style=\"");
			this.Writer.Write(base.UserContext.IsRtl ? "right:" : "left:");
			this.Writer.Write(this.ViewWidth + 1);
			this.Writer.Write("px; ");
			if (this.ReadingPanePosition == ReadingPanePosition.Off || this.ReadingPanePosition == ReadingPanePosition.Bottom)
			{
				this.Writer.Write("display:none");
			}
			this.Writer.Write("\">");
			base.UserContext.RenderThemeImage(this.Writer, base.UserContext.IsRtl ? ThemeFileId.VLVShadowTopRTL : ThemeFileId.VLVShadowTop, null, new object[]
			{
				"id=\"divVLVShadowTop\""
			});
			this.Writer.Write("<div id=divVLVShadowTile></div>");
			this.Writer.Write("<img id=imgVwDrag class=dividerImgNotDragging src=\"");
			base.UserContext.RenderThemeFileUrl(this.Writer, ThemeFileId.Clear1x1);
			this.Writer.Write("\" alt=\"\">");
			this.Writer.Write("</div>\r\n");
			this.Writer.Write("\t\t<div id=divLVDividerHorizontal class=lvDividerHorizontal style= \"top:");
			this.Writer.Write(this.ViewHeight);
			this.Writer.Write("px; ");
			if (this.ReadingPanePosition == ReadingPanePosition.Off || this.ReadingPanePosition == ReadingPanePosition.Right)
			{
				this.Writer.Write("display:none");
			}
			this.Writer.Write("\">");
			this.Writer.Write("<img id=imgVwDragHor class=dividerImgNotDraggingHorizontal src=\"");
			base.UserContext.RenderThemeFileUrl(this.Writer, ThemeFileId.Clear1x1);
			this.Writer.Write("\" alt=\"\">");
			this.Writer.Write("</div>\r\n");
		}

		// Token: 0x060021C2 RID: 8642 RVA: 0x000C09FC File Offset: 0x000BEBFC
		protected void RenderReadingPaneContainer()
		{
			this.Writer.Write("\t\t<div id=divRPContainer class=rpContainer style=\"");
			if (this.ReadingPanePosition == ReadingPanePosition.Right)
			{
				this.Writer.Write(base.UserContext.IsRtl ? "right:" : "left:");
				this.Writer.Write(this.ViewWidth + 13);
				this.Writer.Write("px; top: 0px;\"");
			}
			else if (this.ReadingPanePosition == ReadingPanePosition.Bottom)
			{
				this.Writer.Write("left: 0px; right: 0px; top:");
				this.Writer.Write(this.ViewHeight + 4);
				this.Writer.Write("px;\"");
			}
			if (this.ReadingPanePosition == ReadingPanePosition.Off)
			{
				this.Writer.Write(" display:none\"");
			}
			this.Writer.Write(" >\r\n");
			Toolbar toolbar = this.CreateActionToolbar();
			if (toolbar != null)
			{
				this.Writer.Write("<div class=ts>");
				toolbar.Render(this.Writer);
				this.Writer.Write("</div>\r\n");
			}
			this.Writer.Write("\t\t\t<div id=divRP class=\"rp ");
			if (toolbar != null)
			{
				this.Writer.Write("rpTop");
			}
			this.Writer.Write("\">");
			this.RenderReadingPane();
			this.Writer.Write("</div>\r\n");
			this.Writer.Write("\t\t</div>\r\n");
		}

		// Token: 0x060021C3 RID: 8643 RVA: 0x000C0B60 File Offset: 0x000BED60
		protected void RenderReadingPane()
		{
			OwaQueryStringParameters defaultItemParameters = this.GetDefaultItemParameters();
			string text = null;
			string text2 = null;
			if (defaultItemParameters != null)
			{
				text = defaultItemParameters.ItemClass;
				FormValue formValue = RequestDispatcherUtilities.DoFormsRegistryLookup(base.UserContext, defaultItemParameters.ApplicationElement, defaultItemParameters.ItemClass, defaultItemParameters.Action, defaultItemParameters.State);
				if (formValue != null)
				{
					text2 = (formValue.Value as string);
				}
			}
			bool flag = this.ReadingPanePosition != ReadingPanePosition.Off && base.UserContext.IsFeatureEnabled(Feature.SMime) && base.UserContext.BrowserType == BrowserType.IE && text != null && ObjectClass.IsSmime(text);
			bool flag2 = this.listView.TotalCount < 1;
			if (!flag && this.ReadingPanePosition != ReadingPanePosition.Off && text2 != null && RequestDispatcher.DoesSubPageSupportSingleDocument(text2))
			{
				if (base.UserContext.IsEmbeddedReadingPaneDisabled)
				{
					this.readingPanePlaceHolder.Controls.Add(new LiteralControl("<div id=ifRP url=\"about:blank\"></div>"));
					this.readingPanePlaceHolder.RenderControl(new HtmlTextWriter(this.Writer));
					return;
				}
				try
				{
					OwaSubPage owaSubPage = (OwaSubPage)this.Page.LoadControl(Path.GetFileName(text2));
					Utilities.PutOwaSubPageIntoPlaceHolder(this.readingPanePlaceHolder, "ifRP", owaSubPage, defaultItemParameters, null, flag2);
					base.ChildSubPages.Add(owaSubPage);
					this.readingPanePlaceHolder.RenderControl(new HtmlTextWriter(this.Writer));
					return;
				}
				catch (Exception innerException)
				{
					throw new OwaRenderingEmbeddedReadingPaneException(innerException);
				}
			}
			string s;
			if (!flag && this.ReadingPanePosition != ReadingPanePosition.Off && defaultItemParameters != null)
			{
				s = defaultItemParameters.QueryString;
			}
			else
			{
				s = base.UserContext.GetBlankPage();
			}
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("<iframe id=\"ifRP\" frameborder=\"0\" allowtransparency src=\"");
			Utilities.HtmlEncode(s, stringBuilder);
			stringBuilder.Append("\"");
			if (flag2)
			{
				stringBuilder.Append(" style=\"display:none\"");
			}
			if (flag)
			{
				stringBuilder.Append(" _Loc=\"");
				Utilities.HtmlEncode(s, stringBuilder);
				stringBuilder.Append("\"");
				stringBuilder.Append(" onload=\"");
				stringBuilder.Append("var oMmCtVer = null;");
				stringBuilder.Append("try {oMmCtVer = new ActiveXObject('OwaSMime2.MimeCtlVer');}catch (e){};");
				stringBuilder.Append("if(!oMmCtVer && this._Loc && this.src != this._Loc){this._Loc='';this.onload='';this.src=this._Loc;}\"");
			}
			stringBuilder.Append("></iframe>");
			this.readingPanePlaceHolder.Controls.Add(new LiteralControl(stringBuilder.ToString()));
			this.readingPanePlaceHolder.RenderControl(new HtmlTextWriter(this.Writer));
		}

		// Token: 0x060021C4 RID: 8644 RVA: 0x000C0DD0 File Offset: 0x000BEFD0
		protected virtual string GetDefaultItemClass()
		{
			return null;
		}

		// Token: 0x060021C5 RID: 8645 RVA: 0x000C0DD3 File Offset: 0x000BEFD3
		protected virtual OwaQueryStringParameters GetDefaultItemParameters()
		{
			return null;
		}

		// Token: 0x060021C6 RID: 8646 RVA: 0x000C0DD6 File Offset: 0x000BEFD6
		protected void RenderListView(ReadingPanePosition readingPanePosition)
		{
			if (this.ReadingPanePosition != readingPanePosition)
			{
				return;
			}
			this.listView.Render(this.Writer);
		}

		// Token: 0x060021C7 RID: 8647 RVA: 0x000C0DF3 File Offset: 0x000BEFF3
		protected void RenderListView()
		{
			this.listView.RenderForCompactWebPart(this.Writer);
		}

		// Token: 0x060021C8 RID: 8648 RVA: 0x000C0E06 File Offset: 0x000BF006
		protected virtual void RenderSearch()
		{
			this.RenderSearch(null);
		}

		// Token: 0x060021C9 RID: 8649 RVA: 0x000C0E10 File Offset: 0x000BF010
		internal void RenderSearch(Folder folder)
		{
			OutlookModule outlookModule = OutlookModule.None;
			SearchScope value = SearchScope.SelectedFolder;
			bool flag = false;
			bool flag2 = false;
			MailboxSession mailboxSession = base.UserContext.MailboxSession;
			if (folder != null)
			{
				outlookModule = Utilities.GetModuleForFolder(folder, base.UserContext);
				value = base.UserContext.UserOptions.GetSearchScope(outlookModule);
				mailboxSession = (folder.Session as MailboxSession);
				if (mailboxSession != null && mailboxSession.Mailbox.IsContentIndexingEnabled && folder.Id.ObjectId.Equals(base.UserContext.GetRootFolderId(mailboxSession)))
				{
					value = SearchScope.AllFoldersAndItems;
				}
				flag = Utilities.IsPublic(folder);
				if (!flag)
				{
					flag2 = Utilities.IsOtherMailbox(folder);
				}
			}
			this.Writer.Write("<div id=divSearchBox class=iactv ");
			this.Writer.Write("style=\"top:");
			this.Writer.Write(this.SearchControlTop);
			this.Writer.Write("px;\">");
			this.Writer.Write("<div id=divBasicSearch");
			if (!this.FindBarOn)
			{
				this.Writer.Write(" style=\"display:none\"");
			}
			this.Writer.Write(">");
			this.Writer.Write("<div id=divSearch");
			this.Writer.Write(" iScp=\"");
			this.Writer.Write((int)value);
			this.Writer.Write("\"");
			if (this.RenderSearchDropDown)
			{
				this.Writer.Write(" sFldNm=\"");
				Utilities.HtmlEncode(this.ContainerName, this.Writer);
				this.Writer.Write("\" L_Fld=\"");
				this.Writer.Write(LocalizedStrings.GetHtmlEncoded(-41655958));
				this.Writer.Write("\" L_FldSub=\"");
				this.Writer.Write(LocalizedStrings.GetHtmlEncoded(-444616176));
				this.Writer.Write("\"");
				if (outlookModule == OutlookModule.Contacts || outlookModule == OutlookModule.Tasks)
				{
					this.Writer.Write(" L_Mod=\"");
					if (outlookModule == OutlookModule.Contacts)
					{
						this.Writer.Write(LocalizedStrings.GetHtmlEncoded(-1237143503));
					}
					else
					{
						this.Writer.Write(LocalizedStrings.GetHtmlEncoded(-464657744));
					}
					this.Writer.Write("\"");
				}
				else
				{
					this.Writer.Write(" L_FldAll=\"");
					if (Utilities.IsInArchiveMailbox(folder))
					{
						string s = string.Format(CultureInfo.InvariantCulture, LocalizedStrings.GetNonEncoded(1451900553), new object[]
						{
							Utilities.GetMailboxOwnerDisplayName((MailboxSession)folder.Session)
						});
						Utilities.HtmlEncode(s, this.Writer);
					}
					else
					{
						this.Writer.Write(LocalizedStrings.GetHtmlEncoded(-622081149));
					}
					this.Writer.Write("\"");
				}
			}
			if (!this.AllowAdvancedSearch)
			{
				this.Writer.Write(" style=\"");
				this.Writer.Write(base.UserContext.IsRtl ? "left:" : "right:");
				this.Writer.Write("5px;\">");
			}
			this.Writer.Write(">");
			this.Writer.Write("<div id=divTxt class=\"");
			this.Writer.Write((this.RenderSearchDropDown && folder != null && !Utilities.IsOtherMailbox(folder)) ? "txtBox" : "txtBoxNoScope");
			this.Writer.Write("\">");
			this.Writer.Write("<input type=text maxlength=256 id=txtS");
			if (this.RenderSearchDropDown)
			{
				string s2;
				switch (value)
				{
				case SearchScope.SelectedFolder:
					s2 = string.Format(CultureInfo.InvariantCulture, LocalizedStrings.GetNonEncoded(-41655958), new object[]
					{
						this.ContainerName
					});
					goto IL_442;
				case SearchScope.SelectedAndSubfolders:
					s2 = string.Format(CultureInfo.InvariantCulture, LocalizedStrings.GetNonEncoded(-444616176), new object[]
					{
						this.ContainerName
					});
					goto IL_442;
				case SearchScope.AllItemsInModule:
					if (outlookModule == OutlookModule.Contacts)
					{
						s2 = LocalizedStrings.GetNonEncoded(-1237143503);
						goto IL_442;
					}
					s2 = LocalizedStrings.GetNonEncoded(-464657744);
					goto IL_442;
				}
				if (Utilities.IsInArchiveMailbox(folder))
				{
					s2 = string.Format(CultureInfo.InvariantCulture, LocalizedStrings.GetNonEncoded(1451900553), new object[]
					{
						Utilities.GetMailboxOwnerDisplayName((MailboxSession)folder.Session)
					});
				}
				else
				{
					s2 = LocalizedStrings.GetNonEncoded(-622081149);
				}
				IL_442:
				this.Writer.Write(" class=inactv fScp=1 value=\"");
				Utilities.HtmlEncode(s2, this.Writer);
				this.Writer.Write("\"");
			}
			this.Writer.Write("></div>");
			this.Writer.Write("<div ");
			if (folder != null && Utilities.IsOtherMailbox(folder))
			{
				this.Writer.Write("class=\"sImgb iactv\" ");
			}
			else
			{
				this.Writer.Write("class=\"sImg iactv\" ");
			}
			if (!this.RenderSearchDropDown)
			{
				this.Writer.Write(" style=\"");
				this.Writer.Write(base.UserContext.IsRtl ? "left:" : "right:");
				this.Writer.Write("0px;\"");
			}
			this.Writer.Write("id=divSIcon>");
			base.UserContext.RenderThemeImage(this.Writer, ThemeFileId.Search, null, new object[]
			{
				"id=imgS"
			});
			this.Writer.Write("</div>");
			if (folder == null || !Utilities.IsOtherMailbox(folder))
			{
				this.Writer.Write("<div id=divSScp class=iactv");
				this.Writer.Write(this.RenderSearchDropDown ? ">" : " style=\"display:none\">");
				base.UserContext.RenderThemeImage(this.Writer, ThemeFileId.DownButton3);
				this.Writer.Write("</div>");
			}
			this.Writer.Write("</div>");
			if (this.AllowAdvancedSearch)
			{
				this.Writer.Write("<div id=divASChevron>");
				base.UserContext.RenderThemeImage(this.Writer, ThemeFileId.Expand, null, new object[]
				{
					"id=imgSAS",
					"title=\"" + LocalizedStrings.GetHtmlEncoded(903934295) + "\""
				});
				base.UserContext.RenderThemeImage(this.Writer, ThemeFileId.Collapse, null, new object[]
				{
					"id=imgHAS",
					"style=display:none",
					"title=\"" + LocalizedStrings.GetHtmlEncoded(-5515128) + "\""
				});
				this.Writer.Write("</div>");
			}
			this.Writer.Write("</div>");
			this.Writer.Write("<div id=divAS style=\"display:none;\">");
			if (this is FolderListViewSubPage && !flag && !mailboxSession.Mailbox.IsContentIndexingEnabled && !flag2)
			{
				this.Writer.Write("<div id=divDsblCI style=\"display:none\">");
				RenderingUtilities.RenderError(base.UserContext, this.Writer, -485624509);
				this.Writer.Write("</div>");
			}
			this.Writer.Write("</div>");
			this.Writer.Write("</div>");
		}

		// Token: 0x170008F4 RID: 2292
		// (get) Token: 0x060021CA RID: 8650 RVA: 0x000C152A File Offset: 0x000BF72A
		public override string BodyCssClass
		{
			get
			{
				if (base.UserContext.IsWebPartRequest)
				{
					return "view webpart";
				}
				return "view";
			}
		}

		// Token: 0x040017CC RID: 6092
		private const string ContainerIdQueryParameter = "id";

		// Token: 0x040017CD RID: 6093
		private const string IsPaaPickerQueryParameter = "dl";

		// Token: 0x040017CE RID: 6094
		protected const int ListViewTopWithSearch = 60;

		// Token: 0x040017CF RID: 6095
		protected const int ListViewTopWithoutSearch = 31;

		// Token: 0x040017D0 RID: 6096
		protected const int DefaultSearchControlTop = 27;

		// Token: 0x040017D1 RID: 6097
		protected const int DefaultToolbarHeight = 25;

		// Token: 0x040017D2 RID: 6098
		protected const int VlvMinWidth = 325;

		// Token: 0x040017D3 RID: 6099
		protected const int VlvMinHeight = 230;

		// Token: 0x040017D4 RID: 6100
		protected IListView listView;

		// Token: 0x040017D5 RID: 6101
		protected PlaceHolder readingPanePlaceHolder;

		// Token: 0x040017D6 RID: 6102
		private string serializedContainerId;

		// Token: 0x040017D7 RID: 6103
		private bool isPaaPicker;

		// Token: 0x040017D8 RID: 6104
		private Trace callTracer;

		// Token: 0x040017D9 RID: 6105
		private Trace algorithmTracer;
	}
}
