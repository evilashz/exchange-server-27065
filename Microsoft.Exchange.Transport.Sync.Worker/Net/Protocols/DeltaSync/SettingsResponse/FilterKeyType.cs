using System;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Net.Protocols.DeltaSync.SettingsResponse
{
	// Token: 0x02000134 RID: 308
	[Serializable]
	public enum FilterKeyType
	{
		// Token: 0x0400050C RID: 1292
		[XmlEnum(Name = "Subject")]
		Subject,
		// Token: 0x0400050D RID: 1293
		[XmlEnum(Name = "From Name")]
		From_Name,
		// Token: 0x0400050E RID: 1294
		[XmlEnum(Name = "From Address")]
		From_Address,
		// Token: 0x0400050F RID: 1295
		[XmlEnum(Name = "To or CC Line")]
		To_or_CC_Line
	}
}
