using System;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Data.ApplicationLogic.Extension
{
	// Token: 0x020000F5 RID: 245
	[XmlType(TypeName = "DisableReasonType", Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[Serializable]
	public enum DisableReasonType
	{
		// Token: 0x040004CE RID: 1230
		NotDisabled,
		// Token: 0x040004CF RID: 1231
		NoError,
		// Token: 0x040004D0 RID: 1232
		NoReason,
		// Token: 0x040004D1 RID: 1233
		OutlookClientPerformance,
		// Token: 0x040004D2 RID: 1234
		OWAClientPerformance,
		// Token: 0x040004D3 RID: 1235
		MobileClientPerformance
	}
}
