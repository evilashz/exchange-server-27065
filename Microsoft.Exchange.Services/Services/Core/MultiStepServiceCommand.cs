using System;
using System.Linq;
using System.Xml;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Core
{
	// Token: 0x02000292 RID: 658
	internal abstract class MultiStepServiceCommand<RequestType, SingleItemType> : BaseStepServiceCommand<RequestType, SingleItemType> where RequestType : BaseRequest
	{
		// Token: 0x0600118F RID: 4495 RVA: 0x000555DF File Offset: 0x000537DF
		public MultiStepServiceCommand(CallContext callContext, RequestType request) : base(callContext, request)
		{
		}

		// Token: 0x17000232 RID: 562
		// (get) Token: 0x06001190 RID: 4496 RVA: 0x000555E9 File Offset: 0x000537E9
		// (set) Token: 0x06001191 RID: 4497 RVA: 0x000555F1 File Offset: 0x000537F1
		private bool HaveLoggedErrorResponse { get; set; }

		// Token: 0x17000233 RID: 563
		// (get) Token: 0x06001192 RID: 4498 RVA: 0x000555FA File Offset: 0x000537FA
		// (set) Token: 0x06001193 RID: 4499 RVA: 0x00055602 File Offset: 0x00053802
		internal ServiceResult<SingleItemType>[] Results { get; private set; }

		// Token: 0x06001194 RID: 4500 RVA: 0x00055668 File Offset: 0x00053868
		internal override bool InternalPreExecute()
		{
			bool success = false;
			ServiceResult<SingleItemType>[] results = ExceptionHandler<SingleItemType>.Execute(delegate()
			{
				FaultInjection.GenerateFault((FaultInjection.LIDs)3716558141U);
				this.PreExecuteCommand();
				if (this.StepCount > 0)
				{
					this.Results = new ServiceResult<SingleItemType>[this.StepCount];
				}
				success = true;
				return null;
			});
			if (!success)
			{
				this.Results = results;
				base.CheckAndThrowFaultExceptionOnRequestLevelErrors<SingleItemType>(results);
			}
			return success;
		}

		// Token: 0x06001195 RID: 4501 RVA: 0x000556B7 File Offset: 0x000538B7
		internal virtual void PreExecuteCommand()
		{
		}

		// Token: 0x06001196 RID: 4502 RVA: 0x000556D8 File Offset: 0x000538D8
		internal override void InternalPostExecute()
		{
			bool success = false;
			ServiceResult<SingleItemType>[] results = ExceptionHandler<SingleItemType>.Execute(delegate()
			{
				this.PostExecuteCommand();
				success = true;
				return null;
			});
			if (!success)
			{
				this.Results = results;
			}
			base.CheckAndThrowFaultExceptionOnRequestLevelErrors<SingleItemType>(this.Results);
		}

		// Token: 0x06001197 RID: 4503 RVA: 0x00055726 File Offset: 0x00053926
		internal virtual void PostExecuteCommand()
		{
		}

		// Token: 0x06001198 RID: 4504 RVA: 0x00055728 File Offset: 0x00053928
		internal override void SetCurrentStepResult(ServiceResult<SingleItemType> result)
		{
			this.Results[base.CurrentStep] = result;
		}

		// Token: 0x06001199 RID: 4505 RVA: 0x00055738 File Offset: 0x00053938
		internal void LogServiceResultErrorAsAppropriate(ServiceResultCode resultCode, ServiceError serviceError)
		{
			if (!this.HaveLoggedErrorResponse && resultCode != ServiceResultCode.Success && serviceError != null)
			{
				base.CallContext.ProtocolLog.Set(ServiceCommonMetadata.ErrorCode, serviceError.MessageKey);
				this.HaveLoggedErrorResponse = true;
			}
		}

		// Token: 0x0600119A RID: 4506 RVA: 0x0005578D File Offset: 0x0005398D
		internal bool LogItemId()
		{
			return base.CurrentStep < 5 && base.CallContext != null && !string.IsNullOrEmpty(base.CallContext.UserAgent) && Global.WellKnownClientsForBackgroundSync.Any((string x) => base.CallContext.UserAgent.IndexOf(x, StringComparison.OrdinalIgnoreCase) >= 0);
		}

		// Token: 0x0600119B RID: 4507 RVA: 0x000557CD File Offset: 0x000539CD
		internal void LogServiceResultErrorAsAppropriate(ServiceResult<XmlNode> result)
		{
			this.LogServiceResultErrorAsAppropriate(result.Code, result.Error);
		}

		// Token: 0x04000CB6 RID: 3254
		private const int ItemIdLoggingBatchLimit = 5;
	}
}
