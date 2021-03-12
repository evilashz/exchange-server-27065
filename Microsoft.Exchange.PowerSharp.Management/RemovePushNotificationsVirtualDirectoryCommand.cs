using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x02000974 RID: 2420
	public class RemovePushNotificationsVirtualDirectoryCommand : SyntheticCommandWithPipelineInput<ADPushNotificationsVirtualDirectory, ADPushNotificationsVirtualDirectory>
	{
		// Token: 0x060078E6 RID: 30950 RVA: 0x000B49F4 File Offset: 0x000B2BF4
		private RemovePushNotificationsVirtualDirectoryCommand() : base("Remove-PushNotificationsVirtualDirectory")
		{
		}

		// Token: 0x060078E7 RID: 30951 RVA: 0x000B4A01 File Offset: 0x000B2C01
		public RemovePushNotificationsVirtualDirectoryCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x060078E8 RID: 30952 RVA: 0x000B4A10 File Offset: 0x000B2C10
		public virtual RemovePushNotificationsVirtualDirectoryCommand SetParameters(RemovePushNotificationsVirtualDirectoryCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x060078E9 RID: 30953 RVA: 0x000B4A1A File Offset: 0x000B2C1A
		public virtual RemovePushNotificationsVirtualDirectoryCommand SetParameters(RemovePushNotificationsVirtualDirectoryCommand.IdentityParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x02000975 RID: 2421
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x170051B7 RID: 20919
			// (set) Token: 0x060078EA RID: 30954 RVA: 0x000B4A24 File Offset: 0x000B2C24
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x170051B8 RID: 20920
			// (set) Token: 0x060078EB RID: 30955 RVA: 0x000B4A37 File Offset: 0x000B2C37
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x170051B9 RID: 20921
			// (set) Token: 0x060078EC RID: 30956 RVA: 0x000B4A4F File Offset: 0x000B2C4F
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x170051BA RID: 20922
			// (set) Token: 0x060078ED RID: 30957 RVA: 0x000B4A67 File Offset: 0x000B2C67
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x170051BB RID: 20923
			// (set) Token: 0x060078EE RID: 30958 RVA: 0x000B4A7F File Offset: 0x000B2C7F
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x170051BC RID: 20924
			// (set) Token: 0x060078EF RID: 30959 RVA: 0x000B4A97 File Offset: 0x000B2C97
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}

			// Token: 0x170051BD RID: 20925
			// (set) Token: 0x060078F0 RID: 30960 RVA: 0x000B4AAF File Offset: 0x000B2CAF
			public virtual SwitchParameter Confirm
			{
				set
				{
					base.PowerSharpParameters["Confirm"] = value;
				}
			}
		}

		// Token: 0x02000976 RID: 2422
		public class IdentityParameters : ParametersBase
		{
			// Token: 0x170051BE RID: 20926
			// (set) Token: 0x060078F2 RID: 30962 RVA: 0x000B4ACF File Offset: 0x000B2CCF
			public virtual VirtualDirectoryIdParameter Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = value;
				}
			}

			// Token: 0x170051BF RID: 20927
			// (set) Token: 0x060078F3 RID: 30963 RVA: 0x000B4AE2 File Offset: 0x000B2CE2
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x170051C0 RID: 20928
			// (set) Token: 0x060078F4 RID: 30964 RVA: 0x000B4AF5 File Offset: 0x000B2CF5
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x170051C1 RID: 20929
			// (set) Token: 0x060078F5 RID: 30965 RVA: 0x000B4B0D File Offset: 0x000B2D0D
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x170051C2 RID: 20930
			// (set) Token: 0x060078F6 RID: 30966 RVA: 0x000B4B25 File Offset: 0x000B2D25
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x170051C3 RID: 20931
			// (set) Token: 0x060078F7 RID: 30967 RVA: 0x000B4B3D File Offset: 0x000B2D3D
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x170051C4 RID: 20932
			// (set) Token: 0x060078F8 RID: 30968 RVA: 0x000B4B55 File Offset: 0x000B2D55
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}

			// Token: 0x170051C5 RID: 20933
			// (set) Token: 0x060078F9 RID: 30969 RVA: 0x000B4B6D File Offset: 0x000B2D6D
			public virtual SwitchParameter Confirm
			{
				set
				{
					base.PowerSharpParameters["Confirm"] = value;
				}
			}
		}
	}
}
