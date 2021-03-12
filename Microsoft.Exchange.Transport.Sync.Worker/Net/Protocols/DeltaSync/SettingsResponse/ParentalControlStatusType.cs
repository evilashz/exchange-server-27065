using System;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Net.Protocols.DeltaSync.SettingsResponse
{
	// Token: 0x02000132 RID: 306
	[Serializable]
	public enum ParentalControlStatusType
	{
		// Token: 0x04000503 RID: 1283
		[XmlEnum(Name = "None")]
		None,
		// Token: 0x04000504 RID: 1284
		[XmlEnum(Name = "FullAccess")]
		FullAccess,
		// Token: 0x04000505 RID: 1285
		[XmlEnum(Name = "RestrictedAccess")]
		RestrictedAccess,
		// Token: 0x04000506 RID: 1286
		[XmlEnum(Name = "NoAccess")]
		NoAccess
	}
}
