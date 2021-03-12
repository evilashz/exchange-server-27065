using System;

namespace Microsoft.Exchange.Hygiene.Data.MessageTrace
{
	// Token: 0x0200014D RID: 333
	internal sealed class MessageActionProperty : PropertyBase
	{
		// Token: 0x06000CFC RID: 3324 RVA: 0x0002803D File Offset: 0x0002623D
		public MessageActionProperty(string nameSpace, string name, object value) : base(nameSpace, name, value)
		{
		}

		// Token: 0x06000CFD RID: 3325 RVA: 0x00028048 File Offset: 0x00026248
		public MessageActionProperty(string nameSpace, string name, int value) : base(nameSpace, name, new int?(value))
		{
		}

		// Token: 0x06000CFE RID: 3326 RVA: 0x00028058 File Offset: 0x00026258
		public MessageActionProperty(string nameSpace, string name, string value) : base(nameSpace, name, value)
		{
		}

		// Token: 0x06000CFF RID: 3327 RVA: 0x00028063 File Offset: 0x00026263
		public MessageActionProperty(string nameSpace, string name, DateTime value) : base(nameSpace, name, new DateTime?(value))
		{
		}

		// Token: 0x06000D00 RID: 3328 RVA: 0x00028073 File Offset: 0x00026273
		public MessageActionProperty(string nameSpace, string name, decimal value) : base(nameSpace, name, new decimal?(value))
		{
		}

		// Token: 0x06000D01 RID: 3329 RVA: 0x00028083 File Offset: 0x00026283
		public MessageActionProperty(string nameSpace, string name, BlobType value) : base(nameSpace, name, value)
		{
		}

		// Token: 0x06000D02 RID: 3330 RVA: 0x0002808E File Offset: 0x0002628E
		public MessageActionProperty(string nameSpace, string name, Guid value) : base(nameSpace, name, value)
		{
		}

		// Token: 0x06000D03 RID: 3331 RVA: 0x00028099 File Offset: 0x00026299
		public MessageActionProperty(string nameSpace, string name, long value) : base(nameSpace, name, new long?(value))
		{
		}

		// Token: 0x06000D04 RID: 3332 RVA: 0x000280A9 File Offset: 0x000262A9
		public MessageActionProperty(string nameSpace, string name, bool value) : base(nameSpace, name, value)
		{
		}

		// Token: 0x06000D05 RID: 3333 RVA: 0x000280B4 File Offset: 0x000262B4
		public MessageActionProperty()
		{
		}

		// Token: 0x170003F7 RID: 1015
		// (get) Token: 0x06000D06 RID: 3334 RVA: 0x000280BC File Offset: 0x000262BC
		// (set) Token: 0x06000D07 RID: 3335 RVA: 0x000280CE File Offset: 0x000262CE
		public Guid RuleActionId
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

		// Token: 0x06000D08 RID: 3336 RVA: 0x000280E1 File Offset: 0x000262E1
		public override void Accept(IMessageTraceVisitor visitor)
		{
			visitor.Visit(this);
		}

		// Token: 0x06000D09 RID: 3337 RVA: 0x000280EA File Offset: 0x000262EA
		public override HygienePropertyDefinition[] GetAllProperties()
		{
			return MessageActionProperty.Properties;
		}

		// Token: 0x0400066E RID: 1646
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
