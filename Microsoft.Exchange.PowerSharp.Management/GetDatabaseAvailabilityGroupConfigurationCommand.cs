using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Management.SystemConfigurationTasks;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x02000538 RID: 1336
	public class GetDatabaseAvailabilityGroupConfigurationCommand : SyntheticCommandWithPipelineInput<DagConfigurationEntry, DagConfigurationEntry>
	{
		// Token: 0x0600477A RID: 18298 RVA: 0x0007431F File Offset: 0x0007251F
		private GetDatabaseAvailabilityGroupConfigurationCommand() : base("Get-DatabaseAvailabilityGroupConfiguration")
		{
		}

		// Token: 0x0600477B RID: 18299 RVA: 0x0007432C File Offset: 0x0007252C
		public GetDatabaseAvailabilityGroupConfigurationCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x0600477C RID: 18300 RVA: 0x0007433B File Offset: 0x0007253B
		public virtual GetDatabaseAvailabilityGroupConfigurationCommand SetParameters(GetDatabaseAvailabilityGroupConfigurationCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x0600477D RID: 18301 RVA: 0x00074345 File Offset: 0x00072545
		public virtual GetDatabaseAvailabilityGroupConfigurationCommand SetParameters(GetDatabaseAvailabilityGroupConfigurationCommand.IdentityParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x02000539 RID: 1337
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x170028C3 RID: 10435
			// (set) Token: 0x0600477E RID: 18302 RVA: 0x0007434F File Offset: 0x0007254F
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x170028C4 RID: 10436
			// (set) Token: 0x0600477F RID: 18303 RVA: 0x00074362 File Offset: 0x00072562
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x170028C5 RID: 10437
			// (set) Token: 0x06004780 RID: 18304 RVA: 0x0007437A File Offset: 0x0007257A
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x170028C6 RID: 10438
			// (set) Token: 0x06004781 RID: 18305 RVA: 0x00074392 File Offset: 0x00072592
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x170028C7 RID: 10439
			// (set) Token: 0x06004782 RID: 18306 RVA: 0x000743AA File Offset: 0x000725AA
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}
		}

		// Token: 0x0200053A RID: 1338
		public class IdentityParameters : ParametersBase
		{
			// Token: 0x170028C8 RID: 10440
			// (set) Token: 0x06004784 RID: 18308 RVA: 0x000743CA File Offset: 0x000725CA
			public virtual DatabaseAvailabilityGroupConfigurationIdParameter Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = value;
				}
			}

			// Token: 0x170028C9 RID: 10441
			// (set) Token: 0x06004785 RID: 18309 RVA: 0x000743DD File Offset: 0x000725DD
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x170028CA RID: 10442
			// (set) Token: 0x06004786 RID: 18310 RVA: 0x000743F0 File Offset: 0x000725F0
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x170028CB RID: 10443
			// (set) Token: 0x06004787 RID: 18311 RVA: 0x00074408 File Offset: 0x00072608
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x170028CC RID: 10444
			// (set) Token: 0x06004788 RID: 18312 RVA: 0x00074420 File Offset: 0x00072620
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x170028CD RID: 10445
			// (set) Token: 0x06004789 RID: 18313 RVA: 0x00074438 File Offset: 0x00072638
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
