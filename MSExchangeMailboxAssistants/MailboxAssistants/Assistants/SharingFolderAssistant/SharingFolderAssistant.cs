using System;
using System.Collections.Generic;
using Microsoft.Exchange.Assistants;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.MailboxAssistants.Assistants.SharingFolder;
using Microsoft.Exchange.InfoWorker.Common;
using Microsoft.Exchange.MailboxAssistants.CalendarSync;
using Microsoft.Mapi;

namespace Microsoft.Exchange.MailboxAssistants.Assistants.SharingFolderAssistant
{
	// Token: 0x020000CD RID: 205
	internal sealed class SharingFolderAssistant : EventBasedAssistant, IEventBasedAssistant, IAssistantBase
	{
		// Token: 0x060008BB RID: 2235 RVA: 0x0003BE30 File Offset: 0x0003A030
		public SharingFolderAssistant(DatabaseInfo databaseInfo, LocalizedString name, string nonLocalizedName) : base(databaseInfo, name, nonLocalizedName)
		{
			this.ProcessEventMethods = new Dictionary<MapiEventTypeFlags, SharingFolderAssistant.ProcessEvent>(3);
			this.ProcessEventMethods[MapiEventTypeFlags.ObjectCreated] = new SharingFolderAssistant.ProcessEvent(this.ProcessObjectCreated);
			this.ProcessEventMethods[MapiEventTypeFlags.ObjectDeleted] = new SharingFolderAssistant.ProcessEvent(this.ProcessObjectDeleted);
			this.ProcessEventMethods[MapiEventTypeFlags.ObjectMoved] = new SharingFolderAssistant.ProcessEvent(this.ProcessObjectMoved);
		}

		// Token: 0x060008BC RID: 2236 RVA: 0x0003BE9C File Offset: 0x0003A09C
		public bool IsEventInteresting(MapiEvent mapiEvent)
		{
			if (!SharingFolderAssistantType.PossibleInterestingEvents.Contains(mapiEvent.EventMask))
			{
				return false;
			}
			if (mapiEvent.EventMask.Contains(MapiEventTypeFlags.ObjectCreated))
			{
				return ObjectClass.IsSharingFolderBindingMessage(mapiEvent.ObjectClass);
			}
			if (!ObjectClass.IsCalendarFolder(mapiEvent.ObjectClass) && !ObjectClass.IsContactsFolder(mapiEvent.ObjectClass))
			{
				return false;
			}
			SharingFolderAssistant.IsEventInterestingTracer.TraceDebug<object, MapiEvent>((long)this.GetHashCode(), "{0}: SharingFolderAssistant.IsEventInteresting is interested on event {1}.", TraceContext.Get(), mapiEvent);
			return true;
		}

		// Token: 0x060008BD RID: 2237 RVA: 0x0003BF10 File Offset: 0x0003A110
		protected override void HandleEventInternal(MapiEvent mapiEvent, MailboxSession mailboxSession, StoreObject item, List<KeyValuePair<string, object>> customDataToLog)
		{
			SharingFolderAssistant.GeneralTracer.TraceDebug<object, MapiEvent>((long)this.GetHashCode(), "{0}: SharingFolderAssistant.HandleEventInternal called for event {1}.", TraceContext.Get(), mapiEvent);
			IList<SynchronizableFolderType> folderTypesToProcess = this.GetFolderTypesToProcess(mapiEvent, mailboxSession, item);
			if (folderTypesToProcess.Count > 0)
			{
				this.ProcessEventMethods[mapiEvent.EventMask](mapiEvent, mailboxSession, StoreObjectId.FromProviderSpecificId(mapiEvent.ParentEntryId), folderTypesToProcess);
			}
		}

		// Token: 0x060008BE RID: 2238 RVA: 0x0003BF70 File Offset: 0x0003A170
		private IList<SynchronizableFolderType> GetFolderTypesToProcess(MapiEvent mapiEvent, MailboxSession mailboxSession, StoreObject item)
		{
			if (mapiEvent.EventMask.Contains(MapiEventTypeFlags.ObjectDeleted))
			{
				return SynchronizableFolderType.All;
			}
			if (mapiEvent.EventMask.Contains(MapiEventTypeFlags.ObjectCreated))
			{
				if (item != null)
				{
					SynchronizableFolderType synchronizableFolderType = SynchronizableFolderType.FromBinding(item);
					if (synchronizableFolderType != null)
					{
						return synchronizableFolderType.ToList();
					}
				}
				return new List<SynchronizableFolderType>(0);
			}
			StoreObjectId storeObjectId = StoreObjectId.FromProviderSpecificId(mapiEvent.ItemEntryId, StoreObjectType.Folder);
			try
			{
				using (Folder folder = Folder.Bind(mailboxSession, storeObjectId))
				{
					SynchronizableFolderType synchronizableFolderType2 = SynchronizableFolderType.FromFolder(folder);
					IList<SynchronizableFolderType> list = (synchronizableFolderType2 != null) ? synchronizableFolderType2.ToList() : null;
					if (list != null)
					{
						if (list.Count == 1)
						{
							StoreObjectId folderId = StoreObjectId.FromProviderSpecificId(mapiEvent.ItemEntryId, list[0].StoreObjectType);
							if (list[0].HasSubscription(mailboxSession, folderId))
							{
								return list;
							}
							SharingFolderAssistant.GeneralTracer.TraceDebug((long)this.GetHashCode(), "{0}: SharingFolderAssistant.GetFolderTypesToProcess: folder {1}, folder type {2}, from user {3} doesn't have a subscription.", new object[]
							{
								TraceContext.Get(),
								folder.DisplayName,
								list[0],
								mailboxSession.DisplayName
							});
						}
						else
						{
							SharingFolderAssistant.GeneralTracer.TraceDebug((long)this.GetHashCode(), "{0}: SharingFolderAssistant.GetFolderTypesToProcess: folder {1}, from user {2} has {3} folder types.", new object[]
							{
								TraceContext.Get(),
								folder.DisplayName,
								mailboxSession.DisplayName,
								list.Count
							});
						}
					}
					else
					{
						SharingFolderAssistant.GeneralTracer.TraceDebug<object, string, string>((long)this.GetHashCode(), "{0}: SharingFolderAssistant.GetFolderTypesToProcess: folder {1} from user {2} is not a shared folder.", TraceContext.Get(), folder.DisplayName, mailboxSession.DisplayName);
					}
				}
			}
			catch (ObjectNotFoundException)
			{
				SharingFolderAssistant.GeneralTracer.TraceDebug<object, StoreObjectId, string>((long)this.GetHashCode(), "{0}: SharingFolderAssistant.GetFolderTypesToProcess: folder {1} from user {2} couldn't be found. Skipping.", TraceContext.Get(), storeObjectId, mailboxSession.DisplayName);
			}
			return new List<SynchronizableFolderType>(0);
		}

		// Token: 0x060008BF RID: 2239 RVA: 0x0003C160 File Offset: 0x0003A360
		private void ProcessObjectCreated(MapiEvent mapiEvent, MailboxSession mailboxSession, StoreObjectId parentId, IList<SynchronizableFolderType> folderTypes)
		{
			SharingFolderAssistant.GeneralTracer.TraceDebug<object, MapiEvent, string>((long)this.GetHashCode(), "{0}: SharingFolderAssistant.ProcessObjectCreated for event {1} will increment the counter for mailbox {2}.", TraceContext.Get(), mapiEvent, mailboxSession.DisplayName);
			this.CalendarSyncAssistantHelper.IncrementSubscriptionCount(mailboxSession, folderTypes[0]);
		}

		// Token: 0x060008C0 RID: 2240 RVA: 0x0003C198 File Offset: 0x0003A398
		private void ProcessObjectDeleted(MapiEvent mapiEvent, MailboxSession mailboxSession, StoreObjectId parentId, IList<SynchronizableFolderType> folderTypes)
		{
			SharingFolderAssistant.GeneralTracer.TraceDebug<object, MapiEvent, string>((long)this.GetHashCode(), "{0}: SharingFolderAssistant.ProcessObjectDeleted for event {1} will recalculate the counter for mailbox {2}.", TraceContext.Get(), mapiEvent, mailboxSession.DisplayName);
			Dictionary<SynchronizableFolderType, List<FolderRow>> synchronizableFolderRows = this.CalendarSyncAssistantHelper.GetSynchronizableFolderRows(mailboxSession, folderTypes);
			foreach (KeyValuePair<SynchronizableFolderType, List<FolderRow>> keyValuePair in synchronizableFolderRows)
			{
				this.CalendarSyncAssistantHelper.SetSubscriptionCount(mailboxSession, keyValuePair.Key, keyValuePair.Value.Count, true);
			}
		}

		// Token: 0x060008C1 RID: 2241 RVA: 0x0003C230 File Offset: 0x0003A430
		private void ProcessObjectMoved(MapiEvent mapiEvent, MailboxSession mailboxSession, StoreObjectId parentId, IList<SynchronizableFolderType> folderTypes)
		{
			if (this.CalendarSyncAssistantHelper.IsDeletedItemsFolder(mailboxSession, parentId))
			{
				SharingFolderAssistant.GeneralTracer.TraceDebug<object, MapiEvent, string>((long)this.GetHashCode(), "{0}: SharingFolderAssistant.ProcessObjectMoved for event {1} deleted a move to deleted items for mailbox {2}.", TraceContext.Get(), mapiEvent, mailboxSession.DisplayName);
				this.CalendarSyncAssistantHelper.DecrementSubscriptionCount(mailboxSession, folderTypes[0]);
				return;
			}
			StoreObjectId parentId2 = StoreObjectId.FromProviderSpecificId(mapiEvent.OldParentEntryId, StoreObjectType.CalendarFolder);
			if (this.CalendarSyncAssistantHelper.IsDeletedItemsFolder(mailboxSession, parentId2))
			{
				SharingFolderAssistant.GeneralTracer.TraceDebug<object, MapiEvent, string>((long)this.GetHashCode(), "{0}: SharingFolderAssistant.ProcessObjectMoved for event {1} detected a move out of deleted items for mailbox {2}.", TraceContext.Get(), mapiEvent, mailboxSession.DisplayName);
				this.CalendarSyncAssistantHelper.IncrementSubscriptionCount(mailboxSession, folderTypes[0]);
			}
		}

		// Token: 0x17000228 RID: 552
		// (get) Token: 0x060008C2 RID: 2242 RVA: 0x0003C2D5 File Offset: 0x0003A4D5
		private CalendarSyncAssistantHelper CalendarSyncAssistantHelper
		{
			get
			{
				return SharingFolderAssistant.calendarSyncAssistantHelper;
			}
		}

		// Token: 0x17000229 RID: 553
		// (get) Token: 0x060008C3 RID: 2243 RVA: 0x0003C2DC File Offset: 0x0003A4DC
		// (set) Token: 0x060008C4 RID: 2244 RVA: 0x0003C2E4 File Offset: 0x0003A4E4
		private Dictionary<MapiEventTypeFlags, SharingFolderAssistant.ProcessEvent> ProcessEventMethods { get; set; }

		// Token: 0x060008C5 RID: 2245 RVA: 0x0003C30D File Offset: 0x0003A50D
		void IAssistantBase.OnShutdown()
		{
			base.OnShutdown();
		}

		// Token: 0x060008C6 RID: 2246 RVA: 0x0003C315 File Offset: 0x0003A515
		LocalizedString IAssistantBase.get_Name()
		{
			return base.Name;
		}

		// Token: 0x060008C7 RID: 2247 RVA: 0x0003C31D File Offset: 0x0003A51D
		string IAssistantBase.get_NonLocalizedName()
		{
			return base.NonLocalizedName;
		}

		// Token: 0x04000610 RID: 1552
		private static readonly Trace IsEventInterestingTracer = ExTraceGlobals.IsEventInterestingTracer;

		// Token: 0x04000611 RID: 1553
		private static readonly Trace GeneralTracer = ExTraceGlobals.GeneralTracer;

		// Token: 0x04000612 RID: 1554
		private static CalendarSyncAssistantHelper calendarSyncAssistantHelper = new CalendarSyncAssistantHelper();

		// Token: 0x020000CE RID: 206
		// (Invoke) Token: 0x060008CA RID: 2250
		private delegate void ProcessEvent(MapiEvent mapiEvent, MailboxSession mailboxSession, StoreObjectId parentId, IList<SynchronizableFolderType> folderTypes);
	}
}
