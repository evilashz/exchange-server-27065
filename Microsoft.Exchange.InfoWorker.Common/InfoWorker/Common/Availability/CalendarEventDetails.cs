using System;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace Microsoft.Exchange.InfoWorker.Common.Availability
{
	// Token: 0x020000ED RID: 237
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class CalendarEventDetails
	{
		// Token: 0x1700016F RID: 367
		// (get) Token: 0x0600063F RID: 1599 RVA: 0x0001B8B4 File Offset: 0x00019AB4
		// (set) Token: 0x06000640 RID: 1600 RVA: 0x0001B8BC File Offset: 0x00019ABC
		[DataMember]
		public string ID
		{
			get
			{
				return this.id;
			}
			set
			{
				this.id = value;
			}
		}

		// Token: 0x17000170 RID: 368
		// (get) Token: 0x06000641 RID: 1601 RVA: 0x0001B8C5 File Offset: 0x00019AC5
		// (set) Token: 0x06000642 RID: 1602 RVA: 0x0001B8CD File Offset: 0x00019ACD
		[XmlElement(IsNullable = false)]
		[DataMember]
		public string Subject
		{
			get
			{
				return this.subject;
			}
			set
			{
				this.subject = value;
			}
		}

		// Token: 0x17000171 RID: 369
		// (get) Token: 0x06000643 RID: 1603 RVA: 0x0001B8D6 File Offset: 0x00019AD6
		// (set) Token: 0x06000644 RID: 1604 RVA: 0x0001B8DE File Offset: 0x00019ADE
		[XmlElement(IsNullable = false)]
		[DataMember]
		public string Location
		{
			get
			{
				return this.location;
			}
			set
			{
				this.location = value;
			}
		}

		// Token: 0x17000172 RID: 370
		// (get) Token: 0x06000645 RID: 1605 RVA: 0x0001B8E7 File Offset: 0x00019AE7
		// (set) Token: 0x06000646 RID: 1606 RVA: 0x0001B8EF File Offset: 0x00019AEF
		[XmlElement(IsNullable = false)]
		[DataMember]
		public bool IsMeeting
		{
			get
			{
				return this.isMeeting;
			}
			set
			{
				this.isMeeting = value;
			}
		}

		// Token: 0x17000173 RID: 371
		// (get) Token: 0x06000647 RID: 1607 RVA: 0x0001B8F8 File Offset: 0x00019AF8
		// (set) Token: 0x06000648 RID: 1608 RVA: 0x0001B900 File Offset: 0x00019B00
		[XmlElement(IsNullable = false)]
		[DataMember]
		public bool IsRecurring
		{
			get
			{
				return this.isRecurring;
			}
			set
			{
				this.isRecurring = value;
			}
		}

		// Token: 0x17000174 RID: 372
		// (get) Token: 0x06000649 RID: 1609 RVA: 0x0001B909 File Offset: 0x00019B09
		// (set) Token: 0x0600064A RID: 1610 RVA: 0x0001B911 File Offset: 0x00019B11
		[XmlElement(IsNullable = false)]
		[DataMember]
		public bool IsException
		{
			get
			{
				return this.isException;
			}
			set
			{
				this.isException = value;
			}
		}

		// Token: 0x17000175 RID: 373
		// (get) Token: 0x0600064B RID: 1611 RVA: 0x0001B91A File Offset: 0x00019B1A
		// (set) Token: 0x0600064C RID: 1612 RVA: 0x0001B922 File Offset: 0x00019B22
		[XmlElement(IsNullable = false)]
		[DataMember]
		public bool IsReminderSet
		{
			get
			{
				return this.isReminderSet;
			}
			set
			{
				this.isReminderSet = value;
			}
		}

		// Token: 0x17000176 RID: 374
		// (get) Token: 0x0600064D RID: 1613 RVA: 0x0001B92B File Offset: 0x00019B2B
		// (set) Token: 0x0600064E RID: 1614 RVA: 0x0001B933 File Offset: 0x00019B33
		[DataMember]
		public bool IsPrivate
		{
			get
			{
				return this.isPrivate;
			}
			set
			{
				this.isPrivate = value;
			}
		}

		// Token: 0x0400039A RID: 922
		private string id;

		// Token: 0x0400039B RID: 923
		private string subject;

		// Token: 0x0400039C RID: 924
		private string location;

		// Token: 0x0400039D RID: 925
		private bool isMeeting;

		// Token: 0x0400039E RID: 926
		private bool isRecurring;

		// Token: 0x0400039F RID: 927
		private bool isException;

		// Token: 0x040003A0 RID: 928
		private bool isReminderSet;

		// Token: 0x040003A1 RID: 929
		private bool isPrivate;
	}
}
