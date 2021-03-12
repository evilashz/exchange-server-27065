using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Web.Services.Protocols;
using System.Xml.Serialization;

namespace Microsoft.Exchange.SoapWebClient.EWS
{
	// Token: 0x02000493 RID: 1171
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[XmlRoot("ManagementRole", Namespace = "http://schemas.microsoft.com/exchange/services/2006/types", IsNullable = false)]
	[DebuggerStepThrough]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[DesignerCategory("code")]
	[Serializable]
	public class ManagementRoleType : SoapHeader
	{
		// Token: 0x040017CA RID: 6090
		[XmlArrayItem("Role", IsNullable = false)]
		public string[] UserRoles;

		// Token: 0x040017CB RID: 6091
		[XmlArrayItem("Role", IsNullable = false)]
		public string[] ApplicationRoles;
	}
}
