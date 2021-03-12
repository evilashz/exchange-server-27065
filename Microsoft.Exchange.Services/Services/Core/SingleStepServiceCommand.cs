using System;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Core
{
	// Token: 0x0200028E RID: 654
	internal abstract class SingleStepServiceCommand<RequestType, SingleItemType> : BaseStepServiceCommand<RequestType, SingleItemType> where RequestType : BaseRequest
	{
		// Token: 0x06001152 RID: 4434 RVA: 0x00053E55 File Offset: 0x00052055
		public SingleStepServiceCommand(CallContext callContext, RequestType request) : base(callContext, request)
		{
		}

		// Token: 0x1700022D RID: 557
		// (get) Token: 0x06001153 RID: 4435 RVA: 0x00053E5F File Offset: 0x0005205F
		// (set) Token: 0x06001154 RID: 4436 RVA: 0x00053E67 File Offset: 0x00052067
		internal ServiceResult<SingleItemType> Result { get; set; }

		// Token: 0x1700022E RID: 558
		// (get) Token: 0x06001155 RID: 4437 RVA: 0x00053E70 File Offset: 0x00052070
		internal override int StepCount
		{
			get
			{
				return 1;
			}
		}

		// Token: 0x06001156 RID: 4438 RVA: 0x00053E74 File Offset: 0x00052074
		internal override void SetCurrentStepResult(ServiceResult<SingleItemType> result)
		{
			this.Result = result;
			base.CheckAndThrowFaultExceptionOnRequestLevelErrors<SingleItemType>(new ServiceResult<SingleItemType>[]
			{
				result
			});
		}
	}
}
