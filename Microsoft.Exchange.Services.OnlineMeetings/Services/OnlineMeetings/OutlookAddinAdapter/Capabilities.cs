using System;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Services.OnlineMeetings.OutlookAddinAdapter
{
	// Token: 0x020000B9 RID: 185
	[XmlType("Capabilities")]
	[DataContract(Name = "Capabilities")]
	[KnownType(typeof(Capabilities))]
	public class Capabilities
	{
		// Token: 0x17000173 RID: 371
		// (get) Token: 0x0600043A RID: 1082 RVA: 0x0000AC35 File Offset: 0x00008E35
		// (set) Token: 0x0600043B RID: 1083 RVA: 0x0000AC3D File Offset: 0x00008E3D
		[XmlElement("CAAEnabled")]
		[DataMember(Name = "CAAEnabled")]
		public bool CAAEnabled { get; set; }

		// Token: 0x17000174 RID: 372
		// (get) Token: 0x0600043C RID: 1084 RVA: 0x0000AC46 File Offset: 0x00008E46
		// (set) Token: 0x0600043D RID: 1085 RVA: 0x0000AC4E File Offset: 0x00008E4E
		[DataMember(Name = "AnonymousAllowed")]
		[XmlElement("AnonymousAllowed")]
		public bool AnonymousAllowed { get; set; }

		// Token: 0x17000175 RID: 373
		// (get) Token: 0x0600043E RID: 1086 RVA: 0x0000AC57 File Offset: 0x00008E57
		// (set) Token: 0x0600043F RID: 1087 RVA: 0x0000AC5F File Offset: 0x00008E5F
		[DataMember(Name = "PublicMeetingLimit")]
		[XmlElement("PublicMeetingLimit")]
		public int PublicMeetingLimit { get; set; }

		// Token: 0x17000176 RID: 374
		// (get) Token: 0x06000440 RID: 1088 RVA: 0x0000AC68 File Offset: 0x00008E68
		// (set) Token: 0x06000441 RID: 1089 RVA: 0x0000AC70 File Offset: 0x00008E70
		[XmlElement("PublicMeetingDefault")]
		[DataMember(Name = "PublicMeetingDefault")]
		public bool PublicMeetingDefault { get; set; }

		// Token: 0x17000177 RID: 375
		// (get) Token: 0x06000442 RID: 1090 RVA: 0x0000AC79 File Offset: 0x00008E79
		// (set) Token: 0x06000443 RID: 1091 RVA: 0x0000AC81 File Offset: 0x00008E81
		[DataMember(Name = "AutoPromoteAllowed")]
		[XmlElement("AutoPromoteAllowed")]
		public AutoPromoteEnum AutoPromoteAllowed { get; set; }

		// Token: 0x17000178 RID: 376
		// (get) Token: 0x06000444 RID: 1092 RVA: 0x0000AC8A File Offset: 0x00008E8A
		// (set) Token: 0x06000445 RID: 1093 RVA: 0x0000AC92 File Offset: 0x00008E92
		[XmlElement("DefaultAutoPromote")]
		[DataMember(Name = "DefaultAutoPromote")]
		public AutoPromoteEnum DefaultAutoPromote { get; set; }

		// Token: 0x17000179 RID: 377
		// (get) Token: 0x06000446 RID: 1094 RVA: 0x0000AC9B File Offset: 0x00008E9B
		// (set) Token: 0x06000447 RID: 1095 RVA: 0x0000ACA3 File Offset: 0x00008EA3
		[DataMember(Name = "BypassLobbyEnabled")]
		[XmlElement("BypassLobbyEnabled")]
		public bool BypassLobbyEnabled { get; set; }

		// Token: 0x1700017A RID: 378
		// (get) Token: 0x06000448 RID: 1096 RVA: 0x0000ACAC File Offset: 0x00008EAC
		// (set) Token: 0x06000449 RID: 1097 RVA: 0x0000ACB4 File Offset: 0x00008EB4
		[XmlElement("ForgetPinUrl")]
		[DataMember(Name = "ForgetPinUrl")]
		public string ForgetPinUrl { get; set; }

		// Token: 0x1700017B RID: 379
		// (get) Token: 0x0600044A RID: 1098 RVA: 0x0000ACBD File Offset: 0x00008EBD
		// (set) Token: 0x0600044B RID: 1099 RVA: 0x0000ACC5 File Offset: 0x00008EC5
		[DataMember(Name = "LocalPhoneUrl")]
		[XmlElement("LocalPhoneUrl")]
		public string LocalPhoneUrl { get; set; }

		// Token: 0x1700017C RID: 380
		// (get) Token: 0x0600044C RID: 1100 RVA: 0x0000ACCE File Offset: 0x00008ECE
		// (set) Token: 0x0600044D RID: 1101 RVA: 0x0000ACD6 File Offset: 0x00008ED6
		[DataMember(Name = "DefaultAnnouncementEnabled")]
		[XmlElement("DefaultAnnouncementEnabled")]
		public bool DefaultAnnouncementEnabled { get; set; }

		// Token: 0x1700017D RID: 381
		// (get) Token: 0x0600044E RID: 1102 RVA: 0x0000ACDF File Offset: 0x00008EDF
		// (set) Token: 0x0600044F RID: 1103 RVA: 0x0000ACE7 File Offset: 0x00008EE7
		[DataMember(Name = "ACPMCUEnabled")]
		[XmlElement("ACPMCUEnabled")]
		public bool ACPMCUEnabled { get; set; }

		// Token: 0x1700017E RID: 382
		// (get) Token: 0x06000450 RID: 1104 RVA: 0x0000ACF0 File Offset: 0x00008EF0
		// (set) Token: 0x06000451 RID: 1105 RVA: 0x0000ACF8 File Offset: 0x00008EF8
		[XmlArray("Regions")]
		[XmlArrayItem("Region")]
		[DataMember(Name = "Regions")]
		public Region[] Regions { get; set; }

		// Token: 0x1700017F RID: 383
		// (get) Token: 0x06000452 RID: 1106 RVA: 0x0000AD01 File Offset: 0x00008F01
		// (set) Token: 0x06000453 RID: 1107 RVA: 0x0000AD09 File Offset: 0x00008F09
		[XmlElement("custom-invite")]
		[DataMember(Name = "custom-invite")]
		public CustomInvite CustomInvite { get; set; }
	}
}
