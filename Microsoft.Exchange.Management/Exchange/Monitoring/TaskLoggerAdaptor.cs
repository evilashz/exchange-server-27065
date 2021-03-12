using System;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Monitoring
{
	// Token: 0x0200050F RID: 1295
	internal sealed class TaskLoggerAdaptor : ChainedLogger
	{
		// Token: 0x06002E81 RID: 11905 RVA: 0x000BA276 File Offset: 0x000B8476
		internal TaskLoggerAdaptor(Task instance)
		{
			if (instance == null)
			{
				throw new ArgumentNullException("instance");
			}
			this.instance = instance;
		}

		// Token: 0x06002E82 RID: 11906 RVA: 0x000BA293 File Offset: 0x000B8493
		protected override void InternalWriteVerbose(LocalizedString message)
		{
			this.instance.WriteVerbose(message);
		}

		// Token: 0x06002E83 RID: 11907 RVA: 0x000BA2A1 File Offset: 0x000B84A1
		protected override void InternalWriteDebug(LocalizedString message)
		{
			this.instance.WriteDebug(message);
		}

		// Token: 0x06002E84 RID: 11908 RVA: 0x000BA2AF File Offset: 0x000B84AF
		protected override void InternalWriteWarning(LocalizedString message)
		{
			this.instance.WriteWarning(message);
		}

		// Token: 0x06002E85 RID: 11909 RVA: 0x000BA2BD File Offset: 0x000B84BD
		protected override string InternalGetDiagnosticInfo(string prefix)
		{
			return prefix;
		}

		// Token: 0x04002148 RID: 8520
		private readonly Task instance;
	}
}
