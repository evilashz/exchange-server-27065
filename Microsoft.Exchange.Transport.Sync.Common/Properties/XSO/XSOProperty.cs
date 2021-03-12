using System;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Transport.Sync.Common.Properties.XSO
{
	// Token: 0x02000091 RID: 145
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class XSOProperty<T> : XSOPropertyBase<T>
	{
		// Token: 0x060003D3 RID: 979 RVA: 0x00015B74 File Offset: 0x00013D74
		public XSOProperty(IXSOPropertyManager propertyManager, PropertyDefinition propertyDefinition) : base(propertyManager, new PropertyDefinition[]
		{
			propertyDefinition
		})
		{
			this.propertyDefinition = propertyDefinition;
		}

		// Token: 0x060003D4 RID: 980 RVA: 0x00015B9B File Offset: 0x00013D9B
		public override T ReadProperty(Item item)
		{
			SyncUtilities.ThrowIfArgumentNull("item", item);
			return SyncUtilities.SafeGetProperty<T>(item, this.propertyDefinition);
		}

		// Token: 0x060003D5 RID: 981 RVA: 0x00015BB4 File Offset: 0x00013DB4
		public override void WriteProperty(Item item, T value)
		{
			SyncUtilities.ThrowIfArgumentNull("item", item);
			if (!object.Equals(default(T), value))
			{
				item[this.propertyDefinition] = value;
			}
		}

		// Token: 0x040001EC RID: 492
		private PropertyDefinition propertyDefinition;
	}
}
