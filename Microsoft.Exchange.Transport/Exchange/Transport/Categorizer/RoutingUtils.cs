using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Transport;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Transport.Categorizer
{
	// Token: 0x02000268 RID: 616
	internal static class RoutingUtils
	{
		// Token: 0x06001AED RID: 6893 RVA: 0x0006E5E6 File Offset: 0x0006C7E6
		public static int CompareNames(string nameA, string nameB)
		{
			return string.Compare(nameA, nameB, StringComparison.OrdinalIgnoreCase);
		}

		// Token: 0x06001AEE RID: 6894 RVA: 0x0006E5F0 File Offset: 0x0006C7F0
		public static bool NullMatch(object reference1, object reference2)
		{
			return reference1 == null == (reference2 == null);
		}

		// Token: 0x06001AEF RID: 6895 RVA: 0x0006E5FC File Offset: 0x0006C7FC
		public static bool MatchStrings(string s1, string s2)
		{
			return string.Equals(s1, s2, StringComparison.OrdinalIgnoreCase);
		}

		// Token: 0x06001AF0 RID: 6896 RVA: 0x0006E606 File Offset: 0x0006C806
		public static bool MatchNextHopServers(INextHopServer server1, INextHopServer server2)
		{
			if (server1.IsIPAddress != server2.IsIPAddress)
			{
				return false;
			}
			if (!server1.IsIPAddress)
			{
				return RoutingUtils.MatchStrings(server1.Fqdn, server2.Fqdn);
			}
			return server1.Address.Equals(server2.Address);
		}

		// Token: 0x06001AF1 RID: 6897 RVA: 0x0006E643 File Offset: 0x0006C843
		public static bool MatchLists<T>(List<T> l1, List<T> l2, Func<T, T, bool> matchValues)
		{
			return RoutingUtils.MatchLists<T>(l1, l2, matchValues, false);
		}

		// Token: 0x06001AF2 RID: 6898 RVA: 0x0006E688 File Offset: 0x0006C888
		public static bool MatchLists<T>(List<T> l1, List<T> l2, Func<T, T, bool> matchValues, bool ordered)
		{
			RoutingUtils.<>c__DisplayClass1<T> CS$<>8__locals1 = new RoutingUtils.<>c__DisplayClass1<T>();
			CS$<>8__locals1.l1 = l1;
			CS$<>8__locals1.matchValues = matchValues;
			if (!RoutingUtils.NullMatch(CS$<>8__locals1.l1, l2) || (CS$<>8__locals1.l1 != null && CS$<>8__locals1.l1.Count != l2.Count))
			{
				return false;
			}
			if (CS$<>8__locals1.l1 == null)
			{
				return true;
			}
			int i;
			for (i = 0; i < CS$<>8__locals1.l1.Count; i++)
			{
				if (!CS$<>8__locals1.matchValues(CS$<>8__locals1.l1[i], l2[i]))
				{
					if (!ordered)
					{
						if (l2.Exists((T element2) => CS$<>8__locals1.matchValues(CS$<>8__locals1.l1[i], element2)))
						{
							goto IL_A9;
						}
					}
					return false;
				}
				IL_A9:;
			}
			return true;
		}

		// Token: 0x06001AF3 RID: 6899 RVA: 0x0006E760 File Offset: 0x0006C960
		public static bool MatchOrderedLists<T>(List<T> l1, List<T> l2, Func<T, T, bool> matchValues)
		{
			return RoutingUtils.MatchLists<T>(l1, l2, matchValues, true);
		}

		// Token: 0x06001AF4 RID: 6900 RVA: 0x0006E76B File Offset: 0x0006C96B
		public static bool MatchLists<T>(ListLoadBalancer<T> l1, ListLoadBalancer<T> l2, Func<T, T, bool> matchValues)
		{
			return RoutingUtils.NullMatch(l1, l2) && (l1 == null || RoutingUtils.MatchLists<T>(l1.NonLoadBalancedList, l2.NonLoadBalancedList, matchValues));
		}

		// Token: 0x06001AF5 RID: 6901 RVA: 0x0006E790 File Offset: 0x0006C990
		public static bool MatchDictionaries<TKey, TValue>(IDictionary<TKey, TValue> d1, IDictionary<TKey, TValue> d2, Func<TValue, TValue, bool> matchValues)
		{
			if (!RoutingUtils.NullMatch(d1, d2) || (d1 != null && d1.Count != d2.Count))
			{
				return false;
			}
			if (d1 == null)
			{
				return true;
			}
			foreach (KeyValuePair<TKey, TValue> keyValuePair in d1)
			{
				TValue arg;
				if (!d2.TryGetValue(keyValuePair.Key, out arg) || !matchValues(keyValuePair.Value, arg))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06001AF6 RID: 6902 RVA: 0x0006E834 File Offset: 0x0006CA34
		public static bool MatchRouteDictionaries<TKey>(IDictionary<TKey, RouteInfo> d1, IDictionary<TKey, RouteInfo> d2, NextHopMatch nextHopMatch)
		{
			return RoutingUtils.MatchDictionaries<TKey, RouteInfo>(d1, d2, (RouteInfo routeInfo1, RouteInfo routeInfo2) => routeInfo1.Match(routeInfo2, nextHopMatch));
		}

		// Token: 0x06001AF7 RID: 6903 RVA: 0x0006E861 File Offset: 0x0006CA61
		public static bool MatchRouteDictionaries<TKey>(IDictionary<TKey, RouteInfo> d1, IDictionary<TKey, RouteInfo> d2)
		{
			return RoutingUtils.MatchRouteDictionaries<TKey>(d1, d2, NextHopMatch.Full);
		}

		// Token: 0x06001AF8 RID: 6904 RVA: 0x0006E86B File Offset: 0x0006CA6B
		public static bool IsSmtpAddressType(string addressType)
		{
			return "smtp".Equals(addressType, StringComparison.OrdinalIgnoreCase);
		}

		// Token: 0x06001AF9 RID: 6905 RVA: 0x0006E87C File Offset: 0x0006CA7C
		public static bool TryConvertToRoutingAddress(SmtpAddress? smtpAddress, out RoutingAddress routingAddress)
		{
			routingAddress = RoutingAddress.Empty;
			if (smtpAddress != null && smtpAddress.Value.IsValidAddress && smtpAddress.Value != SmtpAddress.NullReversePath)
			{
				routingAddress = new RoutingAddress(smtpAddress.ToString());
				return true;
			}
			return false;
		}

		// Token: 0x06001AFA RID: 6906 RVA: 0x0006E8DC File Offset: 0x0006CADC
		public static bool IsNullOrEmpty<T>(IList<T> list)
		{
			return list == null || list.Count == 0;
		}

		// Token: 0x06001AFB RID: 6907 RVA: 0x0006E8EC File Offset: 0x0006CAEC
		public static void AddItemToLazyList<ItemT>(ItemT item, ref List<ItemT> list)
		{
			if (list == null)
			{
				list = new List<ItemT>();
			}
			list.Add(item);
		}

		// Token: 0x06001AFC RID: 6908 RVA: 0x0006E901 File Offset: 0x0006CB01
		public static void AddItemToLazyList<ItemT>(ItemT item, bool randomLoadBalancingOffsetEnabled, ref ListLoadBalancer<ItemT> list)
		{
			if (list == null)
			{
				list = new ListLoadBalancer<ItemT>(randomLoadBalancingOffsetEnabled);
			}
			list.AddItem(item);
		}

		// Token: 0x06001AFD RID: 6909 RVA: 0x0006E918 File Offset: 0x0006CB18
		public static int GetRandomNumber(int maxValue)
		{
			int result;
			lock (RoutingUtils.random)
			{
				result = RoutingUtils.random.Next(maxValue);
			}
			return result;
		}

		// Token: 0x06001AFE RID: 6910 RVA: 0x0006E960 File Offset: 0x0006CB60
		public static void ShuffleList<T>(IList<T> list)
		{
			int i = list.Count;
			int randomNumber = RoutingUtils.GetRandomNumber(int.MaxValue);
			Random random = new Random(randomNumber);
			while (i > 1)
			{
				int index = random.Next(i);
				i--;
				T value = list[i];
				list[i] = list[index];
				list[index] = value;
			}
		}

		// Token: 0x06001AFF RID: 6911 RVA: 0x0006EB68 File Offset: 0x0006CD68
		public static IEnumerable<T> RandomShuffleEnumerate<T>(IList<T> list)
		{
			RoutingUtils.ThrowIfNullOrEmpty<T>(list, "list");
			foreach (int index in RoutingUtils.ShuffleIndices(list.Count))
			{
				yield return list[index];
			}
			yield break;
		}

		// Token: 0x06001B00 RID: 6912 RVA: 0x0006EB85 File Offset: 0x0006CD85
		public static IEnumerable<T> RandomShiftEnumerate<T>(IList<T> list)
		{
			return RoutingUtils.RandomShiftEnumerate<T>(list, 0);
		}

		// Token: 0x06001B01 RID: 6913 RVA: 0x0006EB90 File Offset: 0x0006CD90
		public static IEnumerable<T> RandomShiftEnumerate<T>(IList<T> list, int offset)
		{
			int randomNumber = RoutingUtils.GetRandomNumber(list.Count - offset);
			return RoutingUtils.ShiftedEnumerate<T>(list, offset, randomNumber);
		}

		// Token: 0x06001B02 RID: 6914 RVA: 0x0006EBB3 File Offset: 0x0006CDB3
		public static IEnumerable<T> ShiftedEnumerate<T>(IList<T> list, int shift)
		{
			return RoutingUtils.ShiftedEnumerate<T>(list, 0, shift);
		}

		// Token: 0x06001B03 RID: 6915 RVA: 0x0006ED60 File Offset: 0x0006CF60
		public static IEnumerable<T> ShiftedEnumerate<T>(IList<T> list, int offset, int shift)
		{
			if (offset > list.Count || offset < 0)
			{
				throw new ArgumentOutOfRangeException("offset", offset, "Offset cannot be negative or greater than the number of elements in the list");
			}
			if (shift < 0)
			{
				throw new ArgumentOutOfRangeException("shift", shift, "Shift cannot be negative");
			}
			int enumCount = list.Count - offset;
			for (int i = 0; i < enumCount; i++)
			{
				yield return list[(i + shift) % enumCount + offset];
			}
			yield break;
		}

		// Token: 0x06001B04 RID: 6916 RVA: 0x0006ED8B File Offset: 0x0006CF8B
		public static void ThrowIfNull(object value, string parameterName)
		{
			if (value == null)
			{
				throw new ArgumentNullException(parameterName);
			}
		}

		// Token: 0x06001B05 RID: 6917 RVA: 0x0006ED97 File Offset: 0x0006CF97
		public static void ThrowIfNullOrEmpty(string value, string parameterName)
		{
			if (string.IsNullOrEmpty(value))
			{
				throw new ArgumentNullException(parameterName);
			}
		}

		// Token: 0x06001B06 RID: 6918 RVA: 0x0006EDA8 File Offset: 0x0006CFA8
		public static void ThrowIfNullOrEmpty<T>(ICollection<T> collection, string parameterName)
		{
			if (collection == null || collection.Count == 0)
			{
				throw new ArgumentNullException(parameterName);
			}
		}

		// Token: 0x06001B07 RID: 6919 RVA: 0x0006EDBC File Offset: 0x0006CFBC
		public static void ThrowIfNullOrEmpty<T>(ListLoadBalancer<T> collection, string parameterName)
		{
			if (collection == null || collection.Count == 0)
			{
				throw new ArgumentNullException(parameterName);
			}
		}

		// Token: 0x06001B08 RID: 6920 RVA: 0x0006EDD0 File Offset: 0x0006CFD0
		public static void ThrowIfEmpty(ADObjectId id, string parameterName)
		{
			if (string.IsNullOrEmpty(id.DistinguishedName))
			{
				throw new ArgumentException("Argument " + parameterName + " must have a non-empty DN", parameterName);
			}
			if (Guid.Empty.Equals(id.ObjectGuid))
			{
				throw new ArgumentException("Argument " + parameterName + " must have a non-empty ObjectGuid", parameterName);
			}
		}

		// Token: 0x06001B09 RID: 6921 RVA: 0x0006EE30 File Offset: 0x0006D030
		public static void ThrowIfNullOrEmptyObjectGuid(ADObjectId id, string parameterName)
		{
			RoutingUtils.ThrowIfNull(id, parameterName);
			if (Guid.Empty.Equals(id.ObjectGuid))
			{
				throw new ArgumentException("Argument " + parameterName + " must have a non-empty ObjectGuid", parameterName);
			}
		}

		// Token: 0x06001B0A RID: 6922 RVA: 0x0006EE70 File Offset: 0x0006D070
		public static void ThrowIfNullOrEmpty(ADObjectId id, string parameterName)
		{
			RoutingUtils.ThrowIfNull(id, parameterName);
			RoutingUtils.ThrowIfEmpty(id, parameterName);
		}

		// Token: 0x06001B0B RID: 6923 RVA: 0x0006EE80 File Offset: 0x0006D080
		public static void ThrowIfMissingDependency(object dependency, string dependencyName)
		{
			if (dependency == null)
			{
				throw new InvalidOperationException("Attempted to use missing external component: " + dependencyName);
			}
		}

		// Token: 0x06001B0C RID: 6924 RVA: 0x0006EE98 File Offset: 0x0006D098
		public static void LogErrorWhenAddToDictionaryFails<TKey, TValue>(TKey keyAdded, TValue valueAdded, string message)
		{
			ArgumentValidator.ThrowIfNull("keyAdded", keyAdded);
			RoutingDiag.EventLogger.LogEvent(TransportEventLogConstants.Tuple_RoutingDictionaryInsertFailure, keyAdded.ToString(), new object[]
			{
				keyAdded,
				message
			});
		}

		// Token: 0x06001B0D RID: 6925 RVA: 0x0006F070 File Offset: 0x0006D270
		private static IEnumerable<int> ShuffleIndices(int n)
		{
			if (n <= 0)
			{
				throw new ArgumentOutOfRangeException("n", n, "Count of indices should only be positive");
			}
			List<int> indices = Enumerable.Range(0, n).ToList<int>();
			int seed = RoutingUtils.GetRandomNumber(int.MaxValue);
			Random localRandom = new Random(seed);
			while (n > 0)
			{
				int i = localRandom.Next(n);
				yield return indices[i];
				n--;
				indices[i] = indices[n];
			}
			yield break;
		}

		// Token: 0x04000CC7 RID: 3271
		private static readonly Random random = new Random((int)DateTime.UtcNow.Ticks);
	}
}
