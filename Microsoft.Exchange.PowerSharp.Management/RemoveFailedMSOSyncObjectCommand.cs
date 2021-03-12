using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data.Directory.Sync;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x02000132 RID: 306
	public class RemoveFailedMSOSyncObjectCommand : SyntheticCommandWithPipelineInput<FailedMSOSyncObject, FailedMSOSyncObject>
	{
		// Token: 0x06002019 RID: 8217 RVA: 0x000414B0 File Offset: 0x0003F6B0
		private RemoveFailedMSOSyncObjectCommand() : base("Remove-FailedMSOSyncObject")
		{
		}

		// Token: 0x0600201A RID: 8218 RVA: 0x000414BD File Offset: 0x0003F6BD
		public RemoveFailedMSOSyncObjectCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x0600201B RID: 8219 RVA: 0x000414CC File Offset: 0x0003F6CC
		public virtual RemoveFailedMSOSyncObjectCommand SetParameters(RemoveFailedMSOSyncObjectCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x0600201C RID: 8220 RVA: 0x000414D6 File Offset: 0x0003F6D6
		public virtual RemoveFailedMSOSyncObjectCommand SetParameters(RemoveFailedMSOSyncObjectCommand.IdentityParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x02000133 RID: 307
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x1700096E RID: 2414
			// (set) Token: 0x0600201D RID: 8221 RVA: 0x000414E0 File Offset: 0x0003F6E0
			public virtual SwitchParameter Force
			{
				set
				{
					base.PowerSharpParameters["Force"] = value;
				}
			}

			// Token: 0x1700096F RID: 2415
			// (set) Token: 0x0600201E RID: 8222 RVA: 0x000414F8 File Offset: 0x0003F6F8
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17000970 RID: 2416
			// (set) Token: 0x0600201F RID: 8223 RVA: 0x00041510 File Offset: 0x0003F710
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17000971 RID: 2417
			// (set) Token: 0x06002020 RID: 8224 RVA: 0x00041528 File Offset: 0x0003F728
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17000972 RID: 2418
			// (set) Token: 0x06002021 RID: 8225 RVA: 0x00041540 File Offset: 0x0003F740
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17000973 RID: 2419
			// (set) Token: 0x06002022 RID: 8226 RVA: 0x00041558 File Offset: 0x0003F758
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}

			// Token: 0x17000974 RID: 2420
			// (set) Token: 0x06002023 RID: 8227 RVA: 0x00041570 File Offset: 0x0003F770
			public virtual SwitchParameter Confirm
			{
				set
				{
					base.PowerSharpParameters["Confirm"] = value;
				}
			}
		}

		// Token: 0x02000134 RID: 308
		public class IdentityParameters : ParametersBase
		{
			// Token: 0x17000975 RID: 2421
			// (set) Token: 0x06002025 RID: 8229 RVA: 0x00041590 File Offset: 0x0003F790
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new FailedMSOSyncObjectIdParameter(value) : null);
				}
			}

			// Token: 0x17000976 RID: 2422
			// (set) Token: 0x06002026 RID: 8230 RVA: 0x000415AE File Offset: 0x0003F7AE
			public virtual SwitchParameter Force
			{
				set
				{
					base.PowerSharpParameters["Force"] = value;
				}
			}

			// Token: 0x17000977 RID: 2423
			// (set) Token: 0x06002027 RID: 8231 RVA: 0x000415C6 File Offset: 0x0003F7C6
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17000978 RID: 2424
			// (set) Token: 0x06002028 RID: 8232 RVA: 0x000415DE File Offset: 0x0003F7DE
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17000979 RID: 2425
			// (set) Token: 0x06002029 RID: 8233 RVA: 0x000415F6 File Offset: 0x0003F7F6
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x1700097A RID: 2426
			// (set) Token: 0x0600202A RID: 8234 RVA: 0x0004160E File Offset: 0x0003F80E
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x1700097B RID: 2427
			// (set) Token: 0x0600202B RID: 8235 RVA: 0x00041626 File Offset: 0x0003F826
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}

			// Token: 0x1700097C RID: 2428
			// (set) Token: 0x0600202C RID: 8236 RVA: 0x0004163E File Offset: 0x0003F83E
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
