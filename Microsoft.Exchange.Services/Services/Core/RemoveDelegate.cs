using System;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Services;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Core
{
	// Token: 0x02000357 RID: 855
	internal sealed class RemoveDelegate : DelegateCommandBase<RemoveDelegateRequest>
	{
		// Token: 0x0600180D RID: 6157 RVA: 0x000816EE File Offset: 0x0007F8EE
		public RemoveDelegate(CallContext callContext, RemoveDelegateRequest request) : base(callContext, request)
		{
		}

		// Token: 0x0600180E RID: 6158 RVA: 0x000816F8 File Offset: 0x0007F8F8
		internal override void PreExecuteCommand()
		{
			base.PreExecuteCommand();
			this.delegateUserCollectionHandler = new DelegateUserCollectionHandler(base.GetMailboxIdentityMailboxSession(), base.CallContext.ADRecipientSessionContext);
		}

		// Token: 0x0600180F RID: 6159 RVA: 0x0008171C File Offset: 0x0007F91C
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
			return new RemoveDelegateResponseMessage(serviceResult.Code, serviceResult.Error, serviceResult.Value);
		}

		// Token: 0x06001810 RID: 6160 RVA: 0x00081780 File Offset: 0x0007F980
		internal override void PostExecuteCommand()
		{
			if (this.saveDelegateUsers)
			{
				DelegateUserCollectionSaveResult delegateUserCollectionSaveResult = this.delegateUserCollectionHandler.SaveDelegate(false);
				if (delegateUserCollectionSaveResult.Problems.Count > 0)
				{
					if (ExTraceGlobals.RemoveDelegateCallTracer.IsTraceEnabled(TraceType.ErrorTrace))
					{
						ExTraceGlobals.RemoveDelegateCallTracer.TraceError<string>((long)this.GetHashCode(), "Failed to Save delegates due to the following problems: {0}", DelegateUtilities.BuildErrorStringFromProblems(delegateUserCollectionSaveResult.Problems));
					}
					throw new DelegateSaveFailedException((CoreResources.IDs)3763931121U);
				}
			}
		}

		// Token: 0x06001811 RID: 6161 RVA: 0x000817EC File Offset: 0x0007F9EC
		internal override ServiceResult<DelegateUserType> Execute()
		{
			UserId user = base.Request.UserIds[base.CurrentStep];
			this.delegateUserCollectionHandler.RemoveDelegate(user);
			this.saveDelegateUsers = true;
			return new ServiceResult<DelegateUserType>(null);
		}

		// Token: 0x170002E0 RID: 736
		// (get) Token: 0x06001812 RID: 6162 RVA: 0x00081825 File Offset: 0x0007FA25
		internal override int StepCount
		{
			get
			{
				return base.Request.UserIds.Length;
			}
		}

		// Token: 0x0400101E RID: 4126
		private bool saveDelegateUsers;

		// Token: 0x0400101F RID: 4127
		private DelegateUserCollectionHandler delegateUserCollectionHandler;
	}
}
