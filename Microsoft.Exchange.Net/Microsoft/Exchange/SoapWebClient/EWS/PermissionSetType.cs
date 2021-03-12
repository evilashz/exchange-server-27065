using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.SoapWebClient.EWS
{
	// Token: 0x020002F1 RID: 753
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[Serializable]
	public class PermissionSetType
	{
		// Token: 0x040012D0 RID: 4816
		[XmlArrayItem("Permission", IsNullable = false)]
		public PermissionType[] Permissions;

		// Token: 0x040012D1 RID: 4817
		[XmlArrayItem("UnknownEntry", IsNullable = false)]
		public string[] UnknownEntries;
	}
}
