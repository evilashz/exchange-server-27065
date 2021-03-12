using System;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Services.Core.Types;
using Microsoft.Exchange.Services.Wcf.Types;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x02000972 RID: 2418
	internal sealed class SubscribeInternalCalendar : CalendarActionBase<CalendarActionFolderIdResponse>
	{
		// Token: 0x06004574 RID: 17780 RVA: 0x000F3B37 File Offset: 0x000F1D37
		public SubscribeInternalCalendar(MailboxSession subscriberSession, ADRecipient publisherRecipient, Guid groupId) : base(subscriberSession)
		{
			this.publisherRecipient = publisherRecipient;
			this.groupId = groupId;
		}

		// Token: 0x06004575 RID: 17781 RVA: 0x000F3B50 File Offset: 0x000F1D50
		public override CalendarActionFolderIdResponse Execute()
		{
			VersionedId versionedId = null;
			SubscribeInternalCalendarCommand.TraceDebug(this, "Checking that is a valid user");
			if (this.publisherRecipient == null)
			{
				SubscribeInternalCalendarCommand.TraceDebug(this, "We can open a folder only from an internal user. User passed is invalid user.");
				return new CalendarActionFolderIdResponse(CalendarActionError.CalendarActionUnableToSubscribeToCalendar);
			}
			SubscribeInternalCalendarCommand.TraceDebug(this, "Using the LegacyDN from the current session and the publisherRecipient to identify if they refer to the same user");
			if (string.Equals(base.MailboxSession.MailboxOwnerLegacyDN, this.publisherRecipient.LegacyExchangeDN, StringComparison.OrdinalIgnoreCase))
			{
				SubscribeInternalCalendarCommand.TraceDebug(this, "Cannot subscribe to own folder");
				return new CalendarActionFolderIdResponse(CalendarActionError.CalendarActionCannotSubscribeToOwnCalendar);
			}
			SubscribeInternalCalendarCommand.TraceDebug(this, "Checking if this is a Distribution list");
			MailboxHelper.MailboxTypeType mailboxTypeType = MailboxHelper.ConvertToMailboxType(this.publisherRecipient.RecipientType, this.publisherRecipient.RecipientTypeDetails);
			if (mailboxTypeType == MailboxHelper.MailboxTypeType.PublicDL)
			{
				SubscribeInternalCalendarCommand.TraceDebug(this, "Cannot subscribe to a Distribution List");
				return new CalendarActionFolderIdResponse(CalendarActionError.CalendarActionCannotSubscribeToDistributionList);
			}
			CalendarActionFolderIdResponse result;
			try
			{
				SubscribeInternalCalendarCommand.TraceDebug(this, "Binding to the groupId passed to the request: " + this.groupId);
				using (CalendarGroup calendarGroup = CalendarGroup.Bind(base.MailboxSession, this.groupId))
				{
					CalendarGroupEntryInfo calendarGroupEntryInfo = calendarGroup.FindSharedGSCalendaryEntry(this.publisherRecipient.LegacyExchangeDN);
					if (calendarGroupEntryInfo != null)
					{
						versionedId = calendarGroupEntryInfo.Id;
					}
					else
					{
						using (CalendarGroupEntry calendarGroupEntry = CalendarGroupEntry.Create(base.MailboxSession, this.publisherRecipient.LegacyExchangeDN, calendarGroup))
						{
							if (calendarGroupEntry != null)
							{
								versionedId = this.CreateAndLoadCalendarGroupEntry(this.publisherRecipient.DisplayName, calendarGroupEntry);
							}
						}
					}
				}
				if (versionedId == null)
				{
					SubscribeInternalCalendarCommand.TraceError(this, "Could not create the entry");
					result = new CalendarActionFolderIdResponse(CalendarActionError.CalendarActionUnableToSubscribeToCalendar);
				}
				else
				{
					SubscribeInternalCalendarCommand.TraceDebug(this, "Suceessfully created the entry");
					result = new CalendarActionFolderIdResponse(null, IdConverter.ConvertStoreItemIdToItemId(versionedId, base.MailboxSession));
				}
			}
			catch (ObjectNotFoundException ex)
			{
				string message = string.Format("Parent group id is invalid. ParentGroupId: '{0}'", this.groupId);
				SubscribeInternalCalendarCommand.TraceError(this, ex, message);
				result = new CalendarActionFolderIdResponse(CalendarActionError.CalendarActionInvalidGroupId);
			}
			return result;
		}

		// Token: 0x06004576 RID: 17782 RVA: 0x000F3D28 File Offset: 0x000F1F28
		private VersionedId CreateAndLoadCalendarGroupEntry(string calendarName, CalendarGroupEntry newCalendarGroupEntry)
		{
			SubscribeInternalCalendarCommand.TraceDebug(this, "Creating calendar with name: " + calendarName);
			newCalendarGroupEntry.CalendarName = calendarName;
			ConflictResolutionResult conflictResolutionResult = newCalendarGroupEntry.Save(SaveMode.NoConflictResolution);
			if (conflictResolutionResult.SaveStatus == SaveResult.Success)
			{
				newCalendarGroupEntry.Load();
				SubscribeInternalCalendarCommand.TraceDebug(this, "Successfully created calendar with Name: " + calendarName);
				return newCalendarGroupEntry.CalendarGroupEntryId;
			}
			SubscribeInternalCalendarCommand.TraceError(this, "Unable to create new calendar folder on CommonsView. Name: " + calendarName);
			return null;
		}

		// Token: 0x04002866 RID: 10342
		private readonly Guid groupId;

		// Token: 0x04002867 RID: 10343
		private readonly ADRecipient publisherRecipient;
	}
}
