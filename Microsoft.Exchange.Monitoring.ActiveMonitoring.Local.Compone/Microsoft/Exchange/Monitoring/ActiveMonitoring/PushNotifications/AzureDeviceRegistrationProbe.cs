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
	// Token: 0x02000414 RID: 1044
	public class AzureDeviceRegistrationProbe : ProbeWorkItem
	{
		// Token: 0x06001AB5 RID: 6837 RVA: 0x0009300C File Offset: 0x0009120C
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
			if (propertyBag.ContainsKey("TenantId"))
			{
				probeDefinition.Attributes["TenantId"] = propertyBag["TenantId"].ToString();
				return;
			}
			throw new ArgumentException("Please specify value for TenantId");
		}

		// Token: 0x06001AB6 RID: 6838 RVA: 0x000930A8 File Offset: 0x000912A8
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
			MonitoringMailboxNotificationFactory monitoringMailboxNotificationFactory = PushNotificationsDiscovery.PublisherConfiguration.CreateMonitoringNotificationFactory(new Dictionary<PushNotificationPlatform, IMonitoringMailboxNotificationRecipientFactory>
			{
				{
					PushNotificationPlatform.Azure,
					AzureNotificationFactory.Default
				}
			});
			string azureTag = Guid.NewGuid().ToString();
			string monitoringDeviceToken = monitoringMailboxNotificationFactory.GetMonitoringDeviceToken(base.Result.MachineName, this.targetAppId);
			using (AzureDeviceRegistrationServiceProxy azureDeviceRegistrationServiceProxy = new AzureDeviceRegistrationServiceProxy(null))
			{
				azureDeviceRegistrationServiceProxy.EndDeviceRegistration(azureDeviceRegistrationServiceProxy.BeginDeviceRegistration(AzureDeviceRegistrationInfo.CreateMonitoringDeviceRegistrationInfo(monitoringDeviceToken, azureTag, this.targetAppId, this.tenantId, null), null, null));
			}
		}

		// Token: 0x06001AB7 RID: 6839 RVA: 0x000931B8 File Offset: 0x000913B8
		internal override IEnumerable<PropertyInformation> GetSubstitutePropertyInformation()
		{
			return new List<PropertyInformation>();
		}

		// Token: 0x0400123A RID: 4666
		public const string TargetAppIdProperty = "TargetAppId";

		// Token: 0x0400123B RID: 4667
		public const string TenantIdProperty = "TenantId";

		// Token: 0x0400123C RID: 4668
		protected string targetAppId;

		// Token: 0x0400123D RID: 4669
		protected string tenantId;
	}
}
