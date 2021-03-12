using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x020002F4 RID: 756
	public class RemoveAuthServerCommand : SyntheticCommandWithPipelineInput<AuthServer, AuthServer>
	{
		// Token: 0x0600330A RID: 13066 RVA: 0x0005A19E File Offset: 0x0005839E
		private RemoveAuthServerCommand() : base("Remove-AuthServer")
		{
		}

		// Token: 0x0600330B RID: 13067 RVA: 0x0005A1AB File Offset: 0x000583AB
		public RemoveAuthServerCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x0600330C RID: 13068 RVA: 0x0005A1BA File Offset: 0x000583BA
		public virtual RemoveAuthServerCommand SetParameters(RemoveAuthServerCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x0600330D RID: 13069 RVA: 0x0005A1C4 File Offset: 0x000583C4
		public virtual RemoveAuthServerCommand SetParameters(RemoveAuthServerCommand.IdentityParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x020002F5 RID: 757
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x170018DB RID: 6363
			// (set) Token: 0x0600330E RID: 13070 RVA: 0x0005A1CE File Offset: 0x000583CE
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x170018DC RID: 6364
			// (set) Token: 0x0600330F RID: 13071 RVA: 0x0005A1E1 File Offset: 0x000583E1
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x170018DD RID: 6365
			// (set) Token: 0x06003310 RID: 13072 RVA: 0x0005A1F9 File Offset: 0x000583F9
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x170018DE RID: 6366
			// (set) Token: 0x06003311 RID: 13073 RVA: 0x0005A211 File Offset: 0x00058411
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x170018DF RID: 6367
			// (set) Token: 0x06003312 RID: 13074 RVA: 0x0005A229 File Offset: 0x00058429
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x170018E0 RID: 6368
			// (set) Token: 0x06003313 RID: 13075 RVA: 0x0005A241 File Offset: 0x00058441
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}

			// Token: 0x170018E1 RID: 6369
			// (set) Token: 0x06003314 RID: 13076 RVA: 0x0005A259 File Offset: 0x00058459
			public virtual SwitchParameter Confirm
			{
				set
				{
					base.PowerSharpParameters["Confirm"] = value;
				}
			}
		}

		// Token: 0x020002F6 RID: 758
		public class IdentityParameters : ParametersBase
		{
			// Token: 0x170018E2 RID: 6370
			// (set) Token: 0x06003316 RID: 13078 RVA: 0x0005A279 File Offset: 0x00058479
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new AuthServerIdParameter(value) : null);
				}
			}

			// Token: 0x170018E3 RID: 6371
			// (set) Token: 0x06003317 RID: 13079 RVA: 0x0005A297 File Offset: 0x00058497
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x170018E4 RID: 6372
			// (set) Token: 0x06003318 RID: 13080 RVA: 0x0005A2AA File Offset: 0x000584AA
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x170018E5 RID: 6373
			// (set) Token: 0x06003319 RID: 13081 RVA: 0x0005A2C2 File Offset: 0x000584C2
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x170018E6 RID: 6374
			// (set) Token: 0x0600331A RID: 13082 RVA: 0x0005A2DA File Offset: 0x000584DA
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x170018E7 RID: 6375
			// (set) Token: 0x0600331B RID: 13083 RVA: 0x0005A2F2 File Offset: 0x000584F2
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x170018E8 RID: 6376
			// (set) Token: 0x0600331C RID: 13084 RVA: 0x0005A30A File Offset: 0x0005850A
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}

			// Token: 0x170018E9 RID: 6377
			// (set) Token: 0x0600331D RID: 13085 RVA: 0x0005A322 File Offset: 0x00058522
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
