using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.SoapWebClient.EWS
{
	// Token: 0x0200045D RID: 1117
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
	[Serializable]
	public class GetAttachmentType : BaseRequestType
	{
		// Token: 0x04001709 RID: 5897
		public AttachmentResponseShapeType AttachmentShape;

		// Token: 0x0400170A RID: 5898
		[XmlArrayItem("AttachmentId", Namespace = "http://schemas.microsoft.com/exchange/services/2006/types", IsNullable = false)]
		public RequestAttachmentIdType[] AttachmentIds;
	}
}
