using System;
using System.Collections.Generic;
using System.Text;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x0200018B RID: 395
	[Serializable]
	public sealed class Report
	{
		// Token: 0x06000EBB RID: 3771 RVA: 0x00021858 File Offset: 0x0001FA58
		internal Report(ReportData reportData)
		{
			SessionStatistics stats = new SessionStatistics();
			SessionStatistics stats2 = new SessionStatistics();
			this.SessionStatistics = new SessionStatistics();
			this.ArchiveSessionStatistics = new SessionStatistics();
			foreach (ReportEntry reportEntry in reportData.Entries)
			{
				if ((reportEntry.Flags & (ReportEntryFlags.TargetThrottleDurations | ReportEntryFlags.SourceThrottleDurations)) != ReportEntryFlags.None)
				{
					if ((reportEntry.Flags & ReportEntryFlags.SourceThrottleDurations) != ReportEntryFlags.None)
					{
						this.SourceThrottles = new Throttles(reportEntry.SourceThrottleDurations);
					}
					if ((reportEntry.Flags & ReportEntryFlags.TargetThrottleDurations) != ReportEntryFlags.None)
					{
						this.TargetThrottles = new Throttles(reportEntry.TargetThrottleDurations);
					}
				}
				else
				{
					if (reportEntry.Type == ReportEntryType.Debug)
					{
						this.AddToLazyList<ReportEntry>(ref this.debugEntries, reportEntry);
					}
					else
					{
						this.AddToLazyList<ReportEntry>(ref this.entries, reportEntry);
					}
					if (reportEntry.Type == ReportEntryType.Warning || reportEntry.Type == ReportEntryType.WarningCondition)
					{
						this.AddToLazyList<ReportEntry>(ref this.warnings, reportEntry);
					}
					if (reportEntry.Failure != null)
					{
						this.AddToLazyList<FailureRec>(ref this.failures, reportEntry.Failure);
					}
					if (reportEntry.BadItem != null)
					{
						BadItemKind kind = reportEntry.BadItem.Kind;
						if (kind == BadItemKind.LargeItem)
						{
							this.AddToLazyList<BadMessageRec>(ref this.largeItems, reportEntry.BadItem);
						}
						else
						{
							this.AddToLazyList<BadMessageRec>(ref this.badItems, reportEntry.BadItem);
						}
					}
					if ((reportEntry.Flags & ReportEntryFlags.MailboxVerificationResults) != ReportEntryFlags.None && reportEntry.MailboxVerificationResults != null)
					{
						this.MailboxVerification = new Report.ListWithToString<FolderSizeRec>();
						this.MailboxVerification.AddRange(reportEntry.MailboxVerificationResults);
					}
					if ((reportEntry.Flags & ReportEntryFlags.SessionStatistics) != ReportEntryFlags.None)
					{
						if (reportEntry.SessionStatistics != null)
						{
							if (reportEntry.SessionStatistics.SessionId != this.SessionStatistics.SessionId)
							{
								this.SessionStatistics += stats;
							}
							stats = reportEntry.SessionStatistics;
						}
						if (reportEntry.ArchiveSessionStatistics != null)
						{
							if (reportEntry.ArchiveSessionStatistics.SessionId != this.ArchiveSessionStatistics.SessionId)
							{
								this.ArchiveSessionStatistics += stats2;
							}
							stats2 = reportEntry.ArchiveSessionStatistics;
						}
					}
					if ((reportEntry.Flags & ReportEntryFlags.ConfigObject) != ReportEntryFlags.None && reportEntry.ConfigObject != null)
					{
						if ((reportEntry.Flags & ReportEntryFlags.Before) != ReportEntryFlags.None)
						{
							if ((reportEntry.Flags & ReportEntryFlags.Source) != ReportEntryFlags.None)
							{
								this.SourceMailboxBeforeMove = reportEntry.ConfigObject;
							}
							if ((reportEntry.Flags & ReportEntryFlags.Target) != ReportEntryFlags.None)
							{
								this.TargetMailUserBeforeMove = reportEntry.ConfigObject;
							}
						}
						if ((reportEntry.Flags & ReportEntryFlags.After) != ReportEntryFlags.None)
						{
							if ((reportEntry.Flags & ReportEntryFlags.Source) != ReportEntryFlags.None)
							{
								this.SourceMailUserAfterMove = reportEntry.ConfigObject;
							}
							if ((reportEntry.Flags & ReportEntryFlags.Target) != ReportEntryFlags.None)
							{
								this.TargetMailboxAfterMove = reportEntry.ConfigObject;
							}
						}
					}
					if ((reportEntry.Flags & ReportEntryFlags.MailboxSize) != ReportEntryFlags.None && reportEntry.MailboxSize != null)
					{
						if ((reportEntry.Flags & ReportEntryFlags.Source) != ReportEntryFlags.None)
						{
							if ((reportEntry.Flags & ReportEntryFlags.Primary) != ReportEntryFlags.None)
							{
								this.SourceMailboxSize = reportEntry.MailboxSize;
							}
							if ((reportEntry.Flags & ReportEntryFlags.Archive) != ReportEntryFlags.None)
							{
								this.SourceArchiveMailboxSize = reportEntry.MailboxSize;
							}
						}
						if ((reportEntry.Flags & ReportEntryFlags.Target) != ReportEntryFlags.None)
						{
							if ((reportEntry.Flags & ReportEntryFlags.Primary) != ReportEntryFlags.None)
							{
								this.TargetMailboxSize = reportEntry.MailboxSize;
							}
							if ((reportEntry.Flags & ReportEntryFlags.Archive) != ReportEntryFlags.None)
							{
								this.TargetArchiveMailboxSize = reportEntry.MailboxSize;
							}
						}
					}
					if (reportEntry.Connectivity != null)
					{
						this.AddToLazyList<Report.ConnectivityRecWithTimestamp>(ref this.connectivity, new Report.ConnectivityRecWithTimestamp(reportEntry.CreationTime, reportEntry.Connectivity));
					}
				}
			}
			this.SessionStatistics += stats;
			this.ArchiveSessionStatistics += stats2;
			Comparison<DurationInfo> comparison = (DurationInfo x, DurationInfo y) => y.Duration.CompareTo(x.Duration);
			this.SessionStatistics.SourceProviderInfo.Durations.Sort(comparison);
			this.SessionStatistics.DestinationProviderInfo.Durations.Sort(comparison);
			this.ArchiveSessionStatistics.SourceProviderInfo.Durations.Sort(comparison);
			this.ArchiveSessionStatistics.DestinationProviderInfo.Durations.Sort(comparison);
		}

		// Token: 0x17000490 RID: 1168
		// (get) Token: 0x06000EBC RID: 3772 RVA: 0x00021C84 File Offset: 0x0001FE84
		public Report.ListWithToString<ReportEntry> Entries
		{
			get
			{
				return this.entries;
			}
		}

		// Token: 0x17000491 RID: 1169
		// (get) Token: 0x06000EBD RID: 3773 RVA: 0x00021C8C File Offset: 0x0001FE8C
		public Report.ListWithToString<ReportEntry> Warnings
		{
			get
			{
				return this.warnings;
			}
		}

		// Token: 0x17000492 RID: 1170
		// (get) Token: 0x06000EBE RID: 3774 RVA: 0x00021C94 File Offset: 0x0001FE94
		public Report.ListWithToString<ReportEntry> DebugEntries
		{
			get
			{
				return this.debugEntries;
			}
		}

		// Token: 0x17000493 RID: 1171
		// (get) Token: 0x06000EBF RID: 3775 RVA: 0x00021C9C File Offset: 0x0001FE9C
		public Report.ListWithToString<FailureRec> Failures
		{
			get
			{
				return this.failures;
			}
		}

		// Token: 0x17000494 RID: 1172
		// (get) Token: 0x06000EC0 RID: 3776 RVA: 0x00021CA4 File Offset: 0x0001FEA4
		public Report.ListWithToString<BadMessageRec> BadItems
		{
			get
			{
				return this.badItems;
			}
		}

		// Token: 0x17000495 RID: 1173
		// (get) Token: 0x06000EC1 RID: 3777 RVA: 0x00021CAC File Offset: 0x0001FEAC
		public Report.ListWithToString<BadMessageRec> LargeItems
		{
			get
			{
				return this.largeItems;
			}
		}

		// Token: 0x17000496 RID: 1174
		// (get) Token: 0x06000EC2 RID: 3778 RVA: 0x00021CB4 File Offset: 0x0001FEB4
		public Report.ListWithToString<Report.ConnectivityRecWithTimestamp> Connectivity
		{
			get
			{
				return this.connectivity;
			}
		}

		// Token: 0x17000497 RID: 1175
		// (get) Token: 0x06000EC3 RID: 3779 RVA: 0x00021CBC File Offset: 0x0001FEBC
		// (set) Token: 0x06000EC4 RID: 3780 RVA: 0x00021CC4 File Offset: 0x0001FEC4
		public ConfigurableObjectXML SourceMailboxBeforeMove { get; private set; }

		// Token: 0x17000498 RID: 1176
		// (get) Token: 0x06000EC5 RID: 3781 RVA: 0x00021CCD File Offset: 0x0001FECD
		// (set) Token: 0x06000EC6 RID: 3782 RVA: 0x00021CD5 File Offset: 0x0001FED5
		public ConfigurableObjectXML TargetMailboxAfterMove { get; private set; }

		// Token: 0x17000499 RID: 1177
		// (get) Token: 0x06000EC7 RID: 3783 RVA: 0x00021CDE File Offset: 0x0001FEDE
		// (set) Token: 0x06000EC8 RID: 3784 RVA: 0x00021CE6 File Offset: 0x0001FEE6
		public ConfigurableObjectXML TargetMailUserBeforeMove { get; private set; }

		// Token: 0x1700049A RID: 1178
		// (get) Token: 0x06000EC9 RID: 3785 RVA: 0x00021CEF File Offset: 0x0001FEEF
		// (set) Token: 0x06000ECA RID: 3786 RVA: 0x00021CF7 File Offset: 0x0001FEF7
		public ConfigurableObjectXML SourceMailUserAfterMove { get; private set; }

		// Token: 0x1700049B RID: 1179
		// (get) Token: 0x06000ECB RID: 3787 RVA: 0x00021D00 File Offset: 0x0001FF00
		// (set) Token: 0x06000ECC RID: 3788 RVA: 0x00021D08 File Offset: 0x0001FF08
		public MailboxSizeRec SourceMailboxSize { get; private set; }

		// Token: 0x1700049C RID: 1180
		// (get) Token: 0x06000ECD RID: 3789 RVA: 0x00021D11 File Offset: 0x0001FF11
		// (set) Token: 0x06000ECE RID: 3790 RVA: 0x00021D19 File Offset: 0x0001FF19
		public MailboxSizeRec TargetMailboxSize { get; private set; }

		// Token: 0x1700049D RID: 1181
		// (get) Token: 0x06000ECF RID: 3791 RVA: 0x00021D22 File Offset: 0x0001FF22
		// (set) Token: 0x06000ED0 RID: 3792 RVA: 0x00021D2A File Offset: 0x0001FF2A
		public MailboxSizeRec SourceArchiveMailboxSize { get; private set; }

		// Token: 0x1700049E RID: 1182
		// (get) Token: 0x06000ED1 RID: 3793 RVA: 0x00021D33 File Offset: 0x0001FF33
		// (set) Token: 0x06000ED2 RID: 3794 RVA: 0x00021D3B File Offset: 0x0001FF3B
		public MailboxSizeRec TargetArchiveMailboxSize { get; private set; }

		// Token: 0x1700049F RID: 1183
		// (get) Token: 0x06000ED3 RID: 3795 RVA: 0x00021D44 File Offset: 0x0001FF44
		// (set) Token: 0x06000ED4 RID: 3796 RVA: 0x00021D4C File Offset: 0x0001FF4C
		public Report.ListWithToString<FolderSizeRec> MailboxVerification { get; private set; }

		// Token: 0x170004A0 RID: 1184
		// (get) Token: 0x06000ED5 RID: 3797 RVA: 0x00021D55 File Offset: 0x0001FF55
		// (set) Token: 0x06000ED6 RID: 3798 RVA: 0x00021D5D File Offset: 0x0001FF5D
		public SessionStatistics SessionStatistics { get; private set; }

		// Token: 0x170004A1 RID: 1185
		// (get) Token: 0x06000ED7 RID: 3799 RVA: 0x00021D66 File Offset: 0x0001FF66
		// (set) Token: 0x06000ED8 RID: 3800 RVA: 0x00021D6E File Offset: 0x0001FF6E
		public SessionStatistics ArchiveSessionStatistics { get; private set; }

		// Token: 0x170004A2 RID: 1186
		// (get) Token: 0x06000ED9 RID: 3801 RVA: 0x00021D77 File Offset: 0x0001FF77
		// (set) Token: 0x06000EDA RID: 3802 RVA: 0x00021D7F File Offset: 0x0001FF7F
		public Throttles SourceThrottles { get; private set; }

		// Token: 0x170004A3 RID: 1187
		// (get) Token: 0x06000EDB RID: 3803 RVA: 0x00021D88 File Offset: 0x0001FF88
		// (set) Token: 0x06000EDC RID: 3804 RVA: 0x00021D90 File Offset: 0x0001FF90
		public Throttles TargetThrottles { get; private set; }

		// Token: 0x06000EDD RID: 3805 RVA: 0x00021D99 File Offset: 0x0001FF99
		public override string ToString()
		{
			if (this.Entries != null)
			{
				return this.Entries.ToString();
			}
			return string.Empty;
		}

		// Token: 0x06000EDE RID: 3806 RVA: 0x00021DB4 File Offset: 0x0001FFB4
		private void AddToLazyList<T>(ref Report.ListWithToString<T> list, T entry)
		{
			if (list == null)
			{
				list = new Report.ListWithToString<T>();
			}
			list.Add(entry);
		}

		// Token: 0x04000842 RID: 2114
		private Report.ListWithToString<ReportEntry> entries;

		// Token: 0x04000843 RID: 2115
		private Report.ListWithToString<ReportEntry> debugEntries;

		// Token: 0x04000844 RID: 2116
		private Report.ListWithToString<ReportEntry> warnings;

		// Token: 0x04000845 RID: 2117
		private Report.ListWithToString<FailureRec> failures;

		// Token: 0x04000846 RID: 2118
		private Report.ListWithToString<BadMessageRec> badItems;

		// Token: 0x04000847 RID: 2119
		private Report.ListWithToString<BadMessageRec> largeItems;

		// Token: 0x04000848 RID: 2120
		private Report.ListWithToString<Report.ConnectivityRecWithTimestamp> connectivity;

		// Token: 0x0200018C RID: 396
		[Serializable]
		public class ListWithToString<T> : List<T>
		{
			// Token: 0x06000EE0 RID: 3808 RVA: 0x00021DCC File Offset: 0x0001FFCC
			public override string ToString()
			{
				StringBuilder stringBuilder = new StringBuilder();
				foreach (T t in this)
				{
					string value = t.ToString();
					if (!string.IsNullOrEmpty(value))
					{
						stringBuilder.AppendLine(value);
					}
				}
				return stringBuilder.ToString();
			}
		}

		// Token: 0x0200018D RID: 397
		[Serializable]
		public class ConnectivityRecWithTimestamp : ConnectivityRec
		{
			// Token: 0x06000EE2 RID: 3810 RVA: 0x00021E48 File Offset: 0x00020048
			public ConnectivityRecWithTimestamp(DateTime timestamp, ConnectivityRec entry)
			{
				this.Timestamp = timestamp;
				base.ServerKind = entry.ServerKind;
				base.ServerName = entry.ServerName;
				base.ServerVersion = entry.ServerVersion;
				base.ProxyName = entry.ProxyName;
				base.ProxyVersion = entry.ProxyVersion;
				base.ProviderName = entry.ProviderName;
			}

			// Token: 0x170004A4 RID: 1188
			// (get) Token: 0x06000EE3 RID: 3811 RVA: 0x00021EAA File Offset: 0x000200AA
			// (set) Token: 0x06000EE4 RID: 3812 RVA: 0x00021EB2 File Offset: 0x000200B2
			public DateTime Timestamp { get; private set; }

			// Token: 0x06000EE5 RID: 3813 RVA: 0x00021EBC File Offset: 0x000200BC
			public override string ToString()
			{
				return string.Format("{0} {1}", this.Timestamp.ToLocalTime().ToString(), base.ToString());
			}
		}
	}
}
