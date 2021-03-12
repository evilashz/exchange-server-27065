using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.SoapWebClient.EWS
{
	// Token: 0x020001C2 RID: 450
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[Serializable]
	public class PathToExceptionFieldType : BasePathToElementType
	{
		// Token: 0x04000A68 RID: 2664
		[XmlAttribute]
		public ExceptionPropertyURIType FieldURI;
	}
}
