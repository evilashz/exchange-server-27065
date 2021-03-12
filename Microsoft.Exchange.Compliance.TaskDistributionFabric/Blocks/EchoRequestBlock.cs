using System;
using Microsoft.Exchange.Compliance.TaskDistributionCommon.Protocol;

namespace Microsoft.Exchange.Compliance.TaskDistributionFabric.Blocks
{
	// Token: 0x0200000B RID: 11
	internal class EchoRequestBlock : BlockBase<ComplianceMessage, ComplianceMessage>
	{
		// Token: 0x06000035 RID: 53 RVA: 0x00002BC4 File Offset: 0x00000DC4
		public override ComplianceMessage Process(ComplianceMessage input)
		{
			return input;
		}
	}
}
