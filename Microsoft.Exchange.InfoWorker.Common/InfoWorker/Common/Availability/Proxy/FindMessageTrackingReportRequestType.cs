using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.InfoWorker.Common.Availability.Proxy
{
	// Token: 0x020002EC RID: 748
	[GeneratedCode("wsdl", "2.0.50727.3038")]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[Serializable]
	public class FindMessageTrackingReportRequestType : BaseRequestType
	{
		// Token: 0x170005AF RID: 1455
		// (get) Token: 0x060015EF RID: 5615 RVA: 0x00066FBE File Offset: 0x000651BE
		// (set) Token: 0x060015F0 RID: 5616 RVA: 0x00066FC6 File Offset: 0x000651C6
		public string Scope
		{
			get
			{
				return this.scopeField;
			}
			set
			{
				this.scopeField = value;
			}
		}

		// Token: 0x170005B0 RID: 1456
		// (get) Token: 0x060015F1 RID: 5617 RVA: 0x00066FCF File Offset: 0x000651CF
		// (set) Token: 0x060015F2 RID: 5618 RVA: 0x00066FD7 File Offset: 0x000651D7
		public string Domain
		{
			get
			{
				return this.domainField;
			}
			set
			{
				this.domainField = value;
			}
		}

		// Token: 0x170005B1 RID: 1457
		// (get) Token: 0x060015F3 RID: 5619 RVA: 0x00066FE0 File Offset: 0x000651E0
		// (set) Token: 0x060015F4 RID: 5620 RVA: 0x00066FE8 File Offset: 0x000651E8
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

		// Token: 0x170005B2 RID: 1458
		// (get) Token: 0x060015F5 RID: 5621 RVA: 0x00066FF1 File Offset: 0x000651F1
		// (set) Token: 0x060015F6 RID: 5622 RVA: 0x00066FF9 File Offset: 0x000651F9
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

		// Token: 0x170005B3 RID: 1459
		// (get) Token: 0x060015F7 RID: 5623 RVA: 0x00067002 File Offset: 0x00065202
		// (set) Token: 0x060015F8 RID: 5624 RVA: 0x0006700A File Offset: 0x0006520A
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

		// Token: 0x170005B4 RID: 1460
		// (get) Token: 0x060015F9 RID: 5625 RVA: 0x00067013 File Offset: 0x00065213
		// (set) Token: 0x060015FA RID: 5626 RVA: 0x0006701B File Offset: 0x0006521B
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

		// Token: 0x170005B5 RID: 1461
		// (get) Token: 0x060015FB RID: 5627 RVA: 0x00067024 File Offset: 0x00065224
		// (set) Token: 0x060015FC RID: 5628 RVA: 0x0006702C File Offset: 0x0006522C
		public DateTime StartDateTime
		{
			get
			{
				return this.startDateTimeField;
			}
			set
			{
				this.startDateTimeField = value;
			}
		}

		// Token: 0x170005B6 RID: 1462
		// (get) Token: 0x060015FD RID: 5629 RVA: 0x00067035 File Offset: 0x00065235
		// (set) Token: 0x060015FE RID: 5630 RVA: 0x0006703D File Offset: 0x0006523D
		[XmlIgnore]
		public bool StartDateTimeSpecified
		{
			get
			{
				return this.startDateTimeFieldSpecified;
			}
			set
			{
				this.startDateTimeFieldSpecified = value;
			}
		}

		// Token: 0x170005B7 RID: 1463
		// (get) Token: 0x060015FF RID: 5631 RVA: 0x00067046 File Offset: 0x00065246
		// (set) Token: 0x06001600 RID: 5632 RVA: 0x0006704E File Offset: 0x0006524E
		public DateTime EndDateTime
		{
			get
			{
				return this.endDateTimeField;
			}
			set
			{
				this.endDateTimeField = value;
			}
		}

		// Token: 0x170005B8 RID: 1464
		// (get) Token: 0x06001601 RID: 5633 RVA: 0x00067057 File Offset: 0x00065257
		// (set) Token: 0x06001602 RID: 5634 RVA: 0x0006705F File Offset: 0x0006525F
		[XmlIgnore]
		public bool EndDateTimeSpecified
		{
			get
			{
				return this.endDateTimeFieldSpecified;
			}
			set
			{
				this.endDateTimeFieldSpecified = value;
			}
		}

		// Token: 0x170005B9 RID: 1465
		// (get) Token: 0x06001603 RID: 5635 RVA: 0x00067068 File Offset: 0x00065268
		// (set) Token: 0x06001604 RID: 5636 RVA: 0x00067070 File Offset: 0x00065270
		public string MessageId
		{
			get
			{
				return this.messageIdField;
			}
			set
			{
				this.messageIdField = value;
			}
		}

		// Token: 0x170005BA RID: 1466
		// (get) Token: 0x06001605 RID: 5637 RVA: 0x00067079 File Offset: 0x00065279
		// (set) Token: 0x06001606 RID: 5638 RVA: 0x00067081 File Offset: 0x00065281
		public EmailAddressType FederatedDeliveryMailbox
		{
			get
			{
				return this.federatedDeliveryMailboxField;
			}
			set
			{
				this.federatedDeliveryMailboxField = value;
			}
		}

		// Token: 0x170005BB RID: 1467
		// (get) Token: 0x06001607 RID: 5639 RVA: 0x0006708A File Offset: 0x0006528A
		// (set) Token: 0x06001608 RID: 5640 RVA: 0x00067092 File Offset: 0x00065292
		public string DiagnosticsLevel
		{
			get
			{
				return this.diagnosticsLevelField;
			}
			set
			{
				this.diagnosticsLevelField = value;
			}
		}

		// Token: 0x170005BC RID: 1468
		// (get) Token: 0x06001609 RID: 5641 RVA: 0x0006709B File Offset: 0x0006529B
		// (set) Token: 0x0600160A RID: 5642 RVA: 0x000670A3 File Offset: 0x000652A3
		public string ServerHint
		{
			get
			{
				return this.serverHintField;
			}
			set
			{
				this.serverHintField = value;
			}
		}

		// Token: 0x170005BD RID: 1469
		// (get) Token: 0x0600160B RID: 5643 RVA: 0x000670AC File Offset: 0x000652AC
		// (set) Token: 0x0600160C RID: 5644 RVA: 0x000670B4 File Offset: 0x000652B4
		[XmlArrayItem(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types", IsNullable = false)]
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

		// Token: 0x04000E4B RID: 3659
		private string scopeField;

		// Token: 0x04000E4C RID: 3660
		private string domainField;

		// Token: 0x04000E4D RID: 3661
		private EmailAddressType senderField;

		// Token: 0x04000E4E RID: 3662
		private EmailAddressType purportedSenderField;

		// Token: 0x04000E4F RID: 3663
		private EmailAddressType recipientField;

		// Token: 0x04000E50 RID: 3664
		private string subjectField;

		// Token: 0x04000E51 RID: 3665
		private DateTime startDateTimeField;

		// Token: 0x04000E52 RID: 3666
		private bool startDateTimeFieldSpecified;

		// Token: 0x04000E53 RID: 3667
		private DateTime endDateTimeField;

		// Token: 0x04000E54 RID: 3668
		private bool endDateTimeFieldSpecified;

		// Token: 0x04000E55 RID: 3669
		private string messageIdField;

		// Token: 0x04000E56 RID: 3670
		private EmailAddressType federatedDeliveryMailboxField;

		// Token: 0x04000E57 RID: 3671
		private string diagnosticsLevelField;

		// Token: 0x04000E58 RID: 3672
		private string serverHintField;

		// Token: 0x04000E59 RID: 3673
		private TrackingPropertyType[] propertiesField;
	}
}
