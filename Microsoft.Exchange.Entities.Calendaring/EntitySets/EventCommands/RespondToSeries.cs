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
using Microsoft.Exchange.Entities.DataProviders;

namespace Microsoft.Exchange.Entities.Calendaring.EntitySets.EventCommands
{
	// Token: 0x02000054 RID: 84
	[ClassAccessLevel(AccessLevel.Implementation)]
	[DataContract]
	internal class RespondToSeries : RespondToEventBase, ICalendarInteropSeriesAction
	{
		// Token: 0x17000084 RID: 132
		// (get) Token: 0x0600021B RID: 539 RVA: 0x00008AB1 File Offset: 0x00006CB1
		public Guid CommandId
		{
			get
			{
				return base.Id;
			}
		}

		// Token: 0x17000085 RID: 133
		// (get) Token: 0x0600021C RID: 540 RVA: 0x00008AB9 File Offset: 0x00006CB9
		protected override ITracer Trace
		{
			get
			{
				return ExTraceGlobals.RespondToSeriesTracer;
			}
		}

		// Token: 0x0600021D RID: 541 RVA: 0x00008AC0 File Offset: 0x00006CC0
		public void RestoreExecutionContext(Events entitySet, SeriesInteropCommand seriesInteropCommand)
		{
			this.seriesPropagation = seriesInteropCommand;
			base.RestoreScope(entitySet);
		}

		// Token: 0x0600021E RID: 542 RVA: 0x00008AD0 File Offset: 0x00006CD0
		public Event CleanUp(Event master)
		{
			StoreObjectId id = this.Scope.IdConverter.ToStoreObjectId(master.Id);
			if (!this.CleanUpDeclinedEvent(id))
			{
				return this.seriesPropagation.RemoveActionFromPendingActionQueue(master, this.CommandId);
			}
			return null;
		}

		// Token: 0x0600021F RID: 543 RVA: 0x00008B14 File Offset: 0x00006D14
		public void ExecuteOnInstance(Event seriesInstance)
		{
			base.Parameters.MeetingRequestIdToBeDeleted = null;
			Event @event = new Event
			{
				Id = seriesInstance.Id,
				ChangeKey = seriesInstance.ChangeKey
			};
			IActionPropagationState actionPropagationState = @event;
			actionPropagationState.LastExecutedAction = new Guid?(this.CommandId);
			IEventInternal eventInternal = @event;
			eventInternal.SeriesToInstancePropagation = true;
			RespondToEvent respondToEvent = new RespondToEvent
			{
				EntityKey = @event.Id,
				UpdateToEvent = @event,
				Parameters = base.Parameters,
				Scope = this.Scope
			};
			respondToEvent.Execute(new CommandContext
			{
				IfMatchETag = seriesInstance.ChangeKey
			});
			if (base.Parameters.Response != ResponseType.Declined)
			{
				Event event2 = this.Scope.Read(base.EntityKey, null);
				seriesInstance.ChangeKey = event2.ChangeKey;
			}
		}

		// Token: 0x06000220 RID: 544 RVA: 0x00008BF4 File Offset: 0x00006DF4
		public Event GetInitialMasterValue()
		{
			return new Event
			{
				Id = base.EntityKey
			};
		}

		// Token: 0x06000221 RID: 545 RVA: 0x00008C14 File Offset: 0x00006E14
		public Event InitialMasterOperation(Event updateToMaster)
		{
			StoreId entityStoreId = this.GetEntityStoreId();
			EventDataProvider eventDataProvider = this.Scope.EventDataProvider;
			Event eventObject = eventDataProvider.Read(entityStoreId);
			this.Validate(eventObject);
			RespondToEvent respondToEvent = new RespondToEvent
			{
				EntityKey = updateToMaster.Id,
				UpdateToEvent = updateToMaster,
				SkipDeclinedEventRemoval = true,
				Parameters = base.Parameters,
				Scope = this.Scope
			};
			respondToEvent.Execute(this.Context);
			return this.Scope.Read(base.EntityKey, null);
		}

		// Token: 0x06000222 RID: 546 RVA: 0x00008CA3 File Offset: 0x00006EA3
		protected override VoidResult OnExecute()
		{
			this.seriesPropagation = this.CreateInteropPropagationCommand(null);
			this.seriesPropagation.Execute(null);
			return VoidResult.Value;
		}

		// Token: 0x06000223 RID: 547 RVA: 0x00008CC4 File Offset: 0x00006EC4
		protected virtual SeriesInlineInterop CreateInteropPropagationCommand(ICalendarInteropLog logger = null)
		{
			return new SeriesInlineInterop(this, logger)
			{
				EntityKey = base.EntityKey,
				Scope = this.Scope
			};
		}

		// Token: 0x06000224 RID: 548 RVA: 0x00008CF4 File Offset: 0x00006EF4
		protected override void Validate(Event eventObject)
		{
			base.Validate(eventObject);
			if (base.Parameters.ProposedEndTime != null || base.Parameters.ProposedStartTime != null)
			{
				throw new InvalidRequestException(CalendaringStrings.ErrorProposedNewTimeNotSupportedForNpr);
			}
		}

		// Token: 0x04000099 RID: 153
		private SeriesInteropCommand seriesPropagation;
	}
}
