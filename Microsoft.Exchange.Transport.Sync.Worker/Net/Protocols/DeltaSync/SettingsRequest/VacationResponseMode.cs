using System;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Net.Protocols.DeltaSync.SettingsRequest
{
	// Token: 0x020000ED RID: 237
	[Serializable]
	public enum VacationResponseMode
	{
		// Token: 0x04000423 RID: 1059
		[XmlEnum(Name = "NoVacationResponse")]
		NoVacationResponse,
		// Token: 0x04000424 RID: 1060
		[XmlEnum(Name = "OncePerSender")]
		OncePerSender,
		// Token: 0x04000425 RID: 1061
		[XmlEnum(Name = "OncePerContact")]
		OncePerContact
	}
}
