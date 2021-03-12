using System;
using System.Collections.Generic;
using System.Security.AccessControl;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Storage.ActiveManager;
using Microsoft.Exchange.PST;
using Microsoft.Exchange.RpcClientAccess;
using Microsoft.Mapi;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x02000004 RID: 4
	internal class PstDestinationMailbox : PstMailbox, IDestinationMailbox, IMailbox, IDisposable
	{
		// Token: 0x06000045 RID: 69 RVA: 0x0000313C File Offset: 0x0000133C
		bool IDestinationMailbox.MailboxExists()
		{
			MrsTracer.Provider.Function("PstDestinationMailbox.IDestinationMailbox.MailboxExists()", new object[0]);
			return ((IMailbox)this).IsConnected();
		}

		// Token: 0x06000046 RID: 70 RVA: 0x00003159 File Offset: 0x00001359
		CreateMailboxResult IDestinationMailbox.CreateMailbox(byte[] mailboxData, MailboxSignatureFlags sourceSignatureFlags)
		{
			MrsTracer.Provider.Function("PstDestinationMailbox.CreateMailbox", new object[0]);
			return CreateMailboxResult.Success;
		}

		// Token: 0x06000047 RID: 71 RVA: 0x00003171 File Offset: 0x00001371
		void IDestinationMailbox.ProcessMailboxSignature(byte[] mailboxData)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000048 RID: 72 RVA: 0x00003178 File Offset: 0x00001378
		IDestinationFolder IDestinationMailbox.GetFolder(byte[] entryId)
		{
			MrsTracer.Provider.Function("PstDestinationMailbox.GetFolder({0})", new object[]
			{
				TraceUtils.DumpEntryId(entryId)
			});
			return base.GetFolder<PstDestinationFolder>(entryId);
		}

		// Token: 0x06000049 RID: 73 RVA: 0x000031AC File Offset: 0x000013AC
		IFxProxy IDestinationMailbox.GetFxProxy()
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600004A RID: 74 RVA: 0x000031B4 File Offset: 0x000013B4
		IFxProxyPool IDestinationMailbox.GetFxProxyPool(ICollection<byte[]> folderIds)
		{
			MrsTracer.Provider.Function("PstDestinationMailbox.IDestinationMailbox.GetFxProxyPool", new object[0]);
			PSTFxProxyPool proxy = new PSTFxProxyPool(this, folderIds);
			return new FxProxyPoolWrapper(proxy, null);
		}

		// Token: 0x0600004B RID: 75 RVA: 0x000031E5 File Offset: 0x000013E5
		PropProblemData[] IDestinationMailbox.SetProps(PropValueData[] pvda)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600004C RID: 76 RVA: 0x000031EC File Offset: 0x000013EC
		void IDestinationMailbox.CreateFolder(FolderRec sourceFolder, CreateFolderFlags createFolderFlags, out byte[] newFolderId)
		{
			MrsTracer.Provider.Function("PstDestinationMailbox.CreateFolder(\"{0}\")", new object[]
			{
				sourceFolder.FolderName
			});
			newFolderId = null;
			uint nodeIdFromEntryId = PstMailbox.GetNodeIdFromEntryId(base.IPst.MessageStore.Guid, sourceFolder.ParentId);
			if (sourceFolder.EntryId != null)
			{
				uint nodeIdFromEntryId2 = PstMailbox.GetNodeIdFromEntryId(base.IPst.MessageStore.Guid, sourceFolder.EntryId);
				IFolder folder = base.IPst.ReadFolder(nodeIdFromEntryId2);
				if (folder != null)
				{
					if (createFolderFlags.HasFlag(CreateFolderFlags.FailIfExists))
					{
						throw new UnableToReadPSTFolderPermanentException(nodeIdFromEntryId2);
					}
					if (nodeIdFromEntryId == folder.ParentId)
					{
						return;
					}
				}
			}
			IFolder folder2 = base.IPst.ReadFolder(nodeIdFromEntryId);
			if (folder2 == null)
			{
				throw new UnableToReadPSTFolderPermanentException(nodeIdFromEntryId);
			}
			using (PstFxFolder pstFxFolder = new PstFxFolder(this, folder2.AddFolder()))
			{
				pstFxFolder.PropertyBag.SetProperty(new PropertyValue(PropertyTag.DisplayName, sourceFolder.FolderName));
				pstFxFolder.PropertyBag.SetProperty(new PropertyValue(PropertyTag.FolderType, (int)sourceFolder.FolderType));
				newFolderId = PstMailbox.CreateEntryIdFromNodeId(base.IPst.MessageStore.Guid, pstFxFolder.IPstFolder.Id);
			}
		}

		// Token: 0x0600004D RID: 77 RVA: 0x00003338 File Offset: 0x00001538
		void IDestinationMailbox.MoveFolder(byte[] folderId, byte[] oldParentId, byte[] newParentId)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600004E RID: 78 RVA: 0x0000333F File Offset: 0x0000153F
		void IDestinationMailbox.DeleteFolder(FolderRec folderRec)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600004F RID: 79 RVA: 0x00003348 File Offset: 0x00001548
		string IMailbox.LoadSyncState(byte[] key)
		{
			MrsTracer.Provider.Function("PstDestinationMailbox.IMailbox.LoadSyncState(key={0})", new object[]
			{
				TraceUtils.DumpBytes(key)
			});
			this.LoadSyncStateCache();
			string text = this.syncStateCache[key];
			MrsTracer.Provider.Debug("key {0}found.", new object[]
			{
				(text != null) ? string.Empty : "NOT "
			});
			return text;
		}

		// Token: 0x06000050 RID: 80 RVA: 0x000033B4 File Offset: 0x000015B4
		MessageRec IMailbox.SaveSyncState(byte[] key, string syncStateStr)
		{
			MrsTracer.Provider.Function("PstDestinationMailbox.IMailbox.SaveSyncState(key={0})", new object[]
			{
				TraceUtils.DumpBytes(key)
			});
			if (base.IPst == null)
			{
				MrsTracer.Provider.Debug("Skipping sync state save on a previously failed pst file", new object[0]);
				return null;
			}
			try
			{
				this.LoadSyncStateCache();
				this.syncStateCache[key] = syncStateStr;
				base.MessageStorePropertyBag.SetProperty(new PropertyValue(new PropertyTag((PropertyId)this.syncStatePropId, PropertyType.Unicode), this.syncStateCache.Serialize(false)));
				base.IPst.MessageStore.Save();
			}
			catch (PSTExceptionBase innerException)
			{
				throw new UnableToSavePSTSyncStatePermanentException(base.IPst.FileName, innerException);
			}
			return null;
		}

		// Token: 0x06000051 RID: 81 RVA: 0x00003474 File Offset: 0x00001674
		void IDestinationMailbox.SetMailboxSecurityDescriptor(RawSecurityDescriptor sd)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000052 RID: 82 RVA: 0x0000347B File Offset: 0x0000167B
		void IDestinationMailbox.SetUserSecurityDescriptor(RawSecurityDescriptor sd)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000053 RID: 83 RVA: 0x00003482 File Offset: 0x00001682
		void IDestinationMailbox.PreFinalSyncDataProcessing(int? sourceMailboxVersion)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000054 RID: 84 RVA: 0x00003489 File Offset: 0x00001689
		ConstraintCheckResultType IDestinationMailbox.CheckDataGuarantee(DateTime commitTimestamp, out LocalizedString failureReason)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000055 RID: 85 RVA: 0x00003490 File Offset: 0x00001690
		void IDestinationMailbox.ForceLogRoll()
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000056 RID: 86 RVA: 0x00003497 File Offset: 0x00001697
		List<ReplayAction> IDestinationMailbox.GetActions(string replaySyncState, int maxNumberOfActions)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000057 RID: 87 RVA: 0x0000349E File Offset: 0x0000169E
		void IDestinationMailbox.SetMailboxSettings(ItemPropertiesBase item)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000058 RID: 88 RVA: 0x000034A8 File Offset: 0x000016A8
		private void LoadSyncStateCache()
		{
			MrsTracer.Provider.Function("PstDestinationMailbox.LoadSyncStateData", new object[0]);
			if (this.syncStateCache != null)
			{
				return;
			}
			try
			{
				if (this.syncStatePropId == 0)
				{
					this.syncStatePropId = base.IPst.ReadIdFromNamedProp(PstDestinationMailbox.syncStateNamedProp.Name, 0U, PstDestinationMailbox.syncStateNamedProp.Guid, true);
				}
				this.syncStateCache = new PSTSyncStateDictionary();
				PropertyValue property = base.MessageStorePropertyBag.GetProperty(new PropertyTag((PropertyId)this.syncStatePropId, PropertyType.Unicode));
				if (!property.IsError)
				{
					this.syncStateCache = XMLSerializableBase.Deserialize<PSTSyncStateDictionary>((string)property.Value, true);
				}
			}
			catch (PSTExceptionBase innerException)
			{
				throw new UnableToLoadPSTSyncStatePermanentException(base.IPst.FileName, innerException);
			}
			catch (MoveJobDeserializationFailedPermanentException innerException2)
			{
				throw new UnableToLoadPSTSyncStatePermanentException(base.IPst.FileName, innerException2);
			}
		}

		// Token: 0x04000014 RID: 20
		private static readonly NamedProperty syncStateNamedProp = new NamedProperty(new Guid("{EF406DC2-053E-42ff-9547-C52CAC90D184}"), "MRSSyncState");

		// Token: 0x04000015 RID: 21
		private ushort syncStatePropId;

		// Token: 0x04000016 RID: 22
		private PSTSyncStateDictionary syncStateCache;
	}
}
