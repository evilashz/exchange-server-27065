using System;

namespace Microsoft.Exchange.Configuration.Tasks
{
	// Token: 0x02000068 RID: 104
	public class GenericEventArg<T> : EventArgs
	{
		// Token: 0x17000107 RID: 263
		// (get) Token: 0x06000434 RID: 1076 RVA: 0x0000F225 File Offset: 0x0000D425
		// (set) Token: 0x06000435 RID: 1077 RVA: 0x0000F22D File Offset: 0x0000D42D
		public T Data { get; set; }

		// Token: 0x06000436 RID: 1078 RVA: 0x0000F236 File Offset: 0x0000D436
		public GenericEventArg(T data)
		{
			this.Data = data;
		}
	}
}
