using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x02000BC0 RID: 3008
	public class NewMailboxRepairRequestCommand : SyntheticCommandWithPipelineInput<StoreIntegrityCheckJob, StoreIntegrityCheckJob>
	{
		// Token: 0x06009150 RID: 37200 RVA: 0x000D450D File Offset: 0x000D270D
		private NewMailboxRepairRequestCommand() : base("New-MailboxRepairRequest")
		{
		}

		// Token: 0x06009151 RID: 37201 RVA: 0x000D451A File Offset: 0x000D271A
		public NewMailboxRepairRequestCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x06009152 RID: 37202 RVA: 0x000D4529 File Offset: 0x000D2729
		public virtual NewMailboxRepairRequestCommand SetParameters(NewMailboxRepairRequestCommand.DatabaseParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x06009153 RID: 37203 RVA: 0x000D4533 File Offset: 0x000D2733
		public virtual NewMailboxRepairRequestCommand SetParameters(NewMailboxRepairRequestCommand.MailboxParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x06009154 RID: 37204 RVA: 0x000D453D File Offset: 0x000D273D
		public virtual NewMailboxRepairRequestCommand SetParameters(NewMailboxRepairRequestCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x02000BC1 RID: 3009
		public class DatabaseParameters : ParametersBase
		{
			// Token: 0x17006589 RID: 25993
			// (set) Token: 0x06009155 RID: 37205 RVA: 0x000D4547 File Offset: 0x000D2747
			public virtual DatabaseIdParameter Database
			{
				set
				{
					base.PowerSharpParameters["Database"] = value;
				}
			}

			// Token: 0x1700658A RID: 25994
			// (set) Token: 0x06009156 RID: 37206 RVA: 0x000D455A File Offset: 0x000D275A
			public virtual StoreMailboxIdParameter StoreMailbox
			{
				set
				{
					base.PowerSharpParameters["StoreMailbox"] = value;
				}
			}

			// Token: 0x1700658B RID: 25995
			// (set) Token: 0x06009157 RID: 37207 RVA: 0x000D456D File Offset: 0x000D276D
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x1700658C RID: 25996
			// (set) Token: 0x06009158 RID: 37208 RVA: 0x000D4580 File Offset: 0x000D2780
			public virtual SwitchParameter DetectOnly
			{
				set
				{
					base.PowerSharpParameters["DetectOnly"] = value;
				}
			}

			// Token: 0x1700658D RID: 25997
			// (set) Token: 0x06009159 RID: 37209 RVA: 0x000D4598 File Offset: 0x000D2798
			public virtual SwitchParameter Force
			{
				set
				{
					base.PowerSharpParameters["Force"] = value;
				}
			}

			// Token: 0x1700658E RID: 25998
			// (set) Token: 0x0600915A RID: 37210 RVA: 0x000D45B0 File Offset: 0x000D27B0
			public virtual MailboxCorruptionType CorruptionType
			{
				set
				{
					base.PowerSharpParameters["CorruptionType"] = value;
				}
			}

			// Token: 0x1700658F RID: 25999
			// (set) Token: 0x0600915B RID: 37211 RVA: 0x000D45C8 File Offset: 0x000D27C8
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17006590 RID: 26000
			// (set) Token: 0x0600915C RID: 37212 RVA: 0x000D45E0 File Offset: 0x000D27E0
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17006591 RID: 26001
			// (set) Token: 0x0600915D RID: 37213 RVA: 0x000D45F8 File Offset: 0x000D27F8
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17006592 RID: 26002
			// (set) Token: 0x0600915E RID: 37214 RVA: 0x000D4610 File Offset: 0x000D2810
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17006593 RID: 26003
			// (set) Token: 0x0600915F RID: 37215 RVA: 0x000D4628 File Offset: 0x000D2828
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}

		// Token: 0x02000BC2 RID: 3010
		public class MailboxParameters : ParametersBase
		{
			// Token: 0x17006594 RID: 26004
			// (set) Token: 0x06009161 RID: 37217 RVA: 0x000D4648 File Offset: 0x000D2848
			public virtual string Mailbox
			{
				set
				{
					base.PowerSharpParameters["Mailbox"] = ((value != null) ? new MailboxIdParameter(value) : null);
				}
			}

			// Token: 0x17006595 RID: 26005
			// (set) Token: 0x06009162 RID: 37218 RVA: 0x000D4666 File Offset: 0x000D2866
			public virtual SwitchParameter Archive
			{
				set
				{
					base.PowerSharpParameters["Archive"] = value;
				}
			}

			// Token: 0x17006596 RID: 26006
			// (set) Token: 0x06009163 RID: 37219 RVA: 0x000D467E File Offset: 0x000D287E
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17006597 RID: 26007
			// (set) Token: 0x06009164 RID: 37220 RVA: 0x000D4691 File Offset: 0x000D2891
			public virtual SwitchParameter DetectOnly
			{
				set
				{
					base.PowerSharpParameters["DetectOnly"] = value;
				}
			}

			// Token: 0x17006598 RID: 26008
			// (set) Token: 0x06009165 RID: 37221 RVA: 0x000D46A9 File Offset: 0x000D28A9
			public virtual SwitchParameter Force
			{
				set
				{
					base.PowerSharpParameters["Force"] = value;
				}
			}

			// Token: 0x17006599 RID: 26009
			// (set) Token: 0x06009166 RID: 37222 RVA: 0x000D46C1 File Offset: 0x000D28C1
			public virtual MailboxCorruptionType CorruptionType
			{
				set
				{
					base.PowerSharpParameters["CorruptionType"] = value;
				}
			}

			// Token: 0x1700659A RID: 26010
			// (set) Token: 0x06009167 RID: 37223 RVA: 0x000D46D9 File Offset: 0x000D28D9
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x1700659B RID: 26011
			// (set) Token: 0x06009168 RID: 37224 RVA: 0x000D46F1 File Offset: 0x000D28F1
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x1700659C RID: 26012
			// (set) Token: 0x06009169 RID: 37225 RVA: 0x000D4709 File Offset: 0x000D2909
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x1700659D RID: 26013
			// (set) Token: 0x0600916A RID: 37226 RVA: 0x000D4721 File Offset: 0x000D2921
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x1700659E RID: 26014
			// (set) Token: 0x0600916B RID: 37227 RVA: 0x000D4739 File Offset: 0x000D2939
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}

		// Token: 0x02000BC3 RID: 3011
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x1700659F RID: 26015
			// (set) Token: 0x0600916D RID: 37229 RVA: 0x000D4759 File Offset: 0x000D2959
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x170065A0 RID: 26016
			// (set) Token: 0x0600916E RID: 37230 RVA: 0x000D476C File Offset: 0x000D296C
			public virtual SwitchParameter DetectOnly
			{
				set
				{
					base.PowerSharpParameters["DetectOnly"] = value;
				}
			}

			// Token: 0x170065A1 RID: 26017
			// (set) Token: 0x0600916F RID: 37231 RVA: 0x000D4784 File Offset: 0x000D2984
			public virtual SwitchParameter Force
			{
				set
				{
					base.PowerSharpParameters["Force"] = value;
				}
			}

			// Token: 0x170065A2 RID: 26018
			// (set) Token: 0x06009170 RID: 37232 RVA: 0x000D479C File Offset: 0x000D299C
			public virtual MailboxCorruptionType CorruptionType
			{
				set
				{
					base.PowerSharpParameters["CorruptionType"] = value;
				}
			}

			// Token: 0x170065A3 RID: 26019
			// (set) Token: 0x06009171 RID: 37233 RVA: 0x000D47B4 File Offset: 0x000D29B4
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x170065A4 RID: 26020
			// (set) Token: 0x06009172 RID: 37234 RVA: 0x000D47CC File Offset: 0x000D29CC
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x170065A5 RID: 26021
			// (set) Token: 0x06009173 RID: 37235 RVA: 0x000D47E4 File Offset: 0x000D29E4
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x170065A6 RID: 26022
			// (set) Token: 0x06009174 RID: 37236 RVA: 0x000D47FC File Offset: 0x000D29FC
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x170065A7 RID: 26023
			// (set) Token: 0x06009175 RID: 37237 RVA: 0x000D4814 File Offset: 0x000D2A14
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
