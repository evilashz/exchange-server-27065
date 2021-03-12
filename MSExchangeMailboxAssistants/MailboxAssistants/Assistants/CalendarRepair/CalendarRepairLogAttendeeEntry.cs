using System;
using System.Collections.Generic;
using System.Xml;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.InfoWorker.Common;
using Microsoft.Exchange.Infoworker.MeetingValidator;

namespace Microsoft.Exchange.MailboxAssistants.Assistants.CalendarRepair
{
	// Token: 0x0200014E RID: 334
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal sealed class CalendarRepairLogAttendeeEntry : CalendarRepairLogEntryBase
	{
		// Token: 0x06000DB9 RID: 3513 RVA: 0x0005332B File Offset: 0x0005152B
		private CalendarRepairLogAttendeeEntry()
		{
		}

		// Token: 0x17000380 RID: 896
		// (get) Token: 0x06000DBA RID: 3514 RVA: 0x00053333 File Offset: 0x00051533
		// (set) Token: 0x06000DBB RID: 3515 RVA: 0x0005333B File Offset: 0x0005153B
		internal bool HasFixableInconsistency { get; private set; }

		// Token: 0x06000DBC RID: 3516 RVA: 0x00053344 File Offset: 0x00051544
		internal static CalendarRepairLogAttendeeEntry CreateInstance(MeetingComparisonResult comparisonResult)
		{
			CalendarRepairLogAttendeeEntry calendarRepairLogAttendeeEntry = new CalendarRepairLogAttendeeEntry();
			calendarRepairLogAttendeeEntry.HasFixableInconsistency = false;
			calendarRepairLogAttendeeEntry.attendeePrimarySmtpAddress = comparisonResult.AttendeePrimarySmtpAddress;
			calendarRepairLogAttendeeEntry.consistencyCheckEntries = new List<CalendarRepairLogConsistencyCheckEntry>(comparisonResult.CheckResultCount);
			calendarRepairLogAttendeeEntry.rumEntries = new List<CalendarRepairLogRumEntry>(comparisonResult.RepairInfo.SendableRumsCount);
			comparisonResult.ForEachCheckResult(new Action<ConsistencyCheckResult>(calendarRepairLogAttendeeEntry.AddConsistencyCheckResult));
			comparisonResult.RepairInfo.ForEachSendableRum(new Action<RumInfo>(calendarRepairLogAttendeeEntry.AddRum));
			return calendarRepairLogAttendeeEntry;
		}

		// Token: 0x17000381 RID: 897
		// (get) Token: 0x06000DBD RID: 3517 RVA: 0x000533BB File Offset: 0x000515BB
		protected override bool ShouldLog
		{
			get
			{
				return base.ShouldLog && this.HasFixableInconsistency;
			}
		}

		// Token: 0x06000DBE RID: 3518 RVA: 0x000533D0 File Offset: 0x000515D0
		public override void WriteXml(XmlWriter writer)
		{
			writer.WriteStartElement("Attendee");
			writer.WriteAttributeString("EmailAddress", this.attendeePrimarySmtpAddress);
			base.WriteChildNodes<CalendarRepairLogConsistencyCheckEntry>("ConsistencyChecks", this.consistencyCheckEntries, writer, false, new Pair<string, string>[0]);
			base.WriteChildNodes<CalendarRepairLogRumEntry>("RUMs", this.rumEntries, writer, false, new Pair<string, string>[0]);
			writer.WriteEndElement();
		}

		// Token: 0x06000DBF RID: 3519 RVA: 0x00053434 File Offset: 0x00051634
		private void AddConsistencyCheckResult(ConsistencyCheckResult checkResult)
		{
			CalendarRepairLogConsistencyCheckEntry calendarRepairLogConsistencyCheckEntry = CalendarRepairLogConsistencyCheckEntry.CreateInstance(checkResult);
			if (calendarRepairLogConsistencyCheckEntry.HasFixableInconsistency)
			{
				this.HasFixableInconsistency = true;
			}
			this.consistencyCheckEntries.Add(calendarRepairLogConsistencyCheckEntry);
		}

		// Token: 0x06000DC0 RID: 3520 RVA: 0x00053464 File Offset: 0x00051664
		private void AddRum(RumInfo rum)
		{
			CalendarRepairLogRumEntry item = CalendarRepairLogRumEntry.CreateInstance(rum);
			this.rumEntries.Add(item);
		}

		// Token: 0x0400088D RID: 2189
		private const string ElementName = "Attendee";

		// Token: 0x0400088E RID: 2190
		private const string EmailAttributeName = "EmailAddress";

		// Token: 0x0400088F RID: 2191
		private const string ConsistencyCheckCollectionNodeName = "ConsistencyChecks";

		// Token: 0x04000890 RID: 2192
		private const string RumCollectionNodeName = "RUMs";

		// Token: 0x04000891 RID: 2193
		private string attendeePrimarySmtpAddress;

		// Token: 0x04000892 RID: 2194
		private IList<CalendarRepairLogConsistencyCheckEntry> consistencyCheckEntries;

		// Token: 0x04000893 RID: 2195
		private IList<CalendarRepairLogRumEntry> rumEntries;
	}
}
