using System;
using System.Net;
using System.Xml;
using Microsoft.Exchange.Data.Storage;

namespace Microsoft.Exchange.AirSync
{
	// Token: 0x020000E6 RID: 230
	internal class MoveCollectionCommand : CollectionCommand
	{
		// Token: 0x06000CF5 RID: 3317 RVA: 0x0004629F File Offset: 0x0004449F
		internal MoveCollectionCommand()
		{
			base.PerfCounter = AirSyncCounters.NumberOfMoveCollections;
		}

		// Token: 0x17000510 RID: 1296
		// (get) Token: 0x06000CF6 RID: 3318 RVA: 0x000462B2 File Offset: 0x000444B2
		protected override bool IsInteractiveCommand
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06000CF7 RID: 3319 RVA: 0x000462B8 File Offset: 0x000444B8
		protected override void ProcessCommand(MailboxSession mailboxSession, XmlDocument doc)
		{
			if (base.CollectionRequest.CollectionName == null || base.CollectionRequest.ParentId == null || base.CollectionRequest.CollectionId == null)
			{
				base.ProtocolLogger.SetValue(ProtocolLoggerData.Error, "InvalidURLParameters");
				throw new AirSyncPermanentException(HttpStatusCode.BadRequest, StatusCode.InvalidCombinationOfIDs, null, false);
			}
			using (Folder folder = Folder.Bind(mailboxSession, base.CollectionRequest.CollectionId, null))
			{
				if (folder.DisplayName == base.CollectionRequest.CollectionName && folder.ParentId.Equals(base.CollectionRequest.ParentId))
				{
					base.ProtocolLogger.SetValue(ProtocolLoggerData.Error, "FolderExistsInCollectionMove");
					throw new AirSyncPermanentException(StatusCode.Sync_ProtocolVersionMismatch, CollectionCommand.ConstructErrorXml(StatusCode.Sync_ProtocolVersionMismatch), null, false);
				}
				if (base.CollectionRequest.CollectionName != null)
				{
					folder.DisplayName = base.CollectionRequest.CollectionName;
					folder.Save();
					folder.Load();
				}
				if (!base.CollectionRequest.ParentId.Equals(folder.ParentId))
				{
					OperationResult operationResult = mailboxSession.Move(base.CollectionRequest.ParentId, new StoreObjectId[]
					{
						base.CollectionRequest.CollectionId
					}).OperationResult;
					if (operationResult != OperationResult.Succeeded)
					{
						base.ProtocolLogger.SetValue(ProtocolLoggerData.Error, "FolderNotFoundInCollectionMove");
						throw new AirSyncPermanentException(StatusCode.Sync_ProtocolError, CollectionCommand.ConstructErrorXml(StatusCode.Sync_ProtocolError), null, false);
					}
				}
			}
			XmlNode xmlNode = doc.CreateElement("Response", "FolderHierarchy:");
			XmlNode xmlNode2 = doc.CreateElement("Status", "FolderHierarchy:");
			xmlNode2.InnerText = "1";
			doc.AppendChild(xmlNode);
			xmlNode.AppendChild(xmlNode2);
		}
	}
}
