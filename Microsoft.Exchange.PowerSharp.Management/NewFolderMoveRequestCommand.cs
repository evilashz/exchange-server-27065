using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.MailboxReplicationService;
using Microsoft.Exchange.Management.RecipientTasks;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x020009BD RID: 2493
	public class NewFolderMoveRequestCommand : SyntheticCommandWithPipelineInput<FolderMoveRequest, FolderMoveRequest>
	{
		// Token: 0x06007D1A RID: 32026 RVA: 0x000BA223 File Offset: 0x000B8423
		private NewFolderMoveRequestCommand() : base("New-FolderMoveRequest")
		{
		}

		// Token: 0x06007D1B RID: 32027 RVA: 0x000BA230 File Offset: 0x000B8430
		public NewFolderMoveRequestCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x06007D1C RID: 32028 RVA: 0x000BA23F File Offset: 0x000B843F
		public virtual NewFolderMoveRequestCommand SetParameters(NewFolderMoveRequestCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x020009BE RID: 2494
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x17005559 RID: 21849
			// (set) Token: 0x06007D1D RID: 32029 RVA: 0x000BA249 File Offset: 0x000B8449
			public virtual MailboxFolderIdParameter Folders
			{
				set
				{
					base.PowerSharpParameters["Folders"] = value;
				}
			}

			// Token: 0x1700555A RID: 21850
			// (set) Token: 0x06007D1E RID: 32030 RVA: 0x000BA25C File Offset: 0x000B845C
			public virtual string SourceMailbox
			{
				set
				{
					base.PowerSharpParameters["SourceMailbox"] = ((value != null) ? new MailboxIdParameter(value) : null);
				}
			}

			// Token: 0x1700555B RID: 21851
			// (set) Token: 0x06007D1F RID: 32031 RVA: 0x000BA27A File Offset: 0x000B847A
			public virtual string TargetMailbox
			{
				set
				{
					base.PowerSharpParameters["TargetMailbox"] = ((value != null) ? new MailboxIdParameter(value) : null);
				}
			}

			// Token: 0x1700555C RID: 21852
			// (set) Token: 0x06007D20 RID: 32032 RVA: 0x000BA298 File Offset: 0x000B8498
			public virtual SwitchParameter SuspendWhenReadyToComplete
			{
				set
				{
					base.PowerSharpParameters["SuspendWhenReadyToComplete"] = value;
				}
			}

			// Token: 0x1700555D RID: 21853
			// (set) Token: 0x06007D21 RID: 32033 RVA: 0x000BA2B0 File Offset: 0x000B84B0
			public virtual SwitchParameter AllowLargeItems
			{
				set
				{
					base.PowerSharpParameters["AllowLargeItems"] = value;
				}
			}

			// Token: 0x1700555E RID: 21854
			// (set) Token: 0x06007D22 RID: 32034 RVA: 0x000BA2C8 File Offset: 0x000B84C8
			public virtual DateTime CompleteAfter
			{
				set
				{
					base.PowerSharpParameters["CompleteAfter"] = value;
				}
			}

			// Token: 0x1700555F RID: 21855
			// (set) Token: 0x06007D23 RID: 32035 RVA: 0x000BA2E0 File Offset: 0x000B84E0
			public virtual TimeSpan IncrementalSyncInterval
			{
				set
				{
					base.PowerSharpParameters["IncrementalSyncInterval"] = value;
				}
			}

			// Token: 0x17005560 RID: 21856
			// (set) Token: 0x06007D24 RID: 32036 RVA: 0x000BA2F8 File Offset: 0x000B84F8
			public virtual Unlimited<int> BadItemLimit
			{
				set
				{
					base.PowerSharpParameters["BadItemLimit"] = value;
				}
			}

			// Token: 0x17005561 RID: 21857
			// (set) Token: 0x06007D25 RID: 32037 RVA: 0x000BA310 File Offset: 0x000B8510
			public virtual Unlimited<int> LargeItemLimit
			{
				set
				{
					base.PowerSharpParameters["LargeItemLimit"] = value;
				}
			}

			// Token: 0x17005562 RID: 21858
			// (set) Token: 0x06007D26 RID: 32038 RVA: 0x000BA328 File Offset: 0x000B8528
			public virtual SwitchParameter AcceptLargeDataLoss
			{
				set
				{
					base.PowerSharpParameters["AcceptLargeDataLoss"] = value;
				}
			}

			// Token: 0x17005563 RID: 21859
			// (set) Token: 0x06007D27 RID: 32039 RVA: 0x000BA340 File Offset: 0x000B8540
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17005564 RID: 21860
			// (set) Token: 0x06007D28 RID: 32040 RVA: 0x000BA353 File Offset: 0x000B8553
			public virtual SwitchParameter Suspend
			{
				set
				{
					base.PowerSharpParameters["Suspend"] = value;
				}
			}

			// Token: 0x17005565 RID: 21861
			// (set) Token: 0x06007D29 RID: 32041 RVA: 0x000BA36B File Offset: 0x000B856B
			public virtual string SuspendComment
			{
				set
				{
					base.PowerSharpParameters["SuspendComment"] = value;
				}
			}

			// Token: 0x17005566 RID: 21862
			// (set) Token: 0x06007D2A RID: 32042 RVA: 0x000BA37E File Offset: 0x000B857E
			public virtual string Name
			{
				set
				{
					base.PowerSharpParameters["Name"] = value;
				}
			}

			// Token: 0x17005567 RID: 21863
			// (set) Token: 0x06007D2B RID: 32043 RVA: 0x000BA391 File Offset: 0x000B8591
			public virtual RequestPriority Priority
			{
				set
				{
					base.PowerSharpParameters["Priority"] = value;
				}
			}

			// Token: 0x17005568 RID: 21864
			// (set) Token: 0x06007D2C RID: 32044 RVA: 0x000BA3A9 File Offset: 0x000B85A9
			public virtual RequestWorkloadType WorkloadType
			{
				set
				{
					base.PowerSharpParameters["WorkloadType"] = value;
				}
			}

			// Token: 0x17005569 RID: 21865
			// (set) Token: 0x06007D2D RID: 32045 RVA: 0x000BA3C1 File Offset: 0x000B85C1
			public virtual Unlimited<EnhancedTimeSpan> CompletedRequestAgeLimit
			{
				set
				{
					base.PowerSharpParameters["CompletedRequestAgeLimit"] = value;
				}
			}

			// Token: 0x1700556A RID: 21866
			// (set) Token: 0x06007D2E RID: 32046 RVA: 0x000BA3D9 File Offset: 0x000B85D9
			public virtual InternalMrsFlag InternalFlags
			{
				set
				{
					base.PowerSharpParameters["InternalFlags"] = value;
				}
			}

			// Token: 0x1700556B RID: 21867
			// (set) Token: 0x06007D2F RID: 32047 RVA: 0x000BA3F1 File Offset: 0x000B85F1
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x1700556C RID: 21868
			// (set) Token: 0x06007D30 RID: 32048 RVA: 0x000BA409 File Offset: 0x000B8609
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x1700556D RID: 21869
			// (set) Token: 0x06007D31 RID: 32049 RVA: 0x000BA421 File Offset: 0x000B8621
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x1700556E RID: 21870
			// (set) Token: 0x06007D32 RID: 32050 RVA: 0x000BA439 File Offset: 0x000B8639
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x1700556F RID: 21871
			// (set) Token: 0x06007D33 RID: 32051 RVA: 0x000BA451 File Offset: 0x000B8651
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
