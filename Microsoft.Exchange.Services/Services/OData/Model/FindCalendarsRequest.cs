using System;
using Microsoft.Exchange.Services.OData.Web;
using Microsoft.OData.Core.UriParser.Semantic;

namespace Microsoft.Exchange.Services.OData.Model
{
	// Token: 0x02000F44 RID: 3908
	[AllowedOAuthGrant("Calendars.Read")]
	[AllowedOAuthGrant("Calendars.Write")]
	internal class FindCalendarsRequest : FindEntitiesRequest<Calendar>
	{
		// Token: 0x0600635B RID: 25435 RVA: 0x00135E20 File Offset: 0x00134020
		public FindCalendarsRequest(ODataContext odataContext) : base(odataContext)
		{
		}

		// Token: 0x1700169F RID: 5791
		// (get) Token: 0x0600635C RID: 25436 RVA: 0x00135E29 File Offset: 0x00134029
		// (set) Token: 0x0600635D RID: 25437 RVA: 0x00135E31 File Offset: 0x00134031
		public string CalendarGroupId { get; protected set; }

		// Token: 0x0600635E RID: 25438 RVA: 0x00135E3C File Offset: 0x0013403C
		public override void LoadFromHttpRequest()
		{
			base.LoadFromHttpRequest();
			if (base.ODataContext.ODataPath.ParentOfEntitySegment is KeySegment && base.ODataContext.ODataPath.ParentOfEntitySegment.EdmType.Equals(CalendarGroup.EdmEntityType))
			{
				this.CalendarGroupId = base.ODataContext.ODataPath.ParentOfEntitySegment.GetIdKey();
			}
		}

		// Token: 0x0600635F RID: 25439 RVA: 0x00135EA2 File Offset: 0x001340A2
		public override ODataCommand GetODataCommand()
		{
			return new FindCalendarsCommand(this);
		}
	}
}
