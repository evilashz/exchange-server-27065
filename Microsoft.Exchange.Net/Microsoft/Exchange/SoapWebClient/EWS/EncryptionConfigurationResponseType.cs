using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.SoapWebClient.EWS
{
	// Token: 0x020001ED RID: 493
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[Serializable]
	public class EncryptionConfigurationResponseType : ResponseMessageType
	{
		// Token: 0x04000CBC RID: 3260
		public string ImageBase64;

		// Token: 0x04000CBD RID: 3261
		public string EmailText;

		// Token: 0x04000CBE RID: 3262
		public string PortalText;

		// Token: 0x04000CBF RID: 3263
		public string DisclaimerText;

		// Token: 0x04000CC0 RID: 3264
		public bool OTPEnabled;

		// Token: 0x04000CC1 RID: 3265
		[XmlIgnore]
		public bool OTPEnabledSpecified;
	}
}
