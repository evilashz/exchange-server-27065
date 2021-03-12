using System;
using System.IO;
using Microsoft.Exchange.LogUploaderProxy;

namespace Microsoft.Exchange.LogUploader
{
	// Token: 0x0200001B RID: 27
	internal class LogFileInfo : IComparable<LogFileInfo>, ILogFileInfo
	{
		// Token: 0x0600013E RID: 318 RVA: 0x000059E4 File Offset: 0x00003BE4
		public LogFileInfo(string fileName, bool isActive, string instanceName, IWatermarkFileHelper wmkFileHelper)
		{
			ArgumentValidator.ThrowIfNullOrEmpty("fileName", fileName);
			ArgumentValidator.ThrowIfNull("wmkFileMgr", wmkFileHelper);
			ArgumentValidator.ThrowIfNullOrEmpty("LogFileDirectory", wmkFileHelper.LogFileDirectory);
			this.fileName = Path.GetFileName(fileName);
			ArgumentValidator.ThrowIfNullOrEmpty("this.fileName", this.fileName);
			this.fullFileName = Path.Combine(wmkFileHelper.LogFileDirectory, this.fileName);
			this.status = ProcessingStatus.NeedProcessing;
			this.isActive = isActive;
			this.syncObject = new object();
			this.instance = instanceName;
			if (!File.Exists(this.fullFileName))
			{
				EventLogger.Logger.LogEvent(LogUploaderEventLogConstants.Tuple_FailedToInstantiateLogFileInfoFileNotExist, this.fullFileName, new object[]
				{
					this.fullFileName
				});
				ServiceLogger.LogError(ServiceLogger.Component.LogFileInfo, (LogUploaderEventLogConstants.Message)3221226477U, string.Empty, this.instance, this.fullFileName);
				throw new FailedToInstantiateLogFileInfoException(Strings.FailedToInstantiateLogFileInfoFileNotExist(this.fullFileName));
			}
			this.fileInfo = new FileInfo(this.fullFileName);
			this.fileInfo.Refresh();
			this.creationTimeUtc = this.fileInfo.CreationTimeUtc;
			this.waterMarkFile = wmkFileHelper.CreateWaterMarkFileObj(this.fileName, instanceName);
			Tools.DebugAssert(this.waterMarkFile != null, "this.waterMarkFile != null");
		}

		// Token: 0x17000096 RID: 150
		// (get) Token: 0x0600013F RID: 319 RVA: 0x00005B2A File Offset: 0x00003D2A
		public IWatermarkFile WatermarkFileObj
		{
			get
			{
				return this.waterMarkFile;
			}
		}

		// Token: 0x17000097 RID: 151
		// (get) Token: 0x06000140 RID: 320 RVA: 0x00005B32 File Offset: 0x00003D32
		public DateTime LastWriteTimeUtc
		{
			get
			{
				if (File.Exists(this.fullFileName))
				{
					this.fileInfo.Refresh();
					return this.fileInfo.LastWriteTimeUtc;
				}
				throw new MessageTracingException(Strings.GetLogTimeStampFailed(this.fullFileName));
			}
		}

		// Token: 0x17000098 RID: 152
		// (get) Token: 0x06000141 RID: 321 RVA: 0x00005B68 File Offset: 0x00003D68
		public DateTime CreationTimeUtc
		{
			get
			{
				return this.creationTimeUtc;
			}
		}

		// Token: 0x17000099 RID: 153
		// (get) Token: 0x06000142 RID: 322 RVA: 0x00005B70 File Offset: 0x00003D70
		// (set) Token: 0x06000143 RID: 323 RVA: 0x00005BB4 File Offset: 0x00003DB4
		public ProcessingStatus Status
		{
			get
			{
				ProcessingStatus result;
				lock (this.syncObject)
				{
					result = this.status;
				}
				return result;
			}
			set
			{
				lock (this.syncObject)
				{
					this.status = value;
				}
				if (this.status == ProcessingStatus.CompletedProcessing && this.HasEverBeenInactive && !this.inactiveTimeCounted)
				{
					long ticks = DateTime.UtcNow.Ticks;
					long incrementValue = ticks - this.startOfInactivityInTicks;
					PerfCountersInstanceCache.GetInstance(this.instance).AverageInactiveParseLatencyBase.Increment();
					PerfCountersInstanceCache.GetInstance(this.instance).AverageInactiveParseLatency.IncrementBy(incrementValue);
					this.inactiveTimeCounted = true;
				}
			}
		}

		// Token: 0x1700009A RID: 154
		// (get) Token: 0x06000144 RID: 324 RVA: 0x00005C5C File Offset: 0x00003E5C
		public string FileName
		{
			get
			{
				return this.fileName;
			}
		}

		// Token: 0x1700009B RID: 155
		// (get) Token: 0x06000145 RID: 325 RVA: 0x00005C64 File Offset: 0x00003E64
		public string FullFileName
		{
			get
			{
				return this.fullFileName;
			}
		}

		// Token: 0x1700009C RID: 156
		// (get) Token: 0x06000146 RID: 326 RVA: 0x00005C6C File Offset: 0x00003E6C
		// (set) Token: 0x06000147 RID: 327 RVA: 0x00005C74 File Offset: 0x00003E74
		public bool IsActive
		{
			get
			{
				return this.isActive;
			}
			set
			{
				if (!this.isActive && value)
				{
					ServiceLogger.LogError(ServiceLogger.Component.LogMonitor, (LogUploaderEventLogConstants.Message)2147486660U, string.Empty, this.instance, this.fullFileName);
					EventLogger.Logger.LogEvent(LogUploaderEventLogConstants.Tuple_InactiveFileTurnsToActiveException, this.fullFileName, new object[]
					{
						string.Empty
					});
				}
				this.isActive = value;
				if (!this.isActive && !this.HasEverBeenInactive)
				{
					this.startOfInactivityInTicks = DateTime.UtcNow.Ticks;
				}
			}
		}

		// Token: 0x1700009D RID: 157
		// (get) Token: 0x06000148 RID: 328 RVA: 0x00005CF8 File Offset: 0x00003EF8
		public long Size
		{
			get
			{
				long result = 0L;
				try
				{
					this.fileInfo.Refresh();
					result = this.fileInfo.Length;
				}
				catch (FileNotFoundException)
				{
					ServiceLogger.LogError(ServiceLogger.Component.LogMonitor, (LogUploaderEventLogConstants.Message)3221231489U, string.Empty, this.instance, this.fullFileName);
					result = 0L;
				}
				return result;
			}
		}

		// Token: 0x1700009E RID: 158
		// (get) Token: 0x06000149 RID: 329 RVA: 0x00005D54 File Offset: 0x00003F54
		public long FileSizeAtLastDirectoryCheck
		{
			get
			{
				return this.fileSizeAtLastDirectoryCheck;
			}
		}

		// Token: 0x1700009F RID: 159
		// (get) Token: 0x0600014A RID: 330 RVA: 0x00005D5C File Offset: 0x00003F5C
		// (set) Token: 0x0600014B RID: 331 RVA: 0x00005D64 File Offset: 0x00003F64
		public DateTime LastProcessedTime
		{
			get
			{
				return this.lastProcessedTime;
			}
			internal set
			{
				this.lastProcessedTime = value;
			}
		}

		// Token: 0x170000A0 RID: 160
		// (get) Token: 0x0600014C RID: 332 RVA: 0x00005D6D File Offset: 0x00003F6D
		public bool FileExists
		{
			get
			{
				return File.Exists(this.fullFileName);
			}
		}

		// Token: 0x170000A1 RID: 161
		// (get) Token: 0x0600014D RID: 333 RVA: 0x00005D7A File Offset: 0x00003F7A
		private bool HasEverBeenInactive
		{
			get
			{
				return this.startOfInactivityInTicks != 0L;
			}
		}

		// Token: 0x0600014E RID: 334 RVA: 0x00005D8C File Offset: 0x00003F8C
		public int CompareTo(LogFileInfo other)
		{
			return this.CreationTimeUtc.CompareTo(other.CreationTimeUtc);
		}

		// Token: 0x0600014F RID: 335 RVA: 0x00005DB0 File Offset: 0x00003FB0
		public long AddedLogSize()
		{
			long size = this.Size;
			long num = this.fileSizeAtLastDirectoryCheck;
			this.fileSizeAtLastDirectoryCheck = size;
			return size - num;
		}

		// Token: 0x0400009D RID: 157
		private readonly string fileName;

		// Token: 0x0400009E RID: 158
		private readonly string fullFileName;

		// Token: 0x0400009F RID: 159
		private readonly string instance;

		// Token: 0x040000A0 RID: 160
		private readonly DateTime creationTimeUtc;

		// Token: 0x040000A1 RID: 161
		private ProcessingStatus status;

		// Token: 0x040000A2 RID: 162
		private bool isActive;

		// Token: 0x040000A3 RID: 163
		private long startOfInactivityInTicks;

		// Token: 0x040000A4 RID: 164
		private bool inactiveTimeCounted;

		// Token: 0x040000A5 RID: 165
		private object syncObject;

		// Token: 0x040000A6 RID: 166
		private FileInfo fileInfo;

		// Token: 0x040000A7 RID: 167
		private DateTime lastProcessedTime;

		// Token: 0x040000A8 RID: 168
		private long fileSizeAtLastDirectoryCheck;

		// Token: 0x040000A9 RID: 169
		private IWatermarkFile waterMarkFile;
	}
}
