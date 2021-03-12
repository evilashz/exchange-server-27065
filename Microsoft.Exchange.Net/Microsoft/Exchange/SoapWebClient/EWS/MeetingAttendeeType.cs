using System;
using System.CodeDom.Compiler;
using System.Xml.Serialization;

namespace Microsoft.Exchange.SoapWebClient.EWS
{
	// Token: 0x020003B1 RID: 945
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[Serializable]
	public enum MeetingAttendeeType
	{
		// Token: 0x040014DE RID: 5342
		Organizer,
		// Token: 0x040014DF RID: 5343
		Required,
		// Token: 0x040014E0 RID: 5344
		Optional,
		// Token: 0x040014E1 RID: 5345
		Room,
		// Token: 0x040014E2 RID: 5346
		Resource
	}
}
