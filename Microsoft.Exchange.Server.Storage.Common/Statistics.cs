using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using System.Text;
using System.Threading;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.ManagedStore.Common;
using Microsoft.Exchange.Server.Storage.Common.ExtensionMethods;

namespace Microsoft.Exchange.Server.Storage.Common
{
	// Token: 0x0200008F RID: 143
	public static class Statistics
	{
		// Token: 0x170001B7 RID: 439
		// (get) Token: 0x0600077E RID: 1918 RVA: 0x00014D53 File Offset: 0x00012F53
		// (set) Token: 0x0600077F RID: 1919 RVA: 0x00014D5A File Offset: 0x00012F5A
		internal static RecurringTask<object> DumpStatisticsTask
		{
			get
			{
				return Statistics.dumpStatisticsTask;
			}
			set
			{
				Statistics.dumpStatisticsTask = value;
			}
		}

		// Token: 0x170001B8 RID: 440
		// (get) Token: 0x06000780 RID: 1920 RVA: 0x00014D62 File Offset: 0x00012F62
		// (set) Token: 0x06000781 RID: 1921 RVA: 0x00014D69 File Offset: 0x00012F69
		internal static List<Statistics.StatisticsGroup> Groups
		{
			get
			{
				return Statistics.groups;
			}
			set
			{
				Statistics.groups = value;
			}
		}

		// Token: 0x06000782 RID: 1922 RVA: 0x00014D74 File Offset: 0x00012F74
		[Conditional("STATISTICS")]
		public static void Initialize()
		{
			if (Statistics.dumpStatisticsTask == null)
			{
				Type typeFromHandle = typeof(Statistics);
				foreach (TypeInfo typeInfo in typeFromHandle.GetTypeInfo().DeclaredNestedTypes)
				{
					if (typeInfo.IsClass)
					{
						List<Statistics.StatisticsEntry> list = null;
						foreach (FieldInfo fieldInfo in typeInfo.DeclaredFields)
						{
							if (fieldInfo.IsStatic && fieldInfo.FieldType.GetTypeInfo().IsSubclassOf(typeof(Statistics.StatisticElement)))
							{
								Statistics.CounterNameAttribute customAttribute = fieldInfo.GetCustomAttribute(false);
								string name;
								if (customAttribute != null)
								{
									name = customAttribute.Name;
								}
								else
								{
									name = fieldInfo.Name;
								}
								object value = fieldInfo.GetValue(null);
								if (list == null)
								{
									list = new List<Statistics.StatisticsEntry>();
								}
								((Statistics.StatisticElement)value).Initialize();
								list.Add(new Statistics.StatisticsEntry(name, (Statistics.StatisticElement)value));
							}
						}
						if (list != null)
						{
							Statistics.GroupNameAttribute customAttribute2 = typeInfo.GetCustomAttribute(false);
							string name2;
							if (customAttribute2 != null)
							{
								name2 = customAttribute2.Name;
							}
							else
							{
								name2 = typeInfo.Name;
							}
							if (Statistics.groups == null)
							{
								Statistics.groups = new List<Statistics.StatisticsGroup>();
							}
							Statistics.groups.Add(new Statistics.StatisticsGroup(name2, list));
						}
					}
				}
				foreach (Statistics.StatisticsGroup statisticsGroup in Statistics.groups)
				{
					foreach (Statistics.StatisticsEntry statisticsEntry in statisticsGroup.Entries)
					{
						statisticsEntry.Element.Reset();
					}
				}
				Statistics.DumpStatisticsTask = new RecurringTask<object>(new Task<object>.TaskCallback(Statistics.DumpStatisticsTaskCallback), null, Statistics.tickInterval, true);
				Statistics.lastTimeDumped = Environment.TickCount;
			}
		}

		// Token: 0x06000783 RID: 1923 RVA: 0x00014FCC File Offset: 0x000131CC
		[Conditional("STATISTICS")]
		public static void Terminate()
		{
			if (Statistics.dumpStatisticsTask != null)
			{
				Statistics.dumpStatisticsTask.Dispose();
				Statistics.dumpStatisticsTask = null;
			}
			if (Statistics.groups != null)
			{
				Statistics.groups.Clear();
			}
		}

		// Token: 0x06000784 RID: 1924 RVA: 0x00014FF8 File Offset: 0x000131F8
		private static void DumpStatisticsTaskCallback(TaskExecutionDiagnosticsProxy diagnosticsContext, object context, Func<bool> shouldCallbackContinue)
		{
			if (shouldCallbackContinue())
			{
				if (ExTraceGlobals.StatisticsTracer.IsTraceEnabled(TraceType.PerformanceTrace))
				{
					int num = Statistics.lastTimeDumped;
					int tickCount = Environment.TickCount;
					if (tickCount - num > Statistics.dumpStatisticsInterval && num == Interlocked.CompareExchange(ref Statistics.lastTimeDumped, tickCount, num) && Statistics.groups != null)
					{
						foreach (Statistics.StatisticsGroup statisticsGroup in Statistics.groups)
						{
							StringBuilder stringBuilder = new StringBuilder(statisticsGroup.Entries.Count * 20);
							stringBuilder.Append(statisticsGroup.GroupName);
							stringBuilder.Append(" statistics:[");
							foreach (Statistics.StatisticsEntry statisticsEntry in statisticsGroup.Entries)
							{
								stringBuilder.Append(" ");
								stringBuilder.Append(statisticsEntry.EntryName);
								stringBuilder.Append(":[");
								stringBuilder.Append(statisticsEntry.Element.ToString());
								stringBuilder.Append("]");
							}
							stringBuilder.Append("]");
							ExTraceGlobals.StatisticsTracer.TracePerformance(0L, stringBuilder.ToString());
						}
					}
				}
				if (ExTraceGlobals.ResetStatisticsTracer.IsTraceEnabled(TraceType.PerformanceTrace))
				{
					if (!Statistics.lastTimeResetTag && Statistics.groups != null)
					{
						foreach (Statistics.StatisticsGroup statisticsGroup2 in Statistics.groups)
						{
							foreach (Statistics.StatisticsEntry statisticsEntry2 in statisticsGroup2.Entries)
							{
								statisticsEntry2.Element.Reset();
							}
						}
					}
					Statistics.lastTimeResetTag = true;
					return;
				}
				Statistics.lastTimeResetTag = false;
			}
		}

		// Token: 0x04000696 RID: 1686
		private static int dumpStatisticsInterval = 300000;

		// Token: 0x04000697 RID: 1687
		private static TimeSpan tickInterval = TimeSpan.FromMinutes(1.0);

		// Token: 0x04000698 RID: 1688
		private static RecurringTask<object> dumpStatisticsTask;

		// Token: 0x04000699 RID: 1689
		private static int lastTimeDumped;

		// Token: 0x0400069A RID: 1690
		private static bool lastTimeResetTag = false;

		// Token: 0x0400069B RID: 1691
		private static List<Statistics.StatisticsGroup> groups = null;

		// Token: 0x02000090 RID: 144
		[Statistics.GroupNameAttribute("Logon Notifications")]
		public static class LogonNotifications
		{
			// Token: 0x0400069C RID: 1692
			public static Statistics.Counter32 Total = new Statistics.Counter32();

			// Token: 0x0400069D RID: 1693
			public static Statistics.Counter32 Redundant = new Statistics.Counter32();

			// Token: 0x0400069E RID: 1694
			public static Statistics.Counter32 DropOld = new Statistics.Counter32();

			// Token: 0x0400069F RID: 1695
			public static Statistics.Counter32 DropNew = new Statistics.Counter32();

			// Token: 0x040006A0 RID: 1696
			public static Statistics.Counter32 Merge = new Statistics.Counter32();

			// Token: 0x040006A1 RID: 1697
			public static Statistics.Counter32 ReplaceOld = new Statistics.Counter32();

			// Token: 0x040006A2 RID: 1698
			[Statistics.CounterNameAttribute("OverflowTC")]
			public static Statistics.Counter32 OverflowFlushWithTableChanged = new Statistics.Counter32();

			// Token: 0x040006A3 RID: 1699
			[Statistics.CounterNameAttribute("OverflowDrop")]
			public static Statistics.Counter32 OverflowDrop = new Statistics.Counter32();
		}

		// Token: 0x02000091 RID: 145
		[Statistics.GroupNameAttribute("Context Notifications")]
		public static class ContextNotifications
		{
			// Token: 0x040006A4 RID: 1700
			public static Statistics.Counter32 Total = new Statistics.Counter32();

			// Token: 0x040006A5 RID: 1701
			public static Statistics.Counter32Max Max = new Statistics.Counter32Max();
		}

		// Token: 0x02000092 RID: 146
		[Statistics.GroupNameAttribute("Miscelaneous Notifications")]
		public static class MiscelaneousNotifications
		{
			// Token: 0x040006A6 RID: 1702
			[Statistics.CounterNameAttribute("SkipFld")]
			public static Statistics.Counter32 SkippedFolderTableNotifications = new Statistics.Counter32();

			// Token: 0x040006A7 RID: 1703
			[Statistics.CounterNameAttribute("SkipMsg")]
			public static Statistics.Counter32 SkippedMessageTableNotifications = new Statistics.Counter32();

			// Token: 0x040006A8 RID: 1704
			[Statistics.CounterNameAttribute("NewTC")]
			public static Statistics.Counter32 NewTableChangedWashesAnyOld = new Statistics.Counter32();

			// Token: 0x040006A9 RID: 1705
			[Statistics.CounterNameAttribute("OldTC")]
			public static Statistics.Counter32 OldTableChangedWashesAnyNew = new Statistics.Counter32();

			// Token: 0x040006AA RID: 1706
			[Statistics.CounterNameAttribute("NewRM+OldRA")]
			public static Statistics.Counter32 NewRowModifiedWashesOldRowAdded = new Statistics.Counter32();

			// Token: 0x040006AB RID: 1707
			[Statistics.CounterNameAttribute("NewRM+OldRM")]
			public static Statistics.Counter32 NewRowModifiedWashesOldRowModified = new Statistics.Counter32();

			// Token: 0x040006AC RID: 1708
			[Statistics.CounterNameAttribute("NewRD+OldRA")]
			public static Statistics.Counter32 NewRowDeletedWashesOldRowAdded = new Statistics.Counter32();

			// Token: 0x040006AD RID: 1709
			[Statistics.CounterNameAttribute("NewRD+OldRM")]
			public static Statistics.Counter32 NewRowDeletedWashesOldRowModified = new Statistics.Counter32();

			// Token: 0x040006AE RID: 1710
			[Statistics.CounterNameAttribute("RestartTRN")]
			public static Statistics.Counter32 NotificationHandlingRestartedTransaction = new Statistics.Counter32();
		}

		// Token: 0x02000093 RID: 147
		[Statistics.GroupNameAttribute("Notification Types")]
		public static class NotificationTypes
		{
			// Token: 0x040006AF RID: 1711
			[Statistics.CounterNameAttribute("MsgCreated")]
			public static Statistics.Counter32 MessageCreated = new Statistics.Counter32();

			// Token: 0x040006B0 RID: 1712
			[Statistics.CounterNameAttribute("MsgModified")]
			public static Statistics.Counter32 MessageModified = new Statistics.Counter32();

			// Token: 0x040006B1 RID: 1713
			[Statistics.CounterNameAttribute("MsgDeleted")]
			public static Statistics.Counter32 MessageDeleted = new Statistics.Counter32();

			// Token: 0x040006B2 RID: 1714
			[Statistics.CounterNameAttribute("MsgMoved")]
			public static Statistics.Counter32 MessageMoved = new Statistics.Counter32();

			// Token: 0x040006B3 RID: 1715
			[Statistics.CounterNameAttribute("MsgCopied")]
			public static Statistics.Counter32 MessageCopied = new Statistics.Counter32();

			// Token: 0x040006B4 RID: 1716
			[Statistics.CounterNameAttribute("MsgUnlinked")]
			public static Statistics.Counter32 MessageUnlinked = new Statistics.Counter32();

			// Token: 0x040006B5 RID: 1717
			[Statistics.CounterNameAttribute("MsgSubmitted")]
			public static Statistics.Counter32 MailSubmitted = new Statistics.Counter32();

			// Token: 0x040006B6 RID: 1718
			[Statistics.CounterNameAttribute("NewMail")]
			public static Statistics.Counter32 NewMail = new Statistics.Counter32();

			// Token: 0x040006B7 RID: 1719
			[Statistics.CounterNameAttribute("FldCreated")]
			public static Statistics.Counter32 FolderCreated = new Statistics.Counter32();

			// Token: 0x040006B8 RID: 1720
			[Statistics.CounterNameAttribute("FldModified")]
			public static Statistics.Counter32 FolderModified = new Statistics.Counter32();

			// Token: 0x040006B9 RID: 1721
			[Statistics.CounterNameAttribute("FldDeleted")]
			public static Statistics.Counter32 FolderDeleted = new Statistics.Counter32();

			// Token: 0x040006BA RID: 1722
			[Statistics.CounterNameAttribute("FldMoved")]
			public static Statistics.Counter32 FolderMoved = new Statistics.Counter32();

			// Token: 0x040006BB RID: 1723
			[Statistics.CounterNameAttribute("FldCopied")]
			public static Statistics.Counter32 FolderCopied = new Statistics.Counter32();

			// Token: 0x040006BC RID: 1724
			[Statistics.CounterNameAttribute("TableModified")]
			public static Statistics.Counter32 TableModified = new Statistics.Counter32();

			// Token: 0x040006BD RID: 1725
			[Statistics.CounterNameAttribute("CatRowAdded")]
			public static Statistics.Counter32 CategorizedRowAdded = new Statistics.Counter32();

			// Token: 0x040006BE RID: 1726
			[Statistics.CounterNameAttribute("CatRowModified")]
			public static Statistics.Counter32 CategorizedRowModified = new Statistics.Counter32();

			// Token: 0x040006BF RID: 1727
			[Statistics.CounterNameAttribute("CatRowDeleted")]
			public static Statistics.Counter32 CategorizedRowDeleted = new Statistics.Counter32();

			// Token: 0x040006C0 RID: 1728
			[Statistics.CounterNameAttribute("MsgsLinked")]
			public static Statistics.Counter32 MessagesLinked = new Statistics.Counter32();

			// Token: 0x040006C1 RID: 1729
			[Statistics.CounterNameAttribute("SearchComplete")]
			public static Statistics.Counter32 SearchComplete = new Statistics.Counter32();

			// Token: 0x040006C2 RID: 1730
			[Statistics.CounterNameAttribute("BeginLongOp")]
			public static Statistics.Counter32 BeginLongOperation = new Statistics.Counter32();

			// Token: 0x040006C3 RID: 1731
			[Statistics.CounterNameAttribute("EndLongOp")]
			public static Statistics.Counter32 EndLongOperation = new Statistics.Counter32();

			// Token: 0x040006C4 RID: 1732
			[Statistics.CounterNameAttribute("nonStandardObjectModification")]
			public static Statistics.Counter32 NonStandardObjectModification = new Statistics.Counter32();

			// Token: 0x040006C5 RID: 1733
			[Statistics.CounterNameAttribute("Ics")]
			public static Statistics.Counter32 Ics = new Statistics.Counter32();

			// Token: 0x040006C6 RID: 1734
			[Statistics.CounterNameAttribute("MbxCreated")]
			public static Statistics.Counter32 MailboxCreated = new Statistics.Counter32();

			// Token: 0x040006C7 RID: 1735
			[Statistics.CounterNameAttribute("MbxModified")]
			public static Statistics.Counter32 MailboxModified = new Statistics.Counter32();

			// Token: 0x040006C8 RID: 1736
			[Statistics.CounterNameAttribute("MbxDeleted")]
			public static Statistics.Counter32 MailboxDeleted = new Statistics.Counter32();

			// Token: 0x040006C9 RID: 1737
			[Statistics.CounterNameAttribute("MbxDusconnected")]
			public static Statistics.Counter32 MailboxDisconnected = new Statistics.Counter32();

			// Token: 0x040006CA RID: 1738
			[Statistics.CounterNameAttribute("MbxReconnected")]
			public static Statistics.Counter32 MailboxReconnected = new Statistics.Counter32();

			// Token: 0x040006CB RID: 1739
			[Statistics.CounterNameAttribute("MbxMoveStarted")]
			public static Statistics.Counter32 MailboxMoveStarted = new Statistics.Counter32();

			// Token: 0x040006CC RID: 1740
			[Statistics.CounterNameAttribute("MbxMoveSucceeded")]
			public static Statistics.Counter32 MailboxMoveSucceeded = new Statistics.Counter32();

			// Token: 0x040006CD RID: 1741
			[Statistics.CounterNameAttribute("MbxMoveFailed")]
			public static Statistics.Counter32 MailboxMoveFailed = new Statistics.Counter32();
		}

		// Token: 0x02000094 RID: 148
		[Statistics.GroupNameAttribute("Table Notification Sub-Types")]
		public static class TableNotificationTypes
		{
			// Token: 0x040006CE RID: 1742
			[Statistics.CounterNameAttribute("Changed")]
			public static Statistics.Counter32 Changed = new Statistics.Counter32();

			// Token: 0x040006CF RID: 1743
			[Statistics.CounterNameAttribute("Error")]
			public static Statistics.Counter32 Error = new Statistics.Counter32();

			// Token: 0x040006D0 RID: 1744
			[Statistics.CounterNameAttribute("RowAdded")]
			public static Statistics.Counter32 RowAdded = new Statistics.Counter32();

			// Token: 0x040006D1 RID: 1745
			[Statistics.CounterNameAttribute("RowModified")]
			public static Statistics.Counter32 RowModified = new Statistics.Counter32();

			// Token: 0x040006D2 RID: 1746
			[Statistics.CounterNameAttribute("RowDeleted")]
			public static Statistics.Counter32 RowDeleted = new Statistics.Counter32();

			// Token: 0x040006D3 RID: 1747
			[Statistics.CounterNameAttribute("SortDone")]
			public static Statistics.Counter32 SortDone = new Statistics.Counter32();

			// Token: 0x040006D4 RID: 1748
			[Statistics.CounterNameAttribute("RestrictDone")]
			public static Statistics.Counter32 RestrictDone = new Statistics.Counter32();

			// Token: 0x040006D5 RID: 1749
			[Statistics.CounterNameAttribute("SetcolDone")]
			public static Statistics.Counter32 SetcolDone = new Statistics.Counter32();

			// Token: 0x040006D6 RID: 1750
			[Statistics.CounterNameAttribute("Reload")]
			public static Statistics.Counter32 Reload = new Statistics.Counter32();
		}

		// Token: 0x02000095 RID: 149
		[Statistics.GroupNameAttribute("Unsorted")]
		public static class Unsorted
		{
			// Token: 0x040006D7 RID: 1751
			public static Statistics.Counter32Max MaxPIColumnIndex = new Statistics.Counter32Max();
		}

		// Token: 0x02000096 RID: 150
		[Statistics.GroupNameAttribute("StatementLength")]
		public static class StatementLength
		{
			// Token: 0x040006D8 RID: 1752
			public static Statistics.AveragesGroup Averages = new Statistics.AveragesGroup();
		}

		// Token: 0x02000097 RID: 151
		public class GroupNameAttribute : Attribute
		{
			// Token: 0x0600078D RID: 1933 RVA: 0x000154E7 File Offset: 0x000136E7
			public GroupNameAttribute(string name)
			{
				this.name = name;
			}

			// Token: 0x170001B9 RID: 441
			// (get) Token: 0x0600078E RID: 1934 RVA: 0x000154F6 File Offset: 0x000136F6
			public string Name
			{
				get
				{
					return this.name;
				}
			}

			// Token: 0x040006D9 RID: 1753
			private string name;
		}

		// Token: 0x02000098 RID: 152
		public class CounterNameAttribute : Attribute
		{
			// Token: 0x0600078F RID: 1935 RVA: 0x000154FE File Offset: 0x000136FE
			public CounterNameAttribute(string name)
			{
				this.name = name;
			}

			// Token: 0x170001BA RID: 442
			// (get) Token: 0x06000790 RID: 1936 RVA: 0x0001550D File Offset: 0x0001370D
			public string Name
			{
				get
				{
					return this.name;
				}
			}

			// Token: 0x040006DA RID: 1754
			private string name;
		}

		// Token: 0x02000099 RID: 153
		public abstract class StatisticElement
		{
			// Token: 0x06000791 RID: 1937 RVA: 0x00015515 File Offset: 0x00013715
			public virtual void Initialize()
			{
			}

			// Token: 0x06000792 RID: 1938 RVA: 0x00015517 File Offset: 0x00013717
			public virtual void Reset()
			{
			}
		}

		// Token: 0x0200009A RID: 154
		public abstract class Counter : Statistics.StatisticElement
		{
			// Token: 0x170001BB RID: 443
			// (get) Token: 0x06000794 RID: 1940
			public abstract long CurrentValue { get; }

			// Token: 0x06000795 RID: 1941 RVA: 0x00015524 File Offset: 0x00013724
			public override string ToString()
			{
				return this.CurrentValue.ToString();
			}
		}

		// Token: 0x0200009B RID: 155
		public class Counter32 : Statistics.Counter
		{
			// Token: 0x06000797 RID: 1943 RVA: 0x00015547 File Offset: 0x00013747
			[Conditional("STATISTICS")]
			public void Bump()
			{
				Interlocked.Increment(ref this.value);
			}

			// Token: 0x170001BC RID: 444
			// (get) Token: 0x06000798 RID: 1944 RVA: 0x00015555 File Offset: 0x00013755
			public override long CurrentValue
			{
				get
				{
					return (long)this.value;
				}
			}

			// Token: 0x06000799 RID: 1945 RVA: 0x0001555E File Offset: 0x0001375E
			public override void Reset()
			{
				this.value = 0;
			}

			// Token: 0x040006DB RID: 1755
			private int value;
		}

		// Token: 0x0200009C RID: 156
		public class Counter32Max : Statistics.Counter
		{
			// Token: 0x0600079B RID: 1947 RVA: 0x00015570 File Offset: 0x00013770
			[Conditional("STATISTICS")]
			public void Bump(int newValue)
			{
				int num = this.value;
				while (newValue > num)
				{
					int num2 = Interlocked.CompareExchange(ref this.value, newValue, num);
					if (num2 == num)
					{
						return;
					}
					num = num2;
				}
			}

			// Token: 0x170001BD RID: 445
			// (get) Token: 0x0600079C RID: 1948 RVA: 0x0001559F File Offset: 0x0001379F
			public override long CurrentValue
			{
				get
				{
					return (long)this.value;
				}
			}

			// Token: 0x0600079D RID: 1949 RVA: 0x000155A8 File Offset: 0x000137A8
			public override void Reset()
			{
				this.value = 0;
			}

			// Token: 0x040006DC RID: 1756
			private int value;
		}

		// Token: 0x0200009D RID: 157
		public class AveragesGroup : Statistics.StatisticElement
		{
			// Token: 0x0600079F RID: 1951 RVA: 0x000155B9 File Offset: 0x000137B9
			public override void Initialize()
			{
				this.averages = new Dictionary<string, List<int>>(50);
			}

			// Token: 0x060007A0 RID: 1952 RVA: 0x000155C8 File Offset: 0x000137C8
			public override void Reset()
			{
				if (this.averages != null)
				{
					this.averages.Clear();
				}
			}

			// Token: 0x060007A1 RID: 1953 RVA: 0x000155E0 File Offset: 0x000137E0
			[Conditional("STATISTICS")]
			public void AddSample(string name, int sample)
			{
				if (this.averages != null && ExTraceGlobals.StatisticsTracer.IsTraceEnabled(TraceType.PerformanceTrace))
				{
					using (LockManager.Lock(this.averages))
					{
						List<int> list;
						if (!this.averages.TryGetValue(name, out list))
						{
							list = new List<int>(200);
							this.averages.Add(name, list);
						}
						list.Add(sample);
					}
				}
			}

			// Token: 0x060007A2 RID: 1954 RVA: 0x00015660 File Offset: 0x00013860
			public override string ToString()
			{
				if (this.averages != null)
				{
					StringBuilder stringBuilder = new StringBuilder(this.averages.Count * 40);
					using (LockManager.Lock(this.averages))
					{
						foreach (KeyValuePair<string, List<int>> keyValuePair in this.averages)
						{
							keyValuePair.Value.Sort();
							stringBuilder.Append(" name:[");
							stringBuilder.Append(keyValuePair.Key);
							stringBuilder.Append("] cnt:[");
							stringBuilder.Append(keyValuePair.Value.Count);
							stringBuilder.Append("] avg:[");
							long num = 0L;
							foreach (int num2 in keyValuePair.Value)
							{
								num += (long)num2;
							}
							stringBuilder.Append(num / (long)keyValuePair.Value.Count);
							stringBuilder.Append("]");
							if (keyValuePair.Value.Count <= 10)
							{
								stringBuilder.Append(" all:[");
								stringBuilder.AppendAsString(keyValuePair.Value);
								stringBuilder.Append("]");
							}
							else
							{
								for (int i = 70; i < 100; i += 5)
								{
									stringBuilder.Append(" ");
									stringBuilder.Append(i);
									stringBuilder.Append("%:[");
									stringBuilder.AppendAsString(keyValuePair.Value[(int)((double)keyValuePair.Value.Count / 100.0 * (double)i) - 1]);
									stringBuilder.Append("]");
								}
								stringBuilder.Append(" 100%:[");
								stringBuilder.Append(keyValuePair.Value[keyValuePair.Value.Count - 1]);
								stringBuilder.Append("]");
							}
						}
					}
					return stringBuilder.ToString();
				}
				return "empty";
			}

			// Token: 0x040006DD RID: 1757
			private Dictionary<string, List<int>> averages;
		}

		// Token: 0x0200009E RID: 158
		internal struct StatisticsEntry
		{
			// Token: 0x060007A4 RID: 1956 RVA: 0x000158D0 File Offset: 0x00013AD0
			public StatisticsEntry(string name, Statistics.StatisticElement element)
			{
				this.EntryName = name;
				this.Element = element;
			}

			// Token: 0x040006DE RID: 1758
			public string EntryName;

			// Token: 0x040006DF RID: 1759
			public Statistics.StatisticElement Element;
		}

		// Token: 0x0200009F RID: 159
		internal struct StatisticsGroup
		{
			// Token: 0x060007A5 RID: 1957 RVA: 0x000158E0 File Offset: 0x00013AE0
			public StatisticsGroup(string name, List<Statistics.StatisticsEntry> entries)
			{
				this.GroupName = name;
				this.Entries = entries;
			}

			// Token: 0x040006E0 RID: 1760
			public string GroupName;

			// Token: 0x040006E1 RID: 1761
			public List<Statistics.StatisticsEntry> Entries;
		}
	}
}
