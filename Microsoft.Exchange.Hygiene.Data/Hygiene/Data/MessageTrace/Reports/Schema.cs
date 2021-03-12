using System;
using System.Data;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;

namespace Microsoft.Exchange.Hygiene.Data.MessageTrace.Reports
{
	// Token: 0x02000190 RID: 400
	internal class Schema : ConfigurablePropertyBag
	{
		// Token: 0x170004B3 RID: 1203
		// (get) Token: 0x06001035 RID: 4149 RVA: 0x00032DFC File Offset: 0x00030FFC
		public override ObjectId Identity
		{
			get
			{
				return new ConfigObjectId(Guid.NewGuid().ToString());
			}
		}

		// Token: 0x0400079F RID: 1951
		internal static readonly HygienePropertyDefinition ActionListQueryDefinition = new HygienePropertyDefinition("tvp_ActionList", typeof(DataTable));

		// Token: 0x040007A0 RID: 1952
		internal static readonly HygienePropertyDefinition SummarizeByQueryDefinition = new HygienePropertyDefinition("tvp_SummarizeBy", typeof(DataTable));

		// Token: 0x040007A1 RID: 1953
		internal static readonly HygienePropertyDefinition AggregateByQueryDefinition = new HygienePropertyDefinition("AggregateBy", typeof(string), string.Empty, ADPropertyDefinitionFlags.PersistDefaultValue);

		// Token: 0x040007A2 RID: 1954
		internal static readonly HygienePropertyDefinition ClientMessageIdQueryDefinition = new HygienePropertyDefinition("ClientMessageId", typeof(string));

		// Token: 0x040007A3 RID: 1955
		internal static readonly HygienePropertyDefinition DirectionListQueryDefinition = new HygienePropertyDefinition("tvp_MailDirectionList", typeof(DataTable));

		// Token: 0x040007A4 RID: 1956
		internal static readonly HygienePropertyDefinition DomainListQueryDefinition = new HygienePropertyDefinition("tvp_DomainList", typeof(DataTable));

		// Token: 0x040007A5 RID: 1957
		internal static readonly HygienePropertyDefinition DataSourceListQueryDefinition = new HygienePropertyDefinition("tvp_DataSourceList", typeof(DataTable));

		// Token: 0x040007A6 RID: 1958
		internal static readonly HygienePropertyDefinition EndDateQueryDefinition = new HygienePropertyDefinition("EndDate", typeof(DateTime), DateTime.MaxValue, ADPropertyDefinitionFlags.PersistDefaultValue);

		// Token: 0x040007A7 RID: 1959
		internal static readonly HygienePropertyDefinition EndDateKeyQueryDefinition = new HygienePropertyDefinition("EndDateKey", typeof(int), int.MaxValue, ADPropertyDefinitionFlags.PersistDefaultValue);

		// Token: 0x040007A8 RID: 1960
		internal static readonly HygienePropertyDefinition EndDatetimeQueryDefinition = new HygienePropertyDefinition("EndDatetime", typeof(DateTime?));

		// Token: 0x040007A9 RID: 1961
		internal static readonly HygienePropertyDefinition EndHourKeyQueryDefinition = new HygienePropertyDefinition("EndHourKey", typeof(int), 0, ADPropertyDefinitionFlags.PersistDefaultValue);

		// Token: 0x040007AA RID: 1962
		internal static readonly HygienePropertyDefinition EventListQueryDefinition = new HygienePropertyDefinition("tvp_EventList", typeof(DataTable));

		// Token: 0x040007AB RID: 1963
		internal static readonly HygienePropertyDefinition EventTypeListQueryDefinition = new HygienePropertyDefinition("tvp_EventTypeList", typeof(DataTable));

		// Token: 0x040007AC RID: 1964
		internal static readonly HygienePropertyDefinition FromIPAddressQueryDefinition = new HygienePropertyDefinition("FromIPAddress", typeof(string), string.Empty, ADPropertyDefinitionFlags.PersistDefaultValue);

		// Token: 0x040007AD RID: 1965
		internal static readonly HygienePropertyDefinition InternalMessageIdQueryDefinition = new HygienePropertyDefinition("InternalMessageId", typeof(Guid), Guid.Empty, ADPropertyDefinitionFlags.PersistDefaultValue);

		// Token: 0x040007AE RID: 1966
		internal static readonly HygienePropertyDefinition MalwareListQueryDefinition = new HygienePropertyDefinition("tvp_MalwareList", typeof(DataTable));

		// Token: 0x040007AF RID: 1967
		internal static readonly HygienePropertyDefinition MessageIdListQueryDefinition = new HygienePropertyDefinition("tvp_ClientMessageIdList", typeof(DataTable));

		// Token: 0x040007B0 RID: 1968
		internal static readonly HygienePropertyDefinition OrganizationQueryDefinition = new HygienePropertyDefinition("organizationalUnitRoot", typeof(Guid), Guid.Empty, ADPropertyDefinitionFlags.PersistDefaultValue);

		// Token: 0x040007B1 RID: 1969
		internal static readonly HygienePropertyDefinition PageQueryDefinition = new HygienePropertyDefinition("PageNumber", typeof(int), 1, ADPropertyDefinitionFlags.PersistDefaultValue);

		// Token: 0x040007B2 RID: 1970
		internal static readonly HygienePropertyDefinition PageSizeQueryDefinition = new HygienePropertyDefinition("PageSize", typeof(int), 1000, ADPropertyDefinitionFlags.PersistDefaultValue);

		// Token: 0x040007B3 RID: 1971
		internal static readonly HygienePropertyDefinition PolicyListQueryDefinition = new HygienePropertyDefinition("tvp_PolicyList", typeof(DataTable));

		// Token: 0x040007B4 RID: 1972
		internal static readonly HygienePropertyDefinition RecipientAddressQueryDefinition = new HygienePropertyDefinition("RecipientAddress", typeof(string));

		// Token: 0x040007B5 RID: 1973
		internal static readonly HygienePropertyDefinition RecipientAddressListQueryDefinition = new HygienePropertyDefinition("tvp_RecipientAddressList", typeof(DataTable));

		// Token: 0x040007B6 RID: 1974
		internal static readonly HygienePropertyDefinition RuleListQueryDefinition = new HygienePropertyDefinition("tvp_RuleList", typeof(DataTable));

		// Token: 0x040007B7 RID: 1975
		internal static readonly HygienePropertyDefinition SenderAddressQueryDefinition = new HygienePropertyDefinition("SenderAddress", typeof(string));

		// Token: 0x040007B8 RID: 1976
		internal static readonly HygienePropertyDefinition SenderAddressListQueryDefinition = new HygienePropertyDefinition("tvp_SenderAddressList", typeof(DataTable));

		// Token: 0x040007B9 RID: 1977
		internal static readonly HygienePropertyDefinition StartDateQueryDefinition = new HygienePropertyDefinition("StartDate", typeof(DateTime), DateTime.MinValue, ADPropertyDefinitionFlags.PersistDefaultValue);

		// Token: 0x040007BA RID: 1978
		internal static readonly HygienePropertyDefinition StartDateKeyQueryDefinition = new HygienePropertyDefinition("StartDateKey", typeof(int), int.MinValue, ADPropertyDefinitionFlags.PersistDefaultValue);

		// Token: 0x040007BB RID: 1979
		internal static readonly HygienePropertyDefinition StartDatetimeQueryDefinition = new HygienePropertyDefinition("StartDatetime", typeof(DateTime?));

		// Token: 0x040007BC RID: 1980
		internal static readonly HygienePropertyDefinition StartHourKeyQueryDefinition = new HygienePropertyDefinition("StartHourKey", typeof(int), 0, ADPropertyDefinitionFlags.PersistDefaultValue);

		// Token: 0x040007BD RID: 1981
		internal static readonly HygienePropertyDefinition ToIPAddressQueryDefinition = new HygienePropertyDefinition("ToIPAddress", typeof(string), string.Empty, ADPropertyDefinitionFlags.PersistDefaultValue);

		// Token: 0x040007BE RID: 1982
		internal static readonly HygienePropertyDefinition MailDeliveryStatusListDefinition = new HygienePropertyDefinition("tvp_MailDeliveryStatusList", typeof(DataTable));

		// Token: 0x040007BF RID: 1983
		internal static readonly HygienePropertyDefinition TransportRuleListQueryDefinition = new HygienePropertyDefinition("tvp_TransportRuleList", typeof(DataTable));
	}
}
