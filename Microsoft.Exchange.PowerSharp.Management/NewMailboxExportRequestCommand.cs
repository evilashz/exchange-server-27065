using System;
using System.Globalization;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.MailboxReplicationService;
using Microsoft.Exchange.Management.RecipientTasks;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x02000A09 RID: 2569
	public class NewMailboxExportRequestCommand : SyntheticCommandWithPipelineInput<MailboxExportRequest, MailboxExportRequest>
	{
		// Token: 0x060080A5 RID: 32933 RVA: 0x000BECD1 File Offset: 0x000BCED1
		private NewMailboxExportRequestCommand() : base("New-MailboxExportRequest")
		{
		}

		// Token: 0x060080A6 RID: 32934 RVA: 0x000BECDE File Offset: 0x000BCEDE
		public NewMailboxExportRequestCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x060080A7 RID: 32935 RVA: 0x000BECED File Offset: 0x000BCEED
		public virtual NewMailboxExportRequestCommand SetParameters(NewMailboxExportRequestCommand.MailboxExportRequestParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x060080A8 RID: 32936 RVA: 0x000BECF7 File Offset: 0x000BCEF7
		public virtual NewMailboxExportRequestCommand SetParameters(NewMailboxExportRequestCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x02000A0A RID: 2570
		public class MailboxExportRequestParameters : ParametersBase
		{
			// Token: 0x1700584C RID: 22604
			// (set) Token: 0x060080A9 RID: 32937 RVA: 0x000BED01 File Offset: 0x000BCF01
			public virtual string Mailbox
			{
				set
				{
					base.PowerSharpParameters["Mailbox"] = ((value != null) ? new MailboxOrMailUserIdParameter(value) : null);
				}
			}

			// Token: 0x1700584D RID: 22605
			// (set) Token: 0x060080AA RID: 32938 RVA: 0x000BED1F File Offset: 0x000BCF1F
			public virtual LongPath FilePath
			{
				set
				{
					base.PowerSharpParameters["FilePath"] = value;
				}
			}

			// Token: 0x1700584E RID: 22606
			// (set) Token: 0x060080AB RID: 32939 RVA: 0x000BED32 File Offset: 0x000BCF32
			public virtual string SourceRootFolder
			{
				set
				{
					base.PowerSharpParameters["SourceRootFolder"] = value;
				}
			}

			// Token: 0x1700584F RID: 22607
			// (set) Token: 0x060080AC RID: 32940 RVA: 0x000BED45 File Offset: 0x000BCF45
			public virtual string TargetRootFolder
			{
				set
				{
					base.PowerSharpParameters["TargetRootFolder"] = value;
				}
			}

			// Token: 0x17005850 RID: 22608
			// (set) Token: 0x060080AD RID: 32941 RVA: 0x000BED58 File Offset: 0x000BCF58
			public virtual SwitchParameter IsArchive
			{
				set
				{
					base.PowerSharpParameters["IsArchive"] = value;
				}
			}

			// Token: 0x17005851 RID: 22609
			// (set) Token: 0x060080AE RID: 32942 RVA: 0x000BED70 File Offset: 0x000BCF70
			public virtual PSCredential RemoteCredential
			{
				set
				{
					base.PowerSharpParameters["RemoteCredential"] = value;
				}
			}

			// Token: 0x17005852 RID: 22610
			// (set) Token: 0x060080AF RID: 32943 RVA: 0x000BED83 File Offset: 0x000BCF83
			public virtual Fqdn RemoteHostName
			{
				set
				{
					base.PowerSharpParameters["RemoteHostName"] = value;
				}
			}

			// Token: 0x17005853 RID: 22611
			// (set) Token: 0x060080B0 RID: 32944 RVA: 0x000BED96 File Offset: 0x000BCF96
			public virtual string ContentFilter
			{
				set
				{
					base.PowerSharpParameters["ContentFilter"] = value;
				}
			}

			// Token: 0x17005854 RID: 22612
			// (set) Token: 0x060080B1 RID: 32945 RVA: 0x000BEDA9 File Offset: 0x000BCFA9
			public virtual CultureInfo ContentFilterLanguage
			{
				set
				{
					base.PowerSharpParameters["ContentFilterLanguage"] = value;
				}
			}

			// Token: 0x17005855 RID: 22613
			// (set) Token: 0x060080B2 RID: 32946 RVA: 0x000BEDBC File Offset: 0x000BCFBC
			public virtual string IncludeFolders
			{
				set
				{
					base.PowerSharpParameters["IncludeFolders"] = value;
				}
			}

			// Token: 0x17005856 RID: 22614
			// (set) Token: 0x060080B3 RID: 32947 RVA: 0x000BEDCF File Offset: 0x000BCFCF
			public virtual string ExcludeFolders
			{
				set
				{
					base.PowerSharpParameters["ExcludeFolders"] = value;
				}
			}

			// Token: 0x17005857 RID: 22615
			// (set) Token: 0x060080B4 RID: 32948 RVA: 0x000BEDE2 File Offset: 0x000BCFE2
			public virtual SwitchParameter ExcludeDumpster
			{
				set
				{
					base.PowerSharpParameters["ExcludeDumpster"] = value;
				}
			}

			// Token: 0x17005858 RID: 22616
			// (set) Token: 0x060080B5 RID: 32949 RVA: 0x000BEDFA File Offset: 0x000BCFFA
			public virtual ConflictResolutionOption ConflictResolutionOption
			{
				set
				{
					base.PowerSharpParameters["ConflictResolutionOption"] = value;
				}
			}

			// Token: 0x17005859 RID: 22617
			// (set) Token: 0x060080B6 RID: 32950 RVA: 0x000BEE12 File Offset: 0x000BD012
			public virtual FAICopyOption AssociatedMessagesCopyOption
			{
				set
				{
					base.PowerSharpParameters["AssociatedMessagesCopyOption"] = value;
				}
			}

			// Token: 0x1700585A RID: 22618
			// (set) Token: 0x060080B7 RID: 32951 RVA: 0x000BEE2A File Offset: 0x000BD02A
			public virtual Unlimited<int> BadItemLimit
			{
				set
				{
					base.PowerSharpParameters["BadItemLimit"] = value;
				}
			}

			// Token: 0x1700585B RID: 22619
			// (set) Token: 0x060080B8 RID: 32952 RVA: 0x000BEE42 File Offset: 0x000BD042
			public virtual Unlimited<int> LargeItemLimit
			{
				set
				{
					base.PowerSharpParameters["LargeItemLimit"] = value;
				}
			}

			// Token: 0x1700585C RID: 22620
			// (set) Token: 0x060080B9 RID: 32953 RVA: 0x000BEE5A File Offset: 0x000BD05A
			public virtual SwitchParameter AcceptLargeDataLoss
			{
				set
				{
					base.PowerSharpParameters["AcceptLargeDataLoss"] = value;
				}
			}

			// Token: 0x1700585D RID: 22621
			// (set) Token: 0x060080BA RID: 32954 RVA: 0x000BEE72 File Offset: 0x000BD072
			public virtual string BatchName
			{
				set
				{
					base.PowerSharpParameters["BatchName"] = value;
				}
			}

			// Token: 0x1700585E RID: 22622
			// (set) Token: 0x060080BB RID: 32955 RVA: 0x000BEE85 File Offset: 0x000BD085
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x1700585F RID: 22623
			// (set) Token: 0x060080BC RID: 32956 RVA: 0x000BEE98 File Offset: 0x000BD098
			public virtual SwitchParameter Suspend
			{
				set
				{
					base.PowerSharpParameters["Suspend"] = value;
				}
			}

			// Token: 0x17005860 RID: 22624
			// (set) Token: 0x060080BD RID: 32957 RVA: 0x000BEEB0 File Offset: 0x000BD0B0
			public virtual string SuspendComment
			{
				set
				{
					base.PowerSharpParameters["SuspendComment"] = value;
				}
			}

			// Token: 0x17005861 RID: 22625
			// (set) Token: 0x060080BE RID: 32958 RVA: 0x000BEEC3 File Offset: 0x000BD0C3
			public virtual string Name
			{
				set
				{
					base.PowerSharpParameters["Name"] = value;
				}
			}

			// Token: 0x17005862 RID: 22626
			// (set) Token: 0x060080BF RID: 32959 RVA: 0x000BEED6 File Offset: 0x000BD0D6
			public virtual RequestPriority Priority
			{
				set
				{
					base.PowerSharpParameters["Priority"] = value;
				}
			}

			// Token: 0x17005863 RID: 22627
			// (set) Token: 0x060080C0 RID: 32960 RVA: 0x000BEEEE File Offset: 0x000BD0EE
			public virtual RequestWorkloadType WorkloadType
			{
				set
				{
					base.PowerSharpParameters["WorkloadType"] = value;
				}
			}

			// Token: 0x17005864 RID: 22628
			// (set) Token: 0x060080C1 RID: 32961 RVA: 0x000BEF06 File Offset: 0x000BD106
			public virtual Unlimited<EnhancedTimeSpan> CompletedRequestAgeLimit
			{
				set
				{
					base.PowerSharpParameters["CompletedRequestAgeLimit"] = value;
				}
			}

			// Token: 0x17005865 RID: 22629
			// (set) Token: 0x060080C2 RID: 32962 RVA: 0x000BEF1E File Offset: 0x000BD11E
			public virtual SkippableMergeComponent SkipMerging
			{
				set
				{
					base.PowerSharpParameters["SkipMerging"] = value;
				}
			}

			// Token: 0x17005866 RID: 22630
			// (set) Token: 0x060080C3 RID: 32963 RVA: 0x000BEF36 File Offset: 0x000BD136
			public virtual InternalMrsFlag InternalFlags
			{
				set
				{
					base.PowerSharpParameters["InternalFlags"] = value;
				}
			}

			// Token: 0x17005867 RID: 22631
			// (set) Token: 0x060080C4 RID: 32964 RVA: 0x000BEF4E File Offset: 0x000BD14E
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17005868 RID: 22632
			// (set) Token: 0x060080C5 RID: 32965 RVA: 0x000BEF66 File Offset: 0x000BD166
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17005869 RID: 22633
			// (set) Token: 0x060080C6 RID: 32966 RVA: 0x000BEF7E File Offset: 0x000BD17E
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x1700586A RID: 22634
			// (set) Token: 0x060080C7 RID: 32967 RVA: 0x000BEF96 File Offset: 0x000BD196
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x1700586B RID: 22635
			// (set) Token: 0x060080C8 RID: 32968 RVA: 0x000BEFAE File Offset: 0x000BD1AE
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}

		// Token: 0x02000A0B RID: 2571
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x1700586C RID: 22636
			// (set) Token: 0x060080CA RID: 32970 RVA: 0x000BEFCE File Offset: 0x000BD1CE
			public virtual Unlimited<int> BadItemLimit
			{
				set
				{
					base.PowerSharpParameters["BadItemLimit"] = value;
				}
			}

			// Token: 0x1700586D RID: 22637
			// (set) Token: 0x060080CB RID: 32971 RVA: 0x000BEFE6 File Offset: 0x000BD1E6
			public virtual Unlimited<int> LargeItemLimit
			{
				set
				{
					base.PowerSharpParameters["LargeItemLimit"] = value;
				}
			}

			// Token: 0x1700586E RID: 22638
			// (set) Token: 0x060080CC RID: 32972 RVA: 0x000BEFFE File Offset: 0x000BD1FE
			public virtual SwitchParameter AcceptLargeDataLoss
			{
				set
				{
					base.PowerSharpParameters["AcceptLargeDataLoss"] = value;
				}
			}

			// Token: 0x1700586F RID: 22639
			// (set) Token: 0x060080CD RID: 32973 RVA: 0x000BF016 File Offset: 0x000BD216
			public virtual string BatchName
			{
				set
				{
					base.PowerSharpParameters["BatchName"] = value;
				}
			}

			// Token: 0x17005870 RID: 22640
			// (set) Token: 0x060080CE RID: 32974 RVA: 0x000BF029 File Offset: 0x000BD229
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17005871 RID: 22641
			// (set) Token: 0x060080CF RID: 32975 RVA: 0x000BF03C File Offset: 0x000BD23C
			public virtual SwitchParameter Suspend
			{
				set
				{
					base.PowerSharpParameters["Suspend"] = value;
				}
			}

			// Token: 0x17005872 RID: 22642
			// (set) Token: 0x060080D0 RID: 32976 RVA: 0x000BF054 File Offset: 0x000BD254
			public virtual string SuspendComment
			{
				set
				{
					base.PowerSharpParameters["SuspendComment"] = value;
				}
			}

			// Token: 0x17005873 RID: 22643
			// (set) Token: 0x060080D1 RID: 32977 RVA: 0x000BF067 File Offset: 0x000BD267
			public virtual string Name
			{
				set
				{
					base.PowerSharpParameters["Name"] = value;
				}
			}

			// Token: 0x17005874 RID: 22644
			// (set) Token: 0x060080D2 RID: 32978 RVA: 0x000BF07A File Offset: 0x000BD27A
			public virtual RequestPriority Priority
			{
				set
				{
					base.PowerSharpParameters["Priority"] = value;
				}
			}

			// Token: 0x17005875 RID: 22645
			// (set) Token: 0x060080D3 RID: 32979 RVA: 0x000BF092 File Offset: 0x000BD292
			public virtual RequestWorkloadType WorkloadType
			{
				set
				{
					base.PowerSharpParameters["WorkloadType"] = value;
				}
			}

			// Token: 0x17005876 RID: 22646
			// (set) Token: 0x060080D4 RID: 32980 RVA: 0x000BF0AA File Offset: 0x000BD2AA
			public virtual Unlimited<EnhancedTimeSpan> CompletedRequestAgeLimit
			{
				set
				{
					base.PowerSharpParameters["CompletedRequestAgeLimit"] = value;
				}
			}

			// Token: 0x17005877 RID: 22647
			// (set) Token: 0x060080D5 RID: 32981 RVA: 0x000BF0C2 File Offset: 0x000BD2C2
			public virtual SkippableMergeComponent SkipMerging
			{
				set
				{
					base.PowerSharpParameters["SkipMerging"] = value;
				}
			}

			// Token: 0x17005878 RID: 22648
			// (set) Token: 0x060080D6 RID: 32982 RVA: 0x000BF0DA File Offset: 0x000BD2DA
			public virtual InternalMrsFlag InternalFlags
			{
				set
				{
					base.PowerSharpParameters["InternalFlags"] = value;
				}
			}

			// Token: 0x17005879 RID: 22649
			// (set) Token: 0x060080D7 RID: 32983 RVA: 0x000BF0F2 File Offset: 0x000BD2F2
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x1700587A RID: 22650
			// (set) Token: 0x060080D8 RID: 32984 RVA: 0x000BF10A File Offset: 0x000BD30A
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x1700587B RID: 22651
			// (set) Token: 0x060080D9 RID: 32985 RVA: 0x000BF122 File Offset: 0x000BD322
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x1700587C RID: 22652
			// (set) Token: 0x060080DA RID: 32986 RVA: 0x000BF13A File Offset: 0x000BD33A
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x1700587D RID: 22653
			// (set) Token: 0x060080DB RID: 32987 RVA: 0x000BF152 File Offset: 0x000BD352
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
