using System;
using Microsoft.OData.Core;

namespace Microsoft.Exchange.Services.OData.Model
{
	// Token: 0x02000F14 RID: 3860
	[AllowedOAuthGrant("Mail.Send")]
	internal class RespondToMessageRequest : EntityActionRequest<Message>
	{
		// Token: 0x060062E2 RID: 25314 RVA: 0x00134AA9 File Offset: 0x00132CA9
		public RespondToMessageRequest(ODataContext odataContext) : base(odataContext)
		{
		}

		// Token: 0x17001692 RID: 5778
		// (get) Token: 0x060062E3 RID: 25315 RVA: 0x00134AB2 File Offset: 0x00132CB2
		// (set) Token: 0x060062E4 RID: 25316 RVA: 0x00134ABA File Offset: 0x00132CBA
		public MessageResponseType ResponseType { get; protected set; }

		// Token: 0x17001693 RID: 5779
		// (get) Token: 0x060062E5 RID: 25317 RVA: 0x00134AC3 File Offset: 0x00132CC3
		// (set) Token: 0x060062E6 RID: 25318 RVA: 0x00134ACB File Offset: 0x00132CCB
		public string Comment { get; protected set; }

		// Token: 0x17001694 RID: 5780
		// (get) Token: 0x060062E7 RID: 25319 RVA: 0x00134AD4 File Offset: 0x00132CD4
		// (set) Token: 0x060062E8 RID: 25320 RVA: 0x00134ADC File Offset: 0x00132CDC
		public Recipient[] ToRecipients { get; protected set; }

		// Token: 0x060062E9 RID: 25321 RVA: 0x00134AE8 File Offset: 0x00132CE8
		public override void LoadFromHttpRequest()
		{
			base.LoadFromHttpRequest();
			object obj;
			if (base.Parameters.TryGetValue("Comment", out obj))
			{
				this.Comment = (string)obj;
			}
			string actionName;
			if ((actionName = base.ActionName) != null)
			{
				if (actionName == "Reply")
				{
					this.ResponseType = MessageResponseType.Reply;
					return;
				}
				if (actionName == "ReplyAll")
				{
					this.ResponseType = MessageResponseType.ReplyAll;
					return;
				}
				if (!(actionName == "Forward"))
				{
					return;
				}
				this.ResponseType = MessageResponseType.Forward;
				object obj2;
				if (base.Parameters.TryGetValue("ToRecipients", out obj2))
				{
					this.ToRecipients = RecipientsODataConverter.ODataCollectionValueToRecipients((ODataCollectionValue)obj2);
				}
			}
		}

		// Token: 0x060062EA RID: 25322 RVA: 0x00134B89 File Offset: 0x00132D89
		public override void Validate()
		{
			base.Validate();
			if (this.ResponseType == MessageResponseType.Forward)
			{
				ValidationHelper.ValidateParameterEmpty("ToRecipients", this.ToRecipients);
			}
		}

		// Token: 0x060062EB RID: 25323 RVA: 0x00134BAA File Offset: 0x00132DAA
		public override ODataCommand GetODataCommand()
		{
			return new RespondToMessageCommand(this);
		}
	}
}
