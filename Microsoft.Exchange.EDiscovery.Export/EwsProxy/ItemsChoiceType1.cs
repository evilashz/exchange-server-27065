using System;
using System.CodeDom.Compiler;
using System.Xml.Serialization;

namespace Microsoft.Exchange.EDiscovery.Export.EwsProxy
{
	// Token: 0x0200022C RID: 556
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types", IncludeInSchema = false)]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[Serializable]
	public enum ItemsChoiceType1
	{
		// Token: 0x04000EAD RID: 3757
		Create,
		// Token: 0x04000EAE RID: 3758
		Delete,
		// Token: 0x04000EAF RID: 3759
		Update
	}
}
