using System;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x02000B68 RID: 2920
	internal class TypedCompletedAsyncResult<T> : TypedAsyncResult<T>
	{
		// Token: 0x060052B9 RID: 21177 RVA: 0x0010B8EA File Offset: 0x00109AEA
		public TypedCompletedAsyncResult(T data, AsyncCallback callback, object state) : base(callback, state)
		{
			base.Complete(data, true);
		}

		// Token: 0x060052BA RID: 21178 RVA: 0x0010B8FC File Offset: 0x00109AFC
		public new static T End(IAsyncResult result)
		{
			TypedCompletedAsyncResult<T> typedCompletedAsyncResult = result as TypedCompletedAsyncResult<T>;
			if (typedCompletedAsyncResult == null)
			{
				throw new ArgumentException("Invalid async result.", "result");
			}
			return TypedAsyncResult<T>.End(typedCompletedAsyncResult);
		}
	}
}
