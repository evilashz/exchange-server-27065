using System;
using Microsoft.Exchange.Services.OData.Web;
using Microsoft.OData.Core.UriParser.Semantic;

namespace Microsoft.Exchange.Services.OData.Model
{
	// Token: 0x02000F47 RID: 3911
	[AllowedOAuthGrant("Calendars.Write")]
	internal class CreateCalendarRequest : CreateEntityRequest<Calendar>
	{
		// Token: 0x06006364 RID: 25444 RVA: 0x00135FA0 File Offset: 0x001341A0
		public CreateCalendarRequest(ODataContext odataContext) : base(odataContext)
		{
		}

		// Token: 0x170016A0 RID: 5792
		// (get) Token: 0x06006365 RID: 25445 RVA: 0x00135FA9 File Offset: 0x001341A9
		// (set) Token: 0x06006366 RID: 25446 RVA: 0x00135FB1 File Offset: 0x001341B1
		public string CalendarGroupId { get; protected set; }

		// Token: 0x06006367 RID: 25447 RVA: 0x00135FBC File Offset: 0x001341BC
		public override void LoadFromHttpRequest()
		{
			base.LoadFromHttpRequest();
			if (base.ODataContext.ODataPath.EntitySegment is NavigationPropertySegment && base.ODataContext.ODataPath.ParentOfEntitySegment is KeySegment && base.ODataContext.ODataPath.ParentOfEntitySegment.EdmType.Equals(CalendarGroup.EdmEntityType))
			{
				this.CalendarGroupId = base.ODataContext.ODataPath.ParentOfEntitySegment.GetIdKey();
			}
		}

		// Token: 0x06006368 RID: 25448 RVA: 0x00136039 File Offset: 0x00134239
		public override ODataCommand GetODataCommand()
		{
			return new CreateCalendarCommand(this);
		}
	}
}
