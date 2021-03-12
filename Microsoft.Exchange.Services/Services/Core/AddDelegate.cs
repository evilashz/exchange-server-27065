using System;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Services;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Core
{
	// Token: 0x02000294 RID: 660
	internal sealed class AddDelegate : DelegateCommandBase<AddDelegateRequest>
	{
		// Token: 0x060011A1 RID: 4513 RVA: 0x00055855 File Offset: 0x00053A55
		public AddDelegate(CallContext callContext, AddDelegateRequest request) : base(callContext, request)
		{
		}

		// Token: 0x17000234 RID: 564
		// (get) Token: 0x060011A2 RID: 4514 RVA: 0x0005585F File Offset: 0x00053A5F
		internal override int StepCount
		{
			get
			{
				return base.Request.DelegateUsers.Length;
			}
		}

		// Token: 0x060011A3 RID: 4515 RVA: 0x0005586E File Offset: 0x00053A6E
		internal override void PreExecuteCommand()
		{
			base.PreExecuteCommand();
			this.delegateUserCollectionHandler = new DelegateUserCollectionHandler(base.GetMailboxIdentityMailboxSession(), base.CallContext.ADRecipientSessionContext);
		}

		// Token: 0x060011A4 RID: 4516 RVA: 0x00055894 File Offset: 0x00053A94
		internal override ServiceResult<DelegateUserType> Execute()
		{
			DelegateUserType delegateUserType = base.Request.DelegateUsers[base.CurrentStep];
			DelegateUser delegateUser = this.delegateUserCollectionHandler.AddDelegate(delegateUserType);
			delegateUserType = DelegateUtilities.BuildDelegateUserResponse(delegateUser, false);
			this.saveDelegateUsers = true;
			return new ServiceResult<DelegateUserType>(delegateUserType);
		}

		// Token: 0x060011A5 RID: 4517 RVA: 0x000558D8 File Offset: 0x00053AD8
		internal override void PostExecuteCommand()
		{
			if (base.Request.DeliverMeetingRequests != DeliverMeetingRequestsType.None)
			{
				this.delegateUserCollectionHandler.SetDelegateOptions(base.Request.DeliverMeetingRequests);
			}
			if (this.saveDelegateUsers)
			{
				DelegateUserCollectionSaveResult delegateUserCollectionSaveResult = this.delegateUserCollectionHandler.SaveDelegate(false);
				if (delegateUserCollectionSaveResult.Problems.Count > 0)
				{
					if (ExTraceGlobals.AddDelegateCallTracer.IsTraceEnabled(TraceType.ErrorTrace))
					{
						ExTraceGlobals.AddDelegateCallTracer.TraceError<string>((long)this.GetHashCode(), "Failed to Save delegates due to the following problems: {0}", DelegateUtilities.BuildErrorStringFromProblems(delegateUserCollectionSaveResult.Problems));
					}
					throw new DelegateSaveFailedException(CoreResources.IDs.ErrorAddDelegatesFailed);
				}
			}
		}

		// Token: 0x060011A6 RID: 4518 RVA: 0x00055964 File Offset: 0x00053B64
		internal override IExchangeWebMethodResponse GetResponse()
		{
			ServiceResult<DelegateUserResponseMessageType[]> serviceResult;
			if (base.PreExecuteSucceeded)
			{
				DelegateUserResponseMessageType[] value = DelegateUtilities.BuildDelegateResponseFromResults(base.Results);
				serviceResult = new ServiceResult<DelegateUserResponseMessageType[]>(value);
			}
			else
			{
				serviceResult = new ServiceResult<DelegateUserResponseMessageType[]>(base.Results[0].Code, null, base.Results[0].Error);
			}
			return new AddDelegateResponseMessage(serviceResult.Code, serviceResult.Error, serviceResult.Value);
		}

		// Token: 0x04000CBA RID: 3258
		private bool saveDelegateUsers;

		// Token: 0x04000CBB RID: 3259
		private DelegateUserCollectionHandler delegateUserCollectionHandler;
	}
}
