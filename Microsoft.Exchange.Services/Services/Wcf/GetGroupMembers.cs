using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.Serialization;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.GroupMailbox;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Services;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.Services.Core.Types;
using Microsoft.Exchange.Services.Diagnostics;
using Microsoft.Exchange.Services.Wcf.Types;
using Microsoft.Exchange.UM.UMCommon;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x0200091C RID: 2332
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class GetGroupMembers
	{
		// Token: 0x0600438A RID: 17290 RVA: 0x000E4B28 File Offset: 0x000E2D28
		public GetGroupMembers(IRecipientSession session, OrganizationId orgId, GroupMailboxLocator locator, string infoString, ModernGroupMembersSortOrder order, int rows, string serializedPeopleIKnowGraph, RequestDetailsLogger logger)
		{
			this.adSession = session;
			this.organizationId = orgId;
			this.groupMailboxLocator = locator;
			this.clientInfoString = infoString;
			this.sortOrder = order;
			this.maxRows = rows;
			this.requestDetailsLogger = logger;
			this.InitGroupMemberComparer(serializedPeopleIKnowGraph);
		}

		// Token: 0x17000F76 RID: 3958
		// (get) Token: 0x0600438B RID: 17291 RVA: 0x000E4B83 File Offset: 0x000E2D83
		private List<ModernGroupMemberType> OwnersFromAd
		{
			get
			{
				this.ownersFromAd = (this.ownersFromAd ?? this.GetOwnersFromAD());
				return this.ownersFromAd;
			}
		}

		// Token: 0x0600438C RID: 17292 RVA: 0x000E4BA4 File Offset: 0x000E2DA4
		public ModernGroupMembersResponse GetMembers()
		{
			this.PopulateRecentlyJoinedAndRemovedMembers();
			List<ModernGroupMemberType> list;
			if (this.sortOrder == ModernGroupMembersSortOrder.PeopleIKnow)
			{
				list = this.GetMergedMemberList(false, 1900);
			}
			else
			{
				list = this.GetMergedMemberList(true, this.maxRows + this.recentlyRemovedMembers.Count + 1);
				if (this.sortOrder == ModernGroupMembersSortOrder.OwnerAndDisplayName && list.Count >= this.maxRows)
				{
					List<ModernGroupMemberType> second = this.OwnersFromAd;
					list = list.Union(second, ModernGroupMemberComparerByAdObjectId.Singleton).ToList<ModernGroupMemberType>();
				}
			}
			this.requestDetailsLogger.Set(GetModernGroupMetadata.MemberCount, list.Count);
			bool hasMoreMembers = list.Count > this.maxRows;
			this.requestDetailsLogger.Set(GetModernGroupMetadata.SortOrder, this.sortOrder);
			list.Sort(this.groupMemberComparer);
			ModernGroupMemberType[] array = list.Take(this.maxRows).ToArray<ModernGroupMemberType>();
			return new ModernGroupMembersResponse
			{
				Members = array,
				Count = array.Length,
				HasMoreMembers = hasMoreMembers
			};
		}

		// Token: 0x0600438D RID: 17293 RVA: 0x000E4CA8 File Offset: 0x000E2EA8
		public ModernGroupMembersResponse GetOwners()
		{
			this.OwnersFromAd.Sort(this.groupMemberComparer);
			return new ModernGroupMembersResponse
			{
				Members = this.OwnersFromAd.ToArray(),
				Count = this.OwnersFromAd.Count
			};
		}

		// Token: 0x0600438E RID: 17294 RVA: 0x000E4E2C File Offset: 0x000E302C
		private void PopulateRecentlyJoinedAndRemovedMembers()
		{
			this.MeasureIt(delegate
			{
				this.recentlyAddedMembers = new List<ModernGroupMemberType>();
				this.recentlyRemovedMembers = new List<ModernGroupMemberType>();
				ADUser aduser = this.groupMailboxLocator.FindAdUser();
				ExDateTime exDateTime = ExDateTime.UtcNow.Subtract(TimeSpan.FromMinutes(30.0));
				if (aduser.WhenChangedUTC != null)
				{
					exDateTime = GetGroupMembers.GetMinimumExDateTime(exDateTime, new ExDateTime(ExTimeZone.UtcTimeZone, aduser.WhenChangedUTC.Value));
				}
				List<UserMailbox> membershipChangesFromGroupMailbox = this.GetMembershipChangesFromGroupMailbox(exDateTime);
				this.requestDetailsLogger.Set(GetModernGroupMetadata.MemberMBCount, membershipChangesFromGroupMailbox.Count);
				foreach (UserMailbox userMailbox in membershipChangesFromGroupMailbox)
				{
					if (userMailbox.IsMember)
					{
						this.recentlyAddedMembers.Add(GetGroupMembers.UserMailboxToModernGroupMemberType(userMailbox));
					}
					else
					{
						this.recentlyRemovedMembers.Add(GetGroupMembers.UserMailboxToModernGroupMemberType(userMailbox));
					}
				}
				ExTraceGlobals.ModernGroupsTracer.TraceDebug<int, int, int>((long)this.GetHashCode(), "GetGroupMembers::PopulateRecentlyJoinedAndRemovedMembers. TotalRecentlyChangedMembers={0}, RecentlyAdded={1}, RecentlyRemoved={2}", membershipChangesFromGroupMailbox.Count, this.recentlyAddedMembers.Count, this.recentlyRemovedMembers.Count);
			}, GetModernGroupMetadata.MemberMBLatency);
		}

		// Token: 0x0600438F RID: 17295 RVA: 0x000E4F10 File Offset: 0x000E3110
		private List<ModernGroupMemberType> GetMergedMemberList(bool sortByDisplayName, int maxMembersToRetrieveFromAd)
		{
			List<ModernGroupMemberType> result = null;
			this.MeasureIt(delegate
			{
				ExTraceGlobals.ModernGroupsTracer.TraceDebug<int>((long)this.GetHashCode(), "GetGroupMembers::GetMergedMemberList. Max requested members from AD: {0}", maxMembersToRetrieveFromAd);
				ADUser groupMailbox = this.groupMailboxLocator.FindAdUser();
				UnifiedGroupADAccessLayer unifiedGroupADAccessLayer = new UnifiedGroupADAccessLayer(groupMailbox, null);
				List<ModernGroupMemberType> list = unifiedGroupADAccessLayer.GetMembers(sortByDisplayName, maxMembersToRetrieveFromAd).ConvertAll(new Converter<UnifiedGroupParticipant, ModernGroupMemberType>(GetGroupMembers.ParticipantToModernGroupMemberType));
				ExTraceGlobals.ModernGroupsTracer.TraceDebug<int>((long)this.GetHashCode(), "GetGroupMembers::GetMergedMemberList. Members in AD: {0}", list.Count);
				result = list.Except(this.recentlyRemovedMembers, ModernGroupMemberComparerByAdObjectId.Singleton).Union(this.recentlyAddedMembers, ModernGroupMemberComparerByAdObjectId.Singleton).ToList<ModernGroupMemberType>();
			}, GetModernGroupMetadata.MemberADLatency);
			return result;
		}

		// Token: 0x06004390 RID: 17296 RVA: 0x000E4F58 File Offset: 0x000E3158
		private List<ModernGroupMemberType> GetOwnersFromAD()
		{
			ADUser aduser = this.groupMailboxLocator.FindAdUser();
			Result<ADRawEntry>[] array = this.adSession.FindByADObjectIds(aduser.Owners.ToArray(), GetGroupMembers.AdProperties);
			List<ModernGroupMemberType> list = new List<ModernGroupMemberType>(array.Length);
			foreach (Result<ADRawEntry> result in array)
			{
				if (result.Error != null || result.Data == null)
				{
					ExTraceGlobals.ModernGroupsTracer.TraceError<string>((long)this.GetHashCode(), "GetModernGroup::GetOwnersFromAD. Unable to find an owner in AD: {0}", (result.Error != null) ? result.Error.ToString() : "Result.Data is null");
				}
				else
				{
					UserMailboxLocator locator = this.CreateUserMailboxLocator(result.Data);
					UserMailbox mailbox = new UserMailboxBuilder(locator, aduser.Owners).BuildFromDirectory(result.Data).Mailbox;
					list.Add(GetGroupMembers.UserMailboxToModernGroupMemberType(mailbox));
				}
			}
			return list;
		}

		// Token: 0x06004391 RID: 17297 RVA: 0x000E5044 File Offset: 0x000E3244
		private void MeasureIt(Action action, GetModernGroupMetadata metaData)
		{
			try
			{
				this.stopWatch.Restart();
				action();
			}
			finally
			{
				this.requestDetailsLogger.Set(metaData, (int)this.stopWatch.Elapsed.TotalMilliseconds);
			}
		}

		// Token: 0x06004392 RID: 17298 RVA: 0x000E50A0 File Offset: 0x000E32A0
		private static ExDateTime GetMinimumExDateTime(ExDateTime date1, ExDateTime date2)
		{
			if (ExDateTime.Compare(date1, date2) > 0)
			{
				return date2;
			}
			return date1;
		}

		// Token: 0x06004393 RID: 17299 RVA: 0x000E50B0 File Offset: 0x000E32B0
		private static ModernGroupMemberType UserMailboxToModernGroupMemberType(UserMailbox item)
		{
			Persona persona = new Persona
			{
				ADObjectId = item.ADObjectId.ObjectGuid,
				DisplayName = item.DisplayName,
				Alias = item.Alias,
				Title = item.Title,
				ImAddress = item.ImAddress
			};
			persona.PersonaId = IdConverter.PersonaIdFromADObjectId(persona.ADObjectId);
			persona.EmailAddress = new EmailAddressWrapper
			{
				Name = (persona.DisplayName ?? string.Empty),
				EmailAddress = item.SmtpAddress.ToString(),
				RoutingType = "SMTP",
				MailboxType = MailboxHelper.MailboxTypeType.Mailbox.ToString()
			};
			return new ModernGroupMemberType
			{
				Persona = persona,
				IsOwner = item.IsOwner
			};
		}

		// Token: 0x06004394 RID: 17300 RVA: 0x000E518C File Offset: 0x000E338C
		private static ModernGroupMemberType ParticipantToModernGroupMemberType(UnifiedGroupParticipant participant)
		{
			return new ModernGroupMemberType
			{
				Persona = UnifiedGroupsHelper.UnifiedGroupParticipantToPersona(participant),
				IsOwner = participant.IsOwner
			};
		}

		// Token: 0x06004395 RID: 17301 RVA: 0x000E51F8 File Offset: 0x000E33F8
		private List<UserMailbox> GetMembershipChangesFromGroupMailbox(ExDateTime changeDate)
		{
			List<UserMailbox> results = null;
			GroupMailboxAccessLayer.Execute("GetMembershipChanges", this.adSession, this.groupMailboxLocator.MailboxGuid, this.organizationId, this.clientInfoString, delegate(GroupMailboxAccessLayer accessLayer)
			{
				results = accessLayer.GetMembershipChanges(this.groupMailboxLocator, changeDate, true, new int?(this.maxRows + 1)).ToList<UserMailbox>();
			});
			return results;
		}

		// Token: 0x06004396 RID: 17302 RVA: 0x000E5259 File Offset: 0x000E3459
		private UserMailboxLocator CreateUserMailboxLocator(ADRawEntry adEntry)
		{
			return new UserMailboxLocator(this.adSession, (string)adEntry[ADRecipientSchema.ExternalDirectoryObjectId], (string)adEntry[ADRecipientSchema.LegacyExchangeDN]);
		}

		// Token: 0x06004397 RID: 17303 RVA: 0x000E5288 File Offset: 0x000E3488
		private void InitGroupMemberComparer(string serializedPeopleIKnowGraph)
		{
			if (this.sortOrder == ModernGroupMembersSortOrder.PeopleIKnow && !string.IsNullOrEmpty(serializedPeopleIKnowGraph))
			{
				try
				{
					this.groupMemberComparer = new ModernGroupMemberComparerByRelevanceScore(serializedPeopleIKnowGraph, ExTraceGlobals.ModernGroupsTracer);
					return;
				}
				catch (SerializationException arg)
				{
					ExTraceGlobals.ModernGroupsTracer.TraceError<SerializationException, string>((long)this.GetHashCode(), "SerializationException: {0} when data passed in is {1}.", arg, serializedPeopleIKnowGraph);
					this.groupMemberComparer = ModernGroupMemberComparerByDisplayName.Singleton;
					return;
				}
			}
			if (this.sortOrder == ModernGroupMembersSortOrder.DisplayName)
			{
				this.groupMemberComparer = ModernGroupMemberComparerByDisplayName.Singleton;
				return;
			}
			this.groupMemberComparer = ModernGroupMemberComparerByOwnerAndDisplayName.Singleton;
		}

		// Token: 0x0400276E RID: 10094
		private const int DefaultResultSetSizeForPeopleIKnow = 1900;

		// Token: 0x0400276F RID: 10095
		private static readonly PropertyDefinition[] AdProperties = UserMailboxBuilder.AllADProperties.Concat(new PropertyDefinition[]
		{
			ADRecipientSchema.LegacyExchangeDN,
			ADRecipientSchema.ExternalDirectoryObjectId
		}).ToArray<PropertyDefinition>();

		// Token: 0x04002770 RID: 10096
		private readonly IRecipientSession adSession;

		// Token: 0x04002771 RID: 10097
		private readonly OrganizationId organizationId;

		// Token: 0x04002772 RID: 10098
		private readonly GroupMailboxLocator groupMailboxLocator;

		// Token: 0x04002773 RID: 10099
		private readonly string clientInfoString;

		// Token: 0x04002774 RID: 10100
		private readonly ModernGroupMembersSortOrder sortOrder;

		// Token: 0x04002775 RID: 10101
		private readonly int maxRows;

		// Token: 0x04002776 RID: 10102
		private readonly RequestDetailsLogger requestDetailsLogger;

		// Token: 0x04002777 RID: 10103
		private IComparer<ModernGroupMemberType> groupMemberComparer;

		// Token: 0x04002778 RID: 10104
		private List<ModernGroupMemberType> ownersFromAd;

		// Token: 0x04002779 RID: 10105
		private List<ModernGroupMemberType> recentlyRemovedMembers;

		// Token: 0x0400277A RID: 10106
		private List<ModernGroupMemberType> recentlyAddedMembers;

		// Token: 0x0400277B RID: 10107
		private Stopwatch stopWatch = new Stopwatch();
	}
}
