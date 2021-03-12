using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.Threading.Tasks;

namespace Microsoft.Exchange.Data.Directory.Sync
{
	// Token: 0x02000991 RID: 2449
	[GeneratedCode("System.ServiceModel", "4.0.0.0")]
	[DebuggerStepThrough]
	public class FederatedServiceOnboardingClient : ClientBase<IFederatedServiceOnboarding>, IFederatedServiceOnboarding
	{
		// Token: 0x06007157 RID: 29015 RVA: 0x001781FD File Offset: 0x001763FD
		public FederatedServiceOnboardingClient()
		{
		}

		// Token: 0x06007158 RID: 29016 RVA: 0x00178205 File Offset: 0x00176405
		public FederatedServiceOnboardingClient(string endpointConfigurationName) : base(endpointConfigurationName)
		{
		}

		// Token: 0x06007159 RID: 29017 RVA: 0x0017820E File Offset: 0x0017640E
		public FederatedServiceOnboardingClient(string endpointConfigurationName, string remoteAddress) : base(endpointConfigurationName, remoteAddress)
		{
		}

		// Token: 0x0600715A RID: 29018 RVA: 0x00178218 File Offset: 0x00176418
		public FederatedServiceOnboardingClient(string endpointConfigurationName, EndpointAddress remoteAddress) : base(endpointConfigurationName, remoteAddress)
		{
		}

		// Token: 0x0600715B RID: 29019 RVA: 0x00178222 File Offset: 0x00176422
		public FederatedServiceOnboardingClient(Binding binding, EndpointAddress remoteAddress) : base(binding, remoteAddress)
		{
		}

		// Token: 0x0600715C RID: 29020 RVA: 0x0017822C File Offset: 0x0017642C
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		GetServiceInstanceMapResponse IFederatedServiceOnboarding.GetServiceInstanceMap(GetServiceInstanceMapRequest request)
		{
			return base.Channel.GetServiceInstanceMap(request);
		}

		// Token: 0x0600715D RID: 29021 RVA: 0x0017823C File Offset: 0x0017643C
		public ServiceInstanceMapValue GetServiceInstanceMap(string serviceType)
		{
			GetServiceInstanceMapResponse serviceInstanceMap = ((IFederatedServiceOnboarding)this).GetServiceInstanceMap(new GetServiceInstanceMapRequest
			{
				serviceType = serviceType
			});
			return serviceInstanceMap.GetServiceInstanceMapResult;
		}

		// Token: 0x0600715E RID: 29022 RVA: 0x00178264 File Offset: 0x00176464
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		Task<GetServiceInstanceMapResponse> IFederatedServiceOnboarding.GetServiceInstanceMapAsync(GetServiceInstanceMapRequest request)
		{
			return base.Channel.GetServiceInstanceMapAsync(request);
		}

		// Token: 0x0600715F RID: 29023 RVA: 0x00178274 File Offset: 0x00176474
		public Task<GetServiceInstanceMapResponse> GetServiceInstanceMapAsync(string serviceType)
		{
			return ((IFederatedServiceOnboarding)this).GetServiceInstanceMapAsync(new GetServiceInstanceMapRequest
			{
				serviceType = serviceType
			});
		}

		// Token: 0x06007160 RID: 29024 RVA: 0x00178295 File Offset: 0x00176495
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		SetServiceInstanceMapResponse IFederatedServiceOnboarding.SetServiceInstanceMap(SetServiceInstanceMapRequest request)
		{
			return base.Channel.SetServiceInstanceMap(request);
		}

		// Token: 0x06007161 RID: 29025 RVA: 0x001782A4 File Offset: 0x001764A4
		public ResultCode SetServiceInstanceMap(string serviceType, ServiceInstanceMapValue serviceInstanceMap)
		{
			SetServiceInstanceMapResponse setServiceInstanceMapResponse = ((IFederatedServiceOnboarding)this).SetServiceInstanceMap(new SetServiceInstanceMapRequest
			{
				serviceType = serviceType,
				serviceInstanceMap = serviceInstanceMap
			});
			return setServiceInstanceMapResponse.SetServiceInstanceMapResult;
		}

		// Token: 0x06007162 RID: 29026 RVA: 0x001782D3 File Offset: 0x001764D3
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		Task<SetServiceInstanceMapResponse> IFederatedServiceOnboarding.SetServiceInstanceMapAsync(SetServiceInstanceMapRequest request)
		{
			return base.Channel.SetServiceInstanceMapAsync(request);
		}

		// Token: 0x06007163 RID: 29027 RVA: 0x001782E4 File Offset: 0x001764E4
		public Task<SetServiceInstanceMapResponse> SetServiceInstanceMapAsync(string serviceType, ServiceInstanceMapValue serviceInstanceMap)
		{
			return ((IFederatedServiceOnboarding)this).SetServiceInstanceMapAsync(new SetServiceInstanceMapRequest
			{
				serviceType = serviceType,
				serviceInstanceMap = serviceInstanceMap
			});
		}

		// Token: 0x06007164 RID: 29028 RVA: 0x0017830C File Offset: 0x0017650C
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		GetServiceInstanceInfoResponse IFederatedServiceOnboarding.GetServiceInstanceInfo(GetServiceInstanceInfoRequest request)
		{
			return base.Channel.GetServiceInstanceInfo(request);
		}

		// Token: 0x06007165 RID: 29029 RVA: 0x0017831C File Offset: 0x0017651C
		public ServiceInstanceInfoValue GetServiceInstanceInfo(string serviceInstance)
		{
			GetServiceInstanceInfoResponse serviceInstanceInfo = ((IFederatedServiceOnboarding)this).GetServiceInstanceInfo(new GetServiceInstanceInfoRequest
			{
				serviceInstance = serviceInstance
			});
			return serviceInstanceInfo.GetServiceInstanceInfoResult;
		}

		// Token: 0x06007166 RID: 29030 RVA: 0x00178344 File Offset: 0x00176544
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		Task<GetServiceInstanceInfoResponse> IFederatedServiceOnboarding.GetServiceInstanceInfoAsync(GetServiceInstanceInfoRequest request)
		{
			return base.Channel.GetServiceInstanceInfoAsync(request);
		}

		// Token: 0x06007167 RID: 29031 RVA: 0x00178354 File Offset: 0x00176554
		public Task<GetServiceInstanceInfoResponse> GetServiceInstanceInfoAsync(string serviceInstance)
		{
			return ((IFederatedServiceOnboarding)this).GetServiceInstanceInfoAsync(new GetServiceInstanceInfoRequest
			{
				serviceInstance = serviceInstance
			});
		}

		// Token: 0x06007168 RID: 29032 RVA: 0x00178375 File Offset: 0x00176575
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		SetServiceInstanceInfoResponse IFederatedServiceOnboarding.SetServiceInstanceInfo(SetServiceInstanceInfoRequest request)
		{
			return base.Channel.SetServiceInstanceInfo(request);
		}

		// Token: 0x06007169 RID: 29033 RVA: 0x00178384 File Offset: 0x00176584
		public ResultCode SetServiceInstanceInfo(string serviceInstance, ServiceInstanceInfoValue serviceInstanceInfo)
		{
			SetServiceInstanceInfoResponse setServiceInstanceInfoResponse = ((IFederatedServiceOnboarding)this).SetServiceInstanceInfo(new SetServiceInstanceInfoRequest
			{
				serviceInstance = serviceInstance,
				serviceInstanceInfo = serviceInstanceInfo
			});
			return setServiceInstanceInfoResponse.SetServiceInstanceInfoResult;
		}

		// Token: 0x0600716A RID: 29034 RVA: 0x001783B3 File Offset: 0x001765B3
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		Task<SetServiceInstanceInfoResponse> IFederatedServiceOnboarding.SetServiceInstanceInfoAsync(SetServiceInstanceInfoRequest request)
		{
			return base.Channel.SetServiceInstanceInfoAsync(request);
		}

		// Token: 0x0600716B RID: 29035 RVA: 0x001783C4 File Offset: 0x001765C4
		public Task<SetServiceInstanceInfoResponse> SetServiceInstanceInfoAsync(string serviceInstance, ServiceInstanceInfoValue serviceInstanceInfo)
		{
			return ((IFederatedServiceOnboarding)this).SetServiceInstanceInfoAsync(new SetServiceInstanceInfoRequest
			{
				serviceInstance = serviceInstance,
				serviceInstanceInfo = serviceInstanceInfo
			});
		}

		// Token: 0x0600716C RID: 29036 RVA: 0x001783EC File Offset: 0x001765EC
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		QueueEduFaultinResponse IFederatedServiceOnboarding.QueueEduFaultin(QueueEduFaultinRequest request)
		{
			return base.Channel.QueueEduFaultin(request);
		}

		// Token: 0x0600716D RID: 29037 RVA: 0x001783FC File Offset: 0x001765FC
		public ExchangeFaultinStatus QueueEduFaultin(string serviceInstance, string contextId)
		{
			QueueEduFaultinResponse queueEduFaultinResponse = ((IFederatedServiceOnboarding)this).QueueEduFaultin(new QueueEduFaultinRequest
			{
				serviceInstance = serviceInstance,
				contextId = contextId
			});
			return queueEduFaultinResponse.QueueEduFaultinResult;
		}

		// Token: 0x0600716E RID: 29038 RVA: 0x0017842B File Offset: 0x0017662B
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		Task<QueueEduFaultinResponse> IFederatedServiceOnboarding.QueueEduFaultinAsync(QueueEduFaultinRequest request)
		{
			return base.Channel.QueueEduFaultinAsync(request);
		}

		// Token: 0x0600716F RID: 29039 RVA: 0x0017843C File Offset: 0x0017663C
		public Task<QueueEduFaultinResponse> QueueEduFaultinAsync(string serviceInstance, string contextId)
		{
			return ((IFederatedServiceOnboarding)this).QueueEduFaultinAsync(new QueueEduFaultinRequest
			{
				serviceInstance = serviceInstance,
				contextId = contextId
			});
		}

		// Token: 0x06007170 RID: 29040 RVA: 0x00178464 File Offset: 0x00176664
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		GetEduFaultinStatusResponse IFederatedServiceOnboarding.GetEduFaultinStatus(GetEduFaultinStatusRequest request)
		{
			return base.Channel.GetEduFaultinStatus(request);
		}

		// Token: 0x06007171 RID: 29041 RVA: 0x00178474 File Offset: 0x00176674
		public ExchangeFaultinStatus[] GetEduFaultinStatus(string[] contextIds)
		{
			GetEduFaultinStatusResponse eduFaultinStatus = ((IFederatedServiceOnboarding)this).GetEduFaultinStatus(new GetEduFaultinStatusRequest
			{
				contextIds = contextIds
			});
			return eduFaultinStatus.GetEduFaultinStatusResult;
		}

		// Token: 0x06007172 RID: 29042 RVA: 0x0017849C File Offset: 0x0017669C
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		Task<GetEduFaultinStatusResponse> IFederatedServiceOnboarding.GetEduFaultinStatusAsync(GetEduFaultinStatusRequest request)
		{
			return base.Channel.GetEduFaultinStatusAsync(request);
		}

		// Token: 0x06007173 RID: 29043 RVA: 0x001784AC File Offset: 0x001766AC
		public Task<GetEduFaultinStatusResponse> GetEduFaultinStatusAsync(string[] contextIds)
		{
			return ((IFederatedServiceOnboarding)this).GetEduFaultinStatusAsync(new GetEduFaultinStatusRequest
			{
				contextIds = contextIds
			});
		}
	}
}
