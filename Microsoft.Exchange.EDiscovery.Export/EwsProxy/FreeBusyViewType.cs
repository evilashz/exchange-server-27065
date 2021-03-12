using System;
using System.CodeDom.Compiler;
using System.Xml.Serialization;

namespace Microsoft.Exchange.EDiscovery.Export.EwsProxy
{
	// Token: 0x02000280 RID: 640
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[Flags]
	[Serializable]
	public enum FreeBusyViewType
	{
		// Token: 0x04000FF7 RID: 4087
		None = 1,
		// Token: 0x04000FF8 RID: 4088
		MergedOnly = 2,
		// Token: 0x04000FF9 RID: 4089
		FreeBusy = 4,
		// Token: 0x04000FFA RID: 4090
		FreeBusyMerged = 8,
		// Token: 0x04000FFB RID: 4091
		Detailed = 16,
		// Token: 0x04000FFC RID: 4092
		DetailedMerged = 32
	}
}
