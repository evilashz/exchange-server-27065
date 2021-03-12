using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x020007AB RID: 1963
	public class UpdateEmailAddressPolicyCommand : SyntheticCommandWithPipelineInput<EmailAddressPolicy, EmailAddressPolicy>
	{
		// Token: 0x06006275 RID: 25205 RVA: 0x00097327 File Offset: 0x00095527
		private UpdateEmailAddressPolicyCommand() : base("Update-EmailAddressPolicy")
		{
		}

		// Token: 0x06006276 RID: 25206 RVA: 0x00097334 File Offset: 0x00095534
		public UpdateEmailAddressPolicyCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x06006277 RID: 25207 RVA: 0x00097343 File Offset: 0x00095543
		public virtual UpdateEmailAddressPolicyCommand SetParameters(UpdateEmailAddressPolicyCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x06006278 RID: 25208 RVA: 0x0009734D File Offset: 0x0009554D
		public virtual UpdateEmailAddressPolicyCommand SetParameters(UpdateEmailAddressPolicyCommand.IdentityParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x020007AC RID: 1964
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x17003ED8 RID: 16088
			// (set) Token: 0x06006279 RID: 25209 RVA: 0x00097357 File Offset: 0x00095557
			public virtual SwitchParameter FixMissingAlias
			{
				set
				{
					base.PowerSharpParameters["FixMissingAlias"] = value;
				}
			}

			// Token: 0x17003ED9 RID: 16089
			// (set) Token: 0x0600627A RID: 25210 RVA: 0x0009736F File Offset: 0x0009556F
			public virtual SwitchParameter UpdateSecondaryAddressesOnly
			{
				set
				{
					base.PowerSharpParameters["UpdateSecondaryAddressesOnly"] = value;
				}
			}

			// Token: 0x17003EDA RID: 16090
			// (set) Token: 0x0600627B RID: 25211 RVA: 0x00097387 File Offset: 0x00095587
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17003EDB RID: 16091
			// (set) Token: 0x0600627C RID: 25212 RVA: 0x0009739A File Offset: 0x0009559A
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17003EDC RID: 16092
			// (set) Token: 0x0600627D RID: 25213 RVA: 0x000973B2 File Offset: 0x000955B2
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17003EDD RID: 16093
			// (set) Token: 0x0600627E RID: 25214 RVA: 0x000973CA File Offset: 0x000955CA
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17003EDE RID: 16094
			// (set) Token: 0x0600627F RID: 25215 RVA: 0x000973E2 File Offset: 0x000955E2
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17003EDF RID: 16095
			// (set) Token: 0x06006280 RID: 25216 RVA: 0x000973FA File Offset: 0x000955FA
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}

		// Token: 0x020007AD RID: 1965
		public class IdentityParameters : ParametersBase
		{
			// Token: 0x17003EE0 RID: 16096
			// (set) Token: 0x06006282 RID: 25218 RVA: 0x0009741A File Offset: 0x0009561A
			public virtual EmailAddressPolicyIdParameter Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = value;
				}
			}

			// Token: 0x17003EE1 RID: 16097
			// (set) Token: 0x06006283 RID: 25219 RVA: 0x0009742D File Offset: 0x0009562D
			public virtual SwitchParameter FixMissingAlias
			{
				set
				{
					base.PowerSharpParameters["FixMissingAlias"] = value;
				}
			}

			// Token: 0x17003EE2 RID: 16098
			// (set) Token: 0x06006284 RID: 25220 RVA: 0x00097445 File Offset: 0x00095645
			public virtual SwitchParameter UpdateSecondaryAddressesOnly
			{
				set
				{
					base.PowerSharpParameters["UpdateSecondaryAddressesOnly"] = value;
				}
			}

			// Token: 0x17003EE3 RID: 16099
			// (set) Token: 0x06006285 RID: 25221 RVA: 0x0009745D File Offset: 0x0009565D
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17003EE4 RID: 16100
			// (set) Token: 0x06006286 RID: 25222 RVA: 0x00097470 File Offset: 0x00095670
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17003EE5 RID: 16101
			// (set) Token: 0x06006287 RID: 25223 RVA: 0x00097488 File Offset: 0x00095688
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17003EE6 RID: 16102
			// (set) Token: 0x06006288 RID: 25224 RVA: 0x000974A0 File Offset: 0x000956A0
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17003EE7 RID: 16103
			// (set) Token: 0x06006289 RID: 25225 RVA: 0x000974B8 File Offset: 0x000956B8
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17003EE8 RID: 16104
			// (set) Token: 0x0600628A RID: 25226 RVA: 0x000974D0 File Offset: 0x000956D0
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}
	}
}
