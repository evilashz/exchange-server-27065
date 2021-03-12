using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics.Components.Services;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Core.DataConverter
{
	// Token: 0x020001D8 RID: 472
	internal abstract class BaseConverter
	{
		// Token: 0x06000CA1 RID: 3233 RVA: 0x0004152C File Offset: 0x0003F72C
		static BaseConverter()
		{
			BaseConverter.propDefTypeOverrideMap.Add(CalendarItemBaseSchema.FreeBusyStatus, typeof(BusyType));
			BaseConverter.propDefTypeOverrideMap.Add(CalendarItemBaseSchema.IntendedFreeBusyStatus, typeof(BusyType));
			BaseConverter.propDefTypeOverrideMap.Add(CalendarItemBaseSchema.ResponseType, typeof(ResponseType));
			BaseConverter.propDefTypeOverrideMap.Add(ContactSchema.FileAsId, typeof(FileAsMapping));
			BaseConverter.propDefTypeOverrideMap.Add(ContactSchema.PostalAddressId, typeof(ContactConverter.PostalAddressIndexType));
			BaseConverter.propDefTypeOverrideMap.Add(TaskSchema.DelegationState, typeof(TaskDelegateState));
			BaseConverter.propDefTypeOverrideMap.Add(ItemSchema.FlagStatus, typeof(FlagStatus));
			BaseConverter.propDefTypeOverrideMap.Add(ConversationItemSchema.ConversationImportance, typeof(Importance));
			BaseConverter.propDefTypeOverrideMap.Add(ConversationItemSchema.ConversationGlobalImportance, typeof(Importance));
			BaseConverter.propDefTypeOverrideMap.Add(ConversationItemSchema.ConversationFlagStatus, typeof(FlagStatus));
			BaseConverter.propDefTypeOverrideMap.Add(ConversationItemSchema.ConversationGlobalFlagStatus, typeof(FlagStatus));
			BaseConverter.typeToConverterMap = new Dictionary<Type, BaseConverter>();
			BaseConverter.AddConversionEntry(typeof(byte[]), new Base64StringConverter());
			BaseConverter.typeToConverterMap.Add(typeof(bool), new BooleanConverter());
			BaseConverter.AddConversionEntry(typeof(ExDateTime), new ExDateTimeConverter());
			BaseConverter.AddConversionEntry(typeof(double), new DoubleConverter());
			BaseConverter.AddConversionEntry(typeof(float), new FloatConverter());
			BaseConverter.AddConversionEntry(typeof(Guid), new GuidConverter());
			BaseConverter.AddConversionEntry(typeof(int), new IntConverter());
			BaseConverter.AddConversionEntry(typeof(long), new LongConverter());
			BaseConverter.AddConversionEntry(typeof(short), new ShortConverter());
			BaseConverter.AddConversionEntry(typeof(string), new StringConverter());
			BaseConverter.AddConversionEntry(typeof(BusyType), new BusyTypeConverter());
			BaseConverter.AddConversionEntry(typeof(CalendarItemType), new CalendarItemTypeConverter());
			BaseConverter.AddConversionEntry(typeof(FileAsMapping), new ContactConverter.FileAsMapping());
			BaseConverter.AddConversionEntry(typeof(FlagStatus), new FlagStatusConverter());
			BaseConverter.AddConversionEntry(typeof(Importance), new ImportanceConverter());
			BaseConverter.AddConversionEntry(typeof(ResponseType), new ResponseTypeConverter());
			BaseConverter.AddConversionEntry(typeof(Sensitivity), new SensitivityConverter());
			BaseConverter.AddConversionEntry(typeof(TaskDelegateState), new TaskDelegateStateConverter());
			BaseConverter.AddConversionEntry(typeof(AppointmentStateFlags), new IntConverter());
			BaseConverter.AddConversionEntry(typeof(ContactConverter.PostalAddressIndexType), new ContactConverter.PostalAddressIndex());
			BaseConverter.AddConversionEntry(typeof(EmailAddress), new EmailAddressValueConverter());
			BaseConverter.AddConversionEntry(typeof(PersonType), new PersonaTypeConverter());
			BaseConverter.AddConversionEntry(typeof(PhoneNumber), new PhoneNumberConverter());
			BaseConverter.AddConversionEntry(typeof(Participant), new ParticipantConverter());
			BaseConverter.AddConversionEntry<StoreObjectId>(new StoreObjectIdConverter());
			BaseConverter.AddConversionEntry<Attribution>(new AttributionConverter());
			BaseConverter.AddConversionEntry<AttributedValue<string>>(new StringAttributedValueConverter());
			BaseConverter.AddConversionEntry<AttributedValue<string[]>>(new StringArrayAttributedValueConverter());
			BaseConverter.AddConversionEntry<AttributedValue<ExDateTime>>(new ExDateTimeAttributedValueConverter());
			BaseConverter.AddConversionEntry<AttributedValue<EmailAddress>>(new EmailAddressAttributedValueConverter());
			BaseConverter.AddConversionEntry<AttributedValue<Participant>>(new EmailAddressAttributedValueConverter());
			BaseConverter.AddConversionEntry<AttributedValue<Microsoft.Exchange.Data.Storage.PostalAddress>>(new PostalAddressAttributedValueConverter());
			BaseConverter.AddConversionEntry<AttributedValue<PhoneNumber>>(new PhoneNumberAttributedValueConverter());
			BaseConverter.AddConversionEntry<AttributedValue<ContactExtendedPropertyData>>(new ExtendedPropertyAttributedValueConverter());
		}

		// Token: 0x06000CA2 RID: 3234 RVA: 0x000418A9 File Offset: 0x0003FAA9
		private static void AddConversionEntry(Type type, BaseConverter converter)
		{
			BaseConverter.typeToConverterMap.Add(type, converter);
			BaseConverter.typeToConverterMap.Add(type.MakeArrayType(), converter);
		}

		// Token: 0x06000CA3 RID: 3235 RVA: 0x000418C8 File Offset: 0x0003FAC8
		private static void AddConversionEntry<T>(BaseConverter converter)
		{
			BaseConverter.typeToConverterMap.Add(typeof(T), converter);
			BaseConverter.typeToConverterMap.Add(typeof(IEnumerable<T>), converter);
		}

		// Token: 0x06000CA4 RID: 3236
		public abstract object ConvertToObject(string propertyString);

		// Token: 0x06000CA5 RID: 3237
		public abstract string ConvertToString(object propertyValue);

		// Token: 0x06000CA6 RID: 3238 RVA: 0x000418F4 File Offset: 0x0003FAF4
		protected virtual object ConvertToServiceObjectValue(object propertyValue)
		{
			return propertyValue;
		}

		// Token: 0x06000CA7 RID: 3239 RVA: 0x000418F7 File Offset: 0x0003FAF7
		public virtual object ConvertToServiceObjectValue(object propertyValue, IdConverterWithCommandSettings idConverterWithCommandSettings)
		{
			return this.ConvertToServiceObjectValue(propertyValue);
		}

		// Token: 0x06000CA8 RID: 3240 RVA: 0x00041900 File Offset: 0x0003FB00
		private static Type GetTypeOverride(PropertyDefinition propertyDefinition)
		{
			Type result;
			if (BaseConverter.propDefTypeOverrideMap.TryGetValue(propertyDefinition, out result))
			{
				return result;
			}
			return propertyDefinition.Type;
		}

		// Token: 0x06000CA9 RID: 3241 RVA: 0x00041924 File Offset: 0x0003FB24
		public static bool TryGetConverterForType(Type type, out BaseConverter converter)
		{
			return BaseConverter.typeToConverterMap.TryGetValue(type, out converter);
		}

		// Token: 0x06000CAA RID: 3242 RVA: 0x00041934 File Offset: 0x0003FB34
		public static BaseConverter GetConverterForType(Type type)
		{
			BaseConverter result;
			if (!BaseConverter.TryGetConverterForType(type, out result))
			{
				ExTraceGlobals.CommonAlgorithmTracer.TraceError<Type>(0L, "BaseConverter.GetConverterForType throwing UnsupportedTypeForConversionException for type: {0}", type);
				throw new UnsupportedTypeForConversionException();
			}
			return result;
		}

		// Token: 0x06000CAB RID: 3243 RVA: 0x00041964 File Offset: 0x0003FB64
		public static BaseConverter GetConverterForPropertyDefinition(PropertyDefinition propertyDefinition)
		{
			BaseConverter result;
			if (!BaseConverter.TryGetConverterForPropertyDefinition(propertyDefinition, out result))
			{
				ExTraceGlobals.CommonAlgorithmTracer.TraceError<PropertyDefinition>(0L, "BaseConverter.GetConverterForPropertyDefinition throwing UnsupportedTypeForConversionException for property: {0}", propertyDefinition);
				throw new UnsupportedTypeForConversionException();
			}
			return result;
		}

		// Token: 0x06000CAC RID: 3244 RVA: 0x00041994 File Offset: 0x0003FB94
		public static bool TryGetConverterForPropertyDefinition(PropertyDefinition propertyDefinition, out BaseConverter converter)
		{
			Type typeOverride = BaseConverter.GetTypeOverride(propertyDefinition);
			return BaseConverter.typeToConverterMap.TryGetValue(typeOverride, out converter);
		}

		// Token: 0x04000A4F RID: 2639
		private static Dictionary<PropertyDefinition, Type> propDefTypeOverrideMap = new Dictionary<PropertyDefinition, Type>();

		// Token: 0x04000A50 RID: 2640
		private static Dictionary<Type, BaseConverter> typeToConverterMap;
	}
}
