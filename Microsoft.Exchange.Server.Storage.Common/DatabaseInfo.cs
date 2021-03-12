using System;
using Microsoft.Exchange.Security.Authorization;

namespace Microsoft.Exchange.Server.Storage.Common
{
	// Token: 0x02000029 RID: 41
	public class DatabaseInfo
	{
		// Token: 0x1700003E RID: 62
		// (get) Token: 0x060002D3 RID: 723 RVA: 0x00007BF0 File Offset: 0x00005DF0
		// (set) Token: 0x060002D4 RID: 724 RVA: 0x00007BF8 File Offset: 0x00005DF8
		public Guid MdbGuid { get; private set; }

		// Token: 0x1700003F RID: 63
		// (get) Token: 0x060002D5 RID: 725 RVA: 0x00007C01 File Offset: 0x00005E01
		// (set) Token: 0x060002D6 RID: 726 RVA: 0x00007C09 File Offset: 0x00005E09
		public string MdbName { get; private set; }

		// Token: 0x17000040 RID: 64
		// (get) Token: 0x060002D7 RID: 727 RVA: 0x00007C12 File Offset: 0x00005E12
		// (set) Token: 0x060002D8 RID: 728 RVA: 0x00007C1A File Offset: 0x00005E1A
		public Guid DagOrServerGuid { get; private set; }

		// Token: 0x17000041 RID: 65
		// (get) Token: 0x060002D9 RID: 729 RVA: 0x00007C23 File Offset: 0x00005E23
		// (set) Token: 0x060002DA RID: 730 RVA: 0x00007C2B File Offset: 0x00005E2B
		public string FilePath { get; private set; }

		// Token: 0x17000042 RID: 66
		// (get) Token: 0x060002DB RID: 731 RVA: 0x00007C34 File Offset: 0x00005E34
		// (set) Token: 0x060002DC RID: 732 RVA: 0x00007C3C File Offset: 0x00005E3C
		public string FileName { get; private set; }

		// Token: 0x17000043 RID: 67
		// (get) Token: 0x060002DD RID: 733 RVA: 0x00007C45 File Offset: 0x00005E45
		// (set) Token: 0x060002DE RID: 734 RVA: 0x00007C4D File Offset: 0x00005E4D
		public string LogPath { get; private set; }

		// Token: 0x17000044 RID: 68
		// (get) Token: 0x060002DF RID: 735 RVA: 0x00007C56 File Offset: 0x00005E56
		// (set) Token: 0x060002E0 RID: 736 RVA: 0x00007C5E File Offset: 0x00005E5E
		public string LegacyDN { get; private set; }

		// Token: 0x17000045 RID: 69
		// (get) Token: 0x060002E1 RID: 737 RVA: 0x00007C67 File Offset: 0x00005E67
		// (set) Token: 0x060002E2 RID: 738 RVA: 0x00007C6F File Offset: 0x00005E6F
		public TimeSpan EventHistoryRetentionPeriod { get; private set; }

		// Token: 0x17000046 RID: 70
		// (get) Token: 0x060002E3 RID: 739 RVA: 0x00007C78 File Offset: 0x00005E78
		// (set) Token: 0x060002E4 RID: 740 RVA: 0x00007C80 File Offset: 0x00005E80
		public bool IsPublicFolderDatabase { get; private set; }

		// Token: 0x17000047 RID: 71
		// (get) Token: 0x060002E5 RID: 741 RVA: 0x00007C89 File Offset: 0x00005E89
		// (set) Token: 0x060002E6 RID: 742 RVA: 0x00007C91 File Offset: 0x00005E91
		public bool IsRecoveryDatabase { get; private set; }

		// Token: 0x17000048 RID: 72
		// (get) Token: 0x060002E7 RID: 743 RVA: 0x00007C9A File Offset: 0x00005E9A
		// (set) Token: 0x060002E8 RID: 744 RVA: 0x00007CA2 File Offset: 0x00005EA2
		public string Description { get; private set; }

		// Token: 0x17000049 RID: 73
		// (get) Token: 0x060002E9 RID: 745 RVA: 0x00007CAB File Offset: 0x00005EAB
		// (set) Token: 0x060002EA RID: 746 RVA: 0x00007CB3 File Offset: 0x00005EB3
		public string ServerName { get; private set; }

		// Token: 0x1700004A RID: 74
		// (get) Token: 0x060002EB RID: 747 RVA: 0x00007CBC File Offset: 0x00005EBC
		// (set) Token: 0x060002EC RID: 748 RVA: 0x00007CC4 File Offset: 0x00005EC4
		public string ForestName { get; private set; }

		// Token: 0x1700004B RID: 75
		// (get) Token: 0x060002ED RID: 749 RVA: 0x00007CCD File Offset: 0x00005ECD
		// (set) Token: 0x060002EE RID: 750 RVA: 0x00007CD5 File Offset: 0x00005ED5
		public SecurityDescriptor NTSecurityDescriptor { get; private set; }

		// Token: 0x1700004C RID: 76
		// (get) Token: 0x060002EF RID: 751 RVA: 0x00007CDE File Offset: 0x00005EDE
		// (set) Token: 0x060002F0 RID: 752 RVA: 0x00007CE6 File Offset: 0x00005EE6
		public bool CircularLoggingEnabled { get; private set; }

		// Token: 0x1700004D RID: 77
		// (get) Token: 0x060002F1 RID: 753 RVA: 0x00007CEF File Offset: 0x00005EEF
		// (set) Token: 0x060002F2 RID: 754 RVA: 0x00007CF7 File Offset: 0x00005EF7
		public TimeSpan MailboxRetentionPeriod { get; private set; }

		// Token: 0x1700004E RID: 78
		// (get) Token: 0x060002F3 RID: 755 RVA: 0x00007D00 File Offset: 0x00005F00
		// (set) Token: 0x060002F4 RID: 756 RVA: 0x00007D08 File Offset: 0x00005F08
		public string[] HostServerNames { get; private set; }

		// Token: 0x1700004F RID: 79
		// (get) Token: 0x060002F5 RID: 757 RVA: 0x00007D11 File Offset: 0x00005F11
		// (set) Token: 0x060002F6 RID: 758 RVA: 0x00007D19 File Offset: 0x00005F19
		public bool AllowFileRestore { get; private set; }

		// Token: 0x17000050 RID: 80
		// (get) Token: 0x060002F7 RID: 759 RVA: 0x00007D22 File Offset: 0x00005F22
		// (set) Token: 0x060002F8 RID: 760 RVA: 0x00007D39 File Offset: 0x00005F39
		public DatabaseOptions Options
		{
			get
			{
				if (this.databaseOptions == null)
				{
					return null;
				}
				return this.databaseOptions.Clone();
			}
			private set
			{
				this.databaseOptions = ((value != null) ? value.Clone() : null);
			}
		}

		// Token: 0x17000051 RID: 81
		// (get) Token: 0x060002F9 RID: 761 RVA: 0x00007D4D File Offset: 0x00005F4D
		// (set) Token: 0x060002FA RID: 762 RVA: 0x00007D55 File Offset: 0x00005F55
		public QuotaInfo QuotaInfo { get; private set; }

		// Token: 0x17000052 RID: 82
		// (get) Token: 0x060002FB RID: 763 RVA: 0x00007D5E File Offset: 0x00005F5E
		// (set) Token: 0x060002FC RID: 764 RVA: 0x00007D66 File Offset: 0x00005F66
		public int DataMoveReplicationConstraint { get; private set; }

		// Token: 0x060002FD RID: 765 RVA: 0x00007D70 File Offset: 0x00005F70
		public DatabaseInfo(Guid mdbGuid, string mdbName, Guid dagOrServerGuid, string filePath, string fileName, string logPath, string legacyDN, TimeSpan eventHistoryRetentionPeriod, bool isPublicFolderDatabase, bool isRecoveryDatabase, string description, string serverName, SecurityDescriptor ntSecurityDescriptor, bool circularLoggingEnabled, bool allowFileRestore, TimeSpan mailboxRetentionPeriod, string[] hostServerNames, DatabaseOptions databaseOptions, QuotaInfo quotaInfo, int dataMoveReplicationConstraint, string forestName)
		{
			this.MdbGuid = mdbGuid;
			this.MdbName = mdbName;
			this.DagOrServerGuid = dagOrServerGuid;
			this.FilePath = filePath;
			this.FileName = fileName;
			this.LogPath = logPath;
			this.LegacyDN = legacyDN;
			this.EventHistoryRetentionPeriod = eventHistoryRetentionPeriod;
			this.IsPublicFolderDatabase = isPublicFolderDatabase;
			this.IsRecoveryDatabase = isRecoveryDatabase;
			this.Description = description;
			this.ServerName = serverName;
			this.NTSecurityDescriptor = ntSecurityDescriptor;
			this.CircularLoggingEnabled = circularLoggingEnabled;
			this.AllowFileRestore = allowFileRestore;
			this.MailboxRetentionPeriod = mailboxRetentionPeriod;
			this.HostServerNames = hostServerNames;
			this.Options = databaseOptions;
			this.QuotaInfo = quotaInfo;
			this.DataMoveReplicationConstraint = dataMoveReplicationConstraint;
			this.ForestName = forestName;
		}

		// Token: 0x040003F5 RID: 1013
		private DatabaseOptions databaseOptions;
	}
}
