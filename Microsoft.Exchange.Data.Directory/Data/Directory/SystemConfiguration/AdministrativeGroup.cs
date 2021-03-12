using System;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x0200034F RID: 847
	[Serializable]
	public sealed class AdministrativeGroup : ADLegacyVersionableObject
	{
		// Token: 0x17000A72 RID: 2674
		// (get) Token: 0x06002727 RID: 10023 RVA: 0x000A5866 File Offset: 0x000A3A66
		internal override ADObjectSchema Schema
		{
			get
			{
				return AdministrativeGroup.schema;
			}
		}

		// Token: 0x17000A73 RID: 2675
		// (get) Token: 0x06002728 RID: 10024 RVA: 0x000A586D File Offset: 0x000A3A6D
		internal override string MostDerivedObjectClass
		{
			get
			{
				return AdministrativeGroup.mostDerivedClass;
			}
		}

		// Token: 0x17000A74 RID: 2676
		// (get) Token: 0x06002729 RID: 10025 RVA: 0x000A5874 File Offset: 0x000A3A74
		// (set) Token: 0x0600272A RID: 10026 RVA: 0x000A5886 File Offset: 0x000A3A86
		public ADObjectId PublicFolderDatabase
		{
			get
			{
				return (ADObjectId)this[AdministrativeGroupSchema.PublicFolderDatabase];
			}
			internal set
			{
				this[AdministrativeGroupSchema.PublicFolderDatabase] = value;
			}
		}

		// Token: 0x17000A75 RID: 2677
		// (get) Token: 0x0600272B RID: 10027 RVA: 0x000A5894 File Offset: 0x000A3A94
		public string LegacyExchangeDN
		{
			get
			{
				return (string)this[AdministrativeGroupSchema.LegacyExchangeDN];
			}
		}

		// Token: 0x17000A76 RID: 2678
		// (get) Token: 0x0600272C RID: 10028 RVA: 0x000A58A6 File Offset: 0x000A3AA6
		// (set) Token: 0x0600272D RID: 10029 RVA: 0x000A58B8 File Offset: 0x000A3AB8
		public bool DefaultAdminGroup
		{
			get
			{
				return (bool)this[AdministrativeGroupSchema.DefaultAdminGroup];
			}
			internal set
			{
				this[AdministrativeGroupSchema.DefaultAdminGroup] = value;
			}
		}

		// Token: 0x0600272E RID: 10030 RVA: 0x000A58CB File Offset: 0x000A3ACB
		internal void StampLegacyExchangeDN(string orgLegDN, string agName)
		{
			this[AdministrativeGroupSchema.LegacyExchangeDN] = string.Format("{0}/ou={1}", orgLegDN, LegacyDN.LegitimizeCN(agName));
		}

		// Token: 0x0600272F RID: 10031 RVA: 0x000A58EC File Offset: 0x000A3AEC
		internal override void StampPersistableDefaultValues()
		{
			if (!base.IsModified(AdministrativeGroupSchema.DisplayName))
			{
				this[AdministrativeGroupSchema.DisplayName] = AdministrativeGroup.DefaultName;
			}
			if (!base.IsModified(ADConfigurationObjectSchema.SystemFlags))
			{
				this[ADConfigurationObjectSchema.SystemFlags] = SystemFlagsEnum.None;
			}
			base.StampPersistableDefaultValues();
		}

		// Token: 0x040017E5 RID: 6117
		private static AdministrativeGroupSchema schema = ObjectSchema.GetInstance<AdministrativeGroupSchema>();

		// Token: 0x040017E6 RID: 6118
		private static string mostDerivedClass = "msExchAdminGroup";

		// Token: 0x040017E7 RID: 6119
		public static readonly string DefaultName = "Exchange Administrative Group (FYDIBOHF23SPDLT)";
	}
}
