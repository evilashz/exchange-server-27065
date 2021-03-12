using System;
using System.Collections.Generic;
using System.Threading;
using Microsoft.Exchange.PushNotifications;
using Microsoft.Exchange.PushNotifications.Client;
using Microsoft.Office.Datacenter.ActiveMonitoring;
using Microsoft.Office.Datacenter.WorkerTaskFramework;

namespace Microsoft.Exchange.Monitoring.ActiveMonitoring.PushNotifications
{
	// Token: 0x02000415 RID: 1045
	public class AzureHubCreationProbe : ProbeWorkItem
	{
		// Token: 0x06001AB9 RID: 6841 RVA: 0x000931D4 File Offset: 0x000913D4
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

		// Token: 0x06001ABA RID: 6842 RVA: 0x00093270 File Offset: 0x00091470
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
			using (AzureHubCreationServiceProxy azureHubCreationServiceProxy = new AzureHubCreationServiceProxy(null))
			{
				azureHubCreationServiceProxy.EndCreateHub(azureHubCreationServiceProxy.BeginCreateHub(AzureHubDefinition.CreateMonitoringHubDefinition(this.tenantId, this.targetAppId), null, null));
			}
		}

		// Token: 0x06001ABB RID: 6843 RVA: 0x00093330 File Offset: 0x00091530
		internal override IEnumerable<PropertyInformation> GetSubstitutePropertyInformation()
		{
			return new List<PropertyInformation>();
		}

		// Token: 0x0400123E RID: 4670
		public const string TargetAppIdProperty = "TargetAppId";

		// Token: 0x0400123F RID: 4671
		public const string TenantIdProperty = "TenantId";

		// Token: 0x04001240 RID: 4672
		protected string targetAppId;

		// Token: 0x04001241 RID: 4673
		protected string tenantId;
	}
}
