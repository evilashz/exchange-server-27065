using System;
using Microsoft.Exchange.Data;

namespace Microsoft.Exchange.Hygiene.Data.Spam
{
	// Token: 0x020001DD RID: 477
	internal class MaxProcessorID : ConfigurablePropertyBag
	{
		// Token: 0x1700062C RID: 1580
		// (get) Token: 0x0600143F RID: 5183 RVA: 0x00040B58 File Offset: 0x0003ED58
		public override ObjectId Identity
		{
			get
			{
				return new ConfigObjectId(Guid.NewGuid().ToString());
			}
		}

		// Token: 0x1700062D RID: 1581
		// (get) Token: 0x06001440 RID: 5184 RVA: 0x00040B7D File Offset: 0x0003ED7D
		// (set) Token: 0x06001441 RID: 5185 RVA: 0x00040B8F File Offset: 0x0003ED8F
		public long? ProcessorID
		{
			get
			{
				return (long?)this[MaxProcessorID.ProcessorIDProperty];
			}
			set
			{
				this[MaxProcessorID.ProcessorIDProperty] = value;
			}
		}

		// Token: 0x040009F5 RID: 2549
		public static readonly HygienePropertyDefinition ProcessorIDProperty = new HygienePropertyDefinition("bi_ProcessorId", typeof(long?));
	}
}
