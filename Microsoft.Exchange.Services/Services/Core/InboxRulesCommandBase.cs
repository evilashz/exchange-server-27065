using System;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Core
{
	// Token: 0x02000310 RID: 784
	internal abstract class InboxRulesCommandBase<RequestType, ResponseType> : SingleStepServiceCommand<RequestType, ResponseType> where RequestType : InboxRulesRequest where ResponseType : ResponseMessage, new()
	{
		// Token: 0x170002BF RID: 703
		// (get) Token: 0x0600161F RID: 5663 RVA: 0x0007291C File Offset: 0x00070B1C
		// (set) Token: 0x06001620 RID: 5664 RVA: 0x00072924 File Offset: 0x00070B24
		private protected int HashCode { protected get; private set; }

		// Token: 0x06001621 RID: 5665 RVA: 0x0007292D File Offset: 0x00070B2D
		public InboxRulesCommandBase(CallContext callContext, Trace tracer, FaultInjection.LIDs faultInjectionLid, RequestType request) : base(callContext, request)
		{
			this.tracer = tracer;
			this.faultInjectionLid = faultInjectionLid;
			this.HashCode = this.GetHashCode();
		}

		// Token: 0x06001622 RID: 5666 RVA: 0x00072952 File Offset: 0x00070B52
		internal override IExchangeWebMethodResponse GetResponse()
		{
			ResponseType responseType;
			if ((responseType = base.Result.Value) == null)
			{
				responseType = this.CreateErrorResponse(base.Result);
			}
			return responseType;
		}

		// Token: 0x06001623 RID: 5667
		protected abstract ResponseType Execute(Rules serverRules, MailboxSession mailboxSession);

		// Token: 0x06001624 RID: 5668 RVA: 0x0007297C File Offset: 0x00070B7C
		internal override ServiceResult<ResponseType> Execute()
		{
			this.tracer.TraceDebug((long)this.HashCode, "InboxRulesCommandBase.Execute() called");
			this.ValidateRequest();
			MailboxSession mailboxSessionByMailboxId = base.CallContext.SessionCache.GetMailboxSessionByMailboxId(this.mailboxId);
			FaultInjection.GenerateFault(this.faultInjectionLid);
			Rules inboxRules = mailboxSessionByMailboxId.InboxRules;
			ResponseType value = default(ResponseType);
			try
			{
				this.tracer.TraceDebug<int>((long)this.HashCode, "RulesCount={0}", inboxRules.Count);
				value = this.Execute(inboxRules, mailboxSessionByMailboxId);
				this.tracer.TraceDebug((long)this.HashCode, "InboxRulesCommandBase.Execute() ended");
			}
			finally
			{
				inboxRules.Nuke();
			}
			return new ServiceResult<ResponseType>(value);
		}

		// Token: 0x06001625 RID: 5669 RVA: 0x00072A34 File Offset: 0x00070C34
		private void ValidateRequest()
		{
			if (this.tracer.IsTraceEnabled(TraceType.DebugTrace))
			{
				RequestType request = base.Request;
				string name = request.GetType().Name;
				this.tracer.TraceDebug<string>((long)this.HashCode, "BaseRequestType={0}", name);
			}
			ServiceCommandBase.ThrowIfNull(base.Request, "this.Request", "InboxRulesCommandBase.ValidateBaseRequest");
			if (base.CallContext is ExternalCallContext)
			{
				this.tracer.TraceDebug((long)this.HashCode, "ExternalCallContext detected. InboxRulesCommandBase does not support ExternalCallContext, AccessDenied error is returned.");
				throw new ServiceAccessDeniedException();
			}
			RequestType request2 = base.Request;
			if (request2.MailboxSmtpAddress != null)
			{
				Trace trace = this.tracer;
				long id = (long)this.HashCode;
				string formatString = "MailboxSmtpAddress {0} specified";
				RequestType request3 = base.Request;
				trace.TraceDebug<string>(id, formatString, request3.MailboxSmtpAddress);
				RequestType request4 = base.Request;
				if (!SmtpAddress.IsValidSmtpAddress(request4.MailboxSmtpAddress))
				{
					Trace trace2 = this.tracer;
					long id2 = (long)this.HashCode;
					string formatString2 = "Invalid MailboxSmtpAddress {0} specified.";
					RequestType request5 = base.Request;
					trace2.TraceError<string>(id2, formatString2, request5.MailboxSmtpAddress);
					Enum messageId = (CoreResources.IDs)2489326695U;
					RequestType request6 = base.Request;
					throw new NonExistentMailboxException(messageId, request6.MailboxSmtpAddress);
				}
				RequestType request7 = base.Request;
				this.mailboxId = new MailboxId(request7.MailboxSmtpAddress);
				return;
			}
			else
			{
				if (base.CallContext.AccessingPrincipal == null)
				{
					this.tracer.TraceError((long)this.HashCode, "CallContext.AccessingPrincipal=null, InboxRulesCommandBaseRequest.MailboxSmtpAddress not specified. NonExistentMailbox error returned.");
					throw new NonExistentMailboxException((CoreResources.IDs)2489326695U, (base.CallContext.EffectiveCaller == null) ? string.Empty : base.CallContext.EffectiveCaller.PrimarySmtpAddress);
				}
				this.TraceCallContext(base.CallContext);
				this.mailboxId = new MailboxId(base.CallContext.AccessingPrincipal.MailboxInfo.MailboxGuid);
				return;
			}
		}

		// Token: 0x06001626 RID: 5670 RVA: 0x00072C1C File Offset: 0x00070E1C
		private ResponseType CreateErrorResponse(ServiceResult<ResponseType> serviceResult)
		{
			ResponseType result = Activator.CreateInstance<ResponseType>();
			result.Initialize(serviceResult.Code, serviceResult.Error);
			return result;
		}

		// Token: 0x06001627 RID: 5671 RVA: 0x00072C4C File Offset: 0x00070E4C
		private void TraceCallContext(CallContext callContext)
		{
			this.tracer.TraceDebug((long)this.HashCode, "CallContext:PrimarySmtpAddress={0};MbxGuid={1};MbxType={2};MdbGuid={3};DatabaseLocationInfo={4}", new object[]
			{
				callContext.GetEffectiveAccessingSmtpAddress(),
				callContext.AccessingPrincipal.MailboxInfo.MailboxGuid,
				callContext.AccessingPrincipal.RecipientTypeDetails,
				callContext.AccessingPrincipal.MailboxInfo.GetDatabaseGuid(),
				callContext.AccessingPrincipal.MailboxInfo.Location
			});
		}

		// Token: 0x04000EDD RID: 3805
		private Trace tracer;

		// Token: 0x04000EDE RID: 3806
		private MailboxId mailboxId;

		// Token: 0x04000EDF RID: 3807
		private FaultInjection.LIDs faultInjectionLid;
	}
}
