using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;
using System.Threading;

namespace Microsoft.Exchange.Diagnostics
{
	// Token: 0x020001BC RID: 444
	internal class LogDirectory
	{
		// Token: 0x06000C50 RID: 3152 RVA: 0x0002D40C File Offset: 0x0002B60C
		public LogDirectory(string path, string prefix, TimeSpan maxAge, long maxLogFileSize, long maxDirectorySize, string logComponent) : this(path, prefix, maxAge, maxLogFileSize, maxDirectorySize, logComponent, false, 0, TimeSpan.MaxValue)
		{
		}

		// Token: 0x06000C51 RID: 3153 RVA: 0x0002D430 File Offset: 0x0002B630
		public LogDirectory(string path, string prefix, TimeSpan maxAge, long maxLogFileSize, long maxDirectorySize, string logComponent, bool enforceAccurateAge, int bufferSize, TimeSpan streamFlushInterval) : this(path, prefix, maxAge, maxLogFileSize, maxDirectorySize, logComponent, false, string.Empty, bufferSize, streamFlushInterval)
		{
		}

		// Token: 0x06000C52 RID: 3154 RVA: 0x0002D458 File Offset: 0x0002B658
		public LogDirectory(string path, string prefix, TimeSpan maxAge, long maxLogFileSize, long maxDirectorySize, string logComponent, bool enforceAccurateAge, int bufferSize, TimeSpan streamFlushInterval, bool flushToDisk) : this(path, prefix, maxAge, LogFileRollOver.Daily, maxLogFileSize, maxDirectorySize, logComponent, enforceAccurateAge, string.Empty, bufferSize, streamFlushInterval, flushToDisk)
		{
		}

		// Token: 0x06000C53 RID: 3155 RVA: 0x0002D484 File Offset: 0x0002B684
		public LogDirectory(string path, string prefix, TimeSpan maxAge, LogFileRollOver logFileRollOver, string logComponent, int bufferSize, TimeSpan streamFlushInterval, bool flushToDisk) : this(path, prefix, maxAge, logFileRollOver, 0L, 0L, logComponent, false, string.Empty, bufferSize, streamFlushInterval, flushToDisk)
		{
		}

		// Token: 0x06000C54 RID: 3156 RVA: 0x0002D4B0 File Offset: 0x0002B6B0
		public LogDirectory(string path, string prefix, TimeSpan maxAge, LogFileRollOver logFileRollOver, string logComponent, string note, int bufferSize, TimeSpan streamFlushInterval, bool flushToDisk) : this(path, prefix, maxAge, logFileRollOver, 0L, 0L, logComponent, false, note, bufferSize, streamFlushInterval, flushToDisk)
		{
		}

		// Token: 0x06000C55 RID: 3157 RVA: 0x0002D4D8 File Offset: 0x0002B6D8
		public LogDirectory(string path, string prefix, LogFileRollOver logFileRollOver, string logComponent, int bufferSize, TimeSpan streamFlushInterval) : this(path, prefix, TimeSpan.MaxValue, logFileRollOver, 0L, 0L, logComponent, false, string.Empty, bufferSize, streamFlushInterval)
		{
		}

		// Token: 0x06000C56 RID: 3158 RVA: 0x0002D504 File Offset: 0x0002B704
		public LogDirectory(string path, string prefix, LogFileRollOver logFileRollOver, string logComponent, int bufferSize, TimeSpan streamFlushInterval, bool flushToDisk) : this(path, prefix, TimeSpan.MaxValue, logFileRollOver, 0L, 0L, logComponent, false, string.Empty, bufferSize, streamFlushInterval, flushToDisk)
		{
		}

		// Token: 0x06000C57 RID: 3159 RVA: 0x0002D534 File Offset: 0x0002B734
		public LogDirectory(string path, string prefix, TimeSpan maxAge, long maxLogFileSize, long maxDirectorySize, string logComponent, bool enforceAccurateAge, string note, int bufferSize, TimeSpan streamFlushInterval) : this(path, prefix, maxAge, LogFileRollOver.Daily, maxLogFileSize, maxDirectorySize, logComponent, enforceAccurateAge, note, bufferSize, streamFlushInterval)
		{
		}

		// Token: 0x06000C58 RID: 3160 RVA: 0x0002D55C File Offset: 0x0002B75C
		public LogDirectory(string path, string prefix, TimeSpan maxAge, long maxLogFileSize, long maxDirectorySize, string logComponent, bool enforceAccurateAge, string note, int bufferSize, TimeSpan streamFlushInterval, bool flushtodisk) : this(path, prefix, maxAge, LogFileRollOver.Daily, maxLogFileSize, maxDirectorySize, logComponent, enforceAccurateAge, note, bufferSize, streamFlushInterval, flushtodisk)
		{
		}

		// Token: 0x06000C59 RID: 3161 RVA: 0x0002D584 File Offset: 0x0002B784
		private LogDirectory(string path, string prefix, TimeSpan maxAge, LogFileRollOver logFileRollOver, long maxLogFileSize, long maxDirectorySize, string logComponent, bool enforceAccurateAge, string note, int bufferSize, TimeSpan streamFlushInterval) : this(path, prefix, maxAge, logFileRollOver, maxLogFileSize, maxDirectorySize, logComponent, enforceAccurateAge, note, bufferSize, streamFlushInterval, false)
		{
		}

		// Token: 0x06000C5A RID: 3162 RVA: 0x0002D5AC File Offset: 0x0002B7AC
		private LogDirectory(string path, string prefix, TimeSpan maxAge, LogFileRollOver logFileRollOver, long maxLogFileSize, long maxDirectorySize, string logComponent, bool enforceAccurateAge, string note, int bufferSize, TimeSpan streamFlushInterval, bool flushToDisk)
		{
			if (streamFlushInterval <= TimeSpan.Zero)
			{
				throw new ArgumentOutOfRangeException("streamFlushInterval", streamFlushInterval, "streamFlushInterval should be greater than zero");
			}
			if (bufferSize < 0)
			{
				throw new ArgumentOutOfRangeException("bufferSize", bufferSize, "buffer size must be non-negative");
			}
			this.bufferLength = bufferSize;
			this.directory = Log.CreateLogDirectory(path);
			this.prefix = prefix;
			this.maxAge = maxAge;
			this.logFileRollOver = logFileRollOver;
			this.maxLogFileSize = maxLogFileSize;
			this.maxDirectorySize = maxDirectorySize;
			this.logComponent = logComponent;
			this.enforceAccurateAge = enforceAccurateAge;
			this.note = (note ?? string.Empty);
			this.flushToDisk = flushToDisk;
			this.streamFlushInterval = streamFlushInterval;
			if (maxLogFileSize != 0L)
			{
				this.matcher = new Regex(string.Concat(new string[]
				{
					"^",
					Regex.Escape(prefix),
					enforceAccurateAge ? "(?<year>\\d{4})(?<month>\\d{2})(?<day>\\d{2})(?<hour>\\d{0,2})-(?<instance>\\d+)(?<note>.*)" : "(?<year>\\d{4})(?<month>\\d{2})(?<day>\\d{2})-(?<instance>\\d+)(?<note>.*)",
					Regex.Escape(".LOG"),
					"$"
				}), RegexOptions.IgnoreCase);
				this.production = (enforceAccurateAge ? "{0}{1:yyyyMMddHH}-{2:d}{3}{4}" : "{0}{1:yyyyMMdd}-{2:d}{3}{4}");
				this.dirTemplate = (enforceAccurateAge ? (this.prefix + "????????*-*.LOG") : (this.prefix + "????????-*.LOG"));
				return;
			}
			switch (this.LogFileRollOver)
			{
			case LogFileRollOver.Hourly:
				this.matcher = new Regex(string.Concat(new string[]
				{
					"^",
					Regex.Escape(prefix),
					"(?<year>\\d{4})(?<month>\\d{2})(?<day>\\d{2})(?<hour>\\d{2})-(?<instance>\\d+)(?<note>.*)",
					Regex.Escape(".LOG"),
					"$"
				}), RegexOptions.IgnoreCase | RegexOptions.Compiled);
				this.production = "{0}{1:yyyyMMddHH}-{2:d}{3}{4}";
				this.dirTemplate = this.prefix + "??????????-*.LOG";
				return;
			case LogFileRollOver.Daily:
				this.matcher = new Regex(string.Concat(new string[]
				{
					"^",
					Regex.Escape(prefix),
					"(?<year>\\d{4})(?<month>\\d{2})(?<day>\\d{2})-(?<instance>\\d+)(?<note>.*)",
					Regex.Escape(".LOG"),
					"$"
				}), RegexOptions.IgnoreCase | RegexOptions.Compiled);
				this.production = "{0}{1:yyyyMMdd}-{2:d}{3}{4}";
				this.dirTemplate = this.prefix + "????????-*.LOG";
				return;
			case LogFileRollOver.Weekly:
				this.matcher = new Regex(string.Concat(new string[]
				{
					"^",
					Regex.Escape(prefix),
					"(?<year>\\d{4})(?<month>\\d{2})W(?<week>\\d{1})-(?<instance>\\d+)(?<note>.*)",
					Regex.Escape(".LOG"),
					"$"
				}), RegexOptions.IgnoreCase | RegexOptions.Compiled);
				this.production = "{0}{1:yyyyMM}W{5}-{2:d}{3}{4}";
				this.dirTemplate = this.prefix + "??????W?-*.LOG";
				return;
			case LogFileRollOver.Monthly:
				this.matcher = new Regex(string.Concat(new string[]
				{
					"^",
					Regex.Escape(prefix),
					"(?<year>\\d{4})(?<month>\\d{2})-(?<instance>\\d+)(?<note>.*)",
					Regex.Escape(".LOG"),
					"$"
				}), RegexOptions.IgnoreCase | RegexOptions.Compiled);
				this.production = "{0}{1:yyyyMM}-{2:d}{3}{4}";
				this.dirTemplate = this.prefix + "??????-*.LOG";
				return;
			default:
				throw new InvalidOperationException("The code should never be hit.");
			}
		}

		// Token: 0x14000006 RID: 6
		// (add) Token: 0x06000C5B RID: 3163 RVA: 0x0002D8F8 File Offset: 0x0002BAF8
		// (remove) Token: 0x06000C5C RID: 3164 RVA: 0x0002D930 File Offset: 0x0002BB30
		public event LogDirectory.OnDirSizeQuotaExceededHandler OnDirSizeQuotaExceeded;

		// Token: 0x17000284 RID: 644
		// (get) Token: 0x06000C5D RID: 3165 RVA: 0x0002D965 File Offset: 0x0002BB65
		internal LogFileRollOver LogFileRollOver
		{
			get
			{
				return this.logFileRollOver;
			}
		}

		// Token: 0x17000285 RID: 645
		// (get) Token: 0x06000C5E RID: 3166 RVA: 0x0002D96D File Offset: 0x0002BB6D
		internal string FullName
		{
			get
			{
				return this.directory.FullName;
			}
		}

		// Token: 0x17000286 RID: 646
		// (get) Token: 0x06000C5F RID: 3167 RVA: 0x0002D97A File Offset: 0x0002BB7A
		private string Note
		{
			get
			{
				return this.note;
			}
		}

		// Token: 0x17000287 RID: 647
		// (get) Token: 0x06000C60 RID: 3168 RVA: 0x0002D982 File Offset: 0x0002BB82
		private bool EnforceAccurateAge
		{
			get
			{
				return this.enforceAccurateAge;
			}
		}

		// Token: 0x17000288 RID: 648
		// (get) Token: 0x06000C61 RID: 3169 RVA: 0x0002D98A File Offset: 0x0002BB8A
		private long MaxLogFileSize
		{
			get
			{
				return this.maxLogFileSize;
			}
		}

		// Token: 0x06000C62 RID: 3170 RVA: 0x0002D992 File Offset: 0x0002BB92
		public Stream GetLogFile(DateTime forDate)
		{
			return this.GetLogFile(forDate, null, false);
		}

		// Token: 0x06000C63 RID: 3171 RVA: 0x0002D9A0 File Offset: 0x0002BBA0
		public Stream GetLogFile(DateTime forDate, LogHeaderFormatter logHeaderFormatter, bool forceLogFileRollOver)
		{
			if (this.logFile != null)
			{
				try
				{
					if (forceLogFileRollOver || !this.logFileId.SameLogSeries(forDate, this) || (this.maxLogFileSize > 0L && this.logFile.Position > this.maxLogFileSize))
					{
						this.logFile.Close();
						this.logFile = null;
					}
				}
				catch (ObjectDisposedException)
				{
					this.logFile = null;
				}
			}
			if (this.logFile == null)
			{
				lock (this.fileRollOverLock)
				{
					if (this.logFile == null)
					{
						SortedList<LogDirectory.LogFileId, FileInfo> logFileList = this.GetLogFileList();
						int num = this.EnforceDirectorySizeQuota(logFileList);
						if (num > 0)
						{
							Log.EventLog.LogEvent(CommonEventLogConstants.Tuple_DeleteLogDueToQuota, null, new object[]
							{
								this.logComponent,
								this.FullName,
								this.maxDirectorySize,
								num
							});
							LogDirectory.OnDirSizeQuotaExceededHandler onDirSizeQuotaExceeded = this.OnDirSizeQuotaExceeded;
							if (onDirSizeQuotaExceeded != null)
							{
								onDirSizeQuotaExceeded(this.logComponent, this.FullName, this.maxDirectorySize, num);
							}
						}
						if (this.maxAge < TimeSpan.MaxValue)
						{
							this.DeleteOldLogFiles(logFileList, num, forDate - this.maxAge);
						}
						bool startNewLog = forceLogFileRollOver || (logHeaderFormatter != null && logHeaderFormatter.CsvOption == LogHeaderCsvOption.CsvStrict);
						bool flag2 = false;
						this.logFileId = this.GenerateLogFileId(logFileList, forDate, startNewLog, out flag2);
						int i = 0;
						while (i < 20)
						{
							try
							{
								this.logFile = new LogDirectory.BufferedStream(this.GetLogFileNameFromId(this.logFileId), this.bufferLength, this.flushToDisk);
								if (this.maxLogFileSize > 0L && this.logFile.Position > this.maxLogFileSize)
								{
									this.logFile.Close();
									this.logFile = null;
								}
							}
							catch (IOException)
							{
								this.logFile = null;
							}
							if (this.logFile == null)
							{
								this.logFileId = this.logFileId.Next;
								flag2 = true;
								i++;
							}
							else
							{
								if (logHeaderFormatter != null && flag2)
								{
									logHeaderFormatter.Write(this.logFile, forDate);
								}
								if (this.streamFlushTimer == null && this.streamFlushInterval != TimeSpan.MaxValue)
								{
									this.streamFlushTimer = new Timer(new TimerCallback(this.FlushStream), null, TimeSpan.Zero, this.streamFlushInterval);
									break;
								}
								break;
							}
						}
					}
				}
			}
			return this.logFile;
		}

		// Token: 0x06000C64 RID: 3172 RVA: 0x0002DC44 File Offset: 0x0002BE44
		public void Flush()
		{
			this.FlushStream(null);
		}

		// Token: 0x06000C65 RID: 3173 RVA: 0x0002DC4D File Offset: 0x0002BE4D
		public void Close()
		{
			if (this.streamFlushTimer != null)
			{
				this.streamFlushTimer.Dispose();
				this.streamFlushTimer = null;
			}
			if (this.logFile != null)
			{
				this.logFile.Close();
				this.logFile = null;
			}
		}

		// Token: 0x06000C66 RID: 3174 RVA: 0x0002DC84 File Offset: 0x0002BE84
		private static DateTime GetFirstDateForWeek(int year, int month, int week)
		{
			if (week < 1 || week > 6)
			{
				throw new ArgumentOutOfRangeException("week", "Week number is out of range");
			}
			DateTime result = new DateTime(year, month, 1);
			int num = 7 + CultureInfo.CurrentCulture.DateTimeFormat.FirstDayOfWeek - result.DayOfWeek;
			if (week > 1)
			{
				result = result.AddDays((double)(num + 7 * (week - 2)));
			}
			if (result.Month != month)
			{
				throw new ArgumentOutOfRangeException("week", "Week number is out of range for the month");
			}
			return result;
		}

		// Token: 0x06000C67 RID: 3175 RVA: 0x0002DCFC File Offset: 0x0002BEFC
		private static int GetWeekNumber(DateTime date)
		{
			DateTime dateTime = new DateTime(date.Year, date.Month, 1);
			int num = 7 + CultureInfo.CurrentCulture.DateTimeFormat.FirstDayOfWeek - dateTime.DayOfWeek;
			if (date.Day <= num)
			{
				return 1;
			}
			return 2 + (date.Day - num - 1) / 7;
		}

		// Token: 0x06000C68 RID: 3176 RVA: 0x0002DD54 File Offset: 0x0002BF54
		private static bool IsDiskFullException(IOException e)
		{
			return Marshal.GetHRForException(e) == -2147024784;
		}

		// Token: 0x06000C69 RID: 3177 RVA: 0x0002DD64 File Offset: 0x0002BF64
		private LogDirectory.LogFileId GetLogFileIdFromName(string filename)
		{
			Match match = this.matcher.Match(filename);
			if (!match.Success)
			{
				return null;
			}
			int hour = 0;
			int instance;
			DateTime date;
			string value2;
			try
			{
				int year = Convert.ToInt32(match.Groups["year"].Captures[0].Value, CultureInfo.InvariantCulture);
				int month = Convert.ToInt32(match.Groups["month"].Captures[0].Value, CultureInfo.InvariantCulture);
				instance = Convert.ToInt32(match.Groups["instance"].Captures[0].Value, CultureInfo.InvariantCulture);
				if (this.maxLogFileSize == 0L)
				{
					switch (this.LogFileRollOver)
					{
					case LogFileRollOver.Hourly:
					{
						int day = Convert.ToInt32(match.Groups["day"].Captures[0].Value, CultureInfo.InvariantCulture);
						hour = Convert.ToInt32(match.Groups["hour"].Captures[0].Value, CultureInfo.InvariantCulture);
						date = new DateTime(year, month, day, hour, 0, 0);
						break;
					}
					case LogFileRollOver.Daily:
					{
						int day = Convert.ToInt32(match.Groups["day"].Captures[0].Value, CultureInfo.InvariantCulture);
						date = new DateTime(year, month, day);
						break;
					}
					case LogFileRollOver.Weekly:
					{
						int week = Convert.ToInt32(match.Groups["week"].Captures[0].Value, CultureInfo.InvariantCulture);
						date = LogDirectory.GetFirstDateForWeek(year, month, week);
						break;
					}
					case LogFileRollOver.Monthly:
						date = new DateTime(year, month, 1);
						break;
					default:
						throw new InvalidOperationException("The code should never be hit.");
					}
				}
				else
				{
					int day = Convert.ToInt32(match.Groups["day"].Captures[0].Value, CultureInfo.InvariantCulture);
					if (this.enforceAccurateAge)
					{
						string value = match.Groups["hour"].Captures[0].Value;
						if (!string.IsNullOrEmpty(value))
						{
							hour = Convert.ToInt32(value, CultureInfo.InvariantCulture);
						}
					}
					date = (this.enforceAccurateAge ? new DateTime(year, month, day, hour, 0, 0) : new DateTime(year, month, day));
				}
				value2 = match.Groups["note"].Captures[0].Value;
			}
			catch (ArgumentOutOfRangeException)
			{
				return null;
			}
			catch (FormatException)
			{
				return null;
			}
			catch (OverflowException)
			{
				return null;
			}
			return new LogDirectory.LogFileId(date, value2, instance);
		}

		// Token: 0x06000C6A RID: 3178 RVA: 0x0002E05C File Offset: 0x0002C25C
		private SortedList<LogDirectory.LogFileId, FileInfo> GetLogFileList()
		{
			SortedList<LogDirectory.LogFileId, FileInfo> sortedList = new SortedList<LogDirectory.LogFileId, FileInfo>();
			FileInfo[] files = this.directory.GetFiles(this.dirTemplate);
			for (int i = 0; i < files.Length; i++)
			{
				LogDirectory.LogFileId logFileIdFromName = this.GetLogFileIdFromName(files[i].Name);
				if (logFileIdFromName != null)
				{
					sortedList[logFileIdFromName] = files[i];
				}
			}
			return sortedList;
		}

		// Token: 0x06000C6B RID: 3179 RVA: 0x0002E0AC File Offset: 0x0002C2AC
		private int EnforceDirectorySizeQuota(SortedList<LogDirectory.LogFileId, FileInfo> logFiles)
		{
			long num = 0L;
			int i = logFiles.Count - 1;
			if (this.maxDirectorySize == 0L)
			{
				return 0;
			}
			while (i >= 0)
			{
				long length = logFiles.Values[i].Length;
				if (num + length >= this.maxDirectorySize)
				{
					break;
				}
				num += length;
				i--;
			}
			int result = i + 1;
			while (i >= 0)
			{
				FileInfo fileInfo = logFiles.Values[i];
				try
				{
					fileInfo.Delete();
				}
				catch (IOException)
				{
				}
				i--;
			}
			return result;
		}

		// Token: 0x06000C6C RID: 3180 RVA: 0x0002E134 File Offset: 0x0002C334
		private void DeleteOldLogFiles(SortedList<LogDirectory.LogFileId, FileInfo> logFiles, int start, DateTime earliestToKeep)
		{
			int num = 0;
			int num2 = start;
			while (num2 < logFiles.Count && logFiles.Keys[num2].Date < earliestToKeep)
			{
				try
				{
					logFiles.Values[num2].Delete();
					num++;
				}
				catch (IOException)
				{
				}
				num2++;
			}
			if (num > 0)
			{
				Log.EventLog.LogEvent(CommonEventLogConstants.Tuple_DeleteOldLog, null, new object[]
				{
					this.logComponent,
					this.FullName,
					earliestToKeep
				});
			}
		}

		// Token: 0x06000C6D RID: 3181 RVA: 0x0002E1D0 File Offset: 0x0002C3D0
		private string GetLogFileNameFromId(LogDirectory.LogFileId id)
		{
			int weekNumber = LogDirectory.GetWeekNumber(id.Date);
			return Path.Combine(this.FullName, string.Format(CultureInfo.InvariantCulture, this.production, new object[]
			{
				this.prefix,
				id.Date,
				id.Instance,
				this.note,
				".LOG",
				weekNumber
			}));
		}

		// Token: 0x06000C6E RID: 3182 RVA: 0x0002E24C File Offset: 0x0002C44C
		private LogDirectory.LogFileId GenerateLogFileId(SortedList<LogDirectory.LogFileId, FileInfo> logFiles, DateTime date, bool startNewLog, out bool newLogStarted)
		{
			int i = logFiles.Count - 1;
			bool flag = false;
			LogDirectory.LogFileId logFileId = null;
			newLogStarted = true;
			while (i >= 0)
			{
				logFileId = logFiles.Keys[i];
				if (logFileId.SameLogSeries(date, this))
				{
					flag = true;
					break;
				}
				i--;
			}
			if (!flag)
			{
				return new LogDirectory.LogFileId(date, this.note, 1);
			}
			if (this.maxLogFileSize > 0L && logFiles.Values[i].Length >= this.maxLogFileSize)
			{
				startNewLog = true;
			}
			newLogStarted = startNewLog;
			if (!startNewLog)
			{
				return logFileId;
			}
			return logFileId.Next;
		}

		// Token: 0x06000C6F RID: 3183 RVA: 0x0002E2D8 File Offset: 0x0002C4D8
		private void FlushStream(object state)
		{
			Stream stream = this.logFile;
			if (stream != null)
			{
				try
				{
					stream.Flush();
				}
				catch (IOException ex)
				{
					if (!LogDirectory.IsDiskFullException(ex))
					{
						Log.EventLog.LogEvent(CommonEventLogConstants.Tuple_FailedToAppendLog, null, new object[]
						{
							this.logComponent,
							ex.Message
						});
					}
				}
			}
		}

		// Token: 0x0400093B RID: 2363
		private const int MaxRecreateAttempts = 20;

		// Token: 0x0400093C RID: 2364
		private const string Extension = ".LOG";

		// Token: 0x0400093D RID: 2365
		private readonly int bufferLength;

		// Token: 0x0400093E RID: 2366
		private string production;

		// Token: 0x0400093F RID: 2367
		private DirectoryInfo directory;

		// Token: 0x04000940 RID: 2368
		private string prefix;

		// Token: 0x04000941 RID: 2369
		private string dirTemplate;

		// Token: 0x04000942 RID: 2370
		private TimeSpan maxAge;

		// Token: 0x04000943 RID: 2371
		private LogFileRollOver logFileRollOver;

		// Token: 0x04000944 RID: 2372
		private long maxLogFileSize;

		// Token: 0x04000945 RID: 2373
		private long maxDirectorySize;

		// Token: 0x04000946 RID: 2374
		private Stream logFile;

		// Token: 0x04000947 RID: 2375
		private LogDirectory.LogFileId logFileId;

		// Token: 0x04000948 RID: 2376
		private Regex matcher;

		// Token: 0x04000949 RID: 2377
		private string logComponent;

		// Token: 0x0400094A RID: 2378
		private bool enforceAccurateAge;

		// Token: 0x0400094B RID: 2379
		private string note;

		// Token: 0x0400094C RID: 2380
		private TimeSpan streamFlushInterval;

		// Token: 0x0400094D RID: 2381
		private Timer streamFlushTimer;

		// Token: 0x0400094E RID: 2382
		private bool flushToDisk;

		// Token: 0x0400094F RID: 2383
		private object fileRollOverLock = new object();

		// Token: 0x020001BD RID: 445
		// (Invoke) Token: 0x06000C71 RID: 3185
		public delegate void OnDirSizeQuotaExceededHandler(string component, string directory, long maxDirectorySize, int trimmed);

		// Token: 0x020001BE RID: 446
		internal class BufferedStream : Stream
		{
			// Token: 0x06000C74 RID: 3188 RVA: 0x0002E340 File Offset: 0x0002C540
			public BufferedStream(string filePath, int bufferLength) : this(filePath, bufferLength, false)
			{
			}

			// Token: 0x06000C75 RID: 3189 RVA: 0x0002E34C File Offset: 0x0002C54C
			public BufferedStream(string filePath, int bufferLength, bool flushToDisk)
			{
				this.wrappedStream = File.Open(filePath, FileMode.Append, FileAccess.Write, FileShare.Read);
				this.bufferSize = bufferLength;
				this.buffer = new byte[(bufferLength != 0) ? bufferLength : 4096];
				this.flushToDisk = flushToDisk;
			}

			// Token: 0x17000289 RID: 649
			// (get) Token: 0x06000C76 RID: 3190 RVA: 0x0002E39D File Offset: 0x0002C59D
			public int BufferSize
			{
				get
				{
					return this.bufferSize;
				}
			}

			// Token: 0x1700028A RID: 650
			// (get) Token: 0x06000C77 RID: 3191 RVA: 0x0002E3A5 File Offset: 0x0002C5A5
			// (set) Token: 0x06000C78 RID: 3192 RVA: 0x0002E3BA File Offset: 0x0002C5BA
			public override long Position
			{
				get
				{
					return this.wrappedStream.Position + (long)this.currPos;
				}
				set
				{
					throw new NotImplementedException();
				}
			}

			// Token: 0x1700028B RID: 651
			// (get) Token: 0x06000C79 RID: 3193 RVA: 0x0002E3C1 File Offset: 0x0002C5C1
			public override bool CanRead
			{
				get
				{
					return false;
				}
			}

			// Token: 0x1700028C RID: 652
			// (get) Token: 0x06000C7A RID: 3194 RVA: 0x0002E3C4 File Offset: 0x0002C5C4
			public override bool CanSeek
			{
				get
				{
					return false;
				}
			}

			// Token: 0x1700028D RID: 653
			// (get) Token: 0x06000C7B RID: 3195 RVA: 0x0002E3C7 File Offset: 0x0002C5C7
			public override bool CanWrite
			{
				get
				{
					return true;
				}
			}

			// Token: 0x1700028E RID: 654
			// (get) Token: 0x06000C7C RID: 3196 RVA: 0x0002E3CA File Offset: 0x0002C5CA
			public override long Length
			{
				get
				{
					throw new NotImplementedException();
				}
			}

			// Token: 0x06000C7D RID: 3197 RVA: 0x0002E3D4 File Offset: 0x0002C5D4
			public override void Write(byte[] bufferToWrite, int index, int count)
			{
				lock (this.syncObj)
				{
					if (this.currPos + count > this.buffer.Length)
					{
						this.Flush();
					}
					if (this.currPos + count <= this.buffer.Length)
					{
						Buffer.BlockCopy(bufferToWrite, index, this.buffer, this.currPos, count);
						this.currPos += count;
					}
					else
					{
						this.Flush();
						this.InternalWrite(bufferToWrite, index, count);
						this.wrappedStream.Flush();
					}
				}
			}

			// Token: 0x06000C7E RID: 3198 RVA: 0x0002E478 File Offset: 0x0002C678
			public override long Seek(long offset, SeekOrigin origin)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000C7F RID: 3199 RVA: 0x0002E47F File Offset: 0x0002C67F
			public override void SetLength(long value)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000C80 RID: 3200 RVA: 0x0002E486 File Offset: 0x0002C686
			public override int Read(byte[] buffer, int offset, int count)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000C81 RID: 3201 RVA: 0x0002E490 File Offset: 0x0002C690
			public override void Flush()
			{
				lock (this.syncObj)
				{
					if (this.currPos != 0)
					{
						this.wrappedStream.Write(this.buffer, 0, this.currPos);
						this.wrappedStream.Flush();
						if (this.flushToDisk)
						{
							DiagnosticsNativeMethods.FlushFileBuffers(((FileStream)this.wrappedStream).SafeFileHandle);
						}
						this.currPos = 0;
					}
				}
			}

			// Token: 0x06000C82 RID: 3202 RVA: 0x0002E51C File Offset: 0x0002C71C
			public override void Close()
			{
				lock (this.syncObj)
				{
					this.Flush();
					this.wrappedStream.Flush();
					this.wrappedStream.Dispose();
					this.Dispose(true);
				}
			}

			// Token: 0x06000C83 RID: 3203 RVA: 0x0002E57C File Offset: 0x0002C77C
			protected override void Dispose(bool disposing)
			{
				base.Dispose(disposing);
			}

			// Token: 0x06000C84 RID: 3204 RVA: 0x0002E585 File Offset: 0x0002C785
			private void InternalWrite(byte[] bufferToWrite, int index, int count)
			{
				this.wrappedStream.Write(bufferToWrite, index, count);
			}

			// Token: 0x04000951 RID: 2385
			private const int DefaultBufferSize = 4096;

			// Token: 0x04000952 RID: 2386
			private object syncObj = new object();

			// Token: 0x04000953 RID: 2387
			private byte[] buffer;

			// Token: 0x04000954 RID: 2388
			private int bufferSize;

			// Token: 0x04000955 RID: 2389
			private int currPos;

			// Token: 0x04000956 RID: 2390
			private Stream wrappedStream;

			// Token: 0x04000957 RID: 2391
			private bool flushToDisk;
		}

		// Token: 0x020001BF RID: 447
		private class LogFileId : IComparable
		{
			// Token: 0x06000C85 RID: 3205 RVA: 0x0002E595 File Offset: 0x0002C795
			public LogFileId(DateTime date, string note, int instance)
			{
				this.date = new DateTime(date.Year, date.Month, date.Day, date.Hour, 0, 0);
				this.note = note;
				this.instance = instance;
			}

			// Token: 0x1700028F RID: 655
			// (get) Token: 0x06000C86 RID: 3206 RVA: 0x0002E5D4 File Offset: 0x0002C7D4
			public LogDirectory.LogFileId Next
			{
				get
				{
					return new LogDirectory.LogFileId(this.date, this.note, this.instance + 1);
				}
			}

			// Token: 0x17000290 RID: 656
			// (get) Token: 0x06000C87 RID: 3207 RVA: 0x0002E5EF File Offset: 0x0002C7EF
			public DateTime Date
			{
				get
				{
					return this.date;
				}
			}

			// Token: 0x17000291 RID: 657
			// (get) Token: 0x06000C88 RID: 3208 RVA: 0x0002E5F7 File Offset: 0x0002C7F7
			public int Instance
			{
				get
				{
					return this.instance;
				}
			}

			// Token: 0x17000292 RID: 658
			// (get) Token: 0x06000C89 RID: 3209 RVA: 0x0002E5FF File Offset: 0x0002C7FF
			public string Note
			{
				get
				{
					return this.note;
				}
			}

			// Token: 0x06000C8A RID: 3210 RVA: 0x0002E608 File Offset: 0x0002C808
			int IComparable.CompareTo(object o)
			{
				LogDirectory.LogFileId logFileId = o as LogDirectory.LogFileId;
				if (logFileId == null)
				{
					throw new ArgumentException("object is not a LogFileId");
				}
				if (this.date < logFileId.date)
				{
					return -1;
				}
				if (logFileId.date < this.date)
				{
					return 1;
				}
				if (this.instance != logFileId.instance)
				{
					return this.instance - logFileId.instance;
				}
				return this.note.CompareTo(logFileId.note);
			}

			// Token: 0x06000C8B RID: 3211 RVA: 0x0002E684 File Offset: 0x0002C884
			public bool SameLogSeries(DateTime date, LogDirectory logDirectory)
			{
				if (!this.note.Equals(logDirectory.Note, StringComparison.OrdinalIgnoreCase))
				{
					return false;
				}
				if (logDirectory.MaxLogFileSize != 0L)
				{
					return logDirectory.MaxLogFileSize == long.MaxValue || ((!logDirectory.EnforceAccurateAge || this.Date.Hour == date.Hour) && (this.Date.Year == date.Year && this.Date.Month == date.Month) && this.Date.Day == date.Day);
				}
				switch (logDirectory.LogFileRollOver)
				{
				case LogFileRollOver.Hourly:
					return this.Date.Year == date.Year && this.Date.Month == date.Month && this.Date.Day == date.Day && this.Date.Hour == date.Hour;
				case LogFileRollOver.Daily:
					return this.Date.Year == date.Year && this.Date.Month == date.Month && this.Date.Day == date.Day;
				case LogFileRollOver.Weekly:
					return this.Date.Year == date.Year && this.Date.Month == date.Month && LogDirectory.GetWeekNumber(this.Date) == LogDirectory.GetWeekNumber(date);
				case LogFileRollOver.Monthly:
					return this.Date.Year == date.Year && this.Date.Month == date.Month;
				default:
					throw new InvalidOperationException("The code should never be hit.");
				}
			}

			// Token: 0x04000958 RID: 2392
			private DateTime date;

			// Token: 0x04000959 RID: 2393
			private int instance;

			// Token: 0x0400095A RID: 2394
			private string note;
		}
	}
}
