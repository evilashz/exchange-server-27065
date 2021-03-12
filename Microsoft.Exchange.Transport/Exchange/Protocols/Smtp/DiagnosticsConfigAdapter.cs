using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Transport;

namespace Microsoft.Exchange.Protocols.Smtp
{
	// Token: 0x020004CC RID: 1228
	internal class DiagnosticsConfigAdapter : IDiagnosticsConfigProvider
	{
		// Token: 0x1700112D RID: 4397
		// (get) Token: 0x0600388A RID: 14474 RVA: 0x000E85D6 File Offset: 0x000E67D6
		public TimeSpan SmtpRecvLogAsyncInterval
		{
			get
			{
				return this.appConfig.Logging.SmtpRecvLogAsyncInterval;
			}
		}

		// Token: 0x1700112E RID: 4398
		// (get) Token: 0x0600388B RID: 14475 RVA: 0x000E85E8 File Offset: 0x000E67E8
		public int SmtpRecvLogBufferSize
		{
			get
			{
				return this.appConfig.Logging.SmtpRecvLogBufferSize;
			}
		}

		// Token: 0x1700112F RID: 4399
		// (get) Token: 0x0600388C RID: 14476 RVA: 0x000E85FA File Offset: 0x000E67FA
		public TimeSpan SmtpRecvLogFlushInterval
		{
			get
			{
				return this.appConfig.Logging.SmtpRecvLogFlushInterval;
			}
		}

		// Token: 0x0600388D RID: 14477 RVA: 0x000E860C File Offset: 0x000E680C
		public static IDiagnosticsConfigProvider Create(ITransportAppConfig appConfig)
		{
			ArgumentValidator.ThrowIfNull("appConfig", appConfig);
			return new DiagnosticsConfigAdapter(appConfig);
		}

		// Token: 0x0600388E RID: 14478 RVA: 0x000E861F File Offset: 0x000E681F
		private DiagnosticsConfigAdapter(ITransportAppConfig appConfig)
		{
			this.appConfig = appConfig;
		}

		// Token: 0x04001CF6 RID: 7414
		private readonly ITransportAppConfig appConfig;
	}
}
