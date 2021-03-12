using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Core.DataConverter
{
	// Token: 0x020000D8 RID: 216
	internal class AttendeesProperty : ComplexPropertyBase, IPregatherParticipants, IToXmlCommand, IToServiceObjectCommand, ISetCommand, IAppendUpdateCommand, IDeleteUpdateCommand, ISetUpdateCommand, IUpdateCommand, IPropertyCommand
	{
		// Token: 0x060005D0 RID: 1488 RVA: 0x0001E6C4 File Offset: 0x0001C8C4
		private AttendeesProperty(CommandContext commandContext, AttendeeType attendeeType) : base(commandContext)
		{
			this.attendeeType = attendeeType;
		}

		// Token: 0x060005D1 RID: 1489 RVA: 0x0001E6D4 File Offset: 0x0001C8D4
		public static AttendeesProperty CreateCommandForOptionalAttendees(CommandContext commandContext)
		{
			return new AttendeesProperty(commandContext, AttendeeType.Optional);
		}

		// Token: 0x060005D2 RID: 1490 RVA: 0x0001E6DD File Offset: 0x0001C8DD
		public static AttendeesProperty CreateCommandForRequiredAttendees(CommandContext commandContext)
		{
			return new AttendeesProperty(commandContext, AttendeeType.Required);
		}

		// Token: 0x060005D3 RID: 1491 RVA: 0x0001E6E6 File Offset: 0x0001C8E6
		public static AttendeesProperty CreateCommandForResources(CommandContext commandContext)
		{
			return new AttendeesProperty(commandContext, AttendeeType.Resource);
		}

		// Token: 0x060005D4 RID: 1492 RVA: 0x0001E6F0 File Offset: 0x0001C8F0
		void IPregatherParticipants.Pregather(StoreObject storeObject, List<Participant> participants)
		{
			CalendarItemBase calendarItemBase = storeObject as CalendarItemBase;
			int num = 0;
			ToServiceObjectCommandSettings commandSettings = base.GetCommandSettings<ToServiceObjectCommandSettings>();
			ItemResponseShape itemResponseShape = commandSettings.ResponseShape as ItemResponseShape;
			if (calendarItemBase == null)
			{
				calendarItemBase = ((MeetingRequest)storeObject).GetCachedEmbeddedItem();
				using (IEnumerator<Attendee> enumerator = calendarItemBase.AttendeeCollection.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						Attendee attendee = enumerator.Current;
						if (attendee.AttendeeType == this.attendeeType)
						{
							participants.Add(attendee.Participant);
							num++;
							if (itemResponseShape.MaximumRecipientsToReturn > 0 && num >= itemResponseShape.MaximumRecipientsToReturn)
							{
								break;
							}
						}
					}
					return;
				}
			}
			foreach (Attendee attendee2 in calendarItemBase.AttendeeCollection)
			{
				if (attendee2.AttendeeType == this.attendeeType)
				{
					participants.Add(attendee2.Participant);
					num++;
					if (itemResponseShape.MaximumRecipientsToReturn > 0 && num >= itemResponseShape.MaximumRecipientsToReturn)
					{
						break;
					}
				}
			}
		}

		// Token: 0x060005D5 RID: 1493 RVA: 0x0001E808 File Offset: 0x0001CA08
		public void ToXml()
		{
			throw new InvalidOperationException("Do not call AttendeesProperty.ToXml");
		}

		// Token: 0x17000113 RID: 275
		// (get) Token: 0x060005D6 RID: 1494 RVA: 0x0001E814 File Offset: 0x0001CA14
		public override bool ToServiceObjectRequiresMailboxAccess
		{
			get
			{
				return true;
			}
		}

		// Token: 0x060005D7 RID: 1495 RVA: 0x0001E818 File Offset: 0x0001CA18
		public void ToServiceObject()
		{
			ToServiceObjectCommandSettings commandSettings = base.GetCommandSettings<ToServiceObjectCommandSettings>();
			ServiceObject serviceObject = commandSettings.ServiceObject;
			CalendarItemBase calendarItemBase = commandSettings.StoreObject as CalendarItemBase;
			ItemResponseShape itemResponseShape = commandSettings.ResponseShape as ItemResponseShape;
			if (calendarItemBase == null)
			{
				calendarItemBase = ((MeetingRequest)commandSettings.StoreObject).GetCachedEmbeddedItem();
				serviceObject[this.commandContext.PropertyInformation] = this.AttendeeCollectionToEwsAttendees(calendarItemBase, itemResponseShape.MaximumRecipientsToReturn);
				return;
			}
			serviceObject[this.commandContext.PropertyInformation] = this.AttendeeCollectionToEwsAttendees(calendarItemBase, itemResponseShape.MaximumRecipientsToReturn);
		}

		// Token: 0x060005D8 RID: 1496 RVA: 0x0001E89C File Offset: 0x0001CA9C
		public void Set()
		{
			SetCommandSettings commandSettings = base.GetCommandSettings<SetCommandSettings>();
			ServiceObject serviceObject = commandSettings.ServiceObject;
			CalendarItemBase calendarItemBase = (CalendarItemBase)commandSettings.StoreObject;
			this.SetProperty(serviceObject, calendarItemBase, false);
		}

		// Token: 0x060005D9 RID: 1497 RVA: 0x0001E8CC File Offset: 0x0001CACC
		public override void SetUpdate(SetPropertyUpdate setPropertyUpdate, UpdateCommandSettings updateCommandSettings)
		{
			CalendarItemBase calendarItemBase = (CalendarItemBase)updateCommandSettings.StoreObject;
			ServiceObject serviceObject = setPropertyUpdate.ServiceObject;
			this.SetProperty(serviceObject, calendarItemBase, false);
		}

		// Token: 0x060005DA RID: 1498 RVA: 0x0001E8F8 File Offset: 0x0001CAF8
		public override void DeleteUpdate(DeletePropertyUpdate deletePropertyUpdate, UpdateCommandSettings updateCommandSettings)
		{
			CalendarItemBase calendarItemBase = (CalendarItemBase)updateCommandSettings.StoreObject;
			this.ClearAttendeeTypeFromCollection(calendarItemBase.AttendeeCollection);
			if (calendarItemBase.AttachmentCollection.Count == 0 && !calendarItemBase.MeetingRequestWasSent)
			{
				calendarItemBase.IsMeeting = false;
			}
		}

		// Token: 0x060005DB RID: 1499 RVA: 0x0001E93C File Offset: 0x0001CB3C
		public override void AppendUpdate(AppendPropertyUpdate appendPropertyUpdate, UpdateCommandSettings updateCommandSettings)
		{
			CalendarItemBase calendarItemBase = (CalendarItemBase)updateCommandSettings.StoreObject;
			ServiceObject serviceObject = appendPropertyUpdate.ServiceObject;
			this.SetProperty(serviceObject, calendarItemBase, true);
		}

		// Token: 0x060005DC RID: 1500 RVA: 0x0001E968 File Offset: 0x0001CB68
		private EwsAttendeeType[] AttendeeCollectionToEwsAttendees(CalendarItemBase calendarItemBase, int maximumAttendeesToAdd)
		{
			List<EwsAttendeeType> list = new List<EwsAttendeeType>();
			int num = 0;
			ParticipantInformationDictionary participantInformation = EWSSettings.ParticipantInformation;
			bool flag = calendarItemBase.IsOrganizer();
			bool flag2 = ExchangeVersion.Current.Supports(ExchangeVersion.Exchange2012);
			if (calendarItemBase.AttendeeCollection == null)
			{
				return null;
			}
			foreach (Attendee attendee in calendarItemBase.AttendeeCollection)
			{
				ParticipantInformation participantInformation2 = null;
				if (attendee.AttendeeType == this.attendeeType && participantInformation.TryGetParticipantFromDictionary(attendee.Participant, out participantInformation2))
				{
					ResponseType responseType = attendee.ResponseType;
					EwsAttendeeType ewsAttendeeType = new EwsAttendeeType();
					ewsAttendeeType.Mailbox = new EmailAddressWrapper();
					ewsAttendeeType.Mailbox.Name = participantInformation2.DisplayName;
					ewsAttendeeType.Mailbox.EmailAddress = participantInformation2.EmailAddress;
					ewsAttendeeType.Mailbox.RoutingType = participantInformation2.RoutingType;
					ewsAttendeeType.Mailbox.SipUri = participantInformation2.SipUri;
					StoreParticipantOrigin storeParticipantOrigin = participantInformation2.Origin as StoreParticipantOrigin;
					if (storeParticipantOrigin != null)
					{
						ConcatenatedIdAndChangeKey concatenatedId = IdConverter.GetConcatenatedId(storeParticipantOrigin.OriginItemId, new MailboxId(calendarItemBase.Session.MailboxGuid), null);
						ewsAttendeeType.Mailbox.ItemId = new ItemId(concatenatedId.Id, concatenatedId.ChangeKey);
					}
					if (flag || !flag2)
					{
						ewsAttendeeType.ResponseTypeString = ResponseTypeConverter.ToString(responseType);
						if (responseType != ResponseType.None && responseType != ResponseType.NotResponded)
						{
							ewsAttendeeType.LastResponseTime = ExDateTimeConverter.ToSoapHeaderTimeZoneRelatedXsdDateTime(attendee.ReplyTime);
						}
					}
					if (flag && attendee.HasTimeProposal && ExchangeVersion.Current.Supports(ExchangeVersion.Exchange2013_SP1))
					{
						ewsAttendeeType.ProposedStart = ExDateTimeConverter.ToSoapHeaderTimeZoneRelatedXsdDateTime(attendee.ProposedStart);
						ewsAttendeeType.ProposedEnd = ExDateTimeConverter.ToSoapHeaderTimeZoneRelatedXsdDateTime(attendee.ProposedEnd);
					}
					this.SetMailboxTypeForExchange2012(participantInformation2, ewsAttendeeType);
					list.Add(ewsAttendeeType);
					num++;
					if (maximumAttendeesToAdd > 0 && num >= maximumAttendeesToAdd)
					{
						break;
					}
				}
			}
			if (list.Count > 0)
			{
				return list.ToArray();
			}
			return null;
		}

		// Token: 0x060005DD RID: 1501 RVA: 0x0001EB7C File Offset: 0x0001CD7C
		private void SetMailboxTypeForExchange2012(ParticipantInformation participantInformation, EwsAttendeeType attendeeEntry)
		{
			if (ExchangeVersion.Current.Supports(ExchangeVersion.Exchange2012))
			{
				attendeeEntry.Mailbox.MailboxType = participantInformation.MailboxType.ToString();
			}
		}

		// Token: 0x060005DE RID: 1502 RVA: 0x0001EBAC File Offset: 0x0001CDAC
		private void SetProperty(ServiceObject serviceObject, CalendarItemBase calendarItemBase, bool appendAttendees)
		{
			EwsAttendeeType[] valueOrDefault = serviceObject.GetValueOrDefault<EwsAttendeeType[]>(this.commandContext.PropertyInformation);
			if (!appendAttendees)
			{
				this.ClearAttendeeTypeFromCollection(calendarItemBase.AttendeeCollection);
			}
			if (valueOrDefault != null)
			{
				foreach (EwsAttendeeType ewsAttendeeType in valueOrDefault)
				{
					if (ewsAttendeeType.Mailbox != null)
					{
						calendarItemBase.AttendeeCollection.Add(base.GetParticipantOrDLFromAddress(ewsAttendeeType.Mailbox, calendarItemBase), this.attendeeType, null, null, false);
					}
				}
			}
		}

		// Token: 0x060005DF RID: 1503 RVA: 0x0001EC30 File Offset: 0x0001CE30
		private void ClearAttendeeTypeFromCollection(IAttendeeCollection attendees)
		{
			for (int i = attendees.Count - 1; i >= 0; i--)
			{
				if (attendees[i].AttendeeType == this.attendeeType)
				{
					attendees.Remove(attendees[i]);
				}
			}
		}

		// Token: 0x0400069C RID: 1692
		private AttendeeType attendeeType;
	}
}
