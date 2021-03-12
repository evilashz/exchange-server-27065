using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Management.SystemConfigurationTasks;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x0200094E RID: 2382
	public class NewPswsVirtualDirectoryCommand : SyntheticCommandWithPipelineInput<ADPswsVirtualDirectory, ADPswsVirtualDirectory>
	{
		// Token: 0x060077BE RID: 30654 RVA: 0x000B333E File Offset: 0x000B153E
		private NewPswsVirtualDirectoryCommand() : base("New-PswsVirtualDirectory")
		{
		}

		// Token: 0x060077BF RID: 30655 RVA: 0x000B334B File Offset: 0x000B154B
		public NewPswsVirtualDirectoryCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x060077C0 RID: 30656 RVA: 0x000B335A File Offset: 0x000B155A
		public virtual NewPswsVirtualDirectoryCommand SetParameters(NewPswsVirtualDirectoryCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x0200094F RID: 2383
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x170050DB RID: 20699
			// (set) Token: 0x060077C1 RID: 30657 RVA: 0x000B3364 File Offset: 0x000B1564
			public virtual string Name
			{
				set
				{
					base.PowerSharpParameters["Name"] = value;
				}
			}

			// Token: 0x170050DC RID: 20700
			// (set) Token: 0x060077C2 RID: 30658 RVA: 0x000B3377 File Offset: 0x000B1577
			public virtual bool CertificateAuthentication
			{
				set
				{
					base.PowerSharpParameters["CertificateAuthentication"] = value;
				}
			}

			// Token: 0x170050DD RID: 20701
			// (set) Token: 0x060077C3 RID: 30659 RVA: 0x000B338F File Offset: 0x000B158F
			public virtual bool LimitMaximumMemory
			{
				set
				{
					base.PowerSharpParameters["LimitMaximumMemory"] = value;
				}
			}

			// Token: 0x170050DE RID: 20702
			// (set) Token: 0x060077C4 RID: 30660 RVA: 0x000B33A7 File Offset: 0x000B15A7
			public virtual bool BasicAuthentication
			{
				set
				{
					base.PowerSharpParameters["BasicAuthentication"] = value;
				}
			}

			// Token: 0x170050DF RID: 20703
			// (set) Token: 0x060077C5 RID: 30661 RVA: 0x000B33BF File Offset: 0x000B15BF
			public virtual bool WindowsAuthentication
			{
				set
				{
					base.PowerSharpParameters["WindowsAuthentication"] = value;
				}
			}

			// Token: 0x170050E0 RID: 20704
			// (set) Token: 0x060077C6 RID: 30662 RVA: 0x000B33D7 File Offset: 0x000B15D7
			public virtual string WebSiteName
			{
				set
				{
					base.PowerSharpParameters["WebSiteName"] = value;
				}
			}

			// Token: 0x170050E1 RID: 20705
			// (set) Token: 0x060077C7 RID: 30663 RVA: 0x000B33EA File Offset: 0x000B15EA
			public virtual string Path
			{
				set
				{
					base.PowerSharpParameters["Path"] = value;
				}
			}

			// Token: 0x170050E2 RID: 20706
			// (set) Token: 0x060077C8 RID: 30664 RVA: 0x000B33FD File Offset: 0x000B15FD
			public virtual string AppPoolId
			{
				set
				{
					base.PowerSharpParameters["AppPoolId"] = value;
				}
			}

			// Token: 0x170050E3 RID: 20707
			// (set) Token: 0x060077C9 RID: 30665 RVA: 0x000B3410 File Offset: 0x000B1610
			public virtual VirtualDirectoryRole Role
			{
				set
				{
					base.PowerSharpParameters["Role"] = value;
				}
			}

			// Token: 0x170050E4 RID: 20708
			// (set) Token: 0x060077CA RID: 30666 RVA: 0x000B3428 File Offset: 0x000B1628
			public virtual ServerIdParameter Server
			{
				set
				{
					base.PowerSharpParameters["Server"] = value;
				}
			}

			// Token: 0x170050E5 RID: 20709
			// (set) Token: 0x060077CB RID: 30667 RVA: 0x000B343B File Offset: 0x000B163B
			public virtual Uri InternalUrl
			{
				set
				{
					base.PowerSharpParameters["InternalUrl"] = value;
				}
			}

			// Token: 0x170050E6 RID: 20710
			// (set) Token: 0x060077CC RID: 30668 RVA: 0x000B344E File Offset: 0x000B164E
			public virtual Uri ExternalUrl
			{
				set
				{
					base.PowerSharpParameters["ExternalUrl"] = value;
				}
			}

			// Token: 0x170050E7 RID: 20711
			// (set) Token: 0x060077CD RID: 30669 RVA: 0x000B3461 File Offset: 0x000B1661
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x170050E8 RID: 20712
			// (set) Token: 0x060077CE RID: 30670 RVA: 0x000B3474 File Offset: 0x000B1674
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x170050E9 RID: 20713
			// (set) Token: 0x060077CF RID: 30671 RVA: 0x000B348C File Offset: 0x000B168C
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x170050EA RID: 20714
			// (set) Token: 0x060077D0 RID: 30672 RVA: 0x000B34A4 File Offset: 0x000B16A4
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x170050EB RID: 20715
			// (set) Token: 0x060077D1 RID: 30673 RVA: 0x000B34BC File Offset: 0x000B16BC
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x170050EC RID: 20716
			// (set) Token: 0x060077D2 RID: 30674 RVA: 0x000B34D4 File Offset: 0x000B16D4
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
