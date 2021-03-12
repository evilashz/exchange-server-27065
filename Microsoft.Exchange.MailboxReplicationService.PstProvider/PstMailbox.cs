using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Net;
using System.Security;
using System.Security.AccessControl;
using System.Text;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.PST;
using Microsoft.Exchange.PST.Common;
using Microsoft.Exchange.RpcClientAccess;
using Microsoft.Mapi;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x02000003 RID: 3
	internal abstract class PstMailbox : MailboxProviderBase, IMailbox, IDisposable
	{
		// Token: 0x06000008 RID: 8 RVA: 0x0000225A File Offset: 0x0000045A
		public PstMailbox() : base(LocalMailboxFlags.None)
		{
		}

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000009 RID: 9 RVA: 0x0000226F File Offset: 0x0000046F
		public IPST IPst
		{
			get
			{
				return this.iPst;
			}
		}

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x0600000A RID: 10 RVA: 0x00002277 File Offset: 0x00000477
		public PSTPropertyBag MessageStorePropertyBag
		{
			get
			{
				if (this.messageStorePropertyBag == null)
				{
					this.messageStorePropertyBag = new PSTPropertyBag(this, this.iPst.MessageStore.PropertyBag);
				}
				return this.messageStorePropertyBag;
			}
		}

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x0600000B RID: 11 RVA: 0x000022A3 File Offset: 0x000004A3
		public Encoding CachedEncoding
		{
			get
			{
				if (this.cachedEncoding == null)
				{
					this.cachedEncoding = (this.ContentEncoding ?? this.TryGetEncodingFromLanguage());
					this.encodingFound = new bool?(this.cachedEncoding != null);
				}
				return this.cachedEncoding;
			}
		}

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x0600000C RID: 12 RVA: 0x000022E0 File Offset: 0x000004E0
		public Encoding ContentEncoding
		{
			get
			{
				return this.contentEncoding;
			}
		}

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x0600000D RID: 13 RVA: 0x000022E8 File Offset: 0x000004E8
		// (set) Token: 0x0600000E RID: 14 RVA: 0x000022EF File Offset: 0x000004EF
		public override int ServerVersion
		{
			get
			{
				throw new NotImplementedException();
			}
			protected set
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x0600000F RID: 15 RVA: 0x000022F6 File Offset: 0x000004F6
		public static PropertyTag MoMTPtagFromPtag(PropTag ptag)
		{
			return new PropertyTag((uint)ptag);
		}

		// Token: 0x06000010 RID: 16 RVA: 0x00002300 File Offset: 0x00000500
		public static PropertyTag[] MoMTPtaFromPta(PropTag[] pta)
		{
			if (pta == null)
			{
				return null;
			}
			PropertyTag[] array = new PropertyTag[pta.Length];
			for (int i = 0; i < pta.Length; i++)
			{
				array[i] = PstMailbox.MoMTPtagFromPtag(pta[i]);
			}
			return array;
		}

		// Token: 0x06000011 RID: 17 RVA: 0x00002340 File Offset: 0x00000540
		public static PropValue[] PvaFromMoMTPva(PropertyValue[] momtPva)
		{
			PropValue[] array = new PropValue[momtPva.Length];
			for (int i = 0; i < momtPva.Length; i++)
			{
				array[i] = PstMailbox.PvFromMoMTPv(momtPva[i]);
			}
			return array;
		}

		// Token: 0x06000012 RID: 18 RVA: 0x00002384 File Offset: 0x00000584
		public static PropValue PvFromMoMTPv(PropertyValue momtPv)
		{
			object value;
			if (momtPv.PropertyTag.PropertyType == PropertyType.SysTime)
			{
				value = (DateTime)((ExDateTime)momtPv.Value);
			}
			else
			{
				value = momtPv.Value;
			}
			return new PropValue((PropTag)momtPv.PropertyTag, value);
		}

		// Token: 0x06000013 RID: 19 RVA: 0x000023D8 File Offset: 0x000005D8
		public static PropertyValue MoMTPvFromPv(PropValue pv)
		{
			PropertyValue result;
			if (pv.PropType == PropType.SysTime)
			{
				result = new PropertyValue(new PropertyTag((uint)pv.PropTag), (ExDateTime)pv.GetDateTime());
			}
			else
			{
				result = new PropertyValue(new PropertyTag((uint)pv.PropTag), pv.Value);
			}
			return result;
		}

		// Token: 0x06000014 RID: 20 RVA: 0x00002434 File Offset: 0x00000634
		public static NamedPropData[] NpdaFromMoMTNpva(NamedProperty[] momtNpva)
		{
			NamedPropData[] array = new NamedPropData[momtNpva.Length];
			for (int i = 0; i < momtNpva.Length; i++)
			{
				array[i] = new NamedPropData();
				array[i].Guid = momtNpva[i].Guid;
				if (momtNpva[i].Kind == NamedPropertyKind.Id)
				{
					array[i].Id = (int)momtNpva[i].Id;
				}
				else
				{
					array[i].Name = momtNpva[i].Name;
				}
			}
			return array;
		}

		// Token: 0x06000015 RID: 21 RVA: 0x000024A0 File Offset: 0x000006A0
		public static byte[] CreateEntryIdFromNodeId(Guid guid, uint nodeId)
		{
			byte[] array = new byte[24];
			Array.Copy(guid.ToByteArray(), 0, array, 4, 16);
			Array.Copy(BitConverter.GetBytes(nodeId), 0, array, 20, 4);
			return array;
		}

		// Token: 0x06000016 RID: 22 RVA: 0x000024D7 File Offset: 0x000006D7
		public static uint GetNodeIdFromEntryId(Guid guid, byte[] entryId)
		{
			return PstMailbox.GetNodeIdFromEntryId(guid, entryId, false);
		}

		// Token: 0x06000017 RID: 23 RVA: 0x000024E1 File Offset: 0x000006E1
		public override SyncProtocol GetSyncProtocol()
		{
			return SyncProtocol.None;
		}

		// Token: 0x06000018 RID: 24 RVA: 0x000024E4 File Offset: 0x000006E4
		void IMailbox.ConfigPst(string filePath, int? contentCodePage)
		{
			this.filePath = filePath;
			this.contentCodePage = contentCodePage;
		}

		// Token: 0x06000019 RID: 25 RVA: 0x000024F4 File Offset: 0x000006F4
		List<WellKnownFolder> IMailbox.DiscoverWellKnownFolders(int flags)
		{
			return FolderHierarchyUtils.DiscoverWellKnownFolders(this, flags);
		}

		// Token: 0x0600001A RID: 26 RVA: 0x000024FD File Offset: 0x000006FD
		void IMailbox.Config(IReservation reservation, Guid primaryMailboxGuid, Guid physicalMailboxGuid, TenantPartitionHint partitionHint, Guid mdbGuid, MailboxType mbxType, Guid? mailboxContainerGuid)
		{
			MrsTracer.Provider.Function("PstMailbox.IMailbox.Config", new object[0]);
		}

		// Token: 0x0600001B RID: 27 RVA: 0x00002514 File Offset: 0x00000714
		void IMailbox.ConfigRestore(MailboxRestoreType restoreFlags)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600001C RID: 28 RVA: 0x0000251B File Offset: 0x0000071B
		void IMailbox.ConfigMDBByName(string mdbName)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600001D RID: 29 RVA: 0x00002522 File Offset: 0x00000722
		void IMailbox.ConfigADConnection(string domainControllerName, string configDomainControllerName, NetworkCredential cred)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600001E RID: 30 RVA: 0x00002529 File Offset: 0x00000729
		void IMailbox.ConfigMailboxOptions(MailboxOptions options)
		{
		}

		// Token: 0x0600001F RID: 31 RVA: 0x0000252C File Offset: 0x0000072C
		bool IMailbox.IsConnected()
		{
			MrsTracer.Provider.Function("PstMailbox.IMailbox.IsConnected returns '{0}'", new object[]
			{
				this.iPst != null
			});
			return this.iPst != null;
		}

		// Token: 0x06000020 RID: 32 RVA: 0x00002570 File Offset: 0x00000770
		bool IMailbox.IsMailboxCapabilitySupported(MailboxCapabilities capability)
		{
			return false;
		}

		// Token: 0x06000021 RID: 33 RVA: 0x00002573 File Offset: 0x00000773
		MailboxInformation IMailbox.GetMailboxInformation()
		{
			MrsTracer.Provider.Function("PstMailbox.IMailbox.GetMailboxInformation", new object[0]);
			return PstMailbox.MailboxInformation;
		}

		// Token: 0x06000022 RID: 34 RVA: 0x00002590 File Offset: 0x00000790
		void IMailbox.Connect(MailboxConnectFlags connectFlags)
		{
			MrsTracer.Provider.Function("PstMailbox.IMailbox.Connect", new object[0]);
			this.ValidateCodePageAndSetEncoding();
			IPST ipst = null;
			try
			{
				ipst = new PSTSession(new Tracer.TraceMethod(MrsTracer.Provider.Debug), new Tracer.TraceMethod(MrsTracer.Provider.Warning), new Tracer.TraceMethod(MrsTracer.Provider.Error));
				ipst.Open(this.filePath, this is PstDestinationMailbox, this is PstDestinationMailbox, (connectFlags & MailboxConnectFlags.ValidateOnly) != MailboxConnectFlags.None);
			}
			catch (PSTExceptionBase pstexceptionBase)
			{
				throw new UnableToOpenPSTPermanentException(this.filePath, pstexceptionBase.Message, pstexceptionBase);
			}
			catch (UnauthorizedAccessException ex)
			{
				if (Directory.Exists(this.filePath))
				{
					throw new PSTPathMustBeAFilePermanentException(ex);
				}
				throw new UnableToOpenPSTPermanentException(this.filePath, ex.Message, ex);
			}
			catch (IOException ex2)
			{
				if (ex2 is PathTooLongException || ex2 is DirectoryNotFoundException || ex2 is FileNotFoundException)
				{
					throw new UnableToOpenPSTPermanentException(this.filePath, ex2.Message, ex2);
				}
				throw new PSTIOExceptionTransientException(ex2);
			}
			catch (SecurityException ex3)
			{
				throw new UnableToOpenPSTPermanentException(this.filePath, ex3.Message, ex3);
			}
			this.iPst = ipst;
			MrsTracer.Provider.Debug("Successfully opened PST file '{0}'", new object[]
			{
				this.filePath
			});
		}

		// Token: 0x06000023 RID: 35 RVA: 0x00002700 File Offset: 0x00000900
		void IMailbox.Disconnect()
		{
			MrsTracer.Provider.Function("PstMailbox.IMailbox.Disconnect", new object[0]);
			if (this.iPst == null)
			{
				return;
			}
			try
			{
				this.iPst.Close();
			}
			catch (PSTExceptionBase innerException)
			{
				throw new UnableToClosePSTPermanentException(this.filePath, innerException);
			}
			finally
			{
				this.iPst = null;
				this.messageStorePropertyBag = null;
			}
			MrsTracer.Provider.Debug("Successfully closed PST file '{0}'", new object[]
			{
				this.filePath
			});
		}

		// Token: 0x06000024 RID: 36 RVA: 0x00002794 File Offset: 0x00000994
		MailboxServerInformation IMailbox.GetMailboxServerInformation()
		{
			MrsTracer.Provider.Function("PstMailbox.IMailbox.GetMailboxServerInformation", new object[0]);
			return null;
		}

		// Token: 0x06000025 RID: 37 RVA: 0x000027AC File Offset: 0x000009AC
		void IMailbox.SetInTransitStatus(InTransitStatus status, out bool onlineMoveSupported)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000026 RID: 38 RVA: 0x000027B3 File Offset: 0x000009B3
		void IMailbox.SeedMBICache()
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000027 RID: 39 RVA: 0x000027BC File Offset: 0x000009BC
		List<FolderRec> IMailbox.EnumerateFolderHierarchy(EnumerateFolderHierarchyFlags flags, PropTag[] additionalPtagsToLoad)
		{
			MrsTracer.Provider.Function("PstMailbox.IMailbox.EnumerateFolderHierarchy({0})", new object[]
			{
				flags
			});
			List<FolderRec> list = new List<FolderRec>(50);
			List<PropTag> list2 = new List<PropTag>(FolderRec.PtagsToLoad);
			if (additionalPtagsToLoad != null)
			{
				list2.AddRange(additionalPtagsToLoad);
			}
			PropertyTag[] array = PstMailbox.MoMTPtaFromPta(list2.ToArray());
			try
			{
				IFolder folder = this.iPst.ReadFolder(290U);
				if (folder == null)
				{
					throw new UnableToGetPSTHierarchyPermanentException(this.filePath);
				}
				using (PstFxFolder pstFxFolder = new PstFxFolder(this, folder))
				{
					list.Add(FolderRec.Create(PstMailbox.PvaFromMoMTPva(pstFxFolder.GetProps(array))));
					PropertyValue property = this.MessageStorePropertyBag.GetProperty(PropertyTag.IPMSubtreeFolder);
					if (property.IsError || ((byte[])property.Value).Length != 24)
					{
						throw new UnableToGetPSTHierarchyPermanentException(this.filePath);
					}
					uint nodeIdFromEntryId = PstMailbox.GetNodeIdFromEntryId(this.iPst.MessageStore.Guid, (byte[])property.Value, true);
					IFolder folder2 = this.iPst.ReadFolder(nodeIdFromEntryId);
					if (folder2 == null)
					{
						throw new UnableToGetPSTHierarchyPermanentException(this.filePath);
					}
					this.GetHierarchy(folder2, list, array);
				}
			}
			catch (PSTIOException innerException)
			{
				throw new UnableToGetPSTHierarchyTransientException(this.filePath, innerException);
			}
			catch (PSTExceptionBase innerException2)
			{
				throw new UnableToGetPSTHierarchyPermanentException(this.filePath, innerException2);
			}
			MrsTracer.Provider.Debug("PST hierarchy contains {0} folders including root", new object[]
			{
				list.Count
			});
			return list;
		}

		// Token: 0x06000028 RID: 40 RVA: 0x00002960 File Offset: 0x00000B60
		void IMailbox.DeleteMailbox(int flags)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000029 RID: 41 RVA: 0x00002967 File Offset: 0x00000B67
		NamedPropData[] IMailbox.GetNamesFromIDs(PropTag[] pta)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600002A RID: 42 RVA: 0x00002970 File Offset: 0x00000B70
		PropTag[] IMailbox.GetIDsFromNames(bool createIfNotExists, NamedPropData[] npda)
		{
			MrsTracer.Provider.Function("PstMailbox.IMailbox.GetIDsFromNames", new object[0]);
			PropTag[] array = new PropTag[npda.Length];
			for (int i = 0; i < npda.Length; i++)
			{
				ushort num;
				try
				{
					if (npda[i].Kind == 0)
					{
						num = this.iPst.ReadIdFromNamedProp(null, (uint)npda[i].Id, npda[i].Guid, createIfNotExists);
					}
					else
					{
						num = this.iPst.ReadIdFromNamedProp(npda[i].Name, 0U, npda[i].Guid, createIfNotExists);
					}
				}
				catch (PSTExceptionBase innerException)
				{
					throw new MailboxReplicationPermanentException(new LocalizedString(this.IPst.FileName), innerException);
				}
				array[i] = ((num != 0) ? PropTagHelper.PropTagFromIdAndType((int)num, PropType.Unspecified) : PropTag.Unresolved);
			}
			return array;
		}

		// Token: 0x0600002B RID: 43 RVA: 0x00002A30 File Offset: 0x00000C30
		byte[] IMailbox.GetSessionSpecificEntryId(byte[] entryId)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600002C RID: 44 RVA: 0x00002A37 File Offset: 0x00000C37
		bool IMailbox.UpdateRemoteHostName(string value)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600002D RID: 45 RVA: 0x00002A3E File Offset: 0x00000C3E
		ADUser IMailbox.GetADUser()
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600002E RID: 46 RVA: 0x00002A45 File Offset: 0x00000C45
		void IMailbox.UpdateMovedMailbox(UpdateMovedMailboxOperation op, ADUser remoteRecipientData, string domainController, out ReportEntry[] entries, Guid targetDatabaseGuid, Guid targetArchiveDatabaseGuid, string archiveDomain, ArchiveStatusFlags archiveStatus, UpdateMovedMailboxFlags updateMovedMailboxFlags, Guid? newMailboxContainerGuid, CrossTenantObjectId newUnifiedMailboxId)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600002F RID: 47 RVA: 0x00002A4C File Offset: 0x00000C4C
		MappedPrincipal[] IMailbox.ResolvePrincipals(MappedPrincipal[] principals)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000030 RID: 48 RVA: 0x00002A53 File Offset: 0x00000C53
		Guid[] IMailbox.ResolvePolicyTag(string policyTagStr)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000031 RID: 49 RVA: 0x00002A5A File Offset: 0x00000C5A
		RawSecurityDescriptor IMailbox.GetMailboxSecurityDescriptor()
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000032 RID: 50 RVA: 0x00002A61 File Offset: 0x00000C61
		RawSecurityDescriptor IMailbox.GetUserSecurityDescriptor()
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000033 RID: 51 RVA: 0x00002A68 File Offset: 0x00000C68
		void IMailbox.AddMoveHistoryEntry(MoveHistoryEntryInternal mhei, int maxMoveHistoryLength)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000034 RID: 52 RVA: 0x00002A6F File Offset: 0x00000C6F
		ServerHealthStatus IMailbox.CheckServerHealth()
		{
			MrsTracer.Provider.Function("PstMailbox.IMailbox.CheckServerHealth", new object[0]);
			return new ServerHealthStatus(ServerHealthState.Healthy);
		}

		// Token: 0x06000035 RID: 53 RVA: 0x00002A8C File Offset: 0x00000C8C
		PropValueData[] IMailbox.GetProps(PropTag[] pta)
		{
			MrsTracer.Provider.Function("PstMailbox.IMailbox.GetProps", new object[0]);
			PropValue[] array = new PropValue[pta.Length];
			for (int i = 0; i < pta.Length; i++)
			{
				try
				{
					array[i] = PstMailbox.PvFromMoMTPv(this.MessageStorePropertyBag.GetProperty(PstMailbox.MoMTPtagFromPtag(pta[i])));
				}
				catch (PSTIOException innerException)
				{
					throw new UnableToGetPSTPropsTransientException(this.filePath, innerException);
				}
				catch (PSTExceptionBase innerException2)
				{
					throw new UnableToGetPSTPropsPermanentException(this.filePath, innerException2);
				}
			}
			return DataConverter<PropValueConverter, PropValue, PropValueData>.GetData(array);
		}

		// Token: 0x06000036 RID: 54 RVA: 0x00002B2C File Offset: 0x00000D2C
		byte[] IMailbox.GetReceiveFolderEntryId(string msgClass)
		{
			MrsTracer.Provider.Function("PstMailbox.GetReceiveFolderEntryId('{0}')", new object[]
			{
				msgClass
			});
			if (this.receiveFoldersTable == null)
			{
				try
				{
					this.receiveFoldersTable = this.iPst.ReadReceiveFoldersTable();
				}
				catch (PSTIOException innerException)
				{
					throw new UnableToGetPSTReceiveFolderTransientException(this.filePath, msgClass, innerException);
				}
				catch (PSTExceptionBase innerException2)
				{
					throw new UnableToGetPSTReceiveFolderPermanentException(this.filePath, msgClass, innerException2);
				}
			}
			uint num = 0U;
			this.receiveFoldersTable.TryGetValue(msgClass, out num);
			MrsTracer.Provider.Debug("receive folder for messageClass '{0}' {1}found", new object[]
			{
				msgClass,
				(num == 0U) ? "NOT " : string.Empty
			});
			if (num != 0U)
			{
				return PstMailbox.CreateEntryIdFromNodeId(this.iPst.MessageStore.Guid, num);
			}
			return null;
		}

		// Token: 0x06000037 RID: 55 RVA: 0x00002C08 File Offset: 0x00000E08
		string IMailbox.LoadSyncState(byte[] key)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000038 RID: 56 RVA: 0x00002C0F File Offset: 0x00000E0F
		MessageRec IMailbox.SaveSyncState(byte[] key, string syncStateStr)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000039 RID: 57 RVA: 0x00002C16 File Offset: 0x00000E16
		SessionStatistics IMailbox.GetSessionStatistics(SessionStatisticsFlags statisticsTypes)
		{
			return new SessionStatistics();
		}

		// Token: 0x0600003A RID: 58 RVA: 0x00002C1D File Offset: 0x00000E1D
		Guid IMailbox.StartIsInteg(List<uint> mailboxCorruptionTypes)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600003B RID: 59 RVA: 0x00002C24 File Offset: 0x00000E24
		List<StoreIntegrityCheckJob> IMailbox.QueryIsInteg(Guid isIntegRequestGuid)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600003C RID: 60 RVA: 0x00002C2C File Offset: 0x00000E2C
		public T GetFolder<T>(byte[] folderId) where T : PstFolder, new()
		{
			MrsTracer.Provider.Function("PstMailbox.GetFolder({0})", new object[]
			{
				TraceUtils.DumpEntryId(folderId)
			});
			uint nodeIdFromEntryId = PstMailbox.GetNodeIdFromEntryId(this.iPst.MessageStore.Guid, folderId);
			IFolder folder;
			try
			{
				folder = this.iPst.ReadFolder(nodeIdFromEntryId);
			}
			catch (PSTIOException innerException)
			{
				throw new UnableToReadPSTFolderTransientException(nodeIdFromEntryId, innerException);
			}
			catch (PSTExceptionBase innerException2)
			{
				throw new UnableToReadPSTFolderPermanentException(nodeIdFromEntryId, innerException2);
			}
			if (folder == null)
			{
				MrsTracer.Provider.Debug("Folder does not exist", new object[0]);
				return default(T);
			}
			PstFxFolder pstFxFolder = new PstFxFolder(this, folder);
			if (MrsTracer.Provider.IsEnabled(TraceType.DebugTrace))
			{
				MrsTracer.Provider.Debug("Opened folder '{0}'", new object[]
				{
					(string)pstFxFolder.GetProp(PropertyTag.DisplayName).Value
				});
			}
			T result = Activator.CreateInstance<T>();
			result.Config(folderId, pstFxFolder);
			return result;
		}

		// Token: 0x0600003D RID: 61 RVA: 0x00002D3C File Offset: 0x00000F3C
		protected override void InternalDispose(bool calledFromDispose)
		{
			base.InternalDispose(calledFromDispose);
			if (calledFromDispose)
			{
				((IMailbox)this).Disconnect();
			}
		}

		// Token: 0x0600003E RID: 62 RVA: 0x00002D4E File Offset: 0x00000F4E
		protected override DisposeTracker InternalGetDisposeTracker()
		{
			return DisposeTracker.Get<PstMailbox>(this);
		}

		// Token: 0x0600003F RID: 63 RVA: 0x00002D58 File Offset: 0x00000F58
		private static uint GetNodeIdFromEntryId(Guid guid, byte[] entryId, bool ignoreGuidMismatch)
		{
			if (entryId == null || entryId.Length != 24)
			{
				throw new ArgumentException("EntryId is null or wrong length", "entryId");
			}
			if (entryId[0] != 0 || entryId[1] != 0 || entryId[2] != 0 || entryId[3] != 0)
			{
				throw new ArgumentException("EntryId prefix is incorrect", "entryId");
			}
			if (!ignoreGuidMismatch)
			{
				byte[] array = new byte[16];
				Array.Copy(entryId, 4, array, 0, 16);
				if (!ArrayComparer<byte>.EqualityComparer.Equals(array, guid.ToByteArray()))
				{
					throw new ArgumentException("EntryId embedded guid is incorrect", "entryId");
				}
			}
			return BitConverter.ToUInt32(entryId, 20);
		}

		// Token: 0x06000040 RID: 64 RVA: 0x00002DE8 File Offset: 0x00000FE8
		private void GetHierarchy(IFolder iFolder, List<FolderRec> hierarchy, PropertyTag[] momtPtagsToLoad)
		{
			using (PstFxFolder pstFxFolder = new PstFxFolder(this, iFolder))
			{
				hierarchy.Add(FolderRec.Create(PstMailbox.PvaFromMoMTPva(pstFxFolder.GetProps(momtPtagsToLoad))));
				foreach (uint num in iFolder.SubFolderIds)
				{
					IFolder folder;
					try
					{
						folder = this.iPst.ReadFolder(num);
					}
					catch (PSTIOException innerException)
					{
						throw new UnableToReadPSTFolderTransientException(num, innerException);
					}
					catch (PSTExceptionBase innerException2)
					{
						throw new UnableToReadPSTFolderPermanentException(num, innerException2);
					}
					if (folder == null)
					{
						MrsTracer.Provider.Error("Pst folder 0x{0:x} does not exist", new object[]
						{
							num
						});
					}
					else
					{
						this.GetHierarchy(folder, hierarchy, momtPtagsToLoad);
					}
				}
			}
		}

		// Token: 0x06000041 RID: 65 RVA: 0x00002ED8 File Offset: 0x000010D8
		private Encoding TryGetEncodingFromLanguage()
		{
			if (this.encodingFound != null)
			{
				return this.cachedEncoding;
			}
			byte[] array;
			bool flag;
			try
			{
				PSTPropertyStream pstpropertyStream = this.MessageStorePropertyBag.GetPropertyStream(PstMailbox.Language) as PSTPropertyStream;
				if (pstpropertyStream == null)
				{
					return null;
				}
				array = new byte[pstpropertyStream.Length];
				pstpropertyStream.Read(array, 0, array.Length);
				flag = (pstpropertyStream.PropTag.PropertyType == PropertyType.Unicode);
				pstpropertyStream.Close();
			}
			catch (PSTIOException innerException)
			{
				throw new UnableToGetPSTPropsTransientException(this.IPst.FileName, innerException);
			}
			catch (PSTExceptionBase innerException2)
			{
				throw new UnableToGetPSTPropsPermanentException(this.IPst.FileName, innerException2);
			}
			string @string;
			if (flag)
			{
				@string = Encoding.Unicode.GetString(array);
			}
			else
			{
				@string = Encoding.ASCII.GetString(array);
			}
			int culture;
			if (!int.TryParse(@string, out culture))
			{
				MrsTracer.Provider.Warning("PstMailbox.IMailbox: cannot resolve culture info from {0}", new object[]
				{
					@string
				});
				return null;
			}
			try
			{
				CultureInfo cultureInfo = CultureInfo.GetCultureInfo(culture);
				return Encoding.GetEncoding(cultureInfo.TextInfo.ANSICodePage);
			}
			catch (ArgumentException)
			{
				MrsTracer.Provider.Warning("PstMailbox.IMailbox: cannot resolve culture info from {0}", new object[]
				{
					@string
				});
			}
			catch (NotSupportedException)
			{
				MrsTracer.Provider.Warning("PstMailbox.IMailbox: cannot resolve culture info from {0}", new object[]
				{
					@string
				});
			}
			return null;
		}

		// Token: 0x06000042 RID: 66 RVA: 0x0000305C File Offset: 0x0000125C
		private void ValidateCodePageAndSetEncoding()
		{
			if (this.contentCodePage != null)
			{
				Exception ex = null;
				try
				{
					this.contentEncoding = Encoding.GetEncoding(this.contentCodePage.Value);
				}
				catch (ArgumentException ex2)
				{
					ex = ex2;
				}
				catch (NotSupportedException ex3)
				{
					ex = ex3;
				}
				if (ex != null)
				{
					throw new NotSupportedCodePagePermanentException(this.contentCodePage.Value, LocalServer.GetServer().Name);
				}
			}
		}

		// Token: 0x04000006 RID: 6
		private const uint RootFolderId = 290U;

		// Token: 0x04000007 RID: 7
		private static readonly MailboxInformation MailboxInformation = new MailboxInformation
		{
			ProviderName = "PstProvider"
		};

		// Token: 0x04000008 RID: 8
		public static readonly PropertyTag Language = new PropertyTag(973864991U);

		// Token: 0x04000009 RID: 9
		public static readonly PropertyTag MessageCodePage = new PropertyTag(1073545219U);

		// Token: 0x0400000A RID: 10
		public static readonly PropertyTag InternetCPID = new PropertyTag(1071513603U);

		// Token: 0x0400000B RID: 11
		public static readonly PropertyTag MessageSizeExtended = new PropertyTag(235405332U);

		// Token: 0x0400000C RID: 12
		private string filePath;

		// Token: 0x0400000D RID: 13
		private IPST iPst;

		// Token: 0x0400000E RID: 14
		private PSTPropertyBag messageStorePropertyBag;

		// Token: 0x0400000F RID: 15
		private Dictionary<string, uint> receiveFoldersTable;

		// Token: 0x04000010 RID: 16
		private Encoding cachedEncoding;

		// Token: 0x04000011 RID: 17
		private Encoding contentEncoding;

		// Token: 0x04000012 RID: 18
		private int? contentCodePage = null;

		// Token: 0x04000013 RID: 19
		private bool? encodingFound;
	}
}
