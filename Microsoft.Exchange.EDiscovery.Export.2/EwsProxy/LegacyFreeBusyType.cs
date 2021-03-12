using System;
using System.CodeDom.Compiler;
using System.Xml.Serialization;

namespace Microsoft.Exchange.EDiscovery.Export.EwsProxy
{
	// Token: 0x0200012B RID: 299
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[Serializable]
	public enum LegacyFreeBusyType
	{
		// Token: 0x04000984 RID: 2436
		Free,
		// Token: 0x04000985 RID: 2437
		Tentative,
		// Token: 0x04000986 RID: 2438
		Busy,
		// Token: 0x04000987 RID: 2439
		OOF,
		// Token: 0x04000988 RID: 2440
		WorkingElsewhere,
		// Token: 0x04000989 RID: 2441
		NoData
	}
}
