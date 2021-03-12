using System;
using System.Management.Automation;
using System.Security;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.MailboxReplicationService;
using Microsoft.Exchange.Management.RecipientTasks;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x02000AB9 RID: 2745
	public class NewSyncRequestCommand : SyntheticCommandWithPipelineInput<SyncRequest, SyncRequest>
	{
		// Token: 0x06008790 RID: 34704 RVA: 0x000C7B82 File Offset: 0x000C5D82
		private NewSyncRequestCommand() : base("New-SyncRequest")
		{
		}

		// Token: 0x06008791 RID: 34705 RVA: 0x000C7B8F File Offset: 0x000C5D8F
		public NewSyncRequestCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x06008792 RID: 34706 RVA: 0x000C7B9E File Offset: 0x000C5D9E
		public virtual NewSyncRequestCommand SetParameters(NewSyncRequestCommand.ImapParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x06008793 RID: 34707 RVA: 0x000C7BA8 File Offset: 0x000C5DA8
		public virtual NewSyncRequestCommand SetParameters(NewSyncRequestCommand.PopParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x06008794 RID: 34708 RVA: 0x000C7BB2 File Offset: 0x000C5DB2
		public virtual NewSyncRequestCommand SetParameters(NewSyncRequestCommand.EasParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x06008795 RID: 34709 RVA: 0x000C7BBC File Offset: 0x000C5DBC
		public virtual NewSyncRequestCommand SetParameters(NewSyncRequestCommand.AutoDetectParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x06008796 RID: 34710 RVA: 0x000C7BC6 File Offset: 0x000C5DC6
		public virtual NewSyncRequestCommand SetParameters(NewSyncRequestCommand.OlcParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x06008797 RID: 34711 RVA: 0x000C7BD0 File Offset: 0x000C5DD0
		public virtual NewSyncRequestCommand SetParameters(NewSyncRequestCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x02000ABA RID: 2746
		public class ImapParameters : ParametersBase
		{
			// Token: 0x17005DD7 RID: 24023
			// (set) Token: 0x06008798 RID: 34712 RVA: 0x000C7BDA File Offset: 0x000C5DDA
			public virtual Guid AggregatedMailboxGuid
			{
				set
				{
					base.PowerSharpParameters["AggregatedMailboxGuid"] = value;
				}
			}

			// Token: 0x17005DD8 RID: 24024
			// (set) Token: 0x06008799 RID: 34713 RVA: 0x000C7BF2 File Offset: 0x000C5DF2
			public virtual string Mailbox
			{
				set
				{
					base.PowerSharpParameters["Mailbox"] = ((value != null) ? new MailboxIdParameter(value) : null);
				}
			}

			// Token: 0x17005DD9 RID: 24025
			// (set) Token: 0x0600879A RID: 34714 RVA: 0x000C7C10 File Offset: 0x000C5E10
			public virtual Fqdn RemoteServerName
			{
				set
				{
					base.PowerSharpParameters["RemoteServerName"] = value;
				}
			}

			// Token: 0x17005DDA RID: 24026
			// (set) Token: 0x0600879B RID: 34715 RVA: 0x000C7C23 File Offset: 0x000C5E23
			public virtual int RemoteServerPort
			{
				set
				{
					base.PowerSharpParameters["RemoteServerPort"] = value;
				}
			}

			// Token: 0x17005DDB RID: 24027
			// (set) Token: 0x0600879C RID: 34716 RVA: 0x000C7C3B File Offset: 0x000C5E3B
			public virtual Fqdn SmtpServerName
			{
				set
				{
					base.PowerSharpParameters["SmtpServerName"] = value;
				}
			}

			// Token: 0x17005DDC RID: 24028
			// (set) Token: 0x0600879D RID: 34717 RVA: 0x000C7C4E File Offset: 0x000C5E4E
			public virtual int SmtpServerPort
			{
				set
				{
					base.PowerSharpParameters["SmtpServerPort"] = value;
				}
			}

			// Token: 0x17005DDD RID: 24029
			// (set) Token: 0x0600879E RID: 34718 RVA: 0x000C7C66 File Offset: 0x000C5E66
			public virtual SmtpAddress RemoteEmailAddress
			{
				set
				{
					base.PowerSharpParameters["RemoteEmailAddress"] = value;
				}
			}

			// Token: 0x17005DDE RID: 24030
			// (set) Token: 0x0600879F RID: 34719 RVA: 0x000C7C7E File Offset: 0x000C5E7E
			public virtual string UserName
			{
				set
				{
					base.PowerSharpParameters["UserName"] = value;
				}
			}

			// Token: 0x17005DDF RID: 24031
			// (set) Token: 0x060087A0 RID: 34720 RVA: 0x000C7C91 File Offset: 0x000C5E91
			public virtual SecureString Password
			{
				set
				{
					base.PowerSharpParameters["Password"] = value;
				}
			}

			// Token: 0x17005DE0 RID: 24032
			// (set) Token: 0x060087A1 RID: 34721 RVA: 0x000C7CA4 File Offset: 0x000C5EA4
			public virtual AuthenticationMethod Authentication
			{
				set
				{
					base.PowerSharpParameters["Authentication"] = value;
				}
			}

			// Token: 0x17005DE1 RID: 24033
			// (set) Token: 0x060087A2 RID: 34722 RVA: 0x000C7CBC File Offset: 0x000C5EBC
			public virtual IMAPSecurityMechanism Security
			{
				set
				{
					base.PowerSharpParameters["Security"] = value;
				}
			}

			// Token: 0x17005DE2 RID: 24034
			// (set) Token: 0x060087A3 RID: 34723 RVA: 0x000C7CD4 File Offset: 0x000C5ED4
			public virtual SwitchParameter Force
			{
				set
				{
					base.PowerSharpParameters["Force"] = value;
				}
			}

			// Token: 0x17005DE3 RID: 24035
			// (set) Token: 0x060087A4 RID: 34724 RVA: 0x000C7CEC File Offset: 0x000C5EEC
			public virtual SwitchParameter Imap
			{
				set
				{
					base.PowerSharpParameters["Imap"] = value;
				}
			}

			// Token: 0x17005DE4 RID: 24036
			// (set) Token: 0x060087A5 RID: 34725 RVA: 0x000C7D04 File Offset: 0x000C5F04
			public virtual string Name
			{
				set
				{
					base.PowerSharpParameters["Name"] = value;
				}
			}

			// Token: 0x17005DE5 RID: 24037
			// (set) Token: 0x060087A6 RID: 34726 RVA: 0x000C7D17 File Offset: 0x000C5F17
			public virtual DateTime StartAfter
			{
				set
				{
					base.PowerSharpParameters["StartAfter"] = value;
				}
			}

			// Token: 0x17005DE6 RID: 24038
			// (set) Token: 0x060087A7 RID: 34727 RVA: 0x000C7D2F File Offset: 0x000C5F2F
			public virtual DateTime CompleteAfter
			{
				set
				{
					base.PowerSharpParameters["CompleteAfter"] = value;
				}
			}

			// Token: 0x17005DE7 RID: 24039
			// (set) Token: 0x060087A8 RID: 34728 RVA: 0x000C7D47 File Offset: 0x000C5F47
			public virtual TimeSpan IncrementalSyncInterval
			{
				set
				{
					base.PowerSharpParameters["IncrementalSyncInterval"] = value;
				}
			}

			// Token: 0x17005DE8 RID: 24040
			// (set) Token: 0x060087A9 RID: 34729 RVA: 0x000C7D5F File Offset: 0x000C5F5F
			public virtual string TargetRootFolder
			{
				set
				{
					base.PowerSharpParameters["TargetRootFolder"] = value;
				}
			}

			// Token: 0x17005DE9 RID: 24041
			// (set) Token: 0x060087AA RID: 34730 RVA: 0x000C7D72 File Offset: 0x000C5F72
			public virtual SwitchParameter ForestWideDomainControllerAffinityByExecutingUser
			{
				set
				{
					base.PowerSharpParameters["ForestWideDomainControllerAffinityByExecutingUser"] = value;
				}
			}

			// Token: 0x17005DEA RID: 24042
			// (set) Token: 0x060087AB RID: 34731 RVA: 0x000C7D8A File Offset: 0x000C5F8A
			public virtual Unlimited<int> BadItemLimit
			{
				set
				{
					base.PowerSharpParameters["BadItemLimit"] = value;
				}
			}

			// Token: 0x17005DEB RID: 24043
			// (set) Token: 0x060087AC RID: 34732 RVA: 0x000C7DA2 File Offset: 0x000C5FA2
			public virtual Unlimited<int> LargeItemLimit
			{
				set
				{
					base.PowerSharpParameters["LargeItemLimit"] = value;
				}
			}

			// Token: 0x17005DEC RID: 24044
			// (set) Token: 0x060087AD RID: 34733 RVA: 0x000C7DBA File Offset: 0x000C5FBA
			public virtual SwitchParameter AcceptLargeDataLoss
			{
				set
				{
					base.PowerSharpParameters["AcceptLargeDataLoss"] = value;
				}
			}

			// Token: 0x17005DED RID: 24045
			// (set) Token: 0x060087AE RID: 34734 RVA: 0x000C7DD2 File Offset: 0x000C5FD2
			public virtual string BatchName
			{
				set
				{
					base.PowerSharpParameters["BatchName"] = value;
				}
			}

			// Token: 0x17005DEE RID: 24046
			// (set) Token: 0x060087AF RID: 34735 RVA: 0x000C7DE5 File Offset: 0x000C5FE5
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17005DEF RID: 24047
			// (set) Token: 0x060087B0 RID: 34736 RVA: 0x000C7DF8 File Offset: 0x000C5FF8
			public virtual SwitchParameter Suspend
			{
				set
				{
					base.PowerSharpParameters["Suspend"] = value;
				}
			}

			// Token: 0x17005DF0 RID: 24048
			// (set) Token: 0x060087B1 RID: 34737 RVA: 0x000C7E10 File Offset: 0x000C6010
			public virtual string SuspendComment
			{
				set
				{
					base.PowerSharpParameters["SuspendComment"] = value;
				}
			}

			// Token: 0x17005DF1 RID: 24049
			// (set) Token: 0x060087B2 RID: 34738 RVA: 0x000C7E23 File Offset: 0x000C6023
			public virtual RequestPriority Priority
			{
				set
				{
					base.PowerSharpParameters["Priority"] = value;
				}
			}

			// Token: 0x17005DF2 RID: 24050
			// (set) Token: 0x060087B3 RID: 34739 RVA: 0x000C7E3B File Offset: 0x000C603B
			public virtual RequestWorkloadType WorkloadType
			{
				set
				{
					base.PowerSharpParameters["WorkloadType"] = value;
				}
			}

			// Token: 0x17005DF3 RID: 24051
			// (set) Token: 0x060087B4 RID: 34740 RVA: 0x000C7E53 File Offset: 0x000C6053
			public virtual Unlimited<EnhancedTimeSpan> CompletedRequestAgeLimit
			{
				set
				{
					base.PowerSharpParameters["CompletedRequestAgeLimit"] = value;
				}
			}

			// Token: 0x17005DF4 RID: 24052
			// (set) Token: 0x060087B5 RID: 34741 RVA: 0x000C7E6B File Offset: 0x000C606B
			public virtual SkippableMergeComponent SkipMerging
			{
				set
				{
					base.PowerSharpParameters["SkipMerging"] = value;
				}
			}

			// Token: 0x17005DF5 RID: 24053
			// (set) Token: 0x060087B6 RID: 34742 RVA: 0x000C7E83 File Offset: 0x000C6083
			public virtual InternalMrsFlag InternalFlags
			{
				set
				{
					base.PowerSharpParameters["InternalFlags"] = value;
				}
			}

			// Token: 0x17005DF6 RID: 24054
			// (set) Token: 0x060087B7 RID: 34743 RVA: 0x000C7E9B File Offset: 0x000C609B
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17005DF7 RID: 24055
			// (set) Token: 0x060087B8 RID: 34744 RVA: 0x000C7EB3 File Offset: 0x000C60B3
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17005DF8 RID: 24056
			// (set) Token: 0x060087B9 RID: 34745 RVA: 0x000C7ECB File Offset: 0x000C60CB
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17005DF9 RID: 24057
			// (set) Token: 0x060087BA RID: 34746 RVA: 0x000C7EE3 File Offset: 0x000C60E3
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17005DFA RID: 24058
			// (set) Token: 0x060087BB RID: 34747 RVA: 0x000C7EFB File Offset: 0x000C60FB
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}

		// Token: 0x02000ABB RID: 2747
		public class PopParameters : ParametersBase
		{
			// Token: 0x17005DFB RID: 24059
			// (set) Token: 0x060087BD RID: 34749 RVA: 0x000C7F1B File Offset: 0x000C611B
			public virtual Guid AggregatedMailboxGuid
			{
				set
				{
					base.PowerSharpParameters["AggregatedMailboxGuid"] = value;
				}
			}

			// Token: 0x17005DFC RID: 24060
			// (set) Token: 0x060087BE RID: 34750 RVA: 0x000C7F33 File Offset: 0x000C6133
			public virtual string Mailbox
			{
				set
				{
					base.PowerSharpParameters["Mailbox"] = ((value != null) ? new MailboxIdParameter(value) : null);
				}
			}

			// Token: 0x17005DFD RID: 24061
			// (set) Token: 0x060087BF RID: 34751 RVA: 0x000C7F51 File Offset: 0x000C6151
			public virtual Fqdn RemoteServerName
			{
				set
				{
					base.PowerSharpParameters["RemoteServerName"] = value;
				}
			}

			// Token: 0x17005DFE RID: 24062
			// (set) Token: 0x060087C0 RID: 34752 RVA: 0x000C7F64 File Offset: 0x000C6164
			public virtual int RemoteServerPort
			{
				set
				{
					base.PowerSharpParameters["RemoteServerPort"] = value;
				}
			}

			// Token: 0x17005DFF RID: 24063
			// (set) Token: 0x060087C1 RID: 34753 RVA: 0x000C7F7C File Offset: 0x000C617C
			public virtual Fqdn SmtpServerName
			{
				set
				{
					base.PowerSharpParameters["SmtpServerName"] = value;
				}
			}

			// Token: 0x17005E00 RID: 24064
			// (set) Token: 0x060087C2 RID: 34754 RVA: 0x000C7F8F File Offset: 0x000C618F
			public virtual int SmtpServerPort
			{
				set
				{
					base.PowerSharpParameters["SmtpServerPort"] = value;
				}
			}

			// Token: 0x17005E01 RID: 24065
			// (set) Token: 0x060087C3 RID: 34755 RVA: 0x000C7FA7 File Offset: 0x000C61A7
			public virtual SmtpAddress RemoteEmailAddress
			{
				set
				{
					base.PowerSharpParameters["RemoteEmailAddress"] = value;
				}
			}

			// Token: 0x17005E02 RID: 24066
			// (set) Token: 0x060087C4 RID: 34756 RVA: 0x000C7FBF File Offset: 0x000C61BF
			public virtual string UserName
			{
				set
				{
					base.PowerSharpParameters["UserName"] = value;
				}
			}

			// Token: 0x17005E03 RID: 24067
			// (set) Token: 0x060087C5 RID: 34757 RVA: 0x000C7FD2 File Offset: 0x000C61D2
			public virtual SecureString Password
			{
				set
				{
					base.PowerSharpParameters["Password"] = value;
				}
			}

			// Token: 0x17005E04 RID: 24068
			// (set) Token: 0x060087C6 RID: 34758 RVA: 0x000C7FE5 File Offset: 0x000C61E5
			public virtual AuthenticationMethod Authentication
			{
				set
				{
					base.PowerSharpParameters["Authentication"] = value;
				}
			}

			// Token: 0x17005E05 RID: 24069
			// (set) Token: 0x060087C7 RID: 34759 RVA: 0x000C7FFD File Offset: 0x000C61FD
			public virtual IMAPSecurityMechanism Security
			{
				set
				{
					base.PowerSharpParameters["Security"] = value;
				}
			}

			// Token: 0x17005E06 RID: 24070
			// (set) Token: 0x060087C8 RID: 34760 RVA: 0x000C8015 File Offset: 0x000C6215
			public virtual SwitchParameter Force
			{
				set
				{
					base.PowerSharpParameters["Force"] = value;
				}
			}

			// Token: 0x17005E07 RID: 24071
			// (set) Token: 0x060087C9 RID: 34761 RVA: 0x000C802D File Offset: 0x000C622D
			public virtual SwitchParameter Pop
			{
				set
				{
					base.PowerSharpParameters["Pop"] = value;
				}
			}

			// Token: 0x17005E08 RID: 24072
			// (set) Token: 0x060087CA RID: 34762 RVA: 0x000C8045 File Offset: 0x000C6245
			public virtual string Name
			{
				set
				{
					base.PowerSharpParameters["Name"] = value;
				}
			}

			// Token: 0x17005E09 RID: 24073
			// (set) Token: 0x060087CB RID: 34763 RVA: 0x000C8058 File Offset: 0x000C6258
			public virtual DateTime StartAfter
			{
				set
				{
					base.PowerSharpParameters["StartAfter"] = value;
				}
			}

			// Token: 0x17005E0A RID: 24074
			// (set) Token: 0x060087CC RID: 34764 RVA: 0x000C8070 File Offset: 0x000C6270
			public virtual DateTime CompleteAfter
			{
				set
				{
					base.PowerSharpParameters["CompleteAfter"] = value;
				}
			}

			// Token: 0x17005E0B RID: 24075
			// (set) Token: 0x060087CD RID: 34765 RVA: 0x000C8088 File Offset: 0x000C6288
			public virtual TimeSpan IncrementalSyncInterval
			{
				set
				{
					base.PowerSharpParameters["IncrementalSyncInterval"] = value;
				}
			}

			// Token: 0x17005E0C RID: 24076
			// (set) Token: 0x060087CE RID: 34766 RVA: 0x000C80A0 File Offset: 0x000C62A0
			public virtual string TargetRootFolder
			{
				set
				{
					base.PowerSharpParameters["TargetRootFolder"] = value;
				}
			}

			// Token: 0x17005E0D RID: 24077
			// (set) Token: 0x060087CF RID: 34767 RVA: 0x000C80B3 File Offset: 0x000C62B3
			public virtual SwitchParameter ForestWideDomainControllerAffinityByExecutingUser
			{
				set
				{
					base.PowerSharpParameters["ForestWideDomainControllerAffinityByExecutingUser"] = value;
				}
			}

			// Token: 0x17005E0E RID: 24078
			// (set) Token: 0x060087D0 RID: 34768 RVA: 0x000C80CB File Offset: 0x000C62CB
			public virtual Unlimited<int> BadItemLimit
			{
				set
				{
					base.PowerSharpParameters["BadItemLimit"] = value;
				}
			}

			// Token: 0x17005E0F RID: 24079
			// (set) Token: 0x060087D1 RID: 34769 RVA: 0x000C80E3 File Offset: 0x000C62E3
			public virtual Unlimited<int> LargeItemLimit
			{
				set
				{
					base.PowerSharpParameters["LargeItemLimit"] = value;
				}
			}

			// Token: 0x17005E10 RID: 24080
			// (set) Token: 0x060087D2 RID: 34770 RVA: 0x000C80FB File Offset: 0x000C62FB
			public virtual SwitchParameter AcceptLargeDataLoss
			{
				set
				{
					base.PowerSharpParameters["AcceptLargeDataLoss"] = value;
				}
			}

			// Token: 0x17005E11 RID: 24081
			// (set) Token: 0x060087D3 RID: 34771 RVA: 0x000C8113 File Offset: 0x000C6313
			public virtual string BatchName
			{
				set
				{
					base.PowerSharpParameters["BatchName"] = value;
				}
			}

			// Token: 0x17005E12 RID: 24082
			// (set) Token: 0x060087D4 RID: 34772 RVA: 0x000C8126 File Offset: 0x000C6326
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17005E13 RID: 24083
			// (set) Token: 0x060087D5 RID: 34773 RVA: 0x000C8139 File Offset: 0x000C6339
			public virtual SwitchParameter Suspend
			{
				set
				{
					base.PowerSharpParameters["Suspend"] = value;
				}
			}

			// Token: 0x17005E14 RID: 24084
			// (set) Token: 0x060087D6 RID: 34774 RVA: 0x000C8151 File Offset: 0x000C6351
			public virtual string SuspendComment
			{
				set
				{
					base.PowerSharpParameters["SuspendComment"] = value;
				}
			}

			// Token: 0x17005E15 RID: 24085
			// (set) Token: 0x060087D7 RID: 34775 RVA: 0x000C8164 File Offset: 0x000C6364
			public virtual RequestPriority Priority
			{
				set
				{
					base.PowerSharpParameters["Priority"] = value;
				}
			}

			// Token: 0x17005E16 RID: 24086
			// (set) Token: 0x060087D8 RID: 34776 RVA: 0x000C817C File Offset: 0x000C637C
			public virtual RequestWorkloadType WorkloadType
			{
				set
				{
					base.PowerSharpParameters["WorkloadType"] = value;
				}
			}

			// Token: 0x17005E17 RID: 24087
			// (set) Token: 0x060087D9 RID: 34777 RVA: 0x000C8194 File Offset: 0x000C6394
			public virtual Unlimited<EnhancedTimeSpan> CompletedRequestAgeLimit
			{
				set
				{
					base.PowerSharpParameters["CompletedRequestAgeLimit"] = value;
				}
			}

			// Token: 0x17005E18 RID: 24088
			// (set) Token: 0x060087DA RID: 34778 RVA: 0x000C81AC File Offset: 0x000C63AC
			public virtual SkippableMergeComponent SkipMerging
			{
				set
				{
					base.PowerSharpParameters["SkipMerging"] = value;
				}
			}

			// Token: 0x17005E19 RID: 24089
			// (set) Token: 0x060087DB RID: 34779 RVA: 0x000C81C4 File Offset: 0x000C63C4
			public virtual InternalMrsFlag InternalFlags
			{
				set
				{
					base.PowerSharpParameters["InternalFlags"] = value;
				}
			}

			// Token: 0x17005E1A RID: 24090
			// (set) Token: 0x060087DC RID: 34780 RVA: 0x000C81DC File Offset: 0x000C63DC
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17005E1B RID: 24091
			// (set) Token: 0x060087DD RID: 34781 RVA: 0x000C81F4 File Offset: 0x000C63F4
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17005E1C RID: 24092
			// (set) Token: 0x060087DE RID: 34782 RVA: 0x000C820C File Offset: 0x000C640C
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17005E1D RID: 24093
			// (set) Token: 0x060087DF RID: 34783 RVA: 0x000C8224 File Offset: 0x000C6424
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17005E1E RID: 24094
			// (set) Token: 0x060087E0 RID: 34784 RVA: 0x000C823C File Offset: 0x000C643C
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}

		// Token: 0x02000ABC RID: 2748
		public class EasParameters : ParametersBase
		{
			// Token: 0x17005E1F RID: 24095
			// (set) Token: 0x060087E2 RID: 34786 RVA: 0x000C825C File Offset: 0x000C645C
			public virtual Guid AggregatedMailboxGuid
			{
				set
				{
					base.PowerSharpParameters["AggregatedMailboxGuid"] = value;
				}
			}

			// Token: 0x17005E20 RID: 24096
			// (set) Token: 0x060087E3 RID: 34787 RVA: 0x000C8274 File Offset: 0x000C6474
			public virtual string Mailbox
			{
				set
				{
					base.PowerSharpParameters["Mailbox"] = ((value != null) ? new MailboxIdParameter(value) : null);
				}
			}

			// Token: 0x17005E21 RID: 24097
			// (set) Token: 0x060087E4 RID: 34788 RVA: 0x000C8292 File Offset: 0x000C6492
			public virtual Fqdn RemoteServerName
			{
				set
				{
					base.PowerSharpParameters["RemoteServerName"] = value;
				}
			}

			// Token: 0x17005E22 RID: 24098
			// (set) Token: 0x060087E5 RID: 34789 RVA: 0x000C82A5 File Offset: 0x000C64A5
			public virtual Fqdn SmtpServerName
			{
				set
				{
					base.PowerSharpParameters["SmtpServerName"] = value;
				}
			}

			// Token: 0x17005E23 RID: 24099
			// (set) Token: 0x060087E6 RID: 34790 RVA: 0x000C82B8 File Offset: 0x000C64B8
			public virtual int SmtpServerPort
			{
				set
				{
					base.PowerSharpParameters["SmtpServerPort"] = value;
				}
			}

			// Token: 0x17005E24 RID: 24100
			// (set) Token: 0x060087E7 RID: 34791 RVA: 0x000C82D0 File Offset: 0x000C64D0
			public virtual SmtpAddress RemoteEmailAddress
			{
				set
				{
					base.PowerSharpParameters["RemoteEmailAddress"] = value;
				}
			}

			// Token: 0x17005E25 RID: 24101
			// (set) Token: 0x060087E8 RID: 34792 RVA: 0x000C82E8 File Offset: 0x000C64E8
			public virtual string UserName
			{
				set
				{
					base.PowerSharpParameters["UserName"] = value;
				}
			}

			// Token: 0x17005E26 RID: 24102
			// (set) Token: 0x060087E9 RID: 34793 RVA: 0x000C82FB File Offset: 0x000C64FB
			public virtual SecureString Password
			{
				set
				{
					base.PowerSharpParameters["Password"] = value;
				}
			}

			// Token: 0x17005E27 RID: 24103
			// (set) Token: 0x060087EA RID: 34794 RVA: 0x000C830E File Offset: 0x000C650E
			public virtual SwitchParameter Force
			{
				set
				{
					base.PowerSharpParameters["Force"] = value;
				}
			}

			// Token: 0x17005E28 RID: 24104
			// (set) Token: 0x060087EB RID: 34795 RVA: 0x000C8326 File Offset: 0x000C6526
			public virtual SwitchParameter Eas
			{
				set
				{
					base.PowerSharpParameters["Eas"] = value;
				}
			}

			// Token: 0x17005E29 RID: 24105
			// (set) Token: 0x060087EC RID: 34796 RVA: 0x000C833E File Offset: 0x000C653E
			public virtual string Name
			{
				set
				{
					base.PowerSharpParameters["Name"] = value;
				}
			}

			// Token: 0x17005E2A RID: 24106
			// (set) Token: 0x060087ED RID: 34797 RVA: 0x000C8351 File Offset: 0x000C6551
			public virtual DateTime StartAfter
			{
				set
				{
					base.PowerSharpParameters["StartAfter"] = value;
				}
			}

			// Token: 0x17005E2B RID: 24107
			// (set) Token: 0x060087EE RID: 34798 RVA: 0x000C8369 File Offset: 0x000C6569
			public virtual DateTime CompleteAfter
			{
				set
				{
					base.PowerSharpParameters["CompleteAfter"] = value;
				}
			}

			// Token: 0x17005E2C RID: 24108
			// (set) Token: 0x060087EF RID: 34799 RVA: 0x000C8381 File Offset: 0x000C6581
			public virtual TimeSpan IncrementalSyncInterval
			{
				set
				{
					base.PowerSharpParameters["IncrementalSyncInterval"] = value;
				}
			}

			// Token: 0x17005E2D RID: 24109
			// (set) Token: 0x060087F0 RID: 34800 RVA: 0x000C8399 File Offset: 0x000C6599
			public virtual string TargetRootFolder
			{
				set
				{
					base.PowerSharpParameters["TargetRootFolder"] = value;
				}
			}

			// Token: 0x17005E2E RID: 24110
			// (set) Token: 0x060087F1 RID: 34801 RVA: 0x000C83AC File Offset: 0x000C65AC
			public virtual SwitchParameter ForestWideDomainControllerAffinityByExecutingUser
			{
				set
				{
					base.PowerSharpParameters["ForestWideDomainControllerAffinityByExecutingUser"] = value;
				}
			}

			// Token: 0x17005E2F RID: 24111
			// (set) Token: 0x060087F2 RID: 34802 RVA: 0x000C83C4 File Offset: 0x000C65C4
			public virtual Unlimited<int> BadItemLimit
			{
				set
				{
					base.PowerSharpParameters["BadItemLimit"] = value;
				}
			}

			// Token: 0x17005E30 RID: 24112
			// (set) Token: 0x060087F3 RID: 34803 RVA: 0x000C83DC File Offset: 0x000C65DC
			public virtual Unlimited<int> LargeItemLimit
			{
				set
				{
					base.PowerSharpParameters["LargeItemLimit"] = value;
				}
			}

			// Token: 0x17005E31 RID: 24113
			// (set) Token: 0x060087F4 RID: 34804 RVA: 0x000C83F4 File Offset: 0x000C65F4
			public virtual SwitchParameter AcceptLargeDataLoss
			{
				set
				{
					base.PowerSharpParameters["AcceptLargeDataLoss"] = value;
				}
			}

			// Token: 0x17005E32 RID: 24114
			// (set) Token: 0x060087F5 RID: 34805 RVA: 0x000C840C File Offset: 0x000C660C
			public virtual string BatchName
			{
				set
				{
					base.PowerSharpParameters["BatchName"] = value;
				}
			}

			// Token: 0x17005E33 RID: 24115
			// (set) Token: 0x060087F6 RID: 34806 RVA: 0x000C841F File Offset: 0x000C661F
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17005E34 RID: 24116
			// (set) Token: 0x060087F7 RID: 34807 RVA: 0x000C8432 File Offset: 0x000C6632
			public virtual SwitchParameter Suspend
			{
				set
				{
					base.PowerSharpParameters["Suspend"] = value;
				}
			}

			// Token: 0x17005E35 RID: 24117
			// (set) Token: 0x060087F8 RID: 34808 RVA: 0x000C844A File Offset: 0x000C664A
			public virtual string SuspendComment
			{
				set
				{
					base.PowerSharpParameters["SuspendComment"] = value;
				}
			}

			// Token: 0x17005E36 RID: 24118
			// (set) Token: 0x060087F9 RID: 34809 RVA: 0x000C845D File Offset: 0x000C665D
			public virtual RequestPriority Priority
			{
				set
				{
					base.PowerSharpParameters["Priority"] = value;
				}
			}

			// Token: 0x17005E37 RID: 24119
			// (set) Token: 0x060087FA RID: 34810 RVA: 0x000C8475 File Offset: 0x000C6675
			public virtual RequestWorkloadType WorkloadType
			{
				set
				{
					base.PowerSharpParameters["WorkloadType"] = value;
				}
			}

			// Token: 0x17005E38 RID: 24120
			// (set) Token: 0x060087FB RID: 34811 RVA: 0x000C848D File Offset: 0x000C668D
			public virtual Unlimited<EnhancedTimeSpan> CompletedRequestAgeLimit
			{
				set
				{
					base.PowerSharpParameters["CompletedRequestAgeLimit"] = value;
				}
			}

			// Token: 0x17005E39 RID: 24121
			// (set) Token: 0x060087FC RID: 34812 RVA: 0x000C84A5 File Offset: 0x000C66A5
			public virtual SkippableMergeComponent SkipMerging
			{
				set
				{
					base.PowerSharpParameters["SkipMerging"] = value;
				}
			}

			// Token: 0x17005E3A RID: 24122
			// (set) Token: 0x060087FD RID: 34813 RVA: 0x000C84BD File Offset: 0x000C66BD
			public virtual InternalMrsFlag InternalFlags
			{
				set
				{
					base.PowerSharpParameters["InternalFlags"] = value;
				}
			}

			// Token: 0x17005E3B RID: 24123
			// (set) Token: 0x060087FE RID: 34814 RVA: 0x000C84D5 File Offset: 0x000C66D5
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17005E3C RID: 24124
			// (set) Token: 0x060087FF RID: 34815 RVA: 0x000C84ED File Offset: 0x000C66ED
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17005E3D RID: 24125
			// (set) Token: 0x06008800 RID: 34816 RVA: 0x000C8505 File Offset: 0x000C6705
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17005E3E RID: 24126
			// (set) Token: 0x06008801 RID: 34817 RVA: 0x000C851D File Offset: 0x000C671D
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17005E3F RID: 24127
			// (set) Token: 0x06008802 RID: 34818 RVA: 0x000C8535 File Offset: 0x000C6735
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}

		// Token: 0x02000ABD RID: 2749
		public class AutoDetectParameters : ParametersBase
		{
			// Token: 0x17005E40 RID: 24128
			// (set) Token: 0x06008804 RID: 34820 RVA: 0x000C8555 File Offset: 0x000C6755
			public virtual Guid AggregatedMailboxGuid
			{
				set
				{
					base.PowerSharpParameters["AggregatedMailboxGuid"] = value;
				}
			}

			// Token: 0x17005E41 RID: 24129
			// (set) Token: 0x06008805 RID: 34821 RVA: 0x000C856D File Offset: 0x000C676D
			public virtual string Mailbox
			{
				set
				{
					base.PowerSharpParameters["Mailbox"] = ((value != null) ? new MailboxIdParameter(value) : null);
				}
			}

			// Token: 0x17005E42 RID: 24130
			// (set) Token: 0x06008806 RID: 34822 RVA: 0x000C858B File Offset: 0x000C678B
			public virtual SmtpAddress RemoteEmailAddress
			{
				set
				{
					base.PowerSharpParameters["RemoteEmailAddress"] = value;
				}
			}

			// Token: 0x17005E43 RID: 24131
			// (set) Token: 0x06008807 RID: 34823 RVA: 0x000C85A3 File Offset: 0x000C67A3
			public virtual string UserName
			{
				set
				{
					base.PowerSharpParameters["UserName"] = value;
				}
			}

			// Token: 0x17005E44 RID: 24132
			// (set) Token: 0x06008808 RID: 34824 RVA: 0x000C85B6 File Offset: 0x000C67B6
			public virtual SecureString Password
			{
				set
				{
					base.PowerSharpParameters["Password"] = value;
				}
			}

			// Token: 0x17005E45 RID: 24133
			// (set) Token: 0x06008809 RID: 34825 RVA: 0x000C85C9 File Offset: 0x000C67C9
			public virtual SwitchParameter Force
			{
				set
				{
					base.PowerSharpParameters["Force"] = value;
				}
			}

			// Token: 0x17005E46 RID: 24134
			// (set) Token: 0x0600880A RID: 34826 RVA: 0x000C85E1 File Offset: 0x000C67E1
			public virtual string Name
			{
				set
				{
					base.PowerSharpParameters["Name"] = value;
				}
			}

			// Token: 0x17005E47 RID: 24135
			// (set) Token: 0x0600880B RID: 34827 RVA: 0x000C85F4 File Offset: 0x000C67F4
			public virtual DateTime StartAfter
			{
				set
				{
					base.PowerSharpParameters["StartAfter"] = value;
				}
			}

			// Token: 0x17005E48 RID: 24136
			// (set) Token: 0x0600880C RID: 34828 RVA: 0x000C860C File Offset: 0x000C680C
			public virtual DateTime CompleteAfter
			{
				set
				{
					base.PowerSharpParameters["CompleteAfter"] = value;
				}
			}

			// Token: 0x17005E49 RID: 24137
			// (set) Token: 0x0600880D RID: 34829 RVA: 0x000C8624 File Offset: 0x000C6824
			public virtual TimeSpan IncrementalSyncInterval
			{
				set
				{
					base.PowerSharpParameters["IncrementalSyncInterval"] = value;
				}
			}

			// Token: 0x17005E4A RID: 24138
			// (set) Token: 0x0600880E RID: 34830 RVA: 0x000C863C File Offset: 0x000C683C
			public virtual string TargetRootFolder
			{
				set
				{
					base.PowerSharpParameters["TargetRootFolder"] = value;
				}
			}

			// Token: 0x17005E4B RID: 24139
			// (set) Token: 0x0600880F RID: 34831 RVA: 0x000C864F File Offset: 0x000C684F
			public virtual SwitchParameter ForestWideDomainControllerAffinityByExecutingUser
			{
				set
				{
					base.PowerSharpParameters["ForestWideDomainControllerAffinityByExecutingUser"] = value;
				}
			}

			// Token: 0x17005E4C RID: 24140
			// (set) Token: 0x06008810 RID: 34832 RVA: 0x000C8667 File Offset: 0x000C6867
			public virtual Unlimited<int> BadItemLimit
			{
				set
				{
					base.PowerSharpParameters["BadItemLimit"] = value;
				}
			}

			// Token: 0x17005E4D RID: 24141
			// (set) Token: 0x06008811 RID: 34833 RVA: 0x000C867F File Offset: 0x000C687F
			public virtual Unlimited<int> LargeItemLimit
			{
				set
				{
					base.PowerSharpParameters["LargeItemLimit"] = value;
				}
			}

			// Token: 0x17005E4E RID: 24142
			// (set) Token: 0x06008812 RID: 34834 RVA: 0x000C8697 File Offset: 0x000C6897
			public virtual SwitchParameter AcceptLargeDataLoss
			{
				set
				{
					base.PowerSharpParameters["AcceptLargeDataLoss"] = value;
				}
			}

			// Token: 0x17005E4F RID: 24143
			// (set) Token: 0x06008813 RID: 34835 RVA: 0x000C86AF File Offset: 0x000C68AF
			public virtual string BatchName
			{
				set
				{
					base.PowerSharpParameters["BatchName"] = value;
				}
			}

			// Token: 0x17005E50 RID: 24144
			// (set) Token: 0x06008814 RID: 34836 RVA: 0x000C86C2 File Offset: 0x000C68C2
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17005E51 RID: 24145
			// (set) Token: 0x06008815 RID: 34837 RVA: 0x000C86D5 File Offset: 0x000C68D5
			public virtual SwitchParameter Suspend
			{
				set
				{
					base.PowerSharpParameters["Suspend"] = value;
				}
			}

			// Token: 0x17005E52 RID: 24146
			// (set) Token: 0x06008816 RID: 34838 RVA: 0x000C86ED File Offset: 0x000C68ED
			public virtual string SuspendComment
			{
				set
				{
					base.PowerSharpParameters["SuspendComment"] = value;
				}
			}

			// Token: 0x17005E53 RID: 24147
			// (set) Token: 0x06008817 RID: 34839 RVA: 0x000C8700 File Offset: 0x000C6900
			public virtual RequestPriority Priority
			{
				set
				{
					base.PowerSharpParameters["Priority"] = value;
				}
			}

			// Token: 0x17005E54 RID: 24148
			// (set) Token: 0x06008818 RID: 34840 RVA: 0x000C8718 File Offset: 0x000C6918
			public virtual RequestWorkloadType WorkloadType
			{
				set
				{
					base.PowerSharpParameters["WorkloadType"] = value;
				}
			}

			// Token: 0x17005E55 RID: 24149
			// (set) Token: 0x06008819 RID: 34841 RVA: 0x000C8730 File Offset: 0x000C6930
			public virtual Unlimited<EnhancedTimeSpan> CompletedRequestAgeLimit
			{
				set
				{
					base.PowerSharpParameters["CompletedRequestAgeLimit"] = value;
				}
			}

			// Token: 0x17005E56 RID: 24150
			// (set) Token: 0x0600881A RID: 34842 RVA: 0x000C8748 File Offset: 0x000C6948
			public virtual SkippableMergeComponent SkipMerging
			{
				set
				{
					base.PowerSharpParameters["SkipMerging"] = value;
				}
			}

			// Token: 0x17005E57 RID: 24151
			// (set) Token: 0x0600881B RID: 34843 RVA: 0x000C8760 File Offset: 0x000C6960
			public virtual InternalMrsFlag InternalFlags
			{
				set
				{
					base.PowerSharpParameters["InternalFlags"] = value;
				}
			}

			// Token: 0x17005E58 RID: 24152
			// (set) Token: 0x0600881C RID: 34844 RVA: 0x000C8778 File Offset: 0x000C6978
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17005E59 RID: 24153
			// (set) Token: 0x0600881D RID: 34845 RVA: 0x000C8790 File Offset: 0x000C6990
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17005E5A RID: 24154
			// (set) Token: 0x0600881E RID: 34846 RVA: 0x000C87A8 File Offset: 0x000C69A8
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17005E5B RID: 24155
			// (set) Token: 0x0600881F RID: 34847 RVA: 0x000C87C0 File Offset: 0x000C69C0
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17005E5C RID: 24156
			// (set) Token: 0x06008820 RID: 34848 RVA: 0x000C87D8 File Offset: 0x000C69D8
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}

		// Token: 0x02000ABE RID: 2750
		public class OlcParameters : ParametersBase
		{
			// Token: 0x17005E5D RID: 24157
			// (set) Token: 0x06008822 RID: 34850 RVA: 0x000C87F8 File Offset: 0x000C69F8
			public virtual string Mailbox
			{
				set
				{
					base.PowerSharpParameters["Mailbox"] = ((value != null) ? new MailboxIdParameter(value) : null);
				}
			}

			// Token: 0x17005E5E RID: 24158
			// (set) Token: 0x06008823 RID: 34851 RVA: 0x000C8816 File Offset: 0x000C6A16
			public virtual Fqdn RemoteServerName
			{
				set
				{
					base.PowerSharpParameters["RemoteServerName"] = value;
				}
			}

			// Token: 0x17005E5F RID: 24159
			// (set) Token: 0x06008824 RID: 34852 RVA: 0x000C8829 File Offset: 0x000C6A29
			public virtual int RemoteServerPort
			{
				set
				{
					base.PowerSharpParameters["RemoteServerPort"] = value;
				}
			}

			// Token: 0x17005E60 RID: 24160
			// (set) Token: 0x06008825 RID: 34853 RVA: 0x000C8841 File Offset: 0x000C6A41
			public virtual SwitchParameter Force
			{
				set
				{
					base.PowerSharpParameters["Force"] = value;
				}
			}

			// Token: 0x17005E61 RID: 24161
			// (set) Token: 0x06008826 RID: 34854 RVA: 0x000C8859 File Offset: 0x000C6A59
			public virtual SwitchParameter Olc
			{
				set
				{
					base.PowerSharpParameters["Olc"] = value;
				}
			}

			// Token: 0x17005E62 RID: 24162
			// (set) Token: 0x06008827 RID: 34855 RVA: 0x000C8871 File Offset: 0x000C6A71
			public virtual long? Puid
			{
				set
				{
					base.PowerSharpParameters["Puid"] = value;
				}
			}

			// Token: 0x17005E63 RID: 24163
			// (set) Token: 0x06008828 RID: 34856 RVA: 0x000C8889 File Offset: 0x000C6A89
			public virtual int? DGroup
			{
				set
				{
					base.PowerSharpParameters["DGroup"] = value;
				}
			}

			// Token: 0x17005E64 RID: 24164
			// (set) Token: 0x06008829 RID: 34857 RVA: 0x000C88A1 File Offset: 0x000C6AA1
			public virtual string TargetRootFolder
			{
				set
				{
					base.PowerSharpParameters["TargetRootFolder"] = value;
				}
			}

			// Token: 0x17005E65 RID: 24165
			// (set) Token: 0x0600882A RID: 34858 RVA: 0x000C88B4 File Offset: 0x000C6AB4
			public virtual SwitchParameter ForestWideDomainControllerAffinityByExecutingUser
			{
				set
				{
					base.PowerSharpParameters["ForestWideDomainControllerAffinityByExecutingUser"] = value;
				}
			}

			// Token: 0x17005E66 RID: 24166
			// (set) Token: 0x0600882B RID: 34859 RVA: 0x000C88CC File Offset: 0x000C6ACC
			public virtual Unlimited<int> BadItemLimit
			{
				set
				{
					base.PowerSharpParameters["BadItemLimit"] = value;
				}
			}

			// Token: 0x17005E67 RID: 24167
			// (set) Token: 0x0600882C RID: 34860 RVA: 0x000C88E4 File Offset: 0x000C6AE4
			public virtual Unlimited<int> LargeItemLimit
			{
				set
				{
					base.PowerSharpParameters["LargeItemLimit"] = value;
				}
			}

			// Token: 0x17005E68 RID: 24168
			// (set) Token: 0x0600882D RID: 34861 RVA: 0x000C88FC File Offset: 0x000C6AFC
			public virtual SwitchParameter AcceptLargeDataLoss
			{
				set
				{
					base.PowerSharpParameters["AcceptLargeDataLoss"] = value;
				}
			}

			// Token: 0x17005E69 RID: 24169
			// (set) Token: 0x0600882E RID: 34862 RVA: 0x000C8914 File Offset: 0x000C6B14
			public virtual string BatchName
			{
				set
				{
					base.PowerSharpParameters["BatchName"] = value;
				}
			}

			// Token: 0x17005E6A RID: 24170
			// (set) Token: 0x0600882F RID: 34863 RVA: 0x000C8927 File Offset: 0x000C6B27
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17005E6B RID: 24171
			// (set) Token: 0x06008830 RID: 34864 RVA: 0x000C893A File Offset: 0x000C6B3A
			public virtual SwitchParameter Suspend
			{
				set
				{
					base.PowerSharpParameters["Suspend"] = value;
				}
			}

			// Token: 0x17005E6C RID: 24172
			// (set) Token: 0x06008831 RID: 34865 RVA: 0x000C8952 File Offset: 0x000C6B52
			public virtual string SuspendComment
			{
				set
				{
					base.PowerSharpParameters["SuspendComment"] = value;
				}
			}

			// Token: 0x17005E6D RID: 24173
			// (set) Token: 0x06008832 RID: 34866 RVA: 0x000C8965 File Offset: 0x000C6B65
			public virtual RequestPriority Priority
			{
				set
				{
					base.PowerSharpParameters["Priority"] = value;
				}
			}

			// Token: 0x17005E6E RID: 24174
			// (set) Token: 0x06008833 RID: 34867 RVA: 0x000C897D File Offset: 0x000C6B7D
			public virtual RequestWorkloadType WorkloadType
			{
				set
				{
					base.PowerSharpParameters["WorkloadType"] = value;
				}
			}

			// Token: 0x17005E6F RID: 24175
			// (set) Token: 0x06008834 RID: 34868 RVA: 0x000C8995 File Offset: 0x000C6B95
			public virtual Unlimited<EnhancedTimeSpan> CompletedRequestAgeLimit
			{
				set
				{
					base.PowerSharpParameters["CompletedRequestAgeLimit"] = value;
				}
			}

			// Token: 0x17005E70 RID: 24176
			// (set) Token: 0x06008835 RID: 34869 RVA: 0x000C89AD File Offset: 0x000C6BAD
			public virtual SkippableMergeComponent SkipMerging
			{
				set
				{
					base.PowerSharpParameters["SkipMerging"] = value;
				}
			}

			// Token: 0x17005E71 RID: 24177
			// (set) Token: 0x06008836 RID: 34870 RVA: 0x000C89C5 File Offset: 0x000C6BC5
			public virtual InternalMrsFlag InternalFlags
			{
				set
				{
					base.PowerSharpParameters["InternalFlags"] = value;
				}
			}

			// Token: 0x17005E72 RID: 24178
			// (set) Token: 0x06008837 RID: 34871 RVA: 0x000C89DD File Offset: 0x000C6BDD
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17005E73 RID: 24179
			// (set) Token: 0x06008838 RID: 34872 RVA: 0x000C89F5 File Offset: 0x000C6BF5
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17005E74 RID: 24180
			// (set) Token: 0x06008839 RID: 34873 RVA: 0x000C8A0D File Offset: 0x000C6C0D
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17005E75 RID: 24181
			// (set) Token: 0x0600883A RID: 34874 RVA: 0x000C8A25 File Offset: 0x000C6C25
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17005E76 RID: 24182
			// (set) Token: 0x0600883B RID: 34875 RVA: 0x000C8A3D File Offset: 0x000C6C3D
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}

		// Token: 0x02000ABF RID: 2751
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x17005E77 RID: 24183
			// (set) Token: 0x0600883D RID: 34877 RVA: 0x000C8A5D File Offset: 0x000C6C5D
			public virtual SwitchParameter ForestWideDomainControllerAffinityByExecutingUser
			{
				set
				{
					base.PowerSharpParameters["ForestWideDomainControllerAffinityByExecutingUser"] = value;
				}
			}

			// Token: 0x17005E78 RID: 24184
			// (set) Token: 0x0600883E RID: 34878 RVA: 0x000C8A75 File Offset: 0x000C6C75
			public virtual Unlimited<int> BadItemLimit
			{
				set
				{
					base.PowerSharpParameters["BadItemLimit"] = value;
				}
			}

			// Token: 0x17005E79 RID: 24185
			// (set) Token: 0x0600883F RID: 34879 RVA: 0x000C8A8D File Offset: 0x000C6C8D
			public virtual Unlimited<int> LargeItemLimit
			{
				set
				{
					base.PowerSharpParameters["LargeItemLimit"] = value;
				}
			}

			// Token: 0x17005E7A RID: 24186
			// (set) Token: 0x06008840 RID: 34880 RVA: 0x000C8AA5 File Offset: 0x000C6CA5
			public virtual SwitchParameter AcceptLargeDataLoss
			{
				set
				{
					base.PowerSharpParameters["AcceptLargeDataLoss"] = value;
				}
			}

			// Token: 0x17005E7B RID: 24187
			// (set) Token: 0x06008841 RID: 34881 RVA: 0x000C8ABD File Offset: 0x000C6CBD
			public virtual string BatchName
			{
				set
				{
					base.PowerSharpParameters["BatchName"] = value;
				}
			}

			// Token: 0x17005E7C RID: 24188
			// (set) Token: 0x06008842 RID: 34882 RVA: 0x000C8AD0 File Offset: 0x000C6CD0
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17005E7D RID: 24189
			// (set) Token: 0x06008843 RID: 34883 RVA: 0x000C8AE3 File Offset: 0x000C6CE3
			public virtual SwitchParameter Suspend
			{
				set
				{
					base.PowerSharpParameters["Suspend"] = value;
				}
			}

			// Token: 0x17005E7E RID: 24190
			// (set) Token: 0x06008844 RID: 34884 RVA: 0x000C8AFB File Offset: 0x000C6CFB
			public virtual string SuspendComment
			{
				set
				{
					base.PowerSharpParameters["SuspendComment"] = value;
				}
			}

			// Token: 0x17005E7F RID: 24191
			// (set) Token: 0x06008845 RID: 34885 RVA: 0x000C8B0E File Offset: 0x000C6D0E
			public virtual RequestPriority Priority
			{
				set
				{
					base.PowerSharpParameters["Priority"] = value;
				}
			}

			// Token: 0x17005E80 RID: 24192
			// (set) Token: 0x06008846 RID: 34886 RVA: 0x000C8B26 File Offset: 0x000C6D26
			public virtual RequestWorkloadType WorkloadType
			{
				set
				{
					base.PowerSharpParameters["WorkloadType"] = value;
				}
			}

			// Token: 0x17005E81 RID: 24193
			// (set) Token: 0x06008847 RID: 34887 RVA: 0x000C8B3E File Offset: 0x000C6D3E
			public virtual Unlimited<EnhancedTimeSpan> CompletedRequestAgeLimit
			{
				set
				{
					base.PowerSharpParameters["CompletedRequestAgeLimit"] = value;
				}
			}

			// Token: 0x17005E82 RID: 24194
			// (set) Token: 0x06008848 RID: 34888 RVA: 0x000C8B56 File Offset: 0x000C6D56
			public virtual SkippableMergeComponent SkipMerging
			{
				set
				{
					base.PowerSharpParameters["SkipMerging"] = value;
				}
			}

			// Token: 0x17005E83 RID: 24195
			// (set) Token: 0x06008849 RID: 34889 RVA: 0x000C8B6E File Offset: 0x000C6D6E
			public virtual InternalMrsFlag InternalFlags
			{
				set
				{
					base.PowerSharpParameters["InternalFlags"] = value;
				}
			}

			// Token: 0x17005E84 RID: 24196
			// (set) Token: 0x0600884A RID: 34890 RVA: 0x000C8B86 File Offset: 0x000C6D86
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17005E85 RID: 24197
			// (set) Token: 0x0600884B RID: 34891 RVA: 0x000C8B9E File Offset: 0x000C6D9E
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17005E86 RID: 24198
			// (set) Token: 0x0600884C RID: 34892 RVA: 0x000C8BB6 File Offset: 0x000C6DB6
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17005E87 RID: 24199
			// (set) Token: 0x0600884D RID: 34893 RVA: 0x000C8BCE File Offset: 0x000C6DCE
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17005E88 RID: 24200
			// (set) Token: 0x0600884E RID: 34894 RVA: 0x000C8BE6 File Offset: 0x000C6DE6
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
