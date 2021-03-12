using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.Sync;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x02000137 RID: 311
	public class RemoveSyncServiceInstanceCommand : SyntheticCommandWithPipelineInput<SyncServiceInstance, SyncServiceInstance>
	{
		// Token: 0x0600203A RID: 8250 RVA: 0x00041742 File Offset: 0x0003F942
		private RemoveSyncServiceInstanceCommand() : base("Remove-SyncServiceInstance")
		{
		}

		// Token: 0x0600203B RID: 8251 RVA: 0x0004174F File Offset: 0x0003F94F
		public RemoveSyncServiceInstanceCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x0600203C RID: 8252 RVA: 0x0004175E File Offset: 0x0003F95E
		public virtual RemoveSyncServiceInstanceCommand SetParameters(RemoveSyncServiceInstanceCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x0600203D RID: 8253 RVA: 0x00041768 File Offset: 0x0003F968
		public virtual RemoveSyncServiceInstanceCommand SetParameters(RemoveSyncServiceInstanceCommand.IdentityParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x02000138 RID: 312
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x17000985 RID: 2437
			// (set) Token: 0x0600203E RID: 8254 RVA: 0x00041772 File Offset: 0x0003F972
			public virtual SwitchParameter Force
			{
				set
				{
					base.PowerSharpParameters["Force"] = value;
				}
			}

			// Token: 0x17000986 RID: 2438
			// (set) Token: 0x0600203F RID: 8255 RVA: 0x0004178A File Offset: 0x0003F98A
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17000987 RID: 2439
			// (set) Token: 0x06002040 RID: 8256 RVA: 0x0004179D File Offset: 0x0003F99D
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17000988 RID: 2440
			// (set) Token: 0x06002041 RID: 8257 RVA: 0x000417B5 File Offset: 0x0003F9B5
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17000989 RID: 2441
			// (set) Token: 0x06002042 RID: 8258 RVA: 0x000417CD File Offset: 0x0003F9CD
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x1700098A RID: 2442
			// (set) Token: 0x06002043 RID: 8259 RVA: 0x000417E5 File Offset: 0x0003F9E5
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x1700098B RID: 2443
			// (set) Token: 0x06002044 RID: 8260 RVA: 0x000417FD File Offset: 0x0003F9FD
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}

			// Token: 0x1700098C RID: 2444
			// (set) Token: 0x06002045 RID: 8261 RVA: 0x00041815 File Offset: 0x0003FA15
			public virtual SwitchParameter Confirm
			{
				set
				{
					base.PowerSharpParameters["Confirm"] = value;
				}
			}
		}

		// Token: 0x02000139 RID: 313
		public class IdentityParameters : ParametersBase
		{
			// Token: 0x1700098D RID: 2445
			// (set) Token: 0x06002047 RID: 8263 RVA: 0x00041835 File Offset: 0x0003FA35
			public virtual ServiceInstanceIdParameter Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = value;
				}
			}

			// Token: 0x1700098E RID: 2446
			// (set) Token: 0x06002048 RID: 8264 RVA: 0x00041848 File Offset: 0x0003FA48
			public virtual SwitchParameter Force
			{
				set
				{
					base.PowerSharpParameters["Force"] = value;
				}
			}

			// Token: 0x1700098F RID: 2447
			// (set) Token: 0x06002049 RID: 8265 RVA: 0x00041860 File Offset: 0x0003FA60
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17000990 RID: 2448
			// (set) Token: 0x0600204A RID: 8266 RVA: 0x00041873 File Offset: 0x0003FA73
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17000991 RID: 2449
			// (set) Token: 0x0600204B RID: 8267 RVA: 0x0004188B File Offset: 0x0003FA8B
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17000992 RID: 2450
			// (set) Token: 0x0600204C RID: 8268 RVA: 0x000418A3 File Offset: 0x0003FAA3
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17000993 RID: 2451
			// (set) Token: 0x0600204D RID: 8269 RVA: 0x000418BB File Offset: 0x0003FABB
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17000994 RID: 2452
			// (set) Token: 0x0600204E RID: 8270 RVA: 0x000418D3 File Offset: 0x0003FAD3
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}

			// Token: 0x17000995 RID: 2453
			// (set) Token: 0x0600204F RID: 8271 RVA: 0x000418EB File Offset: 0x0003FAEB
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
