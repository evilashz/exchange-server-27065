using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Mapi;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x02000011 RID: 17
	internal class StorageSourceMailbox : StorageMailbox, ISourceMailbox, IMailbox, IDisposable
	{
		// Token: 0x060000E3 RID: 227 RVA: 0x000094AE File Offset: 0x000076AE
		public StorageSourceMailbox(LocalMailboxFlags flags) : base(flags)
		{
		}

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x060000E4 RID: 228 RVA: 0x000094B7 File Offset: 0x000076B7
		internal override bool SupportsSavingSyncState
		{
			get
			{
				return true;
			}
		}

		// Token: 0x060000E5 RID: 229 RVA: 0x000094BC File Offset: 0x000076BC
		byte[] ISourceMailbox.GetMailboxBasicInfo(MailboxSignatureFlags flags)
		{
			MrsTracer.Provider.Function("StorageSourceMailbox.GetMailboxBasicInfo", new object[0]);
			base.CheckDisposed();
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

		// Token: 0x060000E6 RID: 230 RVA: 0x0000959C File Offset: 0x0000779C
		ISourceFolder ISourceMailbox.GetFolder(byte[] entryId)
		{
			MrsTracer.Provider.Function("StorageSourceMailbox.GetFolder({0})", new object[]
			{
				TraceUtils.DumpEntryId(entryId)
			});
			return base.GetFolder<StorageSourceFolder>(entryId);
		}

		// Token: 0x060000E7 RID: 231 RVA: 0x000095D0 File Offset: 0x000077D0
		void ISourceMailbox.CopyTo(IFxProxy fxProxy, PropTag[] excludeTags)
		{
			MrsTracer.Provider.Function("StorageSourceMailbox.CopyTo", new object[0]);
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
						base.StoreSession.Mailbox.MapiStore.ExportObject(fxProxyBudgetWrapper, copyPropertiesFlags, excludeTags);
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

		// Token: 0x060000E8 RID: 232 RVA: 0x000096D4 File Offset: 0x000078D4
		void ISourceMailbox.SetMailboxSyncState(string syncStateStr)
		{
			base.SetMailboxSyncState(syncStateStr);
		}

		// Token: 0x060000E9 RID: 233 RVA: 0x000096DD File Offset: 0x000078DD
		string ISourceMailbox.GetMailboxSyncState()
		{
			return base.GetMailboxSyncState();
		}

		// Token: 0x060000EA RID: 234 RVA: 0x0000971C File Offset: 0x0000791C
		MailboxChangesManifest ISourceMailbox.EnumerateHierarchyChanges(EnumerateHierarchyChangesFlags flags, int maxChanges)
		{
			bool catchup = flags.HasFlag(EnumerateHierarchyChangesFlags.Catchup);
			return this.EnumerateHierarchyChanges(catchup, (SyncHierarchyManifestState hierarchyData) => this.DoManifestSync(flags, maxChanges, hierarchyData, this.StoreSession.Mailbox.MapiStore));
		}

		// Token: 0x060000EB RID: 235 RVA: 0x00009770 File Offset: 0x00007970
		void ISourceMailbox.ExportMessages(List<MessageRec> messages, IFxProxyPool proxyPool, ExportMessagesFlags flags, PropTag[] propsToCopyExplicitly, PropTag[] excludeProps)
		{
			MrsTracer.Provider.Function("StorageSourceMailbox.ExportMessages({0} messages)", new object[]
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

		// Token: 0x060000EC RID: 236 RVA: 0x00009898 File Offset: 0x00007A98
		void ISourceMailbox.ExportFolders(List<byte[]> folderIds, IFxProxyPool proxyPool, ExportFoldersDataToCopyFlags exportFoldersDataToCopyFlags, GetFolderRecFlags folderRecFlags, PropTag[] additionalFolderRecProps, CopyPropertiesFlags copyPropertiesFlags, PropTag[] excludeProps, AclFlags extendedAclFlags)
		{
			MrsTracer.ProxyClient.Function("StorageSourceMailbox.ExportFolders", new object[0]);
			base.VerifyMailboxConnection(VerifyMailboxConnectionFlags.None);
			bool exportCompleted = false;
			CommonUtils.ProcessKnownExceptions(delegate
			{
				foreach (byte[] folderEntryId in folderIds)
				{
					this.ExportSingleFolder(proxyPool, folderEntryId, exportFoldersDataToCopyFlags, folderRecFlags, additionalFolderRecProps, copyPropertiesFlags, excludeProps, extendedAclFlags);
				}
				exportCompleted = true;
				proxyPool.Flush();
			}, delegate(Exception ex)
			{
				if (!exportCompleted)
				{
					MrsTracer.Provider.Debug("Flushing target proxy after receiving an exception.", new object[0]);
					CommonUtils.CatchKnownExceptions(new Action(proxyPool.Flush), null);
				}
				return false;
			});
		}

		// Token: 0x060000ED RID: 237 RVA: 0x0000992F File Offset: 0x00007B2F
		List<ReplayActionResult> ISourceMailbox.ReplayActions(List<ReplayAction> actions)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060000EE RID: 238 RVA: 0x00009938 File Offset: 0x00007B38
		protected override StoreSession CreateStoreConnection(MailboxConnectFlags mailboxConnectFlags)
		{
			StoreSession storeSession = base.CreateStoreConnection(mailboxConnectFlags);
			if ((base.Flags.HasFlag(LocalMailboxFlags.Move) || base.Flags.HasFlag(LocalMailboxFlags.PublicFolderMove)) && !mailboxConnectFlags.HasFlag(MailboxConnectFlags.DoNotOpenMapiSession) && !mailboxConnectFlags.HasFlag(MailboxConnectFlags.NonMrsLogon) && !mailboxConnectFlags.HasFlag(MailboxConnectFlags.PublicFolderHierarchyReplication) && !mailboxConnectFlags.HasFlag(MailboxConnectFlags.ValidateOnly))
			{
				storeSession.IsMoveSource = true;
			}
			return storeSession;
		}

		// Token: 0x060000EF RID: 239 RVA: 0x000099D9 File Offset: 0x00007BD9
		protected override Exception GetMailboxInTransitException(Exception innerException)
		{
			MrsTracer.Provider.Error("Source mailbox is being moved.", new object[0]);
			return new SourceMailboxAlreadyBeingMovedTransientException(innerException);
		}

		// Token: 0x060000F0 RID: 240 RVA: 0x000099F6 File Offset: 0x00007BF6
		protected override OpenEntryFlags GetFolderOpenEntryFlags()
		{
			if (base.IsPublicFolderMove)
			{
				return OpenEntryFlags.Modify | OpenEntryFlags.DontThrowIfEntryIsMissing;
			}
			return OpenEntryFlags.DontThrowIfEntryIsMissing;
		}

		// Token: 0x060000F1 RID: 241 RVA: 0x00009C90 File Offset: 0x00007E90
		protected override void CopySingleMessage(MessageRec curMsg, IFolderProxy folderProxy, PropTag[] propsToCopyExplicitly, PropTag[] excludeProps)
		{
			ExecutionContext.Create(new DataContext[]
			{
				new OperationDataContext("StorageSourceMailbox.CopySingleMessage", OperationType.None),
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

		// Token: 0x060000F2 RID: 242 RVA: 0x00009D08 File Offset: 0x00007F08
		private static IFolderProxy GetFolderProxyForExportFolder(IFxProxyPool proxyPool, IFolder sourceFolder, ExportFoldersDataToCopyFlags exportFoldersDataToCopyFlags, GetFolderRecFlags folderRecFlags, PropTag[] additionalFolderRecProps)
		{
			IFolderProxy result;
			if ((exportFoldersDataToCopyFlags & ExportFoldersDataToCopyFlags.OutputCreateMessages) == ExportFoldersDataToCopyFlags.OutputCreateMessages)
			{
				FolderRec folderRec = sourceFolder.GetFolderRec(additionalFolderRecProps, folderRecFlags);
				result = proxyPool.CreateFolder(folderRec);
			}
			else
			{
				result = proxyPool.GetFolderProxy(sourceFolder.GetFolderId());
			}
			return result;
		}

		// Token: 0x060000F3 RID: 243 RVA: 0x00009D40 File Offset: 0x00007F40
		private void ExportExtendedAcls(AclFlags extendedAclFlags, ISourceFolder srcFolder, IFolderProxy targetFolder)
		{
			base.VerifyCapability(MRSProxyCapabilities.SetItemProperties, CapabilityCheck.BothMRSAndOtherProvider);
			if (extendedAclFlags.HasFlag(AclFlags.FolderAcl))
			{
				PropValueData[][] extendedAcl = srcFolder.GetExtendedAcl(AclFlags.FolderAcl);
				if (extendedAcl != null)
				{
					targetFolder.SetItemProperties(new FolderAcl(AclFlags.FolderAcl, extendedAcl));
				}
			}
			if (extendedAclFlags.HasFlag(AclFlags.FreeBusyAcl))
			{
				PropValueData[][] extendedAcl2 = srcFolder.GetExtendedAcl(AclFlags.FreeBusyAcl);
				if (extendedAcl2 != null)
				{
					targetFolder.SetItemProperties(new FolderAcl(AclFlags.FreeBusyAcl, extendedAcl2));
				}
			}
		}

		// Token: 0x060000F4 RID: 244 RVA: 0x00009EE0 File Offset: 0x000080E0
		private void CopyMessageBatch(List<MessageRec> messages, IFxProxyPool proxyPool)
		{
			MrsTracer.Provider.Function("StorageSourceMailbox.CopyMessageBatch({0} messages)", new object[]
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
				CommonUtils.ProcessKnownExceptions(delegate
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
				}, delegate(Exception ex)
				{
					if (!exportCompleted)
					{
						MrsTracer.Provider.Debug("Flushing target proxy after receiving an exception.", new object[0]);
						CommonUtils.CatchKnownExceptions(new Action(proxyPool.Flush), null);
					}
					return false;
				});
			});
		}

		// Token: 0x060000F5 RID: 245 RVA: 0x0000A17C File Offset: 0x0000837C
		private void ExportSingleFolder(IFxProxyPool proxyPool, byte[] folderEntryId, ExportFoldersDataToCopyFlags exportFoldersDataToCopyFlags, GetFolderRecFlags folderRecFlags, PropTag[] additionalFolderRecProps, CopyPropertiesFlags copyPropertiesFlags, PropTag[] excludePropertiesFromCopy, AclFlags extendedAclFlags)
		{
			ExecutionContext.Create(new DataContext[]
			{
				new OperationDataContext("StorageSourceMailbox.ExportSingleFolder", OperationType.None),
				new EntryIDsDataContext(folderEntryId),
				new SimpleValueDataContext("exportFoldersDataToCopyFlags", exportFoldersDataToCopyFlags),
				new SimpleValueDataContext("folderRecFlags", folderRecFlags),
				new PropTagsDataContext(additionalFolderRecProps),
				new SimpleValueDataContext("copyPropertiesFlags", copyPropertiesFlags),
				new PropTagsDataContext(excludePropertiesFromCopy),
				new SimpleValueDataContext("extendedAclFlags", extendedAclFlags)
			}).Execute(delegate
			{
				using (this.RHTracker.Start())
				{
					using (ISourceFolder folder = this.GetFolder<StorageSourceFolder>(folderEntryId))
					{
						if (folder == null)
						{
							MrsTracer.Provider.Debug("Folder {0} is missing in source. Skipping.", new object[]
							{
								TraceUtils.DumpEntryId(folderEntryId)
							});
						}
						else
						{
							using (IFolderProxy folderProxyForExportFolder = StorageSourceMailbox.GetFolderProxyForExportFolder(proxyPool, folder, exportFoldersDataToCopyFlags, folderRecFlags, additionalFolderRecProps))
							{
								if (extendedAclFlags != AclFlags.None)
								{
									this.ExportExtendedAcls(extendedAclFlags, folder, folderProxyForExportFolder);
								}
								using (FxProxyBudgetWrapper fxProxyBudgetWrapper = new FxProxyBudgetWrapper(folderProxyForExportFolder, false, new Func<IDisposable>(this.RHTracker.StartExclusive), new Action<uint>(this.RHTracker.Charge)))
								{
									if (exportFoldersDataToCopyFlags.HasFlag(ExportFoldersDataToCopyFlags.IncludeCopyToStream))
									{
										folder.CopyTo(fxProxyBudgetWrapper, copyPropertiesFlags, excludePropertiesFromCopy);
									}
								}
							}
						}
					}
				}
			});
		}

		// Token: 0x060000F6 RID: 246 RVA: 0x0000A28C File Offset: 0x0000848C
		private void FlushBatchToFolder(List<MessageRec> batch, IFxProxyPool proxyPool)
		{
			if (batch.Count == 0)
			{
				return;
			}
			MrsTracer.Provider.Function("StorageSourceMailbox.FlushBatchToFolder({0} messages)", new object[]
			{
				batch.Count
			});
			byte[] folderId = batch[0].FolderId;
			using (StorageSourceFolder folder = base.GetFolder<StorageSourceFolder>(folderId))
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
	}
}
