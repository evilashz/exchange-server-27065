using System;
using System.Globalization;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Diagnostics.Components.Services;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Core.Search
{
	// Token: 0x0200025F RID: 607
	internal class BitmaskFilterConverter : BaseLeafFilterConverter
	{
		// Token: 0x06000FEC RID: 4076 RVA: 0x0004D270 File Offset: 0x0004B470
		internal override QueryFilter ConvertToQueryFilter(SearchExpressionType searchExpression)
		{
			ExcludesType excludesType = searchExpression as ExcludesType;
			string text = excludesType.Bitmask.Value;
			string.IsNullOrEmpty(text);
			uint num = 0U;
			NumberStyles style = NumberStyles.None;
			if (text.StartsWith("0x", StringComparison.OrdinalIgnoreCase))
			{
				style = NumberStyles.HexNumber;
				text = text.Replace("0x", string.Empty).Replace("0X", string.Empty);
			}
			if (!uint.TryParse(text, style, null, out num))
			{
				throw new InvalidRestrictionException(CoreResources.IDs.ErrorInvalidExcludesRestriction);
			}
			PropertyDefinition andValidatePropertyDefinitionForQuery = BaseLeafFilterConverter.GetAndValidatePropertyDefinitionForQuery(excludesType.Item);
			QueryFilter result;
			try
			{
				result = new BitMaskFilter(andValidatePropertyDefinitionForQuery, (ulong)num, false);
			}
			catch (ArgumentOutOfRangeException ex)
			{
				ExTraceGlobals.SearchTracer.TraceError<string, Type, ArgumentOutOfRangeException>((long)this.GetHashCode(), "[BitmaskFilterConverter::ConvertToQueryFilter] Encountered ArgumentOutOfRangeException when trying to create an MED.BitmaskFilter.  This happens when trying to use Excludes on a non-integer type. Property Name: [{0}]; Property Type: [{1}]; Exception: {2}", andValidatePropertyDefinitionForQuery.Name, andValidatePropertyDefinitionForQuery.Type, ex);
				throw new InvalidRestrictionException(CoreResources.IDs.ErrorInvalidRestriction, ex);
			}
			return result;
		}

		// Token: 0x06000FED RID: 4077 RVA: 0x0004D354 File Offset: 0x0004B554
		private static string ConvertBitmaskValueToString(ulong mask)
		{
			return string.Format(CultureInfo.InvariantCulture, "0x{0:X}", new object[]
			{
				mask
			});
		}

		// Token: 0x06000FEE RID: 4078 RVA: 0x0004D384 File Offset: 0x0004B584
		internal override SearchExpressionType ConvertToSearchExpression(QueryFilter queryFilter)
		{
			BitMaskFilter bitMaskFilter = (BitMaskFilter)queryFilter;
			ExcludesType excludesType = new ExcludesType
			{
				Bitmask = new ExcludesValueType
				{
					Value = BitmaskFilterConverter.ConvertBitmaskValueToString(bitMaskFilter.Mask)
				},
				Item = SearchSchemaMap.GetPropertyPath(bitMaskFilter.Property)
			};
			if (bitMaskFilter.IsNonZero)
			{
				return new NotType
				{
					Item = excludesType
				};
			}
			return excludesType;
		}
	}
}
