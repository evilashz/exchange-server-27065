using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.EDiscovery.Export.EwsProxy
{
	// Token: 0x02000385 RID: 901
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[Serializable]
	public class ApplyConversationActionType : BaseRequestType
	{
		// Token: 0x17000A64 RID: 2660
		// (get) Token: 0x06001C7D RID: 7293 RVA: 0x0002A01B File Offset: 0x0002821B
		// (set) Token: 0x06001C7E RID: 7294 RVA: 0x0002A023 File Offset: 0x00028223
		[XmlArrayItem("ConversationAction", Namespace = "http://schemas.microsoft.com/exchange/services/2006/types", IsNullable = false)]
		public ConversationActionType[] ConversationActions
		{
			get
			{
				return this.conversationActionsField;
			}
			set
			{
				this.conversationActionsField = value;
			}
		}

		// Token: 0x040012DF RID: 4831
		private ConversationActionType[] conversationActionsField;
	}
}
