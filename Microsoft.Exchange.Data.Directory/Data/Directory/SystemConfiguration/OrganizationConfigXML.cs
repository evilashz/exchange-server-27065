using System;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x02000528 RID: 1320
	[XmlType(TypeName = "OrgConfig")]
	[Serializable]
	public sealed class OrganizationConfigXML : XMLSerializableBase
	{
		// Token: 0x17001293 RID: 4755
		// (get) Token: 0x06003AB9 RID: 15033 RVA: 0x000E0DF0 File Offset: 0x000DEFF0
		// (set) Token: 0x06003ABA RID: 15034 RVA: 0x000E0DF8 File Offset: 0x000DEFF8
		[XmlElement(ElementName = "DefaultMovePriority")]
		public int DefaultMovePriority { get; set; }

		// Token: 0x17001294 RID: 4756
		// (get) Token: 0x06003ABB RID: 15035 RVA: 0x000E0E01 File Offset: 0x000DF001
		// (set) Token: 0x06003ABC RID: 15036 RVA: 0x000E0E09 File Offset: 0x000DF009
		[XmlElement(ElementName = "UpgradeConstraints")]
		public UpgradeConstraintArray UpgradeConstraints { get; set; }

		// Token: 0x17001295 RID: 4757
		// (get) Token: 0x06003ABD RID: 15037 RVA: 0x000E0E12 File Offset: 0x000DF012
		// (set) Token: 0x06003ABE RID: 15038 RVA: 0x000E0E1A File Offset: 0x000DF01A
		[XmlElement(ElementName = "UpgradeMessage")]
		public string UpgradeMessage { get; set; }

		// Token: 0x17001296 RID: 4758
		// (get) Token: 0x06003ABF RID: 15039 RVA: 0x000E0E23 File Offset: 0x000DF023
		// (set) Token: 0x06003AC0 RID: 15040 RVA: 0x000E0E2B File Offset: 0x000DF02B
		[XmlElement(ElementName = "UpgradeDetails")]
		public string UpgradeDetails { get; set; }

		// Token: 0x17001297 RID: 4759
		// (get) Token: 0x06003AC1 RID: 15041 RVA: 0x000E0E34 File Offset: 0x000DF034
		// (set) Token: 0x06003AC2 RID: 15042 RVA: 0x000E0E3C File Offset: 0x000DF03C
		[XmlElement(ElementName = "PreviousMailboxRelease")]
		public string PreviousMailboxRelease { get; set; }

		// Token: 0x17001298 RID: 4760
		// (get) Token: 0x06003AC3 RID: 15043 RVA: 0x000E0E45 File Offset: 0x000DF045
		// (set) Token: 0x06003AC4 RID: 15044 RVA: 0x000E0E4D File Offset: 0x000DF04D
		[XmlElement(ElementName = "PilotMailboxRelease")]
		public string PilotMailboxRelease { get; set; }

		// Token: 0x17001299 RID: 4761
		// (get) Token: 0x06003AC5 RID: 15045 RVA: 0x000E0E56 File Offset: 0x000DF056
		// (set) Token: 0x06003AC6 RID: 15046 RVA: 0x000E0E5E File Offset: 0x000DF05E
		[XmlElement(ElementName = "U14MbxC")]
		public int? UpgradeE14MbxCountForCurrentStage { get; set; }

		// Token: 0x1700129A RID: 4762
		// (get) Token: 0x06003AC7 RID: 15047 RVA: 0x000E0E67 File Offset: 0x000DF067
		// (set) Token: 0x06003AC8 RID: 15048 RVA: 0x000E0E6F File Offset: 0x000DF06F
		[XmlElement(ElementName = "U14RequestC")]
		public int? UpgradeE14RequestCountForCurrentStage { get; set; }

		// Token: 0x1700129B RID: 4763
		// (get) Token: 0x06003AC9 RID: 15049 RVA: 0x000E0E78 File Offset: 0x000DF078
		// (set) Token: 0x06003ACA RID: 15050 RVA: 0x000E0E80 File Offset: 0x000DF080
		[XmlElement(ElementName = "UE14CUTime")]
		public DateTime? UpgradeLastE14CountsUpdateTime { get; set; }

		// Token: 0x1700129C RID: 4764
		// (get) Token: 0x06003ACB RID: 15051 RVA: 0x000E0E89 File Offset: 0x000DF089
		// (set) Token: 0x06003ACC RID: 15052 RVA: 0x000E0E91 File Offset: 0x000DF091
		[XmlIgnore]
		public UpgradeStage? UpgradeStage { get; set; }

		// Token: 0x1700129D RID: 4765
		// (get) Token: 0x06003ACD RID: 15053 RVA: 0x000E0E9C File Offset: 0x000DF09C
		// (set) Token: 0x06003ACE RID: 15054 RVA: 0x000E0ED0 File Offset: 0x000DF0D0
		[XmlElement(ElementName = "UStageInt")]
		public int? UpgradeStageInt
		{
			get
			{
				UpgradeStage? upgradeStage = this.UpgradeStage;
				if (upgradeStage == null)
				{
					return null;
				}
				return new int?((int)upgradeStage.GetValueOrDefault());
			}
			set
			{
				int? num = value;
				this.UpgradeStage = ((num != null) ? new UpgradeStage?((UpgradeStage)num.GetValueOrDefault()) : null);
			}
		}

		// Token: 0x1700129E RID: 4766
		// (get) Token: 0x06003ACF RID: 15055 RVA: 0x000E0F05 File Offset: 0x000DF105
		// (set) Token: 0x06003AD0 RID: 15056 RVA: 0x000E0F0D File Offset: 0x000DF10D
		[XmlElement(ElementName = "UStageTime")]
		public DateTime? UpgradeStageTimeStamp { get; set; }

		// Token: 0x1700129F RID: 4767
		// (get) Token: 0x06003AD1 RID: 15057 RVA: 0x000E0F16 File Offset: 0x000DF116
		// (set) Token: 0x06003AD2 RID: 15058 RVA: 0x000E0F1E File Offset: 0x000DF11E
		[XmlElement(ElementName = "UpgradeUnitsOverride")]
		public int? UpgradeUnitsOverride { get; set; }

		// Token: 0x170012A0 RID: 4768
		// (get) Token: 0x06003AD3 RID: 15059 RVA: 0x000E0F27 File Offset: 0x000DF127
		// (set) Token: 0x06003AD4 RID: 15060 RVA: 0x000E0F2F File Offset: 0x000DF12F
		[XmlElement(ElementName = "ReleaseTrack")]
		public ReleaseTrack? ReleaseTrack { get; set; }

		// Token: 0x170012A1 RID: 4769
		// (get) Token: 0x06003AD5 RID: 15061 RVA: 0x000E0F38 File Offset: 0x000DF138
		// (set) Token: 0x06003AD6 RID: 15062 RVA: 0x000E0F40 File Offset: 0x000DF140
		[XmlElement(ElementName = "UpgradeConstraintsDisabled")]
		public bool? UpgradeConstraintsDisabled { get; set; }

		// Token: 0x170012A2 RID: 4770
		// (get) Token: 0x06003AD7 RID: 15063 RVA: 0x000E0F49 File Offset: 0x000DF149
		// (set) Token: 0x06003AD8 RID: 15064 RVA: 0x000E0F51 File Offset: 0x000DF151
		[XmlElement(ElementName = "RelocationConstraints")]
		public RelocationConstraintArray RelocationConstraints { get; set; }

		// Token: 0x170012A3 RID: 4771
		// (get) Token: 0x06003AD9 RID: 15065 RVA: 0x000E0F5A File Offset: 0x000DF15A
		// (set) Token: 0x06003ADA RID: 15066 RVA: 0x000E0F62 File Offset: 0x000DF162
		[XmlElement(ElementName = "LastSuccessfulRelocationSyncStart")]
		public DateTime? LastSuccessfulRelocationSyncStart { get; set; }

		// Token: 0x06003ADB RID: 15067 RVA: 0x000E0F6C File Offset: 0x000DF16C
		public bool ShouldSerializeUpgradeE14MbxCountForCurrentStage()
		{
			return this.UpgradeE14MbxCountForCurrentStage != null;
		}

		// Token: 0x06003ADC RID: 15068 RVA: 0x000E0F88 File Offset: 0x000DF188
		public bool ShouldSerializeUpgradeE14RequestCountForCurrentStage()
		{
			return this.UpgradeE14RequestCountForCurrentStage != null;
		}

		// Token: 0x06003ADD RID: 15069 RVA: 0x000E0FA4 File Offset: 0x000DF1A4
		public bool ShouldSerializeUpgradeLastE14CountsUpdateTime()
		{
			return this.UpgradeLastE14CountsUpdateTime != null;
		}

		// Token: 0x06003ADE RID: 15070 RVA: 0x000E0FC0 File Offset: 0x000DF1C0
		public bool ShouldSerializeUpgradeStageInt()
		{
			return this.UpgradeStageInt != null;
		}

		// Token: 0x06003ADF RID: 15071 RVA: 0x000E0FDC File Offset: 0x000DF1DC
		public bool ShouldSerializeUpgradeStageTimeStamp()
		{
			return this.UpgradeStageTimeStamp != null;
		}

		// Token: 0x06003AE0 RID: 15072 RVA: 0x000E0FF8 File Offset: 0x000DF1F8
		public bool ShouldSerializeUpgradeUnitsOverride()
		{
			return this.UpgradeUnitsOverride != null;
		}

		// Token: 0x06003AE1 RID: 15073 RVA: 0x000E1014 File Offset: 0x000DF214
		public bool ShouldSerializeUpgradeConstraintsDisabled()
		{
			return this.UpgradeConstraintsDisabled != null;
		}
	}
}
