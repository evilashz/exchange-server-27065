using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.SoapWebClient.EWS
{
	// Token: 0x0200034D RID: 845
	[DebuggerStepThrough]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
	[DesignerCategory("code")]
	[Serializable]
	public class GetUserOofSettingsResponse
	{
		// Token: 0x04001406 RID: 5126
		public ResponseMessageType ResponseMessage;

		// Token: 0x04001407 RID: 5127
		[XmlElement(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
		public UserOofSettings OofSettings;

		// Token: 0x04001408 RID: 5128
		public ExternalAudience AllowExternalOof;

		// Token: 0x04001409 RID: 5129
		[XmlIgnore]
		public bool AllowExternalOofSpecified;
	}
}
