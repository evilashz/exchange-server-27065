using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data;

namespace Microsoft.Exchange.Hygiene.Data.MessageTrace
{
	// Token: 0x0200015E RID: 350
	internal sealed class MessageEventSourceItem : MessageTraceEntityBase, IExtendedPropertyStore<MessageEventSourceItemProperty>
	{
		// Token: 0x06000DD0 RID: 3536 RVA: 0x00029744 File Offset: 0x00027944
		public MessageEventSourceItem()
		{
			this.extendedProperties = new ExtendedPropertyStore<MessageEventSourceItemProperty>();
			this.SourceItemId = IdGenerator.GenerateIdentifier(IdScope.MessageEventSourceItem);
		}

		// Token: 0x17000422 RID: 1058
		// (get) Token: 0x06000DD1 RID: 3537 RVA: 0x00029763 File Offset: 0x00027963
		public override ObjectId Identity
		{
			get
			{
				return new ConfigObjectId(this[MessageEventSourceItemSchema.SourceItemIdProperty].ToString());
			}
		}

		// Token: 0x17000423 RID: 1059
		// (get) Token: 0x06000DD2 RID: 3538 RVA: 0x0002977A File Offset: 0x0002797A
		// (set) Token: 0x06000DD3 RID: 3539 RVA: 0x0002978C File Offset: 0x0002798C
		public Guid SourceItemId
		{
			get
			{
				return (Guid)this[MessageEventSourceItemSchema.SourceItemIdProperty];
			}
			set
			{
				this[MessageEventSourceItemSchema.SourceItemIdProperty] = value;
			}
		}

		// Token: 0x17000424 RID: 1060
		// (get) Token: 0x06000DD4 RID: 3540 RVA: 0x0002979F File Offset: 0x0002799F
		// (set) Token: 0x06000DD5 RID: 3541 RVA: 0x000297B1 File Offset: 0x000279B1
		public Guid EventId
		{
			get
			{
				return (Guid)this[MessageEventSourceItemSchema.EventIdProperty];
			}
			set
			{
				this[MessageEventSourceItemSchema.EventIdProperty] = value;
			}
		}

		// Token: 0x17000425 RID: 1061
		// (get) Token: 0x06000DD6 RID: 3542 RVA: 0x000297C4 File Offset: 0x000279C4
		// (set) Token: 0x06000DD7 RID: 3543 RVA: 0x000297D6 File Offset: 0x000279D6
		public string Name
		{
			get
			{
				return (string)this[MessageEventSourceItemSchema.NameProperty];
			}
			set
			{
				this[MessageEventSourceItemSchema.NameProperty] = value;
			}
		}

		// Token: 0x17000426 RID: 1062
		// (get) Token: 0x06000DD8 RID: 3544 RVA: 0x000297E4 File Offset: 0x000279E4
		public int ExtendedPropertiesCount
		{
			get
			{
				return this.extendedProperties.ExtendedPropertiesCount;
			}
		}

		// Token: 0x06000DD9 RID: 3545 RVA: 0x000297F1 File Offset: 0x000279F1
		public bool TryGetExtendedProperty(string nameSpace, string name, out MessageEventSourceItemProperty extendedProperty)
		{
			return this.extendedProperties.TryGetExtendedProperty(nameSpace, name, out extendedProperty);
		}

		// Token: 0x06000DDA RID: 3546 RVA: 0x00029801 File Offset: 0x00027A01
		public MessageEventSourceItemProperty GetExtendedProperty(string nameSpace, string name)
		{
			return this.extendedProperties.GetExtendedProperty(nameSpace, name);
		}

		// Token: 0x06000DDB RID: 3547 RVA: 0x00029810 File Offset: 0x00027A10
		public IEnumerable<MessageEventSourceItemProperty> GetExtendedPropertiesEnumerable()
		{
			return this.extendedProperties.GetExtendedPropertiesEnumerable();
		}

		// Token: 0x06000DDC RID: 3548 RVA: 0x0002981D File Offset: 0x00027A1D
		public void AddExtendedProperty(MessageEventSourceItemProperty extendedProperty)
		{
			this.extendedProperties.AddExtendedProperty(extendedProperty);
			extendedProperty.SourceItemId = this.SourceItemId;
		}

		// Token: 0x06000DDD RID: 3549 RVA: 0x00029838 File Offset: 0x00027A38
		public override void Accept(IMessageTraceVisitor visitor)
		{
			visitor.Visit(this);
			foreach (MessageEventSourceItemProperty messageEventSourceItemProperty in this.GetExtendedPropertiesEnumerable())
			{
				messageEventSourceItemProperty.Accept(visitor);
			}
		}

		// Token: 0x06000DDE RID: 3550 RVA: 0x0002988C File Offset: 0x00027A8C
		public override Type GetSchemaType()
		{
			return typeof(MessageEventSourceItemSchema);
		}

		// Token: 0x06000DDF RID: 3551 RVA: 0x00029898 File Offset: 0x00027A98
		public override HygienePropertyDefinition[] GetAllProperties()
		{
			return MessageEventSourceItem.Properties;
		}

		// Token: 0x04000699 RID: 1689
		internal static readonly HygienePropertyDefinition[] Properties = new HygienePropertyDefinition[]
		{
			MessageEventSourceItemSchema.SourceItemIdProperty,
			MessageEventSourceItemSchema.EventIdProperty,
			MessageEventSourceItemSchema.NameProperty,
			CommonMessageTraceSchema.ExMessageIdProperty,
			CommonMessageTraceSchema.EventHashKeyProperty
		};

		// Token: 0x0400069A RID: 1690
		private ExtendedPropertyStore<MessageEventSourceItemProperty> extendedProperties;
	}
}
