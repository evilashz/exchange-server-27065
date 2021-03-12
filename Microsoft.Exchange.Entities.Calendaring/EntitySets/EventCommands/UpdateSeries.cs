using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Entities.Calendaring;
using Microsoft.Exchange.Entities.Calendaring.DataProviders;
using Microsoft.Exchange.Entities.Calendaring.Interop;
using Microsoft.Exchange.Entities.DataModel;
using Microsoft.Exchange.Entities.DataModel.Calendaring;
using Microsoft.Exchange.Entities.DataModel.ReliableActions;

namespace Microsoft.Exchange.Entities.Calendaring.EntitySets.EventCommands
{
	// Token: 0x02000057 RID: 87
	[ClassAccessLevel(AccessLevel.Implementation)]
	[DataContract]
	internal class UpdateSeries : UpdateEventBase, ICalendarInteropSeriesAction
	{
		// Token: 0x1700008C RID: 140
		// (get) Token: 0x06000242 RID: 578 RVA: 0x000092E4 File Offset: 0x000074E4
		public virtual Guid CommandId
		{
			get
			{
				return base.Id;
			}
		}

		// Token: 0x1700008D RID: 141
		// (get) Token: 0x06000243 RID: 579 RVA: 0x000092EC File Offset: 0x000074EC
		protected override ITracer Trace
		{
			get
			{
				return ExTraceGlobals.UpdateSeriesTracer;
			}
		}

		// Token: 0x06000244 RID: 580 RVA: 0x000092F4 File Offset: 0x000074F4
		public void ExecuteOnInstance(Event seriesInstanceInformation)
		{
			Event @event = new Event
			{
				Id = seriesInstanceInformation.Id,
				ChangeKey = seriesInstanceInformation.ChangeKey,
				Type = EventType.Exception
			};
			this.PrepareDataForInstance(@event);
			IActionPropagationState actionPropagationState = @event;
			actionPropagationState.LastExecutedAction = new Guid?(this.CommandId);
			UpdateEvent updateEvent = new UpdateEvent
			{
				EntityKey = @event.Id,
				Entity = @event,
				Scope = this.Scope,
				SeriesSequenceNumber = new int?(this.seriesSequenceNumber),
				SendMeetingMessagesOnSave = true,
				MasterGoid = this.masterGoid,
				PropagationInProgress = true
			};
			Event event2 = updateEvent.Execute(new CommandContext
			{
				IfMatchETag = seriesInstanceInformation.ChangeKey
			});
			seriesInstanceInformation.ChangeKey = event2.ChangeKey;
		}

		// Token: 0x06000245 RID: 581 RVA: 0x000093CB File Offset: 0x000075CB
		public void RestoreExecutionContext(Events entitySet, SeriesInteropCommand seriesInteropCommand)
		{
			this.seriesPropagation = seriesInteropCommand;
			base.RestoreScope(entitySet);
		}

		// Token: 0x06000246 RID: 582 RVA: 0x000093DB File Offset: 0x000075DB
		public Event CleanUp(Event master)
		{
			return this.seriesPropagation.RemoveActionFromPendingActionQueue(master, this.CommandId);
		}

		// Token: 0x06000247 RID: 583 RVA: 0x000093EF File Offset: 0x000075EF
		public Event GetInitialMasterValue()
		{
			return base.Entity;
		}

		// Token: 0x06000248 RID: 584 RVA: 0x000093F8 File Offset: 0x000075F8
		public Event InitialMasterOperation(Event updateToMaster)
		{
			UpdateEvent updateEvent = new UpdateEvent
			{
				Entity = updateToMaster,
				EntityKey = updateToMaster.Id,
				Scope = this.Scope,
				SendMeetingMessagesOnSave = true,
				SeriesSequenceNumber = new int?(this.seriesSequenceNumber)
			};
			return updateEvent.Execute(null);
		}

		// Token: 0x06000249 RID: 585 RVA: 0x0000944B File Offset: 0x0000764B
		protected virtual void PrepareDataForInstance(Event instanceDelta)
		{
			base.Entity.CopyMasterPropertiesTo(instanceDelta, false, null, true);
		}

		// Token: 0x0600024A RID: 586 RVA: 0x0000945C File Offset: 0x0000765C
		protected override Event OnExecute()
		{
			Event initialMasterValue = this.GetInitialMasterValue();
			Event @event = this.Scope.Read(initialMasterValue.Id, this.Context);
			base.MergeAttendeesList(@event.Attendees);
			IEventInternal eventInternal = initialMasterValue;
			IEventInternal eventInternal2 = @event;
			eventInternal.SeriesSequenceNumber = eventInternal2.SeriesSequenceNumber + 1;
			this.seriesSequenceNumber = eventInternal.SeriesSequenceNumber;
			this.masterGoid = ((eventInternal2.GlobalObjectId != null) ? new GlobalObjectId(eventInternal2.GlobalObjectId).Bytes : null);
			this.seriesPropagation = this.CreateInteropPropagationCommand(null);
			return this.seriesPropagation.Execute(null);
		}

		// Token: 0x0600024B RID: 587 RVA: 0x000094EC File Offset: 0x000076EC
		protected virtual SeriesInlineInterop CreateInteropPropagationCommand(ICalendarInteropLog logger = null)
		{
			return new SeriesInlineInterop(this, logger)
			{
				EntityKey = base.EntityKey,
				Scope = this.Scope
			};
		}

		// Token: 0x040000A1 RID: 161
		private SeriesInteropCommand seriesPropagation;

		// Token: 0x040000A2 RID: 162
		[DataMember]
		private int seriesSequenceNumber;

		// Token: 0x040000A3 RID: 163
		[DataMember]
		private byte[] masterGoid;
	}
}
