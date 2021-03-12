using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.SoapWebClient.EWS
{
	// Token: 0x02000440 RID: 1088
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
	[Serializable]
	public class RemoveDelegateType : BaseDelegateType
	{
		// Token: 0x040016C4 RID: 5828
		[XmlArrayItem("UserId", Namespace = "http://schemas.microsoft.com/exchange/services/2006/types", IsNullable = false)]
		public UserIdType[] UserIds;
	}
}
