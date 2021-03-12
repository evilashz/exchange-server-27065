using System;
using System.Collections.Generic;
using System.DirectoryServices.Protocols;
using System.Globalization;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Diagnostics.Components.Mserve;
using Microsoft.Exchange.EdgeSync.Common;
using Microsoft.Exchange.EdgeSync.Common.Internal;
using Microsoft.Exchange.EdgeSync.Logging;

namespace Microsoft.Exchange.EdgeSync.Mserve
{
	// Token: 0x02000035 RID: 53
	internal class MserveSynchronizationProvider : SynchronizationProvider
	{
		// Token: 0x1700007D RID: 125
		// (get) Token: 0x06000243 RID: 579 RVA: 0x0000F3B6 File Offset: 0x0000D5B6
		// (set) Token: 0x06000244 RID: 580 RVA: 0x0000F3BD File Offset: 0x0000D5BD
		public static int PartnerId
		{
			get
			{
				return MserveSynchronizationProvider.partnerId;
			}
			set
			{
				MserveSynchronizationProvider.partnerId = value;
			}
		}

		// Token: 0x1700007E RID: 126
		// (get) Token: 0x06000245 RID: 581 RVA: 0x0000F3C5 File Offset: 0x0000D5C5
		public override string Identity
		{
			get
			{
				return this.identity;
			}
		}

		// Token: 0x1700007F RID: 127
		// (get) Token: 0x06000246 RID: 582 RVA: 0x0000F3CD File Offset: 0x0000D5CD
		public override int LeaseLockTryCount
		{
			get
			{
				return 1;
			}
		}

		// Token: 0x17000080 RID: 128
		// (get) Token: 0x06000247 RID: 583 RVA: 0x0000F3D0 File Offset: 0x0000D5D0
		public override List<TargetServerConfig> TargetServerConfigs
		{
			get
			{
				return this.targetServerConfigs;
			}
		}

		// Token: 0x17000081 RID: 129
		// (get) Token: 0x06000248 RID: 584 RVA: 0x0000F3D8 File Offset: 0x0000D5D8
		public override EnhancedTimeSpan RecipientSyncInterval
		{
			get
			{
				return EdgeSyncSvc.EdgeSync.Config.ServiceConfig.RecipientSyncInterval;
			}
		}

		// Token: 0x17000082 RID: 130
		// (get) Token: 0x06000249 RID: 585 RVA: 0x0000F3EE File Offset: 0x0000D5EE
		public override EnhancedTimeSpan ConfigurationSyncInterval
		{
			get
			{
				return EdgeSyncSvc.EdgeSync.Config.ServiceConfig.ConfigurationSyncInterval;
			}
		}

		// Token: 0x0600024A RID: 586 RVA: 0x0000F404 File Offset: 0x0000D604
		public override void Initialize(EdgeSyncConnector connector)
		{
			this.identity = ((EdgeSyncMservConnector)connector).Name;
			this.targetServerConfigs = new List<TargetServerConfig>();
			this.targetServerConfigs.Add(new MserveTargetServerConfig(((EdgeSyncMservConnector)connector).Name, ((EdgeSyncMservConnector)connector).ProvisionUrl.AbsoluteUri, ((EdgeSyncMservConnector)connector).SettingUrl.AbsoluteUri, ((EdgeSyncMservConnector)connector).RemoteCertificate, ((EdgeSyncMservConnector)connector).PrimaryLeaseLocation, ((EdgeSyncMservConnector)connector).BackupLeaseLocation));
			if (MserveSynchronizationProvider.partnerId == -1)
			{
				MserveSynchronizationProvider.LoadPartnerId();
			}
			if (string.IsNullOrEmpty(MserveSynchronizationProvider.rootDomainLostAndFoundContainerDN))
			{
				MserveSynchronizationProvider.LoadRootDomainLostAndFoundContainerDN();
			}
		}

		// Token: 0x0600024B RID: 587 RVA: 0x0000F4E0 File Offset: 0x0000D6E0
		private static void LoadRootDomainLostAndFoundContainerDN()
		{
			ADOperationResult adoperationResult = ADNotificationAdapter.TryRunADOperation(delegate()
			{
				string rootDomainNamingContextFromCurrentReadConnection = MserveSynchronizationProvider.ConfigSession.GetRootDomainNamingContextFromCurrentReadConnection();
				LostAndFound lostAndFound = MserveSynchronizationProvider.ConfigSession.ResolveWellKnownGuid<LostAndFound>(WellKnownGuid.LostAndFoundContainerWkGuid, rootDomainNamingContextFromCurrentReadConnection);
				if (lostAndFound != null)
				{
					MserveSynchronizationProvider.rootDomainLostAndFoundContainerDN = lostAndFound.DistinguishedName;
				}
			}, 3);
			if (!adoperationResult.Succeeded)
			{
				throw new ExDirectoryException("Failed to get rootDomainLostAndFoundContainerDN because of AD exception.", adoperationResult.Exception);
			}
			if (string.IsNullOrEmpty(MserveSynchronizationProvider.rootDomainLostAndFoundContainerDN))
			{
				throw new ExDirectoryException("Failed to get rootDomainLostAndFoundContainerDN because its value is null.", null);
			}
		}

		// Token: 0x0600024C RID: 588 RVA: 0x0000F578 File Offset: 0x0000D778
		private static void LoadPartnerId()
		{
			ADSite localSite = null;
			ADOperationResult adoperationResult = ADOperationResult.Success;
			adoperationResult = ADNotificationAdapter.TryRunADOperation(delegate()
			{
				localSite = MserveSynchronizationProvider.ConfigSession.GetLocalSite();
				if (localSite == null)
				{
					throw new ExDirectoryException(Strings.CannotGetLocalSite, null);
				}
			}, 3);
			if (!adoperationResult.Succeeded)
			{
				throw new ExDirectoryException(Strings.CannotGetLocalSite, adoperationResult.Exception);
			}
			MserveSynchronizationProvider.partnerId = localSite.PartnerId;
		}

		// Token: 0x0600024D RID: 589 RVA: 0x0000F5DC File Offset: 0x0000D7DC
		public override List<TypeSynchronizer> CreateTypeSynchronizer(SyncTreeType type)
		{
			List<TypeSynchronizer> list = new List<TypeSynchronizer>();
			if (type == SyncTreeType.Recipients)
			{
				list.Add(new TypeSynchronizer(new Filter(MserveSynchronizationProvider.LoadAndFilter), new PreDecorate(MserveSynchronizationProvider.PreDecorate), null, null, null, null, "Mserve hosted recipients", Schema.Query.QueryAllHostedSmtpRecipients, null, SearchScope.Subtree, MserveSynchronizationProvider.ReplicationAttributes, null, true));
				ExTraceGlobals.ProviderTracer.TraceDebug((long)this.GetHashCode(), "Mserve provider created typeSynchronizer");
			}
			return list;
		}

		// Token: 0x0600024E RID: 590 RVA: 0x0000F644 File Offset: 0x0000D844
		public override TargetConnection CreateTargetConnection(TargetServerConfig targetServerConfig, SyncTreeType type, TestShutdownAndLeaseDelegate testShutdownAndLease, EdgeSyncLogSession logSession)
		{
			return new MserveTargetConnection(EdgeSyncSvc.EdgeSync.Topology.LocalServer.VersionNumber, targetServerConfig as MserveTargetServerConfig, this.RecipientSyncInterval, testShutdownAndLease, logSession);
		}

		// Token: 0x0600024F RID: 591 RVA: 0x0000F670 File Offset: 0x0000D870
		private static bool PreDecorate(ExSearchResultEntry entry, Connection sourceConnection, TargetConnection targetConnection, object state)
		{
			if (!entry.IsDeleted)
			{
				foreach (string text in MserveSynchronizationProvider.AddressAttributeNames)
				{
					if (entry.Attributes.ContainsKey(text))
					{
						DirectoryAttribute directoryAttribute = entry.Attributes[text];
						if (text.Equals("msExchArchiveGUID", StringComparison.OrdinalIgnoreCase))
						{
							if (entry.Attributes.ContainsKey("msExchArchiveGUID"))
							{
								DirectoryAttribute value = null;
								if (MserveSynchronizationProvider.TryTransformAchiveGuidToSmtpAddress(directoryAttribute, out value))
								{
									entry.Attributes["ArchiveAddress"] = value;
								}
								else
								{
									entry.Attributes["ArchiveAddress"] = new DirectoryAttribute("ArchiveAddress", MserveSynchronizationProvider.EmptyList);
								}
							}
							entry.Attributes.Remove("msExchArchiveGUID");
						}
						else
						{
							List<string> list = new List<string>();
							for (int j = 0; j < directoryAttribute.Count; j++)
							{
								string text2 = directoryAttribute[j] as string;
								if (text2.StartsWith("smtp:", StringComparison.OrdinalIgnoreCase) || text2.StartsWith("meum:", StringComparison.OrdinalIgnoreCase))
								{
									list.Add(text2);
								}
							}
							entry.Attributes[text] = new DirectoryAttribute(directoryAttribute.Name, list.ToArray());
						}
					}
				}
			}
			else
			{
				Dictionary<string, DirectoryAttribute> dictionary = sourceConnection.ReadObjectAttribute(entry.DistinguishedName, true, new string[]
				{
					"msExchExternalSyncState"
				});
				foreach (string key in dictionary.Keys)
				{
					entry.Attributes.Add(key, dictionary[key]);
				}
			}
			return true;
		}

		// Token: 0x06000250 RID: 592 RVA: 0x0000F828 File Offset: 0x0000DA28
		private static bool TryTransformAchiveGuidToSmtpAddress(DirectoryAttribute archiveGuidAttribute, out DirectoryAttribute archiveAddressAttribute)
		{
			archiveAddressAttribute = null;
			if (archiveGuidAttribute == null || archiveGuidAttribute.Count < 1)
			{
				return false;
			}
			byte[] array = archiveGuidAttribute[0] as byte[];
			if (array != null)
			{
				Guid guid = new Guid(array);
				string text = guid.ToString() + "@archive.exchangelabs.com";
				archiveAddressAttribute = new DirectoryAttribute("ArchiveAddress", new string[]
				{
					text
				});
				return true;
			}
			return false;
		}

		// Token: 0x06000251 RID: 593 RVA: 0x0000F894 File Offset: 0x0000DA94
		private static FilterResult LoadAndFilter(ExSearchResultEntry entry, Connection sourceConnection, TargetConnection targetConnection)
		{
			if (entry.DistinguishedName.EndsWith(MserveSynchronizationProvider.rootDomainLostAndFoundContainerDN, StringComparison.OrdinalIgnoreCase))
			{
				return FilterResult.Skip;
			}
			ExSearchResultEntry exSearchResultEntry = sourceConnection.ReadObjectEntry(entry.DistinguishedName, MserveSynchronizationProvider.RequiredAttributes);
			if (exSearchResultEntry == null)
			{
				return FilterResult.Skip;
			}
			RecipientTypeDetails recipientTypeDetails = RecipientTypeDetails.None;
			DirectoryAttribute directoryAttribute = null;
			if (exSearchResultEntry.Attributes.TryGetValue("msExchRecipientTypeDetails", out directoryAttribute) && directoryAttribute != null && directoryAttribute.Count > 0)
			{
				try
				{
					recipientTypeDetails = (RecipientTypeDetails)Enum.Parse(typeof(RecipientTypeDetails), directoryAttribute[0] as string, true);
				}
				catch (ArgumentException)
				{
				}
				catch (OverflowException)
				{
				}
			}
			if (recipientTypeDetails == RecipientTypeDetails.MailboxPlan || recipientTypeDetails == RecipientTypeDetails.RoleGroup)
			{
				return FilterResult.Skip;
			}
			if (!entry.Attributes.ContainsKey("msExchExternalSyncState") && exSearchResultEntry.Attributes.ContainsKey("msExchExternalSyncState"))
			{
				entry.Attributes.Add("msExchExternalSyncState", exSearchResultEntry.Attributes["msExchExternalSyncState"]);
			}
			if (entry.Attributes.ContainsKey("msExchTransportRecipientSettingsFlags"))
			{
				if (!entry.Attributes.ContainsKey("proxyAddresses") && exSearchResultEntry.Attributes.ContainsKey("proxyAddresses"))
				{
					entry.Attributes.Add("proxyAddresses", exSearchResultEntry.Attributes["proxyAddresses"]);
				}
				if (!entry.Attributes.ContainsKey("msExchSignupAddresses") && exSearchResultEntry.Attributes.ContainsKey("msExchSignupAddresses"))
				{
					entry.Attributes.Add("msExchSignupAddresses", exSearchResultEntry.Attributes["msExchSignupAddresses"]);
				}
				if (!entry.Attributes.ContainsKey("msExchUMAddresses") && exSearchResultEntry.Attributes.ContainsKey("msExchUMAddresses"))
				{
					entry.Attributes.Add("msExchUMAddresses", exSearchResultEntry.Attributes["msExchUMAddresses"]);
				}
				if (!entry.Attributes.ContainsKey("msExchArchiveGUID") && exSearchResultEntry.Attributes.ContainsKey("msExchArchiveGUID"))
				{
					entry.Attributes.Add("msExchArchiveGUID", exSearchResultEntry.Attributes["msExchArchiveGUID"]);
				}
			}
			if (exSearchResultEntry.Attributes.ContainsKey("msExchTransportRecipientSettingsFlags"))
			{
				entry.Attributes["msExchTransportRecipientSettingsFlags"] = exSearchResultEntry.Attributes["msExchTransportRecipientSettingsFlags"];
			}
			if (exSearchResultEntry.Attributes.ContainsKey("msExchCU"))
			{
				entry.Attributes["msExchCU"] = exSearchResultEntry.Attributes["msExchCU"];
			}
			if (exSearchResultEntry.Attributes.ContainsKey("mailNickname"))
			{
				entry.Attributes["mailNickname"] = exSearchResultEntry.Attributes["mailNickname"];
			}
			if (exSearchResultEntry.Attributes.ContainsKey("msExchHomeServerName"))
			{
				entry.Attributes["msExchHomeServerName"] = exSearchResultEntry.Attributes["msExchHomeServerName"];
			}
			bool flag = false;
			int num;
			if (entry.Attributes.ContainsKey("msExchTransportRecipientSettingsFlags") && entry.Attributes["msExchTransportRecipientSettingsFlags"].Count != 0 && int.TryParse((string)entry.Attributes["msExchTransportRecipientSettingsFlags"][0], NumberStyles.Number, CultureInfo.InvariantCulture, out num) && (num & 8) != 0)
			{
				bool flag2 = (num & 64) != 0;
				foreach (string text in MserveSynchronizationProvider.AddressAttributeNames)
				{
					if (text.Equals("msExchArchiveGUID"))
					{
						if (!flag2)
						{
							entry.Attributes["msExchArchiveGUID"] = new DirectoryAttribute("msExchArchiveGUID", MserveSynchronizationProvider.EmptyList);
						}
					}
					else if (entry.Attributes.ContainsKey(text))
					{
						DirectoryAttribute directoryAttribute2 = entry.Attributes[text];
						entry.Attributes[text] = new DirectoryAttribute(directoryAttribute2.Name, MserveSynchronizationProvider.EmptyList);
						flag = text.Equals("proxyAddresses", StringComparison.OrdinalIgnoreCase);
					}
				}
			}
			if (!flag)
			{
				MserveTargetConnection mserveTargetConnection = targetConnection as MserveTargetConnection;
				if (targetConnection == null)
				{
					throw new InvalidOperationException("targetConnection is not the type of MserveTargetConnection");
				}
				mserveTargetConnection.FilterSmtpProxyAddressesBasedOnTenantSetting(entry, recipientTypeDetails);
			}
			return FilterResult.None;
		}

		// Token: 0x040000F1 RID: 241
		private const int LeaseLockTryCountInternal = 1;

		// Token: 0x040000F2 RID: 242
		internal static readonly string[] EmptyList = new string[0];

		// Token: 0x040000F3 RID: 243
		private static readonly string[] ReplicationAttributes = new string[]
		{
			"proxyAddresses",
			"msExchSignupAddresses",
			"msExchTransportRecipientSettingsFlags",
			"msExchUMAddresses",
			"msExchArchiveGUID"
		};

		// Token: 0x040000F4 RID: 244
		private static readonly string[] RequiredAttributes = new string[]
		{
			"proxyAddresses",
			"msExchSignupAddresses",
			"msExchExternalSyncState",
			"msExchTransportRecipientSettingsFlags",
			"targetAddress",
			"msExchArchiveGUID",
			"msExchRecipientTypeDetails",
			"msExchCU",
			"mailNickname",
			"msExchHomeServerName"
		};

		// Token: 0x040000F5 RID: 245
		private static readonly string[] AddressAttributeNames = new string[]
		{
			"proxyAddresses",
			"msExchSignupAddresses",
			"msExchUMAddresses",
			"msExchArchiveGUID"
		};

		// Token: 0x040000F6 RID: 246
		private static readonly ITopologyConfigurationSession ConfigSession = DirectorySessionFactory.Default.CreateTopologyConfigurationSession(ConsistencyMode.PartiallyConsistent, ADSessionSettings.FromRootOrgScopeSet(), 91, "ConfigSession", "f:\\15.00.1497\\sources\\dev\\EdgeSync\\src\\Mserve\\MserveSynchronizationProvider.cs");

		// Token: 0x040000F7 RID: 247
		private static int partnerId = -1;

		// Token: 0x040000F8 RID: 248
		private static string rootDomainLostAndFoundContainerDN;

		// Token: 0x040000F9 RID: 249
		private List<TargetServerConfig> targetServerConfigs;

		// Token: 0x040000FA RID: 250
		private string identity;
	}
}
