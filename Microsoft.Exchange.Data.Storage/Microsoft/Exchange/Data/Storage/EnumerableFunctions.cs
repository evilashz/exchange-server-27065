﻿using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x0200044D RID: 1101
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal static class EnumerableFunctions
	{
		// Token: 0x0600310D RID: 12557 RVA: 0x000C9070 File Offset: 0x000C7270
		public static ICollection<T> Union<T>(this ICollection<T> first, ICollection<T> second)
		{
			Util.ThrowOnNullArgument(first, "first");
			ICollection<T> result;
			if (EnumerableFunctions.TryPickWinner<T>(first, second, out result))
			{
				return result;
			}
			if (first == second)
			{
				return first;
			}
			HashSet<T> hashSet = new HashSet<T>(first);
			hashSet.UnionWith(second);
			return hashSet;
		}

		// Token: 0x0600310E RID: 12558 RVA: 0x000C90AC File Offset: 0x000C72AC
		internal static ICollection<T> Concat<T>(this ICollection<T> first, ICollection<T> second)
		{
			ICollection<T> result;
			if (EnumerableFunctions.TryPickWinner<T>(first, second, out result))
			{
				return result;
			}
			return new EnumerableFunctions.ConcatCollection<T>(first, second);
		}

		// Token: 0x0600310F RID: 12559 RVA: 0x000C90CD File Offset: 0x000C72CD
		public static IEnumerable<TResult> Cast<TSource, TResult>(this IEnumerable<TSource> source) where TSource : PropertyDefinition where TResult : PropertyDefinition
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			return EnumerableFunctions.CastIterator<TResult, TSource>(source);
		}

		// Token: 0x06003110 RID: 12560 RVA: 0x000C90E3 File Offset: 0x000C72E3
		public static ICollection<TResult> Select<TSource, TResult>(this ICollection<TSource> first, Func<TSource, TResult> selector)
		{
			return new EnumerableFunctions.SelectCollection<TSource, TResult>(first, selector);
		}

		// Token: 0x06003111 RID: 12561 RVA: 0x000C90EC File Offset: 0x000C72EC
		private static bool TryPickWinner<T>(ICollection<T> first, ICollection<T> second, out ICollection<T> winner)
		{
			if (first == null)
			{
				throw new ArgumentNullException("first");
			}
			if (second == null || second.Count == 0)
			{
				winner = first;
				return true;
			}
			if (first.Count == 0)
			{
				winner = second;
				return true;
			}
			winner = null;
			return false;
		}

		// Token: 0x06003112 RID: 12562 RVA: 0x000C92FC File Offset: 0x000C74FC
		private static IEnumerable<TResult> CastIterator<TResult, TSource>(IEnumerable<TSource> source) where TResult : PropertyDefinition where TSource : PropertyDefinition
		{
			foreach (TSource element in source)
			{
				TResult temp = element as TResult;
				if (temp == null)
				{
					throw new InvalidCastException(string.Format("Can't convert {0} to {1}", element, typeof(TSource)));
				}
				yield return temp;
			}
			yield break;
		}

		// Token: 0x0200044E RID: 1102
		private sealed class ConcatCollection<T> : ReadOnlyDelegatingCollection<T>
		{
			// Token: 0x06003113 RID: 12563 RVA: 0x000C9319 File Offset: 0x000C7519
			public ConcatCollection(ICollection<T> first, ICollection<T> second)
			{
				Util.ThrowOnNullArgument(first, "first");
				Util.ThrowOnNullArgument(second, "second");
				this.first = first;
				this.second = second;
			}

			// Token: 0x06003114 RID: 12564 RVA: 0x000C9345 File Offset: 0x000C7545
			public override bool Contains(T item)
			{
				return this.first.Contains(item) || this.second.Contains(item);
			}

			// Token: 0x06003115 RID: 12565 RVA: 0x000C9363 File Offset: 0x000C7563
			public override void CopyTo(T[] array, int arrayIndex)
			{
				this.first.CopyTo(array, arrayIndex);
				this.second.CopyTo(array, arrayIndex + this.first.Count);
			}

			// Token: 0x17000F61 RID: 3937
			// (get) Token: 0x06003116 RID: 12566 RVA: 0x000C938B File Offset: 0x000C758B
			public override int Count
			{
				get
				{
					return this.first.Count + this.second.Count;
				}
			}

			// Token: 0x06003117 RID: 12567 RVA: 0x000C93A4 File Offset: 0x000C75A4
			public override IEnumerator<T> GetEnumerator()
			{
				return this.first.Concat(this.second).GetEnumerator();
			}

			// Token: 0x04001A9C RID: 6812
			private readonly ICollection<T> first;

			// Token: 0x04001A9D RID: 6813
			private readonly ICollection<T> second;
		}

		// Token: 0x0200044F RID: 1103
		private sealed class SelectCollection<TSource, TResult> : ReadOnlyDelegatingCollection<TResult>
		{
			// Token: 0x06003118 RID: 12568 RVA: 0x000C93BC File Offset: 0x000C75BC
			public SelectCollection(ICollection<TSource> source, Func<TSource, TResult> selector)
			{
				Util.ThrowOnNullArgument(source, "source");
				Util.ThrowOnNullArgument(selector, "selector");
				this.source = source;
				this.selector = selector;
			}

			// Token: 0x17000F62 RID: 3938
			// (get) Token: 0x06003119 RID: 12569 RVA: 0x000C93E8 File Offset: 0x000C75E8
			public override int Count
			{
				get
				{
					return this.source.Count;
				}
			}

			// Token: 0x0600311A RID: 12570 RVA: 0x000C93F5 File Offset: 0x000C75F5
			public override IEnumerator<TResult> GetEnumerator()
			{
				return this.source.Select(this.selector).GetEnumerator();
			}

			// Token: 0x04001A9E RID: 6814
			private readonly ICollection<TSource> source;

			// Token: 0x04001A9F RID: 6815
			private readonly Func<TSource, TResult> selector;
		}
	}
}
