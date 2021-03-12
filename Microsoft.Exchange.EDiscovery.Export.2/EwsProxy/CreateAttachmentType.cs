using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.EDiscovery.Export.EwsProxy
{
	// Token: 0x0200037E RID: 894
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
	[DesignerCategory("code")]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DebuggerStepThrough]
	[Serializable]
	public class CreateAttachmentType : BaseRequestType
	{
		// Token: 0x17000A4B RID: 2635
		// (get) Token: 0x06001C46 RID: 7238 RVA: 0x00029E4A File Offset: 0x0002804A
		// (set) Token: 0x06001C47 RID: 7239 RVA: 0x00029E52 File Offset: 0x00028052
		public ItemIdType ParentItemId
		{
			get
			{
				return this.parentItemIdField;
			}
			set
			{
				this.parentItemIdField = value;
			}
		}

		// Token: 0x17000A4C RID: 2636
		// (get) Token: 0x06001C48 RID: 7240 RVA: 0x00029E5B File Offset: 0x0002805B
		// (set) Token: 0x06001C49 RID: 7241 RVA: 0x00029E63 File Offset: 0x00028063
		[XmlArrayItem("FileAttachment", typeof(FileAttachmentType), Namespace = "http://schemas.microsoft.com/exchange/services/2006/types", IsNullable = false)]
		[XmlArrayItem("ItemAttachment", typeof(ItemAttachmentType), Namespace = "http://schemas.microsoft.com/exchange/services/2006/types", IsNullable = false)]
		public AttachmentType[] Attachments
		{
			get
			{
				return this.attachmentsField;
			}
			set
			{
				this.attachmentsField = value;
			}
		}

		// Token: 0x040012BA RID: 4794
		private ItemIdType parentItemIdField;

		// Token: 0x040012BB RID: 4795
		private AttachmentType[] attachmentsField;
	}
}
