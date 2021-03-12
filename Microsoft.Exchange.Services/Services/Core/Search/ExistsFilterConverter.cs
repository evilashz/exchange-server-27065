using System;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Core.Search
{
	// Token: 0x02000267 RID: 615
	internal class ExistsFilterConverter : BaseLeafFilterConverter
	{
		// Token: 0x0600101E RID: 4126 RVA: 0x0004DC38 File Offset: 0x0004BE38
		internal override QueryFilter ConvertToQueryFilter(SearchExpressionType element)
		{
			ExistsType existsType = element as ExistsType;
			ExistsFilterConverter.ConfirmNotMessageFlag(existsType.Item as PropertyUri);
			PropertyDefinition andValidatePropertyDefinitionForQuery = BaseLeafFilterConverter.GetAndValidatePropertyDefinitionForQuery(existsType.Item);
			return new ExistsFilter(andValidatePropertyDefinitionForQuery);
		}

		// Token: 0x0600101F RID: 4127 RVA: 0x0004DC70 File Offset: 0x0004BE70
		internal override SearchExpressionType ConvertToSearchExpression(QueryFilter queryFilter)
		{
			ExistsFilter existsFilter = (ExistsFilter)queryFilter;
			return new ExistsType
			{
				Item = SearchSchemaMap.GetPropertyPath(existsFilter.Property)
			};
		}

		// Token: 0x06001020 RID: 4128 RVA: 0x0004DC9C File Offset: 0x0004BE9C
		private static void ConfirmNotMessageFlag(PropertyUri uri)
		{
			if (uri == null)
			{
				return;
			}
			PropertyUriEnum uri2 = uri.Uri;
			switch (uri2)
			{
			case PropertyUriEnum.IsDraft:
			case PropertyUriEnum.IsFromMe:
			case PropertyUriEnum.IsResend:
			case PropertyUriEnum.IsSubmitted:
			case PropertyUriEnum.IsUnmodified:
				break;
			default:
				if (uri2 != PropertyUriEnum.IsRead)
				{
					return;
				}
				break;
			}
			throw new InvalidPropertyForExistsException(uri);
		}
	}
}
