using System;
using System.Collections.Generic;
using Microsoft.Exchange.Compliance.TaskDistributionCommon.Protocol;

namespace Microsoft.Exchange.Compliance.TaskDistributionCommon.Resolver
{
	// Token: 0x0200005A RID: 90
	internal interface ITargetResolver
	{
		// Token: 0x060002A4 RID: 676
		IEnumerable<ComplianceMessage> Resolve(IEnumerable<ComplianceMessage> sources);
	}
}
