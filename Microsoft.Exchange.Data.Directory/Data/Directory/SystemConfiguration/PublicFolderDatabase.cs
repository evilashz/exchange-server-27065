using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using Microsoft.Exchange.Collections;
using Microsoft.Exchange.Data.Directory.ExchangeTopology;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x02000545 RID: 1349
	[ObjectScope(ConfigScopes.Database)]
	[Serializable]
	public sealed class PublicFolderDatabase : Database
	{
		// Token: 0x1700133C RID: 4924
		// (get) Token: 0x06003C58 RID: 15448 RVA: 0x000E6D95 File Offset: 0x000E4F95
		internal override ADObjectSchema Schema
		{
			get
			{
				return PublicFolderDatabase.schema;
			}
		}

		// Token: 0x1700133D RID: 4925
		// (get) Token: 0x06003C59 RID: 15449 RVA: 0x000E6D9C File Offset: 0x000E4F9C
		internal override string MostDerivedObjectClass
		{
			get
			{
				return PublicFolderDatabase.MostDerivedClass;
			}
		}

		// Token: 0x1700133E RID: 4926
		// (get) Token: 0x06003C5A RID: 15450 RVA: 0x000E6DA3 File Offset: 0x000E4FA3
		internal override QueryFilter ImplicitFilter
		{
			get
			{
				return new ComparisonFilter(ComparisonOperator.Equal, ADObjectSchema.ObjectCategory, this.MostDerivedObjectClass);
			}
		}

		// Token: 0x06003C5B RID: 15451 RVA: 0x000E6DB8 File Offset: 0x000E4FB8
		protected override void ValidateWrite(List<ValidationError> errors)
		{
			base.ValidateWrite(errors);
			errors.AddRange(Database.ValidateAscendingQuotas(this.propertyBag, new ProviderPropertyDefinition[]
			{
				DatabaseSchema.IssueWarningQuota,
				PublicFolderDatabaseSchema.ProhibitPostQuota
			}, this.Identity));
			errors.AddRange(Database.ValidateAscendingQuotas(this.propertyBag, new ProviderPropertyDefinition[]
			{
				PublicFolderDatabaseSchema.MaxItemSize,
				PublicFolderDatabaseSchema.ProhibitPostQuota
			}, this.Identity));
			if (!this.UseCustomReferralServerList && this.CustomReferralServerList.Count != 0)
			{
				this.CustomReferralServerList.Clear();
			}
			foreach (ServerCostPair serverCostPair in this.CustomReferralServerList)
			{
				if (string.IsNullOrEmpty(serverCostPair.ServerName))
				{
					errors.Add(new ObjectValidationError(DirectoryStrings.PublicFolderReferralServerNotExisting(serverCostPair.ServerGuid.ToString()), this.Identity, string.Empty));
				}
			}
			if (this.CustomReferralServerList.Count > 1)
			{
				for (int i = 0; i < this.CustomReferralServerList.Count - 1; i++)
				{
					for (int j = i + 1; j < this.CustomReferralServerList.Count; j++)
					{
						if (this.CustomReferralServerList[i].ServerGuid == this.CustomReferralServerList[j].ServerGuid && this.CustomReferralServerList[i].Cost != this.CustomReferralServerList[j].Cost)
						{
							errors.Add(new ObjectValidationError(DirectoryStrings.ErrorPublicFolderReferralConflict(this.CustomReferralServerList[i].ToString(), this.CustomReferralServerList[j].ToString()), this.Identity, string.Empty));
							break;
						}
					}
				}
			}
		}

		// Token: 0x1700133F RID: 4927
		// (get) Token: 0x06003C5C RID: 15452 RVA: 0x000E6FA4 File Offset: 0x000E51A4
		// (set) Token: 0x06003C5D RID: 15453 RVA: 0x000E6FB6 File Offset: 0x000E51B6
		public string Alias
		{
			get
			{
				return (string)this[PublicFolderDatabaseSchema.Alias];
			}
			internal set
			{
				this[PublicFolderDatabaseSchema.Alias] = value;
			}
		}

		// Token: 0x17001340 RID: 4928
		// (get) Token: 0x06003C5E RID: 15454 RVA: 0x000E6FC4 File Offset: 0x000E51C4
		// (set) Token: 0x06003C5F RID: 15455 RVA: 0x000E6FD6 File Offset: 0x000E51D6
		public bool FirstInstance
		{
			get
			{
				return (bool)this[PublicFolderDatabaseSchema.FirstInstance];
			}
			internal set
			{
				this[PublicFolderDatabaseSchema.FirstInstance] = value;
			}
		}

		// Token: 0x17001341 RID: 4929
		// (get) Token: 0x06003C60 RID: 15456 RVA: 0x000E6FE9 File Offset: 0x000E51E9
		// (set) Token: 0x06003C61 RID: 15457 RVA: 0x000E6FFB File Offset: 0x000E51FB
		internal ADObjectId HomeMta
		{
			get
			{
				return (ADObjectId)this[PublicFolderDatabaseSchema.HomeMta];
			}
			set
			{
				this[PublicFolderDatabaseSchema.HomeMta] = value;
			}
		}

		// Token: 0x17001342 RID: 4930
		// (get) Token: 0x06003C62 RID: 15458 RVA: 0x000E7009 File Offset: 0x000E5209
		// (set) Token: 0x06003C63 RID: 15459 RVA: 0x000E701B File Offset: 0x000E521B
		[Parameter(Mandatory = false)]
		public Unlimited<ByteQuantifiedSize> MaxItemSize
		{
			get
			{
				return (Unlimited<ByteQuantifiedSize>)this[PublicFolderDatabaseSchema.MaxItemSize];
			}
			set
			{
				this[PublicFolderDatabaseSchema.MaxItemSize] = value;
			}
		}

		// Token: 0x17001343 RID: 4931
		// (get) Token: 0x06003C64 RID: 15460 RVA: 0x000E702E File Offset: 0x000E522E
		// (set) Token: 0x06003C65 RID: 15461 RVA: 0x000E7040 File Offset: 0x000E5240
		[Parameter(Mandatory = false)]
		public Unlimited<EnhancedTimeSpan> ItemRetentionPeriod
		{
			get
			{
				return (Unlimited<EnhancedTimeSpan>)this[PublicFolderDatabaseSchema.ItemRetentionPeriod];
			}
			set
			{
				this[PublicFolderDatabaseSchema.ItemRetentionPeriod] = value;
			}
		}

		// Token: 0x17001344 RID: 4932
		// (get) Token: 0x06003C66 RID: 15462 RVA: 0x000E7053 File Offset: 0x000E5253
		// (set) Token: 0x06003C67 RID: 15463 RVA: 0x000E7065 File Offset: 0x000E5265
		[Parameter(Mandatory = false)]
		public uint ReplicationPeriod
		{
			get
			{
				return (uint)this[PublicFolderDatabaseSchema.ReplicationPeriod];
			}
			set
			{
				this[PublicFolderDatabaseSchema.ReplicationPeriod] = value;
			}
		}

		// Token: 0x17001345 RID: 4933
		// (get) Token: 0x06003C68 RID: 15464 RVA: 0x000E7078 File Offset: 0x000E5278
		// (set) Token: 0x06003C69 RID: 15465 RVA: 0x000E708A File Offset: 0x000E528A
		[Parameter(Mandatory = false)]
		public Unlimited<ByteQuantifiedSize> ProhibitPostQuota
		{
			get
			{
				return (Unlimited<ByteQuantifiedSize>)this[PublicFolderDatabaseSchema.ProhibitPostQuota];
			}
			set
			{
				this[PublicFolderDatabaseSchema.ProhibitPostQuota] = value;
			}
		}

		// Token: 0x17001346 RID: 4934
		// (get) Token: 0x06003C6A RID: 15466 RVA: 0x000E709D File Offset: 0x000E529D
		// (set) Token: 0x06003C6B RID: 15467 RVA: 0x000E70AF File Offset: 0x000E52AF
		public ADObjectId PublicFolderHierarchy
		{
			get
			{
				return (ADObjectId)this[PublicFolderDatabaseSchema.PublicFolderHierarchy];
			}
			internal set
			{
				this[PublicFolderDatabaseSchema.PublicFolderHierarchy] = value;
			}
		}

		// Token: 0x17001347 RID: 4935
		// (get) Token: 0x06003C6C RID: 15468 RVA: 0x000E70BD File Offset: 0x000E52BD
		public MultiValuedProperty<ADObjectId> Organizations
		{
			get
			{
				return (MultiValuedProperty<ADObjectId>)this[PublicFolderDatabaseSchema.Organizations];
			}
		}

		// Token: 0x17001348 RID: 4936
		// (get) Token: 0x06003C6D RID: 15469 RVA: 0x000E70CF File Offset: 0x000E52CF
		// (set) Token: 0x06003C6E RID: 15470 RVA: 0x000E70E1 File Offset: 0x000E52E1
		[Parameter(Mandatory = false)]
		public ByteQuantifiedSize ReplicationMessageSize
		{
			get
			{
				return (ByteQuantifiedSize)this[PublicFolderDatabaseSchema.ReplicationMessageSize];
			}
			set
			{
				this[PublicFolderDatabaseSchema.ReplicationMessageSize] = value;
			}
		}

		// Token: 0x17001349 RID: 4937
		// (get) Token: 0x06003C6F RID: 15471 RVA: 0x000E70F4 File Offset: 0x000E52F4
		internal ScheduleMode ReplicationMode
		{
			get
			{
				return (ScheduleMode)this[PublicFolderDatabaseSchema.ReplicationMode];
			}
		}

		// Token: 0x1700134A RID: 4938
		// (get) Token: 0x06003C70 RID: 15472 RVA: 0x000E7106 File Offset: 0x000E5306
		// (set) Token: 0x06003C71 RID: 15473 RVA: 0x000E7118 File Offset: 0x000E5318
		[Parameter(Mandatory = false)]
		public Schedule ReplicationSchedule
		{
			get
			{
				return (Schedule)this[PublicFolderDatabaseSchema.ReplicationSchedule];
			}
			set
			{
				this[PublicFolderDatabaseSchema.ReplicationSchedule] = value;
			}
		}

		// Token: 0x1700134B RID: 4939
		// (get) Token: 0x06003C72 RID: 15474 RVA: 0x000E7126 File Offset: 0x000E5326
		// (set) Token: 0x06003C73 RID: 15475 RVA: 0x000E7138 File Offset: 0x000E5338
		public bool UseCustomReferralServerList
		{
			get
			{
				return (bool)this[PublicFolderDatabaseSchema.UseCustomReferralServerList];
			}
			set
			{
				this[PublicFolderDatabaseSchema.UseCustomReferralServerList] = value;
			}
		}

		// Token: 0x1700134C RID: 4940
		// (get) Token: 0x06003C74 RID: 15476 RVA: 0x000E714B File Offset: 0x000E534B
		// (set) Token: 0x06003C75 RID: 15477 RVA: 0x000E715D File Offset: 0x000E535D
		public MultiValuedProperty<ServerCostPair> CustomReferralServerList
		{
			get
			{
				return (MultiValuedProperty<ServerCostPair>)this[PublicFolderDatabaseSchema.CustomReferralServerList];
			}
			set
			{
				this[PublicFolderDatabaseSchema.CustomReferralServerList] = value;
			}
		}

		// Token: 0x06003C76 RID: 15478 RVA: 0x000E716C File Offset: 0x000E536C
		internal override void StampPersistableDefaultValues()
		{
			if (!base.IsModified(PublicFolderDatabaseSchema.MaxItemSize))
			{
				this.MaxItemSize = new Unlimited<ByteQuantifiedSize>(ByteQuantifiedSize.FromKB(10240UL));
			}
			if (!base.IsModified(PublicFolderDatabaseSchema.ProhibitPostQuota))
			{
				this.ProhibitPostQuota = new Unlimited<ByteQuantifiedSize>(ByteQuantifiedSize.FromGB(2UL));
			}
			base.StampPersistableDefaultValues();
		}

		// Token: 0x06003C77 RID: 15479 RVA: 0x000E71C4 File Offset: 0x000E53C4
		internal static object ReplicationScheduleGetter(IPropertyBag propertyBag)
		{
			switch ((ScheduleMode)propertyBag[PublicFolderDatabaseSchema.ReplicationMode])
			{
			case ScheduleMode.Never:
				return Schedule.Never;
			case ScheduleMode.Always:
				return Schedule.Always;
			}
			return propertyBag[PublicFolderDatabaseSchema.ReplicationScheduleBitmaps];
		}

		// Token: 0x06003C78 RID: 15480 RVA: 0x000E720D File Offset: 0x000E540D
		internal static void ReplicationScheduleSetter(object value, IPropertyBag propertyBag)
		{
			if (value == null)
			{
				value = Schedule.Never;
			}
			propertyBag[PublicFolderDatabaseSchema.ReplicationMode] = ((Schedule)value).Mode;
			propertyBag[PublicFolderDatabaseSchema.ReplicationScheduleBitmaps] = value;
		}

		// Token: 0x06003C79 RID: 15481 RVA: 0x000E7240 File Offset: 0x000E5440
		internal static ADObjectId FindClosestPublicFolderDatabase(IConfigDataProvider scSession, ADObjectId sourceServerId)
		{
			PublicFolderDatabase publicFolderDatabase = PublicFolderDatabase.FindClosestPublicFolderDatabase(scSession, sourceServerId, null);
			if (publicFolderDatabase == null)
			{
				return null;
			}
			return (ADObjectId)publicFolderDatabase.Identity;
		}

		// Token: 0x06003C7A RID: 15482 RVA: 0x000E7268 File Offset: 0x000E5468
		internal static PublicFolderDatabase FindClosestPublicFolderDatabase(IConfigDataProvider scSession, ADObjectId sourceServerId, Func<PublicFolderDatabase, bool> candidateMatcher)
		{
			if (scSession == null)
			{
				throw new ArgumentNullException("scSession");
			}
			PublicFolderDatabase result = null;
			PublicFolderDatabase[] array = (PublicFolderDatabase[])scSession.Find<PublicFolderDatabase>(null, null, true, null);
			if (candidateMatcher != null && 0 < array.Length)
			{
				array = array.Where(candidateMatcher).ToArray<PublicFolderDatabase>();
			}
			if (1 == array.Length)
			{
				result = array[0];
			}
			else if (array.Length > 1)
			{
				ExchangeTopology exchangeTopology = ExchangeTopology.Discover(null, ExchangeTopologyScope.ADAndExchangeServerAndSiteTopology);
				TopologySite topologySite = null;
				TopologySite topologySite2 = null;
				if (sourceServerId == null)
				{
					topologySite = exchangeTopology.LocalSite;
				}
				else
				{
					string text = null;
					Server server = (Server)scSession.Read<Server>(sourceServerId);
					if (server != null)
					{
						text = server.Fqdn;
					}
					if (!string.IsNullOrEmpty(text))
					{
						topologySite = exchangeTopology.SiteFromADServer(text);
					}
				}
				if (topologySite != null)
				{
					ReadOnlyCollection<TopologySite> allTopologySites = exchangeTopology.AllTopologySites;
					ReadOnlyCollection<TopologySiteLink> allTopologySiteLinks = exchangeTopology.AllTopologySiteLinks;
					ReadOnlyCollection<TopologyServer> allTopologyServers = exchangeTopology.AllTopologyServers;
					Dictionary<TopologyServer, TopologySite> dictionary = new Dictionary<TopologyServer, TopologySite>();
					foreach (TopologyServer topologyServer in allTopologyServers)
					{
						if (topologyServer.TopologySite != null)
						{
							foreach (TopologySite topologySite3 in allTopologySites)
							{
								if (topologySite3.DistinguishedName.Equals(topologyServer.TopologySite.DistinguishedName, StringComparison.OrdinalIgnoreCase))
								{
									dictionary[topologyServer] = topologySite3;
									break;
								}
							}
						}
					}
					Dictionary<TopologySite, PublicFolderDatabase> dictionary2 = new Dictionary<TopologySite, PublicFolderDatabase>();
					List<TopologySite> list = new List<TopologySite>();
					foreach (PublicFolderDatabase publicFolderDatabase in array)
					{
						foreach (KeyValuePair<TopologyServer, TopologySite> keyValuePair in dictionary)
						{
							if (keyValuePair.Key.DistinguishedName.Equals(publicFolderDatabase.Server.DistinguishedName, StringComparison.OrdinalIgnoreCase))
							{
								if (!dictionary2.ContainsKey(keyValuePair.Value))
								{
									dictionary2[keyValuePair.Value] = publicFolderDatabase;
									list.Add(keyValuePair.Value);
									break;
								}
								if (keyValuePair.Key.IsExchange2007OrLater)
								{
									dictionary2[keyValuePair.Value] = publicFolderDatabase;
									break;
								}
								break;
							}
						}
					}
					topologySite2 = exchangeTopology.FindClosestDestinationSite(topologySite, list);
					if (topologySite2 != null)
					{
						result = dictionary2[topologySite2];
					}
				}
				if (topologySite2 == null)
				{
					result = array[0];
				}
			}
			return result;
		}

		// Token: 0x06003C7B RID: 15483 RVA: 0x000E74D8 File Offset: 0x000E56D8
		public static string CalculateServerLegacyDNFromPfdbLegacyDN(string pfdbLegacyDN)
		{
			if (string.IsNullOrEmpty(pfdbLegacyDN))
			{
				throw new ArgumentNullException("pfdbLegacyDN");
			}
			return Database.GetRcaLegacyDNFromDatabaseLegacyDN(LegacyDN.Parse(pfdbLegacyDN)).ToString();
		}

		// Token: 0x040028E2 RID: 10466
		internal const int MaxPublicFolderDatabaseCount = 10000;

		// Token: 0x040028E3 RID: 10467
		private static PublicFolderDatabaseSchema schema = ObjectSchema.GetInstance<PublicFolderDatabaseSchema>();

		// Token: 0x040028E4 RID: 10468
		internal static readonly string MostDerivedClass = "msExchPublicMDB";
	}
}
