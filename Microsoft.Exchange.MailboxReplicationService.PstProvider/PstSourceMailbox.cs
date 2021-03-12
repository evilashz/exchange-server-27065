﻿using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.PST;
using Microsoft.Exchange.RpcClientAccess;
using Microsoft.Exchange.RpcClientAccess.FastTransfer.Handler;
using Microsoft.Exchange.RpcClientAccess.FastTransfer.Parser;
using Microsoft.Mapi;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x02000013 RID: 19
	internal class PstSourceMailbox : PstMailbox, ISourceMailbox, IMailbox, IDisposable
	{
		// Token: 0x060000EB RID: 235 RVA: 0x000067B6 File Offset: 0x000049B6
		byte[] ISourceMailbox.GetMailboxBasicInfo(MailboxSignatureFlags flags)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060000EC RID: 236 RVA: 0x000067C0 File Offset: 0x000049C0
		ISourceFolder ISourceMailbox.GetFolder(byte[] entryId)
		{
			MrsTracer.Provider.Function("PstSourceMailbox.GetFolder({0})", new object[]
			{
				TraceUtils.DumpEntryId(entryId)
			});
			return base.GetFolder<PstSourceFolder>(entryId);
		}

		// Token: 0x060000ED RID: 237 RVA: 0x000067F4 File Offset: 0x000049F4
		void ISourceMailbox.CopyTo(IFxProxy destMailboxProxy, PropTag[] excludeTags)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060000EE RID: 238 RVA: 0x000067FB File Offset: 0x000049FB
		void ISourceMailbox.SetMailboxSyncState(string syncStateStr)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060000EF RID: 239 RVA: 0x00006802 File Offset: 0x00004A02
		string ISourceMailbox.GetMailboxSyncState()
		{
			throw new NotImplementedException();
		}

		// Token: 0x060000F0 RID: 240 RVA: 0x00006809 File Offset: 0x00004A09
		MailboxChangesManifest ISourceMailbox.EnumerateHierarchyChanges(EnumerateHierarchyChangesFlags flags, int maxChanges)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060000F1 RID: 241 RVA: 0x00006810 File Offset: 0x00004A10
		void ISourceMailbox.ExportMessages(List<MessageRec> messages, IFxProxyPool proxyPool, ExportMessagesFlags flags, PropTag[] propsToCopyExplicitly, PropTag[] excludeProps)
		{
			MrsTracer.Provider.Function("PstSourceMailbox.ExportMessages({0} messages)", new object[]
			{
				messages.Count
			});
			this.CopyMessagesOneByOne(messages, proxyPool, propsToCopyExplicitly, excludeProps, null);
		}

		// Token: 0x060000F2 RID: 242 RVA: 0x0000684F File Offset: 0x00004A4F
		void ISourceMailbox.ExportFolders(List<byte[]> folderIds, IFxProxyPool proxyPool, ExportFoldersDataToCopyFlags exportFoldersDataToCopyFlags, GetFolderRecFlags folderRecFlags, PropTag[] additionalFolderRecProps, CopyPropertiesFlags copyPropertiesFlags, PropTag[] excludeProps, AclFlags extendedAclFlags)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060000F3 RID: 243 RVA: 0x00006856 File Offset: 0x00004A56
		List<ReplayActionResult> ISourceMailbox.ReplayActions(List<ReplayAction> actions)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060000F4 RID: 244 RVA: 0x0000685D File Offset: 0x00004A5D
		protected override DisposeTracker InternalGetDisposeTracker()
		{
			return DisposeTracker.Get<PstSourceMailbox>(this);
		}

		// Token: 0x060000F5 RID: 245 RVA: 0x000069DC File Offset: 0x00004BDC
		protected override void CopySingleMessage(MessageRec message, IFolderProxy targetFolderProxy, PropTag[] propsToCopyExplicitly, PropTag[] propTagsToExclude)
		{
			ExecutionContext.Create(new DataContext[]
			{
				new OperationDataContext("PstSourceMailbox.CopySingleMessage", OperationType.None),
				new EntryIDsDataContext(message.EntryId)
			}).Execute(delegate
			{
				try
				{
					uint nodeIdFromEntryId = PstMailbox.GetNodeIdFromEntryId(this.IPst.MessageStore.Guid, message.EntryId);
					IMessage message2 = this.IPst.ReadMessage(nodeIdFromEntryId);
					if (message2 == null)
					{
						throw new UnableToReadPSTMessagePermanentException(this.IPst.FileName, nodeIdFromEntryId);
					}
					PSTMessage pstmessage = new PSTMessage(this, message2);
					using (IMessageProxy messageProxy = targetFolderProxy.OpenMessage(message.EntryId))
					{
						FxCollectorSerializer fxCollectorSerializer = new FxCollectorSerializer(messageProxy);
						fxCollectorSerializer.Config(0, 1);
						using (FastTransferDownloadContext fastTransferDownloadContext = FastTransferDownloadContext.CreateForDownload(FastTransferSendOption.Unicode | FastTransferSendOption.UseCpId | FastTransferSendOption.ForceUnicode, 1U, pstmessage.RawPropertyBag.CachedEncoding, NullResourceTracker.Instance, this.GetPropertyFilterFactory(PstMailbox.MoMTPtaFromPta(propTagsToExclude)), false))
						{
							FastTransferMessageCopyTo fastTransferObject = new FastTransferMessageCopyTo(false, pstmessage, true);
							fastTransferDownloadContext.PushInitial(fastTransferObject);
							FxUtils.TransferFxBuffers(fastTransferDownloadContext, fxCollectorSerializer);
							messageProxy.SaveChanges();
						}
					}
				}
				catch (PSTExceptionBase innerException)
				{
					throw new UnableToReadPSTMessagePermanentException(this.IPst.FileName, PstMailbox.GetNodeIdFromEntryId(this.IPst.MessageStore.Guid, message.EntryId), innerException);
				}
			});
		}

		// Token: 0x060000F6 RID: 246 RVA: 0x00006A4C File Offset: 0x00004C4C
		private static HashSet<PropertyTag> GetPropertyTagsToAlwaysExclude()
		{
			if (PstSourceMailbox.propertyTagsToAlwaysExclude == null)
			{
				HashSet<PropertyTag> hashSet = new HashSet<PropertyTag>(PropertyFilterFactory.ExcludePropertiesForFxMessageCommon);
				hashSet.UnionWith(PropertyTag.OneOffEntryIdPropertyTags);
				hashSet.UnionWith(new PropertyTag[]
				{
					PropertyTag.CreationTime,
					PropertyTag.LastModificationTime
				});
				hashSet.Remove(PropertyTag.SenderEntryId);
				hashSet.Remove(PropertyTag.ReceivedRepresentingEntryId);
				hashSet.Remove(PropertyTag.ReceivedByEntryId);
				PstSourceMailbox.propertyTagsToAlwaysExclude = hashSet.ToArray<PropertyTag>();
			}
			return new HashSet<PropertyTag>(PstSourceMailbox.propertyTagsToAlwaysExclude);
		}

		// Token: 0x060000F7 RID: 247 RVA: 0x00006AE0 File Offset: 0x00004CE0
		private PropertyFilterFactory GetPropertyFilterFactory(PropertyTag[] additionalPropertyTags)
		{
			if (this.propertyTagsToExclude != null && (additionalPropertyTags == null || this.propertyTagsToExclude.IsSupersetOf(additionalPropertyTags)))
			{
				return this.propertyFilterFactory;
			}
			this.propertyTagsToExclude = PstSourceMailbox.GetPropertyTagsToAlwaysExclude();
			if (additionalPropertyTags != null)
			{
				this.propertyTagsToExclude.UnionWith(additionalPropertyTags);
			}
			this.propertyFilterFactory = new PropertyFilterFactory(false, false, this.propertyTagsToExclude.ToArray<PropertyTag>());
			return this.propertyFilterFactory;
		}

		// Token: 0x04000044 RID: 68
		private static PropertyTag[] propertyTagsToAlwaysExclude;

		// Token: 0x04000045 RID: 69
		private HashSet<PropertyTag> propertyTagsToExclude;

		// Token: 0x04000046 RID: 70
		private PropertyFilterFactory propertyFilterFactory;
	}
}
