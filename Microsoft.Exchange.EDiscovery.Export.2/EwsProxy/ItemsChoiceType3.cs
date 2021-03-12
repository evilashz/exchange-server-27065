using System;
using System.CodeDom.Compiler;
using System.Xml.Serialization;

namespace Microsoft.Exchange.EDiscovery.Export.EwsProxy
{
	// Token: 0x020001CB RID: 459
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types", IncludeInSchema = false)]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[Serializable]
	public enum ItemsChoiceType3
	{
		// Token: 0x04000D84 RID: 3460
		AllInternal,
		// Token: 0x04000D85 RID: 3461
		And,
		// Token: 0x04000D86 RID: 3462
		RecipientIs,
		// Token: 0x04000D87 RID: 3463
		SenderDepartments,
		// Token: 0x04000D88 RID: 3464
		True
	}
}
