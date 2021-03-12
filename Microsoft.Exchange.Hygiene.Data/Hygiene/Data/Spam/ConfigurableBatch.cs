using System;
using Microsoft.Exchange.Data;

namespace Microsoft.Exchange.Hygiene.Data.Spam
{
	// Token: 0x020001D2 RID: 466
	internal class ConfigurableBatch : ConfigurablePropertyBag
	{
		// Token: 0x1700060C RID: 1548
		// (get) Token: 0x06001389 RID: 5001 RVA: 0x0003ADFC File Offset: 0x00038FFC
		public override ObjectId Identity
		{
			get
			{
				return new ConfigObjectId(Guid.NewGuid().ToString());
			}
		}

		// Token: 0x1700060D RID: 1549
		// (get) Token: 0x0600138A RID: 5002 RVA: 0x0003AE21 File Offset: 0x00039021
		// (set) Token: 0x0600138B RID: 5003 RVA: 0x0003AE33 File Offset: 0x00039033
		public BatchPropertyTable Batch
		{
			get
			{
				return (BatchPropertyTable)this[ConfigurableBatch.BatchProp];
			}
			set
			{
				this[ConfigurableBatch.BatchProp] = value;
			}
		}

		// Token: 0x04000964 RID: 2404
		public static readonly HygienePropertyDefinition BatchProp = new HygienePropertyDefinition("batch", typeof(BatchPropertyTable));
	}
}
