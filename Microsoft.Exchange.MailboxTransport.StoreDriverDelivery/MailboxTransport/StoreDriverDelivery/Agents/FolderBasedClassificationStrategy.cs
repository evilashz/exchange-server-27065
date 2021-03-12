using System;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage;

namespace Microsoft.Exchange.MailboxTransport.StoreDriverDelivery.Agents
{
	// Token: 0x02000079 RID: 121
	internal class FolderBasedClassificationStrategy : IDeliveryClassificationStrategy
	{
		// Token: 0x0600044A RID: 1098 RVA: 0x00014F70 File Offset: 0x00013170
		public void ApplyClassification(StoreDriverDeliveryEventArgsImpl argsImpl, InferenceClassificationResult result)
		{
			argsImpl.PropertiesForAllMessageCopies[ItemSchema.IsClutter] = result.HasFlag(InferenceClassificationResult.IsClutterFinal);
			if ((result & InferenceClassificationResult.IsOverridden) != InferenceClassificationResult.None)
			{
				argsImpl.PropertiesForAllMessageCopies[MessageItemSchema.IsClutterOverridden] = true;
			}
			if (result.HasFlag(InferenceClassificationResult.IsClutterFinal))
			{
				if (argsImpl.MailboxSession.GetDefaultFolderId(DefaultFolderType.Clutter) == null)
				{
					argsImpl.MailboxSession.CreateDefaultFolder(DefaultFolderType.Clutter);
				}
				using (Folder folder = Folder.Bind(argsImpl.MailboxSession, DefaultFolderType.Clutter))
				{
					argsImpl.SetDeliveryFolder(folder);
				}
			}
		}
	}
}
