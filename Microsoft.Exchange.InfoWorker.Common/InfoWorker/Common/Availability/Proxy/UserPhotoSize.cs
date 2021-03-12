using System;
using System.Xml.Serialization;

namespace Microsoft.Exchange.InfoWorker.Common.Availability.Proxy
{
	// Token: 0x0200013E RID: 318
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[Serializable]
	public enum UserPhotoSize
	{
		// Token: 0x040006C7 RID: 1735
		HR48x48,
		// Token: 0x040006C8 RID: 1736
		HR64x64,
		// Token: 0x040006C9 RID: 1737
		HR96x96,
		// Token: 0x040006CA RID: 1738
		HR120x120,
		// Token: 0x040006CB RID: 1739
		HR240x240,
		// Token: 0x040006CC RID: 1740
		HR360x360,
		// Token: 0x040006CD RID: 1741
		HR432x432,
		// Token: 0x040006CE RID: 1742
		HR504x504,
		// Token: 0x040006CF RID: 1743
		HR648x648
	}
}
