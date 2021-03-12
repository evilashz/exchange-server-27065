using System;
using System.Globalization;
using System.Net;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using Microsoft.Exchange.Conversion;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Transport.Smtp;
using Microsoft.Exchange.Net;
using Microsoft.Exchange.Security.Cryptography.X509Certificates;

namespace Microsoft.Exchange.Transport.Logging
{
	// Token: 0x02000085 RID: 133
	internal class ProtocolLogSession : IProtocolLogSession
	{
		// Token: 0x060003BD RID: 957 RVA: 0x0001079D File Offset: 0x0000E99D
		internal ProtocolLogSession(ProtocolLog protocolLog, ProtocolLogRowFormatter row, ProtocolLoggingLevel loggingLevel)
		{
			this.protocolLog = protocolLog;
			this.row = row;
			this.loggingLevel = loggingLevel;
			this.row[3] = 0;
		}

		// Token: 0x170000E9 RID: 233
		// (set) Token: 0x060003BE RID: 958 RVA: 0x000107D7 File Offset: 0x0000E9D7
		public IPEndPoint LocalEndPoint
		{
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("value", "Cannot set local endpoint to null after initialization");
				}
				if (this.row[4] != null)
				{
					throw new InvalidOperationException("Cannot change local endpoint once it is set");
				}
				this.row[4] = value;
			}
		}

		// Token: 0x170000EA RID: 234
		// (get) Token: 0x060003BF RID: 959 RVA: 0x00010812 File Offset: 0x0000EA12
		// (set) Token: 0x060003C0 RID: 960 RVA: 0x0001081A File Offset: 0x0000EA1A
		public ProtocolLoggingLevel ProtocolLoggingLevel
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

		// Token: 0x060003C1 RID: 961 RVA: 0x00010823 File Offset: 0x0000EA23
		public void LogConnect()
		{
			this.LogEvent(ProtocolLoggingLevel.Verbose, ProtocolEvent.Connect, null, null);
		}

		// Token: 0x060003C2 RID: 962 RVA: 0x0001082F File Offset: 0x0000EA2F
		public void LogDisconnect(DisconnectReason reason)
		{
			this.LogDisconnect(reason, null);
		}

		// Token: 0x060003C3 RID: 963 RVA: 0x0001083C File Offset: 0x0000EA3C
		public void LogDisconnect(DisconnectReason reason, string remoteError)
		{
			string text = reason.ToString();
			if (!string.IsNullOrEmpty(remoteError))
			{
				text += string.Format("({0})", remoteError);
			}
			this.LogEvent(ProtocolLoggingLevel.Verbose, ProtocolEvent.Disconnect, null, text);
		}

		// Token: 0x060003C4 RID: 964 RVA: 0x00010879 File Offset: 0x0000EA79
		public void LogSend(byte[] data)
		{
			this.LogEvent(ProtocolLoggingLevel.Verbose, ProtocolEvent.Send, data, null);
		}

		// Token: 0x060003C5 RID: 965 RVA: 0x00010885 File Offset: 0x0000EA85
		public void LogReceive(byte[] data)
		{
			this.LogEvent(ProtocolLoggingLevel.Verbose, ProtocolEvent.Receive, data, null);
		}

		// Token: 0x060003C6 RID: 966 RVA: 0x00010894 File Offset: 0x0000EA94
		public void LogInformation(ProtocolLoggingLevel level, byte[] data, string formatString, params object[] parameterList)
		{
			if (this.loggingLevel < level)
			{
				return;
			}
			string context = string.Format(CultureInfo.InvariantCulture, formatString, parameterList);
			this.LogEvent(this.loggingLevel, ProtocolEvent.Information, data, context);
		}

		// Token: 0x060003C7 RID: 967 RVA: 0x000108C8 File Offset: 0x0000EAC8
		public void LogInformation(ProtocolLoggingLevel level, byte[] data, string context)
		{
			this.LogEvent(level, ProtocolEvent.Information, data, context);
		}

		// Token: 0x060003C8 RID: 968 RVA: 0x000108D4 File Offset: 0x0000EAD4
		public void LogCertificate(string type, IX509Certificate2 cert)
		{
			if (cert != null)
			{
				this.LogCertificate(type, cert.Certificate);
			}
		}

		// Token: 0x060003C9 RID: 969 RVA: 0x000108E8 File Offset: 0x0000EAE8
		public void LogCertificate(string type, X509Certificate2 cert)
		{
			if (cert == null)
			{
				return;
			}
			this.LogInformation(ProtocolLoggingLevel.Verbose, null, type);
			this.LogStringInformation(ProtocolLoggingLevel.Verbose, cert.Subject, "Certificate subject");
			this.LogStringInformation(ProtocolLoggingLevel.Verbose, cert.IssuerName.Name, "Certificate issuer name");
			this.LogStringInformation(ProtocolLoggingLevel.Verbose, cert.SerialNumber, "Certificate serial number");
			this.LogStringInformation(ProtocolLoggingLevel.Verbose, cert.Thumbprint, "Certificate thumbprint");
			StringBuilder stringBuilder = new StringBuilder(256);
			try
			{
				foreach (string value in TlsCertificateInfo.GetFQDNs(cert))
				{
					if (stringBuilder.Length != 0)
					{
						stringBuilder.Append(';');
					}
					stringBuilder.Append(value);
				}
			}
			catch (CryptographicException ex)
			{
				this.LogInformation(ProtocolLoggingLevel.Verbose, null, "CryptographicException was thrown while attempting to get certificate alternate names: {0}", new object[]
				{
					ex.Message
				});
			}
			this.LogStringInformation(ProtocolLoggingLevel.Verbose, stringBuilder.ToString(), "Certificate alternate names");
		}

		// Token: 0x060003CA RID: 970 RVA: 0x000109F0 File Offset: 0x0000EBF0
		public void LogCertificateThumbprint(string type, IX509Certificate2 cert)
		{
			if (cert != null)
			{
				this.LogCertificateThumbprint(type, cert.Certificate);
			}
		}

		// Token: 0x060003CB RID: 971 RVA: 0x00010A02 File Offset: 0x0000EC02
		public void LogCertificateThumbprint(string type, X509Certificate2 cert)
		{
			if (cert == null)
			{
				return;
			}
			this.LogInformation(ProtocolLoggingLevel.Verbose, null, type);
			this.LogStringInformation(ProtocolLoggingLevel.Verbose, cert.Thumbprint, "Certificate thumbprint");
		}

		// Token: 0x060003CC RID: 972 RVA: 0x00010A24 File Offset: 0x0000EC24
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

		// Token: 0x060003CD RID: 973 RVA: 0x00010A96 File Offset: 0x0000EC96
		private static int IndexOf(byte[] buffer, byte val, int offset)
		{
			return ExBuffer.IndexOf(buffer, val, offset, buffer.Length - offset);
		}

		// Token: 0x060003CE RID: 974 RVA: 0x00010AA8 File Offset: 0x0000ECA8
		private void LogEvent(ProtocolLoggingLevel level, ProtocolEvent eventId, byte[] data, string context)
		{
			if (this.loggingLevel < level)
			{
				return;
			}
			lock (this.loggingLock)
			{
				this.row[6] = eventId;
				this.row[8] = context;
				int num = 0;
				byte[] line;
				do
				{
					line = ProtocolLogSession.GetLine(data, num);
					if (line != null)
					{
						num += line.Length + 2;
					}
					if (line != null || num == 0)
					{
						this.row[7] = line;
						this.protocolLog.Append(this.row);
						this.row[3] = (int)this.row[3] + 1;
					}
				}
				while (line != null);
				this.row[7] = null;
			}
		}

		// Token: 0x060003CF RID: 975 RVA: 0x00010B7C File Offset: 0x0000ED7C
		private void LogStringInformation(ProtocolLoggingLevel level, string data, string context)
		{
			this.LogEvent(level, ProtocolEvent.Information, Encoding.UTF8.GetBytes(data), context);
		}

		// Token: 0x04000237 RID: 567
		private readonly ProtocolLog protocolLog;

		// Token: 0x04000238 RID: 568
		private readonly ProtocolLogRowFormatter row;

		// Token: 0x04000239 RID: 569
		private ProtocolLoggingLevel loggingLevel;

		// Token: 0x0400023A RID: 570
		private readonly object loggingLock = new object();
	}
}
