using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x020005FF RID: 1535
	public class SetMailboxDatabaseCommand : SyntheticCommandWithPipelineInputNoOutput<MailboxDatabase>
	{
		// Token: 0x06004EA9 RID: 20137 RVA: 0x0007D391 File Offset: 0x0007B591
		private SetMailboxDatabaseCommand() : base("Set-MailboxDatabase")
		{
		}

		// Token: 0x06004EAA RID: 20138 RVA: 0x0007D39E File Offset: 0x0007B59E
		public SetMailboxDatabaseCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x06004EAB RID: 20139 RVA: 0x0007D3AD File Offset: 0x0007B5AD
		public virtual SetMailboxDatabaseCommand SetParameters(SetMailboxDatabaseCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x06004EAC RID: 20140 RVA: 0x0007D3B7 File Offset: 0x0007B5B7
		public virtual SetMailboxDatabaseCommand SetParameters(SetMailboxDatabaseCommand.IdentityParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x02000600 RID: 1536
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x17002E64 RID: 11876
			// (set) Token: 0x06004EAD RID: 20141 RVA: 0x0007D3C1 File Offset: 0x0007B5C1
			public virtual DatabaseIdParameter PublicFolderDatabase
			{
				set
				{
					base.PowerSharpParameters["PublicFolderDatabase"] = value;
				}
			}

			// Token: 0x17002E65 RID: 11877
			// (set) Token: 0x06004EAE RID: 20142 RVA: 0x0007D3D4 File Offset: 0x0007B5D4
			public virtual OfflineAddressBookIdParameter OfflineAddressBook
			{
				set
				{
					base.PowerSharpParameters["OfflineAddressBook"] = value;
				}
			}

			// Token: 0x17002E66 RID: 11878
			// (set) Token: 0x06004EAF RID: 20143 RVA: 0x0007D3E7 File Offset: 0x0007B5E7
			public virtual string JournalRecipient
			{
				set
				{
					base.PowerSharpParameters["JournalRecipient"] = ((value != null) ? new RecipientIdParameter(value) : null);
				}
			}

			// Token: 0x17002E67 RID: 11879
			// (set) Token: 0x06004EB0 RID: 20144 RVA: 0x0007D405 File Offset: 0x0007B605
			public virtual MailboxProvisioningAttributes MailboxProvisioningAttributes
			{
				set
				{
					base.PowerSharpParameters["MailboxProvisioningAttributes"] = value;
				}
			}

			// Token: 0x17002E68 RID: 11880
			// (set) Token: 0x06004EB1 RID: 20145 RVA: 0x0007D418 File Offset: 0x0007B618
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17002E69 RID: 11881
			// (set) Token: 0x06004EB2 RID: 20146 RVA: 0x0007D42B File Offset: 0x0007B62B
			public virtual EnhancedTimeSpan MailboxRetention
			{
				set
				{
					base.PowerSharpParameters["MailboxRetention"] = value;
				}
			}

			// Token: 0x17002E6A RID: 11882
			// (set) Token: 0x06004EB3 RID: 20147 RVA: 0x0007D443 File Offset: 0x0007B643
			public virtual Unlimited<ByteQuantifiedSize> ProhibitSendReceiveQuota
			{
				set
				{
					base.PowerSharpParameters["ProhibitSendReceiveQuota"] = value;
				}
			}

			// Token: 0x17002E6B RID: 11883
			// (set) Token: 0x06004EB4 RID: 20148 RVA: 0x0007D45B File Offset: 0x0007B65B
			public virtual Unlimited<ByteQuantifiedSize> ProhibitSendQuota
			{
				set
				{
					base.PowerSharpParameters["ProhibitSendQuota"] = value;
				}
			}

			// Token: 0x17002E6C RID: 11884
			// (set) Token: 0x06004EB5 RID: 20149 RVA: 0x0007D473 File Offset: 0x0007B673
			public virtual Unlimited<ByteQuantifiedSize> RecoverableItemsQuota
			{
				set
				{
					base.PowerSharpParameters["RecoverableItemsQuota"] = value;
				}
			}

			// Token: 0x17002E6D RID: 11885
			// (set) Token: 0x06004EB6 RID: 20150 RVA: 0x0007D48B File Offset: 0x0007B68B
			public virtual Unlimited<ByteQuantifiedSize> RecoverableItemsWarningQuota
			{
				set
				{
					base.PowerSharpParameters["RecoverableItemsWarningQuota"] = value;
				}
			}

			// Token: 0x17002E6E RID: 11886
			// (set) Token: 0x06004EB7 RID: 20151 RVA: 0x0007D4A3 File Offset: 0x0007B6A3
			public virtual Unlimited<ByteQuantifiedSize> CalendarLoggingQuota
			{
				set
				{
					base.PowerSharpParameters["CalendarLoggingQuota"] = value;
				}
			}

			// Token: 0x17002E6F RID: 11887
			// (set) Token: 0x06004EB8 RID: 20152 RVA: 0x0007D4BB File Offset: 0x0007B6BB
			public virtual bool IndexEnabled
			{
				set
				{
					base.PowerSharpParameters["IndexEnabled"] = value;
				}
			}

			// Token: 0x17002E70 RID: 11888
			// (set) Token: 0x06004EB9 RID: 20153 RVA: 0x0007D4D3 File Offset: 0x0007B6D3
			public virtual bool IsExcludedFromProvisioning
			{
				set
				{
					base.PowerSharpParameters["IsExcludedFromProvisioning"] = value;
				}
			}

			// Token: 0x17002E71 RID: 11889
			// (set) Token: 0x06004EBA RID: 20154 RVA: 0x0007D4EB File Offset: 0x0007B6EB
			public virtual bool IsExcludedFromProvisioningBySchemaVersionMonitoring
			{
				set
				{
					base.PowerSharpParameters["IsExcludedFromProvisioningBySchemaVersionMonitoring"] = value;
				}
			}

			// Token: 0x17002E72 RID: 11890
			// (set) Token: 0x06004EBB RID: 20155 RVA: 0x0007D503 File Offset: 0x0007B703
			public virtual bool IsExcludedFromInitialProvisioning
			{
				set
				{
					base.PowerSharpParameters["IsExcludedFromInitialProvisioning"] = value;
				}
			}

			// Token: 0x17002E73 RID: 11891
			// (set) Token: 0x06004EBC RID: 20156 RVA: 0x0007D51B File Offset: 0x0007B71B
			public virtual bool IsSuspendedFromProvisioning
			{
				set
				{
					base.PowerSharpParameters["IsSuspendedFromProvisioning"] = value;
				}
			}

			// Token: 0x17002E74 RID: 11892
			// (set) Token: 0x06004EBD RID: 20157 RVA: 0x0007D533 File Offset: 0x0007B733
			public virtual bool IsExcludedFromProvisioningBySpaceMonitoring
			{
				set
				{
					base.PowerSharpParameters["IsExcludedFromProvisioningBySpaceMonitoring"] = value;
				}
			}

			// Token: 0x17002E75 RID: 11893
			// (set) Token: 0x06004EBE RID: 20158 RVA: 0x0007D54B File Offset: 0x0007B74B
			public virtual ByteQuantifiedSize? MailboxLoadBalanceMaximumEdbFileSize
			{
				set
				{
					base.PowerSharpParameters["MailboxLoadBalanceMaximumEdbFileSize"] = value;
				}
			}

			// Token: 0x17002E76 RID: 11894
			// (set) Token: 0x06004EBF RID: 20159 RVA: 0x0007D563 File Offset: 0x0007B763
			public virtual int? MailboxLoadBalanceRelativeLoadCapacity
			{
				set
				{
					base.PowerSharpParameters["MailboxLoadBalanceRelativeLoadCapacity"] = value;
				}
			}

			// Token: 0x17002E77 RID: 11895
			// (set) Token: 0x06004EC0 RID: 20160 RVA: 0x0007D57B File Offset: 0x0007B77B
			public virtual int? MailboxLoadBalanceOverloadedThreshold
			{
				set
				{
					base.PowerSharpParameters["MailboxLoadBalanceOverloadedThreshold"] = value;
				}
			}

			// Token: 0x17002E78 RID: 11896
			// (set) Token: 0x06004EC1 RID: 20161 RVA: 0x0007D593 File Offset: 0x0007B793
			public virtual int? MailboxLoadBalanceUnderloadedThreshold
			{
				set
				{
					base.PowerSharpParameters["MailboxLoadBalanceUnderloadedThreshold"] = value;
				}
			}

			// Token: 0x17002E79 RID: 11897
			// (set) Token: 0x06004EC2 RID: 20162 RVA: 0x0007D5AB File Offset: 0x0007B7AB
			public virtual bool? MailboxLoadBalanceEnabled
			{
				set
				{
					base.PowerSharpParameters["MailboxLoadBalanceEnabled"] = value;
				}
			}

			// Token: 0x17002E7A RID: 11898
			// (set) Token: 0x06004EC3 RID: 20163 RVA: 0x0007D5C3 File Offset: 0x0007B7C3
			public virtual bool AllowFileRestore
			{
				set
				{
					base.PowerSharpParameters["AllowFileRestore"] = value;
				}
			}

			// Token: 0x17002E7B RID: 11899
			// (set) Token: 0x06004EC4 RID: 20164 RVA: 0x0007D5DB File Offset: 0x0007B7DB
			public virtual bool BackgroundDatabaseMaintenance
			{
				set
				{
					base.PowerSharpParameters["BackgroundDatabaseMaintenance"] = value;
				}
			}

			// Token: 0x17002E7C RID: 11900
			// (set) Token: 0x06004EC5 RID: 20165 RVA: 0x0007D5F3 File Offset: 0x0007B7F3
			public virtual EnhancedTimeSpan DeletedItemRetention
			{
				set
				{
					base.PowerSharpParameters["DeletedItemRetention"] = value;
				}
			}

			// Token: 0x17002E7D RID: 11901
			// (set) Token: 0x06004EC6 RID: 20166 RVA: 0x0007D60B File Offset: 0x0007B80B
			public virtual Schedule MaintenanceSchedule
			{
				set
				{
					base.PowerSharpParameters["MaintenanceSchedule"] = value;
				}
			}

			// Token: 0x17002E7E RID: 11902
			// (set) Token: 0x06004EC7 RID: 20167 RVA: 0x0007D61E File Offset: 0x0007B81E
			public virtual bool MountAtStartup
			{
				set
				{
					base.PowerSharpParameters["MountAtStartup"] = value;
				}
			}

			// Token: 0x17002E7F RID: 11903
			// (set) Token: 0x06004EC8 RID: 20168 RVA: 0x0007D636 File Offset: 0x0007B836
			public virtual Schedule QuotaNotificationSchedule
			{
				set
				{
					base.PowerSharpParameters["QuotaNotificationSchedule"] = value;
				}
			}

			// Token: 0x17002E80 RID: 11904
			// (set) Token: 0x06004EC9 RID: 20169 RVA: 0x0007D649 File Offset: 0x0007B849
			public virtual bool RetainDeletedItemsUntilBackup
			{
				set
				{
					base.PowerSharpParameters["RetainDeletedItemsUntilBackup"] = value;
				}
			}

			// Token: 0x17002E81 RID: 11905
			// (set) Token: 0x06004ECA RID: 20170 RVA: 0x0007D661 File Offset: 0x0007B861
			public virtual bool AutoDagExcludeFromMonitoring
			{
				set
				{
					base.PowerSharpParameters["AutoDagExcludeFromMonitoring"] = value;
				}
			}

			// Token: 0x17002E82 RID: 11906
			// (set) Token: 0x06004ECB RID: 20171 RVA: 0x0007D679 File Offset: 0x0007B879
			public virtual AutoDatabaseMountDial AutoDatabaseMountDial
			{
				set
				{
					base.PowerSharpParameters["AutoDatabaseMountDial"] = value;
				}
			}

			// Token: 0x17002E83 RID: 11907
			// (set) Token: 0x06004ECC RID: 20172 RVA: 0x0007D691 File Offset: 0x0007B891
			public virtual string DatabaseGroup
			{
				set
				{
					base.PowerSharpParameters["DatabaseGroup"] = value;
				}
			}

			// Token: 0x17002E84 RID: 11908
			// (set) Token: 0x06004ECD RID: 20173 RVA: 0x0007D6A4 File Offset: 0x0007B8A4
			public virtual Unlimited<ByteQuantifiedSize> IssueWarningQuota
			{
				set
				{
					base.PowerSharpParameters["IssueWarningQuota"] = value;
				}
			}

			// Token: 0x17002E85 RID: 11909
			// (set) Token: 0x06004ECE RID: 20174 RVA: 0x0007D6BC File Offset: 0x0007B8BC
			public virtual EnhancedTimeSpan EventHistoryRetentionPeriod
			{
				set
				{
					base.PowerSharpParameters["EventHistoryRetentionPeriod"] = value;
				}
			}

			// Token: 0x17002E86 RID: 11910
			// (set) Token: 0x06004ECF RID: 20175 RVA: 0x0007D6D4 File Offset: 0x0007B8D4
			public virtual string Name
			{
				set
				{
					base.PowerSharpParameters["Name"] = value;
				}
			}

			// Token: 0x17002E87 RID: 11911
			// (set) Token: 0x06004ED0 RID: 20176 RVA: 0x0007D6E7 File Offset: 0x0007B8E7
			public virtual bool CircularLoggingEnabled
			{
				set
				{
					base.PowerSharpParameters["CircularLoggingEnabled"] = value;
				}
			}

			// Token: 0x17002E88 RID: 11912
			// (set) Token: 0x06004ED1 RID: 20177 RVA: 0x0007D6FF File Offset: 0x0007B8FF
			public virtual DataMoveReplicationConstraintParameter DataMoveReplicationConstraint
			{
				set
				{
					base.PowerSharpParameters["DataMoveReplicationConstraint"] = value;
				}
			}

			// Token: 0x17002E89 RID: 11913
			// (set) Token: 0x06004ED2 RID: 20178 RVA: 0x0007D717 File Offset: 0x0007B917
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17002E8A RID: 11914
			// (set) Token: 0x06004ED3 RID: 20179 RVA: 0x0007D72F File Offset: 0x0007B92F
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17002E8B RID: 11915
			// (set) Token: 0x06004ED4 RID: 20180 RVA: 0x0007D747 File Offset: 0x0007B947
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17002E8C RID: 11916
			// (set) Token: 0x06004ED5 RID: 20181 RVA: 0x0007D75F File Offset: 0x0007B95F
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17002E8D RID: 11917
			// (set) Token: 0x06004ED6 RID: 20182 RVA: 0x0007D777 File Offset: 0x0007B977
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}

		// Token: 0x02000601 RID: 1537
		public class IdentityParameters : ParametersBase
		{
			// Token: 0x17002E8E RID: 11918
			// (set) Token: 0x06004ED8 RID: 20184 RVA: 0x0007D797 File Offset: 0x0007B997
			public virtual DatabaseIdParameter Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = value;
				}
			}

			// Token: 0x17002E8F RID: 11919
			// (set) Token: 0x06004ED9 RID: 20185 RVA: 0x0007D7AA File Offset: 0x0007B9AA
			public virtual DatabaseIdParameter PublicFolderDatabase
			{
				set
				{
					base.PowerSharpParameters["PublicFolderDatabase"] = value;
				}
			}

			// Token: 0x17002E90 RID: 11920
			// (set) Token: 0x06004EDA RID: 20186 RVA: 0x0007D7BD File Offset: 0x0007B9BD
			public virtual OfflineAddressBookIdParameter OfflineAddressBook
			{
				set
				{
					base.PowerSharpParameters["OfflineAddressBook"] = value;
				}
			}

			// Token: 0x17002E91 RID: 11921
			// (set) Token: 0x06004EDB RID: 20187 RVA: 0x0007D7D0 File Offset: 0x0007B9D0
			public virtual string JournalRecipient
			{
				set
				{
					base.PowerSharpParameters["JournalRecipient"] = ((value != null) ? new RecipientIdParameter(value) : null);
				}
			}

			// Token: 0x17002E92 RID: 11922
			// (set) Token: 0x06004EDC RID: 20188 RVA: 0x0007D7EE File Offset: 0x0007B9EE
			public virtual MailboxProvisioningAttributes MailboxProvisioningAttributes
			{
				set
				{
					base.PowerSharpParameters["MailboxProvisioningAttributes"] = value;
				}
			}

			// Token: 0x17002E93 RID: 11923
			// (set) Token: 0x06004EDD RID: 20189 RVA: 0x0007D801 File Offset: 0x0007BA01
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17002E94 RID: 11924
			// (set) Token: 0x06004EDE RID: 20190 RVA: 0x0007D814 File Offset: 0x0007BA14
			public virtual EnhancedTimeSpan MailboxRetention
			{
				set
				{
					base.PowerSharpParameters["MailboxRetention"] = value;
				}
			}

			// Token: 0x17002E95 RID: 11925
			// (set) Token: 0x06004EDF RID: 20191 RVA: 0x0007D82C File Offset: 0x0007BA2C
			public virtual Unlimited<ByteQuantifiedSize> ProhibitSendReceiveQuota
			{
				set
				{
					base.PowerSharpParameters["ProhibitSendReceiveQuota"] = value;
				}
			}

			// Token: 0x17002E96 RID: 11926
			// (set) Token: 0x06004EE0 RID: 20192 RVA: 0x0007D844 File Offset: 0x0007BA44
			public virtual Unlimited<ByteQuantifiedSize> ProhibitSendQuota
			{
				set
				{
					base.PowerSharpParameters["ProhibitSendQuota"] = value;
				}
			}

			// Token: 0x17002E97 RID: 11927
			// (set) Token: 0x06004EE1 RID: 20193 RVA: 0x0007D85C File Offset: 0x0007BA5C
			public virtual Unlimited<ByteQuantifiedSize> RecoverableItemsQuota
			{
				set
				{
					base.PowerSharpParameters["RecoverableItemsQuota"] = value;
				}
			}

			// Token: 0x17002E98 RID: 11928
			// (set) Token: 0x06004EE2 RID: 20194 RVA: 0x0007D874 File Offset: 0x0007BA74
			public virtual Unlimited<ByteQuantifiedSize> RecoverableItemsWarningQuota
			{
				set
				{
					base.PowerSharpParameters["RecoverableItemsWarningQuota"] = value;
				}
			}

			// Token: 0x17002E99 RID: 11929
			// (set) Token: 0x06004EE3 RID: 20195 RVA: 0x0007D88C File Offset: 0x0007BA8C
			public virtual Unlimited<ByteQuantifiedSize> CalendarLoggingQuota
			{
				set
				{
					base.PowerSharpParameters["CalendarLoggingQuota"] = value;
				}
			}

			// Token: 0x17002E9A RID: 11930
			// (set) Token: 0x06004EE4 RID: 20196 RVA: 0x0007D8A4 File Offset: 0x0007BAA4
			public virtual bool IndexEnabled
			{
				set
				{
					base.PowerSharpParameters["IndexEnabled"] = value;
				}
			}

			// Token: 0x17002E9B RID: 11931
			// (set) Token: 0x06004EE5 RID: 20197 RVA: 0x0007D8BC File Offset: 0x0007BABC
			public virtual bool IsExcludedFromProvisioning
			{
				set
				{
					base.PowerSharpParameters["IsExcludedFromProvisioning"] = value;
				}
			}

			// Token: 0x17002E9C RID: 11932
			// (set) Token: 0x06004EE6 RID: 20198 RVA: 0x0007D8D4 File Offset: 0x0007BAD4
			public virtual bool IsExcludedFromProvisioningBySchemaVersionMonitoring
			{
				set
				{
					base.PowerSharpParameters["IsExcludedFromProvisioningBySchemaVersionMonitoring"] = value;
				}
			}

			// Token: 0x17002E9D RID: 11933
			// (set) Token: 0x06004EE7 RID: 20199 RVA: 0x0007D8EC File Offset: 0x0007BAEC
			public virtual bool IsExcludedFromInitialProvisioning
			{
				set
				{
					base.PowerSharpParameters["IsExcludedFromInitialProvisioning"] = value;
				}
			}

			// Token: 0x17002E9E RID: 11934
			// (set) Token: 0x06004EE8 RID: 20200 RVA: 0x0007D904 File Offset: 0x0007BB04
			public virtual bool IsSuspendedFromProvisioning
			{
				set
				{
					base.PowerSharpParameters["IsSuspendedFromProvisioning"] = value;
				}
			}

			// Token: 0x17002E9F RID: 11935
			// (set) Token: 0x06004EE9 RID: 20201 RVA: 0x0007D91C File Offset: 0x0007BB1C
			public virtual bool IsExcludedFromProvisioningBySpaceMonitoring
			{
				set
				{
					base.PowerSharpParameters["IsExcludedFromProvisioningBySpaceMonitoring"] = value;
				}
			}

			// Token: 0x17002EA0 RID: 11936
			// (set) Token: 0x06004EEA RID: 20202 RVA: 0x0007D934 File Offset: 0x0007BB34
			public virtual ByteQuantifiedSize? MailboxLoadBalanceMaximumEdbFileSize
			{
				set
				{
					base.PowerSharpParameters["MailboxLoadBalanceMaximumEdbFileSize"] = value;
				}
			}

			// Token: 0x17002EA1 RID: 11937
			// (set) Token: 0x06004EEB RID: 20203 RVA: 0x0007D94C File Offset: 0x0007BB4C
			public virtual int? MailboxLoadBalanceRelativeLoadCapacity
			{
				set
				{
					base.PowerSharpParameters["MailboxLoadBalanceRelativeLoadCapacity"] = value;
				}
			}

			// Token: 0x17002EA2 RID: 11938
			// (set) Token: 0x06004EEC RID: 20204 RVA: 0x0007D964 File Offset: 0x0007BB64
			public virtual int? MailboxLoadBalanceOverloadedThreshold
			{
				set
				{
					base.PowerSharpParameters["MailboxLoadBalanceOverloadedThreshold"] = value;
				}
			}

			// Token: 0x17002EA3 RID: 11939
			// (set) Token: 0x06004EED RID: 20205 RVA: 0x0007D97C File Offset: 0x0007BB7C
			public virtual int? MailboxLoadBalanceUnderloadedThreshold
			{
				set
				{
					base.PowerSharpParameters["MailboxLoadBalanceUnderloadedThreshold"] = value;
				}
			}

			// Token: 0x17002EA4 RID: 11940
			// (set) Token: 0x06004EEE RID: 20206 RVA: 0x0007D994 File Offset: 0x0007BB94
			public virtual bool? MailboxLoadBalanceEnabled
			{
				set
				{
					base.PowerSharpParameters["MailboxLoadBalanceEnabled"] = value;
				}
			}

			// Token: 0x17002EA5 RID: 11941
			// (set) Token: 0x06004EEF RID: 20207 RVA: 0x0007D9AC File Offset: 0x0007BBAC
			public virtual bool AllowFileRestore
			{
				set
				{
					base.PowerSharpParameters["AllowFileRestore"] = value;
				}
			}

			// Token: 0x17002EA6 RID: 11942
			// (set) Token: 0x06004EF0 RID: 20208 RVA: 0x0007D9C4 File Offset: 0x0007BBC4
			public virtual bool BackgroundDatabaseMaintenance
			{
				set
				{
					base.PowerSharpParameters["BackgroundDatabaseMaintenance"] = value;
				}
			}

			// Token: 0x17002EA7 RID: 11943
			// (set) Token: 0x06004EF1 RID: 20209 RVA: 0x0007D9DC File Offset: 0x0007BBDC
			public virtual EnhancedTimeSpan DeletedItemRetention
			{
				set
				{
					base.PowerSharpParameters["DeletedItemRetention"] = value;
				}
			}

			// Token: 0x17002EA8 RID: 11944
			// (set) Token: 0x06004EF2 RID: 20210 RVA: 0x0007D9F4 File Offset: 0x0007BBF4
			public virtual Schedule MaintenanceSchedule
			{
				set
				{
					base.PowerSharpParameters["MaintenanceSchedule"] = value;
				}
			}

			// Token: 0x17002EA9 RID: 11945
			// (set) Token: 0x06004EF3 RID: 20211 RVA: 0x0007DA07 File Offset: 0x0007BC07
			public virtual bool MountAtStartup
			{
				set
				{
					base.PowerSharpParameters["MountAtStartup"] = value;
				}
			}

			// Token: 0x17002EAA RID: 11946
			// (set) Token: 0x06004EF4 RID: 20212 RVA: 0x0007DA1F File Offset: 0x0007BC1F
			public virtual Schedule QuotaNotificationSchedule
			{
				set
				{
					base.PowerSharpParameters["QuotaNotificationSchedule"] = value;
				}
			}

			// Token: 0x17002EAB RID: 11947
			// (set) Token: 0x06004EF5 RID: 20213 RVA: 0x0007DA32 File Offset: 0x0007BC32
			public virtual bool RetainDeletedItemsUntilBackup
			{
				set
				{
					base.PowerSharpParameters["RetainDeletedItemsUntilBackup"] = value;
				}
			}

			// Token: 0x17002EAC RID: 11948
			// (set) Token: 0x06004EF6 RID: 20214 RVA: 0x0007DA4A File Offset: 0x0007BC4A
			public virtual bool AutoDagExcludeFromMonitoring
			{
				set
				{
					base.PowerSharpParameters["AutoDagExcludeFromMonitoring"] = value;
				}
			}

			// Token: 0x17002EAD RID: 11949
			// (set) Token: 0x06004EF7 RID: 20215 RVA: 0x0007DA62 File Offset: 0x0007BC62
			public virtual AutoDatabaseMountDial AutoDatabaseMountDial
			{
				set
				{
					base.PowerSharpParameters["AutoDatabaseMountDial"] = value;
				}
			}

			// Token: 0x17002EAE RID: 11950
			// (set) Token: 0x06004EF8 RID: 20216 RVA: 0x0007DA7A File Offset: 0x0007BC7A
			public virtual string DatabaseGroup
			{
				set
				{
					base.PowerSharpParameters["DatabaseGroup"] = value;
				}
			}

			// Token: 0x17002EAF RID: 11951
			// (set) Token: 0x06004EF9 RID: 20217 RVA: 0x0007DA8D File Offset: 0x0007BC8D
			public virtual Unlimited<ByteQuantifiedSize> IssueWarningQuota
			{
				set
				{
					base.PowerSharpParameters["IssueWarningQuota"] = value;
				}
			}

			// Token: 0x17002EB0 RID: 11952
			// (set) Token: 0x06004EFA RID: 20218 RVA: 0x0007DAA5 File Offset: 0x0007BCA5
			public virtual EnhancedTimeSpan EventHistoryRetentionPeriod
			{
				set
				{
					base.PowerSharpParameters["EventHistoryRetentionPeriod"] = value;
				}
			}

			// Token: 0x17002EB1 RID: 11953
			// (set) Token: 0x06004EFB RID: 20219 RVA: 0x0007DABD File Offset: 0x0007BCBD
			public virtual string Name
			{
				set
				{
					base.PowerSharpParameters["Name"] = value;
				}
			}

			// Token: 0x17002EB2 RID: 11954
			// (set) Token: 0x06004EFC RID: 20220 RVA: 0x0007DAD0 File Offset: 0x0007BCD0
			public virtual bool CircularLoggingEnabled
			{
				set
				{
					base.PowerSharpParameters["CircularLoggingEnabled"] = value;
				}
			}

			// Token: 0x17002EB3 RID: 11955
			// (set) Token: 0x06004EFD RID: 20221 RVA: 0x0007DAE8 File Offset: 0x0007BCE8
			public virtual DataMoveReplicationConstraintParameter DataMoveReplicationConstraint
			{
				set
				{
					base.PowerSharpParameters["DataMoveReplicationConstraint"] = value;
				}
			}

			// Token: 0x17002EB4 RID: 11956
			// (set) Token: 0x06004EFE RID: 20222 RVA: 0x0007DB00 File Offset: 0x0007BD00
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17002EB5 RID: 11957
			// (set) Token: 0x06004EFF RID: 20223 RVA: 0x0007DB18 File Offset: 0x0007BD18
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17002EB6 RID: 11958
			// (set) Token: 0x06004F00 RID: 20224 RVA: 0x0007DB30 File Offset: 0x0007BD30
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17002EB7 RID: 11959
			// (set) Token: 0x06004F01 RID: 20225 RVA: 0x0007DB48 File Offset: 0x0007BD48
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17002EB8 RID: 11960
			// (set) Token: 0x06004F02 RID: 20226 RVA: 0x0007DB60 File Offset: 0x0007BD60
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
