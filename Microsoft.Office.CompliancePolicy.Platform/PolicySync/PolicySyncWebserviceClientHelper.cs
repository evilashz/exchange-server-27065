using System;
using System.Linq;
using Microsoft.Office.CompliancePolicy.Monitor;
using Microsoft.Office.CompliancePolicy.PolicyConfiguration;

namespace Microsoft.Office.CompliancePolicy.PolicySync
{
	// Token: 0x0200012A RID: 298
	internal static class PolicySyncWebserviceClientHelper
	{
		// Token: 0x0600086E RID: 2158 RVA: 0x0001A6B0 File Offset: 0x000188B0
		public static PolicyChange GetSingleTenantChanges(this IPolicySyncWebserviceClient syncSvcClient, TenantCookie tenantCookie, SyncMonitorEventTracker monitorTracker)
		{
			PolicyChange result = null;
			int deltaObjectNumber = 0;
			monitorTracker.TrackLatencyWrapper(LatencyType.FfoWsCall, tenantCookie.ObjectType, ref deltaObjectNumber, delegate()
			{
				result = syncSvcClient.GetSingleTenantChanges(tenantCookie);
				deltaObjectNumber = PolicySyncWebserviceClientHelper.CalculateObjectNumber(result, tenantCookie.ObjectType);
			}, true, false);
			return result;
		}

		// Token: 0x0600086F RID: 2159 RVA: 0x0001A738 File Offset: 0x00018938
		public static IAsyncResult BeginGetSingleTenantChanges(this IPolicySyncWebserviceClient syncSvcClient, TenantCookie tenantCookie, AsyncCallback userCallback, object stateObject, SyncMonitorEventTracker monitorTracker)
		{
			IAsyncResult result = null;
			monitorTracker.TrackLatencyWrapper(LatencyType.FfoWsCall, tenantCookie.ObjectType, delegate()
			{
				result = syncSvcClient.BeginGetSingleTenantChanges(tenantCookie, userCallback, stateObject);
			}, false);
			return result;
		}

		// Token: 0x06000870 RID: 2160 RVA: 0x0001A7CC File Offset: 0x000189CC
		public static PolicyChange EndGetSingleTenantChanges(this IPolicySyncWebserviceClient syncSvcClient, IAsyncResult asyncResult, SyncMonitorEventTracker monitorTracker, ConfigurationObjectType objectType)
		{
			PolicyChange result = null;
			int deltaObjectNumber = 0;
			monitorTracker.TrackLatencyWrapper(LatencyType.FfoWsCall, objectType, ref deltaObjectNumber, delegate()
			{
				result = syncSvcClient.EndGetSingleTenantChanges(asyncResult);
				deltaObjectNumber = PolicySyncWebserviceClientHelper.CalculateObjectNumber(result, objectType);
			}, true, true);
			return result;
		}

		// Token: 0x06000871 RID: 2161 RVA: 0x0001A888 File Offset: 0x00018A88
		public static PolicyConfigurationBase GetObject(this IPolicySyncWebserviceClient syncSvcClient, SyncCallerContext callerContext, Guid tenantId, ConfigurationObjectType objectType, Guid objectId, bool includeDeletedObjects, SyncMonitorEventTracker monitorTracker)
		{
			PolicyConfigurationBase result = null;
			int deltaObjectNumber = 0;
			monitorTracker.TrackLatencyWrapper(LatencyType.FfoWsCall, objectType, ref deltaObjectNumber, delegate()
			{
				result = syncSvcClient.GetObject(callerContext, tenantId, objectType, objectId, includeDeletedObjects);
				deltaObjectNumber = PolicySyncWebserviceClientHelper.CalculateObjectNumber(result, objectType);
			}, true, false);
			return result;
		}

		// Token: 0x06000872 RID: 2162 RVA: 0x0001A950 File Offset: 0x00018B50
		public static IAsyncResult BeginGetObject(this IPolicySyncWebserviceClient syncSvcClient, SyncCallerContext callerContext, Guid tenantId, ConfigurationObjectType objectType, Guid objectId, bool includeDeletedObjects, AsyncCallback userCallback, object stateObject, SyncMonitorEventTracker monitorTracker)
		{
			IAsyncResult result = null;
			monitorTracker.TrackLatencyWrapper(LatencyType.FfoWsCall, objectType, delegate()
			{
				result = syncSvcClient.BeginGetObject(callerContext, tenantId, objectType, objectId, includeDeletedObjects, userCallback, stateObject);
			}, false);
			return result;
		}

		// Token: 0x06000873 RID: 2163 RVA: 0x0001AA00 File Offset: 0x00018C00
		public static PolicyConfigurationBase EndGetObject(this IPolicySyncWebserviceClient syncSvcClient, IAsyncResult asyncResult, SyncMonitorEventTracker monitorTracker, ConfigurationObjectType objectType)
		{
			PolicyConfigurationBase result = null;
			int deltaObjectNumber = 0;
			monitorTracker.TrackLatencyWrapper(LatencyType.FfoWsCall, objectType, ref deltaObjectNumber, delegate()
			{
				result = syncSvcClient.EndGetObject(asyncResult);
				deltaObjectNumber = PolicySyncWebserviceClientHelper.CalculateObjectNumber(result, objectType);
			}, true, true);
			return result;
		}

		// Token: 0x06000874 RID: 2164 RVA: 0x0001AA60 File Offset: 0x00018C60
		private static int CalculateObjectNumber(PolicyConfigurationBase deltaObject, ConfigurationObjectType objectType)
		{
			int result = 0;
			if (deltaObject != null)
			{
				if (objectType != ConfigurationObjectType.Binding)
				{
					result = 1;
				}
				else
				{
					BindingConfiguration bindingConfiguration = (BindingConfiguration)deltaObject;
					if (bindingConfiguration.AppliedScopes != null && bindingConfiguration.AppliedScopes.Changed)
					{
						int num = (bindingConfiguration.AppliedScopes.RemovedValues == null) ? 0 : bindingConfiguration.AppliedScopes.RemovedValues.Count<ScopeConfiguration>();
						int num2 = (bindingConfiguration.AppliedScopes.ChangedValues == null) ? 0 : bindingConfiguration.AppliedScopes.ChangedValues.Count<ScopeConfiguration>();
						result = num + num2;
					}
				}
			}
			return result;
		}

		// Token: 0x06000875 RID: 2165 RVA: 0x0001AAE0 File Offset: 0x00018CE0
		private static int CalculateObjectNumber(PolicyChange deltaObjects, ConfigurationObjectType objectType)
		{
			int num = 0;
			if (deltaObjects != null && deltaObjects.Changes != null)
			{
				if (objectType != ConfigurationObjectType.Binding)
				{
					num = deltaObjects.Changes.Count<PolicyConfigurationBase>();
				}
				else
				{
					foreach (PolicyConfigurationBase deltaObject in deltaObjects.Changes)
					{
						num += PolicySyncWebserviceClientHelper.CalculateObjectNumber(deltaObject, objectType);
					}
				}
			}
			return num;
		}
	}
}
