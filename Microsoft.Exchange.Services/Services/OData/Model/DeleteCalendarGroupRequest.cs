using System;

namespace Microsoft.Exchange.Services.OData.Model
{
	// Token: 0x02000F5C RID: 3932
	[AllowedOAuthGrant("Calendars.Write")]
	internal class DeleteCalendarGroupRequest : DeleteEntityRequest<CalendarGroup>
	{
		// Token: 0x0600638C RID: 25484 RVA: 0x001364E3 File Offset: 0x001346E3
		public DeleteCalendarGroupRequest(ODataContext odataContext) : base(odataContext)
		{
		}

		// Token: 0x0600638D RID: 25485 RVA: 0x001364EC File Offset: 0x001346EC
		public override ODataCommand GetODataCommand()
		{
			return new DeleteCalendarGroupCommand(this);
		}
	}
}
