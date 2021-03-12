using System;
using Microsoft.Exchange.Services.Core.Types;
using Microsoft.Exchange.Services.Wcf.Types;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x02000958 RID: 2392
	internal sealed class GetCalendarSharingPermissionsCommand : ServiceCommand<GetCalendarSharingPermissionsResponse>
	{
		// Token: 0x060044ED RID: 17645 RVA: 0x000EF93C File Offset: 0x000EDB3C
		public GetCalendarSharingPermissionsCommand(CallContext callContext, GetCalendarSharingPermissionsRequest request) : base(callContext)
		{
			this.Request = request;
		}

		// Token: 0x17000F99 RID: 3993
		// (get) Token: 0x060044EE RID: 17646 RVA: 0x000EF94C File Offset: 0x000EDB4C
		// (set) Token: 0x060044EF RID: 17647 RVA: 0x000EF954 File Offset: 0x000EDB54
		private GetCalendarSharingPermissionsRequest Request { get; set; }

		// Token: 0x060044F0 RID: 17648 RVA: 0x000EF960 File Offset: 0x000EDB60
		protected override GetCalendarSharingPermissionsResponse InternalExecute()
		{
			GetCalendarSharingPermissions.TraceDebug(this.GetHashCode(), "Validating Request", new object[0]);
			this.Request.ValidateRequest();
			return new GetCalendarSharingPermissions(base.CallContext.SessionCache.GetMailboxIdentityMailboxSession(), this.Request.CalendarStoreId, base.CallContext.AccessingPrincipal.MailboxInfo.OrganizationId, base.CallContext.ADRecipientSessionContext).Execute();
		}

		// Token: 0x0400281E RID: 10270
		private const string AnonymousDomainName = "Anonymous";
	}
}
