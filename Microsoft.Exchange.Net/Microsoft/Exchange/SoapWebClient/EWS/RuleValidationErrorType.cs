using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.SoapWebClient.EWS
{
	// Token: 0x0200028F RID: 655
	[DebuggerStepThrough]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[DesignerCategory("code")]
	[Serializable]
	public class RuleValidationErrorType
	{
		// Token: 0x040010A9 RID: 4265
		public RuleFieldURIType FieldURI;

		// Token: 0x040010AA RID: 4266
		public RuleValidationErrorCodeType ErrorCode;

		// Token: 0x040010AB RID: 4267
		public string ErrorMessage;

		// Token: 0x040010AC RID: 4268
		public string FieldValue;
	}
}
