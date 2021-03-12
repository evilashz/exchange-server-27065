using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x020002DB RID: 731
	public class GetPartnerApplicationCommand : SyntheticCommandWithPipelineInput<PartnerApplication, PartnerApplication>
	{
		// Token: 0x06003212 RID: 12818 RVA: 0x00058DD0 File Offset: 0x00056FD0
		private GetPartnerApplicationCommand() : base("Get-PartnerApplication")
		{
		}

		// Token: 0x06003213 RID: 12819 RVA: 0x00058DDD File Offset: 0x00056FDD
		public GetPartnerApplicationCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x06003214 RID: 12820 RVA: 0x00058DEC File Offset: 0x00056FEC
		public virtual GetPartnerApplicationCommand SetParameters(GetPartnerApplicationCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x06003215 RID: 12821 RVA: 0x00058DF6 File Offset: 0x00056FF6
		public virtual GetPartnerApplicationCommand SetParameters(GetPartnerApplicationCommand.IdentityParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x020002DC RID: 732
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x17001815 RID: 6165
			// (set) Token: 0x06003216 RID: 12822 RVA: 0x00058E00 File Offset: 0x00057000
			public virtual string Organization
			{
				set
				{
					base.PowerSharpParameters["Organization"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x17001816 RID: 6166
			// (set) Token: 0x06003217 RID: 12823 RVA: 0x00058E1E File Offset: 0x0005701E
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17001817 RID: 6167
			// (set) Token: 0x06003218 RID: 12824 RVA: 0x00058E31 File Offset: 0x00057031
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17001818 RID: 6168
			// (set) Token: 0x06003219 RID: 12825 RVA: 0x00058E49 File Offset: 0x00057049
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17001819 RID: 6169
			// (set) Token: 0x0600321A RID: 12826 RVA: 0x00058E61 File Offset: 0x00057061
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x1700181A RID: 6170
			// (set) Token: 0x0600321B RID: 12827 RVA: 0x00058E79 File Offset: 0x00057079
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}
		}

		// Token: 0x020002DD RID: 733
		public class IdentityParameters : ParametersBase
		{
			// Token: 0x1700181B RID: 6171
			// (set) Token: 0x0600321D RID: 12829 RVA: 0x00058E99 File Offset: 0x00057099
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new PartnerApplicationIdParameter(value) : null);
				}
			}

			// Token: 0x1700181C RID: 6172
			// (set) Token: 0x0600321E RID: 12830 RVA: 0x00058EB7 File Offset: 0x000570B7
			public virtual string Organization
			{
				set
				{
					base.PowerSharpParameters["Organization"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x1700181D RID: 6173
			// (set) Token: 0x0600321F RID: 12831 RVA: 0x00058ED5 File Offset: 0x000570D5
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x1700181E RID: 6174
			// (set) Token: 0x06003220 RID: 12832 RVA: 0x00058EE8 File Offset: 0x000570E8
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x1700181F RID: 6175
			// (set) Token: 0x06003221 RID: 12833 RVA: 0x00058F00 File Offset: 0x00057100
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17001820 RID: 6176
			// (set) Token: 0x06003222 RID: 12834 RVA: 0x00058F18 File Offset: 0x00057118
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17001821 RID: 6177
			// (set) Token: 0x06003223 RID: 12835 RVA: 0x00058F30 File Offset: 0x00057130
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
