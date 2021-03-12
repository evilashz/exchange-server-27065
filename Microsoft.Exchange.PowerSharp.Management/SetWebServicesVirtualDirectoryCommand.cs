using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x0200099B RID: 2459
	public class SetWebServicesVirtualDirectoryCommand : SyntheticCommandWithPipelineInputNoOutput<ADWebServicesVirtualDirectory>
	{
		// Token: 0x06007BD1 RID: 31697 RVA: 0x000B87FF File Offset: 0x000B69FF
		private SetWebServicesVirtualDirectoryCommand() : base("Set-WebServicesVirtualDirectory")
		{
		}

		// Token: 0x06007BD2 RID: 31698 RVA: 0x000B880C File Offset: 0x000B6A0C
		public SetWebServicesVirtualDirectoryCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x06007BD3 RID: 31699 RVA: 0x000B881B File Offset: 0x000B6A1B
		public virtual SetWebServicesVirtualDirectoryCommand SetParameters(SetWebServicesVirtualDirectoryCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x06007BD4 RID: 31700 RVA: 0x000B8825 File Offset: 0x000B6A25
		public virtual SetWebServicesVirtualDirectoryCommand SetParameters(SetWebServicesVirtualDirectoryCommand.IdentityParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x0200099C RID: 2460
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x17005454 RID: 21588
			// (set) Token: 0x06007BD5 RID: 31701 RVA: 0x000B882F File Offset: 0x000B6A2F
			public virtual Uri InternalNLBBypassUrl
			{
				set
				{
					base.PowerSharpParameters["InternalNLBBypassUrl"] = value;
				}
			}

			// Token: 0x17005455 RID: 21589
			// (set) Token: 0x06007BD6 RID: 31702 RVA: 0x000B8842 File Offset: 0x000B6A42
			public virtual GzipLevel GzipLevel
			{
				set
				{
					base.PowerSharpParameters["GzipLevel"] = value;
				}
			}

			// Token: 0x17005456 RID: 21590
			// (set) Token: 0x06007BD7 RID: 31703 RVA: 0x000B885A File Offset: 0x000B6A5A
			public virtual bool WSSecurityAuthentication
			{
				set
				{
					base.PowerSharpParameters["WSSecurityAuthentication"] = value;
				}
			}

			// Token: 0x17005457 RID: 21591
			// (set) Token: 0x06007BD8 RID: 31704 RVA: 0x000B8872 File Offset: 0x000B6A72
			public virtual bool OAuthAuthentication
			{
				set
				{
					base.PowerSharpParameters["OAuthAuthentication"] = value;
				}
			}

			// Token: 0x17005458 RID: 21592
			// (set) Token: 0x06007BD9 RID: 31705 RVA: 0x000B888A File Offset: 0x000B6A8A
			public virtual bool CertificateAuthentication
			{
				set
				{
					base.PowerSharpParameters["CertificateAuthentication"] = value;
				}
			}

			// Token: 0x17005459 RID: 21593
			// (set) Token: 0x06007BDA RID: 31706 RVA: 0x000B88A2 File Offset: 0x000B6AA2
			public virtual bool MRSProxyEnabled
			{
				set
				{
					base.PowerSharpParameters["MRSProxyEnabled"] = value;
				}
			}

			// Token: 0x1700545A RID: 21594
			// (set) Token: 0x06007BDB RID: 31707 RVA: 0x000B88BA File Offset: 0x000B6ABA
			public virtual SwitchParameter Force
			{
				set
				{
					base.PowerSharpParameters["Force"] = value;
				}
			}

			// Token: 0x1700545B RID: 21595
			// (set) Token: 0x06007BDC RID: 31708 RVA: 0x000B88D2 File Offset: 0x000B6AD2
			public virtual bool LiveIdBasicAuthentication
			{
				set
				{
					base.PowerSharpParameters["LiveIdBasicAuthentication"] = value;
				}
			}

			// Token: 0x1700545C RID: 21596
			// (set) Token: 0x06007BDD RID: 31709 RVA: 0x000B88EA File Offset: 0x000B6AEA
			public virtual bool LiveIdNegotiateAuthentication
			{
				set
				{
					base.PowerSharpParameters["LiveIdNegotiateAuthentication"] = value;
				}
			}

			// Token: 0x1700545D RID: 21597
			// (set) Token: 0x06007BDE RID: 31710 RVA: 0x000B8902 File Offset: 0x000B6B02
			public virtual bool BasicAuthentication
			{
				set
				{
					base.PowerSharpParameters["BasicAuthentication"] = value;
				}
			}

			// Token: 0x1700545E RID: 21598
			// (set) Token: 0x06007BDF RID: 31711 RVA: 0x000B891A File Offset: 0x000B6B1A
			public virtual bool DigestAuthentication
			{
				set
				{
					base.PowerSharpParameters["DigestAuthentication"] = value;
				}
			}

			// Token: 0x1700545F RID: 21599
			// (set) Token: 0x06007BE0 RID: 31712 RVA: 0x000B8932 File Offset: 0x000B6B32
			public virtual bool WindowsAuthentication
			{
				set
				{
					base.PowerSharpParameters["WindowsAuthentication"] = value;
				}
			}

			// Token: 0x17005460 RID: 21600
			// (set) Token: 0x06007BE1 RID: 31713 RVA: 0x000B894A File Offset: 0x000B6B4A
			public virtual ExtendedProtectionTokenCheckingMode ExtendedProtectionTokenChecking
			{
				set
				{
					base.PowerSharpParameters["ExtendedProtectionTokenChecking"] = value;
				}
			}

			// Token: 0x17005461 RID: 21601
			// (set) Token: 0x06007BE2 RID: 31714 RVA: 0x000B8962 File Offset: 0x000B6B62
			public virtual MultiValuedProperty<ExtendedProtectionFlag> ExtendedProtectionFlags
			{
				set
				{
					base.PowerSharpParameters["ExtendedProtectionFlags"] = value;
				}
			}

			// Token: 0x17005462 RID: 21602
			// (set) Token: 0x06007BE3 RID: 31715 RVA: 0x000B8975 File Offset: 0x000B6B75
			public virtual MultiValuedProperty<string> ExtendedProtectionSPNList
			{
				set
				{
					base.PowerSharpParameters["ExtendedProtectionSPNList"] = value;
				}
			}

			// Token: 0x17005463 RID: 21603
			// (set) Token: 0x06007BE4 RID: 31716 RVA: 0x000B8988 File Offset: 0x000B6B88
			public virtual Uri InternalUrl
			{
				set
				{
					base.PowerSharpParameters["InternalUrl"] = value;
				}
			}

			// Token: 0x17005464 RID: 21604
			// (set) Token: 0x06007BE5 RID: 31717 RVA: 0x000B899B File Offset: 0x000B6B9B
			public virtual Uri ExternalUrl
			{
				set
				{
					base.PowerSharpParameters["ExternalUrl"] = value;
				}
			}

			// Token: 0x17005465 RID: 21605
			// (set) Token: 0x06007BE6 RID: 31718 RVA: 0x000B89AE File Offset: 0x000B6BAE
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17005466 RID: 21606
			// (set) Token: 0x06007BE7 RID: 31719 RVA: 0x000B89C1 File Offset: 0x000B6BC1
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17005467 RID: 21607
			// (set) Token: 0x06007BE8 RID: 31720 RVA: 0x000B89D9 File Offset: 0x000B6BD9
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17005468 RID: 21608
			// (set) Token: 0x06007BE9 RID: 31721 RVA: 0x000B89F1 File Offset: 0x000B6BF1
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17005469 RID: 21609
			// (set) Token: 0x06007BEA RID: 31722 RVA: 0x000B8A09 File Offset: 0x000B6C09
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x1700546A RID: 21610
			// (set) Token: 0x06007BEB RID: 31723 RVA: 0x000B8A21 File Offset: 0x000B6C21
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}

		// Token: 0x0200099D RID: 2461
		public class IdentityParameters : ParametersBase
		{
			// Token: 0x1700546B RID: 21611
			// (set) Token: 0x06007BED RID: 31725 RVA: 0x000B8A41 File Offset: 0x000B6C41
			public virtual VirtualDirectoryIdParameter Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = value;
				}
			}

			// Token: 0x1700546C RID: 21612
			// (set) Token: 0x06007BEE RID: 31726 RVA: 0x000B8A54 File Offset: 0x000B6C54
			public virtual Uri InternalNLBBypassUrl
			{
				set
				{
					base.PowerSharpParameters["InternalNLBBypassUrl"] = value;
				}
			}

			// Token: 0x1700546D RID: 21613
			// (set) Token: 0x06007BEF RID: 31727 RVA: 0x000B8A67 File Offset: 0x000B6C67
			public virtual GzipLevel GzipLevel
			{
				set
				{
					base.PowerSharpParameters["GzipLevel"] = value;
				}
			}

			// Token: 0x1700546E RID: 21614
			// (set) Token: 0x06007BF0 RID: 31728 RVA: 0x000B8A7F File Offset: 0x000B6C7F
			public virtual bool WSSecurityAuthentication
			{
				set
				{
					base.PowerSharpParameters["WSSecurityAuthentication"] = value;
				}
			}

			// Token: 0x1700546F RID: 21615
			// (set) Token: 0x06007BF1 RID: 31729 RVA: 0x000B8A97 File Offset: 0x000B6C97
			public virtual bool OAuthAuthentication
			{
				set
				{
					base.PowerSharpParameters["OAuthAuthentication"] = value;
				}
			}

			// Token: 0x17005470 RID: 21616
			// (set) Token: 0x06007BF2 RID: 31730 RVA: 0x000B8AAF File Offset: 0x000B6CAF
			public virtual bool CertificateAuthentication
			{
				set
				{
					base.PowerSharpParameters["CertificateAuthentication"] = value;
				}
			}

			// Token: 0x17005471 RID: 21617
			// (set) Token: 0x06007BF3 RID: 31731 RVA: 0x000B8AC7 File Offset: 0x000B6CC7
			public virtual bool MRSProxyEnabled
			{
				set
				{
					base.PowerSharpParameters["MRSProxyEnabled"] = value;
				}
			}

			// Token: 0x17005472 RID: 21618
			// (set) Token: 0x06007BF4 RID: 31732 RVA: 0x000B8ADF File Offset: 0x000B6CDF
			public virtual SwitchParameter Force
			{
				set
				{
					base.PowerSharpParameters["Force"] = value;
				}
			}

			// Token: 0x17005473 RID: 21619
			// (set) Token: 0x06007BF5 RID: 31733 RVA: 0x000B8AF7 File Offset: 0x000B6CF7
			public virtual bool LiveIdBasicAuthentication
			{
				set
				{
					base.PowerSharpParameters["LiveIdBasicAuthentication"] = value;
				}
			}

			// Token: 0x17005474 RID: 21620
			// (set) Token: 0x06007BF6 RID: 31734 RVA: 0x000B8B0F File Offset: 0x000B6D0F
			public virtual bool LiveIdNegotiateAuthentication
			{
				set
				{
					base.PowerSharpParameters["LiveIdNegotiateAuthentication"] = value;
				}
			}

			// Token: 0x17005475 RID: 21621
			// (set) Token: 0x06007BF7 RID: 31735 RVA: 0x000B8B27 File Offset: 0x000B6D27
			public virtual bool BasicAuthentication
			{
				set
				{
					base.PowerSharpParameters["BasicAuthentication"] = value;
				}
			}

			// Token: 0x17005476 RID: 21622
			// (set) Token: 0x06007BF8 RID: 31736 RVA: 0x000B8B3F File Offset: 0x000B6D3F
			public virtual bool DigestAuthentication
			{
				set
				{
					base.PowerSharpParameters["DigestAuthentication"] = value;
				}
			}

			// Token: 0x17005477 RID: 21623
			// (set) Token: 0x06007BF9 RID: 31737 RVA: 0x000B8B57 File Offset: 0x000B6D57
			public virtual bool WindowsAuthentication
			{
				set
				{
					base.PowerSharpParameters["WindowsAuthentication"] = value;
				}
			}

			// Token: 0x17005478 RID: 21624
			// (set) Token: 0x06007BFA RID: 31738 RVA: 0x000B8B6F File Offset: 0x000B6D6F
			public virtual ExtendedProtectionTokenCheckingMode ExtendedProtectionTokenChecking
			{
				set
				{
					base.PowerSharpParameters["ExtendedProtectionTokenChecking"] = value;
				}
			}

			// Token: 0x17005479 RID: 21625
			// (set) Token: 0x06007BFB RID: 31739 RVA: 0x000B8B87 File Offset: 0x000B6D87
			public virtual MultiValuedProperty<ExtendedProtectionFlag> ExtendedProtectionFlags
			{
				set
				{
					base.PowerSharpParameters["ExtendedProtectionFlags"] = value;
				}
			}

			// Token: 0x1700547A RID: 21626
			// (set) Token: 0x06007BFC RID: 31740 RVA: 0x000B8B9A File Offset: 0x000B6D9A
			public virtual MultiValuedProperty<string> ExtendedProtectionSPNList
			{
				set
				{
					base.PowerSharpParameters["ExtendedProtectionSPNList"] = value;
				}
			}

			// Token: 0x1700547B RID: 21627
			// (set) Token: 0x06007BFD RID: 31741 RVA: 0x000B8BAD File Offset: 0x000B6DAD
			public virtual Uri InternalUrl
			{
				set
				{
					base.PowerSharpParameters["InternalUrl"] = value;
				}
			}

			// Token: 0x1700547C RID: 21628
			// (set) Token: 0x06007BFE RID: 31742 RVA: 0x000B8BC0 File Offset: 0x000B6DC0
			public virtual Uri ExternalUrl
			{
				set
				{
					base.PowerSharpParameters["ExternalUrl"] = value;
				}
			}

			// Token: 0x1700547D RID: 21629
			// (set) Token: 0x06007BFF RID: 31743 RVA: 0x000B8BD3 File Offset: 0x000B6DD3
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x1700547E RID: 21630
			// (set) Token: 0x06007C00 RID: 31744 RVA: 0x000B8BE6 File Offset: 0x000B6DE6
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x1700547F RID: 21631
			// (set) Token: 0x06007C01 RID: 31745 RVA: 0x000B8BFE File Offset: 0x000B6DFE
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17005480 RID: 21632
			// (set) Token: 0x06007C02 RID: 31746 RVA: 0x000B8C16 File Offset: 0x000B6E16
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17005481 RID: 21633
			// (set) Token: 0x06007C03 RID: 31747 RVA: 0x000B8C2E File Offset: 0x000B6E2E
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17005482 RID: 21634
			// (set) Token: 0x06007C04 RID: 31748 RVA: 0x000B8C46 File Offset: 0x000B6E46
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
