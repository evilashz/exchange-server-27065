using System;

namespace Microsoft.Exchange.Diagnostics.Service.ExchangeJobs.Inference
{
	// Token: 0x02000028 RID: 40
	public static class CounterNames
	{
		// Token: 0x0400011B RID: 283
		public const string PipelineObjectName = "MSExchangeInference Pipeline";

		// Token: 0x0400011C RID: 284
		public const string DeliveryStoreDriverObjectName = "MSExchange Delivery Store Driver";

		// Token: 0x0400011D RID: 285
		public const string ClassificationProcessingObjectName = "MSExchangeInference Classification Processing";

		// Token: 0x0400011E RID: 286
		public const string DeliveryStoreAgentsObjectName = "MSExchange Delivery Store Driver Agents";

		// Token: 0x0400011F RID: 287
		public const string NumberOfSucceededDocumentsCounterName = "Number Of Succeeded Documents";

		// Token: 0x04000120 RID: 288
		public const string NumberOfFailedDocumentsCounterName = "Number Of Failed Documents";

		// Token: 0x04000121 RID: 289
		public const string PipelineInstanceName = "classificationpipeline";

		// Token: 0x04000122 RID: 290
		public const string ClassificationAgentInstanceName = "inference classification agent";

		// Token: 0x04000123 RID: 291
		public const string SuccessfulDeliveriesCounterName = "SuccessfulDeliveries";

		// Token: 0x04000124 RID: 292
		public const string AgentFailureCounterName = "StoreDriverDelivery Agent Failure";

		// Token: 0x04000125 RID: 293
		public const string ItemSkippedCounterName = "Items Skipped";

		// Token: 0x04000126 RID: 294
		public const string NumberOfQuotaExceededExceptionsCounterName = "Number of Quota Exceeded Exceptions in Pipeline";

		// Token: 0x04000127 RID: 295
		public const string NumberOfTransientExceptionsCounterName = "Number of Transient Exceptions in Pipeline";
	}
}
