using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Security;
using System.Threading;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.LogSearch;
using Microsoft.Win32;

namespace Microsoft.Exchange.Transport.Logging.Search
{
	// Token: 0x0200001F RID: 31
	internal class LogFileInfo
	{
		// Token: 0x14000001 RID: 1
		// (add) Token: 0x06000064 RID: 100 RVA: 0x000042A8 File Offset: 0x000024A8
		// (remove) Token: 0x06000065 RID: 101 RVA: 0x000042DC File Offset: 0x000024DC
		public static event EventHandler LogFileOpened;

		// Token: 0x14000002 RID: 2
		// (add) Token: 0x06000066 RID: 102 RVA: 0x00004310 File Offset: 0x00002510
		// (remove) Token: 0x06000067 RID: 103 RVA: 0x00004348 File Offset: 0x00002548
		public event ProcessRowEventHandler ProcessRow;

		// Token: 0x14000003 RID: 3
		// (add) Token: 0x06000068 RID: 104 RVA: 0x00004380 File Offset: 0x00002580
		// (remove) Token: 0x06000069 RID: 105 RVA: 0x000043B8 File Offset: 0x000025B8
		public event EventHandler LogFileClosed;

		// Token: 0x14000004 RID: 4
		// (add) Token: 0x0600006A RID: 106 RVA: 0x000043F0 File Offset: 0x000025F0
		// (remove) Token: 0x0600006B RID: 107 RVA: 0x00004428 File Offset: 0x00002628
		public event EventHandler LogFileDeleted;

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x0600006C RID: 108 RVA: 0x0000445D File Offset: 0x0000265D
		public string FullName
		{
			get
			{
				return this.fullName;
			}
		}

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x0600006D RID: 109 RVA: 0x00004465 File Offset: 0x00002665
		public long Length
		{
			get
			{
				return this.length;
			}
		}

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x0600006E RID: 110 RVA: 0x0000446D File Offset: 0x0000266D
		public DateTime StartTime
		{
			get
			{
				return this.startTime;
			}
		}

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x0600006F RID: 111 RVA: 0x00004475 File Offset: 0x00002675
		public string Prefix
		{
			get
			{
				return this.prefix;
			}
		}

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x06000070 RID: 112 RVA: 0x0000447D File Offset: 0x0000267D
		public bool NeedsTimeCheck
		{
			get
			{
				return this.needsTimeCheck;
			}
		}

		// Token: 0x06000071 RID: 113 RVA: 0x00004485 File Offset: 0x00002685
		public LogFileInfo(string fullName)
		{
			this.fullName = fullName.ToLower();
		}

		// Token: 0x06000072 RID: 114 RVA: 0x000044AF File Offset: 0x000026AF
		public LogFileInfo(DateTime startTime)
		{
			this.startTime = startTime;
		}

		// Token: 0x06000073 RID: 115 RVA: 0x000044D4 File Offset: 0x000026D4
		public LogFileInfo(string fullName, long length, DateTime startTime, CsvTable csvTable, bool isActive)
		{
			ExTraceGlobals.ServiceTracer.TraceDebug<string, long, string>((long)this.GetHashCode(), "MsExchangeLogSearch constructs LogFileInfo with FilePath {0}, FileLength {1}, FileCreateTime {2}", fullName, length, startTime.ToString());
			this.fullName = fullName.ToLower();
			this.length = length;
			this.startTime = startTime;
			this.prefix = LogMonitor.GetFullPrefix(this.fullName);
			this.csvTable = csvTable;
			this.isActive = isActive;
			this.needsTimeCheck = true;
			if (LogFileInfo.LogFileOpened != null)
			{
				LogFileInfo.LogFileOpened(new LogFile(this), null);
			}
		}

		// Token: 0x06000074 RID: 116 RVA: 0x0000457C File Offset: 0x0000277C
		public IEnumerator<uint> GetOffsetsForData(string columnName, string searchData)
		{
			List<uint> list = null;
			try
			{
				this.fileProcessingLock.EnterReadLock();
				list = this.indexes[columnName].GetOffsetsForData(searchData);
			}
			finally
			{
				this.fileProcessingLock.ExitReadLock();
			}
			return list.GetEnumerator();
		}

		// Token: 0x06000075 RID: 117 RVA: 0x000045D4 File Offset: 0x000027D4
		public void MarkInactiveIfNeeded(bool isCurrentlyActive)
		{
			try
			{
				this.fileProcessingLock.EnterUpgradeableReadLock();
				if (this.isActive && !isCurrentlyActive)
				{
					this.ContinueProcessingActiveFile();
					try
					{
						this.fileProcessingLock.EnterWriteLock();
						if (this.LogFileClosed != null)
						{
							this.MarkAsProcessed();
							this.LogFileClosed(this, null);
						}
						this.isActive = false;
					}
					finally
					{
						this.fileProcessingLock.ExitWriteLock();
					}
				}
			}
			finally
			{
				this.fileProcessingLock.ExitUpgradeableReadLock();
			}
		}

		// Token: 0x06000076 RID: 118 RVA: 0x00004664 File Offset: 0x00002864
		public bool TryProcessFile()
		{
			this.RegisterIndexingComponent();
			bool flag;
			if (!this.TryOpenAndProcessLogFile(out flag))
			{
				return false;
			}
			if (!this.isActive && this.LogFileClosed != null)
			{
				if (!flag)
				{
					this.MarkAsProcessed();
				}
				this.LogFileClosed(this, null);
			}
			return true;
		}

		// Token: 0x06000077 RID: 119 RVA: 0x000046AC File Offset: 0x000028AC
		public void ContinueProcessingActiveFile()
		{
			try
			{
				this.fileProcessingLock.EnterWriteLock();
				bool flag;
				if (!this.TryOpenAndProcessLogFile(out flag))
				{
					ExTraceGlobals.ServiceTracer.TraceError<string>((long)this.GetHashCode(), "Could not process logfile: {0}", this.fullName);
				}
			}
			finally
			{
				this.fileProcessingLock.ExitWriteLock();
			}
		}

		// Token: 0x06000078 RID: 120 RVA: 0x0000470C File Offset: 0x0000290C
		public void ProcessFileDeletion()
		{
			if (this.LogFileDeleted != null)
			{
				this.LogFileDeleted(this, null);
			}
		}

		// Token: 0x06000079 RID: 121 RVA: 0x00004724 File Offset: 0x00002924
		private static RegistryKey OpenSubKey(RegistryKey key, string subKeyName, bool createIfMissing)
		{
			RegistryKey registryKey = key.OpenSubKey(subKeyName, createIfMissing);
			if (registryKey != null)
			{
				return registryKey;
			}
			if (createIfMissing)
			{
				ExTraceGlobals.ServiceTracer.TraceDebug<RegistryKey, string>(0L, "Creating subkey: {0}\\{1}", key, subKeyName);
				return key.CreateSubKey(subKeyName);
			}
			ExTraceGlobals.ServiceTracer.TraceDebug<RegistryKey, string>(0L, "Cannot open {0}\\{1}", key, subKeyName);
			return null;
		}

		// Token: 0x0600007A RID: 122 RVA: 0x00004771 File Offset: 0x00002971
		private bool CheckValidity()
		{
			return true;
		}

		// Token: 0x0600007B RID: 123 RVA: 0x00004774 File Offset: 0x00002974
		private bool TryOpenAndProcessLogFile(out bool isAlreadyProcessed)
		{
			isAlreadyProcessed = false;
			FileStream fileStream = null;
			bool result;
			try
			{
				object obj;
				if (!this.TryCheckAlreadyProcessed(out isAlreadyProcessed))
				{
					result = false;
				}
				else if (isAlreadyProcessed)
				{
					result = true;
				}
				else if (!IOHelper.TryIOOperation(new IOHelper.FileIOOperation(this.OpenLogFile), out obj))
				{
					result = false;
				}
				else if (obj == null)
				{
					result = true;
				}
				else
				{
					CsvFieldCache cursor = ((LogFileInfo.OpenLogFileResult)obj).Cursor;
					fileStream = ((LogFileInfo.OpenLogFileResult)obj).FileStream;
					if (this.isActive && this.activeOffset != 0L)
					{
						cursor.Seek(this.activeOffset);
					}
					long position = cursor.Position;
					while (cursor.MoveNext(false))
					{
						if (this.ProcessRow != null)
						{
							ReadOnlyRow readOnlyRow = new ReadOnlyRow(cursor, position);
							ProcessRowEventArgs args = new ProcessRowEventArgs(readOnlyRow);
							this.ProcessRow(this, args);
						}
						position = cursor.Position;
					}
					this.activeOffset = cursor.Position;
					this.length = cursor.Length;
					result = true;
				}
			}
			finally
			{
				if (fileStream != null)
				{
					fileStream.Close();
				}
			}
			return result;
		}

		// Token: 0x0600007C RID: 124 RVA: 0x00004880 File Offset: 0x00002A80
		private LogFileInfo.OpenLogFileResult OpenLogFile()
		{
			FileStream fileStream = CsvFieldCache.OpenLogFile(this.fullName);
			if (fileStream == null)
			{
				return null;
			}
			CsvFieldCache cursor = new CsvFieldCache(this.csvTable, fileStream, 32768);
			return new LogFileInfo.OpenLogFileResult(cursor, fileStream);
		}

		// Token: 0x0600007D RID: 125 RVA: 0x000048B8 File Offset: 0x00002AB8
		private void RegisterIndexingComponent()
		{
			CsvField[] indexedFields = this.csvTable.IndexedFields;
			foreach (CsvField csvField in indexedFields)
			{
				LogIndex logIndex = new LogIndex(this.fullName, this.csvTable, csvField, this.isActive);
				this.indexes.Add(csvField.Name, logIndex);
				if (!this.isActive)
				{
					logIndex.TryReadFromDisk();
				}
				this.RegisterHandlersForIndex(logIndex);
			}
		}

		// Token: 0x0600007E RID: 126 RVA: 0x0000492A File Offset: 0x00002B2A
		private void RegisterHandlersForIndex(LogIndex logIndex)
		{
			this.ProcessRow += logIndex.OnProcessRow;
			this.LogFileClosed += logIndex.OnLogFileClosed;
			this.LogFileDeleted += logIndex.OnLogFileDeleted;
		}

		// Token: 0x0600007F RID: 127 RVA: 0x00004964 File Offset: 0x00002B64
		private bool TryCheckAlreadyProcessed(out bool isProcessed)
		{
			Exception arg = null;
			isProcessed = false;
			try
			{
				ExTraceGlobals.ServiceTracer.TraceDebug<string>((long)this.GetHashCode(), "Checking if file is already processed: {0}", this.fullName);
				if (this.isActive)
				{
					ExTraceGlobals.ServiceTracer.TraceDebug((long)this.GetHashCode(), "File is active");
					return true;
				}
				using (RegistryKey registryKey = this.OpenWatermarkKey(false))
				{
					if (registryKey == null)
					{
						ExTraceGlobals.ServiceTracer.TraceDebug((long)this.GetHashCode(), "No watermark regkey yet");
						return true;
					}
					string name = this.prefix;
					string text = registryKey.GetValue(name, LogFileInfo.MinFileTimeString) as string;
					long num;
					if (!long.TryParse(text, out num) || num < 0L)
					{
						ExTraceGlobals.ServiceTracer.TraceDebug<string>((long)this.GetHashCode(), "Will reprocess file. Invalid Watermark: {0}", text);
						return true;
					}
					FileInfo fileInfo = new FileInfo(this.fullName);
					if (fileInfo.CreationTimeUtc.ToFileTime() > num)
					{
						ExTraceGlobals.ServiceTracer.TraceDebug<DateTime, long>((long)this.GetHashCode(), "File not yet processed. CreateTime={0}, Watermark={1}", fileInfo.CreationTimeUtc, num);
						return true;
					}
					ExTraceGlobals.ServiceTracer.TraceDebug<string, string>((long)this.GetHashCode(), "File already processed. CreateTime={0}, Watermark:{1}", fileInfo.CreationTimeUtc.ToString(CultureInfo.InvariantCulture), text);
					isProcessed = true;
					return true;
				}
			}
			catch (SecurityException ex)
			{
				arg = ex;
			}
			catch (UnauthorizedAccessException ex2)
			{
				arg = ex2;
			}
			catch (IOException ex3)
			{
				arg = ex3;
			}
			ExTraceGlobals.ServiceTracer.TraceError<Exception>((long)this.GetHashCode(), "Error checking watermark: {0}", arg);
			return false;
		}

		// Token: 0x06000080 RID: 128 RVA: 0x00004B40 File Offset: 0x00002D40
		private void MarkAsProcessed()
		{
			Exception ex = null;
			try
			{
				using (RegistryKey registryKey = this.OpenWatermarkKey(true))
				{
					if (registryKey == null)
					{
						ExTraceGlobals.ServiceTracer.TraceError((long)this.GetHashCode(), "Unable to open or create watermark key");
						return;
					}
					FileInfo fileInfo = new FileInfo(this.fullName);
					string text = fileInfo.CreationTimeUtc.ToFileTime().ToString(CultureInfo.InvariantCulture);
					ExTraceGlobals.ServiceTracer.TraceDebug<string, string>((long)this.GetHashCode(), "Updating watermark for {0} to {1}", this.prefix, text);
					registryKey.SetValue(this.prefix, text);
				}
			}
			catch (SecurityException ex2)
			{
				ex = ex2;
			}
			catch (UnauthorizedAccessException ex3)
			{
				ex = ex3;
			}
			catch (IOException ex4)
			{
				ex = ex4;
			}
			if (ex != null)
			{
				ExTraceGlobals.ServiceTracer.TraceError<Exception>((long)this.GetHashCode(), "Error setting watermark, error will be ignored and some files will be reprocessed on startup: {0}", ex);
			}
		}

		// Token: 0x06000081 RID: 129 RVA: 0x00004C3C File Offset: 0x00002E3C
		private RegistryKey OpenWatermarkKey(bool createIfMissing)
		{
			RegistryKey result;
			using (RegistryKey registryKey = Registry.LocalMachine.OpenSubKey("SOFTWARE\\Microsoft\\ExchangeServer\\v15\\Transport", createIfMissing))
			{
				if (registryKey == null)
				{
					ExTraceGlobals.ServiceTracer.TraceError<string>((long)this.GetHashCode(), "Cannot open: {0}", "SOFTWARE\\Microsoft\\ExchangeServer\\v15\\Transport");
					result = null;
				}
				else
				{
					using (RegistryKey registryKey2 = LogFileInfo.OpenSubKey(registryKey, "LogSearch", createIfMissing))
					{
						if (registryKey2 == null)
						{
							ExTraceGlobals.ServiceTracer.TraceDebug<RegistryKey, string>((long)this.GetHashCode(), "Cannot open: {0}\\{1}", registryKey, "LogSearch");
							result = null;
						}
						else
						{
							RegistryKey registryKey3 = LogFileInfo.OpenSubKey(registryKey2, "Watermark", createIfMissing);
							if (registryKey3 == null)
							{
								ExTraceGlobals.ServiceTracer.TraceDebug<RegistryKey, string>((long)this.GetHashCode(), "Cannot open {0}\\{1}", registryKey2, "Watermark");
								result = null;
							}
							else
							{
								ExTraceGlobals.ServiceTracer.TraceDebug<RegistryKey>((long)this.GetHashCode(), "Opened {0}", registryKey3);
								result = registryKey3;
							}
						}
					}
				}
			}
			return result;
		}

		// Token: 0x04000042 RID: 66
		public static readonly string MinFileTimeString = "0";

		// Token: 0x04000047 RID: 71
		private CsvTable csvTable;

		// Token: 0x04000048 RID: 72
		private string fullName;

		// Token: 0x04000049 RID: 73
		private long length;

		// Token: 0x0400004A RID: 74
		private long activeOffset;

		// Token: 0x0400004B RID: 75
		private string prefix;

		// Token: 0x0400004C RID: 76
		private bool needsTimeCheck;

		// Token: 0x0400004D RID: 77
		private DateTime startTime;

		// Token: 0x0400004E RID: 78
		private bool isActive;

		// Token: 0x0400004F RID: 79
		private Dictionary<string, LogIndex> indexes = new Dictionary<string, LogIndex>();

		// Token: 0x04000050 RID: 80
		private ReaderWriterLockSlim fileProcessingLock = new ReaderWriterLockSlim();

		// Token: 0x02000020 RID: 32
		internal class OpenLogFileResult
		{
			// Token: 0x1700000F RID: 15
			// (get) Token: 0x06000083 RID: 131 RVA: 0x00004D38 File Offset: 0x00002F38
			public CsvFieldCache Cursor
			{
				get
				{
					return this.cursor;
				}
			}

			// Token: 0x17000010 RID: 16
			// (get) Token: 0x06000084 RID: 132 RVA: 0x00004D40 File Offset: 0x00002F40
			public FileStream FileStream
			{
				get
				{
					return this.fileStream;
				}
			}

			// Token: 0x06000085 RID: 133 RVA: 0x00004D48 File Offset: 0x00002F48
			public OpenLogFileResult(CsvFieldCache cursor, FileStream fileStream)
			{
				this.cursor = cursor;
				this.fileStream = fileStream;
			}

			// Token: 0x04000051 RID: 81
			private CsvFieldCache cursor;

			// Token: 0x04000052 RID: 82
			private FileStream fileStream;
		}
	}
}
