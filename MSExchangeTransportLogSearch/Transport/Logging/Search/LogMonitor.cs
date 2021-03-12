using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Security;
using System.Threading;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.LogSearch;
using Microsoft.Exchange.Threading;

namespace Microsoft.Exchange.Transport.Logging.Search
{
	// Token: 0x02000027 RID: 39
	internal class LogMonitor
	{
		// Token: 0x17000016 RID: 22
		// (get) Token: 0x060000AD RID: 173 RVA: 0x000057DF File Offset: 0x000039DF
		public bool Initializing
		{
			get
			{
				return this.initializing;
			}
		}

		// Token: 0x060000AE RID: 174 RVA: 0x000057E8 File Offset: 0x000039E8
		public LogMonitor(string path, string prefix, string server, string extension, CsvTable csvTable, Dictionary<string, double> indexPercentageByPrefix)
		{
			ExTraceGlobals.ServiceTracer.TraceDebug((long)this.GetHashCode(), "MsExchangeLogSearch constructs LogMoniter with path {0}, prefix {1}, server {2}, file extension {3}", new object[]
			{
				path,
				prefix,
				server,
				extension
			});
			this.path = path;
			this.extension = "." + extension.ToUpperInvariant();
			this.csvTable = csvTable;
			this.prefix = prefix.ToUpperInvariant();
			this.pattern = string.Format("{0}*????????-*{1}", this.prefix, this.extension);
			this.indexPercentageByPrefix = indexPercentageByPrefix;
		}

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x060000AF RID: 175 RVA: 0x000058C5 File Offset: 0x00003AC5
		public string Path
		{
			get
			{
				return this.path;
			}
		}

		// Token: 0x060000B0 RID: 176 RVA: 0x00005934 File Offset: 0x00003B34
		public void StartAsync()
		{
			ThreadPool.QueueUserWorkItem(delegate(object args)
			{
				lock (this.syncLock)
				{
					ExTraceGlobals.ServiceTracer.TraceDebug((long)this.GetHashCode(), "Async startup in progress");
					this.asyncStartLockAcquireEvent.Set();
					this.Start();
				}
			});
			this.asyncStartLockAcquireEvent.WaitOne();
			ExTraceGlobals.ServiceTracer.TraceDebug((long)this.GetHashCode(), "Async startup thread progressing, returning from StartAsync");
		}

		// Token: 0x060000B1 RID: 177 RVA: 0x0000596C File Offset: 0x00003B6C
		public void Start()
		{
			this.initializing = true;
			this.startupTimer = Stopwatch.StartNew();
			this.shutdown = false;
			ExTraceGlobals.ServiceTracer.TraceDebug((long)this.GetHashCode(), "MsExchangeLogSearch LogMonitor Start begun");
			this.RefreshViewOfDirectory();
			this.refreshTimer = new GuardedTimer(new TimerCallback(this.PeriodicRefresh), null, LogSearchAppConfig.Instance.LogSearchIndexing.RefreshInterval, LogMonitor.infiniteInterval);
			this.RefreshFileSystemWatcher();
			this.startupTimer.Stop();
			this.initializing = false;
			ExTraceGlobals.ServiceTracer.TraceDebug<TimeSpan>((long)this.GetHashCode(), "MsExchangeLogSearch LogMonitor Start finished, time: {0}", this.startupTimer.Elapsed);
			LogSearchService.Logger.LogEvent(MSExchangeTransportLogSearchEventLogConstants.Tuple_LogSearchServiceIndexingComplete, null, new object[]
			{
				this.startupTimer.Elapsed.Seconds
			});
		}

		// Token: 0x060000B2 RID: 178 RVA: 0x00005A48 File Offset: 0x00003C48
		public void Stop()
		{
			ExTraceGlobals.ServiceTracer.TraceDebug((long)this.GetHashCode(), "MsExchangeLogSearch LogMoniter Stop");
			lock (this.syncLock)
			{
				this.shutdown = true;
				if (this.watcher != null)
				{
					this.watcher.Dispose();
					this.watcher = null;
				}
				if (this.refreshTimer != null)
				{
					this.refreshTimer.Dispose(false);
					this.refreshTimer = null;
				}
			}
		}

		// Token: 0x060000B3 RID: 179 RVA: 0x00005AD4 File Offset: 0x00003CD4
		public List<LogFileInfo> GetFileInfoForTimeRange(DateTime start, DateTime end)
		{
			ExTraceGlobals.ServiceTracer.TraceDebug<string, string>((long)this.GetHashCode(), "MsExchangeLogSearch LogMoniter GetFileInfoFortimeRange with start time {0} and end time {1}", start.ToString(), end.ToString());
			List<LogFileInfo> list = new List<LogFileInfo>();
			LogFileInfo item = new LogFileInfo(start);
			LogFileInfo item2 = new LogFileInfo(end);
			try
			{
				this.fileListLock.EnterReadLock();
				foreach (List<LogFileInfo> list2 in this.filesByDate.Values)
				{
					int num = list2.BinarySearch(item, LogMonitor.ByDateComparer.Default);
					int num2 = list2.BinarySearch(item2, LogMonitor.ByDateComparer.Default);
					if (num < 0)
					{
						num = ~num;
						if (num > 0)
						{
							num--;
						}
					}
					if (num2 < 0)
					{
						num2 = ~num2;
					}
					for (int i = num; i < num2; i++)
					{
						list.Add(list2[i]);
					}
				}
			}
			finally
			{
				this.fileListLock.ExitReadLock();
			}
			return list;
		}

		// Token: 0x060000B4 RID: 180 RVA: 0x00005BEC File Offset: 0x00003DEC
		private void RefreshFileSystemWatcher()
		{
			ExTraceGlobals.ServiceTracer.TraceDebug((long)this.GetHashCode(), "MsExchangeLogSearch LogMoniter RefreshFileSystemWatcher");
			if (this.watcher != null)
			{
				this.watcher.Dispose();
				this.watcher = null;
			}
			Exception ex = null;
			try
			{
				this.watcher = new FileSystemWatcher(this.path, this.pattern);
				this.watcher.Created += this.OnFileCreated;
				this.watcher.EnableRaisingEvents = true;
			}
			catch (ArgumentException ex2)
			{
				ex = ex2;
			}
			catch (FileNotFoundException ex3)
			{
				ex = ex3;
			}
			if (ex != null)
			{
				ExTraceGlobals.ServiceTracer.TraceError<string>((long)this.GetHashCode(), "MsExchangeLogSearch LogMoniter FileSystemWatcher failure. The error is {0}", ex.ToString());
				this.watcher = null;
			}
		}

		// Token: 0x060000B5 RID: 181 RVA: 0x00005CB4 File Offset: 0x00003EB4
		private void PeriodicRefresh(object state)
		{
			lock (this.syncLock)
			{
				if (!this.shutdown)
				{
					ExTraceGlobals.ServiceTracer.TraceDebug((long)this.GetHashCode(), "MsExchangeLogSearch LogMoniter PeriodicRefresh");
					this.refreshCount++;
					if (this.refreshCount == 60)
					{
						try
						{
							this.fileListLock.EnterWriteLock();
							this.RefreshViewOfDirectory();
						}
						finally
						{
							this.fileListLock.ExitWriteLock();
						}
						this.refreshCount = 0;
					}
					List<LogFileInfo> list = new List<LogFileInfo>();
					try
					{
						this.fileListLock.EnterReadLock();
						foreach (List<LogFileInfo> list2 in this.filesByDate.Values)
						{
							if (list2.Count > 0)
							{
								list.Add(list2[list2.Count - 1]);
							}
						}
					}
					finally
					{
						this.fileListLock.ExitReadLock();
					}
					foreach (LogFileInfo logFileInfo in list)
					{
						logFileInfo.ContinueProcessingActiveFile();
					}
					this.refreshTimer.Change(LogSearchAppConfig.Instance.LogSearchIndexing.RefreshInterval, LogMonitor.infiniteInterval);
				}
			}
		}

		// Token: 0x060000B6 RID: 182 RVA: 0x00005E80 File Offset: 0x00004080
		private void OnFileCreated(object sender, FileSystemEventArgs e)
		{
			lock (this.syncLock)
			{
				if (!this.shutdown)
				{
					this.RefreshViewOfDirectory();
					this.RefreshFileSystemWatcher();
				}
			}
		}

		// Token: 0x060000B7 RID: 183 RVA: 0x00005EFC File Offset: 0x000040FC
		private void RefreshViewOfDirectory()
		{
			ExTraceGlobals.ServiceTracer.TraceDebug((long)this.GetHashCode(), "MsExchangeLogSearch LogMonitor ReadDirectory");
			Exception ex = null;
			FileInfo[] source = null;
			try
			{
				DirectoryInfo directoryInfo = new DirectoryInfo(this.path);
				source = directoryInfo.GetFiles(this.pattern, SearchOption.TopDirectoryOnly);
			}
			catch (UnauthorizedAccessException ex2)
			{
				ex = ex2;
			}
			catch (SecurityException ex3)
			{
				ex = ex3;
			}
			catch (IOException ex4)
			{
				ex = ex4;
			}
			catch (ArgumentException ex5)
			{
				ex = ex5;
			}
			if (ex != null)
			{
				ExTraceGlobals.ServiceTracer.TraceDebug<string, string>((long)this.GetHashCode(), "Could not open directory {0}, files will not be refreshed, exception: {1}", this.path, ex.ToString());
				return;
			}
			IOrderedEnumerable<FileInfo> source2 = from fileInfo in source
			orderby fileInfo.CreationTimeUtc descending
			select fileInfo;
			ulong indexSizeLimit = LogSearchIndexingParameters.GetIndexLimit(this.prefix);
			ulong accumulatedIndexSize = 0UL;
			IEnumerable<FileInfo> source3 = source2.TakeWhile((FileInfo fileInfo) => this.IsIndexUnderLimit(fileInfo, indexSizeLimit, ref accumulatedIndexSize));
			Dictionary<string, List<FileInfo>> dictionary = new Dictionary<string, List<FileInfo>>();
			foreach (FileInfo fileInfo2 in source3.Reverse<FileInfo>())
			{
				if (fileInfo2.Exists)
				{
					string fullPrefix = LogMonitor.GetFullPrefix(fileInfo2.FullName);
					List<FileInfo> list;
					if (!dictionary.TryGetValue(fullPrefix, out list))
					{
						dictionary[fullPrefix] = new List<FileInfo>();
					}
					dictionary[fullPrefix].Add(fileInfo2);
				}
			}
			this.ProcessNewFiles(dictionary);
			this.RetireUnusedFiles(dictionary);
		}

		// Token: 0x060000B8 RID: 184 RVA: 0x000060B0 File Offset: 0x000042B0
		private void RetireUnusedFiles(Dictionary<string, List<FileInfo>> dirFilesByPrefix)
		{
			foreach (List<FileInfo> list in dirFilesByPrefix.Values)
			{
				list.Sort(LogMonitor.FileInfoNameComparer.Default);
			}
			List<LogFileInfo> list2 = new List<LogFileInfo>();
			try
			{
				this.fileListLock.EnterReadLock();
				foreach (string key in this.filesByName.Keys)
				{
					List<LogFileInfo> list3 = this.filesByName[key];
					List<FileInfo> list4 = null;
					if (!dirFilesByPrefix.TryGetValue(key, out list4))
					{
						list2.AddRange(list3);
					}
					else
					{
						foreach (LogFileInfo logFileInfo in list3)
						{
							FileInfo item = new FileInfo(logFileInfo.FullName);
							int num = list4.BinarySearch(item, LogMonitor.FileInfoNameComparer.Default);
							if (num < 0)
							{
								list2.Add(logFileInfo);
							}
						}
					}
				}
			}
			finally
			{
				this.fileListLock.ExitReadLock();
			}
			if (list2.Count == 0)
			{
				return;
			}
			try
			{
				this.fileListLock.EnterWriteLock();
				foreach (LogFileInfo logFileInfo2 in list2)
				{
					List<LogFileInfo> list5 = null;
					if (this.filesByDate.TryGetValue(logFileInfo2.Prefix, out list5))
					{
						list5.Remove(logFileInfo2);
					}
					if (this.filesByName.TryGetValue(logFileInfo2.Prefix, out list5))
					{
						list5.Remove(logFileInfo2);
					}
					logFileInfo2.ProcessFileDeletion();
				}
			}
			finally
			{
				this.fileListLock.ExitWriteLock();
			}
		}

		// Token: 0x060000B9 RID: 185 RVA: 0x000062B4 File Offset: 0x000044B4
		private void ProcessNewFiles(Dictionary<string, List<FileInfo>> dirFilesByPrefix)
		{
			List<LogFileInfo> list = new List<LogFileInfo>();
			try
			{
				this.fileListLock.EnterReadLock();
				foreach (string key in dirFilesByPrefix.Keys)
				{
					List<FileInfo> list2 = dirFilesByPrefix[key];
					List<LogFileInfo> list3 = null;
					if (!this.filesByName.TryGetValue(key, out list3))
					{
						list3 = new List<LogFileInfo>();
					}
					for (int i = 0; i < list2.Count; i++)
					{
						try
						{
							FileInfo fileInfo = list2[i];
							int num = list3.BinarySearch(new LogFileInfo(fileInfo.FullName), LogMonitor.ByNameComparer.Default);
							bool flag = i == list2.Count - 1;
							if (num < 0)
							{
								LogFileInfo logFileInfo = new LogFileInfo(fileInfo.FullName, fileInfo.Length, fileInfo.CreationTimeUtc, this.csvTable, flag);
								if (!logFileInfo.TryProcessFile())
								{
									ExTraceGlobals.ServiceTracer.TraceError((long)this.GetHashCode(), "Failed to process file due to I/O error, not processing file now");
									return;
								}
								list.Add(logFileInfo);
							}
							else
							{
								list3[num].MarkInactiveIfNeeded(flag);
							}
						}
						catch (FileNotFoundException arg)
						{
							ExTraceGlobals.ServiceTracer.TraceDebug<FileNotFoundException>((long)this.GetHashCode(), "Encountered deleted file {0}.", arg);
						}
					}
				}
			}
			finally
			{
				this.fileListLock.ExitReadLock();
			}
			if (list.Count != 0)
			{
				try
				{
					this.fileListLock.EnterWriteLock();
					foreach (LogFileInfo logFileInfo2 in list)
					{
						this.InsertByPrefix(logFileInfo2, this.filesByDate, LogMonitor.ByDateComparer.Default);
						this.InsertByPrefix(logFileInfo2, this.filesByName, LogMonitor.ByNameComparer.Default);
					}
				}
				finally
				{
					this.fileListLock.ExitWriteLock();
				}
				return;
			}
		}

		// Token: 0x060000BA RID: 186 RVA: 0x000064F8 File Offset: 0x000046F8
		private void InsertByPrefix(LogFileInfo logFileInfo, Dictionary<string, List<LogFileInfo>> listsByPrefix, IComparer<LogFileInfo> comparer)
		{
			List<LogFileInfo> list;
			if (!listsByPrefix.TryGetValue(logFileInfo.Prefix, out list))
			{
				list = new List<LogFileInfo>();
				listsByPrefix[logFileInfo.Prefix] = list;
			}
			int num = list.BinarySearch(logFileInfo, comparer);
			if (num < 0)
			{
				num = ~num;
			}
			list.Insert(num, logFileInfo);
		}

		// Token: 0x060000BB RID: 187 RVA: 0x00006540 File Offset: 0x00004740
		private bool TryParseName(string name, out DateTime timestamp)
		{
			ExTraceGlobals.ServiceTracer.TraceDebug<string>((long)this.GetHashCode(), "MsExchangeLogSearch LogMoniter TryParseName {0}", name);
			timestamp = default(DateTime);
			return this.HasPrefix(name) && this.HasExtension(name) && this.TryGetTimestampFromName(name, out timestamp);
		}

		// Token: 0x060000BC RID: 188 RVA: 0x00006580 File Offset: 0x00004780
		private bool TryRefreshFileInfo(FileInfo i)
		{
			ExTraceGlobals.ServiceTracer.TraceDebug((long)this.GetHashCode(), "MsExchangeLogSearch LogMoniter TryRefreshFileInfo");
			bool result;
			try
			{
				i.Refresh();
				result = true;
			}
			catch (IOException ex)
			{
				ExTraceGlobals.ServiceTracer.TraceError<string>((long)this.GetHashCode(), "MsExchangeLogSearch LogMoniter TryRefreshFileInfo failed with exception {0}", ex.Message);
				result = false;
			}
			catch (UnauthorizedAccessException ex2)
			{
				ExTraceGlobals.ServiceTracer.TraceError<string>((long)this.GetHashCode(), "MsExchangeLogSearch LogMoniter TryRefreshFileInfo failed with exception {0}", ex2.Message);
				result = false;
			}
			return result;
		}

		// Token: 0x060000BD RID: 189 RVA: 0x0000660C File Offset: 0x0000480C
		private bool HasPrefix(string name)
		{
			return name.StartsWith(this.prefix);
		}

		// Token: 0x060000BE RID: 190 RVA: 0x0000661A File Offset: 0x0000481A
		private bool HasExtension(string name)
		{
			return name.EndsWith(this.extension);
		}

		// Token: 0x060000BF RID: 191 RVA: 0x00006628 File Offset: 0x00004828
		private bool TryGetTimestampFromName(string name, out DateTime timestamp)
		{
			ExTraceGlobals.ServiceTracer.TraceDebug<string>((long)this.GetHashCode(), "MsExchangeLogSearch LogMoniter TryGetTimestampFromName {0}", name);
			timestamp = default(DateTime);
			int num = name.Length - this.extension.Length - "yyyyMMddTHHmmssfff".Length;
			if (num < this.prefix.Length)
			{
				return false;
			}
			int year;
			if (!LogMonitor.TryGetDigits(name, num, 4, out year))
			{
				return false;
			}
			num += 4;
			int month;
			if (!LogMonitor.TryGetDigits(name, num, 2, out month))
			{
				return false;
			}
			num += 2;
			int day;
			if (!LogMonitor.TryGetDigits(name, num, 2, out day))
			{
				return false;
			}
			num += 2;
			if (name[num++] != 'T')
			{
				return false;
			}
			int hour;
			if (!LogMonitor.TryGetDigits(name, num, 2, out hour))
			{
				return false;
			}
			num += 2;
			int minute;
			if (!LogMonitor.TryGetDigits(name, num, 2, out minute))
			{
				return false;
			}
			num += 2;
			int second;
			if (!LogMonitor.TryGetDigits(name, num, 2, out second))
			{
				return false;
			}
			num += 2;
			int millisecond;
			if (!LogMonitor.TryGetDigits(name, num, 3, out millisecond))
			{
				return false;
			}
			num += 3;
			try
			{
				timestamp = new DateTime(year, month, day, hour, minute, second, millisecond);
			}
			catch (ArgumentException)
			{
				return false;
			}
			return true;
		}

		// Token: 0x060000C0 RID: 192 RVA: 0x00006744 File Offset: 0x00004944
		private bool IsIndexUnderLimit(FileInfo fileInfo, ulong indexSizeLimit, ref ulong accumulatedIndexSize)
		{
			long num = 0L;
			try
			{
				if (fileInfo.Exists)
				{
					num = fileInfo.Length;
				}
			}
			catch (FileNotFoundException arg)
			{
				ExTraceGlobals.ServiceTracer.TraceDebug<FileNotFoundException>((long)this.GetHashCode(), "Encountered deleted file {0}.", arg);
			}
			if (0L < num)
			{
				double indexPercentage = this.GetIndexPercentage(fileInfo);
				accumulatedIndexSize += Convert.ToUInt64(indexPercentage * (double)num);
				if (accumulatedIndexSize > indexSizeLimit)
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x060000C1 RID: 193 RVA: 0x000067B4 File Offset: 0x000049B4
		private double GetIndexPercentage(FileInfo fileInfo)
		{
			string fullPrefix = LogMonitor.GetFullPrefix(fileInfo.FullName);
			double result;
			if (this.indexPercentageByPrefix != null && this.indexPercentageByPrefix.TryGetValue(fullPrefix, out result))
			{
				return result;
			}
			return LogSearchIndexingParameters.GetDefaultIndexPercentage(this.prefix);
		}

		// Token: 0x060000C2 RID: 194 RVA: 0x000067F4 File Offset: 0x000049F4
		private static bool TryGetDigits(string src, int offset, int count, out int result)
		{
			ExTraceGlobals.ServiceTracer.TraceDebug(0L, "MsExchangeLogSearch LogMoniter TryGetDigits");
			result = 0;
			int i = offset;
			int num = offset + count;
			while (i < num)
			{
				char c = src[i++];
				if (c < '0' || c > '9')
				{
					return false;
				}
				result *= 10;
				result += (int)(c - '0');
			}
			return true;
		}

		// Token: 0x060000C3 RID: 195 RVA: 0x0000684C File Offset: 0x00004A4C
		public static string GetFullPrefix(string fileName)
		{
			ExTraceGlobals.ServiceTracer.TraceDebug<string>(0L, "MsExchangeLogSearch LogMoniter GetfullPrefix for file {0}", fileName);
			string fileNameWithoutExtension = System.IO.Path.GetFileNameWithoutExtension(fileName);
			int num = fileNameWithoutExtension.LastIndexOf('-') - "yyyyMMdd".Length;
			if (num <= 0)
			{
				throw new ArgumentException("invalid log file name");
			}
			return fileNameWithoutExtension.Substring(0, num).ToUpperInvariant();
		}

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x060000C4 RID: 196 RVA: 0x000068A2 File Offset: 0x00004AA2
		public Dictionary<string, List<LogFileInfo>> ByDateListTest
		{
			get
			{
				return this.filesByDate;
			}
		}

		// Token: 0x17000019 RID: 25
		// (set) Token: 0x060000C5 RID: 197 RVA: 0x000068AA File Offset: 0x00004AAA
		public static TimeSpan RefreshIntervalTest
		{
			set
			{
				LogSearchAppConfig.Instance.LogSearchIndexing.RefreshInterval = value;
			}
		}

		// Token: 0x060000C6 RID: 198 RVA: 0x000068BC File Offset: 0x00004ABC
		public void RefreshTest()
		{
			this.PeriodicRefresh(null);
			this.DisableWatcher();
		}

		// Token: 0x060000C7 RID: 199 RVA: 0x000068CB File Offset: 0x00004ACB
		public void SetupDirectoryRefresh()
		{
			this.refreshCount = 59;
		}

		// Token: 0x060000C8 RID: 200 RVA: 0x000068D5 File Offset: 0x00004AD5
		public void DisableWatcher()
		{
			if (this.watcher != null)
			{
				this.watcher.Dispose();
				this.watcher = null;
			}
		}

		// Token: 0x04000067 RID: 103
		private const int DirectoryViewRefreshFrequency = 60;

		// Token: 0x04000068 RID: 104
		private static TimeSpan infiniteInterval = new TimeSpan(0, 0, 0, 0, -1);

		// Token: 0x04000069 RID: 105
		private Stopwatch startupTimer = Stopwatch.StartNew();

		// Token: 0x0400006A RID: 106
		private string prefix;

		// Token: 0x0400006B RID: 107
		private string extension;

		// Token: 0x0400006C RID: 108
		private string pattern;

		// Token: 0x0400006D RID: 109
		private string path;

		// Token: 0x0400006E RID: 110
		private bool initializing;

		// Token: 0x0400006F RID: 111
		private ReaderWriterLockSlim fileListLock = new ReaderWriterLockSlim(LockRecursionPolicy.SupportsRecursion);

		// Token: 0x04000070 RID: 112
		private int refreshCount;

		// Token: 0x04000071 RID: 113
		private Dictionary<string, List<LogFileInfo>> filesByDate = new Dictionary<string, List<LogFileInfo>>();

		// Token: 0x04000072 RID: 114
		private Dictionary<string, List<LogFileInfo>> filesByName = new Dictionary<string, List<LogFileInfo>>();

		// Token: 0x04000073 RID: 115
		private FileSystemWatcher watcher;

		// Token: 0x04000074 RID: 116
		private GuardedTimer refreshTimer;

		// Token: 0x04000075 RID: 117
		private readonly object syncLock = new object();

		// Token: 0x04000076 RID: 118
		private bool shutdown;

		// Token: 0x04000077 RID: 119
		private CsvTable csvTable;

		// Token: 0x04000078 RID: 120
		private Dictionary<string, double> indexPercentageByPrefix;

		// Token: 0x04000079 RID: 121
		private EventWaitHandle asyncStartLockAcquireEvent = new EventWaitHandle(false, EventResetMode.ManualReset);

		// Token: 0x02000028 RID: 40
		internal class ByDateComparer : IComparer<LogFileInfo>
		{
			// Token: 0x060000CC RID: 204 RVA: 0x00006904 File Offset: 0x00004B04
			public int Compare(LogFileInfo x, LogFileInfo y)
			{
				return x.StartTime.CompareTo(y.StartTime);
			}

			// Token: 0x0400007B RID: 123
			public static LogMonitor.ByDateComparer Default = new LogMonitor.ByDateComparer();
		}

		// Token: 0x02000029 RID: 41
		internal class ByNameComparer : IComparer<LogFileInfo>
		{
			// Token: 0x060000CF RID: 207 RVA: 0x00006939 File Offset: 0x00004B39
			public int Compare(LogFileInfo x, LogFileInfo y)
			{
				return x.FullName.CompareTo(y.FullName);
			}

			// Token: 0x0400007C RID: 124
			public static LogMonitor.ByNameComparer Default = new LogMonitor.ByNameComparer();
		}

		// Token: 0x0200002A RID: 42
		internal class FileInfoNameComparer : IComparer<FileInfo>
		{
			// Token: 0x060000D2 RID: 210 RVA: 0x00006960 File Offset: 0x00004B60
			public int Compare(FileInfo x, FileInfo y)
			{
				return x.FullName.ToLower().CompareTo(y.FullName.ToLower());
			}

			// Token: 0x0400007D RID: 125
			public static LogMonitor.FileInfoNameComparer Default = new LogMonitor.FileInfoNameComparer();
		}

		// Token: 0x0200002B RID: 43
		internal class FileInfoDateComparer : IComparer<FileInfo>
		{
			// Token: 0x060000D5 RID: 213 RVA: 0x00006994 File Offset: 0x00004B94
			public int Compare(FileInfo x, FileInfo y)
			{
				return x.CreationTimeUtc.CompareTo(y.CreationTimeUtc);
			}

			// Token: 0x0400007E RID: 126
			public static LogMonitor.FileInfoDateComparer Default = new LogMonitor.FileInfoDateComparer();
		}
	}
}
