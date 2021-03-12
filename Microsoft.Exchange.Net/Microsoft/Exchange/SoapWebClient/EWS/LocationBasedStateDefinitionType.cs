using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.SoapWebClient.EWS
{
	// Token: 0x0200039C RID: 924
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[Serializable]
	public class LocationBasedStateDefinitionType : BaseCalendarItemStateDefinitionType
	{
		// Token: 0x040014A0 RID: 5280
		public string OrganizerLocation;

		// Token: 0x040014A1 RID: 5281
		public string AttendeeLocation;
	}
}
