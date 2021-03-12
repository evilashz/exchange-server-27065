using System;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x02000508 RID: 1288
	[Serializable]
	public sealed class MsoTenantCookieContainer : Organization
	{
		// Token: 0x170011D0 RID: 4560
		// (get) Token: 0x06003912 RID: 14610 RVA: 0x000DC5F9 File Offset: 0x000DA7F9
		internal MultiValuedProperty<byte[]> MsoForwardSyncRecipientCookie
		{
			get
			{
				return (MultiValuedProperty<byte[]>)this[MsoTenantCookieContainerSchema.MsoForwardSyncRecipientCookie];
			}
		}

		// Token: 0x170011D1 RID: 4561
		// (get) Token: 0x06003913 RID: 14611 RVA: 0x000DC60B File Offset: 0x000DA80B
		internal MultiValuedProperty<byte[]> MsoForwardSyncNonRecipientCookie
		{
			get
			{
				return (MultiValuedProperty<byte[]>)this[MsoTenantCookieContainerSchema.MsoForwardSyncNonRecipientCookie];
			}
		}

		// Token: 0x170011D2 RID: 4562
		// (get) Token: 0x06003914 RID: 14612 RVA: 0x000DC61D File Offset: 0x000DA81D
		internal string ExternalDirectoryOrganizationId
		{
			get
			{
				return (string)this[MsoTenantCookieContainerSchema.ExternalDirectoryOrganizationId];
			}
		}

		// Token: 0x170011D3 RID: 4563
		// (get) Token: 0x06003915 RID: 14613 RVA: 0x000DC62F File Offset: 0x000DA82F
		internal MultiValuedProperty<string> DirSyncStatus
		{
			get
			{
				return (MultiValuedProperty<string>)this[OrganizationSchema.DirSyncStatus];
			}
		}

		// Token: 0x170011D4 RID: 4564
		// (get) Token: 0x06003916 RID: 14614 RVA: 0x000DC641 File Offset: 0x000DA841
		internal string DirSyncServiceInstance
		{
			get
			{
				return (string)this[MsoTenantCookieContainerSchema.DirSyncServiceInstance];
			}
		}

		// Token: 0x170011D5 RID: 4565
		// (get) Token: 0x06003917 RID: 14615 RVA: 0x000DC653 File Offset: 0x000DA853
		internal override string MostDerivedObjectClass
		{
			get
			{
				return ExchangeConfigurationUnit.MostDerivedClass;
			}
		}

		// Token: 0x170011D6 RID: 4566
		// (get) Token: 0x06003918 RID: 14616 RVA: 0x000DC65A File Offset: 0x000DA85A
		internal override ADObjectSchema Schema
		{
			get
			{
				return MsoTenantCookieContainer.schema;
			}
		}

		// Token: 0x040026FC RID: 9980
		private static readonly MsoTenantCookieContainerSchema schema = ObjectSchema.GetInstance<MsoTenantCookieContainerSchema>();
	}
}
