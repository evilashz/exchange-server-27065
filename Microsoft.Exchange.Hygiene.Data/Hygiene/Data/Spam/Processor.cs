using System;
using Microsoft.Exchange.Data;

namespace Microsoft.Exchange.Hygiene.Data.Spam
{
	// Token: 0x020001E5 RID: 485
	internal class Processor : ConfigurablePropertyTable
	{
		// Token: 0x17000642 RID: 1602
		// (get) Token: 0x06001472 RID: 5234 RVA: 0x000410C8 File Offset: 0x0003F2C8
		public override ObjectId Identity
		{
			get
			{
				return new ConfigObjectId(Guid.NewGuid().ToString());
			}
		}

		// Token: 0x17000643 RID: 1603
		// (get) Token: 0x06001473 RID: 5235 RVA: 0x000410ED File Offset: 0x0003F2ED
		// (set) Token: 0x06001474 RID: 5236 RVA: 0x000410FF File Offset: 0x0003F2FF
		public long? ProcessorID
		{
			get
			{
				return (long?)this[Processor.ProcessorIDProperty];
			}
			set
			{
				this[Processor.ProcessorIDProperty] = value;
			}
		}

		// Token: 0x04000A14 RID: 2580
		public static readonly HygienePropertyDefinition ProcessorIDProperty = new HygienePropertyDefinition("bi_ProcessorId", typeof(long?));
	}
}
