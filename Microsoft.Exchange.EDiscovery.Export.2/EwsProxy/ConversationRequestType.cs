using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.EDiscovery.Export.EwsProxy
{
	// Token: 0x020002C2 RID: 706
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DesignerCategory("code")]
	[DebuggerStepThrough]
	[Serializable]
	public class ConversationRequestType
	{
		// Token: 0x17000886 RID: 2182
		// (get) Token: 0x06001820 RID: 6176 RVA: 0x00027B4E File Offset: 0x00025D4E
		// (set) Token: 0x06001821 RID: 6177 RVA: 0x00027B56 File Offset: 0x00025D56
		public ItemIdType ConversationId
		{
			get
			{
				return this.conversationIdField;
			}
			set
			{
				this.conversationIdField = value;
			}
		}

		// Token: 0x17000887 RID: 2183
		// (get) Token: 0x06001822 RID: 6178 RVA: 0x00027B5F File Offset: 0x00025D5F
		// (set) Token: 0x06001823 RID: 6179 RVA: 0x00027B67 File Offset: 0x00025D67
		[XmlElement(DataType = "base64Binary")]
		public byte[] SyncState
		{
			get
			{
				return this.syncStateField;
			}
			set
			{
				this.syncStateField = value;
			}
		}

		// Token: 0x04001060 RID: 4192
		private ItemIdType conversationIdField;

		// Token: 0x04001061 RID: 4193
		private byte[] syncStateField;
	}
}
