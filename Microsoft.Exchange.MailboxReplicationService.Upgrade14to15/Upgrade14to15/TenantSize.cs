using System;
using Microsoft.Exchange.Data;

namespace Microsoft.Exchange.MailboxReplicationService.Upgrade14to15
{
	// Token: 0x020000A8 RID: 168
	internal struct TenantSize
	{
		// Token: 0x170001CF RID: 463
		// (get) Token: 0x0600049D RID: 1181 RVA: 0x000084F0 File Offset: 0x000066F0
		// (set) Token: 0x0600049E RID: 1182 RVA: 0x000084F8 File Offset: 0x000066F8
		public Guid ExternalDirectoryOrganizationId { get; set; }

		// Token: 0x170001D0 RID: 464
		// (get) Token: 0x0600049F RID: 1183 RVA: 0x00008501 File Offset: 0x00006701
		// (set) Token: 0x060004A0 RID: 1184 RVA: 0x00008509 File Offset: 0x00006709
		public string Name { get; set; }

		// Token: 0x170001D1 RID: 465
		// (get) Token: 0x060004A1 RID: 1185 RVA: 0x00008512 File Offset: 0x00006712
		// (set) Token: 0x060004A2 RID: 1186 RVA: 0x0000851A File Offset: 0x0000671A
		public string[] Constraints { get; set; }

		// Token: 0x170001D2 RID: 466
		// (get) Token: 0x060004A3 RID: 1187 RVA: 0x00008523 File Offset: 0x00006723
		// (set) Token: 0x060004A4 RID: 1188 RVA: 0x0000852B File Offset: 0x0000672B
		public bool? UpgradeConstraintsDisabled { get; set; }

		// Token: 0x170001D3 RID: 467
		// (get) Token: 0x060004A5 RID: 1189 RVA: 0x00008534 File Offset: 0x00006734
		// (set) Token: 0x060004A6 RID: 1190 RVA: 0x0000853C File Offset: 0x0000673C
		public int? UpgradeUnitsOverride { get; set; }

		// Token: 0x170001D4 RID: 468
		// (get) Token: 0x060004A7 RID: 1191 RVA: 0x00008545 File Offset: 0x00006745
		// (set) Token: 0x060004A8 RID: 1192 RVA: 0x0000854D File Offset: 0x0000674D
		public string ServicePlan { get; set; }

		// Token: 0x170001D5 RID: 469
		// (get) Token: 0x060004A9 RID: 1193 RVA: 0x00008556 File Offset: 0x00006756
		// (set) Token: 0x060004AA RID: 1194 RVA: 0x0000855E File Offset: 0x0000675E
		public string ProgramId { get; set; }

		// Token: 0x170001D6 RID: 470
		// (get) Token: 0x060004AB RID: 1195 RVA: 0x00008567 File Offset: 0x00006767
		// (set) Token: 0x060004AC RID: 1196 RVA: 0x0000856F File Offset: 0x0000676F
		public string OfferId { get; set; }

		// Token: 0x170001D7 RID: 471
		// (get) Token: 0x060004AD RID: 1197 RVA: 0x00008578 File Offset: 0x00006778
		// (set) Token: 0x060004AE RID: 1198 RVA: 0x00008580 File Offset: 0x00006780
		public ExchangeObjectVersion AdminDisplayVersion { get; set; }

		// Token: 0x170001D8 RID: 472
		// (get) Token: 0x060004AF RID: 1199 RVA: 0x00008589 File Offset: 0x00006789
		// (set) Token: 0x060004B0 RID: 1200 RVA: 0x00008591 File Offset: 0x00006791
		public bool IsUpgradingOrganization { get; set; }

		// Token: 0x170001D9 RID: 473
		// (get) Token: 0x060004B1 RID: 1201 RVA: 0x0000859A File Offset: 0x0000679A
		// (set) Token: 0x060004B2 RID: 1202 RVA: 0x000085A2 File Offset: 0x000067A2
		public bool IsPilotingOrganization { get; set; }

		// Token: 0x170001DA RID: 474
		// (get) Token: 0x060004B3 RID: 1203 RVA: 0x000085AB File Offset: 0x000067AB
		// (set) Token: 0x060004B4 RID: 1204 RVA: 0x000085B3 File Offset: 0x000067B3
		public int E14PrimaryMbxCount { get; set; }

		// Token: 0x170001DB RID: 475
		// (get) Token: 0x060004B5 RID: 1205 RVA: 0x000085BC File Offset: 0x000067BC
		// (set) Token: 0x060004B6 RID: 1206 RVA: 0x000085C4 File Offset: 0x000067C4
		public double E14PrimaryMbxSize { get; set; }

		// Token: 0x170001DC RID: 476
		// (get) Token: 0x060004B7 RID: 1207 RVA: 0x000085CD File Offset: 0x000067CD
		// (set) Token: 0x060004B8 RID: 1208 RVA: 0x000085D5 File Offset: 0x000067D5
		public int E14ArchiveMbxCount { get; set; }

		// Token: 0x170001DD RID: 477
		// (get) Token: 0x060004B9 RID: 1209 RVA: 0x000085DE File Offset: 0x000067DE
		// (set) Token: 0x060004BA RID: 1210 RVA: 0x000085E6 File Offset: 0x000067E6
		public double E14ArchiveMbxSize { get; set; }

		// Token: 0x170001DE RID: 478
		// (get) Token: 0x060004BB RID: 1211 RVA: 0x000085EF File Offset: 0x000067EF
		// (set) Token: 0x060004BC RID: 1212 RVA: 0x000085F7 File Offset: 0x000067F7
		public int E15PrimaryMbxCount { get; set; }

		// Token: 0x170001DF RID: 479
		// (get) Token: 0x060004BD RID: 1213 RVA: 0x00008600 File Offset: 0x00006800
		// (set) Token: 0x060004BE RID: 1214 RVA: 0x00008608 File Offset: 0x00006808
		public double E15PrimaryMbxSize { get; set; }

		// Token: 0x170001E0 RID: 480
		// (get) Token: 0x060004BF RID: 1215 RVA: 0x00008611 File Offset: 0x00006811
		// (set) Token: 0x060004C0 RID: 1216 RVA: 0x00008619 File Offset: 0x00006819
		public int E15ArchiveMbxCount { get; set; }

		// Token: 0x170001E1 RID: 481
		// (get) Token: 0x060004C1 RID: 1217 RVA: 0x00008622 File Offset: 0x00006822
		// (set) Token: 0x060004C2 RID: 1218 RVA: 0x0000862A File Offset: 0x0000682A
		public double E15ArchiveMbxSize { get; set; }

		// Token: 0x170001E2 RID: 482
		// (get) Token: 0x060004C3 RID: 1219 RVA: 0x00008633 File Offset: 0x00006833
		// (set) Token: 0x060004C4 RID: 1220 RVA: 0x0000863B File Offset: 0x0000683B
		public int TotalPrimaryMbxCount { get; set; }

		// Token: 0x170001E3 RID: 483
		// (get) Token: 0x060004C5 RID: 1221 RVA: 0x00008644 File Offset: 0x00006844
		// (set) Token: 0x060004C6 RID: 1222 RVA: 0x0000864C File Offset: 0x0000684C
		public double TotalPrimaryMbxSize { get; set; }

		// Token: 0x170001E4 RID: 484
		// (get) Token: 0x060004C7 RID: 1223 RVA: 0x00008655 File Offset: 0x00006855
		// (set) Token: 0x060004C8 RID: 1224 RVA: 0x0000865D File Offset: 0x0000685D
		public int TotalArchiveMbxCount { get; set; }

		// Token: 0x170001E5 RID: 485
		// (get) Token: 0x060004C9 RID: 1225 RVA: 0x00008666 File Offset: 0x00006866
		// (set) Token: 0x060004CA RID: 1226 RVA: 0x0000866E File Offset: 0x0000686E
		public double TotalArchiveMbxSize { get; set; }

		// Token: 0x170001E6 RID: 486
		// (get) Token: 0x060004CB RID: 1227 RVA: 0x00008677 File Offset: 0x00006877
		// (set) Token: 0x060004CC RID: 1228 RVA: 0x0000867F File Offset: 0x0000687F
		public int UploadedSize { get; set; }

		// Token: 0x170001E7 RID: 487
		// (get) Token: 0x060004CD RID: 1229 RVA: 0x00008688 File Offset: 0x00006888
		// (set) Token: 0x060004CE RID: 1230 RVA: 0x00008690 File Offset: 0x00006890
		public string ValidationError { get; set; }
	}
}
