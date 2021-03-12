using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.MailboxReplicationService;
using Microsoft.Exchange.Management.RecipientTasks;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x02000A51 RID: 2641
	public class NewMailboxRestoreRequestCommand : SyntheticCommandWithPipelineInput<MailboxRestoreRequest, MailboxRestoreRequest>
	{
		// Token: 0x0600835D RID: 33629 RVA: 0x000C24B6 File Offset: 0x000C06B6
		private NewMailboxRestoreRequestCommand() : base("New-MailboxRestoreRequest")
		{
		}

		// Token: 0x0600835E RID: 33630 RVA: 0x000C24C3 File Offset: 0x000C06C3
		public NewMailboxRestoreRequestCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x0600835F RID: 33631 RVA: 0x000C24D2 File Offset: 0x000C06D2
		public virtual NewMailboxRestoreRequestCommand SetParameters(NewMailboxRestoreRequestCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x06008360 RID: 33632 RVA: 0x000C24DC File Offset: 0x000C06DC
		public virtual NewMailboxRestoreRequestCommand SetParameters(NewMailboxRestoreRequestCommand.MigrationLocalMailboxRestoreParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x06008361 RID: 33633 RVA: 0x000C24E6 File Offset: 0x000C06E6
		public virtual NewMailboxRestoreRequestCommand SetParameters(NewMailboxRestoreRequestCommand.RemoteMailboxRestoreParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x02000A52 RID: 2642
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x17005A74 RID: 23156
			// (set) Token: 0x06008362 RID: 33634 RVA: 0x000C24F0 File Offset: 0x000C06F0
			public virtual StoreMailboxIdParameter SourceStoreMailbox
			{
				set
				{
					base.PowerSharpParameters["SourceStoreMailbox"] = value;
				}
			}

			// Token: 0x17005A75 RID: 23157
			// (set) Token: 0x06008363 RID: 33635 RVA: 0x000C2503 File Offset: 0x000C0703
			public virtual string TargetMailbox
			{
				set
				{
					base.PowerSharpParameters["TargetMailbox"] = ((value != null) ? new MailboxOrMailUserIdParameter(value) : null);
				}
			}

			// Token: 0x17005A76 RID: 23158
			// (set) Token: 0x06008364 RID: 33636 RVA: 0x000C2521 File Offset: 0x000C0721
			public virtual string SourceRootFolder
			{
				set
				{
					base.PowerSharpParameters["SourceRootFolder"] = value;
				}
			}

			// Token: 0x17005A77 RID: 23159
			// (set) Token: 0x06008365 RID: 33637 RVA: 0x000C2534 File Offset: 0x000C0734
			public virtual string TargetRootFolder
			{
				set
				{
					base.PowerSharpParameters["TargetRootFolder"] = value;
				}
			}

			// Token: 0x17005A78 RID: 23160
			// (set) Token: 0x06008366 RID: 33638 RVA: 0x000C2547 File Offset: 0x000C0747
			public virtual SwitchParameter TargetIsArchive
			{
				set
				{
					base.PowerSharpParameters["TargetIsArchive"] = value;
				}
			}

			// Token: 0x17005A79 RID: 23161
			// (set) Token: 0x06008367 RID: 33639 RVA: 0x000C255F File Offset: 0x000C075F
			public virtual Unlimited<int> BadItemLimit
			{
				set
				{
					base.PowerSharpParameters["BadItemLimit"] = value;
				}
			}

			// Token: 0x17005A7A RID: 23162
			// (set) Token: 0x06008368 RID: 33640 RVA: 0x000C2577 File Offset: 0x000C0777
			public virtual Unlimited<int> LargeItemLimit
			{
				set
				{
					base.PowerSharpParameters["LargeItemLimit"] = value;
				}
			}

			// Token: 0x17005A7B RID: 23163
			// (set) Token: 0x06008369 RID: 33641 RVA: 0x000C258F File Offset: 0x000C078F
			public virtual SwitchParameter AcceptLargeDataLoss
			{
				set
				{
					base.PowerSharpParameters["AcceptLargeDataLoss"] = value;
				}
			}

			// Token: 0x17005A7C RID: 23164
			// (set) Token: 0x0600836A RID: 33642 RVA: 0x000C25A7 File Offset: 0x000C07A7
			public virtual string BatchName
			{
				set
				{
					base.PowerSharpParameters["BatchName"] = value;
				}
			}

			// Token: 0x17005A7D RID: 23165
			// (set) Token: 0x0600836B RID: 33643 RVA: 0x000C25BA File Offset: 0x000C07BA
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17005A7E RID: 23166
			// (set) Token: 0x0600836C RID: 33644 RVA: 0x000C25CD File Offset: 0x000C07CD
			public virtual SwitchParameter Suspend
			{
				set
				{
					base.PowerSharpParameters["Suspend"] = value;
				}
			}

			// Token: 0x17005A7F RID: 23167
			// (set) Token: 0x0600836D RID: 33645 RVA: 0x000C25E5 File Offset: 0x000C07E5
			public virtual string SuspendComment
			{
				set
				{
					base.PowerSharpParameters["SuspendComment"] = value;
				}
			}

			// Token: 0x17005A80 RID: 23168
			// (set) Token: 0x0600836E RID: 33646 RVA: 0x000C25F8 File Offset: 0x000C07F8
			public virtual string Name
			{
				set
				{
					base.PowerSharpParameters["Name"] = value;
				}
			}

			// Token: 0x17005A81 RID: 23169
			// (set) Token: 0x0600836F RID: 33647 RVA: 0x000C260B File Offset: 0x000C080B
			public virtual RequestPriority Priority
			{
				set
				{
					base.PowerSharpParameters["Priority"] = value;
				}
			}

			// Token: 0x17005A82 RID: 23170
			// (set) Token: 0x06008370 RID: 33648 RVA: 0x000C2623 File Offset: 0x000C0823
			public virtual RequestWorkloadType WorkloadType
			{
				set
				{
					base.PowerSharpParameters["WorkloadType"] = value;
				}
			}

			// Token: 0x17005A83 RID: 23171
			// (set) Token: 0x06008371 RID: 33649 RVA: 0x000C263B File Offset: 0x000C083B
			public virtual Unlimited<EnhancedTimeSpan> CompletedRequestAgeLimit
			{
				set
				{
					base.PowerSharpParameters["CompletedRequestAgeLimit"] = value;
				}
			}

			// Token: 0x17005A84 RID: 23172
			// (set) Token: 0x06008372 RID: 33650 RVA: 0x000C2653 File Offset: 0x000C0853
			public virtual SkippableMergeComponent SkipMerging
			{
				set
				{
					base.PowerSharpParameters["SkipMerging"] = value;
				}
			}

			// Token: 0x17005A85 RID: 23173
			// (set) Token: 0x06008373 RID: 33651 RVA: 0x000C266B File Offset: 0x000C086B
			public virtual InternalMrsFlag InternalFlags
			{
				set
				{
					base.PowerSharpParameters["InternalFlags"] = value;
				}
			}

			// Token: 0x17005A86 RID: 23174
			// (set) Token: 0x06008374 RID: 33652 RVA: 0x000C2683 File Offset: 0x000C0883
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17005A87 RID: 23175
			// (set) Token: 0x06008375 RID: 33653 RVA: 0x000C269B File Offset: 0x000C089B
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17005A88 RID: 23176
			// (set) Token: 0x06008376 RID: 33654 RVA: 0x000C26B3 File Offset: 0x000C08B3
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17005A89 RID: 23177
			// (set) Token: 0x06008377 RID: 33655 RVA: 0x000C26CB File Offset: 0x000C08CB
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17005A8A RID: 23178
			// (set) Token: 0x06008378 RID: 33656 RVA: 0x000C26E3 File Offset: 0x000C08E3
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}

		// Token: 0x02000A53 RID: 2643
		public class MigrationLocalMailboxRestoreParameters : ParametersBase
		{
			// Token: 0x17005A8B RID: 23179
			// (set) Token: 0x0600837A RID: 33658 RVA: 0x000C2703 File Offset: 0x000C0903
			public virtual DatabaseIdParameter SourceDatabase
			{
				set
				{
					base.PowerSharpParameters["SourceDatabase"] = value;
				}
			}

			// Token: 0x17005A8C RID: 23180
			// (set) Token: 0x0600837B RID: 33659 RVA: 0x000C2716 File Offset: 0x000C0916
			public virtual SwitchParameter AllowLegacyDNMismatch
			{
				set
				{
					base.PowerSharpParameters["AllowLegacyDNMismatch"] = value;
				}
			}

			// Token: 0x17005A8D RID: 23181
			// (set) Token: 0x0600837C RID: 33660 RVA: 0x000C272E File Offset: 0x000C092E
			public virtual string IncludeFolders
			{
				set
				{
					base.PowerSharpParameters["IncludeFolders"] = value;
				}
			}

			// Token: 0x17005A8E RID: 23182
			// (set) Token: 0x0600837D RID: 33661 RVA: 0x000C2741 File Offset: 0x000C0941
			public virtual string ExcludeFolders
			{
				set
				{
					base.PowerSharpParameters["ExcludeFolders"] = value;
				}
			}

			// Token: 0x17005A8F RID: 23183
			// (set) Token: 0x0600837E RID: 33662 RVA: 0x000C2754 File Offset: 0x000C0954
			public virtual SwitchParameter ExcludeDumpster
			{
				set
				{
					base.PowerSharpParameters["ExcludeDumpster"] = value;
				}
			}

			// Token: 0x17005A90 RID: 23184
			// (set) Token: 0x0600837F RID: 33663 RVA: 0x000C276C File Offset: 0x000C096C
			public virtual ConflictResolutionOption ConflictResolutionOption
			{
				set
				{
					base.PowerSharpParameters["ConflictResolutionOption"] = value;
				}
			}

			// Token: 0x17005A91 RID: 23185
			// (set) Token: 0x06008380 RID: 33664 RVA: 0x000C2784 File Offset: 0x000C0984
			public virtual FAICopyOption AssociatedMessagesCopyOption
			{
				set
				{
					base.PowerSharpParameters["AssociatedMessagesCopyOption"] = value;
				}
			}

			// Token: 0x17005A92 RID: 23186
			// (set) Token: 0x06008381 RID: 33665 RVA: 0x000C279C File Offset: 0x000C099C
			public virtual StoreMailboxIdParameter SourceStoreMailbox
			{
				set
				{
					base.PowerSharpParameters["SourceStoreMailbox"] = value;
				}
			}

			// Token: 0x17005A93 RID: 23187
			// (set) Token: 0x06008382 RID: 33666 RVA: 0x000C27AF File Offset: 0x000C09AF
			public virtual string TargetMailbox
			{
				set
				{
					base.PowerSharpParameters["TargetMailbox"] = ((value != null) ? new MailboxOrMailUserIdParameter(value) : null);
				}
			}

			// Token: 0x17005A94 RID: 23188
			// (set) Token: 0x06008383 RID: 33667 RVA: 0x000C27CD File Offset: 0x000C09CD
			public virtual string SourceRootFolder
			{
				set
				{
					base.PowerSharpParameters["SourceRootFolder"] = value;
				}
			}

			// Token: 0x17005A95 RID: 23189
			// (set) Token: 0x06008384 RID: 33668 RVA: 0x000C27E0 File Offset: 0x000C09E0
			public virtual string TargetRootFolder
			{
				set
				{
					base.PowerSharpParameters["TargetRootFolder"] = value;
				}
			}

			// Token: 0x17005A96 RID: 23190
			// (set) Token: 0x06008385 RID: 33669 RVA: 0x000C27F3 File Offset: 0x000C09F3
			public virtual SwitchParameter TargetIsArchive
			{
				set
				{
					base.PowerSharpParameters["TargetIsArchive"] = value;
				}
			}

			// Token: 0x17005A97 RID: 23191
			// (set) Token: 0x06008386 RID: 33670 RVA: 0x000C280B File Offset: 0x000C0A0B
			public virtual Unlimited<int> BadItemLimit
			{
				set
				{
					base.PowerSharpParameters["BadItemLimit"] = value;
				}
			}

			// Token: 0x17005A98 RID: 23192
			// (set) Token: 0x06008387 RID: 33671 RVA: 0x000C2823 File Offset: 0x000C0A23
			public virtual Unlimited<int> LargeItemLimit
			{
				set
				{
					base.PowerSharpParameters["LargeItemLimit"] = value;
				}
			}

			// Token: 0x17005A99 RID: 23193
			// (set) Token: 0x06008388 RID: 33672 RVA: 0x000C283B File Offset: 0x000C0A3B
			public virtual SwitchParameter AcceptLargeDataLoss
			{
				set
				{
					base.PowerSharpParameters["AcceptLargeDataLoss"] = value;
				}
			}

			// Token: 0x17005A9A RID: 23194
			// (set) Token: 0x06008389 RID: 33673 RVA: 0x000C2853 File Offset: 0x000C0A53
			public virtual string BatchName
			{
				set
				{
					base.PowerSharpParameters["BatchName"] = value;
				}
			}

			// Token: 0x17005A9B RID: 23195
			// (set) Token: 0x0600838A RID: 33674 RVA: 0x000C2866 File Offset: 0x000C0A66
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17005A9C RID: 23196
			// (set) Token: 0x0600838B RID: 33675 RVA: 0x000C2879 File Offset: 0x000C0A79
			public virtual SwitchParameter Suspend
			{
				set
				{
					base.PowerSharpParameters["Suspend"] = value;
				}
			}

			// Token: 0x17005A9D RID: 23197
			// (set) Token: 0x0600838C RID: 33676 RVA: 0x000C2891 File Offset: 0x000C0A91
			public virtual string SuspendComment
			{
				set
				{
					base.PowerSharpParameters["SuspendComment"] = value;
				}
			}

			// Token: 0x17005A9E RID: 23198
			// (set) Token: 0x0600838D RID: 33677 RVA: 0x000C28A4 File Offset: 0x000C0AA4
			public virtual string Name
			{
				set
				{
					base.PowerSharpParameters["Name"] = value;
				}
			}

			// Token: 0x17005A9F RID: 23199
			// (set) Token: 0x0600838E RID: 33678 RVA: 0x000C28B7 File Offset: 0x000C0AB7
			public virtual RequestPriority Priority
			{
				set
				{
					base.PowerSharpParameters["Priority"] = value;
				}
			}

			// Token: 0x17005AA0 RID: 23200
			// (set) Token: 0x0600838F RID: 33679 RVA: 0x000C28CF File Offset: 0x000C0ACF
			public virtual RequestWorkloadType WorkloadType
			{
				set
				{
					base.PowerSharpParameters["WorkloadType"] = value;
				}
			}

			// Token: 0x17005AA1 RID: 23201
			// (set) Token: 0x06008390 RID: 33680 RVA: 0x000C28E7 File Offset: 0x000C0AE7
			public virtual Unlimited<EnhancedTimeSpan> CompletedRequestAgeLimit
			{
				set
				{
					base.PowerSharpParameters["CompletedRequestAgeLimit"] = value;
				}
			}

			// Token: 0x17005AA2 RID: 23202
			// (set) Token: 0x06008391 RID: 33681 RVA: 0x000C28FF File Offset: 0x000C0AFF
			public virtual SkippableMergeComponent SkipMerging
			{
				set
				{
					base.PowerSharpParameters["SkipMerging"] = value;
				}
			}

			// Token: 0x17005AA3 RID: 23203
			// (set) Token: 0x06008392 RID: 33682 RVA: 0x000C2917 File Offset: 0x000C0B17
			public virtual InternalMrsFlag InternalFlags
			{
				set
				{
					base.PowerSharpParameters["InternalFlags"] = value;
				}
			}

			// Token: 0x17005AA4 RID: 23204
			// (set) Token: 0x06008393 RID: 33683 RVA: 0x000C292F File Offset: 0x000C0B2F
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17005AA5 RID: 23205
			// (set) Token: 0x06008394 RID: 33684 RVA: 0x000C2947 File Offset: 0x000C0B47
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17005AA6 RID: 23206
			// (set) Token: 0x06008395 RID: 33685 RVA: 0x000C295F File Offset: 0x000C0B5F
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17005AA7 RID: 23207
			// (set) Token: 0x06008396 RID: 33686 RVA: 0x000C2977 File Offset: 0x000C0B77
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17005AA8 RID: 23208
			// (set) Token: 0x06008397 RID: 33687 RVA: 0x000C298F File Offset: 0x000C0B8F
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}

		// Token: 0x02000A54 RID: 2644
		public class RemoteMailboxRestoreParameters : ParametersBase
		{
			// Token: 0x17005AA9 RID: 23209
			// (set) Token: 0x06008399 RID: 33689 RVA: 0x000C29AF File Offset: 0x000C0BAF
			public virtual Guid RemoteDatabaseGuid
			{
				set
				{
					base.PowerSharpParameters["RemoteDatabaseGuid"] = value;
				}
			}

			// Token: 0x17005AAA RID: 23210
			// (set) Token: 0x0600839A RID: 33690 RVA: 0x000C29C7 File Offset: 0x000C0BC7
			public virtual RemoteRestoreType RemoteRestoreType
			{
				set
				{
					base.PowerSharpParameters["RemoteRestoreType"] = value;
				}
			}

			// Token: 0x17005AAB RID: 23211
			// (set) Token: 0x0600839B RID: 33691 RVA: 0x000C29DF File Offset: 0x000C0BDF
			public virtual Fqdn RemoteHostName
			{
				set
				{
					base.PowerSharpParameters["RemoteHostName"] = value;
				}
			}

			// Token: 0x17005AAC RID: 23212
			// (set) Token: 0x0600839C RID: 33692 RVA: 0x000C29F2 File Offset: 0x000C0BF2
			public virtual PSCredential RemoteCredential
			{
				set
				{
					base.PowerSharpParameters["RemoteCredential"] = value;
				}
			}

			// Token: 0x17005AAD RID: 23213
			// (set) Token: 0x0600839D RID: 33693 RVA: 0x000C2A05 File Offset: 0x000C0C05
			public virtual SwitchParameter AllowLegacyDNMismatch
			{
				set
				{
					base.PowerSharpParameters["AllowLegacyDNMismatch"] = value;
				}
			}

			// Token: 0x17005AAE RID: 23214
			// (set) Token: 0x0600839E RID: 33694 RVA: 0x000C2A1D File Offset: 0x000C0C1D
			public virtual string IncludeFolders
			{
				set
				{
					base.PowerSharpParameters["IncludeFolders"] = value;
				}
			}

			// Token: 0x17005AAF RID: 23215
			// (set) Token: 0x0600839F RID: 33695 RVA: 0x000C2A30 File Offset: 0x000C0C30
			public virtual string ExcludeFolders
			{
				set
				{
					base.PowerSharpParameters["ExcludeFolders"] = value;
				}
			}

			// Token: 0x17005AB0 RID: 23216
			// (set) Token: 0x060083A0 RID: 33696 RVA: 0x000C2A43 File Offset: 0x000C0C43
			public virtual SwitchParameter ExcludeDumpster
			{
				set
				{
					base.PowerSharpParameters["ExcludeDumpster"] = value;
				}
			}

			// Token: 0x17005AB1 RID: 23217
			// (set) Token: 0x060083A1 RID: 33697 RVA: 0x000C2A5B File Offset: 0x000C0C5B
			public virtual ConflictResolutionOption ConflictResolutionOption
			{
				set
				{
					base.PowerSharpParameters["ConflictResolutionOption"] = value;
				}
			}

			// Token: 0x17005AB2 RID: 23218
			// (set) Token: 0x060083A2 RID: 33698 RVA: 0x000C2A73 File Offset: 0x000C0C73
			public virtual FAICopyOption AssociatedMessagesCopyOption
			{
				set
				{
					base.PowerSharpParameters["AssociatedMessagesCopyOption"] = value;
				}
			}

			// Token: 0x17005AB3 RID: 23219
			// (set) Token: 0x060083A3 RID: 33699 RVA: 0x000C2A8B File Offset: 0x000C0C8B
			public virtual StoreMailboxIdParameter SourceStoreMailbox
			{
				set
				{
					base.PowerSharpParameters["SourceStoreMailbox"] = value;
				}
			}

			// Token: 0x17005AB4 RID: 23220
			// (set) Token: 0x060083A4 RID: 33700 RVA: 0x000C2A9E File Offset: 0x000C0C9E
			public virtual string TargetMailbox
			{
				set
				{
					base.PowerSharpParameters["TargetMailbox"] = ((value != null) ? new MailboxOrMailUserIdParameter(value) : null);
				}
			}

			// Token: 0x17005AB5 RID: 23221
			// (set) Token: 0x060083A5 RID: 33701 RVA: 0x000C2ABC File Offset: 0x000C0CBC
			public virtual string SourceRootFolder
			{
				set
				{
					base.PowerSharpParameters["SourceRootFolder"] = value;
				}
			}

			// Token: 0x17005AB6 RID: 23222
			// (set) Token: 0x060083A6 RID: 33702 RVA: 0x000C2ACF File Offset: 0x000C0CCF
			public virtual string TargetRootFolder
			{
				set
				{
					base.PowerSharpParameters["TargetRootFolder"] = value;
				}
			}

			// Token: 0x17005AB7 RID: 23223
			// (set) Token: 0x060083A7 RID: 33703 RVA: 0x000C2AE2 File Offset: 0x000C0CE2
			public virtual SwitchParameter TargetIsArchive
			{
				set
				{
					base.PowerSharpParameters["TargetIsArchive"] = value;
				}
			}

			// Token: 0x17005AB8 RID: 23224
			// (set) Token: 0x060083A8 RID: 33704 RVA: 0x000C2AFA File Offset: 0x000C0CFA
			public virtual Unlimited<int> BadItemLimit
			{
				set
				{
					base.PowerSharpParameters["BadItemLimit"] = value;
				}
			}

			// Token: 0x17005AB9 RID: 23225
			// (set) Token: 0x060083A9 RID: 33705 RVA: 0x000C2B12 File Offset: 0x000C0D12
			public virtual Unlimited<int> LargeItemLimit
			{
				set
				{
					base.PowerSharpParameters["LargeItemLimit"] = value;
				}
			}

			// Token: 0x17005ABA RID: 23226
			// (set) Token: 0x060083AA RID: 33706 RVA: 0x000C2B2A File Offset: 0x000C0D2A
			public virtual SwitchParameter AcceptLargeDataLoss
			{
				set
				{
					base.PowerSharpParameters["AcceptLargeDataLoss"] = value;
				}
			}

			// Token: 0x17005ABB RID: 23227
			// (set) Token: 0x060083AB RID: 33707 RVA: 0x000C2B42 File Offset: 0x000C0D42
			public virtual string BatchName
			{
				set
				{
					base.PowerSharpParameters["BatchName"] = value;
				}
			}

			// Token: 0x17005ABC RID: 23228
			// (set) Token: 0x060083AC RID: 33708 RVA: 0x000C2B55 File Offset: 0x000C0D55
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17005ABD RID: 23229
			// (set) Token: 0x060083AD RID: 33709 RVA: 0x000C2B68 File Offset: 0x000C0D68
			public virtual SwitchParameter Suspend
			{
				set
				{
					base.PowerSharpParameters["Suspend"] = value;
				}
			}

			// Token: 0x17005ABE RID: 23230
			// (set) Token: 0x060083AE RID: 33710 RVA: 0x000C2B80 File Offset: 0x000C0D80
			public virtual string SuspendComment
			{
				set
				{
					base.PowerSharpParameters["SuspendComment"] = value;
				}
			}

			// Token: 0x17005ABF RID: 23231
			// (set) Token: 0x060083AF RID: 33711 RVA: 0x000C2B93 File Offset: 0x000C0D93
			public virtual string Name
			{
				set
				{
					base.PowerSharpParameters["Name"] = value;
				}
			}

			// Token: 0x17005AC0 RID: 23232
			// (set) Token: 0x060083B0 RID: 33712 RVA: 0x000C2BA6 File Offset: 0x000C0DA6
			public virtual RequestPriority Priority
			{
				set
				{
					base.PowerSharpParameters["Priority"] = value;
				}
			}

			// Token: 0x17005AC1 RID: 23233
			// (set) Token: 0x060083B1 RID: 33713 RVA: 0x000C2BBE File Offset: 0x000C0DBE
			public virtual RequestWorkloadType WorkloadType
			{
				set
				{
					base.PowerSharpParameters["WorkloadType"] = value;
				}
			}

			// Token: 0x17005AC2 RID: 23234
			// (set) Token: 0x060083B2 RID: 33714 RVA: 0x000C2BD6 File Offset: 0x000C0DD6
			public virtual Unlimited<EnhancedTimeSpan> CompletedRequestAgeLimit
			{
				set
				{
					base.PowerSharpParameters["CompletedRequestAgeLimit"] = value;
				}
			}

			// Token: 0x17005AC3 RID: 23235
			// (set) Token: 0x060083B3 RID: 33715 RVA: 0x000C2BEE File Offset: 0x000C0DEE
			public virtual SkippableMergeComponent SkipMerging
			{
				set
				{
					base.PowerSharpParameters["SkipMerging"] = value;
				}
			}

			// Token: 0x17005AC4 RID: 23236
			// (set) Token: 0x060083B4 RID: 33716 RVA: 0x000C2C06 File Offset: 0x000C0E06
			public virtual InternalMrsFlag InternalFlags
			{
				set
				{
					base.PowerSharpParameters["InternalFlags"] = value;
				}
			}

			// Token: 0x17005AC5 RID: 23237
			// (set) Token: 0x060083B5 RID: 33717 RVA: 0x000C2C1E File Offset: 0x000C0E1E
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17005AC6 RID: 23238
			// (set) Token: 0x060083B6 RID: 33718 RVA: 0x000C2C36 File Offset: 0x000C0E36
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17005AC7 RID: 23239
			// (set) Token: 0x060083B7 RID: 33719 RVA: 0x000C2C4E File Offset: 0x000C0E4E
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17005AC8 RID: 23240
			// (set) Token: 0x060083B8 RID: 33720 RVA: 0x000C2C66 File Offset: 0x000C0E66
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17005AC9 RID: 23241
			// (set) Token: 0x060083B9 RID: 33721 RVA: 0x000C2C7E File Offset: 0x000C0E7E
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
