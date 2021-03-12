using System;
using System.Collections.Generic;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using Microsoft.Exchange.Security.Authorization;

namespace Microsoft.Exchange.Data.Transport.Smtp
{
	// Token: 0x0200002A RID: 42
	public abstract class SmtpSession
	{
		// Token: 0x060000E5 RID: 229 RVA: 0x00005CBB File Offset: 0x00003EBB
		internal SmtpSession()
		{
		}

		// Token: 0x1700001D RID: 29
		// (get) Token: 0x060000E6 RID: 230
		// (set) Token: 0x060000E7 RID: 231
		public abstract string HelloDomain { get; internal set; }

		// Token: 0x1700001E RID: 30
		// (get) Token: 0x060000E8 RID: 232
		public abstract IPEndPoint LocalEndPoint { get; }

		// Token: 0x1700001F RID: 31
		// (get) Token: 0x060000E9 RID: 233
		// (set) Token: 0x060000EA RID: 234
		public abstract IPEndPoint RemoteEndPoint { get; internal set; }

		// Token: 0x17000020 RID: 32
		// (get) Token: 0x060000EB RID: 235
		public abstract IDictionary<string, object> Properties { get; }

		// Token: 0x17000021 RID: 33
		// (get) Token: 0x060000EC RID: 236
		public abstract long SessionId { get; }

		// Token: 0x17000022 RID: 34
		// (get) Token: 0x060000ED RID: 237
		public abstract bool IsConnected { get; }

		// Token: 0x17000023 RID: 35
		// (get) Token: 0x060000EE RID: 238
		// (set) Token: 0x060000EF RID: 239
		public abstract IPAddress LastExternalIPAddress { get; internal set; }

		// Token: 0x17000024 RID: 36
		// (get) Token: 0x060000F0 RID: 240
		public abstract AuthenticationSource AuthenticationSource { get; }

		// Token: 0x17000025 RID: 37
		// (get) Token: 0x060000F1 RID: 241
		public abstract bool AntispamBypass { get; }

		// Token: 0x17000026 RID: 38
		// (get) Token: 0x060000F2 RID: 242
		// (set) Token: 0x060000F3 RID: 243
		public abstract bool IsExternalConnection { get; internal set; }

		// Token: 0x17000027 RID: 39
		// (get) Token: 0x060000F4 RID: 244
		public abstract bool IsTls { get; }

		// Token: 0x17000028 RID: 40
		// (get) Token: 0x060000F5 RID: 245
		internal abstract bool DiscardingMessage { get; }

		// Token: 0x17000029 RID: 41
		// (get) Token: 0x060000F6 RID: 246
		// (set) Token: 0x060000F7 RID: 247
		internal abstract bool ShouldDisconnect { get; set; }

		// Token: 0x1700002A RID: 42
		// (get) Token: 0x060000F8 RID: 248
		// (set) Token: 0x060000F9 RID: 249
		internal abstract bool IsInboundProxiedSession { get; set; }

		// Token: 0x1700002B RID: 43
		// (get) Token: 0x060000FA RID: 250
		// (set) Token: 0x060000FB RID: 251
		internal abstract bool IsClientProxiedSession { get; set; }

		// Token: 0x1700002C RID: 44
		// (get) Token: 0x060000FC RID: 252
		internal abstract bool XAttrAdvertised { get; }

		// Token: 0x1700002D RID: 45
		// (get) Token: 0x060000FD RID: 253
		internal abstract string ReceiveConnectorName { get; }

		// Token: 0x1700002E RID: 46
		// (get) Token: 0x060000FE RID: 254
		internal abstract X509Certificate2 TlsRemoteCertificate { get; }

		// Token: 0x1700002F RID: 47
		// (get) Token: 0x060000FF RID: 255
		// (set) Token: 0x06000100 RID: 256
		internal abstract SmtpResponse Banner { get; set; }

		// Token: 0x17000030 RID: 48
		// (get) Token: 0x06000101 RID: 257
		// (set) Token: 0x06000102 RID: 258
		internal abstract SmtpResponse SmtpResponse { get; set; }

		// Token: 0x17000031 RID: 49
		// (get) Token: 0x06000103 RID: 259
		// (set) Token: 0x06000104 RID: 260
		internal abstract DisconnectReason DisconnectReason { get; set; }

		// Token: 0x17000032 RID: 50
		// (get) Token: 0x06000105 RID: 261
		// (set) Token: 0x06000106 RID: 262
		internal abstract IExecutionControl ExecutionControl { get; set; }

		// Token: 0x17000033 RID: 51
		// (get) Token: 0x06000107 RID: 263
		internal abstract string CurrentMessageTemporaryId { get; }

		// Token: 0x17000034 RID: 52
		// (get) Token: 0x06000108 RID: 264
		// (set) Token: 0x06000109 RID: 265
		internal abstract bool DisableStartTls { get; set; }

		// Token: 0x17000035 RID: 53
		// (get) Token: 0x0600010A RID: 266
		// (set) Token: 0x0600010B RID: 267
		internal abstract bool RequestClientTlsCertificate { get; set; }

		// Token: 0x0600010C RID: 268
		internal abstract void RejectMessage(SmtpResponse response);

		// Token: 0x0600010D RID: 269
		internal abstract void RejectMessage(SmtpResponse response, string trackingContext);

		// Token: 0x0600010E RID: 270
		internal abstract void DiscardMessage(SmtpResponse response, string trackingContext);

		// Token: 0x0600010F RID: 271
		internal abstract void Disconnect();

		// Token: 0x06000110 RID: 272
		internal abstract CertificateValidationStatus ValidateCertificate();

		// Token: 0x06000111 RID: 273
		internal abstract CertificateValidationStatus ValidateCertificate(string domain, out string matchedCertDomain);

		// Token: 0x06000112 RID: 274
		internal abstract void GrantMailItemPermissions(Permission permissionsToGrant);
	}
}
