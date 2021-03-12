using System;
using System.Collections.Generic;
using System.Globalization;
using System.Security.AccessControl;
using System.Security.Principal;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration.ConfigurationSettings;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Data.Storage.ActiveManager;
using Microsoft.Exchange.Extensions;
using Microsoft.Exchange.Search.Fast;
using Microsoft.Mapi;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x02000008 RID: 8
	internal class StorageDestinationMailbox : StorageMailbox, IDestinationMailbox, IMailbox, IDisposable
	{
		// Token: 0x06000083 RID: 131 RVA: 0x000074BD File Offset: 0x000056BD
		public StorageDestinationMailbox(LocalMailboxFlags flags) : base(flags)
		{
		}

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x06000084 RID: 132 RVA: 0x000074C6 File Offset: 0x000056C6
		public MrsPSHandler PSHandler
		{
			get
			{
				if (this.psHandlerInternal == null)
				{
					this.psHandlerInternal = new MrsPSHandler("StorageDestinationMailbox.SetMailboxSettings Monad");
				}
				return this.psHandlerInternal;
			}
		}

		// Token: 0x06000085 RID: 133 RVA: 0x000074E6 File Offset: 0x000056E6
		bool IDestinationMailbox.MailboxExists()
		{
			return base.StoreSession != null;
		}

		// Token: 0x06000086 RID: 134 RVA: 0x000074F4 File Offset: 0x000056F4
		CreateMailboxResult IDestinationMailbox.CreateMailbox(byte[] mailboxData, MailboxSignatureFlags sourceSignatureFlags)
		{
			MrsTracer.Provider.Function("StorageDestinationMailbox.CreateMailbox", new object[0]);
			base.CheckDisposed();
			base.ResolveMDB(true);
			CreateMailboxResult createMailboxResult = base.CreateMailbox(mailboxData, sourceSignatureFlags);
			if (createMailboxResult != CreateMailboxResult.Success)
			{
				return createMailboxResult;
			}
			base.StoreSession = this.ConnectToTargetMailbox(true, MailboxConnectFlags.None);
			this.AfterConnect();
			return CreateMailboxResult.Success;
		}

		// Token: 0x06000087 RID: 135 RVA: 0x00007548 File Offset: 0x00005748
		void IDestinationMailbox.ProcessMailboxSignature(byte[] mailboxData)
		{
			MrsTracer.Provider.Function("StorageDestinationMailbox.ProcessMailboxSignature", new object[0]);
			base.CheckDisposed();
			using (base.RHTracker.Start())
			{
				base.ProcessMailboxSignature(mailboxData);
			}
		}

		// Token: 0x06000088 RID: 136 RVA: 0x000075A0 File Offset: 0x000057A0
		IDestinationFolder IDestinationMailbox.GetFolder(byte[] entryId)
		{
			MrsTracer.Provider.Function("StorageDestinationMailbox.GetFolder({0})", new object[]
			{
				TraceUtils.DumpEntryId(entryId)
			});
			IDestinationFolder folder;
			using (base.RHTracker.Start())
			{
				folder = base.GetFolder<StorageDestinationFolder>(entryId);
			}
			return folder;
		}

		// Token: 0x06000089 RID: 137 RVA: 0x00007600 File Offset: 0x00005800
		IFxProxy IDestinationMailbox.GetFxProxy()
		{
			MrsTracer.Provider.Function("StorageDestinationMailbox.GetFxProxy", new object[0]);
			base.VerifyMailboxConnection(VerifyMailboxConnectionFlags.None);
			IFxProxy result;
			using (base.RHTracker.Start())
			{
				FxProxyBudgetWrapper proxy = new FxProxyBudgetWrapper(base.StoreSession.Mailbox.MapiStore.GetFxProxyCollector(), true, new Func<IDisposable>(base.RHTracker.Start), new Action<uint>(base.RHTracker.Charge));
				FxProxyWrapper fxProxyWrapper = new FxProxyWrapper(proxy, null);
				result = fxProxyWrapper;
			}
			return result;
		}

		// Token: 0x0600008A RID: 138 RVA: 0x0000769C File Offset: 0x0000589C
		IFxProxyPool IDestinationMailbox.GetFxProxyPool(ICollection<byte[]> folderIds)
		{
			MrsTracer.Provider.Function("StorageDestinationMailbox.GetFxProxyPool", new object[0]);
			base.VerifyMailboxConnection(VerifyMailboxConnectionFlags.None);
			IFxProxyPool result;
			using (base.RHTracker.Start())
			{
				StorageFxProxyPool proxy = new StorageFxProxyPool(this, folderIds);
				IFxProxyPool proxy2 = new FxProxyPoolBudgetWrapper(proxy, true, new Func<IDisposable>(base.RHTracker.Start), new Action<uint>(base.RHTracker.Charge));
				IFxProxyPool fxProxyPool = new FxProxyPoolWrapper(proxy2, null);
				result = fxProxyPool;
			}
			return result;
		}

		// Token: 0x0600008B RID: 139 RVA: 0x00007730 File Offset: 0x00005930
		PropProblemData[] IDestinationMailbox.SetProps(PropValueData[] pvda)
		{
			MrsTracer.Provider.Function("StorageDestinationMailbox.SetProps", new object[0]);
			if (pvda == null || pvda.Length == 0)
			{
				return null;
			}
			base.VerifyMailboxConnection(VerifyMailboxConnectionFlags.None);
			object[] array = new object[pvda.Length];
			PropTag[] array2 = new PropTag[pvda.Length];
			for (int i = 0; i < pvda.Length; i++)
			{
				array2[i] = (PropTag)pvda[i].PropTag;
				array[i] = pvda[i].Value;
			}
			base.StoreSession.Mailbox.SetProperties(base.ConvertPropTagsToDefinitions(array2), array);
			using (base.RHTracker.Start())
			{
				base.StoreSession.Mailbox.Save();
				base.StoreSession.Mailbox.Load();
			}
			return null;
		}

		// Token: 0x0600008C RID: 140 RVA: 0x000077FC File Offset: 0x000059FC
		void IDestinationMailbox.CreateFolder(FolderRec sourceFolder, CreateFolderFlags createFolderFlags, out byte[] newFolderId)
		{
			MrsTracer.Provider.Function("StorageDestinationMailbox.CreateFolder(\"{0}\")", new object[]
			{
				sourceFolder.FolderName
			});
			base.VerifyMailboxConnection(VerifyMailboxConnectionFlags.None);
			FolderRec folderRec = null;
			newFolderId = null;
			bool isSearchFolder = sourceFolder.FolderType == FolderType.Search;
			string text = sourceFolder.FolderName;
			if (string.IsNullOrWhiteSpace(text))
			{
				text = Guid.NewGuid().ToString();
			}
			using (base.RHTracker.Start())
			{
				if (sourceFolder.EntryId != null)
				{
					using (StorageDestinationFolder folder = base.GetFolder<StorageDestinationFolder>(sourceFolder.EntryId))
					{
						if (folder != null)
						{
							MrsTracer.Provider.Debug("Source folder '{0}' already exists in destination.", new object[]
							{
								sourceFolder.FolderName
							});
							if (createFolderFlags.HasFlag(CreateFolderFlags.FailIfExists))
							{
								throw new FolderAlreadyExistsException(sourceFolder.FolderName);
							}
							folderRec = ((IFolder)folder).GetFolderRec(null, GetFolderRecFlags.None);
							newFolderId = folderRec.EntryId;
						}
					}
				}
				if (newFolderId == null)
				{
					CreateMode createMode = CreateMode.OverrideFolderCreationBlock;
					if (createFolderFlags.HasFlag(CreateFolderFlags.CreatePublicFolderDumpster))
					{
						createMode |= CreateMode.CreatePublicFolderDumpster;
					}
					using (CoreFolder coreFolder = createFolderFlags.HasFlag(CreateFolderFlags.InternalAccess) ? CoreFolder.CreateSecure(base.StoreSession, base.GetFolderId(sourceFolder.ParentId), isSearchFolder, text, createMode) : CoreFolder.Create(base.StoreSession, base.GetFolderId(sourceFolder.ParentId), isSearchFolder, text, createMode))
					{
						if (sourceFolder.EntryId != null)
						{
							coreFolder.PropertyBag[StoreObjectSchema.EntryId] = sourceFolder.EntryId;
						}
						string value;
						if (!string.IsNullOrEmpty(sourceFolder.FolderClass) && StorageDestinationMailbox.folderClassMap.TryGetValue(sourceFolder.FolderClass, out value))
						{
							coreFolder.PropertyBag[StoreObjectSchema.ContainerClass] = value;
						}
						coreFolder.Save(SaveMode.FailOnAnyConflict);
						coreFolder.PropertyBag.Load(FolderSchema.Instance.AutoloadProperties);
						newFolderId = coreFolder.Id.ObjectId.ProviderLevelItemId;
						goto IL_225;
					}
				}
				if (!CommonUtils.IsSameEntryId(folderRec.ParentId, sourceFolder.ParentId))
				{
					MrsTracer.Common.Debug("Existing folder is under the wrong parent. Moving it.", new object[0]);
					((IDestinationMailbox)this).MoveFolder(sourceFolder.EntryId, folderRec.ParentId, sourceFolder.ParentId);
				}
				IL_225:
				PropTag[] promotedProperties = sourceFolder.GetPromotedProperties();
				if ((promotedProperties != null && promotedProperties.Length > 0) || (sourceFolder.Restrictions != null && sourceFolder.Restrictions.Length > 0) || (sourceFolder.Views != null && sourceFolder.Views.Length > 0) || (sourceFolder.ICSViews != null && sourceFolder.ICSViews.Length > 0))
				{
					using (StorageDestinationFolder folder2 = base.GetFolder<StorageDestinationFolder>(sourceFolder.EntryId))
					{
						folder2.SetExtendedProps(promotedProperties, sourceFolder.Restrictions, sourceFolder.Views, sourceFolder.ICSViews);
					}
				}
			}
		}

		// Token: 0x0600008D RID: 141 RVA: 0x00007B24 File Offset: 0x00005D24
		void IDestinationMailbox.MoveFolder(byte[] folderId, byte[] oldParentId, byte[] newParentId)
		{
			MrsTracer.Provider.Function("StorageDestinationMailbox.MoveFolder", new object[0]);
			base.VerifyMailboxConnection(VerifyMailboxConnectionFlags.None);
			using (base.RHTracker.Start())
			{
				using (CoreFolder coreFolder = CoreFolder.Bind(base.StoreSession, base.GetFolderId(oldParentId)))
				{
					using (CoreFolder coreFolder2 = CoreFolder.Bind(base.StoreSession, base.GetFolderId(newParentId)))
					{
						GroupOperationResult groupOperationResult = coreFolder.MoveFolder(coreFolder2, StoreObjectId.FromProviderSpecificId(folderId));
						if (groupOperationResult.OperationResult != OperationResult.Succeeded)
						{
							MrsTracer.Provider.Error("StorageDestinationMailbox.MoveFolder failed with {0}", new object[]
							{
								groupOperationResult.Exception.ToString()
							});
							groupOperationResult.Exception.PreserveExceptionStack();
							throw groupOperationResult.Exception;
						}
					}
				}
			}
		}

		// Token: 0x0600008E RID: 142 RVA: 0x00007C1C File Offset: 0x00005E1C
		void IDestinationMailbox.DeleteFolder(FolderRec folderRec)
		{
			MrsTracer.Provider.Function("StorageDestinationMailbox.DeleteFolder(\"{0}\")", new object[]
			{
				folderRec.FolderName
			});
			base.VerifyMailboxConnection(VerifyMailboxConnectionFlags.None);
			using (base.RHTracker.Start())
			{
				using (CoreFolder coreFolder = CoreFolder.Bind(base.StoreSession, base.GetFolderId(folderRec.ParentId)))
				{
					GroupOperationResult groupOperationResult = coreFolder.DeleteFolder(DeleteFolderFlags.DeleteMessages | DeleteFolderFlags.DeleteSubFolders, StoreObjectId.FromProviderSpecificId(folderRec.EntryId));
					if (groupOperationResult.OperationResult != OperationResult.Succeeded)
					{
						MrsTracer.Provider.Error("StorageDestinationMailbox.DeleteFolder failed with '{0}'. Folder name: '{1}', type: '{2}', entry id: '{3}'.", new object[]
						{
							groupOperationResult.Exception.ToString(),
							folderRec.FolderName,
							folderRec.FolderType,
							TraceUtils.DumpEntryId(folderRec.EntryId)
						});
						groupOperationResult.Exception.PreserveExceptionStack();
						throw groupOperationResult.Exception;
					}
				}
			}
		}

		// Token: 0x0600008F RID: 143 RVA: 0x00007D28 File Offset: 0x00005F28
		void IDestinationMailbox.SetMailboxSecurityDescriptor(RawSecurityDescriptor sd)
		{
			base.SetMailboxSecurityDescriptor(sd);
		}

		// Token: 0x06000090 RID: 144 RVA: 0x00007D31 File Offset: 0x00005F31
		void IDestinationMailbox.SetUserSecurityDescriptor(RawSecurityDescriptor sd)
		{
			base.SetUserSecurityDescriptor(sd);
		}

		// Token: 0x06000091 RID: 145 RVA: 0x00007D3C File Offset: 0x00005F3C
		void IDestinationMailbox.PreFinalSyncDataProcessing(int? sourceMailboxVersion)
		{
			try
			{
				base.StoreSession.BlockFolderCreation = false;
				base.StoreSession.PreFinalSyncDataProcessing(sourceMailboxVersion);
			}
			finally
			{
				if (!base.TestIntegration.DisableFolderCreationBlockFeature)
				{
					base.StoreSession.BlockFolderCreation = true;
				}
				base.StoreSession.MailboxMoveStage = MailboxMoveStage.None;
			}
		}

		// Token: 0x06000092 RID: 146 RVA: 0x00007D9C File Offset: 0x00005F9C
		ConstraintCheckResultType IDestinationMailbox.CheckDataGuarantee(DateTime commitTimestamp, out LocalizedString failureReason)
		{
			return CommonUtils.DumpsterStatus.CheckReplicationFlushed(base.MdbGuid, commitTimestamp, out failureReason);
		}

		// Token: 0x06000093 RID: 147 RVA: 0x00007DB0 File Offset: 0x00005FB0
		void IDestinationMailbox.ForceLogRoll()
		{
			using (ExRpcAdmin rpcAdmin = base.GetRpcAdmin())
			{
				try
				{
					using (base.RHTracker.Start())
					{
						rpcAdmin.ForceNewLog(base.MdbGuid);
					}
				}
				catch (MapiExceptionVersion)
				{
				}
			}
		}

		// Token: 0x06000094 RID: 148 RVA: 0x00007E24 File Offset: 0x00006024
		List<ReplayAction> IDestinationMailbox.GetActions(string replaySyncState, int maxNumberOfActions)
		{
			MrsTracer.Provider.Function("StorageDestinationMailbox.GetActions", new object[0]);
			base.VerifyMailboxConnection(VerifyMailboxConnectionFlags.None);
			IActionsSource actionsSource = new StorageActionsSource((MailboxSession)base.StoreSession);
			StorageActionWatermark storageActionWatermark;
			if ((storageActionWatermark = StorageActionWatermark.Deserialize(replaySyncState)) == null)
			{
				storageActionWatermark = new StorageActionWatermark
				{
					TimeStamp = DateTime.UtcNow.AddHours(-1.0)
				};
			}
			IActionWatermark watermark = storageActionWatermark;
			List<ReplayAction> list = new List<ReplayAction>(maxNumberOfActions);
			foreach (ReplayAction item in actionsSource.ReadActions(watermark))
			{
				if (list.Count == maxNumberOfActions)
				{
					break;
				}
				list.Add(item);
			}
			return list;
		}

		// Token: 0x06000095 RID: 149 RVA: 0x00007EE8 File Offset: 0x000060E8
		void IDestinationMailbox.SetMailboxSettings(ItemPropertiesBase item)
		{
			MrsTracer.Provider.Function("StorageDestinationMailbox.SetMailboxSettings", new object[0]);
			base.VerifyMailboxConnection(VerifyMailboxConnectionFlags.None);
			MailboxSession mailboxSession = base.StoreSession as MailboxSession;
			item.Apply(this.PSHandler, mailboxSession);
		}

		// Token: 0x06000096 RID: 150 RVA: 0x00007F2C File Offset: 0x0000612C
		public override void Disconnect()
		{
			base.CheckDisposed();
			lock (this)
			{
				base.Disconnect();
				if (this.psHandlerInternal != null)
				{
					this.psHandlerInternal.Dispose();
					this.psHandlerInternal = null;
				}
			}
		}

		// Token: 0x06000097 RID: 151 RVA: 0x00007F88 File Offset: 0x00006188
		protected override void PerformPreLogonOperations(ExchangePrincipal exchangePrincipal, MailboxConnectFlags mailboxConnectFlags, string clientAppId)
		{
			if (!base.IsMove && !base.IsPublicFolderMailbox && !mailboxConnectFlags.HasFlag(MailboxConnectFlags.ValidateOnly) && !mailboxConnectFlags.HasFlag(MailboxConnectFlags.NonMrsLogon) && ConfigBase<MRSConfigSchema>.GetConfig<bool>("OwnerLogonToMergeDestination"))
			{
				MailboxSession.InitializationFlags initFlags = MailboxSession.InitializationFlags.DefaultFolders | MailboxSession.InitializationFlags.UserConfigurationManager | MailboxSession.InitializationFlags.UseNamedProperties;
				MailboxAccessInfo accessInfo = new MailboxAccessInfo(new WindowsPrincipal(WindowsIdentity.GetCurrent()));
				DefaultFolderType[] foldersToInit = (DefaultFolderType[])Enum.GetValues(typeof(DefaultFolderType));
				using (MailboxSession.ConfigurableOpen(exchangePrincipal, accessInfo, CultureInfo.InvariantCulture, clientAppId, LogonType.Owner, null, initFlags, foldersToInit))
				{
				}
			}
		}

		// Token: 0x06000098 RID: 152 RVA: 0x00008034 File Offset: 0x00006234
		protected override Exception GetMailboxInTransitException(Exception innerException)
		{
			MrsTracer.Provider.Error("Destination mailbox is being moved into.", new object[0]);
			return new DestMailboxAlreadyBeingMovedTransientException(innerException);
		}

		// Token: 0x06000099 RID: 153 RVA: 0x00008051 File Offset: 0x00006251
		protected override OpenEntryFlags GetFolderOpenEntryFlags()
		{
			return OpenEntryFlags.Modify | OpenEntryFlags.DontThrowIfEntryIsMissing;
		}

		// Token: 0x0600009A RID: 154 RVA: 0x00008058 File Offset: 0x00006258
		protected override bool IsMailboxCapabilitySupportedInternal(MailboxCapabilities capability)
		{
			return capability == MailboxCapabilities.ExportFolders || base.IsMailboxCapabilitySupportedInternal(capability);
		}

		// Token: 0x0600009B RID: 155 RVA: 0x00008074 File Offset: 0x00006274
		protected override StoreSession CreateStoreConnection(MailboxConnectFlags mailboxConnectFlags)
		{
			StoreSession storeSession = null;
			StoreSession result;
			try
			{
				if (base.UseHomeMDB || base.IsPublicFolderMove || base.IsFolderMove || mailboxConnectFlags.HasFlag(MailboxConnectFlags.NonMrsLogon))
				{
					storeSession = base.CreateStoreConnection(mailboxConnectFlags);
					if (storeSession != null && base.Flags.HasFlag(LocalMailboxFlags.WordBreak))
					{
						bool invalidateAnnotations = base.Flags.HasFlag(LocalMailboxFlags.InvalidateContentIndexAnnotations);
						storeSession.ContentIndexingSession = ContentIndexingSession.CreateSession(storeSession, invalidateAnnotations);
					}
				}
				else
				{
					storeSession = this.ConnectToTargetMailbox(false, mailboxConnectFlags);
				}
				StoreSession storeSession2 = storeSession;
				storeSession = null;
				result = storeSession2;
			}
			finally
			{
				if (storeSession != null)
				{
					storeSession.Dispose();
				}
			}
			return result;
		}

		// Token: 0x0600009C RID: 156 RVA: 0x0000812C File Offset: 0x0000632C
		private StoreSession ConnectToTargetMailbox(bool mailboxMustExist, MailboxConnectFlags mailboxConnectFlags)
		{
			MrsTracer.Provider.Function("StorageDestinationMailbox.ConnectToTargetMailbox", new object[0]);
			base.CheckDisposed();
			StoreSession result = null;
			ConnectFlag connectFlag = ConnectFlag.UseAdminPrivilege;
			connectFlag |= ConnectFlag.UseRpcContextPool;
			if (!mailboxMustExist)
			{
				bool flag = false;
				MrsTracer.Provider.Debug("Checking if destination mailbox exists...", new object[0]);
				using (ExRpcAdmin rpcAdmin = base.GetRpcAdmin())
				{
					using (base.RHTracker.Start())
					{
						flag = MapiUtils.IsMailboxInDatabase(rpcAdmin, base.MdbGuid, base.MailboxGuid);
					}
				}
				if (!flag)
				{
					MrsTracer.Provider.Debug("Mailbox {0} does not exist in database {1}", new object[]
					{
						base.MailboxGuid,
						base.MdbGuid
					});
					return null;
				}
				MrsTracer.Provider.Debug("Mailbox {0} exists in database {1}", new object[]
				{
					base.MailboxGuid,
					base.MdbGuid
				});
			}
			StoreSession storeSession = null;
			try
			{
				using (base.RHTracker.Start())
				{
					DefaultFolderType[] foldersToInit = (DefaultFolderType[])Enum.GetValues(typeof(DefaultFolderType));
					MailboxSession.InitializationFlags initializationFlags = MailboxSession.InitializationFlags.DefaultFolders | MailboxSession.InitializationFlags.UserConfigurationManager | MailboxSession.InitializationFlags.DeadSessionChecking | MailboxSession.InitializationFlags.RequestLocalRpc | MailboxSession.InitializationFlags.OverrideHomeMdb | MailboxSession.InitializationFlags.SuppressFolderIdPrefetch | MailboxSession.InitializationFlags.UseNamedProperties | MailboxSession.InitializationFlags.DisconnectedMailbox | MailboxSession.InitializationFlags.MoveUser;
					if (base.MbxType == MailboxType.DestMailboxCrossOrg)
					{
						initializationFlags |= MailboxSession.InitializationFlags.XForestMove;
					}
					MrsTracer.Provider.Debug("Opening Mailbox Session: mailbox='{0}', mailboxGuid={1}, dbGuid={2}, connectFlags=[{3}], initFlags=[{4}]", new object[]
					{
						base.TraceMailboxId,
						base.MailboxGuid.ToString(),
						base.MdbGuid.ToString(),
						connectFlag,
						initializationFlags
					});
					OrganizationId organizationId;
					if (base.PartitionHint != null)
					{
						ADSessionSettings adsessionSettings = ADSessionSettings.FromTenantPartitionHint(base.PartitionHint);
						organizationId = adsessionSettings.CurrentOrganizationId;
					}
					else
					{
						organizationId = OrganizationId.ForestWideOrgId;
					}
					ExchangePrincipal exchangePrincipal = ExchangePrincipal.FromMailboxData(base.MailboxGuid, base.MdbGuid, organizationId, StorageMailbox.CultureInfos);
					string clientInfoString = "Client=MSExchangeMigration";
					if ((mailboxConnectFlags & MailboxConnectFlags.PublicFolderHierarchyReplication) != MailboxConnectFlags.None)
					{
						clientInfoString = "Client=PublicFolderSystem;Action=PublicFolderHierarchyReplication";
					}
					if (base.IsPublicFolderMailbox)
					{
						storeSession = PublicFolderSession.OpenAsMRS(exchangePrincipal, clientInfoString, OpenMailboxSessionFlags.None);
					}
					else
					{
						MailboxAccessInfo accessInfo = new MailboxAccessInfo(new WindowsPrincipal(WindowsIdentity.GetCurrent()));
						storeSession = MailboxSession.ConfigurableOpen(exchangePrincipal, accessInfo, CultureInfo.InvariantCulture, clientInfoString, LogonType.SystemService, null, initializationFlags, foldersToInit);
					}
					if (!base.TestIntegration.DisableFolderCreationBlockFeature)
					{
						storeSession.BlockFolderCreation = true;
					}
					if (storeSession != null && base.Flags.HasFlag(LocalMailboxFlags.WordBreak))
					{
						bool invalidateAnnotations = base.Flags.HasFlag(LocalMailboxFlags.InvalidateContentIndexAnnotations);
						storeSession.ContentIndexingSession = ContentIndexingSession.CreateSession(storeSession, invalidateAnnotations);
					}
					result = storeSession;
					storeSession = null;
				}
			}
			catch (Exception ex)
			{
				if (CommonUtils.ExceptionIs(ex, new WellKnownException[]
				{
					WellKnownException.MapiNotFound
				}))
				{
					base.VerifyMdbIsOnline(ex);
					if (mailboxMustExist)
					{
						throw;
					}
				}
				else
				{
					if (CommonUtils.ExceptionIs(ex, new WellKnownException[]
					{
						WellKnownException.MapiMailboxInTransit
					}))
					{
						throw this.GetMailboxInTransitException(ex);
					}
					throw;
				}
			}
			finally
			{
				if (storeSession != null)
				{
					storeSession.Dispose();
				}
			}
			return result;
		}

		// Token: 0x04000016 RID: 22
		private static readonly IReadOnlyDictionary<string, string> folderClassMap = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
		{
			{
				"Mail",
				"IPF.Note"
			},
			{
				"Calendar",
				"IPF.Appointment"
			},
			{
				"BirthdayCalendar",
				"IPF.Appointment.Birthday"
			},
			{
				"Contact",
				"IPF.Contact"
			},
			{
				"Journal",
				"IPF.Journal"
			},
			{
				"Note",
				"IPF.StickyNote"
			},
			{
				"Task",
				"IPF.Task"
			},
			{
				"Generic",
				"IPF"
			}
		};

		// Token: 0x04000017 RID: 23
		private MrsPSHandler psHandlerInternal;
	}
}
