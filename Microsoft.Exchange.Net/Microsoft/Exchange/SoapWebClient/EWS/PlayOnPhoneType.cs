using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.SoapWebClient.EWS
{
	// Token: 0x02000457 RID: 1111
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
	[DebuggerStepThrough]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[Serializable]
	public class PlayOnPhoneType : BaseRequestType
	{
		// Token: 0x040016F7 RID: 5879
		public ItemIdType ItemId;

		// Token: 0x040016F8 RID: 5880
		public string DialString;
	}
}
