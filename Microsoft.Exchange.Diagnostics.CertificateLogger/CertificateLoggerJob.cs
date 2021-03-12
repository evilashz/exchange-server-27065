using System;
using System.IO;
using Microsoft.Exchange.Diagnostics.Service;
using Microsoft.Exchange.Diagnostics.Service.Common;
using Microsoft.Exchange.LogAnalyzer.Extensions.CertificateLog;
using Microsoft.ExLogAnalyzer;

namespace Microsoft.Exchange.Diagnostics.CertificateLogger
{
	// Token: 0x02000005 RID: 5
	public sealed class CertificateLoggerJob
	{
		// Token: 0x0600000F RID: 15 RVA: 0x000026FC File Offset: 0x000008FC
		public static Job Create(OutputStream stream, Watermarks watermarks, string jobName)
		{
			Logger.LogInformationMessage("Create certificate logging job.", new object[0]);
			if (stream == null)
			{
				throw new ArgumentNullException("stream");
			}
			if (watermarks == null)
			{
				throw new ArgumentNullException("watermarks");
			}
			JobConfiguration jobConfiguration = new JobConfiguration(jobName);
			Watermark watermark = watermarks.Get(jobName);
			string text = Path.Combine(jobConfiguration.DiagnosticsRootDirectory, "CertificateLogs");
			LogDirectorySource logSource = new LogDirectorySource(jobName, new CertificateLogSchema(), null, text, "*.log", new Comparison<LogFileInfo>(LogFileInfo.LastWriteTimeComparer), new DateTime?(watermark.Timestamp), null, 6, null);
			SingleStreamJob singleStreamJob = new SingleStreamJob(jobName, logSource, "CertificateLog", stream, Watermark.LatestWatermark, watermarks.Directory);
			singleStreamJob.Extension = new CertificateLogExtension(singleStreamJob);
			Logger.LogInformationMessage("Created certificate logging job", new object[0]);
			return singleStreamJob;
		}
	}
}
