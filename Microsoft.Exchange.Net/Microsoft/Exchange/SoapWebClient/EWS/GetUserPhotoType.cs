using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.SoapWebClient.EWS
{
	// Token: 0x020003FD RID: 1021
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[Serializable]
	public class GetUserPhotoType : BaseRequestType
	{
		// Token: 0x040015D1 RID: 5585
		public string Email;

		// Token: 0x040015D2 RID: 5586
		public UserPhotoSizeType SizeRequested;
	}
}
