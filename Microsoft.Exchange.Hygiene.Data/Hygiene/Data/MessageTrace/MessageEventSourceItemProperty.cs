using System;

namespace Microsoft.Exchange.Hygiene.Data.MessageTrace
{
	// Token: 0x0200015F RID: 351
	internal sealed class MessageEventSourceItemProperty : PropertyBase
	{
		// Token: 0x06000DE1 RID: 3553 RVA: 0x000298E2 File Offset: 0x00027AE2
		public MessageEventSourceItemProperty(string nameSpace, string name, object value) : base(nameSpace, name, value)
		{
		}

		// Token: 0x06000DE2 RID: 3554 RVA: 0x000298ED File Offset: 0x00027AED
		public MessageEventSourceItemProperty(string nameSpace, string name, int value) : base(nameSpace, name, new int?(value))
		{
		}

		// Token: 0x06000DE3 RID: 3555 RVA: 0x000298FD File Offset: 0x00027AFD
		public MessageEventSourceItemProperty(string nameSpace, string name, string value) : base(nameSpace, name, value)
		{
		}

		// Token: 0x06000DE4 RID: 3556 RVA: 0x00029908 File Offset: 0x00027B08
		public MessageEventSourceItemProperty(string nameSpace, string name, DateTime value) : base(nameSpace, name, new DateTime?(value))
		{
		}

		// Token: 0x06000DE5 RID: 3557 RVA: 0x00029918 File Offset: 0x00027B18
		public MessageEventSourceItemProperty(string nameSpace, string name, decimal value) : base(nameSpace, name, new decimal?(value))
		{
		}

		// Token: 0x06000DE6 RID: 3558 RVA: 0x00029928 File Offset: 0x00027B28
		public MessageEventSourceItemProperty(string nameSpace, string name, BlobType value) : base(nameSpace, name, value)
		{
		}

		// Token: 0x06000DE7 RID: 3559 RVA: 0x00029933 File Offset: 0x00027B33
		public MessageEventSourceItemProperty(string nameSpace, string name, Guid value) : base(nameSpace, name, value)
		{
		}

		// Token: 0x06000DE8 RID: 3560 RVA: 0x0002993E File Offset: 0x00027B3E
		public MessageEventSourceItemProperty(string nameSpace, string name, long value) : base(nameSpace, name, new long?(value))
		{
		}

		// Token: 0x06000DE9 RID: 3561 RVA: 0x0002994E File Offset: 0x00027B4E
		public MessageEventSourceItemProperty(string nameSpace, string name, bool value) : base(nameSpace, name, value)
		{
		}

		// Token: 0x06000DEA RID: 3562 RVA: 0x00029959 File Offset: 0x00027B59
		public MessageEventSourceItemProperty()
		{
		}

		// Token: 0x17000427 RID: 1063
		// (get) Token: 0x06000DEB RID: 3563 RVA: 0x00029961 File Offset: 0x00027B61
		// (set) Token: 0x06000DEC RID: 3564 RVA: 0x00029973 File Offset: 0x00027B73
		public Guid SourceItemPropertyId
		{
			get
			{
				return (Guid)this[PropertyBase.PropertyIdProperty];
			}
			set
			{
				this[PropertyBase.PropertyIdProperty] = value;
			}
		}

		// Token: 0x17000428 RID: 1064
		// (get) Token: 0x06000DED RID: 3565 RVA: 0x00029986 File Offset: 0x00027B86
		// (set) Token: 0x06000DEE RID: 3566 RVA: 0x00029998 File Offset: 0x00027B98
		public Guid SourceItemId
		{
			get
			{
				return (Guid)this[PropertyBase.ParentIdProperty];
			}
			set
			{
				this[PropertyBase.ParentIdProperty] = value;
			}
		}

		// Token: 0x06000DEF RID: 3567 RVA: 0x000299AB File Offset: 0x00027BAB
		public override void Accept(IMessageTraceVisitor visitor)
		{
			visitor.Visit(this);
		}

		// Token: 0x06000DF0 RID: 3568 RVA: 0x000299B4 File Offset: 0x00027BB4
		public override HygienePropertyDefinition[] GetAllProperties()
		{
			return MessageEventSourceItemProperty.Properties;
		}

		// Token: 0x0400069B RID: 1691
		internal static readonly HygienePropertyDefinition[] Properties = new HygienePropertyDefinition[]
		{
			PropertyBase.PropertyIdProperty,
			PropertyBase.ParentIdProperty,
			CommonMessageTraceSchema.ExMessageIdProperty,
			PropertyBase.NamespaceProperty,
			PropertyBase.PropertyNameProperty,
			PropertyBase.PropertyIndexProperty,
			PropertyBase.PropertyValueGuidProperty,
			PropertyBase.PropertyValueIntegerProperty,
			PropertyBase.PropertyValueLongProperty,
			PropertyBase.PropertyValueStringProperty,
			PropertyBase.PropertyValueDatetimeProperty,
			PropertyBase.PropertyValueBitProperty,
			PropertyBase.PropertyValueDecimalProperty,
			PropertyBase.PropertyValueBlobProperty,
			PropertyBase.EventHashKeyProperty,
			PropertyBase.EmailHashKeyProperty,
			PropertyBase.ParentObjectIdProperty,
			PropertyBase.RefObjectIdProperty,
			PropertyBase.RefNameProperty,
			PropertyBase.PropIdProperty
		};
	}
}
