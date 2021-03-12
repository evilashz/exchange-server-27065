using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.Exchange.Clients.Owa.Core;
using Microsoft.Exchange.Clients.Owa.Core.Controls;
using Microsoft.Exchange.Clients.Owa.Core.Directory;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage;

namespace Microsoft.Exchange.Clients.Owa.Premium.Controls
{
	// Token: 0x020003CF RID: 975
	public abstract class NavigationHost : OwaPage
	{
		// Token: 0x170009A5 RID: 2469
		// (get) Token: 0x0600242B RID: 9259 RVA: 0x000D0A8D File Offset: 0x000CEC8D
		protected NavigationModule NavigationModule
		{
			get
			{
				return this.navigationModule;
			}
		}

		// Token: 0x170009A6 RID: 2470
		// (get) Token: 0x0600242C RID: 9260 RVA: 0x000D0A95 File Offset: 0x000CEC95
		protected List<OwaSubPage> ChildSubPages
		{
			get
			{
				if (this.childSubPages == null)
				{
					this.childSubPages = new List<OwaSubPage>();
				}
				return this.childSubPages;
			}
		}

		// Token: 0x170009A7 RID: 2471
		// (get) Token: 0x0600242D RID: 9261 RVA: 0x000D0AB0 File Offset: 0x000CECB0
		protected virtual IEnumerable<string> ExternalScriptFiles
		{
			get
			{
				return new string[]
				{
					"startpage.js"
				};
			}
		}

		// Token: 0x170009A8 RID: 2472
		// (get) Token: 0x0600242E RID: 9262 RVA: 0x000D0DAC File Offset: 0x000CEFAC
		public IEnumerable<string> ExternalScriptFilesIncludeChildSubPages
		{
			get
			{
				foreach (string scriptFile in this.ExternalScriptFiles)
				{
					yield return scriptFile;
				}
				foreach (OwaSubPage owaSubPage in this.ChildSubPages)
				{
					foreach (string scriptFile2 in owaSubPage.ExternalScriptFilesIncludeChildSubPages)
					{
						yield return scriptFile2;
					}
				}
				yield break;
			}
		}

		// Token: 0x0600242F RID: 9263
		protected abstract NavigationModule SelectNavagationModule();

		// Token: 0x06002430 RID: 9264 RVA: 0x000D0DCC File Offset: 0x000CEFCC
		protected override void OnInit(EventArgs e)
		{
			base.OnInit(e);
			this.lastModuleContainerId = base.UserContext.InboxFolderId.ToBase64String();
			this.navigationModule = this.SelectNavagationModule();
			this.lastModuleName = this.navigationModule.ToString();
			switch (this.navigationModule)
			{
			case NavigationModule.Mail:
				this.lastModuleApplicationElement = "Folder";
				this.lastModuleContentClass = "IPF.Note";
				this.lastModuleContainerId = base.UserContext.InboxFolderId.ToBase64String();
				this.lastMailFolderId = Utilities.GetQueryStringParameter(base.Request, "id", false);
				goto IL_342;
			case NavigationModule.Calendar:
				if (!base.UserContext.IsFeatureEnabled(Feature.Calendar))
				{
					throw new OwaSegmentationException("Calendar is disabled");
				}
				this.lastModuleApplicationElement = "Folder";
				this.lastModuleContentClass = "IPF.Appointment";
				this.lastModuleContainerId = base.UserContext.CalendarFolderId.ToBase64String();
				goto IL_342;
			case NavigationModule.Contacts:
				if (!base.UserContext.IsFeatureEnabled(Feature.Contacts))
				{
					throw new OwaSegmentationException("Contacts feature is disabled");
				}
				this.lastModuleApplicationElement = "Folder";
				this.lastModuleContentClass = "IPF.Contact";
				this.lastModuleContainerId = base.UserContext.ContactsFolderId.ToBase64String();
				goto IL_342;
			case NavigationModule.Tasks:
				if (!base.UserContext.IsFeatureEnabled(Feature.Tasks))
				{
					throw new OwaSegmentationException("Tasks are disabled");
				}
				this.lastModuleContentClass = "IPF.Task";
				this.lastModuleContainerId = base.UserContext.FlaggedItemsAndTasksFolderId.ToBase64String();
				goto IL_342;
			case NavigationModule.AddressBook:
			{
				if (!(this is AddressBook))
				{
					throw new OwaInvalidRequestException("Invalid navigation module value.");
				}
				if (base.UserContext.IsFeatureEnabled(Feature.GlobalAddressList))
				{
					this.lastModuleApplicationElement = "AddressList";
					this.recipientBlockType = ((AddressBook)this).RecipientBlockType;
					if (((AddressBook)this).IsRoomPicker && base.UserContext.AllRoomsAddressBookInfo != null && !base.UserContext.AllRoomsAddressBookInfo.IsEmpty)
					{
						this.lastModuleContentClass = "Rooms";
						this.lastModuleContainerId = base.UserContext.AllRoomsAddressBookInfo.ToBase64String();
					}
					else
					{
						this.lastModuleContentClass = "Recipients";
						this.lastModuleContainerId = base.UserContext.GlobalAddressListInfo.ToBase64String();
					}
					this.lastModuleName = this.lastModuleContentClass;
					goto IL_342;
				}
				this.lastModuleApplicationElement = "Folder";
				this.lastModuleContentClass = "IPF.Contact";
				this.lastModuleContainerId = base.UserContext.ContactsFolderId.ToBase64String();
				bool isPicker = ((AddressBook)this).IsPicker;
				if (isPicker)
				{
					this.lastModuleState = "AddressBookPicker";
					goto IL_342;
				}
				this.lastModuleState = "AddressBookBrowse";
				goto IL_342;
			}
			case NavigationModule.Documents:
				throw new OwaInvalidRequestException("Invalid navigation module value.");
			case NavigationModule.PublicFolders:
				if (!base.UserContext.IsFeatureEnabled(Feature.PublicFolders))
				{
					throw new OwaSegmentationException("Public Folders are disabled");
				}
				this.lastModuleApplicationElement = "Folder";
				this.lastModuleContentClass = "IPF.Note";
				this.lastModuleContainerId = (base.UserContext.TryGetPublicFolderRootIdString() ?? string.Empty);
				this.lastMailFolderId = this.lastModuleContainerId;
				goto IL_342;
			}
			this.lastModuleContentClass = "IPF.Note";
			this.lastModuleContainerId = base.UserContext.InboxFolderId.ToBase64String();
			this.lastModuleName = NavigationModule.Mail.ToString();
			IL_342:
			if (this.viewPlaceHolder != null)
			{
				this.InitializeView(this.viewPlaceHolder);
			}
		}

		// Token: 0x06002431 RID: 9265 RVA: 0x000D112F File Offset: 0x000CF32F
		protected void RenderSecondaryNavigation(TextWriter output)
		{
			this.RenderSecondaryNavigation(output, base.UserContext.IsFeatureEnabled(Feature.Contacts));
		}

		// Token: 0x06002432 RID: 9266 RVA: 0x000D1148 File Offset: 0x000CF348
		protected void RenderSecondaryNavigation(TextWriter output, bool showContacts)
		{
			if (output == null)
			{
				throw new ArgumentNullException("output");
			}
			output.Write("<div id=\"{0}\" class=\"secNvPaneCont\">", this.lastModuleName);
			switch (this.navigationModule)
			{
			case NavigationModule.Mail:
				NavigationHost.RenderMailSecondaryNavigation(output, base.UserContext);
				goto IL_229;
			case NavigationModule.Calendar:
			{
				PropertyDefinition[] propsToReturn = new PropertyDefinition[]
				{
					ViewStateProperties.CalendarViewType,
					ViewStateProperties.DailyViewDays
				};
				using (CalendarFolder calendarFolder = CalendarFolder.Bind(base.UserContext.MailboxSession, DefaultFolderType.Calendar, propsToReturn))
				{
					DailyView.RenderSecondaryNavigation(output, calendarFolder, base.UserContext);
					goto IL_229;
				}
				break;
			}
			case NavigationModule.Contacts:
				break;
			case NavigationModule.Tasks:
				TaskView.RenderSecondaryNavigation(output, base.UserContext);
				goto IL_229;
			case NavigationModule.Options:
				goto IL_21D;
			case NavigationModule.AddressBook:
				this.recipientBlockType = ((AddressBook)this).RecipientBlockType;
				if (base.UserContext.IsFeatureEnabled(Feature.GlobalAddressList))
				{
					bool isRoomPicker = ((AddressBook)this).IsRoomPicker && DirectoryAssistance.IsRoomsAddressListAvailable(base.UserContext);
					output.Write("<div class=\"abNavPane\" style=\"height:");
					output.Write(showContacts ? "30" : "100");
					output.Write("%;top:0px;\"><div id=\"divMdNmAD\">");
					output.Write(LocalizedStrings.GetHtmlEncoded(346766088));
					output.Write("</div><div id=\"divSecNvAD\">");
					DirectoryView.RenderSecondaryNavigation(output, base.UserContext, isRoomPicker);
					output.Write("</div></div>");
				}
				if (showContacts)
				{
					output.Write("<div class=\"abNavPane\" style=\"height:");
					output.Write(base.UserContext.IsFeatureEnabled(Feature.GlobalAddressList) ? "70" : "100");
					output.Write("%;bottom:0px;\"><div id=\"divMdNmC\">");
					output.Write(LocalizedStrings.GetHtmlEncoded(-1165546057));
					output.Write("</div><div id=\"divSecNvC\"");
					bool isPicker = ((AddressBook)this).IsPicker;
					if (isPicker)
					{
						output.Write(" class=\"noFltrsCntRg\"");
					}
					output.Write(">");
					ContactView.RenderSecondaryNavigation(output, base.UserContext, isPicker);
					output.Write("</div></div>");
					goto IL_229;
				}
				goto IL_229;
			case NavigationModule.Documents:
				DocumentLibraryUtilities.RenderSecondaryNavigation(output, base.UserContext);
				goto IL_229;
			case NavigationModule.PublicFolders:
				NavigationHost.RenderPublicFolderSecondaryNavigation(output, base.UserContext);
				goto IL_229;
			default:
				goto IL_21D;
			}
			ContactView.RenderSecondaryNavigation(output, base.UserContext, false);
			goto IL_229;
			IL_21D:
			NavigationHost.RenderMailSecondaryNavigation(output, base.UserContext);
			IL_229:
			output.Write("</div>");
		}

		// Token: 0x06002433 RID: 9267 RVA: 0x000D139C File Offset: 0x000CF59C
		internal static void RenderPublicFolderSecondaryNavigation(TextWriter output, UserContext userContext)
		{
			if (output == null)
			{
				throw new ArgumentNullException("output");
			}
			if (userContext == null)
			{
				throw new ArgumentNullException("userContext");
			}
			output.Write("<div id=\"divPFFlt\"></div>");
			PublicFolderTree publicFolderTree = PublicFolderTree.CreatePublicFolderRootTree(userContext);
			ContextMenu contextMenu = new PublicFolderTreeContextMenu(userContext);
			output.Write("<div id=\"divPFTrR\">");
			Infobar infobar = new Infobar("divErrPF", "infobar");
			infobar.Render(output);
			NavigationHost.RenderTreeRegionDivStart(output, null);
			NavigationHost.RenderTreeDivStart(output, "publictree");
			publicFolderTree.ErrDiv = "divErrPF";
			publicFolderTree.Render(output);
			NavigationHost.RenderTreeDivEnd(output);
			NavigationHost.RenderTreeRegionDivEnd(output);
			contextMenu.Render(output);
			output.Write("</div>");
		}

		// Token: 0x06002434 RID: 9268 RVA: 0x000D1444 File Offset: 0x000CF644
		internal static void RenderMailSecondaryNavigation(TextWriter output, UserContext userContext)
		{
			if (output == null)
			{
				throw new ArgumentNullException("output");
			}
			if (userContext == null)
			{
				throw new ArgumentNullException("userContext");
			}
			output.Write("<div id=\"divMTrR\">");
			Infobar infobar = new Infobar("divErrMail", "infobar");
			infobar.Render(output);
			NavigationHost.RenderTreeRegionDivStart(output, null);
			NavigationTree navigationTree;
			MailboxFolderTree mailboxFolderTree;
			MailboxFolderTree[] array;
			bool expandBuddyList;
			NavigationTree.CreateFavoriteAndMailboxTreeAndGetBuddyListStatus(userContext, out navigationTree, out mailboxFolderTree, out array, out expandBuddyList);
			NavigationHost.RenderTreeDivStart(output, "favTr");
			navigationTree.ErrDiv = "divErrMail";
			navigationTree.Render(output);
			NavigationHost.RenderTreeDivEnd(output);
			NavigationHost.RenderTreeDivStart(output, "mailtree");
			mailboxFolderTree.ErrDiv = "divErrMail";
			mailboxFolderTree.Render(output);
			NavigationHost.RenderTreeDivEnd(output);
			if (!userContext.IsExplicitLogon)
			{
				if (userContext.HasArchive)
				{
					NavigationHost.RenderTreeDivStart(output, "archivetree", "othTr");
					MailboxFolderTree mailboxFolderTree2 = MailboxFolderTree.CreateStartPageDummyArchiveMailboxFolderTree(userContext);
					mailboxFolderTree2.ErrDiv = "divErrMail";
					mailboxFolderTree2.Render(output);
					NavigationHost.RenderTreeDivEnd(output);
				}
				foreach (OtherMailboxConfigEntry entry in OtherMailboxConfiguration.GetOtherMailboxes(userContext))
				{
					NavigationHost.RenderOtherMailboxFolderTree(output, userContext, entry, false);
				}
			}
			if (userContext.IsInstantMessageEnabled())
			{
				NavigationHost.RenderTreeDivStart(output, "buddytree");
				NavigationHost.RenderBuddyListTreeControl(output, userContext, expandBuddyList);
				NavigationHost.RenderTreeDivEnd(output);
			}
			NavigationHost.RenderTreeRegionDivEnd(output);
			output.Write("</div>");
			ContextMenu contextMenu = new FolderTreeContextMenu(userContext);
			contextMenu.Render(output);
			if (userContext.IsInstantMessageEnabled())
			{
				ContextMenu contextMenu2 = new BuddyTreeContextMenu(userContext);
				contextMenu2.Render(output);
			}
		}

		// Token: 0x06002435 RID: 9269 RVA: 0x000D15D4 File Offset: 0x000CF7D4
		internal static void RenderOtherMailboxFolderTree(TextWriter writer, UserContext userContext, OtherMailboxConfigEntry entry, bool isExpanded)
		{
			MailboxFolderTree mailboxFolderTree = MailboxFolderTree.CreateOtherMailboxFolderTree(userContext, entry, isExpanded);
			if (mailboxFolderTree != null)
			{
				NavigationHost.RenderTreeDivStart(writer, "t" + Convert.ToBase64String(Encoding.UTF8.GetBytes(mailboxFolderTree.RootNode.FolderId.MailboxOwnerLegacyDN)), "othTr");
				mailboxFolderTree.ErrDiv = "divErrMail";
				mailboxFolderTree.Render(writer);
				NavigationHost.RenderTreeDivEnd(writer);
			}
		}

		// Token: 0x06002436 RID: 9270 RVA: 0x000D163C File Offset: 0x000CF83C
		public static void RenderTreeRegionDivStart(TextWriter output, string treeRegionId)
		{
			output.Write("<div");
			if (!string.IsNullOrEmpty(treeRegionId))
			{
				output.Write(" id=\"");
				output.Write(treeRegionId);
				output.Write("\"");
			}
			output.Write(" class=\"trRgO\">");
			output.Write("<div class=\"trRgI\">");
		}

		// Token: 0x06002437 RID: 9271 RVA: 0x000D168F File Offset: 0x000CF88F
		public static void RenderTreeDivStart(TextWriter output, string treeId)
		{
			NavigationHost.RenderTreeDivStart(output, treeId, null);
		}

		// Token: 0x06002438 RID: 9272 RVA: 0x000D169C File Offset: 0x000CF89C
		public static void RenderTreeDivStart(TextWriter output, string treeId, string treeClassName)
		{
			output.Write("<div id=\"");
			Utilities.HtmlEncode(treeId, output);
			output.Write("\"");
			if (!string.IsNullOrEmpty(treeClassName))
			{
				output.Write(" class=\"");
				output.Write(treeClassName);
				output.Write("\"");
			}
			output.Write(">");
		}

		// Token: 0x06002439 RID: 9273 RVA: 0x000D16F6 File Offset: 0x000CF8F6
		public static void RenderTreeRegionDivEnd(TextWriter output)
		{
			output.Write("</div></div>");
		}

		// Token: 0x0600243A RID: 9274 RVA: 0x000D1703 File Offset: 0x000CF903
		public static void RenderTreeDivEnd(TextWriter output)
		{
			output.Write("</div>");
		}

		// Token: 0x0600243B RID: 9275 RVA: 0x000D1710 File Offset: 0x000CF910
		protected void InitializeView(PlaceHolder placeHolder)
		{
			if (placeHolder == null)
			{
				throw new ArgumentNullException("placeHolder");
			}
			OwaQueryStringParameters owaQueryStringParameters = new OwaQueryStringParameters();
			owaQueryStringParameters.SetApplicationElement(this.lastModuleApplicationElement);
			owaQueryStringParameters.ItemClass = this.lastModuleContentClass;
			if (this.lastModuleState != null)
			{
				owaQueryStringParameters.State = this.lastModuleState;
			}
			if (this.lastModuleMappingAction != null)
			{
				owaQueryStringParameters.Action = this.lastModuleMappingAction;
			}
			if (this.lastMailFolderId != null)
			{
				owaQueryStringParameters.Id = this.lastMailFolderId;
			}
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append(owaQueryStringParameters.QueryString);
			if (this.recipientBlockType == RecipientBlockType.DL)
			{
				stringBuilder.Append("&dl=1");
			}
			FormValue formValue = RequestDispatcherUtilities.DoFormsRegistryLookup(base.SessionContext, owaQueryStringParameters.ApplicationElement, owaQueryStringParameters.ItemClass, owaQueryStringParameters.Action, owaQueryStringParameters.State);
			if (formValue != null && RequestDispatcher.DoesSubPageSupportSingleDocument(formValue.Value as string))
			{
				OwaSubPage owaSubPage = (OwaSubPage)this.Page.LoadControl(Path.GetFileName(formValue.Value as string));
				Utilities.PutOwaSubPageIntoPlaceHolder(placeHolder, "b" + Utilities.HtmlEncode(this.lastModuleContainerId), owaSubPage, owaQueryStringParameters, "class=\"mainView\"", false);
				this.ChildSubPages.Add(owaSubPage);
				return;
			}
			StringBuilder stringBuilder2 = new StringBuilder();
			stringBuilder2.Append("<iframe allowtransparency id=\"");
			stringBuilder2.Append("b");
			Utilities.HtmlEncode(this.lastModuleContainerId, stringBuilder2);
			stringBuilder2.Append("\" src=\"");
			stringBuilder2.Append(stringBuilder.ToString());
			stringBuilder2.Append("\" class=\"mainView\" _cF=\"1\" frameborder=\"0\"></iframe>");
			placeHolder.Controls.Add(new LiteralControl(stringBuilder2.ToString()));
		}

		// Token: 0x0600243C RID: 9276 RVA: 0x000D18A4 File Offset: 0x000CFAA4
		internal static void RenderBuddyListTreeControl(TextWriter output, UserContext userContext, bool expandBuddyList)
		{
			SimpleTreeNode simpleTreeNode = new SimpleTreeNode(userContext, "bddyRt");
			simpleTreeNode.SetSelectable(false);
			simpleTreeNode.ClientNodeType = "buddyRootNode";
			SimpleTreeNode simpleTreeNode2 = simpleTreeNode;
			simpleTreeNode2.HighlightClassName += " trNdGpHdHl";
			SimpleTreeNode simpleTreeNode3 = simpleTreeNode;
			simpleTreeNode3.NodeClassName += " trNdGpHd";
			simpleTreeNode.NeedSync = false;
			simpleTreeNode.IsExpanded = expandBuddyList;
			simpleTreeNode.SetContent(LocalizedStrings.GetNonEncoded(2047178654));
			new SimpleTree(userContext, simpleTreeNode, "divTrBddy")
			{
				ErrDiv = "divErrMail"
			}.Render(output);
		}

		// Token: 0x0600243D RID: 9277 RVA: 0x000D1938 File Offset: 0x000CFB38
		private static string GetNavigationTreeId(NavigationNodeGroupSection groupSection)
		{
			switch (groupSection)
			{
			case NavigationNodeGroupSection.First:
				return "favTr";
			case NavigationNodeGroupSection.Calendar:
				return "calTr";
			case NavigationNodeGroupSection.Contacts:
				return "cntTr";
			case NavigationNodeGroupSection.Tasks:
				return "tskTr";
			}
			throw new ArgumentException("Navigation tree is only available in calendar, contact and task.");
		}

		// Token: 0x0600243E RID: 9278 RVA: 0x000D1988 File Offset: 0x000CFB88
		private static string GetNavigationTreeRegionId(NavigationNodeGroupSection groupSection)
		{
			switch (groupSection)
			{
			case NavigationNodeGroupSection.Calendar:
				return "divCalTrR";
			case NavigationNodeGroupSection.Contacts:
				return "divCntTrR";
			case NavigationNodeGroupSection.Tasks:
				return "divTskTrR";
			default:
				throw new ArgumentException("Navigation tree is only available in calendar, contact and task.");
			}
		}

		// Token: 0x0600243F RID: 9279 RVA: 0x000D19CC File Offset: 0x000CFBCC
		private static string GetNavigationTreeErrDivId(NavigationNodeGroupSection groupSection)
		{
			switch (groupSection)
			{
			case NavigationNodeGroupSection.First:
				return "divErrMail";
			case NavigationNodeGroupSection.Calendar:
				return "divErrCal";
			case NavigationNodeGroupSection.Contacts:
				return "divErrCnt";
			case NavigationNodeGroupSection.Tasks:
				return "divErrTsk";
			}
			throw new ArgumentException("Navigation tree is only available in calendar, contact and task.");
		}

		// Token: 0x06002440 RID: 9280 RVA: 0x000D1A1C File Offset: 0x000CFC1C
		internal static void RenderNavigationTreeControl(TextWriter writer, UserContext userContext, NavigationNodeGroupSection groupSection, OwaStoreObjectId newFolderId)
		{
			if (groupSection == NavigationNodeGroupSection.First)
			{
				throw new ArgumentException("Should not use this function to render favorites tree.");
			}
			string navigationTreeErrDivId = NavigationHost.GetNavigationTreeErrDivId(groupSection);
			string navigationTreeId = NavigationHost.GetNavigationTreeId(groupSection);
			string navigationTreeRegionId = NavigationHost.GetNavigationTreeRegionId(groupSection);
			writer.Write("<div id=\"");
			writer.Write(navigationTreeRegionId);
			writer.Write("\">");
			Infobar infobar = new Infobar(navigationTreeErrDivId, "infobar");
			infobar.Render(writer);
			NavigationHost.RenderTreeRegionDivStart(writer, null);
			NavigationTree navigationTree = NavigationTree.CreateNavigationTree(userContext, groupSection);
			if (newFolderId != null)
			{
				navigationTree.RootNode.SelectSpecifiedFolder(newFolderId);
			}
			NavigationHost.RenderTreeDivStart(writer, navigationTreeId);
			navigationTree.ErrDiv = navigationTreeErrDivId;
			navigationTree.Render(writer);
			NavigationHost.RenderTreeDivEnd(writer);
			NavigationHost.RenderTreeRegionDivEnd(writer);
			writer.Write("</div>");
		}

		// Token: 0x06002441 RID: 9281 RVA: 0x000D1ACC File Offset: 0x000CFCCC
		internal static void RenderNavigationTreeControl(TextWriter writer, UserContext userContext, NavigationModule navigationModule)
		{
			NavigationNodeGroupSection groupSection;
			switch (navigationModule)
			{
			case NavigationModule.Calendar:
				groupSection = NavigationNodeGroupSection.Calendar;
				break;
			case NavigationModule.Contacts:
				groupSection = NavigationNodeGroupSection.Contacts;
				break;
			case NavigationModule.Tasks:
				groupSection = NavigationNodeGroupSection.Tasks;
				break;
			default:
				throw new ArgumentException("Navigation tree is only available in calendar, contact and task.");
			}
			NavigationHost.RenderNavigationTreeControl(writer, userContext, groupSection, null);
		}

		// Token: 0x06002442 RID: 9282 RVA: 0x000D1B14 File Offset: 0x000CFD14
		internal static void RenderFavoritesAndNavigationTrees(TextWriter writer, UserContext userContext, OwaStoreObjectId newFolderId, params NavigationNodeGroupSection[] groupSections)
		{
			NavigationTree[] array = NavigationTree.CreateFavoriteAndNavigationTrees(userContext, groupSections);
			writer.Write("<div id=ntn>");
			for (int i = 0; i < array.Length; i++)
			{
				if (array[i] != null)
				{
					if (newFolderId != null)
					{
						array[i].RootNode.SelectSpecifiedFolder(newFolderId);
					}
					NavigationHost.RenderTreeDivStart(writer, NavigationHost.GetNavigationTreeId(groupSections[i]));
					array[i].ErrDiv = NavigationHost.GetNavigationTreeErrDivId(groupSections[i]);
					array[i].ErrHideId = array[i].ErrDiv + "Tr";
					array[i].Render(writer);
					NavigationHost.RenderTreeDivEnd(writer);
				}
			}
			writer.Write("</div>");
		}

		// Token: 0x04001915 RID: 6421
		private const string MailErrDivId = "divErrMail";

		// Token: 0x04001916 RID: 6422
		private const string PublicFolderErrorDivId = "divErrPF";

		// Token: 0x04001917 RID: 6423
		private const string Folder = "Folder";

		// Token: 0x04001918 RID: 6424
		private const string AddressList = "AddressList";

		// Token: 0x04001919 RID: 6425
		private const string DocumentsFolder = "IPF.DocumentLibrary";

		// Token: 0x0400191A RID: 6426
		private const string RecipientAddressList = "Recipients";

		// Token: 0x0400191B RID: 6427
		private const string RoomsAddressList = "Rooms";

		// Token: 0x0400191C RID: 6428
		private const string FolderIdPrefix = "b";

		// Token: 0x0400191D RID: 6429
		private const string BuddyTreeRootId = "bddyRt";

		// Token: 0x0400191E RID: 6430
		protected NavigationModule navigationModule;

		// Token: 0x0400191F RID: 6431
		private RecipientBlockType recipientBlockType;

		// Token: 0x04001920 RID: 6432
		protected PlaceHolder viewPlaceHolder;

		// Token: 0x04001921 RID: 6433
		protected string lastModuleContainerId = string.Empty;

		// Token: 0x04001922 RID: 6434
		protected string lastModuleContentClass = "IPF.Note";

		// Token: 0x04001923 RID: 6435
		protected string lastModuleName;

		// Token: 0x04001924 RID: 6436
		protected string lastModuleApplicationElement = "Folder";

		// Token: 0x04001925 RID: 6437
		protected string lastModuleMappingAction = string.Empty;

		// Token: 0x04001926 RID: 6438
		protected string lastModuleMappingState = string.Empty;

		// Token: 0x04001927 RID: 6439
		protected string lastMailFolderId;

		// Token: 0x04001928 RID: 6440
		protected string lastModuleState;

		// Token: 0x04001929 RID: 6441
		private List<OwaSubPage> childSubPages;
	}
}
