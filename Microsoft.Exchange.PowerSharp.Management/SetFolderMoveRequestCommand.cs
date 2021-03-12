using System;
using System.Management.Automation;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.MailboxReplicationService;
using Microsoft.Exchange.Management.RecipientTasks;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x020009C6 RID: 2502
	public class SetFolderMoveRequestCommand : SyntheticCommandWithPipelineInputNoOutput<FolderMoveRequestIdParameter>
	{
		// Token: 0x06007D68 RID: 32104 RVA: 0x000BA869 File Offset: 0x000B8A69
		private SetFolderMoveRequestCommand() : base("Set-FolderMoveRequest")
		{
		}

		// Token: 0x06007D69 RID: 32105 RVA: 0x000BA876 File Offset: 0x000B8A76
		public SetFolderMoveRequestCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x06007D6A RID: 32106 RVA: 0x000BA885 File Offset: 0x000B8A85
		public virtual SetFolderMoveRequestCommand SetParameters(SetFolderMoveRequestCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x06007D6B RID: 32107 RVA: 0x000BA88F File Offset: 0x000B8A8F
		public virtual SetFolderMoveRequestCommand SetParameters(SetFolderMoveRequestCommand.IdentityParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x020009C7 RID: 2503
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x17005595 RID: 21909
			// (set) Token: 0x06007D6C RID: 32108 RVA: 0x000BA899 File Offset: 0x000B8A99
			public virtual bool SuspendWhenReadyToComplete
			{
				set
				{
					base.PowerSharpParameters["SuspendWhenReadyToComplete"] = value;
				}
			}

			// Token: 0x17005596 RID: 21910
			// (set) Token: 0x06007D6D RID: 32109 RVA: 0x000BA8B1 File Offset: 0x000B8AB1
			public virtual DateTime? CompleteAfter
			{
				set
				{
					base.PowerSharpParameters["CompleteAfter"] = value;
				}
			}

			// Token: 0x17005597 RID: 21911
			// (set) Token: 0x06007D6E RID: 32110 RVA: 0x000BA8C9 File Offset: 0x000B8AC9
			public virtual TimeSpan IncrementalSyncInterval
			{
				set
				{
					base.PowerSharpParameters["IncrementalSyncInterval"] = value;
				}
			}

			// Token: 0x17005598 RID: 21912
			// (set) Token: 0x06007D6F RID: 32111 RVA: 0x000BA8E1 File Offset: 0x000B8AE1
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new FolderMoveRequestIdParameter(value) : null);
				}
			}

			// Token: 0x17005599 RID: 21913
			// (set) Token: 0x06007D70 RID: 32112 RVA: 0x000BA8FF File Offset: 0x000B8AFF
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x1700559A RID: 21914
			// (set) Token: 0x06007D71 RID: 32113 RVA: 0x000BA912 File Offset: 0x000B8B12
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x1700559B RID: 21915
			// (set) Token: 0x06007D72 RID: 32114 RVA: 0x000BA92A File Offset: 0x000B8B2A
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x1700559C RID: 21916
			// (set) Token: 0x06007D73 RID: 32115 RVA: 0x000BA942 File Offset: 0x000B8B42
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x1700559D RID: 21917
			// (set) Token: 0x06007D74 RID: 32116 RVA: 0x000BA95A File Offset: 0x000B8B5A
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x1700559E RID: 21918
			// (set) Token: 0x06007D75 RID: 32117 RVA: 0x000BA972 File Offset: 0x000B8B72
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}

		// Token: 0x020009C8 RID: 2504
		public class IdentityParameters : ParametersBase
		{
			// Token: 0x1700559F RID: 21919
			// (set) Token: 0x06007D77 RID: 32119 RVA: 0x000BA992 File Offset: 0x000B8B92
			public virtual Unlimited<int> BadItemLimit
			{
				set
				{
					base.PowerSharpParameters["BadItemLimit"] = value;
				}
			}

			// Token: 0x170055A0 RID: 21920
			// (set) Token: 0x06007D78 RID: 32120 RVA: 0x000BA9AA File Offset: 0x000B8BAA
			public virtual Unlimited<int> LargeItemLimit
			{
				set
				{
					base.PowerSharpParameters["LargeItemLimit"] = value;
				}
			}

			// Token: 0x170055A1 RID: 21921
			// (set) Token: 0x06007D79 RID: 32121 RVA: 0x000BA9C2 File Offset: 0x000B8BC2
			public virtual SwitchParameter AcceptLargeDataLoss
			{
				set
				{
					base.PowerSharpParameters["AcceptLargeDataLoss"] = value;
				}
			}

			// Token: 0x170055A2 RID: 21922
			// (set) Token: 0x06007D7A RID: 32122 RVA: 0x000BA9DA File Offset: 0x000B8BDA
			public virtual RequestPriority Priority
			{
				set
				{
					base.PowerSharpParameters["Priority"] = value;
				}
			}

			// Token: 0x170055A3 RID: 21923
			// (set) Token: 0x06007D7B RID: 32123 RVA: 0x000BA9F2 File Offset: 0x000B8BF2
			public virtual Unlimited<EnhancedTimeSpan> CompletedRequestAgeLimit
			{
				set
				{
					base.PowerSharpParameters["CompletedRequestAgeLimit"] = value;
				}
			}

			// Token: 0x170055A4 RID: 21924
			// (set) Token: 0x06007D7C RID: 32124 RVA: 0x000BAA0A File Offset: 0x000B8C0A
			public virtual InternalMrsFlag InternalFlags
			{
				set
				{
					base.PowerSharpParameters["InternalFlags"] = value;
				}
			}

			// Token: 0x170055A5 RID: 21925
			// (set) Token: 0x06007D7D RID: 32125 RVA: 0x000BAA22 File Offset: 0x000B8C22
			public virtual bool SuspendWhenReadyToComplete
			{
				set
				{
					base.PowerSharpParameters["SuspendWhenReadyToComplete"] = value;
				}
			}

			// Token: 0x170055A6 RID: 21926
			// (set) Token: 0x06007D7E RID: 32126 RVA: 0x000BAA3A File Offset: 0x000B8C3A
			public virtual DateTime? CompleteAfter
			{
				set
				{
					base.PowerSharpParameters["CompleteAfter"] = value;
				}
			}

			// Token: 0x170055A7 RID: 21927
			// (set) Token: 0x06007D7F RID: 32127 RVA: 0x000BAA52 File Offset: 0x000B8C52
			public virtual TimeSpan IncrementalSyncInterval
			{
				set
				{
					base.PowerSharpParameters["IncrementalSyncInterval"] = value;
				}
			}

			// Token: 0x170055A8 RID: 21928
			// (set) Token: 0x06007D80 RID: 32128 RVA: 0x000BAA6A File Offset: 0x000B8C6A
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new FolderMoveRequestIdParameter(value) : null);
				}
			}

			// Token: 0x170055A9 RID: 21929
			// (set) Token: 0x06007D81 RID: 32129 RVA: 0x000BAA88 File Offset: 0x000B8C88
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x170055AA RID: 21930
			// (set) Token: 0x06007D82 RID: 32130 RVA: 0x000BAA9B File Offset: 0x000B8C9B
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x170055AB RID: 21931
			// (set) Token: 0x06007D83 RID: 32131 RVA: 0x000BAAB3 File Offset: 0x000B8CB3
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x170055AC RID: 21932
			// (set) Token: 0x06007D84 RID: 32132 RVA: 0x000BAACB File Offset: 0x000B8CCB
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x170055AD RID: 21933
			// (set) Token: 0x06007D85 RID: 32133 RVA: 0x000BAAE3 File Offset: 0x000B8CE3
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x170055AE RID: 21934
			// (set) Token: 0x06007D86 RID: 32134 RVA: 0x000BAAFB File Offset: 0x000B8CFB
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
