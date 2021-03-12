using System;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x02000684 RID: 1668
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[Serializable]
	public enum UnifiedGroupsSortType
	{
		// Token: 0x04001CEA RID: 7402
		None,
		// Token: 0x04001CEB RID: 7403
		DisplayName,
		// Token: 0x04001CEC RID: 7404
		JoinDate,
		// Token: 0x04001CED RID: 7405
		FavoriteDate
	}
}
