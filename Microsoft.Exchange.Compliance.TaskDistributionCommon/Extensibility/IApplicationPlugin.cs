using System;
using Microsoft.Exchange.Compliance.TaskDistributionCommon.Protocol;

namespace Microsoft.Exchange.Compliance.TaskDistributionCommon.Extensibility
{
	// Token: 0x02000044 RID: 68
	internal interface IApplicationPlugin
	{
		// Token: 0x0600019B RID: 411
		WorkPayload Preprocess(ComplianceMessage target, WorkPayload payload);

		// Token: 0x0600019C RID: 412
		WorkPayload DoWork(ComplianceMessage target, WorkPayload payload);

		// Token: 0x0600019D RID: 413
		ResultBase RecordResult(ResultBase existing, WorkPayload addition);
	}
}
