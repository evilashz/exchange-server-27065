using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Management.RecipientTasks;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x02000C3F RID: 3135
	public class RemoveGroupMailboxCommand : SyntheticCommandWithPipelineInput<MailboxIdParameter, MailboxIdParameter>
	{
		// Token: 0x06009933 RID: 39219 RVA: 0x000DE971 File Offset: 0x000DCB71
		private RemoveGroupMailboxCommand() : base("Remove-GroupMailbox")
		{
		}

		// Token: 0x06009934 RID: 39220 RVA: 0x000DE97E File Offset: 0x000DCB7E
		public RemoveGroupMailboxCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x06009935 RID: 39221 RVA: 0x000DE98D File Offset: 0x000DCB8D
		public virtual RemoveGroupMailboxCommand SetParameters(RemoveGroupMailboxCommand.IdentityParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x06009936 RID: 39222 RVA: 0x000DE997 File Offset: 0x000DCB97
		public virtual RemoveGroupMailboxCommand SetParameters(RemoveGroupMailboxCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x02000C40 RID: 3136
		public class IdentityParameters : ParametersBase
		{
			// Token: 0x17006C6E RID: 27758
			// (set) Token: 0x06009937 RID: 39223 RVA: 0x000DE9A1 File Offset: 0x000DCBA1
			public virtual string ExecutingUser
			{
				set
				{
					base.PowerSharpParameters["ExecutingUser"] = ((value != null) ? new RecipientIdParameter(value) : null);
				}
			}

			// Token: 0x17006C6F RID: 27759
			// (set) Token: 0x06009938 RID: 39224 RVA: 0x000DE9BF File Offset: 0x000DCBBF
			public virtual RecipientIdType RecipientIdType
			{
				set
				{
					base.PowerSharpParameters["RecipientIdType"] = value;
				}
			}

			// Token: 0x17006C70 RID: 27760
			// (set) Token: 0x06009939 RID: 39225 RVA: 0x000DE9D7 File Offset: 0x000DCBD7
			public virtual SwitchParameter FromSyncClient
			{
				set
				{
					base.PowerSharpParameters["FromSyncClient"] = value;
				}
			}

			// Token: 0x17006C71 RID: 27761
			// (set) Token: 0x0600993A RID: 39226 RVA: 0x000DE9EF File Offset: 0x000DCBEF
			public virtual bool Permanent
			{
				set
				{
					base.PowerSharpParameters["Permanent"] = value;
				}
			}

			// Token: 0x17006C72 RID: 27762
			// (set) Token: 0x0600993B RID: 39227 RVA: 0x000DEA07 File Offset: 0x000DCC07
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new MailboxIdParameter(value) : null);
				}
			}

			// Token: 0x17006C73 RID: 27763
			// (set) Token: 0x0600993C RID: 39228 RVA: 0x000DEA25 File Offset: 0x000DCC25
			public virtual SwitchParameter ForReconciliation
			{
				set
				{
					base.PowerSharpParameters["ForReconciliation"] = value;
				}
			}

			// Token: 0x17006C74 RID: 27764
			// (set) Token: 0x0600993D RID: 39229 RVA: 0x000DEA3D File Offset: 0x000DCC3D
			public virtual SwitchParameter RemoveArbitrationMailboxWithOABsAllowed
			{
				set
				{
					base.PowerSharpParameters["RemoveArbitrationMailboxWithOABsAllowed"] = value;
				}
			}

			// Token: 0x17006C75 RID: 27765
			// (set) Token: 0x0600993E RID: 39230 RVA: 0x000DEA55 File Offset: 0x000DCC55
			public virtual SwitchParameter Force
			{
				set
				{
					base.PowerSharpParameters["Force"] = value;
				}
			}

			// Token: 0x17006C76 RID: 27766
			// (set) Token: 0x0600993F RID: 39231 RVA: 0x000DEA6D File Offset: 0x000DCC6D
			public virtual SwitchParameter AuditLog
			{
				set
				{
					base.PowerSharpParameters["AuditLog"] = value;
				}
			}

			// Token: 0x17006C77 RID: 27767
			// (set) Token: 0x06009940 RID: 39232 RVA: 0x000DEA85 File Offset: 0x000DCC85
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17006C78 RID: 27768
			// (set) Token: 0x06009941 RID: 39233 RVA: 0x000DEA98 File Offset: 0x000DCC98
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17006C79 RID: 27769
			// (set) Token: 0x06009942 RID: 39234 RVA: 0x000DEAB0 File Offset: 0x000DCCB0
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17006C7A RID: 27770
			// (set) Token: 0x06009943 RID: 39235 RVA: 0x000DEAC8 File Offset: 0x000DCCC8
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17006C7B RID: 27771
			// (set) Token: 0x06009944 RID: 39236 RVA: 0x000DEAE0 File Offset: 0x000DCCE0
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17006C7C RID: 27772
			// (set) Token: 0x06009945 RID: 39237 RVA: 0x000DEAF8 File Offset: 0x000DCCF8
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}

			// Token: 0x17006C7D RID: 27773
			// (set) Token: 0x06009946 RID: 39238 RVA: 0x000DEB10 File Offset: 0x000DCD10
			public virtual SwitchParameter Confirm
			{
				set
				{
					base.PowerSharpParameters["Confirm"] = value;
				}
			}
		}

		// Token: 0x02000C41 RID: 3137
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x17006C7E RID: 27774
			// (set) Token: 0x06009948 RID: 39240 RVA: 0x000DEB30 File Offset: 0x000DCD30
			public virtual SwitchParameter ForReconciliation
			{
				set
				{
					base.PowerSharpParameters["ForReconciliation"] = value;
				}
			}

			// Token: 0x17006C7F RID: 27775
			// (set) Token: 0x06009949 RID: 39241 RVA: 0x000DEB48 File Offset: 0x000DCD48
			public virtual SwitchParameter RemoveArbitrationMailboxWithOABsAllowed
			{
				set
				{
					base.PowerSharpParameters["RemoveArbitrationMailboxWithOABsAllowed"] = value;
				}
			}

			// Token: 0x17006C80 RID: 27776
			// (set) Token: 0x0600994A RID: 39242 RVA: 0x000DEB60 File Offset: 0x000DCD60
			public virtual SwitchParameter Force
			{
				set
				{
					base.PowerSharpParameters["Force"] = value;
				}
			}

			// Token: 0x17006C81 RID: 27777
			// (set) Token: 0x0600994B RID: 39243 RVA: 0x000DEB78 File Offset: 0x000DCD78
			public virtual SwitchParameter AuditLog
			{
				set
				{
					base.PowerSharpParameters["AuditLog"] = value;
				}
			}

			// Token: 0x17006C82 RID: 27778
			// (set) Token: 0x0600994C RID: 39244 RVA: 0x000DEB90 File Offset: 0x000DCD90
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17006C83 RID: 27779
			// (set) Token: 0x0600994D RID: 39245 RVA: 0x000DEBA3 File Offset: 0x000DCDA3
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17006C84 RID: 27780
			// (set) Token: 0x0600994E RID: 39246 RVA: 0x000DEBBB File Offset: 0x000DCDBB
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17006C85 RID: 27781
			// (set) Token: 0x0600994F RID: 39247 RVA: 0x000DEBD3 File Offset: 0x000DCDD3
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17006C86 RID: 27782
			// (set) Token: 0x06009950 RID: 39248 RVA: 0x000DEBEB File Offset: 0x000DCDEB
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17006C87 RID: 27783
			// (set) Token: 0x06009951 RID: 39249 RVA: 0x000DEC03 File Offset: 0x000DCE03
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}

			// Token: 0x17006C88 RID: 27784
			// (set) Token: 0x06009952 RID: 39250 RVA: 0x000DEC1B File Offset: 0x000DCE1B
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
