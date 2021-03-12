using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Metering;
using Microsoft.Exchange.Data.Metering.Throttling;
using Microsoft.Exchange.Data.Transport;
using Microsoft.Exchange.Transport.LoggingCommon;

namespace Microsoft.Exchange.Transport
{
	// Token: 0x02000353 RID: 851
	internal class QueueQuotaImpl : IQueueQuotaThresholdFetcher
	{
		// Token: 0x060024DF RID: 9439 RVA: 0x0008DA5E File Offset: 0x0008BC5E
		public QueueQuotaImpl(IQueueQuotaConfig config, IFlowControlLog log, IQueueQuotaComponentPerformanceCounters perfCounters, IProcessingQuotaComponent processingQuotaComponent, ICountTracker<MeteredEntity, MeteredCount> metering) : this(config, log, perfCounters, processingQuotaComponent, metering, () => DateTime.UtcNow)
		{
		}

		// Token: 0x060024E0 RID: 9440 RVA: 0x0008DA8C File Offset: 0x0008BC8C
		public QueueQuotaImpl(IQueueQuotaConfig config, IFlowControlLog log, IQueueQuotaComponentPerformanceCounters perfCounters, IProcessingQuotaComponent processingQuotaComponent, ICountTracker<MeteredEntity, MeteredCount> metering, Func<DateTime> currentTimeProvider)
		{
			this.config = config;
			this.flowControlLog = log;
			this.flowControlLog.TrackSummary += this.UnthrottleIfNeeded;
			this.perfCounters = perfCounters;
			this.processingQuotaComponent = processingQuotaComponent;
			this.metering = metering;
			this.currentTimeProvider = currentTimeProvider;
			this.totalEntity = new CountedEntity<MeteredEntity>(new SimpleEntityName<MeteredEntity>(MeteredEntity.Total, "All"), new SimpleEntityName<MeteredEntity>(MeteredEntity.Total, "All"));
			this.acceptedCountConfig = AbsoluteCountConfig.Create(false, 0, TimeSpan.Zero, TimeSpan.Zero, false, TimeSpan.FromMinutes(5.0), TimeSpan.Zero);
			this.rejectedCountConfig = AbsoluteCountConfig.Create(false, 0, TimeSpan.Zero, TimeSpan.Zero, false, TimeSpan.FromMinutes(5.0), TimeSpan.Zero);
			this.pastRejectedCountConfig = RollingCountConfig.Create(false, 0, TimeSpan.Zero, TimeSpan.Zero, true, TimeSpan.FromMinutes(5.0), this.config.TrackSummaryLoggingInterval, this.config.TrackSummaryBucketLength);
			this.logger = new QueueQuotaDiagnosticLogger(this.config, this.flowControlLog, this.metering, this.totalEntity, this.perfCounters, this, this.currentTimeProvider);
		}

		// Token: 0x060024E1 RID: 9441 RVA: 0x0008DBC8 File Offset: 0x0008BDC8
		public void TrackEnteringQueue(IQueueQuotaMailItem mailItem, QueueQuotaResources resources)
		{
			Guid externalOrganizationId = mailItem.ExternalOrganizationId;
			string originalFromAddress = mailItem.OriginalFromAddress;
			string exoAccountForest = mailItem.ExoAccountForest;
			IEnumerable<MeteredCount> meteredCount = QueueQuotaHelper.GetMeteredCount(resources);
			this.IncrementUsage(this.totalEntity, meteredCount, 1);
			if (this.config.AccountForestEnabled && !string.IsNullOrEmpty(exoAccountForest))
			{
				ICountedEntity<MeteredEntity> entity = QueueQuotaHelper.CreateEntity(exoAccountForest);
				this.IncrementUsage(entity, meteredCount, 1);
				mailItem.QueueQuotaTrackingBits[QueueQuotaEntity.AccountForest, resources] = true;
			}
			ICountedEntity<MeteredEntity> entity2 = QueueQuotaHelper.CreateEntity(externalOrganizationId);
			this.IncrementUsage(entity2, meteredCount, 1);
			mailItem.QueueQuotaTrackingBits[QueueQuotaEntity.Organization, resources] = true;
			if (!string.IsNullOrEmpty(originalFromAddress))
			{
				long usageSum = this.GetUsageSum(entity2, meteredCount);
				if (usageSum > (long)this.config.SenderTrackingThreshold)
				{
					ICountedEntity<MeteredEntity> entity3 = QueueQuotaHelper.CreateEntity(externalOrganizationId, originalFromAddress);
					this.IncrementUsage(entity3, meteredCount, 1);
					mailItem.QueueQuotaTrackingBits[QueueQuotaEntity.Sender, resources] = true;
				}
			}
		}

		// Token: 0x060024E2 RID: 9442 RVA: 0x0008DC98 File Offset: 0x0008BE98
		public void TrackExitingQueue(IQueueQuotaMailItem mailItem, QueueQuotaResources resources)
		{
			Guid externalOrganizationId = mailItem.ExternalOrganizationId;
			string originalFromAddress = mailItem.OriginalFromAddress;
			string exoAccountForest = mailItem.ExoAccountForest;
			IEnumerable<MeteredCount> meteredCount = QueueQuotaHelper.GetMeteredCount(resources);
			this.IncrementUsage(this.totalEntity, meteredCount, -1);
			if (mailItem.QueueQuotaTrackingBits[QueueQuotaEntity.Organization, resources])
			{
				ICountedEntity<MeteredEntity> entity = QueueQuotaHelper.CreateEntity(externalOrganizationId);
				this.IncrementUsage(entity, meteredCount, -1);
				mailItem.QueueQuotaTrackingBits[QueueQuotaEntity.Organization, resources] = false;
				if (!string.IsNullOrEmpty(originalFromAddress) && mailItem.QueueQuotaTrackingBits[QueueQuotaEntity.Sender, resources])
				{
					ICountedEntity<MeteredEntity> entity2 = QueueQuotaHelper.CreateEntity(externalOrganizationId, originalFromAddress);
					this.IncrementUsage(entity2, meteredCount, -1);
					mailItem.QueueQuotaTrackingBits[QueueQuotaEntity.Sender, resources] = false;
				}
			}
			if (mailItem.QueueQuotaTrackingBits[QueueQuotaEntity.AccountForest, resources])
			{
				ICountedEntity<MeteredEntity> entity3 = QueueQuotaHelper.CreateEntity(exoAccountForest);
				this.IncrementUsage(entity3, meteredCount, -1);
				mailItem.QueueQuotaTrackingBits[QueueQuotaEntity.AccountForest, resources] = false;
			}
		}

		// Token: 0x060024E3 RID: 9443 RVA: 0x0008DD68 File Offset: 0x0008BF68
		public bool IsOrganizationOverQuota(string accountForest, Guid externalOrganizationId, string sender, out string reason)
		{
			QueueQuotaEntity? entity;
			QueueQuotaResources? queueQuotaResources;
			bool flag = this.IsOverQuota(accountForest, externalOrganizationId, sender, out reason, out entity, out queueQuotaResources);
			if (flag)
			{
				QueueQuotaEntity valueOrDefault = entity.GetValueOrDefault();
				if (entity != null)
				{
					switch (valueOrDefault)
					{
					case QueueQuotaEntity.Organization:
					{
						ICountedEntity<MeteredEntity> entity2 = QueueQuotaHelper.CreateEntity(externalOrganizationId);
						this.metering.AddUsage(entity2, QueueQuotaHelper.GetRejectedMeter(queueQuotaResources.Value), this.rejectedCountConfig, 1L);
						goto IL_F2;
					}
					case QueueQuotaEntity.Sender:
						if (!string.IsNullOrEmpty(sender))
						{
							ICountedEntity<MeteredEntity> entity3 = QueueQuotaHelper.CreateEntity(externalOrganizationId, sender);
							this.metering.AddUsage(entity3, QueueQuotaHelper.GetRejectedMeter(queueQuotaResources.Value), this.rejectedCountConfig, 1L);
							goto IL_F2;
						}
						goto IL_F2;
					case QueueQuotaEntity.AccountForest:
					{
						ICountedEntity<MeteredEntity> entity4 = QueueQuotaHelper.CreateEntity(accountForest);
						this.metering.AddUsage(entity4, QueueQuotaHelper.GetRejectedMeter(queueQuotaResources.Value), this.rejectedCountConfig, 1L);
						goto IL_F2;
					}
					}
				}
				this.metering.AddUsage(this.totalEntity, QueueQuotaHelper.GetRejectedMeter(queueQuotaResources.Value), this.pastRejectedCountConfig, 1L);
			}
			IL_F2:
			if (this.config.EnforceQuota && flag)
			{
				this.perfCounters.IncrementMessagesRejected(entity, new Guid?(externalOrganizationId));
				return true;
			}
			return false;
		}

		// Token: 0x060024E4 RID: 9444 RVA: 0x0008DE90 File Offset: 0x0008C090
		public bool IsOrganizationOverWarning(string accountForest, Guid externalOrganizationId, string sender, QueueQuotaResources resource)
		{
			IEnumerable<MeteredCount> meteredCount = QueueQuotaHelper.GetMeteredCount(resource);
			IDictionary<MeteredCount, ICount<MeteredEntity, MeteredCount>> usage;
			if (this.config.AccountForestEnabled && !string.IsNullOrEmpty(accountForest))
			{
				ICountedEntity<MeteredEntity> entity = QueueQuotaHelper.CreateEntity(accountForest);
				usage = this.metering.GetUsage(entity, meteredCount.ToArray<MeteredCount>());
				if (usage.Values.Any(new Func<ICount<MeteredEntity, MeteredCount>, bool>(QueueQuotaHelper.IsWarningLogged)))
				{
					return true;
				}
			}
			ICountedEntity<MeteredEntity> entity2 = QueueQuotaHelper.CreateEntity(externalOrganizationId);
			usage = this.metering.GetUsage(entity2, meteredCount.ToArray<MeteredCount>());
			if (usage.Values.Any(new Func<ICount<MeteredEntity, MeteredCount>, bool>(QueueQuotaHelper.IsWarningLogged)))
			{
				return true;
			}
			if (!string.IsNullOrEmpty(sender))
			{
				ICountedEntity<MeteredEntity> entity3 = QueueQuotaHelper.CreateEntity(externalOrganizationId, sender);
				usage = this.metering.GetUsage(entity3, meteredCount.ToArray<MeteredCount>());
				if (usage.Values.Any(new Func<ICount<MeteredEntity, MeteredCount>, bool>(QueueQuotaHelper.IsWarningLogged)))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x060024E5 RID: 9445 RVA: 0x0008DF68 File Offset: 0x0008C168
		public void TimedUpdate()
		{
			this.perfCounters.Refresh(new QueueQuotaEntity?(QueueQuotaEntity.Organization));
			this.perfCounters.Refresh(new QueueQuotaEntity?(QueueQuotaEntity.Sender));
			this.perfCounters.Refresh(null);
		}

		// Token: 0x060024E6 RID: 9446 RVA: 0x0008DFE4 File Offset: 0x0008C1E4
		internal bool IsOverQuota(string accountForest, Guid externalOrganizationId, string sender, out string reason, out QueueQuotaEntity? reasonEntity, out QueueQuotaResources? reasonResource)
		{
			reason = null;
			reasonEntity = null;
			reasonResource = null;
			foreach (QueueQuotaResources queueQuotaResources in QueueQuotaHelper.AllResources)
			{
				long usageSum = this.GetUsageSum(this.totalEntity, QueueQuotaHelper.GetMeteredCount(queueQuotaResources).ToArray<MeteredCount>());
				int resourceCapacity = this.GetResourceCapacity(queueQuotaResources);
				if (usageSum > (long)resourceCapacity)
				{
					reason = string.Format("Resource {0} beyond capacity. Count:{1} Capacity:{2}", queueQuotaResources, usageSum, resourceCapacity);
					reasonResource = new QueueQuotaResources?(queueQuotaResources);
					return true;
				}
			}
			if (this.config.AccountForestEnabled && !string.IsNullOrEmpty(accountForest))
			{
				ICountedEntity<MeteredEntity> countedEntity = QueueQuotaHelper.CreateEntity(accountForest);
				if (this.CheckQuotaForEntity(countedEntity, QueueQuotaEntity.AccountForest, countedEntity.Name.Value, externalOrganizationId, new Func<QueueQuotaResources, int>(this.GetAccountForestQuotaHighMark), out reason, out reasonResource))
				{
					reasonEntity = new QueueQuotaEntity?(QueueQuotaEntity.AccountForest);
					return true;
				}
			}
			if (this.IsOrganizationBlocked(externalOrganizationId))
			{
				reason = "Organization is in block list.";
				reasonEntity = new QueueQuotaEntity?(QueueQuotaEntity.Organization);
				reasonResource = new QueueQuotaResources?(QueueQuotaResources.SubmissionQueueSize);
				return true;
			}
			if (this.IsOrganizationAllowListed(externalOrganizationId))
			{
				reason = "Organization is in allow list.";
				return false;
			}
			ICountedEntity<MeteredEntity> entity = QueueQuotaHelper.CreateEntity(externalOrganizationId);
			if (this.CheckQuotaForEntity(entity, QueueQuotaEntity.Organization, externalOrganizationId.ToString(), externalOrganizationId, (QueueQuotaResources r) => ((IQueueQuotaThresholdFetcher)this).GetOrganizationQuotaHighMark(externalOrganizationId, r), out reason, out reasonResource))
			{
				reasonEntity = new QueueQuotaEntity?(QueueQuotaEntity.Organization);
				return true;
			}
			if (!string.IsNullOrEmpty(sender))
			{
				ICountedEntity<MeteredEntity> countedEntity2 = QueueQuotaHelper.CreateEntity(externalOrganizationId, sender);
				if (this.CheckQuotaForEntity(countedEntity2, QueueQuotaEntity.Sender, QueueQuotaHelper.GetRedactedSender(QueueQuotaEntity.Sender, countedEntity2.Name.Value), externalOrganizationId, (QueueQuotaResources r) => ((IQueueQuotaThresholdFetcher)this).GetSenderQuotaHighMark(externalOrganizationId, sender, r), out reason, out reasonResource))
				{
					reasonEntity = new QueueQuotaEntity?(QueueQuotaEntity.Sender);
					return true;
				}
			}
			return false;
		}

		// Token: 0x060024E7 RID: 9447 RVA: 0x0008E209 File Offset: 0x0008C409
		internal XElement GetDiagnosticInfo()
		{
			return this.logger.GetDiagnosticInfo();
		}

		// Token: 0x060024E8 RID: 9448 RVA: 0x0008E216 File Offset: 0x0008C416
		internal XElement GetDiagnosticInfo(Guid externalOrganizationId)
		{
			return this.logger.GetDiagnosticInfo(externalOrganizationId);
		}

		// Token: 0x060024E9 RID: 9449 RVA: 0x0008E224 File Offset: 0x0008C424
		internal XElement GetDiagnosticInfo(string accountForest)
		{
			return this.logger.GetDiagnosticInfo(accountForest);
		}

		// Token: 0x060024EA RID: 9450 RVA: 0x0008E234 File Offset: 0x0008C434
		private bool CheckQuotaForEntity(ICountedEntity<MeteredEntity> entity, QueueQuotaEntity entityType, string entityValue, Guid externalOrganizationId, Func<QueueQuotaResources, int> getHighMark, out string reason, out QueueQuotaResources? reasonResource)
		{
			IDictionary<MeteredCount, ICount<MeteredEntity, MeteredCount>> usage = this.metering.GetUsage(entity, QueueQuotaHelper.AllAcceptedCounts);
			foreach (ICount<MeteredEntity, MeteredCount> count in usage.Values)
			{
				bool wasAnyThrottled = QueueQuotaHelper.IsAnyThrottled(usage);
				QueueQuotaResources resource = QueueQuotaHelper.GetResource(count.Measure);
				if (this.ComputeIsOverQuota(externalOrganizationId, entityType, entityValue, count, resource, getHighMark(resource), false, wasAnyThrottled, out reason))
				{
					reasonResource = new QueueQuotaResources?(resource);
					return true;
				}
			}
			reason = null;
			reasonResource = null;
			return false;
		}

		// Token: 0x060024EB RID: 9451 RVA: 0x0008E2E0 File Offset: 0x0008C4E0
		private bool ComputeIsOverQuota(Guid externalOrganizationId, QueueQuotaEntity entityType, string entityId, ICount<MeteredEntity, MeteredCount> count, QueueQuotaResources resource, int high, bool onlySetUnthrottle, bool wasAnyThrottled, out string reason)
		{
			reason = null;
			long total = count.Total;
			int num = (int)((double)high * this.config.LowWatermarkRatio);
			int num2 = (int)((double)high * this.config.WarningRatio);
			DateTime value;
			bool flag = QueueQuotaHelper.HasThrottledTime(count, out value);
			bool flag2 = QueueQuotaHelper.IsWarningLogged(count);
			bool flag3 = total > (long)high || (flag && total > (long)num);
			bool flag4 = total > (long)num2 && !flag3;
			if (flag3)
			{
				if (onlySetUnthrottle)
				{
					return true;
				}
				reason = string.Format("{0} is above quota for {1}.Actual:{2} Low:{3} High:{4}", new object[]
				{
					entityType,
					resource,
					total,
					num,
					high
				});
				if (!flag)
				{
					count.SetObject("ThrottledStartTime", this.currentTimeProvider());
					count.SetObject("WarningLogged", false);
					this.flowControlLog.LogThrottle(QueueQuotaHelper.GetThrottlingResource(resource), ThrottlingAction.TempReject, high, TimeSpan.Zero, QueueQuotaHelper.GetThrottlingScope(entityType), QueueQuotaHelper.GetOrgId(entityType, externalOrganizationId), QueueQuotaHelper.GetSender(entityType, entityId), null, null, ThrottlingSource.QueueQuota, !this.config.EnforceQuota, new List<KeyValuePair<string, object>>
					{
						new KeyValuePair<string, object>("AvailableCapacity", ((IQueueQuotaThresholdFetcher)this).GetAvailableResourceSize(resource)),
						new KeyValuePair<string, object>("observedValue", (int)total),
						new KeyValuePair<string, object>("ScopeValue", entityId)
					});
					if (!wasAnyThrottled)
					{
						this.perfCounters.IncrementThrottledEntities(entityType, externalOrganizationId);
					}
				}
			}
			else if (flag)
			{
				MeteredCount rejectedMeter = QueueQuotaHelper.GetRejectedMeter(count.Measure);
				ICount<MeteredEntity, MeteredCount> usage = this.metering.GetUsage(count.Entity, rejectedMeter);
				count.SetObject("ThrottledStartTime", null);
				this.flowControlLog.LogUnthrottle(QueueQuotaHelper.GetThrottlingResource(resource), ThrottlingAction.TempReject, num, TimeSpan.Zero, (int)usage.Total, (int)total, QueueQuotaHelper.GetThrottlingScope(entityType), QueueQuotaHelper.GetOrgId(entityType, externalOrganizationId), QueueQuotaHelper.GetSender(entityType, entityId), null, null, ThrottlingSource.QueueQuota, !this.config.EnforceQuota, new List<KeyValuePair<string, object>>
				{
					new KeyValuePair<string, object>("AvailableCapacity", ((IQueueQuotaThresholdFetcher)this).GetAvailableResourceSize(resource)),
					new KeyValuePair<string, object>("throttledDuration", this.currentTimeProvider().Subtract(value).ToString("d\\.hh\\:mm\\:ss")),
					new KeyValuePair<string, object>("ScopeValue", entityId)
				});
				this.metering.AddUsage(count.Entity, QueueQuotaHelper.GetRejectedMeter(rejectedMeter), this.pastRejectedCountConfig, (long)((int)usage.Total));
				this.metering.TrySetUsage(count.Entity, rejectedMeter, 0L);
				if (!QueueQuotaHelper.IsAnyThrottled(this.metering.GetUsage(count.Entity, QueueQuotaHelper.AllAcceptedCounts)))
				{
					this.perfCounters.DecrementThrottledEntities(entityType, externalOrganizationId);
				}
			}
			else if (!onlySetUnthrottle && flag4 && !flag2)
			{
				count.SetObject("WarningLogged", true);
				this.flowControlLog.LogWarning(QueueQuotaHelper.GetThrottlingResource(resource), ThrottlingAction.TempReject, num2, TimeSpan.Zero, QueueQuotaHelper.GetThrottlingScope(entityType), QueueQuotaHelper.GetOrgId(entityType, externalOrganizationId), QueueQuotaHelper.GetSender(entityType, entityId), null, null, ThrottlingSource.QueueQuota, !this.config.EnforceQuota, new List<KeyValuePair<string, object>>
				{
					new KeyValuePair<string, object>("AvailableCapacity", ((IQueueQuotaThresholdFetcher)this).GetAvailableResourceSize(resource)),
					new KeyValuePair<string, object>("observedValue", (int)total),
					new KeyValuePair<string, object>("ScopeValue", entityId)
				});
			}
			else if (flag2 && !flag4)
			{
				count.SetObject("WarningLogged", false);
			}
			return flag3;
		}

		// Token: 0x060024EC RID: 9452 RVA: 0x0008E6E8 File Offset: 0x0008C8E8
		private void UnthrottleIfNeeded(string sequenceNumber)
		{
			IEnumerable<ICount<MeteredEntity, MeteredCount>> enumerable = this.metering.Filter((ICount<MeteredEntity, MeteredCount> count) => QueueQuotaHelper.IsQueueQuotaAcceptedCount(count) && (this.IsOrgLessThanLow(count) || this.IsSenderLessThanLow(count) || this.IsForestLessThanLow(count)));
			foreach (ICount<MeteredEntity, MeteredCount> count2 in enumerable)
			{
				if (QueueQuotaHelper.ShouldProcessEntity(count2.Entity))
				{
					QueueQuotaResources resource = QueueQuotaHelper.GetResource(count2.Measure);
					QueueQuotaLoggingContext queueQuotaLoggingContext = new QueueQuotaLoggingContext(count2.Entity, resource, this);
					bool wasAnyThrottled = QueueQuotaHelper.IsAnyThrottled(this.metering.GetUsage(count2.Entity, QueueQuotaHelper.AllAcceptedCounts));
					string text;
					this.ComputeIsOverQuota(queueQuotaLoggingContext.OrgId, QueueQuotaHelper.GetQueueQuotaEntity(count2.Entity.Name.Type), queueQuotaLoggingContext.ScopeValue, count2, resource, queueQuotaLoggingContext.HighThreshold, true, wasAnyThrottled, out text);
				}
			}
		}

		// Token: 0x060024ED RID: 9453 RVA: 0x0008E7C8 File Offset: 0x0008C9C8
		private void IncrementUsage(ICountedEntity<MeteredEntity> entity, IEnumerable<MeteredCount> meteredCounts, int increment)
		{
			foreach (MeteredCount measure in meteredCounts)
			{
				this.metering.AddUsage(entity, measure, this.acceptedCountConfig, (long)increment);
			}
		}

		// Token: 0x060024EE RID: 9454 RVA: 0x0008E820 File Offset: 0x0008CA20
		private long GetUsageSum(ICountedEntity<MeteredEntity> entity, IEnumerable<MeteredCount> meteredCounts)
		{
			IDictionary<MeteredCount, ICount<MeteredEntity, MeteredCount>> usage = this.metering.GetUsage(entity, meteredCounts.ToArray<MeteredCount>());
			return QueueQuotaHelper.GetSum(usage.Values);
		}

		// Token: 0x060024EF RID: 9455 RVA: 0x0008E84C File Offset: 0x0008CA4C
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

		// Token: 0x060024F0 RID: 9456 RVA: 0x0008E87C File Offset: 0x0008CA7C
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

		// Token: 0x060024F1 RID: 9457 RVA: 0x0008E8A9 File Offset: 0x0008CAA9
		int IQueueQuotaThresholdFetcher.GetAvailableResourceSize(QueueQuotaResources resource)
		{
			return Math.Max(this.GetResourceCapacity(resource) - (int)this.GetUsageSum(this.totalEntity, QueueQuotaHelper.GetMeteredCount(resource).ToArray<MeteredCount>()), 0);
		}

		// Token: 0x060024F2 RID: 9458 RVA: 0x0008E8D4 File Offset: 0x0008CAD4
		private bool IsOrgLessThanLow(ICount<MeteredEntity, MeteredCount> count)
		{
			double num = Math.Max(this.config.LowWatermarkRatio, this.config.WarningRatio);
			Guid organizationId;
			return QueueQuotaHelper.IsOrg(count.Entity, out organizationId) && (double)count.Total < (double)((IQueueQuotaThresholdFetcher)this).GetOrganizationQuotaHighMark(organizationId, QueueQuotaHelper.GetResource(count.Measure)) * num;
		}

		// Token: 0x060024F3 RID: 9459 RVA: 0x0008E92C File Offset: 0x0008CB2C
		private bool IsSenderLessThanLow(ICount<MeteredEntity, MeteredCount> count)
		{
			double num = Math.Max(this.config.LowWatermarkRatio, this.config.WarningRatio);
			Guid organizationId;
			return QueueQuotaHelper.IsSender(count.Entity, out organizationId) && (double)count.Total < (double)((IQueueQuotaThresholdFetcher)this).GetSenderQuotaHighMark(organizationId, count.Entity.Name.Value, QueueQuotaHelper.GetResource(count.Measure)) * num;
		}

		// Token: 0x060024F4 RID: 9460 RVA: 0x0008E994 File Offset: 0x0008CB94
		private bool IsForestLessThanLow(ICount<MeteredEntity, MeteredCount> count)
		{
			double num = Math.Max(this.config.LowWatermarkRatio, this.config.WarningRatio);
			return QueueQuotaHelper.IsAccountForest(count.Entity) && (double)count.Total < (double)((IQueueQuotaThresholdFetcher)this).GetAccountForestQuotaHighMark(QueueQuotaHelper.GetResource(count.Measure)) * num;
		}

		// Token: 0x060024F5 RID: 9461 RVA: 0x0008E9EC File Offset: 0x0008CBEC
		int IQueueQuotaThresholdFetcher.GetOrganizationQuotaHighMark(Guid organizationId, QueueQuotaResources resource)
		{
			int availableResourceSize = ((IQueueQuotaThresholdFetcher)this).GetAvailableResourceSize(resource);
			if (organizationId == MultiTenantTransport.SafeTenantId)
			{
				return availableResourceSize * this.config.SafeTenantOrganizationQueueQuota / 100;
			}
			if (organizationId == TemplateTenantConfiguration.TemplateTenantExternalDirectoryOrganizationIdGuid)
			{
				return availableResourceSize * this.config.OutlookTenantOrganizationQueueQuota / 100;
			}
			return availableResourceSize * this.config.OrganizationQueueQuota / 100;
		}

		// Token: 0x060024F6 RID: 9462 RVA: 0x0008EA4D File Offset: 0x0008CC4D
		int IQueueQuotaThresholdFetcher.GetOrganizationWarningMark(Guid organizationId, QueueQuotaResources resource)
		{
			return (int)((double)((IQueueQuotaThresholdFetcher)this).GetOrganizationQuotaHighMark(organizationId, resource) * this.config.WarningRatio);
		}

		// Token: 0x060024F7 RID: 9463 RVA: 0x0008EA68 File Offset: 0x0008CC68
		int IQueueQuotaThresholdFetcher.GetSenderQuotaHighMark(Guid organizationId, string sender, QueueQuotaResources resource)
		{
			int num = (sender == RoutingAddress.NullReversePath.ToString()) ? this.config.NullSenderQueueQuota : this.config.SenderQueueQuota;
			int num2 = this.config.OrganizationQueueQuota;
			if (organizationId == TemplateTenantConfiguration.TemplateTenantExternalDirectoryOrganizationIdGuid)
			{
				num = this.config.OutlookTenantSenderQueueQuota;
				num2 = this.config.OutlookTenantOrganizationQueueQuota;
			}
			return (int)((long)((IQueueQuotaThresholdFetcher)this).GetAvailableResourceSize(resource) * (long)num2 * (long)num / 10000L);
		}

		// Token: 0x060024F8 RID: 9464 RVA: 0x0008EAF0 File Offset: 0x0008CCF0
		int IQueueQuotaThresholdFetcher.GetSenderWarningMark(Guid organizationId, string sender, QueueQuotaResources resource)
		{
			return (int)((double)((IQueueQuotaThresholdFetcher)this).GetSenderQuotaHighMark(organizationId, sender, resource) * this.config.WarningRatio);
		}

		// Token: 0x060024F9 RID: 9465 RVA: 0x0008EB09 File Offset: 0x0008CD09
		int IQueueQuotaThresholdFetcher.GetAccountForestQuotaHighMark(QueueQuotaResources resource)
		{
			return ((IQueueQuotaThresholdFetcher)this).GetAvailableResourceSize(resource) * this.config.AccountForestQueueQuota / 100;
		}

		// Token: 0x060024FA RID: 9466 RVA: 0x0008EB21 File Offset: 0x0008CD21
		int IQueueQuotaThresholdFetcher.GetAccountForestWarningMark(QueueQuotaResources resource)
		{
			return (int)((double)((IQueueQuotaThresholdFetcher)this).GetAccountForestQuotaHighMark(resource) * this.config.WarningRatio);
		}

		// Token: 0x060024FB RID: 9467 RVA: 0x0008EB38 File Offset: 0x0008CD38
		private int GetResourceCapacity(QueueQuotaResources resource)
		{
			return QueueQuotaHelper.GetResourceCapacity(resource, this.config);
		}

		// Token: 0x0400131E RID: 4894
		private readonly AbsoluteCountConfig acceptedCountConfig;

		// Token: 0x0400131F RID: 4895
		private readonly AbsoluteCountConfig rejectedCountConfig;

		// Token: 0x04001320 RID: 4896
		private readonly RollingCountConfig pastRejectedCountConfig;

		// Token: 0x04001321 RID: 4897
		private readonly Func<DateTime> currentTimeProvider;

		// Token: 0x04001322 RID: 4898
		private IQueueQuotaConfig config;

		// Token: 0x04001323 RID: 4899
		private IFlowControlLog flowControlLog;

		// Token: 0x04001324 RID: 4900
		private IQueueQuotaComponentPerformanceCounters perfCounters;

		// Token: 0x04001325 RID: 4901
		private IProcessingQuotaComponent processingQuotaComponent;

		// Token: 0x04001326 RID: 4902
		private ICountTracker<MeteredEntity, MeteredCount> metering;

		// Token: 0x04001327 RID: 4903
		private ICountedEntity<MeteredEntity> totalEntity;

		// Token: 0x04001328 RID: 4904
		private QueueQuotaDiagnosticLogger logger;
	}
}
