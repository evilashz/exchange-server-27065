using System;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using Microsoft.Exchange.Diagnostics.Service.Common;
using PlaLibrary;
using TaskScheduler;

namespace Microsoft.Exchange.Diagnostics.PerformanceLogger
{
	// Token: 0x02000004 RID: 4
	public class PerformanceLogSet
	{
		// Token: 0x0600000F RID: 15 RVA: 0x00002552 File Offset: 0x00000752
		public PerformanceLogSet(string performanceLogXmlFile, string counterSetName) : this(performanceLogXmlFile, counterSetName, null, new PerformanceLogSet.PerformanceLogFormat?(PerformanceLogSet.PerformanceLogFormat.CSV), new PerformanceLogSet.FileNameFormatPattern?(PerformanceLogSet.FileNameFormatPattern.NNNNNN), TimeSpan.FromSeconds(15.0), TimeSpan.FromMinutes(5.0))
		{
		}

		// Token: 0x06000010 RID: 16 RVA: 0x00002589 File Offset: 0x00000789
		public PerformanceLogSet(string performanceLogXmlFile, string counterSetName, string performanceLogPath) : this(performanceLogXmlFile, counterSetName, performanceLogPath, new PerformanceLogSet.PerformanceLogFormat?(PerformanceLogSet.PerformanceLogFormat.CSV))
		{
		}

		// Token: 0x06000011 RID: 17 RVA: 0x0000259A File Offset: 0x0000079A
		public PerformanceLogSet(string performanceLogXmlFile, string counterSetName, string performanceLogPath, PerformanceLogSet.PerformanceLogFormat? logFormat) : this(performanceLogXmlFile, counterSetName, performanceLogPath, logFormat, new PerformanceLogSet.FileNameFormatPattern?(PerformanceLogSet.FileNameFormatPattern.MMddHHmm), TimeSpan.FromSeconds(15.0), TimeSpan.FromMinutes(5.0))
		{
		}

		// Token: 0x06000012 RID: 18 RVA: 0x000025D0 File Offset: 0x000007D0
		public PerformanceLogSet(string performanceLogXmlFile, string counterSetName, string performanceLogPath, PerformanceLogSet.PerformanceLogFormat? logFormat, PerformanceLogSet.FileNameFormatPattern? fileNameFormat, TimeSpan sampleInterval, TimeSpan maximumDuration)
		{
			Directory.CreateDirectory(performanceLogPath);
			this.counterSetName = counterSetName;
			this.performanceLogXmlFile = performanceLogXmlFile;
			this.performanceLogPath = performanceLogPath;
			this.logFormat = logFormat;
			this.fileNameFormat = fileNameFormat;
			this.sampleInterval = sampleInterval;
			this.maximumDuration = maximumDuration;
		}

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x06000013 RID: 19 RVA: 0x0000261F File Offset: 0x0000081F
		public string CounterSetName
		{
			get
			{
				return this.counterSetName;
			}
		}

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x06000014 RID: 20 RVA: 0x00002628 File Offset: 0x00000828
		public PerformanceLogSet.PerformanceLogSetStatus Status
		{
			get
			{
				PerformanceLogSet.PerformanceLogSetStatus result = PerformanceLogSet.PerformanceLogSetStatus.DoesNotExist;
				try
				{
					IRegisteredTask task = PerformanceLogSet.GetTask(this.counterSetName);
					if (task.State == 4)
					{
						result = PerformanceLogSet.PerformanceLogSetStatus.Running;
					}
					else
					{
						result = PerformanceLogSet.PerformanceLogSetStatus.Stopped;
					}
				}
				catch (FileNotFoundException)
				{
				}
				catch (Exception ex)
				{
					Logger.LogErrorMessage("Exception querying task scheduler: {0}", new object[]
					{
						ex
					});
				}
				return result;
			}
		}

		// Token: 0x06000015 RID: 21 RVA: 0x00002690 File Offset: 0x00000890
		public static void DeleteTask(string counterSetName)
		{
			ITaskService taskService = new TaskSchedulerClass();
			taskService.Connect(Missing.Value, Missing.Value, Missing.Value, Missing.Value);
			try
			{
				ITaskFolder folder = taskService.GetFolder("\\Microsoft\\Windows\\PLA");
				folder.DeleteTask(counterSetName, 0);
			}
			catch (Exception)
			{
			}
		}

		// Token: 0x06000016 RID: 22 RVA: 0x000026E8 File Offset: 0x000008E8
		public static void StopCounterSet(string counterSetName)
		{
			try
			{
				IRegisteredTask task = PerformanceLogSet.GetTask(counterSetName);
				task.Stop(0);
			}
			catch (FileNotFoundException)
			{
			}
		}

		// Token: 0x06000017 RID: 23 RVA: 0x00002718 File Offset: 0x00000918
		public bool LogStatus()
		{
			return this.Status == PerformanceLogSet.PerformanceLogSetStatus.Running;
		}

		// Token: 0x06000018 RID: 24 RVA: 0x00002723 File Offset: 0x00000923
		public bool Exists()
		{
			return this.Status != PerformanceLogSet.PerformanceLogSetStatus.DoesNotExist;
		}

		// Token: 0x06000019 RID: 25 RVA: 0x00002734 File Offset: 0x00000934
		public void StartLog(bool synchronous)
		{
			try
			{
				Logger.LogInformationMessage("Starting counter set name {0}", new object[]
				{
					this.counterSetName
				});
				DataCollectorSet dataCollectorSet = this.ConstructDataCollectorSet();
				dataCollectorSet.start(synchronous);
			}
			catch (COMException ex)
			{
				if (-2147216609 != ex.ErrorCode)
				{
					throw;
				}
			}
		}

		// Token: 0x0600001A RID: 26 RVA: 0x00002790 File Offset: 0x00000990
		public void StopLog(bool synchronous)
		{
			try
			{
				Logger.LogInformationMessage("Stopping counter set name {0}", new object[]
				{
					this.counterSetName
				});
				PerformanceLogSet.StopCounterSet(this.counterSetName);
				this.ConstructDataCollectorSet();
			}
			catch (COMException ex)
			{
				if (-2144337918 != ex.ErrorCode && -2144337660 != ex.ErrorCode)
				{
					throw;
				}
			}
		}

		// Token: 0x0600001B RID: 27 RVA: 0x000027FC File Offset: 0x000009FC
		public void DeleteLogSettings()
		{
			if (this.Status != PerformanceLogSet.PerformanceLogSetStatus.DoesNotExist)
			{
				try
				{
					this.StopLog(true);
					Logger.LogInformationMessage("Deleting counter set name {0}", new object[]
					{
						this.counterSetName
					});
					DataCollectorSet dataCollectorSet = this.ConstructDataCollectorSet();
					dataCollectorSet.Delete();
				}
				catch (COMException ex)
				{
					if (-2144337918 != ex.ErrorCode)
					{
						throw;
					}
				}
			}
		}

		// Token: 0x0600001C RID: 28 RVA: 0x00002864 File Offset: 0x00000A64
		public void CreateLogSettings()
		{
			Logger.LogInformationMessage("Creating counter set name {0}", new object[]
			{
				this.counterSetName
			});
			DataCollectorSet dataCollectorSet = this.ConstructDataCollectorSet();
			try
			{
				if (dataCollectorSet.Status != null)
				{
					dataCollectorSet.Stop(true);
					dataCollectorSet.Delete();
				}
			}
			catch (Exception)
			{
			}
			dataCollectorSet.Commit(this.counterSetName, null, 3);
		}

		// Token: 0x0600001D RID: 29 RVA: 0x000028CC File Offset: 0x00000ACC
		private static IRegisteredTask GetTask(string counterSetName)
		{
			ITaskService taskService = new TaskSchedulerClass();
			taskService.Connect(null, null, null, null);
			ITaskFolder folder = taskService.GetFolder("\\Microsoft\\Windows\\PLA");
			return folder.GetTask(counterSetName);
		}

		// Token: 0x0600001E RID: 30 RVA: 0x000028FC File Offset: 0x00000AFC
		private DataCollectorSet ConstructDataCollectorSet()
		{
			if (string.IsNullOrEmpty(this.performanceLogXmlFile))
			{
				throw new ArgumentException("performanceLogXmlFile");
			}
			if (!File.Exists(this.performanceLogXmlFile))
			{
				throw new ArgumentException(string.Format("PerformanceLogXmlFile {0} does not exist", this.performanceLogXmlFile));
			}
			string xml = File.ReadAllText(this.performanceLogXmlFile);
			if (PerformanceLogSet.twentyFourHours < this.sampleInterval)
			{
				throw new ArgumentOutOfRangeException("sampleInterval", string.Format("Must be less than or equal to {0} hours", PerformanceLogSet.twentyFourHours.ToString()));
			}
			if (PerformanceLogSet.twentyFourHours < this.maximumDuration)
			{
				throw new ArgumentOutOfRangeException("maximumDuration", string.Format("Must be less than or equal to {0} hours", PerformanceLogSet.twentyFourHours.ToString()));
			}
			DataCollectorSet dataCollectorSet = new DataCollectorSetClass();
			dataCollectorSet.SetXml(xml);
			dataCollectorSet.Security = null;
			IDataCollectorCollection dataCollectors = dataCollectorSet.DataCollectors;
			if (1 != dataCollectors.Count || dataCollectors[0].DataCollectorType != null)
			{
				throw new ArgumentException("performanceLogXmlFile is invalid. It must contain only 1 data collected of type Performance Counter");
			}
			IPerformanceCounterDataCollector performanceCounterDataCollector = (IPerformanceCounterDataCollector)dataCollectors[0];
			if (this.counterSetName == null)
			{
				throw new ArgumentNullException("counterSetName");
			}
			dataCollectorSet.DisplayName = this.counterSetName;
			performanceCounterDataCollector.FileName = this.counterSetName;
			if (this.performanceLogPath != null)
			{
				dataCollectorSet.RootPath = this.performanceLogPath;
			}
			if (this.logFormat != null)
			{
				performanceCounterDataCollector.LogFileFormat = this.logFormat.Value;
			}
			if (this.fileNameFormat != null)
			{
				performanceCounterDataCollector.FileNameFormat = this.fileNameFormat.Value;
			}
			if (0.0 != this.maximumDuration.TotalSeconds)
			{
				dataCollectorSet.SegmentMaxDuration = (uint)this.maximumDuration.TotalSeconds;
			}
			if (0.0 != this.sampleInterval.TotalSeconds)
			{
				performanceCounterDataCollector.SampleInterval = (uint)this.sampleInterval.TotalSeconds;
			}
			dataCollectorSet.Commit(this.counterSetName, null, 4096);
			return dataCollectorSet;
		}

		// Token: 0x0400000E RID: 14
		private const int HResultDataCollectorSetNotFound = -2144337918;

		// Token: 0x0400000F RID: 15
		private const int HResultDataCollectorSetNotRunning = -2144337660;

		// Token: 0x04000010 RID: 16
		private const int HResultDataCollectorSetAlreadyRunning = -2147216609;

		// Token: 0x04000011 RID: 17
		private static readonly TimeSpan twentyFourHours = TimeSpan.FromHours(24.0);

		// Token: 0x04000012 RID: 18
		private readonly string counterSetName;

		// Token: 0x04000013 RID: 19
		private readonly string performanceLogXmlFile;

		// Token: 0x04000014 RID: 20
		private readonly string performanceLogPath;

		// Token: 0x04000015 RID: 21
		private readonly PerformanceLogSet.PerformanceLogFormat? logFormat;

		// Token: 0x04000016 RID: 22
		private readonly PerformanceLogSet.FileNameFormatPattern? fileNameFormat;

		// Token: 0x04000017 RID: 23
		private readonly TimeSpan sampleInterval;

		// Token: 0x04000018 RID: 24
		private readonly TimeSpan maximumDuration;

		// Token: 0x02000005 RID: 5
		public enum PerformanceLogFormat
		{
			// Token: 0x0400001A RID: 26
			CSV,
			// Token: 0x0400001B RID: 27
			TSV,
			// Token: 0x0400001C RID: 28
			SQL,
			// Token: 0x0400001D RID: 29
			BIN
		}

		// Token: 0x02000006 RID: 6
		public enum FileNameFormatPattern
		{
			// Token: 0x0400001F RID: 31
			MMddHH = 256,
			// Token: 0x04000020 RID: 32
			NNNNNN = 512,
			// Token: 0x04000021 RID: 33
			yyyyDDD = 1024,
			// Token: 0x04000022 RID: 34
			yyyyMM = 2048,
			// Token: 0x04000023 RID: 35
			yyyyMMdd = 4096,
			// Token: 0x04000024 RID: 36
			yyyyMMddHH = 8192,
			// Token: 0x04000025 RID: 37
			MMddHHmm = 16384
		}

		// Token: 0x02000007 RID: 7
		public enum PerformanceLogSetStatus
		{
			// Token: 0x04000027 RID: 39
			DoesNotExist,
			// Token: 0x04000028 RID: 40
			Stopped,
			// Token: 0x04000029 RID: 41
			Running
		}
	}
}
