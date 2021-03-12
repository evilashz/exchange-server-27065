using System;
using System.CodeDom.Compiler;
using System.Xml.Serialization;

namespace Microsoft.Exchange.SoapWebClient.EWS
{
	// Token: 0x02000469 RID: 1129
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[Serializable]
	public enum ViewFilterType
	{
		// Token: 0x04001741 RID: 5953
		All,
		// Token: 0x04001742 RID: 5954
		Flagged,
		// Token: 0x04001743 RID: 5955
		HasAttachment,
		// Token: 0x04001744 RID: 5956
		ToOrCcMe,
		// Token: 0x04001745 RID: 5957
		Unread,
		// Token: 0x04001746 RID: 5958
		TaskActive,
		// Token: 0x04001747 RID: 5959
		TaskOverdue,
		// Token: 0x04001748 RID: 5960
		TaskCompleted,
		// Token: 0x04001749 RID: 5961
		NoClutter,
		// Token: 0x0400174A RID: 5962
		Clutter
	}
}
