using System;
using System.Collections.Generic;
using Microsoft.Exchange.Common;
using Microsoft.Exchange.Common.Cache;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Data.Directory;

namespace Microsoft.Exchange.Data.Directory.IsMemberOfProvider
{
	// Token: 0x020001C5 RID: 453
	internal class IsMemberOfResolver<TGroupKeyType> : DisposeTrackableBase, IIsMemberOfResolver<TGroupKeyType>, IDisposable
	{
		// Token: 0x0600127A RID: 4730 RVA: 0x00059198 File Offset: 0x00057398
		public IsMemberOfResolver(IsMemberOfResolverConfig config, IsMemberOfResolverPerformanceCounters perfCounters, IsMemberOfResolverADAdapter<TGroupKeyType> adAdapter)
		{
			this.enabled = config.Enabled;
			this.adAdapter = adAdapter;
			this.perfCounters = perfCounters;
			long resolvedGroupsMaxSize = config.ResolvedGroupsMaxSize;
			this.resolvedGroups = new AutoRefreshCache<Tuple<PartitionId, OrganizationId, TGroupKeyType>, ResolvedGroup, object>(resolvedGroupsMaxSize, config.ResolvedGroupsExpirationInterval, config.ResolvedGroupsCleanupInterval, config.ResolvedGroupsPurgeInterval, config.ResolvedGroupsRefreshInterval, new DefaultCacheTracer<Tuple<PartitionId, OrganizationId, TGroupKeyType>>(IsMemberOfResolver<Tuple<PartitionId, OrganizationId, TGroupKeyType>>.Tracer, "ResolvedGroups"), perfCounters.GetResolvedGroupsCacheCounters(resolvedGroupsMaxSize), new AutoRefreshCache<Tuple<PartitionId, OrganizationId, TGroupKeyType>, ResolvedGroup, object>.CreateEntryDelegate(this.ResolveGroupInAD));
			long expandedGroupsMaxSize = config.ExpandedGroupsMaxSize;
			this.expandedGroups = new AutoRefreshCache<Tuple<PartitionId, OrganizationId, Guid>, ExpandedGroup, object>(expandedGroupsMaxSize, config.ExpandedGroupsExpirationInterval, config.ExpandedGroupsCleanupInterval, config.ExpandedGroupsPurgeInterval, config.ExpandedGroupsRefreshInterval, new DefaultCacheTracer<Tuple<PartitionId, OrganizationId, Guid>>(IsMemberOfResolver<Tuple<PartitionId, OrganizationId, Guid>>.Tracer, "ExpandedGroups"), perfCounters.GetExpandedGroupsCacheCounters(expandedGroupsMaxSize), new AutoRefreshCache<Tuple<PartitionId, OrganizationId, Guid>, ExpandedGroup, object>.CreateEntryDelegate(this.ExpandGroupInAD));
		}

		// Token: 0x1700030F RID: 783
		// (get) Token: 0x0600127B RID: 4731 RVA: 0x0005925F File Offset: 0x0005745F
		internal static Trace Tracer
		{
			get
			{
				return ExTraceGlobals.IsMemberOfResolverTracer;
			}
		}

		// Token: 0x0600127C RID: 4732 RVA: 0x00059266 File Offset: 0x00057466
		public void ClearCache()
		{
			this.ThrowIfDisposed();
			this.resolvedGroups.Clear();
			this.expandedGroups.Clear();
		}

		// Token: 0x0600127D RID: 4733 RVA: 0x00059284 File Offset: 0x00057484
		protected override DisposeTracker InternalGetDisposeTracker()
		{
			return DisposeTracker.Get<IsMemberOfResolver<TGroupKeyType>>(this);
		}

		// Token: 0x0600127E RID: 4734 RVA: 0x0005928C File Offset: 0x0005748C
		protected override void InternalDispose(bool disposing)
		{
			this.disposed = true;
			if (disposing)
			{
				this.resolvedGroups.Dispose();
				this.resolvedGroups = null;
				this.expandedGroups.Dispose();
				this.expandedGroups = null;
			}
		}

		// Token: 0x0600127F RID: 4735 RVA: 0x000592BC File Offset: 0x000574BC
		public bool IsMemberOf(IRecipientSession session, ADObjectId recipientId, TGroupKeyType groupKey)
		{
			return this.IsMemberOf(session, recipientId.ObjectGuid, groupKey);
		}

		// Token: 0x06001280 RID: 4736 RVA: 0x000592CC File Offset: 0x000574CC
		public bool IsMemberOf(IRecipientSession session, Guid recipientObjectGuid, TGroupKeyType groupKey)
		{
			this.ThrowIfDisposed();
			if (session == null)
			{
				throw new ArgumentNullException("session");
			}
			if (this.enabled)
			{
				ResolvedGroup value = this.resolvedGroups.GetValue(null, new Tuple<PartitionId, OrganizationId, TGroupKeyType>(session.SessionSettings.PartitionId, session.SessionSettings.CurrentOrganizationId, groupKey));
				if (value != null && value.GroupGuid != Guid.Empty)
				{
					return this.IsMemberOf(session, recipientObjectGuid, value.GroupGuid);
				}
			}
			return false;
		}

		// Token: 0x06001281 RID: 4737 RVA: 0x00059344 File Offset: 0x00057544
		private bool IsMemberOf(IRecipientSession session, Guid recipientGuid, Guid groupGuid)
		{
			Queue<Guid> queue = new Queue<Guid>();
			queue.Enqueue(groupGuid);
			HashSet<Guid> hashSet = new HashSet<Guid>();
			bool flag = false;
			while (queue.Count > 0)
			{
				Guid guid = queue.Dequeue();
				if (!hashSet.Contains(guid))
				{
					hashSet.Add(guid);
					ExpandedGroup value = this.expandedGroups.GetValue(null, new Tuple<PartitionId, OrganizationId, Guid>(session.SessionSettings.PartitionId, session.SessionSettings.CurrentOrganizationId, guid));
					if (value != null)
					{
						if (value.ContainsRecipient(recipientGuid))
						{
							flag = true;
							break;
						}
						foreach (Guid guid2 in value.MemberGroups)
						{
							if (guid2 == recipientGuid)
							{
								flag = true;
								break;
							}
							queue.Enqueue(guid2);
						}
						if (flag)
						{
							break;
						}
					}
				}
			}
			IsMemberOfResolver<TGroupKeyType>.Tracer.TraceDebug<Guid, Guid, bool>((long)this.GetHashCode(), "IsMemberOf result for recipientGuid='{0}', groupGuid='{1}' is '{2}'", recipientGuid, groupGuid, flag);
			return flag;
		}

		// Token: 0x06001282 RID: 4738 RVA: 0x00059444 File Offset: 0x00057644
		private ResolvedGroup ResolveGroupInAD(object stateNotUsed, Tuple<PartitionId, OrganizationId, TGroupKeyType> tuple)
		{
			PartitionId item = tuple.Item1;
			OrganizationId item2 = tuple.Item2;
			TGroupKeyType item3 = tuple.Item3;
			IRecipientSession session = this.ReconstructRecipientSession(item, item2);
			IsMemberOfResolver<TGroupKeyType>.Tracer.TraceDebug<TGroupKeyType>((long)this.GetHashCode(), "Resolving group '{0}' in AD", item3);
			int count;
			ResolvedGroup result = this.adAdapter.ResolveGroup(session, item3, out count) ?? new ResolvedGroup();
			this.perfCounters.IncrementLDAPQueryCount(count);
			return result;
		}

		// Token: 0x06001283 RID: 4739 RVA: 0x000594B0 File Offset: 0x000576B0
		private ExpandedGroup ExpandGroupInAD(object stateNotUsed, Tuple<PartitionId, OrganizationId, Guid> tuple)
		{
			PartitionId item = tuple.Item1;
			OrganizationId item2 = tuple.Item2;
			Guid item3 = tuple.Item3;
			IRecipientSession session = this.ReconstructRecipientSession(item, item2);
			IsMemberOfResolver<TGroupKeyType>.Tracer.TraceDebug<Guid>((long)this.GetHashCode(), "Expanding group '{0}' in AD", item3);
			int count;
			ExpandedGroup result = this.adAdapter.ExpandGroup(session, new ADObjectId(item3), out count) ?? new ExpandedGroup();
			this.perfCounters.IncrementLDAPQueryCount(count);
			return result;
		}

		// Token: 0x06001284 RID: 4740 RVA: 0x00059520 File Offset: 0x00057720
		private IRecipientSession ReconstructRecipientSession(PartitionId partitionId, OrganizationId orgId)
		{
			ADSessionSettings sessionSettings = Datacenter.IsMultiTenancyEnabled() ? ADSessionSettings.FromOrganizationIdWithoutRbacScopesServiceOnly(orgId) : ADSessionSettings.FromAccountPartitionRootOrgScopeSet(partitionId);
			return DirectorySessionFactory.Default.GetTenantOrRootOrgRecipientSession(true, ConsistencyMode.IgnoreInvalid, sessionSettings, 307, "ReconstructRecipientSession", "f:\\15.00.1497\\sources\\dev\\data\\src\\directory\\IsMemberOfProvider\\IsMemberOfResolver.cs");
		}

		// Token: 0x06001285 RID: 4741 RVA: 0x0005955F File Offset: 0x0005775F
		protected void ThrowIfDisposed()
		{
			if (this.disposed)
			{
				throw new ObjectDisposedException("IsMemberOfResolver");
			}
		}

		// Token: 0x04000AAE RID: 2734
		protected bool enabled;

		// Token: 0x04000AAF RID: 2735
		private readonly IsMemberOfResolverPerformanceCounters perfCounters;

		// Token: 0x04000AB0 RID: 2736
		private readonly IsMemberOfResolverADAdapter<TGroupKeyType> adAdapter;

		// Token: 0x04000AB1 RID: 2737
		protected AutoRefreshCache<Tuple<PartitionId, OrganizationId, TGroupKeyType>, ResolvedGroup, object> resolvedGroups;

		// Token: 0x04000AB2 RID: 2738
		protected AutoRefreshCache<Tuple<PartitionId, OrganizationId, Guid>, ExpandedGroup, object> expandedGroups;

		// Token: 0x04000AB3 RID: 2739
		private bool disposed;
	}
}
