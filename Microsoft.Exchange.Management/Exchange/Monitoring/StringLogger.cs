using System;
using System.Text;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Monitoring
{
	// Token: 0x02000510 RID: 1296
	internal sealed class StringLogger : ChainedLogger
	{
		// Token: 0x06002E86 RID: 11910 RVA: 0x000BA2C0 File Offset: 0x000B84C0
		internal StringLogger()
		{
			this.instance = new StringBuilder();
		}

		// Token: 0x06002E87 RID: 11911 RVA: 0x000BA2D3 File Offset: 0x000B84D3
		protected override void InternalWriteVerbose(LocalizedString message)
		{
			this.WriteLine(Strings.Verbose, message);
		}

		// Token: 0x06002E88 RID: 11912 RVA: 0x000BA2E1 File Offset: 0x000B84E1
		protected override void InternalWriteDebug(LocalizedString message)
		{
			this.WriteLine(Strings.Debug, message);
		}

		// Token: 0x06002E89 RID: 11913 RVA: 0x000BA2EF File Offset: 0x000B84EF
		protected override void InternalWriteWarning(LocalizedString message)
		{
			this.WriteLine(Strings.Warning, message);
		}

		// Token: 0x06002E8A RID: 11914 RVA: 0x000BA2FD File Offset: 0x000B84FD
		private void WriteLine(LocalizedString prefix, LocalizedString message)
		{
			this.instance.AppendLine(string.Format("{0}{1}", prefix, message));
		}

		// Token: 0x06002E8B RID: 11915 RVA: 0x000BA321 File Offset: 0x000B8521
		protected override string InternalGetDiagnosticInfo(string prefix)
		{
			return string.Format("{0}{1}{2}", prefix, Environment.NewLine, this.instance);
		}

		// Token: 0x04002149 RID: 8521
		private readonly StringBuilder instance;
	}
}
