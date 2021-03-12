using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x02000561 RID: 1377
	public class RemoveDatabaseAvailabilityGroupServerCommand : SyntheticCommandWithPipelineInput<DatabaseAvailabilityGroup, DatabaseAvailabilityGroup>
	{
		// Token: 0x060048C0 RID: 18624 RVA: 0x00075C32 File Offset: 0x00073E32
		private RemoveDatabaseAvailabilityGroupServerCommand() : base("Remove-DatabaseAvailabilityGroupServer")
		{
		}

		// Token: 0x060048C1 RID: 18625 RVA: 0x00075C3F File Offset: 0x00073E3F
		public RemoveDatabaseAvailabilityGroupServerCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x060048C2 RID: 18626 RVA: 0x00075C4E File Offset: 0x00073E4E
		public virtual RemoveDatabaseAvailabilityGroupServerCommand SetParameters(RemoveDatabaseAvailabilityGroupServerCommand.IdentityParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x060048C3 RID: 18627 RVA: 0x00075C58 File Offset: 0x00073E58
		public virtual RemoveDatabaseAvailabilityGroupServerCommand SetParameters(RemoveDatabaseAvailabilityGroupServerCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x02000562 RID: 1378
		public class IdentityParameters : ParametersBase
		{
			// Token: 0x170029B7 RID: 10679
			// (set) Token: 0x060048C4 RID: 18628 RVA: 0x00075C62 File Offset: 0x00073E62
			public virtual ServerIdParameter MailboxServer
			{
				set
				{
					base.PowerSharpParameters["MailboxServer"] = value;
				}
			}

			// Token: 0x170029B8 RID: 10680
			// (set) Token: 0x060048C5 RID: 18629 RVA: 0x00075C75 File Offset: 0x00073E75
			public virtual DatabaseAvailabilityGroupIdParameter Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = value;
				}
			}

			// Token: 0x170029B9 RID: 10681
			// (set) Token: 0x060048C6 RID: 18630 RVA: 0x00075C88 File Offset: 0x00073E88
			public virtual SwitchParameter ConfigurationOnly
			{
				set
				{
					base.PowerSharpParameters["ConfigurationOnly"] = value;
				}
			}

			// Token: 0x170029BA RID: 10682
			// (set) Token: 0x060048C7 RID: 18631 RVA: 0x00075CA0 File Offset: 0x00073EA0
			public virtual SwitchParameter SkipDagValidation
			{
				set
				{
					base.PowerSharpParameters["SkipDagValidation"] = value;
				}
			}

			// Token: 0x170029BB RID: 10683
			// (set) Token: 0x060048C8 RID: 18632 RVA: 0x00075CB8 File Offset: 0x00073EB8
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x170029BC RID: 10684
			// (set) Token: 0x060048C9 RID: 18633 RVA: 0x00075CCB File Offset: 0x00073ECB
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x170029BD RID: 10685
			// (set) Token: 0x060048CA RID: 18634 RVA: 0x00075CE3 File Offset: 0x00073EE3
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x170029BE RID: 10686
			// (set) Token: 0x060048CB RID: 18635 RVA: 0x00075CFB File Offset: 0x00073EFB
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x170029BF RID: 10687
			// (set) Token: 0x060048CC RID: 18636 RVA: 0x00075D13 File Offset: 0x00073F13
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x170029C0 RID: 10688
			// (set) Token: 0x060048CD RID: 18637 RVA: 0x00075D2B File Offset: 0x00073F2B
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}

			// Token: 0x170029C1 RID: 10689
			// (set) Token: 0x060048CE RID: 18638 RVA: 0x00075D43 File Offset: 0x00073F43
			public virtual SwitchParameter Confirm
			{
				set
				{
					base.PowerSharpParameters["Confirm"] = value;
				}
			}
		}

		// Token: 0x02000563 RID: 1379
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x170029C2 RID: 10690
			// (set) Token: 0x060048D0 RID: 18640 RVA: 0x00075D63 File Offset: 0x00073F63
			public virtual SwitchParameter ConfigurationOnly
			{
				set
				{
					base.PowerSharpParameters["ConfigurationOnly"] = value;
				}
			}

			// Token: 0x170029C3 RID: 10691
			// (set) Token: 0x060048D1 RID: 18641 RVA: 0x00075D7B File Offset: 0x00073F7B
			public virtual SwitchParameter SkipDagValidation
			{
				set
				{
					base.PowerSharpParameters["SkipDagValidation"] = value;
				}
			}

			// Token: 0x170029C4 RID: 10692
			// (set) Token: 0x060048D2 RID: 18642 RVA: 0x00075D93 File Offset: 0x00073F93
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x170029C5 RID: 10693
			// (set) Token: 0x060048D3 RID: 18643 RVA: 0x00075DA6 File Offset: 0x00073FA6
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x170029C6 RID: 10694
			// (set) Token: 0x060048D4 RID: 18644 RVA: 0x00075DBE File Offset: 0x00073FBE
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x170029C7 RID: 10695
			// (set) Token: 0x060048D5 RID: 18645 RVA: 0x00075DD6 File Offset: 0x00073FD6
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x170029C8 RID: 10696
			// (set) Token: 0x060048D6 RID: 18646 RVA: 0x00075DEE File Offset: 0x00073FEE
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x170029C9 RID: 10697
			// (set) Token: 0x060048D7 RID: 18647 RVA: 0x00075E06 File Offset: 0x00074006
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}

			// Token: 0x170029CA RID: 10698
			// (set) Token: 0x060048D8 RID: 18648 RVA: 0x00075E1E File Offset: 0x0007401E
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
