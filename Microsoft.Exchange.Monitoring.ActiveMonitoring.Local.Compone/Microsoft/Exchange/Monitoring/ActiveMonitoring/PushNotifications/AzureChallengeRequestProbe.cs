using System;
using System.Collections.Generic;
using System.Threading;
using Microsoft.Exchange.Common;
using Microsoft.Exchange.PushNotifications;
using Microsoft.Exchange.PushNotifications.Client;
using Microsoft.Exchange.PushNotifications.Publishers;
using Microsoft.Office.Datacenter.ActiveMonitoring;
using Microsoft.Office.Datacenter.WorkerTaskFramework;

namespace Microsoft.Exchange.Monitoring.ActiveMonitoring.PushNotifications
{
	// Token: 0x02000413 RID: 1043
	public class AzureChallengeRequestProbe : ProbeWorkItem
	{
		// Token: 0x06001AB1 RID: 6833 RVA: 0x00092DB4 File Offset: 0x00090FB4
		public override void PopulateDefinition<Definition>(Definition definition, Dictionary<string, string> propertyBag)
		{
			ProbeDefinition probeDefinition = definition as ProbeDefinition;
			if (probeDefinition == null)
			{
				throw new ArgumentException("definition must be a ProbeDefinition");
			}
			if (!propertyBag.ContainsKey("TargetAppId"))
			{
				throw new ArgumentException("Please specify value for TargetAppIdMask");
			}
			probeDefinition.Attributes["TargetAppId"] = propertyBag["TargetAppId"].ToString();
			if (!propertyBag.ContainsKey("TenantId"))
			{
				throw new ArgumentException("Please specify value for TenantId");
			}
			probeDefinition.Attributes["TenantId"] = propertyBag["TenantId"].ToString();
			if (propertyBag.ContainsKey("AppPlatform"))
			{
				probeDefinition.Attributes["AppPlatform"] = propertyBag["AppPlatform"].ToString();
				return;
			}
			throw new ArgumentException("Please specify value for AppPlatform");
		}

		// Token: 0x06001AB2 RID: 6834 RVA: 0x00092E88 File Offset: 0x00091088
		protected override void DoWork(CancellationToken cancellationToken)
		{
			if (base.Definition.Attributes.ContainsKey("TargetAppId"))
			{
				this.targetAppId = base.Definition.Attributes["TargetAppId"].ToString();
			}
			if (base.Definition.Attributes.ContainsKey("TenantId"))
			{
				this.tenantId = base.Definition.Attributes["TenantId"].ToString();
			}
			if (base.Definition.Attributes.ContainsKey("AppPlatform"))
			{
				this.appPlatform = base.Definition.Attributes["AppPlatform"].ToString();
			}
			MonitoringMailboxNotificationFactory monitoringMailboxNotificationFactory = PushNotificationsDiscovery.PublisherConfiguration.CreateMonitoringNotificationFactory(new Dictionary<PushNotificationPlatform, IMonitoringMailboxNotificationRecipientFactory>
			{
				{
					PushNotificationPlatform.Azure,
					AzureNotificationFactory.Default
				}
			});
			string challenge = Guid.NewGuid().ToString();
			string monitoringDeviceToken = monitoringMailboxNotificationFactory.GetMonitoringDeviceToken(base.Result.MachineName, this.targetAppId);
			PushNotificationPlatform platform = (PushNotificationPlatform)Enum.Parse(typeof(PushNotificationPlatform), this.appPlatform, true);
			using (AzureChallengeRequestServiceProxy azureChallengeRequestServiceProxy = new AzureChallengeRequestServiceProxy(null))
			{
				azureChallengeRequestServiceProxy.EndChallengeRequest(azureChallengeRequestServiceProxy.BeginChallengeRequest(AzureChallengeRequestInfo.CreateMonitoringAzureChallengeRequestInfo(this.targetAppId, platform, monitoringDeviceToken, challenge, this.tenantId), null, null));
			}
		}

		// Token: 0x06001AB3 RID: 6835 RVA: 0x00092FF0 File Offset: 0x000911F0
		internal override IEnumerable<PropertyInformation> GetSubstitutePropertyInformation()
		{
			return new List<PropertyInformation>();
		}

		// Token: 0x04001234 RID: 4660
		public const string TargetAppIdProperty = "TargetAppId";

		// Token: 0x04001235 RID: 4661
		public const string TenantIdProperty = "TenantId";

		// Token: 0x04001236 RID: 4662
		public const string AppPlatformProperty = "AppPlatform";

		// Token: 0x04001237 RID: 4663
		protected string targetAppId;

		// Token: 0x04001238 RID: 4664
		protected string tenantId;

		// Token: 0x04001239 RID: 4665
		protected string appPlatform;
	}
}
