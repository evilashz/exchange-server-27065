using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.EDiscovery.Export.EwsProxy
{
	// Token: 0x02000346 RID: 838
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[Serializable]
	public class GetInboxRulesRequestType : BaseRequestType
	{
		// Token: 0x170009D1 RID: 2513
		// (get) Token: 0x06001B22 RID: 6946 RVA: 0x000294A9 File Offset: 0x000276A9
		// (set) Token: 0x06001B23 RID: 6947 RVA: 0x000294B1 File Offset: 0x000276B1
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

		// Token: 0x0400121E RID: 4638
		private string mailboxSmtpAddressField;
	}
}
