using System;
using System.CodeDom.Compiler;
using System.Xml.Serialization;

namespace Microsoft.Exchange.EDiscovery.Export.EwsProxy
{
	// Token: 0x02000388 RID: 904
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[Serializable]
	public enum ViewFilterType
	{
		// Token: 0x040012EF RID: 4847
		All,
		// Token: 0x040012F0 RID: 4848
		Flagged,
		// Token: 0x040012F1 RID: 4849
		HasAttachment,
		// Token: 0x040012F2 RID: 4850
		ToOrCcMe,
		// Token: 0x040012F3 RID: 4851
		Unread,
		// Token: 0x040012F4 RID: 4852
		TaskActive,
		// Token: 0x040012F5 RID: 4853
		TaskOverdue,
		// Token: 0x040012F6 RID: 4854
		TaskCompleted,
		// Token: 0x040012F7 RID: 4855
		NoClutter,
		// Token: 0x040012F8 RID: 4856
		Clutter
	}
}
