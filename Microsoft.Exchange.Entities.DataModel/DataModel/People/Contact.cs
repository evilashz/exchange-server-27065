using System;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Entities.DataModel.PropertyBags;

namespace Microsoft.Exchange.Entities.DataModel.People
{
	// Token: 0x0200009A RID: 154
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal sealed class Contact : IStorageEntity, IEntity, IPropertyChangeTracker<PropertyDefinition>, IVersioned
	{
		// Token: 0x17000192 RID: 402
		// (get) Token: 0x060003EB RID: 1003 RVA: 0x000074B0 File Offset: 0x000056B0
		// (set) Token: 0x060003EC RID: 1004 RVA: 0x000074B8 File Offset: 0x000056B8
		public IStorePropertyBag PropertyBag { get; set; }

		// Token: 0x17000193 RID: 403
		// (get) Token: 0x060003ED RID: 1005 RVA: 0x000074C1 File Offset: 0x000056C1
		// (set) Token: 0x060003EE RID: 1006 RVA: 0x000074C8 File Offset: 0x000056C8
		public string Id
		{
			get
			{
				throw new NotImplementedException();
			}
			set
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x17000194 RID: 404
		// (get) Token: 0x060003EF RID: 1007 RVA: 0x000074CF File Offset: 0x000056CF
		// (set) Token: 0x060003F0 RID: 1008 RVA: 0x000074D6 File Offset: 0x000056D6
		public string ChangeKey
		{
			get
			{
				throw new NotImplementedException();
			}
			set
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x060003F1 RID: 1009 RVA: 0x000074DD File Offset: 0x000056DD
		public bool IsPropertySet(PropertyDefinition property)
		{
			throw new NotImplementedException();
		}
	}
}
