using System;
using System.Collections.Generic;
using System.Xml;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage.Principal;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.InfoWorker.Common;
using Microsoft.Exchange.Infoworker.MeetingValidator;

namespace Microsoft.Exchange.MailboxAssistants.Assistants.CalendarRepair
{
	// Token: 0x02000154 RID: 340
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal sealed class CalendarRepairLogRootEntry : CalendarRepairLogEntryBase
	{
		// Token: 0x06000DDC RID: 3548 RVA: 0x00053EB0 File Offset: 0x000520B0
		private CalendarRepairLogRootEntry()
		{
		}

		// Token: 0x17000386 RID: 902
		// (get) Token: 0x06000DDD RID: 3549 RVA: 0x00053EB8 File Offset: 0x000520B8
		public int TotalInconsistentMeetings
		{
			get
			{
				if (this.inconsistentMeetingsComputed)
				{
					return this.totalInconsistentMeetings;
				}
				if (this.meetingEntries.Values != null)
				{
					foreach (CalendarRepairLogMeetingEntry calendarRepairLogMeetingEntry in this.meetingEntries.Values)
					{
						if (calendarRepairLogMeetingEntry.HasFixableInconsistency)
						{
							this.totalInconsistentMeetings++;
						}
					}
				}
				this.inconsistentMeetingsComputed = true;
				return this.totalInconsistentMeetings;
			}
		}

		// Token: 0x06000DDE RID: 3550 RVA: 0x00053F44 File Offset: 0x00052144
		internal static CalendarRepairLogRootEntry CreateInstance(IExchangePrincipal mailboxOwner, ExDateTime rangeStart, ExDateTime rangeEnd, bool subjectLoggingEnabled)
		{
			return new CalendarRepairLogRootEntry
			{
				mailboxOwner = mailboxOwner,
				rangeStart = rangeStart,
				rangeEnd = rangeEnd,
				subjectLoggingEnabled = subjectLoggingEnabled,
				meetingEntries = new Dictionary<string, CalendarRepairLogMeetingEntry>()
			};
		}

		// Token: 0x06000DDF RID: 3551 RVA: 0x00053F80 File Offset: 0x00052180
		internal void AddValidationResult(MeetingValidationResult result)
		{
			this.totalLocalMeetings++;
			if (!result.IsConsistent)
			{
				using (Dictionary<string, MeetingComparisonResult>.KeyCollection.Enumerator enumerator = result.ResultsPerAttendee.Keys.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						string key = enumerator.Current;
						CalendarRepairLogMeetingEntry calendarRepairLogMeetingEntry = null;
						MeetingComparisonResult meetingComparisonResult = result.ResultsPerAttendee[key];
						string key2 = meetingComparisonResult.Meeting.GlobalObjectId.ToString();
						if (!this.meetingEntries.TryGetValue(key2, out calendarRepairLogMeetingEntry))
						{
							calendarRepairLogMeetingEntry = CalendarRepairLogMeetingEntry.CreateInstance(meetingComparisonResult.Meeting, this.subjectLoggingEnabled);
							this.meetingEntries.Add(key2, calendarRepairLogMeetingEntry);
						}
						calendarRepairLogMeetingEntry.AddComparisonResult(meetingComparisonResult);
					}
					return;
				}
			}
			if (result.DuplicateResults != null && result.DuplicateResults.Count > 0)
			{
				string key3 = result.MeetingData.GlobalObjectId.ToString();
				CalendarRepairLogMeetingEntry calendarRepairLogMeetingEntry2 = null;
				if (!this.meetingEntries.TryGetValue(key3, out calendarRepairLogMeetingEntry2))
				{
					calendarRepairLogMeetingEntry2 = CalendarRepairLogMeetingEntry.CreateInstance(result.MeetingData, this.subjectLoggingEnabled);
					this.meetingEntries.Add(key3, calendarRepairLogMeetingEntry2);
				}
				calendarRepairLogMeetingEntry2.AddMeetingDuplicateResult(result.DuplicateResults, result.ErrorDescription);
			}
		}

		// Token: 0x06000DE0 RID: 3552 RVA: 0x000540B4 File Offset: 0x000522B4
		public override void WriteXml(XmlWriter writer)
		{
			ServerVersion serverVersion = new ServerVersion(this.mailboxOwner.MailboxInfo.Location.ServerVersion);
			writer.WriteStartDocument();
			writer.WriteStartElement("CalendarRepairLog");
			writer.WriteAttributeString("SchemaVersion", "3.01");
			writer.WriteAttributeString("MailboxUser", this.mailboxOwner.Alias);
			writer.WriteAttributeString("ServerVersion", serverVersion.ToString());
			writer.WriteAttributeString("RangeStart", CalendarRepairLogEntryBase.GetDateTimeString(this.rangeStart));
			writer.WriteAttributeString("RangeEnd", CalendarRepairLogEntryBase.GetDateTimeString(this.rangeEnd));
			writer.WriteAttributeString("TotalLocalMeetings", this.totalLocalMeetings.ToString());
			writer.WriteAttributeString("TotalInconsistentMeetings", this.TotalInconsistentMeetings.ToString());
			base.WriteChildNodes<CalendarRepairLogMeetingEntry>("Meetings", this.meetingEntries.Values, writer, false, new Pair<string, string>[0]);
			writer.WriteEndElement();
			writer.WriteEndDocument();
		}

		// Token: 0x040008CC RID: 2252
		private const string LogSchemaVersion = "3.01";

		// Token: 0x040008CD RID: 2253
		private const string ElementName = "CalendarRepairLog";

		// Token: 0x040008CE RID: 2254
		private const string MailboxUserAttributeName = "MailboxUser";

		// Token: 0x040008CF RID: 2255
		private const string RangeStartAttributeName = "RangeStart";

		// Token: 0x040008D0 RID: 2256
		private const string RangeEndAttributeName = "RangeEnd";

		// Token: 0x040008D1 RID: 2257
		private const string SchemaVersionAttributeName = "SchemaVersion";

		// Token: 0x040008D2 RID: 2258
		private const string ServerVersionAttributeName = "ServerVersion";

		// Token: 0x040008D3 RID: 2259
		private const string TotalLocalMeetingsAttributeName = "TotalLocalMeetings";

		// Token: 0x040008D4 RID: 2260
		private const string TotalInconsistentMeetingsAttributeName = "TotalInconsistentMeetings";

		// Token: 0x040008D5 RID: 2261
		private const string MeetingCollectionNodeName = "Meetings";

		// Token: 0x040008D6 RID: 2262
		private IExchangePrincipal mailboxOwner;

		// Token: 0x040008D7 RID: 2263
		private ExDateTime rangeStart;

		// Token: 0x040008D8 RID: 2264
		private ExDateTime rangeEnd;

		// Token: 0x040008D9 RID: 2265
		private bool subjectLoggingEnabled;

		// Token: 0x040008DA RID: 2266
		private int totalLocalMeetings;

		// Token: 0x040008DB RID: 2267
		private int totalInconsistentMeetings;

		// Token: 0x040008DC RID: 2268
		private bool inconsistentMeetingsComputed;

		// Token: 0x040008DD RID: 2269
		private IDictionary<string, CalendarRepairLogMeetingEntry> meetingEntries;
	}
}
