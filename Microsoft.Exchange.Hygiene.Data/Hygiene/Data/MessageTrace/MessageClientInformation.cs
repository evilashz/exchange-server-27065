using System;
using System.Collections.Generic;

namespace Microsoft.Exchange.Hygiene.Data.MessageTrace
{
	// Token: 0x02000152 RID: 338
	internal class MessageClientInformation : MessageTraceEntityBase, IExtendedPropertyStore<MessageClientInformationProperty>
	{
		// Token: 0x06000D31 RID: 3377 RVA: 0x0002856E File Offset: 0x0002676E
		public MessageClientInformation()
		{
			this.extendedProperties = new ExtendedPropertyStore<MessageClientInformationProperty>();
			this.ClientInformationId = IdGenerator.GenerateIdentifier(IdScope.MessageClientInformation);
		}

		// Token: 0x170003FF RID: 1023
		// (get) Token: 0x06000D32 RID: 3378 RVA: 0x0002858D File Offset: 0x0002678D
		// (set) Token: 0x06000D33 RID: 3379 RVA: 0x0002859F File Offset: 0x0002679F
		public Guid ClientInformationId
		{
			get
			{
				return (Guid)this[MessageClientInformationSchema.ClientInformationIdProperty];
			}
			set
			{
				this[MessageClientInformationSchema.ClientInformationIdProperty] = value;
			}
		}

		// Token: 0x17000400 RID: 1024
		// (get) Token: 0x06000D34 RID: 3380 RVA: 0x000285B2 File Offset: 0x000267B2
		// (set) Token: 0x06000D35 RID: 3381 RVA: 0x000285C4 File Offset: 0x000267C4
		public Guid DataClassificationId
		{
			get
			{
				return (Guid)this[MessageClientInformationSchema.DataClassificationIdProperty];
			}
			set
			{
				this[MessageClientInformationSchema.DataClassificationIdProperty] = value;
			}
		}

		// Token: 0x17000401 RID: 1025
		// (get) Token: 0x06000D36 RID: 3382 RVA: 0x000285D7 File Offset: 0x000267D7
		// (set) Token: 0x06000D37 RID: 3383 RVA: 0x000285E9 File Offset: 0x000267E9
		public Guid ExMessageId
		{
			get
			{
				return (Guid)this[CommonMessageTraceSchema.ExMessageIdProperty];
			}
			set
			{
				this[CommonMessageTraceSchema.ExMessageIdProperty] = value;
			}
		}

		// Token: 0x17000402 RID: 1026
		// (get) Token: 0x06000D38 RID: 3384 RVA: 0x000285FC File Offset: 0x000267FC
		public int ExtendedPropertiesCount
		{
			get
			{
				return this.extendedProperties.ExtendedPropertiesCount;
			}
		}

		// Token: 0x06000D39 RID: 3385 RVA: 0x00028609 File Offset: 0x00026809
		public bool TryGetExtendedProperty(string nameSpace, string name, out MessageClientInformationProperty extendedProperty)
		{
			return this.extendedProperties.TryGetExtendedProperty(nameSpace, name, out extendedProperty);
		}

		// Token: 0x06000D3A RID: 3386 RVA: 0x00028619 File Offset: 0x00026819
		public MessageClientInformationProperty GetExtendedProperty(string nameSpace, string name)
		{
			return this.extendedProperties.GetExtendedProperty(nameSpace, name);
		}

		// Token: 0x06000D3B RID: 3387 RVA: 0x00028628 File Offset: 0x00026828
		public IEnumerable<MessageClientInformationProperty> GetExtendedPropertiesEnumerable()
		{
			return this.extendedProperties.GetExtendedPropertiesEnumerable();
		}

		// Token: 0x06000D3C RID: 3388 RVA: 0x00028635 File Offset: 0x00026835
		public void AddExtendedProperty(MessageClientInformationProperty extendedProperty)
		{
			this.extendedProperties.AddExtendedProperty(extendedProperty);
			extendedProperty.ClientInformationId = this.ClientInformationId;
		}

		// Token: 0x06000D3D RID: 3389 RVA: 0x00028650 File Offset: 0x00026850
		public override void Accept(IMessageTraceVisitor visitor)
		{
			visitor.Visit(this);
			foreach (MessageClientInformationProperty messageClientInformationProperty in this.GetExtendedPropertiesEnumerable())
			{
				messageClientInformationProperty.Accept(visitor);
			}
		}

		// Token: 0x06000D3E RID: 3390 RVA: 0x000286A4 File Offset: 0x000268A4
		public override Type GetSchemaType()
		{
			return typeof(MessageClientInformation);
		}

		// Token: 0x06000D3F RID: 3391 RVA: 0x000286B0 File Offset: 0x000268B0
		public override HygienePropertyDefinition[] GetAllProperties()
		{
			return MessageClientInformation.Properties;
		}

		// Token: 0x04000678 RID: 1656
		internal static readonly HygienePropertyDefinition[] Properties = new HygienePropertyDefinition[]
		{
			MessageClientInformationSchema.ClientInformationIdProperty,
			MessageClientInformationSchema.DataClassificationIdProperty,
			CommonMessageTraceSchema.ExMessageIdProperty
		};

		// Token: 0x04000679 RID: 1657
		private ExtendedPropertyStore<MessageClientInformationProperty> extendedProperties;
	}
}
