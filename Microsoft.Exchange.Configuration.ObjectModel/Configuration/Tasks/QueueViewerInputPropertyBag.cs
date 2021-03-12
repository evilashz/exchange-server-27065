using System;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.QueueViewer;

namespace Microsoft.Exchange.Configuration.Tasks
{
	// Token: 0x02000015 RID: 21
	[Serializable]
	internal class QueueViewerInputPropertyBag : PropertyBag
	{
		// Token: 0x060000B6 RID: 182 RVA: 0x00004682 File Offset: 0x00002882
		public QueueViewerInputPropertyBag(bool readOnly, int initialSize) : base(readOnly, initialSize)
		{
			base.SetField(this.ObjectVersionPropertyDefinition, ExchangeObjectVersion.Exchange2010);
		}

		// Token: 0x060000B7 RID: 183 RVA: 0x0000469E File Offset: 0x0000289E
		public QueueViewerInputPropertyBag() : base(false, 16)
		{
			base.SetField(this.ObjectVersionPropertyDefinition, ExchangeObjectVersion.Exchange2010);
		}

		// Token: 0x1700002E RID: 46
		// (get) Token: 0x060000B8 RID: 184 RVA: 0x000046BB File Offset: 0x000028BB
		internal override ProviderPropertyDefinition ObjectVersionPropertyDefinition
		{
			get
			{
				return QueueViewerInputObjectSchema.ExchangeVersion;
			}
		}

		// Token: 0x1700002F RID: 47
		// (get) Token: 0x060000B9 RID: 185 RVA: 0x000046C2 File Offset: 0x000028C2
		internal override ProviderPropertyDefinition ObjectStatePropertyDefinition
		{
			get
			{
				return QueueViewerInputObjectSchema.ObjectState;
			}
		}

		// Token: 0x17000030 RID: 48
		// (get) Token: 0x060000BA RID: 186 RVA: 0x000046C9 File Offset: 0x000028C9
		internal override ProviderPropertyDefinition ObjectIdentityPropertyDefinition
		{
			get
			{
				return QueueViewerInputObjectSchema.Identity;
			}
		}

		// Token: 0x060000BB RID: 187 RVA: 0x000046D0 File Offset: 0x000028D0
		internal override object SerializeData(ProviderPropertyDefinition propertyDefinition, object input)
		{
			if (!(propertyDefinition.Type == typeof(IConfigurable)) || input == null)
			{
				return base.SerializeData(propertyDefinition, input);
			}
			if (input is ExtensibleMessageInfo)
			{
				return input as ExtensibleMessageInfo;
			}
			if (input is ExtensibleQueueInfo)
			{
				return input as ExtensibleQueueInfo;
			}
			throw new NotImplementedException();
		}

		// Token: 0x060000BC RID: 188 RVA: 0x00004724 File Offset: 0x00002924
		internal override object DeserializeData(ProviderPropertyDefinition propertyDefinition, object input)
		{
			if (!(propertyDefinition.Type == typeof(IConfigurable)) || input == null)
			{
				return base.DeserializeData(propertyDefinition, input);
			}
			if (input is ExtensibleMessageInfo)
			{
				return input as ExtensibleMessageInfo;
			}
			if (input is ExtensibleQueueInfo)
			{
				return input as ExtensibleQueueInfo;
			}
			throw new NotImplementedException();
		}
	}
}
