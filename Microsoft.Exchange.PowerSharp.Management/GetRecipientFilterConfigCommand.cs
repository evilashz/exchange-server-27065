using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x02000795 RID: 1941
	public class GetRecipientFilterConfigCommand : SyntheticCommandWithPipelineInput<RecipientFilterConfig, RecipientFilterConfig>
	{
		// Token: 0x060061D5 RID: 25045 RVA: 0x000966BE File Offset: 0x000948BE
		private GetRecipientFilterConfigCommand() : base("Get-RecipientFilterConfig")
		{
		}

		// Token: 0x060061D6 RID: 25046 RVA: 0x000966CB File Offset: 0x000948CB
		public GetRecipientFilterConfigCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x060061D7 RID: 25047 RVA: 0x000966DA File Offset: 0x000948DA
		public virtual GetRecipientFilterConfigCommand SetParameters(GetRecipientFilterConfigCommand.IdentityParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x060061D8 RID: 25048 RVA: 0x000966E4 File Offset: 0x000948E4
		public virtual GetRecipientFilterConfigCommand SetParameters(GetRecipientFilterConfigCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x02000796 RID: 1942
		public class IdentityParameters : ParametersBase
		{
			// Token: 0x17003E64 RID: 15972
			// (set) Token: 0x060061D9 RID: 25049 RVA: 0x000966EE File Offset: 0x000948EE
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x17003E65 RID: 15973
			// (set) Token: 0x060061DA RID: 25050 RVA: 0x0009670C File Offset: 0x0009490C
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17003E66 RID: 15974
			// (set) Token: 0x060061DB RID: 25051 RVA: 0x0009671F File Offset: 0x0009491F
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17003E67 RID: 15975
			// (set) Token: 0x060061DC RID: 25052 RVA: 0x00096737 File Offset: 0x00094937
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17003E68 RID: 15976
			// (set) Token: 0x060061DD RID: 25053 RVA: 0x0009674F File Offset: 0x0009494F
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17003E69 RID: 15977
			// (set) Token: 0x060061DE RID: 25054 RVA: 0x00096767 File Offset: 0x00094967
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}
		}

		// Token: 0x02000797 RID: 1943
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x17003E6A RID: 15978
			// (set) Token: 0x060061E0 RID: 25056 RVA: 0x00096787 File Offset: 0x00094987
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17003E6B RID: 15979
			// (set) Token: 0x060061E1 RID: 25057 RVA: 0x0009679A File Offset: 0x0009499A
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17003E6C RID: 15980
			// (set) Token: 0x060061E2 RID: 25058 RVA: 0x000967B2 File Offset: 0x000949B2
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17003E6D RID: 15981
			// (set) Token: 0x060061E3 RID: 25059 RVA: 0x000967CA File Offset: 0x000949CA
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17003E6E RID: 15982
			// (set) Token: 0x060061E4 RID: 25060 RVA: 0x000967E2 File Offset: 0x000949E2
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
