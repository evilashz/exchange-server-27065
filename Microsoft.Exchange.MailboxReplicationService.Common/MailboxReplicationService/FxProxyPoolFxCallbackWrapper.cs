using System;
using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Mapi;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x02000233 RID: 563
	internal class FxProxyPoolFxCallbackWrapper : DisposableWrapper<IFxProxyPool>, IFxProxyPool, IDisposable
	{
		// Token: 0x06001DF4 RID: 7668 RVA: 0x0003DC09 File Offset: 0x0003BE09
		public FxProxyPoolFxCallbackWrapper(IFxProxyPool destProxies, bool ownsObject, Action<TimeSpan> updateDuration) : base(destProxies, ownsObject)
		{
			this.updateDuration = updateDuration;
		}

		// Token: 0x06001DF5 RID: 7669 RVA: 0x0003DC1C File Offset: 0x0003BE1C
		IFolderProxy IFxProxyPool.CreateFolder(FolderRec folder)
		{
			IFolderProxy proxy = base.WrappedObject.CreateFolder(folder);
			return new FxProxyPoolFxCallbackWrapper.FolderProxyFxCallbackWrapper(proxy, true, this.updateDuration);
		}

		// Token: 0x06001DF6 RID: 7670 RVA: 0x0003DC48 File Offset: 0x0003BE48
		IFolderProxy IFxProxyPool.GetFolderProxy(byte[] folderId)
		{
			IFolderProxy folderProxy = base.WrappedObject.GetFolderProxy(folderId);
			if (folderProxy == null)
			{
				return null;
			}
			return new FxProxyPoolFxCallbackWrapper.FolderProxyFxCallbackWrapper(folderProxy, true, this.updateDuration);
		}

		// Token: 0x06001DF7 RID: 7671 RVA: 0x0003DC76 File Offset: 0x0003BE76
		EntryIdMap<byte[]> IFxProxyPool.GetFolderData()
		{
			return base.WrappedObject.GetFolderData();
		}

		// Token: 0x06001DF8 RID: 7672 RVA: 0x0003DC83 File Offset: 0x0003BE83
		void IFxProxyPool.Flush()
		{
			base.WrappedObject.Flush();
		}

		// Token: 0x06001DF9 RID: 7673 RVA: 0x0003DC90 File Offset: 0x0003BE90
		void IFxProxyPool.SetItemProperties(ItemPropertiesBase props)
		{
			base.WrappedObject.SetItemProperties(props);
		}

		// Token: 0x06001DFA RID: 7674 RVA: 0x0003DC9E File Offset: 0x0003BE9E
		List<byte[]> IFxProxyPool.GetUploadedMessageIDs()
		{
			return base.WrappedObject.GetUploadedMessageIDs();
		}

		// Token: 0x04000C5A RID: 3162
		private Action<TimeSpan> updateDuration;

		// Token: 0x02000234 RID: 564
		private class FolderProxyFxCallbackWrapper : DisposableWrapper<IFolderProxy>, IFolderProxy, IMapiFxProxyEx, IMapiFxProxy, IDisposeTrackable, IDisposable
		{
			// Token: 0x06001DFB RID: 7675 RVA: 0x0003DCAB File Offset: 0x0003BEAB
			public FolderProxyFxCallbackWrapper(IFolderProxy proxy, bool ownsObject, Action<TimeSpan> updateDuration) : base(proxy, ownsObject)
			{
				this.updateDuration = updateDuration;
			}

			// Token: 0x06001DFC RID: 7676 RVA: 0x0003DCBC File Offset: 0x0003BEBC
			IMessageProxy IFolderProxy.OpenMessage(byte[] entryId)
			{
				return base.WrappedObject.OpenMessage(entryId);
			}

			// Token: 0x06001DFD RID: 7677 RVA: 0x0003DCCA File Offset: 0x0003BECA
			IMessageProxy IFolderProxy.CreateMessage(bool isAssociated)
			{
				return base.WrappedObject.CreateMessage(isAssociated);
			}

			// Token: 0x06001DFE RID: 7678 RVA: 0x0003DCD8 File Offset: 0x0003BED8
			void IFolderProxy.DeleteMessage(byte[] entryId)
			{
				base.WrappedObject.DeleteMessage(entryId);
			}

			// Token: 0x06001DFF RID: 7679 RVA: 0x0003DCE6 File Offset: 0x0003BEE6
			void IMapiFxProxyEx.SetProps(PropValueData[] pvda)
			{
				base.WrappedObject.SetProps(pvda);
			}

			// Token: 0x06001E00 RID: 7680 RVA: 0x0003DCF4 File Offset: 0x0003BEF4
			void IMapiFxProxyEx.SetItemProperties(ItemPropertiesBase props)
			{
				base.WrappedObject.SetItemProperties(props);
			}

			// Token: 0x06001E01 RID: 7681 RVA: 0x0003DD02 File Offset: 0x0003BF02
			byte[] IMapiFxProxy.GetObjectData()
			{
				return base.WrappedObject.GetObjectData();
			}

			// Token: 0x06001E02 RID: 7682 RVA: 0x0003DD10 File Offset: 0x0003BF10
			void IMapiFxProxy.ProcessRequest(FxOpcodes opCode, byte[] request)
			{
				Stopwatch stopwatch = Stopwatch.StartNew();
				try
				{
					base.WrappedObject.ProcessRequest(opCode, request);
				}
				finally
				{
					this.updateDuration(stopwatch.Elapsed);
					stopwatch.Stop();
				}
			}

			// Token: 0x04000C5B RID: 3163
			private Action<TimeSpan> updateDuration;
		}
	}
}
