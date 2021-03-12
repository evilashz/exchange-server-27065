using System;
using System.Diagnostics;

namespace Microsoft.Exchange.Hygiene.Data.MessageTrace
{
	// Token: 0x02000156 RID: 342
	[DebuggerDisplay("Name = {PropertyName}; Namespace = {Namespace}; Id = {PropertyId}")]
	internal sealed class MessageEventProperty : PropertyBase
	{
		// Token: 0x06000D6F RID: 3439 RVA: 0x00028C9A File Offset: 0x00026E9A
		public MessageEventProperty(string nameSpace, string name, object value) : base(nameSpace, name, value)
		{
		}

		// Token: 0x06000D70 RID: 3440 RVA: 0x00028CA5 File Offset: 0x00026EA5
		public MessageEventProperty(string nameSpace, string name, int value) : base(nameSpace, name, new int?(value))
		{
		}

		// Token: 0x06000D71 RID: 3441 RVA: 0x00028CB5 File Offset: 0x00026EB5
		public MessageEventProperty(string nameSpace, string name, string value) : base(nameSpace, name, value)
		{
		}

		// Token: 0x06000D72 RID: 3442 RVA: 0x00028CC0 File Offset: 0x00026EC0
		public MessageEventProperty(string nameSpace, string name, DateTime value) : base(nameSpace, name, new DateTime?(value))
		{
		}

		// Token: 0x06000D73 RID: 3443 RVA: 0x00028CD0 File Offset: 0x00026ED0
		public MessageEventProperty(string nameSpace, string name, decimal value) : base(nameSpace, name, new decimal?(value))
		{
		}

		// Token: 0x06000D74 RID: 3444 RVA: 0x00028CE0 File Offset: 0x00026EE0
		public MessageEventProperty(string nameSpace, string name, BlobType value) : base(nameSpace, name, value)
		{
		}

		// Token: 0x06000D75 RID: 3445 RVA: 0x00028CEB File Offset: 0x00026EEB
		public MessageEventProperty(string nameSpace, string name, Guid value) : base(nameSpace, name, value)
		{
		}

		// Token: 0x06000D76 RID: 3446 RVA: 0x00028CF6 File Offset: 0x00026EF6
		public MessageEventProperty(string nameSpace, string name, long value) : base(nameSpace, name, new long?(value))
		{
		}

		// Token: 0x06000D77 RID: 3447 RVA: 0x00028D06 File Offset: 0x00026F06
		public MessageEventProperty(string nameSpace, string name, bool value) : base(nameSpace, name, value)
		{
		}

		// Token: 0x06000D78 RID: 3448 RVA: 0x00028D11 File Offset: 0x00026F11
		public MessageEventProperty()
		{
		}

		// Token: 0x1700040F RID: 1039
		// (get) Token: 0x06000D79 RID: 3449 RVA: 0x00028D19 File Offset: 0x00026F19
		// (set) Token: 0x06000D7A RID: 3450 RVA: 0x00028D2B File Offset: 0x00026F2B
		public Guid EventPropertyId
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

		// Token: 0x17000410 RID: 1040
		// (get) Token: 0x06000D7B RID: 3451 RVA: 0x00028D3E File Offset: 0x00026F3E
		// (set) Token: 0x06000D7C RID: 3452 RVA: 0x00028D50 File Offset: 0x00026F50
		public Guid EventId
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

		// Token: 0x06000D7D RID: 3453 RVA: 0x00028D63 File Offset: 0x00026F63
		public override void Accept(IMessageTraceVisitor visitor)
		{
			visitor.Visit(this);
		}

		// Token: 0x06000D7E RID: 3454 RVA: 0x00028D6C File Offset: 0x00026F6C
		public override HygienePropertyDefinition[] GetAllProperties()
		{
			return MessageEventProperty.Properties;
		}

		// Token: 0x04000683 RID: 1667
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
