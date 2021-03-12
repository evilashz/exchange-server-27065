using System;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x02000685 RID: 1669
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[Serializable]
	public enum UnifiedGroupsFilterType
	{
		// Token: 0x04001CEF RID: 7407
		All,
		// Token: 0x04001CF0 RID: 7408
		Favorites,
		// Token: 0x04001CF1 RID: 7409
		ExcludeFavorites
	}
}
