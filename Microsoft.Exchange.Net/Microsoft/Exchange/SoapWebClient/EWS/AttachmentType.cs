using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.SoapWebClient.EWS
{
	// Token: 0x02000207 RID: 519
	[DebuggerStepThrough]
	[XmlInclude(typeof(ReferenceAttachmentType))]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DesignerCategory("code")]
	[XmlInclude(typeof(FileAttachmentType))]
	[XmlInclude(typeof(ItemAttachmentType))]
	[Serializable]
	public class AttachmentType
	{
		// Token: 0x04000D77 RID: 3447
		public AttachmentIdType AttachmentId;

		// Token: 0x04000D78 RID: 3448
		public string Name;

		// Token: 0x04000D79 RID: 3449
		public string ContentType;

		// Token: 0x04000D7A RID: 3450
		public string ContentId;

		// Token: 0x04000D7B RID: 3451
		public string ContentLocation;

		// Token: 0x04000D7C RID: 3452
		public int Size;

		// Token: 0x04000D7D RID: 3453
		[XmlIgnore]
		public bool SizeSpecified;

		// Token: 0x04000D7E RID: 3454
		public DateTime LastModifiedTime;

		// Token: 0x04000D7F RID: 3455
		[XmlIgnore]
		public bool LastModifiedTimeSpecified;

		// Token: 0x04000D80 RID: 3456
		public bool IsInline;

		// Token: 0x04000D81 RID: 3457
		[XmlIgnore]
		public bool IsInlineSpecified;
	}
}
