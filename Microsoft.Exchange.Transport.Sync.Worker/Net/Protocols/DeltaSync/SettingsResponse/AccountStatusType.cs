using System;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Net.Protocols.DeltaSync.SettingsResponse
{
	// Token: 0x02000133 RID: 307
	[Serializable]
	public enum AccountStatusType
	{
		// Token: 0x04000508 RID: 1288
		[XmlEnum(Name = "OK")]
		OK,
		// Token: 0x04000509 RID: 1289
		[XmlEnum(Name = "Blocked")]
		Blocked,
		// Token: 0x0400050A RID: 1290
		[XmlEnum(Name = "RequiresHIP")]
		RequiresHIP
	}
}
