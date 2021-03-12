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

namespace Microsoft.Exchange.AnchorService.Storage
{
	// Token: 0x02000032 RID: 50
	internal static class AnchorHelper
	{
		// Token: 0x06000228 RID: 552 RVA: 0x00008236 File Offset: 0x00006436
		public static string AppendDiagnosticHistory(string history, params string[] entryFields)
		{
			return AnchorHelper.MigrationDiagnosticHistory.AppendDiagnosticHistory(history, entryFields);
		}

		// Token: 0x06000229 RID: 553 RVA: 0x00008240 File Offset: 0x00006440
		internal static T? GetEnumPropertyOrNull<T>(IAnchorStoreObject item, StorePropertyDefinition propertyDefinition) where T : struct
		{
			object valueOrDefault = item.GetValueOrDefault<object>(propertyDefinition, null);
			if (valueOrDefault == null)
			{
				return null;
			}
			return new T?(AnchorHelper.GetEnumProperty<T>(propertyDefinition, valueOrDefault));
		}

		// Token: 0x0600022A RID: 554 RVA: 0x0000826F File Offset: 0x0000646F
		internal static T GetEnumProperty<T>(IAnchorStoreObject item, StorePropertyDefinition propertyDefinition) where T : struct
		{
			return AnchorHelper.GetEnumProperty<T>(propertyDefinition, item[propertyDefinition]);
		}

		// Token: 0x0600022B RID: 555 RVA: 0x00008280 File Offset: 0x00006480
		internal static ExTimeZone GetExTimeZoneProperty(IPropertyBag item, StorePropertyDefinition propertyDefinition)
		{
			Exception ex = null;
			object objectValue = item[propertyDefinition];
			ExTimeZone exTimeZoneValue = AnchorHelper.GetExTimeZoneValue(objectValue, ref ex);
			if (ex != null)
			{
				throw new InvalidDataException(AnchorHelper.GetPropertyErrorMessage(propertyDefinition, objectValue), ex);
			}
			return exTimeZoneValue;
		}

		// Token: 0x0600022C RID: 556 RVA: 0x000082B4 File Offset: 0x000064B4
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

		// Token: 0x0600022D RID: 557 RVA: 0x00008310 File Offset: 0x00006510
		internal static CultureInfo GetCultureInfoPropertyOrDefault(IAnchorStoreObject item, PropertyDefinition propertyDefinition)
		{
			string valueOrDefault = item.GetValueOrDefault<string>(propertyDefinition, "en-US");
			CultureInfo result;
			try
			{
				result = new CultureInfo(valueOrDefault);
			}
			catch (ArgumentException innerException)
			{
				throw new InvalidDataException(AnchorHelper.GetPropertyErrorMessage(propertyDefinition, valueOrDefault), innerException);
			}
			return result;
		}

		// Token: 0x0600022E RID: 558 RVA: 0x00008354 File Offset: 0x00006554
		internal static ExDateTime GetExDateTimePropertyOrDefault(IAnchorStoreObject item, StorePropertyDefinition propertyDefinition, ExDateTime defaultTime)
		{
			ExDateTime? exDateTimePropertyOrNull = AnchorHelper.GetExDateTimePropertyOrNull(item, propertyDefinition);
			if (exDateTimePropertyOrNull != null)
			{
				return exDateTimePropertyOrNull.Value;
			}
			return defaultTime;
		}

		// Token: 0x0600022F RID: 559 RVA: 0x0000837C File Offset: 0x0000657C
		internal static ExDateTime? GetExDateTimePropertyOrNull(IAnchorStoreObject item, StorePropertyDefinition propertyDefinition)
		{
			object property = AnchorHelper.GetProperty<object>(item, propertyDefinition, false);
			return MigrationHelperBase.GetValidExDateTime(property as ExDateTime?);
		}

		// Token: 0x06000230 RID: 560 RVA: 0x000083A4 File Offset: 0x000065A4
		internal static Fqdn GetFqdnProperty(IAnchorStoreObject item, StorePropertyDefinition propertyDefinition, bool required)
		{
			Exception innerException = null;
			try
			{
				string property = AnchorHelper.GetProperty<string>(item, propertyDefinition, required);
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
			throw new InvalidDataException(AnchorHelper.GetPropertyErrorMessage(propertyDefinition, null), innerException);
		}

		// Token: 0x06000231 RID: 561 RVA: 0x000083F0 File Offset: 0x000065F0
		internal static SmtpAddress GetSmtpAddressProperty(IAnchorStoreObject item, StorePropertyDefinition propertyDefinition)
		{
			string property = AnchorHelper.GetProperty<string>(item, propertyDefinition, false);
			if (string.IsNullOrEmpty(property))
			{
				return SmtpAddress.Empty;
			}
			return AnchorHelper.GetSmtpAddress(property, propertyDefinition);
		}

		// Token: 0x06000232 RID: 562 RVA: 0x0000841C File Offset: 0x0000661C
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
			throw new InvalidDataException(AnchorHelper.GetPropertyErrorMessage(propertyDefinition, null), innerException);
		}

		// Token: 0x06000233 RID: 563 RVA: 0x0000846C File Offset: 0x0000666C
		internal static Guid GetGuidProperty(IAnchorStoreObject item, StorePropertyDefinition propertyDefinition, bool required)
		{
			Guid valueOrDefault = item.GetValueOrDefault<Guid>(propertyDefinition, Guid.Empty);
			if (required && valueOrDefault == Guid.Empty)
			{
				throw new InvalidDataException(AnchorHelper.GetPropertyErrorMessage(propertyDefinition, null));
			}
			return valueOrDefault;
		}

		// Token: 0x06000234 RID: 564 RVA: 0x000084A4 File Offset: 0x000066A4
		internal static ADObjectId GetADObjectId(IAnchorStoreObject item, StorePropertyDefinition propertyDefinition)
		{
			return AnchorHelper.GetADObjectIdImpl(item, propertyDefinition, false);
		}

		// Token: 0x06000235 RID: 565 RVA: 0x000084AE File Offset: 0x000066AE
		internal static bool TryGetADObjectId(IAnchorStoreObject item, StorePropertyDefinition propertyDefinition, out ADObjectId id)
		{
			id = AnchorHelper.GetADObjectIdImpl(item, propertyDefinition, true);
			return id != null;
		}

		// Token: 0x06000236 RID: 566 RVA: 0x000084C4 File Offset: 0x000066C4
		internal static StoreObjectId GetObjectIdProperty(IAnchorStoreObject item, StorePropertyDefinition propertyDefinition, bool required)
		{
			Exception innerException = null;
			try
			{
				byte[] property = AnchorHelper.GetProperty<byte[]>(item, propertyDefinition, required);
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
			throw new InvalidDataException(AnchorHelper.GetPropertyErrorMessage(propertyDefinition, null), innerException);
		}

		// Token: 0x06000237 RID: 567 RVA: 0x0000851C File Offset: 0x0000671C
		internal static Unlimited<int>? ReadUnlimitedProperty(IPropertyBag bag, PropertyDefinition property)
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

		// Token: 0x06000238 RID: 568 RVA: 0x000085C0 File Offset: 0x000067C0
		internal static T GetProperty<T>(IAnchorStoreObject item, PropertyDefinition propertyDefinition, bool required) where T : class
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
			throw new MigrationDataCorruptionException(AnchorHelper.GetPropertyErrorMessage(propertyDefinition, null), innerException);
		}

		// Token: 0x06000239 RID: 569 RVA: 0x00008654 File Offset: 0x00006854
		internal static PersistableDictionary GetDictionaryProperty(IAnchorStoreObject item, PropertyDefinition propertyDefinition, bool required)
		{
			AnchorUtil.ThrowOnNullArgument(item, "item");
			AnchorUtil.ThrowOnNullArgument(propertyDefinition, "propertyDefinition");
			string property = AnchorHelper.GetProperty<string>(item, propertyDefinition, required);
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
				throw new InvalidDataException(AnchorHelper.GetPropertyErrorMessage(propertyDefinition, property), innerException);
			}
			return result;
		}

		// Token: 0x0600023A RID: 570 RVA: 0x000086B0 File Offset: 0x000068B0
		internal static void SetDictionaryProperty(IPropertyBag item, PropertyDefinition propertyDefinition, PersistableDictionary persistDictionary)
		{
			AnchorUtil.ThrowOnNullArgument(item, "item");
			AnchorUtil.ThrowOnNullArgument(propertyDefinition, "propertyDefinition");
			AnchorUtil.ThrowOnNullArgument(persistDictionary, "persistDictionary");
			item[propertyDefinition] = persistDictionary.Serialize();
		}

		// Token: 0x0600023B RID: 571 RVA: 0x000086E0 File Offset: 0x000068E0
		internal static string GetPropertyErrorMessage(PropertyDefinition propertyDefinition, object objectValue)
		{
			return string.Format(CultureInfo.InvariantCulture, "Property error: {0}={1}", new object[]
			{
				(propertyDefinition == null) ? "Null" : propertyDefinition.ToString(),
				(objectValue == null) ? "Null" : objectValue.ToString()
			});
		}

		// Token: 0x0600023C RID: 572 RVA: 0x0000872C File Offset: 0x0000692C
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

		// Token: 0x0600023D RID: 573 RVA: 0x0000878C File Offset: 0x0000698C
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

		// Token: 0x0600023E RID: 574 RVA: 0x000087FF File Offset: 0x000069FF
		internal static PropertyDefinition[] AggregateProperties(params IList<PropertyDefinition>[] items)
		{
			return AnchorHelper.AggregateArrays<PropertyDefinition>(items);
		}

		// Token: 0x0600023F RID: 575 RVA: 0x00008807 File Offset: 0x00006A07
		internal static ProviderPropertyDefinition GetDefaultPropertyDefinition(string propertyName, PropertyDefinitionConstraint[] constraints)
		{
			if (constraints == null)
			{
				constraints = PropertyDefinitionConstraint.None;
			}
			return new SimpleProviderPropertyDefinition(propertyName, ExchangeObjectVersion.Exchange2010, typeof(string), PropertyDefinitionFlags.None, null, constraints, constraints);
		}

		// Token: 0x06000240 RID: 576 RVA: 0x0000882C File Offset: 0x00006A2C
		internal static string ToTruncatedString(this string val)
		{
			if (val == null || val.Length <= 15)
			{
				return val;
			}
			return val.Substring(0, 15);
		}

		// Token: 0x06000241 RID: 577 RVA: 0x00008848 File Offset: 0x00006A48
		internal static ExDateTime? GetUniversalDateTime(ExDateTime? dateTime)
		{
			if (dateTime == null)
			{
				return null;
			}
			return new ExDateTime?(dateTime.Value.ToUtc());
		}

		// Token: 0x06000242 RID: 578 RVA: 0x0000887C File Offset: 0x00006A7C
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

		// Token: 0x06000243 RID: 579 RVA: 0x000088B4 File Offset: 0x00006AB4
		internal static void RunUpdateOperation(AnchorContext context, Action updateOperation)
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
					context.Logger.Log(MigrationEventType.Warning, "Encountered a transient exception", new object[]
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

		// Token: 0x06000244 RID: 580 RVA: 0x0000890C File Offset: 0x00006B0C
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

		// Token: 0x06000245 RID: 581 RVA: 0x00008938 File Offset: 0x00006B38
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
			throw new InvalidDataException(AnchorHelper.GetPropertyErrorMessage(propertyDefinition, objectValue), innerException);
		}

		// Token: 0x06000246 RID: 582 RVA: 0x0000899C File Offset: 0x00006B9C
		private static ADObjectId GetADObjectIdImpl(IAnchorStoreObject item, StorePropertyDefinition propertyDefinition, bool useTryGet)
		{
			AnchorUtil.ThrowOnNullArgument(item, "item");
			AnchorUtil.ThrowOnNullArgument(propertyDefinition, "propertyDefinition");
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
			throw new InvalidDataException(AnchorHelper.GetPropertyErrorMessage(propertyDefinition, null), innerException);
		}

		// Token: 0x04000097 RID: 151
		internal const int MaxNumberOfRowsInOneChunk = 100;

		// Token: 0x04000098 RID: 152
		private const string DefaultLanguage = "en-US";

		// Token: 0x04000099 RID: 153
		private const int MaximumRetryCount = 3;

		// Token: 0x0400009A RID: 154
		internal static readonly PropertyDefinition[] ItemIdProperties = new PropertyDefinition[]
		{
			ItemSchema.Id
		};

		// Token: 0x02000033 RID: 51
		private static class MigrationDiagnosticHistory
		{
			// Token: 0x06000249 RID: 585 RVA: 0x00008A50 File Offset: 0x00006C50
			public static string AppendDiagnosticHistory(string history, params string[] entryFields)
			{
				AnchorUtil.ThrowOnNullArgument(history, "history");
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

			// Token: 0x0400009C RID: 156
			private const char HistoryEntryDelim = ';';

			// Token: 0x0400009D RID: 157
			private const string HistoryEntryValueDelim = ":";

			// Token: 0x0400009E RID: 158
			private const int MaximumHistoryEntryValue = 35;
		}
	}
}
