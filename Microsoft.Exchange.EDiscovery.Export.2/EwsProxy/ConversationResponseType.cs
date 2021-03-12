using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.EDiscovery.Export.EwsProxy
{
	// Token: 0x02000113 RID: 275
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[Serializable]
	public class ConversationResponseType
	{
		// Token: 0x1700032E RID: 814
		// (get) Token: 0x06000C03 RID: 3075 RVA: 0x0002150E File Offset: 0x0001F70E
		// (set) Token: 0x06000C04 RID: 3076 RVA: 0x00021516 File Offset: 0x0001F716
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

		// Token: 0x1700032F RID: 815
		// (get) Token: 0x06000C05 RID: 3077 RVA: 0x0002151F File Offset: 0x0001F71F
		// (set) Token: 0x06000C06 RID: 3078 RVA: 0x00021527 File Offset: 0x0001F727
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

		// Token: 0x17000330 RID: 816
		// (get) Token: 0x06000C07 RID: 3079 RVA: 0x00021530 File Offset: 0x0001F730
		// (set) Token: 0x06000C08 RID: 3080 RVA: 0x00021538 File Offset: 0x0001F738
		[XmlArrayItem("ConversationNode", IsNullable = false)]
		public ConversationNodeType[] ConversationNodes
		{
			get
			{
				return this.conversationNodesField;
			}
			set
			{
				this.conversationNodesField = value;
			}
		}

		// Token: 0x04000891 RID: 2193
		private ItemIdType conversationIdField;

		// Token: 0x04000892 RID: 2194
		private byte[] syncStateField;

		// Token: 0x04000893 RID: 2195
		private ConversationNodeType[] conversationNodesField;
	}
}
