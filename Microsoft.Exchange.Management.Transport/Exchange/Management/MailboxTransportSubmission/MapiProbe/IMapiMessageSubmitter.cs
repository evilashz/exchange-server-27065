using System;

namespace Microsoft.Exchange.Management.MailboxTransportSubmission.MapiProbe
{
	// Token: 0x02000056 RID: 86
	internal interface IMapiMessageSubmitter
	{
		// Token: 0x06000307 RID: 775
		void SendMapiMessage(string lamNotificationId, SendMapiMailDefinition mapiMailDefinition, out string entryId, out string internetMessageId, out Guid senderMbxGuid);

		// Token: 0x06000308 RID: 776
		void SendMapiMessage(SendMapiMailDefinition mapiMailDefinition, out string entryId, out string internetMessageId, out Guid senderMbxGuid);

		// Token: 0x06000309 RID: 777
		DeletionResult DeleteMessageFromOutbox(DeleteMapiMailDefinition deleteMapiMailDefinition);
	}
}
