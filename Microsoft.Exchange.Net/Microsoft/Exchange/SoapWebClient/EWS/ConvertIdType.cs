using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.SoapWebClient.EWS
{
	// Token: 0x02000443 RID: 1091
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[Serializable]
	public class ConvertIdType : BaseRequestType
	{
		// Token: 0x040016CA RID: 5834
		[XmlArrayItem("AlternateId", typeof(AlternateIdType), Namespace = "http://schemas.microsoft.com/exchange/services/2006/types", IsNullable = false)]
		[XmlArrayItem("AlternatePublicFolderItemId", typeof(AlternatePublicFolderItemIdType), Namespace = "http://schemas.microsoft.com/exchange/services/2006/types", IsNullable = false)]
		[XmlArrayItem("AlternatePublicFolderId", typeof(AlternatePublicFolderIdType), Namespace = "http://schemas.microsoft.com/exchange/services/2006/types", IsNullable = false)]
		public AlternateIdBaseType[] SourceIds;

		// Token: 0x040016CB RID: 5835
		[XmlAttribute]
		public IdFormatType DestinationFormat;
	}
}
