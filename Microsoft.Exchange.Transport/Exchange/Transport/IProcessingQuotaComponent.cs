using System;

namespace Microsoft.Exchange.Transport
{
	// Token: 0x02000342 RID: 834
	internal interface IProcessingQuotaComponent : ITransportComponent
	{
		// Token: 0x060023FB RID: 9211
		ProcessingQuotaComponent.ProcessingData GetQuotaOverride(Guid externalOrgId);

		// Token: 0x060023FC RID: 9212
		ProcessingQuotaComponent.ProcessingData GetQuotaOverride(WaitCondition condition);

		// Token: 0x060023FD RID: 9213
		void SetLoadTimeDependencies(TransportAppConfig.IProcessingQuotaConfig processingQuota);

		// Token: 0x060023FE RID: 9214
		void TimedUpdate();
	}
}
