using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.ServiceModel;
using Microsoft.Office.CompliancePolicy.Monitor;
using Microsoft.Office.CompliancePolicy.PolicyConfiguration;

namespace Microsoft.Office.CompliancePolicy.PolicySync
{
	// Token: 0x02000120 RID: 288
	internal sealed class InitializationSubWorkItem : SubWorkItemBase
	{
		// Token: 0x06000833 RID: 2099 RVA: 0x00018E7D File Offset: 0x0001707D
		internal InitializationSubWorkItem(SyncJob syncJob) : base(syncJob, null)
		{
		}

		// Token: 0x06000834 RID: 2100 RVA: 0x000191E0 File Offset: 0x000173E0
		public override void Execute()
		{
			base.SyncJob.LogProvider.LogOneEntry("UnifiedPolicySyncAgent", base.SyncJob.CurrentWorkItem.TenantContext.TenantId.ToString(), base.SyncJob.CurrentWorkItem.ExternalIdentity, ExecutionLog.EventType.Information, "Unified Policy Sync Initialization SubWorkItem Begin", "Unified Policy Sync Initialization SubWorkItem Begin", null, new KeyValuePair<string, object>[0]);
			SyncWorkItem workItem = base.SyncJob.CurrentWorkItem;
			workItem.TryCount++;
			base.SyncJob.MonitorEventTracker.TrackLatencyWrapper(LatencyType.TenantInfo, delegate()
			{
				this.SyncJob.TenantInfoProvider = this.SyncJob.SyncAgentContext.TenantInfoProviderFactory.CreateTenantInfoProvider(workItem.TenantContext);
				this.SyncJob.TenantInfo = this.SyncJob.TenantInfoProvider.Load();
				if (this.SyncJob.TenantInfo == null || this.SyncJob.TenantInfo.SyncInfoTable == null || !this.SyncJob.TenantInfo.SyncInfoTable.Any<KeyValuePair<ConfigurationObjectType, SyncInfo>>())
				{
					this.SyncJob.TenantInfo = new TenantInfo(workItem.TenantContext.TenantId, workItem.SyncSvcUrl, new Dictionary<ConfigurationObjectType, SyncInfo>
					{
						{
							ConfigurationObjectType.Policy,
							new SyncInfo()
						},
						{
							ConfigurationObjectType.Rule,
							new SyncInfo()
						},
						{
							ConfigurationObjectType.Binding,
							new SyncInfo()
						},
						{
							ConfigurationObjectType.Association,
							new SyncInfo()
						}
					});
				}
				else
				{
					this.SyncJob.TenantInfo.TenantId = workItem.TenantContext.TenantId;
					this.SyncJob.TenantInfo.SyncSvcUrl = workItem.SyncSvcUrl;
				}
				this.SyncJob.TenantInfo.LastAttemptedSyncUTC = new DateTime?(DateTime.UtcNow);
				this.SyncJob.TenantInfoProvider.Save(this.SyncJob.TenantInfo);
			});
			base.SyncJob.MonitorEventTracker.TrackLatencyWrapper(LatencyType.Initialization, delegate()
			{
				if (string.IsNullOrWhiteSpace(this.SyncJob.SyncAgentContext.SyncAgentConfig.CertificateSubject))
				{
					ICredentials credential = this.SyncJob.SyncAgentContext.CredentialFactory.GetCredential(workItem.TenantContext);
					this.SyncJob.SyncSvcClient = this.SyncJob.SyncAgentContext.SyncSvcClientFactory.CreatePolicySyncWebserviceClient(new EndpointAddress(workItem.SyncSvcUrl), credential, this.SyncJob.SyncAgentContext.SyncAgentConfig.PartnerName);
				}
				else
				{
					X509Certificate2 credential2 = this.SyncJob.SyncAgentContext.CredentialFactory.GetCredential(this.SyncJob.SyncAgentContext.SyncAgentConfig.CertificateSubject);
					this.SyncJob.SyncSvcClient = this.SyncJob.SyncAgentContext.SyncSvcClientFactory.CreatePolicySyncWebserviceClient(new EndpointAddress(workItem.SyncSvcUrl), credential2, this.SyncJob.SyncAgentContext.SyncAgentConfig.PartnerName);
				}
				this.SyncJob.PolicyConfigProvider = this.SyncJob.SyncAgentContext.PolicyConfigProviderFactory.CreateForSyncEngine(workItem.TenantContext.TenantId, workItem.TenantContext.TenantContextInfo, this.SyncJob.SyncAgentContext.SyncAgentConfig.EnablePolicyApplication, this.SyncJob.LogProvider);
			});
			base.SyncJob.LogProvider.LogOneEntry("UnifiedPolicySyncAgent", base.SyncJob.CurrentWorkItem.TenantContext.TenantId.ToString(), base.SyncJob.CurrentWorkItem.ExternalIdentity, ExecutionLog.EventType.Information, "Unified Policy Sync Initialization SubWorkItem End", "Unified Policy Sync Initialization SubWorkItem End", null, new KeyValuePair<string, object>[0]);
		}

		// Token: 0x06000835 RID: 2101 RVA: 0x0001930C File Offset: 0x0001750C
		public override void BeginExecute(Action<SubWorkItemBase> callback)
		{
			this.Execute();
			callback(this);
		}
	}
}
