using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x0200049C RID: 1180
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal abstract class ContactBase : Item, IContactBase, IItem, IStoreObject, IStorePropertyBag, IPropertyBag, IReadOnlyPropertyBag, IDisposable
	{
		// Token: 0x06003433 RID: 13363 RVA: 0x000D3C0F File Offset: 0x000D1E0F
		internal ContactBase(ICoreItem coreItem) : base(coreItem, false)
		{
		}

		// Token: 0x1700103B RID: 4155
		// (get) Token: 0x06003434 RID: 13364 RVA: 0x000D3C19 File Offset: 0x000D1E19
		// (set) Token: 0x06003435 RID: 13365 RVA: 0x000D3C36 File Offset: 0x000D1E36
		public string DisplayName
		{
			get
			{
				this.CheckDisposed("DisplayName::get");
				return base.GetValueOrDefault<string>(StoreObjectSchema.DisplayName, string.Empty);
			}
			set
			{
				this.CheckDisposed("DisplayName::set");
				this[StoreObjectSchema.DisplayName] = value;
			}
		}
	}
}
