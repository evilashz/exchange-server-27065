using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.SoapWebClient.EWS
{
	// Token: 0x02000201 RID: 513
	[DesignerCategory("code")]
	[DebuggerStepThrough]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[Serializable]
	public class VotingOptionDataType
	{
		// Token: 0x04000D5E RID: 3422
		public string DisplayName;

		// Token: 0x04000D5F RID: 3423
		public SendPromptType SendPrompt;

		// Token: 0x04000D60 RID: 3424
		[XmlIgnore]
		public bool SendPromptSpecified;
	}
}
