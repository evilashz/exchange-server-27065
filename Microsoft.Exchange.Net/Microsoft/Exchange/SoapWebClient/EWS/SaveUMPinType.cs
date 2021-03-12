using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.SoapWebClient.EWS
{
	// Token: 0x020003F2 RID: 1010
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DebuggerStepThrough]
	[Serializable]
	public class SaveUMPinType : BaseRequestType
	{
		// Token: 0x040015B1 RID: 5553
		public PinInfoType PinInfo;

		// Token: 0x040015B2 RID: 5554
		public string UserUMMailboxPolicyGuid;
	}
}
