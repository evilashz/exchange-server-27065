using System;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x020003B2 RID: 946
	[ObjectScope(new ConfigScopes[]
	{
		ConfigScopes.TenantLocal,
		ConfigScopes.TenantSubTree
	})]
	[Serializable]
	public sealed class AvailabilityConfig : ADConfigurationObject
	{
		// Token: 0x17000BC8 RID: 3016
		// (get) Token: 0x06002B44 RID: 11076 RVA: 0x000B3BFB File Offset: 0x000B1DFB
		internal override ADObjectSchema Schema
		{
			get
			{
				return AvailabilityConfig.schema;
			}
		}

		// Token: 0x17000BC9 RID: 3017
		// (get) Token: 0x06002B45 RID: 11077 RVA: 0x000B3C02 File Offset: 0x000B1E02
		internal override string MostDerivedObjectClass
		{
			get
			{
				return "msExchAvailabilityConfig";
			}
		}

		// Token: 0x17000BCA RID: 3018
		// (get) Token: 0x06002B47 RID: 11079 RVA: 0x000B3C11 File Offset: 0x000B1E11
		// (set) Token: 0x06002B48 RID: 11080 RVA: 0x000B3C19 File Offset: 0x000B1E19
		public new string Name
		{
			get
			{
				return base.Name;
			}
			internal set
			{
				base.Name = value;
			}
		}

		// Token: 0x17000BCB RID: 3019
		// (get) Token: 0x06002B49 RID: 11081 RVA: 0x000B3C22 File Offset: 0x000B1E22
		// (set) Token: 0x06002B4A RID: 11082 RVA: 0x000B3C34 File Offset: 0x000B1E34
		public ADObjectId PerUserAccount
		{
			get
			{
				return (ADObjectId)this[AvailabilityConfigSchema.PerUserAccount];
			}
			set
			{
				this[AvailabilityConfigSchema.PerUserAccount] = value;
			}
		}

		// Token: 0x17000BCC RID: 3020
		// (get) Token: 0x06002B4B RID: 11083 RVA: 0x000B3C42 File Offset: 0x000B1E42
		// (set) Token: 0x06002B4C RID: 11084 RVA: 0x000B3C54 File Offset: 0x000B1E54
		public ADObjectId OrgWideAccount
		{
			get
			{
				return (ADObjectId)this[AvailabilityConfigSchema.OrgWideAccount];
			}
			set
			{
				this[AvailabilityConfigSchema.OrgWideAccount] = value;
			}
		}

		// Token: 0x040019F4 RID: 6644
		internal const string LdapName = "msExchAvailabilityConfig";

		// Token: 0x040019F5 RID: 6645
		public static string ContainerName = "Availability Configuration";

		// Token: 0x040019F6 RID: 6646
		private static AvailabilityConfigSchema schema = ObjectSchema.GetInstance<AvailabilityConfigSchema>();

		// Token: 0x040019F7 RID: 6647
		private static ADObjectId path = new ADObjectId("CN=Availability Configuration");

		// Token: 0x040019F8 RID: 6648
		internal static readonly ADObjectId Container = new ADObjectId(string.Format("CN={0}", AvailabilityConfig.ContainerName));
	}
}
