using System;
using System.Globalization;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Transport.RightsManagement
{
	// Token: 0x020003D1 RID: 977
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class AsyncOperationResult<TData>
	{
		// Token: 0x06002CBD RID: 11453 RVA: 0x000B24AB File Offset: 0x000B06AB
		public AsyncOperationResult(TData data, Exception exception)
		{
			this.data = data;
			this.exception = exception;
		}

		// Token: 0x17000D8F RID: 3471
		// (get) Token: 0x06002CBE RID: 11454 RVA: 0x000B24C1 File Offset: 0x000B06C1
		public Exception Exception
		{
			get
			{
				return this.exception;
			}
		}

		// Token: 0x17000D90 RID: 3472
		// (get) Token: 0x06002CBF RID: 11455 RVA: 0x000B24C9 File Offset: 0x000B06C9
		public TData Data
		{
			get
			{
				return this.data;
			}
		}

		// Token: 0x17000D91 RID: 3473
		// (get) Token: 0x06002CC0 RID: 11456 RVA: 0x000B24D1 File Offset: 0x000B06D1
		public bool IsSucceeded
		{
			get
			{
				return this.exception == null;
			}
		}

		// Token: 0x17000D92 RID: 3474
		// (get) Token: 0x06002CC1 RID: 11457 RVA: 0x000B24DC File Offset: 0x000B06DC
		public virtual bool IsTransientException
		{
			get
			{
				return this.exception is TransientException;
			}
		}

		// Token: 0x06002CC2 RID: 11458 RVA: 0x000B24EC File Offset: 0x000B06EC
		public override string ToString()
		{
			if (this.IsSucceeded)
			{
				return "Success";
			}
			return string.Format(CultureInfo.InvariantCulture, "{0}: {1}", new object[]
			{
				this.exception.GetType().FullName,
				this.exception.Message
			});
		}

		// Token: 0x04001654 RID: 5716
		private readonly TData data;

		// Token: 0x04001655 RID: 5717
		private readonly Exception exception;
	}
}
