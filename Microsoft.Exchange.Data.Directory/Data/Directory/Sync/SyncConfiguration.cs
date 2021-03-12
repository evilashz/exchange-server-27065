using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.BackSync;
using Microsoft.Win32;

namespace Microsoft.Exchange.Data.Directory.Sync
{
	// Token: 0x020007B6 RID: 1974
	internal abstract class SyncConfiguration
	{
		// Token: 0x06006202 RID: 25090 RVA: 0x0014FE4C File Offset: 0x0014E04C
		protected SyncConfiguration(Guid invocationId, OutputResultDelegate writeResult, ISyncEventLogger eventLogger, IExcludedObjectReporter excludedObjectReporter)
		{
			ExTraceGlobals.BackSyncTracer.TraceDebug((long)SyncConfiguration.TraceId, "New SyncConfiguration");
			ExTraceGlobals.BackSyncTracer.TraceDebug<Guid>((long)SyncConfiguration.TraceId, "invocationId {0}", invocationId);
			this.WriteResult = writeResult;
			this.EventLogger = eventLogger;
			this.ExcludedObjectReporter = excludedObjectReporter;
			this.errorSyncObjects = new Dictionary<SyncObject, Exception>();
			this.InvocationId = invocationId;
		}

		// Token: 0x170022F1 RID: 8945
		// (get) Token: 0x06006203 RID: 25091
		public abstract bool MoreData { get; }

		// Token: 0x170022F2 RID: 8946
		// (get) Token: 0x06006204 RID: 25092 RVA: 0x0014FEB2 File Offset: 0x0014E0B2
		// (set) Token: 0x06006205 RID: 25093 RVA: 0x0014FEB9 File Offset: 0x0014E0B9
		internal static TimeSpan FailoverTimeout { get; private set; } = TimeSpan.FromSeconds((double)SyncConfiguration.GetConfigurationValue<int>("FailoverTimeout", 60));

		// Token: 0x170022F3 RID: 8947
		// (get) Token: 0x06006206 RID: 25094 RVA: 0x0014FEC1 File Offset: 0x0014E0C1
		// (set) Token: 0x06006207 RID: 25095 RVA: 0x0014FEC9 File Offset: 0x0014E0C9
		internal Guid InvocationId { get; private set; }

		// Token: 0x170022F4 RID: 8948
		// (get) Token: 0x06006208 RID: 25096 RVA: 0x0014FED2 File Offset: 0x0014E0D2
		internal static int TraceId
		{
			get
			{
				return Environment.CurrentManagedThreadId;
			}
		}

		// Token: 0x170022F5 RID: 8949
		// (get) Token: 0x06006209 RID: 25097 RVA: 0x0014FED9 File Offset: 0x0014E0D9
		// (set) Token: 0x0600620A RID: 25098 RVA: 0x0014FEE1 File Offset: 0x0014E0E1
		private protected OutputResultDelegate WriteResult { protected get; private set; }

		// Token: 0x170022F6 RID: 8950
		// (get) Token: 0x0600620B RID: 25099 RVA: 0x0014FEEA File Offset: 0x0014E0EA
		// (set) Token: 0x0600620C RID: 25100 RVA: 0x0014FEF2 File Offset: 0x0014E0F2
		public IExcludedObjectReporter ExcludedObjectReporter { get; private set; }

		// Token: 0x170022F7 RID: 8951
		// (get) Token: 0x0600620D RID: 25101 RVA: 0x0014FEFB File Offset: 0x0014E0FB
		// (set) Token: 0x0600620E RID: 25102 RVA: 0x0014FF03 File Offset: 0x0014E103
		private protected ISyncEventLogger EventLogger { protected get; private set; }

		// Token: 0x170022F8 RID: 8952
		// (get) Token: 0x0600620F RID: 25103 RVA: 0x0014FF0C File Offset: 0x0014E10C
		// (set) Token: 0x06006210 RID: 25104 RVA: 0x0014FF14 File Offset: 0x0014E114
		internal ITenantRecipientSession RecipientSession
		{
			get
			{
				return this.recipientSession;
			}
			private set
			{
				this.recipientSession = value;
			}
		}

		// Token: 0x170022F9 RID: 8953
		// (get) Token: 0x06006211 RID: 25105 RVA: 0x0014FF1D File Offset: 0x0014E11D
		// (set) Token: 0x06006212 RID: 25106 RVA: 0x0014FF25 File Offset: 0x0014E125
		internal ITopologyConfigurationSession RootOrgConfigurationSession
		{
			get
			{
				return this.rootOrgConfigurationSession;
			}
			private set
			{
				this.rootOrgConfigurationSession = value;
			}
		}

		// Token: 0x170022FA RID: 8954
		// (get) Token: 0x06006213 RID: 25107 RVA: 0x0014FF2E File Offset: 0x0014E12E
		// (set) Token: 0x06006214 RID: 25108 RVA: 0x0014FF36 File Offset: 0x0014E136
		internal ITenantConfigurationSession TenantConfigurationSession
		{
			get
			{
				return this.tenantConfigurationSession;
			}
			private set
			{
				this.tenantConfigurationSession = value;
			}
		}

		// Token: 0x170022FB RID: 8955
		// (get) Token: 0x06006215 RID: 25109 RVA: 0x0014FF3F File Offset: 0x0014E13F
		internal Dictionary<SyncObject, Exception> ErrorSyncObjects
		{
			get
			{
				return this.errorSyncObjects;
			}
		}

		// Token: 0x170022FC RID: 8956
		// (get) Token: 0x06006216 RID: 25110 RVA: 0x0014FF47 File Offset: 0x0014E147
		internal static bool SkipSchemaValidation
		{
			get
			{
				return SyncConfiguration.GetConfigurationValue<int>("SkipOutputSchemaValidation", 0) == 1;
			}
		}

		// Token: 0x06006217 RID: 25111
		public abstract IEnumerable<ADRawEntry> GetDataPage();

		// Token: 0x06006218 RID: 25112
		public abstract byte[] GetResultCookie();

		// Token: 0x06006219 RID: 25113 RVA: 0x0014FF57 File Offset: 0x0014E157
		public bool IsKnownException(Exception exception)
		{
			return exception is InvalidCookieException;
		}

		// Token: 0x0600621A RID: 25114
		public abstract Exception HandleException(Exception e);

		// Token: 0x0600621B RID: 25115 RVA: 0x0014FF62 File Offset: 0x0014E162
		public virtual void CheckIfConnectionAllowed()
		{
		}

		// Token: 0x0600621C RID: 25116 RVA: 0x0014FF64 File Offset: 0x0014E164
		internal static bool InlcudeLinks(BackSyncOptions backSyncOptions)
		{
			return (backSyncOptions & BackSyncOptions.IncludeLinks) == BackSyncOptions.IncludeLinks;
		}

		// Token: 0x0600621D RID: 25117 RVA: 0x0014FF6C File Offset: 0x0014E16C
		internal static WatermarkMap GetReplicationCursors(ITopologyConfigurationSession configSession)
		{
			return SyncConfiguration.GetReplicationCursors(configSession, false, false);
		}

		// Token: 0x0600621E RID: 25118 RVA: 0x0014FF78 File Offset: 0x0014E178
		internal static WatermarkMap GetReplicationCursors(ITopologyConfigurationSession configSession, bool useConfigNC = false, bool includeRetiredDCs = false)
		{
			WatermarkMap empty = WatermarkMap.Empty;
			bool useConfigNC2 = configSession.UseConfigNC;
			ADObjectId id = useConfigNC ? configSession.GetConfigurationNamingContext() : configSession.GetDomainNamingContext();
			try
			{
				configSession.UseConfigNC = useConfigNC;
				MultiValuedProperty<ReplicationCursor> multiValuedProperty = configSession.ReadReplicationCursors(id);
				foreach (ReplicationCursor replicationCursor in multiValuedProperty)
				{
					if (includeRetiredDCs || replicationCursor.SourceDsa != null)
					{
						empty.Add(replicationCursor.SourceInvocationId, replicationCursor.UpToDatenessUsn);
					}
				}
			}
			finally
			{
				configSession.UseConfigNC = useConfigNC2;
			}
			return empty;
		}

		// Token: 0x0600621F RID: 25119 RVA: 0x00150028 File Offset: 0x0014E228
		internal static string FindDomainControllerByInvocationId(Guid dcInvocationId, PartitionId partitionId)
		{
			ExTraceGlobals.BackSyncTracer.TraceDebug<Guid>((long)SyncConfiguration.TraceId, "FindDomainControllerByInvocationIddcInvocationId {0}", dcInvocationId);
			ADTransientException exceptionOnDcNotFound = new ADTransientException(DirectoryStrings.ErrorDCNotFound(string.Format("InvocationId: {0}", dcInvocationId)));
			string text = SyncConfiguration.FindDomainControllerByInvocationId(dcInvocationId, exceptionOnDcNotFound, partitionId);
			ExTraceGlobals.BackSyncTracer.TraceDebug<string>((long)SyncConfiguration.TraceId, "FindDomainControllerByInvocationId dcFqdn {0}", text);
			return text;
		}

		// Token: 0x06006220 RID: 25120 RVA: 0x00150088 File Offset: 0x0014E288
		internal static string FindDomainControllerByInvocationId(Guid dcInvocationId, Exception exceptionOnDcNotFound, PartitionId partitionId)
		{
			string result = null;
			ITopologyConfigurationSession topologyConfigurationSession = DirectorySessionFactory.Default.CreateTopologyConfigurationSession(true, ConsistencyMode.IgnoreInvalid, ADSessionSettings.FromAccountPartitionRootOrgScopeSet(partitionId), 467, "FindDomainControllerByInvocationId", "f:\\15.00.1497\\sources\\dev\\data\\src\\directory\\Sync\\BackSync\\Configuration\\SyncConfiguration.cs");
			ADServer adserver = topologyConfigurationSession.FindDCByInvocationId(dcInvocationId);
			if (adserver != null)
			{
				result = adserver.DnsHostName;
				ExTraceGlobals.BackSyncTracer.TraceDebug<string>((long)SyncConfiguration.TraceId, "adServer.DnsHostName{0}", adserver.DnsHostName);
			}
			else
			{
				ExTraceGlobals.BackSyncTracer.TraceError<Guid>((long)SyncConfiguration.TraceId, "Unable to find a DC for the invocation id {0}", dcInvocationId);
				if (exceptionOnDcNotFound != null)
				{
					throw exceptionOnDcNotFound;
				}
			}
			return result;
		}

		// Token: 0x06006221 RID: 25121 RVA: 0x00150104 File Offset: 0x0014E304
		internal static Guid GetPreferredDCWithContainerizedUsnChanged(string serviceInstanceName)
		{
			if (!SyncConfiguration.EnableContainerizedUsnChangedOptimization())
			{
				throw new InvalidOperationException("SyncConfiguration.EnableContainerizedUsnChangedOptimization() == false");
			}
			if (serviceInstanceName == null)
			{
				throw new ArgumentNullException("serviceInstanceName");
			}
			string text = serviceInstanceName.ToLowerInvariant() + "_PreferredContainerizedUsnChangedDC";
			string configurationValue = SyncConfiguration.GetConfigurationValue<string>(text, null);
			if (configurationValue != null)
			{
				List<Guid> list = new List<Guid>();
				foreach (string text2 in configurationValue.Split(new char[]
				{
					','
				}))
				{
					Guid item;
					if (Guid.TryParse(text2, out item))
					{
						list.Add(item);
						ExTraceGlobals.BackSyncTracer.TraceDebug<string, string>((long)SyncConfiguration.TraceId, "GetPreferredDCWithContainerizedUsnChanged Adding a valid DC invocation id: {0} for service instance {1}.", text2, serviceInstanceName);
					}
					else
					{
						ExTraceGlobals.BackSyncTracer.TraceDebug<string, string>((long)SyncConfiguration.TraceId, "GetPreferredDCWithContainerizedUsnChanged Invalid DC invocation id: {0} defined for service instance {1}. Skipping it.", text2, serviceInstanceName);
					}
				}
				if (list.Count > 0)
				{
					Random random = new Random();
					return list.ElementAt(random.Next(list.Count));
				}
				ExTraceGlobals.BackSyncTracer.TraceDebug<string>((long)SyncConfiguration.TraceId, "GetPreferredDCWithContainerizedUsnChanged - Found no valid DC invocation ids for service instance: {0}", serviceInstanceName);
			}
			else
			{
				ExTraceGlobals.BackSyncTracer.TraceDebug<string>((long)SyncConfiguration.TraceId, "GetPreferredDCWithContainerizedUsnChanged - RegistryValue: {0} is null", text);
			}
			return Guid.Empty;
		}

		// Token: 0x06006222 RID: 25122 RVA: 0x00150228 File Offset: 0x0014E428
		internal static T GetConfigurationValue<T>(string registryValueName, T defaultValue)
		{
			T result;
			using (RegistryKey registryKey = Registry.LocalMachine.OpenSubKey("SOFTWARE\\Microsoft\\ExchangeServer\\v15\\BackSync"))
			{
				ExTraceGlobals.BackSyncTracer.TraceDebug<string>((long)SyncConfiguration.TraceId, "Open registry key {0}", "SOFTWARE\\Microsoft\\ExchangeServer\\v15\\BackSync");
				if (registryKey != null)
				{
					ExTraceGlobals.BackSyncTracer.TraceDebug<string>((long)SyncConfiguration.TraceId, "Read value from registry name {0}", registryValueName);
					result = (T)((object)registryKey.GetValue(registryValueName, defaultValue));
				}
				else
				{
					result = defaultValue;
				}
			}
			return result;
		}

		// Token: 0x06006223 RID: 25123 RVA: 0x001502AC File Offset: 0x0014E4AC
		internal void SetConfiguration(ITopologyConfigurationSession rootOrgConfigurationSession, ITenantConfigurationSession tenantSystemConfigurationSession, ITenantRecipientSession adRecipientSession)
		{
			ExTraceGlobals.BackSyncTracer.TraceDebug((long)SyncConfiguration.TraceId, "SyncConfiguration SetConfiguration ...");
			this.RootOrgConfigurationSession = rootOrgConfigurationSession;
			this.TenantConfigurationSession = tenantSystemConfigurationSession;
			this.RecipientSession = adRecipientSession;
			ExTraceGlobals.BackSyncTracer.TraceDebug<string>((long)SyncConfiguration.TraceId, "this.RootOrgConfigurationSession.DomainController {0}", this.RootOrgConfigurationSession.DomainController);
			ExTraceGlobals.BackSyncTracer.TraceDebug<string>((long)SyncConfiguration.TraceId, "this.TenantConfigurationSession.DomainController {0}", this.TenantConfigurationSession.DomainController);
			ExTraceGlobals.BackSyncTracer.TraceDebug<string>((long)SyncConfiguration.TraceId, "this.RecipientSession.DomainController {0}", this.RecipientSession.DomainController);
		}

		// Token: 0x06006224 RID: 25124 RVA: 0x00150344 File Offset: 0x0014E544
		public virtual Result<ADRawEntry>[] GetProperties(ADObjectId[] objectIds, PropertyDefinition[] properties)
		{
			if (ExTraceGlobals.BackSyncTracer.IsTraceEnabled(TraceType.DebugTrace))
			{
				ExTraceGlobals.BackSyncTracer.TraceDebug<string>((long)SyncConfiguration.TraceId, "GetProperties objectIds {0}", string.Join(";", this.GetDistinguishedNames(objectIds)));
			}
			return this.RecipientSession.ReadMultipleWithDeletedObjects(objectIds, properties);
		}

		// Token: 0x06006225 RID: 25125 RVA: 0x00150394 File Offset: 0x0014E594
		public virtual Result<ADRawEntry>[] GetOrganizationProperties(ADObjectId[] organizationOUIds, PropertyDefinition[] properties)
		{
			if (ExTraceGlobals.BackSyncTracer.IsTraceEnabled(TraceType.DebugTrace))
			{
				ExTraceGlobals.BackSyncTracer.TraceDebug<string>((long)SyncConfiguration.TraceId, "GetOrganizationProperties objectIds {0}", string.Join(";", this.GetDistinguishedNames(organizationOUIds)));
			}
			return this.TenantConfigurationSession.ReadMultipleOrganizationProperties(organizationOUIds, properties);
		}

		// Token: 0x06006226 RID: 25126 RVA: 0x001503E4 File Offset: 0x0014E5E4
		internal void AddErrorSyncObject(SyncObject errorObject, Exception errorDetail)
		{
			ExTraceGlobals.BackSyncTracer.TraceDebug((long)SyncConfiguration.TraceId, "SyncConfiguration.AddErrorSyncObject entering");
			ExTraceGlobals.BackSyncTracer.TraceDebug<string>((long)SyncConfiguration.TraceId, "SyncConfiguration.AddErrorSyncObject errorObject {0}", errorObject.ObjectId);
			ExTraceGlobals.BackSyncTracer.TraceDebug<string>((long)SyncConfiguration.TraceId, "SyncConfiguration.AddErrorSyncObject errorDetail {0}", errorDetail.ToString());
			bool flag = false;
			string objectId = errorObject.ObjectId;
			foreach (SyncObject syncObject in this.ErrorSyncObjects.Keys)
			{
				if (!string.IsNullOrEmpty(syncObject.ObjectId) && syncObject.ObjectId.Equals(objectId, StringComparison.OrdinalIgnoreCase))
				{
					ExTraceGlobals.BackSyncTracer.TraceDebug<string>((long)SyncConfiguration.TraceId, "SyncConfiguration.AddErrorSyncObject already exists {0} in ErrorSyncObjects", objectId);
					flag = true;
					break;
				}
			}
			if (!flag)
			{
				ExTraceGlobals.BackSyncTracer.TraceDebug<string>((long)SyncConfiguration.TraceId, "SyncConfiguration.AddErrorSyncObject add {0} to ErrorSyncObjects", objectId);
				this.ErrorSyncObjects.Add(errorObject, errorDetail);
			}
		}

		// Token: 0x06006227 RID: 25127 RVA: 0x001504E8 File Offset: 0x0014E6E8
		protected static Guid FindInvocationIdByFqdn(string dcFqdn, PartitionId partitionId)
		{
			ExTraceGlobals.BackSyncTracer.TraceDebug<string>((long)SyncConfiguration.TraceId, "FindInvocationIdByFqdn dcFqdn {0}", dcFqdn);
			ITopologyConfigurationSession topologyConfigurationSession = DirectorySessionFactory.Default.CreateTopologyConfigurationSession(true, ConsistencyMode.IgnoreInvalid, ADSessionSettings.FromAccountPartitionRootOrgScopeSet(partitionId), 678, "FindInvocationIdByFqdn", "f:\\15.00.1497\\sources\\dev\\data\\src\\directory\\Sync\\BackSync\\Configuration\\SyncConfiguration.cs");
			Guid invocationIdByFqdn = topologyConfigurationSession.GetInvocationIdByFqdn(dcFqdn);
			ExTraceGlobals.BackSyncTracer.TraceDebug<Guid>((long)SyncConfiguration.TraceId, "FindInvocationIdByFqdn invocationId {0}", invocationIdByFqdn);
			return invocationIdByFqdn;
		}

		// Token: 0x06006228 RID: 25128 RVA: 0x0015054C File Offset: 0x0014E74C
		protected void UpdateSyncCookieErrorObjectsAndFailureCounts(ISyncCookie syncCookie)
		{
			try
			{
				ExTraceGlobals.BackSyncTracer.TraceDebug<int>((long)SyncConfiguration.TraceId, "SyncConfiguration.UpdateSyncCookieErrorObjectsAndFailureCounts this.ErrorSyncObjects.Count = {0}", this.ErrorSyncObjects.Count);
				foreach (KeyValuePair<SyncObject, Exception> keyValuePair in this.ErrorSyncObjects)
				{
					SyncObject key = keyValuePair.Key;
					string objectId = key.ObjectId;
					ExTraceGlobals.BackSyncTracer.TraceDebug<string>((long)SyncConfiguration.TraceId, "SyncConfiguration.UpdateSyncCookieErrorObjectsAndFailureCounts objectId {0}", objectId);
					Exception value = keyValuePair.Value;
					ExTraceGlobals.BackSyncTracer.TraceDebug<string>((long)SyncConfiguration.TraceId, "SyncConfiguration.UpdateSyncCookieErrorObjectsAndFailureCounts errorDetail {0}", value.ToString());
					int num2;
					if (syncCookie.ErrorObjectsAndFailureCounts.ContainsKey(objectId))
					{
						int num = syncCookie.ErrorObjectsAndFailureCounts[objectId];
						ExTraceGlobals.BackSyncTracer.TraceDebug<int>((long)SyncConfiguration.TraceId, "SyncConfiguration.UpdateSyncCookieErrorObjectsAndFailureCounts previous error count = {0}", num);
						num2 = num + 1;
						syncCookie.ErrorObjectsAndFailureCounts[objectId] = num2;
						ExTraceGlobals.BackSyncTracer.TraceDebug<int>((long)SyncConfiguration.TraceId, "SyncConfiguration.UpdateSyncCookieErrorObjectsAndFailureCounts new error count = {0}", num2);
					}
					else
					{
						ExTraceGlobals.BackSyncTracer.TraceDebug((long)SyncConfiguration.TraceId, "SyncConfiguration.UpdateSyncCookieErrorObjectsAndFailureCounts add new error object to backsync cookie");
						num2 = 1;
						syncCookie.ErrorObjectsAndFailureCounts.Add(objectId, num2);
					}
					if (num2 >= 3)
					{
						ExTraceGlobals.BackSyncTracer.TraceDebug<string>((long)SyncConfiguration.TraceId, "SyncConfiguration.UpdateSyncCookieErrorObjectsAndFailureCounts mark exclude-flag on object {0}", objectId);
						ADRecipient adrecipient = this.RecipientSession.Read(key.Id);
						if (adrecipient != null)
						{
							adrecipient[ADRecipientSchema.ExcludedFromBackSync] = true;
							this.RecipientSession.Save(adrecipient);
							if (this.EventLogger != null)
							{
								this.EventLogger.LogSerializationFailedEvent(objectId, num2);
							}
						}
					}
				}
			}
			catch (Exception ex)
			{
				ExTraceGlobals.BackSyncTracer.TraceError<string>((long)SyncConfiguration.TraceId, "SyncConfiguration.UpdateSyncCookieErrorObjectsAndFailureCounts exception {0}", ex.ToString());
			}
		}

		// Token: 0x06006229 RID: 25129
		protected abstract DateTime GetLastReadFailureStartTime();

		// Token: 0x0600622A RID: 25130
		protected abstract DateTime GetSyncSequenceStartTime();

		// Token: 0x0600622B RID: 25131
		protected abstract bool IsDCFailoverResilienceEnabled();

		// Token: 0x0600622C RID: 25132 RVA: 0x00150740 File Offset: 0x0014E940
		protected bool IsTransientException(Exception e)
		{
			if (e is DataSourceTransientException)
			{
				ExTraceGlobals.BackSyncTracer.TraceDebug((long)SyncConfiguration.TraceId, "DataSourceTransientException is transient");
				return true;
			}
			ExTraceGlobals.BackSyncTracer.TraceDebug<string>((long)SyncConfiguration.TraceId, "Non-transient exception {0}", e.ToString());
			return false;
		}

		// Token: 0x0600622D RID: 25133 RVA: 0x00150780 File Offset: 0x0014E980
		protected bool IsSubsequentFailedAttempt()
		{
			bool flag = this.GetLastReadFailureStartTime() != DateTime.MinValue;
			ExTraceGlobals.BackSyncTracer.TraceDebug<bool>((long)SyncConfiguration.TraceId, "isSubsequentFailedAttempt {0}", flag);
			return flag;
		}

		// Token: 0x0600622E RID: 25134 RVA: 0x001507B8 File Offset: 0x0014E9B8
		protected bool IsFailoverTimeoutExceeded(DateTime now)
		{
			if (this.IsDCFailoverResilienceEnabled() && this.GetSyncSequenceStartTime() != DateTime.MinValue)
			{
				return this.ShouldSmartFailover(now);
			}
			bool flag = now.Subtract(this.GetLastReadFailureStartTime()) > SyncConfiguration.FailoverTimeout;
			ExTraceGlobals.BackSyncTracer.TraceDebug<bool, bool, DateTime>((long)SyncConfiguration.TraceId, "isFailoverTimeoutExceeded: {0} EnableDCFailoverResilienceForIncrementalSync: {1} SyncSequenceStartTime: {2}", flag, SyncConfiguration.EnableDCFailoverResilienceForIncrementalSync(), this.GetSyncSequenceStartTime());
			return flag;
		}

		// Token: 0x0600622F RID: 25135 RVA: 0x00150828 File Offset: 0x0014EA28
		private bool ShouldSmartFailover(DateTime now)
		{
			TimeSpan timeSpan = this.GetLastReadFailureStartTime().Subtract(this.GetSyncSequenceStartTime());
			bool flag = now.Subtract(this.GetLastReadFailureStartTime()).TotalSeconds > timeSpan.TotalSeconds * (double)SyncConfiguration.FailoverWaitTimeFactor() / 100.0;
			ExTraceGlobals.BackSyncTracer.TraceDebug<bool, DateTime, DateTime>((long)SyncConfiguration.TraceId, "shouldSmartFailover: {0} FailureStartTime: {1} SyncSequenceStartTime: {2}", flag, this.GetLastReadFailureStartTime(), this.GetSyncSequenceStartTime());
			return flag;
		}

		// Token: 0x06006230 RID: 25136 RVA: 0x001508A4 File Offset: 0x0014EAA4
		private string[] GetDistinguishedNames(ADObjectId[] objectIds)
		{
			string[] array = new string[objectIds.Length];
			for (int i = 0; i < objectIds.Length; i++)
			{
				array[i] = objectIds[i].DistinguishedName;
			}
			return array;
		}

		// Token: 0x040041B2 RID: 16818
		public const string BackSyncSettingsOverrideRegistryKey = "SOFTWARE\\Microsoft\\ExchangeServer\\v15\\BackSync";

		// Token: 0x040041B3 RID: 16819
		internal const int MAX_ERROR_OBJECT_RETRY_COUNT = 3;

		// Token: 0x040041B4 RID: 16820
		private const string FailoverTimeoutValueName = "FailoverTimeout";

		// Token: 0x040041B5 RID: 16821
		private const int DefaultFailoverTimeout = 60;

		// Token: 0x040041B6 RID: 16822
		private ITenantRecipientSession recipientSession;

		// Token: 0x040041B7 RID: 16823
		private ITopologyConfigurationSession rootOrgConfigurationSession;

		// Token: 0x040041B8 RID: 16824
		private ITenantConfigurationSession tenantConfigurationSession;

		// Token: 0x040041B9 RID: 16825
		private Dictionary<SyncObject, Exception> errorSyncObjects;

		// Token: 0x040041BA RID: 16826
		internal static readonly Func<bool> EnableIgnoreCookieDCDuringTenantFaultin = () => SyncConfiguration.GetConfigurationValue<int>("EnableIgnoreCookieDCDuringTenantFaultin", 0) == 1;

		// Token: 0x040041BB RID: 16827
		internal static readonly Func<long> DirSyncBasedTenantFullSyncThreshold = () => SyncConfiguration.GetConfigurationValue<long>("DirSyncBasedTenantFullSyncThreshold", -1L);

		// Token: 0x040041BC RID: 16828
		internal static readonly Func<bool> EnableDCFailoverResilienceForIncrementalSync = () => SyncConfiguration.GetConfigurationValue<int>("EnableDCFailoverResilienceForIncrementalSync", 0) == 1;

		// Token: 0x040041BD RID: 16829
		internal static readonly Func<bool> EnableDCFailoverResilienceForTenantFullSync = () => SyncConfiguration.GetConfigurationValue<int>("EnableDCFailoverResilienceForTenantFullSync", 0) == 1;

		// Token: 0x040041BE RID: 16830
		internal static readonly Func<int> FailoverWaitTimeFactor = () => SyncConfiguration.GetConfigurationValue<int>("FailoverWaitTimeFactor", 25);

		// Token: 0x040041BF RID: 16831
		internal static readonly Func<bool> EnableContainerizedUsnChangedOptimization = () => SyncConfiguration.GetConfigurationValue<int>("EnableContainerizedUsnChangedOptimization", 0) == 1;

		// Token: 0x040041C0 RID: 16832
		internal static readonly Func<bool> EnableSyncingBackCloudLinks = () => SyncConfiguration.GetConfigurationValue<int>("EnableSyncingBackCloudLinks", 1) == 1;

		// Token: 0x040041C1 RID: 16833
		internal static readonly Func<bool> EnableCloudPublicDelegatesRecipientFiltering = () => SyncConfiguration.GetConfigurationValue<int>("EnableCloudPublicDelegatesRecipientFiltering", 0) == 1;
	}
}
