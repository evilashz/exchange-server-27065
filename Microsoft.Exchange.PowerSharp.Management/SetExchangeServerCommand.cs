using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.Management;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Management.SystemConfigurationTasks;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x02000657 RID: 1623
	public class SetExchangeServerCommand : SyntheticCommandWithPipelineInputNoOutput<ExchangeServer>
	{
		// Token: 0x06005188 RID: 20872 RVA: 0x00080CCC File Offset: 0x0007EECC
		private SetExchangeServerCommand() : base("Set-ExchangeServer")
		{
		}

		// Token: 0x06005189 RID: 20873 RVA: 0x00080CD9 File Offset: 0x0007EED9
		public SetExchangeServerCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x0600518A RID: 20874 RVA: 0x00080CE8 File Offset: 0x0007EEE8
		public virtual SetExchangeServerCommand SetParameters(SetExchangeServerCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x0600518B RID: 20875 RVA: 0x00080CF2 File Offset: 0x0007EEF2
		public virtual SetExchangeServerCommand SetParameters(SetExchangeServerCommand.IdentityParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x02000658 RID: 1624
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x17003093 RID: 12435
			// (set) Token: 0x0600518C RID: 20876 RVA: 0x00080CFC File Offset: 0x0007EEFC
			public virtual ProductKey ProductKey
			{
				set
				{
					base.PowerSharpParameters["ProductKey"] = value;
				}
			}

			// Token: 0x17003094 RID: 12436
			// (set) Token: 0x0600518D RID: 20877 RVA: 0x00080D14 File Offset: 0x0007EF14
			public virtual bool ErrorReportingEnabled
			{
				set
				{
					base.PowerSharpParameters["ErrorReportingEnabled"] = value;
				}
			}

			// Token: 0x17003095 RID: 12437
			// (set) Token: 0x0600518E RID: 20878 RVA: 0x00080D2C File Offset: 0x0007EF2C
			public virtual MailboxRelease MailboxRelease
			{
				set
				{
					base.PowerSharpParameters["MailboxRelease"] = value;
				}
			}

			// Token: 0x17003096 RID: 12438
			// (set) Token: 0x0600518F RID: 20879 RVA: 0x00080D44 File Offset: 0x0007EF44
			public virtual MailboxProvisioningAttributes MailboxProvisioningAttributes
			{
				set
				{
					base.PowerSharpParameters["MailboxProvisioningAttributes"] = value;
				}
			}

			// Token: 0x17003097 RID: 12439
			// (set) Token: 0x06005190 RID: 20880 RVA: 0x00080D57 File Offset: 0x0007EF57
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17003098 RID: 12440
			// (set) Token: 0x06005191 RID: 20881 RVA: 0x00080D6A File Offset: 0x0007EF6A
			public virtual bool? CustomerFeedbackEnabled
			{
				set
				{
					base.PowerSharpParameters["CustomerFeedbackEnabled"] = value;
				}
			}

			// Token: 0x17003099 RID: 12441
			// (set) Token: 0x06005192 RID: 20882 RVA: 0x00080D82 File Offset: 0x0007EF82
			public virtual Uri InternetWebProxy
			{
				set
				{
					base.PowerSharpParameters["InternetWebProxy"] = value;
				}
			}

			// Token: 0x1700309A RID: 12442
			// (set) Token: 0x06005193 RID: 20883 RVA: 0x00080D95 File Offset: 0x0007EF95
			public virtual MultiValuedProperty<string> StaticDomainControllers
			{
				set
				{
					base.PowerSharpParameters["StaticDomainControllers"] = value;
				}
			}

			// Token: 0x1700309B RID: 12443
			// (set) Token: 0x06005194 RID: 20884 RVA: 0x00080DA8 File Offset: 0x0007EFA8
			public virtual MultiValuedProperty<string> StaticGlobalCatalogs
			{
				set
				{
					base.PowerSharpParameters["StaticGlobalCatalogs"] = value;
				}
			}

			// Token: 0x1700309C RID: 12444
			// (set) Token: 0x06005195 RID: 20885 RVA: 0x00080DBB File Offset: 0x0007EFBB
			public virtual string StaticConfigDomainController
			{
				set
				{
					base.PowerSharpParameters["StaticConfigDomainController"] = value;
				}
			}

			// Token: 0x1700309D RID: 12445
			// (set) Token: 0x06005196 RID: 20886 RVA: 0x00080DCE File Offset: 0x0007EFCE
			public virtual MultiValuedProperty<string> StaticExcludedDomainControllers
			{
				set
				{
					base.PowerSharpParameters["StaticExcludedDomainControllers"] = value;
				}
			}

			// Token: 0x1700309E RID: 12446
			// (set) Token: 0x06005197 RID: 20887 RVA: 0x00080DE1 File Offset: 0x0007EFE1
			public virtual string MonitoringGroup
			{
				set
				{
					base.PowerSharpParameters["MonitoringGroup"] = value;
				}
			}

			// Token: 0x1700309F RID: 12447
			// (set) Token: 0x06005198 RID: 20888 RVA: 0x00080DF4 File Offset: 0x0007EFF4
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x170030A0 RID: 12448
			// (set) Token: 0x06005199 RID: 20889 RVA: 0x00080E0C File Offset: 0x0007F00C
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x170030A1 RID: 12449
			// (set) Token: 0x0600519A RID: 20890 RVA: 0x00080E24 File Offset: 0x0007F024
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x170030A2 RID: 12450
			// (set) Token: 0x0600519B RID: 20891 RVA: 0x00080E3C File Offset: 0x0007F03C
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x170030A3 RID: 12451
			// (set) Token: 0x0600519C RID: 20892 RVA: 0x00080E54 File Offset: 0x0007F054
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}

		// Token: 0x02000659 RID: 1625
		public class IdentityParameters : ParametersBase
		{
			// Token: 0x170030A4 RID: 12452
			// (set) Token: 0x0600519E RID: 20894 RVA: 0x00080E74 File Offset: 0x0007F074
			public virtual ServerIdParameter Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = value;
				}
			}

			// Token: 0x170030A5 RID: 12453
			// (set) Token: 0x0600519F RID: 20895 RVA: 0x00080E87 File Offset: 0x0007F087
			public virtual ProductKey ProductKey
			{
				set
				{
					base.PowerSharpParameters["ProductKey"] = value;
				}
			}

			// Token: 0x170030A6 RID: 12454
			// (set) Token: 0x060051A0 RID: 20896 RVA: 0x00080E9F File Offset: 0x0007F09F
			public virtual bool ErrorReportingEnabled
			{
				set
				{
					base.PowerSharpParameters["ErrorReportingEnabled"] = value;
				}
			}

			// Token: 0x170030A7 RID: 12455
			// (set) Token: 0x060051A1 RID: 20897 RVA: 0x00080EB7 File Offset: 0x0007F0B7
			public virtual MailboxRelease MailboxRelease
			{
				set
				{
					base.PowerSharpParameters["MailboxRelease"] = value;
				}
			}

			// Token: 0x170030A8 RID: 12456
			// (set) Token: 0x060051A2 RID: 20898 RVA: 0x00080ECF File Offset: 0x0007F0CF
			public virtual MailboxProvisioningAttributes MailboxProvisioningAttributes
			{
				set
				{
					base.PowerSharpParameters["MailboxProvisioningAttributes"] = value;
				}
			}

			// Token: 0x170030A9 RID: 12457
			// (set) Token: 0x060051A3 RID: 20899 RVA: 0x00080EE2 File Offset: 0x0007F0E2
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x170030AA RID: 12458
			// (set) Token: 0x060051A4 RID: 20900 RVA: 0x00080EF5 File Offset: 0x0007F0F5
			public virtual bool? CustomerFeedbackEnabled
			{
				set
				{
					base.PowerSharpParameters["CustomerFeedbackEnabled"] = value;
				}
			}

			// Token: 0x170030AB RID: 12459
			// (set) Token: 0x060051A5 RID: 20901 RVA: 0x00080F0D File Offset: 0x0007F10D
			public virtual Uri InternetWebProxy
			{
				set
				{
					base.PowerSharpParameters["InternetWebProxy"] = value;
				}
			}

			// Token: 0x170030AC RID: 12460
			// (set) Token: 0x060051A6 RID: 20902 RVA: 0x00080F20 File Offset: 0x0007F120
			public virtual MultiValuedProperty<string> StaticDomainControllers
			{
				set
				{
					base.PowerSharpParameters["StaticDomainControllers"] = value;
				}
			}

			// Token: 0x170030AD RID: 12461
			// (set) Token: 0x060051A7 RID: 20903 RVA: 0x00080F33 File Offset: 0x0007F133
			public virtual MultiValuedProperty<string> StaticGlobalCatalogs
			{
				set
				{
					base.PowerSharpParameters["StaticGlobalCatalogs"] = value;
				}
			}

			// Token: 0x170030AE RID: 12462
			// (set) Token: 0x060051A8 RID: 20904 RVA: 0x00080F46 File Offset: 0x0007F146
			public virtual string StaticConfigDomainController
			{
				set
				{
					base.PowerSharpParameters["StaticConfigDomainController"] = value;
				}
			}

			// Token: 0x170030AF RID: 12463
			// (set) Token: 0x060051A9 RID: 20905 RVA: 0x00080F59 File Offset: 0x0007F159
			public virtual MultiValuedProperty<string> StaticExcludedDomainControllers
			{
				set
				{
					base.PowerSharpParameters["StaticExcludedDomainControllers"] = value;
				}
			}

			// Token: 0x170030B0 RID: 12464
			// (set) Token: 0x060051AA RID: 20906 RVA: 0x00080F6C File Offset: 0x0007F16C
			public virtual string MonitoringGroup
			{
				set
				{
					base.PowerSharpParameters["MonitoringGroup"] = value;
				}
			}

			// Token: 0x170030B1 RID: 12465
			// (set) Token: 0x060051AB RID: 20907 RVA: 0x00080F7F File Offset: 0x0007F17F
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x170030B2 RID: 12466
			// (set) Token: 0x060051AC RID: 20908 RVA: 0x00080F97 File Offset: 0x0007F197
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x170030B3 RID: 12467
			// (set) Token: 0x060051AD RID: 20909 RVA: 0x00080FAF File Offset: 0x0007F1AF
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x170030B4 RID: 12468
			// (set) Token: 0x060051AE RID: 20910 RVA: 0x00080FC7 File Offset: 0x0007F1C7
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x170030B5 RID: 12469
			// (set) Token: 0x060051AF RID: 20911 RVA: 0x00080FDF File Offset: 0x0007F1DF
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
