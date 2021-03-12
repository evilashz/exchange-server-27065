using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Hygiene.Data.MessageTrace
{
	// Token: 0x02000155 RID: 341
	internal class MessageEvent : MessageTraceEntityBase, IExtendedPropertyStore<MessageEventProperty>
	{
		// Token: 0x06000D54 RID: 3412 RVA: 0x000288D0 File Offset: 0x00026AD0
		public MessageEvent()
		{
			this.extendedProperties = new ExtendedPropertyStore<MessageEventProperty>();
			this.EventId = IdGenerator.GenerateIdentifier(IdScope.MessageEvent);
		}

		// Token: 0x17000405 RID: 1029
		// (get) Token: 0x06000D55 RID: 3413 RVA: 0x000288EF File Offset: 0x00026AEF
		public override ObjectId Identity
		{
			get
			{
				return new ConfigObjectId(this[MessageEventSchema.EventIdProperty].ToString());
			}
		}

		// Token: 0x17000406 RID: 1030
		// (get) Token: 0x06000D56 RID: 3414 RVA: 0x00028906 File Offset: 0x00026B06
		// (set) Token: 0x06000D57 RID: 3415 RVA: 0x00028918 File Offset: 0x00026B18
		public Guid ExMessageId
		{
			get
			{
				return (Guid)this[MessageEventSchema.ExMessageIdProperty];
			}
			set
			{
				this[MessageEventSchema.ExMessageIdProperty] = value;
			}
		}

		// Token: 0x17000407 RID: 1031
		// (get) Token: 0x06000D58 RID: 3416 RVA: 0x0002892B File Offset: 0x00026B2B
		// (set) Token: 0x06000D59 RID: 3417 RVA: 0x0002893D File Offset: 0x00026B3D
		public Guid EventId
		{
			get
			{
				return (Guid)this[MessageEventSchema.EventIdProperty];
			}
			set
			{
				this[MessageEventSchema.EventIdProperty] = value;
			}
		}

		// Token: 0x17000408 RID: 1032
		// (get) Token: 0x06000D5A RID: 3418 RVA: 0x00028950 File Offset: 0x00026B50
		// (set) Token: 0x06000D5B RID: 3419 RVA: 0x00028962 File Offset: 0x00026B62
		public MessageTrackingEvent EventType
		{
			get
			{
				return (MessageTrackingEvent)this[MessageEventSchema.EventTypeProperty];
			}
			set
			{
				this[MessageEventSchema.EventTypeProperty] = value;
			}
		}

		// Token: 0x17000409 RID: 1033
		// (get) Token: 0x06000D5C RID: 3420 RVA: 0x00028975 File Offset: 0x00026B75
		// (set) Token: 0x06000D5D RID: 3421 RVA: 0x00028987 File Offset: 0x00026B87
		public MessageTrackingSource EventSource
		{
			get
			{
				return (MessageTrackingSource)this[MessageEventSchema.EventSourceProperty];
			}
			set
			{
				this[MessageEventSchema.EventSourceProperty] = value;
			}
		}

		// Token: 0x1700040A RID: 1034
		// (get) Token: 0x06000D5E RID: 3422 RVA: 0x0002899A File Offset: 0x00026B9A
		// (set) Token: 0x06000D5F RID: 3423 RVA: 0x000289AC File Offset: 0x00026BAC
		public DateTime TimeStamp
		{
			get
			{
				return (DateTime)this[MessageEventSchema.TimeStampProperty];
			}
			set
			{
				this[MessageEventSchema.TimeStampProperty] = value;
			}
		}

		// Token: 0x1700040B RID: 1035
		// (get) Token: 0x06000D60 RID: 3424 RVA: 0x000289BF File Offset: 0x00026BBF
		public int ExtendedPropertiesCount
		{
			get
			{
				return this.extendedProperties.ExtendedPropertiesCount;
			}
		}

		// Token: 0x1700040C RID: 1036
		// (get) Token: 0x06000D61 RID: 3425 RVA: 0x000289CC File Offset: 0x00026BCC
		public List<MessageEventRule> Rules
		{
			get
			{
				List<MessageEventRule> result;
				if ((result = this.rules) == null)
				{
					result = (this.rules = new List<MessageEventRule>());
				}
				return result;
			}
		}

		// Token: 0x1700040D RID: 1037
		// (get) Token: 0x06000D62 RID: 3426 RVA: 0x000289F4 File Offset: 0x00026BF4
		public List<MessageEventSourceItem> SourceItems
		{
			get
			{
				List<MessageEventSourceItem> result;
				if ((result = this.sourceItems) == null)
				{
					result = (this.sourceItems = new List<MessageEventSourceItem>());
				}
				return result;
			}
		}

		// Token: 0x1700040E RID: 1038
		// (get) Token: 0x06000D63 RID: 3427 RVA: 0x00028A1C File Offset: 0x00026C1C
		public List<MessageRecipientStatus> Statuses
		{
			get
			{
				List<MessageRecipientStatus> result;
				if ((result = this.statuses) == null)
				{
					result = (this.statuses = new List<MessageRecipientStatus>());
				}
				return result;
			}
		}

		// Token: 0x06000D64 RID: 3428 RVA: 0x00028A41 File Offset: 0x00026C41
		public bool TryGetExtendedProperty(string nameSpace, string name, out MessageEventProperty extendedProperty)
		{
			return this.extendedProperties.TryGetExtendedProperty(nameSpace, name, out extendedProperty);
		}

		// Token: 0x06000D65 RID: 3429 RVA: 0x00028A51 File Offset: 0x00026C51
		public MessageEventProperty GetExtendedProperty(string nameSpace, string name)
		{
			return this.extendedProperties.GetExtendedProperty(nameSpace, name);
		}

		// Token: 0x06000D66 RID: 3430 RVA: 0x00028A60 File Offset: 0x00026C60
		public IEnumerable<MessageEventProperty> GetExtendedPropertiesEnumerable()
		{
			return this.extendedProperties.GetExtendedPropertiesEnumerable();
		}

		// Token: 0x06000D67 RID: 3431 RVA: 0x00028A6D File Offset: 0x00026C6D
		public void AddExtendedProperty(MessageEventProperty extendedProperty)
		{
			this.extendedProperties.AddExtendedProperty(extendedProperty);
			extendedProperty.EventId = this.EventId;
		}

		// Token: 0x06000D68 RID: 3432 RVA: 0x00028A88 File Offset: 0x00026C88
		public override void Accept(IMessageTraceVisitor visitor)
		{
			visitor.Visit(this);
			foreach (MessageEventProperty messageEventProperty in this.GetExtendedPropertiesEnumerable())
			{
				messageEventProperty.Accept(visitor);
			}
			if (this.sourceItems != null)
			{
				foreach (MessageEventSourceItem messageEventSourceItem in this.sourceItems)
				{
					messageEventSourceItem.Accept(visitor);
				}
			}
			if (this.rules != null)
			{
				foreach (MessageEventRule messageEventRule in this.rules)
				{
					messageEventRule.Accept(visitor);
				}
			}
			if (this.statuses != null)
			{
				foreach (MessageRecipientStatus messageRecipientStatus in this.statuses)
				{
					messageRecipientStatus.Accept(visitor);
				}
			}
		}

		// Token: 0x06000D69 RID: 3433 RVA: 0x00028BC4 File Offset: 0x00026DC4
		public void Add(MessageEventRule eventRule)
		{
			if (eventRule == null)
			{
				throw new ArgumentNullException("eventRule");
			}
			eventRule.EventId = this.EventId;
			this.Rules.Add(eventRule);
		}

		// Token: 0x06000D6A RID: 3434 RVA: 0x00028BEC File Offset: 0x00026DEC
		public void Add(MessageEventSourceItem sourceItem)
		{
			if (sourceItem == null)
			{
				throw new ArgumentNullException("sourceItem");
			}
			sourceItem.EventId = this.EventId;
			this.SourceItems.Add(sourceItem);
		}

		// Token: 0x06000D6B RID: 3435 RVA: 0x00028C14 File Offset: 0x00026E14
		public void Add(MessageRecipientStatus status)
		{
			if (status == null)
			{
				throw new ArgumentNullException("status");
			}
			status.EventId = this.EventId;
			this.Statuses.Add(status);
		}

		// Token: 0x06000D6C RID: 3436 RVA: 0x00028C3C File Offset: 0x00026E3C
		public override Type GetSchemaType()
		{
			return typeof(MessageEventSchema);
		}

		// Token: 0x06000D6D RID: 3437 RVA: 0x00028C48 File Offset: 0x00026E48
		public override HygienePropertyDefinition[] GetAllProperties()
		{
			return MessageEvent.Properties;
		}

		// Token: 0x0400067E RID: 1662
		internal static readonly HygienePropertyDefinition[] Properties = new HygienePropertyDefinition[]
		{
			CommonMessageTraceSchema.ExMessageIdProperty,
			CommonMessageTraceSchema.EventIdProperty,
			MessageEventSchema.EventTypeProperty,
			MessageEventSchema.EventSourceProperty,
			MessageEventSchema.TimeStampProperty,
			CommonMessageTraceSchema.EventHashKeyProperty
		};

		// Token: 0x0400067F RID: 1663
		private List<MessageEventRule> rules;

		// Token: 0x04000680 RID: 1664
		private List<MessageEventSourceItem> sourceItems;

		// Token: 0x04000681 RID: 1665
		private List<MessageRecipientStatus> statuses;

		// Token: 0x04000682 RID: 1666
		private ExtendedPropertyStore<MessageEventProperty> extendedProperties;
	}
}
