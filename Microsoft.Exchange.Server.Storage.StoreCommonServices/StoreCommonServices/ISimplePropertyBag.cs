using System;
using System.IO;
using Microsoft.Exchange.Server.Storage.Common;
using Microsoft.Exchange.Server.Storage.PropTags;

namespace Microsoft.Exchange.Server.Storage.StoreCommonServices
{
	// Token: 0x0200008A RID: 138
	public interface ISimplePropertyBag : ISimpleReadOnlyPropertyBag, ISimplePropertyStorage, ISimpleReadOnlyPropertyStorage
	{
		// Token: 0x17000151 RID: 337
		// (get) Token: 0x060004F3 RID: 1267
		IReplidGuidMap ReplidGuidMap { get; }

		// Token: 0x060004F4 RID: 1268
		ErrorCode SetProperty(Context context, StorePropTag propTag, object value);

		// Token: 0x060004F5 RID: 1269
		ErrorCode OpenPropertyReadStream(Context context, StorePropTag propTag, out Stream stream);

		// Token: 0x060004F6 RID: 1270
		ErrorCode OpenPropertyWriteStream(Context context, StorePropTag propTag, out Stream stream);
	}
}
