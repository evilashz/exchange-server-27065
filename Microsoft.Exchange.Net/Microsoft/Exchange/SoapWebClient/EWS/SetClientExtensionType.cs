using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.SoapWebClient.EWS
{
	// Token: 0x02000415 RID: 1045
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DebuggerStepThrough]
	[Serializable]
	public class SetClientExtensionType : BaseRequestType
	{
		// Token: 0x04001607 RID: 5639
		[XmlArrayItem("Action", Namespace = "http://schemas.microsoft.com/exchange/services/2006/types", IsNullable = false)]
		public SetClientExtensionActionType[] Actions;
	}
}
