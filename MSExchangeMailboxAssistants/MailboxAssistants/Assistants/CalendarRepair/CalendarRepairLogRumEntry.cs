using System;
using System.Xml;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.MailboxAssistants.Assistants.CalendarRepair
{
	// Token: 0x02000155 RID: 341
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal sealed class CalendarRepairLogRumEntry : CalendarRepairLogEntryBase
	{
		// Token: 0x06000DE1 RID: 3553 RVA: 0x000541A9 File Offset: 0x000523A9
		private CalendarRepairLogRumEntry()
		{
		}

		// Token: 0x06000DE2 RID: 3554 RVA: 0x000541B4 File Offset: 0x000523B4
		internal static CalendarRepairLogRumEntry CreateInstance(RumInfo repairInfo)
		{
			return new CalendarRepairLogRumEntry
			{
				repairInfo = repairInfo
			};
		}

		// Token: 0x06000DE3 RID: 3555 RVA: 0x000541D0 File Offset: 0x000523D0
		public override void WriteXml(XmlWriter writer)
		{
			writer.WriteStartElement("RUM");
			writer.WriteAttributeString("IsOccurrence", this.repairInfo.IsOccurrenceRum.ToString());
			if (this.repairInfo.IsOccurrenceRum)
			{
				writer.WriteAttributeString("OriginalStartTime", CalendarRepairLogEntryBase.GetDateTimeString(this.repairInfo.OccurrenceOriginalStartTime));
			}
			writer.WriteAttributeString("Type", this.repairInfo.Type.ToString());
			if (this.repairInfo is AttendeeInquiryRumInfo)
			{
				writer.WriteAttributeString("PredictedRepairAction", ((AttendeeInquiryRumInfo)this.repairInfo).PredictedRepairAction.ToString());
			}
			writer.WriteAttributeString("Sent", this.repairInfo.IsSuccessfullySent.ToString());
			if (this.repairInfo.IsSuccessfullySent)
			{
				writer.WriteAttributeString("Time", CalendarRepairLogEntryBase.GetDateTimeString(this.repairInfo.SendTime));
			}
			if (this.repairInfo is UpdateRumInfo)
			{
				UpdateRumInfo updateRumInfo = (UpdateRumInfo)this.repairInfo;
				writer.WriteStartElement("Flags");
				foreach (CalendarInconsistencyFlag calendarInconsistencyFlag in updateRumInfo.InconsistencyFlagList)
				{
					writer.WriteElementString("Flag", calendarInconsistencyFlag.ToString());
				}
				writer.WriteEndElement();
			}
			writer.WriteEndElement();
		}

		// Token: 0x040008DE RID: 2270
		private const string ElementName = "RUM";

		// Token: 0x040008DF RID: 2271
		private const string TypeAttributeName = "Type";

		// Token: 0x040008E0 RID: 2272
		private const string PredictedRepairActionAttributeName = "PredictedRepairAction";

		// Token: 0x040008E1 RID: 2273
		private const string IsOccurrenceAttributeName = "IsOccurrence";

		// Token: 0x040008E2 RID: 2274
		private const string OriginalStartTimeAttributeName = "OriginalStartTime";

		// Token: 0x040008E3 RID: 2275
		private const string SentAttributeName = "Sent";

		// Token: 0x040008E4 RID: 2276
		private const string TimeAttributeName = "Time";

		// Token: 0x040008E5 RID: 2277
		private const string FlagNodeName = "Flag";

		// Token: 0x040008E6 RID: 2278
		private const string FlagCollectionNodeName = "Flags";

		// Token: 0x040008E7 RID: 2279
		private RumInfo repairInfo;
	}
}
