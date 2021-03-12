using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Data.ApplicationLogic.TextMessaging
{
	// Token: 0x02000081 RID: 129
	[XmlType(AnonymousType = true)]
	[GeneratedCode("xsd", "4.0.30319.17627")]
	[DesignerCategory("code")]
	[DebuggerStepThrough]
	[Serializable]
	public class TextMessagingHostingDataServicesService
	{
		// Token: 0x1700018A RID: 394
		// (get) Token: 0x060005FD RID: 1533 RVA: 0x00015C0D File Offset: 0x00013E0D
		// (set) Token: 0x060005FE RID: 1534 RVA: 0x00015C15 File Offset: 0x00013E15
		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public string RegionIso2
		{
			get
			{
				return this.regionIso2Field;
			}
			set
			{
				this.regionIso2Field = value;
			}
		}

		// Token: 0x1700018B RID: 395
		// (get) Token: 0x060005FF RID: 1535 RVA: 0x00015C1E File Offset: 0x00013E1E
		// (set) Token: 0x06000600 RID: 1536 RVA: 0x00015C26 File Offset: 0x00013E26
		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public int CarrierIdentity
		{
			get
			{
				return this.carrierIdentityField;
			}
			set
			{
				this.carrierIdentityField = value;
			}
		}

		// Token: 0x1700018C RID: 396
		// (get) Token: 0x06000601 RID: 1537 RVA: 0x00015C2F File Offset: 0x00013E2F
		// (set) Token: 0x06000602 RID: 1538 RVA: 0x00015C37 File Offset: 0x00013E37
		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public TextMessagingHostingDataServicesServiceType Type
		{
			get
			{
				return this.typeField;
			}
			set
			{
				this.typeField = value;
			}
		}

		// Token: 0x1700018D RID: 397
		// (get) Token: 0x06000603 RID: 1539 RVA: 0x00015C40 File Offset: 0x00013E40
		// (set) Token: 0x06000604 RID: 1540 RVA: 0x00015C48 File Offset: 0x00013E48
		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public TextMessagingHostingDataServicesServiceVoiceCallForwarding VoiceCallForwarding
		{
			get
			{
				return this.voiceCallForwardingField;
			}
			set
			{
				this.voiceCallForwardingField = value;
			}
		}

		// Token: 0x1700018E RID: 398
		// (get) Token: 0x06000605 RID: 1541 RVA: 0x00015C51 File Offset: 0x00013E51
		// (set) Token: 0x06000606 RID: 1542 RVA: 0x00015C59 File Offset: 0x00013E59
		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public TextMessagingHostingDataServicesServiceSmtpToSmsGateway SmtpToSmsGateway
		{
			get
			{
				return this.smtpToSmsGatewayField;
			}
			set
			{
				this.smtpToSmsGatewayField = value;
			}
		}

		// Token: 0x0400026E RID: 622
		private string regionIso2Field;

		// Token: 0x0400026F RID: 623
		private int carrierIdentityField;

		// Token: 0x04000270 RID: 624
		private TextMessagingHostingDataServicesServiceType typeField;

		// Token: 0x04000271 RID: 625
		private TextMessagingHostingDataServicesServiceVoiceCallForwarding voiceCallForwardingField;

		// Token: 0x04000272 RID: 626
		private TextMessagingHostingDataServicesServiceSmtpToSmsGateway smtpToSmsGatewayField;
	}
}
