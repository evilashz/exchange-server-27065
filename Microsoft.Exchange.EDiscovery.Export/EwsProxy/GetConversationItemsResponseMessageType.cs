using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.EDiscovery.Export.EwsProxy
{
	// Token: 0x02000112 RID: 274
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DebuggerStepThrough]
	[Serializable]
	public class GetConversationItemsResponseMessageType : ResponseMessageType
	{
		// Token: 0x1700032D RID: 813
		// (get) Token: 0x06000C00 RID: 3072 RVA: 0x000214F5 File Offset: 0x0001F6F5
		// (set) Token: 0x06000C01 RID: 3073 RVA: 0x000214FD File Offset: 0x0001F6FD
		public ConversationResponseType Conversation
		{
			get
			{
				return this.conversationField;
			}
			set
			{
				this.conversationField = value;
			}
		}

		// Token: 0x04000890 RID: 2192
		private ConversationResponseType conversationField;
	}
}
