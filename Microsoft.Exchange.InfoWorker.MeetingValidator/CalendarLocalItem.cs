using System;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Data.Storage.CalendarDiagnostics;
using Microsoft.Exchange.Data.Storage.Management;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.Infoworker.MeetingValidator
{
	// Token: 0x02000020 RID: 32
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal class CalendarLocalItem : CalendarInstance
	{
		// Token: 0x060000FF RID: 255 RVA: 0x00006DFE File Offset: 0x00004FFE
		internal CalendarLocalItem(ExchangePrincipal principal, MailboxSession session) : base(principal)
		{
			this.session = session;
		}

		// Token: 0x17000032 RID: 50
		// (get) Token: 0x06000100 RID: 256 RVA: 0x00006E0E File Offset: 0x0000500E
		// (set) Token: 0x06000101 RID: 257 RVA: 0x00006E16 File Offset: 0x00005016
		internal byte[] CalendarFolderId { get; set; }

		// Token: 0x06000102 RID: 258 RVA: 0x00006E20 File Offset: 0x00005020
		internal override CalendarProcessingFlags? GetCalendarConfig()
		{
			CalendarConfiguration calendarConfiguration = null;
			using (CalendarConfigurationDataProvider calendarConfigurationDataProvider = new CalendarConfigurationDataProvider(this.session))
			{
				calendarConfiguration = (CalendarConfiguration)calendarConfigurationDataProvider.Read<CalendarConfiguration>(null);
			}
			if (calendarConfiguration == null)
			{
				Globals.ConsistencyChecksTracer.TraceWarning(0L, "Could not load the attendee's calendar configuration data. Skipping mailbox.");
				return null;
			}
			return new CalendarProcessingFlags?(calendarConfiguration.AutomateProcessing);
		}

		// Token: 0x06000103 RID: 259 RVA: 0x00006E90 File Offset: 0x00005090
		internal override Inconsistency GetInconsistency(CalendarValidationContext context, string fullDescription)
		{
			if (base.LoadInconsistency != null)
			{
				return base.LoadInconsistency;
			}
			if (this.CalendarFolderId == null)
			{
				return null;
			}
			bool isCleanGlobalObjectId = context.BaseItem.GlobalObjectId.IsCleanGlobalObjectId;
			Inconsistency inconsistency;
			if (context.OppositeRole == RoleType.Attendee)
			{
				ClientIntentQuery.QueryResult missingItemIntent = this.GetMissingItemIntent(context, isCleanGlobalObjectId);
				inconsistency = (isCleanGlobalObjectId ? this.GetAttendeeMissingItemInconsistency(context, fullDescription, missingItemIntent, ClientIntentFlags.RespondedDecline, ClientIntentFlags.DeletedWithNoResponse) : this.GetAttendeeMissingItemInconsistency(context, fullDescription, missingItemIntent, ClientIntentFlags.RespondedExceptionDecline, ClientIntentFlags.DeletedExceptionWithNoResponse));
			}
			else
			{
				inconsistency = MissingItemInconsistency.CreateOrganizerMissingItemInstance(fullDescription, context);
				if (RumFactory.Instance.Policy.RepairMode == CalendarRepairType.ValidateOnly)
				{
					inconsistency.Intent = this.GetMissingItemIntent(context, isCleanGlobalObjectId).Intent;
				}
			}
			return inconsistency;
		}

		// Token: 0x06000104 RID: 260 RVA: 0x00006F2C File Offset: 0x0000512C
		internal override bool WouldTryToRepairIfMissing(CalendarValidationContext context, out MeetingInquiryAction predictedAction)
		{
			return MeetingInquiryMessage.WouldTryToRepairIfMissing(context.CvsGateway, context.AttendeeItem.GlobalObjectId, this.session, this.CalendarFolderId, out predictedAction);
		}

		// Token: 0x06000105 RID: 261 RVA: 0x00006F54 File Offset: 0x00005154
		internal override ClientIntentFlags? GetLocationIntent(CalendarValidationContext context, GlobalObjectId globalObjectId, string organizerLocation, string attendeeLocation)
		{
			ICalendarItemStateDefinition initialState = new LocationBasedCalendarItemStateDefinition(organizerLocation);
			ICalendarItemStateDefinition targetState = new LocationBasedCalendarItemStateDefinition(attendeeLocation);
			ClientIntentQuery clientIntentQuery = new TransitionalClientIntentQuery(globalObjectId, initialState, targetState);
			return clientIntentQuery.Execute(this.session, context.CvsGateway).Intent;
		}

		// Token: 0x06000106 RID: 262 RVA: 0x00006F94 File Offset: 0x00005194
		private ClientIntentQuery.QueryResult GetMissingItemIntent(CalendarValidationContext context, bool isNotOccurrence)
		{
			ClientIntentQuery clientIntentQuery;
			if (isNotOccurrence)
			{
				ICalendarItemStateDefinition deletedFromFolderStateDefinition = CompositeCalendarItemStateDefinition.GetDeletedFromFolderStateDefinition(this.CalendarFolderId);
				clientIntentQuery = new ItemDeletionClientIntentQuery(context.BaseItem.GlobalObjectId, deletedFromFolderStateDefinition);
			}
			else
			{
				ICalendarItemStateDefinition targetState = new DeletedOccurrenceCalendarItemStateDefinition(context.BaseItem.GlobalObjectId.Date, false);
				clientIntentQuery = new NonTransitionalClientIntentQuery(context.BaseItem.GlobalObjectId, targetState);
			}
			return clientIntentQuery.Execute(this.session, context.CvsGateway);
		}

		// Token: 0x06000107 RID: 263 RVA: 0x00007004 File Offset: 0x00005204
		private Inconsistency GetAttendeeMissingItemInconsistency(CalendarValidationContext context, string fullDescription, ClientIntentQuery.QueryResult inconsistencyIntent, ClientIntentFlags declineIntent, ClientIntentFlags deleteIntent)
		{
			Inconsistency inconsistency = null;
			if (ClientIntentQuery.CheckDesiredClientIntent(inconsistencyIntent.Intent, new ClientIntentFlags[]
			{
				declineIntent
			}))
			{
				inconsistency = ResponseInconsistency.CreateInstance(fullDescription, ResponseType.Decline, context.Attendee.ResponseType, ExDateTime.MinValue, context.Attendee.ReplyTime, context);
				inconsistency.Intent = inconsistencyIntent.Intent;
			}
			else if (ClientIntentQuery.CheckDesiredClientIntent(inconsistencyIntent.Intent, new ClientIntentFlags[]
			{
				deleteIntent
			}))
			{
				inconsistency = null;
			}
			else if (inconsistencyIntent.SourceVersionId != null)
			{
				int? deletedItemVersion = null;
				using (CalendarItemBase calendarItemBase = CalendarItemBase.Bind(this.session, inconsistencyIntent.SourceVersionId))
				{
					deletedItemVersion = calendarItemBase.GetValueAsNullable<int>(CalendarItemBaseSchema.ItemVersion);
				}
				if (deletedItemVersion != null)
				{
					inconsistency = MissingItemInconsistency.CreateAttendeeMissingItemInstance(fullDescription, inconsistencyIntent.Intent, deletedItemVersion, context);
					inconsistency.Intent = inconsistencyIntent.Intent;
				}
			}
			return inconsistency;
		}

		// Token: 0x04000074 RID: 116
		private MailboxSession session;
	}
}
