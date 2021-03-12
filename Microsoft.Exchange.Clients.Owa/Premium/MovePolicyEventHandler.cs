using System;
using Microsoft.Exchange.Clients.Owa.Core;
using Microsoft.Exchange.Clients.Owa.Premium.Controls;
using Microsoft.Exchange.Data.Storage;

namespace Microsoft.Exchange.Clients.Owa.Premium
{
	// Token: 0x020004C5 RID: 1221
	[OwaEventNamespace("MovePolicy")]
	internal sealed class MovePolicyEventHandler : PolicyEventHandlerBase
	{
		// Token: 0x06002E9E RID: 11934 RVA: 0x0010A6EE File Offset: 0x001088EE
		public static void Register()
		{
			OwaEventRegistry.RegisterHandler(typeof(MovePolicyEventHandler));
		}

		// Token: 0x06002E9F RID: 11935 RVA: 0x0010A6FF File Offset: 0x001088FF
		public MovePolicyEventHandler() : base(PolicyProvider.MovePolicyProvider)
		{
		}

		// Token: 0x06002EA0 RID: 11936 RVA: 0x0010A70C File Offset: 0x0010890C
		protected override PolicyContextMenuBase InternalGetPolicyMenu(ref OwaStoreObjectId itemId)
		{
			if (itemId != null)
			{
				if (itemId.OwaStoreObjectIdType != OwaStoreObjectIdType.MailBoxObject && itemId.OwaStoreObjectIdType != OwaStoreObjectIdType.Conversation)
				{
					throw new OwaInvalidRequestException("Only mailbox and conversation objects can be handled.");
				}
				if (itemId.IsConversationId)
				{
					OwaStoreObjectId[] allItemIds = ConversationUtilities.GetAllItemIds(base.UserContext, new OwaStoreObjectId[]
					{
						itemId
					}, new StoreObjectId[0]);
					if (allItemIds.Length == 1)
					{
						itemId = allItemIds[0];
					}
				}
			}
			return MovePolicyContextMenu.Create(base.UserContext);
		}

		// Token: 0x04002059 RID: 8281
		public const string EventNamespace = "MovePolicy";
	}
}
