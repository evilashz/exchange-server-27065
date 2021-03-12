using System;
using Microsoft.Exchange.Data;

namespace Microsoft.Exchange.Hygiene.Data.Spam
{
	// Token: 0x02000203 RID: 515
	internal class SpamRuleBlob : ConfigurablePropertyBag
	{
		// Token: 0x170006B7 RID: 1719
		// (get) Token: 0x0600157A RID: 5498 RVA: 0x000430CA File Offset: 0x000412CA
		public override ObjectId Identity
		{
			get
			{
				return new ConfigObjectId(string.Format("{0}\\{1}", this.RuleId, this.ScopeId));
			}
		}

		// Token: 0x170006B8 RID: 1720
		// (get) Token: 0x0600157B RID: 5499 RVA: 0x000430F1 File Offset: 0x000412F1
		// (set) Token: 0x0600157C RID: 5500 RVA: 0x00043103 File Offset: 0x00041303
		public Guid Id
		{
			get
			{
				return (Guid)this[SpamRuleBlobSchema.IdProperty];
			}
			set
			{
				this[SpamRuleBlobSchema.IdProperty] = value;
			}
		}

		// Token: 0x170006B9 RID: 1721
		// (get) Token: 0x0600157D RID: 5501 RVA: 0x00043116 File Offset: 0x00041316
		// (set) Token: 0x0600157E RID: 5502 RVA: 0x00043128 File Offset: 0x00041328
		public long RuleId
		{
			get
			{
				return (long)this[SpamRuleBlobSchema.RuleIdProperty];
			}
			set
			{
				this[SpamRuleBlobSchema.RuleIdProperty] = value;
			}
		}

		// Token: 0x170006BA RID: 1722
		// (get) Token: 0x0600157F RID: 5503 RVA: 0x0004313B File Offset: 0x0004133B
		// (set) Token: 0x06001580 RID: 5504 RVA: 0x0004314D File Offset: 0x0004134D
		public long GroupId
		{
			get
			{
				return (long)this[SpamRuleBlobSchema.GroupIdProperty];
			}
			set
			{
				this[SpamRuleBlobSchema.GroupIdProperty] = value;
			}
		}

		// Token: 0x170006BB RID: 1723
		// (get) Token: 0x06001581 RID: 5505 RVA: 0x00043160 File Offset: 0x00041360
		// (set) Token: 0x06001582 RID: 5506 RVA: 0x00043172 File Offset: 0x00041372
		public byte ScopeId
		{
			get
			{
				return (byte)this[SpamRuleBlobSchema.ScopeIdProperty];
			}
			set
			{
				this[SpamRuleBlobSchema.ScopeIdProperty] = value;
			}
		}

		// Token: 0x170006BC RID: 1724
		// (get) Token: 0x06001583 RID: 5507 RVA: 0x00043185 File Offset: 0x00041385
		// (set) Token: 0x06001584 RID: 5508 RVA: 0x00043197 File Offset: 0x00041397
		public byte PublishingState
		{
			get
			{
				return (byte)this[SpamRuleBlobSchema.PublishingStateProperty];
			}
			set
			{
				this[SpamRuleBlobSchema.PublishingStateProperty] = value;
			}
		}

		// Token: 0x170006BD RID: 1725
		// (get) Token: 0x06001585 RID: 5509 RVA: 0x000431AA File Offset: 0x000413AA
		// (set) Token: 0x06001586 RID: 5510 RVA: 0x000431BC File Offset: 0x000413BC
		public byte Priority
		{
			get
			{
				return (byte)this[SpamRuleBlobSchema.PriorityProperty];
			}
			set
			{
				this[SpamRuleBlobSchema.PriorityProperty] = value;
			}
		}

		// Token: 0x170006BE RID: 1726
		// (get) Token: 0x06001587 RID: 5511 RVA: 0x000431CF File Offset: 0x000413CF
		// (set) Token: 0x06001588 RID: 5512 RVA: 0x000431E1 File Offset: 0x000413E1
		public string RuleData
		{
			get
			{
				return this[SpamRuleBlobSchema.RuleDataProperty] as string;
			}
			set
			{
				this[SpamRuleBlobSchema.RuleDataProperty] = value;
			}
		}

		// Token: 0x170006BF RID: 1727
		// (get) Token: 0x06001589 RID: 5513 RVA: 0x000431EF File Offset: 0x000413EF
		// (set) Token: 0x0600158A RID: 5514 RVA: 0x00043201 File Offset: 0x00041401
		public string RuleMetaData
		{
			get
			{
				return this[SpamRuleBlobSchema.RuleMetaDataProperty] as string;
			}
			set
			{
				this[SpamRuleBlobSchema.RuleMetaDataProperty] = value;
			}
		}

		// Token: 0x170006C0 RID: 1728
		// (get) Token: 0x0600158B RID: 5515 RVA: 0x0004320F File Offset: 0x0004140F
		// (set) Token: 0x0600158C RID: 5516 RVA: 0x00043221 File Offset: 0x00041421
		public string ProcessorData
		{
			get
			{
				return this[SpamRuleBlobSchema.ProcessorDataProperty] as string;
			}
			set
			{
				this[SpamRuleBlobSchema.ProcessorDataProperty] = value;
			}
		}

		// Token: 0x170006C1 RID: 1729
		// (get) Token: 0x0600158D RID: 5517 RVA: 0x0004322F File Offset: 0x0004142F
		// (set) Token: 0x0600158E RID: 5518 RVA: 0x00043246 File Offset: 0x00041446
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

		// Token: 0x170006C2 RID: 1730
		// (get) Token: 0x0600158F RID: 5519 RVA: 0x00043259 File Offset: 0x00041459
		// (set) Token: 0x06001590 RID: 5520 RVA: 0x00043270 File Offset: 0x00041470
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

		// Token: 0x170006C3 RID: 1731
		// (get) Token: 0x06001591 RID: 5521 RVA: 0x00043283 File Offset: 0x00041483
		// (set) Token: 0x06001592 RID: 5522 RVA: 0x0004329A File Offset: 0x0004149A
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

		// Token: 0x06001593 RID: 5523 RVA: 0x000432AD File Offset: 0x000414AD
		public override Type GetSchemaType()
		{
			return typeof(SpamRuleBlobSchema);
		}
	}
}
