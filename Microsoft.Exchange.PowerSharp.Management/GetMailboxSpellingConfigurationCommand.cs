using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage.Management;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x020004A1 RID: 1185
	public class GetMailboxSpellingConfigurationCommand : SyntheticCommandWithPipelineInput<MailboxSpellingConfiguration, MailboxSpellingConfiguration>
	{
		// Token: 0x06004281 RID: 17025 RVA: 0x0006E092 File Offset: 0x0006C292
		private GetMailboxSpellingConfigurationCommand() : base("Get-MailboxSpellingConfiguration")
		{
		}

		// Token: 0x06004282 RID: 17026 RVA: 0x0006E09F File Offset: 0x0006C29F
		public GetMailboxSpellingConfigurationCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x06004283 RID: 17027 RVA: 0x0006E0AE File Offset: 0x0006C2AE
		public virtual GetMailboxSpellingConfigurationCommand SetParameters(GetMailboxSpellingConfigurationCommand.IdentityParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x06004284 RID: 17028 RVA: 0x0006E0B8 File Offset: 0x0006C2B8
		public virtual GetMailboxSpellingConfigurationCommand SetParameters(GetMailboxSpellingConfigurationCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x020004A2 RID: 1186
		public class IdentityParameters : ParametersBase
		{
			// Token: 0x170024F8 RID: 9464
			// (set) Token: 0x06004285 RID: 17029 RVA: 0x0006E0C2 File Offset: 0x0006C2C2
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new MailboxIdParameter(value) : null);
				}
			}

			// Token: 0x170024F9 RID: 9465
			// (set) Token: 0x06004286 RID: 17030 RVA: 0x0006E0E0 File Offset: 0x0006C2E0
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x170024FA RID: 9466
			// (set) Token: 0x06004287 RID: 17031 RVA: 0x0006E0F3 File Offset: 0x0006C2F3
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x170024FB RID: 9467
			// (set) Token: 0x06004288 RID: 17032 RVA: 0x0006E10B File Offset: 0x0006C30B
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x170024FC RID: 9468
			// (set) Token: 0x06004289 RID: 17033 RVA: 0x0006E123 File Offset: 0x0006C323
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x170024FD RID: 9469
			// (set) Token: 0x0600428A RID: 17034 RVA: 0x0006E13B File Offset: 0x0006C33B
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}
		}

		// Token: 0x020004A3 RID: 1187
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x170024FE RID: 9470
			// (set) Token: 0x0600428C RID: 17036 RVA: 0x0006E15B File Offset: 0x0006C35B
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x170024FF RID: 9471
			// (set) Token: 0x0600428D RID: 17037 RVA: 0x0006E16E File Offset: 0x0006C36E
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17002500 RID: 9472
			// (set) Token: 0x0600428E RID: 17038 RVA: 0x0006E186 File Offset: 0x0006C386
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17002501 RID: 9473
			// (set) Token: 0x0600428F RID: 17039 RVA: 0x0006E19E File Offset: 0x0006C39E
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17002502 RID: 9474
			// (set) Token: 0x06004290 RID: 17040 RVA: 0x0006E1B6 File Offset: 0x0006C3B6
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
