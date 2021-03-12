using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.EDiscovery.Export.EwsProxy
{
	// Token: 0x02000378 RID: 888
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[Serializable]
	public class GetPasswordExpirationDateType : BaseRequestType
	{
		// Token: 0x17000A40 RID: 2624
		// (get) Token: 0x06001C2B RID: 7211 RVA: 0x00029D60 File Offset: 0x00027F60
		// (set) Token: 0x06001C2C RID: 7212 RVA: 0x00029D68 File Offset: 0x00027F68
		public string MailboxSmtpAddress
		{
			get
			{
				return this.mailboxSmtpAddressField;
			}
			set
			{
				this.mailboxSmtpAddressField = value;
			}
		}

		// Token: 0x040012AA RID: 4778
		private string mailboxSmtpAddressField;
	}
}
