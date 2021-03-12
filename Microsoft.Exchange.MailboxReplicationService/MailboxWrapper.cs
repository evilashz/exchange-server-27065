using System;
using System.Collections.Generic;
using System.Globalization;
using System.Net;
using System.Security.AccessControl;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Mapi;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x020000A4 RID: 164
	internal abstract class MailboxWrapper : WrapperBase<IMailbox>, IFilterBuilderHelper, IMailbox, IDisposable
	{
		// Token: 0x06000841 RID: 2113 RVA: 0x00038974 File Offset: 0x00036B74
		public MailboxWrapper(IMailbox mailbox, MailboxWrapperFlags flags, LocalizedString tracingId) : base(mailbox, null)
		{
			this.Flags = flags;
			this.tracingContext = new WrappedDataContext(tracingId);
			this.mailboxVersion = null;
			base.CreateContext = new CommonUtils.CreateContextDelegate(this.CreateDataContext);
			this.NamedPropMapper = new NamedPropMapper(this.Mailbox, (this.Flags & MailboxWrapperFlags.Target) != (MailboxWrapperFlags)0);
			this.PrincipalMapper = new PrincipalMapper(this.Mailbox);
		}

		// Token: 0x170001D3 RID: 467
		// (get) Token: 0x06000842 RID: 2114 RVA: 0x000389FB File Offset: 0x00036BFB
		public IMailbox Mailbox
		{
			get
			{
				return this;
			}
		}

		// Token: 0x170001D4 RID: 468
		// (get) Token: 0x06000843 RID: 2115 RVA: 0x000389FE File Offset: 0x00036BFE
		// (set) Token: 0x06000844 RID: 2116 RVA: 0x00038A06 File Offset: 0x00036C06
		public MailboxServerInformation LastConnectedServerInfo { get; private set; }

		// Token: 0x170001D5 RID: 469
		// (get) Token: 0x06000845 RID: 2117 RVA: 0x00038A0F File Offset: 0x00036C0F
		// (set) Token: 0x06000846 RID: 2118 RVA: 0x00038A17 File Offset: 0x00036C17
		public MailboxWrapperFlags Flags { get; private set; }

		// Token: 0x170001D6 RID: 470
		// (get) Token: 0x06000847 RID: 2119 RVA: 0x00038A20 File Offset: 0x00036C20
		// (set) Token: 0x06000848 RID: 2120 RVA: 0x00038A28 File Offset: 0x00036C28
		public NamedPropMapper NamedPropMapper { get; private set; }

		// Token: 0x170001D7 RID: 471
		// (get) Token: 0x06000849 RID: 2121 RVA: 0x00038A31 File Offset: 0x00036C31
		// (set) Token: 0x0600084A RID: 2122 RVA: 0x00038A39 File Offset: 0x00036C39
		public PrincipalMapper PrincipalMapper { get; private set; }

		// Token: 0x170001D8 RID: 472
		// (get) Token: 0x0600084B RID: 2123 RVA: 0x00038A42 File Offset: 0x00036C42
		// (set) Token: 0x0600084C RID: 2124 RVA: 0x00038A4A File Offset: 0x00036C4A
		public FolderMap FolderMap { get; private set; }

		// Token: 0x170001D9 RID: 473
		// (get) Token: 0x0600084D RID: 2125 RVA: 0x00038A53 File Offset: 0x00036C53
		// (set) Token: 0x0600084E RID: 2126 RVA: 0x00038A5B File Offset: 0x00036C5B
		public bool HasMapiSession { get; private set; }

		// Token: 0x170001DA RID: 474
		// (get) Token: 0x0600084F RID: 2127 RVA: 0x00038A64 File Offset: 0x00036C64
		public int? MailboxVersion
		{
			get
			{
				if (this.mailboxVersion == null && this.Mailbox.IsConnected())
				{
					this.Mailbox.GetMailboxInformation();
				}
				return this.mailboxVersion;
			}
		}

		// Token: 0x170001DB RID: 475
		// (get) Token: 0x06000850 RID: 2128 RVA: 0x00038A94 File Offset: 0x00036C94
		public CultureInfo MailboxCulture
		{
			get
			{
				int num = 0;
				PropValueData[] props = this.Mailbox.GetProps(new PropTag[]
				{
					PropTag.LocaleId
				});
				if (1721827331 == props[0].PropTag)
				{
					num = (int)props[0].Value;
				}
				CultureInfo cultureInfo;
				if (num == 0 || num == 1024 || num == 127 || num == CultureInfo.InvariantCulture.LCID)
				{
					cultureInfo = new CultureInfo("en-US");
				}
				else
				{
					cultureInfo = CultureInfo.GetCultureInfo(num);
				}
				MrsTracer.Service.Debug("Destination MailboxCulture: LCID = {0}, Culture = {1}", new object[]
				{
					num,
					(cultureInfo == null) ? "null" : cultureInfo.ToString()
				});
				return cultureInfo;
			}
		}

		// Token: 0x170001DC RID: 476
		// (get) Token: 0x06000851 RID: 2129
		protected abstract OperationSideDataContext SideOperationContext { get; }

		// Token: 0x06000852 RID: 2130 RVA: 0x00038B45 File Offset: 0x00036D45
		public void LoadFolderMap(GetFolderMapFlags flags, Func<FolderMap> getFolderMap)
		{
			if (this.FolderMap != null && !flags.HasFlag(GetFolderMapFlags.ForceRefresh))
			{
				return;
			}
			this.FolderMap = getFolderMap();
		}

		// Token: 0x06000853 RID: 2131 RVA: 0x00038B6F File Offset: 0x00036D6F
		public List<FolderRecWrapper> LoadFolders<TFolderRec>(EnumerateFolderHierarchyFlags enumerateFolderHierarchyFlags, PropTag[] additionalPtags) where TFolderRec : FolderRecWrapper, new()
		{
			return FolderRecWrapper.WrapList<TFolderRec>(this.Mailbox.EnumerateFolderHierarchy(enumerateFolderHierarchyFlags, additionalPtags));
		}

		// Token: 0x06000854 RID: 2132
		public abstract IFolder GetFolder(byte[] folderId);

		// Token: 0x06000855 RID: 2133 RVA: 0x00038B84 File Offset: 0x00036D84
		public void Ping()
		{
			using (IFolder folder = this.GetFolder(null))
			{
				folder.GetProps(MailboxWrapper.pingProperties);
			}
		}

		// Token: 0x06000856 RID: 2134 RVA: 0x00038BC4 File Offset: 0x00036DC4
		public void UpdateLastConnectedServerInfo(out MailboxServerInformation serverInfo, out bool hasDatabaseFailedOver)
		{
			serverInfo = this.Mailbox.GetMailboxServerInformation();
			hasDatabaseFailedOver = false;
			if (serverInfo != null)
			{
				if (this.LastConnectedServerInfo != null && serverInfo.MailboxServerGuid != Guid.Empty && this.LastConnectedServerInfo.MailboxServerGuid != serverInfo.MailboxServerGuid)
				{
					this.LastConnectedServerInfo = null;
					hasDatabaseFailedOver = true;
					return;
				}
				this.LastConnectedServerInfo = serverInfo;
			}
		}

		// Token: 0x06000857 RID: 2135 RVA: 0x00038C2C File Offset: 0x00036E2C
		public virtual void Clear()
		{
			this.FolderMap = null;
		}

		// Token: 0x06000858 RID: 2136 RVA: 0x00038C35 File Offset: 0x00036E35
		PropTag IFilterBuilderHelper.MapNamedProperty(NamedPropData npd, PropType propType)
		{
			return this.NamedPropMapper.MapNamedProp(npd, propType);
		}

		// Token: 0x06000859 RID: 2137 RVA: 0x00038C44 File Offset: 0x00036E44
		Guid[] IFilterBuilderHelper.MapPolicyTag(string policyTagStr)
		{
			return this.Mailbox.ResolvePolicyTag(policyTagStr);
		}

		// Token: 0x0600085A RID: 2138 RVA: 0x00038C54 File Offset: 0x00036E54
		string[] IFilterBuilderHelper.MapRecipient(string recipientId)
		{
			MappedPrincipal mappedPrincipal = new MappedPrincipal();
			mappedPrincipal.Alias = recipientId;
			MappedPrincipal[] array = this.Mailbox.ResolvePrincipals(new MappedPrincipal[]
			{
				mappedPrincipal
			});
			if (array == null || array[0] == null)
			{
				return null;
			}
			List<string> list = new List<string>();
			for (mappedPrincipal = array[0]; mappedPrincipal != null; mappedPrincipal = mappedPrincipal.NextEntry)
			{
				if (!string.IsNullOrEmpty(mappedPrincipal.Alias))
				{
					list.Add(mappedPrincipal.Alias);
				}
				if (!string.IsNullOrEmpty(mappedPrincipal.DisplayName))
				{
					list.Add(mappedPrincipal.DisplayName);
				}
				if (!string.IsNullOrEmpty(mappedPrincipal.LegacyDN))
				{
					list.Add(mappedPrincipal.LegacyDN);
				}
				ProxyAddressCollection proxyAddressCollection = new ProxyAddressCollection(mappedPrincipal.ProxyAddresses);
				ProxyAddress proxyAddress = proxyAddressCollection.FindPrimary(ProxyAddressPrefix.Smtp);
				if (proxyAddress != null && !string.IsNullOrEmpty(proxyAddress.AddressString))
				{
					list.Add(proxyAddress.AddressString);
				}
			}
			return list.ToArray();
		}

		// Token: 0x0600085B RID: 2139 RVA: 0x00038D60 File Offset: 0x00036F60
		LatencyInfo IMailbox.GetLatencyInfo()
		{
			LatencyInfo result = new LatencyInfo();
			base.CreateContext("IMailbox.GetLatency", new DataContext[0]).Execute(delegate
			{
				result = this.WrappedObject.GetLatencyInfo();
			}, true);
			return result;
		}

		// Token: 0x0600085C RID: 2140 RVA: 0x00038DD4 File Offset: 0x00036FD4
		bool IMailbox.IsConnected()
		{
			bool result = false;
			base.CreateContext("IMailbox.IsConnected", new DataContext[0]).Execute(delegate
			{
				result = this.WrappedObject.IsConnected();
			}, true);
			return result;
		}

		// Token: 0x0600085D RID: 2141 RVA: 0x00038E50 File Offset: 0x00037050
		void IMailbox.ConfigADConnection(string domainControllerName, string configDomainControllerName, NetworkCredential cred)
		{
			base.CreateContext("IMailbox.ConfigADConnection", new DataContext[0]).Execute(delegate
			{
				this.WrappedObject.ConfigADConnection(domainControllerName, configDomainControllerName, cred);
			}, true);
		}

		// Token: 0x0600085E RID: 2142 RVA: 0x00038EC8 File Offset: 0x000370C8
		void IMailbox.ConfigPreferredADConnection(string preferredDomainControllerName)
		{
			base.CreateContext("IMailbox.ConfigPreferredADConnection", new DataContext[]
			{
				new SimpleValueDataContext("preferredDomainControllerName", preferredDomainControllerName)
			}).Execute(delegate
			{
				this.WrappedObject.ConfigPreferredADConnection(preferredDomainControllerName);
			}, true);
		}

		// Token: 0x0600085F RID: 2143 RVA: 0x00038F6C File Offset: 0x0003716C
		void IMailbox.Config(IReservation reservation, Guid primaryMailboxGuid, Guid physicalMailboxGuid, TenantPartitionHint partitionHint, Guid mdbGuid, MailboxType mbxType, Guid? mailboxContainerGuid)
		{
			base.CreateContext("IMailbox.Config", new DataContext[0]).Execute(delegate
			{
				this.WrappedObject.Config(reservation, primaryMailboxGuid, physicalMailboxGuid, partitionHint, mdbGuid, mbxType, mailboxContainerGuid);
			}, true);
		}

		// Token: 0x06000860 RID: 2144 RVA: 0x0003900C File Offset: 0x0003720C
		void IMailbox.ConfigPst(string filePath, int? contentCodePage)
		{
			base.CreateContext("IMailbox.ConfigPst", new DataContext[0]).Execute(delegate
			{
				this.WrappedObject.ConfigPst(filePath, contentCodePage);
			}, true);
		}

		// Token: 0x06000861 RID: 2145 RVA: 0x00039090 File Offset: 0x00037290
		void IMailbox.ConfigEas(NetworkCredential userCredential, SmtpAddress smtpAddress, Guid mailboxGuid, string remoteHostName)
		{
			base.CreateContext("IMailbox.ConfigEas", new DataContext[0]).Execute(delegate
			{
				this.WrappedObject.ConfigEas(userCredential, smtpAddress, mailboxGuid, remoteHostName);
			}, true);
		}

		// Token: 0x06000862 RID: 2146 RVA: 0x00039110 File Offset: 0x00037310
		void IMailbox.ConfigRestore(MailboxRestoreType restoreFlags)
		{
			base.CreateContext("IMailbox.ConfigRestore", new DataContext[0]).Execute(delegate
			{
				this.WrappedObject.ConfigRestore(restoreFlags);
			}, true);
		}

		// Token: 0x06000863 RID: 2147 RVA: 0x0003917C File Offset: 0x0003737C
		void IMailbox.ConfigMDBByName(string mdbName)
		{
			base.CreateContext("IMailbox.ConfigMDBByName", new DataContext[0]).Execute(delegate
			{
				this.WrappedObject.ConfigMDBByName(mdbName);
			}, true);
		}

		// Token: 0x06000864 RID: 2148 RVA: 0x000391E8 File Offset: 0x000373E8
		void IMailbox.ConfigOlc(OlcMailboxConfiguration config)
		{
			base.CreateContext("IMailbox.ConfigOlc", new DataContext[0]).Execute(delegate
			{
				this.WrappedObject.ConfigOlc(config);
			}, true);
		}

		// Token: 0x06000865 RID: 2149 RVA: 0x00039254 File Offset: 0x00037454
		void IMailbox.ConfigMailboxOptions(MailboxOptions options)
		{
			base.CreateContext("IMailbox.ConfigMailboxOptions", new DataContext[0]).Execute(delegate
			{
				this.WrappedObject.ConfigMailboxOptions(options);
			}, true);
		}

		// Token: 0x06000866 RID: 2150 RVA: 0x000392C0 File Offset: 0x000374C0
		MailboxInformation IMailbox.GetMailboxInformation()
		{
			MailboxInformation result = null;
			base.CreateContext("IMailbox.GetMailboxInformation", new DataContext[0]).Execute(delegate
			{
				result = this.WrappedObject.GetMailboxInformation();
			}, true);
			if (result != null)
			{
				this.mailboxVersion = new int?(result.ServerVersion);
			}
			return result;
		}

		// Token: 0x06000867 RID: 2151 RVA: 0x00039350 File Offset: 0x00037550
		void IMailbox.Connect(MailboxConnectFlags connectFlags)
		{
			this.CreateDataContextWithType("IMailbox.Connect", OperationType.Connect, new DataContext[]
			{
				new SimpleValueDataContext("Flags", connectFlags)
			}).Execute(delegate
			{
				this.WrappedObject.Connect(connectFlags);
			}, true);
			this.HasMapiSession = !connectFlags.HasFlag(MailboxConnectFlags.DoNotOpenMapiSession);
		}

		// Token: 0x06000868 RID: 2152 RVA: 0x000393F4 File Offset: 0x000375F4
		bool IMailbox.IsCapabilitySupported(MRSProxyCapabilities capability)
		{
			bool result = false;
			base.CreateContext("IMailbox.IsCapabilitySupported", new DataContext[]
			{
				new SimpleValueDataContext("Capability", capability)
			}).Execute(delegate
			{
				result = this.WrappedObject.IsCapabilitySupported(capability);
			}, true);
			return result;
		}

		// Token: 0x06000869 RID: 2153 RVA: 0x00039500 File Offset: 0x00037700
		bool IMailbox.IsMailboxCapabilitySupported(MailboxCapabilities capability)
		{
			bool result = false;
			base.CreateContext("IMailbox.IsMailboxCapabilitySupported", new DataContext[]
			{
				new SimpleValueDataContext("Capability", capability)
			}).Execute(delegate
			{
				bool result;
				if (this.mailboxCapabilities.TryGetValue((int)capability, out result))
				{
					result = result;
					return;
				}
				MailboxCapabilities capability2 = capability;
				MailboxCapabilities capability3 = capability;
				MailboxCapabilities capability4 = capability;
				MailboxCapabilities capability5 = capability;
				result = this.WrappedObject.IsMailboxCapabilitySupported(capability);
				this.mailboxCapabilities.Add((int)capability, result);
			}, true);
			return result;
		}

		// Token: 0x0600086A RID: 2154 RVA: 0x00039580 File Offset: 0x00037780
		void IMailbox.Disconnect()
		{
			this.mailboxVersion = null;
			this.NamedPropMapper.Clear();
			this.PrincipalMapper.Clear();
			this.HasMapiSession = false;
			base.CreateContext("IMailbox.Disconnect", new DataContext[0]).Execute(delegate
			{
				base.WrappedObject.Disconnect();
			}, true);
		}

		// Token: 0x0600086B RID: 2155 RVA: 0x00039604 File Offset: 0x00037804
		void IMailbox.SetInTransitStatus(InTransitStatus status, out bool onlineMoveSupported)
		{
			bool onlineMoveSupportedInt = false;
			base.CreateContext("IMailbox.SetInTransitStatus", new DataContext[]
			{
				new SimpleValueDataContext("Status", status)
			}).Execute(delegate
			{
				this.WrappedObject.SetInTransitStatus(status, out onlineMoveSupportedInt);
			}, true);
			onlineMoveSupported = onlineMoveSupportedInt;
		}

		// Token: 0x0600086C RID: 2156 RVA: 0x00039683 File Offset: 0x00037883
		void IMailbox.SeedMBICache()
		{
			base.CreateContext("IMailbox.SeedMBICache", new DataContext[0]).Execute(delegate
			{
				base.WrappedObject.SeedMBICache();
			}, true);
		}

		// Token: 0x0600086D RID: 2157 RVA: 0x000396D4 File Offset: 0x000378D4
		List<WellKnownFolder> IMailbox.DiscoverWellKnownFolders(int flags)
		{
			List<WellKnownFolder> wellKnownFolders = null;
			base.CreateContext("IMailbox.DiscoverWellKnownFolders", new DataContext[0]).Execute(delegate
			{
				wellKnownFolders = this.WrappedObject.DiscoverWellKnownFolders(flags);
			}, true);
			return wellKnownFolders;
		}

		// Token: 0x0600086E RID: 2158 RVA: 0x0003974C File Offset: 0x0003794C
		MailboxServerInformation IMailbox.GetMailboxServerInformation()
		{
			MailboxServerInformation result = null;
			base.CreateContext("IMailbox.GetMailboxServerInformation", new DataContext[0]).Execute(delegate
			{
				result = this.WrappedObject.GetMailboxServerInformation();
			}, true);
			return result;
		}

		// Token: 0x0600086F RID: 2159 RVA: 0x000397B0 File Offset: 0x000379B0
		VersionInformation IMailbox.GetVersion()
		{
			if (this.providerVersion == null)
			{
				base.CreateContext("IMailbox.GetVersion", new DataContext[0]).Execute(delegate
				{
					this.providerVersion = base.WrappedObject.GetVersion();
				}, true);
			}
			return this.providerVersion;
		}

		// Token: 0x06000870 RID: 2160 RVA: 0x0003981C File Offset: 0x00037A1C
		void IMailbox.SetOtherSideVersion(VersionInformation otherSideVersion)
		{
			base.CreateContext("IMailbox.SetOtherSideMailboxServerInformation", new DataContext[0]).Execute(delegate
			{
				this.WrappedObject.SetOtherSideVersion(otherSideVersion);
			}, true);
		}

		// Token: 0x06000871 RID: 2161 RVA: 0x00039894 File Offset: 0x00037A94
		List<FolderRec> IMailbox.EnumerateFolderHierarchy(EnumerateFolderHierarchyFlags flags, PropTag[] additionalPtagsToLoad)
		{
			List<FolderRec> result = null;
			base.CreateContext("IMailbox.EnumerateFolderHierarchy", new DataContext[]
			{
				new PropTagsDataContext(additionalPtagsToLoad)
			}).Execute(delegate
			{
				result = this.WrappedObject.EnumerateFolderHierarchy(flags, additionalPtagsToLoad);
			}, true);
			return result;
		}

		// Token: 0x06000872 RID: 2162 RVA: 0x00039924 File Offset: 0x00037B24
		void IMailbox.DeleteMailbox(int flags)
		{
			base.CreateContext("IMailbox.DeleteMailbox", new DataContext[]
			{
				new SimpleValueDataContext("Flags", flags)
			}).Execute(delegate
			{
				this.WrappedObject.DeleteMailbox(flags);
			}, true);
		}

		// Token: 0x06000873 RID: 2163 RVA: 0x000399B0 File Offset: 0x00037BB0
		NamedPropData[] IMailbox.GetNamesFromIDs(PropTag[] pta)
		{
			NamedPropData[] result = null;
			base.CreateContext("IMailbox.GetNamesFromIDs", new DataContext[]
			{
				new PropTagsDataContext(pta)
			}).Execute(delegate
			{
				result = this.WrappedObject.GetNamesFromIDs(pta);
			}, true);
			return result;
		}

		// Token: 0x06000874 RID: 2164 RVA: 0x00039A44 File Offset: 0x00037C44
		PropTag[] IMailbox.GetIDsFromNames(bool createIfNotExists, NamedPropData[] npa)
		{
			PropTag[] result = null;
			base.CreateContext("IMailbox.GetIDsFromNames", new DataContext[0]).Execute(delegate
			{
				result = this.WrappedObject.GetIDsFromNames(createIfNotExists, npa);
			}, true);
			return result;
		}

		// Token: 0x06000875 RID: 2165 RVA: 0x00039AC8 File Offset: 0x00037CC8
		byte[] IMailbox.GetSessionSpecificEntryId(byte[] entryId)
		{
			byte[] result = null;
			base.CreateContext("IMailbox.GetSessionSpecificEntryId", new DataContext[0]).Execute(delegate
			{
				result = this.WrappedObject.GetSessionSpecificEntryId(entryId);
			}, true);
			return result;
		}

		// Token: 0x06000876 RID: 2166 RVA: 0x00039B44 File Offset: 0x00037D44
		MappedPrincipal[] IMailbox.ResolvePrincipals(MappedPrincipal[] principals)
		{
			MappedPrincipal[] result = null;
			base.CreateContext("IMailbox.ResolvePrincipals", new DataContext[0]).Execute(delegate
			{
				result = this.WrappedObject.ResolvePrincipals(principals);
			}, true);
			return result;
		}

		// Token: 0x06000877 RID: 2167 RVA: 0x00039BC0 File Offset: 0x00037DC0
		bool IMailbox.UpdateRemoteHostName(string value)
		{
			bool result = false;
			base.CreateContext("IMailbox.UpdateRemoteHostName", new DataContext[0]).Execute(delegate
			{
				result = this.WrappedObject.UpdateRemoteHostName(value);
			}, true);
			return result;
		}

		// Token: 0x06000878 RID: 2168 RVA: 0x00039C38 File Offset: 0x00037E38
		ADUser IMailbox.GetADUser()
		{
			ADUser result = null;
			base.CreateContext("IMailbox.GetADUser", new DataContext[0]).Execute(delegate
			{
				result = this.WrappedObject.GetADUser();
			}, true);
			return result;
		}

		// Token: 0x06000879 RID: 2169 RVA: 0x00039CF0 File Offset: 0x00037EF0
		void IMailbox.UpdateMovedMailbox(UpdateMovedMailboxOperation op, ADUser remoteRecipientData, string domainController, out ReportEntry[] entries, Guid newDatabaseGuid, Guid newArchiveDatabaseGuid, string archiveDomain, ArchiveStatusFlags archiveStatus, UpdateMovedMailboxFlags updateMovedMailboxFlags, Guid? newMailboxContainerGuid, CrossTenantObjectId newUnifiedMailboxId)
		{
			ReportEntry[] entriesInt = null;
			base.CreateContext("IMailbox.UpdateMovedMailbox", new DataContext[0]).Execute(delegate
			{
				this.WrappedObject.UpdateMovedMailbox(op, remoteRecipientData, domainController, out entriesInt, newDatabaseGuid, newArchiveDatabaseGuid, archiveDomain, archiveStatus, updateMovedMailboxFlags, newMailboxContainerGuid, newUnifiedMailboxId);
			}, true);
			entries = entriesInt;
		}

		// Token: 0x0600087A RID: 2170 RVA: 0x00039DB0 File Offset: 0x00037FB0
		RawSecurityDescriptor IMailbox.GetMailboxSecurityDescriptor()
		{
			RawSecurityDescriptor result = null;
			base.CreateContext("IMailbox.GetMailboxSecurityDescriptor", new DataContext[0]).Execute(delegate
			{
				result = this.WrappedObject.GetMailboxSecurityDescriptor();
			}, true);
			return result;
		}

		// Token: 0x0600087B RID: 2171 RVA: 0x00039E20 File Offset: 0x00038020
		RawSecurityDescriptor IMailbox.GetUserSecurityDescriptor()
		{
			RawSecurityDescriptor result = null;
			base.CreateContext("IMailbox.GetUserSecurityDescriptor", new DataContext[0]).Execute(delegate
			{
				result = this.WrappedObject.GetUserSecurityDescriptor();
			}, true);
			return result;
		}

		// Token: 0x0600087C RID: 2172 RVA: 0x00039E98 File Offset: 0x00038098
		void IMailbox.AddMoveHistoryEntry(MoveHistoryEntryInternal mhei, int maxMoveHistoryLength)
		{
			base.CreateContext("IMailbox.AddMoveHistoryEntry", new DataContext[0]).Execute(delegate
			{
				this.WrappedObject.AddMoveHistoryEntry(mhei, maxMoveHistoryLength);
			}, true);
		}

		// Token: 0x0600087D RID: 2173 RVA: 0x00039F08 File Offset: 0x00038108
		ServerHealthStatus IMailbox.CheckServerHealth()
		{
			ServerHealthStatus result = null;
			base.CreateContext("IMailbox.CheckServerHealth", new DataContext[0]).Execute(delegate
			{
				result = this.WrappedObject.CheckServerHealth();
			}, true);
			return result;
		}

		// Token: 0x0600087E RID: 2174 RVA: 0x00039F80 File Offset: 0x00038180
		PropValueData[] IMailbox.GetProps(PropTag[] ptags)
		{
			PropValueData[] result = null;
			base.CreateContext("IMailbox.GetProps", new DataContext[]
			{
				new PropTagsDataContext(ptags)
			}).Execute(delegate
			{
				result = this.WrappedObject.GetProps(ptags);
			}, true);
			return result;
		}

		// Token: 0x0600087F RID: 2175 RVA: 0x0003A00C File Offset: 0x0003820C
		byte[] IMailbox.GetReceiveFolderEntryId(string msgClass)
		{
			byte[] result = null;
			base.CreateContext("IMailbox.GetReceiveFolderEntryId", new DataContext[]
			{
				new SimpleValueDataContext("MsgClass", msgClass)
			}).Execute(delegate
			{
				result = this.WrappedObject.GetReceiveFolderEntryId(msgClass);
			}, true);
			return result;
		}

		// Token: 0x06000880 RID: 2176 RVA: 0x0003A0A0 File Offset: 0x000382A0
		Guid[] IMailbox.ResolvePolicyTag(string policyTagStr)
		{
			Guid[] result = null;
			base.CreateContext("IMailbox.ResolvePolicyTag", new DataContext[]
			{
				new SimpleValueDataContext("PolicyTag", policyTagStr)
			}).Execute(delegate
			{
				result = this.WrappedObject.ResolvePolicyTag(policyTagStr);
			}, true);
			return result;
		}

		// Token: 0x06000881 RID: 2177 RVA: 0x0003A134 File Offset: 0x00038334
		string IMailbox.LoadSyncState(byte[] key)
		{
			string result = null;
			base.CreateContext("IMailbox.LoadSyncState", new DataContext[]
			{
				new SimpleValueDataContext("Key", TraceUtils.DumpBytes(key))
			}).Execute(delegate
			{
				result = this.WrappedObject.LoadSyncState(key);
			}, true);
			return result;
		}

		// Token: 0x06000882 RID: 2178 RVA: 0x0003A1D0 File Offset: 0x000383D0
		MessageRec IMailbox.SaveSyncState(byte[] key, string syncState)
		{
			MessageRec result = null;
			base.CreateContext("IMailbox.SaveSyncState", new DataContext[]
			{
				new SimpleValueDataContext("Key", TraceUtils.DumpBytes(key)),
				new SimpleValueDataContext("SyncStateLength", (syncState != null) ? syncState.Length : 0)
			}).Execute(delegate
			{
				result = this.WrappedObject.SaveSyncState(key, syncState);
			}, true);
			return result;
		}

		// Token: 0x06000883 RID: 2179 RVA: 0x0003A298 File Offset: 0x00038498
		SessionStatistics IMailbox.GetSessionStatistics(SessionStatisticsFlags statisticsTypes)
		{
			SessionStatistics result = null;
			base.CreateContext("IDestinationMailbox.GetSessionStatistics", new DataContext[0]).Execute(delegate
			{
				result = this.WrappedObject.GetSessionStatistics(statisticsTypes);
			}, true);
			return result;
		}

		// Token: 0x06000884 RID: 2180 RVA: 0x0003A314 File Offset: 0x00038514
		Guid IMailbox.StartIsInteg(List<uint> mailboxCorruptionTypes)
		{
			Guid result = Guid.Empty;
			base.CreateContext("IMailbox.StartIsInteg", new DataContext[0]).Execute(delegate
			{
				result = this.WrappedObject.StartIsInteg(mailboxCorruptionTypes);
			}, true);
			return result;
		}

		// Token: 0x06000885 RID: 2181 RVA: 0x0003A394 File Offset: 0x00038594
		List<StoreIntegrityCheckJob> IMailbox.QueryIsInteg(Guid isIntegRequestGuid)
		{
			List<StoreIntegrityCheckJob> jobs = null;
			base.CreateContext("IMailbox.QueryIsInteg", new DataContext[0]).Execute(delegate
			{
				jobs = this.WrappedObject.QueryIsInteg(isIntegRequestGuid);
			}, true);
			return jobs;
		}

		// Token: 0x06000886 RID: 2182 RVA: 0x0003A418 File Offset: 0x00038618
		public virtual List<ItemPropertiesBase> GetMailboxSettings(GetMailboxSettingsFlags flags)
		{
			List<ItemPropertiesBase> result = null;
			if (base.WrappedObject is ISourceMailbox)
			{
				base.CreateContext("ISourceMailbox.GetMailboxSettings", new DataContext[0]).Execute(delegate
				{
					result = ((ISourceMailbox)this.WrappedObject).GetMailboxSettings(flags);
				}, true);
			}
			return result;
		}

		// Token: 0x06000887 RID: 2183 RVA: 0x0003A482 File Offset: 0x00038682
		private ExecutionContextWrapper CreateDataContext(string callName, params DataContext[] additionalContexts)
		{
			return this.CreateDataContextWithType(callName, OperationType.None, additionalContexts);
		}

		// Token: 0x06000888 RID: 2184 RVA: 0x0003A490 File Offset: 0x00038690
		private ExecutionContextWrapper CreateDataContextWithType(string callName, OperationType operationType, params DataContext[] additionalContexts)
		{
			List<DataContext> list = new List<DataContext>();
			list.Add(new OperationDataContext(callName, operationType));
			list.Add(this.SideOperationContext);
			list.Add(this.tracingContext);
			if (additionalContexts != null)
			{
				list.AddRange(additionalContexts);
			}
			return new ExecutionContextWrapper(new CommonUtils.UpdateDuration(base.UpdateDuration), callName, list.ToArray());
		}

		// Token: 0x04000322 RID: 802
		private static PropTag[] pingProperties = new PropTag[]
		{
			PropTag.LocalCommitTimeMax,
			PropTag.ContentCount,
			PropTag.DeletedCountTotal
		};

		// Token: 0x04000323 RID: 803
		private WrappedDataContext tracingContext;

		// Token: 0x04000324 RID: 804
		private int? mailboxVersion;

		// Token: 0x04000325 RID: 805
		private readonly Dictionary<int, bool> mailboxCapabilities = new Dictionary<int, bool>(7);

		// Token: 0x04000326 RID: 806
		private VersionInformation providerVersion;
	}
}
