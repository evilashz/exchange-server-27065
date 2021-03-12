using System;

namespace Microsoft.Exchange.Hygiene.Data.MessageTrace
{
	// Token: 0x02000167 RID: 359
	internal sealed class MessageRecipientStatusProperty : PropertyBase
	{
		// Token: 0x06000E3F RID: 3647 RVA: 0x0002A246 File Offset: 0x00028446
		public MessageRecipientStatusProperty(string nameSpace, string name, object value) : base(nameSpace, name, value)
		{
		}

		// Token: 0x06000E40 RID: 3648 RVA: 0x0002A251 File Offset: 0x00028451
		public MessageRecipientStatusProperty(string nameSpace, string name, int value) : base(nameSpace, name, new int?(value))
		{
		}

		// Token: 0x06000E41 RID: 3649 RVA: 0x0002A261 File Offset: 0x00028461
		public MessageRecipientStatusProperty(string nameSpace, string name, string value) : base(nameSpace, name, value)
		{
		}

		// Token: 0x06000E42 RID: 3650 RVA: 0x0002A26C File Offset: 0x0002846C
		public MessageRecipientStatusProperty(string nameSpace, string name, DateTime value) : base(nameSpace, name, new DateTime?(value))
		{
		}

		// Token: 0x06000E43 RID: 3651 RVA: 0x0002A27C File Offset: 0x0002847C
		public MessageRecipientStatusProperty(string nameSpace, string name, decimal value) : base(nameSpace, name, new decimal?(value))
		{
		}

		// Token: 0x06000E44 RID: 3652 RVA: 0x0002A28C File Offset: 0x0002848C
		public MessageRecipientStatusProperty(string nameSpace, string name, BlobType value) : base(nameSpace, name, value)
		{
		}

		// Token: 0x06000E45 RID: 3653 RVA: 0x0002A297 File Offset: 0x00028497
		public MessageRecipientStatusProperty(string nameSpace, string name, Guid value) : base(nameSpace, name, value)
		{
		}

		// Token: 0x06000E46 RID: 3654 RVA: 0x0002A2A2 File Offset: 0x000284A2
		public MessageRecipientStatusProperty(string nameSpace, string name, long value) : base(nameSpace, name, new long?(value))
		{
		}

		// Token: 0x06000E47 RID: 3655 RVA: 0x0002A2B2 File Offset: 0x000284B2
		public MessageRecipientStatusProperty(string nameSpace, string name, bool value) : base(nameSpace, name, value)
		{
		}

		// Token: 0x06000E48 RID: 3656 RVA: 0x0002A2BD File Offset: 0x000284BD
		public MessageRecipientStatusProperty()
		{
		}

		// Token: 0x17000439 RID: 1081
		// (get) Token: 0x06000E49 RID: 3657 RVA: 0x0002A2C5 File Offset: 0x000284C5
		// (set) Token: 0x06000E4A RID: 3658 RVA: 0x0002A2D7 File Offset: 0x000284D7
		public Guid RecipientStatusId
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

		// Token: 0x06000E4B RID: 3659 RVA: 0x0002A2EA File Offset: 0x000284EA
		public override void Accept(IMessageTraceVisitor visitor)
		{
			visitor.Visit(this);
		}

		// Token: 0x06000E4C RID: 3660 RVA: 0x0002A2F3 File Offset: 0x000284F3
		public override HygienePropertyDefinition[] GetAllProperties()
		{
			return MessageRecipientStatusProperty.Properties;
		}

		// Token: 0x040006B0 RID: 1712
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
