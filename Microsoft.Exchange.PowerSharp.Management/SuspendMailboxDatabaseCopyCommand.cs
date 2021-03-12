using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x02000582 RID: 1410
	public class SuspendMailboxDatabaseCopyCommand : SyntheticCommandWithPipelineInputNoOutput<DatabaseCopyIdParameter>
	{
		// Token: 0x06004A15 RID: 18965 RVA: 0x00077779 File Offset: 0x00075979
		private SuspendMailboxDatabaseCopyCommand() : base("Suspend-MailboxDatabaseCopy")
		{
		}

		// Token: 0x06004A16 RID: 18966 RVA: 0x00077786 File Offset: 0x00075986
		public SuspendMailboxDatabaseCopyCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x06004A17 RID: 18967 RVA: 0x00077795 File Offset: 0x00075995
		public virtual SuspendMailboxDatabaseCopyCommand SetParameters(SuspendMailboxDatabaseCopyCommand.EnableReplayLagParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x06004A18 RID: 18968 RVA: 0x0007779F File Offset: 0x0007599F
		public virtual SuspendMailboxDatabaseCopyCommand SetParameters(SuspendMailboxDatabaseCopyCommand.IdentityParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x06004A19 RID: 18969 RVA: 0x000777A9 File Offset: 0x000759A9
		public virtual SuspendMailboxDatabaseCopyCommand SetParameters(SuspendMailboxDatabaseCopyCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x02000583 RID: 1411
		public class EnableReplayLagParameters : ParametersBase
		{
			// Token: 0x17002ACA RID: 10954
			// (set) Token: 0x06004A1A RID: 18970 RVA: 0x000777B3 File Offset: 0x000759B3
			public virtual DatabaseCopyIdParameter Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = value;
				}
			}

			// Token: 0x17002ACB RID: 10955
			// (set) Token: 0x06004A1B RID: 18971 RVA: 0x000777C6 File Offset: 0x000759C6
			public virtual SwitchParameter EnableReplayLag
			{
				set
				{
					base.PowerSharpParameters["EnableReplayLag"] = value;
				}
			}

			// Token: 0x17002ACC RID: 10956
			// (set) Token: 0x06004A1C RID: 18972 RVA: 0x000777DE File Offset: 0x000759DE
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17002ACD RID: 10957
			// (set) Token: 0x06004A1D RID: 18973 RVA: 0x000777F1 File Offset: 0x000759F1
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17002ACE RID: 10958
			// (set) Token: 0x06004A1E RID: 18974 RVA: 0x00077809 File Offset: 0x00075A09
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17002ACF RID: 10959
			// (set) Token: 0x06004A1F RID: 18975 RVA: 0x00077821 File Offset: 0x00075A21
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17002AD0 RID: 10960
			// (set) Token: 0x06004A20 RID: 18976 RVA: 0x00077839 File Offset: 0x00075A39
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17002AD1 RID: 10961
			// (set) Token: 0x06004A21 RID: 18977 RVA: 0x00077851 File Offset: 0x00075A51
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}

		// Token: 0x02000584 RID: 1412
		public class IdentityParameters : ParametersBase
		{
			// Token: 0x17002AD2 RID: 10962
			// (set) Token: 0x06004A23 RID: 18979 RVA: 0x00077871 File Offset: 0x00075A71
			public virtual DatabaseCopyIdParameter Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = value;
				}
			}

			// Token: 0x17002AD3 RID: 10963
			// (set) Token: 0x06004A24 RID: 18980 RVA: 0x00077884 File Offset: 0x00075A84
			public virtual string SuspendComment
			{
				set
				{
					base.PowerSharpParameters["SuspendComment"] = value;
				}
			}

			// Token: 0x17002AD4 RID: 10964
			// (set) Token: 0x06004A25 RID: 18981 RVA: 0x00077897 File Offset: 0x00075A97
			public virtual SwitchParameter ActivationOnly
			{
				set
				{
					base.PowerSharpParameters["ActivationOnly"] = value;
				}
			}

			// Token: 0x17002AD5 RID: 10965
			// (set) Token: 0x06004A26 RID: 18982 RVA: 0x000778AF File Offset: 0x00075AAF
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17002AD6 RID: 10966
			// (set) Token: 0x06004A27 RID: 18983 RVA: 0x000778C2 File Offset: 0x00075AC2
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17002AD7 RID: 10967
			// (set) Token: 0x06004A28 RID: 18984 RVA: 0x000778DA File Offset: 0x00075ADA
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17002AD8 RID: 10968
			// (set) Token: 0x06004A29 RID: 18985 RVA: 0x000778F2 File Offset: 0x00075AF2
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17002AD9 RID: 10969
			// (set) Token: 0x06004A2A RID: 18986 RVA: 0x0007790A File Offset: 0x00075B0A
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17002ADA RID: 10970
			// (set) Token: 0x06004A2B RID: 18987 RVA: 0x00077922 File Offset: 0x00075B22
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}

		// Token: 0x02000585 RID: 1413
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x17002ADB RID: 10971
			// (set) Token: 0x06004A2D RID: 18989 RVA: 0x00077942 File Offset: 0x00075B42
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17002ADC RID: 10972
			// (set) Token: 0x06004A2E RID: 18990 RVA: 0x00077955 File Offset: 0x00075B55
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17002ADD RID: 10973
			// (set) Token: 0x06004A2F RID: 18991 RVA: 0x0007796D File Offset: 0x00075B6D
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17002ADE RID: 10974
			// (set) Token: 0x06004A30 RID: 18992 RVA: 0x00077985 File Offset: 0x00075B85
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17002ADF RID: 10975
			// (set) Token: 0x06004A31 RID: 18993 RVA: 0x0007799D File Offset: 0x00075B9D
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17002AE0 RID: 10976
			// (set) Token: 0x06004A32 RID: 18994 RVA: 0x000779B5 File Offset: 0x00075BB5
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
