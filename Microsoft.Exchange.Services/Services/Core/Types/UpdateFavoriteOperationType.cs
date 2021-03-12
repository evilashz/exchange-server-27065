using System;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x020005CC RID: 1484
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[Serializable]
	public enum UpdateFavoriteOperationType
	{
		// Token: 0x04001AE2 RID: 6882
		Add,
		// Token: 0x04001AE3 RID: 6883
		Remove,
		// Token: 0x04001AE4 RID: 6884
		Move,
		// Token: 0x04001AE5 RID: 6885
		Rename
	}
}
