using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x020003F2 RID: 1010
	public class SetIRMConfigurationCommand : SyntheticCommandWithPipelineInputNoOutput<IRMConfiguration>
	{
		// Token: 0x06003BF7 RID: 15351 RVA: 0x00065995 File Offset: 0x00063B95
		private SetIRMConfigurationCommand() : base("Set-IRMConfiguration")
		{
		}

		// Token: 0x06003BF8 RID: 15352 RVA: 0x000659A2 File Offset: 0x00063BA2
		public SetIRMConfigurationCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x06003BF9 RID: 15353 RVA: 0x000659B1 File Offset: 0x00063BB1
		public virtual SetIRMConfigurationCommand SetParameters(SetIRMConfigurationCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x06003BFA RID: 15354 RVA: 0x000659BB File Offset: 0x00063BBB
		public virtual SetIRMConfigurationCommand SetParameters(SetIRMConfigurationCommand.IdentityParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x020003F3 RID: 1011
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x17001FCC RID: 8140
			// (set) Token: 0x06003BFB RID: 15355 RVA: 0x000659C5 File Offset: 0x00063BC5
			public virtual SwitchParameter Force
			{
				set
				{
					base.PowerSharpParameters["Force"] = value;
				}
			}

			// Token: 0x17001FCD RID: 8141
			// (set) Token: 0x06003BFC RID: 15356 RVA: 0x000659DD File Offset: 0x00063BDD
			public virtual SwitchParameter RefreshServerCertificates
			{
				set
				{
					base.PowerSharpParameters["RefreshServerCertificates"] = value;
				}
			}

			// Token: 0x17001FCE RID: 8142
			// (set) Token: 0x06003BFD RID: 15357 RVA: 0x000659F5 File Offset: 0x00063BF5
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17001FCF RID: 8143
			// (set) Token: 0x06003BFE RID: 15358 RVA: 0x00065A08 File Offset: 0x00063C08
			public virtual TransportDecryptionSetting TransportDecryptionSetting
			{
				set
				{
					base.PowerSharpParameters["TransportDecryptionSetting"] = value;
				}
			}

			// Token: 0x17001FD0 RID: 8144
			// (set) Token: 0x06003BFF RID: 15359 RVA: 0x00065A20 File Offset: 0x00063C20
			public virtual Uri ServiceLocation
			{
				set
				{
					base.PowerSharpParameters["ServiceLocation"] = value;
				}
			}

			// Token: 0x17001FD1 RID: 8145
			// (set) Token: 0x06003C00 RID: 15360 RVA: 0x00065A33 File Offset: 0x00063C33
			public virtual Uri PublishingLocation
			{
				set
				{
					base.PowerSharpParameters["PublishingLocation"] = value;
				}
			}

			// Token: 0x17001FD2 RID: 8146
			// (set) Token: 0x06003C01 RID: 15361 RVA: 0x00065A46 File Offset: 0x00063C46
			public virtual MultiValuedProperty<Uri> LicensingLocation
			{
				set
				{
					base.PowerSharpParameters["LicensingLocation"] = value;
				}
			}

			// Token: 0x17001FD3 RID: 8147
			// (set) Token: 0x06003C02 RID: 15362 RVA: 0x00065A59 File Offset: 0x00063C59
			public virtual bool JournalReportDecryptionEnabled
			{
				set
				{
					base.PowerSharpParameters["JournalReportDecryptionEnabled"] = value;
				}
			}

			// Token: 0x17001FD4 RID: 8148
			// (set) Token: 0x06003C03 RID: 15363 RVA: 0x00065A71 File Offset: 0x00063C71
			public virtual bool ExternalLicensingEnabled
			{
				set
				{
					base.PowerSharpParameters["ExternalLicensingEnabled"] = value;
				}
			}

			// Token: 0x17001FD5 RID: 8149
			// (set) Token: 0x06003C04 RID: 15364 RVA: 0x00065A89 File Offset: 0x00063C89
			public virtual bool InternalLicensingEnabled
			{
				set
				{
					base.PowerSharpParameters["InternalLicensingEnabled"] = value;
				}
			}

			// Token: 0x17001FD6 RID: 8150
			// (set) Token: 0x06003C05 RID: 15365 RVA: 0x00065AA1 File Offset: 0x00063CA1
			public virtual bool SearchEnabled
			{
				set
				{
					base.PowerSharpParameters["SearchEnabled"] = value;
				}
			}

			// Token: 0x17001FD7 RID: 8151
			// (set) Token: 0x06003C06 RID: 15366 RVA: 0x00065AB9 File Offset: 0x00063CB9
			public virtual bool ClientAccessServerEnabled
			{
				set
				{
					base.PowerSharpParameters["ClientAccessServerEnabled"] = value;
				}
			}

			// Token: 0x17001FD8 RID: 8152
			// (set) Token: 0x06003C07 RID: 15367 RVA: 0x00065AD1 File Offset: 0x00063CD1
			public virtual bool EDiscoverySuperUserEnabled
			{
				set
				{
					base.PowerSharpParameters["EDiscoverySuperUserEnabled"] = value;
				}
			}

			// Token: 0x17001FD9 RID: 8153
			// (set) Token: 0x06003C08 RID: 15368 RVA: 0x00065AE9 File Offset: 0x00063CE9
			public virtual Uri RMSOnlineKeySharingLocation
			{
				set
				{
					base.PowerSharpParameters["RMSOnlineKeySharingLocation"] = value;
				}
			}

			// Token: 0x17001FDA RID: 8154
			// (set) Token: 0x06003C09 RID: 15369 RVA: 0x00065AFC File Offset: 0x00063CFC
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17001FDB RID: 8155
			// (set) Token: 0x06003C0A RID: 15370 RVA: 0x00065B14 File Offset: 0x00063D14
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17001FDC RID: 8156
			// (set) Token: 0x06003C0B RID: 15371 RVA: 0x00065B2C File Offset: 0x00063D2C
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17001FDD RID: 8157
			// (set) Token: 0x06003C0C RID: 15372 RVA: 0x00065B44 File Offset: 0x00063D44
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17001FDE RID: 8158
			// (set) Token: 0x06003C0D RID: 15373 RVA: 0x00065B5C File Offset: 0x00063D5C
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}

		// Token: 0x020003F4 RID: 1012
		public class IdentityParameters : ParametersBase
		{
			// Token: 0x17001FDF RID: 8159
			// (set) Token: 0x06003C0F RID: 15375 RVA: 0x00065B7C File Offset: 0x00063D7C
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x17001FE0 RID: 8160
			// (set) Token: 0x06003C10 RID: 15376 RVA: 0x00065B9A File Offset: 0x00063D9A
			public virtual SwitchParameter Force
			{
				set
				{
					base.PowerSharpParameters["Force"] = value;
				}
			}

			// Token: 0x17001FE1 RID: 8161
			// (set) Token: 0x06003C11 RID: 15377 RVA: 0x00065BB2 File Offset: 0x00063DB2
			public virtual SwitchParameter RefreshServerCertificates
			{
				set
				{
					base.PowerSharpParameters["RefreshServerCertificates"] = value;
				}
			}

			// Token: 0x17001FE2 RID: 8162
			// (set) Token: 0x06003C12 RID: 15378 RVA: 0x00065BCA File Offset: 0x00063DCA
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17001FE3 RID: 8163
			// (set) Token: 0x06003C13 RID: 15379 RVA: 0x00065BDD File Offset: 0x00063DDD
			public virtual TransportDecryptionSetting TransportDecryptionSetting
			{
				set
				{
					base.PowerSharpParameters["TransportDecryptionSetting"] = value;
				}
			}

			// Token: 0x17001FE4 RID: 8164
			// (set) Token: 0x06003C14 RID: 15380 RVA: 0x00065BF5 File Offset: 0x00063DF5
			public virtual Uri ServiceLocation
			{
				set
				{
					base.PowerSharpParameters["ServiceLocation"] = value;
				}
			}

			// Token: 0x17001FE5 RID: 8165
			// (set) Token: 0x06003C15 RID: 15381 RVA: 0x00065C08 File Offset: 0x00063E08
			public virtual Uri PublishingLocation
			{
				set
				{
					base.PowerSharpParameters["PublishingLocation"] = value;
				}
			}

			// Token: 0x17001FE6 RID: 8166
			// (set) Token: 0x06003C16 RID: 15382 RVA: 0x00065C1B File Offset: 0x00063E1B
			public virtual MultiValuedProperty<Uri> LicensingLocation
			{
				set
				{
					base.PowerSharpParameters["LicensingLocation"] = value;
				}
			}

			// Token: 0x17001FE7 RID: 8167
			// (set) Token: 0x06003C17 RID: 15383 RVA: 0x00065C2E File Offset: 0x00063E2E
			public virtual bool JournalReportDecryptionEnabled
			{
				set
				{
					base.PowerSharpParameters["JournalReportDecryptionEnabled"] = value;
				}
			}

			// Token: 0x17001FE8 RID: 8168
			// (set) Token: 0x06003C18 RID: 15384 RVA: 0x00065C46 File Offset: 0x00063E46
			public virtual bool ExternalLicensingEnabled
			{
				set
				{
					base.PowerSharpParameters["ExternalLicensingEnabled"] = value;
				}
			}

			// Token: 0x17001FE9 RID: 8169
			// (set) Token: 0x06003C19 RID: 15385 RVA: 0x00065C5E File Offset: 0x00063E5E
			public virtual bool InternalLicensingEnabled
			{
				set
				{
					base.PowerSharpParameters["InternalLicensingEnabled"] = value;
				}
			}

			// Token: 0x17001FEA RID: 8170
			// (set) Token: 0x06003C1A RID: 15386 RVA: 0x00065C76 File Offset: 0x00063E76
			public virtual bool SearchEnabled
			{
				set
				{
					base.PowerSharpParameters["SearchEnabled"] = value;
				}
			}

			// Token: 0x17001FEB RID: 8171
			// (set) Token: 0x06003C1B RID: 15387 RVA: 0x00065C8E File Offset: 0x00063E8E
			public virtual bool ClientAccessServerEnabled
			{
				set
				{
					base.PowerSharpParameters["ClientAccessServerEnabled"] = value;
				}
			}

			// Token: 0x17001FEC RID: 8172
			// (set) Token: 0x06003C1C RID: 15388 RVA: 0x00065CA6 File Offset: 0x00063EA6
			public virtual bool EDiscoverySuperUserEnabled
			{
				set
				{
					base.PowerSharpParameters["EDiscoverySuperUserEnabled"] = value;
				}
			}

			// Token: 0x17001FED RID: 8173
			// (set) Token: 0x06003C1D RID: 15389 RVA: 0x00065CBE File Offset: 0x00063EBE
			public virtual Uri RMSOnlineKeySharingLocation
			{
				set
				{
					base.PowerSharpParameters["RMSOnlineKeySharingLocation"] = value;
				}
			}

			// Token: 0x17001FEE RID: 8174
			// (set) Token: 0x06003C1E RID: 15390 RVA: 0x00065CD1 File Offset: 0x00063ED1
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17001FEF RID: 8175
			// (set) Token: 0x06003C1F RID: 15391 RVA: 0x00065CE9 File Offset: 0x00063EE9
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17001FF0 RID: 8176
			// (set) Token: 0x06003C20 RID: 15392 RVA: 0x00065D01 File Offset: 0x00063F01
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17001FF1 RID: 8177
			// (set) Token: 0x06003C21 RID: 15393 RVA: 0x00065D19 File Offset: 0x00063F19
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17001FF2 RID: 8178
			// (set) Token: 0x06003C22 RID: 15394 RVA: 0x00065D31 File Offset: 0x00063F31
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
