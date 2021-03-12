﻿using System;
using System.Collections;
using System.Collections.Generic;
using Microsoft.Exchange.AirSync.SchemaConverter.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics.Components.SchemaConverter;

namespace Microsoft.Exchange.AirSync.SchemaConverter.XSO
{
	// Token: 0x0200021B RID: 539
	[Serializable]
	internal class XsoExtendedAttendeesProperty : XsoProperty, IExtendedAttendeesProperty, IMultivaluedProperty<ExtendedAttendeeData>, IProperty, IEnumerable<ExtendedAttendeeData>, IEnumerable
	{
		// Token: 0x06001495 RID: 5269 RVA: 0x0007720F File Offset: 0x0007540F
		public XsoExtendedAttendeesProperty() : base(null)
		{
		}

		// Token: 0x17000742 RID: 1858
		// (get) Token: 0x06001496 RID: 5270 RVA: 0x00077218 File Offset: 0x00075418
		public int Count
		{
			get
			{
				return ((CalendarItemBase)base.XsoItem).AttendeeCollection.Count;
			}
		}

		// Token: 0x06001497 RID: 5271 RVA: 0x000775BC File Offset: 0x000757BC
		public IEnumerator<ExtendedAttendeeData> GetEnumerator()
		{
			CalendarItemBase calItem = base.XsoItem as CalendarItemBase;
			if (calItem == null)
			{
				throw new UnexpectedTypeException("CalendarItemBase", base.XsoItem);
			}
			AirSyncDiagnostics.TraceInfo<int, GlobalObjectId>(ExTraceGlobals.XsoTracer, this, "Adding Attendees to meeting request.Count :{0}, GlobalObjectID: {1}", calItem.AttendeeCollection.Count, calItem.GlobalObjectId);
			bool anyAttendeeAdded = false;
			foreach (Attendee attendee in calItem.AttendeeCollection)
			{
				if (!attendee.IsOrganizer)
				{
					bool sendData = calItem.IsOrganizer();
					if (attendee.Participant.EmailAddress != null)
					{
						anyAttendeeAdded = true;
						yield return new ExtendedAttendeeData(EmailAddressConverter.LookupEmailAddressString(attendee.Participant, calItem.Session.MailboxOwner), attendee.Participant.DisplayName, (int)attendee.ResponseType, (int)attendee.AttendeeType, sendData);
					}
					else
					{
						AirSyncDiagnostics.TraceDebug<string, string>(ExTraceGlobals.XsoTracer, this, "Attendee '{0}' skipped because there is no email address. Meeting Subject:'{1}'.", (attendee.Participant.DisplayName == null) ? "<Null>" : attendee.Participant.DisplayName, calItem.Subject);
					}
				}
			}
			if (!anyAttendeeAdded && !calItem.IsOrganizer() && calItem.IsMeeting && calItem.AttendeeCollection.Count > 0)
			{
				AirSyncDiagnostics.TraceDebug<int, GlobalObjectId, string>(ExTraceGlobals.XsoTracer, this, "No Attendees were added for this meeting, Adding current user as default attendee. Actual Attendees Count: {0}, GlobalObjectId:{1}, Subject:{2}", calItem.AttendeeCollection.Count, calItem.GlobalObjectId, calItem.Subject);
				Command.CurrentCommand.ProtocolLogger.SetValue(ProtocolLoggerData.Error, "ExplictlyAddingUserToMeeting");
				MailboxSession session = (MailboxSession)calItem.Session;
				yield return new ExtendedAttendeeData(session.MailboxOwner.MailboxInfo.PrimarySmtpAddress.ToString(), session.MailboxOwner.MailboxInfo.DisplayName, 0, 1, false);
			}
			yield break;
		}

		// Token: 0x06001498 RID: 5272 RVA: 0x000775D8 File Offset: 0x000757D8
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x06001499 RID: 5273 RVA: 0x000775E0 File Offset: 0x000757E0
		protected override void InternalCopyFromModified(IProperty srcProperty)
		{
			CalendarItemBase calendarItemBase = base.XsoItem as CalendarItemBase;
			if (calendarItemBase == null)
			{
				throw new UnexpectedTypeException("CalendarItemBase", base.XsoItem);
			}
			IExtendedAttendeesProperty extendedAttendeesProperty = srcProperty as IExtendedAttendeesProperty;
			if (extendedAttendeesProperty == null)
			{
				throw new UnexpectedTypeException("IExtendedAttendeesProperty", srcProperty);
			}
			if (calendarItemBase.IsOrganizer())
			{
				ADObjectId adobjectId = null;
				calendarItemBase.AttendeeCollection.Clear();
				foreach (ExtendedAttendeeData extendedAttendeeData in extendedAttendeesProperty)
				{
					Participant participant = new Participant(extendedAttendeeData.DisplayName, extendedAttendeeData.EmailAddress, "SMTP");
					if (adobjectId == null && calendarItemBase.Session.MailboxOwner.MailboxInfo.Configuration.AddressBookPolicy != null)
					{
						adobjectId = DirectoryHelper.GetGlobalAddressListFromAddressBookPolicy(calendarItemBase.Session.MailboxOwner.MailboxInfo.Configuration.AddressBookPolicy, calendarItemBase.Session.GetADConfigurationSession(true, ConsistencyMode.IgnoreInvalid));
					}
					Participant[] array = Participant.TryConvertTo(new Participant[]
					{
						participant
					}, "EX", calendarItemBase.Session.MailboxOwner, adobjectId);
					Participant participant2 = (array != null && array.Length > 0) ? array[0] : null;
					if (participant2 == null)
					{
						AirSyncDiagnostics.TraceDebug<string>(ExTraceGlobals.XsoTracer, this, "participant Conversion from SMTP to Ex failed. {0}", extendedAttendeeData.EmailAddress);
					}
					if (extendedAttendeeData.SendExtendedData)
					{
						calendarItemBase.AttendeeCollection.Add((participant2 != null) ? participant2 : participant, (AttendeeType)extendedAttendeeData.Type, new ResponseType?((ResponseType)extendedAttendeeData.Status), null, false);
					}
					else
					{
						calendarItemBase.AttendeeCollection.Add((participant2 != null) ? participant2 : participant, (AttendeeType)extendedAttendeeData.Type, null, null, false);
					}
				}
				if (calendarItemBase.AttendeeCollection.Count > 0)
				{
					calendarItemBase.IsMeeting = true;
				}
				calendarItemBase.UnsafeSetMeetingRequestWasSent(true);
				return;
			}
			AirSyncDiagnostics.TraceDebug<int>(ExTraceGlobals.XsoTracer, this, "XsoExtendedAttendeesProperty::InternalCopyFromModified.Skip copying attendees as current user is not organizer.Existing Attendee Count:{0}", calendarItemBase.AttendeeCollection.Count);
		}

		// Token: 0x0600149A RID: 5274 RVA: 0x00077800 File Offset: 0x00075A00
		protected override void InternalSetToDefault(IProperty srcProperty)
		{
			CalendarItemBase calendarItemBase = base.XsoItem as CalendarItemBase;
			if (calendarItemBase == null)
			{
				throw new UnexpectedTypeException("CalendarItemBase", base.XsoItem);
			}
			if (calendarItemBase.IsOrganizer())
			{
				calendarItemBase.AttendeeCollection.Clear();
				return;
			}
			AirSyncDiagnostics.TraceDebug<int>(ExTraceGlobals.XsoTracer, this, "XsoExtendedAttendeesProperty::InternalSetToDefault.Skip copying attendees as current user is not organizer.Existing Attendee Count:{0}", calendarItemBase.AttendeeCollection.Count);
		}
	}
}
