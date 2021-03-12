using System;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x0200079A RID: 1946
	[XmlType(TypeName = "IdFormatType", Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[Serializable]
	public enum IdFormat
	{
		// Token: 0x0400209C RID: 8348
		EwsLegacyId,
		// Token: 0x0400209D RID: 8349
		EwsId,
		// Token: 0x0400209E RID: 8350
		EntryId,
		// Token: 0x0400209F RID: 8351
		HexEntryId,
		// Token: 0x040020A0 RID: 8352
		StoreId,
		// Token: 0x040020A1 RID: 8353
		OwaId
	}
}
