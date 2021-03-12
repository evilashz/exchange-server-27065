using System;
using System.CodeDom.Compiler;
using System.Xml.Serialization;

namespace Microsoft.Exchange.EDiscovery.Export.EwsProxy
{
	// Token: 0x02000205 RID: 517
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[Serializable]
	public enum CalendarPermissionReadAccessType
	{
		// Token: 0x04000E30 RID: 3632
		None,
		// Token: 0x04000E31 RID: 3633
		TimeOnly,
		// Token: 0x04000E32 RID: 3634
		TimeAndSubjectAndLocation,
		// Token: 0x04000E33 RID: 3635
		FullDetails
	}
}
