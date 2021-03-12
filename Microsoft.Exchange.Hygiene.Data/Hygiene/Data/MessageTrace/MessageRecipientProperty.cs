using System;

namespace Microsoft.Exchange.Hygiene.Data.MessageTrace
{
	// Token: 0x02000164 RID: 356
	internal sealed class MessageRecipientProperty : PropertyBase
	{
		// Token: 0x06000E19 RID: 3609 RVA: 0x00029E5A File Offset: 0x0002805A
		public MessageRecipientProperty(string nameSpace, string name, object value) : base(nameSpace, name, value)
		{
		}

		// Token: 0x06000E1A RID: 3610 RVA: 0x00029E65 File Offset: 0x00028065
		public MessageRecipientProperty(string nameSpace, string name, int value) : base(nameSpace, name, new int?(value))
		{
		}

		// Token: 0x06000E1B RID: 3611 RVA: 0x00029E75 File Offset: 0x00028075
		public MessageRecipientProperty(string nameSpace, string name, string value) : base(nameSpace, name, value)
		{
		}

		// Token: 0x06000E1C RID: 3612 RVA: 0x00029E80 File Offset: 0x00028080
		public MessageRecipientProperty(string nameSpace, string name, DateTime value) : base(nameSpace, name, new DateTime?(value))
		{
		}

		// Token: 0x06000E1D RID: 3613 RVA: 0x00029E90 File Offset: 0x00028090
		public MessageRecipientProperty(string nameSpace, string name, decimal value) : base(nameSpace, name, new decimal?(value))
		{
		}

		// Token: 0x06000E1E RID: 3614 RVA: 0x00029EA0 File Offset: 0x000280A0
		public MessageRecipientProperty(string nameSpace, string name, BlobType value) : base(nameSpace, name, value)
		{
		}

		// Token: 0x06000E1F RID: 3615 RVA: 0x00029EAB File Offset: 0x000280AB
		public MessageRecipientProperty(string nameSpace, string name, Guid value) : base(nameSpace, name, value)
		{
		}

		// Token: 0x06000E20 RID: 3616 RVA: 0x00029EB6 File Offset: 0x000280B6
		public MessageRecipientProperty(string nameSpace, string name, long value) : base(nameSpace, name, new long?(value))
		{
		}

		// Token: 0x06000E21 RID: 3617 RVA: 0x00029EC6 File Offset: 0x000280C6
		public MessageRecipientProperty(string nameSpace, string name, bool value) : base(nameSpace, name, value)
		{
		}

		// Token: 0x06000E22 RID: 3618 RVA: 0x00029ED1 File Offset: 0x000280D1
		public MessageRecipientProperty()
		{
		}

		// Token: 0x17000431 RID: 1073
		// (get) Token: 0x06000E23 RID: 3619 RVA: 0x00029ED9 File Offset: 0x000280D9
		// (set) Token: 0x06000E24 RID: 3620 RVA: 0x00029EEB File Offset: 0x000280EB
		public Guid RecipientId
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

		// Token: 0x06000E25 RID: 3621 RVA: 0x00029EFE File Offset: 0x000280FE
		public override void Accept(IMessageTraceVisitor visitor)
		{
			visitor.Visit(this);
		}

		// Token: 0x06000E26 RID: 3622 RVA: 0x00029F07 File Offset: 0x00028107
		public override HygienePropertyDefinition[] GetAllProperties()
		{
			return MessageRecipientProperty.Properties;
		}

		// Token: 0x040006A8 RID: 1704
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
