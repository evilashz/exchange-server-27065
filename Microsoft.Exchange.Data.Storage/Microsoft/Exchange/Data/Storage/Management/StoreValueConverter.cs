using System;
using System.Collections;
using System.Globalization;
using System.Reflection;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Storage.Infoworker.MailboxSearch;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Data.Storage;

namespace Microsoft.Exchange.Data.Storage.Management
{
	// Token: 0x02000A4F RID: 2639
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal static class StoreValueConverter
	{
		// Token: 0x06006068 RID: 24680 RVA: 0x001967B8 File Offset: 0x001949B8
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
			if (type == typeof(ExTimeZoneValue))
			{
				return ExTimeZoneValue.Parse(originalValue.ToString());
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
				return StoreValueConverter.ConvertStringArrayToMvpString(originalValue);
			}
			if (type == typeof(ADObjectId))
			{
				if (property.IsMultivalued)
				{
					if (originalValue is byte[][])
					{
						return StoreValueConverter.ConvertByteMatrixToMvpADObjectId(originalValue);
					}
					return StoreValueConverter.ConvertStringArrayToMvpADObjectId(originalValue);
				}
				else if (originalValue is byte[])
				{
					return new ADObjectId((byte[])originalValue);
				}
			}
			if (type == typeof(KeywordHit) && property.IsMultivalued)
			{
				return StoreValueConverter.ConvertStringArrayToMvpKeywordHit(originalValue);
			}
			if (type == typeof(DiscoverySearchStats) && property.IsMultivalued)
			{
				return StoreValueConverter.ConvertStringArrayToMvpDiscoverySearchStats(originalValue);
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

		// Token: 0x06006069 RID: 24681 RVA: 0x00196968 File Offset: 0x00194B68
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
			if (originalValue is ExTimeZoneValue)
			{
				return ((ExTimeZoneValue)originalValue).ToString();
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
				return StoreValueConverter.ConvertMvpStringToStringArray((MultiValuedProperty<string>)originalValue);
			}
			if (originalValue is MultiValuedProperty<ADObjectId>)
			{
				return StoreValueConverter.ConvertMvpADObjectIdToStringArray((MultiValuedProperty<ADObjectId>)originalValue);
			}
			if (originalValue is MultiValuedProperty<KeywordHit>)
			{
				return StoreValueConverter.ConvertMvpToStringArray<KeywordHit>((MultiValuedProperty<KeywordHit>)originalValue);
			}
			if (originalValue is MultiValuedProperty<DiscoverySearchStats>)
			{
				return StoreValueConverter.ConvertMvpToStringArray<DiscoverySearchStats>((MultiValuedProperty<DiscoverySearchStats>)originalValue);
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

		// Token: 0x0600606A RID: 24682 RVA: 0x00196A94 File Offset: 0x00194C94
		internal static MultiValuedPropertyBase CreateGenericMultiValuedProperty(ProviderPropertyDefinition propertyDefinition, bool createAsReadOnly, ICollection values, ICollection invalidValues, LocalizedString? readOnlyErrorMessage)
		{
			MultiValuedPropertyBase result;
			if (!StoreValueConverter.TryCreateGenericMultiValuedProperty(propertyDefinition, createAsReadOnly, values, invalidValues, readOnlyErrorMessage, out result))
			{
				throw new NotImplementedException(DataStrings.ErrorMvpNotImplemented(propertyDefinition.Type.ToString(), propertyDefinition.Name));
			}
			return result;
		}

		// Token: 0x0600606B RID: 24683 RVA: 0x00196AD4 File Offset: 0x00194CD4
		internal static bool TryCreateGenericMultiValuedProperty(ProviderPropertyDefinition propertyDefinition, bool createAsReadOnly, ICollection values, ICollection invalidValues, LocalizedString? readOnlyErrorMessage, out MultiValuedPropertyBase mvp)
		{
			mvp = null;
			if (propertyDefinition.Type == typeof(MigrationBatchError))
			{
				mvp = new MultiValuedProperty<MigrationBatchError>(createAsReadOnly, propertyDefinition, values, invalidValues, readOnlyErrorMessage);
			}
			if (propertyDefinition.Type == typeof(MigrationError))
			{
				mvp = new MultiValuedProperty<MigrationError>(createAsReadOnly, propertyDefinition, values, invalidValues, readOnlyErrorMessage);
			}
			if (propertyDefinition.Type == typeof(MigrationUserSkippedItem))
			{
				mvp = new MultiValuedProperty<MigrationUserSkippedItem>(createAsReadOnly, propertyDefinition, values, invalidValues, readOnlyErrorMessage);
			}
			if (propertyDefinition.Type == typeof(MigrationReportSet))
			{
				mvp = new MultiValuedProperty<MigrationReportSet>(createAsReadOnly, propertyDefinition, values, invalidValues, readOnlyErrorMessage);
			}
			if (propertyDefinition.Type == typeof(E164Number))
			{
				mvp = new MultiValuedProperty<E164Number>(createAsReadOnly, propertyDefinition, values, invalidValues, readOnlyErrorMessage);
			}
			if (propertyDefinition.Type == typeof(ADObjectId))
			{
				mvp = new MultiValuedProperty<ADObjectId>(createAsReadOnly, propertyDefinition, values, invalidValues, readOnlyErrorMessage);
			}
			if (propertyDefinition.Type == typeof(ADRecipientOrAddress))
			{
				mvp = new MultiValuedProperty<ADRecipientOrAddress>(createAsReadOnly, propertyDefinition, values, invalidValues, readOnlyErrorMessage);
			}
			return mvp != null || ValueConvertor.TryCreateGenericMultiValuedProperty(propertyDefinition, createAsReadOnly, values, invalidValues, readOnlyErrorMessage, out mvp);
		}

		// Token: 0x0600606C RID: 24684 RVA: 0x00196BFC File Offset: 0x00194DFC
		private static string[] ConvertMvpStringToStringArray(MultiValuedProperty<string> mvps)
		{
			if (mvps == null)
			{
				return null;
			}
			return mvps.ToArray();
		}

		// Token: 0x0600606D RID: 24685 RVA: 0x00196C2D File Offset: 0x00194E2D
		private static string[] ConvertMvpADObjectIdToStringArray(MultiValuedProperty<ADObjectId> mvpIds)
		{
			if (mvpIds != null)
			{
				return Array.ConvertAll<ADObjectId, string>(mvpIds.ToArray(), (ADObjectId mvpId) => mvpId.ObjectGuid.ToString());
			}
			return Array<string>.Empty;
		}

		// Token: 0x0600606E RID: 24686 RVA: 0x00196C6F File Offset: 0x00194E6F
		private static string[] ConvertMvpToStringArray<T>(MultiValuedProperty<T> mvp)
		{
			if (mvp != null)
			{
				return Array.ConvertAll<T, string>(mvp.ToArray(), (T obj) => obj.ToString());
			}
			return Array<string>.Empty;
		}

		// Token: 0x0600606F RID: 24687 RVA: 0x00196C94 File Offset: 0x00194E94
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

		// Token: 0x06006070 RID: 24688 RVA: 0x00196D04 File Offset: 0x00194F04
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

		// Token: 0x06006071 RID: 24689 RVA: 0x00196DB0 File Offset: 0x00194FB0
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

		// Token: 0x06006072 RID: 24690 RVA: 0x00196DF8 File Offset: 0x00194FF8
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

		// Token: 0x06006073 RID: 24691 RVA: 0x00196ECC File Offset: 0x001950CC
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
