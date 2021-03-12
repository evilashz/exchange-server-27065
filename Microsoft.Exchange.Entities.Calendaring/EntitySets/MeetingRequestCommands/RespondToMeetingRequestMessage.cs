using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Entities.Calendaring;
using Microsoft.Exchange.Entities.Calendaring.DataProviders;
using Microsoft.Exchange.Entities.DataModel.Calendaring.CustomActions;
using Microsoft.Exchange.Entities.DataProviders;
using Microsoft.Exchange.Entities.EntitySets.Commands;

namespace Microsoft.Exchange.Entities.Calendaring.EntitySets.MeetingRequestCommands
{
	// Token: 0x0200005C RID: 92
	[DataContract]
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal class RespondToMeetingRequestMessage : EntityCommand<MeetingRequestMessages, VoidResult>
	{
		// Token: 0x17000097 RID: 151
		// (get) Token: 0x0600026A RID: 618 RVA: 0x000098B6 File Offset: 0x00007AB6
		// (set) Token: 0x0600026B RID: 619 RVA: 0x000098BE File Offset: 0x00007ABE
		[DataMember]
		public RespondToEventParameters Parameters { get; set; }

		// Token: 0x17000098 RID: 152
		// (get) Token: 0x0600026C RID: 620 RVA: 0x000098C7 File Offset: 0x00007AC7
		protected override ITracer Trace
		{
			get
			{
				return ExTraceGlobals.RespondToMeetingRequestTracer;
			}
		}

		// Token: 0x0600026D RID: 621 RVA: 0x000098CE File Offset: 0x00007ACE
		protected void Validate()
		{
			if (this.Parameters == null)
			{
				throw new InvalidRequestException(Strings.ErrorMissingRequiredParameter("RespondToEventParameters"));
			}
			if (this.Parameters.MeetingRequestIdToBeDeleted == null)
			{
				throw new InvalidRequestException(Strings.ErrorMissingRequiredParameter("RespondToEventParameters.MeetingRequestIdToBeDeleted"));
			}
		}

		// Token: 0x0600026E RID: 622 RVA: 0x00009908 File Offset: 0x00007B08
		protected override VoidResult OnExecute()
		{
			this.Validate();
			MeetingRequestMessageDataProvider meetingRequestMessageDataProvider = this.Scope.MeetingRequestMessageDataProvider;
			string changeKey = (this.Context == null) ? null : this.Context.IfMatchETag;
			StoreObjectId id = this.Scope.IdConverter.ToStoreObjectId(this.Parameters.MeetingRequestIdToBeDeleted);
			string key;
			using (IMeetingRequest meetingRequest = meetingRequestMessageDataProvider.BindToWrite(id, changeKey))
			{
				StoreId storeId = meetingRequestMessageDataProvider.GetCorrelatedItemId(meetingRequest);
				if (storeId == null)
				{
					storeId = this.CreateCorrelatedEvent(meetingRequest);
				}
				key = this.Scope.IdConverter.ToStringId(storeId, this.Scope.Session);
			}
			this.Scope.Events.Respond(key, this.Parameters, this.Context);
			return VoidResult.Value;
		}

		// Token: 0x0600026F RID: 623 RVA: 0x000099DC File Offset: 0x00007BDC
		protected virtual StoreId CreateCorrelatedEvent(IMeetingRequest meetingRequest)
		{
			CalendarItemBase calendarItemBase = null;
			StoreId result = null;
			try
			{
				if (meetingRequest.TryUpdateCalendarItem(ref calendarItemBase, meetingRequest.IsDelegated()))
				{
					calendarItemBase.Save(SaveMode.NoConflictResolution);
					this.Scope.MeetingRequestMessageDataProvider.SaveMeetingRequest(meetingRequest, this.Context);
					result = calendarItemBase.Id;
				}
			}
			finally
			{
				if (calendarItemBase != null)
				{
					calendarItemBase.Dispose();
				}
			}
			return result;
		}
	}
}
