using System;
using System.Collections.Generic;
using System.Xml;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.InfoWorker.Common;
using Microsoft.Exchange.Infoworker.MeetingValidator;

namespace Microsoft.Exchange.MailboxAssistants.Assistants.CalendarRepair
{
	// Token: 0x02000153 RID: 339
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal sealed class CalendarRepairLogMeetingEntry : CalendarRepairLogEntryBase
	{
		// Token: 0x06000DD4 RID: 3540 RVA: 0x00053BEA File Offset: 0x00051DEA
		private CalendarRepairLogMeetingEntry()
		{
		}

		// Token: 0x17000384 RID: 900
		// (get) Token: 0x06000DD5 RID: 3541 RVA: 0x00053BF2 File Offset: 0x00051DF2
		// (set) Token: 0x06000DD6 RID: 3542 RVA: 0x00053BFA File Offset: 0x00051DFA
		internal bool HasFixableInconsistency { get; private set; }

		// Token: 0x06000DD7 RID: 3543 RVA: 0x00053C04 File Offset: 0x00051E04
		internal static CalendarRepairLogMeetingEntry CreateInstance(MeetingData meeting, bool subjectLoggingEnabled)
		{
			return new CalendarRepairLogMeetingEntry
			{
				HasFixableInconsistency = false,
				meeting = meeting,
				attendeeEntries = new List<CalendarRepairLogAttendeeEntry>(),
				duplicates = new List<CalendarRepairDuplicateMeetingLogEntry>(),
				subjectLoggingEnabled = subjectLoggingEnabled,
				hasDuplicates = false
			};
		}

		// Token: 0x06000DD8 RID: 3544 RVA: 0x00053C4C File Offset: 0x00051E4C
		internal void AddComparisonResult(MeetingComparisonResult result)
		{
			if (result != null && !string.IsNullOrEmpty(result.AttendeePrimarySmtpAddress) && result.CheckResultCount != 0)
			{
				CalendarRepairLogAttendeeEntry calendarRepairLogAttendeeEntry = CalendarRepairLogAttendeeEntry.CreateInstance(result);
				if (calendarRepairLogAttendeeEntry.HasFixableInconsistency)
				{
					this.HasFixableInconsistency = true;
				}
				this.attendeeEntries.Add(calendarRepairLogAttendeeEntry);
			}
		}

		// Token: 0x06000DD9 RID: 3545 RVA: 0x00053C94 File Offset: 0x00051E94
		internal void AddMeetingDuplicateResult(List<MeetingValidationResult> duplicates, string error)
		{
			if (duplicates.Count > 0)
			{
				this.hasDuplicates = true;
				foreach (MeetingValidationResult meetingValidationResult in duplicates)
				{
					this.duplicates.Add(CalendarRepairDuplicateMeetingLogEntry.CreateInstance(meetingValidationResult));
				}
			}
		}

		// Token: 0x17000385 RID: 901
		// (get) Token: 0x06000DDA RID: 3546 RVA: 0x00053CFC File Offset: 0x00051EFC
		protected override bool ShouldLog
		{
			get
			{
				return this.hasDuplicates || (base.ShouldLog && this.HasFixableInconsistency);
			}
		}

		// Token: 0x06000DDB RID: 3547 RVA: 0x00053D18 File Offset: 0x00051F18
		public override void WriteXml(XmlWriter writer)
		{
			if (this.meeting == null)
			{
				throw new InvalidOperationException("A 'Meeting' entry can only be logged if the meeting is properly initialized and repaired (Invalid Data: meeting).");
			}
			writer.WriteStartElement("Meeting");
			if (this.subjectLoggingEnabled)
			{
				writer.WriteAttributeString("Subject", this.meeting.Subject);
			}
			writer.WriteAttributeString("MeetingType", this.meeting.CalendarItemType.ToString());
			writer.WriteAttributeString("StartTime", CalendarRepairLogEntryBase.GetDateTimeString(this.meeting.StartTime));
			writer.WriteAttributeString("EndTime", CalendarRepairLogEntryBase.GetDateTimeString(this.meeting.EndTime));
			writer.WriteAttributeString("Organizer", this.meeting.OrganizerPrimarySmtpAddress);
			CalendarRepairLogEntryBase.WriteXmlElementStringIfNotNullOrEmpty(writer, "InternetMessageId", this.meeting.InternetMessageId);
			if (this.meeting.GlobalObjectId != null)
			{
				CalendarRepairLogEntryBase.WriteXmlElementStringIfNotNullOrEmpty(writer, "GlobalObjectId", this.meeting.GlobalObjectId.ToString());
			}
			if (this.meeting.CleanGlobalObjectId != null)
			{
				CalendarRepairLogEntryBase.WriteXmlElementStringIfNotNullOrEmpty(writer, "CleanGlobalObjectId", this.meeting.CleanGlobalObjectId);
			}
			CalendarRepairLogEntryBase.WriteXmlElementStringIfNotNullOrEmpty(writer, "OwnerAppointmentId", (this.meeting.OwnerAppointmentId != null) ? this.meeting.OwnerAppointmentId.Value.ToString() : string.Empty);
			if (this.hasDuplicates)
			{
				base.WriteChildNodes<CalendarRepairDuplicateMeetingLogEntry>("DuplicatesRemoved", this.duplicates, writer, false, new Pair<string, string>[0]);
			}
			base.WriteChildNodes<CalendarRepairLogAttendeeEntry>("Attendees", this.attendeeEntries, writer, false, new Pair<string, string>[0]);
			writer.WriteEndElement();
		}

		// Token: 0x040008B9 RID: 2233
		private const string ElementName = "Meeting";

		// Token: 0x040008BA RID: 2234
		private const string SubjectAttributeName = "Subject";

		// Token: 0x040008BB RID: 2235
		private const string MeetingTypeAttributeName = "MeetingType";

		// Token: 0x040008BC RID: 2236
		private const string StartTimeAttributeName = "StartTime";

		// Token: 0x040008BD RID: 2237
		private const string EndTimeAttributeName = "EndTime";

		// Token: 0x040008BE RID: 2238
		private const string OrganizerAttributeName = "Organizer";

		// Token: 0x040008BF RID: 2239
		private const string InternetMessageIdNodeName = "InternetMessageId";

		// Token: 0x040008C0 RID: 2240
		private const string GlobalObjectIdNodeName = "GlobalObjectId";

		// Token: 0x040008C1 RID: 2241
		private const string CleanGlobalObjectIdNodeName = "CleanGlobalObjectId";

		// Token: 0x040008C2 RID: 2242
		private const string OwnerAppointmentIdNodeName = "OwnerAppointmentId";

		// Token: 0x040008C3 RID: 2243
		private const string AttendeeCollectionNodeName = "Attendees";

		// Token: 0x040008C4 RID: 2244
		private const string DuplicateRemovedNodeName = "DuplicatesRemoved";

		// Token: 0x040008C5 RID: 2245
		private const string DuplicateErrorNodeName = "DuplicateError";

		// Token: 0x040008C6 RID: 2246
		private MeetingData meeting;

		// Token: 0x040008C7 RID: 2247
		private IList<CalendarRepairLogAttendeeEntry> attendeeEntries;

		// Token: 0x040008C8 RID: 2248
		private bool subjectLoggingEnabled;

		// Token: 0x040008C9 RID: 2249
		private bool hasDuplicates;

		// Token: 0x040008CA RID: 2250
		private IList<CalendarRepairDuplicateMeetingLogEntry> duplicates;
	}
}
