using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Xml;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Data.Storage.Management;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.Migration.Logging;

namespace Microsoft.Exchange.Migration
{
	// Token: 0x020000C8 RID: 200
	internal static class MigrationHelper
	{
		// Token: 0x06000AA0 RID: 2720 RVA: 0x0002C976 File Offset: 0x0002AB76
		public static string AppendDiagnosticHistory(string history, params string[] entryFields)
		{
			return MigrationHelper.MigrationDiagnosticHistory.AppendDiagnosticHistory(history, entryFields);
		}

		// Token: 0x06000AA1 RID: 2721 RVA: 0x0002C980 File Offset: 0x0002AB80
		internal static T? GetEnumPropertyOrNull<T>(IMigrationStoreObject item, StorePropertyDefinition propertyDefinition) where T : struct
		{
			object valueOrDefault = item.GetValueOrDefault<object>(propertyDefinition, null);
			if (valueOrDefault == null)
			{
				return null;
			}
			return new T?(MigrationHelper.GetEnumProperty<T>(propertyDefinition, valueOrDefault));
		}

		// Token: 0x06000AA2 RID: 2722 RVA: 0x0002C9AF File Offset: 0x0002ABAF
		internal static T GetEnumProperty<T>(IMigrationStoreObject item, StorePropertyDefinition propertyDefinition) where T : struct
		{
			return MigrationHelper.GetEnumProperty<T>(propertyDefinition, item[propertyDefinition]);
		}

		// Token: 0x06000AA3 RID: 2723 RVA: 0x0002C9C0 File Offset: 0x0002ABC0
		internal static ExTimeZone GetExTimeZoneProperty(IPropertyBag item, StorePropertyDefinition propertyDefinition)
		{
			Exception ex = null;
			object objectValue = item[propertyDefinition];
			ExTimeZone exTimeZoneValue = MigrationHelper.GetExTimeZoneValue(objectValue, ref ex);
			if (ex != null)
			{
				throw new InvalidDataException(MigrationHelper.GetPropertyErrorMessage(propertyDefinition, objectValue), ex);
			}
			return exTimeZoneValue;
		}

		// Token: 0x06000AA4 RID: 2724 RVA: 0x0002C9F4 File Offset: 0x0002ABF4
		internal static ExTimeZone GetExTimeZoneValue(object objectValue, ref Exception ex)
		{
			try
			{
				if (ExTimeZone.UtcTimeZone.Id.Equals((string)objectValue, StringComparison.Ordinal))
				{
					return ExTimeZone.UtcTimeZone;
				}
				return ExTimeZoneValue.Parse((string)objectValue).ExTimeZone;
			}
			catch (FormatException ex2)
			{
				ex = ex2;
			}
			return null;
		}

		// Token: 0x06000AA5 RID: 2725 RVA: 0x0002CA50 File Offset: 0x0002AC50
		internal static CultureInfo GetCultureInfoPropertyOrDefault(IMigrationStoreObject item, PropertyDefinition propertyDefinition)
		{
			string valueOrDefault = item.GetValueOrDefault<string>(propertyDefinition, "en-US");
			CultureInfo result;
			try
			{
				result = new CultureInfo(valueOrDefault);
			}
			catch (ArgumentException innerException)
			{
				throw new InvalidDataException(MigrationHelper.GetPropertyErrorMessage(propertyDefinition, valueOrDefault), innerException);
			}
			return result;
		}

		// Token: 0x06000AA6 RID: 2726 RVA: 0x0002CA94 File Offset: 0x0002AC94
		internal static ExDateTime GetExDateTimePropertyOrDefault(IMigrationStoreObject item, StorePropertyDefinition propertyDefinition, ExDateTime defaultTime)
		{
			ExDateTime? exDateTimePropertyOrNull = MigrationHelper.GetExDateTimePropertyOrNull(item, propertyDefinition);
			if (exDateTimePropertyOrNull != null)
			{
				return exDateTimePropertyOrNull.Value;
			}
			return defaultTime;
		}

		// Token: 0x06000AA7 RID: 2727 RVA: 0x0002CABC File Offset: 0x0002ACBC
		internal static ExDateTime? GetExDateTimePropertyOrNull(IMigrationStoreObject item, StorePropertyDefinition propertyDefinition)
		{
			object property = MigrationHelper.GetProperty<object>(item, propertyDefinition, false);
			return MigrationHelperBase.GetValidExDateTime(property as ExDateTime?);
		}

		// Token: 0x06000AA8 RID: 2728 RVA: 0x0002CAE4 File Offset: 0x0002ACE4
		internal static Fqdn GetFqdnProperty(IMigrationStoreObject item, StorePropertyDefinition propertyDefinition, bool required)
		{
			Exception innerException = null;
			try
			{
				string property = MigrationHelper.GetProperty<string>(item, propertyDefinition, required);
				if (property == null)
				{
					return null;
				}
				return new Fqdn(property);
			}
			catch (FormatException ex)
			{
				innerException = ex;
			}
			throw new InvalidDataException(MigrationHelper.GetPropertyErrorMessage(propertyDefinition, null), innerException);
		}

		// Token: 0x06000AA9 RID: 2729 RVA: 0x0002CB30 File Offset: 0x0002AD30
		internal static SmtpAddress GetSmtpAddressProperty(IMigrationStoreObject item, StorePropertyDefinition propertyDefinition)
		{
			string property = MigrationHelper.GetProperty<string>(item, propertyDefinition, false);
			if (string.IsNullOrEmpty(property))
			{
				return SmtpAddress.Empty;
			}
			return MigrationHelper.GetSmtpAddress(property, propertyDefinition);
		}

		// Token: 0x06000AAA RID: 2730 RVA: 0x0002CB5C File Offset: 0x0002AD5C
		internal static SmtpAddress GetSmtpAddress(string smtpAddress, StorePropertyDefinition propertyDefinition)
		{
			Exception innerException = null;
			try
			{
				return SmtpAddress.Parse(smtpAddress);
			}
			catch (PropertyErrorException ex)
			{
				innerException = ex;
			}
			catch (FormatException ex2)
			{
				innerException = ex2;
			}
			throw new InvalidDataException(MigrationHelper.GetPropertyErrorMessage(propertyDefinition, null), innerException);
		}

		// Token: 0x06000AAB RID: 2731 RVA: 0x0002CBAC File Offset: 0x0002ADAC
		internal static Guid GetGuidProperty(IMigrationStoreObject item, StorePropertyDefinition propertyDefinition, bool required)
		{
			Guid valueOrDefault = item.GetValueOrDefault<Guid>(propertyDefinition, Guid.Empty);
			if (required && valueOrDefault == Guid.Empty)
			{
				throw new InvalidDataException(MigrationHelper.GetPropertyErrorMessage(propertyDefinition, null));
			}
			return valueOrDefault;
		}

		// Token: 0x06000AAC RID: 2732 RVA: 0x0002CBE4 File Offset: 0x0002ADE4
		internal static ADObjectId GetADObjectId(IMigrationStoreObject item, StorePropertyDefinition propertyDefinition)
		{
			return MigrationHelper.GetADObjectIdImpl(item, propertyDefinition, false);
		}

		// Token: 0x06000AAD RID: 2733 RVA: 0x0002CBEE File Offset: 0x0002ADEE
		internal static bool TryGetADObjectId(IMigrationStoreObject item, StorePropertyDefinition propertyDefinition, out ADObjectId id)
		{
			id = MigrationHelper.GetADObjectIdImpl(item, propertyDefinition, true);
			return id != null;
		}

		// Token: 0x06000AAE RID: 2734 RVA: 0x0002CC04 File Offset: 0x0002AE04
		internal static StoreObjectId GetObjectIdProperty(IMigrationStoreObject item, StorePropertyDefinition propertyDefinition, bool required)
		{
			Exception innerException = null;
			try
			{
				byte[] property = MigrationHelper.GetProperty<byte[]>(item, propertyDefinition, required);
				if (property == null)
				{
					return null;
				}
				return MigrationHelperBase.GetStoreObjectId(property);
			}
			catch (CorruptDataException ex)
			{
				innerException = ex;
			}
			throw new InvalidDataException(MigrationHelper.GetPropertyErrorMessage(propertyDefinition, null), innerException);
		}

		// Token: 0x06000AAF RID: 2735 RVA: 0x0002CC5C File Offset: 0x0002AE5C
		internal static Unlimited<int>? ReadUnlimitedProperty(IMigrationStoreObject bag, PropertyDefinition property)
		{
			Unlimited<int>? result;
			try
			{
				string text = bag[property] as string;
				if (string.IsNullOrEmpty(text))
				{
					result = null;
				}
				else
				{
					result = new Unlimited<int>?(Unlimited<int>.Parse(text));
				}
			}
			catch (PropertyErrorException ex)
			{
				if (!ex.PropertyErrors.Any((PropertyError error) => error.PropertyErrorCode == PropertyErrorCode.NotFound))
				{
					throw;
				}
				result = null;
			}
			catch (KeyNotFoundException)
			{
				result = null;
			}
			return result;
		}

		// Token: 0x06000AB0 RID: 2736 RVA: 0x0002CD00 File Offset: 0x0002AF00
		internal static void WriteUnlimitedProperty(IMigrationStoreObject bag, PropertyDefinition property, Unlimited<int>? value)
		{
			if (value != null)
			{
				bag[property] = value.Value.ToString();
				return;
			}
			bag.Delete(property);
		}

		// Token: 0x06000AB1 RID: 2737 RVA: 0x0002CD3A File Offset: 0x0002AF3A
		internal static void WriteOrDeleteNullableProperty<T>(IMigrationStoreObject bag, PropertyDefinition property, T value)
		{
			if (value != null)
			{
				bag[property] = value;
				return;
			}
			bag.Delete(property);
		}

		// Token: 0x06000AB2 RID: 2738 RVA: 0x0002CD5C File Offset: 0x0002AF5C
		internal static T GetProperty<T>(IMigrationStoreObject item, PropertyDefinition propertyDefinition, bool required) where T : class
		{
			Exception innerException = null;
			try
			{
				if (required)
				{
					return (T)((object)item[propertyDefinition]);
				}
				return item.GetValueOrDefault<T>(propertyDefinition, default(T));
			}
			catch (PropertyErrorException ex)
			{
				innerException = ex;
			}
			catch (InvalidCastException ex2)
			{
				innerException = ex2;
			}
			catch (CorruptDataException ex3)
			{
				innerException = ex3;
			}
			catch (InvalidDataException ex4)
			{
				innerException = ex4;
			}
			throw new MigrationDataCorruptionException(MigrationHelper.GetPropertyErrorMessage(propertyDefinition, null), innerException);
		}

		// Token: 0x06000AB3 RID: 2739 RVA: 0x0002CDF0 File Offset: 0x0002AFF0
		internal static PersistableDictionary GetDictionaryProperty(IMigrationStoreObject item, PropertyDefinition propertyDefinition, bool required)
		{
			MigrationUtil.ThrowOnNullArgument(item, "item");
			MigrationUtil.ThrowOnNullArgument(propertyDefinition, "propertyDefinition");
			string property = MigrationHelper.GetProperty<string>(item, propertyDefinition, required);
			if (property == null)
			{
				return null;
			}
			PersistableDictionary result;
			try
			{
				result = PersistableDictionary.Create(property);
			}
			catch (XmlException innerException)
			{
				throw new InvalidDataException(MigrationHelper.GetPropertyErrorMessage(propertyDefinition, property), innerException);
			}
			return result;
		}

		// Token: 0x06000AB4 RID: 2740 RVA: 0x0002CE4C File Offset: 0x0002B04C
		internal static void SetDictionaryProperty(IPropertyBag item, PropertyDefinition propertyDefinition, PersistableDictionary persistDictionary)
		{
			MigrationUtil.ThrowOnNullArgument(item, "item");
			MigrationUtil.ThrowOnNullArgument(propertyDefinition, "propertyDefinition");
			MigrationUtil.ThrowOnNullArgument(persistDictionary, "persistDictionary");
			item[propertyDefinition] = persistDictionary.Serialize();
		}

		// Token: 0x06000AB5 RID: 2741 RVA: 0x0002CE7C File Offset: 0x0002B07C
		internal static string GetPropertyErrorMessage(PropertyDefinition propertyDefinition, object objectValue)
		{
			return string.Format(CultureInfo.InvariantCulture, "Property error: {0}={1}", new object[]
			{
				(propertyDefinition == null) ? "Null" : propertyDefinition.ToString(),
				(objectValue == null) ? "Null" : objectValue.ToString()
			});
		}

		// Token: 0x06000AB6 RID: 2742 RVA: 0x0002CEC8 File Offset: 0x0002B0C8
		internal static MultiValuedProperty<T> ToMultiValuedProperty<T>(IEnumerable<T> collection)
		{
			if (collection is MultiValuedProperty<T>)
			{
				return (MultiValuedProperty<T>)collection;
			}
			MultiValuedProperty<T> multiValuedProperty = new MultiValuedProperty<T>();
			foreach (T item in collection)
			{
				multiValuedProperty.TryAdd(item);
			}
			return multiValuedProperty;
		}

		// Token: 0x06000AB7 RID: 2743 RVA: 0x0002CFA4 File Offset: 0x0002B1A4
		internal static IEnumerable<StoreObjectId> FindMessageIds(IMigrationDataProvider dataProvider, MigrationEqualityFilter primaryFilter, MigrationEqualityFilter[] secondaryFilters, SortBy[] additionalSortCriteria, int? maxCount)
		{
			if (secondaryFilters == null)
			{
				secondaryFilters = new MigrationEqualityFilter[0];
			}
			if (additionalSortCriteria == null)
			{
				additionalSortCriteria = new SortBy[0];
			}
			PropertyDefinition[] array = new PropertyDefinition[secondaryFilters.Length + additionalSortCriteria.Length];
			SortBy[] array2 = new SortBy[secondaryFilters.Length + additionalSortCriteria.Length];
			for (int i = 0; i < secondaryFilters.Length; i++)
			{
				array[i] = secondaryFilters[i].Property;
				array2[i] = new SortBy(secondaryFilters[i].Property, SortOrder.Ascending);
			}
			for (int j = 0; j < additionalSortCriteria.Length; j++)
			{
				int num = secondaryFilters.Length + j;
				array[num] = additionalSortCriteria[j].ColumnDefinition;
				array2[num] = additionalSortCriteria[j];
			}
			bool matchRegionStarted = false;
			return dataProvider.FindMessageIds(primaryFilter, array, array2, delegate(IDictionary<PropertyDefinition, object> rowData)
			{
				if (!MigrationHelper.FitsFilter(primaryFilter, rowData[primaryFilter.Property]))
				{
					return MigrationRowSelectorResult.RejectRowStopProcessing;
				}
				foreach (MigrationEqualityFilter migrationEqualityFilter in secondaryFilters)
				{
					if (!MigrationHelper.FitsFilter(migrationEqualityFilter, rowData[migrationEqualityFilter.Property]))
					{
						MigrationRowSelectorResult result;
						if (!matchRegionStarted)
						{
							result = MigrationRowSelectorResult.RejectRowContinueProcessing;
						}
						else
						{
							result = MigrationRowSelectorResult.RejectRowStopProcessing;
						}
						return result;
					}
				}
				matchRegionStarted = true;
				return MigrationRowSelectorResult.AcceptRow;
			}, maxCount);
		}

		// Token: 0x06000AB8 RID: 2744 RVA: 0x0002D0A4 File Offset: 0x0002B2A4
		internal static bool FitsFilter(MigrationEqualityFilter filter, object xsoValue)
		{
			object value = filter.Value;
			ComparisonFilter comparisonFilter = filter.Filter as ComparisonFilter;
			if (comparisonFilter == null || comparisonFilter.ComparisonOperator == ComparisonOperator.Equal)
			{
				return MigrationHelper.IsEqualXsoValues(value, xsoValue);
			}
			ComparisonOperator comparisonOperator = comparisonFilter.ComparisonOperator;
			if (comparisonOperator == ComparisonOperator.NotEqual)
			{
				return !MigrationHelper.IsEqualXsoValues(value, xsoValue);
			}
			if (value == null && xsoValue == null)
			{
				return comparisonOperator == ComparisonOperator.Equal || comparisonOperator == ComparisonOperator.LessThanOrEqual || comparisonOperator == ComparisonOperator.GreaterThanOrEqual;
			}
			if (value == null && xsoValue != null)
			{
				return comparisonOperator == ComparisonOperator.GreaterThan || comparisonOperator == ComparisonOperator.GreaterThanOrEqual;
			}
			if (value != null && xsoValue == null)
			{
				return comparisonOperator == ComparisonOperator.LessThan || comparisonOperator == ComparisonOperator.LessThanOrEqual;
			}
			IComparable obj;
			IComparable comparable;
			if (value is ExDateTime?)
			{
				obj = (value as ExDateTime?).Value;
				comparable = (xsoValue as ExDateTime?).Value;
			}
			else
			{
				obj = (value as IComparable);
				comparable = (xsoValue as IComparable);
			}
			switch (comparisonFilter.ComparisonOperator)
			{
			case ComparisonOperator.LessThan:
				return comparable.CompareTo(obj) < 0;
			case ComparisonOperator.LessThanOrEqual:
				return comparable.CompareTo(obj) <= 0;
			case ComparisonOperator.GreaterThan:
				return comparable.CompareTo(obj) > 0;
			case ComparisonOperator.GreaterThanOrEqual:
				return comparable.CompareTo(obj) >= 0;
			default:
				return false;
			}
		}

		// Token: 0x06000AB9 RID: 2745 RVA: 0x0002D1D0 File Offset: 0x0002B3D0
		internal static bool IsEqualXsoValues(object propertyDefinitionValue, object xsoValue)
		{
			if (object.Equals(propertyDefinitionValue, xsoValue))
			{
				return true;
			}
			if (xsoValue == null)
			{
				return false;
			}
			if (xsoValue is string)
			{
				return string.Equals(propertyDefinitionValue as string, xsoValue as string, StringComparison.OrdinalIgnoreCase);
			}
			if (propertyDefinitionValue is Enum)
			{
				if (Enum.GetUnderlyingType(propertyDefinitionValue.GetType()) != xsoValue.GetType())
				{
					return false;
				}
				propertyDefinitionValue = Convert.ChangeType(propertyDefinitionValue, xsoValue.GetType());
				return object.Equals(propertyDefinitionValue, xsoValue);
			}
			else
			{
				byte[] array = xsoValue as byte[];
				if (array == null)
				{
					return false;
				}
				byte[] array2 = propertyDefinitionValue as byte[];
				if (array2 == null)
				{
					return false;
				}
				if (array2.Length != array.Length)
				{
					return false;
				}
				for (int i = 0; i < array2.Length; i++)
				{
					if (array2[i] != array[i])
					{
						return false;
					}
				}
				return true;
			}
		}

		// Token: 0x06000ABA RID: 2746 RVA: 0x0002D27B File Offset: 0x0002B47B
		internal static void VerifyMigrationTypeEquality(MigrationType expected, MigrationType actual)
		{
			if (expected != actual)
			{
				throw new ArgumentException(string.Format("IMAPMigrationJobSettings expects a batch of type {0} but found {1}", expected, actual));
			}
		}

		// Token: 0x06000ABB RID: 2747 RVA: 0x0002D2A0 File Offset: 0x0002B4A0
		internal static T[] AggregateArrays<T>(params IList<T>[] itemLists)
		{
			int num = 0;
			foreach (IList<T> list in itemLists)
			{
				if (list != null)
				{
					num += list.Count;
				}
			}
			T[] array = new T[num];
			foreach (IList<T> list2 in itemLists)
			{
				if (list2 != null)
				{
					list2.CopyTo(array, array.Length - num);
					num -= list2.Count;
				}
			}
			return array;
		}

		// Token: 0x06000ABC RID: 2748 RVA: 0x0002D313 File Offset: 0x0002B513
		internal static PropertyDefinition[] AggregateProperties(params IList<PropertyDefinition>[] items)
		{
			return MigrationHelper.AggregateArrays<PropertyDefinition>(items);
		}

		// Token: 0x06000ABD RID: 2749 RVA: 0x0002D31B File Offset: 0x0002B51B
		internal static string ToTruncatedString(this string val)
		{
			if (val == null || val.Length <= 15)
			{
				return val;
			}
			return val.Substring(0, 15);
		}

		// Token: 0x06000ABE RID: 2750 RVA: 0x0002D338 File Offset: 0x0002B538
		internal static ExDateTime? GetUniversalDateTime(ExDateTime? dateTime)
		{
			if (dateTime == null)
			{
				return null;
			}
			return new ExDateTime?(dateTime.Value.ToUtc());
		}

		// Token: 0x06000ABF RID: 2751 RVA: 0x0002D36C File Offset: 0x0002B56C
		internal static ExDateTime? GetLocalizedDateTime(ExDateTime? dateTime, ExTimeZone timeZone)
		{
			if (dateTime == null)
			{
				return null;
			}
			if (timeZone == null)
			{
				return dateTime;
			}
			return new ExDateTime?(timeZone.ConvertDateTime(dateTime.Value));
		}

		// Token: 0x06000AC0 RID: 2752 RVA: 0x0002D3A4 File Offset: 0x0002B5A4
		internal static void RunUpdateOperation(Action updateOperation)
		{
			for (int i = 1; i <= 3; i++)
			{
				try
				{
					updateOperation();
					break;
				}
				catch (TransientException ex)
				{
					MigrationLogger.Log(MigrationEventType.Warning, "Encountered a transient exception: {0}", new object[]
					{
						ex
					});
					if (i == 3)
					{
						throw;
					}
				}
			}
		}

		// Token: 0x06000AC1 RID: 2753 RVA: 0x0002D3F8 File Offset: 0x0002B5F8
		internal static void SendFriendlyWatson(Exception ex, bool collectMemoryDump, string additionalData = null)
		{
			if (Debugger.IsAttached)
			{
				Debugger.Break();
			}
			ReportOptions reportOptions = ReportOptions.DeepStackTraceHash;
			if (!collectMemoryDump)
			{
				reportOptions |= ReportOptions.DoNotCollectDumps;
			}
			ExWatson.SendReport(ex, reportOptions, additionalData);
		}

		// Token: 0x06000AC2 RID: 2754 RVA: 0x0002D424 File Offset: 0x0002B624
		private static T GetEnumProperty<T>(StorePropertyDefinition propertyDefinition, object objectValue) where T : struct
		{
			Exception innerException = null;
			try
			{
				return (T)((object)objectValue);
			}
			catch (PropertyErrorException ex)
			{
				innerException = ex;
			}
			catch (FormatException ex2)
			{
				innerException = ex2;
			}
			catch (InvalidCastException ex3)
			{
				innerException = ex3;
			}
			throw new InvalidDataException(MigrationHelper.GetPropertyErrorMessage(propertyDefinition, objectValue), innerException);
		}

		// Token: 0x06000AC3 RID: 2755 RVA: 0x0002D488 File Offset: 0x0002B688
		private static ADObjectId GetADObjectIdImpl(IMigrationStoreObject item, StorePropertyDefinition propertyDefinition, bool useTryGet)
		{
			MigrationUtil.ThrowOnNullArgument(item, "item");
			MigrationUtil.ThrowOnNullArgument(propertyDefinition, "propertyDefinition");
			Exception innerException = null;
			try
			{
				byte[] array;
				if (useTryGet)
				{
					array = item.GetValueOrDefault<byte[]>(propertyDefinition, null);
					if (array == null)
					{
						return null;
					}
				}
				else
				{
					array = (byte[])item[propertyDefinition];
				}
				return new ADObjectId(array);
			}
			catch (CorruptDataException ex)
			{
				innerException = ex;
			}
			catch (ArgumentNullException ex2)
			{
				innerException = ex2;
			}
			throw new InvalidDataException(MigrationHelper.GetPropertyErrorMessage(propertyDefinition, null), innerException);
		}

		// Token: 0x04000418 RID: 1048
		internal const int MaxNumberOfRowsInOneChunk = 100;

		// Token: 0x04000419 RID: 1049
		private const string DefaultLanguage = "en-US";

		// Token: 0x0400041A RID: 1050
		private const int MaximumRetryCount = 3;

		// Token: 0x0400041B RID: 1051
		internal static readonly PropertyDefinition[] ItemIdProperties = new PropertyDefinition[]
		{
			ItemSchema.Id
		};

		// Token: 0x020000C9 RID: 201
		private static class MigrationDiagnosticHistory
		{
			// Token: 0x06000AC6 RID: 2758 RVA: 0x0002D53C File Offset: 0x0002B73C
			public static string AppendDiagnosticHistory(string history, params string[] entryFields)
			{
				MigrationUtil.ThrowOnNullArgument(history, "history");
				int num = history.Count((char p) => p == ';');
				if (num >= 35)
				{
					int num2 = -1;
					while (num-- >= 35)
					{
						num2 = history.IndexOf(';', num2 + 1);
						if (num2 < 0)
						{
							break;
						}
					}
					if (num2 < 0)
					{
						throw new MigrationDataCorruptionException(string.Format("bad format of history {0}, expected to find delim", history));
					}
					history = history.Substring(num2);
				}
				return history + string.Join(":", entryFields) + ';';
			}

			// Token: 0x0400041D RID: 1053
			private const char HistoryEntryDelim = ';';

			// Token: 0x0400041E RID: 1054
			private const string HistoryEntryValueDelim = ":";

			// Token: 0x0400041F RID: 1055
			private const int MaximumHistoryEntryValue = 35;
		}
	}
}
