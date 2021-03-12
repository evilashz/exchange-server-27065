using System;
using Microsoft.Exchange.Services.OData.Web;
using Microsoft.OData.Core.UriParser.Semantic;

namespace Microsoft.Exchange.Services.OData.Model
{
	// Token: 0x02000F32 RID: 3890
	[AllowedOAuthGrant("Calendars.Write")]
	internal class CreateEventRequest : CreateEntityRequest<Event>
	{
		// Token: 0x0600632C RID: 25388 RVA: 0x00135500 File Offset: 0x00133700
		public CreateEventRequest(ODataContext odataContext) : base(odataContext)
		{
		}

		// Token: 0x1700169A RID: 5786
		// (get) Token: 0x0600632D RID: 25389 RVA: 0x00135509 File Offset: 0x00133709
		// (set) Token: 0x0600632E RID: 25390 RVA: 0x00135511 File Offset: 0x00133711
		public string CalendarId { get; protected set; }

		// Token: 0x0600632F RID: 25391 RVA: 0x0013551C File Offset: 0x0013371C
		public override void LoadFromHttpRequest()
		{
			base.LoadFromHttpRequest();
			if (base.ODataContext.ODataPath.EntitySegment is NavigationPropertySegment)
			{
				if (base.ODataContext.ODataPath.ParentOfEntitySegment is KeySegment && base.ODataContext.ODataPath.ParentOfEntitySegment.EdmType.Equals(Calendar.EdmEntityType))
				{
					this.CalendarId = base.ODataContext.ODataPath.ParentOfEntitySegment.GetIdKey();
					return;
				}
				if (base.ODataContext.ODataPath.ParentOfEntitySegment is NavigationPropertySegment)
				{
					this.CalendarId = null;
				}
			}
		}

		// Token: 0x06006330 RID: 25392 RVA: 0x001355B8 File Offset: 0x001337B8
		public override ODataCommand GetODataCommand()
		{
			return new CreateEventCommand(this);
		}

		// Token: 0x06006331 RID: 25393 RVA: 0x001355C0 File Offset: 0x001337C0
		public override void Validate()
		{
			base.Validate();
		}
	}
}
