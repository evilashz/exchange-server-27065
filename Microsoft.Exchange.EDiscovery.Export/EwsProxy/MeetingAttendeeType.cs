using System;
using System.CodeDom.Compiler;
using System.Xml.Serialization;

namespace Microsoft.Exchange.EDiscovery.Export.EwsProxy
{
	// Token: 0x020002D0 RID: 720
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[Serializable]
	public enum MeetingAttendeeType
	{
		// Token: 0x0400108C RID: 4236
		Organizer,
		// Token: 0x0400108D RID: 4237
		Required,
		// Token: 0x0400108E RID: 4238
		Optional,
		// Token: 0x0400108F RID: 4239
		Room,
		// Token: 0x04001090 RID: 4240
		Resource
	}
}
