using System;
using System.Xml.Linq;
using Microsoft.Exchange.Data.Metering;
using Microsoft.Exchange.Data.Metering.Throttling;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Transport
{
	// Token: 0x0200034C RID: 844
	internal class QueueQuotaComponentWithMetering : IQueueQuotaComponent, ITransportComponent, IDiagnosable
	{
		// Token: 0x06002483 RID: 9347 RVA: 0x0008BB6C File Offset: 0x00089D6C
		public QueueQuotaComponentWithMetering() : this(() => DateTime.UtcNow)
		{
		}

		// Token: 0x06002484 RID: 9348 RVA: 0x0008BB91 File Offset: 0x00089D91
		public QueueQuotaComponentWithMetering(Func<DateTime> currentTimeProvider)
		{
			this.currentTimeProvider = currentTimeProvider;
		}

		// Token: 0x06002485 RID: 9349 RVA: 0x0008BBDC File Offset: 0x00089DDC
		public void SetRunTimeDependencies(IQueueQuotaConfig config, IFlowControlLog log, IQueueQuotaComponentPerformanceCounters perfCounters, IProcessingQuotaComponent processingQuotaComponent, IQueueQuotaObservableComponent submissionQueue, IQueueQuotaObservableComponent deliveryQueue, ICountTracker<MeteredEntity, MeteredCount> metering)
		{
			this.config = config;
			this.queueQuota = new QueueQuotaImpl(config, log, perfCounters, processingQuotaComponent, metering, this.currentTimeProvider);
			if (submissionQueue != null)
			{
				submissionQueue.OnAcquire += delegate(TransportMailItem tmi)
				{
					this.queueQuota.TrackEnteringQueue(tmi, QueueQuotaResources.SubmissionQueueSize | QueueQuotaResources.TotalQueueSize);
				};
				submissionQueue.OnRelease += delegate(TransportMailItem tmi)
				{
					this.queueQuota.TrackExitingQueue(tmi, QueueQuotaResources.SubmissionQueueSize | QueueQuotaResources.TotalQueueSize);
				};
			}
			if (deliveryQueue != null)
			{
				deliveryQueue.OnAcquire += delegate(TransportMailItem tmi)
				{
					this.queueQuota.TrackEnteringQueue(tmi, QueueQuotaResources.TotalQueueSize);
				};
				deliveryQueue.OnRelease += delegate(TransportMailItem tmi)
				{
					this.queueQuota.TrackExitingQueue(tmi, QueueQuotaResources.TotalQueueSize);
				};
			}
		}

		// Token: 0x06002486 RID: 9350 RVA: 0x0008BC78 File Offset: 0x00089E78
		public void TrackEnteringQueue(IQueueQuotaMailItem mailItem, QueueQuotaResources resources)
		{
			this.queueQuota.TrackEnteringQueue(mailItem, resources);
		}

		// Token: 0x06002487 RID: 9351 RVA: 0x0008BC87 File Offset: 0x00089E87
		public void TrackExitingQueue(IQueueQuotaMailItem mailItem, QueueQuotaResources resources)
		{
			this.queueQuota.TrackExitingQueue(mailItem, resources);
		}

		// Token: 0x06002488 RID: 9352 RVA: 0x0008BC96 File Offset: 0x00089E96
		public bool IsOrganizationOverQuota(string accountForest, Guid externalOrganizationId, string sender, out string reason)
		{
			return this.queueQuota.IsOrganizationOverQuota(accountForest, externalOrganizationId, sender, out reason);
		}

		// Token: 0x06002489 RID: 9353 RVA: 0x0008BCA8 File Offset: 0x00089EA8
		public bool IsOrganizationOverWarning(string accountForest, Guid externalOrganizationId, string sender, QueueQuotaResources resource)
		{
			return this.queueQuota.IsOrganizationOverWarning(accountForest, externalOrganizationId, sender, resource);
		}

		// Token: 0x0600248A RID: 9354 RVA: 0x0008BCBA File Offset: 0x00089EBA
		public void TimedUpdate()
		{
			this.queueQuota.TimedUpdate();
		}

		// Token: 0x0600248B RID: 9355 RVA: 0x0008BCC7 File Offset: 0x00089EC7
		internal bool IsOverQuota(string accountForest, Guid externalOrganizationId, string sender, out string reason, out QueueQuotaEntity? reasonEntity, out QueueQuotaResources? reasonResource)
		{
			return this.queueQuota.IsOverQuota(accountForest, externalOrganizationId, sender, out reason, out reasonEntity, out reasonResource);
		}

		// Token: 0x0600248C RID: 9356 RVA: 0x0008BCDD File Offset: 0x00089EDD
		string IDiagnosable.GetDiagnosticComponentName()
		{
			return "QueueQuota";
		}

		// Token: 0x0600248D RID: 9357 RVA: 0x0008BCE4 File Offset: 0x00089EE4
		XElement IDiagnosable.GetDiagnosticInfo(DiagnosableParameters parameters)
		{
			XElement xelement = new XElement(((IDiagnosable)this).GetDiagnosticComponentName());
			xelement.SetAttributeValue("Version", "NewMetering");
			bool flag = parameters.Argument.IndexOf("verbose", StringComparison.OrdinalIgnoreCase) != -1;
			bool flag2 = parameters.Argument.IndexOf("Tenant:", StringComparison.OrdinalIgnoreCase) != -1;
			bool flag3 = parameters.Argument.IndexOf("Forest:", StringComparison.OrdinalIgnoreCase) != -1;
			bool flag4 = parameters.Argument.Equals("config", StringComparison.InvariantCultureIgnoreCase);
			bool flag5 = (!flag4 && !flag && !flag2) || parameters.Argument.IndexOf("help", StringComparison.OrdinalIgnoreCase) != -1;
			if (flag)
			{
				xelement.Add(this.queueQuota.GetDiagnosticInfo());
			}
			if (flag2)
			{
				string text = parameters.Argument.Substring("Tenant:".Length);
				Guid externalOrganizationId;
				if (Guid.TryParse(text, out externalOrganizationId))
				{
					xelement.Add(this.queueQuota.GetDiagnosticInfo(externalOrganizationId));
				}
				else
				{
					xelement.Add(new XElement("Error", string.Format("Invalid external organization id {0} passed as argument. Expecting a Guid.", text)));
				}
			}
			if (flag3)
			{
				string accountForest = parameters.Argument.Substring("Forest:".Length);
				xelement.Add(this.queueQuota.GetDiagnosticInfo(accountForest));
			}
			if (flag4)
			{
				xelement.Add(TransportAppConfig.GetDiagnosticInfoForType(this.config));
			}
			if (flag5)
			{
				xelement.Add(new XElement("help", string.Format("Supported arguments: verbose, help, config, {0}'tenantID e.g.1afa2e80-0251-4521-8086-039fb2f9d8d6', {1}'forestFQDN e.g. nampr03a001.prod.outlook.com'.", "Tenant:", "Forest:")));
			}
			return xelement;
		}

		// Token: 0x0600248E RID: 9358 RVA: 0x0008BE7E File Offset: 0x0008A07E
		public void Load()
		{
		}

		// Token: 0x0600248F RID: 9359 RVA: 0x0008BE80 File Offset: 0x0008A080
		public void Unload()
		{
		}

		// Token: 0x06002490 RID: 9360 RVA: 0x0008BE82 File Offset: 0x0008A082
		public string OnUnhandledException(Exception e)
		{
			return null;
		}

		// Token: 0x040012E6 RID: 4838
		private const string DiagnosticsComponentName = "QueueQuota";

		// Token: 0x040012E7 RID: 4839
		private readonly Func<DateTime> currentTimeProvider;

		// Token: 0x040012E8 RID: 4840
		private QueueQuotaImpl queueQuota;

		// Token: 0x040012E9 RID: 4841
		private IQueueQuotaConfig config;
	}
}
