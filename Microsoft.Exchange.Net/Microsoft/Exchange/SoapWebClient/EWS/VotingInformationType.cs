using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.SoapWebClient.EWS
{
	// Token: 0x02000200 RID: 512
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[Serializable]
	public class VotingInformationType
	{
		// Token: 0x04000D5C RID: 3420
		[XmlArrayItem("VotingOptionData", IsNullable = false)]
		public VotingOptionDataType[] UserOptions;

		// Token: 0x04000D5D RID: 3421
		public string VotingResponse;
	}
}
