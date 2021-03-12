using System;
using System.Collections.Generic;
using Microsoft.Exchange.Common.Cache;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.ADRecipientPermission;

namespace Microsoft.Exchange.Data.Directory.Recipient
{
	// Token: 0x02000203 RID: 515
	internal class ADRecipientRestriction
	{
		// Token: 0x06001AC6 RID: 6854 RVA: 0x00070518 File Offset: 0x0006E718
		private ADRecipientRestriction(ADObjectId senderId, bool isAdRecipient, bool senderIsAuthenticated, ICollection<ADObjectId> rejectMessagesFrom, ICollection<ADObjectId> rejectMessagesFromDLMembers, ICollection<ADObjectId> acceptMessagesFrom, ICollection<ADObjectId> acceptMessagesFromDLMembers, ICollection<ADObjectId> bypassModerationFrom, ICollection<ADObjectId> bypassModerationFromDLMembers, ICollection<ADObjectId> moderators, ICollection<ADObjectId> managedBy, bool requiresAllSendersAreAuthenticated, bool moderationEnabled, RecipientType recipientType, IRecipientSession session, ISimpleCache<ADObjectId, bool> senderMembershipCache, int adQueryLimit)
		{
			this.senderId = senderId;
			this.isAdRecipient = isAdRecipient;
			this.senderIsAuthenticated = senderIsAuthenticated;
			this.rejectMessagesFrom = rejectMessagesFrom;
			this.rejectMessagesFromDLMembers = rejectMessagesFromDLMembers;
			this.acceptMessagesFrom = acceptMessagesFrom;
			this.acceptMessagesFromDLMembers = acceptMessagesFromDLMembers;
			this.bypassModerationFrom = bypassModerationFrom;
			this.bypassModerationFromDLMembers = bypassModerationFromDLMembers;
			this.moderators = moderators;
			this.managedBy = managedBy;
			this.moderationEnabled = moderationEnabled;
			this.recipientType = recipientType;
			this.requiresAllSendersAreAuthenticated = requiresAllSendersAreAuthenticated;
			this.session = session;
			this.memberOfGroupsCache = senderMembershipCache;
			this.adQueryLimit = adQueryLimit;
			this.isFirstGroupSearch = true;
		}

		// Token: 0x06001AC7 RID: 6855 RVA: 0x000705B7 File Offset: 0x0006E7B7
		public static bool Accepted(RestrictionCheckResult result)
		{
			return (result & (RestrictionCheckResult)3221225472U) == (RestrictionCheckResult)0U;
		}

		// Token: 0x06001AC8 RID: 6856 RVA: 0x000705C3 File Offset: 0x0006E7C3
		public static bool Moderated(RestrictionCheckResult result)
		{
			return (result & RestrictionCheckResult.Moderated) != (RestrictionCheckResult)0U;
		}

		// Token: 0x06001AC9 RID: 6857 RVA: 0x000705D2 File Offset: 0x0006E7D2
		public static bool Failed(RestrictionCheckResult result)
		{
			return (result & (RestrictionCheckResult)2147483648U) != (RestrictionCheckResult)0U;
		}

		// Token: 0x06001ACA RID: 6858 RVA: 0x000705E1 File Offset: 0x0006E7E1
		public static RestrictionCheckResult CheckDeliveryRestrictionForOneOffRecipient(ADObjectId senderId, bool senderIsAuthenticated)
		{
			return RestrictionCheckResult.AcceptedNoPermissionList;
		}

		// Token: 0x06001ACB RID: 6859 RVA: 0x000705E4 File Offset: 0x0006E7E4
		public static RestrictionCheckResult CheckDeliveryRestrictionForAuthenticatedSender(ADObjectId senderId, ADRawEntry recipientEntry, IRecipientSession session)
		{
			return ADRecipientRestriction.CheckDeliveryRestriction(senderId, true, (MultiValuedProperty<ADObjectId>)recipientEntry[ADRecipientSchema.RejectMessagesFrom], (MultiValuedProperty<ADObjectId>)recipientEntry[ADRecipientSchema.RejectMessagesFromDLMembers], (MultiValuedProperty<ADObjectId>)recipientEntry[ADRecipientSchema.AcceptMessagesOnlyFrom], (MultiValuedProperty<ADObjectId>)recipientEntry[ADRecipientSchema.AcceptMessagesOnlyFromDLMembers], (MultiValuedProperty<ADObjectId>)recipientEntry[ADRecipientSchema.BypassModerationFrom], (MultiValuedProperty<ADObjectId>)recipientEntry[ADRecipientSchema.BypassModerationFromDLMembers], (MultiValuedProperty<ADObjectId>)recipientEntry[ADRecipientSchema.ModeratedBy], (MultiValuedProperty<ADObjectId>)recipientEntry[ADGroupSchema.ManagedBy], true, (bool)recipientEntry[ADRecipientSchema.ModerationEnabled], (RecipientType)recipientEntry[ADRecipientSchema.RecipientType], session, null, -1);
		}

		// Token: 0x06001ACC RID: 6860 RVA: 0x0007069C File Offset: 0x0006E89C
		public static RestrictionCheckResult CheckDeliveryRestriction(ADObjectId senderId, bool senderIsAuthenticated, ICollection<ADObjectId> rejectMessagesFrom, ICollection<ADObjectId> rejectMessagesFromDLMembers, ICollection<ADObjectId> acceptMessagesFrom, ICollection<ADObjectId> acceptMessagesFromDLMembers, ICollection<ADObjectId> bypassModerationFrom, ICollection<ADObjectId> bypassModerationFromDLMembers, ICollection<ADObjectId> moderators, ICollection<ADObjectId> managedBy, bool requiresAllSendersAreAuthenticated, bool moderationEnabled, RecipientType recipientType, IRecipientSession session, ISimpleCache<ADObjectId, bool> senderMembershipCache)
		{
			return ADRecipientRestriction.CheckDeliveryRestriction(senderId, senderIsAuthenticated, rejectMessagesFrom, rejectMessagesFromDLMembers, acceptMessagesFrom, acceptMessagesFromDLMembers, bypassModerationFrom, bypassModerationFromDLMembers, moderators, managedBy, requiresAllSendersAreAuthenticated, moderationEnabled, recipientType, session, senderMembershipCache, -1);
		}

		// Token: 0x06001ACD RID: 6861 RVA: 0x000706CC File Offset: 0x0006E8CC
		public static RestrictionCheckResult CheckDeliveryRestriction(ADObjectId senderId, bool senderIsAuthenticated, ICollection<ADObjectId> rejectMessagesFrom, ICollection<ADObjectId> rejectMessagesFromDLMembers, ICollection<ADObjectId> acceptMessagesFrom, ICollection<ADObjectId> acceptMessagesFromDLMembers, ICollection<ADObjectId> bypassModerationFrom, ICollection<ADObjectId> bypassModerationFromDLMembers, ICollection<ADObjectId> moderators, ICollection<ADObjectId> managedBy, bool requiresAllSendersAreAuthenticated, bool moderationEnabled, RecipientType recipientType, IRecipientSession session, ISimpleCache<ADObjectId, bool> senderMembershipCache, int adQueryLimit)
		{
			ADRecipientRestriction adrecipientRestriction = new ADRecipientRestriction(senderId, true, senderIsAuthenticated, rejectMessagesFrom, rejectMessagesFromDLMembers, acceptMessagesFrom, acceptMessagesFromDLMembers, bypassModerationFrom, bypassModerationFromDLMembers, moderators, managedBy, requiresAllSendersAreAuthenticated, moderationEnabled, recipientType, session, senderMembershipCache, adQueryLimit);
			return adrecipientRestriction.Check();
		}

		// Token: 0x06001ACE RID: 6862 RVA: 0x00070702 File Offset: 0x0006E902
		private static bool IsNullOrEmptyCollection<T>(ICollection<T> collection)
		{
			return collection == null || collection.Count == 0;
		}

		// Token: 0x1700064F RID: 1615
		// (get) Token: 0x06001ACF RID: 6863 RVA: 0x00070712 File Offset: 0x0006E912
		private IRecipientSession Session
		{
			get
			{
				return this.session;
			}
		}

		// Token: 0x06001AD0 RID: 6864 RVA: 0x0007071C File Offset: 0x0006E91C
		private RestrictionCheckResult Check()
		{
			if (!this.CheckAuthentication())
			{
				return (RestrictionCheckResult)2147483655U;
			}
			RestrictionCheckResult restrictionCheckResult = this.CheckPermissionRestriction();
			if (this.moderationEnabled && ADRecipientRestriction.Accepted(restrictionCheckResult))
			{
				ADRecipientRestriction.tracer.TraceDebug<ADObjectId, RestrictionCheckResult>((long)this.GetHashCode(), "Sender {0} permission check result is {1}, with moderation enabled, now checking moderation bypasses", this.senderId, restrictionCheckResult);
				restrictionCheckResult = this.CheckModerationBypass();
			}
			ADRecipientRestriction.tracer.TraceDebug<ADObjectId, RestrictionCheckResult>((long)this.GetHashCode(), "Sender {0} permission check result is {1}", this.senderId, restrictionCheckResult);
			return restrictionCheckResult;
		}

		// Token: 0x06001AD1 RID: 6865 RVA: 0x00070790 File Offset: 0x0006E990
		private RestrictionCheckResult CheckModerationBypass()
		{
			if (this.IsSenderOnSecurityList(this.managedBy))
			{
				return RestrictionCheckResult.AcceptedInOwnersList;
			}
			if (this.IsSenderOnSecurityList(this.moderators))
			{
				return RestrictionCheckResult.AcceptedInModeratorsList;
			}
			if (this.IsSenderOnSecurityList(this.bypassModerationFrom))
			{
				return RestrictionCheckResult.AcceptedInBypassModerationRecipientList;
			}
			bool flag2;
			bool flag = this.TryIsSenderMemberOfWithLimit(this.bypassModerationFromDLMembers, out flag2);
			if (!flag || flag2)
			{
				return RestrictionCheckResult.AcceptedInBypassModerationGroupList;
			}
			return RestrictionCheckResult.Moderated;
		}

		// Token: 0x06001AD2 RID: 6866 RVA: 0x000707EC File Offset: 0x0006E9EC
		private RestrictionCheckResult CheckPermissionRestriction()
		{
			if (!this.HasPermissionRestriction())
			{
				return RestrictionCheckResult.AcceptedNoPermissionList;
			}
			if (this.senderId == null)
			{
				return this.ShouldAcceptByDefault();
			}
			if (this.IsSenderOnSecurityList(this.rejectMessagesFrom))
			{
				return (RestrictionCheckResult)2147483652U;
			}
			if (this.IsSenderOnSecurityList(this.acceptMessagesFrom))
			{
				return RestrictionCheckResult.AcceptedInRecipientList;
			}
			if (this.rejectMessagesFromDLMembers == null)
			{
				if (this.acceptMessagesFromDLMembers == null)
				{
					goto IL_A9;
				}
			}
			try
			{
				bool flag;
				if (this.TryIsSenderMemberOfWithLimit(this.rejectMessagesFromDLMembers, out flag) && flag)
				{
					return (RestrictionCheckResult)2147483653U;
				}
				bool flag2 = this.TryIsSenderMemberOfWithLimit(this.acceptMessagesFromDLMembers, out flag);
				if (!flag2 || flag)
				{
					return RestrictionCheckResult.AcceptedInGroupList;
				}
			}
			catch (DataValidationException arg)
			{
				ADRecipientRestriction.tracer.TraceError<DataValidationException, ADObjectId>((long)this.GetHashCode(), "Data validation exception {0} while checking DL membership for sender '{1}'", arg, this.senderId);
				return (RestrictionCheckResult)2147483656U;
			}
			IL_A9:
			return this.ShouldAcceptByDefault();
		}

		// Token: 0x06001AD3 RID: 6867 RVA: 0x000708BC File Offset: 0x0006EABC
		private RestrictionCheckResult ShouldAcceptByDefault()
		{
			if (ADRecipientRestriction.IsNullOrEmptyCollection<ADObjectId>(this.acceptMessagesFrom) && ADRecipientRestriction.IsNullOrEmptyCollection<ADObjectId>(this.acceptMessagesFromDLMembers))
			{
				return RestrictionCheckResult.AcceptedAcceptanceListEmpty;
			}
			return (RestrictionCheckResult)2147483654U;
		}

		// Token: 0x06001AD4 RID: 6868 RVA: 0x000708E0 File Offset: 0x0006EAE0
		private bool HasPermissionRestriction()
		{
			if (!this.isAdRecipient)
			{
				return false;
			}
			bool flag = ADRecipientRestriction.IsNullOrEmptyCollection<ADObjectId>(this.acceptMessagesFrom) && ADRecipientRestriction.IsNullOrEmptyCollection<ADObjectId>(this.acceptMessagesFromDLMembers) && ADRecipientRestriction.IsNullOrEmptyCollection<ADObjectId>(this.rejectMessagesFrom) && ADRecipientRestriction.IsNullOrEmptyCollection<ADObjectId>(this.rejectMessagesFromDLMembers);
			return !flag;
		}

		// Token: 0x06001AD5 RID: 6869 RVA: 0x00070931 File Offset: 0x0006EB31
		private bool CheckAuthentication()
		{
			return !this.isAdRecipient || (!this.requiresAllSendersAreAuthenticated && this.recipientType != RecipientType.SystemMailbox && this.recipientType != RecipientType.SystemAttendantMailbox) || this.senderIsAuthenticated;
		}

		// Token: 0x06001AD6 RID: 6870 RVA: 0x00070964 File Offset: 0x0006EB64
		private bool IsSenderOnSecurityList(ICollection<ADObjectId> list)
		{
			return list != null && list.Contains(this.senderId);
		}

		// Token: 0x06001AD7 RID: 6871 RVA: 0x00070978 File Offset: 0x0006EB78
		private bool TryIsSenderMemberOfWithLimit(IEnumerable<ADObjectId> groupIdList, out bool isSenderMemberOf)
		{
			isSenderMemberOf = false;
			if (groupIdList == null)
			{
				return true;
			}
			if (this.CheckMemberOfGroupInformationInCache(groupIdList))
			{
				ADRecipientRestriction.tracer.TraceDebug<ADObjectId>((long)this.GetHashCode(), "Found the IsMemberOf info in cache: {0}", this.senderId);
				isSenderMemberOf = true;
				return true;
			}
			foreach (ADObjectId adobjectId in groupIdList)
			{
				bool flag;
				if (this.memberOfGroupsCache != null && this.memberOfGroupsCache.TryGetValue(adobjectId, out flag))
				{
					ADRecipientRestriction.tracer.TraceDebug<ADObjectId, bool>((long)this.GetHashCode(), "Found the MemberOfGroup info in Cache: {0} : {1}", this.senderId, flag);
					ExAssert.RetailAssert(!flag, "We should have checked the cache before going to AD.");
				}
				else
				{
					if (!ADRecipient.TryIsMemberOfWithLimit(this.senderId, adobjectId, false, this.Session, ref this.adQueryLimit, out flag))
					{
						if (this.isFirstGroupSearch)
						{
							this.AddMemberOfGroup(adobjectId, false);
						}
						this.isFirstGroupSearch = false;
						return false;
					}
					this.isFirstGroupSearch = false;
					this.AddMemberOfGroup(adobjectId, flag);
					if (flag)
					{
						ADRecipientRestriction.tracer.TraceDebug<ADObjectId, ADObjectId>((long)this.GetHashCode(), "Sender {0} is a member of group {1}", this.senderId, adobjectId);
						isSenderMemberOf = true;
						return true;
					}
				}
			}
			return true;
		}

		// Token: 0x06001AD8 RID: 6872 RVA: 0x00070AA8 File Offset: 0x0006ECA8
		private bool CheckMemberOfGroupInformationInCache(IEnumerable<ADObjectId> groupIdList)
		{
			if (this.memberOfGroupsCache == null)
			{
				return false;
			}
			foreach (ADObjectId key in groupIdList)
			{
				bool flag;
				if (this.memberOfGroupsCache.TryGetValue(key, out flag) && flag)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06001AD9 RID: 6873 RVA: 0x00070B10 File Offset: 0x0006ED10
		private void AddMemberOfGroup(ADObjectId groupId, bool isMember)
		{
			if (this.memberOfGroupsCache != null)
			{
				this.memberOfGroupsCache.TryAddValue(groupId, isMember);
			}
		}

		// Token: 0x04000BB4 RID: 2996
		internal const int ADQueryUnlimited = -1;

		// Token: 0x04000BB5 RID: 2997
		private static readonly Trace tracer = ExTraceGlobals.ADPermissionTracer;

		// Token: 0x04000BB6 RID: 2998
		private ADObjectId senderId;

		// Token: 0x04000BB7 RID: 2999
		private bool senderIsAuthenticated;

		// Token: 0x04000BB8 RID: 3000
		private bool isAdRecipient;

		// Token: 0x04000BB9 RID: 3001
		private ICollection<ADObjectId> rejectMessagesFrom;

		// Token: 0x04000BBA RID: 3002
		private ICollection<ADObjectId> rejectMessagesFromDLMembers;

		// Token: 0x04000BBB RID: 3003
		private ICollection<ADObjectId> acceptMessagesFrom;

		// Token: 0x04000BBC RID: 3004
		private ICollection<ADObjectId> acceptMessagesFromDLMembers;

		// Token: 0x04000BBD RID: 3005
		private ICollection<ADObjectId> bypassModerationFrom;

		// Token: 0x04000BBE RID: 3006
		private ICollection<ADObjectId> bypassModerationFromDLMembers;

		// Token: 0x04000BBF RID: 3007
		private ICollection<ADObjectId> moderators;

		// Token: 0x04000BC0 RID: 3008
		private ICollection<ADObjectId> managedBy;

		// Token: 0x04000BC1 RID: 3009
		private bool moderationEnabled;

		// Token: 0x04000BC2 RID: 3010
		private bool requiresAllSendersAreAuthenticated;

		// Token: 0x04000BC3 RID: 3011
		private RecipientType recipientType;

		// Token: 0x04000BC4 RID: 3012
		private IRecipientSession session;

		// Token: 0x04000BC5 RID: 3013
		private ISimpleCache<ADObjectId, bool> memberOfGroupsCache;

		// Token: 0x04000BC6 RID: 3014
		private int adQueryLimit;

		// Token: 0x04000BC7 RID: 3015
		private bool isFirstGroupSearch;
	}
}
