using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.EDiscovery.Export.EwsProxy
{
	// Token: 0x02000114 RID: 276
	[DebuggerStepThrough]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DesignerCategory("code")]
	[Serializable]
	public class ConversationNodeType
	{
		// Token: 0x17000331 RID: 817
		// (get) Token: 0x06000C0A RID: 3082 RVA: 0x00021549 File Offset: 0x0001F749
		// (set) Token: 0x06000C0B RID: 3083 RVA: 0x00021551 File Offset: 0x0001F751
		public string InternetMessageId
		{
			get
			{
				return this.internetMessageIdField;
			}
			set
			{
				this.internetMessageIdField = value;
			}
		}

		// Token: 0x17000332 RID: 818
		// (get) Token: 0x06000C0C RID: 3084 RVA: 0x0002155A File Offset: 0x0001F75A
		// (set) Token: 0x06000C0D RID: 3085 RVA: 0x00021562 File Offset: 0x0001F762
		public string ParentInternetMessageId
		{
			get
			{
				return this.parentInternetMessageIdField;
			}
			set
			{
				this.parentInternetMessageIdField = value;
			}
		}

		// Token: 0x17000333 RID: 819
		// (get) Token: 0x06000C0E RID: 3086 RVA: 0x0002156B File Offset: 0x0001F76B
		// (set) Token: 0x06000C0F RID: 3087 RVA: 0x00021573 File Offset: 0x0001F773
		public NonEmptyArrayOfAllItemsType Items
		{
			get
			{
				return this.itemsField;
			}
			set
			{
				this.itemsField = value;
			}
		}

		// Token: 0x04000894 RID: 2196
		private string internetMessageIdField;

		// Token: 0x04000895 RID: 2197
		private string parentInternetMessageIdField;

		// Token: 0x04000896 RID: 2198
		private NonEmptyArrayOfAllItemsType itemsField;
	}
}
