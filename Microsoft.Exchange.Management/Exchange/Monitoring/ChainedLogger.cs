using System;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Monitoring
{
	// Token: 0x0200050E RID: 1294
	internal abstract class ChainedLogger
	{
		// Token: 0x06002E77 RID: 11895 RVA: 0x000BA1BD File Offset: 0x000B83BD
		public void WriteVerbose(LocalizedString message)
		{
			this.InternalWriteVerbose(message);
			if (this.nextLogger != null)
			{
				this.nextLogger.WriteVerbose(message);
			}
		}

		// Token: 0x06002E78 RID: 11896 RVA: 0x000BA1DA File Offset: 0x000B83DA
		public void WriteDebug(LocalizedString message)
		{
			this.InternalWriteDebug(message);
			if (this.nextLogger != null)
			{
				this.nextLogger.WriteDebug(message);
			}
		}

		// Token: 0x06002E79 RID: 11897 RVA: 0x000BA1F7 File Offset: 0x000B83F7
		public void WriteWarning(LocalizedString message)
		{
			this.InternalWriteWarning(message);
			if (this.nextLogger != null)
			{
				this.nextLogger.WriteWarning(message);
			}
		}

		// Token: 0x06002E7A RID: 11898 RVA: 0x000BA214 File Offset: 0x000B8414
		public void Append(ChainedLogger nextLogger)
		{
			if (this.nextLogger == null)
			{
				this.nextLogger = nextLogger;
				return;
			}
			this.nextLogger.Append(nextLogger);
		}

		// Token: 0x06002E7B RID: 11899 RVA: 0x000BA234 File Offset: 0x000B8434
		public string GetDiagnosticInfo(string prefix)
		{
			string text = this.InternalGetDiagnosticInfo(prefix);
			if (this.nextLogger != null)
			{
				text = string.Format("{0}{1}", text, this.nextLogger.GetDiagnosticInfo(string.Empty));
			}
			return text;
		}

		// Token: 0x06002E7C RID: 11900
		protected abstract void InternalWriteVerbose(LocalizedString message);

		// Token: 0x06002E7D RID: 11901
		protected abstract void InternalWriteDebug(LocalizedString message);

		// Token: 0x06002E7E RID: 11902
		protected abstract void InternalWriteWarning(LocalizedString message);

		// Token: 0x06002E7F RID: 11903
		protected abstract string InternalGetDiagnosticInfo(string prefix);

		// Token: 0x04002147 RID: 8519
		private ChainedLogger nextLogger;
	}
}
