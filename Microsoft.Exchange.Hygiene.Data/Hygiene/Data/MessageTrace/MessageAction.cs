using System;
using System.Collections.Generic;

namespace Microsoft.Exchange.Hygiene.Data.MessageTrace
{
	// Token: 0x0200014B RID: 331
	internal class MessageAction : MessageTraceEntityBase, IExtendedPropertyStore<MessageActionProperty>
	{
		// Token: 0x06000CB6 RID: 3254 RVA: 0x000275E6 File Offset: 0x000257E6
		public MessageAction()
		{
			this.extendedProperties = new ExtendedPropertyStore<MessageActionProperty>();
			this.RuleActionId = IdGenerator.GenerateIdentifier(IdScope.MessageAction);
		}

		// Token: 0x170003E2 RID: 994
		// (get) Token: 0x06000CB7 RID: 3255 RVA: 0x00027605 File Offset: 0x00025805
		// (set) Token: 0x06000CB8 RID: 3256 RVA: 0x00027617 File Offset: 0x00025817
		public Guid RuleActionId
		{
			get
			{
				return (Guid)this[MessageActionSchema.RuleActionIdProperty];
			}
			set
			{
				this[MessageActionSchema.RuleActionIdProperty] = value;
			}
		}

		// Token: 0x170003E3 RID: 995
		// (get) Token: 0x06000CB9 RID: 3257 RVA: 0x0002762A File Offset: 0x0002582A
		// (set) Token: 0x06000CBA RID: 3258 RVA: 0x0002763C File Offset: 0x0002583C
		public Guid EventRuleId
		{
			get
			{
				return (Guid)this[MessageActionSchema.EventRuleIdProperty];
			}
			set
			{
				this[MessageActionSchema.EventRuleIdProperty] = value;
			}
		}

		// Token: 0x170003E4 RID: 996
		// (get) Token: 0x06000CBB RID: 3259 RVA: 0x0002764F File Offset: 0x0002584F
		// (set) Token: 0x06000CBC RID: 3260 RVA: 0x00027661 File Offset: 0x00025861
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

		// Token: 0x170003E5 RID: 997
		// (get) Token: 0x06000CBD RID: 3261 RVA: 0x00027674 File Offset: 0x00025874
		// (set) Token: 0x06000CBE RID: 3262 RVA: 0x00027686 File Offset: 0x00025886
		public string Name
		{
			get
			{
				return (string)this[MessageActionSchema.NameProperty];
			}
			set
			{
				this[MessageActionSchema.NameProperty] = value;
			}
		}

		// Token: 0x170003E6 RID: 998
		// (get) Token: 0x06000CBF RID: 3263 RVA: 0x00027694 File Offset: 0x00025894
		// (set) Token: 0x06000CC0 RID: 3264 RVA: 0x000276A6 File Offset: 0x000258A6
		public Guid OrganizationalUnitRoot
		{
			get
			{
				return (Guid)this[CommonMessageTraceSchema.OrganizationalUnitRootProperty];
			}
			set
			{
				this[CommonMessageTraceSchema.OrganizationalUnitRootProperty] = value;
			}
		}

		// Token: 0x170003E7 RID: 999
		// (get) Token: 0x06000CC1 RID: 3265 RVA: 0x000276B9 File Offset: 0x000258B9
		public int ExtendedPropertiesCount
		{
			get
			{
				return this.extendedProperties.ExtendedPropertiesCount;
			}
		}

		// Token: 0x06000CC2 RID: 3266 RVA: 0x000276C6 File Offset: 0x000258C6
		public bool TryGetExtendedProperty(string nameSpace, string name, out MessageActionProperty extendedProperty)
		{
			return this.extendedProperties.TryGetExtendedProperty(nameSpace, name, out extendedProperty);
		}

		// Token: 0x06000CC3 RID: 3267 RVA: 0x000276D6 File Offset: 0x000258D6
		public MessageActionProperty GetExtendedProperty(string nameSpace, string name)
		{
			return this.extendedProperties.GetExtendedProperty(nameSpace, name);
		}

		// Token: 0x06000CC4 RID: 3268 RVA: 0x000276E5 File Offset: 0x000258E5
		public IEnumerable<MessageActionProperty> GetExtendedPropertiesEnumerable()
		{
			return this.extendedProperties.GetExtendedPropertiesEnumerable();
		}

		// Token: 0x06000CC5 RID: 3269 RVA: 0x000276F2 File Offset: 0x000258F2
		public void AddExtendedProperty(MessageActionProperty extendedProperty)
		{
			this.extendedProperties.AddExtendedProperty(extendedProperty);
			extendedProperty.RuleActionId = this.RuleActionId;
		}

		// Token: 0x06000CC6 RID: 3270 RVA: 0x0002770C File Offset: 0x0002590C
		public override void Accept(IMessageTraceVisitor visitor)
		{
			visitor.Visit(this);
			foreach (MessageActionProperty messageActionProperty in this.GetExtendedPropertiesEnumerable())
			{
				messageActionProperty.Accept(visitor);
			}
		}

		// Token: 0x06000CC7 RID: 3271 RVA: 0x00027760 File Offset: 0x00025960
		public override Type GetSchemaType()
		{
			return typeof(MessageActionSchema);
		}

		// Token: 0x06000CC8 RID: 3272 RVA: 0x0002776C File Offset: 0x0002596C
		public override HygienePropertyDefinition[] GetAllProperties()
		{
			return MessageAction.Properties;
		}

		// Token: 0x04000657 RID: 1623
		internal static readonly HygienePropertyDefinition[] Properties = new HygienePropertyDefinition[]
		{
			MessageActionSchema.RuleActionIdProperty,
			CommonMessageTraceSchema.ExMessageIdProperty,
			MessageActionSchema.EventRuleIdProperty,
			MessageActionSchema.NameProperty,
			CommonMessageTraceSchema.RuleIdProperty,
			CommonMessageTraceSchema.EventHashKeyProperty
		};

		// Token: 0x04000658 RID: 1624
		private ExtendedPropertyStore<MessageActionProperty> extendedProperties;
	}
}
