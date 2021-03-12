using System;
using System.CodeDom.Compiler;
using System.Xml.Serialization;

namespace Microsoft.Exchange.EDiscovery.Export.EwsProxy
{
	// Token: 0x020001F6 RID: 502
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[Serializable]
	public enum IdFormatType
	{
		// Token: 0x04000DFB RID: 3579
		EwsLegacyId,
		// Token: 0x04000DFC RID: 3580
		EwsId,
		// Token: 0x04000DFD RID: 3581
		EntryId,
		// Token: 0x04000DFE RID: 3582
		HexEntryId,
		// Token: 0x04000DFF RID: 3583
		StoreId,
		// Token: 0x04000E00 RID: 3584
		OwaId
	}
}
