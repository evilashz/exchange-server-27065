using System;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Data.Storage.CalendarDiagnostics;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Services;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Core
{
	// Token: 0x020002FE RID: 766
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal sealed class GetClientIntent : SingleStepServiceCommand<GetClientIntentRequest, GetClientIntentResponseMessage>
	{
		// Token: 0x060015A2 RID: 5538 RVA: 0x000704D7 File Offset: 0x0006E6D7
		public GetClientIntent(CallContext callContext, GetClientIntentRequest request) : base(callContext, request)
		{
			this.globalObjectId = request.GlobalObjectId;
			this.stateDefinition = request.StateDefinition;
		}

		// Token: 0x060015A3 RID: 5539 RVA: 0x000704F9 File Offset: 0x0006E6F9
		internal override IExchangeWebMethodResponse GetResponse()
		{
			return new GetClientIntentResponseMessage(base.Result.Code, base.Result.Error, base.Result.Value);
		}

		// Token: 0x060015A4 RID: 5540 RVA: 0x00070524 File Offset: 0x0006E724
		internal override ServiceResult<GetClientIntentResponseMessage> Execute()
		{
			if (this.stateDefinition == null || this.stateDefinition.Length < 1)
			{
				throw new ClientIntentInvalidStateDefinitionException();
			}
			BaseCalendarItemStateDefinition baseCalendarItemStateDefinition = this.stateDefinition[0];
			GetClientIntent.Tracer.TraceDebug<string>(0L, "GetClientIntent called for GlobalObjectId '{0}'", this.globalObjectId);
			byte[] globalObjectIdBytes = Convert.FromBase64String(this.globalObjectId);
			GlobalObjectId globalObjectId = new GlobalObjectId(globalObjectIdBytes);
			MailboxSession mailboxIdentityMailboxSession = base.CallContext.SessionCache.GetMailboxIdentityMailboxSession();
			byte[] providerLevelItemId;
			using (CalendarFolder calendarFolder = CalendarFolder.Bind(mailboxIdentityMailboxSession, DefaultFolderType.Calendar, null))
			{
				providerLevelItemId = calendarFolder.Id.ObjectId.ProviderLevelItemId;
			}
			ClientIntentQuery clientIntentQuery = null;
			if (baseCalendarItemStateDefinition is DeleteFromFolderStateDefinition)
			{
				DeleteFromFolderStateDefinition deleteFromFolderStateDefinition = (DeleteFromFolderStateDefinition)baseCalendarItemStateDefinition;
				ICalendarItemStateDefinition deletedFromFolderStateDefinition = CompositeCalendarItemStateDefinition.GetDeletedFromFolderStateDefinition(providerLevelItemId);
				clientIntentQuery = new ItemDeletionClientIntentQuery(globalObjectId, deletedFromFolderStateDefinition);
			}
			else if (baseCalendarItemStateDefinition is DeletedOccurrenceStateDefinition)
			{
				DeletedOccurrenceStateDefinition deletedOccurrenceStateDefinition = (DeletedOccurrenceStateDefinition)baseCalendarItemStateDefinition;
				ICalendarItemStateDefinition targetState = new DeletedOccurrenceCalendarItemStateDefinition(deletedOccurrenceStateDefinition.OccurrenceExDateTime, deletedOccurrenceStateDefinition.IsOccurrencePresent);
				clientIntentQuery = new NonTransitionalClientIntentQuery(globalObjectId, targetState);
			}
			else if (baseCalendarItemStateDefinition is LocationBasedStateDefinition)
			{
				LocationBasedStateDefinition locationBasedStateDefinition = (LocationBasedStateDefinition)baseCalendarItemStateDefinition;
				ICalendarItemStateDefinition initialState = new LocationBasedCalendarItemStateDefinition(locationBasedStateDefinition.OrganizerLocation);
				ICalendarItemStateDefinition targetState2 = new LocationBasedCalendarItemStateDefinition(locationBasedStateDefinition.AttendeeLocation);
				clientIntentQuery = new TransitionalClientIntentQuery(globalObjectId, initialState, targetState2);
			}
			if (clientIntentQuery == null)
			{
				throw new ClientIntentInvalidStateDefinitionException();
			}
			CalendarVersionStoreGateway cvsGateway = new CalendarVersionStoreGateway(new CalendarVersionStoreQueryPolicy(CalendarVersionStoreQueryPolicy.DefaultWaitTimeForPopulation), true);
			ClientIntentQuery.QueryResult queryResult = clientIntentQuery.Execute(mailboxIdentityMailboxSession, cvsGateway);
			if (queryResult.SourceVersionId == null)
			{
				throw new ClientIntentNotFoundException();
			}
			int? valueAsNullable;
			using (CalendarItemBase calendarItemBase = CalendarItemBase.Bind(mailboxIdentityMailboxSession, queryResult.SourceVersionId))
			{
				valueAsNullable = calendarItemBase.GetValueAsNullable<int>(CalendarItemBaseSchema.ItemVersion);
			}
			MeetingInquiryAction action;
			bool wouldRepair = MeetingInquiryMessage.WouldTryToRepairIfMissing(cvsGateway, globalObjectId, mailboxIdentityMailboxSession, providerLevelItemId, out action);
			MailboxId mailboxId = new MailboxId(mailboxIdentityMailboxSession);
			GetClientIntentResponseMessage getClientIntentResponseMessage = new GetClientIntentResponseMessage();
			GetClientIntentResponseMessage getClientIntentResponseMessage2 = getClientIntentResponseMessage;
			ClientIntent clientIntent = new ClientIntent();
			clientIntent.ItemId = IdConverter.GetItemIdFromStoreId(queryResult.SourceVersionId, mailboxId);
			ClientIntent clientIntent2 = clientIntent;
			ClientIntentFlags? intent = queryResult.Intent;
			clientIntent2.Intent = ((intent != null) ? new int?((int)intent.GetValueOrDefault()) : null);
			clientIntent.ItemVersion = valueAsNullable;
			clientIntent.WouldRepair = wouldRepair;
			clientIntent.PredictedAction = GetClientIntent.ConvertToClientIntentMeetingInquiryAction(action);
			getClientIntentResponseMessage2.ClientIntent = clientIntent;
			return new ServiceResult<GetClientIntentResponseMessage>(getClientIntentResponseMessage);
		}

		// Token: 0x060015A5 RID: 5541 RVA: 0x00070768 File Offset: 0x0006E968
		private static ClientIntentMeetingInquiryAction ConvertToClientIntentMeetingInquiryAction(MeetingInquiryAction action)
		{
			switch (action)
			{
			case MeetingInquiryAction.SendCancellation:
				return ClientIntentMeetingInquiryAction.SendCancellation;
			case MeetingInquiryAction.ReviveMeeting:
				return ClientIntentMeetingInquiryAction.ReviveMeeting;
			case MeetingInquiryAction.SendUpdateForMaster:
				return ClientIntentMeetingInquiryAction.SendUpdateForMaster;
			case MeetingInquiryAction.MeetingAlreadyExists:
				return ClientIntentMeetingInquiryAction.MeetingAlreadyExists;
			case MeetingInquiryAction.ExistingOccurrence:
				return ClientIntentMeetingInquiryAction.ExistingOccurrence;
			case MeetingInquiryAction.HasDelegates:
				return ClientIntentMeetingInquiryAction.HasDelegates;
			case MeetingInquiryAction.PairedCancellationFound:
				return ClientIntentMeetingInquiryAction.PairedCancellationFound;
			case MeetingInquiryAction.FailedToRevive:
				return ClientIntentMeetingInquiryAction.FailedToRevive;
			}
			return ClientIntentMeetingInquiryAction.DeletedVersionNotFound;
		}

		// Token: 0x04000EB1 RID: 3761
		private static readonly Trace Tracer = ExTraceGlobals.ServiceCommandBaseCallTracer;

		// Token: 0x04000EB2 RID: 3762
		private readonly string globalObjectId;

		// Token: 0x04000EB3 RID: 3763
		private readonly BaseCalendarItemStateDefinition[] stateDefinition;
	}
}
