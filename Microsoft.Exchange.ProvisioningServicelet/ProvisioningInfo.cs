using System;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.Migration;

namespace Microsoft.Exchange.Servicelets.Provisioning
{
	// Token: 0x0200000D RID: 13
	internal struct ProvisioningInfo
	{
		// Token: 0x06000067 RID: 103 RVA: 0x00004C2C File Offset: 0x00002E2C
		public ProvisioningInfo(ObjectId itemId, Guid jobId, IProvisioningData data)
		{
			this.ItemId = itemId;
			this.JobId = jobId;
			this.Data = data;
			this.Worker = null;
			this.Canceled = false;
			this.TimesAttempted = 0;
			this.LastAttempted = ExDateTime.Now;
		}

		// Token: 0x04000052 RID: 82
		public readonly ObjectId ItemId;

		// Token: 0x04000053 RID: 83
		public readonly Guid JobId;

		// Token: 0x04000054 RID: 84
		public readonly IProvisioningData Data;

		// Token: 0x04000055 RID: 85
		public ProvisioningWorker Worker;

		// Token: 0x04000056 RID: 86
		public bool Canceled;

		// Token: 0x04000057 RID: 87
		public int TimesAttempted;

		// Token: 0x04000058 RID: 88
		public ExDateTime LastAttempted;
	}
}
