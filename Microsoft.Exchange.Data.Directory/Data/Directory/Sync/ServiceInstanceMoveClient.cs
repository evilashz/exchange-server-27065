using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.Threading.Tasks;

namespace Microsoft.Exchange.Data.Directory.Sync
{
	// Token: 0x0200099F RID: 2463
	[DebuggerStepThrough]
	[GeneratedCode("System.ServiceModel", "4.0.0.0")]
	public class ServiceInstanceMoveClient : ClientBase<IServiceInstanceMove>, IServiceInstanceMove
	{
		// Token: 0x06007192 RID: 29074 RVA: 0x001785D7 File Offset: 0x001767D7
		public ServiceInstanceMoveClient()
		{
		}

		// Token: 0x06007193 RID: 29075 RVA: 0x001785DF File Offset: 0x001767DF
		public ServiceInstanceMoveClient(string endpointConfigurationName) : base(endpointConfigurationName)
		{
		}

		// Token: 0x06007194 RID: 29076 RVA: 0x001785E8 File Offset: 0x001767E8
		public ServiceInstanceMoveClient(string endpointConfigurationName, string remoteAddress) : base(endpointConfigurationName, remoteAddress)
		{
		}

		// Token: 0x06007195 RID: 29077 RVA: 0x001785F2 File Offset: 0x001767F2
		public ServiceInstanceMoveClient(string endpointConfigurationName, EndpointAddress remoteAddress) : base(endpointConfigurationName, remoteAddress)
		{
		}

		// Token: 0x06007196 RID: 29078 RVA: 0x001785FC File Offset: 0x001767FC
		public ServiceInstanceMoveClient(Binding binding, EndpointAddress remoteAddress) : base(binding, remoteAddress)
		{
		}

		// Token: 0x06007197 RID: 29079 RVA: 0x00178606 File Offset: 0x00176806
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		StartServiceInstanceMoveTaskResponse IServiceInstanceMove.StartServiceInstanceMoveTask(StartServiceInstanceMoveTaskRequest request)
		{
			return base.Channel.StartServiceInstanceMoveTask(request);
		}

		// Token: 0x06007198 RID: 29080 RVA: 0x00178614 File Offset: 0x00176814
		public ServiceInstanceMoveOperationResult StartServiceInstanceMoveTask(string contextId, string oldServiceInstance, string newServiceInstance, ServiceInstanceMoveOptions options)
		{
			StartServiceInstanceMoveTaskResponse startServiceInstanceMoveTaskResponse = ((IServiceInstanceMove)this).StartServiceInstanceMoveTask(new StartServiceInstanceMoveTaskRequest
			{
				contextId = contextId,
				oldServiceInstance = oldServiceInstance,
				newServiceInstance = newServiceInstance,
				options = options
			});
			return startServiceInstanceMoveTaskResponse.StartServiceInstanceMoveTaskResult;
		}

		// Token: 0x06007199 RID: 29081 RVA: 0x00178652 File Offset: 0x00176852
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		Task<StartServiceInstanceMoveTaskResponse> IServiceInstanceMove.StartServiceInstanceMoveTaskAsync(StartServiceInstanceMoveTaskRequest request)
		{
			return base.Channel.StartServiceInstanceMoveTaskAsync(request);
		}

		// Token: 0x0600719A RID: 29082 RVA: 0x00178660 File Offset: 0x00176860
		public Task<StartServiceInstanceMoveTaskResponse> StartServiceInstanceMoveTaskAsync(string contextId, string oldServiceInstance, string newServiceInstance, ServiceInstanceMoveOptions options)
		{
			return ((IServiceInstanceMove)this).StartServiceInstanceMoveTaskAsync(new StartServiceInstanceMoveTaskRequest
			{
				contextId = contextId,
				oldServiceInstance = oldServiceInstance,
				newServiceInstance = newServiceInstance,
				options = options
			});
		}

		// Token: 0x0600719B RID: 29083 RVA: 0x00178697 File Offset: 0x00176897
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		GetServiceInstanceMoveTaskStatusResponse IServiceInstanceMove.GetServiceInstanceMoveTaskStatus(GetServiceInstanceMoveTaskStatusRequest request)
		{
			return base.Channel.GetServiceInstanceMoveTaskStatus(request);
		}

		// Token: 0x0600719C RID: 29084 RVA: 0x001786A8 File Offset: 0x001768A8
		public ServiceInstanceMoveOperationResult GetServiceInstanceMoveTaskStatus(ServiceInstanceMoveTask serviceInstanceMoveTask, byte[] lastCookie)
		{
			GetServiceInstanceMoveTaskStatusResponse serviceInstanceMoveTaskStatus = ((IServiceInstanceMove)this).GetServiceInstanceMoveTaskStatus(new GetServiceInstanceMoveTaskStatusRequest
			{
				serviceInstanceMoveTask = serviceInstanceMoveTask,
				lastCookie = lastCookie
			});
			return serviceInstanceMoveTaskStatus.GetServiceInstanceMoveTaskStatusResult;
		}

		// Token: 0x0600719D RID: 29085 RVA: 0x001786D7 File Offset: 0x001768D7
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		Task<GetServiceInstanceMoveTaskStatusResponse> IServiceInstanceMove.GetServiceInstanceMoveTaskStatusAsync(GetServiceInstanceMoveTaskStatusRequest request)
		{
			return base.Channel.GetServiceInstanceMoveTaskStatusAsync(request);
		}

		// Token: 0x0600719E RID: 29086 RVA: 0x001786E8 File Offset: 0x001768E8
		public Task<GetServiceInstanceMoveTaskStatusResponse> GetServiceInstanceMoveTaskStatusAsync(ServiceInstanceMoveTask serviceInstanceMoveTask, byte[] lastCookie)
		{
			return ((IServiceInstanceMove)this).GetServiceInstanceMoveTaskStatusAsync(new GetServiceInstanceMoveTaskStatusRequest
			{
				serviceInstanceMoveTask = serviceInstanceMoveTask,
				lastCookie = lastCookie
			});
		}

		// Token: 0x0600719F RID: 29087 RVA: 0x00178710 File Offset: 0x00176910
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		FinalizeServiceInstanceMoveTaskResponse IServiceInstanceMove.FinalizeServiceInstanceMoveTask(FinalizeServiceInstanceMoveTaskRequest request)
		{
			return base.Channel.FinalizeServiceInstanceMoveTask(request);
		}

		// Token: 0x060071A0 RID: 29088 RVA: 0x00178720 File Offset: 0x00176920
		public ServiceInstanceMoveOperationResult FinalizeServiceInstanceMoveTask(ServiceInstanceMoveTask serviceInstanceMoveTask, byte[] lastCookie)
		{
			FinalizeServiceInstanceMoveTaskResponse finalizeServiceInstanceMoveTaskResponse = ((IServiceInstanceMove)this).FinalizeServiceInstanceMoveTask(new FinalizeServiceInstanceMoveTaskRequest
			{
				serviceInstanceMoveTask = serviceInstanceMoveTask,
				lastCookie = lastCookie
			});
			return finalizeServiceInstanceMoveTaskResponse.FinalizeServiceInstanceMoveTaskResult;
		}

		// Token: 0x060071A1 RID: 29089 RVA: 0x0017874F File Offset: 0x0017694F
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		Task<FinalizeServiceInstanceMoveTaskResponse> IServiceInstanceMove.FinalizeServiceInstanceMoveTaskAsync(FinalizeServiceInstanceMoveTaskRequest request)
		{
			return base.Channel.FinalizeServiceInstanceMoveTaskAsync(request);
		}

		// Token: 0x060071A2 RID: 29090 RVA: 0x00178760 File Offset: 0x00176960
		public Task<FinalizeServiceInstanceMoveTaskResponse> FinalizeServiceInstanceMoveTaskAsync(ServiceInstanceMoveTask serviceInstanceMoveTask, byte[] lastCookie)
		{
			return ((IServiceInstanceMove)this).FinalizeServiceInstanceMoveTaskAsync(new FinalizeServiceInstanceMoveTaskRequest
			{
				serviceInstanceMoveTask = serviceInstanceMoveTask,
				lastCookie = lastCookie
			});
		}

		// Token: 0x060071A3 RID: 29091 RVA: 0x00178788 File Offset: 0x00176988
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		DeleteServiceInstanceMoveTaskResponse IServiceInstanceMove.DeleteServiceInstanceMoveTask(DeleteServiceInstanceMoveTaskRequest request)
		{
			return base.Channel.DeleteServiceInstanceMoveTask(request);
		}

		// Token: 0x060071A4 RID: 29092 RVA: 0x00178798 File Offset: 0x00176998
		public ServiceInstanceMoveOperationResult DeleteServiceInstanceMoveTask(ServiceInstanceMoveTask serviceInstanceMoveTask)
		{
			DeleteServiceInstanceMoveTaskResponse deleteServiceInstanceMoveTaskResponse = ((IServiceInstanceMove)this).DeleteServiceInstanceMoveTask(new DeleteServiceInstanceMoveTaskRequest
			{
				serviceInstanceMoveTask = serviceInstanceMoveTask
			});
			return deleteServiceInstanceMoveTaskResponse.DeleteServiceInstanceMoveTaskResult;
		}

		// Token: 0x060071A5 RID: 29093 RVA: 0x001787C0 File Offset: 0x001769C0
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		Task<DeleteServiceInstanceMoveTaskResponse> IServiceInstanceMove.DeleteServiceInstanceMoveTaskAsync(DeleteServiceInstanceMoveTaskRequest request)
		{
			return base.Channel.DeleteServiceInstanceMoveTaskAsync(request);
		}

		// Token: 0x060071A6 RID: 29094 RVA: 0x001787D0 File Offset: 0x001769D0
		public Task<DeleteServiceInstanceMoveTaskResponse> DeleteServiceInstanceMoveTaskAsync(ServiceInstanceMoveTask serviceInstanceMoveTask)
		{
			return ((IServiceInstanceMove)this).DeleteServiceInstanceMoveTaskAsync(new DeleteServiceInstanceMoveTaskRequest
			{
				serviceInstanceMoveTask = serviceInstanceMoveTask
			});
		}

		// Token: 0x060071A7 RID: 29095 RVA: 0x001787F1 File Offset: 0x001769F1
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		RecoverServiceInstanceMoveTaskResponse IServiceInstanceMove.RecoverServiceInstanceMoveTask(RecoverServiceInstanceMoveTaskRequest request)
		{
			return base.Channel.RecoverServiceInstanceMoveTask(request);
		}

		// Token: 0x060071A8 RID: 29096 RVA: 0x00178800 File Offset: 0x00176A00
		public ServiceInstanceMoveOperationResult RecoverServiceInstanceMoveTask(ServiceInstanceMoveTask serviceInstanceMoveTask)
		{
			RecoverServiceInstanceMoveTaskResponse recoverServiceInstanceMoveTaskResponse = ((IServiceInstanceMove)this).RecoverServiceInstanceMoveTask(new RecoverServiceInstanceMoveTaskRequest
			{
				serviceInstanceMoveTask = serviceInstanceMoveTask
			});
			return recoverServiceInstanceMoveTaskResponse.RecoverServiceInstanceMoveTaskResult;
		}

		// Token: 0x060071A9 RID: 29097 RVA: 0x00178828 File Offset: 0x00176A28
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		Task<RecoverServiceInstanceMoveTaskResponse> IServiceInstanceMove.RecoverServiceInstanceMoveTaskAsync(RecoverServiceInstanceMoveTaskRequest request)
		{
			return base.Channel.RecoverServiceInstanceMoveTaskAsync(request);
		}

		// Token: 0x060071AA RID: 29098 RVA: 0x00178838 File Offset: 0x00176A38
		public Task<RecoverServiceInstanceMoveTaskResponse> RecoverServiceInstanceMoveTaskAsync(ServiceInstanceMoveTask serviceInstanceMoveTask)
		{
			return ((IServiceInstanceMove)this).RecoverServiceInstanceMoveTaskAsync(new RecoverServiceInstanceMoveTaskRequest
			{
				serviceInstanceMoveTask = serviceInstanceMoveTask
			});
		}
	}
}
