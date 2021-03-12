using System;
using System.CodeDom.Compiler;
using System.Xml.Serialization;

namespace Microsoft.Exchange.EDiscovery.Export.EwsProxy
{
	// Token: 0x020001CC RID: 460
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types", IncludeInSchema = false)]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[Serializable]
	public enum ItemChoiceType
	{
		// Token: 0x04000D8A RID: 3466
		AllInternal,
		// Token: 0x04000D8B RID: 3467
		And,
		// Token: 0x04000D8C RID: 3468
		RecipientIs,
		// Token: 0x04000D8D RID: 3469
		SenderDepartments,
		// Token: 0x04000D8E RID: 3470
		True
	}
}
