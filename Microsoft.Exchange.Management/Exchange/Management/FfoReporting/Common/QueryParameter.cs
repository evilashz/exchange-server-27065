using System;
using System.Collections.Generic;
using System.Reflection;
using Microsoft.Exchange.Data;

namespace Microsoft.Exchange.Management.FfoReporting.Common
{
	// Token: 0x020003D5 RID: 981
	[AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
	internal sealed class QueryParameter : Attribute
	{
		// Token: 0x06002311 RID: 8977 RVA: 0x0008E76F File Offset: 0x0008C96F
		internal QueryParameter(string definition, params string[] optionalDefinitions)
		{
			this.definition = definition;
			this.optionalDefinitions = optionalDefinitions;
		}

		// Token: 0x17000A5B RID: 2651
		// (get) Token: 0x06002312 RID: 8978 RVA: 0x0008E785 File Offset: 0x0008C985
		// (set) Token: 0x06002313 RID: 8979 RVA: 0x0008E78D File Offset: 0x0008C98D
		public string MethodName { get; set; }

		// Token: 0x06002314 RID: 8980 RVA: 0x0008E798 File Offset: 0x0008C998
		internal void AddFilter(List<ComparisonFilter> filters, object value)
		{
			if (string.IsNullOrWhiteSpace(this.MethodName))
			{
				if (value is MultiValuedPropertyBase)
				{
					value = Schema.Utilities.CreateDataTable((MultiValuedPropertyBase)value);
				}
				filters.Add(new ComparisonFilter(ComparisonOperator.Equal, Schema.Utilities.GetSchemaPropertyDefinition(this.definition), value));
				return;
			}
			Type type = base.GetType();
			MethodInfo method = type.GetMethod(this.MethodName, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
			if (method == null)
			{
				throw new NullReferenceException("Unknown method name");
			}
			Schema.Utilities.Invoke(method, this, new object[]
			{
				filters,
				value
			});
		}

		// Token: 0x06002315 RID: 8981 RVA: 0x0008E824 File Offset: 0x0008CA24
		private void AddDateFilter(List<ComparisonFilter> filters, object value)
		{
			DateTime date = (DateTime)value;
			filters.Add(new ComparisonFilter(ComparisonOperator.Equal, Schema.Utilities.GetSchemaPropertyDefinition(this.definition), Schema.Utilities.ToQueryDate(date)));
			if (this.optionalDefinitions.Length > 0)
			{
				filters.Add(new ComparisonFilter(ComparisonOperator.Equal, Schema.Utilities.GetSchemaPropertyDefinition(this.optionalDefinitions[0]), Schema.Utilities.ToQueryHour(date)));
			}
		}

		// Token: 0x04001BA7 RID: 7079
		private readonly string definition;

		// Token: 0x04001BA8 RID: 7080
		private string[] optionalDefinitions;

		// Token: 0x020003D6 RID: 982
		internal static class Methods
		{
			// Token: 0x04001BAA RID: 7082
			internal const string AddDate = "AddDateFilter";

			// Token: 0x04001BAB RID: 7083
			internal const string AddGuid = "AddGuidFilter";
		}

		// Token: 0x020003D7 RID: 983
		internal sealed class Ids
		{
			// Token: 0x04001BAC RID: 7084
			internal const string Action = "ActionListQueryDefinition";

			// Token: 0x04001BAD RID: 7085
			internal const string Actor = "SenderAddressListQueryDefinition";

			// Token: 0x04001BAE RID: 7086
			internal const string AggregateBy = "AggregateByQueryDefinition";

			// Token: 0x04001BAF RID: 7087
			internal const string ClientMessageId = "ClientMessageIdQueryDefinition";

			// Token: 0x04001BB0 RID: 7088
			internal const string Direction = "DirectionListQueryDefinition";

			// Token: 0x04001BB1 RID: 7089
			internal const string Domain = "DomainListQueryDefinition";

			// Token: 0x04001BB2 RID: 7090
			internal const string EndDate = "EndDateQueryDefinition";

			// Token: 0x04001BB3 RID: 7091
			internal const string EndDateKey = "EndDateKeyQueryDefinition";

			// Token: 0x04001BB4 RID: 7092
			internal const string EndHourKey = "EndHourKeyQueryDefinition";

			// Token: 0x04001BB5 RID: 7093
			internal const string Event = "EventListQueryDefinition";

			// Token: 0x04001BB6 RID: 7094
			internal const string EventType = "EventTypeListQueryDefinition";

			// Token: 0x04001BB7 RID: 7095
			internal const string Source = "DataSourceListQueryDefinition";

			// Token: 0x04001BB8 RID: 7096
			internal const string FromIP = "FromIPAddressQueryDefinition";

			// Token: 0x04001BB9 RID: 7097
			internal const string InternalMessageId = "InternalMessageIdQueryDefinition";

			// Token: 0x04001BBA RID: 7098
			internal const string MalwareName = "MalwareListQueryDefinition";

			// Token: 0x04001BBB RID: 7099
			internal const string MessageId = "MessageIdListQueryDefinition";

			// Token: 0x04001BBC RID: 7100
			internal const string Organization = "OrganizationQueryDefinition";

			// Token: 0x04001BBD RID: 7101
			internal const string Page = "PageQueryDefinition";

			// Token: 0x04001BBE RID: 7102
			internal const string PageSize = "PageSizeQueryDefinition";

			// Token: 0x04001BBF RID: 7103
			internal const string PolicyName = "PolicyListQueryDefinition";

			// Token: 0x04001BC0 RID: 7104
			internal const string RecipientAddress = "RecipientAddressQueryDefinition";

			// Token: 0x04001BC1 RID: 7105
			internal const string RecipientAddressList = "RecipientAddressListQueryDefinition";

			// Token: 0x04001BC2 RID: 7106
			internal const string RuleName = "RuleListQueryDefinition";

			// Token: 0x04001BC3 RID: 7107
			internal const string SenderAddress = "SenderAddressQueryDefinition";

			// Token: 0x04001BC4 RID: 7108
			internal const string SenderAddressList = "SenderAddressListQueryDefinition";

			// Token: 0x04001BC5 RID: 7109
			internal const string SummarizeBy = "SummarizeByQueryDefinition";

			// Token: 0x04001BC6 RID: 7110
			internal const string StartDate = "StartDateQueryDefinition";

			// Token: 0x04001BC7 RID: 7111
			internal const string StartDateKey = "StartDateKeyQueryDefinition";

			// Token: 0x04001BC8 RID: 7112
			internal const string StartHourKey = "StartHourKeyQueryDefinition";

			// Token: 0x04001BC9 RID: 7113
			internal const string Status = "MailDeliveryStatusListDefinition";

			// Token: 0x04001BCA RID: 7114
			internal const string ToIP = "ToIPAddressQueryDefinition";

			// Token: 0x04001BCB RID: 7115
			internal const string TransportRuleName = "TransportRuleListQueryDefinition";
		}
	}
}
