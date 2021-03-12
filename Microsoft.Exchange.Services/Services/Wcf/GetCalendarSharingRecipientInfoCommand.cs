using System;
using Microsoft.Exchange.Diagnostics.Components.Services;
using Microsoft.Exchange.Services.Core.Types;
using Microsoft.Exchange.Services.Wcf.Types;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x0200095A RID: 2394
	internal sealed class GetCalendarSharingRecipientInfoCommand : ServiceCommand<GetCalendarSharingRecipientInfoResponse>
	{
		// Token: 0x060044F8 RID: 17656 RVA: 0x000EFEF8 File Offset: 0x000EE0F8
		public GetCalendarSharingRecipientInfoCommand(CallContext callContext, GetCalendarSharingRecipientInfoRequest request) : base(callContext)
		{
			this.Request = request;
		}

		// Token: 0x17000F9C RID: 3996
		// (get) Token: 0x060044F9 RID: 17657 RVA: 0x000EFF08 File Offset: 0x000EE108
		// (set) Token: 0x060044FA RID: 17658 RVA: 0x000EFF10 File Offset: 0x000EE110
		private GetCalendarSharingRecipientInfoRequest Request { get; set; }

		// Token: 0x060044FB RID: 17659 RVA: 0x000EFF1C File Offset: 0x000EE11C
		protected override GetCalendarSharingRecipientInfoResponse InternalExecute()
		{
			this.TraceDebug("Validating Request", new object[0]);
			this.Request.ValidateRequest(base.CallContext.ADRecipientSessionContext);
			return new GetCalendarSharingRecipientInfo(base.CallContext.SessionCache.GetMailboxIdentityMailboxSession(), this.Request, base.CallContext.AccessingPrincipal).Execute();
		}

		// Token: 0x060044FC RID: 17660 RVA: 0x000EFF7B File Offset: 0x000EE17B
		private void TraceDebug(string messageFormat, params object[] args)
		{
			ExTraceGlobals.GetCalendarSharingRecipientInfoCallTracer.TraceDebug((long)this.GetHashCode(), messageFormat, args);
		}
	}
}
