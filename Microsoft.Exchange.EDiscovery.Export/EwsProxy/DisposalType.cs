using System;
using System.CodeDom.Compiler;
using System.Xml.Serialization;

namespace Microsoft.Exchange.EDiscovery.Export.EwsProxy
{
	// Token: 0x020002E3 RID: 739
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[Serializable]
	public enum DisposalType
	{
		// Token: 0x040010F6 RID: 4342
		HardDelete,
		// Token: 0x040010F7 RID: 4343
		SoftDelete,
		// Token: 0x040010F8 RID: 4344
		MoveToDeletedItems
	}
}
