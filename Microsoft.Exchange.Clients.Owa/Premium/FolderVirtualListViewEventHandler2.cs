using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;
using Microsoft.Exchange.Clients.Owa.Core;
using Microsoft.Exchange.Clients.Owa.Core.Controls;
using Microsoft.Exchange.Clients.Owa.Premium.Controls;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Search;
using Microsoft.Exchange.Data.Search.AqsParser;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Data.Storage.Conversations;
using Microsoft.Exchange.Diagnostics.Components.Clients;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.Clients.Owa.Premium
{
	// Token: 0x02000487 RID: 1159
	internal abstract class FolderVirtualListViewEventHandler2 : VirtualListViewEventHandler2
	{
		// Token: 0x17000D28 RID: 3368
		// (get) Token: 0x06002CB6 RID: 11446 RVA: 0x000FA59D File Offset: 0x000F879D
		protected Folder ContextFolder
		{
			get
			{
				return this.folder;
			}
		}

		// Token: 0x17000D29 RID: 3369
		// (get) Token: 0x06002CB7 RID: 11447 RVA: 0x000FA5A5 File Offset: 0x000F87A5
		protected Folder DataFolderForSearch
		{
			get
			{
				return this.filteredFolder ?? this.folder;
			}
		}

		// Token: 0x17000D2A RID: 3370
		// (get) Token: 0x06002CB8 RID: 11448 RVA: 0x000FA5B7 File Offset: 0x000F87B7
		protected Folder FilteredFolder
		{
			get
			{
				return this.filteredFolder;
			}
		}

		// Token: 0x17000D2B RID: 3371
		// (get) Token: 0x06002CB9 RID: 11449 RVA: 0x000FA5BF File Offset: 0x000F87BF
		protected Folder DataFolder
		{
			get
			{
				Folder result;
				if ((result = this.searchFolder) == null)
				{
					result = (this.filteredFolder ?? this.folder);
				}
				return result;
			}
		}

		// Token: 0x17000D2C RID: 3372
		// (get) Token: 0x06002CBA RID: 11450 RVA: 0x000FA5DB File Offset: 0x000F87DB
		// (set) Token: 0x06002CBB RID: 11451 RVA: 0x000FA5E3 File Offset: 0x000F87E3
		protected QueryFilter FolderQueryFilter
		{
			get
			{
				return this.folderQueryFilter;
			}
			set
			{
				this.folderQueryFilter = value;
			}
		}

		// Token: 0x17000D2D RID: 3373
		// (get) Token: 0x06002CBC RID: 11452 RVA: 0x000FA5EC File Offset: 0x000F87EC
		protected SearchScope SearchScope
		{
			get
			{
				if (this.folder is SearchFolder)
				{
					return SearchScope.AllFoldersAndItems;
				}
				if (this.IsFilteredView)
				{
					return SearchScope.SelectedFolder;
				}
				if (this.IsFiltered)
				{
					FolderVirtualListViewSearchFilter folderVirtualListViewSearchFilter = (FolderVirtualListViewSearchFilter)base.GetParameter("srchf");
					return folderVirtualListViewSearchFilter.Scope;
				}
				return SearchScope.SelectedFolder;
			}
		}

		// Token: 0x17000D2E RID: 3374
		// (get) Token: 0x06002CBD RID: 11453 RVA: 0x000FA633 File Offset: 0x000F8833
		protected bool IsFilteredView
		{
			get
			{
				return base.IsParameterSet("fldrfltr") && this.FilterCondition.IsValidFilter();
			}
		}

		// Token: 0x17000D2F RID: 3375
		// (get) Token: 0x06002CBE RID: 11454 RVA: 0x000FA650 File Offset: 0x000F8850
		protected FolderVirtualListViewFilter FilterCondition
		{
			get
			{
				if (this.filterObject == null)
				{
					this.filterObject = (FolderVirtualListViewFilter)base.GetParameter("fldrfltr");
					if (this.filterObject != null)
					{
						if (!string.IsNullOrEmpty(this.filterObject.To) && this.filterObject.To.Length > 255)
						{
							this.filterObject.To = this.filterObject.To.Substring(0, 255);
						}
						if (!string.IsNullOrEmpty(this.filterObject.From) && this.filterObject.From.Length > 255)
						{
							this.filterObject.From = this.filterObject.From.Substring(0, 255);
						}
					}
				}
				return this.filterObject;
			}
		}

		// Token: 0x17000D30 RID: 3376
		// (get) Token: 0x06002CBF RID: 11455 RVA: 0x000FA723 File Offset: 0x000F8923
		protected bool IsFiltered
		{
			get
			{
				return base.IsParameterSet("srchf");
			}
		}

		// Token: 0x17000D31 RID: 3377
		// (get) Token: 0x06002CC0 RID: 11456 RVA: 0x000FA730 File Offset: 0x000F8930
		protected override VirtualListViewState ListViewState
		{
			get
			{
				return (FolderVirtualListViewState)base.GetParameter("St");
			}
		}

		// Token: 0x06002CC1 RID: 11457 RVA: 0x000FA742 File Offset: 0x000F8942
		[OwaEventParameter("SR", typeof(int))]
		[OwaEventVerb(OwaEventVerb.Post)]
		[OwaEventParameter("RC", typeof(int))]
		[OwaEventParameter("St", typeof(FolderVirtualListViewState))]
		[OwaEventParameter("srchf", typeof(FolderVirtualListViewSearchFilter), false, true)]
		[OwaEventParameter("fltr", typeof(int), false, true)]
		[OwaEvent("LoadFresh")]
		public override void LoadFresh()
		{
			base.InternalLoadFresh();
		}

		// Token: 0x06002CC2 RID: 11458 RVA: 0x000FA74A File Offset: 0x000F894A
		[OwaEventParameter("St", typeof(FolderVirtualListViewState))]
		[OwaEventVerb(OwaEventVerb.Post)]
		[OwaEventParameter("RC", typeof(int))]
		[OwaEvent("LoadNext")]
		[OwaEventParameter("srchf", typeof(FolderVirtualListViewSearchFilter), false, true)]
		[OwaEventParameter("fltr", typeof(int), false, true)]
		[OwaEventParameter("AId", typeof(OwaStoreObjectId))]
		public override void LoadNext()
		{
			base.InternalLoadNext();
		}

		// Token: 0x06002CC3 RID: 11459 RVA: 0x000FA752 File Offset: 0x000F8952
		[OwaEventVerb(OwaEventVerb.Post)]
		[OwaEventParameter("AId", typeof(OwaStoreObjectId))]
		[OwaEventParameter("RC", typeof(int))]
		[OwaEventParameter("St", typeof(FolderVirtualListViewState))]
		[OwaEventParameter("srchf", typeof(FolderVirtualListViewSearchFilter), false, true)]
		[OwaEventParameter("fltr", typeof(int), false, true)]
		[OwaEvent("LoadPrevious")]
		public override void LoadPrevious()
		{
			base.InternalLoadPrevious();
		}

		// Token: 0x06002CC4 RID: 11460 RVA: 0x000FA75A File Offset: 0x000F895A
		[OwaEventVerb(OwaEventVerb.Post)]
		[OwaEventParameter("SId", typeof(OwaStoreObjectId))]
		[OwaEventParameter("RC", typeof(int))]
		[OwaEventParameter("nwSel", typeof(bool), false, true)]
		[OwaEvent("SeekNext")]
		[OwaEventParameter("St", typeof(FolderVirtualListViewState))]
		[OwaEventParameter("srchf", typeof(FolderVirtualListViewSearchFilter), false, true)]
		[OwaEventParameter("fltr", typeof(int), false, true)]
		public override void SeekNext()
		{
			base.InternalSeekNext();
		}

		// Token: 0x06002CC5 RID: 11461 RVA: 0x000FA762 File Offset: 0x000F8962
		[OwaEventParameter("SId", typeof(OwaStoreObjectId))]
		[OwaEventParameter("srchf", typeof(FolderVirtualListViewSearchFilter), false, true)]
		[OwaEvent("SeekPrevious")]
		[OwaEventVerb(OwaEventVerb.Post)]
		[OwaEventParameter("nwSel", typeof(bool), false, true)]
		[OwaEventParameter("RC", typeof(int))]
		[OwaEventParameter("St", typeof(FolderVirtualListViewState))]
		[OwaEventParameter("fltr", typeof(int), false, true)]
		public override void SeekPrevious()
		{
			base.InternalSeekPrevious();
		}

		// Token: 0x06002CC6 RID: 11462 RVA: 0x000FA76A File Offset: 0x000F896A
		[OwaEventParameter("srchf", typeof(FolderVirtualListViewSearchFilter), false, true)]
		[OwaEventParameter("fltr", typeof(int), false, true)]
		[OwaEvent("Sort")]
		[OwaEventVerb(OwaEventVerb.Post)]
		[OwaEventParameter("RC", typeof(int))]
		[OwaEventParameter("St", typeof(FolderVirtualListViewState))]
		[OwaEventParameter("SId", typeof(OwaStoreObjectId), false, true)]
		public override void Sort()
		{
			base.InternalSort();
		}

		// Token: 0x06002CC7 RID: 11463 RVA: 0x000FA772 File Offset: 0x000F8972
		[OwaEventVerb(OwaEventVerb.Post)]
		[OwaEventParameter("SR", typeof(int))]
		[OwaEventParameter("RC", typeof(int))]
		[OwaEventParameter("srchf", typeof(FolderVirtualListViewSearchFilter), false, true)]
		[OwaEvent("SetML")]
		[OwaEventParameter("St", typeof(FolderVirtualListViewState))]
		public override void SetMultiLineState()
		{
			base.InternalSetMultiLineState();
		}

		// Token: 0x06002CC8 RID: 11464 RVA: 0x000FA77C File Offset: 0x000F897C
		[OwaEventParameter("fId", typeof(OwaStoreObjectId))]
		[OwaEventParameter("fShwELC", typeof(int))]
		[OwaEvent("PersistELCCommentState")]
		public void PersistELCCommentState()
		{
			if (base.UserContext.IsWebPartRequest)
			{
				return;
			}
			int num = (int)base.GetParameter("fShwELC");
			this.BindToFolder();
			int num2 = 0;
			object obj = this.folder.TryGetProperty(FolderSchema.ExtendedFolderFlags);
			if (!(obj is PropertyError))
			{
				num2 = (int)obj;
			}
			if (num == 0)
			{
				num2 |= 32;
			}
			else
			{
				num2 &= -33;
			}
			this.folder[FolderSchema.ExtendedFolderFlags] = num2;
			this.folder.Save();
		}

		// Token: 0x06002CC9 RID: 11465 RVA: 0x000FA803 File Offset: 0x000F8A03
		[OwaEventParameter("srchf", typeof(FolderVirtualListViewSearchFilter), false, true)]
		[OwaEventVerb(OwaEventVerb.Post)]
		[OwaEventParameter("td", typeof(string))]
		[OwaEventParameter("RC", typeof(int))]
		[OwaEventParameter("St", typeof(FolderVirtualListViewState))]
		[OwaEvent("TypeDown")]
		[OwaEventParameter("fltr", typeof(int), false, true)]
		public override void TypeDownSearch()
		{
			base.InternalTypeDownSearch();
		}

		// Token: 0x06002CCA RID: 11466 RVA: 0x000FA80B File Offset: 0x000F8A0B
		[OwaEventParameter("fId", typeof(OwaStoreObjectId))]
		[OwaEventParameter("RTOcc", typeof(OwaStoreObjectId), true, true)]
		[OwaEvent("Delete")]
		[OwaEventParameter("Itms", typeof(DeleteItemInfo), true)]
		public virtual void Delete()
		{
			ExTraceGlobals.MailCallTracer.TraceDebug((long)this.GetHashCode(), "FolderVirtualListViewEventHandler.Delete");
			this.InternalDelete(false, null);
		}

		// Token: 0x06002CCB RID: 11467 RVA: 0x000FA82B File Offset: 0x000F8A2B
		[OwaEventParameter("RTOcc", typeof(OwaStoreObjectId), true, true)]
		[OwaEvent("PermanentDelete")]
		[OwaEventParameter("Itms", typeof(DeleteItemInfo), true)]
		[OwaEventParameter("JS", typeof(JunkEmailStatus), false, true)]
		[OwaEventParameter("fId", typeof(OwaStoreObjectId))]
		public virtual void PermanentDelete()
		{
			ExTraceGlobals.MailCallTracer.TraceDebug((long)this.GetHashCode(), "FolderVirtualListViewEventHandler.PermanentDelete");
			this.InternalDelete(true, null);
		}

		// Token: 0x06002CCC RID: 11468 RVA: 0x000FA84B File Offset: 0x000F8A4B
		[OwaEventParameter("fAddToMRU", typeof(bool), false, true)]
		[OwaEvent("Move")]
		[OwaEventParameter("id", typeof(OwaStoreObjectId), true)]
		[OwaEventParameter("fId", typeof(OwaStoreObjectId), false, true)]
		[OwaEventParameter("destId", typeof(OwaStoreObjectId))]
		public virtual void Move()
		{
			ExTraceGlobals.MailCallTracer.TraceDebug((long)this.GetHashCode(), "FolderVirtualListViewEventHandler.Move");
			this.CheckSizeOfBatchOperation(null);
			this.CopyOrMoveItems(false, null);
		}

		// Token: 0x06002CCD RID: 11469 RVA: 0x000FA872 File Offset: 0x000F8A72
		[OwaEvent("Copy")]
		[OwaEventParameter("destId", typeof(OwaStoreObjectId))]
		[OwaEventParameter("id", typeof(OwaStoreObjectId), true)]
		[OwaEventParameter("fAddToMRU", typeof(bool), false, true)]
		[OwaEventParameter("fId", typeof(OwaStoreObjectId), false, true)]
		public virtual void Copy()
		{
			ExTraceGlobals.MailCallTracer.TraceDebug((long)this.GetHashCode(), "FolderVirtualListViewEventHandler.Copy");
			this.CheckSizeOfBatchOperation(null);
			this.CopyOrMoveItems(true, null);
		}

		// Token: 0x06002CCE RID: 11470 RVA: 0x000FA89C File Offset: 0x000F8A9C
		[OwaEventParameter("fId", typeof(OwaStoreObjectId))]
		[OwaEvent("AddToMru")]
		public void AddToMru()
		{
			ExTraceGlobals.MailCallTracer.TraceDebug((long)this.GetHashCode(), "FolderVirtualListViewEventHandler.AddToMru");
			this.Writer.Write('[');
			OwaStoreObjectId folderId = (OwaStoreObjectId)base.GetParameter("fId");
			IList<TargetFolderMRUEntry> list = TargetFolderMRU.AddAndGetFolders(folderId, base.UserContext);
			for (int i = 0; i < list.Count; i++)
			{
				if (i > 0)
				{
					this.Writer.Write(',');
				}
				this.Writer.Write('"');
				this.Writer.Write(Utilities.JavascriptEncode(list[i].FolderId));
				this.Writer.Write('"');
			}
			this.Writer.Write(']');
		}

		// Token: 0x06002CCF RID: 11471 RVA: 0x000FA950 File Offset: 0x000F8B50
		[OwaEvent("GetCopyMoveMenu")]
		public void GetCopyMoveMenu()
		{
			ExTraceGlobals.MailCallTracer.TraceDebug((long)this.GetHashCode(), "FolderVirtualListViewEventHandler.GetCopyMoveMenu");
			CopyMoveContextMenu copyMoveContextMenu = new CopyMoveContextMenu(base.UserContext);
			copyMoveContextMenu.Render(this.Writer);
		}

		// Token: 0x06002CD0 RID: 11472 RVA: 0x000FA98B File Offset: 0x000F8B8B
		[OwaEvent("PersistWidth")]
		[OwaEventParameter("fId", typeof(ObjectId))]
		[OwaEventParameter("w", typeof(int))]
		public void PersistWidth()
		{
			ExTraceGlobals.MailCallTracer.TraceDebug((long)this.GetHashCode(), "FolderVirtualListViewEventHandler.PersistWidth");
			this.PersistWidthOrHeight(true);
		}

		// Token: 0x06002CD1 RID: 11473 RVA: 0x000FA9AA File Offset: 0x000F8BAA
		[OwaEventParameter("h", typeof(int))]
		[OwaEventParameter("fId", typeof(ObjectId))]
		[OwaEvent("PersistHeight")]
		public void PersistHeight()
		{
			ExTraceGlobals.MailCallTracer.TraceDebug((long)this.GetHashCode(), "FolderVirtualListViewEventHandler.PersistHeight");
			this.PersistWidthOrHeight(false);
		}

		// Token: 0x06002CD2 RID: 11474 RVA: 0x000FA9CC File Offset: 0x000F8BCC
		[OwaEvent("PersistReadingPane")]
		[OwaEventParameter("s", typeof(ReadingPanePosition))]
		[OwaEventParameter("fId", typeof(ObjectId))]
		public virtual void PersistReadingPane()
		{
			ExTraceGlobals.MailCallTracer.TraceDebug((long)this.GetHashCode(), "FolderVirtualListViewEventHandler.PersistReadingPane");
			OwaStoreObjectId folderId = (OwaStoreObjectId)base.GetParameter("fId");
			using (this.folder = Utilities.GetFolder<Folder>(base.UserContext, folderId, new PropertyDefinition[]
			{
				StoreObjectSchema.EffectiveRights
			}))
			{
				if (Utilities.IsPublic(this.folder) || Utilities.IsOtherMailbox(this.folder) || Utilities.CanModifyFolderProperties(this.folder))
				{
					FolderViewStates folderViewStates = base.UserContext.GetFolderViewStates(this.folder);
					folderViewStates.ReadingPanePosition = (ReadingPanePosition)base.GetParameter("s");
					folderViewStates.Save();
				}
			}
		}

		// Token: 0x06002CD3 RID: 11475 RVA: 0x000FAA9C File Offset: 0x000F8C9C
		[OwaEventParameter("isNewestOnTop", typeof(bool))]
		[OwaEvent("PersistReadingPaneSortOrder")]
		public virtual void PersistReadingPaneSortOrder()
		{
			ExTraceGlobals.MailCallTracer.TraceDebug((long)this.GetHashCode(), "FolderVirtualListViewEventHandler.PersistReadingPaneSortOrder");
			bool flag = (bool)base.GetParameter("isNewestOnTop");
			base.UserContext.UserOptions.ConversationSortOrder = (flag ? ConversationSortOrder.ChronologicalNewestOnTop : ConversationSortOrder.ChronologicalNewestOnBottom);
			base.UserContext.UserOptions.CommitChanges();
		}

		// Token: 0x06002CD4 RID: 11476 RVA: 0x000FAAF8 File Offset: 0x000F8CF8
		[OwaEventParameter("showTreeInListView", typeof(bool))]
		[OwaEvent("PersistShowTreeInListView")]
		public virtual void PersistShowTree()
		{
			ExTraceGlobals.MailCallTracer.TraceDebug((long)this.GetHashCode(), "FolderVirtualListViewEventHandler.PersistShowTree");
			bool showTreeInListView = (bool)base.GetParameter("showTreeInListView");
			base.UserContext.UserOptions.ShowTreeInListView = showTreeInListView;
			base.UserContext.UserOptions.CommitChanges();
		}

		// Token: 0x06002CD5 RID: 11477 RVA: 0x000FAB50 File Offset: 0x000F8D50
		internal void InternalRespondToMeetingRequest(MeetingRequest meetingRequest, FolderVirtualListViewEventHandler2.RespondToMeetingRequestDelegate dele)
		{
			try
			{
				dele();
			}
			catch (CorrelationFailedException ex)
			{
				if (ex.InnerException is NotSupportedWithServerVersionException)
				{
					string displayName = meetingRequest.ReceivedRepresenting.DisplayName;
					throw new OwaRespondOlderVersionMeetingException("Cannot respond to meeting request shared from older version", displayName);
				}
				throw;
			}
		}

		// Token: 0x06002CD6 RID: 11478 RVA: 0x000FAD34 File Offset: 0x000F8F34
		[OwaEventParameter("Rsp", typeof(ResponseType))]
		[OwaEventParameter("id", typeof(OwaStoreObjectId))]
		[OwaEvent("ER")]
		public virtual void EditMeetingResponse()
		{
			FolderVirtualListViewEventHandler2.<>c__DisplayClass1 CS$<>8__locals1 = new FolderVirtualListViewEventHandler2.<>c__DisplayClass1();
			CS$<>8__locals1.<>4__this = this;
			ExTraceGlobals.MailCallTracer.TraceDebug((long)this.GetHashCode(), "FolderVirtualListViewEventHandler.EditMeetingResponse");
			CS$<>8__locals1.owaStoreObjectId = (OwaStoreObjectId)base.GetParameter("id");
			if (CS$<>8__locals1.owaStoreObjectId.IsPublic)
			{
				throw new OwaInvalidRequestException("Cannot response to a meeting request from public folder");
			}
			CS$<>8__locals1.responseType = (ResponseType)base.GetParameter("Rsp");
			using (MeetingRequest meetingRequest = Utilities.GetItem<MeetingRequest>(base.UserContext, CS$<>8__locals1.owaStoreObjectId, new PropertyDefinition[0]))
			{
				meetingRequest.OpenAsReadWrite();
				this.InternalRespondToMeetingRequest(meetingRequest, delegate
				{
					using (MeetingResponse meetingResponse = MeetingUtilities.EditResponse(CS$<>8__locals1.responseType, meetingRequest))
					{
						meetingResponse.Load();
						meetingRequest.Load();
						CS$<>8__locals1.<>4__this.Writer.Write("?ae=Item&a=New&s=Draft&t=");
						CS$<>8__locals1.<>4__this.Writer.Write(Utilities.UrlEncode(meetingResponse.ClassName));
						CS$<>8__locals1.<>4__this.Writer.Write("&id=");
						CS$<>8__locals1.<>4__this.Writer.Write(Utilities.UrlEncode(Utilities.GetIdAsString(meetingResponse)));
						CS$<>8__locals1.<>4__this.Writer.Write("&r=");
						TextWriter writer = CS$<>8__locals1.<>4__this.Writer;
						int responseType = (int)CS$<>8__locals1.responseType;
						writer.Write(responseType.ToString(CultureInfo.InvariantCulture));
						CS$<>8__locals1.<>4__this.Writer.Write("&mid=");
						CS$<>8__locals1.<>4__this.Writer.Write(Utilities.UrlEncode(CS$<>8__locals1.owaStoreObjectId.ToString()));
						if (Utilities.IsItemInDefaultFolder(meetingRequest, DefaultFolderType.DeletedItems))
						{
							CS$<>8__locals1.<>4__this.Writer.Write("&d=1");
						}
					}
				});
			}
		}

		// Token: 0x06002CD7 RID: 11479 RVA: 0x000FAE50 File Offset: 0x000F9050
		[OwaEvent("SR")]
		[OwaEventParameter("id", typeof(OwaStoreObjectId))]
		[OwaEventParameter("Rsp", typeof(ResponseType))]
		public virtual void SendMeetingResponse()
		{
			ExTraceGlobals.MailCallTracer.TraceDebug((long)this.GetHashCode(), "FolderVirtualListViewEventHandler.SendMeetingResponse");
			OwaStoreObjectId owaStoreObjectId = (OwaStoreObjectId)base.GetParameter("id");
			using (MeetingRequest meetingRequest = Utilities.GetItem<MeetingRequest>(base.UserContext, owaStoreObjectId, new PropertyDefinition[0]))
			{
				meetingRequest.OpenAsReadWrite();
				this.InternalRespondToMeetingRequest(meetingRequest, delegate
				{
					MeetingUtilities.NonEditResponse((ResponseType)this.GetParameter("Rsp"), meetingRequest, true);
				});
			}
		}

		// Token: 0x06002CD8 RID: 11480 RVA: 0x000FAF28 File Offset: 0x000F9128
		[OwaEventParameter("id", typeof(OwaStoreObjectId))]
		[OwaEvent("DR")]
		[OwaEventParameter("Rsp", typeof(ResponseType))]
		public virtual void DontSendMeetingResponse()
		{
			ExTraceGlobals.MailCallTracer.TraceDebug((long)this.GetHashCode(), "FolderVirtualListViewEventHandler.DontSendMeetingResponse");
			OwaStoreObjectId owaStoreObjectId = (OwaStoreObjectId)base.GetParameter("id");
			using (MeetingRequest meetingRequest = Utilities.GetItem<MeetingRequest>(base.UserContext, owaStoreObjectId, new PropertyDefinition[0]))
			{
				meetingRequest.OpenAsReadWrite();
				this.InternalRespondToMeetingRequest(meetingRequest, delegate
				{
					MeetingUtilities.NonEditResponse((ResponseType)this.GetParameter("Rsp"), meetingRequest, false);
				});
			}
		}

		// Token: 0x06002CD9 RID: 11481 RVA: 0x000FAFD4 File Offset: 0x000F91D4
		[OwaEvent("flgItms")]
		[OwaEventParameter("flga", typeof(FlagAction))]
		[OwaEventParameter("Itms", typeof(ObjectId), true)]
		[OwaEventParameter("ddt", typeof(ExDateTime), false, true)]
		public void FlagItems()
		{
			ExTraceGlobals.MailCallTracer.TraceDebug((long)this.GetHashCode(), "FolderVirtualListViewEventHandler.FlagItems");
			FlagAction flagAction = (FlagAction)base.GetParameter("flga");
			OwaStoreObjectId[] array = (OwaStoreObjectId[])base.GetParameter("Itms");
			ExDateTime? dueDate = null;
			if (array.Length == 0)
			{
				throw new OwaInvalidRequestException("There must be at least one item to flag");
			}
			bool[] array2 = new bool[array.Length];
			if (flagAction == FlagAction.SpecificDate)
			{
				if (!base.IsParameterSet("ddt"))
				{
					throw new OwaInvalidRequestException("Due date must be provided if specifying a specific due date");
				}
				dueDate = new ExDateTime?((ExDateTime)base.GetParameter("ddt"));
			}
			for (int i = 0; i < array.Length; i++)
			{
				OwaStoreObjectId[] array3 = null;
				if (array[i].IsConversationId)
				{
					OwaStoreObjectId owaStoreObjectId = array[i];
					MailboxSession mailboxSession = (MailboxSession)owaStoreObjectId.GetSession(base.UserContext);
					Conversation conversation = Conversation.Load(mailboxSession, owaStoreObjectId.ConversationId, base.UserContext.IsIrmEnabled, new PropertyDefinition[]
					{
						ItemSchema.Id,
						StoreObjectSchema.ParentItemId,
						ItemSchema.ReceivedTime,
						ItemSchema.FlagStatus
					});
					conversation.ConversationTree.Sort(ConversationTreeSortOrder.ChronologicalDescending);
					bool flag = false;
					if (flagAction == FlagAction.MarkComplete || flagAction == FlagAction.ClearFlag)
					{
						FlagStatus[] flagsStatus = (flagAction == FlagAction.ClearFlag) ? new FlagStatus[]
						{
							FlagStatus.Complete,
							FlagStatus.Flagged
						} : new FlagStatus[]
						{
							FlagStatus.Flagged
						};
						IList<StoreObjectId> flagedItems = ConversationUtilities.GetFlagedItems(mailboxSession, conversation, owaStoreObjectId.ParentFolderId, flagsStatus);
						if (flagedItems.Count > 0)
						{
							array3 = new OwaStoreObjectId[flagedItems.Count];
							for (int j = 0; j < array3.Length; j++)
							{
								array3[j] = OwaStoreObjectId.CreateFromStoreObjectId(flagedItems[j], owaStoreObjectId);
							}
							flag = true;
						}
					}
					if (flagAction != FlagAction.ClearFlag && !flag)
					{
						StoreObjectId latestMessage = ConversationUtilities.GetLatestMessage(mailboxSession, conversation, owaStoreObjectId.ParentFolderId);
						if (latestMessage != null)
						{
							array3 = new OwaStoreObjectId[]
							{
								OwaStoreObjectId.CreateFromStoreObjectId(latestMessage, owaStoreObjectId)
							};
						}
					}
				}
				else
				{
					array3 = new OwaStoreObjectId[]
					{
						array[i]
					};
				}
				if (array3 != null)
				{
					foreach (OwaStoreObjectId owaStoreObjectId2 in array3)
					{
						if (owaStoreObjectId2 == null)
						{
							array2[i] = false;
						}
						else
						{
							try
							{
								using (Item item = Utilities.GetItem<Item>(base.UserContext, owaStoreObjectId2, new PropertyDefinition[]
								{
									ItemSchema.UtcDueDate
								}))
								{
									MessageItem messageItem = item as MessageItem;
									if (messageItem == null || !messageItem.IsDraft)
									{
										if (!(item is CalendarItemBase))
										{
											if (item is Task && flagAction == FlagAction.ClearFlag)
											{
												OperationResult operationResult = Utilities.Delete(base.UserContext, Utilities.IsPublic(item), new OwaStoreObjectId[]
												{
													OwaStoreObjectId.CreateFromStoreObject(item)
												}).OperationResult;
												array2[i] = (operationResult == OperationResult.Succeeded);
											}
											else
											{
												item.OpenAsReadWrite();
												switch (flagAction)
												{
												case FlagAction.MarkComplete:
													FlagEventHandler.FlagComplete(item);
													break;
												case FlagAction.ClearFlag:
													FlagEventHandler.ClearFlag(item);
													break;
												default:
													dueDate = FlagEventHandler.SetFlag(item, flagAction, dueDate);
													break;
												}
												item.Save(SaveMode.ResolveConflicts);
												array2[i] = true;
											}
										}
									}
								}
							}
							catch (StorageTransientException)
							{
								array2[i] = false;
							}
							catch (StoragePermanentException)
							{
								array2[i] = false;
							}
						}
					}
				}
			}
			this.Writer.Write("var dtDD = ");
			if (dueDate != null)
			{
				this.Writer.Write("new Date(\"");
				this.Writer.Write(DateTimeUtilities.GetJavascriptDate(dueDate.Value));
				this.Writer.Write("\");");
			}
			else
			{
				this.Writer.Write("0;");
			}
			this.RenderGroupOperationResult(array2, -115311901, -1537113578);
		}

		// Token: 0x06002CDA RID: 11482 RVA: 0x000FB3B4 File Offset: 0x000F95B4
		[OwaEvent("catItms")]
		[OwaEventParameter("fId", typeof(OwaStoreObjectId))]
		[OwaEventParameter("catAdd", typeof(string), true, true)]
		[OwaEventParameter("catRem", typeof(string), true, true)]
		[OwaEventParameter("Itms", typeof(OwaStoreObjectId), true)]
		public void CategorizeItems()
		{
			ExTraceGlobals.MailCallTracer.TraceDebug((long)this.GetHashCode(), "FolderVirtualListViewEventHandler.CategorizeItems");
			OwaStoreObjectId[] array = (OwaStoreObjectId[])base.GetParameter("Itms");
			OwaStoreObjectId folderId = (OwaStoreObjectId)base.GetParameter("fId");
			string[] addCategories = (string[])base.GetParameter("catAdd");
			string[] removeCategories = (string[])base.GetParameter("catRem");
			bool[] array2 = new bool[array.Length];
			this.Writer.Write("var rgItmCats = new Array(");
			for (int i = 0; i < array.Length; i++)
			{
				string s = string.Empty;
				string[] array3 = null;
				try
				{
					OwaStoreObjectId owaStoreObjectId;
					if (array[i].IsConversationId)
					{
						owaStoreObjectId = this.UpdateConversationCategories(array[i], folderId, addCategories, removeCategories);
					}
					else
					{
						owaStoreObjectId = array[i];
					}
					using (Item item = Utilities.GetItem<Item>(base.UserContext, owaStoreObjectId, new PropertyDefinition[0]))
					{
						item.OpenAsReadWrite();
						CategoryContextMenu.ModifyCategories(item, addCategories, removeCategories);
						array3 = ItemUtility.GetProperty<string[]>(item, ItemSchema.Categories, null);
						StringBuilder stringBuilder = new StringBuilder();
						StringWriter stringWriter = new StringWriter(stringBuilder, CultureInfo.InvariantCulture);
						CategorySwatch.RenderViewCategorySwatches(stringWriter, base.UserContext, item, folderId);
						stringWriter.Close();
						s = stringBuilder.ToString();
						MeetingMessage meetingMessage = item as MeetingMessage;
						if (meetingMessage != null)
						{
							CalendarItemBase calendarItemBase = MeetingUtilities.TryGetCorrelatedItem(meetingMessage);
							if (calendarItemBase != null && !(calendarItemBase is CalendarItemOccurrence))
							{
								CategoryContextMenu.ModifyCategories(calendarItemBase, addCategories, removeCategories);
								Utilities.SaveItem(calendarItemBase);
							}
						}
						item.Save(SaveMode.ResolveConflicts);
						array2[i] = true;
					}
				}
				catch (StorageTransientException)
				{
					array2[i] = false;
				}
				catch (StoragePermanentException)
				{
					array2[i] = false;
				}
				finally
				{
					if (0 < i)
					{
						this.Writer.Write(",");
					}
					this.Writer.Write("ci(\"");
					Utilities.JavascriptEncode(s, this.Writer);
					this.Writer.Write("\",\"");
					if (array3 != null)
					{
						for (int j = 0; j < array3.Length; j++)
						{
							if (j != 0)
							{
								this.Writer.Write("; ");
							}
							Utilities.JavascriptEncode(array3[j], this.Writer);
						}
					}
					this.Writer.Write("\")");
				}
			}
			this.Writer.Write(");");
			this.RenderGroupOperationResult(array2, -1750619075, 1748200060);
		}

		// Token: 0x06002CDB RID: 11483 RVA: 0x000FB634 File Offset: 0x000F9834
		[OwaEventParameter("Itms", typeof(OwaStoreObjectId), true)]
		[OwaEvent("catClr")]
		public void ClearCategories()
		{
			ExTraceGlobals.MailCallTracer.TraceDebug((long)this.GetHashCode(), "FolderVirtualListViewEventHandler.ClearCategories");
			OwaStoreObjectId[] array = (OwaStoreObjectId[])base.GetParameter("Itms");
			bool[] array2 = new bool[array.Length];
			for (int i = 0; i < array.Length; i++)
			{
				List<OwaStoreObjectId> list = new List<OwaStoreObjectId>();
				if (array[i].IsConversationId)
				{
					ConversationUtilities.ClearAlwaysCategorizeRule(base.UserContext, array[i]);
					list.AddRange(ConversationUtilities.GetAllItemIds(base.UserContext, new OwaStoreObjectId[]
					{
						array[i]
					}, new StoreObjectId[0]));
				}
				else
				{
					list.Add(array[i]);
				}
				try
				{
					foreach (OwaStoreObjectId owaStoreObjectId in list)
					{
						using (Item item = Utilities.GetItem<Item>(base.UserContext, owaStoreObjectId, new PropertyDefinition[0]))
						{
							item.OpenAsReadWrite();
							CategoryContextMenu.ClearCategories(item);
							item.Save(SaveMode.ResolveConflicts);
							array2[i] = true;
						}
					}
				}
				catch (StorageTransientException)
				{
					array2[i] = false;
				}
				catch (StoragePermanentException)
				{
					array2[i] = false;
				}
			}
			this.Writer.Write("var sECS = \"");
			StringBuilder stringBuilder = new StringBuilder();
			StringWriter stringWriter = new StringWriter(stringBuilder, CultureInfo.InvariantCulture);
			CategorySwatch.RenderSwatch(stringWriter, null);
			stringWriter.Close();
			Utilities.JavascriptEncode(stringBuilder.ToString(), this.Writer);
			this.Writer.Write("\";");
			this.RenderGroupOperationResult(array2, 1991025408, -103531289);
		}

		// Token: 0x06002CDC RID: 11484 RVA: 0x000FB7EC File Offset: 0x000F99EC
		[OwaEventParameter("fId", typeof(OwaStoreObjectId))]
		[OwaEventVerb(OwaEventVerb.Get)]
		[OwaEvent("gtAF")]
		public void GetAdvancedFind()
		{
			ExTraceGlobals.MailCallTracer.TraceDebug((long)this.GetHashCode(), "FolderVirtualListViewEventHandler.GetAdvancedFind");
			OwaStoreObjectId folderId = (OwaStoreObjectId)base.GetParameter("fId");
			this.RenderAdvancedFind(this.Writer, folderId);
		}

		// Token: 0x06002CDD RID: 11485 RVA: 0x000FB82D File Offset: 0x000F9A2D
		[OwaEvent("gtSIPF")]
		[OwaEventVerb(OwaEventVerb.Get)]
		public void GetSearchInPublicFolder()
		{
			ExTraceGlobals.MailCallTracer.TraceDebug((long)this.GetHashCode(), "FolderVirtualListViewEventHandler.GetSearchInPublicFolder");
			this.RenderSearchInPublicFolder(this.Writer);
		}

		// Token: 0x06002CDE RID: 11486 RVA: 0x000FB854 File Offset: 0x000F9A54
		[OwaEventParameter("fId", typeof(OwaStoreObjectId))]
		[OwaEvent("GetSearchScopeMenu")]
		[OwaEventVerb(OwaEventVerb.Get)]
		public void GetSearchScopeMenu()
		{
			ExTraceGlobals.MailCallTracer.TraceDebug((long)this.GetHashCode(), "FolderVirtualListViewEventHandler.GetSearchScopeMenu");
			OwaStoreObjectId owaStoreObjectId = (OwaStoreObjectId)base.GetParameter("fId");
			OutlookModule moduleForFolder;
			using (this.folder = Utilities.GetFolder<Folder>(base.UserContext, owaStoreObjectId, new PropertyDefinition[0]))
			{
				moduleForFolder = Utilities.GetModuleForFolder(this.folder, base.UserContext);
			}
			SearchScope searchScope = base.UserContext.UserOptions.GetSearchScope(moduleForFolder);
			this.Writer.Write("g_iDftSS = ");
			this.Writer.Write((int)searchScope);
			this.Writer.Write(";");
			StringBuilder stringBuilder = new StringBuilder();
			StringWriter stringWriter = new StringWriter(stringBuilder, CultureInfo.InvariantCulture);
			SearchScopeMenu searchScopeMenu = new SearchScopeMenu(base.UserContext, moduleForFolder, owaStoreObjectId.IsArchive ? Utilities.GetMailboxOwnerDisplayName((MailboxSession)owaStoreObjectId.GetSession(base.UserContext)) : null);
			searchScopeMenu.Render(stringWriter);
			stringWriter.Close();
			this.Writer.Write("var sSM = \"");
			Utilities.JavascriptEncode(stringBuilder.ToString(), this.Writer);
			this.Writer.Write("\";");
		}

		// Token: 0x06002CDF RID: 11487 RVA: 0x000FB998 File Offset: 0x000F9B98
		[OwaEventParameter("scp", typeof(SearchScope))]
		[OwaEventParameter("fId", typeof(OwaStoreObjectId))]
		[OwaEvent("PSS")]
		[OwaEventVerb(OwaEventVerb.Post)]
		public void PersistSearchScope()
		{
			ExTraceGlobals.MailCallTracer.TraceDebug((long)this.GetHashCode(), "FolderVirtualListViewEventHandler.PersistSearchScope");
			if (base.UserContext.IsWebPartRequest)
			{
				return;
			}
			OwaStoreObjectId folderId = (OwaStoreObjectId)base.GetParameter("fId");
			OutlookModule moduleForFolder;
			using (this.folder = Utilities.GetFolder<Folder>(base.UserContext, folderId, new PropertyDefinition[0]))
			{
				moduleForFolder = Utilities.GetModuleForFolder(this.folder, base.UserContext);
			}
			base.UserContext.UserOptions.SetSearchScope(moduleForFolder, (SearchScope)base.GetParameter("scp"));
			base.UserContext.UserOptions.CommitChanges();
		}

		// Token: 0x06002CE0 RID: 11488 RVA: 0x000FBA58 File Offset: 0x000F9C58
		protected static void Register()
		{
			if (!FolderVirtualListViewEventHandler2.isRegistered)
			{
				OwaEventRegistry.RegisterEnum(typeof(SearchRecipient));
				OwaEventRegistry.RegisterStruct(typeof(FolderVirtualListViewState));
				OwaEventRegistry.RegisterStruct(typeof(FolderVirtualListViewSearchFilter));
				OwaEventRegistry.RegisterStruct(typeof(FolderVirtualListViewFilter));
				FolderVirtualListViewEventHandler2.isRegistered = true;
			}
		}

		// Token: 0x06002CE1 RID: 11489 RVA: 0x000FBAB0 File Offset: 0x000F9CB0
		protected void BindToFolder()
		{
			if (this.folder == null)
			{
				OwaStoreObjectId folderId;
				if (base.IsParameterSet("St"))
				{
					folderId = (OwaStoreObjectId)this.ListViewState.SourceContainerId;
				}
				else if (base.IsParameterSet("fldrfltr"))
				{
					folderId = this.FilterCondition.SourceFolderId;
				}
				else
				{
					if (!base.IsParameterSet("fId"))
					{
						throw new OwaInvalidRequestException("Should have at least state or id.");
					}
					folderId = (OwaStoreObjectId)base.GetParameter("fId");
				}
				this.folder = Utilities.GetFolderForContent<Folder>(base.UserContext, folderId, new PropertyDefinition[]
				{
					FolderSchema.ItemCount,
					FolderSchema.UnreadCount,
					FolderSchema.ExtendedFolderFlags,
					ViewStateProperties.ViewWidth,
					ViewStateProperties.ReadingPanePosition,
					ViewStateProperties.ViewFilter,
					ViewStateProperties.SortColumn,
					ViewStateProperties.SortOrder,
					ViewStateProperties.FilteredViewLabel,
					FolderSchema.SearchFolderAllowAgeout,
					StoreObjectSchema.EffectiveRights
				});
				if (base.UserContext.IsInMyMailbox(this.folder) || Utilities.IsInArchiveMailbox(this.folder))
				{
					this.filteredFolder = this.GetFilteredView(this.folder);
				}
				if (this.IsFiltered && (base.UserContext.IsInMyMailbox(this.folder) || Utilities.IsInArchiveMailbox(this.folder)))
				{
					FolderVirtualListViewSearchFilter folderVirtualListViewSearchFilter = (FolderVirtualListViewSearchFilter)base.GetParameter("srchf");
					if (folderVirtualListViewSearchFilter.SearchString != null && 256 < folderVirtualListViewSearchFilter.SearchString.Length)
					{
						throw new OwaInvalidRequestException("Search string is longer than maximum length of " + 256);
					}
					FolderSearch folderSearch = new FolderSearch();
					string text = folderVirtualListViewSearchFilter.SearchString;
					if (folderVirtualListViewSearchFilter.ResultsIn != SearchResultsIn.DefaultFields && text != null)
					{
						text = null;
					}
					QueryFilter searchFilter = this.GetSearchFilter();
					folderSearch.AdvancedQueryFilter = FolderVirtualListViewEventHandler2.BuildAndFilter(folderSearch.AdvancedQueryFilter, searchFilter);
					this.searchFolder = folderSearch.Execute(base.UserContext, this.DataFolderForSearch, folderVirtualListViewSearchFilter.Scope, text, folderVirtualListViewSearchFilter.ReExecuteSearch, folderVirtualListViewSearchFilter.IsAsyncSearchEnabled);
				}
			}
		}

		// Token: 0x06002CE2 RID: 11490 RVA: 0x000FBCB8 File Offset: 0x000F9EB8
		protected override void PersistSort()
		{
			if (!base.UserContext.IsWebPartRequest)
			{
				this.BindToFolder();
				FolderViewStates folderViewStates = base.UserContext.GetFolderViewStates(this.folder);
				folderViewStates.SortColumn = ColumnIdParser.GetString(this.ListViewState.SortedColumn);
				folderViewStates.SortOrder = this.ListViewState.SortOrder;
				folderViewStates.Save();
				this.folder.Load();
			}
		}

		// Token: 0x06002CE3 RID: 11491 RVA: 0x000FBD24 File Offset: 0x000F9F24
		protected override void PersistMultiLineState()
		{
			if (!base.UserContext.IsWebPartRequest)
			{
				this.BindToFolder();
				FolderViewStates folderViewStates = base.UserContext.GetFolderViewStates(this.folder);
				folderViewStates.MultiLine = this.ListViewState.IsMultiLine;
				folderViewStates.Save();
				this.folder.Load();
			}
		}

		// Token: 0x06002CE4 RID: 11492
		protected abstract void RenderAdvancedFind(TextWriter writer, OwaStoreObjectId folderId);

		// Token: 0x06002CE5 RID: 11493
		protected abstract void RenderSearchInPublicFolder(TextWriter writer);

		// Token: 0x06002CE6 RID: 11494 RVA: 0x000FBD78 File Offset: 0x000F9F78
		protected void RenderAdvancedFind(TextWriter writer, AdvancedFindComponents advancedFindComponents, OwaStoreObjectId folderId)
		{
			int num = 0;
			int num2 = 28;
			writer.Write("<div id=divASExpanded>");
			if ((advancedFindComponents & AdvancedFindComponents.SubjectBody) != (AdvancedFindComponents)0)
			{
				DropDownListItem[] columnThreeDropDownListItems = new DropDownListItem[]
				{
					new DropDownListItem(3, 779520799),
					new DropDownListItem(2, -911352830),
					new DropDownListItem(1, 1732412034)
				};
				this.RenderAdvancedFindRow(writer, num, "RI", LocalizedStrings.GetHtmlEncoded(-891993694), 3.ToString(CultureInfo.InvariantCulture), columnThreeDropDownListItems);
				num += num2;
			}
			if ((advancedFindComponents & AdvancedFindComponents.SearchTextInSubject) != (AdvancedFindComponents)0 || (advancedFindComponents & AdvancedFindComponents.SearchTextInName) != (AdvancedFindComponents)0)
			{
				string columnTwoLabelId = ((advancedFindComponents & AdvancedFindComponents.SearchTextInName) != (AdvancedFindComponents)0) ? LocalizedStrings.GetHtmlEncoded(-2058911220) : LocalizedStrings.GetHtmlEncoded(-881075747);
				this.RenderAdvancedFindRow(writer, num, "SS", columnTwoLabelId);
				num += num2;
			}
			if ((advancedFindComponents & AdvancedFindComponents.FromTo) != (AdvancedFindComponents)0)
			{
				DropDownListItem[] columnTwoDropDownListItems = new DropDownListItem[]
				{
					new DropDownListItem(0, -1327933906),
					new DropDownListItem(1, 300298801)
				};
				this.RenderAdvancedFindRow(writer, num, "Rcp", 0.ToString(CultureInfo.InvariantCulture), columnTwoDropDownListItems);
				num += num2;
			}
			if ((advancedFindComponents & AdvancedFindComponents.Categories) != (AdvancedFindComponents)0)
			{
				this.RenderAdvancedFindRow(writer, num, "Cat", LocalizedStrings.GetHtmlEncoded(-1642040455), folderId);
				num += num2;
			}
			if ((advancedFindComponents & AdvancedFindComponents.SearchButton) != (AdvancedFindComponents)0)
			{
				writer.Write("<div class=\"ASRow\" style=\"top:");
				writer.Write(num);
				writer.Write("px;text-align:");
				writer.Write(base.UserContext.IsRtl ? "left" : "right");
				writer.Write("\">");
				using (StringWriter stringWriter = new StringWriter())
				{
					base.UserContext.RenderThemeImage(stringWriter, ThemeFileId.Search, null, new object[]
					{
						"id=imgBtnAF"
					});
					stringWriter.Write("<span id=\"spnAFSch\">");
					stringWriter.Write(LocalizedStrings.GetHtmlEncoded(656259478));
					stringWriter.Write("</span><span id=\"spnAFClr\" style=\"display:none;\">");
					stringWriter.Write(LocalizedStrings.GetHtmlEncoded(613695225));
					stringWriter.Write("</span>");
					RenderingUtilities.RenderButton(writer, "btnAF", "class=dsbl", string.Empty, stringWriter.ToString(), true);
				}
				writer.Write("</div>");
			}
			writer.Write("</div>");
		}

		// Token: 0x06002CE7 RID: 11495 RVA: 0x000FBFD8 File Offset: 0x000FA1D8
		private static QueryFilter BuildAndFilter(QueryFilter queryFilter1, QueryFilter queryFilter2)
		{
			if (queryFilter1 == null)
			{
				return queryFilter2;
			}
			if (queryFilter2 == null)
			{
				return queryFilter1;
			}
			return new AndFilter(new QueryFilter[]
			{
				queryFilter1,
				queryFilter2
			});
		}

		// Token: 0x06002CE8 RID: 11496 RVA: 0x000FC004 File Offset: 0x000FA204
		private void RenderAdvancedFindRow(TextWriter writer, int rowTop, string rowId, string columnTwoLabelId, string columnThreeDropDownSelectedValue, DropDownListItem[] columnThreeDropDownListItems)
		{
			this.RenderAdvancedFindRow(writer, rowTop, rowId, AdvancedFindColumnType.Label, columnTwoLabelId, null, null, AdvancedFindColumnType.DropDownList, columnThreeDropDownSelectedValue, columnThreeDropDownListItems, null);
		}

		// Token: 0x06002CE9 RID: 11497 RVA: 0x000FC028 File Offset: 0x000FA228
		private void RenderAdvancedFindRow(TextWriter writer, int rowTop, string rowId, string columnTwoLabelId)
		{
			this.RenderAdvancedFindRow(writer, rowTop, rowId, AdvancedFindColumnType.Label, columnTwoLabelId, null, null, AdvancedFindColumnType.InputBox, null, null, null);
		}

		// Token: 0x06002CEA RID: 11498 RVA: 0x000FC048 File Offset: 0x000FA248
		private void RenderAdvancedFindRow(TextWriter writer, int rowTop, string rowId, string columnTwoDropDownSelectedValue, DropDownListItem[] columnTwoDropDownListItems)
		{
			this.RenderAdvancedFindRow(writer, rowTop, rowId, AdvancedFindColumnType.DropDownList, null, columnTwoDropDownSelectedValue, columnTwoDropDownListItems, AdvancedFindColumnType.InputBox, null, null, null);
		}

		// Token: 0x06002CEB RID: 11499 RVA: 0x000FC068 File Offset: 0x000FA268
		private void RenderAdvancedFindRow(TextWriter writer, int rowTop, string rowId, string columnTwoLabelId, OwaStoreObjectId folderId)
		{
			this.RenderAdvancedFindRow(writer, rowTop, rowId, AdvancedFindColumnType.Label, columnTwoLabelId, null, null, AdvancedFindColumnType.CategoryDropDownList, null, null, folderId);
		}

		// Token: 0x06002CEC RID: 11500 RVA: 0x000FC088 File Offset: 0x000FA288
		private void RenderAdvancedFindRow(TextWriter writer, int rowTop, string rowId, AdvancedFindColumnType columnTwoType, string columnTwoLabelId, string columnTwoDropDownSelectedValue, DropDownListItem[] columnTwoDropDownListItems, AdvancedFindColumnType columnThreeType, string columnThreeDropDownSelectedValue, DropDownListItem[] columnThreeDropDownListItems, OwaStoreObjectId folderId)
		{
			writer.Write("<div class=ASRow style=\"top:");
			writer.Write(rowTop);
			writer.Write("px;\">");
			writer.Write("<div id=divASCol1 class=ASCol1><input type=checkbox id=");
			writer.Write("chk" + rowId);
			writer.Write("></div>");
			writer.Write("<div id=divASCol2 class=\"ASCol2");
			switch (columnTwoType)
			{
			case AdvancedFindColumnType.Label:
				writer.Write(" label nowrap\">");
				writer.Write("<label for=");
				writer.Write("chk" + rowId);
				writer.Write(">");
				writer.Write(columnTwoLabelId);
				writer.Write("</label>");
				break;
			case AdvancedFindColumnType.DropDownList:
				writer.Write("\">");
				DropDownList.RenderDropDownList(writer, "div" + rowId, columnTwoDropDownSelectedValue, columnTwoDropDownListItems);
				break;
			default:
				throw new ArgumentException("Invalid value for columnTwoType");
			}
			writer.Write("</div>");
			writer.Write("<div id=divASCol3 class=\"ASCol3");
			switch (columnThreeType)
			{
			case AdvancedFindColumnType.DropDownList:
				writer.Write("\">");
				DropDownList.RenderDropDownList(writer, "div" + rowId, columnThreeDropDownSelectedValue, columnThreeDropDownListItems);
				break;
			case AdvancedFindColumnType.CategoryDropDownList:
				writer.Write("\">");
				CategoryDropDownList.RenderCategoryDropDownList(writer, folderId);
				break;
			case AdvancedFindColumnType.InputBox:
				writer.Write(" input\">");
				writer.Write("<input type=text maxlength=256 id=");
				writer.Write("txt" + rowId);
				writer.Write(">");
				break;
			default:
				throw new ArgumentException("Invalid value for columnThreeType");
			}
			writer.Write("</div>");
			writer.Write("</div>");
		}

		// Token: 0x06002CED RID: 11501 RVA: 0x000FC228 File Offset: 0x000FA428
		protected virtual QueryFilter GetViewFilter()
		{
			FolderVirtualListViewSearchFilter folderVirtualListViewSearchFilter = (FolderVirtualListViewSearchFilter)base.GetParameter("srchf");
			if (folderVirtualListViewSearchFilter != null && (Utilities.IsPublic(this.folder) || Utilities.IsOtherMailbox(this.folder)))
			{
				QueryFilter queryFilter = this.GetSearchFilter();
				if (folderVirtualListViewSearchFilter.SearchString != null && folderVirtualListViewSearchFilter.ResultsIn == SearchResultsIn.DefaultFields && Utilities.IsOtherMailbox(this.folder))
				{
					QueryFilter queryFilter2 = this.BuildDefaultQueryFilter();
					queryFilter = FolderVirtualListViewEventHandler2.BuildAndFilter(queryFilter2, queryFilter);
				}
				return FolderVirtualListViewEventHandler2.BuildAndFilter(this.folderQueryFilter, queryFilter);
			}
			return this.folderQueryFilter;
		}

		// Token: 0x06002CEE RID: 11502 RVA: 0x000FC2AC File Offset: 0x000FA4AC
		protected QueryFilter BuildDefaultQueryFilter()
		{
			FolderVirtualListViewSearchFilter folderVirtualListViewSearchFilter = (FolderVirtualListViewSearchFilter)base.GetParameter("srchf");
			string searchString = folderVirtualListViewSearchFilter.SearchString;
			if (string.IsNullOrEmpty(searchString))
			{
				return null;
			}
			bool isContentIndexingEnabled = ((this.folder.Session as MailboxSession) ?? base.UserContext.MailboxSession).Mailbox.IsContentIndexingEnabled;
			return AqsParser.ParseAndBuildQuery(searchString, SearchFilterGenerator.GetAqsParseOption(this.folder, isContentIndexingEnabled), base.UserContext.UserCulture, RescopedAll.Default, null, new PolicyTagMailboxProvider(base.UserContext.MailboxSession));
		}

		// Token: 0x06002CEF RID: 11503 RVA: 0x000FC334 File Offset: 0x000FA534
		protected override void RenderExtraData(VirtualListView2 listView)
		{
			if (this.IsFiltered)
			{
				this.RenderSearchResultsHeader(listView);
			}
			if (this.IsFilteredView)
			{
				string s = string.Format(CultureInfo.CurrentCulture, LocalizedStrings.GetNonEncoded(2114296142), new object[]
				{
					Utilities.GetDisplayNameByFolder(this.ContextFolder, base.UserContext),
					this.FilterCondition.ToDescription()
				});
				this.Writer.Write("<div id=\"fltrDesc\">");
				Utilities.HtmlEncode(s, this.Writer, true);
				this.Writer.Write("</div>");
			}
		}

		// Token: 0x06002CF0 RID: 11504 RVA: 0x000FC3C4 File Offset: 0x000FA5C4
		protected override void EndProcessEvent()
		{
			if (this.folder != null)
			{
				this.folder.Dispose();
				this.folder = null;
			}
			if (this.filteredFolder != null)
			{
				if (this.filteredFolder.PropertyBag.HasAllPropertiesLoaded)
				{
					this.filteredFolder[ViewStateProperties.FilteredViewAccessTime] = ExDateTime.Now;
					this.filteredFolder.Save();
				}
				this.filteredFolder.Dispose();
				this.filteredFolder = null;
			}
			if (this.searchFolder != null)
			{
				this.searchFolder.Dispose();
				this.searchFolder = null;
			}
		}

		// Token: 0x06002CF1 RID: 11505 RVA: 0x000FC458 File Offset: 0x000FA658
		protected void InternalDelete(bool permanentDelete, OwaStoreObjectId[] itemIds)
		{
			OwaStoreObjectId owaStoreObjectId = (OwaStoreObjectId)base.GetParameter("fId");
			OwaStoreObjectId[] array = (OwaStoreObjectId[])base.GetParameter("RTOcc");
			OperationResult operationResult;
			if (itemIds == null)
			{
				DeleteItemInfo[] deleteItemInfos = (DeleteItemInfo[])base.GetParameter("Itms");
				operationResult = this.DeleteItems(deleteItemInfos, permanentDelete);
			}
			else
			{
				operationResult = this.DeleteItems(itemIds, permanentDelete);
			}
			OperationResult operationResult2 = OperationResult.Succeeded;
			if (array != null)
			{
				operationResult2 = this.DeleteTaskOccurrences(array, permanentDelete);
			}
			if (operationResult != OperationResult.Succeeded || operationResult2 != OperationResult.Succeeded)
			{
				this.RenderErrorResult("errDel", 166628739);
			}
		}

		// Token: 0x06002CF2 RID: 11506 RVA: 0x000FC4D8 File Offset: 0x000FA6D8
		private OperationResult DeleteItems(DeleteItemInfo[] deleteItemInfos, bool permanentDelete)
		{
			OwaStoreObjectId[] array = new OwaStoreObjectId[deleteItemInfos.Length];
			this.CheckSizeOfBatchOperation(array);
			for (int i = 0; i < deleteItemInfos.Length; i++)
			{
				DeleteItemInfo deleteItemInfo = deleteItemInfos[i];
				array[i] = deleteItemInfo.OwaStoreObjectId;
				if (deleteItemInfo.IsMeetingMessage && !deleteItemInfo.OwaStoreObjectId.IsPublic)
				{
					try
					{
						MeetingUtilities.DeleteMeetingMessageCalendarItem(deleteItemInfo.OwaStoreObjectId.StoreObjectId);
					}
					catch (ObjectNotFoundException)
					{
					}
				}
			}
			return this.DeleteItems(array, permanentDelete);
		}

		// Token: 0x06002CF3 RID: 11507 RVA: 0x000FC554 File Offset: 0x000FA754
		private OperationResult DeleteItems(OwaStoreObjectId[] itemIds, bool permanentDelete)
		{
			List<OwaStoreObjectId> list = new List<OwaStoreObjectId>();
			List<OwaStoreObjectId> list2 = new List<OwaStoreObjectId>();
			this.CheckSizeOfBatchOperation(itemIds);
			foreach (OwaStoreObjectId owaStoreObjectId in itemIds)
			{
				if (permanentDelete || Utilities.IsDefaultFolderId(base.UserContext, Utilities.GetParentFolderId(owaStoreObjectId), DefaultFolderType.DeletedItems))
				{
					list.Add(owaStoreObjectId);
				}
				else
				{
					list2.Add(owaStoreObjectId);
				}
			}
			OwaStoreObjectId[] array = list.ToArray();
			OwaStoreObjectId[] array2 = list2.ToArray();
			OperationResult operationResult = (OperationResult)0;
			if (array2.Length > 0)
			{
				operationResult |= Utilities.Delete(base.UserContext, permanentDelete, array2).OperationResult;
			}
			if (array.Length > 0)
			{
				if (itemIds[0].IsPublic || Utilities.ShouldSuppressReadReceipt(base.UserContext))
				{
					operationResult |= Utilities.Delete(base.UserContext, DeleteItemFlags.SoftDelete | DeleteItemFlags.SuppressReadReceipt, array).OperationResult;
				}
				else
				{
					OwaStoreObjectId[] array3 = null;
					OwaStoreObjectId[] array4 = null;
					JunkEmailStatus junkEmailStatus = JunkEmailStatus.NotJunk;
					if (base.IsParameterSet("JS"))
					{
						junkEmailStatus = (JunkEmailStatus)base.GetParameter("JS");
					}
					switch (junkEmailStatus)
					{
					case JunkEmailStatus.NotJunk:
						array4 = itemIds;
						break;
					case JunkEmailStatus.Junk:
						array3 = array;
						break;
					default:
						JunkEmailUtilities.SortJunkEmailIds(base.UserContext, array, out array3, out array4);
						break;
					}
					OperationResult operationResult2 = (array3 == null || array3.Length == 0) ? OperationResult.Succeeded : Utilities.Delete(base.UserContext, DeleteItemFlags.SoftDelete | DeleteItemFlags.SuppressReadReceipt, array3).OperationResult;
					OperationResult operationResult3 = (array4 == null || array4.Length == 0) ? OperationResult.Succeeded : Utilities.Delete(base.UserContext, DeleteItemFlags.SoftDelete, array4).OperationResult;
					if (operationResult2 == OperationResult.Succeeded && operationResult3 == OperationResult.Succeeded)
					{
						operationResult |= OperationResult.Succeeded;
					}
					if (operationResult2 == OperationResult.Failed && operationResult3 == OperationResult.Failed)
					{
						operationResult |= OperationResult.Failed;
					}
				}
			}
			if (operationResult != (OperationResult)0)
			{
				return operationResult;
			}
			return OperationResult.PartiallySucceeded;
		}

		// Token: 0x06002CF4 RID: 11508 RVA: 0x000FC6F0 File Offset: 0x000FA8F0
		private OperationResult DeleteTaskOccurrences(OwaStoreObjectId[] recurringTasks, bool permanentDelete)
		{
			bool flag = true;
			bool flag2 = false;
			for (int i = 0; i < recurringTasks.Length; i++)
			{
				using (Task item = Utilities.GetItem<Task>(base.UserContext, recurringTasks[i], new PropertyDefinition[0]))
				{
					if (item.Recurrence.Pattern is RegeneratingPattern)
					{
						throw new OwaInvalidRequestException("Cannot delete an occurrence of a regenerating task.");
					}
					if (item.IsLastOccurrence)
					{
						OperationResult operationResult = Utilities.Delete(base.UserContext, permanentDelete, new OwaStoreObjectId[]
						{
							recurringTasks[i]
						}).OperationResult;
						if (operationResult != OperationResult.Succeeded)
						{
							flag = false;
						}
						else
						{
							flag2 = true;
						}
					}
					else
					{
						item.OpenAsReadWrite();
						item.DeleteCurrentOccurrence();
						Utilities.SaveItem(item);
					}
				}
			}
			if (flag)
			{
				return OperationResult.Succeeded;
			}
			if (flag2)
			{
				return OperationResult.PartiallySucceeded;
			}
			return OperationResult.Failed;
		}

		// Token: 0x06002CF5 RID: 11509 RVA: 0x000FC7BC File Offset: 0x000FA9BC
		protected void CopyOrMoveItems(bool isCopy, OwaStoreObjectId[] sourceIds)
		{
			OwaStoreObjectId owaStoreObjectId = (OwaStoreObjectId)base.GetParameter("destId");
			if (sourceIds == null)
			{
				sourceIds = (OwaStoreObjectId[])base.GetParameter("id");
			}
			if (Utilities.IsELCRootFolder(owaStoreObjectId, base.UserContext))
			{
				throw new OwaInvalidRequestException("Cannot move messages to the root ELC folder.");
			}
			if (Utilities.IsExternalSharedInFolder(base.UserContext, owaStoreObjectId))
			{
				this.RenderErrorResult("errCpyMv", 995407892);
				return;
			}
			AggregateOperationResult aggregateOperationResult = Utilities.CopyOrMoveItems(base.UserContext, isCopy, owaStoreObjectId, sourceIds);
			if (aggregateOperationResult.OperationResult != OperationResult.Succeeded)
			{
				Strings.IDs errorMessage;
				if (aggregateOperationResult.OperationResult == OperationResult.Failed)
				{
					errorMessage = -2079833764;
				}
				else
				{
					errorMessage = (isCopy ? -133951803 : -1645967063);
					OwaStoreObjectId owaStoreObjectId2 = (OwaStoreObjectId)base.GetParameter("fId");
					if (owaStoreObjectId != null && owaStoreObjectId2 != null)
					{
						if (owaStoreObjectId.IsPublic || owaStoreObjectId2.IsPublic)
						{
							errorMessage = (isCopy ? 1524003350 : -551950934);
						}
						else if (owaStoreObjectId.IsOtherMailbox || owaStoreObjectId2.IsOtherMailbox)
						{
							errorMessage = (isCopy ? 198120285 : 404204505);
						}
					}
				}
				this.RenderErrorResult("errCpyMv", errorMessage);
				return;
			}
			bool flag = true;
			object parameter = base.GetParameter("fAddToMRU");
			if (parameter != null)
			{
				flag = (bool)parameter;
			}
			if (flag)
			{
				TargetFolderMRU.AddAndGetFolders(owaStoreObjectId, base.UserContext);
			}
		}

		// Token: 0x06002CF6 RID: 11510 RVA: 0x000FC8F8 File Offset: 0x000FAAF8
		private void PersistWidthOrHeight(bool isWidth)
		{
			if (!base.UserContext.IsWebPartRequest)
			{
				OwaStoreObjectId folderId = (OwaStoreObjectId)base.GetParameter("fId");
				using (this.folder = Utilities.GetFolder<Folder>(base.UserContext, folderId, new PropertyDefinition[0]))
				{
					FolderViewStates folderViewStates = base.UserContext.GetFolderViewStates(this.folder);
					try
					{
						if (isWidth)
						{
							folderViewStates.ViewWidth = (int)base.GetParameter("w");
						}
						else
						{
							folderViewStates.ViewHeight = (int)base.GetParameter("h");
						}
						folderViewStates.Save();
					}
					catch (ArgumentOutOfRangeException ex)
					{
						throw new OwaInvalidRequestException(ex.Message, ex);
					}
				}
			}
		}

		// Token: 0x06002CF7 RID: 11511 RVA: 0x000FC9C8 File Offset: 0x000FABC8
		private QueryFilter GetSearchFilter()
		{
			FolderVirtualListViewSearchFilter folderVirtualListViewSearchFilter = (FolderVirtualListViewSearchFilter)base.GetParameter("srchf");
			string searchString = folderVirtualListViewSearchFilter.SearchString;
			QueryFilter queryFilter = null;
			bool isContentIndexingEnabled = ((this.folder.Session as MailboxSession) ?? base.UserContext.MailboxSession).Mailbox.IsContentIndexingEnabled;
			if (folderVirtualListViewSearchFilter.ResultsIn != SearchResultsIn.DefaultFields && searchString != null)
			{
				RescopedAll rescopedAll = RescopedAll.Default;
				switch (folderVirtualListViewSearchFilter.ResultsIn)
				{
				case SearchResultsIn.Subject:
					rescopedAll = RescopedAll.Subject;
					break;
				case SearchResultsIn.Body:
					rescopedAll = RescopedAll.Body;
					break;
				case SearchResultsIn.BodyAndSubject:
					rescopedAll = RescopedAll.BodyAndSubject;
					break;
				}
				QueryFilter queryFilter2 = AqsParser.ParseAndBuildQuery(searchString, SearchFilterGenerator.GetAqsParseOption(this.folder, isContentIndexingEnabled), base.UserContext.UserCulture, rescopedAll, null, new PolicyTagMailboxProvider(base.UserContext.MailboxSession));
				if (queryFilter2 != null)
				{
					queryFilter = FolderVirtualListViewEventHandler2.BuildAndFilter(queryFilter, queryFilter2);
				}
			}
			if (folderVirtualListViewSearchFilter.Category != null)
			{
				ComparisonFilter queryFilter3 = new ComparisonFilter(ComparisonOperator.Equal, ItemSchema.Categories, folderVirtualListViewSearchFilter.Category);
				queryFilter = FolderVirtualListViewEventHandler2.BuildAndFilter(queryFilter, queryFilter3);
			}
			if (folderVirtualListViewSearchFilter.RecipientValue != null)
			{
				RescopedAll rescopedAll2 = RescopedAll.Default;
				switch (folderVirtualListViewSearchFilter.RecipientType)
				{
				case SearchRecipient.From:
					rescopedAll2 = RescopedAll.From;
					break;
				case SearchRecipient.SentTo:
					rescopedAll2 = RescopedAll.Participants;
					break;
				}
				QueryFilter queryFilter4 = AqsParser.ParseAndBuildQuery(folderVirtualListViewSearchFilter.RecipientValue, SearchFilterGenerator.GetAqsParseOption(this.folder, isContentIndexingEnabled), base.UserContext.UserCulture, rescopedAll2, null, new PolicyTagMailboxProvider(base.UserContext.MailboxSession));
				if (queryFilter4 != null)
				{
					queryFilter = FolderVirtualListViewEventHandler2.BuildAndFilter(queryFilter, queryFilter4);
				}
			}
			return queryFilter;
		}

		// Token: 0x06002CF8 RID: 11512 RVA: 0x000FCB2C File Offset: 0x000FAD2C
		private void RenderSearchResultsHeader(VirtualListView2 listView)
		{
			int num = listView.TotalCount;
			string displayNameByFolder = Utilities.GetDisplayNameByFolder(this.folder, base.UserContext);
			if (!Utilities.IsPublic(this.folder) && !Utilities.IsOtherMailbox(this.folder))
			{
				if (listView.GetDidLastSearchFail())
				{
					num = 0;
				}
				else
				{
					object obj = this.searchFolder.TryGetProperty(FolderSchema.SearchFolderItemCount);
					if (obj is int)
					{
						num = (int)obj;
						if (num > 1000)
						{
							num = 1000;
						}
					}
				}
			}
			this.Writer.Write("<span id=\"spnSR\">");
			FolderVirtualListViewSearchFilter folderVirtualListViewSearchFilter = (FolderVirtualListViewSearchFilter)base.GetParameter("srchf");
			switch (folderVirtualListViewSearchFilter.Scope)
			{
			case SearchScope.SelectedFolder:
				this.Writer.Write(LocalizedStrings.GetHtmlEncoded(-373089553), num, Utilities.HtmlEncode(displayNameByFolder));
				break;
			case SearchScope.SelectedAndSubfolders:
				this.Writer.Write(LocalizedStrings.GetHtmlEncoded(1952153313), num, Utilities.HtmlEncode(displayNameByFolder));
				break;
			case SearchScope.AllItemsInModule:
			{
				OutlookModule moduleForFolder = Utilities.GetModuleForFolder(this.folder, base.UserContext);
				if (moduleForFolder == OutlookModule.Contacts)
				{
					this.Writer.Write(LocalizedStrings.GetHtmlEncoded(-1478799838), num);
				}
				else
				{
					this.Writer.Write(LocalizedStrings.GetHtmlEncoded(24888175), num);
				}
				break;
			}
			case SearchScope.AllFoldersAndItems:
				this.Writer.Write(LocalizedStrings.GetHtmlEncoded(59311153), num);
				break;
			}
			this.Writer.Write("</span>");
		}

		// Token: 0x06002CF9 RID: 11513 RVA: 0x000FCCB4 File Offset: 0x000FAEB4
		protected void RenderErrorResult(string divId, Strings.IDs errorMessage)
		{
			this.Writer.Write("<div id=\"");
			this.Writer.Write(divId);
			this.Writer.Write("\" _msg=\"");
			this.Writer.Write(LocalizedStrings.GetHtmlEncoded(errorMessage));
			this.Writer.Write("\"></div>");
		}

		// Token: 0x06002CFA RID: 11514 RVA: 0x000FCD10 File Offset: 0x000FAF10
		private void RenderGroupOperationResult(bool[] operationSuccessResults, Strings.IDs partialFailureWarningMessage, Strings.IDs totalFailureWarningMessage)
		{
			bool flag = false;
			bool flag2 = true;
			this.Writer.Write("var sCR = \"");
			foreach (bool flag3 in operationSuccessResults)
			{
				this.Writer.Write(flag3 ? "1" : "0");
				if (flag3)
				{
					flag2 = false;
				}
				else
				{
					flag = true;
				}
			}
			this.Writer.Write("\";");
			if (flag)
			{
				this.Writer.Write("alrt(\"");
				if (flag2)
				{
					this.Writer.Write(LocalizedStrings.GetJavascriptEncoded(totalFailureWarningMessage));
				}
				else
				{
					this.Writer.Write(LocalizedStrings.GetJavascriptEncoded(partialFailureWarningMessage));
				}
				this.Writer.Write("\", null, Owa.BUTTON_DIALOG_ICON.WARNING, L_Wrng);");
			}
		}

		// Token: 0x06002CFB RID: 11515 RVA: 0x000FCDC8 File Offset: 0x000FAFC8
		private Folder GetFilteredView(Folder contextFolder)
		{
			if (!this.IsFilteredView)
			{
				return null;
			}
			int num = 0;
			StoreObjectId storeObjectId = null;
			bool flag = false;
			MailboxSession mailboxSession = (contextFolder.Session as MailboxSession) ?? base.UserContext.MailboxSession;
			using (Folder folder = Folder.Bind(mailboxSession, DefaultFolderType.SearchFolders))
			{
				object[][] array;
				using (QueryResult queryResult = folder.FolderQuery(FolderQueryFlags.None, null, null, FolderList.FolderTreeQueryProperties))
				{
					array = Utilities.FetchRowsFromQueryResult(queryResult, 10000);
				}
				int num2 = FolderList.FolderTreePropertyIndexes[FolderSchema.Id];
				int num3 = FolderList.FolderTreePropertyIndexes[ViewStateProperties.FilteredViewLabel];
				int num4 = FolderList.FolderTreePropertyIndexes[FolderSchema.SearchFolderAllowAgeout];
				int num5 = FolderList.FolderTreePropertyIndexes[StoreObjectSchema.LastModifiedTime];
				int num6 = FolderList.FolderTreePropertyIndexes[ViewStateProperties.FilteredViewAccessTime];
				ExDateTime other = ExDateTime.MaxValue;
				for (int i = 0; i < array.Length; i++)
				{
					FolderVirtualListViewFilter folderVirtualListViewFilter = FolderVirtualListViewFilter.ParseFromPropertyValue(array[i][num3]);
					if (folderVirtualListViewFilter != null)
					{
						if (this.FilterCondition.Equals(folderVirtualListViewFilter))
						{
							return Folder.Bind(mailboxSession, (array[i][num2] as VersionedId).ObjectId, FolderList.FolderTreeQueryProperties);
						}
						if (this.FilterCondition.EqualsIgnoreVersion(folderVirtualListViewFilter))
						{
							storeObjectId = (array[i][num2] as VersionedId).ObjectId;
							flag = true;
						}
						if (!flag && array[i][num4] is bool && (bool)array[i][num4])
						{
							num++;
							if (!folderVirtualListViewFilter.IsCurrentVersion)
							{
								storeObjectId = (array[i][num2] as VersionedId).ObjectId;
								flag = true;
							}
							else if (storeObjectId == null)
							{
								storeObjectId = (array[i][num2] as VersionedId).ObjectId;
							}
							else
							{
								ExDateTime exDateTime = ExDateTime.MinValue;
								ExDateTime exDateTime2 = ExDateTime.MinValue;
								if (!(array[i][num5] is PropertyError))
								{
									exDateTime = (ExDateTime)array[i][num5];
								}
								if (!(array[i][num6] is PropertyError))
								{
									exDateTime2 = (ExDateTime)array[i][num6];
								}
								if (exDateTime2.CompareTo(exDateTime) < 0)
								{
									exDateTime2 = exDateTime;
								}
								if (exDateTime2.CompareTo(other) < 0)
								{
									other = exDateTime2;
									storeObjectId = (array[i][num2] as VersionedId).ObjectId;
								}
							}
						}
					}
				}
			}
			SearchFolder searchFolder;
			if (flag || num >= Globals.MaximumTemporaryFilteredViewPerUser)
			{
				searchFolder = SearchFolder.Bind(mailboxSession, storeObjectId, FolderList.FolderTreeQueryProperties);
			}
			else
			{
				searchFolder = SearchFolder.Create(mailboxSession, base.UserContext.GetSearchFoldersId(mailboxSession).StoreObjectId, Utilities.GetRandomNameForTempFilteredView(base.UserContext), CreateMode.OpenIfExists);
			}
			searchFolder[FolderSchema.SearchFolderAllowAgeout] = true;
			searchFolder[ViewStateProperties.FilteredViewLabel] = this.FilterCondition.GetPropertyValueToSave();
			this.CopyFolderProperties(contextFolder, searchFolder, new PropertyDefinition[]
			{
				ViewStateProperties.ReadingPanePosition,
				ViewStateProperties.ViewWidth,
				ViewStateProperties.SortColumn,
				ViewStateProperties.SortOrder
			});
			searchFolder.Save();
			searchFolder.Load(FolderList.FolderTreeQueryProperties);
			this.FilterCondition.ApplyFilter(searchFolder, FolderList.FolderTreeQueryProperties);
			return searchFolder;
		}

		// Token: 0x06002CFC RID: 11516 RVA: 0x000FD10C File Offset: 0x000FB30C
		private void CopyFolderProperties(Folder sourceFolder, Folder targetFolder, params PropertyDefinition[] properties)
		{
			foreach (PropertyDefinition propertyDefinition in properties)
			{
				object obj = sourceFolder.TryGetProperty(propertyDefinition);
				targetFolder.SetOrDeleteProperty(propertyDefinition, (obj is PropertyError) ? null : obj);
			}
		}

		// Token: 0x06002CFD RID: 11517 RVA: 0x000FD148 File Offset: 0x000FB348
		protected void CheckSizeOfBatchOperation(OwaStoreObjectId[] sourceIds)
		{
			sourceIds = (sourceIds ?? ((OwaStoreObjectId[])base.GetParameter("id")));
			int num = sourceIds.Length;
			if (num > 500)
			{
				throw new OwaInvalidOperationException(string.Format(CultureInfo.InvariantCulture, "Copying or moving {0} item(s) in a single request is not supported", new object[]
				{
					num
				}));
			}
		}

		// Token: 0x06002CFE RID: 11518 RVA: 0x000FD1A0 File Offset: 0x000FB3A0
		private OwaStoreObjectId UpdateConversationCategories(OwaStoreObjectId conversationId, OwaStoreObjectId folderId, string[] addCategories, string[] removeCategories)
		{
			Conversation conversation = ConversationUtilities.LoadConversation(base.UserContext, conversationId, new PropertyDefinition[]
			{
				ItemSchema.Id,
				StoreObjectSchema.ParentItemId,
				ItemSchema.Categories
			});
			ConversationUtilities.UpdateAlwaysCategorizeRule(base.UserContext, conversationId, conversation, addCategories, removeCategories);
			if (removeCategories != null && removeCategories.Length > 0)
			{
				foreach (IConversationTreeNode conversationTreeNode in conversation.ConversationTree)
				{
					foreach (IStorePropertyBag storePropertyBag in conversationTreeNode.StorePropertyBags)
					{
						string[] property = ItemUtility.GetProperty<string[]>(storePropertyBag, ItemSchema.Categories, null);
						if (property != null)
						{
							foreach (string value in property)
							{
								if (Array.IndexOf<string>(removeCategories, value) != -1)
								{
									VersionedId versionedId = (VersionedId)storePropertyBag.TryGetProperty(ItemSchema.Id);
									using (Item item = Utilities.GetItem<Item>(base.UserContext, versionedId.ObjectId, new PropertyDefinition[0]))
									{
										item.OpenAsReadWrite();
										CategoryContextMenu.ModifyCategories(item, null, removeCategories);
										item.Save(SaveMode.ResolveConflicts);
										break;
									}
								}
							}
						}
					}
				}
			}
			StoreObjectId latestMessage = ConversationUtilities.GetLatestMessage((MailboxSession)conversationId.GetSession(base.UserContext), conversation, folderId.StoreObjectId);
			if (latestMessage == null)
			{
				latestMessage = ConversationUtilities.GetLatestMessage((MailboxSession)conversationId.GetSession(base.UserContext), conversation, null);
			}
			return OwaStoreObjectId.CreateFromStoreObjectId(latestMessage, conversationId);
		}

		// Token: 0x04001DA3 RID: 7587
		public const string MethodDelete = "Delete";

		// Token: 0x04001DA4 RID: 7588
		public const string MethodPermanentDelete = "PermanentDelete";

		// Token: 0x04001DA5 RID: 7589
		public const string MethodMove = "Move";

		// Token: 0x04001DA6 RID: 7590
		public const string MethodCopy = "Copy";

		// Token: 0x04001DA7 RID: 7591
		public const string MethodAddToMru = "AddToMru";

		// Token: 0x04001DA8 RID: 7592
		public const string MethodGetCopyMoveMenu = "GetCopyMoveMenu";

		// Token: 0x04001DA9 RID: 7593
		public const string MethodPersistWidth = "PersistWidth";

		// Token: 0x04001DAA RID: 7594
		public const string MethodPersistHeight = "PersistHeight";

		// Token: 0x04001DAB RID: 7595
		public const string MethodPersistReadingPane = "PersistReadingPane";

		// Token: 0x04001DAC RID: 7596
		public const string MethodPersistReadingPaneSortOrder = "PersistReadingPaneSortOrder";

		// Token: 0x04001DAD RID: 7597
		public const string MethodPersistShowTreeInListView = "PersistShowTreeInListView";

		// Token: 0x04001DAE RID: 7598
		public const string MethodEditMeetingResponse = "ER";

		// Token: 0x04001DAF RID: 7599
		public const string MethodSendMeetingResponse = "SR";

		// Token: 0x04001DB0 RID: 7600
		public const string MethodDontSendMeetingResponse = "DR";

		// Token: 0x04001DB1 RID: 7601
		public const string MethodFlagItems = "flgItms";

		// Token: 0x04001DB2 RID: 7602
		public const string MethodCategorizeItems = "catItms";

		// Token: 0x04001DB3 RID: 7603
		public const string MethodClearCategories = "catClr";

		// Token: 0x04001DB4 RID: 7604
		public const string MethodGetAdvancedFind = "gtAF";

		// Token: 0x04001DB5 RID: 7605
		public const string MethodGetSearchInPublicFolder = "gtSIPF";

		// Token: 0x04001DB6 RID: 7606
		public const string MethodGetSearchScopeMenu = "GetSearchScopeMenu";

		// Token: 0x04001DB7 RID: 7607
		public const string MethodPersistELCCommentState = "PersistELCCommentState";

		// Token: 0x04001DB8 RID: 7608
		public const string MethodPersistSearchScope = "PSS";

		// Token: 0x04001DB9 RID: 7609
		public const string FolderId = "fId";

		// Token: 0x04001DBA RID: 7610
		public const string DestinationFolderId = "destId";

		// Token: 0x04001DBB RID: 7611
		public const string AddTargetFolderToMruParameter = "fAddToMRU";

		// Token: 0x04001DBC RID: 7612
		public const string SearchFilter = "srchf";

		// Token: 0x04001DBD RID: 7613
		public const string FolderFilter = "fldrfltr";

		// Token: 0x04001DBE RID: 7614
		public const string Items = "Itms";

		// Token: 0x04001DBF RID: 7615
		public const string Width = "w";

		// Token: 0x04001DC0 RID: 7616
		public const string Height = "h";

		// Token: 0x04001DC1 RID: 7617
		public const string Response = "Rsp";

		// Token: 0x04001DC2 RID: 7618
		public const string FlagActionParameter = "flga";

		// Token: 0x04001DC3 RID: 7619
		public const string DueDate = "ddt";

		// Token: 0x04001DC4 RID: 7620
		public const string AddCategories = "catAdd";

		// Token: 0x04001DC5 RID: 7621
		public const string RemoveCategories = "catRem";

		// Token: 0x04001DC6 RID: 7622
		public const string SearchScopeParameter = "scp";

		// Token: 0x04001DC7 RID: 7623
		public const string JunkStatus = "JS";

		// Token: 0x04001DC8 RID: 7624
		public const string ExpandELCComment = "fShwELC";

		// Token: 0x04001DC9 RID: 7625
		public const string RecurringTaskOccurrences = "RTOcc";

		// Token: 0x04001DCA RID: 7626
		protected const int MaxFolderNameLength = 256;

		// Token: 0x04001DCB RID: 7627
		private const int MaxSearchStringLength = 256;

		// Token: 0x04001DCC RID: 7628
		private const int MaxFromToFilterStringLength = 255;

		// Token: 0x04001DCD RID: 7629
		private static bool isRegistered;

		// Token: 0x04001DCE RID: 7630
		private Folder folder;

		// Token: 0x04001DCF RID: 7631
		private Folder searchFolder;

		// Token: 0x04001DD0 RID: 7632
		private Folder filteredFolder;

		// Token: 0x04001DD1 RID: 7633
		private QueryFilter folderQueryFilter;

		// Token: 0x04001DD2 RID: 7634
		private FolderVirtualListViewFilter filterObject;

		// Token: 0x02000488 RID: 1160
		// (Invoke) Token: 0x06002D01 RID: 11521
		internal delegate void RespondToMeetingRequestDelegate();
	}
}
