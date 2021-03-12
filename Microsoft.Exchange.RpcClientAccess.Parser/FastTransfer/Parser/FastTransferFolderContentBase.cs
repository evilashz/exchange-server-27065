using System;
using System.Collections.Generic;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.RpcClientAccess.FastTransfer.Parser
{
	// Token: 0x02000160 RID: 352
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal abstract class FastTransferFolderContentBase : FastTransferObject
	{
		// Token: 0x06000691 RID: 1681 RVA: 0x00013282 File Offset: 0x00011482
		protected FastTransferFolderContentBase(IFolder folder, FastTransferFolderContentBase.IncludeSubObject includeSubObject, bool isTopLevel) : base(isTopLevel)
		{
			Util.ThrowOnNullArgument(folder, "folder");
			this.folder = folder;
			this.includeSubObject = includeSubObject;
		}

		// Token: 0x06000692 RID: 1682 RVA: 0x000132A4 File Offset: 0x000114A4
		protected override void InternalDispose()
		{
			this.folder.Dispose();
			base.InternalDispose();
		}

		// Token: 0x17000132 RID: 306
		// (get) Token: 0x06000693 RID: 1683 RVA: 0x000132B7 File Offset: 0x000114B7
		protected IFolder Folder
		{
			get
			{
				return this.folder;
			}
		}

		// Token: 0x17000133 RID: 307
		// (get) Token: 0x06000694 RID: 1684 RVA: 0x000132BF File Offset: 0x000114BF
		protected FastTransferFolderContentBase.IncludeSubObject Include
		{
			get
			{
				return this.includeSubObject;
			}
		}

		// Token: 0x06000695 RID: 1685 RVA: 0x00013364 File Offset: 0x00011564
		protected IEnumerator<FastTransferStateMachine?> DownloadProperties(FastTransferDownloadContext context, IPropertyFilter filter)
		{
			yield return new FastTransferStateMachine?(context.CreateStateMachine(new FastTransferPropList(this.folder.PropertyBag, filter)));
			yield break;
		}

		// Token: 0x06000696 RID: 1686 RVA: 0x000136A0 File Offset: 0x000118A0
		protected IEnumerator<FastTransferStateMachine?> DownloadSubObjects(FastTransferDownloadContext context, bool includeFXDelProp)
		{
			bool distributedFolderInfoSent = false;
			bool isContentAvailable = this.Folder.IsContentAvailable;
			FastTransferFolderContentBase.IncludeSubObject[] subObjectOrder = includeFXDelProp ? FastTransferFolderContentBase.AssociatedMessagesFirst : FastTransferFolderContentBase.NormalMessagesFirst;
			foreach (FastTransferFolderContentBase.IncludeSubObject subObject in subObjectOrder)
			{
				bool isNormalMessages = subObject == FastTransferFolderContentBase.IncludeSubObject.Messages;
				if ((this.Include & subObject) == subObject)
				{
					if (includeFXDelProp)
					{
						yield return new FastTransferStateMachine?(FastTransferPropertyValue.Serialize(context, isNormalMessages ? FastTransferFolderContentBase.FXDelPropContainerContentsPropertyValue : FastTransferFolderContentBase.FXDelPropFolderAssociatedContentsPropertyValue));
						distributedFolderInfoSent = false;
					}
					if (isContentAvailable)
					{
						yield return new FastTransferStateMachine?(new FastTransferStateMachine(this.DownloadMessages(context, isNormalMessages ? FastTransferMessage.MessageType.Normal : FastTransferMessage.MessageType.Associated, isNormalMessages ? this.Folder.GetContents() : this.Folder.GetAssociatedContents())));
					}
					else if (!distributedFolderInfoSent)
					{
						yield return new FastTransferStateMachine?(new FastTransferStateMachine(this.DownloadDistributedFolderInfo(context)));
						distributedFolderInfoSent = true;
					}
				}
			}
			if ((this.Include & FastTransferFolderContentBase.IncludeSubObject.Subfolders) == FastTransferFolderContentBase.IncludeSubObject.Subfolders)
			{
				if (includeFXDelProp)
				{
					yield return new FastTransferStateMachine?(FastTransferPropertyValue.Serialize(context, FastTransferFolderContentBase.FXDelPropContainerHierarchyPropertyValue));
				}
				yield return new FastTransferStateMachine?(new FastTransferStateMachine(this.DownloadSubfolders(context)));
			}
			yield break;
		}

		// Token: 0x06000697 RID: 1687 RVA: 0x0001382C File Offset: 0x00011A2C
		private IEnumerator<FastTransferStateMachine?> DownloadMessages(FastTransferDownloadContext context, FastTransferMessage.MessageType messageType, IEnumerable<IMessage> messageEnumerable)
		{
			foreach (IMessage message in messageEnumerable)
			{
				FastTransferMessage fastTransferMessage = this.CreateDownloadFastTransferMessage(message, messageType);
				yield return new FastTransferStateMachine?(context.CreateStateMachine(fastTransferMessage));
			}
			yield break;
		}

		// Token: 0x06000698 RID: 1688 RVA: 0x00013860 File Offset: 0x00011A60
		private FastTransferMessage CreateDownloadFastTransferMessage(IMessage message, FastTransferMessage.MessageType messageType)
		{
			FastTransferMessage result;
			using (DisposeGuard disposeGuard = default(DisposeGuard))
			{
				disposeGuard.Add<IMessage>(message);
				FastTransferMessage fastTransferMessage = new FastTransferMessage(message, messageType, false);
				disposeGuard.Add<FastTransferMessage>(fastTransferMessage);
				disposeGuard.Success();
				result = fastTransferMessage;
			}
			return result;
		}

		// Token: 0x06000699 RID: 1689 RVA: 0x00013A20 File Offset: 0x00011C20
		private IEnumerator<FastTransferStateMachine?> DownloadSubfolders(FastTransferDownloadContext context)
		{
			foreach (IFolder subFolder in this.folder.GetFolders())
			{
				IFastTransferProcessor<FastTransferDownloadContext> fastTransferFolderContent = this.CreateDownloadFastTransferFolderContent(subFolder);
				yield return new FastTransferStateMachine?(context.CreateStateMachine(fastTransferFolderContent));
			}
			yield break;
		}

		// Token: 0x0600069A RID: 1690 RVA: 0x00013A44 File Offset: 0x00011C44
		private IFastTransferProcessor<FastTransferDownloadContext> CreateDownloadFastTransferFolderContent(IFolder subFolder)
		{
			IFastTransferProcessor<FastTransferDownloadContext> result;
			using (DisposeGuard disposeGuard = default(DisposeGuard))
			{
				disposeGuard.Add<IFolder>(subFolder);
				FastTransferFolderContent fastTransferFolderContent = new FastTransferFolderContent(subFolder, FastTransferFolderContentBase.IncludeSubObject.All, false);
				disposeGuard.Add<FastTransferFolderContent>(fastTransferFolderContent);
				FastTransferDownloadDelimitedObject fastTransferDownloadDelimitedObject = new FastTransferDownloadDelimitedObject(fastTransferFolderContent, PropertyTag.StartSubFld, PropertyTag.EndFolder);
				disposeGuard.Add<FastTransferDownloadDelimitedObject>(fastTransferDownloadDelimitedObject);
				disposeGuard.Success();
				result = fastTransferDownloadDelimitedObject;
			}
			return result;
		}

		// Token: 0x0600069B RID: 1691 RVA: 0x00013BE4 File Offset: 0x00011DE4
		private IEnumerator<FastTransferStateMachine?> DownloadDistributedFolderInfo(FastTransferDownloadContext context)
		{
			StoreLongTermId folderLongTermId = this.Folder.GetLongTermId();
			ushort localSiteDatabaseCount;
			string[] replicaDatabases = this.Folder.GetReplicaDatabases(out localSiteDatabaseCount);
			FastTransferDistributedFolderInfo distributedFolderInfo = new FastTransferDistributedFolderInfo(0U, 0U, folderLongTermId, (uint)localSiteDatabaseCount, replicaDatabases);
			yield return new FastTransferStateMachine?(FastTransferPropertyValue.Serialize(context, new PropertyValue(PropertyTag.NewFXFolder, BufferWriter.Serialize(delegate(Writer writer)
			{
				distributedFolderInfo.Serialize(writer);
			}))));
			context.DataInterface.ForceBufferFull();
			yield break;
		}

		// Token: 0x0600069C RID: 1692 RVA: 0x00013D90 File Offset: 0x00011F90
		protected IEnumerator<FastTransferStateMachine?> UploadProperties(FastTransferUploadContext context)
		{
			if (base.IsTopLevel)
			{
				context.SetEndOfBufferAction(delegate
				{
					this.Folder.Save();
				});
			}
			try
			{
				yield return new FastTransferStateMachine?(context.CreateStateMachine(new FastTransferPropList(this.folder.PropertyBag)));
			}
			finally
			{
				if (base.IsTopLevel)
				{
					context.SetEndOfBufferAction(null);
				}
			}
			this.Folder.Save();
			yield break;
		}

		// Token: 0x0600069D RID: 1693 RVA: 0x00013F08 File Offset: 0x00012108
		protected IEnumerator<FastTransferStateMachine?> UploadMessages(FastTransferUploadContext context)
		{
			while (!context.NoMoreData)
			{
				PropertyTag propertyTag;
				if (context.DataInterface.TryPeekMarker(out propertyTag))
				{
					if (!(propertyTag == PropertyTag.StartMessage) && !(propertyTag == PropertyTag.StartFAIMsg))
					{
						break;
					}
					FastTransferMessage.MessageType messageType = (propertyTag == PropertyTag.StartFAIMsg) ? FastTransferMessage.MessageType.Associated : FastTransferMessage.MessageType.Normal;
					FastTransferMessage fastTransferMessage = this.CreateUploadFastTransferMessage(messageType);
					yield return new FastTransferStateMachine?(context.CreateStateMachine(fastTransferMessage));
				}
				else
				{
					if (!propertyTag.IsMetaProperty)
					{
						break;
					}
					yield return new FastTransferStateMachine?(FastTransferPropertyValue.SkipPropertyIfExists(context, propertyTag));
				}
			}
			yield break;
		}

		// Token: 0x0600069E RID: 1694 RVA: 0x00013F2C File Offset: 0x0001212C
		private FastTransferMessage CreateUploadFastTransferMessage(FastTransferMessage.MessageType messageType)
		{
			FastTransferMessage result;
			using (DisposeGuard disposeGuard = default(DisposeGuard))
			{
				IMessage message = this.folder.CreateMessage(messageType == FastTransferMessage.MessageType.Associated);
				disposeGuard.Add<IMessage>(message);
				FastTransferMessage fastTransferMessage = new FastTransferMessage(message, messageType, false);
				disposeGuard.Add<FastTransferMessage>(fastTransferMessage);
				disposeGuard.Success();
				result = fastTransferMessage;
			}
			return result;
		}

		// Token: 0x0600069F RID: 1695 RVA: 0x000140B4 File Offset: 0x000122B4
		protected IEnumerator<FastTransferStateMachine?> UploadSubfolders(FastTransferUploadContext context)
		{
			while (!context.NoMoreData)
			{
				PropertyTag propertyTag;
				if (context.DataInterface.TryPeekMarker(out propertyTag))
				{
					if (!(propertyTag == PropertyTag.StartSubFld))
					{
						break;
					}
					IFastTransferProcessor<FastTransferUploadContext> fastTransferFolderContent = this.CreateUploadFastTransferFolderContent();
					yield return new FastTransferStateMachine?(context.CreateStateMachine(fastTransferFolderContent));
				}
				else
				{
					if (!propertyTag.IsMetaProperty)
					{
						break;
					}
					yield return new FastTransferStateMachine?(FastTransferPropertyValue.SkipPropertyIfExists(context, propertyTag));
				}
			}
			yield break;
		}

		// Token: 0x060006A0 RID: 1696 RVA: 0x000140D8 File Offset: 0x000122D8
		private IFastTransferProcessor<FastTransferUploadContext> CreateUploadFastTransferFolderContent()
		{
			IFastTransferProcessor<FastTransferUploadContext> result;
			using (DisposeGuard disposeGuard = default(DisposeGuard))
			{
				IFolder disposable = this.folder.CreateFolder();
				disposeGuard.Add<IFolder>(disposable);
				FastTransferFolderContent fastTransferFolderContent = new FastTransferFolderContent(disposable, FastTransferFolderContentBase.IncludeSubObject.All, false);
				disposeGuard.Add<FastTransferFolderContent>(fastTransferFolderContent);
				FastTransferUploadDelimitedObject fastTransferUploadDelimitedObject = new FastTransferUploadDelimitedObject(fastTransferFolderContent, PropertyTag.StartSubFld, PropertyTag.EndFolder);
				disposeGuard.Add<FastTransferUploadDelimitedObject>(fastTransferUploadDelimitedObject);
				disposeGuard.Success();
				result = fastTransferUploadDelimitedObject;
			}
			return result;
		}

		// Token: 0x04000357 RID: 855
		private static readonly PropertyValue FXDelPropContainerContentsPropertyValue = new PropertyValue(PropertyTag.FXDelProp, (int)PropertyTag.ContainerContents);

		// Token: 0x04000358 RID: 856
		private static readonly PropertyValue FXDelPropContainerHierarchyPropertyValue = new PropertyValue(PropertyTag.FXDelProp, (int)PropertyTag.ContainerHierarchy);

		// Token: 0x04000359 RID: 857
		private static readonly PropertyValue FXDelPropFolderAssociatedContentsPropertyValue = new PropertyValue(PropertyTag.FXDelProp, (int)PropertyTag.FolderAssociatedContents);

		// Token: 0x0400035A RID: 858
		private static readonly FastTransferFolderContentBase.IncludeSubObject[] AssociatedMessagesFirst = new FastTransferFolderContentBase.IncludeSubObject[]
		{
			FastTransferFolderContentBase.IncludeSubObject.AssociatedMessages,
			FastTransferFolderContentBase.IncludeSubObject.Messages
		};

		// Token: 0x0400035B RID: 859
		private static readonly FastTransferFolderContentBase.IncludeSubObject[] NormalMessagesFirst = new FastTransferFolderContentBase.IncludeSubObject[]
		{
			FastTransferFolderContentBase.IncludeSubObject.Messages,
			FastTransferFolderContentBase.IncludeSubObject.AssociatedMessages
		};

		// Token: 0x0400035C RID: 860
		private readonly IFolder folder;

		// Token: 0x0400035D RID: 861
		private readonly FastTransferFolderContentBase.IncludeSubObject includeSubObject;

		// Token: 0x02000161 RID: 353
		[Flags]
		public enum IncludeSubObject
		{
			// Token: 0x0400035F RID: 863
			None = 0,
			// Token: 0x04000360 RID: 864
			Messages = 1,
			// Token: 0x04000361 RID: 865
			AssociatedMessages = 2,
			// Token: 0x04000362 RID: 866
			Subfolders = 4,
			// Token: 0x04000363 RID: 867
			All = 7
		}
	}
}
