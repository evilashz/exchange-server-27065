using System;
using System.Collections.Generic;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x0200005B RID: 91
	internal class TranslatorPFProxy : DisposableWrapper<IFxProxyPool>, IFxProxyPool, IDisposable
	{
		// Token: 0x060004C3 RID: 1219 RVA: 0x0001CE70 File Offset: 0x0001B070
		public TranslatorPFProxy(ISourceMailbox sourceMailbox, IDestinationMailbox destinationMailbox, IFxProxyPool destinationProxyPool) : base(destinationProxyPool, true)
		{
			this.sourceMailbox = sourceMailbox;
			this.destinationMailbox = destinationMailbox;
		}

		// Token: 0x060004C4 RID: 1220 RVA: 0x0001CE88 File Offset: 0x0001B088
		EntryIdMap<byte[]> IFxProxyPool.GetFolderData()
		{
			EntryIdMap<byte[]> entryIdMap = new EntryIdMap<byte[]>();
			foreach (KeyValuePair<byte[], byte[]> keyValuePair in base.WrappedObject.GetFolderData())
			{
				if (keyValuePair.Key.Length == 46)
				{
					entryIdMap.Add(this.sourceMailbox.GetSessionSpecificEntryId(keyValuePair.Key), keyValuePair.Value);
				}
			}
			return entryIdMap;
		}

		// Token: 0x060004C5 RID: 1221 RVA: 0x0001CF0C File Offset: 0x0001B10C
		void IFxProxyPool.Flush()
		{
			base.WrappedObject.Flush();
		}

		// Token: 0x060004C6 RID: 1222 RVA: 0x0001CF19 File Offset: 0x0001B119
		void IFxProxyPool.SetItemProperties(ItemPropertiesBase props)
		{
			base.WrappedObject.SetItemProperties(props);
		}

		// Token: 0x060004C7 RID: 1223 RVA: 0x0001CF27 File Offset: 0x0001B127
		IFolderProxy IFxProxyPool.CreateFolder(FolderRec folder)
		{
			return base.WrappedObject.CreateFolder(folder);
		}

		// Token: 0x060004C8 RID: 1224 RVA: 0x0001CF35 File Offset: 0x0001B135
		IFolderProxy IFxProxyPool.GetFolderProxy(byte[] sourceFolderId)
		{
			return base.WrappedObject.GetFolderProxy(this.destinationMailbox.GetSessionSpecificEntryId(sourceFolderId));
		}

		// Token: 0x060004C9 RID: 1225 RVA: 0x0001CF4E File Offset: 0x0001B14E
		List<byte[]> IFxProxyPool.GetUploadedMessageIDs()
		{
			return base.WrappedObject.GetUploadedMessageIDs();
		}

		// Token: 0x040001FC RID: 508
		private const int SizeOfFolderId = 46;

		// Token: 0x040001FD RID: 509
		private ISourceMailbox sourceMailbox;

		// Token: 0x040001FE RID: 510
		private IDestinationMailbox destinationMailbox;
	}
}
