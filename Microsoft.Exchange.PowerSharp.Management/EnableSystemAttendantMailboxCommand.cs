using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.Recipient;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x02000C71 RID: 3185
	public class EnableSystemAttendantMailboxCommand : SyntheticCommandWithPipelineInput<ADSystemAttendantMailbox, ADSystemAttendantMailbox>
	{
		// Token: 0x06009C77 RID: 40055 RVA: 0x000E2EDB File Offset: 0x000E10DB
		private EnableSystemAttendantMailboxCommand() : base("Enable-SystemAttendantMailbox")
		{
		}

		// Token: 0x06009C78 RID: 40056 RVA: 0x000E2EE8 File Offset: 0x000E10E8
		public EnableSystemAttendantMailboxCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x06009C79 RID: 40057 RVA: 0x000E2EF7 File Offset: 0x000E10F7
		public virtual EnableSystemAttendantMailboxCommand SetParameters(EnableSystemAttendantMailboxCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x06009C7A RID: 40058 RVA: 0x000E2F01 File Offset: 0x000E1101
		public virtual EnableSystemAttendantMailboxCommand SetParameters(EnableSystemAttendantMailboxCommand.IdentityParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x02000C72 RID: 3186
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x17006F4E RID: 28494
			// (set) Token: 0x06009C7B RID: 40059 RVA: 0x000E2F0B File Offset: 0x000E110B
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17006F4F RID: 28495
			// (set) Token: 0x06009C7C RID: 40060 RVA: 0x000E2F1E File Offset: 0x000E111E
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17006F50 RID: 28496
			// (set) Token: 0x06009C7D RID: 40061 RVA: 0x000E2F36 File Offset: 0x000E1136
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17006F51 RID: 28497
			// (set) Token: 0x06009C7E RID: 40062 RVA: 0x000E2F4E File Offset: 0x000E114E
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17006F52 RID: 28498
			// (set) Token: 0x06009C7F RID: 40063 RVA: 0x000E2F66 File Offset: 0x000E1166
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}
		}

		// Token: 0x02000C73 RID: 3187
		public class IdentityParameters : ParametersBase
		{
			// Token: 0x17006F53 RID: 28499
			// (set) Token: 0x06009C81 RID: 40065 RVA: 0x000E2F86 File Offset: 0x000E1186
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new SystemAttendantIdParameter(value) : null);
				}
			}

			// Token: 0x17006F54 RID: 28500
			// (set) Token: 0x06009C82 RID: 40066 RVA: 0x000E2FA4 File Offset: 0x000E11A4
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17006F55 RID: 28501
			// (set) Token: 0x06009C83 RID: 40067 RVA: 0x000E2FB7 File Offset: 0x000E11B7
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17006F56 RID: 28502
			// (set) Token: 0x06009C84 RID: 40068 RVA: 0x000E2FCF File Offset: 0x000E11CF
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17006F57 RID: 28503
			// (set) Token: 0x06009C85 RID: 40069 RVA: 0x000E2FE7 File Offset: 0x000E11E7
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17006F58 RID: 28504
			// (set) Token: 0x06009C86 RID: 40070 RVA: 0x000E2FFF File Offset: 0x000E11FF
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
