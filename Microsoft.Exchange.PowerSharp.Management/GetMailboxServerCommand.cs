using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x02000639 RID: 1593
	public class GetMailboxServerCommand : SyntheticCommandWithPipelineInput<Server, Server>
	{
		// Token: 0x060050C7 RID: 20679 RVA: 0x0007FE6E File Offset: 0x0007E06E
		private GetMailboxServerCommand() : base("Get-MailboxServer")
		{
		}

		// Token: 0x060050C8 RID: 20680 RVA: 0x0007FE7B File Offset: 0x0007E07B
		public GetMailboxServerCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x060050C9 RID: 20681 RVA: 0x0007FE8A File Offset: 0x0007E08A
		public virtual GetMailboxServerCommand SetParameters(GetMailboxServerCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x060050CA RID: 20682 RVA: 0x0007FE94 File Offset: 0x0007E094
		public virtual GetMailboxServerCommand SetParameters(GetMailboxServerCommand.IdentityParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x0200063A RID: 1594
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x1700300E RID: 12302
			// (set) Token: 0x060050CB RID: 20683 RVA: 0x0007FE9E File Offset: 0x0007E09E
			public virtual SwitchParameter Status
			{
				set
				{
					base.PowerSharpParameters["Status"] = value;
				}
			}

			// Token: 0x1700300F RID: 12303
			// (set) Token: 0x060050CC RID: 20684 RVA: 0x0007FEB6 File Offset: 0x0007E0B6
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17003010 RID: 12304
			// (set) Token: 0x060050CD RID: 20685 RVA: 0x0007FEC9 File Offset: 0x0007E0C9
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17003011 RID: 12305
			// (set) Token: 0x060050CE RID: 20686 RVA: 0x0007FEE1 File Offset: 0x0007E0E1
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17003012 RID: 12306
			// (set) Token: 0x060050CF RID: 20687 RVA: 0x0007FEF9 File Offset: 0x0007E0F9
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17003013 RID: 12307
			// (set) Token: 0x060050D0 RID: 20688 RVA: 0x0007FF11 File Offset: 0x0007E111
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}
		}

		// Token: 0x0200063B RID: 1595
		public class IdentityParameters : ParametersBase
		{
			// Token: 0x17003014 RID: 12308
			// (set) Token: 0x060050D2 RID: 20690 RVA: 0x0007FF31 File Offset: 0x0007E131
			public virtual MailboxServerIdParameter Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = value;
				}
			}

			// Token: 0x17003015 RID: 12309
			// (set) Token: 0x060050D3 RID: 20691 RVA: 0x0007FF44 File Offset: 0x0007E144
			public virtual SwitchParameter Status
			{
				set
				{
					base.PowerSharpParameters["Status"] = value;
				}
			}

			// Token: 0x17003016 RID: 12310
			// (set) Token: 0x060050D4 RID: 20692 RVA: 0x0007FF5C File Offset: 0x0007E15C
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17003017 RID: 12311
			// (set) Token: 0x060050D5 RID: 20693 RVA: 0x0007FF6F File Offset: 0x0007E16F
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17003018 RID: 12312
			// (set) Token: 0x060050D6 RID: 20694 RVA: 0x0007FF87 File Offset: 0x0007E187
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17003019 RID: 12313
			// (set) Token: 0x060050D7 RID: 20695 RVA: 0x0007FF9F File Offset: 0x0007E19F
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x1700301A RID: 12314
			// (set) Token: 0x060050D8 RID: 20696 RVA: 0x0007FFB7 File Offset: 0x0007E1B7
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
