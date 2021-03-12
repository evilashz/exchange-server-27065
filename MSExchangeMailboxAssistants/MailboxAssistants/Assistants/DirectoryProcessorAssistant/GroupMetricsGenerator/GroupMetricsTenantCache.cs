using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.InfoWorker.EventLog;
using Microsoft.Exchange.UM.UMCommon;

namespace Microsoft.Exchange.MailboxAssistants.Assistants.DirectoryProcessorAssistant.GroupMetricsGenerator
{
	// Token: 0x020001A7 RID: 423
	internal class GroupMetricsTenantCache
	{
		// Token: 0x060010A7 RID: 4263 RVA: 0x00061ACC File Offset: 0x0005FCCC
		private GroupMetricsTenantCache()
		{
			this.syncTenantsNeedingGroupMetrics = new Dictionary<Guid, Dictionary<Guid, OrganizationId>>();
		}

		// Token: 0x060010A8 RID: 4264 RVA: 0x00061AEC File Offset: 0x0005FCEC
		public List<DirectoryProcessorMailboxData> GetMailboxesNeedingGroupMetrics(Guid databaseGuid)
		{
			GroupMetricsTenantCache.Tracer.TraceDebug<Guid>((long)this.GetHashCode(), "GroupMetricsTenantCache.GetOrganizationsNeedingGroupMetrics for database {0}", databaseGuid);
			List<DirectoryProcessorMailboxData> list = new List<DirectoryProcessorMailboxData>(10);
			lock (this.instanceLock)
			{
				if (this.syncTenantsNeedingGroupMetrics.ContainsKey(databaseGuid))
				{
					Dictionary<Guid, OrganizationId> dictionary = this.syncTenantsNeedingGroupMetrics[databaseGuid];
					foreach (KeyValuePair<Guid, OrganizationId> keyValuePair in dictionary)
					{
						list.Add(new DirectoryProcessorMailboxData(keyValuePair.Value, databaseGuid, keyValuePair.Key));
					}
				}
			}
			GroupMetricsUtility.Mailboxes = list;
			return list;
		}

		// Token: 0x060010A9 RID: 4265 RVA: 0x00061BBC File Offset: 0x0005FDBC
		public void Update(Guid databaseGuid)
		{
			GroupMetricsTenantCache.Tracer.TraceDebug<Guid>((long)this.GetHashCode(), "GroupMetricsTenantCache.Update for database {0}", databaseGuid);
			List<ADUser> allSystemMailboxes = this.GetAllSystemMailboxes(databaseGuid);
			Dictionary<Guid, OrganizationId> tenantsNeedingGroupMetrics = this.GetTenantsNeedingGroupMetrics(allSystemMailboxes);
			lock (this.instanceLock)
			{
				this.syncTenantsNeedingGroupMetrics[databaseGuid] = tenantsNeedingGroupMetrics;
			}
		}

		// Token: 0x060010AA RID: 4266 RVA: 0x00061C2C File Offset: 0x0005FE2C
		private Dictionary<Guid, OrganizationId> GetTenantsNeedingGroupMetrics(List<ADUser> systemMailboxes)
		{
			GroupMetricsTenantCache.Tracer.TraceDebug((long)this.GetHashCode(), "DatacenterUMGrammarTenantCache.GetTenantsNeedingGroupMetrics");
			Dictionary<Guid, OrganizationId> dictionary = new Dictionary<Guid, OrganizationId>();
			foreach (ADUser aduser in systemMailboxes)
			{
				if (!GroupMetricsUtility.GroupMetricsEnabledByOrgConfig(aduser.OrganizationId))
				{
					GroupMetricsTenantCache.Tracer.TraceDebug<Guid, OrganizationId>((long)this.GetHashCode(), "Skipping Mbox='{0}', Organization='{1}' because OrgConfig disables this org's Group Metrics Generation. ", aduser.ExchangeGuid, aduser.OrganizationId);
				}
				else if (!dictionary.ContainsKey(aduser.ExchangeGuid))
				{
					GroupMetricsTenantCache.Tracer.TraceDebug<Guid, OrganizationId>((long)this.GetHashCode(), "Adding Mbox='{0}', Organization='{1}'", aduser.ExchangeGuid, aduser.OrganizationId);
					dictionary.Add(aduser.ExchangeGuid, aduser.OrganizationId);
				}
				else
				{
					GroupMetricsTenantCache.Tracer.TraceDebug<Guid, OrganizationId>((long)this.GetHashCode(), "Skipping Mbox='{0}', Organization='{1}' because Mbox is already included. ", aduser.ExchangeGuid, aduser.OrganizationId);
				}
			}
			if (systemMailboxes.Count > 0 && GroupMetricsUtility.LocalServerMustGenerateGroupMetrics() && GroupMetricsUtility.GroupMetricsEnabledByOrgConfig(OrganizationId.ForestWideOrgId) && !dictionary.ContainsValue(OrganizationId.ForestWideOrgId))
			{
				GroupMetricsTenantCache.Tracer.TraceDebug(0L, "Group metrics generation enabled for First Org by local server config");
				dictionary.Add(Guid.NewGuid(), OrganizationId.ForestWideOrgId);
			}
			return dictionary;
		}

		// Token: 0x060010AB RID: 4267 RVA: 0x00061DA0 File Offset: 0x0005FFA0
		private List<ADUser> GetAllSystemMailboxes(Guid databaseGuid)
		{
			GroupMetricsTenantCache.Tracer.TraceDebug<Guid>((long)this.GetHashCode(), "GroupMetricsTenantCache.GetAllSystemMailboxes for database {0}", databaseGuid);
			List<ADUser> systemMailboxes = new List<ADUser>();
			Exception ex = Utilities.RunSafeADOperation(GroupMetricsTenantCache.Tracer, delegate
			{
				systemMailboxes.AddRange(OrganizationMailbox.FindByDatabaseId(OrganizationCapability.GMGen, new ADObjectId(databaseGuid)));
			}, "GetAllSystemMailboxes: Getting all system mailboxes in a given database");
			if (ex != null)
			{
				GroupMetricsTenantCache.Tracer.TraceDebug<Guid, Exception>((long)this.GetHashCode(), "GetAllSystemMailboxes: Failed in AD operation for database '{0}'. Error='{1}'", databaseGuid, ex);
				GroupMetricsTenantCache.EventLogger.LogEvent(InfoWorkerEventLogConstants.Tuple_GroupMetricsGenerationCouldntFindSystemMailbox, null, new object[]
				{
					databaseGuid.ToString(),
					CommonUtil.ToEventLogString(ex)
				});
			}
			return systemMailboxes;
		}

		// Token: 0x04000A78 RID: 2680
		private static readonly Trace Tracer = GroupMetricsUtility.Tracer;

		// Token: 0x04000A79 RID: 2681
		private static readonly ExEventLog EventLogger = GroupMetricsUtility.Logger;

		// Token: 0x04000A7A RID: 2682
		private object instanceLock = new object();

		// Token: 0x04000A7B RID: 2683
		private Dictionary<Guid, Dictionary<Guid, OrganizationId>> syncTenantsNeedingGroupMetrics;

		// Token: 0x04000A7C RID: 2684
		public static GroupMetricsTenantCache Instance = new GroupMetricsTenantCache();
	}
}
