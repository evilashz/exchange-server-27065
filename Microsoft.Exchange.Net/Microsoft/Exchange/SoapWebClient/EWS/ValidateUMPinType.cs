using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.SoapWebClient.EWS
{
	// Token: 0x020003F3 RID: 1011
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[Serializable]
	public class ValidateUMPinType : BaseRequestType
	{
		// Token: 0x040015B3 RID: 5555
		public PinInfoType PinInfo;

		// Token: 0x040015B4 RID: 5556
		public string UserUMMailboxPolicyGuid;
	}
}
