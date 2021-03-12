using System;
using System.CodeDom.Compiler;
using System.Xml.Serialization;

namespace Microsoft.Exchange.EDiscovery.Export.EwsProxy
{
	// Token: 0x02000353 RID: 851
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[Serializable]
	public enum TeamMailboxLifecycleStateType
	{
		// Token: 0x04001254 RID: 4692
		Active,
		// Token: 0x04001255 RID: 4693
		Closed,
		// Token: 0x04001256 RID: 4694
		Unlinked,
		// Token: 0x04001257 RID: 4695
		PendingDelete
	}
}
