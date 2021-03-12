using System;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory.ResourceHealth;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Core
{
	// Token: 0x0200028D RID: 653
	internal abstract class BaseStepServiceCommand<RequestType, SingleItemType> : ServiceCommandBase where RequestType : BaseRequest
	{
		// Token: 0x06001148 RID: 4424 RVA: 0x00053D56 File Offset: 0x00051F56
		public BaseStepServiceCommand(CallContext callContext, RequestType request) : base(callContext)
		{
			this.Request = request;
		}

		// Token: 0x06001149 RID: 4425 RVA: 0x00053D68 File Offset: 0x00051F68
		internal override ResourceKey[] GetResources()
		{
			RequestType request = this.Request;
			return request.GetResources(base.CallContext, base.CurrentStep);
		}

		// Token: 0x1700022C RID: 556
		// (get) Token: 0x0600114A RID: 4426 RVA: 0x00053D95 File Offset: 0x00051F95
		// (set) Token: 0x0600114B RID: 4427 RVA: 0x00053D9D File Offset: 0x00051F9D
		internal RequestType Request { get; private set; }

		// Token: 0x0600114C RID: 4428 RVA: 0x00053DA6 File Offset: 0x00051FA6
		internal void CheckAndThrowFaultExceptionOnRequestLevelErrors<TResult>(params ServiceResult<TResult>[] results)
		{
			if (base.CallContext.IsOwa)
			{
				return;
			}
			ServiceErrors.CheckAndThrowFaultExceptionOnRequestLevelErrors<TResult>(results);
		}

		// Token: 0x0600114D RID: 4429 RVA: 0x00053DC4 File Offset: 0x00051FC4
		internal override void InternalExecuteStep(out bool isBatchStopResponse)
		{
			try
			{
				ServiceResult<SingleItemType> serviceResult = ExceptionHandler<SingleItemType>.Execute((int step) => this.Execute(), base.CurrentStep);
				this.SetCurrentStepResult(serviceResult);
				isBatchStopResponse = serviceResult.IsStopBatchProcessingError;
			}
			finally
			{
				base.LogRequestTraces();
			}
		}

		// Token: 0x0600114E RID: 4430 RVA: 0x00053E18 File Offset: 0x00052018
		internal override void InternalCancelStep(LocalizedException exception, out bool isBatchStopResponse)
		{
			if (exception != null)
			{
				RequestDetailsLoggerBase<RequestDetailsLogger>.SafeLogRequestException(base.CallContext.ProtocolLog, exception, "BaseStepSvcCmd_InternalCancel");
			}
			ServiceResult<SingleItemType> serviceResult = ExceptionHandler<SingleItemType>.GetServiceResult<SingleItemType>(exception, null);
			this.SetCurrentStepResult(serviceResult);
			isBatchStopResponse = serviceResult.IsStopBatchProcessingError;
		}

		// Token: 0x0600114F RID: 4431
		internal abstract void SetCurrentStepResult(ServiceResult<SingleItemType> result);

		// Token: 0x06001150 RID: 4432
		internal abstract ServiceResult<SingleItemType> Execute();
	}
}
