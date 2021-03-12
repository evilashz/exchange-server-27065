using System;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.WebServices.Data;

namespace Microsoft.Exchange.Data.Storage.Management
{
	// Token: 0x020009CB RID: 2507
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal static class EwsStoreValueConverter
	{
		// Token: 0x06005CA1 RID: 23713 RVA: 0x00181C08 File Offset: 0x0017FE08
		internal static object ConvertValueFromStore(EwsStoreObjectPropertyDefinition property, object originalValue)
		{
			if (originalValue == null)
			{
				return null;
			}
			Type type = originalValue.GetType();
			if (type == typeof(EmailAddressCollection) && property.Type == typeof(ADRecipientOrAddress))
			{
				return new MultiValuedProperty<ADRecipientOrAddress>(false, property, (from x in (EmailAddressCollection)originalValue
				select EwsStoreValueConverter.ConvertValueFromStore(property, x)).ToArray<object>());
			}
			if (type == typeof(PolicyTag))
			{
				return ((PolicyTag)originalValue).RetentionId;
			}
			Type type2 = property.Type;
			if (type2 == typeof(ADRecipientOrAddress))
			{
				EmailAddress emailAddress = (EmailAddress)originalValue;
				return new ADRecipientOrAddress(new Participant(emailAddress.Name, emailAddress.Address, emailAddress.RoutingType));
			}
			if (!(type2 != typeof(byte[])) || !(property.StorePropertyDefinition.Type == typeof(byte[])) || !(originalValue is byte[]))
			{
				return StoreValueConverter.ConvertValueFromStore(property, originalValue);
			}
			if (type2 == typeof(ADObjectId))
			{
				return StoreValueConverter.ConvertValueFromStore(property, originalValue);
			}
			return Convert.ChangeType(EwsStoreValueConverter.DeserializeFromBinary((byte[])originalValue), type2);
		}

		// Token: 0x06005CA2 RID: 23714 RVA: 0x00181D84 File Offset: 0x0017FF84
		internal static object ConvertValueToStore(EwsStoreObjectPropertyDefinition property, object originalValue)
		{
			if (originalValue == null)
			{
				return null;
			}
			Type type = originalValue.GetType();
			if (type == typeof(ADRecipientOrAddress))
			{
				ADRecipientOrAddress adrecipientOrAddress = (ADRecipientOrAddress)originalValue;
				return new EmailAddress(adrecipientOrAddress.DisplayName, adrecipientOrAddress.Address, adrecipientOrAddress.RoutingType);
			}
			if (type == typeof(MultiValuedProperty<ADRecipientOrAddress>))
			{
				return (from x in (MultiValuedProperty<ADRecipientOrAddress>)originalValue
				select x.Address).ToArray<string>();
			}
			if (type == typeof(AuditLogSearchId))
			{
				return ((AuditLogSearchId)originalValue).Guid;
			}
			if (type == typeof(ADObjectId) && property.StorePropertyDefinition.Type == typeof(byte[]))
			{
				return ((ADObjectId)originalValue).GetBytes();
			}
			if (type == typeof(MultiValuedProperty<ADObjectId>) && property.IsMultivalued && property.StorePropertyDefinition is ExtendedPropertyDefinition && ((ExtendedPropertyDefinition)property.StorePropertyDefinition).MapiType == 3)
			{
				return (from x in (MultiValuedProperty<ADObjectId>)originalValue
				select x.GetBytes()).ToArray<byte[]>();
			}
			if (type != typeof(byte[]) && property.StorePropertyDefinition.Type == typeof(byte[]))
			{
				return EwsStoreValueConverter.SerializeToBinary(originalValue);
			}
			if (!(property.StorePropertyDefinition.Type == typeof(PolicyTag)))
			{
				return ValueConvertor.ConvertValue(StoreValueConverter.ConvertValueToStore(originalValue), EwsStoreValueConverter.GetStorePropertyDefinitionActualType(property), null);
			}
			if ((Guid?)originalValue == null)
			{
				return null;
			}
			return new PolicyTag(true, (Guid)originalValue);
		}

		// Token: 0x06005CA3 RID: 23715 RVA: 0x00181F58 File Offset: 0x00180158
		internal static Type GetStorePropertyDefinitionActualType(EwsStoreObjectPropertyDefinition property)
		{
			if (property.IsMultivalued)
			{
				if (property.StorePropertyDefinition is ExtendedPropertyDefinition && ((ExtendedPropertyDefinition)property.StorePropertyDefinition).MapiType == 3)
				{
					return typeof(byte[][]);
				}
				return typeof(string[]);
			}
			else
			{
				if (property.StorePropertyDefinition == ItemSchema.Attachments)
				{
					return typeof(byte[]);
				}
				return property.StorePropertyDefinition.Type;
			}
		}

		// Token: 0x06005CA4 RID: 23716 RVA: 0x00181FC8 File Offset: 0x001801C8
		internal static byte[] SerializeToBinary(object input)
		{
			if (input == null)
			{
				return null;
			}
			BinaryFormatter binaryFormatter = ExchangeBinaryFormatterFactory.CreateBinaryFormatter(null);
			byte[] result;
			using (MemoryStream memoryStream = new MemoryStream())
			{
				binaryFormatter.Serialize(memoryStream, input);
				result = memoryStream.ToArray();
			}
			return result;
		}

		// Token: 0x06005CA5 RID: 23717 RVA: 0x00182014 File Offset: 0x00180214
		internal static object DeserializeFromBinary(byte[] input)
		{
			if (input == null || input.Length == 0)
			{
				return null;
			}
			BinaryFormatter binaryFormatter = ExchangeBinaryFormatterFactory.CreateBinaryFormatter(null);
			object result;
			using (MemoryStream memoryStream = new MemoryStream(input))
			{
				result = binaryFormatter.Deserialize(memoryStream);
			}
			return result;
		}
	}
}
