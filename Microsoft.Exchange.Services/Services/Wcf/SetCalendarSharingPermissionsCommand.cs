using System;
using Microsoft.Exchange.Services.Core.Types;
using Microsoft.Exchange.Services.Wcf.Types;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x0200096E RID: 2414
	internal sealed class SetCalendarSharingPermissionsCommand : ServiceCommand<CalendarActionResponse>
	{
		// Token: 0x06004551 RID: 17745 RVA: 0x000F2E39 File Offset: 0x000F1039
		public SetCalendarSharingPermissionsCommand(CallContext callContext, SetCalendarSharingPermissionsRequest request) : base(callContext)
		{
			this.Request = request;
		}

		// Token: 0x17000FA3 RID: 4003
		// (get) Token: 0x06004552 RID: 17746 RVA: 0x000F2E49 File Offset: 0x000F1049
		// (set) Token: 0x06004553 RID: 17747 RVA: 0x000F2E51 File Offset: 0x000F1051
		private SetCalendarSharingPermissionsRequest Request { get; set; }

		// Token: 0x06004554 RID: 17748 RVA: 0x000F2E5C File Offset: 0x000F105C
		protected override CalendarActionResponse InternalExecute()
		{
			SetCalendarSharingPermissions.TraceDebug(this.GetHashCode(), "Validating Request", new object[0]);
			this.Request.ValidateRequest();
			return new SetCalendarSharingPermissions(base.CallContext.SessionCache.GetMailboxIdentityMailboxSession(), this.Request, base.CallContext.AccessingPrincipal.ObjectId, base.CallContext.ADRecipientSessionContext, this.Request.CalendarStoreId, base.CallContext.AccessingPrincipal).Execute();
		}
	}
}
