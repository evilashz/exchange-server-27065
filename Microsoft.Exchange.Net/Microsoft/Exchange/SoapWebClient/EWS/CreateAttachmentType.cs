using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.SoapWebClient.EWS
{
	// Token: 0x0200045F RID: 1119
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DebuggerStepThrough]
	[Serializable]
	public class CreateAttachmentType : BaseRequestType
	{
		// Token: 0x0400170C RID: 5900
		public ItemIdType ParentItemId;

		// Token: 0x0400170D RID: 5901
		[XmlArrayItem("ItemAttachment", typeof(ItemAttachmentType), Namespace = "http://schemas.microsoft.com/exchange/services/2006/types", IsNullable = false)]
		[XmlArrayItem("FileAttachment", typeof(FileAttachmentType), Namespace = "http://schemas.microsoft.com/exchange/services/2006/types", IsNullable = false)]
		public AttachmentType[] Attachments;
	}
}
