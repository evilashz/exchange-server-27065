using System;

namespace Microsoft.Exchange.Services.OData.Model
{
	// Token: 0x02000F11 RID: 3857
	[AllowedOAuthGrant("Mail.Write")]
	internal class CreateMessageResponseDraftRequest : EntityActionRequest<Message>
	{
		// Token: 0x060062D9 RID: 25305 RVA: 0x001349C2 File Offset: 0x00132BC2
		public CreateMessageResponseDraftRequest(ODataContext odataContext) : base(odataContext)
		{
		}

		// Token: 0x17001690 RID: 5776
		// (get) Token: 0x060062DA RID: 25306 RVA: 0x001349CB File Offset: 0x00132BCB
		// (set) Token: 0x060062DB RID: 25307 RVA: 0x001349D3 File Offset: 0x00132BD3
		public MessageResponseType ResponseType { get; protected set; }

		// Token: 0x060062DC RID: 25308 RVA: 0x001349DC File Offset: 0x00132BDC
		public override void LoadFromHttpRequest()
		{
			base.LoadFromHttpRequest();
			string actionName;
			if ((actionName = base.ActionName) != null)
			{
				if (actionName == "CreateReply")
				{
					this.ResponseType = MessageResponseType.Reply;
					return;
				}
				if (actionName == "CreateReplyAll")
				{
					this.ResponseType = MessageResponseType.ReplyAll;
					return;
				}
				if (!(actionName == "CreateForward"))
				{
					return;
				}
				this.ResponseType = MessageResponseType.Forward;
			}
		}

		// Token: 0x060062DD RID: 25309 RVA: 0x00134A38 File Offset: 0x00132C38
		public override ODataCommand GetODataCommand()
		{
			return new CreateMessageResponseDraftCommand(this);
		}
	}
}
