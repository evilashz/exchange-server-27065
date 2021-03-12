using System;
using System.CodeDom.Compiler;
using System.Xml.Serialization;

namespace Microsoft.Exchange.EDiscovery.Export.EwsProxy
{
	// Token: 0x02000367 RID: 871
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[Serializable]
	public enum SyncFolderItemsScopeType
	{
		// Token: 0x04001289 RID: 4745
		NormalItems,
		// Token: 0x0400128A RID: 4746
		NormalAndAssociatedItems
	}
}
