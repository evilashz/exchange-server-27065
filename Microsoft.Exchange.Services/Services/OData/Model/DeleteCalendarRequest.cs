using System;

namespace Microsoft.Exchange.Services.OData.Model
{
	// Token: 0x02000F4A RID: 3914
	[AllowedOAuthGrant("Calendars.Write")]
	internal class DeleteCalendarRequest : DeleteEntityRequest<Calendar>
	{
		// Token: 0x0600636C RID: 25452 RVA: 0x00136102 File Offset: 0x00134302
		public DeleteCalendarRequest(ODataContext odataContext) : base(odataContext)
		{
		}

		// Token: 0x0600636D RID: 25453 RVA: 0x0013610B File Offset: 0x0013430B
		public override ODataCommand GetODataCommand()
		{
			return new DeleteCalendarCommand(this);
		}
	}
}
