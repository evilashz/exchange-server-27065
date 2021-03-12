using System;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x02000B67 RID: 2919
	internal abstract class TypedAsyncResult<T> : AsyncResultBase
	{
		// Token: 0x060052B5 RID: 21173 RVA: 0x0010B8AC File Offset: 0x00109AAC
		protected TypedAsyncResult(AsyncCallback callback, object state) : base(callback, state)
		{
		}

		// Token: 0x1700141B RID: 5147
		// (get) Token: 0x060052B6 RID: 21174 RVA: 0x0010B8B6 File Offset: 0x00109AB6
		public T Data
		{
			get
			{
				return this.data;
			}
		}

		// Token: 0x060052B7 RID: 21175 RVA: 0x0010B8BE File Offset: 0x00109ABE
		protected void Complete(T data, bool completedSynchronously)
		{
			this.data = data;
			base.Complete(completedSynchronously);
		}

		// Token: 0x060052B8 RID: 21176 RVA: 0x0010B8D0 File Offset: 0x00109AD0
		public static T End(IAsyncResult result)
		{
			TypedAsyncResult<T> typedAsyncResult = AsyncResultBase.End<TypedAsyncResult<T>>(result);
			return typedAsyncResult.Data;
		}

		// Token: 0x04002E12 RID: 11794
		private T data;
	}
}
