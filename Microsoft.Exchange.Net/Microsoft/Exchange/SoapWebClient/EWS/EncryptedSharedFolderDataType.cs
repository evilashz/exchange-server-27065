using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml;
using System.Xml.Serialization;

namespace Microsoft.Exchange.SoapWebClient.EWS
{
	// Token: 0x020002C6 RID: 710
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[DebuggerStepThrough]
	[Serializable]
	public class EncryptedSharedFolderDataType
	{
		// Token: 0x04001215 RID: 4629
		public XmlElement Token;

		// Token: 0x04001216 RID: 4630
		public XmlElement Data;
	}
}
