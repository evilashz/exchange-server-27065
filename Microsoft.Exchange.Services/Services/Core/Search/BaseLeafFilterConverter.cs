using System;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics.Components.Services;
using Microsoft.Exchange.Services.Core.DataConverter;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Core.Search
{
	// Token: 0x0200025A RID: 602
	internal abstract class BaseLeafFilterConverter : BaseSingleFilterConverter
	{
		// Token: 0x170001EE RID: 494
		// (get) Token: 0x06000FBC RID: 4028 RVA: 0x0004CDA1 File Offset: 0x0004AFA1
		internal override bool IsLeafFilter
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06000FBD RID: 4029
		internal abstract QueryFilter ConvertToQueryFilter(SearchExpressionType searchExpression);

		// Token: 0x06000FBE RID: 4030
		internal abstract SearchExpressionType ConvertToSearchExpression(QueryFilter queryFilter);

		// Token: 0x06000FBF RID: 4031 RVA: 0x0004CDA4 File Offset: 0x0004AFA4
		protected static bool TryGetOperandAsConstantValue(object operandElement, out string constantValue)
		{
			ConstantValueType constantValueType = operandElement as ConstantValueType;
			if (constantValueType != null)
			{
				constantValue = constantValueType.Value;
				return true;
			}
			constantValue = string.Empty;
			return false;
		}

		// Token: 0x06000FC0 RID: 4032 RVA: 0x0004CDD0 File Offset: 0x0004AFD0
		protected static object GetConvertedValueForPropertyDefinition(PropertyDefinition propDef, string valueAsString)
		{
			object result;
			try
			{
				result = BaseConverter.GetConverterForPropertyDefinition(propDef).ConvertToObject(valueAsString);
			}
			catch (UnsupportedTypeForConversionException innerException)
			{
				throw new InvalidValueForPropertyException(SearchSchemaMap.GetPropertyPath(propDef), innerException);
			}
			catch (FormatException innerException2)
			{
				throw new InvalidValueForPropertyException(SearchSchemaMap.GetPropertyPath(propDef), innerException2);
			}
			catch (OverflowException innerException3)
			{
				throw new InvalidValueForPropertyException(SearchSchemaMap.GetPropertyPath(propDef), innerException3);
			}
			catch (InvalidCastException innerException4)
			{
				throw new InvalidValueForPropertyException(SearchSchemaMap.GetPropertyPath(propDef), innerException4);
			}
			catch (ArgumentNullException innerException5)
			{
				throw new InvalidValueForPropertyException(SearchSchemaMap.GetPropertyPath(propDef), innerException5);
			}
			return result;
		}

		// Token: 0x06000FC1 RID: 4033 RVA: 0x0004CE78 File Offset: 0x0004B078
		protected static string GetStringForPropertyValue(object value)
		{
			if (value == null)
			{
				return null;
			}
			return BaseConverter.GetConverterForType(value.GetType()).ConvertToString(value);
		}

		// Token: 0x06000FC2 RID: 4034 RVA: 0x0004CE90 File Offset: 0x0004B090
		protected static PropertyDefinition GetAndValidatePropertyDefinitionForQuery(PropertyPath propertyPath)
		{
			PropertyDefinition propertyDefinition;
			if (!SearchSchemaMap.TryGetPropertyDefinition(propertyPath, out propertyDefinition))
			{
				throw new UnsupportedPathForQueryException(propertyPath);
			}
			StorePropertyDefinition storePropertyDefinition = (StorePropertyDefinition)propertyDefinition;
			if ((storePropertyDefinition.Capabilities & StorePropertyCapabilities.CanQuery) != StorePropertyCapabilities.CanQuery)
			{
				throw new UnsupportedPathForQueryException(propertyPath);
			}
			return propertyDefinition;
		}

		// Token: 0x06000FC3 RID: 4035 RVA: 0x0004CEC8 File Offset: 0x0004B0C8
		protected string ConvertSmtpToExAddress(PropertyDefinition propertyDefinition, string smtpAddressIn)
		{
			if (propertyDefinition != MessageItemSchema.SenderEmailAddress && propertyDefinition != ItemSchema.SentRepresentingEmailAddress)
			{
				if (propertyDefinition != CalendarItemBaseSchema.OrganizerEmailAddress)
				{
					return smtpAddressIn;
				}
			}
			try
			{
				Participant participant = new Participant(string.Empty, smtpAddressIn, "SMTP");
				Participant participant2 = MailboxHelper.TryConvertTo(participant, "EX");
				if (participant2 != null)
				{
					return participant2.EmailAddress;
				}
			}
			catch (ArgumentException ex)
			{
				ExTraceGlobals.ExceptionTracer.TraceError<string, string, string>((long)this.GetHashCode(), "[BaseLeafFilterConverter::ConvertSmtpToExAddress] Exception encountered trying to convert from address: '{0}'.  Exception: {1},  {2}", smtpAddressIn, ex.GetType().FullName, ex.Message);
				return smtpAddressIn;
			}
			catch (InvalidParticipantException ex2)
			{
				ExTraceGlobals.ExceptionTracer.TraceError<string, string, string>((long)this.GetHashCode(), "[BaseLeafFilterConverter::ConvertSmtpToExAddress] Exception encountered trying to convert from address: '{0}'.  Exception: {1},  {2}", smtpAddressIn, ex2.GetType().FullName, ex2.Message);
				return smtpAddressIn;
			}
			catch (NotSupportedException ex3)
			{
				ExTraceGlobals.ExceptionTracer.TraceError<string, string, string>((long)this.GetHashCode(), "[BaseLeafFilterConverter::ConvertSmtpToExAddress] Exception encountered trying to convert from address: '{0}'.  Exception: {1},  {2}", smtpAddressIn, ex3.GetType().FullName, ex3.Message);
				return smtpAddressIn;
			}
			return smtpAddressIn;
		}
	}
}
