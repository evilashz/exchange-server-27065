using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Data.ApplicationLogic.TextMessaging
{
	// Token: 0x02000086 RID: 134
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[XmlType(AnonymousType = true)]
	[GeneratedCode("xsd", "4.0.30319.17627")]
	[Serializable]
	public class TextMessagingHostingDataServicesServiceSmtpToSmsGatewayRecipientAddressing
	{
		// Token: 0x17000195 RID: 405
		// (get) Token: 0x06000616 RID: 1558 RVA: 0x00015CE0 File Offset: 0x00013EE0
		// (set) Token: 0x06000617 RID: 1559 RVA: 0x00015CE8 File Offset: 0x00013EE8
		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public string SmtpAddress
		{
			get
			{
				return this.smtpAddressField;
			}
			set
			{
				this.smtpAddressField = value;
			}
		}

		// Token: 0x04000281 RID: 641
		private string smtpAddressField;
	}
}
