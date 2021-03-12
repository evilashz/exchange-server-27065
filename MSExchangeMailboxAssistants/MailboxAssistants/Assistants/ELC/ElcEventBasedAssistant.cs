using System;
using System.Collections.Generic;
using Microsoft.Exchange.Assistants;
using Microsoft.Exchange.Collections;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.MailboxAssistants.Assistants.ELC;
using Microsoft.Exchange.InfoWorker.Common;
using Microsoft.Mapi;

namespace Microsoft.Exchange.MailboxAssistants.Assistants.ELC
{
	// Token: 0x0200007E RID: 126
	internal class ElcEventBasedAssistant : EventBasedAssistant, IEventBasedAssistant, IAssistantBase
	{
		// Token: 0x060004A0 RID: 1184 RVA: 0x00021E6C File Offset: 0x0002006C
		private void ManageCache(Guid mbxGuid)
		{
			int num = Array.IndexOf<Guid>(this.mbxGuidFifo, mbxGuid);
			if (num >= 0 && num != this.mbxGuidFifoStart)
			{
				int num2 = (num == 0) ? (ElcEventBasedAssistant.mbxFifoCacheSize - 1) : (num - 1);
				this.mbxGuidFifo[num] = this.mbxGuidFifo[num2];
				this.mbxGuidFifo[num2] = mbxGuid;
			}
			if (num < 0)
			{
				this.mbxGuidFifoStart = ((this.mbxGuidFifoStart == 0) ? (ElcEventBasedAssistant.mbxFifoCacheSize - 1) : (this.mbxGuidFifoStart - 1));
				Guid mbxGuid2 = this.mbxGuidFifo[this.mbxGuidFifoStart];
				if (!Guid.Empty.Equals(this.mbxGuidFifoStart))
				{
					CachedState cachedState = AssistantsService.CachedObjectsList.GetCachedState(mbxGuid2);
					if (cachedState != null)
					{
						cachedState.LockForWrite();
						UserRetentionPolicyCache userRetentionPolicyCache = cachedState.State[5] as UserRetentionPolicyCache;
						if (userRetentionPolicyCache != null && !userRetentionPolicyCache.ReadOnlyCache)
						{
							cachedState.State[5] = null;
						}
						cachedState.ReleaseWriterLock();
					}
				}
				this.mbxGuidFifo[this.mbxGuidFifoStart] = mbxGuid;
			}
		}

		// Token: 0x060004A1 RID: 1185 RVA: 0x00021F8C File Offset: 0x0002018C
		public ElcEventBasedAssistant(DatabaseInfo databaseInfo, LocalizedString name, string nonLocalizedName) : base(databaseInfo, name, nonLocalizedName)
		{
		}

		// Token: 0x060004A2 RID: 1186 RVA: 0x00021FA8 File Offset: 0x000201A8
		public bool IsEventInteresting(MapiEvent mapiEvent)
		{
			ElcEventBasedAssistant.Tracer.TraceDebug<MapiEvent>((long)this.GetHashCode(), "MapiEvent is dispatched: {0}", mapiEvent);
			if (!this.IsEventRelevant(mapiEvent))
			{
				return false;
			}
			CachedState cachedState = AssistantsService.CachedObjectsList.GetCachedState(mapiEvent.MailboxGuid);
			UserRetentionPolicyCache userRetentionPolicyCache = RetentionPolicyCheck.QuickCheckForRetentionPolicy(mapiEvent, cachedState);
			if (userRetentionPolicyCache == null || userRetentionPolicyCache.UnderRetentionPolicy)
			{
				bool flag = RetentionPolicyCheck.IsEventConfigChange(mapiEvent);
				if (flag)
				{
					RetentionPolicyCheck.UpdateStateForPendingFaiEvent(mapiEvent.EventCounter, cachedState);
					ElcEventBasedAssistant.Tracer.TraceDebug<object, MapiEvent>((long)this.GetHashCode(), "{0}: this event is interesting because it is a change to our FAI: {1}", TraceContext.Get(), mapiEvent);
					return true;
				}
				if (RetentionPolicyCheck.IsAutoTagFai(mapiEvent))
				{
					ElcEventBasedAssistant.Tracer.TraceDebug<object, MapiEvent>((long)this.GetHashCode(), "{0}: this event is interesting because it is a change to an Autotag FAI: {1}", TraceContext.Get(), mapiEvent);
					return true;
				}
				MapiEventTypeFlags mapiEventTypeFlags = MapiEventTypeFlags.ObjectCreated | MapiEventTypeFlags.ObjectModified | MapiEventTypeFlags.ObjectMoved | MapiEventTypeFlags.ObjectCopied;
				if ((mapiEvent.EventMask & mapiEventTypeFlags) != (MapiEventTypeFlags)0)
				{
					if (mapiEvent.ClientType == MapiEventClientTypes.Transport && userRetentionPolicyCache != null && !this.InSentItems(userRetentionPolicyCache, mapiEvent))
					{
						ElcEventBasedAssistant.Tracer.TraceDebug<MapiEvent>((long)this.GetHashCode(), "Event is from transport and item not in SentItems. Ignoring from IsEventInteresting. Mapievent: {0}", mapiEvent);
						return false;
					}
					if (userRetentionPolicyCache != null && !this.IsEventOnRelevantFolder(userRetentionPolicyCache, mapiEvent))
					{
						return false;
					}
					if (userRetentionPolicyCache != null && TagAssistantHelper.IsConflictableItem(mapiEvent.ObjectClass, mapiEvent.ParentEntryId, userRetentionPolicyCache.DeletedItemsId))
					{
						return false;
					}
					MapiEventTypeFlags mapiEventTypeFlags2 = MapiEventTypeFlags.ObjectMoved | MapiEventTypeFlags.ObjectCopied;
					if (mapiEvent.ItemType == ObjectType.MAPI_FOLDER && (mapiEvent.EventMask & mapiEventTypeFlags2) != (MapiEventTypeFlags)0)
					{
						ElcEventBasedAssistant.Tracer.TraceDebug<object, MapiEvent>((long)this.GetHashCode(), "{0}: this event is interesting because is a moved folder: {1}", TraceContext.Get(), mapiEvent);
						return true;
					}
					if (mapiEvent.ItemType == ObjectType.MAPI_MESSAGE)
					{
						ElcEventBasedAssistant.Tracer.TraceDebug<object, MapiEvent>((long)this.GetHashCode(), "{0}: this event is interesting because is a message: {1}", TraceContext.Get(), mapiEvent);
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x060004A3 RID: 1187 RVA: 0x00022124 File Offset: 0x00020324
		protected override void HandleEventInternal(MapiEvent mapiEvent, MailboxSession itemStore, StoreObject item, List<KeyValuePair<string, object>> customDataToLog)
		{
			ElcEventBasedAssistant.Tracer.TraceDebug<MapiEvent>((long)this.GetHashCode(), "MapiEvent is handled: {0}", mapiEvent);
			this.ManageCache(mapiEvent.MailboxGuid);
			if (this.IsIgnorableDraft(mapiEvent, item))
			{
				return;
			}
			CachedState cachedState = AssistantsService.CachedObjectsList.GetCachedState(mapiEvent.MailboxGuid);
			cachedState.LockForRead();
			UserRetentionPolicyCache userRetentionPolicyCache = null;
			try
			{
				userRetentionPolicyCache = RetentionPolicyCheck.DetailedCheckForRetentionPolicy(mapiEvent, itemStore, item, cachedState);
			}
			finally
			{
				cachedState.ReleaseReaderLock();
			}
			if (RetentionPolicyCheck.IsEventConfigChange(mapiEvent))
			{
				ElcEventBasedAssistant.Tracer.TraceDebug<object, MapiEvent>((long)this.GetHashCode(), "{0}: updated configuration {1}", TraceContext.Get(), mapiEvent);
				return;
			}
			if (userRetentionPolicyCache != null && userRetentionPolicyCache.UnderRetentionPolicy)
			{
				bool flag = false;
				if (!this.IsEventOnRelevantFolder(userRetentionPolicyCache, mapiEvent))
				{
					return;
				}
				if (mapiEvent.ClientType == MapiEventClientTypes.Transport && !this.InSentItems(userRetentionPolicyCache, mapiEvent))
				{
					ElcEventBasedAssistant.Tracer.TraceDebug<ElcEventBasedAssistant, MapiEvent>((long)this.GetHashCode(), "{0} Event is from transport and item not in SentItems. Ignoring from HandleEvent. Mapievent: {1}", this, mapiEvent);
					return;
				}
				if (TagAssistantHelper.IsConflictableItem(mapiEvent.ObjectClass, mapiEvent.ParentEntryId, userRetentionPolicyCache.DeletedItemsId))
				{
					return;
				}
				if ((mapiEvent.EventMask & MapiEventTypeFlags.ObjectMoved) != (MapiEventTypeFlags)0 && mapiEvent.ItemType == ObjectType.MAPI_FOLDER)
				{
					userRetentionPolicyCache.ResetFolderCaches();
				}
				StoreObjectId storeObjectId = StoreObjectId.FromProviderSpecificId(mapiEvent.ItemEntryId);
				if (mapiEvent.ItemType == ObjectType.MAPI_FOLDER && item == null && mapiEvent.ItemEntryId != null && (mapiEvent.EventMask & MapiEventTypeFlags.ObjectDeleted) == (MapiEventTypeFlags)0)
				{
					Exception ex = null;
					try
					{
						ElcEventBasedAssistant.Tracer.TraceDebug<object, MapiEvent>((long)this.GetHashCode(), "{0}: A folder was changed and needs to be manually loaded {1}", TraceContext.Get(), mapiEvent);
						item = Folder.Bind(itemStore, storeObjectId, ElcEventBasedAssistantType.InternalPreloadItemProperties);
						flag = true;
					}
					catch (ObjectNotFoundException ex2)
					{
						ex = ex2;
					}
					catch (ConversionFailedException ex3)
					{
						ex = ex3;
					}
					catch (VirusMessageDeletedException ex4)
					{
						ex = ex4;
					}
					if (ex != null)
					{
						ElcEventBasedAssistant.Tracer.TraceDebug<ElcEventBasedAssistant, Exception>((long)this.GetHashCode(), "{0}: Problems loading a folder. It will not be processed. Exception: {1}", this, ex);
						return;
					}
				}
				try
				{
					StoreObjectId parentId = StoreObjectId.FromProviderSpecificId(mapiEvent.ParentEntryId);
					ElcEventProcessor elcEventProcessor = ElcEventProcessor.GetElcEventProcessor();
					elcEventProcessor.ValidateStoreObject(userRetentionPolicyCache, itemStore, parentId, storeObjectId, item, mapiEvent);
				}
				finally
				{
					if (flag)
					{
						item.Dispose();
					}
				}
			}
		}

		// Token: 0x060004A4 RID: 1188 RVA: 0x00022330 File Offset: 0x00020530
		private bool IsEventRelevant(MapiEvent mapiEvent)
		{
			if ((mapiEvent.ExtendedEventFlags & MapiExtendedEventFlags.NonIPMFolder) == MapiExtendedEventFlags.NonIPMFolder)
			{
				ElcEventBasedAssistant.Tracer.TraceDebug<MapiEvent>((long)this.GetHashCode(), "Non IPM Folder events are not relevant. MapiEvent: {0}", mapiEvent);
				return false;
			}
			if ((mapiEvent.EventFlags & MapiEventFlags.SearchFolder) == MapiEventFlags.SearchFolder)
			{
				ElcEventBasedAssistant.Tracer.TraceDebug<MapiEvent>((long)this.GetHashCode(), "SearchFolder events are not relevant. MapiEvent: {0}", mapiEvent);
				return false;
			}
			if ((mapiEvent.EventFlags & MapiEventFlags.ContentOnly) == MapiEventFlags.ContentOnly)
			{
				ElcEventBasedAssistant.Tracer.TraceDebug<MapiEvent>((long)this.GetHashCode(), "ContentOnly events are not relevant. MapiEvent: {0}", mapiEvent);
				return false;
			}
			if (ObjectClass.IsCalendarItem(mapiEvent.ObjectClass) || ObjectClass.IsTask(mapiEvent.ObjectClass))
			{
				ElcEventBasedAssistant.Tracer.TraceDebug<MapiEvent>((long)this.GetHashCode(), "Calendar and task items are not relevent. Mapievent: {0}", mapiEvent);
				return false;
			}
			if (mapiEvent.ClientType == MapiEventClientTypes.Transport)
			{
				ElcEventBasedAssistant.Tracer.TraceDebug<MapiEvent>((long)this.GetHashCode(), "Skip events from Transport. Mapievent: {0}", mapiEvent);
				return false;
			}
			MapiEventTypeFlags mapiEventTypeFlags = MapiEventTypeFlags.ObjectCreated | MapiEventTypeFlags.ObjectModified | MapiEventTypeFlags.ObjectMoved | MapiEventTypeFlags.ObjectCopied;
			if ((mapiEvent.EventMask & mapiEventTypeFlags) == (MapiEventTypeFlags)0)
			{
				ElcEventBasedAssistant.Tracer.TraceDebug<MapiEvent>((long)this.GetHashCode(), "No interesting MapiEventTypeFlags. Skip. Mapievent: {0}", mapiEvent);
				return false;
			}
			if ((mapiEvent.EventMask & MapiEventTypeFlags.ObjectModified) != (MapiEventTypeFlags)0 && (mapiEvent.EventFlags & MapiEventFlags.FolderAssociated) == MapiEventFlags.None && mapiEvent.ItemType == ObjectType.MAPI_MESSAGE && (mapiEvent.ExtendedEventFlags & MapiExtendedEventFlags.RetentionTagModified) == MapiExtendedEventFlags.None && (mapiEvent.ExtendedEventFlags & MapiExtendedEventFlags.RetentionPropertiesModified) == MapiExtendedEventFlags.None)
			{
				ElcEventBasedAssistant.Tracer.TraceDebug<MapiEvent>((long)this.GetHashCode(), "Item was modified. Either the property modified is not relevent to us . Mapievent: {0}", mapiEvent);
				return false;
			}
			if (mapiEvent.ClientType == MapiEventClientTypes.TimeBasedAssistants && !RetentionPolicyCheck.IsEventConfigChange(mapiEvent) && !RetentionPolicyCheck.IsAutoTagFai(mapiEvent))
			{
				ElcEventBasedAssistant.Tracer.TraceDebug<MapiEvent>((long)this.GetHashCode(), "Event is from Time Based Assistant and not FAI related. Ignoring. Mapievent: {0}", mapiEvent);
				return false;
			}
			if (mapiEvent.ClientType == MapiEventClientTypes.EventBasedAssistants)
			{
				ElcEventBasedAssistant.Tracer.TraceDebug<MapiEvent>((long)this.GetHashCode(), "Event is from this assistant. Ignoring. Mapievent: {0}", mapiEvent);
				return false;
			}
			if ((mapiEvent.EventFlags & MapiEventFlags.FolderAssociated) != MapiEventFlags.None && !RetentionPolicyCheck.IsEventConfigChange(mapiEvent) && !RetentionPolicyCheck.IsAutoTagFai(mapiEvent))
			{
				ElcEventBasedAssistant.Tracer.TraceDebug<MapiEvent>((long)this.GetHashCode(), "FAI we don't care about. Ignoring. Mapievent: {0}", mapiEvent);
				return false;
			}
			if (mapiEvent.ItemType != ObjectType.MAPI_MESSAGE && mapiEvent.ItemType != ObjectType.MAPI_FOLDER)
			{
				ElcEventBasedAssistant.Tracer.TraceDebug<MapiEvent>((long)this.GetHashCode(), "Not a message or a folder. Ignoring. Mapievent: {0}", mapiEvent);
				return false;
			}
			if ((mapiEvent.EventMask & MapiEventTypeFlags.ObjectDeleted) != (MapiEventTypeFlags)0)
			{
				ElcEventBasedAssistant.Tracer.TraceDebug<MapiEvent>((long)this.GetHashCode(), "Item Deleted. Ignoring. Mapievent: {0}", mapiEvent);
				return false;
			}
			ElcEventBasedAssistant.Tracer.TraceDebug<MapiEvent>((long)this.GetHashCode(), "IsEventRelevant is true. Mapievent: {0}", mapiEvent);
			return true;
		}

		// Token: 0x060004A5 RID: 1189 RVA: 0x00022577 File Offset: 0x00020777
		private bool IsEventOnRelevantFolder(UserRetentionPolicyCache mailboxState, MapiEvent mapiEvent)
		{
			if (mapiEvent.ItemType == ObjectType.MAPI_MESSAGE && mailboxState.IsFolderIdToSkip(mapiEvent.ParentEntryId))
			{
				ElcEventBasedAssistant.Tracer.TraceDebug<string, MapiEvent>((long)this.GetHashCode(), "Event is in non-IPM folder (entryid {0}). Ignoring. Mapievent: {1}", mapiEvent.ParentEntryIdString, mapiEvent);
				return false;
			}
			return true;
		}

		// Token: 0x060004A6 RID: 1190 RVA: 0x000225B0 File Offset: 0x000207B0
		private bool InSentItems(UserRetentionPolicyCache mailboxState, MapiEvent mapiEvent)
		{
			if (mapiEvent.ItemType == ObjectType.MAPI_MESSAGE && ArrayComparer<byte>.Comparer.Equals(mapiEvent.ParentEntryId, mailboxState.SentItemsId))
			{
				ElcEventBasedAssistant.Tracer.TraceDebug<ElcEventBasedAssistant, MapiEvent>((long)this.GetHashCode(), "{0} Event is on item in SentItems folder. We must handle it. Mapievent: {1}", this, mapiEvent);
				return true;
			}
			ElcEventBasedAssistant.Tracer.TraceDebug<ElcEventBasedAssistant, MapiEvent>((long)this.GetHashCode(), "{0} Item not in SentItems folder. Mapievent: {1}", this, mapiEvent);
			return false;
		}

		// Token: 0x060004A7 RID: 1191 RVA: 0x00022614 File Offset: 0x00020814
		private bool IsIgnorableDraft(MapiEvent mapiEvent, StoreObject item)
		{
			if (item != null && item.GetValueOrDefault<bool>(MessageItemSchema.IsDraft) && (mapiEvent.EventFlags & MapiEventFlags.FolderAssociated) == MapiEventFlags.None)
			{
				ElcEventBasedAssistant.Tracer.TraceDebug((long)this.GetHashCode(), "{0}: Item is a draft. Don't handle it. Item subject: {1}. Item class: {2}. Mapi event: {3}.", new object[]
				{
					TraceContext.Get(),
					(item is MessageItem) ? ((MessageItem)item).Subject : string.Empty,
					item.ClassName,
					mapiEvent
				});
				return true;
			}
			return false;
		}

		// Token: 0x060004A8 RID: 1192 RVA: 0x000226AB File Offset: 0x000208AB
		void IAssistantBase.OnShutdown()
		{
			base.OnShutdown();
		}

		// Token: 0x060004A9 RID: 1193 RVA: 0x000226B3 File Offset: 0x000208B3
		LocalizedString IAssistantBase.get_Name()
		{
			return base.Name;
		}

		// Token: 0x060004AA RID: 1194 RVA: 0x000226BB File Offset: 0x000208BB
		string IAssistantBase.get_NonLocalizedName()
		{
			return base.NonLocalizedName;
		}

		// Token: 0x0400039A RID: 922
		private static readonly Trace Tracer = ExTraceGlobals.EventBasedAssistantTracer;

		// Token: 0x0400039B RID: 923
		private static readonly Trace TracerPfd = ExTraceGlobals.PFDTracer;

		// Token: 0x0400039C RID: 924
		private static int mbxFifoCacheSize = 100;

		// Token: 0x0400039D RID: 925
		private Guid[] mbxGuidFifo = new Guid[ElcEventBasedAssistant.mbxFifoCacheSize];

		// Token: 0x0400039E RID: 926
		private int mbxGuidFifoStart;
	}
}
