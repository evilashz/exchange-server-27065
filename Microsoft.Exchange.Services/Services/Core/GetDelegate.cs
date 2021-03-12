using System;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Core
{
	// Token: 0x02000305 RID: 773
	internal sealed class GetDelegate : DelegateCommandBase<GetDelegateRequest>
	{
		// Token: 0x060015EC RID: 5612 RVA: 0x00071C10 File Offset: 0x0006FE10
		public GetDelegate(CallContext callContext, GetDelegateRequest request) : base(callContext, request)
		{
		}

		// Token: 0x060015ED RID: 5613 RVA: 0x00071C1C File Offset: 0x0006FE1C
		internal override IExchangeWebMethodResponse GetResponse()
		{
			DelegateUserResponseMessageType[] value = null;
			if (base.Results != null)
			{
				value = DelegateUtilities.BuildDelegateResponseFromResults(base.Results);
			}
			DeliverMeetingRequestsType deliverMeetingRequest = (this.xsoDelegateUsers != null) ? DelegateUtilities.ConvertToDeliverMeetingRequestType(this.xsoDelegateUsers.DelegateRuleType) : DeliverMeetingRequestsType.None;
			ServiceResult<DelegateUserResponseMessageType[]> serviceResult;
			if (base.PreExecuteSucceeded)
			{
				serviceResult = new ServiceResult<DelegateUserResponseMessageType[]>(value);
			}
			else
			{
				serviceResult = new ServiceResult<DelegateUserResponseMessageType[]>(base.Results[0].Code, null, base.Results[0].Error);
			}
			return new GetDelegateResponseMessage(serviceResult.Code, serviceResult.Error, serviceResult.Value, deliverMeetingRequest);
		}

		// Token: 0x060015EE RID: 5614 RVA: 0x00071CA5 File Offset: 0x0006FEA5
		internal override void PreExecuteCommand()
		{
			base.PreExecuteCommand();
			this.stepCount = ((base.Request.UserIds != null) ? base.Request.UserIds.Length : this.xsoDelegateUsers.Count);
		}

		// Token: 0x170002BB RID: 699
		// (get) Token: 0x060015EF RID: 5615 RVA: 0x00071CDA File Offset: 0x0006FEDA
		internal override int StepCount
		{
			get
			{
				return this.stepCount;
			}
		}

		// Token: 0x060015F0 RID: 5616 RVA: 0x00071CE4 File Offset: 0x0006FEE4
		internal override ServiceResult<DelegateUserType> Execute()
		{
			DelegateUser delegateUser;
			if (base.Request.UserIds != null)
			{
				UserId user = base.Request.UserIds[base.CurrentStep];
				delegateUser = DelegateUtilities.GetDelegateUser(user, this.xsoDelegateUsers, base.CallContext.ADRecipientSessionContext);
			}
			else
			{
				delegateUser = this.xsoDelegateUsers[base.CurrentStep];
			}
			DelegateUserType value = this.BuildDelegateResponse(delegateUser);
			return new ServiceResult<DelegateUserType>(value);
		}

		// Token: 0x060015F1 RID: 5617 RVA: 0x00071D50 File Offset: 0x0006FF50
		private DelegateUserType BuildDelegateResponse(DelegateUser delegateUser)
		{
			if ((delegateUser.Problems & DelegateProblems.NoADUser) == DelegateProblems.NoADUser)
			{
				throw new DelegateExceptionInvalidDelegateUser(CoreResources.IDs.ErrorDelegateNoUser);
			}
			if ((delegateUser.Problems & DelegateProblems.NoADPublicDelegate) == DelegateProblems.NoADPublicDelegate || (delegateUser.Problems & DelegateProblems.NoDelegateInfo) == DelegateProblems.NoDelegateInfo)
			{
				throw new DelegateExceptionInvalidDelegateUser((CoreResources.IDs)3438146603U);
			}
			return DelegateUtilities.BuildDelegateUserResponse(delegateUser, base.Request.IncludePermissions);
		}

		// Token: 0x04000EC6 RID: 3782
		private int stepCount;
	}
}
