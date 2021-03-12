using System;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Management.Deployment;
using Microsoft.Exchange.Management.Hybrid;

namespace Microsoft.Exchange.Setup.Common
{
	// Token: 0x02000006 RID: 6
	public class BridgeLogger : ILogger, IDisposable
	{
		// Token: 0x06000017 RID: 23 RVA: 0x0000234C File Offset: 0x0000054C
		public BridgeLogger(ISetupLogger setupLogger)
		{
			if (setupLogger == null)
			{
				throw new ArgumentNullException();
			}
			this.logger = setupLogger;
		}

		// Token: 0x06000018 RID: 24 RVA: 0x00002364 File Offset: 0x00000564
		public void Log(LocalizedString text)
		{
			this.logger.Log(text);
		}

		// Token: 0x06000019 RID: 25 RVA: 0x00002372 File Offset: 0x00000572
		public void Log(Exception e)
		{
			this.logger.LogError(e);
		}

		// Token: 0x0600001A RID: 26 RVA: 0x00002380 File Offset: 0x00000580
		public void LogError(string text)
		{
			this.logger.Log(new LocalizedString(text));
		}

		// Token: 0x0600001B RID: 27 RVA: 0x00002393 File Offset: 0x00000593
		public void LogWarning(string text)
		{
			this.logger.Log(new LocalizedString(text));
		}

		// Token: 0x0600001C RID: 28 RVA: 0x000023A6 File Offset: 0x000005A6
		public void LogInformation(string text)
		{
			this.logger.Log(new LocalizedString(text));
		}

		// Token: 0x0600001D RID: 29 RVA: 0x000023B9 File Offset: 0x000005B9
		public void Dispose()
		{
		}

		// Token: 0x04000008 RID: 8
		private ISetupLogger logger;
	}
}
