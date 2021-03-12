using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.EDiscovery.Export.EwsProxy
{
	// Token: 0x02000194 RID: 404
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
	[DebuggerStepThrough]
	[Serializable]
	public class SetHoldOnMailboxesResponseMessageType : ResponseMessageType
	{
		// Token: 0x170005A4 RID: 1444
		// (get) Token: 0x06001154 RID: 4436 RVA: 0x0002420C File Offset: 0x0002240C
		// (set) Token: 0x06001155 RID: 4437 RVA: 0x00024214 File Offset: 0x00022414
		public MailboxHoldResultType MailboxHoldResult
		{
			get
			{
				return this.mailboxHoldResultField;
			}
			set
			{
				this.mailboxHoldResultField = value;
			}
		}

		// Token: 0x04000BEE RID: 3054
		private MailboxHoldResultType mailboxHoldResultField;
	}
}
