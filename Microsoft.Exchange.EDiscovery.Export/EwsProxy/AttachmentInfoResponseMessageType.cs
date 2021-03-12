using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.EDiscovery.Export.EwsProxy
{
	// Token: 0x02000262 RID: 610
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DesignerCategory("code")]
	[DebuggerStepThrough]
	[Serializable]
	public class AttachmentInfoResponseMessageType : ResponseMessageType
	{
		// Token: 0x17000826 RID: 2086
		// (get) Token: 0x06001706 RID: 5894 RVA: 0x0002721E File Offset: 0x0002541E
		// (set) Token: 0x06001707 RID: 5895 RVA: 0x00027226 File Offset: 0x00025426
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

		// Token: 0x04000F9E RID: 3998
		private AttachmentType[] attachmentsField;
	}
}
