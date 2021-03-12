using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Microsoft.Exchange.Server.Storage.Common.ExtensionMethods.Linq
{
	// Token: 0x0200004A RID: 74
	public static class LinqNamespaceExtensionMethods
	{
		// Token: 0x060004A4 RID: 1188 RVA: 0x0000CDF9 File Offset: 0x0000AFF9
		public static bool All<TSource>(this IEnumerable<TSource> source, Func<TSource, bool> predicate)
		{
			return source.All(predicate);
		}

		// Token: 0x060004A5 RID: 1189 RVA: 0x0000CE02 File Offset: 0x0000B002
		public static bool Any<TSource>(this IEnumerable<TSource> source, Func<TSource, bool> predicate)
		{
			return source.Any(predicate);
		}

		// Token: 0x060004A6 RID: 1190 RVA: 0x0000CE0B File Offset: 0x0000B00B
		public static bool Contains<TSource>(this IEnumerable<TSource> source, TSource value)
		{
			return source.Contains(value);
		}

		// Token: 0x060004A7 RID: 1191 RVA: 0x0000CE14 File Offset: 0x0000B014
		public static IEnumerable<TResult> Cast<TResult>(this IEnumerable source)
		{
			return source.Cast<TResult>();
		}

		// Token: 0x060004A8 RID: 1192 RVA: 0x0000CE1C File Offset: 0x0000B01C
		public static int Count<TSource>(this IEnumerable<TSource> source)
		{
			return source.Count<TSource>();
		}

		// Token: 0x060004A9 RID: 1193 RVA: 0x0000CE24 File Offset: 0x0000B024
		public static IEnumerable<TSource> Except<TSource>(this IEnumerable<TSource> first, IEnumerable<TSource> second)
		{
			return first.Except(second);
		}

		// Token: 0x060004AA RID: 1194 RVA: 0x0000CE2D File Offset: 0x0000B02D
		public static TSource FirstOrDefault<TSource>(this IEnumerable<TSource> source)
		{
			return source.FirstOrDefault<TSource>();
		}

		// Token: 0x060004AB RID: 1195 RVA: 0x0000CE35 File Offset: 0x0000B035
		public static IEnumerable<TSource> Intersect<TSource>(this IEnumerable<TSource> first, IEnumerable<TSource> second)
		{
			return first.Intersect(second);
		}

		// Token: 0x060004AC RID: 1196 RVA: 0x0000CE3E File Offset: 0x0000B03E
		public static IEnumerable<TSource> OrderBy<TSource, TKey>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector)
		{
			return source.OrderBy(keySelector);
		}

		// Token: 0x060004AD RID: 1197 RVA: 0x0000CE47 File Offset: 0x0000B047
		public static IEnumerable<TSource> OrderByDescending<TSource, TKey>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector)
		{
			return source.OrderByDescending(keySelector);
		}

		// Token: 0x060004AE RID: 1198 RVA: 0x0000CE50 File Offset: 0x0000B050
		public static IEnumerable<TResult> Select<TSource, TResult>(this IEnumerable<TSource> source, Func<TSource, TResult> selector)
		{
			return source.Select(selector);
		}

		// Token: 0x060004AF RID: 1199 RVA: 0x0000CE59 File Offset: 0x0000B059
		public static IEnumerable<TSource> Take<TSource>(this IEnumerable<TSource> source, int count)
		{
			return source.Take(count);
		}

		// Token: 0x060004B0 RID: 1200 RVA: 0x0000CE62 File Offset: 0x0000B062
		public static TSource[] ToArray<TSource>(this IEnumerable<TSource> source)
		{
			return source.ToArray<TSource>();
		}

		// Token: 0x060004B1 RID: 1201 RVA: 0x0000CE6A File Offset: 0x0000B06A
		public static List<TSource> ToList<TSource>(this IEnumerable<TSource> source)
		{
			return source.ToList<TSource>();
		}

		// Token: 0x060004B2 RID: 1202 RVA: 0x0000CE72 File Offset: 0x0000B072
		public static IEnumerable<TSource> Union<TSource>(this IEnumerable<TSource> first, IEnumerable<TSource> second)
		{
			return first.Union(second);
		}

		// Token: 0x060004B3 RID: 1203 RVA: 0x0000CE7B File Offset: 0x0000B07B
		public static IEnumerable<TSource> Distinct<TSource>(this IEnumerable<TSource> source)
		{
			return source.Distinct<TSource>();
		}

		// Token: 0x060004B4 RID: 1204 RVA: 0x0000CE83 File Offset: 0x0000B083
		public static IEnumerable<TSource> Where<TSource>(this IEnumerable<TSource> source, Func<TSource, bool> predicate)
		{
			return source.Where(predicate);
		}
	}
}
