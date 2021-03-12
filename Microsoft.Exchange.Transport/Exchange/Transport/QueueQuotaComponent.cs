using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data.Metering;
using Microsoft.Exchange.Data.Metering.Throttling;
using Microsoft.Exchange.Data.Transport;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Transport.LoggingCommon;
using Microsoft.Exchange.Transport.QueueQuota;

namespace Microsoft.Exchange.Transport
{
	// Token: 0x0200034A RID: 842
	internal class QueueQuotaComponent : IQueueQuotaComponent, ITransportComponent, IDiagnosable
	{
		// Token: 0x06002446 RID: 9286 RVA: 0x0008A10F File Offset: 0x0008830F
		public QueueQuotaComponent() : this(() => DateTime.UtcNow)
		{
		}

		// Token: 0x06002447 RID: 9287 RVA: 0x0008A134 File Offset: 0x00088334
		public QueueQuotaComponent(Func<DateTime> currentTimeProvider)
		{
			this.currentTimeProvider = currentTimeProvider;
		}

		// Token: 0x06002448 RID: 9288 RVA: 0x0008A1A4 File Offset: 0x000883A4
		public void SetRunTimeDependencies(IQueueQuotaConfig config, IFlowControlLog log, IQueueQuotaComponentPerformanceCounters perfCounters, IProcessingQuotaComponent processingQuotaComponent, IQueueQuotaObservableComponent submissionQueue, IQueueQuotaObservableComponent deliveryQueue, ICountTracker<MeteredEntity, MeteredCount> meteringComponent)
		{
			this.config = config;
			this.flowControlLog = log;
			this.flowControlLog.TrackSummary += this.LogSummary;
			this.perfCounters = perfCounters;
			this.processingQuotaComponent = processingQuotaComponent;
			this.totalData = new UsageData(this.config.TrackSummaryLoggingInterval, this.config.TrackSummaryBucketLength);
			if (submissionQueue != null)
			{
				submissionQueue.OnAcquire += delegate(TransportMailItem tmi)
				{
					this.TrackEnteringQueue(tmi, QueueQuotaResources.SubmissionQueueSize | QueueQuotaResources.TotalQueueSize);
				};
				submissionQueue.OnRelease += delegate(TransportMailItem tmi)
				{
					this.TrackExitingQueue(tmi, QueueQuotaResources.SubmissionQueueSize | QueueQuotaResources.TotalQueueSize);
				};
			}
			if (deliveryQueue != null)
			{
				deliveryQueue.OnAcquire += delegate(TransportMailItem tmi)
				{
					this.TrackEnteringQueue(tmi, QueueQuotaResources.TotalQueueSize);
				};
				deliveryQueue.OnRelease += delegate(TransportMailItem tmi)
				{
					this.TrackExitingQueue(tmi, QueueQuotaResources.TotalQueueSize);
				};
			}
		}

		// Token: 0x06002449 RID: 9289 RVA: 0x0008A278 File Offset: 0x00088478
		public void TrackEnteringQueue(IQueueQuotaMailItem mailItem, QueueQuotaResources resources)
		{
			Guid externalOrganizationId = mailItem.ExternalOrganizationId;
			string originalFromAddress = mailItem.OriginalFromAddress;
			this.totalData.IncrementUsage(resources);
			OrganizationUsageData orAdd = this.orgQuotaDictionary.GetOrAdd(externalOrganizationId, new Func<Guid, OrganizationUsageData>(this.NewOrganizationUsageData));
			orAdd.IncrementUsage(resources);
			mailItem.QueueQuotaTrackingBits[QueueQuotaEntity.Organization, resources] = true;
			UsageData.AddOrMerge<Guid, OrganizationUsageData>(this.orgQuotaDictionary, externalOrganizationId, orAdd);
			if (!string.IsNullOrEmpty(originalFromAddress) && orAdd.GetUsage(resources) > this.config.SenderTrackingThreshold)
			{
				UsageData orAdd2 = orAdd.SenderQuotaDictionary.GetOrAdd(originalFromAddress, new Func<string, UsageData>(this.NewUsageData));
				orAdd2.IncrementUsage(resources);
				mailItem.QueueQuotaTrackingBits[QueueQuotaEntity.Sender, resources] = true;
				UsageData.AddOrMerge<string, UsageData>(orAdd.SenderQuotaDictionary, originalFromAddress, orAdd2);
			}
		}

		// Token: 0x0600244A RID: 9290 RVA: 0x0008A334 File Offset: 0x00088534
		public void TrackExitingQueue(IQueueQuotaMailItem mailItem, QueueQuotaResources resources)
		{
			Guid externalOrganizationId = mailItem.ExternalOrganizationId;
			string originalFromAddress = mailItem.OriginalFromAddress;
			this.totalData.DecrementUsage(resources);
			if (mailItem.QueueQuotaTrackingBits[QueueQuotaEntity.Organization, resources])
			{
				OrganizationUsageData orAdd = this.orgQuotaDictionary.GetOrAdd(externalOrganizationId, new Func<Guid, OrganizationUsageData>(this.NewOrganizationUsageData));
				orAdd.DecrementUsage(resources);
				mailItem.QueueQuotaTrackingBits[QueueQuotaEntity.Organization, resources] = false;
				if (!string.IsNullOrEmpty(originalFromAddress) && mailItem.QueueQuotaTrackingBits[QueueQuotaEntity.Sender, resources])
				{
					UsageData orAdd2 = orAdd.SenderQuotaDictionary.GetOrAdd(originalFromAddress, new Func<string, UsageData>(this.NewUsageData));
					orAdd2.DecrementUsage(resources);
					mailItem.QueueQuotaTrackingBits[QueueQuotaEntity.Sender, resources] = false;
				}
			}
		}

		// Token: 0x0600244B RID: 9291 RVA: 0x0008A3E0 File Offset: 0x000885E0
		public bool IsOrganizationOverQuota(string accountForest, Guid externalOrganizationId, string sender, out string reason)
		{
			QueueQuotaEntity? entity;
			QueueQuotaResources? queueQuotaResources;
			bool flag = this.IsOverQuota(externalOrganizationId, sender, out reason, out entity, out queueQuotaResources);
			if (flag)
			{
				QueueQuotaEntity valueOrDefault = entity.GetValueOrDefault();
				if (entity != null)
				{
					switch (valueOrDefault)
					{
					case QueueQuotaEntity.Organization:
					{
						OrganizationUsageData orAdd = this.orgQuotaDictionary.GetOrAdd(externalOrganizationId, new Func<Guid, OrganizationUsageData>(this.NewOrganizationUsageData));
						orAdd.IncrementRejected((queueQuotaResources != null) ? queueQuotaResources.Value : QueueQuotaResources.All);
						goto IL_EB;
					}
					case QueueQuotaEntity.Sender:
						if (!string.IsNullOrEmpty(sender))
						{
							OrganizationUsageData orAdd = this.orgQuotaDictionary.GetOrAdd(externalOrganizationId, new Func<Guid, OrganizationUsageData>(this.NewOrganizationUsageData));
							UsageData orAdd2 = orAdd.SenderQuotaDictionary.GetOrAdd(sender, new Func<string, UsageData>(this.NewUsageData));
							orAdd2.IncrementRejected(queueQuotaResources.Value);
							goto IL_EB;
						}
						goto IL_EB;
					}
				}
				this.totalData.IncrementRejected(queueQuotaResources.Value);
				int num;
				DateTime dateTime;
				this.totalData.ResetThrottledData(queueQuotaResources.Value, out num, out dateTime);
			}
			IL_EB:
			if (this.config.EnforceQuota && flag)
			{
				this.perfCounters.IncrementMessagesRejected(entity, new Guid?(externalOrganizationId));
				return true;
			}
			return false;
		}

		// Token: 0x0600244C RID: 9292 RVA: 0x0008A500 File Offset: 0x00088700
		public void TimedUpdate()
		{
			this.perfCounters.Refresh(new QueueQuotaEntity?(QueueQuotaEntity.Organization));
			this.perfCounters.Refresh(new QueueQuotaEntity?(QueueQuotaEntity.Sender));
			this.perfCounters.Refresh(null);
		}

		// Token: 0x0600244D RID: 9293 RVA: 0x0008A544 File Offset: 0x00088744
		public bool IsOrganizationOverWarning(string accountForest, Guid externalOrganizationId, string sender, QueueQuotaResources resource)
		{
			OrganizationUsageData organizationUsageData;
			if (!this.orgQuotaDictionary.TryGetValue(externalOrganizationId, out organizationUsageData))
			{
				return false;
			}
			UsageData usageData;
			if (!string.IsNullOrEmpty(sender) && organizationUsageData.SenderQuotaDictionary.TryGetValue(sender, out usageData))
			{
				return usageData.GetOverWarningFlag(resource);
			}
			return organizationUsageData.GetOverWarningFlag(resource);
		}

		// Token: 0x0600244E RID: 9294 RVA: 0x0008A58C File Offset: 0x0008878C
		internal bool IsOverQuota(Guid externalOrganizationId, string sender, out string reason, out QueueQuotaEntity? reasonEntity, out QueueQuotaResources? reasonResource)
		{
			reason = null;
			reasonEntity = null;
			reasonResource = null;
			if (this.IsOrganizationBlocked(externalOrganizationId))
			{
				reason = "Organization is in block list.";
				reasonEntity = new QueueQuotaEntity?(QueueQuotaEntity.Organization);
				return true;
			}
			if (this.IsOrganizationAllowListed(externalOrganizationId))
			{
				reason = "Organization is in allow list.";
				return false;
			}
			foreach (QueueQuotaResources queueQuotaResources in this.allResources)
			{
				if (this.GetUsage(queueQuotaResources) > this.GetResourceCapacity(queueQuotaResources))
				{
					reason = string.Format("Resource {0} beyond capacity. Count:{1} Capacity:{2}", queueQuotaResources, this.GetUsage(queueQuotaResources), this.GetResourceCapacity(queueQuotaResources));
					reasonResource = new QueueQuotaResources?(queueQuotaResources);
					return true;
				}
			}
			OrganizationUsageData organizationUsageData;
			if (this.orgQuotaDictionary.TryGetValue(externalOrganizationId, out organizationUsageData))
			{
				UsageData usageData = null;
				bool flag = !string.IsNullOrEmpty(sender) && organizationUsageData.SenderQuotaDictionary.TryGetValue(sender, out usageData);
				foreach (QueueQuotaResources queueQuotaResources2 in this.allResources)
				{
					if (this.ComputeIsOverQuota(externalOrganizationId, QueueQuotaEntity.Organization, externalOrganizationId.ToString(), organizationUsageData, queueQuotaResources2, this.GetOrganizationQuotaHighMark(externalOrganizationId, queueQuotaResources2), false, out reason))
					{
						reasonEntity = new QueueQuotaEntity?(QueueQuotaEntity.Organization);
						reasonResource = new QueueQuotaResources?(queueQuotaResources2);
						return true;
					}
					if (flag && this.ComputeIsOverQuota(externalOrganizationId, QueueQuotaEntity.Sender, sender, usageData, queueQuotaResources2, this.GetSenderQuotaHighMark(externalOrganizationId, sender, queueQuotaResources2), false, out reason))
					{
						reasonEntity = new QueueQuotaEntity?(QueueQuotaEntity.Sender);
						reasonResource = new QueueQuotaResources?(queueQuotaResources2);
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x0600244F RID: 9295 RVA: 0x0008A72C File Offset: 0x0008892C
		private bool ComputeIsOverQuota(Guid externalOrganizationId, QueueQuotaEntity entityType, string entityId, UsageData usageData, QueueQuotaResources resource, int high, bool onlySetUnthrottle, out string reason)
		{
			reason = null;
			int usage = usageData.GetUsage(resource);
			int num = (int)((double)high * this.config.LowWatermarkRatio);
			int num2 = (int)((double)high * this.config.WarningRatio);
			bool isOverQuota = usageData.GetIsOverQuota(resource);
			bool isOverQuota2 = usageData.GetIsOverQuota(QueueQuotaResources.All);
			bool overWarningFlag = usageData.GetOverWarningFlag(resource);
			bool flag = usage > high || (isOverQuota && usage > num);
			bool flag2 = usage > num2 && !flag;
			if ((!onlySetUnthrottle && ((flag ^ isOverQuota) || (flag2 ^ overWarningFlag))) || (onlySetUnthrottle && !flag && isOverQuota) || (!flag2 && overWarningFlag))
			{
				usageData.SetOverQuotaFlags(resource, flag, flag2);
			}
			if (flag)
			{
				if (onlySetUnthrottle)
				{
					return flag;
				}
				reason = string.Format("{0} is above quota for {1}.Actual:{2} Low:{3} High:{4}", new object[]
				{
					entityType,
					resource,
					usage,
					num,
					high
				});
				if (!isOverQuota)
				{
					this.flowControlLog.LogThrottle(QueueQuotaComponent.GetThrottlingResource(resource), ThrottlingAction.TempReject, high, TimeSpan.Zero, QueueQuotaComponent.GetThrottlingScope(entityType), externalOrganizationId, QueueQuotaComponent.GetRedactedSender(entityType, entityId), null, null, ThrottlingSource.QueueQuota, !this.config.EnforceQuota, new List<KeyValuePair<string, object>>
					{
						new KeyValuePair<string, object>("AvailableCapacity", this.GetAvailableResourceSize(resource)),
						new KeyValuePair<string, object>("observedValue", usage)
					});
					if (!isOverQuota2)
					{
						this.perfCounters.IncrementThrottledEntities(entityType, externalOrganizationId);
					}
				}
			}
			else if (isOverQuota)
			{
				int impact;
				DateTime value;
				usageData.ResetThrottledData(resource, out impact, out value);
				this.flowControlLog.LogUnthrottle(QueueQuotaComponent.GetThrottlingResource(resource), ThrottlingAction.TempReject, num, TimeSpan.Zero, impact, usage, QueueQuotaComponent.GetThrottlingScope(entityType), externalOrganizationId, QueueQuotaComponent.GetRedactedSender(entityType, entityId), null, null, ThrottlingSource.QueueQuota, !this.config.EnforceQuota, new List<KeyValuePair<string, object>>
				{
					new KeyValuePair<string, object>("AvailableCapacity", this.GetAvailableResourceSize(resource)),
					new KeyValuePair<string, object>("throttledDuration", this.currentTimeProvider().Subtract(value).ToString("d\\.hh\\:mm\\:ss"))
				});
				if (!usageData.GetIsOverQuota(QueueQuotaResources.All))
				{
					this.perfCounters.DecrementThrottledEntities(entityType, externalOrganizationId);
				}
			}
			else if (!onlySetUnthrottle && flag2 && !overWarningFlag)
			{
				this.flowControlLog.LogWarning(QueueQuotaComponent.GetThrottlingResource(resource), ThrottlingAction.TempReject, num2, TimeSpan.Zero, QueueQuotaComponent.GetThrottlingScope(entityType), externalOrganizationId, QueueQuotaComponent.GetRedactedSender(entityType, entityId), null, null, ThrottlingSource.QueueQuota, !this.config.EnforceQuota, new List<KeyValuePair<string, object>>
				{
					new KeyValuePair<string, object>("AvailableCapacity", this.GetAvailableResourceSize(resource)),
					new KeyValuePair<string, object>("observedValue", usage)
				});
			}
			return flag;
		}

		// Token: 0x06002450 RID: 9296 RVA: 0x0008AA24 File Offset: 0x00088C24
		private static string GetRedactedSender(QueueQuotaEntity entityType, string entityId)
		{
			string result = null;
			if (QueueQuotaComponent.GetSender(entityType, entityId) != null)
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

		// Token: 0x06002451 RID: 9297 RVA: 0x0008AA68 File Offset: 0x00088C68
		private static ThrottlingScope GetThrottlingScope(QueueQuotaEntity entityType)
		{
			switch (entityType)
			{
			case QueueQuotaEntity.Organization:
				return ThrottlingScope.Tenant;
			case QueueQuotaEntity.Sender:
				return ThrottlingScope.Sender;
			default:
				throw new InvalidOperationException(string.Format("Unexpected QueueQuotaEntity found: {0}", entityType));
			}
		}

		// Token: 0x06002452 RID: 9298 RVA: 0x0008AAA0 File Offset: 0x00088CA0
		private static ThrottlingResource GetThrottlingResource(QueueQuotaResources resource)
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

		// Token: 0x06002453 RID: 9299 RVA: 0x0008AADA File Offset: 0x00088CDA
		private static string GetSender(QueueQuotaEntity entityType, string entityId)
		{
			if (entityType != QueueQuotaEntity.Sender)
			{
				return null;
			}
			return entityId;
		}

		// Token: 0x06002454 RID: 9300 RVA: 0x0008AAE3 File Offset: 0x00088CE3
		private UsageData NewUsageData(string key)
		{
			return new UsageData(this.config.TrackSummaryLoggingInterval, this.config.TrackSummaryBucketLength, this.currentTimeProvider);
		}

		// Token: 0x06002455 RID: 9301 RVA: 0x0008AB06 File Offset: 0x00088D06
		private OrganizationUsageData NewOrganizationUsageData(Guid key)
		{
			return new OrganizationUsageData(key, this.config.TrackSummaryLoggingInterval, this.config.TrackSummaryBucketLength, this.currentTimeProvider);
		}

		// Token: 0x06002456 RID: 9302 RVA: 0x0008AB2A File Offset: 0x00088D2A
		private int GetAvailableResourceSize(QueueQuotaResources resource)
		{
			return Math.Max(this.GetResourceCapacity(resource) - this.GetUsage(resource), 0);
		}

		// Token: 0x06002457 RID: 9303 RVA: 0x0008AB41 File Offset: 0x00088D41
		private int GetOrganizationQuotaHighMark(Guid organizationId, QueueQuotaResources resource)
		{
			if (organizationId == MultiTenantTransport.SafeTenantId)
			{
				return this.GetAvailableResourceSize(resource) * this.config.SafeTenantOrganizationQueueQuota / 100;
			}
			return this.GetAvailableResourceSize(resource) * this.config.OrganizationQueueQuota / 100;
		}

		// Token: 0x06002458 RID: 9304 RVA: 0x0008AB7D File Offset: 0x00088D7D
		private int GetOrganizationWarningMark(Guid organizationId, QueueQuotaResources resource)
		{
			return (int)((double)this.GetOrganizationQuotaHighMark(organizationId, resource) * this.config.WarningRatio);
		}

		// Token: 0x06002459 RID: 9305 RVA: 0x0008AB98 File Offset: 0x00088D98
		private int GetSenderQuotaHighMark(Guid organizationId, string sender, QueueQuotaResources resource)
		{
			int num = (sender == RoutingAddress.NullReversePath.ToString()) ? this.config.NullSenderQueueQuota : this.config.SenderQueueQuota;
			return this.GetAvailableResourceSize(resource) * this.config.OrganizationQueueQuota * num / 10000;
		}

		// Token: 0x0600245A RID: 9306 RVA: 0x0008ABF4 File Offset: 0x00088DF4
		private int GetSenderWarningMark(Guid organizationId, string sender, QueueQuotaResources resource)
		{
			return (int)((double)this.GetSenderQuotaHighMark(organizationId, sender, resource) * this.config.WarningRatio);
		}

		// Token: 0x0600245B RID: 9307 RVA: 0x0008AC10 File Offset: 0x00088E10
		private int GetResourceCapacity(QueueQuotaResources resource)
		{
			switch (resource)
			{
			case QueueQuotaResources.SubmissionQueueSize:
				return this.config.SubmissionQueueCapacity;
			case QueueQuotaResources.TotalQueueSize:
				return this.config.TotalQueueCapacity;
			default:
				throw new ArgumentOutOfRangeException("resource");
			}
		}

		// Token: 0x0600245C RID: 9308 RVA: 0x0008AC54 File Offset: 0x00088E54
		private bool IsOrganizationAllowListed(Guid organizationId)
		{
			if (this.processingQuotaComponent != null)
			{
				ProcessingQuotaComponent.ProcessingData quotaOverride = this.processingQuotaComponent.GetQuotaOverride(organizationId);
				if (quotaOverride != null)
				{
					return quotaOverride.IsAllowListed;
				}
			}
			return false;
		}

		// Token: 0x0600245D RID: 9309 RVA: 0x0008AC84 File Offset: 0x00088E84
		private bool IsOrganizationBlocked(Guid organizationId)
		{
			if (this.processingQuotaComponent != null)
			{
				ProcessingQuotaComponent.ProcessingData quotaOverride = this.processingQuotaComponent.GetQuotaOverride(organizationId);
				if (quotaOverride != null)
				{
					return quotaOverride.IsBlocked;
				}
			}
			return false;
		}

		// Token: 0x0600245E RID: 9310 RVA: 0x0008AD68 File Offset: 0x00088F68
		private void LogSummary(string sequenceNumber)
		{
			this.Cleanup(this.config.TrackerEntryLifeTime);
			foreach (QueueQuotaResources queueQuotaResources in this.allResources)
			{
				if (this.totalData.GetRejectedCount(queueQuotaResources) > 0)
				{
					this.flowControlLog.LogSummary(sequenceNumber, QueueQuotaComponent.GetThrottlingResource(queueQuotaResources), ThrottlingAction.TempReject, this.GetResourceCapacity(queueQuotaResources), TimeSpan.Zero, this.totalData.GetUsage(queueQuotaResources), this.totalData.GetRejectedCount(queueQuotaResources), ThrottlingScope.All, Guid.Empty, null, null, null, ThrottlingSource.QueueQuota, !this.config.EnforceQuota, null);
				}
			}
			IEnumerable<QueueQuotaComponent.SortableEntry> enumerable = Enumerable.Empty<QueueQuotaComponent.SortableEntry>();
			QueueQuotaResources[] array2 = this.allResources;
			for (int j = 0; j < array2.Length; j++)
			{
				QueueQuotaResources resource = array2[j];
				enumerable = enumerable.Concat(this.orgQuotaDictionary.SelectMany((KeyValuePair<Guid, OrganizationUsageData> o) => from s in o.Value
				select new QueueQuotaComponent.SortableEntry(o.Key, s.Key, resource, s.Value)));
			}
			this.LogUnthrottledAndUpdateState(enumerable);
			int num;
			if (!this.LogThrottled(enumerable, sequenceNumber, out num))
			{
				IEnumerable<QueueQuotaComponent.SortableEntry> enumerable2 = (from data in enumerable
				where data.Usage.GetOverWarningFlag(data.Resource) && !data.Usage.GetIsOverQuota(data.Resource)
				orderby data.Usage.GetUsage(data.Resource) descending
				select data).Take(Math.Max(this.config.MaxSummaryLinesLogged - num, 0));
				foreach (QueueQuotaComponent.SortableEntry sortableEntry in enumerable2)
				{
					bool isOrganization = sortableEntry.IsOrganization;
					this.flowControlLog.LogSummaryWarning(QueueQuotaComponent.GetThrottlingResource(sortableEntry.Resource), ThrottlingAction.TempReject, isOrganization ? this.GetOrganizationWarningMark(sortableEntry.Tenant, sortableEntry.Resource) : this.GetSenderWarningMark(sortableEntry.Tenant, sortableEntry.Sender, sortableEntry.Resource), TimeSpan.Zero, isOrganization ? ThrottlingScope.Tenant : ThrottlingScope.Sender, sortableEntry.Tenant, (!isOrganization) ? QueueQuotaComponent.GetRedactedSender(QueueQuotaEntity.Sender, sortableEntry.Sender) : null, null, null, ThrottlingSource.QueueQuota, !this.config.EnforceQuota, new List<KeyValuePair<string, object>>
					{
						new KeyValuePair<string, object>("AvailableCapacity", this.GetAvailableResourceSize(sortableEntry.Resource)),
						new KeyValuePair<string, object>("observedValue", sortableEntry.Usage.GetUsage(sortableEntry.Resource))
					});
				}
			}
		}

		// Token: 0x0600245F RID: 9311 RVA: 0x0008B0A0 File Offset: 0x000892A0
		private void LogUnthrottledAndUpdateState(IEnumerable<QueueQuotaComponent.SortableEntry> allRows)
		{
			double ratio = Math.Max(this.config.LowWatermarkRatio, this.config.WarningRatio);
			IEnumerable<QueueQuotaComponent.SortableEntry> enumerable = from r in allRows
			where (r.IsOrganization && (double)r.Usage.GetUsage(r.Resource) < (double)this.GetOrganizationQuotaHighMark(r.Tenant, r.Resource) * ratio) || (!r.IsOrganization && (double)r.Usage.GetUsage(r.Resource) < (double)this.GetSenderQuotaHighMark(r.Tenant, r.Sender, r.Resource) * ratio)
			select r;
			foreach (QueueQuotaComponent.SortableEntry sortableEntry in enumerable)
			{
				string text;
				this.ComputeIsOverQuota(sortableEntry.Tenant, sortableEntry.IsOrganization ? QueueQuotaEntity.Organization : QueueQuotaEntity.Sender, sortableEntry.IsOrganization ? sortableEntry.Tenant.ToString() : sortableEntry.Sender, sortableEntry.Usage, sortableEntry.Resource, sortableEntry.IsOrganization ? this.GetOrganizationQuotaHighMark(sortableEntry.Tenant, sortableEntry.Resource) : this.GetSenderQuotaHighMark(sortableEntry.Tenant, sortableEntry.Sender, sortableEntry.Resource), true, out text);
			}
		}

		// Token: 0x06002460 RID: 9312 RVA: 0x0008B204 File Offset: 0x00089404
		private bool LogThrottled(IEnumerable<QueueQuotaComponent.SortableEntry> allRows, string sequenceNumber, out int resultCount)
		{
			IEnumerable<QueueQuotaComponent.SortableEntry> enumerable = from data in allRows
			where data.Usage.GetIsOverQuota(data.Resource) || data.Usage.GetRejectedCount(data.Resource) > 0
			orderby data.Usage.GetRejectedCount(data.Resource) descending, data.Usage.GetUsage(data.Resource)
			select data;
			bool flag = false;
			resultCount = enumerable.Count<QueueQuotaComponent.SortableEntry>();
			if (resultCount > this.config.MaxSummaryLinesLogged)
			{
				enumerable = enumerable.Take(this.config.MaxSummaryLinesLogged);
				flag = true;
			}
			foreach (QueueQuotaComponent.SortableEntry sortableEntry in enumerable)
			{
				bool isOrganization = sortableEntry.IsOrganization;
				this.flowControlLog.LogSummary(sequenceNumber, QueueQuotaComponent.GetThrottlingResource(sortableEntry.Resource), ThrottlingAction.TempReject, isOrganization ? this.GetOrganizationQuotaHighMark(sortableEntry.Tenant, sortableEntry.Resource) : this.GetSenderQuotaHighMark(sortableEntry.Tenant, sortableEntry.Sender, sortableEntry.Resource), TimeSpan.Zero, sortableEntry.Usage.GetUsage(sortableEntry.Resource), sortableEntry.Usage.GetRejectedCount(sortableEntry.Resource), isOrganization ? ThrottlingScope.Tenant : ThrottlingScope.Sender, sortableEntry.Tenant, (!isOrganization) ? QueueQuotaComponent.GetRedactedSender(QueueQuotaEntity.Sender, sortableEntry.Sender) : null, null, null, ThrottlingSource.QueueQuota, !this.config.EnforceQuota, new List<KeyValuePair<string, object>>
				{
					new KeyValuePair<string, object>("AvailableCapacity", this.GetAvailableResourceSize(sortableEntry.Resource))
				});
			}
			if (flag)
			{
				this.flowControlLog.LogMaxLinesExceeded(sequenceNumber, ThrottlingSource.QueueQuota, this.config.MaxSummaryLinesLogged, resultCount, null);
			}
			this.UpdatedOldestEntityPerfCounter(allRows, QueueQuotaEntity.Organization, false);
			this.UpdatedOldestEntityPerfCounter(allRows, QueueQuotaEntity.Sender, false);
			this.UpdatedOldestEntityPerfCounter(allRows, QueueQuotaEntity.Organization, true);
			return flag;
		}

		// Token: 0x06002461 RID: 9313 RVA: 0x0008B458 File Offset: 0x00089658
		private void UpdatedOldestEntityPerfCounter(IEnumerable<QueueQuotaComponent.SortableEntry> allRows, QueueQuotaEntity entity, bool isForSafeTenant)
		{
			bool isOrg = entity == QueueQuotaEntity.Organization;
			bool flag = false;
			Guid organizationId = isForSafeTenant ? MultiTenantTransport.SafeTenantId : Guid.Empty;
			using (IEnumerator<QueueQuotaComponent.SortableEntry> enumerator = (from data in allRows
			where data.Usage.GetIsOverQuota(data.Resource) && data.IsOrganization == isOrg && (!isOrg || data.Tenant == MultiTenantTransport.SafeTenantId == isForSafeTenant)
			orderby data.Usage.ThrottlingStartTime
			select data).Take(1).GetEnumerator())
			{
				if (enumerator.MoveNext())
				{
					QueueQuotaComponent.SortableEntry sortableEntry = enumerator.Current;
					this.perfCounters.UpdateOldestThrottledEntity(entity, this.currentTimeProvider() - sortableEntry.Usage.ThrottlingStartTime, sortableEntry.Tenant);
					flag = true;
					organizationId = sortableEntry.Tenant;
				}
			}
			if (!flag)
			{
				this.perfCounters.UpdateOldestThrottledEntity(entity, TimeSpan.Zero, organizationId);
			}
		}

		// Token: 0x06002462 RID: 9314 RVA: 0x0008B584 File Offset: 0x00089784
		private XElement GetDiagnosticInfo()
		{
			XElement xelement = new XElement("QueueUsage");
			xelement.SetAttributeValue("Version", "OldQQ");
			xelement.SetAttributeValue("NumberOfOrganizationsTracked", this.TrackedOrganizationCount);
			xelement.SetAttributeValue("NumberOfSendersTracked", this.TrackedSenderCount);
			xelement.SetAttributeValue("NumberOfOrganizationsDisplayed", this.config.NumberOfOrganizationsLoggedInSummary);
			QueueQuotaResources[] array = this.allResources;
			for (int i = 0; i < array.Length; i++)
			{
				QueueQuotaResources resource = array[i];
				XElement xelement2 = new XElement(resource.ToString());
				xelement2.SetAttributeValue("TotalUsage", this.GetUsage(resource));
				foreach (KeyValuePair<Guid, OrganizationUsageData> keyValuePair in (from data in this.orgQuotaDictionary
				orderby data.Value.GetUsage(resource) descending
				select data).Take(this.config.NumberOfOrganizationsLoggedInSummary))
				{
					XElement usageElement = keyValuePair.Value.GetUsageElement(QueueQuotaEntity.Organization.ToString(), resource, keyValuePair.Key.ToString());
					foreach (KeyValuePair<string, UsageData> keyValuePair2 in (from s in keyValuePair.Value.SenderQuotaDictionary
					orderby s.Value.GetUsage(resource) descending
					select s).Take(this.config.NumberOfSendersLoggedInSummary))
					{
						usageElement.Add(keyValuePair2.Value.GetUsageElement(QueueQuotaEntity.Sender.ToString(), resource, keyValuePair2.Key));
					}
					xelement2.Add(usageElement);
				}
				xelement.Add(xelement2);
			}
			return xelement;
		}

		// Token: 0x06002463 RID: 9315 RVA: 0x0008B7E8 File Offset: 0x000899E8
		private XElement GetDiagnosticInfo(Guid externalOrganizationId)
		{
			XElement xelement = new XElement("QueueUsage");
			OrganizationUsageData organizationUsageData;
			if (this.orgQuotaDictionary.TryGetValue(externalOrganizationId, out organizationUsageData))
			{
				QueueQuotaResources[] array = this.allResources;
				for (int i = 0; i < array.Length; i++)
				{
					QueueQuotaResources resource = array[i];
					XElement xelement2 = new XElement(resource.ToString());
					XElement usageElement = organizationUsageData.GetUsageElement(QueueQuotaEntity.Organization.ToString(), resource, externalOrganizationId.ToString());
					foreach (KeyValuePair<string, UsageData> keyValuePair in from s in organizationUsageData.SenderQuotaDictionary
					orderby s.Value.GetUsage(resource) descending
					select s)
					{
						usageElement.Add(keyValuePair.Value.GetUsageElement(QueueQuotaEntity.Sender.ToString(), resource, keyValuePair.Key));
					}
					xelement2.Add(usageElement);
					xelement.Add(xelement2);
				}
			}
			else
			{
				xelement.Add(new XElement("Error", string.Format("Organization with id {0} not present in queue quota component", externalOrganizationId)));
			}
			return xelement;
		}

		// Token: 0x17000B3F RID: 2879
		// (get) Token: 0x06002464 RID: 9316 RVA: 0x0008B94C File Offset: 0x00089B4C
		private int TrackedOrganizationCount
		{
			get
			{
				return this.orgQuotaDictionary.Count;
			}
		}

		// Token: 0x17000B40 RID: 2880
		// (get) Token: 0x06002465 RID: 9317 RVA: 0x0008B97C File Offset: 0x00089B7C
		private int TrackedSenderCount
		{
			get
			{
				return this.orgQuotaDictionary.Sum(delegate(KeyValuePair<Guid, OrganizationUsageData> data)
				{
					if (data.Value.SenderQuotaDictionary == null)
					{
						return 0;
					}
					return data.Value.SenderQuotaDictionary.Count;
				});
			}
		}

		// Token: 0x06002466 RID: 9318 RVA: 0x0008B9A6 File Offset: 0x00089BA6
		private int GetUsage(QueueQuotaResources resource)
		{
			return this.totalData.GetUsage(resource);
		}

		// Token: 0x06002467 RID: 9319 RVA: 0x0008B9B4 File Offset: 0x00089BB4
		public void Cleanup(TimeSpan cleanupInterval)
		{
			UsageData.Cleanup<Guid, OrganizationUsageData>(this.orgQuotaDictionary, cleanupInterval);
		}

		// Token: 0x06002468 RID: 9320 RVA: 0x0008B9C2 File Offset: 0x00089BC2
		string IDiagnosable.GetDiagnosticComponentName()
		{
			return "QueueQuota";
		}

		// Token: 0x06002469 RID: 9321 RVA: 0x0008B9CC File Offset: 0x00089BCC
		XElement IDiagnosable.GetDiagnosticInfo(DiagnosableParameters parameters)
		{
			XElement xelement = new XElement(((IDiagnosable)this).GetDiagnosticComponentName());
			bool flag = parameters.Argument.IndexOf("verbose", StringComparison.OrdinalIgnoreCase) != -1;
			bool flag2 = parameters.Argument.IndexOf("tenant:", StringComparison.OrdinalIgnoreCase) != -1;
			bool flag3 = parameters.Argument.Equals("config", StringComparison.InvariantCultureIgnoreCase);
			bool flag4 = (!flag3 && !flag && !flag2) || parameters.Argument.IndexOf("help", StringComparison.OrdinalIgnoreCase) != -1;
			if (flag)
			{
				xelement.Add(this.GetDiagnosticInfo());
			}
			if (flag2)
			{
				string text = parameters.Argument.Substring(7);
				Guid externalOrganizationId;
				if (Guid.TryParse(text, out externalOrganizationId))
				{
					xelement.Add(this.GetDiagnosticInfo(externalOrganizationId));
				}
				else
				{
					xelement.Add(new XElement("Error", string.Format("Invalid external organization id {0} passed as argument. Expecting a Guid.", text)));
				}
			}
			if (flag3)
			{
				xelement.Add(TransportAppConfig.GetDiagnosticInfoForType(this.config));
			}
			if (flag4)
			{
				xelement.Add(new XElement("help", "Supported arguments: verbose, help, config, tenant:{tenantID e.g.1afa2e80-0251-4521-8086-039fb2f9d8d6}."));
			}
			return xelement;
		}

		// Token: 0x0600246A RID: 9322 RVA: 0x0008BAE5 File Offset: 0x00089CE5
		public void Load()
		{
		}

		// Token: 0x0600246B RID: 9323 RVA: 0x0008BAE7 File Offset: 0x00089CE7
		public void Unload()
		{
		}

		// Token: 0x0600246C RID: 9324 RVA: 0x0008BAE9 File Offset: 0x00089CE9
		public string OnUnhandledException(Exception e)
		{
			return null;
		}

		// Token: 0x040012D0 RID: 4816
		private const string DiagnosticsComponentName = "QueueQuota";

		// Token: 0x040012D1 RID: 4817
		private const string AvailableCapacityProperty = "AvailableCapacity";

		// Token: 0x040012D2 RID: 4818
		private IQueueQuotaConfig config;

		// Token: 0x040012D3 RID: 4819
		private readonly ConcurrentDictionary<Guid, OrganizationUsageData> orgQuotaDictionary = new ConcurrentDictionary<Guid, OrganizationUsageData>(Environment.ProcessorCount, 1000);

		// Token: 0x040012D4 RID: 4820
		private readonly QueueQuotaResources[] allResources = new QueueQuotaResources[]
		{
			QueueQuotaResources.SubmissionQueueSize,
			QueueQuotaResources.TotalQueueSize
		};

		// Token: 0x040012D5 RID: 4821
		private readonly Func<DateTime> currentTimeProvider;

		// Token: 0x040012D6 RID: 4822
		private UsageData totalData;

		// Token: 0x040012D7 RID: 4823
		private IFlowControlLog flowControlLog;

		// Token: 0x040012D8 RID: 4824
		private IQueueQuotaComponentPerformanceCounters perfCounters;

		// Token: 0x040012D9 RID: 4825
		private IProcessingQuotaComponent processingQuotaComponent;

		// Token: 0x0200034B RID: 843
		private class SortableEntry
		{
			// Token: 0x06002479 RID: 9337 RVA: 0x0008BAEC File Offset: 0x00089CEC
			public SortableEntry(Guid tenant, string sender, QueueQuotaResources resource, UsageData usage)
			{
				this.Tenant = tenant;
				this.Sender = sender;
				this.Resource = resource;
				this.Usage = usage;
			}

			// Token: 0x17000B41 RID: 2881
			// (get) Token: 0x0600247A RID: 9338 RVA: 0x0008BB11 File Offset: 0x00089D11
			// (set) Token: 0x0600247B RID: 9339 RVA: 0x0008BB19 File Offset: 0x00089D19
			public Guid Tenant { get; set; }

			// Token: 0x17000B42 RID: 2882
			// (get) Token: 0x0600247C RID: 9340 RVA: 0x0008BB22 File Offset: 0x00089D22
			// (set) Token: 0x0600247D RID: 9341 RVA: 0x0008BB2A File Offset: 0x00089D2A
			public string Sender { get; set; }

			// Token: 0x17000B43 RID: 2883
			// (get) Token: 0x0600247E RID: 9342 RVA: 0x0008BB33 File Offset: 0x00089D33
			// (set) Token: 0x0600247F RID: 9343 RVA: 0x0008BB3B File Offset: 0x00089D3B
			public QueueQuotaResources Resource { get; set; }

			// Token: 0x17000B44 RID: 2884
			// (get) Token: 0x06002480 RID: 9344 RVA: 0x0008BB44 File Offset: 0x00089D44
			// (set) Token: 0x06002481 RID: 9345 RVA: 0x0008BB4C File Offset: 0x00089D4C
			public UsageData Usage { get; set; }

			// Token: 0x17000B45 RID: 2885
			// (get) Token: 0x06002482 RID: 9346 RVA: 0x0008BB55 File Offset: 0x00089D55
			public bool IsOrganization
			{
				get
				{
					return this.Usage is OrganizationUsageData;
				}
			}
		}
	}
}
