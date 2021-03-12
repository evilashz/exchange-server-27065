using System;
using System.Collections;
using System.Globalization;
using System.Reflection;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Storage.Infoworker.MailboxSearch;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Data.Storage;

namespace Microsoft.Exchange.Data.Directory.Recipient
{
	// Token: 0x02000153 RID: 339
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal static class SimpleStoreValueConverter
	{
		// Token: 0x06000E7D RID: 3709 RVA: 0x00045858 File Offset: 0x00043A58
		internal static object ConvertValueFromStore(ProviderPropertyDefinition property, object originalValue)
		{
			if (originalValue == null)
			{
				return null;
			}
			Type type = property.Type;
			if (type.Equals(originalValue.GetType()))
			{
				return originalValue;
			}
			if (type.GetTypeInfo().IsEnum)
			{
				return Enum.ToObject(type, originalValue);
			}
			if (type == typeof(TimeSpan))
			{
				return TimeSpan.FromMinutes((double)((int)originalValue));
			}
			if (type == typeof(Uri))
			{
				return new Uri(originalValue.ToString(), UriKind.Absolute);
			}
			if (type == typeof(string) && property.IsMultivalued)
			{
				return SimpleStoreValueConverter.ConvertStringArrayToMvpString(originalValue);
			}
			if (type == typeof(ADObjectId))
			{
				if (property.IsMultivalued)
				{
					if (originalValue is byte[][])
					{
						return SimpleStoreValueConverter.ConvertByteMatrixToMvpADObjectId(originalValue);
					}
					return SimpleStoreValueConverter.ConvertStringArrayToMvpADObjectId(originalValue);
				}
				else if (originalValue is byte[])
				{
					return new ADObjectId((byte[])originalValue);
				}
			}
			if (type == typeof(KeywordHit) && property.IsMultivalued)
			{
				return SimpleStoreValueConverter.ConvertStringArrayToMvpKeywordHit(originalValue);
			}
			if (type == typeof(int?) && property.IsMultivalued)
			{
				return SimpleStoreValueConverter.ConvertIntArrayToMvpNullableInt(originalValue);
			}
			if (type == typeof(DiscoverySearchStats) && property.IsMultivalued)
			{
				return SimpleStoreValueConverter.ConvertStringArrayToMvpDiscoverySearchStats(originalValue);
			}
			if (!(type == typeof(CultureInfo)))
			{
				if (type == typeof(Unlimited<EnhancedTimeSpan>))
				{
					if (originalValue == null)
					{
						return Unlimited<EnhancedTimeSpan>.UnlimitedValue;
					}
					if (originalValue is long)
					{
						return new Unlimited<EnhancedTimeSpan>(EnhancedTimeSpan.FromTicks((long)originalValue));
					}
				}
				return ValueConvertor.ConvertValue(originalValue, type, null);
			}
			if (originalValue != null)
			{
				return new CultureInfo((string)originalValue);
			}
			return null;
		}

		// Token: 0x06000E7E RID: 3710 RVA: 0x00045A0C File Offset: 0x00043C0C
		internal static object ConvertValueToStore(object originalValue)
		{
			if (originalValue == null)
			{
				return null;
			}
			if (originalValue is Enum)
			{
				return (int)originalValue;
			}
			if (originalValue is TimeSpan)
			{
				return (int)((TimeSpan)originalValue).TotalMinutes;
			}
			if (originalValue is Uri)
			{
				return ((Uri)originalValue).ToString();
			}
			if (originalValue is ADObjectId)
			{
				return ((ADObjectId)originalValue).GetBytes();
			}
			if (originalValue is MultiValuedProperty<string>)
			{
				return SimpleStoreValueConverter.ConvertMvpStringToStringArray((MultiValuedProperty<string>)originalValue);
			}
			if (originalValue is MultiValuedProperty<int>)
			{
				return SimpleStoreValueConverter.ConvertMvpIntToIntArray((MultiValuedProperty<int>)originalValue);
			}
			if (originalValue is MultiValuedProperty<ADObjectId>)
			{
				return SimpleStoreValueConverter.ConvertMvpADObjectIdToStringArray((MultiValuedProperty<ADObjectId>)originalValue);
			}
			if (originalValue is MultiValuedProperty<KeywordHit>)
			{
				return SimpleStoreValueConverter.ConvertMvpToStringArray<KeywordHit>((MultiValuedProperty<KeywordHit>)originalValue);
			}
			if (originalValue is MultiValuedProperty<DiscoverySearchStats>)
			{
				return SimpleStoreValueConverter.ConvertMvpToStringArray<DiscoverySearchStats>((MultiValuedProperty<DiscoverySearchStats>)originalValue);
			}
			if (originalValue is CultureInfo)
			{
				return originalValue.ToString();
			}
			if (originalValue is ExchangeObjectVersion)
			{
				return ((ExchangeObjectVersion)originalValue).ToInt64();
			}
			if (!(originalValue is Unlimited<EnhancedTimeSpan>))
			{
				return originalValue;
			}
			Unlimited<EnhancedTimeSpan> value = (Unlimited<EnhancedTimeSpan>)originalValue;
			if (value == Unlimited<EnhancedTimeSpan>.UnlimitedValue)
			{
				return null;
			}
			return value.Value.Ticks;
		}

		// Token: 0x06000E7F RID: 3711 RVA: 0x00045B38 File Offset: 0x00043D38
		internal static MultiValuedPropertyBase CreateGenericMultiValuedProperty(ProviderPropertyDefinition propertyDefinition, bool createAsReadOnly, ICollection values, ICollection invalidValues, LocalizedString? readOnlyErrorMessage)
		{
			MultiValuedPropertyBase result;
			if (!SimpleStoreValueConverter.TryCreateGenericMultiValuedProperty(propertyDefinition, createAsReadOnly, values, invalidValues, readOnlyErrorMessage, out result))
			{
				throw new NotImplementedException(DataStrings.ErrorMvpNotImplemented(propertyDefinition.Type.ToString(), propertyDefinition.Name));
			}
			return result;
		}

		// Token: 0x06000E80 RID: 3712 RVA: 0x00045B76 File Offset: 0x00043D76
		internal static bool TryCreateGenericMultiValuedProperty(ProviderPropertyDefinition propertyDefinition, bool createAsReadOnly, ICollection values, ICollection invalidValues, LocalizedString? readOnlyErrorMessage, out MultiValuedPropertyBase mvp)
		{
			mvp = null;
			if (propertyDefinition.Type == typeof(ADObjectId))
			{
				mvp = new MultiValuedProperty<ADObjectId>(createAsReadOnly, propertyDefinition, values, invalidValues, readOnlyErrorMessage);
			}
			return mvp != null || ValueConvertor.TryCreateGenericMultiValuedProperty(propertyDefinition, createAsReadOnly, values, invalidValues, readOnlyErrorMessage, out mvp);
		}

		// Token: 0x06000E81 RID: 3713 RVA: 0x00045BB5 File Offset: 0x00043DB5
		private static string[] ConvertMvpStringToStringArray(MultiValuedProperty<string> mvps)
		{
			if (mvps == null)
			{
				return null;
			}
			return mvps.ToArray();
		}

		// Token: 0x06000E82 RID: 3714 RVA: 0x00045BC2 File Offset: 0x00043DC2
		private static int[] ConvertMvpIntToIntArray(MultiValuedProperty<int> mvps)
		{
			if (mvps == null)
			{
				return null;
			}
			return mvps.ToArray();
		}

		// Token: 0x06000E83 RID: 3715 RVA: 0x00045BF1 File Offset: 0x00043DF1
		private static string[] ConvertMvpADObjectIdToStringArray(MultiValuedProperty<ADObjectId> mvpIds)
		{
			if (mvpIds != null)
			{
				return Array.ConvertAll<ADObjectId, string>(mvpIds.ToArray(), (ADObjectId mvpId) => mvpId.ObjectGuid.ToString());
			}
			return new string[0];
		}

		// Token: 0x06000E84 RID: 3716 RVA: 0x00045C34 File Offset: 0x00043E34
		private static string[] ConvertMvpToStringArray<T>(MultiValuedProperty<T> mvp)
		{
			if (mvp != null)
			{
				return Array.ConvertAll<T, string>(mvp.ToArray(), (T obj) => obj.ToString());
			}
			return new string[0];
		}

		// Token: 0x06000E85 RID: 3717 RVA: 0x00045C58 File Offset: 0x00043E58
		private static MultiValuedProperty<int?> ConvertIntArrayToMvpNullableInt(object obj)
		{
			if (obj == null)
			{
				return null;
			}
			ADMultiValuedProperty<int?> admultiValuedProperty = new ADMultiValuedProperty<int?>();
			if (obj is int)
			{
				admultiValuedProperty.Add(new int?((int)obj));
			}
			else if (obj is int[])
			{
				int[] array = (int[])obj;
				foreach (int value in array)
				{
					admultiValuedProperty.Add(new int?(value));
				}
			}
			return admultiValuedProperty;
		}

		// Token: 0x06000E86 RID: 3718 RVA: 0x00045CC0 File Offset: 0x00043EC0
		private static MultiValuedProperty<string> ConvertStringArrayToMvpString(object obj)
		{
			if (obj == null)
			{
				return null;
			}
			MultiValuedProperty<string> multiValuedProperty = new MultiValuedProperty<string>();
			if (obj is string)
			{
				multiValuedProperty.Add((string)obj);
			}
			else if (obj is string[])
			{
				string[] array = (string[])obj;
				foreach (string text in array)
				{
					if (text != null)
					{
						multiValuedProperty.Add(text);
					}
				}
			}
			return multiValuedProperty;
		}

		// Token: 0x06000E87 RID: 3719 RVA: 0x00045D30 File Offset: 0x00043F30
		private static MultiValuedProperty<ADObjectId> ConvertStringArrayToMvpADObjectId(object guids)
		{
			MultiValuedProperty<ADObjectId> result = new MultiValuedProperty<ADObjectId>();
			if (guids is string)
			{
				guids = new string[]
				{
					(string)guids
				};
			}
			if (guids is string[])
			{
				try
				{
					result = new MultiValuedProperty<ADObjectId>(Array.ConvertAll<string, ADObjectId>((string[])guids, (string guidString) => new ADObjectId(new Guid(guidString))));
				}
				catch (FormatException)
				{
					ExTraceGlobals.StorageTracer.TraceDebug(0L, "The guid strings of user configuration is corrupted");
				}
				catch (OverflowException)
				{
					ExTraceGlobals.StorageTracer.TraceDebug(0L, "The guid strings of user configuration is corrupted");
				}
			}
			return result;
		}

		// Token: 0x06000E88 RID: 3720 RVA: 0x00045DDC File Offset: 0x00043FDC
		private static MultiValuedProperty<ADObjectId> ConvertByteMatrixToMvpADObjectId(object mailboxIds)
		{
			byte[][] array = (byte[][])mailboxIds;
			MultiValuedProperty<ADObjectId> multiValuedProperty = new MultiValuedProperty<ADObjectId>();
			foreach (byte[] bytes in array)
			{
				multiValuedProperty.Add(new ADObjectId(bytes));
			}
			return multiValuedProperty;
		}

		// Token: 0x06000E89 RID: 3721 RVA: 0x00045E24 File Offset: 0x00044024
		private static MultiValuedProperty<KeywordHit> ConvertStringArrayToMvpKeywordHit(object keywordHits)
		{
			MultiValuedProperty<KeywordHit> result = new MultiValuedProperty<KeywordHit>();
			if (keywordHits is string)
			{
				keywordHits = new string[]
				{
					(string)keywordHits
				};
			}
			if (keywordHits is string[])
			{
				try
				{
					result = new MultiValuedProperty<KeywordHit>(Array.ConvertAll<string, KeywordHit>((string[])keywordHits, (string keywordHit) => KeywordHit.Parse(keywordHit)));
				}
				catch (ArgumentNullException)
				{
					ExTraceGlobals.StorageTracer.TraceDebug(0L, "KeywordHit.Parse() throws ArgumentNullException");
				}
				catch (FormatException)
				{
					ExTraceGlobals.StorageTracer.TraceDebug(0L, "KeywordHit.Parse() throws FormatException");
				}
				catch (OverflowException)
				{
					ExTraceGlobals.StorageTracer.TraceDebug(0L, "KeywordHit.Parse() throws OverflowException");
				}
			}
			return result;
		}

		// Token: 0x06000E8A RID: 3722 RVA: 0x00045EF8 File Offset: 0x000440F8
		private static MultiValuedProperty<DiscoverySearchStats> ConvertStringArrayToMvpDiscoverySearchStats(object searchStats)
		{
			MultiValuedProperty<DiscoverySearchStats> result = new MultiValuedProperty<DiscoverySearchStats>();
			if (searchStats is string)
			{
				searchStats = new string[]
				{
					(string)searchStats
				};
			}
			if (searchStats is string[])
			{
				try
				{
					result = new MultiValuedProperty<DiscoverySearchStats>(Array.ConvertAll<string, DiscoverySearchStats>((string[])searchStats, (string searchStat) => DiscoverySearchStats.Parse(searchStat)));
				}
				catch (ArgumentNullException)
				{
					ExTraceGlobals.StorageTracer.TraceDebug(0L, "DiscoverySearchStats.Parse() throws ArgumentNullException");
				}
				catch (FormatException)
				{
					ExTraceGlobals.StorageTracer.TraceDebug(0L, "DiscoverySearchStats.Parse() throws FormatException");
				}
				catch (OverflowException)
				{
					ExTraceGlobals.StorageTracer.TraceDebug(0L, "DiscoverySearchStats.Parse() throws OverflowException");
				}
			}
			return result;
		}
	}
}
