using System;
using System.Collections.Generic;
using System.Threading;
using Microsoft.Exchange.Collections;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Data.Storage;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Mapi;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x020006F6 RID: 1782
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal abstract class EventSink : DisposableObject
	{
		// Token: 0x0600469A RID: 18074 RVA: 0x0012CA78 File Offset: 0x0012AC78
		internal EventSink(Guid mailboxGuid, bool isPublicFolderDatabase, EventCondition condition)
		{
			using (DisposeGuard disposeGuard = this.Guard())
			{
				if (condition == null)
				{
					throw new ArgumentNullException("condition");
				}
				this.mailboxGuid = mailboxGuid;
				this.isPublicFolderDatabase = isPublicFolderDatabase;
				this.condition = new EventCondition(condition);
				this.mapiEventTypes = (MapiEventTypeFlags)0;
				if ((condition.EventType & EventType.NewMail) == EventType.NewMail)
				{
					this.mapiEventTypes |= MapiEventTypeFlags.NewMail;
				}
				if ((condition.EventType & EventType.ObjectCreated) == EventType.ObjectCreated)
				{
					this.mapiEventTypes |= MapiEventTypeFlags.ObjectCreated;
				}
				if ((condition.EventType & EventType.ObjectDeleted) == EventType.ObjectDeleted)
				{
					this.mapiEventTypes |= MapiEventTypeFlags.ObjectDeleted;
				}
				if ((condition.EventType & EventType.ObjectModified) == EventType.ObjectModified)
				{
					this.mapiEventTypes |= MapiEventTypeFlags.ObjectModified;
				}
				if ((condition.EventType & EventType.ObjectMoved) == EventType.ObjectMoved)
				{
					this.mapiEventTypes |= MapiEventTypeFlags.ObjectMoved;
				}
				if ((condition.EventType & EventType.ObjectCopied) == EventType.ObjectCopied)
				{
					this.mapiEventTypes |= MapiEventTypeFlags.ObjectCopied;
				}
				this.parentEntryIds = new byte[condition.ContainerFolderIds.Count][];
				int num = 0;
				foreach (StoreObjectId storeObjectId in condition.ContainerFolderIds)
				{
					if (storeObjectId == null)
					{
						throw new ArgumentException("condition.ContainerFolderIds contains a Null id.");
					}
					this.parentEntryIds[num++] = storeObjectId.ProviderLevelItemId;
				}
				this.objectEntryIds = new byte[condition.ObjectIds.Count][];
				num = 0;
				foreach (StoreObjectId storeObjectId2 in condition.ObjectIds)
				{
					this.objectEntryIds[num++] = storeObjectId2.ProviderLevelItemId;
				}
				if (condition.ClassNames.Count != 0)
				{
					this.considerClassNames = true;
					List<string> list = new List<string>();
					List<string> list2 = new List<string>();
					foreach (string text in condition.ClassNames)
					{
						if (text == "*")
						{
							this.considerClassNames = false;
							break;
						}
						if (text.EndsWith(".*"))
						{
							list2.Add(text.Remove(text.Length - 1));
						}
						else
						{
							list.Add(text);
						}
					}
					if (this.considerClassNames)
					{
						this.expectedClassNameExactMatches = list.ToArray();
						this.expectedClassNamePrefixes = list2.ToArray();
					}
				}
				disposeGuard.Success();
			}
		}

		// Token: 0x1700148A RID: 5258
		// (get) Token: 0x0600469B RID: 18075 RVA: 0x0012CD64 File Offset: 0x0012AF64
		public EventWatermark CurrentWatermark
		{
			get
			{
				this.CheckDisposed(null);
				return this.GetCurrentEventWatermark();
			}
		}

		// Token: 0x1700148B RID: 5259
		// (get) Token: 0x0600469C RID: 18076 RVA: 0x0012CD73 File Offset: 0x0012AF73
		internal EventPump EventPump
		{
			get
			{
				return this.eventPump;
			}
		}

		// Token: 0x1700148C RID: 5260
		// (get) Token: 0x0600469D RID: 18077 RVA: 0x0012CD7B File Offset: 0x0012AF7B
		internal Guid MailboxGuid
		{
			get
			{
				return this.mailboxGuid;
			}
		}

		// Token: 0x1700148D RID: 5261
		// (get) Token: 0x0600469E RID: 18078 RVA: 0x0012CD83 File Offset: 0x0012AF83
		internal bool IsPublicFolderDatabase
		{
			get
			{
				return this.isPublicFolderDatabase;
			}
		}

		// Token: 0x1700148E RID: 5262
		// (get) Token: 0x0600469F RID: 18079 RVA: 0x0012CD8B File Offset: 0x0012AF8B
		internal EventWatermark FirstMissedEventWaterMark
		{
			get
			{
				return this.firstMissedEventWatermark;
			}
		}

		// Token: 0x1700148F RID: 5263
		// (get) Token: 0x060046A0 RID: 18080 RVA: 0x0012CD93 File Offset: 0x0012AF93
		protected bool IsExceptionPresent
		{
			get
			{
				return this.exception != null;
			}
		}

		// Token: 0x17001490 RID: 5264
		// (get) Token: 0x060046A1 RID: 18081 RVA: 0x0012CDA1 File Offset: 0x0012AFA1
		protected Guid MdbGuid
		{
			get
			{
				return this.EventPump.MdbGuid;
			}
		}

		// Token: 0x17001491 RID: 5265
		// (get) Token: 0x060046A2 RID: 18082 RVA: 0x0012CDAE File Offset: 0x0012AFAE
		// (set) Token: 0x060046A3 RID: 18083 RVA: 0x0012CDB6 File Offset: 0x0012AFB6
		protected long FirstEventToConsumeWatermark
		{
			get
			{
				return this.firstEventToConsumeWatermark;
			}
			set
			{
				this.firstEventToConsumeWatermark = value;
			}
		}

		// Token: 0x060046A4 RID: 18084 RVA: 0x0012CDBF File Offset: 0x0012AFBF
		public override string ToString()
		{
			return string.Format("EventPump = {0}. MailboxGuid = {1}. EventCondition = {2}.", this.eventPump, this.mailboxGuid, this.condition);
		}

		// Token: 0x060046A5 RID: 18085 RVA: 0x0012CDE4 File Offset: 0x0012AFE4
		internal static T InternalCreateEventSink<T>(StoreSession session, EventWatermark watermark, EventSink.ConstructSinkDelegate<T> constructEventSinkDelegate) where T : EventSink
		{
			if (session == null)
			{
				throw new ArgumentNullException("session");
			}
			T t = constructEventSinkDelegate();
			bool flag = false;
			T result;
			try
			{
				EventSink.CheckEventPreCondition(session, t);
				EventPumpManager.Instance.RegisterEventSink(session, t);
				if (watermark != null)
				{
					t.VerifyWatermarkIsInEventTable(watermark);
				}
				flag = true;
				result = t;
			}
			finally
			{
				if (!flag && t != null)
				{
					t.Dispose();
				}
			}
			return result;
		}

		// Token: 0x060046A6 RID: 18086 RVA: 0x0012CE68 File Offset: 0x0012B068
		internal void Consume(MapiEvent mapiEvent)
		{
			if (!base.IsDisposed && mapiEvent.EventCounter >= this.FirstEventToConsumeWatermark)
			{
				this.CheckForFinalEvents(mapiEvent);
				if (this.IsEventRelevant(mapiEvent))
				{
					this.InternalConsume(mapiEvent);
				}
			}
		}

		// Token: 0x060046A7 RID: 18087 RVA: 0x0012CE97 File Offset: 0x0012B097
		internal virtual void HandleException(Exception exception)
		{
			if (!base.IsDisposed)
			{
				if (this.exception == null)
				{
					this.exception = exception;
				}
				ExTraceGlobals.EventTracer.TraceDebug<EventSink, Exception>((long)this.GetHashCode(), "EventSink::HandleException. {0}. We got an error while reading events. The EventSink has been disabled. Error = {1}.", this, this.exception);
			}
		}

		// Token: 0x060046A8 RID: 18088 RVA: 0x0012CECD File Offset: 0x0012B0CD
		internal void SetEventPump(EventPump eventPump)
		{
			this.CheckDisposed(null);
			this.eventPump = eventPump;
			ExTraceGlobals.EventTracer.TraceDebug<string, EventSink>((long)this.GetHashCode(), "{0}::SetEventPump(Construction). {1}", base.GetType().Name, this);
		}

		// Token: 0x060046A9 RID: 18089
		internal abstract void SetFirstEventToConsumeOnSink(long firstEventToConsumeWatermark);

		// Token: 0x060046AA RID: 18090
		internal abstract IRecoveryEventSink StartRecovery();

		// Token: 0x060046AB RID: 18091
		internal abstract EventWatermark GetCurrentEventWatermark();

		// Token: 0x060046AC RID: 18092
		internal abstract void SetLastKnownWatermark(long mapiWatermark, bool mayInitiateRecovery);

		// Token: 0x060046AD RID: 18093 RVA: 0x0012CF00 File Offset: 0x0012B100
		internal bool IsEventRelevant(Guid mailboxGuid, MapiEventTypeFlags mapiEventType, ObjectType mapiObjectType, MapiEventFlags mapiEventFlags, MapiExtendedEventFlags mapiExtendedEventFlags, byte[] entryId, byte[] parentEntryId, byte[] oldParentEntryId, string messageClass, string containerClass)
		{
			EnumValidator.AssertValid<ObjectType>(mapiObjectType);
			EnumValidator.AssertValid<MapiEventTypeFlags>(mapiEventType);
			if (mailboxGuid != this.mailboxGuid)
			{
				return false;
			}
			if (this.condition.EventType != EventType.None && ((this.condition.EventType & EventType.FreeBusyChanged) != EventType.FreeBusyChanged || !EventSink.HasFreeBusyChanged(messageClass, mapiEventType, mapiExtendedEventFlags)) && (mapiEventType & this.mapiEventTypes) == (MapiEventTypeFlags)0)
			{
				return false;
			}
			if (this.condition.ObjectType != EventObjectType.None)
			{
				switch (this.condition.ObjectType)
				{
				case EventObjectType.Item:
					if (mapiObjectType != ObjectType.MAPI_MESSAGE)
					{
						return false;
					}
					break;
				case EventObjectType.Folder:
					if (mapiObjectType != ObjectType.MAPI_FOLDER)
					{
						return false;
					}
					break;
				}
			}
			if (this.condition.EventSubtree != EventSubtreeFlag.All)
			{
				if (this.condition.EventSubtree == EventSubtreeFlag.NonIPMSubtree && (mapiExtendedEventFlags & MapiExtendedEventFlags.NonIPMFolder) != MapiExtendedEventFlags.NonIPMFolder)
				{
					return false;
				}
				if (this.condition.EventSubtree == EventSubtreeFlag.IPMSubtree && (mapiExtendedEventFlags & (MapiExtendedEventFlags)(-2147483648)) != (MapiExtendedEventFlags)(-2147483648))
				{
					return false;
				}
			}
			if (this.condition.EventFlags != EventFlags.None)
			{
				EventFlags eventFlags = ((mapiExtendedEventFlags & MapiExtendedEventFlags.NoReminderPropertyModified) == MapiExtendedEventFlags.NoReminderPropertyModified) ? EventFlags.None : EventFlags.ReminderPropertiesModified;
				if ((mapiExtendedEventFlags & MapiExtendedEventFlags.TimerEventFired) == MapiExtendedEventFlags.TimerEventFired)
				{
					eventFlags |= EventFlags.TimerEventFired;
				}
				if ((eventFlags & this.condition.EventFlags) != this.condition.EventFlags)
				{
					return false;
				}
			}
			if ((mapiEventFlags & MapiEventFlags.SoftDeleted) == MapiEventFlags.SoftDeleted)
			{
				return false;
			}
			if (this.parentEntryIds.Length != 0 || this.objectEntryIds.Length != 0)
			{
				bool flag = false;
				bool flag2 = (mapiEventType & MapiEventTypeFlags.ObjectMoved) == MapiEventTypeFlags.ObjectMoved;
				IEqualityComparer<byte[]> comparer = ArrayComparer<byte>.Comparer;
				foreach (byte[] x in this.parentEntryIds)
				{
					if (comparer.Equals(x, parentEntryId))
					{
						flag = true;
						break;
					}
					if (flag2 && comparer.Equals(x, oldParentEntryId))
					{
						flag = true;
						break;
					}
				}
				if (!flag)
				{
					foreach (byte[] x2 in this.objectEntryIds)
					{
						if (comparer.Equals(x2, entryId))
						{
							flag = true;
							break;
						}
					}
					if (!flag)
					{
						return false;
					}
				}
			}
			if (this.considerClassNames)
			{
				string text;
				if (mapiObjectType == ObjectType.MAPI_FOLDER)
				{
					text = containerClass;
				}
				else
				{
					text = messageClass;
				}
				foreach (string b in this.expectedClassNameExactMatches)
				{
					if (text == b)
					{
						return true;
					}
				}
				foreach (string value in this.expectedClassNamePrefixes)
				{
					if (text.StartsWith(value))
					{
						return true;
					}
				}
				return false;
			}
			return true;
		}

		// Token: 0x060046AE RID: 18094 RVA: 0x0012D170 File Offset: 0x0012B370
		internal void VerifyWatermarkIsInEventTable(EventWatermark watermark)
		{
			this.EventPump.VerifyWatermarkIsInEventTable(watermark);
		}

		// Token: 0x060046AF RID: 18095 RVA: 0x0012D17E File Offset: 0x0012B37E
		protected override void InternalDispose(bool disposing)
		{
			if (disposing)
			{
				if (this.eventPump != null)
				{
					this.eventPump.RemoveEventSink(this);
				}
				base.InternalDispose(true);
			}
		}

		// Token: 0x060046B0 RID: 18096
		protected abstract void InternalConsume(MapiEvent mapiEvent);

		// Token: 0x060046B1 RID: 18097 RVA: 0x0012D1A0 File Offset: 0x0012B3A0
		protected void CheckException()
		{
			if (!this.IsExceptionPresent)
			{
				return;
			}
			if (this.exception is FinalEventException)
			{
				throw new FinalEventException((FinalEventException)this.exception);
			}
			if (this.exception is EventNotFoundException)
			{
				throw new EventNotFoundException(ServerStrings.ExEventNotFound, this.exception);
			}
			if (this.exception is StorageTransientException)
			{
				throw new ReadEventsFailedTransientException(ServerStrings.ExReadEventsFailed, this.exception, this.GetCurrentEventWatermark());
			}
			throw new ReadEventsFailedException(ServerStrings.ExReadEventsFailed, this.exception, this.GetCurrentEventWatermark());
		}

		// Token: 0x060046B2 RID: 18098 RVA: 0x0012D22C File Offset: 0x0012B42C
		protected bool IsEventRelevant(MapiEvent mapiEvent)
		{
			return this.IsEventRelevant(mapiEvent.MailboxGuid, mapiEvent.EventMask, mapiEvent.ItemType, mapiEvent.EventFlags, mapiEvent.ExtendedEventFlags, mapiEvent.ItemEntryId, mapiEvent.ParentEntryId, mapiEvent.OldParentEntryId, mapiEvent.ObjectClass, mapiEvent.ObjectClass);
		}

		// Token: 0x060046B3 RID: 18099 RVA: 0x0012D27B File Offset: 0x0012B47B
		protected void RequestRecovery()
		{
			ThreadPool.UnsafeQueueUserWorkItem(new WaitCallback(this.RequestRecovery), null);
		}

		// Token: 0x060046B4 RID: 18100 RVA: 0x0012D290 File Offset: 0x0012B490
		protected void CheckForFinalEvents(MapiEvent mapiEvent)
		{
			if ((mapiEvent.EventMask & (MapiEventTypeFlags.CriticalError | MapiEventTypeFlags.MailboxDeleted | MapiEventTypeFlags.MailboxDisconnected | MapiEventTypeFlags.MailboxMoveStarted | MapiEventTypeFlags.MailboxMoveSucceeded | MapiEventTypeFlags.MailboxMoveFailed)) != (MapiEventTypeFlags)0 && mapiEvent.MailboxGuid == this.MailboxGuid)
			{
				throw new FinalEventException(new Event(this.MdbGuid, mapiEvent));
			}
		}

		// Token: 0x060046B5 RID: 18101 RVA: 0x0012D2C8 File Offset: 0x0012B4C8
		private static void CheckEventPreCondition(StoreSession session, EventSink eventSink)
		{
			if (session is MailboxSession && session.LogonType != LogonType.Delegated)
			{
				return;
			}
			if (eventSink.condition == null)
			{
				throw new InvalidOperationException("The condition must be specified when the subscriber is logging on as a delegate.");
			}
			if (eventSink.condition.ContainerFolderIds.Count == 0 && eventSink.condition.ObjectType != EventObjectType.Folder)
			{
				throw new InvalidOperationException("The user must specify the container folder that it's going to subscribe for events.");
			}
			if (eventSink.condition.ObjectType == EventObjectType.Folder && eventSink.condition.ObjectIds.Count == 0)
			{
				throw new InvalidOperationException("The user must specify the id of the folder for a folder events.");
			}
			if (eventSink.condition.ContainerFolderIds.Count > 0)
			{
				EventSink.CheckPermissions(session, eventSink.condition.ContainerFolderIds);
			}
			if (eventSink.condition.ObjectType == EventObjectType.Folder)
			{
				EventSink.CheckPermissions(session, eventSink.condition.ObjectIds);
			}
		}

		// Token: 0x060046B6 RID: 18102 RVA: 0x0012D394 File Offset: 0x0012B594
		private static void CheckPermissions(StoreSession session, ICollection<StoreObjectId> ids)
		{
			foreach (StoreObjectId storeObjectId in ids)
			{
				using (Folder folder = Folder.Bind(session, storeObjectId, new PropertyDefinition[]
				{
					InternalSchema.EffectiveRights
				}))
				{
					EffectiveRights valueOrDefault = folder.GetValueOrDefault<EffectiveRights>(InternalSchema.EffectiveRights);
					if ((valueOrDefault & EffectiveRights.Read) == EffectiveRights.None)
					{
						throw new AccessDeniedException(ServerStrings.UserHasNoEventPermisson(storeObjectId.ToBase64String()));
					}
				}
			}
		}

		// Token: 0x060046B7 RID: 18103 RVA: 0x0012D42C File Offset: 0x0012B62C
		private static bool HasFreeBusyChanged(string messageClass, MapiEventTypeFlags mapiEventType, MapiExtendedEventFlags mapiExtendedEventFlags)
		{
			if (messageClass == "IPM.Appointment")
			{
				if (mapiEventType == MapiEventTypeFlags.ObjectModified)
				{
					bool flag = (mapiExtendedEventFlags & MapiExtendedEventFlags.AppointmentFreeBusyNotModified) != MapiExtendedEventFlags.AppointmentFreeBusyNotModified;
					if (flag)
					{
						return true;
					}
					flag = ((mapiExtendedEventFlags & MapiExtendedEventFlags.AppointmentTimeNotModified) != MapiExtendedEventFlags.AppointmentTimeNotModified);
					if (flag)
					{
						return true;
					}
				}
				else if (mapiEventType == MapiEventTypeFlags.ObjectCreated || mapiEventType == MapiEventTypeFlags.ObjectDeleted || mapiEventType == MapiEventTypeFlags.ObjectMoved)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x060046B8 RID: 18104 RVA: 0x0012D487 File Offset: 0x0012B687
		private void RequestRecovery(object state)
		{
			this.EventPump.RequestRecovery(this);
		}

		// Token: 0x040026A0 RID: 9888
		private const MapiEventTypeFlags FinalEvents = MapiEventTypeFlags.CriticalError | MapiEventTypeFlags.MailboxDeleted | MapiEventTypeFlags.MailboxDisconnected | MapiEventTypeFlags.MailboxMoveStarted | MapiEventTypeFlags.MailboxMoveSucceeded | MapiEventTypeFlags.MailboxMoveFailed;

		// Token: 0x040026A1 RID: 9889
		protected EventWatermark firstMissedEventWatermark;

		// Token: 0x040026A2 RID: 9890
		private readonly Guid mailboxGuid;

		// Token: 0x040026A3 RID: 9891
		private readonly bool isPublicFolderDatabase;

		// Token: 0x040026A4 RID: 9892
		private readonly EventCondition condition;

		// Token: 0x040026A5 RID: 9893
		private readonly MapiEventTypeFlags mapiEventTypes;

		// Token: 0x040026A6 RID: 9894
		private readonly byte[][] parentEntryIds;

		// Token: 0x040026A7 RID: 9895
		private readonly byte[][] objectEntryIds;

		// Token: 0x040026A8 RID: 9896
		private readonly string[] expectedClassNameExactMatches;

		// Token: 0x040026A9 RID: 9897
		private readonly string[] expectedClassNamePrefixes;

		// Token: 0x040026AA RID: 9898
		private readonly bool considerClassNames;

		// Token: 0x040026AB RID: 9899
		private EventPump eventPump;

		// Token: 0x040026AC RID: 9900
		private Exception exception;

		// Token: 0x040026AD RID: 9901
		private long firstEventToConsumeWatermark = long.MinValue;

		// Token: 0x020006F7 RID: 1783
		// (Invoke) Token: 0x060046BA RID: 18106
		internal delegate T ConstructSinkDelegate<T>() where T : EventSink;
	}
}
