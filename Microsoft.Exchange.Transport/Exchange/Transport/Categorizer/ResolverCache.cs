using System;
using System.Collections.Generic;
using Microsoft.Exchange.Common.Cache;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Diagnostics.Components.Transport;

namespace Microsoft.Exchange.Transport.Categorizer
{
	// Token: 0x020001F0 RID: 496
	internal class ResolverCache : ISimpleCache<ADObjectId, bool>
	{
		// Token: 0x0600162F RID: 5679 RVA: 0x0005A8C8 File Offset: 0x00058AC8
		public virtual bool TryGetValue(ADObjectId groupId, out bool isMember)
		{
			isMember = false;
			ExTraceGlobals.ResolverTracer.TraceDebug<Guid, string, ADObjectId>(0L, "Checking if sender is MemberOf {0} : {1} : {2}", groupId.ObjectGuid, groupId.DistinguishedName, groupId);
			if (this.memberOfGroupsCache == null || !this.memberOfGroupsCache.TryGetValue(groupId, out isMember))
			{
				return false;
			}
			ExTraceGlobals.ResolverTracer.TraceDebug<ADObjectId>(0L, "Found item {0} in MemberOfGroup cache", groupId);
			return true;
		}

		// Token: 0x06001630 RID: 5680 RVA: 0x0005A924 File Offset: 0x00058B24
		public virtual bool TryAddValue(ADObjectId groupId, bool isMember)
		{
			if (this.memberOfGroupsCache == null)
			{
				this.memberOfGroupsCache = new Dictionary<ADObjectId, bool>();
			}
			if (this.memberOfGroupsCache.Count >= Components.TransportAppConfig.Resolver.MaxResolverMemberOfGroupCacheSize)
			{
				ExTraceGlobals.ResolverTracer.TraceDebug<int, int>(0L, "Cache size {0} reached the maximum size {1} allowed for MemberOfGroup cache. Not adding items to cache.", this.memberOfGroupsCache.Count, Components.TransportAppConfig.Resolver.MaxResolverMemberOfGroupCacheSize);
				return false;
			}
			ExTraceGlobals.ResolverTracer.TraceDebug<Guid, string>(0L, "Adding {0} : {1} to the MemberOfGroup cache.", groupId.ObjectGuid, groupId.DistinguishedName);
			if (!this.memberOfGroupsCache.ContainsKey(groupId))
			{
				this.memberOfGroupsCache.Add(groupId, isMember);
				return true;
			}
			throw new InvalidOperationException(string.Format("Found the same item '{0}' in cache and in the result. It is expected to use the information from the cache if there is one.", groupId));
		}

		// Token: 0x06001631 RID: 5681 RVA: 0x0005A9D8 File Offset: 0x00058BD8
		public virtual void AddToResolvedRecipientCache(Guid recipientGuid)
		{
			if (recipientGuid == Guid.Empty)
			{
				throw new ArgumentException("Should not add empty guid to the visited list.", "recipientGuid");
			}
			if (this.visitedDGsAndContacts == null)
			{
				this.visitedDGsAndContacts = new HashSet<Guid>();
			}
			if (this.visitedDGsAndContactsForCurrentExpansion == null)
			{
				this.visitedDGsAndContactsForCurrentExpansion = new HashSet<Guid>();
			}
			if (this.visitedDGsAndContacts.Count + this.visitedDGsAndContactsForCurrentExpansion.Count >= Components.TransportAppConfig.Resolver.MaxResolveRecipientCacheSize)
			{
				ExTraceGlobals.ResolverTracer.TraceDebug<int, int>(0L, "Cache size {0} reached the maximum size {1}. Not adding item to cache.", this.visitedDGsAndContacts.Count, Components.TransportAppConfig.Resolver.MaxResolveRecipientCacheSize);
				return;
			}
			this.visitedDGsAndContactsForCurrentExpansion.Add(recipientGuid);
		}

		// Token: 0x06001632 RID: 5682 RVA: 0x0005AA89 File Offset: 0x00058C89
		public bool RecipientExists(Guid recipientGuid)
		{
			return (this.visitedDGsAndContacts != null || this.visitedDGsAndContactsForCurrentExpansion != null) && (this.visitedDGsAndContactsForCurrentExpansion.Contains(recipientGuid) || this.visitedDGsAndContacts.Contains(recipientGuid));
		}

		// Token: 0x06001633 RID: 5683 RVA: 0x0005AAB9 File Offset: 0x00058CB9
		public void MergeResultsFromCurrentExpansion()
		{
			if (this.visitedDGsAndContactsForCurrentExpansion != null && this.visitedDGsAndContacts != null)
			{
				this.visitedDGsAndContacts.UnionWith(this.visitedDGsAndContactsForCurrentExpansion);
				this.visitedDGsAndContactsForCurrentExpansion.Clear();
			}
		}

		// Token: 0x06001634 RID: 5684 RVA: 0x0005AAE7 File Offset: 0x00058CE7
		public void DiscardResultsFromCurrentExpansion()
		{
			if (this.visitedDGsAndContactsForCurrentExpansion != null)
			{
				this.visitedDGsAndContactsForCurrentExpansion.Clear();
			}
		}

		// Token: 0x04000AFC RID: 2812
		private Dictionary<ADObjectId, bool> memberOfGroupsCache;

		// Token: 0x04000AFD RID: 2813
		private HashSet<Guid> visitedDGsAndContacts;

		// Token: 0x04000AFE RID: 2814
		private HashSet<Guid> visitedDGsAndContactsForCurrentExpansion;
	}
}
