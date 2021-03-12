using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x02000865 RID: 2149
	public class SetAccountPartitionCommand : SyntheticCommandWithPipelineInputNoOutput<AccountPartition>
	{
		// Token: 0x06006A95 RID: 27285 RVA: 0x000A1B2F File Offset: 0x0009FD2F
		private SetAccountPartitionCommand() : base("Set-AccountPartition")
		{
		}

		// Token: 0x06006A96 RID: 27286 RVA: 0x000A1B3C File Offset: 0x0009FD3C
		public SetAccountPartitionCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x06006A97 RID: 27287 RVA: 0x000A1B4B File Offset: 0x0009FD4B
		public virtual SetAccountPartitionCommand SetParameters(SetAccountPartitionCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x06006A98 RID: 27288 RVA: 0x000A1B55 File Offset: 0x0009FD55
		public virtual SetAccountPartitionCommand SetParameters(SetAccountPartitionCommand.IdentityParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x02000866 RID: 2150
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x17004584 RID: 17796
			// (set) Token: 0x06006A99 RID: 27289 RVA: 0x000A1B5F File Offset: 0x0009FD5F
			public virtual Fqdn Trust
			{
				set
				{
					base.PowerSharpParameters["Trust"] = value;
				}
			}

			// Token: 0x17004585 RID: 17797
			// (set) Token: 0x06006A9A RID: 27290 RVA: 0x000A1B72 File Offset: 0x0009FD72
			public virtual SwitchParameter EnabledForProvisioning
			{
				set
				{
					base.PowerSharpParameters["EnabledForProvisioning"] = value;
				}
			}

			// Token: 0x17004586 RID: 17798
			// (set) Token: 0x06006A9B RID: 27291 RVA: 0x000A1B8A File Offset: 0x0009FD8A
			public virtual SwitchParameter Force
			{
				set
				{
					base.PowerSharpParameters["Force"] = value;
				}
			}

			// Token: 0x17004587 RID: 17799
			// (set) Token: 0x06006A9C RID: 27292 RVA: 0x000A1BA2 File Offset: 0x0009FDA2
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17004588 RID: 17800
			// (set) Token: 0x06006A9D RID: 27293 RVA: 0x000A1BB5 File Offset: 0x0009FDB5
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17004589 RID: 17801
			// (set) Token: 0x06006A9E RID: 27294 RVA: 0x000A1BCD File Offset: 0x0009FDCD
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x1700458A RID: 17802
			// (set) Token: 0x06006A9F RID: 27295 RVA: 0x000A1BE5 File Offset: 0x0009FDE5
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x1700458B RID: 17803
			// (set) Token: 0x06006AA0 RID: 27296 RVA: 0x000A1BFD File Offset: 0x0009FDFD
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x1700458C RID: 17804
			// (set) Token: 0x06006AA1 RID: 27297 RVA: 0x000A1C15 File Offset: 0x0009FE15
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}

			// Token: 0x1700458D RID: 17805
			// (set) Token: 0x06006AA2 RID: 27298 RVA: 0x000A1C2D File Offset: 0x0009FE2D
			public virtual SwitchParameter Confirm
			{
				set
				{
					base.PowerSharpParameters["Confirm"] = value;
				}
			}
		}

		// Token: 0x02000867 RID: 2151
		public class IdentityParameters : ParametersBase
		{
			// Token: 0x1700458E RID: 17806
			// (set) Token: 0x06006AA4 RID: 27300 RVA: 0x000A1C4D File Offset: 0x0009FE4D
			public virtual AccountPartitionIdParameter Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = value;
				}
			}

			// Token: 0x1700458F RID: 17807
			// (set) Token: 0x06006AA5 RID: 27301 RVA: 0x000A1C60 File Offset: 0x0009FE60
			public virtual Fqdn Trust
			{
				set
				{
					base.PowerSharpParameters["Trust"] = value;
				}
			}

			// Token: 0x17004590 RID: 17808
			// (set) Token: 0x06006AA6 RID: 27302 RVA: 0x000A1C73 File Offset: 0x0009FE73
			public virtual SwitchParameter EnabledForProvisioning
			{
				set
				{
					base.PowerSharpParameters["EnabledForProvisioning"] = value;
				}
			}

			// Token: 0x17004591 RID: 17809
			// (set) Token: 0x06006AA7 RID: 27303 RVA: 0x000A1C8B File Offset: 0x0009FE8B
			public virtual SwitchParameter Force
			{
				set
				{
					base.PowerSharpParameters["Force"] = value;
				}
			}

			// Token: 0x17004592 RID: 17810
			// (set) Token: 0x06006AA8 RID: 27304 RVA: 0x000A1CA3 File Offset: 0x0009FEA3
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17004593 RID: 17811
			// (set) Token: 0x06006AA9 RID: 27305 RVA: 0x000A1CB6 File Offset: 0x0009FEB6
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17004594 RID: 17812
			// (set) Token: 0x06006AAA RID: 27306 RVA: 0x000A1CCE File Offset: 0x0009FECE
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17004595 RID: 17813
			// (set) Token: 0x06006AAB RID: 27307 RVA: 0x000A1CE6 File Offset: 0x0009FEE6
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17004596 RID: 17814
			// (set) Token: 0x06006AAC RID: 27308 RVA: 0x000A1CFE File Offset: 0x0009FEFE
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17004597 RID: 17815
			// (set) Token: 0x06006AAD RID: 27309 RVA: 0x000A1D16 File Offset: 0x0009FF16
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}

			// Token: 0x17004598 RID: 17816
			// (set) Token: 0x06006AAE RID: 27310 RVA: 0x000A1D2E File Offset: 0x0009FF2E
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
