using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.AccessControl;
using System.ServiceModel;
using System.Threading;
using System.Web;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.ConfigurationSettings;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Management;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Data.Storage.ActiveManager;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.VariantConfiguration;
using Microsoft.Mapi;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x02000010 RID: 16
	[ServiceBehavior(InstanceContextMode = InstanceContextMode.PerSession, AddressFilterMode = AddressFilterMode.Any, ConcurrencyMode = ConcurrencyMode.Multiple)]
	internal class MailboxReplicationProxyService : DisposeTrackableBase, IMailboxReplicationProxyService
	{
		// Token: 0x0600004D RID: 77 RVA: 0x00003E38 File Offset: 0x00002038
		static MailboxReplicationProxyService()
		{
			MailboxReplicationProxyService.activeConnectionsUpdateLock = new object();
			ServerThrottlingResource.InitializeServerThrottlingObjects(false);
		}

		// Token: 0x0600004E RID: 78 RVA: 0x00003E78 File Offset: 0x00002078
		public MailboxReplicationProxyService()
		{
			ADSession.DisableAdminTopologyMode();
			this.handleCache = new HandleCache();
			MrsTracer.ProxyService.Debug("MailboxReplicationProxyService instance created.", new object[0]);
			this.clientVersion = null;
			this.proxyControlFlags = ProxyControlFlags.None;
		}

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x0600004F RID: 79 RVA: 0x00003EDF File Offset: 0x000020DF
		public VersionInformation ClientVersion
		{
			get
			{
				return this.clientVersion;
			}
		}

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x06000050 RID: 80 RVA: 0x00003EE7 File Offset: 0x000020E7
		public bool UseCompression
		{
			get
			{
				return !this.proxyControlFlags.HasFlag(ProxyControlFlags.DoNotCompress);
			}
		}

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x06000051 RID: 81 RVA: 0x00003F02 File Offset: 0x00002102
		public bool UseBufferring
		{
			get
			{
				return !this.proxyControlFlags.HasFlag(ProxyControlFlags.DoNotBuffer);
			}
		}

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x06000052 RID: 82 RVA: 0x00003F1D File Offset: 0x0000211D
		// (set) Token: 0x06000053 RID: 83 RVA: 0x00003F25 File Offset: 0x00002125
		public int ExportBufferSizeFromMrsKB { get; private set; }

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x06000054 RID: 84 RVA: 0x00003F2E File Offset: 0x0000212E
		public bool SkipWLMThrottling
		{
			get
			{
				return this.proxyControlFlags.HasFlag(ProxyControlFlags.SkipWLMThrottling) || this.proxyControlFlags.HasFlag(ProxyControlFlags.DoNotApplyProxyThrottling);
			}
		}

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x06000055 RID: 85 RVA: 0x00003F61 File Offset: 0x00002161
		// (set) Token: 0x06000056 RID: 86 RVA: 0x00003F69 File Offset: 0x00002169
		public Guid ExchangeGuid { get; private set; }

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x06000057 RID: 87 RVA: 0x00003F72 File Offset: 0x00002172
		// (set) Token: 0x06000058 RID: 88 RVA: 0x00003F7A File Offset: 0x0000217A
		public bool IsE15OrHigher { get; private set; }

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x06000059 RID: 89 RVA: 0x00003F83 File Offset: 0x00002183
		// (set) Token: 0x0600005A RID: 90 RVA: 0x00003F8B File Offset: 0x0000218B
		public bool IsHighPriority { get; private set; }

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x0600005B RID: 91 RVA: 0x00003F94 File Offset: 0x00002194
		// (set) Token: 0x0600005C RID: 92 RVA: 0x00003F9C File Offset: 0x0000219C
		public bool IsInFinalization { get; private set; }

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x0600005D RID: 93 RVA: 0x00003FA5 File Offset: 0x000021A5
		private bool IsThrottled
		{
			get
			{
				return !this.proxyControlFlags.HasFlag(ProxyControlFlags.DoNotApplyProxyThrottling);
			}
		}

		// Token: 0x0600005E RID: 94 RVA: 0x00003FC0 File Offset: 0x000021C0
		public string DecompressString(byte[] data)
		{
			return CommonUtils.UnpackString(data, this.UseCompression);
		}

		// Token: 0x0600005F RID: 95 RVA: 0x00003FCE File Offset: 0x000021CE
		public byte[] CompressString(string str)
		{
			return CommonUtils.PackString(str, this.UseCompression);
		}

		// Token: 0x06000060 RID: 96 RVA: 0x00004048 File Offset: 0x00002248
		void IMailboxReplicationProxyService.ExchangeVersionInformation(VersionInformation clientVersion, out VersionInformation serverVersion)
		{
			MrsTracer.ProxyService.Function("MRSProxy.ExchangeVersionInformation", new object[0]);
			serverVersion = VersionInformation.MRSProxy;
			this.ExecuteServiceCall<object>(-1L, ExecutionFlags.ThrottlingNotRequired | ExecutionFlags.NoLock, DelayScopeKind.NoDelay, delegate(object o)
			{
				this.clientVersion = clientVersion;
				if (!clientVersion[24])
				{
					MrsTracer.ProxyService.Error("Client does not support TenantHint.", new object[0]);
					throw new UnsupportedClientVersionPermanentException(clientVersion.ComputerName, clientVersion.ToString(), "TenantHint");
				}
			});
		}

		// Token: 0x06000061 RID: 97 RVA: 0x0000411C File Offset: 0x0000231C
		ProxyServerInformation IMailboxReplicationProxyService.FindServerByDatabaseOrMailbox(string databaseId, Guid? physicalMailboxGuid, Guid? primaryMailboxGuid, byte[] tenantPartitionHintBytes)
		{
			MrsTracer.ProxyService.Function("MRSProxy.FindServerByDatabaseOrMailbox", new object[0]);
			ProxyServerInformation result = null;
			this.ExecuteServiceCall<object>(-1L, ExecutionFlags.ThrottlingNotRequired | ExecutionFlags.NoLock, DelayScopeKind.NoDelay, delegate(object o)
			{
				ProxyServerSettings proxyServerSettings;
				if (databaseId != null)
				{
					Guid guid;
					if (!Guid.TryParse(databaseId, out guid))
					{
						throw new UnexpectedErrorPermanentException(-2147024809);
					}
					proxyServerSettings = CommonUtils.MapDatabaseToProxyServer(new ADObjectId(guid, PartitionId.LocalForest.ForestFQDN));
				}
				else
				{
					proxyServerSettings = CommonUtils.MapMailboxToProxyServer(physicalMailboxGuid, primaryMailboxGuid, tenantPartitionHintBytes);
				}
				result = new ProxyServerInformation(proxyServerSettings.Fqdn, proxyServerSettings.IsProxyLocal);
			});
			return result;
		}

		// Token: 0x06000062 RID: 98 RVA: 0x000042E8 File Offset: 0x000024E8
		IEnumerable<ContainerMailboxInformation> IMailboxReplicationProxyService.GetMailboxContainerMailboxes(Guid mdbGuid, Guid primaryMailboxGuid)
		{
			MrsTracer.ProxyService.Function("MRSProxy.GetMailboxContainerMailboxes({0}, {1})", new object[]
			{
				mdbGuid,
				primaryMailboxGuid
			});
			List<ContainerMailboxInformation> containerMailboxes = null;
			this.ExecuteServiceCall<object>(-1L, ExecutionFlags.ThrottlingNotRequired | ExecutionFlags.NoLock, DelayScopeKind.NoDelay, delegate(object o)
			{
				using (ExRpcAdmin exRpcAdmin = ExRpcAdmin.Create("Client=MSExchangeMigration", null, null, null, null))
				{
					Guid[] array = null;
					PropValue[][] mailboxTableInfo = exRpcAdmin.GetMailboxTableInfo(mdbGuid, primaryMailboxGuid, new PropTag[]
					{
						PropTag.MailboxPartitionMailboxGuids
					});
					if (mailboxTableInfo != null)
					{
						foreach (PropValue[] array3 in mailboxTableInfo)
						{
							if (array3 != null)
							{
								array = array3[0].GetGuidArray();
							}
						}
						if (array != null)
						{
							containerMailboxes = new List<ContainerMailboxInformation>();
							foreach (Guid guid in array)
							{
								byte[] persistableTenantPartitionHint = null;
								mailboxTableInfo = exRpcAdmin.GetMailboxTableInfo(mdbGuid, guid, new PropTag[]
								{
									PropTag.PersistableTenantPartitionHint
								});
								if (mailboxTableInfo != null)
								{
									foreach (PropValue[] array6 in mailboxTableInfo)
									{
										if (array6 != null)
										{
											persistableTenantPartitionHint = array6[0].GetBytes();
										}
									}
								}
								containerMailboxes.Add(new ContainerMailboxInformation
								{
									MailboxGuid = guid,
									PersistableTenantPartitionHint = persistableTenantPartitionHint
								});
							}
						}
					}
				}
			});
			return containerMailboxes;
		}

		// Token: 0x06000063 RID: 99 RVA: 0x00004360 File Offset: 0x00002560
		bool IMailboxReplicationProxyService.ArePublicFoldersReadyForMigrationCompletion()
		{
			if (!VariantConfiguration.GetSnapshot(MachineSettingsContext.Local, null, null).Mrs.PublicFolderMailboxesMigration.Enabled)
			{
				throw new NotImplementedException();
			}
			IConfigurationSession tenantOrTopologyConfigurationSession = DirectorySessionFactory.NonCacheSessionFactory.GetTenantOrTopologyConfigurationSession(ConsistencyMode.FullyConsistent, ADSessionSettings.FromOrganizationIdWithoutRbacScopesServiceOnly(OrganizationId.ForestWideOrgId), 469, "ArePublicFoldersReadyForMigrationCompletion", "f:\\15.00.1497\\sources\\dev\\mrs\\src\\ProxyService\\MRSProxyService.cs");
			Organization orgContainer = tenantOrTopologyConfigurationSession.GetOrgContainer();
			if (!orgContainer.Heuristics.HasFlag(HeuristicsFlags.PublicFolderMailboxesLockedForNewConnections))
			{
				return false;
			}
			List<MoveRequest> moveRequests = CommonUtils.GetMoveRequests();
			if (moveRequests != null && moveRequests.Any<MoveRequest>())
			{
				foreach (MoveRequest moveRequest in moveRequests)
				{
					if (moveRequest.RecipientTypeDetails == RecipientTypeDetails.PublicFolderMailbox)
					{
						return false;
					}
				}
			}
			List<PublicFolderMoveRequest> publicFolderMoveRequests = CommonUtils.GetPublicFolderMoveRequests();
			return publicFolderMoveRequests == null || !publicFolderMoveRequests.Any<PublicFolderMoveRequest>();
		}

		// Token: 0x06000064 RID: 100 RVA: 0x0000445C File Offset: 0x0000265C
		List<Guid> IMailboxReplicationProxyService.GetPublicFolderMailboxesExchangeGuids()
		{
			if (!VariantConfiguration.GetSnapshot(MachineSettingsContext.Local, null, null).Mrs.PublicFolderMailboxesMigration.Enabled)
			{
				throw new NotImplementedException();
			}
			List<Mailbox> publicFolderMailboxes = CommonUtils.GetPublicFolderMailboxes();
			List<Guid> list = new List<Guid>();
			if (publicFolderMailboxes != null && publicFolderMailboxes.Any<Mailbox>())
			{
				foreach (Mailbox mailbox in publicFolderMailboxes)
				{
					list.Add(mailbox.ExchangeGuid);
				}
			}
			return list;
		}

		// Token: 0x06000065 RID: 101 RVA: 0x0000454C File Offset: 0x0000274C
		Guid IMailboxReplicationProxyService.IReservationManager_ReserveResources(Guid mailboxGuid, byte[] partitionHintBytes, Guid mdbGuid, int flags)
		{
			MrsTracer.ProxyService.Function("MRSProxy.IReservationManager_ReserveResources({0}, {1}, {2})", new object[]
			{
				mailboxGuid,
				mdbGuid,
				flags
			});
			ReservationBase reservation = null;
			this.ExecuteServiceCall<object>(-1L, ExecutionFlags.ThrottlingNotRequired, DelayScopeKind.CPUOnly, delegate(object o)
			{
				TenantPartitionHint partitionHint = (partitionHintBytes != null) ? TenantPartitionHint.FromPersistablePartitionHint(partitionHintBytes) : null;
				reservation = ReservationManager.CreateReservation(mailboxGuid, partitionHint, mdbGuid, (ReservationFlags)flags, this.ClientVersion.ComputerName);
			});
			return reservation.ReservationId;
		}

		// Token: 0x06000066 RID: 102 RVA: 0x0000462C File Offset: 0x0000282C
		void IMailboxReplicationProxyService.IReservationManager_ReleaseResources(Guid reservationId)
		{
			this.ExecuteServiceCall<object>(-1L, ExecutionFlags.ThrottlingNotRequired, DelayScopeKind.CPUOnly, delegate(object o)
			{
				try
				{
					ReservationBase reservationBase = ReservationManager.FindReservation(reservationId);
					if (reservationBase != null)
					{
						reservationBase.Dispose();
					}
				}
				catch (ExpiredReservationException)
				{
				}
			});
		}

		// Token: 0x06000067 RID: 103 RVA: 0x0000465C File Offset: 0x0000285C
		long IMailboxReplicationProxyService.IMailbox_Config3(Guid primaryMailboxGuid, Guid physicalMailboxGuid, Guid mdbGuid, string mdbName, MailboxType mbxType, int proxyControlFlags)
		{
			MrsTracer.ProxyService.Function("MRSProxy.IMailbox_Config3({0})", new object[]
			{
				primaryMailboxGuid
			});
			LocalMailboxFlags localMailboxFlags = LocalMailboxFlags.None;
			if (mbxType == MailboxType.SourceMailbox && (proxyControlFlags & 8) != 0)
			{
				localMailboxFlags |= LocalMailboxFlags.StripLargeRulesForDownlevelTargets;
				proxyControlFlags &= -9;
			}
			return ((IMailboxReplicationProxyService)this).IMailbox_Config4(primaryMailboxGuid, physicalMailboxGuid, null, mdbGuid, mdbName, mbxType, proxyControlFlags, (int)localMailboxFlags);
		}

		// Token: 0x06000068 RID: 104 RVA: 0x000046B4 File Offset: 0x000028B4
		long IMailboxReplicationProxyService.IMailbox_Config4(Guid primaryMailboxGuid, Guid physicalMailboxGuid, byte[] partitionHintBytes, Guid mdbGuid, string mdbName, MailboxType mbxType, int proxyControlFlags, int localMbxFlags)
		{
			return ((IMailboxReplicationProxyService)this).IMailbox_Config5(Guid.Empty, primaryMailboxGuid, physicalMailboxGuid, partitionHintBytes, mdbGuid, mdbName, mbxType, proxyControlFlags, localMbxFlags);
		}

		// Token: 0x06000069 RID: 105 RVA: 0x000046DC File Offset: 0x000028DC
		long IMailboxReplicationProxyService.IMailbox_Config5(Guid reservationId, Guid primaryMailboxGuid, Guid physicalMailboxGuid, byte[] partitionHintBytes, Guid mdbGuid, string mdbName, MailboxType mbxType, int proxyControlFlags, int localMbxFlags)
		{
			return ((IMailboxReplicationProxyService)this).IMailbox_Config6(reservationId, primaryMailboxGuid, physicalMailboxGuid, null, partitionHintBytes, mdbGuid, mdbName, mbxType, proxyControlFlags, localMbxFlags);
		}

		// Token: 0x0600006A RID: 106 RVA: 0x00004700 File Offset: 0x00002900
		long IMailboxReplicationProxyService.IMailbox_Config6(Guid reservationId, Guid primaryMailboxGuid, Guid physicalMailboxGuid, string filePath, byte[] partitionHintBytes, Guid mdbGuid, string mdbName, MailboxType mbxType, int proxyControlFlags, int localMbxFlags)
		{
			return this.Config(reservationId, primaryMailboxGuid, physicalMailboxGuid, filePath, partitionHintBytes, mdbGuid, mdbName, mbxType, proxyControlFlags, localMbxFlags, null);
		}

		// Token: 0x0600006B RID: 107 RVA: 0x00004730 File Offset: 0x00002930
		long IMailboxReplicationProxyService.IMailbox_Config7(Guid reservationId, Guid primaryMailboxGuid, Guid physicalMailboxGuid, byte[] partitionHintBytes, Guid mdbGuid, string mdbName, MailboxType mbxType, int proxyControlFlags, int localMbxFlags, Guid? mailboxContainerGuid)
		{
			return this.Config(reservationId, primaryMailboxGuid, physicalMailboxGuid, null, partitionHintBytes, mdbGuid, mdbName, mbxType, proxyControlFlags, localMbxFlags, mailboxContainerGuid);
		}

		// Token: 0x0600006C RID: 108 RVA: 0x00004778 File Offset: 0x00002978
		void IMailboxReplicationProxyService.IMailbox_ConfigureProxyService(ProxyConfiguration configuration)
		{
			this.ExecuteServiceCall<object>(-1L, ExecutionFlags.ThrottlingNotRequired, DelayScopeKind.CPUOnly, delegate(object o)
			{
				this.ExportBufferSizeFromMrsKB = configuration.ExportBufferSizeKB;
			});
		}

		// Token: 0x0600006D RID: 109 RVA: 0x000047D4 File Offset: 0x000029D4
		void IMailboxReplicationProxyService.IMailbox_ConfigADConnection(long mailboxHandle, string domainControllerName, string userName, string userDomain, string userPassword)
		{
			MrsTracer.ProxyService.Function("MRSProxy.IMailbox_ConfigADConnection(domainControllerName={0}, userName={1}, userDomain={2}, pwd))", new object[]
			{
				domainControllerName,
				userName,
				userDomain
			});
			NetworkCredential cred = null;
			if (userName != null || userDomain != null || userPassword != null)
			{
				cred = new NetworkCredential(userName, userDomain, userPassword);
			}
			this.ExecuteServiceCall<IMailbox>(mailboxHandle, ExecutionFlags.ThrottlingNotRequired, DelayScopeKind.CPUOnly, delegate(IMailbox mbx)
			{
				mbx.ConfigADConnection(domainControllerName, domainControllerName, cred);
			});
		}

		// Token: 0x0600006E RID: 110 RVA: 0x0000486C File Offset: 0x00002A6C
		void IMailboxReplicationProxyService.IMailbox_ConfigPst(long mailboxHandle, string filePath, int? contentCodePage)
		{
			MrsTracer.ProxyService.Function("MRSProxy.IMailbox_ConfigPst({0}, {1}, {2})", new object[]
			{
				mailboxHandle,
				filePath,
				contentCodePage
			});
			this.ExecuteServiceCall<IMailbox>(mailboxHandle, ExecutionFlags.ThrottlingNotRequired, DelayScopeKind.CPUOnly, delegate(IMailbox mbx)
			{
				mbx.ConfigPst(filePath, contentCodePage);
			});
		}

		// Token: 0x0600006F RID: 111 RVA: 0x00004920 File Offset: 0x00002B20
		void IMailboxReplicationProxyService.IMailbox_ConfigEas(long mailboxHandle, string password, string address)
		{
			MrsTracer.ProxyService.Function("MRSProxy.IMailbox_ConfigEas({0}, {1})", new object[]
			{
				mailboxHandle,
				address
			});
			this.ExecuteServiceCall<IMailbox>(mailboxHandle, ExecutionFlags.ThrottlingNotRequired, DelayScopeKind.CPUOnly, delegate(IMailbox mbx)
			{
				NetworkCredential userCredential = new NetworkCredential(address, password);
				SmtpAddress smtpAddress = new SmtpAddress(address);
				mbx.ConfigEas(userCredential, smtpAddress, Guid.Empty, null);
			});
		}

		// Token: 0x06000070 RID: 112 RVA: 0x000049C8 File Offset: 0x00002BC8
		void IMailboxReplicationProxyService.IMailbox_ConfigEas2(long mailboxHandle, string password, string address, Guid mailboxGuid, string remoteHostName)
		{
			MrsTracer.ProxyService.Function("MRSProxy.IMailbox_ConfigEas2({0}, {1}, {2}, {3})", new object[]
			{
				mailboxHandle,
				address,
				mailboxGuid,
				remoteHostName
			});
			this.ExecuteServiceCall<IMailbox>(mailboxHandle, ExecutionFlags.ThrottlingNotRequired, DelayScopeKind.CPUOnly, delegate(IMailbox mbx)
			{
				NetworkCredential userCredential = new NetworkCredential(address, password);
				SmtpAddress smtpAddress = new SmtpAddress(address);
				mbx.ConfigEas(userCredential, smtpAddress, mailboxGuid, remoteHostName);
			});
		}

		// Token: 0x06000071 RID: 113 RVA: 0x00004A64 File Offset: 0x00002C64
		void IMailboxReplicationProxyService.IMailbox_ConfigRestore(long mailboxHandle, int restoreFlags)
		{
			MrsTracer.ProxyService.Function("MRSProxy.IMailbox_ConfigRestore({0}, 0x{1:x})", new object[]
			{
				mailboxHandle,
				restoreFlags
			});
			this.ExecuteServiceCall<IMailbox>(mailboxHandle, ExecutionFlags.ThrottlingNotRequired, DelayScopeKind.CPUOnly, delegate(IMailbox mbx)
			{
				mbx.ConfigRestore((MailboxRestoreType)restoreFlags);
			});
		}

		// Token: 0x06000072 RID: 114 RVA: 0x00004AD8 File Offset: 0x00002CD8
		void IMailboxReplicationProxyService.IMailbox_ConfigOlc(long mailboxHandle, OlcMailboxConfiguration config)
		{
			MrsTracer.ProxyService.Function("MRSProxy.IMailbox_ConfigOlc({0}, {1})", new object[]
			{
				mailboxHandle,
				config.ToString()
			});
			this.ExecuteServiceCall<IMailbox>(mailboxHandle, ExecutionFlags.ThrottlingNotRequired, DelayScopeKind.CPUOnly, delegate(IMailbox mbx)
			{
				mbx.ConfigOlc(config);
			});
		}

		// Token: 0x06000073 RID: 115 RVA: 0x00004B35 File Offset: 0x00002D35
		int IMailboxReplicationProxyService.IMailbox_ReserveResources(Guid reservationId, Guid resourceId, int reservationType)
		{
			MrsTracer.ProxyService.Function("MRSProxy.IMailbox_ReserveResources", new object[0]);
			throw new UnexpectedErrorPermanentException(-2147024809);
		}

		// Token: 0x06000074 RID: 116 RVA: 0x00004B78 File Offset: 0x00002D78
		void IMailboxReplicationProxyService.CloseHandle(long handle)
		{
			MrsTracer.ProxyService.Function("MRSProxy.CloseHandle({0})", new object[]
			{
				handle
			});
			this.ExecuteServiceCall<object>(-1L, ExecutionFlags.ThrottlingNotRequired, DelayScopeKind.NoDelay, delegate(object o)
			{
				this.handleCache.ReleaseObject(handle);
			});
		}

		// Token: 0x06000075 RID: 117 RVA: 0x00004BF8 File Offset: 0x00002DF8
		void IMailboxReplicationProxyService.DataExport_CancelExport(long dataExportHandle)
		{
			MrsTracer.ProxyService.Function("MRSProxy.DataExport_CancelExport({0})", new object[]
			{
				dataExportHandle
			});
			this.ExecuteServiceCall<IDataExport>(dataExportHandle, ExecutionFlags.Default, DelayScopeKind.CPUOnly, delegate(IDataExport dataExport)
			{
				dataExport.CancelExport();
				((IMailboxReplicationProxyService)this).CloseHandle(dataExportHandle);
			});
		}

		// Token: 0x06000076 RID: 118 RVA: 0x00004C8C File Offset: 0x00002E8C
		DataExportBatch IMailboxReplicationProxyService.DataExport_ExportData2(long dataExportHandle)
		{
			MrsTracer.ProxyService.Function("MRSProxy.DataExport_ExportData({0})", new object[]
			{
				dataExportHandle
			});
			DataExportBatch result = null;
			this.ExecuteServiceCall<IDataExport>(dataExportHandle, ExecutionFlags.Default, DelayScopeKind.DbRead, delegate(IDataExport dataExport)
			{
				result = dataExport.ExportData();
				if (result.IsLastBatch)
				{
					((IMailboxReplicationProxyService)this).CloseHandle(dataExportHandle);
				}
			});
			return result;
		}

		// Token: 0x06000077 RID: 119 RVA: 0x00004D08 File Offset: 0x00002F08
		void IMailboxReplicationProxyService.IDataImport_Flush(long dataImportHandle)
		{
			MrsTracer.ProxyService.Function("MRSProxy.IDataImport_Flush({0})", new object[]
			{
				dataImportHandle
			});
			this.ExecuteServiceCall<IDataImport>(dataImportHandle, ExecutionFlags.Default, DelayScopeKind.DbWrite, delegate(IDataImport dataImport)
			{
				dataImport.SendMessageAndWaitForReply(FlushMessage.Instance);
			});
		}

		// Token: 0x06000078 RID: 120 RVA: 0x00004D98 File Offset: 0x00002F98
		void IMailboxReplicationProxyService.IDataImport_ImportBuffer(long dataImportHandle, int opcode, byte[] data)
		{
			MrsTracer.ProxyService.Function("MRSProxy.IDataImport_ImportBuffer({0}, opcode={1}, data={2})", new object[]
			{
				dataImportHandle,
				opcode,
				TraceUtils.DumpArray(data)
			});
			this.ExecuteServiceCall<IDataImport>(dataImportHandle, ExecutionFlags.Default, DelayScopeKind.DbWrite, delegate(IDataImport dataImport)
			{
				IDataMessage message = DataMessageSerializer.Deserialize(opcode, data, this.UseCompression);
				dataImport.SendMessage(message);
			});
		}

		// Token: 0x06000079 RID: 121 RVA: 0x00004E11 File Offset: 0x00003011
		long IMailboxReplicationProxyService.IDestinationFolder_GetFxProxy(long folderHandle, out byte[] objectData)
		{
			return ((IMailboxReplicationProxyService)this).IDestinationFolder_GetFxProxy2(folderHandle, 0, out objectData);
		}

		// Token: 0x0600007A RID: 122 RVA: 0x00004E90 File Offset: 0x00003090
		long IMailboxReplicationProxyService.IDestinationFolder_GetFxProxy2(long folderHandle, int flags, out byte[] objectData)
		{
			MrsTracer.ProxyService.Function("MRSProxy.IDestinationFolder_GetFxProxy({0})", new object[]
			{
				folderHandle
			});
			long result = -1L;
			byte[] objData = null;
			this.ExecuteServiceCall<IDestinationFolder>(folderHandle, ExecutionFlags.Default, DelayScopeKind.CPUOnly, delegate(IDestinationFolder folder)
			{
				IFxProxy fxProxy = folder.GetFxProxy((FastTransferFlags)flags);
				IDataImport destination = new FxProxyReceiver(fxProxy, true);
				objData = fxProxy.GetObjectData();
				BufferedReceiver obj = new BufferedReceiver(destination, true, this.UseBufferring, this.UseCompression);
				result = this.handleCache.AddObject(obj, folderHandle);
			});
			objectData = objData;
			return result;
		}

		// Token: 0x0600007B RID: 123 RVA: 0x00004F14 File Offset: 0x00003114
		PropProblemData[] IMailboxReplicationProxyService.IDestinationFolder_SetProps(long folderHandle, PropValueData[] pva)
		{
			MrsTracer.ProxyService.Function("MRSProxy.IDestinationFolder_SetProps({0})", new object[]
			{
				folderHandle
			});
			return ((IMailboxReplicationProxyService)this).IFolder_SetProps(folderHandle, pva);
		}

		// Token: 0x0600007C RID: 124 RVA: 0x00004F74 File Offset: 0x00003174
		bool IMailboxReplicationProxyService.IDestinationFolder_SetSearchCriteria(long folderHandle, RestrictionData restriction, byte[][] entryIDs, int searchFlags)
		{
			MrsTracer.ProxyService.Function("MRSProxy.IDestinationFolder_SetSearchCriteria({0})", new object[]
			{
				folderHandle
			});
			bool result = false;
			this.ExecuteServiceCall<IDestinationFolder>(folderHandle, ExecutionFlags.Default, DelayScopeKind.DbWrite, delegate(IDestinationFolder folder)
			{
				result = folder.SetSearchCriteria(restriction, entryIDs, (SearchCriteriaFlags)searchFlags);
			});
			return result;
		}

		// Token: 0x0600007D RID: 125 RVA: 0x00005024 File Offset: 0x00003224
		PropProblemData[] IMailboxReplicationProxyService.IDestinationFolder_SetSecurityDescriptor(long folderHandle, int secProp, byte[] sdData)
		{
			MrsTracer.ProxyService.Function("MRSProxy.IDestinationFolder_SetSecurityDescriptor({0})", new object[]
			{
				folderHandle
			});
			PropProblemData[] result = null;
			this.ExecuteServiceCall<IDestinationFolder>(folderHandle, ExecutionFlags.Default, DelayScopeKind.DbWrite, delegate(IDestinationFolder folder)
			{
				RawSecurityDescriptor sd;
				if (sdData != null)
				{
					sd = new RawSecurityDescriptor(sdData, 0);
				}
				else
				{
					sd = null;
				}
				result = folder.SetSecurityDescriptor((SecurityProp)secProp, sd);
			});
			return result;
		}

		// Token: 0x0600007E RID: 126 RVA: 0x00005088 File Offset: 0x00003288
		void IMailboxReplicationProxyService.IDestinationFolder_DeleteMessages(long folderHandle, byte[][] entryIds)
		{
			MrsTracer.ProxyService.Function("MRSProxy.IDestinationFolder_DeleteMessages({0}, {1})", new object[]
			{
				folderHandle,
				(entryIds == null) ? 0 : entryIds.Length
			});
			((IMailboxReplicationProxyService)this).IFolder_DeleteMessages(folderHandle, entryIds);
		}

		// Token: 0x0600007F RID: 127 RVA: 0x000050EC File Offset: 0x000032EC
		void IMailboxReplicationProxyService.IDestinationFolder_SetReadFlagsOnMessages(long folderHandle, int flags, byte[][] entryIds)
		{
			MrsTracer.ProxyService.Function("MRSProxy.IDestinationFolder_SetReadFlagsOnMessages({0})", new object[]
			{
				folderHandle
			});
			this.ExecuteServiceCall<IDestinationFolder>(folderHandle, ExecutionFlags.Default, DelayScopeKind.DbWrite, delegate(IDestinationFolder folder)
			{
				folder.SetReadFlagsOnMessages((SetReadFlags)flags, entryIds);
			});
		}

		// Token: 0x06000080 RID: 128 RVA: 0x00005160 File Offset: 0x00003360
		void IMailboxReplicationProxyService.IDestinationFolder_SetMessageProps(long folderHandle, byte[] entryId, PropValueData[] propValues)
		{
			MrsTracer.ProxyService.Function("MRSProxy.IDestinationFolder_SetMessageProps({0})", new object[]
			{
				folderHandle
			});
			this.ExecuteServiceCall<IDestinationFolder>(folderHandle, ExecutionFlags.Default, DelayScopeKind.DbWrite, delegate(IDestinationFolder folder)
			{
				folder.SetMessageProps(entryId, propValues);
			});
		}

		// Token: 0x06000081 RID: 129 RVA: 0x000051B8 File Offset: 0x000033B8
		void IMailboxReplicationProxyService.IDestinationMailbox_CreateFolder(long mailboxHandle, FolderRec sourceFolder, bool failIfExists)
		{
			MrsTracer.ProxyService.Function("MRSProxy.IDestinationMailbox_CreateFolder({0})", new object[]
			{
				mailboxHandle
			});
			byte[] array;
			((IMailboxReplicationProxyService)this).IDestinationMailbox_CreateFolder2(mailboxHandle, sourceFolder, failIfExists, out array);
		}

		// Token: 0x06000082 RID: 130 RVA: 0x000051F0 File Offset: 0x000033F0
		void IMailboxReplicationProxyService.IDestinationMailbox_CreateFolder2(long mailboxHandle, FolderRec sourceFolder, bool failIfExists, out byte[] newFolderId)
		{
			MrsTracer.ProxyService.Function("MRSProxy.IDestinationMailbox_CreateFolder2({0})", new object[]
			{
				mailboxHandle
			});
			CreateFolderFlags createFolderFlags = failIfExists ? CreateFolderFlags.FailIfExists : CreateFolderFlags.None;
			((IMailboxReplicationProxyService)this).IDestinationMailbox_CreateFolder3(mailboxHandle, sourceFolder, (int)createFolderFlags, out newFolderId);
		}

		// Token: 0x06000083 RID: 131 RVA: 0x00005254 File Offset: 0x00003454
		void IMailboxReplicationProxyService.IDestinationMailbox_CreateFolder3(long mailboxHandle, FolderRec sourceFolder, int createFolderFlags, out byte[] newFolderId)
		{
			MrsTracer.ProxyService.Function("MRSProxy.IDestinationMailbox_CreateFolder2({0})", new object[]
			{
				mailboxHandle
			});
			byte[] newFolderIdInt = null;
			this.ExecuteServiceCall<IDestinationMailbox>(mailboxHandle, ExecutionFlags.Default, DelayScopeKind.DbWrite, delegate(IDestinationMailbox mbx)
			{
				mbx.CreateFolder(sourceFolder, (CreateFolderFlags)createFolderFlags, out newFolderIdInt);
			});
			newFolderId = newFolderIdInt;
		}

		// Token: 0x06000084 RID: 132 RVA: 0x000052D0 File Offset: 0x000034D0
		void IMailboxReplicationProxyService.IDestinationFolder_SetRules(long folderHandle, RuleData[] rules)
		{
			MrsTracer.ProxyService.Function("MRSProxy.IDestinationFolder_SetRules({0})", new object[]
			{
				folderHandle
			});
			this.ExecuteServiceCall<IDestinationFolder>(folderHandle, ExecutionFlags.Default, DelayScopeKind.DbWrite, delegate(IDestinationFolder folder)
			{
				folder.SetRules(rules);
			});
		}

		// Token: 0x06000085 RID: 133 RVA: 0x0000533C File Offset: 0x0000353C
		void IMailboxReplicationProxyService.IDestinationFolder_SetACL(long folderHandle, int secProp, PropValueData[][] aclData)
		{
			MrsTracer.ProxyService.Function("MRSProxy.IDestinationFolder_SetACL({0})", new object[]
			{
				folderHandle
			});
			this.ExecuteServiceCall<IDestinationFolder>(folderHandle, ExecutionFlags.Default, DelayScopeKind.DbWrite, delegate(IDestinationFolder folder)
			{
				folder.SetACL((SecurityProp)secProp, aclData);
			});
		}

		// Token: 0x06000086 RID: 134 RVA: 0x000053B0 File Offset: 0x000035B0
		void IMailboxReplicationProxyService.IDestinationFolder_SetExtendedAcl(long folderHandle, int aclFlags, PropValueData[][] aclData)
		{
			MrsTracer.ProxyService.Function("MRSProxy.IDestinationFolder_SetExtendedAcl({0})", new object[]
			{
				folderHandle
			});
			this.ExecuteServiceCall<IDestinationFolder>(folderHandle, ExecutionFlags.Default, DelayScopeKind.DbWrite, delegate(IDestinationFolder folder)
			{
				folder.SetExtendedAcl((AclFlags)aclFlags, aclData);
			});
		}

		// Token: 0x06000087 RID: 135 RVA: 0x00005428 File Offset: 0x00003628
		Guid IMailboxReplicationProxyService.IDestinationFolder_LinkMailPublicFolder(long folderHandle, LinkMailPublicFolderFlags flags, byte[] objectId)
		{
			MrsTracer.ProxyService.Function("MRSProxy.IDestinationFolder_SetReadFlagsOnMessages({0})", new object[]
			{
				folderHandle
			});
			Guid objectGuid = Guid.Empty;
			this.ExecuteServiceCall<IDestinationFolder>(folderHandle, ExecutionFlags.Default, DelayScopeKind.CPUOnly, delegate(IDestinationFolder folder)
			{
				objectGuid = folder.LinkMailPublicFolder(flags, objectId);
			});
			return objectGuid;
		}

		// Token: 0x06000088 RID: 136 RVA: 0x000054AC File Offset: 0x000036AC
		CreateMailboxResult IMailboxReplicationProxyService.IDestinationMailbox_CreateMailbox(long mailboxHandle, byte[] mailboxData)
		{
			MrsTracer.ProxyService.Function("MRSProxy.IDestinationMailbox_CreateMailbox({0})", new object[]
			{
				mailboxHandle
			});
			CreateMailboxResult result = CreateMailboxResult.Success;
			this.ExecuteServiceCall<IDestinationMailbox>(mailboxHandle, ExecutionFlags.Default, DelayScopeKind.DbWrite, delegate(IDestinationMailbox mbx)
			{
				result = mbx.CreateMailbox(mailboxData, MailboxSignatureFlags.GetLegacy);
			});
			return result;
		}

		// Token: 0x06000089 RID: 137 RVA: 0x0000552C File Offset: 0x0000372C
		CreateMailboxResult IMailboxReplicationProxyService.IDestinationMailbox_CreateMailbox2(long mailboxHandle, byte[] mailboxData, int sourceSignatureFlags)
		{
			MrsTracer.ProxyService.Function("MRSProxy.IDestinationMailbox_CreateMailbox2({0})", new object[]
			{
				mailboxHandle
			});
			CreateMailboxResult result = CreateMailboxResult.Success;
			this.ExecuteServiceCall<IDestinationMailbox>(mailboxHandle, ExecutionFlags.Default, DelayScopeKind.DbWrite, delegate(IDestinationMailbox mbx)
			{
				result = mbx.CreateMailbox(mailboxData, (MailboxSignatureFlags)sourceSignatureFlags);
			});
			return result;
		}

		// Token: 0x0600008A RID: 138 RVA: 0x000055A8 File Offset: 0x000037A8
		void IMailboxReplicationProxyService.IDestinationMailbox_ProcessMailboxSignature(long mailboxHandle, byte[] mailboxData)
		{
			MrsTracer.ProxyService.Function("MRSProxy.IDestinationMailbox_ProcessMailboxSignature({0})", new object[]
			{
				mailboxHandle
			});
			this.ExecuteServiceCall<IDestinationMailbox>(mailboxHandle, ExecutionFlags.Default, DelayScopeKind.DbWrite, delegate(IDestinationMailbox mbx)
			{
				mbx.ProcessMailboxSignature(mailboxData);
			});
		}

		// Token: 0x0600008B RID: 139 RVA: 0x00005610 File Offset: 0x00003810
		void IMailboxReplicationProxyService.IDestinationMailbox_DeleteFolder(long mailboxHandle, FolderRec folderRec)
		{
			MrsTracer.ProxyService.Function("MRSProxy.IDestinationMailbox_DeleteFolder({0})", new object[]
			{
				mailboxHandle
			});
			this.ExecuteServiceCall<IDestinationMailbox>(mailboxHandle, ExecutionFlags.Default, DelayScopeKind.DbWrite, delegate(IDestinationMailbox mbx)
			{
				mbx.DeleteFolder(folderRec);
			});
		}

		// Token: 0x0600008C RID: 140 RVA: 0x000056AC File Offset: 0x000038AC
		long IMailboxReplicationProxyService.IDestinationMailbox_GetFolder(long mailboxHandle, byte[] entryId)
		{
			MrsTracer.ProxyService.Function("MRSProxy.IDestinationMailbox_GetFolder({0})", new object[]
			{
				mailboxHandle
			});
			long result = -1L;
			this.ExecuteServiceCall<IDestinationMailbox>(mailboxHandle, ExecutionFlags.Default, DelayScopeKind.DbRead, delegate(IDestinationMailbox mbx)
			{
				IDestinationFolder folder = mbx.GetFolder(entryId);
				if (folder != null)
				{
					result = this.handleCache.AddObject(folder, mailboxHandle);
					return;
				}
				result = 0L;
			});
			return result;
		}

		// Token: 0x0600008D RID: 141 RVA: 0x00005790 File Offset: 0x00003990
		long IMailboxReplicationProxyService.IDestinationMailbox_GetFxProxy(long mailboxHandle, out byte[] objectData)
		{
			MrsTracer.ProxyService.Function("MRSProxy.IDestinationMailbox_GetFxProxy({0})", new object[]
			{
				mailboxHandle
			});
			long result = -1L;
			byte[] objData = null;
			this.ExecuteServiceCall<IDestinationMailbox>(mailboxHandle, ExecutionFlags.Default, DelayScopeKind.DbWrite, delegate(IDestinationMailbox mbx)
			{
				IFxProxy fxProxy = mbx.GetFxProxy();
				IDataImport destination = new FxProxyReceiver(fxProxy, true);
				objData = fxProxy.GetObjectData();
				BufferedReceiver obj = new BufferedReceiver(destination, true, this.UseBufferring, this.UseCompression);
				result = this.handleCache.AddObject(obj, mailboxHandle);
			});
			objectData = objData;
			return result;
		}

		// Token: 0x0600008E RID: 142 RVA: 0x0000589C File Offset: 0x00003A9C
		long IMailboxReplicationProxyService.IDestinationMailbox_GetFxProxyPool(long mailboxHandle, byte[][] folderIds, out byte[] objectData)
		{
			MrsTracer.ProxyService.Function("MRSProxy.IDestinationMailbox_GetFxProxyPool({0})", new object[]
			{
				mailboxHandle
			});
			long result = -1L;
			byte[] objData = null;
			this.ExecuteServiceCall<IDestinationMailbox>(mailboxHandle, ExecutionFlags.Default, DelayScopeKind.DbWrite, delegate(IDestinationMailbox mbx)
			{
				IFxProxyPool fxProxyPool = mbx.GetFxProxyPool(folderIds);
				IDataImport dataImport = new FxProxyPoolReceiver(fxProxyPool, true);
				IDataMessage dataMessage = dataImport.SendMessageAndWaitForReply(FxProxyPoolGetFolderDataRequestMessage.Instance);
				DataMessageOpcode dataMessageOpcode;
				dataMessage.Serialize(this.UseCompression, out dataMessageOpcode, out objData);
				BufferedReceiver obj = new BufferedReceiver(dataImport, true, this.UseBufferring, this.UseCompression);
				result = this.handleCache.AddObject(obj, mailboxHandle);
			});
			objectData = objData;
			return result;
		}

		// Token: 0x0600008F RID: 143 RVA: 0x00005990 File Offset: 0x00003B90
		DataExportBatch IMailboxReplicationProxyService.IDestinationMailbox_LoadSyncState2(long mailboxHandle, byte[] key)
		{
			MrsTracer.ProxyService.Function("MRSProxy.IDestinationMailbox_LoadSyncState2({0})", new object[]
			{
				mailboxHandle
			});
			DataExportBatch result = null;
			this.ExecuteServiceCall<IDestinationMailbox>(mailboxHandle, ExecutionFlags.Default, DelayScopeKind.NoDelay, delegate(IDestinationMailbox mbx)
			{
				string data = mbx.LoadSyncState(key);
				IDataExport dataExport = new PagedTransmitter(data, this.UseCompression);
				result = dataExport.ExportData();
				if (!result.IsLastBatch)
				{
					result.DataExportHandle = this.handleCache.AddObject(dataExport, mailboxHandle);
				}
			});
			return result;
		}

		// Token: 0x06000090 RID: 144 RVA: 0x00005AF0 File Offset: 0x00003CF0
		long IMailboxReplicationProxyService.IDestinationMailbox_SaveSyncState2(long mailboxHandle, byte[] key, DataExportBatch firstBatch)
		{
			MrsTracer.ProxyService.Function("MRSProxy.ISourceMailbox_GetMailboxSyncState({0})", new object[]
			{
				mailboxHandle
			});
			long result = -1L;
			this.ExecuteServiceCall<IDestinationMailbox>(mailboxHandle, ExecutionFlags.Default, DelayScopeKind.NoDelay, delegate(IDestinationMailbox mbx)
			{
				IDataImport dataImport = new PagedReceiver(delegate(string str)
				{
					mbx.SaveSyncState(key, str);
				}, this.UseCompression);
				try
				{
					IDataMessage message = DataMessageSerializer.Deserialize(firstBatch.Opcode, firstBatch.Data, this.UseCompression);
					dataImport.SendMessage(message);
					if (!firstBatch.IsLastBatch)
					{
						result = this.handleCache.AddObject(dataImport, mailboxHandle);
						dataImport = null;
					}
					else
					{
						result = 0L;
					}
				}
				finally
				{
					if (dataImport != null)
					{
						dataImport.Dispose();
					}
				}
			});
			return result;
		}

		// Token: 0x06000091 RID: 145 RVA: 0x00005B84 File Offset: 0x00003D84
		bool IMailboxReplicationProxyService.IDestinationMailbox_MailboxExists(long mailboxHandle)
		{
			MrsTracer.ProxyService.Function("MRSProxy.IDestinationMailbox_MailboxExists({0})", new object[]
			{
				mailboxHandle
			});
			bool result = false;
			this.ExecuteServiceCall<IDestinationMailbox>(mailboxHandle, ExecutionFlags.ThrottlingNotRequired, DelayScopeKind.CPUOnly, delegate(IDestinationMailbox mbx)
			{
				result = mbx.MailboxExists();
			});
			return result;
		}

		// Token: 0x06000092 RID: 146 RVA: 0x00005BFC File Offset: 0x00003DFC
		void IMailboxReplicationProxyService.IDestinationMailbox_MoveFolder(long mailboxHandle, byte[] folderId, byte[] oldParentId, byte[] newParentId)
		{
			MrsTracer.ProxyService.Function("MRSProxy.IDestinationMailbox_MoveFolder({0})", new object[]
			{
				mailboxHandle
			});
			this.ExecuteServiceCall<IDestinationMailbox>(mailboxHandle, ExecutionFlags.Default, DelayScopeKind.DbWrite, delegate(IDestinationMailbox mbx)
			{
				mbx.MoveFolder(folderId, oldParentId, newParentId);
			});
		}

		// Token: 0x06000093 RID: 147 RVA: 0x00005C78 File Offset: 0x00003E78
		PropProblemData[] IMailboxReplicationProxyService.IDestinationMailbox_SetProps(long mailboxHandle, PropValueData[] pva)
		{
			MrsTracer.ProxyService.Function("MRSProxy.IDestinationMailbox_SetProps({0})", new object[]
			{
				mailboxHandle
			});
			PropProblemData[] result = null;
			this.ExecuteServiceCall<IDestinationMailbox>(mailboxHandle, ExecutionFlags.Default, DelayScopeKind.DbWrite, delegate(IDestinationMailbox mbx)
			{
				result = mbx.SetProps(pva);
			});
			return result;
		}

		// Token: 0x06000094 RID: 148 RVA: 0x00005D08 File Offset: 0x00003F08
		void IMailboxReplicationProxyService.IDestinationMailbox_SetMailboxSecurityDescriptor(long mailboxHandle, byte[] sdData)
		{
			MrsTracer.ProxyService.Function("MRSProxy.IDestinationMailbox_SetMailboxSecurityDescriptor({0})", new object[]
			{
				mailboxHandle
			});
			this.ExecuteServiceCall<IDestinationMailbox>(mailboxHandle, ExecutionFlags.Default, DelayScopeKind.DbWrite, delegate(IDestinationMailbox mbx)
			{
				if (sdData != null)
				{
					RawSecurityDescriptor mailboxSecurityDescriptor = new RawSecurityDescriptor(sdData, 0);
					mbx.SetMailboxSecurityDescriptor(mailboxSecurityDescriptor);
				}
			});
		}

		// Token: 0x06000095 RID: 149 RVA: 0x00005D8C File Offset: 0x00003F8C
		void IMailboxReplicationProxyService.IDestinationMailbox_SetUserSecurityDescriptor(long mailboxHandle, byte[] sdData)
		{
			MrsTracer.ProxyService.Function("MRSProxy.IDestinationMailbox_SetUserSecurityDescriptor({0})", new object[]
			{
				mailboxHandle
			});
			this.ExecuteServiceCall<IDestinationMailbox>(mailboxHandle, ExecutionFlags.Default, DelayScopeKind.CPUOnly, delegate(IDestinationMailbox mbx)
			{
				if (sdData != null)
				{
					RawSecurityDescriptor userSecurityDescriptor = new RawSecurityDescriptor(sdData, 0);
					mbx.SetUserSecurityDescriptor(userSecurityDescriptor);
				}
			});
		}

		// Token: 0x06000096 RID: 150 RVA: 0x00005E18 File Offset: 0x00004018
		List<MessageRec> IMailboxReplicationProxyService.IFolder_EnumerateMessagesPaged2(long folderHandle, EnumerateMessagesFlags emFlags, int[] additionalPtagsToLoad, out bool moreData)
		{
			MrsTracer.ProxyService.Function("MRSProxy.IFolder_EnumerateMessagesPaged2({0})", new object[]
			{
				folderHandle
			});
			this.enumerateMessagesRemainingData = null;
			this.enumerateMessagesFolder = -1L;
			this.ExecuteServiceCall<IFolder>(folderHandle, ExecutionFlags.Default, DelayScopeKind.DbRead, delegate(IFolder folder)
			{
				this.enumerateMessagesRemainingData = folder.EnumerateMessages(emFlags, DataConverter<PropTagConverter, PropTag, int>.GetNative(additionalPtagsToLoad));
				this.enumerateMessagesFolder = folderHandle;
			});
			if (this.enumerateMessagesRemainingData != null)
			{
				foreach (MessageRec messageRec in this.enumerateMessagesRemainingData)
				{
					messageRec.FolderId = null;
				}
			}
			return ((IMailboxReplicationProxyService)this).IFolder_EnumerateMessagesNextBatch(folderHandle, out moreData);
		}

		// Token: 0x06000097 RID: 151 RVA: 0x00005F7C File Offset: 0x0000417C
		List<MessageRec> IMailboxReplicationProxyService.IFolder_EnumerateMessagesNextBatch(long folderHandle, out bool moreData)
		{
			MrsTracer.ProxyService.Function("MRSProxy.IFolder_EnumerateMessagesNextBatch({0})", new object[]
			{
				folderHandle
			});
			List<MessageRec> result = null;
			bool moreDataTemp = false;
			this.ExecuteServiceCall<object>(-1L, ExecutionFlags.Default, DelayScopeKind.CPUOnly, delegate(object o)
			{
				if (folderHandle != this.enumerateMessagesFolder)
				{
					MrsTracer.ProxyService.Warning("EnumerateMessagesNextBatch is called on the wrong folder handle", new object[0]);
					moreDataTemp = false;
					result = null;
					return;
				}
				int num = 1000;
				result = CommonUtils.ExtractBatch<MessageRec>(ref this.enumerateMessagesRemainingData, ref num, out moreDataTemp);
				if (!moreDataTemp)
				{
					this.enumerateMessagesFolder = -1L;
				}
			});
			moreData = moreDataTemp;
			return result;
		}

		// Token: 0x06000098 RID: 152 RVA: 0x00005FF4 File Offset: 0x000041F4
		FolderRec IMailboxReplicationProxyService.IFolder_GetFolderRec2(long folderHandle, int[] additionalPtagsToLoad)
		{
			MrsTracer.ProxyService.Function("MRSProxy.IFolder_GetFolderRec2({0})", new object[]
			{
				folderHandle
			});
			return ((IMailboxReplicationProxyService)this).IFolder_GetFolderRec3(folderHandle, additionalPtagsToLoad, 0);
		}

		// Token: 0x06000099 RID: 153 RVA: 0x00006054 File Offset: 0x00004254
		FolderRec IMailboxReplicationProxyService.IFolder_GetFolderRec3(long folderHandle, int[] additionalPtagsToLoad, int flags)
		{
			MrsTracer.ProxyService.Function("MRSProxy.IFolder_GetFolderRec({0})", new object[]
			{
				folderHandle
			});
			FolderRec result = null;
			this.ExecuteServiceCall<IFolder>(folderHandle, ExecutionFlags.Default, DelayScopeKind.DbRead, delegate(IFolder folder)
			{
				result = folder.GetFolderRec(DataConverter<PropTagConverter, PropTag, int>.GetNative(additionalPtagsToLoad), (GetFolderRecFlags)flags);
			});
			return result;
		}

		// Token: 0x0600009A RID: 154 RVA: 0x000060FC File Offset: 0x000042FC
		byte[] IMailboxReplicationProxyService.IFolder_GetSecurityDescriptor(long folderHandle, int secProp)
		{
			MrsTracer.ProxyService.Function("MRSProxy.IFolder_GetSecurityDescriptor({0})", new object[]
			{
				folderHandle
			});
			byte[] result = null;
			this.ExecuteServiceCall<IFolder>(folderHandle, ExecutionFlags.Default, DelayScopeKind.DbRead, delegate(IFolder folder)
			{
				RawSecurityDescriptor securityDescriptor = folder.GetSecurityDescriptor((SecurityProp)secProp);
				byte[] array;
				if (securityDescriptor != null)
				{
					array = new byte[securityDescriptor.BinaryLength];
					securityDescriptor.GetBinaryForm(array, 0);
				}
				else
				{
					array = null;
				}
				result = array;
			});
			return result;
		}

		// Token: 0x0600009B RID: 155 RVA: 0x00006170 File Offset: 0x00004370
		void IMailboxReplicationProxyService.IFolder_SetContentsRestriction(long folderHandle, RestrictionData restriction)
		{
			MrsTracer.ProxyService.Function("MRSProxy.IFolder_SetContentsRestriction({0})", new object[]
			{
				folderHandle
			});
			this.ExecuteServiceCall<IFolder>(folderHandle, ExecutionFlags.Default, DelayScopeKind.DbRead, delegate(IFolder folder)
			{
				folder.SetContentsRestriction(restriction);
			});
		}

		// Token: 0x0600009C RID: 156 RVA: 0x00006208 File Offset: 0x00004408
		PropValueData[] IMailboxReplicationProxyService.IFolder_GetProps(long folderHandle, int[] pta)
		{
			MrsTracer.ProxyService.Function("MRSProxy.IFolder_GetProps({0})", new object[]
			{
				folderHandle
			});
			PropValueData[] result = null;
			this.ExecuteServiceCall<IFolder>(folderHandle, ExecutionFlags.Default, DelayScopeKind.DbRead, delegate(IFolder folder)
			{
				result = folder.GetProps(DataConverter<PropTagConverter, PropTag, int>.GetNative(pta));
				DiagnosticContext currentContext = DiagnosticContext.GetCurrentContext();
				if (currentContext != null)
				{
					this.mapiDiagnosticGetProp = currentContext.ToString();
				}
			});
			return result;
		}

		// Token: 0x0600009D RID: 157 RVA: 0x00006288 File Offset: 0x00004488
		PropProblemData[] IMailboxReplicationProxyService.IFolder_SetProps(long folderHandle, PropValueData[] pva)
		{
			MrsTracer.ProxyService.Function("MRSProxy.IFolder_SetProps({0})", new object[]
			{
				folderHandle
			});
			PropProblemData[] result = null;
			this.ExecuteServiceCall<IFolder>(folderHandle, ExecutionFlags.Default, DelayScopeKind.DbWrite, delegate(IFolder folder)
			{
				result = folder.SetProps(pva);
			});
			return result;
		}

		// Token: 0x0600009E RID: 158 RVA: 0x000062FC File Offset: 0x000044FC
		void IMailboxReplicationProxyService.IFolder_DeleteMessages(long folderHandle, byte[][] entryIds)
		{
			MrsTracer.ProxyService.Function("MRSProxy.IFolder_DeleteMessages({0})", new object[]
			{
				folderHandle
			});
			this.ExecuteServiceCall<IFolder>(folderHandle, ExecutionFlags.Default, DelayScopeKind.DbWrite, delegate(IFolder folder)
			{
				folder.DeleteMessages(entryIds);
			});
		}

		// Token: 0x0600009F RID: 159 RVA: 0x0000638C File Offset: 0x0000458C
		RuleData[] IMailboxReplicationProxyService.IFolder_GetRules(long folderHandle, int[] extraProps)
		{
			MrsTracer.ProxyService.Function("MRSProxy.IFolder_GetRules({0})", new object[]
			{
				folderHandle
			});
			RuleData[] result = null;
			this.ExecuteServiceCall<IFolder>(folderHandle, ExecutionFlags.Default, DelayScopeKind.DbRead, delegate(IFolder folder)
			{
				result = folder.GetRules(DataConverter<PropTagConverter, PropTag, int>.GetNative(extraProps));
				if (!this.ClientVersion[32])
				{
					RuleData.ConvertRulesToDownlevel(result);
				}
			});
			return result;
		}

		// Token: 0x060000A0 RID: 160 RVA: 0x0000640C File Offset: 0x0000460C
		PropValueData[][] IMailboxReplicationProxyService.IFolder_GetACL(long folderHandle, int secProp)
		{
			MrsTracer.ProxyService.Function("MRSProxy.IFolder_GetACL({0})", new object[]
			{
				folderHandle
			});
			PropValueData[][] result = null;
			this.ExecuteServiceCall<IFolder>(folderHandle, ExecutionFlags.Default, DelayScopeKind.DbRead, delegate(IFolder folder)
			{
				result = folder.GetACL((SecurityProp)secProp);
			});
			return result;
		}

		// Token: 0x060000A1 RID: 161 RVA: 0x00006484 File Offset: 0x00004684
		PropValueData[][] IMailboxReplicationProxyService.IFolder_GetExtendedAcl(long folderHandle, int aclFlags)
		{
			MrsTracer.ProxyService.Function("MRSProxy.IFolder_GetExtendedAcl({0})", new object[]
			{
				folderHandle
			});
			PropValueData[][] result = null;
			this.ExecuteServiceCall<IFolder>(folderHandle, ExecutionFlags.Default, DelayScopeKind.DbRead, delegate(IFolder folder)
			{
				result = folder.GetExtendedAcl((AclFlags)aclFlags);
			});
			return result;
		}

		// Token: 0x060000A2 RID: 162 RVA: 0x00006504 File Offset: 0x00004704
		void IMailboxReplicationProxyService.IFolder_GetSearchCriteria(long folderHandle, out RestrictionData restriction, out byte[][] entryIDs, out int searchState)
		{
			MrsTracer.ProxyService.Function("MRSProxy.IFolder_GetSearchCriteria({0})", new object[]
			{
				folderHandle
			});
			RestrictionData rd = null;
			byte[][] eids = null;
			SearchState ss = SearchState.None;
			this.ExecuteServiceCall<IFolder>(folderHandle, ExecutionFlags.Default, DelayScopeKind.DbRead, delegate(IFolder folder)
			{
				folder.GetSearchCriteria(out rd, out eids, out ss);
			});
			restriction = rd;
			entryIDs = eids;
			searchState = (int)ss;
		}

		// Token: 0x060000A3 RID: 163 RVA: 0x000065B4 File Offset: 0x000047B4
		List<MessageRec> IMailboxReplicationProxyService.IFolder_LookupMessages(long folderHandle, int ptagToLookup, byte[][] keysToLookup, int[] additionalPtagsToLoad)
		{
			MrsTracer.ProxyService.Function("MRSProxy.IFolder_LookupMessages({0})", new object[]
			{
				folderHandle
			});
			List<MessageRec> result = null;
			this.ExecuteServiceCall<IFolder>(folderHandle, ExecutionFlags.Default, DelayScopeKind.DbRead, delegate(IFolder folder)
			{
				result = folder.LookupMessages(DataConverter<PropTagConverter, PropTag, int>.GetNative(ptagToLookup), new List<byte[]>(keysToLookup), DataConverter<PropTagConverter, PropTag, int>.GetNative(additionalPtagsToLoad));
			});
			return result;
		}

		// Token: 0x060000A4 RID: 164 RVA: 0x00006620 File Offset: 0x00004820
		void IMailboxReplicationProxyService.IMailbox_Connect(long mailboxHandle)
		{
			MrsTracer.ProxyService.Function("MRSProxy.IMailbox_Connect({0})", new object[]
			{
				mailboxHandle
			});
			((IMailboxReplicationProxyService)this).IMailbox_Connect2(mailboxHandle, 0);
		}

		// Token: 0x060000A5 RID: 165 RVA: 0x0000666C File Offset: 0x0000486C
		void IMailboxReplicationProxyService.IMailbox_Connect2(long mailboxHandle, int connectFlags)
		{
			MrsTracer.ProxyService.Function("MRSProxy.IMailbox_Connect({0})", new object[]
			{
				mailboxHandle
			});
			this.IncrementConnectionCount();
			this.IsHighPriority = ((connectFlags & 16) != 0);
			this.ExecuteServiceCall<IMailbox>(mailboxHandle, ExecutionFlags.ThrottlingNotRequired, DelayScopeKind.CPUOnly, delegate(IMailbox mbx)
			{
				mbx.Connect((MailboxConnectFlags)connectFlags);
			});
		}

		// Token: 0x060000A6 RID: 166 RVA: 0x000066EC File Offset: 0x000048EC
		void IMailboxReplicationProxyService.IMailbox_DeleteMailbox(long mailboxHandle, int flags)
		{
			MrsTracer.ProxyService.Function("MRSProxy.IMailbox_DeleteMailbox({0})", new object[]
			{
				mailboxHandle
			});
			this.ExecuteServiceCall<IMailbox>(mailboxHandle, ExecutionFlags.Default, DelayScopeKind.DbWrite, delegate(IMailbox mbx)
			{
				mbx.DeleteMailbox(flags);
			});
		}

		// Token: 0x060000A7 RID: 167 RVA: 0x00006758 File Offset: 0x00004958
		void IMailboxReplicationProxyService.IMailbox_Disconnect(long mailboxHandle)
		{
			MrsTracer.ProxyService.Function("MRSProxy.IMailbox_Disconnect({0})", new object[]
			{
				mailboxHandle
			});
			this.ExecuteServiceCall<IMailbox>(mailboxHandle, ExecutionFlags.ThrottlingNotRequired, DelayScopeKind.NoDelay, delegate(IMailbox mbx)
			{
				this.DisconnectMailboxSession(mailboxHandle);
			});
			this.DecrementConnectionCount();
		}

		// Token: 0x060000A8 RID: 168 RVA: 0x000067D4 File Offset: 0x000049D4
		void IMailboxReplicationProxyService.IMailbox_ConfigMailboxOptions(long mailboxHandle, int options)
		{
			MrsTracer.ProxyService.Function("MRSProxy.IMailbox_ConfigMailboxOptions({0}, {1})", new object[]
			{
				mailboxHandle,
				options
			});
			this.IsInFinalization = ((MailboxOptions)options).HasFlag(MailboxOptions.Finalize);
			this.ExecuteServiceCall<IMailbox>(mailboxHandle, ExecutionFlags.ThrottlingNotRequired, DelayScopeKind.CPUOnly, delegate(IMailbox mbx)
			{
				mbx.ConfigMailboxOptions((MailboxOptions)options);
			});
		}

		// Token: 0x060000A9 RID: 169 RVA: 0x00006864 File Offset: 0x00004A64
		void IMailboxReplicationProxyService.IMailbox_ConfigPreferredADConnection(long mailboxHandle, string preferredDomainControllerName)
		{
			MrsTracer.ProxyService.Function("MRSProxy.IMailbox_ConfigPreferredADConnection(preferredDomainControllerName={0})", new object[]
			{
				preferredDomainControllerName
			});
			this.ExecuteServiceCall<IMailbox>(mailboxHandle, ExecutionFlags.ThrottlingNotRequired, DelayScopeKind.CPUOnly, delegate(IMailbox mbx)
			{
				mbx.ConfigPreferredADConnection(preferredDomainControllerName);
			});
		}

		// Token: 0x060000AA RID: 170 RVA: 0x00006944 File Offset: 0x00004B44
		MailboxServerInformation IMailboxReplicationProxyService.IMailbox_GetMailboxServerInformation(long mailboxHandle)
		{
			MrsTracer.ProxyService.Function("MRSProxy.IMailbox_GetMailboxServerInformation({0})", new object[]
			{
				mailboxHandle
			});
			MailboxServerInformation result = null;
			this.ExecuteServiceCall<IMailbox>(mailboxHandle, ExecutionFlags.ThrottlingNotRequired, DelayScopeKind.CPUOnly, delegate(IMailbox mbx)
			{
				result = mbx.GetMailboxServerInformation();
				if (result == null)
				{
					result = new MailboxServerInformation();
					Server server = LocalServer.GetServer();
					result.MailboxServerName = server.Name;
					result.MailboxServerVersion = server.VersionNumber;
					result.MailboxServerGuid = server.Guid;
				}
				result.ProxyServerName = CommonUtils.LocalComputerName;
				result.ProxyServerVersion = VersionInformation.MRSProxy;
			});
			return result;
		}

		// Token: 0x060000AB RID: 171 RVA: 0x000069B0 File Offset: 0x00004BB0
		void IMailboxReplicationProxyService.IMailbox_SetOtherSideVersion(long mailboxHandle, VersionInformation otherSideInfo)
		{
			MrsTracer.ProxyService.Function("MRSProxy.IMailbox_SetOtherSideMailboxServerInformation({0})", new object[]
			{
				mailboxHandle
			});
			this.ExecuteServiceCall<IMailbox>(mailboxHandle, ExecutionFlags.ThrottlingNotRequired, DelayScopeKind.CPUOnly, delegate(IMailbox mbx)
			{
				mbx.SetOtherSideVersion(otherSideInfo);
			});
		}

		// Token: 0x060000AC RID: 172 RVA: 0x00006A3C File Offset: 0x00004C3C
		List<FolderRec> IMailboxReplicationProxyService.IMailbox_EnumerateFolderHierarchyPaged2(long mailboxHandle, EnumerateFolderHierarchyFlags flags, int[] additionalPtagsToLoad, out bool moreData)
		{
			MrsTracer.ProxyService.Function("MRSProxy.IMailbox_EnumerateFolderHierarchyPaged2({0})", new object[]
			{
				mailboxHandle
			});
			this.enumerateFoldersRemainingData = null;
			this.enumerateFoldersMailbox = -1L;
			this.ExecuteServiceCall<IMailbox>(mailboxHandle, ExecutionFlags.Default, DelayScopeKind.DbRead, delegate(IMailbox mbx)
			{
				this.enumerateFoldersRemainingData = mbx.EnumerateFolderHierarchy(flags, DataConverter<PropTagConverter, PropTag, int>.GetNative(additionalPtagsToLoad));
				this.enumerateFoldersMailbox = mailboxHandle;
			});
			return ((IMailboxReplicationProxyService)this).IMailbox_EnumerateFolderHierarchyNextBatch(mailboxHandle, out moreData);
		}

		// Token: 0x060000AD RID: 173 RVA: 0x00006B3C File Offset: 0x00004D3C
		List<FolderRec> IMailboxReplicationProxyService.IMailbox_EnumerateFolderHierarchyNextBatch(long mailboxHandle, out bool moreData)
		{
			MrsTracer.ProxyService.Function("MRSProxy.IMailbox_EnumerateFolderHierarchyNextBatch({0})", new object[]
			{
				mailboxHandle
			});
			List<FolderRec> result = null;
			bool moreDataInt = false;
			this.ExecuteServiceCall<object>(-1L, ExecutionFlags.Default, DelayScopeKind.CPUOnly, delegate(object o)
			{
				if (mailboxHandle != this.enumerateFoldersMailbox)
				{
					MrsTracer.ProxyService.Warning("EnumerateFolderHierarchyNextBatch is called on the wrong mailbox handle", new object[0]);
					return;
				}
				int num = 100;
				result = CommonUtils.ExtractBatch<FolderRec>(ref this.enumerateFoldersRemainingData, ref num, out moreDataInt);
				if (!moreDataInt)
				{
					this.enumerateFoldersMailbox = -1L;
				}
			});
			moreData = moreDataInt;
			return result;
		}

		// Token: 0x060000AE RID: 174 RVA: 0x00006BD0 File Offset: 0x00004DD0
		List<WellKnownFolder> IMailboxReplicationProxyService.IMailbox_DiscoverWellKnownFolders(long mailboxHandle, int flags)
		{
			MrsTracer.ProxyService.Function("MRSProxy.IMailbox_DiscoverWellKnownFolders({0})", new object[]
			{
				mailboxHandle
			});
			List<WellKnownFolder> result = null;
			this.ExecuteServiceCall<IMailbox>(mailboxHandle, ExecutionFlags.Default, DelayScopeKind.DbRead, delegate(IMailbox mbx)
			{
				result = mbx.DiscoverWellKnownFolders(flags);
			});
			return result;
		}

		// Token: 0x060000AF RID: 175 RVA: 0x00006C2C File Offset: 0x00004E2C
		bool IMailboxReplicationProxyService.IMailbox_IsMailboxCapabilitySupported(long mailboxHandle, MailboxCapabilities capability)
		{
			return ((IMailboxReplicationProxyService)this).IMailbox_IsMailboxCapabilitySupported2(mailboxHandle, (int)capability);
		}

		// Token: 0x060000B0 RID: 176 RVA: 0x00006C54 File Offset: 0x00004E54
		bool IMailboxReplicationProxyService.IMailbox_IsMailboxCapabilitySupported2(long mailboxHandle, int capability)
		{
			bool result = false;
			this.ExecuteServiceCall<IMailbox>(mailboxHandle, ExecutionFlags.Default, DelayScopeKind.CPUOnly, delegate(IMailbox mbx)
			{
				result = mbx.IsMailboxCapabilitySupported((MailboxCapabilities)capability);
			});
			return result;
		}

		// Token: 0x060000B1 RID: 177 RVA: 0x00006CB4 File Offset: 0x00004EB4
		int[] IMailboxReplicationProxyService.IMailbox_GetIDsFromNames(long mailboxHandle, bool createIfNotExists, NamedPropData[] npa)
		{
			MrsTracer.ProxyService.Function("MRSProxy.IMailbox_GetIDsFromNames({0})", new object[]
			{
				mailboxHandle
			});
			PropTag[] propTags = null;
			this.ExecuteServiceCall<IMailbox>(mailboxHandle, ExecutionFlags.Default, DelayScopeKind.DbRead, delegate(IMailbox mbx)
			{
				propTags = mbx.GetIDsFromNames(createIfNotExists, npa);
			});
			if (propTags == null)
			{
				return null;
			}
			int[] array = new int[propTags.Length];
			for (int i = 0; i < propTags.Length; i++)
			{
				array[i] = (int)propTags[i];
			}
			return array;
		}

		// Token: 0x060000B2 RID: 178 RVA: 0x00006D6C File Offset: 0x00004F6C
		NamedPropData[] IMailboxReplicationProxyService.IMailbox_GetNamesFromIDs(long mailboxHandle, int[] pta)
		{
			MrsTracer.ProxyService.Function("MRSProxy.IMailbox_GetNamesFromIDs({0})", new object[]
			{
				mailboxHandle
			});
			NamedPropData[] result = null;
			this.ExecuteServiceCall<IMailbox>(mailboxHandle, ExecutionFlags.Default, DelayScopeKind.DbRead, delegate(IMailbox mbx)
			{
				result = mbx.GetNamesFromIDs(DataConverter<PropTagConverter, PropTag, int>.GetNative(pta));
			});
			return result;
		}

		// Token: 0x060000B3 RID: 179 RVA: 0x00006DE4 File Offset: 0x00004FE4
		byte[] IMailboxReplicationProxyService.IMailbox_GetSessionSpecificEntryId(long mailboxHandle, byte[] entryId)
		{
			MrsTracer.ProxyService.Function("MRSProxy.IMailbox_GetSessionSpecificEntryId({0})", new object[]
			{
				mailboxHandle
			});
			byte[] result = null;
			this.ExecuteServiceCall<IMailbox>(mailboxHandle, ExecutionFlags.Default, DelayScopeKind.CPUOnly, delegate(IMailbox mbx)
			{
				result = mbx.GetSessionSpecificEntryId(entryId);
			});
			return result;
		}

		// Token: 0x060000B4 RID: 180 RVA: 0x00006E9C File Offset: 0x0000509C
		MappedPrincipal[] IMailboxReplicationProxyService.IMailbox_GetPrincipalsFromMailboxGuids(long mailboxHandle, Guid[] mailboxGuids)
		{
			MrsTracer.ProxyService.Function("MRSProxy.IMailbox_GetPrincipalsFromMailboxGuids({0})", new object[]
			{
				mailboxHandle
			});
			MappedPrincipal[] result = null;
			this.ExecuteServiceCall<IMailbox>(mailboxHandle, ExecutionFlags.Default, DelayScopeKind.CPUOnly, delegate(IMailbox mbx)
			{
				MappedPrincipal[] array = new MappedPrincipal[mailboxGuids.Length];
				for (int i = 0; i < mailboxGuids.Length; i++)
				{
					array[i] = new MappedPrincipal(mailboxGuids[i]);
				}
				result = mbx.ResolvePrincipals(array);
			});
			return result;
		}

		// Token: 0x060000B5 RID: 181 RVA: 0x00006F5C File Offset: 0x0000515C
		Guid[] IMailboxReplicationProxyService.IMailbox_GetMailboxGuidsFromPrincipals(long mailboxHandle, MappedPrincipal[] principals)
		{
			MrsTracer.ProxyService.Function("MRSProxy.IMailbox_GetMailboxGuidsFromPrincipals({0})", new object[]
			{
				mailboxHandle
			});
			Guid[] result = null;
			this.ExecuteServiceCall<IMailbox>(mailboxHandle, ExecutionFlags.Default, DelayScopeKind.CPUOnly, delegate(IMailbox mbx)
			{
				MappedPrincipal[] array = mbx.ResolvePrincipals(principals);
				result = new Guid[array.Length];
				for (int i = 0; i < array.Length; i++)
				{
					result[i] = ((array[i] != null) ? array[i].MailboxGuid : Guid.Empty);
				}
			});
			return result;
		}

		// Token: 0x060000B6 RID: 182 RVA: 0x00006FD4 File Offset: 0x000051D4
		MappedPrincipal[] IMailboxReplicationProxyService.IMailbox_ResolvePrincipals(long mailboxHandle, MappedPrincipal[] principals)
		{
			MrsTracer.ProxyService.Function("MRSProxy.IMailbox_ResolvePrincipals({0})", new object[]
			{
				mailboxHandle
			});
			MappedPrincipal[] result = null;
			this.ExecuteServiceCall<IMailbox>(mailboxHandle, ExecutionFlags.Default, DelayScopeKind.CPUOnly, delegate(IMailbox mbx)
			{
				result = mbx.ResolvePrincipals(principals);
			});
			return result;
		}

		// Token: 0x060000B7 RID: 183 RVA: 0x00007070 File Offset: 0x00005270
		MailboxInformation IMailboxReplicationProxyService.IMailbox_GetMailboxInformation(long mailboxHandle)
		{
			MrsTracer.ProxyService.Function("MRSProxy.IMailbox_GetMailboxInformation({0})", new object[]
			{
				mailboxHandle
			});
			MailboxInformation result = null;
			this.ExecuteServiceCall<IMailbox>(mailboxHandle, ExecutionFlags.ThrottlingNotRequired, DelayScopeKind.DbRead, delegate(IMailbox mbx)
			{
				result = mbx.GetMailboxInformation();
				if (result != null)
				{
					this.IsE15OrHigher = (result.ServerVersion >= Server.E15MinVersion);
				}
			});
			return result;
		}

		// Token: 0x060000B8 RID: 184 RVA: 0x0000710C File Offset: 0x0000530C
		byte[] IMailboxReplicationProxyService.IMailbox_GetMailboxSecurityDescriptor(long mailboxHandle)
		{
			MrsTracer.ProxyService.Function("MRSProxy.IMailbox_GetMailboxSecurityDescriptor({0})", new object[]
			{
				mailboxHandle
			});
			byte[] result = null;
			this.ExecuteServiceCall<IMailbox>(mailboxHandle, ExecutionFlags.Default, DelayScopeKind.DbRead, delegate(IMailbox mbx)
			{
				RawSecurityDescriptor mailboxSecurityDescriptor = mbx.GetMailboxSecurityDescriptor();
				byte[] array;
				if (mailboxSecurityDescriptor != null)
				{
					array = new byte[mailboxSecurityDescriptor.BinaryLength];
					mailboxSecurityDescriptor.GetBinaryForm(array, 0);
				}
				else
				{
					array = null;
				}
				result = array;
			});
			return result;
		}

		// Token: 0x060000B9 RID: 185 RVA: 0x000071A4 File Offset: 0x000053A4
		byte[] IMailboxReplicationProxyService.IMailbox_GetUserSecurityDescriptor(long mailboxHandle)
		{
			MrsTracer.ProxyService.Function("MRSProxy.IMailbox_GetUserSecurityDescriptor({0})", new object[]
			{
				mailboxHandle
			});
			byte[] result = null;
			this.ExecuteServiceCall<IMailbox>(mailboxHandle, ExecutionFlags.Default, DelayScopeKind.CPUOnly, delegate(IMailbox mbx)
			{
				RawSecurityDescriptor userSecurityDescriptor = mbx.GetUserSecurityDescriptor();
				byte[] array;
				if (userSecurityDescriptor != null)
				{
					array = new byte[userSecurityDescriptor.BinaryLength];
					userSecurityDescriptor.GetBinaryForm(array, 0);
				}
				else
				{
					array = null;
				}
				result = array;
			});
			return result;
		}

		// Token: 0x060000BA RID: 186 RVA: 0x00007238 File Offset: 0x00005438
		void IMailboxReplicationProxyService.IMailbox_SetInTransitStatus(long mailboxHandle, int status, out bool onlineMoveSupported)
		{
			MrsTracer.ProxyService.Function("MRSProxy.IMailbox_SetInTransitStatus({0})", new object[]
			{
				mailboxHandle
			});
			bool oms = false;
			this.ExecuteServiceCall<IMailbox>(mailboxHandle, ExecutionFlags.Default, DelayScopeKind.DbRead, delegate(IMailbox mbx)
			{
				mbx.SetInTransitStatus((InTransitStatus)status, out oms);
				if (!this.ClientVersion[11] && status == 0)
				{
					mbx.SeedMBICache();
				}
			});
			onlineMoveSupported = oms;
		}

		// Token: 0x060000BB RID: 187 RVA: 0x000072A8 File Offset: 0x000054A8
		void IMailboxReplicationProxyService.IMailbox_SeedMBICache(long mailboxHandle)
		{
			MrsTracer.ProxyService.Function("MRSProxy.IMailbox_SeedMBICache({0})", new object[]
			{
				mailboxHandle
			});
			this.ExecuteServiceCall<IMailbox>(mailboxHandle, ExecutionFlags.Default, DelayScopeKind.DbRead, delegate(IMailbox mbx)
			{
				mbx.SeedMBICache();
			});
		}

		// Token: 0x060000BC RID: 188 RVA: 0x00007318 File Offset: 0x00005518
		bool IMailboxReplicationProxyService.IMailbox_UpdateRemoteHostName(long mailboxHandle, string value)
		{
			MrsTracer.ProxyService.Function("MRSProxy.IMailbox_UpdateRemoteHostName({0}, {1})", new object[]
			{
				mailboxHandle,
				value
			});
			bool result = false;
			this.ExecuteServiceCall<IMailbox>(mailboxHandle, ExecutionFlags.ThrottlingNotRequired, DelayScopeKind.CPUOnly, delegate(IMailbox mbx)
			{
				result = mbx.UpdateRemoteHostName(value);
			});
			return result;
		}

		// Token: 0x060000BD RID: 189 RVA: 0x000073F0 File Offset: 0x000055F0
		string IMailboxReplicationProxyService.IMailbox_GetADUser(long mailboxHandle)
		{
			MrsTracer.ProxyService.Function("MRSProxy.IMailbox_GetADUser({0})", new object[]
			{
				mailboxHandle
			});
			string result = null;
			this.ExecuteServiceCall<IMailbox>(mailboxHandle, ExecutionFlags.ThrottlingNotRequired, DelayScopeKind.CPUOnly, delegate(IMailbox mbx)
			{
				ADUser aduser = mbx.GetADUser();
				if (!this.ClientVersion[9] && aduser.HasSeparatedArchive)
				{
					throw new UnsupportedClientVersionPermanentException(this.ClientVersion.ComputerName, this.ClientVersion.ToString(), "ArchiveSeparation");
				}
				result = ConfigurableObjectXML.Serialize<ADUser>(aduser);
			});
			return result;
		}

		// Token: 0x060000BE RID: 190 RVA: 0x0000744C File Offset: 0x0000564C
		void IMailboxReplicationProxyService.IMailbox_UpdateMovedMailbox(long mailboxHandle, UpdateMovedMailboxOperation op, string remoteRecipientData, string domainController, out string entries)
		{
			((IMailboxReplicationProxyService)this).IMailbox_UpdateMovedMailbox3(mailboxHandle, op, remoteRecipientData, domainController, out entries, Guid.Empty, Guid.Empty, null, 0, 0);
		}

		// Token: 0x060000BF RID: 191 RVA: 0x00007474 File Offset: 0x00005674
		void IMailboxReplicationProxyService.IMailbox_UpdateMovedMailbox2(long mailboxHandle, UpdateMovedMailboxOperation op, string remoteRecipientData, string domainController, out string entries, Guid newDatabaseGuid, Guid newArchiveDatabaseGuid, string archiveDomain, int archiveStatus)
		{
			((IMailboxReplicationProxyService)this).IMailbox_UpdateMovedMailbox3(mailboxHandle, op, remoteRecipientData, domainController, out entries, newDatabaseGuid, newArchiveDatabaseGuid, archiveDomain, archiveStatus, 0);
		}

		// Token: 0x060000C0 RID: 192 RVA: 0x00007498 File Offset: 0x00005698
		void IMailboxReplicationProxyService.IMailbox_UpdateMovedMailbox3(long mailboxHandle, UpdateMovedMailboxOperation op, string remoteRecipientData, string domainController, out string entries, Guid newDatabaseGuid, Guid newArchiveDatabaseGuid, string archiveDomain, int archiveStatus, int updateMovedMailboxFlags)
		{
			((IMailboxReplicationProxyService)this).IMailbox_UpdateMovedMailbox4(mailboxHandle, op, remoteRecipientData, domainController, out entries, newDatabaseGuid, newArchiveDatabaseGuid, archiveDomain, archiveStatus, updateMovedMailboxFlags, null, null);
		}

		// Token: 0x060000C1 RID: 193 RVA: 0x00007540 File Offset: 0x00005740
		void IMailboxReplicationProxyService.IMailbox_UpdateMovedMailbox4(long mailboxHandle, UpdateMovedMailboxOperation op, string remoteRecipientData, string domainController, out string entries, Guid newDatabaseGuid, Guid newArchiveDatabaseGuid, string archiveDomain, int archiveStatus, int updateMovedMailboxFlags, Guid? newMailboxContainerGuid, byte[] newUnifiedMailboxIdData)
		{
			MrsTracer.ProxyService.Function("MRSProxy.IMailbox_UpdateMovedMailbox4({0}, {1}, {2}, {3}, {4}, {5}, {6})", new object[]
			{
				mailboxHandle,
				op,
				remoteRecipientData,
				domainController,
				updateMovedMailboxFlags,
				newMailboxContainerGuid,
				TraceUtils.DumpBytes(newUnifiedMailboxIdData)
			});
			entries = null;
			ReportEntry[] entryArray = null;
			try
			{
				this.ExecuteServiceCall<IMailbox>(mailboxHandle, ExecutionFlags.Default, DelayScopeKind.CPUOnly, delegate(IMailbox mbx)
				{
					ADUser remoteRecipientData2 = ConfigurableObjectXML.Deserialize<ADUser>(remoteRecipientData);
					CrossTenantObjectId newUnifiedMailboxId = (newUnifiedMailboxIdData == null) ? null : CrossTenantObjectId.Parse(newUnifiedMailboxIdData, true);
					mbx.UpdateMovedMailbox(op, remoteRecipientData2, domainController, out entryArray, newDatabaseGuid, newArchiveDatabaseGuid, archiveDomain, (ArchiveStatusFlags)archiveStatus, (UpdateMovedMailboxFlags)updateMovedMailboxFlags, newMailboxContainerGuid, newUnifiedMailboxId);
				});
			}
			finally
			{
				if (entryArray != null)
				{
					entries = XMLSerializableBase.Serialize(new List<ReportEntry>(entryArray), false);
				}
			}
		}

		// Token: 0x060000C2 RID: 194 RVA: 0x00007694 File Offset: 0x00005894
		void IMailboxReplicationProxyService.IMailbox_AddMoveHistoryEntry(long mailboxHandle, string mheData, int maxMoveHistoryLength)
		{
			MrsTracer.ProxyService.Function("MRSProxy.IMailbox_AddMoveHistoryEntry({0})", new object[]
			{
				mailboxHandle
			});
			this.ExecuteServiceCall<IMailbox>(mailboxHandle, ExecutionFlags.Default, DelayScopeKind.DbWrite, delegate(IMailbox mbx)
			{
				MoveHistoryEntryInternal mhei = XMLSerializableBase.Deserialize<MoveHistoryEntryInternal>(mheData, true);
				mbx.AddMoveHistoryEntry(mhei, maxMoveHistoryLength);
			});
		}

		// Token: 0x060000C3 RID: 195 RVA: 0x00007718 File Offset: 0x00005918
		void IMailboxReplicationProxyService.IMailbox_CheckServerHealth(long mailboxHandle)
		{
			MrsTracer.ProxyService.Function("MRSProxy.IMailbox_CheckServerHealth({0})", new object[]
			{
				mailboxHandle
			});
			this.ExecuteServiceCall<IMailbox>(mailboxHandle, ExecutionFlags.Default, DelayScopeKind.CPUOnly, delegate(IMailbox mbx)
			{
				ServerHealthStatus serverHealthStatus = mbx.CheckServerHealth();
				if (serverHealthStatus.HealthState == ServerHealthState.NotHealthy)
				{
					throw new MailboxReplicationTransientException(serverHealthStatus.FailureReason);
				}
			});
		}

		// Token: 0x060000C4 RID: 196 RVA: 0x00007784 File Offset: 0x00005984
		ServerHealthStatus IMailboxReplicationProxyService.IMailbox_CheckServerHealth2(long mailboxHandle)
		{
			MrsTracer.ProxyService.Function("MRSProxy.IMailbox_CheckServerHealth2({0})", new object[]
			{
				mailboxHandle
			});
			ServerHealthStatus serverHealthStatus = null;
			this.ExecuteServiceCall<IMailbox>(mailboxHandle, ExecutionFlags.ThrottlingNotRequired, DelayScopeKind.CPUOnly, delegate(IMailbox mbx)
			{
				serverHealthStatus = mbx.CheckServerHealth();
			});
			return serverHealthStatus;
		}

		// Token: 0x060000C5 RID: 197 RVA: 0x000077FC File Offset: 0x000059FC
		PropValueData[] IMailboxReplicationProxyService.IMailbox_GetProps(long mailboxHandle, int[] ptags)
		{
			MrsTracer.ProxyService.Function("MRSProxy.IMailbox_GetProps({0})", new object[]
			{
				mailboxHandle
			});
			PropValueData[] result = null;
			this.ExecuteServiceCall<IMailbox>(mailboxHandle, ExecutionFlags.Default, DelayScopeKind.DbRead, delegate(IMailbox mbx)
			{
				result = mbx.GetProps(DataConverter<PropTagConverter, PropTag, int>.GetNative(ptags));
			});
			return result;
		}

		// Token: 0x060000C6 RID: 198 RVA: 0x00007874 File Offset: 0x00005A74
		byte[] IMailboxReplicationProxyService.IMailbox_GetReceiveFolderEntryId(long mailboxHandle, string msgClass)
		{
			MrsTracer.ProxyService.Function("MRSProxy.IMailbox_GetReceiveFolderEntryId({0})", new object[]
			{
				mailboxHandle
			});
			byte[] result = null;
			this.ExecuteServiceCall<IMailbox>(mailboxHandle, ExecutionFlags.Default, DelayScopeKind.DbRead, delegate(IMailbox mbx)
			{
				result = mbx.GetReceiveFolderEntryId(msgClass);
			});
			return result;
		}

		// Token: 0x060000C7 RID: 199 RVA: 0x00007980 File Offset: 0x00005B80
		DataExportBatch IMailboxReplicationProxyService.ISourceFolder_CopyTo(long folderHandle, int flags, int[] excludeTags, byte[] targetObjectData)
		{
			MrsTracer.ProxyService.Function("MRSProxy.ISourceFolder_CopyTo({0})", new object[]
			{
				folderHandle
			});
			DataExportBatch result = null;
			this.ExecuteServiceCall<ISourceFolder>(folderHandle, ExecutionFlags.Default, DelayScopeKind.DbRead, delegate(ISourceFolder folder)
			{
				IDataMessage getDataResponseMsg = FxProxyGetObjectDataResponseMessage.Deserialize(DataMessageOpcode.FxProxyGetObjectDataResponse, targetObjectData, this.UseCompression);
				DataExport dataExport = new DataExport(getDataResponseMsg, this);
				try
				{
					dataExport.FolderExport(folder, (CopyPropertiesFlags)flags, DataConverter<PropTagConverter, PropTag, int>.GetNative(excludeTags));
					result = ((IDataExport)dataExport).ExportData();
					if (!result.IsLastBatch)
					{
						result.DataExportHandle = this.handleCache.AddObject(dataExport, folderHandle);
						dataExport = null;
					}
				}
				finally
				{
					if (dataExport != null)
					{
						dataExport.Dispose();
					}
				}
			});
			return result;
		}

		// Token: 0x060000C8 RID: 200 RVA: 0x00007A04 File Offset: 0x00005C04
		DataExportBatch IMailboxReplicationProxyService.ISourceFolder_Export2(long folderHandle, int[] excludeTags, byte[] targetObjectData)
		{
			MrsTracer.ProxyService.Function("MRSProxy.ISourceFolder_Export2({0})", new object[]
			{
				folderHandle
			});
			return ((IMailboxReplicationProxyService)this).ISourceFolder_CopyTo(folderHandle, 0, excludeTags, targetObjectData);
		}

		// Token: 0x060000C9 RID: 201 RVA: 0x00007AE8 File Offset: 0x00005CE8
		DataExportBatch IMailboxReplicationProxyService.ISourceFolder_ExportMessages(long folderHandle, int flags, byte[][] entryIds, byte[] targetObjectData)
		{
			MrsTracer.ProxyService.Function("MRSProxy.ISourceFolder_ExportMessages({0})", new object[]
			{
				folderHandle
			});
			DataExportBatch result = null;
			this.ExecuteServiceCall<ISourceFolder>(folderHandle, ExecutionFlags.Default, DelayScopeKind.DbRead, delegate(ISourceFolder folder)
			{
				IDataMessage getDataResponseMsg = FxProxyGetObjectDataResponseMessage.Deserialize(DataMessageOpcode.FxProxyGetObjectDataResponse, targetObjectData, this.UseCompression);
				DataExport dataExport = new DataExport(getDataResponseMsg, this);
				try
				{
					dataExport.FolderExportMessages(folder, (CopyMessagesFlags)flags, entryIds);
					result = ((IDataExport)dataExport).ExportData();
					if (!result.IsLastBatch)
					{
						result.DataExportHandle = this.handleCache.AddObject(dataExport, folderHandle);
						dataExport = null;
					}
				}
				finally
				{
					if (dataExport != null)
					{
						dataExport.Dispose();
					}
				}
			});
			return result;
		}

		// Token: 0x060000CA RID: 202 RVA: 0x00007B6C File Offset: 0x00005D6C
		FolderChangesManifest IMailboxReplicationProxyService.ISourceFolder_EnumerateChanges(long folderHandle, bool catchup)
		{
			MrsTracer.ProxyService.Function("MRSProxy.ISourceFolder_EnumerateChanges({0})", new object[]
			{
				folderHandle
			});
			EnumerateContentChangesFlags flags = catchup ? EnumerateContentChangesFlags.Catchup : EnumerateContentChangesFlags.None;
			return ((IMailboxReplicationProxyService)this).ISourceFolder_EnumerateChanges2(folderHandle, (int)flags, 0);
		}

		// Token: 0x060000CB RID: 203 RVA: 0x00007BCC File Offset: 0x00005DCC
		FolderChangesManifest IMailboxReplicationProxyService.ISourceFolder_EnumerateChanges2(long folderHandle, int flags, int maxChanges)
		{
			MrsTracer.ProxyService.Function("MRSProxy.ISourceFolder_EnumerateChanges2({0})", new object[]
			{
				folderHandle
			});
			FolderChangesManifest result = null;
			this.ExecuteServiceCall<ISourceFolder>(folderHandle, ExecutionFlags.Default, DelayScopeKind.DbRead, delegate(ISourceFolder folder)
			{
				result = folder.EnumerateChanges((EnumerateContentChangesFlags)flags, maxChanges);
			});
			return result;
		}

		// Token: 0x060000CC RID: 204 RVA: 0x00007C4C File Offset: 0x00005E4C
		List<MessageRec> IMailboxReplicationProxyService.ISourceFolder_EnumerateMessagesPaged(long folderHandle, int maxPageSize)
		{
			MrsTracer.ProxyService.Function("MRSProxy.ISourceFolder_EnumerateMessagesPaged({0}, {1})", new object[]
			{
				folderHandle,
				maxPageSize
			});
			List<MessageRec> result = null;
			this.ExecuteServiceCall<ISourceFolder>(folderHandle, ExecutionFlags.Default, DelayScopeKind.DbRead, delegate(ISourceFolder folder)
			{
				result = folder.EnumerateMessagesPaged(maxPageSize);
			});
			return result;
		}

		// Token: 0x060000CD RID: 205 RVA: 0x00007CCC File Offset: 0x00005ECC
		int IMailboxReplicationProxyService.ISourceFolder_GetEstimatedItemCount(long folderHandle)
		{
			MrsTracer.ProxyService.Function("MRSProxy.ISourceFolder_GetEstimatedItemCount({0})", new object[]
			{
				folderHandle
			});
			int result = 0;
			this.ExecuteServiceCall<ISourceFolder>(folderHandle, ExecutionFlags.Default, DelayScopeKind.DbRead, delegate(ISourceFolder folder)
			{
				result = folder.GetEstimatedItemCount();
			});
			return result;
		}

		// Token: 0x060000CE RID: 206 RVA: 0x00007D24 File Offset: 0x00005F24
		PropValueData[] IMailboxReplicationProxyService.ISourceFolder_GetProps(long folderHandle, int[] pta)
		{
			MrsTracer.ProxyService.Function("MRSProxy.ISourceFolder_GetProps({0})", new object[]
			{
				folderHandle
			});
			return ((IMailboxReplicationProxyService)this).IFolder_GetProps(folderHandle, pta);
		}

		// Token: 0x060000CF RID: 207 RVA: 0x00007D5C File Offset: 0x00005F5C
		void IMailboxReplicationProxyService.ISourceFolder_GetSearchCriteria(long folderHandle, out RestrictionData restriction, out byte[][] entryIDs, out int searchState)
		{
			MrsTracer.ProxyService.Function("MRSProxy.ISourceFolder_GetSearchCriteria({0})", new object[]
			{
				folderHandle
			});
			((IMailboxReplicationProxyService)this).IFolder_GetSearchCriteria(folderHandle, out restriction, out entryIDs, out searchState);
		}

		// Token: 0x060000D0 RID: 208 RVA: 0x00007E40 File Offset: 0x00006040
		DataExportBatch IMailboxReplicationProxyService.ISourceMailbox_Export2(long mailboxHandle, int[] excludeProps, byte[] targetObjectData)
		{
			MrsTracer.ProxyService.Function("MRSProxy.ISourceMailbox_Export2({0})", new object[]
			{
				mailboxHandle
			});
			DataExportBatch result = null;
			this.ExecuteServiceCall<ISourceMailbox>(mailboxHandle, ExecutionFlags.Default, DelayScopeKind.DbRead, delegate(ISourceMailbox mbx)
			{
				IDataMessage getDataResponseMsg = FxProxyGetObjectDataResponseMessage.Deserialize(DataMessageOpcode.FxProxyGetObjectDataResponse, targetObjectData, this.UseCompression);
				DataExport dataExport = new DataExport(getDataResponseMsg, this);
				try
				{
					dataExport.MailboxExport(mbx, DataConverter<PropTagConverter, PropTag, int>.GetNative(excludeProps));
					result = ((IDataExport)dataExport).ExportData();
					if (!result.IsLastBatch)
					{
						result.DataExportHandle = this.handleCache.AddObject(dataExport, mailboxHandle);
						dataExport = null;
					}
				}
				finally
				{
					if (dataExport != null)
					{
						dataExport.Dispose();
					}
				}
			});
			return result;
		}

		// Token: 0x060000D1 RID: 209 RVA: 0x00007F64 File Offset: 0x00006164
		DataExportBatch IMailboxReplicationProxyService.ISourceMailbox_ExportMessageBatch2(long mailboxHandle, List<MessageRec> messages, byte[] targetObjectData)
		{
			MrsTracer.ProxyService.Function("MRSProxy.ISourceMailbox_ExportMessageBatch2({0})", new object[]
			{
				mailboxHandle
			});
			DataExportBatch result = null;
			this.ExecuteServiceCall<ISourceMailbox>(mailboxHandle, ExecutionFlags.Default, DelayScopeKind.DbRead, delegate(ISourceMailbox mbx)
			{
				IDataMessage getDataResponseMsg = FxProxyPoolGetFolderDataResponseMessage.Deserialize(DataMessageOpcode.FxProxyPoolGetFolderDataResponse, targetObjectData, this.UseCompression);
				DataExport dataExport = new DataExport(getDataResponseMsg, this);
				try
				{
					dataExport.MessageExportWithBadMessageDetection(mbx, messages, ExportMessagesFlags.None, null, true);
					result = ((IDataExport)dataExport).ExportData();
					if (!result.IsLastBatch)
					{
						result.DataExportHandle = this.handleCache.AddObject(dataExport, mailboxHandle);
						dataExport = null;
					}
				}
				finally
				{
					if (dataExport != null)
					{
						dataExport.Dispose();
					}
				}
			});
			return result;
		}

		// Token: 0x060000D2 RID: 210 RVA: 0x000080C4 File Offset: 0x000062C4
		DataExportBatch IMailboxReplicationProxyService.ISourceMailbox_ExportMessages(long mailboxHandle, List<MessageRec> messages, int flags, int[] excludeProps, byte[] targetObjectData)
		{
			MrsTracer.ProxyService.Function("MRSProxy.ISourceMailbox_ExportMessages({0})", new object[]
			{
				mailboxHandle
			});
			DataExportBatch result = null;
			this.ExecuteServiceCall<ISourceMailbox>(mailboxHandle, ExecutionFlags.Default, DelayScopeKind.DbRead, delegate(ISourceMailbox mbx)
			{
				IDataMessage getDataResponseMsg = FxProxyPoolGetFolderDataResponseMessage.Deserialize(DataMessageOpcode.FxProxyPoolGetFolderDataResponse, targetObjectData, this.UseCompression);
				DataExport dataExport = new DataExport(getDataResponseMsg, this);
				try
				{
					PropTag[] native = DataConverter<PropTagConverter, PropTag, int>.GetNative(excludeProps);
					if (!this.ClientVersion[16])
					{
						dataExport.MessageExportWithBadMessageDetection(mbx, messages, (ExportMessagesFlags)flags, native, false);
					}
					else
					{
						dataExport.MessageExport(mbx, messages, (ExportMessagesFlags)flags, native);
					}
					result = ((IDataExport)dataExport).ExportData();
					if (!result.IsLastBatch)
					{
						result.DataExportHandle = this.handleCache.AddObject(dataExport, mailboxHandle);
						dataExport = null;
					}
				}
				finally
				{
					if (dataExport != null)
					{
						dataExport.Dispose();
					}
				}
			});
			return result;
		}

		// Token: 0x060000D3 RID: 211 RVA: 0x00008264 File Offset: 0x00006464
		DataExportBatch IMailboxReplicationProxyService.ISourceMailbox_ExportFolders(long mailboxHandle, List<byte[]> folderIds, int exportFoldersDataToCopyFlags, int folderRecFlags, int[] additionalFolderRecProps, int copyPropertiesFlags, int[] excludeProps, int extendedAclFlags)
		{
			MrsTracer.ProxyService.Function("MRSProxy.ISourceMailbox_ExportFolders({0})", new object[]
			{
				mailboxHandle
			});
			DataExportBatch result = null;
			this.ExecuteServiceCall<ISourceMailbox>(mailboxHandle, ExecutionFlags.Default, DelayScopeKind.DbRead, delegate(ISourceMailbox mbx)
			{
				EntryIdMap<byte[]> entryIdMap = new EntryIdMap<byte[]>();
				foreach (byte[] key in folderIds)
				{
					entryIdMap.Add(key, MapiUtils.MapiFolderObjectData);
				}
				IDataMessage getDataResponseMsg = new FxProxyPoolGetFolderDataResponseMessage(entryIdMap);
				DataExport dataExport = new DataExport(getDataResponseMsg, this);
				try
				{
					PropTag[] native = DataConverter<PropTagConverter, PropTag, int>.GetNative(additionalFolderRecProps);
					PropTag[] native2 = DataConverter<PropTagConverter, PropTag, int>.GetNative(excludeProps);
					dataExport.FoldersExport(mbx, folderIds, (ExportFoldersDataToCopyFlags)exportFoldersDataToCopyFlags, (GetFolderRecFlags)folderRecFlags, native, (CopyPropertiesFlags)copyPropertiesFlags, native2, (AclFlags)extendedAclFlags);
					result = ((IDataExport)dataExport).ExportData();
					if (!result.IsLastBatch)
					{
						result.DataExportHandle = this.handleCache.AddObject(dataExport, mailboxHandle);
						dataExport = null;
					}
				}
				finally
				{
					if (dataExport != null)
					{
						dataExport.Dispose();
					}
				}
			});
			return result;
		}

		// Token: 0x060000D4 RID: 212 RVA: 0x00008308 File Offset: 0x00006508
		MailboxChangesManifest IMailboxReplicationProxyService.ISourceMailbox_EnumerateHierarchyChanges(long mailboxHandle, bool catchup)
		{
			MrsTracer.ProxyService.Function("MRSProxy.ISourceMailbox_EnumerateHierarchyChanges({0})", new object[]
			{
				mailboxHandle
			});
			EnumerateHierarchyChangesFlags flags = catchup ? EnumerateHierarchyChangesFlags.Catchup : EnumerateHierarchyChangesFlags.None;
			return ((IMailboxReplicationProxyService)this).ISourceMailbox_EnumerateHierarchyChanges2(mailboxHandle, (int)flags, 0);
		}

		// Token: 0x060000D5 RID: 213 RVA: 0x00008368 File Offset: 0x00006568
		MailboxChangesManifest IMailboxReplicationProxyService.ISourceMailbox_EnumerateHierarchyChanges2(long mailboxHandle, int flags, int maxChanges)
		{
			MrsTracer.ProxyService.Function("MRSProxy.ISourceMailbox_EnumerateHierarchyChanges({0})", new object[]
			{
				mailboxHandle
			});
			MailboxChangesManifest result = null;
			this.ExecuteServiceCall<ISourceMailbox>(mailboxHandle, ExecutionFlags.Default, DelayScopeKind.DbRead, delegate(ISourceMailbox mbx)
			{
				result = mbx.EnumerateHierarchyChanges((EnumerateHierarchyChangesFlags)flags, maxChanges);
			});
			return result;
		}

		// Token: 0x060000D6 RID: 214 RVA: 0x00008418 File Offset: 0x00006618
		long IMailboxReplicationProxyService.ISourceMailbox_GetFolder(long mailboxHandle, byte[] entryId)
		{
			MrsTracer.ProxyService.Function("MRSProxy.ISourceMailbox_GetFolder({0})", new object[]
			{
				mailboxHandle
			});
			long result = -1L;
			this.ExecuteServiceCall<ISourceMailbox>(mailboxHandle, ExecutionFlags.Default, DelayScopeKind.DbRead, delegate(ISourceMailbox mbx)
			{
				ISourceFolder folder = mbx.GetFolder(entryId);
				if (folder != null)
				{
					result = this.handleCache.AddObject(folder, mailboxHandle);
					return;
				}
				result = 0L;
			});
			return result;
		}

		// Token: 0x060000D7 RID: 215 RVA: 0x000084AC File Offset: 0x000066AC
		List<ReplayActionResult> IMailboxReplicationProxyService.ISourceMailbox_ReplayActions(long mailboxHandle, List<ReplayAction> actions)
		{
			MrsTracer.ProxyService.Function("MRSProxy.ISourceMailbox_ReplayActions({0})", new object[]
			{
				mailboxHandle
			});
			List<ReplayActionResult> result = null;
			this.ExecuteServiceCall<ISourceMailbox>(mailboxHandle, ExecutionFlags.Default, DelayScopeKind.CPUOnly, delegate(ISourceMailbox mbx)
			{
				result = mbx.ReplayActions(actions);
			});
			return result;
		}

		// Token: 0x060000D8 RID: 216 RVA: 0x00008524 File Offset: 0x00006724
		List<ItemPropertiesBase> IMailboxReplicationProxyService.ISourceMailbox_GetMailboxSettings(long mailboxHandle, int flags)
		{
			MrsTracer.ProxyService.Function("MRSProxy.ISourceMailbox_GetMailboxSettings({0})", new object[]
			{
				mailboxHandle
			});
			List<ItemPropertiesBase> result = null;
			this.ExecuteServiceCall<ISourceMailbox>(mailboxHandle, ExecutionFlags.Default, DelayScopeKind.CPUOnly, delegate(ISourceMailbox mbx)
			{
				result = mbx.GetMailboxSettings((GetMailboxSettingsFlags)flags);
			});
			return result;
		}

		// Token: 0x060000D9 RID: 217 RVA: 0x0000859C File Offset: 0x0000679C
		Guid IMailboxReplicationProxyService.IMailbox_StartIsInteg(long mailboxHandle, List<uint> mailboxCorruptionTypes)
		{
			Guid result = Guid.Empty;
			MrsTracer.ProxyService.Function("MRSProxy.ISourceMailbox_StartIsInteg({0})", new object[]
			{
				mailboxHandle
			});
			this.ExecuteServiceCall<ISourceMailbox>(mailboxHandle, ExecutionFlags.Default, DelayScopeKind.DbRead, delegate(ISourceMailbox mbx)
			{
				result = mbx.StartIsInteg(mailboxCorruptionTypes);
			});
			return result;
		}

		// Token: 0x060000DA RID: 218 RVA: 0x00008618 File Offset: 0x00006818
		List<StoreIntegrityCheckJob> IMailboxReplicationProxyService.IMailbox_QueryIsInteg(long mailboxHandle, Guid isIntegRequestGuid)
		{
			List<StoreIntegrityCheckJob> jobs = null;
			MrsTracer.ProxyService.Function("MRSProxy.ISourceMailbox_QueryIsInteg({0})", new object[]
			{
				mailboxHandle
			});
			this.ExecuteServiceCall<ISourceMailbox>(mailboxHandle, ExecutionFlags.Default, DelayScopeKind.DbRead, delegate(ISourceMailbox mbx)
			{
				jobs = mbx.QueryIsInteg(isIntegRequestGuid);
			});
			return jobs;
		}

		// Token: 0x060000DB RID: 219 RVA: 0x00008690 File Offset: 0x00006890
		SessionStatistics IMailboxReplicationProxyService.IMailbox_GetSessionStatistics(long mailboxHandle, int statisticsTypes)
		{
			MrsTracer.ProxyService.Function("MRSProxy.IMailbox_GetSessionStatistics)", new object[0]);
			SessionStatistics result = null;
			SessionStatisticsFlags flags = (SessionStatisticsFlags)statisticsTypes;
			if (flags.HasFlag(SessionStatisticsFlags.MapiDiagnosticGetProp))
			{
				result = new SessionStatistics();
				result.MapiDiagnosticGetProp = this.mapiDiagnosticGetProp;
			}
			else
			{
				this.ExecuteServiceCall<IMailbox>(mailboxHandle, ExecutionFlags.Default, DelayScopeKind.CPUOnly, delegate(IMailbox mbx)
				{
					result = mbx.GetSessionStatistics(flags);
				});
			}
			return result;
		}

		// Token: 0x060000DC RID: 220 RVA: 0x00008738 File Offset: 0x00006938
		byte[] IMailboxReplicationProxyService.ISourceMailbox_GetMailboxBasicInfo(long mailboxHandle)
		{
			MrsTracer.ProxyService.Function("MRSProxy.ISourceMailbox_GetMailboxBasicInfo({0})", new object[]
			{
				mailboxHandle
			});
			byte[] result = null;
			this.ExecuteServiceCall<ISourceMailbox>(mailboxHandle, ExecutionFlags.Default, DelayScopeKind.DbRead, delegate(ISourceMailbox mbx)
			{
				result = mbx.GetMailboxBasicInfo(MailboxSignatureFlags.GetLegacy);
			});
			return result;
		}

		// Token: 0x060000DD RID: 221 RVA: 0x000087AC File Offset: 0x000069AC
		byte[] IMailboxReplicationProxyService.ISourceMailbox_GetMailboxBasicInfo2(long mailboxHandle, int signatureFlags)
		{
			MrsTracer.ProxyService.Function("MRSProxy.ISourceMailbox_GetMailboxBasicInfo2({0}, {1})", new object[]
			{
				mailboxHandle,
				signatureFlags
			});
			byte[] result = null;
			this.ExecuteServiceCall<ISourceMailbox>(mailboxHandle, ExecutionFlags.Default, DelayScopeKind.DbRead, delegate(ISourceMailbox mbx)
			{
				result = mbx.GetMailboxBasicInfo((MailboxSignatureFlags)signatureFlags);
			});
			return result;
		}

		// Token: 0x060000DE RID: 222 RVA: 0x00008838 File Offset: 0x00006A38
		PropValueData[] IMailboxReplicationProxyService.ISourceMailbox_GetProps(long mailboxHandle, int[] ptags)
		{
			MrsTracer.ProxyService.Function("MRSProxy.ISourceMailbox_GetProps({0})", new object[]
			{
				mailboxHandle
			});
			PropValueData[] result = null;
			this.ExecuteServiceCall<ISourceMailbox>(mailboxHandle, ExecutionFlags.Default, DelayScopeKind.DbRead, delegate(ISourceMailbox mbx)
			{
				result = mbx.GetProps(DataConverter<PropTagConverter, PropTag, int>.GetNative(ptags));
			});
			return result;
		}

		// Token: 0x060000DF RID: 223 RVA: 0x00008900 File Offset: 0x00006B00
		DataExportBatch IMailboxReplicationProxyService.ISourceMailbox_GetMailboxSyncState(long mailboxHandle)
		{
			MrsTracer.ProxyService.Function("MRSProxy.ISourceMailbox_GetMailboxSyncState({0})", new object[]
			{
				mailboxHandle
			});
			DataExportBatch result = null;
			this.ExecuteServiceCall<ISourceMailbox>(mailboxHandle, ExecutionFlags.Default, DelayScopeKind.DbRead, delegate(ISourceMailbox mbx)
			{
				string mailboxSyncState = mbx.GetMailboxSyncState();
				IDataExport dataExport = new PagedTransmitter(mailboxSyncState, this.UseCompression);
				result = dataExport.ExportData();
				if (!result.IsLastBatch)
				{
					result.DataExportHandle = this.handleCache.AddObject(dataExport, mailboxHandle);
				}
			});
			return result;
		}

		// Token: 0x060000E0 RID: 224 RVA: 0x00008A4C File Offset: 0x00006C4C
		long IMailboxReplicationProxyService.ISourceMailbox_SetMailboxSyncState(long mailboxHandle, DataExportBatch firstBatch)
		{
			MrsTracer.ProxyService.Function("MRSProxy.ISourceMailbox_GetMailboxSyncState({0})", new object[]
			{
				mailboxHandle
			});
			long result = -1L;
			this.ExecuteServiceCall<ISourceMailbox>(mailboxHandle, ExecutionFlags.Default, DelayScopeKind.CPUOnly, delegate(ISourceMailbox mbx)
			{
				IDataImport dataImport = new PagedReceiver(delegate(string str)
				{
					mbx.SetMailboxSyncState(str);
				}, this.UseCompression);
				try
				{
					IDataMessage message = DataMessageSerializer.Deserialize(firstBatch.Opcode, firstBatch.Data, this.UseCompression);
					dataImport.SendMessage(message);
					if (!firstBatch.IsLastBatch)
					{
						result = this.handleCache.AddObject(dataImport, mailboxHandle);
						dataImport = null;
					}
					else
					{
						result = 0L;
					}
				}
				finally
				{
					if (dataImport != null)
					{
						dataImport.Dispose();
					}
				}
			});
			return result;
		}

		// Token: 0x060000E1 RID: 225 RVA: 0x00008AD8 File Offset: 0x00006CD8
		void IMailboxReplicationProxyService.IDestinationMailbox_PreFinalSyncDataProcessing(long mailboxHandle, int? sourceMailboxVersion)
		{
			MrsTracer.ProxyService.Function("MRSProxy.IDestinationMailbox_PreFinalSyncDataProcessing({0}, {1})", new object[]
			{
				mailboxHandle,
				sourceMailboxVersion
			});
			this.ExecuteServiceCall<IDestinationMailbox>(mailboxHandle, ExecutionFlags.Default, DelayScopeKind.DbWrite, delegate(IDestinationMailbox mbx)
			{
				mbx.PreFinalSyncDataProcessing(sourceMailboxVersion);
			});
		}

		// Token: 0x060000E2 RID: 226 RVA: 0x00008B58 File Offset: 0x00006D58
		int IMailboxReplicationProxyService.IDestinationMailbox_CheckDataGuarantee(long mailboxHandle, DateTime commitTimestamp, out byte[] failureReasonData)
		{
			MrsTracer.ProxyService.Function("MRSProxy.IDestinationMailbox_CheckDataGuarantee({0})", new object[]
			{
				mailboxHandle
			});
			ConstraintCheckResultType result = ConstraintCheckResultType.Satisfied;
			LocalizedString failureReason = LocalizedString.Empty;
			this.ExecuteServiceCall<IDestinationMailbox>(mailboxHandle, ExecutionFlags.Default, DelayScopeKind.DbWrite, delegate(IDestinationMailbox mbx)
			{
				result = mbx.CheckDataGuarantee(commitTimestamp, out failureReason);
			});
			failureReasonData = CommonUtils.ByteSerialize(failureReason);
			return (int)result;
		}

		// Token: 0x060000E3 RID: 227 RVA: 0x00008BD4 File Offset: 0x00006DD4
		void IMailboxReplicationProxyService.IDestinationMailbox_ForceLogRoll(long mailboxHandle)
		{
			MrsTracer.ProxyService.Function("MRSProxy.IDestinationMailbox_ForceLogRoll({0})", new object[]
			{
				mailboxHandle
			});
			this.ExecuteServiceCall<IDestinationMailbox>(mailboxHandle, ExecutionFlags.Default, DelayScopeKind.DbWrite, delegate(IDestinationMailbox mbx)
			{
				mbx.ForceLogRoll();
			});
		}

		// Token: 0x060000E4 RID: 228 RVA: 0x00008C4C File Offset: 0x00006E4C
		List<ReplayAction> IMailboxReplicationProxyService.IDestinationMailbox_GetActions(long mailboxHandle, string replaySyncState, int maxNumberOfActions)
		{
			MrsTracer.ProxyService.Function("MRSProxy.IDestinationMailbox_GetActions({0})", new object[]
			{
				mailboxHandle
			});
			List<ReplayAction> result = null;
			this.ExecuteServiceCall<IDestinationMailbox>(mailboxHandle, ExecutionFlags.Default, DelayScopeKind.DbRead, delegate(IDestinationMailbox mbx)
			{
				result = mbx.GetActions(replaySyncState, maxNumberOfActions);
			});
			return result;
		}

		// Token: 0x060000E5 RID: 229 RVA: 0x00008CC8 File Offset: 0x00006EC8
		void IMailboxReplicationProxyService.IDestinationMailbox_SetMailboxSettings(long mailboxHandle, ItemPropertiesBase item)
		{
			MrsTracer.ProxyService.Function("MRSProxy.IDestinationMailbox_GetActions({0})", new object[]
			{
				mailboxHandle
			});
			this.ExecuteServiceCall<IDestinationMailbox>(mailboxHandle, ExecutionFlags.Default, DelayScopeKind.DbWrite, delegate(IDestinationMailbox mbx)
			{
				mbx.SetMailboxSettings(item);
			});
		}

		// Token: 0x060000E6 RID: 230 RVA: 0x00008D17 File Offset: 0x00006F17
		MigrationAccount[] IMailboxReplicationProxyService.SelectAccountsToMigrate(long maximumAccounts, long? maximumTotalSize, int? constraintId)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060000E7 RID: 231 RVA: 0x00008D20 File Offset: 0x00006F20
		protected override void InternalDispose(bool calledFromDispose)
		{
			lock (MailboxReplicationProxyService.activeConnectionsUpdateLock)
			{
				if (this.connections > 0)
				{
					MailboxReplicationProxyService.activeConnections -= this.connections;
					this.connections = 0;
				}
			}
			if (calledFromDispose)
			{
				lock (this.locker)
				{
					this.handleCache.Dispose();
				}
			}
			MrsTracer.ProxyService.Debug("MailboxReplicationProxyService instance disposed.", new object[0]);
		}

		// Token: 0x060000E8 RID: 232 RVA: 0x00008DC8 File Offset: 0x00006FC8
		protected override DisposeTracker InternalGetDisposeTracker()
		{
			return DisposeTracker.Get<MailboxReplicationProxyService>(this);
		}

		// Token: 0x060000E9 RID: 233 RVA: 0x00008DD0 File Offset: 0x00006FD0
		private void IncrementConnectionCount()
		{
			if (!this.IsThrottled)
			{
				return;
			}
			lock (MailboxReplicationProxyService.activeConnectionsUpdateLock)
			{
				int maxMRSConnections = MRSProxyConfiguration.Instance.MaxMRSConnections;
				if (MailboxReplicationProxyService.activeConnections >= maxMRSConnections)
				{
					MrsTracer.ProxyService.Debug("MRSProxy Service cannot currently accept more connections.", new object[0]);
					MailboxReplicationServiceFault.Throw(new MRSProxyTooManyConnectionsTransientException(MailboxReplicationProxyService.activeConnections, maxMRSConnections));
				}
				MailboxReplicationProxyService.activeConnections++;
				this.connections++;
			}
		}

		// Token: 0x060000EA RID: 234 RVA: 0x00008E64 File Offset: 0x00007064
		private void DecrementConnectionCount()
		{
			if (!this.IsThrottled)
			{
				return;
			}
			lock (MailboxReplicationProxyService.activeConnectionsUpdateLock)
			{
				if (this.connections > 0)
				{
					MailboxReplicationProxyService.activeConnections--;
					this.connections--;
				}
			}
		}

		// Token: 0x060000EB RID: 235 RVA: 0x00008ECC File Offset: 0x000070CC
		private void DisconnectOrphanedSession(Guid physicalMailboxGuid)
		{
			lock (this.locker)
			{
				long mailboxHandle;
				if (this.activeMailboxes.TryGetValue(physicalMailboxGuid, out mailboxHandle))
				{
					this.DisconnectMailboxSession(mailboxHandle);
				}
			}
		}

		// Token: 0x060000EC RID: 236 RVA: 0x00008F20 File Offset: 0x00007120
		private void DisconnectMailboxSession(long mailboxHandle)
		{
			this.handleCache.ReleaseObject(mailboxHandle);
			Guid? guid = null;
			foreach (KeyValuePair<Guid, long> keyValuePair in this.activeMailboxes)
			{
				if (keyValuePair.Value == mailboxHandle)
				{
					guid = new Guid?(keyValuePair.Key);
				}
			}
			if (guid != null)
			{
				this.activeMailboxes.Remove(guid.Value);
			}
		}

		// Token: 0x060000ED RID: 237 RVA: 0x000090B0 File Offset: 0x000072B0
		private void ExecuteServiceCall<T>(long handle, ExecutionFlags flags, DelayScopeKind delayScopeKind, Action<T> operation) where T : class
		{
			if (!flags.HasFlag(ExecutionFlags.NoLock))
			{
				Monitor.Enter(this.locker);
			}
			try
			{
				Exception failure = null;
				try
				{
					CommonUtils.CatchKnownExceptions(delegate
					{
						if (!flags.HasFlag(ExecutionFlags.ThrottlingNotRequired) && !this.IsThrottled)
						{
							MrsTracer.ProxyService.Warning("MRSProxy Service cannot process this call since this connection is not throttled.", new object[0]);
							throw new MRSProxyCannotProcessRequestOnUnthrottledConnectionPermanentException();
						}
						T t = default(T);
						if (handle != -1L)
						{
							t = this.handleCache.GetObject<T>(handle);
						}
						using (this.ActivateSettingsContext(t, handle))
						{
							using (MicroDelayScope.Create(this, delayScopeKind))
							{
								operation(t);
							}
						}
					}, delegate(Exception f)
					{
						failure = f;
					});
				}
				catch (Exception ex)
				{
					MrsTracer.ProxyService.Error("Unhandled exception in MRSProxy:\n{0}\n{1}", new object[]
					{
						CommonUtils.FullExceptionMessage(ex),
						ex.StackTrace
					});
					ExWatson.SendReport(ex);
					throw;
				}
				if (failure != null)
				{
					MailboxReplicationServiceFault.Throw(failure);
				}
			}
			finally
			{
				if (!flags.HasFlag(ExecutionFlags.NoLock))
				{
					Monitor.Exit(this.locker);
				}
			}
		}

		// Token: 0x060000EE RID: 238 RVA: 0x000091E8 File Offset: 0x000073E8
		private IDisposable ActivateSettingsContext(object curObject, long handle)
		{
			if (curObject == null)
			{
				return null;
			}
			ISettingsContextProvider settingsContextProvider;
			for (;;)
			{
				settingsContextProvider = (curObject as ISettingsContextProvider);
				if (settingsContextProvider != null)
				{
					break;
				}
				handle = this.handleCache.GetParentHandle(handle);
				if (handle == -1L)
				{
					goto IL_39;
				}
				curObject = this.handleCache.GetObject<object>(handle);
			}
			return SettingsContextBase.ActivateContext(settingsContextProvider);
			IL_39:
			return null;
		}

		// Token: 0x060000EF RID: 239 RVA: 0x0000970C File Offset: 0x0000790C
		private long Config(Guid reservationId, Guid primaryMailboxGuid, Guid physicalMailboxGuid, string filePath, byte[] partitionHintBytes, Guid mdbGuid, string mdbName, MailboxType mbxType, int proxyControlFlags, int localMbxFlags, Guid? mailboxContainerGuid)
		{
			MrsTracer.ProxyService.Function("MRSProxy.Config(reservationId={0}, primaryMailboxGuid={1}, physicalMailboxGuid={2}, filePath={3}, mdbGuid={4}, mdbName='{5}', mbxType={6}, proxyControlFlags={7}, localMbxFlags={8}, mailboxContainerGuid={9})", new object[]
			{
				reservationId,
				primaryMailboxGuid,
				physicalMailboxGuid,
				filePath,
				mdbGuid,
				mdbName,
				mbxType,
				(ProxyControlFlags)proxyControlFlags,
				(LocalMailboxFlags)localMbxFlags,
				mailboxContainerGuid
			});
			this.ExchangeGuid = physicalMailboxGuid;
			this.proxyControlFlags = (ProxyControlFlags)proxyControlFlags;
			LocalMailboxFlags localMailboxFlags = (LocalMailboxFlags)localMbxFlags;
			long result = -1L;
			this.ExecuteServiceCall<object>(-1L, ExecutionFlags.ThrottlingNotRequired, DelayScopeKind.NoDelay, delegate(object o)
			{
				Guid guid = Guid.Empty;
				if (HttpContext.Current != null)
				{
					if (HttpContext.Current.Items.Contains("MRSProxy.MailboxGuid"))
					{
						object obj = HttpContext.Current.Items["MRSProxy.MailboxGuid"];
						if (obj is Guid)
						{
							guid = (Guid)obj;
						}
					}
					if (primaryMailboxGuid != guid)
					{
						MrsTracer.ProxyService.Debug("The mailbox guid passed in by the caller {0} does not match the one in message headers {1}, bailing.", new object[]
						{
							primaryMailboxGuid,
							guid
						});
						throw new UnexpectedErrorPermanentException(-2147024809);
					}
				}
				TenantPartitionHint partitionHint = null;
				if (partitionHintBytes != null)
				{
					partitionHint = TenantPartitionHint.FromPersistablePartitionHint(partitionHintBytes);
				}
				if (localMailboxFlags.HasFlag(LocalMailboxFlags.ParallelPublicFolderMigration) && VariantConfiguration.GetSnapshot(MachineSettingsContext.Local, null, null).Mrs.PublicFolderMailboxesMigration.Enabled && primaryMailboxGuid == Guid.Empty)
				{
					ADSessionSettings adsessionSettings = ADSessionSettings.FromOrganizationIdWithoutRbacScopesServiceOnly(OrganizationId.ForestWideOrgId);
					PublicFolderInformation hierarchyMailboxInformation = TenantPublicFolderConfigurationCache.Instance.GetValue(adsessionSettings.CurrentOrganizationId).GetHierarchyMailboxInformation();
					if (hierarchyMailboxInformation.Type != PublicFolderInformation.HierarchyType.MailboxGuid || !(hierarchyMailboxInformation.HierarchyMailboxGuid != Guid.Empty))
					{
						throw new NoPublicFolderMailboxFoundInSourceException();
					}
					this.ExchangeGuid = (physicalMailboxGuid = (primaryMailboxGuid = hierarchyMailboxInformation.HierarchyMailboxGuid));
				}
				if (primaryMailboxGuid != Guid.Empty && (localMailboxFlags.HasFlag(LocalMailboxFlags.UseHomeMDB) || (mdbGuid == Guid.Empty && mbxType == MailboxType.SourceMailbox)))
				{
					MiniRecipient miniRecipient = CommonUtils.FindUserByMailboxGuid(primaryMailboxGuid, partitionHint, null, null, MailboxReplicationProxyService.UserPropertiesToLoad);
					if (miniRecipient != null)
					{
						ADObjectId adobjectId;
						if (primaryMailboxGuid == physicalMailboxGuid)
						{
							adobjectId = miniRecipient.Database;
						}
						else
						{
							adobjectId = miniRecipient.ArchiveDatabase;
						}
						if (adobjectId != null)
						{
							mdbGuid = adobjectId.ObjectGuid;
							MrsTracer.ProxyService.Debug("MRSProxy.IMailbox_Config4: Using homeMdb {0} for mailbox {1}", new object[]
							{
								mdbGuid,
								physicalMailboxGuid
							});
						}
					}
				}
				if (!string.IsNullOrEmpty(mdbName))
				{
					MailboxDatabase mailboxDatabase = CommonUtils.FindMdbByName(mdbName, null, null);
					mdbGuid = mailboxDatabase.Guid;
				}
				bool flag = mdbGuid != Guid.Empty && MapiUtils.FindServerForMdb(mdbGuid, null, null, FindServerFlags.None).IsOnThisServer;
				IMailbox mailbox;
				switch (mbxType)
				{
				case MailboxType.SourceMailbox:
					if (localMailboxFlags.HasFlag(LocalMailboxFlags.EasSync))
					{
						mailbox = new EasSourceMailbox();
					}
					else if (localMailboxFlags.HasFlag(LocalMailboxFlags.PstImport))
					{
						mailbox = new PstSourceMailbox();
						mailbox.ConfigPst(filePath, null);
					}
					else if (!flag || localMailboxFlags.HasFlag(LocalMailboxFlags.UseMapiProvider))
					{
						mailbox = new MapiSourceMailbox(localMailboxFlags);
					}
					else
					{
						mailbox = new StorageSourceMailbox(localMailboxFlags);
					}
					break;
				case MailboxType.DestMailboxIntraOrg:
				case MailboxType.DestMailboxCrossOrg:
					if (localMailboxFlags.HasFlag(LocalMailboxFlags.PstExport))
					{
						mailbox = new PstDestinationMailbox();
						mailbox.ConfigPst(filePath, null);
					}
					else if (!flag || localMailboxFlags.HasFlag(LocalMailboxFlags.UseMapiProvider))
					{
						mailbox = new MapiDestinationMailbox(localMailboxFlags);
					}
					else
					{
						mailbox = new StorageDestinationMailbox(localMailboxFlags);
					}
					break;
				default:
					throw new UnexpectedErrorPermanentException(-2147024809);
				}
				((MailboxProviderBase)mailbox).MRSVersion = this.clientVersion;
				ReservationBase reservationBase = ReservationManager.FindReservation(reservationId);
				if (reservationBase != null)
				{
					MailboxReservation mailboxReservation = reservationBase as MailboxReservation;
					mailboxReservation.DisconnectOrphanedSession(physicalMailboxGuid);
					mailboxReservation.RegisterDisconnectOrphanedSessionAction(physicalMailboxGuid, new Action<Guid>(this.DisconnectOrphanedSession));
				}
				mailbox.Config(reservationBase, primaryMailboxGuid, physicalMailboxGuid, partitionHint, mdbGuid, mbxType, mailboxContainerGuid);
				if (!string.IsNullOrEmpty(mdbName))
				{
					mailbox.ConfigMDBByName(mdbName);
				}
				result = this.handleCache.AddObject(mailbox, -1L);
				this.activeMailboxes[physicalMailboxGuid] = result;
			});
			return result;
		}

		// Token: 0x0400003D RID: 61
		private const int FolderBatchSize = 100;

		// Token: 0x0400003E RID: 62
		private const int MessageBatchSize = 1000;

		// Token: 0x0400003F RID: 63
		private const long NoHandle = -1L;

		// Token: 0x04000040 RID: 64
		private static readonly ADPropertyDefinition[] UserPropertiesToLoad = new ADPropertyDefinition[]
		{
			ADMailboxRecipientSchema.Database,
			ADUserSchema.ArchiveDatabase
		};

		// Token: 0x04000041 RID: 65
		private static readonly object activeConnectionsUpdateLock;

		// Token: 0x04000042 RID: 66
		private static int activeConnections = 0;

		// Token: 0x04000043 RID: 67
		private readonly object locker = new object();

		// Token: 0x04000044 RID: 68
		private readonly Dictionary<Guid, long> activeMailboxes = new Dictionary<Guid, long>();

		// Token: 0x04000045 RID: 69
		private HandleCache handleCache;

		// Token: 0x04000046 RID: 70
		private VersionInformation clientVersion;

		// Token: 0x04000047 RID: 71
		private int connections;

		// Token: 0x04000048 RID: 72
		private ProxyControlFlags proxyControlFlags;

		// Token: 0x04000049 RID: 73
		private long enumerateMessagesFolder;

		// Token: 0x0400004A RID: 74
		private List<MessageRec> enumerateMessagesRemainingData;

		// Token: 0x0400004B RID: 75
		private long enumerateFoldersMailbox;

		// Token: 0x0400004C RID: 76
		private List<FolderRec> enumerateFoldersRemainingData;

		// Token: 0x0400004D RID: 77
		private string mapiDiagnosticGetProp = string.Empty;
	}
}
