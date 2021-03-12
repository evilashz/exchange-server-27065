using System;

namespace Microsoft.Exchange.Hygiene.Data.MessageTrace
{
	// Token: 0x02000153 RID: 339
	internal sealed class MessageClientInformationProperty : PropertyBase
	{
		// Token: 0x06000D41 RID: 3393 RVA: 0x000286EA File Offset: 0x000268EA
		public MessageClientInformationProperty(string nameSpace, string name, object value) : base(nameSpace, name, value)
		{
		}

		// Token: 0x06000D42 RID: 3394 RVA: 0x000286F5 File Offset: 0x000268F5
		public MessageClientInformationProperty(string nameSpace, string name, int value) : base(nameSpace, name, new int?(value))
		{
		}

		// Token: 0x06000D43 RID: 3395 RVA: 0x00028705 File Offset: 0x00026905
		public MessageClientInformationProperty(string nameSpace, string name, string value) : base(nameSpace, name, value)
		{
		}

		// Token: 0x06000D44 RID: 3396 RVA: 0x00028710 File Offset: 0x00026910
		public MessageClientInformationProperty(string nameSpace, string name, DateTime value) : base(nameSpace, name, new DateTime?(value))
		{
		}

		// Token: 0x06000D45 RID: 3397 RVA: 0x00028720 File Offset: 0x00026920
		public MessageClientInformationProperty(string nameSpace, string name, decimal value) : base(nameSpace, name, new decimal?(value))
		{
		}

		// Token: 0x06000D46 RID: 3398 RVA: 0x00028730 File Offset: 0x00026930
		public MessageClientInformationProperty(string nameSpace, string name, BlobType value) : base(nameSpace, name, value)
		{
		}

		// Token: 0x06000D47 RID: 3399 RVA: 0x0002873B File Offset: 0x0002693B
		public MessageClientInformationProperty(string nameSpace, string name, Guid value) : base(nameSpace, name, value)
		{
		}

		// Token: 0x06000D48 RID: 3400 RVA: 0x00028746 File Offset: 0x00026946
		public MessageClientInformationProperty(string nameSpace, string name, long value) : base(nameSpace, name, new long?(value))
		{
		}

		// Token: 0x06000D49 RID: 3401 RVA: 0x00028756 File Offset: 0x00026956
		public MessageClientInformationProperty(string nameSpace, string name, bool value) : base(nameSpace, name, value)
		{
		}

		// Token: 0x06000D4A RID: 3402 RVA: 0x00028761 File Offset: 0x00026961
		public MessageClientInformationProperty()
		{
		}

		// Token: 0x17000403 RID: 1027
		// (get) Token: 0x06000D4B RID: 3403 RVA: 0x00028769 File Offset: 0x00026969
		// (set) Token: 0x06000D4C RID: 3404 RVA: 0x0002877B File Offset: 0x0002697B
		public Guid ClientInformationPropertyId
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

		// Token: 0x17000404 RID: 1028
		// (get) Token: 0x06000D4D RID: 3405 RVA: 0x0002878E File Offset: 0x0002698E
		// (set) Token: 0x06000D4E RID: 3406 RVA: 0x000287A0 File Offset: 0x000269A0
		public Guid ClientInformationId
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

		// Token: 0x06000D4F RID: 3407 RVA: 0x000287B3 File Offset: 0x000269B3
		public override void Accept(IMessageTraceVisitor visitor)
		{
			visitor.Visit(this);
		}

		// Token: 0x06000D50 RID: 3408 RVA: 0x000287BC File Offset: 0x000269BC
		public override HygienePropertyDefinition[] GetAllProperties()
		{
			return MessageClientInformationProperty.Properties;
		}

		// Token: 0x0400067A RID: 1658
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
