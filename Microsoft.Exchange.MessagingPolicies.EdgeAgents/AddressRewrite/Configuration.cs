using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Data.Transport;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.MessagingPolicies;

namespace Microsoft.Exchange.MessagingPolicies.AddressRewrite
{
	// Token: 0x02000018 RID: 24
	internal sealed class Configuration
	{
		// Token: 0x1700000F RID: 15
		// (get) Token: 0x06000061 RID: 97 RVA: 0x000044B0 File Offset: 0x000026B0
		internal static Configuration Current
		{
			get
			{
				DateTime utcNow = DateTime.UtcNow;
				if (Configuration.currentConfig == null || utcNow > Configuration.currentConfig.validUntil)
				{
					lock (Configuration.lockVar)
					{
						if (Configuration.currentConfig == null || utcNow > Configuration.currentConfig.validUntil)
						{
							Configuration configuration = new Configuration();
							if (configuration.Load())
							{
								Configuration.currentConfig = configuration;
							}
							else if (Configuration.currentConfig != null)
							{
								Configuration.currentConfig.validUntil += Configuration.configLoadErrorRetryInterval;
								ExTraceGlobals.AddressRewritingTracer.TraceError<DateTime>(0L, "Configuration reload failed. We will use the last-known-good configuration. Configuration reload will be attempted at: {0}", Configuration.currentConfig.validUntil);
							}
						}
					}
				}
				return Configuration.currentConfig;
			}
		}

		// Token: 0x06000062 RID: 98 RVA: 0x0000465C File Offset: 0x0000285C
		private bool Load()
		{
			LocalizedException ex = null;
			try
			{
				IConfigurationSession session = null;
				ADObjectId addressRewriteRootId = null;
				ADObjectId domainEntriesRoot = null;
				ADObjectId emailEntriesRoot = null;
				ADNotificationAdapter.RunADOperation(delegate()
				{
					session = DirectorySessionFactory.Default.GetTenantOrTopologyConfigurationSession(ConsistencyMode.IgnoreInvalid, ADSessionSettings.FromRootOrgScopeSet(), 164, "Load", "f:\\15.00.1497\\sources\\dev\\MessagingPolicies\\src\\addressrewrite\\Common\\Configuration.cs");
					session.UseConfigNC = false;
					addressRewriteRootId = new ADObjectId("OU=MSExchangeGateway");
					addressRewriteRootId = addressRewriteRootId.GetChildId("Address Rewrite Configuration");
					domainEntriesRoot = addressRewriteRootId.GetChildId("Domain Entries");
					emailEntriesRoot = addressRewriteRootId.GetChildId("Email Entries");
					AddressRewriteEntry[] entries = session.Find<AddressRewriteEntry>(domainEntriesRoot, QueryScope.OneLevel, null, null, 0);
					this.ProcessDomainEntries(entries);
				});
				ADNotificationAdapter.ReadConfigurationPaged<AddressRewriteEntry>(() => session.FindPaged<AddressRewriteEntry>(emailEntriesRoot, QueryScope.OneLevel, null, null, 1024), delegate(AddressRewriteEntry entry)
				{
					this.AddToMapTable(entry);
				});
				if (this.domainTableOutboundOnly != null)
				{
					this.domainTableOutboundOnly.Sort();
				}
				if (this.domainTableBidirectional != null)
				{
					this.domainTableBidirectional.Sort();
				}
				this.addressMapTables = new MapTable[this.mapTableList.Count];
				int num = 0;
				foreach (MapTable mapTable in this.mapTableList.Values)
				{
					this.addressMapTables[num++] = mapTable;
					mapTable.Sort();
				}
			}
			catch (TransientException ex2)
			{
				ex = ex2;
			}
			catch (DataValidationException ex3)
			{
				ex = ex3;
			}
			catch (ExchangeConfigurationException ex4)
			{
				ex = ex4;
			}
			catch (InvalidDataException ex5)
			{
				ex = new LocalizedException(new LocalizedString(ex5.Message), ex5);
			}
			if (ex != null)
			{
				ExTraceGlobals.AddressRewritingTracer.TraceError<string>((long)this.GetHashCode(), "Unable to load configuration: {0}", ex.ToString());
				Configuration.logger.LogEvent(MessagingPoliciesEventLogConstants.Tuple_AddressRewriteConfigCorrupt, DateTime.UtcNow.Hour.ToString(), new object[]
				{
					ex.LocalizedString
				});
				return false;
			}
			Configuration.logger.LogEvent(MessagingPoliciesEventLogConstants.Tuple_AddressRewriteConfigLoaded, null, new object[0]);
			ExTraceGlobals.AddressRewritingTracer.TraceDebug((long)this.GetHashCode(), "Configuration loaded");
			return true;
		}

		// Token: 0x06000063 RID: 99 RVA: 0x00004854 File Offset: 0x00002A54
		private void ProcessDomainEntries(IEnumerable<AddressRewriteEntry> entries)
		{
			foreach (AddressRewriteEntry entry in entries)
			{
				this.AddDomainMapping(entry);
			}
		}

		// Token: 0x06000064 RID: 100 RVA: 0x0000489C File Offset: 0x00002A9C
		private void AddToMapTable(AddressRewriteEntry mapEntry)
		{
			string internalAddress = mapEntry.InternalAddress;
			string externalAddress = mapEntry.ExternalAddress;
			if (!RoutingAddress.IsValidAddress(internalAddress) || !RoutingAddress.IsValidAddress(externalAddress))
			{
				throw new InvalidDataException(mapEntry.DistinguishedName);
			}
			RoutingAddress routingAddress = new RoutingAddress(internalAddress);
			RoutingAddress routingAddress2 = new RoutingAddress(externalAddress);
			string key = routingAddress.DomainPart + "," + routingAddress2.DomainPart;
			if (!this.mapTableList.ContainsKey(key))
			{
				MapTable value = new MapTable(routingAddress.DomainPart, routingAddress2.DomainPart);
				this.mapTableList.Add(key, value);
			}
			this.mapTableList[key].AddEntry(routingAddress.LocalPart, routingAddress2.LocalPart);
		}

		// Token: 0x06000065 RID: 101 RVA: 0x00004950 File Offset: 0x00002B50
		private void AddDomainMapping(AddressRewriteEntry entry)
		{
			string text = entry.InternalAddress;
			string externalAddress = entry.ExternalAddress;
			bool outboundOnly = entry.OutboundOnly;
			if (outboundOnly)
			{
				if (this.domainTableOutboundOnly == null)
				{
					this.domainTableOutboundOnly = new DomainTable();
				}
				bool flag = text.StartsWith("*.");
				if (flag)
				{
					text = text.Substring(2);
				}
				this.domainTableOutboundOnly.Add(text, externalAddress, entry.ExceptionList, flag);
				return;
			}
			if (this.domainTableBidirectional == null)
			{
				this.domainTableBidirectional = new DomainTable();
			}
			this.domainTableBidirectional.Add(text, externalAddress, null, false);
		}

		// Token: 0x06000066 RID: 102 RVA: 0x000049D8 File Offset: 0x00002BD8
		internal string RewriteOutbound(RoutingAddress address)
		{
			if (!this.AddressRemappable(address))
			{
				return null;
			}
			string text = this.DirectionalRewrite(address, MapTable.MapEntryType.Internal);
			if (!string.IsNullOrEmpty(text))
			{
				return text;
			}
			if (this.domainTableOutboundOnly != null)
			{
				ExTraceGlobals.AddressRewritingTracer.TraceDebug((long)this.GetHashCode(), "Checking against 1-way table for matches");
				string text2 = this.domainTableOutboundOnly.Remap(address.DomainPart, MapTable.MapEntryType.Internal);
				if (!string.IsNullOrEmpty(text2))
				{
					ExTraceGlobals.AddressRewritingTracer.TraceDebug((long)this.GetHashCode(), "Match found, address will be rewritten");
					return string.Format("{0}@{1}", address.LocalPart, text2);
				}
			}
			return null;
		}

		// Token: 0x06000067 RID: 103 RVA: 0x00004A67 File Offset: 0x00002C67
		internal string RewriteInbound(RoutingAddress address)
		{
			if (this.AddressRemappable(address))
			{
				return this.DirectionalRewrite(address, MapTable.MapEntryType.External);
			}
			return null;
		}

		// Token: 0x06000068 RID: 104 RVA: 0x00004A7C File Offset: 0x00002C7C
		private bool AddressRemappable(RoutingAddress address)
		{
			return address != RoutingAddress.NullReversePath && address.IsValid;
		}

		// Token: 0x06000069 RID: 105 RVA: 0x00004A94 File Offset: 0x00002C94
		private string DirectionalRewrite(RoutingAddress address, MapTable.MapEntryType entryType)
		{
			string domainPart = address.DomainPart;
			ExTraceGlobals.AddressRewritingTracer.TraceDebug((long)this.GetHashCode(), "Looking for maptables");
			if (this.addressMapTables != null)
			{
				foreach (MapTable mapTable in this.addressMapTables)
				{
					if (mapTable.IsCorrectMapTable(domainPart, entryType))
					{
						ExTraceGlobals.AddressRewritingTracer.TraceDebug((long)this.GetHashCode(), "Found a matching table");
						string text = mapTable.Remap(address.LocalPart, entryType);
						if (!string.IsNullOrEmpty(text))
						{
							MapTable.MapEntryType mapEntryType = (entryType == MapTable.MapEntryType.Internal) ? MapTable.MapEntryType.External : MapTable.MapEntryType.Internal;
							ExTraceGlobals.AddressRewritingTracer.TraceDebug((long)this.GetHashCode(), "Found a matching address-map to rewrite with");
							string arg = mapTable.Domain[(int)mapEntryType];
							return string.Format("{0}@{1}", text, arg);
						}
						ExTraceGlobals.AddressRewritingTracer.TraceDebug<string>((long)this.GetHashCode(), "Table did not have entry for: {0}", address.LocalPart);
					}
				}
			}
			ExTraceGlobals.AddressRewritingTracer.TraceDebug((long)this.GetHashCode(), "Looking for domain-maps for address");
			if (this.domainTableBidirectional != null)
			{
				string text2 = this.domainTableBidirectional.Remap(domainPart, entryType);
				if (!string.IsNullOrEmpty(text2))
				{
					ExTraceGlobals.AddressRewritingTracer.TraceDebug((long)this.GetHashCode(), "Found matching a domain-map to rewrite with");
					return string.Format("{0}@{1}", address.LocalPart, text2);
				}
			}
			ExTraceGlobals.AddressRewritingTracer.TraceDebug((long)this.GetHashCode(), "No suitable mappings found for address");
			return null;
		}

		// Token: 0x04000058 RID: 88
		private static Configuration currentConfig = null;

		// Token: 0x04000059 RID: 89
		private static ExEventLog logger = new ExEventLog(new Guid("7D2A0005-2C75-42ac-B495-8FE62F3B4FCF"), "MSExchange Messaging Policies");

		// Token: 0x0400005A RID: 90
		private static object lockVar = new object();

		// Token: 0x0400005B RID: 91
		private static TimeSpan reloadInterval = new TimeSpan(4, 0, 0);

		// Token: 0x0400005C RID: 92
		private static TimeSpan configLoadErrorRetryInterval = new TimeSpan(0, 30, 0);

		// Token: 0x0400005D RID: 93
		private MapTable[] addressMapTables;

		// Token: 0x0400005E RID: 94
		private Dictionary<string, MapTable> mapTableList = new Dictionary<string, MapTable>();

		// Token: 0x0400005F RID: 95
		private DomainTable domainTableBidirectional;

		// Token: 0x04000060 RID: 96
		private DomainTable domainTableOutboundOnly;

		// Token: 0x04000061 RID: 97
		private DateTime validUntil = DateTime.UtcNow + Configuration.reloadInterval;
	}
}
