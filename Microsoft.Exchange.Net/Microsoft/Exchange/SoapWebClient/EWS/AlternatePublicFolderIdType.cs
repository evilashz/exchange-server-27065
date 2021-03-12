using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.SoapWebClient.EWS
{
	// Token: 0x020002D8 RID: 728
	[DebuggerStepThrough]
	[XmlInclude(typeof(AlternatePublicFolderItemIdType))]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[DesignerCategory("code")]
	[Serializable]
	public class AlternatePublicFolderIdType : AlternateIdBaseType
	{
		// Token: 0x04001253 RID: 4691
		[XmlAttribute]
		public string FolderId;
	}
}
