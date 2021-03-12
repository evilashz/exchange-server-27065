using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.InfoWorker.Common.Availability.Proxy
{
	// Token: 0x020002EB RID: 747
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[GeneratedCode("wsdl", "2.0.50727.3038")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[Serializable]
	public class MessageTrackingReportType
	{
		// Token: 0x170005A7 RID: 1447
		// (get) Token: 0x060015DE RID: 5598 RVA: 0x00066F2E File Offset: 0x0006512E
		// (set) Token: 0x060015DF RID: 5599 RVA: 0x00066F36 File Offset: 0x00065136
		public EmailAddressType Sender
		{
			get
			{
				return this.senderField;
			}
			set
			{
				this.senderField = value;
			}
		}

		// Token: 0x170005A8 RID: 1448
		// (get) Token: 0x060015E0 RID: 5600 RVA: 0x00066F3F File Offset: 0x0006513F
		// (set) Token: 0x060015E1 RID: 5601 RVA: 0x00066F47 File Offset: 0x00065147
		public EmailAddressType PurportedSender
		{
			get
			{
				return this.purportedSenderField;
			}
			set
			{
				this.purportedSenderField = value;
			}
		}

		// Token: 0x170005A9 RID: 1449
		// (get) Token: 0x060015E2 RID: 5602 RVA: 0x00066F50 File Offset: 0x00065150
		// (set) Token: 0x060015E3 RID: 5603 RVA: 0x00066F58 File Offset: 0x00065158
		public string Subject
		{
			get
			{
				return this.subjectField;
			}
			set
			{
				this.subjectField = value;
			}
		}

		// Token: 0x170005AA RID: 1450
		// (get) Token: 0x060015E4 RID: 5604 RVA: 0x00066F61 File Offset: 0x00065161
		// (set) Token: 0x060015E5 RID: 5605 RVA: 0x00066F69 File Offset: 0x00065169
		public DateTime SubmitTime
		{
			get
			{
				return this.submitTimeField;
			}
			set
			{
				this.submitTimeField = value;
			}
		}

		// Token: 0x170005AB RID: 1451
		// (get) Token: 0x060015E6 RID: 5606 RVA: 0x00066F72 File Offset: 0x00065172
		// (set) Token: 0x060015E7 RID: 5607 RVA: 0x00066F7A File Offset: 0x0006517A
		[XmlIgnore]
		public bool SubmitTimeSpecified
		{
			get
			{
				return this.submitTimeFieldSpecified;
			}
			set
			{
				this.submitTimeFieldSpecified = value;
			}
		}

		// Token: 0x170005AC RID: 1452
		// (get) Token: 0x060015E8 RID: 5608 RVA: 0x00066F83 File Offset: 0x00065183
		// (set) Token: 0x060015E9 RID: 5609 RVA: 0x00066F8B File Offset: 0x0006518B
		[XmlArrayItem("Address", IsNullable = false)]
		public EmailAddressType[] OriginalRecipients
		{
			get
			{
				return this.originalRecipientsField;
			}
			set
			{
				this.originalRecipientsField = value;
			}
		}

		// Token: 0x170005AD RID: 1453
		// (get) Token: 0x060015EA RID: 5610 RVA: 0x00066F94 File Offset: 0x00065194
		// (set) Token: 0x060015EB RID: 5611 RVA: 0x00066F9C File Offset: 0x0006519C
		[XmlArrayItem("RecipientTrackingEvent", IsNullable = false)]
		public RecipientTrackingEventType[] RecipientTrackingEvents
		{
			get
			{
				return this.recipientTrackingEventsField;
			}
			set
			{
				this.recipientTrackingEventsField = value;
			}
		}

		// Token: 0x170005AE RID: 1454
		// (get) Token: 0x060015EC RID: 5612 RVA: 0x00066FA5 File Offset: 0x000651A5
		// (set) Token: 0x060015ED RID: 5613 RVA: 0x00066FAD File Offset: 0x000651AD
		[XmlArrayItem(IsNullable = false)]
		public TrackingPropertyType[] Properties
		{
			get
			{
				return this.propertiesField;
			}
			set
			{
				this.propertiesField = value;
			}
		}

		// Token: 0x04000E43 RID: 3651
		private EmailAddressType senderField;

		// Token: 0x04000E44 RID: 3652
		private EmailAddressType purportedSenderField;

		// Token: 0x04000E45 RID: 3653
		private string subjectField;

		// Token: 0x04000E46 RID: 3654
		private DateTime submitTimeField;

		// Token: 0x04000E47 RID: 3655
		private bool submitTimeFieldSpecified;

		// Token: 0x04000E48 RID: 3656
		private EmailAddressType[] originalRecipientsField;

		// Token: 0x04000E49 RID: 3657
		private RecipientTrackingEventType[] recipientTrackingEventsField;

		// Token: 0x04000E4A RID: 3658
		private TrackingPropertyType[] propertiesField;
	}
}
