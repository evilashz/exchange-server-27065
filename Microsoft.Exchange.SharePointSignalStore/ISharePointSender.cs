using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.SharePointSignalStore
{
	// Token: 0x02000008 RID: 8
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal interface ISharePointSender<T>
	{
		// Token: 0x06000023 RID: 35
		void SetData(T data);

		// Token: 0x06000024 RID: 36
		void Send();
	}
}
