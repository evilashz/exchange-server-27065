using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data;

namespace Microsoft.Exchange.Hygiene.Data.MessageTrace
{
	// Token: 0x02000166 RID: 358
	internal sealed class MessageRecipientStatus : MessageTraceEntityBase, IExtendedPropertyStore<MessageRecipientStatusProperty>
	{
		// Token: 0x06000E2A RID: 3626 RVA: 0x0002A04C File Offset: 0x0002824C
		public MessageRecipientStatus()
		{
			this.extendedProperties = new ExtendedPropertyStore<MessageRecipientStatusProperty>();
			this.RecipientStatusId = IdGenerator.GenerateIdentifier(IdScope.MessageRecipientStatus);
		}

		// Token: 0x17000432 RID: 1074
		// (get) Token: 0x06000E2B RID: 3627 RVA: 0x0002A06B File Offset: 0x0002826B
		public override ObjectId Identity
		{
			get
			{
				return new ConfigObjectId(this[MessageRecipientStatusSchema.RecipientStatusIdProperty].ToString());
			}
		}

		// Token: 0x17000433 RID: 1075
		// (get) Token: 0x06000E2C RID: 3628 RVA: 0x0002A082 File Offset: 0x00028282
		// (set) Token: 0x06000E2D RID: 3629 RVA: 0x0002A094 File Offset: 0x00028294
		public Guid RecipientStatusId
		{
			get
			{
				return (Guid)this[MessageRecipientStatusSchema.RecipientStatusIdProperty];
			}
			set
			{
				this[MessageRecipientStatusSchema.RecipientStatusIdProperty] = value;
			}
		}

		// Token: 0x17000434 RID: 1076
		// (get) Token: 0x06000E2E RID: 3630 RVA: 0x0002A0A7 File Offset: 0x000282A7
		// (set) Token: 0x06000E2F RID: 3631 RVA: 0x0002A0B9 File Offset: 0x000282B9
		public Guid RecipientId
		{
			get
			{
				return (Guid)this[MessageRecipientStatusSchema.RecipientIdProperty];
			}
			set
			{
				this[MessageRecipientStatusSchema.RecipientIdProperty] = value;
			}
		}

		// Token: 0x17000435 RID: 1077
		// (get) Token: 0x06000E30 RID: 3632 RVA: 0x0002A0CC File Offset: 0x000282CC
		// (set) Token: 0x06000E31 RID: 3633 RVA: 0x0002A0DE File Offset: 0x000282DE
		public Guid EventId
		{
			get
			{
				return (Guid)this[MessageRecipientStatusSchema.EventIdProperty];
			}
			set
			{
				this[MessageRecipientStatusSchema.EventIdProperty] = value;
			}
		}

		// Token: 0x17000436 RID: 1078
		// (get) Token: 0x06000E32 RID: 3634 RVA: 0x0002A0F1 File Offset: 0x000282F1
		// (set) Token: 0x06000E33 RID: 3635 RVA: 0x0002A103 File Offset: 0x00028303
		public string Status
		{
			get
			{
				return this[MessageRecipientStatusSchema.StatusProperty] as string;
			}
			set
			{
				this[MessageRecipientStatusSchema.StatusProperty] = value;
			}
		}

		// Token: 0x17000437 RID: 1079
		// (get) Token: 0x06000E34 RID: 3636 RVA: 0x0002A111 File Offset: 0x00028311
		// (set) Token: 0x06000E35 RID: 3637 RVA: 0x0002A123 File Offset: 0x00028323
		public string Reference
		{
			get
			{
				return this[MessageRecipientStatusSchema.ReferenceProperty] as string;
			}
			set
			{
				this[MessageRecipientStatusSchema.ReferenceProperty] = value;
			}
		}

		// Token: 0x17000438 RID: 1080
		// (get) Token: 0x06000E36 RID: 3638 RVA: 0x0002A131 File Offset: 0x00028331
		public int ExtendedPropertiesCount
		{
			get
			{
				return this.extendedProperties.ExtendedPropertiesCount;
			}
		}

		// Token: 0x06000E37 RID: 3639 RVA: 0x0002A13E File Offset: 0x0002833E
		public bool TryGetExtendedProperty(string nameSpace, string name, out MessageRecipientStatusProperty extendedProperty)
		{
			return this.extendedProperties.TryGetExtendedProperty(nameSpace, name, out extendedProperty);
		}

		// Token: 0x06000E38 RID: 3640 RVA: 0x0002A14E File Offset: 0x0002834E
		public MessageRecipientStatusProperty GetExtendedProperty(string nameSpace, string name)
		{
			return this.extendedProperties.GetExtendedProperty(nameSpace, name);
		}

		// Token: 0x06000E39 RID: 3641 RVA: 0x0002A15D File Offset: 0x0002835D
		public IEnumerable<MessageRecipientStatusProperty> GetExtendedPropertiesEnumerable()
		{
			return this.extendedProperties.GetExtendedPropertiesEnumerable();
		}

		// Token: 0x06000E3A RID: 3642 RVA: 0x0002A16A File Offset: 0x0002836A
		public void AddExtendedProperty(MessageRecipientStatusProperty extendedProperty)
		{
			this.extendedProperties.AddExtendedProperty(extendedProperty);
			extendedProperty.RecipientStatusId = this.RecipientStatusId;
		}

		// Token: 0x06000E3B RID: 3643 RVA: 0x0002A184 File Offset: 0x00028384
		public override void Accept(IMessageTraceVisitor visitor)
		{
			visitor.Visit(this);
			foreach (MessageRecipientStatusProperty messageRecipientStatusProperty in this.GetExtendedPropertiesEnumerable())
			{
				messageRecipientStatusProperty.Accept(visitor);
			}
		}

		// Token: 0x06000E3C RID: 3644 RVA: 0x0002A1D8 File Offset: 0x000283D8
		public override Type GetSchemaType()
		{
			return typeof(MessageRecipientStatusSchema);
		}

		// Token: 0x06000E3D RID: 3645 RVA: 0x0002A1E4 File Offset: 0x000283E4
		public override HygienePropertyDefinition[] GetAllProperties()
		{
			return MessageRecipientStatus.Properties;
		}

		// Token: 0x040006AE RID: 1710
		internal static readonly HygienePropertyDefinition[] Properties = new HygienePropertyDefinition[]
		{
			MessageRecipientStatusSchema.RecipientStatusIdProperty,
			MessageRecipientStatusSchema.RecipientIdProperty,
			CommonMessageTraceSchema.ExMessageIdProperty,
			MessageRecipientStatusSchema.EventIdProperty,
			MessageRecipientStatusSchema.StatusProperty,
			MessageRecipientStatusSchema.ReferenceProperty,
			CommonMessageTraceSchema.EmailHashKeyProperty,
			CommonMessageTraceSchema.EventHashKeyProperty
		};

		// Token: 0x040006AF RID: 1711
		private ExtendedPropertyStore<MessageRecipientStatusProperty> extendedProperties;
	}
}
