using System;
using System.Collections.Generic;
using System.Xml;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.InfoWorker.Common;
using Microsoft.Exchange.Infoworker.MeetingValidator;

namespace Microsoft.Exchange.MailboxAssistants.Assistants.CalendarRepair
{
	// Token: 0x0200014F RID: 335
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal sealed class CalendarRepairLogConsistencyCheckEntry : CalendarRepairLogEntryBase
	{
		// Token: 0x06000DC1 RID: 3521 RVA: 0x00053484 File Offset: 0x00051684
		private CalendarRepairLogConsistencyCheckEntry()
		{
		}

		// Token: 0x17000382 RID: 898
		// (get) Token: 0x06000DC2 RID: 3522 RVA: 0x0005348C File Offset: 0x0005168C
		// (set) Token: 0x06000DC3 RID: 3523 RVA: 0x00053494 File Offset: 0x00051694
		internal bool HasFixableInconsistency { get; private set; }

		// Token: 0x06000DC4 RID: 3524 RVA: 0x000534A0 File Offset: 0x000516A0
		internal static CalendarRepairLogConsistencyCheckEntry CreateInstance(ConsistencyCheckResult checkResult)
		{
			CalendarRepairLogConsistencyCheckEntry calendarRepairLogConsistencyCheckEntry = new CalendarRepairLogConsistencyCheckEntry();
			calendarRepairLogConsistencyCheckEntry.HasFixableInconsistency = false;
			calendarRepairLogConsistencyCheckEntry.checkResult = checkResult;
			calendarRepairLogConsistencyCheckEntry.inconsistencyEntries = new List<CalendarRepairLogInconsistencyEntry>(checkResult.InconsistencyCount);
			checkResult.ForEachInconsistency(new Action<Inconsistency>(calendarRepairLogConsistencyCheckEntry.AddInconsistency));
			return calendarRepairLogConsistencyCheckEntry;
		}

		// Token: 0x06000DC5 RID: 3525 RVA: 0x000534E8 File Offset: 0x000516E8
		private void AddInconsistency(Inconsistency inconsistency)
		{
			if (inconsistency.ShouldFix)
			{
				this.HasFixableInconsistency = true;
			}
			CalendarRepairLogInconsistencyEntry item = CalendarRepairLogInconsistencyEntry.CreateInstance(inconsistency);
			this.inconsistencyEntries.Add(item);
		}

		// Token: 0x06000DC6 RID: 3526 RVA: 0x00053518 File Offset: 0x00051718
		public override void WriteXml(XmlWriter writer)
		{
			writer.WriteStartElement("ConsistencyCheck");
			writer.WriteAttributeString("Type", this.checkResult.CheckType.ToString());
			writer.WriteAttributeString("Result", this.checkResult.Status.ToString());
			CalendarRepairLogEntryBase.WriteXmlElementStringIfNotNullOrEmpty(writer, "Description", this.checkResult.CheckDescription);
			base.WriteChildNodes<CalendarRepairLogInconsistencyEntry>("Inconsistencies", this.inconsistencyEntries, writer, false, new Pair<string, string>[0]);
			writer.WriteEndElement();
		}

		// Token: 0x04000895 RID: 2197
		private const string ElementName = "ConsistencyCheck";

		// Token: 0x04000896 RID: 2198
		private const string TypeAttributeName = "Type";

		// Token: 0x04000897 RID: 2199
		private const string ResultAttributeName = "Result";

		// Token: 0x04000898 RID: 2200
		private const string DescriptionNodeName = "Description";

		// Token: 0x04000899 RID: 2201
		private const string InconsistencyCollectionNodeName = "Inconsistencies";

		// Token: 0x0400089A RID: 2202
		private ConsistencyCheckResult checkResult;

		// Token: 0x0400089B RID: 2203
		private IList<CalendarRepairLogInconsistencyEntry> inconsistencyEntries;
	}
}
