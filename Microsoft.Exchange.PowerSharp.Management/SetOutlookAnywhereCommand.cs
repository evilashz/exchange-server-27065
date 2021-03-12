using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x02000998 RID: 2456
	public class SetOutlookAnywhereCommand : SyntheticCommandWithPipelineInputNoOutput<ADRpcHttpVirtualDirectory>
	{
		// Token: 0x06007BA2 RID: 31650 RVA: 0x000B843C File Offset: 0x000B663C
		private SetOutlookAnywhereCommand() : base("Set-OutlookAnywhere")
		{
		}

		// Token: 0x06007BA3 RID: 31651 RVA: 0x000B8449 File Offset: 0x000B6649
		public SetOutlookAnywhereCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x06007BA4 RID: 31652 RVA: 0x000B8458 File Offset: 0x000B6658
		public virtual SetOutlookAnywhereCommand SetParameters(SetOutlookAnywhereCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x06007BA5 RID: 31653 RVA: 0x000B8462 File Offset: 0x000B6662
		public virtual SetOutlookAnywhereCommand SetParameters(SetOutlookAnywhereCommand.IdentityParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x02000999 RID: 2457
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x1700542B RID: 21547
			// (set) Token: 0x06007BA6 RID: 31654 RVA: 0x000B846C File Offset: 0x000B666C
			public virtual bool SSLOffloading
			{
				set
				{
					base.PowerSharpParameters["SSLOffloading"] = value;
				}
			}

			// Token: 0x1700542C RID: 21548
			// (set) Token: 0x06007BA7 RID: 31655 RVA: 0x000B8484 File Offset: 0x000B6684
			public virtual bool ExternalClientsRequireSsl
			{
				set
				{
					base.PowerSharpParameters["ExternalClientsRequireSsl"] = value;
				}
			}

			// Token: 0x1700542D RID: 21549
			// (set) Token: 0x06007BA8 RID: 31656 RVA: 0x000B849C File Offset: 0x000B669C
			public virtual bool InternalClientsRequireSsl
			{
				set
				{
					base.PowerSharpParameters["InternalClientsRequireSsl"] = value;
				}
			}

			// Token: 0x1700542E RID: 21550
			// (set) Token: 0x06007BA9 RID: 31657 RVA: 0x000B84B4 File Offset: 0x000B66B4
			public virtual string ExternalHostname
			{
				set
				{
					base.PowerSharpParameters["ExternalHostname"] = value;
				}
			}

			// Token: 0x1700542F RID: 21551
			// (set) Token: 0x06007BAA RID: 31658 RVA: 0x000B84C7 File Offset: 0x000B66C7
			public virtual string InternalHostname
			{
				set
				{
					base.PowerSharpParameters["InternalHostname"] = value;
				}
			}

			// Token: 0x17005430 RID: 21552
			// (set) Token: 0x06007BAB RID: 31659 RVA: 0x000B84DA File Offset: 0x000B66DA
			public virtual AuthenticationMethod DefaultAuthenticationMethod
			{
				set
				{
					base.PowerSharpParameters["DefaultAuthenticationMethod"] = value;
				}
			}

			// Token: 0x17005431 RID: 21553
			// (set) Token: 0x06007BAC RID: 31660 RVA: 0x000B84F2 File Offset: 0x000B66F2
			public virtual AuthenticationMethod ExternalClientAuthenticationMethod
			{
				set
				{
					base.PowerSharpParameters["ExternalClientAuthenticationMethod"] = value;
				}
			}

			// Token: 0x17005432 RID: 21554
			// (set) Token: 0x06007BAD RID: 31661 RVA: 0x000B850A File Offset: 0x000B670A
			public virtual AuthenticationMethod InternalClientAuthenticationMethod
			{
				set
				{
					base.PowerSharpParameters["InternalClientAuthenticationMethod"] = value;
				}
			}

			// Token: 0x17005433 RID: 21555
			// (set) Token: 0x06007BAE RID: 31662 RVA: 0x000B8522 File Offset: 0x000B6722
			public virtual Uri XropUrl
			{
				set
				{
					base.PowerSharpParameters["XropUrl"] = value;
				}
			}

			// Token: 0x17005434 RID: 21556
			// (set) Token: 0x06007BAF RID: 31663 RVA: 0x000B8535 File Offset: 0x000B6735
			public virtual MultiValuedProperty<AuthenticationMethod> IISAuthenticationMethods
			{
				set
				{
					base.PowerSharpParameters["IISAuthenticationMethods"] = value;
				}
			}

			// Token: 0x17005435 RID: 21557
			// (set) Token: 0x06007BB0 RID: 31664 RVA: 0x000B8548 File Offset: 0x000B6748
			public virtual ExtendedProtectionTokenCheckingMode ExtendedProtectionTokenChecking
			{
				set
				{
					base.PowerSharpParameters["ExtendedProtectionTokenChecking"] = value;
				}
			}

			// Token: 0x17005436 RID: 21558
			// (set) Token: 0x06007BB1 RID: 31665 RVA: 0x000B8560 File Offset: 0x000B6760
			public virtual MultiValuedProperty<ExtendedProtectionFlag> ExtendedProtectionFlags
			{
				set
				{
					base.PowerSharpParameters["ExtendedProtectionFlags"] = value;
				}
			}

			// Token: 0x17005437 RID: 21559
			// (set) Token: 0x06007BB2 RID: 31666 RVA: 0x000B8573 File Offset: 0x000B6773
			public virtual MultiValuedProperty<string> ExtendedProtectionSPNList
			{
				set
				{
					base.PowerSharpParameters["ExtendedProtectionSPNList"] = value;
				}
			}

			// Token: 0x17005438 RID: 21560
			// (set) Token: 0x06007BB3 RID: 31667 RVA: 0x000B8586 File Offset: 0x000B6786
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17005439 RID: 21561
			// (set) Token: 0x06007BB4 RID: 31668 RVA: 0x000B8599 File Offset: 0x000B6799
			public virtual string Name
			{
				set
				{
					base.PowerSharpParameters["Name"] = value;
				}
			}

			// Token: 0x1700543A RID: 21562
			// (set) Token: 0x06007BB5 RID: 31669 RVA: 0x000B85AC File Offset: 0x000B67AC
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x1700543B RID: 21563
			// (set) Token: 0x06007BB6 RID: 31670 RVA: 0x000B85C4 File Offset: 0x000B67C4
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x1700543C RID: 21564
			// (set) Token: 0x06007BB7 RID: 31671 RVA: 0x000B85DC File Offset: 0x000B67DC
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x1700543D RID: 21565
			// (set) Token: 0x06007BB8 RID: 31672 RVA: 0x000B85F4 File Offset: 0x000B67F4
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x1700543E RID: 21566
			// (set) Token: 0x06007BB9 RID: 31673 RVA: 0x000B860C File Offset: 0x000B680C
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}

		// Token: 0x0200099A RID: 2458
		public class IdentityParameters : ParametersBase
		{
			// Token: 0x1700543F RID: 21567
			// (set) Token: 0x06007BBB RID: 31675 RVA: 0x000B862C File Offset: 0x000B682C
			public virtual VirtualDirectoryIdParameter Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = value;
				}
			}

			// Token: 0x17005440 RID: 21568
			// (set) Token: 0x06007BBC RID: 31676 RVA: 0x000B863F File Offset: 0x000B683F
			public virtual bool SSLOffloading
			{
				set
				{
					base.PowerSharpParameters["SSLOffloading"] = value;
				}
			}

			// Token: 0x17005441 RID: 21569
			// (set) Token: 0x06007BBD RID: 31677 RVA: 0x000B8657 File Offset: 0x000B6857
			public virtual bool ExternalClientsRequireSsl
			{
				set
				{
					base.PowerSharpParameters["ExternalClientsRequireSsl"] = value;
				}
			}

			// Token: 0x17005442 RID: 21570
			// (set) Token: 0x06007BBE RID: 31678 RVA: 0x000B866F File Offset: 0x000B686F
			public virtual bool InternalClientsRequireSsl
			{
				set
				{
					base.PowerSharpParameters["InternalClientsRequireSsl"] = value;
				}
			}

			// Token: 0x17005443 RID: 21571
			// (set) Token: 0x06007BBF RID: 31679 RVA: 0x000B8687 File Offset: 0x000B6887
			public virtual string ExternalHostname
			{
				set
				{
					base.PowerSharpParameters["ExternalHostname"] = value;
				}
			}

			// Token: 0x17005444 RID: 21572
			// (set) Token: 0x06007BC0 RID: 31680 RVA: 0x000B869A File Offset: 0x000B689A
			public virtual string InternalHostname
			{
				set
				{
					base.PowerSharpParameters["InternalHostname"] = value;
				}
			}

			// Token: 0x17005445 RID: 21573
			// (set) Token: 0x06007BC1 RID: 31681 RVA: 0x000B86AD File Offset: 0x000B68AD
			public virtual AuthenticationMethod DefaultAuthenticationMethod
			{
				set
				{
					base.PowerSharpParameters["DefaultAuthenticationMethod"] = value;
				}
			}

			// Token: 0x17005446 RID: 21574
			// (set) Token: 0x06007BC2 RID: 31682 RVA: 0x000B86C5 File Offset: 0x000B68C5
			public virtual AuthenticationMethod ExternalClientAuthenticationMethod
			{
				set
				{
					base.PowerSharpParameters["ExternalClientAuthenticationMethod"] = value;
				}
			}

			// Token: 0x17005447 RID: 21575
			// (set) Token: 0x06007BC3 RID: 31683 RVA: 0x000B86DD File Offset: 0x000B68DD
			public virtual AuthenticationMethod InternalClientAuthenticationMethod
			{
				set
				{
					base.PowerSharpParameters["InternalClientAuthenticationMethod"] = value;
				}
			}

			// Token: 0x17005448 RID: 21576
			// (set) Token: 0x06007BC4 RID: 31684 RVA: 0x000B86F5 File Offset: 0x000B68F5
			public virtual Uri XropUrl
			{
				set
				{
					base.PowerSharpParameters["XropUrl"] = value;
				}
			}

			// Token: 0x17005449 RID: 21577
			// (set) Token: 0x06007BC5 RID: 31685 RVA: 0x000B8708 File Offset: 0x000B6908
			public virtual MultiValuedProperty<AuthenticationMethod> IISAuthenticationMethods
			{
				set
				{
					base.PowerSharpParameters["IISAuthenticationMethods"] = value;
				}
			}

			// Token: 0x1700544A RID: 21578
			// (set) Token: 0x06007BC6 RID: 31686 RVA: 0x000B871B File Offset: 0x000B691B
			public virtual ExtendedProtectionTokenCheckingMode ExtendedProtectionTokenChecking
			{
				set
				{
					base.PowerSharpParameters["ExtendedProtectionTokenChecking"] = value;
				}
			}

			// Token: 0x1700544B RID: 21579
			// (set) Token: 0x06007BC7 RID: 31687 RVA: 0x000B8733 File Offset: 0x000B6933
			public virtual MultiValuedProperty<ExtendedProtectionFlag> ExtendedProtectionFlags
			{
				set
				{
					base.PowerSharpParameters["ExtendedProtectionFlags"] = value;
				}
			}

			// Token: 0x1700544C RID: 21580
			// (set) Token: 0x06007BC8 RID: 31688 RVA: 0x000B8746 File Offset: 0x000B6946
			public virtual MultiValuedProperty<string> ExtendedProtectionSPNList
			{
				set
				{
					base.PowerSharpParameters["ExtendedProtectionSPNList"] = value;
				}
			}

			// Token: 0x1700544D RID: 21581
			// (set) Token: 0x06007BC9 RID: 31689 RVA: 0x000B8759 File Offset: 0x000B6959
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x1700544E RID: 21582
			// (set) Token: 0x06007BCA RID: 31690 RVA: 0x000B876C File Offset: 0x000B696C
			public virtual string Name
			{
				set
				{
					base.PowerSharpParameters["Name"] = value;
				}
			}

			// Token: 0x1700544F RID: 21583
			// (set) Token: 0x06007BCB RID: 31691 RVA: 0x000B877F File Offset: 0x000B697F
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17005450 RID: 21584
			// (set) Token: 0x06007BCC RID: 31692 RVA: 0x000B8797 File Offset: 0x000B6997
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17005451 RID: 21585
			// (set) Token: 0x06007BCD RID: 31693 RVA: 0x000B87AF File Offset: 0x000B69AF
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17005452 RID: 21586
			// (set) Token: 0x06007BCE RID: 31694 RVA: 0x000B87C7 File Offset: 0x000B69C7
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17005453 RID: 21587
			// (set) Token: 0x06007BCF RID: 31695 RVA: 0x000B87DF File Offset: 0x000B69DF
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
