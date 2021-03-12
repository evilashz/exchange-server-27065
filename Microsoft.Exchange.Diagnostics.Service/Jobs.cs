using System;
using System.IO;
using System.Text;
using Microsoft.Exchange.Diagnostics.Service.Common;
using Microsoft.Exchange.LogAnalyzer.Extensions.Perflog;
using Microsoft.ExLogAnalyzer;

namespace Microsoft.Exchange.Diagnostics.Service
{
	// Token: 0x0200000D RID: 13
	public class Jobs
	{
		// Token: 0x06000042 RID: 66 RVA: 0x00004D38 File Offset: 0x00002F38
		public static Job NewMachineInformationJob(OutputStream stream, Watermarks watermarks, string jobName)
		{
			Logger.LogInformationMessage("Creating machine information job.", new object[0]);
			if (stream == null)
			{
				throw new ArgumentNullException("stream");
			}
			MachineInformationSource logSource = new MachineInformationSource(jobName, TimeSpan.FromMinutes(5.0));
			SingleStreamJob singleStreamJob = new SingleStreamJob(jobName, logSource, "CsvPassThrough", stream, Watermark.LatestWatermark, watermarks.Directory);
			CsvPassThroughExtension extension = new CsvPassThroughExtension(singleStreamJob);
			singleStreamJob.Extension = extension;
			Logger.LogInformationMessage("Created machine information job.", new object[0]);
			return singleStreamJob;
		}

		// Token: 0x06000043 RID: 67 RVA: 0x00004E40 File Offset: 0x00003040
		public static Job NewPerfLogJob(OutputStream stream, Watermarks watermarks, string jobName)
		{
			Logger.LogInformationMessage("Creating performance log job.", new object[0]);
			if (stream == null)
			{
				throw new ArgumentNullException("stream");
			}
			JobConfiguration jobConfiguration = new JobConfiguration(jobName);
			Watermark watermark = watermarks.Get(jobName);
			string text = Path.Combine(jobConfiguration.DiagnosticsRootDirectory, "PerformanceLogsToBeProcessed");
			bool configBool = Configuration.GetConfigBool("PerformProcessedFileAction", false);
			Action<string> action = delegate(string filePath)
			{
				if (string.IsNullOrEmpty(filePath))
				{
					return;
				}
				try
				{
					File.Delete(filePath);
					Log.LogInformationMessage("Deleted processed file '{0}'.", new object[]
					{
						filePath
					});
				}
				catch (IOException ex)
				{
					Log.LogErrorMessage("Unable to delete processed file '{0}', due to an IO exception: {1}", new object[]
					{
						filePath,
						ex
					});
				}
				catch (UnauthorizedAccessException ex2)
				{
					Log.LogErrorMessage("Unable to delete processed file '{0}', due to an unauthorized access exception: {1}", new object[]
					{
						filePath,
						ex2
					});
				}
			};
			LogDirectorySource logSource = new LogDirectorySource(jobName, new LogPerfLogSchema(), null, text, "*.csv", new Comparison<LogFileInfo>(LogFileInfo.LastWriteTimeComparer), new DateTime?(watermark.Timestamp), null, 6, configBool ? action : null, Encoding.Default);
			SingleStreamJob singleStreamJob = new SingleStreamJob(jobName, logSource, "PerfLog", stream, watermark, watermarks.Directory);
			PerfLogExtension extension = new PerfLogExtension(singleStreamJob);
			singleStreamJob.Extension = extension;
			Logger.LogInformationMessage("Created performance log job.", new object[0]);
			return singleStreamJob;
		}

		// Token: 0x06000044 RID: 68 RVA: 0x00004F98 File Offset: 0x00003198
		public static Job NewSqlUploaderJob(OutputStream stream, Watermarks watermarks, string jobName)
		{
			Logger.LogInformationMessage("Creating sql uploader job.", new object[0]);
			if (stream == null)
			{
				Logger.LogErrorMessage("No sql output stream, SqlUploaderJob will not be added.", new object[0]);
				return null;
			}
			if (watermarks == null)
			{
				throw new ArgumentNullException("watermarks");
			}
			JobConfiguration jobConfiguration = new JobConfiguration(jobName);
			Watermark watermark = watermarks.Get(jobName);
			string text = Path.Combine(jobConfiguration.DiagnosticsRootDirectory, "AnalyzerLogs");
			string cosmosLogDirectory = Path.Combine(jobConfiguration.DiagnosticsRootDirectory, "CosmosLog");
			bool configBool = Configuration.GetConfigBool("PerformProcessedFileAction", false);
			Action<string> action = delegate(string filePath)
			{
				if (string.IsNullOrEmpty(filePath))
				{
					return;
				}
				try
				{
					File.Move(filePath, Path.Combine(cosmosLogDirectory, Path.GetFileName(filePath)));
				}
				catch (Exception ex)
				{
					Logger.LogErrorMessage("Unable to move a csv file to CosmosLog directory. Ignore this file: '{0}' Exception '{1}'", new object[]
					{
						filePath,
						ex
					});
				}
			};
			LogDirectorySource logSource = new LogDirectorySource(jobName, new FileChunkSchema(), null, text, "*.log", new Comparison<LogFileInfo>(LogFileInfo.LastWriteTimeComparer), new DateTime?(watermark.Timestamp), null, 6, configBool ? action : null);
			SingleStreamJob singleStreamJob = new SingleStreamJob(jobName, logSource, "CsvPassThrough", stream, watermark, watermarks.Directory);
			CsvPassThroughExtension extension = new CsvPassThroughExtension(singleStreamJob);
			singleStreamJob.Extension = extension;
			return singleStreamJob;
		}
	}
}
