using System;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using Microsoft.Exchange.Conversion;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Security.Cryptography.X509Certificates;

namespace Microsoft.Exchange.Net.Logging
{
	// Token: 0x0200076C RID: 1900
	internal class ProtocolLogSession
	{
		// Token: 0x06002592 RID: 9618 RVA: 0x0004EF0B File Offset: 0x0004D10B
		internal ProtocolLogSession(ProtocolLog protocolLog, LogRowFormatter row)
		{
			this.protocolLog = protocolLog;
			this.row = row;
		}

		// Token: 0x06002593 RID: 9619 RVA: 0x0004EF21 File Offset: 0x0004D121
		public void LogConnect(string context)
		{
			this.LogEvent(ProtocolLoggingLevel.All, ProtocolEvent.Connect, null, context);
		}

		// Token: 0x06002594 RID: 9620 RVA: 0x0004EF31 File Offset: 0x0004D131
		public void LogDisconnect(string reason)
		{
			this.LogEvent(ProtocolLoggingLevel.All, ProtocolEvent.Disconnect, null, reason);
		}

		// Token: 0x06002595 RID: 9621 RVA: 0x0004EF41 File Offset: 0x0004D141
		public void LogSend(byte[] data)
		{
			this.LogEvent(ProtocolLoggingLevel.All, ProtocolEvent.Send, data, null);
		}

		// Token: 0x06002596 RID: 9622 RVA: 0x0004EF51 File Offset: 0x0004D151
		public void LogReceive(byte[] data)
		{
			this.LogEvent(ProtocolLoggingLevel.All, ProtocolEvent.Receive, data, null);
		}

		// Token: 0x06002597 RID: 9623 RVA: 0x0004EF61 File Offset: 0x0004D161
		public void LogInformation(ProtocolLoggingLevel loggingLevel, byte[] data, string context)
		{
			this.LogEvent(loggingLevel, ProtocolEvent.Information, data, context);
		}

		// Token: 0x06002598 RID: 9624 RVA: 0x0004EF71 File Offset: 0x0004D171
		public void LogStringInformation(ProtocolLoggingLevel loggingLevel, string data, string context)
		{
			this.LogEvent(loggingLevel, ProtocolEvent.Information, Encoding.UTF8.GetBytes(data), context);
		}

		// Token: 0x06002599 RID: 9625 RVA: 0x0004EF8C File Offset: 0x0004D18C
		public void LogCertificate(string type, X509Certificate2 cert)
		{
			if (cert == null)
			{
				return;
			}
			this.LogInformation(ProtocolLoggingLevel.All, null, type);
			this.LogStringInformation(ProtocolLoggingLevel.All, cert.Subject, "Certificate subject");
			this.LogStringInformation(ProtocolLoggingLevel.All, cert.IssuerName.Name, "Certificate issuer name");
			this.LogStringInformation(ProtocolLoggingLevel.All, cert.SerialNumber, "Certificate serial number");
			this.LogStringInformation(ProtocolLoggingLevel.All, cert.Thumbprint, "Certificate thumbprint");
			StringBuilder stringBuilder = new StringBuilder(256);
			foreach (string value in TlsCertificateInfo.GetFQDNs(cert))
			{
				if (stringBuilder.Length != 0)
				{
					stringBuilder.Append(';');
				}
				stringBuilder.Append(value);
			}
			this.LogStringInformation(ProtocolLoggingLevel.All, stringBuilder.ToString(), "Certificate alternate names");
		}

		// Token: 0x0600259A RID: 9626 RVA: 0x0004F064 File Offset: 0x0004D264
		public void LogCertificateThumbprint(string type, X509Certificate2 cert)
		{
			if (cert == null)
			{
				return;
			}
			this.LogInformation(ProtocolLoggingLevel.All, null, type);
			this.LogStringInformation(ProtocolLoggingLevel.All, cert.Thumbprint, "Certificate thumbprint");
		}

		// Token: 0x0600259B RID: 9627 RVA: 0x0004F088 File Offset: 0x0004D288
		private static byte[] GetLine(byte[] buffer, int start)
		{
			int i = start;
			int num = -1;
			byte[] array = null;
			if (buffer == null)
			{
				return null;
			}
			while (i < buffer.Length)
			{
				i = ProtocolLogSession.IndexOf(buffer, 10, i);
				if (i == -1)
				{
					num = buffer.Length - start;
					break;
				}
				if (i > start && buffer[i - 1] == 13)
				{
					num = i - start + 1 - 2;
					break;
				}
				i++;
			}
			if (num > 0)
			{
				if (start == 0 && num == buffer.Length)
				{
					array = buffer;
				}
				else
				{
					array = new byte[num];
					Buffer.BlockCopy(buffer, start, array, 0, num);
				}
			}
			return array;
		}

		// Token: 0x0600259C RID: 9628 RVA: 0x0004F0FA File Offset: 0x0004D2FA
		private static int IndexOf(byte[] buffer, byte val, int offset)
		{
			return ExBuffer.IndexOf(buffer, val, offset, buffer.Length - offset);
		}

		// Token: 0x0600259D RID: 9629 RVA: 0x0004F10C File Offset: 0x0004D30C
		private void LogEvent(ProtocolLoggingLevel loggingLevel, ProtocolEvent eventId, byte[] data, string context)
		{
			if (this.protocolLog.LoggingLevel < loggingLevel)
			{
				return;
			}
			this.row[4] = eventId;
			this.row[6] = context;
			int num = 0;
			byte[] line;
			do
			{
				line = ProtocolLogSession.GetLine(data, num);
				if (line != null)
				{
					num += line.Length + 2;
				}
				if (line != null || (line == null && num == 0))
				{
					this.row[5] = line;
					this.protocolLog.Append(this.row);
				}
			}
			while (line != null);
			this.row[5] = null;
		}

		// Token: 0x040022E2 RID: 8930
		private ProtocolLog protocolLog;

		// Token: 0x040022E3 RID: 8931
		private LogRowFormatter row;
	}
}
