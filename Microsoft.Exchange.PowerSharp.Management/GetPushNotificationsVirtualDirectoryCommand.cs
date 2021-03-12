using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x0200092C RID: 2348
	public class GetPushNotificationsVirtualDirectoryCommand : SyntheticCommandWithPipelineInput<ADPushNotificationsVirtualDirectory, ADPushNotificationsVirtualDirectory>
	{
		// Token: 0x06007676 RID: 30326 RVA: 0x000B19E7 File Offset: 0x000AFBE7
		private GetPushNotificationsVirtualDirectoryCommand() : base("Get-PushNotificationsVirtualDirectory")
		{
		}

		// Token: 0x06007677 RID: 30327 RVA: 0x000B19F4 File Offset: 0x000AFBF4
		public GetPushNotificationsVirtualDirectoryCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x06007678 RID: 30328 RVA: 0x000B1A03 File Offset: 0x000AFC03
		public virtual GetPushNotificationsVirtualDirectoryCommand SetParameters(GetPushNotificationsVirtualDirectoryCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x06007679 RID: 30329 RVA: 0x000B1A0D File Offset: 0x000AFC0D
		public virtual GetPushNotificationsVirtualDirectoryCommand SetParameters(GetPushNotificationsVirtualDirectoryCommand.ServerParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x0600767A RID: 30330 RVA: 0x000B1A17 File Offset: 0x000AFC17
		public virtual GetPushNotificationsVirtualDirectoryCommand SetParameters(GetPushNotificationsVirtualDirectoryCommand.IdentityParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x0200092D RID: 2349
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x17004FD7 RID: 20439
			// (set) Token: 0x0600767B RID: 30331 RVA: 0x000B1A21 File Offset: 0x000AFC21
			public virtual SwitchParameter ADPropertiesOnly
			{
				set
				{
					base.PowerSharpParameters["ADPropertiesOnly"] = value;
				}
			}

			// Token: 0x17004FD8 RID: 20440
			// (set) Token: 0x0600767C RID: 30332 RVA: 0x000B1A39 File Offset: 0x000AFC39
			public virtual SwitchParameter ShowMailboxVirtualDirectories
			{
				set
				{
					base.PowerSharpParameters["ShowMailboxVirtualDirectories"] = value;
				}
			}

			// Token: 0x17004FD9 RID: 20441
			// (set) Token: 0x0600767D RID: 30333 RVA: 0x000B1A51 File Offset: 0x000AFC51
			public virtual SwitchParameter ShowBackEndVirtualDirectories
			{
				set
				{
					base.PowerSharpParameters["ShowBackEndVirtualDirectories"] = value;
				}
			}

			// Token: 0x17004FDA RID: 20442
			// (set) Token: 0x0600767E RID: 30334 RVA: 0x000B1A69 File Offset: 0x000AFC69
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17004FDB RID: 20443
			// (set) Token: 0x0600767F RID: 30335 RVA: 0x000B1A7C File Offset: 0x000AFC7C
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17004FDC RID: 20444
			// (set) Token: 0x06007680 RID: 30336 RVA: 0x000B1A94 File Offset: 0x000AFC94
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17004FDD RID: 20445
			// (set) Token: 0x06007681 RID: 30337 RVA: 0x000B1AAC File Offset: 0x000AFCAC
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17004FDE RID: 20446
			// (set) Token: 0x06007682 RID: 30338 RVA: 0x000B1AC4 File Offset: 0x000AFCC4
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}
		}

		// Token: 0x0200092E RID: 2350
		public class ServerParameters : ParametersBase
		{
			// Token: 0x17004FDF RID: 20447
			// (set) Token: 0x06007684 RID: 30340 RVA: 0x000B1AE4 File Offset: 0x000AFCE4
			public virtual ServerIdParameter Server
			{
				set
				{
					base.PowerSharpParameters["Server"] = value;
				}
			}

			// Token: 0x17004FE0 RID: 20448
			// (set) Token: 0x06007685 RID: 30341 RVA: 0x000B1AF7 File Offset: 0x000AFCF7
			public virtual SwitchParameter ADPropertiesOnly
			{
				set
				{
					base.PowerSharpParameters["ADPropertiesOnly"] = value;
				}
			}

			// Token: 0x17004FE1 RID: 20449
			// (set) Token: 0x06007686 RID: 30342 RVA: 0x000B1B0F File Offset: 0x000AFD0F
			public virtual SwitchParameter ShowMailboxVirtualDirectories
			{
				set
				{
					base.PowerSharpParameters["ShowMailboxVirtualDirectories"] = value;
				}
			}

			// Token: 0x17004FE2 RID: 20450
			// (set) Token: 0x06007687 RID: 30343 RVA: 0x000B1B27 File Offset: 0x000AFD27
			public virtual SwitchParameter ShowBackEndVirtualDirectories
			{
				set
				{
					base.PowerSharpParameters["ShowBackEndVirtualDirectories"] = value;
				}
			}

			// Token: 0x17004FE3 RID: 20451
			// (set) Token: 0x06007688 RID: 30344 RVA: 0x000B1B3F File Offset: 0x000AFD3F
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17004FE4 RID: 20452
			// (set) Token: 0x06007689 RID: 30345 RVA: 0x000B1B52 File Offset: 0x000AFD52
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17004FE5 RID: 20453
			// (set) Token: 0x0600768A RID: 30346 RVA: 0x000B1B6A File Offset: 0x000AFD6A
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17004FE6 RID: 20454
			// (set) Token: 0x0600768B RID: 30347 RVA: 0x000B1B82 File Offset: 0x000AFD82
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17004FE7 RID: 20455
			// (set) Token: 0x0600768C RID: 30348 RVA: 0x000B1B9A File Offset: 0x000AFD9A
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}
		}

		// Token: 0x0200092F RID: 2351
		public class IdentityParameters : ParametersBase
		{
			// Token: 0x17004FE8 RID: 20456
			// (set) Token: 0x0600768E RID: 30350 RVA: 0x000B1BBA File Offset: 0x000AFDBA
			public virtual VirtualDirectoryIdParameter Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = value;
				}
			}

			// Token: 0x17004FE9 RID: 20457
			// (set) Token: 0x0600768F RID: 30351 RVA: 0x000B1BCD File Offset: 0x000AFDCD
			public virtual SwitchParameter ADPropertiesOnly
			{
				set
				{
					base.PowerSharpParameters["ADPropertiesOnly"] = value;
				}
			}

			// Token: 0x17004FEA RID: 20458
			// (set) Token: 0x06007690 RID: 30352 RVA: 0x000B1BE5 File Offset: 0x000AFDE5
			public virtual SwitchParameter ShowMailboxVirtualDirectories
			{
				set
				{
					base.PowerSharpParameters["ShowMailboxVirtualDirectories"] = value;
				}
			}

			// Token: 0x17004FEB RID: 20459
			// (set) Token: 0x06007691 RID: 30353 RVA: 0x000B1BFD File Offset: 0x000AFDFD
			public virtual SwitchParameter ShowBackEndVirtualDirectories
			{
				set
				{
					base.PowerSharpParameters["ShowBackEndVirtualDirectories"] = value;
				}
			}

			// Token: 0x17004FEC RID: 20460
			// (set) Token: 0x06007692 RID: 30354 RVA: 0x000B1C15 File Offset: 0x000AFE15
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17004FED RID: 20461
			// (set) Token: 0x06007693 RID: 30355 RVA: 0x000B1C28 File Offset: 0x000AFE28
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17004FEE RID: 20462
			// (set) Token: 0x06007694 RID: 30356 RVA: 0x000B1C40 File Offset: 0x000AFE40
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17004FEF RID: 20463
			// (set) Token: 0x06007695 RID: 30357 RVA: 0x000B1C58 File Offset: 0x000AFE58
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17004FF0 RID: 20464
			// (set) Token: 0x06007696 RID: 30358 RVA: 0x000B1C70 File Offset: 0x000AFE70
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}
		}
	}
}
