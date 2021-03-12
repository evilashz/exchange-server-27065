using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using Microsoft.Exchange.Connections.Eas.Model.Common.Email;
using Microsoft.Exchange.Connections.Eas.Model.Common.WindowsLive;
using Microsoft.Exchange.Connections.Eas.Model.Response.AirSyncBase;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Connections.Eas.Model.Response.AirSync
{
	// Token: 0x020000AA RID: 170
	[XmlType(Namespace = "AirSync", TypeName = "ApplicationData")]
	[ClassAccessLevel(AccessLevel.Implementation)]
	public class ApplicationData
	{
		// Token: 0x1700019F RID: 415
		// (get) Token: 0x06000434 RID: 1076 RVA: 0x0000A807 File Offset: 0x00008A07
		// (set) Token: 0x06000435 RID: 1077 RVA: 0x0000A80F File Offset: 0x00008A0F
		[XmlElement(ElementName = "To", Namespace = "Email")]
		public string To { get; set; }

		// Token: 0x170001A0 RID: 416
		// (get) Token: 0x06000436 RID: 1078 RVA: 0x0000A818 File Offset: 0x00008A18
		// (set) Token: 0x06000437 RID: 1079 RVA: 0x0000A820 File Offset: 0x00008A20
		[XmlElement(ElementName = "From", Namespace = "Email")]
		public string From { get; set; }

		// Token: 0x170001A1 RID: 417
		// (get) Token: 0x06000438 RID: 1080 RVA: 0x0000A829 File Offset: 0x00008A29
		// (set) Token: 0x06000439 RID: 1081 RVA: 0x0000A831 File Offset: 0x00008A31
		[XmlElement(ElementName = "Subject", Namespace = "Email")]
		public string Subject { get; set; }

		// Token: 0x170001A2 RID: 418
		// (get) Token: 0x0600043A RID: 1082 RVA: 0x0000A83A File Offset: 0x00008A3A
		// (set) Token: 0x0600043B RID: 1083 RVA: 0x0000A842 File Offset: 0x00008A42
		[XmlElement(ElementName = "DateReceived", Namespace = "Email")]
		public string DateReceived { get; set; }

		// Token: 0x170001A3 RID: 419
		// (get) Token: 0x0600043C RID: 1084 RVA: 0x0000A84B File Offset: 0x00008A4B
		// (set) Token: 0x0600043D RID: 1085 RVA: 0x0000A853 File Offset: 0x00008A53
		[XmlElement(ElementName = "Importance", Namespace = "Email")]
		public byte Importance { get; set; }

		// Token: 0x170001A4 RID: 420
		// (get) Token: 0x0600043E RID: 1086 RVA: 0x0000A85C File Offset: 0x00008A5C
		// (set) Token: 0x0600043F RID: 1087 RVA: 0x0000A864 File Offset: 0x00008A64
		[XmlElement(ElementName = "Read", Namespace = "Email")]
		public byte? Read { get; set; }

		// Token: 0x170001A5 RID: 421
		// (get) Token: 0x06000440 RID: 1088 RVA: 0x0000A86D File Offset: 0x00008A6D
		// (set) Token: 0x06000441 RID: 1089 RVA: 0x0000A875 File Offset: 0x00008A75
		[XmlElement(ElementName = "Body", Namespace = "AirSyncBase")]
		public Body Body { get; set; }

		// Token: 0x170001A6 RID: 422
		// (get) Token: 0x06000442 RID: 1090 RVA: 0x0000A87E File Offset: 0x00008A7E
		// (set) Token: 0x06000443 RID: 1091 RVA: 0x0000A886 File Offset: 0x00008A86
		[XmlElement(ElementName = "MessageClass", Namespace = "Email")]
		public string MessageClass { get; set; }

		// Token: 0x170001A7 RID: 423
		// (get) Token: 0x06000444 RID: 1092 RVA: 0x0000A88F File Offset: 0x00008A8F
		// (set) Token: 0x06000445 RID: 1093 RVA: 0x0000A897 File Offset: 0x00008A97
		[XmlElement(ElementName = "InternetCPID", Namespace = "Email")]
		public string InternetCpid { get; set; }

		// Token: 0x170001A8 RID: 424
		// (get) Token: 0x06000446 RID: 1094 RVA: 0x0000A8A0 File Offset: 0x00008AA0
		// (set) Token: 0x06000447 RID: 1095 RVA: 0x0000A8A8 File Offset: 0x00008AA8
		[XmlElement(ElementName = "Flag", Namespace = "Email")]
		public Flag Flag { get; set; }

		// Token: 0x170001A9 RID: 425
		// (get) Token: 0x06000448 RID: 1096 RVA: 0x0000A8B1 File Offset: 0x00008AB1
		// (set) Token: 0x06000449 RID: 1097 RVA: 0x0000A8B9 File Offset: 0x00008AB9
		[XmlElement(ElementName = "ConversationId", Namespace = "Email2")]
		public string ConversationId { get; set; }

		// Token: 0x170001AA RID: 426
		// (get) Token: 0x0600044A RID: 1098 RVA: 0x0000A8C2 File Offset: 0x00008AC2
		// (set) Token: 0x0600044B RID: 1099 RVA: 0x0000A8CA File Offset: 0x00008ACA
		[XmlElement(ElementName = "ConversationIndex", Namespace = "Email2")]
		public string ConversationIndex { get; set; }

		// Token: 0x170001AB RID: 427
		// (get) Token: 0x0600044C RID: 1100 RVA: 0x0000A8D3 File Offset: 0x00008AD3
		// (set) Token: 0x0600044D RID: 1101 RVA: 0x0000A8DB File Offset: 0x00008ADB
		[XmlArray(ElementName = "Categories", Namespace = "Email")]
		public List<Category> Categories { get; set; }

		// Token: 0x170001AC RID: 428
		// (get) Token: 0x0600044E RID: 1102 RVA: 0x0000A8E4 File Offset: 0x00008AE4
		// (set) Token: 0x0600044F RID: 1103 RVA: 0x0000A8EC File Offset: 0x00008AEC
		[XmlArray(ElementName = "SystemCategories", Namespace = "WindowsLive")]
		public List<CategoryId> SystemCategories { get; set; }

		// Token: 0x170001AD RID: 429
		// (get) Token: 0x06000450 RID: 1104 RVA: 0x0000A8F5 File Offset: 0x00008AF5
		// (set) Token: 0x06000451 RID: 1105 RVA: 0x0000A8FD File Offset: 0x00008AFD
		[XmlElement(ElementName = "Anniversary", Namespace = "Contacts")]
		public string Anniversary { get; set; }

		// Token: 0x170001AE RID: 430
		// (get) Token: 0x06000452 RID: 1106 RVA: 0x0000A906 File Offset: 0x00008B06
		// (set) Token: 0x06000453 RID: 1107 RVA: 0x0000A90E File Offset: 0x00008B0E
		[XmlElement(ElementName = "AssistantName", Namespace = "Contacts")]
		public string AssistantName { get; set; }

		// Token: 0x170001AF RID: 431
		// (get) Token: 0x06000454 RID: 1108 RVA: 0x0000A917 File Offset: 0x00008B17
		// (set) Token: 0x06000455 RID: 1109 RVA: 0x0000A91F File Offset: 0x00008B1F
		[XmlElement(ElementName = "AssistantPhoneNumber", Namespace = "Contacts")]
		public string AssistantPhoneNumber { get; set; }

		// Token: 0x170001B0 RID: 432
		// (get) Token: 0x06000456 RID: 1110 RVA: 0x0000A928 File Offset: 0x00008B28
		// (set) Token: 0x06000457 RID: 1111 RVA: 0x0000A930 File Offset: 0x00008B30
		[XmlElement(ElementName = "Birthday", Namespace = "Contacts")]
		public string Birthday { get; set; }

		// Token: 0x170001B1 RID: 433
		// (get) Token: 0x06000458 RID: 1112 RVA: 0x0000A939 File Offset: 0x00008B39
		// (set) Token: 0x06000459 RID: 1113 RVA: 0x0000A941 File Offset: 0x00008B41
		[XmlElement(ElementName = "Business2PhoneNumber", Namespace = "Contacts")]
		public string Business2PhoneNumber { get; set; }

		// Token: 0x170001B2 RID: 434
		// (get) Token: 0x0600045A RID: 1114 RVA: 0x0000A94A File Offset: 0x00008B4A
		// (set) Token: 0x0600045B RID: 1115 RVA: 0x0000A952 File Offset: 0x00008B52
		[XmlElement(ElementName = "BusinessAddressCity", Namespace = "Contacts")]
		public string BusinessAddressCity { get; set; }

		// Token: 0x170001B3 RID: 435
		// (get) Token: 0x0600045C RID: 1116 RVA: 0x0000A95B File Offset: 0x00008B5B
		// (set) Token: 0x0600045D RID: 1117 RVA: 0x0000A963 File Offset: 0x00008B63
		[XmlElement(ElementName = "BusinessPhoneNumber", Namespace = "Contacts")]
		public string BusinessPhoneNumber { get; set; }

		// Token: 0x170001B4 RID: 436
		// (get) Token: 0x0600045E RID: 1118 RVA: 0x0000A96C File Offset: 0x00008B6C
		// (set) Token: 0x0600045F RID: 1119 RVA: 0x0000A974 File Offset: 0x00008B74
		[XmlElement(ElementName = "WebPage", Namespace = "Contacts")]
		public string WebPage { get; set; }

		// Token: 0x170001B5 RID: 437
		// (get) Token: 0x06000460 RID: 1120 RVA: 0x0000A97D File Offset: 0x00008B7D
		// (set) Token: 0x06000461 RID: 1121 RVA: 0x0000A985 File Offset: 0x00008B85
		[XmlElement(ElementName = "BusinessAddressCountry", Namespace = "Contacts")]
		public string BusinessAddressCountry { get; set; }

		// Token: 0x170001B6 RID: 438
		// (get) Token: 0x06000462 RID: 1122 RVA: 0x0000A98E File Offset: 0x00008B8E
		// (set) Token: 0x06000463 RID: 1123 RVA: 0x0000A996 File Offset: 0x00008B96
		[XmlElement(ElementName = "Department", Namespace = "Contacts")]
		public string Department { get; set; }

		// Token: 0x170001B7 RID: 439
		// (get) Token: 0x06000464 RID: 1124 RVA: 0x0000A99F File Offset: 0x00008B9F
		// (set) Token: 0x06000465 RID: 1125 RVA: 0x0000A9A7 File Offset: 0x00008BA7
		[XmlElement(ElementName = "Email1Address", Namespace = "Contacts")]
		public string Email1Address { get; set; }

		// Token: 0x170001B8 RID: 440
		// (get) Token: 0x06000466 RID: 1126 RVA: 0x0000A9B0 File Offset: 0x00008BB0
		// (set) Token: 0x06000467 RID: 1127 RVA: 0x0000A9B8 File Offset: 0x00008BB8
		[XmlElement(ElementName = "Email2Address", Namespace = "Contacts")]
		public string Email2Address { get; set; }

		// Token: 0x170001B9 RID: 441
		// (get) Token: 0x06000468 RID: 1128 RVA: 0x0000A9C1 File Offset: 0x00008BC1
		// (set) Token: 0x06000469 RID: 1129 RVA: 0x0000A9C9 File Offset: 0x00008BC9
		[XmlElement(ElementName = "Email3Address", Namespace = "Contacts")]
		public string Email3Address { get; set; }

		// Token: 0x170001BA RID: 442
		// (get) Token: 0x0600046A RID: 1130 RVA: 0x0000A9D2 File Offset: 0x00008BD2
		// (set) Token: 0x0600046B RID: 1131 RVA: 0x0000A9DA File Offset: 0x00008BDA
		[XmlElement(ElementName = "BusinessFaxNumber", Namespace = "Contacts")]
		public string BusinessFaxNumber { get; set; }

		// Token: 0x170001BB RID: 443
		// (get) Token: 0x0600046C RID: 1132 RVA: 0x0000A9E3 File Offset: 0x00008BE3
		// (set) Token: 0x0600046D RID: 1133 RVA: 0x0000A9EB File Offset: 0x00008BEB
		[XmlElement(ElementName = "FileAs", Namespace = "Contacts")]
		public string FileAs { get; set; }

		// Token: 0x170001BC RID: 444
		// (get) Token: 0x0600046E RID: 1134 RVA: 0x0000A9F4 File Offset: 0x00008BF4
		// (set) Token: 0x0600046F RID: 1135 RVA: 0x0000A9FC File Offset: 0x00008BFC
		[XmlElement(ElementName = "Alias", Namespace = "Contacts")]
		public string Alias { get; set; }

		// Token: 0x170001BD RID: 445
		// (get) Token: 0x06000470 RID: 1136 RVA: 0x0000AA05 File Offset: 0x00008C05
		// (set) Token: 0x06000471 RID: 1137 RVA: 0x0000AA0D File Offset: 0x00008C0D
		[XmlElement(ElementName = "WeightedRank", Namespace = "Contacts")]
		public int WeightedRank { get; set; }

		// Token: 0x170001BE RID: 446
		// (get) Token: 0x06000472 RID: 1138 RVA: 0x0000AA16 File Offset: 0x00008C16
		// (set) Token: 0x06000473 RID: 1139 RVA: 0x0000AA1E File Offset: 0x00008C1E
		[XmlElement(ElementName = "FirstName", Namespace = "Contacts")]
		public string FirstName { get; set; }

		// Token: 0x170001BF RID: 447
		// (get) Token: 0x06000474 RID: 1140 RVA: 0x0000AA27 File Offset: 0x00008C27
		// (set) Token: 0x06000475 RID: 1141 RVA: 0x0000AA2F File Offset: 0x00008C2F
		[XmlElement(ElementName = "MiddleName", Namespace = "Contacts")]
		public string MiddleName { get; set; }

		// Token: 0x170001C0 RID: 448
		// (get) Token: 0x06000476 RID: 1142 RVA: 0x0000AA38 File Offset: 0x00008C38
		// (set) Token: 0x06000477 RID: 1143 RVA: 0x0000AA40 File Offset: 0x00008C40
		[XmlElement(ElementName = "HomeAddressCity", Namespace = "Contacts")]
		public string HomeAddressCity { get; set; }

		// Token: 0x170001C1 RID: 449
		// (get) Token: 0x06000478 RID: 1144 RVA: 0x0000AA49 File Offset: 0x00008C49
		// (set) Token: 0x06000479 RID: 1145 RVA: 0x0000AA51 File Offset: 0x00008C51
		[XmlElement(ElementName = "HomeAddressCountry", Namespace = "Contacts")]
		public string HomeAddressCountry { get; set; }

		// Token: 0x170001C2 RID: 450
		// (get) Token: 0x0600047A RID: 1146 RVA: 0x0000AA5A File Offset: 0x00008C5A
		// (set) Token: 0x0600047B RID: 1147 RVA: 0x0000AA62 File Offset: 0x00008C62
		[XmlElement(ElementName = "HomeFaxNumber", Namespace = "Contacts")]
		public string HomeFaxNumber { get; set; }

		// Token: 0x170001C3 RID: 451
		// (get) Token: 0x0600047C RID: 1148 RVA: 0x0000AA6B File Offset: 0x00008C6B
		// (set) Token: 0x0600047D RID: 1149 RVA: 0x0000AA73 File Offset: 0x00008C73
		[XmlElement(ElementName = "HomePhoneNumber", Namespace = "Contacts")]
		public string HomePhoneNumber { get; set; }

		// Token: 0x170001C4 RID: 452
		// (get) Token: 0x0600047E RID: 1150 RVA: 0x0000AA7C File Offset: 0x00008C7C
		// (set) Token: 0x0600047F RID: 1151 RVA: 0x0000AA84 File Offset: 0x00008C84
		[XmlElement(ElementName = "Home2PhoneNumber", Namespace = "Contacts")]
		public string Home2PhoneNumber { get; set; }

		// Token: 0x170001C5 RID: 453
		// (get) Token: 0x06000480 RID: 1152 RVA: 0x0000AA8D File Offset: 0x00008C8D
		// (set) Token: 0x06000481 RID: 1153 RVA: 0x0000AA95 File Offset: 0x00008C95
		[XmlElement(ElementName = "HomeAddressPostalCode", Namespace = "Contacts")]
		public string HomeAddressPostalCode { get; set; }

		// Token: 0x170001C6 RID: 454
		// (get) Token: 0x06000482 RID: 1154 RVA: 0x0000AA9E File Offset: 0x00008C9E
		// (set) Token: 0x06000483 RID: 1155 RVA: 0x0000AAA6 File Offset: 0x00008CA6
		[XmlElement(ElementName = "HomeAddressState", Namespace = "Contacts")]
		public string HomeAddressState { get; set; }

		// Token: 0x170001C7 RID: 455
		// (get) Token: 0x06000484 RID: 1156 RVA: 0x0000AAAF File Offset: 0x00008CAF
		// (set) Token: 0x06000485 RID: 1157 RVA: 0x0000AAB7 File Offset: 0x00008CB7
		[XmlElement(ElementName = "HomeAddressStreet", Namespace = "Contacts")]
		public string HomeAddressStreet { get; set; }

		// Token: 0x170001C8 RID: 456
		// (get) Token: 0x06000486 RID: 1158 RVA: 0x0000AAC0 File Offset: 0x00008CC0
		// (set) Token: 0x06000487 RID: 1159 RVA: 0x0000AAC8 File Offset: 0x00008CC8
		[XmlElement(ElementName = "MobilePhoneNumber", Namespace = "Contacts")]
		public string MobilePhoneNumber { get; set; }

		// Token: 0x170001C9 RID: 457
		// (get) Token: 0x06000488 RID: 1160 RVA: 0x0000AAD1 File Offset: 0x00008CD1
		// (set) Token: 0x06000489 RID: 1161 RVA: 0x0000AAD9 File Offset: 0x00008CD9
		[XmlElement(ElementName = "Suffix", Namespace = "Contacts")]
		public string Suffix { get; set; }

		// Token: 0x170001CA RID: 458
		// (get) Token: 0x0600048A RID: 1162 RVA: 0x0000AAE2 File Offset: 0x00008CE2
		// (set) Token: 0x0600048B RID: 1163 RVA: 0x0000AAEA File Offset: 0x00008CEA
		[XmlElement(ElementName = "CompanyName", Namespace = "Contacts")]
		public string CompanyName { get; set; }

		// Token: 0x170001CB RID: 459
		// (get) Token: 0x0600048C RID: 1164 RVA: 0x0000AAF3 File Offset: 0x00008CF3
		// (set) Token: 0x0600048D RID: 1165 RVA: 0x0000AAFB File Offset: 0x00008CFB
		[XmlElement(ElementName = "OtherAddressCity", Namespace = "Contacts")]
		public string OtherAddressCity { get; set; }

		// Token: 0x170001CC RID: 460
		// (get) Token: 0x0600048E RID: 1166 RVA: 0x0000AB04 File Offset: 0x00008D04
		// (set) Token: 0x0600048F RID: 1167 RVA: 0x0000AB0C File Offset: 0x00008D0C
		[XmlElement(ElementName = "OtherAddressCountry", Namespace = "Contacts")]
		public string OtherAddressCountry { get; set; }

		// Token: 0x170001CD RID: 461
		// (get) Token: 0x06000490 RID: 1168 RVA: 0x0000AB15 File Offset: 0x00008D15
		// (set) Token: 0x06000491 RID: 1169 RVA: 0x0000AB1D File Offset: 0x00008D1D
		[XmlElement(ElementName = "CarPhoneNumber", Namespace = "Contacts")]
		public string CarPhoneNumber { get; set; }

		// Token: 0x170001CE RID: 462
		// (get) Token: 0x06000492 RID: 1170 RVA: 0x0000AB26 File Offset: 0x00008D26
		// (set) Token: 0x06000493 RID: 1171 RVA: 0x0000AB2E File Offset: 0x00008D2E
		[XmlElement(ElementName = "OtherAddressPostalCode", Namespace = "Contacts")]
		public string OtherAddressPostalCode { get; set; }

		// Token: 0x170001CF RID: 463
		// (get) Token: 0x06000494 RID: 1172 RVA: 0x0000AB37 File Offset: 0x00008D37
		// (set) Token: 0x06000495 RID: 1173 RVA: 0x0000AB3F File Offset: 0x00008D3F
		[XmlElement(ElementName = "OtherAddressState", Namespace = "Contacts")]
		public string OtherAddressState { get; set; }

		// Token: 0x170001D0 RID: 464
		// (get) Token: 0x06000496 RID: 1174 RVA: 0x0000AB48 File Offset: 0x00008D48
		// (set) Token: 0x06000497 RID: 1175 RVA: 0x0000AB50 File Offset: 0x00008D50
		[XmlElement(ElementName = "OtherAddressStreet", Namespace = "Contacts")]
		public string OtherAddressStreet { get; set; }

		// Token: 0x170001D1 RID: 465
		// (get) Token: 0x06000498 RID: 1176 RVA: 0x0000AB59 File Offset: 0x00008D59
		// (set) Token: 0x06000499 RID: 1177 RVA: 0x0000AB61 File Offset: 0x00008D61
		[XmlElement(ElementName = "PagerNumber", Namespace = "Contacts")]
		public string PagerNumber { get; set; }

		// Token: 0x170001D2 RID: 466
		// (get) Token: 0x0600049A RID: 1178 RVA: 0x0000AB6A File Offset: 0x00008D6A
		// (set) Token: 0x0600049B RID: 1179 RVA: 0x0000AB72 File Offset: 0x00008D72
		[XmlElement(ElementName = "Title", Namespace = "Contacts")]
		public string Title { get; set; }

		// Token: 0x170001D3 RID: 467
		// (get) Token: 0x0600049C RID: 1180 RVA: 0x0000AB7B File Offset: 0x00008D7B
		// (set) Token: 0x0600049D RID: 1181 RVA: 0x0000AB83 File Offset: 0x00008D83
		[XmlElement(ElementName = "BusinessAddressPostalCode", Namespace = "Contacts")]
		public string BusinessAddressPostalCode { get; set; }

		// Token: 0x170001D4 RID: 468
		// (get) Token: 0x0600049E RID: 1182 RVA: 0x0000AB8C File Offset: 0x00008D8C
		// (set) Token: 0x0600049F RID: 1183 RVA: 0x0000AB94 File Offset: 0x00008D94
		[XmlElement(ElementName = "LastName", Namespace = "Contacts")]
		public string LastName { get; set; }

		// Token: 0x170001D5 RID: 469
		// (get) Token: 0x060004A0 RID: 1184 RVA: 0x0000AB9D File Offset: 0x00008D9D
		// (set) Token: 0x060004A1 RID: 1185 RVA: 0x0000ABA5 File Offset: 0x00008DA5
		[XmlElement(ElementName = "Spouse", Namespace = "Contacts")]
		public string Spouse { get; set; }

		// Token: 0x170001D6 RID: 470
		// (get) Token: 0x060004A2 RID: 1186 RVA: 0x0000ABAE File Offset: 0x00008DAE
		// (set) Token: 0x060004A3 RID: 1187 RVA: 0x0000ABB6 File Offset: 0x00008DB6
		[XmlElement(ElementName = "BusinessAddressState", Namespace = "Contacts")]
		public string BusinessAddressState { get; set; }

		// Token: 0x170001D7 RID: 471
		// (get) Token: 0x060004A4 RID: 1188 RVA: 0x0000ABBF File Offset: 0x00008DBF
		// (set) Token: 0x060004A5 RID: 1189 RVA: 0x0000ABC7 File Offset: 0x00008DC7
		[XmlElement(ElementName = "BusinessAddressStreet", Namespace = "Contacts")]
		public string BusinessAddressStreet { get; set; }

		// Token: 0x170001D8 RID: 472
		// (get) Token: 0x060004A6 RID: 1190 RVA: 0x0000ABD0 File Offset: 0x00008DD0
		// (set) Token: 0x060004A7 RID: 1191 RVA: 0x0000ABD8 File Offset: 0x00008DD8
		[XmlElement(ElementName = "JobTitle", Namespace = "Contacts")]
		public string JobTitle { get; set; }

		// Token: 0x170001D9 RID: 473
		// (get) Token: 0x060004A8 RID: 1192 RVA: 0x0000ABE1 File Offset: 0x00008DE1
		// (set) Token: 0x060004A9 RID: 1193 RVA: 0x0000ABE9 File Offset: 0x00008DE9
		[XmlElement(ElementName = "YomiFirstName", Namespace = "Contacts")]
		public string YomiFirstName { get; set; }

		// Token: 0x170001DA RID: 474
		// (get) Token: 0x060004AA RID: 1194 RVA: 0x0000ABF2 File Offset: 0x00008DF2
		// (set) Token: 0x060004AB RID: 1195 RVA: 0x0000ABFA File Offset: 0x00008DFA
		[XmlElement(ElementName = "YomiLastName", Namespace = "Contacts")]
		public string YomiLastName { get; set; }

		// Token: 0x170001DB RID: 475
		// (get) Token: 0x060004AC RID: 1196 RVA: 0x0000AC03 File Offset: 0x00008E03
		// (set) Token: 0x060004AD RID: 1197 RVA: 0x0000AC0B File Offset: 0x00008E0B
		[XmlElement(ElementName = "YomiCompanyName", Namespace = "Contacts")]
		public string YomiCompanyName { get; set; }

		// Token: 0x170001DC RID: 476
		// (get) Token: 0x060004AE RID: 1198 RVA: 0x0000AC14 File Offset: 0x00008E14
		// (set) Token: 0x060004AF RID: 1199 RVA: 0x0000AC1C File Offset: 0x00008E1C
		[XmlElement(ElementName = "OfficeLocation", Namespace = "Contacts")]
		public string OfficeLocation { get; set; }

		// Token: 0x170001DD RID: 477
		// (get) Token: 0x060004B0 RID: 1200 RVA: 0x0000AC25 File Offset: 0x00008E25
		// (set) Token: 0x060004B1 RID: 1201 RVA: 0x0000AC2D File Offset: 0x00008E2D
		[XmlElement(ElementName = "RadioPhoneNumber", Namespace = "Contacts")]
		public string RadioPhoneNumber { get; set; }

		// Token: 0x170001DE RID: 478
		// (get) Token: 0x060004B2 RID: 1202 RVA: 0x0000AC36 File Offset: 0x00008E36
		// (set) Token: 0x060004B3 RID: 1203 RVA: 0x0000AC3E File Offset: 0x00008E3E
		[XmlElement(ElementName = "Picture", Namespace = "Contacts")]
		public string Picture { get; set; }

		// Token: 0x170001DF RID: 479
		// (get) Token: 0x060004B4 RID: 1204 RVA: 0x0000AC47 File Offset: 0x00008E47
		// (set) Token: 0x060004B5 RID: 1205 RVA: 0x0000AC4F File Offset: 0x00008E4F
		[XmlElement(ElementName = "CustomerId", Namespace = "Contacts2")]
		public string CustomerId { get; set; }

		// Token: 0x170001E0 RID: 480
		// (get) Token: 0x060004B6 RID: 1206 RVA: 0x0000AC58 File Offset: 0x00008E58
		// (set) Token: 0x060004B7 RID: 1207 RVA: 0x0000AC60 File Offset: 0x00008E60
		[XmlElement(ElementName = "GovernmentId", Namespace = "Contacts2")]
		public string GovernmentId { get; set; }

		// Token: 0x170001E1 RID: 481
		// (get) Token: 0x060004B8 RID: 1208 RVA: 0x0000AC69 File Offset: 0x00008E69
		// (set) Token: 0x060004B9 RID: 1209 RVA: 0x0000AC71 File Offset: 0x00008E71
		[XmlElement(ElementName = "IMAddress", Namespace = "Contacts2")]
		public string IMAddress { get; set; }

		// Token: 0x170001E2 RID: 482
		// (get) Token: 0x060004BA RID: 1210 RVA: 0x0000AC7A File Offset: 0x00008E7A
		// (set) Token: 0x060004BB RID: 1211 RVA: 0x0000AC82 File Offset: 0x00008E82
		[XmlElement(ElementName = "IMAddress2", Namespace = "Contacts2")]
		public string IMAddress2 { get; set; }

		// Token: 0x170001E3 RID: 483
		// (get) Token: 0x060004BC RID: 1212 RVA: 0x0000AC8B File Offset: 0x00008E8B
		// (set) Token: 0x060004BD RID: 1213 RVA: 0x0000AC93 File Offset: 0x00008E93
		[XmlElement(ElementName = "IMAddress3", Namespace = "Contacts2")]
		public string IMAddress3 { get; set; }

		// Token: 0x170001E4 RID: 484
		// (get) Token: 0x060004BE RID: 1214 RVA: 0x0000AC9C File Offset: 0x00008E9C
		// (set) Token: 0x060004BF RID: 1215 RVA: 0x0000ACA4 File Offset: 0x00008EA4
		[XmlElement(ElementName = "ManagerName", Namespace = "Contacts2")]
		public string ManagerName { get; set; }

		// Token: 0x170001E5 RID: 485
		// (get) Token: 0x060004C0 RID: 1216 RVA: 0x0000ACAD File Offset: 0x00008EAD
		// (set) Token: 0x060004C1 RID: 1217 RVA: 0x0000ACB5 File Offset: 0x00008EB5
		[XmlElement(ElementName = "CompanyMainPhone", Namespace = "Contacts2")]
		public string CompanyMainPhone { get; set; }

		// Token: 0x170001E6 RID: 486
		// (get) Token: 0x060004C2 RID: 1218 RVA: 0x0000ACBE File Offset: 0x00008EBE
		// (set) Token: 0x060004C3 RID: 1219 RVA: 0x0000ACC6 File Offset: 0x00008EC6
		[XmlElement(ElementName = "AccountName", Namespace = "Contacts2")]
		public string AccountName { get; set; }

		// Token: 0x170001E7 RID: 487
		// (get) Token: 0x060004C4 RID: 1220 RVA: 0x0000ACCF File Offset: 0x00008ECF
		// (set) Token: 0x060004C5 RID: 1221 RVA: 0x0000ACD7 File Offset: 0x00008ED7
		[XmlElement(ElementName = "NickName", Namespace = "Contacts2")]
		public string NickName { get; set; }

		// Token: 0x170001E8 RID: 488
		// (get) Token: 0x060004C6 RID: 1222 RVA: 0x0000ACE0 File Offset: 0x00008EE0
		// (set) Token: 0x060004C7 RID: 1223 RVA: 0x0000ACE8 File Offset: 0x00008EE8
		[XmlElement(ElementName = "MMS", Namespace = "Contacts2")]
		public string MMS { get; set; }
	}
}
