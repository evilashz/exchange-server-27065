using System;
using Microsoft.Exchange.Cluster.Replay;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Rpc.Cluster;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x02000894 RID: 2196
	[Serializable]
	public sealed class DatabaseCopyStatusEntry : IConfigurable
	{
		// Token: 0x170016A1 RID: 5793
		// (get) Token: 0x06004C90 RID: 19600 RVA: 0x0013F711 File Offset: 0x0013D911
		public ObjectId Identity
		{
			get
			{
				return this.m_identity;
			}
		}

		// Token: 0x170016A2 RID: 5794
		// (get) Token: 0x06004C91 RID: 19601 RVA: 0x0013F719 File Offset: 0x0013D919
		public ADObjectId Id
		{
			get
			{
				return this.m_identity;
			}
		}

		// Token: 0x170016A3 RID: 5795
		// (get) Token: 0x06004C92 RID: 19602 RVA: 0x0013F721 File Offset: 0x0013D921
		public string Name
		{
			get
			{
				return this.m_name;
			}
		}

		// Token: 0x170016A4 RID: 5796
		// (get) Token: 0x06004C93 RID: 19603 RVA: 0x0013F729 File Offset: 0x0013D929
		public string DatabaseName
		{
			get
			{
				return this.m_databaseName;
			}
		}

		// Token: 0x170016A5 RID: 5797
		// (get) Token: 0x06004C94 RID: 19604 RVA: 0x0013F731 File Offset: 0x0013D931
		// (set) Token: 0x06004C95 RID: 19605 RVA: 0x0013F739 File Offset: 0x0013D939
		public CopyStatus Status
		{
			get
			{
				return this.m_copyStatus;
			}
			set
			{
				this.m_copyStatus = value;
			}
		}

		// Token: 0x170016A6 RID: 5798
		// (get) Token: 0x06004C96 RID: 19606 RVA: 0x0013F742 File Offset: 0x0013D942
		// (set) Token: 0x06004C97 RID: 19607 RVA: 0x0013F74A File Offset: 0x0013D94A
		public DateTime? InstanceStartTime { get; internal set; }

		// Token: 0x170016A7 RID: 5799
		// (get) Token: 0x06004C98 RID: 19608 RVA: 0x0013F753 File Offset: 0x0013D953
		public DateTime? LastStatusTransitionTime
		{
			get
			{
				return this.m_lastStatusTransitionTime;
			}
		}

		// Token: 0x170016A8 RID: 5800
		// (get) Token: 0x06004C99 RID: 19609 RVA: 0x0013F75B File Offset: 0x0013D95B
		public string MailboxServer
		{
			get
			{
				return this.m_mailboxServer;
			}
		}

		// Token: 0x170016A9 RID: 5801
		// (get) Token: 0x06004C9A RID: 19610 RVA: 0x0013F763 File Offset: 0x0013D963
		public string ActiveDatabaseCopy
		{
			get
			{
				return this.m_activeDatabaseCopy;
			}
		}

		// Token: 0x170016AA RID: 5802
		// (get) Token: 0x06004C9B RID: 19611 RVA: 0x0013F76B File Offset: 0x0013D96B
		// (set) Token: 0x06004C9C RID: 19612 RVA: 0x0013F773 File Offset: 0x0013D973
		public bool ActiveCopy
		{
			get
			{
				return this.m_isActiveCopy;
			}
			internal set
			{
				this.m_isActiveCopy = value;
			}
		}

		// Token: 0x170016AB RID: 5803
		// (get) Token: 0x06004C9D RID: 19613 RVA: 0x0013F77C File Offset: 0x0013D97C
		// (set) Token: 0x06004C9E RID: 19614 RVA: 0x0013F784 File Offset: 0x0013D984
		public int? ActivationPreference { get; internal set; }

		// Token: 0x170016AC RID: 5804
		// (get) Token: 0x06004C9F RID: 19615 RVA: 0x0013F78D File Offset: 0x0013D98D
		public DateTime? StatusRetrievedTime
		{
			get
			{
				return this.m_statusRetrievedTime;
			}
		}

		// Token: 0x170016AD RID: 5805
		// (get) Token: 0x06004CA0 RID: 19616 RVA: 0x0013F795 File Offset: 0x0013D995
		public int? WorkerProcessId
		{
			get
			{
				return this.m_workerProcessId;
			}
		}

		// Token: 0x170016AE RID: 5806
		// (get) Token: 0x06004CA1 RID: 19617 RVA: 0x0013F79D File Offset: 0x0013D99D
		// (set) Token: 0x06004CA2 RID: 19618 RVA: 0x0013F7A5 File Offset: 0x0013D9A5
		public bool? IsLastCopyAvailabilityChecksPassed { get; internal set; }

		// Token: 0x170016AF RID: 5807
		// (get) Token: 0x06004CA3 RID: 19619 RVA: 0x0013F7AE File Offset: 0x0013D9AE
		// (set) Token: 0x06004CA4 RID: 19620 RVA: 0x0013F7B6 File Offset: 0x0013D9B6
		public DateTime? LastCopyAvailabilityChecksPassedTime { get; internal set; }

		// Token: 0x170016B0 RID: 5808
		// (get) Token: 0x06004CA5 RID: 19621 RVA: 0x0013F7BF File Offset: 0x0013D9BF
		// (set) Token: 0x06004CA6 RID: 19622 RVA: 0x0013F7C7 File Offset: 0x0013D9C7
		public bool? IsLastCopyRedundancyChecksPassed { get; internal set; }

		// Token: 0x170016B1 RID: 5809
		// (get) Token: 0x06004CA7 RID: 19623 RVA: 0x0013F7D0 File Offset: 0x0013D9D0
		// (set) Token: 0x06004CA8 RID: 19624 RVA: 0x0013F7D8 File Offset: 0x0013D9D8
		public DateTime? LastCopyRedundancyChecksPassedTime { get; internal set; }

		// Token: 0x170016B2 RID: 5810
		// (get) Token: 0x06004CA9 RID: 19625 RVA: 0x0013F7E1 File Offset: 0x0013D9E1
		public bool ActivationSuspended
		{
			get
			{
				return this.m_activationSuspended;
			}
		}

		// Token: 0x170016B3 RID: 5811
		// (get) Token: 0x06004CAA RID: 19626 RVA: 0x0013F7E9 File Offset: 0x0013D9E9
		public ActionInitiatorType ActionInitiator
		{
			get
			{
				return this.m_actionInitiator;
			}
		}

		// Token: 0x170016B4 RID: 5812
		// (get) Token: 0x06004CAB RID: 19627 RVA: 0x0013F7F1 File Offset: 0x0013D9F1
		public string ErrorMessage
		{
			get
			{
				return this.m_errorMessage;
			}
		}

		// Token: 0x170016B5 RID: 5813
		// (get) Token: 0x06004CAC RID: 19628 RVA: 0x0013F7F9 File Offset: 0x0013D9F9
		public uint? ErrorEventId
		{
			get
			{
				return this.m_errorEventId;
			}
		}

		// Token: 0x170016B6 RID: 5814
		// (get) Token: 0x06004CAD RID: 19629 RVA: 0x0013F801 File Offset: 0x0013DA01
		public ExtendedErrorInfo ExtendedErrorInfo
		{
			get
			{
				return this.m_extendedErrorInfo;
			}
		}

		// Token: 0x170016B7 RID: 5815
		// (get) Token: 0x06004CAE RID: 19630 RVA: 0x0013F809 File Offset: 0x0013DA09
		public string SuspendComment
		{
			get
			{
				return this.m_suspendMessage;
			}
		}

		// Token: 0x170016B8 RID: 5816
		// (get) Token: 0x06004CAF RID: 19631 RVA: 0x0013F811 File Offset: 0x0013DA11
		public bool? RequiredLogsPresent
		{
			get
			{
				return this.m_requiredLogsPresent;
			}
		}

		// Token: 0x170016B9 RID: 5817
		// (get) Token: 0x06004CB0 RID: 19632 RVA: 0x0013F819 File Offset: 0x0013DA19
		public long SinglePageRestore
		{
			get
			{
				return this.m_singlePageRestore;
			}
		}

		// Token: 0x170016BA RID: 5818
		// (get) Token: 0x06004CB1 RID: 19633 RVA: 0x0013F821 File Offset: 0x0013DA21
		public ContentIndexStatusType ContentIndexState
		{
			get
			{
				return this.m_contentIndexState;
			}
		}

		// Token: 0x170016BB RID: 5819
		// (get) Token: 0x06004CB2 RID: 19634 RVA: 0x0013F829 File Offset: 0x0013DA29
		public string ContentIndexErrorMessage
		{
			get
			{
				return this.m_contentIndexErrorMessage;
			}
		}

		// Token: 0x170016BC RID: 5820
		// (get) Token: 0x06004CB3 RID: 19635 RVA: 0x0013F831 File Offset: 0x0013DA31
		public int? ContentIndexErrorCode
		{
			get
			{
				return this.m_contentIndexErrorCode;
			}
		}

		// Token: 0x170016BD RID: 5821
		// (get) Token: 0x06004CB4 RID: 19636 RVA: 0x0013F839 File Offset: 0x0013DA39
		public int? ContentIndexVersion
		{
			get
			{
				return this.m_contentIndexVersion;
			}
		}

		// Token: 0x170016BE RID: 5822
		// (get) Token: 0x06004CB5 RID: 19637 RVA: 0x0013F841 File Offset: 0x0013DA41
		public int? ContentIndexBacklog
		{
			get
			{
				return this.m_contentIndexBacklog;
			}
		}

		// Token: 0x170016BF RID: 5823
		// (get) Token: 0x06004CB6 RID: 19638 RVA: 0x0013F849 File Offset: 0x0013DA49
		public int? ContentIndexRetryQueueSize
		{
			get
			{
				return this.m_contentIndexRetryQueueSize;
			}
		}

		// Token: 0x170016C0 RID: 5824
		// (get) Token: 0x06004CB7 RID: 19639 RVA: 0x0013F851 File Offset: 0x0013DA51
		public int? ContentIndexMailboxesToCrawl
		{
			get
			{
				return this.m_contentIndexMailboxesToCrawl;
			}
		}

		// Token: 0x170016C1 RID: 5825
		// (get) Token: 0x06004CB8 RID: 19640 RVA: 0x0013F859 File Offset: 0x0013DA59
		public int? ContentIndexSeedingPercent
		{
			get
			{
				return this.m_contentIndexSeedingPercent;
			}
		}

		// Token: 0x170016C2 RID: 5826
		// (get) Token: 0x06004CB9 RID: 19641 RVA: 0x0013F861 File Offset: 0x0013DA61
		public string ContentIndexSeedingSource
		{
			get
			{
				return this.m_contentIndexSeedingSource;
			}
		}

		// Token: 0x170016C3 RID: 5827
		// (get) Token: 0x06004CBA RID: 19642 RVA: 0x0013F86C File Offset: 0x0013DA6C
		public string ContentIndexServerSource
		{
			get
			{
				string text = this.ContentIndexSeedingSource;
				if (string.IsNullOrEmpty(text))
				{
					return string.Empty;
				}
				int num = text.IndexOf(":");
				int num2 = text.IndexOf(".");
				int num3 = num2 - (num + 1);
				if (num2 != -1 && num3 > 0)
				{
					text = text.Substring(num + 1, num3);
				}
				return text;
			}
		}

		// Token: 0x170016C4 RID: 5828
		// (get) Token: 0x06004CBB RID: 19643 RVA: 0x0013F8C0 File Offset: 0x0013DAC0
		public long? CopyQueueLength
		{
			get
			{
				if (this.ActiveCopy)
				{
					return new long?(0L);
				}
				return new long?(Math.Max(0L, this.m_latestLogGenerationNumber - this.m_inspectorGenerationNumber));
			}
		}

		// Token: 0x170016C5 RID: 5829
		// (get) Token: 0x06004CBC RID: 19644 RVA: 0x0013F8EB File Offset: 0x0013DAEB
		public long? ReplayQueueLength
		{
			get
			{
				return new long?(Math.Max(0L, this.m_inspectorGenerationNumber - this.m_replayGenerationNumber));
			}
		}

		// Token: 0x170016C6 RID: 5830
		// (get) Token: 0x06004CBD RID: 19645 RVA: 0x0013F906 File Offset: 0x0013DB06
		public bool? ReplaySuspended
		{
			get
			{
				return this.m_replaySuspended;
			}
		}

		// Token: 0x170016C7 RID: 5831
		// (get) Token: 0x06004CBE RID: 19646 RVA: 0x0013F90E File Offset: 0x0013DB0E
		// (set) Token: 0x06004CBF RID: 19647 RVA: 0x0013F916 File Offset: 0x0013DB16
		public bool? ResumeBlocked { get; internal set; }

		// Token: 0x170016C8 RID: 5832
		// (get) Token: 0x06004CC0 RID: 19648 RVA: 0x0013F91F File Offset: 0x0013DB1F
		// (set) Token: 0x06004CC1 RID: 19649 RVA: 0x0013F927 File Offset: 0x0013DB27
		public bool? ReseedBlocked { get; internal set; }

		// Token: 0x170016C9 RID: 5833
		// (get) Token: 0x06004CC2 RID: 19650 RVA: 0x0013F930 File Offset: 0x0013DB30
		// (set) Token: 0x06004CC3 RID: 19651 RVA: 0x0013F938 File Offset: 0x0013DB38
		public string MinimumSupportedDatabaseSchemaVersion { get; internal set; }

		// Token: 0x170016CA RID: 5834
		// (get) Token: 0x06004CC4 RID: 19652 RVA: 0x0013F941 File Offset: 0x0013DB41
		// (set) Token: 0x06004CC5 RID: 19653 RVA: 0x0013F949 File Offset: 0x0013DB49
		public string MaximumSupportedDatabaseSchemaVersion { get; internal set; }

		// Token: 0x170016CB RID: 5835
		// (get) Token: 0x06004CC6 RID: 19654 RVA: 0x0013F952 File Offset: 0x0013DB52
		// (set) Token: 0x06004CC7 RID: 19655 RVA: 0x0013F95A File Offset: 0x0013DB5A
		public string RequestedDatabaseSchemaVersion { get; internal set; }

		// Token: 0x170016CC RID: 5836
		// (get) Token: 0x06004CC8 RID: 19656 RVA: 0x0013F963 File Offset: 0x0013DB63
		public DateTime? LatestAvailableLogTime
		{
			get
			{
				return this.m_latestAvailableLogTime;
			}
		}

		// Token: 0x170016CD RID: 5837
		// (get) Token: 0x06004CC9 RID: 19657 RVA: 0x0013F96B File Offset: 0x0013DB6B
		public DateTime? LastCopyNotificationedLogTime
		{
			get
			{
				return this.m_latestCopyNotificationTime;
			}
		}

		// Token: 0x170016CE RID: 5838
		// (get) Token: 0x06004CCA RID: 19658 RVA: 0x0013F973 File Offset: 0x0013DB73
		public DateTime? LastCopiedLogTime
		{
			get
			{
				return this.m_latestCopyTime;
			}
		}

		// Token: 0x170016CF RID: 5839
		// (get) Token: 0x06004CCB RID: 19659 RVA: 0x0013F97B File Offset: 0x0013DB7B
		public DateTime? LastInspectedLogTime
		{
			get
			{
				return this.m_latestInspectorTime;
			}
		}

		// Token: 0x170016D0 RID: 5840
		// (get) Token: 0x06004CCC RID: 19660 RVA: 0x0013F983 File Offset: 0x0013DB83
		public DateTime? LastReplayedLogTime
		{
			get
			{
				return this.m_latestReplayTime;
			}
		}

		// Token: 0x170016D1 RID: 5841
		// (get) Token: 0x06004CCD RID: 19661 RVA: 0x0013F98B File Offset: 0x0013DB8B
		public long LastLogGenerated
		{
			get
			{
				return this.m_latestLogGenerationNumber;
			}
		}

		// Token: 0x170016D2 RID: 5842
		// (get) Token: 0x06004CCE RID: 19662 RVA: 0x0013F993 File Offset: 0x0013DB93
		public long LastLogCopyNotified
		{
			get
			{
				return this.m_copyNotificationGenerationNumber;
			}
		}

		// Token: 0x170016D3 RID: 5843
		// (get) Token: 0x06004CCF RID: 19663 RVA: 0x0013F99B File Offset: 0x0013DB9B
		public long LastLogCopied
		{
			get
			{
				return this.m_copyGenerationNumber;
			}
		}

		// Token: 0x170016D4 RID: 5844
		// (get) Token: 0x06004CD0 RID: 19664 RVA: 0x0013F9A3 File Offset: 0x0013DBA3
		public long LastLogInspected
		{
			get
			{
				return this.m_inspectorGenerationNumber;
			}
		}

		// Token: 0x170016D5 RID: 5845
		// (get) Token: 0x06004CD1 RID: 19665 RVA: 0x0013F9AB File Offset: 0x0013DBAB
		public long LastLogReplayed
		{
			get
			{
				return this.m_replayGenerationNumber;
			}
		}

		// Token: 0x170016D6 RID: 5846
		// (get) Token: 0x06004CD2 RID: 19666 RVA: 0x0013F9B3 File Offset: 0x0013DBB3
		// (set) Token: 0x06004CD3 RID: 19667 RVA: 0x0013F9BB File Offset: 0x0013DBBB
		public long LowestLogPresent { get; set; }

		// Token: 0x170016D7 RID: 5847
		// (get) Token: 0x06004CD4 RID: 19668 RVA: 0x0013F9C4 File Offset: 0x0013DBC4
		// (set) Token: 0x06004CD5 RID: 19669 RVA: 0x0013F9CC File Offset: 0x0013DBCC
		public bool LastLogInfoIsStale { get; set; }

		// Token: 0x170016D8 RID: 5848
		// (get) Token: 0x06004CD6 RID: 19670 RVA: 0x0013F9D5 File Offset: 0x0013DBD5
		// (set) Token: 0x06004CD7 RID: 19671 RVA: 0x0013F9DD File Offset: 0x0013DBDD
		public DateTime? LastLogInfoFromCopierTime { get; set; }

		// Token: 0x170016D9 RID: 5849
		// (get) Token: 0x06004CD8 RID: 19672 RVA: 0x0013F9E6 File Offset: 0x0013DBE6
		// (set) Token: 0x06004CD9 RID: 19673 RVA: 0x0013F9EE File Offset: 0x0013DBEE
		public DateTime? LastLogInfoFromClusterTime { get; set; }

		// Token: 0x170016DA RID: 5850
		// (get) Token: 0x06004CDA RID: 19674 RVA: 0x0013F9F7 File Offset: 0x0013DBF7
		// (set) Token: 0x06004CDB RID: 19675 RVA: 0x0013F9FF File Offset: 0x0013DBFF
		public long LastLogInfoFromClusterGen { get; set; }

		// Token: 0x170016DB RID: 5851
		// (get) Token: 0x06004CDC RID: 19676 RVA: 0x0013FA08 File Offset: 0x0013DC08
		// (set) Token: 0x06004CDD RID: 19677 RVA: 0x0013FA10 File Offset: 0x0013DC10
		public bool ReplicationIsInBlockMode { get; set; }

		// Token: 0x170016DC RID: 5852
		// (get) Token: 0x06004CDE RID: 19678 RVA: 0x0013FA19 File Offset: 0x0013DC19
		// (set) Token: 0x06004CDF RID: 19679 RVA: 0x0013FA21 File Offset: 0x0013DC21
		public bool ActivationDisabledAndMoveNow { get; set; }

		// Token: 0x170016DD RID: 5853
		// (get) Token: 0x06004CE0 RID: 19680 RVA: 0x0013FA2A File Offset: 0x0013DC2A
		// (set) Token: 0x06004CE1 RID: 19681 RVA: 0x0013FA32 File Offset: 0x0013DC32
		public DatabaseCopyAutoActivationPolicyType AutoActivationPolicy { get; set; }

		// Token: 0x170016DE RID: 5854
		// (get) Token: 0x06004CE2 RID: 19682 RVA: 0x0013FA3B File Offset: 0x0013DC3B
		public long? LogsReplayedSinceInstanceStart
		{
			get
			{
				return this.m_logsReplayedSinceInstanceStart;
			}
		}

		// Token: 0x170016DF RID: 5855
		// (get) Token: 0x06004CE3 RID: 19683 RVA: 0x0013FA43 File Offset: 0x0013DC43
		public long? LogsCopiedSinceInstanceStart
		{
			get
			{
				return this.m_logsCopiedSinceInstanceStart;
			}
		}

		// Token: 0x170016E0 RID: 5856
		// (get) Token: 0x06004CE4 RID: 19684 RVA: 0x0013FA4B File Offset: 0x0013DC4B
		public DateTime? LatestFullBackupTime
		{
			get
			{
				return this.m_latestFullBackupTime;
			}
		}

		// Token: 0x170016E1 RID: 5857
		// (get) Token: 0x06004CE5 RID: 19685 RVA: 0x0013FA53 File Offset: 0x0013DC53
		public DateTime? LatestIncrementalBackupTime
		{
			get
			{
				return this.m_latestIncrementalBackupTime;
			}
		}

		// Token: 0x170016E2 RID: 5858
		// (get) Token: 0x06004CE6 RID: 19686 RVA: 0x0013FA5B File Offset: 0x0013DC5B
		public DateTime? LatestDifferentialBackupTime
		{
			get
			{
				return this.m_latestDifferentialBackupTime;
			}
		}

		// Token: 0x170016E3 RID: 5859
		// (get) Token: 0x06004CE7 RID: 19687 RVA: 0x0013FA63 File Offset: 0x0013DC63
		public DateTime? LatestCopyBackupTime
		{
			get
			{
				return this.m_latestCopyBackupTime;
			}
		}

		// Token: 0x170016E4 RID: 5860
		// (get) Token: 0x06004CE8 RID: 19688 RVA: 0x0013FA6B File Offset: 0x0013DC6B
		public bool? SnapshotBackup
		{
			get
			{
				return this.m_snapshotBackup;
			}
		}

		// Token: 0x170016E5 RID: 5861
		// (get) Token: 0x06004CE9 RID: 19689 RVA: 0x0013FA73 File Offset: 0x0013DC73
		public bool? SnapshotLatestFullBackup
		{
			get
			{
				return this.m_snapshotLatestFullBackup;
			}
		}

		// Token: 0x170016E6 RID: 5862
		// (get) Token: 0x06004CEA RID: 19690 RVA: 0x0013FA7B File Offset: 0x0013DC7B
		public bool? SnapshotLatestIncrementalBackup
		{
			get
			{
				return this.m_snapshotLatestIncrementalBackup;
			}
		}

		// Token: 0x170016E7 RID: 5863
		// (get) Token: 0x06004CEB RID: 19691 RVA: 0x0013FA83 File Offset: 0x0013DC83
		public bool? SnapshotLatestDifferentialBackup
		{
			get
			{
				return this.m_snapshotLatestDifferentialBackup;
			}
		}

		// Token: 0x170016E8 RID: 5864
		// (get) Token: 0x06004CEC RID: 19692 RVA: 0x0013FA8B File Offset: 0x0013DC8B
		public bool? SnapshotLatestCopyBackup
		{
			get
			{
				return this.m_snapshotLatestCopyBackup;
			}
		}

		// Token: 0x170016E9 RID: 5865
		// (get) Token: 0x06004CED RID: 19693 RVA: 0x0013FA94 File Offset: 0x0013DC94
		public bool? LogReplayQueueIncreasing
		{
			get
			{
				return new bool?(this.m_logReplayQueueIncreasing ?? false);
			}
		}

		// Token: 0x170016EA RID: 5866
		// (get) Token: 0x06004CEE RID: 19694 RVA: 0x0013FAC0 File Offset: 0x0013DCC0
		public bool? LogCopyQueueIncreasing
		{
			get
			{
				return new bool?(this.m_logCopyQueueIncreasing ?? false);
			}
		}

		// Token: 0x170016EB RID: 5867
		// (get) Token: 0x06004CEF RID: 19695 RVA: 0x0013FAEC File Offset: 0x0013DCEC
		public ReplayLagStatus ReplayLagStatus
		{
			get
			{
				return this.m_replayLagStatus;
			}
		}

		// Token: 0x170016EC RID: 5868
		// (get) Token: 0x06004CF0 RID: 19696 RVA: 0x0013FAF4 File Offset: 0x0013DCF4
		public DatabaseSeedStatus DatabaseSeedStatus
		{
			get
			{
				return this.m_dbSeedStatus;
			}
		}

		// Token: 0x170016ED RID: 5869
		// (get) Token: 0x06004CF1 RID: 19697 RVA: 0x0013FAFC File Offset: 0x0013DCFC
		public DumpsterRequestEntry[] OutstandingDumpsterRequests
		{
			get
			{
				return this.m_outstandingDumpsterRequests;
			}
		}

		// Token: 0x170016EE RID: 5870
		// (get) Token: 0x06004CF2 RID: 19698 RVA: 0x0013FB04 File Offset: 0x0013DD04
		public ConnectionStatus[] OutgoingConnections
		{
			get
			{
				return this.m_outgoingConnections;
			}
		}

		// Token: 0x170016EF RID: 5871
		// (get) Token: 0x06004CF3 RID: 19699 RVA: 0x0013FB0C File Offset: 0x0013DD0C
		public ConnectionStatus IncomingLogCopyingNetwork
		{
			get
			{
				return this.m_incomingLogCopyingNetwork;
			}
		}

		// Token: 0x170016F0 RID: 5872
		// (get) Token: 0x06004CF4 RID: 19700 RVA: 0x0013FB14 File Offset: 0x0013DD14
		public ConnectionStatus SeedingNetwork
		{
			get
			{
				return this.m_seedingNetwork;
			}
		}

		// Token: 0x170016F1 RID: 5873
		// (get) Token: 0x06004CF5 RID: 19701 RVA: 0x0013FB1C File Offset: 0x0013DD1C
		// (set) Token: 0x06004CF6 RID: 19702 RVA: 0x0013FB24 File Offset: 0x0013DD24
		public int DiskFreeSpacePercent { get; internal set; }

		// Token: 0x170016F2 RID: 5874
		// (get) Token: 0x06004CF7 RID: 19703 RVA: 0x0013FB2D File Offset: 0x0013DD2D
		// (set) Token: 0x06004CF8 RID: 19704 RVA: 0x0013FB35 File Offset: 0x0013DD35
		public ByteQuantifiedSize DiskFreeSpace { get; internal set; }

		// Token: 0x170016F3 RID: 5875
		// (get) Token: 0x06004CF9 RID: 19705 RVA: 0x0013FB3E File Offset: 0x0013DD3E
		// (set) Token: 0x06004CFA RID: 19706 RVA: 0x0013FB46 File Offset: 0x0013DD46
		public ByteQuantifiedSize DiskTotalSpace { get; internal set; }

		// Token: 0x170016F4 RID: 5876
		// (get) Token: 0x06004CFB RID: 19707 RVA: 0x0013FB4F File Offset: 0x0013DD4F
		// (set) Token: 0x06004CFC RID: 19708 RVA: 0x0013FB57 File Offset: 0x0013DD57
		public string ExchangeVolumeMountPoint { get; internal set; }

		// Token: 0x170016F5 RID: 5877
		// (get) Token: 0x06004CFD RID: 19709 RVA: 0x0013FB60 File Offset: 0x0013DD60
		// (set) Token: 0x06004CFE RID: 19710 RVA: 0x0013FB68 File Offset: 0x0013DD68
		public string DatabaseVolumeMountPoint { get; internal set; }

		// Token: 0x170016F6 RID: 5878
		// (get) Token: 0x06004CFF RID: 19711 RVA: 0x0013FB71 File Offset: 0x0013DD71
		// (set) Token: 0x06004D00 RID: 19712 RVA: 0x0013FB79 File Offset: 0x0013DD79
		public string DatabaseVolumeName { get; internal set; }

		// Token: 0x170016F7 RID: 5879
		// (get) Token: 0x06004D01 RID: 19713 RVA: 0x0013FB82 File Offset: 0x0013DD82
		// (set) Token: 0x06004D02 RID: 19714 RVA: 0x0013FB8A File Offset: 0x0013DD8A
		public bool? DatabasePathIsOnMountedFolder { get; internal set; }

		// Token: 0x170016F8 RID: 5880
		// (get) Token: 0x06004D03 RID: 19715 RVA: 0x0013FB93 File Offset: 0x0013DD93
		// (set) Token: 0x06004D04 RID: 19716 RVA: 0x0013FB9B File Offset: 0x0013DD9B
		public string LogVolumeMountPoint { get; internal set; }

		// Token: 0x170016F9 RID: 5881
		// (get) Token: 0x06004D05 RID: 19717 RVA: 0x0013FBA4 File Offset: 0x0013DDA4
		// (set) Token: 0x06004D06 RID: 19718 RVA: 0x0013FBAC File Offset: 0x0013DDAC
		public string LogVolumeName { get; internal set; }

		// Token: 0x170016FA RID: 5882
		// (get) Token: 0x06004D07 RID: 19719 RVA: 0x0013FBB5 File Offset: 0x0013DDB5
		// (set) Token: 0x06004D08 RID: 19720 RVA: 0x0013FBBD File Offset: 0x0013DDBD
		public bool? LogPathIsOnMountedFolder { get; internal set; }

		// Token: 0x170016FB RID: 5883
		// (get) Token: 0x06004D09 RID: 19721 RVA: 0x0013FBC6 File Offset: 0x0013DDC6
		// (set) Token: 0x06004D0A RID: 19722 RVA: 0x0013FBCE File Offset: 0x0013DDCE
		public string LastDatabaseVolumeName { get; internal set; }

		// Token: 0x170016FC RID: 5884
		// (get) Token: 0x06004D0B RID: 19723 RVA: 0x0013FBD7 File Offset: 0x0013DDD7
		// (set) Token: 0x06004D0C RID: 19724 RVA: 0x0013FBDF File Offset: 0x0013DDDF
		public DateTime? LastDatabaseVolumeNameTransitionTime { get; internal set; }

		// Token: 0x170016FD RID: 5885
		// (get) Token: 0x06004D0D RID: 19725 RVA: 0x0013FBE8 File Offset: 0x0013DDE8
		// (set) Token: 0x06004D0E RID: 19726 RVA: 0x0013FBF0 File Offset: 0x0013DDF0
		public string VolumeInfoError { get; internal set; }

		// Token: 0x170016FE RID: 5886
		// (get) Token: 0x06004D0F RID: 19727 RVA: 0x0013FBF9 File Offset: 0x0013DDF9
		// (set) Token: 0x06004D10 RID: 19728 RVA: 0x0013FC01 File Offset: 0x0013DE01
		public long MaxLogToReplay { get; internal set; }

		// Token: 0x170016FF RID: 5887
		// (get) Token: 0x06004D11 RID: 19729 RVA: 0x0013FC0A File Offset: 0x0013DE0A
		internal bool IsValid
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17001700 RID: 5888
		// (get) Token: 0x06004D12 RID: 19730 RVA: 0x0013FC0D File Offset: 0x0013DE0D
		bool IConfigurable.IsValid
		{
			get
			{
				return this.IsValid;
			}
		}

		// Token: 0x17001701 RID: 5889
		// (get) Token: 0x06004D13 RID: 19731 RVA: 0x0013FC15 File Offset: 0x0013DE15
		internal ObjectState ObjectState
		{
			get
			{
				return ObjectState.Unchanged;
			}
		}

		// Token: 0x17001702 RID: 5890
		// (get) Token: 0x06004D14 RID: 19732 RVA: 0x0013FC18 File Offset: 0x0013DE18
		ObjectState IConfigurable.ObjectState
		{
			get
			{
				return this.ObjectState;
			}
		}

		// Token: 0x06004D15 RID: 19733 RVA: 0x0013FC20 File Offset: 0x0013DE20
		public ValidationError[] Validate()
		{
			return new ValidationError[0];
		}

		// Token: 0x06004D16 RID: 19734 RVA: 0x0013FC28 File Offset: 0x0013DE28
		public void CopyChangesFrom(IConfigurable source)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06004D17 RID: 19735 RVA: 0x0013FC2F File Offset: 0x0013DE2F
		public void ResetChangeTracking()
		{
			throw new NotImplementedException();
		}

		// Token: 0x04002DB7 RID: 11703
		internal ADObjectId m_identity;

		// Token: 0x04002DB8 RID: 11704
		internal string m_name;

		// Token: 0x04002DB9 RID: 11705
		internal string m_databaseName;

		// Token: 0x04002DBA RID: 11706
		internal CopyStatus m_copyStatus;

		// Token: 0x04002DBB RID: 11707
		internal DateTime? m_lastStatusTransitionTime;

		// Token: 0x04002DBC RID: 11708
		internal string m_mailboxServer;

		// Token: 0x04002DBD RID: 11709
		internal string m_activeDatabaseCopy;

		// Token: 0x04002DBE RID: 11710
		private bool m_isActiveCopy;

		// Token: 0x04002DBF RID: 11711
		internal DateTime? m_statusRetrievedTime;

		// Token: 0x04002DC0 RID: 11712
		internal int? m_workerProcessId;

		// Token: 0x04002DC1 RID: 11713
		internal bool m_activationSuspended;

		// Token: 0x04002DC2 RID: 11714
		internal ActionInitiatorType m_actionInitiator;

		// Token: 0x04002DC3 RID: 11715
		internal string m_errorMessage;

		// Token: 0x04002DC4 RID: 11716
		internal uint? m_errorEventId;

		// Token: 0x04002DC5 RID: 11717
		internal ExtendedErrorInfo m_extendedErrorInfo;

		// Token: 0x04002DC6 RID: 11718
		internal string m_suspendMessage;

		// Token: 0x04002DC7 RID: 11719
		internal bool? m_requiredLogsPresent;

		// Token: 0x04002DC8 RID: 11720
		internal long m_singlePageRestore;

		// Token: 0x04002DC9 RID: 11721
		internal ContentIndexStatusType m_contentIndexState;

		// Token: 0x04002DCA RID: 11722
		internal string m_contentIndexErrorMessage;

		// Token: 0x04002DCB RID: 11723
		internal int? m_contentIndexErrorCode;

		// Token: 0x04002DCC RID: 11724
		internal int? m_contentIndexVersion;

		// Token: 0x04002DCD RID: 11725
		internal int? m_contentIndexBacklog;

		// Token: 0x04002DCE RID: 11726
		internal int? m_contentIndexRetryQueueSize;

		// Token: 0x04002DCF RID: 11727
		internal int? m_contentIndexMailboxesToCrawl;

		// Token: 0x04002DD0 RID: 11728
		internal int? m_contentIndexSeedingPercent;

		// Token: 0x04002DD1 RID: 11729
		internal string m_contentIndexSeedingSource;

		// Token: 0x04002DD2 RID: 11730
		internal bool? m_replaySuspended;

		// Token: 0x04002DD3 RID: 11731
		internal DateTime? m_latestAvailableLogTime;

		// Token: 0x04002DD4 RID: 11732
		internal DateTime? m_latestCopyNotificationTime;

		// Token: 0x04002DD5 RID: 11733
		internal DateTime? m_latestCopyTime;

		// Token: 0x04002DD6 RID: 11734
		internal DateTime? m_latestInspectorTime;

		// Token: 0x04002DD7 RID: 11735
		internal DateTime? m_latestReplayTime;

		// Token: 0x04002DD8 RID: 11736
		internal long m_latestLogGenerationNumber;

		// Token: 0x04002DD9 RID: 11737
		internal long m_copyNotificationGenerationNumber;

		// Token: 0x04002DDA RID: 11738
		internal long m_copyGenerationNumber;

		// Token: 0x04002DDB RID: 11739
		internal long m_inspectorGenerationNumber;

		// Token: 0x04002DDC RID: 11740
		internal long m_replayGenerationNumber;

		// Token: 0x04002DDD RID: 11741
		internal long? m_logsReplayedSinceInstanceStart;

		// Token: 0x04002DDE RID: 11742
		internal long? m_logsCopiedSinceInstanceStart;

		// Token: 0x04002DDF RID: 11743
		internal DateTime? m_latestFullBackupTime;

		// Token: 0x04002DE0 RID: 11744
		internal DateTime? m_latestIncrementalBackupTime;

		// Token: 0x04002DE1 RID: 11745
		internal DateTime? m_latestDifferentialBackupTime;

		// Token: 0x04002DE2 RID: 11746
		internal DateTime? m_latestCopyBackupTime;

		// Token: 0x04002DE3 RID: 11747
		internal bool? m_snapshotBackup;

		// Token: 0x04002DE4 RID: 11748
		internal bool? m_snapshotLatestFullBackup;

		// Token: 0x04002DE5 RID: 11749
		internal bool? m_snapshotLatestIncrementalBackup;

		// Token: 0x04002DE6 RID: 11750
		internal bool? m_snapshotLatestDifferentialBackup;

		// Token: 0x04002DE7 RID: 11751
		internal bool? m_snapshotLatestCopyBackup;

		// Token: 0x04002DE8 RID: 11752
		internal bool? m_logReplayQueueIncreasing;

		// Token: 0x04002DE9 RID: 11753
		internal bool? m_logCopyQueueIncreasing;

		// Token: 0x04002DEA RID: 11754
		internal ReplayLagStatus m_replayLagStatus;

		// Token: 0x04002DEB RID: 11755
		internal DatabaseSeedStatus m_dbSeedStatus;

		// Token: 0x04002DEC RID: 11756
		internal DumpsterRequestEntry[] m_outstandingDumpsterRequests;

		// Token: 0x04002DED RID: 11757
		internal ConnectionStatus[] m_outgoingConnections;

		// Token: 0x04002DEE RID: 11758
		internal ConnectionStatus m_incomingLogCopyingNetwork;

		// Token: 0x04002DEF RID: 11759
		internal ConnectionStatus m_seedingNetwork;
	}
}
