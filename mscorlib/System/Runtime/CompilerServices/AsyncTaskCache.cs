using System;
using System.Threading;
using System.Threading.Tasks;

namespace System.Runtime.CompilerServices
{
	// Token: 0x020008C4 RID: 2244
	internal static class AsyncTaskCache
	{
		// Token: 0x06005D10 RID: 23824 RVA: 0x00146438 File Offset: 0x00144638
		private static Task<int>[] CreateInt32Tasks()
		{
			Task<int>[] array = new Task<int>[10];
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = AsyncTaskCache.CreateCacheableTask<int>(i + -1);
			}
			return array;
		}

		// Token: 0x06005D11 RID: 23825 RVA: 0x00146468 File Offset: 0x00144668
		internal static Task<TResult> CreateCacheableTask<TResult>(TResult result)
		{
			return new Task<TResult>(false, result, (TaskCreationOptions)16384, default(CancellationToken));
		}

		// Token: 0x04002993 RID: 10643
		internal static readonly Task<bool> TrueTask = AsyncTaskCache.CreateCacheableTask<bool>(true);

		// Token: 0x04002994 RID: 10644
		internal static readonly Task<bool> FalseTask = AsyncTaskCache.CreateCacheableTask<bool>(false);

		// Token: 0x04002995 RID: 10645
		internal static readonly Task<int>[] Int32Tasks = AsyncTaskCache.CreateInt32Tasks();

		// Token: 0x04002996 RID: 10646
		internal const int INCLUSIVE_INT32_MIN = -1;

		// Token: 0x04002997 RID: 10647
		internal const int EXCLUSIVE_INT32_MAX = 9;
	}
}
