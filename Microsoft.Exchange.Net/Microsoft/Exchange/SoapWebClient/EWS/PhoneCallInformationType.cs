using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.SoapWebClient.EWS
{
	// Token: 0x02000323 RID: 803
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[Serializable]
	public class PhoneCallInformationType
	{
		// Token: 0x0400133F RID: 4927
		public PhoneCallStateType PhoneCallState;

		// Token: 0x04001340 RID: 4928
		public ConnectionFailureCauseType ConnectionFailureCause;

		// Token: 0x04001341 RID: 4929
		public string SIPResponseText;

		// Token: 0x04001342 RID: 4930
		public int SIPResponseCode;

		// Token: 0x04001343 RID: 4931
		[XmlIgnore]
		public bool SIPResponseCodeSpecified;
	}
}
