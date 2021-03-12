using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.EDiscovery.Export.EwsProxy
{
	// Token: 0x0200036F RID: 879
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[Serializable]
	public class ExpandDLType : BaseRequestType
	{
		// Token: 0x17000A2F RID: 2607
		// (get) Token: 0x06001C00 RID: 7168 RVA: 0x00029BF7 File Offset: 0x00027DF7
		// (set) Token: 0x06001C01 RID: 7169 RVA: 0x00029BFF File Offset: 0x00027DFF
		public EmailAddressType Mailbox
		{
			get
			{
				return this.mailboxField;
			}
			set
			{
				this.mailboxField = value;
			}
		}

		// Token: 0x04001299 RID: 4761
		private EmailAddressType mailboxField;
	}
}
