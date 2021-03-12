using System;
using System.Net;
using System.Xml;
using Microsoft.Exchange.Data.Storage;

namespace Microsoft.Exchange.AirSync
{
	// Token: 0x0200005E RID: 94
	internal class DeleteCollectionCommand : CollectionCommand
	{
		// Token: 0x0600052D RID: 1325 RVA: 0x0001E89F File Offset: 0x0001CA9F
		internal DeleteCollectionCommand()
		{
			base.PerfCounter = AirSyncCounters.NumberOfDeleteCollections;
		}

		// Token: 0x0600052E RID: 1326 RVA: 0x0001E8B4 File Offset: 0x0001CAB4
		protected override void ProcessCommand(MailboxSession mailboxSession, XmlDocument doc)
		{
			if (base.CollectionRequest.CollectionId == null)
			{
				base.Context.ProtocolLogger.SetValue(ProtocolLoggerData.Error, "InvalidURLParameters");
				throw new AirSyncPermanentException(HttpStatusCode.BadRequest, StatusCode.InvalidCombinationOfIDs, null, false);
			}
			mailboxSession.Delete(DeleteItemFlags.SoftDelete, new StoreObjectId[]
			{
				base.CollectionRequest.CollectionId
			});
			FolderIdMapping folderIdMapping = (FolderIdMapping)base.SyncState[CustomStateDatumType.IdMapping];
			folderIdMapping.Delete(new ISyncItemId[]
			{
				MailboxSyncItemId.CreateForNewItem(base.CollectionRequest.CollectionId)
			});
			XmlNode xmlNode = doc.CreateElement("Response", "FolderHierarchy:");
			XmlNode xmlNode2 = doc.CreateElement("Status", "FolderHierarchy:");
			xmlNode2.InnerText = "1";
			doc.AppendChild(xmlNode);
			xmlNode.AppendChild(xmlNode2);
		}
	}
}
