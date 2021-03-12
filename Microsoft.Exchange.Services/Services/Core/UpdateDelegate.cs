using System;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Services;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Core
{
	// Token: 0x02000393 RID: 915
	internal sealed class UpdateDelegate : DelegateCommandBase<UpdateDelegateRequest>
	{
		// Token: 0x06001994 RID: 6548 RVA: 0x00091837 File Offset: 0x0008FA37
		public UpdateDelegate(CallContext callContext, UpdateDelegateRequest request) : base(callContext, request)
		{
		}

		// Token: 0x06001995 RID: 6549 RVA: 0x00091848 File Offset: 0x0008FA48
		internal override void PreExecuteCommand()
		{
			base.PreExecuteCommand();
			if (base.Request.DelegateUsers == null && base.Request.DeliverMeetingRequests == DeliverMeetingRequestsType.None)
			{
				throw new InvalidRequestException(CoreResources.IDs.MessageMissingUpdateDelegateRequestInformation);
			}
			this.totalDelegateUsers = ((base.Request.DelegateUsers != null) ? base.Request.DelegateUsers.Length : 0);
			this.delegateUserCollectionHandler = new DelegateUserCollectionHandler(base.GetMailboxIdentityMailboxSession(), base.CallContext.ADRecipientSessionContext);
		}

		// Token: 0x06001996 RID: 6550 RVA: 0x000918C4 File Offset: 0x0008FAC4
		internal override IExchangeWebMethodResponse GetResponse()
		{
			ServiceResult<DelegateUserResponseMessageType[]> serviceResult;
			if (base.PreExecuteSucceeded)
			{
				DelegateUserResponseMessageType[] value = null;
				if (this.totalDelegateUsers > 0)
				{
					value = DelegateUtilities.BuildDelegateResponseFromResults(base.Results);
				}
				serviceResult = new ServiceResult<DelegateUserResponseMessageType[]>(value);
			}
			else
			{
				serviceResult = new ServiceResult<DelegateUserResponseMessageType[]>(base.Results[0].Code, null, base.Results[0].Error);
			}
			return new UpdateDelegateResponseMessage(serviceResult.Code, serviceResult.Error, serviceResult.Value);
		}

		// Token: 0x17000307 RID: 775
		// (get) Token: 0x06001997 RID: 6551 RVA: 0x00091931 File Offset: 0x0008FB31
		internal override int StepCount
		{
			get
			{
				return this.totalDelegateUsers;
			}
		}

		// Token: 0x06001998 RID: 6552 RVA: 0x0009193C File Offset: 0x0008FB3C
		internal override void PostExecuteCommand()
		{
			if (base.Request.DeliverMeetingRequests != DeliverMeetingRequestsType.None)
			{
				this.delegateUserCollectionHandler.SetDelegateOptions(base.Request.DeliverMeetingRequests);
			}
			if (this.saveDelegateUsers || this.totalDelegateUsers == 0)
			{
				DelegateUserCollectionSaveResult delegateUserCollectionSaveResult = this.delegateUserCollectionHandler.SaveDelegate(false);
				if (delegateUserCollectionSaveResult.Problems.Count > 0)
				{
					if (ExTraceGlobals.UpdateDelegateCallTracer.IsTraceEnabled(TraceType.ErrorTrace))
					{
						ExTraceGlobals.UpdateDelegateCallTracer.TraceError<string>((long)this.GetHashCode(), "Failed to Save delegates due to the following problems: {0}", DelegateUtilities.BuildErrorStringFromProblems(delegateUserCollectionSaveResult.Problems));
					}
					throw new DelegateSaveFailedException(CoreResources.IDs.ErrorUpdateDelegatesFailed);
				}
			}
		}

		// Token: 0x06001999 RID: 6553 RVA: 0x000919D0 File Offset: 0x0008FBD0
		internal override ServiceResult<DelegateUserType> Execute()
		{
			DelegateUserType delegateUser = base.Request.DelegateUsers[base.CurrentStep];
			DelegateUser delegateUser2 = this.delegateUserCollectionHandler.UpdateDelegate(delegateUser);
			this.saveDelegateUsers = true;
			DelegateUserType value = DelegateUtilities.BuildDelegateUserResponse(delegateUser2, false);
			return new ServiceResult<DelegateUserType>(value);
		}

		// Token: 0x04001124 RID: 4388
		private bool saveDelegateUsers;

		// Token: 0x04001125 RID: 4389
		private int totalDelegateUsers = -1;

		// Token: 0x04001126 RID: 4390
		private DelegateUserCollectionHandler delegateUserCollectionHandler;
	}
}
