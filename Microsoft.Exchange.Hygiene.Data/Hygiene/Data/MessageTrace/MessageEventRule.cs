using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data;

namespace Microsoft.Exchange.Hygiene.Data.MessageTrace
{
	// Token: 0x02000157 RID: 343
	internal class MessageEventRule : MessageTraceEntityBase, IExtendedPropertyStore<MessageEventRuleProperty>
	{
		// Token: 0x06000D80 RID: 3456 RVA: 0x00028E3A File Offset: 0x0002703A
		public MessageEventRule()
		{
			this.extendedProperties = new ExtendedPropertyStore<MessageEventRuleProperty>();
			this.EventRuleId = IdGenerator.GenerateIdentifier(IdScope.MessageEventRule);
		}

		// Token: 0x17000411 RID: 1041
		// (get) Token: 0x06000D81 RID: 3457 RVA: 0x00028E59 File Offset: 0x00027059
		public override ObjectId Identity
		{
			get
			{
				return new ConfigObjectId(this[MessageEventRuleSchema.EventRuleIdProperty].ToString());
			}
		}

		// Token: 0x17000412 RID: 1042
		// (get) Token: 0x06000D82 RID: 3458 RVA: 0x00028E70 File Offset: 0x00027070
		// (set) Token: 0x06000D83 RID: 3459 RVA: 0x00028E82 File Offset: 0x00027082
		public Guid EventRuleId
		{
			get
			{
				return (Guid)this[MessageEventRuleSchema.EventRuleIdProperty];
			}
			set
			{
				this[MessageEventRuleSchema.EventRuleIdProperty] = value;
			}
		}

		// Token: 0x17000413 RID: 1043
		// (get) Token: 0x06000D84 RID: 3460 RVA: 0x00028E95 File Offset: 0x00027095
		// (set) Token: 0x06000D85 RID: 3461 RVA: 0x00028EA7 File Offset: 0x000270A7
		public Guid RuleId
		{
			get
			{
				return (Guid)this[MessageEventRuleSchema.RuleIdProperty];
			}
			set
			{
				this[MessageEventRuleSchema.RuleIdProperty] = value;
			}
		}

		// Token: 0x17000414 RID: 1044
		// (get) Token: 0x06000D86 RID: 3462 RVA: 0x00028EBA File Offset: 0x000270BA
		// (set) Token: 0x06000D87 RID: 3463 RVA: 0x00028ECC File Offset: 0x000270CC
		public Guid EventId
		{
			get
			{
				return (Guid)this[MessageEventRuleSchema.EventIdProperty];
			}
			set
			{
				this[MessageEventRuleSchema.EventIdProperty] = value;
			}
		}

		// Token: 0x17000415 RID: 1045
		// (get) Token: 0x06000D88 RID: 3464 RVA: 0x00028EDF File Offset: 0x000270DF
		// (set) Token: 0x06000D89 RID: 3465 RVA: 0x00028EF1 File Offset: 0x000270F1
		public string RuleType
		{
			get
			{
				return (string)this[MessageEventRuleSchema.RuleTypeProperty];
			}
			set
			{
				this[MessageEventRuleSchema.RuleTypeProperty] = value;
			}
		}

		// Token: 0x17000416 RID: 1046
		// (get) Token: 0x06000D8A RID: 3466 RVA: 0x00028F00 File Offset: 0x00027100
		public List<MessageAction> Actions
		{
			get
			{
				List<MessageAction> result;
				if ((result = this.actions) == null)
				{
					result = (this.actions = new List<MessageAction>());
				}
				return result;
			}
		}

		// Token: 0x17000417 RID: 1047
		// (get) Token: 0x06000D8B RID: 3467 RVA: 0x00028F28 File Offset: 0x00027128
		public List<MessageEventRuleClassification> Classifications
		{
			get
			{
				List<MessageEventRuleClassification> result;
				if ((result = this.classifications) == null)
				{
					result = (this.classifications = new List<MessageEventRuleClassification>());
				}
				return result;
			}
		}

		// Token: 0x17000418 RID: 1048
		// (get) Token: 0x06000D8C RID: 3468 RVA: 0x00028F4D File Offset: 0x0002714D
		public int ExtendedPropertiesCount
		{
			get
			{
				return this.extendedProperties.ExtendedPropertiesCount;
			}
		}

		// Token: 0x06000D8D RID: 3469 RVA: 0x00028F5A File Offset: 0x0002715A
		public bool TryGetExtendedProperty(string nameSpace, string name, out MessageEventRuleProperty extendedProperty)
		{
			return this.extendedProperties.TryGetExtendedProperty(nameSpace, name, out extendedProperty);
		}

		// Token: 0x06000D8E RID: 3470 RVA: 0x00028F6A File Offset: 0x0002716A
		public MessageEventRuleProperty GetExtendedProperty(string nameSpace, string name)
		{
			return this.extendedProperties.GetExtendedProperty(nameSpace, name);
		}

		// Token: 0x06000D8F RID: 3471 RVA: 0x00028F79 File Offset: 0x00027179
		public IEnumerable<MessageEventRuleProperty> GetExtendedPropertiesEnumerable()
		{
			return this.extendedProperties.GetExtendedPropertiesEnumerable();
		}

		// Token: 0x06000D90 RID: 3472 RVA: 0x00028F86 File Offset: 0x00027186
		public void AddExtendedProperty(MessageEventRuleProperty extendedProperty)
		{
			this.extendedProperties.AddExtendedProperty(extendedProperty);
			extendedProperty.EventRuleId = this.EventRuleId;
		}

		// Token: 0x06000D91 RID: 3473 RVA: 0x00028FA0 File Offset: 0x000271A0
		public override void Accept(IMessageTraceVisitor visitor)
		{
			visitor.Visit(this);
			if (this.actions != null)
			{
				foreach (MessageAction messageAction in this.actions)
				{
					messageAction.Accept(visitor);
				}
			}
			if (this.classifications != null)
			{
				foreach (MessageEventRuleClassification messageEventRuleClassification in this.classifications)
				{
					messageEventRuleClassification.Accept(visitor);
				}
			}
			foreach (MessageEventRuleProperty messageEventRuleProperty in this.GetExtendedPropertiesEnumerable())
			{
				messageEventRuleProperty.Accept(visitor);
			}
		}

		// Token: 0x06000D92 RID: 3474 RVA: 0x00029090 File Offset: 0x00027290
		public void Add(MessageAction action)
		{
			if (action == null)
			{
				throw new ArgumentNullException("action");
			}
			action.EventRuleId = this.EventRuleId;
			this.Actions.Add(action);
		}

		// Token: 0x06000D93 RID: 3475 RVA: 0x000290B8 File Offset: 0x000272B8
		public void Add(MessageEventRuleClassification classification)
		{
			if (classification == null)
			{
				throw new ArgumentNullException("classification");
			}
			classification.EventRuleId = this.EventRuleId;
			this.Classifications.Add(classification);
		}

		// Token: 0x06000D94 RID: 3476 RVA: 0x000290E0 File Offset: 0x000272E0
		public override Type GetSchemaType()
		{
			return typeof(MessageEventRuleSchema);
		}

		// Token: 0x06000D95 RID: 3477 RVA: 0x000290EC File Offset: 0x000272EC
		public override HygienePropertyDefinition[] GetAllProperties()
		{
			return MessageEventRule.Properties;
		}

		// Token: 0x04000684 RID: 1668
		internal static readonly HygienePropertyDefinition[] Properties = new HygienePropertyDefinition[]
		{
			MessageEventRuleSchema.EventRuleIdProperty,
			MessageEventRuleSchema.EventIdProperty,
			MessageEventRuleSchema.RuleIdProperty,
			MessageEventRuleSchema.RuleTypeProperty,
			CommonMessageTraceSchema.ExMessageIdProperty,
			CommonMessageTraceSchema.EventHashKeyProperty
		};

		// Token: 0x04000685 RID: 1669
		private List<MessageAction> actions;

		// Token: 0x04000686 RID: 1670
		private List<MessageEventRuleClassification> classifications;

		// Token: 0x04000687 RID: 1671
		private ExtendedPropertyStore<MessageEventRuleProperty> extendedProperties;
	}
}
