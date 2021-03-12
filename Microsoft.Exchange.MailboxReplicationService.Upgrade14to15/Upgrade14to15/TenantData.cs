using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.MailboxReplicationService.Upgrade14to15
{
	// Token: 0x020000A2 RID: 162
	public class TenantData
	{
		// Token: 0x0600041E RID: 1054 RVA: 0x00006627 File Offset: 0x00004827
		public TenantData(string tenantName) : this(Guid.Empty, tenantName, null, null, false, false, null)
		{
		}

		// Token: 0x0600041F RID: 1055 RVA: 0x0000663A File Offset: 0x0000483A
		public TenantData(Guid tenantId, string tenantName, string servicePlan, ExchangeObjectVersion version, bool isUpgradingOrganization, bool isPilotingOrganization, string[] constraints)
		{
			this.TenantId = tenantId;
			this.TenantName = tenantName;
			this.ServicePlan = servicePlan;
			this.Constraints = constraints;
			this.Version = version;
			this.IsUpgradingOrganization = isUpgradingOrganization;
			this.IsPilotingOrganization = isPilotingOrganization;
		}

		// Token: 0x1700019C RID: 412
		// (get) Token: 0x06000420 RID: 1056 RVA: 0x00006677 File Offset: 0x00004877
		// (set) Token: 0x06000421 RID: 1057 RVA: 0x0000667F File Offset: 0x0000487F
		public Guid TenantId { get; private set; }

		// Token: 0x1700019D RID: 413
		// (get) Token: 0x06000422 RID: 1058 RVA: 0x00006688 File Offset: 0x00004888
		// (set) Token: 0x06000423 RID: 1059 RVA: 0x00006690 File Offset: 0x00004890
		public string TenantName { get; private set; }

		// Token: 0x1700019E RID: 414
		// (get) Token: 0x06000424 RID: 1060 RVA: 0x00006699 File Offset: 0x00004899
		// (set) Token: 0x06000425 RID: 1061 RVA: 0x000066A1 File Offset: 0x000048A1
		public string ServicePlan { get; private set; }

		// Token: 0x1700019F RID: 415
		// (get) Token: 0x06000426 RID: 1062 RVA: 0x000066AA File Offset: 0x000048AA
		// (set) Token: 0x06000427 RID: 1063 RVA: 0x000066B2 File Offset: 0x000048B2
		public string[] Constraints { get; private set; }

		// Token: 0x170001A0 RID: 416
		// (get) Token: 0x06000428 RID: 1064 RVA: 0x000066BB File Offset: 0x000048BB
		public VersionData E14MbxData
		{
			get
			{
				return this.e14MbxData;
			}
		}

		// Token: 0x170001A1 RID: 417
		// (get) Token: 0x06000429 RID: 1065 RVA: 0x000066C3 File Offset: 0x000048C3
		public VersionData E15MbxData
		{
			get
			{
				return this.e15MbxData;
			}
		}

		// Token: 0x170001A2 RID: 418
		// (get) Token: 0x0600042A RID: 1066 RVA: 0x000066CB File Offset: 0x000048CB
		// (set) Token: 0x0600042B RID: 1067 RVA: 0x000066D3 File Offset: 0x000048D3
		public int TenantSize { get; set; }

		// Token: 0x170001A3 RID: 419
		// (get) Token: 0x0600042C RID: 1068 RVA: 0x000066DC File Offset: 0x000048DC
		public int TotalPrimaryMbxCount
		{
			get
			{
				return this.E14MbxData.PrimaryData.Count + this.E15MbxData.PrimaryData.Count;
			}
		}

		// Token: 0x170001A4 RID: 420
		// (get) Token: 0x0600042D RID: 1069 RVA: 0x000066FF File Offset: 0x000048FF
		public double TotalPrimaryMbxSize
		{
			get
			{
				return this.E14MbxData.PrimaryData.Size + this.E15MbxData.PrimaryData.Size;
			}
		}

		// Token: 0x170001A5 RID: 421
		// (get) Token: 0x0600042E RID: 1070 RVA: 0x00006722 File Offset: 0x00004922
		public int TotalArchiveMbxCount
		{
			get
			{
				return this.E14MbxData.ArchiveData.Count + this.E15MbxData.ArchiveData.Count;
			}
		}

		// Token: 0x170001A6 RID: 422
		// (get) Token: 0x0600042F RID: 1071 RVA: 0x00006745 File Offset: 0x00004945
		public double TotalArchiveMbxSize
		{
			get
			{
				return this.E14MbxData.ArchiveData.Size + this.E15MbxData.ArchiveData.Size;
			}
		}

		// Token: 0x170001A7 RID: 423
		// (get) Token: 0x06000430 RID: 1072 RVA: 0x00006768 File Offset: 0x00004968
		// (set) Token: 0x06000431 RID: 1073 RVA: 0x00006770 File Offset: 0x00004970
		public string ProgramId { get; private set; }

		// Token: 0x170001A8 RID: 424
		// (get) Token: 0x06000432 RID: 1074 RVA: 0x00006779 File Offset: 0x00004979
		// (set) Token: 0x06000433 RID: 1075 RVA: 0x00006781 File Offset: 0x00004981
		public string OfferId { get; private set; }

		// Token: 0x170001A9 RID: 425
		// (get) Token: 0x06000434 RID: 1076 RVA: 0x0000678A File Offset: 0x0000498A
		public bool ShouldIgnore
		{
			get
			{
				return !this.ShouldUpload();
			}
		}

		// Token: 0x170001AA RID: 426
		// (get) Token: 0x06000435 RID: 1077 RVA: 0x00006795 File Offset: 0x00004995
		// (set) Token: 0x06000436 RID: 1078 RVA: 0x0000679D File Offset: 0x0000499D
		public ExchangeObjectVersion Version { get; private set; }

		// Token: 0x170001AB RID: 427
		// (get) Token: 0x06000437 RID: 1079 RVA: 0x000067A6 File Offset: 0x000049A6
		// (set) Token: 0x06000438 RID: 1080 RVA: 0x000067AE File Offset: 0x000049AE
		public bool IsUpgradingOrganization { get; private set; }

		// Token: 0x170001AC RID: 428
		// (get) Token: 0x06000439 RID: 1081 RVA: 0x000067B7 File Offset: 0x000049B7
		// (set) Token: 0x0600043A RID: 1082 RVA: 0x000067BF File Offset: 0x000049BF
		public bool IsPilotingOrganization { get; private set; }

		// Token: 0x170001AD RID: 429
		// (get) Token: 0x0600043B RID: 1083 RVA: 0x000067C8 File Offset: 0x000049C8
		// (set) Token: 0x0600043C RID: 1084 RVA: 0x000067D0 File Offset: 0x000049D0
		public bool? UpgradeConstraintsDisabled { get; private set; }

		// Token: 0x170001AE RID: 430
		// (get) Token: 0x0600043D RID: 1085 RVA: 0x000067D9 File Offset: 0x000049D9
		// (set) Token: 0x0600043E RID: 1086 RVA: 0x000067E1 File Offset: 0x000049E1
		public int? UpgradeUnitsOverride { get; private set; }

		// Token: 0x0600043F RID: 1087 RVA: 0x000067EA File Offset: 0x000049EA
		public void AddValues(int primaryCount, double primarySize, int archiveCount, double archiveSize, bool isE14Data)
		{
			if (isE14Data)
			{
				this.e14MbxData.Add(primaryCount, primarySize, archiveCount, archiveSize);
				return;
			}
			this.e15MbxData.Add(primaryCount, primarySize, archiveCount, archiveSize);
		}

		// Token: 0x06000440 RID: 1088 RVA: 0x00006814 File Offset: 0x00004A14
		public void UpdateFromTenant(TenantOrganizationPresentationObjectWrapper tenant)
		{
			List<string> list = new List<string>();
			if (tenant.UpgradeConstraints != null && tenant.UpgradeConstraints.UpgradeConstraints != null && tenant.UpgradeConstraints.UpgradeConstraints.Length > 0)
			{
				foreach (UpgradeConstraint upgradeConstraint in tenant.UpgradeConstraints.UpgradeConstraints)
				{
					if (!string.IsNullOrWhiteSpace(upgradeConstraint.Name) && (upgradeConstraint.ExpirationDate > DateTime.UtcNow || upgradeConstraint.ExpirationDate == DateTime.MinValue))
					{
						list.Add(upgradeConstraint.Name);
					}
				}
			}
			this.Constraints = list.ToArray();
			this.Version = tenant.AdminDisplayVersion;
			this.IsPilotingOrganization = tenant.IsPilotingOrganization;
			this.IsUpgradingOrganization = tenant.IsUpgradingOrganization;
			this.ServicePlan = tenant.ServicePlan;
			this.ProgramId = tenant.ProgramId;
			this.OfferId = tenant.OfferId;
			this.UpgradeConstraintsDisabled = tenant.UpgradeConstraintsDisabled;
			this.UpgradeUnitsOverride = tenant.UpgradeUnitsOverride;
			Guid tenantId;
			Guid.TryParse(tenant.ExternalDirectoryOrganizationId, out tenantId);
			this.TenantId = tenantId;
		}

		// Token: 0x06000441 RID: 1089 RVA: 0x0000692D File Offset: 0x00004B2D
		public bool IsEdu()
		{
			return this.ProgramId.Equals("EDU", StringComparison.OrdinalIgnoreCase) || (this.ProgramId.Equals("MSOnlineMigration", StringComparison.OrdinalIgnoreCase) && this.OfferId.Equals("EDU", StringComparison.OrdinalIgnoreCase));
		}

		// Token: 0x06000442 RID: 1090 RVA: 0x0000696A File Offset: 0x00004B6A
		public bool IsFfdf()
		{
			return this.ProgramId.Equals("FFDF", StringComparison.OrdinalIgnoreCase) || (this.ProgramId.Equals("MSOnlineMigration", StringComparison.OrdinalIgnoreCase) && this.OfferId.Equals("FFDF", StringComparison.OrdinalIgnoreCase));
		}

		// Token: 0x06000443 RID: 1091 RVA: 0x000069A8 File Offset: 0x00004BA8
		internal void Validate()
		{
			if (this.Version.ExchangeBuild.Major == ExchangeObjectVersion.Exchange2010.ExchangeBuild.Major)
			{
				if (this.IsPilotingOrganization)
				{
					if (this.E15MbxData.PrimaryData.Count > 101 || this.E15MbxData.ArchiveData.Count > 101)
					{
						throw new TooManyPilotMailboxesException();
					}
				}
				else if (this.E15MbxData.PrimaryData.Count > 0 || this.E15MbxData.ArchiveData.Count > 0)
				{
					throw new InvalidE15MailboxesException();
				}
			}
			else if (this.Version.ExchangeBuild.Major == ExchangeObjectVersion.Exchange2012.ExchangeBuild.Major && !this.IsPilotingOrganization && !this.IsUpgradingOrganization && (this.E14MbxData.PrimaryData.Count > 0 || this.E14MbxData.PrimaryData.Count > 0))
			{
				throw new InvalidE14MailboxesException();
			}
		}

		// Token: 0x06000444 RID: 1092 RVA: 0x00006A98 File Offset: 0x00004C98
		private bool ShouldUpload()
		{
			return !this.TenantId.Equals(Guid.Empty) && !this.IsEdu() && !this.IsFfdf() && this.TenantSize >= 0;
		}

		// Token: 0x040001D4 RID: 468
		private const string Edu = "EDU";

		// Token: 0x040001D5 RID: 469
		private const string Ffdf = "FFDF";

		// Token: 0x040001D6 RID: 470
		private const string MsOnlineMigration = "MSOnlineMigration";

		// Token: 0x040001D7 RID: 471
		private VersionData e14MbxData;

		// Token: 0x040001D8 RID: 472
		private VersionData e15MbxData;
	}
}
