using System;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x0200068B RID: 1675
	[ObjectScope(new ConfigScopes[]
	{
		ConfigScopes.TenantLocal,
		ConfigScopes.TenantSubTree
	})]
	[Serializable]
	public sealed class AuthConfig : ADConfigurationObject
	{
		// Token: 0x1700198D RID: 6541
		// (get) Token: 0x06004DE6 RID: 19942 RVA: 0x0011F1FB File Offset: 0x0011D3FB
		// (set) Token: 0x06004DE7 RID: 19943 RVA: 0x0011F20D File Offset: 0x0011D40D
		public string CurrentCertificateThumbprint
		{
			get
			{
				return (string)this[AuthConfigSchema.CurrentCertificateThumbprint];
			}
			set
			{
				this[AuthConfigSchema.CurrentCertificateThumbprint] = value;
			}
		}

		// Token: 0x1700198E RID: 6542
		// (get) Token: 0x06004DE8 RID: 19944 RVA: 0x0011F21B File Offset: 0x0011D41B
		// (set) Token: 0x06004DE9 RID: 19945 RVA: 0x0011F22D File Offset: 0x0011D42D
		public string PreviousCertificateThumbprint
		{
			get
			{
				return (string)this[AuthConfigSchema.PreviousCertificateThumbprint];
			}
			set
			{
				this[AuthConfigSchema.PreviousCertificateThumbprint] = value;
			}
		}

		// Token: 0x1700198F RID: 6543
		// (get) Token: 0x06004DEA RID: 19946 RVA: 0x0011F23B File Offset: 0x0011D43B
		// (set) Token: 0x06004DEB RID: 19947 RVA: 0x0011F24D File Offset: 0x0011D44D
		public string NextCertificateThumbprint
		{
			get
			{
				return (string)this[AuthConfigSchema.NextCertificateThumbprint];
			}
			set
			{
				this[AuthConfigSchema.NextCertificateThumbprint] = value;
			}
		}

		// Token: 0x17001990 RID: 6544
		// (get) Token: 0x06004DEC RID: 19948 RVA: 0x0011F25B File Offset: 0x0011D45B
		// (set) Token: 0x06004DED RID: 19949 RVA: 0x0011F26D File Offset: 0x0011D46D
		public DateTime? NextCertificateEffectiveDate
		{
			get
			{
				return (DateTime?)this[AuthConfigSchema.NextCertificateEffectiveDate];
			}
			set
			{
				this[AuthConfigSchema.NextCertificateEffectiveDate] = value;
			}
		}

		// Token: 0x17001991 RID: 6545
		// (get) Token: 0x06004DEE RID: 19950 RVA: 0x0011F280 File Offset: 0x0011D480
		// (set) Token: 0x06004DEF RID: 19951 RVA: 0x0011F292 File Offset: 0x0011D492
		public string ServiceName
		{
			get
			{
				return (string)this[AuthConfigSchema.ServiceName];
			}
			set
			{
				this[AuthConfigSchema.ServiceName] = value;
			}
		}

		// Token: 0x17001992 RID: 6546
		// (get) Token: 0x06004DF0 RID: 19952 RVA: 0x0011F2A0 File Offset: 0x0011D4A0
		// (set) Token: 0x06004DF1 RID: 19953 RVA: 0x0011F2B2 File Offset: 0x0011D4B2
		public string Realm
		{
			get
			{
				return (string)this[AuthConfigSchema.Realm];
			}
			set
			{
				this[AuthConfigSchema.Realm] = value;
			}
		}

		// Token: 0x17001993 RID: 6547
		// (get) Token: 0x06004DF2 RID: 19954 RVA: 0x0011F2C0 File Offset: 0x0011D4C0
		internal override ADObjectSchema Schema
		{
			get
			{
				return AuthConfig.SchemaObject;
			}
		}

		// Token: 0x17001994 RID: 6548
		// (get) Token: 0x06004DF3 RID: 19955 RVA: 0x0011F2C7 File Offset: 0x0011D4C7
		internal override string MostDerivedObjectClass
		{
			get
			{
				return AuthConfig.ObjectClassName;
			}
		}

		// Token: 0x17001995 RID: 6549
		// (get) Token: 0x06004DF4 RID: 19956 RVA: 0x0011F2CE File Offset: 0x0011D4CE
		internal override ExchangeObjectVersion MaximumSupportedExchangeObjectVersion
		{
			get
			{
				return ExchangeObjectVersion.Exchange2012;
			}
		}

		// Token: 0x17001996 RID: 6550
		// (get) Token: 0x06004DF5 RID: 19957 RVA: 0x0011F2D5 File Offset: 0x0011D4D5
		// (set) Token: 0x06004DF6 RID: 19958 RVA: 0x0011F2DD File Offset: 0x0011D4DD
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

		// Token: 0x06004DF7 RID: 19959 RVA: 0x0011F2E8 File Offset: 0x0011D4E8
		internal static ADObjectId GetContainerId(IConfigurationSession configSession)
		{
			if (configSession == null)
			{
				throw new ArgumentNullException("configSession");
			}
			return configSession.GetOrgContainerId().GetChildId(AuthConfig.ContainerName);
		}

		// Token: 0x06004DF8 RID: 19960 RVA: 0x0011F315 File Offset: 0x0011D515
		internal static AuthConfig Read(IConfigurationSession configSession)
		{
			if (configSession == null)
			{
				throw new ArgumentNullException("configSession");
			}
			return configSession.Read<AuthConfig>(AuthConfig.GetContainerId(configSession));
		}

		// Token: 0x040034F0 RID: 13552
		public static readonly string DefaultServiceNameValue = WellknownPartnerApplicationIdentifiers.Exchange;

		// Token: 0x040034F1 RID: 13553
		internal static readonly string ContainerName = "Auth Configuration";

		// Token: 0x040034F2 RID: 13554
		internal static readonly string ObjectClassName = "msExchAuthAuthConfig";

		// Token: 0x040034F3 RID: 13555
		private static readonly AuthConfigSchema SchemaObject = ObjectSchema.GetInstance<AuthConfigSchema>();
	}
}
