using System;
using System.IO;
using System.Text;
using Microsoft.Exchange.Clients.Owa.Basic.Controls;
using Microsoft.Exchange.Clients.Owa.Core;
using Microsoft.Exchange.Clients.Owa.Core.Controls;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage;

namespace Microsoft.Exchange.Clients.Owa.Basic
{
	// Token: 0x0200009F RID: 159
	public class MoveItem : OwaPage, IRegistryOnlyForm
	{
		// Token: 0x170001CA RID: 458
		// (get) Token: 0x060005A7 RID: 1447 RVA: 0x0002C573 File Offset: 0x0002A773
		protected override bool UseStrictMode
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170001CB RID: 459
		// (get) Token: 0x060005A8 RID: 1448 RVA: 0x0002C576 File Offset: 0x0002A776
		public NavigationModule Module
		{
			get
			{
				return this.module;
			}
		}

		// Token: 0x170001CC RID: 460
		// (get) Token: 0x060005A9 RID: 1449 RVA: 0x0002C57E File Offset: 0x0002A77E
		public string Type
		{
			get
			{
				return this.type;
			}
		}

		// Token: 0x170001CD RID: 461
		// (get) Token: 0x060005AA RID: 1450 RVA: 0x0002C586 File Offset: 0x0002A786
		protected string SelectedFolderIdString
		{
			get
			{
				return this.selectedFolderId.ToBase64String();
			}
		}

		// Token: 0x170001CE RID: 462
		// (get) Token: 0x060005AB RID: 1451 RVA: 0x0002C593 File Offset: 0x0002A793
		protected bool ContainMruRadios
		{
			get
			{
				return this.mruFolderList != null && this.mruFolderList.Count != 0;
			}
		}

		// Token: 0x170001CF RID: 463
		// (get) Token: 0x060005AC RID: 1452 RVA: 0x0002C5B0 File Offset: 0x0002A7B0
		protected Infobar Infobar
		{
			get
			{
				return this.infobar;
			}
		}

		// Token: 0x170001D0 RID: 464
		// (get) Token: 0x060005AD RID: 1453 RVA: 0x0002C5B8 File Offset: 0x0002A7B8
		protected string HeaderLabelForMove
		{
			get
			{
				return this.headerLabelForMove;
			}
		}

		// Token: 0x060005AE RID: 1454 RVA: 0x0002C5C0 File Offset: 0x0002A7C0
		private static void RenderIconForItem(Item item, TextWriter writer, UserContext userContext)
		{
			int iconFlag = -1;
			bool isInConflict = false;
			bool isRead = false;
			if (item is MessageItem)
			{
				iconFlag = ItemUtility.GetProperty<int>(item, ItemSchema.IconIndex, -1);
				isInConflict = ItemUtility.GetProperty<bool>(item, MessageItemSchema.MessageInConflict, false);
				isRead = ItemUtility.GetProperty<bool>(item, MessageItemSchema.IsRead, false);
			}
			writer.Write("<img class=\"sI\" alt=\"\" src=\"");
			SmallIconManager.RenderItemIconUrl(writer, userContext, item.ClassName, null, isInConflict, isRead, iconFlag);
			writer.Write("\">");
		}

		// Token: 0x060005AF RID: 1455 RVA: 0x0002C62C File Offset: 0x0002A82C
		protected string GetBackURL()
		{
			if (this.previousFormApplicationElement == ApplicationElement.Item)
			{
				StringBuilder stringBuilder = new StringBuilder(200);
				stringBuilder.Append("?ae=");
				stringBuilder.Append(this.previousFormApplicationElement);
				stringBuilder.Append("&t=");
				stringBuilder.Append(Utilities.UrlEncode(this.type));
				stringBuilder.Append("&id=");
				stringBuilder.Append(Utilities.UrlEncode(this.items[0].Id.ObjectId.ToBase64String()));
				return stringBuilder.ToString();
			}
			return base.UserContext.LastClientViewState.ToQueryString();
		}

		// Token: 0x060005B0 RID: 1456 RVA: 0x0002C6D0 File Offset: 0x0002A8D0
		protected string GetMoveActionURL()
		{
			StringBuilder stringBuilder = new StringBuilder(200);
			stringBuilder.Append("?ae=PreFormAction&a=Move&t=");
			stringBuilder.Append(Utilities.UrlEncode(this.type));
			stringBuilder.Append("&");
			stringBuilder.Append("fid");
			stringBuilder.Append("=");
			stringBuilder.Append(Utilities.UrlEncode(this.SelectedFolderIdString));
			return stringBuilder.ToString();
		}

		// Token: 0x060005B1 RID: 1457 RVA: 0x0002C744 File Offset: 0x0002A944
		protected string GetFolderManagementURL()
		{
			StringBuilder stringBuilder = new StringBuilder(200);
			stringBuilder.Append("?ae=Dialog&t=FolderManagement&m=");
			stringBuilder.Append((int)this.module);
			return stringBuilder.ToString();
		}

		// Token: 0x060005B2 RID: 1458 RVA: 0x0002C77C File Offset: 0x0002A97C
		protected override void OnLoad(EventArgs e)
		{
			base.OnLoad(e);
			this.targetFolderId = RequestParser.GetTargetFolderIdFromQueryString(base.Request, false);
			this.selectedFolderId = RequestParser.GetFolderIdFromQueryString(base.Request, false);
			this.FetchModule();
			this.FetchSelectedItems();
			this.InitializeFolderList();
			this.folderDropDown = new FolderDropdown(base.UserContext);
		}

		// Token: 0x060005B3 RID: 1459 RVA: 0x0002C7D8 File Offset: 0x0002A9D8
		protected void RenderNavigation()
		{
			Navigation navigation = new Navigation(this.module, base.OwaContext, base.Response.Output);
			navigation.Render();
		}

		// Token: 0x060005B4 RID: 1460 RVA: 0x0002C808 File Offset: 0x0002AA08
		protected void RenderSecondaryNavigation()
		{
			switch (this.module)
			{
			case NavigationModule.Mail:
			{
				MailSecondaryNavigation mailSecondaryNavigation = new MailSecondaryNavigation(base.OwaContext, this.selectedFolderId, this.allFolderList, this.mruFolderList, null);
				mailSecondaryNavigation.Render(base.Response.Output);
				return;
			}
			case NavigationModule.Calendar:
				break;
			case NavigationModule.Contacts:
			{
				ContactSecondaryNavigation contactSecondaryNavigation = new ContactSecondaryNavigation(base.OwaContext, this.selectedFolderId, this.contactFolderList);
				contactSecondaryNavigation.RenderContacts(base.Response.Output);
				break;
			}
			default:
				return;
			}
		}

		// Token: 0x060005B5 RID: 1461 RVA: 0x0002C894 File Offset: 0x0002AA94
		protected void RenderHeaderToolbar()
		{
			Toolbar toolbar = new Toolbar(base.Response.Output, true);
			toolbar.RenderStart();
			toolbar.RenderButton(ToolbarButtons.CloseText);
			toolbar.RenderFill();
			toolbar.RenderButton(ToolbarButtons.CloseImage);
			toolbar.RenderEnd();
		}

		// Token: 0x060005B6 RID: 1462 RVA: 0x0002C8DC File Offset: 0x0002AADC
		protected void RenderFooterToolbar()
		{
			Toolbar toolbar = new Toolbar(base.Response.Output, false);
			toolbar.RenderStart();
			toolbar.RenderFill();
			toolbar.RenderEnd();
		}

		// Token: 0x060005B7 RID: 1463 RVA: 0x0002C910 File Offset: 0x0002AB10
		protected void RenderOptions(string helpFile)
		{
			OptionsBar optionsBar = new OptionsBar(base.UserContext, base.Response.Output, OptionsBar.SearchModule.None);
			optionsBar.Render(helpFile);
		}

		// Token: 0x060005B8 RID: 1464 RVA: 0x0002C93C File Offset: 0x0002AB3C
		private void FetchModule()
		{
			this.type = base.OwaContext.FormsRegistryContext.Type;
			this.module = Navigation.GetNavigationModuleFromFolder(base.UserContext, this.selectedFolderId);
			switch (this.module)
			{
			case NavigationModule.Mail:
				this.headerLabelForMove = LocalizedStrings.GetHtmlEncoded(1182470434);
				return;
			case NavigationModule.Contacts:
				if (!base.UserContext.IsFeatureEnabled(Feature.Contacts))
				{
					throw new OwaSegmentationException("Contacts is not enabled");
				}
				this.headerLabelForMove = LocalizedStrings.GetHtmlEncoded(-1217485730);
				return;
			}
			throw new OwaInvalidRequestException("The " + this.module + " module is not supported to move items in Owa Basic");
		}

		// Token: 0x060005B9 RID: 1465 RVA: 0x0002C9EC File Offset: 0x0002ABEC
		private void InitializeFolderList()
		{
			if (this.Module == NavigationModule.Mail)
			{
				this.mruFolderList = new MruFolderList(base.UserContext);
				this.allFolderList = new FolderList(base.UserContext, base.UserContext.MailboxSession, null, 1024, true, null, FolderList.FolderPropertiesInBasic);
				return;
			}
			if (this.Module == NavigationModule.Contacts)
			{
				this.contactFolderList = new ContactFolderList(base.UserContext, this.selectedFolderId);
			}
		}

		// Token: 0x060005BA RID: 1466 RVA: 0x0002CA5C File Offset: 0x0002AC5C
		private void FetchSelectedItems()
		{
			this.previousFormApplicationElement = MoveItemHelper.GetApplicationElementFromStoreType(this.type);
			if (!Utilities.IsPostRequest(base.Request))
			{
				if (this.previousFormApplicationElement != ApplicationElement.Item)
				{
					throw new OwaInvalidRequestException("GET request for move item page can only triggered from reading Item Page");
				}
				this.selectedItemsParameterIn = ParameterIn.QueryString;
				this.selectedItemsParameterName = "id";
			}
			else
			{
				this.selectedItemsParameterIn = ParameterIn.Form;
				if (base.IsPostFromMyself())
				{
					this.selectedItemsParameterName = "hidid";
				}
				else if (this.module == NavigationModule.Mail)
				{
					this.selectedItemsParameterName = "chkmsg";
				}
				else if (this.module == NavigationModule.Contacts)
				{
					this.selectedItemsParameterName = "chkRcpt";
				}
			}
			string text;
			if (this.selectedItemsParameterIn == ParameterIn.QueryString)
			{
				text = Utilities.GetQueryStringParameter(base.Request, this.selectedItemsParameterName, true);
			}
			else
			{
				text = Utilities.GetFormParameter(base.Request, this.selectedItemsParameterName, true);
			}
			if (string.IsNullOrEmpty(text))
			{
				throw new OwaInvalidRequestException("No item is selected to be moved");
			}
			string[] array = text.Split(new char[]
			{
				','
			});
			if (base.UserContext.UserOptions.BasicViewRowCount < array.Length)
			{
				throw new OwaInvalidRequestException("According to the user's option, at most " + base.UserContext.UserOptions.BasicViewRowCount + " items are allow to move at a time");
			}
			this.items = new Item[array.Length];
			PropertyDefinition[] prefetchProperties = new PropertyDefinition[]
			{
				ItemSchema.Id,
				ItemSchema.Subject,
				StoreObjectSchema.ItemClass,
				MessageItemSchema.IsRead,
				ItemSchema.IconIndex,
				MessageItemSchema.MessageInConflict,
				ContactBaseSchema.FileAs
			};
			for (int i = 0; i < array.Length; i++)
			{
				StoreObjectId storeId = Utilities.CreateStoreObjectId(base.UserContext.MailboxSession, array[i]);
				this.items[i] = Utilities.GetItem<Item>(base.UserContext, storeId, prefetchProperties);
			}
		}

		// Token: 0x060005BB RID: 1467 RVA: 0x0002CC24 File Offset: 0x0002AE24
		protected void RenderTargetFolderList(TextWriter writer)
		{
			if (writer == null)
			{
				throw new ArgumentNullException("writer");
			}
			writer.Write("<td class=\"ddt\" nowrap>");
			writer.Write(LocalizedStrings.GetHtmlEncoded(-1166023766));
			writer.Write("</td>");
			writer.Write("<td class=\"dd\" nowrap>");
			writer.Write("<table cellspacing=0 cellpadding=0>");
			bool flag = false;
			if (this.Module == NavigationModule.Mail && this.mruFolderList != null)
			{
				for (int i = 0; i < this.mruFolderList.Count; i++)
				{
					writer.Write("<tr><td class=\"chkb\">");
					writer.Write("<input type=radio name=\"tfId\" onClick=\"return onClkRdo()\" value=\"");
					Utilities.HtmlEncode(this.mruFolderList[i].Id.ToBase64String(), writer);
					writer.Write("\"");
					if (this.targetFolderId != null && this.targetFolderId.Equals(this.mruFolderList[i].Id))
					{
						writer.Write(" checked=true");
						flag = true;
					}
					writer.Write(" id=\"rdomru");
					writer.Write(i + 1);
					writer.Write("\">");
					writer.Write("</td><td class=\"mru\" nowrap><label for=\"rdomru");
					writer.Write(i + 1);
					writer.Write("\"><a href=\"#\" onClick=\"return onClkMru(");
					writer.Write(i + 1);
					writer.Write(")\"><img src=\"");
					SmallIconManager.RenderFolderIconUrl(writer, base.OwaContext.UserContext, null);
					writer.Write("\" alt=\"\">");
					Utilities.CropAndRenderText(writer, this.mruFolderList[i].DisplayName, 24);
					writer.Write("</a></label></td><td></td></tr>");
				}
			}
			writer.Write("<tr><td>");
			string dropdownName = "tfId";
			StoreObjectId storeObjectId = flag ? null : this.targetFolderId;
			switch (this.Module)
			{
			case NavigationModule.Mail:
				if (this.mruFolderList != null && this.mruFolderList.Count != 0)
				{
					writer.Write("<input type=radio name=\"tfId\" id=\"rdofldlst\" onClick=\"return onClkRdo()\" ");
					if (this.targetFolderId != null && !flag)
					{
						writer.Write(" checked");
					}
					writer.Write("></td><td>");
					dropdownName = null;
				}
				if (storeObjectId == null)
				{
					storeObjectId = base.UserContext.InboxFolderId;
				}
				this.folderDropDown.RenderMailMove(this.allFolderList, storeObjectId, dropdownName, writer);
				break;
			case NavigationModule.Calendar:
				if (storeObjectId == null)
				{
					storeObjectId = base.UserContext.CalendarFolderId;
				}
				break;
			case NavigationModule.Contacts:
				if (storeObjectId == null)
				{
					storeObjectId = base.UserContext.ContactsFolderId;
				}
				this.folderDropDown.RenderContactMove(this.contactFolderList, storeObjectId, dropdownName, writer);
				break;
			}
			writer.Write("</td><td class=\"btn\" align=\"left\">");
			bool flag2 = this.mruFolderList == null || this.mruFolderList.Count == 0 || this.targetFolderId != null;
			writer.Write("<a href=\"#\" onClick=\"return onClkMv();\" onKeyPress=\"return onKPMv(event);\" id=\"btnmv\"");
			if (!flag2)
			{
				writer.Write(" class=\"fmbtn fmbtnDis\"");
			}
			else
			{
				writer.Write(" class=\"fmbtn fmbtnEnb\"");
			}
			writer.Write(">");
			writer.Write(LocalizedStrings.GetHtmlEncoded(1414245993));
			writer.Write("</a></td></tr></table></td>");
		}

		// Token: 0x060005BC RID: 1468 RVA: 0x0002CF0C File Offset: 0x0002B10C
		protected void RenderSelectedItems(TextWriter writer)
		{
			if (writer == null)
			{
				throw new ArgumentNullException("writer");
			}
			for (int i = 0; i < this.items.Length; i++)
			{
				if (i != 0)
				{
					writer.Write("; ");
				}
				Item item = this.items[i];
				MoveItem.RenderIconForItem(item, writer, base.UserContext);
				string text;
				if (item is Contact)
				{
					text = ItemUtility.GetProperty<string>(item, ContactBaseSchema.FileAs, string.Empty);
					if (string.IsNullOrEmpty(text))
					{
						text = LocalizedStrings.GetNonEncoded(-808148510);
					}
				}
				else
				{
					text = ItemUtility.GetProperty<string>(item, ItemSchema.Subject, string.Empty);
					if (string.IsNullOrEmpty(text))
					{
						text = LocalizedStrings.GetNonEncoded(730745110);
					}
				}
				writer.Write("&nbsp;");
				Utilities.CropAndRenderText(writer, text, 32);
				writer.Write("<input type=hidden name=\"");
				writer.Write("hidid");
				writer.Write("\" value=\"");
				Utilities.HtmlEncode(item.Id.ObjectId.ToBase64String(), writer);
				writer.Write("\">");
				writer.Write("<input type=hidden name=\"");
				writer.Write("hidt");
				writer.Write("\" value=\"");
				Utilities.HtmlEncode(item.ClassName, writer);
				writer.Write("\">");
			}
		}

		// Token: 0x060005BD RID: 1469 RVA: 0x0002D048 File Offset: 0x0002B248
		protected override void OnUnload(EventArgs e)
		{
			if (this.items == null)
			{
				return;
			}
			for (int i = 0; i < this.items.Length; i++)
			{
				if (this.items[i] != null)
				{
					this.items[i].Dispose();
				}
			}
			base.OnUnload(e);
		}

		// Token: 0x04000428 RID: 1064
		private const string CheckedMessageParameterName = "chkmsg";

		// Token: 0x04000429 RID: 1065
		private const string CheckedContactParameterName = "chkRcpt";

		// Token: 0x0400042A RID: 1066
		private const string ItemIdQueryStringParameterName = "id";

		// Token: 0x0400042B RID: 1067
		private string selectedItemsParameterName;

		// Token: 0x0400042C RID: 1068
		private ParameterIn selectedItemsParameterIn;

		// Token: 0x0400042D RID: 1069
		private NavigationModule module;

		// Token: 0x0400042E RID: 1070
		private ApplicationElement previousFormApplicationElement;

		// Token: 0x0400042F RID: 1071
		private string type;

		// Token: 0x04000430 RID: 1072
		private Item[] items;

		// Token: 0x04000431 RID: 1073
		private StoreObjectId selectedFolderId;

		// Token: 0x04000432 RID: 1074
		private StoreObjectId targetFolderId;

		// Token: 0x04000433 RID: 1075
		private Infobar infobar = new Infobar();

		// Token: 0x04000434 RID: 1076
		private string headerLabelForMove;

		// Token: 0x04000435 RID: 1077
		private MruFolderList mruFolderList;

		// Token: 0x04000436 RID: 1078
		private FolderList allFolderList;

		// Token: 0x04000437 RID: 1079
		private ContactFolderList contactFolderList;

		// Token: 0x04000438 RID: 1080
		private FolderDropdown folderDropDown;
	}
}
