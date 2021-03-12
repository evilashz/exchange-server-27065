using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.EDiscovery.Export.EwsProxy
{
	// Token: 0x020001BB RID: 443
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[DesignerCategory("code")]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DebuggerStepThrough]
	[Serializable]
	public class MessageTrackingReportType
	{
		// Token: 0x17000668 RID: 1640
		// (get) Token: 0x060012FD RID: 4861 RVA: 0x00025018 File Offset: 0x00023218
		// (set) Token: 0x060012FE RID: 4862 RVA: 0x00025020 File Offset: 0x00023220
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

		// Token: 0x17000669 RID: 1641
		// (get) Token: 0x060012FF RID: 4863 RVA: 0x00025029 File Offset: 0x00023229
		// (set) Token: 0x06001300 RID: 4864 RVA: 0x00025031 File Offset: 0x00023231
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

		// Token: 0x1700066A RID: 1642
		// (get) Token: 0x06001301 RID: 4865 RVA: 0x0002503A File Offset: 0x0002323A
		// (set) Token: 0x06001302 RID: 4866 RVA: 0x00025042 File Offset: 0x00023242
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

		// Token: 0x1700066B RID: 1643
		// (get) Token: 0x06001303 RID: 4867 RVA: 0x0002504B File Offset: 0x0002324B
		// (set) Token: 0x06001304 RID: 4868 RVA: 0x00025053 File Offset: 0x00023253
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

		// Token: 0x1700066C RID: 1644
		// (get) Token: 0x06001305 RID: 4869 RVA: 0x0002505C File Offset: 0x0002325C
		// (set) Token: 0x06001306 RID: 4870 RVA: 0x00025064 File Offset: 0x00023264
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

		// Token: 0x1700066D RID: 1645
		// (get) Token: 0x06001307 RID: 4871 RVA: 0x0002506D File Offset: 0x0002326D
		// (set) Token: 0x06001308 RID: 4872 RVA: 0x00025075 File Offset: 0x00023275
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

		// Token: 0x1700066E RID: 1646
		// (get) Token: 0x06001309 RID: 4873 RVA: 0x0002507E File Offset: 0x0002327E
		// (set) Token: 0x0600130A RID: 4874 RVA: 0x00025086 File Offset: 0x00023286
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

		// Token: 0x1700066F RID: 1647
		// (get) Token: 0x0600130B RID: 4875 RVA: 0x0002508F File Offset: 0x0002328F
		// (set) Token: 0x0600130C RID: 4876 RVA: 0x00025097 File Offset: 0x00023297
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

		// Token: 0x04000D3F RID: 3391
		private EmailAddressType senderField;

		// Token: 0x04000D40 RID: 3392
		private EmailAddressType purportedSenderField;

		// Token: 0x04000D41 RID: 3393
		private string subjectField;

		// Token: 0x04000D42 RID: 3394
		private DateTime submitTimeField;

		// Token: 0x04000D43 RID: 3395
		private bool submitTimeFieldSpecified;

		// Token: 0x04000D44 RID: 3396
		private EmailAddressType[] originalRecipientsField;

		// Token: 0x04000D45 RID: 3397
		private RecipientTrackingEventType[] recipientTrackingEventsField;

		// Token: 0x04000D46 RID: 3398
		private TrackingPropertyType[] propertiesField;
	}
}
