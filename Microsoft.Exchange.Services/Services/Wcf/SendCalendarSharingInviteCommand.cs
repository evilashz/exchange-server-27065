using System;
using Microsoft.Exchange.Diagnostics.Components.Services;
using Microsoft.Exchange.Services.Core.Types;
using Microsoft.Exchange.Services.Wcf.Types;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x02000966 RID: 2406
	internal sealed class SendCalendarSharingInviteCommand : ServiceCommand<CalendarShareInviteResponse>
	{
		// Token: 0x06004526 RID: 17702 RVA: 0x000F1A26 File Offset: 0x000EFC26
		public SendCalendarSharingInviteCommand(CallContext callContext, CalendarShareInviteRequest calendarShareInviteRequest) : base(callContext)
		{
			this.Request = calendarShareInviteRequest;
		}

		// Token: 0x17000F9F RID: 3999
		// (get) Token: 0x06004527 RID: 17703 RVA: 0x000F1A36 File Offset: 0x000EFC36
		// (set) Token: 0x06004528 RID: 17704 RVA: 0x000F1A3E File Offset: 0x000EFC3E
		private CalendarShareInviteRequest Request { get; set; }

		// Token: 0x06004529 RID: 17705 RVA: 0x000F1A48 File Offset: 0x000EFC48
		protected override CalendarShareInviteResponse InternalExecute()
		{
			this.TraceDebug("Validating Request", new object[0]);
			this.Request.ValidateRequest(base.CallContext.SessionCache.GetMailboxIdentityMailboxSession(), base.CallContext.ADRecipientSessionContext);
			return new SendCalendarSharingInvite(base.CallContext.SessionCache.GetMailboxIdentityMailboxSession(), this.Request, base.CallContext.AccessingPrincipal, base.CallContext.ADRecipientSessionContext).Execute();
		}

		// Token: 0x0600452A RID: 17706 RVA: 0x000F1AC2 File Offset: 0x000EFCC2
		private void TraceDebug(string messageFormat, params object[] args)
		{
			ExTraceGlobals.GetCalendarSharingRecipientInfoCallTracer.TraceDebug((long)this.GetHashCode(), messageFormat, args);
		}
	}
}
