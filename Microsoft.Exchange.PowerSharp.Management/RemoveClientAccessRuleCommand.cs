using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x02000529 RID: 1321
	public class RemoveClientAccessRuleCommand : SyntheticCommandWithPipelineInput<ADClientAccessRule, ADClientAccessRule>
	{
		// Token: 0x060046EF RID: 18159 RVA: 0x00073872 File Offset: 0x00071A72
		private RemoveClientAccessRuleCommand() : base("Remove-ClientAccessRule")
		{
		}

		// Token: 0x060046F0 RID: 18160 RVA: 0x0007387F File Offset: 0x00071A7F
		public RemoveClientAccessRuleCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x060046F1 RID: 18161 RVA: 0x0007388E File Offset: 0x00071A8E
		public virtual RemoveClientAccessRuleCommand SetParameters(RemoveClientAccessRuleCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x060046F2 RID: 18162 RVA: 0x00073898 File Offset: 0x00071A98
		public virtual RemoveClientAccessRuleCommand SetParameters(RemoveClientAccessRuleCommand.IdentityParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x0200052A RID: 1322
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x17002856 RID: 10326
			// (set) Token: 0x060046F3 RID: 18163 RVA: 0x000738A2 File Offset: 0x00071AA2
			public virtual SwitchParameter DatacenterAdminsOnly
			{
				set
				{
					base.PowerSharpParameters["DatacenterAdminsOnly"] = value;
				}
			}

			// Token: 0x17002857 RID: 10327
			// (set) Token: 0x060046F4 RID: 18164 RVA: 0x000738BA File Offset: 0x00071ABA
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17002858 RID: 10328
			// (set) Token: 0x060046F5 RID: 18165 RVA: 0x000738CD File Offset: 0x00071ACD
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17002859 RID: 10329
			// (set) Token: 0x060046F6 RID: 18166 RVA: 0x000738E5 File Offset: 0x00071AE5
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x1700285A RID: 10330
			// (set) Token: 0x060046F7 RID: 18167 RVA: 0x000738FD File Offset: 0x00071AFD
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x1700285B RID: 10331
			// (set) Token: 0x060046F8 RID: 18168 RVA: 0x00073915 File Offset: 0x00071B15
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x1700285C RID: 10332
			// (set) Token: 0x060046F9 RID: 18169 RVA: 0x0007392D File Offset: 0x00071B2D
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}

			// Token: 0x1700285D RID: 10333
			// (set) Token: 0x060046FA RID: 18170 RVA: 0x00073945 File Offset: 0x00071B45
			public virtual SwitchParameter Confirm
			{
				set
				{
					base.PowerSharpParameters["Confirm"] = value;
				}
			}
		}

		// Token: 0x0200052B RID: 1323
		public class IdentityParameters : ParametersBase
		{
			// Token: 0x1700285E RID: 10334
			// (set) Token: 0x060046FC RID: 18172 RVA: 0x00073965 File Offset: 0x00071B65
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new ClientAccessRuleIdParameter(value) : null);
				}
			}

			// Token: 0x1700285F RID: 10335
			// (set) Token: 0x060046FD RID: 18173 RVA: 0x00073983 File Offset: 0x00071B83
			public virtual SwitchParameter DatacenterAdminsOnly
			{
				set
				{
					base.PowerSharpParameters["DatacenterAdminsOnly"] = value;
				}
			}

			// Token: 0x17002860 RID: 10336
			// (set) Token: 0x060046FE RID: 18174 RVA: 0x0007399B File Offset: 0x00071B9B
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17002861 RID: 10337
			// (set) Token: 0x060046FF RID: 18175 RVA: 0x000739AE File Offset: 0x00071BAE
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17002862 RID: 10338
			// (set) Token: 0x06004700 RID: 18176 RVA: 0x000739C6 File Offset: 0x00071BC6
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17002863 RID: 10339
			// (set) Token: 0x06004701 RID: 18177 RVA: 0x000739DE File Offset: 0x00071BDE
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17002864 RID: 10340
			// (set) Token: 0x06004702 RID: 18178 RVA: 0x000739F6 File Offset: 0x00071BF6
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17002865 RID: 10341
			// (set) Token: 0x06004703 RID: 18179 RVA: 0x00073A0E File Offset: 0x00071C0E
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}

			// Token: 0x17002866 RID: 10342
			// (set) Token: 0x06004704 RID: 18180 RVA: 0x00073A26 File Offset: 0x00071C26
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
