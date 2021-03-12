using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x02000D27 RID: 3367
	public class GetADPermissionCommand : SyntheticCommandWithPipelineInput<ADRawEntry, ADRawEntry>
	{
		// Token: 0x0600B27D RID: 45693 RVA: 0x0010156A File Offset: 0x000FF76A
		private GetADPermissionCommand() : base("Get-ADPermission")
		{
		}

		// Token: 0x0600B27E RID: 45694 RVA: 0x00101577 File Offset: 0x000FF777
		public GetADPermissionCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x0600B27F RID: 45695 RVA: 0x00101586 File Offset: 0x000FF786
		public virtual GetADPermissionCommand SetParameters(GetADPermissionCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x0600B280 RID: 45696 RVA: 0x00101590 File Offset: 0x000FF790
		public virtual GetADPermissionCommand SetParameters(GetADPermissionCommand.AccessRightsParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x0600B281 RID: 45697 RVA: 0x0010159A File Offset: 0x000FF79A
		public virtual GetADPermissionCommand SetParameters(GetADPermissionCommand.OwnerParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x02000D28 RID: 3368
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x170083E8 RID: 33768
			// (set) Token: 0x0600B282 RID: 45698 RVA: 0x001015A4 File Offset: 0x000FF7A4
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x170083E9 RID: 33769
			// (set) Token: 0x0600B283 RID: 45699 RVA: 0x001015B7 File Offset: 0x000FF7B7
			public virtual ADRawEntryIdParameter Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = value;
				}
			}

			// Token: 0x170083EA RID: 33770
			// (set) Token: 0x0600B284 RID: 45700 RVA: 0x001015CA File Offset: 0x000FF7CA
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x170083EB RID: 33771
			// (set) Token: 0x0600B285 RID: 45701 RVA: 0x001015E2 File Offset: 0x000FF7E2
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x170083EC RID: 33772
			// (set) Token: 0x0600B286 RID: 45702 RVA: 0x001015FA File Offset: 0x000FF7FA
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x170083ED RID: 33773
			// (set) Token: 0x0600B287 RID: 45703 RVA: 0x00101612 File Offset: 0x000FF812
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}
		}

		// Token: 0x02000D29 RID: 3369
		public class AccessRightsParameters : ParametersBase
		{
			// Token: 0x170083EE RID: 33774
			// (set) Token: 0x0600B289 RID: 45705 RVA: 0x00101632 File Offset: 0x000FF832
			public virtual string User
			{
				set
				{
					base.PowerSharpParameters["User"] = ((value != null) ? new SecurityPrincipalIdParameter(value) : null);
				}
			}

			// Token: 0x170083EF RID: 33775
			// (set) Token: 0x0600B28A RID: 45706 RVA: 0x00101650 File Offset: 0x000FF850
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x170083F0 RID: 33776
			// (set) Token: 0x0600B28B RID: 45707 RVA: 0x00101663 File Offset: 0x000FF863
			public virtual ADRawEntryIdParameter Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = value;
				}
			}

			// Token: 0x170083F1 RID: 33777
			// (set) Token: 0x0600B28C RID: 45708 RVA: 0x00101676 File Offset: 0x000FF876
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x170083F2 RID: 33778
			// (set) Token: 0x0600B28D RID: 45709 RVA: 0x0010168E File Offset: 0x000FF88E
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x170083F3 RID: 33779
			// (set) Token: 0x0600B28E RID: 45710 RVA: 0x001016A6 File Offset: 0x000FF8A6
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x170083F4 RID: 33780
			// (set) Token: 0x0600B28F RID: 45711 RVA: 0x001016BE File Offset: 0x000FF8BE
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}
		}

		// Token: 0x02000D2A RID: 3370
		public class OwnerParameters : ParametersBase
		{
			// Token: 0x170083F5 RID: 33781
			// (set) Token: 0x0600B291 RID: 45713 RVA: 0x001016DE File Offset: 0x000FF8DE
			public virtual SwitchParameter Owner
			{
				set
				{
					base.PowerSharpParameters["Owner"] = value;
				}
			}

			// Token: 0x170083F6 RID: 33782
			// (set) Token: 0x0600B292 RID: 45714 RVA: 0x001016F6 File Offset: 0x000FF8F6
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x170083F7 RID: 33783
			// (set) Token: 0x0600B293 RID: 45715 RVA: 0x00101709 File Offset: 0x000FF909
			public virtual ADRawEntryIdParameter Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = value;
				}
			}

			// Token: 0x170083F8 RID: 33784
			// (set) Token: 0x0600B294 RID: 45716 RVA: 0x0010171C File Offset: 0x000FF91C
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x170083F9 RID: 33785
			// (set) Token: 0x0600B295 RID: 45717 RVA: 0x00101734 File Offset: 0x000FF934
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x170083FA RID: 33786
			// (set) Token: 0x0600B296 RID: 45718 RVA: 0x0010174C File Offset: 0x000FF94C
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x170083FB RID: 33787
			// (set) Token: 0x0600B297 RID: 45719 RVA: 0x00101764 File Offset: 0x000FF964
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
