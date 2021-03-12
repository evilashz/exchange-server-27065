using System;
using Microsoft.Exchange.LogAnalyzer.Extensions.CertificateLog;

namespace Microsoft.Exchange.Diagnostics.CertificateLogger
{
	// Token: 0x02000002 RID: 2
	public class CertificateLog : IDisposable
	{
		// Token: 0x06000002 RID: 2 RVA: 0x000020EC File Offset: 0x000002EC
		public CertificateLog(string logDirectory, long maxDirectorySize, long maxFileSize, int maxBufferSize, TimeSpan flushInterval)
		{
			this.logSchema = new LogSchema("Microsoft Exchange Server", "15.0.0.0", "Certificate Log", CertificateInformation.GetColumnHeaders());
			LogHeaderFormatter headerFormatter = new LogHeaderFormatter(this.logSchema, LogHeaderCsvOption.CsvStrict);
			this.exlog = new Log(CertificateLog.LogFilePrefix, headerFormatter, "Certificate Log");
			this.exlog.Configure(logDirectory, TimeSpan.Zero, maxDirectorySize, maxFileSize, maxBufferSize, flushInterval, true);
		}

		// Token: 0x06000003 RID: 3 RVA: 0x00002159 File Offset: 0x00000359
		public void Dispose()
		{
			if (this.exlog != null)
			{
				this.exlog.Close();
			}
		}

		// Token: 0x06000004 RID: 4 RVA: 0x00002170 File Offset: 0x00000370
		public void LogCertificateInformation(CertificateInformation info)
		{
			if (info == null)
			{
				throw new ArgumentNullException("info");
			}
			string value = (info.RootCertificate == null) ? string.Empty : info.RootCertificate.Format(false);
			LogRowFormatter logRowFormatter = new LogRowFormatter(this.logSchema);
			logRowFormatter[1] = Environment.MachineName;
			logRowFormatter[2] = info.StoreName;
			logRowFormatter[3] = info.Version;
			logRowFormatter[4] = info.Subject.Format(false);
			logRowFormatter[5] = info.Issuer.Format(false);
			logRowFormatter[6] = info.ValidFrom;
			logRowFormatter[7] = info.ValidTo;
			logRowFormatter[8] = info.Thumbprint;
			logRowFormatter[9] = info.SignatureAlgorithm;
			logRowFormatter[10] = info.PublicKeySize;
			logRowFormatter[11] = info.SerialNumber;
			logRowFormatter[12] = value;
			logRowFormatter[13] = info.SubjectKeyIdentifier;
			logRowFormatter[14] = info.BasicConstraints;
			logRowFormatter[15] = info.KeyUsage;
			logRowFormatter[16] = info.EnhancedKeyUsage;
			logRowFormatter[17] = info.ComponentOwner;
			this.exlog.Append(logRowFormatter, 0);
		}

		// Token: 0x04000001 RID: 1
		private const string ComponentName = "Certificate Log";

		// Token: 0x04000002 RID: 2
		private static readonly string LogFilePrefix = string.Format("{0}_{1}_", Environment.MachineName, "CertificateLog");

		// Token: 0x04000003 RID: 3
		private readonly Log exlog;

		// Token: 0x04000004 RID: 4
		private readonly LogSchema logSchema;
	}
}
