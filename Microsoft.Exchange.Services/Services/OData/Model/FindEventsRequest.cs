using System;
using Microsoft.Exchange.Services.OData.Web;
using Microsoft.OData.Core.UriParser.Semantic;

namespace Microsoft.Exchange.Services.OData.Model
{
	// Token: 0x02000F3E RID: 3902
	[AllowedOAuthGrant("Calendars.Write")]
	[AllowedOAuthGrant("Calendars.Read")]
	internal class FindEventsRequest : FindEntitiesRequest<Event>
	{
		// Token: 0x0600634A RID: 25418 RVA: 0x001358B4 File Offset: 0x00133AB4
		public FindEventsRequest(ODataContext odataContext) : base(odataContext)
		{
		}

		// Token: 0x1700169E RID: 5790
		// (get) Token: 0x0600634B RID: 25419 RVA: 0x001358BD File Offset: 0x00133ABD
		// (set) Token: 0x0600634C RID: 25420 RVA: 0x001358C5 File Offset: 0x00133AC5
		public string CalendarId { get; protected set; }

		// Token: 0x0600634D RID: 25421 RVA: 0x001358D0 File Offset: 0x00133AD0
		public override void LoadFromHttpRequest()
		{
			base.LoadFromHttpRequest();
			if (base.ODataContext.ODataPath.ParentOfEntitySegment is KeySegment && base.ODataContext.ODataPath.ParentOfEntitySegment.EdmType.Equals(Calendar.EdmEntityType))
			{
				this.CalendarId = base.ODataContext.ODataPath.ParentOfEntitySegment.GetIdKey();
				return;
			}
			this.CalendarId = null;
		}

		// Token: 0x0600634E RID: 25422 RVA: 0x0013593E File Offset: 0x00133B3E
		public override ODataCommand GetODataCommand()
		{
			return new FindEventsCommand(this);
		}

		// Token: 0x0600634F RID: 25423 RVA: 0x00135946 File Offset: 0x00133B46
		public override void Validate()
		{
			base.Validate();
		}
	}
}
