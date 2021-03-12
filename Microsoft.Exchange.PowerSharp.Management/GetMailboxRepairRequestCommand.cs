using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x02000BBB RID: 3003
	public class GetMailboxRepairRequestCommand : SyntheticCommandWithPipelineInput<StoreIntegrityCheckJob, StoreIntegrityCheckJob>
	{
		// Token: 0x0600912C RID: 37164 RVA: 0x000D424B File Offset: 0x000D244B
		private GetMailboxRepairRequestCommand() : base("Get-MailboxRepairRequest")
		{
		}

		// Token: 0x0600912D RID: 37165 RVA: 0x000D4258 File Offset: 0x000D2458
		public GetMailboxRepairRequestCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x0600912E RID: 37166 RVA: 0x000D4267 File Offset: 0x000D2467
		public virtual GetMailboxRepairRequestCommand SetParameters(GetMailboxRepairRequestCommand.IdentityParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x0600912F RID: 37167 RVA: 0x000D4271 File Offset: 0x000D2471
		public virtual GetMailboxRepairRequestCommand SetParameters(GetMailboxRepairRequestCommand.DatabaseParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x06009130 RID: 37168 RVA: 0x000D427B File Offset: 0x000D247B
		public virtual GetMailboxRepairRequestCommand SetParameters(GetMailboxRepairRequestCommand.MailboxParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x06009131 RID: 37169 RVA: 0x000D4285 File Offset: 0x000D2485
		public virtual GetMailboxRepairRequestCommand SetParameters(GetMailboxRepairRequestCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x02000BBC RID: 3004
		public class IdentityParameters : ParametersBase
		{
			// Token: 0x1700656F RID: 25967
			// (set) Token: 0x06009132 RID: 37170 RVA: 0x000D428F File Offset: 0x000D248F
			public virtual SwitchParameter Detailed
			{
				set
				{
					base.PowerSharpParameters["Detailed"] = value;
				}
			}

			// Token: 0x17006570 RID: 25968
			// (set) Token: 0x06009133 RID: 37171 RVA: 0x000D42A7 File Offset: 0x000D24A7
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new StoreIntegrityCheckJobIdParameter(value) : null);
				}
			}

			// Token: 0x17006571 RID: 25969
			// (set) Token: 0x06009134 RID: 37172 RVA: 0x000D42C5 File Offset: 0x000D24C5
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17006572 RID: 25970
			// (set) Token: 0x06009135 RID: 37173 RVA: 0x000D42D8 File Offset: 0x000D24D8
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17006573 RID: 25971
			// (set) Token: 0x06009136 RID: 37174 RVA: 0x000D42F0 File Offset: 0x000D24F0
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17006574 RID: 25972
			// (set) Token: 0x06009137 RID: 37175 RVA: 0x000D4308 File Offset: 0x000D2508
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17006575 RID: 25973
			// (set) Token: 0x06009138 RID: 37176 RVA: 0x000D4320 File Offset: 0x000D2520
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}
		}

		// Token: 0x02000BBD RID: 3005
		public class DatabaseParameters : ParametersBase
		{
			// Token: 0x17006576 RID: 25974
			// (set) Token: 0x0600913A RID: 37178 RVA: 0x000D4340 File Offset: 0x000D2540
			public virtual DatabaseIdParameter Database
			{
				set
				{
					base.PowerSharpParameters["Database"] = value;
				}
			}

			// Token: 0x17006577 RID: 25975
			// (set) Token: 0x0600913B RID: 37179 RVA: 0x000D4353 File Offset: 0x000D2553
			public virtual StoreMailboxIdParameter StoreMailbox
			{
				set
				{
					base.PowerSharpParameters["StoreMailbox"] = value;
				}
			}

			// Token: 0x17006578 RID: 25976
			// (set) Token: 0x0600913C RID: 37180 RVA: 0x000D4366 File Offset: 0x000D2566
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17006579 RID: 25977
			// (set) Token: 0x0600913D RID: 37181 RVA: 0x000D4379 File Offset: 0x000D2579
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x1700657A RID: 25978
			// (set) Token: 0x0600913E RID: 37182 RVA: 0x000D4391 File Offset: 0x000D2591
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x1700657B RID: 25979
			// (set) Token: 0x0600913F RID: 37183 RVA: 0x000D43A9 File Offset: 0x000D25A9
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x1700657C RID: 25980
			// (set) Token: 0x06009140 RID: 37184 RVA: 0x000D43C1 File Offset: 0x000D25C1
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}
		}

		// Token: 0x02000BBE RID: 3006
		public class MailboxParameters : ParametersBase
		{
			// Token: 0x1700657D RID: 25981
			// (set) Token: 0x06009142 RID: 37186 RVA: 0x000D43E1 File Offset: 0x000D25E1
			public virtual string Mailbox
			{
				set
				{
					base.PowerSharpParameters["Mailbox"] = ((value != null) ? new MailboxIdParameter(value) : null);
				}
			}

			// Token: 0x1700657E RID: 25982
			// (set) Token: 0x06009143 RID: 37187 RVA: 0x000D43FF File Offset: 0x000D25FF
			public virtual SwitchParameter Archive
			{
				set
				{
					base.PowerSharpParameters["Archive"] = value;
				}
			}

			// Token: 0x1700657F RID: 25983
			// (set) Token: 0x06009144 RID: 37188 RVA: 0x000D4417 File Offset: 0x000D2617
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17006580 RID: 25984
			// (set) Token: 0x06009145 RID: 37189 RVA: 0x000D442A File Offset: 0x000D262A
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17006581 RID: 25985
			// (set) Token: 0x06009146 RID: 37190 RVA: 0x000D4442 File Offset: 0x000D2642
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17006582 RID: 25986
			// (set) Token: 0x06009147 RID: 37191 RVA: 0x000D445A File Offset: 0x000D265A
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17006583 RID: 25987
			// (set) Token: 0x06009148 RID: 37192 RVA: 0x000D4472 File Offset: 0x000D2672
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}
		}

		// Token: 0x02000BBF RID: 3007
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x17006584 RID: 25988
			// (set) Token: 0x0600914A RID: 37194 RVA: 0x000D4492 File Offset: 0x000D2692
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17006585 RID: 25989
			// (set) Token: 0x0600914B RID: 37195 RVA: 0x000D44A5 File Offset: 0x000D26A5
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17006586 RID: 25990
			// (set) Token: 0x0600914C RID: 37196 RVA: 0x000D44BD File Offset: 0x000D26BD
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17006587 RID: 25991
			// (set) Token: 0x0600914D RID: 37197 RVA: 0x000D44D5 File Offset: 0x000D26D5
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17006588 RID: 25992
			// (set) Token: 0x0600914E RID: 37198 RVA: 0x000D44ED File Offset: 0x000D26ED
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
