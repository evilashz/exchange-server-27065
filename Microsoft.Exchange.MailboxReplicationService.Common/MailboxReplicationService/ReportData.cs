using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Exchange.Data.Common;
using Microsoft.Mapi;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x0200018F RID: 399
	internal sealed class ReportData
	{
		// Token: 0x06000EE6 RID: 3814 RVA: 0x00021EF5 File Offset: 0x000200F5
		internal ReportData(Guid guid, ReportVersion version) : this(guid, version, "MailboxReplicationService Move Reports", "IPM.MS-Exchange.MailboxMoveReports")
		{
		}

		// Token: 0x06000EE7 RID: 3815 RVA: 0x00021F0C File Offset: 0x0002010C
		internal ReportData(Guid guid, ReportVersion version, string reportFolder, string reportMessage)
		{
			this.MessageId = null;
			this.IdentifyingGuid = guid;
			this.Version = version;
			this.existingEntries = new List<ReportEntry>();
			this.newEntries = new List<ReportEntry>();
			switch (this.Version)
			{
			case ReportVersion.ReportE14R4:
				this.reportHelper = new DownlevelReportHelper(reportFolder, reportMessage);
				return;
			case ReportVersion.ReportE14R6Compression:
				this.reportHelper = new CompressedReportHelper(reportFolder, reportMessage);
				return;
			default:
				return;
			}
		}

		// Token: 0x170004A5 RID: 1189
		// (get) Token: 0x06000EE8 RID: 3816 RVA: 0x00021F88 File Offset: 0x00020188
		// (set) Token: 0x06000EE9 RID: 3817 RVA: 0x00021FD8 File Offset: 0x000201D8
		public List<ReportEntry> Entries
		{
			get
			{
				List<ReportEntry> result;
				lock (this.locker)
				{
					result = ReportHelper<CompressedReport>.MergeEntries(this.existingEntries, this.newEntries);
				}
				return result;
			}
			set
			{
				lock (this.locker)
				{
					this.existingEntries = (value ?? new List<ReportEntry>());
					this.newEntries = new List<ReportEntry>();
				}
			}
		}

		// Token: 0x170004A6 RID: 1190
		// (get) Token: 0x06000EEA RID: 3818 RVA: 0x00022030 File Offset: 0x00020230
		public bool HasNewEntries
		{
			get
			{
				bool result;
				lock (this.locker)
				{
					result = (this.newEntries != null && this.newEntries.Count > 0);
				}
				return result;
			}
		}

		// Token: 0x170004A7 RID: 1191
		// (get) Token: 0x06000EEB RID: 3819 RVA: 0x00022088 File Offset: 0x00020288
		public List<ReportEntry> NewEntries
		{
			get
			{
				List<ReportEntry> result = null;
				lock (this.locker)
				{
					result = new List<ReportEntry>(this.newEntries);
				}
				return result;
			}
		}

		// Token: 0x170004A8 RID: 1192
		// (get) Token: 0x06000EEC RID: 3820 RVA: 0x000220D4 File Offset: 0x000202D4
		// (set) Token: 0x06000EED RID: 3821 RVA: 0x000220DC File Offset: 0x000202DC
		public byte[] MessageId { get; set; }

		// Token: 0x170004A9 RID: 1193
		// (get) Token: 0x06000EEE RID: 3822 RVA: 0x000220E5 File Offset: 0x000202E5
		// (set) Token: 0x06000EEF RID: 3823 RVA: 0x000220ED File Offset: 0x000202ED
		public Guid IdentifyingGuid { get; private set; }

		// Token: 0x170004AA RID: 1194
		// (get) Token: 0x06000EF0 RID: 3824 RVA: 0x000220F6 File Offset: 0x000202F6
		// (set) Token: 0x06000EF1 RID: 3825 RVA: 0x000220FE File Offset: 0x000202FE
		public ReportVersion Version { get; private set; }

		// Token: 0x06000EF2 RID: 3826 RVA: 0x00022108 File Offset: 0x00020308
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			foreach (ReportEntry reportEntry in this.Entries)
			{
				stringBuilder.AppendLine(reportEntry.ToString());
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06000EF3 RID: 3827 RVA: 0x00022170 File Offset: 0x00020370
		public void Clear()
		{
			lock (this.locker)
			{
				this.existingEntries.Clear();
				this.newEntries.Clear();
			}
		}

		// Token: 0x06000EF4 RID: 3828 RVA: 0x000221C0 File Offset: 0x000203C0
		public void Append(LocalizedString msg)
		{
			ReportEntry entry = new ReportEntry(msg);
			this.Append(entry);
		}

		// Token: 0x06000EF5 RID: 3829 RVA: 0x000221DC File Offset: 0x000203DC
		public void Append(LocalizedString msg, Exception failure, ReportEntryFlags flags)
		{
			this.Append(new ReportEntry(msg, ReportEntryType.Error)
			{
				Failure = FailureRec.Create(failure),
				Flags = (flags | ReportEntryFlags.Failure)
			});
		}

		// Token: 0x06000EF6 RID: 3830 RVA: 0x00022210 File Offset: 0x00020410
		public void Append(LocalizedString msg, FailureRec failureRec, ReportEntryFlags flags)
		{
			this.Append(new ReportEntry(msg, ReportEntryType.Error)
			{
				Failure = failureRec,
				Flags = (flags | ReportEntryFlags.Failure)
			});
		}

		// Token: 0x06000EF7 RID: 3831 RVA: 0x0002223C File Offset: 0x0002043C
		public void Append(LocalizedString msg, BadMessageRec badItem)
		{
			this.Append(new ReportEntry(msg)
			{
				BadItem = badItem,
				Failure = badItem.Failure,
				Flags = (ReportEntryFlags.BadItem | ReportEntryFlags.Failure)
			});
		}

		// Token: 0x06000EF8 RID: 3832 RVA: 0x00022274 File Offset: 0x00020474
		public void Append(LocalizedString msg, ConfigurableObjectXML configObject, ReportEntryFlags flags)
		{
			this.Append(new ReportEntry(msg)
			{
				ConfigObject = configObject,
				Flags = (flags | ReportEntryFlags.ConfigObject)
			});
		}

		// Token: 0x06000EF9 RID: 3833 RVA: 0x000222A0 File Offset: 0x000204A0
		public void Append(LocalizedString msg, MailboxSizeRec mailboxSizeRec, ReportEntryFlags flags)
		{
			this.Append(new ReportEntry(msg)
			{
				MailboxSize = mailboxSizeRec,
				Flags = (flags | ReportEntryFlags.MailboxSize)
			});
		}

		// Token: 0x06000EFA RID: 3834 RVA: 0x000222CC File Offset: 0x000204CC
		public void Append(LocalizedString msg, List<FolderSizeRec> mailboxVerificationResults)
		{
			ReportEntry reportEntry = new ReportEntry(msg);
			reportEntry.MailboxVerificationResults = mailboxVerificationResults;
			reportEntry.Flags |= ReportEntryFlags.MailboxVerificationResults;
			this.Append(reportEntry);
		}

		// Token: 0x06000EFB RID: 3835 RVA: 0x00022300 File Offset: 0x00020500
		public void Append(LocalizedString msg, SessionStatistics sessionStatistics, SessionStatistics archiveSessionStatistics)
		{
			if (sessionStatistics == null && archiveSessionStatistics == null)
			{
				this.Append(new ReportEntry(msg));
				return;
			}
			ReportEntry reportEntry = new ReportEntry(msg);
			reportEntry.Flags |= ReportEntryFlags.SessionStatistics;
			if (sessionStatistics != null)
			{
				reportEntry.SessionStatistics = sessionStatistics;
				reportEntry.Flags |= ReportEntryFlags.Primary;
			}
			if (archiveSessionStatistics != null)
			{
				reportEntry.ArchiveSessionStatistics = archiveSessionStatistics;
				reportEntry.Flags |= ReportEntryFlags.Archive;
			}
			this.Append(reportEntry);
		}

		// Token: 0x06000EFC RID: 3836 RVA: 0x00022378 File Offset: 0x00020578
		public void Append(RequestJobTimeTracker timeTracker, ReportEntryFlags flags)
		{
			ThrottleDurations sourceThrottleDurations;
			ThrottleDurations targetThrottleDurations;
			timeTracker.GetThrottledDurations(out sourceThrottleDurations, out targetThrottleDurations);
			ReportEntry reportEntry = new ReportEntry();
			reportEntry.Flags = flags;
			if ((flags & ReportEntryFlags.SourceThrottleDurations) != ReportEntryFlags.None)
			{
				reportEntry.SourceThrottleDurations = sourceThrottleDurations;
			}
			if ((flags & ReportEntryFlags.TargetThrottleDurations) != ReportEntryFlags.None)
			{
				reportEntry.TargetThrottleDurations = targetThrottleDurations;
			}
			this.Append(reportEntry);
		}

		// Token: 0x06000EFD RID: 3837 RVA: 0x000223C4 File Offset: 0x000205C4
		public void Append(LocalizedString msg, ConnectivityRec connectivityRec)
		{
			this.Append(new ReportEntry(msg)
			{
				Connectivity = connectivityRec
			});
		}

		// Token: 0x06000EFE RID: 3838 RVA: 0x000223E8 File Offset: 0x000205E8
		public void AppendDebug(string debugData)
		{
			this.Append(new ReportEntry(LocalizedString.Empty, ReportEntryType.Debug)
			{
				DebugData = debugData
			});
		}

		// Token: 0x06000EFF RID: 3839 RVA: 0x00022410 File Offset: 0x00020610
		public void Append(ReportEntry entry)
		{
			if (entry != null)
			{
				lock (this.locker)
				{
					this.newEntries.Add(entry);
				}
				MrsTracer.Common.Debug("{0}", new object[]
				{
					entry
				});
			}
		}

		// Token: 0x06000F00 RID: 3840 RVA: 0x00022474 File Offset: 0x00020674
		public void Append(ICollection<ReportEntry> reportEntries)
		{
			if (reportEntries != null)
			{
				foreach (ReportEntry entry in reportEntries)
				{
					this.Append(entry);
				}
			}
		}

		// Token: 0x06000F01 RID: 3841 RVA: 0x000224C0 File Offset: 0x000206C0
		public void Load(MapiStore storeToUse)
		{
			this.reportHelper.Load(this, storeToUse);
		}

		// Token: 0x06000F02 RID: 3842 RVA: 0x000224CF File Offset: 0x000206CF
		public void Flush(MapiStore storeToUse)
		{
			this.reportHelper.Flush(this, storeToUse);
		}

		// Token: 0x06000F03 RID: 3843 RVA: 0x000224DE File Offset: 0x000206DE
		public void Delete(MapiStore storeToUse)
		{
			this.reportHelper.Delete(this, storeToUse);
		}

		// Token: 0x06000F04 RID: 3844 RVA: 0x000224ED File Offset: 0x000206ED
		public Report ToReport()
		{
			return new Report(this);
		}

		// Token: 0x0400085B RID: 2139
		private const string ReportFolderName = "MailboxReplicationService Move Reports";

		// Token: 0x0400085C RID: 2140
		private const string ReportMessageClass = "IPM.MS-Exchange.MailboxMoveReports";

		// Token: 0x0400085D RID: 2141
		private List<ReportEntry> existingEntries;

		// Token: 0x0400085E RID: 2142
		private List<ReportEntry> newEntries;

		// Token: 0x0400085F RID: 2143
		private IReportHelper reportHelper;

		// Token: 0x04000860 RID: 2144
		private object locker = new object();
	}
}
