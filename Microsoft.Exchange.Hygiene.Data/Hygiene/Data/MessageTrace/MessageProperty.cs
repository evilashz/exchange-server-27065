using System;

namespace Microsoft.Exchange.Hygiene.Data.MessageTrace
{
	// Token: 0x02000161 RID: 353
	internal sealed class MessageProperty : PropertyBase
	{
		// Token: 0x06000DF4 RID: 3572 RVA: 0x00029AB9 File Offset: 0x00027CB9
		public MessageProperty(string nameSpace, string name, object value) : base(nameSpace, name, value)
		{
		}

		// Token: 0x06000DF5 RID: 3573 RVA: 0x00029AC4 File Offset: 0x00027CC4
		public MessageProperty(string nameSpace, string name, int value) : base(nameSpace, name, new int?(value))
		{
		}

		// Token: 0x06000DF6 RID: 3574 RVA: 0x00029AD4 File Offset: 0x00027CD4
		public MessageProperty(string nameSpace, string name, string value) : base(nameSpace, name, value)
		{
		}

		// Token: 0x06000DF7 RID: 3575 RVA: 0x00029ADF File Offset: 0x00027CDF
		public MessageProperty(string nameSpace, string name, DateTime value) : base(nameSpace, name, new DateTime?(value))
		{
		}

		// Token: 0x06000DF8 RID: 3576 RVA: 0x00029AEF File Offset: 0x00027CEF
		public MessageProperty(string nameSpace, string name, decimal value) : base(nameSpace, name, new decimal?(value))
		{
		}

		// Token: 0x06000DF9 RID: 3577 RVA: 0x00029AFF File Offset: 0x00027CFF
		public MessageProperty(string nameSpace, string name, BlobType value) : base(nameSpace, name, value)
		{
		}

		// Token: 0x06000DFA RID: 3578 RVA: 0x00029B0A File Offset: 0x00027D0A
		public MessageProperty(string nameSpace, string name, Guid value) : base(nameSpace, name, value)
		{
		}

		// Token: 0x06000DFB RID: 3579 RVA: 0x00029B15 File Offset: 0x00027D15
		public MessageProperty(string nameSpace, string name, long value) : base(nameSpace, name, new long?(value))
		{
		}

		// Token: 0x06000DFC RID: 3580 RVA: 0x00029B25 File Offset: 0x00027D25
		public MessageProperty(string nameSpace, string name, bool value) : base(nameSpace, name, value)
		{
		}

		// Token: 0x06000DFD RID: 3581 RVA: 0x00029B30 File Offset: 0x00027D30
		public MessageProperty()
		{
		}

		// Token: 0x17000429 RID: 1065
		// (get) Token: 0x06000DFE RID: 3582 RVA: 0x00029B38 File Offset: 0x00027D38
		// (set) Token: 0x06000DFF RID: 3583 RVA: 0x00029B4A File Offset: 0x00027D4A
		public new Guid ExMessageId
		{
			get
			{
				return (Guid)this[PropertyBase.ParentIdProperty];
			}
			set
			{
				this[PropertyBase.ParentIdProperty] = value;
				this[CommonMessageTraceSchema.ExMessageIdProperty] = value;
			}
		}

		// Token: 0x06000E00 RID: 3584 RVA: 0x00029B6E File Offset: 0x00027D6E
		public override void Accept(IMessageTraceVisitor visitor)
		{
			visitor.Visit(this);
		}

		// Token: 0x06000E01 RID: 3585 RVA: 0x00029B77 File Offset: 0x00027D77
		public override HygienePropertyDefinition[] GetAllProperties()
		{
			return MessageProperty.Properties;
		}

		// Token: 0x0400069F RID: 1695
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
