using System;
using System.Collections.Generic;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Mapi;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x02000236 RID: 566
	internal class FxProxyPoolBudgetWrapper : BudgetWrapperBase<IFxProxyPool>, IFxProxyPool, IDisposable
	{
		// Token: 0x06001E08 RID: 7688 RVA: 0x0003DD97 File Offset: 0x0003BF97
		public FxProxyPoolBudgetWrapper(IFxProxyPool proxy, bool ownsObject, Func<IDisposable> createCostHandleDelegate, Action<uint> chargeDelegate) : base(proxy, ownsObject, createCostHandleDelegate, chargeDelegate)
		{
		}

		// Token: 0x06001E09 RID: 7689 RVA: 0x0003DDA4 File Offset: 0x0003BFA4
		IFolderProxy IFxProxyPool.CreateFolder(FolderRec folder)
		{
			IFolderProxy proxy = null;
			using (base.CreateCostHandle())
			{
				proxy = base.WrappedObject.CreateFolder(folder);
			}
			return new FxProxyPoolBudgetWrapper.FolderProxyBudgetWrapper(proxy, true, base.CreateCostHandle, base.Charge);
		}

		// Token: 0x06001E0A RID: 7690 RVA: 0x0003DDFC File Offset: 0x0003BFFC
		IFolderProxy IFxProxyPool.GetFolderProxy(byte[] folderId)
		{
			IFolderProxy folderProxy = null;
			using (base.CreateCostHandle())
			{
				folderProxy = base.WrappedObject.GetFolderProxy(folderId);
			}
			if (folderProxy == null)
			{
				return null;
			}
			return new FxProxyPoolBudgetWrapper.FolderProxyBudgetWrapper(folderProxy, true, base.CreateCostHandle, base.Charge);
		}

		// Token: 0x06001E0B RID: 7691 RVA: 0x0003DE58 File Offset: 0x0003C058
		EntryIdMap<byte[]> IFxProxyPool.GetFolderData()
		{
			EntryIdMap<byte[]> result = null;
			using (base.CreateCostHandle())
			{
				result = base.WrappedObject.GetFolderData();
			}
			return result;
		}

		// Token: 0x06001E0C RID: 7692 RVA: 0x0003DE9C File Offset: 0x0003C09C
		void IFxProxyPool.Flush()
		{
			using (base.CreateCostHandle())
			{
				base.WrappedObject.Flush();
			}
		}

		// Token: 0x06001E0D RID: 7693 RVA: 0x0003DEDC File Offset: 0x0003C0DC
		void IFxProxyPool.SetItemProperties(ItemPropertiesBase props)
		{
			using (base.CreateCostHandle())
			{
				base.WrappedObject.SetItemProperties(props);
			}
		}

		// Token: 0x06001E0E RID: 7694 RVA: 0x0003DF20 File Offset: 0x0003C120
		List<byte[]> IFxProxyPool.GetUploadedMessageIDs()
		{
			List<byte[]> uploadedMessageIDs;
			using (base.CreateCostHandle())
			{
				uploadedMessageIDs = base.WrappedObject.GetUploadedMessageIDs();
			}
			return uploadedMessageIDs;
		}

		// Token: 0x02000238 RID: 568
		private class MapiFxProxyBudgetExWrapper : FxProxyBudgetWrapper, IMapiFxProxyEx, IMapiFxProxy, IDisposeTrackable, IDisposable
		{
			// Token: 0x06001E13 RID: 7699 RVA: 0x0003E058 File Offset: 0x0003C258
			public MapiFxProxyBudgetExWrapper(IMapiFxProxyEx proxy, bool ownsObject, Func<IDisposable> createCostHandleDelegate, Action<uint> chargeDelegate) : base(proxy, ownsObject, createCostHandleDelegate, chargeDelegate)
			{
			}

			// Token: 0x06001E14 RID: 7700 RVA: 0x0003E068 File Offset: 0x0003C268
			void IMapiFxProxyEx.SetProps(PropValueData[] pvda)
			{
				using (base.CreateCostHandle())
				{
					((IMapiFxProxyEx)base.WrappedObject).SetProps(pvda);
				}
			}

			// Token: 0x06001E15 RID: 7701 RVA: 0x0003E0B0 File Offset: 0x0003C2B0
			void IMapiFxProxyEx.SetItemProperties(ItemPropertiesBase props)
			{
				using (base.CreateCostHandle())
				{
					((IMapiFxProxyEx)base.WrappedObject).SetItemProperties(props);
				}
			}
		}

		// Token: 0x02000239 RID: 569
		private class MessageProxyBudgetWrapper : FxProxyPoolBudgetWrapper.MapiFxProxyBudgetExWrapper, IMessageProxy, IMapiFxProxyEx, IMapiFxProxy, IDisposeTrackable, IDisposable
		{
			// Token: 0x06001E16 RID: 7702 RVA: 0x0003E0F8 File Offset: 0x0003C2F8
			public MessageProxyBudgetWrapper(IMessageProxy proxy, bool ownsObject, Func<IDisposable> createCostHandleDelegate, Action<uint> chargeDelegate) : base(proxy, ownsObject, createCostHandleDelegate, chargeDelegate)
			{
			}

			// Token: 0x06001E17 RID: 7703 RVA: 0x0003E108 File Offset: 0x0003C308
			void IMessageProxy.SaveChanges()
			{
				using (base.CreateCostHandle())
				{
					((IMessageProxy)base.WrappedObject).SaveChanges();
				}
			}

			// Token: 0x06001E18 RID: 7704 RVA: 0x0003E150 File Offset: 0x0003C350
			void IMessageProxy.WriteToMime(byte[] buffer)
			{
				using (base.CreateCostHandle())
				{
					((IMessageProxy)base.WrappedObject).WriteToMime(buffer);
				}
				if (buffer != null)
				{
					base.Charge((uint)buffer.Length);
				}
			}
		}

		// Token: 0x0200023A RID: 570
		private class FolderProxyBudgetWrapper : FxProxyPoolBudgetWrapper.MapiFxProxyBudgetExWrapper, IFolderProxy, IMapiFxProxyEx, IMapiFxProxy, IDisposeTrackable, IDisposable
		{
			// Token: 0x06001E19 RID: 7705 RVA: 0x0003E1A8 File Offset: 0x0003C3A8
			public FolderProxyBudgetWrapper(IFolderProxy proxy, bool ownsObject, Func<IDisposable> createCostHandleDelegate, Action<uint> chargeDelegate) : base(proxy, ownsObject, createCostHandleDelegate, chargeDelegate)
			{
			}

			// Token: 0x06001E1A RID: 7706 RVA: 0x0003E1B8 File Offset: 0x0003C3B8
			IMessageProxy IFolderProxy.OpenMessage(byte[] entryId)
			{
				IMessageProxy messageProxy = null;
				using (base.CreateCostHandle())
				{
					messageProxy = ((IFolderProxy)base.WrappedObject).OpenMessage(entryId);
				}
				if (messageProxy == null)
				{
					return null;
				}
				return new FxProxyPoolBudgetWrapper.MessageProxyBudgetWrapper(messageProxy, true, base.CreateCostHandle, base.Charge);
			}

			// Token: 0x06001E1B RID: 7707 RVA: 0x0003E21C File Offset: 0x0003C41C
			IMessageProxy IFolderProxy.CreateMessage(bool isAssociated)
			{
				IMessageProxy messageProxy = null;
				using (base.CreateCostHandle())
				{
					messageProxy = ((IFolderProxy)base.WrappedObject).CreateMessage(isAssociated);
				}
				if (messageProxy == null)
				{
					return null;
				}
				return new FxProxyPoolBudgetWrapper.MessageProxyBudgetWrapper(messageProxy, true, base.CreateCostHandle, base.Charge);
			}

			// Token: 0x06001E1C RID: 7708 RVA: 0x0003E280 File Offset: 0x0003C480
			void IFolderProxy.DeleteMessage(byte[] entryId)
			{
				using (base.CreateCostHandle())
				{
					((IFolderProxy)base.WrappedObject).DeleteMessage(entryId);
				}
			}
		}
	}
}
