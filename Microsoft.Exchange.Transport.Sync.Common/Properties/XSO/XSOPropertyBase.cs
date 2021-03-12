using System;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Transport.Sync.Common.Properties.XSO
{
	// Token: 0x02000090 RID: 144
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal abstract class XSOPropertyBase<T> : IXSOProperty<T>, IProperty<Item, T>, IWriteableProperty<Item, T>, IReadableProperty<Item, T>
	{
		// Token: 0x060003D0 RID: 976 RVA: 0x00015B30 File Offset: 0x00013D30
		protected XSOPropertyBase(IXSOPropertyManager propertyManager, params PropertyDefinition[] propertyDefinitions)
		{
			SyncUtilities.ThrowIfArgumentNull("propertyManager", propertyManager);
			SyncUtilities.ThrowIfArgumentNullOrEmpty("propertyDefinitions", propertyDefinitions);
			foreach (PropertyDefinition propertyDefinition in propertyDefinitions)
			{
				propertyManager.AddPropertyDefinition(propertyDefinition);
			}
		}

		// Token: 0x060003D1 RID: 977
		public abstract T ReadProperty(Item item);

		// Token: 0x060003D2 RID: 978
		public abstract void WriteProperty(Item item, T value);
	}
}
