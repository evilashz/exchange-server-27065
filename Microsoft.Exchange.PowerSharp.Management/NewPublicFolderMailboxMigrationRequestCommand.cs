using System;
using System.IO;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.MailboxReplicationService;
using Microsoft.Exchange.Management.RecipientTasks;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x02000A95 RID: 2709
	public class NewPublicFolderMailboxMigrationRequestCommand : SyntheticCommandWithPipelineInput<PublicFolderMailboxMigrationRequest, PublicFolderMailboxMigrationRequest>
	{
		// Token: 0x06008619 RID: 34329 RVA: 0x000C5D16 File Offset: 0x000C3F16
		private NewPublicFolderMailboxMigrationRequestCommand() : base("New-PublicFolderMailboxMigrationRequest")
		{
		}

		// Token: 0x0600861A RID: 34330 RVA: 0x000C5D23 File Offset: 0x000C3F23
		public NewPublicFolderMailboxMigrationRequestCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x0600861B RID: 34331 RVA: 0x000C5D32 File Offset: 0x000C3F32
		public virtual NewPublicFolderMailboxMigrationRequestCommand SetParameters(NewPublicFolderMailboxMigrationRequestCommand.MailboxMigrationLocalPublicFolderParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x0600861C RID: 34332 RVA: 0x000C5D3C File Offset: 0x000C3F3C
		public virtual NewPublicFolderMailboxMigrationRequestCommand SetParameters(NewPublicFolderMailboxMigrationRequestCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x0600861D RID: 34333 RVA: 0x000C5D46 File Offset: 0x000C3F46
		public virtual NewPublicFolderMailboxMigrationRequestCommand SetParameters(NewPublicFolderMailboxMigrationRequestCommand.MailboxMigrationOutlookAnywherePublicFolderParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x02000A96 RID: 2710
		public class MailboxMigrationLocalPublicFolderParameters : ParametersBase
		{
			// Token: 0x17005CA8 RID: 23720
			// (set) Token: 0x0600861E RID: 34334 RVA: 0x000C5D50 File Offset: 0x000C3F50
			public virtual DatabaseIdParameter SourceDatabase
			{
				set
				{
					base.PowerSharpParameters["SourceDatabase"] = value;
				}
			}

			// Token: 0x17005CA9 RID: 23721
			// (set) Token: 0x0600861F RID: 34335 RVA: 0x000C5D63 File Offset: 0x000C3F63
			public virtual Stream CSVStream
			{
				set
				{
					base.PowerSharpParameters["CSVStream"] = value;
				}
			}

			// Token: 0x17005CAA RID: 23722
			// (set) Token: 0x06008620 RID: 34336 RVA: 0x000C5D76 File Offset: 0x000C3F76
			public virtual byte CSVData
			{
				set
				{
					base.PowerSharpParameters["CSVData"] = value;
				}
			}

			// Token: 0x17005CAB RID: 23723
			// (set) Token: 0x06008621 RID: 34337 RVA: 0x000C5D8E File Offset: 0x000C3F8E
			public virtual string Organization
			{
				set
				{
					base.PowerSharpParameters["Organization"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x17005CAC RID: 23724
			// (set) Token: 0x06008622 RID: 34338 RVA: 0x000C5DAC File Offset: 0x000C3FAC
			public virtual string TargetMailbox
			{
				set
				{
					base.PowerSharpParameters["TargetMailbox"] = ((value != null) ? new MailboxIdParameter(value) : null);
				}
			}

			// Token: 0x17005CAD RID: 23725
			// (set) Token: 0x06008623 RID: 34339 RVA: 0x000C5DCA File Offset: 0x000C3FCA
			public virtual Unlimited<int> BadItemLimit
			{
				set
				{
					base.PowerSharpParameters["BadItemLimit"] = value;
				}
			}

			// Token: 0x17005CAE RID: 23726
			// (set) Token: 0x06008624 RID: 34340 RVA: 0x000C5DE2 File Offset: 0x000C3FE2
			public virtual Unlimited<int> LargeItemLimit
			{
				set
				{
					base.PowerSharpParameters["LargeItemLimit"] = value;
				}
			}

			// Token: 0x17005CAF RID: 23727
			// (set) Token: 0x06008625 RID: 34341 RVA: 0x000C5DFA File Offset: 0x000C3FFA
			public virtual SwitchParameter AcceptLargeDataLoss
			{
				set
				{
					base.PowerSharpParameters["AcceptLargeDataLoss"] = value;
				}
			}

			// Token: 0x17005CB0 RID: 23728
			// (set) Token: 0x06008626 RID: 34342 RVA: 0x000C5E12 File Offset: 0x000C4012
			public virtual string BatchName
			{
				set
				{
					base.PowerSharpParameters["BatchName"] = value;
				}
			}

			// Token: 0x17005CB1 RID: 23729
			// (set) Token: 0x06008627 RID: 34343 RVA: 0x000C5E25 File Offset: 0x000C4025
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17005CB2 RID: 23730
			// (set) Token: 0x06008628 RID: 34344 RVA: 0x000C5E38 File Offset: 0x000C4038
			public virtual SwitchParameter Suspend
			{
				set
				{
					base.PowerSharpParameters["Suspend"] = value;
				}
			}

			// Token: 0x17005CB3 RID: 23731
			// (set) Token: 0x06008629 RID: 34345 RVA: 0x000C5E50 File Offset: 0x000C4050
			public virtual string SuspendComment
			{
				set
				{
					base.PowerSharpParameters["SuspendComment"] = value;
				}
			}

			// Token: 0x17005CB4 RID: 23732
			// (set) Token: 0x0600862A RID: 34346 RVA: 0x000C5E63 File Offset: 0x000C4063
			public virtual RequestPriority Priority
			{
				set
				{
					base.PowerSharpParameters["Priority"] = value;
				}
			}

			// Token: 0x17005CB5 RID: 23733
			// (set) Token: 0x0600862B RID: 34347 RVA: 0x000C5E7B File Offset: 0x000C407B
			public virtual RequestWorkloadType WorkloadType
			{
				set
				{
					base.PowerSharpParameters["WorkloadType"] = value;
				}
			}

			// Token: 0x17005CB6 RID: 23734
			// (set) Token: 0x0600862C RID: 34348 RVA: 0x000C5E93 File Offset: 0x000C4093
			public virtual Unlimited<EnhancedTimeSpan> CompletedRequestAgeLimit
			{
				set
				{
					base.PowerSharpParameters["CompletedRequestAgeLimit"] = value;
				}
			}

			// Token: 0x17005CB7 RID: 23735
			// (set) Token: 0x0600862D RID: 34349 RVA: 0x000C5EAB File Offset: 0x000C40AB
			public virtual SkippableMergeComponent SkipMerging
			{
				set
				{
					base.PowerSharpParameters["SkipMerging"] = value;
				}
			}

			// Token: 0x17005CB8 RID: 23736
			// (set) Token: 0x0600862E RID: 34350 RVA: 0x000C5EC3 File Offset: 0x000C40C3
			public virtual InternalMrsFlag InternalFlags
			{
				set
				{
					base.PowerSharpParameters["InternalFlags"] = value;
				}
			}

			// Token: 0x17005CB9 RID: 23737
			// (set) Token: 0x0600862F RID: 34351 RVA: 0x000C5EDB File Offset: 0x000C40DB
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17005CBA RID: 23738
			// (set) Token: 0x06008630 RID: 34352 RVA: 0x000C5EF3 File Offset: 0x000C40F3
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17005CBB RID: 23739
			// (set) Token: 0x06008631 RID: 34353 RVA: 0x000C5F0B File Offset: 0x000C410B
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17005CBC RID: 23740
			// (set) Token: 0x06008632 RID: 34354 RVA: 0x000C5F23 File Offset: 0x000C4123
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17005CBD RID: 23741
			// (set) Token: 0x06008633 RID: 34355 RVA: 0x000C5F3B File Offset: 0x000C413B
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}

		// Token: 0x02000A97 RID: 2711
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x17005CBE RID: 23742
			// (set) Token: 0x06008635 RID: 34357 RVA: 0x000C5F5B File Offset: 0x000C415B
			public virtual Stream CSVStream
			{
				set
				{
					base.PowerSharpParameters["CSVStream"] = value;
				}
			}

			// Token: 0x17005CBF RID: 23743
			// (set) Token: 0x06008636 RID: 34358 RVA: 0x000C5F6E File Offset: 0x000C416E
			public virtual byte CSVData
			{
				set
				{
					base.PowerSharpParameters["CSVData"] = value;
				}
			}

			// Token: 0x17005CC0 RID: 23744
			// (set) Token: 0x06008637 RID: 34359 RVA: 0x000C5F86 File Offset: 0x000C4186
			public virtual string Organization
			{
				set
				{
					base.PowerSharpParameters["Organization"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x17005CC1 RID: 23745
			// (set) Token: 0x06008638 RID: 34360 RVA: 0x000C5FA4 File Offset: 0x000C41A4
			public virtual string TargetMailbox
			{
				set
				{
					base.PowerSharpParameters["TargetMailbox"] = ((value != null) ? new MailboxIdParameter(value) : null);
				}
			}

			// Token: 0x17005CC2 RID: 23746
			// (set) Token: 0x06008639 RID: 34361 RVA: 0x000C5FC2 File Offset: 0x000C41C2
			public virtual Unlimited<int> BadItemLimit
			{
				set
				{
					base.PowerSharpParameters["BadItemLimit"] = value;
				}
			}

			// Token: 0x17005CC3 RID: 23747
			// (set) Token: 0x0600863A RID: 34362 RVA: 0x000C5FDA File Offset: 0x000C41DA
			public virtual Unlimited<int> LargeItemLimit
			{
				set
				{
					base.PowerSharpParameters["LargeItemLimit"] = value;
				}
			}

			// Token: 0x17005CC4 RID: 23748
			// (set) Token: 0x0600863B RID: 34363 RVA: 0x000C5FF2 File Offset: 0x000C41F2
			public virtual SwitchParameter AcceptLargeDataLoss
			{
				set
				{
					base.PowerSharpParameters["AcceptLargeDataLoss"] = value;
				}
			}

			// Token: 0x17005CC5 RID: 23749
			// (set) Token: 0x0600863C RID: 34364 RVA: 0x000C600A File Offset: 0x000C420A
			public virtual string BatchName
			{
				set
				{
					base.PowerSharpParameters["BatchName"] = value;
				}
			}

			// Token: 0x17005CC6 RID: 23750
			// (set) Token: 0x0600863D RID: 34365 RVA: 0x000C601D File Offset: 0x000C421D
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17005CC7 RID: 23751
			// (set) Token: 0x0600863E RID: 34366 RVA: 0x000C6030 File Offset: 0x000C4230
			public virtual SwitchParameter Suspend
			{
				set
				{
					base.PowerSharpParameters["Suspend"] = value;
				}
			}

			// Token: 0x17005CC8 RID: 23752
			// (set) Token: 0x0600863F RID: 34367 RVA: 0x000C6048 File Offset: 0x000C4248
			public virtual string SuspendComment
			{
				set
				{
					base.PowerSharpParameters["SuspendComment"] = value;
				}
			}

			// Token: 0x17005CC9 RID: 23753
			// (set) Token: 0x06008640 RID: 34368 RVA: 0x000C605B File Offset: 0x000C425B
			public virtual RequestPriority Priority
			{
				set
				{
					base.PowerSharpParameters["Priority"] = value;
				}
			}

			// Token: 0x17005CCA RID: 23754
			// (set) Token: 0x06008641 RID: 34369 RVA: 0x000C6073 File Offset: 0x000C4273
			public virtual RequestWorkloadType WorkloadType
			{
				set
				{
					base.PowerSharpParameters["WorkloadType"] = value;
				}
			}

			// Token: 0x17005CCB RID: 23755
			// (set) Token: 0x06008642 RID: 34370 RVA: 0x000C608B File Offset: 0x000C428B
			public virtual Unlimited<EnhancedTimeSpan> CompletedRequestAgeLimit
			{
				set
				{
					base.PowerSharpParameters["CompletedRequestAgeLimit"] = value;
				}
			}

			// Token: 0x17005CCC RID: 23756
			// (set) Token: 0x06008643 RID: 34371 RVA: 0x000C60A3 File Offset: 0x000C42A3
			public virtual SkippableMergeComponent SkipMerging
			{
				set
				{
					base.PowerSharpParameters["SkipMerging"] = value;
				}
			}

			// Token: 0x17005CCD RID: 23757
			// (set) Token: 0x06008644 RID: 34372 RVA: 0x000C60BB File Offset: 0x000C42BB
			public virtual InternalMrsFlag InternalFlags
			{
				set
				{
					base.PowerSharpParameters["InternalFlags"] = value;
				}
			}

			// Token: 0x17005CCE RID: 23758
			// (set) Token: 0x06008645 RID: 34373 RVA: 0x000C60D3 File Offset: 0x000C42D3
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17005CCF RID: 23759
			// (set) Token: 0x06008646 RID: 34374 RVA: 0x000C60EB File Offset: 0x000C42EB
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17005CD0 RID: 23760
			// (set) Token: 0x06008647 RID: 34375 RVA: 0x000C6103 File Offset: 0x000C4303
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17005CD1 RID: 23761
			// (set) Token: 0x06008648 RID: 34376 RVA: 0x000C611B File Offset: 0x000C431B
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17005CD2 RID: 23762
			// (set) Token: 0x06008649 RID: 34377 RVA: 0x000C6133 File Offset: 0x000C4333
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}

		// Token: 0x02000A98 RID: 2712
		public class MailboxMigrationOutlookAnywherePublicFolderParameters : ParametersBase
		{
			// Token: 0x17005CD3 RID: 23763
			// (set) Token: 0x0600864B RID: 34379 RVA: 0x000C6153 File Offset: 0x000C4353
			public virtual string RemoteMailboxLegacyDN
			{
				set
				{
					base.PowerSharpParameters["RemoteMailboxLegacyDN"] = value;
				}
			}

			// Token: 0x17005CD4 RID: 23764
			// (set) Token: 0x0600864C RID: 34380 RVA: 0x000C6166 File Offset: 0x000C4366
			public virtual string RemoteMailboxServerLegacyDN
			{
				set
				{
					base.PowerSharpParameters["RemoteMailboxServerLegacyDN"] = value;
				}
			}

			// Token: 0x17005CD5 RID: 23765
			// (set) Token: 0x0600864D RID: 34381 RVA: 0x000C6179 File Offset: 0x000C4379
			public virtual Fqdn OutlookAnywhereHostName
			{
				set
				{
					base.PowerSharpParameters["OutlookAnywhereHostName"] = value;
				}
			}

			// Token: 0x17005CD6 RID: 23766
			// (set) Token: 0x0600864E RID: 34382 RVA: 0x000C618C File Offset: 0x000C438C
			public virtual AuthenticationMethod AuthenticationMethod
			{
				set
				{
					base.PowerSharpParameters["AuthenticationMethod"] = value;
				}
			}

			// Token: 0x17005CD7 RID: 23767
			// (set) Token: 0x0600864F RID: 34383 RVA: 0x000C61A4 File Offset: 0x000C43A4
			public virtual PSCredential RemoteCredential
			{
				set
				{
					base.PowerSharpParameters["RemoteCredential"] = value;
				}
			}

			// Token: 0x17005CD8 RID: 23768
			// (set) Token: 0x06008650 RID: 34384 RVA: 0x000C61B7 File Offset: 0x000C43B7
			public virtual Stream CSVStream
			{
				set
				{
					base.PowerSharpParameters["CSVStream"] = value;
				}
			}

			// Token: 0x17005CD9 RID: 23769
			// (set) Token: 0x06008651 RID: 34385 RVA: 0x000C61CA File Offset: 0x000C43CA
			public virtual byte CSVData
			{
				set
				{
					base.PowerSharpParameters["CSVData"] = value;
				}
			}

			// Token: 0x17005CDA RID: 23770
			// (set) Token: 0x06008652 RID: 34386 RVA: 0x000C61E2 File Offset: 0x000C43E2
			public virtual string Organization
			{
				set
				{
					base.PowerSharpParameters["Organization"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x17005CDB RID: 23771
			// (set) Token: 0x06008653 RID: 34387 RVA: 0x000C6200 File Offset: 0x000C4400
			public virtual string TargetMailbox
			{
				set
				{
					base.PowerSharpParameters["TargetMailbox"] = ((value != null) ? new MailboxIdParameter(value) : null);
				}
			}

			// Token: 0x17005CDC RID: 23772
			// (set) Token: 0x06008654 RID: 34388 RVA: 0x000C621E File Offset: 0x000C441E
			public virtual Unlimited<int> BadItemLimit
			{
				set
				{
					base.PowerSharpParameters["BadItemLimit"] = value;
				}
			}

			// Token: 0x17005CDD RID: 23773
			// (set) Token: 0x06008655 RID: 34389 RVA: 0x000C6236 File Offset: 0x000C4436
			public virtual Unlimited<int> LargeItemLimit
			{
				set
				{
					base.PowerSharpParameters["LargeItemLimit"] = value;
				}
			}

			// Token: 0x17005CDE RID: 23774
			// (set) Token: 0x06008656 RID: 34390 RVA: 0x000C624E File Offset: 0x000C444E
			public virtual SwitchParameter AcceptLargeDataLoss
			{
				set
				{
					base.PowerSharpParameters["AcceptLargeDataLoss"] = value;
				}
			}

			// Token: 0x17005CDF RID: 23775
			// (set) Token: 0x06008657 RID: 34391 RVA: 0x000C6266 File Offset: 0x000C4466
			public virtual string BatchName
			{
				set
				{
					base.PowerSharpParameters["BatchName"] = value;
				}
			}

			// Token: 0x17005CE0 RID: 23776
			// (set) Token: 0x06008658 RID: 34392 RVA: 0x000C6279 File Offset: 0x000C4479
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17005CE1 RID: 23777
			// (set) Token: 0x06008659 RID: 34393 RVA: 0x000C628C File Offset: 0x000C448C
			public virtual SwitchParameter Suspend
			{
				set
				{
					base.PowerSharpParameters["Suspend"] = value;
				}
			}

			// Token: 0x17005CE2 RID: 23778
			// (set) Token: 0x0600865A RID: 34394 RVA: 0x000C62A4 File Offset: 0x000C44A4
			public virtual string SuspendComment
			{
				set
				{
					base.PowerSharpParameters["SuspendComment"] = value;
				}
			}

			// Token: 0x17005CE3 RID: 23779
			// (set) Token: 0x0600865B RID: 34395 RVA: 0x000C62B7 File Offset: 0x000C44B7
			public virtual RequestPriority Priority
			{
				set
				{
					base.PowerSharpParameters["Priority"] = value;
				}
			}

			// Token: 0x17005CE4 RID: 23780
			// (set) Token: 0x0600865C RID: 34396 RVA: 0x000C62CF File Offset: 0x000C44CF
			public virtual RequestWorkloadType WorkloadType
			{
				set
				{
					base.PowerSharpParameters["WorkloadType"] = value;
				}
			}

			// Token: 0x17005CE5 RID: 23781
			// (set) Token: 0x0600865D RID: 34397 RVA: 0x000C62E7 File Offset: 0x000C44E7
			public virtual Unlimited<EnhancedTimeSpan> CompletedRequestAgeLimit
			{
				set
				{
					base.PowerSharpParameters["CompletedRequestAgeLimit"] = value;
				}
			}

			// Token: 0x17005CE6 RID: 23782
			// (set) Token: 0x0600865E RID: 34398 RVA: 0x000C62FF File Offset: 0x000C44FF
			public virtual SkippableMergeComponent SkipMerging
			{
				set
				{
					base.PowerSharpParameters["SkipMerging"] = value;
				}
			}

			// Token: 0x17005CE7 RID: 23783
			// (set) Token: 0x0600865F RID: 34399 RVA: 0x000C6317 File Offset: 0x000C4517
			public virtual InternalMrsFlag InternalFlags
			{
				set
				{
					base.PowerSharpParameters["InternalFlags"] = value;
				}
			}

			// Token: 0x17005CE8 RID: 23784
			// (set) Token: 0x06008660 RID: 34400 RVA: 0x000C632F File Offset: 0x000C452F
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17005CE9 RID: 23785
			// (set) Token: 0x06008661 RID: 34401 RVA: 0x000C6347 File Offset: 0x000C4547
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17005CEA RID: 23786
			// (set) Token: 0x06008662 RID: 34402 RVA: 0x000C635F File Offset: 0x000C455F
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17005CEB RID: 23787
			// (set) Token: 0x06008663 RID: 34403 RVA: 0x000C6377 File Offset: 0x000C4577
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17005CEC RID: 23788
			// (set) Token: 0x06008664 RID: 34404 RVA: 0x000C638F File Offset: 0x000C458F
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
