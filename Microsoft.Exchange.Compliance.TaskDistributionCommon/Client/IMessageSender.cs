using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Exchange.Compliance.TaskDistributionCommon.Protocol;

namespace Microsoft.Exchange.Compliance.TaskDistributionCommon.Client
{
	// Token: 0x02000004 RID: 4
	public interface IMessageSender
	{
		// Token: 0x06000001 RID: 1
		Task<bool[]> SendMessageAsync(IEnumerable<ComplianceMessage> messages);
	}
}
