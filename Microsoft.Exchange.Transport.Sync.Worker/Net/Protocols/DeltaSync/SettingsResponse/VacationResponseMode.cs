using System;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Net.Protocols.DeltaSync.SettingsResponse
{
	// Token: 0x02000131 RID: 305
	[Serializable]
	public enum VacationResponseMode
	{
		// Token: 0x040004FF RID: 1279
		[XmlEnum(Name = "NoVacationResponse")]
		NoVacationResponse,
		// Token: 0x04000500 RID: 1280
		[XmlEnum(Name = "OncePerSender")]
		OncePerSender,
		// Token: 0x04000501 RID: 1281
		[XmlEnum(Name = "OncePerContact")]
		OncePerContact
	}
}
