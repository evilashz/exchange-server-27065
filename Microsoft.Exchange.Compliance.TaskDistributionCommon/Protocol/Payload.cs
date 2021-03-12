using System;

namespace Microsoft.Exchange.Compliance.TaskDistributionCommon.Protocol
{
	// Token: 0x0200004E RID: 78
	public abstract class Payload
	{
		// Token: 0x17000083 RID: 131
		// (get) Token: 0x060001FE RID: 510 RVA: 0x0000ADC5 File Offset: 0x00008FC5
		// (set) Token: 0x060001FF RID: 511 RVA: 0x0000ADCD File Offset: 0x00008FCD
		public string PayloadId { get; set; }

		// Token: 0x06000200 RID: 512 RVA: 0x0000ADD8 File Offset: 0x00008FD8
		public virtual PayloadReference ToPayloadReference()
		{
			return new PayloadReference
			{
				PayloadId = this.PayloadId
			};
		}
	}
}
