using System;
using System.IO;
using Microsoft.Exchange.Clients.Owa.Basic.Controls;
using Microsoft.Exchange.Clients.Owa.Core;
using Microsoft.Exchange.Clients.Owa.Core.Controls;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage;

namespace Microsoft.Exchange.Clients.Owa.Basic
{
	// Token: 0x0200009A RID: 154
	public class FolderManagement : OwaPage
	{
		// Token: 0x1700019A RID: 410
		// (get) Token: 0x06000531 RID: 1329 RVA: 0x00029AC6 File Offset: 0x00027CC6
		protected override bool UseStrictMode
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700019B RID: 411
		// (get) Token: 0x06000532 RID: 1330 RVA: 0x00029AC9 File Offset: 0x00027CC9
		protected NavigationModule Module
		{
			get
			{
				return this.module;
			}
		}

		// Token: 0x1700019C RID: 412
		// (get) Token: 0x06000533 RID: 1331 RVA: 0x00029AD1 File Offset: 0x00027CD1
		protected string BackUrl
		{
			get
			{
				return base.UserContext.LastClientViewState.ToQueryString();
			}
		}

		// Token: 0x1700019D RID: 413
		// (get) Token: 0x06000534 RID: 1332 RVA: 0x00029AE3 File Offset: 0x00027CE3
		public string SelectedFolderId
		{
			get
			{
				return this.selectedFolderId.ToBase64String();
			}
		}

		// Token: 0x1700019E RID: 414
		// (get) Token: 0x06000535 RID: 1333 RVA: 0x00029AF0 File Offset: 0x00027CF0
		public Infobar Infobar
		{
			get
			{
				return this.infobar;
			}
		}

		// Token: 0x06000536 RID: 1334 RVA: 0x00029AF8 File Offset: 0x00027CF8
		private static void RenderLeadingText(Strings.IDs text, TextWriter writer)
		{
			writer.Write("<td class=\"ddtfm\" nowrap>");
			writer.Write(LocalizedStrings.GetHtmlEncoded(text));
			writer.Write("</td>");
		}

		// Token: 0x06000537 RID: 1335 RVA: 0x00029B1C File Offset: 0x00027D1C
		private static void RenderTextInput(string name, string onKeyUpEvent, string onBlurEvent, string onKeyPressEvent, TextWriter writer)
		{
			writer.Write("<td class=\"dd\"><input maxlength=");
			writer.Write(256);
			writer.Write(" type=\"text\" name=\"");
			writer.Write(name);
			writer.Write("\" id=\"");
			writer.Write(name);
			writer.Write("\" class=\"fldnm\" onkeyup=\"");
			writer.Write(onKeyUpEvent);
			writer.Write("\" onblur=\"");
			writer.Write(onBlurEvent);
			writer.Write("\" onkeypress=\"");
			writer.Write(onKeyPressEvent);
			writer.Write("\"></td>");
		}

		// Token: 0x06000538 RID: 1336 RVA: 0x00029BB4 File Offset: 0x00027DB4
		private static void RenderButton(Strings.IDs label, string id, string onClickEvent, TextWriter writer)
		{
			writer.Write("<td class=\"btn\"><a href=\"#\" id=");
			writer.Write(id);
			writer.Write(" class=\"fmbtn fmbtnDis\" onClick=\"");
			writer.Write(onClickEvent);
			writer.Write("\">");
			writer.Write(LocalizedStrings.GetHtmlEncoded(label));
			writer.Write("</a></td>");
		}

		// Token: 0x06000539 RID: 1337 RVA: 0x00029C08 File Offset: 0x00027E08
		protected override void OnLoad(EventArgs e)
		{
			base.OnLoad(e);
			this.folderDropdown = new FolderDropdown(base.UserContext);
			this.selectedFolderId = RequestParser.GetFolderIdFromQueryString(base.Request, false);
			if (this.selectedFolderId == null)
			{
				ModuleViewState moduleViewState = base.UserContext.LastClientViewState as ModuleViewState;
				if (moduleViewState != null)
				{
					this.selectedFolderId = moduleViewState.FolderId;
				}
			}
			if (this.selectedFolderId == null)
			{
				switch (this.module)
				{
				case NavigationModule.Mail:
					this.selectedFolderId = base.UserContext.InboxFolderId;
					break;
				case NavigationModule.Calendar:
					this.selectedFolderId = base.UserContext.CalendarFolderId;
					break;
				case NavigationModule.Contacts:
					this.selectedFolderId = base.UserContext.ContactsFolderId;
					break;
				}
			}
			this.module = RequestParser.GetNavigationModuleFromQueryString(base.Request, NavigationModule.Mail, true);
			if ((this.module == NavigationModule.Calendar && !base.UserContext.IsFeatureEnabled(Feature.Calendar)) || (this.module == NavigationModule.Contacts && !base.UserContext.IsFeatureEnabled(Feature.Contacts)))
			{
				throw new OwaSegmentationException("The " + this.module.ToString() + " feature is disabled");
			}
			this.InitializeFolderList();
		}

		// Token: 0x0600053A RID: 1338 RVA: 0x00029D2F File Offset: 0x00027F2F
		private void InitializeFolderList()
		{
			this.allFolderList = new FolderList(base.UserContext, base.UserContext.MailboxSession, null, 1024, true, null, FolderList.FolderPropertiesInBasic);
		}

		// Token: 0x0600053B RID: 1339 RVA: 0x00029D5C File Offset: 0x00027F5C
		private void RenderTitle(Strings.IDs title, TextWriter writer)
		{
			writer.Write("<tr><td class=\"hdl\" colspan=3>");
			writer.Write(LocalizedStrings.GetHtmlEncoded(title));
			writer.Write("</td></tr>");
			writer.Write("<tr><td colspan=3 class=dvdr>");
			RenderingUtilities.RenderHorizontalDividerForFolderManagerForm(base.UserContext, writer);
			writer.Write("</td></tr>");
		}

		// Token: 0x0600053C RID: 1340 RVA: 0x00029DB0 File Offset: 0x00027FB0
		private void RenderCreateMailFolder(TextWriter writer)
		{
			this.RenderTitle(-1171996716, writer);
			writer.Write("<tr>");
			FolderManagement.RenderLeadingText(1058761412, writer);
			writer.Write("<td class=\"dd\" nowrap><table class=\"tblIcnSel\" cellspacing=0 cellpadding=0><tr><td><img src=\"");
			base.UserContext.RenderThemeFileUrl(writer, ThemeFileId.EMail2Small);
			writer.Write("\" alt=\"\"></td><td class=\"tdNewFldSel\">");
			this.folderDropdown.RenderMailFolderToCreateIn(this.allFolderList, this.selectedFolderId, writer);
			writer.Write("</td></tr></table></td><td class=\"btn\"></td>");
			writer.Write("</tr><tr>");
			FolderManagement.RenderLeadingText(-868987232, writer);
			FolderManagement.RenderTextInput("nnfc", "onKUFNCr()", "onBlurFNCr()", "onKPFNCr(event)", writer);
			FolderManagement.RenderButton(-119614694, "btnCr", "return onClkCr()", writer);
			writer.Write("</tr>");
		}

		// Token: 0x0600053D RID: 1341 RVA: 0x00029E78 File Offset: 0x00028078
		private void RenderRenameMailFolder(TextWriter writer)
		{
			this.RenderTitle(-1362943956, writer);
			writer.Write("<tr>");
			FolderManagement.RenderLeadingText(824414759, writer);
			writer.Write("<td class=\"dd\" nowrap>");
			this.folderDropdown.RenderMailFolderToRename(this.allFolderList, this.selectedFolderId, writer);
			writer.Write("</td><td class=\"btn\"></td>");
			writer.Write("</tr><tr>");
			FolderManagement.RenderLeadingText(437857602, writer);
			FolderManagement.RenderTextInput("nnfr", "onKUFNRn()", "onBlurFNRn()", "return onKPFNRn(event)", writer);
			FolderManagement.RenderButton(461135208, "btnRn", "return onClkRn()", writer);
		}

		// Token: 0x0600053E RID: 1342 RVA: 0x00029F1C File Offset: 0x0002811C
		private void RenderMoveMailFolder(TextWriter writer)
		{
			this.RenderTitle(1847000671, writer);
			writer.Write("<tr>");
			FolderManagement.RenderLeadingText(-1506697407, writer);
			writer.Write("<td class=\"dd\" nowrap>");
			this.folderDropdown.RenderMailFolderToMove(this.allFolderList, this.selectedFolderId, writer);
			writer.Write("</td><td class=\"btn\"></td>");
			writer.Write("</tr><tr>");
			FolderManagement.RenderLeadingText(1349630, writer);
			writer.Write("<td class=\"dd\">");
			this.folderDropdown.RenderMailNewLocationForMove(this.allFolderList, this.selectedFolderId, writer);
			FolderManagement.RenderButton(1414245993, "btnMv", "return onClkMvFld()", writer);
			writer.Write("</tr>");
		}

		// Token: 0x0600053F RID: 1343 RVA: 0x00029FD4 File Offset: 0x000281D4
		private void RenderDeleteFolder(TextWriter writer)
		{
			this.RenderTitle(677440499, writer);
			writer.Write("<tr><td colspan=3 class=info>");
			writer.Write(LocalizedStrings.GetHtmlEncoded(-404811311));
			writer.Write("</td></tr>");
			writer.Write("<tr>");
			FolderManagement.RenderLeadingText(-868987232, writer);
			writer.Write("<td class=\"dd\">");
			this.folderDropdown.RenderFolderToDelete(this.allFolderList, LocalizedStrings.GetHtmlEncoded(283502113), writer, new FolderDropDownFilterDelegate[]
			{
				new FolderDropDownFilterDelegate(this.ExternalFolderFilter)
			});
			FolderManagement.RenderButton(1381996313, "btnDel", "return onClkDel()", writer);
			writer.Write("</tr>");
		}

		// Token: 0x06000540 RID: 1344 RVA: 0x0002A088 File Offset: 0x00028288
		private void RenderCreateCalendarFolder(TextWriter writer)
		{
			this.RenderTitle(-1989251350, writer);
			writer.Write("<tr>");
			FolderManagement.RenderLeadingText(1058761412, writer);
			writer.Write("<td class=\"dfn\" nowrap><img src=\"");
			base.UserContext.RenderThemeFileUrl(writer, ThemeFileId.Appointment);
			writer.Write("\" alt=\"\">");
			using (Folder folder = Folder.Bind(base.UserContext.MailboxSession, base.UserContext.CalendarFolderId, new PropertyDefinition[]
			{
				FolderSchema.DisplayName
			}))
			{
				Utilities.HtmlEncode(folder.DisplayName, writer);
			}
			writer.Write("</td><td class=\"btn\"></td>");
			writer.Write("</tr><tr>");
			FolderManagement.RenderLeadingText(2101032677, writer);
			FolderManagement.RenderTextInput("nnfc", "onKUFNCr()", "onBlurFNCr()", "return onKPFNCr(event)", writer);
			FolderManagement.RenderButton(-119614694, "btnCr", "return onClkCr()", writer);
			writer.Write("</tr>");
		}

		// Token: 0x06000541 RID: 1345 RVA: 0x0002A18C File Offset: 0x0002838C
		private void RenderRenameCalendarFolder(TextWriter writer)
		{
			this.RenderTitle(-2073351774, writer);
			writer.Write("<tr>");
			FolderManagement.RenderLeadingText(824414759, writer);
			writer.Write("<td class=\"dd\">");
			this.folderDropdown.RenderCalendarFolderToRename(this.allFolderList, this.selectedFolderId, writer, new FolderDropDownFilterDelegate[]
			{
				new FolderDropDownFilterDelegate(this.ExternalFolderFilter)
			});
			writer.Write("</td><td class=\"btn\"></td>");
			writer.Write("</tr><tr>");
			FolderManagement.RenderLeadingText(437857602, writer);
			FolderManagement.RenderTextInput("nnfr", "onKUFNRn()", "onBlurFNRn()", "return onKPFNRn(event)", writer);
			FolderManagement.RenderButton(461135208, "btnRn", "return onClkRn()", writer);
			writer.Write("</tr>");
		}

		// Token: 0x06000542 RID: 1346 RVA: 0x0002A250 File Offset: 0x00028450
		private void RenderCreateContactFolder(TextWriter writer)
		{
			this.RenderTitle(1463488076, writer);
			writer.Write("<tr>");
			FolderManagement.RenderLeadingText(1058761412, writer);
			writer.Write("<td class=\"dfn\" nowrap><img src=\"");
			base.UserContext.RenderThemeFileUrl(writer, ThemeFileId.Contact2Small);
			writer.Write("\" alt=\"\">");
			using (Folder folder = Folder.Bind(base.UserContext.MailboxSession, base.UserContext.ContactsFolderId, new PropertyDefinition[]
			{
				FolderSchema.DisplayName
			}))
			{
				Utilities.HtmlEncode(folder.DisplayName, writer);
			}
			writer.Write("</td><td class=\"btn\"></td>");
			writer.Write("</tr><tr>");
			FolderManagement.RenderLeadingText(-868987232, writer);
			FolderManagement.RenderTextInput("nnfc", "onKUFNCr()", "onBlurFNCr()", "return onKPFNCr(event)", writer);
			FolderManagement.RenderButton(-119614694, "btnCr", "return onClkCr()", writer);
			writer.Write("</tr>");
		}

		// Token: 0x06000543 RID: 1347 RVA: 0x0002A354 File Offset: 0x00028554
		private void RenderRenameContactFolder(TextWriter writer)
		{
			this.RenderTitle(1632531764, writer);
			writer.Write("<tr>");
			FolderManagement.RenderLeadingText(824414759, writer);
			writer.Write("<td class=\"dd\">");
			this.folderDropdown.RenderContactFolderToRename(this.allFolderList, this.selectedFolderId, writer);
			writer.Write("</td><td class=\"btn\"></td>");
			writer.Write("</tr><tr>");
			FolderManagement.RenderLeadingText(437857602, writer);
			FolderManagement.RenderTextInput("nnfr", "onKUFNRn()", "onBlurFNRn()", "return onKPFNRn(event)", writer);
			FolderManagement.RenderButton(461135208, "btnRn", "return onClkRn()", writer);
			writer.Write("</tr>");
		}

		// Token: 0x06000544 RID: 1348 RVA: 0x0002A404 File Offset: 0x00028604
		public static void RenderHeaderToolbar(TextWriter writer)
		{
			Toolbar toolbar = new Toolbar(writer, true);
			toolbar.RenderStart();
			toolbar.RenderButton(ToolbarButtons.CloseText);
			toolbar.RenderFill();
			toolbar.RenderButton(ToolbarButtons.CloseImage);
			toolbar.RenderEnd();
		}

		// Token: 0x06000545 RID: 1349 RVA: 0x0002A444 File Offset: 0x00028644
		public static void RenderFooterToolbar(TextWriter writer)
		{
			Toolbar toolbar = new Toolbar(writer, false);
			toolbar.RenderStart();
			toolbar.RenderFill();
			toolbar.RenderEnd();
		}

		// Token: 0x06000546 RID: 1350 RVA: 0x0002A46C File Offset: 0x0002866C
		public void RenderFolderManagementForm(TextWriter writer)
		{
			if (writer == null)
			{
				throw new ArgumentNullException("writer");
			}
			switch (this.Module)
			{
			case NavigationModule.Mail:
				this.RenderCreateMailFolder(writer);
				this.RenderRenameMailFolder(writer);
				this.RenderMoveMailFolder(writer);
				this.RenderDeleteFolder(writer);
				return;
			case NavigationModule.Calendar:
				this.RenderCreateCalendarFolder(writer);
				this.RenderRenameCalendarFolder(writer);
				this.RenderDeleteFolder(writer);
				return;
			case NavigationModule.Contacts:
				this.RenderCreateContactFolder(writer);
				this.RenderRenameContactFolder(writer);
				this.RenderDeleteFolder(writer);
				return;
			default:
				return;
			}
		}

		// Token: 0x06000547 RID: 1351 RVA: 0x0002A4EC File Offset: 0x000286EC
		public void RenderNavigation(TextWriter writer)
		{
			Navigation navigation = new Navigation(this.Module, base.OwaContext, writer);
			navigation.Render();
		}

		// Token: 0x06000548 RID: 1352 RVA: 0x0002A514 File Offset: 0x00028714
		public void RenderSecondaryNavigation(TextWriter writer)
		{
			switch (this.Module)
			{
			case NavigationModule.Mail:
			{
				MailSecondaryNavigation mailSecondaryNavigation = new MailSecondaryNavigation(base.OwaContext, this.selectedFolderId, this.allFolderList, null, null);
				mailSecondaryNavigation.Render(writer);
				return;
			}
			case NavigationModule.Calendar:
			{
				CalendarSecondaryNavigation calendarSecondaryNavigation = new CalendarSecondaryNavigation(base.OwaContext, this.selectedFolderId, null, null);
				calendarSecondaryNavigation.Render(writer);
				return;
			}
			case NavigationModule.Contacts:
			{
				ContactSecondaryNavigation contactSecondaryNavigation = new ContactSecondaryNavigation(base.OwaContext, this.selectedFolderId, null);
				contactSecondaryNavigation.RenderContacts(writer);
				return;
			}
			default:
				throw new ArgumentOutOfRangeException("Module", "The secondary navigation for Module " + this.Module + " is not supported");
			}
		}

		// Token: 0x06000549 RID: 1353 RVA: 0x0002A5CC File Offset: 0x000287CC
		public void RenderOptions(string helpFile)
		{
			OptionsBar optionsBar = new OptionsBar(base.UserContext, base.Response.Output, OptionsBar.SearchModule.None);
			optionsBar.Render(helpFile);
		}

		// Token: 0x0600054A RID: 1354 RVA: 0x0002A5F8 File Offset: 0x000287F8
		protected void RenderJavascriptEncodedInboxFolderId()
		{
			Utilities.JavascriptEncode(base.UserContext.InboxFolderId.ToBase64String(), base.Response.Output);
		}

		// Token: 0x0600054B RID: 1355 RVA: 0x0002A61A File Offset: 0x0002881A
		private bool ExternalFolderFilter(FolderList folderList, StoreObjectId folderId)
		{
			return !Utilities.IsExternalSharedInFolder(folderList.GetFolderProperty(folderId, FolderSchema.ExtendedFolderFlags));
		}

		// Token: 0x040003DA RID: 986
		internal const string FolderToCreateInFormParameter = "ftci";

		// Token: 0x040003DB RID: 987
		internal const string NewNameForCreateFormParameter = "nnfc";

		// Token: 0x040003DC RID: 988
		internal const string FolderToRenameFormParameter = "ftr";

		// Token: 0x040003DD RID: 989
		internal const string NewNameForRenameFormParameter = "nnfr";

		// Token: 0x040003DE RID: 990
		internal const string FolderToMoveFormParameter = "ftm";

		// Token: 0x040003DF RID: 991
		internal const string NewLocationForMoveFormParameter = "nlfm";

		// Token: 0x040003E0 RID: 992
		internal const string FolderToDeleteFormParameter = "ftd";

		// Token: 0x040003E1 RID: 993
		internal const string PermanentDeleteFolderQueryStringParameter = "hd";

		// Token: 0x040003E2 RID: 994
		private NavigationModule module;

		// Token: 0x040003E3 RID: 995
		private StoreObjectId selectedFolderId;

		// Token: 0x040003E4 RID: 996
		private FolderList allFolderList;

		// Token: 0x040003E5 RID: 997
		private FolderDropdown folderDropdown;

		// Token: 0x040003E6 RID: 998
		private Infobar infobar = new Infobar();
	}
}
