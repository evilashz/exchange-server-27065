using System;
using System.CodeDom.Compiler;
using System.Xml.Serialization;

namespace Microsoft.Exchange.EDiscovery.Export.EwsProxy
{
	// Token: 0x02000162 RID: 354
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[Serializable]
	public enum TaskDelegateStateType
	{
		// Token: 0x04000B1D RID: 2845
		NoMatch,
		// Token: 0x04000B1E RID: 2846
		OwnNew,
		// Token: 0x04000B1F RID: 2847
		Owned,
		// Token: 0x04000B20 RID: 2848
		Accepted,
		// Token: 0x04000B21 RID: 2849
		Declined,
		// Token: 0x04000B22 RID: 2850
		Max
	}
}
