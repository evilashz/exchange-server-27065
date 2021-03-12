using System;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core
{
	// Token: 0x0200017D RID: 381
	internal class InferenceNewMailNotificationHandler : NewMailNotificationHandler
	{
		// Token: 0x06000DDC RID: 3548 RVA: 0x000349C1 File Offset: 0x00032BC1
		public InferenceNewMailNotificationHandler(string subscriptionId, IMailboxContext userContext) : base(subscriptionId, userContext)
		{
		}

		// Token: 0x06000DDD RID: 3549 RVA: 0x000349CB File Offset: 0x00032BCB
		protected override PropertyDefinition[] GetAdditionalPropsToLoad()
		{
			return InferenceNewMailNotificationHandler.additionalPropsToBind;
		}

		// Token: 0x06000DDE RID: 3550 RVA: 0x000349D4 File Offset: 0x00032BD4
		protected override void OnPayloadCreated(MessageItem newMessage, NewMailNotificationPayload payload)
		{
			object obj = newMessage.TryGetProperty(ItemSchema.IsClutter);
			bool isClutter = false;
			if (obj is bool)
			{
				isClutter = (bool)obj;
			}
			payload.IsClutter = isClutter;
		}

		// Token: 0x04000867 RID: 2151
		private static readonly PropertyDefinition[] additionalPropsToBind = new PropertyDefinition[]
		{
			ItemSchema.IsClutter
		};
	}
}
