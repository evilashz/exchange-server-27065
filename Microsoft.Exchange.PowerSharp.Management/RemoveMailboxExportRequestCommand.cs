using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.MailboxReplicationService;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x02000A0C RID: 2572
	public class RemoveMailboxExportRequestCommand : SyntheticCommandWithPipelineInput<MailboxExportRequestIdParameter, MailboxExportRequestIdParameter>
	{
		// Token: 0x060080DD RID: 32989 RVA: 0x000BF172 File Offset: 0x000BD372
		private RemoveMailboxExportRequestCommand() : base("Remove-MailboxExportRequest")
		{
		}

		// Token: 0x060080DE RID: 32990 RVA: 0x000BF17F File Offset: 0x000BD37F
		public RemoveMailboxExportRequestCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x060080DF RID: 32991 RVA: 0x000BF18E File Offset: 0x000BD38E
		public virtual RemoveMailboxExportRequestCommand SetParameters(RemoveMailboxExportRequestCommand.IdentityParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x060080E0 RID: 32992 RVA: 0x000BF198 File Offset: 0x000BD398
		public virtual RemoveMailboxExportRequestCommand SetParameters(RemoveMailboxExportRequestCommand.MigrationRequestQueueParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x060080E1 RID: 32993 RVA: 0x000BF1A2 File Offset: 0x000BD3A2
		public virtual RemoveMailboxExportRequestCommand SetParameters(RemoveMailboxExportRequestCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x02000A0D RID: 2573
		public class IdentityParameters : ParametersBase
		{
			// Token: 0x1700587E RID: 22654
			// (set) Token: 0x060080E2 RID: 32994 RVA: 0x000BF1AC File Offset: 0x000BD3AC
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new MailboxExportRequestIdParameter(value) : null);
				}
			}

			// Token: 0x1700587F RID: 22655
			// (set) Token: 0x060080E3 RID: 32995 RVA: 0x000BF1CA File Offset: 0x000BD3CA
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17005880 RID: 22656
			// (set) Token: 0x060080E4 RID: 32996 RVA: 0x000BF1DD File Offset: 0x000BD3DD
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17005881 RID: 22657
			// (set) Token: 0x060080E5 RID: 32997 RVA: 0x000BF1F5 File Offset: 0x000BD3F5
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17005882 RID: 22658
			// (set) Token: 0x060080E6 RID: 32998 RVA: 0x000BF20D File Offset: 0x000BD40D
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17005883 RID: 22659
			// (set) Token: 0x060080E7 RID: 32999 RVA: 0x000BF225 File Offset: 0x000BD425
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17005884 RID: 22660
			// (set) Token: 0x060080E8 RID: 33000 RVA: 0x000BF23D File Offset: 0x000BD43D
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}

			// Token: 0x17005885 RID: 22661
			// (set) Token: 0x060080E9 RID: 33001 RVA: 0x000BF255 File Offset: 0x000BD455
			public virtual SwitchParameter Confirm
			{
				set
				{
					base.PowerSharpParameters["Confirm"] = value;
				}
			}
		}

		// Token: 0x02000A0E RID: 2574
		public class MigrationRequestQueueParameters : ParametersBase
		{
			// Token: 0x17005886 RID: 22662
			// (set) Token: 0x060080EB RID: 33003 RVA: 0x000BF275 File Offset: 0x000BD475
			public virtual DatabaseIdParameter RequestQueue
			{
				set
				{
					base.PowerSharpParameters["RequestQueue"] = value;
				}
			}

			// Token: 0x17005887 RID: 22663
			// (set) Token: 0x060080EC RID: 33004 RVA: 0x000BF288 File Offset: 0x000BD488
			public virtual Guid RequestGuid
			{
				set
				{
					base.PowerSharpParameters["RequestGuid"] = value;
				}
			}

			// Token: 0x17005888 RID: 22664
			// (set) Token: 0x060080ED RID: 33005 RVA: 0x000BF2A0 File Offset: 0x000BD4A0
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17005889 RID: 22665
			// (set) Token: 0x060080EE RID: 33006 RVA: 0x000BF2B3 File Offset: 0x000BD4B3
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x1700588A RID: 22666
			// (set) Token: 0x060080EF RID: 33007 RVA: 0x000BF2CB File Offset: 0x000BD4CB
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x1700588B RID: 22667
			// (set) Token: 0x060080F0 RID: 33008 RVA: 0x000BF2E3 File Offset: 0x000BD4E3
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x1700588C RID: 22668
			// (set) Token: 0x060080F1 RID: 33009 RVA: 0x000BF2FB File Offset: 0x000BD4FB
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x1700588D RID: 22669
			// (set) Token: 0x060080F2 RID: 33010 RVA: 0x000BF313 File Offset: 0x000BD513
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}

			// Token: 0x1700588E RID: 22670
			// (set) Token: 0x060080F3 RID: 33011 RVA: 0x000BF32B File Offset: 0x000BD52B
			public virtual SwitchParameter Confirm
			{
				set
				{
					base.PowerSharpParameters["Confirm"] = value;
				}
			}
		}

		// Token: 0x02000A0F RID: 2575
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x1700588F RID: 22671
			// (set) Token: 0x060080F5 RID: 33013 RVA: 0x000BF34B File Offset: 0x000BD54B
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17005890 RID: 22672
			// (set) Token: 0x060080F6 RID: 33014 RVA: 0x000BF35E File Offset: 0x000BD55E
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17005891 RID: 22673
			// (set) Token: 0x060080F7 RID: 33015 RVA: 0x000BF376 File Offset: 0x000BD576
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17005892 RID: 22674
			// (set) Token: 0x060080F8 RID: 33016 RVA: 0x000BF38E File Offset: 0x000BD58E
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17005893 RID: 22675
			// (set) Token: 0x060080F9 RID: 33017 RVA: 0x000BF3A6 File Offset: 0x000BD5A6
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17005894 RID: 22676
			// (set) Token: 0x060080FA RID: 33018 RVA: 0x000BF3BE File Offset: 0x000BD5BE
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}

			// Token: 0x17005895 RID: 22677
			// (set) Token: 0x060080FB RID: 33019 RVA: 0x000BF3D6 File Offset: 0x000BD5D6
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
