using System;

namespace System
{
	// Token: 0x0200005F RID: 95
	[__DynamicallyInvokable]
	public static class Tuple
	{
		// Token: 0x06000349 RID: 841 RVA: 0x00008607 File Offset: 0x00006807
		[__DynamicallyInvokable]
		public static Tuple<T1> Create<T1>(T1 item1)
		{
			return new Tuple<T1>(item1);
		}

		// Token: 0x0600034A RID: 842 RVA: 0x0000860F File Offset: 0x0000680F
		[__DynamicallyInvokable]
		public static Tuple<T1, T2> Create<T1, T2>(T1 item1, T2 item2)
		{
			return new Tuple<T1, T2>(item1, item2);
		}

		// Token: 0x0600034B RID: 843 RVA: 0x00008618 File Offset: 0x00006818
		[__DynamicallyInvokable]
		public static Tuple<T1, T2, T3> Create<T1, T2, T3>(T1 item1, T2 item2, T3 item3)
		{
			return new Tuple<T1, T2, T3>(item1, item2, item3);
		}

		// Token: 0x0600034C RID: 844 RVA: 0x00008622 File Offset: 0x00006822
		[__DynamicallyInvokable]
		public static Tuple<T1, T2, T3, T4> Create<T1, T2, T3, T4>(T1 item1, T2 item2, T3 item3, T4 item4)
		{
			return new Tuple<T1, T2, T3, T4>(item1, item2, item3, item4);
		}

		// Token: 0x0600034D RID: 845 RVA: 0x0000862D File Offset: 0x0000682D
		[__DynamicallyInvokable]
		public static Tuple<T1, T2, T3, T4, T5> Create<T1, T2, T3, T4, T5>(T1 item1, T2 item2, T3 item3, T4 item4, T5 item5)
		{
			return new Tuple<T1, T2, T3, T4, T5>(item1, item2, item3, item4, item5);
		}

		// Token: 0x0600034E RID: 846 RVA: 0x0000863A File Offset: 0x0000683A
		[__DynamicallyInvokable]
		public static Tuple<T1, T2, T3, T4, T5, T6> Create<T1, T2, T3, T4, T5, T6>(T1 item1, T2 item2, T3 item3, T4 item4, T5 item5, T6 item6)
		{
			return new Tuple<T1, T2, T3, T4, T5, T6>(item1, item2, item3, item4, item5, item6);
		}

		// Token: 0x0600034F RID: 847 RVA: 0x00008649 File Offset: 0x00006849
		[__DynamicallyInvokable]
		public static Tuple<T1, T2, T3, T4, T5, T6, T7> Create<T1, T2, T3, T4, T5, T6, T7>(T1 item1, T2 item2, T3 item3, T4 item4, T5 item5, T6 item6, T7 item7)
		{
			return new Tuple<T1, T2, T3, T4, T5, T6, T7>(item1, item2, item3, item4, item5, item6, item7);
		}

		// Token: 0x06000350 RID: 848 RVA: 0x0000865A File Offset: 0x0000685A
		[__DynamicallyInvokable]
		public static Tuple<T1, T2, T3, T4, T5, T6, T7, Tuple<T8>> Create<T1, T2, T3, T4, T5, T6, T7, T8>(T1 item1, T2 item2, T3 item3, T4 item4, T5 item5, T6 item6, T7 item7, T8 item8)
		{
			return new Tuple<T1, T2, T3, T4, T5, T6, T7, Tuple<T8>>(item1, item2, item3, item4, item5, item6, item7, new Tuple<T8>(item8));
		}

		// Token: 0x06000351 RID: 849 RVA: 0x00008672 File Offset: 0x00006872
		internal static int CombineHashCodes(int h1, int h2)
		{
			return (h1 << 5) + h1 ^ h2;
		}

		// Token: 0x06000352 RID: 850 RVA: 0x0000867B File Offset: 0x0000687B
		internal static int CombineHashCodes(int h1, int h2, int h3)
		{
			return Tuple.CombineHashCodes(Tuple.CombineHashCodes(h1, h2), h3);
		}

		// Token: 0x06000353 RID: 851 RVA: 0x0000868A File Offset: 0x0000688A
		internal static int CombineHashCodes(int h1, int h2, int h3, int h4)
		{
			return Tuple.CombineHashCodes(Tuple.CombineHashCodes(h1, h2), Tuple.CombineHashCodes(h3, h4));
		}

		// Token: 0x06000354 RID: 852 RVA: 0x0000869F File Offset: 0x0000689F
		internal static int CombineHashCodes(int h1, int h2, int h3, int h4, int h5)
		{
			return Tuple.CombineHashCodes(Tuple.CombineHashCodes(h1, h2, h3, h4), h5);
		}

		// Token: 0x06000355 RID: 853 RVA: 0x000086B1 File Offset: 0x000068B1
		internal static int CombineHashCodes(int h1, int h2, int h3, int h4, int h5, int h6)
		{
			return Tuple.CombineHashCodes(Tuple.CombineHashCodes(h1, h2, h3, h4), Tuple.CombineHashCodes(h5, h6));
		}

		// Token: 0x06000356 RID: 854 RVA: 0x000086CA File Offset: 0x000068CA
		internal static int CombineHashCodes(int h1, int h2, int h3, int h4, int h5, int h6, int h7)
		{
			return Tuple.CombineHashCodes(Tuple.CombineHashCodes(h1, h2, h3, h4), Tuple.CombineHashCodes(h5, h6, h7));
		}

		// Token: 0x06000357 RID: 855 RVA: 0x000086E5 File Offset: 0x000068E5
		internal static int CombineHashCodes(int h1, int h2, int h3, int h4, int h5, int h6, int h7, int h8)
		{
			return Tuple.CombineHashCodes(Tuple.CombineHashCodes(h1, h2, h3, h4), Tuple.CombineHashCodes(h5, h6, h7, h8));
		}
	}
}
