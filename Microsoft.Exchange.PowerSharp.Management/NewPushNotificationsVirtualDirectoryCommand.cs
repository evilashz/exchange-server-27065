using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Management.SystemConfigurationTasks;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x02000950 RID: 2384
	public class NewPushNotificationsVirtualDirectoryCommand : SyntheticCommandWithPipelineInput<ADPushNotificationsVirtualDirectory, ADPushNotificationsVirtualDirectory>
	{
		// Token: 0x060077D4 RID: 30676 RVA: 0x000B34F4 File Offset: 0x000B16F4
		private NewPushNotificationsVirtualDirectoryCommand() : base("New-PushNotificationsVirtualDirectory")
		{
		}

		// Token: 0x060077D5 RID: 30677 RVA: 0x000B3501 File Offset: 0x000B1701
		public NewPushNotificationsVirtualDirectoryCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x060077D6 RID: 30678 RVA: 0x000B3510 File Offset: 0x000B1710
		public virtual NewPushNotificationsVirtualDirectoryCommand SetParameters(NewPushNotificationsVirtualDirectoryCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x02000951 RID: 2385
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x170050ED RID: 20717
			// (set) Token: 0x060077D7 RID: 30679 RVA: 0x000B351A File Offset: 0x000B171A
			public virtual bool LiveIdAuthentication
			{
				set
				{
					base.PowerSharpParameters["LiveIdAuthentication"] = value;
				}
			}

			// Token: 0x170050EE RID: 20718
			// (set) Token: 0x060077D8 RID: 30680 RVA: 0x000B3532 File Offset: 0x000B1732
			public virtual bool OAuthAuthentication
			{
				set
				{
					base.PowerSharpParameters["OAuthAuthentication"] = value;
				}
			}

			// Token: 0x170050EF RID: 20719
			// (set) Token: 0x060077D9 RID: 30681 RVA: 0x000B354A File Offset: 0x000B174A
			public virtual VirtualDirectoryRole Role
			{
				set
				{
					base.PowerSharpParameters["Role"] = value;
				}
			}

			// Token: 0x170050F0 RID: 20720
			// (set) Token: 0x060077DA RID: 30682 RVA: 0x000B3562 File Offset: 0x000B1762
			public virtual ServerIdParameter Server
			{
				set
				{
					base.PowerSharpParameters["Server"] = value;
				}
			}

			// Token: 0x170050F1 RID: 20721
			// (set) Token: 0x060077DB RID: 30683 RVA: 0x000B3575 File Offset: 0x000B1775
			public virtual Uri InternalUrl
			{
				set
				{
					base.PowerSharpParameters["InternalUrl"] = value;
				}
			}

			// Token: 0x170050F2 RID: 20722
			// (set) Token: 0x060077DC RID: 30684 RVA: 0x000B3588 File Offset: 0x000B1788
			public virtual Uri ExternalUrl
			{
				set
				{
					base.PowerSharpParameters["ExternalUrl"] = value;
				}
			}

			// Token: 0x170050F3 RID: 20723
			// (set) Token: 0x060077DD RID: 30685 RVA: 0x000B359B File Offset: 0x000B179B
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x170050F4 RID: 20724
			// (set) Token: 0x060077DE RID: 30686 RVA: 0x000B35AE File Offset: 0x000B17AE
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x170050F5 RID: 20725
			// (set) Token: 0x060077DF RID: 30687 RVA: 0x000B35C6 File Offset: 0x000B17C6
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x170050F6 RID: 20726
			// (set) Token: 0x060077E0 RID: 30688 RVA: 0x000B35DE File Offset: 0x000B17DE
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x170050F7 RID: 20727
			// (set) Token: 0x060077E1 RID: 30689 RVA: 0x000B35F6 File Offset: 0x000B17F6
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x170050F8 RID: 20728
			// (set) Token: 0x060077E2 RID: 30690 RVA: 0x000B360E File Offset: 0x000B180E
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
