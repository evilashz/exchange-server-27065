using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.InfoWorker.Common.Availability.Proxy
{
	// Token: 0x020002E4 RID: 740
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[GeneratedCode("wsdl", "2.0.50727.3038")]
	[Serializable]
	public class RecipientTrackingEventType
	{
		// Token: 0x17000584 RID: 1412
		// (get) Token: 0x06001592 RID: 5522 RVA: 0x00066CAB File Offset: 0x00064EAB
		// (set) Token: 0x06001593 RID: 5523 RVA: 0x00066CB3 File Offset: 0x00064EB3
		public DateTime Date
		{
			get
			{
				return this.dateField;
			}
			set
			{
				this.dateField = value;
			}
		}

		// Token: 0x17000585 RID: 1413
		// (get) Token: 0x06001594 RID: 5524 RVA: 0x00066CBC File Offset: 0x00064EBC
		// (set) Token: 0x06001595 RID: 5525 RVA: 0x00066CC4 File Offset: 0x00064EC4
		public EmailAddressType Recipient
		{
			get
			{
				return this.recipientField;
			}
			set
			{
				this.recipientField = value;
			}
		}

		// Token: 0x17000586 RID: 1414
		// (get) Token: 0x06001596 RID: 5526 RVA: 0x00066CCD File Offset: 0x00064ECD
		// (set) Token: 0x06001597 RID: 5527 RVA: 0x00066CD5 File Offset: 0x00064ED5
		public string DeliveryStatus
		{
			get
			{
				return this.deliveryStatusField;
			}
			set
			{
				this.deliveryStatusField = value;
			}
		}

		// Token: 0x17000587 RID: 1415
		// (get) Token: 0x06001598 RID: 5528 RVA: 0x00066CDE File Offset: 0x00064EDE
		// (set) Token: 0x06001599 RID: 5529 RVA: 0x00066CE6 File Offset: 0x00064EE6
		public string EventDescription
		{
			get
			{
				return this.eventDescriptionField;
			}
			set
			{
				this.eventDescriptionField = value;
			}
		}

		// Token: 0x17000588 RID: 1416
		// (get) Token: 0x0600159A RID: 5530 RVA: 0x00066CEF File Offset: 0x00064EEF
		// (set) Token: 0x0600159B RID: 5531 RVA: 0x00066CF7 File Offset: 0x00064EF7
		[XmlArrayItem("String", IsNullable = false)]
		public string[] EventData
		{
			get
			{
				return this.eventDataField;
			}
			set
			{
				this.eventDataField = value;
			}
		}

		// Token: 0x17000589 RID: 1417
		// (get) Token: 0x0600159C RID: 5532 RVA: 0x00066D00 File Offset: 0x00064F00
		// (set) Token: 0x0600159D RID: 5533 RVA: 0x00066D08 File Offset: 0x00064F08
		public string Server
		{
			get
			{
				return this.serverField;
			}
			set
			{
				this.serverField = value;
			}
		}

		// Token: 0x1700058A RID: 1418
		// (get) Token: 0x0600159E RID: 5534 RVA: 0x00066D11 File Offset: 0x00064F11
		// (set) Token: 0x0600159F RID: 5535 RVA: 0x00066D19 File Offset: 0x00064F19
		[XmlElement(DataType = "nonNegativeInteger")]
		public string InternalId
		{
			get
			{
				return this.internalIdField;
			}
			set
			{
				this.internalIdField = value;
			}
		}

		// Token: 0x1700058B RID: 1419
		// (get) Token: 0x060015A0 RID: 5536 RVA: 0x00066D22 File Offset: 0x00064F22
		// (set) Token: 0x060015A1 RID: 5537 RVA: 0x00066D2A File Offset: 0x00064F2A
		public bool BccRecipient
		{
			get
			{
				return this.bccRecipientField;
			}
			set
			{
				this.bccRecipientField = value;
			}
		}

		// Token: 0x1700058C RID: 1420
		// (get) Token: 0x060015A2 RID: 5538 RVA: 0x00066D33 File Offset: 0x00064F33
		// (set) Token: 0x060015A3 RID: 5539 RVA: 0x00066D3B File Offset: 0x00064F3B
		[XmlIgnore]
		public bool BccRecipientSpecified
		{
			get
			{
				return this.bccRecipientFieldSpecified;
			}
			set
			{
				this.bccRecipientFieldSpecified = value;
			}
		}

		// Token: 0x1700058D RID: 1421
		// (get) Token: 0x060015A4 RID: 5540 RVA: 0x00066D44 File Offset: 0x00064F44
		// (set) Token: 0x060015A5 RID: 5541 RVA: 0x00066D4C File Offset: 0x00064F4C
		public bool HiddenRecipient
		{
			get
			{
				return this.hiddenRecipientField;
			}
			set
			{
				this.hiddenRecipientField = value;
			}
		}

		// Token: 0x1700058E RID: 1422
		// (get) Token: 0x060015A6 RID: 5542 RVA: 0x00066D55 File Offset: 0x00064F55
		// (set) Token: 0x060015A7 RID: 5543 RVA: 0x00066D5D File Offset: 0x00064F5D
		[XmlIgnore]
		public bool HiddenRecipientSpecified
		{
			get
			{
				return this.hiddenRecipientFieldSpecified;
			}
			set
			{
				this.hiddenRecipientFieldSpecified = value;
			}
		}

		// Token: 0x1700058F RID: 1423
		// (get) Token: 0x060015A8 RID: 5544 RVA: 0x00066D66 File Offset: 0x00064F66
		// (set) Token: 0x060015A9 RID: 5545 RVA: 0x00066D6E File Offset: 0x00064F6E
		public string UniquePathId
		{
			get
			{
				return this.uniquePathIdField;
			}
			set
			{
				this.uniquePathIdField = value;
			}
		}

		// Token: 0x17000590 RID: 1424
		// (get) Token: 0x060015AA RID: 5546 RVA: 0x00066D77 File Offset: 0x00064F77
		// (set) Token: 0x060015AB RID: 5547 RVA: 0x00066D7F File Offset: 0x00064F7F
		public string RootAddress
		{
			get
			{
				return this.rootAddressField;
			}
			set
			{
				this.rootAddressField = value;
			}
		}

		// Token: 0x17000591 RID: 1425
		// (get) Token: 0x060015AC RID: 5548 RVA: 0x00066D88 File Offset: 0x00064F88
		// (set) Token: 0x060015AD RID: 5549 RVA: 0x00066D90 File Offset: 0x00064F90
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

		// Token: 0x04000E1D RID: 3613
		private DateTime dateField;

		// Token: 0x04000E1E RID: 3614
		private EmailAddressType recipientField;

		// Token: 0x04000E1F RID: 3615
		private string deliveryStatusField;

		// Token: 0x04000E20 RID: 3616
		private string eventDescriptionField;

		// Token: 0x04000E21 RID: 3617
		private string[] eventDataField;

		// Token: 0x04000E22 RID: 3618
		private string serverField;

		// Token: 0x04000E23 RID: 3619
		private string internalIdField;

		// Token: 0x04000E24 RID: 3620
		private bool bccRecipientField;

		// Token: 0x04000E25 RID: 3621
		private bool bccRecipientFieldSpecified;

		// Token: 0x04000E26 RID: 3622
		private bool hiddenRecipientField;

		// Token: 0x04000E27 RID: 3623
		private bool hiddenRecipientFieldSpecified;

		// Token: 0x04000E28 RID: 3624
		private string uniquePathIdField;

		// Token: 0x04000E29 RID: 3625
		private string rootAddressField;

		// Token: 0x04000E2A RID: 3626
		private TrackingPropertyType[] propertiesField;
	}
}
