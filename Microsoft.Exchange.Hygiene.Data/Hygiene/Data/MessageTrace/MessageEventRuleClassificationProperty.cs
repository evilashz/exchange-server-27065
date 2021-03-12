using System;

namespace Microsoft.Exchange.Hygiene.Data.MessageTrace
{
	// Token: 0x02000159 RID: 345
	internal sealed class MessageEventRuleClassificationProperty : PropertyBase
	{
		// Token: 0x06000DAA RID: 3498 RVA: 0x0002930E File Offset: 0x0002750E
		public MessageEventRuleClassificationProperty(string nameSpace, string name, object value) : base(nameSpace, name, value)
		{
		}

		// Token: 0x06000DAB RID: 3499 RVA: 0x00029319 File Offset: 0x00027519
		public MessageEventRuleClassificationProperty(string nameSpace, string name, int value) : base(nameSpace, name, new int?(value))
		{
		}

		// Token: 0x06000DAC RID: 3500 RVA: 0x00029329 File Offset: 0x00027529
		public MessageEventRuleClassificationProperty(string nameSpace, string name, string value) : base(nameSpace, name, value)
		{
		}

		// Token: 0x06000DAD RID: 3501 RVA: 0x00029334 File Offset: 0x00027534
		public MessageEventRuleClassificationProperty(string nameSpace, string name, DateTime value) : base(nameSpace, name, new DateTime?(value))
		{
		}

		// Token: 0x06000DAE RID: 3502 RVA: 0x00029344 File Offset: 0x00027544
		public MessageEventRuleClassificationProperty(string nameSpace, string name, decimal value) : base(nameSpace, name, new decimal?(value))
		{
		}

		// Token: 0x06000DAF RID: 3503 RVA: 0x00029354 File Offset: 0x00027554
		public MessageEventRuleClassificationProperty(string nameSpace, string name, BlobType value) : base(nameSpace, name, value)
		{
		}

		// Token: 0x06000DB0 RID: 3504 RVA: 0x0002935F File Offset: 0x0002755F
		public MessageEventRuleClassificationProperty(string nameSpace, string name, Guid value) : base(nameSpace, name, value)
		{
		}

		// Token: 0x06000DB1 RID: 3505 RVA: 0x0002936A File Offset: 0x0002756A
		public MessageEventRuleClassificationProperty(string nameSpace, string name, long value) : base(nameSpace, name, new long?(value))
		{
		}

		// Token: 0x06000DB2 RID: 3506 RVA: 0x0002937A File Offset: 0x0002757A
		public MessageEventRuleClassificationProperty(string nameSpace, string name, bool value) : base(nameSpace, name, value)
		{
		}

		// Token: 0x06000DB3 RID: 3507 RVA: 0x00029385 File Offset: 0x00027585
		public MessageEventRuleClassificationProperty()
		{
		}

		// Token: 0x1700041F RID: 1055
		// (get) Token: 0x06000DB4 RID: 3508 RVA: 0x0002938D File Offset: 0x0002758D
		// (set) Token: 0x06000DB5 RID: 3509 RVA: 0x0002939F File Offset: 0x0002759F
		public Guid EventRuleClassificationId
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

		// Token: 0x06000DB6 RID: 3510 RVA: 0x000293B2 File Offset: 0x000275B2
		public override void Accept(IMessageTraceVisitor visitor)
		{
			visitor.Visit(this);
		}

		// Token: 0x06000DB7 RID: 3511 RVA: 0x000293BB File Offset: 0x000275BB
		public override HygienePropertyDefinition[] GetAllProperties()
		{
			return MessageEventRuleClassificationProperty.Properties;
		}

		// Token: 0x0400068A RID: 1674
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
