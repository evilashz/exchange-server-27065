using System;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Services.OnlineMeetings.OutlookAddinAdapter
{
	// Token: 0x020000C4 RID: 196
	[XmlType("ACP")]
	[DataContract(Name = "ACP")]
	[KnownType(typeof(AcpAudioType))]
	public class AcpAudioType
	{
		// Token: 0x170001A8 RID: 424
		// (get) Token: 0x060004C3 RID: 1219 RVA: 0x0000BEBB File Offset: 0x0000A0BB
		// (set) Token: 0x060004C4 RID: 1220 RVA: 0x0000BEC3 File Offset: 0x0000A0C3
		[DataMember(Name = "Domain", EmitDefaultValue = true)]
		[XmlElement("Domain")]
		public string Domain { get; set; }

		// Token: 0x170001A9 RID: 425
		// (get) Token: 0x060004C5 RID: 1221 RVA: 0x0000BECC File Offset: 0x0000A0CC
		// (set) Token: 0x060004C6 RID: 1222 RVA: 0x0000BED4 File Offset: 0x0000A0D4
		[XmlElement("ACPMCUEnabled")]
		[DataMember(Name = "ACPMCUEnabled", EmitDefaultValue = true)]
		public bool AcpMcuEnabled { get; set; }
	}
}
