using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.SoapWebClient.EWS
{
	// Token: 0x020002E9 RID: 745
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[Serializable]
	public class CalendarPermissionType : BasePermissionType
	{
		// Token: 0x04001297 RID: 4759
		public CalendarPermissionReadAccessType ReadItems;

		// Token: 0x04001298 RID: 4760
		[XmlIgnore]
		public bool ReadItemsSpecified;

		// Token: 0x04001299 RID: 4761
		public CalendarPermissionLevelType CalendarPermissionLevel;
	}
}
