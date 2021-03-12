using System;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Net.Protocols.DeltaSync.SettingsRequest
{
	// Token: 0x020000EC RID: 236
	[Serializable]
	public enum AccountStatusType
	{
		// Token: 0x0400041F RID: 1055
		[XmlEnum(Name = "OK")]
		OK,
		// Token: 0x04000420 RID: 1056
		[XmlEnum(Name = "Blocked")]
		Blocked,
		// Token: 0x04000421 RID: 1057
		[XmlEnum(Name = "RequiresHIP")]
		RequiresHIP
	}
}
