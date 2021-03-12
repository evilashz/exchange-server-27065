using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.SoapWebClient.EWS
{
	// Token: 0x0200028E RID: 654
	[DebuggerStepThrough]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DesignerCategory("code")]
	[Serializable]
	public class RuleOperationErrorType
	{
		// Token: 0x040010A7 RID: 4263
		public int OperationIndex;

		// Token: 0x040010A8 RID: 4264
		[XmlArrayItem("Error", IsNullable = false)]
		public RuleValidationErrorType[] ValidationErrors;
	}
}
