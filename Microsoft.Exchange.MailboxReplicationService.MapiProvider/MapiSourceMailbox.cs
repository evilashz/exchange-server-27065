using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Mapi;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x0200000B RID: 11
	internal class MapiSourceMailbox : MapiMailbox, ISourceMailbox, IMailbox, IDisposable
	{
		// Token: 0x0600008F RID: 143 RVA: 0x0000796A File Offset: 0x00005B6A
		public MapiSourceMailbox(LocalMailboxFlags flags) : base(flags)
		{
		}

		// Token: 0x06000090 RID: 144 RVA: 0x00007973 File Offset: 0x00005B73
		internal MapiSourceMailbox(MapiStore mapiStore) : base(mapiStore)
		{
		}

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x06000091 RID: 145 RVA: 0x0000797C File Offset: 0x00005B7C
		internal override bool SupportsSavingSyncState
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06000092 RID: 146 RVA: 0x00007980 File Offset: 0x00005B80
		byte[] ISourceMailbox.GetMailboxBasicInfo(MailboxSignatureFlags flags)
		{
			MrsTracer.Provider.Function("MapiSourceMailbox.GetMailboxBasicInfo", new object[0]);
			base.CheckDisposed();
			if (base.IsPublicFolderMigrationSource)
			{
				return MailboxSignatureConverter.CreatePublicFolderMailboxBasicInformation();
			}
			base.VerifyMailboxConnection(VerifyMailboxConnectionFlags.None);
			uint mailboxSignatureServerVersion;
			byte[] array;
			using (ExRpcAdmin rpcAdmin = base.GetRpcAdmin())
			{
				using (base.RHTracker.Start())
				{
					MrsTracer.Provider.Debug("Reading mailbox basic info \"{0}\" in MDB {1}, flags {2}", new object[]
					{
						base.TraceMailboxId,
						base.MdbGuid,
						flags
					});
					mailboxSignatureServerVersion = rpcAdmin.GetMailboxSignatureServerVersion();
					array = rpcAdmin.GetMailboxBasicInfo(base.MdbGuid, base.MailboxGuid, flags);
				}
			}
			if (flags == MailboxSignatureFlags.GetLegacy && mailboxSignatureServerVersion >= 102U)
			{
				array = MailboxSignatureConverter.ExtractMailboxBasicInfo(array);
			}
			return array;
		}

		// Token: 0x06000093 RID: 147 RVA: 0x00007A6C File Offset: 0x00005C6C
		ISourceFolder ISourceMailbox.GetFolder(byte[] entryId)
		{
			MrsTracer.Provider.Function("MapiSourceMailbox.GetFolder({0})", new object[]
			{
				TraceUtils.DumpEntryId(entryId)
			});
			return base.GetFolder<MapiSourceFolder>(entryId);
		}

		// Token: 0x06000094 RID: 148 RVA: 0x00007AA0 File Offset: 0x00005CA0
		void ISourceMailbox.CopyTo(IFxProxy fxProxy, PropTag[] excludeTags)
		{
			MrsTracer.Provider.Function("MapiSourceMailbox.CopyTo", new object[0]);
			base.VerifyMailboxConnection(VerifyMailboxConnectionFlags.None);
			bool flag = false;
			try
			{
				CopyPropertiesFlags copyPropertiesFlags = CopyPropertiesFlags.None;
				if ((base.Flags & LocalMailboxFlags.StripLargeRulesForDownlevelTargets) != LocalMailboxFlags.None)
				{
					copyPropertiesFlags |= CopyPropertiesFlags.StripLargeRulesForDownlevelTargets;
				}
				using (base.RHTracker.Start())
				{
					using (FxProxyBudgetWrapper fxProxyBudgetWrapper = new FxProxyBudgetWrapper(fxProxy, false, new Func<IDisposable>(base.RHTracker.StartExclusive), new Action<uint>(base.RHTracker.Charge)))
					{
						base.MapiStore.ExportObject(fxProxyBudgetWrapper, copyPropertiesFlags, excludeTags);
					}
				}
				flag = true;
				fxProxy.Flush();
			}
			finally
			{
				if (!flag)
				{
					MrsTracer.Provider.Debug("Flushing target proxy after receiving an exception.", new object[0]);
					CommonUtils.CatchKnownExceptions(new Action(fxProxy.Flush), null);
				}
			}
		}

		// Token: 0x06000095 RID: 149 RVA: 0x00007B9C File Offset: 0x00005D9C
		void ISourceMailbox.SetMailboxSyncState(string syncStateStr)
		{
			base.SetMailboxSyncState(syncStateStr);
		}

		// Token: 0x06000096 RID: 150 RVA: 0x00007BA5 File Offset: 0x00005DA5
		string ISourceMailbox.GetMailboxSyncState()
		{
			return base.GetMailboxSyncState();
		}

		// Token: 0x06000097 RID: 151 RVA: 0x00007C20 File Offset: 0x00005E20
		MailboxChangesManifest ISourceMailbox.EnumerateHierarchyChanges(EnumerateHierarchyChangesFlags flags, int maxChanges)
		{
			bool catchup = flags.HasFlag(EnumerateHierarchyChangesFlags.Catchup);
			return this.EnumerateHierarchyChanges(catchup, delegate(SyncHierarchyManifestState hierarchyData)
			{
				bool flag = false;
				MailboxChangesManifest result;
				try
				{
					IL_02:
					result = this.DoManifestSync(flags, maxChanges, hierarchyData, this.MapiStore);
				}
				catch (MapiExceptionCallFailed mapiExceptionCallFailed)
				{
					if (mapiExceptionCallFailed.LowLevelError == 1228 && !flag)
					{
						this.ReadRulesFromAllFolders();
						flag = true;
						goto IL_02;
					}
					throw;
				}
				return result;
			});
		}

		// Token: 0x06000098 RID: 152 RVA: 0x00007C74 File Offset: 0x00005E74
		void ISourceMailbox.ExportMessages(List<MessageRec> messages, IFxProxyPool proxyPool, ExportMessagesFlags flags, PropTag[] propsToCopyExplicitly, PropTag[] excludeProps)
		{
			MrsTracer.Provider.Function("MapiSourceMailbox.ExportMessages({0} messages)", new object[]
			{
				messages.Count
			});
			base.VerifyMailboxConnection(VerifyMailboxConnectionFlags.None);
			if ((flags & ExportMessagesFlags.OneByOne) != ExportMessagesFlags.None)
			{
				this.CopyMessagesOneByOne(messages, proxyPool, propsToCopyExplicitly, excludeProps, null);
				return;
			}
			this.CopyMessageBatch(messages, proxyPool);
		}

		// Token: 0x06000099 RID: 153 RVA: 0x00007CC8 File Offset: 0x00005EC8
		void ISourceMailbox.ExportFolders(List<byte[]> folderIds, IFxProxyPool proxyPool, ExportFoldersDataToCopyFlags exportFoldersDataToCopyFlags, GetFolderRecFlags folderRecFlags, PropTag[] additionalFolderRecProps, CopyPropertiesFlags copyPropertiesFlags, PropTag[] excludeProps, AclFlags extendedAclFlags)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600009A RID: 154 RVA: 0x00007CCF File Offset: 0x00005ECF
		List<ReplayActionResult> ISourceMailbox.ReplayActions(List<ReplayAction> actions)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600009B RID: 155 RVA: 0x00007CD6 File Offset: 0x00005ED6
		public void ConfigPublicFolders(ADObjectId databaseId)
		{
			if (base.IsPureMAPI)
			{
				return;
			}
			base.ConfiguredMdbGuid = databaseId.ObjectGuid;
		}

		// Token: 0x0600009C RID: 156 RVA: 0x00007CED File Offset: 0x00005EED
		protected override Exception GetMailboxInTransitException(Exception innerException)
		{
			MrsTracer.Provider.Error("Source mailbox is being moved.", new object[0]);
			return new SourceMailboxAlreadyBeingMovedTransientException(innerException);
		}

		// Token: 0x0600009D RID: 157 RVA: 0x00007D0A File Offset: 0x00005F0A
		protected override OpenEntryFlags GetFolderOpenEntryFlags()
		{
			if (base.IsPublicFolderMove)
			{
				return OpenEntryFlags.Modify | OpenEntryFlags.DontThrowIfEntryIsMissing;
			}
			return OpenEntryFlags.DontThrowIfEntryIsMissing;
		}

		// Token: 0x0600009E RID: 158 RVA: 0x00007FA4 File Offset: 0x000061A4
		protected override void CopySingleMessage(MessageRec curMsg, IFolderProxy folderProxy, PropTag[] propsToCopyExplicitly, PropTag[] excludeProps)
		{
			ExecutionContext.Create(new DataContext[]
			{
				new OperationDataContext("MapiSourceMailbox.CopySingleMessage", OperationType.None),
				new EntryIDsDataContext(curMsg.EntryId)
			}).Execute(delegate
			{
				using (this.RHTracker.Start())
				{
					using (MapiMessage mapiMessage = (MapiMessage)this.OpenMapiEntry(curMsg.FolderId, curMsg.EntryId, OpenEntryFlags.DontThrowIfEntryIsMissing))
					{
						if (mapiMessage == null)
						{
							MrsTracer.Provider.Debug("Message {0} is missing in source, ignoring", new object[]
							{
								TraceUtils.DumpEntryId(curMsg.EntryId)
							});
						}
						else
						{
							using (IMessageProxy messageProxy = folderProxy.OpenMessage(curMsg.EntryId))
							{
								using (FxProxyBudgetWrapper fxProxyBudgetWrapper = new FxProxyBudgetWrapper(messageProxy, false, new Func<IDisposable>(this.RHTracker.StartExclusive), new Action<uint>(this.RHTracker.Charge)))
								{
									mapiMessage.ExportObject(fxProxyBudgetWrapper, CopyPropertiesFlags.None, excludeProps);
								}
								if (propsToCopyExplicitly != null && propsToCopyExplicitly.Length > 0)
								{
									PropValue[] props = mapiMessage.GetProps(propsToCopyExplicitly);
									using (this.RHTracker.StartExclusive())
									{
										List<PropValueData> list = new List<PropValueData>();
										foreach (PropValue propValue in props)
										{
											if (!propValue.IsNull() && !propValue.IsError())
											{
												list.Add(new PropValueData(propValue.PropTag, propValue.Value));
											}
										}
										if (list.Count > 0)
										{
											messageProxy.SetProps(list.ToArray());
										}
									}
								}
								using (this.RHTracker.StartExclusive())
								{
									messageProxy.SaveChanges();
								}
							}
						}
					}
				}
			});
		}

		// Token: 0x0600009F RID: 159 RVA: 0x0000813C File Offset: 0x0000633C
		private void CopyMessageBatch(List<MessageRec> messages, IFxProxyPool proxyPool)
		{
			MrsTracer.Provider.Function("MapiSourceMailbox.CopyMessageBatch({0} messages)", new object[]
			{
				messages.Count
			});
			byte[] curFolderId = null;
			List<MessageRec> curBatch = new List<MessageRec>();
			bool exportCompleted = false;
			List<byte[]> list = new List<byte[]>(messages.Count);
			foreach (MessageRec messageRec in messages)
			{
				list.Add(messageRec.EntryId);
			}
			ExecutionContext.Create(new DataContext[]
			{
				new OperationDataContext("LocalSourceMailbox.CopyMessageBatch", OperationType.None),
				new EntryIDsDataContext(list)
			}).Execute(delegate
			{
				try
				{
					foreach (MessageRec messageRec2 in messages)
					{
						if (curFolderId != null && !CommonUtils.IsSameEntryId(curFolderId, messageRec2.FolderId))
						{
							this.FlushBatchToFolder(curBatch, proxyPool);
							curFolderId = null;
							curBatch.Clear();
						}
						curFolderId = messageRec2.FolderId;
						curBatch.Add(messageRec2);
					}
					this.FlushBatchToFolder(curBatch, proxyPool);
					exportCompleted = true;
					proxyPool.Flush();
				}
				catch (LocalizedException)
				{
					if (!exportCompleted)
					{
						MrsTracer.Provider.Debug("Flushing target proxy after receiving an exception.", new object[0]);
						CommonUtils.CatchKnownExceptions(new Action(proxyPool.Flush), null);
					}
					throw;
				}
			});
		}

		// Token: 0x060000A0 RID: 160 RVA: 0x00008240 File Offset: 0x00006440
		private void FlushBatchToFolder(List<MessageRec> batch, IFxProxyPool proxyPool)
		{
			if (batch.Count == 0)
			{
				return;
			}
			MrsTracer.Provider.Function("MapiSourceMailbox.FlushBatchToFolder({0} messages)", new object[]
			{
				batch.Count
			});
			byte[] folderId = batch[0].FolderId;
			using (MapiSourceFolder folder = base.GetFolder<MapiSourceFolder>(folderId))
			{
				if (folder == null)
				{
					MrsTracer.Provider.Debug("Folder {0} is missing in source. Will sync its deletion later.", new object[]
					{
						TraceUtils.DumpEntryId(folderId)
					});
				}
				else
				{
					folder.CopyBatch(proxyPool, batch);
				}
			}
		}

		// Token: 0x060000A1 RID: 161 RVA: 0x000082DC File Offset: 0x000064DC
		private void ReadRulesFromAllFolders()
		{
			List<FolderRec> list = ((IMailbox)this).EnumerateFolderHierarchy(EnumerateFolderHierarchyFlags.None, null);
			foreach (FolderRec folderRec in list)
			{
				if (folderRec.FolderType != FolderType.Search)
				{
					using (MapiSourceFolder folder = base.GetFolder<MapiSourceFolder>(folderRec.EntryId))
					{
						if (folder == null)
						{
							MrsTracer.Provider.Debug("Folder {0} is missing in source while reading rules. Will sync its deletion later.", new object[]
							{
								TraceUtils.DumpEntryId(folderRec.EntryId)
							});
							break;
						}
						using (base.RHTracker.Start())
						{
							folder.Folder.GetRules(null);
						}
					}
				}
			}
		}
	}
}
