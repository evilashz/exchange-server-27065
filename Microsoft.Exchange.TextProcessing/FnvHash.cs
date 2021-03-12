using System;
using System.Collections.Generic;

namespace Microsoft.Exchange.TextProcessing
{
	// Token: 0x0200001B RID: 27
	internal class FnvHash
	{
		// Token: 0x06000115 RID: 277 RVA: 0x0000B56C File Offset: 0x0000976C
		public static ulong Fnv1A64(byte[] input)
		{
			ulong num = 14695981039346656037UL;
			if (input != null && input.Length != 0)
			{
				foreach (byte b in input)
				{
					num = (num ^ (ulong)b) * 1099511628211UL;
				}
			}
			return num;
		}

		// Token: 0x06000116 RID: 278 RVA: 0x0000B5B0 File Offset: 0x000097B0
		public static ulong Fnv1A64(string input)
		{
			ulong num = 14695981039346656037UL;
			if (!string.IsNullOrEmpty(input))
			{
				foreach (char c in input)
				{
					num = (num ^ (ulong)c) * 1099511628211UL;
				}
			}
			return num;
		}

		// Token: 0x06000117 RID: 279 RVA: 0x0000B5FC File Offset: 0x000097FC
		public static uint Fnv1A32(string input)
		{
			uint num = 2166136261U;
			if (!string.IsNullOrEmpty(input))
			{
				foreach (char c in input)
				{
					num = (num ^ (uint)c) * 16777619U;
				}
			}
			return num;
		}

		// Token: 0x06000118 RID: 280 RVA: 0x0000B640 File Offset: 0x00009840
		public static uint Fnv1A32(List<char> input)
		{
			uint num = 2166136261U;
			if (input != null && input.Count != 0)
			{
				foreach (char c in input)
				{
					num = (num ^ (uint)c) * 16777619U;
				}
			}
			return num;
		}
	}
}
