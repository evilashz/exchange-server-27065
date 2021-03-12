using System;
using System.IO;
using System.Web;
using Microsoft.Exchange.Clients.Owa.Basic.Controls;
using Microsoft.Exchange.Clients.Owa.Core;
using Microsoft.Exchange.Clients.Owa.Core.Controls;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Search;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics.Components.Clients;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.InfoWorker.Common.OOF;
using Microsoft.Exchange.Security;

namespace Microsoft.Exchange.Clients.Owa.Basic
{
	// Token: 0x0200009E RID: 158
	public class MessageView : ListViewPage, IRegistryOnlyForm
	{
		// Token: 0x0600058C RID: 1420 RVA: 0x0002BBD6 File Offset: 0x00029DD6
		public MessageView() : base(ExTraceGlobals.MailCallTracer, ExTraceGlobals.MailTracer)
		{
		}

		// Token: 0x0600058D RID: 1421 RVA: 0x0002BBE8 File Offset: 0x00029DE8
		protected override void OnLoad(EventArgs e)
		{
			base.FilteredView = false;
			base.OnLoad(e);
			EditCalendarItemHelper.ClearUserContextData(base.UserContext);
			this.CreateExpiringPasswordNotification();
			this.CreateOutOfOfficeNotification();
			int num = RequestParser.TryGetIntValueFromQueryString(base.Request, "slUsng", -1);
			if (num >= 0 && num <= 2)
			{
				this.selectedUsing = (SecondaryNavigationArea)num;
			}
			if (base.UserContext.IsWebPartRequest)
			{
				base.UserContext.LastClientViewState = new WebPartModuleViewState(base.FolderId, base.Folder.ClassName, base.PageNumber, NavigationModule.Mail, base.SortOrder, base.SortedColumn);
				return;
			}
			if (base.FilteredView)
			{
				base.UserContext.LastClientViewState = new MessageModuleSearchViewState(base.UserContext.LastClientViewState, base.FolderId, base.OwaContext.FormsRegistryContext.Type, this.selectedUsing, base.PageNumber, base.SearchString, base.SearchScope);
				return;
			}
			base.UserContext.LastClientViewState = new MessageModuleViewState(base.FolderId, base.OwaContext.FormsRegistryContext.Type, this.selectedUsing, base.PageNumber);
		}

		// Token: 0x170001BD RID: 445
		// (get) Token: 0x0600058E RID: 1422 RVA: 0x0002BD04 File Offset: 0x00029F04
		internal override StoreObjectId DefaultFolderId
		{
			get
			{
				return base.UserContext.InboxFolderId;
			}
		}

		// Token: 0x170001BE RID: 446
		// (get) Token: 0x0600058F RID: 1423 RVA: 0x0002BD11 File Offset: 0x00029F11
		protected override SortOrder DefaultSortOrder
		{
			get
			{
				return SortOrder.Descending;
			}
		}

		// Token: 0x170001BF RID: 447
		// (get) Token: 0x06000590 RID: 1424 RVA: 0x0002BD14 File Offset: 0x00029F14
		protected override ColumnId DefaultSortedColumn
		{
			get
			{
				DefaultFolderType defaultFolderType = Utilities.GetDefaultFolderType(base.UserContext.MailboxSession, base.FolderId);
				if (defaultFolderType == DefaultFolderType.SentItems || defaultFolderType == DefaultFolderType.Outbox || defaultFolderType == DefaultFolderType.Drafts)
				{
					return ColumnId.SentTime;
				}
				return ColumnId.DeliveryTime;
			}
		}

		// Token: 0x170001C0 RID: 448
		// (get) Token: 0x06000591 RID: 1425 RVA: 0x0002BD49 File Offset: 0x00029F49
		public string UrlEncodedFolderId
		{
			get
			{
				return HttpUtility.UrlEncode(base.Folder.Id.ObjectId.ToBase64String());
			}
		}

		// Token: 0x170001C1 RID: 449
		// (get) Token: 0x06000592 RID: 1426 RVA: 0x0002BD65 File Offset: 0x00029F65
		public bool IsDeletedItemsFolder
		{
			get
			{
				return Utilities.IsDefaultFolder(base.Folder, DefaultFolderType.DeletedItems);
			}
		}

		// Token: 0x170001C2 RID: 450
		// (get) Token: 0x06000593 RID: 1427 RVA: 0x0002BD73 File Offset: 0x00029F73
		protected bool IsJunkEmailFolder
		{
			get
			{
				return Utilities.IsDefaultFolder(base.Folder, DefaultFolderType.JunkEmail);
			}
		}

		// Token: 0x170001C3 RID: 451
		// (get) Token: 0x06000594 RID: 1428 RVA: 0x0002BD81 File Offset: 0x00029F81
		public string EmptyFolderWarning
		{
			get
			{
				if (this.IsDeletedItemsFolder || this.IsJunkEmailFolder)
				{
					return string.Format(LocalizedStrings.GetNonEncoded(1984261115), base.FolderName);
				}
				return LocalizedStrings.GetNonEncoded(1984261112);
			}
		}

		// Token: 0x170001C4 RID: 452
		// (get) Token: 0x06000595 RID: 1429 RVA: 0x0002BDB3 File Offset: 0x00029FB3
		public bool IsDraftsFolder
		{
			get
			{
				return Utilities.IsDefaultFolder(base.Folder, DefaultFolderType.Drafts);
			}
		}

		// Token: 0x170001C5 RID: 453
		// (get) Token: 0x06000596 RID: 1430 RVA: 0x0002BDC1 File Offset: 0x00029FC1
		public string ApplicationElement
		{
			get
			{
				return Convert.ToString(base.OwaContext.FormsRegistryContext.ApplicationElement);
			}
		}

		// Token: 0x170001C6 RID: 454
		// (get) Token: 0x06000597 RID: 1431 RVA: 0x0002BDDD File Offset: 0x00029FDD
		public string Type
		{
			get
			{
				if (base.OwaContext.FormsRegistryContext.Type != null)
				{
					return base.OwaContext.FormsRegistryContext.Type;
				}
				return string.Empty;
			}
		}

		// Token: 0x170001C7 RID: 455
		// (get) Token: 0x06000598 RID: 1432 RVA: 0x0002BE07 File Offset: 0x0002A007
		protected override string CheckBoxId
		{
			get
			{
				return "chkmsg";
			}
		}

		// Token: 0x170001C8 RID: 456
		// (get) Token: 0x06000599 RID: 1433 RVA: 0x0002BE0E File Offset: 0x0002A00E
		protected SecondaryNavigationArea SelectedUsing
		{
			get
			{
				return this.selectedUsing;
			}
		}

		// Token: 0x170001C9 RID: 457
		// (get) Token: 0x0600059A RID: 1434 RVA: 0x0002BE16 File Offset: 0x0002A016
		protected bool ShouldShowOofDialog
		{
			get
			{
				return this.shouldShowOofDialog;
			}
		}

		// Token: 0x0600059B RID: 1435 RVA: 0x0002BE20 File Offset: 0x0002A020
		public void RenderMailSecondaryNavigation()
		{
			MailSecondaryNavigation mailSecondaryNavigation = new MailSecondaryNavigation(base.OwaContext, base.Folder.Id.ObjectId, null, null, new SecondaryNavigationArea?(this.selectedUsing));
			mailSecondaryNavigation.Render(base.Response.Output);
		}

		// Token: 0x0600059C RID: 1436 RVA: 0x0002BE68 File Offset: 0x0002A068
		private void UpdateMru(StoreObjectId folderId)
		{
			if (string.CompareOrdinal(Utilities.GetQueryStringParameter(base.Request, "mru", false), "1") == 0)
			{
				string className = base.Folder.ClassName;
				bool flag = true;
				if (string.IsNullOrEmpty(className) || string.CompareOrdinal(className, "IPF.Note") == 0)
				{
					flag = false;
				}
				DefaultFolderType defaultFolderType = Utilities.GetDefaultFolderType(base.Folder);
				if (defaultFolderType == DefaultFolderType.Inbox || defaultFolderType == DefaultFolderType.DeletedItems || defaultFolderType == DefaultFolderType.Drafts || defaultFolderType == DefaultFolderType.JunkEmail || defaultFolderType == DefaultFolderType.SentItems)
				{
					flag = true;
				}
				if (!flag)
				{
					FolderMruCache cacheInstance = FolderMruCache.GetCacheInstance(base.UserContext);
					FolderMruCacheEntry newEntry = new FolderMruCacheEntry(folderId);
					cacheInstance.AddEntry(newEntry);
					cacheInstance.Commit();
				}
			}
		}

		// Token: 0x0600059D RID: 1437 RVA: 0x0002BF00 File Offset: 0x0002A100
		protected override void CreateListView(ColumnId sortColumn, SortOrder sortOrder)
		{
			this.UpdateMru(base.Folder.Id.ObjectId);
			if (!base.FilteredView)
			{
				base.ListView = new MessageListView(base.UserContext, sortColumn, sortOrder, base.Folder);
			}
			else
			{
				base.ListView = new MessageListView(base.UserContext, sortColumn, sortOrder, base.SearchFolder, base.SearchScope);
			}
			base.InitializeListView();
		}

		// Token: 0x0600059E RID: 1438 RVA: 0x0002BF6C File Offset: 0x0002A16C
		protected override SanitizedHtmlString BuildConcretSearchInfobarMessage(int resultsCount, SanitizedHtmlString clearSearchLink)
		{
			if (base.SearchScope == SearchScope.AllFoldersAndItems)
			{
				return SanitizedHtmlString.Format(LocalizedStrings.GetHtmlEncoded(-1390621969), new object[]
				{
					resultsCount,
					base.SearchString,
					clearSearchLink
				});
			}
			return SanitizedHtmlString.Format(LocalizedStrings.GetHtmlEncoded((base.SearchScope == SearchScope.SelectedFolder) ? 609609633 : -1674214459), new object[]
			{
				resultsCount,
				base.SearchString,
				base.Folder.DisplayName,
				clearSearchLink
			});
		}

		// Token: 0x0600059F RID: 1439 RVA: 0x0002BFFC File Offset: 0x0002A1FC
		public void RenderMessageListHeaderToolbar()
		{
			Toolbar toolbar = new Toolbar(base.Response.Output, true);
			toolbar.RenderStart();
			if (this.IsJunkEmailFolder)
			{
				if (base.UserContext.IsJunkEmailEnabled)
				{
					toolbar.RenderButton(ToolbarButtons.NotJunk);
					toolbar.RenderDivider();
				}
			}
			else
			{
				toolbar.RenderButton(ToolbarButtons.NewMessage);
				toolbar.RenderDivider();
			}
			toolbar.RenderButton(ToolbarButtons.Move);
			toolbar.RenderSpace();
			toolbar.RenderButton(ToolbarButtons.Delete);
			toolbar.RenderSpace();
			toolbar.RenderDivider();
			if (!this.IsJunkEmailFolder && base.UserContext.IsJunkEmailEnabled)
			{
				toolbar.RenderButton(ToolbarButtons.Junk);
				toolbar.RenderDivider();
			}
			if (this.IsDeletedItemsFolder)
			{
				string toolTip = string.Format(LocalizedStrings.GetHtmlEncoded(462976341), Utilities.HtmlEncode(base.FolderName));
				toolbar.RenderButton(new ToolbarButton("emptyfolder", ToolbarButtonFlags.Image, 491943887, ThemeFileId.BasicDarkDeleted, toolTip));
				toolbar.RenderDivider();
			}
			else if (this.IsJunkEmailFolder)
			{
				string toolTip2 = string.Format(LocalizedStrings.GetHtmlEncoded(462976341), Utilities.HtmlEncode(base.FolderName));
				toolbar.RenderButton(new ToolbarButton("emptyfolder", ToolbarButtonFlags.ImageAndText, 1628292131, ThemeFileId.BasicDarkDeleted, toolTip2));
			}
			if (!this.IsJunkEmailFolder)
			{
				toolbar.RenderButton(ToolbarButtons.MarkAsRead);
				toolbar.RenderSpace();
				toolbar.RenderButton(ToolbarButtons.MarkAsUnread);
				toolbar.RenderDivider();
				toolbar.RenderButton(ToolbarButtons.CheckMessagesImage);
			}
			toolbar.RenderFill();
			base.RenderPaging(false);
			toolbar.RenderEnd();
		}

		// Token: 0x060005A0 RID: 1440 RVA: 0x0002C170 File Offset: 0x0002A370
		public void RenderMessageListFooterToolbar()
		{
			Toolbar toolbar = new Toolbar(base.Response.Output, false);
			toolbar.RenderStart();
			if (!base.UserContext.IsWebPartRequest)
			{
				toolbar.RenderButton(ToolbarButtons.Move);
				toolbar.RenderSpace();
			}
			toolbar.RenderButton(ToolbarButtons.Delete);
			toolbar.RenderFill();
			base.RenderPaging(true);
			toolbar.RenderEnd();
		}

		// Token: 0x060005A1 RID: 1441 RVA: 0x0002C1D4 File Offset: 0x0002A3D4
		protected void RenderInfobar()
		{
			if (base.Infobar.MessageCount > 0)
			{
				TextWriter output = base.Response.Output;
				output.Write("<tr id=trPwdIB><td class=vwinfbr>");
				base.Infobar.Render(output);
				output.Write("</td></tr>");
			}
		}

		// Token: 0x060005A2 RID: 1442 RVA: 0x0002C220 File Offset: 0x0002A420
		private void CreateExpiringPasswordNotification()
		{
			int num;
			if (!Utilities.ShouldRenderExpiringPasswordInfobar(base.UserContext, out num))
			{
				return;
			}
			SanitizingStringBuilder<OwaHtml> sanitizingStringBuilder = new SanitizingStringBuilder<OwaHtml>();
			sanitizingStringBuilder.Append("<table cellpadding=0 cellspacing=0><tr><td class=tdMvIBSe>");
			if (num == 0)
			{
				sanitizingStringBuilder.Append(LocalizedStrings.GetNonEncoded(352263686));
			}
			else
			{
				sanitizingStringBuilder.AppendFormat(LocalizedStrings.GetHtmlEncoded(-2025544575), new object[]
				{
					num
				});
			}
			sanitizingStringBuilder.Append("</td>");
			sanitizingStringBuilder.Append("<td class=tdMvIBSe><a href=# onClick=\"return onPwdNtf('yes');\">");
			sanitizingStringBuilder.Append(LocalizedStrings.GetNonEncoded(-1273337393));
			sanitizingStringBuilder.Append("</a></td>");
			sanitizingStringBuilder.Append("<td class=tdMvIBSe><a href=# onClick=\"return onPwdNtf('no');\">");
			sanitizingStringBuilder.Append(LocalizedStrings.GetNonEncoded(1496915101));
			sanitizingStringBuilder.Append("</a></td>");
			sanitizingStringBuilder.Append("</tr></table>");
			base.Infobar.AddMessageHtml(sanitizingStringBuilder.ToSanitizedString<SanitizedHtmlString>(), InfobarMessageType.Informational);
		}

		// Token: 0x060005A3 RID: 1443 RVA: 0x0002C2F8 File Offset: 0x0002A4F8
		protected void CreateOutOfOfficeNotification()
		{
			this.shouldShowOofDialog = ((base.UserContext.MailboxSession.Mailbox.TryGetProperty(MailboxSchema.MailboxOofState) as bool?) ?? false);
			if (!this.shouldShowOofDialog || base.UserContext.IsWebPartRequest)
			{
				return;
			}
			UserOofSettings userOofSettings = null;
			try
			{
				userOofSettings = UserOofSettings.GetUserOofSettings(base.UserContext.MailboxSession);
			}
			catch (QuotaExceededException ex)
			{
				ExTraceGlobals.CoreTracer.TraceDebug<string>(0L, "BasicMessageView.CreateOutOfOfficeNotification: Failed. Exception: {0}", ex.Message);
				return;
			}
			switch (userOofSettings.OofState)
			{
			case OofState.Enabled:
				this.shouldShowOofDialog = base.UserContext.MessageViewFirstRender;
				break;
			case OofState.Scheduled:
			{
				this.shouldShowOofDialog = false;
				if (RenderingFlags.HideOutOfOfficeInfoBar(base.UserContext))
				{
					return;
				}
				DateTime utcNow = DateTime.UtcNow;
				DateTime t = DateTime.MinValue;
				DateTime t2 = DateTime.MinValue;
				if (userOofSettings.Duration != null)
				{
					t = userOofSettings.Duration.StartTime;
					t2 = userOofSettings.Duration.EndTime;
				}
				if (utcNow > t && t2 > utcNow)
				{
					ExDateTime exDateTime = new ExDateTime(base.UserContext.TimeZone, userOofSettings.Duration.EndTime);
					SanitizingStringBuilder<OwaHtml> sanitizingStringBuilder = new SanitizingStringBuilder<OwaHtml>();
					sanitizingStringBuilder.AppendFormat(LocalizedStrings.GetHtmlEncoded(-1261886615), new object[]
					{
						exDateTime.ToLongDateString() + " " + exDateTime.ToString(base.UserContext.UserOptions.TimeFormat)
					});
					sanitizingStringBuilder.Append(" <a href=# onclick=\"onClkHdOof()\">");
					sanitizingStringBuilder.Append(LocalizedStrings.GetNonEncoded(1303059585));
					sanitizingStringBuilder.Append("</a>");
					base.Infobar.AddMessageHtml(sanitizingStringBuilder.ToSanitizedString<SanitizedHtmlString>(), InfobarMessageType.Informational);
				}
				return;
			}
			default:
				this.shouldShowOofDialog = false;
				return;
			}
		}

		// Token: 0x060005A4 RID: 1444 RVA: 0x0002C4DC File Offset: 0x0002A6DC
		protected void RenderNoScriptInfobar()
		{
			SanitizedHtmlString noScriptHtml = Utilities.GetNoScriptHtml();
			Infobar infobar = new Infobar();
			infobar.AddMessageHtml(noScriptHtml, InfobarMessageType.Error);
			infobar.Render(base.SanitizingResponse);
		}

		// Token: 0x060005A5 RID: 1445 RVA: 0x0002C509 File Offset: 0x0002A709
		protected override void OnUnload(EventArgs e)
		{
			base.OnUnload(e);
			base.UserContext.MessageViewFirstRender = false;
		}

		// Token: 0x060005A6 RID: 1446 RVA: 0x0002C520 File Offset: 0x0002A720
		protected void RenderOptions(string helpFile)
		{
			OptionsBar optionsBar = new OptionsBar(base.UserContext, base.Response.Output, ObjectClass.IsCalendarFolder(base.Folder.ClassName) ? OptionsBar.SearchModule.Calendar : OptionsBar.SearchModule.Mail, OptionsBar.RenderingFlags.ShowSearchContext, OptionsBar.BuildFolderSearchUrlSuffix(base.UserContext, base.FolderId));
			optionsBar.Render(helpFile);
		}

		// Token: 0x04000423 RID: 1059
		internal const string SelectedUsingQueryStringParameter = "slUsng";

		// Token: 0x04000424 RID: 1060
		private const string MruFlag = "mru";

		// Token: 0x04000425 RID: 1061
		private const string MessageCheckBox = "chkmsg";

		// Token: 0x04000426 RID: 1062
		private SecondaryNavigationArea selectedUsing;

		// Token: 0x04000427 RID: 1063
		private bool shouldShowOofDialog;
	}
}
