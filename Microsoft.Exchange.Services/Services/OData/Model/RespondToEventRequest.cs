using System;
using Microsoft.Exchange.Entities.DataModel.Calendaring;
using Microsoft.Exchange.Entities.DataModel.Calendaring.CustomActions;
using Microsoft.Exchange.Entities.DataModel.Items;

namespace Microsoft.Exchange.Services.OData.Model
{
	// Token: 0x02000F38 RID: 3896
	[AllowedOAuthGrant("Calendars.Write")]
	internal class RespondToEventRequest : EntityActionRequest<Event>
	{
		// Token: 0x0600633A RID: 25402 RVA: 0x001356B2 File Offset: 0x001338B2
		public RespondToEventRequest(ODataContext odataContext) : base(odataContext)
		{
		}

		// Token: 0x1700169B RID: 5787
		// (get) Token: 0x0600633B RID: 25403 RVA: 0x001356BB File Offset: 0x001338BB
		// (set) Token: 0x0600633C RID: 25404 RVA: 0x001356C3 File Offset: 0x001338C3
		public string Comment { get; protected set; }

		// Token: 0x1700169C RID: 5788
		// (get) Token: 0x0600633D RID: 25405 RVA: 0x001356CC File Offset: 0x001338CC
		// (set) Token: 0x0600633E RID: 25406 RVA: 0x001356D4 File Offset: 0x001338D4
		public RespondToEventParameters RespondToEventParameters { get; protected set; }

		// Token: 0x0600633F RID: 25407 RVA: 0x001356DD File Offset: 0x001338DD
		public override ODataCommand GetODataCommand()
		{
			return new RespondToEventCommand(this);
		}

		// Token: 0x06006340 RID: 25408 RVA: 0x001356E8 File Offset: 0x001338E8
		public override void LoadFromHttpRequest()
		{
			base.LoadFromHttpRequest();
			this.RespondToEventParameters = new RespondToEventParameters();
			this.RespondToEventParameters.SendResponse = true;
			object obj;
			if (base.Parameters.TryGetValue("Comment", out obj))
			{
				this.RespondToEventParameters.Notes = new ItemBody();
				this.RespondToEventParameters.Notes.ContentType = BodyType.Text;
				this.RespondToEventParameters.Notes.Content = (string)obj;
			}
			string actionName = base.ActionName;
			string a;
			if ((a = actionName) != null)
			{
				if (a == "TentativelyAccept")
				{
					this.RespondToEventParameters.Response = ResponseType.TentativelyAccepted;
					return;
				}
				if (a == "Accept")
				{
					this.RespondToEventParameters.Response = ResponseType.Accepted;
					return;
				}
				if (!(a == "Decline"))
				{
					return;
				}
				this.RespondToEventParameters.Response = ResponseType.Declined;
			}
		}
	}
}
