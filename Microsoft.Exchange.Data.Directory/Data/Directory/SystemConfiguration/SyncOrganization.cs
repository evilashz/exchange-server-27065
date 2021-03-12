using System;
using System.Management.Automation;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x020005B6 RID: 1462
	[ObjectScope(new ConfigScopes[]
	{
		ConfigScopes.TenantLocal,
		ConfigScopes.TenantSubTree
	})]
	[Serializable]
	public sealed class SyncOrganization : ADLegacyVersionableObject
	{
		// Token: 0x170015FD RID: 5629
		// (get) Token: 0x0600433B RID: 17211 RVA: 0x000FCA1D File Offset: 0x000FAC1D
		internal override string MostDerivedObjectClass
		{
			get
			{
				return ExchangeConfigurationUnit.MostDerivedClass;
			}
		}

		// Token: 0x170015FE RID: 5630
		// (get) Token: 0x0600433C RID: 17212 RVA: 0x000FCA24 File Offset: 0x000FAC24
		internal override ADObjectSchema Schema
		{
			get
			{
				return SyncOrganization.schema;
			}
		}

		// Token: 0x170015FF RID: 5631
		// (get) Token: 0x0600433D RID: 17213 RVA: 0x000FCA2B File Offset: 0x000FAC2B
		public new string Name
		{
			get
			{
				return base.Name;
			}
		}

		// Token: 0x17001600 RID: 5632
		// (get) Token: 0x0600433E RID: 17214 RVA: 0x000FCA33 File Offset: 0x000FAC33
		// (set) Token: 0x0600433F RID: 17215 RVA: 0x000FCA45 File Offset: 0x000FAC45
		[Parameter(Mandatory = false, ParameterSetName = "Managed")]
		public bool DisableWindowsLiveID
		{
			get
			{
				return (bool)this[SyncOrganizationSchema.DisableWindowsLiveID];
			}
			set
			{
				this[SyncOrganizationSchema.DisableWindowsLiveID] = value;
			}
		}

		// Token: 0x17001601 RID: 5633
		// (get) Token: 0x06004340 RID: 17216 RVA: 0x000FCA58 File Offset: 0x000FAC58
		// (set) Token: 0x06004341 RID: 17217 RVA: 0x000FCA6A File Offset: 0x000FAC6A
		[Parameter(Mandatory = false, ParameterSetName = "Federated")]
		[ValidateNotNullOrEmpty]
		public string FederatedIdentitySourceADAttribute
		{
			get
			{
				return (string)this[SyncOrganizationSchema.FederatedIdentitySourceADAttribute];
			}
			set
			{
				this[SyncOrganizationSchema.FederatedIdentitySourceADAttribute] = value;
			}
		}

		// Token: 0x17001602 RID: 5634
		// (get) Token: 0x06004342 RID: 17218 RVA: 0x000FCA78 File Offset: 0x000FAC78
		// (set) Token: 0x06004343 RID: 17219 RVA: 0x000FCA8A File Offset: 0x000FAC8A
		[Parameter]
		public bool WlidUseSMTPPrimary
		{
			get
			{
				return (bool)this[SyncOrganizationSchema.WlidUseSMTPPrimary];
			}
			set
			{
				this[SyncOrganizationSchema.WlidUseSMTPPrimary] = value;
			}
		}

		// Token: 0x17001603 RID: 5635
		// (get) Token: 0x06004344 RID: 17220 RVA: 0x000FCA9D File Offset: 0x000FAC9D
		// (set) Token: 0x06004345 RID: 17221 RVA: 0x000FCAAF File Offset: 0x000FACAF
		[ValidateNotNullOrEmpty]
		[Parameter(Mandatory = false, ParameterSetName = "Managed")]
		public string PasswordFilePath
		{
			get
			{
				return (string)this[SyncOrganizationSchema.PasswordFilePath];
			}
			set
			{
				this[SyncOrganizationSchema.PasswordFilePath] = value;
			}
		}

		// Token: 0x17001604 RID: 5636
		// (get) Token: 0x06004346 RID: 17222 RVA: 0x000FCABD File Offset: 0x000FACBD
		// (set) Token: 0x06004347 RID: 17223 RVA: 0x000FCACF File Offset: 0x000FACCF
		[Parameter(Mandatory = false, ParameterSetName = "Managed")]
		public bool ResetPasswordOnNextLogon
		{
			get
			{
				return (bool)this[SyncOrganizationSchema.ResetPasswordOnNextLogon];
			}
			set
			{
				this[SyncOrganizationSchema.ResetPasswordOnNextLogon] = value;
			}
		}

		// Token: 0x17001605 RID: 5637
		// (get) Token: 0x06004348 RID: 17224 RVA: 0x000FCAE2 File Offset: 0x000FACE2
		// (set) Token: 0x06004349 RID: 17225 RVA: 0x000FCAF4 File Offset: 0x000FACF4
		[ValidateNotNullOrEmpty]
		[Parameter]
		public SmtpDomainWithSubdomains ProvisioningDomain
		{
			get
			{
				return (SmtpDomainWithSubdomains)this[SyncOrganizationSchema.ProvisioningDomain];
			}
			set
			{
				this[SyncOrganizationSchema.ProvisioningDomain] = value;
			}
		}

		// Token: 0x17001606 RID: 5638
		// (get) Token: 0x0600434A RID: 17226 RVA: 0x000FCB02 File Offset: 0x000FAD02
		// (set) Token: 0x0600434B RID: 17227 RVA: 0x000FCB14 File Offset: 0x000FAD14
		[Parameter]
		public EnterpriseExchangeVersionEnum EnterpriseExchangeVersion
		{
			get
			{
				return (EnterpriseExchangeVersionEnum)this[SyncOrganizationSchema.EnterpriseExchangeVersion];
			}
			set
			{
				this[SyncOrganizationSchema.EnterpriseExchangeVersion] = value;
			}
		}

		// Token: 0x17001607 RID: 5639
		// (get) Token: 0x0600434C RID: 17228 RVA: 0x000FCB27 File Offset: 0x000FAD27
		public bool FederatedTenant
		{
			get
			{
				return (bool)this[SyncOrganizationSchema.FederatedTenant];
			}
		}

		// Token: 0x04002DA6 RID: 11686
		private static SyncOrganizationSchema schema = ObjectSchema.GetInstance<SyncOrganizationSchema>();
	}
}
