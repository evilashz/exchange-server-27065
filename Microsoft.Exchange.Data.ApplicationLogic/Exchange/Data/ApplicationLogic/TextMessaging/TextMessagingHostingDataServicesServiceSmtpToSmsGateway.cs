using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Data.ApplicationLogic.TextMessaging
{
	// Token: 0x02000085 RID: 133
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[XmlType(AnonymousType = true)]
	[GeneratedCode("xsd", "4.0.30319.17627")]
	[Serializable]
	public class TextMessagingHostingDataServicesServiceSmtpToSmsGateway
	{
		// Token: 0x17000193 RID: 403
		// (get) Token: 0x06000611 RID: 1553 RVA: 0x00015CB6 File Offset: 0x00013EB6
		// (set) Token: 0x06000612 RID: 1554 RVA: 0x00015CBE File Offset: 0x00013EBE
		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public TextMessagingHostingDataServicesServiceSmtpToSmsGatewayRecipientAddressing RecipientAddressing
		{
			get
			{
				return this.recipientAddressingField;
			}
			set
			{
				this.recipientAddressingField = value;
			}
		}

		// Token: 0x17000194 RID: 404
		// (get) Token: 0x06000613 RID: 1555 RVA: 0x00015CC7 File Offset: 0x00013EC7
		// (set) Token: 0x06000614 RID: 1556 RVA: 0x00015CCF File Offset: 0x00013ECF
		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public TextMessagingHostingDataServicesServiceSmtpToSmsGatewayMessageRendering MessageRendering
		{
			get
			{
				return this.messageRenderingField;
			}
			set
			{
				this.messageRenderingField = value;
			}
		}

		// Token: 0x0400027F RID: 639
		private TextMessagingHostingDataServicesServiceSmtpToSmsGatewayRecipientAddressing recipientAddressingField;

		// Token: 0x04000280 RID: 640
		private TextMessagingHostingDataServicesServiceSmtpToSmsGatewayMessageRendering messageRenderingField;
	}
}
