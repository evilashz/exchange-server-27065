using System;
using System.Collections.Generic;
using System.DirectoryServices.Protocols;
using System.Globalization;
using Microsoft.Exchange.Collections;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Diagnostics.Components.EdgeSync;
using Microsoft.Exchange.EdgeSync.Common;
using Microsoft.Exchange.EdgeSync.Logging;
using Microsoft.Exchange.Transport.RecipientAPI;

namespace Microsoft.Exchange.EdgeSync
{
	// Token: 0x02000010 RID: 16
	internal class RecipientSynchronizerManager : SynchronizerManager
	{
		// Token: 0x0600009F RID: 159 RVA: 0x00007684 File Offset: 0x00005884
		public RecipientSynchronizerManager(EdgeServer edgeServer, SynchronizationProvider provider, List<TypeSynchronizer> typeSynchronizers, ITopologyConfigurationSession configSession, IDirectorySession sourceSession, SyncNowState syncNowState, EdgeSyncLogSession logSession) : base(edgeServer, provider, typeSynchronizers, configSession, sourceSession, syncNowState, provider.RecipientSyncInterval, logSession)
		{
			base.Type = SyncTreeType.Recipients;
			base.Status = new Status(edgeServer.Name, base.Type);
			base.PerfCounters = EdgeSynchronizerPerfCounters.GetInstance(string.Format("{0} {1}", edgeServer.Name, SyncTreeType.Recipients.ToString()));
		}

		// Token: 0x060000A0 RID: 160 RVA: 0x000076FC File Offset: 0x000058FC
		public static FilterResult FilterRecipientOnAdd(ExSearchResultEntry entry, Connection sourceConnection, TargetConnection targetConnection)
		{
			if (entry.IsNew)
			{
				foreach (string text in RecipientSchema.AttributeNames)
				{
					if (!text.Equals("objectClass", StringComparison.OrdinalIgnoreCase) && !text.Equals(ADObjectSchema.ExchangeVersion.LdapDisplayName, StringComparison.OrdinalIgnoreCase) && entry.Attributes.ContainsKey(text) && entry.Attributes[text].Count != 0)
					{
						return FilterResult.None;
					}
				}
				return FilterResult.Skip;
			}
			return FilterResult.None;
		}

		// Token: 0x060000A1 RID: 161 RVA: 0x00007774 File Offset: 0x00005974
		public static bool HashRecipientProxyAddressesAndAdjustVersion(ExSearchResultEntry entry, Connection connection, TargetConnection targetConnection, object state)
		{
			if (entry.IsDeleted)
			{
				return true;
			}
			RecipientSynchronizerManager recipientSynchronizerManager = (RecipientSynchronizerManager)state;
			string[] array = new string[]
			{
				"proxyAddresses",
				"msExchSignupAddresses"
			};
			foreach (string key in array)
			{
				if (entry.Attributes.ContainsKey(key))
				{
					DirectoryAttribute directoryAttribute = entry.Attributes[key];
					HashSet<string> hashSet = new HashSet<string>(directoryAttribute.Count);
					for (int j = 0; j < directoryAttribute.Count; j++)
					{
						string text = directoryAttribute[j] as string;
						if (text.StartsWith("smtp:", StringComparison.OrdinalIgnoreCase))
						{
							string emailAddress = text.Substring(5);
							string hashedFormWithPrefix = recipientSynchronizerManager.GetHashedFormWithPrefix(emailAddress);
							hashSet.Add(hashedFormWithPrefix);
						}
					}
					DirectoryAttribute value = new DirectoryAttribute(directoryAttribute.Name, hashSet.ToArray());
					entry.Attributes[key] = value;
				}
			}
			DirectoryAttribute directoryAttribute2 = null;
			if (entry.Attributes.TryGetValue("msExchVersion", out directoryAttribute2) && directoryAttribute2.Count > 0)
			{
				TargetEdgeTransportServerConfig targetEdgeTransportServerConfig = recipientSynchronizerManager.EdgeServer.Config as TargetEdgeTransportServerConfig;
				if (targetEdgeTransportServerConfig == null)
				{
					ExTraceGlobals.SynchronizationJobTracer.TraceError<string>(0L, "Target server config {0} should be of type TargetEdgeTransportServerConfig", recipientSynchronizerManager.EdgeServer.Name);
					throw new InvalidOperationException("Target server config should be of type TargetEdgeTransportServerConfig");
				}
				long encodedVersion = 0L;
				if (!long.TryParse((string)directoryAttribute2[0], NumberStyles.Any, NumberFormatInfo.InvariantInfo, out encodedVersion))
				{
					ExTraceGlobals.SynchronizationJobTracer.TraceError<string, string>(0L, "Skipping replicating msExchVersion for {0} because its source version {1} is not valid", entry.DistinguishedName, (string)directoryAttribute2[0]);
					return true;
				}
				ExchangeObjectVersion exchangeObjectVersion = new ExchangeObjectVersion(encodedVersion);
				int num = targetEdgeTransportServerConfig.VersionNumber >> 22 & 63;
				ExchangeObjectVersion exchangeObjectVersion2 = ADRecipient.PublicFolderMailboxObjectVersion;
				if (num == Server.Exchange2007MajorVersion)
				{
					exchangeObjectVersion2 = ExchangeObjectVersion.Exchange2007;
				}
				else if (num == Server.Exchange2009MajorVersion)
				{
					exchangeObjectVersion2 = ADRecipient.ArbitrationMailboxObjectVersion;
				}
				ExchangeObjectVersion exchangeObjectVersion3;
				if (exchangeObjectVersion.IsOlderThan(exchangeObjectVersion2))
				{
					exchangeObjectVersion3 = exchangeObjectVersion;
				}
				else
				{
					exchangeObjectVersion3 = exchangeObjectVersion2;
				}
				ExTraceGlobals.SynchronizationJobTracer.TraceDebug(0L, "Recipient {0} for target {1} original msExchVersion is {2}, final replicated version is {3}", new object[]
				{
					entry.DistinguishedName,
					recipientSynchronizerManager.EdgeServer.Name,
					exchangeObjectVersion,
					exchangeObjectVersion3
				});
				entry.Attributes["msExchVersion"] = new DirectoryAttribute("msExchVersion", exchangeObjectVersion3.ToInt64().ToString(NumberFormatInfo.InvariantInfo));
			}
			return true;
		}

		// Token: 0x060000A2 RID: 162 RVA: 0x000079E5 File Offset: 0x00005BE5
		public string GetHashedFormWithPrefix(string emailAddress)
		{
			return this.proxyAddressHasher.GetHashedFormWithPrefix(emailAddress);
		}

		// Token: 0x060000A3 RID: 163 RVA: 0x00007AE4 File Offset: 0x00005CE4
		protected override bool TryInitializeCookie()
		{
			Dictionary<string, Cookie> savedCookies = null;
			Dictionary<string, Cookie> newCookies = new Dictionary<string, Cookie>();
			if (!base.ForceFullSync)
			{
				lock (base.EdgeServer)
				{
					if (!base.TargetConnection.TryReadCookie(out savedCookies))
					{
						ExTraceGlobals.SynchronizationJobTracer.TraceError<string>((long)this.GetHashCode(), "Failed to read cookie record for {0}", base.EdgeServer.Host);
						return false;
					}
					goto IL_89;
				}
			}
			savedCookies = new Dictionary<string, Cookie>();
			IL_89:
			ADOperationResult adoperationResult = ADNotificationAdapter.TryRunADOperation(delegate()
			{
				ADForest localForest = ADForest.GetLocalForest(this.ConfigSession.DomainController);
				ADCrossRef[] domainPartitions = localForest.GetDomainPartitions();
				foreach (ADCrossRef adcrossRef in domainPartitions)
				{
					string distinguishedName = adcrossRef.NCName.DistinguishedName;
					if (!savedCookies.ContainsKey(distinguishedName) || this.IsCookieExpired(savedCookies[distinguishedName]))
					{
						Cookie cookie = new Cookie(distinguishedName);
						newCookies.Add(cookie.BaseDN, cookie);
						ExTraceGlobals.SynchronizationJobTracer.TraceDebug<string, string, DateTime>((long)this.GetHashCode(), "Load cookie for domain {0}. DC {1}; LastUpdate {2}", cookie.BaseDN, cookie.DomainController, cookie.LastUpdated);
					}
					else
					{
						newCookies[distinguishedName] = savedCookies[distinguishedName];
					}
				}
			}, 3);
			if (!adoperationResult.Succeeded)
			{
				ExTraceGlobals.SynchronizationJobTracer.TraceError<Exception>((long)this.GetHashCode(), "Failed to load cookie info for domains with {0}", adoperationResult.Exception);
				return false;
			}
			base.Cookies = newCookies;
			return true;
		}

		// Token: 0x04000068 RID: 104
		private readonly ProxyAddressHasher proxyAddressHasher = new ProxyAddressHasher();
	}
}
