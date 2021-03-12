using System;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Core.Search
{
	// Token: 0x02000262 RID: 610
	internal class ComparisonFilterConverter : BaseLeafFilterConverter
	{
		// Token: 0x06000FFE RID: 4094 RVA: 0x0004D688 File Offset: 0x0004B888
		internal override SearchExpressionType ConvertToSearchExpression(QueryFilter queryFilter)
		{
			ComparisonFilter comparisonFilter = queryFilter as ComparisonFilter;
			if (comparisonFilter != null)
			{
				return ComparisonFilterConverter.ConvertFromComparisonFilter(comparisonFilter);
			}
			return ComparisonFilterConverter.ConvertFromPropertyComparisonFilter((PropertyComparisonFilter)queryFilter);
		}

		// Token: 0x06000FFF RID: 4095 RVA: 0x0004D6B4 File Offset: 0x0004B8B4
		internal override QueryFilter ConvertToQueryFilter(SearchExpressionType searchExpression)
		{
			TwoOperandExpressionType twoOperandExpressionType = searchExpression as TwoOperandExpressionType;
			ComparisonOperator comparisonOperator = ComparisonFilterConverter.ElementNameToComparisonOperator(twoOperandExpressionType.FilterType);
			PropertyDefinition andValidatePropertyDefinitionForQuery = BaseLeafFilterConverter.GetAndValidatePropertyDefinitionForQuery(twoOperandExpressionType.Item);
			string text = string.Empty;
			if (BaseLeafFilterConverter.TryGetOperandAsConstantValue(twoOperandExpressionType.FieldURIOrConstant.Item, out text))
			{
				try
				{
					text = base.ConvertSmtpToExAddress(andValidatePropertyDefinitionForQuery, text);
					object propertyValue;
					if (andValidatePropertyDefinitionForQuery is ItemIdProperty)
					{
						propertyValue = IdConverter.EwsIdToMessageStoreObjectId(text);
					}
					else if (andValidatePropertyDefinitionForQuery is ConversationIdFromIndexProperty)
					{
						propertyValue = IdConverter.EwsIdToConversationId(text);
					}
					else
					{
						propertyValue = BaseLeafFilterConverter.GetConvertedValueForPropertyDefinition(andValidatePropertyDefinitionForQuery, text);
					}
					return new ComparisonFilter(comparisonOperator, andValidatePropertyDefinitionForQuery, propertyValue);
				}
				catch (UnsupportedTypeForConversionException innerException)
				{
					throw new UnsupportedPathForQueryException(andValidatePropertyDefinitionForQuery, innerException);
				}
			}
			PropertyDefinition andValidatePropertyDefinitionForQuery2 = BaseLeafFilterConverter.GetAndValidatePropertyDefinitionForQuery(twoOperandExpressionType.FieldURIOrConstant.Item as PropertyPath);
			return new PropertyComparisonFilter(comparisonOperator, andValidatePropertyDefinitionForQuery, andValidatePropertyDefinitionForQuery2);
		}

		// Token: 0x06001000 RID: 4096 RVA: 0x0004D780 File Offset: 0x0004B980
		private static SearchExpressionType ConvertFromComparisonFilter(ComparisonFilter comparisonFilter)
		{
			string stringForPropertyValue = BaseLeafFilterConverter.GetStringForPropertyValue(comparisonFilter.PropertyValue);
			TwoOperandExpressionType twoOperandExpressionType = ComparisonFilterConverter.ComparisonOperatorToSearchExpression(comparisonFilter.ComparisonOperator);
			twoOperandExpressionType.Item = SearchSchemaMap.GetPropertyPath(comparisonFilter.Property);
			twoOperandExpressionType.FieldURIOrConstant = new FieldURIOrConstantType
			{
				Item = new ConstantValueType
				{
					Value = stringForPropertyValue
				}
			};
			return twoOperandExpressionType;
		}

		// Token: 0x06001001 RID: 4097 RVA: 0x0004D7D8 File Offset: 0x0004B9D8
		private static SearchExpressionType ConvertFromPropertyComparisonFilter(PropertyComparisonFilter propertyComparisonFilter)
		{
			PropertyPath propertyPath = SearchSchemaMap.GetPropertyPath(propertyComparisonFilter.Property1);
			PropertyPath propertyPath2 = SearchSchemaMap.GetPropertyPath(propertyComparisonFilter.Property2);
			TwoOperandExpressionType twoOperandExpressionType = ComparisonFilterConverter.ComparisonOperatorToSearchExpression(propertyComparisonFilter.ComparisonOperator);
			twoOperandExpressionType.Item = propertyPath;
			twoOperandExpressionType.FieldURIOrConstant = new FieldURIOrConstantType
			{
				Item = propertyPath2
			};
			return twoOperandExpressionType;
		}

		// Token: 0x06001002 RID: 4098 RVA: 0x0004D828 File Offset: 0x0004BA28
		private static ComparisonOperator ElementNameToComparisonOperator(string elementName)
		{
			switch (elementName)
			{
			case "IsEqualTo":
				return ComparisonOperator.Equal;
			case "IsNotEqualTo":
				return ComparisonOperator.NotEqual;
			case "IsGreaterThan":
				return ComparisonOperator.GreaterThan;
			case "IsGreaterThanOrEqualTo":
				return ComparisonOperator.GreaterThanOrEqual;
			case "IsLessThan":
				return ComparisonOperator.LessThan;
			case "IsLessThanOrEqualTo":
				return ComparisonOperator.LessThanOrEqual;
			}
			return ComparisonOperator.Equal;
		}

		// Token: 0x06001003 RID: 4099 RVA: 0x0004D8DC File Offset: 0x0004BADC
		private static string ComparisonOperatorToElementName(ComparisonOperator comparisonOperator)
		{
			string result = string.Empty;
			switch (comparisonOperator)
			{
			case ComparisonOperator.Equal:
				result = "IsEqualTo";
				break;
			case ComparisonOperator.NotEqual:
				result = "IsNotEqualTo";
				break;
			case ComparisonOperator.LessThan:
				result = "IsLessThan";
				break;
			case ComparisonOperator.LessThanOrEqual:
				result = "IsLessThanOrEqualTo";
				break;
			case ComparisonOperator.GreaterThan:
				result = "IsGreaterThan";
				break;
			case ComparisonOperator.GreaterThanOrEqual:
				result = "IsGreaterThanOrEqualTo";
				break;
			}
			return result;
		}

		// Token: 0x06001004 RID: 4100 RVA: 0x0004D940 File Offset: 0x0004BB40
		private static TwoOperandExpressionType ComparisonOperatorToSearchExpression(ComparisonOperator comparisonOperator)
		{
			switch (comparisonOperator)
			{
			case ComparisonOperator.Equal:
				return new IsEqualToType();
			case ComparisonOperator.NotEqual:
				return new IsNotEqualToType();
			case ComparisonOperator.LessThan:
				return new IsLessThanType();
			case ComparisonOperator.LessThanOrEqual:
				return new IsLessThanOrEqualToType();
			case ComparisonOperator.GreaterThan:
				return new IsGreaterThanType();
			case ComparisonOperator.GreaterThanOrEqual:
				return new IsGreaterThanOrEqualToType();
			default:
				return null;
			}
		}
	}
}
