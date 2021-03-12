using System;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Net.Protocols.DeltaSync.SettingsRequest
{
	// Token: 0x020000EB RID: 235
	[Serializable]
	public enum ParentalControlStatusType
	{
		// Token: 0x0400041A RID: 1050
		[XmlEnum(Name = "None")]
		None,
		// Token: 0x0400041B RID: 1051
		[XmlEnum(Name = "FullAccess")]
		FullAccess,
		// Token: 0x0400041C RID: 1052
		[XmlEnum(Name = "RestrictedAccess")]
		RestrictedAccess,
		// Token: 0x0400041D RID: 1053
		[XmlEnum(Name = "NoAccess")]
		NoAccess
	}
}
