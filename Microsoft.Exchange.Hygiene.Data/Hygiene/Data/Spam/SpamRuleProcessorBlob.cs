using System;
using Microsoft.Exchange.Data;

namespace Microsoft.Exchange.Hygiene.Data.Spam
{
	// Token: 0x02000209 RID: 521
	internal class SpamRuleProcessorBlob : ConfigurablePropertyBag
	{
		// Token: 0x170006CE RID: 1742
		// (get) Token: 0x060015B0 RID: 5552 RVA: 0x00043637 File Offset: 0x00041837
		public override ObjectId Identity
		{
			get
			{
				return new ConfigObjectId(string.Format("{0}", this.ProcessorId));
			}
		}

		// Token: 0x170006CF RID: 1743
		// (get) Token: 0x060015B1 RID: 5553 RVA: 0x0004364E File Offset: 0x0004184E
		// (set) Token: 0x060015B2 RID: 5554 RVA: 0x00043660 File Offset: 0x00041860
		public Guid Id
		{
			get
			{
				return (Guid)this[SpamRuleProcessorBlobSchema.IdProperty];
			}
			set
			{
				this[SpamRuleProcessorBlobSchema.IdProperty] = value;
			}
		}

		// Token: 0x170006D0 RID: 1744
		// (get) Token: 0x060015B3 RID: 5555 RVA: 0x00043673 File Offset: 0x00041873
		// (set) Token: 0x060015B4 RID: 5556 RVA: 0x00043685 File Offset: 0x00041885
		public string ProcessorId
		{
			get
			{
				return this[SpamRuleProcessorBlobSchema.ProcessorIdProperty] as string;
			}
			set
			{
				this[SpamRuleProcessorBlobSchema.ProcessorIdProperty] = value;
			}
		}

		// Token: 0x170006D1 RID: 1745
		// (get) Token: 0x060015B5 RID: 5557 RVA: 0x00043693 File Offset: 0x00041893
		// (set) Token: 0x060015B6 RID: 5558 RVA: 0x000436A5 File Offset: 0x000418A5
		public string Data
		{
			get
			{
				return this[SpamRuleProcessorBlobSchema.DataProperty] as string;
			}
			set
			{
				this[SpamRuleProcessorBlobSchema.DataProperty] = value;
			}
		}

		// Token: 0x170006D2 RID: 1746
		// (get) Token: 0x060015B7 RID: 5559 RVA: 0x000436B3 File Offset: 0x000418B3
		// (set) Token: 0x060015B8 RID: 5560 RVA: 0x000436CA File Offset: 0x000418CA
		public DateTime? CreatedDatetime
		{
			get
			{
				return this[SpamRuleBlobSchema.CreatedDatetimeProperty] as DateTime?;
			}
			set
			{
				this[SpamRuleBlobSchema.CreatedDatetimeProperty] = value;
			}
		}

		// Token: 0x170006D3 RID: 1747
		// (get) Token: 0x060015B9 RID: 5561 RVA: 0x000436DD File Offset: 0x000418DD
		// (set) Token: 0x060015BA RID: 5562 RVA: 0x000436F4 File Offset: 0x000418F4
		public DateTime? ChangeDatetime
		{
			get
			{
				return this[SpamRuleBlobSchema.ChangedDatetimeProperty] as DateTime?;
			}
			set
			{
				this[SpamRuleBlobSchema.ChangedDatetimeProperty] = value;
			}
		}

		// Token: 0x170006D4 RID: 1748
		// (get) Token: 0x060015BB RID: 5563 RVA: 0x00043707 File Offset: 0x00041907
		// (set) Token: 0x060015BC RID: 5564 RVA: 0x0004371E File Offset: 0x0004191E
		public DateTime? DeletedDatetime
		{
			get
			{
				return this[SpamRuleBlobSchema.DeletedDatetimeProperty] as DateTime?;
			}
			set
			{
				this[SpamRuleBlobSchema.DeletedDatetimeProperty] = value;
			}
		}

		// Token: 0x060015BD RID: 5565 RVA: 0x00043731 File Offset: 0x00041931
		public override Type GetSchemaType()
		{
			return typeof(SpamRuleProcessorBlobSchema);
		}
	}
}
