using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x02000055 RID: 85
	public class RemoveActiveSyncDeviceClassCommand : SyntheticCommandWithPipelineInput<ActiveSyncDeviceClass, ActiveSyncDeviceClass>
	{
		// Token: 0x060016EB RID: 5867 RVA: 0x00035748 File Offset: 0x00033948
		private RemoveActiveSyncDeviceClassCommand() : base("Remove-ActiveSyncDeviceClass")
		{
		}

		// Token: 0x060016EC RID: 5868 RVA: 0x00035755 File Offset: 0x00033955
		public RemoveActiveSyncDeviceClassCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x060016ED RID: 5869 RVA: 0x00035764 File Offset: 0x00033964
		public virtual RemoveActiveSyncDeviceClassCommand SetParameters(RemoveActiveSyncDeviceClassCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x060016EE RID: 5870 RVA: 0x0003576E File Offset: 0x0003396E
		public virtual RemoveActiveSyncDeviceClassCommand SetParameters(RemoveActiveSyncDeviceClassCommand.IdentityParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x02000056 RID: 86
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x170001FA RID: 506
			// (set) Token: 0x060016EF RID: 5871 RVA: 0x00035778 File Offset: 0x00033978
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x170001FB RID: 507
			// (set) Token: 0x060016F0 RID: 5872 RVA: 0x0003578B File Offset: 0x0003398B
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x170001FC RID: 508
			// (set) Token: 0x060016F1 RID: 5873 RVA: 0x000357A3 File Offset: 0x000339A3
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x170001FD RID: 509
			// (set) Token: 0x060016F2 RID: 5874 RVA: 0x000357BB File Offset: 0x000339BB
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x170001FE RID: 510
			// (set) Token: 0x060016F3 RID: 5875 RVA: 0x000357D3 File Offset: 0x000339D3
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x170001FF RID: 511
			// (set) Token: 0x060016F4 RID: 5876 RVA: 0x000357EB File Offset: 0x000339EB
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}

			// Token: 0x17000200 RID: 512
			// (set) Token: 0x060016F5 RID: 5877 RVA: 0x00035803 File Offset: 0x00033A03
			public virtual SwitchParameter Confirm
			{
				set
				{
					base.PowerSharpParameters["Confirm"] = value;
				}
			}
		}

		// Token: 0x02000057 RID: 87
		public class IdentityParameters : ParametersBase
		{
			// Token: 0x17000201 RID: 513
			// (set) Token: 0x060016F7 RID: 5879 RVA: 0x00035823 File Offset: 0x00033A23
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new ActiveSyncDeviceClassIdParameter(value) : null);
				}
			}

			// Token: 0x17000202 RID: 514
			// (set) Token: 0x060016F8 RID: 5880 RVA: 0x00035841 File Offset: 0x00033A41
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17000203 RID: 515
			// (set) Token: 0x060016F9 RID: 5881 RVA: 0x00035854 File Offset: 0x00033A54
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17000204 RID: 516
			// (set) Token: 0x060016FA RID: 5882 RVA: 0x0003586C File Offset: 0x00033A6C
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17000205 RID: 517
			// (set) Token: 0x060016FB RID: 5883 RVA: 0x00035884 File Offset: 0x00033A84
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17000206 RID: 518
			// (set) Token: 0x060016FC RID: 5884 RVA: 0x0003589C File Offset: 0x00033A9C
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17000207 RID: 519
			// (set) Token: 0x060016FD RID: 5885 RVA: 0x000358B4 File Offset: 0x00033AB4
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}

			// Token: 0x17000208 RID: 520
			// (set) Token: 0x060016FE RID: 5886 RVA: 0x000358CC File Offset: 0x00033ACC
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
