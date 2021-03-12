using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x02000992 RID: 2450
	public class SetPowerShellVirtualDirectoryCommand : SyntheticCommandWithPipelineInputNoOutput<ADPowerShellVirtualDirectory>
	{
		// Token: 0x06007B52 RID: 31570 RVA: 0x000B7DA2 File Offset: 0x000B5FA2
		private SetPowerShellVirtualDirectoryCommand() : base("Set-PowerShellVirtualDirectory")
		{
		}

		// Token: 0x06007B53 RID: 31571 RVA: 0x000B7DAF File Offset: 0x000B5FAF
		public SetPowerShellVirtualDirectoryCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x06007B54 RID: 31572 RVA: 0x000B7DBE File Offset: 0x000B5FBE
		public virtual SetPowerShellVirtualDirectoryCommand SetParameters(SetPowerShellVirtualDirectoryCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x06007B55 RID: 31573 RVA: 0x000B7DC8 File Offset: 0x000B5FC8
		public virtual SetPowerShellVirtualDirectoryCommand SetParameters(SetPowerShellVirtualDirectoryCommand.IdentityParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x02000993 RID: 2451
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x170053E7 RID: 21479
			// (set) Token: 0x06007B56 RID: 31574 RVA: 0x000B7DD2 File Offset: 0x000B5FD2
			public virtual bool EnableSessionKeyRedirectionModule
			{
				set
				{
					base.PowerSharpParameters["EnableSessionKeyRedirectionModule"] = value;
				}
			}

			// Token: 0x170053E8 RID: 21480
			// (set) Token: 0x06007B57 RID: 31575 RVA: 0x000B7DEA File Offset: 0x000B5FEA
			public virtual bool EnableDelegatedAuthModule
			{
				set
				{
					base.PowerSharpParameters["EnableDelegatedAuthModule"] = value;
				}
			}

			// Token: 0x170053E9 RID: 21481
			// (set) Token: 0x06007B58 RID: 31576 RVA: 0x000B7E02 File Offset: 0x000B6002
			public virtual bool RequireSSL
			{
				set
				{
					base.PowerSharpParameters["RequireSSL"] = value;
				}
			}

			// Token: 0x170053EA RID: 21482
			// (set) Token: 0x06007B59 RID: 31577 RVA: 0x000B7E1A File Offset: 0x000B601A
			public virtual bool CertificateAuthentication
			{
				set
				{
					base.PowerSharpParameters["CertificateAuthentication"] = value;
				}
			}

			// Token: 0x170053EB RID: 21483
			// (set) Token: 0x06007B5A RID: 31578 RVA: 0x000B7E32 File Offset: 0x000B6032
			public virtual bool EnableCertificateHeaderAuthModule
			{
				set
				{
					base.PowerSharpParameters["EnableCertificateHeaderAuthModule"] = value;
				}
			}

			// Token: 0x170053EC RID: 21484
			// (set) Token: 0x06007B5B RID: 31579 RVA: 0x000B7E4A File Offset: 0x000B604A
			public virtual bool LiveIdBasicAuthentication
			{
				set
				{
					base.PowerSharpParameters["LiveIdBasicAuthentication"] = value;
				}
			}

			// Token: 0x170053ED RID: 21485
			// (set) Token: 0x06007B5C RID: 31580 RVA: 0x000B7E62 File Offset: 0x000B6062
			public virtual bool LiveIdNegotiateAuthentication
			{
				set
				{
					base.PowerSharpParameters["LiveIdNegotiateAuthentication"] = value;
				}
			}

			// Token: 0x170053EE RID: 21486
			// (set) Token: 0x06007B5D RID: 31581 RVA: 0x000B7E7A File Offset: 0x000B607A
			public virtual bool BasicAuthentication
			{
				set
				{
					base.PowerSharpParameters["BasicAuthentication"] = value;
				}
			}

			// Token: 0x170053EF RID: 21487
			// (set) Token: 0x06007B5E RID: 31582 RVA: 0x000B7E92 File Offset: 0x000B6092
			public virtual bool WindowsAuthentication
			{
				set
				{
					base.PowerSharpParameters["WindowsAuthentication"] = value;
				}
			}

			// Token: 0x170053F0 RID: 21488
			// (set) Token: 0x06007B5F RID: 31583 RVA: 0x000B7EAA File Offset: 0x000B60AA
			public virtual Uri InternalUrl
			{
				set
				{
					base.PowerSharpParameters["InternalUrl"] = value;
				}
			}

			// Token: 0x170053F1 RID: 21489
			// (set) Token: 0x06007B60 RID: 31584 RVA: 0x000B7EBD File Offset: 0x000B60BD
			public virtual Uri ExternalUrl
			{
				set
				{
					base.PowerSharpParameters["ExternalUrl"] = value;
				}
			}

			// Token: 0x170053F2 RID: 21490
			// (set) Token: 0x06007B61 RID: 31585 RVA: 0x000B7ED0 File Offset: 0x000B60D0
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x170053F3 RID: 21491
			// (set) Token: 0x06007B62 RID: 31586 RVA: 0x000B7EE3 File Offset: 0x000B60E3
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x170053F4 RID: 21492
			// (set) Token: 0x06007B63 RID: 31587 RVA: 0x000B7EFB File Offset: 0x000B60FB
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x170053F5 RID: 21493
			// (set) Token: 0x06007B64 RID: 31588 RVA: 0x000B7F13 File Offset: 0x000B6113
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x170053F6 RID: 21494
			// (set) Token: 0x06007B65 RID: 31589 RVA: 0x000B7F2B File Offset: 0x000B612B
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x170053F7 RID: 21495
			// (set) Token: 0x06007B66 RID: 31590 RVA: 0x000B7F43 File Offset: 0x000B6143
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}

		// Token: 0x02000994 RID: 2452
		public class IdentityParameters : ParametersBase
		{
			// Token: 0x170053F8 RID: 21496
			// (set) Token: 0x06007B68 RID: 31592 RVA: 0x000B7F63 File Offset: 0x000B6163
			public virtual VirtualDirectoryIdParameter Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = value;
				}
			}

			// Token: 0x170053F9 RID: 21497
			// (set) Token: 0x06007B69 RID: 31593 RVA: 0x000B7F76 File Offset: 0x000B6176
			public virtual bool EnableSessionKeyRedirectionModule
			{
				set
				{
					base.PowerSharpParameters["EnableSessionKeyRedirectionModule"] = value;
				}
			}

			// Token: 0x170053FA RID: 21498
			// (set) Token: 0x06007B6A RID: 31594 RVA: 0x000B7F8E File Offset: 0x000B618E
			public virtual bool EnableDelegatedAuthModule
			{
				set
				{
					base.PowerSharpParameters["EnableDelegatedAuthModule"] = value;
				}
			}

			// Token: 0x170053FB RID: 21499
			// (set) Token: 0x06007B6B RID: 31595 RVA: 0x000B7FA6 File Offset: 0x000B61A6
			public virtual bool RequireSSL
			{
				set
				{
					base.PowerSharpParameters["RequireSSL"] = value;
				}
			}

			// Token: 0x170053FC RID: 21500
			// (set) Token: 0x06007B6C RID: 31596 RVA: 0x000B7FBE File Offset: 0x000B61BE
			public virtual bool CertificateAuthentication
			{
				set
				{
					base.PowerSharpParameters["CertificateAuthentication"] = value;
				}
			}

			// Token: 0x170053FD RID: 21501
			// (set) Token: 0x06007B6D RID: 31597 RVA: 0x000B7FD6 File Offset: 0x000B61D6
			public virtual bool EnableCertificateHeaderAuthModule
			{
				set
				{
					base.PowerSharpParameters["EnableCertificateHeaderAuthModule"] = value;
				}
			}

			// Token: 0x170053FE RID: 21502
			// (set) Token: 0x06007B6E RID: 31598 RVA: 0x000B7FEE File Offset: 0x000B61EE
			public virtual bool LiveIdBasicAuthentication
			{
				set
				{
					base.PowerSharpParameters["LiveIdBasicAuthentication"] = value;
				}
			}

			// Token: 0x170053FF RID: 21503
			// (set) Token: 0x06007B6F RID: 31599 RVA: 0x000B8006 File Offset: 0x000B6206
			public virtual bool LiveIdNegotiateAuthentication
			{
				set
				{
					base.PowerSharpParameters["LiveIdNegotiateAuthentication"] = value;
				}
			}

			// Token: 0x17005400 RID: 21504
			// (set) Token: 0x06007B70 RID: 31600 RVA: 0x000B801E File Offset: 0x000B621E
			public virtual bool BasicAuthentication
			{
				set
				{
					base.PowerSharpParameters["BasicAuthentication"] = value;
				}
			}

			// Token: 0x17005401 RID: 21505
			// (set) Token: 0x06007B71 RID: 31601 RVA: 0x000B8036 File Offset: 0x000B6236
			public virtual bool WindowsAuthentication
			{
				set
				{
					base.PowerSharpParameters["WindowsAuthentication"] = value;
				}
			}

			// Token: 0x17005402 RID: 21506
			// (set) Token: 0x06007B72 RID: 31602 RVA: 0x000B804E File Offset: 0x000B624E
			public virtual Uri InternalUrl
			{
				set
				{
					base.PowerSharpParameters["InternalUrl"] = value;
				}
			}

			// Token: 0x17005403 RID: 21507
			// (set) Token: 0x06007B73 RID: 31603 RVA: 0x000B8061 File Offset: 0x000B6261
			public virtual Uri ExternalUrl
			{
				set
				{
					base.PowerSharpParameters["ExternalUrl"] = value;
				}
			}

			// Token: 0x17005404 RID: 21508
			// (set) Token: 0x06007B74 RID: 31604 RVA: 0x000B8074 File Offset: 0x000B6274
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17005405 RID: 21509
			// (set) Token: 0x06007B75 RID: 31605 RVA: 0x000B8087 File Offset: 0x000B6287
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17005406 RID: 21510
			// (set) Token: 0x06007B76 RID: 31606 RVA: 0x000B809F File Offset: 0x000B629F
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17005407 RID: 21511
			// (set) Token: 0x06007B77 RID: 31607 RVA: 0x000B80B7 File Offset: 0x000B62B7
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17005408 RID: 21512
			// (set) Token: 0x06007B78 RID: 31608 RVA: 0x000B80CF File Offset: 0x000B62CF
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17005409 RID: 21513
			// (set) Token: 0x06007B79 RID: 31609 RVA: 0x000B80E7 File Offset: 0x000B62E7
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
