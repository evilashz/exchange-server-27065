using System;

namespace Microsoft.Exchange.Hygiene.Data.MessageTrace
{
	// Token: 0x02000150 RID: 336
	internal sealed class MessageClassificationProperty : PropertyBase
	{
		// Token: 0x06000D1E RID: 3358 RVA: 0x000283A6 File Offset: 0x000265A6
		public MessageClassificationProperty(string nameSpace, string name, object value) : base(nameSpace, name, value)
		{
		}

		// Token: 0x06000D1F RID: 3359 RVA: 0x000283B1 File Offset: 0x000265B1
		public MessageClassificationProperty(string nameSpace, string name, int value) : base(nameSpace, name, new int?(value))
		{
		}

		// Token: 0x06000D20 RID: 3360 RVA: 0x000283C1 File Offset: 0x000265C1
		public MessageClassificationProperty(string nameSpace, string name, string value) : base(nameSpace, name, value)
		{
		}

		// Token: 0x06000D21 RID: 3361 RVA: 0x000283CC File Offset: 0x000265CC
		public MessageClassificationProperty(string nameSpace, string name, DateTime value) : base(nameSpace, name, new DateTime?(value))
		{
		}

		// Token: 0x06000D22 RID: 3362 RVA: 0x000283DC File Offset: 0x000265DC
		public MessageClassificationProperty(string nameSpace, string name, decimal value) : base(nameSpace, name, new decimal?(value))
		{
		}

		// Token: 0x06000D23 RID: 3363 RVA: 0x000283EC File Offset: 0x000265EC
		public MessageClassificationProperty(string nameSpace, string name, BlobType value) : base(nameSpace, name, value)
		{
		}

		// Token: 0x06000D24 RID: 3364 RVA: 0x000283F7 File Offset: 0x000265F7
		public MessageClassificationProperty(string nameSpace, string name, Guid value) : base(nameSpace, name, value)
		{
		}

		// Token: 0x06000D25 RID: 3365 RVA: 0x00028402 File Offset: 0x00026602
		public MessageClassificationProperty(string nameSpace, string name, long value) : base(nameSpace, name, new long?(value))
		{
		}

		// Token: 0x06000D26 RID: 3366 RVA: 0x00028412 File Offset: 0x00026612
		public MessageClassificationProperty(string nameSpace, string name, bool value) : base(nameSpace, name, value)
		{
		}

		// Token: 0x06000D27 RID: 3367 RVA: 0x0002841D File Offset: 0x0002661D
		public MessageClassificationProperty()
		{
		}

		// Token: 0x170003FD RID: 1021
		// (get) Token: 0x06000D28 RID: 3368 RVA: 0x00028425 File Offset: 0x00026625
		// (set) Token: 0x06000D29 RID: 3369 RVA: 0x00028437 File Offset: 0x00026637
		public Guid ClassificationPropertyId
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

		// Token: 0x170003FE RID: 1022
		// (get) Token: 0x06000D2A RID: 3370 RVA: 0x0002844A File Offset: 0x0002664A
		// (set) Token: 0x06000D2B RID: 3371 RVA: 0x0002845C File Offset: 0x0002665C
		public Guid ClassificationId
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

		// Token: 0x06000D2C RID: 3372 RVA: 0x0002846F File Offset: 0x0002666F
		public override void Accept(IMessageTraceVisitor visitor)
		{
			visitor.Visit(this);
		}

		// Token: 0x06000D2D RID: 3373 RVA: 0x00028478 File Offset: 0x00026678
		public override HygienePropertyDefinition[] GetAllProperties()
		{
			return MessageClassificationProperty.Properties;
		}

		// Token: 0x04000674 RID: 1652
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
