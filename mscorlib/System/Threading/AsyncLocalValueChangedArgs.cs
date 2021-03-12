using System;

namespace System.Threading
{
	// Token: 0x020004BA RID: 1210
	[__DynamicallyInvokable]
	public struct AsyncLocalValueChangedArgs<T>
	{
		// Token: 0x170008EB RID: 2283
		// (get) Token: 0x06003A5E RID: 14942 RVA: 0x000DDAA6 File Offset: 0x000DBCA6
		// (set) Token: 0x06003A5F RID: 14943 RVA: 0x000DDAAE File Offset: 0x000DBCAE
		[__DynamicallyInvokable]
		public T PreviousValue { [__DynamicallyInvokable] get; private set; }

		// Token: 0x170008EC RID: 2284
		// (get) Token: 0x06003A60 RID: 14944 RVA: 0x000DDAB7 File Offset: 0x000DBCB7
		// (set) Token: 0x06003A61 RID: 14945 RVA: 0x000DDABF File Offset: 0x000DBCBF
		[__DynamicallyInvokable]
		public T CurrentValue { [__DynamicallyInvokable] get; private set; }

		// Token: 0x170008ED RID: 2285
		// (get) Token: 0x06003A62 RID: 14946 RVA: 0x000DDAC8 File Offset: 0x000DBCC8
		// (set) Token: 0x06003A63 RID: 14947 RVA: 0x000DDAD0 File Offset: 0x000DBCD0
		[__DynamicallyInvokable]
		public bool ThreadContextChanged { [__DynamicallyInvokable] get; private set; }

		// Token: 0x06003A64 RID: 14948 RVA: 0x000DDAD9 File Offset: 0x000DBCD9
		internal AsyncLocalValueChangedArgs(T previousValue, T currentValue, bool contextChanged)
		{
			this = default(AsyncLocalValueChangedArgs<T>);
			this.PreviousValue = previousValue;
			this.CurrentValue = currentValue;
			this.ThreadContextChanged = contextChanged;
		}
	}
}
