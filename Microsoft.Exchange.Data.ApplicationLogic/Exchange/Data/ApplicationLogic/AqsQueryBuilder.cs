using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Microsoft.Exchange.Data.Search.AqsParser;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.Data.ApplicationLogic
{
	// Token: 0x02000099 RID: 153
	internal static class AqsQueryBuilder
	{
		// Token: 0x060006A8 RID: 1704 RVA: 0x000180A8 File Offset: 0x000162A8
		internal static void AppendDateClause(StringBuilder query, PropertyKeyword keyword, DateRangeQueryOperation operation, DateTime date)
		{
			EnumValidator.ThrowIfInvalid<PropertyKeyword>(keyword, AqsQueryBuilder.ValidDateKeywords);
			EnumValidator.ThrowIfInvalid<DateRangeQueryOperation>(operation);
			AqsQueryBuilder.AppendLeadingSpaceIfNecessary(query);
			query.Append(keyword.ToString().ToLower()).Append(":(");
			switch (operation)
			{
			case DateRangeQueryOperation.Equal:
				query.Append("=");
				break;
			case DateRangeQueryOperation.GreaterThan:
				query.Append(">");
				break;
			case DateRangeQueryOperation.GreaterThanOrEqual:
				query.Append(">=");
				break;
			case DateRangeQueryOperation.LessThan:
				query.Append("<");
				break;
			case DateRangeQueryOperation.LessThanOrEqual:
				query.Append("<=");
				break;
			}
			query.Append(date.ToLocalTime().ToString(AqsQueryBuilder.AqsDateTimeFormat, CultureInfo.InvariantCulture));
			query.Append(")");
		}

		// Token: 0x060006A9 RID: 1705 RVA: 0x0001817C File Offset: 0x0001637C
		internal static void AppendKeywordOrClause(StringBuilder query, PropertyKeyword keyword, ICollection<string> valuesToQuery)
		{
			EnumValidator.ThrowIfInvalid<PropertyKeyword>(keyword);
			if (valuesToQuery != null && valuesToQuery.Count > 0)
			{
				bool flag = false;
				foreach (string value in valuesToQuery)
				{
					if (!string.IsNullOrEmpty(value))
					{
						if (!flag)
						{
							flag = true;
							AqsQueryBuilder.AppendLeadingSpaceIfNecessary(query);
							query.Append(keyword.ToString().ToLower()).Append(":(");
						}
						else
						{
							query.Append(AqsQueryBuilder.AqsQueryORSeparator);
						}
						query.Append("\"").Append(value).Append("\"");
					}
				}
				if (flag)
				{
					query.Append(")");
				}
			}
		}

		// Token: 0x060006AA RID: 1706 RVA: 0x00018244 File Offset: 0x00016444
		private static void AppendLeadingSpaceIfNecessary(StringBuilder query)
		{
			if (query.Length > 0)
			{
				query.Append(" ");
			}
		}

		// Token: 0x040002F2 RID: 754
		private static readonly string AqsDateTimeFormat = "yyyy-MM-dd HH:mm:ss";

		// Token: 0x040002F3 RID: 755
		private static readonly string AqsQueryORSeparator = " OR ";

		// Token: 0x040002F4 RID: 756
		private static readonly PropertyKeyword[] ValidDateKeywords = new PropertyKeyword[]
		{
			PropertyKeyword.Sent,
			PropertyKeyword.Received
		};
	}
}
