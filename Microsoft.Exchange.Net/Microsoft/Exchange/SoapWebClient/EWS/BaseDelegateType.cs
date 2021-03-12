using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.SoapWebClient.EWS
{
	// Token: 0x0200043E RID: 1086
	[DesignerCategory("code")]
	[DebuggerStepThrough]
	[XmlInclude(typeof(UpdateDelegateType))]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
	[XmlInclude(typeof(RemoveDelegateType))]
	[XmlInclude(typeof(AddDelegateType))]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[XmlInclude(typeof(GetDelegateType))]
	[Serializable]
	public abstract class BaseDelegateType : BaseRequestType
	{
		// Token: 0x040016C0 RID: 5824
		public EmailAddressType Mailbox;
	}
}
