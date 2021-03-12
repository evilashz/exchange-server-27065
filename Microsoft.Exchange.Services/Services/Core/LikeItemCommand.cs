using System;
using System.Linq;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Data.Storage.ActivityLog;
using Microsoft.Exchange.Data.Storage.Principal;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Services.Core.Types;
using Microsoft.Exchange.Services.Wcf;

namespace Microsoft.Exchange.Services.Core
{
	// Token: 0x02000345 RID: 837
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal class LikeItemCommand : ServiceCommand<LikeItemResponse>
	{
		// Token: 0x0600179F RID: 6047 RVA: 0x0007E6E9 File Offset: 0x0007C8E9
		public LikeItemCommand(CallContext callContext, LikeItemRequest request) : base(callContext)
		{
			WcfServiceCommandBase.ThrowIfNull(request, "request", "LikeItemCommand::ctor");
			this.request = request;
			this.callContext = callContext;
		}

		// Token: 0x060017A0 RID: 6048 RVA: 0x0007E730 File Offset: 0x0007C930
		protected override LikeItemResponse InternalExecute()
		{
			this.request.Validate();
			IMailboxInfo mailboxInfo = this.callContext.AccessingPrincipal.MailboxInfo;
			Participant likingParticipant = new Participant(mailboxInfo.DisplayName, mailboxInfo.PrimarySmtpAddress.ToString(), "SMTP");
			using (MailboxSession mailboxSession = MailboxSession.OpenAsAdmin(this.callContext.MailboxIdentityPrincipal, this.callContext.ClientCulture, StoreSessionCacheBase.BuildMapiApplicationId(this.callContext, null)))
			{
				using (MessageItem messageItem = MessageItem.Bind(mailboxSession, IdConverter.EwsIdToMessageStoreObjectId(this.request.ItemId.Id), Likers.RequiredProperties))
				{
					Participant participant = messageItem.Likers.FirstOrDefault((Participant liker) => liker.EmailAddress == likingParticipant.EmailAddress);
					bool flag = participant != null;
					if (this.request.IsUnlike && flag)
					{
						messageItem.OpenAsReadWrite();
						messageItem.Likers.Remove(participant);
						messageItem.Save(SaveMode.ResolveConflicts);
					}
					else
					{
						if (this.request.IsUnlike || flag)
						{
							throw new ServiceInvalidOperationException((CoreResources.IDs)4151155219U);
						}
						messageItem.OpenAsReadWrite();
						messageItem.Likers.Add(likingParticipant);
						messageItem.Save(SaveMode.ResolveConflicts);
					}
					if (mailboxSession.ActivitySession != null)
					{
						mailboxSession.ActivitySession.CaptureActivity(this.request.IsUnlike ? ActivityId.Unlike : ActivityId.Like, messageItem.StoreObjectId, null, base.CallContext.GetAccessingInformation());
					}
				}
			}
			return new LikeItemResponse();
		}

		// Token: 0x04000FDC RID: 4060
		private static readonly PropertyDefinition[] PropertiesToFetch = new PropertyDefinition[]
		{
			MessageItemSchema.LikeCount,
			MessageItemSchema.LikersBlob
		};

		// Token: 0x04000FDD RID: 4061
		private readonly LikeItemRequest request;

		// Token: 0x04000FDE RID: 4062
		private readonly CallContext callContext;
	}
}
