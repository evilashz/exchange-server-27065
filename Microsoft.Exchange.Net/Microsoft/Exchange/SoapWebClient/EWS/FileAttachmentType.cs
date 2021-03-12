using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.SoapWebClient.EWS
{
	// Token: 0x02000208 RID: 520
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[DebuggerStepThrough]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[Serializable]
	public class FileAttachmentType : AttachmentType
	{
		// Token: 0x04000D82 RID: 3458
		public bool IsContactPhoto;

		// Token: 0x04000D83 RID: 3459
		[XmlIgnore]
		public bool IsContactPhotoSpecified;

		// Token: 0x04000D84 RID: 3460
		[XmlElement(DataType = "base64Binary")]
		public byte[] Content;
	}
}
