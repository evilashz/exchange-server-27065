using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.SoapWebClient.EWS
{
	// Token: 0x02000414 RID: 1044
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
	[DesignerCategory("code")]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DebuggerStepThrough]
	[Serializable]
	public class SetEncryptionConfigurationType : BaseRequestType
	{
		// Token: 0x04001601 RID: 5633
		public string ImageBase64;

		// Token: 0x04001602 RID: 5634
		public string EmailText;

		// Token: 0x04001603 RID: 5635
		public string PortalText;

		// Token: 0x04001604 RID: 5636
		public string DisclaimerText;

		// Token: 0x04001605 RID: 5637
		public bool OTPEnabled;

		// Token: 0x04001606 RID: 5638
		[XmlIgnore]
		public bool OTPEnabledSpecified;
	}
}
