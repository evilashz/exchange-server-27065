using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x02000857 RID: 2135
	public class RemoveRemoteAccountPolicyCommand : SyntheticCommandWithPipelineInput<RemoteAccountPolicy, RemoteAccountPolicy>
	{
		// Token: 0x06006A22 RID: 27170 RVA: 0x000A124C File Offset: 0x0009F44C
		private RemoveRemoteAccountPolicyCommand() : base("Remove-RemoteAccountPolicy")
		{
		}

		// Token: 0x06006A23 RID: 27171 RVA: 0x000A1259 File Offset: 0x0009F459
		public RemoveRemoteAccountPolicyCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x06006A24 RID: 27172 RVA: 0x000A1268 File Offset: 0x0009F468
		public virtual RemoveRemoteAccountPolicyCommand SetParameters(RemoveRemoteAccountPolicyCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x06006A25 RID: 27173 RVA: 0x000A1272 File Offset: 0x0009F472
		public virtual RemoveRemoteAccountPolicyCommand SetParameters(RemoveRemoteAccountPolicyCommand.IdentityParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x02000858 RID: 2136
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x1700452D RID: 17709
			// (set) Token: 0x06006A26 RID: 27174 RVA: 0x000A127C File Offset: 0x0009F47C
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x1700452E RID: 17710
			// (set) Token: 0x06006A27 RID: 27175 RVA: 0x000A128F File Offset: 0x0009F48F
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x1700452F RID: 17711
			// (set) Token: 0x06006A28 RID: 27176 RVA: 0x000A12A7 File Offset: 0x0009F4A7
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17004530 RID: 17712
			// (set) Token: 0x06006A29 RID: 27177 RVA: 0x000A12BF File Offset: 0x0009F4BF
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17004531 RID: 17713
			// (set) Token: 0x06006A2A RID: 27178 RVA: 0x000A12D7 File Offset: 0x0009F4D7
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17004532 RID: 17714
			// (set) Token: 0x06006A2B RID: 27179 RVA: 0x000A12EF File Offset: 0x0009F4EF
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}

			// Token: 0x17004533 RID: 17715
			// (set) Token: 0x06006A2C RID: 27180 RVA: 0x000A1307 File Offset: 0x0009F507
			public virtual SwitchParameter Confirm
			{
				set
				{
					base.PowerSharpParameters["Confirm"] = value;
				}
			}
		}

		// Token: 0x02000859 RID: 2137
		public class IdentityParameters : ParametersBase
		{
			// Token: 0x17004534 RID: 17716
			// (set) Token: 0x06006A2E RID: 27182 RVA: 0x000A1327 File Offset: 0x0009F527
			public virtual RemoteAccountPolicyIdParameter Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = value;
				}
			}

			// Token: 0x17004535 RID: 17717
			// (set) Token: 0x06006A2F RID: 27183 RVA: 0x000A133A File Offset: 0x0009F53A
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17004536 RID: 17718
			// (set) Token: 0x06006A30 RID: 27184 RVA: 0x000A134D File Offset: 0x0009F54D
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17004537 RID: 17719
			// (set) Token: 0x06006A31 RID: 27185 RVA: 0x000A1365 File Offset: 0x0009F565
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17004538 RID: 17720
			// (set) Token: 0x06006A32 RID: 27186 RVA: 0x000A137D File Offset: 0x0009F57D
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17004539 RID: 17721
			// (set) Token: 0x06006A33 RID: 27187 RVA: 0x000A1395 File Offset: 0x0009F595
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x1700453A RID: 17722
			// (set) Token: 0x06006A34 RID: 27188 RVA: 0x000A13AD File Offset: 0x0009F5AD
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}

			// Token: 0x1700453B RID: 17723
			// (set) Token: 0x06006A35 RID: 27189 RVA: 0x000A13C5 File Offset: 0x0009F5C5
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
