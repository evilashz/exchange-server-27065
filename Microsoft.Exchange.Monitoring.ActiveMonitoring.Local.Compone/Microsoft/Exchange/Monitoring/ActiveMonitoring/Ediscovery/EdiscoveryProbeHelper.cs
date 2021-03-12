using System;
using System.Collections.Generic;
using System.Globalization;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Search.KqlParser;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.InfoWorker.Common.MultiMailboxSearch;
using Microsoft.Exchange.Monitoring.ActiveMonitoring.Common;
using Microsoft.Exchange.Monitoring.ActiveMonitoring.Search;
using Microsoft.Exchange.Rpc.MultiMailboxSearch;
using Microsoft.Mapi;
using Microsoft.Office.Datacenter.ActiveMonitoring;

namespace Microsoft.Exchange.Monitoring.ActiveMonitoring.Ediscovery
{
	// Token: 0x02000169 RID: 361
	internal static class EdiscoveryProbeHelper
	{
		// Token: 0x06000A6D RID: 2669 RVA: 0x00041860 File Offset: 0x0003FA60
		public static ExRpcAdmin CreateRPCAdminConnection(string mailboxDatabaseServerFQDN, Guid mailboxDatabaseGuid)
		{
			ExRpcAdmin exRpcAdmin = ExRpcAdmin.Create("Client=EDiscoverySearch", mailboxDatabaseServerFQDN, null, null, null);
			if (exRpcAdmin == null)
			{
				throw new SearchTransientException(SearchType.Statistics, new DiscoverySearchFailed(mailboxDatabaseGuid, -1));
			}
			return exRpcAdmin;
		}

		// Token: 0x06000A6E RID: 2670 RVA: 0x00041890 File Offset: 0x0003FA90
		public static List<KeyValuePair<string, byte[]>> ConvertQueryFilterToRestriction(string query, ExRpcAdmin rpcAdmin, string account)
		{
			if (query == null || query.Length == 0)
			{
				return new List<KeyValuePair<string, byte[]>>(0);
			}
			List<KeyValuePair<string, byte[]>> list = new List<KeyValuePair<string, byte[]>>(1);
			QueryFilter queryFilter = KqlParser.ParseAndBuildQuery(query, KqlParser.ParseOption.ImplicitOr | KqlParser.ParseOption.UseCiKeywordOnly | KqlParser.ParseOption.DisablePrefixMatch, CultureInfo.CurrentCulture, null, null);
			using (MailboxSession mailboxSession = SearchStoreHelper.GetMailboxSession(account, false, "Monitoring"))
			{
				list.Add(new KeyValuePair<string, byte[]>(query, EdiscoveryProbeHelper.CreateRestriction(mailboxSession, rpcAdmin, queryFilter)));
			}
			list.TrimExcess();
			return list;
		}

		// Token: 0x06000A6F RID: 2671 RVA: 0x0004190C File Offset: 0x0003FB0C
		private static byte[] CreateRestriction(MailboxSession session, ExRpcAdmin rpcAdmin, QueryFilter queryFilter)
		{
			Restriction restriction = FilterRestrictionConverter.CreateRestriction(session, ExTimeZone.UtcTimeZone, session.Mailbox.MapiStore, queryFilter);
			return rpcAdmin.SerializeAndFormatRestriction(restriction);
		}

		// Token: 0x06000A70 RID: 2672 RVA: 0x00041938 File Offset: 0x0003FB38
		public static bool IsSearchResponseValid(byte[] searchResponseByteArray, out MultiMailboxKeywordStatsResult[] searchResponseResult)
		{
			MultiMailboxKeywordStatsResponse multiMailboxKeywordStatsResponse = MultiMailboxKeywordStatsResponse.DeSerialize(searchResponseByteArray);
			searchResponseResult = null;
			if (multiMailboxKeywordStatsResponse != null && multiMailboxKeywordStatsResponse.Results != null)
			{
				searchResponseResult = (MultiMailboxKeywordStatsResult[])multiMailboxKeywordStatsResponse.Results;
				return searchResponseResult[0].Count == 0L;
			}
			return false;
		}

		// Token: 0x06000A71 RID: 2673 RVA: 0x00041978 File Offset: 0x0003FB78
		public static ProbeDefinition ConfigureEdiscoveryProbe(this ProbeDefinition definition, MailboxDatabaseInfo dbInfo)
		{
			if (string.IsNullOrEmpty(dbInfo.MonitoringAccountPassword))
			{
				throw new InvalidOperationException("Authentication requires a valid monitoring account password");
			}
			definition.AccountDisplayName = dbInfo.MonitoringAccount;
			definition.Account = dbInfo.MonitoringAccount + "@" + dbInfo.MonitoringAccountDomain;
			definition.AccountPassword = dbInfo.MonitoringAccountPassword;
			definition.RecurrenceIntervalSeconds = (int)EdiscoveryProbeHelper.ProbeInterval.TotalSeconds;
			definition.TimeoutSeconds = (int)EdiscoveryProbeHelper.ProbeTimeout.TotalSeconds;
			definition.TargetResource = dbInfo.MailboxDatabaseName;
			definition.Attributes["MailboxDatabaseGuid"] = dbInfo.MailboxDatabaseGuid.ToString();
			definition.Attributes["MailboxGuid"] = dbInfo.MonitoringAccountMailboxGuid.ToString();
			definition.Attributes["MailboxDatabaseServerFQDN"] = DirectoryAccessor.Instance.GetServerFqdnForDatabase(dbInfo.MailboxDatabaseGuid);
			return definition;
		}

		// Token: 0x04000764 RID: 1892
		public const int NumberOfFailuresThreshold = 4;

		// Token: 0x04000765 RID: 1893
		public const string MailboxDatabaseGuidstr = "MailboxDatabaseGuid";

		// Token: 0x04000766 RID: 1894
		public const string MailboxGuidstr = "MailboxGuid";

		// Token: 0x04000767 RID: 1895
		public const string MailboxDatabaseServerFQDNstr = "MailboxDatabaseServerFQDN";

		// Token: 0x04000768 RID: 1896
		public const string SearchQuery = "{fc697f5b-7943-4f20-a6da-47380bf6cd97}+{768d984f-6fe4-4b79-8b5d-7dc52ee0ca62}";

		// Token: 0x04000769 RID: 1897
		public static readonly TimeSpan MonitoringInterval = TimeSpan.FromHours(2.0);

		// Token: 0x0400076A RID: 1898
		public static readonly TimeSpan MonitoringRecurrenceInterval = TimeSpan.FromMinutes(15.0);

		// Token: 0x0400076B RID: 1899
		public static readonly TimeSpan ProbeInterval = TimeSpan.FromMinutes(15.0);

		// Token: 0x0400076C RID: 1900
		public static readonly TimeSpan ResponderRecurrenceInterval = TimeSpan.FromMinutes(15.0);

		// Token: 0x0400076D RID: 1901
		public static readonly TimeSpan ProbeTimeout = TimeSpan.FromMinutes(1.0);
	}
}
