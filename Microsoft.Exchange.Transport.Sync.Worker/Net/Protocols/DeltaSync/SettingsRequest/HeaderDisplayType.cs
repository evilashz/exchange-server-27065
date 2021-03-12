using System;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Net.Protocols.DeltaSync.SettingsRequest
{
	// Token: 0x020000E9 RID: 233
	[Serializable]
	public enum HeaderDisplayType
	{
		// Token: 0x04000411 RID: 1041
		[XmlEnum(Name = "No Header")]
		No_Header,
		// Token: 0x04000412 RID: 1042
		[XmlEnum(Name = "Basic")]
		Basic,
		// Token: 0x04000413 RID: 1043
		[XmlEnum(Name = "Full")]
		Full,
		// Token: 0x04000414 RID: 1044
		[XmlEnum(Name = "Advanced")]
		Advanced
	}
}
