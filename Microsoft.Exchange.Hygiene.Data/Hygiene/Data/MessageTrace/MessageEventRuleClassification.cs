using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data;

namespace Microsoft.Exchange.Hygiene.Data.MessageTrace
{
	// Token: 0x02000158 RID: 344
	internal class MessageEventRuleClassification : MessageTraceEntityBase, IExtendedPropertyStore<MessageEventRuleClassificationProperty>
	{
		// Token: 0x06000D97 RID: 3479 RVA: 0x0002913E File Offset: 0x0002733E
		public MessageEventRuleClassification()
		{
			this.extendedProperties = new ExtendedPropertyStore<MessageEventRuleClassificationProperty>();
			this.EventRuleClassificationId = IdGenerator.GenerateIdentifier(IdScope.MessageEventRuleClassification);
		}

		// Token: 0x17000419 RID: 1049
		// (get) Token: 0x06000D98 RID: 3480 RVA: 0x0002915E File Offset: 0x0002735E
		public override ObjectId Identity
		{
			get
			{
				return new ConfigObjectId(this[MessageEventRuleClassificationSchema.DataClassificationIdProperty].ToString());
			}
		}

		// Token: 0x1700041A RID: 1050
		// (get) Token: 0x06000D99 RID: 3481 RVA: 0x00029175 File Offset: 0x00027375
		// (set) Token: 0x06000D9A RID: 3482 RVA: 0x00029187 File Offset: 0x00027387
		public Guid EventRuleClassificationId
		{
			get
			{
				return (Guid)this[CommonMessageTraceSchema.EventRuleClassificationIdProperty];
			}
			set
			{
				this[CommonMessageTraceSchema.EventRuleClassificationIdProperty] = value;
			}
		}

		// Token: 0x1700041B RID: 1051
		// (get) Token: 0x06000D9B RID: 3483 RVA: 0x0002919A File Offset: 0x0002739A
		// (set) Token: 0x06000D9C RID: 3484 RVA: 0x000291AC File Offset: 0x000273AC
		public Guid EventRuleId
		{
			get
			{
				return (Guid)this[CommonMessageTraceSchema.EventRuleIdProperty];
			}
			set
			{
				this[CommonMessageTraceSchema.EventRuleIdProperty] = value;
			}
		}

		// Token: 0x1700041C RID: 1052
		// (get) Token: 0x06000D9D RID: 3485 RVA: 0x000291BF File Offset: 0x000273BF
		// (set) Token: 0x06000D9E RID: 3486 RVA: 0x000291D1 File Offset: 0x000273D1
		public Guid DataClassificationId
		{
			get
			{
				return (Guid)this[CommonMessageTraceSchema.DataClassificationIdProperty];
			}
			set
			{
				this[CommonMessageTraceSchema.DataClassificationIdProperty] = value;
			}
		}

		// Token: 0x1700041D RID: 1053
		// (get) Token: 0x06000D9F RID: 3487 RVA: 0x000291E4 File Offset: 0x000273E4
		// (set) Token: 0x06000DA0 RID: 3488 RVA: 0x000291F6 File Offset: 0x000273F6
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

		// Token: 0x1700041E RID: 1054
		// (get) Token: 0x06000DA1 RID: 3489 RVA: 0x00029209 File Offset: 0x00027409
		public int ExtendedPropertiesCount
		{
			get
			{
				return this.extendedProperties.ExtendedPropertiesCount;
			}
		}

		// Token: 0x06000DA2 RID: 3490 RVA: 0x00029216 File Offset: 0x00027416
		public bool TryGetExtendedProperty(string nameSpace, string name, out MessageEventRuleClassificationProperty extendedProperty)
		{
			return this.extendedProperties.TryGetExtendedProperty(nameSpace, name, out extendedProperty);
		}

		// Token: 0x06000DA3 RID: 3491 RVA: 0x00029226 File Offset: 0x00027426
		public MessageEventRuleClassificationProperty GetExtendedProperty(string nameSpace, string name)
		{
			return this.extendedProperties.GetExtendedProperty(nameSpace, name);
		}

		// Token: 0x06000DA4 RID: 3492 RVA: 0x00029235 File Offset: 0x00027435
		public IEnumerable<MessageEventRuleClassificationProperty> GetExtendedPropertiesEnumerable()
		{
			return this.extendedProperties.GetExtendedPropertiesEnumerable();
		}

		// Token: 0x06000DA5 RID: 3493 RVA: 0x00029242 File Offset: 0x00027442
		public void AddExtendedProperty(MessageEventRuleClassificationProperty extendedProperty)
		{
			this.extendedProperties.AddExtendedProperty(extendedProperty);
			extendedProperty.EventRuleClassificationId = this.EventRuleClassificationId;
		}

		// Token: 0x06000DA6 RID: 3494 RVA: 0x0002925C File Offset: 0x0002745C
		public override void Accept(IMessageTraceVisitor visitor)
		{
			visitor.Visit(this);
			foreach (MessageEventRuleClassificationProperty messageEventRuleClassificationProperty in this.GetExtendedPropertiesEnumerable())
			{
				messageEventRuleClassificationProperty.Accept(visitor);
			}
		}

		// Token: 0x06000DA7 RID: 3495 RVA: 0x000292B0 File Offset: 0x000274B0
		public override Type GetSchemaType()
		{
			return typeof(MessageEventRuleClassificationSchema);
		}

		// Token: 0x06000DA8 RID: 3496 RVA: 0x000292BC File Offset: 0x000274BC
		public override HygienePropertyDefinition[] GetAllProperties()
		{
			return MessageEventRuleClassification.Properties;
		}

		// Token: 0x04000688 RID: 1672
		internal static readonly HygienePropertyDefinition[] Properties = new HygienePropertyDefinition[]
		{
			CommonMessageTraceSchema.EventRuleClassificationIdProperty,
			CommonMessageTraceSchema.EventRuleIdProperty,
			CommonMessageTraceSchema.DataClassificationIdProperty,
			CommonMessageTraceSchema.ExMessageIdProperty,
			CommonMessageTraceSchema.RuleIdProperty,
			CommonMessageTraceSchema.EventHashKeyProperty
		};

		// Token: 0x04000689 RID: 1673
		private ExtendedPropertyStore<MessageEventRuleClassificationProperty> extendedProperties;
	}
}
