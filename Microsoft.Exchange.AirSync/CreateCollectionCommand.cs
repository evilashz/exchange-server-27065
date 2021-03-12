using System;
using System.Net;
using System.Xml;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics.Components.AirSync;

namespace Microsoft.Exchange.AirSync
{
	// Token: 0x02000058 RID: 88
	internal class CreateCollectionCommand : CollectionCommand
	{
		// Token: 0x060004C7 RID: 1223 RVA: 0x0001D6C8 File Offset: 0x0001B8C8
		internal CreateCollectionCommand()
		{
			base.PerfCounter = AirSyncCounters.NumberOfCreateCollections;
		}

		// Token: 0x060004C8 RID: 1224 RVA: 0x0001D6DC File Offset: 0x0001B8DC
		protected override void ProcessCommand(MailboxSession mailboxSession, XmlDocument doc)
		{
			if (base.CollectionRequest.CollectionName == null || base.CollectionRequest.ParentId == null)
			{
				base.Context.ProtocolLogger.SetValue(ProtocolLoggerData.Error, "InvalidURLParameters");
				throw new AirSyncPermanentException(HttpStatusCode.BadRequest, StatusCode.InvalidCombinationOfIDs, null, false);
			}
			AirSyncDiagnostics.TraceDebug<string, StoreObjectId>(ExTraceGlobals.RequestsTracer, this, "Creating collection with name {0} and parent Id {1}.", base.CollectionRequest.CollectionName, base.CollectionRequest.ParentId);
			using (Folder folder = Folder.Create(mailboxSession, base.CollectionRequest.ParentId, StoreObjectType.Folder))
			{
				folder.DisplayName = base.CollectionRequest.CollectionName;
				folder.ClassName = "IPF.Note";
				folder.Save();
				XmlNode xmlNode = doc.CreateElement("Response", "FolderHierarchy:");
				XmlNode xmlNode2 = doc.CreateElement("Status", "FolderHierarchy:");
				XmlNode xmlNode3 = doc.CreateElement("Folder", "FolderHierarchy:");
				XmlNode xmlNode4 = doc.CreateElement("ServerId", "FolderHierarchy:");
				xmlNode2.InnerText = "1";
				folder.Load();
				xmlNode4.InnerText = ((FolderIdMapping)base.SyncState[CustomStateDatumType.IdMapping]).Add(MailboxSyncItemId.CreateForNewItem(folder.Id.ObjectId));
				doc.AppendChild(xmlNode);
				xmlNode.AppendChild(xmlNode2);
				xmlNode.AppendChild(xmlNode3);
				xmlNode3.AppendChild(xmlNode4);
			}
		}
	}
}
