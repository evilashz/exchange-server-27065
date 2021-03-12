using System;

namespace Microsoft.Exchange.Services.OData.Model
{
	// Token: 0x02000EC8 RID: 3784
	[AllowedOAuthGrant("Mail.Read")]
	[AllowedOAuthGrant("Calendars.Read")]
	[AllowedOAuthGrant("Contacts.Read")]
	[AllowedOAuthGrant("Mail.Write")]
	[AllowedOAuthGrant("Mail.Send")]
	[AllowedOAuthGrant("Calendars.Write")]
	[AllowedOAuthGrant("user_impersonation")]
	[AllowedOAuthGrant("Contacts.Write")]
	internal class GetUserRequest : GetEntityRequest<User>
	{
		// Token: 0x06006243 RID: 25155 RVA: 0x001338FB File Offset: 0x00131AFB
		public GetUserRequest(ODataContext odataContext) : base(odataContext)
		{
		}

		// Token: 0x06006244 RID: 25156 RVA: 0x00133904 File Offset: 0x00131B04
		public override ODataCommand GetODataCommand()
		{
			return new GetUserCommand(this);
		}
	}
}
