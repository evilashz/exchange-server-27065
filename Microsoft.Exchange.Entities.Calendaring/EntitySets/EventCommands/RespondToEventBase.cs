using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Entities.DataModel.Calendaring;
using Microsoft.Exchange.Entities.DataModel.Calendaring.CustomActions;
using Microsoft.Exchange.Entities.DataProviders;
using Microsoft.Exchange.Entities.EntitySets.Commands;

namespace Microsoft.Exchange.Entities.Calendaring.EntitySets.EventCommands
{
	// Token: 0x02000052 RID: 82
	[DataContract]
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal abstract class RespondToEventBase : KeyedEntityCommand<Events, VoidResult>
	{
		// Token: 0x17000080 RID: 128
		// (get) Token: 0x0600020B RID: 523 RVA: 0x000088B0 File Offset: 0x00006AB0
		// (set) Token: 0x0600020C RID: 524 RVA: 0x000088B8 File Offset: 0x00006AB8
		[DataMember]
		public RespondToEventParameters Parameters { get; set; }

		// Token: 0x0600020D RID: 525 RVA: 0x000088C1 File Offset: 0x00006AC1
		protected override string GetCommandTraceDetails()
		{
			return string.Format("{0}?Response={1}", base.GetCommandTraceDetails(), this.Parameters.Response);
		}

		// Token: 0x0600020E RID: 526 RVA: 0x000088E3 File Offset: 0x00006AE3
		protected override void UpdateCustomLoggingData()
		{
			base.UpdateCustomLoggingData();
			this.SetCustomLoggingData("RespondToEventParameters", this.Parameters);
		}

		// Token: 0x0600020F RID: 527 RVA: 0x000088FC File Offset: 0x00006AFC
		protected virtual void Validate(Event eventObject)
		{
			if (!eventObject.ResponseRequested && this.Parameters.SendResponse)
			{
				throw new InvalidRequestException(CalendaringStrings.ErrorResponseNotRequested);
			}
			if (eventObject.IsOrganizer)
			{
				throw new InvalidRequestException(CalendaringStrings.ErrorOrganizerCantRespond);
			}
			if (eventObject.IsCancelled)
			{
				throw new InvalidRequestException(CalendaringStrings.ErrorRespondToCancelledEvent);
			}
		}

		// Token: 0x06000210 RID: 528 RVA: 0x00008950 File Offset: 0x00006B50
		protected virtual bool CleanUpDeclinedEvent(StoreId id)
		{
			if (this.Parameters.Response == ResponseType.Declined)
			{
				DeleteItemFlags deleteItemFlags = DeleteItemFlags.MoveToDeletedItems;
				deleteItemFlags |= (this.Parameters.SendResponse ? DeleteItemFlags.DeclineCalendarItemWithResponse : DeleteItemFlags.DeclineCalendarItemWithoutResponse);
				this.Scope.EventDataProvider.Delete(id, deleteItemFlags);
				return true;
			}
			return false;
		}

		// Token: 0x04000095 RID: 149
		protected const DeleteItemFlags MoveToDeletedItems = DeleteItemFlags.MoveToDeletedItems;
	}
}
