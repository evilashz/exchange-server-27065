using System;
using System.CodeDom.Compiler;
using System.Xml.Serialization;

namespace Microsoft.Exchange.EDiscovery.Export.EwsProxy
{
	// Token: 0x0200021C RID: 540
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[Serializable]
	public enum ContainmentComparisonType
	{
		// Token: 0x04000E98 RID: 3736
		Exact,
		// Token: 0x04000E99 RID: 3737
		IgnoreCase,
		// Token: 0x04000E9A RID: 3738
		IgnoreNonSpacingCharacters,
		// Token: 0x04000E9B RID: 3739
		Loose,
		// Token: 0x04000E9C RID: 3740
		IgnoreCaseAndNonSpacingCharacters,
		// Token: 0x04000E9D RID: 3741
		LooseAndIgnoreCase,
		// Token: 0x04000E9E RID: 3742
		LooseAndIgnoreNonSpace,
		// Token: 0x04000E9F RID: 3743
		LooseAndIgnoreCaseAndIgnoreNonSpace
	}
}
