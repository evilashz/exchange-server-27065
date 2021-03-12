using System;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Services.OnlineMeetings.OutlookAddinAdapter
{
	// Token: 0x020000BC RID: 188
	[KnownType(typeof(Inband))]
	[XmlRoot("Inband")]
	[DataContract(Name = "Inband")]
	public class Inband
	{
		// Token: 0x1700018C RID: 396
		// (get) Token: 0x06000478 RID: 1144 RVA: 0x0000B206 File Offset: 0x00009406
		// (set) Token: 0x06000479 RID: 1145 RVA: 0x0000B20E File Offset: 0x0000940E
		[XmlArray("ACPs")]
		[XmlArrayItem("acpInformation")]
		[DataMember(Name = "ACPs", EmitDefaultValue = true)]
		public AcpInformation[] ACPs { get; set; }

		// Token: 0x1700018D RID: 397
		// (get) Token: 0x0600047A RID: 1146 RVA: 0x0000B217 File Offset: 0x00009417
		// (set) Token: 0x0600047B RID: 1147 RVA: 0x0000B21F File Offset: 0x0000941F
		[DataMember(Name = "MaxMeetingSize", EmitDefaultValue = true)]
		[XmlElement("MaxMeetingSize")]
		public int MaxMeetingSize { get; set; }

		// Token: 0x1700018E RID: 398
		// (get) Token: 0x0600047C RID: 1148 RVA: 0x0000B228 File Offset: 0x00009428
		// (set) Token: 0x0600047D RID: 1149 RVA: 0x0000B230 File Offset: 0x00009430
		[DataMember(Name = "AudioEnabled", EmitDefaultValue = true)]
		[XmlElement("AudioEnabled")]
		public bool AudioEnabled { get; set; }

		// Token: 0x1700018F RID: 399
		// (get) Token: 0x0600047E RID: 1150 RVA: 0x0000B239 File Offset: 0x00009439
		// (set) Token: 0x0600047F RID: 1151 RVA: 0x0000B241 File Offset: 0x00009441
		[DataMember(Name = "EnableEnterpriseCustomizedHelp", EmitDefaultValue = true)]
		[XmlElement("EnableEnterpriseCustomizedHelp")]
		public bool EnableEnterpriseCustomizedHelp { get; set; }

		// Token: 0x17000190 RID: 400
		// (get) Token: 0x06000480 RID: 1152 RVA: 0x0000B24A File Offset: 0x0000944A
		// (set) Token: 0x06000481 RID: 1153 RVA: 0x0000B252 File Offset: 0x00009452
		[DataMember(Name = "CustomizedHelpUrl", EmitDefaultValue = true)]
		[XmlElement("CustomizedHelpUrl")]
		public string CustomizedHelpUrl { get; set; }
	}
}
