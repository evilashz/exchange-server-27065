using System;
using System.Collections.Generic;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Mapi;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x0200023C RID: 572
	internal class FxProxyPoolWrapper : WrapperBase<IFxProxyPool>, IFxProxyPool, IDisposable
	{
		// Token: 0x06001E27 RID: 7719 RVA: 0x0003E43B File Offset: 0x0003C63B
		public FxProxyPoolWrapper(IFxProxyPool proxy, CommonUtils.CreateContextDelegate createContext) : base(proxy, createContext)
		{
		}

		// Token: 0x06001E28 RID: 7720 RVA: 0x0003E46C File Offset: 0x0003C66C
		IFolderProxy IFxProxyPool.CreateFolder(FolderRec folder)
		{
			IFolderProxy result = null;
			base.CreateContext("IFxProxyPool.CreateFolder", new DataContext[]
			{
				new FolderRecDataContext(folder)
			}).Execute(delegate
			{
				result = this.WrappedObject.CreateFolder(folder);
			}, true);
			return new FxProxyPoolWrapper.FolderProxyWrapper(result, base.CreateContext);
		}

		// Token: 0x06001E29 RID: 7721 RVA: 0x0003E504 File Offset: 0x0003C704
		IFolderProxy IFxProxyPool.GetFolderProxy(byte[] folderId)
		{
			IFolderProxy result = null;
			base.CreateContext("IFxProxyPool.GetFolderProxy", new DataContext[]
			{
				new EntryIDsDataContext(folderId)
			}).Execute(delegate
			{
				result = this.WrappedObject.GetFolderProxy(folderId);
			}, true);
			if (result == null)
			{
				return null;
			}
			return new FxProxyPoolWrapper.FolderProxyWrapper(result, base.CreateContext);
		}

		// Token: 0x06001E2A RID: 7722 RVA: 0x0003E5A0 File Offset: 0x0003C7A0
		EntryIdMap<byte[]> IFxProxyPool.GetFolderData()
		{
			EntryIdMap<byte[]> result = null;
			base.CreateContext("IFxProxyPool.GetFolderData", new DataContext[0]).Execute(delegate
			{
				result = this.WrappedObject.GetFolderData();
			}, true);
			return result;
		}

		// Token: 0x06001E2B RID: 7723 RVA: 0x0003E5FC File Offset: 0x0003C7FC
		void IFxProxyPool.Flush()
		{
			base.CreateContext("IFxProxyPool.Flush", new DataContext[0]).Execute(delegate
			{
				base.WrappedObject.Flush();
			}, true);
		}

		// Token: 0x06001E2C RID: 7724 RVA: 0x0003E648 File Offset: 0x0003C848
		void IFxProxyPool.SetItemProperties(ItemPropertiesBase props)
		{
			base.CreateContext("IFxProxyPool.SetItemProperties", new DataContext[]
			{
				new ItemPropertiesDataContext(props)
			}).Execute(delegate
			{
				this.WrappedObject.SetItemProperties(props);
			}, true);
		}

		// Token: 0x06001E2D RID: 7725 RVA: 0x0003E6C4 File Offset: 0x0003C8C4
		List<byte[]> IFxProxyPool.GetUploadedMessageIDs()
		{
			List<byte[]> result = null;
			base.CreateContext("IFxProxyPool.GetUploadedMessageIDs", new DataContext[0]).Execute(delegate
			{
				result = this.WrappedObject.GetUploadedMessageIDs();
			}, true);
			return result;
		}

		// Token: 0x0200023E RID: 574
		private class MapiFxProxyExWrapper : FxProxyWrapper, IMapiFxProxyEx, IMapiFxProxy, IDisposeTrackable, IDisposable
		{
			// Token: 0x06001E34 RID: 7732 RVA: 0x0003E894 File Offset: 0x0003CA94
			public MapiFxProxyExWrapper(IMapiFxProxyEx proxy, CommonUtils.CreateContextDelegate createContext) : base(proxy, createContext)
			{
			}

			// Token: 0x06001E35 RID: 7733 RVA: 0x0003E8C4 File Offset: 0x0003CAC4
			void IMapiFxProxyEx.SetProps(PropValueData[] pvda)
			{
				base.CreateContext("IMapiFxProxyEx.SetProps", new DataContext[]
				{
					new PropValuesDataContext(pvda)
				}).Execute(delegate
				{
					((IMapiFxProxyEx)this.WrappedObject).SetProps(pvda);
				}, true);
			}

			// Token: 0x06001E36 RID: 7734 RVA: 0x0003E944 File Offset: 0x0003CB44
			void IMapiFxProxyEx.SetItemProperties(ItemPropertiesBase props)
			{
				base.CreateContext("IMapiFxProxyEx.SetItemProperties", new DataContext[]
				{
					new ItemPropertiesDataContext(props)
				}).Execute(delegate
				{
					((IMapiFxProxyEx)this.WrappedObject).SetItemProperties(props);
				}, true);
			}
		}

		// Token: 0x0200023F RID: 575
		private class MessageProxyWrapper : FxProxyPoolWrapper.MapiFxProxyExWrapper, IMessageProxy, IMapiFxProxyEx, IMapiFxProxy, IDisposeTrackable, IDisposable
		{
			// Token: 0x06001E37 RID: 7735 RVA: 0x0003E99D File Offset: 0x0003CB9D
			public MessageProxyWrapper(IMessageProxy proxy, CommonUtils.CreateContextDelegate createContext) : base(proxy, createContext)
			{
			}

			// Token: 0x06001E38 RID: 7736 RVA: 0x0003E9B9 File Offset: 0x0003CBB9
			void IMessageProxy.SaveChanges()
			{
				base.CreateContext("IMessageProxy.SaveChanges", new DataContext[0]).Execute(delegate
				{
					((IMessageProxy)base.WrappedObject).SaveChanges();
				}, true);
			}

			// Token: 0x06001E39 RID: 7737 RVA: 0x0003EA08 File Offset: 0x0003CC08
			void IMessageProxy.WriteToMime(byte[] buffer)
			{
				base.CreateContext("IMessageProxy.WriteToMime", new DataContext[0]).Execute(delegate
				{
					((IMessageProxy)this.WrappedObject).WriteToMime(buffer);
				}, true);
			}
		}

		// Token: 0x02000240 RID: 576
		private class FolderProxyWrapper : FxProxyPoolWrapper.MapiFxProxyExWrapper, IFolderProxy, IMapiFxProxyEx, IMapiFxProxy, IDisposeTrackable, IDisposable
		{
			// Token: 0x06001E3B RID: 7739 RVA: 0x0003EA51 File Offset: 0x0003CC51
			public FolderProxyWrapper(IFolderProxy proxy, CommonUtils.CreateContextDelegate createContext) : base(proxy, createContext)
			{
			}

			// Token: 0x06001E3C RID: 7740 RVA: 0x0003EA88 File Offset: 0x0003CC88
			IMessageProxy IFolderProxy.OpenMessage(byte[] entryId)
			{
				IMessageProxy result = null;
				base.CreateContext("IFolderProxy.OpenMessage", new DataContext[]
				{
					new EntryIDsDataContext(entryId)
				}).Execute(delegate
				{
					result = ((IFolderProxy)this.WrappedObject).OpenMessage(entryId);
				}, true);
				if (result == null)
				{
					return null;
				}
				return new FxProxyPoolWrapper.MessageProxyWrapper(result, base.CreateContext);
			}

			// Token: 0x06001E3D RID: 7741 RVA: 0x0003EB30 File Offset: 0x0003CD30
			IMessageProxy IFolderProxy.CreateMessage(bool isAssociated)
			{
				IMessageProxy result = null;
				base.CreateContext("IFolderProxy.CreateMessage", new DataContext[]
				{
					new SimpleValueDataContext("IsAssociated", isAssociated)
				}).Execute(delegate
				{
					result = ((IFolderProxy)this.WrappedObject).CreateMessage(isAssociated);
				}, true);
				if (result == null)
				{
					return null;
				}
				return new FxProxyPoolWrapper.MessageProxyWrapper(result, base.CreateContext);
			}

			// Token: 0x06001E3E RID: 7742 RVA: 0x0003EBDC File Offset: 0x0003CDDC
			void IFolderProxy.DeleteMessage(byte[] entryId)
			{
				base.CreateContext("IFolderProxy.DeleteMessage", new DataContext[]
				{
					new EntryIDsDataContext(entryId)
				}).Execute(delegate
				{
					((IFolderProxy)this.WrappedObject).DeleteMessage(entryId);
				}, true);
			}
		}
	}
}
