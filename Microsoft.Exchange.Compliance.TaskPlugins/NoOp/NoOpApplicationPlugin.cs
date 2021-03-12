using System;
using Microsoft.Exchange.Compliance.TaskDistributionCommon.Extensibility;
using Microsoft.Exchange.Compliance.TaskDistributionCommon.Protocol;

namespace Microsoft.Exchange.Compliance.TaskPlugins.NoOp
{
	// Token: 0x02000007 RID: 7
	internal class NoOpApplicationPlugin : IApplicationPlugin
	{
		// Token: 0x0600000F RID: 15 RVA: 0x00002A19 File Offset: 0x00000C19
		public WorkPayload Preprocess(ComplianceMessage target, WorkPayload payload)
		{
			return payload;
		}

		// Token: 0x06000010 RID: 16 RVA: 0x00002A1C File Offset: 0x00000C1C
		public WorkPayload DoWork(ComplianceMessage target, WorkPayload payload)
		{
			return payload;
		}

		// Token: 0x06000011 RID: 17 RVA: 0x00002A1F File Offset: 0x00000C1F
		public ResultBase RecordResult(ResultBase existing, WorkPayload addition)
		{
			return existing;
		}
	}
}
