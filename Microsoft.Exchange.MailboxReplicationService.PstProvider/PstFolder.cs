using System;
using System.Collections.Generic;
using System.Security.AccessControl;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.PST;
using Microsoft.Exchange.RpcClientAccess;
using Microsoft.Mapi;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x02000005 RID: 5
	internal abstract class PstFolder : DisposeTrackableBase, IFolder, IDisposable
	{
		// Token: 0x0600005A RID: 90 RVA: 0x000035AB File Offset: 0x000017AB
		public PstFolder()
		{
		}

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x0600005B RID: 91 RVA: 0x000035B3 File Offset: 0x000017B3
		public PstFxFolder Folder
		{
			get
			{
				return this.folder;
			}
		}

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x0600005C RID: 92 RVA: 0x000035BB File Offset: 0x000017BB
		public byte[] FolderId
		{
			get
			{
				return this.folderId;
			}
		}

		// Token: 0x0600005D RID: 93 RVA: 0x000035C4 File Offset: 0x000017C4
		FolderRec IFolder.GetFolderRec(PropTag[] additionalPtagsToLoad, GetFolderRecFlags flags)
		{
			if (this.folderRec != null)
			{
				return this.folderRec;
			}
			PropTag[] pta;
			if (additionalPtagsToLoad != null)
			{
				List<PropTag> list = new List<PropTag>();
				list.AddRange(FolderRec.PtagsToLoad);
				list.AddRange(additionalPtagsToLoad);
				pta = list.ToArray();
			}
			else
			{
				pta = FolderRec.PtagsToLoad;
			}
			PropertyValue[] momtPva = null;
			try
			{
				momtPva = this.folder.GetProps(PstMailbox.MoMTPtaFromPta(pta));
			}
			catch (PSTIOException innerException)
			{
				throw new UnableToGetPSTFolderPropsTransientException(BitConverter.ToUInt32(this.folderId, 0), innerException);
			}
			catch (PSTExceptionBase innerException2)
			{
				uint nodeIdFromEntryId = PstMailbox.GetNodeIdFromEntryId(this.folder.PstMailbox.IPst.MessageStore.Guid, this.folderId);
				throw new UnableToGetPSTFolderPropsPermanentException(nodeIdFromEntryId, innerException2);
			}
			this.folderRec = FolderRec.Create(PstMailbox.PvaFromMoMTPva(momtPva));
			return this.folderRec;
		}

		// Token: 0x0600005E RID: 94 RVA: 0x0000369C File Offset: 0x0000189C
		List<MessageRec> IFolder.EnumerateMessages(EnumerateMessagesFlags emFlags, PropTag[] additionalPtagsToLoad)
		{
			List<MessageRec> list = new List<MessageRec>();
			this.EnumerateMessagesHelper(this.folder.IPstFolder.AssociatedMessageIds, additionalPtagsToLoad, list);
			this.EnumerateMessagesHelper(this.folder.IPstFolder.MessageIds, additionalPtagsToLoad, list);
			MrsTracer.Provider.Debug("PstFolder.EnumerateMessages returns {0} items.", new object[]
			{
				list.Count
			});
			return list;
		}

		// Token: 0x0600005F RID: 95 RVA: 0x00003708 File Offset: 0x00001908
		List<MessageRec> IFolder.LookupMessages(PropTag ptagToLookup, List<byte[]> keysToLookup, PropTag[] additionalPtagsToLoad)
		{
			if (ptagToLookup != PropTag.EntryId)
			{
				throw new NotImplementedException();
			}
			List<uint> list = new List<uint>(keysToLookup.Count);
			foreach (byte[] entryId in keysToLookup)
			{
				list.Add(PstMailbox.GetNodeIdFromEntryId(this.folder.PstMailbox.IPst.MessageStore.Guid, entryId));
			}
			List<MessageRec> list2 = new List<MessageRec>();
			this.EnumerateMessagesHelper(list, additionalPtagsToLoad, list2);
			return list2;
		}

		// Token: 0x06000060 RID: 96 RVA: 0x000037A0 File Offset: 0x000019A0
		RawSecurityDescriptor IFolder.GetSecurityDescriptor(SecurityProp secProp)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000061 RID: 97 RVA: 0x000037A7 File Offset: 0x000019A7
		void IFolder.DeleteMessages(byte[][] entryIds)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000062 RID: 98 RVA: 0x000037AE File Offset: 0x000019AE
		byte[] IFolder.GetFolderId()
		{
			return this.FolderId;
		}

		// Token: 0x06000063 RID: 99 RVA: 0x000037B6 File Offset: 0x000019B6
		void IFolder.SetContentsRestriction(RestrictionData restriction)
		{
		}

		// Token: 0x06000064 RID: 100 RVA: 0x000037B8 File Offset: 0x000019B8
		PropValueData[] IFolder.GetProps(PropTag[] pta)
		{
			MrsTracer.Provider.Function("PstFolder.IFolder.GetProps", new object[0]);
			PropValue[] a = PstMailbox.PvaFromMoMTPva(this.folder.GetProps(PstMailbox.MoMTPtaFromPta(pta)));
			return DataConverter<PropValueConverter, PropValue, PropValueData>.GetData(a);
		}

		// Token: 0x06000065 RID: 101 RVA: 0x000037F9 File Offset: 0x000019F9
		void IFolder.GetSearchCriteria(out RestrictionData restriction, out byte[][] entryIds, out SearchState state)
		{
			MrsTracer.Provider.Function("PstFolder.GetSearchCriteria", new object[0]);
			restriction = null;
			entryIds = null;
			state = SearchState.None;
		}

		// Token: 0x06000066 RID: 102 RVA: 0x00003819 File Offset: 0x00001A19
		RuleData[] IFolder.GetRules(PropTag[] extraProps)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000067 RID: 103 RVA: 0x00003820 File Offset: 0x00001A20
		PropValueData[][] IFolder.GetACL(SecurityProp secProp)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000068 RID: 104 RVA: 0x00003827 File Offset: 0x00001A27
		PropValueData[][] IFolder.GetExtendedAcl(AclFlags aclFlags)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000069 RID: 105 RVA: 0x00003830 File Offset: 0x00001A30
		PropProblemData[] IFolder.SetProps(PropValueData[] pvda)
		{
			MrsTracer.Provider.Function("PstFolder.SetProps(num of props={0})", new object[]
			{
				pvda.Length
			});
			foreach (PropValueData data in pvda)
			{
				this.Folder.PropertyBag.SetProperty(PstMailbox.MoMTPvFromPv(DataConverter<PropValueConverter, PropValue, PropValueData>.GetNative(data)));
			}
			try
			{
				this.Folder.IPstFolder.Save();
			}
			catch (PSTExceptionBase innerException)
			{
				throw new MailboxReplicationPermanentException(new LocalizedString(this.Folder.PstMailbox.IPst.FileName), innerException);
			}
			return null;
		}

		// Token: 0x0600006A RID: 106 RVA: 0x000038D8 File Offset: 0x00001AD8
		internal virtual void Config(byte[] folderId, PstFxFolder folder)
		{
			this.folderId = folderId;
			this.folder = folder;
			this.folderRec = null;
		}

		// Token: 0x0600006B RID: 107 RVA: 0x000038EF File Offset: 0x00001AEF
		protected override void InternalDispose(bool calledFromDispose)
		{
			if (calledFromDispose && this.folder != null)
			{
				this.folder.Dispose();
				this.folder = null;
			}
		}

		// Token: 0x0600006C RID: 108 RVA: 0x0000390E File Offset: 0x00001B0E
		protected override DisposeTracker InternalGetDisposeTracker()
		{
			return DisposeTracker.Get<PstFolder>(this);
		}

		// Token: 0x0600006D RID: 109 RVA: 0x00003918 File Offset: 0x00001B18
		private void EnumerateMessagesHelper(List<uint> messageIds, PropTag[] additionalPtagsToLoad, List<MessageRec> messageList)
		{
			foreach (uint num in messageIds)
			{
				IMessage message = null;
				PropValueData[] additionalProps = null;
				PropertyValue property = PstFolder.errorPropertyValue;
				PropertyValue property2 = PstFolder.errorPropertyValue;
				try
				{
					message = this.folder.PstMailbox.IPst.ReadMessage(num);
				}
				catch (PSTIOException innerException)
				{
					throw new UnableToGetPSTFolderPropsTransientException(BitConverter.ToUInt32(this.folderId, 0), innerException);
				}
				catch (PSTExceptionBase pstexceptionBase)
				{
					MrsTracer.Provider.Error("PstFolder.EnumerateMessages failed while reading message: {0}", new object[]
					{
						pstexceptionBase
					});
				}
				if (message != null)
				{
					using (PSTMessage pstmessage = new PSTMessage(this.folder.PstMailbox, message))
					{
						if (additionalPtagsToLoad != null)
						{
							additionalProps = new PropValueData[additionalPtagsToLoad.Length];
							PropertyValue[] array = new PropertyValue[additionalPtagsToLoad.Length];
							for (int i = 0; i < additionalPtagsToLoad.Length; i++)
							{
								array[i] = pstmessage.RawPropertyBag.GetProperty(PstMailbox.MoMTPtagFromPtag(additionalPtagsToLoad[i]));
							}
							additionalProps = DataConverter<PropValueConverter, PropValue, PropValueData>.GetData(PstMailbox.PvaFromMoMTPva(array));
						}
						property = pstmessage.RawPropertyBag.GetProperty(PropertyTag.CreationTime);
						property2 = pstmessage.RawPropertyBag.GetProperty(PropertyTag.MessageSize);
					}
				}
				byte[] entryId = PstMailbox.CreateEntryIdFromNodeId(this.folder.PstMailbox.IPst.MessageStore.Guid, num);
				MessageRec item = new MessageRec(entryId, this.FolderId, property.IsError ? DateTime.MinValue : ((DateTime)((ExDateTime)property.Value)), property2.IsError ? 1000 : ((int)property2.Value), this.folder.PstMailbox.IPst.CheckIfAssociatedMessageId(num) ? MsgRecFlags.Associated : MsgRecFlags.None, additionalProps);
				messageList.Add(item);
			}
		}

		// Token: 0x04000017 RID: 23
		private static PropertyValue errorPropertyValue = PropertyValue.Error(PropertyId.Invalid, (ErrorCode)2147746063U);

		// Token: 0x04000018 RID: 24
		private byte[] folderId;

		// Token: 0x04000019 RID: 25
		private PstFxFolder folder;

		// Token: 0x0400001A RID: 26
		private FolderRec folderRec;
	}
}
