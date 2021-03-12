using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace Microsoft.Office.CompliancePolicy.PolicyEvaluation
{
	// Token: 0x020000C5 RID: 197
	internal static class PolicyUtils
	{
		// Token: 0x060004EB RID: 1259 RVA: 0x0000ED88 File Offset: 0x0000CF88
		internal static bool CompareStringValues(object leftValue, object rightValue, IStringComparer comparer)
		{
			if (leftValue == null && rightValue == null)
			{
				return true;
			}
			if (leftValue == null || rightValue == null)
			{
				return false;
			}
			string text = leftValue as string;
			if (text != null)
			{
				string text2 = rightValue as string;
				if (text2 != null)
				{
					return comparer.Equals(text, text2);
				}
				IEnumerable<string> enumerable = rightValue as IEnumerable<string>;
				if (enumerable != null)
				{
					return PolicyUtils.MatchAny(enumerable, text, comparer);
				}
			}
			IEnumerable<string> enumerable2 = leftValue as IEnumerable<string>;
			if (enumerable2 != null)
			{
				string text3 = rightValue as string;
				if (text3 != null)
				{
					return PolicyUtils.MatchAny(enumerable2, text3, comparer);
				}
				IEnumerable<string> enumerable3 = rightValue as IEnumerable<string>;
				if (enumerable3 != null)
				{
					return PolicyUtils.MatchAny(enumerable2, enumerable3, comparer);
				}
			}
			throw new CompliancePolicyException("Only string values are supported!");
		}

		// Token: 0x060004EC RID: 1260 RVA: 0x0000EE18 File Offset: 0x0000D018
		internal static bool CompareGuidValues(object leftValue, object rightValue)
		{
			if (leftValue == null && rightValue == null)
			{
				return true;
			}
			if (leftValue == null || rightValue == null)
			{
				return false;
			}
			Guid? guid = leftValue as Guid?;
			if (guid != null)
			{
				Guid? guid2 = rightValue as Guid?;
				if (guid2 != null)
				{
					return guid.Equals(guid2);
				}
				IEnumerable<Guid> enumerable = rightValue as IEnumerable<Guid>;
				if (enumerable != null)
				{
					return PolicyUtils.MatchAny<Guid>(enumerable, guid.Value);
				}
			}
			IEnumerable<Guid> enumerable2 = leftValue as IEnumerable<Guid>;
			if (enumerable2 != null)
			{
				Guid? guid3 = rightValue as Guid?;
				if (guid3 != null)
				{
					return PolicyUtils.MatchAny<Guid>(enumerable2, guid3.Value);
				}
				IEnumerable<Guid> enumerable3 = rightValue as IEnumerable<Guid>;
				if (enumerable3 != null)
				{
					return PolicyUtils.MatchAny<Guid>(enumerable2, enumerable3);
				}
			}
			throw new CompliancePolicyException("Only Guid values are supported!");
		}

		// Token: 0x060004ED RID: 1261 RVA: 0x0000EED8 File Offset: 0x0000D0D8
		internal static bool CompareValues(object leftValue, object rightValue)
		{
			if (leftValue == null && rightValue == null)
			{
				return true;
			}
			if (leftValue == null || rightValue == null)
			{
				return false;
			}
			bool flag = PolicyUtils.IsTypeCollection(leftValue.GetType());
			bool flag2 = PolicyUtils.IsTypeCollection(rightValue.GetType());
			if (!flag)
			{
				if (!flag2)
				{
					return leftValue.Equals(rightValue);
				}
				return PolicyUtils.MatchAny((IEnumerable)rightValue, leftValue);
			}
			else
			{
				if (!flag2)
				{
					return PolicyUtils.MatchAny((IEnumerable)leftValue, rightValue);
				}
				return PolicyUtils.CollectionMatchAny((IEnumerable)leftValue, (IEnumerable)rightValue);
			}
		}

		// Token: 0x060004EE RID: 1262 RVA: 0x0000EF6C File Offset: 0x0000D16C
		internal static bool MatchAny<T>(IEnumerable<T> collection, T item)
		{
			return collection != null && collection.Any((T entry) => entry.Equals(item));
		}

		// Token: 0x060004EF RID: 1263 RVA: 0x0000F104 File Offset: 0x0000D304
		internal static bool MatchAny<T>(IEnumerable<T> listX, IEnumerable<T> listY)
		{
			return listX != null && listY != null && (from <>h__TransparentIdentifier3 in (from x in listX
			from y in listY
			select new
			{
				x,
				y
			}).Where(delegate(<>h__TransparentIdentifier3)
			{
				T x = <>h__TransparentIdentifier3.x;
				return x.Equals(<>h__TransparentIdentifier3.y);
			})
			select <>h__TransparentIdentifier3.x).Any<T>();
		}

		// Token: 0x060004F0 RID: 1264 RVA: 0x0000F18C File Offset: 0x0000D38C
		internal static bool MatchAny(IEnumerable<string> stringCollection, string str, IStringComparer comparer)
		{
			return stringCollection != null && stringCollection.Any((string entry) => comparer.Equals(entry, str));
		}

		// Token: 0x060004F1 RID: 1265 RVA: 0x0000F200 File Offset: 0x0000D400
		internal static bool MatchAny(IEnumerable<string> listX, IEnumerable<string> listY, IStringComparer comparer)
		{
			if (listX == null || listY == null)
			{
				return false;
			}
			return (from x in listX
			from y in listY
			where comparer.Equals(x, y)
			select x).Any<string>();
		}

		// Token: 0x060004F2 RID: 1266 RVA: 0x0000F298 File Offset: 0x0000D498
		internal static bool TryParseNullableDateTimeUtc(string input, out DateTime? outputDate)
		{
			if (string.IsNullOrEmpty(input))
			{
				outputDate = null;
				return true;
			}
			DateTime value;
			bool flag = DateTime.TryParse(input, DateTimeFormatInfo.InvariantInfo, DateTimeStyles.AssumeUniversal, out value);
			if (flag)
			{
				outputDate = new DateTime?(value);
				return true;
			}
			outputDate = null;
			return false;
		}

		// Token: 0x060004F3 RID: 1267 RVA: 0x0000F2E0 File Offset: 0x0000D4E0
		internal static string DateTimeToUtcString(DateTime input)
		{
			return input.ToUniversalTime().ToString("u", CultureInfo.InvariantCulture);
		}

		// Token: 0x060004F4 RID: 1268 RVA: 0x0000F306 File Offset: 0x0000D506
		internal static bool TryParseBool(string input, bool defaultValue, out bool output)
		{
			if (string.IsNullOrEmpty(input))
			{
				output = defaultValue;
				return true;
			}
			if (bool.TryParse(input, out output))
			{
				return true;
			}
			output = defaultValue;
			return false;
		}

		// Token: 0x060004F5 RID: 1269 RVA: 0x0000F324 File Offset: 0x0000D524
		internal static bool IsTypeCollection(Type type)
		{
			return type != typeof(string) && type.GetInterface(typeof(IEnumerable).FullName) != null;
		}

		// Token: 0x060004F6 RID: 1270 RVA: 0x0000F358 File Offset: 0x0000D558
		private static bool MatchAny(IEnumerable collection, object item)
		{
			foreach (object obj in collection)
			{
				if (obj != null && obj.Equals(item))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x060004F7 RID: 1271 RVA: 0x0000F3B4 File Offset: 0x0000D5B4
		private static bool CollectionMatchAny(IEnumerable collection1, IEnumerable collection2)
		{
			foreach (object obj in collection1)
			{
				foreach (object obj2 in collection2)
				{
					if (obj == null && obj2 == null)
					{
						return true;
					}
					if (obj != null && obj.Equals(obj2))
					{
						return true;
					}
				}
			}
			return false;
		}
	}
}
