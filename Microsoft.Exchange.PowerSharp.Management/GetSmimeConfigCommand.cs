using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x02000818 RID: 2072
	public class GetSmimeConfigCommand : SyntheticCommandWithPipelineInput<SmimeConfigurationContainer, SmimeConfigurationContainer>
	{
		// Token: 0x06006651 RID: 26193 RVA: 0x0009C111 File Offset: 0x0009A311
		private GetSmimeConfigCommand() : base("Get-SmimeConfig")
		{
		}

		// Token: 0x06006652 RID: 26194 RVA: 0x0009C11E File Offset: 0x0009A31E
		public GetSmimeConfigCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x06006653 RID: 26195 RVA: 0x0009C12D File Offset: 0x0009A32D
		public virtual GetSmimeConfigCommand SetParameters(GetSmimeConfigCommand.IdentityParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x06006654 RID: 26196 RVA: 0x0009C137 File Offset: 0x0009A337
		public virtual GetSmimeConfigCommand SetParameters(GetSmimeConfigCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x02000819 RID: 2073
		public class IdentityParameters : ParametersBase
		{
			// Token: 0x170041DA RID: 16858
			// (set) Token: 0x06006655 RID: 26197 RVA: 0x0009C141 File Offset: 0x0009A341
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x170041DB RID: 16859
			// (set) Token: 0x06006656 RID: 26198 RVA: 0x0009C15F File Offset: 0x0009A35F
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x170041DC RID: 16860
			// (set) Token: 0x06006657 RID: 26199 RVA: 0x0009C172 File Offset: 0x0009A372
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x170041DD RID: 16861
			// (set) Token: 0x06006658 RID: 26200 RVA: 0x0009C18A File Offset: 0x0009A38A
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x170041DE RID: 16862
			// (set) Token: 0x06006659 RID: 26201 RVA: 0x0009C1A2 File Offset: 0x0009A3A2
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x170041DF RID: 16863
			// (set) Token: 0x0600665A RID: 26202 RVA: 0x0009C1BA File Offset: 0x0009A3BA
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}
		}

		// Token: 0x0200081A RID: 2074
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x170041E0 RID: 16864
			// (set) Token: 0x0600665C RID: 26204 RVA: 0x0009C1DA File Offset: 0x0009A3DA
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x170041E1 RID: 16865
			// (set) Token: 0x0600665D RID: 26205 RVA: 0x0009C1ED File Offset: 0x0009A3ED
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x170041E2 RID: 16866
			// (set) Token: 0x0600665E RID: 26206 RVA: 0x0009C205 File Offset: 0x0009A405
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x170041E3 RID: 16867
			// (set) Token: 0x0600665F RID: 26207 RVA: 0x0009C21D File Offset: 0x0009A41D
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x170041E4 RID: 16868
			// (set) Token: 0x06006660 RID: 26208 RVA: 0x0009C235 File Offset: 0x0009A435
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
