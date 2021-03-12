using System;
using System.Collections.Generic;
using System.Net;
using System.Security.AccessControl;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Data.Directory.SystemConfiguration.ConfigurationSettings;
using Microsoft.Mapi;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x02000007 RID: 7
	internal abstract class RemoteMailbox : RemoteObject, IMailbox, IDisposable
	{
		// Token: 0x0600002E RID: 46 RVA: 0x00002C84 File Offset: 0x00000E84
		public RemoteMailbox(string serverName, string remoteOrgName, NetworkCredential remoteCred, ProxyControlFlags proxyControlFlags, IEnumerable<MRSProxyCapabilities> requiredCapabilities, bool useHttps, LocalMailboxFlags flags) : base(null, 0L)
		{
			this.serverName = serverName;
			this.remoteOrgName = remoteOrgName;
			this.remoteCred = remoteCred;
			this.proxyControlFlags = proxyControlFlags;
			this.requiredCapabilities = requiredCapabilities;
			this.useHttps = useHttps;
			this.flags = flags;
			TestIntegration.Instance.ForceRefresh();
			this.longOperationTimeout = ConfigBase<MRSConfigSchema>.GetConfig<TimeSpan>("MRSProxyLongOperationTimeout");
			this.ExportBufferSizeKB = ConfigBase<MRSConfigSchema>.GetConfig<int>("ExportBufferSizeKB");
		}

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x0600002F RID: 47 RVA: 0x00002D04 File Offset: 0x00000F04
		// (set) Token: 0x06000030 RID: 48 RVA: 0x00002D0C File Offset: 0x00000F0C
		public int ExportBufferSizeKB { get; private set; }

		// Token: 0x06000031 RID: 49 RVA: 0x00002D15 File Offset: 0x00000F15
		LatencyInfo IMailbox.GetLatencyInfo()
		{
			if (base.MrsProxyClient != null)
			{
				return base.MrsProxyClient.LatencyInfo;
			}
			return new LatencyInfo();
		}

		// Token: 0x06000032 RID: 50 RVA: 0x00002D30 File Offset: 0x00000F30
		bool IMailbox.IsConnected()
		{
			return base.MrsProxy != null;
		}

		// Token: 0x06000033 RID: 51 RVA: 0x00002D40 File Offset: 0x00000F40
		bool IMailbox.IsCapabilitySupported(MRSProxyCapabilities capability)
		{
			MrsTracer.ProxyClient.Function("RemoteMailbox.IsCapabilitySupported({0})", new object[]
			{
				capability
			});
			this.VerifyMailboxConnection();
			return base.MrsProxyClient.ServerVersion[(int)capability];
		}

		// Token: 0x06000034 RID: 52 RVA: 0x00002D84 File Offset: 0x00000F84
		bool IMailbox.IsMailboxCapabilitySupported(MailboxCapabilities capability)
		{
			MrsTracer.ProxyClient.Function("RemoteMailbox.IsMailboxCapabilitySupported({0})", new object[]
			{
				capability
			});
			bool result;
			if (this.mailboxCapabilities.TryGetValue(capability, out result))
			{
				return result;
			}
			this.VerifyMailboxConnection();
			if (capability == MailboxCapabilities.FolderRules && this is RemoteDestinationMailbox)
			{
				MailboxInformation mailboxInformation = ((IMailbox)this).GetMailboxInformation();
				if (mailboxInformation != null)
				{
					this.mailboxCapabilities[capability] = (mailboxInformation.ServerVersion >= Server.E14MinVersion);
					return this.mailboxCapabilities[capability];
				}
			}
			if (capability == MailboxCapabilities.FolderRules || capability == MailboxCapabilities.FolderAcls)
			{
				this.mailboxCapabilities[capability] = true;
				if (base.ServerVersion[60])
				{
					this.mailboxCapabilities[capability] = base.MrsProxy.IMailbox_IsMailboxCapabilitySupported2(base.Handle, (int)capability);
				}
				return this.mailboxCapabilities[capability];
			}
			if (base.ServerVersion[47])
			{
				this.mailboxCapabilities[capability] = base.MrsProxy.IMailbox_IsMailboxCapabilitySupported2(base.Handle, (int)capability);
				return this.mailboxCapabilities[capability];
			}
			if (base.ServerVersion[43] && (capability == MailboxCapabilities.PagedEnumerateChanges || capability == MailboxCapabilities.PagedGetActions || capability == MailboxCapabilities.ReplayActions))
			{
				this.mailboxCapabilities[capability] = base.MrsProxy.IMailbox_IsMailboxCapabilitySupported(base.Handle, capability);
				return this.mailboxCapabilities[capability];
			}
			this.mailboxCapabilities[capability] = false;
			return this.mailboxCapabilities[capability];
		}

		// Token: 0x06000035 RID: 53 RVA: 0x00002EFC File Offset: 0x000010FC
		void IMailbox.Config(IReservation reservation, Guid primaryMailboxGuid, Guid physicalMailboxGuid, TenantPartitionHint partitionHint, Guid mdbGuid, MailboxType mbxType, Guid? mailboxContainerGuid)
		{
			if (TestIntegration.Instance.RemoteExchangeGuidOverride != Guid.Empty)
			{
				bool flag = physicalMailboxGuid != primaryMailboxGuid;
				primaryMailboxGuid = TestIntegration.Instance.RemoteExchangeGuidOverride;
				physicalMailboxGuid = (flag ? TestIntegration.Instance.RemoteArchiveGuidOverride : TestIntegration.Instance.RemoteExchangeGuidOverride);
			}
			this.reservation = reservation;
			this.physicalMailboxGuid = physicalMailboxGuid;
			this.primaryMailboxGuid = primaryMailboxGuid;
			this.partitionHint = partitionHint;
			this.mdbGuid = mdbGuid;
			this.mbxType = mbxType;
			this.mailboxContainerGuid = mailboxContainerGuid;
		}

		// Token: 0x06000036 RID: 54 RVA: 0x00002F84 File Offset: 0x00001184
		void IMailbox.ConfigRestore(MailboxRestoreType restoreFlags)
		{
			MrsTracer.ProxyClient.Function("RemoteMailbox.ConfigRestore(restoreFlags={0}", new object[]
			{
				restoreFlags
			});
			this.restoreType = restoreFlags;
		}

		// Token: 0x06000037 RID: 55 RVA: 0x00002FB8 File Offset: 0x000011B8
		void IMailbox.ConfigADConnection(string domainControllerName, string configDomainControllerName, NetworkCredential cred)
		{
			if (!base.ServerVersion[24])
			{
				throw new UnsupportedRemoteServerVersionWithOperationPermanentException(base.MrsProxyClient.ServerName, base.ServerVersion.ToString(), "IMailbox_ConfigADConnection");
			}
			base.MrsProxy.IMailbox_ConfigADConnection(base.Handle, domainControllerName, (cred != null) ? cred.UserName : null, (cred != null) ? cred.Domain : null, (cred != null) ? cred.Password : null);
		}

		// Token: 0x06000038 RID: 56 RVA: 0x0000302B File Offset: 0x0000122B
		void IMailbox.ConfigMDBByName(string mdbName)
		{
			this.mdbName = mdbName;
		}

		// Token: 0x06000039 RID: 57 RVA: 0x00003034 File Offset: 0x00001234
		void IMailbox.ConfigMailboxOptions(MailboxOptions options)
		{
			if (base.ServerVersion[13])
			{
				base.MrsProxy.IMailbox_ConfigMailboxOptions(base.Handle, (int)options);
				return;
			}
			if (options != MailboxOptions.None)
			{
				throw new UnsupportedRemoteServerVersionWithOperationPermanentException(base.MrsProxyClient.ServerName, base.ServerVersion.ToString(), "IMailbox_ConfigMailboxOptions");
			}
		}

		// Token: 0x0600003A RID: 58 RVA: 0x00003087 File Offset: 0x00001287
		void IMailbox.ConfigPreferredADConnection(string preferredDomainControllerName)
		{
			this.preferredDomainControllerName = preferredDomainControllerName;
		}

		// Token: 0x0600003B RID: 59 RVA: 0x00003090 File Offset: 0x00001290
		void IMailbox.ConfigPst(string filePath, int? contentCodePage)
		{
			this.filePath = filePath;
			this.contentCodePage = contentCodePage;
		}

		// Token: 0x0600003C RID: 60 RVA: 0x000030A0 File Offset: 0x000012A0
		void IMailbox.ConfigEas(NetworkCredential userCredential, SmtpAddress smtpAddress, Guid mailboxGuid, string remoteHostName)
		{
			this.primaryMailboxGuid = mailboxGuid;
			this.easConfiguration = new RemoteMailbox.EasConfiguration(userCredential, smtpAddress, remoteHostName);
		}

		// Token: 0x0600003D RID: 61 RVA: 0x000030B8 File Offset: 0x000012B8
		void IMailbox.ConfigOlc(OlcMailboxConfiguration config)
		{
			this.olcConfig = config;
			this.serverName = this.olcConfig.RemoteHostName;
		}

		// Token: 0x0600003E RID: 62 RVA: 0x000030D4 File Offset: 0x000012D4
		MailboxInformation IMailbox.GetMailboxInformation()
		{
			MrsTracer.ProxyClient.Function("RemoteMailbox.GetMailboxInformation", new object[0]);
			this.VerifyMailboxConnection();
			return base.MrsProxy.IMailbox_GetMailboxInformation(base.Handle);
		}

		// Token: 0x0600003F RID: 63 RVA: 0x0000312C File Offset: 0x0000132C
		void IMailbox.Connect(MailboxConnectFlags connectFlags)
		{
			if (base.MrsProxy == null)
			{
				MailboxReplicationProxyClient mailboxReplicationProxyClient = null;
				IMailboxReplicationProxyService iMRPS = null;
				long handle = 0L;
				string database = null;
				if (!this.flags.HasFlag(LocalMailboxFlags.PstExport) && (this.mbxType == MailboxType.DestMailboxCrossOrg || this.restoreType != MailboxRestoreType.None))
				{
					database = ((this.mdbGuid != Guid.Empty) ? this.mdbGuid.ToString() : this.mdbName);
				}
				try
				{
					if (this.proxyControlFlags.HasFlag(ProxyControlFlags.Olc))
					{
						mailboxReplicationProxyClient = MailboxReplicationProxyClient.CreateForOlcConnection(this.serverName, this.proxyControlFlags);
					}
					else
					{
						mailboxReplicationProxyClient = MailboxReplicationProxyClient.Create(this.serverName, this.remoteOrgName, this.remoteCred, this.physicalMailboxGuid, this.primaryMailboxGuid, this.filePath, database, this.partitionHint, this.useHttps, this.proxyControlFlags, this.longOperationTimeout);
					}
					if (this.requiredCapabilities != null)
					{
						foreach (MRSProxyCapabilities mrsproxyCapabilities in this.requiredCapabilities)
						{
							if (!mailboxReplicationProxyClient.ServerVersion[(int)mrsproxyCapabilities])
							{
								MrsTracer.ProxyClient.Error("Talking to downlevel server '{0}': no {1} support", new object[]
								{
									mailboxReplicationProxyClient.ServerVersion.ComputerName,
									mrsproxyCapabilities.ToString()
								});
								throw new UnsupportedRemoteServerVersionWithOperationPermanentException(mailboxReplicationProxyClient.ServerVersion.ComputerName, mailboxReplicationProxyClient.ServerVersion.ToString(), mrsproxyCapabilities.ToString());
							}
						}
					}
					if (!mailboxReplicationProxyClient.ServerVersion[24])
					{
						this.flags &= RemoteMailbox.PreE15LocalMailboxFlags;
					}
					if ((this.flags & ~LocalMailboxFlags.StripLargeRulesForDownlevelTargets) != LocalMailboxFlags.None && !mailboxReplicationProxyClient.ServerVersion[24])
					{
						throw new UnsupportedRemoteServerVersionWithOperationPermanentException(mailboxReplicationProxyClient.ServerName, mailboxReplicationProxyClient.ServerVersion.ToString(), "TenantHint");
					}
					if (this.mailboxContainerGuid != null && !mailboxReplicationProxyClient.ServerVersion[46])
					{
						throw new UnsupportedRemoteServerVersionWithOperationPermanentException(mailboxReplicationProxyClient.ServerName, mailboxReplicationProxyClient.ServerVersion.ToString(), "ContainerOperations");
					}
					if ((connectFlags & MailboxConnectFlags.DoNotOpenMapiSession) != MailboxConnectFlags.None && !mailboxReplicationProxyClient.ServerVersion[11])
					{
						throw new UnsupportedRemoteServerVersionWithOperationPermanentException(mailboxReplicationProxyClient.ServerName, mailboxReplicationProxyClient.ServerVersion.ToString(), "PostMoveCleanup");
					}
					if (this.restoreType != MailboxRestoreType.None && !mailboxReplicationProxyClient.ServerVersion[34])
					{
						throw new UnsupportedRemoteServerVersionWithOperationPermanentException(mailboxReplicationProxyClient.ServerName, mailboxReplicationProxyClient.ServerVersion.ToString(), "IMailbox_ConfigRestore");
					}
					iMRPS = mailboxReplicationProxyClient;
					if (this.flags.HasFlag(LocalMailboxFlags.PstImport) && !mailboxReplicationProxyClient.ServerVersion[39])
					{
						throw new UnsupportedRemoteServerVersionWithOperationPermanentException(mailboxReplicationProxyClient.ServerName, mailboxReplicationProxyClient.ServerVersion.ToString(), "Pst");
					}
					if (this.flags.HasFlag(LocalMailboxFlags.PstExport) && !mailboxReplicationProxyClient.ServerVersion[57])
					{
						throw new UnsupportedRemoteServerVersionWithOperationPermanentException(mailboxReplicationProxyClient.ServerName, mailboxReplicationProxyClient.ServerVersion.ToString(), "RemotePstExport");
					}
					if (this.flags.HasFlag(LocalMailboxFlags.EasSync) && !mailboxReplicationProxyClient.ServerVersion[44])
					{
						throw new UnsupportedRemoteServerVersionWithOperationPermanentException(mailboxReplicationProxyClient.ServerName, mailboxReplicationProxyClient.ServerVersion.ToString(), "Eas");
					}
					if (mailboxReplicationProxyClient.ServerVersion[46])
					{
						handle = iMRPS.IMailbox_Config7((this.reservation != null) ? this.reservation.Id : Guid.Empty, this.primaryMailboxGuid, this.physicalMailboxGuid, (this.partitionHint != null) ? this.partitionHint.GetPersistablePartitionHint() : null, this.mdbGuid, this.mdbName, this.mbxType, (int)this.proxyControlFlags, (int)this.flags, this.mailboxContainerGuid);
					}
					else if (mailboxReplicationProxyClient.ServerVersion[41])
					{
						handle = iMRPS.IMailbox_Config5((this.reservation != null) ? this.reservation.Id : Guid.Empty, this.primaryMailboxGuid, this.physicalMailboxGuid, (this.partitionHint != null) ? this.partitionHint.GetPersistablePartitionHint() : null, this.mdbGuid, this.mdbName, this.mbxType, (int)this.proxyControlFlags, (int)this.flags);
					}
					else if (mailboxReplicationProxyClient.ServerVersion[39])
					{
						handle = iMRPS.IMailbox_Config6((this.reservation != null) ? this.reservation.Id : Guid.Empty, this.primaryMailboxGuid, this.physicalMailboxGuid, this.filePath, (this.partitionHint != null) ? this.partitionHint.GetPersistablePartitionHint() : null, this.mdbGuid, this.mdbName, this.mbxType, (int)this.proxyControlFlags, (int)this.flags);
					}
					else if (mailboxReplicationProxyClient.ServerVersion[37])
					{
						handle = iMRPS.IMailbox_Config5((this.reservation != null) ? this.reservation.Id : Guid.Empty, this.primaryMailboxGuid, this.physicalMailboxGuid, (this.partitionHint != null) ? this.partitionHint.GetPersistablePartitionHint() : null, this.mdbGuid, this.mdbName, this.mbxType, (int)this.proxyControlFlags, (int)this.flags);
					}
					else
					{
						RemoteReservation remoteReservation = this.reservation as RemoteReservation;
						if (remoteReservation != null)
						{
							remoteReservation.ConfirmLegacyReservation(mailboxReplicationProxyClient);
						}
						if (mailboxReplicationProxyClient.ServerVersion[24])
						{
							handle = iMRPS.IMailbox_Config4(this.primaryMailboxGuid, this.physicalMailboxGuid, (this.partitionHint != null) ? this.partitionHint.GetPersistablePartitionHint() : null, this.mdbGuid, this.mdbName, this.mbxType, (int)this.proxyControlFlags, (int)this.flags);
						}
						else
						{
							ProxyControlFlags proxyControlFlags = this.proxyControlFlags;
							if ((this.flags & LocalMailboxFlags.StripLargeRulesForDownlevelTargets) != LocalMailboxFlags.None)
							{
								proxyControlFlags |= ProxyControlFlags.StripLargeRulesForDownlevelTargets;
							}
							handle = iMRPS.IMailbox_Config3(this.primaryMailboxGuid, this.physicalMailboxGuid, this.mdbGuid, this.mdbName, this.mbxType, (int)proxyControlFlags);
						}
					}
					if (!string.IsNullOrEmpty(this.preferredDomainControllerName))
					{
						if (mailboxReplicationProxyClient.ServerVersion[48])
						{
							iMRPS.IMailbox_ConfigPreferredADConnection(handle, this.preferredDomainControllerName);
						}
						else
						{
							MrsTracer.ProxyClient.Warning("IMailbox_ConfigPreferredADConnection not expected to be called for server:{0} version:{1}", new object[]
							{
								mailboxReplicationProxyClient.ServerName,
								mailboxReplicationProxyClient.ServerVersion.ToString()
							});
						}
					}
					if ((this.flags.HasFlag(LocalMailboxFlags.PstImport) || this.flags.HasFlag(LocalMailboxFlags.PstExport)) && mailboxReplicationProxyClient.ServerVersion[41])
					{
						iMRPS.IMailbox_ConfigPst(handle, this.filePath, this.contentCodePage);
					}
					if (this.flags.HasFlag(LocalMailboxFlags.EasSync))
					{
						if (mailboxReplicationProxyClient.ServerVersion[53])
						{
							iMRPS.IMailbox_ConfigEas2(handle, this.easConfiguration.UserCred.Password, this.easConfiguration.SmtpAddress, this.primaryMailboxGuid, this.easConfiguration.RemoteHostName);
						}
						else
						{
							iMRPS.IMailbox_ConfigEas(handle, this.easConfiguration.UserCred.Password, this.easConfiguration.SmtpAddress);
						}
					}
					if (this.proxyControlFlags.HasFlag(ProxyControlFlags.Olc))
					{
						if (!mailboxReplicationProxyClient.ServerVersion[55])
						{
							throw new UnsupportedRemoteServerVersionWithOperationPermanentException(mailboxReplicationProxyClient.ServerName, mailboxReplicationProxyClient.ServerVersion.ToString(), "IMailbox_ConfigOlc");
						}
						iMRPS.IMailbox_ConfigOlc(handle, this.olcConfig);
					}
					if (mailboxReplicationProxyClient.ServerVersion[42])
					{
						iMRPS.IMailbox_ConfigureProxyService(new ProxyConfiguration());
					}
					if (this.restoreType != MailboxRestoreType.None)
					{
						iMRPS.IMailbox_ConfigRestore(handle, (int)this.restoreType);
					}
					if (mailboxReplicationProxyClient.ServerVersion[11])
					{
						iMRPS.IMailbox_Connect2(handle, (int)connectFlags);
					}
					else
					{
						iMRPS.IMailbox_Connect(handle);
					}
					base.MrsProxy = mailboxReplicationProxyClient;
					base.Handle = handle;
					mailboxReplicationProxyClient = null;
					handle = 0L;
				}
				finally
				{
					if (handle != 0L)
					{
						CommonUtils.CatchKnownExceptions(delegate
						{
							iMRPS.CloseHandle(handle);
						}, null);
					}
					if (mailboxReplicationProxyClient != null)
					{
						mailboxReplicationProxyClient.Dispose();
					}
				}
			}
		}

		// Token: 0x06000040 RID: 64 RVA: 0x00003A53 File Offset: 0x00001C53
		void IMailbox.Disconnect()
		{
			if (!((IMailbox)this).IsConnected())
			{
				return;
			}
			CommonUtils.CatchKnownExceptions(delegate
			{
				base.MrsProxy.IMailbox_Disconnect(base.Handle);
			}, null);
			base.MrsProxyClient.Dispose();
			base.Handle = 0L;
			base.MrsProxy = null;
		}

		// Token: 0x06000041 RID: 65 RVA: 0x00003A8A File Offset: 0x00001C8A
		MailboxServerInformation IMailbox.GetMailboxServerInformation()
		{
			MrsTracer.ProxyClient.Function("RemoteMailbox.GetMailboxServerInformation", new object[0]);
			this.VerifyMailboxConnection();
			return base.MrsProxy.IMailbox_GetMailboxServerInformation(base.Handle);
		}

		// Token: 0x06000042 RID: 66 RVA: 0x00003AB8 File Offset: 0x00001CB8
		VersionInformation IMailbox.GetVersion()
		{
			return base.ServerVersion;
		}

		// Token: 0x06000043 RID: 67 RVA: 0x00003AC0 File Offset: 0x00001CC0
		void IMailbox.SetOtherSideVersion(VersionInformation otherSideVersion)
		{
			MrsTracer.ProxyClient.Function("RemoteMailbox.GetMailboxServerInformation", new object[0]);
			this.VerifyMailboxConnection();
			if (!base.ServerVersion[56])
			{
				return;
			}
			base.MrsProxy.IMailbox_SetOtherSideVersion(base.Handle, otherSideVersion);
		}

		// Token: 0x06000044 RID: 68 RVA: 0x00003B00 File Offset: 0x00001D00
		void IMailbox.DeleteMailbox(int flags)
		{
			MrsTracer.ProxyClient.Function("RemoteMailbox.DeleteMailbox({0})", new object[]
			{
				flags
			});
			this.VerifyMailboxConnection();
			base.MrsProxy.IMailbox_DeleteMailbox(base.Handle, flags);
		}

		// Token: 0x06000045 RID: 69 RVA: 0x00003B48 File Offset: 0x00001D48
		List<FolderRec> IMailbox.EnumerateFolderHierarchy(EnumerateFolderHierarchyFlags flags, PropTag[] additionalPtagsToLoad)
		{
			MrsTracer.ProxyClient.Function("RemoteMailbox.EnumerateFolderHierarchy({0})", new object[]
			{
				flags
			});
			this.VerifyMailboxConnection();
			bool flag;
			List<FolderRec> list = base.MrsProxy.IMailbox_EnumerateFolderHierarchyPaged2(base.Handle, flags, DataConverter<PropTagConverter, PropTag, int>.GetData(additionalPtagsToLoad), out flag);
			while (flag)
			{
				List<FolderRec> collection = base.MrsProxy.IMailbox_EnumerateFolderHierarchyNextBatch(base.Handle, out flag);
				list.AddRange(collection);
			}
			return list;
		}

		// Token: 0x06000046 RID: 70 RVA: 0x00003BB8 File Offset: 0x00001DB8
		List<WellKnownFolder> IMailbox.DiscoverWellKnownFolders(int flags)
		{
			MrsTracer.ProxyClient.Function("RemoteMailbox.DiscoverWellKnownFolders", new object[0]);
			this.VerifyMailboxConnection();
			if (!base.ServerVersion[35])
			{
				return FolderHierarchyUtils.DiscoverWellKnownFolders(this, flags);
			}
			return base.MrsProxy.IMailbox_DiscoverWellKnownFolders(base.Handle, flags);
		}

		// Token: 0x06000047 RID: 71 RVA: 0x00003C10 File Offset: 0x00001E10
		PropTag[] IMailbox.GetIDsFromNames(bool createIfNotExists, NamedPropData[] npa)
		{
			MrsTracer.ProxyClient.Function("RemoteMailbox.GetIDsFromNames", new object[0]);
			this.VerifyMailboxConnection();
			int[] array = base.MrsProxy.IMailbox_GetIDsFromNames(base.Handle, createIfNotExists, npa);
			PropTag[] array2 = new PropTag[array.Length];
			for (int i = 0; i < array.Length; i++)
			{
				array2[i] = (PropTag)array[i];
			}
			return array2;
		}

		// Token: 0x06000048 RID: 72 RVA: 0x00003C6A File Offset: 0x00001E6A
		byte[] IMailbox.GetSessionSpecificEntryId(byte[] entryId)
		{
			MrsTracer.ProxyClient.Function("RemoteMailbox.GetSessionSpecificEntryId", new object[0]);
			this.VerifyMailboxConnection();
			return base.MrsProxy.IMailbox_GetSessionSpecificEntryId(base.Handle, entryId);
		}

		// Token: 0x06000049 RID: 73 RVA: 0x00003C99 File Offset: 0x00001E99
		NamedPropData[] IMailbox.GetNamesFromIDs(PropTag[] pta)
		{
			MrsTracer.ProxyClient.Function("RemoteMailbox.GetNamesFromIDs", new object[0]);
			this.VerifyMailboxConnection();
			return base.MrsProxy.IMailbox_GetNamesFromIDs(base.Handle, DataConverter<PropTagConverter, PropTag, int>.GetData(pta));
		}

		// Token: 0x0600004A RID: 74 RVA: 0x00003CD0 File Offset: 0x00001ED0
		MappedPrincipal[] IMailbox.ResolvePrincipals(MappedPrincipal[] principals)
		{
			MrsTracer.ProxyClient.Function("RemoteMailbox.ResolvePrincipals", new object[0]);
			this.VerifyMailboxConnection();
			if (!base.ServerVersion[8])
			{
				Guid[] array = new Guid[principals.Length];
				int i = 0;
				foreach (MappedPrincipal mappedPrincipal in principals)
				{
					if (!mappedPrincipal.HasField(MappedPrincipalFields.MailboxGuid))
					{
						break;
					}
					array[i++] = mappedPrincipal.MailboxGuid;
				}
				if (i < principals.Length)
				{
					array = base.MrsProxy.IMailbox_GetMailboxGuidsFromPrincipals(base.Handle, principals);
				}
				List<Guid> list = new List<Guid>();
				foreach (Guid guid in array)
				{
					if (guid != Guid.Empty)
					{
						list.Add(guid);
					}
				}
				MappedPrincipal[] array3 = base.MrsProxy.IMailbox_GetPrincipalsFromMailboxGuids(base.Handle, list.ToArray());
				Dictionary<Guid, MappedPrincipal> dictionary = new Dictionary<Guid, MappedPrincipal>();
				for (i = 0; i < array3.Length; i++)
				{
					if (array3[i] != null)
					{
						dictionary[list[i]] = array3[i];
					}
				}
				MappedPrincipal[] array4 = new MappedPrincipal[array.Length];
				for (i = 0; i < array.Length; i++)
				{
					array4[i] = null;
					MappedPrincipal mappedPrincipal2;
					if (array[i] != Guid.Empty && dictionary.TryGetValue(array[i], out mappedPrincipal2))
					{
						array4[i] = mappedPrincipal2;
					}
				}
				return array4;
			}
			return base.MrsProxy.IMailbox_ResolvePrincipals(base.Handle, principals);
		}

		// Token: 0x0600004B RID: 75 RVA: 0x00003E58 File Offset: 0x00002058
		void IMailbox.SetInTransitStatus(InTransitStatus status, out bool onlineMoveSupported)
		{
			MrsTracer.ProxyClient.Function("RemoteMailbox.SetInTransitStatus({0})", new object[]
			{
				status
			});
			this.VerifyMailboxConnection();
			base.MrsProxy.IMailbox_SetInTransitStatus(base.Handle, (int)status, out onlineMoveSupported);
		}

		// Token: 0x0600004C RID: 76 RVA: 0x00003E9E File Offset: 0x0000209E
		void IMailbox.SeedMBICache()
		{
			MrsTracer.ProxyClient.Function("RemoteMailbox.SeedMBICache", new object[0]);
			this.VerifyMailboxConnection();
			if (!base.ServerVersion[11])
			{
				return;
			}
			base.MrsProxy.IMailbox_SeedMBICache(base.Handle);
		}

		// Token: 0x0600004D RID: 77 RVA: 0x00003EDC File Offset: 0x000020DC
		bool IMailbox.UpdateRemoteHostName(string value)
		{
			MrsTracer.ProxyClient.Function("RemoteMailbox.UpdateRemoteHostName", new object[0]);
			this.VerifyMailboxConnection();
			return base.MrsProxy.IMailbox_UpdateRemoteHostName(base.Handle, value);
		}

		// Token: 0x0600004E RID: 78 RVA: 0x00003F0C File Offset: 0x0000210C
		ADUser IMailbox.GetADUser()
		{
			MrsTracer.ProxyClient.Function("RemoteMailbox.GetADUser", new object[0]);
			this.VerifyMailboxConnection();
			string xml = base.MrsProxy.IMailbox_GetADUser(base.Handle);
			return ConfigurableObjectXML.Deserialize<ADUser>(xml);
		}

		// Token: 0x0600004F RID: 79 RVA: 0x00003F4C File Offset: 0x0000214C
		void IMailbox.UpdateMovedMailbox(UpdateMovedMailboxOperation op, ADUser remoteRecipientData, string domainController, out ReportEntry[] entries, Guid newDatabaseGuid, Guid newArchiveDatabaseGuid, string archiveDomain, ArchiveStatusFlags archiveStatus, UpdateMovedMailboxFlags updateMovedMailboxFlags, Guid? newMailboxContainerGuid, CrossTenantObjectId newUnifiedMailboxId)
		{
			entries = null;
			MrsTracer.ProxyClient.Function("RemoteMailbox.UpdateMovedMailbox", new object[0]);
			this.VerifyMailboxConnection();
			string remoteRecipientData2 = ConfigurableObjectXML.Serialize<ADUser>(remoteRecipientData);
			string text = null;
			if (base.ServerVersion[46])
			{
				byte[] newUnifiedMailboxIdData = (newUnifiedMailboxId == null) ? null : newUnifiedMailboxId.GetBytes();
				base.MrsProxy.IMailbox_UpdateMovedMailbox4(base.Handle, op, remoteRecipientData2, domainController, out text, newDatabaseGuid, newArchiveDatabaseGuid, archiveDomain, (int)archiveStatus, (int)updateMovedMailboxFlags, newMailboxContainerGuid, newUnifiedMailboxIdData);
			}
			else if (base.ServerVersion[36])
			{
				base.MrsProxy.IMailbox_UpdateMovedMailbox3(base.Handle, op, remoteRecipientData2, domainController, out text, newDatabaseGuid, newArchiveDatabaseGuid, archiveDomain, (int)archiveStatus, (int)updateMovedMailboxFlags);
			}
			else if (base.ServerVersion[9])
			{
				base.MrsProxy.IMailbox_UpdateMovedMailbox2(base.Handle, op, remoteRecipientData2, domainController, out text, newDatabaseGuid, newArchiveDatabaseGuid, archiveDomain, (int)archiveStatus);
			}
			else
			{
				base.MrsProxy.IMailbox_UpdateMovedMailbox(base.Handle, op, remoteRecipientData2, domainController, out text);
			}
			if (text != null)
			{
				List<ReportEntry> list = XMLSerializableBase.Deserialize<List<ReportEntry>>(text, false);
				entries = ((list != null) ? list.ToArray() : null);
			}
		}

		// Token: 0x06000050 RID: 80 RVA: 0x00004054 File Offset: 0x00002254
		RawSecurityDescriptor IMailbox.GetMailboxSecurityDescriptor()
		{
			MrsTracer.ProxyClient.Function("RemoteMailbox.GetMailboxSecurityDescriptor", new object[0]);
			this.VerifyMailboxConnection();
			byte[] array = base.MrsProxy.IMailbox_GetMailboxSecurityDescriptor(base.Handle);
			if (array == null)
			{
				return null;
			}
			return new RawSecurityDescriptor(array, 0);
		}

		// Token: 0x06000051 RID: 81 RVA: 0x0000409C File Offset: 0x0000229C
		RawSecurityDescriptor IMailbox.GetUserSecurityDescriptor()
		{
			MrsTracer.ProxyClient.Function("RemoteMailbox.GetUserSecurityDescriptor", new object[0]);
			this.VerifyMailboxConnection();
			byte[] array = base.MrsProxy.IMailbox_GetUserSecurityDescriptor(base.Handle);
			if (array == null)
			{
				return null;
			}
			return new RawSecurityDescriptor(array, 0);
		}

		// Token: 0x06000052 RID: 82 RVA: 0x000040E4 File Offset: 0x000022E4
		void IMailbox.AddMoveHistoryEntry(MoveHistoryEntryInternal mhei, int maxMoveHistoryLength)
		{
			MrsTracer.ProxyClient.Function("RemoteMailbox.AddMoveHistoryEntry", new object[0]);
			this.VerifyMailboxConnection();
			string mheData = mhei.Serialize(false);
			base.MrsProxy.IMailbox_AddMoveHistoryEntry(base.Handle, mheData, maxMoveHistoryLength);
		}

		// Token: 0x06000053 RID: 83 RVA: 0x00004128 File Offset: 0x00002328
		ServerHealthStatus IMailbox.CheckServerHealth()
		{
			MrsTracer.ProxyClient.Function("RemoteMailbox.CheckServerHealth", new object[0]);
			this.VerifyMailboxConnection();
			if (base.ServerVersion[12])
			{
				return base.MrsProxy.IMailbox_CheckServerHealth2(base.Handle);
			}
			ServerHealthStatus serverHealthStatus = new ServerHealthStatus(ServerHealthState.Healthy);
			try
			{
				base.MrsProxy.IMailbox_CheckServerHealth(base.Handle);
			}
			catch (MailboxReplicationTransientException)
			{
				serverHealthStatus.HealthState = ServerHealthState.NotHealthy;
			}
			return serverHealthStatus;
		}

		// Token: 0x06000054 RID: 84 RVA: 0x000041A8 File Offset: 0x000023A8
		PropValueData[] IMailbox.GetProps(PropTag[] ptags)
		{
			MrsTracer.ProxyClient.Function("RemoteMailbox.GetProps", new object[0]);
			this.VerifyMailboxConnection();
			if (base.ServerVersion[8])
			{
				return base.MrsProxy.IMailbox_GetProps(base.Handle, DataConverter<PropTagConverter, PropTag, int>.GetData(ptags));
			}
			if (this is RemoteSourceMailbox)
			{
				return base.MrsProxy.ISourceMailbox_GetProps(base.Handle, DataConverter<PropTagConverter, PropTag, int>.GetData(ptags));
			}
			throw new UnsupportedRemoteServerVersionWithOperationPermanentException(base.MrsProxyClient.ServerName, base.ServerVersion.ToString(), "IMailbox_GetProps");
		}

		// Token: 0x06000055 RID: 85 RVA: 0x00004238 File Offset: 0x00002438
		byte[] IMailbox.GetReceiveFolderEntryId(string msgClass)
		{
			MrsTracer.ProxyClient.Function("RemoteMailbox.GetReceiveFolderEntryId", new object[0]);
			this.VerifyMailboxConnection();
			if (!base.ServerVersion[8])
			{
				throw new UnsupportedRemoteServerVersionWithOperationPermanentException(base.MrsProxyClient.ServerName, base.ServerVersion.ToString(), "IMailbox_GetReceiveFolderEntryId");
			}
			return base.MrsProxy.IMailbox_GetReceiveFolderEntryId(base.Handle, msgClass);
		}

		// Token: 0x06000056 RID: 86 RVA: 0x000042A1 File Offset: 0x000024A1
		Guid[] IMailbox.ResolvePolicyTag(string policyTagStr)
		{
			throw new UnsupportedRemoteServerVersionWithOperationPermanentException(base.MrsProxyClient.ServerName, base.ServerVersion.ToString(), "IMailbox_ResolvePolicyTag");
		}

		// Token: 0x06000057 RID: 87 RVA: 0x000042D4 File Offset: 0x000024D4
		string IMailbox.LoadSyncState(byte[] key)
		{
			MrsTracer.ProxyClient.Function("RemoteMailbox.LoadSyncState", new object[0]);
			if (!(this is RemoteDestinationMailbox))
			{
				throw new UnsupportedRemoteServerVersionWithOperationPermanentException(base.MrsProxyClient.ServerName, base.ServerVersion.ToString(), "IMailbox_LoadSyncState");
			}
			this.VerifyMailboxConnection();
			DataExportBatch dataExportBatch = base.MrsProxy.IDestinationMailbox_LoadSyncState2(base.Handle, key);
			string syncState = null;
			using (PagedReceiver pagedReceiver = new PagedReceiver(delegate(string data)
			{
				syncState = data;
			}, base.MrsProxyClient.UseCompression))
			{
				RemoteDataExport.ExportRoutine(base.MrsProxy, dataExportBatch.DataExportHandle, pagedReceiver, dataExportBatch, base.MrsProxyClient.UseCompression);
			}
			return syncState;
		}

		// Token: 0x06000058 RID: 88 RVA: 0x000043AC File Offset: 0x000025AC
		MessageRec IMailbox.SaveSyncState(byte[] key, string syncState)
		{
			MrsTracer.ProxyClient.Function("RemoteMailbox.SaveSyncState", new object[0]);
			if (!(this is RemoteDestinationMailbox))
			{
				throw new UnsupportedRemoteServerVersionWithOperationPermanentException(base.MrsProxyClient.ServerName, base.ServerVersion.ToString(), "IMailbox_SaveSyncState");
			}
			this.VerifyMailboxConnection();
			IDataExport dataExport = new PagedTransmitter(syncState, base.MrsProxyClient.UseCompression);
			DataExportBatch dataExportBatch = dataExport.ExportData();
			long handle = base.MrsProxy.IDestinationMailbox_SaveSyncState2(base.Handle, key, dataExportBatch);
			if (!dataExportBatch.IsLastBatch)
			{
				using (IDataImport dataImport = new RemoteDataImport(base.MrsProxy, handle, null))
				{
					do
					{
						dataExportBatch = dataExport.ExportData();
						IDataMessage message = DataMessageSerializer.Deserialize(dataExportBatch.Opcode, dataExportBatch.Data, base.MrsProxyClient.UseCompression);
						dataImport.SendMessage(message);
					}
					while (!dataExportBatch.IsLastBatch);
				}
			}
			return null;
		}

		// Token: 0x06000059 RID: 89 RVA: 0x00004494 File Offset: 0x00002694
		SessionStatistics IMailbox.GetSessionStatistics(SessionStatisticsFlags statisticsTypes)
		{
			MrsTracer.ProxyClient.Function("RemoteMailbox.GetSessionStatistics()", new object[0]);
			if (!base.ServerVersion[31])
			{
				return new SessionStatistics();
			}
			return base.MrsProxy.IMailbox_GetSessionStatistics(base.Handle, (int)statisticsTypes);
		}

		// Token: 0x0600005A RID: 90 RVA: 0x000044D2 File Offset: 0x000026D2
		Guid IMailbox.StartIsInteg(List<uint> mailboxCorruptionTypes)
		{
			MrsTracer.ProxyClient.Function("RemoteSourceMailbox.StartIsInteg()", new object[0]);
			this.VerifyMailboxConnection();
			return base.MrsProxy.IMailbox_StartIsInteg(base.Handle, mailboxCorruptionTypes);
		}

		// Token: 0x0600005B RID: 91 RVA: 0x00004501 File Offset: 0x00002701
		List<StoreIntegrityCheckJob> IMailbox.QueryIsInteg(Guid isIntegRequestGuid)
		{
			MrsTracer.ProxyClient.Function("RemoteSourceMailbox.QueryIsInteg()", new object[0]);
			this.VerifyMailboxConnection();
			return base.MrsProxy.IMailbox_QueryIsInteg(base.Handle, isIntegRequestGuid);
		}

		// Token: 0x0600005C RID: 92 RVA: 0x00004530 File Offset: 0x00002730
		protected void VerifyMailboxConnection()
		{
			if (!((IMailbox)this).IsConnected())
			{
				throw new NotConnectedPermanentException();
			}
		}

		// Token: 0x0600005D RID: 93 RVA: 0x00004540 File Offset: 0x00002740
		protected override void InternalDispose(bool disposing)
		{
			if (disposing)
			{
				((IMailbox)this).Disconnect();
			}
		}

		// Token: 0x04000009 RID: 9
		private static readonly TimeSpan DelaySamplingIntervalForMRSProxy = TimeSpan.FromSeconds(60.0);

		// Token: 0x0400000A RID: 10
		private static readonly LocalMailboxFlags PreE15LocalMailboxFlags = LocalMailboxFlags.StripLargeRulesForDownlevelTargets | LocalMailboxFlags.UseHomeMDB | LocalMailboxFlags.PureMAPI | LocalMailboxFlags.CredentialIsNotAdmin | LocalMailboxFlags.UseNTLMAuth | LocalMailboxFlags.ConnectToMoMT | LocalMailboxFlags.LegacyPublicFolders | LocalMailboxFlags.Restore;

		// Token: 0x0400000B RID: 11
		private readonly NetworkCredential remoteCred;

		// Token: 0x0400000C RID: 12
		private readonly string remoteOrgName;

		// Token: 0x0400000D RID: 13
		private readonly ProxyControlFlags proxyControlFlags;

		// Token: 0x0400000E RID: 14
		private readonly bool useHttps;

		// Token: 0x0400000F RID: 15
		private readonly TimeSpan longOperationTimeout;

		// Token: 0x04000010 RID: 16
		private Dictionary<MailboxCapabilities, bool> mailboxCapabilities = new Dictionary<MailboxCapabilities, bool>();

		// Token: 0x04000011 RID: 17
		private string serverName;

		// Token: 0x04000012 RID: 18
		private string preferredDomainControllerName;

		// Token: 0x04000013 RID: 19
		private string filePath;

		// Token: 0x04000014 RID: 20
		private IReservation reservation;

		// Token: 0x04000015 RID: 21
		private Guid? mailboxContainerGuid;

		// Token: 0x04000016 RID: 22
		private Guid primaryMailboxGuid;

		// Token: 0x04000017 RID: 23
		private Guid physicalMailboxGuid;

		// Token: 0x04000018 RID: 24
		private TenantPartitionHint partitionHint;

		// Token: 0x04000019 RID: 25
		private Guid mdbGuid;

		// Token: 0x0400001A RID: 26
		private string mdbName;

		// Token: 0x0400001B RID: 27
		private MailboxType mbxType;

		// Token: 0x0400001C RID: 28
		private LocalMailboxFlags flags;

		// Token: 0x0400001D RID: 29
		private MailboxRestoreType restoreType;

		// Token: 0x0400001E RID: 30
		private IEnumerable<MRSProxyCapabilities> requiredCapabilities;

		// Token: 0x0400001F RID: 31
		private int? contentCodePage;

		// Token: 0x04000020 RID: 32
		private RemoteMailbox.EasConfiguration easConfiguration;

		// Token: 0x04000021 RID: 33
		private OlcMailboxConfiguration olcConfig;

		// Token: 0x02000008 RID: 8
		private class EasConfiguration
		{
			// Token: 0x06000060 RID: 96 RVA: 0x0000456A File Offset: 0x0000276A
			public EasConfiguration(NetworkCredential userCredential, SmtpAddress smtpAddress, string remoteHostName)
			{
				this.UserCred = userCredential;
				this.SmtpAddress = smtpAddress.ToString();
				this.RemoteHostName = remoteHostName;
			}

			// Token: 0x17000009 RID: 9
			// (get) Token: 0x06000061 RID: 97 RVA: 0x00004593 File Offset: 0x00002793
			// (set) Token: 0x06000062 RID: 98 RVA: 0x0000459B File Offset: 0x0000279B
			public NetworkCredential UserCred { get; private set; }

			// Token: 0x1700000A RID: 10
			// (get) Token: 0x06000063 RID: 99 RVA: 0x000045A4 File Offset: 0x000027A4
			// (set) Token: 0x06000064 RID: 100 RVA: 0x000045AC File Offset: 0x000027AC
			public string SmtpAddress { get; private set; }

			// Token: 0x1700000B RID: 11
			// (get) Token: 0x06000065 RID: 101 RVA: 0x000045B5 File Offset: 0x000027B5
			// (set) Token: 0x06000066 RID: 102 RVA: 0x000045BD File Offset: 0x000027BD
			public string RemoteHostName { get; private set; }
		}
	}
}
