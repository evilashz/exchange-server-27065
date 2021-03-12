using System;
using System.Collections.Generic;
using Microsoft.Exchange.Compliance.TaskDistributionCommon.Protocol;
using Microsoft.Exchange.Compliance.TaskDistributionCommon.Serialization;

namespace Microsoft.Exchange.Compliance.TaskDistributionFabric.Payload
{
	// Token: 0x02000016 RID: 22
	internal interface IPayloadRetriever
	{
		// Token: 0x06000059 RID: 89
		bool TryGetPayload<T>(ComplianceSerializationDescription<T> description, byte[] blob, out T payload, out FaultDefinition faultDefinition) where T : Payload, new();

		// Token: 0x0600005A RID: 90
		IEnumerable<T> GetAllPayloads<T>(ComplianceSerializationDescription<T> description, byte[] blob, out FaultDefinition faultDefinition) where T : Payload, new();
	}
}
