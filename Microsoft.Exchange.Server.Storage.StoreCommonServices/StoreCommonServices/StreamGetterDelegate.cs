using System;
using System.IO;
using Microsoft.Exchange.Server.Storage.Common;

namespace Microsoft.Exchange.Server.Storage.StoreCommonServices
{
	// Token: 0x020000F1 RID: 241
	// (Invoke) Token: 0x06000988 RID: 2440
	public delegate ErrorCode StreamGetterDelegate(Context context, ISimplePropertyBag bag, out Stream stream);
}
