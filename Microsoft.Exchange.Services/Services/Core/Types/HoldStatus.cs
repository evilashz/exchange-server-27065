using System;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x020007EF RID: 2031
	[XmlType(TypeName = "HoldStatusType", Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[Serializable]
	public enum HoldStatus
	{
		// Token: 0x040020C8 RID: 8392
		NotOnHold,
		// Token: 0x040020C9 RID: 8393
		Pending,
		// Token: 0x040020CA RID: 8394
		OnHold,
		// Token: 0x040020CB RID: 8395
		PartialHold,
		// Token: 0x040020CC RID: 8396
		Failed
	}
}
