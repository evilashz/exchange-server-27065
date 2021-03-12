using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.SoapWebClient.EWS
{
	// Token: 0x020002D6 RID: 726
	[XmlInclude(typeof(AlternatePublicFolderIdType))]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[XmlInclude(typeof(AlternatePublicFolderItemIdType))]
	[XmlInclude(typeof(AlternateIdType))]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[Serializable]
	public abstract class AlternateIdBaseType
	{
		// Token: 0x0400124B RID: 4683
		[XmlAttribute]
		public IdFormatType Format;
	}
}
