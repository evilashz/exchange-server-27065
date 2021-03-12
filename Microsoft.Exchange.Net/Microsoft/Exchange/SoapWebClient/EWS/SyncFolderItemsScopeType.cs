using System;
using System.CodeDom.Compiler;
using System.Xml.Serialization;

namespace Microsoft.Exchange.SoapWebClient.EWS
{
	// Token: 0x02000448 RID: 1096
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[Serializable]
	public enum SyncFolderItemsScopeType
	{
		// Token: 0x040016DB RID: 5851
		NormalItems,
		// Token: 0x040016DC RID: 5852
		NormalAndAssociatedItems
	}
}
