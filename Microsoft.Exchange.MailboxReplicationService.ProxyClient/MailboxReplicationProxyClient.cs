using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net;
using System.Net.Security;
using System.Security.Principal;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.Text;
using System.Threading;
using Microsoft.Exchange.Collections.TimeoutCache;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Data.Directory.SystemConfiguration.ConfigurationSettings;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.WorkloadManagement;
using Microsoft.Exchange.Net.Protocols;
using Microsoft.Exchange.Security.Authorization;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x0200000A RID: 10
	internal class MailboxReplicationProxyClient : WcfClientWithFaultHandling<IMailboxReplicationProxyService, FaultException<MailboxReplicationServiceFault>>, IMailboxReplicationProxyService
	{
		// Token: 0x0600001E RID: 30 RVA: 0x00002C38 File Offset: 0x00000E38
		private MailboxReplicationProxyClient(Binding binding, EndpointAddress address, Guid physicalMbxGuid, Guid primaryMbxGuid, string filePath, string targetDatabase, TenantPartitionHint partitionHint, ProxyControlFlags proxyControlFlags, TimeSpan longOperationTimeout) : base(binding, address)
		{
			this.Init(physicalMbxGuid, primaryMbxGuid, filePath, targetDatabase, partitionHint, proxyControlFlags, longOperationTimeout);
		}

		// Token: 0x0600001F RID: 31 RVA: 0x00002C76 File Offset: 0x00000E76
		private MailboxReplicationProxyClient(string endpointConfigurationName, Guid physicalMbxGuid, Guid primaryMbxGuid, string filePath, string targetDatabase, TenantPartitionHint partitionHint, ProxyControlFlags proxyControlFlags, TimeSpan longOperationTimeout) : base(endpointConfigurationName)
		{
			this.Init(physicalMbxGuid, primaryMbxGuid, filePath, targetDatabase, partitionHint, proxyControlFlags, longOperationTimeout);
		}

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000020 RID: 32 RVA: 0x00002CB2 File Offset: 0x00000EB2
		public LatencyInfo LatencyInfo
		{
			get
			{
				return this.latencyInfo;
			}
		}

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x06000021 RID: 33 RVA: 0x00002CBA File Offset: 0x00000EBA
		public bool UseCompression
		{
			get
			{
				return !this.proxyControlFlags.HasFlag(ProxyControlFlags.DoNotCompress);
			}
		}

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x06000022 RID: 34 RVA: 0x00002CD5 File Offset: 0x00000ED5
		public bool UseBuffering
		{
			get
			{
				return !this.proxyControlFlags.HasFlag(ProxyControlFlags.DoNotBuffer);
			}
		}

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x06000023 RID: 35 RVA: 0x00002CF0 File Offset: 0x00000EF0
		public bool UseCertificateToAuthenticate
		{
			get
			{
				return this.proxyControlFlags.HasFlag(ProxyControlFlags.UseCertificateToAuthenticate);
			}
		}

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x06000024 RID: 36 RVA: 0x00002D0C File Offset: 0x00000F0C
		public string ServerName
		{
			get
			{
				if (base.ServerVersion == null)
				{
					return base.Endpoint.Address.ToString();
				}
				return base.ServerVersion.ComputerName;
			}
		}

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x06000025 RID: 37 RVA: 0x00002D32 File Offset: 0x00000F32
		// (set) Token: 0x06000026 RID: 38 RVA: 0x00002D3A File Offset: 0x00000F3A
		public TimeSpan LongOperationTimeout { get; private set; }

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x06000027 RID: 39 RVA: 0x00002D43 File Offset: 0x00000F43
		// (set) Token: 0x06000028 RID: 40 RVA: 0x00002D4B File Offset: 0x00000F4B
		public MRSProxyRequestContext RequestContext { get; private set; }

		// Token: 0x06000029 RID: 41 RVA: 0x00002D54 File Offset: 0x00000F54
		public static MailboxReplicationProxyClient CreateForOlcConnection(string serverName, ProxyControlFlags flags)
		{
			flags |= (ProxyControlFlags.UseCertificateToAuthenticate | ProxyControlFlags.Olc);
			return MailboxReplicationProxyClient.Create(serverName, null, null, Guid.Empty, Guid.Empty, null, null, null, true, flags, ConfigBase<MRSConfigSchema>.GetConfig<TimeSpan>("MRSProxyLongOperationTimeout"));
		}

		// Token: 0x0600002A RID: 42 RVA: 0x00002D8C File Offset: 0x00000F8C
		public static MailboxReplicationProxyClient Create(string serverName, string remoteOrgName, NetworkCredential remoteCred, Guid physicalMbxGuid, Guid primaryMbxGuid, string filePath, string database, TenantPartitionHint partitionHint, bool useHttps, ProxyControlFlags flags, TimeSpan longOperationTimeout)
		{
			string endpointConfigurationName;
			if (flags.HasFlag(ProxyControlFlags.UseTcp) || TestIntegration.Instance.UseTcpForRemoteMoves)
			{
				endpointConfigurationName = "MrsProxyClientTcpEndpoint";
			}
			else if (TestIntegration.Instance.UseHttpsForLocalMoves && !useHttps)
			{
				endpointConfigurationName = "MrsProxyClientMrsHttpsEndpoint";
			}
			else if (flags.HasFlag(ProxyControlFlags.UseCertificateToAuthenticate))
			{
				endpointConfigurationName = "MrsProxyClientCertEndpoint";
			}
			else if (useHttps)
			{
				endpointConfigurationName = "MrsProxyClientHttpsEndpoint";
			}
			else
			{
				endpointConfigurationName = "MrsProxyClientTcpEndpoint";
			}
			MailboxReplicationProxyClient result;
			using (DisposeGuard disposeGuard = default(DisposeGuard))
			{
				MailboxReplicationProxyClient mailboxReplicationProxyClient;
				if (TestIntegration.Instance.ProtocolTest)
				{
					NetTcpBinding netTcpBinding = new NetTcpBinding(SecurityMode.Transport);
					netTcpBinding.Security.Transport.ProtectionLevel = ProtectionLevel.EncryptAndSign;
					netTcpBinding.Security.Transport.ClientCredentialType = TcpClientCredentialType.Windows;
					netTcpBinding.Security.Message.ClientCredentialType = MessageCredentialType.Windows;
					netTcpBinding.TransactionFlow = false;
					netTcpBinding.TransferMode = TransferMode.Buffered;
					netTcpBinding.ReceiveTimeout = TimeSpan.FromMinutes(20.0);
					netTcpBinding.SendTimeout = TimeSpan.FromMinutes(20.0);
					netTcpBinding.MaxReceivedMessageSize = 100000000L;
					netTcpBinding.ReaderQuotas.MaxDepth = 256;
					netTcpBinding.ReaderQuotas.MaxStringContentLength = 35000000;
					netTcpBinding.ReaderQuotas.MaxArrayLength = 35000000;
					netTcpBinding.ReaderQuotas.MaxBytesPerRead = 4096;
					netTcpBinding.ReaderQuotas.MaxNameTableCharCount = 16384;
					EndpointAddress address = new EndpointAddress("net.tcp://localhost/Microsoft.Exchange.MailboxReplicationService.ProxyService");
					mailboxReplicationProxyClient = new MailboxReplicationProxyClient(netTcpBinding, address, physicalMbxGuid, primaryMbxGuid, filePath, database, partitionHint, flags, longOperationTimeout);
				}
				else
				{
					mailboxReplicationProxyClient = new MailboxReplicationProxyClient(endpointConfigurationName, physicalMbxGuid, primaryMbxGuid, filePath, database, partitionHint, flags, longOperationTimeout);
				}
				disposeGuard.Add<MailboxReplicationProxyClient>(mailboxReplicationProxyClient);
				mailboxReplicationProxyClient.Initialize(serverName, remoteCred, useHttps);
				if (flags.HasFlag(ProxyControlFlags.ResolveServerName) && mailboxReplicationProxyClient.ServerVersion[45])
				{
					ProxyServerInformation proxyServerInformation = ((IMailboxReplicationProxyService)mailboxReplicationProxyClient).FindServerByDatabaseOrMailbox(database, new Guid?(physicalMbxGuid), new Guid?(primaryMbxGuid), (partitionHint != null) ? partitionHint.GetPersistablePartitionHint() : null);
					if (!proxyServerInformation.IsProxyLocal)
					{
						flags &= ~ProxyControlFlags.ResolveServerName;
						return MailboxReplicationProxyClient.Create(proxyServerInformation.ServerFqdn, remoteOrgName, remoteCred, physicalMbxGuid, primaryMbxGuid, filePath, database, partitionHint, useHttps, flags, longOperationTimeout);
					}
				}
				disposeGuard.Success();
				result = mailboxReplicationProxyClient;
			}
			return result;
		}

		// Token: 0x0600002B RID: 43 RVA: 0x00002FF4 File Offset: 0x000011F4
		public static MailboxReplicationProxyClient CreateWithoutThrottling(string serverFQDN, NetworkCredential credentials, Guid mailboxGuid, Guid mdbGuid)
		{
			return MailboxReplicationProxyClient.Create(serverFQDN, null, credentials, mailboxGuid, mailboxGuid, null, mdbGuid.ToString(), null, false, ProxyControlFlags.DoNotApplyProxyThrottling | ProxyControlFlags.DoNotCompress, MailboxReplicationProxyClient.DefaultOperationTimeout);
		}

		// Token: 0x0600002C RID: 44 RVA: 0x00003024 File Offset: 0x00001224
		public static MailboxReplicationProxyClient CreateConnectivityTestClient(string serverFQDN, NetworkCredential credentials, bool useHttps)
		{
			return MailboxReplicationProxyClient.Create(serverFQDN, null, credentials, MailboxReplicationProxyClient.ConnectionTestGuid, MailboxReplicationProxyClient.ConnectionTestGuid, null, null, null, useHttps, ProxyControlFlags.DoNotApplyProxyThrottling | ProxyControlFlags.DoNotAddIdentifyingCafeHeaders, MailboxReplicationProxyClient.DefaultOperationTimeout);
		}

		// Token: 0x0600002D RID: 45 RVA: 0x00003050 File Offset: 0x00001250
		public void ExchangeVersionInformation()
		{
			VersionInformation serverVersion = null;
			((IMailboxReplicationProxyService)this).ExchangeVersionInformation(VersionInformation.MRSProxy, out serverVersion);
			base.ServerVersion = serverVersion;
			this.VerifyRequiredCapability(MRSProxyCapabilities.CachedMailboxSyncState);
		}

		// Token: 0x0600002E RID: 46 RVA: 0x0000307A File Offset: 0x0000127A
		public string UnpackString(byte[] data)
		{
			return CommonUtils.UnpackString(data, this.UseCompression);
		}

		// Token: 0x0600002F RID: 47 RVA: 0x00003088 File Offset: 0x00001288
		public byte[] PackString(string str)
		{
			return CommonUtils.PackString(str, this.UseCompression);
		}

		// Token: 0x06000030 RID: 48 RVA: 0x000030BC File Offset: 0x000012BC
		void IMailboxReplicationProxyService.ExchangeVersionInformation(VersionInformation clientVersion, out VersionInformation serverVersion)
		{
			VersionInformation sv = null;
			this.CallService(delegate()
			{
				this.Channel.ExchangeVersionInformation(clientVersion, out sv);
			});
			serverVersion = sv;
		}

		// Token: 0x06000031 RID: 49 RVA: 0x00003138 File Offset: 0x00001338
		ProxyServerInformation IMailboxReplicationProxyService.FindServerByDatabaseOrMailbox(string databaseId, Guid? physicalMailboxGuid, Guid? primaryMailboxGuid, byte[] tenantPartitionHintBytes)
		{
			ProxyServerInformation result = null;
			this.CallService(delegate()
			{
				result = this.Channel.FindServerByDatabaseOrMailbox(databaseId, physicalMailboxGuid, primaryMailboxGuid, tenantPartitionHintBytes);
			});
			return result;
		}

		// Token: 0x06000032 RID: 50 RVA: 0x000031BC File Offset: 0x000013BC
		IEnumerable<ContainerMailboxInformation> IMailboxReplicationProxyService.GetMailboxContainerMailboxes(Guid mdbGuid, Guid primaryMailboxGuid)
		{
			this.VerifyRequiredCapability(MRSProxyCapabilities.GetMailboxContainerMailboxes);
			IEnumerable<ContainerMailboxInformation> result = null;
			this.CallService(delegate()
			{
				result = this.Channel.GetMailboxContainerMailboxes(mdbGuid, primaryMailboxGuid);
			});
			return result;
		}

		// Token: 0x06000033 RID: 51 RVA: 0x0000322C File Offset: 0x0000142C
		bool IMailboxReplicationProxyService.ArePublicFoldersReadyForMigrationCompletion()
		{
			this.VerifyRequiredCapability(MRSProxyCapabilities.PublicFolderMailboxesMigrationSupport);
			bool result = false;
			this.CallService(delegate()
			{
				result = this.Channel.ArePublicFoldersReadyForMigrationCompletion();
			});
			return result;
		}

		// Token: 0x06000034 RID: 52 RVA: 0x00003290 File Offset: 0x00001490
		List<Guid> IMailboxReplicationProxyService.GetPublicFolderMailboxesExchangeGuids()
		{
			this.VerifyRequiredCapability(MRSProxyCapabilities.PublicFolderMailboxesMigrationSupport);
			List<Guid> result = null;
			this.CallService(delegate()
			{
				result = this.Channel.GetPublicFolderMailboxesExchangeGuids();
			});
			return result;
		}

		// Token: 0x06000035 RID: 53 RVA: 0x00003300 File Offset: 0x00001500
		long IMailboxReplicationProxyService.IDestinationMailbox_GetFxProxy(long mailboxHandle, out byte[] objectData)
		{
			long result = -1L;
			byte[] objData = null;
			this.CallService(delegate()
			{
				result = this.Channel.IDestinationMailbox_GetFxProxy(mailboxHandle, out objData);
			});
			objectData = objData;
			return result;
		}

		// Token: 0x06000036 RID: 54 RVA: 0x00003378 File Offset: 0x00001578
		PropProblemData[] IMailboxReplicationProxyService.IDestinationMailbox_SetProps(long mailboxHandle, PropValueData[] pva)
		{
			PropProblemData[] result = null;
			this.CallService(delegate()
			{
				this.Channel.IDestinationMailbox_SetProps(mailboxHandle, pva);
			});
			return result;
		}

		// Token: 0x06000037 RID: 55 RVA: 0x000033E8 File Offset: 0x000015E8
		long IMailboxReplicationProxyService.IDestinationMailbox_GetFxProxyPool(long mailboxHandle, byte[][] folderIds, out byte[] objectData)
		{
			long result = -1L;
			byte[] objData = null;
			this.CallService(delegate()
			{
				result = this.Channel.IDestinationMailbox_GetFxProxyPool(mailboxHandle, folderIds, out objData);
			});
			objectData = objData;
			return result;
		}

		// Token: 0x06000038 RID: 56 RVA: 0x0000346C File Offset: 0x0000166C
		void IMailboxReplicationProxyService.IDestinationMailbox_CreateFolder(long mailboxHandle, FolderRec sourceFolder, bool failIfExists)
		{
			this.CallServiceWithTimeout(this.LongOperationTimeout, delegate
			{
				this.Channel.IDestinationMailbox_CreateFolder(mailboxHandle, sourceFolder, failIfExists);
			});
		}

		// Token: 0x06000039 RID: 57 RVA: 0x000034E8 File Offset: 0x000016E8
		void IMailboxReplicationProxyService.IDestinationMailbox_CreateFolder2(long mailboxHandle, FolderRec sourceFolder, bool failIfExists, out byte[] newFolderId)
		{
			byte[] newFolderIdInt = null;
			this.CallServiceWithTimeout(this.LongOperationTimeout, delegate
			{
				this.Channel.IDestinationMailbox_CreateFolder2(mailboxHandle, sourceFolder, failIfExists, out newFolderIdInt);
			});
			newFolderId = newFolderIdInt;
		}

		// Token: 0x0600003A RID: 58 RVA: 0x00003574 File Offset: 0x00001774
		void IMailboxReplicationProxyService.IDestinationMailbox_CreateFolder3(long mailboxHandle, FolderRec sourceFolder, int createFolderFlags, out byte[] newFolderId)
		{
			byte[] newFolderIdInt = null;
			this.CallServiceWithTimeout(this.LongOperationTimeout, delegate
			{
				this.Channel.IDestinationMailbox_CreateFolder3(mailboxHandle, sourceFolder, createFolderFlags, out newFolderIdInt);
			});
			newFolderId = newFolderIdInt;
		}

		// Token: 0x0600003B RID: 59 RVA: 0x00003600 File Offset: 0x00001800
		void IMailboxReplicationProxyService.IDestinationMailbox_MoveFolder(long mailboxHandle, byte[] folderId, byte[] oldParentId, byte[] newParentId)
		{
			this.CallService(delegate()
			{
				this.Channel.IDestinationMailbox_MoveFolder(mailboxHandle, folderId, oldParentId, newParentId);
			});
		}

		// Token: 0x0600003C RID: 60 RVA: 0x00003670 File Offset: 0x00001870
		void IMailboxReplicationProxyService.IDestinationMailbox_DeleteFolder(long mailboxHandle, FolderRec folderRec)
		{
			this.CallService(delegate()
			{
				this.Channel.IDestinationMailbox_DeleteFolder(mailboxHandle, folderRec);
			});
		}

		// Token: 0x0600003D RID: 61 RVA: 0x000036D8 File Offset: 0x000018D8
		DataExportBatch IMailboxReplicationProxyService.IDestinationMailbox_LoadSyncState2(long mailboxHandle, byte[] key)
		{
			DataExportBatch result = null;
			this.CallServiceWithTimeout(this.LongOperationTimeout, delegate
			{
				result = this.Channel.IDestinationMailbox_LoadSyncState2(mailboxHandle, key);
			});
			return result;
		}

		// Token: 0x0600003E RID: 62 RVA: 0x00003758 File Offset: 0x00001958
		long IMailboxReplicationProxyService.IDestinationMailbox_SaveSyncState2(long mailboxHandle, byte[] key, DataExportBatch firstBatch)
		{
			long result = -1L;
			this.CallServiceWithTimeout(this.LongOperationTimeout, delegate
			{
				result = this.Channel.IDestinationMailbox_SaveSyncState2(mailboxHandle, key, firstBatch);
			});
			return result;
		}

		// Token: 0x0600003F RID: 63 RVA: 0x000037D0 File Offset: 0x000019D0
		void IMailboxReplicationProxyService.CloseHandle(long handle)
		{
			this.CallService(delegate()
			{
				this.Channel.CloseHandle(handle);
			});
		}

		// Token: 0x06000040 RID: 64 RVA: 0x0000382C File Offset: 0x00001A2C
		DataExportBatch IMailboxReplicationProxyService.DataExport_ExportData2(long dataExportHandle)
		{
			DataExportBatch result = null;
			this.CallService(delegate()
			{
				result = this.Channel.DataExport_ExportData2(dataExportHandle);
			});
			return result;
		}

		// Token: 0x06000041 RID: 65 RVA: 0x0000388C File Offset: 0x00001A8C
		void IMailboxReplicationProxyService.DataExport_CancelExport(long dataExportHandle)
		{
			this.CallService(delegate()
			{
				this.Channel.DataExport_CancelExport(dataExportHandle);
			});
		}

		// Token: 0x06000042 RID: 66 RVA: 0x00003928 File Offset: 0x00001B28
		void IMailboxReplicationProxyService.IDataImport_ImportBuffer(long dataImportHandle, int opcode, byte[] data)
		{
			this.CallServiceWithTimeout(this.LongOperationTimeout, delegate
			{
				try
				{
					this.Channel.IDataImport_ImportBuffer(dataImportHandle, opcode, data);
				}
				catch (TimeoutException ex)
				{
					MrsTracer.ProxyClient.Warning("Import buffer timed out. {0}", new object[]
					{
						ex
					});
					throw;
				}
			});
		}

		// Token: 0x06000043 RID: 67 RVA: 0x00003990 File Offset: 0x00001B90
		void IMailboxReplicationProxyService.IDataImport_Flush(long dataImportHandle)
		{
			this.CallServiceWithTimeout(this.LongOperationTimeout, delegate
			{
				this.Channel.IDataImport_Flush(dataImportHandle);
			});
		}

		// Token: 0x06000044 RID: 68 RVA: 0x000039F8 File Offset: 0x00001BF8
		FolderRec IMailboxReplicationProxyService.IFolder_GetFolderRec2(long folderHandle, int[] additionalPtagsToLoad)
		{
			FolderRec result = null;
			this.CallService(delegate()
			{
				result = this.Channel.IFolder_GetFolderRec2(folderHandle, additionalPtagsToLoad);
			});
			return result;
		}

		// Token: 0x06000045 RID: 69 RVA: 0x00003A74 File Offset: 0x00001C74
		FolderRec IMailboxReplicationProxyService.IFolder_GetFolderRec3(long folderHandle, int[] additionalPtagsToLoad, int flags)
		{
			FolderRec result = null;
			this.CallService(delegate()
			{
				result = this.Channel.IFolder_GetFolderRec3(folderHandle, additionalPtagsToLoad, flags);
			});
			return result;
		}

		// Token: 0x06000046 RID: 70 RVA: 0x00003AFC File Offset: 0x00001CFC
		List<MessageRec> IMailboxReplicationProxyService.IFolder_EnumerateMessagesPaged2(long folderHandle, EnumerateMessagesFlags emFlags, int[] additionalPtagsToLoad, out bool moreData)
		{
			List<MessageRec> result = null;
			bool moreDataInt = false;
			this.CallServiceWithTimeout(this.LongOperationTimeout, delegate
			{
				result = this.Channel.IFolder_EnumerateMessagesPaged2(folderHandle, emFlags, additionalPtagsToLoad, out moreDataInt);
			});
			moreData = moreDataInt;
			return result;
		}

		// Token: 0x06000047 RID: 71 RVA: 0x00003B8C File Offset: 0x00001D8C
		List<MessageRec> IMailboxReplicationProxyService.IFolder_EnumerateMessagesNextBatch(long folderHandle, out bool moreData)
		{
			List<MessageRec> result = null;
			bool moreDataInt = false;
			this.CallService(delegate()
			{
				result = this.Channel.IFolder_EnumerateMessagesNextBatch(folderHandle, out moreDataInt);
			});
			moreData = moreDataInt;
			return result;
		}

		// Token: 0x06000048 RID: 72 RVA: 0x00003C08 File Offset: 0x00001E08
		byte[] IMailboxReplicationProxyService.IFolder_GetSecurityDescriptor(long folderHandle, int secProp)
		{
			byte[] result = null;
			this.CallService(delegate()
			{
				result = this.Channel.IFolder_GetSecurityDescriptor(folderHandle, secProp);
			});
			return result;
		}

		// Token: 0x06000049 RID: 73 RVA: 0x00003C78 File Offset: 0x00001E78
		void IMailboxReplicationProxyService.IFolder_SetContentsRestriction(long folderHandle, RestrictionData restriction)
		{
			this.CallService(delegate()
			{
				this.Channel.IFolder_SetContentsRestriction(folderHandle, restriction);
			});
		}

		// Token: 0x0600004A RID: 74 RVA: 0x00003CE0 File Offset: 0x00001EE0
		PropValueData[] IMailboxReplicationProxyService.IFolder_GetProps(long folderHandle, int[] pta)
		{
			PropValueData[] result = null;
			this.CallService(delegate()
			{
				result = this.Channel.IFolder_GetProps(folderHandle, pta);
			});
			return result;
		}

		// Token: 0x0600004B RID: 75 RVA: 0x00003D54 File Offset: 0x00001F54
		PropProblemData[] IMailboxReplicationProxyService.IFolder_SetProps(long folderHandle, PropValueData[] pva)
		{
			PropProblemData[] result = null;
			this.CallService(delegate()
			{
				result = this.Channel.IFolder_SetProps(folderHandle, pva);
			});
			return result;
		}

		// Token: 0x0600004C RID: 76 RVA: 0x00003DC8 File Offset: 0x00001FC8
		RuleData[] IMailboxReplicationProxyService.IFolder_GetRules(long folderHandle, int[] extraProps)
		{
			RuleData[] result = null;
			this.CallServiceWithTimeout(this.LongOperationTimeout, delegate
			{
				result = this.Channel.IFolder_GetRules(folderHandle, extraProps);
			});
			return result;
		}

		// Token: 0x0600004D RID: 77 RVA: 0x00003E44 File Offset: 0x00002044
		PropValueData[][] IMailboxReplicationProxyService.IFolder_GetACL(long folderHandle, int secProp)
		{
			PropValueData[][] result = null;
			this.CallServiceWithTimeout(this.LongOperationTimeout, delegate
			{
				result = this.Channel.IFolder_GetACL(folderHandle, secProp);
			});
			return result;
		}

		// Token: 0x0600004E RID: 78 RVA: 0x00003EC0 File Offset: 0x000020C0
		PropValueData[][] IMailboxReplicationProxyService.IFolder_GetExtendedAcl(long folderHandle, int aclFlags)
		{
			PropValueData[][] result = null;
			this.CallServiceWithTimeout(this.LongOperationTimeout, delegate
			{
				result = this.Channel.IFolder_GetExtendedAcl(folderHandle, aclFlags);
			});
			return result;
		}

		// Token: 0x0600004F RID: 79 RVA: 0x00003F34 File Offset: 0x00002134
		void IMailboxReplicationProxyService.IFolder_DeleteMessages(long folderHandle, byte[][] entryIds)
		{
			this.CallServiceWithTimeout(this.LongOperationTimeout, delegate
			{
				this.Channel.IFolder_DeleteMessages(folderHandle, entryIds);
			});
		}

		// Token: 0x06000050 RID: 80 RVA: 0x00003FA0 File Offset: 0x000021A0
		PropValueData[] IMailboxReplicationProxyService.ISourceFolder_GetProps(long folderHandle, int[] pta)
		{
			PropValueData[] result = null;
			this.CallService(delegate()
			{
				result = this.Channel.ISourceFolder_GetProps(folderHandle, pta);
			});
			return result;
		}

		// Token: 0x06000051 RID: 81 RVA: 0x0000401C File Offset: 0x0000221C
		void IMailboxReplicationProxyService.IFolder_GetSearchCriteria(long folderHandle, out RestrictionData restrictionData, out byte[][] entryIDs, out int searchState)
		{
			RestrictionData rd = null;
			byte[][] eids = null;
			int ss = -1;
			this.CallService(delegate()
			{
				this.Channel.IFolder_GetSearchCriteria(folderHandle, out rd, out eids, out ss);
			});
			restrictionData = rd;
			entryIDs = eids;
			searchState = ss;
		}

		// Token: 0x06000052 RID: 82 RVA: 0x000040B8 File Offset: 0x000022B8
		List<MessageRec> IMailboxReplicationProxyService.IFolder_LookupMessages(long folderHandle, int ptagToLookup, byte[][] keysToLookup, int[] additionalPtagsToLoad)
		{
			List<MessageRec> result = null;
			this.CallServiceWithTimeout(this.LongOperationTimeout, delegate
			{
				result = this.Channel.IFolder_LookupMessages(folderHandle, ptagToLookup, keysToLookup, additionalPtagsToLoad);
			});
			return result;
		}

		// Token: 0x06000053 RID: 83 RVA: 0x00004148 File Offset: 0x00002348
		void IMailboxReplicationProxyService.ISourceFolder_GetSearchCriteria(long folderHandle, out RestrictionData restrictionData, out byte[][] entryIDs, out int searchState)
		{
			RestrictionData rd = null;
			byte[][] eids = null;
			int ss = -1;
			this.CallService(delegate()
			{
				this.Channel.ISourceFolder_GetSearchCriteria(folderHandle, out rd, out eids, out ss);
			});
			restrictionData = rd;
			entryIDs = eids;
			searchState = ss;
		}

		// Token: 0x06000054 RID: 84 RVA: 0x000041E4 File Offset: 0x000023E4
		DataExportBatch IMailboxReplicationProxyService.ISourceFolder_CopyTo(long folderHandle, int flags, int[] excludeTags, byte[] targetObjectData)
		{
			DataExportBatch result = null;
			this.CallService(delegate()
			{
				result = this.Channel.ISourceFolder_CopyTo(folderHandle, flags, excludeTags, targetObjectData);
			});
			return result;
		}

		// Token: 0x06000055 RID: 85 RVA: 0x0000426C File Offset: 0x0000246C
		DataExportBatch IMailboxReplicationProxyService.ISourceFolder_Export2(long folderHandle, int[] excludeTags, byte[] targetObjectData)
		{
			DataExportBatch result = null;
			this.CallService(delegate()
			{
				result = this.Channel.ISourceFolder_Export2(folderHandle, excludeTags, targetObjectData);
			});
			return result;
		}

		// Token: 0x06000056 RID: 86 RVA: 0x000042F4 File Offset: 0x000024F4
		DataExportBatch IMailboxReplicationProxyService.ISourceFolder_ExportMessages(long folderHandle, int flags, byte[][] entryIds, byte[] targetObjectData)
		{
			DataExportBatch result = null;
			this.CallService(delegate()
			{
				result = this.Channel.ISourceFolder_ExportMessages(folderHandle, flags, entryIds, targetObjectData);
			});
			return result;
		}

		// Token: 0x06000057 RID: 87 RVA: 0x00004378 File Offset: 0x00002578
		FolderChangesManifest IMailboxReplicationProxyService.ISourceFolder_EnumerateChanges(long folderHandle, bool catchup)
		{
			FolderChangesManifest result = null;
			this.CallServiceWithTimeout(this.LongOperationTimeout, delegate
			{
				result = this.Channel.ISourceFolder_EnumerateChanges(folderHandle, catchup);
			});
			return result;
		}

		// Token: 0x06000058 RID: 88 RVA: 0x000043F8 File Offset: 0x000025F8
		FolderChangesManifest IMailboxReplicationProxyService.ISourceFolder_EnumerateChanges2(long folderHandle, int flags, int maxChanges)
		{
			FolderChangesManifest result = null;
			this.CallServiceWithTimeout(this.LongOperationTimeout, delegate
			{
				result = this.Channel.ISourceFolder_EnumerateChanges2(folderHandle, flags, maxChanges);
			});
			return result;
		}

		// Token: 0x06000059 RID: 89 RVA: 0x00004478 File Offset: 0x00002678
		List<MessageRec> IMailboxReplicationProxyService.ISourceFolder_EnumerateMessagesPaged(long folderHandle, int maxPageSize)
		{
			List<MessageRec> result = null;
			this.CallServiceWithTimeout(this.LongOperationTimeout, delegate
			{
				result = this.Channel.ISourceFolder_EnumerateMessagesPaged(folderHandle, maxPageSize);
			});
			return result;
		}

		// Token: 0x0600005A RID: 90 RVA: 0x000044EC File Offset: 0x000026EC
		int IMailboxReplicationProxyService.ISourceFolder_GetEstimatedItemCount(long folderHandle)
		{
			int result = 0;
			this.CallService(delegate()
			{
				result = this.Channel.ISourceFolder_GetEstimatedItemCount(folderHandle);
			});
			return result;
		}

		// Token: 0x0600005B RID: 91 RVA: 0x00004558 File Offset: 0x00002758
		PropProblemData[] IMailboxReplicationProxyService.IDestinationFolder_SetProps(long folderHandle, PropValueData[] pva)
		{
			PropProblemData[] result = null;
			this.CallService(delegate()
			{
				result = this.Channel.IDestinationFolder_SetProps(folderHandle, pva);
			});
			return result;
		}

		// Token: 0x0600005C RID: 92 RVA: 0x000045D4 File Offset: 0x000027D4
		PropProblemData[] IMailboxReplicationProxyService.IDestinationFolder_SetSecurityDescriptor(long folderHandle, int secProp, byte[] sdData)
		{
			PropProblemData[] result = null;
			this.CallService(delegate()
			{
				result = this.Channel.IDestinationFolder_SetSecurityDescriptor(folderHandle, secProp, sdData);
			});
			return result;
		}

		// Token: 0x0600005D RID: 93 RVA: 0x0000465C File Offset: 0x0000285C
		bool IMailboxReplicationProxyService.IDestinationFolder_SetSearchCriteria(long folderHandle, RestrictionData restriction, byte[][] entryIDs, int searchFlags)
		{
			bool result = false;
			this.CallService(delegate()
			{
				result = this.Channel.IDestinationFolder_SetSearchCriteria(folderHandle, restriction, entryIDs, searchFlags);
			});
			return result;
		}

		// Token: 0x0600005E RID: 94 RVA: 0x000046E4 File Offset: 0x000028E4
		long IMailboxReplicationProxyService.IDestinationFolder_GetFxProxy2(long folderHandle, int flags, out byte[] objectData)
		{
			long result = -1L;
			byte[] objData = null;
			this.CallService(delegate()
			{
				result = this.Channel.IDestinationFolder_GetFxProxy2(folderHandle, flags, out objData);
			});
			objectData = objData;
			return result;
		}

		// Token: 0x0600005F RID: 95 RVA: 0x00004768 File Offset: 0x00002968
		long IMailboxReplicationProxyService.IDestinationFolder_GetFxProxy(long folderHandle, out byte[] objectData)
		{
			long result = -1L;
			byte[] objData = null;
			this.CallService(delegate()
			{
				result = this.Channel.IDestinationFolder_GetFxProxy(folderHandle, out objData);
			});
			objectData = objData;
			return result;
		}

		// Token: 0x06000060 RID: 96 RVA: 0x000047E0 File Offset: 0x000029E0
		void IMailboxReplicationProxyService.IDestinationFolder_DeleteMessages(long folderHandle, byte[][] entryIds)
		{
			this.CallServiceWithTimeout(this.LongOperationTimeout, delegate
			{
				this.Channel.IDestinationFolder_DeleteMessages(folderHandle, entryIds);
			});
		}

		// Token: 0x06000061 RID: 97 RVA: 0x0000484C File Offset: 0x00002A4C
		void IMailboxReplicationProxyService.IDestinationFolder_SetReadFlagsOnMessages(long folderHandle, int flags, byte[][] entryIds)
		{
			this.CallServiceWithTimeout(this.LongOperationTimeout, delegate
			{
				this.Channel.IDestinationFolder_SetReadFlagsOnMessages(folderHandle, flags, entryIds);
			});
		}

		// Token: 0x06000062 RID: 98 RVA: 0x000048C0 File Offset: 0x00002AC0
		void IMailboxReplicationProxyService.IDestinationFolder_SetMessageProps(long folderHandle, byte[] entryId, PropValueData[] propValues)
		{
			this.CallService(delegate()
			{
				this.Channel.IDestinationFolder_SetMessageProps(folderHandle, entryId, propValues);
			});
		}

		// Token: 0x06000063 RID: 99 RVA: 0x00004928 File Offset: 0x00002B28
		void IMailboxReplicationProxyService.IDestinationFolder_SetRules(long folderHandle, RuleData[] rules)
		{
			this.CallServiceWithTimeout(this.LongOperationTimeout, delegate
			{
				this.Channel.IDestinationFolder_SetRules(folderHandle, rules);
			});
		}

		// Token: 0x06000064 RID: 100 RVA: 0x00004994 File Offset: 0x00002B94
		void IMailboxReplicationProxyService.IDestinationFolder_SetACL(long folderHandle, int secProp, PropValueData[][] aclData)
		{
			this.CallServiceWithTimeout(this.LongOperationTimeout, delegate
			{
				this.Channel.IDestinationFolder_SetACL(folderHandle, secProp, aclData);
			});
		}

		// Token: 0x06000065 RID: 101 RVA: 0x00004A08 File Offset: 0x00002C08
		void IMailboxReplicationProxyService.IDestinationFolder_SetExtendedAcl(long folderHandle, int aclFlags, PropValueData[][] aclData)
		{
			this.CallServiceWithTimeout(this.LongOperationTimeout, delegate
			{
				this.Channel.IDestinationFolder_SetExtendedAcl(folderHandle, aclFlags, aclData);
			});
		}

		// Token: 0x06000066 RID: 102 RVA: 0x00004A84 File Offset: 0x00002C84
		Guid IMailboxReplicationProxyService.IDestinationFolder_LinkMailPublicFolder(long folderHandle, LinkMailPublicFolderFlags flags, byte[] objectId)
		{
			Guid result = Guid.Empty;
			this.CallService(delegate()
			{
				result = this.Channel.IDestinationFolder_LinkMailPublicFolder(folderHandle, flags, objectId);
			});
			return result;
		}

		// Token: 0x06000067 RID: 103 RVA: 0x00004B1C File Offset: 0x00002D1C
		long IMailboxReplicationProxyService.IMailbox_Config3(Guid primaryMailboxGuid, Guid physicalMailboxGuid, Guid mdbGuid, string mdbName, MailboxType mbxType, int proxyControlFlags)
		{
			long result = -1L;
			this.CallService(delegate()
			{
				result = this.Channel.IMailbox_Config3(primaryMailboxGuid, physicalMailboxGuid, mdbGuid, mdbName, mbxType, proxyControlFlags);
			});
			return result;
		}

		// Token: 0x06000068 RID: 104 RVA: 0x00004BE0 File Offset: 0x00002DE0
		long IMailboxReplicationProxyService.IMailbox_Config4(Guid primaryMailboxGuid, Guid physicalMailboxGuid, byte[] partitionHint, Guid mdbGuid, string mdbName, MailboxType mbxType, int proxyControlFlags, int localMailboxFlags)
		{
			long result = -1L;
			this.CallService(delegate()
			{
				result = this.Channel.IMailbox_Config4(primaryMailboxGuid, physicalMailboxGuid, partitionHint, mdbGuid, mdbName, mbxType, proxyControlFlags, localMailboxFlags);
			});
			return result;
		}

		// Token: 0x06000069 RID: 105 RVA: 0x00004CBC File Offset: 0x00002EBC
		long IMailboxReplicationProxyService.IMailbox_Config5(Guid reservationId, Guid primaryMailboxGuid, Guid physicalMailboxGuid, byte[] partitionHint, Guid mdbGuid, string mdbName, MailboxType mbxType, int proxyControlFlags, int localMailboxFlags)
		{
			long result = -1L;
			this.CallService(delegate()
			{
				result = this.Channel.IMailbox_Config5(reservationId, primaryMailboxGuid, physicalMailboxGuid, partitionHint, mdbGuid, mdbName, mbxType, proxyControlFlags, localMailboxFlags);
			});
			return result;
		}

		// Token: 0x0600006A RID: 106 RVA: 0x00004DA4 File Offset: 0x00002FA4
		long IMailboxReplicationProxyService.IMailbox_Config6(Guid reservationId, Guid primaryMailboxGuid, Guid physicalMailboxGuid, string filePath, byte[] partitionHint, Guid mdbGuid, string mdbName, MailboxType mbxType, int proxyControlFlags, int localMailboxFlags)
		{
			long result = -1L;
			this.CallService(delegate()
			{
				result = this.Channel.IMailbox_Config6(reservationId, primaryMailboxGuid, physicalMailboxGuid, filePath, partitionHint, mdbGuid, mdbName, mbxType, proxyControlFlags, localMailboxFlags);
			});
			return result;
		}

		// Token: 0x0600006B RID: 107 RVA: 0x00004E94 File Offset: 0x00003094
		long IMailboxReplicationProxyService.IMailbox_Config7(Guid reservationId, Guid primaryMailboxGuid, Guid physicalMailboxGuid, byte[] partitionHint, Guid mdbGuid, string mdbName, MailboxType mbxType, int proxyControlFlags, int localMailboxFlags, Guid? mailboxContainerGuid)
		{
			long result = -1L;
			this.CallService(delegate()
			{
				result = this.Channel.IMailbox_Config7(reservationId, primaryMailboxGuid, physicalMailboxGuid, partitionHint, mdbGuid, mdbName, mbxType, proxyControlFlags, localMailboxFlags, mailboxContainerGuid);
			});
			return result;
		}

		// Token: 0x0600006C RID: 108 RVA: 0x00004F3C File Offset: 0x0000313C
		void IMailboxReplicationProxyService.IMailbox_ConfigureProxyService(ProxyConfiguration configuration)
		{
			this.CallService(delegate()
			{
				this.Channel.IMailbox_ConfigureProxyService(configuration);
			});
		}

		// Token: 0x0600006D RID: 109 RVA: 0x00004FA8 File Offset: 0x000031A8
		void IMailboxReplicationProxyService.IMailbox_ConfigADConnection(long mailboxHandle, string domainControllerName, string userName, string userDomain, string userPassword)
		{
			this.CallService(delegate()
			{
				this.Channel.IMailbox_ConfigADConnection(mailboxHandle, domainControllerName, userName, userDomain, userPassword);
			});
		}

		// Token: 0x0600006E RID: 110 RVA: 0x00005028 File Offset: 0x00003228
		void IMailboxReplicationProxyService.IMailbox_ConfigEas(long mailboxHandle, string password, string address)
		{
			this.CallService(delegate()
			{
				this.Channel.IMailbox_ConfigEas(mailboxHandle, password, address);
			});
		}

		// Token: 0x0600006F RID: 111 RVA: 0x000050A4 File Offset: 0x000032A4
		void IMailboxReplicationProxyService.IMailbox_ConfigEas2(long mailboxHandle, string password, string address, Guid mailboxGuid, string remoteHostName)
		{
			this.CallService(delegate()
			{
				this.Channel.IMailbox_ConfigEas2(mailboxHandle, password, address, mailboxGuid, remoteHostName);
			});
		}

		// Token: 0x06000070 RID: 112 RVA: 0x0000511C File Offset: 0x0000331C
		void IMailboxReplicationProxyService.IMailbox_ConfigOlc(long mailboxHandle, OlcMailboxConfiguration config)
		{
			this.CallService(delegate()
			{
				this.Channel.IMailbox_ConfigOlc(mailboxHandle, config);
			});
		}

		// Token: 0x06000071 RID: 113 RVA: 0x0000517C File Offset: 0x0000337C
		void IMailboxReplicationProxyService.IMailbox_ConfigPreferredADConnection(long mailboxHandle, string preferredDomainControllerName)
		{
			this.CallService(delegate()
			{
				this.Channel.IMailbox_ConfigPreferredADConnection(mailboxHandle, preferredDomainControllerName);
			});
		}

		// Token: 0x06000072 RID: 114 RVA: 0x000051E4 File Offset: 0x000033E4
		void IMailboxReplicationProxyService.IMailbox_ConfigPst(long mailboxHandle, string filePath, int? contentCodePage)
		{
			this.CallService(delegate()
			{
				this.Channel.IMailbox_ConfigPst(mailboxHandle, filePath, contentCodePage);
			});
		}

		// Token: 0x06000073 RID: 115 RVA: 0x0000524C File Offset: 0x0000344C
		void IMailboxReplicationProxyService.IMailbox_ConfigRestore(long mailboxHandle, int restoreFlags)
		{
			this.CallService(delegate()
			{
				this.Channel.IMailbox_ConfigRestore(mailboxHandle, restoreFlags);
			});
		}

		// Token: 0x06000074 RID: 116 RVA: 0x000052AC File Offset: 0x000034AC
		MailboxInformation IMailboxReplicationProxyService.IMailbox_GetMailboxInformation(long mailboxHandle)
		{
			MailboxInformation result = null;
			this.CallService(delegate()
			{
				result = this.Channel.IMailbox_GetMailboxInformation(mailboxHandle);
			});
			return result;
		}

		// Token: 0x06000075 RID: 117 RVA: 0x00005324 File Offset: 0x00003524
		Guid IMailboxReplicationProxyService.IReservationManager_ReserveResources(Guid mailboxGuid, byte[] partitionHintBytes, Guid mdbGuid, int flags)
		{
			Guid result = Guid.Empty;
			this.CallService(delegate()
			{
				result = this.Channel.IReservationManager_ReserveResources(mailboxGuid, partitionHintBytes, mdbGuid, flags);
			});
			return result;
		}

		// Token: 0x06000076 RID: 118 RVA: 0x000053A0 File Offset: 0x000035A0
		void IMailboxReplicationProxyService.IReservationManager_ReleaseResources(Guid reservationId)
		{
			this.CallService(delegate()
			{
				this.Channel.IReservationManager_ReleaseResources(reservationId);
			});
		}

		// Token: 0x06000077 RID: 119 RVA: 0x00005408 File Offset: 0x00003608
		int IMailboxReplicationProxyService.IMailbox_ReserveResources(Guid reservationId, Guid resourceId, int reservationType)
		{
			int status = 0;
			this.CallService(delegate()
			{
				status = this.Channel.IMailbox_ReserveResources(reservationId, resourceId, reservationType);
			});
			return status;
		}

		// Token: 0x06000078 RID: 120 RVA: 0x00005478 File Offset: 0x00003678
		void IMailboxReplicationProxyService.IMailbox_Connect(long mailboxHandle)
		{
			this.CallService(delegate()
			{
				this.Channel.IMailbox_Connect(mailboxHandle);
			});
		}

		// Token: 0x06000079 RID: 121 RVA: 0x000054D4 File Offset: 0x000036D4
		void IMailboxReplicationProxyService.IMailbox_Connect2(long mailboxHandle, int connectFlags)
		{
			this.CallService(delegate()
			{
				this.Channel.IMailbox_Connect2(mailboxHandle, connectFlags);
			});
		}

		// Token: 0x0600007A RID: 122 RVA: 0x00005530 File Offset: 0x00003730
		void IMailboxReplicationProxyService.IMailbox_Disconnect(long mailboxHandle)
		{
			MailboxReplicationProxyClient.KeepAlivePinger.Remove(this.RequestContext.Id);
			this.CallService(delegate()
			{
				this.Channel.IMailbox_Disconnect(mailboxHandle);
			});
		}

		// Token: 0x0600007B RID: 123 RVA: 0x000055A0 File Offset: 0x000037A0
		void IMailboxReplicationProxyService.IMailbox_ConfigMailboxOptions(long mailboxHandle, int options)
		{
			this.CallService(delegate()
			{
				this.Channel.IMailbox_ConfigMailboxOptions(mailboxHandle, options);
			});
		}

		// Token: 0x0600007C RID: 124 RVA: 0x00005600 File Offset: 0x00003800
		MailboxServerInformation IMailboxReplicationProxyService.IMailbox_GetMailboxServerInformation(long mailboxHandle)
		{
			MailboxServerInformation result = null;
			this.CallService(delegate()
			{
				result = this.Channel.IMailbox_GetMailboxServerInformation(mailboxHandle);
			});
			return result;
		}

		// Token: 0x0600007D RID: 125 RVA: 0x00005668 File Offset: 0x00003868
		void IMailboxReplicationProxyService.IMailbox_SetOtherSideVersion(long mailboxHandle, VersionInformation otherSideInfo)
		{
			this.CallService(delegate()
			{
				this.Channel.IMailbox_SetOtherSideVersion(mailboxHandle, otherSideInfo);
			});
		}

		// Token: 0x0600007E RID: 126 RVA: 0x000056D0 File Offset: 0x000038D0
		void IMailboxReplicationProxyService.IMailbox_SetInTransitStatus(long mailboxHandle, int status, out bool onlineMoveSupported)
		{
			bool oms = false;
			this.CallService(delegate()
			{
				this.Channel.IMailbox_SetInTransitStatus(mailboxHandle, status, out oms);
			});
			onlineMoveSupported = oms;
		}

		// Token: 0x0600007F RID: 127 RVA: 0x0000573C File Offset: 0x0000393C
		void IMailboxReplicationProxyService.IMailbox_SeedMBICache(long mailboxHandle)
		{
			this.CallService(delegate()
			{
				this.Channel.IMailbox_SeedMBICache(mailboxHandle);
			});
		}

		// Token: 0x06000080 RID: 128 RVA: 0x000057A8 File Offset: 0x000039A8
		List<FolderRec> IMailboxReplicationProxyService.IMailbox_EnumerateFolderHierarchyPaged2(long mailboxHandle, EnumerateFolderHierarchyFlags flags, int[] additionalPtagsToLoad, out bool moreData)
		{
			List<FolderRec> result = null;
			bool moreDataInt = false;
			this.CallServiceWithTimeout(this.LongOperationTimeout, delegate
			{
				result = this.Channel.IMailbox_EnumerateFolderHierarchyPaged2(mailboxHandle, flags, additionalPtagsToLoad, out moreDataInt);
			});
			moreData = moreDataInt;
			return result;
		}

		// Token: 0x06000081 RID: 129 RVA: 0x00005838 File Offset: 0x00003A38
		List<FolderRec> IMailboxReplicationProxyService.IMailbox_EnumerateFolderHierarchyNextBatch(long mailboxHandle, out bool moreData)
		{
			List<FolderRec> result = null;
			bool moreDataInt = false;
			this.CallService(delegate()
			{
				result = this.Channel.IMailbox_EnumerateFolderHierarchyNextBatch(mailboxHandle, out moreDataInt);
			});
			moreData = moreDataInt;
			return result;
		}

		// Token: 0x06000082 RID: 130 RVA: 0x000058B4 File Offset: 0x00003AB4
		List<WellKnownFolder> IMailboxReplicationProxyService.IMailbox_DiscoverWellKnownFolders(long mailboxHandle, int flags)
		{
			List<WellKnownFolder> result = null;
			this.CallService(delegate()
			{
				result = this.Channel.IMailbox_DiscoverWellKnownFolders(mailboxHandle, flags);
			});
			return result;
		}

		// Token: 0x06000083 RID: 131 RVA: 0x00005928 File Offset: 0x00003B28
		bool IMailboxReplicationProxyService.IMailbox_IsMailboxCapabilitySupported(long mailboxHandle, MailboxCapabilities capability)
		{
			bool result = false;
			this.CallService(delegate()
			{
				result = this.Channel.IMailbox_IsMailboxCapabilitySupported(mailboxHandle, capability);
			});
			return result;
		}

		// Token: 0x06000084 RID: 132 RVA: 0x0000599C File Offset: 0x00003B9C
		bool IMailboxReplicationProxyService.IMailbox_IsMailboxCapabilitySupported2(long mailboxHandle, int capability)
		{
			bool result = false;
			this.CallService(delegate()
			{
				result = this.Channel.IMailbox_IsMailboxCapabilitySupported2(mailboxHandle, capability);
			});
			return result;
		}

		// Token: 0x06000085 RID: 133 RVA: 0x00005A0C File Offset: 0x00003C0C
		void IMailboxReplicationProxyService.IMailbox_DeleteMailbox(long mailboxHandle, int flags)
		{
			this.CallService(delegate()
			{
				this.Channel.IMailbox_DeleteMailbox(mailboxHandle, flags);
			});
		}

		// Token: 0x06000086 RID: 134 RVA: 0x00005A74 File Offset: 0x00003C74
		NamedPropData[] IMailboxReplicationProxyService.IMailbox_GetNamesFromIDs(long mailboxHandle, int[] pta)
		{
			NamedPropData[] result = null;
			this.CallService(delegate()
			{
				result = this.Channel.IMailbox_GetNamesFromIDs(mailboxHandle, pta);
			});
			return result;
		}

		// Token: 0x06000087 RID: 135 RVA: 0x00005AF0 File Offset: 0x00003CF0
		int[] IMailboxReplicationProxyService.IMailbox_GetIDsFromNames(long mailboxHandle, bool createIfNotExists, NamedPropData[] npa)
		{
			int[] result = null;
			this.CallService(delegate()
			{
				result = this.Channel.IMailbox_GetIDsFromNames(mailboxHandle, createIfNotExists, npa);
			});
			return result;
		}

		// Token: 0x06000088 RID: 136 RVA: 0x00005B6C File Offset: 0x00003D6C
		byte[] IMailboxReplicationProxyService.IMailbox_GetSessionSpecificEntryId(long mailboxHandle, byte[] entryId)
		{
			byte[] result = null;
			this.CallService(delegate()
			{
				result = this.Channel.IMailbox_GetSessionSpecificEntryId(mailboxHandle, entryId);
			});
			return result;
		}

		// Token: 0x06000089 RID: 137 RVA: 0x00005BE0 File Offset: 0x00003DE0
		bool IMailboxReplicationProxyService.IMailbox_UpdateRemoteHostName(long mailboxHandle, string value)
		{
			bool result = false;
			this.CallService(delegate()
			{
				result = this.Channel.IMailbox_UpdateRemoteHostName(mailboxHandle, value);
			});
			return result;
		}

		// Token: 0x0600008A RID: 138 RVA: 0x00005C50 File Offset: 0x00003E50
		string IMailboxReplicationProxyService.IMailbox_GetADUser(long mailboxHandle)
		{
			string result = null;
			this.CallService(delegate()
			{
				result = this.Channel.IMailbox_GetADUser(mailboxHandle);
			});
			return result;
		}

		// Token: 0x0600008B RID: 139 RVA: 0x00005CC8 File Offset: 0x00003EC8
		void IMailboxReplicationProxyService.IMailbox_UpdateMovedMailbox(long mailboxHandle, UpdateMovedMailboxOperation op, string remoteRecipientData, string domainController, out string entries)
		{
			entries = null;
			string tempEntries = null;
			try
			{
				this.CallServiceWithTimeout(this.LongOperationTimeout, delegate
				{
					this.Channel.IMailbox_UpdateMovedMailbox(mailboxHandle, op, remoteRecipientData, domainController, out tempEntries);
				});
			}
			finally
			{
				entries = tempEntries;
			}
		}

		// Token: 0x0600008C RID: 140 RVA: 0x00005DA4 File Offset: 0x00003FA4
		void IMailboxReplicationProxyService.IMailbox_UpdateMovedMailbox2(long mailboxHandle, UpdateMovedMailboxOperation op, string remoteRecipientData, string domainController, out string entries, Guid newDatabaseGuid, Guid newArchiveDatabaseGuid, string archiveDomain, int archiveStatus)
		{
			entries = null;
			string tempEntries = null;
			try
			{
				this.CallServiceWithTimeout(this.LongOperationTimeout, delegate
				{
					this.Channel.IMailbox_UpdateMovedMailbox2(mailboxHandle, op, remoteRecipientData, domainController, out tempEntries, newDatabaseGuid, newArchiveDatabaseGuid, archiveDomain, archiveStatus);
				});
			}
			finally
			{
				entries = tempEntries;
			}
		}

		// Token: 0x0600008D RID: 141 RVA: 0x00005EA8 File Offset: 0x000040A8
		void IMailboxReplicationProxyService.IMailbox_UpdateMovedMailbox3(long mailboxHandle, UpdateMovedMailboxOperation op, string remoteRecipientData, string domainController, out string entries, Guid newDatabaseGuid, Guid newArchiveDatabaseGuid, string archiveDomain, int archiveStatus, int updateMovedMailboxFlags)
		{
			entries = null;
			string tempEntries = null;
			try
			{
				this.CallServiceWithTimeout(this.LongOperationTimeout, delegate
				{
					this.Channel.IMailbox_UpdateMovedMailbox3(mailboxHandle, op, remoteRecipientData, domainController, out tempEntries, newDatabaseGuid, newArchiveDatabaseGuid, archiveDomain, archiveStatus, updateMovedMailboxFlags);
				});
			}
			finally
			{
				entries = tempEntries;
			}
		}

		// Token: 0x0600008E RID: 142 RVA: 0x00005FC0 File Offset: 0x000041C0
		void IMailboxReplicationProxyService.IMailbox_UpdateMovedMailbox4(long mailboxHandle, UpdateMovedMailboxOperation op, string remoteRecipientData, string domainController, out string entries, Guid newDatabaseGuid, Guid newArchiveDatabaseGuid, string archiveDomain, int archiveStatus, int updateMovedMailboxFlags, Guid? newMailboxContainerGuid, byte[] newUnifiedMailboxIdData)
		{
			entries = null;
			string tempEntries = null;
			try
			{
				this.CallServiceWithTimeout(this.LongOperationTimeout, delegate
				{
					this.Channel.IMailbox_UpdateMovedMailbox4(mailboxHandle, op, remoteRecipientData, domainController, out tempEntries, newDatabaseGuid, newArchiveDatabaseGuid, archiveDomain, archiveStatus, updateMovedMailboxFlags, newMailboxContainerGuid, newUnifiedMailboxIdData);
				});
			}
			finally
			{
				entries = tempEntries;
			}
		}

		// Token: 0x0600008F RID: 143 RVA: 0x000060A4 File Offset: 0x000042A4
		MappedPrincipal[] IMailboxReplicationProxyService.IMailbox_GetPrincipalsFromMailboxGuids(long mailboxHandle, Guid[] mailboxGuids)
		{
			MappedPrincipal[] result = null;
			this.CallService(delegate()
			{
				result = this.Channel.IMailbox_GetPrincipalsFromMailboxGuids(mailboxHandle, mailboxGuids);
			});
			return result;
		}

		// Token: 0x06000090 RID: 144 RVA: 0x00006118 File Offset: 0x00004318
		Guid[] IMailboxReplicationProxyService.IMailbox_GetMailboxGuidsFromPrincipals(long mailboxHandle, MappedPrincipal[] principals)
		{
			Guid[] result = null;
			this.CallService(delegate()
			{
				result = this.Channel.IMailbox_GetMailboxGuidsFromPrincipals(mailboxHandle, principals);
			});
			return result;
		}

		// Token: 0x06000091 RID: 145 RVA: 0x0000618C File Offset: 0x0000438C
		MappedPrincipal[] IMailboxReplicationProxyService.IMailbox_ResolvePrincipals(long mailboxHandle, MappedPrincipal[] principals)
		{
			MappedPrincipal[] result = null;
			this.CallService(delegate()
			{
				result = this.Channel.IMailbox_ResolvePrincipals(mailboxHandle, principals);
			});
			return result;
		}

		// Token: 0x06000092 RID: 146 RVA: 0x000061FC File Offset: 0x000043FC
		byte[] IMailboxReplicationProxyService.IMailbox_GetMailboxSecurityDescriptor(long mailboxHandle)
		{
			byte[] result = null;
			this.CallService(delegate()
			{
				result = this.Channel.IMailbox_GetMailboxSecurityDescriptor(mailboxHandle);
			});
			return result;
		}

		// Token: 0x06000093 RID: 147 RVA: 0x00006264 File Offset: 0x00004464
		byte[] IMailboxReplicationProxyService.IMailbox_GetUserSecurityDescriptor(long mailboxHandle)
		{
			byte[] result = null;
			this.CallService(delegate()
			{
				result = this.Channel.IMailbox_GetUserSecurityDescriptor(mailboxHandle);
			});
			return result;
		}

		// Token: 0x06000094 RID: 148 RVA: 0x000062D0 File Offset: 0x000044D0
		void IMailboxReplicationProxyService.IMailbox_AddMoveHistoryEntry(long mailboxHandle, string mheData, int maxMoveHistoryLength)
		{
			this.CallService(delegate()
			{
				this.Channel.IMailbox_AddMoveHistoryEntry(mailboxHandle, mheData, maxMoveHistoryLength);
			});
		}

		// Token: 0x06000095 RID: 149 RVA: 0x00006334 File Offset: 0x00004534
		void IMailboxReplicationProxyService.IMailbox_CheckServerHealth(long mailboxHandle)
		{
			this.CallService(delegate()
			{
				this.Channel.IMailbox_CheckServerHealth(mailboxHandle);
			});
		}

		// Token: 0x06000096 RID: 150 RVA: 0x00006390 File Offset: 0x00004590
		ServerHealthStatus IMailboxReplicationProxyService.IMailbox_CheckServerHealth2(long mailboxHandle)
		{
			ServerHealthStatus serverHealthStatus = new ServerHealthStatus(ServerHealthState.Healthy);
			this.CallService(delegate()
			{
				serverHealthStatus = this.Channel.IMailbox_CheckServerHealth2(mailboxHandle);
			});
			return serverHealthStatus;
		}

		// Token: 0x06000097 RID: 151 RVA: 0x00006404 File Offset: 0x00004604
		PropValueData[] IMailboxReplicationProxyService.IMailbox_GetProps(long mailboxHandle, int[] ptags)
		{
			PropValueData[] result = null;
			this.CallService(delegate()
			{
				result = this.Channel.IMailbox_GetProps(mailboxHandle, ptags);
			});
			return result;
		}

		// Token: 0x06000098 RID: 152 RVA: 0x00006478 File Offset: 0x00004678
		byte[] IMailboxReplicationProxyService.IMailbox_GetReceiveFolderEntryId(long mailboxHandle, string msgClass)
		{
			byte[] result = null;
			this.CallService(delegate()
			{
				result = this.Channel.IMailbox_GetReceiveFolderEntryId(mailboxHandle, msgClass);
			});
			return result;
		}

		// Token: 0x06000099 RID: 153 RVA: 0x000064EC File Offset: 0x000046EC
		SessionStatistics IMailboxReplicationProxyService.IMailbox_GetSessionStatistics(long mailboxHandle, int statisticsTypes)
		{
			SessionStatistics result = null;
			this.CallService(delegate()
			{
				result = this.Channel.IMailbox_GetSessionStatistics(mailboxHandle, statisticsTypes);
			});
			return result;
		}

		// Token: 0x0600009A RID: 154 RVA: 0x0000655C File Offset: 0x0000475C
		byte[] IMailboxReplicationProxyService.ISourceMailbox_GetMailboxBasicInfo(long mailboxHandle)
		{
			byte[] result = null;
			this.CallService(delegate()
			{
				result = this.Channel.ISourceMailbox_GetMailboxBasicInfo(mailboxHandle);
			});
			return result;
		}

		// Token: 0x0600009B RID: 155 RVA: 0x000065C8 File Offset: 0x000047C8
		byte[] IMailboxReplicationProxyService.ISourceMailbox_GetMailboxBasicInfo2(long mailboxHandle, int signatureFlags)
		{
			byte[] result = null;
			this.CallService(delegate()
			{
				result = this.Channel.ISourceMailbox_GetMailboxBasicInfo2(mailboxHandle, signatureFlags);
			});
			return result;
		}

		// Token: 0x0600009C RID: 156 RVA: 0x0000663C File Offset: 0x0000483C
		long IMailboxReplicationProxyService.ISourceMailbox_GetFolder(long mailboxHandle, byte[] entryId)
		{
			long result = -1L;
			this.CallService(delegate()
			{
				result = this.Channel.ISourceMailbox_GetFolder(mailboxHandle, entryId);
			});
			return result;
		}

		// Token: 0x0600009D RID: 157 RVA: 0x000066B0 File Offset: 0x000048B0
		PropValueData[] IMailboxReplicationProxyService.ISourceMailbox_GetProps(long mailboxHandle, int[] ptags)
		{
			PropValueData[] result = null;
			this.CallService(delegate()
			{
				result = this.Channel.ISourceMailbox_GetProps(mailboxHandle, ptags);
			});
			return result;
		}

		// Token: 0x0600009E RID: 158 RVA: 0x0000672C File Offset: 0x0000492C
		DataExportBatch IMailboxReplicationProxyService.ISourceMailbox_Export2(long mailboxHandle, int[] excludeProps, byte[] targetObjectData)
		{
			DataExportBatch result = null;
			this.CallService(delegate()
			{
				result = this.Channel.ISourceMailbox_Export2(mailboxHandle, excludeProps, targetObjectData);
			});
			return result;
		}

		// Token: 0x0600009F RID: 159 RVA: 0x000067A8 File Offset: 0x000049A8
		MailboxChangesManifest IMailboxReplicationProxyService.ISourceMailbox_EnumerateHierarchyChanges(long mailboxHandle, bool catchup)
		{
			MailboxChangesManifest result = null;
			this.CallServiceWithTimeout(this.LongOperationTimeout, delegate
			{
				result = this.Channel.ISourceMailbox_EnumerateHierarchyChanges(mailboxHandle, catchup);
			});
			return result;
		}

		// Token: 0x060000A0 RID: 160 RVA: 0x00006828 File Offset: 0x00004A28
		MailboxChangesManifest IMailboxReplicationProxyService.ISourceMailbox_EnumerateHierarchyChanges2(long mailboxHandle, int flags, int maxChanges)
		{
			MailboxChangesManifest result = null;
			this.CallServiceWithTimeout(this.LongOperationTimeout, delegate
			{
				result = this.Channel.ISourceMailbox_EnumerateHierarchyChanges2(mailboxHandle, flags, maxChanges);
			});
			return result;
		}

		// Token: 0x060000A1 RID: 161 RVA: 0x000068A4 File Offset: 0x00004AA4
		DataExportBatch IMailboxReplicationProxyService.ISourceMailbox_GetMailboxSyncState(long mailboxHandle)
		{
			DataExportBatch result = null;
			this.CallService(delegate()
			{
				result = this.Channel.ISourceMailbox_GetMailboxSyncState(mailboxHandle);
			});
			return result;
		}

		// Token: 0x060000A2 RID: 162 RVA: 0x00006910 File Offset: 0x00004B10
		long IMailboxReplicationProxyService.ISourceMailbox_SetMailboxSyncState(long mailboxHandle, DataExportBatch firstBatch)
		{
			long result = -1L;
			this.CallService(delegate()
			{
				result = this.Channel.ISourceMailbox_SetMailboxSyncState(mailboxHandle, firstBatch);
			});
			return result;
		}

		// Token: 0x060000A3 RID: 163 RVA: 0x0000698C File Offset: 0x00004B8C
		DataExportBatch IMailboxReplicationProxyService.ISourceMailbox_ExportMessageBatch2(long mailboxHandle, List<MessageRec> messages, byte[] targetObjectData)
		{
			DataExportBatch result = null;
			this.CallService(delegate()
			{
				result = this.Channel.ISourceMailbox_ExportMessageBatch2(mailboxHandle, messages, targetObjectData);
			});
			return result;
		}

		// Token: 0x060000A4 RID: 164 RVA: 0x00006A18 File Offset: 0x00004C18
		DataExportBatch IMailboxReplicationProxyService.ISourceMailbox_ExportMessages(long mailboxHandle, List<MessageRec> messages, int flags, int[] excludeProps, byte[] targetObjectData)
		{
			DataExportBatch result = null;
			this.CallService(delegate()
			{
				result = this.Channel.ISourceMailbox_ExportMessages(mailboxHandle, messages, flags, excludeProps, targetObjectData);
			});
			return result;
		}

		// Token: 0x060000A5 RID: 165 RVA: 0x00006AD4 File Offset: 0x00004CD4
		DataExportBatch IMailboxReplicationProxyService.ISourceMailbox_ExportFolders(long mailboxHandle, List<byte[]> folderIds, int exportFoldersDataToCopyFlags, int folderRecFlags, int[] additionalFolderRecProps, int copyPropertiesFlags, int[] excludeProps, int extendedAclFlags)
		{
			DataExportBatch result = null;
			this.CallService(delegate()
			{
				result = this.Channel.ISourceMailbox_ExportFolders(mailboxHandle, folderIds, exportFoldersDataToCopyFlags, folderRecFlags, additionalFolderRecProps, copyPropertiesFlags, excludeProps, extendedAclFlags);
			});
			return result;
		}

		// Token: 0x060000A6 RID: 166 RVA: 0x00006B78 File Offset: 0x00004D78
		List<ReplayActionResult> IMailboxReplicationProxyService.ISourceMailbox_ReplayActions(long mailboxHandle, List<ReplayAction> actions)
		{
			List<ReplayActionResult> result = null;
			this.CallService(delegate()
			{
				result = this.Channel.ISourceMailbox_ReplayActions(mailboxHandle, actions);
			});
			return result;
		}

		// Token: 0x060000A7 RID: 167 RVA: 0x00006BEC File Offset: 0x00004DEC
		public List<ItemPropertiesBase> ISourceMailbox_GetMailboxSettings(long mailboxHandle, int flags)
		{
			List<ItemPropertiesBase> result = null;
			this.CallService(delegate()
			{
				result = this.Channel.ISourceMailbox_GetMailboxSettings(mailboxHandle, flags);
			});
			return result;
		}

		// Token: 0x060000A8 RID: 168 RVA: 0x00006C60 File Offset: 0x00004E60
		Guid IMailboxReplicationProxyService.IMailbox_StartIsInteg(long mailboxHandle, List<uint> mailboxCorruptionTypes)
		{
			Guid result = Guid.Empty;
			this.CallService(delegate()
			{
				result = this.Channel.IMailbox_StartIsInteg(mailboxHandle, mailboxCorruptionTypes);
			});
			return result;
		}

		// Token: 0x060000A9 RID: 169 RVA: 0x00006CD8 File Offset: 0x00004ED8
		List<StoreIntegrityCheckJob> IMailboxReplicationProxyService.IMailbox_QueryIsInteg(long mailboxHandle, Guid isIntegRequestGuid)
		{
			List<StoreIntegrityCheckJob> jobs = null;
			this.CallService(delegate()
			{
				jobs = this.Channel.IMailbox_QueryIsInteg(mailboxHandle, isIntegRequestGuid);
			});
			return jobs;
		}

		// Token: 0x060000AA RID: 170 RVA: 0x00006D48 File Offset: 0x00004F48
		bool IMailboxReplicationProxyService.IDestinationMailbox_MailboxExists(long mailboxHandle)
		{
			bool result = false;
			this.CallService(delegate()
			{
				result = this.Channel.IDestinationMailbox_MailboxExists(mailboxHandle);
			});
			return result;
		}

		// Token: 0x060000AB RID: 171 RVA: 0x00006DB4 File Offset: 0x00004FB4
		CreateMailboxResult IMailboxReplicationProxyService.IDestinationMailbox_CreateMailbox(long mailboxHandle, byte[] mailboxData)
		{
			CreateMailboxResult result = CreateMailboxResult.Success;
			this.CallService(delegate()
			{
				result = this.Channel.IDestinationMailbox_CreateMailbox(mailboxHandle, mailboxData);
			});
			return result;
		}

		// Token: 0x060000AC RID: 172 RVA: 0x00006E30 File Offset: 0x00005030
		CreateMailboxResult IMailboxReplicationProxyService.IDestinationMailbox_CreateMailbox2(long mailboxHandle, byte[] mailboxData, int sourceSignatureFlags)
		{
			CreateMailboxResult result = CreateMailboxResult.Success;
			this.CallService(delegate()
			{
				result = this.Channel.IDestinationMailbox_CreateMailbox2(mailboxHandle, mailboxData, sourceSignatureFlags);
			});
			return result;
		}

		// Token: 0x060000AD RID: 173 RVA: 0x00006EA4 File Offset: 0x000050A4
		void IMailboxReplicationProxyService.IDestinationMailbox_ProcessMailboxSignature(long mailboxHandle, byte[] mailboxData)
		{
			this.CallService(delegate()
			{
				this.Channel.IDestinationMailbox_ProcessMailboxSignature(mailboxHandle, mailboxData);
			});
		}

		// Token: 0x060000AE RID: 174 RVA: 0x00006F0C File Offset: 0x0000510C
		long IMailboxReplicationProxyService.IDestinationMailbox_GetFolder(long mailboxHandle, byte[] entryId)
		{
			long result = -1L;
			this.CallService(delegate()
			{
				result = this.Channel.IDestinationMailbox_GetFolder(mailboxHandle, entryId);
			});
			return result;
		}

		// Token: 0x060000AF RID: 175 RVA: 0x00006F7C File Offset: 0x0000517C
		void IMailboxReplicationProxyService.IDestinationMailbox_SetMailboxSecurityDescriptor(long mailboxHandle, byte[] sdData)
		{
			this.CallService(delegate()
			{
				this.Channel.IDestinationMailbox_SetMailboxSecurityDescriptor(mailboxHandle, sdData);
			});
		}

		// Token: 0x060000B0 RID: 176 RVA: 0x00006FDC File Offset: 0x000051DC
		void IMailboxReplicationProxyService.IDestinationMailbox_SetUserSecurityDescriptor(long mailboxHandle, byte[] sdData)
		{
			this.CallService(delegate()
			{
				this.Channel.IDestinationMailbox_SetUserSecurityDescriptor(mailboxHandle, sdData);
			});
		}

		// Token: 0x060000B1 RID: 177 RVA: 0x0000703C File Offset: 0x0000523C
		void IMailboxReplicationProxyService.IDestinationMailbox_PreFinalSyncDataProcessing(long mailboxHandle, int? sourceMailboxVersion)
		{
			this.CallServiceWithTimeout(this.LongOperationTimeout, delegate
			{
				this.Channel.IDestinationMailbox_PreFinalSyncDataProcessing(mailboxHandle, sourceMailboxVersion);
			});
		}

		// Token: 0x060000B2 RID: 178 RVA: 0x000070B0 File Offset: 0x000052B0
		int IMailboxReplicationProxyService.IDestinationMailbox_CheckDataGuarantee(long mailboxHandle, DateTime commitTimestamp, out byte[] failureReasonData)
		{
			int result = -1;
			byte[] frd = null;
			this.CallService(delegate()
			{
				result = this.Channel.IDestinationMailbox_CheckDataGuarantee(mailboxHandle, commitTimestamp, out frd);
			});
			failureReasonData = frd;
			return result;
		}

		// Token: 0x060000B3 RID: 179 RVA: 0x00007128 File Offset: 0x00005328
		void IMailboxReplicationProxyService.IDestinationMailbox_ForceLogRoll(long mailboxHandle)
		{
			this.CallService(delegate()
			{
				this.Channel.IDestinationMailbox_ForceLogRoll(mailboxHandle);
			});
		}

		// Token: 0x060000B4 RID: 180 RVA: 0x00007190 File Offset: 0x00005390
		List<ReplayAction> IMailboxReplicationProxyService.IDestinationMailbox_GetActions(long mailboxHandle, string replaySyncState, int maxNumberOfActions)
		{
			List<ReplayAction> result = null;
			this.CallService(delegate()
			{
				result = this.Channel.IDestinationMailbox_GetActions(mailboxHandle, replaySyncState, maxNumberOfActions);
			});
			return result;
		}

		// Token: 0x060000B5 RID: 181 RVA: 0x00007204 File Offset: 0x00005404
		void IMailboxReplicationProxyService.IDestinationMailbox_SetMailboxSettings(long mailboxHandle, ItemPropertiesBase item)
		{
			this.CallServiceWithTimeout(this.LongOperationTimeout, delegate
			{
				this.Channel.IDestinationMailbox_SetMailboxSettings(mailboxHandle, item);
			});
		}

		// Token: 0x060000B6 RID: 182 RVA: 0x00007278 File Offset: 0x00005478
		MigrationAccount[] IMailboxReplicationProxyService.SelectAccountsToMigrate(long maximumAccounts, long? maximumTotalSize, int? constraintId)
		{
			MigrationAccount[] result = null;
			this.CallService(delegate()
			{
				result = this.Channel.SelectAccountsToMigrate(maximumAccounts, maximumTotalSize, constraintId);
			});
			return result;
		}

		// Token: 0x060000B7 RID: 183 RVA: 0x000072C6 File Offset: 0x000054C6
		protected override void InternalDispose(bool disposing)
		{
			base.InternalDispose(disposing);
			if (disposing)
			{
				this.RequestContext.Unregister();
			}
		}

		// Token: 0x060000B8 RID: 184 RVA: 0x000072DD File Offset: 0x000054DD
		protected override IMailboxReplicationProxyService CreateChannel()
		{
			return ExchangeSessionAwareClientsHelper.CreateChannel(this);
		}

		// Token: 0x060000B9 RID: 185 RVA: 0x000072E5 File Offset: 0x000054E5
		protected override void HandleFaultException(FaultException<MailboxReplicationServiceFault> fault, string context)
		{
			fault.Detail.ReconstructAndThrow(context, base.ServerVersion);
		}

		// Token: 0x060000BA RID: 186 RVA: 0x000072FC File Offset: 0x000054FC
		private static bool KeepAlivePingerShouldRemove(Guid key, MailboxReplicationProxyClient client)
		{
			bool flag = false;
			try
			{
				Monitor.TryEnter(client.serviceCallLock, ref flag);
				if (flag)
				{
					if (client.State != CommunicationState.Opened)
					{
						return true;
					}
					client.Ping();
				}
			}
			finally
			{
				if (flag)
				{
					Monitor.Exit(client.serviceCallLock);
				}
				else
				{
					client.pingPostponed = true;
				}
			}
			return false;
		}

		// Token: 0x060000BB RID: 187 RVA: 0x0000735C File Offset: 0x0000555C
		private void Init(Guid physicalMbxGuid, Guid primaryMbxGuid, string filePath, string targetDatabase, TenantPartitionHint partitionHint, ProxyControlFlags proxyControlFlags, TimeSpan longOperationTimeout)
		{
			this.primaryMbxGuid = primaryMbxGuid;
			this.partitionHint = partitionHint;
			this.proxyControlFlags = proxyControlFlags;
			this.LongOperationTimeout = longOperationTimeout;
			this.lastFailure = null;
			CertificateValidationManager.RegisterCallback(base.GetType().FullName, new RemoteCertificateValidationCallback(ExchangeCertificateValidator.CertificateValidatorCallback));
			this.RequestContext = new MRSProxyRequestContext();
			this.RequestContext.HttpHeaders[WellKnownHeader.ClientVersion] = Microsoft.Exchange.Data.ServerVersion.InstalledVersion.ToString();
			this.RequestContext.HttpHeaders[CertificateValidationManager.ComponentIdHeaderName] = CertificateValidationManager.GenerateComponentIdQueryString(base.GetType().FullName);
			if (!this.proxyControlFlags.HasFlag(ProxyControlFlags.DoNotAddIdentifyingCafeHeaders))
			{
				if (physicalMbxGuid != Guid.Empty)
				{
					this.RequestContext.HttpHeaders[WellKnownHeader.AnchorMailbox] = physicalMbxGuid.ToString();
				}
				if (!string.IsNullOrEmpty(filePath))
				{
					this.RequestContext.HttpHeaders[WellKnownHeader.GenericAnchorHint] = Convert.ToBase64String(Encoding.Unicode.GetBytes(filePath));
				}
				if (targetDatabase != null)
				{
					this.RequestContext.HttpHeaders[WellKnownHeader.TargetDatabase] = targetDatabase;
				}
			}
		}

		// Token: 0x060000BC RID: 188 RVA: 0x000074C8 File Offset: 0x000056C8
		private void Ping()
		{
			this.pingPostponed = false;
			TimeSpan proxyClientPingInterval = TestIntegration.Instance.ProxyClientPingInterval;
			MrsTracer.ProxyClient.Debug("KeepAlive pinger making dummy wcf call, previous call time {0}, timeout interval {1}", new object[]
			{
				this.lastCallTimeStamp,
				proxyClientPingInterval
			});
			Stopwatch watch = Stopwatch.StartNew();
			CommonUtils.CatchKnownExceptions(delegate
			{
				this.ExchangeVersionInformation();
				this.latencyInfo.AddSample((int)watch.ElapsedMilliseconds);
				watch.Stop();
			}, null);
		}

		// Token: 0x060000BD RID: 189 RVA: 0x00007540 File Offset: 0x00005740
		private void Initialize(string serverName, NetworkCredential remoteCred, bool useHttps)
		{
			ExchangeCertificateValidator.Initialize();
			Uri uri = base.Endpoint.Address.Uri;
			string uri2 = string.Format("{0}://{1}{2}", uri.Scheme, serverName, uri.PathAndQuery);
			try
			{
				base.Endpoint.Address = new EndpointAddress(uri2);
			}
			catch (UriFormatException innerException)
			{
				MrsTracer.ProxyClient.Error("Invalid ServerName in MRSProxyClient.Create", new object[0]);
				throw new InvalidServerNamePermanentException(serverName, innerException);
			}
			this.RequestContext.EndpointUri = base.Endpoint.Address.Uri;
			if (useHttps)
			{
				Server localServer = LocalServerCache.LocalServer;
				if (localServer != null && localServer.InternetWebProxy != null)
				{
					MrsTracer.ProxyClient.Debug("Using custom InternetWebProxy {0}", new object[]
					{
						localServer.InternetWebProxy
					});
					CustomBinding customBinding = base.Endpoint.Binding as CustomBinding;
					if (customBinding != null)
					{
						HttpsTransportBindingElement httpsTransportBindingElement = customBinding.Elements.Find<HttpsTransportBindingElement>();
						if (httpsTransportBindingElement != null)
						{
							httpsTransportBindingElement.UseDefaultWebProxy = false;
							httpsTransportBindingElement.ProxyAddress = localServer.InternetWebProxy;
							httpsTransportBindingElement.BypassProxyOnLocal = true;
						}
					}
				}
			}
			base.ClientCredentials.Windows.AllowedImpersonationLevel = TokenImpersonationLevel.Impersonation;
			base.ClientCredentials.Windows.ClientCredential = remoteCred;
			base.Endpoint.Behaviors.Add(new MaxFaultSizeBehavior(10485760));
			this.ExchangeVersionInformation();
			MailboxReplicationProxyClient.KeepAlivePinger.TryInsertAbsolute(this.RequestContext.Id, this, TestIntegration.Instance.ProxyClientPingInterval);
		}

		// Token: 0x060000BE RID: 190 RVA: 0x000076BC File Offset: 0x000058BC
		private void CallService(Action serviceCall)
		{
			this.CallServiceWithTimeout(MailboxReplicationProxyClient.DefaultOperationTimeout, serviceCall);
		}

		// Token: 0x060000BF RID: 191 RVA: 0x000076CC File Offset: 0x000058CC
		private void CallServiceWithTimeout(TimeSpan timeout, Action serviceCall)
		{
			lock (this.serviceCallLock)
			{
				if (this.lastCallTimeStamp != DateTime.MinValue)
				{
					TimeSpan timeSpan = DateTime.UtcNow - this.lastCallTimeStamp;
					TimeSpan timeSpan2 = TestIntegration.Instance.ProxyClientPingInterval;
					timeSpan2 += timeSpan2;
					if (timeSpan >= timeSpan2)
					{
						MrsTracer.ProxyClient.Error("The socket connection has been stale for {0}, more than expected {1}", new object[]
						{
							timeSpan,
							timeSpan2
						});
					}
				}
				this.lastCallTimeStamp = DateTime.UtcNow;
				if (base.State == CommunicationState.Faulted)
				{
					string text = base.Endpoint.Address.ToString();
					if (base.ServerVersion != null)
					{
						text += string.Format(" {0} ({1})", base.ServerVersion.ComputerName, base.ServerVersion.ToString());
					}
					throw new CommunicationWithRemoteServiceFailedTransientException(text, this.lastFailure);
				}
				using (new OperationContextScope(base.InnerChannel))
				{
					((IContextChannel)base.Channel).OperationTimeout = timeout;
					OperationContext.Current.OutgoingMessageHeaders.Clear();
					MessageHeader header = MessageHeader.CreateHeader("MailboxGuid", "http://schemas.microsoft.com/exchange/services/2006/types", this.primaryMbxGuid);
					OperationContext.Current.OutgoingMessageHeaders.Add(header);
					MessageHeader header2 = MessageHeader.CreateHeader("PartitionHint", "http://schemas.microsoft.com/exchange/services/2006/types", this.partitionHint);
					OperationContext.Current.OutgoingMessageHeaders.Add(header2);
					IActivityScope currentActivityScope = ActivityContext.GetCurrentActivityScope();
					if (currentActivityScope != null)
					{
						currentActivityScope.SerializeTo(OperationContext.Current);
					}
					string context = base.Endpoint.Address.ToString();
					Stopwatch stopwatch = Stopwatch.StartNew();
					try
					{
						this.CallService(serviceCall, context);
						this.lastFailure = null;
					}
					catch (Exception ex)
					{
						this.lastFailure = ex;
						throw;
					}
					finally
					{
						this.latencyInfo.TotalRemoteCallDuration += stopwatch.Elapsed;
						this.latencyInfo.TotalNumberOfRemoteCalls++;
						this.lastCallTimeStamp = DateTime.UtcNow;
						if (!this.isBackendCookieFetched)
						{
							this.SetBackendCookie();
						}
					}
				}
				if (this.pingPostponed)
				{
					this.Ping();
				}
			}
		}

		// Token: 0x060000C0 RID: 192 RVA: 0x00007964 File Offset: 0x00005B64
		private void SetBackendCookie()
		{
			if (base.Endpoint.Address.Uri.Scheme != Uri.UriSchemeHttps && base.Endpoint.Address.Uri.Scheme != Uri.UriSchemeHttp)
			{
				this.isBackendCookieFetched = true;
				return;
			}
			string backendCookieValue = this.GetBackendCookieValue();
			if (string.IsNullOrWhiteSpace(backendCookieValue))
			{
				return;
			}
			this.RequestContext.BackendCookie = new Cookie("X-BackEndCookie", backendCookieValue);
			this.isBackendCookieFetched = true;
		}

		// Token: 0x060000C1 RID: 193 RVA: 0x000079E8 File Offset: 0x00005BE8
		private string GetBackendCookieValue()
		{
			if (OperationContext.Current == null || OperationContext.Current.IncomingMessageProperties == null || !OperationContext.Current.IncomingMessageProperties.ContainsKey(HttpResponseMessageProperty.Name))
			{
				MrsTracer.ProxyClient.Warning("Http response is missing or not initialized.", new object[0]);
				return null;
			}
			HttpResponseMessageProperty httpResponseMessageProperty = OperationContext.Current.IncomingMessageProperties[HttpResponseMessageProperty.Name] as HttpResponseMessageProperty;
			if (httpResponseMessageProperty == null || httpResponseMessageProperty.Headers == null)
			{
				MrsTracer.ProxyClient.Warning("Http response header is missing.", new object[0]);
				return null;
			}
			string text = httpResponseMessageProperty.Headers[HttpResponseHeader.SetCookie];
			if (string.IsNullOrWhiteSpace(text))
			{
				MrsTracer.ProxyClient.Debug("No cookies from server. This is expected when MRS proxy is e14.", new object[0]);
				return null;
			}
			MrsTracer.ProxyClient.Debug("Found cookies: {0}", new object[]
			{
				text
			});
			int num = text.IndexOf("X-BackEndCookie=");
			if (num < 0)
			{
				MrsTracer.ProxyClient.Debug("No backend cookie from server. This is expected when MRS proxy is e14.", new object[0]);
				return null;
			}
			int num2 = num + "X-BackEndCookie=".Length;
			num = text.IndexOf(';', num2);
			if (num < 0)
			{
				MrsTracer.ProxyClient.Warning("Malformed cookie. Cannot locate terminating semicolon.", new object[0]);
				return null;
			}
			return text.Substring(num2, num - num2);
		}

		// Token: 0x060000C2 RID: 194 RVA: 0x00007B20 File Offset: 0x00005D20
		private void VerifyRequiredCapability(MRSProxyCapabilities requiredCapability)
		{
			if (!base.ServerVersion[(int)requiredCapability])
			{
				MrsTracer.ProxyClient.Error("Talking to downlevel server: no {0} support", new object[]
				{
					requiredCapability.ToString()
				});
				throw new UnsupportedRemoteServerVersionWithOperationPermanentException(base.Endpoint.Address.ToString(), base.ServerVersion.ToString(), requiredCapability.ToString());
			}
		}

		// Token: 0x0400001A RID: 26
		public const string EwsTypeNamespace = "http://schemas.microsoft.com/exchange/services/2006/types";

		// Token: 0x0400001B RID: 27
		public const string MailboxGuidHeaderName = "MailboxGuid";

		// Token: 0x0400001C RID: 28
		private const string PartitionHintHeaderName = "PartitionHint";

		// Token: 0x0400001D RID: 29
		private const string BackendCookieName = "X-BackEndCookie";

		// Token: 0x0400001E RID: 30
		private const string BackendCookiePrefix = "X-BackEndCookie=";

		// Token: 0x0400001F RID: 31
		private static readonly Guid ConnectionTestGuid = new Guid("2985c88d-e426-474b-b7c1-bae258c73db3");

		// Token: 0x04000020 RID: 32
		private static readonly TimeSpan DefaultOperationTimeout = TimeSpan.FromSeconds(50.0);

		// Token: 0x04000021 RID: 33
		private static readonly ExactTimeoutCache<Guid, MailboxReplicationProxyClient> KeepAlivePinger = new ExactTimeoutCache<Guid, MailboxReplicationProxyClient>(null, new ShouldRemoveDelegate<Guid, MailboxReplicationProxyClient>(MailboxReplicationProxyClient.KeepAlivePingerShouldRemove), null, 2000, false);

		// Token: 0x04000022 RID: 34
		private readonly object serviceCallLock = new object();

		// Token: 0x04000023 RID: 35
		private Guid primaryMbxGuid;

		// Token: 0x04000024 RID: 36
		private ProxyControlFlags proxyControlFlags;

		// Token: 0x04000025 RID: 37
		private TenantPartitionHint partitionHint;

		// Token: 0x04000026 RID: 38
		private Exception lastFailure;

		// Token: 0x04000027 RID: 39
		private DateTime lastCallTimeStamp = DateTime.MinValue;

		// Token: 0x04000028 RID: 40
		private LatencyInfo latencyInfo = new LatencyInfo();

		// Token: 0x04000029 RID: 41
		private bool isBackendCookieFetched;

		// Token: 0x0400002A RID: 42
		private bool pingPostponed;
	}
}
