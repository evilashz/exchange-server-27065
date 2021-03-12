using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.EDiscovery.Export.EwsProxy
{
	// Token: 0x0200024D RID: 589
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[Serializable]
	public class GetMailTipsResponseMessageType : ResponseMessageType
	{
		// Token: 0x170007B6 RID: 1974
		// (get) Token: 0x06001612 RID: 5650 RVA: 0x00026A0E File Offset: 0x00024C0E
		// (set) Token: 0x06001613 RID: 5651 RVA: 0x00026A16 File Offset: 0x00024C16
		[XmlArrayItem(IsNullable = false)]
		public MailTipsResponseMessageType[] ResponseMessages
		{
			get
			{
				return this.responseMessagesField;
			}
			set
			{
				this.responseMessagesField = value;
			}
		}

		// Token: 0x04000F2A RID: 3882
		private MailTipsResponseMessageType[] responseMessagesField;
	}
}
