using System;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.UM.UMCommon;

namespace Microsoft.Exchange.UM.UMCore
{
	// Token: 0x0200023A RID: 570
	internal class VoicemailReplyAll : VoicemailReply
	{
		// Token: 0x060010AD RID: 4269 RVA: 0x0004B078 File Offset: 0x00049278
		internal VoicemailReplyAll(BaseUMCallSession vo, StoreObjectId originalId, UMSubscriber user, ContactInfo sender, RetrieveVoicemailManager context, bool isOriginalMessageIsProtected) : base(vo, originalId, user, sender, context, isOriginalMessageIsProtected)
		{
		}

		// Token: 0x060010AE RID: 4270 RVA: 0x0004B089 File Offset: 0x00049289
		protected override MessageItem InternalCreateReply(MessageItem original, MailboxSession session, ReplyForwardConfiguration replyConfiguration)
		{
			return original.CreateReplyAll(XsoUtil.GetDraftsFolderId(session), replyConfiguration);
		}
	}
}
