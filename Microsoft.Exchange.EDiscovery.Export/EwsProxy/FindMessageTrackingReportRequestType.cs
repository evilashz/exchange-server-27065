using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.EDiscovery.Export.EwsProxy
{
	// Token: 0x02000349 RID: 841
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[Serializable]
	public class FindMessageTrackingReportRequestType : BaseRequestType
	{
		// Token: 0x170009DA RID: 2522
		// (get) Token: 0x06001B36 RID: 6966 RVA: 0x00029552 File Offset: 0x00027752
		// (set) Token: 0x06001B37 RID: 6967 RVA: 0x0002955A File Offset: 0x0002775A
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

		// Token: 0x170009DB RID: 2523
		// (get) Token: 0x06001B38 RID: 6968 RVA: 0x00029563 File Offset: 0x00027763
		// (set) Token: 0x06001B39 RID: 6969 RVA: 0x0002956B File Offset: 0x0002776B
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

		// Token: 0x170009DC RID: 2524
		// (get) Token: 0x06001B3A RID: 6970 RVA: 0x00029574 File Offset: 0x00027774
		// (set) Token: 0x06001B3B RID: 6971 RVA: 0x0002957C File Offset: 0x0002777C
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

		// Token: 0x170009DD RID: 2525
		// (get) Token: 0x06001B3C RID: 6972 RVA: 0x00029585 File Offset: 0x00027785
		// (set) Token: 0x06001B3D RID: 6973 RVA: 0x0002958D File Offset: 0x0002778D
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

		// Token: 0x170009DE RID: 2526
		// (get) Token: 0x06001B3E RID: 6974 RVA: 0x00029596 File Offset: 0x00027796
		// (set) Token: 0x06001B3F RID: 6975 RVA: 0x0002959E File Offset: 0x0002779E
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

		// Token: 0x170009DF RID: 2527
		// (get) Token: 0x06001B40 RID: 6976 RVA: 0x000295A7 File Offset: 0x000277A7
		// (set) Token: 0x06001B41 RID: 6977 RVA: 0x000295AF File Offset: 0x000277AF
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

		// Token: 0x170009E0 RID: 2528
		// (get) Token: 0x06001B42 RID: 6978 RVA: 0x000295B8 File Offset: 0x000277B8
		// (set) Token: 0x06001B43 RID: 6979 RVA: 0x000295C0 File Offset: 0x000277C0
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

		// Token: 0x170009E1 RID: 2529
		// (get) Token: 0x06001B44 RID: 6980 RVA: 0x000295C9 File Offset: 0x000277C9
		// (set) Token: 0x06001B45 RID: 6981 RVA: 0x000295D1 File Offset: 0x000277D1
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

		// Token: 0x170009E2 RID: 2530
		// (get) Token: 0x06001B46 RID: 6982 RVA: 0x000295DA File Offset: 0x000277DA
		// (set) Token: 0x06001B47 RID: 6983 RVA: 0x000295E2 File Offset: 0x000277E2
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

		// Token: 0x170009E3 RID: 2531
		// (get) Token: 0x06001B48 RID: 6984 RVA: 0x000295EB File Offset: 0x000277EB
		// (set) Token: 0x06001B49 RID: 6985 RVA: 0x000295F3 File Offset: 0x000277F3
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

		// Token: 0x170009E4 RID: 2532
		// (get) Token: 0x06001B4A RID: 6986 RVA: 0x000295FC File Offset: 0x000277FC
		// (set) Token: 0x06001B4B RID: 6987 RVA: 0x00029604 File Offset: 0x00027804
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

		// Token: 0x170009E5 RID: 2533
		// (get) Token: 0x06001B4C RID: 6988 RVA: 0x0002960D File Offset: 0x0002780D
		// (set) Token: 0x06001B4D RID: 6989 RVA: 0x00029615 File Offset: 0x00027815
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

		// Token: 0x170009E6 RID: 2534
		// (get) Token: 0x06001B4E RID: 6990 RVA: 0x0002961E File Offset: 0x0002781E
		// (set) Token: 0x06001B4F RID: 6991 RVA: 0x00029626 File Offset: 0x00027826
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

		// Token: 0x170009E7 RID: 2535
		// (get) Token: 0x06001B50 RID: 6992 RVA: 0x0002962F File Offset: 0x0002782F
		// (set) Token: 0x06001B51 RID: 6993 RVA: 0x00029637 File Offset: 0x00027837
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

		// Token: 0x170009E8 RID: 2536
		// (get) Token: 0x06001B52 RID: 6994 RVA: 0x00029640 File Offset: 0x00027840
		// (set) Token: 0x06001B53 RID: 6995 RVA: 0x00029648 File Offset: 0x00027848
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

		// Token: 0x0400122A RID: 4650
		private string scopeField;

		// Token: 0x0400122B RID: 4651
		private string domainField;

		// Token: 0x0400122C RID: 4652
		private EmailAddressType senderField;

		// Token: 0x0400122D RID: 4653
		private EmailAddressType purportedSenderField;

		// Token: 0x0400122E RID: 4654
		private EmailAddressType recipientField;

		// Token: 0x0400122F RID: 4655
		private string subjectField;

		// Token: 0x04001230 RID: 4656
		private DateTime startDateTimeField;

		// Token: 0x04001231 RID: 4657
		private bool startDateTimeFieldSpecified;

		// Token: 0x04001232 RID: 4658
		private DateTime endDateTimeField;

		// Token: 0x04001233 RID: 4659
		private bool endDateTimeFieldSpecified;

		// Token: 0x04001234 RID: 4660
		private string messageIdField;

		// Token: 0x04001235 RID: 4661
		private EmailAddressType federatedDeliveryMailboxField;

		// Token: 0x04001236 RID: 4662
		private string diagnosticsLevelField;

		// Token: 0x04001237 RID: 4663
		private string serverHintField;

		// Token: 0x04001238 RID: 4664
		private TrackingPropertyType[] propertiesField;
	}
}
