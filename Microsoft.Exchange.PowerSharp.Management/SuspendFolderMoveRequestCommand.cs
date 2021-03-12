using System;
using System.Management.Automation;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.MailboxReplicationService;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x020009C9 RID: 2505
	public class SuspendFolderMoveRequestCommand : SyntheticCommandWithPipelineInput<FolderMoveRequestIdParameter, FolderMoveRequestIdParameter>
	{
		// Token: 0x06007D88 RID: 32136 RVA: 0x000BAB1B File Offset: 0x000B8D1B
		private SuspendFolderMoveRequestCommand() : base("Suspend-FolderMoveRequest")
		{
		}

		// Token: 0x06007D89 RID: 32137 RVA: 0x000BAB28 File Offset: 0x000B8D28
		public SuspendFolderMoveRequestCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x06007D8A RID: 32138 RVA: 0x000BAB37 File Offset: 0x000B8D37
		public virtual SuspendFolderMoveRequestCommand SetParameters(SuspendFolderMoveRequestCommand.IdentityParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x06007D8B RID: 32139 RVA: 0x000BAB41 File Offset: 0x000B8D41
		public virtual SuspendFolderMoveRequestCommand SetParameters(SuspendFolderMoveRequestCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x020009CA RID: 2506
		public class IdentityParameters : ParametersBase
		{
			// Token: 0x170055AF RID: 21935
			// (set) Token: 0x06007D8C RID: 32140 RVA: 0x000BAB4B File Offset: 0x000B8D4B
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new FolderMoveRequestIdParameter(value) : null);
				}
			}

			// Token: 0x170055B0 RID: 21936
			// (set) Token: 0x06007D8D RID: 32141 RVA: 0x000BAB69 File Offset: 0x000B8D69
			public virtual string SuspendComment
			{
				set
				{
					base.PowerSharpParameters["SuspendComment"] = value;
				}
			}

			// Token: 0x170055B1 RID: 21937
			// (set) Token: 0x06007D8E RID: 32142 RVA: 0x000BAB7C File Offset: 0x000B8D7C
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x170055B2 RID: 21938
			// (set) Token: 0x06007D8F RID: 32143 RVA: 0x000BAB8F File Offset: 0x000B8D8F
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x170055B3 RID: 21939
			// (set) Token: 0x06007D90 RID: 32144 RVA: 0x000BABA7 File Offset: 0x000B8DA7
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x170055B4 RID: 21940
			// (set) Token: 0x06007D91 RID: 32145 RVA: 0x000BABBF File Offset: 0x000B8DBF
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x170055B5 RID: 21941
			// (set) Token: 0x06007D92 RID: 32146 RVA: 0x000BABD7 File Offset: 0x000B8DD7
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x170055B6 RID: 21942
			// (set) Token: 0x06007D93 RID: 32147 RVA: 0x000BABEF File Offset: 0x000B8DEF
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}

			// Token: 0x170055B7 RID: 21943
			// (set) Token: 0x06007D94 RID: 32148 RVA: 0x000BAC07 File Offset: 0x000B8E07
			public virtual SwitchParameter Confirm
			{
				set
				{
					base.PowerSharpParameters["Confirm"] = value;
				}
			}
		}

		// Token: 0x020009CB RID: 2507
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x170055B8 RID: 21944
			// (set) Token: 0x06007D96 RID: 32150 RVA: 0x000BAC27 File Offset: 0x000B8E27
			public virtual string SuspendComment
			{
				set
				{
					base.PowerSharpParameters["SuspendComment"] = value;
				}
			}

			// Token: 0x170055B9 RID: 21945
			// (set) Token: 0x06007D97 RID: 32151 RVA: 0x000BAC3A File Offset: 0x000B8E3A
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x170055BA RID: 21946
			// (set) Token: 0x06007D98 RID: 32152 RVA: 0x000BAC4D File Offset: 0x000B8E4D
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x170055BB RID: 21947
			// (set) Token: 0x06007D99 RID: 32153 RVA: 0x000BAC65 File Offset: 0x000B8E65
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x170055BC RID: 21948
			// (set) Token: 0x06007D9A RID: 32154 RVA: 0x000BAC7D File Offset: 0x000B8E7D
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x170055BD RID: 21949
			// (set) Token: 0x06007D9B RID: 32155 RVA: 0x000BAC95 File Offset: 0x000B8E95
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x170055BE RID: 21950
			// (set) Token: 0x06007D9C RID: 32156 RVA: 0x000BACAD File Offset: 0x000B8EAD
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}

			// Token: 0x170055BF RID: 21951
			// (set) Token: 0x06007D9D RID: 32157 RVA: 0x000BACC5 File Offset: 0x000B8EC5
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
