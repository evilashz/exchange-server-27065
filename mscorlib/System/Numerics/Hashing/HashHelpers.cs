using System;

namespace System.Numerics.Hashing
{
	// Token: 0x0200035C RID: 860
	internal static class HashHelpers
	{
		// Token: 0x06002BB8 RID: 11192 RVA: 0x000A4708 File Offset: 0x000A2908
		public static int Combine(int h1, int h2)
		{
			uint num = (uint)(h1 << 5 | (int)((uint)h1 >> 27));
			return (int)(num + (uint)h1 ^ (uint)h2);
		}
	}
}
