using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.SoapWebClient.EWS
{
	// Token: 0x02000442 RID: 1090
	[DebuggerStepThrough]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
	[DesignerCategory("code")]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[Serializable]
	public class GetDelegateType : BaseDelegateType
	{
		// Token: 0x040016C8 RID: 5832
		[XmlArrayItem("UserId", Namespace = "http://schemas.microsoft.com/exchange/services/2006/types", IsNullable = false)]
		public UserIdType[] UserIds;

		// Token: 0x040016C9 RID: 5833
		[XmlAttribute]
		public bool IncludePermissions;
	}
}
