using System;
using System.Management.Automation;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x0200068F RID: 1679
	[ObjectScope(new ConfigScopes[]
	{
		ConfigScopes.TenantLocal,
		ConfigScopes.TenantSubTree
	})]
	[Serializable]
	public sealed class AuthServer : ADConfigurationObject
	{
		// Token: 0x17001997 RID: 6551
		// (get) Token: 0x06004E08 RID: 19976 RVA: 0x0011F965 File Offset: 0x0011DB65
		internal override ADObjectSchema Schema
		{
			get
			{
				return AuthServer.SchemaObject;
			}
		}

		// Token: 0x17001998 RID: 6552
		// (get) Token: 0x06004E09 RID: 19977 RVA: 0x0011F96C File Offset: 0x0011DB6C
		internal override string MostDerivedObjectClass
		{
			get
			{
				return AuthServer.ObjectClassName;
			}
		}

		// Token: 0x17001999 RID: 6553
		// (get) Token: 0x06004E0A RID: 19978 RVA: 0x0011F973 File Offset: 0x0011DB73
		internal override ExchangeObjectVersion MaximumSupportedExchangeObjectVersion
		{
			get
			{
				return ExchangeObjectVersion.Exchange2012;
			}
		}

		// Token: 0x1700199A RID: 6554
		// (get) Token: 0x06004E0B RID: 19979 RVA: 0x0011F97A File Offset: 0x0011DB7A
		// (set) Token: 0x06004E0C RID: 19980 RVA: 0x0011F98C File Offset: 0x0011DB8C
		public string IssuerIdentifier
		{
			get
			{
				return (string)this[AuthServerSchema.IssuerIdentifier];
			}
			set
			{
				this[AuthServerSchema.IssuerIdentifier] = value;
			}
		}

		// Token: 0x1700199B RID: 6555
		// (get) Token: 0x06004E0D RID: 19981 RVA: 0x0011F99A File Offset: 0x0011DB9A
		// (set) Token: 0x06004E0E RID: 19982 RVA: 0x0011F9AC File Offset: 0x0011DBAC
		internal MultiValuedProperty<byte[]> CertificateBytes
		{
			get
			{
				return (MultiValuedProperty<byte[]>)this[AuthServerSchema.CertificateDataRaw];
			}
			set
			{
				this[AuthServerSchema.CertificateDataRaw] = value;
			}
		}

		// Token: 0x1700199C RID: 6556
		// (get) Token: 0x06004E0F RID: 19983 RVA: 0x0011F9BA File Offset: 0x0011DBBA
		public MultiValuedProperty<string> CertificateStrings
		{
			get
			{
				return (MultiValuedProperty<string>)this[AuthServerSchema.CertificateDataString];
			}
		}

		// Token: 0x1700199D RID: 6557
		// (get) Token: 0x06004E10 RID: 19984 RVA: 0x0011F9CC File Offset: 0x0011DBCC
		// (set) Token: 0x06004E11 RID: 19985 RVA: 0x0011F9DE File Offset: 0x0011DBDE
		public string CurrentEncryptedAppSecret
		{
			get
			{
				return (string)this[AuthServerSchema.CurrentEncryptedAppSecret];
			}
			set
			{
				this[AuthServerSchema.CurrentEncryptedAppSecret] = value;
			}
		}

		// Token: 0x1700199E RID: 6558
		// (get) Token: 0x06004E12 RID: 19986 RVA: 0x0011F9EC File Offset: 0x0011DBEC
		// (set) Token: 0x06004E13 RID: 19987 RVA: 0x0011F9FE File Offset: 0x0011DBFE
		public string PreviousEncryptedAppSecret
		{
			get
			{
				return (string)this[AuthServerSchema.PreviousEncryptedAppSecret];
			}
			set
			{
				this[AuthServerSchema.PreviousEncryptedAppSecret] = value;
			}
		}

		// Token: 0x1700199F RID: 6559
		// (get) Token: 0x06004E14 RID: 19988 RVA: 0x0011FA0C File Offset: 0x0011DC0C
		// (set) Token: 0x06004E15 RID: 19989 RVA: 0x0011FA1E File Offset: 0x0011DC1E
		public string TokenIssuingEndpoint
		{
			get
			{
				return (string)this[AuthServerSchema.TokenIssuingEndpoint];
			}
			set
			{
				this[AuthServerSchema.TokenIssuingEndpoint] = value;
			}
		}

		// Token: 0x170019A0 RID: 6560
		// (get) Token: 0x06004E16 RID: 19990 RVA: 0x0011FA2C File Offset: 0x0011DC2C
		// (set) Token: 0x06004E17 RID: 19991 RVA: 0x0011FA3E File Offset: 0x0011DC3E
		public string AuthorizationEndpoint
		{
			get
			{
				return (string)this[AuthServerSchema.AuthorizationEndpoint];
			}
			set
			{
				this[AuthServerSchema.AuthorizationEndpoint] = value;
			}
		}

		// Token: 0x170019A1 RID: 6561
		// (get) Token: 0x06004E18 RID: 19992 RVA: 0x0011FA4C File Offset: 0x0011DC4C
		// (set) Token: 0x06004E19 RID: 19993 RVA: 0x0011FA5E File Offset: 0x0011DC5E
		public string ApplicationIdentifier
		{
			get
			{
				return (string)this[AuthServerSchema.ApplicationIdentifier];
			}
			set
			{
				this[AuthServerSchema.ApplicationIdentifier] = value;
			}
		}

		// Token: 0x170019A2 RID: 6562
		// (get) Token: 0x06004E1A RID: 19994 RVA: 0x0011FA6C File Offset: 0x0011DC6C
		// (set) Token: 0x06004E1B RID: 19995 RVA: 0x0011FA7E File Offset: 0x0011DC7E
		public string AuthMetadataUrl
		{
			get
			{
				return (string)this[AuthServerSchema.AuthMetadataUrl];
			}
			set
			{
				this[AuthServerSchema.AuthMetadataUrl] = value;
			}
		}

		// Token: 0x170019A3 RID: 6563
		// (get) Token: 0x06004E1C RID: 19996 RVA: 0x0011FA8C File Offset: 0x0011DC8C
		// (set) Token: 0x06004E1D RID: 19997 RVA: 0x0011FA9E File Offset: 0x0011DC9E
		public string Realm
		{
			get
			{
				return (string)this[AuthServerSchema.Realm];
			}
			set
			{
				this[AuthServerSchema.Realm] = value;
			}
		}

		// Token: 0x170019A4 RID: 6564
		// (get) Token: 0x06004E1E RID: 19998 RVA: 0x0011FAAC File Offset: 0x0011DCAC
		// (set) Token: 0x06004E1F RID: 19999 RVA: 0x0011FABE File Offset: 0x0011DCBE
		public AuthServerType Type
		{
			get
			{
				return (AuthServerType)this[AuthServerSchema.Type];
			}
			set
			{
				this[AuthServerSchema.Type] = value;
			}
		}

		// Token: 0x170019A5 RID: 6565
		// (get) Token: 0x06004E20 RID: 20000 RVA: 0x0011FAD1 File Offset: 0x0011DCD1
		// (set) Token: 0x06004E21 RID: 20001 RVA: 0x0011FAE3 File Offset: 0x0011DCE3
		[Parameter]
		public bool Enabled
		{
			get
			{
				return (bool)this[AuthServerSchema.Enabled];
			}
			set
			{
				this[AuthServerSchema.Enabled] = value;
			}
		}

		// Token: 0x170019A6 RID: 6566
		// (get) Token: 0x06004E22 RID: 20002 RVA: 0x0011FAF6 File Offset: 0x0011DCF6
		// (set) Token: 0x06004E23 RID: 20003 RVA: 0x0011FB08 File Offset: 0x0011DD08
		public bool IsDefaultAuthorizationEndpoint
		{
			get
			{
				return (bool)this[AuthServerSchema.IsDefaultAuthorizationEndpoint];
			}
			set
			{
				this[AuthServerSchema.IsDefaultAuthorizationEndpoint] = value;
			}
		}

		// Token: 0x06004E24 RID: 20004 RVA: 0x0011FB1C File Offset: 0x0011DD1C
		internal static ADObjectId GetContainerId(IConfigurationSession configSession)
		{
			return configSession.GetOrgContainerId().GetChildId(AuthConfig.ContainerName).GetChildId(AuthServer.ParentContainerName);
		}

		// Token: 0x0400350C RID: 13580
		internal static readonly string ParentContainerName = "Auth Servers";

		// Token: 0x0400350D RID: 13581
		internal static readonly string ObjectClassName = "msExchAuthAuthServer";

		// Token: 0x0400350E RID: 13582
		private static readonly AuthServerSchema SchemaObject = ObjectSchema.GetInstance<AuthServerSchema>();
	}
}
