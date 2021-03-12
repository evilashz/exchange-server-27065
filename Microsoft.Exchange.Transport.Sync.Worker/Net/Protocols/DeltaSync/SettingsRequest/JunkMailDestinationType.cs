using System;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Net.Protocols.DeltaSync.SettingsRequest
{
	// Token: 0x020000E7 RID: 231
	[Serializable]
	public enum JunkMailDestinationType
	{
		// Token: 0x0400040A RID: 1034
		[XmlEnum(Name = "Immediate Deletion")]
		Immediate_Deletion,
		// Token: 0x0400040B RID: 1035
		[XmlEnum(Name = "Junk Mail")]
		Junk_Mail
	}
}
