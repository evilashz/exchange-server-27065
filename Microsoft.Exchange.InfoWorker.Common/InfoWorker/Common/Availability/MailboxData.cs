using System;
using System.Runtime.Serialization;
using System.Xml.Serialization;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.InfoWorker.Common.MeetingSuggestions;

namespace Microsoft.Exchange.InfoWorker.Common.Availability
{
	// Token: 0x02000102 RID: 258
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class MailboxData
	{
		// Token: 0x170001A5 RID: 421
		// (get) Token: 0x060006E4 RID: 1764 RVA: 0x0001E5F7 File Offset: 0x0001C7F7
		// (set) Token: 0x060006E5 RID: 1765 RVA: 0x0001E5FF File Offset: 0x0001C7FF
		[XmlElement]
		[DataMember]
		public EmailAddress Email
		{
			get
			{
				return this.email;
			}
			set
			{
				this.email = value;
			}
		}

		// Token: 0x170001A6 RID: 422
		// (get) Token: 0x060006E6 RID: 1766 RVA: 0x0001E608 File Offset: 0x0001C808
		// (set) Token: 0x060006E7 RID: 1767 RVA: 0x0001E610 File Offset: 0x0001C810
		[XmlElement]
		[IgnoreDataMember]
		public MeetingAttendeeType AttendeeType
		{
			get
			{
				return this.attendeeType;
			}
			set
			{
				this.attendeeType = value;
			}
		}

		// Token: 0x170001A7 RID: 423
		// (get) Token: 0x060006E8 RID: 1768 RVA: 0x0001E619 File Offset: 0x0001C819
		// (set) Token: 0x060006E9 RID: 1769 RVA: 0x0001E626 File Offset: 0x0001C826
		[XmlIgnore]
		[DataMember(Name = "AttendeeType")]
		public string AttendeeTypeString
		{
			get
			{
				return EnumUtil.ToString<MeetingAttendeeType>(this.AttendeeType);
			}
			set
			{
				this.AttendeeType = EnumUtil.Parse<MeetingAttendeeType>(value);
			}
		}

		// Token: 0x170001A8 RID: 424
		// (get) Token: 0x060006EA RID: 1770 RVA: 0x0001E634 File Offset: 0x0001C834
		// (set) Token: 0x060006EB RID: 1771 RVA: 0x0001E63C File Offset: 0x0001C83C
		[XmlElement]
		[DataMember]
		public bool ExcludeConflicts
		{
			get
			{
				return this.excludeConflicts;
			}
			set
			{
				this.excludeConflicts = value;
			}
		}

		// Token: 0x060006EC RID: 1772 RVA: 0x0001E645 File Offset: 0x0001C845
		public override string ToString()
		{
			return string.Format("EmailAddress = {0}, Attendee Type = {1}, Exclude Conflicts = {2}", this.email, this.attendeeType, this.excludeConflicts);
		}

		// Token: 0x060006ED RID: 1773 RVA: 0x0001E66D File Offset: 0x0001C86D
		public MailboxData()
		{
			this.Init();
		}

		// Token: 0x060006EE RID: 1774 RVA: 0x0001E67B File Offset: 0x0001C87B
		internal MailboxData(EmailAddress email)
		{
			this.Init();
			this.email = email;
		}

		// Token: 0x170001A9 RID: 425
		// (get) Token: 0x060006EF RID: 1775 RVA: 0x0001E690 File Offset: 0x0001C890
		// (set) Token: 0x060006F0 RID: 1776 RVA: 0x0001E698 File Offset: 0x0001C898
		internal StoreObjectId AssociatedFolderId
		{
			get
			{
				return this.associatedFolderId;
			}
			set
			{
				this.associatedFolderId = value;
			}
		}

		// Token: 0x060006F1 RID: 1777 RVA: 0x0001E6A1 File Offset: 0x0001C8A1
		[OnDeserializing]
		private void Init(StreamingContext context)
		{
			this.Init();
		}

		// Token: 0x060006F2 RID: 1778 RVA: 0x0001E6A9 File Offset: 0x0001C8A9
		private void Init()
		{
			this.attendeeType = MeetingAttendeeType.Required;
		}

		// Token: 0x04000423 RID: 1059
		private EmailAddress email;

		// Token: 0x04000424 RID: 1060
		private MeetingAttendeeType attendeeType;

		// Token: 0x04000425 RID: 1061
		private bool excludeConflicts;

		// Token: 0x04000426 RID: 1062
		private StoreObjectId associatedFolderId;
	}
}
