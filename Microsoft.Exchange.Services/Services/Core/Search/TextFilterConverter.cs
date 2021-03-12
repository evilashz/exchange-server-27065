using System;
using System.Xml;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Services.Core.DataConverter;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Core.Search
{
	// Token: 0x02000285 RID: 645
	internal class TextFilterConverter : BaseLeafFilterConverter
	{
		// Token: 0x060010C7 RID: 4295 RVA: 0x00051434 File Offset: 0x0004F634
		internal override QueryFilter ConvertToQueryFilter(SearchExpressionType searchExpression)
		{
			ContainsExpressionType containsExpressionType = searchExpression as ContainsExpressionType;
			SubFilterType subFilterType = SubFilterBuilder.ValidateExpressionForSubFilter(containsExpressionType);
			if (subFilterType != SubFilterType.None)
			{
				return SubFilterBuilder.BuildSubFilter(containsExpressionType, subFilterType);
			}
			PropertyDefinition andValidatePropertyDefinitionForQuery = BaseLeafFilterConverter.GetAndValidatePropertyDefinitionForQuery(containsExpressionType.Item);
			if (andValidatePropertyDefinitionForQuery.Type != typeof(string))
			{
				throw new ContainsFilterWrongTypeException();
			}
			MatchOptions matchOptions = SubFilterBuilder.ExtractMatchOptions(containsExpressionType);
			MatchFlags matchFlags = SubFilterBuilder.ExtractMatchFlags(containsExpressionType);
			string text = containsExpressionType.Constant.Value;
			text = base.ConvertSmtpToExAddress(andValidatePropertyDefinitionForQuery, text);
			return new TextFilter(andValidatePropertyDefinitionForQuery, text, matchOptions, matchFlags);
		}

		// Token: 0x060010C8 RID: 4296 RVA: 0x000514B8 File Offset: 0x0004F6B8
		internal override SearchExpressionType ConvertToSearchExpression(QueryFilter queryFilter)
		{
			TextFilter textFilter = (TextFilter)queryFilter;
			return new ContainsExpressionType
			{
				Constant = new ConstantValueType
				{
					Value = textFilter.Text
				},
				ContainmentComparisonString = SubFilterBuilder.ExtractMatchFlagsText(textFilter),
				ContainmentModeString = SubFilterBuilder.ExtractMatchOptionsText(textFilter),
				Item = SearchSchemaMap.GetPropertyPath(textFilter.Property)
			};
		}

		// Token: 0x060010C9 RID: 4297 RVA: 0x00051518 File Offset: 0x0004F718
		internal static XmlElement CreateContainsElementFromTextFilter(XmlDocument xmlDocument, TextFilter textFilter)
		{
			XmlElement xmlElement = ServiceXml.CreateElement(xmlDocument, "Contains", "http://schemas.microsoft.com/exchange/services/2006/types");
			string text = SubFilterBuilder.ExtractMatchOptionsText(textFilter);
			string text2 = SubFilterBuilder.ExtractMatchFlagsText(textFilter);
			if (!string.IsNullOrEmpty(text))
			{
				ServiceXml.CreateAttribute(xmlElement, "ContainmentMode", text);
			}
			if (!string.IsNullOrEmpty(text2))
			{
				ServiceXml.CreateAttribute(xmlElement, "ContainmentComparison", text2);
			}
			return xmlElement;
		}
	}
}
