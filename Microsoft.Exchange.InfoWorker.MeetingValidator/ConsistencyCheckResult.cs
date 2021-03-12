using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Infoworker.MeetingValidator
{
	// Token: 0x0200002B RID: 43
	[ClassAccessLevel(AccessLevel.MSInternal)]
	public class ConsistencyCheckResult : IXmlSerializable, IEnumerable<Inconsistency>, IEnumerable
	{
		// Token: 0x06000198 RID: 408 RVA: 0x0000C02C File Offset: 0x0000A22C
		private ConsistencyCheckResult() : this(ConsistencyCheckType.None, null)
		{
		}

		// Token: 0x06000199 RID: 409 RVA: 0x0000C036 File Offset: 0x0000A236
		protected ConsistencyCheckResult(ConsistencyCheckType checkType, string checkDescription)
		{
			this.Initialize(checkType, checkDescription, CheckStatusType.Passed, new List<Inconsistency>(0));
		}

		// Token: 0x0600019A RID: 410 RVA: 0x0000C04D File Offset: 0x0000A24D
		internal static ConsistencyCheckResult CreateInstance(ConsistencyCheckType checkType, string checkDescription)
		{
			return new ConsistencyCheckResult(checkType, checkDescription);
		}

		// Token: 0x0600019B RID: 411 RVA: 0x0000C058 File Offset: 0x0000A258
		private void Initialize(ConsistencyCheckType checkType, string checkDescription, CheckStatusType status, List<Inconsistency> inconsistencies)
		{
			this.CheckDescription = checkDescription;
			this.CheckType = checkType;
			this.status = status;
			this.RepairInfo = RepairSteps.CreateInstance();
			this.Inconsistencies = inconsistencies;
			this.Severity = SeverityType.Information;
			this.timestamp = DateTime.UtcNow;
			this.comparedRecurrenceBlobs = false;
			this.recurrenceBlobComparison = false;
			this.meetingOverlap = int.MinValue;
			this.responseStatus = int.MinValue;
			this.replyTime = DateTime.MinValue;
			this.fbStatus = int.MinValue;
			this.ShouldBeReported = false;
		}

		// Token: 0x1700005B RID: 91
		// (get) Token: 0x0600019C RID: 412 RVA: 0x0000C0E0 File Offset: 0x0000A2E0
		// (set) Token: 0x0600019D RID: 413 RVA: 0x0000C0E8 File Offset: 0x0000A2E8
		internal string CheckDescription { get; private set; }

		// Token: 0x1700005C RID: 92
		// (get) Token: 0x0600019E RID: 414 RVA: 0x0000C0F1 File Offset: 0x0000A2F1
		// (set) Token: 0x0600019F RID: 415 RVA: 0x0000C0F9 File Offset: 0x0000A2F9
		internal ConsistencyCheckType CheckType { get; private set; }

		// Token: 0x1700005D RID: 93
		// (get) Token: 0x060001A0 RID: 416 RVA: 0x0000C102 File Offset: 0x0000A302
		// (set) Token: 0x060001A1 RID: 417 RVA: 0x0000C10A File Offset: 0x0000A30A
		internal SeverityType Severity { get; set; }

		// Token: 0x1700005E RID: 94
		// (get) Token: 0x060001A2 RID: 418 RVA: 0x0000C113 File Offset: 0x0000A313
		// (set) Token: 0x060001A3 RID: 419 RVA: 0x0000C11B File Offset: 0x0000A31B
		internal bool ShouldBeReported { get; set; }

		// Token: 0x1700005F RID: 95
		// (get) Token: 0x060001A4 RID: 420 RVA: 0x0000C124 File Offset: 0x0000A324
		// (set) Token: 0x060001A5 RID: 421 RVA: 0x0000C12C File Offset: 0x0000A32C
		internal CheckStatusType Status
		{
			get
			{
				return this.status;
			}
			set
			{
				this.status = value;
			}
		}

		// Token: 0x17000060 RID: 96
		// (get) Token: 0x060001A6 RID: 422 RVA: 0x0000C135 File Offset: 0x0000A335
		// (set) Token: 0x060001A7 RID: 423 RVA: 0x0000C13D File Offset: 0x0000A33D
		internal RepairSteps RepairInfo { get; private set; }

		// Token: 0x17000061 RID: 97
		// (get) Token: 0x060001A8 RID: 424 RVA: 0x0000C146 File Offset: 0x0000A346
		public int InconsistencyCount
		{
			get
			{
				return this.Inconsistencies.Count;
			}
		}

		// Token: 0x17000062 RID: 98
		// (get) Token: 0x060001A9 RID: 425 RVA: 0x0000C153 File Offset: 0x0000A353
		// (set) Token: 0x060001AA RID: 426 RVA: 0x0000C15B File Offset: 0x0000A35B
		internal string ErrorString
		{
			get
			{
				return this.errorString;
			}
			set
			{
				this.errorString = value;
			}
		}

		// Token: 0x17000063 RID: 99
		// (get) Token: 0x060001AB RID: 427 RVA: 0x0000C164 File Offset: 0x0000A364
		internal DateTime Timestamp
		{
			get
			{
				return this.timestamp;
			}
		}

		// Token: 0x17000064 RID: 100
		// (get) Token: 0x060001AC RID: 428 RVA: 0x0000C16C File Offset: 0x0000A36C
		// (set) Token: 0x060001AD RID: 429 RVA: 0x0000C174 File Offset: 0x0000A374
		internal RecurrenceInfo OrganizerRecurrence
		{
			get
			{
				return this.organizerRecurrence;
			}
			set
			{
				this.organizerRecurrence = value;
			}
		}

		// Token: 0x17000065 RID: 101
		// (get) Token: 0x060001AE RID: 430 RVA: 0x0000C17D File Offset: 0x0000A37D
		// (set) Token: 0x060001AF RID: 431 RVA: 0x0000C185 File Offset: 0x0000A385
		internal RecurrenceInfo AttendeeRecurrence
		{
			get
			{
				return this.attendeeRecurrence;
			}
			set
			{
				this.attendeeRecurrence = value;
			}
		}

		// Token: 0x17000066 RID: 102
		// (get) Token: 0x060001B0 RID: 432 RVA: 0x0000C18E File Offset: 0x0000A38E
		// (set) Token: 0x060001B1 RID: 433 RVA: 0x0000C196 File Offset: 0x0000A396
		internal bool ComparedRecurrenceBlobs
		{
			get
			{
				return this.comparedRecurrenceBlobs;
			}
			set
			{
				this.comparedRecurrenceBlobs = value;
			}
		}

		// Token: 0x17000067 RID: 103
		// (get) Token: 0x060001B2 RID: 434 RVA: 0x0000C19F File Offset: 0x0000A39F
		// (set) Token: 0x060001B3 RID: 435 RVA: 0x0000C1A7 File Offset: 0x0000A3A7
		internal bool RecurrenceBlobComparison
		{
			get
			{
				return this.recurrenceBlobComparison;
			}
			set
			{
				this.recurrenceBlobComparison = value;
			}
		}

		// Token: 0x17000068 RID: 104
		// (get) Token: 0x060001B4 RID: 436 RVA: 0x0000C1B0 File Offset: 0x0000A3B0
		// (set) Token: 0x060001B5 RID: 437 RVA: 0x0000C1B8 File Offset: 0x0000A3B8
		public int MeetingOverlap
		{
			get
			{
				return this.meetingOverlap;
			}
			set
			{
				this.meetingOverlap = value;
			}
		}

		// Token: 0x17000069 RID: 105
		// (get) Token: 0x060001B6 RID: 438 RVA: 0x0000C1C1 File Offset: 0x0000A3C1
		// (set) Token: 0x060001B7 RID: 439 RVA: 0x0000C1C9 File Offset: 0x0000A3C9
		public int ResponseStatus
		{
			get
			{
				return this.responseStatus;
			}
			set
			{
				this.responseStatus = value;
			}
		}

		// Token: 0x1700006A RID: 106
		// (get) Token: 0x060001B8 RID: 440 RVA: 0x0000C1D2 File Offset: 0x0000A3D2
		// (set) Token: 0x060001B9 RID: 441 RVA: 0x0000C1DA File Offset: 0x0000A3DA
		public DateTime ReplyTime
		{
			get
			{
				return this.replyTime;
			}
			set
			{
				this.replyTime = value;
			}
		}

		// Token: 0x1700006B RID: 107
		// (get) Token: 0x060001BA RID: 442 RVA: 0x0000C1E3 File Offset: 0x0000A3E3
		// (set) Token: 0x060001BB RID: 443 RVA: 0x0000C1EB File Offset: 0x0000A3EB
		public int FreeBusyStatus
		{
			get
			{
				return this.fbStatus;
			}
			set
			{
				this.fbStatus = value;
			}
		}

		// Token: 0x1700006C RID: 108
		// (get) Token: 0x060001BC RID: 444 RVA: 0x0000C1F4 File Offset: 0x0000A3F4
		// (set) Token: 0x060001BD RID: 445 RVA: 0x0000C1FC File Offset: 0x0000A3FC
		public List<Inconsistency> Inconsistencies { get; private set; }

		// Token: 0x060001BE RID: 446 RVA: 0x0000C205 File Offset: 0x0000A405
		public void ForEachInconsistency(Action<Inconsistency> action)
		{
			this.Inconsistencies.ForEach(action);
		}

		// Token: 0x060001BF RID: 447 RVA: 0x0000C214 File Offset: 0x0000A414
		internal void AddInconsistency(CalendarValidationContext context, Inconsistency inconsistency)
		{
			this.Inconsistencies.Add(inconsistency);
			RumInfo rumInfo = RumFactory.Instance.CreateRumInfo(context, inconsistency);
			if (!rumInfo.IsNullOp)
			{
				inconsistency.ShouldFix = true;
				this.RepairInfo.AddStep(rumInfo);
			}
		}

		// Token: 0x060001C0 RID: 448 RVA: 0x0000C255 File Offset: 0x0000A455
		public XmlSchema GetSchema()
		{
			return null;
		}

		// Token: 0x060001C1 RID: 449 RVA: 0x0000C258 File Offset: 0x0000A458
		public void ReadXml(XmlReader reader)
		{
			throw new NotSupportedException("XML deserialization is not supported.");
		}

		// Token: 0x060001C2 RID: 450 RVA: 0x0000C264 File Offset: 0x0000A464
		public void WriteXml(XmlWriter writer)
		{
			writer.WriteElementString("Description", this.CheckDescription);
			writer.WriteElementString("Status", this.status.ToString());
			writer.WriteElementString("Timestamp", this.timestamp.ToString());
			if (this.Status != CheckStatusType.Passed)
			{
				writer.WriteElementString("Severity", this.Severity.ToString());
			}
			if (this.ComparedRecurrenceBlobs)
			{
				writer.WriteElementString("XSORecurrenceBlobComparison", this.RecurrenceBlobComparison.ToString());
			}
			if (this.meetingOverlap != -2147483648)
			{
				writer.WriteElementString("MeetingOverlap", this.meetingOverlap.ToString());
			}
			if (this.responseStatus != -2147483648)
			{
				writer.WriteElementString("ResponseStatus", this.responseStatus.ToString());
			}
			if (this.replyTime != DateTime.MinValue)
			{
				writer.WriteElementString("ReplyTime", this.replyTime.ToString());
			}
			if (this.fbStatus != -2147483648)
			{
				writer.WriteElementString("FBStatus", this.fbStatus.ToString());
			}
			XmlSerializer xmlSerializer;
			if (this.OrganizerRecurrence != null || this.AttendeeRecurrence != null)
			{
				xmlSerializer = new XmlSerializer(typeof(RecurrenceInfo));
				if (this.OrganizerRecurrence != null)
				{
					xmlSerializer.Serialize(writer, this.organizerRecurrence);
				}
				if (this.AttendeeRecurrence != null)
				{
					xmlSerializer.Serialize(writer, this.attendeeRecurrence);
				}
			}
			writer.WriteStartElement("Inconsistencies");
			xmlSerializer = new XmlSerializer(typeof(Inconsistency));
			foreach (Inconsistency o in this.Inconsistencies)
			{
				xmlSerializer.Serialize(writer, o);
			}
			writer.WriteEndElement();
		}

		// Token: 0x060001C3 RID: 451 RVA: 0x0000C448 File Offset: 0x0000A648
		public IEnumerator<Inconsistency> GetEnumerator()
		{
			return this.Inconsistencies.GetEnumerator();
		}

		// Token: 0x060001C4 RID: 452 RVA: 0x0000C45A File Offset: 0x0000A65A
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x040000F6 RID: 246
		private CheckStatusType status;

		// Token: 0x040000F7 RID: 247
		private string errorString;

		// Token: 0x040000F8 RID: 248
		private DateTime timestamp;

		// Token: 0x040000F9 RID: 249
		private RecurrenceInfo organizerRecurrence;

		// Token: 0x040000FA RID: 250
		private RecurrenceInfo attendeeRecurrence;

		// Token: 0x040000FB RID: 251
		private bool comparedRecurrenceBlobs;

		// Token: 0x040000FC RID: 252
		private bool recurrenceBlobComparison;

		// Token: 0x040000FD RID: 253
		private int meetingOverlap;

		// Token: 0x040000FE RID: 254
		private int responseStatus;

		// Token: 0x040000FF RID: 255
		private DateTime replyTime;

		// Token: 0x04000100 RID: 256
		private int fbStatus;
	}
}
