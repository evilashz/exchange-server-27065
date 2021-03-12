using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.Recipient;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x02000B7C RID: 2940
	public class RemoveUMMailboxPromptCommand : SyntheticCommandWithPipelineInput<ADUser, ADUser>
	{
		// Token: 0x06008E2D RID: 36397 RVA: 0x000D03AB File Offset: 0x000CE5AB
		private RemoveUMMailboxPromptCommand() : base("Remove-UMMailboxPrompt")
		{
		}

		// Token: 0x06008E2E RID: 36398 RVA: 0x000D03B8 File Offset: 0x000CE5B8
		public RemoveUMMailboxPromptCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x06008E2F RID: 36399 RVA: 0x000D03C7 File Offset: 0x000CE5C7
		public virtual RemoveUMMailboxPromptCommand SetParameters(RemoveUMMailboxPromptCommand.CustomAwayGreetingParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x06008E30 RID: 36400 RVA: 0x000D03D1 File Offset: 0x000CE5D1
		public virtual RemoveUMMailboxPromptCommand SetParameters(RemoveUMMailboxPromptCommand.CustomVoicemailGreetingParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x06008E31 RID: 36401 RVA: 0x000D03DB File Offset: 0x000CE5DB
		public virtual RemoveUMMailboxPromptCommand SetParameters(RemoveUMMailboxPromptCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x02000B7D RID: 2941
		public class CustomAwayGreetingParameters : ParametersBase
		{
			// Token: 0x170062EE RID: 25326
			// (set) Token: 0x06008E32 RID: 36402 RVA: 0x000D03E5 File Offset: 0x000CE5E5
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new MailboxIdParameter(value) : null);
				}
			}

			// Token: 0x170062EF RID: 25327
			// (set) Token: 0x06008E33 RID: 36403 RVA: 0x000D0403 File Offset: 0x000CE603
			public virtual SwitchParameter CustomAwayGreeting
			{
				set
				{
					base.PowerSharpParameters["CustomAwayGreeting"] = value;
				}
			}

			// Token: 0x170062F0 RID: 25328
			// (set) Token: 0x06008E34 RID: 36404 RVA: 0x000D041B File Offset: 0x000CE61B
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x170062F1 RID: 25329
			// (set) Token: 0x06008E35 RID: 36405 RVA: 0x000D042E File Offset: 0x000CE62E
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x170062F2 RID: 25330
			// (set) Token: 0x06008E36 RID: 36406 RVA: 0x000D0446 File Offset: 0x000CE646
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x170062F3 RID: 25331
			// (set) Token: 0x06008E37 RID: 36407 RVA: 0x000D045E File Offset: 0x000CE65E
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x170062F4 RID: 25332
			// (set) Token: 0x06008E38 RID: 36408 RVA: 0x000D0476 File Offset: 0x000CE676
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x170062F5 RID: 25333
			// (set) Token: 0x06008E39 RID: 36409 RVA: 0x000D048E File Offset: 0x000CE68E
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}

			// Token: 0x170062F6 RID: 25334
			// (set) Token: 0x06008E3A RID: 36410 RVA: 0x000D04A6 File Offset: 0x000CE6A6
			public virtual SwitchParameter Confirm
			{
				set
				{
					base.PowerSharpParameters["Confirm"] = value;
				}
			}
		}

		// Token: 0x02000B7E RID: 2942
		public class CustomVoicemailGreetingParameters : ParametersBase
		{
			// Token: 0x170062F7 RID: 25335
			// (set) Token: 0x06008E3C RID: 36412 RVA: 0x000D04C6 File Offset: 0x000CE6C6
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new MailboxIdParameter(value) : null);
				}
			}

			// Token: 0x170062F8 RID: 25336
			// (set) Token: 0x06008E3D RID: 36413 RVA: 0x000D04E4 File Offset: 0x000CE6E4
			public virtual SwitchParameter CustomVoicemailGreeting
			{
				set
				{
					base.PowerSharpParameters["CustomVoicemailGreeting"] = value;
				}
			}

			// Token: 0x170062F9 RID: 25337
			// (set) Token: 0x06008E3E RID: 36414 RVA: 0x000D04FC File Offset: 0x000CE6FC
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x170062FA RID: 25338
			// (set) Token: 0x06008E3F RID: 36415 RVA: 0x000D050F File Offset: 0x000CE70F
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x170062FB RID: 25339
			// (set) Token: 0x06008E40 RID: 36416 RVA: 0x000D0527 File Offset: 0x000CE727
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x170062FC RID: 25340
			// (set) Token: 0x06008E41 RID: 36417 RVA: 0x000D053F File Offset: 0x000CE73F
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x170062FD RID: 25341
			// (set) Token: 0x06008E42 RID: 36418 RVA: 0x000D0557 File Offset: 0x000CE757
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x170062FE RID: 25342
			// (set) Token: 0x06008E43 RID: 36419 RVA: 0x000D056F File Offset: 0x000CE76F
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}

			// Token: 0x170062FF RID: 25343
			// (set) Token: 0x06008E44 RID: 36420 RVA: 0x000D0587 File Offset: 0x000CE787
			public virtual SwitchParameter Confirm
			{
				set
				{
					base.PowerSharpParameters["Confirm"] = value;
				}
			}
		}

		// Token: 0x02000B7F RID: 2943
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x17006300 RID: 25344
			// (set) Token: 0x06008E46 RID: 36422 RVA: 0x000D05A7 File Offset: 0x000CE7A7
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17006301 RID: 25345
			// (set) Token: 0x06008E47 RID: 36423 RVA: 0x000D05BA File Offset: 0x000CE7BA
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17006302 RID: 25346
			// (set) Token: 0x06008E48 RID: 36424 RVA: 0x000D05D2 File Offset: 0x000CE7D2
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17006303 RID: 25347
			// (set) Token: 0x06008E49 RID: 36425 RVA: 0x000D05EA File Offset: 0x000CE7EA
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17006304 RID: 25348
			// (set) Token: 0x06008E4A RID: 36426 RVA: 0x000D0602 File Offset: 0x000CE802
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17006305 RID: 25349
			// (set) Token: 0x06008E4B RID: 36427 RVA: 0x000D061A File Offset: 0x000CE81A
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}

			// Token: 0x17006306 RID: 25350
			// (set) Token: 0x06008E4C RID: 36428 RVA: 0x000D0632 File Offset: 0x000CE832
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
