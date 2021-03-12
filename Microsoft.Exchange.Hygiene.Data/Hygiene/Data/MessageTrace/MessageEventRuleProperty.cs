using System;

namespace Microsoft.Exchange.Hygiene.Data.MessageTrace
{
	// Token: 0x0200015B RID: 347
	internal sealed class MessageEventRuleProperty : PropertyBase
	{
		// Token: 0x06000DBB RID: 3515 RVA: 0x000294BC File Offset: 0x000276BC
		public MessageEventRuleProperty(string nameSpace, string name, object value) : base(nameSpace, name, value)
		{
		}

		// Token: 0x06000DBC RID: 3516 RVA: 0x000294C7 File Offset: 0x000276C7
		public MessageEventRuleProperty(string nameSpace, string name, int value) : base(nameSpace, name, new int?(value))
		{
		}

		// Token: 0x06000DBD RID: 3517 RVA: 0x000294D7 File Offset: 0x000276D7
		public MessageEventRuleProperty(string nameSpace, string name, string value) : base(nameSpace, name, value)
		{
		}

		// Token: 0x06000DBE RID: 3518 RVA: 0x000294E2 File Offset: 0x000276E2
		public MessageEventRuleProperty(string nameSpace, string name, DateTime value) : base(nameSpace, name, new DateTime?(value))
		{
		}

		// Token: 0x06000DBF RID: 3519 RVA: 0x000294F2 File Offset: 0x000276F2
		public MessageEventRuleProperty(string nameSpace, string name, decimal value) : base(nameSpace, name, new decimal?(value))
		{
		}

		// Token: 0x06000DC0 RID: 3520 RVA: 0x00029502 File Offset: 0x00027702
		public MessageEventRuleProperty(string nameSpace, string name, BlobType value) : base(nameSpace, name, value)
		{
		}

		// Token: 0x06000DC1 RID: 3521 RVA: 0x0002950D File Offset: 0x0002770D
		public MessageEventRuleProperty(string nameSpace, string name, Guid value) : base(nameSpace, name, value)
		{
		}

		// Token: 0x06000DC2 RID: 3522 RVA: 0x00029518 File Offset: 0x00027718
		public MessageEventRuleProperty(string nameSpace, string name, long value) : base(nameSpace, name, new long?(value))
		{
		}

		// Token: 0x06000DC3 RID: 3523 RVA: 0x00029528 File Offset: 0x00027728
		public MessageEventRuleProperty(string nameSpace, string name, bool value) : base(nameSpace, name, value)
		{
		}

		// Token: 0x06000DC4 RID: 3524 RVA: 0x00029533 File Offset: 0x00027733
		public MessageEventRuleProperty()
		{
		}

		// Token: 0x17000420 RID: 1056
		// (get) Token: 0x06000DC5 RID: 3525 RVA: 0x0002953B File Offset: 0x0002773B
		// (set) Token: 0x06000DC6 RID: 3526 RVA: 0x0002954D File Offset: 0x0002774D
		public Guid EventRulePropertyId
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

		// Token: 0x17000421 RID: 1057
		// (get) Token: 0x06000DC7 RID: 3527 RVA: 0x00029560 File Offset: 0x00027760
		// (set) Token: 0x06000DC8 RID: 3528 RVA: 0x00029572 File Offset: 0x00027772
		public Guid EventRuleId
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

		// Token: 0x06000DC9 RID: 3529 RVA: 0x00029585 File Offset: 0x00027785
		public override void Accept(IMessageTraceVisitor visitor)
		{
			visitor.Visit(this);
		}

		// Token: 0x06000DCA RID: 3530 RVA: 0x0002958E File Offset: 0x0002778E
		public override HygienePropertyDefinition[] GetAllProperties()
		{
			return MessageEventRuleProperty.Properties;
		}

		// Token: 0x0400068F RID: 1679
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
