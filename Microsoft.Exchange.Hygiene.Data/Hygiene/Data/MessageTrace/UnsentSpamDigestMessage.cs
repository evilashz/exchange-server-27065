using System;
using Microsoft.Exchange.Data;

namespace Microsoft.Exchange.Hygiene.Data.MessageTrace
{
	// Token: 0x020001AD RID: 429
	internal class UnsentSpamDigestMessage : ConfigurablePropertyBag
	{
		// Token: 0x1700057C RID: 1404
		// (get) Token: 0x06001209 RID: 4617 RVA: 0x000374E7 File Offset: 0x000356E7
		// (set) Token: 0x0600120A RID: 4618 RVA: 0x000374F9 File Offset: 0x000356F9
		public Guid OrganizationalUnitRoot
		{
			get
			{
				return (Guid)this[UnsentSpamDigestMessageSchema.OrganizationalUnitRootProperty];
			}
			set
			{
				this[UnsentSpamDigestMessageSchema.OrganizationalUnitRootProperty] = value;
			}
		}

		// Token: 0x1700057D RID: 1405
		// (get) Token: 0x0600120B RID: 4619 RVA: 0x0003750C File Offset: 0x0003570C
		// (set) Token: 0x0600120C RID: 4620 RVA: 0x0003751E File Offset: 0x0003571E
		public Guid ExMessageId
		{
			get
			{
				return (Guid)this[UnsentSpamDigestMessageSchema.ExMessageIdProperty];
			}
			set
			{
				this[UnsentSpamDigestMessageSchema.ExMessageIdProperty] = value;
			}
		}

		// Token: 0x1700057E RID: 1406
		// (get) Token: 0x0600120D RID: 4621 RVA: 0x00037531 File Offset: 0x00035731
		// (set) Token: 0x0600120E RID: 4622 RVA: 0x00037543 File Offset: 0x00035743
		public string FromEmailDomain
		{
			get
			{
				return (string)this[UnsentSpamDigestMessageSchema.FromEmailDomainProperty];
			}
			set
			{
				this[UnsentSpamDigestMessageSchema.FromEmailDomainProperty] = value;
			}
		}

		// Token: 0x1700057F RID: 1407
		// (get) Token: 0x0600120F RID: 4623 RVA: 0x00037551 File Offset: 0x00035751
		// (set) Token: 0x06001210 RID: 4624 RVA: 0x00037563 File Offset: 0x00035763
		public string FromEmailPrefix
		{
			get
			{
				return (string)this[UnsentSpamDigestMessageSchema.FromEmailPrefixProperty];
			}
			set
			{
				this[UnsentSpamDigestMessageSchema.FromEmailPrefixProperty] = value;
			}
		}

		// Token: 0x17000580 RID: 1408
		// (get) Token: 0x06001211 RID: 4625 RVA: 0x00037571 File Offset: 0x00035771
		// (set) Token: 0x06001212 RID: 4626 RVA: 0x00037583 File Offset: 0x00035783
		public string ToEmailDomain
		{
			get
			{
				return (string)this[UnsentSpamDigestMessageSchema.ToEmailDomainProperty];
			}
			set
			{
				this[UnsentSpamDigestMessageSchema.ToEmailDomainProperty] = value;
			}
		}

		// Token: 0x17000581 RID: 1409
		// (get) Token: 0x06001213 RID: 4627 RVA: 0x00037591 File Offset: 0x00035791
		// (set) Token: 0x06001214 RID: 4628 RVA: 0x000375A3 File Offset: 0x000357A3
		public string ToEmailPrefix
		{
			get
			{
				return (string)this[UnsentSpamDigestMessageSchema.ToEmailPrefixProperty];
			}
			set
			{
				this[UnsentSpamDigestMessageSchema.ToEmailPrefixProperty] = value;
			}
		}

		// Token: 0x17000582 RID: 1410
		// (get) Token: 0x06001215 RID: 4629 RVA: 0x000375B1 File Offset: 0x000357B1
		// (set) Token: 0x06001216 RID: 4630 RVA: 0x000375C3 File Offset: 0x000357C3
		public string SenderName
		{
			get
			{
				return (string)this[UnsentSpamDigestMessageSchema.SenderNameProperty];
			}
			set
			{
				this[UnsentSpamDigestMessageSchema.SenderNameProperty] = value;
			}
		}

		// Token: 0x17000583 RID: 1411
		// (get) Token: 0x06001217 RID: 4631 RVA: 0x000375D1 File Offset: 0x000357D1
		// (set) Token: 0x06001218 RID: 4632 RVA: 0x000375E3 File Offset: 0x000357E3
		public string RecipientName
		{
			get
			{
				return (string)this[UnsentSpamDigestMessageSchema.RecipientNameProperty];
			}
			set
			{
				this[UnsentSpamDigestMessageSchema.RecipientNameProperty] = value;
			}
		}

		// Token: 0x17000584 RID: 1412
		// (get) Token: 0x06001219 RID: 4633 RVA: 0x000375F1 File Offset: 0x000357F1
		// (set) Token: 0x0600121A RID: 4634 RVA: 0x00037603 File Offset: 0x00035803
		public string Subject
		{
			get
			{
				return (string)this[UnsentSpamDigestMessageSchema.SubjectProperty];
			}
			set
			{
				this[UnsentSpamDigestMessageSchema.SubjectProperty] = value;
			}
		}

		// Token: 0x17000585 RID: 1413
		// (get) Token: 0x0600121B RID: 4635 RVA: 0x00037611 File Offset: 0x00035811
		// (set) Token: 0x0600121C RID: 4636 RVA: 0x00037623 File Offset: 0x00035823
		public int MessageSize
		{
			get
			{
				return (int)this[UnsentSpamDigestMessageSchema.MessageSizeProperty];
			}
			set
			{
				this[UnsentSpamDigestMessageSchema.MessageSizeProperty] = value;
			}
		}

		// Token: 0x17000586 RID: 1414
		// (get) Token: 0x0600121D RID: 4637 RVA: 0x00037636 File Offset: 0x00035836
		// (set) Token: 0x0600121E RID: 4638 RVA: 0x00037648 File Offset: 0x00035848
		public DateTime DateReceived
		{
			get
			{
				return (DateTime)this[UnsentSpamDigestMessageSchema.DateReceivedProperty];
			}
			set
			{
				this[UnsentSpamDigestMessageSchema.DateReceivedProperty] = value;
			}
		}

		// Token: 0x17000587 RID: 1415
		// (get) Token: 0x0600121F RID: 4639 RVA: 0x0003765B File Offset: 0x0003585B
		// (set) Token: 0x06001220 RID: 4640 RVA: 0x0003766D File Offset: 0x0003586D
		public DateTime? LastNotified
		{
			get
			{
				return (DateTime?)this[UnsentSpamDigestMessageSchema.LastNotifiedProperty];
			}
			set
			{
				this[UnsentSpamDigestMessageSchema.LastNotifiedProperty] = value;
			}
		}

		// Token: 0x17000588 RID: 1416
		// (get) Token: 0x06001221 RID: 4641 RVA: 0x00037680 File Offset: 0x00035880
		public override ObjectId Identity
		{
			get
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x06001222 RID: 4642 RVA: 0x00037687 File Offset: 0x00035887
		public override Type GetSchemaType()
		{
			return typeof(UnsentSpamDigestMessageSchema);
		}
	}
}
