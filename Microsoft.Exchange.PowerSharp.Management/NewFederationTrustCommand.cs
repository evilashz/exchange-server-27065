using System;
using System.Management.Automation;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x02000689 RID: 1673
	public class NewFederationTrustCommand : SyntheticCommandWithPipelineInput<FederationTrust, FederationTrust>
	{
		// Token: 0x060058EA RID: 22762 RVA: 0x0008B273 File Offset: 0x00089473
		private NewFederationTrustCommand() : base("New-FederationTrust")
		{
		}

		// Token: 0x060058EB RID: 22763 RVA: 0x0008B280 File Offset: 0x00089480
		public NewFederationTrustCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x060058EC RID: 22764 RVA: 0x0008B28F File Offset: 0x0008948F
		public virtual NewFederationTrustCommand SetParameters(NewFederationTrustCommand.SkipNamespaceProviderProvisioningParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x060058ED RID: 22765 RVA: 0x0008B299 File Offset: 0x00089499
		public virtual NewFederationTrustCommand SetParameters(NewFederationTrustCommand.FederationTrustParameterParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x060058EE RID: 22766 RVA: 0x0008B2A3 File Offset: 0x000894A3
		public virtual NewFederationTrustCommand SetParameters(NewFederationTrustCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x0200068A RID: 1674
		public class SkipNamespaceProviderProvisioningParameters : ParametersBase
		{
			// Token: 0x17003791 RID: 14225
			// (set) Token: 0x060058EF RID: 22767 RVA: 0x0008B2AD File Offset: 0x000894AD
			public virtual string ApplicationIdentifier
			{
				set
				{
					base.PowerSharpParameters["ApplicationIdentifier"] = value;
				}
			}

			// Token: 0x17003792 RID: 14226
			// (set) Token: 0x060058F0 RID: 22768 RVA: 0x0008B2C0 File Offset: 0x000894C0
			public virtual string AdministratorProvisioningId
			{
				set
				{
					base.PowerSharpParameters["AdministratorProvisioningId"] = value;
				}
			}

			// Token: 0x17003793 RID: 14227
			// (set) Token: 0x060058F1 RID: 22769 RVA: 0x0008B2D3 File Offset: 0x000894D3
			public virtual SwitchParameter SkipNamespaceProviderProvisioning
			{
				set
				{
					base.PowerSharpParameters["SkipNamespaceProviderProvisioning"] = value;
				}
			}

			// Token: 0x17003794 RID: 14228
			// (set) Token: 0x060058F2 RID: 22770 RVA: 0x0008B2EB File Offset: 0x000894EB
			public virtual string ApplicationUri
			{
				set
				{
					base.PowerSharpParameters["ApplicationUri"] = value;
				}
			}

			// Token: 0x17003795 RID: 14229
			// (set) Token: 0x060058F3 RID: 22771 RVA: 0x0008B2FE File Offset: 0x000894FE
			public virtual string Thumbprint
			{
				set
				{
					base.PowerSharpParameters["Thumbprint"] = value;
				}
			}

			// Token: 0x17003796 RID: 14230
			// (set) Token: 0x060058F4 RID: 22772 RVA: 0x0008B311 File Offset: 0x00089511
			public virtual Uri MetadataUrl
			{
				set
				{
					base.PowerSharpParameters["MetadataUrl"] = value;
				}
			}

			// Token: 0x17003797 RID: 14231
			// (set) Token: 0x060058F5 RID: 22773 RVA: 0x0008B324 File Offset: 0x00089524
			public virtual string Name
			{
				set
				{
					base.PowerSharpParameters["Name"] = value;
				}
			}

			// Token: 0x17003798 RID: 14232
			// (set) Token: 0x060058F6 RID: 22774 RVA: 0x0008B337 File Offset: 0x00089537
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17003799 RID: 14233
			// (set) Token: 0x060058F7 RID: 22775 RVA: 0x0008B34A File Offset: 0x0008954A
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x1700379A RID: 14234
			// (set) Token: 0x060058F8 RID: 22776 RVA: 0x0008B362 File Offset: 0x00089562
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x1700379B RID: 14235
			// (set) Token: 0x060058F9 RID: 22777 RVA: 0x0008B37A File Offset: 0x0008957A
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x1700379C RID: 14236
			// (set) Token: 0x060058FA RID: 22778 RVA: 0x0008B392 File Offset: 0x00089592
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x1700379D RID: 14237
			// (set) Token: 0x060058FB RID: 22779 RVA: 0x0008B3AA File Offset: 0x000895AA
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}

		// Token: 0x0200068B RID: 1675
		public class FederationTrustParameterParameters : ParametersBase
		{
			// Token: 0x1700379E RID: 14238
			// (set) Token: 0x060058FD RID: 22781 RVA: 0x0008B3CA File Offset: 0x000895CA
			public virtual string Thumbprint
			{
				set
				{
					base.PowerSharpParameters["Thumbprint"] = value;
				}
			}

			// Token: 0x1700379F RID: 14239
			// (set) Token: 0x060058FE RID: 22782 RVA: 0x0008B3DD File Offset: 0x000895DD
			public virtual Uri MetadataUrl
			{
				set
				{
					base.PowerSharpParameters["MetadataUrl"] = value;
				}
			}

			// Token: 0x170037A0 RID: 14240
			// (set) Token: 0x060058FF RID: 22783 RVA: 0x0008B3F0 File Offset: 0x000895F0
			public virtual SwitchParameter UseLegacyProvisioningService
			{
				set
				{
					base.PowerSharpParameters["UseLegacyProvisioningService"] = value;
				}
			}

			// Token: 0x170037A1 RID: 14241
			// (set) Token: 0x06005900 RID: 22784 RVA: 0x0008B408 File Offset: 0x00089608
			public virtual string Name
			{
				set
				{
					base.PowerSharpParameters["Name"] = value;
				}
			}

			// Token: 0x170037A2 RID: 14242
			// (set) Token: 0x06005901 RID: 22785 RVA: 0x0008B41B File Offset: 0x0008961B
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x170037A3 RID: 14243
			// (set) Token: 0x06005902 RID: 22786 RVA: 0x0008B42E File Offset: 0x0008962E
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x170037A4 RID: 14244
			// (set) Token: 0x06005903 RID: 22787 RVA: 0x0008B446 File Offset: 0x00089646
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x170037A5 RID: 14245
			// (set) Token: 0x06005904 RID: 22788 RVA: 0x0008B45E File Offset: 0x0008965E
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x170037A6 RID: 14246
			// (set) Token: 0x06005905 RID: 22789 RVA: 0x0008B476 File Offset: 0x00089676
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x170037A7 RID: 14247
			// (set) Token: 0x06005906 RID: 22790 RVA: 0x0008B48E File Offset: 0x0008968E
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}

		// Token: 0x0200068C RID: 1676
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x170037A8 RID: 14248
			// (set) Token: 0x06005908 RID: 22792 RVA: 0x0008B4AE File Offset: 0x000896AE
			public virtual string Name
			{
				set
				{
					base.PowerSharpParameters["Name"] = value;
				}
			}

			// Token: 0x170037A9 RID: 14249
			// (set) Token: 0x06005909 RID: 22793 RVA: 0x0008B4C1 File Offset: 0x000896C1
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x170037AA RID: 14250
			// (set) Token: 0x0600590A RID: 22794 RVA: 0x0008B4D4 File Offset: 0x000896D4
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x170037AB RID: 14251
			// (set) Token: 0x0600590B RID: 22795 RVA: 0x0008B4EC File Offset: 0x000896EC
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x170037AC RID: 14252
			// (set) Token: 0x0600590C RID: 22796 RVA: 0x0008B504 File Offset: 0x00089704
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x170037AD RID: 14253
			// (set) Token: 0x0600590D RID: 22797 RVA: 0x0008B51C File Offset: 0x0008971C
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x170037AE RID: 14254
			// (set) Token: 0x0600590E RID: 22798 RVA: 0x0008B534 File Offset: 0x00089734
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}
	}
}
