using System;
using System.Globalization;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Security.Cryptography.X509Certificates;

namespace Microsoft.Exchange.EdgeSync.Logging
{
	// Token: 0x02000053 RID: 83
	internal class EdgeSyncLogSession
	{
		// Token: 0x06000217 RID: 535 RVA: 0x0000A34B File Offset: 0x0000854B
		internal EdgeSyncLogSession(EdgeSyncLog edgeSyncLog, LogRowFormatter row, EdgeSyncLoggingLevel loggingLevel)
		{
			this.edgeSyncLog = edgeSyncLog;
			this.row = row;
			this.loggingLevel = loggingLevel;
			this.row[2] = 0;
		}

		// Token: 0x170000BB RID: 187
		// (get) Token: 0x06000218 RID: 536 RVA: 0x0000A37A File Offset: 0x0000857A
		// (set) Token: 0x06000219 RID: 537 RVA: 0x0000A382 File Offset: 0x00008582
		public EdgeSyncLoggingLevel LoggingLevel
		{
			get
			{
				return this.loggingLevel;
			}
			set
			{
				this.loggingLevel = value;
			}
		}

		// Token: 0x0600021A RID: 538 RVA: 0x0000A38B File Offset: 0x0000858B
		public void LogService(string message)
		{
			this.LogEvent(EdgeSyncLoggingLevel.None, EdgeSyncEvent.Service, null, message);
		}

		// Token: 0x0600021B RID: 539 RVA: 0x0000A397 File Offset: 0x00008597
		public void LogConfiguration(string message)
		{
			this.LogEvent(EdgeSyncLoggingLevel.Low, EdgeSyncEvent.Configuration, null, message);
		}

		// Token: 0x0600021C RID: 540 RVA: 0x0000A3A3 File Offset: 0x000085A3
		public void LogLease(string message, string host)
		{
			this.LogEvent(EdgeSyncLoggingLevel.High, EdgeSyncEvent.SyncEngine, host, message);
		}

		// Token: 0x0600021D RID: 541 RVA: 0x0000A3B0 File Offset: 0x000085B0
		public void LogLeaseTakeover(string oldLease, string newLease)
		{
			this.LogEvent(EdgeSyncLoggingLevel.Medium, EdgeSyncEvent.SyncEngine, string.Format(CultureInfo.InvariantCulture, "Old:{0} New:{1}", new object[]
			{
				oldLease,
				newLease
			}), "LeaseTakeover");
		}

		// Token: 0x0600021E RID: 542 RVA: 0x0000A3EC File Offset: 0x000085EC
		public void LogLeaseRefresh(string oldLease, string newLease)
		{
			this.LogEvent(EdgeSyncLoggingLevel.High, EdgeSyncEvent.SyncEngine, string.Format(CultureInfo.InvariantCulture, "Old:{0} New:{1}", new object[]
			{
				oldLease,
				newLease
			}), "LeaseRefresh");
		}

		// Token: 0x0600021F RID: 543 RVA: 0x0000A425 File Offset: 0x00008625
		public void LogLeaseHeld(string leaseHolder)
		{
			this.LogEvent(EdgeSyncLoggingLevel.High, EdgeSyncEvent.SyncEngine, "Can't take over because was held by " + leaseHolder, "LeaseHeld");
		}

		// Token: 0x06000220 RID: 544 RVA: 0x0000A43F File Offset: 0x0000863F
		public void LogConnectFailure(string message, string username, string host, string hash)
		{
			this.LogEvent(EdgeSyncLoggingLevel.Low, EdgeSyncEvent.TargetConnection, host, message);
			this.LogEvent(EdgeSyncLoggingLevel.Low, EdgeSyncEvent.TargetConnection, username, "Username");
			this.LogEvent(EdgeSyncLoggingLevel.Low, EdgeSyncEvent.TargetConnection, hash, "Password Hash");
		}

		// Token: 0x06000221 RID: 545 RVA: 0x0000A468 File Offset: 0x00008668
		public void LogRenewal(string message, string username, string host, DateTime startUtcDate, string hash)
		{
			string data = string.Format(CultureInfo.InvariantCulture, "Host:{0}, UserName:{1}, StartUtcDate:{2}, PasswordHash:{3}", new object[]
			{
				host,
				username,
				startUtcDate.ToString(CultureInfo.InvariantCulture),
				hash
			});
			this.LogEvent(EdgeSyncLoggingLevel.Low, EdgeSyncEvent.Credential, data, message);
		}

		// Token: 0x06000222 RID: 546 RVA: 0x0000A4B3 File Offset: 0x000086B3
		public void LogFailedDirectTrust(string host, string message, X509Certificate2 cert)
		{
			this.LogEvent(EdgeSyncLoggingLevel.Low, EdgeSyncEvent.TargetConnection, cert.Subject, message);
			this.LogCertificate(EdgeSyncLoggingLevel.Low, EdgeSyncEvent.TargetConnection, cert);
		}

		// Token: 0x06000223 RID: 547 RVA: 0x0000A4D0 File Offset: 0x000086D0
		public void LogCredentialDetail(string userName, DateTime startUtcDate)
		{
			string data = string.Format(CultureInfo.InvariantCulture, "User:{0}, StartUtcDate:{1}", new object[]
			{
				userName,
				startUtcDate.ToString(CultureInfo.InvariantCulture)
			});
			this.LogEvent(EdgeSyncLoggingLevel.High, EdgeSyncEvent.TargetConnection, data, "EdgeSync Credential used for connection");
		}

		// Token: 0x06000224 RID: 548 RVA: 0x0000A516 File Offset: 0x00008716
		public void LogTopology(string host, string message)
		{
			this.LogEvent(EdgeSyncLoggingLevel.Medium, EdgeSyncEvent.Topology, host, message);
		}

		// Token: 0x06000225 RID: 549 RVA: 0x0000A524 File Offset: 0x00008724
		public void LogRenewalException(string message, Exception exception, string host)
		{
			string data = string.Format(CultureInfo.InvariantCulture, "Host:{0}, Exception:{1}", new object[]
			{
				host,
				(exception != null) ? exception.Message : string.Empty
			});
			this.LogEvent(EdgeSyncLoggingLevel.None, EdgeSyncEvent.Credential, data, message);
		}

		// Token: 0x06000226 RID: 550 RVA: 0x0000A56A File Offset: 0x0000876A
		public void LogSyncNow(string message)
		{
			this.LogEvent(EdgeSyncLoggingLevel.Low, EdgeSyncEvent.Service, null, message);
		}

		// Token: 0x06000227 RID: 551 RVA: 0x0000A576 File Offset: 0x00008776
		public void LogCredential(string host, string message)
		{
			this.LogEvent(EdgeSyncLoggingLevel.Low, EdgeSyncEvent.TargetConnection, host, message);
		}

		// Token: 0x06000228 RID: 552 RVA: 0x0000A584 File Offset: 0x00008784
		public void LogProbe(string message, string username, string host, DateTime startUtcDate, string hash)
		{
			string data = string.Format(CultureInfo.InvariantCulture, "Host:{0}, UserName:{1}, StartUtcDate:{2}, PasswordHash:{3}", new object[]
			{
				host,
				username,
				startUtcDate.ToString(CultureInfo.InvariantCulture),
				hash
			});
			this.LogEvent(EdgeSyncLoggingLevel.Low, EdgeSyncEvent.Credential, data, message);
		}

		// Token: 0x06000229 RID: 553 RVA: 0x0000A5D0 File Offset: 0x000087D0
		public void LogException(EdgeSyncLoggingLevel level, EdgeSyncEvent edgeEvent, Exception exception, string context)
		{
			if (!this.CanLogEvent(level))
			{
				return;
			}
			StringBuilder stringBuilder = new StringBuilder(exception.Message.Length + exception.GetType().Name.Length + 3);
			stringBuilder.AppendFormat("{0} [{1}]", exception.Message, exception.GetType().Name);
			if (exception.InnerException != null)
			{
				stringBuilder.AppendFormat("; Inner Exception: {0} [{1}]", exception.InnerException.Message, exception.InnerException.GetType().Name);
			}
			this.LogEvent(level, edgeEvent, stringBuilder.ToString(), context);
		}

		// Token: 0x0600022A RID: 554 RVA: 0x0000A668 File Offset: 0x00008868
		public void LogCertificate(EdgeSyncLoggingLevel level, EdgeSyncEvent logEvent, X509Certificate2 cert)
		{
			if (cert == null)
			{
				return;
			}
			if (!this.CanLogEvent(level))
			{
				return;
			}
			this.LogEvent(level, logEvent, cert.Subject, "Certificate subject");
			this.LogEvent(level, logEvent, cert.IssuerName.Name, "Certificate issuer name");
			this.LogEvent(level, logEvent, cert.SerialNumber, "Certificate serial number");
			this.LogEvent(level, logEvent, cert.Thumbprint, "Certificate thumbprint");
			StringBuilder stringBuilder = new StringBuilder(256);
			foreach (string value in TlsCertificateInfo.GetFQDNs(cert))
			{
				if (stringBuilder.Length != 0)
				{
					stringBuilder.Append(';');
				}
				stringBuilder.Append(value);
			}
			this.LogEvent(level, logEvent, stringBuilder.ToString(), "Certificate alternate names");
		}

		// Token: 0x0600022B RID: 555 RVA: 0x0000A744 File Offset: 0x00008944
		public void LogEvent(EdgeSyncLoggingLevel loggingLevel, EdgeSyncEvent eventId, string data, string context)
		{
			if (!this.CanLogEvent(loggingLevel))
			{
				return;
			}
			this.row[6] = eventId;
			this.row[9] = context;
			this.row[8] = data;
			this.row[7] = loggingLevel;
			this.edgeSyncLog.Append(this.row);
			this.row[2] = (int)this.row[2] + 1;
		}

		// Token: 0x0600022C RID: 556 RVA: 0x0000A7D0 File Offset: 0x000089D0
		private bool CanLogEvent(EdgeSyncLoggingLevel loggingLevel)
		{
			return this.edgeSyncLog.Enabled && this.loggingLevel >= loggingLevel;
		}

		// Token: 0x04000177 RID: 375
		private EdgeSyncLog edgeSyncLog;

		// Token: 0x04000178 RID: 376
		private LogRowFormatter row;

		// Token: 0x04000179 RID: 377
		private EdgeSyncLoggingLevel loggingLevel;
	}
}
