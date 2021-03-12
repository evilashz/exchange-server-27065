using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000014 RID: 20
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal interface IStoreObject : IStorePropertyBag, IPropertyBag, IReadOnlyPropertyBag, IDisposable
	{
		// Token: 0x17000090 RID: 144
		// (get) Token: 0x0600022F RID: 559
		StoreObjectId StoreObjectId { get; }

		// Token: 0x17000091 RID: 145
		// (get) Token: 0x06000230 RID: 560
		PersistablePropertyBag PropertyBag { get; }

		// Token: 0x17000092 RID: 146
		// (get) Token: 0x06000231 RID: 561
		// (set) Token: 0x06000232 RID: 562
		string ClassName { get; set; }

		// Token: 0x17000093 RID: 147
		// (get) Token: 0x06000233 RID: 563
		bool IsNew { get; }

		// Token: 0x17000094 RID: 148
		// (get) Token: 0x06000234 RID: 564
		IStoreSession Session { get; }

		// Token: 0x17000095 RID: 149
		// (get) Token: 0x06000235 RID: 565
		StoreObjectId ParentId { get; }

		// Token: 0x17000096 RID: 150
		// (get) Token: 0x06000236 RID: 566
		VersionedId Id { get; }

		// Token: 0x17000097 RID: 151
		// (get) Token: 0x06000237 RID: 567
		byte[] RecordKey { get; }

		// Token: 0x17000098 RID: 152
		// (get) Token: 0x06000238 RID: 568
		LocationIdentifierHelper LocationIdentifierHelperInstance { get; }
	}
}
