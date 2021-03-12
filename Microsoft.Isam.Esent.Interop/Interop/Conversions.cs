using System;
using System.Collections.Generic;
using System.Globalization;

namespace Microsoft.Isam.Esent.Interop
{
	// Token: 0x0200009F RID: 159
	public static class Conversions
	{
		// Token: 0x06000746 RID: 1862 RVA: 0x00010EB4 File Offset: 0x0000F0B4
		public static DateTime ConvertDoubleToDateTime(double d)
		{
			DateTime result;
			try
			{
				result = LibraryHelpers.FromOADate(d);
			}
			catch (ArgumentException)
			{
				result = ((d < 0.0) ? DateTime.MinValue : DateTime.MaxValue);
			}
			return result;
		}

		// Token: 0x06000747 RID: 1863 RVA: 0x00010EF8 File Offset: 0x0000F0F8
		[CLSCompliant(false)]
		public static CompareOptions CompareOptionsFromLCMapFlags(uint lcmapFlags)
		{
			CompareOptions compareOptions = CompareOptions.None;
			foreach (uint num in Conversions.LcmapFlagsToCompareOptions.Keys)
			{
				if (num == (lcmapFlags & num))
				{
					compareOptions |= Conversions.LcmapFlagsToCompareOptions[num];
				}
			}
			return compareOptions;
		}

		// Token: 0x06000748 RID: 1864 RVA: 0x00010F5C File Offset: 0x0000F15C
		[CLSCompliant(false)]
		public static uint LCMapFlagsFromCompareOptions(CompareOptions compareOptions)
		{
			uint num = 0U;
			foreach (CompareOptions compareOptions2 in Conversions.CompareOptionsToLcmapFlags.Keys)
			{
				if (compareOptions2 == (compareOptions & compareOptions2))
				{
					num |= Conversions.CompareOptionsToLcmapFlags[compareOptions2];
				}
			}
			return num;
		}

		// Token: 0x06000749 RID: 1865 RVA: 0x00010FC0 File Offset: 0x0000F1C0
		private static IDictionary<TKey, TValue> InvertDictionary<TValue, TKey>(ICollection<KeyValuePair<TValue, TKey>> dict)
		{
			Dictionary<TKey, TValue> dictionary = new Dictionary<TKey, TValue>(dict.Count);
			foreach (KeyValuePair<TValue, TKey> keyValuePair in dict)
			{
				dictionary.Add(keyValuePair.Value, keyValuePair.Key);
			}
			return dictionary;
		}

		// Token: 0x0400032C RID: 812
		private static readonly IDictionary<CompareOptions, uint> CompareOptionsToLcmapFlags = new Dictionary<CompareOptions, uint>
		{
			{
				CompareOptions.IgnoreCase,
				1U
			},
			{
				CompareOptions.IgnoreKanaType,
				65536U
			},
			{
				CompareOptions.IgnoreNonSpace,
				2U
			},
			{
				CompareOptions.IgnoreSymbols,
				4U
			},
			{
				CompareOptions.IgnoreWidth,
				131072U
			},
			{
				CompareOptions.StringSort,
				4096U
			}
		};

		// Token: 0x0400032D RID: 813
		private static readonly IDictionary<uint, CompareOptions> LcmapFlagsToCompareOptions = Conversions.InvertDictionary<CompareOptions, uint>(Conversions.CompareOptionsToLcmapFlags);

		// Token: 0x020000A0 RID: 160
		internal static class NativeMethods
		{
			// Token: 0x0400032E RID: 814
			public const uint NORM_IGNORECASE = 1U;

			// Token: 0x0400032F RID: 815
			public const uint NORM_IGNORENONSPACE = 2U;

			// Token: 0x04000330 RID: 816
			public const uint NORM_IGNORESYMBOLS = 4U;

			// Token: 0x04000331 RID: 817
			public const uint NORM_IGNOREKANATYPE = 65536U;

			// Token: 0x04000332 RID: 818
			public const uint NORM_IGNOREWIDTH = 131072U;

			// Token: 0x04000333 RID: 819
			public const uint SORT_STRINGSORT = 4096U;

			// Token: 0x04000334 RID: 820
			public const uint LCMAP_SORTKEY = 1024U;
		}
	}
}
