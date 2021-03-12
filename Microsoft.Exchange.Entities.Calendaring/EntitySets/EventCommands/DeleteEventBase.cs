using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Entities.DataModel.Calendaring;
using Microsoft.Exchange.Entities.DataModel.Calendaring.CustomActions;
using Microsoft.Exchange.Entities.EntitySets.Commands;

namespace Microsoft.Exchange.Entities.Calendaring.EntitySets.EventCommands
{
	// Token: 0x02000046 RID: 70
	[ClassAccessLevel(AccessLevel.Implementation)]
	[DataContract]
	internal abstract class DeleteEventBase : DeleteEntityCommand<Events>
	{
		// Token: 0x060001B7 RID: 439 RVA: 0x000074E0 File Offset: 0x000056E0
		protected override VoidResult OnExecute()
		{
			Event @event = this.ReadEvent(base.EntityKey);
			if (@event.IsOrganizer)
			{
				this.CancelEvent(base.EntityKey);
			}
			else if (@event.IsCancelled)
			{
				this.DeleteCancelledEventFromAttendeesCalendar(@event);
			}
			else
			{
				RespondToEventParameters parameters = new RespondToEventParameters
				{
					Response = ResponseType.Declined,
					SendResponse = true
				};
				this.RespondToEvent(base.EntityKey, parameters);
			}
			return VoidResult.Value;
		}

		// Token: 0x060001B8 RID: 440 RVA: 0x00007549 File Offset: 0x00005749
		protected virtual Event ReadEvent(string eventId)
		{
			return this.Scope.Read(eventId, this.Context);
		}

		// Token: 0x060001B9 RID: 441 RVA: 0x0000755D File Offset: 0x0000575D
		protected virtual void CancelEvent(string eventId)
		{
			this.Scope.Cancel(eventId, null, this.Context);
		}

		// Token: 0x060001BA RID: 442 RVA: 0x00007572 File Offset: 0x00005772
		protected virtual void RespondToEvent(string eventId, RespondToEventParameters parameters)
		{
			this.Scope.Respond(eventId, parameters, this.Context);
		}

		// Token: 0x060001BB RID: 443
		protected abstract void DeleteCancelledEventFromAttendeesCalendar(Event eventToDelete);
	}
}
