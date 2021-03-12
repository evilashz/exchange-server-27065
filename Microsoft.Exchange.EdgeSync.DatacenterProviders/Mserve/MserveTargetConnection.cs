using System;
using System.Collections.Generic;
using System.DirectoryServices.Protocols;
using System.Globalization;
using System.Text;
using Microsoft.Exchange.Common.Cache;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Diagnostics.Components.Mserve;
using Microsoft.Exchange.EdgeSync.Common;
using Microsoft.Exchange.EdgeSync.Datacenter;
using Microsoft.Exchange.EdgeSync.Logging;
using Microsoft.Exchange.Net.Mserve;

namespace Microsoft.Exchange.EdgeSync.Mserve
{
	// Token: 0x02000036 RID: 54
	internal class MserveTargetConnection : DatacenterTargetConnection
	{
		// Token: 0x06000255 RID: 597 RVA: 0x0000FDC4 File Offset: 0x0000DFC4
		public MserveTargetConnection(int localServerVersion, MserveTargetServerConfig config, EnhancedTimeSpan syncInterval, TestShutdownAndLeaseDelegate testShutdownAndLease, EdgeSyncLogSession logSession) : base(localServerVersion, config, syncInterval, logSession, ExTraceGlobals.TargetConnectionTracer)
		{
			this.testShutdownAndLease = testShutdownAndLease;
			this.config = config;
			this.InitializeTenantMEUSyncControlCache();
			this.InitializeClientToken();
			this.configSession = DirectorySessionFactory.Default.GetTenantOrTopologyConfigurationSession(false, ConsistencyMode.IgnoreInvalid, ADSessionSettings.FromRootOrgScopeSet(), 313, ".ctor", "f:\\15.00.1497\\sources\\dev\\EdgeSync\\src\\Mserve\\MserveTargetConnection.cs");
			if (EdgeSyncSvc.EdgeSync != null && EdgeSyncSvc.EdgeSync.AppConfig != null)
			{
				this.duplicatedAddEntriesCacheSize = EdgeSyncSvc.EdgeSync.AppConfig.DuplicatedAddEntriesCacheSize;
				this.podSiteStartRange = EdgeSyncSvc.EdgeSync.AppConfig.PodSiteStartRange;
				this.podSiteEndRange = EdgeSyncSvc.EdgeSync.AppConfig.PodSiteEndRange;
				this.trackDuplicatedAddEntries = EdgeSyncSvc.EdgeSync.AppConfig.TrackDuplicatedAddEntries;
				if (!this.trackDuplicatedAddEntries)
				{
					this.duplicatedAddEntriesCacheSize = 0;
				}
			}
		}

		// Token: 0x06000256 RID: 598 RVA: 0x0000FECC File Offset: 0x0000E0CC
		private void InitializeTenantMEUSyncControlCache()
		{
			if (MserveTargetConnection.tenantSyncControlCache == null)
			{
				if (EdgeSyncSvc.EdgeSync != null && EdgeSyncSvc.EdgeSync.AppConfig != null)
				{
					MserveTargetConnection.tenantSyncControlCache = new Cache<string, MserveTargetConnection.TenantSyncControl>(EdgeSyncSvc.EdgeSync.AppConfig.TenantSyncControlCacheSize, EdgeSyncSvc.EdgeSync.AppConfig.TenantSyncControlCacheExpiryInterval, EdgeSyncSvc.EdgeSync.AppConfig.TenantSyncControlCacheCleanupInterval, EdgeSyncSvc.EdgeSync.AppConfig.TenantSyncControlCachePurgeInterval, new MserveTargetConnection.TenantSyncControlCacheLogger<string>(EdgeSyncSvc.EdgeSync.EdgeSyncLogSession), null);
					return;
				}
				MserveTargetConnection.tenantSyncControlCache = new Cache<string, MserveTargetConnection.TenantSyncControl>(1048576L, TimeSpan.FromHours(1.0), TimeSpan.FromMinutes(15.0), TimeSpan.FromMinutes(5.0), null, null);
			}
		}

		// Token: 0x06000257 RID: 599 RVA: 0x0000FFB0 File Offset: 0x0000E1B0
		private void InitializeClientToken()
		{
			if (string.IsNullOrEmpty(MserveTargetConnection.clientToken))
			{
				ADOperationResult adoperationResult = ADNotificationAdapter.TryRunADOperation(delegate()
				{
					MserveTargetConnection.clientToken = EdgeSyncMservConnector.GetMserveWebServiceClientTokenFromEndpointConfig(null);
					if (string.IsNullOrEmpty(MserveTargetConnection.clientToken))
					{
						throw new ExDirectoryException("Client token from Endpoint configuration is null or empty", null);
					}
				}, 3);
				if (!adoperationResult.Succeeded)
				{
					throw new ExDirectoryException("Unable to read client token from Endpoint configuration", adoperationResult.Exception);
				}
			}
		}

		// Token: 0x17000083 RID: 131
		// (get) Token: 0x06000258 RID: 600 RVA: 0x00010006 File Offset: 0x0000E206
		protected override string LeaseFileName
		{
			get
			{
				return "mserv.lease";
			}
		}

		// Token: 0x17000084 RID: 132
		// (get) Token: 0x06000259 RID: 601 RVA: 0x0001000D File Offset: 0x0000E20D
		protected override IConfigurationSession ConfigSession
		{
			get
			{
				return this.configSession;
			}
		}

		// Token: 0x17000085 RID: 133
		// (get) Token: 0x0600025A RID: 602 RVA: 0x00010015 File Offset: 0x0000E215
		// (set) Token: 0x0600025B RID: 603 RVA: 0x0001001D File Offset: 0x0000E21D
		protected IMserveService MserveService
		{
			get
			{
				return this.mserveService;
			}
			set
			{
				this.mserveService = value;
			}
		}

		// Token: 0x0600025C RID: 604 RVA: 0x00010026 File Offset: 0x0000E226
		public static ADObjectId GetCookieContainerId(IConfigurationSession notUsed)
		{
			return ADSession.GetConfigurationUnitsRootForLocalForest();
		}

		// Token: 0x0600025D RID: 605 RVA: 0x00010030 File Offset: 0x0000E230
		public override bool OnSynchronizing()
		{
			MserveEdgeSyncService mserveEdgeSyncService = null;
			try
			{
				mserveEdgeSyncService = new MserveEdgeSyncService(this.config.ProvisioningUrl, this.config.SettingsUrl, this.config.RemoteCertSubject, MserveTargetConnection.clientToken, base.LogSession, true, this.trackDuplicatedAddEntries);
				mserveEdgeSyncService.Initialize();
			}
			catch (MserveException ex)
			{
				ExTraceGlobals.TargetConnectionTracer.TraceError<string>((long)this.GetHashCode(), "Failed to initialize MserveWebService because of {0}", ex.Message);
				base.LogSession.LogException(EdgeSyncLoggingLevel.Low, EdgeSyncEvent.TargetConnection, ex, "Failed to initialize MserveWebService");
				return false;
			}
			this.mserveService = mserveEdgeSyncService;
			return true;
		}

		// Token: 0x0600025E RID: 606 RVA: 0x000100D0 File Offset: 0x0000E2D0
		public override void OnConnectedToSource(Connection sourceConnection)
		{
			ExTraceGlobals.TargetConnectionTracer.TraceDebug<string>((long)this.GetHashCode(), "Connected to the source Domain Controller {0}", sourceConnection.Fqdn);
			this.sourceConnection = sourceConnection;
		}

		// Token: 0x0600025F RID: 607 RVA: 0x000100F8 File Offset: 0x0000E2F8
		public override bool OnSynchronized()
		{
			List<RecipientSyncOperation> results = null;
			try
			{
				results = this.mserveService.Synchronize();
			}
			catch (MserveException ex)
			{
				ExTraceGlobals.TargetConnectionTracer.TraceError<string>((long)this.GetHashCode(), "MserveWebService failed to flush operations because of {0}", ex.Message);
				base.LogSession.LogException(EdgeSyncLoggingLevel.Low, EdgeSyncEvent.TargetConnection, ex, "MserveWebService failed to flush");
				return false;
			}
			MserveTargetConnection.ProcessSyncResult processSyncResult = this.ProcessSyncResults(results);
			if (processSyncResult != MserveTargetConnection.ProcessSyncResult.Success)
			{
				base.LogSession.LogException(EdgeSyncLoggingLevel.Low, EdgeSyncEvent.TargetConnection, new ExDirectoryException("ProcessSyncResults " + processSyncResult, null), "Failed OnSynchronized");
			}
			return processSyncResult == MserveTargetConnection.ProcessSyncResult.Success && this.ResolveDuplicateAddedEntries();
		}

		// Token: 0x06000260 RID: 608 RVA: 0x0001019C File Offset: 0x0000E39C
		public override SyncResult OnAddEntry(ExSearchResultEntry entry, SortedList<string, DirectoryAttribute> sourceAttributes)
		{
			this.OnEntryChanged(entry);
			return SyncResult.Added;
		}

		// Token: 0x06000261 RID: 609 RVA: 0x000101A6 File Offset: 0x0000E3A6
		public override SyncResult OnModifyEntry(ExSearchResultEntry entry, SortedList<string, DirectoryAttribute> sourceAttributes)
		{
			this.OnEntryChanged(entry);
			return SyncResult.Modified;
		}

		// Token: 0x06000262 RID: 610 RVA: 0x000101B0 File Offset: 0x0000E3B0
		public override SyncResult OnDeleteEntry(ExSearchResultEntry entry)
		{
			this.OnEntryChanged(entry);
			return SyncResult.Deleted;
		}

		// Token: 0x06000263 RID: 611 RVA: 0x000101BA File Offset: 0x0000E3BA
		public override SyncResult OnRenameEntry(ExSearchResultEntry entry)
		{
			return SyncResult.Renamed;
		}

		// Token: 0x06000264 RID: 612 RVA: 0x000101BD File Offset: 0x0000E3BD
		public override void Dispose()
		{
			if (this.updateConnection != null)
			{
				this.updateConnection.Dispose();
				this.updateConnection = null;
			}
		}

		// Token: 0x06000265 RID: 613 RVA: 0x000101DC File Offset: 0x0000E3DC
		public void FilterSmtpProxyAddressesBasedOnTenantSetting(ExSearchResultEntry entry, RecipientTypeDetails recipientTypeDetail)
		{
			DirectoryAttribute directoryAttribute = null;
			if (!entry.Attributes.TryGetValue("msExchCU", out directoryAttribute))
			{
				throw new ExDirectoryException("TenantCU is missing for the user", null);
			}
			if (directoryAttribute == null || directoryAttribute.Count <= 0)
			{
				throw new ExDirectoryException("TenantCU has empty value for the user", null);
			}
			string text = directoryAttribute[0] as string;
			if (string.IsNullOrEmpty(text))
			{
				throw new ExDirectoryException("TenantCU attribute is not string value", null);
			}
			ADObjectId tenantCUId = null;
			try
			{
				tenantCUId = new ADObjectId(text);
			}
			catch (FormatException e)
			{
				throw new ExDirectoryException("TenantCU DN is of invalid format as " + text, e);
			}
			bool flag = MserveTargetConnection.IsEntryMailEnabledUser(entry, recipientTypeDetail);
			MserveTargetConnection.TenantSyncControl tenantSyncControlAndUpdateCache = this.GetTenantSyncControlAndUpdateCache(tenantCUId);
			if ((flag && !tenantSyncControlAndUpdateCache.SyncMEUSMTPToMServ) || (!flag && !tenantSyncControlAndUpdateCache.SyncMailboxSMTPToMserv))
			{
				entry.Attributes["proxyAddresses"] = new DirectoryAttribute("proxyAddresses", MserveSynchronizationProvider.EmptyList);
			}
		}

		// Token: 0x06000266 RID: 614 RVA: 0x000102C0 File Offset: 0x0000E4C0
		protected override ADObjectId GetCookieContainerId()
		{
			if (MserveTargetConnection.configUnitsId == null)
			{
				MserveTargetConnection.configUnitsId = MserveTargetConnection.GetCookieContainerId(this.ConfigSession);
			}
			return MserveTargetConnection.configUnitsId;
		}

		// Token: 0x06000267 RID: 615 RVA: 0x000102E0 File Offset: 0x0000E4E0
		protected List<RecipientSyncOperation> GetRecipientSyncOperation(ExSearchResultEntry entry)
		{
			List<RecipientSyncOperation> list = new List<RecipientSyncOperation>();
			ExTraceGlobals.TargetConnectionTracer.TraceDebug<string>(0L, "Try to GetRecipientSyncOperation for {0}", entry.DistinguishedName);
			RecipientSyncState recipientSyncState = null;
			if (entry.Attributes.ContainsKey("msExchExternalSyncState"))
			{
				byte[] bytes = Encoding.ASCII.GetBytes((string)entry.Attributes["msExchExternalSyncState"][0]);
				recipientSyncState = RecipientSyncState.DeserializeRecipientSyncState(bytes);
				ExTraceGlobals.TargetConnectionTracer.TraceDebug<string>(0L, "{0} has existing syncState", entry.DistinguishedName);
			}
			if (recipientSyncState == null)
			{
				if (entry.IsDeleted)
				{
					ExTraceGlobals.TargetConnectionTracer.TraceDebug<string>(0L, "{0} is deleted entry without syncState. Ignore the entry", entry.DistinguishedName);
					return list;
				}
				ExTraceGlobals.TargetConnectionTracer.TraceDebug<string>(0L, "{0} is a normal entry without syncState. Creating one", entry.DistinguishedName);
				recipientSyncState = new RecipientSyncState();
			}
			if (!entry.IsDeleted)
			{
				int partnerId = MserveSynchronizationProvider.PartnerId;
				if (partnerId == -1)
				{
					ExTraceGlobals.TargetConnectionTracer.TraceError<string>(0L, "Failed the sync because we could not get the partner Id for {0}", entry.DistinguishedName);
					throw new ExDirectoryException(new ArgumentException("Failed the sync because we could not get the partner Id for " + entry.DistinguishedName));
				}
				int num = (recipientSyncState.PartnerId != 0) ? recipientSyncState.PartnerId : partnerId;
				recipientSyncState.PartnerId = partnerId;
				if (num != partnerId)
				{
					ExTraceGlobals.TargetConnectionTracer.TraceDebug<string, int, int>(0L, "{0}'s partnerId changed from {1} to {2}", entry.DistinguishedName, num, partnerId);
					base.LogSession.LogEvent(EdgeSyncLoggingLevel.Low, EdgeSyncEvent.TargetConnection, entry.DistinguishedName, "Warning: Changing partner ID from " + num.ToString() + " to " + partnerId.ToString());
				}
				else
				{
					RecipientSyncOperation recipientSyncOperation = new RecipientSyncOperation(entry.DistinguishedName, partnerId, recipientSyncState, false);
					ExTraceGlobals.TargetConnectionTracer.TraceDebug<int>(0L, "Create new operation with partner Id {0}", partnerId);
					foreach (string key in MserveTargetConnection.ReplicationAddressAttributes)
					{
						if (entry.Attributes.ContainsKey(key))
						{
							MserveTargetConnection.OnAddressChange(entry.Attributes[key], recipientSyncOperation);
						}
					}
					if (recipientSyncOperation.RemovedEntries.Count != 0 || recipientSyncOperation.AddedEntries.Count != 0)
					{
						list.Add(recipientSyncOperation);
					}
				}
			}
			else
			{
				int partnerId2 = recipientSyncState.PartnerId;
				if (partnerId2 != 0)
				{
					RecipientSyncOperation recipientSyncOperation2 = new RecipientSyncOperation(entry.DistinguishedName, partnerId2, null, true);
					ExTraceGlobals.TargetConnectionTracer.TraceDebug<int>(0L, "Create remove operation with partner Id {0}", partnerId2);
					foreach (string text in MserveTargetConnection.ReplicationAddressAttributes)
					{
						string recipientSyncStateAttribute = MserveTargetConnection.GetRecipientSyncStateAttribute(recipientSyncState, text);
						if (recipientSyncStateAttribute != null)
						{
							List<string> list2 = RecipientSyncState.AddressToList(recipientSyncStateAttribute);
							if (this.CanRemove(entry.DistinguishedName, text, list2))
							{
								foreach (string text2 in list2)
								{
									recipientSyncOperation2.RemovedEntries.Add(text2);
									ExTraceGlobals.TargetConnectionTracer.TraceDebug<string>(0L, "Add {0} to RemovedEntries", text2);
								}
							}
						}
					}
					if (recipientSyncOperation2.RemovedEntries.Count != 0)
					{
						list.Add(recipientSyncOperation2);
					}
				}
				else
				{
					ExTraceGlobals.TargetConnectionTracer.TraceDebug(0L, "No partner Id present on syncState. Skip the recipient");
				}
			}
			return list;
		}

		// Token: 0x06000268 RID: 616 RVA: 0x000105F8 File Offset: 0x0000E7F8
		public void UpdateRecipientSyncStateValue(RecipientSyncOperation operation)
		{
			Dictionary<string, HashSet<string>> dictionary = new Dictionary<string, HashSet<string>>(MserveTargetConnection.ReplicationAddressAttributes.Length, StringComparer.OrdinalIgnoreCase);
			foreach (string text in MserveTargetConnection.ReplicationAddressAttributes)
			{
				string recipientSyncStateAttribute = MserveTargetConnection.GetRecipientSyncStateAttribute(operation.RecipientSyncState, text);
				dictionary[text] = RecipientSyncState.AddressHashSetFromConcatStringValue(recipientSyncStateAttribute);
			}
			foreach (OperationType key in operation.PendingSyncStateCommitEntries.Keys)
			{
				foreach (string text2 in operation.PendingSyncStateCommitEntries[key])
				{
					string key2 = null;
					if (!operation.AddressTypeTable.TryGetValue(text2, out key2))
					{
						throw new InvalidOperationException(text2 + " is not in AddressTypeTable");
					}
					switch (key)
					{
					case OperationType.Add:
						if (!dictionary[key2].Contains(text2))
						{
							dictionary[key2].Add(text2);
						}
						break;
					case OperationType.Delete:
						if (dictionary[key2].Contains(text2))
						{
							dictionary[key2].Remove(text2);
						}
						break;
					}
				}
			}
			foreach (string text3 in MserveTargetConnection.ReplicationAddressAttributes)
			{
				MserveTargetConnection.SetRecipientSyncStateAttribute(operation.RecipientSyncState, text3, RecipientSyncState.AddressHashSetToConcatStringValue(dictionary[text3]));
			}
		}

		// Token: 0x06000269 RID: 617 RVA: 0x000107F8 File Offset: 0x0000E9F8
		protected virtual void UpdateRecipientSyncStateValueInAD(RecipientSyncOperation operation)
		{
			if (this.updateConnection == null)
			{
				ADObjectId rootId = null;
				ADOperationResult adoperationResult = ADNotificationAdapter.TryRunADOperation(delegate()
				{
					PooledLdapConnection readConnection = this.ConfigSession.GetReadConnection(this.sourceConnection.Fqdn, ref rootId);
					this.updateConnection = new Connection(readConnection, EdgeSyncSvc.EdgeSync.AppConfig);
				}, 3);
				if (!adoperationResult.Succeeded)
				{
					ExTraceGlobals.TargetConnectionTracer.TraceError<string>((long)this.GetHashCode(), "Failed to get AD connection to update SyncState because of {0}", adoperationResult.Exception.Message);
					throw new ExDirectoryException("Failed to get AD connection to update SyncState", adoperationResult.Exception);
				}
			}
			byte[] array = RecipientSyncState.SerializeRecipientSyncState(operation.RecipientSyncState);
			ModifyRequest request = new ModifyRequest(operation.DistinguishedName, DirectoryAttributeOperation.Replace, "msExchExternalSyncState", new object[]
			{
				array
			});
			this.updateConnection.SendRequest(request);
			ExTraceGlobals.TargetConnectionTracer.TraceDebug<string>((long)this.GetHashCode(), "Successfully updated SyncState in AD for {0}", operation.DistinguishedName);
			base.LogSession.LogEvent(EdgeSyncLoggingLevel.Low, EdgeSyncEvent.TargetConnection, operation.DistinguishedName, "Successfully synced to MSERV and updated SyncState");
		}

		// Token: 0x0600026A RID: 618 RVA: 0x000108DC File Offset: 0x0000EADC
		private static void SetRecipientSyncStateAttribute(RecipientSyncState syncState, string name, string value)
		{
			if (name.Equals("proxyAddresses", StringComparison.OrdinalIgnoreCase))
			{
				syncState.ProxyAddresses = value;
				return;
			}
			if (name.Equals("msExchSignupAddresses", StringComparison.OrdinalIgnoreCase))
			{
				syncState.SignupAddresses = value;
				return;
			}
			if (name.Equals("msExchUMAddresses", StringComparison.OrdinalIgnoreCase))
			{
				syncState.UMProxyAddresses = value;
				return;
			}
			if (name.Equals("ArchiveAddress", StringComparison.OrdinalIgnoreCase))
			{
				syncState.ArchiveAddress = value;
				return;
			}
			throw new ArgumentOutOfRangeException(name);
		}

		// Token: 0x0600026B RID: 619 RVA: 0x00010948 File Offset: 0x0000EB48
		private static string GetRecipientSyncStateAttribute(RecipientSyncState syncState, string name)
		{
			if (name.Equals("proxyAddresses", StringComparison.OrdinalIgnoreCase))
			{
				return syncState.ProxyAddresses;
			}
			if (name.Equals("msExchSignupAddresses", StringComparison.OrdinalIgnoreCase))
			{
				return syncState.SignupAddresses;
			}
			if (name.Equals("msExchUMAddresses", StringComparison.OrdinalIgnoreCase))
			{
				return syncState.UMProxyAddresses;
			}
			if (name.Equals("ArchiveAddress", StringComparison.OrdinalIgnoreCase))
			{
				return syncState.ArchiveAddress;
			}
			throw new ArgumentOutOfRangeException(name);
		}

		// Token: 0x0600026C RID: 620 RVA: 0x000109B0 File Offset: 0x0000EBB0
		private static void OnAddressChange(DirectoryAttribute attribute, RecipientSyncOperation operation)
		{
			HashSet<string> hashSet = RecipientSyncState.AddressHashSetFromConcatStringValue(MserveTargetConnection.GetRecipientSyncStateAttribute(operation.RecipientSyncState, attribute.Name));
			foreach (object obj in attribute)
			{
				string text = (string)obj;
				string text2;
				if (text.StartsWith("smtp:", StringComparison.OrdinalIgnoreCase))
				{
					text2 = text.Substring(5);
				}
				else if (text.StartsWith("meum:", StringComparison.OrdinalIgnoreCase))
				{
					text2 = text.Substring(5);
				}
				else
				{
					text2 = text;
				}
				if (!hashSet.Contains(text2))
				{
					operation.AddedEntries.Add(text2);
					operation.AddressTypeTable[text2] = attribute.Name;
					ExTraceGlobals.TargetConnectionTracer.TraceDebug<string>(0L, "Add {0} to AddedEntries", text2);
				}
				else
				{
					hashSet.Remove(text2);
				}
			}
			foreach (string text3 in hashSet)
			{
				operation.RemovedEntries.Add(text3);
				operation.AddressTypeTable[text3] = attribute.Name;
				ExTraceGlobals.TargetConnectionTracer.TraceDebug<string>(0L, "Add {0} to RemovedEntries", text3);
			}
		}

		// Token: 0x0600026D RID: 621 RVA: 0x00010B00 File Offset: 0x0000ED00
		private static bool IsEntryMailEnabledUser(ExSearchResultEntry entry, RecipientTypeDetails recipientTypeDetail)
		{
			if (recipientTypeDetail != RecipientTypeDetails.None)
			{
				return recipientTypeDetail == RecipientTypeDetails.MailUser;
			}
			return entry.Attributes.ContainsKey("mailNickname") && !entry.Attributes.ContainsKey("msExchHomeServerName");
		}

		// Token: 0x0600026E RID: 622 RVA: 0x00010BB4 File Offset: 0x0000EDB4
		private MserveTargetConnection.TenantSyncControl GetTenantSyncControlSettingFromAD(ADObjectId tenantCUId, string key)
		{
			ExchangeConfigurationUnit tenantCU = null;
			int tryCount = 0;
			string savedDomainController = this.ConfigSession.DomainController;
			ADOperationResult adoperationResult = ADNotificationAdapter.TryRunADOperation(delegate()
			{
				if (tryCount++ == 0)
				{
					this.ConfigSession.DomainController = this.sourceConnection.Fqdn;
				}
				else
				{
					this.ConfigSession.DomainController = savedDomainController;
				}
				tenantCU = this.ConfigSession.Read<ExchangeConfigurationUnit>(tenantCUId);
			}, 2);
			this.ConfigSession.DomainController = savedDomainController;
			if (!adoperationResult.Succeeded)
			{
				throw new ExDirectoryException(string.Format("Failed to read user's ExchangeConfigurationUnit {0}", tenantCUId.DistinguishedName), adoperationResult.Exception);
			}
			if (tenantCU == null)
			{
				throw new ExDirectoryException(string.Format("Failed to read user's ExchangeConfigurationUnit {0} because AD returns null", tenantCUId.DistinguishedName), null);
			}
			base.LogSession.LogEvent(EdgeSyncLoggingLevel.High, EdgeSyncEvent.TargetConnection, tenantCUId.DistinguishedName, "OrgStatus:" + tenantCU.OrganizationStatus);
			if (!tenantCU.IsOrganizationReadyForMservSync)
			{
				throw new ExDirectoryException(string.Format("Warning: ExchangeConfigurationUnit {0} with OrgStatus {1} is not ready for Mserv Sync yet.", tenantCUId.DistinguishedName, tenantCU.OrganizationStatus), null);
			}
			return new MserveTargetConnection.TenantSyncControl(tenantCU.SyncMEUSMTPToMServ, tenantCU.SyncMBXAndDLToMServ);
		}

		// Token: 0x0600026F RID: 623 RVA: 0x00010CEC File Offset: 0x0000EEEC
		private MserveTargetConnection.TenantSyncControl GetTenantSyncControlAndUpdateCache(ADObjectId tenantCUId)
		{
			string unescapedName = tenantCUId.AncestorDN(1).Rdn.UnescapedName;
			MserveTargetConnection.TenantSyncControl tenantSyncControl = null;
			bool flag = false;
			if (!MserveTargetConnection.tenantSyncControlCache.TryGetValue(unescapedName, out tenantSyncControl, out flag) || flag)
			{
				tenantSyncControl = this.GetTenantSyncControlSettingFromAD(tenantCUId, unescapedName);
				MserveTargetConnection.tenantSyncControlCache.TryAdd(unescapedName, tenantSyncControl);
			}
			return tenantSyncControl;
		}

		// Token: 0x06000270 RID: 624 RVA: 0x00010D3C File Offset: 0x0000EF3C
		private void OnEntryChanged(ExSearchResultEntry entry)
		{
			List<RecipientSyncOperation> recipientSyncOperation = this.GetRecipientSyncOperation(entry);
			List<RecipientSyncOperation> results = null;
			try
			{
				results = this.mserveService.Synchronize(recipientSyncOperation);
			}
			catch (MserveException e)
			{
				throw new ExDirectoryException("Mserve synchronization failed", e);
			}
			MserveTargetConnection.ProcessSyncResult processSyncResult = this.ProcessSyncResults(results);
			if (processSyncResult != MserveTargetConnection.ProcessSyncResult.Success)
			{
				throw new ExDirectoryException("OnEntryChanged failed with ProcessSyncResults " + processSyncResult, null);
			}
		}

		// Token: 0x06000271 RID: 625 RVA: 0x00010DA4 File Offset: 0x0000EFA4
		private MserveTargetConnection.ProcessSyncResult ProcessSyncResults(List<RecipientSyncOperation> results)
		{
			MserveTargetConnection.ProcessSyncResult processSyncResult = MserveTargetConnection.ProcessSyncResult.Success;
			foreach (RecipientSyncOperation recipientSyncOperation in results)
			{
				if (recipientSyncOperation.HasNonRetryableErrors)
				{
					this.LogFailedAddresses(recipientSyncOperation.NonRetryableEntries);
				}
				if (recipientSyncOperation.HasRetryableErrors)
				{
					this.LogFailedAddresses(recipientSyncOperation.RetryableEntries);
					processSyncResult |= MserveTargetConnection.ProcessSyncResult.FailedMServ;
				}
				if (recipientSyncOperation.DuplicatedAddEntries.Count > 0 && this.operationsWithDuplicatedAddEntries.Count <= this.duplicatedAddEntriesCacheSize)
				{
					this.operationsWithDuplicatedAddEntries.Add(recipientSyncOperation);
				}
				if (!recipientSyncOperation.SuppressSyncStateUpdate && recipientSyncOperation.TotalPendingSyncStateCommitEntries != 0)
				{
					this.UpdateRecipientSyncStateValue(recipientSyncOperation);
					try
					{
						this.UpdateRecipientSyncStateValueInAD(recipientSyncOperation);
						foreach (OperationType key in recipientSyncOperation.PendingSyncStateCommitEntries.Keys)
						{
							recipientSyncOperation.PendingSyncStateCommitEntries[key].Clear();
						}
					}
					catch (ExDirectoryException exception)
					{
						base.LogSession.LogException(EdgeSyncLoggingLevel.Low, EdgeSyncEvent.TargetConnection, exception, "Failed to update SyncState for " + recipientSyncOperation.DistinguishedName);
						processSyncResult |= MserveTargetConnection.ProcessSyncResult.FailedUpdateSyncState;
					}
					if (this.testShutdownAndLease())
					{
						processSyncResult |= MserveTargetConnection.ProcessSyncResult.ShutdownOrLostLease;
						return processSyncResult;
					}
				}
			}
			return processSyncResult;
		}

		// Token: 0x06000272 RID: 626 RVA: 0x00010F34 File Offset: 0x0000F134
		private bool ResolveDuplicateAddedEntries()
		{
			if (this.operationsWithDuplicatedAddEntries.Count == 0)
			{
				return true;
			}
			int num = 0;
			foreach (RecipientSyncOperation recipientSyncOperation in this.operationsWithDuplicatedAddEntries)
			{
				num += recipientSyncOperation.DuplicatedAddEntries.Count;
			}
			base.LogSession.LogEvent(EdgeSyncLoggingLevel.Medium, EdgeSyncEvent.TargetConnection, null, "ResolveDuplicateAddedEntries: Count=" + this.operationsWithDuplicatedAddEntries.Count);
			bool result;
			try
			{
				this.mserveService.Reset();
				this.mserveService.TrackDuplicatedAddEntries = false;
				List<RecipientSyncOperation> list = new List<RecipientSyncOperation>();
				List<RecipientSyncOperation> list2 = null;
				foreach (RecipientSyncOperation recipientSyncOperation2 in this.operationsWithDuplicatedAddEntries)
				{
					foreach (string item in recipientSyncOperation2.DuplicatedAddEntries)
					{
						list.Add(new RecipientSyncOperation
						{
							ReadEntries = 
							{
								item
							}
						});
					}
				}
				base.LogSession.LogEvent(EdgeSyncLoggingLevel.Medium, EdgeSyncEvent.TargetConnection, null, "ResolveDuplicateAddedEntries: Read duplicated entry's partnerId from MSERV");
				if (!this.SyncBatchOperations(list, out list2))
				{
					result = false;
				}
				else
				{
					List<RecipientSyncOperation> list3 = new List<RecipientSyncOperation>();
					List<RecipientSyncOperation> list4 = null;
					foreach (RecipientSyncOperation recipientSyncOperation3 in list2)
					{
						if (recipientSyncOperation3.PartnerId < this.podSiteStartRange || recipientSyncOperation3.PartnerId > this.podSiteEndRange)
						{
							RecipientSyncOperation recipientSyncOperation4 = new RecipientSyncOperation();
							recipientSyncOperation4.PartnerId = recipientSyncOperation3.PartnerId;
							recipientSyncOperation4.RemovedEntries.AddRange(recipientSyncOperation3.ReadEntries);
							list3.Add(recipientSyncOperation4);
						}
						else
						{
							base.LogSession.LogEvent(EdgeSyncLoggingLevel.Medium, EdgeSyncEvent.TargetConnection, null, string.Concat(new object[]
							{
								"Warning: ",
								recipientSyncOperation3.ReadEntries[0],
								" has existing Exchange partnerID ",
								recipientSyncOperation3.PartnerId,
								". Skip fixing its partnerId as changing partnerID of Exchange forest is not supported."
							}));
						}
					}
					base.LogSession.LogEvent(EdgeSyncLoggingLevel.Medium, EdgeSyncEvent.TargetConnection, null, "ResolveDuplicateAddedEntries: Delete duplicated entry's partnerId from MSERV");
					if (!this.SyncBatchOperations(list3, out list4))
					{
						result = false;
					}
					else
					{
						List<RecipientSyncOperation> list5 = null;
						foreach (RecipientSyncOperation recipientSyncOperation5 in this.operationsWithDuplicatedAddEntries)
						{
							recipientSyncOperation5.AddedEntries.Clear();
							recipientSyncOperation5.RemovedEntries.Clear();
							recipientSyncOperation5.ReadEntries.Clear();
							recipientSyncOperation5.AddedEntries.AddRange(recipientSyncOperation5.DuplicatedAddEntries);
							recipientSyncOperation5.DuplicatedAddEntries.Clear();
						}
						base.LogSession.LogEvent(EdgeSyncLoggingLevel.Medium, EdgeSyncEvent.TargetConnection, null, "ResolveDuplicateAddedEntries: Add duplicated entry back to MSERV with Exchange partnerId");
						if (this.operationsWithDuplicatedAddEntries.Count > this.duplicatedAddEntriesCacheSize)
						{
							base.LogSession.LogEvent(EdgeSyncLoggingLevel.Medium, EdgeSyncEvent.TargetConnection, null, "Failed: DuplicatedAddEntriesCacheSize limit reached");
							this.SyncBatchOperations(this.operationsWithDuplicatedAddEntries, out list5);
							result = false;
						}
						else
						{
							result = this.SyncBatchOperations(this.operationsWithDuplicatedAddEntries, out list5);
						}
					}
				}
			}
			finally
			{
				this.mserveService.TrackDuplicatedAddEntries = true;
			}
			return result;
		}

		// Token: 0x06000273 RID: 627 RVA: 0x00011304 File Offset: 0x0000F504
		private bool SyncBatchOperations(List<RecipientSyncOperation> operations, out List<RecipientSyncOperation> operationResults)
		{
			operationResults = null;
			bool result;
			try
			{
				operationResults = this.mserveService.Synchronize(operations);
				operationResults.AddRange(this.mserveService.Synchronize());
				result = (this.ProcessSyncResults(operationResults) == MserveTargetConnection.ProcessSyncResult.Success);
			}
			catch (MserveException exception)
			{
				base.LogSession.LogException(EdgeSyncLoggingLevel.Low, EdgeSyncEvent.TargetConnection, exception, "SyncBatchOperations failed with MserveException");
				result = false;
			}
			return result;
		}

		// Token: 0x06000274 RID: 628 RVA: 0x0001136C File Offset: 0x0000F56C
		private void LogFailedAddresses(Dictionary<OperationType, List<FailedAddress>> failedEntries)
		{
			foreach (KeyValuePair<OperationType, List<FailedAddress>> keyValuePair in failedEntries)
			{
				foreach (FailedAddress failedAddress in keyValuePair.Value)
				{
					if (!failedAddress.IsTransientError)
					{
						EdgeSyncEvents.Log.LogEvent(EdgeSyncEventLogConstants.Tuple_MservEntrySyncFailure, null, new object[]
						{
							failedAddress.Name,
							failedAddress.ErrorCode
						});
					}
					base.LogSession.LogEvent(EdgeSyncLoggingLevel.Low, EdgeSyncEvent.TargetConnection, failedAddress.IsTransientError ? "Transient error" : "Non-transient error", string.Concat(new object[]
					{
						"Failed to synchronize the address ",
						failedAddress.Name,
						" to MSERV with errorCode ",
						failedAddress.ErrorCode
					}));
				}
			}
		}

		// Token: 0x06000275 RID: 629 RVA: 0x00011494 File Offset: 0x0000F694
		private bool CanRemove(string distinguishedName, string attributeName, List<string> addressList)
		{
			bool result = true;
			if (addressList.Count > 0)
			{
				string query = Schema.Query.BuildHostedRecipientAddressQuery(attributeName, addressList);
				foreach (ExSearchResultEntry exSearchResultEntry in this.sourceConnection.PagedScan(null, query, new string[]
				{
					attributeName
				}))
				{
					ExTraceGlobals.TargetConnectionTracer.TraceDebug<string, string, string>(0L, "Cannot remove {0} values from MSERVE for user {1} because it is still referenced by {2}", attributeName, distinguishedName, exSearchResultEntry.DistinguishedName);
					result = false;
				}
			}
			return result;
		}

		// Token: 0x040000FC RID: 252
		public const string MserveLeaseFileName = "mserv.lease";

		// Token: 0x040000FD RID: 253
		private static readonly string[] ReplicationAddressAttributes = new string[]
		{
			"proxyAddresses",
			"msExchSignupAddresses",
			"msExchUMAddresses",
			"ArchiveAddress"
		};

		// Token: 0x040000FE RID: 254
		private static Cache<string, MserveTargetConnection.TenantSyncControl> tenantSyncControlCache;

		// Token: 0x040000FF RID: 255
		private static string clientToken;

		// Token: 0x04000100 RID: 256
		private readonly MserveTargetServerConfig config;

		// Token: 0x04000101 RID: 257
		private readonly TestShutdownAndLeaseDelegate testShutdownAndLease;

		// Token: 0x04000102 RID: 258
		private readonly List<RecipientSyncOperation> operationsWithDuplicatedAddEntries = new List<RecipientSyncOperation>();

		// Token: 0x04000103 RID: 259
		private readonly bool trackDuplicatedAddEntries = true;

		// Token: 0x04000104 RID: 260
		private readonly int duplicatedAddEntriesCacheSize = 1500;

		// Token: 0x04000105 RID: 261
		private readonly int podSiteStartRange = 50000;

		// Token: 0x04000106 RID: 262
		private readonly int podSiteEndRange = 59999;

		// Token: 0x04000107 RID: 263
		private static ADObjectId configUnitsId;

		// Token: 0x04000108 RID: 264
		private IMserveService mserveService;

		// Token: 0x04000109 RID: 265
		private Connection updateConnection;

		// Token: 0x0400010A RID: 266
		private Connection sourceConnection;

		// Token: 0x0400010B RID: 267
		private IConfigurationSession configSession;

		// Token: 0x02000037 RID: 55
		private sealed class TenantSyncControl : CachableItem
		{
			// Token: 0x06000278 RID: 632 RVA: 0x0001155E File Offset: 0x0000F75E
			public TenantSyncControl(bool syncMEUSMTPToMServ, bool syncMailboxSMTPToMserv)
			{
				this.SyncMEUSMTPToMServ = syncMEUSMTPToMServ;
				this.SyncMailboxSMTPToMserv = syncMailboxSMTPToMserv;
			}

			// Token: 0x17000086 RID: 134
			// (get) Token: 0x06000279 RID: 633 RVA: 0x00011574 File Offset: 0x0000F774
			public override long ItemSize
			{
				get
				{
					return MserveTargetConnection.TenantSyncControl.Size;
				}
			}

			// Token: 0x0400010D RID: 269
			public static readonly long Size = 2L;

			// Token: 0x0400010E RID: 270
			public readonly bool SyncMEUSMTPToMServ;

			// Token: 0x0400010F RID: 271
			public readonly bool SyncMailboxSMTPToMserv;
		}

		// Token: 0x02000038 RID: 56
		private sealed class TenantSyncControlCacheLogger<K> : ICacheTracer<K>
		{
			// Token: 0x0600027B RID: 635 RVA: 0x00011584 File Offset: 0x0000F784
			public TenantSyncControlCacheLogger(EdgeSyncLogSession logSession)
			{
				this.logSession = logSession;
			}

			// Token: 0x0600027C RID: 636 RVA: 0x00011594 File Offset: 0x0000F794
			public void Accessed(K key, CachableItem value, AccessStatus accessStatus, DateTime timestamp)
			{
				MserveTargetConnection.TenantSyncControl tenantSyncControl = value as MserveTargetConnection.TenantSyncControl;
				this.logSession.LogEvent(EdgeSyncLoggingLevel.High, EdgeSyncEvent.TargetConnection, null, string.Format(CultureInfo.InvariantCulture, "TenantSyncControlCache-Accessed: AccessStatus {0}, key {1}, SyncMEUSMTPToMServ {2}, SyncMailboxSMTPToMServ {3}", new object[]
				{
					accessStatus,
					key,
					(tenantSyncControl != null) ? tenantSyncControl.SyncMEUSMTPToMServ.ToString() : "NULL",
					(tenantSyncControl != null) ? tenantSyncControl.SyncMailboxSMTPToMserv.ToString() : "NULL"
				}));
			}

			// Token: 0x0600027D RID: 637 RVA: 0x00011618 File Offset: 0x0000F818
			public void Flushed(long cacheSize, DateTime timestamp)
			{
				this.logSession.LogEvent(EdgeSyncLoggingLevel.Low, EdgeSyncEvent.TargetConnection, null, string.Format(CultureInfo.InvariantCulture, "TenantSyncControlCache-Flushed: Cached flushed. CacheSize was {0} bytes", new object[]
				{
					cacheSize
				}));
			}

			// Token: 0x0600027E RID: 638 RVA: 0x00011654 File Offset: 0x0000F854
			public void ItemAdded(K key, CachableItem value, DateTime timestamp)
			{
				MserveTargetConnection.TenantSyncControl tenantSyncControl = value as MserveTargetConnection.TenantSyncControl;
				this.logSession.LogEvent(EdgeSyncLoggingLevel.Low, EdgeSyncEvent.TargetConnection, null, string.Format(CultureInfo.InvariantCulture, "TenantSyncControlCache-ItemAdded: key {0}, SyncMEUSMTPToMServ {1}, SyncMailboxSMTPToMServ {2}", new object[]
				{
					key,
					(tenantSyncControl != null) ? tenantSyncControl.SyncMEUSMTPToMServ.ToString() : "NULL",
					(tenantSyncControl != null) ? tenantSyncControl.SyncMailboxSMTPToMserv.ToString() : "NULL"
				}));
			}

			// Token: 0x0600027F RID: 639 RVA: 0x000116CC File Offset: 0x0000F8CC
			public void ItemRemoved(K key, CachableItem value, CacheItemRemovedReason removeReason, DateTime timestamp)
			{
				MserveTargetConnection.TenantSyncControl tenantSyncControl = value as MserveTargetConnection.TenantSyncControl;
				this.logSession.LogEvent(EdgeSyncLoggingLevel.High, EdgeSyncEvent.TargetConnection, null, string.Format(CultureInfo.InvariantCulture, "TenantSyncControlCache-ItemRemoved: CacheItemRemovedReason {0}, key {1}, SyncMEUSMTPToMServ {2}, SyncMailboxSMTPToMserv {3}", new object[]
				{
					removeReason,
					key,
					(tenantSyncControl != null) ? tenantSyncControl.SyncMEUSMTPToMServ.ToString() : "NULL",
					(tenantSyncControl != null) ? tenantSyncControl.SyncMailboxSMTPToMserv.ToString() : "NULL"
				}));
			}

			// Token: 0x06000280 RID: 640 RVA: 0x0001174D File Offset: 0x0000F94D
			public void TraceException(string details, Exception exception)
			{
				this.logSession.LogException(EdgeSyncLoggingLevel.Low, EdgeSyncEvent.TargetConnection, exception, details);
			}

			// Token: 0x04000110 RID: 272
			private readonly EdgeSyncLogSession logSession;
		}

		// Token: 0x02000039 RID: 57
		[Flags]
		private enum ProcessSyncResult : uint
		{
			// Token: 0x04000112 RID: 274
			Success = 0U,
			// Token: 0x04000113 RID: 275
			FailedMServ = 1U,
			// Token: 0x04000114 RID: 276
			FailedUpdateSyncState = 2U,
			// Token: 0x04000115 RID: 277
			ShutdownOrLostLease = 4U
		}
	}
}
