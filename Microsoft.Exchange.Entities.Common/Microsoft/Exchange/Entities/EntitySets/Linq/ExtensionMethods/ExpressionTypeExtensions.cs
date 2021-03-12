using System;
using System.Linq.Expressions;
using Microsoft.Exchange.Data;

namespace Microsoft.Exchange.Entities.EntitySets.Linq.ExtensionMethods
{
	// Token: 0x0200004E RID: 78
	public static class ExpressionTypeExtensions
	{
		// Token: 0x060001A8 RID: 424 RVA: 0x0000662C File Offset: 0x0000482C
		internal static QueryFilter ToBlobComparisonFilter(this ExpressionType expressionType, PropertyDefinition propertyDefinition, byte[] propertyValue)
		{
			BinaryFilter binaryFilter = new BinaryFilter(propertyDefinition, propertyValue, MatchOptions.ExactPhrase, MatchFlags.Default);
			if (expressionType == ExpressionType.Equal)
			{
				return binaryFilter;
			}
			if (expressionType != ExpressionType.NotEqual)
			{
				throw new UnsupportedExpressionException(Strings.UnsupportedOperatorWithNull(expressionType));
			}
			return new NotFilter(binaryFilter);
		}

		// Token: 0x060001A9 RID: 425 RVA: 0x00006668 File Offset: 0x00004868
		internal static ComparisonOperator ToComparisonOperator(this ExpressionType expressionType)
		{
			switch (expressionType)
			{
			case ExpressionType.Equal:
				return ComparisonOperator.Equal;
			case ExpressionType.ExclusiveOr:
			case ExpressionType.Invoke:
			case ExpressionType.Lambda:
			case ExpressionType.LeftShift:
				break;
			case ExpressionType.GreaterThan:
				return ComparisonOperator.GreaterThan;
			case ExpressionType.GreaterThanOrEqual:
				return ComparisonOperator.GreaterThanOrEqual;
			case ExpressionType.LessThan:
				return ComparisonOperator.LessThan;
			case ExpressionType.LessThanOrEqual:
				return ComparisonOperator.LessThanOrEqual;
			default:
				if (expressionType == ExpressionType.NotEqual)
				{
					return ComparisonOperator.NotEqual;
				}
				break;
			}
			throw new UnsupportedExpressionException(Strings.UnsupportedOperator(expressionType));
		}

		// Token: 0x060001AA RID: 426 RVA: 0x000066C4 File Offset: 0x000048C4
		internal static QueryFilter ToNullComparisonFilter(this ExpressionType expressionType, PropertyDefinition propertyDefinition)
		{
			ExistsFilter existsFilter = new ExistsFilter(propertyDefinition);
			if (expressionType == ExpressionType.Equal)
			{
				return new NotFilter(existsFilter);
			}
			if (expressionType == ExpressionType.NotEqual)
			{
				return existsFilter;
			}
			throw new UnsupportedExpressionException(Strings.UnsupportedOperatorWithNull(expressionType));
		}
	}
}
