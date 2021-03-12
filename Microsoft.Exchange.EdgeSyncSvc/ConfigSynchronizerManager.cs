using System;
using System.Collections.Generic;
using System.DirectoryServices;
using System.DirectoryServices.Protocols;
using System.Globalization;
using System.Security.AccessControl;
using System.Security.Principal;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Diagnostics.Components.EdgeSync;
using Microsoft.Exchange.EdgeSync.Common;
using Microsoft.Exchange.EdgeSync.Logging;
using Microsoft.Exchange.EdgeSync.Validation;

namespace Microsoft.Exchange.EdgeSync
{
	// Token: 0x02000011 RID: 17
	internal class ConfigSynchronizerManager : SynchronizerManager
	{
		// Token: 0x060000A4 RID: 164 RVA: 0x00007BD4 File Offset: 0x00005DD4
		public ConfigSynchronizerManager(EdgeServer edgeServer, SynchronizationProvider provider, List<TypeSynchronizer> typeSynchronizers, ITopologyConfigurationSession configSession, IDirectorySession sourceSession, SyncNowState syncNowState, EdgeSyncLogSession logSession) : base(edgeServer, provider, typeSynchronizers, configSession, sourceSession, syncNowState, provider.ConfigurationSyncInterval, logSession)
		{
			base.Type = SyncTreeType.Configuration;
			base.Status = new Status(edgeServer.Name, base.Type);
			base.PerfCounters = EdgeSynchronizerPerfCounters.GetInstance(string.Format("{0} {1}", edgeServer.Name, SyncTreeType.Configuration.ToString()));
		}

		// Token: 0x060000A5 RID: 165 RVA: 0x00007C44 File Offset: 0x00005E44
		public static FilterResult FilterLocalSiteServers(ExSearchResultEntry entry, Connection sourceConnection, TargetConnection targetConnection)
		{
			ExSearchResultEntry exSearchResultEntry = sourceConnection.ReadObjectEntry(entry.DistinguishedName, ConfigSynchronizerManager.RequiredAttributesForLocalSiteFiltering);
			if (exSearchResultEntry == null)
			{
				return FilterResult.Skip;
			}
			entry.Attributes["objectClass"] = exSearchResultEntry.Attributes["objectClass"];
			if (string.Equals(entry.ObjectClass, "msExchExchangeServer", StringComparison.OrdinalIgnoreCase))
			{
				DirectoryAttribute directoryAttribute = null;
				if (!exSearchResultEntry.Attributes.TryGetValue("msExchServerSite", out directoryAttribute) || directoryAttribute == null || directoryAttribute.Count <= 0)
				{
					throw new ExDirectoryException("msExchServerSite attribute is required but is missing from Server Object " + entry.DistinguishedName, null);
				}
				string text = directoryAttribute[0] as string;
				if (string.IsNullOrEmpty(text))
				{
					throw new ExDirectoryException("msExchServerSite attribute is required but its value is either null or empty string on Server Object " + entry.DistinguishedName, null);
				}
				if (!ADObjectId.IsValidDistinguishedName(text))
				{
					throw new ExDirectoryException("msExchServerSite attribute contains invalid DistinguishedName for Server Object " + entry.DistinguishedName, null);
				}
				ADObjectId id = new ADObjectId(text);
				if (EdgeSyncSvc.EdgeSync.Topology.LocalServer.ServerSite.Equals(id))
				{
					return FilterResult.None;
				}
			}
			return FilterResult.SkipAndRemoveFromTarget;
		}

		// Token: 0x060000A6 RID: 166 RVA: 0x00007D48 File Offset: 0x00005F48
		public static FilterResult FilterSendConnector(ExSearchResultEntry entry, Connection sourceConnection, TargetConnection targetConnection)
		{
			if (!entry.Attributes.ContainsKey("msExchSourceBridgeheadServersDN"))
			{
				entry = sourceConnection.ReadObjectEntry(entry.DistinguishedName, new string[]
				{
					"msExchSourceBridgeheadServersDN"
				});
				if (entry == null)
				{
					return FilterResult.Skip;
				}
			}
			string text = null;
			foreach (Server server in EdgeSyncSvc.EdgeSync.Topology.SiteEdgeServers.Values)
			{
				if (((LdapTargetConnection)targetConnection).ServerName.Equals(server.Name, StringComparison.OrdinalIgnoreCase))
				{
					text = server.DistinguishedName;
					break;
				}
			}
			if (text == null)
			{
				return FilterResult.SkipAndRemoveFromTarget;
			}
			if (entry.Attributes.ContainsKey("msExchSourceBridgeheadServersDN"))
			{
				DirectoryAttribute directoryAttribute = entry.Attributes["msExchSourceBridgeheadServersDN"];
				for (int i = 0; i < directoryAttribute.Count; i++)
				{
					string child = (string)directoryAttribute[i];
					if (DistinguishedName.IsChildOf(child, text))
					{
						return FilterResult.None;
					}
				}
				return FilterResult.SkipAndRemoveFromTarget;
			}
			return FilterResult.SkipAndRemoveFromTarget;
		}

		// Token: 0x060000A7 RID: 167 RVA: 0x00007E58 File Offset: 0x00006058
		public static bool FilterSendConnectorNewerAttributes(ExSearchResultEntry entry, Connection sourceConnection, TargetConnection targetConnection, object state)
		{
			SynchronizerManager synchronizerManager = (SynchronizerManager)state;
			TargetEdgeTransportServerConfig targetEdgeTransportServerConfig = synchronizerManager.EdgeServer.Config as TargetEdgeTransportServerConfig;
			ServerVersion a = new ServerVersion(targetEdgeTransportServerConfig.VersionNumber);
			if (ServerVersion.Compare(a, Schema.SendConnector.ServerVersion14_1_144) < 0)
			{
				foreach (string key in Schema.SendConnector.NewAttributesInServerVersion14_1_144)
				{
					entry.Attributes.Remove(key);
				}
			}
			if (ServerVersion.Compare(a, Schema.SendConnector.ServerVersion15_0_620) < 0)
			{
				foreach (string key2 in Schema.SendConnector.NewAttributesInServerVersion15_0_620)
				{
					entry.Attributes.Remove(key2);
				}
			}
			return true;
		}

		// Token: 0x060000A8 RID: 168 RVA: 0x00007F04 File Offset: 0x00006104
		public static void TransformSecurityDescriptor(ExSearchResultEntry entry, Connection sourceConnection, TargetConnection targetConnection, object state)
		{
			if (!entry.Attributes.ContainsKey("nTSecurityDescriptor"))
			{
				ExSearchResultEntry exSearchResultEntry = sourceConnection.ReadObjectEntry(entry.DistinguishedName, new string[]
				{
					"nTSecurityDescriptor"
				});
				if (exSearchResultEntry == null)
				{
					return;
				}
				if (!exSearchResultEntry.Attributes.ContainsKey("nTSecurityDescriptor"))
				{
					return;
				}
				entry.Attributes.Add("nTSecurityDescriptor", exSearchResultEntry.Attributes["nTSecurityDescriptor"]);
			}
			string targetPath = ((LdapTargetConnection)targetConnection).GetTargetPath(entry);
			DirectoryAttribute directoryAttribute = entry.Attributes["nTSecurityDescriptor"];
			if (directoryAttribute == null || directoryAttribute.Count <= 0)
			{
				ExTraceGlobals.SynchronizationJobTracer.TraceError<string>(0L, "Security Descriptor is deleted for {0}", entry.DistinguishedName);
				return;
			}
			byte[] binaryForm = (byte[])directoryAttribute[0];
			ActiveDirectorySecurity activeDirectorySecurity = new ActiveDirectorySecurity();
			activeDirectorySecurity.SetSecurityDescriptorBinaryForm(binaryForm, AccessControlSections.Access);
			ActiveDirectorySecurity activeDirectorySecurity2 = ((LdapTargetConnection)targetConnection).ReadSecurityDescriptorDacl(targetPath);
			if (activeDirectorySecurity2 != null)
			{
				ConfigSynchronizerManager.MergeAclsForReplication(activeDirectorySecurity2, activeDirectorySecurity);
				((LdapTargetConnection)targetConnection).WriteSecurityDescriptorDacl(targetPath, activeDirectorySecurity2);
				return;
			}
			if (!((LdapTargetConnection)targetConnection).Exists(targetPath))
			{
				throw new InvalidOperationException(string.Concat(new string[]
				{
					"Unable to replicate SD from source object ",
					entry.DistinguishedName,
					" because target object: ",
					targetPath,
					" doesn't exists in ADAM/Edge server ",
					((LdapTargetConnection)targetConnection).ServerName
				}));
			}
			throw new InvalidOperationException("Unable to replicate SD from source object " + entry.DistinguishedName + " because we could not read SD from target object: " + targetPath);
		}

		// Token: 0x060000A9 RID: 169 RVA: 0x0000807C File Offset: 0x0000627C
		public static bool SetApplicableTransportSettingFlags(ExSearchResultEntry entry, Connection sourceConnection, TargetConnection targetConnection, object state)
		{
			if (entry.IsDeleted)
			{
				return true;
			}
			DirectoryAttribute attribute;
			if (!entry.Attributes.TryGetValue(ADAMTransportConfigContainerSchema.Flags.LdapDisplayName, out attribute))
			{
				return true;
			}
			int num = 0;
			try
			{
				num = TransportConfigValidator.ParseTransportFlagsFromDirectoryAttribute(attribute);
			}
			catch (ExDirectoryException arg)
			{
				ExTraceGlobals.SynchronizationJobTracer.TraceError<ExDirectoryException>(0L, "SetApplicableTransportSettingFlags: Unable to parse TransportConfigContainerSchema.Flags attribute because of {0}, skip syncing this object", arg);
				return false;
			}
			LdapTargetConnection ldapTargetConnection = (LdapTargetConnection)targetConnection;
			string targetPath = ldapTargetConnection.GetTargetPath(entry);
			ExSearchResultEntry exSearchResultEntry = ldapTargetConnection.ReadObjectEntry(targetPath, new string[]
			{
				ADAMTransportConfigContainerSchema.Flags.LdapDisplayName
			});
			DirectoryAttribute attribute2;
			int num2;
			if (exSearchResultEntry == null || !exSearchResultEntry.Attributes.TryGetValue(ADAMTransportConfigContainerSchema.Flags.LdapDisplayName, out attribute2))
			{
				num2 = (num & TransportConfigValidator.TransportSettingsSyncedFlags);
			}
			else
			{
				num2 = TransportConfigValidator.ParseTransportFlagsFromDirectoryAttribute(attribute2);
				int num3 = num & TransportConfigValidator.TransportSettingsSyncedFlags;
				int num4 = num | ~TransportConfigValidator.TransportSettingsSyncedFlags;
				num2 |= num3;
				num2 &= num4;
			}
			entry.Attributes[ADAMTransportConfigContainerSchema.Flags.LdapDisplayName] = new DirectoryAttribute(ADAMTransportConfigContainerSchema.Flags.LdapDisplayName, num2.ToString(CultureInfo.InvariantCulture));
			return true;
		}

		// Token: 0x060000AA RID: 170 RVA: 0x00008248 File Offset: 0x00006448
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
				string distinguishedName = this.ConfigSession.ConfigurationNamingContext.DistinguishedName;
				if (!savedCookies.ContainsKey(distinguishedName) || this.IsCookieExpired(savedCookies[distinguishedName]))
				{
					Cookie cookie = new Cookie(distinguishedName);
					newCookies.Add(distinguishedName, cookie);
					ExTraceGlobals.SynchronizationJobTracer.TraceDebug<string, string, DateTime>((long)this.GetHashCode(), "Load cookie for config {0}. DC {1}; LastUpdate {2}", distinguishedName, cookie.DomainController, cookie.LastUpdated);
					return;
				}
				newCookies[distinguishedName] = savedCookies[distinguishedName];
			}, 3);
			if (!adoperationResult.Succeeded)
			{
				ExTraceGlobals.SynchronizationJobTracer.TraceError<Exception>((long)this.GetHashCode(), "Failed to load cookie info for domains with {0}", adoperationResult.Exception);
				return false;
			}
			base.Cookies = newCookies;
			return true;
		}

		// Token: 0x060000AB RID: 171 RVA: 0x00008338 File Offset: 0x00006538
		private static void MergeAclsForReplication(ActiveDirectorySecurity targetAcl, ActiveDirectorySecurity sourceAcl)
		{
			List<ActiveDirectoryAccessRule> list = new List<ActiveDirectoryAccessRule>();
			foreach (object obj in targetAcl.GetAccessRules(true, false, typeof(SecurityIdentifier)))
			{
				ActiveDirectoryAccessRule item = (ActiveDirectoryAccessRule)obj;
				list.Add(item);
			}
			foreach (ActiveDirectoryAccessRule rule in list)
			{
				targetAcl.RemoveAccessRuleSpecific(rule);
			}
			foreach (object obj2 in sourceAcl.GetAccessRules(true, false, typeof(SecurityIdentifier)))
			{
				ActiveDirectoryAccessRule rule2 = (ActiveDirectoryAccessRule)obj2;
				targetAcl.AddAccessRule(rule2);
			}
		}

		// Token: 0x04000069 RID: 105
		private static readonly string[] RequiredAttributesForLocalSiteFiltering = new string[]
		{
			"objectClass",
			"msExchServerSite"
		};
	}
}
