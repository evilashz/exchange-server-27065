using System;
using System.IO;
using Microsoft.Exchange.Clients.Owa.Basic.Controls;
using Microsoft.Exchange.Clients.Owa.Core;
using Microsoft.Exchange.Clients.Owa.Core.Controls;
using Microsoft.Exchange.Clients.Owa.Core.Directory;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Data.Search;
using Microsoft.Exchange.Data.Storage;

namespace Microsoft.Exchange.Clients.Owa.Basic
{
	// Token: 0x0200008D RID: 141
	public class AddressBook : OwaForm
	{
		// Token: 0x17000123 RID: 291
		// (get) Token: 0x06000414 RID: 1044 RVA: 0x00022640 File Offset: 0x00020840
		internal MessageItem Message
		{
			get
			{
				return base.Item as MessageItem;
			}
		}

		// Token: 0x17000124 RID: 292
		// (get) Token: 0x06000415 RID: 1045 RVA: 0x0002264D File Offset: 0x0002084D
		internal CalendarItemBase CalendarItemBase
		{
			get
			{
				return base.Item as CalendarItemBase;
			}
		}

		// Token: 0x17000125 RID: 293
		// (get) Token: 0x06000416 RID: 1046 RVA: 0x0002265A File Offset: 0x0002085A
		protected string MessageId
		{
			get
			{
				if (base.Item != null && base.Item.Id != null)
				{
					return base.Item.Id.ObjectId.ToBase64String();
				}
				return string.Empty;
			}
		}

		// Token: 0x17000126 RID: 294
		// (get) Token: 0x06000417 RID: 1047 RVA: 0x0002268C File Offset: 0x0002088C
		protected int PageNumber
		{
			get
			{
				return this.pageNumber;
			}
		}

		// Token: 0x17000127 RID: 295
		// (get) Token: 0x06000418 RID: 1048 RVA: 0x00022694 File Offset: 0x00020894
		protected string ChangeKey
		{
			get
			{
				if (base.Item != null && base.Item.Id != null)
				{
					return base.Item.Id.ChangeKeyAsBase64String();
				}
				return string.Empty;
			}
		}

		// Token: 0x17000128 RID: 296
		// (get) Token: 0x06000419 RID: 1049 RVA: 0x000226C1 File Offset: 0x000208C1
		protected AddressBook.Mode AddressBookMode
		{
			get
			{
				return this.viewMode;
			}
		}

		// Token: 0x17000129 RID: 297
		// (get) Token: 0x0600041A RID: 1050 RVA: 0x000226C9 File Offset: 0x000208C9
		protected string AddressBookToSearch
		{
			get
			{
				return this.addressBookToSearch;
			}
		}

		// Token: 0x1700012A RID: 298
		// (get) Token: 0x0600041B RID: 1051 RVA: 0x000226D1 File Offset: 0x000208D1
		protected string SearchString
		{
			get
			{
				return this.searchString;
			}
		}

		// Token: 0x1700012B RID: 299
		// (get) Token: 0x0600041C RID: 1052 RVA: 0x000226D9 File Offset: 0x000208D9
		protected ColumnId SortColumnId
		{
			get
			{
				return this.sortColumn;
			}
		}

		// Token: 0x1700012C RID: 300
		// (get) Token: 0x0600041D RID: 1053 RVA: 0x000226E1 File Offset: 0x000208E1
		protected SortOrder SortOrder
		{
			get
			{
				return this.sortOrder;
			}
		}

		// Token: 0x1700012D RID: 301
		// (get) Token: 0x0600041E RID: 1054 RVA: 0x000226E9 File Offset: 0x000208E9
		protected int RecipientWell
		{
			get
			{
				return (int)this.recipientWell;
			}
		}

		// Token: 0x1700012E RID: 302
		// (get) Token: 0x0600041F RID: 1055 RVA: 0x000226F1 File Offset: 0x000208F1
		protected string BackUrl
		{
			get
			{
				return ((AddressBookViewState)base.UserContext.LastClientViewState).PreviousViewState.ToQueryString();
			}
		}

		// Token: 0x06000420 RID: 1056 RVA: 0x00022710 File Offset: 0x00020910
		protected override void OnLoad(EventArgs e)
		{
			base.OnLoad(e);
			string queryStringParameter = Utilities.GetQueryStringParameter(base.Request, "ctx");
			int num;
			if (!int.TryParse(queryStringParameter, out num))
			{
				throw new OwaInvalidRequestException("Context parameter is having invalid format");
			}
			if (num < 0 || num > 4)
			{
				throw new OwaInvalidRequestException("Invalid context value in the querystring parameter");
			}
			this.viewMode = (AddressBook.Mode)num;
			if (this.viewMode == AddressBook.Mode.None)
			{
				this.viewMode = AddressBook.Mode.Lookup;
			}
			if (base.IsPostFromMyself())
			{
				this.action = Utilities.GetFormParameter(base.Request, "hidcmdpst", false);
				this.searchString = Utilities.GetFormParameter(base.Request, "hidss", false);
				this.pageNumber = RequestParser.TryGetIntValueFromForm(base.Request, "hidpg", 1);
				this.sortColumn = (ColumnId)RequestParser.TryGetIntValueFromForm(base.Request, "hidcid", 11);
				this.sortOrder = (SortOrder)RequestParser.TryGetIntValueFromForm(base.Request, "hidso", 1);
				this.addressBookToSearch = Utilities.GetFormParameter(base.Request, "hidAB", false);
				if (string.IsNullOrEmpty(this.addressBookToSearch))
				{
					throw new OwaInvalidRequestException("addressbookGuid can't be null");
				}
				this.recipientWell = (RecipientItemType)RequestParser.TryGetIntValueFromForm(base.Request, "hidrw", 1);
			}
			else
			{
				this.searchString = Utilities.GetQueryStringParameter(base.Request, "sch", false);
				if (!string.IsNullOrEmpty(this.searchString))
				{
					Utilities.VerifySearchCanaryInGetRequest(base.Request);
				}
				this.pageNumber = RequestParser.TryGetIntValueFromQueryString(base.Request, "pg", 1);
				this.sortColumn = (ColumnId)RequestParser.TryGetIntValueFromQueryString(base.Request, "cid", 11);
				this.sortOrder = (SortOrder)RequestParser.TryGetIntValueFromQueryString(base.Request, "so", 1);
				this.addressBookToSearch = Utilities.GetQueryStringParameter(base.Request, "ab", false);
				this.recipientWell = (RecipientItemType)RequestParser.TryGetIntValueFromQueryString(base.Request, "rw", 1);
			}
			this.GetSearchLocation();
			if (AddressBook.IsEditingMode(this.viewMode))
			{
				if (!base.IsPostFromMyself())
				{
					bool required = this.viewMode != AddressBook.Mode.EditCalendar;
					StoreObjectId itemId = QueryStringUtilities.CreateItemStoreObjectId(base.UserContext.MailboxSession, base.Request, required);
					base.Item = AddressBookHelper.GetItem(base.UserContext, this.viewMode, itemId, null);
				}
				else
				{
					StoreObjectId itemId2 = null;
					string formParameter = Utilities.GetFormParameter(base.Request, "hidid", true);
					if (!string.IsNullOrEmpty(formParameter))
					{
						itemId2 = Utilities.CreateStoreObjectId(base.UserContext.MailboxSession, formParameter);
					}
					string formParameter2 = Utilities.GetFormParameter(base.Request, "hidchk", true);
					base.Item = AddressBookHelper.GetItem(base.UserContext, this.viewMode, itemId2, formParameter2);
					string a;
					if ((a = this.action) != null)
					{
						if (!(a == "addrcp"))
						{
							if (a == "rmrcp")
							{
								int intValueFromForm = RequestParser.GetIntValueFromForm(base.Request, "hidri");
								if (this.viewMode == AddressBook.Mode.EditMessage || this.viewMode == AddressBook.Mode.EditMeetingResponse)
								{
									if (intValueFromForm >= 0 && intValueFromForm < this.Message.Recipients.Count)
									{
										this.Message.Recipients.RemoveAt(intValueFromForm);
										AddressBookHelper.SaveItem(base.Item);
									}
								}
								else if (this.viewMode == AddressBook.Mode.EditCalendar)
								{
									CalendarUtilities.RemoveAttendeeAt(this.CalendarItemBase, intValueFromForm);
									EditCalendarItemHelper.CreateUserContextData(base.UserContext, this.CalendarItemBase);
								}
							}
						}
						else
						{
							int num2 = RequestParser.TryGetIntValueFromQueryString(base.Request, "rt", 1);
							if (num2 == 1)
							{
								this.type = RecipientItemType.To;
							}
							else if (num2 == 2)
							{
								this.type = RecipientItemType.Cc;
							}
							else if (num2 == 3)
							{
								this.type = RecipientItemType.Bcc;
							}
							string text = base.Request.Form["chkRcpt"];
							if (!string.IsNullOrEmpty(text))
							{
								this.ids = text.Split(new char[]
								{
									','
								});
								if (this.searchLocation == AddressBook.SearchLocation.AddressBook)
								{
									AddressBookHelper.AddRecipientsToDraft(this.ids, base.Item, this.type, base.UserContext);
								}
								else
								{
									AddressBookHelper.AddContactsToDraft(base.Item, this.type, base.UserContext, this.ids);
								}
							}
						}
					}
				}
			}
			if (!string.IsNullOrEmpty(this.searchString))
			{
				this.searchString = this.searchString.Trim();
				if (this.searchString.Length > Globals.MaxSearchStringLength)
				{
					throw new OwaInvalidRequestException("Search string length is more than 256 characters");
				}
			}
			if (this.pageNumber == 0)
			{
				this.pageNumber = 1;
			}
			this.firstItemOnPage = (this.pageNumber - 1) * base.UserContext.UserOptions.BasicViewRowCount + 1;
			this.lastItemOnPage = this.firstItemOnPage + base.UserContext.UserOptions.BasicViewRowCount - 1;
			this.CreateListView();
			if (AddressBook.IsEditingMode(this.viewMode) || !string.IsNullOrEmpty(this.searchString))
			{
				base.UserContext.LastClientViewState = new AddressBookSearchViewState(base.UserContext.LastClientViewState, this.viewMode, this.addressBookToSearch, this.searchString, this.pageNumber, (base.Item == null || base.Item.Id == null) ? null : base.Item.Id.ObjectId, (base.Item == null || base.Item.Id == null) ? null : base.Item.Id.ChangeKeyAsBase64String(), this.recipientWell, this.sortColumn, this.sortOrder);
				return;
			}
			base.UserContext.LastClientViewState = new AddressBookViewState(base.UserContext.LastClientViewState, this.viewMode, this.pageNumber, (base.Item == null || base.Item.Id == null) ? null : base.Item.Id.ObjectId, (base.Item == null || base.Item.Id == null) ? null : base.Item.Id.ChangeKeyAsBase64String(), this.recipientWell, this.sortColumn, this.sortOrder);
		}

		// Token: 0x06000421 RID: 1057 RVA: 0x00022CEB File Offset: 0x00020EEB
		protected override void OnUnload(EventArgs e)
		{
			if (this.folder != null)
			{
				this.folder.Dispose();
				this.folder = null;
			}
			if (this.searchFolder != null)
			{
				this.searchFolder.Dispose();
				this.searchFolder = null;
			}
			base.OnUnload(e);
		}

		// Token: 0x06000422 RID: 1058 RVA: 0x00022D28 File Offset: 0x00020F28
		private void GetSearchLocation()
		{
			if (string.IsNullOrEmpty(this.searchString) || string.IsNullOrEmpty(this.addressBookToSearch))
			{
				this.addressBookBase = base.UserContext.GlobalAddressListInfo.ToAddressBookBase();
				this.addressBookToSearch = "Ad" + ';' + this.addressBookBase.Base64Guid;
				this.addressBookInfo = new string[]
				{
					"Ad",
					this.addressBookBase.Base64Guid
				};
				return;
			}
			bool flag = false;
			this.addressBookInfo = this.addressBookToSearch.Split(new char[]
			{
				';'
			});
			if (this.addressBookInfo.Length == 2)
			{
				if (string.CompareOrdinal(this.addressBookInfo[0], "Ad") == 0)
				{
					if (!string.IsNullOrEmpty(this.addressBookInfo[1]))
					{
						flag = true;
						this.addressBookBase = DirectoryAssistance.FindAddressBook(this.addressBookInfo[1], base.UserContext);
					}
				}
				else if (string.CompareOrdinal(this.addressBookInfo[0], "Con") == 0)
				{
					flag = true;
					if (string.CompareOrdinal(this.action, "s") == 0)
					{
						this.isNewSearch = false;
					}
					this.searchLocation = AddressBook.SearchLocation.Contacts;
					this.folder = Folder.Bind(base.UserContext.MailboxSession, base.UserContext.ContactsFolderId);
				}
			}
			if (!flag)
			{
				throw new OwaInvalidRequestException("Invalid search location for addressbook");
			}
		}

		// Token: 0x06000423 RID: 1059 RVA: 0x00022E80 File Offset: 0x00021080
		public void CreateListView()
		{
			if (this.searchLocation == AddressBook.SearchLocation.AddressBook)
			{
				this.listView = new AddressBookListView(this.searchString, base.UserContext, ColumnId.DisplayNameAD, SortOrder.Ascending, this.addressBookBase, AddressBookBase.RecipientCategory.All);
			}
			else
			{
				FolderSearch folderSearch = new FolderSearch();
				SearchScope searchScope = base.UserContext.MailboxSession.Mailbox.IsContentIndexingEnabled ? SearchScope.SelectedAndSubfolders : SearchScope.SelectedFolder;
				this.searchFolder = folderSearch.Execute(base.UserContext, this.folder, searchScope, this.searchString, this.isNewSearch, false);
				object obj = this.searchFolder.TryGetProperty(FolderSchema.ItemCount);
				if (!(obj is PropertyError))
				{
					this.itemCount = (int)obj;
				}
				this.listView = new ContactsListView(base.UserContext, this.sortColumn, this.sortOrder, this.searchFolder, searchScope);
			}
			this.listView.Initialize(this.firstItemOnPage, this.lastItemOnPage);
			if (!string.IsNullOrEmpty(this.searchString))
			{
				SanitizedHtmlString sanitizedHtmlString = new SanitizedHtmlString("<a href=\"#\" onclick=\"return onClkClrLnk();\">" + LocalizedStrings.GetHtmlEncoded(1155007962) + "</a>");
				sanitizedHtmlString.DecreeToBeTrusted();
				SanitizedHtmlString messageHtml;
				if (this.listView.TotalCount == 0)
				{
					messageHtml = SanitizedHtmlString.Format(LocalizedStrings.GetHtmlEncoded(-761327948), new object[]
					{
						sanitizedHtmlString
					});
				}
				else if (this.searchLocation == AddressBook.SearchLocation.AddressBook)
				{
					if (this.addressBookBase.Base64Guid == base.UserContext.GlobalAddressListInfo.ToAddressBookBase().Base64Guid)
					{
						messageHtml = SanitizedHtmlString.Format(LocalizedStrings.GetHtmlEncoded(-121508646), new object[]
						{
							this.listView.TotalCount,
							this.searchString,
							sanitizedHtmlString
						});
					}
					else
					{
						messageHtml = SanitizedHtmlString.Format(LocalizedStrings.GetHtmlEncoded(-1725472335), new object[]
						{
							this.listView.TotalCount,
							this.searchString,
							this.addressBookBase.DisplayName,
							sanitizedHtmlString
						});
					}
				}
				else
				{
					messageHtml = SanitizedHtmlString.Format(LocalizedStrings.GetHtmlEncoded(-1403744948), new object[]
					{
						this.listView.TotalCount,
						this.searchString,
						sanitizedHtmlString
					});
				}
				base.Infobar.AddMessageHtml(messageHtml, InfobarMessageType.Informational);
			}
		}

		// Token: 0x06000424 RID: 1060 RVA: 0x000230E0 File Offset: 0x000212E0
		public void RenderAddressBookView(TextWriter writer)
		{
			if (writer == null)
			{
				throw new ArgumentNullException("writer");
			}
			string empty = string.Empty;
			this.listView.Render(writer, empty);
		}

		// Token: 0x06000425 RID: 1061 RVA: 0x00023110 File Offset: 0x00021310
		public void RenderRecipientWell(TextWriter writer)
		{
			if (writer == null)
			{
				throw new ArgumentNullException("writer");
			}
			if (base.Item == null)
			{
				throw new OwaInvalidRequestException("item cannot be null in the people picker mode");
			}
			if (AddressBook.IsEditingMode(this.viewMode))
			{
				writer.Write("<table class=\"rw\" cellpadding=0 cellspacing=0><tr><td class=\"hPd\">");
				Strings.IDs label = (this.viewMode == AddressBook.Mode.EditMessage || this.viewMode == AddressBook.Mode.EditMeetingResponse) ? 932616230 : 1982771038;
				Strings.IDs label2 = (this.viewMode == AddressBook.Mode.EditMessage || this.viewMode == AddressBook.Mode.EditMeetingResponse) ? -876870293 : 1605591873;
				Strings.IDs label3 = (this.viewMode == AddressBook.Mode.EditMessage || this.viewMode == AddressBook.Mode.EditMeetingResponse) ? 125372521 : 1671797350;
				AddressBookHelper.RenderAddressBookButton(writer, "1", label);
				writer.Write("</td><td class=\"pd\"><div class=\"rWll\">");
				if (this.viewMode == AddressBook.Mode.EditMessage || this.viewMode == AddressBook.Mode.EditMeetingResponse)
				{
					AddressBookHelper.RenderRecipients(writer, RecipientItemType.To, this.Message);
				}
				else
				{
					AddressBookHelper.RenderAttendees(writer, AttendeeType.Required, this.CalendarItemBase);
				}
				writer.Write("</div></td></tr><tr><td class=\"hPd\">");
				AddressBookHelper.RenderAddressBookButton(writer, "2", label2);
				writer.Write("</td><td class=\"pd\"><div class=\"rWll\">");
				if (this.viewMode == AddressBook.Mode.EditMessage || this.viewMode == AddressBook.Mode.EditMeetingResponse)
				{
					AddressBookHelper.RenderRecipients(writer, RecipientItemType.Cc, this.Message);
				}
				else
				{
					AddressBookHelper.RenderAttendees(writer, AttendeeType.Optional, this.CalendarItemBase);
				}
				writer.Write("</div></td></tr><tr><td class=\"hPd\">");
				AddressBookHelper.RenderAddressBookButton(writer, "3", label3);
				writer.Write("</td><td class=\"pd\"><div class=\"rWll\">");
				if (this.viewMode == AddressBook.Mode.EditMessage || this.viewMode == AddressBook.Mode.EditMeetingResponse)
				{
					AddressBookHelper.RenderRecipients(writer, RecipientItemType.Bcc, this.Message);
				}
				else
				{
					AddressBookHelper.RenderAttendees(writer, AttendeeType.Resource, this.CalendarItemBase);
				}
				writer.Write("</div></td></tr><tr><td colspan=2 class=\"spc\"></td></tr></table>");
			}
		}

		// Token: 0x06000426 RID: 1062 RVA: 0x000232A8 File Offset: 0x000214A8
		public void RenderNavigation()
		{
			Navigation navigation = null;
			if (this.viewMode == AddressBook.Mode.Lookup)
			{
				navigation = new Navigation(NavigationModule.AddressBook, base.OwaContext, base.Response.Output);
			}
			else if (this.viewMode == AddressBook.Mode.EditMessage || this.viewMode == AddressBook.Mode.EditMeetingResponse)
			{
				navigation = new Navigation(NavigationModule.Mail, base.OwaContext, base.Response.Output);
			}
			else if (this.viewMode == AddressBook.Mode.EditCalendar)
			{
				navigation = new Navigation(NavigationModule.Calendar, base.OwaContext, base.Response.Output);
			}
			if (navigation != null)
			{
				navigation.Render();
			}
		}

		// Token: 0x06000427 RID: 1063 RVA: 0x00023330 File Offset: 0x00021530
		protected override void RenderOptions(string helpFile)
		{
			OptionsBar.SearchModule searchModule = OptionsBar.SearchModule.None;
			OptionsBar.RenderingFlags renderingFlags = OptionsBar.RenderingFlags.AddressBookSelected | OptionsBar.RenderingFlags.ShowSearchContext;
			string searchUrlSuffix = null;
			if (AddressBook.IsEditingMode(this.viewMode))
			{
				searchModule = OptionsBar.SearchModule.PeoplePicker;
				searchUrlSuffix = OptionsBar.BuildPeoplePickerSearchUrlSuffix(this.viewMode, this.MessageId, this.recipientWell);
			}
			OptionsBar optionsBar = new OptionsBar(base.UserContext, base.Response.Output, searchModule, renderingFlags, searchUrlSuffix);
			optionsBar.Render(helpFile);
		}

		// Token: 0x06000428 RID: 1064 RVA: 0x0002338C File Offset: 0x0002158C
		public void RenderSecondaryNavigation(TextWriter writer)
		{
			if (writer == null)
			{
				throw new ArgumentNullException("writer");
			}
			writer.Write("<table class=\"snt\"><tr><td class=\"absn\">");
			writer.Write(LocalizedStrings.GetHtmlEncoded(-454140714));
			writer.Write("</td></tr>");
			if (AddressBook.IsEditingMode(this.viewMode))
			{
				writer.Write("<tr><td class=\"absn\">");
				if (this.viewMode == AddressBook.Mode.EditCalendar)
				{
					writer.Write(LocalizedStrings.GetHtmlEncoded(-611516900));
				}
				else
				{
					writer.Write(LocalizedStrings.GetHtmlEncoded(72613029));
				}
				writer.Write("</td></tr>");
			}
			writer.Write("</table>");
		}

		// Token: 0x06000429 RID: 1065 RVA: 0x00023428 File Offset: 0x00021628
		public void RenderHeaderToolbar()
		{
			Toolbar toolbar = new Toolbar(base.Response.Output, true);
			toolbar.RenderStart();
			switch (this.viewMode)
			{
			case AddressBook.Mode.Lookup:
				toolbar.RenderButton(ToolbarButtons.SendEmail);
				if (base.UserContext.IsFeatureEnabled(Feature.Calendar))
				{
					toolbar.RenderButton(ToolbarButtons.SendMeetingRequest);
					toolbar.RenderDivider();
				}
				if (this.searchLocation == AddressBook.SearchLocation.AddressBook && base.UserContext.IsFeatureEnabled(Feature.Contacts))
				{
					toolbar.RenderButton(ToolbarButtons.AddToContacts);
					toolbar.RenderDivider();
				}
				toolbar.RenderButton(ToolbarButtons.CloseText);
				break;
			case AddressBook.Mode.EditMessage:
			case AddressBook.Mode.EditCalendar:
			case AddressBook.Mode.EditMeetingResponse:
				toolbar.RenderButton(ToolbarButtons.MessageRecipients);
				toolbar.RenderButton(ToolbarButtons.Done);
				toolbar.RenderDivider();
				toolbar.RenderButton(ToolbarButtons.CloseText);
				break;
			}
			toolbar.RenderFill();
			this.RenderPaging(false);
			toolbar.RenderSpace();
			toolbar.RenderButton(ToolbarButtons.CloseImage);
			toolbar.RenderEnd();
		}

		// Token: 0x0600042A RID: 1066 RVA: 0x0002351C File Offset: 0x0002171C
		public void RenderFooterToolbar()
		{
			Toolbar toolbar = new Toolbar(base.Response.Output, false);
			toolbar.RenderStart();
			switch (this.viewMode)
			{
			case AddressBook.Mode.Lookup:
				toolbar.RenderFill();
				break;
			case AddressBook.Mode.EditMessage:
			case AddressBook.Mode.EditCalendar:
			case AddressBook.Mode.EditMeetingResponse:
				toolbar.RenderButton(ToolbarButtons.Done);
				toolbar.RenderDivider();
				toolbar.RenderButton(ToolbarButtons.CloseText);
				toolbar.RenderFill();
				break;
			}
			this.RenderPaging(true);
			toolbar.RenderEnd();
		}

		// Token: 0x0600042B RID: 1067 RVA: 0x0002359C File Offset: 0x0002179C
		protected void RenderPaging(bool renderPageNumbers)
		{
			if (this.searchLocation == AddressBook.SearchLocation.AddressBook)
			{
				if (renderPageNumbers)
				{
					this.listView.RenderADContentsPaging(base.Response.Output, this.pageNumber);
					return;
				}
				this.listView.RenderADContentsHeaderPaging(base.Response.Output, this.pageNumber);
				return;
			}
			else
			{
				if (this.pageNumber == 0)
				{
					this.pageNumber = 1;
				}
				int totalNumberOfPages;
				if (this.itemCount % base.UserContext.UserOptions.BasicViewRowCount == 0)
				{
					totalNumberOfPages = this.itemCount / base.UserContext.UserOptions.BasicViewRowCount;
				}
				else
				{
					totalNumberOfPages = this.itemCount / base.UserContext.UserOptions.BasicViewRowCount + 1;
				}
				if (renderPageNumbers)
				{
					ListView.RenderPageNumbers(base.Response.Output, this.pageNumber, totalNumberOfPages);
					base.Response.Write("<td>&nbsp;</td>");
					this.listView.RenderPagingControls(base.Response.Output, this.pageNumber, totalNumberOfPages);
					return;
				}
				this.listView.RenderHeaderPagingControls(base.Response.Output, this.pageNumber, totalNumberOfPages);
				return;
			}
		}

		// Token: 0x0600042C RID: 1068 RVA: 0x000236B4 File Offset: 0x000218B4
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

		// Token: 0x0600042D RID: 1069 RVA: 0x000236FD File Offset: 0x000218FD
		internal static bool IsEditingMode(AddressBook.Mode addressBookMode)
		{
			return addressBookMode == AddressBook.Mode.EditMessage || addressBookMode == AddressBook.Mode.EditCalendar || addressBookMode == AddressBook.Mode.EditMeetingResponse;
		}

		// Token: 0x04000345 RID: 837
		internal const string ViewContextQueryStringParameter = "ctx";

		// Token: 0x04000346 RID: 838
		internal const string SearchStringQueryStringParameter = "sch";

		// Token: 0x04000347 RID: 839
		internal const string SearchLocationQueryStringParameter = "ab";

		// Token: 0x04000348 RID: 840
		internal const string ColumnSortOrderQueryStringParameter = "so";

		// Token: 0x04000349 RID: 841
		internal const string SortColumnIdQueryStringParameter = "cid";

		// Token: 0x0400034A RID: 842
		internal const string PageNumberQueryStringParameter = "pg";

		// Token: 0x0400034B RID: 843
		internal const string RecipientWellQueryStringParameter = "rw";

		// Token: 0x0400034C RID: 844
		internal const string ContactSearchLocation = "Con";

		// Token: 0x0400034D RID: 845
		internal const string ADSearchLocation = "Ad";

		// Token: 0x0400034E RID: 846
		internal const char SearchLocationDelimiter = ';';

		// Token: 0x0400034F RID: 847
		private const string CommandFormParameter = "hidcmdpst";

		// Token: 0x04000350 RID: 848
		private const string SortCommand = "s";

		// Token: 0x04000351 RID: 849
		private const string RecipientTypeQueryStringParameter = "rt";

		// Token: 0x04000352 RID: 850
		private const string RecipientCheckBox = "chkRcpt";

		// Token: 0x04000353 RID: 851
		private const string SerachLocationFormParameter = "hidAB";

		// Token: 0x04000354 RID: 852
		private const string MessageIdFormParameter = "hidid";

		// Token: 0x04000355 RID: 853
		private const string ColumnSortOrderFormParameter = "hidso";

		// Token: 0x04000356 RID: 854
		private const string SortColumnIdFormParameter = "hidcid";

		// Token: 0x04000357 RID: 855
		private const string RecipientIndexValue = "hidri";

		// Token: 0x04000358 RID: 856
		private const string SearchStringInFormParameter = "hidss";

		// Token: 0x04000359 RID: 857
		private const string PageNumberFromParameter = "hidpg";

		// Token: 0x0400035A RID: 858
		private const string ChangeKeyString = "hidchk";

		// Token: 0x0400035B RID: 859
		private const string RecipientWellString = "hidrw";

		// Token: 0x0400035C RID: 860
		private Folder folder;

		// Token: 0x0400035D RID: 861
		private Folder searchFolder;

		// Token: 0x0400035E RID: 862
		private ListView listView;

		// Token: 0x0400035F RID: 863
		private AddressBookBase addressBookBase;

		// Token: 0x04000360 RID: 864
		private ColumnId sortColumn = ColumnId.FileAs;

		// Token: 0x04000361 RID: 865
		private SortOrder sortOrder = SortOrder.Descending;

		// Token: 0x04000362 RID: 866
		private string addressBookToSearch;

		// Token: 0x04000363 RID: 867
		private string action;

		// Token: 0x04000364 RID: 868
		private string[] ids;

		// Token: 0x04000365 RID: 869
		private string[] addressBookInfo;

		// Token: 0x04000366 RID: 870
		private string searchString = LocalizedStrings.GetNonEncoded(-903656651);

		// Token: 0x04000367 RID: 871
		private bool isNewSearch = true;

		// Token: 0x04000368 RID: 872
		private AddressBook.Mode viewMode = AddressBook.Mode.Lookup;

		// Token: 0x04000369 RID: 873
		private RecipientItemType type = RecipientItemType.To;

		// Token: 0x0400036A RID: 874
		private RecipientItemType recipientWell = RecipientItemType.To;

		// Token: 0x0400036B RID: 875
		private int itemCount;

		// Token: 0x0400036C RID: 876
		private int pageNumber;

		// Token: 0x0400036D RID: 877
		private int firstItemOnPage;

		// Token: 0x0400036E RID: 878
		private int lastItemOnPage;

		// Token: 0x0400036F RID: 879
		private AddressBook.SearchLocation searchLocation;

		// Token: 0x0200008E RID: 142
		public enum Mode
		{
			// Token: 0x04000371 RID: 881
			None,
			// Token: 0x04000372 RID: 882
			Lookup,
			// Token: 0x04000373 RID: 883
			EditMessage,
			// Token: 0x04000374 RID: 884
			EditCalendar,
			// Token: 0x04000375 RID: 885
			EditMeetingResponse,
			// Token: 0x04000376 RID: 886
			LastMode = 4
		}

		// Token: 0x0200008F RID: 143
		private enum SearchLocation
		{
			// Token: 0x04000378 RID: 888
			AddressBook,
			// Token: 0x04000379 RID: 889
			Contacts
		}
	}
}
