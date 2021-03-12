using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x020004E8 RID: 1256
	public class GetAvailabilityConfigCommand : SyntheticCommandWithPipelineInput<AvailabilityConfig, AvailabilityConfig>
	{
		// Token: 0x0600450F RID: 17679 RVA: 0x0007131C File Offset: 0x0006F51C
		private GetAvailabilityConfigCommand() : base("Get-AvailabilityConfig")
		{
		}

		// Token: 0x06004510 RID: 17680 RVA: 0x00071329 File Offset: 0x0006F529
		public GetAvailabilityConfigCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x06004511 RID: 17681 RVA: 0x00071338 File Offset: 0x0006F538
		public virtual GetAvailabilityConfigCommand SetParameters(GetAvailabilityConfigCommand.IdentityParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x06004512 RID: 17682 RVA: 0x00071342 File Offset: 0x0006F542
		public virtual GetAvailabilityConfigCommand SetParameters(GetAvailabilityConfigCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x020004E9 RID: 1257
		public class IdentityParameters : ParametersBase
		{
			// Token: 0x170026F8 RID: 9976
			// (set) Token: 0x06004513 RID: 17683 RVA: 0x0007134C File Offset: 0x0006F54C
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x170026F9 RID: 9977
			// (set) Token: 0x06004514 RID: 17684 RVA: 0x0007136A File Offset: 0x0006F56A
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x170026FA RID: 9978
			// (set) Token: 0x06004515 RID: 17685 RVA: 0x0007137D File Offset: 0x0006F57D
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x170026FB RID: 9979
			// (set) Token: 0x06004516 RID: 17686 RVA: 0x00071395 File Offset: 0x0006F595
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x170026FC RID: 9980
			// (set) Token: 0x06004517 RID: 17687 RVA: 0x000713AD File Offset: 0x0006F5AD
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x170026FD RID: 9981
			// (set) Token: 0x06004518 RID: 17688 RVA: 0x000713C5 File Offset: 0x0006F5C5
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}
		}

		// Token: 0x020004EA RID: 1258
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x170026FE RID: 9982
			// (set) Token: 0x0600451A RID: 17690 RVA: 0x000713E5 File Offset: 0x0006F5E5
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x170026FF RID: 9983
			// (set) Token: 0x0600451B RID: 17691 RVA: 0x000713F8 File Offset: 0x0006F5F8
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17002700 RID: 9984
			// (set) Token: 0x0600451C RID: 17692 RVA: 0x00071410 File Offset: 0x0006F610
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17002701 RID: 9985
			// (set) Token: 0x0600451D RID: 17693 RVA: 0x00071428 File Offset: 0x0006F628
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17002702 RID: 9986
			// (set) Token: 0x0600451E RID: 17694 RVA: 0x00071440 File Offset: 0x0006F640
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
