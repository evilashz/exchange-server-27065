using System;

namespace Microsoft.Exchange.Compliance.TaskDistributionCommon.Protocol
{
	// Token: 0x02000055 RID: 85
	public enum ComplianceMessageType : byte
	{
		// Token: 0x040001D8 RID: 472
		None,
		// Token: 0x040001D9 RID: 473
		StartWork,
		// Token: 0x040001DA RID: 474
		StartJob,
		// Token: 0x040001DB RID: 475
		RecordResult,
		// Token: 0x040001DC RID: 476
		Status,
		// Token: 0x040001DD RID: 477
		RetrieveRequest,
		// Token: 0x040001DE RID: 478
		EchoRequest
	}
}
