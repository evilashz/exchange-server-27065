using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Runtime.CompilerServices;
using System.Security.Principal;
using Microsoft.Exchange.Collections;
using Microsoft.Exchange.Data.Directory.Diagnostics;
using Microsoft.Exchange.Data.Directory.EventLog;
using Microsoft.Exchange.Data.Directory.Sync;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Data.Directory;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x02000399 RID: 921
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[Serializable]
	internal class ADTopologyConfigurationSession : ADConfigurationSession, ITopologyConfigurationSession, IConfigurationSession, IDirectorySession, IConfigDataProvider
	{
		// Token: 0x06002A4B RID: 10827 RVA: 0x000B009F File Offset: 0x000AE29F
		private static QueryFilter FqdnFilterForServer(string serverFqdn)
		{
			return new ComparisonFilter(ComparisonOperator.Equal, ServerSchema.NetworkAddress, "ncacn_ip_tcp:" + serverFqdn);
		}

		// Token: 0x06002A4C RID: 10828 RVA: 0x000B00B7 File Offset: 0x000AE2B7
		public ADTopologyConfigurationSession(ConsistencyMode consistencyMode, ADSessionSettings sessionSettings) : base(true, true, consistencyMode, null, sessionSettings)
		{
		}

		// Token: 0x06002A4D RID: 10829 RVA: 0x000B00C4 File Offset: 0x000AE2C4
		public ADTopologyConfigurationSession(bool readOnly, ConsistencyMode consistencyMode, ADSessionSettings sessionSettings) : base(true, readOnly, consistencyMode, null, sessionSettings)
		{
		}

		// Token: 0x06002A4E RID: 10830 RVA: 0x000B00D1 File Offset: 0x000AE2D1
		public ADTopologyConfigurationSession(string domainController, bool readOnly, ConsistencyMode consistencyMode, NetworkCredential networkCredential, ADSessionSettings sessionSettings) : base(true, readOnly, consistencyMode, networkCredential, sessionSettings)
		{
			base.DomainController = domainController;
		}

		// Token: 0x06002A4F RID: 10831 RVA: 0x000B00E7 File Offset: 0x000AE2E7
		public ADTopologyConfigurationSession(string domainController, bool readOnly, ConsistencyMode consistencyMode, NetworkCredential networkCredential, ADSessionSettings sessionSettings, ConfigScopes configScope) : this(domainController, readOnly, consistencyMode, networkCredential, sessionSettings)
		{
			if (ConfigScopes.TenantSubTree != configScope)
			{
				throw new NotSupportedException("Only ConfigScopes.TenantSubTree is supported by this constructor");
			}
			if (ConfigScopes.TenantSubTree == configScope)
			{
				base.ConfigScope = configScope;
			}
		}

		// Token: 0x06002A50 RID: 10832 RVA: 0x000B0114 File Offset: 0x000AE314
		private static bool IsValidTrustedHoster(string item)
		{
			if (item.StartsWith("*.", StringComparison.OrdinalIgnoreCase))
			{
				string domain = item.Substring(2);
				if (SmtpAddress.IsValidDomain(domain))
				{
					return true;
				}
			}
			else if (SmtpAddress.IsValidDomain(item))
			{
				return true;
			}
			return false;
		}

		// Token: 0x06002A51 RID: 10833 RVA: 0x000B014C File Offset: 0x000AE34C
		private static void ReadMultipleADLegacyObjectsHashInserter<TResult>(Hashtable hash, TResult entry) where TResult : ADLegacyVersionableObject
		{
			string key = ((string)entry.propertyBag[ADObjectSchema.Name]).ToLowerInvariant();
			if (!hash.ContainsKey(key))
			{
				hash[key] = new Result<TResult>(entry, null);
			}
			key = ((string)entry.propertyBag[ADObjectSchema.DistinguishedName]).ToLowerInvariant();
			if (!hash.ContainsKey(key))
			{
				hash[key] = new Result<TResult>(entry, null);
			}
		}

		// Token: 0x06002A52 RID: 10834 RVA: 0x000B01D4 File Offset: 0x000AE3D4
		private static Result<T> ReadMultipleADLegacyObjectsHashLookup<T>(Hashtable hash, string key) where T : ADLegacyVersionableObject
		{
			if (!string.IsNullOrEmpty(key))
			{
				object obj = hash[key.ToLowerInvariant()];
				if (obj != null)
				{
					return (Result<T>)obj;
				}
			}
			return new Result<T>(default(T), ProviderError.NotFound);
		}

		// Token: 0x06002A53 RID: 10835 RVA: 0x000B0218 File Offset: 0x000AE418
		private static QueryFilter ReadMultipleADLegacyObjectsQueryFilterFromObjectName(string nameOrDN)
		{
			if (nameOrDN == null)
			{
				throw new ArgumentNullException("nameOrDN");
			}
			return new OrFilter(new QueryFilter[]
			{
				new ComparisonFilter(ComparisonOperator.Equal, ADObjectSchema.DistinguishedName, nameOrDN),
				new ComparisonFilter(ComparisonOperator.Equal, ADObjectSchema.Name, nameOrDN)
			});
		}

		// Token: 0x06002A54 RID: 10836 RVA: 0x000B0298 File Offset: 0x000AE498
		public ADCrossRef[] FindADCrossRefByDomainId(ADObjectId domainNc)
		{
			return this.InvokeWithAPILogging<ADCrossRef[]>(delegate
			{
				QueryFilter filter = new ComparisonFilter(ComparisonOperator.Equal, ADCrossRefSchema.NCName, domainNc);
				return this.InternalFind<ADCrossRef>(null, QueryScope.SubTree, filter, null, 0, null);
			}, "FindADCrossRefByDomainId");
		}

		// Token: 0x06002A55 RID: 10837 RVA: 0x000B0308 File Offset: 0x000AE508
		public ADCrossRef[] FindADCrossRefByNetBiosName(string domain)
		{
			return this.InvokeWithAPILogging<ADCrossRef[]>(delegate
			{
				QueryFilter filter = new ComparisonFilter(ComparisonOperator.Equal, ADCrossRefSchema.NetBiosName, domain);
				return this.InternalFind<ADCrossRef>(null, QueryScope.SubTree, filter, null, 0, null);
			}, "FindADCrossRefByNetBiosName");
		}

		// Token: 0x06002A56 RID: 10838 RVA: 0x000B036D File Offset: 0x000AE56D
		public AccountPartition[] FindAllAccountPartitions()
		{
			return this.InvokeWithAPILogging<AccountPartition[]>(delegate
			{
				ADPagedReader<AccountPartition> adpagedReader = base.FindPaged<AccountPartition>(ADSystemConfigurationSession.GetRootOrgContainerIdForLocalForest().GetChildId(AccountPartition.AccountForestContainerName), QueryScope.OneLevel, null, null, 0);
				return adpagedReader.ReadAllPages();
			}, "FindAllAccountPartitions");
		}

		// Token: 0x06002A57 RID: 10839 RVA: 0x000B03AC File Offset: 0x000AE5AC
		public ADSite[] FindAllADSites()
		{
			return this.InvokeWithAPILogging<ADSite[]>(delegate
			{
				ADPagedReader<ADSite> adpagedReader = base.FindPaged<ADSite>(base.ConfigurationNamingContext, QueryScope.SubTree, null, null, 0);
				return adpagedReader.ReadAllPages();
			}, "FindAllADSites");
		}

		// Token: 0x06002A58 RID: 10840 RVA: 0x000B04AD File Offset: 0x000AE6AD
		public IList<PublicFolderDatabase> FindAllPublicFolderDatabaseOfCurrentVersion()
		{
			return this.InvokeWithAPILogging<IList<PublicFolderDatabase>>(delegate
			{
				PublicFolderDatabase[] array = base.Find<PublicFolderDatabase>(base.GetOrgContainerId(), QueryScope.SubTree, null, null, 10000);
				IList<PublicFolderDatabase> list = new List<PublicFolderDatabase>(array.Length);
				if (array.Length != 0)
				{
					QueryFilter[] array2 = new QueryFilter[array.Length];
					for (int i = 0; i < array.Length; i++)
					{
						array2[i] = new ComparisonFilter(ComparisonOperator.Equal, ADObjectSchema.Id, array[i].Server);
					}
					QueryFilter filter = new OrFilter(array2);
					Server[] array3 = base.Find<Server>(base.GetOrgContainerId(), QueryScope.SubTree, filter, null, 10000);
					foreach (Server server in array3)
					{
						if (server.MajorVersion == 15)
						{
							foreach (PublicFolderDatabase publicFolderDatabase in array)
							{
								if (server.Id.Equals(publicFolderDatabase.Server))
								{
									list.Add(publicFolderDatabase);
									break;
								}
							}
						}
					}
				}
				return list;
			}, "FindAllPublicFolderDatabaseOfCurrentVersion");
		}

		// Token: 0x06002A59 RID: 10841 RVA: 0x000B0504 File Offset: 0x000AE704
		public ADPagedReader<Server> FindAllServersWithExactVersionNumber(int versionNumber)
		{
			return this.InvokeWithAPILogging<ADPagedReader<Server>>(delegate
			{
				QueryFilter filter = new ComparisonFilter(ComparisonOperator.Equal, ServerSchema.VersionNumber, versionNumber);
				return this.FindPaged<Server>(null, QueryScope.SubTree, filter, null, 0);
			}, "FindAllServersWithExactVersionNumber");
		}

		// Token: 0x06002A5A RID: 10842 RVA: 0x000B0578 File Offset: 0x000AE778
		public ADPagedReader<Server> FindAllServersWithVersionNumber(int versionNumber)
		{
			return this.InvokeWithAPILogging<ADPagedReader<Server>>(delegate
			{
				QueryFilter filter = new ComparisonFilter(ComparisonOperator.GreaterThanOrEqual, ServerSchema.VersionNumber, versionNumber);
				return this.FindPaged<Server>(null, QueryScope.SubTree, filter, null, 0);
			}, "FindAllServersWithVersionNumber");
		}

		// Token: 0x06002A5B RID: 10843 RVA: 0x000B0610 File Offset: 0x000AE810
		public ADPagedReader<MiniServer> FindAllServersWithExactVersionNumber(int versionNumber, QueryFilter additionalFilter, IEnumerable<PropertyDefinition> properties)
		{
			return this.InvokeWithAPILogging<ADPagedReader<MiniServer>>(delegate
			{
				QueryFilter queryFilter = new ComparisonFilter(ComparisonOperator.Equal, ServerSchema.VersionNumber, versionNumber);
				QueryFilter filter = new AndFilter(new QueryFilter[]
				{
					queryFilter,
					additionalFilter
				});
				return this.FindPaged<MiniServer>(null, QueryScope.SubTree, filter, null, 0, properties);
			}, "FindAllServersWithExactVersionNumber");
		}

		// Token: 0x06002A5C RID: 10844 RVA: 0x000B06B8 File Offset: 0x000AE8B8
		public ADPagedReader<MiniServer> FindAllServersWithVersionNumber(int versionNumber, QueryFilter additionalFilter, IEnumerable<PropertyDefinition> properties)
		{
			return this.InvokeWithAPILogging<ADPagedReader<MiniServer>>(delegate
			{
				QueryFilter queryFilter = new ComparisonFilter(ComparisonOperator.GreaterThanOrEqual, ServerSchema.VersionNumber, versionNumber);
				QueryFilter filter = new AndFilter(new QueryFilter[]
				{
					queryFilter,
					additionalFilter
				});
				return this.FindPaged<MiniServer>(null, QueryScope.SubTree, filter, null, 0, properties);
			}, "FindAllServersWithVersionNumber");
		}

		// Token: 0x06002A5D RID: 10845 RVA: 0x000B0798 File Offset: 0x000AE998
		public CmdletExtensionAgent[] FindCmdletExtensionAgents(bool enabledOnly, bool sortByPriority)
		{
			return this.InvokeWithAPILogging<CmdletExtensionAgent[]>(delegate
			{
				QueryFilter filter = null;
				if (enabledOnly)
				{
					filter = new BitMaskAndFilter(CmdletExtensionAgentSchema.CmdletExtensionFlags, 1UL);
				}
				ADPagedReader<CmdletExtensionAgent> adpagedReader = this.FindPaged<CmdletExtensionAgent>(null, QueryScope.SubTree, filter, null, 0);
				List<CmdletExtensionAgent> list = new List<CmdletExtensionAgent>();
				foreach (CmdletExtensionAgent item in adpagedReader)
				{
					list.Add(item);
				}
				if (sortByPriority)
				{
					list.Sort();
				}
				return list.ToArray();
			}, "FindCmdletExtensionAgents");
		}

		// Token: 0x06002A5E RID: 10846 RVA: 0x000B07D7 File Offset: 0x000AE9D7
		public ADComputer FindComputerByHostName(string hostName)
		{
			return this.FindComputerByHostName(null, hostName);
		}

		// Token: 0x06002A5F RID: 10847 RVA: 0x000B0884 File Offset: 0x000AEA84
		public ADComputer FindComputerByHostName(ADObjectId domainId, string hostName)
		{
			return this.InvokeWithAPILogging<ADComputer>(delegate
			{
				QueryFilter filter = new AndFilter(new QueryFilter[]
				{
					new ComparisonFilter(ComparisonOperator.Equal, ADComputerSchema.ServicePrincipalName, "HOST/" + hostName),
					new NotFilter(new BitMaskOrFilter(ADComputerSchema.UserAccountControl, 2UL))
				});
				ADComputer[] array = this.Find<ADComputer>(domainId, QueryScope.SubTree, filter, null, 2);
				if (array == null || array.Length <= 0)
				{
					return null;
				}
				if (array.Length > 1)
				{
					throw new ADOperationException(DirectoryStrings.HostNameMatchesMultipleComputers(hostName, array[0].DistinguishedName, array[1].DistinguishedName));
				}
				return array[0];
			}, "FindComputerByHostName");
		}

		// Token: 0x06002A60 RID: 10848 RVA: 0x000B097C File Offset: 0x000AEB7C
		public ADComputer FindComputerBySid(SecurityIdentifier sid)
		{
			return this.InvokeWithAPILogging<ADComputer>(delegate
			{
				if (sid.IsWellKnown(WellKnownSidType.LocalSystemSid) || sid.IsWellKnown(WellKnownSidType.NetworkServiceSid))
				{
					ExTraceGlobals.ADTopologyTracer.TraceDebug<SecurityIdentifier>((long)this.GetHashCode(), "The Sid {0} is LocalSystemSid or NetworkServiceSid, finding local computer without using Sid", sid);
					return this.FindLocalComputer();
				}
				QueryFilter filter = new ComparisonFilter(ComparisonOperator.Equal, ADComputerSchema.Sid, sid);
				ADComputer[] array = this.Find<ADComputer>(null, QueryScope.SubTree, filter, null, 2);
				if (array == null)
				{
					return null;
				}
				switch (array.Length)
				{
				case 0:
					return null;
				case 1:
					return array[0];
				default:
					throw new ADOperationException(DirectoryStrings.ErrorNonUniqueSid(sid.ToString()));
				}
			}, "FindComputerBySid");
		}

		// Token: 0x06002A61 RID: 10849 RVA: 0x000B0A28 File Offset: 0x000AEC28
		public TDatabase FindDatabaseByGuid<TDatabase>(Guid dbGuid) where TDatabase : Database, new()
		{
			return this.InvokeWithAPILogging<TDatabase>(delegate
			{
				if (dbGuid == Guid.Empty)
				{
					throw new ArgumentException("dbGuid cannot be Empty.");
				}
				QueryFilter filter = new ComparisonFilter(ComparisonOperator.Equal, ADObjectSchema.Guid, dbGuid);
				TDatabase[] array = this.Find<TDatabase>(null, QueryScope.SubTree, filter, null, 1);
				if (array == null || array.Length <= 0)
				{
					return default(TDatabase);
				}
				return array[0];
			}, "FindDatabaseByGuid");
		}

		// Token: 0x06002A62 RID: 10850 RVA: 0x000B0A9C File Offset: 0x000AEC9C
		public TResult[] Find<TResult>(ADObjectId rootId, QueryScope scope, QueryFilter filter, SortBy sortBy, int maxResults, IEnumerable<PropertyDefinition> properties) where TResult : ADObject, new()
		{
			return this.InvokeWithAPILogging<TResult[]>(() => this.InternalFind<TResult>(rootId, scope, filter, sortBy, maxResults, properties), "Find");
		}

		// Token: 0x06002A63 RID: 10851 RVA: 0x000B0B6C File Offset: 0x000AED6C
		public ADServer FindDCByFqdn(string dnsHostName)
		{
			return this.InvokeWithAPILogging<ADServer>(delegate
			{
				if (string.IsNullOrEmpty(dnsHostName))
				{
					throw new ArgumentNullException("dnsHostName");
				}
				QueryFilter filter = new ComparisonFilter(ComparisonOperator.Equal, ADServerSchema.DnsHostName, dnsHostName);
				ADServer[] array = this.Find<ADServer>(this.GetSitesContainer().Id, QueryScope.SubTree, filter, null, 1);
				if (array == null || array.Length <= 0)
				{
					return null;
				}
				return array[0];
			}, "FindDCByFqdn");
		}

		// Token: 0x06002A64 RID: 10852 RVA: 0x000B0C04 File Offset: 0x000AEE04
		public ADServer FindDCByInvocationId(Guid invocationId)
		{
			return this.InvokeWithAPILogging<ADServer>(delegate
			{
				QueryFilter filter = new ComparisonFilter(ComparisonOperator.Equal, NtdsDsaSchema.InvocationId, invocationId);
				NtdsDsa[] array = this.Find<NtdsDsa>(null, QueryScope.SubTree, filter, null, 1);
				if (array != null && array.Length > 0)
				{
					return this.Read<ADServer>(array[0].Id.Parent);
				}
				return null;
			}, "FindDCByInvocationId");
		}

		// Token: 0x06002A65 RID: 10853 RVA: 0x000B0CD8 File Offset: 0x000AEED8
		public UMDialPlan[] FindDialPlansForServer(Server server)
		{
			return this.InvokeWithAPILogging<UMDialPlan[]>(delegate
			{
				if (server == null)
				{
					throw new ArgumentNullException("server");
				}
				QueryFilter filter = new ComparisonFilter(ComparisonOperator.Equal, UMDialPlanSchema.UMServers, server.Id);
				ADPagedReader<UMDialPlan> adpagedReader = this.FindPaged<UMDialPlan>(null, QueryScope.SubTree, filter, null, 0);
				List<UMDialPlan> list = new List<UMDialPlan>();
				foreach (UMDialPlan item in adpagedReader)
				{
					list.Add(item);
				}
				return list.ToArray();
			}, "FindDialPlansForServer");
		}

		// Token: 0x06002A66 RID: 10854 RVA: 0x000B0D58 File Offset: 0x000AEF58
		public ELCFolder FindElcFolderByName(string name)
		{
			return this.InvokeWithAPILogging<ELCFolder>(delegate
			{
				QueryFilter filter = new ComparisonFilter(ComparisonOperator.Equal, ADObjectSchema.Name, name);
				ELCFolder[] array = this.Find<ELCFolder>(null, QueryScope.SubTree, filter, null, 2);
				if (array == null || array.Length <= 0)
				{
					return null;
				}
				return array[0];
			}, "FindElcFolderByName");
		}

		// Token: 0x06002A67 RID: 10855 RVA: 0x000B0DB5 File Offset: 0x000AEFB5
		public ADComputer FindLocalComputer()
		{
			return this.InvokeWithAPILogging<ADComputer>(delegate
			{
				string hostName = Dns.GetHostEntry("localhost").HostName;
				return this.FindComputerByHostName(null, hostName);
			}, "FindLocalComputer");
		}

		// Token: 0x06002A68 RID: 10856 RVA: 0x000B0DF7 File Offset: 0x000AEFF7
		public Server FindLocalServer()
		{
			return this.InvokeWithAPILogging<Server>(delegate
			{
				string localComputerFqdn = NativeHelpers.GetLocalComputerFqdn(true);
				Server server = this.FindServerByFqdn(localComputerFqdn);
				if (server != null)
				{
					return server;
				}
				throw new LocalServerNotFoundException(localComputerFqdn);
			}, "FindLocalServer");
		}

		// Token: 0x06002A69 RID: 10857 RVA: 0x000B0E68 File Offset: 0x000AF068
		public MailboxDatabase FindMailboxDatabaseByNameAndServer(string databaseName, Server server)
		{
			return this.InvokeWithAPILogging<MailboxDatabase>(delegate
			{
				int num = -1;
				MailboxDatabase[] mailboxDatabases = server.GetMailboxDatabases();
				if (mailboxDatabases != null)
				{
					for (int i = 0; i < mailboxDatabases.Length; i++)
					{
						if (string.Compare(mailboxDatabases[i].Name, databaseName, StringComparison.OrdinalIgnoreCase) == 0)
						{
							num = i;
							break;
						}
					}
				}
				if (num <= -1)
				{
					return null;
				}
				return mailboxDatabases[num];
			}, "FindMailboxDatabaseByNameAndServer");
		}

		// Token: 0x06002A6A RID: 10858 RVA: 0x000B0F18 File Offset: 0x000AF118
		public MesoContainer FindMesoContainer(ADDomain dom)
		{
			return this.InvokeWithAPILogging<MesoContainer>(delegate
			{
				string domainController = this.DomainController;
				MesoContainer result;
				try
				{
					this.DomainController = null;
					MesoContainer[] array = this.Find<MesoContainer>(dom.Id, QueryScope.OneLevel, null, null, 1);
					if (array == null || array.Length == 0)
					{
						result = null;
					}
					else
					{
						result = array[0];
					}
				}
				finally
				{
					this.DomainController = domainController;
				}
				return result;
			}, "FindMesoContainer");
		}

		// Token: 0x06002A6B RID: 10859 RVA: 0x000B0F8C File Offset: 0x000AF18C
		public MiniClientAccessServerOrArray[] FindMiniClientAccessServerOrArray(ADObjectId rootId, QueryScope scope, QueryFilter filter, SortBy sortBy, int maxResults, IEnumerable<PropertyDefinition> properties)
		{
			return this.InvokeWithAPILogging<MiniClientAccessServerOrArray[]>(() => this.InternalFind<MiniClientAccessServerOrArray>(rootId, scope, filter, sortBy, maxResults, properties), "FindMiniClientAccessServerOrArray");
		}

		// Token: 0x06002A6C RID: 10860 RVA: 0x000B104C File Offset: 0x000AF24C
		public MiniClientAccessServerOrArray FindMiniClientAccessServerOrArrayByFqdn(string serverFqdn, IEnumerable<PropertyDefinition> properties)
		{
			return this.InvokeWithAPILogging<MiniClientAccessServerOrArray>(delegate
			{
				if (string.IsNullOrEmpty(serverFqdn))
				{
					throw new ArgumentNullException("serverFqdn");
				}
				QueryFilter filter = ADTopologyConfigurationSession.FqdnFilterForServer(serverFqdn);
				MiniClientAccessServerOrArray[] array = this.FindMiniClientAccessServerOrArray(null, QueryScope.SubTree, filter, null, 2, properties);
				if (array == null || array.Length <= 0)
				{
					return null;
				}
				return array[0];
			}, "FindMiniClientAccessServerOrArrayByFqdn");
		}

		// Token: 0x06002A6D RID: 10861 RVA: 0x000B10C4 File Offset: 0x000AF2C4
		public MiniServer[] FindMiniServer(ADObjectId rootId, QueryScope scope, QueryFilter filter, SortBy sortBy, int maxResults, IEnumerable<PropertyDefinition> properties)
		{
			return this.InvokeWithAPILogging<MiniServer[]>(() => this.InternalFind<MiniServer>(rootId, scope, filter, sortBy, maxResults, properties), "FindMiniServer");
		}

		// Token: 0x06002A6E RID: 10862 RVA: 0x000B1184 File Offset: 0x000AF384
		public MiniServer FindMiniServerByFqdn(string serverFqdn, IEnumerable<PropertyDefinition> properties)
		{
			return this.InvokeWithAPILogging<MiniServer>(delegate
			{
				if (string.IsNullOrEmpty(serverFqdn))
				{
					throw new ArgumentNullException("serverFqdn");
				}
				QueryFilter filter = ADTopologyConfigurationSession.FqdnFilterForServer(serverFqdn);
				MiniServer[] array = this.FindMiniServer(null, QueryScope.SubTree, filter, null, 2, properties);
				if (array == null || array.Length <= 0)
				{
					return null;
				}
				return array[0];
			}, "FindMiniServerByFqdn");
		}

		// Token: 0x06002A6F RID: 10863 RVA: 0x000B1228 File Offset: 0x000AF428
		public MiniServer FindMiniServerByName(string serverName, IEnumerable<PropertyDefinition> properties)
		{
			return this.InvokeWithAPILogging<MiniServer>(delegate
			{
				if (string.IsNullOrEmpty(serverName))
				{
					throw new ArgumentNullException("serverName");
				}
				QueryFilter filter = new ComparisonFilter(ComparisonOperator.Equal, ADObjectSchema.Name, serverName);
				MiniServer[] array = this.FindMiniServer(null, QueryScope.SubTree, filter, null, 2, properties);
				if (array == null || array.Length <= 0)
				{
					return null;
				}
				return array[0];
			}, "FindMiniServerByName");
		}

		// Token: 0x06002A70 RID: 10864 RVA: 0x000B126F File Offset: 0x000AF46F
		public ADOabVirtualDirectory[] FindOABVirtualDirectoriesForLocalServer()
		{
			return this.InvokeWithAPILogging<ADOabVirtualDirectory[]>(() => this.FindVirtualDirectoriesForLocalServer<ADOabVirtualDirectory>(), "FindOABVirtualDirectoriesForLocalServer");
		}

		// Token: 0x06002A71 RID: 10865 RVA: 0x000B1290 File Offset: 0x000AF490
		public ADOwaVirtualDirectory[] FindOWAVirtualDirectoriesForLocalServer()
		{
			return this.InvokeWithAPILogging<ADOwaVirtualDirectory[]>(() => this.FindVirtualDirectoriesForLocalServer<ADOwaVirtualDirectory>(), "FindOWAVirtualDirectoriesForLocalServer");
		}

		// Token: 0x06002A72 RID: 10866 RVA: 0x000B12A9 File Offset: 0x000AF4A9
		public ADO365SuiteServiceVirtualDirectory[] FindO365SuiteServiceVirtualDirectoriesForLocalServer()
		{
			return this.FindVirtualDirectoriesForLocalServer<ADO365SuiteServiceVirtualDirectory>();
		}

		// Token: 0x06002A73 RID: 10867 RVA: 0x000B12B1 File Offset: 0x000AF4B1
		public ADSnackyServiceVirtualDirectory[] FindSnackyServiceVirtualDirectoriesForLocalServer()
		{
			return this.FindVirtualDirectoriesForLocalServer<ADSnackyServiceVirtualDirectory>();
		}

		// Token: 0x06002A74 RID: 10868 RVA: 0x000B12F8 File Offset: 0x000AF4F8
		public MiniVirtualDirectory[] FindMiniVirtualDirectories(ADObjectId serverId)
		{
			return this.InvokeWithAPILogging<MiniVirtualDirectory[]>(delegate
			{
				ArgumentValidator.ThrowIfNull("serverId", serverId);
				return this.Find<MiniVirtualDirectory>(serverId, QueryScope.SubTree, null, null, 0);
			}, "FindMiniVirtualDirectories");
		}

		// Token: 0x06002A75 RID: 10869 RVA: 0x000B136C File Offset: 0x000AF56C
		public ADPagedReader<MiniServer> FindPagedMiniServer(ADObjectId rootId, QueryScope scope, QueryFilter filter, SortBy sortBy, int pageSize, IEnumerable<PropertyDefinition> properties)
		{
			return this.InvokeWithAPILogging<ADPagedReader<MiniServer>>(() => this.FindPaged<MiniServer>(rootId, scope, filter, sortBy, pageSize, properties), "FindPagedMiniServer");
		}

		// Token: 0x06002A76 RID: 10870 RVA: 0x000B1424 File Offset: 0x000AF624
		public MiniServer FindMiniServerByFqdn(string serverFqdn)
		{
			return this.InvokeWithAPILogging<MiniServer>(delegate
			{
				if (string.IsNullOrEmpty(serverFqdn))
				{
					throw new ArgumentNullException("serverFqdn");
				}
				QueryFilter filter = ADTopologyConfigurationSession.FqdnFilterForServer(serverFqdn);
				MiniServer[] array = this.Find<MiniServer>(null, QueryScope.SubTree, filter, null, 1);
				if (array == null || array.Length <= 0)
				{
					return null;
				}
				return array[0];
			}, "FindMiniServerByFqdn");
		}

		// Token: 0x06002A77 RID: 10871 RVA: 0x000B14B4 File Offset: 0x000AF6B4
		public Server FindServerByFqdn(string serverFqdn)
		{
			return this.InvokeWithAPILogging<Server>(delegate
			{
				if (string.IsNullOrEmpty(serverFqdn))
				{
					throw new ArgumentNullException("serverFqdn");
				}
				QueryFilter filter = ADTopologyConfigurationSession.FqdnFilterForServer(serverFqdn);
				Server[] array = this.Find<Server>(null, QueryScope.SubTree, filter, null, 2);
				if (array == null || array.Length <= 0)
				{
					return null;
				}
				return array[0];
			}, "FindServerByFqdn");
		}

		// Token: 0x06002A78 RID: 10872 RVA: 0x000B1554 File Offset: 0x000AF754
		public Server FindServerByLegacyDN(string legacyExchangeDN)
		{
			return this.InvokeWithAPILogging<Server>(() => (from result in this.FindByExchangeLegacyDNs<Server>(new string[]
			{
				legacyExchangeDN
			}, null)
			select result.Data).Single<Server>(), "FindServerByLegacyDN");
		}

		// Token: 0x06002A79 RID: 10873 RVA: 0x000B15EC File Offset: 0x000AF7EC
		public Server FindServerByName(string serverName)
		{
			return this.InvokeWithAPILogging<Server>(delegate
			{
				if (string.IsNullOrEmpty(serverName))
				{
					throw new ArgumentNullException("serverName");
				}
				QueryFilter filter = new ComparisonFilter(ComparisonOperator.Equal, ADObjectSchema.Name, serverName);
				Server[] array = this.Find<Server>(null, QueryScope.SubTree, filter, null, 2);
				if (array == null || array.Length <= 0)
				{
					return null;
				}
				return array[0];
			}, "FindServerByName");
		}

		// Token: 0x06002A7A RID: 10874 RVA: 0x000B1994 File Offset: 0x000AFB94
		public ReadOnlyCollection<ADServer> FindServerWithNtdsdsa(string domainDN, bool gcOnly, bool includingRodc)
		{
			return this.InvokeWithAPILogging<ReadOnlyCollection<ADServer>>(delegate
			{
				ADObjectId id = this.GetSitesContainer().Id;
				QueryFilter queryFilter = null;
				if (!string.IsNullOrEmpty(domainDN))
				{
					queryFilter = new ComparisonFilter(ComparisonOperator.Equal, NtdsDsaSchema.MasterNCs, domainDN);
					if (includingRodc)
					{
						queryFilter = new OrFilter(new QueryFilter[]
						{
							queryFilter,
							new ComparisonFilter(ComparisonOperator.Equal, NtdsDsaSchema.FullReplicaNCs, domainDN)
						});
					}
				}
				if (gcOnly)
				{
					QueryFilter queryFilter2 = new BitMaskAndFilter(NtdsDsaSchema.Options, 1UL);
					queryFilter = ((queryFilter == null) ? queryFilter2 : new AndFilter(new QueryFilter[]
					{
						queryFilter,
						queryFilter2
					}));
				}
				if (!includingRodc)
				{
					QueryFilter queryFilter3 = new ComparisonFilter(ComparisonOperator.Equal, NtdsDsaSchema.DsIsRodc, false);
					queryFilter = ((queryFilter == null) ? queryFilter3 : new AndFilter(new QueryFilter[]
					{
						queryFilter,
						queryFilter3
					}));
				}
				ADPagedReader<NtdsDsa> adpagedReader = this.FindPaged<NtdsDsa>(id, QueryScope.SubTree, queryFilter, null, 0);
				Dictionary<string, string> dictionary = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
				foreach (NtdsDsa ntdsDsa in adpagedReader)
				{
					ExTraceGlobals.ADTopologyTracer.TraceDebug<string>((long)this.GetHashCode(), "Found matching ntdsdsa under {0}", ntdsDsa.Id.Parent.Name);
					string distinguishedName = ntdsDsa.Id.Parent.DistinguishedName;
					dictionary[distinguishedName] = distinguishedName;
				}
				QueryFilter filter = new ExistsFilter(ADServerSchema.DnsHostName);
				ADPagedReader<ADServer> adpagedReader2 = this.FindPaged<ADServer>(id, QueryScope.SubTree, filter, null, 0);
				Dictionary<string, ADServer> dictionary2 = new Dictionary<string, ADServer>(StringComparer.OrdinalIgnoreCase);
				foreach (ADServer adserver in adpagedReader2)
				{
					if (dictionary.ContainsKey(adserver.DistinguishedName))
					{
						ExTraceGlobals.ADTopologyTracer.TraceDebug<string>((long)this.GetHashCode(), "Found AD server {0} that has matching ntdsdsa object", adserver.DnsHostName);
						try
						{
							dictionary2.Add(adserver.DnsHostName, adserver);
							continue;
						}
						catch (ArgumentException)
						{
							ADServer adserver2 = null;
							if (!dictionary2.TryGetValue(adserver.DnsHostName, out adserver2))
							{
								throw;
							}
							DateTime? whenCreated = adserver2.WhenCreated;
							DateTime? whenCreated2 = adserver.WhenCreated;
							if (whenCreated != null && whenCreated2 != null && whenCreated.Value < whenCreated2.Value)
							{
								dictionary2[adserver.DnsHostName] = adserver;
								Globals.LogExchangeTopologyEvent(DirectoryEventLogConstants.Tuple_DSC_EVENT_DUPLICATED_SERVER, adserver.DistinguishedName, new object[]
								{
									adserver.DistinguishedName,
									adserver2.DistinguishedName
								});
							}
							else
							{
								Globals.LogExchangeTopologyEvent(DirectoryEventLogConstants.Tuple_DSC_EVENT_DUPLICATED_SERVER, adserver.DistinguishedName, new object[]
								{
									adserver.DistinguishedName,
									adserver2.DistinguishedName
								});
							}
							continue;
						}
					}
					ExTraceGlobals.ADTopologyTracer.TraceWarning<string>((long)this.GetHashCode(), "Found non-DC AD server or an AD server that doesn't match with ntdsdsa object {0}", adserver.Name);
				}
				List<ADServer> list = new List<ADServer>(dictionary2.Values);
				return new ReadOnlyCollection<ADServer>(list);
			}, "FindServerWithNtdsdsa");
		}

		// Token: 0x06002A7B RID: 10875 RVA: 0x000B1A68 File Offset: 0x000AFC68
		public TResult FindUnique<TResult>(ADObjectId rootId, QueryScope scope, QueryFilter filter) where TResult : ADConfigurationObject, new()
		{
			return this.InvokeWithAPILogging<TResult>(delegate
			{
				TResult[] array = this.InternalFind<TResult>(rootId, scope, filter, null, 2, null);
				if (array.Length == 1)
				{
					return array[0];
				}
				if (array.Length == 2)
				{
					QueryFilter[] array2 = new QueryFilter[2];
					QueryFilter[] array3 = array2;
					int num = 0;
					TResult tresult = Activator.CreateInstance<TResult>();
					array3[num] = tresult.ImplicitFilter;
					array2[1] = filter;
					throw new ADResultsNotUniqueException(QueryFilter.AndTogether(array2).ToString());
				}
				return default(TResult);
			}, "FindUnique");
		}

		// Token: 0x06002A7C RID: 10876 RVA: 0x000B1ACC File Offset: 0x000AFCCC
		public TPolicy[] FindWorkloadManagementChildPolicies<TPolicy>(ADObjectId wlmPolicy) where TPolicy : ADConfigurationObject, new()
		{
			return this.InvokeWithAPILogging<TPolicy[]>(() => this.FindWorkloadManagementChildPolicies<TPolicy>(wlmPolicy, null), "FindWorkloadManagementChildPolicies");
		}

		// Token: 0x06002A7D RID: 10877 RVA: 0x000B1B42 File Offset: 0x000AFD42
		public AdministrativeGroup GetAdministrativeGroup()
		{
			return this.InvokeWithAPILogging<AdministrativeGroup>(delegate
			{
				AdministrativeGroup[] array = base.Find<AdministrativeGroup>(null, QueryScope.SubTree, new ComparisonFilter(ComparisonOperator.Equal, ADObjectSchema.Name, AdministrativeGroup.DefaultName), null, 1);
				if (array == null || array.Length == 0)
				{
					throw new DefaultAdministrativeGroupNotFoundException(AdministrativeGroup.DefaultName);
				}
				return array[0];
			}, "GetAdministrativeGroup");
		}

		// Token: 0x06002A7E RID: 10878 RVA: 0x000B1B8A File Offset: 0x000AFD8A
		public ADObjectId GetAdministrativeGroupId()
		{
			return this.InvokeWithAPILogging<ADObjectId>(delegate
			{
				if (this.adminGroupId == null)
				{
					AdministrativeGroup administrativeGroup = this.GetAdministrativeGroup();
					this.adminGroupId = administrativeGroup.Id;
				}
				return this.adminGroupId;
			}, "GetAdministrativeGroupId");
		}

		// Token: 0x06002A7F RID: 10879 RVA: 0x000B1BBF File Offset: 0x000AFDBF
		public ADPagedReader<ExtendedRight> GetAllExtendedRights()
		{
			return this.InvokeWithAPILogging<ADPagedReader<ExtendedRight>>(() => base.FindPaged<ExtendedRight>(base.ConfigurationNamingContext.GetChildId("Extended-Rights"), QueryScope.OneLevel, null, null, 0), "GetAllExtendedRights");
		}

		// Token: 0x06002A80 RID: 10880 RVA: 0x000B1C01 File Offset: 0x000AFE01
		public ADObjectId GetAutoDiscoverGlobalContainerId()
		{
			return this.InvokeWithAPILogging<ADObjectId>(delegate
			{
				ADObjectId childId = base.ConfigurationNamingContext.GetChildId("Services");
				return childId.GetChildId("Microsoft Exchange Autodiscover");
			}, "GetAutoDiscoverGlobalContainerId");
		}

		// Token: 0x06002A81 RID: 10881 RVA: 0x000B1D00 File Offset: 0x000AFF00
		public string[] GetAutodiscoverTrustedHosters()
		{
			return this.InvokeWithAPILogging<string[]>(delegate
			{
				ADServiceConnectionPoint[] array = base.Find<ADServiceConnectionPoint>(this.GetAutoDiscoverGlobalContainerId(), QueryScope.SubTree, ExchangeScpObjects.AutodiscoverTrustedHosterKeyword.Filter, null, 0);
				if (array == null || array.Length == 0)
				{
					return null;
				}
				List<string> list = new List<string>(array.Length);
				foreach (ADServiceConnectionPoint adserviceConnectionPoint in array)
				{
					if (adserviceConnectionPoint.ServiceBindingInformation != null && adserviceConnectionPoint.ServiceBindingInformation.Count > 0)
					{
						foreach (string text in adserviceConnectionPoint.ServiceBindingInformation)
						{
							if (ADTopologyConfigurationSession.IsValidTrustedHoster(text))
							{
								list.Add(text);
							}
							else
							{
								ExTraceGlobals.ClientThrottlingTracer.TraceError<string>((long)this.GetHashCode(), "[ADTopologyConfigurationSession::GetAutodiscoverTrustedHosters] Ignoring invalid trusted hoster value '{0}'.", text);
							}
						}
					}
				}
				if (list.Count == 0)
				{
					return null;
				}
				return list.ToArray();
			}, "GetAutodiscoverTrustedHosters");
		}

		// Token: 0x06002A82 RID: 10882 RVA: 0x000B1D30 File Offset: 0x000AFF30
		public ADObjectId GetClientAccessContainerId()
		{
			return this.InvokeWithAPILogging<ADObjectId>(() => base.GetOrgContainerId().GetDescendantId(new ADObjectId("CN=Client Access")), "GetClientAccessContainerId");
		}

		// Token: 0x06002A83 RID: 10883 RVA: 0x000B1D8A File Offset: 0x000AFF8A
		public DatabaseAvailabilityGroupContainer GetDatabaseAvailabilityGroupContainer()
		{
			return this.InvokeWithAPILogging<DatabaseAvailabilityGroupContainer>(delegate
			{
				DatabaseAvailabilityGroupContainer[] array = base.Find<DatabaseAvailabilityGroupContainer>(null, QueryScope.SubTree, new ComparisonFilter(ComparisonOperator.Equal, ADObjectSchema.Name, DatabaseAvailabilityGroupContainer.DefaultName), null, 1);
				if (array == null || array.Length == 0)
				{
					throw new DefaultDatabaseAvailabilityGroupContainerNotFoundException(DatabaseAvailabilityGroupContainer.DefaultName);
				}
				return array[0];
			}, "GetDatabaseAvailabilityGroupContainer");
		}

		// Token: 0x06002A84 RID: 10884 RVA: 0x000B1DD2 File Offset: 0x000AFFD2
		public ADObjectId GetDatabaseAvailabilityGroupContainerId()
		{
			return this.InvokeWithAPILogging<ADObjectId>(delegate
			{
				if (this.databaseAvailabilityGroupContainerId == null)
				{
					DatabaseAvailabilityGroupContainer databaseAvailabilityGroupContainer = this.GetDatabaseAvailabilityGroupContainer();
					this.databaseAvailabilityGroupContainerId = databaseAvailabilityGroupContainer.Id;
				}
				return this.databaseAvailabilityGroupContainerId;
			}, "GetDatabaseAvailabilityGroupContainerId");
		}

		// Token: 0x06002A85 RID: 10885 RVA: 0x000B1E2A File Offset: 0x000B002A
		public DatabasesContainer GetDatabasesContainer()
		{
			return this.InvokeWithAPILogging<DatabasesContainer>(delegate
			{
				DatabasesContainer[] array = base.Find<DatabasesContainer>(null, QueryScope.SubTree, new ComparisonFilter(ComparisonOperator.Equal, ADObjectSchema.Name, DatabasesContainer.DefaultName), null, 1);
				if (array == null || array.Length == 0)
				{
					throw new DefaultDatabaseContainerNotFoundException(DatabasesContainer.DefaultName);
				}
				return array[0];
			}, "GetDatabasesContainer");
		}

		// Token: 0x06002A86 RID: 10886 RVA: 0x000B1E72 File Offset: 0x000B0072
		public ADObjectId GetDatabasesContainerId()
		{
			return this.InvokeWithAPILogging<ADObjectId>(delegate
			{
				if (this.databasesContainerId == null)
				{
					DatabasesContainer databasesContainer = this.GetDatabasesContainer();
					this.databasesContainerId = databasesContainer.Id;
				}
				return this.databasesContainerId;
			}, "GetDatabasesContainerId");
		}

		// Token: 0x06002A87 RID: 10887 RVA: 0x000B1ECA File Offset: 0x000B00CA
		public ServiceEndpointContainer GetEndpointContainer()
		{
			return this.InvokeWithAPILogging<ServiceEndpointContainer>(delegate
			{
				ServiceEndpointContainer[] array = base.Find<ServiceEndpointContainer>(null, QueryScope.SubTree, new ComparisonFilter(ComparisonOperator.Equal, ADObjectSchema.Name, ServiceEndpointContainer.DefaultName), null, 1);
				if (array == null || array.Length == 0)
				{
					throw new EndpointContainerNotFoundException(ServiceEndpointContainer.DefaultName);
				}
				return array[0];
			}, "GetEndpointContainer");
		}

		// Token: 0x06002A88 RID: 10888 RVA: 0x000B1EEC File Offset: 0x000B00EC
		public ThrottlingPolicy GetGlobalThrottlingPolicy()
		{
			return this.InvokeWithAPILogging<ThrottlingPolicy>(() => this.InteralGetGlobalThrottlingPolicy(false), "GetGlobalThrottlingPolicy");
		}

		// Token: 0x06002A89 RID: 10889 RVA: 0x000B1F20 File Offset: 0x000B0120
		public ThrottlingPolicy GetGlobalThrottlingPolicy(bool throwError)
		{
			return this.InvokeWithAPILogging<ThrottlingPolicy>(() => this.InteralGetGlobalThrottlingPolicy(throwError), "GetGlobalThrottlingPolicy");
		}

		// Token: 0x06002A8A RID: 10890 RVA: 0x000B1F74 File Offset: 0x000B0174
		public Guid GetInvocationIdByDC(ADServer dc)
		{
			return this.InvokeWithAPILogging<Guid>(() => this.InternalGetInvocationIdByDC(dc), "GetInvocationIdByDC");
		}

		// Token: 0x06002A8B RID: 10891 RVA: 0x000B1FF4 File Offset: 0x000B01F4
		public Guid GetInvocationIdByFqdn(string serverFqdn)
		{
			return this.InvokeWithAPILogging<Guid>(delegate
			{
				ADServer adserver = this.FindDCByFqdn(serverFqdn);
				if (adserver == null)
				{
					throw new ADOperationException(DirectoryStrings.ErrorDCNotFound(serverFqdn));
				}
				return this.InternalGetInvocationIdByDC(adserver);
			}, "GetInvocationIdByFqdn");
		}

		// Token: 0x06002A8C RID: 10892 RVA: 0x000B20A1 File Offset: 0x000B02A1
		public ADSite GetLocalSite()
		{
			return this.InvokeWithAPILogging<ADSite>(delegate
			{
				string siteName = NativeHelpers.GetSiteName(true);
				QueryFilter filter = new ComparisonFilter(ComparisonOperator.Equal, ADObjectSchema.Name, siteName);
				ADSite[] array = base.Find<ADSite>(base.ConfigurationNamingContext, QueryScope.SubTree, filter, null, 1);
				if (array == null || array.Length == 0)
				{
					ExTraceGlobals.ADTopologyTracer.TraceDebug<string>((long)this.GetHashCode(), "No site was found with the name {0}", siteName);
					throw new CannotGetSiteInfoException(DirectoryStrings.CannotGetUsefulSiteInfo);
				}
				ExTraceGlobals.ADTopologyTracer.TraceDebug((long)this.GetHashCode(), "Local site found");
				return array[0];
			}, "GetLocalSite");
		}

		// Token: 0x06002A8D RID: 10893 RVA: 0x000B20FC File Offset: 0x000B02FC
		public MsoMainStreamCookieContainer GetMsoMainStreamCookieContainer(string serviceInstanceName)
		{
			return this.InvokeWithAPILogging<MsoMainStreamCookieContainer>(delegate
			{
				ADObjectId serviceInstanceObjectId = SyncServiceInstance.GetServiceInstanceObjectId(serviceInstanceName);
				MsoMainStreamCookieContainer msoMainStreamCookieContainer = this.Read<MsoMainStreamCookieContainer>(serviceInstanceObjectId);
				if (msoMainStreamCookieContainer == null)
				{
					throw new ServiceInstanceContainerNotFoundException(serviceInstanceName);
				}
				return msoMainStreamCookieContainer;
			}, "GetMsoMainStreamCookieContainer");
		}

		// Token: 0x06002A8E RID: 10894 RVA: 0x000B2154 File Offset: 0x000B0354
		public PooledLdapConnection GetNotifyConnection()
		{
			return this.InvokeWithAPILogging<PooledLdapConnection>(delegate
			{
				string accountOrResourceForestFqdn = base.SessionSettings.GetAccountOrResourceForestFqdn();
				return ConnectionPoolManager.GetConnection(ConnectionType.ConfigDCNotification, accountOrResourceForestFqdn);
			}, "GetNotifyConnection");
		}

		// Token: 0x06002A8F RID: 10895 RVA: 0x000B21E8 File Offset: 0x000B03E8
		public Server GetParentServer(ADObjectId entryId, ADObjectId originalId)
		{
			return this.InvokeWithAPILogging<Server>(delegate
			{
				ADObjectId adobjectId = entryId.DescendantDN(8);
				if (originalId != null)
				{
					ADObjectId id = originalId.DescendantDN(8);
					if (!adobjectId.Equals(id))
					{
						throw new NotSupportedException(string.Format("Moving object '{0}' from server '{1}' to '{2}' is not supported.", entryId.ToString(), originalId.ToString(), adobjectId.ToString()));
					}
				}
				return this.Read<Server>(adobjectId);
			}, "GetParentServer");
		}

		// Token: 0x06002A90 RID: 10896 RVA: 0x000B224F File Offset: 0x000B044F
		public ProvisioningReconciliationConfig GetProvisioningReconciliationConfig()
		{
			return this.InvokeWithAPILogging<ProvisioningReconciliationConfig>(delegate
			{
				ProvisioningReconciliationConfig[] array = base.Find<ProvisioningReconciliationConfig>(null, QueryScope.SubTree, null, null, 1);
				if (array != null && array.Length == 1)
				{
					return array[0];
				}
				return null;
			}, "GetProvisioningReconciliationConfig");
		}

		// Token: 0x06002A91 RID: 10897 RVA: 0x000B22A8 File Offset: 0x000B04A8
		public string GetRootDomainNamingContextFromCurrentReadConnection()
		{
			return this.InvokeWithAPILogging<string>(delegate
			{
				ADObjectId adobjectId = null;
				PooledLdapConnection readConnection = base.GetReadConnection(null, ref adobjectId);
				string rootDomainNC;
				try
				{
					rootDomainNC = readConnection.ADServerInfo.RootDomainNC;
				}
				finally
				{
					readConnection.ReturnToPool();
				}
				return rootDomainNC;
			}, "GetRootDomainNamingContextFromCurrentReadConnection");
		}

		// Token: 0x06002A92 RID: 10898 RVA: 0x000B2325 File Offset: 0x000B0525
		public RootDse GetRootDse()
		{
			return this.InvokeWithAPILogging<RootDse>(delegate
			{
				if (string.IsNullOrEmpty(base.DomainController))
				{
					throw new InvalidOperationException(DirectoryStrings.GetRootDseRequiresDomainController);
				}
				RootDse[] array = base.Find<RootDse>(new ADObjectId(string.Empty), QueryScope.Base, null, null, 0);
				if (array.Length != 1)
				{
					throw new ADOperationException(DirectoryStrings.InvalidRootDse(base.LastUsedDc));
				}
				return array[0];
			}, "GetRootDse");
		}

		// Token: 0x06002A93 RID: 10899 RVA: 0x000B237E File Offset: 0x000B057E
		public RoutingGroup GetRoutingGroup()
		{
			return this.InvokeWithAPILogging<RoutingGroup>(delegate
			{
				RoutingGroup[] array = base.Find<RoutingGroup>(null, QueryScope.SubTree, new ComparisonFilter(ComparisonOperator.Equal, ADObjectSchema.Name, RoutingGroup.DefaultName), null, 1);
				if (array == null || array.Length == 0)
				{
					throw new DefaultRoutingGroupNotFoundException(RoutingGroup.DefaultName);
				}
				return array[0];
			}, "GetRoutingGroup");
		}

		// Token: 0x06002A94 RID: 10900 RVA: 0x000B23C6 File Offset: 0x000B05C6
		public ADObjectId GetRoutingGroupId()
		{
			return this.InvokeWithAPILogging<ADObjectId>(delegate
			{
				if (this.routingGroupId == null)
				{
					RoutingGroup routingGroup = this.GetRoutingGroup();
					this.routingGroupId = routingGroup.Id;
				}
				return this.routingGroupId;
			}, "GetRoutingGroupId");
		}

		// Token: 0x06002A95 RID: 10901 RVA: 0x000B2474 File Offset: 0x000B0674
		public string GetSchemaMasterDC()
		{
			return this.InvokeWithAPILogging<string>(delegate
			{
				if (TopologyProvider.IsAdamTopology())
				{
					return "localhost";
				}
				SchemaContainer[] array = this.InternalFind<SchemaContainer>(base.GetSchemaNamingContext(), QueryScope.Base, null, null, 1, null);
				if (array == null || array.Length == 0)
				{
					throw new SchemaMasterDCNotFoundException(DirectoryStrings.ExceptionNoSchemaContainerObject);
				}
				ADObjectId fsmoRoleOwner = array[0].FsmoRoleOwner;
				if (fsmoRoleOwner == null)
				{
					throw new SchemaMasterDCNotFoundException(DirectoryStrings.ExceptionNoFsmoRoleOwnerAttribute);
				}
				ADObjectId parent = fsmoRoleOwner.Parent;
				ADServer[] array2 = this.InternalFind<ADServer>(parent, QueryScope.Base, null, null, 1, null);
				if (array2 == null || array2.Length == 0)
				{
					throw new SchemaMasterDCNotFoundException(DirectoryStrings.ExceptionNoSchemaMasterServerObject(parent.DistinguishedName ?? string.Empty));
				}
				return array2[0].DnsHostName;
			}, "GetSchemaMasterDC");
		}

		// Token: 0x06002A96 RID: 10902 RVA: 0x000B24F6 File Offset: 0x000B06F6
		public ServicesContainer GetServicesContainer()
		{
			return this.InvokeWithAPILogging<ServicesContainer>(delegate
			{
				ServicesContainer[] array = base.Find<ServicesContainer>(null, QueryScope.OneLevel, new AndFilter(new QueryFilter[]
				{
					new ComparisonFilter(ComparisonOperator.Equal, ADConfigurationObjectSchema.SystemFlags, SystemFlagsEnum.Indispensable),
					new ComparisonFilter(ComparisonOperator.Equal, ADObjectSchema.Name, ServicesContainer.DefaultName)
				}), null, 1);
				if (array == null || array.Length == 0)
				{
					throw new ServicesContainerNotFoundException(DirectoryStrings.ServicesContainerNotFound);
				}
				return array[0];
			}, "GetServicesContainer");
		}

		// Token: 0x06002A97 RID: 10903 RVA: 0x000B253F File Offset: 0x000B073F
		public SitesContainer GetSitesContainer()
		{
			return this.InvokeWithAPILogging<SitesContainer>(delegate
			{
				SitesContainer[] array = base.Find<SitesContainer>(null, QueryScope.SubTree, null, null, 1);
				if (array == null || array.Length == 0)
				{
					throw new SitesContainerNotFoundException(DirectoryStrings.SitesContainerNotFound);
				}
				return array[0];
			}, "GetSitesContainer");
		}

		// Token: 0x06002A98 RID: 10904 RVA: 0x000B2596 File Offset: 0x000B0796
		public StampGroupContainer GetStampGroupContainer()
		{
			return this.InvokeWithAPILogging<StampGroupContainer>(delegate
			{
				StampGroupContainer[] array = base.Find<StampGroupContainer>(null, QueryScope.SubTree, new ComparisonFilter(ComparisonOperator.Equal, ADObjectSchema.Name, StampGroupContainer.DefaultName), null, 1);
				if (array == null || array.Length == 0)
				{
					throw new DefaultDatabaseAvailabilityGroupContainerNotFoundException(StampGroupContainer.DefaultName);
				}
				return array[0];
			}, "GetStampGroupContainer");
		}

		// Token: 0x06002A99 RID: 10905 RVA: 0x000B25B0 File Offset: 0x000B07B0
		public ADObjectId GetStampGroupContainerId()
		{
			if (this.stampGroupContainerId == null)
			{
				StampGroupContainer stampGroupContainer = this.GetStampGroupContainer();
				this.stampGroupContainerId = stampGroupContainer.Id;
			}
			return this.stampGroupContainerId;
		}

		// Token: 0x06002A9A RID: 10906 RVA: 0x000B25F0 File Offset: 0x000B07F0
		public bool HasAnyServer()
		{
			return this.InvokeWithAPILogging<bool>(() => base.Find<Server>(null, QueryScope.SubTree, null, null, 1).Any<Server>(), "HasAnyServer");
		}

		// Token: 0x06002A9B RID: 10907 RVA: 0x000B2669 File Offset: 0x000B0869
		public bool IsInE12InteropMode()
		{
			return this.InvokeWithAPILogging<bool>(delegate
			{
				QueryFilter filter = new AndFilter(new QueryFilter[]
				{
					new ComparisonFilter(ComparisonOperator.GreaterThanOrEqual, ServerSchema.VersionNumber, Server.E2007MinVersion),
					new ComparisonFilter(ComparisonOperator.LessThan, ServerSchema.VersionNumber, Server.E14MinVersion)
				});
				Server[] array = base.Find<Server>(null, QueryScope.SubTree, filter, null, 1);
				return array.Length > 0;
			}, "IsInE12InteropMode");
		}

		// Token: 0x06002A9C RID: 10908 RVA: 0x000B26B9 File Offset: 0x000B08B9
		public bool IsInPreE12InteropMode()
		{
			return this.InvokeWithAPILogging<bool>(delegate
			{
				QueryFilter filter = new ComparisonFilter(ComparisonOperator.LessThan, ServerSchema.VersionNumber, Server.E2007MinVersion);
				Server[] array = base.Find<Server>(null, QueryScope.SubTree, filter, null, 1);
				return array.Length > 0;
			}, "IsInPreE12InteropMode");
		}

		// Token: 0x06002A9D RID: 10909 RVA: 0x000B2709 File Offset: 0x000B0909
		public bool IsInPreE14InteropMode()
		{
			return this.InvokeWithAPILogging<bool>(delegate
			{
				QueryFilter filter = new ComparisonFilter(ComparisonOperator.LessThan, ServerSchema.VersionNumber, Server.E14MinVersion);
				Server[] array = base.Find<Server>(null, QueryScope.SubTree, filter, null, 1);
				return array.Length > 0;
			}, "IsInPreE14InteropMode");
		}

		// Token: 0x06002A9E RID: 10910 RVA: 0x000B2724 File Offset: 0x000B0924
		public Server ReadLocalServer()
		{
			string localComputerFqdn = NativeHelpers.GetLocalComputerFqdn(false);
			if (localComputerFqdn != null)
			{
				return this.FindServerByFqdn(localComputerFqdn);
			}
			return null;
		}

		// Token: 0x06002A9F RID: 10911 RVA: 0x000B2768 File Offset: 0x000B0968
		public MiniClientAccessServerOrArray ReadMiniClientAccessServerOrArray(ADObjectId entryId, IEnumerable<PropertyDefinition> properties)
		{
			return this.InvokeWithAPILogging<MiniClientAccessServerOrArray>(() => this.InternalRead<MiniClientAccessServerOrArray>(entryId, properties), "ReadMiniClientAccessServerOrArray");
		}

		// Token: 0x06002AA0 RID: 10912 RVA: 0x000B27C8 File Offset: 0x000B09C8
		public MiniServer ReadMiniServer(ADObjectId entryId, IEnumerable<PropertyDefinition> properties)
		{
			return this.InvokeWithAPILogging<MiniServer>(() => this.InternalRead<MiniServer>(entryId, properties), "ReadMiniServer");
		}

		// Token: 0x06002AA1 RID: 10913 RVA: 0x000B2848 File Offset: 0x000B0A48
		public Result<TResult>[] ReadMultipleLegacyObjects<TResult>(string[] objectNames) where TResult : ADLegacyVersionableObject, new()
		{
			return this.InvokeWithAPILogging<Result<TResult>[]>(() => this.ReadMultiple<string, TResult>(objectNames, new Converter<string, QueryFilter>(ADTopologyConfigurationSession.ReadMultipleADLegacyObjectsQueryFilterFromObjectName), new ADDataSession.HashInserter<TResult>(ADTopologyConfigurationSession.ReadMultipleADLegacyObjectsHashInserter<TResult>), new ADDataSession.HashLookup<string, TResult>(ADTopologyConfigurationSession.ReadMultipleADLegacyObjectsHashLookup<TResult>), null), "ReadMultipleLegacyObjects");
		}

		// Token: 0x06002AA2 RID: 10914 RVA: 0x000B2880 File Offset: 0x000B0A80
		public Result<Server>[] ReadMultipleServers(string[] serverNames)
		{
			return this.ReadMultipleLegacyObjects<Server>(serverNames);
		}

		// Token: 0x06002AA3 RID: 10915 RVA: 0x000B28B8 File Offset: 0x000B0AB8
		public ManagementScope ReadRootOrgManagementScopeByName(string scopeName)
		{
			return this.InvokeWithAPILogging<ManagementScope>(() => this.Read<ManagementScope>(ADSystemConfigurationSession.GetRootOrgContainerIdForLocalForest().GetDescendantId(ManagementScope.RdnScopesContainerToOrganization).GetChildId(scopeName)), "ReadRootOrgManagementScopeByName");
		}

		// Token: 0x06002AA4 RID: 10916 RVA: 0x000B28FC File Offset: 0x000B0AFC
		private bool TryFindByExchangeLegacyDN<TData>(string legacyExchangeDN, IEnumerable<PropertyDefinition> properties, out TData data) where TData : ADRawEntry, new()
		{
			data = (from result in base.FindByExchangeLegacyDNs<TData>(new string[]
			{
				legacyExchangeDN
			}, properties)
			select result.Data).Single<TData>();
			return data != null;
		}

		// Token: 0x06002AA5 RID: 10917 RVA: 0x000B2949 File Offset: 0x000B0B49
		public bool TryFindByExchangeLegacyDN(string legacyExchangeDN, IEnumerable<PropertyDefinition> properties, out MiniClientAccessServerOrArray miniClientAccessServerOrArray)
		{
			return this.TryFindByExchangeLegacyDN<MiniClientAccessServerOrArray>(legacyExchangeDN, properties, out miniClientAccessServerOrArray);
		}

		// Token: 0x06002AA6 RID: 10918 RVA: 0x000B2954 File Offset: 0x000B0B54
		public bool TryFindByExchangeLegacyDN(string legacyExchangeDN, IEnumerable<PropertyDefinition> properties, out MiniServer miniServer)
		{
			return this.TryFindByExchangeLegacyDN<MiniServer>(legacyExchangeDN, properties, out miniServer);
		}

		// Token: 0x06002AA7 RID: 10919 RVA: 0x000B2960 File Offset: 0x000B0B60
		public bool TryGetDefaultAdQueryPolicy(out ADQueryPolicy queryPolicy)
		{
			ADQueryPolicy[] array = base.Find<ADQueryPolicy>(ADSession.GetConfigurationNamingContextForLocalForest().GetChildId("Services").GetChildId("Windows NT"), QueryScope.SubTree, new ComparisonFilter(ComparisonOperator.Equal, ADObjectSchema.Name, ADQueryPolicy.ADDefaultQueryPolicyName), null, 1);
			if (array != null && array.Length == 1)
			{
				queryPolicy = array[0];
				return true;
			}
			queryPolicy = null;
			return false;
		}

		// Token: 0x06002AA8 RID: 10920 RVA: 0x000B29B4 File Offset: 0x000B0BB4
		public void UpdateGwartLastModified()
		{
			LegacyGwart[] array = base.Find<LegacyGwart>(this.GetAdministrativeGroupId(), QueryScope.SubTree, new ComparisonFilter(ComparisonOperator.Equal, ADObjectSchema.Name, LegacyGwart.DefaultName), null, 1);
			if (array == null || array.Length == 0)
			{
				throw new LegacyGwartNotFoundException(LegacyGwart.DefaultName, AdministrativeGroup.DefaultName);
			}
			DateTime? dateTime = new DateTime?(array[0].GwartLastModified ?? DateTime.UtcNow);
			array[0].GwartLastModified = new DateTime?(dateTime.Value.AddMinutes(8.0));
			base.Save(array[0]);
		}

		// Token: 0x06002AA9 RID: 10921 RVA: 0x000B2A4E File Offset: 0x000B0C4E
		private TResult[] InternalFind<TResult>(ADObjectId rootId, QueryScope scope, QueryFilter filter, SortBy sortBy, int maxResults, IEnumerable<PropertyDefinition> properties) where TResult : ADObject, new()
		{
			return base.Find<TResult>(rootId, scope, filter, sortBy, maxResults, properties, false);
		}

		// Token: 0x06002AAA RID: 10922 RVA: 0x000B2A60 File Offset: 0x000B0C60
		private TPolicy[] FindWorkloadManagementChildPolicies<TPolicy>(ADObjectId wlmPolicy, QueryFilter filter) where TPolicy : ADConfigurationObject, new()
		{
			if (wlmPolicy == null)
			{
				throw new ArgumentNullException("wlmPolicy");
			}
			return base.Find<TPolicy>(wlmPolicy, QueryScope.OneLevel, filter, null, 0);
		}

		// Token: 0x06002AAB RID: 10923 RVA: 0x000B2A7C File Offset: 0x000B0C7C
		private T[] FindVirtualDirectoriesForLocalServer<T>() where T : ExchangeVirtualDirectory, new()
		{
			Server server = null;
			try
			{
				server = this.FindLocalServer();
			}
			catch (LocalServerNotFoundException)
			{
			}
			if (server == null)
			{
				return new T[0];
			}
			return this.FindVirtualDirectories<T>(server);
		}

		// Token: 0x06002AAC RID: 10924 RVA: 0x000B2AB8 File Offset: 0x000B0CB8
		private T[] FindVirtualDirectories<T>(Server server) where T : ExchangeVirtualDirectory, new()
		{
			if (server == null)
			{
				throw new ArgumentNullException("server");
			}
			T[] array = base.Find<T>(server.Id, QueryScope.SubTree, null, null, 0);
			if (array.Length == 0)
			{
				return new T[0];
			}
			return array;
		}

		// Token: 0x06002AAD RID: 10925 RVA: 0x000B2AF4 File Offset: 0x000B0CF4
		private ThrottlingPolicy InteralGetGlobalThrottlingPolicy(bool throwError)
		{
			QueryFilter filter = new ComparisonFilter(ComparisonOperator.Equal, ThrottlingPolicySchema.ThrottlingPolicyScope, ThrottlingPolicyScopeType.Global);
			ADObjectId descendantId = ADSystemConfigurationSession.GetRootOrgContainerIdForLocalForest().GetDescendantId(new ADObjectId("CN=Global Settings"));
			ThrottlingPolicy[] array = base.Find<ThrottlingPolicy>(descendantId, QueryScope.OneLevel, filter, null, 2);
			if (array == null || array.Length == 0)
			{
				ExTraceGlobals.ClientThrottlingTracer.TraceError((long)this.GetHashCode(), "[ADTopologyConfiguartionSession::GetGlobalThrottlingPolicy] No global policy found in first org.");
				Globals.LogExchangeTopologyEvent(DirectoryEventLogConstants.Tuple_GlobalThrottlingPolicyMissing, descendantId.DistinguishedName, new object[0]);
				if (throwError)
				{
					throw new GlobalThrottlingPolicyNotFoundException();
				}
				return null;
			}
			else
			{
				if (array.Length == 1)
				{
					return array[0];
				}
				ExTraceGlobals.ClientThrottlingTracer.TraceError<int>((long)this.GetHashCode(), "[ADTopologyConfiguartionSession::GetGlobalThrottlingPolicy] '{0}' global policies found in first org.", array.Length);
				Globals.LogExchangeTopologyEvent(DirectoryEventLogConstants.Tuple_MoreThanOneGlobalThrottlingPolicy, descendantId.DistinguishedName, new object[]
				{
					array.Length
				});
				if (throwError)
				{
					throw new GlobalThrottlingPolicyAmbiguousException();
				}
				return null;
			}
		}

		// Token: 0x06002AAE RID: 10926 RVA: 0x000B2BC8 File Offset: 0x000B0DC8
		private Guid InternalGetInvocationIdByDC(ADServer dc)
		{
			if (dc == null)
			{
				throw new ArgumentNullException("DC cannot be null");
			}
			NtdsDsa[] array = base.Find<NtdsDsa>(dc.Id, QueryScope.OneLevel, null, null, 1);
			if (array.Length != 1)
			{
				throw new ADOperationException(DirectoryStrings.InvalidNtds(dc.DnsHostName));
			}
			return array[0].InvocationId;
		}

		// Token: 0x06002AAF RID: 10927 RVA: 0x000B2C30 File Offset: 0x000B0E30
		private T InvokeWithAPILogging<T>(Func<T> action, [CallerMemberName] string memberName = null)
		{
			return ADScenarioLog.InvokeWithAPILog<T>(DateTime.UtcNow, memberName, default(Guid), ADTopologyConfigurationSession.ClassName, "", () => action(), () => base.LastUsedDc);
		}

		// Token: 0x04001990 RID: 6544
		private static string ClassName = "ADTopologyConfigurationSession";

		// Token: 0x04001991 RID: 6545
		[NonSerialized]
		private ADObjectId adminGroupId;

		// Token: 0x04001992 RID: 6546
		[NonSerialized]
		private ADObjectId databasesContainerId;

		// Token: 0x04001993 RID: 6547
		[NonSerialized]
		private ADObjectId databaseAvailabilityGroupContainerId;

		// Token: 0x04001994 RID: 6548
		[NonSerialized]
		private ADObjectId stampGroupContainerId;

		// Token: 0x04001995 RID: 6549
		[NonSerialized]
		private ADObjectId routingGroupId;
	}
}
