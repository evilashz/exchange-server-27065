using System;

namespace Microsoft.Exchange.Data
{
	// Token: 0x0200004F RID: 79
	public struct Result<T>
	{
		// Token: 0x06000245 RID: 581 RVA: 0x000089BC File Offset: 0x00006BBC
		public Result(T data, ProviderError error)
		{
			this.data = data;
			this.error = error;
		}

		// Token: 0x1700009C RID: 156
		// (get) Token: 0x06000246 RID: 582 RVA: 0x000089CC File Offset: 0x00006BCC
		public ProviderError Error
		{
			get
			{
				return this.error;
			}
		}

		// Token: 0x1700009D RID: 157
		// (get) Token: 0x06000247 RID: 583 RVA: 0x000089D4 File Offset: 0x00006BD4
		public T Data
		{
			get
			{
				return this.data;
			}
		}

		// Token: 0x040000E4 RID: 228
		private T data;

		// Token: 0x040000E5 RID: 229
		private ProviderError error;
	}
}
