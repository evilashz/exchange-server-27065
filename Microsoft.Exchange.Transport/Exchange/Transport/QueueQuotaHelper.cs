using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data.Metering;
using Microsoft.Exchange.Data.Metering.Throttling;
using Microsoft.Exchange.Data.Transport;
using Microsoft.Exchange.Transport.LoggingCommon;

namespace Microsoft.Exchange.Transport
{
	// Token: 0x02000352 RID: 850
	internal static class QueueQuotaHelper
	{
		// Token: 0x060024C3 RID: 9411 RVA: 0x0008D570 File Offset: 0x0008B770
		internal static ICountedEntity<MeteredEntity> CreateEntity(string accountForest)
		{
			return new CountedEntity<MeteredEntity>(new SimpleEntityName<MeteredEntity>(MeteredEntity.AccountForest, accountForest), new SimpleEntityName<MeteredEntity>(MeteredEntity.AccountForest, accountForest));
		}

		// Token: 0x060024C4 RID: 9412 RVA: 0x0008D587 File Offset: 0x0008B787
		internal static ICountedEntity<MeteredEntity> CreateEntity(Guid orgId)
		{
			return new CountedEntity<MeteredEntity>(new SimpleEntityName<MeteredEntity>(MeteredEntity.Tenant, orgId.ToString()), new SimpleEntityName<MeteredEntity>(MeteredEntity.Tenant, orgId.ToString()));
		}

		// Token: 0x060024C5 RID: 9413 RVA: 0x0008D5B6 File Offset: 0x0008B7B6
		internal static ICountedEntity<MeteredEntity> CreateEntity(Guid orgId, string sender)
		{
			return new CountedEntity<MeteredEntity>(new SimpleEntityName<MeteredEntity>(MeteredEntity.Tenant, orgId.ToString()), new SimpleEntityName<MeteredEntity>(MeteredEntity.Sender, sender));
		}

		// Token: 0x060024C6 RID: 9414 RVA: 0x0008D5D8 File Offset: 0x0008B7D8
		internal static QueueQuotaEntity GetQueueQuotaEntity(MeteredEntity meteredEntity)
		{
			if (meteredEntity == MeteredEntity.Sender)
			{
				return QueueQuotaEntity.Sender;
			}
			if (meteredEntity == MeteredEntity.Tenant)
			{
				return QueueQuotaEntity.Organization;
			}
			throw new InvalidOperationException(string.Format("Got an unexpected entity back: {0}", meteredEntity));
		}

		// Token: 0x060024C7 RID: 9415 RVA: 0x0008D60C File Offset: 0x0008B80C
		internal static QueueQuotaResources GetResource(MeteredCount measure)
		{
			switch (measure)
			{
			case MeteredCount.AllQueue:
				return QueueQuotaResources.All;
			case MeteredCount.AcceptedSubmissionQueue:
				return QueueQuotaResources.SubmissionQueueSize;
			case MeteredCount.AcceptedTotalQueue:
				return QueueQuotaResources.TotalQueueSize;
			default:
				throw new InvalidOperationException(string.Format("Returned a measure that was not requested: {0}", measure));
			}
		}

		// Token: 0x060024C8 RID: 9416 RVA: 0x0008D650 File Offset: 0x0008B850
		internal static IEnumerable<MeteredCount> GetMeteredCount(QueueQuotaResources resource)
		{
			List<MeteredCount> list = new List<MeteredCount>();
			if ((byte)(resource & QueueQuotaResources.SubmissionQueueSize) != 0)
			{
				list.Add(MeteredCount.AcceptedSubmissionQueue);
			}
			if ((byte)(resource & QueueQuotaResources.TotalQueueSize) != 0)
			{
				list.Add(MeteredCount.AcceptedTotalQueue);
			}
			return list;
		}

		// Token: 0x060024C9 RID: 9417 RVA: 0x0008D680 File Offset: 0x0008B880
		internal static MeteredCount GetRejectedMeter(MeteredCount measure)
		{
			switch (measure)
			{
			case MeteredCount.AcceptedSubmissionQueue:
				return MeteredCount.CurrentRejectedSubmissionQueue;
			case MeteredCount.AcceptedTotalQueue:
				return MeteredCount.CurrentRejectedTotalQueue;
			case MeteredCount.CurrentRejectedSubmissionQueue:
				return MeteredCount.RejectedSubmissionQueue;
			case MeteredCount.CurrentRejectedTotalQueue:
				return MeteredCount.RejectedTotalQueue;
			default:
				throw new InvalidOperationException(string.Format("Unexpected measure: {0}", measure));
			}
		}

		// Token: 0x060024CA RID: 9418 RVA: 0x0008D6C8 File Offset: 0x0008B8C8
		internal static MeteredCount GetRejectedMeter(QueueQuotaResources resource)
		{
			switch (resource)
			{
			case QueueQuotaResources.SubmissionQueueSize:
				return MeteredCount.CurrentRejectedSubmissionQueue;
			case QueueQuotaResources.TotalQueueSize:
				return MeteredCount.CurrentRejectedTotalQueue;
			default:
				throw new InvalidOperationException(string.Format("Unexpected resouce: {0}", resource));
			}
		}

		// Token: 0x060024CB RID: 9419 RVA: 0x0008D704 File Offset: 0x0008B904
		internal static MeteredCount[] GetAllRejectedMeters(QueueQuotaResources resource)
		{
			MeteredCount rejectedMeter = QueueQuotaHelper.GetRejectedMeter(resource);
			return new MeteredCount[]
			{
				rejectedMeter,
				QueueQuotaHelper.GetRejectedMeter(rejectedMeter)
			};
		}

		// Token: 0x060024CC RID: 9420 RVA: 0x0008D72D File Offset: 0x0008B92D
		internal static string GetSender(QueueQuotaEntity entityType, string entityId)
		{
			if (entityType != QueueQuotaEntity.Sender)
			{
				return null;
			}
			return entityId;
		}

		// Token: 0x060024CD RID: 9421 RVA: 0x0008D738 File Offset: 0x0008B938
		internal static string GetRedactedSender(QueueQuotaEntity entityType, string entityId)
		{
			string result = null;
			if (QueueQuotaHelper.GetSender(entityType, entityId) != null)
			{
				if (RoutingAddress.IsValidAddress(entityId))
				{
					result = SuppressingPiiData.Redact(RoutingAddress.Parse(entityId)).ToString();
				}
				else
				{
					result = SuppressingPiiData.Redact(entityId);
				}
			}
			return result;
		}

		// Token: 0x060024CE RID: 9422 RVA: 0x0008D77C File Offset: 0x0008B97C
		internal static Guid GetOrgId(QueueQuotaEntity entityType, Guid externalOrganizationId)
		{
			if (entityType == QueueQuotaEntity.Organization || entityType == QueueQuotaEntity.Sender)
			{
				return externalOrganizationId;
			}
			return Guid.Empty;
		}

		// Token: 0x060024CF RID: 9423 RVA: 0x0008D78C File Offset: 0x0008B98C
		internal static ThrottlingScope GetThrottlingScope(QueueQuotaEntity entityType)
		{
			switch (entityType)
			{
			case QueueQuotaEntity.Organization:
				return ThrottlingScope.Tenant;
			case QueueQuotaEntity.Sender:
				return ThrottlingScope.Sender;
			case QueueQuotaEntity.AccountForest:
				return ThrottlingScope.AccountForest;
			default:
				throw new InvalidOperationException(string.Format("Unexpected QueueQuotaEntity found: {0}", entityType));
			}
		}

		// Token: 0x060024D0 RID: 9424 RVA: 0x0008D7CC File Offset: 0x0008B9CC
		internal static ThrottlingResource GetThrottlingResource(QueueQuotaResources resource)
		{
			switch (resource)
			{
			case QueueQuotaResources.SubmissionQueueSize:
				return ThrottlingResource.SubmissionQueueQuota;
			case QueueQuotaResources.TotalQueueSize:
				return ThrottlingResource.TotalQueueQuota;
			default:
				throw new InvalidOperationException(string.Format("Unexpected QueueQuotaResources found: {0}", resource));
			}
		}

		// Token: 0x060024D1 RID: 9425 RVA: 0x0008D808 File Offset: 0x0008BA08
		internal static long GetSum(IEnumerable<ICount<MeteredEntity, MeteredCount>> counts)
		{
			long num = 0L;
			foreach (ICount<MeteredEntity, MeteredCount> count in counts)
			{
				num += count.Total;
			}
			return num;
		}

		// Token: 0x060024D2 RID: 9426 RVA: 0x0008D86D File Offset: 0x0008BA6D
		internal static bool IsAnyThrottled(IDictionary<MeteredCount, ICount<MeteredEntity, MeteredCount>> counts)
		{
			return counts.Values.Any(delegate(ICount<MeteredEntity, MeteredCount> c)
			{
				DateTime dateTime;
				return QueueQuotaHelper.HasThrottledTime(c, out dateTime);
			});
		}

		// Token: 0x060024D3 RID: 9427 RVA: 0x0008D898 File Offset: 0x0008BA98
		internal static bool HasThrottledTime(ICount<MeteredEntity, MeteredCount> count, out DateTime throttledTime)
		{
			object obj;
			if (count.TryGetObject("ThrottledStartTime", out obj) && obj is DateTime)
			{
				throttledTime = (DateTime)obj;
				return true;
			}
			throttledTime = DateTime.MaxValue;
			return false;
		}

		// Token: 0x060024D4 RID: 9428 RVA: 0x0008D8D8 File Offset: 0x0008BAD8
		internal static bool IsWarningLogged(ICount<MeteredEntity, MeteredCount> count)
		{
			object obj;
			return count.TryGetObject("WarningLogged", out obj) && obj is bool && (bool)obj;
		}

		// Token: 0x060024D5 RID: 9429 RVA: 0x0008D904 File Offset: 0x0008BB04
		internal static bool ShouldProcessEntity(ICountedEntity<MeteredEntity> entity)
		{
			return QueueQuotaHelper.IsOrg(entity) || QueueQuotaHelper.IsSender(entity) || QueueQuotaHelper.IsAccountForest(entity);
		}

		// Token: 0x060024D6 RID: 9430 RVA: 0x0008D921 File Offset: 0x0008BB21
		internal static bool IsOrg(ICountedEntity<MeteredEntity> entity, out Guid orgId)
		{
			orgId = Guid.Empty;
			if (!QueueQuotaHelper.IsOrg(entity))
			{
				return false;
			}
			Guid.TryParse(entity.Name.Value, out orgId);
			return true;
		}

		// Token: 0x060024D7 RID: 9431 RVA: 0x0008D94B File Offset: 0x0008BB4B
		internal static bool IsOrg(ICountedEntity<MeteredEntity> entity)
		{
			return entity.Name.Type == MeteredEntity.Tenant;
		}

		// Token: 0x060024D8 RID: 9432 RVA: 0x0008D95C File Offset: 0x0008BB5C
		internal static bool IsSender(ICountedEntity<MeteredEntity> entity, out Guid orgId)
		{
			orgId = Guid.Empty;
			if (!QueueQuotaHelper.IsSender(entity))
			{
				return false;
			}
			Guid.TryParse(entity.GroupName.Value, out orgId);
			return true;
		}

		// Token: 0x060024D9 RID: 9433 RVA: 0x0008D986 File Offset: 0x0008BB86
		internal static bool IsSender(ICountedEntity<MeteredEntity> entity)
		{
			return entity.Name.Type == MeteredEntity.Sender;
		}

		// Token: 0x060024DA RID: 9434 RVA: 0x0008D996 File Offset: 0x0008BB96
		internal static bool IsAccountForest(ICountedEntity<MeteredEntity> entity)
		{
			return entity.Name.Type == MeteredEntity.AccountForest;
		}

		// Token: 0x060024DB RID: 9435 RVA: 0x0008D9A7 File Offset: 0x0008BBA7
		internal static bool IsQueueQuotaAcceptedCount(ICount<MeteredEntity, MeteredCount> count)
		{
			return (count.Entity.Name.Type == MeteredEntity.Tenant || count.Entity.Name.Type == MeteredEntity.Sender) && QueueQuotaHelper.AllAcceptedCounts.Contains(count.Measure);
		}

		// Token: 0x060024DC RID: 9436 RVA: 0x0008D9E4 File Offset: 0x0008BBE4
		internal static int GetResourceCapacity(QueueQuotaResources resource, IQueueQuotaConfig config)
		{
			switch (resource)
			{
			case QueueQuotaResources.SubmissionQueueSize:
				return config.SubmissionQueueCapacity;
			case QueueQuotaResources.TotalQueueSize:
				return config.TotalQueueCapacity;
			default:
				throw new ArgumentOutOfRangeException("resource");
			}
		}

		// Token: 0x04001318 RID: 4888
		internal const string AvailableCapacityProperty = "AvailableCapacity";

		// Token: 0x04001319 RID: 4889
		internal const string ThrottledStartTimeProperty = "ThrottledStartTime";

		// Token: 0x0400131A RID: 4890
		internal const string WarningLoggedProperty = "WarningLogged";

		// Token: 0x0400131B RID: 4891
		internal static readonly QueueQuotaResources[] AllResources = new QueueQuotaResources[]
		{
			QueueQuotaResources.SubmissionQueueSize,
			QueueQuotaResources.TotalQueueSize
		};

		// Token: 0x0400131C RID: 4892
		internal static readonly MeteredCount[] AllAcceptedCounts = new MeteredCount[]
		{
			MeteredCount.AcceptedSubmissionQueue,
			MeteredCount.AcceptedTotalQueue
		};
	}
}
