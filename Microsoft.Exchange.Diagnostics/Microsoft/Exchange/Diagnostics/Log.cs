using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using System.Security.AccessControl;
using System.Security.Principal;
using System.Threading;
using Microsoft.Exchange.Diagnostics.Components.Common;

namespace Microsoft.Exchange.Diagnostics
{
	// Token: 0x020001BB RID: 443
	public class Log
	{
		// Token: 0x06000C32 RID: 3122 RVA: 0x0002C955 File Offset: 0x0002AB55
		public Log(string fileNamePrefix, LogHeaderFormatter headerFormatter, string logComponent) : this(fileNamePrefix, headerFormatter, logComponent, true)
		{
		}

		// Token: 0x06000C33 RID: 3123 RVA: 0x0002C961 File Offset: 0x0002AB61
		public Log(string fileNamePrefix, LogHeaderFormatter headerFormatter, string logComponent, bool handleKnownExceptions)
		{
			this.headerFormatter = headerFormatter;
			this.fileNamePrefix = fileNamePrefix;
			this.logComponent = logComponent;
			this.handleKnownExceptions = handleKnownExceptions;
			this.maxWaitAppendCount = 50;
		}

		// Token: 0x17000281 RID: 641
		// (get) Token: 0x06000C34 RID: 3124 RVA: 0x0002C999 File Offset: 0x0002AB99
		public static ExEventLog EventLog
		{
			get
			{
				return Log.eventLogger;
			}
		}

		// Token: 0x17000282 RID: 642
		// (get) Token: 0x06000C35 RID: 3125 RVA: 0x0002C9A0 File Offset: 0x0002ABA0
		// (set) Token: 0x06000C36 RID: 3126 RVA: 0x0002C9A8 File Offset: 0x0002ABA8
		public bool TestHelper_ForceLogFileRollOver { get; set; }

		// Token: 0x17000283 RID: 643
		// (get) Token: 0x06000C37 RID: 3127 RVA: 0x0002C9B1 File Offset: 0x0002ABB1
		internal LogDirectory LogDirectory
		{
			get
			{
				return this.logDirectory;
			}
		}

		// Token: 0x06000C38 RID: 3128 RVA: 0x0002C9B9 File Offset: 0x0002ABB9
		public static DirectoryInfo CreateLogDirectory(string path)
		{
			return Log.CreateLogDirectory(new DirectoryInfo(path));
		}

		// Token: 0x06000C39 RID: 3129 RVA: 0x0002C9C8 File Offset: 0x0002ABC8
		public void Flush()
		{
			try
			{
				if (this.logDirectory != null)
				{
					this.logDirectory.Flush();
				}
			}
			catch (IOException ex)
			{
				ExTraceGlobals.CommonTracer.TraceError<LogDirectory, string>(30569, (long)this.GetHashCode(), "Failed to Flush the LogDirectory {0}. Error {1}", this.logDirectory, ex.Message);
				this.logDirectory.Close();
				this.logDirectory = null;
			}
		}

		// Token: 0x06000C3A RID: 3130 RVA: 0x0002CA38 File Offset: 0x0002AC38
		public void Close()
		{
			try
			{
				if (this.logDirectory != null)
				{
					this.logDirectory.Close();
				}
			}
			catch (IOException ex)
			{
				ExTraceGlobals.CommonTracer.TraceError<LogDirectory, string>(30569, (long)this.GetHashCode(), "Failed to Close the LogDirectory {0}. Error {1}", this.logDirectory, ex.Message);
			}
			finally
			{
				this.logDirectory = null;
			}
		}

		// Token: 0x06000C3B RID: 3131 RVA: 0x0002CAAC File Offset: 0x0002ACAC
		public void Configure(string path, TimeSpan maxAge, long maxDirectorySize, long maxLogFileSize)
		{
			this.Configure(path, maxAge, maxDirectorySize, maxLogFileSize, 0, TimeSpan.MaxValue);
		}

		// Token: 0x06000C3C RID: 3132 RVA: 0x0002CAC0 File Offset: 0x0002ACC0
		public void Configure(string path, TimeSpan maxAge, long maxDirectorySize, long maxLogFileSize, int bufferSize, TimeSpan streamFlushInterval)
		{
			this.Configure(path, maxAge, LogFileRollOver.Daily, maxDirectorySize, maxLogFileSize, false, bufferSize, streamFlushInterval);
		}

		// Token: 0x06000C3D RID: 3133 RVA: 0x0002CAE0 File Offset: 0x0002ACE0
		public void Configure(string path, TimeSpan maxAge, long maxDirectorySize, long maxLogFileSize, int bufferSize, TimeSpan streamFlushInterval, string note)
		{
			this.Configure(path, maxAge, LogFileRollOver.Daily, maxDirectorySize, maxLogFileSize, false, bufferSize, streamFlushInterval, note, false);
		}

		// Token: 0x06000C3E RID: 3134 RVA: 0x0002CB04 File Offset: 0x0002AD04
		public void Configure(string path, TimeSpan maxAge, long maxDirectorySize, long maxLogFileSize, int bufferSize, TimeSpan streamFlushInterval, string note, bool flushToDisk)
		{
			this.Configure(path, maxAge, LogFileRollOver.Daily, maxDirectorySize, maxLogFileSize, false, bufferSize, streamFlushInterval, note, flushToDisk);
		}

		// Token: 0x06000C3F RID: 3135 RVA: 0x0002CB28 File Offset: 0x0002AD28
		public void Configure(string path, TimeSpan maxAge, long maxDirectorySize, long maxLogFileSize, int bufferSize, TimeSpan streamFlushInterval, bool flushToDisk)
		{
			this.Configure(path, maxAge, LogFileRollOver.Daily, maxDirectorySize, maxLogFileSize, false, bufferSize, streamFlushInterval, flushToDisk);
		}

		// Token: 0x06000C40 RID: 3136 RVA: 0x0002CB48 File Offset: 0x0002AD48
		public void Configure(string path, LogFileRollOver logFileRollOver)
		{
			this.Configure(path, logFileRollOver, 0, TimeSpan.MaxValue);
		}

		// Token: 0x06000C41 RID: 3137 RVA: 0x0002CB58 File Offset: 0x0002AD58
		public void Configure(string path, LogFileRollOver logFileRollOver, int bufferSize, TimeSpan streamFlushInterval)
		{
			this.Configure(path, TimeSpan.MaxValue, logFileRollOver, 0L, 0L, false, bufferSize, streamFlushInterval);
		}

		// Token: 0x06000C42 RID: 3138 RVA: 0x0002CB7A File Offset: 0x0002AD7A
		public void Configure(string path, TimeSpan maxAge, long maxDirectorySize, long maxLogFileSize, bool enforceAccurateAge)
		{
			this.Configure(path, maxAge, maxDirectorySize, maxLogFileSize, enforceAccurateAge, 0, TimeSpan.MaxValue);
		}

		// Token: 0x06000C43 RID: 3139 RVA: 0x0002CB90 File Offset: 0x0002AD90
		public void Configure(string path, LogFileRollOver logFileRollOver, TimeSpan maxAge)
		{
			this.Configure(path, maxAge, logFileRollOver, 0L, 0L, false, 0, TimeSpan.MaxValue);
		}

		// Token: 0x06000C44 RID: 3140 RVA: 0x0002CBB4 File Offset: 0x0002ADB4
		public void Configure(string path, TimeSpan maxAge, long maxDirectorySize, long maxLogFileSize, bool enforceAccurateAge, int bufferSize, TimeSpan streamFlushInterval)
		{
			this.Configure(path, maxAge, LogFileRollOver.Daily, maxDirectorySize, maxLogFileSize, enforceAccurateAge, bufferSize, streamFlushInterval);
		}

		// Token: 0x06000C45 RID: 3141 RVA: 0x0002CBD4 File Offset: 0x0002ADD4
		public void Configure(string path, TimeSpan maxAge, long maxDirectorySize, long maxLogFileSize, bool enforceAccurateAge, int bufferSize, TimeSpan streamFlushInterval, LogFileRollOver logFileRollOver)
		{
			this.Configure(path, maxAge, logFileRollOver, maxDirectorySize, maxLogFileSize, enforceAccurateAge, bufferSize, streamFlushInterval);
		}

		// Token: 0x06000C46 RID: 3142 RVA: 0x0002CBF4 File Offset: 0x0002ADF4
		public void Append(IEnumerable<LogRowFormatter> rows, int timestampField)
		{
			lock (this.logLock)
			{
				foreach (LogRowFormatter row in rows)
				{
					this.Append(row, timestampField);
				}
			}
		}

		// Token: 0x06000C47 RID: 3143 RVA: 0x0002CC68 File Offset: 0x0002AE68
		public void Append(LogRowFormatter row, int timestampField)
		{
			this.Append(row, timestampField, DateTime.MinValue);
		}

		// Token: 0x06000C48 RID: 3144 RVA: 0x0002CC78 File Offset: 0x0002AE78
		public void Append(LogRowFormatter row, int timestampField, DateTime timeStamp)
		{
			Exception ex = null;
			string text = null;
			lock (this.logLock)
			{
				if (this.failedToCreateDirectory)
				{
					ExTraceGlobals.CommonTracer.TraceError<string>(26473, (long)this.GetHashCode(), "Cannot append to the {0} logs because we failed to create the logging directory", this.logComponent);
					if (!this.handleKnownExceptions)
					{
						throw new InvalidOperationException("Not configured");
					}
					return;
				}
				else
				{
					if (this.handleKnownExceptions && this.donotAppend && ++this.donotAppendCount < this.maxWaitAppendCount)
					{
						ExTraceGlobals.CommonTracer.TraceError<string, int>(22377, (long)this.GetHashCode(), "Not appending to the {0} logs. Failed to log in previous attempts. Will wait {1} times before retrying", this.logComponent, this.maxWaitAppendCount - this.donotAppendCount);
						return;
					}
					if (this.logDirectory == null)
					{
						throw new InvalidOperationException("Cannot append to a closed log.");
					}
					this.donotAppend = false;
					DateTime utcNow = DateTime.UtcNow;
					if (timestampField >= 0)
					{
						row[timestampField] = ((timeStamp != DateTime.MinValue) ? timeStamp : utcNow);
					}
					try
					{
						Stream logFile = this.logDirectory.GetLogFile(utcNow, this.headerFormatter, this.TestHelper_ForceLogFileRollOver);
						if (logFile == null)
						{
							this.donotAppend = true;
							this.donotAppendCount = 1;
							text = "Couldn't create a new log file";
						}
						else
						{
							row.Write(logFile);
							LogDirectory.BufferedStream bufferedStream = logFile as LogDirectory.BufferedStream;
							if (bufferedStream != null && bufferedStream.BufferSize == 0)
							{
								bufferedStream.Flush();
							}
						}
					}
					catch (IOException ex2)
					{
						ex = ex2;
						this.donotAppend = true;
						this.donotAppendCount = 1;
						text = ex2.Message;
					}
					catch (UnauthorizedAccessException ex3)
					{
						ex = ex3;
						this.donotAppend = true;
						this.donotAppendCount = 1;
						text = ex3.Message;
					}
				}
			}
			if (this.donotAppend)
			{
				Log.EventLog.LogEvent(CommonEventLogConstants.Tuple_FailedToAppendLog, this.logDirectory.FullName, new object[]
				{
					this.logComponent,
					text
				});
			}
			if (!this.handleKnownExceptions && ex != null)
			{
				throw new LogException("log append failed", ex);
			}
		}

		// Token: 0x06000C49 RID: 3145 RVA: 0x0002CEBC File Offset: 0x0002B0BC
		private static DirectorySecurity GetDefaultDirectorySecurity()
		{
			if (Log.defaultDirectorySecurity == null)
			{
				DirectorySecurity directorySecurity = new DirectorySecurity();
				using (WindowsIdentity current = WindowsIdentity.GetCurrent())
				{
					directorySecurity.SetOwner(current.User);
				}
				for (int i = 0; i < Log.DirectoryAccessRules.Length; i++)
				{
					directorySecurity.AddAccessRule(Log.DirectoryAccessRules[i]);
				}
				Interlocked.CompareExchange<DirectorySecurity>(ref Log.defaultDirectorySecurity, directorySecurity, null);
			}
			return Log.defaultDirectorySecurity;
		}

		// Token: 0x06000C4A RID: 3146 RVA: 0x0002CF38 File Offset: 0x0002B138
		private static DirectoryInfo CreateLogDirectory(DirectoryInfo directory)
		{
			if (!directory.Exists)
			{
				if (directory.Parent != null)
				{
					Log.CreateLogDirectory(directory.Parent);
				}
				Log.InternalCreateLogDirectory(directory.FullName);
			}
			return directory;
		}

		// Token: 0x06000C4B RID: 3147 RVA: 0x0002CF64 File Offset: 0x0002B164
		private static DirectoryInfo InternalCreateLogDirectory(string path)
		{
			DirectoryInfo directoryInfo = Directory.CreateDirectory(path, Log.GetDefaultDirectorySecurity());
			DirectorySecurity accessControl = Directory.GetAccessControl(path);
			accessControl.SetAccessRuleProtection(false, true);
			directoryInfo.SetAccessControl(accessControl);
			return directoryInfo;
		}

		// Token: 0x06000C4C RID: 3148 RVA: 0x0002CF94 File Offset: 0x0002B194
		private void Configure(string path, TimeSpan maxAge, LogFileRollOver logFileRollOver, long maxDirectorySize, long maxLogFileSize, bool enforceAccurateAge, int bufferSize, TimeSpan streamFlushInterval)
		{
			this.Configure(path, maxAge, logFileRollOver, maxDirectorySize, maxLogFileSize, enforceAccurateAge, bufferSize, streamFlushInterval, string.Empty, false);
		}

		// Token: 0x06000C4D RID: 3149 RVA: 0x0002CFBC File Offset: 0x0002B1BC
		private void Configure(string path, TimeSpan maxAge, LogFileRollOver logFileRollOver, long maxDirectorySize, long maxLogFileSize, bool enforceAccurateAge, int bufferSize, TimeSpan streamFlushInterval, bool flushToDisk)
		{
			this.Configure(path, maxAge, logFileRollOver, maxDirectorySize, maxLogFileSize, enforceAccurateAge, bufferSize, streamFlushInterval, string.Empty, false);
		}

		// Token: 0x06000C4E RID: 3150 RVA: 0x0002CFE4 File Offset: 0x0002B1E4
		private void Configure(string path, TimeSpan maxAge, LogFileRollOver logFileRollOver, long maxDirectorySize, long maxLogFileSize, bool enforceAccurateAge, int bufferSize, TimeSpan streamFlushInterval, string note, bool flushToDisk)
		{
			if (path == this.path && maxAge == this.maxAge && logFileRollOver == this.logFileRollOver && maxDirectorySize == this.maxDirectorySize && maxLogFileSize == this.maxLogFileSize && enforceAccurateAge == this.enforceAccurateAge && bufferSize == this.bufferSize && streamFlushInterval == this.streamFlushInterval && flushToDisk == this.flushToDisk)
			{
				return;
			}
			Exception ex = null;
			string text = null;
			lock (this.logLock)
			{
				this.Close();
				try
				{
					text = Path.GetFullPath(path);
					if (maxLogFileSize == 0L)
					{
						this.logDirectory = new LogDirectory(text, this.fileNamePrefix, (maxAge == TimeSpan.Zero) ? TimeSpan.MaxValue : maxAge, logFileRollOver, this.logComponent, note, bufferSize, streamFlushInterval, flushToDisk);
					}
					else
					{
						this.logDirectory = new LogDirectory(text, this.fileNamePrefix, (maxAge == TimeSpan.Zero) ? TimeSpan.MaxValue : maxAge, maxLogFileSize, maxDirectorySize, this.logComponent, enforceAccurateAge, note, bufferSize, streamFlushInterval, flushToDisk);
					}
					this.failedToCreateDirectory = false;
					this.donotAppendCount = 0;
					this.donotAppend = false;
					this.path = path;
					this.maxAge = maxAge;
					this.logFileRollOver = logFileRollOver;
					this.maxDirectorySize = maxDirectorySize;
					this.maxLogFileSize = maxLogFileSize;
					this.enforceAccurateAge = enforceAccurateAge;
					this.bufferSize = bufferSize;
					this.streamFlushInterval = streamFlushInterval;
					this.flushToDisk = flushToDisk;
				}
				catch (DirectoryNotFoundException ex2)
				{
					ex = ex2;
					this.logDirectory = null;
					this.failedToCreateDirectory = true;
					Log.EventLog.LogEvent(CommonEventLogConstants.Tuple_FailedToCreateDirectory, null, new object[]
					{
						this.logComponent,
						text,
						ex2.Message
					});
				}
				catch (ArgumentException ex3)
				{
					ex = ex3;
					this.logDirectory = null;
					this.failedToCreateDirectory = true;
					Log.EventLog.LogEvent(CommonEventLogConstants.Tuple_FailedToCreateDirectory, null, new object[]
					{
						this.logComponent,
						text,
						ex3.Message
					});
				}
				catch (UnauthorizedAccessException ex4)
				{
					ex = ex4;
					this.logDirectory = null;
					this.failedToCreateDirectory = true;
					Log.EventLog.LogEvent(CommonEventLogConstants.Tuple_FailedToCreateDirectory, null, new object[]
					{
						this.logComponent,
						text,
						ex4.Message
					});
				}
				catch (IOException ex5)
				{
					ex = ex5;
					this.logDirectory = null;
					this.failedToCreateDirectory = true;
					Log.EventLog.LogEvent(CommonEventLogConstants.Tuple_FailedToCreateDirectory, null, new object[]
					{
						this.logComponent,
						text,
						ex5.Message
					});
				}
				catch (InvalidOperationException ex6)
				{
					if (Marshal.GetLastWin32Error() != 122)
					{
						throw;
					}
					ex = ex6;
					this.logDirectory = null;
					this.failedToCreateDirectory = true;
					Log.EventLog.LogEvent(CommonEventLogConstants.Tuple_FailedToCreateDirectory, null, new object[]
					{
						this.logComponent,
						text,
						ex6.Message
					});
				}
			}
			if (!this.handleKnownExceptions && ex != null)
			{
				throw new LogException("log config failed", ex);
			}
		}

		// Token: 0x04000924 RID: 2340
		private static readonly FileSystemAccessRule[] DirectoryAccessRules = new FileSystemAccessRule[]
		{
			new FileSystemAccessRule(new SecurityIdentifier(WellKnownSidType.BuiltinAdministratorsSid, null), FileSystemRights.FullControl, InheritanceFlags.ContainerInherit | InheritanceFlags.ObjectInherit, PropagationFlags.None, AccessControlType.Allow),
			new FileSystemAccessRule(new SecurityIdentifier(WellKnownSidType.LocalSystemSid, null), FileSystemRights.FullControl, InheritanceFlags.ContainerInherit | InheritanceFlags.ObjectInherit, PropagationFlags.None, AccessControlType.Allow),
			new FileSystemAccessRule(new SecurityIdentifier(WellKnownSidType.NetworkServiceSid, null), FileSystemRights.FullControl, InheritanceFlags.ContainerInherit | InheritanceFlags.ObjectInherit, PropagationFlags.None, AccessControlType.Allow)
		};

		// Token: 0x04000925 RID: 2341
		private static readonly ExEventLog eventLogger = new ExEventLog(ExTraceGlobals.CommonTracer.Category, "MSExchange Common");

		// Token: 0x04000926 RID: 2342
		private static DirectorySecurity defaultDirectorySecurity;

		// Token: 0x04000927 RID: 2343
		private readonly string fileNamePrefix;

		// Token: 0x04000928 RID: 2344
		private readonly LogHeaderFormatter headerFormatter;

		// Token: 0x04000929 RID: 2345
		private readonly int maxWaitAppendCount;

		// Token: 0x0400092A RID: 2346
		private readonly object logLock = new object();

		// Token: 0x0400092B RID: 2347
		private readonly string logComponent;

		// Token: 0x0400092C RID: 2348
		private readonly bool handleKnownExceptions;

		// Token: 0x0400092D RID: 2349
		private LogDirectory logDirectory;

		// Token: 0x0400092E RID: 2350
		private bool donotAppend;

		// Token: 0x0400092F RID: 2351
		private int donotAppendCount;

		// Token: 0x04000930 RID: 2352
		private bool failedToCreateDirectory;

		// Token: 0x04000931 RID: 2353
		private string path;

		// Token: 0x04000932 RID: 2354
		private TimeSpan maxAge;

		// Token: 0x04000933 RID: 2355
		private LogFileRollOver logFileRollOver;

		// Token: 0x04000934 RID: 2356
		private long maxDirectorySize;

		// Token: 0x04000935 RID: 2357
		private long maxLogFileSize;

		// Token: 0x04000936 RID: 2358
		private bool enforceAccurateAge;

		// Token: 0x04000937 RID: 2359
		private int bufferSize;

		// Token: 0x04000938 RID: 2360
		private TimeSpan streamFlushInterval;

		// Token: 0x04000939 RID: 2361
		private bool flushToDisk;
	}
}
