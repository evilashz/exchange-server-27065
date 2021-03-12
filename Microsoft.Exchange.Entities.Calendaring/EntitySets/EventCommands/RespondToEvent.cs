using System;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Data.Storage.ActivityLog;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Entities.Calendaring;
using Microsoft.Exchange.Entities.Calendaring.DataProviders;
using Microsoft.Exchange.Entities.DataModel.Calendaring;

namespace Microsoft.Exchange.Entities.Calendaring.EntitySets.EventCommands
{
	// Token: 0x02000053 RID: 83
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal class RespondToEvent : RespondToEventBase
	{
		// Token: 0x17000081 RID: 129
		// (get) Token: 0x06000212 RID: 530 RVA: 0x000089A6 File Offset: 0x00006BA6
		// (set) Token: 0x06000213 RID: 531 RVA: 0x000089AE File Offset: 0x00006BAE
		internal Event UpdateToEvent { get; set; }

		// Token: 0x17000082 RID: 130
		// (get) Token: 0x06000214 RID: 532 RVA: 0x000089B7 File Offset: 0x00006BB7
		// (set) Token: 0x06000215 RID: 533 RVA: 0x000089BF File Offset: 0x00006BBF
		internal bool SkipDeclinedEventRemoval { get; set; }

		// Token: 0x17000083 RID: 131
		// (get) Token: 0x06000216 RID: 534 RVA: 0x000089C8 File Offset: 0x00006BC8
		protected override ITracer Trace
		{
			get
			{
				return ExTraceGlobals.RespondToEventTracer;
			}
		}

		// Token: 0x06000217 RID: 535 RVA: 0x000089D0 File Offset: 0x00006BD0
		protected override VoidResult OnExecute()
		{
			StoreId entityStoreId = this.GetEntityStoreId();
			EventDataProvider eventDataProvider = this.Scope.EventDataProvider;
			Event eventObject = eventDataProvider.Read(entityStoreId);
			this.Validate(eventObject);
			eventDataProvider.RespondToEvent(entityStoreId, base.Parameters, this.UpdateToEvent);
			this.Scope.EventDataProvider.TryLogCalendarEventActivity(ActivityId.UpdateCalendarEvent, StoreId.GetStoreObjectId(entityStoreId));
			this.CleanUpDeclinedEvent(entityStoreId);
			this.DeleteMeetingRequestIfRequired(eventObject);
			return VoidResult.Value;
		}

		// Token: 0x06000218 RID: 536 RVA: 0x00008A40 File Offset: 0x00006C40
		protected virtual bool DeleteMeetingRequestIfRequired(Event eventObject)
		{
			if (!string.IsNullOrEmpty(base.Parameters.MeetingRequestIdToBeDeleted))
			{
				eventObject.DeleteRelatedMessage(base.Parameters.MeetingRequestIdToBeDeleted, DeleteItemFlags.MoveToDeletedItems, this.Scope.XsoFactory, this.Scope.IdConverter, this.Scope.Session, true);
				return true;
			}
			return false;
		}

		// Token: 0x06000219 RID: 537 RVA: 0x00008A96 File Offset: 0x00006C96
		protected override bool CleanUpDeclinedEvent(StoreId id)
		{
			return !this.SkipDeclinedEventRemoval && base.CleanUpDeclinedEvent(id);
		}
	}
}
