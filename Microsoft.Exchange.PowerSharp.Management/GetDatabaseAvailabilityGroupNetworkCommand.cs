using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x0200053B RID: 1339
	public class GetDatabaseAvailabilityGroupNetworkCommand : SyntheticCommandWithPipelineInput<DatabaseAvailabilityGroupNetwork, DatabaseAvailabilityGroupNetwork>
	{
		// Token: 0x0600478B RID: 18315 RVA: 0x00074458 File Offset: 0x00072658
		private GetDatabaseAvailabilityGroupNetworkCommand() : base("Get-DatabaseAvailabilityGroupNetwork")
		{
		}

		// Token: 0x0600478C RID: 18316 RVA: 0x00074465 File Offset: 0x00072665
		public GetDatabaseAvailabilityGroupNetworkCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x0600478D RID: 18317 RVA: 0x00074474 File Offset: 0x00072674
		public virtual GetDatabaseAvailabilityGroupNetworkCommand SetParameters(GetDatabaseAvailabilityGroupNetworkCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x0600478E RID: 18318 RVA: 0x0007447E File Offset: 0x0007267E
		public virtual GetDatabaseAvailabilityGroupNetworkCommand SetParameters(GetDatabaseAvailabilityGroupNetworkCommand.IdentityParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x0200053C RID: 1340
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x170028CE RID: 10446
			// (set) Token: 0x0600478F RID: 18319 RVA: 0x00074488 File Offset: 0x00072688
			public virtual ServerIdParameter Server
			{
				set
				{
					base.PowerSharpParameters["Server"] = value;
				}
			}

			// Token: 0x170028CF RID: 10447
			// (set) Token: 0x06004790 RID: 18320 RVA: 0x0007449B File Offset: 0x0007269B
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x170028D0 RID: 10448
			// (set) Token: 0x06004791 RID: 18321 RVA: 0x000744AE File Offset: 0x000726AE
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x170028D1 RID: 10449
			// (set) Token: 0x06004792 RID: 18322 RVA: 0x000744C6 File Offset: 0x000726C6
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x170028D2 RID: 10450
			// (set) Token: 0x06004793 RID: 18323 RVA: 0x000744DE File Offset: 0x000726DE
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x170028D3 RID: 10451
			// (set) Token: 0x06004794 RID: 18324 RVA: 0x000744F6 File Offset: 0x000726F6
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}
		}

		// Token: 0x0200053D RID: 1341
		public class IdentityParameters : ParametersBase
		{
			// Token: 0x170028D4 RID: 10452
			// (set) Token: 0x06004796 RID: 18326 RVA: 0x00074516 File Offset: 0x00072716
			public virtual DatabaseAvailabilityGroupNetworkIdParameter Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = value;
				}
			}

			// Token: 0x170028D5 RID: 10453
			// (set) Token: 0x06004797 RID: 18327 RVA: 0x00074529 File Offset: 0x00072729
			public virtual ServerIdParameter Server
			{
				set
				{
					base.PowerSharpParameters["Server"] = value;
				}
			}

			// Token: 0x170028D6 RID: 10454
			// (set) Token: 0x06004798 RID: 18328 RVA: 0x0007453C File Offset: 0x0007273C
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x170028D7 RID: 10455
			// (set) Token: 0x06004799 RID: 18329 RVA: 0x0007454F File Offset: 0x0007274F
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x170028D8 RID: 10456
			// (set) Token: 0x0600479A RID: 18330 RVA: 0x00074567 File Offset: 0x00072767
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x170028D9 RID: 10457
			// (set) Token: 0x0600479B RID: 18331 RVA: 0x0007457F File Offset: 0x0007277F
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x170028DA RID: 10458
			// (set) Token: 0x0600479C RID: 18332 RVA: 0x00074597 File Offset: 0x00072797
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
