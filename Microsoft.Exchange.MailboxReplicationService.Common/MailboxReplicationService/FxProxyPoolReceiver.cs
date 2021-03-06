using System;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x02000124 RID: 292
	internal class FxProxyPoolReceiver : DisposableWrapper<IFxProxyPool>, IDataImport, IDisposable
	{
		// Token: 0x06000A1E RID: 2590 RVA: 0x000146C1 File Offset: 0x000128C1
		public FxProxyPoolReceiver(IFxProxyPool destination, bool ownsDestination) : base(destination, ownsDestination)
		{
			this.currentFolder = null;
			this.currentMessage = null;
		}

		// Token: 0x17000320 RID: 800
		// (get) Token: 0x06000A1F RID: 2591 RVA: 0x000146D9 File Offset: 0x000128D9
		private IMapiFxProxyEx CurrentEntry
		{
			get
			{
				if (this.currentMessage != null)
				{
					return this.currentMessage;
				}
				if (this.currentFolder != null)
				{
					return this.currentFolder;
				}
				throw new InvalidProxyOperationOrderPermanentException();
			}
		}

		// Token: 0x17000321 RID: 801
		// (get) Token: 0x06000A20 RID: 2592 RVA: 0x000146FE File Offset: 0x000128FE
		private IMessageProxy CurrentMessage
		{
			get
			{
				return this.currentMessage;
			}
		}

		// Token: 0x17000322 RID: 802
		// (get) Token: 0x06000A21 RID: 2593 RVA: 0x00014706 File Offset: 0x00012906
		private IFolderProxy CurrentFolder
		{
			get
			{
				if (this.currentMessage != null)
				{
					this.currentMessage.Dispose();
					this.currentMessage = null;
				}
				return this.currentFolder;
			}
		}

		// Token: 0x06000A22 RID: 2594 RVA: 0x00014728 File Offset: 0x00012928
		IDataMessage IDataImport.SendMessageAndWaitForReply(IDataMessage message)
		{
			if (message is FlushMessage)
			{
				base.WrappedObject.Flush();
				return null;
			}
			if (message is FxProxyPoolGetFolderDataRequestMessage)
			{
				return new FxProxyPoolGetFolderDataResponseMessage(base.WrappedObject.GetFolderData());
			}
			if (message is FxProxyPoolGetUploadedIDsRequestMessage)
			{
				return new FxProxyPoolGetUploadedIDsResponseMessage(base.WrappedObject.GetUploadedMessageIDs());
			}
			throw new UnexpectedErrorPermanentException(-2147024809);
		}

		// Token: 0x06000A23 RID: 2595 RVA: 0x00014788 File Offset: 0x00012988
		void IDataImport.SendMessage(IDataMessage message)
		{
			FxProxyPoolOpenFolderMessage fxProxyPoolOpenFolderMessage = message as FxProxyPoolOpenFolderMessage;
			if (fxProxyPoolOpenFolderMessage != null)
			{
				this.ClearCurrentObjectsReferences();
				this.currentFolder = base.WrappedObject.GetFolderProxy(fxProxyPoolOpenFolderMessage.Buffer);
				return;
			}
			FxProxyPoolCreateFolderMessage fxProxyPoolCreateFolderMessage = message as FxProxyPoolCreateFolderMessage;
			if (fxProxyPoolCreateFolderMessage != null)
			{
				this.ClearCurrentObjectsReferences();
				this.currentFolder = base.WrappedObject.CreateFolder(fxProxyPoolCreateFolderMessage.Data);
				return;
			}
			FxProxyPoolSetItemPropertiesMessage fxProxyPoolSetItemPropertiesMessage = message as FxProxyPoolSetItemPropertiesMessage;
			if (fxProxyPoolSetItemPropertiesMessage != null)
			{
				if (fxProxyPoolSetItemPropertiesMessage.Props != null)
				{
					if (this.currentMessage != null)
					{
						this.currentMessage.SetItemProperties(fxProxyPoolSetItemPropertiesMessage.Props);
						return;
					}
					if (this.currentFolder != null)
					{
						this.currentFolder.SetItemProperties(fxProxyPoolSetItemPropertiesMessage.Props);
						return;
					}
					base.WrappedObject.SetItemProperties(fxProxyPoolSetItemPropertiesMessage.Props);
				}
				return;
			}
			if (this.currentFolder == null)
			{
				throw new FolderIsMissingTransientException();
			}
			FxProxyPoolOpenItemMessage fxProxyPoolOpenItemMessage = message as FxProxyPoolOpenItemMessage;
			if (fxProxyPoolOpenItemMessage != null)
			{
				this.currentMessage = this.CurrentFolder.OpenMessage(fxProxyPoolOpenItemMessage.Buffer);
				return;
			}
			FxProxyPoolCreateItemMessage fxProxyPoolCreateItemMessage = message as FxProxyPoolCreateItemMessage;
			if (fxProxyPoolCreateItemMessage != null)
			{
				this.currentMessage = this.CurrentFolder.CreateMessage(fxProxyPoolCreateItemMessage.CreateFAI);
				return;
			}
			FxProxyPoolDeleteItemMessage fxProxyPoolDeleteItemMessage = message as FxProxyPoolDeleteItemMessage;
			if (fxProxyPoolDeleteItemMessage != null)
			{
				this.CurrentFolder.DeleteMessage(fxProxyPoolDeleteItemMessage.Buffer);
				return;
			}
			FxProxyPoolCloseEntryMessage fxProxyPoolCloseEntryMessage = message as FxProxyPoolCloseEntryMessage;
			if (fxProxyPoolCloseEntryMessage != null)
			{
				if (this.currentMessage != null)
				{
					this.currentMessage.Dispose();
					this.currentMessage = null;
					return;
				}
				if (this.currentFolder != null)
				{
					this.currentFolder.Dispose();
					this.currentFolder = null;
				}
				return;
			}
			else
			{
				FxProxyPoolSetPropsMessage fxProxyPoolSetPropsMessage = message as FxProxyPoolSetPropsMessage;
				if (fxProxyPoolSetPropsMessage != null)
				{
					this.CurrentEntry.SetProps(fxProxyPoolSetPropsMessage.PropValues);
					return;
				}
				FxProxyPoolSetExtendedAclMessage fxProxyPoolSetExtendedAclMessage = message as FxProxyPoolSetExtendedAclMessage;
				if (fxProxyPoolSetExtendedAclMessage != null)
				{
					this.CurrentFolder.SetItemProperties(new FolderAcl(fxProxyPoolSetExtendedAclMessage.AclFlags, fxProxyPoolSetExtendedAclMessage.AclData));
					return;
				}
				FxProxyPoolSaveChangesMessage fxProxyPoolSaveChangesMessage = message as FxProxyPoolSaveChangesMessage;
				if (fxProxyPoolSaveChangesMessage != null)
				{
					this.CurrentMessage.SaveChanges();
					return;
				}
				FxProxyPoolWriteToMimeMessage fxProxyPoolWriteToMimeMessage = message as FxProxyPoolWriteToMimeMessage;
				if (fxProxyPoolWriteToMimeMessage != null)
				{
					this.CurrentMessage.WriteToMime(fxProxyPoolWriteToMimeMessage.Buffer);
					return;
				}
				FxProxyImportBufferMessage fxProxyImportBufferMessage = message as FxProxyImportBufferMessage;
				if (fxProxyImportBufferMessage != null)
				{
					this.CurrentEntry.ProcessRequest(fxProxyImportBufferMessage.Opcode, fxProxyImportBufferMessage.Buffer);
					return;
				}
				throw new UnexpectedErrorPermanentException(-2147024809);
			}
		}

		// Token: 0x06000A24 RID: 2596 RVA: 0x000149A4 File Offset: 0x00012BA4
		protected override void InternalDispose(bool disposing)
		{
			if (disposing)
			{
				this.ClearCurrentObjectsReferences();
			}
			base.InternalDispose(disposing);
		}

		// Token: 0x06000A25 RID: 2597 RVA: 0x000149B6 File Offset: 0x00012BB6
		private void ClearCurrentObjectsReferences()
		{
			if (this.currentMessage != null)
			{
				this.currentMessage.Dispose();
				this.currentMessage = null;
			}
			if (this.currentFolder != null)
			{
				this.currentFolder.Dispose();
				this.currentFolder = null;
			}
		}

		// Token: 0x040005DF RID: 1503
		private IFolderProxy currentFolder;

		// Token: 0x040005E0 RID: 1504
		private IMessageProxy currentMessage;
	}
}
