using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Mapi;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x020006FE RID: 1790
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class Event
	{
		// Token: 0x060046E7 RID: 18151 RVA: 0x0012D9E8 File Offset: 0x0012BBE8
		internal Event(Guid mdbGuid, MapiEvent mapiEvent)
		{
			this.mdbGuid = mdbGuid;
			this.eventType = EventType.None;
			if ((mapiEvent.EventMask & MapiEventTypeFlags.NewMail) == MapiEventTypeFlags.NewMail)
			{
				this.eventType |= EventType.NewMail;
			}
			if ((mapiEvent.EventMask & MapiEventTypeFlags.ObjectCreated) == MapiEventTypeFlags.ObjectCreated)
			{
				this.eventType |= EventType.ObjectCreated;
			}
			if ((mapiEvent.EventMask & MapiEventTypeFlags.ObjectDeleted) == MapiEventTypeFlags.ObjectDeleted)
			{
				this.eventType |= EventType.ObjectDeleted;
			}
			if ((mapiEvent.EventMask & MapiEventTypeFlags.ObjectModified) == MapiEventTypeFlags.ObjectModified)
			{
				this.eventType |= EventType.ObjectModified;
			}
			if ((mapiEvent.EventMask & MapiEventTypeFlags.ObjectMoved) == MapiEventTypeFlags.ObjectMoved)
			{
				this.eventType |= EventType.ObjectMoved;
			}
			if ((mapiEvent.EventMask & MapiEventTypeFlags.ObjectCopied) == MapiEventTypeFlags.ObjectCopied)
			{
				this.eventType |= EventType.ObjectCopied;
			}
			if ((mapiEvent.EventMask & MapiEventTypeFlags.CriticalError) == MapiEventTypeFlags.CriticalError)
			{
				this.eventType |= EventType.CriticalError;
			}
			if ((mapiEvent.EventMask & MapiEventTypeFlags.MailboxDeleted) == MapiEventTypeFlags.MailboxDeleted)
			{
				this.eventType |= EventType.MailboxDeleted;
			}
			if ((mapiEvent.EventMask & MapiEventTypeFlags.MailboxDisconnected) == MapiEventTypeFlags.MailboxDisconnected)
			{
				this.eventType |= EventType.MailboxDisconnected;
			}
			if ((mapiEvent.EventMask & MapiEventTypeFlags.MailboxMoveFailed) == MapiEventTypeFlags.MailboxMoveFailed)
			{
				this.eventType |= EventType.MailboxMoveFailed;
			}
			if ((mapiEvent.EventMask & MapiEventTypeFlags.MailboxMoveStarted) == MapiEventTypeFlags.MailboxMoveStarted)
			{
				this.eventType |= EventType.MailboxMoveStarted;
			}
			if ((mapiEvent.EventMask & MapiEventTypeFlags.MailboxMoveSucceeded) == MapiEventTypeFlags.MailboxMoveSucceeded)
			{
				this.eventType |= EventType.MailboxMoveSucceeded;
			}
			if (mapiEvent.ObjectClass == "IPM.Appointment")
			{
				this.eventType |= Event.GetFreeBusyEventType(mapiEvent);
			}
			this.objectType = EventObjectType.None;
			if (mapiEvent.ItemType == Microsoft.Mapi.ObjectType.MAPI_FOLDER)
			{
				this.objectType |= EventObjectType.Folder;
			}
			if (mapiEvent.ItemType == Microsoft.Mapi.ObjectType.MAPI_MESSAGE)
			{
				this.objectType |= EventObjectType.Item;
			}
			this.objectId = Event.GetStoreObjectId(mapiEvent, out this.className);
			if (mapiEvent.ParentEntryId != null)
			{
				this.parentObjectId = StoreObjectId.FromProviderSpecificId(mapiEvent.ParentEntryId, StoreObjectType.Unknown);
			}
			if (mapiEvent.OldItemEntryId != null)
			{
				this.oldObjectId = StoreObjectId.FromProviderSpecificId(mapiEvent.OldItemEntryId, StoreObjectType.Unknown);
			}
			if (mapiEvent.OldParentEntryId != null)
			{
				this.oldParentObjectId = StoreObjectId.FromProviderSpecificId(mapiEvent.OldParentEntryId, StoreObjectType.Unknown);
			}
			this.mailboxGuid = mapiEvent.MailboxGuid;
			this.timeStamp = (ExDateTime)mapiEvent.CreateTime;
			this.itemCount = mapiEvent.ItemCount;
			this.unreadItemCount = mapiEvent.UnreadItemCount;
			this.watermark = mapiEvent.Watermark.EventCounter;
			this.eventFlags = EventFlags.None;
			if ((mapiEvent.ExtendedEventFlags & MapiExtendedEventFlags.NoReminderPropertyModified) != MapiExtendedEventFlags.NoReminderPropertyModified)
			{
				this.eventFlags |= EventFlags.ReminderPropertiesModified;
			}
			if ((mapiEvent.ExtendedEventFlags & MapiExtendedEventFlags.TimerEventFired) == MapiExtendedEventFlags.TimerEventFired)
			{
				this.eventFlags |= EventFlags.TimerEventFired;
			}
			if ((mapiEvent.ExtendedEventFlags & MapiExtendedEventFlags.NonIPMFolder) == MapiExtendedEventFlags.NonIPMFolder)
			{
				this.eventFlags |= EventFlags.NonIPMChange;
			}
		}

		// Token: 0x1700149E RID: 5278
		// (get) Token: 0x060046E8 RID: 18152 RVA: 0x0012DCEB File Offset: 0x0012BEEB
		public EventType EventType
		{
			get
			{
				return this.eventType;
			}
		}

		// Token: 0x1700149F RID: 5279
		// (get) Token: 0x060046E9 RID: 18153 RVA: 0x0012DCF3 File Offset: 0x0012BEF3
		public EventObjectType ObjectType
		{
			get
			{
				return this.objectType;
			}
		}

		// Token: 0x170014A0 RID: 5280
		// (get) Token: 0x060046EA RID: 18154 RVA: 0x0012DCFB File Offset: 0x0012BEFB
		public StoreObjectId ObjectId
		{
			get
			{
				return this.objectId;
			}
		}

		// Token: 0x170014A1 RID: 5281
		// (get) Token: 0x060046EB RID: 18155 RVA: 0x0012DD03 File Offset: 0x0012BF03
		public StoreObjectId ParentObjectId
		{
			get
			{
				return this.parentObjectId;
			}
		}

		// Token: 0x170014A2 RID: 5282
		// (get) Token: 0x060046EC RID: 18156 RVA: 0x0012DD0B File Offset: 0x0012BF0B
		public StoreObjectId OldObjectId
		{
			get
			{
				return this.oldObjectId;
			}
		}

		// Token: 0x170014A3 RID: 5283
		// (get) Token: 0x060046ED RID: 18157 RVA: 0x0012DD13 File Offset: 0x0012BF13
		public StoreObjectId OldParentObjectId
		{
			get
			{
				return this.oldParentObjectId;
			}
		}

		// Token: 0x170014A4 RID: 5284
		// (get) Token: 0x060046EE RID: 18158 RVA: 0x0012DD1B File Offset: 0x0012BF1B
		public Guid MailboxGuid
		{
			get
			{
				return this.mailboxGuid;
			}
		}

		// Token: 0x170014A5 RID: 5285
		// (get) Token: 0x060046EF RID: 18159 RVA: 0x0012DD23 File Offset: 0x0012BF23
		public string ClassName
		{
			get
			{
				return this.className;
			}
		}

		// Token: 0x170014A6 RID: 5286
		// (get) Token: 0x060046F0 RID: 18160 RVA: 0x0012DD2B File Offset: 0x0012BF2B
		public ExDateTime TimeStamp
		{
			get
			{
				return this.timeStamp;
			}
		}

		// Token: 0x170014A7 RID: 5287
		// (get) Token: 0x060046F1 RID: 18161 RVA: 0x0012DD33 File Offset: 0x0012BF33
		public long UnreadItemCount
		{
			get
			{
				return this.unreadItemCount;
			}
		}

		// Token: 0x170014A8 RID: 5288
		// (get) Token: 0x060046F2 RID: 18162 RVA: 0x0012DD3B File Offset: 0x0012BF3B
		public long ItemCount
		{
			get
			{
				return this.itemCount;
			}
		}

		// Token: 0x170014A9 RID: 5289
		// (get) Token: 0x060046F3 RID: 18163 RVA: 0x0012DD43 File Offset: 0x0012BF43
		public EventWatermark EventWatermark
		{
			get
			{
				return new EventWatermark(this.mdbGuid, this.watermark, true);
			}
		}

		// Token: 0x170014AA RID: 5290
		// (get) Token: 0x060046F4 RID: 18164 RVA: 0x0012DD57 File Offset: 0x0012BF57
		public EventFlags EventFlags
		{
			get
			{
				return this.eventFlags;
			}
		}

		// Token: 0x060046F5 RID: 18165 RVA: 0x0012DD60 File Offset: 0x0012BF60
		internal static StoreObjectId GetStoreObjectId(MapiEvent mapiEvent, out string className)
		{
			className = string.Empty;
			switch (mapiEvent.ItemType)
			{
			case Microsoft.Mapi.ObjectType.MAPI_FOLDER:
			case Microsoft.Mapi.ObjectType.MAPI_MESSAGE:
				className = mapiEvent.ObjectClass;
				break;
			}
			StoreObjectType storeObjectType = ObjectClass.GetObjectType(className);
			StoreObjectId result = null;
			if (mapiEvent.ItemEntryId != null)
			{
				result = StoreObjectId.FromProviderSpecificId(mapiEvent.ItemEntryId, storeObjectType);
			}
			return result;
		}

		// Token: 0x170014AB RID: 5291
		// (get) Token: 0x060046F6 RID: 18166 RVA: 0x0012DDB9 File Offset: 0x0012BFB9
		internal long MapiWatermark
		{
			get
			{
				return this.watermark;
			}
		}

		// Token: 0x060046F7 RID: 18167 RVA: 0x0012DDC4 File Offset: 0x0012BFC4
		public override string ToString()
		{
			return string.Format("Event. TimeStamp = {0}. Event type = {1}, Object Type = {2}, .ObjectId = {3}.", new object[]
			{
				this.TimeStamp,
				this.EventType,
				this.ObjectType,
				this.ObjectId
			});
		}

		// Token: 0x060046F8 RID: 18168 RVA: 0x0012DE18 File Offset: 0x0012C018
		private static EventType GetFreeBusyEventType(MapiEvent mapiEvent)
		{
			if ((mapiEvent.EventMask & MapiEventTypeFlags.ObjectDeleted) == MapiEventTypeFlags.ObjectDeleted || (mapiEvent.EventMask & MapiEventTypeFlags.ObjectCreated) == MapiEventTypeFlags.ObjectCreated || (mapiEvent.EventMask & MapiEventTypeFlags.ObjectMoved) == MapiEventTypeFlags.ObjectMoved)
			{
				return EventType.FreeBusyChanged;
			}
			if ((mapiEvent.EventMask & MapiEventTypeFlags.ObjectModified) == MapiEventTypeFlags.ObjectModified && ((mapiEvent.ExtendedEventFlags & MapiExtendedEventFlags.AppointmentFreeBusyNotModified) != MapiExtendedEventFlags.AppointmentFreeBusyNotModified || (mapiEvent.ExtendedEventFlags & MapiExtendedEventFlags.AppointmentTimeNotModified) != MapiExtendedEventFlags.AppointmentTimeNotModified))
			{
				return EventType.FreeBusyChanged;
			}
			return EventType.None;
		}

		// Token: 0x040026BB RID: 9915
		private readonly EventType eventType;

		// Token: 0x040026BC RID: 9916
		private readonly EventObjectType objectType;

		// Token: 0x040026BD RID: 9917
		private readonly StoreObjectId objectId;

		// Token: 0x040026BE RID: 9918
		private readonly StoreObjectId parentObjectId;

		// Token: 0x040026BF RID: 9919
		private readonly StoreObjectId oldObjectId;

		// Token: 0x040026C0 RID: 9920
		private readonly StoreObjectId oldParentObjectId;

		// Token: 0x040026C1 RID: 9921
		private readonly Guid mailboxGuid;

		// Token: 0x040026C2 RID: 9922
		private readonly string className;

		// Token: 0x040026C3 RID: 9923
		private readonly ExDateTime timeStamp;

		// Token: 0x040026C4 RID: 9924
		private readonly long unreadItemCount;

		// Token: 0x040026C5 RID: 9925
		private readonly long itemCount;

		// Token: 0x040026C6 RID: 9926
		private readonly long watermark;

		// Token: 0x040026C7 RID: 9927
		private readonly EventFlags eventFlags;

		// Token: 0x040026C8 RID: 9928
		private readonly Guid mdbGuid;
	}
}
