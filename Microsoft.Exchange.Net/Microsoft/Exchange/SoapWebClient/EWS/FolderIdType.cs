using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.SoapWebClient.EWS
{
	// Token: 0x020001D2 RID: 466
	[DesignerCategory("code")]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[DebuggerStepThrough]
	[Serializable]
	public class FolderIdType : BaseFolderIdType
	{
		// Token: 0x04000C51 RID: 3153
		[XmlAttribute]
		public string Id;

		// Token: 0x04000C52 RID: 3154
		[XmlAttribute]
		public string ChangeKey;
	}
}
