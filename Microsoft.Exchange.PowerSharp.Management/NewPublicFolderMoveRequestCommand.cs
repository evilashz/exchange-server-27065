using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.MailboxReplicationService;
using Microsoft.Exchange.Management.RecipientTasks;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x02000A6B RID: 2667
	public class NewPublicFolderMoveRequestCommand : SyntheticCommandWithPipelineInput<PublicFolderMoveRequest, PublicFolderMoveRequest>
	{
		// Token: 0x06008479 RID: 33913 RVA: 0x000C3BA3 File Offset: 0x000C1DA3
		private NewPublicFolderMoveRequestCommand() : base("New-PublicFolderMoveRequest")
		{
		}

		// Token: 0x0600847A RID: 33914 RVA: 0x000C3BB0 File Offset: 0x000C1DB0
		public NewPublicFolderMoveRequestCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x0600847B RID: 33915 RVA: 0x000C3BBF File Offset: 0x000C1DBF
		public virtual NewPublicFolderMoveRequestCommand SetParameters(NewPublicFolderMoveRequestCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x02000A6C RID: 2668
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x17005B5C RID: 23388
			// (set) Token: 0x0600847C RID: 33916 RVA: 0x000C3BC9 File Offset: 0x000C1DC9
			public virtual string Organization
			{
				set
				{
					base.PowerSharpParameters["Organization"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x17005B5D RID: 23389
			// (set) Token: 0x0600847D RID: 33917 RVA: 0x000C3BE7 File Offset: 0x000C1DE7
			public virtual PublicFolderIdParameter Folders
			{
				set
				{
					base.PowerSharpParameters["Folders"] = value;
				}
			}

			// Token: 0x17005B5E RID: 23390
			// (set) Token: 0x0600847E RID: 33918 RVA: 0x000C3BFA File Offset: 0x000C1DFA
			public virtual string TargetMailbox
			{
				set
				{
					base.PowerSharpParameters["TargetMailbox"] = ((value != null) ? new MailboxIdParameter(value) : null);
				}
			}

			// Token: 0x17005B5F RID: 23391
			// (set) Token: 0x0600847F RID: 33919 RVA: 0x000C3C18 File Offset: 0x000C1E18
			public virtual SwitchParameter SuspendWhenReadyToComplete
			{
				set
				{
					base.PowerSharpParameters["SuspendWhenReadyToComplete"] = value;
				}
			}

			// Token: 0x17005B60 RID: 23392
			// (set) Token: 0x06008480 RID: 33920 RVA: 0x000C3C30 File Offset: 0x000C1E30
			public virtual SwitchParameter AllowLargeItems
			{
				set
				{
					base.PowerSharpParameters["AllowLargeItems"] = value;
				}
			}

			// Token: 0x17005B61 RID: 23393
			// (set) Token: 0x06008481 RID: 33921 RVA: 0x000C3C48 File Offset: 0x000C1E48
			public virtual Unlimited<int> BadItemLimit
			{
				set
				{
					base.PowerSharpParameters["BadItemLimit"] = value;
				}
			}

			// Token: 0x17005B62 RID: 23394
			// (set) Token: 0x06008482 RID: 33922 RVA: 0x000C3C60 File Offset: 0x000C1E60
			public virtual Unlimited<int> LargeItemLimit
			{
				set
				{
					base.PowerSharpParameters["LargeItemLimit"] = value;
				}
			}

			// Token: 0x17005B63 RID: 23395
			// (set) Token: 0x06008483 RID: 33923 RVA: 0x000C3C78 File Offset: 0x000C1E78
			public virtual SwitchParameter AcceptLargeDataLoss
			{
				set
				{
					base.PowerSharpParameters["AcceptLargeDataLoss"] = value;
				}
			}

			// Token: 0x17005B64 RID: 23396
			// (set) Token: 0x06008484 RID: 33924 RVA: 0x000C3C90 File Offset: 0x000C1E90
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17005B65 RID: 23397
			// (set) Token: 0x06008485 RID: 33925 RVA: 0x000C3CA3 File Offset: 0x000C1EA3
			public virtual SwitchParameter Suspend
			{
				set
				{
					base.PowerSharpParameters["Suspend"] = value;
				}
			}

			// Token: 0x17005B66 RID: 23398
			// (set) Token: 0x06008486 RID: 33926 RVA: 0x000C3CBB File Offset: 0x000C1EBB
			public virtual string SuspendComment
			{
				set
				{
					base.PowerSharpParameters["SuspendComment"] = value;
				}
			}

			// Token: 0x17005B67 RID: 23399
			// (set) Token: 0x06008487 RID: 33927 RVA: 0x000C3CCE File Offset: 0x000C1ECE
			public virtual string Name
			{
				set
				{
					base.PowerSharpParameters["Name"] = value;
				}
			}

			// Token: 0x17005B68 RID: 23400
			// (set) Token: 0x06008488 RID: 33928 RVA: 0x000C3CE1 File Offset: 0x000C1EE1
			public virtual RequestPriority Priority
			{
				set
				{
					base.PowerSharpParameters["Priority"] = value;
				}
			}

			// Token: 0x17005B69 RID: 23401
			// (set) Token: 0x06008489 RID: 33929 RVA: 0x000C3CF9 File Offset: 0x000C1EF9
			public virtual RequestWorkloadType WorkloadType
			{
				set
				{
					base.PowerSharpParameters["WorkloadType"] = value;
				}
			}

			// Token: 0x17005B6A RID: 23402
			// (set) Token: 0x0600848A RID: 33930 RVA: 0x000C3D11 File Offset: 0x000C1F11
			public virtual Unlimited<EnhancedTimeSpan> CompletedRequestAgeLimit
			{
				set
				{
					base.PowerSharpParameters["CompletedRequestAgeLimit"] = value;
				}
			}

			// Token: 0x17005B6B RID: 23403
			// (set) Token: 0x0600848B RID: 33931 RVA: 0x000C3D29 File Offset: 0x000C1F29
			public virtual InternalMrsFlag InternalFlags
			{
				set
				{
					base.PowerSharpParameters["InternalFlags"] = value;
				}
			}

			// Token: 0x17005B6C RID: 23404
			// (set) Token: 0x0600848C RID: 33932 RVA: 0x000C3D41 File Offset: 0x000C1F41
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17005B6D RID: 23405
			// (set) Token: 0x0600848D RID: 33933 RVA: 0x000C3D59 File Offset: 0x000C1F59
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17005B6E RID: 23406
			// (set) Token: 0x0600848E RID: 33934 RVA: 0x000C3D71 File Offset: 0x000C1F71
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17005B6F RID: 23407
			// (set) Token: 0x0600848F RID: 33935 RVA: 0x000C3D89 File Offset: 0x000C1F89
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17005B70 RID: 23408
			// (set) Token: 0x06008490 RID: 33936 RVA: 0x000C3DA1 File Offset: 0x000C1FA1
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
