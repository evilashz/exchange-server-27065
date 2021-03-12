using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.EDiscovery.Export.EwsProxy
{
	// Token: 0x02000198 RID: 408
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[Serializable]
	public class GetHoldOnMailboxesResponseMessageType : ResponseMessageType
	{
		// Token: 0x170005AB RID: 1451
		// (get) Token: 0x06001165 RID: 4453 RVA: 0x0002429B File Offset: 0x0002249B
		// (set) Token: 0x06001166 RID: 4454 RVA: 0x000242A3 File Offset: 0x000224A3
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

		// Token: 0x04000BFB RID: 3067
		private MailboxHoldResultType mailboxHoldResultField;
	}
}
