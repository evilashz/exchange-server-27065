using System;
using System.CodeDom.Compiler;
using System.Xml.Serialization;

namespace Microsoft.Exchange.EDiscovery.Export.EwsProxy
{
	// Token: 0x020001EE RID: 494
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[Serializable]
	public enum DelegateFolderPermissionLevelType
	{
		// Token: 0x04000DEC RID: 3564
		None,
		// Token: 0x04000DED RID: 3565
		Editor,
		// Token: 0x04000DEE RID: 3566
		Reviewer,
		// Token: 0x04000DEF RID: 3567
		Author,
		// Token: 0x04000DF0 RID: 3568
		Custom
	}
}
