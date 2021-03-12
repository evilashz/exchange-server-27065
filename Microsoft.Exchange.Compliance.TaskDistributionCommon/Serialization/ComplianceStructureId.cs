using System;

namespace Microsoft.Exchange.Compliance.TaskDistributionCommon.Serialization
{
	// Token: 0x02000065 RID: 101
	public enum ComplianceStructureId : byte
	{
		// Token: 0x0400023D RID: 573
		MessageEnvelope = 1,
		// Token: 0x0400023E RID: 574
		Target,
		// Token: 0x0400023F RID: 575
		Payload,
		// Token: 0x04000240 RID: 576
		WorkPayload,
		// Token: 0x04000241 RID: 577
		DistributionPayload,
		// Token: 0x04000242 RID: 578
		AggregationPayload,
		// Token: 0x04000243 RID: 579
		StatusPayload,
		// Token: 0x04000244 RID: 580
		ComplianceSearchCondition,
		// Token: 0x04000245 RID: 581
		ComplianceSearchData,
		// Token: 0x04000246 RID: 582
		ComplianceSearch,
		// Token: 0x04000247 RID: 583
		FaultRecord,
		// Token: 0x04000248 RID: 584
		FaultDefinition,
		// Token: 0x04000249 RID: 585
		GeneralStructure = 98,
		// Token: 0x0400024A RID: 586
		ApplicationPayload
	}
}
