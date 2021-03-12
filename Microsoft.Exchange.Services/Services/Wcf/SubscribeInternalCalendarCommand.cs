using System;
using Microsoft.Exchange.Diagnostics.Components.Services;
using Microsoft.Exchange.Services.Core.Types;
using Microsoft.Exchange.Services.Wcf.Types;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x02000973 RID: 2419
	internal sealed class SubscribeInternalCalendarCommand : ServiceCommand<CalendarActionFolderIdResponse>
	{
		// Token: 0x06004577 RID: 17783 RVA: 0x000F3D8D File Offset: 0x000F1F8D
		public SubscribeInternalCalendarCommand(CallContext callContext, SubscribeInternalCalendarRequest request) : base(callContext)
		{
			this.request = request;
			this.request.ValidateRequest();
		}

		// Token: 0x06004578 RID: 17784 RVA: 0x000F3DA8 File Offset: 0x000F1FA8
		protected override CalendarActionFolderIdResponse InternalExecute()
		{
			return new SubscribeInternalCalendar(base.CallContext.SessionCache.GetMailboxIdentityMailboxSession(), this.request.Recipient, this.request.GroupId).Execute();
		}

		// Token: 0x06004579 RID: 17785 RVA: 0x000F3DDA File Offset: 0x000F1FDA
		public static void TraceError(object source, string message)
		{
			SubscribeInternalCalendarCommand.TraceError(source, null, message);
		}

		// Token: 0x0600457A RID: 17786 RVA: 0x000F3DE4 File Offset: 0x000F1FE4
		public static void TraceError(object source, Exception ex, string message)
		{
			if (ex == null)
			{
				ExTraceGlobals.SubscribeInternalCalendarCallTracer.TraceError((long)source.GetHashCode(), message);
				return;
			}
			ExTraceGlobals.SubscribeInternalCalendarCallTracer.TraceError<string, string>((long)source.GetHashCode(), message, ex.Message, ex.StackTrace);
		}

		// Token: 0x0600457B RID: 17787 RVA: 0x000F3E1A File Offset: 0x000F201A
		public static void TraceDebug(object source, string message)
		{
			ExTraceGlobals.SubscribeInternalCalendarCallTracer.TraceDebug((long)source.GetHashCode(), message);
		}

		// Token: 0x04002868 RID: 10344
		private readonly SubscribeInternalCalendarRequest request;
	}
}
