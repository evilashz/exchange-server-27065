using System;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.Management;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.MailboxReplicationService.Upgrade14to15
{
	// Token: 0x020000A7 RID: 167
	public class TenantOrganizationPresentationObjectWrapper
	{
		// Token: 0x06000471 RID: 1137 RVA: 0x00008272 File Offset: 0x00006472
		public TenantOrganizationPresentationObjectWrapper()
		{
		}

		// Token: 0x06000472 RID: 1138 RVA: 0x0000827C File Offset: 0x0000647C
		public TenantOrganizationPresentationObjectWrapper(TenantOrganizationPresentationObject tenant)
		{
			this.Name = tenant.Name;
			this.UpgradeStatus = tenant.UpgradeStatus;
			this.UpgradeRequest = tenant.UpgradeRequest;
			this.UpgradeMessage = tenant.UpgradeMessage;
			this.UpgradeDetails = tenant.UpgradeDetails;
			this.UpgradeConstraints = tenant.UpgradeConstraints;
			this.IsUpgradingOrganization = tenant.IsUpgradingOrganization;
			this.IsPilotingOrganization = tenant.IsPilotingOrganization;
			this.AdminDisplayVersion = tenant.AdminDisplayVersion;
			this.ServicePlan = tenant.ServicePlan;
			this.ExternalDirectoryOrganizationId = tenant.ExternalDirectoryOrganizationId;
			this.IsUpgradeOperationInProgress = tenant.IsUpgradeOperationInProgress;
			this.ProgramId = tenant.ProgramId;
			this.OfferId = tenant.OfferId;
			this.UpgradeStage = tenant.UpgradeStage;
			this.UpgradeStageTimeStamp = tenant.UpgradeStageTimeStamp;
			this.UpgradeE14MbxCountForCurrentStage = tenant.UpgradeE14MbxCountForCurrentStage;
			this.UpgradeE14RequestCountForCurrentStage = tenant.UpgradeE14RequestCountForCurrentStage;
			this.UpgradeLastE14CountsUpdateTime = tenant.UpgradeLastE14CountsUpdateTime;
			this.UpgradeConstraintsDisabled = tenant.UpgradeConstraintsDisabled;
			this.UpgradeUnitsOverride = tenant.UpgradeUnitsOverride;
		}

		// Token: 0x170001BA RID: 442
		// (get) Token: 0x06000473 RID: 1139 RVA: 0x0000838B File Offset: 0x0000658B
		// (set) Token: 0x06000474 RID: 1140 RVA: 0x00008393 File Offset: 0x00006593
		public string Name { get; private set; }

		// Token: 0x170001BB RID: 443
		// (get) Token: 0x06000475 RID: 1141 RVA: 0x0000839C File Offset: 0x0000659C
		// (set) Token: 0x06000476 RID: 1142 RVA: 0x000083A4 File Offset: 0x000065A4
		public UpgradeStatusTypes UpgradeStatus { get; set; }

		// Token: 0x170001BC RID: 444
		// (get) Token: 0x06000477 RID: 1143 RVA: 0x000083AD File Offset: 0x000065AD
		// (set) Token: 0x06000478 RID: 1144 RVA: 0x000083B5 File Offset: 0x000065B5
		public UpgradeRequestTypes UpgradeRequest { get; set; }

		// Token: 0x170001BD RID: 445
		// (get) Token: 0x06000479 RID: 1145 RVA: 0x000083BE File Offset: 0x000065BE
		// (set) Token: 0x0600047A RID: 1146 RVA: 0x000083C6 File Offset: 0x000065C6
		public string UpgradeMessage { get; set; }

		// Token: 0x170001BE RID: 446
		// (get) Token: 0x0600047B RID: 1147 RVA: 0x000083CF File Offset: 0x000065CF
		// (set) Token: 0x0600047C RID: 1148 RVA: 0x000083D7 File Offset: 0x000065D7
		public string UpgradeDetails { get; set; }

		// Token: 0x170001BF RID: 447
		// (get) Token: 0x0600047D RID: 1149 RVA: 0x000083E0 File Offset: 0x000065E0
		// (set) Token: 0x0600047E RID: 1150 RVA: 0x000083E8 File Offset: 0x000065E8
		public UpgradeConstraintArray UpgradeConstraints { get; set; }

		// Token: 0x170001C0 RID: 448
		// (get) Token: 0x0600047F RID: 1151 RVA: 0x000083F1 File Offset: 0x000065F1
		// (set) Token: 0x06000480 RID: 1152 RVA: 0x000083F9 File Offset: 0x000065F9
		public bool IsUpgradingOrganization { get; private set; }

		// Token: 0x170001C1 RID: 449
		// (get) Token: 0x06000481 RID: 1153 RVA: 0x00008402 File Offset: 0x00006602
		// (set) Token: 0x06000482 RID: 1154 RVA: 0x0000840A File Offset: 0x0000660A
		public bool IsPilotingOrganization { get; private set; }

		// Token: 0x170001C2 RID: 450
		// (get) Token: 0x06000483 RID: 1155 RVA: 0x00008413 File Offset: 0x00006613
		// (set) Token: 0x06000484 RID: 1156 RVA: 0x0000841B File Offset: 0x0000661B
		public ExchangeObjectVersion AdminDisplayVersion { get; private set; }

		// Token: 0x170001C3 RID: 451
		// (get) Token: 0x06000485 RID: 1157 RVA: 0x00008424 File Offset: 0x00006624
		// (set) Token: 0x06000486 RID: 1158 RVA: 0x0000842C File Offset: 0x0000662C
		public string ServicePlan { get; private set; }

		// Token: 0x170001C4 RID: 452
		// (get) Token: 0x06000487 RID: 1159 RVA: 0x00008435 File Offset: 0x00006635
		// (set) Token: 0x06000488 RID: 1160 RVA: 0x0000843D File Offset: 0x0000663D
		public string ExternalDirectoryOrganizationId { get; private set; }

		// Token: 0x170001C5 RID: 453
		// (get) Token: 0x06000489 RID: 1161 RVA: 0x00008446 File Offset: 0x00006646
		// (set) Token: 0x0600048A RID: 1162 RVA: 0x0000844E File Offset: 0x0000664E
		public bool IsUpgradeOperationInProgress { get; private set; }

		// Token: 0x170001C6 RID: 454
		// (get) Token: 0x0600048B RID: 1163 RVA: 0x00008457 File Offset: 0x00006657
		// (set) Token: 0x0600048C RID: 1164 RVA: 0x0000845F File Offset: 0x0000665F
		public string ProgramId { get; private set; }

		// Token: 0x170001C7 RID: 455
		// (get) Token: 0x0600048D RID: 1165 RVA: 0x00008468 File Offset: 0x00006668
		// (set) Token: 0x0600048E RID: 1166 RVA: 0x00008470 File Offset: 0x00006670
		public string OfferId { get; private set; }

		// Token: 0x170001C8 RID: 456
		// (get) Token: 0x0600048F RID: 1167 RVA: 0x00008479 File Offset: 0x00006679
		// (set) Token: 0x06000490 RID: 1168 RVA: 0x00008481 File Offset: 0x00006681
		public UpgradeStage? UpgradeStage { get; set; }

		// Token: 0x170001C9 RID: 457
		// (get) Token: 0x06000491 RID: 1169 RVA: 0x0000848A File Offset: 0x0000668A
		// (set) Token: 0x06000492 RID: 1170 RVA: 0x00008492 File Offset: 0x00006692
		public DateTime? UpgradeStageTimeStamp { get; set; }

		// Token: 0x170001CA RID: 458
		// (get) Token: 0x06000493 RID: 1171 RVA: 0x0000849B File Offset: 0x0000669B
		// (set) Token: 0x06000494 RID: 1172 RVA: 0x000084A3 File Offset: 0x000066A3
		public int? UpgradeE14MbxCountForCurrentStage { get; set; }

		// Token: 0x170001CB RID: 459
		// (get) Token: 0x06000495 RID: 1173 RVA: 0x000084AC File Offset: 0x000066AC
		// (set) Token: 0x06000496 RID: 1174 RVA: 0x000084B4 File Offset: 0x000066B4
		public int? UpgradeE14RequestCountForCurrentStage { get; set; }

		// Token: 0x170001CC RID: 460
		// (get) Token: 0x06000497 RID: 1175 RVA: 0x000084BD File Offset: 0x000066BD
		// (set) Token: 0x06000498 RID: 1176 RVA: 0x000084C5 File Offset: 0x000066C5
		public DateTime? UpgradeLastE14CountsUpdateTime { get; set; }

		// Token: 0x170001CD RID: 461
		// (get) Token: 0x06000499 RID: 1177 RVA: 0x000084CE File Offset: 0x000066CE
		// (set) Token: 0x0600049A RID: 1178 RVA: 0x000084D6 File Offset: 0x000066D6
		public bool? UpgradeConstraintsDisabled { get; set; }

		// Token: 0x170001CE RID: 462
		// (get) Token: 0x0600049B RID: 1179 RVA: 0x000084DF File Offset: 0x000066DF
		// (set) Token: 0x0600049C RID: 1180 RVA: 0x000084E7 File Offset: 0x000066E7
		public int? UpgradeUnitsOverride { get; set; }
	}
}
