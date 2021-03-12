using System;

namespace System.Text
{
	// Token: 0x02000A2C RID: 2604
	internal static class StringBuilderCache
	{
		// Token: 0x0600661A RID: 26138 RVA: 0x00157690 File Offset: 0x00155890
		public static StringBuilder Acquire(int capacity = 16)
		{
			if (capacity <= 360)
			{
				StringBuilder cachedInstance = StringBuilderCache.CachedInstance;
				if (cachedInstance != null && capacity <= cachedInstance.Capacity)
				{
					StringBuilderCache.CachedInstance = null;
					cachedInstance.Clear();
					return cachedInstance;
				}
			}
			return new StringBuilder(capacity);
		}

		// Token: 0x0600661B RID: 26139 RVA: 0x001576CC File Offset: 0x001558CC
		public static void Release(StringBuilder sb)
		{
			if (sb.Capacity <= 360)
			{
				StringBuilderCache.CachedInstance = sb;
			}
		}

		// Token: 0x0600661C RID: 26140 RVA: 0x001576E4 File Offset: 0x001558E4
		public static string GetStringAndRelease(StringBuilder sb)
		{
			string result = sb.ToString();
			StringBuilderCache.Release(sb);
			return result;
		}

		// Token: 0x04002D6C RID: 11628
		internal const int MAX_BUILDER_SIZE = 360;

		// Token: 0x04002D6D RID: 11629
		[ThreadStatic]
		private static StringBuilder CachedInstance;
	}
}
