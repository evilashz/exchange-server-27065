using System;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Hygiene.Data.MessageTrace
{
	// Token: 0x0200018D RID: 397
	internal class QuarantinedMessageRecipientBatch : ConfigurablePropertyBag
	{
		// Token: 0x06001020 RID: 4128 RVA: 0x00032AD1 File Offset: 0x00030CD1
		public QuarantinedMessageRecipientBatch(Guid organizationalUnitRoot)
		{
			this[QuarantinedMessageRecipientBatchSchema.OrganizationalUnitRootProperty] = organizationalUnitRoot;
			this[QuarantinedMessageRecipientBatchSchema.BatchAddressesProperty] = this.batchRecipients;
		}

		// Token: 0x170004AB RID: 1195
		// (get) Token: 0x06001021 RID: 4129 RVA: 0x00032B06 File Offset: 0x00030D06
		public override ObjectId Identity
		{
			get
			{
				return new MessageTraceObjectId((Guid)this[QuarantinedMessageRecipientBatchSchema.OrganizationalUnitRootProperty], Guid.Empty);
			}
		}

		// Token: 0x170004AC RID: 1196
		// (get) Token: 0x06001022 RID: 4130 RVA: 0x00032B22 File Offset: 0x00030D22
		// (set) Token: 0x06001023 RID: 4131 RVA: 0x00032B34 File Offset: 0x00030D34
		public int FssCopyId
		{
			get
			{
				return (int)this[QuarantinedMessageRecipientBatchSchema.FssCopyIdProp];
			}
			set
			{
				this[QuarantinedMessageRecipientBatchSchema.FssCopyIdProp] = value;
			}
		}

		// Token: 0x06001024 RID: 4132 RVA: 0x00032B48 File Offset: 0x00030D48
		public void Add(QuarantinedMessageRecipient recipient)
		{
			if (recipient == null)
			{
				throw new ArgumentNullException("recipient");
			}
			if (recipient.EmailPrefix == null)
			{
				throw new ArgumentException("recipient.EmailPrefix");
			}
			if (recipient.EmailDomain == null)
			{
				throw new ArgumentException("recipient.EmailDomain");
			}
			Guid identity = CombGuidGenerator.NewGuid();
			this.batchRecipients.AddPropertyValue(identity, QuarantinedMessageRecipientSchema.ExMessageIdProperty, recipient.ExMessageId);
			this.batchRecipients.AddPropertyValue(identity, QuarantinedMessageRecipientSchema.EmailPrefixProperty, recipient.EmailPrefix);
			this.batchRecipients.AddPropertyValue(identity, QuarantinedMessageRecipientSchema.EmailDomainProperty, recipient.EmailDomain);
			this.batchRecipients.AddPropertyValue(identity, QuarantinedMessageRecipientSchema.QuarantinedProperty, recipient.Quarantined);
			this.batchRecipients.AddPropertyValue(identity, QuarantinedMessageRecipientSchema.NotifiedProperty, recipient.Notified);
			this.batchRecipients.AddPropertyValue(identity, QuarantinedMessageRecipientSchema.ReportedProperty, recipient.Reported);
			this.batchRecipients.AddPropertyValue(identity, QuarantinedMessageRecipientSchema.ReleasedProperty, recipient.Released);
		}

		// Token: 0x06001025 RID: 4133 RVA: 0x00032C49 File Offset: 0x00030E49
		public override Type GetSchemaType()
		{
			return typeof(QuarantinedMessageRecipientBatchSchema);
		}

		// Token: 0x04000795 RID: 1941
		private BatchPropertyTable batchRecipients = new BatchPropertyTable();
	}
}
