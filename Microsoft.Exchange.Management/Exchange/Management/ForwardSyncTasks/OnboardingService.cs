using System;
using Microsoft.Exchange.Data.Directory.Sync;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Management.ForwardSyncTasks
{
	// Token: 0x02000365 RID: 869
	public abstract class OnboardingService : DisposeTrackableBase
	{
		// Token: 0x06001E48 RID: 7752 RVA: 0x000838DF File Offset: 0x00081ADF
		protected OnboardingService()
		{
			this.InitializeSyncServiceProxy();
		}

		// Token: 0x06001E49 RID: 7753 RVA: 0x000838F0 File Offset: 0x00081AF0
		public ServiceInstanceMapValue GetServiceInstanceMap()
		{
			GetServiceInstanceMapRequest request = new GetServiceInstanceMapRequest("EXCHANGE");
			GetServiceInstanceMapResponse serviceInstanceMap = this.onboardingProxy.GetServiceInstanceMap(request);
			return serviceInstanceMap.GetServiceInstanceMapResult;
		}

		// Token: 0x06001E4A RID: 7754 RVA: 0x0008391C File Offset: 0x00081B1C
		public ResultCode SetServiceInstanceMap(ServiceInstanceMapValue serviceInstanceMap)
		{
			SetServiceInstanceMapRequest serviceInstanceMap2 = new SetServiceInstanceMapRequest("EXCHANGE", serviceInstanceMap);
			SetServiceInstanceMapResponse setServiceInstanceMapResponse = this.onboardingProxy.SetServiceInstanceMap(serviceInstanceMap2);
			return setServiceInstanceMapResponse.SetServiceInstanceMapResult;
		}

		// Token: 0x06001E4B RID: 7755 RVA: 0x00083948 File Offset: 0x00081B48
		public ServiceInstanceInfoValue GetServiceInstanceInfo(string serviceInstance)
		{
			GetServiceInstanceInfoRequest request = new GetServiceInstanceInfoRequest(serviceInstance);
			GetServiceInstanceInfoResponse serviceInstanceInfo = this.onboardingProxy.GetServiceInstanceInfo(request);
			return serviceInstanceInfo.GetServiceInstanceInfoResult;
		}

		// Token: 0x06001E4C RID: 7756 RVA: 0x00083970 File Offset: 0x00081B70
		public ResultCode SetServiceInstanceInfo(string serviceInstance, ServiceInstanceInfoValue serviceInstanceInfo)
		{
			SetServiceInstanceInfoRequest serviceInstanceInfo2 = new SetServiceInstanceInfoRequest(serviceInstance, serviceInstanceInfo);
			SetServiceInstanceInfoResponse setServiceInstanceInfoResponse = this.onboardingProxy.SetServiceInstanceInfo(serviceInstanceInfo2);
			return setServiceInstanceInfoResponse.SetServiceInstanceInfoResult;
		}

		// Token: 0x06001E4D RID: 7757 RVA: 0x00083998 File Offset: 0x00081B98
		protected override DisposeTracker InternalGetDisposeTracker()
		{
			return DisposeTracker.Get<OnboardingService>(this);
		}

		// Token: 0x06001E4E RID: 7758 RVA: 0x000839A0 File Offset: 0x00081BA0
		protected override void InternalDispose(bool disposing)
		{
			if (disposing)
			{
				this.DisposeSyncServiceProxy();
			}
		}

		// Token: 0x06001E4F RID: 7759
		protected abstract IFederatedServiceOnboarding CreateService();

		// Token: 0x06001E50 RID: 7760 RVA: 0x000839AB File Offset: 0x00081BAB
		private void InitializeSyncServiceProxy()
		{
			if (this.onboardingProxy == null)
			{
				this.onboardingProxy = this.CreateService();
			}
		}

		// Token: 0x06001E51 RID: 7761 RVA: 0x000839C4 File Offset: 0x00081BC4
		private void DisposeSyncServiceProxy()
		{
			if (this.onboardingProxy != null)
			{
				IDisposable disposable = this.onboardingProxy as IDisposable;
				if (disposable != null)
				{
					disposable.Dispose();
				}
				this.onboardingProxy = null;
			}
		}

		// Token: 0x0400191A RID: 6426
		private const string ServiceType = "EXCHANGE";

		// Token: 0x0400191B RID: 6427
		private IFederatedServiceOnboarding onboardingProxy;
	}
}
