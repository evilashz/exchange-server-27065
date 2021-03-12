using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data;

namespace Microsoft.Exchange.Hygiene.Data.MessageTrace
{
	// Token: 0x0200014F RID: 335
	internal class MessageClassification : MessageTraceEntityBase, IExtendedPropertyStore<MessageClassificationProperty>
	{
		// Token: 0x06000D0D RID: 3341 RVA: 0x00028214 File Offset: 0x00026414
		public MessageClassification()
		{
			this.extendedProperties = new ExtendedPropertyStore<MessageClassificationProperty>();
			this.ClassificationId = IdGenerator.GenerateIdentifier(IdScope.MessageClassification);
		}

		// Token: 0x170003F8 RID: 1016
		// (get) Token: 0x06000D0E RID: 3342 RVA: 0x00028233 File Offset: 0x00026433
		public override ObjectId Identity
		{
			get
			{
				return new ConfigObjectId(this[MessageClassificationSchema.ClassificationIdProperty].ToString());
			}
		}

		// Token: 0x170003F9 RID: 1017
		// (get) Token: 0x06000D0F RID: 3343 RVA: 0x0002824A File Offset: 0x0002644A
		// (set) Token: 0x06000D10 RID: 3344 RVA: 0x0002825C File Offset: 0x0002645C
		public Guid ClassificationId
		{
			get
			{
				return (Guid)this[MessageClassificationSchema.ClassificationIdProperty];
			}
			set
			{
				this[MessageClassificationSchema.ClassificationIdProperty] = value;
			}
		}

		// Token: 0x170003FA RID: 1018
		// (get) Token: 0x06000D11 RID: 3345 RVA: 0x0002826F File Offset: 0x0002646F
		// (set) Token: 0x06000D12 RID: 3346 RVA: 0x00028281 File Offset: 0x00026481
		public Guid ExMessageId
		{
			get
			{
				return (Guid)this[MessageClassificationSchema.ExMessageIdProperty];
			}
			set
			{
				this[MessageClassificationSchema.ExMessageIdProperty] = value;
			}
		}

		// Token: 0x170003FB RID: 1019
		// (get) Token: 0x06000D13 RID: 3347 RVA: 0x00028294 File Offset: 0x00026494
		// (set) Token: 0x06000D14 RID: 3348 RVA: 0x000282A6 File Offset: 0x000264A6
		public Guid DataClassificationId
		{
			get
			{
				return (Guid)this[MessageClassificationSchema.DataClassificationIdProperty];
			}
			set
			{
				this[MessageClassificationSchema.DataClassificationIdProperty] = value;
			}
		}

		// Token: 0x170003FC RID: 1020
		// (get) Token: 0x06000D15 RID: 3349 RVA: 0x000282B9 File Offset: 0x000264B9
		public int ExtendedPropertiesCount
		{
			get
			{
				return this.extendedProperties.ExtendedPropertiesCount;
			}
		}

		// Token: 0x06000D16 RID: 3350 RVA: 0x000282C6 File Offset: 0x000264C6
		public bool TryGetExtendedProperty(string nameSpace, string name, out MessageClassificationProperty extendedProperty)
		{
			return this.extendedProperties.TryGetExtendedProperty(nameSpace, name, out extendedProperty);
		}

		// Token: 0x06000D17 RID: 3351 RVA: 0x000282D6 File Offset: 0x000264D6
		public MessageClassificationProperty GetExtendedProperty(string nameSpace, string name)
		{
			return this.extendedProperties.GetExtendedProperty(nameSpace, name);
		}

		// Token: 0x06000D18 RID: 3352 RVA: 0x000282E5 File Offset: 0x000264E5
		public IEnumerable<MessageClassificationProperty> GetExtendedPropertiesEnumerable()
		{
			return this.extendedProperties.GetExtendedPropertiesEnumerable();
		}

		// Token: 0x06000D19 RID: 3353 RVA: 0x000282F2 File Offset: 0x000264F2
		public void AddExtendedProperty(MessageClassificationProperty extendedProperty)
		{
			this.extendedProperties.AddExtendedProperty(extendedProperty);
			extendedProperty.ClassificationId = this.ClassificationId;
		}

		// Token: 0x06000D1A RID: 3354 RVA: 0x0002830C File Offset: 0x0002650C
		public override void Accept(IMessageTraceVisitor visitor)
		{
			visitor.Visit(this);
			foreach (MessageClassificationProperty messageClassificationProperty in this.GetExtendedPropertiesEnumerable())
			{
				messageClassificationProperty.Accept(visitor);
			}
		}

		// Token: 0x06000D1B RID: 3355 RVA: 0x00028360 File Offset: 0x00026560
		public override Type GetSchemaType()
		{
			return typeof(MessageClassificationSchema);
		}

		// Token: 0x06000D1C RID: 3356 RVA: 0x0002836C File Offset: 0x0002656C
		public override HygienePropertyDefinition[] GetAllProperties()
		{
			return MessageClassification.Properties;
		}

		// Token: 0x04000672 RID: 1650
		internal static readonly HygienePropertyDefinition[] Properties = new HygienePropertyDefinition[]
		{
			MessageClassificationSchema.ClassificationIdProperty,
			CommonMessageTraceSchema.ExMessageIdProperty,
			MessageClassificationSchema.DataClassificationIdProperty
		};

		// Token: 0x04000673 RID: 1651
		private ExtendedPropertyStore<MessageClassificationProperty> extendedProperties;
	}
}
