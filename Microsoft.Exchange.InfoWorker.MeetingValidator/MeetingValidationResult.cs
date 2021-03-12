using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.Infoworker.MeetingValidator
{
	// Token: 0x0200003E RID: 62
	public class MeetingValidationResult : IXmlSerializable
	{
		// Token: 0x0600022C RID: 556 RVA: 0x0000D1D3 File Offset: 0x0000B3D3
		private MeetingValidationResult()
		{
			this.isConsistent = true;
			this.wasValidationSuccessful = false;
			this.ResultsPerAttendee = new Dictionary<string, MeetingComparisonResult>();
		}

		// Token: 0x0600022D RID: 557 RVA: 0x0000D1FC File Offset: 0x0000B3FC
		internal MeetingValidationResult(ExDateTime intervalStartDate, ExDateTime intervalEndDate, UserObject mailboxUser, MeetingData meetingData, bool duplicatesDetected, string errorDescription) : this()
		{
			this.numberOfDelegates = mailboxUser.ExchangePrincipal.Delegates.Count<ADObjectId>();
			this.meetingData = meetingData;
			this.duplicatesDetected = duplicatesDetected;
			this.intervalStartDate = intervalStartDate;
			this.intervalEndDate = intervalEndDate;
			this.errorDescription = errorDescription;
		}

		// Token: 0x0600022E RID: 558 RVA: 0x0000D24B File Offset: 0x0000B44B
		internal MeetingValidationResult(ExDateTime intervalStartDate, ExDateTime intervalEndDate, UserObject mailboxUser, MeetingData meetingDataKept, List<MeetingValidationResult> duplicates) : this()
		{
			this.numberOfDelegates = mailboxUser.ExchangePrincipal.Delegates.Count<ADObjectId>();
			this.meetingData = meetingDataKept;
			this.duplicatesDetected = true;
			this.intervalStartDate = intervalStartDate;
			this.DuplicateResults = duplicates;
		}

		// Token: 0x17000092 RID: 146
		// (get) Token: 0x0600022F RID: 559 RVA: 0x0000D287 File Offset: 0x0000B487
		// (set) Token: 0x06000230 RID: 560 RVA: 0x0000D28F File Offset: 0x0000B48F
		internal bool WasValidationSuccessful
		{
			get
			{
				return this.wasValidationSuccessful;
			}
			set
			{
				this.wasValidationSuccessful = value;
			}
		}

		// Token: 0x17000093 RID: 147
		// (get) Token: 0x06000231 RID: 561 RVA: 0x0000D298 File Offset: 0x0000B498
		internal bool DuplicatesDetected
		{
			get
			{
				return this.duplicatesDetected;
			}
		}

		// Token: 0x17000094 RID: 148
		// (get) Token: 0x06000232 RID: 562 RVA: 0x0000D2A0 File Offset: 0x0000B4A0
		// (set) Token: 0x06000233 RID: 563 RVA: 0x0000D2A8 File Offset: 0x0000B4A8
		internal bool IsDuplicate { get; set; }

		// Token: 0x17000095 RID: 149
		// (get) Token: 0x06000234 RID: 564 RVA: 0x0000D2B1 File Offset: 0x0000B4B1
		// (set) Token: 0x06000235 RID: 565 RVA: 0x0000D2B9 File Offset: 0x0000B4B9
		internal bool IsDuplicateRemoved { get; set; }

		// Token: 0x17000096 RID: 150
		// (get) Token: 0x06000236 RID: 566 RVA: 0x0000D2C2 File Offset: 0x0000B4C2
		internal StoreId MeetingId
		{
			get
			{
				return this.meetingData.Id;
			}
		}

		// Token: 0x17000097 RID: 151
		// (get) Token: 0x06000237 RID: 567 RVA: 0x0000D2CF File Offset: 0x0000B4CF
		internal MeetingData MeetingData
		{
			get
			{
				return this.meetingData;
			}
		}

		// Token: 0x17000098 RID: 152
		// (get) Token: 0x06000238 RID: 568 RVA: 0x0000D2D7 File Offset: 0x0000B4D7
		internal GlobalObjectId GlobalObjectId
		{
			get
			{
				return this.meetingData.GlobalObjectId;
			}
		}

		// Token: 0x17000099 RID: 153
		// (get) Token: 0x06000239 RID: 569 RVA: 0x0000D2E4 File Offset: 0x0000B4E4
		internal string CleanGlobalObjectId
		{
			get
			{
				return this.meetingData.CleanGlobalObjectId;
			}
		}

		// Token: 0x1700009A RID: 154
		// (get) Token: 0x0600023A RID: 570 RVA: 0x0000D2F1 File Offset: 0x0000B4F1
		internal string Subject
		{
			get
			{
				return this.meetingData.Subject;
			}
		}

		// Token: 0x1700009B RID: 155
		// (get) Token: 0x0600023B RID: 571 RVA: 0x0000D2FE File Offset: 0x0000B4FE
		internal CalendarItemType ItemType
		{
			get
			{
				return this.meetingData.CalendarItemType;
			}
		}

		// Token: 0x1700009C RID: 156
		// (get) Token: 0x0600023C RID: 572 RVA: 0x0000D30B File Offset: 0x0000B50B
		internal ExDateTime StartTime
		{
			get
			{
				return this.meetingData.StartTime;
			}
		}

		// Token: 0x1700009D RID: 157
		// (get) Token: 0x0600023D RID: 573 RVA: 0x0000D318 File Offset: 0x0000B518
		internal ExDateTime EndTime
		{
			get
			{
				return this.meetingData.EndTime;
			}
		}

		// Token: 0x1700009E RID: 158
		// (get) Token: 0x0600023E RID: 574 RVA: 0x0000D325 File Offset: 0x0000B525
		internal ExDateTime IntervalStartDate
		{
			get
			{
				return this.intervalStartDate;
			}
		}

		// Token: 0x1700009F RID: 159
		// (get) Token: 0x0600023F RID: 575 RVA: 0x0000D32D File Offset: 0x0000B52D
		internal ExDateTime IntervalEndDate
		{
			get
			{
				return this.intervalEndDate;
			}
		}

		// Token: 0x170000A0 RID: 160
		// (get) Token: 0x06000240 RID: 576 RVA: 0x0000D335 File Offset: 0x0000B535
		internal string MailboxUserPrimarySmtpAddress
		{
			get
			{
				return this.meetingData.MailboxUserPrimarySmtpAddress;
			}
		}

		// Token: 0x170000A1 RID: 161
		// (get) Token: 0x06000241 RID: 577 RVA: 0x0000D342 File Offset: 0x0000B542
		internal int NumberOfDelegates
		{
			get
			{
				return this.numberOfDelegates;
			}
		}

		// Token: 0x170000A2 RID: 162
		// (get) Token: 0x06000242 RID: 578 RVA: 0x0000D34A File Offset: 0x0000B54A
		internal string OrganizerPrimarySmtpAddress
		{
			get
			{
				return this.meetingData.OrganizerPrimarySmtpAddress;
			}
		}

		// Token: 0x170000A3 RID: 163
		// (get) Token: 0x06000243 RID: 579 RVA: 0x0000D357 File Offset: 0x0000B557
		internal bool IsOrganizer
		{
			get
			{
				return this.meetingData.IsOrganizer;
			}
		}

		// Token: 0x170000A4 RID: 164
		// (get) Token: 0x06000244 RID: 580 RVA: 0x0000D364 File Offset: 0x0000B564
		// (set) Token: 0x06000245 RID: 581 RVA: 0x0000D36C File Offset: 0x0000B56C
		internal bool IsConsistent
		{
			get
			{
				return this.isConsistent;
			}
			set
			{
				this.isConsistent = value;
			}
		}

		// Token: 0x170000A5 RID: 165
		// (get) Token: 0x06000246 RID: 582 RVA: 0x0000D375 File Offset: 0x0000B575
		// (set) Token: 0x06000247 RID: 583 RVA: 0x0000D37D File Offset: 0x0000B57D
		internal string FixupDescription
		{
			get
			{
				return this.fixupDescription;
			}
			set
			{
				this.fixupDescription = value;
			}
		}

		// Token: 0x170000A6 RID: 166
		// (get) Token: 0x06000248 RID: 584 RVA: 0x0000D386 File Offset: 0x0000B586
		// (set) Token: 0x06000249 RID: 585 RVA: 0x0000D38E File Offset: 0x0000B58E
		internal Dictionary<string, MeetingComparisonResult> ResultsPerAttendee { get; private set; }

		// Token: 0x170000A7 RID: 167
		// (get) Token: 0x0600024A RID: 586 RVA: 0x0000D397 File Offset: 0x0000B597
		// (set) Token: 0x0600024B RID: 587 RVA: 0x0000D39F File Offset: 0x0000B59F
		internal string ErrorDescription
		{
			get
			{
				return this.errorDescription;
			}
			set
			{
				this.errorDescription = value;
			}
		}

		// Token: 0x170000A8 RID: 168
		// (get) Token: 0x0600024C RID: 588 RVA: 0x0000D3A8 File Offset: 0x0000B5A8
		internal string Location
		{
			get
			{
				return this.meetingData.Location;
			}
		}

		// Token: 0x170000A9 RID: 169
		// (get) Token: 0x0600024D RID: 589 RVA: 0x0000D3B5 File Offset: 0x0000B5B5
		internal ExDateTime CreationTime
		{
			get
			{
				return this.meetingData.CreationTime;
			}
		}

		// Token: 0x170000AA RID: 170
		// (get) Token: 0x0600024E RID: 590 RVA: 0x0000D3C2 File Offset: 0x0000B5C2
		internal ExDateTime LastModifiedTime
		{
			get
			{
				return this.meetingData.LastModifiedTime;
			}
		}

		// Token: 0x170000AB RID: 171
		// (get) Token: 0x0600024F RID: 591 RVA: 0x0000D3CF File Offset: 0x0000B5CF
		// (set) Token: 0x06000250 RID: 592 RVA: 0x0000D3D7 File Offset: 0x0000B5D7
		internal List<MeetingValidationResult> DuplicateResults { get; private set; }

		// Token: 0x06000251 RID: 593 RVA: 0x0000D3E0 File Offset: 0x0000B5E0
		public XmlSchema GetSchema()
		{
			return null;
		}

		// Token: 0x06000252 RID: 594 RVA: 0x0000D3E3 File Offset: 0x0000B5E3
		public void ReadXml(XmlReader reader)
		{
			throw new NotSupportedException("XML deserialization is not supported.");
		}

		// Token: 0x06000253 RID: 595 RVA: 0x0000D3F0 File Offset: 0x0000B5F0
		public void WriteXml(XmlWriter writer)
		{
			writer.WriteAttributeString("type", this.ItemType.ToString());
			writer.WriteAttributeString("validating", this.IsOrganizer ? "organizer" : "attendee");
			if (this.MeetingId != null)
			{
				writer.WriteElementString("MeetingId", this.MeetingId.ToBase64String());
			}
			else
			{
				writer.WriteElementString("MeetingId", "0");
			}
			if (!this.wasValidationSuccessful)
			{
				writer.WriteElementString("ErrorDescription", this.errorDescription);
			}
			writer.WriteElementString("GlobalObjectId", this.GlobalObjectId.ToString());
			writer.WriteElementString("CleanGlobalObjectId", this.CleanGlobalObjectId);
			writer.WriteElementString("CreationTime", this.CreationTime.ToString());
			writer.WriteElementString("LastModifiedTime", this.LastModifiedTime.ToString());
			writer.WriteElementString("Subject", this.Subject);
			writer.WriteElementString("StartTime", this.StartTime.ToString());
			writer.WriteElementString("EndTime", this.EndTime.ToString());
			writer.WriteElementString("Location", this.Location);
			writer.WriteElementString("Organizer", this.OrganizerPrimarySmtpAddress);
			writer.WriteElementString("IsConsistent", this.isConsistent.ToString());
			writer.WriteElementString("DuplicatesDetected", this.duplicatesDetected.ToString());
			writer.WriteElementString("IsDuplicate", this.IsDuplicate.ToString());
			writer.WriteElementString("MeetingDeleted", this.IsDuplicateRemoved.ToString());
			writer.Flush();
			if (this.MeetingData != null)
			{
				writer.WriteElementString("ExtractVersion", string.Format("0x{0:X}", this.MeetingData.ExtractVersion));
				writer.WriteElementString("ExtractTime", this.MeetingData.ExtractTime.ToString());
				writer.WriteElementString("HasConflicts", this.MeetingData.HasConflicts.ToString());
				writer.WriteElementString("NumDelegates", this.numberOfDelegates.ToString());
				writer.WriteElementString("InternetMessageId", this.MeetingData.InternetMessageId);
				if (this.MeetingData.SequenceNumber != -2147483648)
				{
					writer.WriteElementString("SequenceNumber", this.MeetingData.SequenceNumber.ToString());
				}
				if (this.MeetingData.LastSequenceNumber != -2147483648)
				{
					writer.WriteElementString("LastSequenceNumber", this.MeetingData.LastSequenceNumber.ToString());
				}
				if (this.MeetingData.OwnerAppointmentId != null)
				{
					writer.WriteElementString("OwnerApptId", this.MeetingData.OwnerAppointmentId.Value.ToString());
				}
				if (this.MeetingData.OwnerCriticalChangeTime != ExDateTime.MinValue)
				{
					writer.WriteElementString("OwnerCriticalChangeTime", this.MeetingData.OwnerCriticalChangeTime.ToString());
				}
				if (this.MeetingData.AttendeeCriticalChangeTime != ExDateTime.MinValue)
				{
					writer.WriteElementString("AttendeeCriticalChangeTime", this.MeetingData.AttendeeCriticalChangeTime.ToString());
				}
			}
			foreach (KeyValuePair<string, MeetingComparisonResult> keyValuePair in this.ResultsPerAttendee)
			{
				writer.WriteStartElement("ResultsPerAttendee");
				writer.WriteAttributeString("attendee", keyValuePair.Key);
				XmlSerializer xmlSerializer = new XmlSerializer(typeof(ConsistencyCheckResult));
				foreach (ConsistencyCheckResult o in keyValuePair.Value)
				{
					xmlSerializer.Serialize(writer, o);
				}
				writer.WriteEndElement();
			}
			writer.Flush();
		}

		// Token: 0x04000168 RID: 360
		private bool wasValidationSuccessful;

		// Token: 0x04000169 RID: 361
		private MeetingData meetingData;

		// Token: 0x0400016A RID: 362
		private bool isConsistent = true;

		// Token: 0x0400016B RID: 363
		private string errorDescription;

		// Token: 0x0400016C RID: 364
		private bool duplicatesDetected;

		// Token: 0x0400016D RID: 365
		private string fixupDescription;

		// Token: 0x0400016E RID: 366
		private ExDateTime intervalStartDate;

		// Token: 0x0400016F RID: 367
		private ExDateTime intervalEndDate;

		// Token: 0x04000170 RID: 368
		private int numberOfDelegates;
	}
}
