using System;
using System.Collections.Generic;
using System.Threading;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.IsMemberOfProvider;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Transport;

namespace Microsoft.Exchange.InfoWorker.Common.OrganizationConfiguration
{
	// Token: 0x02000159 RID: 345
	internal class GroupsConfiguration
	{
		// Token: 0x06000985 RID: 2437 RVA: 0x000288B2 File Offset: 0x00026AB2
		public GroupsConfiguration(OrganizationId organizationId)
		{
			this.organizationId = organizationId;
		}

		// Token: 0x06000986 RID: 2438 RVA: 0x000288C1 File Offset: 0x00026AC1
		public void Initialize()
		{
		}

		// Token: 0x06000987 RID: 2439 RVA: 0x000288C4 File Offset: 0x00026AC4
		public GroupConfiguration GetGroupInformation(string group)
		{
			IRecipientSession tenantOrRootOrgRecipientSession = DirectorySessionFactory.Default.GetTenantOrRootOrgRecipientSession(ConsistencyMode.IgnoreInvalid, ADSessionSettings.FromOrganizationIdWithoutRbacScopesServiceOnly(this.organizationId), 58, "GetGroupInformation", "f:\\15.00.1497\\sources\\dev\\infoworker\\src\\common\\OrganizationConfiguration\\GroupsConfiguration.cs");
			return this.GetGroupInformation(tenantOrRootOrgRecipientSession, group);
		}

		// Token: 0x06000988 RID: 2440 RVA: 0x000288FC File Offset: 0x00026AFC
		public GroupConfiguration GetGroupInformation(IRecipientSession session, string group)
		{
			return GroupsConfiguration.groupsResolver.Value.GetGroupInfo(session, (RoutingAddress)group);
		}

		// Token: 0x06000989 RID: 2441 RVA: 0x00028914 File Offset: 0x00026B14
		public GroupConfiguration GetGroupInformation(IRecipientSession session, Guid group)
		{
			return GroupsConfiguration.groupsResolver.Value.GetGroupInfo(session, group);
		}

		// Token: 0x0600098A RID: 2442 RVA: 0x00028927 File Offset: 0x00026B27
		public bool IsMemberOf(ADObjectId user, string group)
		{
			return this.IsMemberOf(user.ObjectGuid, (RoutingAddress)group);
		}

		// Token: 0x0600098B RID: 2443 RVA: 0x0002893B File Offset: 0x00026B3B
		public bool IsMemberOf(IRecipientSession session, ADObjectId user, string group)
		{
			return GroupsConfiguration.groupsResolver.Value.IsMemberOf(session, user, (RoutingAddress)group);
		}

		// Token: 0x0600098C RID: 2444 RVA: 0x00028954 File Offset: 0x00026B54
		public bool IsMemberOf(Guid adUserObjectGuid, RoutingAddress group)
		{
			IRecipientSession tenantOrRootOrgRecipientSession = DirectorySessionFactory.Default.GetTenantOrRootOrgRecipientSession(ConsistencyMode.IgnoreInvalid, ADSessionSettings.FromOrganizationIdWithoutRbacScopesServiceOnly(this.organizationId), 120, "IsMemberOf", "f:\\15.00.1497\\sources\\dev\\infoworker\\src\\common\\OrganizationConfiguration\\GroupsConfiguration.cs");
			return this.IsMemberOf(tenantOrRootOrgRecipientSession, adUserObjectGuid, group);
		}

		// Token: 0x0600098D RID: 2445 RVA: 0x0002898D File Offset: 0x00026B8D
		public bool IsMemberOf(IRecipientSession session, Guid adUserObjectGuid, RoutingAddress group)
		{
			return GroupsConfiguration.groupsResolver.Value.IsMemberOf(session, adUserObjectGuid, group);
		}

		// Token: 0x0400074E RID: 1870
		private OrganizationId organizationId;

		// Token: 0x0400074F RID: 1871
		private static Lazy<GroupsConfiguration.GroupsResolver> groupsResolver = new Lazy<GroupsConfiguration.GroupsResolver>(LazyThreadSafetyMode.ExecutionAndPublication);

		// Token: 0x0200015A RID: 346
		private class IsMemberOfResolverADAdapterVersionedResolver : IsMemberOfResolverADAdapter<RoutingAddress>.RoutingAddressResolver
		{
			// Token: 0x0600098F RID: 2447 RVA: 0x000289AE File Offset: 0x00026BAE
			public IsMemberOfResolverADAdapterVersionedResolver(bool disableDynamicGroups) : base(disableDynamicGroups)
			{
			}

			// Token: 0x06000990 RID: 2448 RVA: 0x000289B8 File Offset: 0x00026BB8
			protected override ExpandedGroup CreateExpandedGroup(ADObject group, List<Guid> memberGroups, List<Guid> memberRecipients)
			{
				return new GroupsConfiguration.ExpandedGroupWithVersion(memberGroups, memberRecipients, group.Id.ObjectGuid, group.WhenChangedUTC.Value);
			}
		}

		// Token: 0x0200015B RID: 347
		internal class ExpandedGroupWithVersion : ExpandedGroup
		{
			// Token: 0x06000991 RID: 2449 RVA: 0x000289E5 File Offset: 0x00026BE5
			public ExpandedGroupWithVersion(List<Guid> memberGroups, List<Guid> memberRecipients, Guid groupGuid, DateTime groupVersion) : base(memberGroups, memberRecipients)
			{
				this.GroupGuid = groupGuid;
				this.GroupVersion = groupVersion;
			}

			// Token: 0x17000261 RID: 609
			// (get) Token: 0x06000992 RID: 2450 RVA: 0x000289FE File Offset: 0x00026BFE
			// (set) Token: 0x06000993 RID: 2451 RVA: 0x00028A06 File Offset: 0x00026C06
			public Guid GroupGuid { get; private set; }

			// Token: 0x17000262 RID: 610
			// (get) Token: 0x06000994 RID: 2452 RVA: 0x00028A0F File Offset: 0x00026C0F
			// (set) Token: 0x06000995 RID: 2453 RVA: 0x00028A17 File Offset: 0x00026C17
			public DateTime GroupVersion { get; private set; }

			// Token: 0x17000263 RID: 611
			// (get) Token: 0x06000996 RID: 2454 RVA: 0x00028A20 File Offset: 0x00026C20
			public override long ItemSize
			{
				get
				{
					return base.ItemSize + 16L + 8L;
				}
			}

			// Token: 0x04000750 RID: 1872
			private const int GuidLength = 16;

			// Token: 0x04000751 RID: 1873
			private const int DateTimeLength = 8;
		}

		// Token: 0x0200015C RID: 348
		private class GroupsResolver : IsMemberOfResolver<RoutingAddress>
		{
			// Token: 0x06000997 RID: 2455 RVA: 0x00028A2F File Offset: 0x00026C2F
			public GroupsResolver() : base(new GroupsConfiguration.GroupsResolver.GroupsResolverConfig(), new IsMemberOfResolverPerformanceCounters("Infoworker"), new GroupsConfiguration.IsMemberOfResolverADAdapterVersionedResolver(true))
			{
			}

			// Token: 0x06000998 RID: 2456 RVA: 0x00028A4C File Offset: 0x00026C4C
			public GroupConfiguration GetGroupInfo(IRecipientSession session, RoutingAddress groupKey)
			{
				base.ThrowIfDisposed();
				if (this.enabled)
				{
					ResolvedGroup value = this.resolvedGroups.GetValue(null, new Tuple<PartitionId, OrganizationId, RoutingAddress>(session.SessionSettings.PartitionId, session.SessionSettings.CurrentOrganizationId, groupKey));
					if (value.GroupGuid != Guid.Empty)
					{
						return this.GetGroupInfo(session, value.GroupGuid);
					}
				}
				return null;
			}

			// Token: 0x06000999 RID: 2457 RVA: 0x00028AB4 File Offset: 0x00026CB4
			public GroupConfiguration GetGroupInfo(IRecipientSession session, Guid groupGuid)
			{
				base.ThrowIfDisposed();
				if (this.enabled)
				{
					GroupsConfiguration.ExpandedGroupWithVersion expandedGroupWithVersion = (GroupsConfiguration.ExpandedGroupWithVersion)this.expandedGroups.GetValue(null, new Tuple<PartitionId, OrganizationId, Guid>(session.SessionSettings.PartitionId, session.SessionSettings.CurrentOrganizationId, groupGuid));
					return new GroupConfiguration(expandedGroupWithVersion.GroupGuid, expandedGroupWithVersion.GroupVersion, expandedGroupWithVersion.MemberGroups);
				}
				return null;
			}

			// Token: 0x0200015D RID: 349
			private class GroupsResolverConfig : IsMemberOfResolverConfig
			{
				// Token: 0x17000264 RID: 612
				// (get) Token: 0x0600099A RID: 2458 RVA: 0x00028B16 File Offset: 0x00026D16
				public bool Enabled
				{
					get
					{
						return true;
					}
				}

				// Token: 0x17000265 RID: 613
				// (get) Token: 0x0600099B RID: 2459 RVA: 0x00028B19 File Offset: 0x00026D19
				public long ResolvedGroupsMaxSize
				{
					get
					{
						return 33554432L;
					}
				}

				// Token: 0x17000266 RID: 614
				// (get) Token: 0x0600099C RID: 2460 RVA: 0x00028B21 File Offset: 0x00026D21
				public TimeSpan ResolvedGroupsExpirationInterval
				{
					get
					{
						return TimeSpan.FromHours(3.0);
					}
				}

				// Token: 0x17000267 RID: 615
				// (get) Token: 0x0600099D RID: 2461 RVA: 0x00028B31 File Offset: 0x00026D31
				public TimeSpan ResolvedGroupsCleanupInterval
				{
					get
					{
						return TimeSpan.FromHours(1.0);
					}
				}

				// Token: 0x17000268 RID: 616
				// (get) Token: 0x0600099E RID: 2462 RVA: 0x00028B41 File Offset: 0x00026D41
				public TimeSpan ResolvedGroupsPurgeInterval
				{
					get
					{
						return TimeSpan.FromMinutes(5.0);
					}
				}

				// Token: 0x17000269 RID: 617
				// (get) Token: 0x0600099F RID: 2463 RVA: 0x00028B51 File Offset: 0x00026D51
				public TimeSpan ResolvedGroupsRefreshInterval
				{
					get
					{
						return TimeSpan.FromMinutes(10.0);
					}
				}

				// Token: 0x1700026A RID: 618
				// (get) Token: 0x060009A0 RID: 2464 RVA: 0x00028B61 File Offset: 0x00026D61
				public long ExpandedGroupsMaxSize
				{
					get
					{
						return 134217728L;
					}
				}

				// Token: 0x1700026B RID: 619
				// (get) Token: 0x060009A1 RID: 2465 RVA: 0x00028B69 File Offset: 0x00026D69
				public TimeSpan ExpandedGroupsExpirationInterval
				{
					get
					{
						return TimeSpan.FromHours(3.0);
					}
				}

				// Token: 0x1700026C RID: 620
				// (get) Token: 0x060009A2 RID: 2466 RVA: 0x00028B79 File Offset: 0x00026D79
				public TimeSpan ExpandedGroupsCleanupInterval
				{
					get
					{
						return TimeSpan.FromHours(1.0);
					}
				}

				// Token: 0x1700026D RID: 621
				// (get) Token: 0x060009A3 RID: 2467 RVA: 0x00028B89 File Offset: 0x00026D89
				public TimeSpan ExpandedGroupsPurgeInterval
				{
					get
					{
						return TimeSpan.FromMinutes(5.0);
					}
				}

				// Token: 0x1700026E RID: 622
				// (get) Token: 0x060009A4 RID: 2468 RVA: 0x00028B99 File Offset: 0x00026D99
				public TimeSpan ExpandedGroupsRefreshInterval
				{
					get
					{
						return TimeSpan.FromMinutes(10.0);
					}
				}
			}
		}
	}
}
