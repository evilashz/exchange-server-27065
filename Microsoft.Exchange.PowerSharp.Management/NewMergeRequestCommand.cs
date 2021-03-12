using System;
using System.Globalization;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.MailboxReplicationService;
using Microsoft.Exchange.Management.RecipientTasks;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x020009EF RID: 2543
	public class NewMergeRequestCommand : SyntheticCommandWithPipelineInput<MergeRequest, MergeRequest>
	{
		// Token: 0x06007F87 RID: 32647 RVA: 0x000BD586 File Offset: 0x000BB786
		private NewMergeRequestCommand() : base("New-MergeRequest")
		{
		}

		// Token: 0x06007F88 RID: 32648 RVA: 0x000BD593 File Offset: 0x000BB793
		public NewMergeRequestCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x06007F89 RID: 32649 RVA: 0x000BD5A2 File Offset: 0x000BB7A2
		public virtual NewMergeRequestCommand SetParameters(NewMergeRequestCommand.MigrationLocalMergeParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x06007F8A RID: 32650 RVA: 0x000BD5AC File Offset: 0x000BB7AC
		public virtual NewMergeRequestCommand SetParameters(NewMergeRequestCommand.MigrationOutlookAnywhereMergePullParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x06007F8B RID: 32651 RVA: 0x000BD5B6 File Offset: 0x000BB7B6
		public virtual NewMergeRequestCommand SetParameters(NewMergeRequestCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x020009F0 RID: 2544
		public class MigrationLocalMergeParameters : ParametersBase
		{
			// Token: 0x17005762 RID: 22370
			// (set) Token: 0x06007F8C RID: 32652 RVA: 0x000BD5C0 File Offset: 0x000BB7C0
			public virtual string SourceMailbox
			{
				set
				{
					base.PowerSharpParameters["SourceMailbox"] = ((value != null) ? new MailboxOrMailUserIdParameter(value) : null);
				}
			}

			// Token: 0x17005763 RID: 22371
			// (set) Token: 0x06007F8D RID: 32653 RVA: 0x000BD5DE File Offset: 0x000BB7DE
			public virtual string TargetMailbox
			{
				set
				{
					base.PowerSharpParameters["TargetMailbox"] = ((value != null) ? new MailboxOrMailUserIdParameter(value) : null);
				}
			}

			// Token: 0x17005764 RID: 22372
			// (set) Token: 0x06007F8E RID: 32654 RVA: 0x000BD5FC File Offset: 0x000BB7FC
			public virtual string SourceRootFolder
			{
				set
				{
					base.PowerSharpParameters["SourceRootFolder"] = value;
				}
			}

			// Token: 0x17005765 RID: 22373
			// (set) Token: 0x06007F8F RID: 32655 RVA: 0x000BD60F File Offset: 0x000BB80F
			public virtual string TargetRootFolder
			{
				set
				{
					base.PowerSharpParameters["TargetRootFolder"] = value;
				}
			}

			// Token: 0x17005766 RID: 22374
			// (set) Token: 0x06007F90 RID: 32656 RVA: 0x000BD622 File Offset: 0x000BB822
			public virtual SwitchParameter SourceIsArchive
			{
				set
				{
					base.PowerSharpParameters["SourceIsArchive"] = value;
				}
			}

			// Token: 0x17005767 RID: 22375
			// (set) Token: 0x06007F91 RID: 32657 RVA: 0x000BD63A File Offset: 0x000BB83A
			public virtual SwitchParameter TargetIsArchive
			{
				set
				{
					base.PowerSharpParameters["TargetIsArchive"] = value;
				}
			}

			// Token: 0x17005768 RID: 22376
			// (set) Token: 0x06007F92 RID: 32658 RVA: 0x000BD652 File Offset: 0x000BB852
			public virtual bool SuspendWhenReadyToComplete
			{
				set
				{
					base.PowerSharpParameters["SuspendWhenReadyToComplete"] = value;
				}
			}

			// Token: 0x17005769 RID: 22377
			// (set) Token: 0x06007F93 RID: 32659 RVA: 0x000BD66A File Offset: 0x000BB86A
			public virtual SwitchParameter AllowLegacyDNMismatch
			{
				set
				{
					base.PowerSharpParameters["AllowLegacyDNMismatch"] = value;
				}
			}

			// Token: 0x1700576A RID: 22378
			// (set) Token: 0x06007F94 RID: 32660 RVA: 0x000BD682 File Offset: 0x000BB882
			public virtual string ContentFilter
			{
				set
				{
					base.PowerSharpParameters["ContentFilter"] = value;
				}
			}

			// Token: 0x1700576B RID: 22379
			// (set) Token: 0x06007F95 RID: 32661 RVA: 0x000BD695 File Offset: 0x000BB895
			public virtual CultureInfo ContentFilterLanguage
			{
				set
				{
					base.PowerSharpParameters["ContentFilterLanguage"] = value;
				}
			}

			// Token: 0x1700576C RID: 22380
			// (set) Token: 0x06007F96 RID: 32662 RVA: 0x000BD6A8 File Offset: 0x000BB8A8
			public virtual string IncludeFolders
			{
				set
				{
					base.PowerSharpParameters["IncludeFolders"] = value;
				}
			}

			// Token: 0x1700576D RID: 22381
			// (set) Token: 0x06007F97 RID: 32663 RVA: 0x000BD6BB File Offset: 0x000BB8BB
			public virtual string ExcludeFolders
			{
				set
				{
					base.PowerSharpParameters["ExcludeFolders"] = value;
				}
			}

			// Token: 0x1700576E RID: 22382
			// (set) Token: 0x06007F98 RID: 32664 RVA: 0x000BD6CE File Offset: 0x000BB8CE
			public virtual SwitchParameter ExcludeDumpster
			{
				set
				{
					base.PowerSharpParameters["ExcludeDumpster"] = value;
				}
			}

			// Token: 0x1700576F RID: 22383
			// (set) Token: 0x06007F99 RID: 32665 RVA: 0x000BD6E6 File Offset: 0x000BB8E6
			public virtual ConflictResolutionOption ConflictResolutionOption
			{
				set
				{
					base.PowerSharpParameters["ConflictResolutionOption"] = value;
				}
			}

			// Token: 0x17005770 RID: 22384
			// (set) Token: 0x06007F9A RID: 32666 RVA: 0x000BD6FE File Offset: 0x000BB8FE
			public virtual FAICopyOption AssociatedMessagesCopyOption
			{
				set
				{
					base.PowerSharpParameters["AssociatedMessagesCopyOption"] = value;
				}
			}

			// Token: 0x17005771 RID: 22385
			// (set) Token: 0x06007F9B RID: 32667 RVA: 0x000BD716 File Offset: 0x000BB916
			public virtual DateTime StartAfter
			{
				set
				{
					base.PowerSharpParameters["StartAfter"] = value;
				}
			}

			// Token: 0x17005772 RID: 22386
			// (set) Token: 0x06007F9C RID: 32668 RVA: 0x000BD72E File Offset: 0x000BB92E
			public virtual TimeSpan IncrementalSyncInterval
			{
				set
				{
					base.PowerSharpParameters["IncrementalSyncInterval"] = value;
				}
			}

			// Token: 0x17005773 RID: 22387
			// (set) Token: 0x06007F9D RID: 32669 RVA: 0x000BD746 File Offset: 0x000BB946
			public virtual Unlimited<int> BadItemLimit
			{
				set
				{
					base.PowerSharpParameters["BadItemLimit"] = value;
				}
			}

			// Token: 0x17005774 RID: 22388
			// (set) Token: 0x06007F9E RID: 32670 RVA: 0x000BD75E File Offset: 0x000BB95E
			public virtual Unlimited<int> LargeItemLimit
			{
				set
				{
					base.PowerSharpParameters["LargeItemLimit"] = value;
				}
			}

			// Token: 0x17005775 RID: 22389
			// (set) Token: 0x06007F9F RID: 32671 RVA: 0x000BD776 File Offset: 0x000BB976
			public virtual SwitchParameter AcceptLargeDataLoss
			{
				set
				{
					base.PowerSharpParameters["AcceptLargeDataLoss"] = value;
				}
			}

			// Token: 0x17005776 RID: 22390
			// (set) Token: 0x06007FA0 RID: 32672 RVA: 0x000BD78E File Offset: 0x000BB98E
			public virtual string BatchName
			{
				set
				{
					base.PowerSharpParameters["BatchName"] = value;
				}
			}

			// Token: 0x17005777 RID: 22391
			// (set) Token: 0x06007FA1 RID: 32673 RVA: 0x000BD7A1 File Offset: 0x000BB9A1
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17005778 RID: 22392
			// (set) Token: 0x06007FA2 RID: 32674 RVA: 0x000BD7B4 File Offset: 0x000BB9B4
			public virtual SwitchParameter Suspend
			{
				set
				{
					base.PowerSharpParameters["Suspend"] = value;
				}
			}

			// Token: 0x17005779 RID: 22393
			// (set) Token: 0x06007FA3 RID: 32675 RVA: 0x000BD7CC File Offset: 0x000BB9CC
			public virtual string SuspendComment
			{
				set
				{
					base.PowerSharpParameters["SuspendComment"] = value;
				}
			}

			// Token: 0x1700577A RID: 22394
			// (set) Token: 0x06007FA4 RID: 32676 RVA: 0x000BD7DF File Offset: 0x000BB9DF
			public virtual string Name
			{
				set
				{
					base.PowerSharpParameters["Name"] = value;
				}
			}

			// Token: 0x1700577B RID: 22395
			// (set) Token: 0x06007FA5 RID: 32677 RVA: 0x000BD7F2 File Offset: 0x000BB9F2
			public virtual RequestPriority Priority
			{
				set
				{
					base.PowerSharpParameters["Priority"] = value;
				}
			}

			// Token: 0x1700577C RID: 22396
			// (set) Token: 0x06007FA6 RID: 32678 RVA: 0x000BD80A File Offset: 0x000BBA0A
			public virtual RequestWorkloadType WorkloadType
			{
				set
				{
					base.PowerSharpParameters["WorkloadType"] = value;
				}
			}

			// Token: 0x1700577D RID: 22397
			// (set) Token: 0x06007FA7 RID: 32679 RVA: 0x000BD822 File Offset: 0x000BBA22
			public virtual Unlimited<EnhancedTimeSpan> CompletedRequestAgeLimit
			{
				set
				{
					base.PowerSharpParameters["CompletedRequestAgeLimit"] = value;
				}
			}

			// Token: 0x1700577E RID: 22398
			// (set) Token: 0x06007FA8 RID: 32680 RVA: 0x000BD83A File Offset: 0x000BBA3A
			public virtual SkippableMergeComponent SkipMerging
			{
				set
				{
					base.PowerSharpParameters["SkipMerging"] = value;
				}
			}

			// Token: 0x1700577F RID: 22399
			// (set) Token: 0x06007FA9 RID: 32681 RVA: 0x000BD852 File Offset: 0x000BBA52
			public virtual InternalMrsFlag InternalFlags
			{
				set
				{
					base.PowerSharpParameters["InternalFlags"] = value;
				}
			}

			// Token: 0x17005780 RID: 22400
			// (set) Token: 0x06007FAA RID: 32682 RVA: 0x000BD86A File Offset: 0x000BBA6A
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17005781 RID: 22401
			// (set) Token: 0x06007FAB RID: 32683 RVA: 0x000BD882 File Offset: 0x000BBA82
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17005782 RID: 22402
			// (set) Token: 0x06007FAC RID: 32684 RVA: 0x000BD89A File Offset: 0x000BBA9A
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17005783 RID: 22403
			// (set) Token: 0x06007FAD RID: 32685 RVA: 0x000BD8B2 File Offset: 0x000BBAB2
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17005784 RID: 22404
			// (set) Token: 0x06007FAE RID: 32686 RVA: 0x000BD8CA File Offset: 0x000BBACA
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}

		// Token: 0x020009F1 RID: 2545
		public class MigrationOutlookAnywhereMergePullParameters : ParametersBase
		{
			// Token: 0x17005785 RID: 22405
			// (set) Token: 0x06007FB0 RID: 32688 RVA: 0x000BD8EA File Offset: 0x000BBAEA
			public virtual string TargetMailbox
			{
				set
				{
					base.PowerSharpParameters["TargetMailbox"] = ((value != null) ? new MailboxOrMailUserIdParameter(value) : null);
				}
			}

			// Token: 0x17005786 RID: 22406
			// (set) Token: 0x06007FB1 RID: 32689 RVA: 0x000BD908 File Offset: 0x000BBB08
			public virtual SwitchParameter TargetIsArchive
			{
				set
				{
					base.PowerSharpParameters["TargetIsArchive"] = value;
				}
			}

			// Token: 0x17005787 RID: 22407
			// (set) Token: 0x06007FB2 RID: 32690 RVA: 0x000BD920 File Offset: 0x000BBB20
			public virtual string RemoteSourceMailboxLegacyDN
			{
				set
				{
					base.PowerSharpParameters["RemoteSourceMailboxLegacyDN"] = value;
				}
			}

			// Token: 0x17005788 RID: 22408
			// (set) Token: 0x06007FB3 RID: 32691 RVA: 0x000BD933 File Offset: 0x000BBB33
			public virtual string RemoteSourceUserLegacyDN
			{
				set
				{
					base.PowerSharpParameters["RemoteSourceUserLegacyDN"] = value;
				}
			}

			// Token: 0x17005789 RID: 22409
			// (set) Token: 0x06007FB4 RID: 32692 RVA: 0x000BD946 File Offset: 0x000BBB46
			public virtual string RemoteSourceMailboxServerLegacyDN
			{
				set
				{
					base.PowerSharpParameters["RemoteSourceMailboxServerLegacyDN"] = value;
				}
			}

			// Token: 0x1700578A RID: 22410
			// (set) Token: 0x06007FB5 RID: 32693 RVA: 0x000BD959 File Offset: 0x000BBB59
			public virtual Fqdn OutlookAnywhereHostName
			{
				set
				{
					base.PowerSharpParameters["OutlookAnywhereHostName"] = value;
				}
			}

			// Token: 0x1700578B RID: 22411
			// (set) Token: 0x06007FB6 RID: 32694 RVA: 0x000BD96C File Offset: 0x000BBB6C
			public virtual bool IsAdministrativeCredential
			{
				set
				{
					base.PowerSharpParameters["IsAdministrativeCredential"] = value;
				}
			}

			// Token: 0x1700578C RID: 22412
			// (set) Token: 0x06007FB7 RID: 32695 RVA: 0x000BD984 File Offset: 0x000BBB84
			public virtual AuthenticationMethod AuthenticationMethod
			{
				set
				{
					base.PowerSharpParameters["AuthenticationMethod"] = value;
				}
			}

			// Token: 0x1700578D RID: 22413
			// (set) Token: 0x06007FB8 RID: 32696 RVA: 0x000BD99C File Offset: 0x000BBB9C
			public virtual bool SuspendWhenReadyToComplete
			{
				set
				{
					base.PowerSharpParameters["SuspendWhenReadyToComplete"] = value;
				}
			}

			// Token: 0x1700578E RID: 22414
			// (set) Token: 0x06007FB9 RID: 32697 RVA: 0x000BD9B4 File Offset: 0x000BBBB4
			public virtual PSCredential RemoteCredential
			{
				set
				{
					base.PowerSharpParameters["RemoteCredential"] = value;
				}
			}

			// Token: 0x1700578F RID: 22415
			// (set) Token: 0x06007FBA RID: 32698 RVA: 0x000BD9C7 File Offset: 0x000BBBC7
			public virtual DateTime StartAfter
			{
				set
				{
					base.PowerSharpParameters["StartAfter"] = value;
				}
			}

			// Token: 0x17005790 RID: 22416
			// (set) Token: 0x06007FBB RID: 32699 RVA: 0x000BD9DF File Offset: 0x000BBBDF
			public virtual TimeSpan IncrementalSyncInterval
			{
				set
				{
					base.PowerSharpParameters["IncrementalSyncInterval"] = value;
				}
			}

			// Token: 0x17005791 RID: 22417
			// (set) Token: 0x06007FBC RID: 32700 RVA: 0x000BD9F7 File Offset: 0x000BBBF7
			public virtual Unlimited<int> BadItemLimit
			{
				set
				{
					base.PowerSharpParameters["BadItemLimit"] = value;
				}
			}

			// Token: 0x17005792 RID: 22418
			// (set) Token: 0x06007FBD RID: 32701 RVA: 0x000BDA0F File Offset: 0x000BBC0F
			public virtual Unlimited<int> LargeItemLimit
			{
				set
				{
					base.PowerSharpParameters["LargeItemLimit"] = value;
				}
			}

			// Token: 0x17005793 RID: 22419
			// (set) Token: 0x06007FBE RID: 32702 RVA: 0x000BDA27 File Offset: 0x000BBC27
			public virtual SwitchParameter AcceptLargeDataLoss
			{
				set
				{
					base.PowerSharpParameters["AcceptLargeDataLoss"] = value;
				}
			}

			// Token: 0x17005794 RID: 22420
			// (set) Token: 0x06007FBF RID: 32703 RVA: 0x000BDA3F File Offset: 0x000BBC3F
			public virtual string BatchName
			{
				set
				{
					base.PowerSharpParameters["BatchName"] = value;
				}
			}

			// Token: 0x17005795 RID: 22421
			// (set) Token: 0x06007FC0 RID: 32704 RVA: 0x000BDA52 File Offset: 0x000BBC52
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17005796 RID: 22422
			// (set) Token: 0x06007FC1 RID: 32705 RVA: 0x000BDA65 File Offset: 0x000BBC65
			public virtual SwitchParameter Suspend
			{
				set
				{
					base.PowerSharpParameters["Suspend"] = value;
				}
			}

			// Token: 0x17005797 RID: 22423
			// (set) Token: 0x06007FC2 RID: 32706 RVA: 0x000BDA7D File Offset: 0x000BBC7D
			public virtual string SuspendComment
			{
				set
				{
					base.PowerSharpParameters["SuspendComment"] = value;
				}
			}

			// Token: 0x17005798 RID: 22424
			// (set) Token: 0x06007FC3 RID: 32707 RVA: 0x000BDA90 File Offset: 0x000BBC90
			public virtual string Name
			{
				set
				{
					base.PowerSharpParameters["Name"] = value;
				}
			}

			// Token: 0x17005799 RID: 22425
			// (set) Token: 0x06007FC4 RID: 32708 RVA: 0x000BDAA3 File Offset: 0x000BBCA3
			public virtual RequestPriority Priority
			{
				set
				{
					base.PowerSharpParameters["Priority"] = value;
				}
			}

			// Token: 0x1700579A RID: 22426
			// (set) Token: 0x06007FC5 RID: 32709 RVA: 0x000BDABB File Offset: 0x000BBCBB
			public virtual RequestWorkloadType WorkloadType
			{
				set
				{
					base.PowerSharpParameters["WorkloadType"] = value;
				}
			}

			// Token: 0x1700579B RID: 22427
			// (set) Token: 0x06007FC6 RID: 32710 RVA: 0x000BDAD3 File Offset: 0x000BBCD3
			public virtual Unlimited<EnhancedTimeSpan> CompletedRequestAgeLimit
			{
				set
				{
					base.PowerSharpParameters["CompletedRequestAgeLimit"] = value;
				}
			}

			// Token: 0x1700579C RID: 22428
			// (set) Token: 0x06007FC7 RID: 32711 RVA: 0x000BDAEB File Offset: 0x000BBCEB
			public virtual SkippableMergeComponent SkipMerging
			{
				set
				{
					base.PowerSharpParameters["SkipMerging"] = value;
				}
			}

			// Token: 0x1700579D RID: 22429
			// (set) Token: 0x06007FC8 RID: 32712 RVA: 0x000BDB03 File Offset: 0x000BBD03
			public virtual InternalMrsFlag InternalFlags
			{
				set
				{
					base.PowerSharpParameters["InternalFlags"] = value;
				}
			}

			// Token: 0x1700579E RID: 22430
			// (set) Token: 0x06007FC9 RID: 32713 RVA: 0x000BDB1B File Offset: 0x000BBD1B
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x1700579F RID: 22431
			// (set) Token: 0x06007FCA RID: 32714 RVA: 0x000BDB33 File Offset: 0x000BBD33
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x170057A0 RID: 22432
			// (set) Token: 0x06007FCB RID: 32715 RVA: 0x000BDB4B File Offset: 0x000BBD4B
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x170057A1 RID: 22433
			// (set) Token: 0x06007FCC RID: 32716 RVA: 0x000BDB63 File Offset: 0x000BBD63
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x170057A2 RID: 22434
			// (set) Token: 0x06007FCD RID: 32717 RVA: 0x000BDB7B File Offset: 0x000BBD7B
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}

		// Token: 0x020009F2 RID: 2546
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x170057A3 RID: 22435
			// (set) Token: 0x06007FCF RID: 32719 RVA: 0x000BDB9B File Offset: 0x000BBD9B
			public virtual TimeSpan IncrementalSyncInterval
			{
				set
				{
					base.PowerSharpParameters["IncrementalSyncInterval"] = value;
				}
			}

			// Token: 0x170057A4 RID: 22436
			// (set) Token: 0x06007FD0 RID: 32720 RVA: 0x000BDBB3 File Offset: 0x000BBDB3
			public virtual Unlimited<int> BadItemLimit
			{
				set
				{
					base.PowerSharpParameters["BadItemLimit"] = value;
				}
			}

			// Token: 0x170057A5 RID: 22437
			// (set) Token: 0x06007FD1 RID: 32721 RVA: 0x000BDBCB File Offset: 0x000BBDCB
			public virtual Unlimited<int> LargeItemLimit
			{
				set
				{
					base.PowerSharpParameters["LargeItemLimit"] = value;
				}
			}

			// Token: 0x170057A6 RID: 22438
			// (set) Token: 0x06007FD2 RID: 32722 RVA: 0x000BDBE3 File Offset: 0x000BBDE3
			public virtual SwitchParameter AcceptLargeDataLoss
			{
				set
				{
					base.PowerSharpParameters["AcceptLargeDataLoss"] = value;
				}
			}

			// Token: 0x170057A7 RID: 22439
			// (set) Token: 0x06007FD3 RID: 32723 RVA: 0x000BDBFB File Offset: 0x000BBDFB
			public virtual string BatchName
			{
				set
				{
					base.PowerSharpParameters["BatchName"] = value;
				}
			}

			// Token: 0x170057A8 RID: 22440
			// (set) Token: 0x06007FD4 RID: 32724 RVA: 0x000BDC0E File Offset: 0x000BBE0E
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x170057A9 RID: 22441
			// (set) Token: 0x06007FD5 RID: 32725 RVA: 0x000BDC21 File Offset: 0x000BBE21
			public virtual SwitchParameter Suspend
			{
				set
				{
					base.PowerSharpParameters["Suspend"] = value;
				}
			}

			// Token: 0x170057AA RID: 22442
			// (set) Token: 0x06007FD6 RID: 32726 RVA: 0x000BDC39 File Offset: 0x000BBE39
			public virtual string SuspendComment
			{
				set
				{
					base.PowerSharpParameters["SuspendComment"] = value;
				}
			}

			// Token: 0x170057AB RID: 22443
			// (set) Token: 0x06007FD7 RID: 32727 RVA: 0x000BDC4C File Offset: 0x000BBE4C
			public virtual string Name
			{
				set
				{
					base.PowerSharpParameters["Name"] = value;
				}
			}

			// Token: 0x170057AC RID: 22444
			// (set) Token: 0x06007FD8 RID: 32728 RVA: 0x000BDC5F File Offset: 0x000BBE5F
			public virtual RequestPriority Priority
			{
				set
				{
					base.PowerSharpParameters["Priority"] = value;
				}
			}

			// Token: 0x170057AD RID: 22445
			// (set) Token: 0x06007FD9 RID: 32729 RVA: 0x000BDC77 File Offset: 0x000BBE77
			public virtual RequestWorkloadType WorkloadType
			{
				set
				{
					base.PowerSharpParameters["WorkloadType"] = value;
				}
			}

			// Token: 0x170057AE RID: 22446
			// (set) Token: 0x06007FDA RID: 32730 RVA: 0x000BDC8F File Offset: 0x000BBE8F
			public virtual Unlimited<EnhancedTimeSpan> CompletedRequestAgeLimit
			{
				set
				{
					base.PowerSharpParameters["CompletedRequestAgeLimit"] = value;
				}
			}

			// Token: 0x170057AF RID: 22447
			// (set) Token: 0x06007FDB RID: 32731 RVA: 0x000BDCA7 File Offset: 0x000BBEA7
			public virtual SkippableMergeComponent SkipMerging
			{
				set
				{
					base.PowerSharpParameters["SkipMerging"] = value;
				}
			}

			// Token: 0x170057B0 RID: 22448
			// (set) Token: 0x06007FDC RID: 32732 RVA: 0x000BDCBF File Offset: 0x000BBEBF
			public virtual InternalMrsFlag InternalFlags
			{
				set
				{
					base.PowerSharpParameters["InternalFlags"] = value;
				}
			}

			// Token: 0x170057B1 RID: 22449
			// (set) Token: 0x06007FDD RID: 32733 RVA: 0x000BDCD7 File Offset: 0x000BBED7
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x170057B2 RID: 22450
			// (set) Token: 0x06007FDE RID: 32734 RVA: 0x000BDCEF File Offset: 0x000BBEEF
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x170057B3 RID: 22451
			// (set) Token: 0x06007FDF RID: 32735 RVA: 0x000BDD07 File Offset: 0x000BBF07
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x170057B4 RID: 22452
			// (set) Token: 0x06007FE0 RID: 32736 RVA: 0x000BDD1F File Offset: 0x000BBF1F
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x170057B5 RID: 22453
			// (set) Token: 0x06007FE1 RID: 32737 RVA: 0x000BDD37 File Offset: 0x000BBF37
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
