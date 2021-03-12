using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x02000682 RID: 1666
	public class GetOrganizationRelationshipCommand : SyntheticCommandWithPipelineInput<OrganizationRelationship, OrganizationRelationship>
	{
		// Token: 0x060058C2 RID: 22722 RVA: 0x0008AF62 File Offset: 0x00089162
		private GetOrganizationRelationshipCommand() : base("Get-OrganizationRelationship")
		{
		}

		// Token: 0x060058C3 RID: 22723 RVA: 0x0008AF6F File Offset: 0x0008916F
		public GetOrganizationRelationshipCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x060058C4 RID: 22724 RVA: 0x0008AF7E File Offset: 0x0008917E
		public virtual GetOrganizationRelationshipCommand SetParameters(GetOrganizationRelationshipCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x060058C5 RID: 22725 RVA: 0x0008AF88 File Offset: 0x00089188
		public virtual GetOrganizationRelationshipCommand SetParameters(GetOrganizationRelationshipCommand.IdentityParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x02000683 RID: 1667
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x17003777 RID: 14199
			// (set) Token: 0x060058C6 RID: 22726 RVA: 0x0008AF92 File Offset: 0x00089192
			public virtual string Organization
			{
				set
				{
					base.PowerSharpParameters["Organization"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x17003778 RID: 14200
			// (set) Token: 0x060058C7 RID: 22727 RVA: 0x0008AFB0 File Offset: 0x000891B0
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17003779 RID: 14201
			// (set) Token: 0x060058C8 RID: 22728 RVA: 0x0008AFC3 File Offset: 0x000891C3
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x1700377A RID: 14202
			// (set) Token: 0x060058C9 RID: 22729 RVA: 0x0008AFDB File Offset: 0x000891DB
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x1700377B RID: 14203
			// (set) Token: 0x060058CA RID: 22730 RVA: 0x0008AFF3 File Offset: 0x000891F3
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x1700377C RID: 14204
			// (set) Token: 0x060058CB RID: 22731 RVA: 0x0008B00B File Offset: 0x0008920B
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}
		}

		// Token: 0x02000684 RID: 1668
		public class IdentityParameters : ParametersBase
		{
			// Token: 0x1700377D RID: 14205
			// (set) Token: 0x060058CD RID: 22733 RVA: 0x0008B02B File Offset: 0x0008922B
			public virtual OrganizationRelationshipIdParameter Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = value;
				}
			}

			// Token: 0x1700377E RID: 14206
			// (set) Token: 0x060058CE RID: 22734 RVA: 0x0008B03E File Offset: 0x0008923E
			public virtual string Organization
			{
				set
				{
					base.PowerSharpParameters["Organization"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x1700377F RID: 14207
			// (set) Token: 0x060058CF RID: 22735 RVA: 0x0008B05C File Offset: 0x0008925C
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17003780 RID: 14208
			// (set) Token: 0x060058D0 RID: 22736 RVA: 0x0008B06F File Offset: 0x0008926F
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17003781 RID: 14209
			// (set) Token: 0x060058D1 RID: 22737 RVA: 0x0008B087 File Offset: 0x00089287
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17003782 RID: 14210
			// (set) Token: 0x060058D2 RID: 22738 RVA: 0x0008B09F File Offset: 0x0008929F
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17003783 RID: 14211
			// (set) Token: 0x060058D3 RID: 22739 RVA: 0x0008B0B7 File Offset: 0x000892B7
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
