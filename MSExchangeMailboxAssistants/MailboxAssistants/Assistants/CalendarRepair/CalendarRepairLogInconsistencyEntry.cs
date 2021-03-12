using System;
using System.Xml;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Infoworker.MeetingValidator;

namespace Microsoft.Exchange.MailboxAssistants.Assistants.CalendarRepair
{
	// Token: 0x02000152 RID: 338
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal sealed class CalendarRepairLogInconsistencyEntry : CalendarRepairLogEntryBase
	{
		// Token: 0x06000DD1 RID: 3537 RVA: 0x00053B32 File Offset: 0x00051D32
		private CalendarRepairLogInconsistencyEntry()
		{
		}

		// Token: 0x06000DD2 RID: 3538 RVA: 0x00053B3C File Offset: 0x00051D3C
		internal static CalendarRepairLogInconsistencyEntry CreateInstance(Inconsistency inconsistency)
		{
			return new CalendarRepairLogInconsistencyEntry
			{
				inconsistency = inconsistency
			};
		}

		// Token: 0x06000DD3 RID: 3539 RVA: 0x00053B58 File Offset: 0x00051D58
		public override void WriteXml(XmlWriter writer)
		{
			writer.WriteStartElement("Inconsistency");
			writer.WriteAttributeString("Owner", this.inconsistency.Owner.ToString());
			writer.WriteAttributeString("ShouldFix", this.inconsistency.ShouldFix.ToString());
			writer.WriteAttributeString("Flag", this.inconsistency.Flag.ToString());
			CalendarRepairLogEntryBase.WriteXmlElementStringIfNotNullOrEmpty(writer, "Description", this.inconsistency.Description);
			writer.WriteEndElement();
		}

		// Token: 0x040008B3 RID: 2227
		private const string ElementName = "Inconsistency";

		// Token: 0x040008B4 RID: 2228
		private const string OwnerAttributeName = "Owner";

		// Token: 0x040008B5 RID: 2229
		private const string FlagAttributeName = "Flag";

		// Token: 0x040008B6 RID: 2230
		private const string ShouldFixAttributeName = "ShouldFix";

		// Token: 0x040008B7 RID: 2231
		private const string DescriptionNodeName = "Description";

		// Token: 0x040008B8 RID: 2232
		private Inconsistency inconsistency;
	}
}
