using System;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.Data.Directory.Management
{
	// Token: 0x02000758 RID: 1880
	[Serializable]
	public sealed class SyncConfig : ADPresentationObject
	{
		// Token: 0x17001FB0 RID: 8112
		// (get) Token: 0x06005B43 RID: 23363 RVA: 0x0013F7F1 File Offset: 0x0013D9F1
		internal override ADPresentationSchema PresentationSchema
		{
			get
			{
				return SyncConfig.schema;
			}
		}

		// Token: 0x17001FB1 RID: 8113
		// (get) Token: 0x06005B44 RID: 23364 RVA: 0x0013F7F8 File Offset: 0x0013D9F8
		internal override ExchangeObjectVersion MaximumSupportedExchangeObjectVersion
		{
			get
			{
				return ExchangeObjectVersion.Exchange2010;
			}
		}

		// Token: 0x06005B45 RID: 23365 RVA: 0x0013F800 File Offset: 0x0013DA00
		public SyncConfig(SyncOrganization cu, SmtpDomain federatedNamespace, SmtpDomainWithSubdomains provisioningDomain) : base(cu)
		{
			if (cu == null)
			{
				throw new ArgumentNullException("The value of parameter cu is null!");
			}
			if (cu.FederatedTenant && federatedNamespace == null)
			{
				throw new ArgumentNullException("The value of parameter federatedNamespace should not be null for a federated organization!");
			}
			if (cu.ProvisioningDomain == null && provisioningDomain == null)
			{
				throw new ArgumentNullException("The value of parameter provisioningDomain is null!");
			}
			if (cu.FederatedTenant)
			{
				this.FederatedNamespace = federatedNamespace;
				if (string.IsNullOrEmpty(this.FederatedIdentitySourceADAttribute))
				{
					this[SyncConfigSchema.FederatedIdentitySourceADAttribute] = "objectGuid";
				}
			}
			else if (string.IsNullOrEmpty(this.PasswordFilePath))
			{
				this[SyncConfigSchema.PasswordFilePath] = "password.xml";
			}
			if (cu.ProvisioningDomain == null)
			{
				this[SyncConfigSchema.ProvisioningDomain] = provisioningDomain;
			}
		}

		// Token: 0x17001FB2 RID: 8114
		// (get) Token: 0x06005B46 RID: 23366 RVA: 0x0013F8AD File Offset: 0x0013DAAD
		public bool FederatedTenant
		{
			get
			{
				return (bool)this[SyncConfigSchema.FederatedTenant];
			}
		}

		// Token: 0x17001FB3 RID: 8115
		// (get) Token: 0x06005B47 RID: 23367 RVA: 0x0013F8C0 File Offset: 0x0013DAC0
		public bool? DisableWindowsLiveID
		{
			get
			{
				if (!this.FederatedTenant)
				{
					return (bool?)this[SyncConfigSchema.DisableWindowsLiveID];
				}
				return null;
			}
		}

		// Token: 0x17001FB4 RID: 8116
		// (get) Token: 0x06005B48 RID: 23368 RVA: 0x0013F8EF File Offset: 0x0013DAEF
		public string FederatedIdentitySourceADAttribute
		{
			get
			{
				return (string)this[SyncConfigSchema.FederatedIdentitySourceADAttribute];
			}
		}

		// Token: 0x17001FB5 RID: 8117
		// (get) Token: 0x06005B49 RID: 23369 RVA: 0x0013F901 File Offset: 0x0013DB01
		public bool WlidUseSMTPPrimary
		{
			get
			{
				return (bool)this[SyncConfigSchema.WlidUseSMTPPrimary];
			}
		}

		// Token: 0x17001FB6 RID: 8118
		// (get) Token: 0x06005B4A RID: 23370 RVA: 0x0013F913 File Offset: 0x0013DB13
		public string PasswordFilePath
		{
			get
			{
				return (string)this[SyncConfigSchema.PasswordFilePath];
			}
		}

		// Token: 0x17001FB7 RID: 8119
		// (get) Token: 0x06005B4B RID: 23371 RVA: 0x0013F925 File Offset: 0x0013DB25
		// (set) Token: 0x06005B4C RID: 23372 RVA: 0x0013F92D File Offset: 0x0013DB2D
		public SmtpDomain FederatedNamespace { get; internal set; }

		// Token: 0x17001FB8 RID: 8120
		// (get) Token: 0x06005B4D RID: 23373 RVA: 0x0013F938 File Offset: 0x0013DB38
		public bool? ResetPasswordOnNextLogon
		{
			get
			{
				if (!this.FederatedTenant)
				{
					return (bool?)this[SyncConfigSchema.ResetPasswordOnNextLogon];
				}
				return null;
			}
		}

		// Token: 0x17001FB9 RID: 8121
		// (get) Token: 0x06005B4E RID: 23374 RVA: 0x0013F967 File Offset: 0x0013DB67
		public SmtpDomainWithSubdomains ProvisioningDomain
		{
			get
			{
				return (SmtpDomainWithSubdomains)this[SyncConfigSchema.ProvisioningDomain];
			}
		}

		// Token: 0x17001FBA RID: 8122
		// (get) Token: 0x06005B4F RID: 23375 RVA: 0x0013F979 File Offset: 0x0013DB79
		public EnterpriseExchangeVersionEnum EnterpriseExchangeVersion
		{
			get
			{
				return (EnterpriseExchangeVersionEnum)this[SyncConfigSchema.EnterpriseExchangeVersion];
			}
		}

		// Token: 0x04003E25 RID: 15909
		internal const string DefaultFederatedIdentitySourceADAttribute = "objectGuid";

		// Token: 0x04003E26 RID: 15910
		internal const string DefaultPasswordFile = "password.xml";

		// Token: 0x04003E27 RID: 15911
		private static SyncConfigSchema schema = ObjectSchema.GetInstance<SyncConfigSchema>();
	}
}
