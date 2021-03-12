using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x020000E1 RID: 225
	public class GetADQueryPolicyCommand : SyntheticCommandWithPipelineInput<ADQueryPolicy, ADQueryPolicy>
	{
		// Token: 0x06001D73 RID: 7539 RVA: 0x0003DEFF File Offset: 0x0003C0FF
		private GetADQueryPolicyCommand() : base("Get-ADQueryPolicy")
		{
		}

		// Token: 0x06001D74 RID: 7540 RVA: 0x0003DF0C File Offset: 0x0003C10C
		public GetADQueryPolicyCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x06001D75 RID: 7541 RVA: 0x0003DF1B File Offset: 0x0003C11B
		public virtual GetADQueryPolicyCommand SetParameters(GetADQueryPolicyCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x06001D76 RID: 7542 RVA: 0x0003DF25 File Offset: 0x0003C125
		public virtual GetADQueryPolicyCommand SetParameters(GetADQueryPolicyCommand.IdentityParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x020000E2 RID: 226
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x1700076A RID: 1898
			// (set) Token: 0x06001D77 RID: 7543 RVA: 0x0003DF2F File Offset: 0x0003C12F
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x1700076B RID: 1899
			// (set) Token: 0x06001D78 RID: 7544 RVA: 0x0003DF42 File Offset: 0x0003C142
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x1700076C RID: 1900
			// (set) Token: 0x06001D79 RID: 7545 RVA: 0x0003DF5A File Offset: 0x0003C15A
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x1700076D RID: 1901
			// (set) Token: 0x06001D7A RID: 7546 RVA: 0x0003DF72 File Offset: 0x0003C172
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x1700076E RID: 1902
			// (set) Token: 0x06001D7B RID: 7547 RVA: 0x0003DF8A File Offset: 0x0003C18A
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}
		}

		// Token: 0x020000E3 RID: 227
		public class IdentityParameters : ParametersBase
		{
			// Token: 0x1700076F RID: 1903
			// (set) Token: 0x06001D7D RID: 7549 RVA: 0x0003DFAA File Offset: 0x0003C1AA
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new ADQueryPolicyIdParameter(value) : null);
				}
			}

			// Token: 0x17000770 RID: 1904
			// (set) Token: 0x06001D7E RID: 7550 RVA: 0x0003DFC8 File Offset: 0x0003C1C8
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17000771 RID: 1905
			// (set) Token: 0x06001D7F RID: 7551 RVA: 0x0003DFDB File Offset: 0x0003C1DB
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17000772 RID: 1906
			// (set) Token: 0x06001D80 RID: 7552 RVA: 0x0003DFF3 File Offset: 0x0003C1F3
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17000773 RID: 1907
			// (set) Token: 0x06001D81 RID: 7553 RVA: 0x0003E00B File Offset: 0x0003C20B
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17000774 RID: 1908
			// (set) Token: 0x06001D82 RID: 7554 RVA: 0x0003E023 File Offset: 0x0003C223
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
