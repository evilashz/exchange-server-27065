using System;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x020002E1 RID: 737
	[Serializable]
	public class GlobalLocatorServiceMsaUser : ConfigurableObject
	{
		// Token: 0x06002296 RID: 8854 RVA: 0x0009720E File Offset: 0x0009540E
		internal GlobalLocatorServiceMsaUser() : base(new SimpleProviderPropertyBag())
		{
		}

		// Token: 0x170008B6 RID: 2230
		// (get) Token: 0x06002297 RID: 8855 RVA: 0x0009721B File Offset: 0x0009541B
		internal override ExchangeObjectVersion MaximumSupportedExchangeObjectVersion
		{
			get
			{
				return ExchangeObjectVersion.Exchange2012;
			}
		}

		// Token: 0x170008B7 RID: 2231
		// (get) Token: 0x06002298 RID: 8856 RVA: 0x00097222 File Offset: 0x00095422
		// (set) Token: 0x06002299 RID: 8857 RVA: 0x00097234 File Offset: 0x00095434
		public Guid ExternalDirectoryOrganizationId
		{
			get
			{
				return (Guid)this[GlobalLocatorServiceMsaUserSchema.ExternalDirectoryOrganizationId];
			}
			set
			{
				this[GlobalLocatorServiceMsaUserSchema.ExternalDirectoryOrganizationId] = value;
			}
		}

		// Token: 0x170008B8 RID: 2232
		// (get) Token: 0x0600229A RID: 8858 RVA: 0x00097247 File Offset: 0x00095447
		// (set) Token: 0x0600229B RID: 8859 RVA: 0x00097259 File Offset: 0x00095459
		public SmtpAddress MsaUserMemberName
		{
			get
			{
				return (SmtpAddress)this[GlobalLocatorServiceMsaUserSchema.MsaUserMemberName];
			}
			set
			{
				this[GlobalLocatorServiceMsaUserSchema.MsaUserMemberName] = value;
			}
		}

		// Token: 0x170008B9 RID: 2233
		// (get) Token: 0x0600229C RID: 8860 RVA: 0x0009726C File Offset: 0x0009546C
		// (set) Token: 0x0600229D RID: 8861 RVA: 0x0009727E File Offset: 0x0009547E
		public NetID MsaUserNetId
		{
			get
			{
				return (NetID)this[GlobalLocatorServiceMsaUserSchema.MsaUserNetId];
			}
			set
			{
				this[GlobalLocatorServiceMsaUserSchema.MsaUserNetId] = value;
			}
		}

		// Token: 0x170008BA RID: 2234
		// (get) Token: 0x0600229E RID: 8862 RVA: 0x0009728C File Offset: 0x0009548C
		// (set) Token: 0x0600229F RID: 8863 RVA: 0x0009729E File Offset: 0x0009549E
		public string ResourceForest
		{
			get
			{
				return (string)this[GlobalLocatorServiceMsaUserSchema.ResourceForest];
			}
			set
			{
				this[GlobalLocatorServiceMsaUserSchema.ResourceForest] = value;
			}
		}

		// Token: 0x170008BB RID: 2235
		// (get) Token: 0x060022A0 RID: 8864 RVA: 0x000972AC File Offset: 0x000954AC
		// (set) Token: 0x060022A1 RID: 8865 RVA: 0x000972BE File Offset: 0x000954BE
		public string AccountForest
		{
			get
			{
				return (string)this[GlobalLocatorServiceMsaUserSchema.AccountForest];
			}
			set
			{
				this[GlobalLocatorServiceMsaUserSchema.AccountForest] = value;
			}
		}

		// Token: 0x170008BC RID: 2236
		// (get) Token: 0x060022A2 RID: 8866 RVA: 0x000972CC File Offset: 0x000954CC
		// (set) Token: 0x060022A3 RID: 8867 RVA: 0x000972DE File Offset: 0x000954DE
		public string TenantContainerCN
		{
			get
			{
				return (string)this[GlobalLocatorServiceMsaUserSchema.TenantContainerCN];
			}
			set
			{
				this[GlobalLocatorServiceMsaUserSchema.TenantContainerCN] = value;
			}
		}

		// Token: 0x170008BD RID: 2237
		// (get) Token: 0x060022A4 RID: 8868 RVA: 0x000972EC File Offset: 0x000954EC
		internal override ObjectSchema ObjectSchema
		{
			get
			{
				return ObjectSchema.GetInstance<GlobalLocatorServiceMsaUserSchema>();
			}
		}
	}
}
