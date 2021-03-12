using System;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Services.OnlineMeetings.OutlookAddinAdapter
{
	// Token: 0x020000C7 RID: 199
	[XmlType("CAA")]
	[DataContract(Name = "CAA")]
	[KnownType(typeof(CaaAudioType))]
	public class CaaAudioType
	{
		// Token: 0x170001AE RID: 430
		// (get) Token: 0x060004D2 RID: 1234 RVA: 0x0000C00F File Offset: 0x0000A20F
		// (set) Token: 0x060004D3 RID: 1235 RVA: 0x0000C017 File Offset: 0x0000A217
		[DataMember(Name = "pstnId", EmitDefaultValue = true)]
		[XmlElement("pstnId")]
		public string PstnId { get; set; }

		// Token: 0x170001AF RID: 431
		// (get) Token: 0x060004D4 RID: 1236 RVA: 0x0000C020 File Offset: 0x0000A220
		// (set) Token: 0x060004D5 RID: 1237 RVA: 0x0000C028 File Offset: 0x0000A228
		[XmlElement("region")]
		[DataMember(Name = "Region", EmitDefaultValue = true)]
		public CaaRegion Region { get; set; }

		// Token: 0x170001B0 RID: 432
		// (get) Token: 0x060004D6 RID: 1238 RVA: 0x0000C031 File Offset: 0x0000A231
		// (set) Token: 0x060004D7 RID: 1239 RVA: 0x0000C039 File Offset: 0x0000A239
		[XmlElement("BypassLobby")]
		[DataMember(Name = "BypassLobby", EmitDefaultValue = true)]
		public bool BypassLobby { get; set; }

		// Token: 0x170001B1 RID: 433
		// (get) Token: 0x060004D8 RID: 1240 RVA: 0x0000C042 File Offset: 0x0000A242
		// (set) Token: 0x060004D9 RID: 1241 RVA: 0x0000C04A File Offset: 0x0000A24A
		[DataMember(Name = "AnnouncementEnabled", EmitDefaultValue = true)]
		[XmlElement("AnnouncementEnabled")]
		public bool AnnouncementEnabled { get; set; }
	}
}
