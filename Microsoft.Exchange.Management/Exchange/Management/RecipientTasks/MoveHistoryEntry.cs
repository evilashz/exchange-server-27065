using System;
using System.Collections.Generic;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.MailboxReplicationService;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.RecipientTasks
{
	// Token: 0x02000CC4 RID: 3268
	[Serializable]
	public sealed class MoveHistoryEntry
	{
		// Token: 0x06007D66 RID: 32102 RVA: 0x00200870 File Offset: 0x001FEA70
		internal MoveHistoryEntry(MoveHistoryEntryInternal mhEntry, bool includeMoveReport)
		{
			this.Status = (RequestStatus)mhEntry.Status;
			this.Flags = (RequestFlags)mhEntry.Flags;
			this.SourceDatabase = ADObjectIdXML.Deserialize(mhEntry.SourceDatabase);
			this.SourceVersion = new ServerVersion(mhEntry.SourceVersion);
			this.SourceServer = mhEntry.SourceServer;
			this.SourceArchiveDatabase = ADObjectIdXML.Deserialize(mhEntry.SourceArchiveDatabase);
			this.SourceArchiveVersion = new ServerVersion(mhEntry.SourceArchiveVersion);
			this.SourceArchiveServer = mhEntry.SourceArchiveServer;
			this.TargetDatabase = ADObjectIdXML.Deserialize(mhEntry.DestinationDatabase);
			this.TargetVersion = new ServerVersion(mhEntry.DestinationVersion);
			this.TargetServer = mhEntry.DestinationServer;
			this.TargetArchiveDatabase = ADObjectIdXML.Deserialize(mhEntry.DestinationArchiveDatabase);
			this.TargetArchiveVersion = new ServerVersion(mhEntry.DestinationArchiveVersion);
			this.TargetArchiveServer = mhEntry.DestinationArchiveServer;
			this.RemoteHostName = mhEntry.RemoteHostName;
			this.RemoteCredentialUsername = mhEntry.RemoteCredentialUserName;
			this.RemoteDatabaseName = mhEntry.RemoteDatabaseName;
			this.RemoteArchiveDatabaseName = mhEntry.RemoteArchiveDatabaseName;
			this.BadItemLimit = mhEntry.BadItemLimit;
			this.BadItemsEncountered = mhEntry.BadItemsEncountered;
			this.LargeItemLimit = mhEntry.LargeItemLimit;
			this.LargeItemsEncountered = mhEntry.LargeItemsEncountered;
			this.MRSServerName = mhEntry.MRSServerName;
			this.TotalMailboxSize = new ByteQuantifiedSize(mhEntry.TotalMailboxSize);
			this.TotalMailboxItemCount = mhEntry.TotalMailboxItemCount;
			this.TotalArchiveSize = ((mhEntry.TotalArchiveSize != null) ? new ByteQuantifiedSize?(new ByteQuantifiedSize(mhEntry.TotalArchiveSize.Value)) : null);
			this.TotalArchiveItemCount = mhEntry.TotalArchiveItemCount;
			this.TargetDeliveryDomain = mhEntry.TargetDeliveryDomain;
			this.ArchiveDomain = mhEntry.ArchiveDomain;
			this.FailureCode = mhEntry.FailureCode;
			this.FailureType = mhEntry.FailureType;
			this.Message = CommonUtils.ByteDeserialize(mhEntry.MessageData);
			this.timeTracker = mhEntry.TimeTracker;
			if (includeMoveReport)
			{
				this.Report = mhEntry.Report;
			}
		}

		// Token: 0x170026EE RID: 9966
		// (get) Token: 0x06007D67 RID: 32103 RVA: 0x00200A7D File Offset: 0x001FEC7D
		// (set) Token: 0x06007D68 RID: 32104 RVA: 0x00200A85 File Offset: 0x001FEC85
		public RequestStatus Status { get; private set; }

		// Token: 0x170026EF RID: 9967
		// (get) Token: 0x06007D69 RID: 32105 RVA: 0x00200A8E File Offset: 0x001FEC8E
		// (set) Token: 0x06007D6A RID: 32106 RVA: 0x00200A96 File Offset: 0x001FEC96
		public RequestFlags Flags { get; private set; }

		// Token: 0x170026F0 RID: 9968
		// (get) Token: 0x06007D6B RID: 32107 RVA: 0x00200A9F File Offset: 0x001FEC9F
		public RequestStyle RequestStyle
		{
			get
			{
				if ((this.Flags & RequestFlags.CrossOrg) == RequestFlags.None)
				{
					return RequestStyle.IntraOrg;
				}
				return RequestStyle.CrossOrg;
			}
		}

		// Token: 0x170026F1 RID: 9969
		// (get) Token: 0x06007D6C RID: 32108 RVA: 0x00200AAE File Offset: 0x001FECAE
		public RequestDirection Direction
		{
			get
			{
				if ((this.Flags & RequestFlags.Push) == RequestFlags.None)
				{
					return RequestDirection.Pull;
				}
				return RequestDirection.Push;
			}
		}

		// Token: 0x170026F2 RID: 9970
		// (get) Token: 0x06007D6D RID: 32109 RVA: 0x00200ABD File Offset: 0x001FECBD
		public bool IsOffline
		{
			get
			{
				return (this.Flags & RequestFlags.Offline) != RequestFlags.None;
			}
		}

		// Token: 0x170026F3 RID: 9971
		// (get) Token: 0x06007D6E RID: 32110 RVA: 0x00200ACE File Offset: 0x001FECCE
		// (set) Token: 0x06007D6F RID: 32111 RVA: 0x00200AD6 File Offset: 0x001FECD6
		public ADObjectId SourceDatabase { get; private set; }

		// Token: 0x170026F4 RID: 9972
		// (get) Token: 0x06007D70 RID: 32112 RVA: 0x00200ADF File Offset: 0x001FECDF
		// (set) Token: 0x06007D71 RID: 32113 RVA: 0x00200AE7 File Offset: 0x001FECE7
		public ServerVersion SourceVersion { get; private set; }

		// Token: 0x170026F5 RID: 9973
		// (get) Token: 0x06007D72 RID: 32114 RVA: 0x00200AF0 File Offset: 0x001FECF0
		// (set) Token: 0x06007D73 RID: 32115 RVA: 0x00200AF8 File Offset: 0x001FECF8
		public string SourceServer { get; private set; }

		// Token: 0x170026F6 RID: 9974
		// (get) Token: 0x06007D74 RID: 32116 RVA: 0x00200B01 File Offset: 0x001FED01
		// (set) Token: 0x06007D75 RID: 32117 RVA: 0x00200B09 File Offset: 0x001FED09
		public ADObjectId SourceArchiveDatabase { get; private set; }

		// Token: 0x170026F7 RID: 9975
		// (get) Token: 0x06007D76 RID: 32118 RVA: 0x00200B12 File Offset: 0x001FED12
		// (set) Token: 0x06007D77 RID: 32119 RVA: 0x00200B1A File Offset: 0x001FED1A
		public ServerVersion SourceArchiveVersion { get; private set; }

		// Token: 0x170026F8 RID: 9976
		// (get) Token: 0x06007D78 RID: 32120 RVA: 0x00200B23 File Offset: 0x001FED23
		// (set) Token: 0x06007D79 RID: 32121 RVA: 0x00200B2B File Offset: 0x001FED2B
		public string SourceArchiveServer { get; private set; }

		// Token: 0x170026F9 RID: 9977
		// (get) Token: 0x06007D7A RID: 32122 RVA: 0x00200B34 File Offset: 0x001FED34
		// (set) Token: 0x06007D7B RID: 32123 RVA: 0x00200B3C File Offset: 0x001FED3C
		public ADObjectId TargetDatabase { get; private set; }

		// Token: 0x170026FA RID: 9978
		// (get) Token: 0x06007D7C RID: 32124 RVA: 0x00200B45 File Offset: 0x001FED45
		// (set) Token: 0x06007D7D RID: 32125 RVA: 0x00200B4D File Offset: 0x001FED4D
		public ServerVersion TargetVersion { get; private set; }

		// Token: 0x170026FB RID: 9979
		// (get) Token: 0x06007D7E RID: 32126 RVA: 0x00200B56 File Offset: 0x001FED56
		// (set) Token: 0x06007D7F RID: 32127 RVA: 0x00200B5E File Offset: 0x001FED5E
		public string TargetServer { get; private set; }

		// Token: 0x170026FC RID: 9980
		// (get) Token: 0x06007D80 RID: 32128 RVA: 0x00200B67 File Offset: 0x001FED67
		// (set) Token: 0x06007D81 RID: 32129 RVA: 0x00200B6F File Offset: 0x001FED6F
		public ADObjectId TargetArchiveDatabase { get; private set; }

		// Token: 0x170026FD RID: 9981
		// (get) Token: 0x06007D82 RID: 32130 RVA: 0x00200B78 File Offset: 0x001FED78
		// (set) Token: 0x06007D83 RID: 32131 RVA: 0x00200B80 File Offset: 0x001FED80
		public ServerVersion TargetArchiveVersion { get; private set; }

		// Token: 0x170026FE RID: 9982
		// (get) Token: 0x06007D84 RID: 32132 RVA: 0x00200B89 File Offset: 0x001FED89
		// (set) Token: 0x06007D85 RID: 32133 RVA: 0x00200B91 File Offset: 0x001FED91
		public string TargetArchiveServer { get; private set; }

		// Token: 0x170026FF RID: 9983
		// (get) Token: 0x06007D86 RID: 32134 RVA: 0x00200B9A File Offset: 0x001FED9A
		// (set) Token: 0x06007D87 RID: 32135 RVA: 0x00200BA2 File Offset: 0x001FEDA2
		public string RemoteHostName { get; private set; }

		// Token: 0x17002700 RID: 9984
		// (get) Token: 0x06007D88 RID: 32136 RVA: 0x00200BAC File Offset: 0x001FEDAC
		// (set) Token: 0x06007D89 RID: 32137 RVA: 0x00200BD6 File Offset: 0x001FEDD6
		public string RemoteCredentialUsername
		{
			get
			{
				if (SuppressingPiiContext.NeedPiiSuppression)
				{
					string text;
					string text2;
					return SuppressingPiiData.Redact(this.remoteCredentialUsername, out text, out text2);
				}
				return this.remoteCredentialUsername;
			}
			private set
			{
				this.remoteCredentialUsername = value;
			}
		}

		// Token: 0x17002701 RID: 9985
		// (get) Token: 0x06007D8A RID: 32138 RVA: 0x00200BDF File Offset: 0x001FEDDF
		// (set) Token: 0x06007D8B RID: 32139 RVA: 0x00200BE7 File Offset: 0x001FEDE7
		public string RemoteDatabaseName { get; private set; }

		// Token: 0x17002702 RID: 9986
		// (get) Token: 0x06007D8C RID: 32140 RVA: 0x00200BF0 File Offset: 0x001FEDF0
		// (set) Token: 0x06007D8D RID: 32141 RVA: 0x00200BF8 File Offset: 0x001FEDF8
		public string RemoteArchiveDatabaseName { get; private set; }

		// Token: 0x17002703 RID: 9987
		// (get) Token: 0x06007D8E RID: 32142 RVA: 0x00200C01 File Offset: 0x001FEE01
		// (set) Token: 0x06007D8F RID: 32143 RVA: 0x00200C09 File Offset: 0x001FEE09
		public Unlimited<int> BadItemLimit { get; private set; }

		// Token: 0x17002704 RID: 9988
		// (get) Token: 0x06007D90 RID: 32144 RVA: 0x00200C12 File Offset: 0x001FEE12
		// (set) Token: 0x06007D91 RID: 32145 RVA: 0x00200C1A File Offset: 0x001FEE1A
		public int BadItemsEncountered { get; private set; }

		// Token: 0x17002705 RID: 9989
		// (get) Token: 0x06007D92 RID: 32146 RVA: 0x00200C23 File Offset: 0x001FEE23
		// (set) Token: 0x06007D93 RID: 32147 RVA: 0x00200C2B File Offset: 0x001FEE2B
		public Unlimited<int> LargeItemLimit { get; private set; }

		// Token: 0x17002706 RID: 9990
		// (get) Token: 0x06007D94 RID: 32148 RVA: 0x00200C34 File Offset: 0x001FEE34
		// (set) Token: 0x06007D95 RID: 32149 RVA: 0x00200C3C File Offset: 0x001FEE3C
		public int LargeItemsEncountered { get; private set; }

		// Token: 0x17002707 RID: 9991
		// (get) Token: 0x06007D96 RID: 32150 RVA: 0x00200C45 File Offset: 0x001FEE45
		public DateTime? QueuedTimestamp
		{
			get
			{
				return this.GetTimestamp(RequestJobTimestamp.Creation);
			}
		}

		// Token: 0x17002708 RID: 9992
		// (get) Token: 0x06007D97 RID: 32151 RVA: 0x00200C4E File Offset: 0x001FEE4E
		public DateTime? StartTimestamp
		{
			get
			{
				return this.GetTimestamp(RequestJobTimestamp.Start);
			}
		}

		// Token: 0x17002709 RID: 9993
		// (get) Token: 0x06007D98 RID: 32152 RVA: 0x00200C57 File Offset: 0x001FEE57
		public DateTime? InitialSeedingCompletedTimestamp
		{
			get
			{
				return this.GetTimestamp(RequestJobTimestamp.InitialSeedingCompleted);
			}
		}

		// Token: 0x1700270A RID: 9994
		// (get) Token: 0x06007D99 RID: 32153 RVA: 0x00200C60 File Offset: 0x001FEE60
		public DateTime? FinalSyncTimestamp
		{
			get
			{
				return this.GetTimestamp(RequestJobTimestamp.FinalSync);
			}
		}

		// Token: 0x1700270B RID: 9995
		// (get) Token: 0x06007D9A RID: 32154 RVA: 0x00200C69 File Offset: 0x001FEE69
		public DateTime? CompletionTimestamp
		{
			get
			{
				return this.GetTimestamp(RequestJobTimestamp.Completion);
			}
		}

		// Token: 0x1700270C RID: 9996
		// (get) Token: 0x06007D9B RID: 32155 RVA: 0x00200C72 File Offset: 0x001FEE72
		public EnhancedTimeSpan? OverallDuration
		{
			get
			{
				return this.GetDuration(RequestState.OverallMove);
			}
		}

		// Token: 0x1700270D RID: 9997
		// (get) Token: 0x06007D9C RID: 32156 RVA: 0x00200C7B File Offset: 0x001FEE7B
		public EnhancedTimeSpan? TotalFinalizationDuration
		{
			get
			{
				return this.GetDuration(RequestState.Finalization);
			}
		}

		// Token: 0x1700270E RID: 9998
		// (get) Token: 0x06007D9D RID: 32157 RVA: 0x00200C85 File Offset: 0x001FEE85
		public EnhancedTimeSpan? TotalSuspendedDuration
		{
			get
			{
				return this.GetDuration(RequestState.Suspended);
			}
		}

		// Token: 0x1700270F RID: 9999
		// (get) Token: 0x06007D9E RID: 32158 RVA: 0x00200C8F File Offset: 0x001FEE8F
		public EnhancedTimeSpan? TotalFailedDuration
		{
			get
			{
				return this.GetDuration(RequestState.Failed);
			}
		}

		// Token: 0x17002710 RID: 10000
		// (get) Token: 0x06007D9F RID: 32159 RVA: 0x00200C99 File Offset: 0x001FEE99
		public EnhancedTimeSpan? TotalQueuedDuration
		{
			get
			{
				return this.GetDuration(RequestState.Queued);
			}
		}

		// Token: 0x17002711 RID: 10001
		// (get) Token: 0x06007DA0 RID: 32160 RVA: 0x00200CA2 File Offset: 0x001FEEA2
		public EnhancedTimeSpan? TotalInProgressDuration
		{
			get
			{
				return this.GetDuration(RequestState.InProgress);
			}
		}

		// Token: 0x17002712 RID: 10002
		// (get) Token: 0x06007DA1 RID: 32161 RVA: 0x00200CAB File Offset: 0x001FEEAB
		public EnhancedTimeSpan? TotalStalledDueToHADuration
		{
			get
			{
				return this.GetDuration(RequestState.StalledDueToHA);
			}
		}

		// Token: 0x17002713 RID: 10003
		// (get) Token: 0x06007DA2 RID: 32162 RVA: 0x00200CB5 File Offset: 0x001FEEB5
		public EnhancedTimeSpan? TotalTransientFailureDuration
		{
			get
			{
				return this.GetDuration(RequestState.TransientFailure);
			}
		}

		// Token: 0x17002714 RID: 10004
		// (get) Token: 0x06007DA3 RID: 32163 RVA: 0x00200CBF File Offset: 0x001FEEBF
		// (set) Token: 0x06007DA4 RID: 32164 RVA: 0x00200CC7 File Offset: 0x001FEEC7
		public string MRSServerName { get; private set; }

		// Token: 0x17002715 RID: 10005
		// (get) Token: 0x06007DA5 RID: 32165 RVA: 0x00200CD0 File Offset: 0x001FEED0
		// (set) Token: 0x06007DA6 RID: 32166 RVA: 0x00200CD8 File Offset: 0x001FEED8
		public ByteQuantifiedSize TotalMailboxSize { get; private set; }

		// Token: 0x17002716 RID: 10006
		// (get) Token: 0x06007DA7 RID: 32167 RVA: 0x00200CE1 File Offset: 0x001FEEE1
		// (set) Token: 0x06007DA8 RID: 32168 RVA: 0x00200CE9 File Offset: 0x001FEEE9
		public ulong TotalMailboxItemCount { get; private set; }

		// Token: 0x17002717 RID: 10007
		// (get) Token: 0x06007DA9 RID: 32169 RVA: 0x00200CF2 File Offset: 0x001FEEF2
		// (set) Token: 0x06007DAA RID: 32170 RVA: 0x00200CFA File Offset: 0x001FEEFA
		public ByteQuantifiedSize? TotalArchiveSize { get; private set; }

		// Token: 0x17002718 RID: 10008
		// (get) Token: 0x06007DAB RID: 32171 RVA: 0x00200D03 File Offset: 0x001FEF03
		// (set) Token: 0x06007DAC RID: 32172 RVA: 0x00200D0B File Offset: 0x001FEF0B
		public ulong? TotalArchiveItemCount { get; private set; }

		// Token: 0x17002719 RID: 10009
		// (get) Token: 0x06007DAD RID: 32173 RVA: 0x00200D14 File Offset: 0x001FEF14
		// (set) Token: 0x06007DAE RID: 32174 RVA: 0x00200D1C File Offset: 0x001FEF1C
		public string TargetDeliveryDomain { get; private set; }

		// Token: 0x1700271A RID: 10010
		// (get) Token: 0x06007DAF RID: 32175 RVA: 0x00200D25 File Offset: 0x001FEF25
		// (set) Token: 0x06007DB0 RID: 32176 RVA: 0x00200D2D File Offset: 0x001FEF2D
		public string ArchiveDomain { get; private set; }

		// Token: 0x1700271B RID: 10011
		// (get) Token: 0x06007DB1 RID: 32177 RVA: 0x00200D36 File Offset: 0x001FEF36
		// (set) Token: 0x06007DB2 RID: 32178 RVA: 0x00200D3E File Offset: 0x001FEF3E
		public int? FailureCode { get; private set; }

		// Token: 0x1700271C RID: 10012
		// (get) Token: 0x06007DB3 RID: 32179 RVA: 0x00200D47 File Offset: 0x001FEF47
		// (set) Token: 0x06007DB4 RID: 32180 RVA: 0x00200D4F File Offset: 0x001FEF4F
		public string FailureType { get; private set; }

		// Token: 0x1700271D RID: 10013
		// (get) Token: 0x06007DB5 RID: 32181 RVA: 0x00200D58 File Offset: 0x001FEF58
		// (set) Token: 0x06007DB6 RID: 32182 RVA: 0x00200D60 File Offset: 0x001FEF60
		public LocalizedString Message { get; private set; }

		// Token: 0x1700271E RID: 10014
		// (get) Token: 0x06007DB7 RID: 32183 RVA: 0x00200D69 File Offset: 0x001FEF69
		public DateTime? FailureTimestamp
		{
			get
			{
				return this.GetTimestamp(RequestJobTimestamp.Failure);
			}
		}

		// Token: 0x1700271F RID: 10015
		// (get) Token: 0x06007DB8 RID: 32184 RVA: 0x00200D73 File Offset: 0x001FEF73
		// (set) Token: 0x06007DB9 RID: 32185 RVA: 0x00200D7B File Offset: 0x001FEF7B
		public Report Report { get; private set; }

		// Token: 0x06007DBA RID: 32186 RVA: 0x00200D84 File Offset: 0x001FEF84
		public override string ToString()
		{
			string dbName;
			if (this.TargetDatabase != null)
			{
				dbName = this.TargetDatabase.ToString();
			}
			else
			{
				dbName = this.RemoteDatabaseName;
			}
			if (this.Status == RequestStatus.Completed)
			{
				return Strings.CompletedMoveHistoryEntry(this.CompletionTimestamp.ToString(), dbName, this.TotalMailboxSize.ToString(), this.OverallDuration.ToString());
			}
			if (this.Status == RequestStatus.Failed)
			{
				return Strings.FailedMoveHistoryEntry(this.FailureTimestamp.ToString(), dbName, this.Message);
			}
			if (this.Status == RequestStatus.CompletedWithWarning)
			{
				return Strings.CompletedWithWarningMoveHistoryEntry(this.CompletionTimestamp.ToString(), dbName, this.TotalMailboxSize.ToString(), this.OverallDuration.ToString(), this.Message);
			}
			return Strings.CanceledMoveHistoryEntry(this.CompletionTimestamp.ToString(), dbName);
		}

		// Token: 0x06007DBB RID: 32187 RVA: 0x00200EB8 File Offset: 0x001FF0B8
		internal static List<MoveHistoryEntry> LoadMoveHistory(Guid mailboxGuid, Guid mdbGuid, bool includeMoveReport, UserMailboxFlags flags)
		{
			List<MoveHistoryEntryInternal> list = MoveHistoryEntryInternal.LoadMoveHistory(mailboxGuid, mdbGuid, flags);
			if (list == null || list.Count == 0)
			{
				return null;
			}
			List<MoveHistoryEntry> list2 = new List<MoveHistoryEntry>(list.Count);
			foreach (MoveHistoryEntryInternal mhEntry in list)
			{
				list2.Add(new MoveHistoryEntry(mhEntry, includeMoveReport));
			}
			return list2;
		}

		// Token: 0x06007DBC RID: 32188 RVA: 0x00200F30 File Offset: 0x001FF130
		private DateTime? GetTimestamp(RequestJobTimestamp type)
		{
			if (this.timeTracker == null)
			{
				return null;
			}
			return this.timeTracker.GetDisplayTimestamp(type);
		}

		// Token: 0x06007DBD RID: 32189 RVA: 0x00200F5C File Offset: 0x001FF15C
		private EnhancedTimeSpan? GetDuration(RequestState type)
		{
			if (this.timeTracker == null)
			{
				return null;
			}
			return new EnhancedTimeSpan?(this.timeTracker.GetDisplayDuration(type).Duration);
		}

		// Token: 0x04003DD4 RID: 15828
		private RequestJobTimeTracker timeTracker;

		// Token: 0x04003DD5 RID: 15829
		private string remoteCredentialUsername;
	}
}
