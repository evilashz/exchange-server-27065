using System;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Services.Core.Types;
using Microsoft.Exchange.UM.ClientAccess;

namespace Microsoft.Exchange.Services.Core
{
	// Token: 0x02000351 RID: 849
	internal sealed class PlayOnPhone : SingleStepServiceCommand<PlayOnPhoneRequest, PhoneCallId>
	{
		// Token: 0x060017F2 RID: 6130 RVA: 0x00080C73 File Offset: 0x0007EE73
		public PlayOnPhone(CallContext callContext, PlayOnPhoneRequest request) : base(callContext, request)
		{
			this.itemId = request.ItemId;
			this.dialString = request.DialString;
		}

		// Token: 0x060017F3 RID: 6131 RVA: 0x00080C95 File Offset: 0x0007EE95
		internal override IExchangeWebMethodResponse GetResponse()
		{
			return new PlayOnPhoneResponseMessage(base.Result.Code, base.Result.Error, base.Result.Value);
		}

		// Token: 0x060017F4 RID: 6132 RVA: 0x00080CC0 File Offset: 0x0007EEC0
		internal override ServiceResult<PhoneCallId> Execute()
		{
			string text = null;
			MailboxSession mailboxSession = null;
			IdAndSession idAndSession = base.IdConverter.ConvertItemIdToIdAndSessionReadOnly(this.itemId);
			mailboxSession = (idAndSession.Session as MailboxSession);
			if (mailboxSession == null)
			{
				throw new ServiceInvalidOperationException(CoreResources.IDs.ErrorInvalidOperationForPublicFolderItems);
			}
			using (Item.Bind(mailboxSession, StoreId.GetStoreObjectId(idAndSession.Id)))
			{
			}
			using (UMClientCommon umclientCommon = new UMClientCommon(mailboxSession.MailboxOwner))
			{
				StoreObjectId storeObjectId = StoreId.GetStoreObjectId(idAndSession.Id);
				text = umclientCommon.PlayOnPhone(Convert.ToBase64String(storeObjectId.ProviderLevelItemId), this.dialString);
			}
			PhoneCallId value = (text != null) ? new PhoneCallId(text) : null;
			return new ServiceResult<PhoneCallId>(value);
		}

		// Token: 0x04001013 RID: 4115
		private ItemId itemId;

		// Token: 0x04001014 RID: 4116
		private string dialString;
	}
}
