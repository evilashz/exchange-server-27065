using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.SoapWebClient.EWS
{
	// Token: 0x020002E7 RID: 743
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[Serializable]
	public class CalendarPermissionSetType
	{
		// Token: 0x04001286 RID: 4742
		[XmlArrayItem("CalendarPermission", IsNullable = false)]
		public CalendarPermissionType[] CalendarPermissions;

		// Token: 0x04001287 RID: 4743
		[XmlArrayItem("UnknownEntry", IsNullable = false)]
		public string[] UnknownEntries;
	}
}
