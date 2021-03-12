using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.SoapWebClient.EWS
{
	// Token: 0x02000464 RID: 1124
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[Serializable]
	public class GetPersonaType : BaseRequestType
	{
		// Token: 0x04001728 RID: 5928
		public ItemIdType PersonaId;

		// Token: 0x04001729 RID: 5929
		public EmailAddressType EmailAddress;
	}
}
