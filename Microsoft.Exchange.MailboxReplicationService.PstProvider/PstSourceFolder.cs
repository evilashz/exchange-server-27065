using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Exchange.RpcClientAccess;
using Microsoft.Exchange.RpcClientAccess.FastTransfer;
using Microsoft.Exchange.RpcClientAccess.FastTransfer.Handler;
using Microsoft.Exchange.RpcClientAccess.FastTransfer.Parser;
using Microsoft.Mapi;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x02000014 RID: 20
	internal class PstSourceFolder : PstFolder, ISourceFolder, IFolder, IDisposable
	{
		// Token: 0x060000FA RID: 250 RVA: 0x00006C2C File Offset: 0x00004E2C
		void ISourceFolder.CopyTo(IFxProxy fxFolderProxy, CopyPropertiesFlags flags, PropTag[] propTagsToExclude)
		{
			MrsTracer.Provider.Function("PstSourceFolder.ISourceFolder.CopyTo", new object[0]);
			bool exportCompleted = false;
			CommonUtils.ProcessKnownExceptions(delegate
			{
				FxCollectorSerializer fxCollectorSerializer = new FxCollectorSerializer(fxFolderProxy);
				fxCollectorSerializer.Config(0, 1);
				using (FastTransferDownloadContext fastTransferDownloadContext = FastTransferDownloadContext.CreateForDownload(FastTransferSendOption.Unicode | FastTransferSendOption.UseCpId | FastTransferSendOption.ForceUnicode, 1U, Encoding.ASCII, NullResourceTracker.Instance, this.GetPropertyFilterFactory(PstMailbox.MoMTPtaFromPta(propTagsToExclude)), false))
				{
					IFastTransferProcessor<FastTransferDownloadContext> fastTransferObject = FastTransferFolderCopyTo.CreateDownloadStateMachine(this.Folder, FastTransferFolderContentBase.IncludeSubObject.None);
					fastTransferDownloadContext.PushInitial(fastTransferObject);
					FxUtils.TransferFxBuffers(fastTransferDownloadContext, fxCollectorSerializer);
				}
				exportCompleted = true;
				fxFolderProxy.Flush();
			}, delegate(Exception ex)
			{
				if (!exportCompleted)
				{
					MrsTracer.Provider.Debug("Flushing target proxy after receiving an exception.", new object[0]);
					CommonUtils.CatchKnownExceptions(new Action(fxFolderProxy.Flush), null);
				}
				return false;
			});
		}

		// Token: 0x060000FB RID: 251 RVA: 0x00006C8D File Offset: 0x00004E8D
		void ISourceFolder.ExportMessages(IFxProxy destFolderProxy, CopyMessagesFlags flags, byte[][] entryIds)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060000FC RID: 252 RVA: 0x00006C94 File Offset: 0x00004E94
		FolderChangesManifest ISourceFolder.EnumerateChanges(EnumerateContentChangesFlags flags, int maxChanges)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060000FD RID: 253 RVA: 0x00006C9B File Offset: 0x00004E9B
		List<MessageRec> ISourceFolder.EnumerateMessagesPaged(int maxPageSize)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060000FE RID: 254 RVA: 0x00006CA2 File Offset: 0x00004EA2
		int ISourceFolder.GetEstimatedItemCount()
		{
			throw new NotImplementedException();
		}

		// Token: 0x060000FF RID: 255 RVA: 0x00006CAC File Offset: 0x00004EAC
		private PropertyFilterFactory GetPropertyFilterFactory(PropertyTag[] additionalPropertyTags)
		{
			if (this.propertyTagsToExclude != null && (additionalPropertyTags == null || this.propertyTagsToExclude.IsSupersetOf(additionalPropertyTags)))
			{
				return this.propertyFilterFactory;
			}
			this.propertyTagsToExclude = new HashSet<PropertyTag>(PropertyFilterFactory.ExcludePropertiesForFxCopyFolder);
			if (additionalPropertyTags != null)
			{
				this.propertyTagsToExclude.UnionWith(additionalPropertyTags);
			}
			this.propertyFilterFactory = new PropertyFilterFactory(false, false, this.propertyTagsToExclude.ToArray<PropertyTag>());
			return this.propertyFilterFactory;
		}

		// Token: 0x04000047 RID: 71
		private HashSet<PropertyTag> propertyTagsToExclude;

		// Token: 0x04000048 RID: 72
		private PropertyFilterFactory propertyFilterFactory;
	}
}
