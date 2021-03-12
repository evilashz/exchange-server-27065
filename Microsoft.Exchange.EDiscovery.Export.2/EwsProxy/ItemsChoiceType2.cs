using System;
using System.CodeDom.Compiler;
using System.Xml.Serialization;

namespace Microsoft.Exchange.EDiscovery.Export.EwsProxy
{
	// Token: 0x020001FF RID: 511
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types", IncludeInSchema = false)]
	[Serializable]
	public enum ItemsChoiceType2
	{
		// Token: 0x04000E12 RID: 3602
		Create,
		// Token: 0x04000E13 RID: 3603
		Delete,
		// Token: 0x04000E14 RID: 3604
		ReadFlagChange,
		// Token: 0x04000E15 RID: 3605
		Update
	}
}
