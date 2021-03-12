using System;
using Microsoft.Exchange.Data;

namespace Microsoft.Exchange.MailboxTransport.StoreDriverDelivery.Agents
{
	// Token: 0x02000078 RID: 120
	internal interface IDeliveryClassificationStrategy
	{
		// Token: 0x06000449 RID: 1097
		void ApplyClassification(StoreDriverDeliveryEventArgsImpl argsImpl, InferenceClassificationResult result);
	}
}
