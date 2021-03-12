using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Data.Directory;

namespace Microsoft.Exchange.Data.Directory
{
	// Token: 0x02000078 RID: 120
	[Serializable]
	internal abstract class ADServerSettings : ICloneable, IEquatable<ADServerSettings>
	{
		// Token: 0x06000583 RID: 1411 RVA: 0x0001ED30 File Offset: 0x0001CF30
		public ADServerSettings()
		{
			this.syncRoot = new object();
			this.preferredGlobalCatalog = new Dictionary<string, Fqdn>(StringComparer.OrdinalIgnoreCase);
			this.configurationDomainController = new Dictionary<string, Fqdn>(StringComparer.OrdinalIgnoreCase);
			this.serverPerDomain = new Dictionary<ADObjectId, Fqdn>(ADObjectIdEqualityComparer.Instance);
			this.cachedPreferredDCList = new MultiValuedProperty<Fqdn>();
			this.lastUsedDc = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
			this.serversDown = new List<Fqdn>();
			this.sdListLock = new object();
			this.writeOriginatingChangeTimestamp = ADGlobalConfigSettings.WriteOriginatingChangeTimestamp;
			this.writeShadowProperties = ADGlobalConfigSettings.WriteShadowProperties;
			this.disableGls = false;
		}

		// Token: 0x1700012C RID: 300
		// (get) Token: 0x06000584 RID: 1412 RVA: 0x0001EDCC File Offset: 0x0001CFCC
		// (set) Token: 0x06000585 RID: 1413 RVA: 0x0001EDD4 File Offset: 0x0001CFD4
		internal virtual ADObjectId RecipientViewRoot { get; set; }

		// Token: 0x1700012D RID: 301
		// (get) Token: 0x06000586 RID: 1414 RVA: 0x0001EDDD File Offset: 0x0001CFDD
		// (set) Token: 0x06000587 RID: 1415 RVA: 0x0001EDE8 File Offset: 0x0001CFE8
		internal virtual bool ViewEntireForest
		{
			get
			{
				return this.RecipientViewRoot == null;
			}
			set
			{
				if (this.RecipientViewRoot == null != value)
				{
					ADObjectId recipientViewRoot = null;
					if (!value)
					{
						recipientViewRoot = TopologyProvider.GetInstance().GetDomainNamingContextForLocalForest();
					}
					this.RecipientViewRoot = recipientViewRoot;
				}
			}
		}

		// Token: 0x1700012E RID: 302
		// (get) Token: 0x06000588 RID: 1416 RVA: 0x0001EE18 File Offset: 0x0001D018
		// (set) Token: 0x06000589 RID: 1417 RVA: 0x0001EE20 File Offset: 0x0001D020
		internal bool WriteOriginatingChangeTimestamp
		{
			get
			{
				return this.writeShadowProperties;
			}
			set
			{
				this.writeOriginatingChangeTimestamp = value;
			}
		}

		// Token: 0x1700012F RID: 303
		// (get) Token: 0x0600058A RID: 1418 RVA: 0x0001EE29 File Offset: 0x0001D029
		// (set) Token: 0x0600058B RID: 1419 RVA: 0x0001EE31 File Offset: 0x0001D031
		internal bool WriteShadowProperties
		{
			get
			{
				return this.writeShadowProperties;
			}
			set
			{
				this.writeShadowProperties = value;
			}
		}

		// Token: 0x17000130 RID: 304
		// (get) Token: 0x0600058C RID: 1420 RVA: 0x0001EE3A File Offset: 0x0001D03A
		// (set) Token: 0x0600058D RID: 1421 RVA: 0x0001EE42 File Offset: 0x0001D042
		internal bool DisableGls
		{
			get
			{
				return this.disableGls;
			}
			set
			{
				this.disableGls = value;
			}
		}

		// Token: 0x17000131 RID: 305
		// (get) Token: 0x0600058E RID: 1422 RVA: 0x0001EE4B File Offset: 0x0001D04B
		// (set) Token: 0x0600058F RID: 1423 RVA: 0x0001EE53 File Offset: 0x0001D053
		internal bool ForceADInTemplateScope { get; set; }

		// Token: 0x17000132 RID: 306
		// (get) Token: 0x06000590 RID: 1424 RVA: 0x0001EE5C File Offset: 0x0001D05C
		internal virtual bool IsUpdatableByADSession
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000133 RID: 307
		// (get) Token: 0x06000591 RID: 1425
		protected abstract bool EnforceIsUpdatableByADSession { get; }

		// Token: 0x17000134 RID: 308
		// (get) Token: 0x06000592 RID: 1426 RVA: 0x0001EE5F File Offset: 0x0001D05F
		// (set) Token: 0x06000593 RID: 1427 RVA: 0x0001EE67 File Offset: 0x0001D067
		internal string Token { get; set; }

		// Token: 0x06000594 RID: 1428 RVA: 0x0001EE70 File Offset: 0x0001D070
		internal static string GetPartitionFqdnFromADServerFqdn(string fqdn)
		{
			if (string.IsNullOrEmpty(fqdn))
			{
				throw new ArgumentNullException("fqdn");
			}
			return fqdn.Substring(fqdn.IndexOf(".") + 1);
		}

		// Token: 0x06000595 RID: 1429 RVA: 0x0001EE98 File Offset: 0x0001D098
		internal static ADServerInfo GetServerInfoFromFqdn(string fqdn, ConnectionType connectionType)
		{
			PooledLdapConnection pooledLdapConnection = null;
			string empty = string.Empty;
			ADServerInfo adserverInfo;
			try
			{
				string partitionFqdn = Globals.IsMicrosoftHostedOnly ? ADServerSettings.GetPartitionFqdnFromADServerFqdn(fqdn) : TopologyProvider.LocalForestFqdn;
				pooledLdapConnection = ConnectionPoolManager.GetConnection(connectionType, partitionFqdn, null, fqdn, (connectionType == ConnectionType.GlobalCatalog) ? TopologyProvider.GetInstance().DefaultGCPort : TopologyProvider.GetInstance().DefaultDCPort);
				string writableNC = pooledLdapConnection.ADServerInfo.WritableNC;
				if (!pooledLdapConnection.SessionOptions.HostName.Equals(fqdn, StringComparison.OrdinalIgnoreCase))
				{
					throw new ADOperationException(DirectoryStrings.ErrorInvalidServerFqdn(fqdn, pooledLdapConnection.SessionOptions.HostName));
				}
				adserverInfo = pooledLdapConnection.ADServerInfo;
			}
			finally
			{
				if (pooledLdapConnection != null)
				{
					pooledLdapConnection.ReturnToPool();
				}
			}
			return adserverInfo;
		}

		// Token: 0x06000596 RID: 1430 RVA: 0x0001EF44 File Offset: 0x0001D144
		internal static void ThrowIfServerNameDoesntMatchPartitionId(string serverName, string partitionFqdn)
		{
			if (!ADServerSettings.IsServerNamePartitionSameAsPartitionId(serverName, partitionFqdn))
			{
				throw new DomainControllerFromWrongDomainException(DirectoryStrings.ExceptionSeverNotInPartition(serverName, partitionFqdn));
			}
		}

		// Token: 0x06000597 RID: 1431 RVA: 0x0001EF5C File Offset: 0x0001D15C
		[Conditional("DEBUG")]
		internal static void AssertIfServerNameDoesntMatchPartitionId(string serverName, string partitionFqdn, string additionalInfo)
		{
			if (string.IsNullOrEmpty(serverName))
			{
				return;
			}
			string.IsNullOrEmpty(partitionFqdn);
		}

		// Token: 0x06000598 RID: 1432 RVA: 0x0001EF70 File Offset: 0x0001D170
		internal static bool IsServerNamePartitionSameAsPartitionId(string serverName, string partitionFqdn)
		{
			if (string.IsNullOrEmpty(serverName))
			{
				throw new ArgumentNullException("serverName");
			}
			if (string.IsNullOrEmpty(partitionFqdn))
			{
				throw new ArgumentNullException("partitionFqdn");
			}
			IPAddress ipaddress;
			return !Globals.IsMicrosoftHostedOnly || !serverName.Contains(".") || IPAddress.TryParse(serverName, out ipaddress) || partitionFqdn.Trim().EndsWith(ADServerSettings.GetPartitionFqdnFromADServerFqdn(serverName).Trim(), StringComparison.OrdinalIgnoreCase);
		}

		// Token: 0x06000599 RID: 1433 RVA: 0x0001EFDC File Offset: 0x0001D1DC
		internal virtual bool IsFailoverRequired()
		{
			if (this.EnforceIsUpdatableByADSession && !this.IsUpdatableByADSession)
			{
				return false;
			}
			foreach (Fqdn fqdn in this.serversDown)
			{
				lock (this.syncRoot)
				{
					IEnumerable<Fqdn>[] array = new IEnumerable<Fqdn>[]
					{
						this.configurationDomainController.Values,
						this.preferredGlobalCatalog.Values,
						this.PreferredDomainControllers
					};
					foreach (IEnumerable<Fqdn> enumerable in array)
					{
						foreach (Fqdn fqdn2 in enumerable)
						{
							string text = fqdn2;
							if (text.Equals(fqdn))
							{
								return true;
							}
						}
					}
				}
				if (ADRunspaceServerSettingsProvider.GetInstance().IsServerKnown(fqdn))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x0600059A RID: 1434 RVA: 0x0001F120 File Offset: 0x0001D320
		internal virtual void MarkDcDown(Fqdn fqdn)
		{
			if (this.EnforceIsUpdatableByADSession && !this.IsUpdatableByADSession)
			{
				throw new NotSupportedException("MarkDcDown is not supported by " + base.GetType().Name);
			}
			if (this.serversDown.Contains(fqdn))
			{
				return;
			}
			lock (this.sdListLock)
			{
				if (!this.serversDown.Contains(fqdn))
				{
					this.serversDown = new List<Fqdn>(this.serversDown)
					{
						fqdn
					};
				}
			}
		}

		// Token: 0x0600059B RID: 1435 RVA: 0x0001F1BC File Offset: 0x0001D3BC
		internal virtual void MarkDcUp(Fqdn fqdn)
		{
			if (this.EnforceIsUpdatableByADSession && !this.IsUpdatableByADSession)
			{
				throw new NotSupportedException("MarkDcUp is not supported by " + base.GetType().Name);
			}
			if (!this.serversDown.Contains(fqdn))
			{
				return;
			}
			lock (this.sdListLock)
			{
				if (this.serversDown.Contains(fqdn))
				{
					List<Fqdn> list = new List<Fqdn>(this.serversDown);
					list.Remove(fqdn);
					this.serversDown = list;
				}
			}
		}

		// Token: 0x0600059C RID: 1436 RVA: 0x0001F25C File Offset: 0x0001D45C
		internal virtual bool TryFailover(out ADServerSettings newServerSettings, out string failToFailOverReason, bool forestWideAffinityRequested = false)
		{
			if (this.EnforceIsUpdatableByADSession && !this.IsUpdatableByADSession)
			{
				throw new NotSupportedException("TryFailover is not supported on " + base.GetType().Name);
			}
			bool result = true;
			failToFailOverReason = null;
			ADServerSettings adserverSettings = (ADServerSettings)this.Clone();
			ADRunspaceServerSettingsProvider instance = ADRunspaceServerSettingsProvider.GetInstance();
			string token = adserverSettings.Token;
			foreach (Fqdn fqdn in adserverSettings.serversDown)
			{
				adserverSettings.RemovePreferredDC(fqdn);
				try
				{
					List<string> list = new List<string>();
					foreach (KeyValuePair<string, Fqdn> keyValuePair in adserverSettings.preferredGlobalCatalog)
					{
						if (string.Equals(fqdn, keyValuePair.Value, StringComparison.OrdinalIgnoreCase))
						{
							string key = keyValuePair.Key;
							list.Add(key);
						}
					}
					foreach (string partitionFqdn in list)
					{
						ExTraceGlobals.ServerSettingsProviderTracer.TraceDebug<string>((long)instance.GetHashCode(), "GetGcOnlyRunspaceServerSettings for token {0}", token ?? "<null>");
						bool flag;
						ADServerInfo gcFromToken = instance.GetGcFromToken(partitionFqdn, token, out flag, forestWideAffinityRequested);
						adserverSettings.SetPreferredGlobalCatalog(partitionFqdn, gcFromToken);
						if (adserverSettings.Token != null)
						{
							adserverSettings.SetConfigurationDomainController(partitionFqdn, gcFromToken);
						}
						if (adserverSettings.Token != null || flag)
						{
							adserverSettings.AddPreferredDC(gcFromToken);
						}
					}
				}
				catch (ADTransientException ex)
				{
					failToFailOverReason = ex.ToString();
					result = false;
				}
				catch (ADExternalException ex2)
				{
					failToFailOverReason = ex2.ToString();
					result = false;
				}
				if (token == null)
				{
					List<string> list2 = new List<string>();
					foreach (KeyValuePair<string, Fqdn> keyValuePair2 in adserverSettings.configurationDomainController)
					{
						if (string.Equals(fqdn, keyValuePair2.Value, StringComparison.OrdinalIgnoreCase))
						{
							string key2 = keyValuePair2.Key;
							list2.Add(key2);
						}
					}
					foreach (string text in list2)
					{
						ADServerInfo configDc = instance.GetConfigDc(text);
						if (configDc != null)
						{
							adserverSettings.SetConfigurationDomainController(text, configDc);
						}
					}
				}
			}
			adserverSettings.serversDown.Clear();
			newServerSettings = adserverSettings;
			return result;
		}

		// Token: 0x17000135 RID: 309
		// (get) Token: 0x0600059D RID: 1437 RVA: 0x0001F574 File Offset: 0x0001D774
		internal IDictionary<string, Fqdn> PreferredGlobalCatalogs
		{
			get
			{
				IDictionary<string, Fqdn> result;
				lock (this.syncRoot)
				{
					result = new Dictionary<string, Fqdn>(this.preferredGlobalCatalog, StringComparer.OrdinalIgnoreCase);
				}
				return result;
			}
		}

		// Token: 0x0600059E RID: 1438 RVA: 0x0001F5C0 File Offset: 0x0001D7C0
		internal virtual Fqdn PreferredGlobalCatalog(string partitionFqdn)
		{
			Fqdn result;
			lock (this.syncRoot)
			{
				Fqdn fqdn;
				this.preferredGlobalCatalog.TryGetValue(partitionFqdn, out fqdn);
				result = fqdn;
			}
			return result;
		}

		// Token: 0x0600059F RID: 1439 RVA: 0x0001F60C File Offset: 0x0001D80C
		internal virtual void SetPreferredGlobalCatalog(string partitionFqdn, ADServerInfo serverInfo)
		{
			if (this.EnforceIsUpdatableByADSession && !this.IsUpdatableByADSession)
			{
				throw new NotSupportedException("SetPreferredGlobalCatalog passing ADServerInfo is not supported on " + base.GetType().Name);
			}
			lock (this.syncRoot)
			{
				this.preferredGlobalCatalog[partitionFqdn] = new Fqdn(serverInfo.Fqdn);
			}
		}

		// Token: 0x060005A0 RID: 1440 RVA: 0x0001F688 File Offset: 0x0001D888
		internal virtual void SetPreferredGlobalCatalog(string partitionFqdn, Fqdn fqdn)
		{
			if (this.EnforceIsUpdatableByADSession && !this.IsUpdatableByADSession)
			{
				throw new NotSupportedException("SetPreferredGlobalCatalog passing Fqdn is not supported on " + base.GetType().Name);
			}
			ADServerSettings.GetServerInfoFromFqdn(fqdn, ConnectionType.GlobalCatalog);
			lock (this.syncRoot)
			{
				this.preferredGlobalCatalog[partitionFqdn] = fqdn;
			}
		}

		// Token: 0x17000136 RID: 310
		// (get) Token: 0x060005A1 RID: 1441 RVA: 0x0001F708 File Offset: 0x0001D908
		internal IDictionary<string, Fqdn> ConfigurationDomainControllers
		{
			get
			{
				IDictionary<string, Fqdn> result;
				lock (this.syncRoot)
				{
					result = new Dictionary<string, Fqdn>(this.configurationDomainController, StringComparer.OrdinalIgnoreCase);
				}
				return result;
			}
		}

		// Token: 0x060005A2 RID: 1442 RVA: 0x0001F754 File Offset: 0x0001D954
		internal virtual Fqdn ConfigurationDomainController(string partitionFqdn)
		{
			Fqdn result;
			lock (this.syncRoot)
			{
				Fqdn fqdn;
				this.configurationDomainController.TryGetValue(partitionFqdn, out fqdn);
				result = fqdn;
			}
			return result;
		}

		// Token: 0x060005A3 RID: 1443 RVA: 0x0001F7A0 File Offset: 0x0001D9A0
		internal virtual void SetConfigurationDomainController(string partitionFqdn, ADServerInfo serverInfo)
		{
			if (this.EnforceIsUpdatableByADSession && !this.IsUpdatableByADSession)
			{
				throw new NotSupportedException("SetConfigurationDomainController passing ADServerInfo is not supported on " + base.GetType().Name);
			}
			lock (this.syncRoot)
			{
				this.configurationDomainController[partitionFqdn] = new Fqdn(serverInfo.Fqdn);
			}
		}

		// Token: 0x060005A4 RID: 1444 RVA: 0x0001F81C File Offset: 0x0001DA1C
		internal virtual void SetConfigurationDomainController(string partitionFqdn, Fqdn fqdn)
		{
			if (this.EnforceIsUpdatableByADSession && !this.IsUpdatableByADSession)
			{
				throw new NotSupportedException("SetConfigurationDomainController passing Fqdn is not supported on " + base.GetType().Name);
			}
			ADServerSettings.GetServerInfoFromFqdn(fqdn, ConnectionType.DomainController);
			lock (this.syncRoot)
			{
				this.configurationDomainController[partitionFqdn] = fqdn;
			}
		}

		// Token: 0x17000137 RID: 311
		// (get) Token: 0x060005A5 RID: 1445 RVA: 0x0001F89C File Offset: 0x0001DA9C
		internal virtual MultiValuedProperty<Fqdn> PreferredDomainControllers
		{
			get
			{
				return this.cachedPreferredDCList;
			}
		}

		// Token: 0x060005A6 RID: 1446 RVA: 0x0001F8A4 File Offset: 0x0001DAA4
		internal virtual string GetPreferredDC(ADObjectId domain)
		{
			Fqdn fqdn;
			if (this.serverPerDomain.TryGetValue(domain, out fqdn))
			{
				return fqdn;
			}
			return null;
		}

		// Token: 0x060005A7 RID: 1447 RVA: 0x0001F8C9 File Offset: 0x0001DAC9
		internal virtual void AddPreferredDC(ADServerInfo serverInfo)
		{
			if (this.EnforceIsUpdatableByADSession && !this.IsUpdatableByADSession)
			{
				throw new NotSupportedException("AddPreferredDC passing ADServerInfo is not supported on " + base.GetType().Name);
			}
			this.InternalAddPreferredDC(serverInfo);
		}

		// Token: 0x060005A8 RID: 1448 RVA: 0x0001F900 File Offset: 0x0001DB00
		internal virtual void AddPreferredDC(Fqdn fqdn)
		{
			if (this.EnforceIsUpdatableByADSession && !this.IsUpdatableByADSession)
			{
				throw new NotSupportedException("AddPreferredDC passing Fqdn is not supported on " + base.GetType().Name);
			}
			ADServerInfo serverInfoFromFqdn = ADServerSettings.GetServerInfoFromFqdn(fqdn, ConnectionType.DomainController);
			this.InternalAddPreferredDC(serverInfoFromFqdn);
		}

		// Token: 0x060005A9 RID: 1449 RVA: 0x0001F94C File Offset: 0x0001DB4C
		internal string LastUsedDc(string partitionFqdn)
		{
			string result;
			lock (this.syncRoot)
			{
				string text;
				this.lastUsedDc.TryGetValue(partitionFqdn, out text);
				result = text;
			}
			return result;
		}

		// Token: 0x060005AA RID: 1450 RVA: 0x0001F998 File Offset: 0x0001DB98
		internal void SetLastUsedDc(string partitionFqdn, string serverName)
		{
			lock (this.syncRoot)
			{
				this.lastUsedDc[partitionFqdn] = serverName;
			}
		}

		// Token: 0x060005AB RID: 1451 RVA: 0x0001F9E0 File Offset: 0x0001DBE0
		protected virtual void CopyTo(object targetObj)
		{
			ArgumentValidator.ThrowIfNull("targetObj", targetObj);
			ArgumentValidator.ThrowIfTypeInvalid<ADServerSettings>("targetObj", targetObj);
			ADServerSettings adserverSettings = (ADServerSettings)targetObj;
			adserverSettings.Token = this.Token;
			lock (this.syncRoot)
			{
				adserverSettings.preferredGlobalCatalog = new Dictionary<string, Fqdn>(this.preferredGlobalCatalog, StringComparer.OrdinalIgnoreCase);
				adserverSettings.configurationDomainController = new Dictionary<string, Fqdn>(this.configurationDomainController, StringComparer.OrdinalIgnoreCase);
			}
			adserverSettings.RecipientViewRoot = this.RecipientViewRoot;
			adserverSettings.serverPerDomain = new Dictionary<ADObjectId, Fqdn>(this.serverPerDomain, ADObjectIdEqualityComparer.Instance);
			adserverSettings.cachedPreferredDCList = new MultiValuedProperty<Fqdn>(adserverSettings.serverPerDomain.Values);
			adserverSettings.serversDown = new List<Fqdn>(this.serversDown);
			adserverSettings.writeOriginatingChangeTimestamp = this.writeOriginatingChangeTimestamp;
			adserverSettings.writeShadowProperties = this.writeShadowProperties;
			adserverSettings.disableGls = this.disableGls;
			adserverSettings.ForceADInTemplateScope = this.ForceADInTemplateScope;
		}

		// Token: 0x060005AC RID: 1452
		public abstract object Clone();

		// Token: 0x060005AD RID: 1453 RVA: 0x0001FAE8 File Offset: 0x0001DCE8
		public bool Equals(ADServerSettings other)
		{
			if (other == null)
			{
				return false;
			}
			if (this.writeOriginatingChangeTimestamp != other.writeOriginatingChangeTimestamp || this.writeShadowProperties != other.writeShadowProperties || this.disableGls != other.disableGls || this.ForceADInTemplateScope != other.ForceADInTemplateScope || !ADObjectId.Equals(this.RecipientViewRoot, other.RecipientViewRoot) || !string.Equals(this.Token, other.Token, StringComparison.OrdinalIgnoreCase) || !this.serverPerDomain.Equals(other.serverPerDomain))
			{
				return false;
			}
			bool result;
			lock (this.syncRoot)
			{
				lock (other.syncRoot)
				{
					result = (this.preferredGlobalCatalog.Equals(other.preferredGlobalCatalog) && this.configurationDomainController.Equals(other.configurationDomainController));
				}
			}
			return result;
		}

		// Token: 0x060005AE RID: 1454 RVA: 0x0001FBF4 File Offset: 0x0001DDF4
		protected void InternalAddPreferredDC(ADServerInfo serverInfo)
		{
			if (serverInfo == null)
			{
				throw new ArgumentNullException("serverInfo");
			}
			if (string.IsNullOrEmpty(serverInfo.WritableNC))
			{
				throw new ArgumentException("serverInfo.WritableNC should not be null or empty");
			}
			ADObjectId key = new ADObjectId(serverInfo.WritableNC);
			if (this.serverPerDomain.ContainsKey(key))
			{
				return;
			}
			Fqdn value = new Fqdn(serverInfo.Fqdn);
			lock (this.syncRoot)
			{
				if (!this.serverPerDomain.ContainsKey(key))
				{
					this.serverPerDomain = new Dictionary<ADObjectId, Fqdn>(this.serverPerDomain)
					{
						{
							key,
							value
						}
					};
					this.cachedPreferredDCList = new MultiValuedProperty<Fqdn>(this.serverPerDomain.Values);
				}
			}
		}

		// Token: 0x060005AF RID: 1455 RVA: 0x0001FCBC File Offset: 0x0001DEBC
		private void RemovePreferredDC(string fqdn)
		{
			Fqdn value = new Fqdn(fqdn);
			if (!this.serverPerDomain.ContainsValue(value))
			{
				return;
			}
			lock (this.syncRoot)
			{
				ADObjectId adobjectId = null;
				foreach (KeyValuePair<ADObjectId, Fqdn> keyValuePair in this.serverPerDomain)
				{
					if (string.Equals(keyValuePair.Value, fqdn, StringComparison.OrdinalIgnoreCase))
					{
						adobjectId = keyValuePair.Key;
						break;
					}
				}
				if (adobjectId != null)
				{
					Dictionary<ADObjectId, Fqdn> dictionary = new Dictionary<ADObjectId, Fqdn>(this.serverPerDomain);
					dictionary.Remove(adobjectId);
					this.serverPerDomain = dictionary;
					this.cachedPreferredDCList = new MultiValuedProperty<Fqdn>(this.serverPerDomain.Values);
				}
			}
		}

		// Token: 0x04000256 RID: 598
		private object syncRoot;

		// Token: 0x04000257 RID: 599
		private Dictionary<string, Fqdn> preferredGlobalCatalog;

		// Token: 0x04000258 RID: 600
		private Dictionary<string, Fqdn> configurationDomainController;

		// Token: 0x04000259 RID: 601
		private Dictionary<ADObjectId, Fqdn> serverPerDomain;

		// Token: 0x0400025A RID: 602
		private MultiValuedProperty<Fqdn> cachedPreferredDCList;

		// Token: 0x0400025B RID: 603
		private Dictionary<string, string> lastUsedDc;

		// Token: 0x0400025C RID: 604
		private List<Fqdn> serversDown;

		// Token: 0x0400025D RID: 605
		private object sdListLock;

		// Token: 0x0400025E RID: 606
		private bool writeOriginatingChangeTimestamp;

		// Token: 0x0400025F RID: 607
		private bool writeShadowProperties;

		// Token: 0x04000260 RID: 608
		private bool disableGls;
	}
}
