using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;
using Microsoft.Exchange.Clients.Owa.Core;
using Microsoft.Exchange.Clients.Owa.Core.Controls;
using Microsoft.Exchange.Clients.Owa.Premium.Controls;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Clients;

namespace Microsoft.Exchange.Clients.Owa.Premium
{
	// Token: 0x020004C4 RID: 1220
	[OwaEventNamespace("MsgVLV2")]
	[OwaEventObjectId(typeof(OwaStoreObjectId))]
	internal class MessageVirtualListViewEventHandler2 : FolderVirtualListViewEventHandler2
	{
		// Token: 0x06002E76 RID: 11894 RVA: 0x001095AC File Offset: 0x001077AC
		public new static void Register()
		{
			OwaEventRegistry.RegisterEnum(typeof(JunkEmailStatus));
			FolderVirtualListViewEventHandler2.Register();
			OwaEventRegistry.RegisterHandler(typeof(MessageVirtualListViewEventHandler2));
		}

		// Token: 0x06002E77 RID: 11895 RVA: 0x001095D1 File Offset: 0x001077D1
		[OwaEventParameter("id", typeof(OwaStoreObjectId), true)]
		[OwaEventParameter("JS", typeof(JunkEmailStatus), false, true)]
		[OwaEvent("MarkAsRead")]
		[OwaEventParameter("fId", typeof(OwaStoreObjectId), false, true)]
		public void MarkAsRead()
		{
			ExTraceGlobals.MailCallTracer.TraceDebug((long)this.GetHashCode(), "MessageVirtualListViewEventHandler.MarkAsRead");
			this.MarkAsReadOrUnread(true);
		}

		// Token: 0x06002E78 RID: 11896 RVA: 0x001095F0 File Offset: 0x001077F0
		[OwaEvent("MarkAsUnread")]
		[OwaEventParameter("JS", typeof(JunkEmailStatus), false, true)]
		[OwaEventParameter("id", typeof(OwaStoreObjectId), true)]
		[OwaEventParameter("fId", typeof(OwaStoreObjectId), false, true)]
		public void MarkAsUnread()
		{
			ExTraceGlobals.MailCallTracer.TraceDebug((long)this.GetHashCode(), "MessageVirtualListViewEventHandler.MarkAsUnread");
			this.MarkAsReadOrUnread(false);
		}

		// Token: 0x06002E79 RID: 11897 RVA: 0x00109610 File Offset: 0x00107810
		[OwaEvent("GetSenderSmtp")]
		[OwaEventParameter("id", typeof(string))]
		public void GetSenderSmtp()
		{
			ExTraceGlobals.MailCallTracer.TraceDebug((long)this.GetHashCode(), "MessageVirtualListViewEventHandler.GetSenderSmtp");
			string itemId = (string)base.GetParameter("id");
			this.Writer.Write("<div id=\"sSSA\">");
			Utilities.HtmlEncode(Utilities.GetSenderSmtpAddress(itemId, base.UserContext), this.Writer);
			this.Writer.Write("</div>");
		}

		// Token: 0x06002E7A RID: 11898 RVA: 0x0010967C File Offset: 0x0010787C
		[OwaEventParameter("id", typeof(string))]
		[OwaEvent("GetDeliveryReportUrlParameters")]
		public void GetDeliveryReportUrlParameters()
		{
			ExTraceGlobals.MailCallTracer.TraceDebug((long)this.GetHashCode(), "MessageVirtualListViewEventHandler.GetDeliveryReportUrlParameters");
			this.Writer.Write("<div id=\"sMBX\">");
			Utilities.HtmlEncode(base.UserContext.ExchangePrincipal.ObjectId.ObjectGuid.ToString("N"), this.SanitizingWriter);
			this.Writer.Write("</div>");
			OwaStoreObjectId owaStoreObjectId = OwaStoreObjectId.CreateFromString((string)base.GetParameter("id"));
			string s = Convert.ToBase64String(owaStoreObjectId.StoreObjectId.ProviderLevelItemId);
			this.Writer.Write("<div id=\"sEID\">");
			Utilities.HtmlEncode(s, this.SanitizingWriter);
			this.Writer.Write("</div>");
		}

		// Token: 0x06002E7B RID: 11899 RVA: 0x00109740 File Offset: 0x00107940
		[OwaEventParameter("fId", typeof(string))]
		[OwaEvent("GetELCFolderSize")]
		public void GetELCFolderSize()
		{
			ExTraceGlobals.MailCallTracer.TraceDebug((long)this.GetHashCode(), "MessageVirtualListViewEventHandler.GetELCFolderSize");
			OwaStoreObjectId owaStoreObjectId = OwaStoreObjectId.CreateFromString((string)base.GetParameter("fId"));
			if (owaStoreObjectId.IsPublic)
			{
				throw new OwaInvalidRequestException("Cannot get ELC size for public folders");
			}
			using (Folder folder = Utilities.GetFolder<Folder>(base.UserContext, owaStoreObjectId, new PropertyDefinition[]
			{
				FolderSchema.FolderSize
			}))
			{
				long value = 0L;
				object obj = folder.TryGetProperty(FolderSchema.FolderSize);
				if (!(obj is PropertyError))
				{
					value = (long)((int)obj);
				}
				this.Writer.Write("<div id=\"fldSz\">");
				this.Writer.Write(value);
				this.Writer.Write("</div>");
			}
		}

		// Token: 0x06002E7C RID: 11900 RVA: 0x00109814 File Offset: 0x00107A14
		[OwaEventParameter("expId", typeof(OwaStoreObjectId), false, true)]
		[OwaEventParameter("St", typeof(FolderVirtualListViewState))]
		[OwaEventParameter("fldrfltr", typeof(FolderVirtualListViewFilter), false, true)]
		[OwaEventParameter("srchf", typeof(FolderVirtualListViewSearchFilter), false, true)]
		[OwaEventParameter("fltr", typeof(int), false, true)]
		[OwaEvent("LoadFresh")]
		[OwaEventVerb(OwaEventVerb.Post)]
		[OwaEventParameter("SR", typeof(int))]
		[OwaEventParameter("RC", typeof(int))]
		public override void LoadFresh()
		{
			base.InternalLoadFresh();
		}

		// Token: 0x06002E7D RID: 11901 RVA: 0x0010981C File Offset: 0x00107A1C
		[OwaEventParameter("expId", typeof(OwaStoreObjectId), false, true)]
		[OwaEventParameter("AId", typeof(OwaStoreObjectId))]
		[OwaEventParameter("St", typeof(FolderVirtualListViewState))]
		[OwaEventParameter("fldrfltr", typeof(FolderVirtualListViewFilter), false, true)]
		[OwaEventParameter("srchf", typeof(FolderVirtualListViewSearchFilter), false, true)]
		[OwaEventParameter("fltr", typeof(int), false, true)]
		[OwaEvent("LoadNext")]
		[OwaEventParameter("RC", typeof(int))]
		[OwaEventVerb(OwaEventVerb.Post)]
		public override void LoadNext()
		{
			base.InternalLoadNext();
		}

		// Token: 0x06002E7E RID: 11902 RVA: 0x00109824 File Offset: 0x00107A24
		[OwaEventParameter("AId", typeof(OwaStoreObjectId))]
		[OwaEventParameter("RC", typeof(int))]
		[OwaEventParameter("St", typeof(FolderVirtualListViewState))]
		[OwaEventParameter("fldrfltr", typeof(FolderVirtualListViewFilter), false, true)]
		[OwaEventParameter("srchf", typeof(FolderVirtualListViewSearchFilter), false, true)]
		[OwaEventParameter("fltr", typeof(int), false, true)]
		[OwaEventParameter("expId", typeof(OwaStoreObjectId), false, true)]
		[OwaEventVerb(OwaEventVerb.Post)]
		[OwaEvent("LoadPrevious")]
		public override void LoadPrevious()
		{
			base.InternalLoadPrevious();
		}

		// Token: 0x06002E7F RID: 11903 RVA: 0x0010982C File Offset: 0x00107A2C
		[OwaEventParameter("fldrfltr", typeof(FolderVirtualListViewFilter), false, true)]
		[OwaEvent("SeekNext")]
		[OwaEventParameter("srchf", typeof(FolderVirtualListViewSearchFilter), false, true)]
		[OwaEventParameter("nwSel", typeof(bool), false, true)]
		[OwaEventVerb(OwaEventVerb.Post)]
		[OwaEventParameter("SId", typeof(OwaStoreObjectId))]
		[OwaEventParameter("RC", typeof(int))]
		[OwaEventParameter("St", typeof(FolderVirtualListViewState))]
		[OwaEventParameter("fltr", typeof(int), false, true)]
		[OwaEventParameter("expId", typeof(OwaStoreObjectId), false, true)]
		public override void SeekNext()
		{
			base.InternalSeekNext();
		}

		// Token: 0x06002E80 RID: 11904 RVA: 0x00109834 File Offset: 0x00107A34
		[OwaEventParameter("nwSel", typeof(bool), false, true)]
		[OwaEvent("SeekPrevious")]
		[OwaEventVerb(OwaEventVerb.Post)]
		[OwaEventParameter("SId", typeof(OwaStoreObjectId))]
		[OwaEventParameter("RC", typeof(int))]
		[OwaEventParameter("St", typeof(FolderVirtualListViewState))]
		[OwaEventParameter("fldrfltr", typeof(FolderVirtualListViewFilter), false, true)]
		[OwaEventParameter("srchf", typeof(FolderVirtualListViewSearchFilter), false, true)]
		[OwaEventParameter("fltr", typeof(int), false, true)]
		[OwaEventParameter("expId", typeof(OwaStoreObjectId), false, true)]
		public override void SeekPrevious()
		{
			base.InternalSeekPrevious();
		}

		// Token: 0x06002E81 RID: 11905 RVA: 0x0010983C File Offset: 0x00107A3C
		[OwaEventParameter("RC", typeof(int))]
		[OwaEventParameter("srchf", typeof(FolderVirtualListViewSearchFilter), false, true)]
		[OwaEvent("Sort")]
		[OwaEventVerb(OwaEventVerb.Post)]
		[OwaEventParameter("fltr", typeof(int), false, true)]
		[OwaEventParameter("expId", typeof(OwaStoreObjectId), false, true)]
		[OwaEventParameter("St", typeof(FolderVirtualListViewState))]
		[OwaEventParameter("SId", typeof(OwaStoreObjectId), false, true)]
		[OwaEventParameter("fldrfltr", typeof(FolderVirtualListViewFilter), false, true)]
		public override void Sort()
		{
			base.InternalSort();
		}

		// Token: 0x06002E82 RID: 11906 RVA: 0x00109844 File Offset: 0x00107A44
		[OwaEventParameter("expId", typeof(OwaStoreObjectId), false, true)]
		[OwaEventParameter("srchf", typeof(FolderVirtualListViewSearchFilter), false, true)]
		[OwaEvent("SetML")]
		[OwaEventVerb(OwaEventVerb.Post)]
		[OwaEventParameter("SR", typeof(int))]
		[OwaEventParameter("RC", typeof(int))]
		[OwaEventParameter("St", typeof(FolderVirtualListViewState))]
		[OwaEventParameter("fldrfltr", typeof(FolderVirtualListViewFilter), false, true)]
		public override void SetMultiLineState()
		{
			base.InternalSetMultiLineState();
		}

		// Token: 0x06002E83 RID: 11907 RVA: 0x0010984C File Offset: 0x00107A4C
		[OwaEventParameter("fltr", typeof(int), false, true)]
		[OwaEventParameter("srchf", typeof(FolderVirtualListViewSearchFilter), false, true)]
		[OwaEventParameter("expId", typeof(OwaStoreObjectId), false, true)]
		[OwaEvent("TypeDown")]
		[OwaEventVerb(OwaEventVerb.Post)]
		[OwaEventParameter("td", typeof(string))]
		[OwaEventParameter("RC", typeof(int))]
		[OwaEventParameter("St", typeof(FolderVirtualListViewState))]
		[OwaEventParameter("fldrfltr", typeof(FolderVirtualListViewFilter), false, true)]
		public override void TypeDownSearch()
		{
			base.InternalTypeDownSearch();
		}

		// Token: 0x06002E84 RID: 11908 RVA: 0x00109854 File Offset: 0x00107A54
		[OwaEventParameter("RTOcc", typeof(OwaStoreObjectId), true, true)]
		[OwaEventParameter("fId", typeof(OwaStoreObjectId))]
		[OwaEvent("Delete")]
		[OwaEventParameter("Itms", typeof(DeleteItemInfo), true, true)]
		[OwaEventParameter("Cnvs", typeof(OwaStoreObjectId), true, true)]
		public override void Delete()
		{
			this.DeleteConversationsOrItems(false);
		}

		// Token: 0x06002E85 RID: 11909 RVA: 0x0010985D File Offset: 0x00107A5D
		[OwaEventParameter("Cnvs", typeof(OwaStoreObjectId), true, true)]
		[OwaEventParameter("JS", typeof(JunkEmailStatus), false, true)]
		[OwaEvent("PermanentDelete")]
		[OwaEventParameter("Itms", typeof(DeleteItemInfo), true, true)]
		[OwaEventParameter("RTOcc", typeof(OwaStoreObjectId), true, true)]
		[OwaEventParameter("fId", typeof(OwaStoreObjectId))]
		public override void PermanentDelete()
		{
			this.DeleteConversationsOrItems(true);
		}

		// Token: 0x06002E86 RID: 11910 RVA: 0x00109868 File Offset: 0x00107A68
		[OwaEventParameter("Itms", typeof(DeleteItemInfo), true, true)]
		[OwaEvent("IgnoreConversations")]
		[OwaEventParameter("Cnvs", typeof(OwaStoreObjectId), true, true)]
		[OwaEventParameter("fId", typeof(OwaStoreObjectId))]
		public void IgnoreConversations()
		{
			DeleteItemInfo[] array = (DeleteItemInfo[])base.GetParameter("Itms");
			OwaStoreObjectId[] array2 = (OwaStoreObjectId[])base.GetParameter("Cnvs");
			if (array != null)
			{
				if (array2 != null)
				{
					throw new OwaInvalidRequestException("IgnoreConversations does not accept both conversation IDs and items.");
				}
				if (array.Length > 500)
				{
					throw new OwaInvalidOperationException(string.Format(CultureInfo.InvariantCulture, "Calling ignore conversation on {0} item(s) in a single request is not supported", new object[]
					{
						array.Length
					}));
				}
				OwaStoreObjectId owaStoreObjectId = (OwaStoreObjectId)base.GetParameter("fId");
				MailboxSession session = (MailboxSession)owaStoreObjectId.GetSession(base.UserContext);
				List<OwaStoreObjectId> list = new List<OwaStoreObjectId>(array.Length);
				using (Folder folder = Folder.Bind(session, owaStoreObjectId.StoreObjectId))
				{
					foreach (DeleteItemInfo deleteItemInfo in array)
					{
						ConversationId conversationId = ConversationUtilities.MapItemToConversation(base.UserContext, deleteItemInfo.OwaStoreObjectId);
						if (conversationId != null)
						{
							list.Add(OwaStoreObjectId.CreateFromConversationId(conversationId, folder, null));
						}
					}
				}
				array2 = list.ToArray();
			}
			base.CheckSizeOfBatchOperation(array2);
			bool flag = true;
			foreach (OwaStoreObjectId conversationId2 in array2)
			{
				flag &= ConversationUtilities.IgnoreConversation(base.UserContext, conversationId2);
			}
			if (!flag)
			{
				base.RenderErrorResult("errDel", 166628739);
			}
		}

		// Token: 0x06002E87 RID: 11911 RVA: 0x001099D4 File Offset: 0x00107BD4
		[OwaEvent("CancelIgnoreConversation")]
		[OwaEventParameter("id", typeof(OwaStoreObjectId))]
		public void CancelIgnoreConversation()
		{
			OwaStoreObjectId owaStoreObjectId = (OwaStoreObjectId)base.GetParameter("id");
			ConversationUtilities.CancelIgnoreConversation(base.UserContext, owaStoreObjectId, true);
		}

		// Token: 0x06002E88 RID: 11912 RVA: 0x00109A00 File Offset: 0x00107C00
		[OwaEvent("GetConversationAction")]
		[OwaEventParameter("id", typeof(OwaStoreObjectId))]
		public void GetConversationAction()
		{
			OwaStoreObjectId owaStoreObjectId = (OwaStoreObjectId)base.GetParameter("id");
			bool flag = ConversationUtilities.IsConversationIgnored(base.UserContext, owaStoreObjectId, null);
			this.SanitizingWriter.Write(flag ? "1" : "0");
		}

		// Token: 0x06002E89 RID: 11913 RVA: 0x00109A46 File Offset: 0x00107C46
		[OwaEventParameter("St", typeof(FolderVirtualListViewState), false, true)]
		[OwaEventParameter("id", typeof(OwaStoreObjectId), true)]
		[OwaEventParameter("fId", typeof(OwaStoreObjectId), false, true)]
		[OwaEventParameter("destId", typeof(OwaStoreObjectId))]
		[OwaEventParameter("fAddToMRU", typeof(bool), false, true)]
		[OwaEvent("Copy")]
		[OwaEventParameter("fldrfltr", typeof(FolderVirtualListViewFilter), false, true)]
		[OwaEventParameter("srchf", typeof(FolderVirtualListViewSearchFilter), false, true)]
		[OwaEventParameter("fltr", typeof(int), false, true)]
		[OwaEventParameter("expId", typeof(OwaStoreObjectId), false, true)]
		public override void Copy()
		{
			ExTraceGlobals.MailCallTracer.TraceDebug((long)this.GetHashCode(), "MessageVirtualListViewEventHandler.Copy");
			base.CheckSizeOfBatchOperation(null);
			this.CopyOrMove(true);
		}

		// Token: 0x06002E8A RID: 11914 RVA: 0x00109A6C File Offset: 0x00107C6C
		[OwaEventParameter("srchf", typeof(FolderVirtualListViewSearchFilter), false, true)]
		[OwaEventParameter("fltr", typeof(int), false, true)]
		[OwaEventParameter("expId", typeof(OwaStoreObjectId), false, true)]
		[OwaEvent("Move")]
		[OwaEventParameter("fId", typeof(OwaStoreObjectId), false, true)]
		[OwaEventParameter("destId", typeof(OwaStoreObjectId))]
		[OwaEventParameter("fAddToMRU", typeof(bool), false, true)]
		[OwaEventParameter("St", typeof(FolderVirtualListViewState), false, true)]
		[OwaEventParameter("fldrfltr", typeof(FolderVirtualListViewFilter), false, true)]
		[OwaEventParameter("id", typeof(OwaStoreObjectId), true)]
		public override void Move()
		{
			ExTraceGlobals.MailCallTracer.TraceDebug((long)this.GetHashCode(), "MessageVirtualListViewEventHandler.Move");
			base.CheckSizeOfBatchOperation(null);
			this.CopyOrMove(false);
		}

		// Token: 0x06002E8B RID: 11915 RVA: 0x00109A94 File Offset: 0x00107C94
		[OwaEvent("ExpandConversation")]
		[OwaEventParameter("fltr", typeof(int), false, true)]
		[OwaEventVerb(OwaEventVerb.Post)]
		[OwaEventParameter("St", typeof(FolderVirtualListViewState))]
		[OwaEventParameter("expId", typeof(OwaStoreObjectId))]
		[OwaEventParameter("fldrfltr", typeof(FolderVirtualListViewFilter), false, true)]
		[OwaEventParameter("srchf", typeof(FolderVirtualListViewSearchFilter), false, true)]
		public void ExpandConversation()
		{
			if (!this.IsConversationView)
			{
				throw new OwaInvalidRequestException("Attempt to expand a conversation in a non-conversation view");
			}
			OwaStoreObjectId expId = (OwaStoreObjectId)base.GetParameter("expId");
			this.InternalExpandConversation(expId);
		}

		// Token: 0x06002E8C RID: 11916 RVA: 0x00109ACC File Offset: 0x00107CCC
		[OwaEventVerb(OwaEventVerb.Post)]
		[OwaEvent("GetFilterMenu")]
		public void GetFilterMenu()
		{
			new FilterViewDropDownMenu(base.UserContext).Render(this.Writer);
		}

		// Token: 0x06002E8D RID: 11917 RVA: 0x00109AE4 File Offset: 0x00107CE4
		[OwaEventVerb(OwaEventVerb.Post)]
		[OwaEvent("AddFilterToFavorites")]
		[OwaEventParameter("fltrSbj", typeof(string))]
		[OwaEventParameter("fldrfltr", typeof(FolderVirtualListViewFilter))]
		public void AddFilterToFavorites()
		{
			string text = ((string)base.GetParameter("fltrSbj")).Trim();
			if (text.Length == 0)
			{
				throw new OwaEventHandlerException("User did not provide name for new folder", LocalizedStrings.GetNonEncoded(-41080803), true);
			}
			if (text.Length > 256)
			{
				text = text.Substring(0, 256);
			}
			base.BindToFolder();
			try
			{
				if (Utilities.IsDefaultFolder(base.ContextFolder, DefaultFolderType.DeletedItems) || Utilities.IsItemInDefaultFolder(base.ContextFolder, DefaultFolderType.DeletedItems) || Utilities.IsFavoritesFilterFolder(base.UserContext, base.ContextFolder))
				{
					throw new OwaInvalidRequestException("Only normal folder or Outlook search folder can be filtered.");
				}
				if (base.FilteredFolder != null)
				{
					NavigationNodeCollection navigationNodeCollection = NavigationNodeCollection.TryCreateNavigationNodeCollection(base.UserContext, base.UserContext.MailboxSession, NavigationNodeGroupSection.First);
					if (navigationNodeCollection.FindFoldersById(base.FilteredFolder.Id.ObjectId).Length > 0)
					{
						throw new OwaEventHandlerException("Filtered view has already been added.", LocalizedStrings.GetNonEncoded(-44763698), true);
					}
					int num = 0;
					foreach (NavigationNodeFolder navigationNodeFolder in navigationNodeCollection[0].Children)
					{
						if (navigationNodeFolder.IsValid && navigationNodeFolder.IsFilteredView)
						{
							num++;
						}
					}
					if (num >= Globals.MaximumFilteredViewInFavoritesPerUser)
					{
						string description = string.Format(base.UserContext.UserCulture, LocalizedStrings.GetNonEncoded(-529843556), new object[]
						{
							Globals.MaximumFilteredViewInFavoritesPerUser
						});
						throw new OwaEventHandlerException("Filtered view exceeds budget.", description, true);
					}
					base.FilteredFolder[FolderSchema.SearchFolderAllowAgeout] = false;
					base.FilteredFolder.DisplayName = text;
					FolderSaveResult folderSaveResult = base.FilteredFolder.Save();
					if (folderSaveResult.OperationResult != OperationResult.Succeeded)
					{
						if (Utilities.IsFolderNameConflictError(folderSaveResult))
						{
							throw new OwaEventHandlerException("Folder name exists", LocalizedStrings.GetNonEncoded(-1782850773), OwaEventHandlerErrorCode.FolderNameExists, true);
						}
						throw new OwaEventHandlerException("Fail to save folder properties.", LocalizedStrings.GetNonEncoded(-920350130), true);
					}
					else
					{
						base.FilteredFolder.Load(FolderList.FolderTreeQueryProperties);
						NavigationNodeFolder navigationNodeFolder2 = navigationNodeCollection.AppendFolderToFavorites(base.FilteredFolder);
						navigationNodeFolder2.SetFilterParameter(base.FilterCondition.SourceFolderId.StoreObjectId, base.FilterCondition.GetBooleanFlags(), base.FilterCondition.GetCategories(), base.FilterCondition.From, base.FilterCondition.To);
						navigationNodeCollection.Save(base.UserContext.MailboxSession);
						navigationNodeCollection = NavigationNodeCollection.TryCreateNavigationNodeCollection(base.UserContext, base.UserContext.MailboxSession, NavigationNodeGroupSection.First);
						NavigationHost.RenderFavoritesAndNavigationTrees(this.Writer, base.UserContext, null, new NavigationNodeGroupSection[]
						{
							NavigationNodeGroupSection.First
						});
					}
				}
			}
			finally
			{
				this.EndProcessEvent();
			}
		}

		// Token: 0x06002E8E RID: 11918 RVA: 0x00109DC0 File Offset: 0x00107FC0
		[OwaEventParameter("Itms", typeof(OwaStoreObjectId), true)]
		[OwaEventVerb(OwaEventVerb.Post)]
		[OwaEventParameter("St", typeof(FolderVirtualListViewState))]
		[OwaEvent("Preread")]
		[OwaEventParameter("fId", typeof(OwaStoreObjectId))]
		public void PrereadMessages()
		{
			OwaStoreObjectId owaStoreObjectId = (OwaStoreObjectId)base.GetParameter("fId");
			MailboxSession mailboxSession = owaStoreObjectId.GetSession(base.UserContext) as MailboxSession;
			OwaStoreObjectId[] array = (OwaStoreObjectId[])base.GetParameter("Itms");
			StoreId[] array2 = null;
			if (mailboxSession == null)
			{
				throw new OwaInvalidRequestException("Session type does not support preread.");
			}
			if (array.Length == 0)
			{
				throw new ArgumentNullException("itemIds cannot be empty");
			}
			try
			{
				if (this.IsConversationView)
				{
					array2 = ConversationUtilities.GetPrereadItemIds(mailboxSession, array);
				}
				else
				{
					array2 = new StoreId[array.Length];
					for (int i = 0; i < array.Length; i++)
					{
						array2[i] = Utilities.TryGetStoreId(array[i]);
					}
				}
			}
			catch (ArgumentNullException exception)
			{
				if (Globals.SendWatsonReports)
				{
					ExWatson.AddExtraData(this.GetExtraWatsonData(array));
					ExWatson.SendReport(exception, ReportOptions.None, null);
					return;
				}
			}
			mailboxSession.PrereadMessages(array2);
		}

		// Token: 0x06002E8F RID: 11919 RVA: 0x00109E98 File Offset: 0x00108098
		[OwaEventParameter("destId", typeof(OwaStoreObjectId))]
		[OwaEvent("RemoveSubscriptions")]
		[OwaEventVerb(OwaEventVerb.Post)]
		public void RemoveSubscripitions()
		{
			ExTraceGlobals.MailCallTracer.TraceDebug((long)this.GetHashCode(), "MessageVirtualListViewEventHandler2.RemoveSubscriptions");
			OwaStoreObjectId folderId = (OwaStoreObjectId)base.GetParameter("destId");
			base.UserContext.MapiNotificationManager.UnsubscribeFolderContentChanges(folderId);
		}

		// Token: 0x06002E90 RID: 11920 RVA: 0x00109EE0 File Offset: 0x001080E0
		protected override VirtualListView2 GetListView()
		{
			base.BindToFolder();
			if (!base.IsFiltered)
			{
				OwaStoreObjectId owaStoreObjectId = OwaStoreObjectId.CreateFromSessionFolderId(base.UserContext, base.ContextFolder.Session, base.ContextFolder.Id.ObjectId);
				OwaStoreObjectId dataFolderId = OwaStoreObjectId.CreateFromSessionFolderId(base.UserContext, base.DataFolder.Session, base.DataFolder.Id.ObjectId);
				MailboxSession sessionIn = base.ContextFolder.Session as MailboxSession;
				if (base.UserContext.MapiNotificationManager.HasDataFolderChanged(sessionIn, owaStoreObjectId, dataFolderId))
				{
					base.UserContext.MapiNotificationManager.UnsubscribeFolderContentChanges(owaStoreObjectId);
				}
			}
			return new MessageVirtualListView2(base.UserContext, "divVLV", this.ListViewState.IsMultiLine, this.ListViewState.SortedColumn, this.ListViewState.SortOrder, base.ContextFolder, base.DataFolder, this.GetViewFilter(), base.SearchScope, base.IsFiltered, base.FilterCondition);
		}

		// Token: 0x06002E91 RID: 11921 RVA: 0x00109FD8 File Offset: 0x001081D8
		protected override void PersistSort()
		{
			OwaStoreObjectId owaStoreObjectId = (OwaStoreObjectId)this.ListViewState.SourceContainerId;
			if (owaStoreObjectId.IsPublic && this.ListViewState.SortedColumn == ColumnId.ConversationLastDeliveryTime)
			{
				throw new OwaInvalidRequestException("Cannot arrange by conversation in a public folder");
			}
			base.PersistSort();
		}

		// Token: 0x06002E92 RID: 11922 RVA: 0x0010A020 File Offset: 0x00108220
		protected override void RenderExtraData(VirtualListView2 listView)
		{
			base.RenderExtraData(listView);
			if (!this.IsConversationView)
			{
				return;
			}
			OwaStoreObjectId expId = (OwaStoreObjectId)base.GetParameter("expId");
			this.InternalExpandConversation(expId);
		}

		// Token: 0x06002E93 RID: 11923 RVA: 0x0010A058 File Offset: 0x00108258
		protected override OwaStoreObjectId GetSeekId()
		{
			OwaStoreObjectId owaStoreObjectId = (OwaStoreObjectId)base.GetParameter("SId");
			if (owaStoreObjectId == null)
			{
				return null;
			}
			if (!owaStoreObjectId.IsConversationId && this.IsConversationView)
			{
				ConversationId conversationId = ConversationUtilities.MapItemToConversation(base.UserContext, owaStoreObjectId);
				if (conversationId != null)
				{
					this.newSeekId = OwaStoreObjectId.CreateFromConversationId(conversationId, base.DataFolder, null);
				}
			}
			else if (owaStoreObjectId.IsConversationId && !this.IsConversationView)
			{
				StoreObjectId storeObjectId = ConversationUtilities.MapConversationToItem(base.UserContext, owaStoreObjectId.ConversationId, OwaStoreObjectId.CreateFromStoreObject(base.DataFolder));
				if (storeObjectId != null)
				{
					this.newSeekId = OwaStoreObjectId.CreateFromStoreObjectId(storeObjectId, owaStoreObjectId);
				}
			}
			return this.newSeekId ?? owaStoreObjectId;
		}

		// Token: 0x06002E94 RID: 11924 RVA: 0x0010A0F8 File Offset: 0x001082F8
		protected override OwaStoreObjectId GetNewSelection()
		{
			return this.newSeekId;
		}

		// Token: 0x06002E95 RID: 11925 RVA: 0x0010A100 File Offset: 0x00108300
		protected override void RenderAdvancedFind(TextWriter writer, OwaStoreObjectId folderId)
		{
			base.RenderAdvancedFind(writer, AdvancedFindComponents.Categories | AdvancedFindComponents.FromTo | AdvancedFindComponents.SubjectBody, folderId);
		}

		// Token: 0x06002E96 RID: 11926 RVA: 0x0010A10C File Offset: 0x0010830C
		protected override void RenderSearchInPublicFolder(TextWriter writer)
		{
			AdvancedFindComponents advancedFindComponents = AdvancedFindComponents.Categories | AdvancedFindComponents.FromTo | AdvancedFindComponents.SearchTextInSubject | AdvancedFindComponents.SearchButton;
			base.RenderAdvancedFind(this.Writer, advancedFindComponents, null);
		}

		// Token: 0x06002E97 RID: 11927 RVA: 0x0010A150 File Offset: 0x00108350
		private void MarkAsReadOrUnread(bool isRead)
		{
			OwaStoreObjectId[] array = (OwaStoreObjectId[])base.GetParameter("id");
			if (ConversationUtilities.ContainsConversationItem(base.UserContext, array))
			{
				List<OwaStoreObjectId> list = new List<OwaStoreObjectId>();
				List<OwaStoreObjectId> list2 = new List<OwaStoreObjectId>();
				foreach (OwaStoreObjectId owaStoreObjectId in array)
				{
					if (base.UserContext.SentItemsFolderId.Equals(owaStoreObjectId.ParentFolderId))
					{
						list.Add(owaStoreObjectId);
					}
					else
					{
						list2.Add(owaStoreObjectId);
					}
				}
				List<OwaStoreObjectId> list3 = new List<OwaStoreObjectId>();
				Func<IStorePropertyBag, bool> filter = (IStorePropertyBag propertyBag) => ItemUtility.GetProperty<bool>(propertyBag, MessageItemSchema.IsRead, isRead) != isRead;
				if (list2.Count > 0)
				{
					list3.AddRange(ConversationUtilities.GetAllItemIds(base.UserContext, list2.ToArray(), new PropertyDefinition[]
					{
						MessageItemSchema.IsRead
					}, filter, new StoreObjectId[]
					{
						base.UserContext.SentItemsFolderId
					}));
				}
				if (list.Count > 0)
				{
					list3.AddRange(ConversationUtilities.GetAllItemIds(base.UserContext, list.ToArray(), new PropertyDefinition[]
					{
						MessageItemSchema.IsRead
					}, filter, new StoreObjectId[0]));
				}
				array = list3.ToArray();
			}
			if (array.Length > 500)
			{
				throw new OwaInvalidOperationException(string.Format(CultureInfo.InvariantCulture, "Marking {0} item(s) as read or unread in a single request is not supported", new object[]
				{
					array.Length
				}));
			}
			JunkEmailStatus junkEmailStatus = JunkEmailStatus.NotJunk;
			if (base.IsParameterSet("JS"))
			{
				junkEmailStatus = (JunkEmailStatus)base.GetParameter("JS");
			}
			if (array.Length > 0)
			{
				Utilities.MarkItemsAsRead(base.UserContext, array, junkEmailStatus, !isRead);
			}
		}

		// Token: 0x17000D41 RID: 3393
		// (get) Token: 0x06002E98 RID: 11928 RVA: 0x0010A307 File Offset: 0x00108507
		private bool IsConversationView
		{
			get
			{
				return ConversationUtilities.IsConversationSortColumn(this.ListViewState.SortedColumn);
			}
		}

		// Token: 0x06002E99 RID: 11929 RVA: 0x0010A31C File Offset: 0x0010851C
		private void InternalExpandConversation(OwaStoreObjectId expId)
		{
			try
			{
				if (expId != null)
				{
					SortOrder sortOrder = SortOrder.Ascending;
					ConversationPartsList2 conversationPartsList = new ConversationPartsList2(sortOrder, base.UserContext, base.SearchScope);
					base.BindToFolder();
					ConversationTreeSortOrder sortOrder2 = base.UserContext.UserOptions.ShowTreeInListView ? ConversationTreeSortOrder.DeepTraversalAscending : ConversationUtilities.GetConversationTreeSortOrder(base.UserContext.UserOptions.ConversationSortOrder);
					conversationPartsList.ConversationPartsDataSource = new ConversationPartsDataSource(base.UserContext, conversationPartsList.Properties, base.DataFolder, expId, sortOrder2);
					conversationPartsList.ConversationPartsDataSource.Load();
					OwaStoreObjectId conversationId = expId;
					if (base.IsFiltered)
					{
						conversationId = OwaStoreObjectId.CreateFromConversationId(expId.ConversationId, base.DataFolder, expId.InstanceKey);
					}
					conversationPartsList.RenderConversationParts(this.Writer, conversationId, base.ContextFolder, base.DataFolder);
				}
			}
			finally
			{
				this.EndProcessEvent();
			}
		}

		// Token: 0x06002E9A RID: 11930 RVA: 0x0010A3F8 File Offset: 0x001085F8
		private void CopyOrMove(bool isCopy)
		{
			OwaStoreObjectId[] array = (OwaStoreObjectId[])base.GetParameter("id");
			OwaStoreObjectId owaStoreObjectId = (OwaStoreObjectId)base.GetParameter("fId");
			OwaStoreObjectId owaStoreObjectId2 = (OwaStoreObjectId)base.GetParameter("destId");
			if (!isCopy && owaStoreObjectId != null && Utilities.IsDefaultFolderId(base.UserContext.MailboxSession, owaStoreObjectId.StoreObjectId, DefaultFolderType.DeletedItems) && !Utilities.IsDefaultFolderId(base.UserContext.MailboxSession, owaStoreObjectId2.StoreObjectId, DefaultFolderType.DeletedItems))
			{
				foreach (OwaStoreObjectId owaStoreObjectId3 in array)
				{
					ConversationUtilities.CancelIgnoreConversation(base.UserContext, owaStoreObjectId3, false);
				}
			}
			if (ConversationUtilities.ContainsConversationItem(base.UserContext, array))
			{
				OwaStoreObjectId[] localItemIds = ConversationUtilities.GetLocalItemIds(base.UserContext, array, owaStoreObjectId);
				if (localItemIds.Length == 0)
				{
					return;
				}
				base.CopyOrMoveItems(isCopy, localItemIds);
			}
			else
			{
				base.CopyOrMoveItems(isCopy, null);
			}
			OwaStoreObjectId expId = (OwaStoreObjectId)base.GetParameter("expId");
			this.InternalExpandConversation(expId);
		}

		// Token: 0x06002E9B RID: 11931 RVA: 0x0010A4EC File Offset: 0x001086EC
		private void DeleteConversationsOrItems(bool isPermanent)
		{
			DeleteItemInfo[] array = (DeleteItemInfo[])base.GetParameter("Itms");
			OwaStoreObjectId[] array2 = (OwaStoreObjectId[])base.GetParameter("Cnvs");
			if (array != null && array.Length > 0)
			{
				foreach (DeleteItemInfo deleteItemInfo in array)
				{
					if (!deleteItemInfo.OwaStoreObjectId.IsOtherMailbox && !deleteItemInfo.OwaStoreObjectId.IsPublic && deleteItemInfo.OwaStoreObjectId.IsConversationId)
					{
						return;
					}
				}
				base.InternalDelete(isPermanent, null);
				return;
			}
			if (array2 != null && array2.Length > 0)
			{
				OwaStoreObjectId owaStoreObjectId = (OwaStoreObjectId)base.GetParameter("fId");
				OwaStoreObjectId[] localItemIds = ConversationUtilities.GetLocalItemIds(base.UserContext, array2, owaStoreObjectId);
				if (localItemIds.Length == 0)
				{
					return;
				}
				new List<OwaStoreObjectId>();
				new List<OwaStoreObjectId>();
				if (!Utilities.IsDefaultFolderId(base.UserContext, owaStoreObjectId, DefaultFolderType.DeletedItems) && !isPermanent)
				{
					OwaStoreObjectId[] array4 = localItemIds;
					int j = 0;
					while (j < array4.Length)
					{
						OwaStoreObjectId owaStoreObjectId2 = array4[j];
						if (owaStoreObjectId2.StoreObjectId.ObjectType == StoreObjectType.MeetingRequest)
						{
							goto IL_FD;
						}
						if (owaStoreObjectId2.StoreObjectId.ObjectType == StoreObjectType.MeetingCancellation)
						{
							goto Block_12;
						}
						IL_10E:
						j++;
						continue;
						Block_12:
						try
						{
							IL_FD:
							MeetingUtilities.DeleteMeetingMessageCalendarItem(owaStoreObjectId2.StoreObjectId);
						}
						catch (ObjectNotFoundException)
						{
						}
						goto IL_10E;
					}
				}
				base.InternalDelete(isPermanent, localItemIds);
			}
		}

		// Token: 0x06002E9C RID: 11932 RVA: 0x0010A630 File Offset: 0x00108830
		private string GetExtraWatsonData(OwaStoreObjectId[] itemIds)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("OWA Version: ");
			stringBuilder.Append(Globals.ApplicationVersion);
			stringBuilder.AppendLine();
			stringBuilder.AppendLine("Item List contains " + itemIds.Length.ToString() + " items. \r\n");
			for (int i = 0; i < itemIds.Length; i++)
			{
				stringBuilder.Append(itemIds[i].ToString() + ", ");
			}
			stringBuilder.AppendLine();
			if (base.UserContext != null && !Globals.DisableBreadcrumbs)
			{
				stringBuilder.AppendLine();
				stringBuilder.AppendLine();
				stringBuilder.AppendLine(base.UserContext.DumpBreadcrumbs());
			}
			return stringBuilder.ToString();
		}

		// Token: 0x04002047 RID: 8263
		public const string EventNamespace = "MsgVLV2";

		// Token: 0x04002048 RID: 8264
		public const string MethodMarkAsRead = "MarkAsRead";

		// Token: 0x04002049 RID: 8265
		public const string MethodMarkAsUnread = "MarkAsUnread";

		// Token: 0x0400204A RID: 8266
		public const string MethodGetSenderSmtp = "GetSenderSmtp";

		// Token: 0x0400204B RID: 8267
		public const string MethodGetDeliveryReportUrlParameters = "GetDeliveryReportUrlParameters";

		// Token: 0x0400204C RID: 8268
		public const string MethodGetELCFolderSize = "GetELCFolderSize";

		// Token: 0x0400204D RID: 8269
		public const string MethodExpandConversation = "ExpandConversation";

		// Token: 0x0400204E RID: 8270
		public const string MethodIgnoreConversations = "IgnoreConversations";

		// Token: 0x0400204F RID: 8271
		public const string MethodCancelIgnoreConversation = "CancelIgnoreConversation";

		// Token: 0x04002050 RID: 8272
		public const string MethodGetConversationAction = "GetConversationAction";

		// Token: 0x04002051 RID: 8273
		public const string Conversations = "Cnvs";

		// Token: 0x04002052 RID: 8274
		public const string MethodAddFilterToFavorites = "AddFilterToFavorites";

		// Token: 0x04002053 RID: 8275
		public const string MethodGetFilterMenu = "GetFilterMenu";

		// Token: 0x04002054 RID: 8276
		public const string MethodPreread = "Preread";

		// Token: 0x04002055 RID: 8277
		public const string RemoveSubscriptions = "RemoveSubscriptions";

		// Token: 0x04002056 RID: 8278
		public const string FilterSubject = "fltrSbj";

		// Token: 0x04002057 RID: 8279
		public const string ExpandedConversationId = "expId";

		// Token: 0x04002058 RID: 8280
		private OwaStoreObjectId newSeekId;
	}
}
