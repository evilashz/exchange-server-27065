using System;
using System.Management.Automation;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x02000693 RID: 1683
	[ObjectScope(new ConfigScopes[]
	{
		ConfigScopes.TenantLocal,
		ConfigScopes.TenantSubTree
	})]
	[Serializable]
	public sealed class PartnerApplication : ADConfigurationObject
	{
		// Token: 0x170019AC RID: 6572
		// (get) Token: 0x06004E38 RID: 20024 RVA: 0x0012025A File Offset: 0x0011E45A
		internal override ADObjectSchema Schema
		{
			get
			{
				return PartnerApplication.SchemaObject;
			}
		}

		// Token: 0x170019AD RID: 6573
		// (get) Token: 0x06004E39 RID: 20025 RVA: 0x00120261 File Offset: 0x0011E461
		internal override string MostDerivedObjectClass
		{
			get
			{
				return PartnerApplication.ObjectClassName;
			}
		}

		// Token: 0x170019AE RID: 6574
		// (get) Token: 0x06004E3A RID: 20026 RVA: 0x00120268 File Offset: 0x0011E468
		internal override ExchangeObjectVersion MaximumSupportedExchangeObjectVersion
		{
			get
			{
				return ExchangeObjectVersion.Exchange2012;
			}
		}

		// Token: 0x170019AF RID: 6575
		// (get) Token: 0x06004E3B RID: 20027 RVA: 0x0012026F File Offset: 0x0011E46F
		// (set) Token: 0x06004E3C RID: 20028 RVA: 0x00120281 File Offset: 0x0011E481
		[Parameter]
		public bool Enabled
		{
			get
			{
				return (bool)this[PartnerApplicationSchema.Enabled];
			}
			set
			{
				this[PartnerApplicationSchema.Enabled] = value;
			}
		}

		// Token: 0x170019B0 RID: 6576
		// (get) Token: 0x06004E3D RID: 20029 RVA: 0x00120294 File Offset: 0x0011E494
		// (set) Token: 0x06004E3E RID: 20030 RVA: 0x001202A6 File Offset: 0x0011E4A6
		public string ApplicationIdentifier
		{
			get
			{
				return (string)this[PartnerApplicationSchema.ApplicationIdentifier];
			}
			set
			{
				this[PartnerApplicationSchema.ApplicationIdentifier] = value;
			}
		}

		// Token: 0x170019B1 RID: 6577
		// (get) Token: 0x06004E3F RID: 20031 RVA: 0x001202B4 File Offset: 0x0011E4B4
		// (set) Token: 0x06004E40 RID: 20032 RVA: 0x001202C6 File Offset: 0x0011E4C6
		internal MultiValuedProperty<byte[]> CertificateBytes
		{
			get
			{
				return (MultiValuedProperty<byte[]>)this[PartnerApplicationSchema.CertificateDataRaw];
			}
			set
			{
				this[PartnerApplicationSchema.CertificateDataRaw] = value;
			}
		}

		// Token: 0x170019B2 RID: 6578
		// (get) Token: 0x06004E41 RID: 20033 RVA: 0x001202D4 File Offset: 0x0011E4D4
		public MultiValuedProperty<string> CertificateStrings
		{
			get
			{
				return (MultiValuedProperty<string>)this[PartnerApplicationSchema.CertificateDataString];
			}
		}

		// Token: 0x170019B3 RID: 6579
		// (get) Token: 0x06004E42 RID: 20034 RVA: 0x001202E6 File Offset: 0x0011E4E6
		// (set) Token: 0x06004E43 RID: 20035 RVA: 0x001202F8 File Offset: 0x0011E4F8
		public string AuthMetadataUrl
		{
			get
			{
				return (string)this[PartnerApplicationSchema.AuthMetadataUrl];
			}
			set
			{
				this[PartnerApplicationSchema.AuthMetadataUrl] = value;
			}
		}

		// Token: 0x170019B4 RID: 6580
		// (get) Token: 0x06004E44 RID: 20036 RVA: 0x00120306 File Offset: 0x0011E506
		// (set) Token: 0x06004E45 RID: 20037 RVA: 0x00120318 File Offset: 0x0011E518
		public string Realm
		{
			get
			{
				return (string)this[PartnerApplicationSchema.Realm];
			}
			set
			{
				this[PartnerApplicationSchema.Realm] = value;
			}
		}

		// Token: 0x170019B5 RID: 6581
		// (get) Token: 0x06004E46 RID: 20038 RVA: 0x00120326 File Offset: 0x0011E526
		// (set) Token: 0x06004E47 RID: 20039 RVA: 0x00120338 File Offset: 0x0011E538
		public bool UseAuthServer
		{
			get
			{
				return (bool)this[PartnerApplicationSchema.UseAuthServer];
			}
			set
			{
				this[PartnerApplicationSchema.UseAuthServer] = value;
			}
		}

		// Token: 0x170019B6 RID: 6582
		// (get) Token: 0x06004E48 RID: 20040 RVA: 0x0012034B File Offset: 0x0011E54B
		// (set) Token: 0x06004E49 RID: 20041 RVA: 0x0012035D File Offset: 0x0011E55D
		[Parameter]
		public bool AcceptSecurityIdentifierInformation
		{
			get
			{
				return (bool)this[PartnerApplicationSchema.AcceptSecurityIdentifierInformation];
			}
			set
			{
				this[PartnerApplicationSchema.AcceptSecurityIdentifierInformation] = value;
			}
		}

		// Token: 0x170019B7 RID: 6583
		// (get) Token: 0x06004E4A RID: 20042 RVA: 0x00120370 File Offset: 0x0011E570
		// (set) Token: 0x06004E4B RID: 20043 RVA: 0x00120382 File Offset: 0x0011E582
		public ADObjectId LinkedAccount
		{
			get
			{
				return (ADObjectId)this[PartnerApplicationSchema.LinkedAccount];
			}
			set
			{
				this[PartnerApplicationSchema.LinkedAccount] = value;
			}
		}

		// Token: 0x170019B8 RID: 6584
		// (get) Token: 0x06004E4C RID: 20044 RVA: 0x00120390 File Offset: 0x0011E590
		// (set) Token: 0x06004E4D RID: 20045 RVA: 0x001203A2 File Offset: 0x0011E5A2
		public string IssuerIdentifier
		{
			get
			{
				return (string)this[PartnerApplicationSchema.IssuerIdentifier];
			}
			set
			{
				this[PartnerApplicationSchema.IssuerIdentifier] = value;
			}
		}

		// Token: 0x170019B9 RID: 6585
		// (get) Token: 0x06004E4E RID: 20046 RVA: 0x001203B0 File Offset: 0x0011E5B0
		// (set) Token: 0x06004E4F RID: 20047 RVA: 0x001203C2 File Offset: 0x0011E5C2
		public string[] AppOnlyPermissions
		{
			get
			{
				return (string[])this[PartnerApplicationSchema.AppOnlyPermissions];
			}
			set
			{
				this[PartnerApplicationSchema.AppOnlyPermissions] = value;
			}
		}

		// Token: 0x170019BA RID: 6586
		// (get) Token: 0x06004E50 RID: 20048 RVA: 0x001203D0 File Offset: 0x0011E5D0
		// (set) Token: 0x06004E51 RID: 20049 RVA: 0x001203E2 File Offset: 0x0011E5E2
		public string[] ActAsPermissions
		{
			get
			{
				return (string[])this[PartnerApplicationSchema.ActAsPermissions];
			}
			set
			{
				this[PartnerApplicationSchema.ActAsPermissions] = value;
			}
		}

		// Token: 0x06004E52 RID: 20050 RVA: 0x001203F0 File Offset: 0x0011E5F0
		internal static ADObjectId GetContainerId(IConfigurationSession configSession)
		{
			return configSession.GetOrgContainerId().GetChildId(AuthConfig.ContainerName).GetChildId(PartnerApplication.ParentContainerName);
		}

		// Token: 0x0400353F RID: 13631
		internal static readonly string ParentContainerName = "Partner Applications";

		// Token: 0x04003540 RID: 13632
		internal static readonly string ObjectClassName = "msExchAuthPartnerApplication";

		// Token: 0x04003541 RID: 13633
		private static readonly PartnerApplicationSchema SchemaObject = ObjectSchema.GetInstance<PartnerApplicationSchema>();
	}
}
