using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Services.OnlineMeetings.OutlookAddinAdapter
{
	// Token: 0x020000B6 RID: 182
	[XmlType("acpInformation")]
	[KnownType(typeof(AcpInformation))]
	[DataContract(Name = "acpInformation")]
	public class AcpInformation
	{
		// Token: 0x1700016A RID: 362
		// (get) Token: 0x06000424 RID: 1060 RVA: 0x0000AAC4 File Offset: 0x00008CC4
		// (set) Token: 0x06000425 RID: 1061 RVA: 0x0000AACC File Offset: 0x00008CCC
		[XmlAttribute("default")]
		[DataMember(Name = "default", EmitDefaultValue = false)]
		public bool IsDefault { get; set; }

		// Token: 0x1700016B RID: 363
		// (get) Token: 0x06000426 RID: 1062 RVA: 0x0000AAD5 File Offset: 0x00008CD5
		// (set) Token: 0x06000427 RID: 1063 RVA: 0x0000AADD File Offset: 0x00008CDD
		[XmlElement("tollNumber")]
		[DataMember(Name = "TollNumber", EmitDefaultValue = false)]
		public string TollNumber { get; set; }

		// Token: 0x1700016C RID: 364
		// (get) Token: 0x06000428 RID: 1064 RVA: 0x0000AAE6 File Offset: 0x00008CE6
		// (set) Token: 0x06000429 RID: 1065 RVA: 0x0000AAEE File Offset: 0x00008CEE
		[DataMember(Name = "TollFreeNumber", EmitDefaultValue = false)]
		[XmlElement("tollFreeNumber")]
		public string[] TollFreeNumber { get; set; }

		// Token: 0x1700016D RID: 365
		// (get) Token: 0x0600042A RID: 1066 RVA: 0x0000AAF7 File Offset: 0x00008CF7
		// (set) Token: 0x0600042B RID: 1067 RVA: 0x0000AAFF File Offset: 0x00008CFF
		[XmlElement("participantPassCode")]
		[DataMember(Name = "ParticipantPassCode", EmitDefaultValue = true)]
		public string ParticipantPassCode { get; set; }

		// Token: 0x1700016E RID: 366
		// (get) Token: 0x0600042C RID: 1068 RVA: 0x0000AB08 File Offset: 0x00008D08
		// (set) Token: 0x0600042D RID: 1069 RVA: 0x0000AB10 File Offset: 0x00008D10
		[XmlElement("domain")]
		[DataMember(Name = "Domain", EmitDefaultValue = true)]
		public string Domain { get; set; }

		// Token: 0x1700016F RID: 367
		// (get) Token: 0x0600042E RID: 1070 RVA: 0x0000AB19 File Offset: 0x00008D19
		// (set) Token: 0x0600042F RID: 1071 RVA: 0x0000AB21 File Offset: 0x00008D21
		[DataMember(Name = "name", EmitDefaultValue = false)]
		[XmlElement("name")]
		public string Name { get; set; }

		// Token: 0x17000170 RID: 368
		// (get) Token: 0x06000430 RID: 1072 RVA: 0x0000AB2A File Offset: 0x00008D2A
		// (set) Token: 0x06000431 RID: 1073 RVA: 0x0000AB32 File Offset: 0x00008D32
		[XmlElement("url")]
		[DataMember(Name = "Url", EmitDefaultValue = true)]
		public string Url { get; set; }

		// Token: 0x06000432 RID: 1074 RVA: 0x0000AB3C File Offset: 0x00008D3C
		internal static AcpInformation[] ConvertFrom(DialInInformation dialIn)
		{
			Collection<AcpInformation> collection = new Collection<AcpInformation>();
			if (dialIn != null && dialIn.IsAudioConferenceProviderEnabled)
			{
				collection.Add(new AcpInformation
				{
					Url = dialIn.ExternalDirectoryUri,
					TollFreeNumber = dialIn.TollFreeNumbers,
					TollNumber = dialIn.TollNumber,
					ParticipantPassCode = dialIn.ParticipantPassCode
				});
			}
			return collection.ToArray<AcpInformation>();
		}
	}
}
