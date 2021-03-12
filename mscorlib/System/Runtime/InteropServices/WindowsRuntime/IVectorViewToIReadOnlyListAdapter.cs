using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Security;
using System.StubHelpers;

namespace System.Runtime.InteropServices.WindowsRuntime
{
	// Token: 0x020009BD RID: 2493
	[DebuggerDisplay("Count = {Count}")]
	internal sealed class IVectorViewToIReadOnlyListAdapter
	{
		// Token: 0x06006385 RID: 25477 RVA: 0x00152283 File Offset: 0x00150483
		private IVectorViewToIReadOnlyListAdapter()
		{
		}

		// Token: 0x06006386 RID: 25478 RVA: 0x0015228C File Offset: 0x0015048C
		[SecurityCritical]
		internal T Indexer_Get<T>(int index)
		{
			if (index < 0)
			{
				throw new ArgumentOutOfRangeException("index");
			}
			IVectorView<T> vectorView = JitHelpers.UnsafeCast<IVectorView<T>>(this);
			T at;
			try
			{
				at = vectorView.GetAt((uint)index);
			}
			catch (Exception ex)
			{
				if (-2147483637 == ex._HResult)
				{
					throw new ArgumentOutOfRangeException("index");
				}
				throw;
			}
			return at;
		}

		// Token: 0x06006387 RID: 25479 RVA: 0x001522E8 File Offset: 0x001504E8
		[SecurityCritical]
		internal T Indexer_Get_Variance<T>(int index) where T : class
		{
			bool flag;
			Delegate targetForAmbiguousVariantCall = StubHelpers.GetTargetForAmbiguousVariantCall(this, typeof(IReadOnlyList<T>).TypeHandle.Value, out flag);
			if (targetForAmbiguousVariantCall != null)
			{
				return JitHelpers.UnsafeCast<Indexer_Get_Delegate<T>>(targetForAmbiguousVariantCall)(index);
			}
			if (flag)
			{
				return JitHelpers.UnsafeCast<T>(this.Indexer_Get<string>(index));
			}
			return this.Indexer_Get<T>(index);
		}
	}
}
