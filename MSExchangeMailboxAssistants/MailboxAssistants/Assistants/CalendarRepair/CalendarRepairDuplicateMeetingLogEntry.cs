using System;
using System.Xml;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Infoworker.MeetingValidator;

namespace Microsoft.Exchange.MailboxAssistants.Assistants.CalendarRepair
{
	// Token: 0x02000150 RID: 336
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal sealed class CalendarRepairDuplicateMeetingLogEntry : CalendarRepairLogEntryBase
	{
		// Token: 0x06000DC7 RID: 3527 RVA: 0x000535A5 File Offset: 0x000517A5
		private CalendarRepairDuplicateMeetingLogEntry()
		{
		}

		// Token: 0x06000DC8 RID: 3528 RVA: 0x000535B0 File Offset: 0x000517B0
		internal static CalendarRepairDuplicateMeetingLogEntry CreateInstance(MeetingValidationResult meetingValidationResult)
		{
			return new CalendarRepairDuplicateMeetingLogEntry
			{
				meeting = meetingValidationResult.MeetingData
			};
		}

		// Token: 0x17000383 RID: 899
		// (get) Token: 0x06000DC9 RID: 3529 RVA: 0x000535D0 File Offset: 0x000517D0
		protected override bool ShouldLog
		{
			get
			{
				return base.ShouldLog;
			}
		}

		// Token: 0x06000DCA RID: 3530 RVA: 0x000535D8 File Offset: 0x000517D8
		public override void WriteXml(XmlWriter writer)
		{
			writer.WriteStartElement("Meeting");
			writer.WriteAttributeString("Subject", this.meeting.Subject);
			writer.WriteAttributeString("AppointmentSequenceNumber", this.meeting.SequenceNumber.ToString());
			CalendarRepairLogEntryBase.WriteXmlElementStringIfNotNullOrEmpty(writer, "LastModifiedTime", CalendarRepairLogEntryBase.GetDateTimeString(this.meeting.LastModifiedTime));
			CalendarRepairLogEntryBase.WriteXmlElementStringIfNotNullOrEmpty(writer, "OwnerCriticalChangeTime", CalendarRepairLogEntryBase.GetDateTimeString(this.meeting.OwnerCriticalChangeTime));
			writer.WriteEndElement();
		}

		// Token: 0x0400089D RID: 2205
		private const string ElementName = "Meeting";

		// Token: 0x0400089E RID: 2206
		private const string SubjectAttributeName = "Subject";

		// Token: 0x0400089F RID: 2207
		private const string ApptSequenceNumberNodeName = "AppointmentSequenceNumber";

		// Token: 0x040008A0 RID: 2208
		private const string LastModifiedTimeNodeName = "LastModifiedTime";

		// Token: 0x040008A1 RID: 2209
		private const string OwnerCriticalChangeTimeNodeName = "OwnerCriticalChangeTime";

		// Token: 0x040008A2 RID: 2210
		private MeetingData meeting;
	}
}
