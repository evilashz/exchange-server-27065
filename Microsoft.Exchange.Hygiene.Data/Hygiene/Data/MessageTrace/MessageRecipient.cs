using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data;

namespace Microsoft.Exchange.Hygiene.Data.MessageTrace
{
	// Token: 0x02000162 RID: 354
	internal class MessageRecipient : MessageTraceEntityBase, IExtendedPropertyStore<MessageRecipientProperty>
	{
		// Token: 0x06000E03 RID: 3587 RVA: 0x00029C46 File Offset: 0x00027E46
		public MessageRecipient()
		{
			this.extendedProperties = new ExtendedPropertyStore<MessageRecipientProperty>();
			this.RecipientId = IdGenerator.GenerateIdentifier(IdScope.MessageRecipient);
		}

		// Token: 0x1700042A RID: 1066
		// (get) Token: 0x06000E04 RID: 3588 RVA: 0x00029C65 File Offset: 0x00027E65
		// (set) Token: 0x06000E05 RID: 3589 RVA: 0x00029C77 File Offset: 0x00027E77
		public Guid ExMessageId
		{
			get
			{
				return (Guid)this[CommonMessageTraceSchema.ExMessageIdProperty];
			}
			set
			{
				this[CommonMessageTraceSchema.ExMessageIdProperty] = value;
			}
		}

		// Token: 0x1700042B RID: 1067
		// (get) Token: 0x06000E06 RID: 3590 RVA: 0x00029C8A File Offset: 0x00027E8A
		// (set) Token: 0x06000E07 RID: 3591 RVA: 0x00029C9C File Offset: 0x00027E9C
		public Guid RecipientId
		{
			get
			{
				return (Guid)this[CommonMessageTraceSchema.RecipientIdProperty];
			}
			set
			{
				this[CommonMessageTraceSchema.RecipientIdProperty] = value;
			}
		}

		// Token: 0x1700042C RID: 1068
		// (get) Token: 0x06000E08 RID: 3592 RVA: 0x00029CAF File Offset: 0x00027EAF
		public override ObjectId Identity
		{
			get
			{
				return new ConfigObjectId(this[MessageRecipientSchema.RecipientIdProperty].ToString());
			}
		}

		// Token: 0x1700042D RID: 1069
		// (get) Token: 0x06000E09 RID: 3593 RVA: 0x00029CC6 File Offset: 0x00027EC6
		// (set) Token: 0x06000E0A RID: 3594 RVA: 0x00029CD8 File Offset: 0x00027ED8
		public string ToEmailPrefix
		{
			get
			{
				return (string)this[MessageRecipientSchema.ToEmailPrefixProperty];
			}
			set
			{
				this[MessageRecipientSchema.ToEmailPrefixProperty] = value;
			}
		}

		// Token: 0x1700042E RID: 1070
		// (get) Token: 0x06000E0B RID: 3595 RVA: 0x00029CE6 File Offset: 0x00027EE6
		// (set) Token: 0x06000E0C RID: 3596 RVA: 0x00029CF8 File Offset: 0x00027EF8
		public string ToEmailDomain
		{
			get
			{
				return (string)this[MessageRecipientSchema.ToEmailDomainProperty];
			}
			set
			{
				this[MessageRecipientSchema.ToEmailDomainProperty] = value;
			}
		}

		// Token: 0x1700042F RID: 1071
		// (get) Token: 0x06000E0D RID: 3597 RVA: 0x00029D06 File Offset: 0x00027F06
		// (set) Token: 0x06000E0E RID: 3598 RVA: 0x00029D18 File Offset: 0x00027F18
		public MailDeliveryStatus MailDeliveryStatus
		{
			get
			{
				return (MailDeliveryStatus)this[MessageRecipientSchema.MailDeliveryStatusProperty];
			}
			set
			{
				this[MessageRecipientSchema.MailDeliveryStatusProperty] = value;
			}
		}

		// Token: 0x17000430 RID: 1072
		// (get) Token: 0x06000E0F RID: 3599 RVA: 0x00029D2B File Offset: 0x00027F2B
		public int ExtendedPropertiesCount
		{
			get
			{
				return this.extendedProperties.ExtendedPropertiesCount;
			}
		}

		// Token: 0x06000E10 RID: 3600 RVA: 0x00029D38 File Offset: 0x00027F38
		public static string GetEmailAddress(string emailPrefix, string emailDomain)
		{
			if (string.IsNullOrWhiteSpace(emailDomain))
			{
				return MessageTraceEntityBase.StandardizeEmailPrefix(emailPrefix);
			}
			return MessageTraceEntityBase.StandardizeEmailPrefix(emailPrefix) + "@" + emailDomain;
		}

		// Token: 0x06000E11 RID: 3601 RVA: 0x00029D5A File Offset: 0x00027F5A
		public bool TryGetExtendedProperty(string nameSpace, string name, out MessageRecipientProperty extendedProperty)
		{
			return this.extendedProperties.TryGetExtendedProperty(nameSpace, name, out extendedProperty);
		}

		// Token: 0x06000E12 RID: 3602 RVA: 0x00029D6A File Offset: 0x00027F6A
		public MessageRecipientProperty GetExtendedProperty(string nameSpace, string name)
		{
			return this.extendedProperties.GetExtendedProperty(nameSpace, name);
		}

		// Token: 0x06000E13 RID: 3603 RVA: 0x00029D79 File Offset: 0x00027F79
		public IEnumerable<MessageRecipientProperty> GetExtendedPropertiesEnumerable()
		{
			return this.extendedProperties.GetExtendedPropertiesEnumerable();
		}

		// Token: 0x06000E14 RID: 3604 RVA: 0x00029D86 File Offset: 0x00027F86
		public void AddExtendedProperty(MessageRecipientProperty extendedProperty)
		{
			this.extendedProperties.AddExtendedProperty(extendedProperty);
			extendedProperty.RecipientId = this.RecipientId;
		}

		// Token: 0x06000E15 RID: 3605 RVA: 0x00029DA0 File Offset: 0x00027FA0
		public override void Accept(IMessageTraceVisitor visitor)
		{
			visitor.Visit(this);
			foreach (MessageRecipientProperty messageRecipientProperty in this.GetExtendedPropertiesEnumerable())
			{
				messageRecipientProperty.Accept(visitor);
			}
		}

		// Token: 0x06000E16 RID: 3606 RVA: 0x00029DF4 File Offset: 0x00027FF4
		public override Type GetSchemaType()
		{
			return typeof(MessageRecipientSchema);
		}

		// Token: 0x06000E17 RID: 3607 RVA: 0x00029E00 File Offset: 0x00028000
		public override HygienePropertyDefinition[] GetAllProperties()
		{
			return MessageRecipient.Properties;
		}

		// Token: 0x040006A0 RID: 1696
		internal static readonly HygienePropertyDefinition[] Properties = new HygienePropertyDefinition[]
		{
			CommonMessageTraceSchema.ExMessageIdProperty,
			CommonMessageTraceSchema.RecipientIdProperty,
			MessageRecipientSchema.ToEmailPrefixProperty,
			MessageRecipientSchema.ToEmailDomainProperty,
			MessageRecipientSchema.MailDeliveryStatusProperty,
			CommonMessageTraceSchema.EmailHashKeyProperty,
			CommonMessageTraceSchema.EmailDomainHashKeyProperty
		};

		// Token: 0x040006A1 RID: 1697
		private ExtendedPropertyStore<MessageRecipientProperty> extendedProperties;
	}
}
