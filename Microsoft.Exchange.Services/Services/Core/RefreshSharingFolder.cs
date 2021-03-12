using System;
using System.Xml;
using Microsoft.Exchange.Compliance.Xml;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.InfoWorker.Common.Sharing;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Core
{
	// Token: 0x02000353 RID: 851
	internal sealed class RefreshSharingFolder : SingleStepServiceCommand<RefreshSharingFolderRequest, bool>
	{
		// Token: 0x060017F7 RID: 6135 RVA: 0x00080FD0 File Offset: 0x0007F1D0
		public RefreshSharingFolder(CallContext callContext, RefreshSharingFolderRequest request) : base(callContext, request)
		{
		}

		// Token: 0x060017F8 RID: 6136 RVA: 0x00080FDC File Offset: 0x0007F1DC
		private static XmlNode CreateSyncInProgressXmlNode()
		{
			SafeXmlDocument safeXmlDocument = new SafeXmlDocument();
			XmlNode xmlNode = safeXmlDocument.CreateNode(XmlNodeType.Element, "SynchronizationStatus", "http://schemas.microsoft.com/exchange/services/2006/messages");
			xmlNode.InnerText = "InProgress";
			return xmlNode;
		}

		// Token: 0x060017F9 RID: 6137 RVA: 0x00081010 File Offset: 0x0007F210
		internal override IExchangeWebMethodResponse GetResponse()
		{
			RefreshSharingFolderResponseMessage refreshSharingFolderResponseMessage = new RefreshSharingFolderResponseMessage(base.Result.Code, base.Result.Error);
			if (base.Result.Value && base.Result.Error == null)
			{
				if (refreshSharingFolderResponseMessage.MessageXml == null)
				{
					refreshSharingFolderResponseMessage.MessageXml = new XmlNodeArray();
				}
				refreshSharingFolderResponseMessage.MessageXml.Nodes.Add(RefreshSharingFolder.SyncInProgressXmlNode);
			}
			return refreshSharingFolderResponseMessage;
		}

		// Token: 0x060017FA RID: 6138 RVA: 0x0008107C File Offset: 0x0007F27C
		internal override ServiceResult<bool> Execute()
		{
			IdAndSession idAndSession = base.IdConverter.ConvertXmlToIdAndSessionReadOnly(base.Request.SharingFolderId, BasicTypes.Folder);
			MailboxSession mailboxSession = (MailboxSession)idAndSession.Session;
			if (SyncAssistantInvoker.MailboxServerSupportsSync(mailboxSession))
			{
				SharingEngine.ValidateFolder(mailboxSession, idAndSession.Id);
				SyncAssistantInvoker.SyncFolder(mailboxSession, StoreId.GetStoreObjectId(idAndSession.Id));
				return new ServiceResult<bool>(true);
			}
			bool value = SharingEngine.SyncFolder(mailboxSession, idAndSession.Id);
			return new ServiceResult<bool>(value);
		}

		// Token: 0x04001015 RID: 4117
		private static readonly XmlNode SyncInProgressXmlNode = RefreshSharingFolder.CreateSyncInProgressXmlNode();
	}
}
