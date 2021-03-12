using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Entities.Calendaring;
using Microsoft.Exchange.Entities.Calendaring.Interop;
using Microsoft.Exchange.Entities.DataModel;
using Microsoft.Exchange.Entities.DataModel.Calendaring;
using Microsoft.Exchange.Entities.DataModel.ReliableActions;
using Microsoft.Exchange.Entities.DataProviders;

namespace Microsoft.Exchange.Entities.Calendaring.EntitySets.EventCommands
{
	// Token: 0x0200004F RID: 79
	[ClassAccessLevel(AccessLevel.Implementation)]
	[DataContract]
	internal class ForwardSeries : ForwardEventBase, ICalendarInteropSeriesAction
	{
		// Token: 0x1700007B RID: 123
		// (get) Token: 0x060001F8 RID: 504 RVA: 0x00008470 File Offset: 0x00006670
		public Guid CommandId
		{
			get
			{
				return base.Id;
			}
		}

		// Token: 0x1700007C RID: 124
		// (get) Token: 0x060001F9 RID: 505 RVA: 0x00008478 File Offset: 0x00006678
		protected override ITracer Trace
		{
			get
			{
				return ExTraceGlobals.ForwardSeriesTracer;
			}
		}

		// Token: 0x060001FA RID: 506 RVA: 0x0000847F File Offset: 0x0000667F
		public void RestoreExecutionContext(Events entitySet, SeriesInteropCommand seriesInteropCommand)
		{
			this.seriesPropagation = seriesInteropCommand;
			base.RestoreScope(entitySet);
		}

		// Token: 0x060001FB RID: 507 RVA: 0x0000848F File Offset: 0x0000668F
		public Event CleanUp(Event master)
		{
			return this.seriesPropagation.RemoveActionFromPendingActionQueue(master, this.CommandId);
		}

		// Token: 0x060001FC RID: 508 RVA: 0x000084A4 File Offset: 0x000066A4
		public void ExecuteOnInstance(Event seriesInstance)
		{
			Event @event = new Event
			{
				Id = seriesInstance.Id,
				ChangeKey = seriesInstance.ChangeKey
			};
			IActionPropagationState actionPropagationState = @event;
			actionPropagationState.LastExecutedAction = new Guid?(this.CommandId);
			ForwardEvent forwardEvent = new ForwardEvent
			{
				EntityKey = @event.Id,
				UpdateToEvent = @event,
				Parameters = base.Parameters,
				Scope = this.Scope,
				SeriesSequenceNumber = new int?(this.seriesSequenceNumber)
			};
			forwardEvent.Execute(new CommandContext
			{
				IfMatchETag = seriesInstance.ChangeKey
			});
		}

		// Token: 0x060001FD RID: 509 RVA: 0x0000854C File Offset: 0x0000674C
		public Event GetInitialMasterValue()
		{
			return new Event
			{
				Id = base.EntityKey
			};
		}

		// Token: 0x060001FE RID: 510 RVA: 0x0000856C File Offset: 0x0000676C
		public Event InitialMasterOperation(Event updateToMaster)
		{
			ForwardEvent forwardEvent = new ForwardEvent
			{
				EntityKey = updateToMaster.Id,
				UpdateToEvent = updateToMaster,
				Parameters = base.Parameters,
				Scope = this.Scope,
				SeriesSequenceNumber = new int?(this.seriesSequenceNumber),
				OccurrencesViewPropertiesBlob = this.occurrencesViewPropertiesBlob
			};
			forwardEvent.Execute(this.Context);
			return this.Scope.Read(base.EntityKey, null);
		}

		// Token: 0x060001FF RID: 511 RVA: 0x000085E8 File Offset: 0x000067E8
		protected override VoidResult OnExecute()
		{
			Event @event = this.Scope.Read(base.EntityKey, this.Context);
			if (!@event.HasAttendees)
			{
				throw new InvalidRequestException(CalendaringStrings.ErrorForwardNotSupportedForNprAppointment);
			}
			this.occurrencesViewPropertiesBlob = this.Scope.EventDataProvider.CreateOccurrenceViewPropertiesBlob(@event);
			this.seriesSequenceNumber = ((IEventInternal)@event).SeriesSequenceNumber;
			this.seriesPropagation = this.CreateInteropPropagationCommand(null);
			this.seriesPropagation.Execute(null);
			return VoidResult.Value;
		}

		// Token: 0x06000200 RID: 512 RVA: 0x00008664 File Offset: 0x00006864
		protected virtual SeriesInlineInterop CreateInteropPropagationCommand(ICalendarInteropLog logger = null)
		{
			return new SeriesInlineInterop(this, logger)
			{
				EntityKey = base.EntityKey,
				Scope = this.Scope
			};
		}

		// Token: 0x04000090 RID: 144
		private SeriesInteropCommand seriesPropagation;

		// Token: 0x04000091 RID: 145
		[DataMember]
		private int seriesSequenceNumber;

		// Token: 0x04000092 RID: 146
		private string occurrencesViewPropertiesBlob;
	}
}
