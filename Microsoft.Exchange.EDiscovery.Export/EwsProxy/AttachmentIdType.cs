using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.EDiscovery.Export.EwsProxy
{
	// Token: 0x020000B8 RID: 184
	[DebuggerStepThrough]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DesignerCategory("code")]
	[Serializable]
	public class AttachmentIdType : RequestAttachmentIdType
	{
		// Token: 0x170001F6 RID: 502
		// (get) Token: 0x06000947 RID: 2375 RVA: 0x0001FDF6 File Offset: 0x0001DFF6
		// (set) Token: 0x06000948 RID: 2376 RVA: 0x0001FDFE File Offset: 0x0001DFFE
		[XmlAttribute]
		public string RootItemId
		{
			get
			{
				return this.rootItemIdField;
			}
			set
			{
				this.rootItemIdField = value;
			}
		}

		// Token: 0x170001F7 RID: 503
		// (get) Token: 0x06000949 RID: 2377 RVA: 0x0001FE07 File Offset: 0x0001E007
		// (set) Token: 0x0600094A RID: 2378 RVA: 0x0001FE0F File Offset: 0x0001E00F
		[XmlAttribute]
		public string RootItemChangeKey
		{
			get
			{
				return this.rootItemChangeKeyField;
			}
			set
			{
				this.rootItemChangeKeyField = value;
			}
		}

		// Token: 0x0400055A RID: 1370
		private string rootItemIdField;

		// Token: 0x0400055B RID: 1371
		private string rootItemChangeKeyField;
	}
}
