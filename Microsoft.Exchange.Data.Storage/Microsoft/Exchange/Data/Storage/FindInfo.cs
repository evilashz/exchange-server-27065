using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x0200049B RID: 1179
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal struct FindInfo<RESULT_TYPE> : IDisposable
	{
		// Token: 0x06003431 RID: 13361 RVA: 0x000D3BD8 File Offset: 0x000D1DD8
		public FindInfo(FindStatus findStatus, RESULT_TYPE result)
		{
			this.FindStatus = findStatus;
			this.Result = result;
		}

		// Token: 0x06003432 RID: 13362 RVA: 0x000D3BE8 File Offset: 0x000D1DE8
		public void Dispose()
		{
			IDisposable disposable = this.Result as IDisposable;
			if (disposable != null)
			{
				disposable.Dispose();
			}
		}

		// Token: 0x04001C03 RID: 7171
		public readonly FindStatus FindStatus;

		// Token: 0x04001C04 RID: 7172
		public readonly RESULT_TYPE Result;
	}
}
