using System;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Entities.TypeConversion.Converters;

namespace Microsoft.Exchange.Entities.EntitySets
{
	// Token: 0x02000030 RID: 48
	internal interface IStorageEntitySetScope<out TStoreSession> where TStoreSession : IStoreSession
	{
		// Token: 0x17000029 RID: 41
		// (get) Token: 0x060000F4 RID: 244
		TStoreSession StoreSession { get; }

		// Token: 0x1700002A RID: 42
		// (get) Token: 0x060000F5 RID: 245
		IXSOFactory XsoFactory { get; }

		// Token: 0x1700002B RID: 43
		// (get) Token: 0x060000F6 RID: 246
		IdConverter IdConverter { get; }

		// Token: 0x1700002C RID: 44
		// (get) Token: 0x060000F7 RID: 247
		IRecipientSession RecipientSession { get; }
	}
}
